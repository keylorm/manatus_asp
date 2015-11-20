Imports Microsoft.VisualBasic

Public Class ReportChart
    Inherits ReportItem

    Private _titulo As String
    Private _type As TiposChart

    Public Sub New()
        IniciarVariables()
    End Sub

    Enum TiposChart
        Column
        Bar
        Line
        Pie
        Area
    End Enum

    Public Sub New(ByVal theSource As Data.DataSet)
        IniciarVariables()
        If Not theSource Is Nothing Then
            source = theSource
        End If
    End Sub

    Protected Sub IniciarVariables()
        source = Nothing
        HorizontalAlign = posicionHorizontal.Izquierda
        paddingLeft = 0.1
        paddingTop = 0
        type = TiposChart.Column
    End Sub

    Public Property titulo() As String
        Get
            Return _titulo
        End Get
        Set(ByVal value As String)
            _titulo = value
        End Set
    End Property

    Public Property type() As TiposChart
        Get
            Return _type
        End Get
        Set(ByVal value As TiposChart)
            _type = value
        End Set
    End Property

End Class
