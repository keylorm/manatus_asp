Imports Orbelink.DBHandler
Partial Class orbecatalog_Search_Envios
    Inherits PageBaseClass

    Const codigo_pantalla As String = "EV-SR"
    Const level As Integer = 2

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddScripts(level)
        securityHandler.VerifyPantalla(codigo_pantalla, level)
    End Sub


End Class
