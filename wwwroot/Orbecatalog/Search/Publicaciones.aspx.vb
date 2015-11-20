Imports Orbelink.DBHandler
Imports Orbelink.Entity.Envios
Imports Orbelink.Entity.Publicaciones
Imports Orbelink.Control.Publicaciones

Partial Class _Publicaciones
    Inherits PageBaseClass

    Const codigo_pantalla As String = "PU-SR"
    Const level As Integer = 2

    'Pagina
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddScripts(level)
        securityHandler.VerifyPantalla(codigo_pantalla, level)

        If Not Page.IsPostBack Then
            tr_Producto.Visible = False
            pnl_AccionesBatch.Visible = False
            pnl_Envios.Visible = False

            selectTiposPublicaciones()
            selectEntidades(securityHandler.Usuario)
            selectCategorias(trv_Categorias)
            ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
            DesarmarQuery()
        End If
    End Sub

    Protected Sub ClearInfo(ByVal estado As Configuration.EstadoPantalla, ByVal clearInfo As Boolean)
        'Select Case estado
        '    Case NORMAL
        '    Case MODIFICAR
        'End Select
    End Sub

    Protected Sub DesarmarQuery()
        Dim id_TipoPublicacion As Integer = 0
        Dim id_Categoria As Integer = 0
        Dim tituloSearch As String = ""
        Dim id_Producto As Integer = 0
        Dim general As Boolean = False

        If Not Request.QueryString("search") Is Nothing Then
            pnl_AccionesBatch.Visible = True
            selectEnvios()

            If Not Request.QueryString("tipoPublicacion") Is Nothing Then
                id_TipoPublicacion = Request.QueryString("tipoPublicacion")
                ddl_Filtro_TipoPublicacion.SelectedValue = id_TipoPublicacion
            End If

            If Not Request.QueryString("categoria") Is Nothing Then
                id_Categoria = Request.QueryString("categoria")
            End If

            'If Not Request.QueryString("id_Producto") Is Nothing Then
            '    id_Producto = Request.QueryString("id_Producto")
            '    selectProducto(id_Producto)
            'End If

            If Not Request.QueryString("tituloSearch") Is Nothing Then
                tituloSearch = Request.QueryString("tituloSearch")
                tbx_Search_Nombre.Text = tituloSearch
            End If

            If Not Request.QueryString("general") Is Nothing Then
                tituloSearch = Request.QueryString("general")
                general = True
            End If

            If Not Request.QueryString("pageSize") Is Nothing Then
                dg_Publicaciones.PageSize = Request.QueryString("pageSize")
                ddl_PageSize.SelectedValue = Request.QueryString("pageSize")
            End If

            If Not Request.QueryString("pageNumber") Is Nothing Then
                dg_Publicaciones.CurrentPageIndex = Request.QueryString("pageNumber")
            End If

            selectPublicaciones(id_TipoPublicacion, id_Producto, tituloSearch, general)
        End If
    End Sub

    Protected Function ArmarQueryString(ByVal pageNumber As Integer) As String
        Dim queryString As String = "Publicaciones.aspx?search=true"

        If Not ddl_Filtro_TipoPublicacion.SelectedIndex = ddl_Filtro_TipoPublicacion.Items.Count - 1 Then
            queryString &= "&tipoPublicacion=" & ddl_Filtro_TipoPublicacion.SelectedValue
        End If
        If tbx_Search_Nombre.Text.Length > 0 Then
            queryString &= "&tituloSearch=" & tbx_Search_Nombre.Text
        End If
        If lnk_Categoria.ToolTip.Length > 0 Then
            queryString &= "&Categoria=" & lnk_Categoria.ToolTip
        End If
        If lnk_Producto.Attributes("id_Producto") IsNot Nothing Then
            queryString &= "&id_Producto=" & lnk_Producto.Attributes("id_Producto")
        End If

        queryString &= "&pageNumber=" & pageNumber
        queryString &= "&pageSize=" & ddl_PageSize.SelectedValue
        Return queryString
    End Function

    'Tipos Publicaciones
    Protected Sub selectTiposPublicaciones()
        Dim ds As Data.DataSet
        Dim temp As New TipoPublicacion
        Dim result As String
        temp.Fields.SelectAll()
        result = queryBuilder.SelectQuery(temp)
        ds = connection.executeSelect(result)
        If ds.Tables.Count > 0 Then
            ddl_Filtro_TipoPublicacion.DataSource = ds
            ddl_Filtro_TipoPublicacion.DataTextField = temp.Nombre.Name
            ddl_Filtro_TipoPublicacion.DataValueField = temp.Id_TipoPublicacion.Name
            ddl_Filtro_TipoPublicacion.DataBind()
            ddl_Filtro_TipoPublicacion.Items.Add("-- Todos --")
            ddl_Filtro_TipoPublicacion.SelectedIndex = ddl_Filtro_TipoPublicacion.Items.Count - 1
        End If
    End Sub

    'Publicacion
    Protected Sub selectPublicaciones(ByVal id_tipoPublicacion As Integer, ByVal id_Producto As Integer, ByVal nombre As String, ByVal general As Boolean)
        Dim dataSet As Data.DataSet
        Dim Publicacion As New Publicacion
        Dim Producto_Publicacion As New Relacion_Producto_Publicacion

        Publicacion.Fields.SelectAll()
        Try
            If general Then
                If nombre.Length > 0 Then
                    Publicacion.Titulo.Where.LikeCondition(nombre)
                    Publicacion.Corta.Where.LikeCondition(nombre, Where.FieldRelations.OR_)
                    queryBuilder.GroupFieldsConditions(Publicacion.Titulo, Publicacion.Corta)
                End If
                queryBuilder.From.Add(Publicacion)
            Else
                'Busca si hay filtro
                If id_tipoPublicacion <> 0 Then
                    Publicacion.Id_tipoPublicacion.Where.EqualCondition(id_tipoPublicacion)
                End If

                If nombre.Length > 0 Then
                    Publicacion.Titulo.Where.LikeCondition(nombre)
                End If

                If id_Producto > 0 Then
                    Producto_Publicacion.id_Producto.Where.EqualCondition(id_Producto)
                    queryBuilder.Join.EqualCondition(Producto_Publicacion.id_Publicacion, Publicacion.Id_Publicacion)
                    queryBuilder.From.Add(Producto_Publicacion)
                End If

                queryBuilder.Orderby.Add(Publicacion.Titulo)
                queryBuilder.From.Add(Publicacion)
            End If

            Dim query As String = queryBuilder.RelationalSelectQuery
            dataSet = connection.executeSelect(query)

            If dataSet.Tables.Count > 0 Then
                If dataSet.Tables(0).Rows.Count > 0 Then
                    dg_Publicaciones.DataSource = dataSet
                    dg_Publicaciones.DataKeyField = Publicacion.Id_Publicacion.Name
                    dg_Publicaciones.DataBind()

                    'Llena el grid
                    Dim offset As Integer = dg_Publicaciones.CurrentPageIndex * dg_Publicaciones.PageSize
                    Dim result_Publicacion As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), Publicacion)

                    For counter As Integer = 0 To dg_Publicaciones.Items.Count - 1
                        Dim act_Publicacion As Publicacion = result_Publicacion(offset + counter)
                        'Dim act_Archivo As Archivo = obtenerArchivo(act_Publicacion.Id_Publicacion.Value)

                        Dim lbl_Nombre As Label = dg_Publicaciones.Items(counter).FindControl("lbl_Nombre")
                        Dim lnk_View As HyperLink = dg_Publicaciones.Items(counter).FindControl("lnk_View")
                        'Dim img_Publicacion As Image = dg_Publicaciones.Items(counter).FindControl("img_Publicacion")
                        'Dim lbl_Fecha As Label = dg_Publicaciones.Items(counter).FindControl("lbl_Fecha")
                        Dim lbl_Descripcion As Label = dg_Publicaciones.Items(counter).FindControl("lbl_Descripcion")

                        'Dim chk_Control As CheckBox = dg_Publicaciones.Items(counter).FindControl("chk_Control")
                        'Dim tbl_Publicacion As UI.HtmlControls.HtmlTable = dg_Publicaciones.Items(counter).FindControl("tbl_Publicacion")
                        'Dim img_MasInfo As UI.WebControls.Image = dg_Publicaciones.Items(counter).FindControl("img_MasInfo")


                        lbl_Nombre.Text = act_Publicacion.Titulo.Value
                        lnk_View.NavigateUrl = "../Publicacion/Publicacion.aspx?id_Publicacion=" & act_Publicacion.Id_Publicacion.Value
                        'lbl_Fecha.Text = act_Publicacion.Fecha.ValueLocalized
                        lbl_Descripcion.Text = act_Publicacion.Corta.Value

                        'Javascript
                        'img_MasInfo.Attributes.Add("onclick", "toggleLayer('" & tbl_Publicacion.ClientID & "', this)")
                        dg_Publicaciones.Items(counter).Attributes.Add("onmouseover", "setRowFocus(this, " & counter & ", false)")
                        dg_Publicaciones.Items(counter).Attributes.Add("onmouseout", "unsetRowFocus(this, " & counter & ", false)")
                    Next
                    lbl_ResultadoElementos.Visible = False
                    dg_Publicaciones.Visible = True
                Else
                    lbl_ResultadoElementos.Visible = True
                    dg_Publicaciones.Visible = False
                End If
            End If
        Catch ex As Exception
            MyMaster.MostrarMensaje(ex.Message, True)
        End Try
    End Sub

    'Producto
    'Protected Sub selectProducto(ByVal id_Producto As Integer)
    '    Dim dataSet As Data.DataSet
    '    Dim producto As New Producto

    '    producto.Nombre.ToSelect = True
    '    producto.Id_Producto.Where.EqualCondition(id_Producto)

    '    queryBuilder.From.Add(producto)
    '    dataSet = connection.executeSelect(queryBuilder.RelationalSelectQuery)

    '    If dataSet.Tables.Count > 0 Then
    '        If dataSet.Tables(0).Rows.Count > 0 Then
    '            ObjectBuilder.CreateObject(dataSet.Tables(0), 0, producto)

    '            'Carga la informacion
    '            lnk_Producto.Text = producto.Nombre.Value
    '            lnk_Producto.Attributes.Add("id_Producto", id_Producto)
    '            lnk_Producto.NavigateUrl = "../Productos/Productos.aspx?id_Producto=" & id_Producto

    '            tr_Producto.Visible = True
    '        End If
    '    End If
    'End Sub

    'Envios
    Protected Sub selectEnvios()
        Dim dataSet As Data.DataSet
        Dim Envio As New Envio

        Envio.Fields.SelectAll()
        queryBuilder.Orderby.Add(Envio.Subject)
        dataSet = connection.executeSelect(queryBuilder.SelectQuery(Envio))

        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                ddl_Envios.DataSource = dataSet
                ddl_Envios.DataValueField = Envio.Id_Envio.Name
                ddl_Envios.DataTextField = Envio.Subject.Name
                ddl_Envios.DataBind()

                pnl_Envios.Visible = True
            End If
        End If
    End Sub

    'Publicaciones_Envio
    Protected Function insertPublicaciones_Envio(ByVal id_Publicacion As Integer, ByVal id_Envio As Integer) As Boolean
        Dim Publicaciones_Envio As New Publicaciones_Envio
        Try
            Publicaciones_Envio.Id_Publicacion.Value = id_Publicacion
            Publicaciones_Envio.Id_Envio.Value = id_Envio
            Publicaciones_Envio.Orden.Value = 0
            connection.executeInsert(queryBuilder.InsertQuery(Publicaciones_Envio))
            Return True
        Catch exc As Exception
            'MyMaster.mostrarMensaje(exc.Message, true)
            '
            Return False
        End Try
    End Function

    'Categorias
    Protected Sub selectCategorias(ByVal theTreeView As TreeView)
        Dim categoria As New Categorias
        Dim sqlQuery As String
        Dim dataset As Data.DataSet
        Dim counter As Integer
        categoria.Fields.SelectAll()
        categoria.Id_Categoria.Where.EqualCondition_OnSameTable(categoria.Id_padre)
        sqlQuery = queryBuilder.SelectQuery(categoria)
        dataset = connection.executeSelect(sqlQuery)

        If dataset.Tables.Count > 0 Then
            For counter = 0 To dataset.Tables(0).Rows.Count - 1
                ObjectBuilder.CreateObject(dataset.Tables(0), counter, categoria)   'cambiar por transform
                Dim hijo As New TreeNode(categoria.Nombre.Value, categoria.Id_Categoria.Value)
                theTreeView.Nodes.Add(hijo)
                selectCategoriaesHijos(theTreeView.Nodes(counter))
            Next
        End If
    End Sub

    Protected Sub selectCategoriaesHijos(ByVal elTreeNode As TreeNode)
        Dim padre_id_Categoria As Integer = elTreeNode.Value
        Dim Categoria As New Categorias
        Dim sqlQuery As String
        Dim dataset As Data.DataSet
        Dim counter As Integer

        Categoria.Fields.SelectAll()
        Categoria.Id_padre.Where.EqualCondition(padre_id_Categoria)
        Categoria.Id_Categoria.Where.DiferentCondition(padre_id_Categoria)
        sqlQuery = queryBuilder.SelectQuery(Categoria)
        dataset = connection.executeSelect(sqlQuery)

        For counter = 0 To dataset.Tables(0).Rows.Count - 1
            ObjectBuilder.CreateObject(dataset.Tables(0), counter, Categoria)   'cambiar por transform
            Dim hijo As New TreeNode(Categoria.Nombre.Value, Categoria.Id_Categoria.Value)
            selectCategoriaesHijos(hijo)
            elTreeNode.ChildNodes.Add(hijo)
        Next
    End Sub

    'Entidades
    Protected Sub selectEntidades(ByVal id_entidad As Integer)
        'Dim dataSet As Data.DataSet
        'Dim entidad As New Entidad
        'Dim result As String

        'entidad.Id_entidad.ToSelect = True
        'entidad.Nombre.ToSelect = True
        'entidad.Nombre.Concatenate(entidad.Apellido, " ", "NombreApellido")
        'If id_entidad <> 0 Then
        '    entidad.Id_entidad.Where.EqualCondition(id_entidad)
        'End If
        'result = queryBuilder.SelectQuery(entidad)
        'dataSet = connection.executeSelect(result)

        'If dataSet.Tables.Count > 0 Then
        '    If dataSet.Tables(0).Rows.Count = 1 Then
        '        lbl_Entidad.Visible = True
        '    Else
        '        ddl_Entidad.DataSource = dataSet
        '        ddl_Entidad.DataTextField = entidad.Nombre.AsName
        '        ddl_Entidad.DataValueField = entidad.Id_entidad.Name
        '        ddl_Entidad.DataBind()
        '        ddl_Entidad.Visible = True
        '    End If
        'End If
    End Sub

    'Eventos
    Protected Sub dg_Publicaciones_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dg_Publicaciones.EditCommand
        Dim indiceTabla As Integer = dg_Publicaciones.DataKeys(e.Item.ItemIndex)
        ClearInfo(Configuration.EstadoPantalla.CONSULTAR, True)
        Response.Redirect("../Publicacion/Publicacion.aspx?id_Publicacion=" & indiceTabla)
    End Sub

    Protected Sub btn_Buscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Buscar.Click
        Response.Redirect(ArmarQueryString(0))
    End Sub

    Protected Sub dg_Publicaciones_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dg_Publicaciones.PageIndexChanged
        dg_Publicaciones.CurrentPageIndex = e.NewPageIndex
        Response.Redirect(ArmarQueryString(e.NewPageIndex))
    End Sub

    Protected Sub trv_Categorias_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles trv_Categorias.SelectedNodeChanged
        Dim id_NodoActual As Integer = trv_Categorias.SelectedNode.Value
        lnk_Categoria.Text = trv_Categorias.SelectedNode.Text
        lnk_Categoria.ToolTip = id_NodoActual
        trv_Categorias.Visible = False
    End Sub

    Protected Sub lnk_Categoria_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_Categoria.Click
        trv_Categorias.Visible = True
    End Sub

    Protected Sub chk_Todos_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim counter As Integer = 0
        Dim chk_Todos As CheckBox = sender
        For counter = 0 To dg_Publicaciones.Items.Count - 1
            Dim chk_Control As CheckBox = dg_Publicaciones.Items(counter).FindControl("chk_Control")
            If chk_Control.Enabled Then
                chk_Control.Checked = chk_Todos.Checked
            End If
        Next
        upd_Contenido.Update()
    End Sub

    Protected Sub btn_Eliminar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Eliminar.Click
        Dim controladora As New ControladoraPublicaciones(Configuration.Config_DefaultConnectionString)
        Dim algunoEliminado As Boolean = False
        For counter As Integer = 0 To dg_Publicaciones.Items.Count - 1
            Dim chk_Control As CheckBox = dg_Publicaciones.Items(counter).FindControl("chk_Control")
            Dim id_Publicacion As Integer = dg_Publicaciones.DataKeys(counter)
            If chk_Control.Checked Then
                Dim lbl_Nombre As Label = dg_Publicaciones.Items(counter).FindControl("lbl_Nombre")
                If controladora.EliminarPublicacion(id_Publicacion) Then
                    MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Eliminar, lbl_Nombre.Text, False)
                    algunoEliminado = True
                Else
                    MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Eliminar, lbl_Nombre.Text, True)
                End If
            End If
        Next
        If algunoEliminado Then
            DesarmarQuery()
            upd_Contenido.Update()
        End If
    End Sub

    Protected Sub btn_AgregarEnvio_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_AgregarEnvio.Click

        Dim insertadas As Integer = 0
        If ddl_Envios.Items.Count > 0 Then
            For counter As Integer = 0 To dg_Publicaciones.Items.Count - 1
                Dim chk_Control As CheckBox = dg_Publicaciones.Items(counter).FindControl("chk_Control")
                Dim id_Publicacion As Integer = chk_Control.Attributes("id_Publicacion")

                If chk_Control.Checked Then
                    If insertPublicaciones_Envio(id_Publicacion, ddl_Envios.SelectedValue) Then
                        insertadas += 1
                    End If
                End If
            Next
        End If
        MyMaster.mostrarMensaje("Publicaciones insertadas en el envio: " & insertadas, False)
        'upd_Busqueda.Update()
    End Sub

End Class

