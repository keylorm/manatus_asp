Imports Orbelink.Helpers.Mailer
Imports System.Globalization
Imports Orbelink.DBHandler
Imports Orbelink.Control.Security
Imports Orbelink.Entity.Security

Partial Class pagError500
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Me.IsPostBack Then
            enviarEmailDefaultSoporteTecnico()
        End If
    End Sub

    Sub enviarEmailDefaultSoporteTecnico()
        Dim ultimoLogError As String = Orbelink.Helpers.Logger.LastRutinaHistorial(HttpContext.Current)
        Try
            Dim securityHandler As New SecurityHandler(Configuration.Config_DefaultConnectionString)
            Dim usuario As Usuario = securityHandler.ConsultarUsuario(securityHandler.Usuario)
            If Orbelink.Helpers.Mailer.SendErrorMail(usuario.Email.Value, usuario.UserName.Value, "lalonso@orbelink.com", "Soporte Orbelink", "", "", Configuration.Config_SiteName, Date.UtcNow, Configuration.Config_WebsiteRoot, New String() {ultimoLogError}) Then

            End If
        Catch ex As Exception
            If Orbelink.Helpers.Mailer.SendErrorMail(Configuration.Config_DefaultAdminEmail, Configuration.Config_DefaultAdminEmail, "soporte@orbelink.com", "Soporte Orbelink", "", "", Configuration.Config_SiteName, Date.UtcNow, Configuration.Config_WebsiteRoot, New String() {ultimoLogError}) Then

            End If
        End Try
    End Sub

    Protected Sub lnkbtn_enviar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkbtn_enviar.Click
        Dim body As String
        body = "<br/><b>Página: </b> Manatus <br/> <b>Mensaje: </b>" & txt_comentario.Text

        Dim exito As Boolean = SendOneMail("soporte@orbelink.com", "Manatus", "soporte@orbelink.com", "Soporte", "Se cayó el sitio de Manatus", body)

        pnl_formulario.Visible = False
        pnl_msg.Visible = False
        pnl_mensaje.Visible = True

        If exito = True Then
            lbl_mensaje2.Text = "Your message has been sent"
            txt_comentario.Text = ""
        Else
            lbl_mensaje2.Text = "Your message has not been sent"
        End If
    End Sub
End Class
