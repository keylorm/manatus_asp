Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler

Namespace Orbelink.Control.Facturas
    Public Interface IDetalleFacturaHandler

        Function FindDetalleByDueno(ByVal theConnectionString As String, ByVal id_factura As Integer, ByVal dueno As DBTable) As Integer

        Function AgregarDetalleFactura(ByVal theConnectionString As String, ByVal id_factura As Integer, ByVal Detalle As Integer, ByVal dueno As DBTable) As Boolean

        Function GetNombreDisplay(ByVal theConnectionString As String, ByVal dueno As DBTable) As String

    End Interface
End Namespace