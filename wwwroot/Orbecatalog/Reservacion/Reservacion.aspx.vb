﻿Imports Orbelink.DBHandler
Imports Orbelink.Entity.Reservaciones
Imports Orbelink.Control.Reservaciones
Imports Orbelink.Entity.Productos
Imports Orbelink.Entity.Entidades
Imports Orbelink.DateAndTime

Partial Class Orbecatalog_Reservacion_ReservacionUbicacion
    Inherits PageBaseClass

    Const codigo_pantalla As String = "Rs-02"
    Const codigo_pantalla2 As String = "Rs-11"
    Const level As Integer = 2

    'ViewState
    Dim _temp As Integer
    Protected Property ReservacionActual() As Integer
        Get
            If _temp <= 0 Then
                If ViewState("id_ReservacionActual") IsNot Nothing Then
                    _temp = ViewState("id_ReservacionActual")
                Else
                    ViewState.Add("id_ReservacionActual", 0)
                    _temp = 0
                End If
            End If
            Return _temp
        End Get
        Set(ByVal value As Integer)
            _temp = value
            ViewState("id_ReservacionActual") = _temp
        End Set
    End Property

    Dim _temp2 As Integer
    Protected Property TipoEstadoActual() As Integer
        Get
            If _temp2 <= 0 Then
                If ViewState("id_tipoEstadoActual") IsNot Nothing Then
                    _temp2 = ViewState("id_tipoEstadoActual")
                Else
                    ViewState.Add("id_tipoEstadoActual", 0)
                    _temp2 = 0
                End If
            End If
            Return _temp2
        End Get
        Set(ByVal value As Integer)
            _temp2 = value
            ViewState("id_tipoEstadoActual") = _temp2
        End Set
    End Property

    'Pagina
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddScripts(level)
        'Esto es para que lo lleve a la pagina segun sea el caso del modo de reservacion (por noches(paquetes) y noches extra o por tarifa diaria y personas extra)
        If securityHandler.verificarAcceso(codigo_pantalla) = False Then
            Dim param As String = ""
            If Request.QueryString("id_reservacion") IsNot Nothing Then
                param = "?id_reservacion=" & Request.QueryString("id_reservacion")
            End If
            Response.Redirect("~/" & securityHandler.PantallaByCodigo_Pantalla(codigo_pantalla2) & param)
        End If

        securityHandler.VerifyPantalla(codigo_pantalla, level)

        If Not IsPostBack Then
            Me.Title = Resources.Reservaciones_Resources.Reservacion

            securityHandler.VerifyActions(btn_Salvar, btn_Modificar, btn_Eliminar)

            If Request.QueryString("id_reservacion") IsNot Nothing Then
                ReservacionActual = Request.QueryString("id_reservacion")
            End If

            LoadAllInfo()

            If Request.QueryString("popUp") IsNot Nothing Then
                vistaExternaPropia(True)
            End If
            Me.Hyl_AgregarEntidad.NavigateUrl = MyMaster.obtenerIframeString("../Contactos/Entidad.aspx")
            Me.Hyl_AgregarUbicacion.NavigateUrl = MyMaster.obtenerIframeString("../Contactos/Ubicacion.aspx")
            Me.Hyl_AgregarProducto.NavigateUrl = MyMaster.obtenerIframeString("preciosTemporada.aspx")
        End If
    End Sub

    Protected Sub vistaExternaPropia(ByVal ver As Boolean)
        tab_Buscar.Enabled = Not ver
        tab_facturasReservacion.Enabled = Not ver
        tab_EstadosReservacion.Enabled = Not ver
        pnl_derecha.Visible = Not ver
        pnl_botones.Visible = Not ver
        ddl_TipoEstado.Enabled = Not ver
    End Sub

    Protected Sub ClearInfo(ByVal estado As Configuration.EstadoPantalla, ByVal clearInfo As Boolean)
        If clearInfo Then
            Me.bdp_FechaInicio.Text = Date.UtcNow.Date
            CalendarInicio.SelectedDate = Date.UtcNow.Date
            Me.bdp_FechaFinal.Text = Date.UtcNow.Date
            CalendarFinal.SelectedDate = Date.UtcNow.Date
            Me.ddl_horaFinal.SelectedIndex = 0
            Me.ddl_horaInicio.SelectedIndex = 0
            Me.ddl_minutoInicio.SelectedIndex = 0
            Me.ddl_minutoFinal.SelectedIndex = 0

            ReservacionActual = 0
        End If
        txb_comentario_Reservar.Text = ""
        txb_ComentarioEstado.Text = ""

        Select Case estado
            Case Configuration.EstadoPantalla.NORMAL
                btn_Modificar.Visible = False
                btn_Salvar.Visible = True
                btn_Eliminar.Visible = False

                div_ResultadosBusqueda.Visible = False
                btn_buscar.Visible = True
                btn_cancelarB.Visible = True
                ddl_clientes.Enabled = True
                ddl_ubicaciones.Enabled = True
                div_productos.Visible = True
                hyl_agregarItem.Visible = False

                tab_Buscar.Enabled = True
                tab_Detalle.Enabled = False
                tab_EstadosReservacion.Enabled = False
                tab_facturasReservacion.Enabled = False

                tabs_Reservacion.ActiveTabIndex = 0
            Case Configuration.EstadoPantalla.CONSULTAR
                btn_Modificar.Visible = True
                btn_Salvar.Visible = False
                btn_Eliminar.Visible = True

                div_ResultadosBusqueda.Visible = False
                btn_buscar.Visible = False
                btn_cancelarB.Visible = False
                ddl_clientes.Enabled = False
                ddl_ubicaciones.Enabled = False

                tab_Buscar.Enabled = False
                tab_Detalle.Enabled = True
                tab_EstadosReservacion.Enabled = True
                tab_facturasReservacion.Enabled = True
                tabs_Reservacion.ActiveTabIndex = 1
        End Select
    End Sub

    Protected Sub LoadAllInfo()
        Dim controladora As New ControladorReservaciones(connection, Resources.Reservaciones_Resources.ResourceManager)
        controladora.selectTipoEstadoReservacion_BindListCollection(Me.ddl_TipoEstado.Items)
        ddl_TipoEstado.Items.Add("-- Sin estado --")

        cargarProductos()
        cargarUbicacion()
        cargarEntidades()
        cargarDescripcion()
        cargarReservaciones()
        cargarHoras()
        bdp_FechaFinal.Text = Date.UtcNow.Date
        CalendarFinal.SelectedDate = Date.UtcNow.Date
        bdp_FechaInicio.Text = Date.UtcNow.Date
        CalendarInicio.SelectedDate = Date.UtcNow.Date
        If ReservacionActual > 0 Then
            If loadReservacion(ReservacionActual) Then
                cargarItemsReservados(ReservacionActual)
                cargarReservaciones()
                ClearInfo(Configuration.EstadoPantalla.CONSULTAR, False)

                selectEstados(ReservacionActual, controladora)
            Else
                ClearInfo(Configuration.EstadoPantalla.NORMAL, False)
                ReservacionActual = 0
            End If
        Else
            ClearInfo(Configuration.EstadoPantalla.NORMAL, False)
        End If

    End Sub

    'Cargar Informacion
    Protected Sub cargarHoras()
        For counter As Integer = 0 To 23
            ddl_horaInicio.Items.Add(counter)
            ddl_horaFinal.Items.Add(counter)
        Next
        setHorasIniciales()

    End Sub

    Protected Sub setHorasIniciales()
        ddl_horaInicio.SelectedValue = ControladorReservaciones.Config_HoraEntrada.Hour
        ddl_minutoInicio.SelectedValue = ControladorReservaciones.Config_HoraEntrada.Minute
        ddl_horaFinal.SelectedValue = ControladorReservaciones.Config_HoraSalida.Hour
        ddl_minutoFinal.SelectedValue = ControladorReservaciones.Config_HoraSalida.Minute
    End Sub

    Protected Sub cargarDescripcion()
        Dim item As New Item
        item.descripcion.ToSelect = True
        If ddl_productos.SelectedValue.Length > 0 Then
            item.Id_producto.Where.EqualCondition(ddl_productos.SelectedValue)

            Dim consulta As String = queryBuilder.SelectQuery(item)
            Dim dataTable As Data.DataTable = connection.executeSelect_DT(consulta)

            If dataTable.Rows.Count > 0 Then
                ObjectBuilder.CreateObject(dataTable, 0, item)
                lbl_descripcion.Text = item.descripcion.Value
            Else
                lbl_descripcion.Text = ""
            End If
        End If
      
    End Sub

    Protected Sub cargarProductos()
        Dim Producto As New Producto
        Producto.Nombre.ToSelect = True
        Producto.Id_Producto.ToSelect = True
        Producto.Activo.Where.EqualCondition(1)
        Dim items As New Item
        queryBuilder.Join.EqualCondition(items.Id_producto, Producto.Id_Producto)
        Dim precios As New Precios_Temporada
        queryBuilder.Join.EqualCondition(precios.Id_Producto, Producto.Id_Producto)
        queryBuilder.From.Add(Producto)
        queryBuilder.Distinct = True
        Dim consulta As String = queryBuilder.RelationalSelectQuery
        Dim dataTable As Data.DataTable = connection.executeSelect_DT(consulta)

        If dataTable.Rows.Count > 0 Then
            ddl_productos.DataSource = dataTable
            ddl_productos.DataTextField = Producto.Nombre.Name
            ddl_productos.DataValueField = Producto.Id_Producto.Name
            ddl_productos.DataBind()
        End If

    End Sub

    Protected Sub cargarEntidades()
        Dim Entidad As New Entidad
        Entidad.NombreDisplay.ToSelect = True
        Entidad.Id_entidad.ToSelect = True
        Dim consulta As String = queryBuilder.SelectQuery(Entidad)
        Dim dataTable As Data.DataTable = connection.executeSelect_DT(consulta)

        If dataTable.Rows.Count > 0 Then
            ddl_clientes.DataSource = dataTable
            ddl_clienteDer.DataSource = dataTable
            ddl_clientes.DataTextField = Entidad.NombreDisplay.Name
            ddl_clienteDer.DataTextField = Entidad.NombreDisplay.Name
            ddl_clientes.DataValueField = Entidad.Id_entidad.Name
            ddl_clienteDer.DataValueField = Entidad.Id_entidad.Name
            ddl_clientes.DataBind()
            ddl_clienteDer.DataBind()
        End If
    End Sub

    Protected Sub cargarUbicacion()

        Dim ubicacion As New Ubicacion
        ubicacion.Nombre.ToSelect = True
        ubicacion.Id_ubicacion.ToSelect = True
        Dim consulta As String = queryBuilder.SelectQuery(ubicacion)
        Dim dataTable As Data.DataTable = connection.executeSelect_DT(consulta)

        If dataTable.Rows.Count > 0 Then
            ddl_ubicaciones.DataSource = dataTable
            ddl_ubicaciones.DataTextField = ubicacion.Nombre.Name
            ddl_ubicaciones.DataValueField = ubicacion.Id_ubicacion.Name
            ddl_ubicaciones.DataBind()
        End If
    End Sub

    'Items
    Protected Sub cargarItemsReservados(ByVal id_reservacion As Integer)
        div_productos.Visible = False

        Dim detalle As New Detalle_Reservacion
        Dim item As New Item
        Dim producto As New Producto

        item.Fields.SelectAll()
        producto.Nombre.ToSelect = True

        queryBuilder.Join.EqualCondition(detalle.ordinal, item.ordinal)
        queryBuilder.Join.EqualCondition(detalle.Id_producto, item.Id_producto)
        queryBuilder.Join.EqualCondition(producto.Id_Producto, item.Id_producto)

        detalle.id_reservacion.Where.EqualCondition(id_reservacion)
        detalle.Adultos.ToSelect = True
        detalle.Ninos.ToSelect = True
        detalle.id_reservacion.ToSelect = True
        detalle.Id_producto.ToSelect = True
        detalle.ordinal.ToSelect = True

        queryBuilder.From.Add(item)
        queryBuilder.From.Add(detalle)
        queryBuilder.From.Add(producto)

        Dim consulta As String = queryBuilder.RelationalSelectQuery
        Dim dataTable As Data.DataTable = connection.executeSelect_DT(consulta)
        If dataTable.Rows.Count > 0 Then

            Dim list_item As ArrayList = ObjectBuilder.TransformDataTable(dataTable, item)
            Dim list_producto As ArrayList = ObjectBuilder.TransformDataTable(dataTable, producto)
            Dim results_Detalle As ArrayList = ObjectBuilder.TransformDataTable(dataTable, detalle)

            gv_itemModificar.DataKeyNames = New String() {detalle.id_reservacion.Name, detalle.Id_producto.Name, detalle.ordinal.Name}
            gv_itemModificar.DataSource = dataTable
            gv_itemModificar.DataBind()

            For counter As Integer = 0 To dataTable.Rows.Count - 1
                Dim unItem As GridViewRow = gv_itemModificar.Rows(counter)
                producto = list_producto(counter)
                item = list_item(counter)
                Dim act_Detalle As Detalle_Reservacion = results_Detalle(counter)

                Dim lbl_nombre As Label = unItem.FindControl("lbl_producto")
                Dim lbl_descripcion As Label = unItem.FindControl("lbl_descripcion")
                Dim lbl_Adultos As Label = unItem.FindControl("lbl_Adultos")
                Dim lbl_Ninos As Label = unItem.FindControl("lbl_Ninos")

                lbl_Adultos.Text = act_Detalle.Adultos.Value
                lbl_Ninos.Text = act_Detalle.Ninos.Value

                lbl_nombre.Text = producto.Nombre.Value
                lbl_nombre.Attributes("producto") = act_Detalle.Id_producto.Value
                lbl_nombre.Attributes("ordinal") = act_Detalle.ordinal.Value
                lbl_descripcion.Text = item.descripcion.Value

            Next
            gv_itemModificar.Visible = True
        Else
            gv_itemModificar.Visible = False
        End If
        upd_DetalleReservacion.Update()
    End Sub

    'Reservacion
    Protected Function loadReservacion(ByVal id_reserva As Integer) As Boolean

        Dim reserva As New Reservacion
        Dim cliente As New Entidad
        Dim ubicacion As New Ubicacion
        Dim tipoEstado As New TipoEstadoReservacion

        cliente.NombreDisplay.ToSelect = True
        cliente.Id_entidad.ToSelect = True
        queryBuilder.Join.EqualCondition(cliente.Id_entidad, reserva.id_Cliente)

        ubicacion.Nombre.ToSelect = True
        queryBuilder.Join.EqualCondition(ubicacion.Id_ubicacion, reserva.Id_Ubicacion)

        reserva.Fields.SelectAll()
        reserva.Id_Reservacion.Where.EqualCondition(id_reserva)

        tipoEstado.Fields.SelectAll()
        'tipoEstado.Terminador.Where.EqualCondition(1)
        queryBuilder.Join.EqualCondition(reserva.Id_TipoEstado, tipoEstado.Id_TipoEstadoReservacion)

        queryBuilder.From.Add(reserva)
        queryBuilder.From.Add(cliente)
        queryBuilder.From.Add(ubicacion)
        queryBuilder.From.Add(tipoEstado)

        Dim consulta As String = queryBuilder.RelationalSelectQuery
        Dim dataTable As Data.DataTable = connection.executeSelect_DT(consulta)
        If dataTable.Rows.Count > 0 Then
            ObjectBuilder.CreateObject(dataTable, 0, reserva)
            ObjectBuilder.CreateObject(dataTable, 0, cliente)
            ObjectBuilder.CreateObject(dataTable, 0, ubicacion)
            ObjectBuilder.CreateObject(dataTable, 0, tipoEstado)

            'reserva.descripcion()
            ddl_TipoEstado.SelectedValue = reserva.Id_TipoEstado.Value
            TipoEstadoActual = reserva.Id_TipoEstado.Value

            lbl_FechaFinalProgramado.Text = reserva.fecha_finalProgramado.ValueLocalized.ToString("MM/dd/yyyy hh:mm tt")

            If reserva.fecha_finalReal.IsValid Then
                lbl_FechaFinal.Text = reserva.fecha_finalReal.ValueLocalized
            Else
                lbl_FechaFinal.Text = "--"
            End If

            lbl_FechaInicioProgramado.Text = reserva.fecha_inicioProgramado.ValueLocalized.ToString("MM/dd/yyyy hh:mm tt")
            If reserva.fecha_inicioReal.IsValid Then
                lbl_FechaInicio.Text = reserva.fecha_inicioReal.ValueLocalized
            Else
                lbl_FechaInicio.Text = "--"
            End If
            lbl_FechaCreado.Text = reserva.fechaRegistro.ValueLocalized
            Dim link As String = "../Contactos/Entidad.aspx?id_entidad=" & cliente.Id_entidad.Value
            lbl_Cliente.Text = "<a href =" & link & " a>" & cliente.NombreDisplay.Value & "</a>"
            lbl_Ubicacion.Text = ubicacion.Nombre.Value

            Me.ddl_clientes.SelectedValue = reserva.id_Cliente.Value
            Me.ddl_clienteDer.SelectedValue = reserva.id_Cliente.Value
            Me.ddl_ubicaciones.SelectedValue = reserva.Id_Ubicacion.Value
            If reserva.Id_factura.IsValid Then
                if_Facturas.Attributes("src") = "../../Orbecatalog/Facturas/ReporteDetalleFactura.aspx?popUp=true&id_factura=" & reserva.Id_factura.Value
            Else
                tab_facturasReservacion.Visible = False
                tab_facturasReservacion.HeaderText = ""
            End If
            
            Upd_factura.Update()
            txb_comenReser.Text = reserva.descripcion.Value
            Dim fechaInicio As Date = reserva.fecha_inicioProgramado.ValueLocalized
            Dim fechaFin As Date = reserva.fecha_finalProgramado.ValueLocalized
            Me.bdp_FechaInicio.Text = fechaInicio.Date
            Me.bdp_FechaFinal.Text = fechaFin.Date
            CalendarInicio.SelectedDate = fechaInicio.Date
            CalendarFinal.SelectedDate = fechaFin.Date
            Me.ddl_horaInicio.SelectedValue = fechaInicio.Hour
            Me.ddl_minutoInicio.SelectedValue = fechaInicio.Minute
            Me.ddl_horaFinal.SelectedValue = fechaFin.Hour
            Me.ddl_minutoFinal.SelectedValue = fechaFin.Minute

            Dim controladora As New ControladorReservaciones(connection, Resources.Reservaciones_Resources.ResourceManager)
            selectEstados(id_reserva, controladora)
            If tipoEstado.Terminador.Value = controladora.BuscarEstadoCancelado Or fechaInicio < Date.UtcNow Then
                'botones.Visible = False
                btn_agregar.Visible = False
            Else
                hyl_agregarItemC.Visible = True
                hyl_agregarItemC.NavigateUrl = MyMaster.obtenerIframeString("agregarItem.aspx?id_reserva=" & id_reserva & "&fechaInicio=" & fechaInicio & "&fechaFinal=" & fechaFin)
            End If

            Return True
        End If
        Return False
    End Function

    Protected Sub cargarReservaciones()
        Dim reserva As New Reservacion

        Dim tipoEstado As New TipoEstadoReservacion

        tipoEstado.RepresentaCancelado.Where.EqualCondition(0)
        queryBuilder.Join.EqualCondition(reserva.Id_TipoEstado, tipoEstado.Id_TipoEstadoReservacion)

        reserva.fecha_inicioProgramado.ToSelect = True
        reserva.fecha_finalProgramado.ToSelect = True
        reserva.Id_Reservacion.ToSelect = True
        If ddl_clienteDer.SelectedValue.Length > 0 Then
            reserva.id_Cliente.Where.EqualCondition(ddl_clienteDer.SelectedValue)
        End If

        'reserva.Id_TipoEstado.Where.EqualCondition(ControladorReservaciones.Estado_reservado)

        'Dim fecha As New Date(Date.UtcNow.Year, Date.UtcNow.Month, Date.UtcNow.Day)
        'reserva.fecha_finalProgramado.Where.GreaterThanOrEqualCondition(fecha)

        queryBuilder.From.Add(reserva)
        queryBuilder.From.Add(tipoEstado)

        Dim consulta As String = queryBuilder.RelationalSelectQuery
        Dim dataTable As Data.DataTable = connection.executeSelect_DT(consulta)
        If dataTable.Rows.Count > 0 Then

            gv_buscarReserva.DataSource = dataTable
            gv_buscarReserva.DataBind()

            Dim controladora As New ControladorReservaciones(connection, Resources.Reservaciones_Resources.ResourceManager)

            Dim resul_Reserva As ArrayList = TransformDataTable(dataTable, reserva)
            Dim counter As Integer = 0
            For Each unItem As GridViewRow In gv_buscarReserva.Rows
                reserva = resul_Reserva(counter)

                Dim cantidad As Label = unItem.FindControl("lbl_cantidad")
                Dim fechaInicio As Label = unItem.FindControl("lbl_fechaInicio")
                fechaInicio.Attributes.Add("id_reserva", reserva.Id_Reservacion.Value)
                Dim fechaFinal As Label = unItem.FindControl("lbl_fechaFin")

                cantidad.Text = controladora.CantidadItemsEnReservacion(reserva.Id_Reservacion.Value)
                Dim fechaA As Date = reserva.fecha_inicioProgramado.ValueLocalized
                fechaInicio.Text = fechaA.ToShortDateString & " " & fechaA.ToLongTimeString
                fechaA = reserva.fecha_finalProgramado.ValueLocalized
                fechaFinal.Text = fechaA.ToShortDateString & " " & fechaA.ToLongTimeString
                counter += 1
            Next
            gv_buscarReserva.Visible = True
            lbl_noPoseeDer.Visible = False
        Else
            gv_buscarReserva.Visible = False
            lbl_noPoseeDer.Visible = True
        End If
    End Sub

    'Estados 
    Protected Sub selectEstados(ByVal id_Reservacion As Integer, ByVal controladora As ControladorReservaciones)
        Dim resultado As Data.DataTable = controladora.selectEstados_ByReservacion(id_Reservacion)

        If resultado IsNot Nothing Then
            If gv_Estados.Columns.Count = 0 Then
                gv_Estados.AutoGenerateColumns = False

                For Each columna As Data.DataColumn In resultado.Columns
                    If columna.ColumnMapping <> Data.MappingType.Hidden Then
                        Dim fieldTemp As New BoundField
                        fieldTemp.DataField = columna.ColumnName
                        fieldTemp.HeaderText = columna.ColumnName
                        gv_Estados.Columns.Add(fieldTemp)
                    End If
                Next
            End If
            gv_Estados.DataSource = resultado
            gv_Estados.DataBind()
            gv_Estados.Visible = True
            lbl_NoEstados.Visible = False
        Else
            gv_Estados.Visible = False
            lbl_NoEstados.Visible = True
        End If
    End Sub

    Protected Function crearFechaHora(ByVal fecha As String, ByVal hora As String, ByVal minuto As String) As Date
        Dim laFecha As Date = fecha
        Dim fechaFinal As New Date(laFecha.Year, laFecha.Month, laFecha.Day, hora, minuto, 0)
        Return fechaFinal
    End Function


    'Busquedas
    Protected Sub cargarItemsDisponibles()
        Dim id_producto As Integer = ddl_productos.SelectedValue
        Dim fechaInicio As Date = crearFechaHora(CalendarInicio.SelectedDate, ddl_horaInicio.SelectedValue, ddl_minutoInicio.SelectedValue)
        Dim fechaFin As Date = crearFechaHora(CalendarFinal.SelectedDate, ddl_horaFinal.SelectedValue, ddl_minutoFinal.SelectedValue)

        Dim id_ubicacion As Integer = ddl_ubicaciones.SelectedValue

        Dim controladora As New ControladorReservaciones(connection, Resources.Reservaciones_Resources.ResourceManager)
        Dim dataTable As Data.DataTable = controladora.cargarItemsDisponibles_ByProducto(id_producto, DateHandler.ToUtcFromLocalizedDate(fechaInicio), DateHandler.ToUtcFromLocalizedDate(fechaFin), id_ubicacion)

        If dataTable.Rows.Count > 0 Then
            asignarInformacion(dataTable, gv_ResultadosDisponibles)
            ltl_NoResultadosDisponibles.Visible = False
            btn_agregar.Visible = True
            gv_ResultadosDisponibles.Visible = True
        Else
            ltl_NoResultadosDisponibles.Visible = True
            btn_agregar.Visible = False
            gv_ResultadosDisponibles.Visible = False
        End If
    End Sub

    Protected Sub cargarItemsEnFecha()
        Dim id_producto As Integer = ddl_productos.SelectedValue

        Dim fechaInicio As Date = crearFechaHora(bdp_FechaInicio.Text, ddl_horaInicio.SelectedValue, ddl_minutoInicio.SelectedValue)
        Dim fechaFin As Date = crearFechaHora(bdp_FechaFinal.Text, ddl_horaFinal.SelectedValue, ddl_minutoFinal.SelectedValue)

        Dim id_ubicacion As Integer = ddl_ubicaciones.SelectedValue

        Dim controladora As New ControladorReservaciones(connection, Resources.Reservaciones_Resources.ResourceManager)
        Dim dataTable As Data.DataTable = controladora.cargarItemsEnFecha_ByProducto(id_producto, DateHandler.ToUtcFromLocalizedDate(fechaInicio), DateHandler.ToUtcFromLocalizedDate(fechaFin), id_ubicacion)

        If dataTable.Rows.Count > 0 Then
            asignarInformacion(dataTable, gv_ResultadoEnFechas)
            lbl_NoResultadoEnFechas.Visible = False
            gv_ResultadoEnFechas.Visible = True
        Else
            lbl_NoResultadoEnFechas.Visible = True
            gv_ResultadoEnFechas.Visible = False
        End If
    End Sub

    Protected Sub cargarItemsEnUbicacion()
        Dim id_producto As Integer = ddl_productos.SelectedValue

        Dim fechaInicio As Date = crearFechaHora(CalendarInicio.SelectedDate, ddl_horaInicio.SelectedValue, ddl_minutoInicio.SelectedValue)
        Dim fechaFin As Date = crearFechaHora(CalendarFinal.SelectedDate, ddl_horaFinal.SelectedValue, ddl_minutoFinal.SelectedValue)
        Dim id_ubicacion As Integer = ddl_ubicaciones.SelectedValue

        Dim controladora As New ControladorReservaciones(connection, Resources.Reservaciones_Resources.ResourceManager)
        Dim dataTable As Data.DataTable = controladora.cargarItemsEnUbicacion_ByProducto(id_producto, DateHandler.ToUtcFromLocalizedDate(fechaInicio), DateHandler.ToUtcFromLocalizedDate(fechaFin), id_ubicacion)

        If dataTable.Rows.Count > 0 Then
            asignarInformacion(dataTable, gv_ResultadoEnUbicacion)
            ltl_NoResultadosEnUbicacion.Visible = False
            gv_ResultadoEnUbicacion.Visible = True
        Else
            ltl_NoResultadosEnUbicacion.Visible = True
            gv_ResultadoEnUbicacion.Visible = False
        End If
    End Sub

    Protected Sub asignarInformacion(ByVal dataTable As Data.DataTable, ByVal theGridView As GridView)
        theGridView.DataSource = dataTable
        theGridView.DataBind()

        Dim resul_producto As ArrayList = TransformDataTable(dataTable, New Producto)
        Dim resul_Item As ArrayList = TransformDataTable(dataTable, New Item)

        For counter As Integer = 0 To dataTable.Rows.Count - 1
            Dim unItem As GridViewRow = theGridView.Rows(counter)
            Dim producto As Producto = resul_producto(counter)
            Dim item As Item = resul_Item(counter)

            Dim lbl_nombre As Label = unItem.FindControl("lbl_nombre")
            Dim lbl_descripcion As Label = unItem.FindControl("lbl_descripcion")

            lbl_nombre.Text = producto.Nombre.Value
            lbl_descripcion.Text = item.descripcion.Value
            lbl_nombre.Attributes.Add("ordinal", item.ordinal.Value)
            lbl_nombre.Attributes.Add("producto", producto.Id_Producto.Value)
        Next
    End Sub

    Protected Sub eliminarDetalle(ByVal elItem As GridViewRow)
        Dim lbl_nombre As Label = elItem.FindControl("lbl_producto")
        Dim id_producto As Integer = lbl_nombre.Attributes("producto")
        Dim ordinal As Integer = lbl_nombre.Attributes("ordinal")

        Try
            Dim controladora As New ControladorReservaciones(connection, Resources.Reservaciones_Resources.ResourceManager)
            If controladora.EliminarDetalle(ReservacionActual, id_producto, ordinal, securityHandler.Usuario, txb_ComentarioEstado.Text) Then
                cargarItemsReservados(ReservacionActual)
                Dim reserva As Reservacion = controladora.ConsultarReservacion(ReservacionActual)
                If reserva.Id_TipoEstado.Value <> controladora.BuscarEstadoReservado Then
                    MyMaster.MostrarMensaje("Item borrado exitosamaente", False)
                End If
            Else
                ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
                MyMaster.MostrarMensaje("Reservacion borrada exitosamaente", False)
            End If
            cargarReservaciones()
        Catch ex As Exception
            MyMaster.MostrarMensaje(ex.Message, True)
        End Try
    End Sub

    Protected Function verificarDisponibilidad(ByVal id_reservacion As Integer) As Boolean
        Dim disponibles As Boolean = True
        Dim producto As New Producto

        Dim reserva As New Reservacion
        Dim detalle As New Detalle_Reservacion
        Dim item As New Item
        Dim productosReservados As String = ""

        Dim controladora As New ControladorReservaciones(connection, Resources.Reservaciones_Resources.ResourceManager)

        item.Fields.SelectAll()
        producto.Nombre.ToSelect = True
        queryBuilder.Join.EqualCondition(reserva.Id_Reservacion, detalle.id_reservacion)
        queryBuilder.Join.EqualCondition(detalle.Id_producto, item.Id_producto)
        queryBuilder.Join.EqualCondition(detalle.ordinal, item.ordinal)
        queryBuilder.Join.EqualCondition(item.Id_producto, producto.Id_Producto)
        reserva.Id_Reservacion.Where.EqualCondition(id_reservacion)

        queryBuilder.From.Add(reserva)
        queryBuilder.From.Add(producto)
        queryBuilder.From.Add(detalle)
        queryBuilder.From.Add(item)

        Dim consulta As String = queryBuilder.RelationalSelectQuery
        Dim dataTable As Data.DataTable = connection.executeSelect_DT(consulta)
        If dataTable.Rows.Count > 0 Then
            Dim resul_item As ArrayList = ObjectBuilder.TransformDataTable(dataTable, item)
            Dim resul_producto As ArrayList = ObjectBuilder.TransformDataTable(dataTable, producto)
            For counter As Integer = 0 To dataTable.Rows.Count - 1
                item = resul_item(counter)
                producto = resul_producto(counter)
                If Not controladora.IsItemDisponible_ByReservacion(id_reservacion, item.Id_producto.Value, item.ordinal.Value) Then
                    disponibles = False
                    productosReservados &= producto.Nombre.Value & "<br />"
                    'Exit For
                End If
            Next
        End If

        If Not disponibles Then
            MyMaster.MostrarMensaje("NO es posible cambiar la fecha ya que los siguientes productos se encuntran reservados: <br />" & productosReservados, True)
        End If
        Return disponibles
    End Function

    Protected Function fechaPermitida() As Boolean
        Dim permitida As Boolean = False

        Dim fechaInicio As Date = crearFechaHora(CalendarInicio.SelectedDate, ddl_horaInicio.SelectedValue, ddl_minutoInicio.SelectedValue)
        Dim fechaFin As Date = crearFechaHora(CalendarFinal.SelectedDate, ddl_horaFinal.SelectedValue, ddl_minutoFinal.SelectedValue)

        If fechaInicio.Date <= fechaFin.Date Then
            If fechaInicio.Date = fechaFin.Date Then
                If (ddl_horaInicio.SelectedValue < ddl_horaFinal.SelectedValue) Or (ddl_minutoInicio.SelectedValue < ddl_minutoFinal.SelectedValue) Then
                    permitida = True
                Else
                    MyMaster.MostrarMensaje("Hora Final debe ser mayor que la hora de Inicio", True)
                End If
            Else
                permitida = True
            End If
        Else
            MyMaster.MostrarMensaje("Fecha Inicio debe ser menor que la fecha final", True)
        End If
        Return permitida
    End Function

    Protected Sub desabilitarFechas(ByVal desabilitar As Boolean)
        CalendarFinal.Enabled = Not desabilitar
        bdp_FechaFinal.Enabled = Not desabilitar
        CalendarInicio.Enabled = Not desabilitar
        bdp_FechaInicio.Enabled = Not desabilitar
        ddl_horaInicio.Enabled = Not desabilitar
        ddl_minutoInicio.Enabled = Not desabilitar
        ddl_horaFinal.Enabled = Not desabilitar
        ddl_minutoFinal.Enabled = Not desabilitar
        ddl_clientes.Enabled = Not desabilitar
        ddl_ubicaciones.Enabled = Not desabilitar
    End Sub

    'Eventos
    Protected Sub btn_Salvar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Salvar.Click
        'If IsValid() Then
        '    Dim crmhandler As New CRMHandler(connection)
        '    Dim id_Insertado As Integer = crmhandler.CrearReservacion(tbx_NombreReservacion.Text, tbx_Telefono.Text, securityHandler.Usuario)
        '    If id_Insertado > 0 Then
        '        MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Salvar, "Reservacion", False)
        '        ClearInfo(Configuration.EstadoPantalla.CONSULTAR, False)

        '        Me.ReservacionActual = id_Insertado
        '        loadReservacion(id_Insertado)

        '        upd_Reservacion.Update()
        '    Else
        '        MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Salvar, "Reservacion", True)
        '    End If
        'Else
        MyMaster.MostrarMensaje(Resources.Orbecatalog_Resources.CamposRequeridos, False)
        'End If
    End Sub

    Protected Sub btn_Modificar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Modificar.Click
        If IsValid() Then
            If ddl_TipoEstado.SelectedIndex < ddl_TipoEstado.Items.Count - 1 Then
                modificarComentario(ReservacionActual, txb_comenReser.Text)
                If ddl_TipoEstado.SelectedValue <> Me.TipoEstadoActual Then
                    Dim controladora As New ControladorReservaciones(connection, Resources.Reservaciones_Resources.ResourceManager)
                    Dim code As ControladorReservaciones.Resultado_Code = controladora.NuevoEstadoParaReservacion(Me.ReservacionActual, ddl_TipoEstado.SelectedValue, securityHandler.Usuario, txb_ComentarioEstado.Text)
                    If code = ControladorReservaciones.Resultado_Code.OK Then
                        selectEstados(Me.ReservacionActual, controladora)
                        TipoEstadoActual = ddl_TipoEstado.SelectedValue
                        upd_Estados.Update()
                        MyMaster.MostrarMensaje("Nuevo estado guardado exitosamente", False)
                        pnl_comentarios.Visible = False
                        txb_ComentarioEstado.Text = ""
                    Else
                        MyMaster.MostrarMensaje(controladora.obtenerMensajeError(code), True)
                    End If
                End If
                'Else
                'MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Modificar, "Reservacion", True)
                'End If
            Else
                MyMaster.concatenarMensaje("Debe seleccionar un estado nuevo valido", True)
            End If
        Else
            MyMaster.MostrarMensaje(Resources.Orbecatalog_Resources.CamposRequeridos, True)
        End If
    End Sub

    Protected Function modificarComentario(ByVal id_reservacion As Integer, ByVal comentario As String) As Boolean
        Dim exito As Boolean = False
        Try
            Dim reservacion As New Reservacion
            reservacion.descripcion.Value = comentario
            reservacion.Id_Reservacion.Where.EqualCondition(id_reservacion)
            Dim consulta As String = queryBuilder.UpdateQuery(reservacion)
            connection.executeUpdate(consulta)
            exito = True
        Catch ex As Exception

        End Try

        Return exito
    End Function

    Protected Sub btn_Cancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Cancelar.Click
        If ReservacionActual > 0 Then
            ClearInfo(Configuration.EstadoPantalla.CONSULTAR, True)
            loadReservacion(ReservacionActual)
        Else
            ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
        End If
        'mostrarNombreTitulo()
        upd_DetalleReservacion.Update()
    End Sub

    Protected Sub btn_Eliminar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Eliminar.Click
        Dim controladora As New ControladorReservaciones(connection, Resources.Reservaciones_Resources.ResourceManager)
        Dim code As ControladorReservaciones.Resultado_Code = ControladorReservaciones.Resultado_Code.OK

        code = controladora.NuevoEstadoParaReservacion(ReservacionActual, controladora.BuscarEstadoCancelado, securityHandler.Usuario, txb_ComentarioEstado.Text)
        If code = ControladorReservaciones.Resultado_Code.OK Then
            ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
            MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Eliminar, "Reservacion", False)
            desabilitarFechas(False)
            cargarReservaciones()
            upd_PantallaContenido.Update()
            upd_Busqueda.Update()
        Else
            MyMaster.MostrarMensaje("Error al borrar Reservacion. " & controladora.obtenerMensajeError(code), False)
        End If
    End Sub

    Public Overrides Sub PopUpOkEventHandler(ByVal param As String)
        'Cargar relacionados
        Dim loadSession As Boolean = False

        Select Case param
            Case "Rs-05"
                cargarItemsReservados(ReservacionActual)
                cargarReservaciones()
        End Select
        'End If
    End Sub

    Protected Sub ddl_productos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddl_productos.SelectedIndexChanged
        cargarDescripcion()
        upd_Busqueda.Update()
    End Sub

    Protected Sub btn_buscar_Click(ByVal Sender As Object, ByVal E As System.EventArgs) Handles btn_buscar.Click
        If fechaPermitida() Then
            cargarItemsDisponibles()
            cargarItemsEnFecha()
            cargarItemsEnUbicacion()
            desabilitarFechas(True)
            div_ResultadosBusqueda.Visible = True
        End If
    End Sub

    Protected Sub ddl_clienteDer_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddl_clienteDer.SelectedIndexChanged
        cargarReservaciones()
    End Sub

    Protected Sub gv_buscarReserva_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gv_buscarReserva.RowCommand

        Dim elItem As GridViewRow = gv_buscarReserva.Rows(e.CommandArgument)
        Dim fechaInicio As Label = elItem.FindControl("lbl_fechaInicio")
        Dim id_reserva As Integer = fechaInicio.Attributes("id_reserva")

        ClearInfo(Configuration.EstadoPantalla.CONSULTAR, True)
        loadReservacion(id_reserva)
        ReservacionActual = id_reserva
        cargarItemsReservados(id_reserva)
        desabilitarFechas(True)
        upd_PantallaContenido.Update()
    End Sub

    Protected Sub btn_agregar_Click(ByVal Sender As Object, ByVal E As System.EventArgs) Handles btn_agregar.Click
        Dim controladora As New ControladorReservaciones(connection, Resources.Reservaciones_Resources.ResourceManager)
        Dim exito As Boolean = True
        Dim agregoDetalle As Boolean = False


        Dim fecha As Date = CalendarInicio.SelectedDate
        Dim fechaInicio As New Date(fecha.Year, fecha.Month, fecha.Day, ddl_horaInicio.SelectedValue, ddl_minutoInicio.SelectedValue, 0)
        fecha = CalendarFinal.SelectedDate
        Dim fechaFinal As New Date(fecha.Year, fecha.Month, fecha.Day, ddl_horaFinal.SelectedValue, ddl_minutoFinal.SelectedValue, 0)
        ReservacionActual = controladora.CrearReservacion(ddl_clientes.SelectedValue, DateHandler.ToUtcFromLocalizedDate(fechaInicio), DateHandler.ToUtcFromLocalizedDate(fechaFinal), securityHandler.Usuario, IIf(Chb_bloqueado.Checked, False, True), "", 0, txb_comentario_Reservar.Text)
        cargarReservaciones()


        If ReservacionActual > 0 Then
            For counter As Integer = 0 To gv_ResultadosDisponibles.Rows.Count - 1
                Dim item As GridViewRow = gv_ResultadosDisponibles.Rows(counter)
                Dim lbl_nombre As Label = item.FindControl("lbl_nombre")
                Dim check As CheckBox = item.FindControl("chk_reservar")
                Dim tbx_Adultos As TextBox = item.FindControl("tbx_Adultos")
                Dim tbx_Ninos As TextBox = item.FindControl("tbx_Ninos")

                Dim ordinal As Integer = lbl_nombre.Attributes("ordinal")
                Dim id_producto As Integer = lbl_nombre.Attributes("producto")
                Dim code As ControladorReservaciones.Resultado_Code = ControladorReservaciones.Resultado_Code.OK
                Dim cant_adultos As Integer = 0
                Dim cant_ninos As Integer = 0
                Try
                    cant_adultos = tbx_Adultos.Text
                    cant_ninos = tbx_Ninos.Text
                Catch ex As Exception

                End Try
                If check.Checked Then
                    Try
                        If Not cant_adultos + cant_ninos > 0 Then
                            Throw New Exception("LA cantidad total de personas debe ser mayor que 0")
                        End If
                        code = controladora.AgregarItem(ReservacionActual, securityHandler.Entidad, id_producto, ordinal, cant_adultos, cant_ninos, False)
                        If code = ControladorReservaciones.Resultado_Code.OK Then
                            MyMaster.concatenarMensaje(lbl_nombre.Text & " reservado.", False)
                            agregoDetalle = True
                        Else
                            MyMaster.concatenarMensaje("Error con detalle de " & lbl_nombre.Text & " " & controladora.obtenerMensajeError(code), True)
                        End If
                    Catch ex As Exception
                        exito = False
                        MyMaster.MostrarMensaje("Reservacion cancelada", True)
                        MyMaster.concatenarMensaje(ex.Message, True)
                        controladora.NuevoEstadoParaReservacion(ReservacionActual, controladora.BuscarEstadoCancelado, securityHandler.Usuario, "Reservacion cancelada por error en el proceso", IIf(Chb_bloqueado.Checked, False, True))
                    End Try
                End If
            Next

            'cargarItemsDisponibles()
            ClearInfo(Configuration.EstadoPantalla.CONSULTAR, False)
            If Not agregoDetalle Then
                MyMaster.MostrarMensaje("Reservacion cancelada", True)
                MyMaster.concatenarMensaje("Debe seleccionar productos", True)
                controladora.NuevoEstadoParaReservacion(ReservacionActual, controladora.BuscarEstadoCancelado, securityHandler.Usuario, "Reservacion cancelada  por falta de items", IIf(Chb_bloqueado.Checked, False, True))
                ClearInfo(Configuration.EstadoPantalla.NORMAL, False)
                exito = False
            End If
            If exito Then
                MyMaster.MostrarMensaje("Reservacion realizada", False)
            End If
            loadReservacion(ReservacionActual)
            cargarItemsReservados(ReservacionActual)
            upd_PantallaContenido.Update()
        Else
            MyMaster.MostrarMensaje("Error al reservar", True)
        End If
    End Sub

    'Protected Sub btn_agregar_Click(ByVal Sender As Object, ByVal E As System.EventArgs) Handles btn_agregar.Click
    '    Dim controladora As New ControladorReservaciones(connection, Resources.Reservaciones_Resources.ResourceManager)

    '    If ReservacionActual <= 0 Then
    '        Dim fecha As Date = CalendarInicio.SelectedDate
    '        Dim fechaInicio As New Date(fecha.Year, fecha.Month, fecha.Day, ddl_horaInicio.SelectedValue, ddl_minutoInicio.SelectedValue, 0)
    '        fecha = CalendarFinal.SelectedDate
    '        Dim fechaFinal As New Date(fecha.Year, fecha.Month, fecha.Day, ddl_horaFinal.SelectedValue, ddl_minutoFinal.SelectedValue, 0)
    '        ReservacionActual = controladora.CrearReservacion(ddl_clientes.SelectedValue, fechaInicio, fechaFinal, IIf(Chb_bloqueado.Checked, False, True))
    '        MyMaster.MostrarMensaje("Reservacion realizada", False)
    '        cargarReservaciones()
    '    End If

    '    If ReservacionActual > 0 Then
    '        For counter As Integer = 0 To gv_ResultadosDisponibles.Rows.Count - 1
    '            Dim item As GridViewRow = gv_ResultadosDisponibles.Rows(counter)
    '            Dim lbl_nombre As Label = item.FindControl("lbl_nombre")
    '            Dim check As CheckBox = item.FindControl("chk_reservar")
    '            Dim tbx_Adultos As TextBox = item.FindControl("tbx_Adultos")
    '            Dim tbx_Ninos As TextBox = item.FindControl("tbx_Ninos")

    '            Dim ordinal As Integer = lbl_nombre.Attributes("ordinal")
    '            Dim id_producto As Integer = lbl_nombre.Attributes("producto")
    '            Dim code As ControladorReservaciones.Resultado_Code = ControladorReservaciones.Resultado_Code.OK
    '            If check.Checked Then
    '                Try
    '                    code = controladora.AgregarItem(ReservacionActual, securityHandler.Entidad, id_producto, ordinal, tbx_Adultos.Text, tbx_Ninos.Text)
    '                    If code = ControladorReservaciones.Resultado_Code.OK Then
    '                        MyMaster.concatenarMensaje(lbl_nombre.Text & " reservado.", False)
    '                    Else
    '                        MyMaster.concatenarMensaje("Error con detalle de " & lbl_nombre.Text & " " & controladora.obtenerMensajeError(code), True)
    '                    End If
    '                Catch ex As Exception
    '                    MyMaster.concatenarMensaje(ex.Message & " de " & lbl_nombre.Text, True)
    '                End Try
    '            End If
    '        Next

    '        'cargarItemsDisponibles()
    '        ClearInfo(Configuration.EstadoPantalla.CONSULTAR, False)
    '        loadReservacion(ReservacionActual)
    '        cargarItemsReservados(ReservacionActual)
    '        upd_PantallaContenido.Update()
    '    Else
    '        MyMaster.MostrarMensaje("Error al reservar", True)
    '    End If
    'End Sub

    Protected Sub gv_itemModificar_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gv_itemModificar.RowCommand
        Dim elItem As GridViewRow = gv_itemModificar.Rows(e.CommandArgument)
        eliminarDetalle(elItem)
    End Sub

    Protected Sub btn_cancelarB_Click(ByVal Sender As Object, ByVal E As System.EventArgs) Handles btn_cancelarB.Click
        ClearInfo(Configuration.EstadoPantalla.NORMAL, False)
        desabilitarFechas(False)
    End Sub

    Protected Sub btn_nueva_Click(ByVal Sender As Object, ByVal E As System.EventArgs) Handles btn_nueva.Click
        ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
        setHorasIniciales()
        desabilitarFechas(False)
        upd_PantallaContenido.Update()
    End Sub

    'Protected Sub btn_finalizar_Click(ByVal Sender As Object, ByVal E As System.EventArgs) Handles btn_finalizar.Click
    '    MyMaster.MostrarMensaje("Reservacion Finalizada", False)
    '    ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
    '    desabilitarFechas(False)
    '    algo = Nothing
    'End Sub

    'Protected Sub btn_cambiarFecha_Click(ByVal Sender As Object, ByVal E As System.EventArgs) Handles btn_cambiarFecha.Click
    '    MyMaster.MostrarMensaje("Al cambiar la fecha todos los productos deben verificarse para confirmar su disponibilidad", False)
    '    CalendarExtender1.Enabled = True
    '    CalendarExtender2.Enabled = True
    '    upd_DetalleReservacion.Update()
    'End Sub

    'Protected Sub btn_cancelarVerFecha_Click(ByVal Sender As Object, ByVal E As System.EventArgs) Handles btn_cancelarVerFecha.Click
    '    'botones.Visible = True
    '    'div_fecha.Visible = False
    '    div_ResultadosBusqueda.Visible = True
    '    cargarItemsReservados(algo)
    '    'cargarItemsDisponibles()
    '    desabilitarFechas(True)
    '    upd_Reservacion.Update()
    'End Sub

    'Protected Sub btn_verificarFecha_Click(ByVal Sender As Object, ByVal E As System.EventArgs) Handles btn_verificarFecha.Click
    '    Dim id_reservacion As Integer = algo
    '    If fechaPermitida() Then
    '        If verificarDisponibilidad(id_reservacion) Then
    '            modificarFecha(id_reservacion)
    '            'botones.Visible = True
    '            'div_fecha.Visible = False
    '            desabilitarFechas(True)
    '            hyl_agregarItem.Visible = True
    '            Dim fechaInicio As Date = bdp_FechaInicio.SelectedDate
    '            Dim fechaFin As Date = bdp_FechaFinal.SelectedDate
    '            hyl_agregarItem.NavigateUrl = MyMaster.obtenerIframeString("agregarItem.aspx?id_reserva=" & id_reservacion & "&fechaInicio=" & fechaInicio & "&fechaFinal=" & fechaFin)
    '        End If
    '    Else
    '        MyMaster.MostrarMensaje("Fecha Inicio debe ser menor que la fecha final", True)
    '    End If
    'End Sub

    Protected Sub ddl_TipoEstado_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddl_TipoEstado.SelectedIndexChanged
        Dim controladora As New ControladorReservaciones(connection, Resources.Reservaciones_Resources.ResourceManager)
        If ddl_TipoEstado.SelectedValue <> Me.TipoEstadoActual Then
            pnl_comentarios.Visible = True
            upd_DetalleReservacion.Update()
        End If
    End Sub


End Class
