Imports Orbelink.Orbecatalog6

'Revision: 2.3
'01/19/2007

Partial Class _DefaultFacturas
    Inherits PageBaseClass

    Const codigo_pantalla As String = "FC-01"
    Const level As Integer = 2

    'Pagina

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddScripts(level)

        securityHandler.VerifyPantalla(codigo_pantalla, level)
        If Not IsPostBack Then

            securityHandler.VerifyAveliableScreens(trv_Facturas, level, "FC", False)
        End If
    End Sub

End Class
