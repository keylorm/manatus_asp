Imports Orbelink.DBHandler
Imports Orbelink.Entity.Proyectos
Imports Orbelink.Entity.Entidades

Public Class CronJobs
    Inherits PageBaseClass

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        selectRecordatario()
    End Sub

    'Recordatorio
    Protected Sub selectRecordatario()
        'Dim dataset As Data.DataSet
        'Dim Recordatorio As New Recordatorio
        'Dim Entidad As New Entidad
        'Dim Tareas_Proyecto As New Tareas_Proyecto
        'Dim Tareas As New Tareas

        'Recordatorio.Proximo.ToSelect = True
        'Recordatorio.Intervalo.ToSelect = True
        'Recordatorio.Tiempo.ToSelect = True
        'Recordatorio.Proximo.Where.LessThanOrEqualCondition(Date.UtcNow)

        'queryBuilder.Join.EqualCondition(Entidad.Id_entidad, Recordatorio.Id_Entidad)
        'Entidad.Email.ToSelect = True
        'Entidad.NombreDisplay.ToSelect = True

        'Tareas_Proyecto.Id_TareaProyecto.ToSelect = True
        'queryBuilder.Join.EqualCondition(Tareas_Proyecto.Id_TareaProyecto, Recordatorio.Id_TareaProyecto)

        'queryBuilder.Join.EqualCondition(Tareas.Id_Tarea, Tareas_Proyecto.Id_Tarea)
        'Tareas.Nombre.ToSelect = True

        'queryBuilder.OrderBy.Add(Recordatorio.Proximo, False)
        'queryBuilder.From.Add(Recordatorio)
        'queryBuilder.From.Add(Entidad)
        'queryBuilder.From.Add(Tareas_Proyecto)
        'queryBuilder.From.Add(Tareas)
        'dataset = connection.executeSelect(queryBuilder.RelationalSelectQuery)

        'If dataset.Tables.Count > 0 Then
        '    If dataset.Tables(0).Rows.Count > 0 Then

        '        Dim counter As Integer
        '        Dim result_Recordatorio As ArrayList = ObjectBuilder.TransformDataTable(dataset.Tables(0), Recordatorio)
        '        Dim result_Entidad As ArrayList = ObjectBuilder.TransformDataTable(dataset.Tables(0), Entidad)
        '        Dim result_Tareas_Proyecto As ArrayList = ObjectBuilder.TransformDataTable(dataset.Tables(0), Tareas_Proyecto)
        '        Dim result_Tareas As ArrayList = ObjectBuilder.TransformDataTable(dataset.Tables(0), Tareas)

        '        For counter = 0 To result_Recordatorio.Count - 1
        '            Dim act_Recordatario As Recordatorio = result_Recordatorio(counter)
        '            Dim act_Entidad As Entidad = result_Entidad(counter)
        '            Dim act_Tareas_Proyecto As Tareas_Proyecto = result_Tareas_Proyecto(counter)
        '            Dim act_Tareas As Tareas = result_Tareas(counter)

        '            If act_Entidad.Email.IsValid Then
        '                Dim subject As String = "Recordatorio de Tarea"
        '                Dim body As String = "Usted, tiene la tarea de """ & act_Tareas.Nombre.Value & """ pendiente."
        '                body &= "<br>Por favor visite " & Configuration.Config_WebsiteRoot & "orbecatalog/Proyectos/Seguir_Tarea.aspx?id_TareaProyecto=" & act_Tareas_Proyecto.Id_TareaProyecto.Value & " para realizar su trabajo."
        '                Orbelink.Helpers.Mailer.SendOneMail(act_Entidad.Email.Value, "Recordatorio de Tarea Pendiente", act_Entidad.Email.Value, act_Entidad.NombreDisplay.Value, subject, body)
        '            End If

        '            Try
        '                Dim proximo As Date = DateAdd(CType(act_Recordatario.Intervalo.Value, DateInterval), CType(act_Recordatario.Tiempo.Value, Double), Date.UtcNow)
        '                act_Recordatario.Proximo.ValueUtc = proximo
        '                act_Recordatario.Id_Recordatorio.Where.EqualCondition(act_Recordatario.Id_Recordatorio.Value)
        '                connection.executeUpdate(queryBuilder.UpdateQuery(act_Recordatario))
        '            Catch ex As Exception
        '                'No se pudo actualizar
        '            End Try
        '        Next
        '    End If
        'End If
    End Sub

End Class