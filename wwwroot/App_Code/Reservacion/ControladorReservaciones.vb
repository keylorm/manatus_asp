Imports Microsoft.VisualBasic
Imports System.Resources
Imports Orbelink.DBHandler
Imports Orbelink.Helpers
Imports Orbelink.Control.Facturas
Imports Orbelink.Control.HistorialEstados
Imports Orbelink.Entity.Reservaciones
Imports Orbelink.Entity.Productos
Imports Orbelink.Entity.Facturas
Imports Orbelink.Entity.Entidades
Imports Orbelink.Entity.Currency

Namespace Orbelink.Control.Reservaciones

    Public Class ControladorReservaciones
        Dim connection As SQLServer
        Dim facturaHandler As FacturasHandler
        Dim resourceMan As ResourceManager
        Dim precioTransporte As Integer

        

        'Variables "estaticas"
        Public Shared ReadOnly Property Config_TipoFacturaReservacion() As Integer
            Get
                'Para quitar esta exception, debe comentar esta linea y en el return escribir el valor correspondiente
                'Throw New NotImplementedException("Debe especificar el valor correspondiente.")
                Return 3
            End Get
        End Property

        Public Shared ReadOnly Property Config_UnidadVenta() As Integer
            Get
                'Para quitar esta exception, debe comentar esta linea y en el return escribir el valor correspondiente
                'Throw New NotImplementedException("Debe especificar el valor correspondiente.")
                Return 1
            End Get
        End Property

        Public Shared ReadOnly Property Config_VendedorDefault() As Integer
            Get
                'Para quitar esta exception, debe comentar esta linea y en el return escribir el valor correspondiente
                'Throw New NotImplementedException("Debe especificar el valor correspondiente.")
                Return 1
            End Get
        End Property

        Public Shared ReadOnly Property Config_UbicacionDefault() As Integer
            Get
                'Para quitar esta exception, debe comentar esta linea y en el return escribir el valor correspondiente
                'Throw New NotImplementedException("Debe especificar el valor correspondiente.")
                Return 1
            End Get
        End Property

        Public Shared ReadOnly Property Config_HoraEntrada() As Date
            Get
                Dim hora As Integer = 12
                Dim minuto As Integer = 0
                Dim fecha As New Date(1, 1, 1, hora, minuto, 0)
                Return fecha
            End Get
        End Property

        Public Shared ReadOnly Property Config_HoraSalida() As Date
            Get
                Dim hora As Integer = 11
                Dim minuto As Integer = 0
                Dim fecha As New Date(1, 1, 1, hora, minuto, 0)
                Return fecha
            End Get
        End Property

        Public Shared ReadOnly Property Config_EstadoIncompleto() As Integer
            Get
                Return 5
            End Get
        End Property

        Public Sub New(ByRef theConnection As SQLServer, ByVal theResourceManager As ResourceManager)
            connection = theConnection
            resourceMan = theResourceManager
            facturaHandler = New FacturasHandler(theConnection)
        End Sub

        '''' <summary>
        '''' Devuelve un dataSet con todos los Items disponibles en el rango de las fechas indicadas
        '''' </summary>
        '''' <param name="Theid_producto"></param>
        '''' <param name="theHoraInicio"></param>
        '''' <param name="theHoraFinal"></param>
        '''' <returns></returns>
        '''' <remarks></remarks>
        'Public Function obtenerDisponibles(ByVal Theid_producto As Integer, ByVal theHoraInicioUtc As Date, ByVal theHoraFinalUtc As Date) As Data.DataSet
        '    Dim id_producto As Integer = Theid_producto
        '    Dim horaInicio As Date = theHoraInicio
        '    Dim horaFin As Date = theHoraFinal
        '    Dim fechaInicio As Date = horaInicio
        '    Dim fechaFin As Date = horaFin

        '    Dim item As New Item
        '    Dim producto As New Producto
        '    'Dim unidad As New Moneda
        '    
        '    Dim tipoEstado As New TipoEstadoReservacion

        '    Dim reservacion As New Reservacion
        '    Dim detalle As New Detalle_Reservacion
        '    Dim query2 As New QueryBuilder()
        '    detalle.ordinal.ToSelect = True

        '    Dim condicion1 As Condition = reservacion.fecha_inicioProgramado.Where.GreaterThanOrEqualCondition(fechaInicio)
        '    Dim condicion2 As Condition = reservacion.fecha_inicioProgramado.Where.LessThanOrEqualCondition(fechaFin)
        '    Dim condicion3 As Condition = query2.GroupConditions(condicion1, condicion2, Where.FieldRelations.OR_)

        '    Dim condicion4 As Condition = reservacion.fecha_finalProgramado.Where.GreaterThanOrEqualCondition(fechaInicio)
        '    Dim condicion5 As Condition = reservacion.fecha_finalProgramado.Where.LessThanOrEqualCondition(fechaFin)
        '    Dim condicion6 As Condition = query2.GroupConditions(condicion4, condicion5, Where.FieldRelations.OR_)

        '    Dim condicion7 As Condition = reservacion.fecha_inicioProgramado.Where.LessThanCondition(fechaInicio)
        '    Dim condicion8 As Condition = reservacion.fecha_finalProgramado.Where.GreaterThanCondition(fechaFin)
        '    Dim condicion9 As Condition = query2.GroupConditions(condicion7, condicion8, Where.FieldRelations.OR_)

        '    Dim condicion10 As Condition = query2.GroupConditions(condicion6, condicion3, Where.FieldRelations.OR_)
        '    query2.GroupConditions(condicion9, condicion10)

        '    tipoEstado.Terminador.Where.EqualCondition(1)

        '    detalle.Id_producto.Where.EqualCondition(id_producto)

        '    query2.Join.EqualCondition(detalle.id_reservacion, reservacion.Id_Reservacion)
        '    query2.Join.EqualCondition(reservacion.Id_TipoEstado, tipoEstado.Id_TipoEstadoReservacion)

        '    query2.From.Add(reservacion)
        '    query2.From.Add(detalle)
        '    Dim consultaInterna2 As String = query2.RelationalSelectQuery

        '    'Hace la consultar grande
        '    item.descripcion.ToSelect = True
        '    'item.Id_Moneda.ToSelect = True
        '    'item.precio.ToSelect = True
        '    item.ordinal.ToSelect = True
        '    producto.Nombre.ToSelect = True
        '    producto.Id_Producto.ToSelect = True
        '    'unidad.Simbolo.ToSelect = True

        '    Dim query As New QueryBuilder
        '    query.Join.EqualCondition(item.Id_producto, producto.Id_Producto)
        '    'query.Join.EqualCondition(unidad.Id_Moneda, item.Id_Moneda)
        '    item.Id_producto.Where.EqualCondition(id_producto)
        '    item.ordinal.Where.NotInCondition(consultaInterna2, Where.FieldRelations.AND_)

        '    query.From.Add(item)
        '    query.From.Add(producto)
        '    'query.From.Add(unidad)

        '    Dim consulta As String = query.RelationalSelectQuery
        '    Dim dataTable As Data.DataTable = connection.executeSelect_DT(consulta)
        '    Return dataSet
        'End Function

        ''' <summary>
        ''' Recibe un id de reservacion y de devuelve un dataSet con todos los items que pertenecen a esa reservacion
        ''' </summary>
        ''' <param name="id_reservacion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function obtenerItemsReservados(ByVal id_reservacion As Integer) As Data.DataTable
            Dim detalle As New Detalle_Reservacion
            Dim item As New Item
            Dim producto As New Producto
            'Dim unidad As New Moneda
            Dim query As New QueryBuilder

            item.Fields.SelectAll()
            'unidad.Simbolo.ToSelect = True
            producto.Nombre.ToSelect = True

            query.Join.EqualCondition(detalle.ordinal, item.ordinal)
            query.Join.EqualCondition(detalle.Id_producto, item.Id_producto)
            query.Join.EqualCondition(producto.Id_Producto, item.Id_producto)
            'query.Join.EqualCondition(item.Id_Moneda, unidad.Id_Moneda)
            detalle.id_reservacion.Where.EqualCondition(id_reservacion)

            query.From.Add(item)
            query.From.Add(detalle)
            query.From.Add(producto)
            'query.From.Add(unidad)
            Dim consulta As String = query.RelationalSelectQuery
            Return connection.executeSelect_DT(consulta)
        End Function

        'Reservacion
        Public Function ConsultarReservacion(ByVal id_Reservacion As Integer) As Reservacion
            Dim reserva As New Reservacion
            reserva.Fields.SelectAll()
            reserva.Id_Reservacion.Where.EqualCondition(id_Reservacion)

            Dim query As New QueryBuilder
            query.From.Add(reserva)

            Dim dataTable As Data.DataTable = connection.executeSelect_DT(query.RelationalSelectQuery)
            If dataTable.Rows.Count > 0 Then
                ObjectBuilder.CreateObject(dataTable, 0, reserva)
            End If

            Return reserva
        End Function

        ''' <summary>
        ''' Crea una reservacion a un cliente en un rango indicado de fechas
        ''' </summary>
        ''' <param name="id_cliente"></param>
        ''' <param name="theFechaInicioUtc"></param>
        ''' <param name="theFechaFinalUtc"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function CrearReservacion(ByVal id_cliente As Integer, ByVal theFechaInicioUtc As Date, ByVal theFechaFinalUtc As Date, ByVal id_responsable As Integer, ByVal crearFactura As Boolean, ByVal comentario As String, Optional ByVal id_Ubicacion As Integer = 0, Optional ByVal theDescripcion As String = Nothing) As Integer
            Dim id_reserva As Integer = 0

            If crearFactura Then
                id_reserva = CrearReservacionFactura(id_cliente, theFechaInicioUtc, theFechaFinalUtc, id_responsable, comentario, id_Ubicacion, theDescripcion)
            Else
                id_reserva = CrearReservacionBloqueada(id_responsable, theFechaInicioUtc, theFechaFinalUtc, id_responsable, comentario, id_Ubicacion, theDescripcion)
            End If
            Return id_reserva

        End Function

        Public Function CrearReservacionBloqueada(ByVal id_cliente As Integer, ByVal theFechaInicioUtc As Date, ByVal theFechaFinalUtc As Date, ByVal id_respondable As Integer, ByVal comentario As String, Optional ByVal id_Ubicacion As Integer = 0, Optional ByVal theDescripcion As String = Nothing) As Integer
            Dim id_reserva As Integer = 0
            Try
                Dim reservacion As New Reservacion
                reservacion.fechaRegistro.ValueUtc = Date.UtcNow
                reservacion.fecha_inicioProgramado.ValueUtc = theFechaInicioUtc
                reservacion.fecha_finalProgramado.ValueUtc = theFechaFinalUtc
                reservacion.id_Cliente.Value = id_cliente
                reservacion.descripcion.Value = comentario
                If id_Ubicacion = 0 Then
                    reservacion.Id_Ubicacion.Value = Config_UbicacionDefault
                Else
                    reservacion.Id_Ubicacion.Value = id_Ubicacion
                End If
                reservacion.Id_TipoEstado.Value = BuscaEstadoInicial()
                If theDescripcion IsNot Nothing Then
                    reservacion.descripcion.Value = theDescripcion
                End If

                Dim query As New QueryBuilder
                Dim consulta As String = query.InsertQuery(reservacion)
                connection.executeInsert(consulta)

                'Busca ultima llave insertada
                id_reserva = connection.lastKey(reservacion.TableName, reservacion.Id_Reservacion.Name)
                NuevoEstadoParaReservacion(id_reserva, BuscaEstadoBloqueado, id_respondable, comentario, False)
                Return id_reserva
            Catch ex As Exception
                Dim a As Integer = 0
            End Try
            Return 0
        End Function

        Public Function CrearReservacionFactura(ByVal id_cliente As Integer, ByVal theFechaInicioUtc As Date, ByVal theFechaFinalUtc As Date, ByVal id_responsable As Integer, ByVal comentario As String, Optional ByVal id_Ubicacion As Integer = 0, Optional ByVal theDescripcion As String = Nothing) As Integer
            Dim id_reserva As Integer = 0
            Try
                Dim reservacion As New Reservacion
                Dim id_factura As Integer = facturaHandler.CreateFactura(id_cliente, Config_VendedorDefault, Config_TipoFacturaReservacion, Config_UnidadVenta)
                If id_factura > 0 Then
                    Dim id_estadoInicial As Integer = BuscaEstadoInicial()
                    If id_estadoInicial > 0 Then
                        reservacion.fechaRegistro.ValueUtc = Date.UtcNow
                        reservacion.fecha_inicioProgramado.ValueUtc = theFechaInicioUtc
                        reservacion.fecha_finalProgramado.ValueUtc = theFechaFinalUtc
                        reservacion.id_Cliente.Value = id_cliente
                        reservacion.descripcion.Value = comentario
                        If id_Ubicacion = 0 Then
                            reservacion.Id_Ubicacion.Value = Config_UbicacionDefault
                        Else
                            reservacion.Id_Ubicacion.Value = id_Ubicacion
                        End If

                        reservacion.Id_factura.Value = id_factura
                        reservacion.Id_TipoEstado.Value = id_estadoInicial
                        If theDescripcion IsNot Nothing Then
                            reservacion.descripcion.Value = theDescripcion
                        End If

                        Dim query As New QueryBuilder
                        Dim consulta As String = query.InsertQuery(reservacion)

                        If connection.executeInsert(consulta) > 0 Then
                            'Busca ultima llave insertada
                            id_reserva = connection.lastKey(reservacion.TableName, reservacion.Id_Reservacion.Name)
                            If NuevoEstadoParaReservacion(id_reserva, BuscaEstadoInicial, id_responsable, comentario) Then

                            End If
                        End If

                        Return id_reserva
                    End If
                End If
            Catch ex As Exception
                Dim a As Integer = 0
            End Try
            Return 0
        End Function

        ''' <summary>
        ''' Cambia el estado a una reservacion. NO Existe historial de estados de una reservacion
        ''' </summary>
        ''' <param name="id_Reservacion"></param>
        ''' <param name="NuevoTipoEstado"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function CambiarEstadoReservacion(ByVal id_Reservacion As Integer, ByVal NuevoTipoEstado As Integer) As Resultado_Code
            Try
                Dim reservacion As New Reservacion
                reservacion.Id_Reservacion.Where.EqualCondition(id_Reservacion)
                reservacion.Id_TipoEstado.Value = NuevoTipoEstado

                Dim query As New QueryBuilder
                If connection.executeInsert(query.UpdateQuery(reservacion)) > 0 Then
                    Return Resultado_Code.OK
                End If
            Catch ex As Exception

            End Try
            Return False
        End Function

        ''' <summary>
        ''' Cambia el estado de la reservacion a cancelado
        ''' </summary>
        ''' <param name="id_reservacion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function EliminarReservacion(ByVal id_reservacion As Integer) As Boolean


            Dim reserva As New Reservacion
            Try
                reserva.Id_TipoEstado.Value = BuscarEstadoCancelado()
                reserva.Id_Reservacion.Where.EqualCondition(id_reservacion)

                Dim query As New QueryBuilder
                Dim consulta As String = query.UpdateQuery(reserva)
                connection.executeUpdate(consulta)

                'Cancela Factura
                Dim numFactura As Integer = obtenerFactura(id_reservacion)
                If facturaHandler.EliminarFactura(numFactura) Then
                    Return True
                End If
            Catch ex As Exception

            End Try
            Return False
        End Function

        Public Function BuscarEstadoReservado() As Integer
            Dim estado As New TipoEstadoReservacion
            estado.Id_TipoEstadoReservacion.ToSelect = True
            estado.RepresentaReservado.Where.EqualCondition(1)
            Dim query As New QueryBuilder
            Dim dataSet As Data.DataSet = connection.executeSelect(query.SelectQuery(estado))
            If dataSet.Tables.Count > 0 Then
                If dataSet.Tables(0).Rows.Count > 0 Then
                    ObjectBuilder.CreateObject(dataSet.Tables(0), 0, estado)
                    Return estado.Id_TipoEstadoReservacion.Value
                End If
            End If
            Return 0
        End Function

        Public Function BuscaEstadoInicial() As Integer
            Dim estado As New TipoEstadoReservacion
            estado.Id_TipoEstadoReservacion.ToSelect = True
            estado.Inicial.Where.EqualCondition(1)
            Dim query As New QueryBuilder
            Dim dataSet As Data.DataSet = connection.executeSelect(query.SelectQuery(estado))
            If dataSet.Tables.Count > 0 Then
                If dataSet.Tables(0).Rows.Count > 0 Then
                    ObjectBuilder.CreateObject(dataSet.Tables(0), 0, estado)
                    Return estado.Id_TipoEstadoReservacion.Value
                End If
            End If
            Return 0
        End Function

        Public Function BuscaEstadoBloqueado() As Integer
            Dim estado As New TipoEstadoReservacion
            estado.Id_TipoEstadoReservacion.ToSelect = True
            estado.Bloqueado.Where.EqualCondition(1)
            Dim query As New QueryBuilder
            Dim dataSet As Data.DataSet = connection.executeSelect(query.SelectQuery(estado))
            If dataSet.Tables.Count > 0 Then
                If dataSet.Tables(0).Rows.Count > 0 Then
                    ObjectBuilder.CreateObject(dataSet.Tables(0), 0, estado)
                    Return estado.Id_TipoEstadoReservacion.Value
                End If
            End If
            Return 0
        End Function

        'Estados Reservacion
        Public Sub selectTipoEstadoReservacion_BindListCollection(ByVal theList As ListItemCollection)
            Dim historialEstados As New Orbelink.Control.HistorialEstados.HistorialHandler(connection)
            Dim controladora As New Controladora_HistorialEstados_Reservacion
            historialEstados.selectTipoEstado_BindListCollection(controladora, theList)
        End Sub

        Public Function selectEstados_ByReservacion(ByVal id_Reservacion As Integer, Optional ByVal top As Integer = 0) As Data.DataTable
            Dim historialEstados As New Orbelink.Control.HistorialEstados.HistorialHandler(connection)
            Dim controladora As New Controladora_HistorialEstados_Reservacion
            Return historialEstados.selectEstados(controladora, id_Reservacion, top)
        End Function

        Public Function BuscarEstadoCancelado() As Integer
            Dim tipoEstado As New TipoEstadoReservacion

            tipoEstado.Id_TipoEstadoReservacion.ToSelect = True
            tipoEstado.RepresentaCancelado.Where.EqualCondition(1)

            Dim query As New QueryBuilder
            query.From.Add(tipoEstado)

            Dim dataTable As Data.DataTable = connection.executeSelect_DT(query.RelationalSelectQuery)
            If dataTable.Rows.Count > 0 Then
                ObjectBuilder.CreateObject(dataTable, 0, tipoEstado)
                Return tipoEstado.Id_TipoEstadoReservacion.Value
            End If

            Return 0
        End Function

        Public Function NuevoEstadoParaReservacion(ByVal Id_Reservacion As Integer, ByVal Id_TipoEstadoReservacion As Integer, ByVal Responsable As Integer, ByVal comentario As String, Optional ByVal modificarFactura As Boolean = True) As ControladorReservaciones.Resultado_Code
            Dim historialEstados As New HistorialHandler(connection)
            Dim controladora As New Controladora_HistorialEstados_Reservacion
            Dim result As Resultado_Code = Resultado_Code.Repite_Estado

            Dim reserva As Reservacion = ConsultarReservacion(Id_Reservacion)
            If reserva.Id_TipoEstado.Value = BuscaEstadoBloqueado() Then
                modificarFactura = False
            End If

            If Not historialEstados.existeEstado(controladora, Id_Reservacion, Id_TipoEstadoReservacion) Then
                If Id_TipoEstadoReservacion = BuscarEstadoCancelado() Or IsReservacionDisponible(Id_Reservacion) = Resultado_Code.OK Or Id_TipoEstadoReservacion = Config_EstadoIncompleto Then
                    If modificarFactura Then
                        Dim id_Factura As Integer = obtenerFactura(Id_Reservacion)
                        If id_Factura > 0 Then
                            Dim resultFactura As Boolean = False
                            Dim nuevoEstadoParaFactura As Integer = GetEstadoFactura_SegunEstadoReservacion(Id_TipoEstadoReservacion)
                            Select Case nuevoEstadoParaFactura
                                Case FacturasHandler.EstadoFactura.Transaction_Successful
                                    If facturaHandler.ConsumirFactura(id_Factura, Responsable) Then
                                        resultFactura = True
                                    End If
                                Case FacturasHandler.EstadoFactura.Transaction_Unsuccessful
                                    If facturaHandler.NoConsumirFactura(id_Factura, Responsable) Then
                                        resultFactura = True
                                    End If
                                Case FacturasHandler.EstadoFactura.Approved_Cart
                                    If facturaHandler.AprovarFactura(id_Factura, Responsable) Then
                                        resultFactura = True
                                    End If
                            End Select

                        End If
                        If historialEstados.NuevoEstado(controladora, Id_Reservacion, Id_TipoEstadoReservacion, Responsable, comentario) Then
                            result = CambiarEstadoReservacion(Id_Reservacion, Id_TipoEstadoReservacion)
                        End If
                    Else
                        If historialEstados.NuevoEstado(controladora, Id_Reservacion, Id_TipoEstadoReservacion, Responsable, comentario) Then
                            result = CambiarEstadoReservacion(Id_Reservacion, Id_TipoEstadoReservacion)
                        End If
                    End If
                Else
                    result = Resultado_Code.Item_noDisponible
                End If
            End If
            'permite pasar de un estado a otro
            If result = Resultado_Code.Repite_Estado And Id_TipoEstadoReservacion <> 1 Then
                result = CambiarEstadoReservacion(Id_Reservacion, Id_TipoEstadoReservacion)
            End If

            Return result
        End Function

        Private Function GetEstadoFactura_SegunEstadoReservacion(ByVal Id_TipoEstadoReservacion As Integer) As Integer
            Dim nuevoEstadoParaFactura As Integer = Facturas.FacturasHandler.EstadoFactura.Unapproved_Cart

            If Id_TipoEstadoReservacion = BuscarEstadoReservado() Then
                nuevoEstadoParaFactura = Facturas.FacturasHandler.EstadoFactura.Transaction_Successful
            End If
            If Id_TipoEstadoReservacion = BuscarEstadoCancelado() Then
                nuevoEstadoParaFactura = Facturas.FacturasHandler.EstadoFactura.Transaction_Unsuccessful
            End If
            If Id_TipoEstadoReservacion = BuscaEstadoInicial() Then
                nuevoEstadoParaFactura = Facturas.FacturasHandler.EstadoFactura.Approved_Cart
            End If
            If Id_TipoEstadoReservacion = Config_EstadoIncompleto Then
                nuevoEstadoParaFactura = Facturas.FacturasHandler.EstadoFactura.Transaction_Unsuccessful
            End If
            Return nuevoEstadoParaFactura
        End Function
        'Detalle
        ''' <summary>
        ''' Agrega un item a un reservacion existente, debe recibir cual producto se desea reservar y ademas cual es el ordinal del item a reservar
        ''' </summary>
        ''' <param name="id_reservacion"></param>
        ''' <param name="id_producto"></param>
        ''' <param name="ordinal"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function AgregarItem(ByVal id_reservacion As Integer, ByVal EntidadResponsable As Integer, ByVal id_producto As Integer, ByVal ordinal As Integer, ByVal adultos As Integer, ByVal ninos As Integer, ByVal usarlogicaPorNoches As Boolean, Optional ByVal items_a_reservar As Integer = 1, Optional ByVal noches As Integer = 0, Optional ByVal nochesadicionales As Integer = 0, Optional ByVal nombreDisplay As String = "", Optional ByVal descuento As String = "", Optional ByVal id_TemporadaNueva As Integer = 0, Optional ByVal precio_transporte As Integer = 0) As Resultado_Code

            precioTransporte = precio_transporte

            Dim controladora As New Orbelink.Control.Productos.ProductosHandler(connection.connectionString)
            Dim producto As Producto = controladora.ConsultarProducto(id_producto)

            Dim capacidad As Integer = producto.Capacidad.Value
            Dim capacidadMax As Integer = producto.CapacidadMaxima.Value

            If adultos + ninos <= capacidadMax Or usarlogicaPorNoches = True Then
                Dim resultado As Resultado_Code = Me.IsItemDisponible_ByReservacion(id_reservacion, id_producto, ordinal, items_a_reservar)

                If resultado = Resultado_Code.OK Then

                    'Agrega el detalle
                    Dim detalle As New Detalle_Reservacion
                    detalle.Id_producto.Value = id_producto
                    detalle.ordinal.Value = ordinal
                    detalle.id_reservacion.Value = id_reservacion
                    detalle.Adultos.Value = adultos
                    detalle.Ninos.Value = ninos

                    Dim query As New QueryBuilder
                    connection.executeInsert(query.InsertQuery(detalle))

                    'Calcula montos
                    Dim laReservacion As Reservacion = ConsultarReservacion(id_reservacion)
                    'Dim numFactura As Integer = obtenerFactura(id_reservacion)

                    If laReservacion.Id_TipoEstado.Value <> BuscaEstadoBloqueado() Then
                        Dim result As Boolean = True
                        If usarlogicaPorNoches Then
                            If Not RevisarYAgregarDetallesFactura(laReservacion, id_producto, ordinal, adultos, ninos, EntidadResponsable, noches, nochesadicionales, items_a_reservar, nombreDisplay, descuento, id_TemporadaNueva, precio_transporte) Then
                                result = False
                            End If
                        Else
                            If RevisarYAgregarDetallesFactura(laReservacion, id_producto, ordinal, capacidad, adultos, ninos, EntidadResponsable) Then
                                result = False
                            End If
                        End If
                        If result = False Then
                            deleteDetalleReservacion(id_reservacion, id_producto, ordinal)
                            resultado = Resultado_Code.Item_noDisponible
                        End If
                    End If
                End If
                Return resultado
            Else
                'Excede el maximo
                Return Resultado_Code.ExcedeMaximo
            End If
        End Function

        'Detalle
        ''' <summary>
        ''' Agrega un item a un reservacion fictisia, para poder brindar precios sin necesidad de crear una entidad o reservación en base de datos, 
        ''' debe recibir cual producto se desea reservar y ademas cual es el ordinal del item a reservar
        ''' </summary>
        ''' <param name="id_reservacion"></param>
        ''' <param name="id_producto"></param>
        ''' <param name="ordinal"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function AgregarItemTemporal(ByVal id_producto As Integer, ByVal ordinal As Integer, ByVal adultos As Integer, ByVal ninos As Integer, ByVal usarlogicaPorNoches As Boolean, ByVal fechaInicio As Date, ByVal fechaFin As Date, Optional ByVal items_a_reservar As Integer = 1, Optional ByVal noches As Integer = 0, Optional ByVal nochesadicionales As Integer = 0, Optional ByVal descuento As String = "", Optional ByVal id_TemporadaNueva As Integer = 0, Optional ByVal precio_transporte As Integer = 0, Optional ByVal ubicacion As Integer = 1) As Double
            precioTransporte = precio_transporte


            Dim controladora As New Orbelink.Control.Productos.ProductosHandler(connection.connectionString)
            Dim producto As Producto = controladora.ConsultarProducto(id_producto)

            Dim capacidad As Integer = producto.Capacidad.Value
            Dim capacidadMax As Integer = producto.CapacidadMaxima.Value

            If adultos + ninos <= capacidadMax Or usarlogicaPorNoches = True Then
                Dim resultado As Resultado_Code = Me.IsItemDisponible_WithoutReservation(id_producto, ordinal, ubicacion, fechaInicio, fechaFin, items_a_reservar)
                Dim result As Double = 0
                If resultado = Resultado_Code.OK Then





                    If usarlogicaPorNoches Then
                        If CalculoDetalleFacturaTemporal(id_producto, ordinal, adultos, ninos, fechaInicio, fechaFin, noches, nochesadicionales, items_a_reservar, descuento, id_TemporadaNueva, precio_transporte) <> 0 Then

                            result = CalculoDetalleFacturaTemporal(id_producto, ordinal, adultos, ninos, fechaInicio, fechaFin, noches, nochesadicionales, items_a_reservar, descuento, id_TemporadaNueva, precio_transporte)

                        End If
                    Else
                        If CalculoDetalleFacturaTemporal(id_producto, ordinal, capacidad, adultos, ninos, fechaInicio, fechaFin) <> 0 Then
                            result = CalculoDetalleFacturaTemporal(id_producto, ordinal, capacidad, adultos, ninos, fechaInicio, fechaFin)
                        End If
                    End If


                End If
                Return result
            Else
                'Excede el maximo
                Return 0
            End If
        End Function

        Private Function deleteDetalleReservacion(ByVal id_reservacion As Integer, ByVal id_producto As Integer, ByVal ordinal As Integer) As Boolean
            Try
                Dim detalle As New Detalle_Reservacion
                detalle.Id_producto.Where.EqualCondition(id_producto)
                detalle.ordinal.Where.EqualCondition(ordinal)
                detalle.id_reservacion.Where.EqualCondition(id_reservacion)
                Dim query As New QueryBuilder()
                connection.executeDelete(query.DeleteQuery(detalle))
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function
        ''' <summary>
        '''Logica Calculo por tarifa diariay personas extra  En caso de traslape cobra todos los dias segun la temporada 
        ''' </summary>
        ''' <param name="reservacion"></param>
        ''' <param name="id_producto"></param>
        ''' <param name="ordinal"></param>
        ''' <param name="capacidad"></param>
        ''' <param name="adultos"></param>
        ''' <param name="ninos"></param>
        ''' <param name="EntidadResponsable"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function RevisarYAgregarDetallesFactura(ByVal reservacion As Reservacion, ByVal id_producto As Integer, ByVal ordinal As Integer, ByVal capacidad As Integer, ByVal adultos As Integer, ByVal ninos As Integer, ByVal EntidadResponsable As Integer) As Boolean
            'Dim resMontos As New ResultadoMontosUnitarios
            Dim resultado As Boolean = False

            Dim rangos As New RangoTemporada
            Dim temporada As New Temporada
            Dim datos As Data.DataTable = buscarTemporadas(rangos, temporada, reservacion.fecha_inicioProgramado.ValueUtc, reservacion.fecha_finalProgramado.ValueUtc)

            For counter As Integer = 0 To datos.Rows.Count - 1
                Dim rangoActual As RangoTemporada = ObjectBuilder.CreateDBTable_BySelectIndex(datos.Rows(counter), rangos)
                Dim temporadaActual As Temporada = ObjectBuilder.CreateDBTable_BySelectIndex(datos.Rows(counter), temporada)
                Dim precio As Precios_Temporada = buscarPrecioTemporada(temporadaActual.Id_Temporada.Value, id_producto)

                Dim resMontos As New ResultadoMontosUnitarios
                resMontos = calcularMontosUnitario(precio, capacidad, adultos, ninos)

                Dim fechaBase As Date
                Dim fechaTope As Date
                If reservacion.fecha_inicioProgramado.ValueUtc < rangoActual.fecha_inicio.ValueUtc Then
                    fechaBase = rangoActual.fecha_inicio.ValueUtc
                Else
                    fechaBase = reservacion.fecha_inicioProgramado.ValueUtc
                End If

                If reservacion.fecha_finalProgramado.ValueUtc < rangoActual.fecha_final.ValueUtc Then
                    fechaTope = reservacion.fecha_finalProgramado.ValueUtc
                Else
                    fechaTope = rangoActual.fecha_final.ValueUtc
                End If
                resMontos.dias = calcularCantidadFacturar(fechaBase, fechaTope, precio.UnidadTiempo.Value)

                If facturaHandler.AgregaDetalle_FromItem(reservacion.Id_factura.Value, id_producto, ordinal, resMontos.MontoUnitario, resMontos.MontoUnitarioExtra, resMontos.dias, 0) <> FacturasHandler.Resultado_Code.OK Then
                    resultado = False
                End If
            Next
            Return resultado
        End Function


        ''' <summary>
        ''' Logica Calculo por noches y noches extra. En caso de traslape cobra segun la temporada de la fecha de inicio
        ''' </summary>
        ''' <param name="reservacion"></param>
        ''' <param name="id_producto"></param>
        ''' <param name="ordinal"></param>
        ''' <param name="capacidad"></param>
        ''' <param name="adultos"></param>
        ''' <param name="ninos"></param>
        ''' <param name="EntidadResponsable"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function RevisarYAgregarDetallesFactura(ByVal reservacion As Reservacion, ByVal id_producto As Integer, ByVal ordinal As Integer, ByVal adultos As Integer, ByVal ninos As Integer, ByVal EntidadResponsable As Integer, ByVal noches As Integer, ByVal nochesadicionales As Integer, Optional ByVal items_a_reservar As Integer = 1, Optional ByVal nombreDisplay As String = "", Optional ByVal descuento As String = "", Optional ByVal id_TemporadaNueva As Integer = 0, Optional ByVal precio_transporte As Integer = 0) As Boolean
            'Dim resMontos As New ResultadoMontosUnitarios
            Dim resultado As Boolean = True

            Dim rangos As New RangoTemporada
            Dim temporada As New Temporada
            'PRIMERA TEMPORADA SEGUN FECHA DE INICIO
            Dim datos As Data.DataTable = buscarTemporadas(rangos, temporada, reservacion.fecha_inicioProgramado.ValueUtc, reservacion.fecha_inicioProgramado.ValueUtc)


            'buscar en datos el id que se necesita y con un if se le manda el id temporada directo a.. 
            'buscarPrecioTemporada( temporadaActual.Id_Temporada.Value


            'For counter As Integer = 0 To datos.Rows.Count - 1
            Dim counter As Integer = 0
            Dim rangoActual As RangoTemporada = ObjectBuilder.CreateDBTable_BySelectIndex(datos.Rows(counter), rangos)
            Dim temporadaActual As Temporada = ObjectBuilder.CreateDBTable_BySelectIndex(datos.Rows(counter), temporada)
            Dim precio As Precios_Temporada

            If id_TemporadaNueva <> 0 Then
                precio = buscarPrecioTemporada(id_TemporadaNueva, id_producto, adultos, noches)
            Else
                precio = buscarPrecioTemporada(temporadaActual.Id_Temporada.Value, id_producto, adultos, noches)
            End If

            Dim resMontos As New ResultadoMontosUnitarios
            resMontos = calcularMontosUnitarioXnoches(precio, adultos, ninos, nochesadicionales, id_TemporadaNueva)

            ' Si seleccionaron adquirir transporte 

            If precio_transporte <> 0 Then
                resMontos.MontoUnitario += precio_transporte * adultos
                resMontos.MontoUnitario += precio_transporte * ninos
            End If


            Dim fechaBase As Date
            Dim fechaTope As Date
            If reservacion.fecha_inicioProgramado.ValueUtc < rangoActual.fecha_inicio.ValueUtc Then
                fechaBase = rangoActual.fecha_inicio.ValueUtc
            Else
                fechaBase = reservacion.fecha_inicioProgramado.ValueUtc
            End If

            If reservacion.fecha_finalProgramado.ValueUtc < rangoActual.fecha_final.ValueUtc Then
                fechaTope = reservacion.fecha_finalProgramado.ValueUtc
            Else
                fechaTope = rangoActual.fecha_final.ValueUtc
            End If
            'resMontos.dias = calcularCantidadFacturar(fechaBase, fechaTope, precio.UnidadTiempo.Value)
            resMontos.dias = noches + nochesadicionales

            Dim descuentoPromo As Double
            If descuento = "" Then
                descuentoPromo = 0
            Else
                descuentoPromo = descuento
            End If

            If facturaHandler.AgregaDetalle_FromItem(reservacion.Id_factura.Value, id_producto, ordinal, resMontos.MontoUnitario, resMontos.MontoUnitarioExtra, items_a_reservar, descuentoPromo, nombreDisplay) <> FacturasHandler.Resultado_Code.OK Then
                resultado = False
            End If
            'Next
            Return resultado
        End Function


        ''' <summary>
        '''Logica Calculo por tarifa diariay personas extra  En caso de traslape cobra todos los dias segun la temporada 
        ''' Esta se usa en el paso 1 de la Reservación, cuando aun no se ha registrado la reservacion ni la entidad en Base de Datos
        ''' </summary>
        ''' <param name="reservacion"></param>
        ''' <param name="id_producto"></param>
        ''' <param name="ordinal"></param>
        ''' <param name="capacidad"></param>
        ''' <param name="adultos"></param>
        ''' <param name="ninos"></param>
        ''' <param name="EntidadResponsable"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function CalculoDetalleFacturaTemporal(ByVal id_producto As Integer, ByVal ordinal As Integer, ByVal capacidad As Integer, ByVal adultos As Integer, ByVal ninos As Integer, ByVal fechaInicio As Date, ByVal fechaFin As Date) As Double
            'Dim resMontos As New ResultadoMontosUnitarios
            Dim resultado As Double = 0

            Dim rangos As New RangoTemporada
            Dim temporada As New Temporada
            Dim datos As Data.DataTable = buscarTemporadas(rangos, temporada, fechaInicio, fechaFin)


            For counter As Integer = 0 To datos.Rows.Count - 1
                Dim rangoActual As RangoTemporada = ObjectBuilder.CreateDBTable_BySelectIndex(datos.Rows(counter), rangos)
                Dim temporadaActual As Temporada = ObjectBuilder.CreateDBTable_BySelectIndex(datos.Rows(counter), temporada)
                Dim precio As Precios_Temporada = buscarPrecioTemporada(temporadaActual.Id_Temporada.Value, id_producto)

                Dim resMontos As New ResultadoMontosUnitarios
                resMontos = calcularMontosUnitario(precio, capacidad, adultos, ninos)

                Dim fechaBase As Date
                Dim fechaTope As Date
                If fechaInicio < rangoActual.fecha_inicio.ValueUtc Then
                    fechaBase = rangoActual.fecha_inicio.ValueUtc
                Else
                    fechaBase = fechaInicio
                End If

                If fechaFin < rangoActual.fecha_final.ValueUtc Then
                    fechaTope = fechaFin
                Else
                    fechaTope = rangoActual.fecha_final.ValueUtc
                End If
                resMontos.dias = calcularCantidadFacturar(fechaBase, fechaTope, precio.UnidadTiempo.Value)



                resultado = resMontos.MontoUnitario + resMontos.MontoUnitarioExtra



            Next
            Return resultado
        End Function


        ''' <summary>
        ''' Logica Calculo por noches y noches extra. En caso de traslape cobra segun la temporada de la fecha de inicio
        ''' Esta se usa en el paso 1 de la Reservación, cuando aun no se ha registrado la reservacion ni la entidad en Base de Datos
        ''' </summary>
        ''' <param name="reservacion"></param>
        ''' <param name="id_producto"></param>
        ''' <param name="ordinal"></param>
        ''' <param name="capacidad"></param>
        ''' <param name="adultos"></param>
        ''' <param name="ninos"></param>
        ''' <param name="EntidadResponsable"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected Function CalculoDetalleFacturaTemporal(ByVal id_producto As Integer, ByVal ordinal As Integer, ByVal adultos As Integer, ByVal ninos As Integer, ByVal fechaInicio As Date, ByVal fechaFin As Date, ByVal noches As Integer, ByVal nochesadicionales As Integer, Optional ByVal items_a_reservar As Integer = 1, Optional ByVal descuento As String = "", Optional ByVal id_TemporadaNueva As Integer = 0, Optional ByVal precio_transporte As Integer = 0) As Double
            'Dim resMontos As New ResultadoMontosUnitarios
            Dim resultado As Double = 0

            Dim rangos As New RangoTemporada
            Dim temporada As New Temporada
            'PRIMERA TEMPORADA SEGUN FECHA DE INICIO
            Dim datos As Data.DataTable = buscarTemporadas(rangos, temporada, fechaInicio, fechaInicio)


            'buscar en datos el id que se necesita y con un if se le manda el id temporada directo a.. 
            'buscarPrecioTemporada( temporadaActual.Id_Temporada.Value


            'For counter As Integer = 0 To datos.Rows.Count - 1
            Dim counter As Integer = 0
            Dim rangoActual As RangoTemporada = ObjectBuilder.CreateDBTable_BySelectIndex(datos.Rows(counter), rangos)
            Dim temporadaActual As Temporada = ObjectBuilder.CreateDBTable_BySelectIndex(datos.Rows(counter), temporada)
            Dim precio As Precios_Temporada

            If id_TemporadaNueva <> 0 Then
                precio = buscarPrecioTemporada(id_TemporadaNueva, id_producto, adultos, noches)
            Else
                precio = buscarPrecioTemporada(temporadaActual.Id_Temporada.Value, id_producto, adultos, noches)
            End If

            Dim resMontos As New ResultadoMontosUnitarios
            resMontos = calcularMontosUnitarioXnoches(precio, adultos, ninos, nochesadicionales, id_TemporadaNueva)

            ' Si seleccionaron adquirir transporte 

            If precio_transporte <> 0 Then
                resMontos.MontoUnitario += precio_transporte * adultos
                resMontos.MontoUnitario += precio_transporte * ninos
            End If


            Dim fechaBase As Date
            Dim fechaTope As Date
            If fechaInicio < rangoActual.fecha_inicio.ValueUtc Then
                fechaBase = rangoActual.fecha_inicio.ValueUtc
            Else
                fechaBase = fechaInicio
            End If

            If fechaFin < rangoActual.fecha_final.ValueUtc Then
                fechaTope = fechaFin
            Else
                fechaTope = rangoActual.fecha_final.ValueUtc
            End If
            'resMontos.dias = calcularCantidadFacturar(fechaBase, fechaTope, precio.UnidadTiempo.Value)
            resMontos.dias = noches + nochesadicionales

            Dim descuentoPromo As Double
            If descuento = "" Then
                descuentoPromo = 0
            Else
                descuentoPromo = descuento
            End If



            resultado = resMontos.MontoUnitario + resMontos.MontoUnitarioExtra - descuentoPromo


            'Next
            Return resultado
        End Function




        ''' <summary>
        ''' Retorna la cantidad de items en una reservacion
        ''' </summary>
        ''' <param name="id_reservacion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function CantidadItemsEnReservacion(ByVal id_reservacion As Integer) As Integer
            Dim cantidad As Integer = 0
            Dim detalle As New Detalle_Reservacion
            detalle.Id_producto.ToSelect = True
            detalle.id_reservacion.Where.EqualCondition(id_reservacion)

            Dim query As New QueryBuilder
            Dim consulta As String = query.SelectQuery(detalle)
            Dim dataTable As Data.DataTable = connection.executeSelect_DT(consulta)

            If dataTable.Rows.Count > 0 Then
                cantidad = dataTable.Rows.Count
            End If

            Return cantidad
        End Function

        'Busqueda por producto
        Public Function cargarItemsDisponibles_ByProducto(ByVal id_producto As Integer, ByVal fechaInicioUtc As Date, ByVal fechaFinUtc As Date, ByVal id_ubicacion As Integer, Optional ByVal id_reservacion As Integer = 0) As Data.DataTable
            Return cargarItemsDisponibles(0, id_producto, fechaInicioUtc, fechaFinUtc, id_ubicacion, id_reservacion)
        End Function

        Public Function cargarItemsEnFecha_ByProducto(ByVal id_producto As Integer, ByVal fechaInicioUtc As Date, ByVal fechaFinUtc As Date, ByVal id_ubicacion As Integer) As Data.DataTable
            Return cargarItemsEnFecha(0, id_producto, fechaInicioUtc, fechaFinUtc, id_ubicacion)
        End Function

        Public Function cargarItemsEnUbicacion_ByProducto(ByVal id_producto As Integer, ByVal fechaInicioUtc As Date, ByVal fechaFinUtc As Date, ByVal id_ubicacion As Integer) As Data.DataTable
            Return cargarItemsEnUbicacion(0, id_producto, fechaInicioUtc, fechaFinUtc, id_ubicacion)
        End Function

        'Private Function crearConsultaUltimaUbicacion(ByVal id_producto As Integer, ByVal id_ubicacion As Integer, ByVal fechaInicio As Date, Optional ByVal consultaPositiva As Boolean = True) As String
        '    Dim item As New Item
        '    Dim reservacion As New Reservacion
        '    Dim detalle As New detalle_reservacion
        '    Dim query As New QueryBuilder()

        '    item.ordinal.ToSelect = True

        '    If consultaPositiva Then
        '        Dim conditionfecha As Condition = reservacion.fecha_finalProgramado.Where.LessThanCondition(fechaInicio)

        '        reservacion.estado.Where.EqualCondition(ControladorReservaciones.Estado.consumido)
        '        reservacion.estado.Where.EqualCondition(ControladorReservaciones.Estado.reservado, Where.FieldRelations.OR_)
        '        reservacion.Id_Ubicacion.Where.EqualCondition(id_ubicacion)
        '    Else
        '        'Dim conditionubicaciondiferente As Condition = reservacion.Id_Ubicacion.Where.DiferentCondition(id_ubicacion)
        '        'Dim condicionA As Condition = query2.GroupConditions(conditionfecha, conditionubicaciondiferente)

        '        Dim condicionUbicacionNull As Condition = reservacion.Id_Ubicacion.Where.IsNullCondition(Where.FieldRelations.OR_)
        '        'query2.GroupConditions(condicionA, condicionUbicacionNull)
        '    End If

        '    query2.Join.EqualCondition(item.ordinal, detalle.ordinal, Join.JoinTypes.LEFT_OUTER_JOIN)
        '    query2.Join.EqualCondition(item.Id_producto, detalle.Id_producto, Join.JoinTypes.LEFT_OUTER_JOIN)

        '    query2.Join.EqualCondition(detalle.id_reservacion, reservacion.Id_Reservacion)

        '    item.Id_producto.Where.EqualCondition(id_producto)
        '    query2.From.Add(item)
        '    query2.From.Add(reservacion)
        '    query2.From.Add(detalle)
        '    Return query2.RelationalSelectQuery
        'End Function

        'Private Function crearConsultaUbicacion(ByVal id_producto As Integer, ByVal id_ubicacion As Integer) As String
        '    Dim item As New Item
        '    Dim reservacion As New Reservacion
        '    Dim detalle As New Detalle_Reservacion
        '    Dim query As New QueryBuilder()
        '    Dim tipoEstado As New TipoEstadoReservacion

        '    item.ordinal.ToSelect = True

        '    tipoEstado.Terminador.Where.EqualCondition(1)

        '    reservacion.Id_Ubicacion.Where.EqualCondition(id_ubicacion)

        '    query.Join.EqualCondition(item.ordinal, detalle.ordinal, Join.JoinTypes.LEFT_OUTER_JOIN)
        '    query.Join.EqualCondition(item.Id_producto, detalle.Id_producto, Join.JoinTypes.LEFT_OUTER_JOIN)

        '    query.Join.EqualCondition(detalle.id_reservacion, reservacion.Id_Reservacion)
        '    query.Join.EqualCondition(reservacion.Id_TipoEstado, tipoEstado.Id_TipoEstadoReservacion)

        '    item.Id_producto.Where.EqualCondition(id_producto)
        '    query.From.Add(item)
        '    query.From.Add(reservacion)
        '    query.From.Add(detalle)
        '    query.From.Add(tipoEstado)
        '    Return query.RelationalSelectQuery
        'End Function

        'Busquedas por tipoProducto y ubicaciones

        Public Function cargarItemsDisponibles_ByTipoProducto(ByVal id_TipoProducto As Integer, ByVal fechaInicioUtc As Date, ByVal fechaFinUtc As Date, ByVal id_ubicacion As Integer) As Data.DataTable
            Return cargarItemsDisponibles(id_TipoProducto, 0, fechaInicioUtc, fechaFinUtc, id_ubicacion)
        End Function

        Public Function cargarItemsEnFecha_ByTipoProducto(ByVal id_TipoProducto As Integer, ByVal fechaInicioUtc As Date, ByVal fechaFinUtc As Date, ByVal id_ubicacion As Integer) As Data.DataTable
            Return cargarItemsEnFecha(id_TipoProducto, 0, fechaInicioUtc, fechaFinUtc, id_ubicacion)
        End Function

        Public Function cargarItemsEnUbicacion_ByTipoProducto(ByVal id_TipoProducto As Integer, ByVal fechaInicioUtc As Date, ByVal fechaFinUtc As Date, ByVal id_ubicacion As Integer) As Data.DataTable
            Return cargarItemsEnUbicacion(id_TipoProducto, 0, fechaInicioUtc, fechaFinUtc, id_ubicacion)
        End Function

        Private Function cargarItemsDisponibles(ByVal id_TipoProducto As Integer, ByVal id_Producto As Integer, ByVal fechaInicioUtc As Date, ByVal fechaFinUtc As Date, ByVal id_ubicacion As Integer, Optional ByVal id_reservacion As Integer = 0) As Data.DataTable
            Dim item As New Item
            Dim query As New QueryBuilder

            'Filtro Fechas


            Dim consultaInternaFechas As String = crearConsultaInternaFechas(item, fechaInicioUtc.AddHours(1), fechaFinUtc.AddHours(1))
            item.ordinal.Where.NotInCondition(consultaInternaFechas, Where.FieldRelations.AND_)

            ''Filtro ubicacion
            'Dim consultaUltimaUbicacion As String = crearConsultaUltimaUbicacionNegative(item, id_ubicacion, fechaInicioUtc)
            'Dim consultaInternaNoUbicacion As String = crearConsultaNoUbicacionDelTodo(item)

            'Dim condicionUbicacionSi As Condition = item.ordinal.Where.NotInCondition(consultaUltimaUbicacion, Where.FieldRelations.AND_)
            'Dim condicionUbicacionNo As Condition = item.ordinal.Where.InCondition(consultaInternaNoUbicacion, Where.FieldRelations.OR_)
            'query.GroupConditions(condicionUbicacionSi, condicionUbicacionNo)

            'Filtro Temporada
            'Dim precios As New Precios_Temporada
            'Dim temporada As New Temporada
            'Dim rangos As New RangoTemporada

            'Filtro ordinales reservacion existente solicitud
            If id_reservacion > 0 Then
                Dim queryReservacion As New QueryBuilder
                Dim elDetalle As New Detalle_Reservacion
                elDetalle.ordinal.ToSelect = True
                elDetalle.id_reservacion.Where.EqualCondition(id_reservacion)
                item.ordinal.Where.NotInCondition(queryReservacion.SelectQuery(elDetalle), Where.FieldRelations.AND_)
            End If

            'Dim resultadoTemp As ResultadoBusquedaTemporada = buscarTemporada(fechaInicioUtc, fechaFinUtc)
            'rangos.Ordinal.Where.EqualCondition(resultadoTemp.Ordinal)
            'rangos.Id_Temporada.Where.EqualCondition(resultadoTemp.id_Temporada)
            'precios.Fields.SelectAll()
            'temporada.Nombre.ToSelect = True
            'rangos.Fields.SelectAll()
            'query.Join.EqualCondition(temporada.Id_Temporada, precios.Id_Temporada)
            'query.Join.EqualCondition(temporada.Id_Temporada, rangos.Id_Temporada)

            'Producto
            Dim producto As New Producto
            producto.Nombre.ToSelect = True
            producto.Id_Producto.ToSelect = True
            If id_TipoProducto > 0 And id_Producto = 0 Then
                producto.Id_tipoProducto.Where.EqualCondition(id_TipoProducto)
            ElseIf id_TipoProducto = 0 And id_Producto > 0 Then
                producto.Id_Producto.Where.EqualCondition(id_Producto)
            Else
                Throw New Exception("Error al filtrar, filtros no exclusivos")
            End If
            'query.Join.EqualCondition(precios.Id_Producto, producto.Id_Producto)
            query.Join.EqualCondition(item.Id_producto, producto.Id_Producto)

            'Moneda
            'Dim unidad As New Moneda
            'unidad.Simbolo.ToSelect = True
            'query.Join.EqualCondition(unidad.Id_Moneda, precios.Id_Moneda)

            'Campos a seleccionar
            item.descripcion.ToSelect = True
            item.ordinal.ToSelect = True

            'Consulta
            query.From.Add(item)
            query.From.Add(producto)
            'query.From.Add(unidad)
            'query.From.Add(precios)
            'query.From.Add(temporada)
            'query.From.Add(rangos)

            Dim consulta As String = query.RelationalSelectQuery
            Dim resultado As Data.DataTable = connection.executeSelect_DT(consulta)

            'resultado.Columns(temporada.Nombre.SelectIndex).ColumnName = resourceMan.GetString("Temporada")



            Return resultado
        End Function

        Private Function cargarItemsEnFecha(ByVal id_TipoProducto As Integer, ByVal id_Producto As Integer, ByVal fechaInicioUtc As Date, ByVal fechaFinUtc As Date, ByVal id_ubicacion As Integer) As Data.DataTable
            Dim item As New Item
            Dim query As New QueryBuilder

            Dim consultaUltimaUbicacion As String = crearConsultaUltimaUbicacionNegative(item, id_ubicacion, fechaInicioUtc)
            Dim consultaInternaFechas As String = crearConsultaInternaFechas(item, fechaInicioUtc, fechaFinUtc)
            item.ordinal.Where.NotInCondition(consultaInternaFechas, Where.FieldRelations.AND_)
            item.ordinal.Where.InCondition(consultaUltimaUbicacion, Where.FieldRelations.AND_)

            'Filtro Temporada
            Dim precios As New Precios_Temporada
            Dim temporada As New Temporada
            Dim rangos As New RangoTemporada

            Dim resultadoTemp As ResultadoBusquedaTemporada = buscarTemporada(fechaInicioUtc, fechaFinUtc)
            rangos.Ordinal.Where.EqualCondition(resultadoTemp.Ordinal)
            rangos.Id_Temporada.Where.EqualCondition(resultadoTemp.id_Temporada)
            precios.Fields.SelectAll()
            temporada.Nombre.ToSelect = True
            rangos.Fields.SelectAll()
            query.Join.EqualCondition(temporada.Id_Temporada, precios.Id_Temporada)
            query.Join.EqualCondition(temporada.Id_Temporada, rangos.Id_Temporada)

            'Productos
            Dim producto As New Producto
            producto.Nombre.ToSelect = True
            producto.Id_Producto.ToSelect = True
            If id_TipoProducto > 0 And id_Producto = 0 Then
                producto.Id_tipoProducto.Where.EqualCondition(id_TipoProducto)
            ElseIf id_TipoProducto = 0 And id_Producto > 0 Then
                producto.Id_Producto.Where.EqualCondition(id_Producto)
            Else
                Throw New Exception("Error al filtrar, filtros no exclusivos")
            End If
            query.Join.EqualCondition(precios.Id_Producto, producto.Id_Producto)
            query.Join.EqualCondition(producto.Id_Producto, item.Id_producto)

            'Moneda
            Dim unidad As New Moneda
            unidad.Simbolo.ToSelect = True
            query.Join.EqualCondition(unidad.Id_Moneda, precios.Id_Moneda)

            item.descripcion.ToSelect = True
            item.ordinal.ToSelect = True

            'Consulta
            query.From.Add(item)
            query.From.Add(producto)
            query.From.Add(unidad)
            query.From.Add(precios)
            query.From.Add(temporada)
            query.From.Add(rangos)

            Dim consulta As String = query.RelationalSelectQuery
            Return connection.executeSelect_DT(consulta)
        End Function

        Private Function cargarItemsEnUbicacion(ByVal id_TipoProducto As Integer, ByVal id_Producto As Integer, ByVal fechaInicioUtc As Date, ByVal fechaFinUtc As Date, ByVal id_ubicacion As Integer) As Data.DataTable
            Dim item As New Item
            Dim query As New QueryBuilder
            item.descripcion.ToSelect = True
            item.ordinal.ToSelect = True

            'Filtro Fechas
            'OJO: tomar de maquinariacr
            'Dim consultaInternaFechas As String = crearConsultaInternaFechas(id_producto, fechaInicio, fechaFin)
            'Dim consultaFechasPrevias As String = crearConsultaLimiteFecha(id_producto, fechaInicio.AddDays(-5), fechaInicio, True)
            'Dim consultaFechasPosteriores As String = crearConsultaLimiteFecha(id_producto, fechaFin, fechaFin.AddDays(5), False)

            'item.ordinal.Where.NotInCondition(consultaInternaFechas, Where.FieldRelations.AND_)
            'item.ordinal.Where.NotInCondition(consultaFechasPrevias, Where.FieldRelations.AND_)
            'item.ordinal.Where.NotInCondition(consultaFechasPosteriores, Where.FieldRelations.AND_)
            'item.ordinal.Where.InCondition(consultaInternaFechas, Where.FieldRelations.AND_)

            'Filtro ubicacion
            Dim consultaUltimaUbicacion As String = crearConsultaUltimaUbicacionNegative(item, id_ubicacion, fechaInicioUtc)
            Dim consultaInternaNoUbicacion As String = crearConsultaNoUbicacionDelTodo(item)

            Dim condicionUbicacionSi As Condition = item.ordinal.Where.NotInCondition(consultaUltimaUbicacion, Where.FieldRelations.AND_)
            Dim condicionUbicacionNo As Condition = item.ordinal.Where.InCondition(consultaInternaNoUbicacion, Where.FieldRelations.OR_)
            query.GroupConditions(condicionUbicacionSi, condicionUbicacionNo)

            'Filtro Temporada
            Dim precios As New Precios_Temporada
            Dim temporada As New Temporada
            Dim rangos As New RangoTemporada

            Dim resultadoTemp As ResultadoBusquedaTemporada = buscarTemporada(fechaInicioUtc, fechaFinUtc)
            rangos.Ordinal.Where.EqualCondition(resultadoTemp.Ordinal)
            rangos.Id_Temporada.Where.EqualCondition(resultadoTemp.id_Temporada)
            precios.Fields.SelectAll()
            temporada.Nombre.ToSelect = True
            rangos.Fields.SelectAll()
            query.Join.EqualCondition(temporada.Id_Temporada, precios.Id_Temporada)
            query.Join.EqualCondition(temporada.Id_Temporada, rangos.Id_Temporada)

            'Productos
            Dim producto As New Producto
            producto.Nombre.ToSelect = True
            producto.Id_Producto.ToSelect = True
            If id_TipoProducto > 0 And id_Producto = 0 Then
                producto.Id_tipoProducto.Where.EqualCondition(id_TipoProducto)
            ElseIf id_TipoProducto = 0 And id_Producto > 0 Then
                producto.Id_Producto.Where.EqualCondition(id_Producto)
            Else
                Throw New Exception("Error al filtrar, filtros no exclusivos")
            End If
            query.Join.EqualCondition(precios.Id_Producto, producto.Id_Producto)
            query.Join.EqualCondition(producto.Id_Producto, item.Id_producto)

            'Moneda
            Dim unidad As New Moneda
            unidad.Simbolo.ToSelect = True
            query.Join.EqualCondition(unidad.Id_Moneda, precios.Id_Moneda)

            'Consulta
            query.From.Add(item)
            query.From.Add(producto)
            query.From.Add(unidad)
            query.From.Add(precios)
            query.From.Add(temporada)
            query.From.Add(rangos)

            Dim consulta As String = query.RelationalSelectQuery
            Return connection.executeSelect_DT(consulta)
        End Function

        'Consultas interna Fechas
        Private Function crearConsultaInternaFechas(ByVal itemFuera As Item, ByVal fechaInicioUtc As Date, ByVal fechaFinUtc As Date) As String


            Dim fechaInicio As New Date(fechaInicioUtc.Year, fechaInicioUtc.Month, fechaInicioUtc.Day, 12, 0, 0)
            Dim fechaFin As New Date(fechaFinUtc.Year, fechaFinUtc.Month, fechaFinUtc.Day, 11, 0, 0)

            fechaInicioUtc = fechaInicio
            fechaFinUtc = fechaFin

            Dim reservacion As New Reservacion
            Dim detalle As New Detalle_Reservacion
            Dim item As New Item
            Dim tipoEstado As New TipoEstadoReservacion

            Dim query2 As New QueryBuilder()
            item.ordinal.ToSelect = True

            Dim condicion1 As Condition = reservacion.fecha_inicioProgramado.Where.GreaterThanOrEqualCondition(fechaInicioUtc)
            Dim condicion2 As Condition = reservacion.fecha_inicioProgramado.Where.LessThanOrEqualCondition(fechaFinUtc)
            Dim condicion3 As Condition = query2.GroupConditions(condicion1, condicion2, Where.FieldRelations.OR_)

            Dim condicion4 As Condition = reservacion.fecha_finalProgramado.Where.GreaterThanOrEqualCondition(fechaInicioUtc)
            Dim condicion5 As Condition = reservacion.fecha_finalProgramado.Where.LessThanOrEqualCondition(fechaFinUtc)
            Dim condicion6 As Condition = query2.GroupConditions(condicion4, condicion5, Where.FieldRelations.OR_)

            Dim condicion7 As Condition = reservacion.fecha_inicioProgramado.Where.LessThanCondition(fechaInicioUtc)
            Dim condicion8 As Condition = reservacion.fecha_finalProgramado.Where.GreaterThanCondition(fechaFinUtc)
            Dim condicion9 As Condition = query2.GroupConditions(condicion7, condicion8, Where.FieldRelations.OR_)

            Dim condicion10 As Condition = query2.GroupConditions(condicion6, condicion3, Where.FieldRelations.OR_)
            query2.GroupConditions(condicion9, condicion10)

            tipoEstado.RepresentaReservado.Where.EqualCondition(1)

            item.AsName = "itemIntA"
            query2.Join.EqualCondition(item.ordinal, detalle.ordinal, Join.JoinTypes.LEFT_OUTER_JOIN)
            query2.Join.EqualCondition(item.Id_producto, detalle.Id_producto, Join.JoinTypes.LEFT_OUTER_JOIN)

            item.ordinal.Where.EqualCondition_OuterScript(itemFuera.ordinal)
            item.Id_producto.Where.EqualCondition_OuterScript(itemFuera.Id_producto)

            query2.Join.EqualCondition(detalle.id_reservacion, reservacion.Id_Reservacion)
            query2.Join.EqualCondition(reservacion.Id_TipoEstado, tipoEstado.Id_TipoEstadoReservacion)

            query2.From.Add(item)
            query2.From.Add(reservacion)
            query2.From.Add(detalle)
            query2.From.Add(tipoEstado)

            Dim queryString As String = query2.RelationalSelectQuery
            Return queryString
        End Function

        'Consultas internas ubicacion
        Private Function crearConsultaUltimaUbicacionNegative(ByVal itemFuera As Item, ByVal id_ubicacion As Integer, ByVal fechaInicioUtc As Date) As String
            Dim reservacion As New Reservacion
            Dim detalle As New Detalle_Reservacion
            Dim query2 As New QueryBuilder()
            Dim item As New Item
            Dim tipoEstado As New TipoEstadoReservacion

            item.ordinal.ToSelect = True

            reservacion.fecha_finalProgramado.Where.LessThanCondition(fechaInicioUtc)
            reservacion.fecha_finalProgramado.Where.GreaterThanCondition(fechaInicioUtc.AddDays(-15))

            tipoEstado.Terminador.Where.EqualCondition(1)

            reservacion.Id_Ubicacion.Where.DiferentCondition(id_ubicacion)

            item.AsName = "itemIntB"
            query2.Join.EqualCondition(item.ordinal, detalle.ordinal, Join.JoinTypes.LEFT_OUTER_JOIN)
            query2.Join.EqualCondition(item.Id_producto, detalle.Id_producto, Join.JoinTypes.LEFT_OUTER_JOIN)

            item.ordinal.Where.EqualCondition_OuterScript(itemFuera.ordinal)
            item.Id_producto.Where.EqualCondition_OuterScript(itemFuera.Id_producto)

            query2.Join.EqualCondition(detalle.id_reservacion, reservacion.Id_Reservacion)
            query2.Join.EqualCondition(reservacion.Id_TipoEstado, tipoEstado.Id_TipoEstadoReservacion)

            'query2.Top = 1
            'query2.Orderby.Add(reservacion.fecha_finalProgramado)
            query2.From.Add(item)
            query2.From.Add(reservacion)
            query2.From.Add(detalle)
            query2.From.Add(tipoEstado)

            Dim queryString As String = query2.RelationalSelectQuery
            Return queryString
        End Function

        Private Function crearConsultaNoUbicacionDelTodo(ByVal itemFuera As Item) As String
            Dim reservacion As New Reservacion
            Dim detalle As New Detalle_Reservacion
            Dim query2 As New QueryBuilder()
            Dim item As New Item

            item.ordinal.ToSelect = True

            Dim condicionUbicacionNull As Condition = reservacion.Id_Ubicacion.Where.IsNullCondition()

            item.AsName = "itemIntB"
            query2.Join.EqualCondition(item.ordinal, detalle.ordinal, Join.JoinTypes.LEFT_OUTER_JOIN)
            query2.Join.EqualCondition(item.Id_producto, detalle.Id_producto, Join.JoinTypes.LEFT_OUTER_JOIN)

            item.ordinal.Where.EqualCondition_OuterScript(itemFuera.ordinal)
            item.Id_producto.Where.EqualCondition_OuterScript(itemFuera.Id_producto)

            query2.Join.EqualCondition(detalle.id_reservacion, reservacion.Id_Reservacion, Join.JoinTypes.LEFT_OUTER_JOIN)

            query2.From.Add(item)
            query2.From.Add(detalle)
            query2.From.Add(reservacion)

            Dim queryString As String = query2.RelationalSelectQuery
            Return queryString
        End Function

        'Temporada
        'Private Function crearConsultaPreciosTemporada(ByVal rangosExterno As RangoTemporada, ByVal fechaInicioUtc As Date, ByVal fechaFinUtc As Date) As String
        '    Dim rangos As New RangoTemporada
        '    rangos.AsName = "RangoTemporadaInt"
        '    Dim query As New QueryBuilder()

        '    Dim condicion1 As Condition = rangos.fecha_inicio.Where.GreaterThanOrEqualCondition(fechaInicio)
        '    Dim condicion2 As Condition = rangos.fecha_inicio.Where.LessThanOrEqualCondition(fechaFin)
        '    Dim condicion3 As Condition = query.GroupConditions(condicion1, condicion2, Where.FieldRelations.OR_)

        '    Dim condicion4 As Condition = rangos.fecha_final.Where.GreaterThanOrEqualCondition(fechaInicio)
        '    Dim condicion5 As Condition = rangos.fecha_final.Where.LessThanOrEqualCondition(fechaFin)
        '    Dim condicion6 As Condition = query.GroupConditions(condicion4, condicion5, Where.FieldRelations.OR_)

        '    Dim condicion7 As Condition = rangos.fecha_inicio.Where.LessThanCondition(fechaInicio)
        '    Dim condicion8 As Condition = rangos.fecha_final.Where.GreaterThanCondition(fechaFin)
        '    Dim condicion9 As Condition = query.GroupConditions(condicion7, condicion8, Where.FieldRelations.OR_)

        '    Dim condicion10 As Condition = query.GroupConditions(condicion6, condicion3, Where.FieldRelations.OR_)
        '    query.GroupConditions(condicion9, condicion10)

        '    rangos.Ordinal.ToSelect = True
        '    rangos.Id_Temporada.Where.EqualCondition_OuterScript(rangosExterno.Id_Temporada)

        '    'query.Top=1
        '    'query.Orderby.Add(rangos.Ordinal)
        '    query.From.Add(rangos)

        '    Dim queryString As String = query.RelationalSelectQuery
        '    Return queryString
        'End Function

        Private Function buscarPrecioTemporada(ByVal id_temporada As Integer, ByVal id_Producto As Integer, Optional ByVal personas As Integer = 0, Optional ByVal noches As Integer = 0) As Precios_Temporada
            Dim query As New QueryBuilder
            Dim precio As New Precios_Temporada

            precio.Id_Temporada.Where.EqualCondition(id_temporada)
            precio.Id_Producto.Where.EqualCondition(id_Producto)
            If personas > 0 Then
                precio.CantidadPersonas.Where.EqualCondition(personas)
            End If
            If noches > 0 Then
                precio.CantidadNoches.Where.EqualCondition(noches)
            End If
            precio.Fields.SelectAll()

            query.From.Add(precio)
            Dim elQuery As String = query.RelationalSelectQuery
            Dim dataTable As Data.DataTable = connection.executeSelect_DT(elQuery)
            If dataTable.Rows.Count > 0 Then
                ObjectBuilder.CreateObject(dataTable, 0, precio)
            Else
                precio = Nothing
            End If

            Return precio
        End Function

        Structure ResultadoBusquedaTemporada
            Dim id_Temporada As Integer
            Dim Ordinal As Integer
            Dim Nombre As String
            Dim fechaInicioUtc As Date
            Dim fechaFinUtc As Date
        End Structure

        Public Function buscarTemporada(ByVal fechaInicioUtc As Date, ByVal fechaFinUtc As Date) As ResultadoBusquedaTemporada
            Dim rangos As New RangoTemporada
            Dim temporada As New Temporada

            Dim datos As Data.DataTable = buscarTemporadas(rangos, temporada, fechaInicioUtc, fechaFinUtc)
            Dim resultado As New ResultadoBusquedaTemporada

            If datos IsNot Nothing Then

                resultado.Nombre = datos.Rows(0).Item(temporada.Nombre.SelectIndex)
                resultado.id_Temporada = datos.Rows(0).Item(temporada.Id_Temporada.SelectIndex)
                resultado.Ordinal = datos.Rows(0).Item(rangos.Ordinal.SelectIndex)
                resultado.fechaInicioUtc = datos.Rows(0).Item(rangos.fecha_inicio.SelectIndex)
                resultado.fechaFinUtc = datos.Rows(0).Item(rangos.fecha_final.SelectIndex)
            End If

            Return resultado
        End Function

        Public Function perteneceATemporada(ByVal laFechaUtc As Date) As Boolean
            Dim resultado As Boolean = False
            Dim rangos As New RangoTemporada
            Dim temporada As New Temporada
            Dim datos As Data.DataTable = buscarTemporadas(rangos, temporada, laFechaUtc, laFechaUtc)
            If Not datos Is Nothing Then
                If datos.Rows.Count > 0 Then
                    resultado = True
                End If
            End If
            Return resultado
        End Function

        Private Function buscarTemporadas(ByVal rangos As RangoTemporada, ByVal temporada As Temporada, ByVal fechaInicioUtc As Date, ByVal fechaFinUtc As Date) As Data.DataTable

            Dim query2 As New QueryBuilder()

            Dim condicion1 As Condition = rangos.fecha_inicio.Where.GreaterThanOrEqualCondition(fechaInicioUtc)
            Dim condicion2 As Condition = rangos.fecha_inicio.Where.LessThanOrEqualCondition(fechaFinUtc)
            Dim condicion3 As Condition = query2.GroupConditions(condicion1, condicion2, Where.FieldRelations.OR_)

            Dim condicion4 As Condition = rangos.fecha_final.Where.GreaterThanOrEqualCondition(fechaInicioUtc)
            Dim condicion5 As Condition = rangos.fecha_final.Where.LessThanOrEqualCondition(fechaFinUtc)
            Dim condicion6 As Condition = query2.GroupConditions(condicion4, condicion5, Where.FieldRelations.OR_)

            Dim condicion7 As Condition = rangos.fecha_inicio.Where.LessThanOrEqualCondition(fechaInicioUtc)
            Dim condicion8 As Condition = rangos.fecha_final.Where.GreaterThanOrEqualCondition(fechaFinUtc)
            Dim condicion9 As Condition = query2.GroupConditions(condicion7, condicion8, Where.FieldRelations.OR_)

            Dim condicion10 As Condition = query2.GroupConditions(condicion6, condicion3, Where.FieldRelations.OR_)
            query2.GroupConditions(condicion9, condicion10)

            temporada.Id_Temporada.ToSelect = True
            temporada.Nombre.ToSelect = True
            temporada.Prioridad.ToSelect = True
            rangos.fecha_inicio.ToSelect = True
            rangos.fecha_final.ToSelect = True
            rangos.Ordinal.ToSelect = True

            query2.Join.EqualCondition(rangos.Id_Temporada, temporada.Id_Temporada)

            query2.Orderby.Add(temporada.Prioridad, False)
            query2.Orderby.Add(rangos.fecha_inicio)

            query2.From.Add(rangos)
            query2.From.Add(temporada)

            Dim queryString As String = query2.RelationalSelectQuery
            Dim datos As Data.DataTable = Nothing
            Dim dataTable As Data.DataTable = connection.executeSelect_DT(queryString)

            If dataTable.Rows.Count > 0 Then
                datos = dataTable
                datos.Columns(temporada.Id_Temporada.SelectIndex).ColumnMapping = Data.MappingType.Hidden
                datos.Columns(temporada.Nombre.SelectIndex).ColumnName = "Temporada"
                datos.Columns(rangos.fecha_inicio.SelectIndex).ColumnName = "Fecha Inicio"
                datos.Columns(rangos.fecha_final.SelectIndex).ColumnName = "Fecha Final"
                datos.Columns(rangos.Ordinal.SelectIndex).ColumnMapping = Data.MappingType.Hidden
            End If
            Return datos
        End Function

        Structure ResultadoMontosUnitarios
            Dim MontoUnitario As Double
            Dim MontoUnitarioExtra As Double
            'Dim TotalCalculoPrevio As Double
            Dim unidad As Integer
            Dim dias As Integer
        End Structure

        Structure unidadesPorTemporada
            Dim id_temporada As Integer
            Dim cantidadUnidades As Integer
        End Structure

        Private Function calcularMontosUnitario(ByVal precio As Precios_Temporada, ByVal capacidadProducto As Integer, ByVal losAdultos As Integer, ByVal losNinos As Integer) As ResultadoMontosUnitarios
            Dim resultado As ResultadoMontosUnitarios
            Dim adultosCapacidad As Integer = 0
            Dim ninosCapacidad As Integer = 0
            Dim adultosExtra As Integer = 0
            Dim ninosExtra As Integer = 0

            'Calcula la distribucion de adultos y ninos
            If losAdultos <= capacidadProducto Then
                adultosCapacidad = losAdultos
                If losAdultos + losNinos <= capacidadProducto Then
                    ninosCapacidad = losNinos
                Else
                    ninosCapacidad = capacidadProducto - losAdultos
                    ninosExtra = losNinos - ninosCapacidad
                End If
            Else
                adultosCapacidad = capacidadProducto
                adultosExtra = losAdultos - adultosCapacidad
                ninosCapacidad = 0
                ninosExtra = losNinos
            End If

            resultado.MontoUnitario = precio.PrecioProducto.Value
            resultado.MontoUnitario += precio.PrecioAdAdulto.Value * adultosCapacidad
            resultado.MontoUnitario += precio.PrecioAdNino.Value * ninosCapacidad
            resultado.MontoUnitarioExtra = precio.PrecioAdAdultoExtra.Value * adultosExtra
            resultado.MontoUnitarioExtra += precio.PrecioAdNinoExtra.Value * ninosExtra
            resultado.unidad = precio.UnidadTiempo.Value

            If precioTransporte <> 0 Then
                resultado.MontoUnitario += precioTransporte * losAdultos
                resultado.MontoUnitario += precio.PrecioAdNino.Value * losNinos
            End If

            'resultado.TotalCalculoPrevio = 0
            Return resultado
        End Function
        Private Function calcularMontosUnitarioXnoches(ByVal precio As Precios_Temporada, ByVal losAdultos As Integer, ByVal losNinos As Integer, ByVal nochesAdicionales As Integer, Optional ByVal id_TemporadaNueva As Integer = 0) As ResultadoMontosUnitarios
            Dim resultado As ResultadoMontosUnitarios
            'Dim adultosCapacidad As Integer = 0
            'Dim ninosCapacidad As Integer = 0
            'Dim adultosExtra As Integer = 0
            'Dim ninosExtra As Integer = 0

            'Calcula la distribucion de adultos y ninos
            'If losAdultos <= capacidadProducto Then
            '    adultosCapacidad = losAdultos
            '    'If losAdultos + losNinos <= capacidadProducto Then
            '    '    ninosCapacidad = losNinos
            '    'Else
            '    '    ninosCapacidad = capacidadProducto - losAdultos
            '    '    ninosExtra = losNinos - ninosCapacidad
            '    'End If
            'Else
            '    adultosCapacidad = capacidadProducto
            '    'adultosExtra = losAdultos - adultosCapacidad
            '    ninosCapacidad = 0
            '    'ninosExtra = losNinos
            'End If

            If id_TemporadaNueva <> 0 Then
                resultado.MontoUnitario = precio.PrecioProducto.Value
            Else
                resultado.MontoUnitario = precio.PrecioProducto.Value * losAdultos
            End If

            If id_TemporadaNueva = 14 Then
                resultado.MontoUnitario = precio.PrecioProducto.Value * losAdultos
            End If

            '  resultado.MontoUnitario += precio.PrecioAdAdulto.Value * adultosCapacidad
            '  resultado.MontoUnitario += precio.PrecioAdNino.Value * ninosCapacidad
            '  resultado.MontoUnitarioExtra = precio.PrecioAdAdultoExtra.Value * adultosExtra
            '  resultado.MontoUnitarioExtra += precio.PrecioAdNinoExtra.Value * ninosExtra

            resultado.MontoUnitarioExtra = (precio.PrecioNocheExtraAd.Value * losAdultos * nochesAdicionales) + (precio.PrecioNocheExtraNino.Value * losNinos * nochesAdicionales)

            resultado.unidad = precio.UnidadTiempo.Value
            'resultado.TotalCalculoPrevio = 0
            Return resultado
        End Function

        Private Function calcularCantidadFacturar(ByVal fechaInicioUtc As Date, ByVal fechaFinUtc As Date, ByVal unidad As unidadTiempo) As Integer
            Dim horas As Double = DateDiff(DateInterval.Hour, fechaInicioUtc, fechaFinUtc)
            Dim total As Integer = Math.Ceiling(horas / unidad)
            Return total
        End Function

        'Verificadores
        Private Function IsReservacionDisponible(ByVal id_Reservacion As Integer) As Resultado_Code
            Dim reservacion As Reservacion = Me.ConsultarReservacion(id_Reservacion)
            Dim disponible As Resultado_Code = Resultado_Code.OK

            Dim detalle As New Detalle_Reservacion
            Dim query As New QueryBuilder

            detalle.Fields.SelectAll()
            detalle.id_reservacion.Where.EqualCondition(id_Reservacion)
            query.From.Add(detalle)

            Dim consulta As String = query.RelationalSelectQuery
            Dim dataTable As Data.DataTable = connection.executeSelect_DT(consulta)

            If dataTable.Rows.Count > 0 Then
                Dim resultados As ArrayList = ObjectBuilder.TransformDataTable(dataTable, detalle)

                For Each act_Detalle As Detalle_Reservacion In resultados

                    If Not IsItemDisponible(act_Detalle.Id_producto.Value, act_Detalle.ordinal.Value, reservacion.fecha_inicioProgramado.ValueUtc, reservacion.fecha_finalProgramado.ValueUtc, reservacion.Id_Ubicacion.Value) = Resultado_Code.OK Then
                        disponible = Resultado_Code.Item_noDisponible
                        Exit For
                    End If
                Next
            End If
            Return disponible
        End Function

        Enum Resultado_Code As Integer
            OK
            DateNo_UbicacionYes
            DateNo_UbicacionNo
            DateYes_UbicacionNo
            ExcedeMaximo
            Repite_Estado
            Item_noDisponible
        End Enum

        Enum unidadTiempo As Integer
            Dia = 24
            Hora = 1
            _8_dias = 24 * 8
            _7_dias = 24 * 7
            _5_dias = 24 * 5
        End Enum

        Public Function obtenerMensajeError(ByVal code As Resultado_Code) As String
            Dim mensaje As String = ""
            Select Case code
                Case Resultado_Code.Repite_Estado
                    mensaje = "La reservacion no puede volver a un estado anterior"

                Case Resultado_Code.Item_noDisponible
                    mensaje = "Los productos a reservar no se encuentran disponibles"

                Case Resultado_Code.ExcedeMaximo
                    mensaje = "Excede la capacidad de personas"
            End Select

            Return mensaje
        End Function

        ''' <summary>
        ''' Verifica disponibilidad de un item
        ''' </summary>
        ''' <param name="id_producto"></param>
        ''' <param name="ordinal"></param>
        ''' <param name="fechaInicioUtc"></param>
        ''' <param name="fechaFinUtc"></param>
        ''' <param name="id_ubicacion"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function IsItemDisponible(ByVal id_producto As Integer, ByVal ordinal As Integer, ByVal fechaInicioUtc As Date, ByVal fechaFinUtc As Date, ByVal id_ubicacion As Integer) As Resultado_Code
            Dim resultado As Resultado_Code

            If Me.IsItemDisponible_ByDate(id_producto, ordinal, fechaInicioUtc, fechaFinUtc) Then
                If Me.IsItemDisponible_ByUbicacion(id_producto, ordinal, fechaInicioUtc, id_ubicacion) Then
                    resultado = Resultado_Code.OK
                Else
                    resultado = Resultado_Code.DateYes_UbicacionNo
                End If
            Else
                If Me.IsItemDisponible_ByUbicacion(id_producto, ordinal, fechaInicioUtc, id_ubicacion) Then
                    resultado = Resultado_Code.DateNo_UbicacionYes
                Else
                    resultado = Resultado_Code.DateNo_UbicacionNo
                End If
            End If

            Return resultado
        End Function

        Public Function IsItemDisponible_ByReservacion(ByVal id_reservacion As Integer, ByVal id_producto As Integer, ByVal ordinal As Integer, Optional ByVal items_a_reservar As Integer = 1) As Resultado_Code
            Dim resultado As Resultado_Code
            Dim reservacionHecha As Reservacion = Me.ConsultarReservacion(id_reservacion)
            Dim id_ubicacion As Integer = reservacionHecha.Id_Ubicacion.Value

            If Me.IsItemDisponible_ByDate(id_producto, ordinal, reservacionHecha.fecha_inicioProgramado.ValueUtc, reservacionHecha.fecha_finalProgramado.ValueUtc) Then
                If Me.IsItemDisponible_ByUbicacion(id_producto, ordinal, reservacionHecha.fecha_inicioProgramado.ValueUtc, id_ubicacion) Then
                    resultado = Resultado_Code.OK
                Else
                    resultado = Resultado_Code.DateYes_UbicacionNo
                End If
            Else
                If Me.IsItemDisponible_ByUbicacion(id_producto, ordinal, reservacionHecha.fecha_inicioProgramado.ValueUtc, id_ubicacion) Then
                    resultado = Resultado_Code.DateNo_UbicacionYes
                Else
                    resultado = Resultado_Code.DateNo_UbicacionNo
                End If
            End If

            Return resultado
        End Function

        Public Function IsItemDisponible_WithoutReservation(ByVal id_producto As Integer, ByVal ordinal As Integer, ByVal ubicacion As Integer, ByVal fechaInicio As Date, ByVal fechaFinal As Date, Optional ByVal items_a_reservar As Integer = 1) As Resultado_Code
            Dim resultado As Resultado_Code
            Dim id_ubicacion As Integer = ubicacion

            If Me.IsItemDisponible_ByDate(id_producto, ordinal, fechaInicio, fechaFinal) Then
                If Me.IsItemDisponible_ByUbicacion(id_producto, ordinal, fechaInicio, id_ubicacion) Then
                    resultado = Resultado_Code.OK
                Else
                    resultado = Resultado_Code.DateYes_UbicacionNo
                End If
            Else
                If Me.IsItemDisponible_ByUbicacion(id_producto, ordinal, fechaInicio, id_ubicacion) Then
                    resultado = Resultado_Code.DateNo_UbicacionYes
                Else
                    resultado = Resultado_Code.DateNo_UbicacionNo
                End If
            End If

            Return resultado
        End Function

        Protected Function cargarItemsDisponibles(ByVal id_producto As Integer, ByVal fechaInicio As Date, ByVal fechaFin As Date, ByVal id_ubicacion As Integer) As Data.DataTable

            'Dim id_ubicacion As Integer = Config_UbicacionDefault

            Dim controladora As New ControladorReservaciones(connection, Resources.Reservaciones_Resources.ResourceManager)
            Dim dataTable As Data.DataTable = controladora.cargarItemsDisponibles_ByProducto(id_producto, fechaInicio, fechaFin, id_ubicacion)

            Return dataTable
        End Function

        Private Function IsItemDisponible_ByDate(ByVal id_producto As Integer, ByVal ordinal As Integer, ByVal fechaInicioUtc As Date, ByVal fechaFinUtc As Date, Optional ByVal items_a_reservar As Integer = 1) As Boolean
            Dim item As New Item
            Dim query As New QueryBuilder

            'Filtro Fechas
            Dim consultaInternaFechas As String = crearConsultaInternaFechas(item, fechaInicioUtc, fechaFinUtc)
            item.ordinal.Where.NotInCondition(consultaInternaFechas, Where.FieldRelations.AND_)

            'Campos a seleccionar
            item.Id_producto.ToSelect = True
            item.ordinal.ToSelect = True

            item.Id_producto.Where.EqualCondition(id_producto)
            item.ordinal.Where.EqualCondition(ordinal)

            query.From.Add(item)

            Dim consulta As String = query.RelationalSelectQuery
            Dim dataTable As Data.DataTable = connection.executeSelect_DT(consulta)
            Dim disponible As Boolean = False
            If dataTable.Rows.Count > 0 Then
                If dataTable.Rows.Count >= items_a_reservar Then
                    disponible = True
                End If
            End If
            Return disponible
        End Function

        Private Function IsItemDisponible_ByUbicacion(ByVal id_producto As Integer, ByVal ordinal As Integer, ByVal fechaInicioUtc As Date, ByVal id_ubicacion As Integer, Optional ByVal items_a_reservar As Integer = 1) As Boolean
            Dim item As New Item
            Dim query As New QueryBuilder

            'Filtro ubicacion
            Dim consultaUltimaUbicacion As String = crearConsultaUltimaUbicacionNegative(item, id_ubicacion, fechaInicioUtc)
            Dim consultaInternaNoUbicacion As String = crearConsultaNoUbicacionDelTodo(item)

            Dim condicionUbicacionSi As Condition = item.ordinal.Where.NotInCondition(consultaUltimaUbicacion, Where.FieldRelations.AND_)
            Dim condicionUbicacionNo As Condition = item.ordinal.Where.InCondition(consultaInternaNoUbicacion, Where.FieldRelations.OR_)
            query.GroupConditions(condicionUbicacionSi, condicionUbicacionNo)

            'Campos a seleccionar
            item.Id_producto.ToSelect = True
            item.ordinal.ToSelect = True

            item.Id_producto.Where.EqualCondition(id_producto)
            item.ordinal.Where.EqualCondition(ordinal)

            query.From.Add(item)

            Dim consulta As String = query.RelationalSelectQuery
            Dim dataTable As Data.DataTable = connection.executeSelect_DT(consulta)
            Dim disponible As Boolean = False
            If dataTable.Rows.Count > 0 Then
                If dataTable.Rows.Count >= items_a_reservar Then
                    disponible = True
                End If
            End If
            Return disponible
        End Function

        Public Function EliminarDetalle(ByVal id_reservacion As Integer, ByVal id_producto As Integer, ByVal ordinal As Integer, ByVal id_entidad As Integer, ByVal comentario As String) As Boolean
            Try
                Dim reserva As Reservacion = ConsultarReservacion(id_reservacion)
                Dim query As New QueryBuilder
                If CantidadItemsEnReservacion(id_reservacion) > 1 Or reserva.Id_TipoEstado.Value = BuscaEstadoBloqueado() Then
                    Dim detalle As New Detalle_Reservacion
                    detalle.Id_producto.Where.EqualCondition(id_producto)
                    detalle.ordinal.Where.EqualCondition(ordinal)
                    detalle.id_reservacion.Where.EqualCondition(id_reservacion)

                    Dim consulta As String = query.DeleteQuery(detalle)
                    If connection.executeDelete(consulta) > 0 Then
                        Return True
                    End If
                Else
                    Dim controladora As New ControladorReservaciones(connection, Resources.Reservaciones_Resources.ResourceManager)

                    Dim code As ControladorReservaciones.Resultado_Code
                    If reserva.Id_TipoEstado.Value <> BuscaEstadoBloqueado() Then
                        code = controladora.NuevoEstadoParaReservacion(id_reservacion, BuscarEstadoCancelado, id_entidad, comentario)
                    Else
                        code = controladora.NuevoEstadoParaReservacion(id_reservacion, BuscarEstadoCancelado, id_entidad, comentario, False)
                    End If
                    If code = Resultado_Code.OK Then
                        Return True
                    End If
                End If
            Catch ex As Exception
            End Try
            Return False
        End Function

        Public Function SelectReservaciones(Optional ByVal top As Integer = 0) As Data.DataTable
            Dim reservacion As New Reservacion
            Dim detalle As New Detalle_Reservacion
            Dim entidad As New Entidad
            Dim tipoEstado As New TipoEstadoReservacion

            Dim query As New QueryBuilder

            'Select
            Me.QueryReservaciones_DoSelect(query, reservacion, detalle, entidad, tipoEstado)
            query.Orderby.Add(reservacion.Id_Reservacion, False)
            query.Top = top

            Return QueryReservaciones_DoQuery(query.RelationalSelectQuery, reservacion, detalle, tipoEstado, entidad)
        End Function

        Public Function SearchReservaciones(ByVal id_Cliente As Integer, ByVal id_Ubicacion As Integer, ByVal id_TipoEstado As Integer, ByVal fechaInicio As Date, ByVal fechafinal As Date) As Data.DataTable
            Dim reservacion As New Reservacion
            Dim detalle As New Detalle_Reservacion
            Dim entidad As New Entidad
            Dim tipoEstado As New TipoEstadoReservacion

            Dim query As New QueryBuilder

            'Select
            Me.QueryReservaciones_DoSelect(query, reservacion, detalle, entidad, tipoEstado)

            'Filtros
            If id_Cliente > 0 Then
                entidad.Id_entidad.Where.EqualCondition(id_Cliente)
            End If

            If id_Ubicacion > 0 Then
                reservacion.Id_Ubicacion.Where.EqualCondition(id_Ubicacion)
            End If

            If id_TipoEstado > 0 Then
                reservacion.Id_TipoEstado.Where.EqualCondition(id_TipoEstado)
            End If
            If fechaInicio > Date.MinValue Then
                reservacion.fecha_inicioProgramado.Where.GreaterThanOrEqualCondition(fechaInicio)
            End If

            If fechafinal > Date.MinValue Then
                reservacion.fecha_inicioProgramado.Where.LessThanOrEqualCondition(fechafinal)
            End If
            'Execute
            Return QueryReservaciones_DoQuery(query.RelationalSelectQuery, reservacion, detalle, tipoEstado, entidad)
        End Function

        Private Sub QueryReservaciones_DoSelect(ByVal query As QueryBuilder, ByVal reservacion As Reservacion, ByVal detalle As Detalle_Reservacion, ByVal entidad As Entidad, ByVal tipoEstado As TipoEstadoReservacion)
            entidad.NombreDisplay.ToSelect = True

            reservacion.fecha_inicioProgramado.ToSelect = True
            reservacion.Id_Reservacion.ToSelect = True
            reservacion.Id_TipoEstado.ToSelect = True
            reservacion.descripcion.ToSelect = True

            tipoEstado.Nombre.ToSelect = True

            detalle.ordinal.AggregateFunction = Field.AggregateFunctionList.COUNT

            query.Join.EqualCondition(detalle.id_reservacion, reservacion.Id_Reservacion)
            query.Join.EqualCondition(entidad.Id_entidad, reservacion.id_Cliente)
            query.Join.EqualCondition(tipoEstado.Id_TipoEstadoReservacion, reservacion.Id_TipoEstado)

            query.From.Add(entidad)
            query.From.Add(reservacion)
            query.From.Add(detalle)
            query.From.Add(tipoEstado)
        End Sub

        Private Function QueryReservaciones_DoQuery(ByVal theQuery As String, ByRef theReservacion As Reservacion, ByVal theDetalle As Detalle_Reservacion, ByVal theTipoEstado As TipoEstadoReservacion, ByRef theEntidad As Entidad) As Data.DataTable
            Dim datos As Data.DataTable = Nothing
            Dim dataTable As Data.DataTable = connection.executeSelect_DT(theQuery)

            If dataTable.Rows.Count > 0 Then
                datos = dataTable
                Dim columna As Data.DataColumn = datos.Columns(theReservacion.Id_Reservacion.SelectIndex)
                columna.ColumnMapping = Data.MappingType.Hidden

                datos.Columns(theReservacion.Id_TipoEstado.SelectIndex).ColumnMapping = Data.MappingType.Hidden

                columna = datos.Columns(theReservacion.fecha_inicioProgramado.SelectIndex)
                columna.ColumnName = "Fecha_Inicio_Programado"

                columna = datos.Columns(theEntidad.NombreDisplay.SelectIndex)
                columna.ColumnName = "Cliente"

                datos.Columns(theTipoEstado.Nombre.SelectIndex).ColumnName = "Estado"


                datos.Columns(theDetalle.ordinal.SelectIndex).ColumnName = "Items"
            End If
            Return datos
        End Function

        'Otros
        Public Function obtenerFactura(ByVal id_reservacion As Integer) As Integer
            Dim numFactura As Integer = 0
            Dim reserva As New Reservacion

            reserva.Id_factura.ToSelect = True
            reserva.Id_Reservacion.Where.EqualCondition(id_reservacion)

            Dim query As New QueryBuilder
            Dim consulta As String = query.SelectQuery(reserva)
            Dim dataTable As Data.DataTable = connection.executeSelect_DT(consulta)
            If dataTable.Rows.Count > 0 Then
                ObjectBuilder.CreateObject(dataTable, 0, reserva)
                If reserva.Id_factura.IsValid Then
                    numFactura = reserva.Id_factura.Value
                End If
            End If
            Return numFactura
        End Function

        Public Function obtenerCliente(ByVal id_reservacion As Integer) As Integer
            Dim id_cliente As Integer = 0
            Dim reserva As New Reservacion

            reserva.id_Cliente.ToSelect = True
            reserva.Id_Reservacion.Where.EqualCondition(id_reservacion)

            Dim query As New QueryBuilder
            Dim consulta As String = query.SelectQuery(reserva)
            Dim dataTable As Data.DataTable = connection.executeSelect_DT(consulta)
            If dataTable.Rows.Count > 0 Then
                ObjectBuilder.CreateObject(dataTable, 0, reserva)
                id_cliente = reserva.id_Cliente.Value
            End If
            Return id_cliente
        End Function
        Public Function Emails(ByVal id_reservacion As String, Optional ByVal solicitud As Boolean = False) As Boolean
            If Orbelink.DBHandler.LanguageHandler.CurrentLanguage = Orbelink.DBHandler.LanguageHandler.Language.INGLES Then
                System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("en-us")
            Else
                System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("es-cr")
            End If

            Dim QueryBuilder As New QueryBuilder
            Dim ds As Data.DataSet
            Dim reservacion As New Reservacion
            reservacion.Fields.SelectAll()
            reservacion.Id_Reservacion.Where.EqualCondition(id_reservacion)
            QueryBuilder.From.Add(reservacion)
            ds = connection.executeSelect(QueryBuilder.RelationalSelectQuery)

            If ds.Tables.Count > 0 Then
                If ds.Tables(0).Rows.Count > 0 Then
                    ObjectBuilder.CreateObject(ds.Tables(0), 0, reservacion)
                    'If reservacion.Id_TipoEstado.Value = BuscarEstadoReservado() Then
                    Dim FacturaMailer As New FacturaMailer(connection.connectionString)
                    Return FacturaMailer.EnviarEmailSegunEstado(reservacion.Id_factura.Value, True, reservacion.Id_Reservacion.Value, reservacion.fecha_inicioProgramado.ValueLocalized.ToLongDateString, reservacion.fecha_finalProgramado.ValueLocalized.ToLongDateString, reservacion.descripcion.Value, solicitud)
                    'End If

                End If
            End If
            Return False
        End Function
   
        Protected Function precioDiaTemporada(ByVal fechaUtc As Date, ByVal id_producto As Integer, ByVal capacidad As Integer, ByVal adultos As Integer, ByVal ninos As Integer) As Double
            Dim resultadoTemporada As ResultadoBusquedaTemporada = buscarTemporada(fechaUtc, fechaUtc)
            Dim precio As Precios_Temporada = buscarPrecioTemporada(resultadoTemporada.id_Temporada, id_producto)
            If Not precio Is Nothing Then
                Dim resMontos As ResultadoMontosUnitarios = calcularMontosUnitario(precio, capacidad, adultos, ninos)
                Return resMontos.MontoUnitario + resMontos.MontoUnitarioExtra
            Else
                Throw New Exception("EL " & fechaUtc & " no pertenece a ninguna temporada")
            End If
            Return 0
        End Function

        'Protected Sub diasXTemporada(ByVal reservacion As Reservacion, ByVal dataTemporadas As Data.DataTable)
        '    Dim inicio As Date = reservacion.fecha_inicioProgramado.Value
        '    Dim final As Date = reservacion.fecha_finalProgramado.Value
        '    Dim temporados As New ArrayList

        '    For counter As Integer = 0 To dataTemporadas.Rows.Count - 1
        '        Dim temporadas As New unidadesPorTemporada
        '        temporadas.id_temporada = dataTemporadas.Rows(counter).Item("id_Temporada")
        '        temporadas.cantidadUnidades = cantUnidadXTemporada(reservacion.fecha_inicioProgramado.Value, reservacion.fecha_finalProgramado.Value, temporadas.id_temporada, temporados)
        '        temporados.Add(temporadas)
        '    Next
        'End Sub

        'Protected Function cantUnidadXTemporada(ByVal fechaInicioUtc As Date, ByVal fechaFinUtc As Date, ByVal id_temporada As Integer, ByVal temporadas As ArrayList) As Integer
        '    Dim rango As New RangoTemporada
        '    Dim precios As New Precios_Temporada
        '    Dim dataSet As New Data.DataSet
        '    Dim query As New QueryBuilder
        '    Dim resultado As Integer = 0

        '    rango.Id_Temporada.ToSelect = True
        '    rango.fecha_inicio.ToSelect = True
        '    rango.fecha_final.ToSelect = True
        '    precios.UnidadTiempo.ToSelect = True
        '    rango.Id_Temporada.Where.EqualCondition(id_temporada)
        '    query.Join.EqualCondition(rango.Id_Temporada, precios.Id_Temporada)

        '    query.From.Add(rango)
        '    query.From.Add(precios)
        '    Dim consulta As String = query.RelationalSelectQuery
        '    dataSet = connection.executeSelect(consulta)
        '    If dataSet.Tables.Count > 0 Then
        '        If dataSet.Tables(0).Rows.Count > 0 Then
        '            ObjectBuilder.CreateObject(dataSet.Tables(0), 0, rango)
        '            ObjectBuilder.CreateObject(dataSet.Tables(0), 0, precios)

        '            If fechaInicio >= rango.fecha_inicio.Value And fechaFin <= rango.fecha_final.Value Then

        '                resultado = calcularCantidadFacturar(fechaInicio, fechaFin, precios.UnidadTiempo.Value)
        '                resultado = resultado - eliminarDiasRepetidos(fechaInicio, fechaFin, id_temporada, temporadas)

        '            ElseIf fechaInicio >= rango.fecha_inicio.Value And fechaFin > rango.fecha_final.Value Then

        '                resultado = calcularCantidadFacturar(fechaInicio, rango.fecha_final.Value, precios.UnidadTiempo.Value)
        '                resultado = resultado - eliminarDiasRepetidos(fechaInicio, rango.fecha_final.Value, id_temporada, temporadas)

        '            ElseIf fechaInicio < rango.fecha_inicio.Value And fechaFin <= rango.fecha_final.Value Then
        '                resultado = calcularCantidadFacturar(rango.fecha_inicio.Value, fechaFin, precios.UnidadTiempo.Value)
        '                resultado = resultado - eliminarDiasRepetidos(rango.fecha_inicio.Value, fechaFin, id_temporada, temporadas)
        '            End If
        '        End If


        '    End If

        '    Return resultado
        'End Function

        'Public Function eliminarDiasRepetidos(ByVal fechaInicioUtc As Date, ByVal fechaFinUtc As Date, ByVal id_temporada As Integer, ByVal temporadasRevisadas As ArrayList) As Integer
        '    Dim diasQuitar As Integer = 0
        '    Dim resultadoTemps As DataTable = obtenerTemporadas(fechaInicio, fechaFin)
        '    If Not tempPrioritaria(fechaInicio, fechaFin, id_temporada, resultadoTemps) Then
        '        For counter As Integer = 0 To resultadoTemps.Rows.Count - 1
        '            Dim elIdTemporada As Integer = resultadoTemps.Rows(counter).Item("id_Temporada")

        '        Next
        '    End If
        '    Return 0
        'End Function

        'Protected Function revisada(ByVal id_temporada As Integer, ByVal tempRevisadas As ArrayList) As Boolean
        '    Dim tempRevisada As Boolean = False
        '    Dim temporadas As unidadesPorTemporada

        '    For counter As Integer = 0 To tempRevisadas.Count - 1
        '        temporadas = tempRevisadas(counter)
        '        If temporadas.id_temporada = id_temporada Then
        '            tempRevisada = True
        '        End If
        '    Next
        '    Return tempRevisada
        'End Function

        'Public Function tempPrioritaria(ByVal fechaInicioUtc As Date, ByVal fechaFinUtc As Date, ByVal id_temporada As Integer, ByVal resultadoTemps As Data.DataTable) As Boolean
        '    Dim prioridad As Boolean = False
        '    Dim temporadaPrioritaria As Integer = resultadoTemps.Rows(0).Item("id_Temporada")
        '    If id_temporada = temporadaPrioritaria Then
        '        prioridad = True
        '    End If
        '    Return prioridad
        'End Function

        Public Function TotalNoches(ByVal fechaInicio As Date, ByVal fechaFin As Date) As Integer
            Dim noches As Integer = 0
            Dim horas As Integer = DateDiff(DateInterval.Hour, fechaInicio, fechaFin)
            If horas = 23 Then
                noches = 1
            Else
                If horas >= 24 Then
                    noches = horas / 24
                End If
            End If

            Return noches
        End Function

        Public Function NochesSegunTarifas(ByVal id_temporada As Integer, ByVal id_producto As Integer, ByVal personas As Integer, ByVal totalNoches As Integer) As Integer
            Dim noches As Integer = totalNoches
            Dim precios As New Precios_Temporada
            precios.PrecioProducto.ToSelect = True
            If id_producto > 0 Then
                precios.Id_Producto.Where.EqualCondition(id_producto)
            End If
            If id_temporada > 0 Then
                precios.Id_Temporada.Where.EqualCondition(id_temporada)
            End If
            If personas > 0 Then
                precios.CantidadPersonas.Where.EqualCondition(personas)
            End If
            precios.CantidadNoches.ToSelect = True
            Dim QueryBuilder As New QueryBuilder
            QueryBuilder.From.Add(precios)
            queryBuilder.Orderby.Add(precios.CantidadNoches, False)

            Dim consulta As String = queryBuilder.RelationalSelectQuery
            Dim dataTable As Data.DataTable = connection.executeSelect_DT(consulta)

            If dataTable.Rows.Count > 0 Then
                For i As Integer = 0 To dataTable.Rows.Count - 1
                    ObjectBuilder.CreateObject(dataTable, i, precios)
                    If precios.CantidadNoches.Value < totalNoches Or precios.CantidadNoches.Value = totalNoches Then
                        noches = precios.CantidadNoches.Value
                        Exit For
                    End If
                Next
            End If
            Return noches
        End Function

        Public Function existPrecios(ByVal id_temporada As Integer, ByVal id_producto As Integer, ByVal noches As Integer, ByVal personas As Integer) As Boolean
            Dim precios As New Precios_Temporada
            precios.PrecioProducto.ToSelect = True
            If id_producto > 0 Then
                precios.Id_Producto.Where.EqualCondition(id_producto)
            End If
            If id_temporada > 0 Then
                precios.Id_Temporada.Where.EqualCondition(id_temporada)
            End If
            If personas > 0 Then
                precios.CantidadPersonas.Where.EqualCondition(personas)
            End If
            If noches > 0 Then
                precios.CantidadNoches.Where.EqualCondition(noches)
            End If
            Dim QueryBuilder As New QueryBuilder
            QueryBuilder.From.Add(precios)
            Dim consulta As String = QueryBuilder.RelationalSelectQuery
            Dim dataTable As Data.DataTable = connection.executeSelect_DT(consulta)

            If dataTable.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        End Function

        Public Function crearFechaHora(ByVal fecha As String, ByVal hora As String, ByVal minuto As String) As Date
            Dim laFecha As Date = fecha
            Dim fechaFinal As New Date(laFecha.Year, laFecha.Month, laFecha.Day, hora, minuto, 0)
            Return fechaFinal '.AddHours(-6)
        End Function
    End Class

End Namespace
