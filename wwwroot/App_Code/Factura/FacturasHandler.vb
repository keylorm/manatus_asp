Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler
Imports Orbelink.Entity.Productos
Imports Orbelink.Entity.Facturas
Imports Orbelink.Entity.Acciones
Imports Orbelink.Control.Acciones
Namespace Orbelink.Control.Facturas

    Public Class FacturasHandler

        Dim querybuilder As Orbelink.DBHandler.QueryBuilder
        Dim connection As Orbelink.DBHandler.SQLServer

        Enum EstadoFactura As Integer
            NA = 0
            Unknown_Client = 1
            Unapproved_Cart = 2
            Approved_Cart = 3
            Transaction_Successful = 4
            Transaction_Unsuccessful = 5
        End Enum

        Private Shared local_Config_ImpuestoVentas As Integer = 0
        Public Shared Property Config_ImpuestoVentas() As Integer
            Get
                'Para quitar esta exception, debe comentar esta linea y en el return escribir el valor correspondiente
                'Throw New NotImplementedException("Debe especificar el valor correspondiente.")
                Return local_Config_ImpuestoVentas
            End Get
            Set(ByVal value As Integer)
                local_Config_ImpuestoVentas = value
            End Set
        End Property

        Public Shared ReadOnly Property Config_MonedaDefault() As Integer
            Get
                Return 1
            End Get
        End Property

        Public Shared ReadOnly Property Config_DiasHabilesParaAccionesPorResiduo() As Integer
            Get
                Return 365
            End Get
        End Property

        Public Shared ReadOnly Property Config_TipoFacturaDefault() As Integer
            Get
                Return 1
            End Get
        End Property

        Public Shared ReadOnly Property Config_TipoFacturaBNCR() As Integer
            Get
                Return 2
            End Get
        End Property

        Public Shared ReadOnly Property Config_Id_ShippingAddressDefault() As Integer
            Get
                Return 1
            End Get
        End Property

        Enum Resultado_Code As Integer
            OK
            errorDetalle
            errorItem
            ErrorGeneral
            PrecioDistinto
            NoExiste
            DuenoYaEnFactura
            FacturaAprovada
            FacturaConsumida
            FacturaNoPuedeSerModificada
        End Enum

        Public Function obtenerMensajeError(ByVal code As Resultado_Code) As String
            Dim mensaje As String = ""
            Select Case code
                Case Resultado_Code.errorDetalle
                    mensaje = "Error al agregar el detalle"

                Case Resultado_Code.errorItem
                    mensaje = "Error al agregar el Item"
                Case Resultado_Code.PrecioDistinto
                    mensaje = "Precio unitario diferente al que ya existe en el detalle"
            End Select

            Return mensaje
        End Function

        Sub New(ByVal theConnection As Orbelink.DBHandler.SQLServer)
            querybuilder = New QueryBuilder
            connection = theConnection
        End Sub

        Sub New(ByVal theConnectionString As String)
            querybuilder = New QueryBuilder
            connection = New SQLServer(theConnectionString)
        End Sub

        'Facturas
        Public Function CreateFactura(ByVal Comprador As Integer, ByVal Vendedor As Integer, ByVal tipoFactura As Integer, ByVal Id_Moneda As Integer) As Integer
            Dim porcentajeImpuestos As Double = buscarPorcentajeImpuestos(tipoFactura)
            Dim id_FacturaTemp As Integer = insertFactura(Comprador, Vendedor, tipoFactura, Id_Moneda, porcentajeImpuestos)
            Dim carrito As New FacturaWebState
            carrito.SetIdCarrito(id_FacturaTemp)
            Return id_FacturaTemp
        End Function

        Public Function ConsultarFactura(ByVal id_Factura As Integer) As Factura
            Dim factura As New Factura

            factura.Fields.SelectAll()
            factura.Id_Factura.Where.EqualCondition(id_Factura)
            querybuilder.From.Add(factura)

            Dim resultTable As Data.DataTable = connection.executeSelect_DT(querybuilder.RelationalSelectQuery)
            If resultTable.Rows.Count > 0 Then
                factura = ObjectBuilder.CreateDBTable_BySelectIndex(resultTable.Rows(0), factura)
                Return factura
            End If
            Return Nothing
        End Function

        Public Function ActualizarFactura(ByVal laFactura As Factura, ByVal id_Factura As Integer) As Boolean
            Dim resultado As Boolean = False
            laFactura.Comprobante.ToUpdate = False
            laFactura.Estado_Factura.ToUpdate = False
            laFactura.Id_Factura.ToUpdate = False
            laFactura.Id_Moneda.ToUpdate = False
            laFactura.SubTotal.ToUpdate = False
            laFactura.Total.ToUpdate = False

            'laFactura.Fecha_Cancelado.ToUpdate = False
            'laFactura.Fecha_Factura.ToUpdate = False

            If laFactura.Comprobante.Value.Length > 0 Then
                laFactura.Fecha_Comprobante.ValueUtc = Date.UtcNow
            Else
                laFactura.Fecha_Comprobante.SetToNull()
            End If

            laFactura.Id_Factura.Where.EqualCondition(id_Factura)
            If updateFactura(laFactura) Then
                resultado = True
                Me.ActualizarFacturaMonto(id_Factura)
            End If
            Return resultado
        End Function

        Private Function insertFactura(ByVal Comprador As Integer, ByVal Vendedor As Integer, ByVal tipoFactura As Integer, ByVal Id_Moneda As Integer, ByVal PorcentajeImpuestos As Double) As Integer
            Dim factura As New Factura
            Dim id_Factura As Integer = 0
            Try
                factura.Id_Comprador.Value = Comprador
                factura.id_ShippingAddress.Value = Config_Id_ShippingAddressDefault
                factura.id_Vendedor.Value = Vendedor
                factura.id_TipoFactura.Value = tipoFactura
                factura.Fecha_Factura.ValueUtc = Date.UtcNow
                'factura.Num_Factura.Value = GetMaxNumFactura()
                factura.Estado_Factura.Value = EstadoFactura.Unapproved_Cart
                factura.PorcentajeImpuestos.Value = PorcentajeImpuestos
                factura.SubTotal.Value = 0
                factura.Total.Value = 0
                factura.Id_Moneda.Value = Id_Moneda

                If connection.executeInsert(querybuilder.InsertQuery(factura)) > 0 Then
                    id_Factura = connection.lastKey(factura.TableName, factura.Id_Factura.Name)
                End If
                Return id_Factura
            Catch ex As Exception
            End Try
            Return id_Factura
        End Function

        Private Function updateFactura(ByVal laFactura As Factura) As Boolean
            Try
                connection.executeUpdate(querybuilder.UpdateQuery(laFactura))
                Return True
            Catch exc As Exception
                Return False
            End Try
        End Function

        Public Function ConsumirFactura(ByVal idFactura As Integer, ByVal usuarioResponsable As Integer) As Resultado_Code
            Dim total As Double = ActualizarFacturaMonto(idFactura)
            Dim resultado As Resultado_Code = Resultado_Code.ErrorGeneral
            Dim laFactura As Factura = ConsultarFactura(idFactura)

            If FacturaAbierta(idFactura) Then
                If NuevoEstadoParaFactura(idFactura, EstadoFactura.Transaction_Successful, usuarioResponsable) Then
                    'ojo
                    GenerarAccionesPorFactura(idFactura)

                    'ojo aqui deberia ir el orbeEventos

                    'Desactiva las acciones que tiene esta factura
                    Dim controladoraAcciones As New Orbelink.Control.Acciones.AccionesHandler(connection.connectionString)
                    Dim revisar_T As New Threading.Thread(AddressOf controladoraAcciones.FacturaConsumida_RevisarAcciones)
                    revisar_T.Start(idFactura)

                    resultado = Resultado_Code.FacturaConsumida

                    If total < 0 Then
                        'Genera la accion que le resta
                        Dim machoteHandler As New MachoteAccionesHandler(connection.connectionString)

                        'ojo deberia devolver el id...
                        Dim prefijo As String = "RE_" & laFactura.Id_Factura.Value
                        If prefijo.Length > 4 Then
                            prefijo = prefijo.Remove(4)
                        End If
                        If machoteHandler.CrearMachoteAccion(laFactura.id_Vendedor.Value, "Resto " & laFactura.Id_Factura.Value, prefijo, -1 * total, laFactura.Id_Moneda.Value, Config_DiasHabilesParaAccionesPorResiduo) Then
                            Dim machote As New MachoteAccion
                            Dim machoteGenerado As Integer = connection.lastKey(machote.TableName, machote.Id_MachoteAccion.Name)
                            Dim id_accion As Integer = controladoraAcciones.GenerarAccion(machoteGenerado, laFactura.id_Vendedor.Value)
                            If id_accion > 0 Then
                                Dim facturasMailer As New FacturaMailer(Configuration.Config_DefaultConnectionString)
                                facturasMailer.EnviarEmailAccion(idFactura, id_accion)
                            End If
                        End If
                    End If
                End If
            Else
                resultado = Resultado_Code.FacturaNoPuedeSerModificada
            End If
            Return resultado
        End Function

        Public Function AprovarFactura(ByVal idFactura As Integer, ByVal usuarioResponsable As Integer) As Resultado_Code
            'Dim total As Double = ActualizarFacturaMonto(idFactura)
            Dim resultado As Resultado_Code = Resultado_Code.ErrorGeneral
            If NuevoEstadoParaFactura(idFactura, EstadoFactura.Approved_Cart, usuarioResponsable) Then
                'If total <= 0 Then
                'resultado = ConsumirFactura(idFactura, usuarioResponsable)
                'Else
                resultado = Resultado_Code.FacturaAprovada
                'End If
            End If
            Return resultado
        End Function

        Public Function AprovarFactura(ByVal idFactura As Integer) As Resultado_Code
            Dim laFactura As Factura = ConsultarFactura(idFactura)
            Return AprovarFactura(idFactura, laFactura.Id_Comprador.Value)
        End Function

        Public Function NoConsumirFactura(ByVal idFactura As Integer, ByVal usuarioResponsable As Integer) As Double
            Dim total As Double = ActualizarFacturaMonto(idFactura)
            If NuevoEstadoParaFactura(idFactura, EstadoFactura.Transaction_Unsuccessful, usuarioResponsable) Then
                'Return GenerarAccionesPorFactura(idFactura)
                Return True
            End If
            Return False
        End Function

        Private Function FacturaAbierta(ByVal idFactura As Integer) As Boolean
            Dim estado As EstadoFactura = GetEstadoActual(idFactura)
            If estado <> EstadoFactura.Transaction_Successful And estado <> EstadoFactura.Transaction_Unsuccessful Then
                Return True
            Else
                Return False
            End If
        End Function

        'Factura
        ''' <summary>
        ''' Devuelve el costo total de los articulos de un carrito
        ''' </summary>
        ''' <param name="id_Factura">Carrito a evaluar</param>
        ''' <returns>Sumatoria de costos todos los productos del carrito</returns>
        ''' <remarks></remarks>
        Public Function GetTotal(ByVal id_Factura As String) As Double
            'Consulta sobre monto de factura
            Dim montoFactura As Double = 0
            Dim factura As New Factura
            factura.Id_Factura.Where.EqualCondition(id_Factura)
            factura.Total.ToSelect = True

            Dim ds As Data.DataSet = connection.executeSelect(querybuilder.SelectQuery(factura))
            If ds.Tables.Count > 0 Then
                If ds.Tables(0).Rows.Count > 0 Then
                    Orbelink.DBHandler.ObjectBuilder.CreateObject(ds.Tables(0), 0, factura)
                    montoFactura = factura.Total.Value
                End If
            End If

            Return montoFactura
        End Function

        ''' <summary>
        ''' Obtiene la ultima factura en la base de datos que tenga ese comprador
        ''' </summary>
        ''' <param name="Id_Comprador"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function UltimaFactura(ByVal Id_Comprador As Integer) As Integer
            Dim Factura As New Factura()
            Dim dataset As Data.DataSet
            Dim elId As Integer = -1

            Factura.Fields.SelectAll()
            Factura.Id_Factura.ToSelect = True
            Factura.Id_Comprador.Where.EqualCondition(Id_Comprador)

            querybuilder.Orderby.Add(Factura.Id_Factura, False)
            dataset = connection.executeSelect(querybuilder.SelectQuery(Factura))
            If dataset.Tables.Count > 0 Then
                If dataset.Tables(0).Rows.Count > 0 Then
                    Orbelink.DBHandler.ObjectBuilder.CreateObject(dataset.Tables(0), 0, Factura)
                    elId = Factura.Id_Factura.Value
                End If
            End If
            Return elId
        End Function

        Public Function EliminarFactura(ByVal id_Factura As Integer) As Boolean
            Return True
        End Function

        'Private Function GetMaxNumFactura() As Integer
        '    Dim Factura As New Factura
        '    Dim ds As Data.DataSet

        '    Factura.Num_Factura.AggregateFunction = Orbelink.DBHandler.Field.AggregateFunctionList.MAXIMUM
        '    ds = connection.executeSelect(querybuilder.SelectQuery(Factura))

        '    Try
        '        If ds.Tables.Count > 0 Then
        '            If ds.Tables(0).Rows.Count > 0 Then
        '                Orbelink.DBHandler.ObjectBuilder.CreateObject(ds.Tables(0), 0, Factura)
        '                Return Factura.Num_Factura.Value + 1
        '            End If
        '        End If
        '    Catch ex As Exception

        '    End Try

        '    Return 1
        'End Function

        Private Function ActualizarFacturaMonto(ByVal id_factura As Integer) As Double
            Dim detalle As New DetalleFactura
            Dim factura As New Factura
            Dim dataSet As New Data.DataSet
            Dim total As Double = 0

            querybuilder.Join.EqualCondition(factura.Id_Factura, detalle.Id_Factura)

            detalle.MontoVenta.AggregateFunction = Field.AggregateFunctionList.SUMMATION
            factura.Id_Factura.Where.EqualCondition(id_factura)
            factura.PorcentajeImpuestos.ToSelect = True

            querybuilder.From.Add(detalle)
            querybuilder.From.Add(factura)

            Dim consulta As String = querybuilder.RelationalSelectQuery
            dataSet = connection.executeSelect(consulta)
            If dataSet.Tables.Count > 0 Then
                If dataSet.Tables(0).Rows.Count > 0 Then
                    ObjectBuilder.CreateObject(dataSet.Tables(0), 0, detalle)
                    ObjectBuilder.CreateObject(dataSet.Tables(0), 0, factura)

                    Dim factura2 As New Factura
                    factura2.SubTotal.Value = detalle.MontoVenta.Value
                    total = detalle.MontoVenta.Value + (detalle.MontoVenta.Value * factura.PorcentajeImpuestos.Value / 100)
                    factura2.Total.Value = total
                    factura2.Id_Factura.Where.EqualCondition(id_factura)
                    consulta = querybuilder.UpdateQuery(factura2)
                    connection.executeUpdate(consulta)
                End If
            End If
            Return total
        End Function

        'DetalleFactura e items
        Public Function AgregaDetalle_FromItem(ByVal id_Factura As Integer, ByVal Id_Producto As Integer, ByVal ordinal As Integer, ByVal montoItemUnitario As Double, ByVal montoItemUnitarioExtra As Double, ByVal cantidad As Integer, ByVal PorcentajeDescuento As Double, Optional ByVal nombreDisplay As String = "") As Resultado_Code
            Dim item As New Item
            item.Fields.SelectAll()
            item.Id_producto.Where.EqualCondition(Id_Producto)
            item.ordinal.Where.EqualCondition(ordinal)
            Dim resultTable As Data.DataTable = connection.executeSelect_DT(querybuilder.SelectQuery(item))

            If resultTable.Rows.Count > 0 Then
                item = ObjectBuilder.CreateDBTable_BySelectIndex(resultTable.Rows(0), item)
                Dim controladora As New HandlerDetalleFactura_Item()
                Return AgregarDetalleFactura(controladora, id_Factura, item, cantidad, montoItemUnitario, montoItemUnitarioExtra, PorcentajeDescuento, nombreDisplay)
            End If
            Return Resultado_Code.NoExiste
        End Function

        Public Function AgregaDetalle_FromProducto(ByVal id_Factura As Integer, ByVal Id_Producto As Integer, ByVal Cantidad As Integer, ByVal PorcentajeDescuento As Double, Optional ByVal nombreDisplay As String = "") As Resultado_Code
            'Busca de donde tomar el precio
            Dim montoItemUnitario As Double = 0
            Try
                Dim Producto As New Producto
                Dim ds As Data.DataSet
                Producto.Fields.SelectAll()
                Producto.Id_Producto.Where.EqualCondition(Id_Producto)
                ds = connection.executeSelect(querybuilder.SelectQuery(Producto))

                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        Orbelink.DBHandler.ObjectBuilder.CreateObject(ds.Tables(0), 0, Producto)
                        montoItemUnitario = Producto.Precio_Unitario.Value

                        Dim controladora As New HandlerDetalleFactura_Producto()
                        Return AgregarDetalleFactura(controladora, id_Factura, Producto, Cantidad, montoItemUnitario, 0, PorcentajeDescuento, nombreDisplay)
                    End If
                End If
            Catch ex As Exception
            End Try

            Return Resultado_Code.NoExiste
        End Function

        Public Function AgregarDetalleFactura(ByVal controladora As IDetalleFacturaHandler, ByVal id_Factura As Integer, ByVal dueno As DBTable, ByVal cantidad As Integer, ByVal Precio_Unitario As Double, ByVal Precio_unitario_Extra As Double, ByVal elPorcentajeDescuento As Double, Optional ByVal nombreDisplay As String = "") As Resultado_Code
            Dim code As Resultado_Code = Resultado_Code.errorDetalle
            Try
                Dim precioVenta As Double = (Precio_Unitario + Precio_unitario_Extra) * cantidad
                Dim descuento As Double = precioVenta * elPorcentajeDescuento / 100
                precioVenta -= descuento

                Dim campoDetalle As Integer = ultimoDetalle(id_Factura) + 1

                'Agrega el detalle
                Dim detalleFactura As New DetalleFactura
                detalleFactura.Id_Factura.Value = id_Factura
                detalleFactura.Detalle.Value = campoDetalle
                detalleFactura.Cantidad.Value = cantidad
                detalleFactura.MontoVenta.Value = precioVenta
                detalleFactura.Precio_Unitario.Value = Precio_Unitario
                detalleFactura.Precio_Unitario_Extra.Value = Precio_unitario_Extra
                detalleFactura.Descuento.Value = descuento
                If nombreDisplay.Length > 0 Then
                    detalleFactura.NombreDisplay.Value = nombreDisplay
                Else
                    detalleFactura.NombreDisplay.Value = controladora.GetNombreDisplay(connection.connectionString, dueno)
                End If

                If insertDetalleFactura(detalleFactura) Then
                    Dim detalleExistente As Integer = controladora.FindDetalleByDueno(connection.connectionString, id_Factura, dueno)
                    If detalleExistente < 0 Then
                        If controladora.AgregarDetalleFactura(connection.connectionString, id_Factura, campoDetalle, dueno) Then
                            code = Resultado_Code.OK
                        Else
                            code = Resultado_Code.errorDetalle
                            deleteDetalleFactura(id_Factura, campoDetalle)
                        End If
                    Else
                        code = Resultado_Code.DuenoYaEnFactura
                    End If
                End If

                'Actualiza monto factua
                Me.ActualizarFacturaMonto(id_Factura)
            Catch ex As Exception
            End Try
            Return code
        End Function

        Public Function ActualizarDetalleCantidad(ByVal id_Factura As Integer, ByVal detalle As Integer, ByVal cantidad As Integer) As Resultado_Code
            Dim code As Resultado_Code = Resultado_Code.errorDetalle
            Dim dataSet As New Data.DataSet
            Try
                Dim detalleFactura As New DetalleFactura
                detalleFactura.Id_Factura.Where.EqualCondition(id_Factura)
                detalleFactura.Detalle.Where.EqualCondition(detalle)

                'consulta

                detalleFactura.Precio_Unitario.ToSelect = True
                detalleFactura.Precio_Unitario_Extra.ToSelect = True
                detalleFactura.Descuento.ToSelect = True
                querybuilder.From.Add(detalleFactura)

                Dim consulta As String = querybuilder.RelationalSelectQuery
                dataSet = connection.executeSelect(consulta)
                If dataSet.Tables.Count > 0 Then
                    If dataSet.Tables(0).Rows.Count > 0 Then
                        ObjectBuilder.CreateObject(dataSet.Tables(0), 0, detalleFactura)
                        Dim precioVenta As Double = (detalleFactura.Precio_Unitario.Value + detalleFactura.Precio_Unitario_Extra.Value) * cantidad
                        Dim descuento As Double = precioVenta * detalleFactura.Descuento.Value / 100
                        precioVenta -= descuento

                        detalleFactura.Cantidad.Value = cantidad
                        detalleFactura.MontoVenta.Value = precioVenta
                        detalleFactura.Id_Factura.Where.EqualCondition(id_Factura)
                        detalleFactura.Detalle.Where.EqualCondition(detalle)

                        If connection.executeUpdate(querybuilder.UpdateQuery(detalleFactura)) Then
                            code = Resultado_Code.OK
                        Else
                            code = Resultado_Code.errorDetalle
                        End If

                        'Actualiza monto factua
                        Me.ActualizarFacturaMonto(id_Factura)
                    End If
                End If
            Catch ex As Exception
            End Try
            Return code
        End Function

        'Private Sub ActualizarDetalleFactura()
        '    Dim detalleFactura As New DetalleFactura

        '    'Si existe, entonces actualiza los valores
        '    detalleFactura.Id_Factura.ToUpdate = False
        '    detalleFactura.Descuento.ToUpdate = False
        '    detalleFactura.Descuento.ToUpdate = False
        '    detalleFactura.Precio_Unitario.ToUpdate = False

        '    If detalleFactura.Precio_Unitario.Value = Precio_Unitario Then
        '        detalleFactura.Cantidad.Value += cantidad
        '        detalleFactura.MontoVenta.Value += precio_Venta
        '        detalleFactura.Id_Factura.Where.EqualCondition(idFactura)
        '        'detalleFactura.Id_Producto.Where.EqualCondition(id_Producto)
        '        detalleFactura.Detalle.Where.EqualCondition(campoDetalle)
        '    Else
        '        code = Resultado_Code.PrecioDistinto
        '        Throw New Exception("PrecioDistinto")
        '    End If

        '    code = updateDetalleFactura(detalleFactura)
        'End Sub

        '''' <summary>
        '''' Actualiza un item en un carrito
        '''' </summary>
        '''' <param name="id_Factura">Carrito en el que se encuentra el item</param>
        '''' <param name="Id_Producto">Producto a modificar en ese carrito</param>
        '''' <param name="Cantidad">Nueva cantidad del producto</param>
        '''' <returns>Si la operacion sue exitosa o no</returns>
        '''' <remarks></remarks>
        'Public Function ActualizaItem(ByVal id_Factura As String, ByVal Id_Producto As Integer, ByVal CampoDetalle As Integer, ByVal Cantidad As Integer) As Integer
        '    Dim montoItemUnitario As Double = 0

        '    Busca de donde tomar el precio
        '    Try
        '        Dim detalleConsultar As New DetalleFactura
        '        Dim ds As Data.DataSet
        '        detalleConsultar.Fields.SelectAll()
        '        detalleConsultar.Id_Factura.Where.EqualCondition(id_Factura)
        '        detalleConsultar.Id_Producto.Where.EqualCondition(Id_Producto)
        '        detalleConsultar.Detalle.Where.EqualCondition(CampoDetalle)
        '        ds = connection.executeSelect(querybuilder.SelectQuery(detalleConsultar))

        '        If ds.Tables.Count > 0 Then
        '            If ds.Tables(0).Rows.Count > 0 Then
        '                Orbelink.DBHandler.ObjectBuilder.CreateObject(ds.Tables(0), 0, detalleConsultar)
        '            End If
        '        End If
        '    Catch ex As Exception
        '    End Try

        '    Dim detalle As New DetalleFactura
        '    Dim precioVenta As Double = (detalleConsultar.Precio_Unitario.Value + detalleConsultar.Precio_Unitario_Extra.Value) * Cantidad
        '    Dim porcentajeDescuento As Double = detalleConsultar.Descuento.Value * 100 / detalleConsultar.MontoVenta.Value
        '    Dim descuento As Double = precioVenta * porcentajeDescuento / 100
        '    precioVenta -= descuento

        '    Try
        '        detalle.Cantidad.Value = Cantidad
        '        detalle.Descuento.Value = descuento
        '        detalle.MontoVenta.Value = precioVenta

        '        detalle.Id_Factura.Where.EqualCondition(id_Factura)
        '        detalle.Id_Producto.Where.EqualCondition(Id_Producto)
        '        detalle.CampoLlave.Where.EqualCondition(CampoLlave)
        '        If connection.executeUpdate(querybuilder.UpdateQuery(detalle)) > 0 Then
        '            Me.ActualizarMontoFactura(id_Factura)
        '            Return True
        '        End If
        '    Catch ex As Exception

        '    End Try
        '    Return False
        'End Function

        ''' <summary>
        ''' Elimina un item de un carrito
        ''' </summary>
        ''' <param name="id_Factura">Carrito en el que se encuentra el item</param>
        ''' <param name="Detalle">Item a eliminar del carrito</param>
        ''' <returns>Si la operacion fue exitosa o no</returns>
        ''' <remarks></remarks>
        Public Function EliminaDetalleFactura(ByVal id_Factura As String, ByVal Detalle As Integer) As Boolean
            If deleteDetalleFactura(id_Factura, Detalle) Then
                ActualizarFacturaMonto(id_Factura)
                Return True
            Else
                Return False
            End If
        End Function

        ''' <summary>
        ''' Devuelve los items actualmente agregados a un carrito
        ''' </summary>
        ''' <param name="id_Factura">El carrito a consultar</param>
        ''' <returns>Dataset con los items</returns>
        ''' <remarks></remarks>
        Public Function GetItems(ByVal id_Factura As Integer, ByVal theDetalleFactura As DetalleFactura) As Data.DataTable
            If theDetalleFactura Is Nothing Then
                theDetalleFactura = New DetalleFactura
            End If
            theDetalleFactura.Fields.SelectAll()
            theDetalleFactura.Id_Factura.Where.EqualCondition(id_Factura)
            querybuilder.From.Add(theDetalleFactura)
            Return connection.executeSelect_DT(querybuilder.RelationalSelectQuery)
        End Function

        Public Function GetItemsProductos(ByVal id_Factura As Integer) As Data.DataTable
            Dim theDetalleFactura As New DetalleFactura
            theDetalleFactura.Fields.SelectAll()
            theDetalleFactura.Id_Factura.Where.EqualCondition(id_Factura)
            Dim producto As New Producto
            producto.Id_Producto.ToSelect = True
            producto.SKU.ToSelect = True
            Dim DetalleFactura_Producto As New DetalleFactura_Producto
            DetalleFactura_Producto.Id_Factura.Where.EqualCondition(id_Factura)
            querybuilder.Join.EqualCondition(theDetalleFactura.Detalle, DetalleFactura_Producto.Detalle)
            'querybuilder.Join.EqualCondition(DetalleFactura_Producto.Id_Factura, theDetalleFactura.Id_Factura)
            querybuilder.Join.EqualCondition(DetalleFactura_Producto.Id_Producto, producto.Id_Producto)
            querybuilder.From.Add(theDetalleFactura)
            querybuilder.From.Add(producto)
            querybuilder.From.Add(DetalleFactura_Producto)
            querybuilder.Distinct = True
            Return connection.executeSelect_DT(querybuilder.RelationalSelectQuery)
        End Function

        ''' <summary>
        ''' Regresa la cantidad de productos en un carrito
        ''' </summary>
        ''' <param name="id_Factura">Carrito a evaluar</param>
        ''' <returns>Cantidad de items</returns>
        ''' <remarks></remarks>
        Public Function GetCantidadItems(ByVal id_Factura As String) As Integer
            Dim DetalleFactura As New DetalleFactura
            DetalleFactura.Detalle.AggregateFunction = Orbelink.DBHandler.Field.AggregateFunctionList.COUNT
            DetalleFactura.Id_Factura.Where.EqualCondition(id_Factura)
            Dim ds As Data.DataSet = connection.executeSelect(querybuilder.SelectQuery(DetalleFactura))
            If ds.Tables.Count > 0 Then
                If ds.Tables(0).Rows.Count > 0 Then
                    Orbelink.DBHandler.ObjectBuilder.CreateObject(ds.Tables(0), 0, DetalleFactura)
                    Return DetalleFactura.Detalle.Value
                End If
            End If
            Return 0
        End Function

        Private Function loadDetalleFactura(ByVal id_Factura As Integer, ByVal campoDetalle As Integer) As DetalleFactura
            Dim detalleFactura As New DetalleFactura
            Dim existe As Boolean = False

            detalleFactura.Fields.SelectAll()
            detalleFactura.Id_Factura.Where.EqualCondition(id_Factura)
            detalleFactura.Detalle.Where.EqualCondition(campoDetalle)
            querybuilder.From.Add(detalleFactura)

            Dim dataset As Data.DataSet = connection.executeSelect(querybuilder.RelationalSelectQuery)
            If dataset.Tables.Count > 0 Then
                If dataset.Tables(0).Rows.Count > 0 Then
                    Orbelink.DBHandler.ObjectBuilder.CreateObject(dataset.Tables(0), 0, detalleFactura)
                    Return detalleFactura
                End If
            End If
            Return Nothing
        End Function

        Private Function insertDetalleFactura(ByVal DetalleFactura As DetalleFactura) As Boolean
            Try
                connection.executeInsert(querybuilder.InsertQuery(DetalleFactura))
                Return True
            Catch exc As Exception
                Return False
            End Try
        End Function

        Private Function updateDetalleFactura(ByVal DetalleFactura As DetalleFactura) As Resultado_Code
            Try
                connection.executeUpdate(querybuilder.UpdateQuery(DetalleFactura))
                Return Resultado_Code.OK
            Catch exc As Exception
                Return Resultado_Code.errorDetalle
            End Try
        End Function

        Private Function deleteDetalleFactura(ByVal id_Factura As Integer, ByVal detalle As Integer) As Boolean
            Try
                Dim DetalleFactura As New DetalleFactura
                DetalleFactura.Id_Factura.Where.EqualCondition(id_Factura)
                DetalleFactura.Detalle.Where.EqualCondition(detalle)
                If connection.executeDelete(querybuilder.DeleteQuery(DetalleFactura)) > 0 Then
                    Return True
                End If
            Catch exc As Exception

            End Try
            Return False
        End Function

        Private Function ultimoDetalle(ByVal idFactura As Integer) As Integer
            Dim detalle As New DetalleFactura
            Dim resultado As Integer = -1

            detalle.Detalle.ToSelect = True
            detalle.Id_Factura.Where.EqualCondition(idFactura)
            querybuilder.From.Add(detalle)
            querybuilder.Orderby.Add(detalle.Detalle, False)
            querybuilder.Top = 1

            Dim dataTable As Data.DataTable = connection.executeSelect_DT(querybuilder.RelationalSelectQuery)
            If dataTable.Rows.Count > 0 Then
                Orbelink.DBHandler.ObjectBuilder.CreateObject(dataTable, 0, detalle)
                resultado = detalle.Detalle.Value
            End If

            Return resultado
        End Function

        'Estados
        Public Function GetEstados() As ArrayList
            Dim losEstados As New ArrayList
            losEstados.Add("NA")
            losEstados.Add("Unknown Client")
            losEstados.Add("Unapproved Cart")
            losEstados.Add("Approved Cart")
            losEstados.Add("Transaction Successful")
            losEstados.Add("Transaction Unsuccessful")
            Return losEstados
        End Function

        ''' <summary>
        ''' Regresa el estado actual del carrito
        ''' </summary>
        ''' <param name="id_Carrito">Carrito a evaluar</param>
        ''' <returns>estado actual</returns>
        ''' <remarks></remarks>
        Public Function GetEstadoActual(ByVal id_Carrito As String) As EstadoFactura
            Dim Carro As New Factura
            Dim ds As Data.DataSet
            Dim estadoActual As Integer = 1
            Carro.Estado_Factura.ToSelect = True
            Carro.Id_Factura.Where.EqualCondition(id_Carrito)

            ds = connection.executeSelect(querybuilder.SelectQuery(Carro))
            If ds.Tables(0).Columns.Count > 0 Then
                If ds.Tables(0).Rows.Count > 0 Then
                    ObjectBuilder.CreateObject(ds.Tables(0), 0, Carro)
                    estadoActual = Carro.Estado_Factura.Value
                End If
            End If
            Return estadoActual
        End Function

        Private Function NuevoEstadoParaFactura(ByVal Id_Factura As Integer, ByVal elEstado As EstadoFactura, ByVal Responsable As Integer) As Boolean
            'Dim historialEstados As New Orbelink.Control.HistorialEstados.HistorialHandler(connection)
            'Dim controladora As New Controladora_HistorialEstados_Factura
            'If HistorialEstados.NuevoEstado(controladora, Id_Factura, Id_TipoEstadoFactura, Responsable) Then
            'If elEstado = EstadoFactura.Approved_Cart Then
            '    Return False
            'Else
            Return CambiarEstadoFactura(Id_Factura, elEstado)
            'End If
            'End If
        End Function

        ''' <summary>
        ''' Cambia el estado a una Factura. NO Existe historial de estados de una Factura
        ''' </summary>
        ''' <param name="id_Factura"></param>
        ''' <param name="NuevoTipoEstado"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function CambiarEstadoFactura(ByVal id_Factura As Integer, ByVal NuevoTipoEstado As Integer) As Boolean
            Try
                Dim factura As New Factura
                factura.Id_Factura.Where.EqualCondition(id_Factura)
                factura.Estado_Factura.Value = NuevoTipoEstado

                If NuevoTipoEstado = EstadoFactura.Approved_Cart Then
                    factura.Fecha_Cancelado.ValueUtc = Date.UtcNow
                End If

                Dim query As New QueryBuilder
                If connection.executeUpdate(query.UpdateQuery(factura)) > 0 Then
                    Return True
                End If
            Catch ex As Exception

            End Try
            Return False
        End Function

        'Cliente
        ''' <summary>
        ''' Regresa el Cliente del carrito
        ''' </summary>
        ''' <param name="id_Carrito">Carrito a evaluar</param>
        ''' <returns>Cliente</returns>
        ''' <remarks></remarks>
        Public Function GetIdCliente(ByVal id_Carrito As String) As Integer
            Dim Carro As New Factura
            Dim ds As Data.DataSet
            Dim Cliente As Integer = 0
            Carro.Id_Comprador.ToSelect = True
            Carro.Id_Factura.Where.EqualCondition(id_Carrito)

            ds = connection.executeSelect(querybuilder.SelectQuery(Carro))
            If ds.Tables(0).Columns.Count > 0 Then
                If ds.Tables(0).Rows.Count > 0 Then
                    ObjectBuilder.CreateObject(ds.Tables(0), 0, Carro)
                    Cliente = Carro.Id_Comprador.Value
                End If
            End If
            Return Cliente
        End Function

        ''' <summary>
        ''' Elimina todos los productos de un carrito determinado
        ''' </summary>
        ''' <param name="id_Factura">Carrito al que pertenecen los productos que se van a eliminar</param>
        ''' <returns>Si la operacion fue exitosa o no</returns>
        ''' <remarks></remarks>
        Public Function LimpaCarrito(ByVal id_Factura As String) As Integer
            Dim DetalleFactura As New DetalleFactura
            Try
                DetalleFactura.Id_Factura.Where.EqualCondition(id_Factura)
                If connection.executeDelete(querybuilder.DeleteQuery(DetalleFactura)) > 0 Then
                    Return True
                End If
            Catch ex As Exception

            End Try
            Return False
        End Function

        Public Function RegistrarComprobante(ByVal id_factura As Integer, ByVal elComprobante As String) As Boolean
            Dim factura As New Factura
            Try
                If elComprobante.Length > 0 Then
                    factura.Comprobante.Value = elComprobante
                    factura.Fecha_Comprobante.ValueUtc = Date.UtcNow
                    factura.Id_Factura.Where.EqualCondition(id_factura)
                    If connection.executeUpdate(querybuilder.UpdateQuery(factura)) > 0 Then
                        Return True
                    End If
                End If
            Catch ex As Exception
            End Try
            Return False
        End Function

        'TipoFactura
        Public Function buscarPorcentajeImpuestos(ByVal Id_TipoFactura As Integer) As Double
            Dim tipo As New TipoFactura
            Dim elPorcentaje As Double = 0.0

            tipo.Fields.SelectAll()
            tipo.Id_TipoFactura.Where.EqualCondition(Id_TipoFactura)
            Dim dataTable As Data.DataTable = connection.executeSelect_DT(querybuilder.SelectQuery(tipo))
            If dataTable.Rows.Count > 0 Then
                Orbelink.DBHandler.ObjectBuilder.CreateObject(dataTable, 0, tipo)
                elPorcentaje = tipo.PorcentajeImpuestos.Value
            End If
            Return elPorcentaje
        End Function




        'Metodos de SERGIO
        Public Function GenerarAccionesPorFactura(ByVal id_factura As Integer) As Boolean

            Dim result As Boolean = False

            Dim AccionesHandler As AccionesHandler = New AccionesHandler(connection.connectionString)
            Dim machoteAccionesHandler As MachoteAccionesHandler = New MachoteAccionesHandler(connection.connectionString)
            Dim DetalleFactura As New DetalleFactura
            DetalleFactura.Fields.SelectAll()
            Dim DS_productos As Data.DataSet
            Dim DetalleFactura_Producto As New DetalleFactura_Producto
            DetalleFactura_Producto.Id_Producto.ToSelect = True
            DetalleFactura_Producto.Id_Factura.Where.EqualCondition(id_factura)
            DetalleFactura.Id_Factura.Where.EqualCondition(id_factura)
            Dim producto As New Producto
            producto.Id_tipoProducto.ToSelect = True
            producto.Id_Moneda.ToSelect = True
            producto.Precio_Unitario.ToSelect = True
            querybuilder.Join.EqualCondition(DetalleFactura_Producto.Id_Producto, producto.Id_Producto)
            querybuilder.Join.EqualCondition(DetalleFactura_Producto.Detalle, DetalleFactura.Detalle)
            querybuilder.From.Add(DetalleFactura_Producto)
            querybuilder.From.Add(DetalleFactura)
            querybuilder.From.Add(producto)
            DS_productos = connection.executeSelect(querybuilder.RelationalSelectQuery)

            If DS_productos.Tables.Count > 0 Then
                If DS_productos.Tables(0).Rows.Count > 0 Then
                    For i2 As Integer = 0 To DS_productos.Tables(0).Rows.Count - 1
                        ObjectBuilder.CreateObject(DS_productos.Tables(0), i2, DetalleFactura)
                        ObjectBuilder.CreateObject(DS_productos.Tables(0), i2, producto)
                        Dim facturasMailer As New FacturaMailer(Configuration.Config_DefaultConnectionString)
                        If producto.Id_tipoProducto.Value = 1 Then 'SOBRES DE EMOCION
                            Dim cliente As Integer = GetIdCliente(id_factura)
                            Dim machote As New MachoteAccion
                            machote.CreadoPor.Value = cliente
                            machote.DiasValidez.Value = Config_DiasHabilesParaAccionesPorResiduo
                            machote.FechaCreado.ValueUtc = Date.UtcNow
                            machote.Moneda.Value = producto.Id_Moneda.Value
                            machote.PrefijoCodigo.Value = "SE"
                            machote.Valor.Value = DetalleFactura.Precio_Unitario.Value
                            machote.Nombre.Value = DetalleFactura.NombreDisplay.Value
                            If machoteAccionesHandler.CrearMachoteAccion(machote) Then
                                Dim id_machote As Integer = connection.lastKey(machote.TableName, machote.Id_MachoteAccion.Name)
                                If id_machote > 0 Then
                                    For cantidad As Integer = 0 To DetalleFactura.Cantidad.Value - 1
                                        Dim id_accion As Integer = AccionesHandler.GenerarAccion(id_machote, cliente)
                                        If id_accion > 0 Then
                                            result = facturasMailer.EnviarEmailAccion(id_factura, id_accion)
                                        End If
                                    Next
                                End If
                            End If
                        Else
                            result = facturasMailer.EnviarEmailRegalo(id_factura, DetalleFactura.NombreDisplay.Value)
                        End If
                    Next
                End If
            End If
      
            Return result
        End Function


        Public Function GetAccionesPorFactura(ByVal id_factura As Integer) As Data.DataTable
            Dim DetalleFactura_Accion As New DetalleFactura_Accion
            DetalleFactura_Accion.Id_Factura.Where.EqualCondition(id_factura)
            Dim Accion As New Accion
            Accion.Fields.SelectAll()
            querybuilder.Join.EqualCondition(DetalleFactura_Accion.Id_Accion, Accion.Id_Accion)
            querybuilder.From.Add(DetalleFactura_Accion)
            querybuilder.From.Add(Accion)
            Return connection.executeSelect_DT(querybuilder.RelationalSelectQuery)
        End Function

        Public Function GetAccionesAplicadasPorFactura(ByVal id_factura As Integer) As Data.DataTable



            Dim DetalleFactura_Producto As New DetalleFactura_Producto
            DetalleFactura_Producto.Id_Producto.ToSelect = True
            DetalleFactura_Producto.Id_Factura.Where.EqualCondition(id_factura)
            Dim producto As New Producto
            producto.Id_tipoProducto.Where.EqualCondition(1) 'Sobres de Emocion
            querybuilder.Join.EqualCondition(DetalleFactura_Producto.Id_Producto, producto.Id_Producto)
            querybuilder.From.Add(DetalleFactura_Producto)
            querybuilder.From.Add(producto)

            Return connection.executeSelect_DT(querybuilder.RelationalSelectQuery)
        End Function

        Public Function insertShippingAddress(ByVal id_entidad As Integer, ByVal actual As Boolean, ByVal nombre As String, ByVal apellido As String, ByVal email As String, ByVal ciudad As String, ByVal region As String, ByVal id_origen As Integer, ByVal tel As String, ByVal tel2 As String, ByVal codigoPostal As String, ByVal direccion1 As String, ByVal direccion2 As String) As Boolean
            Try

                Dim ShippingActual As Integer = 0
                If actual Then
                    ShippingActual = 1
                End If
                Dim Shippings As New Shippings_Address(id_entidad, ShippingActual, nombre, apellido, email, tel, tel2, codigoPostal, ciudad, region, id_origen, direccion1, direccion2)
                connection.executeInsert(querybuilder.InsertQuery(Shippings))
                Return True
            Catch exc As Exception
                Return False
            End Try
        End Function

        Public Function updateShippingAddressActual(ByVal id_entidad As Integer, ByVal nombre As String, ByVal apellido As String, ByVal email As String, ByVal ciudad As String, ByVal region As String, ByVal id_origen As Integer, ByVal tel As String, ByVal tel2 As String, ByVal codigoPostal As String, ByVal direccion1 As String, ByVal direccion2 As String) As Boolean
            Try
                Dim Shippings_Address As New Shippings_Address()
                Shippings_Address.id_entidad.Where.EqualCondition(id_entidad)
                Shippings_Address.Actual.Where.EqualCondition(1)
                Shippings_Address.Id_Pais.Value = id_origen
                Shippings_Address.Nombre.Value = nombre
                Shippings_Address.Apellido.Value = apellido
                Shippings_Address.Email.Value = email
                Shippings_Address.Region.Value = region
                Shippings_Address.Ciudad.Value = ciudad
                Shippings_Address.Codigo_Postal.Value = codigoPostal
                Shippings_Address.Telefono.Value = tel
                Shippings_Address.Fax.Value = tel2
                Shippings_Address.Direccion.Value = direccion1
                Shippings_Address.Direccion_Alterna.Value = direccion2
                connection.executeUpdate(querybuilder.UpdateQuery(Shippings_Address))
                Return True
            Catch exc As Exception
                Return False
            End Try
        End Function

        ''' <summary>
        ''' Actualiza el shipping address de una factura
        ''' </summary>
        ''' <param name="id_Carrito"></param>
        ''' <param name="id_Shipping"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function ActualizaShippingAddressCarrito(ByVal id_Carrito As String, ByVal id_Shipping As String) As Integer
            Dim Factura As New Factura

            Try
                Factura.id_ShippingAddress.Value = id_Shipping
                Factura.Id_Factura.Where.EqualCondition(id_Carrito)
                If connection.executeUpdate(querybuilder.UpdateQuery(Factura)) > 0 Then
                    Return True
                End If
            Catch ex As Exception

            End Try
            Return False
        End Function

        Public Function ActualizaEnviarPorCarrito(ByVal id_Carrito As String, ByVal EnviarPor As String) As Integer
            Dim Factura As New Factura

            Try
                Factura.Enviar_Por.Value = EnviarPor
                Factura.Id_Factura.Where.EqualCondition(id_Carrito)
                If connection.executeUpdate(querybuilder.UpdateQuery(Factura)) > 0 Then
                    Return True
                End If
            Catch ex As Exception

            End Try
            Return False
        End Function

        Public Function GetShippingAddressEntidad(ByVal id_Entidad As Integer) As Integer
            Dim Shippings_Entidad As New Shippings_Address
            Shippings_Entidad.id_Shipping.ToSelect = True
            Shippings_Entidad.Actual.Where.EqualCondition(1)
            Shippings_Entidad.id_entidad.Where.EqualCondition(id_Entidad)
            Dim ds As Data.DataSet
            ds = connection.executeSelect(querybuilder.SelectQuery(Shippings_Entidad))
            If ds.Tables.Count > 0 Then
                If ds.Tables(0).Rows.Count > 0 Then
                    Orbelink.DBHandler.ObjectBuilder.CreateObject(ds.Tables(0), 0, Shippings_Entidad)
                    Return Shippings_Entidad.id_Shipping.Value
                End If
            End If
            Return 0
        End Function

        Public Function ConsultaShippingAddressEntidad(ByVal id_ShippingAddress As Integer) As Shippings_Address
            Dim Shippings_Entidad As New Shippings_Address
            Shippings_Entidad.Fields.SelectAll()

            Shippings_Entidad.id_Shipping.Where.EqualCondition(id_ShippingAddress)
            Dim ds As Data.DataSet
            ds = connection.executeSelect(querybuilder.SelectQuery(Shippings_Entidad))
            If ds.Tables.Count > 0 Then
                If ds.Tables(0).Rows.Count > 0 Then
                    Orbelink.DBHandler.ObjectBuilder.CreateObject(ds.Tables(0), 0, Shippings_Entidad)
                    Return Shippings_Entidad
                End If
            End If
            Return Nothing
        End Function
    End Class

End Namespace