Imports Orbelink.DBHandler
Imports Orbelink.Entity.Reservaciones
Imports Orbelink.Control.Reservaciones
Imports Orbelink.Entity.Facturas
Imports Orbelink.Entity.Entidades
Imports Orbelink.Entity.Productos

Partial Class Orbecatalog_Reservacion_Administracion
    Inherits PageBaseClass

    Const codigo_pantalla As String = "Rs-04"
    Const level As Integer = 2

    Protected Sub Page_Load1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddScripts(level)

        If Not IsPostBack Then
            securityHandler.VerifyPantalla(codigo_pantalla, level)
            cargarInformacion()
            Me.Hyl_AgregarProducto.NavigateUrl = MyMaster.obtenerIframeString("Item.aspx")
            Me.Hyl_AgregarEstado.NavigateUrl = MyMaster.obtenerIframeString("TipoEstadoReservacion.aspx")
            Me.Hyl_AgregarEntidad.NavigateUrl = MyMaster.obtenerIframeString("../Contactos/Entidad.aspx")
        End If
    End Sub


    'Cargar Informacion
    Protected Sub cargarInformacion()
        cargarEntidades()
        cargarProductos()
        cargarEstados()
    End Sub

    Protected Sub cargarProductos()
        Dim Producto As New Producto
        Producto.Nombre.ToSelect = True
        Producto.Id_Producto.ToSelect = True
        Producto.Activo.Where.EqualCondition(1)
        Dim items As New Item
        queryBuilder.Join.EqualCondition(items.Id_producto, Producto.Id_Producto)
        queryBuilder.From.Add(Producto)
        queryBuilder.Distinct = True
        Dim consulta As String = queryBuilder.RelationalSelectQuery
        Dim dataTable As Data.DataTable = connection.executeSelect_DT(consulta)

        If dataTable.Rows.Count > 0 Then
            ddl_productos.DataSource = dataTable
            ddl_productos.DataTextField = Producto.Nombre.Name
            ddl_productos.DataValueField = Producto.Id_Producto.Name
            ddl_productos.DataBind()
            ddl_productos.Items.Add(New ListItem("Cualquiera", -1))
            ddl_productos.SelectedValue = -1
        End If
    End Sub

    Protected Sub cargarEntidades()

        Dim Entidad As New Entidad

        Entidad.NombreDisplay.ToSelect = True
        Entidad.Id_entidad.ToSelect = True
        Dim consulta As String = queryBuilder.SelectQuery(Entidad)
        Dim dataTable As Data.DataTable = connection.executeSelect_DT(consulta)

        If dataTable.Rows.Count > 0 Then
            ddl_cliente.DataSource = dataTable
            ddl_cliente.DataTextField = Entidad.NombreDisplay.Name
            ddl_cliente.DataValueField = Entidad.Id_entidad.Name
            ddl_cliente.DataBind()
            ddl_cliente.Items.Add(New ListItem("Cualquiera", -1))
            ddl_cliente.SelectedValue = -1
        End If
    End Sub

    Protected Sub cargarEstados()
        Dim ControladorReservaciones As New ControladorReservaciones(connection, Resources.Reservaciones_Resources.ResourceManager)
        ControladorReservaciones.selectTipoEstadoReservacion_BindListCollection(ddl_estados.Items)
        ddl_estados.Items.Add("-- Todos --")
        ddl_estados.SelectedIndex = ddl_estados.Items.Count - 1
    End Sub

    Protected Sub cargarBusqueda()
        Dim reserva As New Reservacion
        Dim detalle As New Detalle_Reservacion
        Dim entidad As New Entidad
        Dim factura As New Factura

        Dim tipoEstado As New TipoEstadoReservacion

        tipoEstado.Nombre.ToSelect = True
        queryBuilder.Join.EqualCondition(tipoEstado.Id_TipoEstadoReservacion, reserva.Id_TipoEstado)

        entidad.NombreDisplay.ToSelect = True
        reserva.fecha_inicioProgramado.ToSelect = True
        reserva.fecha_finalProgramado.ToSelect = True
        reserva.Id_TipoEstado.ToSelect = True
        reserva.Id_Reservacion.ToSelect = True
        factura.Total.ToSelect = True

        queryBuilder.Join.EqualCondition(factura.Id_Factura, reserva.Id_factura)
        queryBuilder.Join.EqualCondition(entidad.Id_entidad, reserva.id_Cliente)

        If ddl_productos.SelectedValue > 0 Then
            queryBuilder.Join.EqualCondition(detalle.id_reservacion, reserva.Id_Reservacion)
            detalle.Id_producto.Where.EqualCondition(ddl_productos.SelectedValue)
            queryBuilder.Distinct = True
            queryBuilder.From.Add(detalle)
        End If

        If ddl_estados.SelectedIndex < ddl_estados.Items.Count - 1 Then
            reserva.Id_TipoEstado.Where.EqualCondition(ddl_estados.SelectedValue)
        End If

        If bdp_FechaInicio.SelectedDate <> Date.MinValue Then
            reserva.fecha_inicioProgramado.Where.GreaterThanOrEqualCondition(bdp_FechaInicio.SelectedDate)
        End If


        If bdp_FechaFinal.SelectedDate <> Date.MinValue Then
            reserva.fecha_finalProgramado.Where.LessThanOrEqualCondition(bdp_FechaFinal.SelectedDate)
        End If

        If ddl_cliente.SelectedValue > 0 Then
            reserva.id_Cliente.Where.EqualCondition(ddl_cliente.SelectedValue)
        End If

        queryBuilder.From.Add(reserva)
        queryBuilder.From.Add(entidad)
        queryBuilder.From.Add(factura)
        queryBuilder.From.Add(tipoEstado)

        Try
            Dim consulta As String = queryBuilder.RelationalSelectQuery
            Dim dataTable As Data.DataTable = connection.executeSelect_DT(consulta)
            If dataTable.Rows.Count > 0 Then
                dg_reservaciones.DataSource = dataTable
                dg_reservaciones.DataBind()
                Dim resul_reserva As ArrayList = TransformDataTable(dataTable, reserva)
                Dim resul_entidad As ArrayList = TransformDataTable(dataTable, entidad)
                Dim resul_factura As ArrayList = TransformDataTable(dataTable, factura)
                Dim result_TipoEstado As ArrayList = TransformDataTable(dataTable, tipoEstado)

                Dim counter As Integer = 0
                For Each unItem As GridViewRow In dg_reservaciones.Rows
                    reserva = resul_reserva(counter)
                    entidad = resul_entidad(counter)
                    factura = resul_factura(counter)
                    tipoEstado = result_TipoEstado(counter)

                    Dim nombre As Label = unItem.FindControl("lbl_nombre")
                    Dim fechaInicio As Label = unItem.FindControl("lbl_fechaInicio")
                    fechaInicio.Attributes.Add("id_reserva", reserva.Id_Reservacion.Value)
                    Dim fechaFinal As Label = unItem.FindControl("lbl_fechaFin")
                    Dim cantidad As Label = unItem.FindControl("lbl_cantidad")
                    Dim estado As Label = unItem.FindControl("lbl_estado")
                    Dim total As Label = unItem.FindControl("lbl_total")
                    Dim hyl_detalle As HyperLink = unItem.FindControl("hyl_detalle")

                    nombre.Text = entidad.NombreDisplay.Value
                    fechaInicio.Text = reserva.fecha_inicioProgramado.ValueLocalized
                    fechaFinal.Text = reserva.fecha_finalProgramado.ValueLocalized
                    cantidad.Text = cantidadItems(reserva.Id_Reservacion.Value)
                    estado.Text = tipoEstado.Nombre.Value
                    total.Text = factura.Total.Value
                    hyl_detalle.NavigateUrl = "Reservacion.aspx?id_reservacion=" & reserva.Id_Reservacion.Value
                    counter += 1
                Next
                verReservaciones.Visible = True
                lbl_noDisponibles.Visible = False
                dg_reservaciones.Visible = True
            Else
                verReservaciones.Visible = True
                lbl_noDisponibles.Visible = True
                dg_reservaciones.Visible = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    'Acciones   
    Protected Sub btn_buscar_Click(ByVal Sender As Object, ByVal E As System.EventArgs) Handles btn_buscar.Click
        cargarBusqueda()
    End Sub

    Protected Sub btn_limpiar_Click(ByVal Sender As Object, ByVal E As System.EventArgs) Handles btn_limpiar.Click
        verReservaciones.Visible = False
        bdp_FechaFinal.SelectedDate = Date.MinValue
        bdp_FechaInicio.SelectedDate = Date.MinValue
    End Sub

    Protected Function cantidadItems(ByVal id_reservacion As Integer) As Integer
        Dim cantidad As Integer = 0
        Dim detalle As New Detalle_Reservacion
        detalle.Id_producto.ToSelect = True
        detalle.id_reservacion.Where.EqualCondition(id_reservacion)

        Dim consulta As String = queryBuilder.SelectQuery(detalle)
        Dim dataTable As Data.DataTable = connection.executeSelect_DT(consulta)
        If dataTable.Rows.Count > 0 Then
            cantidad = dataTable.Rows.Count
        End If

        Return cantidad
    End Function

End Class
