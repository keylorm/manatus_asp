Imports Orbelink.DBHandler
Imports Orbelink.Entity.Productos
Imports Orbelink.Control.Productos

Partial Class _Productos
    Inherits PageBaseClass

    Const codigo_pantalla As String = "PR-SR"
    Const level As Integer = 2

    'Pagina
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddScripts(level)
        securityHandler.VerifyPantalla(codigo_pantalla, level)
        If Not Page.IsPostBack Then
            pnl_AccionesBatch.Visible = False

            selectEstados()
            selectEntidades(securityHandler.Entidad)
            ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
            DesarmarQueryString()
        End If
    End Sub

    Protected Sub ClearInfo(ByVal estado As Configuration.EstadoPantalla, ByVal clearInfo As Boolean)
        'Select Case estado
        '    Case NORMAL
        '    Case MODIFICAR
        'End Select
    End Sub

    Protected Sub DesarmarQueryString()
        'Dim soloFoto As Boolean
        Dim id_tipoProducto As Integer
        Dim id_origen As Integer
        Dim id_estado As Integer
        Dim nombre As String = ""
        Dim general As Boolean = False

        If Not Request.QueryString("search") Is Nothing Then
            pnl_AccionesBatch.Visible = True

            If Not Request.QueryString("tipoProducto") Is Nothing Then
                id_tipoProducto = Request.QueryString("tipoProducto")
            End If

            If Not Request.QueryString("origen") Is Nothing Then
                id_origen = Request.QueryString("origen")
            End If

            If Not Request.QueryString("estado") Is Nothing Then
                id_estado = Request.QueryString("estado")
                ddl_Estados.SelectedValue = Request.QueryString("estado")
            End If

            If Not Request.QueryString("nombre") Is Nothing Then
                nombre = Request.QueryString("nombre")
                tbx_Search_Producto.Text = Request.QueryString("nombre")
            End If

            If Not Request.QueryString("general") Is Nothing Then
                nombre = Request.QueryString("general")
                general = True
            End If

            If Not Request.QueryString("pageSize") Is Nothing Then
                dg_Productos.PageSize = Request.QueryString("pageSize")
                ddl_PageSize.SelectedValue = Request.QueryString("pageSize")
            End If

            If Not Request.QueryString("pageNumber") Is Nothing Then
                dg_Productos.CurrentPageIndex = Request.QueryString("pageNumber")
            End If

            selectProductos(id_tipoProducto, id_origen, id_estado, nombre, general)
            lbl_ResultadoElementos.Visible = True
        Else
            lbl_ResultadoElementos.Visible = False
        End If
        selectTipoProducto(trv_TipoProducto, id_tipoProducto)
        selectOrigenes(trv_Origenes, id_origen)
    End Sub

    Protected Function ArmarQueryString(ByVal pageNumber As Integer) As String
        Dim queryString As String = "Productos.aspx?search=true"

        'If chk_SoloFoto.Checked Then
        '    queryString &= "&soloFoto=true"
        'End If
        If lnk_TipoProducto.ToolTip.Length > 0 Then
            queryString &= "&tipoProducto=" & lnk_TipoProducto.ToolTip
        End If
        If lnk_Origen.ToolTip.Length > 0 Then
            queryString &= "&origen=" & lnk_Origen.ToolTip
        End If
        If Not ddl_Estados.SelectedIndex = ddl_Estados.Items.Count - 1 Then
            queryString &= "&estado=" & ddl_Estados.SelectedValue
        End If
        If tbx_Search_Producto.Text.Length > 0 Then
            queryString &= "&nombre=" & tbx_Search_Producto.Text
        End If
        queryString &= "&pageNumber=" & pageNumber
        queryString &= "&pageSize=" & ddl_PageSize.SelectedValue
        Return queryString
    End Function

    'Producto
    Protected Sub selectProductos(ByVal id_tipoProducto As Integer, ByVal id_origen As Integer, ByVal id_estado As Integer, ByVal nombre As String, ByVal general As Boolean)
        Dim dataSet As Data.DataSet
        Dim Producto As New Producto
        'Dim Archivo_Producto As New Archivo_Producto
        Dim tipoProducto As New TipoProducto
        Dim origenes As New Origenes

        'Archivo_Producto.FileName.ToSelect = True
        Producto.Fields.SelectAll()
        Try
            If general And nombre.Length > 0 Then
                Producto.Nombre.Where.LikeCondition(nombre)
                Producto.DescCorta_Producto.Where.LikeCondition(nombre, Where.FieldRelations.OR_)
                queryBuilder.GroupFieldsConditions(Producto.Nombre, Producto.DescCorta_Producto)
                Dim consulta As String = queryBuilder.SelectQuery(Producto)
                dataSet = connection.executeSelect(consulta)

            Else
                'Busca si hay filtro
                If id_tipoProducto <> 0 Then
                    Producto.Id_tipoProducto.Where.EqualCondition(id_tipoProducto)
                End If

                If nombre.Length > 0 Then
                    Producto.Nombre.Where.LikeCondition(nombre)
                End If

                If id_origen <> 0 Then
                    Producto.id_Origen.Where.EqualCondition(id_origen)
                End If

                If id_estado <> 0 Then
                    Producto.id_Estado.Where.EqualCondition(id_estado)
                End If

                'If soloFoto Then
                '    Archivo_Producto.Id_Producto.Where.EqualCondition(Producto.Id_Producto)
                '    Archivo_Producto.Principal.Where.EqualCondition(1)
                '    Archivo_Producto.Id_Archivo.ToSelect = True

                '    queryBuilder.From.Add(Archivo_Producto)
                'End If

                origenes.Nombre.ToSelect = True
                queryBuilder.Join.EqualCondition(origenes.Id_Origen, Producto.id_Origen)
                tipoProducto.Nombre.ToSelect = True
                queryBuilder.Join.EqualCondition(tipoProducto.Id_TipoProducto, Producto.Id_tipoProducto)

                queryBuilder.From.Add(Producto)
                queryBuilder.From.Add(origenes)
                queryBuilder.From.Add(tipoProducto)
                queryBuilder.Orderby.Add(Producto.Nombre)
                dataSet = connection.executeSelect(queryBuilder.RelationalSelectQuery)

            End If

            If dataSet.Tables.Count > 0 Then
                If dataSet.Tables(0).Rows.Count > 0 Then
                    dg_Productos.DataSource = dataSet
                    dg_Productos.DataKeyField = Producto.Id_Producto.Name
                    dg_Productos.DataBind()

                    'Llena el grid
                    Dim offset As Integer = dg_Productos.CurrentPageIndex * dg_Productos.PageSize
                    Dim counter As Integer
                    Dim results_Producto As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), Producto)
                    Dim results_Origenes As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), origenes)
                    Dim results_TipoProducto As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), tipoProducto)
                    Dim results_Archivo_Producto As New ArrayList
                    'If soloFoto Then
                    '    results_Archivo_Producto = ObjectBuilder.TransformDataTable(dataSet.Tables(0), Archivo_Producto)
                    'End If
                    For counter = 0 To dg_Productos.Items.Count - 1
                        Dim act_Producto As Producto = results_Producto(offset + counter)
                        Dim act_origenes As Origenes = results_Origenes(offset + counter)
                        Dim act_TipoProducto As TipoProducto = results_TipoProducto(offset + counter)
                        'Dim act_Archivo_Producto As Archivo_Producto

                        'Busca fotos
                        'If soloFoto Then
                        '    act_Archivo_Producto = results_Archivo_Producto(offset + counter)
                        'Else
                        '    act_Archivo_Producto = selectArchivo_Producto(act_Producto.Id_Producto.Value)
                        'End If

                        Dim lbl_Nombre As Label = dg_Productos.Items(counter).FindControl("lbl_Nombre")
                        Dim lbl_TipoProducto As Label = dg_Productos.Items(counter).FindControl("lbl_TipoProducto")
                        'Dim lbl_Origen As Label = dg_Productos.Items(counter).FindControl("lbl_Origen")
                        Dim lbl_Descripcion As Label = dg_Productos.Items(counter).FindControl("lbl_Descripcion")
                        'Dim lnk_Archivo As HyperLink = dg_Productos.Items(counter).FindControl("lnk_Archivo")
                        Dim lnk_Editar As HyperLink = dg_Productos.Items(counter).FindControl("lnk_Editar")
                        'Dim lnk_Producto As HyperLink = dg_Productos.Items(counter).FindControl("lnk_Producto")
                        Dim chk_Control As CheckBox = dg_Productos.Items(counter).FindControl("chk_Control")
                        'Dim tbl_Producto As UI.HtmlControls.HtmlTable = dg_Productos.Items(counter).FindControl("tbl_Entidad")
                        'Dim img_MasInfo As UI.WebControls.Image = dg_Productos.Items(counter).FindControl("img_MasInfo")

                        lbl_Nombre.Text = act_Producto.Nombre.Value
                        lbl_Descripcion.Text = act_Producto.DescCorta_Producto.Value
                        'lbl_Origen.Text = act_origenes.Nombre.Value
                        lbl_TipoProducto.Text = act_TipoProducto.Nombre.Value

                        'lnk_Archivo.NavigateUrl = "../Productos/Archivo_Producto.aspx?id_Producto=" & act_Producto.Id_Producto.Value
                        lnk_Editar.NavigateUrl = "../Productos/Productos.aspx?id_Producto=" & act_Producto.Id_Producto.Value

                        'If Archivo_Producto.FileName.IsValid Then
                        '    Orbelink.Control.Archivos.ArchivoHandler.GetArchivoLink(Configuration.ArchivoProperties, Configuration.producto_ArchivoId, lnk_Producto, act_Archivo_Producto.FileName.Value, act_Archivo_Producto.Extension.Value, level)
                        'Else
                        'lnk_Producto.ImageUrl = "~/orbecatalog/images/mm.jpg"
                        'End If

                        'Javascript
                        'img_MasInfo.Attributes.Add("onclick", "toggleLayer('" & tbl_Producto.ClientID & "', this)")
                        dg_Productos.Items(counter).Attributes.Add("onmouseover", "setRowFocus(this, " & counter & ", false)")
                        dg_Productos.Items(counter).Attributes.Add("onmouseout", "unsetRowFocus(this, " & counter & ", false)")
                    Next
                    lbl_ResultadoElementos.Text = contadorElementos(offset, dg_Productos.PageSize, dataSet.Tables(0).Rows.Count)
                    dg_Productos.Visible = True
                Else
                    lbl_ResultadoElementos.Text = contadorElementos(0, 0, 0)
                    dg_Productos.Visible = False
                End If

            End If
        Catch ex As Exception
            MyMaster.mostrarMensaje(ex.Message, True)

        End Try
    End Sub

    'TipoProducto
    Protected Sub selectTipoProducto(ByVal theTreeView As TreeView, ByVal id_tipoProducto As Integer)
        Dim tipoProducto As New TipoProducto
        Dim sqlQuery As String
        Dim dataset As Data.DataSet
        Dim counter As Integer

        tipoProducto.Fields.SelectAll()
        tipoProducto.Id_TipoProducto.Where.EqualCondition_OnSameTable(tipoProducto.Id_padre)
        sqlQuery = queryBuilder.SelectQuery(tipoProducto)
        dataset = connection.executeSelect(sqlQuery)

        If dataset.Tables.Count > 0 Then
            If dataset.Tables(0).Rows.Count > 0 Then
                Dim resul_TipoProducto As ArrayList = ObjectBuilder.TransformDataTable(dataset.Tables(0), tipoProducto)

                For counter = 0 To dataset.Tables(0).Rows.Count - 1
                    Dim act_TipoProducto As TipoProducto = resul_TipoProducto(counter)
                    Dim hijo As New TreeNode(act_TipoProducto.Nombre.Value, act_TipoProducto.Id_TipoProducto.Value)
                    theTreeView.Nodes.Add(hijo)
                    If act_TipoProducto.Id_TipoProducto.Value = id_tipoProducto Then
                        lnk_TipoProducto.Text = act_TipoProducto.Nombre.Value
                        lnk_TipoProducto.ToolTip = id_tipoProducto
                    End If
                    selectTipoProductoHijos(theTreeView.Nodes(counter), id_tipoProducto)
                Next
            End If
        End If
        theTreeView.CollapseAll()
    End Sub

    Protected Sub selectTipoProductoHijos(ByVal elTreeNode As TreeNode, ByVal id_tipoProducto As Integer)
        Dim padre_id_TipoProducto As Integer = elTreeNode.Value
        Dim tipoProducto As New TipoProducto
        Dim sqlQuery As String
        Dim dataset As Data.DataSet
        Dim counter As Integer

        tipoProducto.Fields.SelectAll()
        tipoProducto.Id_padre.Where.EqualCondition(padre_id_TipoProducto)
        tipoProducto.Id_TipoProducto.Where.DiferentCondition(padre_id_TipoProducto)
        sqlQuery = queryBuilder.SelectQuery(tipoProducto)
        dataset = connection.executeSelect(sqlQuery)

        If dataset.Tables.Count > 0 Then
            If dataset.Tables(0).Rows.Count > 0 Then
                Dim resul_Origenes As ArrayList = ObjectBuilder.TransformDataTable(dataset.Tables(0), tipoProducto)

                For counter = 0 To dataset.Tables(0).Rows.Count - 1
                    Dim act_tipoProducto As TipoProducto = resul_Origenes(counter)
                    Dim hijo As New TreeNode(act_tipoProducto.Nombre.Value, act_tipoProducto.Id_TipoProducto.Value)
                    If act_tipoProducto.Id_TipoProducto.Value = id_tipoProducto Then
                        lnk_TipoProducto.Text = act_tipoProducto.Nombre.Value
                        lnk_TipoProducto.ToolTip = id_tipoProducto
                    End If
                    selectTipoProductoHijos(hijo, id_tipoProducto)
                    elTreeNode.ChildNodes.Add(hijo)
                Next
            End If
        End If
    End Sub

    'Origenes
    Protected Sub selectOrigenes(ByVal theTreeView As TreeView, ByVal id_origen As Integer)
        Dim origen As New Origenes
        Dim sqlQuery As String
        Dim dataset As Data.DataSet
        Dim counter As Integer
        origen.Fields.SelectAll()
        origen.Id_Origen.Where.EqualCondition_OnSameTable(origen.Id_padre)
        sqlQuery = queryBuilder.SelectQuery(origen)
        dataset = connection.executeSelect(sqlQuery)
        If dataset.Tables.Count > 0 Then
            If dataset.Tables(0).Rows.Count > 0 Then
                Dim resul_Origenes As ArrayList = ObjectBuilder.TransformDataTable(dataset.Tables(0), origen)

                For counter = 0 To dataset.Tables(0).Rows.Count - 1
                    Dim act_Origen As Origenes = resul_Origenes(counter)
                    Dim hijo As New TreeNode(act_Origen.Nombre.Value, act_Origen.Id_Origen.Value)
                    If act_Origen.Id_Origen.Value = id_origen Then
                        lnk_Origen.Text = act_Origen.Nombre.Value
                        lnk_Origen.ToolTip = id_origen
                    End If
                    theTreeView.Nodes.Add(hijo)
                    selectOrigenesHijos(theTreeView.Nodes(counter), id_origen)
                Next
            End If
        End If
        theTreeView.CollapseAll()
    End Sub

    Protected Sub selectOrigenesHijos(ByVal elTreeNode As TreeNode, ByVal id_origen As Integer)
        Dim padre_id_Origenes As Integer = elTreeNode.Value
        Dim origen As New Origenes
        Dim sqlQuery As String
        Dim dataset As Data.DataSet
        Dim counter As Integer

        origen.Fields.SelectAll()
        origen.Id_padre.Where.EqualCondition(padre_id_Origenes)
        origen.Id_Origen.Where.DiferentCondition(padre_id_Origenes)
        sqlQuery = queryBuilder.SelectQuery(origen)
        dataset = connection.executeSelect(sqlQuery)

        If dataset.Tables.Count > 0 Then
            If dataset.Tables(0).Rows.Count > 0 Then
                Dim resul_Origenes As ArrayList = ObjectBuilder.TransformDataTable(dataset.Tables(0), origen)

                For counter = 0 To dataset.Tables(0).Rows.Count - 1
                    Dim act_Origen As Origenes = resul_Origenes(counter)
                    Dim hijo As New TreeNode(act_Origen.Nombre.Value, act_Origen.Id_Origen.Value)
                    If act_Origen.Id_Origen.Value = id_origen Then
                        lnk_Origen.Text = act_Origen.Nombre.Value
                        lnk_Origen.ToolTip = id_origen
                    End If
                    selectOrigenesHijos(hijo, id_origen)
                    elTreeNode.ChildNodes.Add(hijo)
                Next
            End If
        End If
    End Sub

    'Estados
    Protected Sub selectEstados()
        Dim dataset As Data.DataSet
        Dim estados_Productos As New Estados_Productos

        estados_Productos.Fields.SelectAll()
        dataset = connection.executeSelect(queryBuilder.SelectQuery(estados_Productos))
        If dataset.Tables.Count > 0 Then
            If dataset.Tables(0).Rows.Count > 0 Then

                ddl_Estados.DataSource = dataset
                ddl_Estados.DataTextField = estados_Productos.Nombre.Name
                ddl_Estados.DataValueField = estados_Productos.Id_Estado_Producto.Name
                ddl_Estados.DataBind()
            End If
        End If
        ddl_Estados.Items.Add("-- Todos --")
        ddl_Estados.SelectedIndex = ddl_Estados.Items.Count - 1
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
    Protected Sub dg_Productos_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dg_Productos.EditCommand
        Dim indiceTabla As Integer = dg_Productos.DataKeys(e.Item.ItemIndex)
        ClearInfo(Configuration.EstadoPantalla.CONSULTAR, True)
        Response.Redirect("../Producto/Producto.aspx?id_Producto=" & indiceTabla)
    End Sub

    Protected Sub btn_Buscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Buscar.Click
        Response.Redirect(ArmarQueryString(0))
    End Sub

    Protected Sub dg_Productos_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dg_Productos.PageIndexChanged
        dg_Productos.CurrentPageIndex = e.NewPageIndex
        Response.Redirect(ArmarQueryString(e.NewPageIndex))
    End Sub

    Protected Sub trv_Origenes_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles trv_Origenes.SelectedNodeChanged
        Dim id_NodoActual As Integer = trv_Origenes.SelectedNode.Value
        lnk_Origen.Text = trv_Origenes.SelectedNode.Text
        lnk_Origen.ToolTip = id_NodoActual
        trv_Origenes.Visible = False
    End Sub

    Protected Sub lnk_Origen_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_Origen.Click
        trv_Origenes.Visible = True
    End Sub

    Protected Sub lnk_TipoProducto_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_TipoProducto.Click
        trv_TipoProducto.Visible = True
    End Sub

    Protected Sub trv_TipoProducto_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles trv_TipoProducto.SelectedNodeChanged
        lnk_TipoProducto.Text = trv_TipoProducto.SelectedNode.Text
        lnk_TipoProducto.ToolTip = trv_TipoProducto.SelectedNode.Value
        trv_TipoProducto.Visible = False
        'selectAtributos(trv_TipoProducto.SelectedNode.Value)
    End Sub

    Protected Sub chk_Todos_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim counter As Integer = 0
        Dim chk_Todos As CheckBox = sender
        For counter = 0 To dg_Productos.Items.Count - 1
            Dim chk_Control As CheckBox = dg_Productos.Items(counter).FindControl("chk_Control")
            If chk_Control.Enabled Then
                chk_Control.Checked = chk_Todos.Checked
            End If
        Next
        upd_Contenido.Update()
    End Sub

    Protected Sub btn_Eliminar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Eliminar.Click
        Dim controladora As New ProductosHandler(Configuration.Config_DefaultConnectionString)
        Dim algunoEliminado As Boolean = False
        For counter As Integer = 0 To dg_Productos.Items.Count - 1
            Dim chk_Control As CheckBox = dg_Productos.Items(counter).FindControl("chk_Control")
            Dim id_Producto As Integer = dg_Productos.DataKeys(counter)
            If chk_Control.Checked Then
                Dim lbl_Nombre As Label = dg_Productos.Items(counter).FindControl("lbl_Nombre")
                If controladora.EliminarProducto(id_Producto) Then
                    MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Eliminar, lbl_Nombre.Text, False)
                    algunoEliminado = True
                Else
                    MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Eliminar, lbl_Nombre.Text, True)
                End If
            End If
        Next
        If algunoEliminado Then
            DesarmarQueryString()
            upd_Contenido.Update()
        End If
    End Sub

End Class

