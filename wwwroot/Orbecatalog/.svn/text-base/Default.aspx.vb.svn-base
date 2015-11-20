Imports Orbelink.DBHandler
Imports Orbelink.Entity.Entidades
Imports Orbelink.Entity.Orbecatalog

Partial Class Orbecatalog_Default
    Inherits PageBaseClass

    Const codigo_pantalla As String = "OR-01"
    Const level As Integer = 1

    'Pagina

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddScripts(level)


        securityHandler.VerifyPantalla(codigo_pantalla, level, True)
        If Not IsPostBack Then
            selectPendientes(securityHandler.Entidad)
            ddl_Themes.SelectedValue = securityHandler.Theme

            Dim publicacionesHandler As New Orbelink.Control.Publicaciones.ControladoraPublicaciones(Configuration.Config_DefaultConnectionString)
            If Not publicacionesHandler.selectPublicaciones_DataList(dtl_Publicaciones, level, "orbecatalog/Publicacion/Publicacion.aspx", securityHandler.TipoEntidad, securityHandler.Entidad, 0, 4, False, Nothing, True) Then
                pnl_Publicaciones.Visible = False
            End If

            Dim productosHandler As New Orbelink.Control.Productos.ProductosHandler(Configuration.Config_DefaultConnectionString)
            If Not productosHandler.selectProductos_DataGrid(dg_Productos, level) Then
                pnl_Productos.Visible = False
            End If

            'Dim proyectoHandler As New Orbelink.Control.Proyectos.ProyectosHandler(connection)
            'If Not proyectoHandler.selectProyectos_DataGrid(dg_Proyectos, securityHandler.Entidad, Configuration.Config_DefaultIdEntidad, 0, level) Then
            '    pnl_Proyectos.Visible = False
            'End If
        End If
    End Sub

    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        ddl_Themes.SelectedValue = securityHandler.Theme
    End Sub

    'Pendientes
    Protected Sub selectPendientes(ByVal id_Responsable As Integer)
        Dim dataset As Data.DataSet
        Dim entidad As New Entidad
        Dim pendientes As New Pendiente

        pendientes.Fields.SelectAll()
        pendientes.Terminado.Where.EqualCondition(0)

        If id_Responsable <> 0 Then
            pendientes.Id_Responsable.Where.EqualCondition(id_Responsable)
        End If

        queryBuilder.From.Add(pendientes)
        queryBuilder.OrderBy.Add(pendientes.FechaProgramado)
        queryBuilder.Top = 5
        dataset = connection.executeSelect(queryBuilder.RelationalSelectQuery)

        If dataset.Tables.Count > 0 Then
            If dataset.Tables(0).Rows.Count > 0 Then
                dtl_Pendientes.DataSource = dataset
                dtl_Pendientes.DataKeyField = pendientes.Id_Pendiente.Name
                dtl_Pendientes.DataBind()

                'Llena el grid
                Dim counter As Integer
                Dim results_Pendientes As ArrayList = objectBuilder.TransformDataTable(dataset.Tables(0), pendientes)
                For counter = 0 To dtl_Pendientes.Items.Count - 1
                    Dim lnk_Pendiente As HyperLink = dtl_Pendientes.Items(counter).FindControl("lnk_Pendiente")
                    Dim lbl_Fecha As Label = dtl_Pendientes.Items(counter).FindControl("lbl_Fecha")
                    Dim lbl_Comentario As Label = dtl_Pendientes.Items(counter).FindControl("lbl_Comentario")

                    Dim act_Pendientes As Pendiente = results_Pendientes(counter)
                    lnk_Pendiente.Text = act_Pendientes.Nombre.Value
                    lnk_Pendiente.NavigateUrl = "Informe_Pendiente.aspx?id_Pendiente=" & act_Pendientes.Id_Pendiente.Value
                    lbl_Fecha.Text = act_Pendientes.FechaProgramado.ValueLocalized
                    lbl_Comentario.Text = act_Pendientes.Descripcion.Value
                Next
            End If
        End If
    End Sub

    'Entidad
    Protected Function updateEntidad(ByVal id_entidad As Integer, ByVal nuevoTheme As String) As Boolean
        Try
            Dim entidad As New Entidad()
            entidad.Theme.Value = nuevoTheme
            entidad.Id_entidad.Where.EqualCondition(id_entidad)
            connection.executeUpdate(queryBuilder.UpdateQuery(entidad))
            Return True
        Catch exc As Exception
            MyMaster.mostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    Protected Sub ddl_Themes_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddl_Themes.SelectedIndexChanged
        securityHandler.Theme = ddl_Themes.SelectedValue
        If securityHandler.Entidad <> 0 Then
            updateEntidad(securityHandler.Entidad, securityHandler.Theme)
        End If
        Response.Redirect("Default.aspx")
    End Sub

End Class
