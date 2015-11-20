Imports Orbelink.Control.Acciones
Imports Orbelink.Control.Facturas

Partial Class Orbecatalog_Acciones_Default
    Inherits PageBaseClass

    Const codigo_pantalla As String = "AC-01"
    Const level As Integer = 2

    'Pagina
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddScripts(level)
        securityHandler.VerifyPantalla(codigo_pantalla, level)

        If Not IsPostBack Then
            
        End If
    End Sub

End Class
