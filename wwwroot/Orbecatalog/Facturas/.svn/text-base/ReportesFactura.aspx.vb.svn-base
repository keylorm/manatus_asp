Imports Orbelink.DBHandler
Imports Orbelink.Entity.Entidades
Imports Orbelink.Entity.Facturas
Imports Orbelink.Entity.Productos
Imports Orbelink.Control

Partial Class Orbecatalog_Facturas_ReportesFactura
    Inherits PageBaseClass

    Const codigo_pantalla As String = "FC-RP"
    Const level As Integer = 2

    Protected Sub Page_Load1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddScripts(level)
        If Not IsPostBack Then
            securityHandler.VerifyPantalla(codigo_pantalla, level)
            startCalendar.SelectedDate = Date.Today
            txb_inicioReporte.Text = Date.Today
            endCalendar.SelectedDate = Date.Today.AddDays(1)
            txb_finalReporte.Text = Date.Today.AddDays(1)
            cargarEstadosFactura()
            generarReporte()
        End If
    End Sub

    Protected Sub cargarEstadosFactura()
        Dim list As New ListItem("Pendiente", Facturas.FacturasHandler.EstadoFactura.Unapproved_Cart)
        Dim list3 As New ListItem("Cancelado", Facturas.FacturasHandler.EstadoFactura.Transaction_Successful)
        Dim list2 As New ListItem("Cualquiera", -1)
        ddl_estadoFactura.Items.Add(list2)
        ddl_estadoFactura.Items.Add(list)
        ddl_estadoFactura.Items.Add(list3)
    End Sub

    Protected Sub generarReporte()
        Dim factura As New Factura
        Dim vendedor As New Entidad
        Dim cliente As New Entidad
        cliente.AsName = "Cliente"
        Dim dataTable As New Data.DataTable

        vendedor.NombreDisplay.ToSelect = True
        vendedor.NombreDisplay.AsName = "N_Vendedor"
        cliente.NombreDisplay.ToSelect = True
        cliente.NombreDisplay.AsName = "N_Cliente"
        factura.Fecha_Factura.ToSelect = True
        factura.Total.ToSelect = True
        factura.Estado_Factura.ToSelect = True


        factura.Fecha_Factura.Where.GreaterThanOrEqualCondition(startCalendar.SelectedDate)
        factura.Fecha_Factura.Where.LessThanOrEqualCondition(endCalendar.SelectedDate)

        If ddl_estadoFactura.SelectedValue > 0 Then
            factura.Estado_Factura.Where.EqualCondition(ddl_estadoFactura.SelectedValue)
        End If

        queryBuilder.Join.EqualCondition(factura.id_Vendedor, vendedor.Id_entidad)
        queryBuilder.Join.EqualCondition(factura.Id_Comprador, cliente.Id_entidad)

        queryBuilder.From.Add(vendedor)
        queryBuilder.From.Add(cliente)
        queryBuilder.From.Add(factura)
        queryBuilder.Orderby.Add(factura.Estado_Factura)
        queryBuilder.Orderby.Add(factura.Fecha_Factura)
        Dim consulta As String = queryBuilder.RelationalSelectQuery
        dataTable = connection.executeSelect_DT(consulta)
        dataTable = cambiarEstadoString(dataTable)

        RW_factura.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local
        Dim rep As Microsoft.Reporting.WebForms.LocalReport = RW_factura.LocalReport
        rep.ReportPath = "Orbecatalog\Facturas\facturas.rdlc"
        rep.DataSources.Clear()
        Dim dataEntrada As New Microsoft.Reporting.WebForms.ReportDataSource()
        dataEntrada.Name = "facturaDatSet_DataTable1"
        dataEntrada.Value = dataTable
        rep.DataSources.Add(dataEntrada)

    End Sub

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

    Protected Sub btn_mostrar_ServerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_mostrar.ServerClick
        generarReporte()
        Upd_pagina.Update()
    End Sub
End Class

