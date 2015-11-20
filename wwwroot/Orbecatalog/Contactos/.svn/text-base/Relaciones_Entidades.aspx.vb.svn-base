Imports Orbelink.DBHandler
Imports Orbelink.Entity.Entidades

Partial Class _Relacion_Entidades
    Inherits PageBaseClass

    Const codigo_pantalla As String = "CO-08"
    Const level As Integer = 2

    Dim entidadHandler As Orbelink.Control.Entidades.EntidadHandler

    'Pagina
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddScripts(level)

        securityHandler.VerifyPantalla(codigo_pantalla, level)
        entidadHandler = New Orbelink.Control.Entidades.EntidadHandler(Configuration.Config_DefaultConnectionString)

        If Not IsPostBack Then

            dg_Relacion_Entidades.Attributes.Add("SelectedIndex", -1)
            dg_Relacion_Entidades.Attributes.Add("SelectedPage", -1)
            ClearInfo(Configuration.EstadoPantalla.NORMAL, False)
            securityHandler.VerifyActions(btn_Salvar, btn_Modificar, btn_Eliminar)
            SetThemeProperties()

            selectTiposRelacion_Entidades()
            selectTiposEntidades()
            If ddl_TipoEntidad_Base.Items.Count > 0 Then
                entidadHandler.selectEntidades_DDL(ddl_EntidadBase, ddl_TipoEntidad_Base.SelectedValue)
                entidadHandler.selectEntidades_DDL(ddl_EntidadDependiente, ddl_TipoEntidad_Dependiente.SelectedValue)
            End If

            selectRelacion_Entidades(0)

            Dim Id_Relacion As String = Request.QueryString("Id_Relacion")
            If Not Id_Relacion Is Nothing Then
                If loadRelacion_Entidades(Request.QueryString("Id_Relacion")) Then
                    ClearInfo(Configuration.EstadoPantalla.CONSULTAR, False)
                End If
            End If
        End If
    End Sub

    Protected Sub ClearInfo(ByVal estado As Configuration.EstadoPantalla, ByVal clearInfo As Boolean)
        If clearInfo Then
            tbx_ComentariosRelaciones.ToolTip = ""
            tbx_ComentariosRelaciones.Text = ""
            Me.ClearDataGrid(dg_Relacion_Entidades, clearInfo)
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

    Protected Sub SetThemeProperties()




    End Sub

    'Relacion_Entidades
    Protected Sub selectRelacion_Entidades(ByVal Id_entidad As Integer)
        Dim ds1 As Data.DataSet
        Dim ds2 As Data.DataSet
        Dim Relacion_Entidades As New Relacion_Entidades
        Dim entidad As New Entidad
        Dim TipoRelacion_Entidades As New TipoRelacion_Entidades

        If ddl_Filtro_TipoRelaciones.Items.Count > 0 Then
            'TipoRelacion
            TipoRelacion_Entidades.NombreBaseDependiente.ToSelect = True
            queryBuilder.Join.EqualCondition(TipoRelacion_Entidades.Id_TipoRelacion, Relacion_Entidades.Id_TipoRelacion)

            'entidades
            entidad.NombreDisplay.ToSelect = True
            entidad.Id_entidad.ToSelect = True

            'Relacion_Entidades
            queryBuilder.Join.EqualCondition(entidad.Id_entidad, Relacion_Entidades.Id_EntidadBase)
            Relacion_Entidades.Fields.SelectAll()
            If Id_entidad <> 0 Then
                Relacion_Entidades.Id_EntidadBase.Where.EqualCondition(Id_entidad, Where.FieldRelations.AND_)
                Relacion_Entidades.Id_EntidadDependiente.Where.EqualCondition(Id_entidad, Where.FieldRelations.OR_)
                queryBuilder.GroupFieldsConditions(Relacion_Entidades.Id_EntidadBase, Relacion_Entidades.Id_EntidadDependiente)
            End If

            'Busca si hay filtro
            If Not ddl_Filtro_TipoRelaciones.SelectedIndex = ddl_Filtro_TipoRelaciones.Items.Count - 1 Then
                Relacion_Entidades.Id_TipoRelacion.Where.EqualCondition(ddl_Filtro_TipoRelaciones.SelectedValue)
            End If

            'Busca si hay filtro
            If Not ddl_Filtro_TipoEntidades.SelectedIndex = ddl_Filtro_TipoEntidades.Items.Count - 1 Then
                entidad.Id_tipoEntidad.Where.EqualCondition(ddl_Filtro_TipoEntidades.SelectedValue)
            End If

            'Select de entidades base
            queryBuilder.From.Add(Relacion_Entidades)
            queryBuilder.From.Add(entidad)
            queryBuilder.From.Add(TipoRelacion_Entidades)

            ds1 = connection.executeSelect(queryBuilder.RelationalSelectQuery)

            If ds1.Tables.Count > 0 Then
                If ds1.Tables(0).Rows.Count > 0 Then
                    dg_Relacion_Entidades.DataSource = ds1
                    dg_Relacion_Entidades.DataKeyField = Relacion_Entidades.Id_Relacion.Name
                    dg_Relacion_Entidades.DataBind()

                    Dim results As ArrayList = ObjectBuilder.TransformDataTable(ds1.Tables(0), Relacion_Entidades)

                    'Llena el grid
                    Dim offset As Integer = dg_Relacion_Entidades.CurrentPageIndex * dg_Relacion_Entidades.PageSize
                    Dim counter As Integer
                    For counter = 0 To dg_Relacion_Entidades.Items.Count - 1
                        Dim actual As Relacion_Entidades = results(counter)

                        'Select de entidades dependiente
                        entidad.Fields.ClearAll()
                        entidad.Id_entidad.Where.EqualCondition(actual.Id_EntidadDependiente.Value)
                        entidad.Id_entidad.ToSelect = True
                        entidad.NombreDisplay.ToSelect = True
                        ds2 = connection.executeSelect(queryBuilder.SelectQuery(entidad))

                        Dim lbl_Relacion_Entidades As Label = dg_Relacion_Entidades.Items(counter).FindControl("lbl_Relacion_Entidades")
                        lbl_Relacion_Entidades.Text = ds2.Tables(0).Rows(0).Item(entidad.NombreDisplay.Name)
                        lbl_Relacion_Entidades.Text &= " es "
                        lbl_Relacion_Entidades.Text &= ds1.Tables(0).Rows(offset + counter).Item(TipoRelacion_Entidades.NombreBaseDependiente.Name)
                        lbl_Relacion_Entidades.Text &= " de "
                        lbl_Relacion_Entidades.Text &= ds1.Tables(0).Rows(offset + counter).Item(entidad.NombreDisplay.Name)
                    Next
                    lbl_ResultadoElementos.Visible = False
                    dg_Relacion_Entidades.Visible = True
                Else
                    lbl_ResultadoElementos.Visible = True
                    dg_Relacion_Entidades.Visible = False
                End If
            End If
        End If
    End Sub

    Protected Function loadRelacion_Entidades(ByVal Id_Relacion As Integer) As Boolean
        Dim dataSet As Data.DataSet
        Dim relacion_entidades As New Relacion_Entidades
        Dim entidad As New Entidad

        relacion_entidades.Fields.SelectAll()
        relacion_entidades.Id_Relacion.Where.EqualCondition(Id_Relacion)

        entidad.NombreDisplay.ToSelect = True
        entidad.Id_entidad.ToSelect = True
        entidad.Id_tipoEntidad.ToSelect = True
        queryBuilder.Join.EqualCondition(entidad.Id_entidad, relacion_entidades.Id_EntidadBase)
        queryBuilder.From.Add(relacion_entidades)
        queryBuilder.From.Add(entidad)
        dataSet = connection.executeSelect(queryBuilder.RelationalSelectQuery())

        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                ObjectBuilder.CreateObject(dataSet.Tables(0), 0, entidad)
                ObjectBuilder.CreateObject(dataSet.Tables(0), 0, relacion_entidades)

                ddl_TipoEntidad_Base.SelectedValue = entidad.Id_tipoEntidad.Value
                entidadHandler.selectEntidades_DDL(ddl_EntidadBase, ddl_TipoEntidad_Base.SelectedValue)
                ddl_EntidadBase.SelectedValue = entidad.Id_entidad.Value

                tbx_ComentariosRelaciones.ToolTip = relacion_entidades.Id_Relacion.Value
                tbx_ComentariosRelaciones.Text = relacion_entidades.Comentarios.Value
                ddl_TipoRelacion_Entidades.SelectedValue = relacion_entidades.Id_TipoRelacion.Value
                Dim id_entidadDependiente As Integer = relacion_entidades.Id_EntidadDependiente.Value

                'Select de entidad dependiente
                dataSet.Tables.Clear()
                entidad.Fields.ClearAll()
                entidad.Id_entidad.ToSelect = True
                entidad.Id_tipoEntidad.ToSelect = True
                entidad.Id_entidad.Where.EqualCondition(id_entidadDependiente)
                queryBuilder.From.Add(entidad)
                dataSet = connection.executeSelect(queryBuilder.RelationalSelectQuery())

                If dataSet.Tables.Count > 0 Then
                    ObjectBuilder.CreateObject(dataSet.Tables(0), 0, entidad)
                    ddl_TipoEntidad_Dependiente.SelectedValue = entidad.Id_tipoEntidad.Value
                    entidadHandler.selectEntidades_DDL(ddl_EntidadDependiente, ddl_TipoEntidad_Dependiente.SelectedValue)
                    ddl_EntidadDependiente.SelectedValue = entidad.Id_entidad.Value
                End If
                Return True
            End If
        End If
        Return False
    End Function

    Protected Function insertRelacion_Entidades() As Boolean
        Dim Relacion_Entidades As Relacion_Entidades
        Try
            Relacion_Entidades = New Relacion_Entidades(ddl_TipoRelacion_Entidades.SelectedValue, ddl_EntidadBase.SelectedValue, _
                ddl_EntidadDependiente.SelectedValue, tbx_ComentariosRelaciones.Text)
            connection.executeInsert(queryBuilder.InsertQuery(Relacion_Entidades))
            Return True
        Catch exc As Exception
            MyMaster.mostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    Protected Function updateRelacion_Entidades() As Boolean
        Dim Relacion_Entidades As Relacion_Entidades
        Try
            Relacion_Entidades = New Relacion_Entidades(ddl_TipoRelacion_Entidades.SelectedValue, ddl_EntidadBase.SelectedValue, _
                ddl_EntidadDependiente.SelectedValue, tbx_ComentariosRelaciones.Text)
            Relacion_Entidades.Id_Relacion.Where.EqualCondition(tbx_ComentariosRelaciones.ToolTip)
            connection.executeUpdate(queryBuilder.UpdateQuery(Relacion_Entidades))
            Return True
        Catch exc As Exception
            MyMaster.mostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    Protected Function deleteRelacion_Entidades() As Boolean
        Dim Relacion_Entidades As New Relacion_Entidades
        Try
            Relacion_Entidades.Id_Relacion.Where.EqualCondition(tbx_ComentariosRelaciones.ToolTip)
            connection.executeDelete(queryBuilder.DeleteQuery(Relacion_Entidades))
            Return True
        Catch exc As Exception
            MyMaster.mostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    Protected Sub selectTiposRelacion_Entidades()
        Dim ds As Data.DataSet
        Dim TipoRelacion_Entidades As New TipoRelacion_Entidades

        TipoRelacion_Entidades.Fields.SelectAll()
        ds = connection.executeSelect(queryBuilder.SelectQuery(TipoRelacion_Entidades))
        If ds.Tables.Count > 0 Then
            If ds.Tables.Count > 0 Then
                ddl_TipoRelacion_Entidades.DataSource = ds
                ddl_TipoRelacion_Entidades.DataTextField = TipoRelacion_Entidades.NombreBaseDependiente.Name
                ddl_TipoRelacion_Entidades.DataValueField = TipoRelacion_Entidades.Id_TipoRelacion.Name
                ddl_TipoRelacion_Entidades.DataBind()

                ddl_Filtro_TipoRelaciones.DataSource = ds
                ddl_Filtro_TipoRelaciones.DataTextField = TipoRelacion_Entidades.NombreBaseDependiente.Name
                ddl_Filtro_TipoRelaciones.DataValueField = TipoRelacion_Entidades.Id_TipoRelacion.Name
                ddl_Filtro_TipoRelaciones.DataBind()
            End If
        End If
        ddl_Filtro_TipoRelaciones.Items.Add("-- Todos --")
        ddl_Filtro_TipoRelaciones.SelectedIndex = ddl_Filtro_TipoRelaciones.Items.Count - 1
    End Sub

    'TipoEntidades
    Protected Sub selectTiposEntidades()
        Dim ds As Data.DataSet
        Dim TipoEntidad As New TipoEntidad

        TipoEntidad.Fields.SelectAll()
        ds = connection.executeSelect(queryBuilder.SelectQuery(TipoEntidad))
        If ds.Tables.Count > 0 Then
            If ds.Tables(0).Rows.Count > 0 Then
                ddl_TipoEntidad_Base.DataSource = ds
                ddl_TipoEntidad_Base.DataTextField = TipoEntidad.Nombre.Name
                ddl_TipoEntidad_Base.DataValueField = TipoEntidad.Id_TipoEntidad.Name
                ddl_TipoEntidad_Base.DataBind()

                ddl_TipoEntidad_Dependiente.DataSource = ds
                ddl_TipoEntidad_Dependiente.DataTextField = TipoEntidad.Nombre.Name
                ddl_TipoEntidad_Dependiente.DataValueField = TipoEntidad.Id_TipoEntidad.Name
                ddl_TipoEntidad_Dependiente.DataBind()

                ddl_Filtro_TipoEntidades.DataSource = ds
                ddl_Filtro_TipoEntidades.DataTextField = TipoEntidad.Nombre.Name
                ddl_Filtro_TipoEntidades.DataValueField = TipoEntidad.Id_TipoEntidad.Name
                ddl_Filtro_TipoEntidades.DataBind()
            End If
        End If
        ddl_Filtro_TipoEntidades.Items.Add("-- Todos --")
        ddl_Filtro_TipoEntidades.SelectedIndex = ddl_Filtro_TipoEntidades.Items.Count - 1
    End Sub

    'Eventos
    Protected Sub btn_Salvar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Salvar.Click

        If IsValid() Then
            If insertRelacion_Entidades() Then
                selectRelacion_Entidades(0)
                ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Salvar, "Relacion", False)
            Else
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Salvar, "la relacion", True)
            End If
        Else
            MyMaster.MostrarMensaje(Resources.Orbecatalog_Resources.CamposRequeridos, False)
        End If
        upd_Busqueda.Update()
        upd_Contenido.Update()
    End Sub

    Protected Sub btn_Modificar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Modificar.Click

        If IsValid() Then
            If updateRelacion_Entidades() Then
                selectRelacion_Entidades(0)
                ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Modificar, "Relacion", False)
            Else
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Modificar, "relacion", True)
            End If
        Else
            MyMaster.MostrarMensaje(Resources.Orbecatalog_Resources.CamposRequeridos, False)
        End If
        upd_Busqueda.Update()
        upd_Contenido.Update()
    End Sub

    Protected Sub btn_Cancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Cancelar.Click
        ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
    End Sub

    Protected Sub btn_Eliminar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Eliminar.Click

        If deleteRelacion_Entidades() Then
            'Ojo
            If dg_Relacion_Entidades.Items.Count = 1 Then
                If dg_Relacion_Entidades.CurrentPageIndex > 0 Then
                    dg_Relacion_Entidades.CurrentPageIndex -= 1
                Else
                    dg_Relacion_Entidades.CurrentPageIndex = 0
                End If
            End If

            selectRelacion_Entidades(0)
            ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
            MyMaster.mostrarMensaje("Relacion eliminada exitosamente.", False)
        Else
            MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Eliminar, "relacion", True)
        End If
        upd_Busqueda.Update()
        upd_Contenido.Update()
    End Sub

    Protected Sub btn_Buscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Buscar.Click
        dg_Relacion_Entidades.CurrentPageIndex = 0
        selectRelacion_Entidades(0)
    End Sub

    Protected Sub ddl_TipoEntidad_Base_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddl_TipoEntidad_Base.SelectedIndexChanged
        entidadHandler.selectEntidades_DDL(ddl_EntidadBase, ddl_TipoEntidad_Base.SelectedValue)
    End Sub

    Protected Sub ddl_TipoEntidad_Dependiente_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddl_TipoEntidad_Dependiente.SelectedIndexChanged
        entidadHandler.selectEntidades_DDL(ddl_EntidadDependiente, ddl_TipoEntidad_Dependiente.SelectedValue)
    End Sub

    Protected Sub dg_Relacion_Entidades_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dg_Relacion_Entidades.EditCommand
        ClearInfo(Configuration.EstadoPantalla.CONSULTAR, True)
        Me.EditCommandDataGrid(dg_Relacion_Entidades, e.Item.ItemIndex)
        loadRelacion_Entidades(dg_Relacion_Entidades.DataKeys(e.Item.ItemIndex))
        upd_Contenido.Update()
        upd_Busqueda.Update()
    End Sub

    Protected Sub dg_Relacion_Entidades_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dg_Relacion_Entidades.PageIndexChanged
        dg_Relacion_Entidades.CurrentPageIndex = e.NewPageIndex
        selectRelacion_Entidades(0)
        Me.PageIndexChange(dg_Relacion_Entidades)
        upd_Busqueda.Update()
    End Sub

    'Agregar
    Protected Sub btn_AgregarTipoRelacion_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_AgregarTipoRelacion.Click
        Response.Redirect("TipoRelacion.aspx" & Orbelink.Helpers.CommonTasks.RecreateQueryString(codigo_pantalla, True))
    End Sub

    Protected Sub btn_AgregarTipoEntidad_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_AgregarTipoEntidad.Click
        Response.Redirect("TipoEntidad.aspx" & Orbelink.Helpers.CommonTasks.RecreateQueryString(codigo_pantalla, True))
    End Sub

End Class
