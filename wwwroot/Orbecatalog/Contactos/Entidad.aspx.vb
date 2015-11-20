Imports Orbelink.DBHandler
Imports Orbelink.Control.Archivos
Imports Orbelink.Entity.Entidades
Imports Orbelink.Control.Entidades

Partial Class Orbecatalog_Entidades_Entidad
    Inherits PageBaseClass

    Const codigo_pantalla As String = "CO-02"
    Const level As Integer = 2

    'Pagina    
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddScripts(level)

        ocwc_DetalleEntidad.SetVariables(Me.queryBuilder, connection, securityHandler, codigo_pantalla, MyMaster, level)
        ocwc_Entidad_Atributos.SetVariables(Me.queryBuilder, connection, securityHandler, codigo_pantalla, MyMaster)
        ocwc_Entidad_Relaciones.SetVariables(Me.queryBuilder, connection, securityHandler, codigo_pantalla, MyMaster)
        ocwc_Ubicacion.SetVariables(connection, securityHandler, codigo_pantalla, MyMaster)
        ocwc_Rel_Entidad_Producto.SetVariables(Me.queryBuilder, connection, securityHandler, codigo_pantalla, MyMaster)
        ocwc_Rel_Entidad_Publicacion.SetVariables(Me.queryBuilder, connection, securityHandler, codigo_pantalla, MyMaster)
        'ocwc_Rel_Entidad_Proyecto.SetVariables(Me.queryBuilder, connection, securityHandler, codigo_pantalla, MyMaster)
        ocwc_Rel_Entidad_Reservacion.SetVariables(Me.queryBuilder, connection, securityHandler, codigo_pantalla, MyMaster)

        securityHandler.VerifyPantalla(codigo_pantalla, level)

        If Not IsPostBack Then
            'De inicio y theme
            securityHandler.VerifyActions(btn_Salvar, btn_Modificar, btn_Eliminar)
            SetThemeProperties()
            SetControlProperties()

            If Request.QueryString("id_entidad") IsNot Nothing Then
                id_Actual = Request.QueryString("id_entidad")
            End If

            loadInfoGrupos()
            ToLoadPage(Configuration.EstadoPantalla.CONSULTAR)
        End If
    End Sub

    Protected Sub ClearInfo(ByVal estado As Configuration.EstadoPantalla, ByVal clearInfo As Boolean)
        ocwc_Ubicacion.ClearInfo(estado, clearInfo)
        ocwc_Entidad_Atributos.ClearInfo(estado, clearInfo)

        If clearInfo Then
            tbx_Usuario.Text = ""
            tbx_PasswordEntidad.Text = ""
            tbx_Descripcion.Text = ""
            tbx_Usuario.Enabled = True
            tbx_PasswordEntidad.Visible = True
            lbl_Password.Visible = False
            lst_Grupos.Items.Clear()

            tbx_NombreEntidad.Text = ""
            tbx_Apellido.Text = ""
            tbx_Apellido2.Text = ""
            tbx_Telefono.Text = ""
            tbx_Email.Text = ""
            tbx_Celular.Text = ""
            tbx_NombreEntidad.ToolTip = ""
            vld_tbx_NombreEntidad.IsValid = True
        End If

        Select Case estado
            Case Configuration.EstadoPantalla.NORMAL
                btn_Modificar.Visible = False
                btn_Salvar.Visible = True
                btn_Eliminar.Visible = False
                btn_Accion_Editar.Visible = False
                btn_Accion_Ver.Visible = False

                pnl_Grupos.Style("display") = "none"
                tabs_Entidad.Tabs(1).Enabled = False
                tabs_Entidad.Tabs(2).Enabled = False
                pnl_editar.Visible = True
                pnl_editarTitulo.Visible = True
                ocwc_DetalleEntidad.Visible = False
                pnl_verTitulo.Visible = False
            Case Configuration.EstadoPantalla.CONSULTAR
                pnl_editar.Visible = False
                pnl_editarTitulo.Visible = False
                ocwc_DetalleEntidad.Visible = True
                pnl_verTitulo.Visible = True
                btn_Accion_Editar.Visible = True
                btn_Accion_Ver.Visible = False

            Case Configuration.EstadoPantalla.MODIFICAR
                btn_Modificar.Visible = True
                btn_Salvar.Visible = False
                btn_Eliminar.Visible = True
                btn_Accion_Editar.Visible = False
                btn_Accion_Ver.Visible = True

                pnl_Grupos.Style("display") = "block"
                tabs_Entidad.Tabs(1).Enabled = True
                tabs_Entidad.Tabs(2).Enabled = True
                pnl_editar.Visible = True
                pnl_editarTitulo.Visible = True
                ocwc_DetalleEntidad.Visible = False
                pnl_verTitulo.Visible = False
        End Select
        upd_BotonesAcciones.Update()
    End Sub

    Protected Sub cargarTabs(ByVal id_Entidad As Integer)
        
        If id_Entidad > 0 Then
            tabs_Entidad.OnClientActiveTabChanged = "verificarTab"
            Dim funciones As New ArrayList

            Dim tab1 As tabsInformation
            tab1.direccion = ""
            tab1.iframe = ""
            funciones.Add(tab1)

            tab1.direccion = "../Archivos.aspx?popUp=true&id_entidad=" & id_Entidad
            tab1.iframe = if_Archivos.ClientID
            funciones.Add(tab1)

            tab1.direccion = ""
            tab1.iframe = ""
            funciones.Add(tab1)
            tabsFunciones(funciones)
        End If
    End Sub

    Public Overrides Sub PopUpOkEventHandler(ByVal param As String)
        'Cargar relacionados
        Dim id_entidad As Integer
        Dim loadSession As Boolean = False
        If id_Actual Then
            id_entidad = id_Actual
        End If

        Select Case param
            Case "Re-02"
                ocwc_Rel_Entidad_Producto.cargarRelacionados(id_entidad)

            Case "Re-03"
                'ocwc_Rel_Entidad_Proyecto.cargarRelacionados(id_entidad)

            Case "Re-04"
                ocwc_Rel_Entidad_Publicacion.cargarRelacionados(id_entidad)

            Case "CO-03"
                selectTipoEntidad(securityHandler.ClearanceLevel, False)

            Case "CO-05"
                ocwc_Entidad_Atributos.selectAtributos_Para_Entidades(ddl_TipoEntidad.SelectedValue)
                ocwc_Entidad_Atributos.loadAtributos_Entidad(id_entidad, ddl_TipoEntidad.SelectedValue)

            Case "CO-08"
                ocwc_Entidad_Relaciones.selectRelaciones_Entidades(id_entidad)

            Case "CO-10"
                ocwc_Ubicacion.selectUbicaciones()

            Case "CO-11"
                loadInfoGrupos()
                Me.udp_grupo.Update()

            Case "CO-12"
                loadInfoGrupos()
                Me.udp_grupo.Update()
        End Select


    End Sub

    Protected Sub ToLoadPage(ByVal cualEstado As Configuration.EstadoPantalla)
        'Carga entidad
        Dim controladoraEntidad As New EntidadHandler(Configuration.Config_DefaultConnectionString)
        If controladoraEntidad.ExisteEntidad(id_Actual) Then
            cargarTabs(id_Actual)

            'Cargar relacionados
            lbl_nombreTitulo.Text = controladoraEntidad.ConsultarNombreEntidad(id_Actual)

            ocwc_Rel_Entidad_Producto.cargarRelacionados(id_Actual)
            ocwc_Rel_Entidad_Publicacion.cargarRelacionados(id_Actual)
            'ocwc_Rel_Entidad_Proyecto.cargarRelacionados(id_Actual)
            ocwc_Rel_Entidad_Reservacion.cargarRelacionados(id_Actual)

            If cualEstado = Configuration.EstadoPantalla.CONSULTAR Then
                ClearInfo(Configuration.EstadoPantalla.CONSULTAR, True)
                ocwc_DetalleEntidad.LoadEntidad(id_Actual)
            Else
                ClearInfo(Configuration.EstadoPantalla.MODIFICAR, False)
                If id_Actual = securityHandler.Entidad And id_Actual > 0 Then
                    selectTipoEntidad(securityHandler.ClearanceLevel, True)
                    loadEntidad(id_Actual, True)
                Else
                    selectTipoEntidad(securityHandler.ClearanceLevel, False)
                    loadEntidad(id_Actual, False)
                End If

                'Otros
                If ddl_TipoEntidad.Items.Count > 0 Then
                    ocwc_Entidad_Atributos.selectAtributos_Para_Entidades(ddl_TipoEntidad.SelectedValue)
                End If
                ocwc_Entidad_Atributos.loadAtributos_Entidad(id_Actual, ddl_TipoEntidad.SelectedValue)
                ocwc_Entidad_Relaciones.selectRelaciones_Entidades(id_Actual)
                selectEntidades_Grupo(id_Actual)
            End If
        Else
            selectTipoEntidad(securityHandler.ClearanceLevel, False)
            id_Actual = 0
            ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
        End If
        ocwc_Ubicacion.selectUbicaciones()
    End Sub

    Protected Sub SetThemeProperties()
        lnk_Grupo.NavigateUrl = "javascript:toggleLayer('" & trv_Grupos.ClientID & "', this)"
        trv_Grupos.Style("display") = "none"
    End Sub

    Protected Sub SetControlProperties()
        Dim entidad As New Entidad
        tbx_Usuario.Attributes.Add("maxlength", entidad.UserName.Length)
        tbx_Usuario.Attributes.Add("onkeypress", "showTextProgress(this)")
        tbx_NombreEntidad.Attributes.Add("maxlength", entidad.NombreEntidad.Length)
        tbx_NombreEntidad.Attributes.Add("onkeypress", "showTextProgress(this)")
        tbx_Apellido.Attributes.Add("maxlength", entidad.Apellido.Length)
        tbx_Apellido.Attributes.Add("onkeypress", "showTextProgress(this)")
        tbx_Apellido2.Attributes.Add("maxlength", entidad.Apellido2.Length)
        tbx_Apellido2.Attributes.Add("onkeypress", "showTextProgress(this)")
        tbx_Telefono.Attributes.Add("maxlength", entidad.Telefono.Length)
        tbx_Telefono.Attributes.Add("onkeypress", "showTextProgress(this)")
        tbx_Email.Attributes.Add("maxlength", entidad.Email.Length)
        tbx_Email.Attributes.Add("onkeypress", "showTextProgress(this)")
        tbx_Celular.Attributes.Add("maxlength", entidad.Celular.Length)
        tbx_Celular.Attributes.Add("onkeypress", "showTextProgress(this)")
    End Sub

    'Entidad
    Protected Function loadEntidad(ByVal id_Entidad As Integer, ByVal loadMySelf As Boolean) As Boolean
        Dim dataSet As Data.DataSet
        Dim entidad As New Entidad
        Dim ubicacion As New Ubicacion
        Dim exists As Boolean = False

        entidad.Fields.SelectAll()
        entidad.Id_entidad.Where.EqualCondition(id_Entidad)
        ubicacion.Nombre.ToSelect = True
        ubicacion.Id_ubicacion.ToSelect = True
        ubicacion.Id_ubicacion.AsName = "UbicacionId_Ubicacion"

        queryBuilder.Join.EqualCondition(ubicacion.Id_ubicacion, entidad.Id_Ubicacion)

        queryBuilder.From.Add(entidad)
        queryBuilder.From.Add(ubicacion)

        dataSet = connection.executeSelect(queryBuilder.RelationalSelectQuery)
        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                ObjectBuilder.CreateObject(dataSet.Tables(0), 0, ubicacion)
                ObjectBuilder.CreateObject(dataSet.Tables(0), 0, entidad)

                'Carga text boxs
                tbx_NombreEntidad.Text = entidad.NombreEntidad.Value
                tbx_NombreEntidad.ToolTip = entidad.Id_entidad.Value
                tbx_Apellido.Text = entidad.Apellido.Value
                tbx_Apellido2.Text = entidad.Apellido2.Value

                tbx_Telefono.Text = entidad.Telefono.Value
                tbx_Email.Text = entidad.Email.Value
                tbx_Celular.Text = entidad.Celular.Value

                ddl_TipoEntidad.SelectedValue = entidad.Id_tipoEntidad.Value
                If loadMySelf Then
                    ddl_TipoEntidad.Enabled = False
                End If

                ocwc_Ubicacion.loadUbicacion(ubicacion)

                tbx_Usuario.Text = entidad.UserName.Value
                tbx_Descripcion.Text = entidad.Descripcion.Value

                'Si password editable, segun mi entidad
                If id_Entidad = securityHandler.Entidad Or entidad.Password.IsValid = False Then
                    tbx_Usuario.Enabled = True
                    tbx_PasswordEntidad.Visible = True
                    tbx_PasswordEntidad.Text = ""
                    lbl_Password.Visible = False
                Else
                    tbx_Usuario.Enabled = False
                    tbx_PasswordEntidad.Visible = False
                    lbl_Password.Visible = True
                End If
                exists = True

                'lbl_tipoTitulo.Text = ddl_TipoEntidad.SelectedItem.Text
                'lbl_nombreTitulo.Text = entidad.NombreDisplay.Value
            End If
        End If
        upd_Contenido.Update()
        Return exists
    End Function

    Protected Function insertEntidad() As Orbelink.Control.Entidades.EntidadHandler.Resultado
        Dim entidad As New Entidad

        entidad.Id_tipoEntidad.Value = ddl_TipoEntidad.SelectedValue
        entidad.NombreEntidad.Value = tbx_NombreEntidad.Text
        entidad.Apellido2.Value = tbx_Apellido2.Text
        entidad.Telefono.Value = tbx_Telefono.Text
        entidad.Celular.Value = tbx_Celular.Text
        entidad.Apellido.Value = tbx_Apellido.Text
        entidad.Email.Value = tbx_Email.Text
        entidad.Id_Ubicacion.Value = ocwc_Ubicacion.Ubicacion_Selected()

        If tbx_Descripcion.Text.Length > 0 Then
            entidad.Descripcion.Value = tbx_Descripcion.Text
        End If
        If tbx_Usuario.Text.Length > 0 Then
            entidad.UserName.Value = tbx_Usuario.Text
        End If
        If tbx_PasswordEntidad.Text.Length > 0 Then
            entidad.Password.Value = tbx_PasswordEntidad.Text
        End If
        Dim controladoraEntidad As New EntidadHandler(Configuration.Config_DefaultConnectionString)
        Return controladoraEntidad.CrearEntidad(entidad)

    End Function

    Protected Function updateEntidad(ByVal id_entidad As Integer) As Boolean
        Dim entidad As New Entidad()

        entidad.Id_tipoEntidad.Value = ddl_TipoEntidad.SelectedValue
        entidad.NombreEntidad.Value = tbx_NombreEntidad.Text
        entidad.Apellido2.Value = tbx_Apellido2.Text
        entidad.Telefono.Value = tbx_Telefono.Text
        entidad.Celular.Value = tbx_Celular.Text
        entidad.Apellido.Value = tbx_Apellido.Text
        entidad.Id_Ubicacion.Value = ocwc_Ubicacion.Ubicacion_Selected()

        If tbx_Email.Text.Length > 0 Then
            entidad.Email.Value = tbx_Email.Text
        End If
        If tbx_Descripcion.Text.Length > 0 Then
            entidad.Descripcion.Value = tbx_Descripcion.Text
        End If
        If tbx_Usuario.Text.Length > 0 Then
            entidad.UserName.Value = tbx_Usuario.Text
        End If
        If tbx_PasswordEntidad.Text.Length > 0 Then
            entidad.Password.Value = tbx_PasswordEntidad.Text
        End If
        entidad.Theme.ToUpdate = False

        If tbx_Usuario.Text.Length = 0 Or tbx_PasswordEntidad.Text.Length = 0 Then
            entidad.UserName.ToUpdate = False
            entidad.Password.ToUpdate = False
        End If

        entidad.Id_entidad.Where.EqualCondition(id_entidad)

        Dim controladoraEntidad As New EntidadHandler(Configuration.Config_DefaultConnectionString)
        Return controladoraEntidad.ActualizarEntidad(entidad, id_entidad)
    End Function

    Protected Function deleteEntidad(ByVal id_entidad As Integer) As Boolean
        Dim controladoraEntidad As New EntidadHandler(Configuration.Config_DefaultConnectionString)
        controladoraEntidad.EliminarEntidad(id_entidad)


        Dim entidad As New Entidad
        Try
            entidad.Id_entidad.Where.EqualCondition(id_entidad)
            connection.executeDelete(queryBuilder.DeleteQuery(entidad))
            Return True
        Catch exc As Exception
            MyMaster.MostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    Public Function hasMail() As Boolean
        If tbx_Email.Text.Length > 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    'TipoEntidad
    Public Sub selectTipoEntidad(ByVal nivel As Integer, ByVal loadMySelf As Boolean)
        Dim dataset As Data.DataSet
        Dim tipoEntidad As New TipoEntidad

        Me.hyl_AgregarTipoEntidad.NavigateUrl = MyMaster.obtenerIframeString("tipoEntidad.aspx")

        tipoEntidad.Fields.SelectAll()
        If loadMySelf Then
            tipoEntidad.Nivel.Where.LessThanOrEqualCondition(nivel)
        Else
            tipoEntidad.Nivel.Where.LessThanCondition(nivel)
        End If

        queryBuilder.Orderby.Add(tipoEntidad.Nombre)
        dataset = connection.executeSelect(queryBuilder.SelectQuery(tipoEntidad))
        If dataset.Tables.Count > 0 Then
            If dataset.Tables(0).Rows.Count > 0 Then
                ddl_TipoEntidad.DataSource = dataset
                ddl_TipoEntidad.DataTextField = tipoEntidad.Nombre.Name
                ddl_TipoEntidad.DataValueField = tipoEntidad.Id_TipoEntidad.Name
                ddl_TipoEntidad.DataBind()
            End If
        End If
        Me.upd_detalle.Update()
    End Sub

    'TipoRelacion_Grupos
    Protected Sub selectTipoRelacion_Grupos()
        Dim dataSet As Data.DataSet
        Dim TipoRelacion_Entidades As New TipoRelacion_Grupos

        TipoRelacion_Entidades.Id_TipoRelacion.ToSelect = True
        TipoRelacion_Entidades.Nombre.ToSelect = True
        queryBuilder.Orderby.Add(TipoRelacion_Entidades.Nombre)
        dataSet = connection.executeSelect(queryBuilder.SelectQuery(TipoRelacion_Entidades))
        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                ddl_TipoRelacion_Grupos.DataSource = dataSet
                ddl_TipoRelacion_Grupos.DataTextField = TipoRelacion_Entidades.Nombre.Name
                ddl_TipoRelacion_Grupos.DataValueField = TipoRelacion_Entidades.Id_TipoRelacion.Name
                ddl_TipoRelacion_Grupos.DataBind()
            End If
        End If
    End Sub

    'Grupos
    Protected Sub selectGrupos(ByVal theTreeView As TreeView)
        Dim Grupos_Entidades As New Grupos_Entidades
        Dim dataset As Data.DataSet
        Dim counter As Integer
        theTreeView.Nodes.Clear()
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

    Protected Sub loadInfoGrupos()
        Hyp_agregarGrupo.NavigateUrl = MyMaster.obtenerIframeString("Grupo.aspx")
        Hyp_tipoRelacionGrupos.NavigateUrl = MyMaster.obtenerIframeString("TipoRelacion_Grupos.aspx")
        selectGrupos(trv_Grupos)
        selectTipoRelacion_Grupos()
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

    'Entidades_Grupo
    Protected Sub selectEntidades_Grupo(ByVal Id_Entidad As Integer)
        Dim dataset As New Data.DataSet
        Dim entidades_Grupo As New Entidades_Grupo
        Dim Grupos_Entidades As New Grupos_Entidades
        Dim TipoRelacion_Grupos As New TipoRelacion_Grupos

        entidades_Grupo.Id_Entidad.Where.EqualCondition(Id_Entidad)

        Grupos_Entidades.Nombre.ToSelect = True
        Grupos_Entidades.Id_Grupo.ToSelect = True
        queryBuilder.Join.EqualCondition(Grupos_Entidades.Id_Grupo, entidades_Grupo.Id_Grupo)

        TipoRelacion_Grupos.Nombre.ToSelect = True
        TipoRelacion_Grupos.Id_TipoRelacion.ToSelect = True
        queryBuilder.Join.EqualCondition(TipoRelacion_Grupos.Id_TipoRelacion, entidades_Grupo.Id_TipoRelacion)

        queryBuilder.From.Add(entidades_Grupo)
        queryBuilder.From.Add(Grupos_Entidades)
        queryBuilder.From.Add(TipoRelacion_Grupos)
        queryBuilder.Orderby.Add(Grupos_Entidades.Nombre)
        dataset = connection.executeSelect(queryBuilder.RelationalSelectQuery())

        lst_Grupos.Items.Clear()
        If dataset.Tables(0).Rows.Count > 0 Then
            Dim results_Grupos_Entidades As ArrayList = ObjectBuilder.TransformDataTable(dataset.Tables(0), Grupos_Entidades)
            Dim results_TipoRelacion_Grupos As ArrayList = ObjectBuilder.TransformDataTable(dataset.Tables(0), TipoRelacion_Grupos)
            For counter As Integer = 0 To dataset.Tables(0).Rows.Count - 1
                Dim act_Grupos_Entidades As Grupos_Entidades = results_Grupos_Entidades(counter)
                Dim act_TipoRelacion_Entidades As TipoRelacion_Grupos = results_TipoRelacion_Grupos(counter)

                lst_Grupos.Items.Add(act_Grupos_Entidades.Nombre.Value & ", como " & act_TipoRelacion_Entidades.Nombre.Value)
                lst_Grupos.Items(counter).Value = act_Grupos_Entidades.Id_Grupo.Value & "-" & act_TipoRelacion_Entidades.Id_TipoRelacion.Value
            Next
        End If
    End Sub

    Protected Function insertEntidades_Grupo(ByVal id_Entidad As Integer, ByVal id_Grupo As Integer, ByVal id_TipoRelacion As Integer) As Boolean
        Dim entidades_Grupo As New Entidades_Grupo
        Try
            entidades_Grupo.Id_Grupo.Value = id_Grupo
            entidades_Grupo.Id_Entidad.Value = id_Entidad
            entidades_Grupo.Id_TipoRelacion.Value = id_TipoRelacion
            connection.executeInsert(queryBuilder.InsertQuery(entidades_Grupo))
            Return True
        Catch exc As Exception
            MyMaster.MostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    Protected Function deleteEntidades_Grupo(ByVal id_Entidad As Integer, ByVal id_Grupo As Integer, ByVal id_TipoRelacion As Integer) As Boolean
        Dim entidades_Grupo As New Entidades_Grupo
        Try
            entidades_Grupo.Id_Grupo.Where.EqualCondition(id_Grupo)
            entidades_Grupo.Id_Entidad.Where.EqualCondition(id_Entidad)
            entidades_Grupo.Id_TipoRelacion.Where.EqualCondition(id_TipoRelacion)
            connection.executeDelete(queryBuilder.DeleteQuery(entidades_Grupo))
            Return True
        Catch exc As Exception
            MyMaster.MostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    'Eventos
    Protected Sub btn_Salvar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Salvar.Click
        If IsValid And ocwc_Ubicacion.IsValid Then
            Dim resultado As EntidadHandler.Resultado = Me.insertEntidad()

            If resultado.code = EntidadHandler.ResultCodes.OK Then
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Salvar, "Entidad", False)
                id_Actual = resultado.id_Entidad

                If Not ocwc_Entidad_Atributos.executeAtributos_Entidad(resultado.id_Entidad) Then
                    MyMaster.concatenarMensaje("Pero error al guardar atributos.", True)
                Else
                    ocwc_Entidad_Atributos.loadAtributos_Entidad(resultado.id_Entidad, ddl_TipoEntidad.SelectedValue)
                End If

                'id_Actual = resultado.id_Entidad
                'Dim direccion As String = "../Archivos.aspx?popUp=true&id_entidad=" & resultado.id_Entidad
                'if_Archivos.Attributes.Add("src", direccion)
                'cargarTabs(id_Actual)

                ToLoadPage(Configuration.EstadoPantalla.CONSULTAR)
                udp_entidad.Update()
            Else
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Salvar, "entidad", True)
                MyMaster.concatenarMensaje(Resources.Entidades_Resources.ResourceManager.GetString(resultado.code.ToString), True)
            End If
        Else
            Me.MyMaster.MostrarMensaje(Resources.Orbecatalog_Resources.CamposRequeridos, True)
        End If
        upd_Contenido.Update()
    End Sub

    Protected Sub btn_Modificar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Modificar.Click
        Dim controladoraEntidad As New EntidadHandler(Configuration.Config_DefaultConnectionString)

        'If Not controladoraEntidad.existUserName(tbx_Usuario.Text, id_Actual) Then
        If IsValid() Then
            Dim id_update As Integer = id_Actual
            If updateEntidad(id_update) Then
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Modificar, "Entidad", False)
                If Not ocwc_Entidad_Atributos.executeAtributos_Entidad(id_update) Then
                    MyMaster.concatenarMensaje("<br />Pero error al actualizar atributos.", False)
                Else
                    ocwc_Entidad_Atributos.loadAtributos_Entidad(id_update, ddl_TipoEntidad.SelectedValue)
                End If

                ToLoadPage(Configuration.EstadoPantalla.CONSULTAR)
                udp_entidad.Update()
            Else
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Modificar, "entidad", True)
            End If
        Else
            Me.MyMaster.MostrarMensaje(Resources.Orbecatalog_Resources.CamposRequeridos, True)
        End If
        'Else
        'MyMaster.MostrarMensaje("Email y/o nombre de usuario ya registrados.", False)
        'End If
    End Sub

    Protected Sub btn_Cancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Cancelar.Click
        If id_Actual > 0 Then
            Me.ClearInfo(Configuration.EstadoPantalla.MODIFICAR, True)
            ToLoadPage(Configuration.EstadoPantalla.MODIFICAR)
        Else
            Me.ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
        End If
        upd_Contenido.Update()
    End Sub

    Protected Sub btn_Eliminar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Eliminar.Click
        Dim id_entidad As Integer = id_Actual
        If deleteArchivos(id_entidad) Then
            If deleteEntidad(id_entidad) Then
                ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
                MyMaster.MostrarMensaje("Entidad eliminiada exitosamente.", False)
                upd_Contenido.Update()
            Else
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Eliminar, "Entidad", True)
                MyMaster.concatenarMensaje("<br />Si esta entidad ha publicado algo, debe borrarlo antes.", False)
            End If
        End If
    End Sub

    Protected Function deleteArchivos(ByVal id_entidad As Integer) As Boolean
        Dim Archivo_Entidad As New Archivo_Entidad
        Dim counter As Integer
        Dim dataset As Data.DataSet
        Dim deleted As Boolean = True
        Try
            Archivo_Entidad.id_Entidad.Where.EqualCondition(id_entidad)
            Archivo_Entidad.id_Archivo.ToSelect = True

            queryBuilder.From.Add(Archivo_Entidad)
            dataset = connection.executeSelect(queryBuilder.RelationalSelectQuery)

            Dim archivos As New ArchivoHandler(connection)

            If dataset.Tables.Count > 0 Then
                If dataset.Tables(0).Rows.Count > 0 Then
                    Dim results_Archivos As ArrayList = ObjectBuilder.TransformDataTable(dataset.Tables(0), Archivo_Entidad)
                    For counter = 0 To dataset.Tables(0).Rows.Count - 1
                        Dim act_Archivos As Archivo_Entidad = results_Archivos(counter)

                        If archivos.DeleteArchivo(act_Archivos.id_Archivo.Value) Then
                            'Borrar registro 
                            Archivo_Entidad.id_Archivo.Where.EqualCondition(act_Archivos.id_Archivo.Value)
                            connection.executeDelete(queryBuilder.DeleteQuery(Archivo_Entidad))
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

    Protected Sub trv_Grupos_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles trv_Grupos.SelectedNodeChanged
        Dim id_NodoActual As Integer = trv_Grupos.SelectedNode.Value
        lnk_Grupo.Text = trv_Grupos.SelectedNode.Text
        lnk_Grupo.ToolTip = id_NodoActual
    End Sub

    Protected Sub btn_AgregarAGrupo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_AgregarAGrupo.Click

        Dim id_entidad As Integer = id_Actual
        If lnk_Grupo.ToolTip.Length > 0 Then
            If insertEntidades_Grupo(id_entidad, lnk_Grupo.ToolTip, ddl_TipoRelacion_Grupos.SelectedValue) Then
                selectEntidades_Grupo(id_entidad)
                MyMaster.MostrarMensaje("Entidad agregada al Grupo exitosamente.", False)
            Else
                MyMaster.MostrarMensaje("Error al agregar entidad al Grupo.", False)
            End If
        Else
            MyMaster.MostrarMensaje("Debe seleccionar un Grupo.", False)
        End If

    End Sub

    Protected Sub btn_QuitarDeGrupo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_QuitarDeGrupo.Click

        Dim id_entidad As Integer = id_Actual
        If lst_Grupos.SelectedValue.Length > 0 Then
            Dim corte As Integer = lst_Grupos.SelectedValue.IndexOf("-")
            Dim id_Grupo As Integer = lst_Grupos.SelectedValue.Substring(0, corte)
            Dim id_TipoRelacion As Integer = lst_Grupos.SelectedValue.Substring(corte + 1, lst_Grupos.SelectedValue.Length - corte - 1)
            If deleteEntidades_Grupo(id_entidad, id_Grupo, id_TipoRelacion) Then
                selectEntidades_Grupo(id_entidad)
                MyMaster.MostrarMensaje("Entidad eliminada del Grupo exitosamente.", False)
            Else
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Eliminar, "entidad del Grupo", True)
            End If
        Else
            MyMaster.MostrarMensaje("Debe seleccionar un Grupo.", False)
        End If

    End Sub

    Protected Sub btn_ListaGrupo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_ListaGrupo.Click
        If lst_Grupos.SelectedValue.Length > 0 Then
            Dim corte As Integer = lst_Grupos.SelectedValue.IndexOf("-")
            Dim id_Grupo As Integer = lst_Grupos.SelectedValue.Substring(0, corte)
            MyMaster.mostrarPopUp("Grupo_List.aspx?id_Grupo=" & id_Grupo)
        Else
            MyMaster.MostrarMensaje("Debe seleccionar un Grupo.", False)
        End If
    End Sub

    Protected Sub ddl_TipoEntidad_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddl_TipoEntidad.SelectedIndexChanged
        ocwc_Entidad_Atributos.selectAtributos_Para_Entidades(ddl_TipoEntidad.SelectedValue)
    End Sub

    Protected Sub ocwc_Ubicacion_RequestExternalPage(ByVal laRuta As String, ByVal codigo As String) Handles ocwc_Ubicacion.RequestExternalPage, ocwc_Entidad_Atributos.RequestExternalPage, ocwc_Entidad_Relaciones.RequestExternalPage
        MyMaster.mostrarPopUp(laRuta)
    End Sub

    Protected Sub btn_Accion_Ayuda_Click(ByVal Sender As Object, ByVal E As System.EventArgs) Handles btn_Accion_Ayuda.Click
        MyMaster.MostrarMensaje("Puede crear y editar la información de las <b>Entidades</b>.<br /><br />Según el <b>Tipo de Entidad</b> que seleccione, se le pedirá <b>Otros Atributos</b>.<br /><br />Recuerde que debe seleccionar una <b>Ubicación</b>, pues est&aacute; la default.<br /><br />Además si esta entidad tiene acceso al sistema, debe propiciarle un <b>Usuario</b> y un <b>Password</b>.", False)
    End Sub

    Protected Sub btn_Accion_Ver_Click(ByVal Sender As Object, ByVal E As System.EventArgs) Handles btn_Accion_Ver.Click
        ToLoadPage(Configuration.EstadoPantalla.CONSULTAR)
        udp_entidad.Update()
    End Sub

    Protected Sub btn_Accion_Editar_Click(ByVal Sender As Object, ByVal E As System.EventArgs) Handles btn_Accion_Editar.Click
        ToLoadPage(Configuration.EstadoPantalla.MODIFICAR)
        Me.tabs_Entidad.ActiveTabIndex = 0
        udp_entidad.Update()
    End Sub
End Class
