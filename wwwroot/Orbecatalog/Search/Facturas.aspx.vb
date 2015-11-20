Imports Orbelink.DBHandler
Imports Orbelink.Entity.Facturas
Imports Orbelink.Entity.Entidades
Imports Orbelink.Entity.Productos
Imports Orbelink.Entity.Currency

Partial Class orbecatalog_Search_Factura
    Inherits PageBaseClass

    Const codigo_pantalla As String = "FC-SR"
    Const level As Integer = 2

    'Pagina
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddScripts(level)

        securityHandler.VerifyPantalla(codigo_pantalla, level)

        If Not Page.IsPostBack Then
            tr_Producto.Visible = False

            selectEstadosFactura()
            selectEntidades()
            ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
            DesarmarQuery()
        End If
    End Sub

    Protected Sub ClearInfo(ByVal estado As Configuration.EstadoPantalla, ByVal clearInfo As Boolean)
        'Select Case estado
        '    Case NORMAL
        '    Case MODIFICAR
        'End Select
    End Sub

    Protected Sub DesarmarQuery()
        Dim id_Estado As Integer = 0
        Dim id_cliente As Integer = 0
        Dim fechaInicio As String = ""
        Dim fechaFinal As String = ""

        If Not Request.QueryString("search") Is Nothing Then

            If Not Request.QueryString("estadoFactura") Is Nothing Then
                id_Estado = Request.QueryString("estadoFactura")
                ddl_Filtro_Estado.SelectedValue = id_Estado
            End If

            If Not Request.QueryString("cliente") Is Nothing Then
                id_cliente = Request.QueryString("cliente")
                ddl_cliente.SelectedValue = id_cliente
            End If

            If Not Request.QueryString("fechaInicio") Is Nothing Then
                fechaInicio = Request.QueryString("fechaInicio")
                bdp_FechaInicio.SelectedValue = fechaInicio
            End If

            If Not Request.QueryString("fechaFinal") Is Nothing Then
                fechaFinal = Request.QueryString("fechaFinal")
                bdp_FechaFinal.SelectedValue = fechaFinal
            End If
  
            If Not Request.QueryString("pageSize") Is Nothing Then
                dg_Facturas.PageSize = Request.QueryString("pageSize")
                ddl_PageSize.SelectedValue = Request.QueryString("pageSize")
            End If

            If Not Request.QueryString("pageNumber") Is Nothing Then
                dg_Facturas.CurrentPageIndex = Request.QueryString("pageNumber")
            End If

            selectFacturas(fechaInicio, fechaFinal, id_Estado, id_cliente)
        End If
    End Sub

    Protected Function ArmarQueryString(ByVal pageNumber As Integer) As String
        Dim queryString As String = "Facturas.aspx?search=true"

        'If Not ddl_Filtro_Estado.SelectedIndex = ddl_Filtro_Estado.Items.Count - 1 Then
        queryString &= "&estadoFactura=" & ddl_Filtro_Estado.SelectedValue
        'End If

        If ddl_cliente.SelectedIndex <> ddl_cliente.Items.Count - 1 Then
            queryString &= "&cliente=" & ddl_cliente.SelectedValue
        End If

        If bdp_FechaInicio.SelectedDate <> "#12:00:00 AM#" Then
            queryString &= "&fechaInicio=" & bdp_FechaInicio.SelectedValue
        End If

        If bdp_FechaInicio.SelectedDate <> "#12:00:00 AM#" Then
            queryString &= "&fechaFinal=" & bdp_FechaFinal.SelectedDate
        End If

        If lnk_Producto.Attributes("id_Producto") IsNot Nothing Then
            queryString &= "&id_Producto=" & lnk_Producto.Attributes("id_Producto")
        End If

        queryString &= "&pageNumber=" & pageNumber
        queryString &= "&pageSize=" & ddl_PageSize.SelectedValue
        Return queryString
    End Function

    'Tipos Publicaciones
    Protected Sub selectEstadosFactura()
        ddl_Filtro_Estado.Items.Add("NA")
        ddl_Filtro_Estado.Items.Item(ddl_Filtro_Estado.Items.Count - 1).Value = 0
        ddl_Filtro_Estado.Items.Add("Unknown_Client")
        ddl_Filtro_Estado.Items.Item(ddl_Filtro_Estado.Items.Count - 1).Value = 1
        ddl_Filtro_Estado.Items.Add("Unapproved_Cart")
        ddl_Filtro_Estado.Items.Item(ddl_Filtro_Estado.Items.Count - 1).Value = 2
        ddl_Filtro_Estado.Items.Add("Approved_Cart")
        ddl_Filtro_Estado.Items.Item(ddl_Filtro_Estado.Items.Count - 1).Value = 3
        ddl_Filtro_Estado.Items.Add("Transaction_Successful")
        ddl_Filtro_Estado.Items.Item(ddl_Filtro_Estado.Items.Count - 1).Value = 4
        ddl_Filtro_Estado.Items.Add("Transaction_Unsuccessful")
        ddl_Filtro_Estado.Items.Item(ddl_Filtro_Estado.Items.Count - 1).Value = 5
        ddl_Filtro_Estado.Items.Add("-- Todos --")
        ddl_Filtro_Estado.Items.Item(ddl_Filtro_Estado.Items.Count - 1).Value = -1
        ddl_Filtro_Estado.SelectedIndex = ddl_Filtro_Estado.Items.Count - 1
    End Sub

    ''Facturas
    '''' <summary>
    '''' Carga las facturas al datagrid correspondiente.
    '''' </summary>
    '''' <remarks></remarks>
    'Private Sub CargaFacturas(ByVal fechaInicio As Date, ByVal fechaFin As Date, ByVal estado As Integer)
    '    Dim Factura As New Factura
    '    Dim DetalleFactura As New DetalleFactura
    '    Dim ds As New Data.DataSet
    '    Dim item As DataGridItem
    '    Dim i As Integer
    '    Dim totalfactura As Decimal

    '    Dim vlo_NumFact As New BoundColumn()
    '    vlo_NumFact.DataField = "Num_Factura"
    '    vlo_NumFact.HeaderText = "Numero de Factura"
    '    dg_Facturas.Columns.Add(vlo_NumFact)

    '    Factura.Num_Factura.ToSelect = True
    '    Factura.Id_Factura.ToSelect = True

    '    ds = connection.executeSelect(queryBuilder.SelectQuery(Factura))

    '    dg_Facturas.DataSource = ds
    '    dg_Facturas.DataBind()

    '    'Calcula el total de la factura sumando el precio de venta de cada uno de los
    '    'productos contenidos en ella.
    '    For Each item In dg_Facturas.Items
    '        DetalleFactura.Precio_Venta.ToSelect = True
    '        DetalleFactura.Id_Factura.Where.EqualCondition(dg_Facturas.DataKeys(item.ItemIndex))
    '        ds = connection.executeSelect(queryBuilder.SelectQuery(DetalleFactura))
    '        For i = 0 To ds.Tables(0).Rows.Count - 1
    '            totalfactura = totalfactura + ds.Tables(0).Rows(i).Item("Precio_Venta")
    '        Next
    '        CType(dg_Facturas.Items(item.ItemIndex).FindControl("lbl_TotalFactura"), Label).Text = totalfactura
    '        totalfactura = 0
    '    Next
    'End Sub

    Protected Sub selectFacturas(ByVal fechaInicio As String, ByVal fechaFin As String, ByVal id_estado As Integer, ByVal id_cliente As Integer)
        Dim dataSet As Data.DataSet
        Dim Factura As New Factura
        Dim unidad As New Moneda
        Dim entidad As New Entidad

        entidad.NombreDisplay.ToSelect = True

        Factura.Fields.SelectAll()
        Try

            'Busca si hay filtro
            If id_estado <> -1 Then
                Factura.Estado_Factura.Where.EqualCondition(id_estado)
            End If
            If id_cliente <> 0 Then
                Factura.Id_Comprador.Where.EqualCondition(id_cliente)
            End If
            If fechaInicio <> "" Then
                Factura.Fecha_Cancelado.Where.GreaterThanOrEqualCondition(fechaInicio)
            End If
            If fechaFin <> "" Then
                Factura.Fecha_Cancelado.Where.LessThanOrEqualCondition(fechaFin)
            End If

            queryBuilder.Distinct = True
            queryBuilder.Join.EqualCondition(entidad.Id_entidad, Factura.Id_Comprador)
            queryBuilder.Join.EqualCondition(unidad.Id_Moneda, Factura.Id_Moneda)
            queryBuilder.Orderby.Add(Factura.Fecha_Cancelado)

            queryBuilder.From.Add(Factura)
            queryBuilder.From.Add(entidad)
            queryBuilder.From.Add(unidad)
            dataSet = connection.executeSelect(queryBuilder.RelationalSelectQuery)

            Dim controladora As New Orbelink.Control.Facturas.FacturasHandler(connection)
            Dim estados As ArrayList = controladora.GetEstados

            If dataSet.Tables.Count > 0 Then
                If dataSet.Tables(0).Rows.Count > 0 Then
                    dg_Facturas.DataSource = dataSet
                    dg_Facturas.DataKeyField = Factura.Id_Factura.Name
                    dg_Facturas.DataBind()

                    'Llena el grid
                    Dim offset As Integer = dg_Facturas.CurrentPageIndex * dg_Facturas.PageSize
                    Dim result_Factura As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), Factura)
                    Dim results_Entidad As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), entidad)
                    Dim results_Unidades As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), unidad)

                    For counter As Integer = 0 To dg_Facturas.Items.Count - 1
                        Dim act_Factura As Factura = result_Factura(offset + counter)
                        Dim act_Entidad As Entidad = results_Entidad(offset + counter)
                        Dim act_Unidad As Moneda = results_Unidades(offset + counter)

                        Dim lbl_Fecha As Label = dg_Facturas.Items(counter).FindControl("lbl_Fecha")
                        Dim lnk_View As HyperLink = dg_Facturas.Items(counter).FindControl("lnk_View")
                        'Dim lnk_Publicacion As HyperLink = dg_Facturas.Items(counter).FindControl("lnk_Publicacion")
                        Dim lbl_FechaCancelado As Label = dg_Facturas.Items(counter).FindControl("lbl_FechaCancelado")
                        Dim lbl_Cliente As Label = dg_Facturas.Items(counter).FindControl("lbl_Cliente")
                        Dim lbl_Monto As Label = dg_Facturas.Items(counter).FindControl("lbl_Monto")
                        Dim lbl_Estado As Label = dg_Facturas.Items(counter).FindControl("lbl_Estado")

                        lnk_View.NavigateUrl = "~/orbecatalog/Facturas/Factura.aspx?id_Factura=" & act_Factura.Id_Factura.Value
                        lbl_Fecha.Text = act_Factura.Fecha_Factura.ValueLocalized
                        lbl_Cliente.Text = act_Entidad.NombreDisplay.Value

                        lbl_Monto.Text = act_Unidad.Simbolo.Value & act_Factura.Total.Value
                        lbl_Estado.Text = estados(act_Factura.Estado_Factura.Value)

                        'Javascript
                        dg_Facturas.Items(counter).Attributes.Add("onmouseover", "setRowFocus(this, " & counter & ", false)")
                        dg_Facturas.Items(counter).Attributes.Add("onmouseout", "unsetRowFocus(this, " & counter & ", false)")
                    Next
                    lbl_ResultadoElementos.Visible = False
                    dg_Facturas.Visible = True
                Else
                    lbl_ResultadoElementos.Visible = True
                    dg_Facturas.Visible = False
                End If
            End If
        Catch ex As Exception
            MyMaster.MostrarMensaje(ex.Message, True)
        End Try
    End Sub

    'Entidades
    Protected Sub selectEntidades()
        Dim dataSet As Data.DataSet
        Dim entidad As New Entidad

        Dim factura As New Factura
        Dim result As String
        entidad.Id_entidad.ToSelect = True
        entidad.NombreDisplay.ToSelect = True
        
        queryBuilder.Join.EqualCondition(factura.Id_Comprador, entidad.Id_entidad)
        queryBuilder.From.Add(entidad)
        queryBuilder.From.Add(factura)
        queryBuilder.Distinct = True
        result = queryBuilder.RelationalSelectQuery
        dataSet = connection.executeSelect(result)
        dataSet.Tables(0).Rows.Add(0, "Todos")
        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                ddl_cliente.DataSource = dataSet
                ddl_cliente.DataTextField = entidad.NombreDisplay.Name
                ddl_cliente.DataValueField = entidad.Id_entidad.Name
                ddl_cliente.DataBind()
            End If
        End If

        ddl_cliente.SelectedIndex = ddl_cliente.Items.Count - 1
        ddl_cliente.DataBind()
    End Sub

    'Eventos
    Protected Sub dg_Facturas_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dg_Facturas.EditCommand
        Dim indiceTabla As Integer = dg_Facturas.DataKeys(e.Item.ItemIndex)
        ClearInfo(Configuration.EstadoPantalla.CONSULTAR, True)
        Response.Redirect("../Publicacion/Publicacion.aspx?id_Publicacion=" & indiceTabla)
    End Sub

    Protected Sub btn_Buscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Buscar.Click
        Dim fi As Date = Format(Convert.ToDateTime(bdp_FechaInicio.SelectedDate))
        Dim ff As Date = Format(Convert.ToDateTime(bdp_FechaFinal.SelectedDate))

        If Not DateDiff(DateInterval.Day, fi, ff) < 0 Then
            Response.Redirect(ArmarQueryString(0))
        Else
            MyMaster.MostrarMensaje("La fecha de inicio debe ser menor a la fecha final", True)
        End If

    End Sub

    Protected Sub dg_Facturas_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dg_Facturas.PageIndexChanged
        dg_Facturas.CurrentPageIndex = e.NewPageIndex
        Response.Redirect(ArmarQueryString(e.NewPageIndex))
    End Sub

    ''' <summary>
    ''' Método que realiza un redirect a la misma página y le envía el id de la factura a consultar.
    ''' El PageLoad se encarga de decidir el comportamiento de la página.
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub dg_Facturas_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dg_Facturas.ItemCommand
        Dim Comando As LinkButton
        Comando = e.CommandSource
        If Comando.Text = "Editar" Then
            Response.Redirect("../Facturas/Factura.aspx?id_Factura=" & dg_Facturas.DataKeys(e.Item.ItemIndex))
        ElseIf Comando.Text = "Ver" Then
            'Algo
        End If
    End Sub

End Class

