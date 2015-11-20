Imports Orbelink.Orbecatalog6

Partial Class _Default
    Inherits PageBaseClass

    Const codigo_pantalla As String = "SR-01"
    Const level As Integer = 2
    'Pagina

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddScripts(level)

        securityHandler.VerifyPantalla(codigo_pantalla, level)
        If Not IsPostBack Then
            securityHandler.VerifyAveliableScreens(trv_Search, level, "SR", False)
        End If
    End Sub

End Class
