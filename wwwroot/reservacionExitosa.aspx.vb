Imports Orbelink.DBHandler
Imports Orbelink.Entity.Reservaciones
Imports Orbelink.Entity.Productos
Imports Orbelink.Control.Facturas
Imports Orbelink.Control.Reservaciones
Imports Orbelink.Entity.Facturas
Imports Orbelink.Entity.Entidades
Imports System.Data
Partial Class reservacionExitosa
    Inherits Orbelink.FrontEnd6.PageBaseClass

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        'If Session("id_reservacion") IsNot Nothing Then
        '    Dim id_reservacion As Integer = Session("id_reservacion")
        '    numero_orden.InnerText = id_reservacion
        '    CargarCarrito(numero_orden.InnerText)
        'End If

        Dim id_reservacion As Integer = 10959
        numero_orden.InnerText = id_reservacion
        CargarCarrito(numero_orden.InnerText)


        'Dim id_reservacion As Integer = 8461
        'numero_orden.InnerText = id_reservacion
        'CargarCarrito(numero_orden.InnerText)


        'If Not IsPostBack Then
        '    If Orbelink.DBHandler.LanguageHandler.CurrentLanguage = Orbelink.DBHandler.LanguageHandler.Language.INGLES Then
        '        lbl_exito.Text = "<br/><br/>Congratulations!!! The reservation has been recorded correctly.<br/><br/>"
        '    Else
        '        lbl_exito.Text = "<br/><br/>Felicidades!!!  La reservación ha sido registrada correctamente.<br/><br/>"
        '    End If
        '    lbl_exito.ForeColor = Drawing.Color.Black
        '    hyl_inicio.NavigateUrl = "home_sp.html"
        'End If
    End Sub

    Public Function CargarCarrito(ByVal id_reservacion As String) As Boolean

        Dim QueryBuilder As New QueryBuilder
        Dim ds As DataSet
        Dim reservacion As New Reservacion
        Dim factura As New Factura
        Dim ubicacion As New Ubicacion
        Dim entidad As New Entidad()
        Dim detalle_factura As New DetalleFactura

        'selects
        factura.Id_Factura.ToSelect = True
        factura.SubTotal.ToSelect = True
        factura.PorcentajeImpuestos.ToSelect = True
        factura.Total.ToSelect = True
        'entidad.Id_entidad.ToSelect = True
        'entidad.NombreDisplay.ToSelect = True
        'entidad.Email.ToSelect = True
        'entidad.Telefono.ToSelect = True
        'entidad.Descripcion.ToSelect = True
        'entidad.Id_Ubicacion.ToSelect = True
        entidad.Fields.SelectAll()
        ubicacion.Nombre.ToSelect = True
        ubicacion.Id_ubicacion.ToSelect = True
        reservacion.fecha_inicioProgramado.ToSelect = True
        reservacion.fecha_finalProgramado.ToSelect = True
        reservacion.Id_factura.ToSelect = True
        reservacion.id_Cliente.ToSelect = True
        reservacion.Id_Reservacion.ToSelect = True
        reservacion.descripcion.ToSelect = True

        'where
        reservacion.Id_Reservacion.Where.EqualCondition(id_reservacion)

        'joins
        QueryBuilder.Join.EqualCondition(entidad.Id_entidad, reservacion.id_Cliente)
        QueryBuilder.Join.EqualCondition(reservacion.Id_factura, factura.Id_Factura)
        QueryBuilder.Join.EqualCondition(ubicacion.Id_ubicacion, entidad.Id_Ubicacion)

        'froms
        QueryBuilder.From.Add(reservacion)
        QueryBuilder.From.Add(ubicacion)
        QueryBuilder.From.Add(entidad)
        QueryBuilder.From.Add(factura)

        'execute
        ds = connection.executeSelect(QueryBuilder.RelationalSelectQuery)

        If ds.Tables.Count > 0 Then
            If ds.Tables(0).Rows.Count > 0 Then
                ObjectBuilder.CreateObject(ds.Tables(0), 0, entidad)
                ObjectBuilder.CreateObject(ds.Tables(0), 0, reservacion)
                ObjectBuilder.CreateObject(ds.Tables(0), 0, factura)
                ObjectBuilder.CreateObject(ds.Tables(0), 0, ubicacion)

                Try
                    asignaInformacion(reservacion, entidad, factura)

                Catch ex As Exception
                    Return False
                End Try
                

                Dim fechaInicio As Date = FormatDateTime(reservacion.fecha_inicioProgramado.ValueLocalized, DateFormat.ShortDate)
                Dim fechaFinal As Date = FormatDateTime(reservacion.fecha_finalProgramado.ValueLocalized, DateFormat.ShortDate)
                Dim fecha As String = ""

                lbl_nombre_Cliente.Text = entidad.NombreDisplay.Value
                lbl_Direccion.Text = entidad.Descripcion.Value
                lbl_Pais.Text = ubicacion.Nombre.Value
                lbl_Telefono.Text = entidad.Telefono.Value
                lbl_email.Text = entidad.Email.Value
                lbl_check_in.Text = "Arrival Date: " + fechaInicio
                lbl_check_out.Text = "Departure Date: " + fechaFinal
                lbl_details.Text = reservacion.descripcion.Value
                lbl_monto.Text = String.Format("{0:$###,###,###.##}", factura.SubTotal.Value)
                Dim impuestos As Double = CDbl(factura.Total.Value) - CDbl(factura.SubTotal.Value)
                If impuestos = 0 Then
                    lbl_taxes.Text = "$0.00"
                Else
                    lbl_taxes.Text = String.Format("{0:$###,###,###.##}", impuestos)
                End If
                lbl_Total.Text = String.Format("{0:$###,###,###.##}", factura.Total.Value)

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
        detalle_factura.NombreDisplay.ToSelect = True
        detalle_factura.Id_Factura.Where.EqualCondition(reservacion.Id_factura.Value)

        QueryBuilder.From.Add(detalle_factura)

        result = QueryBuilder.RelationalSelectQuery()
        ds = connection.executeSelect(result)

        If ds.Tables.Count > 0 Then
            If ds.Tables(0).Rows.Count > 0 Then

                dtl_shoppingCart.DataSource = ds
                dtl_shoppingCart.DataBind()

                Dim fechaInicio As Date = reservacion.fecha_inicioProgramado.ValueLocalized
                Dim fechaFinal As Date = reservacion.fecha_finalProgramado.ValueLocalized
                Dim fecha As String = ""

                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    ObjectBuilder.CreateObject(ds.Tables(0), i, detalle_factura)

                    Dim lbl_producto As Label = dtl_shoppingCart.Items(i).FindControl("lbl_producto")
                    lbl_producto.Text = detalle_factura.NombreDisplay.Value


                Next

            Else
                esconderCarrito()
            End If
        End If

    End Sub

    Protected Sub esconderCarrito()
        'pnl_principal.Visible = False
        'lbl_mensaje.Visible = True

    End Sub

End Class
