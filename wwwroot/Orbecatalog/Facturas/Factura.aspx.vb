Imports Orbelink.DBHandler
Imports Orbelink.Entity.Facturas
Imports Orbelink.Entity.Entidades
Imports Orbelink.Entity.Productos
Imports Orbelink.Control.Facturas
Imports Orbelink.Entity.Currency

Partial Class orbecatalog_Facturas_Factura
    Inherits PageBaseClass

    Const codigo_pantalla As String = "FC-02"
    Const level As Integer = 2

    'ViewState
    Dim _temp As Integer
    Protected Property FacturaActual() As Integer
        Get
            If _temp <= 0 Then
                If ViewState("id_FacturaActual") IsNot Nothing Then
                    _temp = ViewState("id_FacturaActual")
                Else
                    ViewState.Add("id_FacturaActual", 0)
                    _temp = 0
                End If
            End If
            Return _temp
        End Get
        Set(ByVal value As Integer)
            _temp = value
            ViewState("id_FacturaActual") = _temp
        End Set
    End Property

    Dim _temp2 As Integer
    Protected Property DetalleFacturaActual() As Integer
        Get
            If _temp2 <= 0 Then
                If ViewState("id_Detalle") IsNot Nothing Then
                    _temp2 = ViewState("id_Detalle")
                Else
                    ViewState.Add("id_Detalle", 0)
                    _temp2 = 0
                End If
            End If
            Return _temp2
        End Get
        Set(ByVal value As Integer)
            _temp2 = value
            ViewState("id_Detalle") = _temp2
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddScripts(level)

        securityHandler.VerifyPantalla(codigo_pantalla, level)

        If Not Page.IsPostBack Then
            dg_DetalleFactura.Attributes.Add("SelectedIndex", -1)
            dg_DetalleFactura.Attributes.Add("SelectedPage", -1)
            securityHandler.VerifyActions(btn_Salvar, btn_Modificar, btn_Eliminar)

            selectTiposFacturas()
            selectUnidades()
            selectEntidades(securityHandler.TipoEntidad)
            selectVendedor(securityHandler.Entidad)
            AgregaAtributosBotones()

            If Not Request.Params("id_Factura") Is Nothing Then
                Dim controladora As New FacturasHandler(connection)
                loadFactura(controladora, Request.Params.Item("id_Factura"))

                If FacturaActual > 0 Then
                    selectDetalleFactura(controladora, FacturaActual)
                    ClearInfo(Configuration.EstadoPantalla.CONSULTAR, False)
                Else
                    ClearInfo(Configuration.EstadoPantalla.NORMAL, False)
                End If
            Else
                ClearInfo(Configuration.EstadoPantalla.NORMAL, False)
            End If
        End If
    End Sub

    Protected Sub ClearInfo(ByVal estado As Configuration.EstadoPantalla, ByVal clearInfo As Boolean)
        If clearInfo Then
            txt_NumeroFactura.Text = ""
            FacturaActual = 0
            txt_FechaFactura.SelectedDate = Nothing
            lbl_FechaComprobante.Text = ""
            txt_Comprobante.Text = ""
            txt_PorcentajeImpuestos.Text = ""
            txt_EnviarPor.Text = ""

            Me.ClearDataGrid(DG_DetalleFactura, clearInfo)
        End If

        Select Case estado
            Case Configuration.EstadoPantalla.NORMAL
                btn_Modificar.Visible = False
                btn_Salvar.Visible = True
                btn_Eliminar.Visible = False
                hyl_reporteDetalle.Visible = False
                hyl_reportDetalle.Visible = False
                'div_DetalleProducto.Visible = False
                'div_AgregarProducto.Visible = False
                DG_DetalleFactura.Visible = False
                tab_Productos.Enabled = False
                'tab_reservaciones.Enabled = False
                cmb_Entidad.Enabled = True

            Case Configuration.EstadoPantalla.CONSULTAR
                btn_Modificar.Visible = True
                btn_Salvar.Visible = False
                btn_Eliminar.Visible = True
                tab_Productos.Enabled = True
                cmb_Entidad.Enabled = False
                'div_DetalleProducto.Visible = False
                'div_AgregarProducto.Visible = True
                DG_DetalleFactura.Visible = True
                hyl_reporteDetalle.Visible = True
                hyl_reportDetalle.Visible = True
        End Select
    End Sub

    Sub DisableActions()
        btn_Modificar.Disabled = True
        btn_Cancelar.Disabled = True
        btn_Eliminar.Disabled = True
        'btn_ModificarDetalle.Disabled = True
        'btn_AgregarDetalle.Disabled = True
        btn_MarcarCancelado.Disabled = True
        btn_MarcarCancelado.Visible = False
        btn_Modificar.Visible = False
        btn_Cancelar.Visible = False
        btn_Eliminar.Visible = False
        'div_AgregarProducto.Visible = False
        'btn_ModificarDetalle.Visible = False
        'btn_AgregarDetalle.Visible = False
        'upd_DetalleFactura.Update()
        upd_Factura.Update()
    End Sub

    'Factura
    Private Sub loadFactura(ByVal control As FacturasHandler, ByVal id_Factura As Integer)
        Dim Factura As Factura = control.ConsultarFactura(id_Factura)
        If Factura IsNot Nothing Then
            If Factura.Num_Factura.IsValid Then
                txt_NumeroFactura.Text = Factura.Num_Factura.Value
            End If

            txt_FechaFactura.SelectedDate = Factura.Fecha_Factura.ValueLocalized

            If Factura.Fecha_Comprobante.IsValid Then
                lbl_FechaComprobante.Text = Factura.Fecha_Comprobante.ValueLocalized
                txt_Comprobante.Text = Factura.Comprobante.Value
                div_FechaComprobante.Visible = True
            Else
                div_FechaComprobante.Visible = False
            End If

            Dim simboloUnidad As String = selectUnidadSimbolo(Factura.Id_Moneda.Value)

            txt_PorcentajeImpuestos.Text = Factura.PorcentajeImpuestos.Value
            txt_EnviarPor.Text = Factura.Enviar_Por.Value
            tbx_DireccionAlterna.Text = Factura.Direccion_Alterna.Value
            hyl_reporteDetalle.NavigateUrl = "~/Orbecatalog/Facturas/ReporteDetalleFactura.aspx?id_factura=" & id_Factura
            hyl_reportDetalle.NavigateUrl = "~/Orbecatalog/Facturas/ReporteDetalleFactura.aspx?id_factura=" & id_Factura

            lbl_Total.Text = simboloUnidad & Factura.Total.Value.ToString("#.##")
            lbl_subTotalInfo.Text = simboloUnidad & Factura.SubTotal.Value.ToString("#.##")
            lbl_totalFactura.Text = simboloUnidad & Factura.Total.Value.ToString("#.##")
            lbl_totalFactura.Visible = True
            lbl_totalFacturaTitulo.Visible = True

            If Factura.Estado_Factura.Value = FacturasHandler.EstadoFactura.Approved_Cart Then
                DisableActions()
                If Factura.Fecha_Cancelado.IsValid Then
                    bdp_FechaPago.SelectedDate = Factura.Fecha_Cancelado.ValueLocalized
                    bdp_FechaPago.Enabled = False
                    div_FechaPago.Visible = True
                Else
                    div_FechaPago.Visible = False
                End If
            Else
                div_FechaPago.Visible = False
            End If

            cmb_Entidad.SelectedValue = Factura.Id_Comprador.Value
            ddl_Unidad.SelectedValue = Factura.Id_Moneda.Value
            selectVendedor(Factura.id_Vendedor.Value)
            FacturaActual = id_Factura
        End If
    End Sub

    Protected Function updateFactura(ByVal control As FacturasHandler, ByVal id_factura As Integer) As Boolean
        Dim Factura As New Factura()
        Factura.Enviar_Por.Value = txt_EnviarPor.Text
        Factura.id_TipoFactura.Value = cmb_TipoFactura.SelectedItem.Value
        Factura.Direccion_Alterna.Value = tbx_DireccionAlterna.Text

        Factura.Fecha_Cancelado.ValueLocalized = bdp_FechaPago.SelectedDate
        Factura.Fecha_Factura.ValueLocalized = txt_FechaFactura.SelectedDate
        Factura.PorcentajeImpuestos.Value = txt_PorcentajeImpuestos.Text

        If txt_Comprobante.Text.Length > 0 Then
            Factura.Comprobante.Value = txt_Comprobante.Text
            Factura.Fecha_Comprobante.ValueUtc = Date.UtcNow
        End If

        Return control.ActualizarFactura(Factura, id_factura)
    End Function

    'TiposFacturas
    Private Sub selectTiposFacturas()
        Dim TiposFacturas As New TipoFactura
        Dim dataset As New Data.DataSet

        TiposFacturas.Fields.SelectAll()
        dataset = connection.executeSelect(queryBuilder.SelectQuery(TiposFacturas))

        If dataset.Tables.Count > 0 Then
            If dataset.Tables(0).Rows.Count > 0 Then
                cmb_TipoFactura.DataSource = dataset
                cmb_TipoFactura.DataTextField = TiposFacturas.Nombre.Name
                cmb_TipoFactura.DataValueField = TiposFacturas.Id_TipoFactura.Name
                cmb_TipoFactura.DataBind()
            End If
        End If
    End Sub

    'Entidades
    Private Sub selectEntidades(ByVal id_TipoEntidad As Integer)
        Dim Ent As New Entidad
        Dim dataset As Data.DataSet

        Ent.Id_entidad.ToSelect = True
        Ent.NombreDisplay.ToSelect = True
        If id_TipoEntidad <> 0 Then
            Ent.Id_tipoEntidad.Where.EqualCondition(id_TipoEntidad)
        End If

        dataset = connection.executeSelect(queryBuilder.SelectQuery(Ent))

        cmb_Entidad.DataSource = dataset
        cmb_Entidad.DataTextField = Ent.NombreDisplay.Name
        cmb_Entidad.DataValueField = Ent.Id_entidad.Name
        cmb_Entidad.DataBind()
    End Sub

    Private Sub selectVendedor(ByVal id_Entidad As Integer)
        Dim Entidad As New Entidad
        Dim dataset As Data.DataSet

        Entidad.Id_entidad.ToSelect = True
        Entidad.NombreDisplay.ToSelect = True
        If id_Entidad <> 0 Then
            Entidad.Id_entidad.Where.EqualCondition(id_Entidad)
        End If

        queryBuilder.From.Add(Entidad)
        dataset = connection.executeSelect(queryBuilder.RelationalSelectQuery())
        If dataset.Tables.Count > 0 Then
            If dataset.Tables(0).Rows.Count > 0 Then
                ObjectBuilder.CreateObject(dataset.Tables(0), 0, Entidad)
                lbl_Vendedor.Text = Entidad.NombreDisplay.Value
                lbl_Vendedor.ToolTip = Entidad.Id_entidad.Value
            End If
        End If
    End Sub

    'Detalle Factura
    Private Sub selectDetalleFactura(ByVal control As FacturasHandler, ByVal id_Factura As Integer)
        Dim Detalle As New DetalleFactura
        Dim resultTable As Data.DataTable = control.GetItems(id_Factura, Detalle)

        If resultTable.Rows.Count > 0 Then
            dg_DetalleFactura.DataKeyField = "detalle"
            dg_DetalleFactura.DataSource = resultTable
            dg_DetalleFactura.DataBind()

            For counter As Integer = 0 To dg_DetalleFactura.Items.Count - 1
                Dim act_Detalle As DetalleFactura = ObjectBuilder.CreateDBTable_BySelectIndex(resultTable.Rows(0), Detalle)

                Dim lbl_DetalleFactura As Label = dg_DetalleFactura.Items(counter).FindControl("lbl_DetalleFactura")
                Dim lbl_PrecioVenta As Label = dg_DetalleFactura.Items(counter).FindControl("lbl_PrecioVenta")
                Dim lbl_Cantidad As Label = dg_DetalleFactura.Items(counter).FindControl("lbl_Cantidad")
                Dim lbl_descuento As Label = dg_DetalleFactura.Items(counter).FindControl("lbl_descuento")
                Dim lbl_PrecioUnitario As Label = dg_DetalleFactura.Items(counter).FindControl("lbl_PrecioUnitario")
                Dim lbl_PrecioUnitarioExtra As Label = dg_DetalleFactura.Items(counter).FindControl("lbl_PrecioUnitarioExtra")

                lbl_DetalleFactura.Text = act_Detalle.NombreDisplay.Value
                lbl_Cantidad.Text = act_Detalle.Cantidad.Value
                lbl_descuento.Text = act_Detalle.Descuento.Value
                lbl_PrecioVenta.Text = act_Detalle.MontoVenta.Value.ToString("N")
                lbl_PrecioUnitarioExtra.Text = act_Detalle.Precio_Unitario_Extra.Value.ToString("N")
                lbl_PrecioUnitario.Text = act_Detalle.Precio_Unitario.Value.ToString("N")

                dg_DetalleFactura.Items(counter).Attributes.Add("onmouseover", "setRowFocus(this, " & counter & ", false)")
                dg_DetalleFactura.Items(counter).Attributes.Add("onmouseout", "unsetRowFocus(this, " & counter & ", false)")
            Next
            dg_DetalleFactura.Visible = True
        Else
            dg_DetalleFactura.Visible = False
        End If
    End Sub

    'Moneda
    Function selectUnidadSimbolo(ByVal Id_Moneda As Integer) As String
        Dim Unidad As New Moneda
        Unidad.Nombre.ToSelect = True
        Unidad.Simbolo.ToSelect = True
        Unidad.Id_Moneda.Where.EqualCondition(Id_Moneda)
        queryBuilder.From.Add(Unidad)
        Dim dataset As Data.DataSet = connection.executeSelect(queryBuilder.RelationalSelectQuery)

        If dataset.Tables.Count > 0 Then
            If dataset.Tables(0).Rows.Count > 0 Then
                ObjectBuilder.CreateObject(dataset.Tables(0), 0, Unidad)
                Return Unidad.Simbolo.Value
            End If
        End If
        Return Nothing
    End Function

    Protected Sub selectUnidades()
        Dim dataset As Data.DataSet
        Dim Moneda As New Moneda
        Moneda.Fields.SelectAll()
        queryBuilder.Orderby.Add(Moneda.Nombre)
        dataset = connection.executeSelect(queryBuilder.SelectQuery(Moneda))
        If dataset.Tables.Count > 0 Then
            If dataset.Tables(0).Rows.Count > 0 Then
                ddl_Unidad.DataSource = dataset
                ddl_Unidad.DataTextField = Moneda.Nombre.Name
                ddl_Unidad.DataValueField = Moneda.Id_Moneda.Name
                ddl_Unidad.DataBind()
            End If
        End If
    End Sub

    'Pagina
    Private Sub AgregaAtributosBotones()
        btn_Eliminar.Attributes.Add("onclick", "return confirm('Est� seguro de que desea eliminar esta factura?');")
        btn_Modificar.Attributes.Add("onclick", "return confirm('Est� seguro de que desea modificar la informaci�n de esta factura o Producto?');")
        btn_Cancelar.Attributes.Add("onclick", "return confirm('Esto limpiar� todos los campos. Continuar?');")
    End Sub

    Private Function VerificaCampos() As Boolean
        'If txt_PorcentajeImpuestos.Text <> "" And txt_FechaFactura.SelectedValue.ToString <> "" And txt_NumeroFactura.Text <> "" Then
        '    Return True
        'End If
        'Return False


        Return True
    End Function

    'Eventos
    Protected Sub btn_Salvar_Click(ByVal Sender As Object, ByVal E As System.EventArgs) Handles btn_Salvar.Click
        'Verifica que todos los campos est�n llenos
        If VerificaCampos() Then
            Dim controladora As New FacturasHandler(connection)
            Dim facturaIngresada As Integer = controladora.CreateFactura(cmb_Entidad.SelectedItem.Value, lbl_Vendedor.ToolTip, cmb_TipoFactura.SelectedItem.Value, ddl_Unidad.SelectedValue)
            If facturaIngresada > 0 Then
                FacturaActual = facturaIngresada
                ClearInfo(Configuration.EstadoPantalla.CONSULTAR, False)
                loadFactura(controladora, FacturaActual)
                Upd_pagina.Update()
            Else
                MyMaster.MostrarMensaje("Error insertar factura.", True)
            End If
        Else
            MyMaster.MostrarMensaje("Todos los campos deben estar llenos", True)
        End If
    End Sub

    Protected Sub btn_Modificar_Click(ByVal Sender As Object, ByVal E As System.EventArgs) Handles btn_Modificar.Click
        If VerificaCampos() Then
            Dim controladora As New FacturasHandler(connection)
            If updateFactura(controladora, FacturaActual) Then
                loadFactura(controladora, FacturaActual)
                upd_Factura.Update()
            End If
        End If
    End Sub

    Protected Sub btn_Cancelar_Click(ByVal Sender As Object, ByVal E As System.EventArgs) Handles btn_Cancelar.Click
        ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
        upd_DetalleFactura.Update()
        upd_Factura.Update()
    End Sub

    Protected Sub btn_Eliminar_Click(ByVal Sender As Object, ByVal E As System.EventArgs) Handles btn_Eliminar.Click
        Dim controladora As New FacturasHandler(connection)
        If controladora.EliminarFactura(FacturaActual) Then
            ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
            upd_DetalleFactura.Update()
            upd_Factura.Update()
        End If
    End Sub

    Protected Sub btn_MarcarCancelado_Click(ByVal Sender As Object, ByVal E As System.EventArgs) Handles btn_MarcarCancelado.Click
        Dim controladora As New FacturasHandler(connection)
        If controladora.ConsumirFactura(FacturaActual, securityHandler.Entidad) Then
            MyMaster.MostrarMensaje("Factura registrada como cancelada exitosamente", False)
            DisableActions()
        Else
            MyMaster.MostrarMensaje("Error al registrar la factura como cancelada", True)
        End If
    End Sub

    ''' <summary>
    ''' M�todo llamado ante cualquier evento del datagrid, en este caso, los botones de 
    ''' "Eliminar" y "selectr" pertenecientes a cada fila para eliminar un Producto de la factura
    ''' o selectr un Producto para realizar modificaciones.
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub DG_DetalleFactura_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dg_DetalleFactura.ItemCommand
        Dim detalle As Integer = dg_DetalleFactura.DataKeys(e.Item.ItemIndex)
        Dim controladora As New FacturasHandler(connection)

        'Elimina un Producto de una factura
        If e.CommandSource.Text = "Eliminar" Then
            If controladora.EliminaDetalleFactura(FacturaActual, detalle) Then
                selectDetalleFactura(controladora, FacturaActual)
                upd_DetalleFactura.Update()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Agrega la confirmaci�n de borrar al bot�n de "Eliminar" en el datagrid de
    ''' Productos en una factura.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Sub DG_DetalleFactura_ItemDataBound(ByVal sender As Object, ByVal e As DataGridItemEventArgs) Handles DG_DetalleFactura.ItemDataBound
        If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            Dim btn As LinkButton = e.Item.FindControl("lnkbtn_Delete")
            If Not btn Is Nothing Then
                btn.Attributes.Add("onclick", "return confirm('Est� seguro que desea eliminar este Producto de la factura?');")
            End If
        End If
    End Sub

End Class
