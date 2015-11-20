Imports Orbelink.DBHandler
Imports Orbelink.Entity.Entidades
Imports Orbelink.Entity.Publicaciones

Partial Class _Aprobacion_Publicacion
    Inherits PageBaseClass

    Const codigo_pantalla As String = "PU-07"
    Const level As Integer = 2

    'Pagina

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddScripts(level)
        securityHandler.VerifyPantalla(codigo_pantalla, level)

        If Not Page.IsPostBack Then

            dg_Publicaciones.Attributes.Add("SelectedIndex", -1)
            dg_Publicaciones.Attributes.Add("SelectedPage", -1)

            selectTiposPublicaciones()
            selectPublicaciones()
            If Request.Params.Item("id_Publicacion") > 0 Then
                loadPublicacion(Request.Params.Item("id_Publicacion"))
                selectAprobacion_TipoPublicacion(Request.Params.Item("id_Publicacion"))
                ClearInfo(Configuration.EstadoPantalla.CONSULTAR, False)
            Else
                ClearInfo(Configuration.EstadoPantalla.NORMAL, False)
            End If

        End If
    End Sub

    Protected Sub ClearInfo(ByVal estado As Configuration.EstadoPantalla, ByVal clearInfo As Boolean)
        If clearInfo Then
            Me.ClearDataGrid(dg_Publicaciones, clearInfo)
            For counter As Integer = 0 To dg_Relaciones.Items.Count - 1
                Dim lbl_Entidad As Label = dg_Relaciones.Items(counter).FindControl("lbl_Entidad")
                Dim lbl_Aprobar As Label = dg_Relaciones.Items(counter).FindControl("lbl_Aprobar")
                Dim chk_Aprobado As CheckBox = dg_Relaciones.Items(counter).FindControl("chk_Aprobado")
                Dim tbx_Comentario As TextBox = dg_Relaciones.Items(counter).FindControl("tbx_Comentario")

                chk_Aprobado.Checked = False
                chk_Aprobado.Enabled = False
                chk_Aprobado.ToolTip = ""

                tbx_Comentario.Text = ""

                lbl_Aprobar.Text = "debe aprobar "

                lbl_Entidad.ToolTip = ""
                lbl_Entidad.Text = "Alguien "
            Next
        End If

        Select Case estado
            Case Configuration.EstadoPantalla.NORMAL
                pnl_Aprobacion_Publicacion.Visible = False

            Case Configuration.EstadoPantalla.CONSULTAR
                pnl_Aprobacion_Publicacion.Visible = True
        End Select
    End Sub

    'Publicaciones
    Protected Sub selectPublicaciones()
        Dim dataSet As Data.DataSet
        Dim entidad As New Entidad
        Dim Publicacion As New Publicacion

        Publicacion.Titulo.ToSelect = True
        Publicacion.Id_Publicacion.ToSelect = True
        Publicacion.Aprobada.Where.EqualCondition(0)

        entidad.NombreDisplay.ToSelect = True
        entidad.Id_entidad.ToSelect = True
        queryBuilder.Join.EqualCondition(entidad.Id_entidad, (Publicacion.id_Entidad))

        If ddl_Filtro_TipoPublicacion.SelectedIndex <> ddl_Filtro_TipoPublicacion.Items.Count - 1 Then
            Publicacion.Id_Publicacion.Where.EqualCondition(ddl_Filtro_TipoPublicacion.SelectedValue)
        End If

        queryBuilder.From.Add(Publicacion)
        queryBuilder.From.Add(entidad)
        dataSet = connection.executeSelect(queryBuilder.RelationalSelectQuery())

        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                dg_Publicaciones.DataSource = dataSet
                dg_Publicaciones.DataKeyField = Publicacion.Id_Publicacion.Name
                dg_Publicaciones.DataBind()

                'Llena el grid
                Dim offset As Integer = dg_Publicaciones.CurrentPageIndex * dg_Publicaciones.PageSize
                Dim counter As Integer

                Dim resulEntidad As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), entidad)
                Dim resulPublicacion As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), Publicacion)
                For counter = 0 To dg_Publicaciones.Items.Count - 1
                    Dim act_Entidad As Entidad = resulEntidad(offset + counter)
                    Dim act_Publicacion As Publicacion = resulPublicacion(offset + counter)

                    Dim lbl_Publicacion As Label = dg_Publicaciones.Items(counter).FindControl("lbl_Publicacion")
                    Dim lbl_Autor As Label = dg_Publicaciones.Items(counter).FindControl("lbl_Autor")

                    lbl_Publicacion.Text = act_Publicacion.Titulo.Value
                    lbl_Autor.Text = act_Entidad.NombreDisplay.Value
                    lbl_Autor.ToolTip = act_Entidad.Id_entidad.Value

                    'Javascript
                    dg_Publicaciones.Items(counter).Attributes.Add("onmouseover", "setRowFocus(this, " & counter & ", false)")
                    dg_Publicaciones.Items(counter).Attributes.Add("onmouseout", "unsetRowFocus(this, " & counter & ", false)")
                Next
                lbl_ResultadoElementos.Text = contadorElementos(offset, dg_Publicaciones.PageSize, dataSet.Tables(0).Rows.Count)
                dg_Publicaciones.Visible = True
            Else
                lbl_ResultadoElementos.Text = contadorElementos(0, 0, 0)
                dg_Publicaciones.Visible = False
            End If
        End If
    End Sub

    Protected Sub loadPublicacion(ByVal id_Publicacion As Integer)
        Dim dataSet As Data.DataSet
        Dim Publicacion As New Publicacion
        Dim entidad As New Entidad

        entidad.NombreDisplay.ToSelect = True
        entidad.Id_entidad.ToSelect = True
        Publicacion.Fields.SelectAll()
        Publicacion.Id_Publicacion.Where.EqualCondition(id_Publicacion)
        queryBuilder.Join.EqualCondition(entidad.Id_entidad, (Publicacion.id_Entidad))

        queryBuilder.From.Add(Publicacion)
        queryBuilder.From.Add(entidad)

        dataSet = connection.executeSelect(queryBuilder.RelationalSelectQuery)

        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                ObjectBuilder.CreateObject(dataSet.Tables(0), 0, entidad)
                ObjectBuilder.CreateObject(dataSet.Tables(0), 0, Publicacion)

                'Carga text boxs
                lnk_Publicacion.Text = Publicacion.Titulo.Value
                lnk_Publicacion.ToolTip = Publicacion.Id_Publicacion.Value
                lnk_Publicacion.NavigateUrl = "Publicaciones.aspx?id_Publicacion=" & Publicacion.Id_Publicacion.Value
                lbl_Corta.Text = Publicacion.Corta.Value
                lbl_Autor.Text = entidad.NombreDisplay.Value
            End If
        End If
    End Sub

    'Tipos Publicaciones
    Protected Sub selectTiposPublicaciones()
        Dim dataset As Data.DataSet
        Dim TipoPublicacion As New TipoPublicacion

        TipoPublicacion.Fields.SelectAll()
        dataset = connection.executeSelect(queryBuilder.SelectQuery(TipoPublicacion))
        If dataset.Tables.Count > 0 Then
            If dataset.Tables(0).Rows.Count > 0 Then
                ddl_Filtro_TipoPublicacion.DataSource = dataset
                ddl_Filtro_TipoPublicacion.DataTextField = TipoPublicacion.Nombre.Name
                ddl_Filtro_TipoPublicacion.DataValueField = TipoPublicacion.Id_TipoPublicacion.Name
                ddl_Filtro_TipoPublicacion.DataBind()
            End If
        End If
        ddl_Filtro_TipoPublicacion.Items.Add("-- Todos --")
        ddl_Filtro_TipoPublicacion.SelectedIndex = ddl_Filtro_TipoPublicacion.Items.Count - 1
    End Sub

    'Relaciones
    Protected Sub selectAprobacion_TipoPublicacion(ByVal id_Publicacion As Integer)
        Dim dataSet As Data.DataSet
        Dim Grupos_Entidades As New Grupos_Entidades
        Dim Publicacion As New Publicacion
        Dim Aprobacion_TipoPublicacion As New Aprobacion_TipoPublicacion
        Dim TipoRelacion_Grupos As New TipoRelacion_Grupos

        Grupos_Entidades.Nombre.ToSelect = True
        queryBuilder.Join.EqualCondition(Grupos_Entidades.Id_Grupo, (Aprobacion_TipoPublicacion.Id_Grupo))
        Grupos_Entidades.Id_Grupo.ToSelect = True

        Publicacion.Id_Publicacion.Where.EqualCondition(id_Publicacion)
        Publicacion.Id_tipoPublicacion.ToSelect = True

        queryBuilder.Join.EqualCondition(Aprobacion_TipoPublicacion.Id_TipoPublicacion, Publicacion.Id_tipoPublicacion)

        TipoRelacion_Grupos.Nombre.ToSelect = True
        TipoRelacion_Grupos.Id_TipoRelacion.ToSelect = True
        queryBuilder.Join.EqualCondition(TipoRelacion_Grupos.Id_TipoRelacion, Aprobacion_TipoPublicacion.Id_TipoRelacion)

        queryBuilder.From.Add(Aprobacion_TipoPublicacion)
        queryBuilder.From.Add(Grupos_Entidades)
        queryBuilder.From.Add(Publicacion)
        queryBuilder.From.Add(TipoRelacion_Grupos)
        queryBuilder.Distinct = True
        dataSet = connection.executeSelect(queryBuilder.RelationalSelectQuery)

        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                dg_Relaciones.DataSource = dataSet
                dg_Relaciones.DataBind()

                'Llena el DataGrid
                Dim counter As Integer
                Dim results_Grupos As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), Grupos_Entidades)
                Dim results_TipoRelacion As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), TipoRelacion_Grupos)

                For counter = 0 To dataSet.Tables(0).Rows.Count - 1
                    Dim act_Grupo As Grupos_Entidades = results_Grupos(counter)
                    Dim act_TipoRelacion_Grupos As TipoRelacion_Grupos = results_TipoRelacion(counter)

                    Dim lbl_Grupo As Label = dg_Relaciones.Items(counter).FindControl("lbl_Grupo")
                    Dim lbl_Entidad As Label = dg_Relaciones.Items(counter).FindControl("lbl_Entidad")
                    Dim lbl_TipoRelacion As Label = dg_Relaciones.Items(counter).FindControl("lbl_TipoRelacion")
                    Dim lbl_Aprobar As Label = dg_Relaciones.Items(counter).FindControl("lbl_Aprobar")
                    Dim chk_Aprobado As CheckBox = dg_Relaciones.Items(counter).FindControl("chk_Aprobado")
                    Dim tbx_Comentario As TextBox = dg_Relaciones.Items(counter).FindControl("tbx_Comentario")

                    lbl_Grupo.Text = act_Grupo.Nombre.Value
                    lbl_Grupo.ToolTip = act_Grupo.Id_Grupo.Value


                    lbl_TipoRelacion.Text = act_TipoRelacion_Grupos.Nombre.Value
                    lbl_TipoRelacion.ToolTip = act_TipoRelacion_Grupos.Id_TipoRelacion.Value

                    lbl_Aprobar.Text = "debe aprobar "
                    lbl_Entidad.Text = "Alguien "
                    tbx_Comentario.Enabled = False

                    chk_Aprobado.Enabled = False
                    chk_Aprobado.Checked = False
                    chk_Aprobado.ToolTip = ""

                    loadAprobacion_Publicacion(id_Publicacion, act_Grupo.Id_Grupo.Value, counter)
                Next
                dg_Relaciones.Visible = True
            Else
                dg_Relaciones.Visible = False
            End If
        End If
    End Sub

    'Aprobacion_Publicacion
    Protected Sub loadAprobacion_Publicacion(ByVal id_Publicacion As Integer, ByVal id_Grupo As Integer, ByVal indice As Integer)
        Dim id_Aprobador As Integer
        Dim entidad As New Entidad
        Dim Aprobacion_Publicacion As New Aprobacion_Publicacion
        Dim dataset As Data.DataSet

        Aprobacion_Publicacion.Fields.SelectAll()
        Aprobacion_Publicacion.Id_Publicacion.Where.EqualCondition(id_Publicacion)
        Aprobacion_Publicacion.Id_Grupo.Where.EqualCondition(id_Grupo)

        queryBuilder.Join.EqualCondition(entidad.Id_entidad, Aprobacion_Publicacion.Id_Entidad)
        entidad.NombreDisplay.ToSelect = True
        entidad.Id_entidad.ToSelect = True

        queryBuilder.From.Add(entidad)
        queryBuilder.From.Add(Aprobacion_Publicacion)
        dataset = connection.executeSelect(queryBuilder.RelationalSelectQuery())

        Dim lbl_Entidad As Label = dg_Relaciones.Items(indice).FindControl("lbl_Entidad")
        Dim lbl_Aprobar As Label = dg_Relaciones.Items(indice).FindControl("lbl_Aprobar")
        Dim chk_Aprobado As CheckBox = dg_Relaciones.Items(indice).FindControl("chk_Aprobado")
        Dim tbx_Comentario As TextBox = dg_Relaciones.Items(indice).FindControl("tbx_Comentario")

        If dataset.Tables.Count > 0 Then
            If dataset.Tables(0).Rows.Count > 0 Then
                'Busca si ya han aprobado.
                ObjectBuilder.CreateObject(dataset.Tables(0), 0, entidad)
                ObjectBuilder.CreateObject(dataset.Tables(0), 0, Aprobacion_Publicacion)

                id_Aprobador = entidad.Id_entidad.Value
                chk_Aprobado.ToolTip = "Si"

                'Si soy yo, los deja enable
                If id_Aprobador = securityHandler.Entidad Then
                    chk_Aprobado.Enabled = True
                    tbx_Comentario.Enabled = True
                Else
                    chk_Aprobado.Enabled = False
                    tbx_Comentario.Enabled = False
                End If

                lbl_Entidad.Text = entidad.NombreDisplay.Value
                lbl_Entidad.ToolTip = entidad.Id_entidad.Value

                'Si esta aprobado o no
                If Aprobacion_Publicacion.Aprobado.Value = "1" Then
                    chk_Aprobado.Checked = True
                    lbl_Aprobar.Text = "aprobó el " & Aprobacion_Publicacion.Fecha_Aprobado.ValueLocalized & " "
                    tbx_Comentario.Text = Aprobacion_Publicacion.Comentario.Value
                Else
                    lbl_Aprobar.Text = "vio pero NO aprobó el " & Aprobacion_Publicacion.Fecha_Visto.ValueLocalized & " "
                    tbx_Comentario.Text = Aprobacion_Publicacion.Comentario.Value
                End If
            Else        'Nadie los ha aprobado

                'Ver si pertenezco a ese Grupos_Entidades
                Dim Entidades_Grupo As New Entidades_Grupo
                Entidades_Grupo.Id_Entidad.Where.EqualCondition(securityHandler.Entidad)
                Entidades_Grupo.Id_Grupo.Where.EqualCondition(id_Grupo)
                Entidades_Grupo.Id_Entidad.ToSelect = True
                dataset = connection.executeSelect(queryBuilder.SelectQuery(Entidades_Grupo))
                If dataset.Tables.Count > 0 Then
                    If dataset.Tables(0).Rows.Count > 0 Then     'Busca que yo si tenga esa relacion
                        lbl_Entidad.Text = "Yo "
                        lbl_Aprobar.Text = "apruebo "
                        chk_Aprobado.Enabled = True
                        tbx_Comentario.Enabled = True
                    End If
                End If

            End If
        End If
    End Sub

    Protected Function ejecutarAprobacion_Publicacion(ByVal id_Publicacion As Integer) As Boolean
        Dim counter As Integer
        For counter = 0 To dg_Relaciones.Items.Count - 1
            Dim Aprobacion_Publicacion As New Aprobacion_Publicacion

            Dim lbl_Grupo As Label = dg_Relaciones.Items(counter).FindControl("lbl_Grupo")
            Dim lbl_TipoEntidad As Label = dg_Relaciones.Items(counter).FindControl("lbl_TipoEntidad")
            Dim lbl_Entidad As Label = dg_Relaciones.Items(counter).FindControl("lbl_Entidad")
            Dim lbl_Aprobar As Label = dg_Relaciones.Items(counter).FindControl("lbl_Aprobar")
            Dim chk_Aprobado As CheckBox = dg_Relaciones.Items(counter).FindControl("chk_Aprobado")
            Dim tbx_Comentario As TextBox = dg_Relaciones.Items(counter).FindControl("tbx_Comentario")

            If chk_Aprobado.Enabled Then
                'Busca si aprobado o no
                If chk_Aprobado.Checked Then
                    Aprobacion_Publicacion.Aprobado.Value = "1"
                    Aprobacion_Publicacion.Fecha_Aprobado.ValueUtc = Date.UtcNow
                Else
                    Aprobacion_Publicacion.Aprobado.Value = "0"
                End If
                Aprobacion_Publicacion.Comentario.Value = tbx_Comentario.Text
                Aprobacion_Publicacion.Visto.Value = 1
                Aprobacion_Publicacion.Fecha_Visto.ValueUtc = Date.UtcNow

                'Analiza si update, insertar
                If chk_Aprobado.ToolTip = "Si" Then
                    'If Not chk_Aprobado.Checked Then        'Update
                    Aprobacion_Publicacion.Id_Publicacion.Where.EqualCondition(id_Publicacion)
                    Aprobacion_Publicacion.Id_Entidad.Where.EqualCondition(securityHandler.Entidad)
                    Aprobacion_Publicacion.Id_Grupo.Where.EqualCondition(lbl_Grupo.ToolTip)
                    Aprobacion_Publicacion.Fecha_Aprobado.ValueUtc = Date.UtcNow

                    connection.executeUpdate(queryBuilder.UpdateQuery(Aprobacion_Publicacion))
                    'End If
                Else                                         'Insert
                    Aprobacion_Publicacion.Id_Publicacion.Value = id_Publicacion
                    Aprobacion_Publicacion.Id_Entidad.Value = securityHandler.Entidad
                    Aprobacion_Publicacion.Id_Grupo.Value = lbl_Grupo.ToolTip
                    connection.executeInsert(queryBuilder.InsertQuery(Aprobacion_Publicacion))
                End If
            End If
        Next
        Return True
    End Function

    Function verificarPublicacionAprobado(ByVal id_Publicacion As Integer) As Boolean
        Dim dataset1 As Data.DataSet
        Dim dataset2 As Data.DataSet
        Dim aprobado As Boolean = False
        Dim Publicacion As New Publicacion
        Dim aprobacion_tipoPublicacion As New Aprobacion_TipoPublicacion
        Dim aprobacion_Publicacion As New Aprobacion_Publicacion

        'Aprobaciones necesarias
        Publicacion.Id_Publicacion.Where.EqualCondition(id_Publicacion)
        aprobacion_tipoPublicacion.Id_TipoPublicacion.ToSelect = True
        queryBuilder.Join.EqualCondition(aprobacion_tipoPublicacion.Id_TipoPublicacion, (Publicacion.Id_tipoPublicacion))
        queryBuilder.From.Add(Publicacion)
        queryBuilder.From.Add(aprobacion_tipoPublicacion)
        dataset1 = connection.executeSelect(queryBuilder.RelationalSelectQuery)

        'Aprobaciones hechas
        aprobacion_Publicacion.Id_Publicacion.ToSelect = True
        queryBuilder.Join.EqualCondition(aprobacion_Publicacion.Id_Publicacion, (Publicacion.Id_Publicacion))
        aprobacion_Publicacion.Aprobado.Where.EqualCondition(1)
        queryBuilder.From.Add(Publicacion)
        queryBuilder.From.Add(aprobacion_Publicacion)
        dataset2 = connection.executeSelect(queryBuilder.RelationalSelectQuery)

        If dataset1.Tables.Count > 0 And dataset2.Tables.Count > 0 Then
            Dim rows1 As Integer = dataset1.Tables(0).Rows.Count
            Dim rows2 As Integer = dataset2.Tables(0).Rows.Count
            If rows1 > 0 And rows2 > 0 And rows1 = rows2 Then
                'Publicacion Aprobado
                aprobado = True
                Publicacion.Id_Publicacion.Where.EqualCondition(id_Publicacion)
                Publicacion.Aprobada.Value = 1
                connection.executeUpdate(queryBuilder.UpdateQuery(Publicacion))
            Else
                'Publicacion no Aprobado
                Publicacion.Id_Publicacion.Where.EqualCondition(id_Publicacion)
                Publicacion.Aprobada.Value = 0
                connection.executeUpdate(queryBuilder.UpdateQuery(Publicacion))
            End If
        End If
        Return aprobado
    End Function

    'Eventos
    Protected Sub btn_Salvar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Salvar.Click
        If IsValid() Then
            Dim id_Publicacion As Integer = lnk_Publicacion.ToolTip
            If ejecutarAprobacion_Publicacion(id_Publicacion) Then
                If verificarPublicacionAprobado(id_Publicacion) Then
                    Response.Redirect(lnk_Publicacion.NavigateUrl)
                End If
                selectPublicaciones()
                ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
                MyMaster.mostrarMensaje("Cambios en aprobacion salvados exitosamente.", False)
            Else
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Salvar, "cambios en aprobacion", True)
            End If
        Else
            MyMaster.MostrarMensaje(Resources.Orbecatalog_Resources.CamposRequeridos, False)
        End If
        upd_Contenido.Update()
        upd_Busqueda.Update()
    End Sub

    Protected Sub btn_Cancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Cancelar.Click
        ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
        upd_Contenido.Update()
        upd_Busqueda.Update()
    End Sub

    Protected Sub btn_Buscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Buscar.Click
        dg_Publicaciones.CurrentPageIndex = 0
        ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
        selectPublicaciones()
    End Sub

    Protected Sub dg_Publicaciones_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dg_Publicaciones.EditCommand
        ClearInfo(Configuration.EstadoPantalla.CONSULTAR, True)
        Me.EditCommandDataGrid(dg_Publicaciones, e.Item.ItemIndex)
        loadPublicacion(dg_Publicaciones.DataKeys(e.Item.ItemIndex))
        selectAprobacion_TipoPublicacion(dg_Publicaciones.DataKeys(e.Item.ItemIndex))
        upd_Contenido.Update()
        upd_Busqueda.Update()
    End Sub

    Protected Sub dg_Publicaciones_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dg_Publicaciones.PageIndexChanged
        dg_Publicaciones.CurrentPageIndex = e.NewPageIndex
        selectPublicaciones()
        Me.PageIndexChange(dg_Publicaciones)
        upd_Busqueda.Update()
    End Sub

End Class

