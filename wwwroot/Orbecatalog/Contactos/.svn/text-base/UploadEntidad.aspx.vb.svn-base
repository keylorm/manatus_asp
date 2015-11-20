Imports Orbelink.DBHandler
Imports Orbelink.Control.Entidades
Imports Orbelink.Entity.Entidades
Imports Orbelink.Entity.CRM

Partial Class _Entidad
    Inherits PageBaseClass

    Const codigo_pantalla As String = "CO-13"
    Const level As Integer = 2

    'Pagina
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddScripts(level)
        securityHandler.VerifyPantalla(codigo_pantalla, level)

        If Not IsPostBack Then
            ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
            securityHandler.VerifyActions(btn_Salvar, Nothing, Nothing)

            lnk_Grupo.NavigateUrl = "javascript:toggleLayer('" & trv_Grupos.ClientID & "', this)"
            selectGrupos(trv_Grupos)
            selectTipoRelacion_Grupos()

            selectTiposEntidades(securityHandler.TipoEntidad)
            selectUbicaciones(trv_Ubicaciones)

            cargarComboConColumnasExcel(ddl_upl_NombreEntidad)
            cargarComboConColumnasExcel(ddl_upl_Apellido)
            cargarComboConColumnasExcel(ddl_upl_Apellido2)
            cargarComboConColumnasExcel(ddl_upl_Usuario)
            cargarComboConColumnasExcel(ddl_upl_PasswordEntidad)
            cargarComboConColumnasExcel(ddl_upl_Telefono)
            cargarComboConColumnasExcel(ddl_upl_Email)
            cargarComboConColumnasExcel(ddl_upl_Celular)
            cargarComboConColumnasExcel(ddl_upl_DependienteTexto)
            cargarComboConColumnasExcel(ddl_upl_identificacion)
            cargarComboConColumnasExcel(ddl_upl_FechaImportante)
            cargarComboConColumnasExcel(ddl_upl_Descripcion)
            cargarCuentas()
            cargarRelaciones()
        End If
    End Sub

    Protected Sub ClearInfo(ByVal estado As Configuration.EstadoPantalla, ByVal clearInfo As Boolean)
        If clearInfo Then
            ddl_upl_NombreEntidad.SelectedIndex = 0
            ddl_upl_Apellido.SelectedIndex = 0
            ddl_upl_Apellido2.SelectedIndex = 0
            ddl_upl_Usuario.SelectedIndex = 0
            ddl_upl_PasswordEntidad.SelectedIndex = 0
            ddl_upl_Telefono.SelectedIndex = 0
            ddl_upl_Email.SelectedIndex = 0
            ddl_upl_Celular.SelectedIndex = 0
            vld_Ubicacion.IsValid = True
        End If

        Select Case estado
            Case Configuration.EstadoPantalla.NORMAL
                btn_Salvar.Visible = True
                trv_Ubicaciones.Visible = False

            Case Configuration.EstadoPantalla.CONSULTAR
                btn_Salvar.Visible = False

        End Select
    End Sub

    'Entidad
    Protected Function MapearEntidad(ByRef theRow As Data.DataRow) As Entidad
        Dim entidad As Entidad = Nothing

        If ddl_upl_NombreEntidad.SelectedIndex > 0 Then
            If Not theRow.IsNull(ddl_upl_NombreEntidad.SelectedIndex - 1) Then
                entidad = New Entidad
                entidad.NombreEntidad.Value = theRow.Item(ddl_upl_NombreEntidad.SelectedIndex - 1)
                entidad.Id_tipoEntidad.Value = ddl_Tipoentidad.SelectedValue
                entidad.Id_Ubicacion.Value = lnk_Ubicacion.ToolTip

                If ddl_upl_Apellido.SelectedIndex > 0 Then
                    If Not theRow.IsNull(ddl_upl_Apellido.SelectedIndex - 1) Then
                        entidad.Apellido.Value = theRow.Item(ddl_upl_Apellido.SelectedIndex - 1)
                        entidad.NombreDisplay.Value &= " " & entidad.Apellido.Value

                        If ddl_upl_Apellido2.SelectedIndex > 0 Then
                            If Not theRow.IsNull(ddl_upl_Apellido2.SelectedIndex - 1) Then
                                entidad.Apellido2.Value = theRow.Item(ddl_upl_Apellido2.SelectedIndex - 1)
                                entidad.NombreDisplay.Value &= " " & entidad.Apellido2.Value
                            End If
                        End If
                    End If
                End If

                If ddl_upl_Usuario.SelectedIndex > 0 And ddl_upl_PasswordEntidad.SelectedIndex > 0 Then
                    If Not theRow.IsNull(ddl_upl_Usuario.SelectedIndex - 1) And Not theRow.IsNull(ddl_upl_PasswordEntidad.SelectedIndex - 1) Then
                        entidad.UserName.Value = theRow(ddl_upl_Usuario.SelectedIndex - 1)
                        entidad.Password.Value = theRow(ddl_upl_PasswordEntidad.SelectedIndex - 1)
                    End If
                End If

                If ddl_upl_Telefono.SelectedIndex > 0 Then
                    If Not theRow.IsNull(ddl_upl_Telefono.SelectedIndex - 1) Then
                        entidad.Telefono.Value = theRow.Item(ddl_upl_Telefono.SelectedIndex - 1)
                    End If
                End If

                If ddl_upl_Email.SelectedIndex > 0 Then
                    If Not theRow.IsNull(ddl_upl_Email.SelectedIndex - 1) Then
                        entidad.Email.Value = theRow.Item(ddl_upl_Email.SelectedIndex - 1)
                    End If
                End If

                If ddl_upl_Celular.SelectedIndex > 0 Then
                    If Not theRow.IsNull(ddl_upl_Celular.SelectedIndex - 1) Then
                        entidad.Celular.Value = theRow.Item(ddl_upl_Celular.SelectedIndex - 1)
                    End If
                End If

                If ddl_upl_identificacion.SelectedIndex > 0 Then
                    If Not theRow.IsNull(ddl_upl_identificacion.SelectedIndex - 1) Then
                        entidad.Identificacion.Value = theRow.Item(ddl_upl_identificacion.SelectedIndex - 1)
                    End If
                End If

                If ddl_upl_FechaImportante.SelectedIndex > 0 Then
                    If Not theRow.IsNull(ddl_upl_FechaImportante.SelectedIndex - 1) Then
                        entidad.FechaImportante.ValueLocalized = theRow.Item(ddl_upl_FechaImportante.SelectedIndex - 1)
                    End If
                End If

                If ddl_upl_Descripcion.SelectedIndex > 0 Then
                    If Not theRow.IsNull(ddl_upl_Descripcion.SelectedIndex - 1) Then
                        entidad.Descripcion.Value = theRow.Item(ddl_upl_Descripcion.SelectedIndex - 1)
                    End If
                End If
            End If
        End If
        Return entidad
    End Function

    Protected Function BuscarEntidadBaseTexto(ByRef controladoraEntidad As EntidadHandler, ByRef theRow As Data.DataRow, ByVal tipoBase As Integer, ByVal ubicacion As Integer) As Integer
        Dim Id_entidadBase As Integer = -1
        If ddl_upl_DependienteTexto.SelectedIndex > 0 Then
            If Not theRow.IsNull(ddl_upl_DependienteTexto.SelectedIndex - 1) Then
                Dim textoBuscar As String = theRow(ddl_upl_DependienteTexto.SelectedIndex - 1)

                If textoBuscar.Length > 0 Then
                    Dim laEntidad As Entidad = controladoraEntidad.buscarEntidad_ByNombre(textoBuscar)

                    If laEntidad.Id_entidad.IsValid Then
                        If laEntidad.Id_entidad.Value > 0 Then
                            Id_entidadBase = laEntidad.Id_entidad.Value
                        End If
                    Else
                        Dim resultado As EntidadHandler.Resultado = controladoraEntidad.CrearEntidad_Minimo(tipoBase, textoBuscar, ubicacion)
                        Id_entidadBase = resultado.id_Entidad
                    End If
                End If
            End If
        End If
        Return Id_entidadBase
    End Function

    'Usuarios
    Protected Function existUserName(ByVal username As String) As Boolean
        Dim exist As Boolean = False
        Dim dataSet As Data.DataSet
        Dim entidad As New Entidad

        entidad.Id_entidad.ToSelect = True
        If username.Length > 0 Then
            entidad.UserName.Where.EqualCondition(username)
            queryBuilder.From.Add(entidad)
            dataSet = connection.executeSelect(queryBuilder.RelationalSelectQuery)

            If dataSet.Tables.Count > 0 Then
                If dataSet.Tables(0).Rows.Count > 0 Then
                    exist = True
                End If
            End If
        End If
        Return exist
    End Function

    Protected Function existEmail(ByVal email As String) As Boolean
        Dim exist As Boolean = False
        Dim dataSet As Data.DataSet
        Dim entidad As New Entidad

        entidad.Id_entidad.ToSelect = True
        If email.Length > 0 Then
            entidad.Email.Where.EqualCondition(email)
            queryBuilder.From.Add(entidad)
            dataSet = connection.executeSelect(queryBuilder.RelationalSelectQuery)

            If dataSet.Tables.Count > 0 Then
                If dataSet.Tables(0).Rows.Count > 0 Then
                    exist = True
                End If
            End If
        End If
        Return exist
    End Function

    'TipoEntidad
    Protected Sub selectTiposEntidades(ByVal id_tipoEntidad As Integer)
        Dim dataset As Data.DataSet
        Dim tipoEntidad As New TipoEntidad

        tipoEntidad.Fields.SelectAll()
        dataset = connection.executeSelect(queryBuilder.SelectQuery(tipoEntidad))
        If dataset.Tables.Count > 0 Then
            If dataset.Tables(0).Rows.Count > 0 Then
                ddl_Tipoentidad.DataSource = dataset
                ddl_Tipoentidad.DataTextField = tipoEntidad.Nombre.Name
                ddl_Tipoentidad.DataValueField = tipoEntidad.Id_TipoEntidad.Name
                ddl_Tipoentidad.DataBind()
            End If
        End If
    End Sub

    'Ubicaciones
    Protected Sub selectUbicaciones(ByVal theTreeView As TreeView)
        Dim ubicacion As New Ubicacion
        Dim dataset As Data.DataSet
        Dim counter As Integer
        Dim primero As Boolean = True

        ubicacion.Fields.SelectAll()
        ubicacion.Id_ubicacion.Where.EqualCondition_OnSameTable(ubicacion.Id_padre)
        dataset = connection.executeSelect(queryBuilder.SelectQuery(ubicacion))
        If dataset.Tables.Count > 0 Then
            Dim resul_ubicacion As ArrayList = ObjectBuilder.TransformDataTable(dataset.Tables(0), ubicacion)

            For counter = 0 To dataset.Tables(0).Rows.Count - 1
                Dim act_ubicacion As Ubicacion = resul_ubicacion(counter)
                Dim hijo As New TreeNode(act_ubicacion.Nombre.Value, act_ubicacion.Id_ubicacion.Value)
                theTreeView.Nodes.Add(hijo)
                If primero Then
                    lnk_Ubicacion.Text = act_ubicacion.Nombre.Value
                    lnk_Ubicacion.ToolTip = act_ubicacion.Id_ubicacion.Value
                    primero = False
                End If
                selectUbicacionesHijos(theTreeView.Nodes(counter))
            Next
        End If
        theTreeView.CollapseAll()
    End Sub

    Protected Sub selectUbicacionesHijos(ByVal elTreeNode As TreeNode)
        Dim padre_id_ubicacion As Integer = elTreeNode.Value
        Dim ubicacion As New Ubicacion
        Dim dataset As Data.DataSet
        Dim counter As Integer

        ubicacion.Fields.SelectAll()
        ubicacion.Id_padre.Where.EqualCondition(padre_id_ubicacion)
        ubicacion.Id_ubicacion.Where.DiferentCondition(padre_id_ubicacion)
        dataset = connection.executeSelect(queryBuilder.SelectQuery(ubicacion))

        For counter = 0 To dataset.Tables(0).Rows.Count - 1
            ObjectBuilder.CreateObject(dataset.Tables(0), counter, ubicacion)   'cambiar por transform
            Dim hijo As New TreeNode(ubicacion.Nombre.Value, ubicacion.Id_ubicacion.Value)
            selectUbicacionesHijos(hijo)
            elTreeNode.ChildNodes.Add(hijo)
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

    'Entidades_Grupo
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

    'TipoRelacion_Grupos
    Protected Sub selectTipoRelacion_Grupos()
        Dim dataSet As Data.DataSet
        Dim TipoRelacion_Entidades As New TipoRelacion_Grupos

        TipoRelacion_Entidades.Id_TipoRelacion.ToSelect = True
        TipoRelacion_Entidades.Nombre.ToSelect = True
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

    'Relacion Entidad
    Protected Function insertRelacion_Entidades(ByVal entidadDependiente As Integer, ByVal tipo_relacion As Integer) As Boolean
        'Try
        '    Dim tipo As New TipoRelacion_Entidad_Cuenta
        '    tipo.Id_TipoRelacion.ToSelect = True
        '    tipo.Id_TipoRelacion.Where.EqualCondition(tipo_relacion)
        '    Dim dataSet As New Data.DataSet
        '    Dim query As String = queryBuilder.SelectQuery(tipo)
        '    dataSet = connection.executeSelect(query)
        '    If dataSet.Tables.Count > 0 Then
        '        If dataSet.Tables(0).Rows.Count > 0 Then
        '            Try
        '                Dim relacion As New Relacion_Entidad_Cuenta
        '                relacion.id_Cuenta.Value = ddl_cuentas.SelectedValue
        '                relacion.id_entidad.Value = entidadDependiente
        '                relacion.id_TipoRelacion.Value = tipo_relacion
        '                connection.executeInsert(queryBuilder.InsertQuery(relacion))
        '                Return True
        '            Catch exc As Exception
        '                MyMaster.MostrarMensaje(exc.Message, True)
        '                Return False
        '            End Try
        '        End If
        '    End If
        'Catch ex As Exception
        '    MyMaster.MostrarMensaje(ex.Message, True)
        '    Return False
        'End Try
    End Function

    'Combos de Excel
    Sub cargarComboConColumnasExcel(ByRef theDDL As DropDownList)
        theDDL.Items.Add(" -- ")
        For counter As Integer = 0 To 25
            Dim caracter As Char = Char.ConvertFromUtf32(counter + 65)
            theDDL.Items.Add(caracter)
        Next
        theDDL.SelectedIndex = 0
    End Sub

    'Eventos
    Protected Sub btn_Salvar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Salvar.Click
        Dim cantidad As Integer
        If lnk_Ubicacion.ToolTip.Length = 0 Then
            vld_Ubicacion.IsValid = False
            MyMaster.MostrarMensaje("Debe seleccionar una ubicacion.", False)
        Else
            vld_Ubicacion.IsValid = True
            Dim dataset As Data.DataSet = Orbelink.Helpers.CommonTasks.cargadelexcelDS(Me.upl_Archivo, tbx_upl_NombreHoja.Text)

            If dataset.Tables.Count > 0 Then
                If dataset.Tables(0).Rows.Count > 0 Then

                    'Recorre todo el excel
                    'Dim ultimoBase As Integer = 0
                    For counter As Integer = 0 To dataset.Tables(0).Rows.Count - 1
                        Dim theRow As Data.DataRow = dataset.Tables(0).Rows(counter)
                        Dim entidad As Entidad = MapearEntidad(theRow)
                        If entidad IsNot Nothing Then
                            Dim controladoraEntidad As New EntidadHandler(Configuration.Config_DefaultConnectionString)
                            Dim resultado As EntidadHandler.Resultado = controladoraEntidad.CrearEntidad(entidad)
                            If resultado.code = EntidadHandler.ResultCodes.OK Then
                                For Each item As ListItem In lst_Grupos.Items
                                    insertEntidades_Grupo(resultado.id_Entidad, item.Value, ddl_TipoRelacion_Grupos.SelectedValue)
                                Next

                                If ddl_cuentas.Items.Count > 0 Then
                                    If ddl_cuentas.SelectedValue > 0 And ddl_upl_DependienteTexto.SelectedIndex > 0 Then
                                        insertRelacion_Entidades(resultado.id_Entidad, theRow.Item(ddl_upl_DependienteTexto.SelectedIndex - 1))
                                        verificarContacto(resultado.id_Entidad)
                                    End If
                                End If
                                cantidad += 1
                            End If
                        End If
                    Next
                    upd_Contenido.Update()
                    upd_Busqueda.Update()

                    MyMaster.MostrarMensaje("Entidades salvadas exitosamente: " & cantidad, False)
                Else
                    MyMaster.MostrarMensaje("No hay registros en esa hoja de excel", False)
                End If
            Else
                MyMaster.MostrarMensaje("No hay tabla que procesar", False)
            End If
        End If
    End Sub

    Protected Sub btn_Cancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Cancelar.Click
        'Dim commonTasks As New Orbelink.Helpers.CommonTasks(connection)
        'Response.Redirect(commonTasks.RemoveQueryString(Request, "id_Entidad"))
    End Sub

    Protected Sub trv_Ubicaciones_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles trv_Ubicaciones.SelectedNodeChanged
        Dim id_NodoActual As Integer = trv_Ubicaciones.SelectedNode.Value
        lnk_Ubicacion.Text = trv_Ubicaciones.SelectedNode.Text
        lnk_Ubicacion.ToolTip = id_NodoActual
        trv_Ubicaciones.Visible = False
    End Sub

    Protected Sub lnk_Ubicacion_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnk_Ubicacion.Click
        trv_Ubicaciones.Visible = True
    End Sub

    Protected Sub trv_Grupos_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles trv_Grupos.SelectedNodeChanged
        Dim id_NodoActual As Integer = trv_Grupos.SelectedNode.Value
        lnk_Grupo.Text = trv_Grupos.SelectedNode.Text
        lnk_Grupo.ToolTip = id_NodoActual
    End Sub

    Protected Sub btn_AgregarAGrupo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_AgregarAGrupo.Click

        If lnk_Grupo.ToolTip.Length > 0 Then
            lst_Grupos.Items.Add(lnk_Grupo.Text)
            lst_Grupos.Items(lst_Grupos.Items.Count - 1).Value = lnk_Grupo.ToolTip
        Else
            MyMaster.MostrarMensaje("Debe seleccionar un Grupo.", False)
        End If
        upd_Busqueda.Update()
    End Sub

    Protected Sub btn_QuitarDeGrupo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_QuitarDeGrupo.Click

        If lst_Grupos.SelectedValue.Length > 0 Then
            lst_Grupos.Items.RemoveAt(lst_Grupos.SelectedIndex)
        Else
            MyMaster.MostrarMensaje("Debe seleccionar un Grupo.", False)
        End If
        upd_Busqueda.Update()
    End Sub

    Protected Sub cargarCuentas()
        'Dim cuenta As New Cuenta
        'cuenta.Id_Cuenta.ToSelect = True
        'cuenta.Nombre.ToSelect = True
        'Dim dataSet As New Data.DataSet
        'Dim query As String = queryBuilder.SelectQuery(cuenta)
        'dataSet = connection.executeSelect(query)
        'If dataSet.Tables.Count > 0 Then
        '    If dataSet.Tables(0).Rows.Count > 0 Then
        '        ddl_cuentas.DataSource = dataSet
        '        ddl_cuentas.DataTextField = cuenta.Nombre.Name
        '        ddl_cuentas.DataValueField = cuenta.Id_Cuenta.Name
        '        ddl_cuentas.DataBind()
        '        Dim item As New ListItem("Ninguna", -1)
        '        ddl_cuentas.Items.Add(item)
        '        ddl_cuentas.SelectedIndex = ddl_cuentas.Items.Count - 1
        '    End If
        'End If
    End Sub

    Protected Sub cargarRelaciones()
        'Dim tipo As New TipoRelacion_Entidad_Cuenta
        'tipo.Id_TipoRelacion.AsName = "Numero"
        'tipo.Nombre_Entidad_Cuenta.AsName = "Nombre"
        'tipo.Id_TipoRelacion.ToSelect = True
        'tipo.Nombre_Entidad_Cuenta.ToSelect = True
        'Dim dataSet As New Data.DataSet
        'Dim query As String = queryBuilder.SelectQuery(tipo)
        'dataSet = connection.executeSelect(query)
        'If dataSet.Tables.Count > 0 Then
        '    If dataSet.Tables(0).Rows.Count > 0 Then
        '        dg_relaciones.DataSource = dataSet
        '        dg_relaciones.DataBind()
        '    End If
        'End If
    End Sub

    Protected Sub verificarContacto(ByVal id_entidad As Integer)
        'Dim contacto As New Contacto
        'contacto.Id_Entidad.ToSelect = True
        'contacto.Id_Entidad.Where.EqualCondition(id_entidad)
        'Dim dataSet As New Data.DataSet
        'Dim query As String = queryBuilder.SelectQuery(contacto)
        'dataSet = connection.executeSelect(query)
        'If dataSet.Tables.Count > 0 Then
        '    If Not dataSet.Tables(0).Rows.Count > 0 Then
        '        Dim elContacto As New Contacto
        '        elContacto.Id_Entidad.Value = id_entidad
        '        elContacto.CreadoPor.Value = securityHandler.Usuario
        '        elContacto.FechaCreado.ValueUtc = Date.UtcNow
        '        elContacto.Prioridad.Value = 1
        '        Dim consulta As String = queryBuilder.InsertQuery(elContacto)
        '        connection.executeInsert(consulta)
        '    End If
        'End If
    End Sub

End Class
