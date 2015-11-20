Imports Microsoft.VisualBasic
Imports Orbelink.Entity.OrbeEvents
Imports Orbelink.Entity.Entidades

Namespace Orbelink.Control.OrbeEventos
    Public Class OrbeEventsHandler

        'Instancias de clases
        Protected connection As Orbelink.DBHandler.SQLServer
        Protected queryBuilder As Orbelink.DBHandler.QueryBuilder

        Sub New(ByVal theConnection As Orbelink.DBHandler.SQLServer)
            connection = theConnection
            queryBuilder = New Orbelink.DBHandler.QueryBuilder()
        End Sub

        ''' <summary>
        ''' Revisa todos los eventos y sucesos nuevos, y para cada uno realiza la accion a tomar 
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub revisarSucesos()
            'Dim sucesosThread As New System.Threading.Thread(AddressOf selectSucesos)
            'sucesosThread.Start()
            selectSucesos()
        End Sub

        ''' <summary>
        ''' Funcion que agrega sucesos al evento correspondiente (de haber) segun los parametros.
        ''' </summary>
        ''' <param name="id_Entidad">Entidad que activo el evento</param>
        ''' <param name="codigo_Pantalla">La pantalla a la que pertenece el evento</param>
        ''' <param name="Numero_Evento">El numero de evento segun la pantalla</param>
        ''' <param name="SubReferencia">Sub referencia que se concatena con la referencia del evento</param>
        ''' <param name="Comment">Un comentario adicional para mostrar al notificar</param>
        ''' <param name="EspecificacionLlave">Una llave especifica para comparar con la del evento.</param>
        ''' <param name="Valor">El valor de la llave</param>
        ''' <returns>Valor booleano si fue insertado</returns>
        ''' <remarks></remarks>
        Public Function AgregarSuceso(ByVal id_Entidad As Integer, ByVal codigo_Pantalla As String, ByVal Numero_Evento As Integer, Optional ByVal SubReferencia As String = "", Optional ByVal Comment As String = "", _
            Optional ByVal EspecificacionLlave As String = "", Optional ByVal Valor As String = "") As Boolean
            Dim eventosValidos As ArrayList = Me.selectOrbeEvento(codigo_Pantalla, Numero_Evento, EspecificacionLlave, Valor)
            Dim insertado As Boolean = False

            If eventosValidos IsNot Nothing Then
                insertado = True
                For Each id_EventoValido As Integer In eventosValidos
                    insertado = insertSuceso(id_EventoValido, id_Entidad, SubReferencia, Comment)
                Next
            End If

            Return insertado
        End Function

        'Sucesos
        Private Sub selectSucesos()
            Dim dataset As Data.DataSet
            Dim OrbeEvento As New OrbeEvento
            Dim Suceso As New Suceso
            Dim entidad As New Entidad

            OrbeEvento.Fields.SelectAll()
            queryBuilder.Join.EqualCondition(OrbeEvento.Id_OrbeEvento, Suceso.Id_OrbeEvento)

            Suceso.Fecha.ToSelect = True
            Suceso.SubReferencia.ToSelect = True
            Suceso.TriggeredBy.ToSelect = True
            Suceso.Revisado.Where.EqualCondition("0")
            Suceso.Comment.ToSelect = True

            entidad.NombreDisplay.ToSelect = True
            queryBuilder.Join.EqualCondition(entidad.Id_entidad, Suceso.TriggeredBy)

            queryBuilder.Orderby.Add(Suceso.Fecha)
            queryBuilder.From.Add(Suceso)
            queryBuilder.From.Add(OrbeEvento)
            queryBuilder.From.Add(entidad)
            dataset = connection.executeSelect(queryBuilder.RelationalSelectQuery)

            If dataset.Tables.Count > 0 Then
                If dataset.Tables(0).Rows.Count > 0 Then

                    'Revisa todos los sucesos que no han sido notificados
                    Dim result_OrbeEvento As ArrayList = Orbelink.DBHandler.ObjectBuilder.TransformDataTable(dataset.Tables(0), OrbeEvento)
                    Dim result_Entidad As ArrayList = Orbelink.DBHandler.ObjectBuilder.TransformDataTable(dataset.Tables(0), entidad)
                    Dim result_Suceso As ArrayList = Orbelink.DBHandler.ObjectBuilder.TransformDataTable(dataset.Tables(0), Suceso)

                    For counter As Integer = 0 To dataset.Tables(0).Rows.Count - 1
                        Dim act_OrbeEvento As OrbeEvento = result_OrbeEvento(counter)
                        Dim act_Entidad As Entidad = result_Entidad(counter)
                        Dim act_Suceso As Suceso = result_Suceso(counter)

                        'Hace notificaciones
                        Dim params As New NotificarParams
                        params.TriggeredBy_Nombre = act_Entidad.NombreDisplay.Value
                        params.TriggeredBy_ID = act_Suceso.TriggeredBy.Value
                        params.FechaSuceso = act_Suceso.Fecha.ValueLocalized
                        params.Referencia = act_OrbeEvento.Referencia.Value & act_Suceso.SubReferencia.Value
                        params.id_OrbeEvento = act_OrbeEvento.Id_OrbeEvento.Value
                        params.NombreOrbeEvento = act_OrbeEvento.Nombre.Value
                        params.Comment = act_Suceso.Comment.Value

                        Dim notificacionesThread As New System.Threading.Thread(AddressOf notificarSuscripciones)
                        notificacionesThread.Start(params)

                        'Actualiza suceso como revisado.
                        updateSuceso_Revisado(act_OrbeEvento.Id_OrbeEvento.Value, act_Suceso.Fecha.ValueUtc)
                    Next
                End If
            End If
        End Sub

        Private Function insertSuceso(ByVal id_OrbeEvento As Integer, ByVal id_entidad As Integer, ByVal SubReferencia As String, ByVal Comment As String) As Boolean
            Dim Suceso As Suceso
            Try
                If id_entidad = 0 Then
                    id_entidad = Configuration.Config_DefaultIdEntidad
                End If
                Suceso = New Suceso(id_OrbeEvento, Date.UtcNow, id_entidad, SubReferencia, "0", Comment)
                connection.executeInsert(queryBuilder.InsertQuery(Suceso))
                Return True
            Catch exc As Exception
                Return False
            End Try
        End Function

        Protected Function updateSuceso_Revisado(ByVal id_OrbeEvento As Integer, ByVal fechaSuceso As String) As Boolean
            Try
                Dim Suceso As New Suceso
                Suceso.Id_OrbeEvento.Where.EqualCondition(id_OrbeEvento)
                Suceso.Fecha.Where.EqualCondition(fechaSuceso)
                Suceso.Revisado.Value = "1"

                connection.executeUpdate(queryBuilder.UpdateQuery(Suceso))
                Return True
            Catch exc As Exception
                Return False
            End Try
        End Function

        'OrbeEventos
        Private Function selectOrbeEvento(ByVal codigo_Pantalla As String, ByVal Numero_Evento As Integer, ByVal EspecificacionLlave As String, ByVal Valor As String) As ArrayList
            Dim dataset As Data.DataSet
            Dim OrbeEvento As New OrbeEvento
            Dim eventosValidos As ArrayList = Nothing

            OrbeEvento.Id_OrbeEvento.ToSelect = True
            OrbeEvento.Codigo_Pantalla.Where.EqualCondition(codigo_Pantalla)
            OrbeEvento.Numero_Evento.Where.EqualCondition(Numero_Evento)

            If EspecificacionLlave.Length > 0 Then
                OrbeEvento.EspecificacionLlave.Where.EqualCondition(EspecificacionLlave)
            End If

            dataset = connection.executeSelect(queryBuilder.SelectQuery(OrbeEvento))

            If dataset.Tables.Count > 0 Then
                If dataset.Tables(0).Rows.Count > 0 Then
                    eventosValidos = New ArrayList

                    'Compara los resultado con el valor proporcionado.
                    Dim results_OrbeEvento As ArrayList = Orbelink.DBHandler.ObjectBuilder.TransformDataTable(dataset.Tables(0), OrbeEvento)
                    For counter As Integer = 0 To dataset.Tables(0).Rows.Count - 1
                        Dim act_OrbeEvento As OrbeEvento = results_OrbeEvento(counter)

                        Dim regexp As New System.Text.RegularExpressions.Regex(act_OrbeEvento.Expresion.Value)
                        If regexp.IsMatch(Valor) Then
                            eventosValidos.Add(act_OrbeEvento.Id_OrbeEvento.Value)
                        End If
                    Next
                End If
            End If
            Return eventosValidos
        End Function

        'Notificaciones
        Structure NotificarParams
            Dim TriggeredBy_Nombre As String
            Dim FechaSuceso As String
            Dim Referencia As String
            Dim id_OrbeEvento As Integer
            Dim TriggeredBy_ID As Integer
            Dim NombreOrbeEvento As String
            Dim Comment As String
        End Structure

        Structure SuscripcionParams
            Dim Entidad_Nombre As String
            Dim Entidad_Id As Integer
            Dim Email As String
            Dim MobilePhone As String
        End Structure

        Private Sub notificarSuscripciones(ByVal theParams As Object)
            Dim params As NotificarParams = CType(theParams, NotificarParams)
            Dim dataset As Data.DataSet
            Dim Suscripcion As New Suscripcion
            Dim entidad As New Entidad

            Suscripcion.Fields.SelectAll()
            Suscripcion.Id_OrbeEvento.Where.EqualCondition(params.id_OrbeEvento)
            Suscripcion.Periocidad.Where.EqualCondition(theParams.thePeriocidad)

            entidad.NombreDisplay.ToSelect = True
            entidad.Email.ToSelect = True
            entidad.Email.AsName = "entidad_email"
            'entidad.MobilePhone.toselect = True
            queryBuilder.Join.EqualCondition(entidad.Id_entidad, Suscripcion.Id_Entidad)

            queryBuilder.From.Add(Suscripcion)
            queryBuilder.From.Add(entidad)
            Dim consulta As String = queryBuilder.RelationalSelectQuery
            dataset = connection.executeSelect(consulta)

            If dataset.Tables.Count > 0 Then
                If dataset.Tables(0).Rows.Count > 0 Then

                    'Revisa todos los sucesos que no han sido notificados
                    Dim result_Suscripcion As ArrayList = Orbelink.DBHandler.ObjectBuilder.TransformDataTable(dataset.Tables(0), Suscripcion)
                    Dim result_Entidad As ArrayList = Orbelink.DBHandler.ObjectBuilder.TransformDataTable(dataset.Tables(0), entidad)

                    For counter As Integer = 0 To dataset.Tables(0).Rows.Count - 1
                        Dim act_Suscripcion As Suscripcion = result_Suscripcion(counter)
                        Dim act_Entidad As Entidad = result_Entidad(counter)

                        Dim suscripcionParams As New SuscripcionParams
                        suscripcionParams.Entidad_Nombre = act_Entidad.NombreDisplay.Value
                        suscripcionParams.Entidad_Id = act_Suscripcion.Id_Entidad.Value

                        suscripcionParams.Email = act_Entidad.Email.Value
                        'suscripcionParams.MobilePhone = act_Entidad.mobilePhone.vale
                        suscripcionParams.MobilePhone = ""

                        'Hace notificaciones
                        If act_Suscripcion.Email.Value = "1" Then
                            notificarEmail(params, suscripcionParams)
                        End If

                        If act_Suscripcion.Message.Value = "1" Then
                            notificarMessage(params, suscripcionParams)
                        End If

                        If act_Suscripcion.SMS.Value = "1" Then
                            notificarSMS(params, suscripcionParams)
                        End If
                    Next
                End If
            End If
        End Sub

        Private Function notificarEmail(ByVal notificar As NotificarParams, ByVal suscripcion As SuscripcionParams) As Boolean
            Dim notificado As Boolean = False
            Dim theBody As New StringBuilder

            Dim stringParams As String() = {suscripcion.Entidad_Nombre, notificar.NombreOrbeEvento, notificar.FechaSuceso, notificar.Comment, notificar.Referencia}
            theBody.AppendFormat("Estimado {0}:<br><br> Se le notifica que ""{1}"" ha ocurrido el {2}. <br>{3}<br> Por favor visite {4} para mayor informacion.", stringParams)

            notificado = Orbelink.Helpers.Mailer.SendOneMail(Nothing, Nothing, suscripcion.Email, suscripcion.Entidad_Nombre, notificar.NombreOrbeEvento, theBody.ToString)
            Return notificado
        End Function

        Private Function notificarSMS(ByVal notificar As NotificarParams, ByVal suscripcion As SuscripcionParams) As Boolean
            Dim notificado As Boolean = False

            Return notificado
        End Function

        Private Function notificarMessage(ByVal notificar As NotificarParams, ByVal suscripcion As SuscripcionParams) As Boolean
            Dim notificado As Boolean = False

            Return notificado
        End Function

    End Class

End Namespace