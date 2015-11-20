Imports Orbelink.Orbecatalog6
Imports Orbelink.Entity.Envios

Partial Class _DefaultEnvios
    Inherits PageBaseClass

    Const codigo_pantalla As String = "EV-01"
    Const level As Integer = 2

    'Pagina

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddScripts(level)

        securityHandler.VerifyPantalla(codigo_pantalla, level)
        If Not IsPostBack Then

            securityHandler.VerifyAveliableScreens(trv_Envios, level, "EV", False)
            dg_Envios.Visible = False
        End If
    End Sub

End Class
