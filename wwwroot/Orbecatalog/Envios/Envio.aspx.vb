Imports Orbelink.Orbecatalog6
Imports Orbelink.Entity.Envios

Partial Class orbecatalog_OrbeNewsletter
    Inherits PageBaseClass

    Const codigo_pantalla As String = "EV-03"
    Const level As Integer = 2

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddScripts(level)
        Try
            If Not Request.Params("Id_Correos_Envio") Is Nothing Then
                Dim theID As String = Request.Params("Id_Correos_Envio")
                If theID <> Orbelink.Helpers.Mailer.Replace_Correos_Envio Then
                    updateEntidades_Envio(theID)
                End If
            End If
        Catch ex As Exception

        End Try

        Response.Redirect(Configuration.Config_WebsiteRoot & "orbecatalog/images/spacer.gif")
        Response.End()
    End Sub

    'Correos_Envio
    Protected Function updateEntidades_Envio(ByVal Id_Correos_Envio As Integer) As Boolean
        Dim Correos_Envio As New Correos_Envio
        Try
            Correos_Envio.Id_Correos_Envio.Where.EqualCondition(Id_Correos_Envio)
            Correos_Envio.Fecha.ValueUtc = Date.UtcNow
            Correos_Envio.Status.Value = Orbelink.Helpers.Mailer.StatusEnvio.VISTO
            connection.executeUpdate(queryBuilder.UpdateQuery(Correos_Envio))
            Return True
        Catch exc As Exception
            'MyMaster.mostrarMensaje(exc.Message, true)
            '
            Return False
        End Try
    End Function
End Class
