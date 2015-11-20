Imports Orbelink.DBHandler
Imports Orbelink.Entity.Envios
Imports Orbelink.Entity.Entidades

Partial Class _ReporteEnvio
    Inherits PageBaseClass

    Const codigo_pantalla As String = "EV-06"
    Const level As Integer = 2

    'Pagina
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddScripts(level)
        securityHandler.VerifyPantalla(codigo_pantalla, level)

        If Not IsPostBack Then
            If Not Request.Params("Id_Envio") Is Nothing And Not Request.Params("Lote") Is Nothing Then
                'Cargar info del envio
                'Try
                loadEnvio(Request.Params("Id_Envio"))
                selectEntidades_Envio(Request.Params("Id_Envio"), Request.Params("Lote"))
                'Catch ex As Exception
                '    Response.Redirect("Default.aspx")
                'End Try
            Else
                Response.Redirect("Default.aspx")
            End If
        End If
    End Sub

    'Envio
    Protected Sub loadEnvio(ByVal id_Envio As Integer)
        Dim dataSet As Data.DataSet
        Dim Envio As New Envio
        Dim entidad As New Entidad

        Envio.Fields.SelectAll()
        Envio.Id_Envio.Where.EqualCondition(id_Envio)

        entidad.Id_entidad.ToSelect = True
        entidad.NombreDisplay.ToSelect = True
        queryBuilder.Join.EqualCondition(entidad.Id_entidad, Envio.Id_Entidad)

        queryBuilder.From.Add(Envio)
        queryBuilder.From.Add(entidad)
        dataSet = connection.executeSelect(queryBuilder.RelationalSelectQuery)

        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                ObjectBuilder.CreateObject(dataSet.Tables(0), 0, Envio)
                ObjectBuilder.CreateObject(dataSet.Tables(0), 0, entidad)

                lbl_SubjectEnvio.Text = Envio.Subject.Value
                lbl_SubjectEnvio.ToolTip = Envio.Id_Envio.Value
                lbl_Sender.Text = Envio.SenderName.Value
                lbl_ReplyTo.Text = Envio.ReplyTo.Value
                lbl_Entidad.Text = entidad.NombreDisplay.Value
            End If
        End If
    End Sub

    'Entidades_Envio
    Protected Sub selectEntidades_Envio(ByVal id_Envio As Integer, ByVal Lote As Integer)
        Dim dataSet As Data.DataSet
        Dim Correos_Envio As New Correos_Envio

        Correos_Envio.Correo.ToSelect = True
        Correos_Envio.Status.ToSelect = True

        If id_Envio <> 0 Then
            Correos_Envio.Id_Envio.Where.EqualCondition(id_Envio)
        End If

        If Lote >= 0 Then
            Correos_Envio.Lote.Where.EqualCondition(Lote)
        End If

        queryBuilder.From.Add(Correos_Envio)
        dataSet = connection.executeSelect(queryBuilder.RelationalSelectQuery())

        'Nueva tabla para cambiarle de nombre
        Dim tabla As New Data.DataTable
        Dim fila As Data.DataRow
        Dim correo As New Data.DataColumn(Correos_Envio.Correo.Name, Type.GetType("System.String"))
        Dim status As New Data.DataColumn(Correos_Envio.Status.Name, Type.GetType("System.String"))
        tabla.Columns.Add(correo)
        tabla.Columns.Add(status)

        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then

                'Cambiar nombres
                For counter As Integer = 0 To dataSet.Tables(0).Rows.Count - 1
                    fila = tabla.NewRow()
                    fila.Item(Correos_Envio.Correo.Name) = dataSet.Tables(0).Rows(counter).Item(Correos_Envio.Correo.Name)
                    Dim statusActual As Integer = dataSet.Tables(0).Rows(counter).Item(Correos_Envio.Status.Name)

                    Select Case statusActual
                        Case Orbelink.Helpers.Mailer.StatusEnvio.ENVIADO
                            fila.Item(Correos_Envio.Status.Name) = "Enviados"

                        Case Orbelink.Helpers.Mailer.StatusEnvio.ESPERA
                            fila.Item(Correos_Envio.Status.Name) = "En Espera (Sin Enviar)"

                        Case Orbelink.Helpers.Mailer.StatusEnvio.RECHAZADO
                            fila.Item(Correos_Envio.Status.Name) = "Rechazados"

                        Case Orbelink.Helpers.Mailer.StatusEnvio.VISTO
                            fila.Item(Correos_Envio.Status.Name) = "Vistos"

                    End Select

                    tabla.Rows.Add(fila)
                Next

                report_Entidades_Envio.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local
                Dim rep As Microsoft.Reporting.WebForms.LocalReport = report_Entidades_Envio.LocalReport

                rep.ReportPath = "Orbecatalog\Reports\Entidades_Envio_Pie.rdlc"
                rep.DataSources.Clear()

                'Create a report data source for the sales order data
                Dim dssce As New Microsoft.Reporting.WebForms.ReportDataSource()
                dssce.Name = "Entidades_Envio_DS_Entidades_Envio"
                dssce.Value = tabla         'dataSet.Tables(0)
                rep.DataSources.Add(dssce)
            End If
        End If
    End Sub

End Class
