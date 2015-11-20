Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler

Namespace Orbelink.Control.Archivos

    Public Class FlashHandler
        Implements IHandlerArchivo

        Private Shared _supportedExtension() As String = {"swf", "fla", "flv"}

        Public ReadOnly Property Identifier() As String Implements IHandlerArchivo.Identifier
            Get
                Return "Flash"
            End Get
        End Property

        Public ReadOnly Property SupportedExtension() As String() Implements IHandlerArchivo.SupportedExtension
            Get
                Return _supportedExtension
            End Get
        End Property

        Public Function ApplyConfig(ByVal orignalFilePath As String, ByVal theArchivoConfiguration As Entity.Archivos.Archivo_Configuration, ByVal thumbFilePath As String) As Boolean Implements IHandlerArchivo.ApplyConfig
            Return True
        End Function
    End Class

End Namespace