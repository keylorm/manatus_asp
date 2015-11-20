'Revision: 5.0
'07/23/2007

Partial Class _Entidades_Default
    Inherits PageBaseClass

    Const codigo_pantalla As String = "CO-01"
    Const level As Integer = 2

    Dim entidadHandler As Orbelink.Control.Entidades.EntidadHandler

    'Pagina

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddScripts(level)

        securityHandler.VerifyPantalla(codigo_pantalla, level)
        entidadHandler = New Orbelink.Control.Entidades.EntidadHandler(Configuration.Config_DefaultConnectionString)
        If Not IsPostBack Then

            securityHandler.VerifyAveliableScreens(trv_Entidades, level, "CO", False)
            If Not entidadHandler.selectEntidades_DataGrid(dg_Entidades, level) Then
                'lbl_Errores.Text &= "Consulta de Entidades invalida."
                '
            End If
        End If
    End Sub

End Class
