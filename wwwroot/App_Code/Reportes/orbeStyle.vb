Imports Microsoft.VisualBasic

Public Class orbeStyle
    Private _Color As String
    Private _BorderStyle As String
    Private _FontSize As Integer
    Private _TextDecoration As String
    Private _FontFamily As String
    Private _TextAlign As textALignOptions
    Private _FontWeight As String
    Private _FontStyle As String

    Sub New()
        _Color = "Black"
        _BorderStyle = ""
        _FontSize = 9
        _TextDecoration = ""
        _FontFamily = "Arial"
        _TextAlign = textALignOptions.Center
        _FontWeight = ""
        _FontStyle = ""
    End Sub

    Public Property Color() As String
        Get
            Return _Color
        End Get
        Set(ByVal value As String)
            _Color = value
        End Set
    End Property

    Public Property TextDecoration() As String
        Get
            Return _TextDecoration
        End Get
        Set(ByVal value As String)
            _TextDecoration = value
        End Set
    End Property

    Public Property FontFamily() As String
        Get
            Return _FontFamily
        End Get
        Set(ByVal value As String)
            _FontFamily = value
        End Set
    End Property

    Enum textALignOptions
        Center
        Left
        Right
    End Enum

    Public Property TextAlign() As textALignOptions
        Get
            Return _TextAlign
        End Get
        Set(ByVal value As textALignOptions)
            _TextAlign = value
        End Set
    End Property

    Public Property FontWeight() As String
        Get
            Return _FontWeight
        End Get
        Set(ByVal value As String)
            _FontWeight = value
        End Set
    End Property

    Public Property FontStyle() As String
        Get
            Return _FontStyle
        End Get
        Set(ByVal value As String)
            _FontStyle = value
        End Set
    End Property

    Public Property FontSize() As String
        Get
            Return _FontSize
        End Get
        Set(ByVal value As String)
            _FontSize = value
        End Set
    End Property

    Public Property BorderStyle() As String
        Get
            Return _BorderStyle
        End Get
        Set(ByVal value As String)
            _BorderStyle = value
        End Set
    End Property
End Class
