Imports Orbelink.DBHandler
Partial Class _DefaultProductos
    Inherits PageBaseClass

    Const codigo_pantalla As String = "PR-01"
    Const level As Integer = 2

    'Pagina
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddScripts(level)

        securityHandler.VerifyPantalla(codigo_pantalla, level)
        If Not IsPostBack Then
            Me.Title = Resources.Orbecatalog_Resources.Orbecatalog & " - " & Resources.Productos_Resources.ProductoModulo_Pantalla
            securityHandler.VerifyAveliableScreens(trv_Productos, level, "PR", False)

            Dim controladora As New Orbelink.Control.Productos.ProductosHandler(Configuration.Config_DefaultConnectionString)
            controladora.selectProductos_DataGrid(dg_Productos, level)
        End If
    End Sub

End Class
