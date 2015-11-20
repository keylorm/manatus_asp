Imports Orbelink.DBHandler
Imports Orbelink.Entity.Productos

Partial Class _Atributos_TipoProducto
    Inherits PageBaseClass

    Const codigo_pantalla As String = "PR-05"
    Const level As Integer = 2

    'Pagina
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddScripts(level)
        securityHandler.VerifyPantalla(codigo_pantalla, level)
        If Not IsPostBack Then
            Me.Title = Resources.Orbecatalog_Resources.Orbecatalog & " - " & Resources.Productos_Resources.AtributosTipoProducto_Pantalla

            ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
            securityHandler.VerifyActions(btn_Salvar, btn_Modificar, btn_Eliminar)

            lnk_TipoProducto.NavigateUrl = "javascript:toggleLayer('" & trv_TipoProducto.ClientID & "', this)"
            selectTipoProducto(trv_TipoProducto)
            selectMaestroAtributos_Productos()
            If lnk_TipoProducto.ToolTip.Length > 0 Then
                selectAtributos_TipoProducto(lnk_TipoProducto.ToolTip)
                loadMaestroAtributos_Productos(lnk_TipoProducto.ToolTip)
            End If
        End If
    End Sub

    Protected Sub ClearInfo(ByVal estado As Configuration.EstadoPantalla, ByVal clearInfo As Boolean)
        If clearInfo Then
            'tbx_NombreEntidad.Text = ""
        End If

        Select Case estado
            Case Configuration.EstadoPantalla.NORMAL
                btn_Modificar.Visible = False

                btn_Salvar.Visible = True

                btn_Eliminar.Visible = False

            Case Configuration.EstadoPantalla.CONSULTAR
                btn_Modificar.Visible = True

                btn_Salvar.Visible = False

                btn_Eliminar.Visible = True

        End Select
    End Sub

    'Maestro_Atributos_Productos
    Protected Sub selectMaestroAtributos_Productos()
        Dim dataSet As Data.DataSet
        Dim Atributos_Para_Productos As New Atributos_Para_Productos

        Atributos_Para_Productos.Fields.SelectAll()

        queryBuilder.From.Add(Atributos_Para_Productos)
        queryBuilder.OrderBy.Add(Atributos_Para_Productos.Nombre)
        dataSet = connection.executeSelect(queryBuilder.RelationalSelectQuery)
        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables.Count > 0 Then
                dg_MaestroAtributos_Productos.DataSource = dataSet
                dg_MaestroAtributos_Productos.DataBind()

                Dim results As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), Atributos_Para_Productos)
                For counter As Integer = 0 To dataSet.Tables(0).Rows.Count - 1
                    Dim act_Atributos_Productos As Atributos_Para_Productos = results(counter)
                    Dim chk_Seleccionado As CheckBox = dg_MaestroAtributos_Productos.Items(counter).FindControl("chk_Seleccionado")
                    Dim lbl_NombreCheck As Label = dg_MaestroAtributos_Productos.Items(counter).FindControl("lbl_NombreCheck")
                    Dim img_Info As UI.WebControls.Image = dg_MaestroAtributos_Productos.Items(counter).FindControl("img_Info")

                    lbl_NombreCheck.Text = act_Atributos_Productos.Nombre.Value
                    lbl_NombreCheck.Attributes.Add(act_Atributos_Productos.Id_atributo.Name, act_Atributos_Productos.Id_atributo.Value)
                    chk_Seleccionado.ToolTip = act_Atributos_Productos.Id_atributo.Value

                    If act_Atributos_Productos.Descripcion.IsValid Then
                        img_Info.AlternateText = act_Atributos_Productos.Descripcion.Value
                        img_Info.ToolTip = act_Atributos_Productos.Descripcion.Value
                        img_Info.Visible = True
                    Else
                        img_Info.Visible = False
                    End If
                Next
            Else
                dg_MaestroAtributos_Productos.Visible = False
            End If
        End If
    End Sub

    Protected Sub loadMaestroAtributos_Productos(ByVal id_TipoProducto As Integer)
        Dim dataSet As Data.DataSet
        Dim Atributos_TipoProducto As New Atributos_TipoProducto
        Dim counterA As Integer
        Dim counterB As Integer

        If id_TipoProducto <> 0 Then
            Atributos_TipoProducto.Fields.SelectAll()
            Atributos_TipoProducto.Id_TipoProducto.Where.EqualCondition(id_TipoProducto)
            queryBuilder.From.Add(Atributos_TipoProducto)
            dataSet = connection.executeSelect(queryBuilder.RelationalSelectQuery())

            If dataSet.Tables.Count > 0 Then
                If dataSet.Tables(0).Rows.Count > 0 Then
                    Dim resul_Atributos_TipoProducto As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), Atributos_TipoProducto)
                    For counterA = 0 To resul_Atributos_TipoProducto.Count - 1            'Recorre los Atributos_TipoProducto
                        Dim act_Atributos_TipoProducto As Atributos_TipoProducto = resul_Atributos_TipoProducto(counterA)
                        For counterB = 0 To dg_MaestroAtributos_Productos.Items.Count - 1                  'Recorre todos los Atributos_Para_Productos
                            Dim lbl_NombreCheck As Label = dg_MaestroAtributos_Productos.Items(counterB).FindControl("lbl_NombreCheck")
                            Dim chk_Seleccionado As CheckBox = dg_MaestroAtributos_Productos.Items(counterB).FindControl("chk_Seleccionado")

                            If lbl_NombreCheck.Attributes("Id_Atributo") = act_Atributos_TipoProducto.id_atributo.Value Then
                                chk_Seleccionado.Checked = True
                                lbl_NombreCheck.Attributes.Add("EnDB", "Si")
                                Exit For
                            End If
                        Next
                    Next
                End If
            End If
        End If
    End Sub

    Protected Sub clearAtributos_Productos()
        Dim counter As Integer
        For counter = 0 To dg_MaestroAtributos_Productos.Items.Count - 1                  'Recorre todos los Atributos_Para_Productos
            Dim chk_Seleccionado As CheckBox = dg_MaestroAtributos_Productos.Items(counter).FindControl("chk_Seleccionado")
            Dim lbl_NombreCheck As Label = dg_MaestroAtributos_Productos.Items(counter).FindControl("lbl_NombreCheck")

            chk_Seleccionado.Checked = False
            lbl_NombreCheck.Attributes("EnDB") = "No"
        Next
    End Sub

    Protected Function checkearMaestroAtributos_Productos(ByVal id_TipoProducto As Integer, ByVal offsetOrden As Integer) As Boolean
        Dim Atributos_TipoProducto As New Atributos_TipoProducto
        Dim counter As Integer

        For counter = 0 To dg_MaestroAtributos_Productos.Items.Count - 1      'Recorre Atributos_Para_Productos
            Dim lbl_NombreCheck As Label = dg_MaestroAtributos_Productos.Items(counter).FindControl("lbl_NombreCheck")
            Dim chk_Seleccionado As CheckBox = dg_MaestroAtributos_Productos.Items(counter).FindControl("chk_Seleccionado")
            Dim Id_Atributo As Integer = lbl_NombreCheck.Attributes("Id_Atributo")

            If lbl_NombreCheck.Attributes("EnDB") = "Si" Then
                If Not chk_Seleccionado.Checked Then       'Borra
                    Atributos_TipoProducto.Id_TipoProducto.Where.EqualCondition(id_TipoProducto)
                    Atributos_TipoProducto.id_atributo.Where.EqualCondition(Id_Atributo)
                    connection.executeDelete(queryBuilder.DeleteQuery(Atributos_TipoProducto))
                End If
            Else                                            'Inserta
                If chk_Seleccionado.Checked Then
                    offsetOrden += 1
                    Atributos_TipoProducto.Id_TipoProducto.Value = id_TipoProducto
                    Atributos_TipoProducto.id_atributo.Value = Id_Atributo
                    Atributos_TipoProducto.Orden.Value = offsetOrden
                    Atributos_TipoProducto.Buscable.Value = 0
                    connection.executeInsert(queryBuilder.InsertQuery(Atributos_TipoProducto))
                End If
            End If
        Next

        Return True
    End Function


    'Atributos_TipoProducto
    Protected Sub selectAtributos_TipoProducto(ByVal id_TipoProducto As Integer)
        Dim dataSet As Data.DataSet
        Dim Atributos_TipoProducto As New Atributos_TipoProducto
        Dim Atributos_Para_Productos As New Atributos_Para_Productos
        Dim counter As Integer

        If id_TipoProducto <> 0 Then
            Atributos_TipoProducto.Fields.SelectAll()
            Atributos_TipoProducto.Id_TipoProducto.Where.EqualCondition(id_TipoProducto)
            Atributos_Para_Productos.Fields.SelectAll()
            queryBuilder.Join.EqualCondition(Atributos_Para_Productos.Id_atributo, Atributos_TipoProducto.Id_Atributo)

            queryBuilder.From.Add(Atributos_Para_Productos)
            queryBuilder.From.Add(Atributos_TipoProducto)
            queryBuilder.OrderBy.Add(Atributos_TipoProducto.Orden)
            dataSet = connection.executeSelect(queryBuilder.RelationalSelectQuery())

            If dataSet.Tables.Count > 0 Then
                If dataSet.Tables(0).Rows.Count > 0 Then
                    dg_Atributos_Productos.DataSource = dataSet
                    dg_Atributos_Productos.DataBind()

                    Dim results As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), Atributos_Para_Productos)
                    Dim resul_Atributos_TipoProducto As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), Atributos_TipoProducto)
                    For counter = 0 To dataSet.Tables(0).Rows.Count - 1      'Recorre todos
                        Dim act_Atributos_Productos As Atributos_Para_Productos = results(counter)
                        Dim act_Atributos_TipoProducto As Atributos_TipoProducto = resul_Atributos_TipoProducto(counter)

                        Dim lbl_NombreCheck As Label = dg_Atributos_Productos.Items(counter).FindControl("lbl_NombreCheck")
                        Dim chk_Buscable As CheckBox = dg_Atributos_Productos.Items(counter).FindControl("chk_Buscable")
                        Dim btn_Subir As ImageButton = dg_Atributos_Productos.Items(counter).FindControl("btn_Subir")
                        Dim btn_Bajar As ImageButton = dg_Atributos_Productos.Items(counter).FindControl("btn_Bajar")

                        lbl_NombreCheck.Text = act_Atributos_Productos.Nombre.Value
                        lbl_NombreCheck.Attributes.Add(Atributos_TipoProducto.Id_TipoProducto.Name, act_Atributos_TipoProducto.Id_TipoProducto.Value)
                        lbl_NombreCheck.Attributes.Add(Atributos_TipoProducto.id_atributo.Name, act_Atributos_TipoProducto.Id_TipoProducto.Value)
                        lbl_NombreCheck.Attributes.Add(Atributos_TipoProducto.Orden.Name, act_Atributos_TipoProducto.Orden.Value)

                        If act_Atributos_TipoProducto.Buscable.Value = 1 Then
                            chk_Buscable.Checked = True
                        Else
                            chk_Buscable.Checked = False
                        End If
                        btn_Subir.Attributes.Add("indice", counter)
                        btn_Bajar.Attributes.Add("indice", counter)

                        dg_Atributos_Productos.Items(counter).Attributes.Add("onmouseover", "setRowFocus(this, " & counter & ", false)")
                        dg_Atributos_Productos.Items(counter).Attributes.Add("onmouseout", "unsetRowFocus(this, " & counter & ", false)")
                    Next
                    dg_Atributos_Productos.Visible = True
                Else
                    dg_Atributos_Productos.Visible = False
                End If
            End If
        End If
    End Sub

    Protected Sub updateAtributos_TipoProducto()
        Dim Atributos_TipoProducto As New Atributos_TipoProducto
        Dim counter As Integer

        For counter = 0 To dg_Atributos_Productos.Items.Count - 1      'Recorre Atributos_Para_Productos
            Dim lbl_NombreCheck As Label = dg_Atributos_Productos.Items(counter).FindControl("lbl_NombreCheck")
            Dim chk_Buscable As CheckBox = dg_Atributos_Productos.Items(counter).FindControl("chk_Buscable")

            Atributos_TipoProducto.Orden.Value = lbl_NombreCheck.Attributes(Atributos_TipoProducto.Orden.Name)
            Atributos_TipoProducto.Id_TipoProducto.Where.EqualCondition(lbl_NombreCheck.Attributes(Atributos_TipoProducto.Id_TipoProducto.Name))
            Atributos_TipoProducto.id_atributo.Where.EqualCondition(lbl_NombreCheck.Attributes(Atributos_TipoProducto.id_atributo.Name))
            If chk_Buscable.Checked Then
                Atributos_TipoProducto.Buscable.Value = 1
            Else
                Atributos_TipoProducto.Buscable.Value = 0
            End If

            connection.executeUpdate(queryBuilder.UpdateQuery(Atributos_TipoProducto))
        Next
    End Sub

    'Ordenes
    Protected Sub cambiarInfo(ByVal indiceA As Integer, ByVal indiceB As Integer)
        Dim temp As String
        Dim Atributos_TipoProducto As New Atributos_TipoProducto

        Dim lbl_NombreCheckA As Label = dg_Atributos_Productos.Items(indiceA).FindControl("lbl_NombreCheck")

        Dim lbl_NombreCheckB As Label = dg_Atributos_Productos.Items(indiceB).FindControl("lbl_NombreCheck")

        temp = lbl_NombreCheckA.Text
        lbl_NombreCheckA.Text = lbl_NombreCheckB.Text
        lbl_NombreCheckB.Text = temp

        temp = lbl_NombreCheckA.ToolTip
        lbl_NombreCheckA.ToolTip = lbl_NombreCheckB.ToolTip
        lbl_NombreCheckB.ToolTip = temp

        temp = lbl_NombreCheckA.Attributes(Atributos_TipoProducto.id_atributo.Name)
        lbl_NombreCheckA.Attributes(Atributos_TipoProducto.id_atributo.Name) = lbl_NombreCheckB.Attributes(Atributos_TipoProducto.id_atributo.Name)
        lbl_NombreCheckB.Attributes(Atributos_TipoProducto.id_atributo.Name) = temp
    End Sub

    'TipoProducto
    Protected Sub selectTipoProducto(ByVal theTreeView As TreeView)
        Dim tipoProducto As New TipoProducto
        Dim dataset As Data.DataSet
        Dim counter As Integer

        tipoProducto.Fields.SelectAll()
        tipoProducto.Id_TipoProducto.Where.EqualCondition_OnSameTable(tipoProducto.Id_padre)
        queryBuilder.OrderBy.Add(tipoProducto.Nombre)
        dataset = connection.executeSelect(queryBuilder.SelectQuery(tipoProducto))

        If dataset.Tables.Count > 0 Then
            If dataset.Tables(0).Rows.Count > 0 Then
                Dim resul_TipoProducto As ArrayList = ObjectBuilder.TransformDataTable(dataset.Tables(0), tipoProducto)
                For counter = 0 To dataset.Tables(0).Rows.Count - 1
                    Dim act_TipoProducto As TipoProducto = resul_TipoProducto(counter)
                    Dim hijo As New TreeNode(act_TipoProducto.Nombre.Value, act_TipoProducto.Id_TipoProducto.Value)
                    theTreeView.Nodes.Add(hijo)
                    selectTipoProductoHijos(theTreeView.Nodes(counter))
                Next
            End If
        End If
        theTreeView.CollapseAll()
    End Sub

    Protected Sub selectTipoProductoHijos(ByVal elTreeNode As TreeNode)
        Dim padre_id_TipoProducto As Integer = elTreeNode.Value
        Dim tipoProducto As New TipoProducto
        Dim dataset As Data.DataSet
        Dim counter As Integer

        tipoProducto.Fields.SelectAll()
        tipoProducto.Id_padre.Where.EqualCondition(padre_id_TipoProducto)
        tipoProducto.Id_TipoProducto.Where.DiferentCondition(padre_id_TipoProducto)
        queryBuilder.OrderBy.Add(tipoProducto.Nombre)
        dataset = connection.executeSelect(queryBuilder.SelectQuery(tipoProducto))

        If dataset.Tables.Count > 0 Then
            If dataset.Tables(0).Rows.Count > 0 Then
                Dim resul_Origenes As ArrayList = ObjectBuilder.TransformDataTable(dataset.Tables(0), tipoProducto)
                For counter = 0 To dataset.Tables(0).Rows.Count - 1
                    Dim act_tipoProducto As TipoProducto = resul_Origenes(counter)
                    Dim hijo As New TreeNode(act_tipoProducto.Nombre.Value, act_tipoProducto.Id_TipoProducto.Value)
                    selectTipoProductoHijos(hijo)
                    elTreeNode.ChildNodes.Add(hijo)
                Next
            End If
        End If
    End Sub

    'Eventos
    Protected Sub btn_Salvar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Salvar.Click
        If lnk_TipoProducto.ToolTip.Length > 0 Then
            updateAtributos_TipoProducto()
            ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
            selectAtributos_TipoProducto(lnk_TipoProducto.ToolTip)
        End If
        upd_Contenido.Update()
        upd_Busqueda.Update()
    End Sub

    Protected Sub btn_Cancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Cancelar.Click
        If lnk_TipoProducto.ToolTip.Length > 0 Then
            ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
            selectAtributos_TipoProducto(lnk_TipoProducto.ToolTip)
        End If
        upd_Contenido.Update()
    End Sub

    Protected Sub btn_Actualizar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Actualizar.Click
        If lnk_TipoProducto.ToolTip.Length > 0 Then
            Dim offsetOrden As Integer = 0
            If dg_Atributos_Productos.Items.Count > 0 Then
                Dim lbl_NombreCheck As Label = dg_Atributos_Productos.Items(dg_Atributos_Productos.Items.Count - 1).FindControl("lbl_NombreCheck")
                offsetOrden = lbl_NombreCheck.Attributes("Orden")
            End If
            checkearMaestroAtributos_Productos(lnk_TipoProducto.ToolTip, offsetOrden)
            loadMaestroAtributos_Productos(lnk_TipoProducto.ToolTip)
            selectAtributos_TipoProducto(lnk_TipoProducto.ToolTip)
        End If
        upd_Contenido.Update()
        upd_Busqueda.Update()
    End Sub

    Protected Sub trv_TipoProducto_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles trv_TipoProducto.SelectedNodeChanged
        clearAtributos_Productos()
        lnk_TipoProducto.Text = trv_TipoProducto.SelectedNode.Text
        lnk_TipoProducto.ToolTip = trv_TipoProducto.SelectedNode.Value
        loadMaestroAtributos_Productos(lnk_TipoProducto.ToolTip)
        selectAtributos_TipoProducto(lnk_TipoProducto.ToolTip)
        upd_Contenido.Update()
        upd_Busqueda.Update()
    End Sub

    Protected Sub btn_Subir_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim btn_Subir As ImageButton = sender
        Dim indice As Integer = btn_Subir.Attributes("indice")

        If indice > 0 Then
            Dim otroIndice As Integer = indice - 1
            cambiarInfo(indice, otroIndice)
        End If
        upd_Contenido.Update()
    End Sub

    Protected Sub btn_Bajar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs)
        Dim btn_Bajar As ImageButton = sender
        Dim indice As Integer = btn_Bajar.Attributes("indice")

        If indice < dg_Atributos_Productos.Items.Count - 1 Then
            Dim otroIndice As Integer = indice + 1
            cambiarInfo(indice, otroIndice)
        End If
        upd_Contenido.Update()
    End Sub

    'Agregar
    Protected Sub btn_AgregarAtributo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_AgregarAtributo.Click
        Response.Redirect("Atributos.aspx" & Orbelink.Helpers.CommonTasks.RecreateQueryString(codigo_pantalla, True))
    End Sub

    Protected Sub btn_AgregarTipoProducto_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_AregarTipoProducto.Click
        Response.Redirect("TipoProducto.aspx" & Orbelink.Helpers.CommonTasks.RecreateQueryString(codigo_pantalla, True))
    End Sub

End Class
