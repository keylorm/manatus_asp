Imports Orbelink.Control.Archivos
Imports Orbelink.DBHandler
Imports Orbelink.Entity.Archivos
Imports Orbelink.Entity.Entidades
Imports Orbelink.Entity.Publicaciones
Imports Orbelink.Entity.Orbecatalog
Imports Orbelink.DateAndTime
Imports Orbelink.Control.Comentarios

Partial Class _Publicacion
    Inherits PageBaseClass

    Const codigo_pantalla As String = "PU-02"
    Const level As Integer = 2

    'Pagina
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddScripts(level)
        securityHandler.VerifyPantalla(codigo_pantalla, level)

        ocwc_Atributos.SetVariables(Me.queryBuilder, connection, securityHandler, codigo_pantalla, MyMaster)

        If Not Page.IsPostBack Then
            ClearInfo(Configuration.EstadoPantalla.NORMAL, True)

            If Request.QueryString("id_publicacion") IsNot Nothing Then
                id_Actual = Request.QueryString("id_publicacion")
            End If

            securityHandler.VerifyActions(btn_Salvar, btn_Modificar, btn_Eliminar)

            SetThemeProperties()
            SetControlProperties()

            selectTiposPublicaciones()
            selectTipoRelacion()
            selectEstado_Publicacion()
            selectCategorias(trv_Categorias)
            selectGrupos(trv_Grupos)

            ConsultarUsuario(securityHandler.Entidad)

            ToLoadPage()

            If ddl_TipoPublicacion.Items.Count > 0 Then
                selectTipoEntidad(securityHandler.ClearanceLevel)
                ocwc_Atributos.selectAtributos_Para_Publicaciones(ddl_TipoPublicacion.SelectedValue)
            End If
        End If
    End Sub

    Protected Sub ToLoadPage()
        Dim loadSession As Boolean = False
        If id_Actual > 0 Then
            manejarVistas(True)

            'Cargar relacionados
            ocwc_Publicacion_Entidad.SetVariables(Me.queryBuilder, connection, securityHandler, codigo_pantalla, MyMaster)
            ocwc_Publicacion_Producto.SetVariables(Me.queryBuilder, connection, securityHandler, codigo_pantalla, MyMaster)
            'ocwc_Publicacion_Proyecto.SetVariables(Me.queryBuilder, connection, securityHandler, codigo_pantalla, MyMaster)
            ''ocwc_Publicacion_cuenta.SetVariables(Me.queryBuilder, connection, securityHandler, codigo_pantalla, MyMaster)

            ocwc_Publicacion_Entidad.cargarRelacionados(id_Actual)
            ocwc_Publicacion_Producto.cargarRelacionados(id_Actual)
            'ocwc_Publicacion_Proyecto.cargarRelacionados(id_Actual)
            'ocwc_Publicacion_cuenta.cargarRelacionados(id_Actual)

            lnk_AgregarComentario.NavigateUrl = MyMaster.obtenerIframeString("Comentario.aspx?id_Publicacion=" & id_Actual)
            lnk_AgregarCategorias.NavigateUrl = MyMaster.obtenerIframeString("Categorias.aspx")
            lnk_AgregarTipoPublicacion.NavigateUrl = MyMaster.obtenerIframeString("TipoPublicacion.aspx")
            lnk_AgregarEstados.NavigateUrl = MyMaster.obtenerIframeString("Estado_Publicacion.aspx")

            selectComentarios(id_Actual)

            Dim direccion As String = "../Archivos.aspx?popUp=true&id_publicacion=" & id_Actual
            if_Archivos.Attributes.Add("src", direccion)
            cargarTabs()
        Else
            manejarVistas(False)
        End If

        If Not Request.QueryString("back") Is Nothing Then
            Dim id_publicacionTemp As Integer = LoadSessionWorkingID()
            If id_publicacionTemp > 0 Then
                id_Actual = id_publicacionTemp
            End If
            loadSession = True
        End If

        If loadSession Then
            LoadSessionValues()
        End If
    End Sub

    Protected Sub ClearInfo(ByVal estado As Configuration.EstadoPantalla, ByVal clearInfo As Boolean)
        If clearInfo Then
            tbx_Titulo.Text = ""
            tbx_Corta.Text = ""
            tbx_Texto.Text = ""
            tbx_Link.Text = ""
            id_Actual = 0
            chk_Visible.Checked = False
            lbl_Entidad.ToolTip = securityHandler.Entidad
            limpiarPublicacion_TipoEntidad()
            vld_tbx_Titulo.IsValid = True

            ocwc_Atributos.ClearInfo(estado, clearInfo)
        End If

        Select Case estado
            Case Configuration.EstadoPantalla.NORMAL
                btn_Modificar.Visible = False
                btn_Salvar.Visible = True
                btn_Eliminar.Visible = False

                tab_Grupos.Enabled = False
                tab_Permisos.Enabled = False
                tab_Archivos.Enabled = False
                tab_Comentarios.Enabled = False
            Case Configuration.EstadoPantalla.CONSULTAR
                btn_Modificar.Visible = True
                btn_Salvar.Visible = False
                btn_Eliminar.Visible = True

                tab_Grupos.Enabled = True
                tab_Permisos.Enabled = True
                tab_Archivos.Enabled = True
                tab_Comentarios.Enabled = True
            Case Configuration.EstadoPantalla.MODIFICAR
                btn_Modificar.Visible = True
                btn_Salvar.Visible = False
                btn_Eliminar.Visible = True

                tab_Grupos.Enabled = True
                tab_Permisos.Enabled = True
                tab_Archivos.Enabled = True
                tab_Comentarios.Enabled = True
        End Select
    End Sub

    Protected Sub SetThemeProperties()
        lnk_Categoria.NavigateUrl = "javascript:toggleLayer('" & trv_Categorias.ClientID & "', this)"
        lnk_Grupo.NavigateUrl = "javascript:toggleLayer('" & trv_Grupos.ClientID & "', this)"
        div_PermisosGrupos.Style.Add("display", "none")
    End Sub

    Protected Sub SetControlProperties()
        Dim publicacion As New Publicacion
        tbx_Titulo.Attributes.Add("maxlength", publicacion.Titulo.Length)
        tbx_Titulo.Attributes.Add("onkeypress", "showTextProgress(this)")
        tbx_Corta.Attributes.Add("maxlength", publicacion.Corta.Length)
        tbx_Corta.Attributes.Add("onkeypress", "showTextProgress(this)")
        tbx_Link.Attributes.Add("maxlength", publicacion.Link.Length)
        tbx_Link.Attributes.Add("onkeypress", "showTextProgress(this)")
    End Sub

    Protected Sub cargarTabs()
        tabs_Publicacion.OnClientActiveTabChanged = "verificarTab"
        Dim funciones As New ArrayList
        If id_Actual Then
            Dim id_publicacion As Integer = id_Actual
            Dim tab1 As tabsInformation
            tab1.direccion = ""
            tab1.iframe = ""
            funciones.Add(tab1)
            tab1.direccion = "../Archivos.aspx?popUp=true&id_publicacion=" & id_publicacion
            tab1.iframe = if_Archivos.ClientID
            tabs_Publicacion.Tabs(1).Enabled = True
            funciones.Add(tab1)
            tab1.direccion = ""
            tab1.iframe = ""
            tabs_Publicacion.Tabs(2).Enabled = True
            funciones.Add(tab1)
            tab1.direccion = ""
            tab1.iframe = ""
            tabs_Publicacion.Tabs(3).Enabled = True
            funciones.Add(tab1)
            tabsFunciones(funciones)
        Else
            tabs_Publicacion.Tabs(1).Enabled = False
            tabs_Publicacion.Tabs(2).Enabled = False
        End If
    End Sub

    'Valores_Session
    Protected Function SaveSessionValues() As Boolean
        Dim Valores_Session As Valores_Session
        Try
            Valores_Session = New Valores_Session(securityHandler.Entidad, codigo_pantalla, ddl_TipoPublicacion.ID, ddl_TipoPublicacion.SelectedIndex)
            connection.executeInsert(queryBuilder.InsertQuery(Valores_Session))

            If tbx_Titulo.Text.Length > 0 Then
                Valores_Session = New Valores_Session(securityHandler.Entidad, codigo_pantalla, tbx_Titulo.ID, tbx_Titulo.Text)
                connection.executeInsert(queryBuilder.InsertQuery(Valores_Session))
            End If

            If id_Actual > 0 Then
                Valores_Session = New Valores_Session(securityHandler.Entidad, codigo_pantalla, tbx_Titulo.ID & "-Tooltip", id_Actual)
                connection.executeInsert(queryBuilder.InsertQuery(Valores_Session))
            End If

            If tbx_Corta.Text.Length > 0 Then
                Valores_Session = New Valores_Session(securityHandler.Entidad, codigo_pantalla, tbx_Corta.ID, tbx_Corta.Text)
                connection.executeInsert(queryBuilder.InsertQuery(Valores_Session))
            End If

            If tbx_Texto.Text.Length > 0 Then
                Valores_Session = New Valores_Session(securityHandler.Entidad, codigo_pantalla, tbx_Texto.ID, tbx_Texto.Text)
                connection.executeInsert(queryBuilder.InsertQuery(Valores_Session))
            End If

            If tbx_Link.Text.Length > 0 Then
                Valores_Session = New Valores_Session(securityHandler.Entidad, codigo_pantalla, tbx_Link.ID, tbx_Link.Text)
                connection.executeInsert(queryBuilder.InsertQuery(Valores_Session))
            End If

            If chk_Visible.Checked Then
                Valores_Session = New Valores_Session(securityHandler.Entidad, codigo_pantalla, chk_Visible.ID, chk_Visible.Checked)
                connection.executeInsert(queryBuilder.InsertQuery(Valores_Session))
            End If

            Return True
        Catch exc As Exception
            MyMaster.MostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    Protected Function LoadSessionValues() As Boolean
        Dim Valores_Session As New Valores_Session
        Dim dataset As Data.DataSet
        Dim counter As Integer
        Try
            Valores_Session.Fields.SelectAll()
            Valores_Session.Id_Entidad.Where.EqualCondition(securityHandler.Entidad)
            Valores_Session.Pantalla.Where.EqualCondition(codigo_pantalla)
            dataset = connection.executeSelect(queryBuilder.SelectQuery(Valores_Session))

            If dataset.Tables.Count > 0 Then
                If dataset.Tables(0).Rows.Count > 0 Then
                    Dim results_Valores_Session As ArrayList = ObjectBuilder.TransformDataTable(dataset.Tables(0), Valores_Session)
                    For counter = 0 To results_Valores_Session.Count - 1
                        Dim act_Valores_Session As Valores_Session = results_Valores_Session(counter)

                        If act_Valores_Session.ControlID.Value = ddl_TipoPublicacion.ID Then
                            ddl_TipoPublicacion.SelectedIndex = act_Valores_Session.Valor.Value

                        ElseIf act_Valores_Session.ControlID.Value = tbx_Titulo.ID Then
                            tbx_Titulo.Text = act_Valores_Session.Valor.Value

                        ElseIf act_Valores_Session.ControlID.Value = tbx_Titulo.ID & "-Tooltip" Then
                            id_Actual = act_Valores_Session.Valor.Value

                        ElseIf act_Valores_Session.ControlID.Value = tbx_Corta.ID Then
                            tbx_Corta.Text = act_Valores_Session.Valor.Value

                        ElseIf act_Valores_Session.ControlID.Value = tbx_Texto.ID Then
                            tbx_Texto.Text = act_Valores_Session.Valor.Value

                        ElseIf act_Valores_Session.ControlID.Value = tbx_Link.ID Then
                            tbx_Link.Text = act_Valores_Session.Valor.Value

                        ElseIf act_Valores_Session.ControlID.Value = chk_Visible.ID Then
                            chk_Visible.Checked = act_Valores_Session.Valor.Value

                        Else
                            'Otros varas
                        End If
                    Next
                End If
            End If

            'Borra estos registros
            Valores_Session.Id_Entidad.Where.EqualCondition(securityHandler.Entidad)
            Valores_Session.Pantalla.Where.EqualCondition(codigo_pantalla)
            connection.executeDelete(queryBuilder.DeleteQuery(Valores_Session))
            Return True
        Catch exc As Exception
            MyMaster.MostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    Protected Function SaveSessionWorkingID() As Boolean
        Dim Valores_Session As Valores_Session
        Try
            If id_Actual > 0 Then
                Valores_Session = New Valores_Session(securityHandler.Entidad, codigo_pantalla, "WorkingID", id_Actual)
                connection.executeInsert(queryBuilder.InsertQuery(Valores_Session))
            End If
            Return True
        Catch exc As Exception
            MyMaster.MostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    Protected Function LoadSessionWorkingID() As Integer
        Dim Valores_Session As New Valores_Session
        Dim dataset As Data.DataSet
        Dim workingId As Integer = 0

        Valores_Session.Fields.SelectAll()
        Valores_Session.Id_Entidad.Where.EqualCondition(securityHandler.Entidad)
        Valores_Session.Pantalla.Where.EqualCondition(codigo_pantalla)
        Valores_Session.ControlID.Where.EqualCondition("WorkingID")
        dataset = connection.executeSelect(queryBuilder.SelectQuery(Valores_Session))

        If dataset.Tables.Count > 0 Then
            If dataset.Tables(0).Rows.Count > 0 Then
                ObjectBuilder.CreateObject(dataset.Tables(0), 0, Valores_Session)
                workingId = Valores_Session.Valor.Value
            End If
        End If

        Return workingId
    End Function

    'Publicacion
    Protected Function insertPublicacion() As Boolean
        Dim Publicacion As Publicacion
        Dim esVisible As Integer
        Dim incluirRSS As Integer
        Dim fechaInicio As Date
        Dim fechaFin As Date
        Dim aprobada As Integer = 1

        If time_FechaInicio.IsNull Then
            fechaInicio = bdp_FechaInicio.SelectedDate
        Else
            fechaInicio = bdp_FechaInicio.SelectedDate.Add(time_FechaInicio.SelectedTime)
        End If
        If time_FechaFin.IsNull Then
            fechaFin = bdp_FechaFin.SelectedDate
        Else
            fechaFin = bdp_FechaFin.SelectedDate.Add(time_FechaFin.SelectedTime)
        End If

        Try
            If chk_Visible.Checked Then
                esVisible = 1
            End If
            If chk_IncluirRSS.Checked Then
                incluirRSS = 1
            End If

            If tieneAprobacion_TipoPublicacion(ddl_TipoPublicacion.SelectedValue) Then
                aprobada = 0
            End If

            Publicacion = New Publicacion(ddl_TipoPublicacion.SelectedValue, incluirRSS, tbx_Titulo.Text, _
                tbx_Corta.Text, tbx_Texto.Text, tbx_Link.Text, esVisible, Date.UtcNow, _
                DateHandler.ToUtcFromLocalizedDate(fechaInicio), DateHandler.ToUtcFromLocalizedDate(fechaFin), securityHandler.Usuario, lnk_Categoria.ToolTip, aprobada, ddl_Estados.SelectedValue)
            connection.executeInsert(queryBuilder.InsertQuery(Publicacion))
            Return True
        Catch exc As Exception
            MyMaster.MostrarMensaje(exc.Message, True)
            Return False
        End Try
    End Function

    Protected Function loadPublicacion(ByVal id_publicacion As Integer) As Boolean
        Dim dataSet As Data.DataSet
        Dim publicacion As New Publicacion
        Dim Categoria As New Categorias
        Dim entidad As New Entidad
        Dim permisos As New Publicacion_TipoEntidad
        Dim loaded As Boolean = False

        publicacion.Fields.SelectAll()
        publicacion.Id_Publicacion.Where.EqualCondition(id_publicacion)
        Categoria.Nombre.ToSelect = True
        queryBuilder.Join.EqualCondition(Categoria.Id_Categoria, publicacion.Id_Categoria)

        entidad.NombreDisplay.ToSelect = True
        entidad.Id_entidad.ToSelect = True
        queryBuilder.Join.EqualCondition(entidad.Id_entidad, publicacion.id_Entidad)

        queryBuilder.From.Add(entidad)
        queryBuilder.From.Add(publicacion)
        queryBuilder.From.Add(Categoria)

        Dim sqlQuery As String = queryBuilder.RelationalSelectQuery
        dataSet = connection.executeSelect(sqlQuery)
        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                ObjectBuilder.CreateObject(dataSet.Tables(0), 0, entidad)
                ObjectBuilder.CreateObject(dataSet.Tables(0), 0, Categoria)
                ObjectBuilder.CreateObject(dataSet.Tables(0), 0, publicacion)

                'Carga la informacion
                lbl_Entidad.Text = entidad.NombreDisplay.Value
                ConsultarUsuario(entidad.Id_entidad.Value)

                tbx_Titulo.Text = publicacion.Titulo.Value
                id_Actual = publicacion.Id_Publicacion.Value
                tbx_Corta.Text = publicacion.Corta.Value
                tbx_Texto.Text = publicacion.Texto.Value
                tbx_Link.Text = publicacion.Link.Value
                ddl_Estados.SelectedValue = publicacion.Id_Estado_Publicacion.Value

                If Not publicacion.FechaInicio.IsNull Then
                    bdp_FechaInicio.SelectedDate = publicacion.FechaInicio.ValueLocalized
                    Me.time_FechaInicio.SelectedTime = publicacion.FechaInicio.ValueLocalized.TimeOfDay
                End If
                If Not publicacion.FechaFin.IsNull Then
                    bdp_FechaFin.SelectedDate = publicacion.FechaFin.ValueLocalized
                    Me.time_FechaFin.SelectedTime = publicacion.FechaFin.ValueLocalized.TimeOfDay
                End If

                If publicacion.Visible.Value = 1 Then
                    chk_Visible.Checked = True
                End If
                If publicacion.IncluirRSS.Value = 1 Then
                    chk_IncluirRSS.Checked = True
                End If

                ddl_TipoPublicacion.SelectedValue = publicacion.Id_tipoPublicacion.Value
                lnk_Categoria.ToolTip = publicacion.Id_Categoria.Value
                lnk_Categoria.Text = Categoria.Nombre.Value

                loaded = True
            End If
        End If

        Return loaded
    End Function

    Protected Function updatePublicacion(ByVal id_publicacion As Integer) As Boolean
        Dim esVisible As Integer
        Dim incluirRSS As Integer
        Dim aprobada As Integer = 1
        Dim fechaInicio As Date
        Dim fechaFin As Date

        If time_FechaInicio.IsNull Then
            fechaInicio = bdp_FechaInicio.SelectedDate
        Else
            fechaInicio = bdp_FechaInicio.SelectedDate.Add(time_FechaInicio.SelectedTime)
        End If
        If time_FechaFin.IsNull Then
            fechaFin = bdp_FechaFin.SelectedDate
        Else
            fechaFin = bdp_FechaFin.SelectedDate.Add(time_FechaFin.SelectedTime)
        End If

        Try
            If chk_Visible.Checked Then
                esVisible = 1
            End If
            If chk_IncluirRSS.Checked Then
                incluirRSS = 1
            End If

            If tieneAprobacion_TipoPublicacion(ddl_TipoPublicacion.SelectedValue) Then
                aprobada = 0
            End If

            Dim textoLargo As String = ""

            If tbx_Texto.Text.Length > 0 Then
                textoLargo = Replace(tbx_Texto.Text, "'", "´")
            End If

            Dim temp As New Publicacion(ddl_TipoPublicacion.SelectedValue, incluirRSS, tbx_Titulo.Text, _
                tbx_Corta.Text, textoLargo, tbx_Link.Text, esVisible, Date.UtcNow, _
               DateHandler.ToUtcFromLocalizedDate(fechaInicio), DateHandler.ToUtcFromLocalizedDate(fechaFin), securityHandler.Usuario, lnk_Categoria.ToolTip, aprobada, ddl_Estados.SelectedValue)
            temp.Fecha.ToUpdate = False
            temp.Id_Publicacion.Where.EqualCondition(id_publicacion)
            connection.executeUpdate(queryBuilder.UpdateQuery(temp))
            Return True
        Catch exc As Exception
            MyMaster.MostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    Protected Function deletePublicacion(ByVal id_Publicacion As Integer) As Boolean
        Dim publicacion As New Publicacion
        Try
            publicacion.Id_Publicacion.Where.EqualCondition(id_Publicacion)
            connection.executeDelete(queryBuilder.DeleteQuery(publicacion))
            Return True
        Catch exc As Exception
            MyMaster.MostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    'Tipos Publicaciones
    Protected Sub selectTiposPublicaciones()
        Dim ds As Data.DataSet
        Dim temp As New TipoPublicacion

        temp.Fields.SelectAll()
        queryBuilder.Orderby.Add(temp.Nombre)
        ds = connection.executeSelect(queryBuilder.SelectQuery(temp))

        If ds.Tables.Count > 0 Then
            ddl_TipoPublicacion.DataSource = ds
            ddl_TipoPublicacion.DataTextField = temp.Nombre.Name
            ddl_TipoPublicacion.DataValueField = temp.Id_TipoPublicacion.Name
            ddl_TipoPublicacion.DataBind()
        End If
    End Sub

    'Aprobacion_TipoPublicacion
    Protected Function tieneAprobacion_TipoPublicacion(ByVal Id_TipoPublicacion As Integer) As Boolean
        Dim dataSet As Data.DataSet
        Dim Aprobacion_TipoPublicacion As New Aprobacion_TipoPublicacion
        Dim tieneAprobaciones As Boolean = False

        Aprobacion_TipoPublicacion.Id_TipoPublicacion.ToSelect = True
        Aprobacion_TipoPublicacion.Id_TipoPublicacion.Where.EqualCondition(Id_TipoPublicacion)

        queryBuilder.From.Add(Aprobacion_TipoPublicacion)
        dataSet = connection.executeSelect(queryBuilder.RelationalSelectQuery())

        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                tieneAprobaciones = True
            End If
        End If
        Return tieneAprobaciones
    End Function

    'Categorias
    Protected Sub selectCategorias(ByVal theTreeView As TreeView)
        Dim categoria As New Categorias
        Dim dataset As Data.DataSet
        Dim counter As Integer
        Dim primero As Boolean = True

        categoria.Fields.SelectAll()
        categoria.Id_Categoria.Where.EqualCondition_OnSameTable(categoria.Id_padre)
        dataset = connection.executeSelect(queryBuilder.SelectQuery(categoria))

        If dataset.Tables.Count > 0 Then
            If dataset.Tables(0).Rows.Count Then
                theTreeView.Nodes.Clear()

                Dim result_Categorias As ArrayList = ObjectBuilder.TransformDataTable(dataset.Tables(0), categoria)
                For counter = 0 To dataset.Tables(0).Rows.Count - 1
                    Dim act_Categoria As Categorias = result_Categorias(counter)

                    Dim hijo As New TreeNode(act_Categoria.Nombre.Value, act_Categoria.Id_Categoria.Value)
                    theTreeView.Nodes.Add(hijo)
                    If primero Then
                        lnk_Categoria.Text = act_Categoria.Nombre.Value
                        lnk_Categoria.ToolTip = act_Categoria.Id_Categoria.Value
                        primero = False
                    End If
                    selectCategoriasHijos(theTreeView.Nodes(counter))
                Next
            End If
        End If
        'theTreeView.CollapseAll()
    End Sub

    Protected Sub selectCategoriasHijos(ByVal elTreeNode As TreeNode)
        Dim padre_id_Categoria As Integer = elTreeNode.Value
        Dim Categoria As New Categorias
        Dim dataset As Data.DataSet
        Dim counter As Integer

        Categoria.Fields.SelectAll()
        Categoria.Id_padre.Where.EqualCondition(padre_id_Categoria)
        Categoria.Id_Categoria.Where.DiferentCondition(padre_id_Categoria)
        dataset = connection.executeSelect(queryBuilder.SelectQuery(Categoria))

        Dim result_Categorias As ArrayList = ObjectBuilder.TransformDataTable(dataset.Tables(0), Categoria)
        For counter = 0 To dataset.Tables(0).Rows.Count - 1
            Dim act_Categoria As Categorias = result_Categorias(counter)
            Dim hijo As New TreeNode(act_Categoria.Nombre.Value, act_Categoria.Id_Categoria.Value)
            selectCategoriasHijos(hijo)
            elTreeNode.ChildNodes.Add(hijo)
        Next
    End Sub

    'comentarios
    Sub selectComentarios(ByVal id_Publicacion As Integer)
        Dim controlComentarios As New Orbelink.Control.Publicaciones.ControladoraComentario_Publicacion(Configuration.Config_DefaultConnectionString)
        Dim comentarios As New ComentarioHandler(Configuration.Config_DefaultConnectionString)

        Dim resultado As ComentarioHandler.ResultSelect = comentarios.selectComentarios(controlComentarios, id_Publicacion)
        Me.BindGridView(resultado.datos, gv_Comentarios, Nothing, resultado.DataFieldNames, "Comentario.aspx", True)
    End Sub

    'Usuario
    Private Sub ConsultarUsuario(ByVal id_usuario As Integer)
        Dim controlEntidad As New Orbelink.Control.Entidades.EntidadHandler(Configuration.Config_DefaultConnectionString)
        lbl_Entidad.Text = controlEntidad.ConsultarNombreEntidad(id_usuario)
    End Sub

    'Tipo Entidad
    Protected Sub selectTipoEntidad(ByVal nivel As Integer)
        Dim dataset As Data.DataSet
        Dim tipoEntidad As New TipoEntidad

        tipoEntidad.Fields.SelectAll()
        tipoEntidad.Nivel.Where.LessThanCondition(nivel)
        queryBuilder.Orderby.Add(tipoEntidad.Nombre)
        dataset = connection.executeSelect(queryBuilder.SelectQuery(tipoEntidad))
        If dataset.Tables.Count > 0 Then
            If dataset.Tables(0).Rows.Count > 0 Then
                dg_PermisosTipoEntidad.DataSource = dataset
                dg_PermisosTipoEntidad.DataKeyField = tipoEntidad.Id_TipoEntidad.Name
                dg_PermisosTipoEntidad.DataBind()

                For counter As Integer = 0 To dataset.Tables(0).Rows.Count - 1
                    Dim lbl_TipoEntidad As Label = dg_PermisosTipoEntidad.Items(counter).FindControl("lbl_TipoEntidad")
                    lbl_TipoEntidad.Text = dataset.Tables(0).Rows(counter).Item(tipoEntidad.Nombre.Name)
                    lbl_TipoEntidad.ToolTip = dataset.Tables(0).Rows(counter).Item(tipoEntidad.Id_TipoEntidad.Name)
                    Dim chk_Escribir As CheckBox = dg_PermisosTipoEntidad.Items(counter).FindControl("chk_Escribir")
                    Dim chk_Leer As CheckBox = dg_PermisosTipoEntidad.Items(counter).FindControl("chk_Leer")
                    Dim chk_Descargar As CheckBox = dg_PermisosTipoEntidad.Items(counter).FindControl("chk_Descargar")
                    Dim chk_Subir As CheckBox = dg_PermisosTipoEntidad.Items(counter).FindControl("chk_Subir")
                    chk_Escribir.Checked = False
                    chk_Leer.Checked = False
                    chk_Descargar.Checked = False
                    chk_Subir.Checked = False
                Next
                dg_PermisosTipoEntidad.Visible = True
            Else
                dg_PermisosTipoEntidad.Visible = False
            End If
        End If
    End Sub

    'Archivos
    Protected Function deleteArchivos(ByVal id_Publicacion As Integer) As Boolean
        Dim Archivo_Publicacion As New Archivo_Publicacion
        Dim counter As Integer
        Dim dataset As Data.DataSet
        Dim deleted As Boolean = True
        Try
            Archivo_Publicacion.id_Publicacion.Where.EqualCondition(id_Publicacion)
            Archivo_Publicacion.id_Archivo.ToSelect = True

            queryBuilder.From.Add(Archivo_Publicacion)
            dataset = connection.executeSelect(queryBuilder.RelationalSelectQuery)

            Dim archivos As New ArchivoHandler(connection)

            If dataset.Tables.Count > 0 Then
                If dataset.Tables(0).Rows.Count > 0 Then
                    Dim results_Archivos As ArrayList = ObjectBuilder.TransformDataTable(dataset.Tables(0), Archivo_Publicacion)
                    For counter = 0 To dataset.Tables(0).Rows.Count - 1
                        Dim act_Archivos As Archivo_Publicacion = results_Archivos(counter)

                        If archivos.DeleteArchivo(act_Archivos.id_Archivo.Value) Then
                            'Borrar registro 
                            Archivo_Publicacion.id_Archivo.Where.EqualCondition(act_Archivos.id_Archivo.Value)
                            connection.executeDelete(queryBuilder.DeleteQuery(Archivo_Publicacion))
                        End If
                    Next
                End If
            End If
        Catch exc As Exception
            MyMaster.MostrarMensaje(exc.Message, True)

            deleted = False
        End Try
        Return deleted
    End Function

    'Estado_Publicacion
    Protected Sub selectEstado_Publicacion()
        Dim dataset As Data.DataSet
        Dim Estado_Publicacion As New Estado_Publicacion

        Estado_Publicacion.Fields.SelectAll()
        queryBuilder.Orderby.Add(Estado_Publicacion.Nombre)
        dataset = connection.executeSelect(queryBuilder.SelectQuery(Estado_Publicacion))

        If dataset.Tables.Count > 0 Then
            If dataset.Tables(0).Rows.Count Then
                ddl_Estados.DataSource = dataset
                ddl_Estados.DataTextField = Estado_Publicacion.Nombre.Name
                ddl_Estados.DataValueField = Estado_Publicacion.Id_Estado_Publicacion.Name
                ddl_Estados.DataBind()
            End If
        End If
    End Sub

    'Permisos por Publicacion
    Protected Sub loadPublicacion_TipoEntidad(ByVal id_Publicacion As Integer)
        Dim dataSet As Data.DataSet
        Dim permiso As New Publicacion_TipoEntidad
        Dim tipoEntidad As New TipoEntidad
        Dim result As String
        dg_PermisosTipoEntidad.Visible = True

        permiso.Fields.SelectAll()
        permiso.id_Publicacion.Where.EqualCondition(id_Publicacion)
        result = queryBuilder.SelectQuery(permiso)
        dataSet = connection.executeSelect(result)

        'Llena el DataGrid
        Dim id_tipoEntidad As Integer
        Dim counter As Integer
        Dim results As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), permiso)

        For counter = 0 To dg_PermisosTipoEntidad.Items.Count - 1
            id_tipoEntidad = CType(dg_PermisosTipoEntidad.Items(counter).FindControl("lbl_TipoEntidad"), Label).ToolTip
            Dim counterB As Integer
            For counterB = 0 To dataSet.Tables(0).Rows.Count - 1
                Dim actual As Publicacion_TipoEntidad = results(counterB)
                If id_tipoEntidad = actual.id_TipoEntidad.Value Then
                    obtenerPublicacion_TipoEntidad(actual, counter)
                End If
            Next
        Next
    End Sub

    Protected Function deletePublicacion_TipoEntidad(ByVal id_publicacion As Integer) As Boolean
        Dim Publicacion_TipoEntidad As New Publicacion_TipoEntidad
        Try
            Publicacion_TipoEntidad.id_Publicacion.Where.EqualCondition(id_publicacion)
            connection.executeDelete(queryBuilder.DeleteQuery(Publicacion_TipoEntidad))
            Return True
        Catch exc As Exception
            MyMaster.MostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    Protected Sub obtenerPublicacion_TipoEntidad(ByVal permiso As Publicacion_TipoEntidad, ByVal indiceDataGrid As Integer)
        Dim lbl_TipoEntidad As Label = dg_PermisosTipoEntidad.Items(indiceDataGrid).FindControl("lbl_TipoEntidad")
        Dim chk_Escribir As CheckBox = dg_PermisosTipoEntidad.Items(indiceDataGrid).FindControl("chk_Escribir")
        Dim chk_Leer As CheckBox = dg_PermisosTipoEntidad.Items(indiceDataGrid).FindControl("chk_Leer")
        Dim chk_Descargar As CheckBox = dg_PermisosTipoEntidad.Items(indiceDataGrid).FindControl("chk_Descargar")
        Dim chk_Subir As CheckBox = dg_PermisosTipoEntidad.Items(indiceDataGrid).FindControl("chk_Subir")

        'Dice que si habia registro
        chk_Escribir.ToolTip = "Si"

        'Selecciona los que debe
        If permiso.Escribir.Value = "1" Then
            chk_Escribir.Checked = True
        Else
            chk_Escribir.Checked = False
        End If
        If permiso.Leer.Value = "1" Then
            chk_Leer.Checked = True
        Else
            chk_Leer.Checked = False
        End If
        If permiso.Descargar.Value = "1" Then
            chk_Descargar.Checked = True
        Else
            chk_Descargar.Checked = False
        End If
        If permiso.Subir.Value = "1" Then
            chk_Subir.Checked = True
        Else
            chk_Subir.Checked = False
        End If

    End Sub

    Protected Function ejecutarPublicacion_TipoEntidad(ByVal id_Publicacion As Integer) As Boolean
        Try
            For counter As Integer = 0 To dg_PermisosTipoEntidad.Items.Count - 1
                Dim permiso As New Publicacion_TipoEntidad
                Dim alguno As Boolean = False
                Dim lbl_TipoEntidad As Label = dg_PermisosTipoEntidad.Items(counter).FindControl("lbl_TipoEntidad")
                Dim chk_Escribir As CheckBox = dg_PermisosTipoEntidad.Items(counter).FindControl("chk_Escribir")
                Dim chk_Leer As CheckBox = dg_PermisosTipoEntidad.Items(counter).FindControl("chk_Leer")
                Dim chk_Descargar As CheckBox = dg_PermisosTipoEntidad.Items(counter).FindControl("chk_Descargar")
                Dim chk_Subir As CheckBox = dg_PermisosTipoEntidad.Items(counter).FindControl("chk_Subir")

                'Busca cuales check estan
                If chk_Escribir.Checked Then
                    permiso.Escribir.Value = "1"
                    permiso.Leer.Value = "1"
                    alguno = True
                Else
                    permiso.Escribir.Value = "0"
                End If
                If chk_Descargar.Checked Then
                    permiso.Descargar.Value = "1"
                    permiso.Leer.Value = "1"
                    alguno = True
                Else
                    permiso.Descargar.Value = "0"
                End If
                If chk_Subir.Checked Then
                    permiso.Subir.Value = "1"
                    alguno = True
                Else
                    permiso.Subir.Value = "0"
                End If

                If chk_Leer.Checked Or alguno Then
                    permiso.Leer.Value = "1"
                    alguno = True
                Else
                    permiso.Leer.Value = "0"
                End If

                'Analiza si borrar, insertar o actualizar
                If chk_Escribir.ToolTip = "Si" Then
                    permiso.id_Publicacion.Where.EqualCondition(id_Publicacion)
                    permiso.id_TipoEntidad.Where.EqualCondition(lbl_TipoEntidad.ToolTip)
                    If alguno Then          'Update
                        connection.executeUpdate(queryBuilder.UpdateQuery(permiso))
                    Else                    'Delete
                        connection.executeDelete(queryBuilder.DeleteQuery(permiso))
                    End If
                Else
                    If alguno Then          'Insert
                        permiso.id_Publicacion.Value = id_Publicacion
                        permiso.id_TipoEntidad.Value = lbl_TipoEntidad.ToolTip

                        connection.executeInsert(queryBuilder.InsertQuery(permiso))
                    End If
                End If
            Next
            Return True
        Catch exc As Exception
            MyMaster.MostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    Protected Sub limpiarPublicacion_TipoEntidad()
        Dim counter As Integer

        For counter = 0 To dg_PermisosTipoEntidad.Items.Count - 1
            Dim chk_Escribir As CheckBox = dg_PermisosTipoEntidad.Items(counter).FindControl("chk_Escribir")
            Dim chk_Leer As CheckBox = dg_PermisosTipoEntidad.Items(counter).FindControl("chk_Leer")
            Dim chk_Descargar As CheckBox = dg_PermisosTipoEntidad.Items(counter).FindControl("chk_Descargar")
            Dim chk_Subir As CheckBox = dg_PermisosTipoEntidad.Items(counter).FindControl("chk_Subir")

            chk_Escribir.Checked = False
            chk_Leer.Checked = False
            chk_Descargar.Checked = False
            chk_Subir.Checked = False

            chk_Escribir.ToolTip = ""
        Next
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
                hijo.Collapse()
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
                hijo.Collapse()
            Next
        End If
    End Sub

    'Publicacion por Grupos_Entidades
    Protected Sub loadPublicacion_Grupo(ByVal id_Publicacion As Integer, ByVal id_grupo As Integer)
        Dim dataSet As Data.DataSet
        Dim publicacion_Grupo As New Publicacion_Grupo
        Dim TipoRelacion_Grupos As New TipoRelacion_Grupos

        publicacion_Grupo.Fields.SelectAll()
        publicacion_Grupo.Id_Publicacion.Where.EqualCondition(id_Publicacion)
        publicacion_Grupo.Id_Grupo.Where.EqualCondition(id_grupo)

        dataSet = connection.executeSelect(queryBuilder.SelectQuery(publicacion_Grupo))

        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                'Llena el DataGrid
                Dim Id_TipoRelacion As Integer
                Dim counter As Integer
                Dim results As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), publicacion_Grupo)

                For counter = 0 To dg_PermisosGrupo.Items.Count - 1
                    Id_TipoRelacion = dg_PermisosGrupo.DataKeys.Item(counter)
                    '.Items(counter).FindControl("lbl_TipoRelacion"), Label).ToolTip
                    Dim counterB As Integer
                    For counterB = 0 To dataSet.Tables(0).Rows.Count - 1
                        Dim actual As Publicacion_Grupo = results(counterB)
                        If Id_TipoRelacion = actual.Id_TipoRelacion.Value Then
                            obtenerPublicacion_Grupo(actual, counter)
                        End If
                    Next
                Next
            End If
        End If
    End Sub

    Protected Function deletePublicacion_Grupo(ByVal id_publicacion As Integer) As Boolean
        Dim publicacion_Grupo As New Publicacion_Grupo
        Try
            publicacion_Grupo.Id_Publicacion.Where.EqualCondition(id_publicacion)
            connection.executeDelete(queryBuilder.DeleteQuery(publicacion_Grupo))
            Return True
        Catch exc As Exception
            MyMaster.MostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    Protected Sub obtenerPublicacion_Grupo(ByVal publicacion_Grupo As Publicacion_Grupo, ByVal indiceDataGrid As Integer)
        Dim lbl_TipoRelacion_Entidades As Label = dg_PermisosGrupo.Items(indiceDataGrid).FindControl("lbl_TipoRelacion")
        Dim chk_Escribir As CheckBox = dg_PermisosGrupo.Items(indiceDataGrid).FindControl("chk_Escribir")
        Dim chk_Leer As CheckBox = dg_PermisosGrupo.Items(indiceDataGrid).FindControl("chk_Leer")
        Dim chk_Descargar As CheckBox = dg_PermisosGrupo.Items(indiceDataGrid).FindControl("chk_Descargar")
        Dim chk_Subir As CheckBox = dg_PermisosGrupo.Items(indiceDataGrid).FindControl("chk_Subir")

        'Dice que si habia registro
        chk_Escribir.ToolTip = "Si"

        'Selecciona los que debe
        If publicacion_Grupo.Escribir.Value = "1" Then
            chk_Escribir.Checked = True
        Else
            chk_Escribir.Checked = False
        End If
        If publicacion_Grupo.Leer.Value = "1" Then
            chk_Leer.Checked = True
        Else
            chk_Leer.Checked = False
        End If
        If publicacion_Grupo.Descargar.Value = "1" Then
            chk_Descargar.Checked = True
        Else
            chk_Descargar.Checked = False
        End If
        If publicacion_Grupo.Subir.Value = "1" Then
            chk_Subir.Checked = True
        Else
            chk_Subir.Checked = False
        End If

    End Sub

    Protected Function ejecutarPublicacion_Grupo(ByVal id_Publicacion As Integer, ByVal id_grupo As Integer) As Boolean
        Try
            For counter As Integer = 0 To dg_PermisosGrupo.Items.Count - 1
                Dim publicacion_Grupo As New Publicacion_Grupo
                Dim alguno As Boolean
                Dim lbl_TipoRelacion_Grupos As Label = dg_PermisosGrupo.Items(counter).FindControl("lbl_TipoRelacion")
                Dim chk_Escribir As CheckBox = dg_PermisosGrupo.Items(counter).FindControl("chk_Escribir")
                Dim chk_Leer As CheckBox = dg_PermisosGrupo.Items(counter).FindControl("chk_Leer")
                Dim chk_Descargar As CheckBox = dg_PermisosGrupo.Items(counter).FindControl("chk_Descargar")
                Dim chk_Subir As CheckBox = dg_PermisosGrupo.Items(counter).FindControl("chk_Subir")

                'Busca cuales check estan
                If chk_Escribir.Checked Then
                    publicacion_Grupo.Escribir.Value = "1"
                    publicacion_Grupo.Leer.Value = "1"
                    alguno = True
                Else
                    publicacion_Grupo.Escribir.Value = "0"
                End If
                If chk_Leer.Checked Then
                    publicacion_Grupo.Leer.Value = "1"
                    alguno = True
                Else
                    publicacion_Grupo.Leer.Value = "0"
                End If
                If chk_Descargar.Checked Then
                    publicacion_Grupo.Descargar.Value = "1"
                    publicacion_Grupo.Leer.Value = "1"
                    alguno = True
                Else
                    publicacion_Grupo.Descargar.Value = "0"
                End If
                If chk_Subir.Checked Then
                    publicacion_Grupo.Subir.Value = "1"
                    alguno = True
                Else
                    publicacion_Grupo.Subir.Value = "0"
                End If

                'Analiza si borrar, insertar o actualizar
                If chk_Escribir.ToolTip = "Si" Then
                    publicacion_Grupo.Id_Publicacion.Where.EqualCondition(id_Publicacion)
                    publicacion_Grupo.Id_Grupo.Where.EqualCondition(id_grupo)
                    publicacion_Grupo.Id_TipoRelacion.Where.EqualCondition(lbl_TipoRelacion_Grupos.ToolTip)
                    If alguno Then          'Update
                        connection.executeUpdate(queryBuilder.UpdateQuery(publicacion_Grupo))
                    Else                    'Delete
                        connection.executeDelete(queryBuilder.DeleteQuery(publicacion_Grupo))
                    End If
                Else
                    If alguno Then          'Insert
                        publicacion_Grupo.Id_Publicacion.Value = id_Publicacion
                        publicacion_Grupo.Id_Grupo.Value = id_grupo
                        publicacion_Grupo.Id_TipoRelacion.Value = lbl_TipoRelacion_Grupos.ToolTip
                        connection.executeInsert(queryBuilder.InsertQuery(publicacion_Grupo))
                    End If
                End If
            Next
            Return True
        Catch exc As Exception
            MyMaster.MostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    Protected Sub limpiarPublicacion_Grupo()
        Dim counter As Integer
        Dim toCheck As Integer

        For counter = 0 To dg_PermisosGrupo.Items.Count - 1
            Dim lbl_TipoRelacion_Entidades As Label = dg_PermisosGrupo.Items(counter).FindControl("lbl_TipoRelacion")
            Dim chk_Escribir As CheckBox = dg_PermisosGrupo.Items(counter).FindControl("chk_Escribir")
            Dim chk_Leer As CheckBox = dg_PermisosGrupo.Items(counter).FindControl("chk_Leer")
            Dim chk_Descargar As CheckBox = dg_PermisosGrupo.Items(counter).FindControl("chk_Descargar")
            Dim chk_Subir As CheckBox = dg_PermisosGrupo.Items(counter).FindControl("chk_Subir")

            toCheck = False

            chk_Escribir.Checked = toCheck
            chk_Leer.Checked = toCheck
            chk_Descargar.Checked = toCheck
            chk_Subir.Checked = toCheck

            lbl_TipoRelacion_Entidades.ToolTip = ""

        Next
    End Sub

    'Aprobacion por Publicacion
    Protected Function deleteAprobacion_Publicacion(ByVal id_publicacion As Integer) As Boolean
        Dim Aprobacion_Publicacion As New Aprobacion_Publicacion
        Try
            Aprobacion_Publicacion.Id_Publicacion.Where.EqualCondition(id_publicacion)
            connection.executeDelete(queryBuilder.DeleteQuery(Aprobacion_Publicacion))
            Return True
        Catch exc As Exception
            MyMaster.MostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    'TipoRelacion
    Protected Sub selectTipoRelacion()
        Dim dataSet As Data.DataSet
        Dim TipoRelacion_Grupos As New TipoRelacion_Grupos
        Dim result As String

        TipoRelacion_Grupos.Fields.SelectAll()
        result = queryBuilder.SelectQuery(TipoRelacion_Grupos)
        dataSet = connection.executeSelect(result)

        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then

                dg_PermisosGrupo.DataSource = dataSet
                dg_PermisosGrupo.DataKeyField = TipoRelacion_Grupos.Id_TipoRelacion.Name
                dg_PermisosGrupo.DataBind()

                'Llena el DataGrid

                Dim counter As Integer
                For counter = 0 To dg_PermisosGrupo.Items.Count - 1
                    Dim lbl_TipoRelacion_Entidades As Label = dg_PermisosGrupo.Items(counter).FindControl("lbl_TipoRelacion")
                    Dim chk_Escribir As CheckBox = dg_PermisosGrupo.Items(counter).FindControl("chk_Escribir")
                    Dim chk_Leer As CheckBox = dg_PermisosGrupo.Items(counter).FindControl("chk_Leer")
                    Dim chk_Descargar As CheckBox = dg_PermisosGrupo.Items(counter).FindControl("chk_Descargar")
                    Dim chk_Subir As CheckBox = dg_PermisosGrupo.Items(counter).FindControl("chk_Subir")

                    lbl_TipoRelacion_Entidades.Text = dataSet.Tables(0).Rows(counter).Item(TipoRelacion_Grupos.Nombre.Name)
                    lbl_TipoRelacion_Entidades.ToolTip = dataSet.Tables(0).Rows(counter).Item(TipoRelacion_Grupos.Id_TipoRelacion.Name)
                    chk_Escribir.Checked = False
                    chk_Leer.Checked = False
                    chk_Descargar.Checked = False
                    chk_Subir.Checked = False
                Next
            Else
                dg_PermisosGrupo.Visible = False
            End If
        End If

    End Sub

    'Eventos
    Protected Sub btn_Salvar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Salvar.Click
        If lnk_Categoria.ToolTip.Length = 0 Then
            vld_lnk_Categoria.IsValid = False
        Else
            vld_lnk_Categoria.IsValid = True
        End If
        If IsValid() Then
            If insertPublicacion() Then
                Dim publicacion As New Publicacion
                id_Actual = connection.lastKey(publicacion.TableName, publicacion.Id_Publicacion.Name)

                ClearInfo(Configuration.EstadoPantalla.CONSULTAR, False)

                ocwc_Atributos.verificarAtributos(id_Actual, ddl_TipoPublicacion.SelectedValue)

                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Salvar, "Publicacion", False)

                ToLoadPage()
                upd_Pantalla.Update()
            Else
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Salvar, "Publicacion", True)
            End If
        Else
            upd_Contenido.Update()
            MyMaster.MostrarMensaje(Resources.Orbecatalog_Resources.CamposRequeridos, False)
        End If
    End Sub

    Protected Sub btn_Modificar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Modificar.Click
        If lnk_Categoria.Text.Length = 0 Then
            vld_lnk_Categoria.IsValid = False
        Else
            vld_lnk_Categoria.IsValid = True
        End If
        Dim id_publicacion As Integer = id_Actual
        If IsValid() Then
            If updatePublicacion(id_publicacion) Then
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Modificar, "Publicacion", False)

                ocwc_Atributos.verificarAtributos(id_publicacion, ddl_TipoPublicacion.SelectedValue)

                upd_Contenido.Update()
            Else
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Modificar, "Publicacion", True)
            End If
        Else
            upd_Contenido.Update()
            MyMaster.MostrarMensaje("Algun campo necesario está vacio o erroneo.", False)
        End If
    End Sub

    Protected Sub btn_Cancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Cancelar.Click
        ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
        ToLoadPage()
        upd_Contenido.Update()
    End Sub

    Protected Sub btn_Eliminar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Eliminar.Click
        Dim id_publicacion As Integer = id_Actual
        If deleteArchivos(id_publicacion) Then
            If deletePublicacion(id_publicacion) Then
                ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
                MyMaster.MostrarMensaje("Publicacion eliminiada exitosamente.", False)
                upd_Pantalla.Update()
            Else
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Eliminar, "Publicacion", True)
            End If
        Else
            MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Eliminar, "Archivo de esta publicacion", True)
        End If
    End Sub

    Protected Sub trv_Categorias_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles trv_Categorias.SelectedNodeChanged
        Dim id_NodoActual As Integer = trv_Categorias.SelectedNode.Value
        lnk_Categoria.Text = trv_Categorias.SelectedNode.Text
        lnk_Categoria.ToolTip = id_NodoActual
        upd_Categorias.Update()
    End Sub

    Protected Sub trv_Grupos_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles trv_Grupos.SelectedNodeChanged
        Dim id_NodoActual As Integer = trv_Grupos.SelectedNode.Value
        lnk_Grupo.Text = trv_Grupos.SelectedNode.Text
        lnk_Grupo.ToolTip = id_NodoActual

        Dim id_publicacion As Integer
        If id_Actual > 0 Then
            id_publicacion = id_Actual
        End If
        loadPublicacion_Grupo(id_publicacion, id_NodoActual)
        div_PermisosGrupos.Style.Add("display", "block")
        upd_Grupos.Update()
    End Sub

    Protected Sub btn_GuardarPermisos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_GuardarPermisos.Click
        Dim id_publicacion As Integer = id_Actual
        If lnk_Grupo.ToolTip.Length > 0 Then
            Dim id_grupo As Integer = lnk_Grupo.ToolTip
            If ejecutarPublicacion_Grupo(id_publicacion, id_grupo) Then
                MyMaster.MostrarMensaje("Permisos salvados exitosamente.", False)
                div_PermisosGrupos.Style.Add("display", "none")
            Else
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Salvar, "permisos", True)
            End If
        End If
    End Sub

    Protected Sub btn_PermisosTipoEntidad_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_PermisosTipoEntidad.Click

        Dim id_publicacion As Integer = id_Actual
        If ejecutarPublicacion_TipoEntidad(id_publicacion) Then
            MyMaster.MostrarMensaje("Permisos salvados exitosamente.", False)
        Else
            MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Salvar, "permisos", True)
        End If
    End Sub

    Protected Sub ddl_TipoPublicacion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddl_TipoPublicacion.SelectedIndexChanged
        ocwc_Atributos.selectAtributos_Para_Publicaciones(ddl_TipoPublicacion.SelectedValue)
    End Sub

    'Agregar
    'Protected Sub btn_AgregarAtributo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_OtrosAtributos.Click
    '    MyMaster.mostrarPopUp("AtribTipoPublicacion.aspx")
    'End Sub

    Public Overrides Sub PopUpOkEventHandler(ByVal param As String)
        Select Case param
            Case "PU-04"
                selectCategorias(trv_Categorias)
                upd_Categorias.Update()
            Case "PU-03"
                selectTiposPublicaciones()
                upd_Contenido.Update()
            Case "PU-05"
                selectEstado_Publicacion()
                upd_Contenido.Update()
            Case "PU-09"
                ocwc_Atributos.selectAtributos_Para_Publicaciones(ddl_TipoPublicacion.SelectedValue)
                upd_Contenido.Update()
            Case "PU-10"
                selectComentarios(id_Actual)
                upd_Comentarios.Update()
        End Select

    End Sub

    Protected Sub btn_Accion_Editar_Click(ByVal Sender As Object, ByVal E As System.EventArgs) Handles btn_Accion_Editar.Click
        manejarVistas(False)
        Me.tabs_Publicacion.ActiveTabIndex = 0
        upd_Pantalla.Update()
    End Sub

    Protected Sub btn_Accion_Ver_Click(ByVal Sender As Object, ByVal E As System.EventArgs) Handles btn_Accion_Ver.Click
        manejarVistas(True)
        upd_Pantalla.Update()
    End Sub

    Protected Sub manejarVistas(ByVal vistaVer As Boolean)
        Dim publicacionExist As Boolean = False
        pnl_editar.Visible = Not vistaVer
        pnl_editarTitulo.Visible = Not vistaVer
        ocwc_DetallePublicacion.Visible = vistaVer
        pnl_verTitulo.Visible = vistaVer

        'Se carga la informacion de ver
        If vistaVer Then
            ocwc_DetallePublicacion.SetVariables(Me.queryBuilder, connection, securityHandler, codigo_pantalla, MyMaster, MyMaster.TheScriptManager, level)
            ocwc_DetallePublicacion.cargarInformacion(id_Actual)
        Else
            publicacionExist = loadPublicacion(id_Actual)
            If publicacionExist Then
                ClearInfo(Configuration.EstadoPantalla.MODIFICAR, False)
                selectTipoEntidad(securityHandler.ClearanceLevel)
                loadPublicacion_TipoEntidad(id_Actual)
                ocwc_Atributos.loadAtributos_Publicacion(id_Actual, ddl_TipoPublicacion.SelectedValue)
            Else

            End If
        End If
    End Sub
End Class
