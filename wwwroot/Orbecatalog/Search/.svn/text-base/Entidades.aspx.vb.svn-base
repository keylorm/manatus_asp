Imports Orbelink.DBHandler
Imports Orbelink.Entity.Envios
Imports Orbelink.Entity.Entidades
Imports Orbelink.DateAndTime

Partial Class _Entidades
    Inherits PageBaseClass

    Const codigo_pantalla As String = "CO-SR"
    Const level As Integer = 2

    'Pagina
    Protected Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
        AddScripts(level)
        securityHandler.VerifyPantalla(codigo_pantalla, level)

        If Not IsPostBack Then
            'securityHandler.VerifyActions(btn_Salvar, btn_Modificar, btn_Eliminar)

            selectGrupos()
            selectTiposRelacion_Entidades()
            selectEnvios()

            pnl_Atributos.Visible = False
            selectTipoEntidad(securityHandler.ClearanceLevel)

            lnk_Busqueda.NavigateUrl = "javascript:toggleLayer('" & pnl_Busqueda.ClientID & "', this)"
            lnk_Ubicacion.NavigateUrl = "javascript:toggleLayer('" & trv_Ubicaciones.ClientID & "', this)"

            DesarmarQueryString()
        End If
    End Sub

    Protected Sub DesarmarQueryString()
        Dim id_ubicacion As Integer
        Dim id_tipoEntidad As Integer
        Dim nombre As String = ""
        Dim apellido As String = ""
        Dim id_tipoRelacion As Integer
        Dim searchFor As String = ""
        Dim Grupos As Integer
        Dim general As Boolean = False

        If Not Request.QueryString("search") Is Nothing Then
            lbl_Resultado.Text = ""

            If Not Request.QueryString("searchFor") Is Nothing Then
                searchFor = Request.QueryString("searchFor")
            End If

            If Not Request.QueryString("ubicacion") Is Nothing Then
                id_ubicacion = Request.QueryString("ubicacion")
            End If

            If Not Request.QueryString("tipoEntidad") Is Nothing Then
                id_tipoEntidad = Request.QueryString("tipoEntidad")
                ddl_Filtro_TipoEntidades.SelectedValue = Request.QueryString("tipoEntidad")
            End If

            If Not Request.QueryString("nombre") Is Nothing Then
                nombre = Request.QueryString("nombre")
                tbx_NombreEntidad_Search.Text = Request.QueryString("nombre")
            End If

            If Not Request.QueryString("apellido") Is Nothing Then
                apellido = Request.QueryString("apellido")
                tbx_Apellido_Search.Text = Request.QueryString("apellido")
            End If

            If Not Request.QueryString("id_tipoRelacion") Is Nothing Then
                id_tipoRelacion = Request.QueryString("id_tipoRelacion")
                ddl_TipoRelaciones.SelectedValue = Request.QueryString("id_tipoRelacion")
                If Not Request.QueryString("Grupos") Is Nothing Then
                    Grupos = Request.QueryString("Grupos")
                    ddl_Grupos.SelectedValue = Request.QueryString("Grupos")
                End If
            End If

            If Not Request.QueryString("pageSize") Is Nothing Then
                dg_Entidades.PageSize = Request.QueryString("pageSize")
                ddl_PageSize.SelectedValue = Request.QueryString("pageSize")
            End If

            If Not Request.QueryString("pageNumber") Is Nothing Then
                dg_Entidades.CurrentPageIndex = Request.QueryString("pageNumber")
            End If

            If Not Request.QueryString("general") Is Nothing Then
                general = True
            End If

            If Not ddl_Filtro_TipoEntidades.SelectedIndex = ddl_Filtro_TipoEntidades.Items.Count - 1 Then
                selectAtributos(ddl_Filtro_TipoEntidades.SelectedValue)
            Else
                dg_Atributos.Visible = False
            End If
            selectEntidades(securityHandler.ClearanceLevel, searchFor, id_ubicacion, id_tipoEntidad, nombre, apellido, id_tipoRelacion, Grupos, general, obtenerInQuerys)

        Else
            div_NoResultado.Visible = False
            lbl_Resultado.Text = "Utilice los filtros para buscar"
        End If

        If Request.QueryString("general") Is Nothing Then
            selectUbicaciones(trv_Ubicaciones, id_ubicacion)
        End If
    End Sub

    Protected Function ArmarQueryString(ByVal pageNumber As Integer) As String
        Dim queryString As String = "Entidades.aspx?search=true"

        If Not Request.QueryString("searchFor") Is Nothing Then
            queryString &= "&searchFor=" & Request.QueryString("searchFor")
        End If

        If lnk_Ubicacion.ToolTip.Length > 0 Then
            queryString &= "&ubicacion=" & lnk_Ubicacion.ToolTip
        End If
        If Not ddl_Filtro_TipoEntidades.SelectedIndex = ddl_Filtro_TipoEntidades.Items.Count - 1 Then
            queryString &= "&tipoEntidad=" & ddl_Filtro_TipoEntidades.SelectedValue
        End If
        If tbx_NombreEntidad_Search.Text.Length > 0 Then
            queryString &= "&nombre=" & tbx_NombreEntidad_Search.Text
        End If
        If tbx_Apellido_Search.Text.Length > 0 Then
            queryString &= "&apellido=" & tbx_Apellido_Search.Text
        End If
        If Not ddl_TipoRelaciones.SelectedIndex = ddl_TipoRelaciones.Items.Count - 1 Then
            queryString &= "&id_TipoRelacion=" & ddl_TipoRelaciones.SelectedValue
            If Not ddl_Grupos.SelectedIndex = ddl_Grupos.Items.Count - 1 Then
                queryString &= "&Grupos=" & ddl_Grupos.SelectedValue
            End If
        End If

        queryString &= crearInQuerys()

        queryString &= "&pageNumber=" & pageNumber
        queryString &= "&pageSize=" & ddl_PageSize.SelectedValue
        Return queryString
    End Function

    'Entidad
    Protected Sub selectEntidades(ByVal nivel As Integer, ByVal searchFor As String, ByVal id_ubicacion As Integer, ByVal id_tipoEntidad As Integer, ByVal nombre As String, _
        ByVal apellido As String, ByVal id_TipoRelacion As Integer, ByVal Grupos As Integer, ByVal general As Boolean, Optional ByVal inQuerys As ArrayList = Nothing)
        Dim entidad As New Entidad
        Dim TipoEntidad As New TipoEntidad
        Dim Entidades_Grupo As New Entidades_Grupo

        entidad.Id_entidad.ToSelect = True
        entidad.NombreDisplay.ToSelect = True
        entidad.Email.ToSelect = True
        entidad.Telefono.ToSelect = True
        entidad.Celular.ToSelect = True
        entidad.UserName.ToSelect = True

        'Dim temporal As New HyperLink
        'temporal.Text = "Seleccionar para "
        'securityHandler.VerifySearchFor(temporal, level, securityHandler.TipoEntidad)

        Try
            Dim resultTable As Data.DataTable
            If general Then
                If Not Request.QueryString("general") Is Nothing Then
                    Dim buscar As String = Request.QueryString("general")
                    If buscar.Length > 0 Then
                        entidad.NombreDisplay.Where.LikeCondition(buscar)
                    End If
                End If
                Dim consulta As String = queryBuilder.SelectQuery(entidad)
                resultTable = connection.executeSelect_DT(consulta)

            Else
                TipoEntidad.Nivel.Where.LessThanCondition(nivel)
                queryBuilder.Join.EqualCondition(TipoEntidad.Id_TipoEntidad, entidad.Id_tipoEntidad)

                'Busca si hay filtro
                If id_tipoEntidad <> 0 Then
                    entidad.Id_tipoEntidad.Where.EqualCondition(id_tipoEntidad)
                End If

                If id_ubicacion <> 0 Then
                    entidad.Id_Ubicacion.Where.EqualCondition(id_ubicacion)
                End If

                If nombre.Length > 0 Then
                    entidad.NombreEntidad.Where.LikeCondition(nombre)
                End If

                If apellido.Length > 0 Then
                    entidad.Apellido.Where.LikeCondition(apellido)
                End If

                If id_TipoRelacion > 0 Then
                    Entidades_Grupo.Id_TipoRelacion.Where.EqualCondition(id_TipoRelacion)
                    If Grupos <> 0 Then
                        queryBuilder.Join.EqualCondition(entidad.Id_entidad, Entidades_Grupo.Id_Entidad)
                        'Entidades_Grupo.Id_EntidadBase.Where.EqualCondition(Grupos)
                        queryBuilder.From.Add(Entidades_Grupo)
                    End If
                End If

                If Not inQuerys Is Nothing Then
                    Dim counter As Integer
                    For counter = 0 To inQuerys.Count - 1
                        entidad.Id_entidad.Where.InCondition(inQuerys(counter))
                    Next
                End If

                'Revisar que pueda ordenar por algo...
                queryBuilder.Distinct = True
                queryBuilder.Orderby.Add(entidad.NombreDisplay)
                queryBuilder.From.Add(entidad)
                queryBuilder.From.Add(TipoEntidad)
                resultTable = connection.executeSelect_DT(queryBuilder.RelationalSelectQuery)
            End If

            If resultTable.Rows.Count > 0 Then
                dg_Entidades.DataSource = resultTable
                dg_Entidades.DataKeyField = entidad.Id_entidad.Name
                dg_Entidades.DataBind()

                'Llena el grid
                Dim offset As Integer = dg_Entidades.CurrentPageIndex * dg_Entidades.PageSize
                For counter As Integer = 0 To dg_Entidades.Items.Count - 1
                    Dim act_Entidad As Entidad = ObjectBuilder.CreateDBTable_BySelectIndex(resultTable.Rows(offset + counter), entidad)

                    Dim lnk_Email As HyperLink = dg_Entidades.Items(counter).FindControl("lnk_Email")
                    Dim lbl_Telefono As Label = dg_Entidades.Items(counter).FindControl("lbl_Telefono")
                    Dim lnk_Entidad As HyperLink = dg_Entidades.Items(counter).FindControl("lnk_Entidad")
                    Dim chk_Control As CheckBox = dg_Entidades.Items(counter).FindControl("chk_Control")
                    Dim lbl_Enviado As Label = dg_Entidades.Items(counter).FindControl("lbl_Enviado")

                    lnk_Entidad.Text = act_Entidad.NombreDisplay.Value

                    lnk_Email.Text = act_Entidad.Email.Value
                    lnk_Email.NavigateUrl = "mailto:" & act_Entidad.Email.Value
                    lbl_Telefono.Text = act_Entidad.Telefono.Value

                    lnk_Entidad.NavigateUrl = "../Contactos/Entidad.aspx?id_Entidad=" & act_Entidad.Id_entidad.Value
                    lbl_Enviado.Visible = False
                    If act_Entidad.Email.IsValid = False Then
                        chk_Control.Enabled = False
                    Else
                        chk_Control.Enabled = True
                    End If

                    'If temporal.Visible Then
                    '    lnk_SearchFor.Text = temporal.Text
                    '    lnk_SearchFor.NavigateUrl = temporal.NavigateUrl & "&id_Entidad=" & act_Entidad.Id_entidad.Value
                    '    lnk_SearchFor.Visible = True
                    'Else
                    '    lnk_SearchFor.Visible = False
                    'End If

                    'Javascript
                    dg_Entidades.Items(counter).Attributes.Add("onmouseover", "setRowFocus(this, " & counter & ", false)")
                    dg_Entidades.Items(counter).Attributes.Add("onmouseout", "unsetRowFocus(this, " & counter & ", false)")
                Next
                lbl_Resultado.Text = contadorElementos(offset, dg_Entidades.PageSize, resultTable.Rows.Count)
                dg_Entidades.Visible = True
                div_NoResultado.Visible = False
                pnl_Envios.Visible = True
            Else
                pnl_Envios.Visible = False
                dg_Entidades.Visible = False
                div_NoResultado.Visible = True
            End If
        Catch ex As Exception
            MyMaster.MostrarMensaje("Error al realizar la busqueda", True)
        End Try
    End Sub

    'TipoEntidad
    Protected Sub selectTipoEntidad(ByVal nivel As Integer)
        Dim dataset As Data.DataSet
        Dim tipoEntidad As New TipoEntidad

        tipoEntidad.Fields.SelectAll()
        tipoEntidad.Nivel.Where.LessThanCondition(nivel)
        queryBuilder.Orderby.Add(tipoEntidad.Nombre)
        dataset = connection.executeSelect(queryBuilder.SelectQuery(tipoEntidad))
        If dataset.Tables.Count > 0 Then
            If dataset.Tables(0).Rows.Count > 0 Then
                ddl_Filtro_TipoEntidades.DataSource = dataset
                ddl_Filtro_TipoEntidades.DataTextField = tipoEntidad.Nombre.Name
                ddl_Filtro_TipoEntidades.DataValueField = tipoEntidad.Id_TipoEntidad.Name
                ddl_Filtro_TipoEntidades.DataBind()
            End If
        End If
        ddl_Filtro_TipoEntidades.Items.Add("-- Todos --")
        ddl_Filtro_TipoEntidades.SelectedIndex = ddl_Filtro_TipoEntidades.Items.Count - 1
    End Sub

    'Ubicaciones
    Protected Sub selectUbicaciones(ByVal theTreeView As TreeView, ByVal id_ubicacion As Integer)
        Dim ubicacion As New Ubicacion
        Dim dataset As Data.DataSet
        Dim counter As Integer
        ubicacion.Fields.SelectAll()
        ubicacion.Id_ubicacion.Where.EqualCondition_OnSameTable(ubicacion.Id_padre)
        Dim consulta As String = queryBuilder.SelectQuery(ubicacion)
        dataset = connection.executeSelect(consulta)
        If dataset.Tables.Count > 0 Then
            Dim resul_ubicacion As ArrayList = ObjectBuilder.TransformDataTable(dataset.Tables(0), ubicacion)

            For counter = 0 To dataset.Tables(0).Rows.Count - 1
                Dim act_ubicacion As Ubicacion = resul_ubicacion(counter)
                Dim hijo As New TreeNode(act_ubicacion.Nombre.Value, act_ubicacion.Id_ubicacion.Value)
                theTreeView.Nodes.Add(hijo)
                If act_ubicacion.Id_ubicacion.Value = id_ubicacion Then
                    lnk_Ubicacion.Text = act_ubicacion.Nombre.Value
                    lnk_Ubicacion.ToolTip = id_ubicacion
                End If
                selectUbicacionesHijos(theTreeView.Nodes(counter), id_ubicacion)
            Next
        End If
        theTreeView.CollapseAll()
    End Sub

    Protected Sub selectUbicacionesHijos(ByVal elTreeNode As TreeNode, ByVal id_ubicacion As Integer)
        Dim padre_id_ubicacion As Integer = elTreeNode.Value
        Dim ubicacion As New Ubicacion
        Dim dataset As Data.DataSet
        Dim counter As Integer

        ubicacion.Fields.SelectAll()
        ubicacion.Id_padre.Where.EqualCondition(padre_id_ubicacion)
        ubicacion.Id_ubicacion.Where.DiferentCondition(padre_id_ubicacion)
        dataset = connection.executeSelect(queryBuilder.SelectQuery(ubicacion))
        If dataset.Tables.Count > 0 Then
            If dataset.Tables(0).Rows.Count > 0 Then
                Dim resul_ubicacion As ArrayList = ObjectBuilder.TransformDataTable(dataset.Tables(0), ubicacion)

                For counter = 0 To dataset.Tables(0).Rows.Count - 1
                    Dim act_ubicacion As Ubicacion = resul_ubicacion(counter)
                    Dim hijo As New TreeNode(act_ubicacion.Nombre.Value, act_ubicacion.Id_ubicacion.Value)
                    If act_ubicacion.Id_ubicacion.Value = id_ubicacion Then
                        lnk_Ubicacion.Text = act_ubicacion.Nombre.Value
                        lnk_Ubicacion.ToolTip = id_ubicacion
                    End If
                    selectUbicacionesHijos(hijo, id_ubicacion)
                    elTreeNode.ChildNodes.Add(hijo)
                Next
            End If
        End If
    End Sub

    'Otros Atributos
    Protected Sub selectAtributos(ByVal idTipoEntidad As Integer)
        Dim dataSet As Data.DataSet
        Dim Atributos_Para_Entidades As New Atributos_Para_Entidades()
        Dim atribTipoEntidad As New Atributos_TipoEntidad

        Atributos_Para_Entidades.Fields.SelectAll()
        atribTipoEntidad.id_tipoEntidad.Where.EqualCondition(idTipoEntidad)
        queryBuilder.Join.EqualCondition(Atributos_Para_Entidades.Id_atributo, atribTipoEntidad.id_atributo)
        atribTipoEntidad.buscable.Where.EqualCondition(1)

        queryBuilder.Orderby.Add(atribTipoEntidad.orden)
        queryBuilder.From.Add(Atributos_Para_Entidades)
        queryBuilder.From.Add(atribTipoEntidad)
        dataSet = connection.executeSelect(queryBuilder.RelationalSelectQuery())

        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                dg_Atributos.DataSource = dataSet
                dg_Atributos.DataKeyField = Atributos_Para_Entidades.Id_atributo.Name
                dg_Atributos.DataBind()
                dg_Atributos.Visible = True

                'Llena el DataGrid
                Dim counter As Integer
                For counter = 0 To dataSet.Tables(0).Rows.Count - 1
                    Dim lbl_Atributo As Label = dg_Atributos.Items(counter).FindControl("lbl_Atributo")
                    lbl_Atributo.Text = dataSet.Tables(0).Rows(counter).Item(Atributos_Para_Entidades.Nombre.Name)
                    lbl_Atributo.ToolTip = dataSet.Tables(0).Rows(counter).Item(Atributos_Para_Entidades.Id_atributo.Name)
                    Dim autocompletar As Integer = dataSet.Tables(0).Rows(counter).Item(Atributos_Para_Entidades.AutoCompletar.Name)
                    If autocompletar Then
                        Dim ddl_Atributos_Valores As DropDownList = dg_Atributos.Items(counter).FindControl("ddl_Atributos_Valores")
                        Dim tbx_Atributos_Valor As TextBox = dg_Atributos.Items(counter).FindControl("tbx_Atributos_Valor")
                        tbx_Atributos_Valor.Visible = False
                        ddl_Atributos_Valores.Visible = True
                        llenarDDL(ddl_Atributos_Valores, idTipoEntidad, lbl_Atributo.ToolTip)
                    End If
                Next
                pnl_Atributos.Visible = True
            Else
                pnl_Atributos.Visible = False
            End If
        End If
    End Sub

    Protected Sub llenarDDL(ByVal theDDL As DropDownList, ByVal id_TipoEntidad As Integer, ByVal id_Atributo As Integer)
        Dim atributos_Entidad As New Atributos_Entidad
        Dim entidad As New Entidad
        Dim dataSet As Data.DataSet

        atributos_Entidad.Valor.ToSelect = True
        atributos_Entidad.Id_atributo.Where.EqualCondition(id_Atributo)
        queryBuilder.Join.EqualCondition(atributos_Entidad.Id_entidad, entidad.Id_entidad)
        entidad.Id_tipoEntidad.Where.EqualCondition(id_TipoEntidad)

        queryBuilder.From.Add(atributos_Entidad)
        queryBuilder.From.Add(entidad)
        queryBuilder.Distinct = True
        dataSet = connection.executeSelect(queryBuilder.RelationalSelectQuery())

        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                theDDL.DataSource = dataSet
                theDDL.DataTextField = atributos_Entidad.Valor.Name
                theDDL.DataValueField = atributos_Entidad.Valor.Name
                theDDL.DataBind()
            End If
            theDDL.Items.Add("-- Todos --")
            theDDL.SelectedIndex = theDDL.Items.Count - 1
        End If
    End Sub

    Protected Function crearInQuerys() As String
        Dim counter As Integer
        Dim queryString As String = ""

        For counter = 0 To dg_Atributos.Items.Count - 1
            Dim lbl_Atributo As Label = dg_Atributos.Items(counter).FindControl("lbl_Atributo")
            Dim ddl_Atributos_Valores As DropDownList = dg_Atributos.Items(counter).FindControl("ddl_Atributos_Valores")
            Dim tbx_Atributos_Valor As TextBox = dg_Atributos.Items(counter).FindControl("tbx_Atributos_Valor")

            If ddl_Atributos_Valores.Visible Then
                If ddl_Atributos_Valores.SelectedIndex <> ddl_Atributos_Valores.Items.Count - 1 Then
                    queryString &= "&Atributos_Para_Entidades" & lbl_Atributo.ToolTip & "=" & ddl_Atributos_Valores.SelectedValue
                End If
            ElseIf tbx_Atributos_Valor.Text.Length > 0 Then
                queryString &= "&Atributos_Para_Entidades" & lbl_Atributo.ToolTip & "=" & tbx_Atributos_Valor.Text
            End If
        Next
        Return queryString
    End Function

    Protected Function obtenerInQuerys() As ArrayList
        Dim atribEntidad As New Atributos_Entidad
        Dim inQuerys As New ArrayList
        Dim counter As Integer
        Dim counterB As Integer

        For counter = 0 To Request.QueryString.Keys.Count() - 1
            Dim temp As String = Request.QueryString.Keys.Item(counter)

            atribEntidad.Id_entidad.ToSelect = True

            If temp.Contains("Atributos_Para_Entidades") Then
                temp = temp.Substring(5)
                For counterB = 0 To dg_Atributos.Items.Count - 1
                    If dg_Atributos.DataKeys.Item(counterB) = temp Then
                        Dim ddl_Atributos_Valores As DropDownList = dg_Atributos.Items(counterB).FindControl("ddl_Atributos_Valores")
                        Dim tbx_Atributos_Valor As TextBox = dg_Atributos.Items(counterB).FindControl("tbx_Atributos_Valor")
                        If ddl_Atributos_Valores.Visible Then
                            ddl_Atributos_Valores.SelectedValue = Request.QueryString(counter)
                        Else
                            tbx_Atributos_Valor.Text = Request.QueryString(counter)
                        End If
                    End If
                Next

                atribEntidad.Id_atributo.Where.EqualCondition(temp)
                atribEntidad.Valor.Where.LikeCondition(Request.QueryString(counter))
                inQuerys.Add(queryBuilder.SelectQuery(atribEntidad))
            End If
        Next

        Return inQuerys
    End Function

    'Envios
    Protected Sub selectEnvios()
        Dim dataSet As Data.DataSet
        Dim Envio As New Envio

        Envio.Fields.SelectAll()
        queryBuilder.Orderby.Add(Envio.Subject)
        dataSet = connection.executeSelect(queryBuilder.SelectQuery(Envio))

        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                pnl_Envios.Visible = True
                ddl_Envios.DataSource = dataSet
                ddl_Envios.DataValueField = Envio.Id_Envio.Name
                ddl_Envios.DataTextField = Envio.Subject.Name
                ddl_Envios.DataBind()
            Else
                pnl_Envios.Visible = False
            End If
        End If
    End Sub

    'Correos_Envio
    Protected Function insertCorreos_Envio(ByVal email As String, ByVal nombre As String, ByVal id_Envio As Integer, ByVal lote As Integer) As Boolean
        Dim Correos_Envio As New Correos_Envio
        Try
            Correos_Envio.Correo.Value = email
            Correos_Envio.Nombre.Value = nombre
            Correos_Envio.Id_Envio.Value = id_Envio
            Correos_Envio.Lote.Value = lote
            Correos_Envio.Fecha.ValueUtc = Date.UtcNow
            Correos_Envio.Status.Value = Orbelink.Helpers.Mailer.StatusEnvio.ESPERA
            connection.executeInsert(queryBuilder.InsertQuery(Correos_Envio))
            Return True
        Catch exc As Exception
            'MyMaster.mostrarMensaje(exc.Message, true)
            '
            Return False
        End Try
    End Function

    'Lotes_Envio
    Protected Function insertLotes_Envio(ByVal id_Envio As Integer, ByVal lote As Integer) As Boolean
        Dim Lotes_Envio As New Lotes_Envio
        Try
            Lotes_Envio.Id_Envio.Value = id_Envio
            Lotes_Envio.Lote.Value = lote
            Lotes_Envio.Fecha_Creado.ValueUtc = Date.UtcNow
            Lotes_Envio.Enviado.Value = 0
            connection.executeInsert(queryBuilder.InsertQuery(Lotes_Envio))
            Return True
        Catch exc As Exception
            'MyMaster.mostrarMensaje(exc.Message, true)
            '
            Return False
        End Try
    End Function

    Protected Function selectUltimoLotes_Envio(ByVal id_Envio As Integer) As Integer
        Dim dataSet As Data.DataSet
        Dim Lotes_Envio As New Lotes_Envio
        Dim ultimoLote As Integer = -1

        Lotes_Envio.Fields.SelectAll()
        Lotes_Envio.Id_Envio.Where.EqualCondition(id_Envio)
        queryBuilder.Orderby.Add(Lotes_Envio.Lote, False)
        dataSet = connection.executeSelect(queryBuilder.SelectQuery(Lotes_Envio))

        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                ObjectBuilder.CreateObject(dataSet.Tables(0), 0, Lotes_Envio)
                ultimoLote = Lotes_Envio.Lote.Value
            Else
                'pnl_Envios.visible = False
            End If
        End If
        Return ultimoLote
    End Function

    'Relaciones
    Protected Sub selectTiposRelacion_Entidades()
        Dim dataset As Data.DataSet
        Dim TipoRelacion_Entidades As New TipoRelacion_Entidades
        Try
            TipoRelacion_Entidades.Fields.SelectAll()
            dataset = connection.executeSelect(queryBuilder.SelectQuery(TipoRelacion_Entidades))
            If dataset.Tables.Count > 0 Then
                ddl_TipoRelaciones.DataSource = dataset
                ddl_TipoRelaciones.DataTextField = TipoRelacion_Entidades.NombreBaseDependiente.Name
                ddl_TipoRelaciones.DataValueField = TipoRelacion_Entidades.Id_TipoRelacion.Name
                ddl_TipoRelaciones.DataBind()
                ddl_TipoRelaciones.Items.Add("-- Todos --")
                ddl_TipoRelaciones.SelectedIndex = ddl_TipoRelaciones.Items.Count - 1
            End If
        Catch exc As Exception
            MyMaster.MostrarMensaje(exc.Message, True)

        End Try
    End Sub

    'Grupos
    Protected Sub selectGrupos()
        Dim dataSet As Data.DataSet
        Dim Grupos_Entidades As New Grupos_Entidades

        Grupos_Entidades.Id_Grupo.ToSelect = True
        Grupos_Entidades.Nombre.ToSelect = True
        dataSet = connection.executeSelect(queryBuilder.SelectQuery(Grupos_Entidades))

        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                ddl_Grupos.DataSource = dataSet
                ddl_Grupos.DataTextField = Grupos_Entidades.Nombre.Name
                ddl_Grupos.DataValueField = Grupos_Entidades.Id_Grupo.Name
                ddl_Grupos.DataBind()
                ddl_Grupos.Items.Add("-- Todos --")
                ddl_Grupos.SelectedIndex = ddl_Grupos.Items.Count - 1
            End If
        End If
    End Sub

    'Eventos
    Protected Sub dg_Entidades_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dg_Entidades.PageIndexChanged
        Response.Redirect(ArmarQueryString(e.NewPageIndex))
    End Sub

    Protected Sub btn_Buscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Buscar.Click
        Response.Redirect(ArmarQueryString(0))
    End Sub

    Protected Sub ddl_Filtro_TipoEntidades_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddl_Filtro_TipoEntidades.SelectedIndexChanged
        If Not ddl_Filtro_TipoEntidades.SelectedIndex = ddl_Filtro_TipoEntidades.Items.Count - 1 Then
            selectAtributos(ddl_Filtro_TipoEntidades.SelectedValue)
        Else
            dg_Atributos.Visible = False
        End If
    End Sub

    Protected Sub chk_Todos_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim counter As Integer = 0
        Dim chk_Todos As CheckBox = sender
        For counter = 0 To dg_Entidades.Items.Count - 1
            Dim chk_Control As CheckBox = dg_Entidades.Items(counter).FindControl("chk_Control")
            If chk_Control.Enabled Then
                chk_Control.Checked = chk_Todos.Checked
            End If
        Next
        upd_Contenido.Update()
    End Sub

    Protected Sub trv_Ubicaciones_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles trv_Ubicaciones.SelectedNodeChanged
        Dim id_NodoActual As Integer = trv_Ubicaciones.SelectedNode.Value
        lnk_Ubicacion.Text = trv_Ubicaciones.SelectedNode.Text
        lnk_Ubicacion.ToolTip = id_NodoActual
        upd_Ubicacion.Update()
    End Sub

    Protected Sub btn_AgregarEnvio_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_AgregarEnvio.Click
        Dim guardados As Integer
        If ddl_Envios.Items.Count > 0 Then
            Dim id_Envio As Integer = ddl_Envios.SelectedValue
            Dim lote As Integer = selectUltimoLotes_Envio(id_Envio)

            If Not chk_UltimoLote.Checked Then
                lote += 1
                If insertLotes_Envio(id_Envio, lote) Then

                End If
            End If

            For counter As Integer = 0 To dg_Entidades.Items.Count - 1
                Dim lnk_Entidad As HyperLink = dg_Entidades.Items(counter).FindControl("lnk_Entidad")
                Dim lnk_Email As HyperLink = dg_Entidades.Items(counter).FindControl("lnk_Email")
                Dim chk_Control As CheckBox = dg_Entidades.Items(counter).FindControl("chk_Control")
                Dim lbl_Enviado As Label = dg_Entidades.Items(counter).FindControl("lbl_Enviado")

                If chk_Control.Enabled And chk_Control.Checked Then
                    If insertCorreos_Envio(lnk_Email.Text, lnk_Entidad.Text, ddl_Envios.SelectedValue, lote) Then
                        guardados += 1
                        lbl_Enviado.Text = "OK"
                        lbl_Enviado.Visible = True
                        chk_Control.Checked = False
                    End If
                Else
                    lbl_Enviado.Text = ""
                    lbl_Enviado.Visible = False
                    chk_Control.Checked = False
                End If
            Next

            'Muestra el lote
            'MyMaster.mostrarMensaje("Correos guardados exitosamente: ", false) & guardados
            'MyMaster.concatenarMensaje("<br />Lote de este envio: ", false) & lote
            upd_Contenido.Update()
        End If
    End Sub

End Class
