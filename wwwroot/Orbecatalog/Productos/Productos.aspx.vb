Imports Orbelink.DBHandler
Imports Orbelink.Control.Productos
Imports Orbelink.Entity.Productos
Imports Orbelink.Entity.Entidades
Imports Orbelink.Entity.Currency
Imports Orbelink.Entity.Orbecatalog

Partial Class Orbecatalog_Productos
    Inherits PageBaseClass

    Const codigo_pantalla As String = "PR-02"
    Const level As Integer = 2

    'Pagina
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddScripts(level)

        ocwc_Producto_Entidad.SetVariables(Me.queryBuilder, connection, securityHandler, codigo_pantalla, MyMaster)
        ocwc_Producto_Publicacion.SetVariables(Me.queryBuilder, connection, securityHandler, codigo_pantalla, MyMaster)
        'ocwc_Producto_Proyecto.SetVariables(Me.queryBuilder, connection, securityHandler, codigo_pantalla, MyMaster)
        'ocwc_Producto_cuenta.SetVariables(Me.queryBuilder, connection, securityHandler, codigo_pantalla, MyMaster)
        ocwc_DetalleProducto.SetVariables(Me.queryBuilder, connection, securityHandler, codigo_pantalla, MyMaster, level)
        ocwc_ProductoDirecciones.SetVariables(Me.queryBuilder, connection, securityHandler, codigo_pantalla, MyMaster)

        securityHandler.VerifyPantalla(codigo_pantalla, level)

        If Not IsPostBack Then
            Me.Title = Resources.Orbecatalog_Resources.Orbecatalog & " - " & Resources.Productos_Resources.Producto_Pantalla

            securityHandler.VerifyActions(btn_salvar, btn_modificar, btn_eliminar)
            SetThemeProperties()
            SetControlProperties()
            'AgregaAtributosBotones()

            'Datos Extra
            selectTipoProducto(trv_TipoProducto)
            selectOrigenes(trv_Origenes)
            selectEntidad(securityHandler.Entidad)
            selectEstados()
            selectUnidades()
            selectKeywords()
            infoGrupos()
            If lnk_TipoProducto.ToolTip.Length > 0 Then
                selectAtributos(lnk_TipoProducto.ToolTip)
            End If

            'De producto
            If Request.QueryString("id_Producto") IsNot Nothing Then
                Dim controladora As New Orbelink.Control.Productos.ProductosHandler(Configuration.Config_DefaultConnectionString)
                If controladora.existeProducto(Request.QueryString("id_Producto")) Then
                    id_Actual = Request.QueryString("id_Producto")
                End If
            End If

            ToLoadPage()
            cargarTabs()
        End If
    End Sub

    Public Overrides Sub PopUpOkEventHandler(ByVal param As String)
        'Cargar relacionados
        Dim id_producto As Integer
        Dim loadSession As Boolean = False
        If id_Actual Then
            id_producto = id_Actual
        End If

        Select Case param
            Case "Re-02"
                ocwc_Producto_Entidad.cargarRelacionados(id_producto)

            Case "Re-06"
                ocwc_Producto_Publicacion.cargarRelacionados(id_producto)

            Case "Re-05"
                'ocwc_Producto_Proyecto.cargarRelacionados(id_producto)

            Case "PR-03"
                selectTipoProducto(trv_TipoProducto)
                upd_Contenido.Update()

            Case "PR-05"
                If lnk_TipoProducto.ToolTip.Length > 0 Then
                    selectAtributos(lnk_TipoProducto.ToolTip)
                End If
                upd_KeywordsYAtributos.Update()

            Case "PR-06"
                selectKeywords()
                upd_KeywordsYAtributos.Update()

            Case "PR-07"
                selectUnidades()
                upd_Contenido.Update()

            Case "PR-08"
                selectOrigenes(trv_Origenes)
                upd_Origen.Update()

            Case "PR-11"
                selectEstados()
                upd_Contenido.Update()

            Case "PR-15"
                ocwc_ProductoDirecciones.SetIdProducto(id_Actual)
                ocwc_ProductoDirecciones.selectPuntos_Producto(id_Actual)

            Case "PR-18"
                infoGrupos()
                Me.upd_Grupos.Update()

            Case "PR-19"
                infoGrupos()
                Me.upd_Grupos.Update()
        End Select
    End Sub

    Protected Sub ToLoadPage()
        'Obtiene id_Producto
        Dim loadSession As Boolean = False
        If id_Actual > 0 Then
            ClearInfo(Configuration.EstadoPantalla.CONSULTAR, False)
            ocwc_DetalleProducto.cargarInformacion(id_Actual)
            manejarVistas(True)
        Else
            ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
            manejarVistas(False)
        End If

        'If Not Request.QueryString("back") Is Nothing Then
        '    Dim id_ProductoTemp As Integer = LoadSessionWorkingID()
        '    If id_ProductoTemp > 0 Then
        '        id_Producto = id_ProductoTemp
        '    End If
        '    loadSession = True
        'End If

        If loadSession Then
            LoadSessionValues()
        End If
    End Sub

    Protected Sub ClearInfo(ByVal estado As Configuration.EstadoPantalla, ByVal clearInfo As Boolean)
        If clearInfo Then
            tbx_NombreProducto.Text = ""
            Me.id_Actual = 0
            tbx_DescripcionCorta.Text = ""
            tbx_DescripcionLarga.Text = ""
            tbx_Codigo.Text = ""
            tbx_PrecioUnitario.Text = ""
            Chk_Activo.Checked = False

            vld_Producto.IsValid = True
            vld_TipoProducto.IsValid = True
            ddl_Estados.SelectedIndex = 0

            For counter As Integer = 0 To dg_Atributos.Items.Count - 1
                Dim tbx_Atributos_Valor As TextBox = dg_Atributos.Items(counter).FindControl("tbx_Atributos_Valor")
                Dim chk_Atributo_Visible As CheckBox = dg_Atributos.Items(counter).FindControl("chk_Atributo_Visible")
                chk_Atributo_Visible.Checked = False
                tbx_Atributos_Valor.Text = ""
            Next
            For counter As Integer = 0 To dtl_Keywords.Items.Count - 1
                Dim lbl_Keyword As Label = dtl_Keywords.Items(counter).FindControl("lbl_Keyword")
                Dim chk_Keyword As CheckBox = dtl_Keywords.Items(counter).FindControl("chk_Keyword")
                chk_Keyword.Checked = False
                lbl_Keyword.Attributes("InDB") = "No"
            Next
        End If

        Select Case estado
            Case Configuration.EstadoPantalla.NORMAL
                btn_salvar.Visible = True
                btn_modificar.Visible = False
                btn_eliminar.Visible = False
                pnl_Grupos.Style("display") = "none"

                btn_Accion_Editar.Visible = False
                btn_Accion_Ver.Visible = False
                btn_IngresarNuevo.Visible = False

                tab_Archivos.Enabled = False
                tab_grupos.Enabled = False
                tab_Direcciones.Enabled = False
            Case Configuration.EstadoPantalla.MODIFICAR
                btn_Salvar.Visible = False
                btn_Modificar.Visible = True
                btn_eliminar.Visible = True
                pnl_Grupos.Style("display") = "block"

                btn_Accion_Editar.Visible = False
                btn_Accion_Ver.Visible = True
                btn_IngresarNuevo.Visible = True

                tab_Archivos.Enabled = True
                tab_grupos.Enabled = True
                tab_Direcciones.Enabled = True
            Case Configuration.EstadoPantalla.CONSULTAR
                btn_Salvar.Visible = False
                btn_Modificar.Visible = True
                btn_eliminar.Visible = True
                pnl_Grupos.Style("display") = "block"

                btn_Accion_Editar.Visible = True
                btn_Accion_Ver.Visible = False
                btn_IngresarNuevo.Visible = True

                tab_Archivos.Enabled = True
                tab_grupos.Enabled = True
                tab_Direcciones.Enabled = True
        End Select
        upd_BotonesAcciones.Update()
    End Sub

    Protected Sub cargarTabs()
        tabs_Productos.OnClientActiveTabChanged = "verificarTab"
        Dim funciones As New ArrayList
        If id_Actual Then
            Dim id_producto As Integer = id_Actual
            Dim tab1 As tabsInformation
            tab1.direccion = ""
            tab1.iframe = ""
            funciones.Add(tab1)

            tab1.direccion = "../Archivos.aspx?popUp=true&id_producto=" & id_producto
            tab1.iframe = if_Archivos.ClientID
            funciones.Add(tab1)

            tab1.direccion = ""
            tab1.iframe = ""
            funciones.Add(tab1)
            tabsFunciones(funciones)
        End If
    End Sub

    Protected Sub SetThemeProperties()
        lnk_TipoProducto.NavigateUrl = "javascript:toggleLayer('" & trv_TipoProducto.ClientID & "', this)"
        lnk_Origen.NavigateUrl = "javascript:toggleLayer('" & trv_Origenes.ClientID & "', this)"

        lnk_Grupo.NavigateUrl = "javascript:toggleLayer('" & trv_Grupos.ClientID & "', this)"
        trv_Grupos.Style("display") = "none"
    End Sub

    Protected Sub SetControlProperties()
        Dim producto As New Producto
        tbx_NombreProducto.Attributes.Add("maxlength", producto.Nombre.Length)
        tbx_NombreProducto.Attributes.Add("onkeypress", "showTextProgress(this)")
        tbx_Codigo.Attributes.Add("maxlength", producto.SKU.Length)
        tbx_Codigo.Attributes.Add("onkeypress", "showTextProgress(this)")
        'tbx_DescripcionCorta.Attributes.Add("maxlength", producto.DescCorta_Producto.Length)
        'tbx_DescripcionCorta.Attributes.Add("onkeypress", "showTextProgress(this)")

        Me.Hyl_AregarTipoProducto.NavigateUrl = MyMaster.obtenerIframeString("TipoProducto.aspx")
        Me.Hyl_agregarunidad.NavigateUrl = MyMaster.obtenerIframeString("../Moneda.aspx")
        Me.Hyl_AgregarEstado.NavigateUrl = MyMaster.obtenerIframeString("Estados_Productos.aspx")
        Me.Hyl_AgregarOrigen.NavigateUrl = MyMaster.obtenerIframeString("Origenes.aspx")
        Me.Hyl_AgregarKeyword.NavigateUrl = MyMaster.obtenerIframeString("Keywords.aspx")
        Me.Hyl_AgregarAtVar.NavigateUrl = MyMaster.obtenerIframeString("Atributos_TipoProducto.aspx")
    End Sub

    'Valores_Session
    Protected Sub SaveSessionValues()
        Dim Valores_Session As Valores_Session
        Try
            'WorkingID
            If Me.id_Actual > 0 Then
                Valores_Session = New Valores_Session(securityHandler.Entidad, codigo_pantalla, "WorkingID", Me.id_Actual)
                connection.executeInsert(queryBuilder.InsertQuery(Valores_Session))
            End If

            'Otros
            Valores_Session = New Valores_Session(securityHandler.Entidad, codigo_pantalla, ddl_Unidades.ID, ddl_Unidades.SelectedIndex)
            connection.executeInsert(queryBuilder.InsertQuery(Valores_Session))

            If tbx_NombreProducto.Text.Length > 0 Then
                Valores_Session = New Valores_Session(securityHandler.Entidad, codigo_pantalla, tbx_NombreProducto.ID, tbx_NombreProducto.Text)
                connection.executeInsert(queryBuilder.InsertQuery(Valores_Session))
            End If

            If tbx_Codigo.Text.Length > 0 Then
                Valores_Session = New Valores_Session(securityHandler.Entidad, codigo_pantalla, tbx_Codigo.ID, tbx_Codigo.Text)
                connection.executeInsert(queryBuilder.InsertQuery(Valores_Session))
            End If

            If tbx_PrecioUnitario.Text.Length > 0 Then
                Valores_Session = New Valores_Session(securityHandler.Entidad, codigo_pantalla, tbx_PrecioUnitario.ID, tbx_PrecioUnitario.Text)
                connection.executeInsert(queryBuilder.InsertQuery(Valores_Session))
            End If

            If tbx_DescripcionCorta.Text.Length > 0 Then
                Valores_Session = New Valores_Session(securityHandler.Entidad, codigo_pantalla, tbx_DescripcionCorta.ID, tbx_DescripcionCorta.Text)
                connection.executeInsert(queryBuilder.InsertQuery(Valores_Session))
            End If

            If tbx_DescripcionLarga.Text.Length > 0 Then
                Valores_Session = New Valores_Session(securityHandler.Entidad, codigo_pantalla, tbx_DescripcionLarga.ID, tbx_DescripcionLarga.Text)
                connection.executeInsert(queryBuilder.InsertQuery(Valores_Session))
            End If

        Catch exc As Exception

        End Try
    End Sub

    Protected Function LoadSessionValues() As Boolean
        Dim Valores_Session As New Valores_Session
        Dim counter As Integer
        Try
            Valores_Session.Fields.SelectAll()
            Valores_Session.Id_Entidad.Where.EqualCondition(securityHandler.Entidad)
            Valores_Session.Pantalla.Where.EqualCondition(codigo_pantalla)
            Dim dataTable As Data.DataTable = connection.executeSelect_DT(queryBuilder.SelectQuery(Valores_Session))

            If dataTable.Rows.Count > 0 Then
                Dim results_Valores_Session As ArrayList = ObjectBuilder.TransformDataTable(dataTable, Valores_Session)
                For counter = 0 To results_Valores_Session.Count - 1
                    Dim act_Valores_Session As Valores_Session = results_Valores_Session(counter)

                    If act_Valores_Session.ControlID.Value = ddl_Unidades.ID Then
                        ddl_Unidades.SelectedIndex = act_Valores_Session.Valor.Value

                    ElseIf act_Valores_Session.ControlID.Value = tbx_NombreProducto.ID Then
                        tbx_NombreProducto.Text = act_Valores_Session.Valor.Value

                    ElseIf act_Valores_Session.ControlID.Value = tbx_Codigo.ID Then
                        tbx_Codigo.Text = act_Valores_Session.Valor.Value

                    ElseIf act_Valores_Session.ControlID.Value = tbx_PrecioUnitario.ID Then
                        tbx_PrecioUnitario.Text = act_Valores_Session.Valor.Value

                    ElseIf act_Valores_Session.ControlID.Value = tbx_DescripcionCorta.ID Then
                        tbx_DescripcionCorta.Text = act_Valores_Session.Valor.Value

                    ElseIf act_Valores_Session.ControlID.Value = tbx_DescripcionLarga.ID Then
                        tbx_DescripcionLarga.Text = act_Valores_Session.Valor.Value

                    Else
                        'Otros varas
                    End If
                Next
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

    Protected Function LoadSessionWorkingID() As Integer
        Dim Valores_Session As New Valores_Session
        Dim workingId As Integer = 0

        Valores_Session.Fields.SelectAll()
        Valores_Session.Id_Entidad.Where.EqualCondition(securityHandler.Entidad)
        Valores_Session.Pantalla.Where.EqualCondition(codigo_pantalla)
        Valores_Session.ControlID.Where.EqualCondition("WorkingID")
        Dim dataTable As Data.DataTable = connection.executeSelect_DT(queryBuilder.SelectQuery(Valores_Session))

        If dataTable.Rows.Count > 0 Then
            ObjectBuilder.CreateObject(dataTable, 0, Valores_Session)
            workingId = Valores_Session.Valor.Value
        End If

        Return workingId
    End Function

    'Producto
    Protected Function loadProducto(ByVal id_Producto As Integer) As Boolean
        Dim producto As New Producto
        Dim origen As New Origenes
        Dim tipoProducto As New TipoProducto

        producto.Fields.SelectAll()
        producto.Id_Producto.Where.EqualCondition(id_Producto)

        origen.Nombre.ToSelect = True
        queryBuilder.Join.EqualCondition(origen.Id_Origen, producto.id_Origen)

        tipoProducto.Nombre.ToSelect = True
        queryBuilder.Join.EqualCondition(tipoProducto.Id_TipoProducto, producto.Id_tipoProducto)

        queryBuilder.From.Add(producto)
        queryBuilder.From.Add(origen)
        queryBuilder.From.Add(tipoProducto)

        Dim prueba As String = queryBuilder.RelationalSelectQuery

        Dim dataTable As Data.DataTable = connection.executeSelect_DT(prueba)

        If dataTable.Rows.Count > 0 Then
            ObjectBuilder.CreateObject(dataTable, 0, origen)
            ObjectBuilder.CreateObject(dataTable, 0, producto)
            ObjectBuilder.CreateObject(dataTable, 0, tipoProducto)

            'Carga la informacion
            tbx_NombreProducto.Text = producto.Nombre.Value
            Me.id_Actual = producto.Id_Producto.Value
            tbx_DescripcionCorta.Text = producto.DescCorta_Producto.Value
            tbx_DescripcionLarga.Text = producto.DescLarga_Producto.Value
            tbx_Codigo.Text = producto.SKU.Value
            tbx_PrecioUnitario.Text = producto.Precio_Unitario.Value

            tbx_Capacidad.Text = producto.Capacidad.Value
            tbx_CapacidadMaxima.Text = producto.CapacidadMaxima.Value

            lbl_nombreTitulo.Text = producto.Nombre.Value
            lbl_tipoTitulo.Text = tipoProducto.Nombre.Value

            If producto.Activo.Value = "1" Then
                Chk_Activo.Checked = True
            End If

            'Estado, Origenes, y tipo producto
            lnk_Origen.ToolTip = producto.id_Origen.Value
            lnk_Origen.Text = origen.Nombre.Value
            lnk_TipoProducto.Text = tipoProducto.Nombre.Value
            lnk_TipoProducto.ToolTip = producto.Id_tipoProducto.Value
            ddl_Estados.SelectedValue = producto.id_Estado.Value
            ddl_Unidades.SelectedValue = producto.Id_Moneda.Value

            selectEntidad(producto.id_Entidad.Value)
            Return True
        End If
        Return False
    End Function

    Protected Function insertProducto(ByVal controladora As ProductosHandler, ByVal nombre As String) As Integer
        Dim activo As Integer
        If Chk_Activo.Checked Then
            activo = 1
        End If
        Dim prod As New Producto()
        prod.Id_tipoProducto.Value = lnk_TipoProducto.ToolTip
        prod.Nombre.Value = nombre
        prod.SKU.Value = tbx_Codigo.Text
        prod.Activo.Value = activo
        prod.id_Origen.Value = lnk_Origen.ToolTip
        prod.id_Estado.Value = ddl_Estados.SelectedValue
        prod.Id_Moneda.Value = ddl_Unidades.SelectedValue
        prod.id_Entidad.Value = securityHandler.Usuario
        prod.DescCorta_Producto.Value = tbx_DescripcionCorta.Text
        prod.DescLarga_Producto.Value = tbx_DescripcionLarga.Text
        If tbx_PrecioUnitario.Text.Length > 0 Then
            prod.Precio_Unitario.Value = tbx_PrecioUnitario.Text
        End If
        If tbx_Capacidad.Text.Length > 0 Then
            prod.Capacidad.Value = tbx_Capacidad.Text
        End If
        If tbx_CapacidadMaxima.Text.Length > 0 Then
            prod.CapacidadMaxima.Value = tbx_CapacidadMaxima.Text
        End If
        Return controladora.CrearProducto(prod)
    End Function

    Protected Function updateProducto(ByVal controladora As ProductosHandler, ByVal id_Producto As Integer) As Boolean
        Dim activo As Integer = 0
        If Chk_Activo.Checked Then
            activo = 1
        End If
        Dim prod As New Producto()
        prod.Id_tipoProducto.Value = lnk_TipoProducto.ToolTip
        prod.Nombre.Value = tbx_NombreProducto.Text
        prod.SKU.Value = tbx_Codigo.Text
        prod.Activo.Value = activo
        prod.id_Origen.Value = lnk_Origen.ToolTip
        prod.id_Estado.Value = ddl_Estados.SelectedValue
        prod.Id_Moneda.Value = ddl_Unidades.SelectedValue
        prod.id_Entidad.Value = securityHandler.Usuario
        prod.DescCorta_Producto.Value = tbx_DescripcionCorta.Text
        prod.DescLarga_Producto.Value = tbx_DescripcionLarga.Text
        If tbx_PrecioUnitario.Text.Length > 0 Then
            prod.Precio_Unitario.Value = tbx_PrecioUnitario.Text
        Else
            prod.Precio_Unitario.SetToNull()
        End If
        If tbx_Capacidad.Text.Length > 0 Then
            prod.Capacidad.Value = tbx_Capacidad.Text
        Else
            prod.Capacidad.SetToNull()
        End If
        If tbx_CapacidadMaxima.Text.Length > 0 Then
            prod.CapacidadMaxima.Value = tbx_CapacidadMaxima.Text
        Else
            prod.CapacidadMaxima.SetToNull()
        End If
        Return controladora.ActualizarProducto(prod, id_Producto)
    End Function

    
    'Informacion_Producto
    Protected Function insertInformacion_Producto(ByVal id_Producto As Integer) As Boolean
        Try
            Dim Informacion_Producto As New Informacion_Producto()
            Informacion_Producto.UltimaVisita.SetToNull()
            Informacion_Producto.Contador.Value = 0
            Informacion_Producto.Rating.Value = 0
            Informacion_Producto.Votos.Value = 0
            Informacion_Producto.Id_Producto.Value = id_Producto
            connection.executeInsert(queryBuilder.InsertQuery(Informacion_Producto))
            Return True
        Catch exc As Exception
            MyMaster.MostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    'TipoProducto
    Protected Sub selectTipoProducto(ByVal theTreeView As TreeView)
        Dim tipoProducto As New TipoProducto
        Dim counter As Integer

        tipoProducto.Fields.SelectAll()
        tipoProducto.Id_TipoProducto.Where.EqualCondition_OnSameTable(tipoProducto.Id_padre)
        queryBuilder.Orderby.Add(tipoProducto.Nombre)
        Dim dataTable As Data.DataTable = connection.executeSelect_DT(queryBuilder.SelectQuery(tipoProducto))
        theTreeView.Nodes.Clear()
        If dataTable.Rows.Count > 0 Then
            Dim resul_TipoProducto As ArrayList = ObjectBuilder.TransformDataTable(dataTable, tipoProducto)

            For counter = 0 To dataTable.Rows.Count - 1
                Dim act_TipoProducto As TipoProducto = resul_TipoProducto(counter)
                Dim hijo As New TreeNode(act_TipoProducto.Nombre.Value, act_TipoProducto.Id_TipoProducto.Value)
                theTreeView.Nodes.Add(hijo)
                selectTipoProductoHijos(theTreeView.Nodes(counter))
            Next
        End If
        theTreeView.CollapseAll()
    End Sub

    Protected Sub selectTipoProductoHijos(ByVal elTreeNode As TreeNode)
        Dim padre_id_TipoProducto As Integer = elTreeNode.Value
        Dim tipoProducto As New TipoProducto
        Dim counter As Integer

        tipoProducto.Fields.SelectAll()
        tipoProducto.Id_padre.Where.EqualCondition(padre_id_TipoProducto)
        tipoProducto.Id_TipoProducto.Where.DiferentCondition(padre_id_TipoProducto)
        queryBuilder.Orderby.Add(tipoProducto.Nombre)
        Dim dataTable As Data.DataTable = connection.executeSelect_DT(queryBuilder.SelectQuery(tipoProducto))

        If dataTable.Rows.Count > 0 Then
            Dim resul_Origenes As ArrayList = ObjectBuilder.TransformDataTable(dataTable, tipoProducto)

            For counter = 0 To dataTable.Rows.Count - 1
                Dim act_tipoProducto As TipoProducto = resul_Origenes(counter)
                Dim hijo As New TreeNode(act_tipoProducto.Nombre.Value, act_tipoProducto.Id_TipoProducto.Value)
                selectTipoProductoHijos(hijo)
                elTreeNode.ChildNodes.Add(hijo)
            Next
        End If
    End Sub

    'Atributos_Producto
    Protected Sub selectAtributos(ByVal id_TipoProducto As Integer)
        Dim Atributos_Para_Productos As New Atributos_Para_Productos
        Dim Atributos_TipoProducto As New Atributos_TipoProducto

        Atributos_Para_Productos.Fields.SelectAll()
        Atributos_TipoProducto.Id_TipoProducto.Where.EqualCondition(id_TipoProducto)
        queryBuilder.Join.EqualCondition(Atributos_Para_Productos.Id_atributo, Atributos_TipoProducto.Id_Atributo)

        queryBuilder.Orderby.Add(Atributos_TipoProducto.orden)
        queryBuilder.From.Add(Atributos_Para_Productos)
        queryBuilder.From.Add(Atributos_TipoProducto)
        Dim dataTable As Data.DataTable = connection.executeSelect_DT(queryBuilder.RelationalSelectQuery())

        If dataTable.Rows.Count > 0 Then
            dg_Atributos.DataSource = dataTable
            dg_Atributos.DataKeyField = Atributos_Para_Productos.Id_atributo.Name
            dg_Atributos.DataBind()
            dg_Atributos.Visible = True

            'Llena el DataGrid
            Dim counter As Integer
            Dim result_Atributos_Para_Productos As ArrayList = ObjectBuilder.TransformDataTable(dataTable, Atributos_Para_Productos)
            For counter = 0 To dataTable.Rows.Count - 1
                Dim act_Atributos_Para_Productos As Atributos_Para_Productos = result_Atributos_Para_Productos(counter)
                Dim lbl_Atributo As Label = dg_Atributos.Items(counter).FindControl("lbl_Atributo")

                lbl_Atributo.Text = act_Atributos_Para_Productos.Nombre.Value
                lbl_Atributo.Attributes.Add(act_Atributos_Para_Productos.Id_atributo.Name, act_Atributos_Para_Productos.Id_atributo.Value)
                lbl_Atributo.Attributes.Add("InDB", "False")

                Dim ddl_Atributos_Valores As DropDownList = dg_Atributos.Items(counter).FindControl("ddl_Atributos_Valores")
                Dim tbx_Atributos_Valor As TextBox = dg_Atributos.Items(counter).FindControl("tbx_Atributos_Valor")

                If act_Atributos_Para_Productos.AutoCompletar.Value = 1 Then
                    tbx_Atributos_Valor.Visible = False
                    ddl_Atributos_Valores.Visible = True
                    llenarDDL(ddl_Atributos_Valores, id_TipoProducto, act_Atributos_Para_Productos.Id_atributo.Value)
                Else
                    ddl_Atributos_Valores.Visible = False
                    tbx_Atributos_Valor.Visible = True
                    tbx_Atributos_Valor.Attributes("true") = "Verdad"
                End If
            Next
        Else
            dg_Atributos.Visible = False
        End If
    End Sub

    Protected Sub llenarDDL(ByVal theDDL As DropDownList, ByVal id_TipoProducto As Integer, ByVal id_Atributo As Integer)
        Dim Atributos_Producto As New Atributos_Producto
        Dim producto As New Producto

        Atributos_Producto.Valor.ToSelect = True
        Atributos_Producto.Id_atributo.Where.EqualCondition(id_Atributo)
        queryBuilder.Join.EqualCondition(Atributos_Producto.Id_Producto, producto.Id_Producto)
        producto.Id_tipoProducto.Where.EqualCondition(id_TipoProducto)

        queryBuilder.From.Add(Atributos_Producto)
        queryBuilder.From.Add(producto)
        queryBuilder.Distinct = True
        Dim dataTable As Data.DataTable = connection.executeSelect_DT(queryBuilder.RelationalSelectQuery())

        If dataTable.Rows.Count > 0 Then
            theDDL.DataSource = dataTable
            theDDL.DataTextField = Atributos_Producto.Valor.Name
            theDDL.DataValueField = Atributos_Producto.Valor.Name
            theDDL.DataBind()
        End If
        theDDL.Items.Add("--Sin Asignar--")
        theDDL.SelectedIndex = theDDL.Items.Count - 1
    End Sub

    Protected Sub loadAtributos_Producto(ByVal id_Producto As Integer)
        Dim Atributos_Producto As New Atributos_Producto
        Dim counter As Integer

        Atributos_Producto.Fields.SelectAll()
        Atributos_Producto.Id_Producto.Where.EqualCondition(id_Producto)
        Dim dataTable As Data.DataTable = connection.executeSelect_DT(queryBuilder.SelectQuery(Atributos_Producto))

        If dataTable.Rows.Count > 0 Then
            Dim results_Atributos_Producto As ArrayList = ObjectBuilder.TransformDataTable(dataTable, Atributos_Producto)
            For counter = 0 To dg_Atributos.Items.Count - 1
                Dim lbl_Atributo As Label = dg_Atributos.Items(counter).FindControl("lbl_Atributo")
                Dim ddl_Atributos_Valores As DropDownList = dg_Atributos.Items(counter).FindControl("ddl_Atributos_Valores")
                Dim tbx_Atributos_Valor As TextBox = dg_Atributos.Items(counter).FindControl("tbx_Atributos_Valor")
                Dim chk_Atributo_Visible As CheckBox = dg_Atributos.Items(counter).FindControl("chk_Atributo_Visible")

                Dim counterB As Integer = 0
                For counterB = 0 To results_Atributos_Producto.Count - 1
                    Dim act_Atributos_Producto As Atributos_Producto = results_Atributos_Producto(counterB)

                    If lbl_Atributo.Attributes("id_Atributo") = act_Atributos_Producto.Id_atributo.Value Then
                        lbl_Atributo.Attributes("InDB") = "True"

                        If tbx_Atributos_Valor.Attributes("true") = "Verdad" Then
                            tbx_Atributos_Valor.Text = act_Atributos_Producto.Valor.Value
                        Else
                            ddl_Atributos_Valores.SelectedValue = act_Atributos_Producto.Valor.Value
                        End If

                        If act_Atributos_Producto.Visible.Value = "1" Then
                            chk_Atributo_Visible.Checked = True
                        End If
                    End If
                Next
            Next
        End If
    End Sub

    Protected Function insertAtributos_Producto(ByVal id_Producto As Integer) As Boolean
        Dim counter As Integer
        For counter = 0 To dg_Atributos.Items.Count - 1
            Dim Atributos_Producto As New Atributos_Producto
            Dim lbl_Atributo As Label = dg_Atributos.Items(counter).FindControl("lbl_Atributo")
            Dim ddl_Atributos_Valores As DropDownList = dg_Atributos.Items(counter).FindControl("ddl_Atributos_Valores")
            Dim tbx_Atributos_Valor As TextBox = dg_Atributos.Items(counter).FindControl("tbx_Atributos_Valor")
            Dim chk_Atributo_Visible As CheckBox = dg_Atributos.Items(counter).FindControl("chk_Atributo_Visible")

            Try
                Atributos_Producto.Id_Producto.Value = id_Producto
                Atributos_Producto.Id_atributo.Value = lbl_Atributo.Attributes("id_Atributo")

                'Verifica de donde tomar informacion
                If tbx_Atributos_Valor.Visible Then
                    If tbx_Atributos_Valor.Text.Length > 0 Then
                        Atributos_Producto.Valor.Value = tbx_Atributos_Valor.Text
                    End If
                Else
                    If ddl_Atributos_Valores.SelectedIndex <> ddl_Atributos_Valores.Items.Count - 1 Then
                        Atributos_Producto.Valor.Value = ddl_Atributos_Valores.SelectedValue
                    End If
                End If

                If chk_Atributo_Visible.Checked Then
                    Atributos_Producto.Visible.Value = "1"
                End If

                'Si hay valor ejecuta el insert
                If Atributos_Producto.Valor.IsValid Then
                    connection.executeInsert(queryBuilder.InsertQuery(Atributos_Producto))
                    lbl_Atributo.Attributes("InDB") = "True"
                End If
            Catch exc As Exception
                MyMaster.MostrarMensaje(exc.Message, True)

                Return False
            End Try
        Next
        Return True
    End Function

    Protected Function updateAtributos_Producto(ByVal id_Producto As Integer) As Boolean
        Dim counter As Integer
        For counter = 0 To dg_Atributos.Items.Count - 1
            Dim Atributos_Producto As New Atributos_Producto
            Dim lbl_Atributo As Label = dg_Atributos.Items(counter).FindControl("lbl_Atributo")
            Dim ddl_Atributos_Valores As DropDownList = dg_Atributos.Items(counter).FindControl("ddl_Atributos_Valores")
            Dim tbx_Atributos_Valor As TextBox = dg_Atributos.Items(counter).FindControl("tbx_Atributos_Valor")
            Dim chk_Atributo_Visible As CheckBox = dg_Atributos.Items(counter).FindControl("chk_Atributo_Visible")

            Try
                Atributos_Producto.Id_Producto.Where.EqualCondition(id_Producto)
                Atributos_Producto.Id_atributo.Where.EqualCondition(lbl_Atributo.Attributes("id_Atributo"))

                'Verifica de donde tomar informacion
                If tbx_Atributos_Valor.Visible Then
                    If tbx_Atributos_Valor.Text.Length > 0 Then
                        Atributos_Producto.Valor.Value = tbx_Atributos_Valor.Text
                    End If
                Else
                    If ddl_Atributos_Valores.SelectedIndex <> ddl_Atributos_Valores.Items.Count - 1 Then
                        Atributos_Producto.Valor.Value = ddl_Atributos_Valores.SelectedValue
                    End If
                End If

                If chk_Atributo_Visible.Checked Then
                    Atributos_Producto.Visible.Value = "1"
                Else
                    Atributos_Producto.Visible.Value = "0"
                End If

                If lbl_Atributo.Attributes("InDB") = "True" Then
                    connection.executeUpdate(queryBuilder.UpdateQuery(Atributos_Producto))
                Else
                    If Atributos_Producto.Valor.IsValid Then
                        Atributos_Producto.Id_Producto.Value = id_Producto
                        Atributos_Producto.Id_atributo.Value = lbl_Atributo.Attributes("id_Atributo")
                        connection.executeInsert(queryBuilder.InsertQuery(Atributos_Producto))
                        lbl_Atributo.Attributes("InDB") = "True"
                    End If
                End If
            Catch exc As Exception
                MyMaster.MostrarMensaje(exc.Message, True)

                Return False
            End Try
        Next
        Return True
    End Function

    Protected Function deleteAtributos_Producto(ByVal id_Producto As Integer) As Boolean
        Dim Atributos_Producto As New Atributos_Producto
        Try
            Atributos_Producto.Id_Producto.Where.EqualCondition(id_Producto)
            connection.executeDelete(queryBuilder.DeleteQuery(Atributos_Producto))
            Return True
        Catch exc As Exception
            MyMaster.MostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    'Moneda      
    Protected Sub selectUnidades()
        Dim Moneda As New Moneda
        Moneda.Fields.SelectAll()
        queryBuilder.Orderby.Add(Moneda.Nombre)
        Dim dataTable As Data.DataTable = connection.executeSelect_DT(queryBuilder.SelectQuery(Moneda))
        If dataTable.Rows.Count > 0 Then
            ddl_Unidades.DataSource = dataTable
            ddl_Unidades.DataTextField = Moneda.Nombre.Name
            ddl_Unidades.DataValueField = Moneda.Id_Moneda.Name
            ddl_Unidades.DataBind()
        End If
    End Sub

    'Keywords
    Protected Sub selectKeywords()
        Dim Keywords As New Keywords

        Keywords.Fields.SelectAll()
        queryBuilder.From.Add(Keywords)
        queryBuilder.Orderby.Add(Keywords.Nombre)
        Dim dataTable As Data.DataTable = connection.executeSelect_DT(queryBuilder.RelationalSelectQuery)
        If dataTable.Rows.Count > 0 Then
            dtl_Keywords.DataSource = dataTable
            dtl_Keywords.DataKeyField = Keywords.Id_Keyword.Name
            dtl_Keywords.DataBind()

            'Llena el grid
            Dim counter As Integer
            Dim results_Keywords As ArrayList = ObjectBuilder.TransformDataTable(dataTable, Keywords)
            For counter = 0 To dtl_Keywords.Items.Count - 1
                Dim act_Keywords As Keywords = results_Keywords(counter)
                Dim lbl_Keyword As Label = dtl_Keywords.Items(counter).FindControl("lbl_Keyword")
                Dim chk_Keyword As CheckBox = dtl_Keywords.Items(counter).FindControl("chk_Keyword")
                lbl_Keyword.Text = act_Keywords.Nombre.Value
                lbl_Keyword.Attributes.Add("id_Keyword", act_Keywords.Id_Keyword.Value)
                lbl_Keyword.Attributes.Add("InDB", "No")
            Next
            dtl_Keywords.Visible = True
        Else
            dtl_Keywords.Visible = False
        End If
    End Sub

    Protected Sub selectKeywords_Producto(ByVal Id_Producto As Integer)
        Dim Keywords_Producto As New Keywords_Producto

        Keywords_Producto.Fields.SelectAll()
        Keywords_Producto.Id_Producto.Where.EqualCondition(Id_Producto)
        queryBuilder.Orderby.Add(Keywords_Producto.Id_Keyword)
        Dim dataTable As Data.DataTable = connection.executeSelect_DT(queryBuilder.SelectQuery(Keywords_Producto))
        If dataTable.Rows.Count > 0 Then
            Dim result_Keywords_Producto As ArrayList = ObjectBuilder.TransformDataTable(dataTable, Keywords_Producto)
            For counterA As Integer = 0 To dtl_Keywords.Items.Count - 1
                Dim lbl_Keyword As Label = dtl_Keywords.Items(counterA).FindControl("lbl_Keyword")
                Dim chk_Keyword As CheckBox = dtl_Keywords.Items(counterA).FindControl("chk_Keyword")

                For counterB As Integer = 0 To result_Keywords_Producto.Count - 1
                    Dim act_Keywords_Producto As Keywords_Producto = result_Keywords_Producto(counterB)
                    If lbl_Keyword.Attributes("id_Keyword") = act_Keywords_Producto.Id_Keyword.Value Then
                        chk_Keyword.Checked = True
                        lbl_Keyword.Attributes("InDB") = "Si"
                    End If
                Next
            Next
        End If
    End Sub

    Protected Function deleteKeywords_Producto(ByVal Id_Producto As Integer) As Boolean
        Dim Keywords_Producto As New Keywords_Producto
        Try
            Keywords_Producto.Id_Producto.Where.EqualCondition(Id_Producto)
            connection.executeDelete(queryBuilder.DeleteQuery(Keywords_Producto))
            Return True
        Catch exc As Exception
            MyMaster.MostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    Protected Function insertKeywords_Producto(ByVal Id_Producto As Integer) As Boolean
        Dim Keywords_Producto As New Keywords_Producto
        Try
            Dim counter As Integer
            For counter = 0 To dtl_Keywords.Items.Count - 1
                Dim lbl_Keyword As Label = dtl_Keywords.Items(counter).FindControl("lbl_Keyword")
                Dim chk_Keyword As CheckBox = dtl_Keywords.Items(counter).FindControl("chk_Keyword")

                If chk_Keyword.Checked Then
                    Keywords_Producto.Id_Producto.Value = Id_Producto
                    Keywords_Producto.Id_Keyword.Value = lbl_Keyword.Attributes("id_Keyword")
                    connection.executeInsert(queryBuilder.InsertQuery(Keywords_Producto))
                    lbl_Keyword.Attributes("InDB") = "Si"
                End If
            Next
        Catch exc As Exception
            MyMaster.MostrarMensaje(exc.Message, True)

            Return False
        End Try
        Return True
    End Function

    Protected Function updateKeywords_Producto(ByVal Id_Producto As Integer) As Boolean
        Dim Keywords_Producto As New Keywords_Producto
        Dim counter As Integer
        Try
            For counter = 0 To dtl_Keywords.Items.Count - 1
                Dim lbl_Keyword As Label = dtl_Keywords.Items(counter).FindControl("lbl_Keyword")
                Dim chk_Keyword As CheckBox = dtl_Keywords.Items(counter).FindControl("chk_Keyword")

                If lbl_Keyword.Attributes("InDB") = "No" Then
                    If chk_Keyword.Checked Then     'Insert
                        Keywords_Producto.Id_Producto.Value = Id_Producto
                        Keywords_Producto.Id_Keyword.Value = lbl_Keyword.Attributes("id_Keyword")
                        connection.executeInsert(queryBuilder.InsertQuery(Keywords_Producto))
                    End If
                Else
                    If Not chk_Keyword.Checked Then     'Delete
                        Keywords_Producto.Id_Producto.Where.EqualCondition(Id_Producto)
                        Keywords_Producto.Id_Keyword.Where.EqualCondition(lbl_Keyword.Attributes("id_Keyword"))
                        connection.executeDelete(queryBuilder.DeleteQuery(Keywords_Producto))
                    End If
                End If
            Next
        Catch exc As Exception
            MyMaster.MostrarMensaje(exc.Message, True)

            Return False
        End Try
        Return True
    End Function

    'Origenes
    Protected Sub selectOrigenes(ByVal theTreeView As TreeView)
        Dim origen As New Origenes
        Dim counter As Integer
        Dim primero As Boolean = True

        origen.Fields.SelectAll()
        origen.Id_Origen.Where.EqualCondition_OnSameTable(origen.Id_padre)
        queryBuilder.Orderby.Add(origen.Nombre)
        theTreeView.Nodes.Clear()
        Dim dataTable As Data.DataTable = connection.executeSelect_DT(queryBuilder.SelectQuery(origen))
        If dataTable.Rows.Count > 0 Then
            Dim result_Origenes As ArrayList = ObjectBuilder.TransformDataTable(dataTable, origen)

            For counter = 0 To dataTable.Rows.Count - 1
                Dim act_Origen As Origenes = result_Origenes(counter)
                Dim hijo As New TreeNode(act_Origen.Nombre.Value, act_Origen.Id_Origen.Value)
                theTreeView.Nodes.Add(hijo)
                If primero Then
                    lnk_Origen.Text = act_Origen.Nombre.Value
                    lnk_Origen.ToolTip = act_Origen.Id_Origen.Value
                    primero = False
                End If
                selectOrigenesHijos(theTreeView.Nodes(counter))
            Next
        End If
        theTreeView.CollapseAll()
    End Sub

    Protected Sub selectOrigenesHijos(ByVal elTreeNode As TreeNode)
        Dim padre_id_Origenes As Integer = elTreeNode.Value
        Dim origen As New Origenes
        Dim counter As Integer

        origen.Fields.SelectAll()
        origen.Id_padre.Where.EqualCondition(padre_id_Origenes)
        origen.Id_Origen.Where.DiferentCondition(padre_id_Origenes)
        queryBuilder.Orderby.Add(origen.Nombre)
        Dim dataTable As Data.DataTable = connection.executeSelect_DT(queryBuilder.SelectQuery(origen))

        If dataTable.Rows.Count > 0 Then
            Dim resul_Origenes As ArrayList = ObjectBuilder.TransformDataTable(dataTable, origen)

            For counter = 0 To dataTable.Rows.Count - 1
                Dim act_Origen As Origenes = resul_Origenes(counter)
                Dim hijo As New TreeNode(act_Origen.Nombre.Value, act_Origen.Id_Origen.Value)
                selectOrigenesHijos(hijo)
                elTreeNode.ChildNodes.Add(hijo)
            Next
        End If
    End Sub

    'Estados
    Protected Sub selectEstados()
        Dim estados_Productos As New Estados_Productos
        estados_Productos.Fields.SelectAll()
        queryBuilder.Orderby.Add(estados_Productos.Nombre)
        Dim dataTable As Data.DataTable = connection.executeSelect_DT(queryBuilder.SelectQuery(estados_Productos))
        If dataTable.Rows.Count Then
            ddl_Estados.DataSource = dataTable
            ddl_Estados.DataTextField = estados_Productos.Nombre.Name
            ddl_Estados.DataValueField = estados_Productos.Id_Estado_Producto.Name
            ddl_Estados.DataBind()
        End If
    End Sub

    'Entidades
    Protected Sub selectEntidad(ByVal id_entidad As Integer)
        Dim entidadHandler As New Orbelink.Control.Entidades.EntidadHandler(Configuration.Config_DefaultConnectionString)
        lbl_Entidad.Text = entidadHandler.ConsultarNombreEntidad(id_entidad)
    End Sub

    'Grupos
    Protected Sub infoGrupos()
        Hyp_agregarGrupo.NavigateUrl = MyMaster.obtenerIframeString("Grupo_Producto.aspx")
        Hyp_tipoRelacionGrupos.NavigateUrl = MyMaster.obtenerIframeString("TipoRelacion_Grupos.aspx")
        selectGrupos(trv_Grupos)
        selectTipoRelacion_Grupos()
    End Sub

    Protected Sub selectGrupos(ByVal theTreeView As TreeView)
        Dim Grupos_Productos As New Grupos_Productos
        Dim counter As Integer
        theTreeView.Nodes.Clear()
        Grupos_Productos.Fields.SelectAll()
        Grupos_Productos.Id_Grupo.Where.EqualCondition_OnSameTable(Grupos_Productos.Id_padre)
        queryBuilder.Orderby.Add(Grupos_Productos.Nombre)

        Dim dataTable As Data.DataTable = connection.executeSelect_DT(queryBuilder.SelectQuery(Grupos_Productos))
        If dataTable.Rows.Count > 0 Then
            Dim results_Grupos As ArrayList = ObjectBuilder.TransformDataTable(dataTable, Grupos_Productos)
            For counter = 0 To dataTable.Rows.Count - 1
                Dim act_ubicacion As Grupos_Productos = results_Grupos(counter)
                Dim hijo As New TreeNode(act_ubicacion.Nombre.Value, act_ubicacion.Id_Grupo.Value)
                theTreeView.Nodes.Add(hijo)
                selectGruposHijos(theTreeView.Nodes(counter))
                hijo.Collapse()
            Next
        End If
    End Sub

    Protected Sub selectGruposHijos(ByVal elTreeNode As TreeNode)
        Dim padre_Id_Grupo_Producto As Integer = elTreeNode.Value
        Dim Grupos_Productos As New Grupos_Productos
        Dim counter As Integer

        Grupos_Productos.Fields.SelectAll()
        Grupos_Productos.Id_padre.Where.EqualCondition(padre_Id_Grupo_Producto)
        Grupos_Productos.Id_Grupo.Where.DiferentCondition(padre_Id_Grupo_Producto)
        queryBuilder.Orderby.Add(Grupos_Productos.Nombre)
        Dim dataTable As Data.DataTable = connection.executeSelect_DT(queryBuilder.SelectQuery(Grupos_Productos))

        If dataTable.Rows.Count > 0 Then
            Dim results_Grupos As ArrayList = ObjectBuilder.TransformDataTable(dataTable, Grupos_Productos)
            For counter = 0 To dataTable.Rows.Count - 1
                Dim act_Grupo As Grupos_Productos = results_Grupos(counter)
                Dim hijo As New TreeNode(act_Grupo.Nombre.Value, act_Grupo.Id_Grupo.Value)
                selectGruposHijos(hijo)
                elTreeNode.ChildNodes.Add(hijo)
                hijo.Collapse()
            Next
        End If
    End Sub

    'TipoRelacion_Grupos
    Protected Sub selectTipoRelacion_Grupos()
        Dim TipoRelacion_Grupos_Producto As New TipoRelacion_Grupos_Producto

        TipoRelacion_Grupos_Producto.Id_TipoRelacion.ToSelect = True
        TipoRelacion_Grupos_Producto.Nombre.ToSelect = True
        queryBuilder.Orderby.Add(TipoRelacion_Grupos_Producto.Nombre)
        Dim dataTable As Data.DataTable = connection.executeSelect_DT(queryBuilder.SelectQuery(TipoRelacion_Grupos_Producto))
        If dataTable.Rows.Count > 0 Then
            ddl_TipoRelacion_Grupos.DataSource = dataTable
            ddl_TipoRelacion_Grupos.DataTextField = TipoRelacion_Grupos_Producto.Nombre.Name
            ddl_TipoRelacion_Grupos.DataValueField = TipoRelacion_Grupos_Producto.Id_TipoRelacion.Name
            ddl_TipoRelacion_Grupos.DataBind()
        End If
    End Sub

    'Productos_Grupo
    Protected Sub selectProductos_Grupo(ByVal Id_Producto As Integer)

        Dim Productos_Grupo As New Productos_Grupo
        Dim Grupos_Productos As New Grupos_Productos
        Dim TipoRelacion_Grupos_Producto As New TipoRelacion_Grupos_Producto

        Productos_Grupo.Id_Producto.Where.EqualCondition(Id_Producto)

        Grupos_Productos.Nombre.ToSelect = True
        Grupos_Productos.Id_Grupo.ToSelect = True
        queryBuilder.Join.EqualCondition(Grupos_Productos.Id_Grupo, Productos_Grupo.Id_Grupo)

        TipoRelacion_Grupos_Producto.Nombre.ToSelect = True
        TipoRelacion_Grupos_Producto.Id_TipoRelacion.ToSelect = True
        queryBuilder.Join.EqualCondition(TipoRelacion_Grupos_Producto.Id_TipoRelacion, Productos_Grupo.Id_TipoRelacion)

        queryBuilder.From.Add(Productos_Grupo)
        queryBuilder.From.Add(Grupos_Productos)
        queryBuilder.From.Add(TipoRelacion_Grupos_Producto)
        queryBuilder.Orderby.Add(Grupos_Productos.Nombre)
        Dim dataTable As Data.DataTable = connection.executeSelect_DT(queryBuilder.RelationalSelectQuery())

        lst_Grupos.Items.Clear()
        If dataTable.Rows.Count > 0 Then
            Dim results_Atributos_Para_Productos As ArrayList = ObjectBuilder.TransformDataTable(dataTable, Grupos_Productos)
            Dim results_TipoRelacion_Grupos As ArrayList = ObjectBuilder.TransformDataTable(dataTable, TipoRelacion_Grupos_Producto)
            For counter As Integer = 0 To dataTable.Rows.Count - 1
                Dim act_Grupos_Productos As Grupos_Productos = results_Atributos_Para_Productos(counter)
                Dim act_TipoRelacion_Productos As TipoRelacion_Grupos_Producto = results_TipoRelacion_Grupos(counter)

                lst_Grupos.Items.Add(act_Grupos_Productos.Nombre.Value & ", como " & act_TipoRelacion_Productos.Nombre.Value)
                lst_Grupos.Items(counter).Value = act_Grupos_Productos.Id_Grupo.Value & "-" & act_TipoRelacion_Productos.Id_TipoRelacion.Value
            Next
        End If
    End Sub

    Protected Function insertProductos_Grupo(ByVal id_Producto As Integer, ByVal id_Grupo As Integer, ByVal id_TipoRelacion As Integer) As Boolean
        Dim Productos_Grupo As New Productos_Grupo
        Try
            Productos_Grupo.Id_Grupo.Value = id_Grupo
            Productos_Grupo.Id_Producto.Value = id_Producto
            Productos_Grupo.Id_TipoRelacion.Value = id_TipoRelacion
            connection.executeInsert(queryBuilder.InsertQuery(Productos_Grupo))
            Return True
        Catch exc As Exception
            MyMaster.MostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    Protected Function deleteProductos_Grupo(ByVal id_Producto As Integer, ByVal id_Grupo As Integer, ByVal id_TipoRelacion As Integer) As Boolean
        Dim Productos_Grupo As New Productos_Grupo
        Try
            Productos_Grupo.Id_Grupo.Where.EqualCondition(id_Grupo)
            Productos_Grupo.Id_Producto.Where.EqualCondition(id_Producto)
            Productos_Grupo.Id_TipoRelacion.Where.EqualCondition(id_TipoRelacion)
            connection.executeDelete(queryBuilder.DeleteQuery(Productos_Grupo))
            Return True
        Catch exc As Exception
            MyMaster.MostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    'Eventos
    Protected Sub btn_cancelar_Click(ByVal Sender As Object, ByVal E As System.EventArgs) Handles btn_cancelar.Click
        ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
        upd_Contenido.Update()
    End Sub

    Protected Sub btn_eliminar_Click(ByVal Sender As Object, ByVal E As System.EventArgs) Handles btn_eliminar.Click
        Dim controladoraArchivos As New ProductosHandler(Configuration.Config_DefaultConnectionString)
        If controladoraArchivos.EliminarProducto(Me.id_Actual) Then
            ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
            upd_PantallaContenido.Update()
            MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Eliminar, "Producto", False)
        Else
            MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Eliminar, "Producto", True)
        End If
    End Sub

    Protected Sub btn_modificar_Click(ByVal Sender As Object, ByVal E As System.EventArgs) Handles btn_modificar.Click
        If IsValid Then
            Dim controladoraArchivos As New ProductosHandler(Configuration.Config_DefaultConnectionString)
            If updateProducto(controladoraArchivos, id_Actual) Then
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Modificar, "Producto", False)

                updateAtributos_Producto(id_Actual)
                updateKeywords_Producto(id_Actual)

                upd_Contenido.Update()
            Else
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Modificar, "Producto", True)
            End If
        Else
            MyMaster.MostrarMensaje("Algun campo necesario est� vacio o erroneo.", False)
        End If
    End Sub

    Protected Sub btn_salvar_Click(ByVal Sender As Object, ByVal E As System.EventArgs) Handles btn_salvar.Click
        If lnk_TipoProducto.ToolTip.Length = 0 Then
            vld_TipoProducto.IsValid = False
            MyMaster.MostrarMensaje("Debe seleccionar un tipo de producto.", True)
        Else
            vld_TipoProducto.IsValid = True
            If IsValid Then
                Dim controladora As New ProductosHandler(Configuration.Config_DefaultConnectionString)
                Dim id_Insertado As Integer = insertProducto(controladora, tbx_NombreProducto.Text)
                If id_Insertado > 0 Then
                    MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Salvar, "Producto", False)
                    Me.id_Actual = id_Insertado

                    ClearInfo(Configuration.EstadoPantalla.CONSULTAR, False)
                    If Not insertInformacion_Producto(id_Insertado) Then
                        MyMaster.concatenarMensaje("<br />Pero con errores en informacion adicional.", False)
                    End If

                    'Actualiza los atributos y los keywords
                    If Not insertAtributos_Producto(id_Insertado) Then
                        MyMaster.concatenarMensaje("<br />Pero error con atributos.", False)
                    End If
                    If Not insertKeywords_Producto(id_Insertado) Then
                        MyMaster.concatenarMensaje("<br />Pero error con keywords.", False)
                    End If
                    upd_KeywordsYAtributos.Update()

                    if_Archivos.Attributes.Add("src", "../Archivos.aspx?popUp=true&id_producto=" & id_Insertado)
                    'cargarTabs()
                    ToLoadPage()
                    upd_PantallaContenido.Update()
                Else
                    MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Salvar, "Producto", True)
                End If
            Else
                MyMaster.MostrarMensaje("Algun campo necesario est� vacio o erroneo.", True)
            End If
        End If
    End Sub

    Protected Sub dg_Atributos_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dg_Atributos.EditCommand
        Dim idAtributo As Integer = dg_Atributos.DataKeys(e.Item.ItemIndex)
        Dim ddl_Atributos_Valores As DropDownList = dg_Atributos.Items(e.Item.ItemIndex).FindControl("ddl_Atributos_Valores")
        Dim tbx_Atributos_Valor As TextBox = dg_Atributos.Items(e.Item.ItemIndex).FindControl("tbx_Atributos_Valor")
        If ddl_Atributos_Valores.Visible Then
            tbx_Atributos_Valor.Visible = True
            ddl_Atributos_Valores.Visible = False
        Else
            tbx_Atributos_Valor.Visible = False
            ddl_Atributos_Valores.Visible = True
        End If
    End Sub

    Protected Sub trv_Origenes_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles trv_Origenes.SelectedNodeChanged
        Dim id_NodoActual As Integer = trv_Origenes.SelectedNode.Value
        lnk_Origen.Text = trv_Origenes.SelectedNode.Text
        lnk_Origen.ToolTip = id_NodoActual
        upd_Origen.Update()
    End Sub

    Protected Sub trv_TipoProducto_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles trv_TipoProducto.SelectedNodeChanged
        lnk_TipoProducto.Text = trv_TipoProducto.SelectedNode.Text
        lnk_TipoProducto.ToolTip = trv_TipoProducto.SelectedNode.Value
        selectAtributos(trv_TipoProducto.SelectedNode.Value)
        upd_Contenido.Update()
        upd_KeywordsYAtributos.Update()
    End Sub

    Protected Sub trv_Grupos_SelectedNodeChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles trv_Grupos.SelectedNodeChanged
        Dim id_NodoActual As Integer = trv_Grupos.SelectedNode.Value
        lnk_Grupo.Text = trv_Grupos.SelectedNode.Text
        lnk_Grupo.ToolTip = id_NodoActual
        trv_Grupos.Style("display") = "none"
    End Sub

    Protected Sub btn_AgregarAGrupo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_AgregarAGrupo.Click

        Dim id_Producto As Integer = Me.id_Actual
        If lnk_Grupo.ToolTip.Length > 0 Then
            If insertProductos_Grupo(id_Producto, lnk_Grupo.ToolTip, ddl_TipoRelacion_Grupos.SelectedValue) Then
                selectProductos_Grupo(id_Producto)
                upd_Grupos.Update()
                MyMaster.MostrarMensaje("Producto agregada al Grupo exitosamente.", False)
            Else
                MyMaster.MostrarMensaje("Error al agregar Producto al Grupo.", False)
            End If
        Else
            MyMaster.MostrarMensaje("Debe seleccionar un Grupo.", False)
        End If

    End Sub

    Protected Sub btn_QuitarDeGrupo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_QuitarDeGrupo.Click

        Dim id_Producto As Integer = Me.id_Actual
        If lst_Grupos.SelectedValue.Length > 0 Then
            Dim corte As Integer = lst_Grupos.SelectedValue.IndexOf("-")
            Dim id_Grupo As Integer = lst_Grupos.SelectedValue.Substring(0, corte)
            Dim id_TipoRelacion As Integer = lst_Grupos.SelectedValue.Substring(corte + 1, lst_Grupos.SelectedValue.Length - corte - 1)
            If deleteProductos_Grupo(id_Producto, id_Grupo, id_TipoRelacion) Then
                selectProductos_Grupo(id_Producto)
                upd_Grupos.Update()
                MyMaster.MostrarMensaje("Producto eliminada del Grupo exitosamente.", False)
            Else
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Eliminar, "Producto del Grupo", True)
            End If
        Else
            MyMaster.MostrarMensaje("Debe seleccionar un Grupo.", False)
        End If
    End Sub

    'Agregar
    Protected Sub btn_IngresarNuevo_Click(ByVal Sender As Object, ByVal E As System.EventArgs) Handles btn_IngresarNuevo.Click
        id_Actual = 0
        ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
        manejarVistas(False)
        'mostrarNombreTitulo()
        upd_PantallaContenido.Update()
    End Sub

    Protected Sub btn_Accion_Ayuda_Click(ByVal Sender As Object, ByVal E As System.EventArgs) Handles btn_Accion_Ayuda.Click
        MyMaster.MostrarMensaje("Puede crear y editar la informaci�n de los <b>Productos</b><br /><br />Seg�n el <b>Tipo de Producto</b> que seleccione, se le pedir� <b>Otros Atributos</b>.<br /><br />Recuerde que debe seleccionar un <b>Origen</b>, pues esta la default.<br />", False)
    End Sub

    Protected Sub btn_Accion_Editar_Click(ByVal Sender As Object, ByVal E As System.EventArgs) Handles btn_Accion_Editar.Click
        manejarVistas(False)
        Me.tabs_Productos.ActiveTabIndex = 0
        upd_PantallaContenido.Update()
    End Sub

    Protected Sub btn_Accion_Ver_Click(ByVal Sender As Object, ByVal E As System.EventArgs) Handles btn_Accion_Ver.Click
        manejarVistas(True)
        upd_PantallaContenido.Update()
    End Sub

    Protected Sub manejarVistas(ByVal vistaVer As Boolean)
        Dim productoExist As Boolean = False

        pnl_editar.Visible = Not vistaVer
        pnl_editarTitulo.Visible = Not vistaVer
        ocwc_DetalleProducto.Visible = vistaVer
        pnl_verTitulo.Visible = vistaVer

        'ojo revisar logica
        If id_Actual > 0 Then
            If vistaVer Then
                ClearInfo(Configuration.EstadoPantalla.CONSULTAR, False)
                ocwc_DetalleProducto.cargarInformacion(id_Actual)
            Else
                'Carga producto
                productoExist = loadProducto(id_Actual)
                If productoExist Then
                    ClearInfo(Configuration.EstadoPantalla.MODIFICAR, False)

                    lnk_NuevaDireccion.NavigateUrl = MyMaster.obtenerIframeString("GoogleMaps.aspx?id_Producto=" & id_Actual)
                    ocwc_ProductoDirecciones.SetIdProducto(id_Actual)
                    ocwc_ProductoDirecciones.selectPuntos_Producto(id_Actual)

                    selectAtributos(lnk_TipoProducto.ToolTip)
                    loadAtributos_Producto(id_Actual)
                    selectKeywords_Producto(id_Actual)
                    selectProductos_Grupo(id_Actual)

                    'Cargar relacionados
                    ocwc_Producto_Entidad.cargarRelacionados(id_Actual)
                    ocwc_Producto_Publicacion.cargarRelacionados(id_Actual)
                    'ocwc_Producto_Proyecto.cargarRelacionados(id_Actual)
                    'ocwc_Producto_cuenta.cargarRelacionados(id_Actual)
                End If
            End If
        End If
    End Sub

End Class
