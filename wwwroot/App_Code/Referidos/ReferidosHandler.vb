Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler
Imports Orbelink.Crypter
Imports Orbelink.Entity.Referidos
Imports Orbelink.Entity.Acciones
Imports Orbelink.Entity.Entidades
'Imports Orbelink.Entity.Facturas
Imports Orbelink.Control.Entidades
Imports Orbelink.Control.Acciones

Namespace Orbelink.Control.Referidos
    Public Class ReferidosHandler

        Dim _connString As String
        Dim crypto As Crypto
        Dim randomizer As Random

        Sub New(ByRef theConnectionString As String)
            _connString = theConnectionString
            crypto = New Crypto(crypto.CryptoProvider.DES)
            crypto.IV = "referidosorbelinkivkey"
            crypto.Key = "referido"
            randomizer = New Random(Date.UtcNow.Millisecond)
        End Sub

        Public Enum ResultCodes As Integer
            OK
            ErrorGeneral
            ErrorEnviarCorreo
            ErrorGenerarCodigo
            ErrorBD
            ErrorActualizar
            ValidacionErronea
            NoExiste
        End Enum

        Public Function Invitar(ByVal usuarioInvita As Integer, ByVal nombreInvitado As String, ByVal correoInvitado As String, Optional ByVal machoteAplicar As Integer = 0) As ResultCodes
            Dim resultado As ResultCodes = ReferidosHandler.ResultCodes.ErrorGeneral
            'Genera la accion y sus codigos
            Dim laInvitacion As New Invitacion

            Dim codigo As String = GenerarCodigoInvitacion(usuarioInvita, correoInvitado)
            If codigo IsNot Nothing Then
                Dim entiHandler As New EntidadHandler(_connString)
                Dim invitador As Entidad = entiHandler.ConsultarEntidad(usuarioInvita)

                laInvitacion.CodigoInvitacion.Value = codigo

                laInvitacion.Activo.Value = 1
                laInvitacion.CodigoValidacion.Value = crypto.CifrarCadena(laInvitacion.CodigoInvitacion.Value)
                laInvitacion.CorreoInvitado.Value = correoInvitado
                laInvitacion.FechaInvitacion.ValueUtc = Date.UtcNow
                laInvitacion.NombreInvitado.Value = nombreInvitado
                laInvitacion.UsuarioInvita.Value = usuarioInvita

                If machoteAplicar > 0 Then
                    laInvitacion.Id_MachoteAccion.Value = machoteAplicar
                End If

                'La inserta en la base de datos
                If insertInvitacion(laInvitacion) Then
                    If Orbelink.Helpers.Mailer.SendOneMail(invitador.Email.Value, invitador.NombreDisplay.Value, correoInvitado, nombreInvitado, "Invitacion", codigo) Then
                        resultado = ReferidosHandler.ResultCodes.OK
                    Else
                        resultado = ReferidosHandler.ResultCodes.ErrorEnviarCorreo
                    End If
                Else
                    resultado = ReferidosHandler.ResultCodes.ErrorBD
                End If
            Else
                resultado = ReferidosHandler.ResultCodes.ErrorGenerarCodigo
            End If
            Return resultado
        End Function

        Public Function AceptarInvitacion(ByVal codigoInvitacion As String, ByVal codigoValidacion As String, ByVal usuarioNuevo As Integer) As ResultCodes
            Dim resultado As ResultCodes = ResultCodes.ErrorGeneral
            'Busca la invitacion
            Dim id_Invitacion As Integer = BuscarIdInvitacion_ByCodigo(codigoInvitacion)

            'Valida
            If id_Invitacion > 0 Then
                Dim laInvitacion As Invitacion = ConsultarInvitacion(id_Invitacion)
                If laInvitacion IsNot Nothing Then
                    If laInvitacion.Activo.Value = 1 Then
                        If laInvitacion.CodigoValidacion.Value = codigoValidacion Then
                            'Registra la invitacion aceptada
                            Dim inviAceptada As New InvitacionAceptada
                            inviAceptada.FechaAceptada.ValueUtc = Date.UtcNow
                            inviAceptada.Id_Invitacion.Value = id_Invitacion
                            inviAceptada.UsuarioInvitado.value = usuarioNuevo
                            If Not insertInvitacionAceptada(inviAceptada) Then
                                resultado = ResultCodes.ErrorBD
                            End If

                            'De tener un machote de accion lo aplica
                            If laInvitacion.Id_MachoteAccion.IsValid And Not resultado = ResultCodes.ErrorBD Then
                                Dim controladoraAcciones As New AccionesHandler(_connString)
                                Dim idAccion As Integer = controladoraAcciones.GenerarAccion(laInvitacion.Id_MachoteAccion.Value, usuarioNuevo)
                                controladoraAcciones.ActivarAccion(idAccion, usuarioNuevo)

                                Dim accionInviAceptada As New Accion_InvitacionAceptada
                                accionInviAceptada.Id_Accion.Value = idAccion
                                accionInviAceptada.Id_Invitacion.Value = id_Invitacion
                                If Not insertAccion_InvitacionAceptada(accionInviAceptada) Then
                                    resultado = ResultCodes.ErrorBD
                                    deleteInvitacionAceptada(id_Invitacion)
                                End If
                            End If

                            'Desactiva la invitacion
                            If Not resultado = ResultCodes.ErrorBD Then
                                laInvitacion.Activo.Value = 0
                                If updateInvitacion(laInvitacion, id_Invitacion) Then
                                    resultado = ResultCodes.OK
                                Else
                                    resultado = ResultCodes.ErrorActualizar
                                End If
                            End If
                        Else
                            resultado = ResultCodes.ValidacionErronea
                        End If
                    Else
                        resultado = ResultCodes.ValidacionErronea
                    End If
                Else
                    resultado = ResultCodes.NoExiste
                End If
            Else
                resultado = ResultCodes.NoExiste
            End If
            Return resultado
        End Function

        Public Sub AccionConsumida_VerificarSiGeneradaPorInvitacion(ByVal id_Accion As Integer, ByVal elId_Factura As Integer, ByVal elDetalleFactura As Integer)
            'Busca si esta accion fue generada por una invitacion
            Dim Invitacion As New Invitacion
            Dim accion As New Accion
            Dim accionInviAceptada As New Accion_InvitacionAceptada
            Dim inviAceptada As New InvitacionAceptada
            'Dim detalleFactura As New DetalleFactura_Accion

            Dim query As New QueryBuilder()

            Invitacion.UsuarioInvita.ToSelect = True
            Invitacion.Id_MachoteAccion.ToSelect = True

            accion.Id_Accion.Where.EqualCondition(id_Accion)

            query.Join.EqualCondition(Invitacion.Id_Invitacion, inviAceptada.Id_Invitacion)
            query.Join.EqualCondition(accionInviAceptada.Id_Invitacion, inviAceptada.Id_Invitacion)
            query.Join.EqualCondition(accion.Id_Accion, accionInviAceptada.Id_Invitacion)
            'query.Join.EqualCondition(detalleFactura.Id_Accion, accion.Id_Accion)

            query.From.Add(Invitacion)
            query.From.Add(inviAceptada)
            query.From.Add(accionInviAceptada)
            query.From.Add(accion)
            'query.From.Add(detalleFactura)

            Dim connection As New SQLServer(_connString)
            Dim resultTable As Data.DataTable = connection.executeSelect_DT(query.RelationalSelectQuery)
            If resultTable.Rows.Count > 0 Then
                Invitacion = ObjectBuilder.CreateDBTable_BySelectIndex(resultTable.Rows(0), Invitacion)
                'detalleFactura = ObjectBuilder.CreateDBTable_BySelectIndex(resultTable.Rows(0), detalleFactura)
                'inviAceptada = ObjectBuilder.CreateDBTable_BySelectIndex(resultTable.Rows(0), inviAceptada)
                'accion = ObjectBuilder.CreateDBTable_BySelectIndex(resultTable.Rows(0), accion)

                Dim controladoraAcciones As New AccionesHandler(_connString)
                Dim idAccion As Integer = controladoraAcciones.GenerarAccion(Invitacion.Id_MachoteAccion.Value, Invitacion.UsuarioInvita.Value)
                If idAccion > 0 Then
                    controladoraAcciones.ActivarAccion(idAccion, Invitacion.UsuarioInvita.Value)

                    Dim accionInviConsumida As New Accion_InvitacionConsumida
                    accionInviConsumida.Id_Accion.Value = idAccion
                    accionInviConsumida.Id_Invitacion.Value = Invitacion.Id_Invitacion.Value
                    accionInviConsumida.Detalle.Value = elDetalleFactura
                    accionInviConsumida.Id_Factura.Value = elId_Factura

                    If Not insertAccion_InvitacionConsumida(accionInviConsumida) Then
                        'resultado = ResultCodes.ErrorBD
                        controladoraAcciones.EliminarAccion(idAccion)
                    End If
                End If
            End If

        End Sub

        'Invitacion
        Public Function ConsultarInvitacion(ByVal id_Invitacion As Integer) As Invitacion
            Dim Invitacion As New Invitacion
            Invitacion.Fields.SelectAll()
            Invitacion.Id_Invitacion.Where.EqualCondition(id_Invitacion)
            Dim connection As New SQLServer(_connString)
            Dim query As New QueryBuilder
            Dim resultTable As Data.DataTable = connection.executeSelect_DT(query.SelectQuery(Invitacion))
            If resultTable.Rows.Count > 0 Then
                Return ObjectBuilder.CreateDBTable_BySelectIndex(resultTable.Rows(0), Invitacion)
            End If
            Return Nothing
        End Function

        Private Function insertInvitacion(ByVal laInvitacion As Invitacion) As Boolean
            Dim connection As New SQLServer(_connString)
            Dim query As New QueryBuilder
            Try
                connection.executeInsert(query.InsertQuery(laInvitacion))
                Return True
            Catch exc As Exception
                Return False
            End Try
        End Function

        Private Function updateInvitacion(ByVal laInvitacion As Invitacion, ByVal id_Invitacion As Integer) As Boolean
            Dim connection As New SQLServer(_connString)
            Dim query As New QueryBuilder
            Try
                laInvitacion.Id_Invitacion.Where.EqualCondition(id_Invitacion)
                connection.executeUpdate(query.UpdateQuery(laInvitacion))
                Return True
            Catch exc As Exception
                Return False
            End Try
        End Function

        'InvitacionAceptada
        Private Function insertInvitacionAceptada(ByVal laInvitacionAceptada As InvitacionAceptada) As Boolean
            Dim connection As New SQLServer(_connString)
            Dim query As New QueryBuilder
            Try
                connection.executeInsert(query.InsertQuery(laInvitacionAceptada))
                Return True
            Catch exc As Exception
                Return False
            End Try
        End Function

        Private Function deleteInvitacionAceptada(ByVal id_Invitacion As Integer) As Boolean
            Dim connection As New SQLServer(_connString)
            Dim query As New QueryBuilder
            Try
                Dim laInvitacionAceptada As New InvitacionAceptada
                laInvitacionAceptada.Id_Invitacion.Where.EqualCondition(id_Invitacion)
                connection.executeDelete(query.DeleteQuery(laInvitacionAceptada))
                Return True
            Catch exc As Exception
                Return False
            End Try
        End Function

        'Accion_InvitacionAceptada
        Private Function insertAccion_InvitacionAceptada(ByVal laAccion_InvitacionAceptada As Accion_InvitacionAceptada) As Boolean
            Dim connection As New SQLServer(_connString)
            Dim query As New QueryBuilder
            Try
                connection.executeInsert(query.InsertQuery(laAccion_InvitacionAceptada))
                Return True
            Catch exc As Exception
                Return False
            End Try
        End Function

        'Accion_InvitacionAceptada
        Private Function insertAccion_InvitacionConsumida(ByVal laAccion_InvitacionConsumida As Accion_InvitacionConsumida) As Boolean
            Dim connection As New SQLServer(_connString)
            Dim query As New QueryBuilder
            Try
                connection.executeInsert(query.InsertQuery(laAccion_InvitacionConsumida))
                Return True
            Catch exc As Exception
                Return False
            End Try
        End Function

        'Utils
        Private Function BuscarIdInvitacion_ByCodigo(ByVal elCodigoInvitacion As String) As Integer
            Dim theInvitacion As New Invitacion
            theInvitacion.Id_Invitacion.ToSelect = True
            theInvitacion.CodigoInvitacion.Where.EqualCondition(elCodigoInvitacion)
            Dim connection As New SQLServer(_connString)
            Dim query As New QueryBuilder
            Dim resultTable As Data.DataTable = connection.executeSelect_DT(query.SelectQuery(theInvitacion))
            If resultTable.Rows.Count > 0 Then
                theInvitacion = ObjectBuilder.CreateDBTable_BySelectIndex(resultTable.Rows(0), theInvitacion)
                Return theInvitacion.Id_Invitacion.Value
            End If
            Return 0
        End Function

        Private Function GenerarCodigoInvitacion(ByVal usuarioInvita As Integer, ByVal correoInvitado As String) As String
            Dim continuar As Boolean = True
            Dim temp As String = Nothing
            While continuar
                temp = crypto.CifrarCadena(usuarioInvita & "_" & correoInvitado & "_" & randomizer.Next(1000))
                If BuscarIdInvitacion_ByCodigo(temp) <= 0 Then
                    continuar = False
                Else
                    temp = Nothing
                End If
            End While
            Return temp
        End Function

    End Class
End Namespace
