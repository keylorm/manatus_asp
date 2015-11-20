Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler
Imports Orbelink.Entity.Facturas
Imports Orbelink.Entity.Productos

Namespace Orbelink.Control.Facturas
    Public Class HandlerDetalleFactura_Producto
        Implements IDetalleFacturaHandler

        Public Function AgregarDetalleFactura(ByVal theConnectionString As String, ByVal id_factura As Integer, ByVal Detalle As Integer, ByVal dueno As DBHandler.DBTable) As Boolean Implements IDetalleFacturaHandler.AgregarDetalleFactura
            Dim connection As New SQLServer(theConnectionString)
            Dim query As New QueryBuilder()
            Try
                Dim productoDueno As Producto = dueno
                Dim detalleFacturaIntermedio As New DetalleFactura_Producto
                detalleFacturaIntermedio.Id_Factura.Value = id_factura
                detalleFacturaIntermedio.Detalle.Value = Detalle
                detalleFacturaIntermedio.Id_Producto.Value = productoDueno.Id_Producto.Value
                connection.executeInsert(query.InsertQuery(detalleFacturaIntermedio))
                Return True
            Catch exc As Exception
                Return False
            End Try
        End Function

        Public Function GetNombreDisplay(ByVal theConnectionString As String, ByVal dueno As DBHandler.DBTable) As String Implements IDetalleFacturaHandler.GetNombreDisplay
            Dim productoDueno As Producto = dueno
            Return productoDueno.Nombre.Value
        End Function

        Public Function FindDetalleByDueno(ByVal theConnectionString As String, ByVal id_factura As Integer, ByVal dueno As DBHandler.DBTable) As Integer Implements IDetalleFacturaHandler.FindDetalleByDueno
            Dim connection As New SQLServer(theConnectionString)
            Dim query As New QueryBuilder()

            Dim productoDueno As Producto = dueno
            Dim detalleFacturaIntermedio As New DetalleFactura_Producto
            detalleFacturaIntermedio.Id_Factura.Where.EqualCondition(id_factura)
            detalleFacturaIntermedio.Detalle.ToSelect = True
            detalleFacturaIntermedio.Id_Producto.Where.EqualCondition(productoDueno.Id_Producto.Value)
            Dim resultTable As Data.DataTable = connection.executeSelect_DT(query.SelectQuery(detalleFacturaIntermedio))
            If resultTable.Rows.Count > 0 Then
                detalleFacturaIntermedio = ObjectBuilder.CreateDBTable_BySelectIndex(resultTable.Rows(0), detalleFacturaIntermedio)
                Return detalleFacturaIntermedio.Detalle.Value
            End If
            Return -1
        End Function
    End Class
End Namespace