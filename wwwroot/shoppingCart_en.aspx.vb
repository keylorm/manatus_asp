Imports Orbelink.DBHandler
Imports Orbelink.Entity.Reservaciones
Imports Orbelink.Entity.Productos
Imports Orbelink.Control.Facturas
Imports Orbelink.Control.Reservaciones
Imports Orbelink.Entity.Facturas
Imports Orbelink.Entity.Entidades
Imports System.Data

Partial Class shoppingCar_en
    Inherits Orbelink.FrontEnd6.PageBaseClass

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Orbelink.DBHandler.LanguageHandler.CurrentLanguage = Orbelink.DBHandler.LanguageHandler.Language.INGLES
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim control As New ControladorReservaciones(connection, Resources.Reservaciones_Resources.ResourceManager)
        Dim carrito As New FacturasHandler(connection)
        Dim factura As New Factura
        If carrito.ActualizarFactura(factura, control.obtenerFactura(Session("id_reservacion"))) Then
            control.Emails(Session("id_reservacion"), True)
        End If

        cambiaIdioma()
        Culture = "en-us"
        If Not IsPostBack Then
            If Request.QueryString("id_reservacion") IsNot Nothing Then
                Try
                    Dim id_reservacion As Integer = Request.QueryString("id_reservacion")
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
        End If

        'If Request.QueryString("termino") IsNot Nothing Then
        '    Dim termino As String = (Request.QueryString("termino"))
        '    If termino = "1" Then
        '        btn_pagar.Visible = True
        '        lnkbtn_terminos.Visible = False
        '        lnkbtn_terminos2.Visible = False
        '        lbl_termino1.Visible = False
        '        lbl_termino2.Visible = False
        '        lbl_termino3.Visible = False
        '    Else
        '        btn_pagar.Visible = False
        '    End If

        'End If


    End Sub
    Protected Sub esconderCarrito()
        pnl_principal.Visible = False
        lbl_mensaje.Visible = True

    End Sub
    Protected Sub cambiaIdioma()
        Orbelink.DBHandler.LanguageHandler.CurrentLanguage = Orbelink.DBHandler.LanguageHandler.Language.INGLES
        If Orbelink.DBHandler.LanguageHandler.CurrentLanguage = Orbelink.DBHandler.LanguageHandler.Language.INGLES Then
            lbl_service.Text = "Service"
            lbl_montoventa.Text = "Service Amount"
            lbl_extras.Text = "Extra Night(s) Amount"
            lbl_dias.Text = "Rooms"
            'lbl_taxes.Text = "Impuestos:"
            lbl_MontoTot.Text = "Total:"

            lbl_MontoTot.Text = "Total: "
            lnkbtn_terminos.Text = "here"
            lnkbtn_terminos2.Text = "Terms and Conditiones"
            lbl_termino1.Text = "Click"
            lbl_termino2.Text = "to read and confirm our "
            lbl_termino3.Text = "to continue"
            lbl_mensaje.Text = "  <br />  <br />  <br /> Your Shopping Cart is Empty <br />  <br />  <br />"
        End If
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
                lbl_valor_total_responsivo.Text = String.Format("{0:$###,###,###.##}", factura.Total.Value)
                Session("monto") = factura.Total.Value
                Session("subTotal") = factura.SubTotal.Value
                Dim impuestos As Double = CDbl(factura.Total.Value) - CDbl(factura.SubTotal.Value)
                lbl_taxesT.Text = String.Format("{0:$###,###,###.##}", impuestos)
            Else
                esconderCarrito()
            End If
        End If

    End Sub

    Protected Sub lnk_eliminar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim lnk_eliminar As LinkButton = sender
        Dim controladora As New ControladorReservaciones(connection, Resources.Reservaciones_Resources.ResourceManager)
        controladora.EliminarDetalle(lnk_eliminar.Attributes("reservacion"), lnk_eliminar.Attributes("id_producto"), lnk_eliminar.Attributes("ordinal"), 1, "carritoEliminado")
        CargarCarrito(Request.QueryString("id_reservacion"))
    End Sub

    Protected Sub btn_pagar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_pagar.Click
        Orbelink.DBHandler.LanguageHandler.CurrentLanguage = Orbelink.DBHandler.LanguageHandler.Language.INGLES
        Response.Redirect("checkout.aspx", False)
        'Response.Redirect("Paypal/paypal.aspx", False)
    End Sub

    Protected Sub btn_cancelar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_cancelar.Click
        Dim controladoraReservaciones As New Orbelink.Control.Reservaciones.ControladorReservaciones(connection, Resources.Reservaciones_Resources.ResourceManager)
        controladoraReservaciones.NuevoEstadoParaReservacion(lbl_term.Text, ControladorReservaciones.Config_EstadoIncompleto, ControladorReservaciones.Config_VendedorDefault, "Cancelada en Paso 1", True)
        Response.Redirect("Reservacion_en.aspx")
    End Sub

    Protected Sub btn_pagar_arriba_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_pagar_arriba.Click
        Orbelink.DBHandler.LanguageHandler.CurrentLanguage = Orbelink.DBHandler.LanguageHandler.Language.INGLES
        Response.Redirect("checkout.aspx", False)
        'Response.Redirect("Paypal/paypal.aspx", False)
    End Sub

    Protected Sub btn_cancelar_arriba_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_cancelar_arriba.Click
        Dim controladoraReservaciones As New Orbelink.Control.Reservaciones.ControladorReservaciones(connection, Resources.Reservaciones_Resources.ResourceManager)
        controladoraReservaciones.NuevoEstadoParaReservacion(lbl_term.Text, ControladorReservaciones.Config_EstadoIncompleto, ControladorReservaciones.Config_VendedorDefault, "Cancelada en Paso 1", True)
        Response.Redirect("Reservacion_en.aspx")
    End Sub
End Class
