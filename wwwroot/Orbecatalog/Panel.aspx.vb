Imports Orbelink.Orbecatalog6

Partial Class _Panel
    Inherits PageBaseClass

    Const codigo_pantalla As String = "OR-02"
    Const level As Integer = 1

    'Pagina

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddScripts(level)



        securityHandler.VerifyPantalla(codigo_pantalla, level)
        If Not IsPostBack Then
            securityHandler.VerifyAveliableScreens(trv_Orbecatalog, level, "OR", False)
            securityHandler.VerifyAveliableScreens(trv_Search, level, "SR", False)

            pnl_Productos.Visible = securityHandler.VerifyAveliableScreens(trv_Productos, level, "PR", True)
            pnl_Entidades.Visible = securityHandler.VerifyAveliableScreens(trv_Entidades, level, "CO", True)
            pnl_Facturas.Visible = securityHandler.VerifyAveliableScreens(trv_Facturas, level, "FC", True)
            pnl_Publicaciones.Visible = securityHandler.VerifyAveliableScreens(trv_Publicaciones, level, "PU", True)
            pnl_Proyectos.Visible = securityHandler.VerifyAveliableScreens(trv_Proyectos, level, "PY", True)
            pnl_reportes.Visible = securityHandler.VerifyAveliableScreens(trv_Reportes, level, "RP", True)
            pnl_Procesos.Visible = securityHandler.VerifyAveliableScreens(trv_Procesos, level, "PC", True)
            pnl_Educativo.Visible = securityHandler.VerifyAveliableScreens(trv_Educativo, level, "ED", True)
            pnl_Envios.Visible = securityHandler.VerifyAveliableScreens(trv_Envios, level, "EV", True)
            pnl_Rutas.Visible = securityHandler.VerifyAveliableScreens(trv_Rutas, level, "RU", True)
            pnl_Encuestas.Visible = securityHandler.VerifyAveliableScreens(trv_Encuestas, level, "EC", True)
            pnl_Laboral.Visible = securityHandler.VerifyAveliableScreens(trv_Laboral, level, "LB", True)
        End If
    End Sub

End Class
