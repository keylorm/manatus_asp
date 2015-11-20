Imports Orbelink.Entity.Entidades
Imports Orbelink.Entity.Productos
Imports Orbelink.Entity.Reservaciones
Imports Orbelink.Entity.Facturas
Imports Orbelink.DBHandler
Imports Orbelink.Control.Reservaciones
Imports Orbelink.Control.Facturas
Imports Orbelink.DateAndTime.DateHandler
Imports Orbelink.DateAndTime
Imports System.Data

Partial Class reservacion_sp
    Inherits Orbelink.FrontEnd6.PageBaseClass

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'Orbelink.DBHandler.LanguageHandler.CurrentLanguage = Orbelink.DBHandler.LanguageHandler.Language.INGLES
        parrafo1_checkout_esp.Visible = False
        parrafo2_checkout_esp.Visible = False
        header_menus_esp.Visible = False
        titulo_reservacion_h1_esp.Visible = False
        breadcrumbs_esp.Visible = False

        footer_esp.Visible = False
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Culture = "en-us"
            cambiaIdioma()
            GetTotales()
        End If
        If Session("id_reservacion") IsNot Nothing Then

            Dim control As New ControladorReservaciones(connection, Resources.Reservaciones_Resources.ResourceManager)
            Dim carrito As New FacturasHandler(connection)
            Dim factura As New Factura

            Try
                Dim id_reservacion As Integer = Session("id_reservacion")
                lbl_term.Text = id_reservacion
                Dim controladora As New ControladorReservaciones(connection, Resources.Reservaciones_Resources.ResourceManager)
                Dim reservacion As Reservacion = controladora.ConsultarReservacion(id_reservacion)
                If reservacion.Id_TipoEstado.Value = controladora.BuscaEstadoInicial() Then
                    CargarCarrito(id_reservacion)
                Else
                    esconderCarrito()
                End If

            Catch ex As Exception
                esconderCarrito()
            End Try

        Else

            esconderCarrito()

        End If

    End Sub

    Protected Sub btn_pagar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_pagar.Click
        If Session("id_reservacion") IsNot Nothing Then
            Dim control As New ControladorReservaciones(connection, Resources.Reservaciones_Resources.ResourceManager)
            Dim carrito As New FacturasHandler(connection)
            Dim factura As New Factura
            If radio_checkout.SelectedValue = "paypal" Then
                factura.id_TipoFactura.Value = FacturasHandler.Config_TipoFacturaDefault
                If carrito.ActualizarFactura(factura, control.obtenerFactura(Session("id_reservacion"))) Then
                    control.Emails(Session("id_reservacion"))
                    Response.Redirect("~/Paypal/paypal.aspx", False)
                End If
            ElseIf radio_checkout.SelectedValue = "bncr" Then
                factura.id_TipoFactura.Value = FacturasHandler.Config_TipoFacturaBNCR
                If carrito.ActualizarFactura(factura, control.obtenerFactura(Session("id_reservacion"))) Then
                    control.Emails(Session("id_reservacion"))
                    Response.Redirect("checkoutbncr.aspx")
                    'Response.Redirect("~/BNCR/IntermediaBNCR.aspx", False)
                End If
            End If

            
        End If
    End Sub

    Protected Sub esconderCarrito()
        pnl_principal.Visible = False
        lbl_mensaje.Visible = True

    End Sub

    Public Function CargarCarrito(ByVal id_reservacion As String) As Boolean

        Dim QueryBuilder As New QueryBuilder
        Dim ds As DataSet
        Dim reservacion As New Reservacion
        Dim factura As New Factura
        factura.Fields.SelectAll()
        Dim entidad As New Entidad()
        entidad.Id_entidad.ToSelect = True
        entidad.NombreDisplay.ToSelect = True
        entidad.Email.ToSelect = True
        entidad.Telefono.ToSelect = True
        reservacion.Fields.SelectAll()
        reservacion.Id_Reservacion.Where.EqualCondition(id_reservacion)
        QueryBuilder.Join.EqualCondition(entidad.Id_entidad, reservacion.id_Cliente)
        QueryBuilder.Join.EqualCondition(reservacion.Id_factura, factura.Id_Factura)
        QueryBuilder.From.Add(reservacion)
        QueryBuilder.From.Add(entidad)
        QueryBuilder.From.Add(factura)
        ds = connection.executeSelect(QueryBuilder.RelationalSelectQuery)

        If ds.Tables.Count > 0 Then
            If ds.Tables(0).Rows.Count > 0 Then
                ObjectBuilder.CreateObject(ds.Tables(0), 0, entidad)
                ObjectBuilder.CreateObject(ds.Tables(0), 0, reservacion)
                ObjectBuilder.CreateObject(ds.Tables(0), 0, factura)
                Try
                    asignaInformacion(reservacion, entidad, factura)

                Catch ex As Exception
                    Return False
                End Try
            Else
                esconderCarrito()
            End If
        End If

    End Function

    Public Sub asignaInformacion(ByVal reservacion As Reservacion, ByVal entidad As Entidad, ByVal factura As Factura)
        Dim QueryBuilder As New QueryBuilder
        Dim ds As DataSet
        Dim result As String
        Dim id_carrito As Integer = reservacion.Id_factura.Value

        Dim detalle_factura As New DetalleFactura
        detalle_factura.Precio_Unitario_Extra.ToSelect = True
        detalle_factura.Precio_Unitario.ToSelect = True
        detalle_factura.MontoVenta.ToSelect = True
        detalle_factura.Cantidad.ToSelect = True
        detalle_factura.NombreDisplay.ToSelect = True
        detalle_factura.Descuento.ToSelect = True
        detalle_factura.Id_Factura.Where.EqualCondition(reservacion.Id_factura.Value)

        QueryBuilder.From.Add(detalle_factura)

        result = QueryBuilder.RelationalSelectQuery()
        ds = connection.executeSelect(result)

        If ds.Tables.Count > 0 Then
            If ds.Tables(0).Rows.Count > 0 Then

                dtl_shoppingCart.DataSource = ds
                dtl_shoppingCart_responsivo.DataSource = ds
                dtl_shoppingCart.DataBind()
                dtl_shoppingCart_responsivo.DataBind()

                Dim fechaInicio As Date = reservacion.fecha_inicioProgramado.ValueLocalized
                Dim fechaFinal As Date = reservacion.fecha_finalProgramado.ValueLocalized
                Dim fecha As String = ""

                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    ObjectBuilder.CreateObject(ds.Tables(0), i, detalle_factura)

                    Dim lbl_producto As Label = dtl_shoppingCart.Items(i).FindControl("lbl_producto")
                    lbl_producto.Text = detalle_factura.NombreDisplay.Value

                    Dim lbl_producto_responsivo As Label = dtl_shoppingCart_responsivo.Items(i).FindControl("lbl_producto_responsivo")
                    lbl_producto_responsivo.Text = detalle_factura.NombreDisplay.Value

                    Dim lbl_cantidad As Label = dtl_shoppingCart.Items(i).FindControl("lbl_cantidad")
                    lbl_cantidad.Text = detalle_factura.Cantidad.Value

                    Dim lbl_cantidad_responsivo As Label = dtl_shoppingCart_responsivo.Items(i).FindControl("lbl_cantidad_responsivo")
                    lbl_cantidad_responsivo.Text = detalle_factura.Cantidad.Value

                    Dim lbl_montoventa As Label = dtl_shoppingCart.Items(i).FindControl("lbl_montoventa")
                    lbl_montoventa.Text = FormatCurrency(detalle_factura.Precio_Unitario.Value, 2)

                    Dim lbl_montoventa_responsivo As Label = dtl_shoppingCart_responsivo.Items(i).FindControl("lbl_montoventa_responsivo")
                    lbl_montoventa_responsivo.Text = FormatCurrency(detalle_factura.Precio_Unitario.Value, 2)

                    Dim lbl_extras As Label = dtl_shoppingCart.Items(i).FindControl("lbl_extras")
                    lbl_extras.Text = FormatCurrency(detalle_factura.Precio_Unitario_Extra.Value, 2)

                    Dim lbl_extras_responsivo As Label = dtl_shoppingCart_responsivo.Items(i).FindControl("lbl_extras_responsivo")
                    lbl_extras_responsivo.Text = FormatCurrency(detalle_factura.Precio_Unitario_Extra.Value, 2)

                    Dim lbl_descuento As Label = dtl_shoppingCart.Items(i).FindControl("lbl_desc")
                    lbl_descuento.Text = FormatCurrency(detalle_factura.Descuento.Value, 2)

                    Dim lbl_descuento_responsivo As Label = dtl_shoppingCart_responsivo.Items(i).FindControl("lbl_descuento_responsivo")
                    lbl_descuento_responsivo.Text = FormatCurrency(detalle_factura.Descuento.Value, 2)


                    Dim lbl_subTot As Label = dtl_shoppingCart.Items(i).FindControl("lbl_subTot")
                    lbl_subTot.Text = FormatCurrency(detalle_factura.MontoVenta.Value, 2)


                    Dim lbl_subTot_responsivo As Label = dtl_shoppingCart_responsivo.Items(i).FindControl("lbl_subTot_responsivo")
                    lbl_subTot_responsivo.Text = FormatCurrency(detalle_factura.MontoVenta.Value, 2)

                    'Dim lnk_eliminar As LinkButton = dtl_shoppingCart.Items(i).FindControl("lnk_eliminar")
                    'lnk_eliminar.Attributes.Add("reservacion", Request.QueryString("id_reservacion"))
                    'lnk_eliminar.Attributes.Add("id_producto", detalle_factura.Id_Producto.Value)
                    'lnk_eliminar.Attributes.Add("ordinal", detalle_factura.CampoLlave.Value)

                Next

                lbl_monto.Text = String.Format("{0:$###,###,###.##}", factura.Total.Value)
                Session("monto") = factura.Total.Value
                Session("subTotal") = factura.SubTotal.Value
                'Dim impuestos As Double = CDbl(factura.Total.Value) - CDbl(factura.SubTotal.Value)
                'lbl_taxesT.Text = String.Format("{0:$###,###,###.##}", impuestos)
            Else
                esconderCarrito()
            End If
        End If

    End Sub

    Protected Sub btn_pagar_arriba_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_pagar_arriba.Click
        If Session("id_reservacion") IsNot Nothing Then
            Dim control As New ControladorReservaciones(connection, Resources.Reservaciones_Resources.ResourceManager)
            Dim carrito As New FacturasHandler(connection)
            Dim factura As New Factura
            If radio_checkout.SelectedValue = "paypal" Then
                factura.id_TipoFactura.Value = FacturasHandler.Config_TipoFacturaDefault
                If carrito.ActualizarFactura(factura, control.obtenerFactura(Session("id_reservacion"))) Then
                    control.Emails(Session("id_reservacion"))
                    Response.Redirect("~/Paypal/paypal.aspx", False)
                End If
            ElseIf radio_checkout.SelectedValue = "bncr" Then
                factura.id_TipoFactura.Value = FacturasHandler.Config_TipoFacturaBNCR
                If carrito.ActualizarFactura(factura, control.obtenerFactura(Session("id_reservacion"))) Then
                    control.Emails(Session("id_reservacion"))
                    Response.Redirect("checkoutbncr.aspx")
                    'Response.Redirect("~/BNCR/IntermediaBNCR.aspx", False)
                End If
            End If
        End If
    End Sub
    Protected Sub btn_cancelar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_cancelar.Click
        If Session("id_reservacion") IsNot Nothing Then
            Dim controladoraReservaciones As New Orbelink.Control.Reservaciones.ControladorReservaciones(connection, Resources.Reservaciones_Resources.ResourceManager)
            controladoraReservaciones.NuevoEstadoParaReservacion(Session("id_reservacion"), ControladorReservaciones.Config_EstadoIncompleto, ControladorReservaciones.Config_VendedorDefault, "Cancelada en Paso 2", True)
            Response.Redirect("reservacion_en.aspx")
        End If
    End Sub
    Protected Sub btn_cancelar_arriba_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_cancelar_arriba.Click
        If Session("id_reservacion") IsNot Nothing Then
            Dim controladoraReservaciones As New Orbelink.Control.Reservaciones.ControladorReservaciones(connection, Resources.Reservaciones_Resources.ResourceManager)
            controladoraReservaciones.NuevoEstadoParaReservacion(Session("id_reservacion"), ControladorReservaciones.Config_EstadoIncompleto, ControladorReservaciones.Config_VendedorDefault, "Cancelada en Paso 2", True)
            Response.Redirect("reservacion_en.aspx")
        End If
    End Sub


    Protected Sub GetTotales()
        Dim monto_total As Double = Session("monto")
        Dim subtotal As Double = Session("subtotal")
        Dim taxes As Double = monto_total - subtotal
        If subtotal > 0 Then
            lbl_monto.Text = Format(CType(subtotal, Decimal), "$###.###.###")
        Else
            lbl_monto.Text = "$0.00"
        End If

        If taxes > 0 Then
            lbl_taxes.Text = Format(CType(taxes, Decimal), "$###.###.###")
        Else
            lbl_taxes.Text = "$0.00"
            'lbl_taxes.ForeColor = Drawing.Color.Red
        End If
        If monto_total > 0 Then
            lbl_Total.Text = Format(CType(monto_total, Decimal), "$###.###.###")
        Else
            lbl_Total.Text = "$0.00"
        End If
    End Sub

    Protected Function ArmarQueryString(ByVal id_carrito As Integer) As String
        Dim query As String = ""
        ' If id_carrito > 0 Then
        'query = "micuenta.aspx?return=Checkout.aspx?id_carrito=" & id_carrito
        'Else
        query = "micuenta.aspx?return=Checkout.aspx"
        'End If
        Return query
    End Function

    Protected Sub cambiaIdioma()
        'Orbelink.DBHandler.LanguageHandler.CurrentLanguage = Orbelink.DBHandler.LanguageHandler.Language.ESPANOL
        If Orbelink.DBHandler.LanguageHandler.CurrentLanguage = Orbelink.DBHandler.LanguageHandler.Language.ESPANOL Then
            header_menus_eng.Visible = False
            header_menus_esp.Visible = True
            titulo_reservacion_h1_eng.Visible = False
            titulo_reservacion_h1_esp.Visible = True
            breadcrumbs_eng.Visible = False
            breadcrumbs_esp.Visible = True
            footer_esp.Visible = True
            footer_eng.Visible = False
            titulo_pagina_checkout.Text = "DESCRIPCIÓN DEL PAGO"
            btn_cancelar_arriba.ImageUrl = "~/images/2014/img-btn-back-sp.png"
            btn_pagar_arriba.ImageUrl = "~/images/2014/completar_pago_esp.jpg"
            btn_cancelar.ImageUrl = "~/images/2014/img-btn-cancel-sp.png"
            btn_pagar.ImageUrl = "~/images/2014/completar_pago_esp.jpg"
            lbl_service.Text = "Servicio"
            lbl_montoventa.Text = "Monto de Venta"
            lbl_extras.Text = "Monto de Noches Adicionales"
            lbl_dias.Text = "Hab."
            lbl_descuento.Text = "Descuen."
            parrafo1_checkout.Visible = False
            parrafo2_checkout.Visible = False
            parrafo1_checkout_esp.Visible = True
            parrafo2_checkout_esp.Visible = True
            label_Taxes.InnerText = "Impuestos"
            radio_checkout.Items.Item(0).Text = "Pague con tarjeta de crédito utilizando:"
            nota_debajo.Text = "Este establecimiento está autorizado por Visa para realizar transacciones electrónicas."
            'lbl_nombre.Text = "Name:"
            'lbl_dir.Text = "Address:"
            'lbl_codigo.Text = "Postal Code:"
            'lbl_ubicacion.Text = "Country:"
            'lbl_codigoA.Text = "Code Area:"
            'lbl_telefono.Text = "Phone"
            'lbl_email.Text = "Email:"
            'lbl_fechaE.Text = "Check In:"
            'lbl_fechaS.Text = "Check Out:"

            'lbl_nHabitaciones.Text = "Rooms:"
            'calendarEntrada.CultureName = "en-us"
            'calendarSalida.CultureName = "en-us"
            'lbl_observaciones.Text = "Observations:"
            ''gv_ResultadosDisponibles.Columns(1).HeaderText = "Person(s)"

            'rfv_nombre.ErrorMessage = "Required Field"
            'rfv_codigoA.ErrorMessage = "Required Field"
            'rfv_email.ErrorMessage = "Required Field"
            'rfv_direccion.ErrorMessage = "Required Field"
            'rfv_fecha.ErrorMessage = "Required Field"
            'rfv_fecha2.ErrorMessage = "Required Field"
            'rfv_Tel.ErrorMessage = "Required Field"
            'rev_email.ErrorMessage = "Wrong Format"
            'cv_codigo.ErrorMessage = "Wrong Format"
            'cv_tel.ErrorMessage = "Wrong Format"

            'pnl_info1_ing.Visible = True
            'pnl_info2_ing.Visible = True
            'pnl_info3_ing.Visible = True

            'pnl_info1_esp.Visible = False
            'pnl_info2_esp.Visible = False
            'pnl_info3_esp.Visible = False
        Else
            'pnl_info1_ing.Visible = False
            'pnl_info2_ing.Visible = False
            'pnl_info3_ing.Visible = False

            'pnl_info1_esp.Visible = True
            'pnl_info2_esp.Visible = True
            'pnl_info3_esp.Visible = True

            'calendarEntrada.CultureName = "es-cr"
            'calendarSalida.CultureName = "es-cr"
        End If
    End Sub
    Protected Sub cargarPanelExito(ByVal exito As Integer)
        'If Request.QueryString("exito") IsNot Nothing Then
        '    'lbl_ResultadoReservacion.Visible = True
        '    pnl_contenido.Visible = False
        '    pnl_exito.Visible = True

        '    If Request.QueryString("exito") = 1 Then
        '        If Orbelink.DBHandler.LanguageHandler.CurrentLanguage = Orbelink.DBHandler.LanguageHandler.Language.INGLES Then
        '            lbl_exito.Text = "<br/><br/>Congratulations!!! The reservation has been recorded correctly.<br/><br/>"
        '        Else
        '            lbl_exito.Text = "<br/><br/>Felicidades!!!  La reservación ha sido registrada correctamente.<br/><br/>"
        '        End If
        '        lbl_exito.ForeColor = Drawing.Color.Black
        '        hyl_inicio.NavigateUrl = "home_sp.html"
        '    Else
        '        lbl_exito.ForeColor = Drawing.Color.Red
        '        If Request.QueryString("exito") = 0 Then
        '            If Orbelink.DBHandler.LanguageHandler.CurrentLanguage = Orbelink.DBHandler.LanguageHandler.Language.INGLES Then
        '                lbl_exito.Text = "<br/><br/>Sorry, your transacction was cancelled. Please click <a href='reservacion_en.aspx'>HERE</a> to try again.<br/><br/>"
        '            Else
        '                lbl_exito.Text = "<br/><br/>Lo sentimos, su transacción fue cancelada.  Por favor haga click <a href='reservacion_sp.aspx'>AQUI</a> para intentar de nuevo.<br/><br/>"
        '            End If
        '        End If
        '        hyl_inicio.NavigateUrl = "index.html"
        '    End If
        'Else

        'End If
    End Sub
End Class
