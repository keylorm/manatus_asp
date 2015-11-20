Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler

Namespace Orbelink.Control.Archivos
    Public Interface IControladorArchivos

        Function generarDataSet(ByVal id_dueno As Integer, ByVal fileType As Integer) As Data.DataSet

        Function insertarArchivoIntermedio(ByVal id_Archivo As Integer, ByVal id_dueno As Integer, ByVal principal As Integer, ByVal pertenencias As Integer) As Boolean

        Function ActualizarArchivoIntermedio(ByVal id_Archivo As Integer, ByVal id_dueno As Integer, ByVal principal As Integer) As Boolean

    End Interface
End Namespace