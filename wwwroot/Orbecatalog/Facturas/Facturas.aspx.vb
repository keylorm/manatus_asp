Imports Orbelink.DBHandler
Imports Orbelink.Entity.Facturas
Imports Orbelink.Entity.Entidades

Partial Class Orbecatalog_Facturas_Facturas
    Inherits PageBaseClass

    Const codigo_pantalla As String = "SR-03"
    Const level As Integer = 2

    'Pagina
    Protected Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        securityHandler.VerifyPantalla(codigo_pantalla, level)
        If Not IsPostBack Then
            DesarmarQueryString()
        End If
    End Sub

    Protected Sub DesarmarQueryString()
        Dim id_tipoFactura As Integer
        Dim Num_Factura As String = ""
        Dim Numero_Orden As String = ""
        Dim pagar As Boolean

        If Not Request.QueryString("search") Is Nothing Then
            If Not Request.QueryString("tipoFactura") Is Nothing Then
                id_tipoFactura = Request.QueryString("tipoFactura")
            End If

            If Not Request.QueryString("Num_Factura") Is Nothing Then
                Num_Factura = Request.QueryString("Num_Factura")
            End If

            If Not Request.QueryString("pagar") Is Nothing Then
                pagar = True
            End If
            selectFacturas(id_tipoFactura, Num_Factura, pagar)
        End If
    End Sub

    'Factura
    Protected Sub selectFacturas(ByVal id_tipoFactura As Integer, ByVal Num_Factura As String, ByVal porPagar As Boolean)
        Dim dataSet As Data.DataSet
        Dim Factura As New Factura
        Dim entidad As New Entidad
        Dim sumaMontos As Double = 0

        Factura.Id_Factura.ToSelect = True
        Factura.Num_Factura.ToSelect = True
        Factura.Fecha_Factura.ToSelect = True
        Factura.Fecha_Cancelado.ToSelect = True
        Factura.Total.ToSelect = True

        entidad.NombreDisplay.ToSelect = True
        entidad.Apellido.ToSelect = True
        entidad.Telefono.ToSelect = True
        entidad.NombreDisplay.AsName = "n_entidad"
        queryBuilder.Join.EqualCondition(entidad.Id_entidad, Factura.Id_Comprador)
        Try
            'Busca si hay filtro
            If id_tipoFactura <> 0 Then
                Factura.id_TipoFactura.Where.EqualCondition(id_tipoFactura)
            End If

            If Num_Factura.Length > 0 Then
                Factura.Num_Factura.Where.LikeCondition(Num_Factura)
            End If

            If porPagar Then
                Factura.Fecha_Cancelado.Where.IsNotNullCondition()
            End If

            'Revisar que pueda ordenar por algo...
            queryBuilder.Distinct = True
            queryBuilder.Orderby.Add(Factura.Fecha_Cancelado)
            queryBuilder.From.Add(Factura)
            queryBuilder.From.Add(entidad)
            dataSet = connection.executeSelect(queryBuilder.RelationalSelectQuery)
            If dataSet.Tables.Count > 0 Then
                If dataSet.Tables(0).Rows.Count > 0 Then
                    llenarGrafico(dataSet)
                End If
            End If
        Catch ex As Exception
            MyMaster.MostrarMensaje("Busqueda invalida", True)

        End Try
    End Sub

    Sub llenarGrafico(ByRef theDataset As Data.DataSet)

        ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local
        Dim rep As Microsoft.Reporting.WebForms.LocalReport = ReportViewer1.LocalReport

        rep.ReportPath = "Orbecatalog\Facturas\Reports\Gra_MontoProgSemanas.rdlc"
        rep.DataSources.Clear()

        'Dim ds As Data.DataSet = getdata(TextBox1.Text)
        If theDataset.Tables(0).Rows.Count > 0 Then
            'Create a report data source for the sales order data
            Dim dssce As New Microsoft.Reporting.WebForms.ReportDataSource()
            dssce.Name = "BusquedaFactura_BusquedaFactura"
            dssce.Value = theDataset.Tables(0)
            rep.DataSources.Add(dssce)
        End If
    End Sub

End Class
