Imports Microsoft.VisualBasic
Imports Orbelink.Entity.Archivos

Public Interface IHandlerArchivo

    ReadOnly Property Identifier() As String

    ReadOnly Property SupportedExtension() As String()

    Function ApplyConfig(ByVal orignalFilePath As String, ByVal theArchivoConfiguration As Archivo_Configuration, ByVal thumbFilePath As String) As Boolean

End Interface
