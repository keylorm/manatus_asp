Imports Orbelink.DBHandler
Imports Orbelink.Entity.Reservaciones
Imports Orbelink.Entity.Productos
Imports Orbelink.Control.Reservaciones
Imports Orbelink.Entity.Currency
Imports Orbelink.DateAndTime

Partial Class Orbecatalog_Reservacion_AgregarItem
    Inherits PageBaseClass

    Const codigo_pantalla As String = "Rs-05"
    Const level As Integer = 2

    Protected Sub Orbecatalog_Reservacion_AgregarItem_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddScripts(level)

        If Not Request.Params("id_reserva") Is Nothing Then
            id_Actual = Request.Params("id_reserva")

            If Not IsPostBack Then
                securityHandler.VerifyPantalla(codigo_pantalla, level)
                cargarProductos()
                cargarDescripcion()
                cargarItemsDisponibles(Request.Params("fechaInicio"), Request.Params("fechaFinal"))
            End If
        Else
            Response.Redirect("default.aspx")
        End If
    End Sub

    'Cargar datos
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
        End If
    End Sub

    Protected Sub cargarDescripcion()
        Dim item As New Item

        item.descripcion.ToSelect = True
        item.Id_producto.Where.EqualCondition(ddl_productos.SelectedValue)

        Dim consulta As String = queryBuilder.SelectQuery(item)
        Dim dataTable As Data.DataTable = connection.executeSelect_DT(consulta)
        If dataTable.Rows.Count > 0 Then
            ObjectBuilder.CreateObject(dataTable, 0, item)
            lbl_descripcion.Text = item.descripcion.Value
        Else
            lbl_descripcion.Text = ""
        End If
    End Sub

    Protected Sub cargarItemsDisponibles(ByVal laFechaInicio As Date, ByVal laFechaFinal As Date)
        Dim id_producto As Integer = ddl_productos.SelectedValue
        cargarDescripcion()

        Dim controladora As New ControladorReservaciones(connection, Resources.Reservaciones_Resources.ResourceManager)
        Dim dataTable As Data.DataTable = controladora.cargarItemsDisponibles_ByProducto(id_producto, DateHandler.ToUtcFromLocalizedDate(laFechaInicio), DateHandler.ToUtcFromLocalizedDate(laFechaFinal), ControladorReservaciones.Config_UbicacionDefault, id_Actual)
        If dataTable.Rows.Count > 0 Then
            asignarInformacion(dataTable)
        Else
            noHayDisponibles(True)
        End If
    End Sub

    Protected Sub asignarInformacion(ByVal dataTable As Data.DataTable)
        dg_reservaciones.DataSource = dataTable
        dg_reservaciones.DataBind()

        Dim resul_producto As ArrayList = TransformDataTable(dataTable, New Producto)
        Dim resul_Item As ArrayList = TransformDataTable(dataTable, New Item)
        Dim resul_Unidad As ArrayList = TransformDataTable(dataTable, New Moneda)

        Dim unItem As GridViewRow
        Dim producto As Producto
        Dim item As Item
        Dim unidad As Moneda

        For counter As Integer = 0 To dataTable.Rows.Count - 1
            unItem = dg_reservaciones.Rows(counter)
            producto = resul_producto(counter)
            item = resul_Item(counter)
            unidad = resul_Unidad(counter)

            Dim lbl_nombre As Label = unItem.FindControl("lbl_nombre")
            Dim lbl_descripcion As Label = unItem.FindControl("lbl_descripcion")

            lbl_nombre.Text = producto.Nombre.Value
            lbl_descripcion.Text = item.descripcion.Value
            lbl_nombre.Attributes.Add("ordinal", item.ordinal.Value)
            lbl_nombre.Attributes.Add("producto", producto.Id_Producto.Value)
        Next
        noHayDisponibles(False)
    End Sub

    'Eventos
    Protected Sub btn_buscar_Click(ByVal Sender As Object, ByVal E As System.EventArgs) Handles btn_buscar.Click
        cargarItemsDisponibles(Request.Params("fechaInicio"), Request.Params("fechaFinal"))
    End Sub

    Protected Sub btn_agregar_Click(ByVal Sender As Object, ByVal E As System.EventArgs) Handles btn_agregar.Click
        Dim reservado As Boolean = True
        Dim code As ControladorReservaciones.Resultado_Code = ControladorReservaciones.Resultado_Code.OK
        Dim codeError As ControladorReservaciones.Resultado_Code = ControladorReservaciones.Resultado_Code.OK
        Dim controladora As New ControladorReservaciones(connection, Resources.Reservaciones_Resources.ResourceManager)
        For counter As Integer = 0 To dg_reservaciones.Rows.Count - 1
            Dim item As GridViewRow = dg_reservaciones.Rows(counter)
            Dim lbl_nombre As Label = item.FindControl("lbl_nombre")
            Dim check As CheckBox = item.FindControl("chk_reservar")
            Dim tbx_Adultos As TextBox = item.FindControl("tbx_Adultos")
            Dim tbx_Ninos As TextBox = item.FindControl("tbx_Ninos")

            Dim ordinal As Integer = lbl_nombre.Attributes("ordinal")
            Dim id_producto As Integer = lbl_nombre.Attributes("producto")

            If check.Checked Then
                code = controladora.AgregarItem(id_Actual, securityHandler.Usuario, id_producto, ordinal, tbx_Adultos.Text, tbx_Ninos.Text, False)
                If code <> ControladorReservaciones.Resultado_Code.OK Then
                    codeError = code
                    reservado = False
                End If
            End If
        Next
        cargarItemsDisponibles(Request.Params("fechaInicio"), Request.Params("fechaFinal"))
        If reservado Then
            MyMaster.MostrarMensaje("Reservacion realizada", False)
        Else
            MyMaster.MostrarMensaje(controladora.obtenerMensajeError(codeError), True)
        End If

    End Sub

    Protected Function obtenerFactura(ByVal id_reservacion As Integer) As Integer
        Dim numFactura As Integer = 0
        Dim reserva As New Reservacion

        reserva.Id_factura.ToSelect = True
        reserva.Id_Reservacion.Where.EqualCondition(id_reservacion)
        Dim consulta As String = queryBuilder.SelectQuery(reserva)
        Dim dataTable As Data.DataTable = connection.executeSelect_DT(consulta)
        If dataTable.Rows.Count > 0 Then
            ObjectBuilder.CreateObject(dataTable, 0, reserva)
            numFactura = reserva.Id_factura.Value
        End If
        Return numFactura
    End Function

    Protected Sub noHayDisponibles(ByVal disponibles As Boolean)
        lbl_noDisponibles.Visible = disponibles
        btn_agregar.Visible = Not disponibles
        dg_reservaciones.Visible = Not disponibles
        verReservaciones.Visible = True
    End Sub


End Class
