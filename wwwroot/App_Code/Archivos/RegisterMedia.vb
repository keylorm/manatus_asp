Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols

<WebService(Namespace:="http://www.orbelink.com/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class RegisterArchivo
    Inherits Orbelink.Orbecatalog6.WebServiceBaseClass

    Public Shared ReadOnly Property Config_DefaultConfiguration() As Integer
        Get
            'Para quitar esta exception, debe comentar esta linea y en el return escribir el valor correspondiente
            Throw New NotImplementedException("Debe especificar el valor correspondiente.")
            Return 0
        End Get
    End Property

    'Producto
    <WebMethod()> _
    Public Function RegisterArchivoProducto(ByVal id_Producto As Integer, ByVal FileName As String, ByVal Extension As String, ByVal sizeInBytes As Integer) As Boolean
        Dim archivo As String = Configuration.uploadPath & FileName & Extension
        Dim fileType As Integer = Orbelink.Control.Archivos.ArchivoHandler.GetArchivoType(Extension)
        Dim nuevaRuta As String = Orbelink.Control.Archivos.ArchivoHandler.GetFolder(fileType)
        Dim resultado As Boolean = False
        If nuevaRuta.Length > 0 Then
            nuevaRuta &= FileName & "." & Extension
            If id_Producto = 0 Then
                id_Producto = 1
            End If

            'File.Move(archivo, nuevaRuta)
            resultado = insertArchivo_Producto(id_Producto, FileName, Extension, sizeInBytes, fileType, Config_DefaultConfiguration)
        End If
        Return resultado
    End Function
    Private Function insertArchivo_Producto(ByVal id_Producto As Integer, ByVal fileName As String, _
    ByVal extension As String, ByVal sizeInBytes As Integer, ByVal theFileType As Integer, ByVal Id_ArchivoConfiguration As Integer) As Boolean
        'Dim Archivo_Producto As New Archivo
        Try
            '    Archivo_Producto.Nombre.Value = fileName
            '    Archivo_Producto.Fecha.ValueUtc = Date.UtcNow
            '    'Archivo_Producto.Comentario.Value = ""
            '    Archivo_Producto.FileName.Value = fileName
            '    Archivo_Producto.Extension.Value = extension
            '    Archivo_Producto.Size.Value = sizeInBytes
            '    Archivo_Producto.FileType.Value = theFileType
            '    Archivo_Producto.Id_Producto.Value = id_Producto
            '    Archivo_Producto.Id_ArchivoConfiguration.Value = Id_ArchivoConfiguration
            '    Archivo_Producto.Principal.Value = 0
            'If connection.executeInsert(queryBuilder.InsertQuery(Archivo_Producto)) > 0 Then
            '    Return True
            'End If
        Catch exc As Exception
        End Try
        Return False
    End Function
End Class
