Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler
Imports Orbelink.Entity.Acciones
Imports Orbelink.Crypter
Imports Orbelink.Control.Facturas
Imports Orbelink.Entity.Facturas

Namespace Orbelink.Control.Acciones
    Public Class AccionesHandler

        Dim _connString As String
        Dim crypto As Crypto
        Dim randomizer As Random

        Sub New(ByRef theConnectionString As String)
            _connString = theConnectionString
            crypto = New Crypto(crypto.CryptoProvider.DES)
            crypto.IV = "accionescontrolorbelinkiv"
            crypto.Key = "acciones"
            randomizer = New Random(Date.UtcNow.Millisecond)
        End Sub

        'Enums
        Public Enum EstadosAcciones As Integer
            SinActivar
            Activada
            Consumida
        End Enum

        Public Enum ResultadoAccion As Integer
            NoExisteAccion
            YaActivadaAntes
            ActivadaExitosamente
            YaCaducada
            ErrorGeneral
            ErrorFactura
            AplicadaEnFactura
            NoActivada
            NoDueno
        End Enum

        'Accion
        Public Function ConsultarAccion(ByVal id_Accion As Integer) As Accion
            Dim Accion As New Accion
            Accion.Fields.SelectAll()
            Accion.Id_Accion.Where.EqualCondition(id_Accion)
            Dim connection As New SQLServer(_connString)
            Dim query As New QueryBuilder
            Dim resultTable As Data.DataTable = connection.executeSelect_DT(query.SelectQuery(Accion))
            If resultTable.Rows.Count > 0 Then
                Return ObjectBuilder.CreateDBTable_BySelectIndex(resultTable.Rows(0), Accion)
            End If
            Return Nothing
        End Function

        Public Function AplicarAccionEnFactura(ByVal id_Accion As Integer, ByVal id_Factura As Integer, ByVal cliente As Integer) As ResultadoAccion
            'revisar que puede aplicarse una accion pero no consumirse, ademas si le sobra algo generar una nueva accion
            Dim laAccion As Accion = ConsultarAccion(id_Accion)
            Dim elresultado As ResultadoAccion = ResultadoAccion.ErrorGeneral

            If laAccion IsNot Nothing Then
                If laAccion.Cliente.Value = cliente Then
                    If laAccion.Estado.Value = EstadosAcciones.Activada Then
                        Dim elValor As Double = laAccion.Valor.Value * -1
                        elresultado = AgregaDetalleFactura_FromAccion(id_Factura, id_Accion, elValor, 0, 1, 0)
                    Else
                        elresultado = ResultadoAccion.NoActivada
                    End If
                Else
                    elresultado = ResultadoAccion.NoDueno
                End If
            Else
                elresultado = ResultadoAccion.NoExisteAccion
            End If
            Return elresultado
        End Function

        Public Sub FacturaConsumida_RevisarAcciones(ByVal id_Factura As Integer)
            'ojo()
            'Busca si esta accion fue generada por una invitacion
            'Dim accion As New Accion
            Dim detFactAccion As New DetalleFactura_Accion

            Dim query As New QueryBuilder()

            detFactAccion.Id_Factura.Where.EqualCondition(id_Factura)
            detFactAccion.Detalle.ToSelect = True
            detFactAccion.Id_Factura.ToSelect = True
            detFactAccion.Id_Accion.ToSelect = True

            'query.Join.EqualCondition(detFactAccion.Id_Accion, accion.Id_Accion)

            'query.From.Add(accion)
            query.From.Add(detFactAccion)

            Dim connection As New SQLServer(_connString)
            Dim resultTable As Data.DataTable = connection.executeSelect_DT(query.RelationalSelectQuery)
            If resultTable.Rows.Count > 0 Then

                Dim controlReferidos As New Orbelink.Control.Referidos.ReferidosHandler(_connString)

                For counter As Integer = 0 To resultTable.Rows.Count - 1
                    'Dim act_accion As Accion = ObjectBuilder.CreateDBTable_BySelectIndex(resultTable.Rows(0), accion)
                    Dim act_detFactAccion As DetalleFactura_Accion = ObjectBuilder.CreateDBTable_BySelectIndex(resultTable.Rows(0), detFactAccion)

                    controlReferidos.AccionConsumida_VerificarSiGeneradaPorInvitacion(act_detFactAccion.Id_Accion.Value, act_detFactAccion.Id_Factura.Value, act_detFactAccion.Detalle.Value)
                Next
            End If
        End Sub

        Public Function ActivarAccion(ByVal id_Accion As Integer, ByVal cliente As Integer) As ResultadoAccion
            Dim laAccion As Accion = ConsultarAccion(id_Accion)
            Dim elResultado As ResultadoAccion = ResultadoAccion.ErrorGeneral

            If laAccion IsNot Nothing Then
                If laAccion.FechaActivada.IsValid Then
                    elResultado = ResultadoAccion.YaActivadaAntes
                End If

                If laAccion.FechaCaducidad.IsValid Then
                    If laAccion.FechaCaducidad.ValueUtc <= Date.UtcNow Then
                        elResultado = ResultadoAccion.YaCaducada
                    End If
                End If

                If elResultado = ResultadoAccion.ErrorGeneral Then
                    laAccion.Cliente.Value = cliente
                    laAccion.Estado.Value = EstadosAcciones.Activada
                    laAccion.FechaActivada.ValueUtc = Date.UtcNow
                    laAccion.Id_Accion.Where.EqualCondition(id_Accion)
                    If updateAccion(laAccion) Then
                        elResultado = ResultadoAccion.ActivadaExitosamente
                    End If
                End If
            Else
                elResultado = ResultadoAccion.NoExisteAccion
            End If
            Return elResultado
        End Function

        Public Function GenerarLoteAcciones(ByVal id_MachoteAccion As Integer, ByVal cantidadGenerar As Integer, ByVal usuarioQueGenera As Integer) As Boolean
            Dim controladoraMachotes As New MachoteAccionesHandler(_connString)
            Dim elMachote As MachoteAccion = controladoraMachotes.ConsultarMachoteAccion(id_MachoteAccion)

            Dim resultado As Boolean = False
            Dim fechaCreada As Date = Date.UtcNow

            If elMachote IsNot Nothing Then
                resultado = True
                For counter As Integer = 0 To cantidadGenerar - 1
                    Dim accionGenerada As Integer = GenerarAccion(elMachote, usuarioQueGenera, fechaCreada, counter)
                    If accionGenerada <= 0 Then
                        resultado = False
                        Exit For
                    End If
                Next
            End If
            Return resultado
        End Function

        Public Function GenerarAccion(ByVal id_MachoteAccion As Integer, ByVal usuarioQueGenera As Integer) As Integer
            Dim controladoraMachotes As New MachoteAccionesHandler(_connString)
            Dim elMachote As MachoteAccion = controladoraMachotes.ConsultarMachoteAccion(id_MachoteAccion)
            Return GenerarAccion(elMachote, usuarioQueGenera, Date.UtcNow, 0)
        End Function

        Public Function EliminarAccion(ByVal id_Accion As Integer) As Boolean
            Return deleteAccion(id_Accion)
        End Function

        Private Function GenerarAccion(ByVal elMachote As MachoteAccion, ByVal usuarioQueGenera As Integer, ByVal fechaCreada As Date, ByVal counter As Integer) As Integer
            'Dim resultado As Boolean = False

            Dim accionActual As New Accion
            accionActual.Cliente.SetToNull()
            accionActual.Estado.Value = EstadosAcciones.SinActivar
            If elMachote.FechaExpiracion.IsValid Then
                accionActual.FechaCaducidad.ValueUtc = elMachote.FechaExpiracion.ValueUtc
            Else
                accionActual.FechaCaducidad.ValueUtc = fechaCreada.AddDays(elMachote.DiasValidez.Value)
            End If
            accionActual.FechaCreada.ValueUtc = fechaCreada
            accionActual.GeneradoPor.Value = usuarioQueGenera
            accionActual.Id_MachoteAccion.Value = elMachote.Id_MachoteAccion.Value
            accionActual.Id_Moneda.Value = elMachote.Moneda.Value
            accionActual.Valor.Value = elMachote.Valor.Value

            Dim codigo As String = GenerarCodigoAccion(elMachote.PrefijoCodigo.Value, accionActual.FechaCaducidad.ValueUtc, counter)
            If codigo IsNot Nothing Then
                accionActual.CodigoAccion.Value = codigo
            End If
            accionActual.CodigoValidacion.Value = randomizer.Next(1, 9999).ToString("####")

            If insertAccion(accionActual) Then
                Dim connection As New SQLServer(_connString)
                Return connection.lastKey(accionActual.TableName, accionActual.Id_Accion.Name)
            End If
            Return 0
        End Function

        Private Function insertAccion(ByVal laAccion As Accion) As Boolean
            Dim connection As New SQLServer(_connString)
            Dim query As New QueryBuilder
            Try
                If Not laAccion.FechaCreada.IsValid Then
                    laAccion.FechaCreada.ValueUtc = Date.UtcNow
                End If
                connection.executeInsert(query.InsertQuery(laAccion))
                Return True
            Catch exc As Exception
                Return False
            End Try
        End Function

        Private Function updateAccion(ByVal laAccion As Accion) As Boolean
            Dim connection As New SQLServer(_connString)
            Dim query As New QueryBuilder
            Try
                connection.executeUpdate(query.UpdateQuery(laAccion))
                Return True
            Catch exc As Exception
                Return False
            End Try
        End Function

        Private Function deleteAccion(ByVal id_Accion As Integer) As Boolean
            Dim connection As New SQLServer(_connString)
            Dim query As New QueryBuilder
            Try
                Dim laAccion As New Accion
                laAccion.Id_Accion.Where.EqualCondition(id_Accion)
                connection.executeDelete(query.DeleteQuery(laAccion))
                Return True
            Catch exc As Exception
                Return False
            End Try
        End Function

        'Acciones consultas
        Public Function ConsultarAcciones_ByCliente(ByVal cliente As Integer) As Data.DataTable
            Dim theAccion As New Accion
            theAccion.Cliente.Where.EqualCondition(cliente)
            theAccion.Fields.SelectAll()
            Dim query As New QueryBuilder()
            Return QueryAcciones_DoQuery(query.SelectQuery(theAccion), theAccion)
        End Function

        Private Function QueryAcciones_DoQuery(ByVal theQuery As String, ByVal theAccion As Accion) As Data.DataTable
            Dim connection As New SQLServer(_connString)
            Dim datos As Data.DataTable = connection.executeSelect_DT(theQuery)

            If datos.Rows.Count > 0 Then
                datos.Columns(theAccion.CodigoAccion.SelectIndex).ColumnName = Resources.Acciones_Resources.Accion_Codigo
                datos.Columns(theAccion.Valor.SelectIndex).ColumnName = Resources.Acciones_Resources.Accion_Valor
                datos.Columns(theAccion.FechaActivada.SelectIndex).ColumnName = Resources.Acciones_Resources.Accion_FechaActivada
                datos.Columns(theAccion.FechaCaducidad.SelectIndex).ColumnName = Resources.Acciones_Resources.Accion_FechaCaducidad
                datos.Columns(theAccion.FechaCreada.SelectIndex).ColumnName = Resources.Acciones_Resources.Accion_FechaCreada
                datos.Columns(theAccion.Estado.SelectIndex).ColumnMapping = Data.MappingType.Hidden

                datos.Columns(theAccion.Id_Accion.SelectIndex).ColumnMapping = Data.MappingType.Hidden
                datos.Columns(theAccion.Id_MachoteAccion.SelectIndex).ColumnMapping = Data.MappingType.Hidden
                datos.Columns(theAccion.Id_Moneda.SelectIndex).ColumnMapping = Data.MappingType.Hidden
                datos.Columns(theAccion.GeneradoPor.SelectIndex).ColumnMapping = Data.MappingType.Hidden

                datos.Columns.Add(Resources.Acciones_Resources.Accion_Estado)
                Dim ultimoIndice As Integer = datos.Columns.Count - 1

                For counter As Integer = 0 To datos.Rows.Count - 1
                    Dim theRow As Data.DataRow = datos.Rows(counter)
                    Dim elEstado As EstadosAcciones = theRow(theAccion.Estado.SelectIndex)
                    theRow(ultimoIndice) = Resources.Acciones_Resources.ResourceManager.GetString(elEstado.ToString)
                Next

            End If

            Return datos
        End Function

        Public Function BuscarIdAccion_ByCodigo(ByVal elCodigo As String, ByVal elCodigoValidacion As String) As Integer
            Dim Accion As New Accion
            Accion.Id_Accion.ToSelect = True
            Accion.CodigoValidacion.toselect = True
            Accion.CodigoAccion.Where.EqualCondition(elCodigo)
            Dim connection As New SQLServer(_connString)
            Dim query As New QueryBuilder
            Dim resultTable As Data.DataTable = connection.executeSelect_DT(query.SelectQuery(Accion))
            If resultTable.Rows.Count > 0 Then
                Accion = ObjectBuilder.CreateDBTable_BySelectIndex(resultTable.Rows(0), Accion)
                If elCodigoValidacion = Accion.CodigoValidacion.value Then
                    Return Accion.Id_Accion.Value
                End If
            End If
            Return 0
        End Function

        Public Function ExisteAccionCodigo(ByVal elCodigo As String) As Boolean
            Dim Accion As New Accion
            Accion.Id_Accion.ToSelect = True
            Accion.CodigoAccion.Where.EqualCondition(elCodigo)
            Dim connection As New SQLServer(_connString)
            Dim query As New QueryBuilder
            Dim resultTable As Data.DataTable = connection.executeSelect_DT(query.SelectQuery(Accion))
            If resultTable.Rows.Count > 0 Then
                Return True
            End If
            Return False
        End Function

        'Factura
        Private Function AgregaDetalleFactura_FromAccion(ByVal id_Factura As Integer, ByVal Id_Accion As Integer, ByVal montoItemUnitario As Double, ByVal montoItemUnitarioExtra As Double, ByVal cantidad As Integer, ByVal PorcentajeDescuento As Double) As ResultadoAccion
            Dim connection As New SQLServer(_connString)
            Dim query As New QueryBuilder
            Dim elResultado As ResultadoAccion = ResultadoAccion.ErrorGeneral

            Dim accion As New Accion
            accion.Fields.SelectAll()
            accion.Id_Accion.Where.EqualCondition(Id_Accion)

            Dim resultTable As Data.DataTable = connection.executeSelect_DT(query.SelectQuery(accion))

            If resultTable.Rows.Count > 0 Then
                accion = ObjectBuilder.CreateDBTable_BySelectIndex(resultTable.Rows(0), accion)
                Dim controladora As New HandlerDetalleFactura_Accion()
                Dim control As New FacturasHandler(Configuration.Config_DefaultConnectionString)
                Dim resultadoFactura As FacturasHandler.Resultado_Code = control.AgregarDetalleFactura(controladora, id_Factura, accion, cantidad, montoItemUnitario, 0, PorcentajeDescuento)

                'Revisa resultado de la factura
                Select Case resultadoFactura
                    Case FacturasHandler.Resultado_Code.DuenoYaEnFactura
                        elResultado = ResultadoAccion.AplicadaEnFactura

                    Case FacturasHandler.Resultado_Code.OK
                        elResultado = ResultadoAccion.AplicadaEnFactura

                    Case Else
                        elResultado = ResultadoAccion.ErrorFactura
                End Select

            End If
            Return elResultado
        End Function

        'Utils
        Private Function GenerarCodigoAccion(ByVal PrefijoCodigo As String, ByVal FechaExpiracion As Date, ByVal counter As Integer) As String
            Dim continuar As Boolean = True
            Dim temp As String = Nothing
            While continuar
                'temp = crypto.CifrarCadena(PrefijoCodigo & "_" & FechaExpiracion.ToFileTime & "_" & counter & "_" & randomizer.Next(1000))
                temp = PrefijoCodigo & "_" & FechaExpiracion.ToFileTime & "_" & counter & "_" & randomizer.Next(1000)
                If Not ExisteAccionCodigo(temp) Then
                    continuar = False
                Else
                    temp = Nothing
                End If
            End While
            Return temp
        End Function
    End Class
End Namespace