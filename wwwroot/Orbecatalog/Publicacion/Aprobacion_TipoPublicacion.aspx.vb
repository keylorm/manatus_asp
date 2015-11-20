Imports Orbelink.DBHandler
Imports Orbelink.Entity.Entidades
Imports Orbelink.Entity.Publicaciones

Partial Class _Aprobacion_TipoPublicacion
    Inherits PageBaseClass

    Const codigo_pantalla As String = "PU-06"
    Const level As Integer = 2

    'Pagina

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddScripts(level)


        securityHandler.VerifyPantalla(codigo_pantalla, level)
        securityHandler.VerifyActions(btn_Salvar, btn_Modificar, btn_Eliminar)
        If Not Page.IsPostBack Then
            lnk_Grupos_Entidades.NavigateUrl = "javascript:toggleLayer('" & trv_Grupos.ClientID & "', this)"
            selectGrupos(trv_Grupos)

            selectAprobacion_TipoPublicacion()
            selectTipoPublicaciones()
            selectTiposRelacion_Entidades()
            resetCampos(Configuration.EstadoPantalla.NORMAL)
        End If


    End Sub

    Protected Sub resetCampos(ByVal estado As Configuration.EstadoPantalla)

        Select Case estado
            Case Configuration.EstadoPantalla.NORMAL
                btn_Modificar.Visible = False

                btn_Salvar.Visible = True

                btn_Eliminar.Visible = False


                ddl_TipoPublicacion.ToolTip = ""
            Case Configuration.EstadoPantalla.CONSULTAR
                btn_Modificar.Visible = True

                btn_Salvar.Visible = False

                btn_Eliminar.Visible = True

        End Select
    End Sub

    'Aprobacion_TipoPublicacion
    Protected Sub selectAprobacion_TipoPublicacion()
        Dim dataSet As Data.DataSet
        Dim Aprobacion_TipoPublicacion As New Aprobacion_TipoPublicacion
        Dim Grupos_Entidades As New Grupos_Entidades
        Dim TipoPublicacion As New TipoPublicacion
        Dim TipoRelacion_Grupos As New TipoRelacion_Grupos

        Aprobacion_TipoPublicacion.Fields.SelectAll()
        TipoPublicacion.Nombre.ToSelect = True
        TipoPublicacion.Id_TipoPublicacion.ToSelect = True

        Grupos_Entidades.Id_Grupo.ToSelect = True
        Grupos_Entidades.Nombre.ToSelect = True

        'If Not ddl_Filtro_TipoPublicacion.SelectedIndex = ddl_Filtro_TipoPublicacion.Items.Count - 1 Then
        '    TipoPublicacion.Id_TipoPublicacion.Where.EqualCondition(ddl_Filtro_TipoPublicacion.SelectedValue)
        'End If

        TipoRelacion_Grupos.Nombre.ToSelect = True
        TipoRelacion_Grupos.Id_TipoRelacion.ToSelect = True
        queryBuilder.Join.EqualCondition(TipoRelacion_Grupos.Id_TipoRelacion, Aprobacion_TipoPublicacion.Id_TipoRelacion)

        queryBuilder.Join.EqualCondition(Aprobacion_TipoPublicacion.Id_TipoPublicacion, TipoPublicacion.Id_TipoPublicacion)
        queryBuilder.Join.EqualCondition(Grupos_Entidades.Id_Grupo, Aprobacion_TipoPublicacion.Id_Grupo)

        queryBuilder.Orderby.Add(TipoPublicacion.Nombre)
        queryBuilder.From.Add(TipoPublicacion)
        queryBuilder.From.Add(Grupos_Entidades)
        queryBuilder.From.Add(Aprobacion_TipoPublicacion)
        queryBuilder.From.Add(TipoRelacion_Grupos)
        dataSet = connection.executeSelect(queryBuilder.RelationalSelectQuery())

        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                dg_Aprobacion_TipoPublicacions.DataSource = dataSet
                dg_Aprobacion_TipoPublicacions.DataKeyField = TipoPublicacion.Id_TipoPublicacion.Name
                dg_Aprobacion_TipoPublicacions.DataBind()

                'Llena el grid
                Dim offset As Integer = dg_Aprobacion_TipoPublicacions.CurrentPageIndex * dg_Aprobacion_TipoPublicacions.PageSize
                Dim counter As Integer
                Dim results_Grupos_Entidadess As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), Grupos_Entidades)
                Dim resulTipoPublicacion As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), TipoPublicacion)
                Dim results_TipoRelacion As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), TipoRelacion_Grupos)

                For counter = 0 To dg_Aprobacion_TipoPublicacions.Items.Count - 1
                    Dim act_Grupos_Entidades As Grupos_Entidades = results_Grupos_Entidadess(offset + counter)
                    Dim act_TipoPublicacion As TipoPublicacion = resulTipoPublicacion(offset + counter)
                    Dim act_TipoRelacion_Grupos As TipoRelacion_Grupos = results_TipoRelacion(offset + counter)

                    Dim lbl_Grupos_Entidades As Label = dg_Aprobacion_TipoPublicacions.Items(counter).FindControl("lbl_Grupos_Entidades")
                    Dim lbl_TipoEntidad As Label = dg_Aprobacion_TipoPublicacions.Items(counter).FindControl("lbl_TipoEntidad")
                    Dim lbl_TipoPublicacion As Label = dg_Aprobacion_TipoPublicacions.Items(counter).FindControl("lbl_TipoPublicacion")
                    Dim lbl_TipoRelacion As Label = dg_Aprobacion_TipoPublicacions.Items(counter).FindControl("lbl_TipoRelacion")

                    lbl_Grupos_Entidades.Text = act_Grupos_Entidades.Nombre.Value
                    lbl_Grupos_Entidades.ToolTip = act_Grupos_Entidades.Id_Grupo.Value

                    lbl_TipoRelacion.Text = act_TipoRelacion_Grupos.Nombre.Value
                    lbl_TipoRelacion.ToolTip = act_TipoRelacion_Grupos.Id_TipoRelacion.Value

                    lbl_TipoPublicacion.Text = act_TipoPublicacion.Nombre.Value
                    lbl_TipoPublicacion.ToolTip = act_TipoPublicacion.Id_TipoPublicacion.Value
                Next
                dg_Aprobacion_TipoPublicacions.Visible = True
            Else
                dg_Aprobacion_TipoPublicacions.Visible = False
            End If
        End If
    End Sub

    Protected Sub loadAprobacion_TipoPublicacion(ByVal id_TipoEntidad As Integer, ByVal id_TipoPublicacion As Integer, ByVal id_TipoRelacion As Integer, ByVal Id_Grupo As Integer)
        Dim dataSet As Data.DataSet
        Dim Aprobacion_TipoPublicacion As New Aprobacion_TipoPublicacion
        Dim Grupos_Entidades As New Grupos_Entidades

        Grupos_Entidades.Id_Grupo.ToSelect = True
        Grupos_Entidades.Nombre.ToSelect = True
        queryBuilder.Join.EqualCondition(Grupos_Entidades.Id_Grupo, Aprobacion_TipoPublicacion.Id_Grupo)

        Aprobacion_TipoPublicacion.Fields.SelectAll()
        Aprobacion_TipoPublicacion.Id_TipoPublicacion.Where.EqualCondition(id_TipoPublicacion)
        Aprobacion_TipoPublicacion.Id_Grupo.Where.EqualCondition(Id_Grupo)

        queryBuilder.From.Add(Aprobacion_TipoPublicacion)
        queryBuilder.From.Add(Grupos_Entidades)
        dataSet = connection.executeSelect(queryBuilder.RelationalSelectQuery)

        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                ObjectBuilder.CreateObject(dataSet.Tables(0), 0, Aprobacion_TipoPublicacion)
                ObjectBuilder.CreateObject(dataSet.Tables(0), 0, Grupos_Entidades)

                ddl_TipoPublicacion.SelectedValue = Aprobacion_TipoPublicacion.Id_TipoPublicacion.Value
                ddl_TipoPublicacion.Attributes("id_TipoPublicacion") = Aprobacion_TipoPublicacion.Id_TipoPublicacion.Value

                ddl_tipoRelacion.SelectedValue = Aprobacion_TipoPublicacion.Id_TipoRelacion.Value
                ddl_TipoPublicacion.Attributes("Id_TipoRelacion") = Aprobacion_TipoPublicacion.Id_TipoRelacion.Value

                lnk_Grupos_Entidades.Text = Grupos_Entidades.Nombre.Value
                lnk_Grupos_Entidades.ToolTip = Grupos_Entidades.Id_Grupo.Value
                lnk_Grupos_Entidades.Attributes("Id_Grupo") = Grupos_Entidades.Id_Grupo.Value
            End If
        End If
    End Sub

    Protected Function insertAprobacion_TipoPublicacion() As Boolean
        Dim Aprobacion_TipoPublicacion As Aprobacion_TipoPublicacion
        Try
            Aprobacion_TipoPublicacion = New Aprobacion_TipoPublicacion(ddl_TipoPublicacion.SelectedValue, ddl_tipoRelacion.SelectedValue, lnk_Grupos_Entidades.ToolTip)
            connection.executeInsert(queryBuilder.InsertQuery(Aprobacion_TipoPublicacion))
            Return True
        Catch exc As Exception
            MyMaster.mostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    Protected Function updateAprobacion_TipoPublicacion(ByVal id_TipoPublicacion As Integer, ByVal id_TipoRelacion As Integer, ByVal Id_Grupo As Integer) As Boolean
        Try
            Dim Aprobacion_TipoPublicacion As New Aprobacion_TipoPublicacion(ddl_TipoPublicacion.SelectedValue, ddl_tipoRelacion.SelectedValue, lnk_Grupos_Entidades.ToolTip)
            Aprobacion_TipoPublicacion.Id_TipoPublicacion.Where.EqualCondition(id_TipoPublicacion)
            Aprobacion_TipoPublicacion.Id_Grupo.Where.EqualCondition(Id_Grupo)
            Aprobacion_TipoPublicacion.Id_TipoRelacion.Where.EqualCondition(id_TipoRelacion)
            connection.executeUpdate(queryBuilder.UpdateQuery(Aprobacion_TipoPublicacion))
            Return True
        Catch exc As Exception
            MyMaster.mostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    Protected Function deleteAprobacion_TipoPublicacion(ByVal id_TipoPublicacion As Integer, ByVal id_TipoRelacion As Integer, ByVal Id_Grupo As Integer) As Boolean
        Dim Aprobacion_TipoPublicacion As New Aprobacion_TipoPublicacion
        Try
            Aprobacion_TipoPublicacion.Id_TipoPublicacion.Where.EqualCondition(id_TipoPublicacion)
            Aprobacion_TipoPublicacion.Id_Grupo.Where.EqualCondition(Id_Grupo)
            Aprobacion_TipoPublicacion.Id_TipoRelacion.Where.EqualCondition(id_TipoRelacion)
            connection.executeDelete(queryBuilder.DeleteQuery(Aprobacion_TipoPublicacion))
            Return True
        Catch exc As Exception
            MyMaster.mostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    'TipoPublicacion
    Protected Sub selectTipoPublicaciones()
        Dim dataSet As Data.DataSet
        Dim TipoPublicacion As New TipoPublicacion
        Dim result As String

        TipoPublicacion.Id_TipoPublicacion.ToSelect = True
        TipoPublicacion.Nombre.ToSelect = True
        result = queryBuilder.SelectQuery(TipoPublicacion)
        dataSet = connection.executeSelect(result)
        If dataSet.Tables.Count > 0 Then
            ddl_TipoPublicacion.DataSource = dataSet
            ddl_TipoPublicacion.DataTextField = TipoPublicacion.Nombre.Name
            ddl_TipoPublicacion.DataValueField = TipoPublicacion.Id_TipoPublicacion.Name
            ddl_TipoPublicacion.DataBind()

            ddl_Filtro_TipoPublicacion.DataSource = dataSet
            ddl_Filtro_TipoPublicacion.DataTextField = TipoPublicacion.Nombre.Name
            ddl_Filtro_TipoPublicacion.DataValueField = TipoPublicacion.Id_TipoPublicacion.Name
            ddl_Filtro_TipoPublicacion.DataBind()
            ddl_Filtro_TipoPublicacion.Items.Add("-- Todos --")
            ddl_Filtro_TipoPublicacion.SelectedIndex = ddl_Filtro_TipoPublicacion.Items.Count - 1
        End If
    End Sub

    'TipoRelacion_Grupos
    Protected Sub selectTiposRelacion_Entidades()
        Dim ds As Data.DataSet
        Dim TipoRelacion_Grupos As New TipoRelacion_Grupos
        Dim result As String
        Try
            TipoRelacion_Grupos.Fields.SelectAll()
            result = queryBuilder.SelectQuery(TipoRelacion_Grupos)
            ds = connection.executeSelect(result)
            If ds.Tables.Count > 0 Then
                ddl_tipoRelacion.DataSource = ds
                ddl_tipoRelacion.DataTextField = TipoRelacion_Grupos.Nombre.Name
                ddl_tipoRelacion.DataValueField = TipoRelacion_Grupos.Id_TipoRelacion.Name
                ddl_tipoRelacion.DataBind()
            End If
        Catch exc As Exception
            MyMaster.mostrarMensaje(exc.Message, True)

        End Try
    End Sub

    'Grupos
    Protected Sub selectGrupos(ByVal theTreeView As TreeView)
        Dim Grupos_Entidades As New Grupos_Entidades
        Dim dataset As Data.DataSet
        Dim counter As Integer

        Grupos_Entidades.Fields.SelectAll()
        Grupos_Entidades.Id_Grupo.Where.EqualCondition_OnSameTable(Grupos_Entidades.Id_padre)
        queryBuilder.Orderby.Add(Grupos_Entidades.Nombre)

        dataset = connection.executeSelect(queryBuilder.SelectQuery(Grupos_Entidades))
        If dataset.Tables(0).Rows.Count > 0 Then
            Dim results_Grupos As ArrayList = ObjectBuilder.TransformDataTable(dataset.Tables(0), Grupos_Entidades)
            For counter = 0 To dataset.Tables(0).Rows.Count - 1
                Dim act_ubicacion As Grupos_Entidades = results_Grupos(counter)
                Dim hijo As New TreeNode(act_ubicacion.Nombre.Value, act_ubicacion.Id_Grupo.Value)
                theTreeView.Nodes.Add(hijo)
                selectGruposHijos(theTreeView.Nodes(counter))
            Next
        End If
    End Sub

    Protected Sub selectGruposHijos(ByVal elTreeNode As TreeNode)
        Dim padre_Id_Grupo_Producto As Integer = elTreeNode.Value
        Dim Grupos_Entidades As New Grupos_Entidades
        Dim dataset As Data.DataSet
        Dim counter As Integer

        Grupos_Entidades.Fields.SelectAll()
        Grupos_Entidades.Id_padre.Where.EqualCondition(padre_Id_Grupo_Producto)
        Grupos_Entidades.Id_Grupo.Where.DiferentCondition(padre_Id_Grupo_Producto)
        queryBuilder.Orderby.Add(Grupos_Entidades.Nombre)
        dataset = connection.executeSelect(queryBuilder.SelectQuery(Grupos_Entidades))

        If dataset.Tables(0).Rows.Count > 0 Then
            Dim results_Grupos As ArrayList = ObjectBuilder.TransformDataTable(dataset.Tables(0), Grupos_Entidades)
            For counter = 0 To dataset.Tables(0).Rows.Count - 1
                Dim act_Grupo As Grupos_Entidades = results_Grupos(counter)
                Dim hijo As New TreeNode(act_Grupo.Nombre.Value, act_Grupo.Id_Grupo.Value)
                selectGruposHijos(hijo)
                elTreeNode.ChildNodes.Add(hijo)
            Next
        End If
    End Sub

    'Eventos
    Protected Sub btn_Salvar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Salvar.Click

        If lnk_Grupos_Entidades.ToolTip.Length = 0 Then
            vld_Grupo.IsValid = False
            MyMaster.mostrarMensaje("Debe seleccionar un tipo de producto.", False)
        End If

        If IsValid() Then
            If insertAprobacion_TipoPublicacion() Then
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Salvar, "Aprobacion", False)
                resetCampos(Configuration.EstadoPantalla.NORMAL)
                selectAprobacion_TipoPublicacion()
            Else
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Salvar, "aprobacion", True)
            End If
        End If
    End Sub

    Protected Sub btn_Modificar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Modificar.Click

        If IsValid() Then
            If updateAprobacion_TipoPublicacion(ddl_TipoPublicacion.Attributes("id_TipoPublicacion"), ddl_TipoPublicacion.Attributes("Id_TipoRelacion"), lnk_Grupos_Entidades.Attributes("id_Grupo")) Then
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Modificar, "Aprobacion", False)
                resetCampos(Configuration.EstadoPantalla.NORMAL)
                selectAprobacion_TipoPublicacion()
            Else
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Modificar, "la aprobacion", True)
            End If

        End If
    End Sub

    Protected Sub btn_Cancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Cancelar.Click
        resetCampos(Configuration.EstadoPantalla.NORMAL)
        ddl_TipoPublicacion.Enabled = True
    End Sub

    Protected Sub btn_Eliminar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Eliminar.Click

        If deleteAprobacion_TipoPublicacion(ddl_TipoPublicacion.Attributes("id_TipoPublicacion"), ddl_TipoPublicacion.Attributes("Id_TipoRelacion"), lnk_Grupos_Entidades.Attributes("id_Grupo")) Then
            MyMaster.mostrarMensaje("Aprobacion eliminada exitosamente.", False)
            resetCampos(Configuration.EstadoPantalla.NORMAL)
            selectAprobacion_TipoPublicacion()
        Else
            MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Eliminar, "la aprobacion", True)
        End If
    End Sub

    Protected Sub dg_Aprobacion_TipoPublicacions_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dg_Aprobacion_TipoPublicacions.EditCommand
        Dim indice As Integer = e.Item.ItemIndex
        Dim lbl_Grupos_Entidades As Label = dg_Aprobacion_TipoPublicacions.Items(indice).FindControl("lbl_Grupos_Entidades")
        Dim lbl_TipoEntidad As Label = dg_Aprobacion_TipoPublicacions.Items(indice).FindControl("lbl_TipoEntidad")
        Dim lbl_TipoPublicacion As Label = dg_Aprobacion_TipoPublicacions.Items(indice).FindControl("lbl_TipoPublicacion")
        Dim lbl_TipoRelacion As Label = dg_Aprobacion_TipoPublicacions.Items(indice).FindControl("lbl_TipoRelacion")

        resetCampos(Configuration.EstadoPantalla.CONSULTAR)
        loadAprobacion_TipoPublicacion(lbl_TipoEntidad.ToolTip, lbl_TipoPublicacion.ToolTip, lbl_TipoRelacion.ToolTip, lbl_Grupos_Entidades.ToolTip)
    End Sub

    Protected Sub btn_Buscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Buscar.Click
        dg_Aprobacion_TipoPublicacions.CurrentPageIndex = 0
        resetCampos(Configuration.EstadoPantalla.NORMAL)
        selectAprobacion_TipoPublicacion()
    End Sub

    Protected Sub dg_Aprobacion_TipoPublicacions_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dg_Aprobacion_TipoPublicacions.PageIndexChanged
        dg_Aprobacion_TipoPublicacions.CurrentPageIndex = e.NewPageIndex
        selectAprobacion_TipoPublicacion()
    End Sub

    Protected Sub btn_AgregarTipoRelacion_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_AgregarTipoRelacion.Click
        Response.Redirect("../Contactos/TipoRelacion.aspx" & Orbelink.Helpers.CommonTasks.RecreateQueryString(codigo_pantalla, True))
    End Sub

    Protected Sub btn_AgregarTipoPublicacion_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_AgregarTipoPublicacion.Click
        Response.Redirect("TipoPublicacion.aspx" & Orbelink.Helpers.CommonTasks.RecreateQueryString(codigo_pantalla, True))
    End Sub

    Protected Sub trv_Grupos_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles trv_Grupos.SelectedNodeChanged
        lnk_Grupos_Entidades.Text = trv_Grupos.SelectedNode.Text
        lnk_Grupos_Entidades.ToolTip = trv_Grupos.SelectedNode.Value
    End Sub

End Class

