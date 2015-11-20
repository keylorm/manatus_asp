Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler
Imports System.IO

Namespace Orbelink.Control.Archivos

    ''' <summary>
    ''' Informacion del resultado del archivo
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ArchivoInfo

        Public Const classIdentifier As String = "Orbelink.Control.Archivos.ArchivoInfo Error: "

        Protected _Path As String
        Protected _Name As String
        Protected _RealName As String
        Protected _Extension As String
        Protected _FileName As String
        Protected _FilePath As String
        Protected _FileType As ArchivoHandler.ArchivoType
        Protected _FileSize As Integer

        Public Saved As Boolean
        Public Overrided As Boolean
        Public Forced As Boolean

        Sub New(ByVal theFilePath As String)
            _Path = ""
            _Extension = ""
            _FilePath = ""

            Dim algo As New FileInfo(theFilePath)
            _FilePath = theFilePath
            _Path = _FilePath.Substring(0, _FilePath.LastIndexOf("\") + 1)
            _FileName = algo.Name.Substring(0, algo.Name.LastIndexOf("."))
            _Extension = algo.Extension
            _Name = _FileName
            _RealName = _FileName
            _FileType = ArchivoHandler.GetArchivoType(_Extension)
            _FileSize = File.ReadAllBytes(_FilePath).Length
        End Sub

        Protected Sub New(ByVal theFilePath As String, ByVal theFileType As Integer)
            _Path = ""
            _Extension = ""
            _FilePath = ""

            _FilePath = theFilePath
            _Path = _FilePath.Substring(0, _FilePath.LastIndexOf("\") + 1)
            _FileName = _FilePath.Substring(_FilePath.LastIndexOf("\") + 1)
            _Extension = _FileName.Substring(_FileName.LastIndexOf(".") + 1)
            _Name = _FileName.Substring(0, _FileName.LastIndexOf("."))
            _RealName = _Name
            _FileType = theFileType
            _FileSize = File.ReadAllBytes(_FilePath).Length
        End Sub

        Public ReadOnly Property Extension() As String
            Get
                Return _Extension
            End Get
        End Property

        Public ReadOnly Property FileType() As ArchivoHandler.ArchivoType
            Get
                Return _FileType
            End Get
        End Property

        ''' <summary>
        ''' Propiedad con el nombre y la extension
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property FileName() As String
            Get
                Return _FileName
            End Get
        End Property

        ''' <summary>
        ''' Direccion absoluta del archivo. (Incluye nombre y extension)
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property FilePath() As String
            Get
                Return _FilePath
            End Get
        End Property

        ''' <summary>
        ''' Direccion absoluta del folder 
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Path() As String
            Get
                Return _Path
            End Get
        End Property

        ''' <summary>
        ''' Nombre del archivo, sin extension
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property Name() As String
            Get
                Return _Name
            End Get
        End Property

        ''' <summary>
        ''' Tamano del archivo
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public ReadOnly Property FileSize() As Integer
            Get
                Return _FileSize
            End Get
        End Property
    End Class

End Namespace
