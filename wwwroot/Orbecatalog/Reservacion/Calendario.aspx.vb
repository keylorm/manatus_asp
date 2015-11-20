Imports Orbelink.DBHandler
Imports Orbelink.Entity.Reservaciones
Imports Orbelink.Entity.Productos
Imports Orbelink.Control.Reservaciones
Imports Orbelink.DateAndTime

Partial Class Orbecatalog_Reservacion_Calendario
    Inherits PageBaseClass

    Const codigo_pantalla As String = "Rs-06"
    Const level As Integer = 2

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'AddScripts(level)
        securityHandler.VerifyPantalla(codigo_pantalla, level)

        'If Not IsPostBack Then
        'Me.Title = Resources.Reservaciones_Resources.Reservacion

        'securityHandler.VerifyActions(btn_Salvar, btn_Modificar, btn_Eliminar)

        'If Request.QueryString("id_reservacion") IsNot Nothing Then
        '    ReservacionActual = Request.QueryString("id_reservacion")
        'End If

        'LoadAllInfo()
        'End If
    End Sub

    Protected Sub Orbecatalog_Reservacion_Calendario_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddScripts(level)

        If Not IsPostBack Then
            cargarProductos()
            llenarano()
            LLenarCalendario(Date.UtcNow.Year, Date.UtcNow.Month)
            ddl_meses.SelectedValue = Date.UtcNow.Month
            ddl_ano.SelectedValue = Date.UtcNow.Year
        End If
    End Sub

    Protected Sub cargarProductos()

        Dim Producto As New Producto
        Dim item As New Item
        Producto.Nombre.ToSelect = True
        Producto.Id_Producto.ToSelect = True
        Producto.Activo.Where.EqualCondition(1)
        queryBuilder.Join.EqualCondition(item.Id_producto, Producto.Id_Producto)
        queryBuilder.From.Add(Producto)
        queryBuilder.From.Add(item)
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

    Protected Sub LLenarCalendario(ByVal ano As Integer, Optional ByVal elMes As Integer = -1)
        Dim fechaLocalizada As Date
        If elMes > -1 And ano > -1 Then
            fechaLocalizada = New Date(ano, elMes, 1)
        End If

        'Crea items
        Dim cantidad As Integer = cantidadItems()
        Dim itemsTable As New Data.DataTable("items")
        itemsTable.Columns.Add("item")
        For i As Integer = 1 To cantidad
            itemsTable.Rows.Add(i)
        Next
        Dim dias As Integer = Date.DaysInMonth(fechaLocalizada.Year, fechaLocalizada.Month)

        'Dias
        Dim diasTable As New Data.DataTable("dias")
        diasTable.Columns.Add("dia")
        For i As Integer = 1 To dias
            diasTable.Rows.Add(i)
        Next

        diasTable.Rows.Add("Descripcion")

        DL_calendario.DataSource = diasTable
        DL_calendario.DataBind()

        For counter As Integer = 0 To DL_calendario.Items.Count - 1
            Dim p_fecha As Panel = DL_calendario.Items(counter).FindControl("fecha")
            Dim p_dia As Panel = DL_calendario.Items(counter).FindControl("dia")
            Dim lbl_fecha As Label = DL_calendario.Items(counter).FindControl("lbl_fecha")
            Dim lbl_dia As Label = DL_calendario.Items(counter).FindControl("lbl_dia")
            Dim dl_cantItem As DataList = DL_calendario.Items(counter).FindControl("dl_cantItem")

            If counter < DL_calendario.Items.Count - 1 Then
                Dim fechaActual As Date = New Date(fechaLocalizada.Year, fechaLocalizada.Month, counter + 1)

                lbl_fecha.Text = counter + 1
                lbl_fecha.CssClass = "letraMorada"

                If fechaActual.DayOfWeek = 0 Then 'Borde para dividir la semana
                    p_fecha.CssClass = "bordeIzqGris"
                    p_dia.CssClass = "bordeIzqGris"
                End If
                lbl_dia.Text = Inicialdia(fechaActual.DayOfWeek)

                dl_cantItem.DataSource = itemsTable
                dl_cantItem.DataBind()
                buscarReservaciones(fechaActual.ToUniversalTime, dl_cantItem)
            Else
                lbl_fecha.Text = "."
                lbl_fecha.CssClass = ""
                lbl_fecha.Style.Item("color") = "White"
                lbl_dia.Text = "uu"
                lbl_dia.Style.Item("color") = "White"
                lbl_fecha.Style.Item("padding-left") = "20px"
                lbl_fecha.Style.Item("color") = "Black"
                obtenerDescripcion(dl_cantItem)
            End If
        Next
    End Sub

    Protected Sub buscarReservaciones(ByVal fechaUTC As Date, ByVal ddl_cantItem As DataList)
        Dim reservacion As New Reservacion
        Dim detalle As New Detalle_Reservacion
        Dim query As New QueryBuilder()
        Dim tipoEstado As New TipoEstadoReservacion
        Dim fechaInicio As New Date(fechaUTC.Year, fechaUTC.Month, fechaUTC.Day, ControladorReservaciones.Config_HoraEntrada.Hour, ControladorReservaciones.Config_HoraEntrada.Minute, 0)
        Dim fechaFinal As New Date(fechaUTC.Year, fechaUTC.Month, fechaUTC.Day, ControladorReservaciones.Config_HoraSalida.Hour, ControladorReservaciones.Config_HoraSalida.Minute, 0)
        reservacion.fecha_inicioProgramado.Where.LessThanOrEqualCondition(fechaInicio.ToUniversalTime)
        reservacion.fecha_finalProgramado.Where.GreaterThanCondition(fechaFinal.ToUniversalTime)
        reservacion.Id_Reservacion.ToSelect = True

        detalle.ordinal.ToSelect = True
        If ddl_productos.SelectedValue.Length > 0 Then
            detalle.Id_producto.Where.EqualCondition(ddl_productos.SelectedValue)
        End If


        tipoEstado.RepresentaReservado.Where.EqualCondition(1)

        query.Join.EqualCondition(reservacion.Id_TipoEstado, tipoEstado.Id_TipoEstadoReservacion)
        query.Join.EqualCondition(detalle.id_reservacion, reservacion.Id_Reservacion)

        query.From.Add(reservacion)
        query.From.Add(detalle)
        query.From.Add(tipoEstado)

        Dim consulta As String = query.RelationalSelectQuery

        Dim dataTable As Data.DataTable = connection.executeSelect_DT(consulta)
        If dataTable.Rows.Count > 0 Then
            'Dim items As ArrayList = ObjectBuilder.TransformDataTable(dataTable, New Detalle_Reservacion)
            Dim reservas As ArrayList = ObjectBuilder.TransformDataTable(dataTable, New Reservacion)
            Dim detalles As ArrayList = ObjectBuilder.TransformDataTable(dataTable, New Detalle_Reservacion)
            For counter As Integer = 0 To reservas.Count - 1
                'Dim detalle As Detalle_Reservacion = items(counter)
                Dim reserva As Reservacion = reservas(counter)
                Dim elDetalle As Detalle_Reservacion = detalles(counter)
                Dim panel As Panel = ddl_cantItem.Items(elDetalle.ordinal.Value).FindControl("diaItem")
                Dim hyl_detalle As HyperLink = ddl_cantItem.Items(elDetalle.ordinal.Value).FindControl("hyl_detalle")

                panel.BackColor = System.Drawing.Color.FromArgb(247, 147, 30)
                hyl_detalle.Text = "2"
                hyl_detalle.NavigateUrl = "Reservacion.aspx?id_reservacion=" & reserva.Id_Reservacion.Value
            Next
        End If
    End Sub

    Protected Sub llenarano()
        ddl_ano.Items.Clear()
        Dim ano As Integer = Date.Now.Year - 1
        For counter As Integer = 0 To 5
            Dim itema As New ListItem(ano, ano)
            ddl_ano.Items.Add(itema)
            ano += 1
        Next
    End Sub

    Protected Sub obtenerDescripcion(ByVal ddl_cantItem As DataList)
        Dim item As New Item
        Dim dataSet As New Data.DataSet
        item.ordinal.ToSelect = True
        item.descripcion.ToSelect = True
        item.Id_producto.Where.EqualCondition(ddl_productos.SelectedValue)
        Dim consulta As String = queryBuilder.SelectQuery(item)
        dataSet = connection.executeSelect(consulta)

        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                ddl_cantItem.DataSource = dataSet
                ddl_cantItem.DataBind()
                For counter As Integer = 0 To dataSet.Tables(0).Rows.Count - 1
                    Dim panel As Panel = ddl_cantItem.Items(counter).FindControl("diaItem")
                    Dim hyl_detalle As HyperLink = ddl_cantItem.Items(counter).FindControl("hyl_detalle")
                    panel.Style.Item("background-color") = "White"
                    panel.Style.Item("Text-align") = "Left"
                    panel.Style.Item("padding-left") = "10px"
                    panel.Width = 300
                    hyl_detalle.Style.Item("color") = "black"
                    hyl_detalle.Style.Item("Text-align") = "Left"

                    hyl_detalle.Style.Item("text-decoration") = "none"
                    Dim descripcion As String = ""
                    Try
                        descripcion = dataSet.Tables(0).Rows(counter).Item(1)
                    Catch ex As Exception
                        descripcion = ""
                    End Try
                    If descripcion.Length > 30 Then
                        hyl_detalle.Text = descripcion.Substring(0, 29)
                    Else
                        hyl_detalle.Text = descripcion
                    End If
                Next
            End If
        End If
    End Sub

    Protected Function cantidadItems() As Integer
        Dim item As New Item

        item.ordinal.ToSelect = True
        If ddl_productos.SelectedValue.Length > 0 Then
            item.Id_producto.Where.EqualCondition(ddl_productos.SelectedValue)
        End If

        'queryBuilder.Orderby.Add(item.ordinal, True)
        Dim consulta As String = queryBuilder.SelectQuery(item)
        Dim dataTable As Data.DataTable = connection.executeSelect_DT(consulta)
        If dataTable.Rows.Count > 0 Then
            lbl_vacio.Visible = False
            Return dataTable.Rows.Count
        End If
        lbl_vacio.Visible = True
        Return 0
    End Function

    Protected Function Inicialdia(ByVal fecha As Integer) As String
        Dim dia As String = "D"
        Select Case fecha
            Case 0
                dia = "D"
            Case 1
                dia = "L"
            Case 2
                dia = "K"
            Case 3
                dia = "M"
            Case 4
                dia = "J"
            Case 5
                dia = "V"
            Case 6
                dia = "S"
        End Select

        Return dia
    End Function

    Protected Sub ddl_productos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddl_productos.SelectedIndexChanged
        ' Dim fechaLocalizada As Date = DateHandler.ToLocalizedDateFromUtc(Date.UtcNow)
        LLenarCalendario(ddl_ano.SelectedValue, ddl_meses.SelectedValue)
    End Sub

    Protected Sub ddl_meses_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddl_meses.SelectedIndexChanged

        LLenarCalendario(ddl_ano.SelectedValue, ddl_meses.SelectedValue)
    End Sub

    Protected Sub ddl_ano_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddl_ano.SelectedIndexChanged
        LLenarCalendario(ddl_ano.SelectedValue, ddl_meses.SelectedValue)
    End Sub
End Class
