Imports Orbelink.Orbecatalog6

Partial Class Orbecatalog_Sistema
    Inherits PageBaseClass

    Const codigo_pantalla As String = "MA-01"
    Const level As Integer = 1

    Protected Sub Page_Load1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddScripts(level)

        securityHandler.VerifyPantalla(codigo_pantalla, level)
        If Not IsPostBack Then
            securityHandler.VerifyAveliableScreens(trv_Orbecatalog, level, "OR", True)

            pnl_Productos.Visible = securityHandler.VerifyAveliableScreens_ForMantenimiento(trv_Productos, level, "PR", True)
            pnl_Entidades.Visible = securityHandler.VerifyAveliableScreens_ForMantenimiento(trv_Entidades, level, "CO", True)
            pnl_Facturas.Visible = securityHandler.VerifyAveliableScreens_ForMantenimiento(trv_Facturas, level, "FC", True)
            pnl_Publicaciones.Visible = securityHandler.VerifyAveliableScreens_ForMantenimiento(trv_Publicaciones, level, "PU", True)
            pnl_Proyectos.Visible = securityHandler.VerifyAveliableScreens_ForMantenimiento(trv_Proyectos, level, "PY", True)
            pnl_crm.Visible = securityHandler.VerifyAveliableScreens_ForMantenimiento(trv_crm, level, "CRM", True)
            pnl_reportes.Visible = securityHandler.VerifyAveliableScreens_ForMantenimiento(trv_Reportes, level, "RP", True)
            pnl_Procesos.Visible = securityHandler.VerifyAveliableScreens_ForMantenimiento(trv_Procesos, level, "PC", True)
            pnl_Educativo.Visible = securityHandler.VerifyAveliableScreens_ForMantenimiento(trv_Educativo, level, "ED", True)
            pnl_Envios.Visible = securityHandler.VerifyAveliableScreens_ForMantenimiento(trv_Envios, level, "EV", True)
            pnl_Rutas.Visible = securityHandler.VerifyAveliableScreens_ForMantenimiento(trv_Rutas, level, "RU", True)
            pnl_Encuestas.Visible = securityHandler.VerifyAveliableScreens_ForMantenimiento(trv_Encuestas, level, "EC", True)
            pnl_Laboral.Visible = securityHandler.VerifyAveliableScreens_ForMantenimiento(trv_Laboral, level, "LB", True)
            pnl_mantenimiento.Visible = securityHandler.VerifyAveliableScreens_ForMantenimiento(trv_mantenimiento, level, "MA", True)
        End If
    End Sub


    'Pagina





End Class
