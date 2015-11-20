Imports Orbelink.DBHandler
Imports Orbelink.Entity.Reservaciones
Imports Orbelink.Control.Reservaciones
Imports Orbelink.Entity.Productos
Imports Orbelink.Entity.Currency

Partial Class Orbecatalog_Reservacion_preciosTemporadaYNoches
    Inherits PageBaseClass

    Const codigo_pantalla As String = "Rs-10"
    Const level As Integer = 2

    Protected Sub Orbecatalog_Reservacion_preciosTemporada_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddScripts(level)
        If Not IsPostBack Then
            securityHandler.VerifyPantalla(codigo_pantalla, level)
            cargarProductos()
            cargarUnidades()
            cargarUnidadesTiempo()
            cargarNoches()
            selectTemporadas()

            If ddl_temporada.SelectedValue.Length > 0 And ddl_producto.SelectedValue.Length > 0 And ddl_cantidadNoches.SelectedValue.Length > 0 Then
                cargarCantidadPersonas(ddl_producto.SelectedValue)
                cargarDatosTemporada(ddl_temporada.SelectedValue, ddl_producto.SelectedValue, ddl_cantidadNoches.SelectedValue, ddl_cantidadPersonas.SelectedValue)
            Else
                cargarCantidadPersonas(0)
                limpiarDatos()
            End If
            Me.Hyl_AgregarTemporada.NavigateUrl = MyMaster.obtenerIframeString("Temporada.aspx")
            Me.Hyl_agregarunidad.NavigateUrl = MyMaster.obtenerIframeString("../Moneda.aspx")
            Me.Hyl_AgregarProducto.NavigateUrl = MyMaster.obtenerIframeString("Item.aspx")
        End If
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
            ddl_producto.DataSource = dataTable
            ddl_producto.DataTextField = Producto.Nombre.Name
            ddl_producto.DataValueField = Producto.Id_Producto.Name
            ddl_producto.DataBind()
        End If
    End Sub

    'Protected Sub cargarUnidades()
    '    Dim unidad As New Unidades
    '    unidad.Nombre.ToSelect = True
    '    unidad.Id_Unidad.ToSelect = True
    '    Dim consulta As String = queryBuilder.SelectQuery(unidad)
    '    Dim dataTable As Data.DataTable = connection.executeSelect_DT(consulta)

    '    If dataTable.Rows.Count > 0 Then
    '        ddl_unidades.DataSource = dataTable
    '        ddl_unidades.DataTextField = unidad.Nombre.Name
    '        ddl_unidades.DataValueField = unidad.Id_Unidad.Name
    '        ddl_unidades.DataBind()
    '    End If
    'End Sub

    Protected Sub cargarUnidades()
        Dim unidad As New Moneda
        unidad.Nombre.ToSelect = True
        unidad.Id_Moneda.ToSelect = True
        Dim consulta As String = queryBuilder.SelectQuery(unidad)
        Dim dataTable As Data.DataTable = connection.executeSelect_DT(consulta)

        If dataTable.Rows.Count > 0 Then
            ddl_unidades.DataSource = dataTable
            ddl_unidades.DataTextField = unidad.Nombre.Name
            ddl_unidades.DataValueField = unidad.Id_Moneda.Name
            ddl_unidades.DataBind()
        End If
    End Sub

    Protected Sub cargarUnidadesTiempo()
        Dim item As New ListItem("Hora", ControladorReservaciones.unidadTiempo.Hora)
        Dim item2 As New ListItem("Dia", ControladorReservaciones.unidadTiempo.Dia)
        Dim item3 As New ListItem("5 Dias", ControladorReservaciones.unidadTiempo._5_dias)
        Dim item4 As New ListItem("7 Dias", ControladorReservaciones.unidadTiempo._7_dias)
        Dim item5 As New ListItem("8 Dias", ControladorReservaciones.unidadTiempo._8_dias)
        ddl_unidadTiempo.Items.Add(item)
        ddl_unidadTiempo.Items.Add(item2)
        ddl_unidadTiempo.Items.Add(item3)
        ddl_unidadTiempo.Items.Add(item4)
        ddl_unidadTiempo.Items.Add(item5)
        ddl_unidadTiempo.SelectedIndex = 1
        ddl_unidadTiempo.Enabled = False
    End Sub

    Protected Sub cargarNoches()

        For i As Integer = 0 To 30
            ddl_cantidadNoches.Items.Add(i + 1)
        Next
      
    End Sub

    Protected Sub selectTemporadas()
        Dim Temporada As New Temporada

        Temporada.Fields.SelectAll()
        queryBuilder.Orderby.Add(Temporada.Nombre)
        queryBuilder.From.Add(Temporada)
        Dim dataTable As Data.DataTable = connection.executeSelect_DT(queryBuilder.RelationalSelectQuery)

        If dataTable.Rows.Count > 0 Then
            ddl_temporada.DataSource = dataTable
            ddl_temporada.DataTextField = Temporada.Nombre.Name
            ddl_temporada.DataValueField = Temporada.Id_Temporada.Name
            ddl_temporada.DataBind()
        End If
    End Sub

    Protected Sub cargarDatosTemporada(ByVal id_temporada As Integer, ByVal id_producto As Integer, ByVal noches As Integer, ByVal personas As Integer)
        id_Actual = id_temporada
        Dim precios As New Precios_Temporada
        precios.Fields.SelectAll()
        precios.Id_Producto.Where.EqualCondition(id_producto)
        precios.Id_Temporada.Where.EqualCondition(id_temporada)
        precios.CantidadNoches.Where.EqualCondition(noches)
        precios.CantidadPersonas.Where.EqualCondition(personas)
        queryBuilder.From.Add(precios)
        Dim consulta As String = queryBuilder.RelationalSelectQuery
        Dim dataTable As Data.DataTable = connection.executeSelect_DT(consulta)
        If dataTable.Rows.Count > 0 Then
            ObjectBuilder.CreateObject(dataTable, 0, precios)
            ddl_unidadTiempo.SelectedValue = precios.UnidadTiempo.Value
            txb_precioProducto.Text = precios.PrecioProducto.Value
            txb_precioNocheExtraAdulto.Text = precios.PrecioNocheExtraAd.Value
            txb_precioNocheExtraNino.Text = precios.PrecioNocheExtraNino.Value
            'txb_cantidadNoches.Text = precios.CantidadNoches.Value
            ddl_cantidadNoches.SelectedValue = precios.CantidadNoches.Value
            cargarCantidadPersonas(precios.Id_Producto.Value)
            ddl_cantidadPersonas.SelectedValue = precios.CantidadPersonas.Value
            ddl_unidades.SelectedValue = precios.Id_Moneda.Value

        Else
            'cargarCantidadPersonas(id_producto)
            limpiarDatos()
        End If
        upd_Contenido.Update()
    End Sub

    Protected Sub cargarCantidadPersonas(ByVal id_producto As Integer)
        Dim cantidadPersonasAnterior As Integer
        If ddl_cantidadPersonas.SelectedValue.Length > 0 Then
            cantidadPersonasAnterior = ddl_cantidadPersonas.SelectedValue
        Else
            cantidadPersonasAnterior = 1
        End If

        ddl_cantidadPersonas.Items.Clear()
        If id_producto > 0 Then
            Dim productoshandler As New Orbelink.Control.Productos.ProductosHandler(connection.connectionString)
            Dim producto As Producto = productoshandler.ConsultarProducto(id_producto)
            If producto.CapacidadMaxima.Value > 0 Then
                For i As Integer = 1 To producto.CapacidadMaxima.Value
                    ddl_cantidadPersonas.Items.Add(i)
                Next
            Else
                ddl_cantidadPersonas.Items.Add(1)
            End If
        Else
            ddl_cantidadPersonas.Items.Add(1)
        End If
        Try
            ddl_cantidadPersonas.SelectedValue = cantidadPersonasAnterior
        Catch ex As Exception
        End Try
    End Sub

    Protected Sub limpiarDatos()
        txb_precioProducto.Text = 0
        txb_precioNocheExtraAdulto.Text = 0
        txb_precioNocheExtraNino.Text = 0
        'txb_cantidadNoches.Text = 0
        'ddl_cantidadNoches.SelectedValue = 1
        'ddl_cantidadPersonas.SelectedValue = 1
        upd_Contenido.Update()
    End Sub


    Protected Sub btn_Cancelar_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Cancelar.ServerClick
        'cargarDatosTemporada(id_Actual)
     cargarPrecios()
    End Sub

    Protected Sub btn_Salvar_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Salvar.ServerClick
        If ddl_temporada.SelectedValue.Length > 0 And ddl_producto.SelectedValue.Length > 0 And ddl_cantidadNoches.SelectedValue.Length > 0 And ddl_cantidadPersonas.SelectedValue.Length > 0 Then
            salvar(ddl_temporada.SelectedValue, ddl_producto.SelectedValue, ddl_cantidadNoches.SelectedValue, ddl_cantidadPersonas.SelectedValue)
        Else
            MyMaster.MostrarMensaje("Datos inválidos", True)
        End If

    End Sub

    Protected Sub salvar(ByVal id_temporada As Integer, ByVal id_producto As Integer, ByVal noches As Integer, ByVal personas As Integer)
    
        Try
            Dim precio As New Precios_Temporada
            precio.Id_Temporada.ToSelect = True
            precio.Id_Producto.Where.EqualCondition(id_producto)
            precio.Id_Temporada.Where.EqualCondition(id_temporada)
            precio.CantidadNoches.Where.EqualCondition(noches)
            precio.CantidadPersonas.Where.EqualCondition(personas)

            Dim consulta As String = queryBuilder.SelectQuery(precio)
            Dim dataTable As Data.DataTable = connection.executeSelect_DT(consulta)
            precio.UnidadTiempo.Value = ddl_unidadTiempo.SelectedValue
            precio.PrecioProducto.Value = txb_precioProducto.Text
            precio.PrecioAdAdulto.Value = 0
            precio.PrecioAdNino.Value = 0
            precio.PrecioAdAdultoExtra.Value = 0
            precio.PrecioAdNinoExtra.Value = 0
            precio.Id_Moneda.Value = ddl_unidades.SelectedValue

            precio.PrecioNocheExtraAd.Value = txb_precioNocheExtraAdulto.Text
            precio.PrecioNocheExtraNino.Value = txb_precioNocheExtraNino.Text

            If dataTable.Rows.Count > 0 Then
                precio.Id_Temporada.Where.EqualCondition(id_temporada)
                precio.Id_Producto.Where.EqualCondition(id_producto)
                precio.CantidadPersonas.Where.EqualCondition(personas)
                precio.CantidadNoches.Where.EqualCondition(noches)
                consulta = queryBuilder.UpdateQuery(precio)
                connection.executeUpdate(consulta)
                MyMaster.MostrarMensaje("Precios modificados exitosamente", False)
            Else
                precio.Id_Temporada.Value = id_temporada
                precio.Id_Producto.Value = id_producto
                precio.CantidadNoches.Value = noches
                precio.CantidadPersonas.Value = personas
                consulta = queryBuilder.InsertQuery(precio)
                connection.executeSelect(consulta)
                MyMaster.MostrarMensaje("Precios introducidos exitosamente", False)
            End If
        Catch ex As Exception
            'cargarDatosTemporada(id_Actual)
            cargarPrecios()
            MyMaster.MostrarMensaje(ex.Message, True)
        End Try

    End Sub

    'Protected Sub selectTemporada()
    '    Dim Temporada As New Temporada

    '    Temporada.Fields.SelectAll()
    '    queryBuilder.Orderby.Add(Temporada.Nombre)
    '    Dim dataTable As Data.DataTable = connection.executeSelect_DT(queryBuilder.SelectQuery(Temporada))

    '    If dataTable.Rows.Count > 0 Then
    '        dg_Temporada.DataSource = dataTable
    '        dg_Temporada.DataKeyField = Temporada.Id_Temporada.Name
    '        dg_Temporada.DataBind()

    '        'Llena el grid
    '        Dim offset As Integer = dg_Temporada.CurrentPageIndex * dg_Temporada.PageSize
    '        Dim counter As Integer
    '        Dim result_Temporada As ArrayList = ObjectBuilder.TransformDataTable(dataTable, Temporada)
    '        For counter = 0 To dg_Temporada.Items.Count - 1
    '            Dim act_Temporada As Temporada = result_Temporada(offset + counter)
    '            Dim lbl_NombreAtributo As Label = dg_Temporada.Items(counter).FindControl("lbl_Nombre")
    '            lbl_NombreAtributo.Text = act_Temporada.Nombre.Value

    '            'Javascript
    '            dg_Temporada.Items(counter).Attributes.Add("onmouseover", "setRowFocus(this, " & counter & ", false)")
    '            dg_Temporada.Items(counter).Attributes.Add("onmouseout", "unsetRowFocus(this, " & counter & ", false)")
    '        Next
    '        lbl_ResultadoElementos.Text = contadorElementos(offset, dg_Temporada.PageSize, dataTable.Rows.Count)
    '        dg_Temporada.Visible = True
    '    Else
    '        lbl_ResultadoElementos.Text = contadorElementos(0, 0, 0)
    '        dg_Temporada.Visible = False
    '    End If
    'End Sub

    'Protected Sub dg_Temporada_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dg_Temporada.EditCommand
    '    Me.EditCommandDataGrid(dg_Temporada, e.Item.ItemIndex)
    '    cargarDatosTemporada(dg_Temporada.DataKeys(e.Item.ItemIndex))
    '    Dim temporada As New Temporada
    '    temporada.Nombre.ToSelect = True
    '    temporada.Id_Temporada.Where.EqualCondition(dg_Temporada.DataKeys(e.Item.ItemIndex))
    '    Dim consulta As String = queryBuilder.SelectQuery(temporada)
    '    Dim dataTable As Data.DataTable = connection.executeSelect_DT(consulta)
    '    If dataTable.Rows.Count > 0 Then
    '        ObjectBuilder.CreateObject(dataTable, 0, temporada)
    '        lbl_temporada.Text = temporada.Nombre.Value
    '    Else
    '        lbl_temporada.Text = ""
    '    End If
    '    upd_Contenido.Update()
    '    upd_Busqueda.Update()
    'End Sub

    'Protected Sub dg_Temporada_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dg_Temporada.PageIndexChanged
    '    dg_Temporada.CurrentPageIndex = e.NewPageIndex
    '    selectTemporada()
    '    Me.PageIndexChange(dg_Temporada)
    '    upd_Busqueda.Update()
    'End Sub

    Protected Sub ddl_producto_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddl_producto.SelectedIndexChanged
        'cargarDatosTemporada(id_Actual)
        If ddl_producto.SelectedValue.Length > 0 Then
            cargarCantidadPersonas(ddl_producto.SelectedValue)
        End If
        cargarPrecios()
    End Sub

    Protected Sub ddl_temporada_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddl_temporada.SelectedIndexChanged
      cargarPrecios()
    End Sub

    Protected Sub ddl_cantidadNoches_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddl_cantidadNoches.SelectedIndexChanged
     cargarPrecios()
    End Sub

    Protected Sub ddl_cantidadPersonas_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddl_cantidadPersonas.SelectedIndexChanged
        cargarPrecios()
    End Sub

    Protected Sub cargarPrecios()
        If ddl_temporada.SelectedValue.Length > 0 And ddl_producto.SelectedValue.Length > 0 And ddl_cantidadNoches.SelectedValue.Length > 0 And ddl_cantidadPersonas.SelectedValue.Length > 0 Then
            cargarDatosTemporada(ddl_temporada.SelectedValue, ddl_producto.SelectedValue, ddl_cantidadNoches.SelectedValue, ddl_cantidadPersonas.SelectedValue)
        End If
        upd_Contenido.Update()
        upd_Busqueda.Update()
    End Sub
End Class
