Imports Orbelink.DBHandler
Imports Orbelink.Entity.Entidades
Imports Orbelink.Entity.Facturas
Imports Orbelink.Entity.Productos
Imports Orbelink.Control

Partial Class Orbecatalog_Facturas_ReporteDetalleFactura
    Inherits PageBaseClass

    Const codigo_pantalla As String = "FC-RD"
    Const level As Integer = 2

    Protected Sub Page_Load1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddScripts(level)
        If Not IsPostBack Then
            securityHandler.VerifyPantalla(codigo_pantalla, level)
            If Not Request.Params("id_factura") Is Nothing Then
                id_Actual = Request.Params("id_factura")
                generarReporte(id_Actual)
            End If
        End If
    End Sub

    Protected Sub generarReporte(ByVal id_factura As Integer)
        Dim dataTableFactura As Data.DataTable = infoFactura(id_factura)
        Dim dataTableDetalle As Data.DataTable = infoDetalle(id_factura)

        RW_factura.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local
        Dim rep As Microsoft.Reporting.WebForms.LocalReport = RW_factura.LocalReport
        rep.ReportPath = "Orbecatalog\Facturas\detalleFactura.rdlc"
        rep.DataSources.Clear()
        Dim dataFactura As New Microsoft.Reporting.WebForms.ReportDataSource()
        dataFactura.Name = "detalleFacturaRe_DataTable3"
        dataFactura.Value = dataTableFactura
        rep.DataSources.Add(dataFactura)
        Dim dataDetalle As New Microsoft.Reporting.WebForms.ReportDataSource()
        dataDetalle.Name = "detalleFacturaRe_DataTable4"
        dataDetalle.Value = dataTableDetalle
        rep.DataSources.Add(dataDetalle)
    End Sub

    Protected Function infoFactura(ByVal id_factura As Integer) As Data.DataTable
        Dim factura As New Factura
        Dim vendedor As New Entidad
        Dim cliente As New Entidad
        cliente.AsName = "Cliente"
        Dim dataTable As New Data.DataTable
        Try
            vendedor.NombreDisplay.ToSelect = True
            vendedor.NombreDisplay.AsName = "N_Vendedor"
            cliente.NombreDisplay.ToSelect = True
            cliente.NombreDisplay.AsName = "N_Cliente"
            factura.Fecha_Factura.ToSelect = True
            factura.Total.ToSelect = True
            factura.Estado_Factura.ToSelect = True

            queryBuilder.Join.EqualCondition(factura.id_Vendedor, vendedor.Id_entidad)
            queryBuilder.Join.EqualCondition(factura.Id_Comprador, cliente.Id_entidad)
            factura.Id_Factura.Where.EqualCondition(id_factura)

            queryBuilder.From.Add(vendedor)
            queryBuilder.From.Add(cliente)
            queryBuilder.From.Add(factura)
            queryBuilder.Orderby.Add(factura.Estado_Factura)
            queryBuilder.Orderby.Add(factura.Fecha_Factura)
            Dim consulta As String = queryBuilder.RelationalSelectQuery
            dataTable = connection.executeSelect_DT(consulta)
            dataTable = cambiarEstadoString(dataTable)
            Return dataTable
        Catch ex As Exception
            Return New Data.DataTable
        End Try
    End Function

    Protected Function infoDetalle(ByVal id_factura As Integer) As Data.DataTable
        Try
            Dim factura As New Factura
            Dim detalle As New DetalleFactura
            'Dim producto As New Producto
            Dim dataTable As New Data.DataTable
            'producto.Nombre.ToSelect = True
            'producto.Nombre.AsName = "N_Producto"
            detalle.Cantidad.ToSelect = True
            detalle.Precio_Unitario.ToSelect = True
            detalle.Precio_Unitario_Extra.ToSelect = True
            detalle.Descuento.ToSelect = True
            detalle.MontoVenta.ToSelect = True
            detalle.NombreDisplay.ToSelect = True
            detalle.NombreDisplay.AsName = "N_Producto"
            factura.Id_Factura.Where.EqualCondition(id_factura)
            queryBuilder.Join.EqualCondition(factura.Id_Factura, detalle.Id_Factura)
            ' queryBuilder.Join.EqualCondition(detalle.Id_Producto, producto.Id_Producto)
            queryBuilder.From.Add(detalle)
            'queryBuilder.From.Add(producto)
            queryBuilder.From.Add(factura)
            Dim consulta As String = queryBuilder.RelationalSelectQuery
            dataTable = connection.executeSelect_DT(consulta)
            Return dataTable
        Catch ex As Exception
            Return New Data.DataTable
        End Try
    End Function

    Protected Function cambiarEstadoString(ByVal dataTable As Data.DataTable) As Data.DataTable
        dataTable.Columns.Add("Estado")

        For Each row As Data.DataRow In dataTable.Rows
            If row.Item("Estado_Factura") = Facturas.FacturasHandler.EstadoFactura.Transaction_Successful Then
                row.Item("Estado") = "Cancelado"
            Else
                row.Item("Estado") = "Pendiente"
            End If
        Next
        Return dataTable
    End Function

End Class
