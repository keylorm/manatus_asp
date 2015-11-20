Imports Orbelink.DBHandler
Partial Class _DefaultPublicaciones
    Inherits PageBaseClass

    Const codigo_pantalla As String = "PU-01"
    Const level As Integer = 2

    'Pagina
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddScripts(level)
        securityHandler.VerifyPantalla(codigo_pantalla, level, True)
        If Not IsPostBack Then
            securityHandler.VerifyAveliableScreens(trv_Publicaciones, level, "PU", False)

            Dim publicacionesHandler As New Orbelink.Control.Publicaciones.ControladoraPublicaciones(Configuration.Config_DefaultConnectionString)
            If Not publicacionesHandler.selectPublicaciones_DataList(dtl_Publicaciones, level, "orbecatalog/Publicacion/Publicacion.aspx", securityHandler.TipoEntidad, securityHandler.Entidad, 0, 4, False, Nothing, True) Then
                'pnl_Publicaciones.Visible = False
                'lbl_Errores.Text &= " Consulta de publicaciones invalida."
            End If
        End If
    End Sub

End Class
