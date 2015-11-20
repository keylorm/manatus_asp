Imports Microsoft.VisualBasic

Public Class ReportTable
    Inherits ReportItem

    Private _indiceColumnaTotal As Integer
    Private _SumColumn As String
    Private _titulo As String
    Private _anchoTotal As Boolean
    Private _rowsStyle As orbeStyle
    Private _headerStyle As orbeStyle
    Private _charts As Generic.List(Of ReportChart)
    Private _titleStyle As orbeStyle

    Public Property SumColumn() As String
        Get
            Return _SumColumn
        End Get
        Set(ByVal value As String)
            _SumColumn = value
        End Set
    End Property

    Public Sub New()
        IniciarVariables()
    End Sub

    Public Sub New(ByVal theSource As Data.DataSet)
        IniciarVariables()
        If Not theSource Is Nothing Then
            source = theSource
        End If
    End Sub

    Protected Sub IniciarVariables()
        source = Nothing
        HorizontalAlign = posicionHorizontal.Izquierda
        paddingLeft = 1
        paddingTop = 0
        anchoTotal = True
        rowsStyle = New orbeStyle
        headerStyle = New orbeStyle
        titleStye = New orbeStyle

        headerStyle.FontWeight = "Bold"
        headerStyle.Color = "Green"
        headerStyle.FontSize = 10
        headerStyle.BorderStyle = "Solid"
        rowsStyle.BorderStyle = "Solid"

        titleStye.FontWeight = "Bold"
        titleStye.FontSize = 12
        titleStye.TextAlign = orbeStyle.textALignOptions.Left

        charts = New Generic.List(Of ReportChart)
    End Sub

    ''' <summary>
    ''' Recibe un dataSet el cual contiene el nombre de las columnas del reportTable, los nombres son tomados de la columna 0
    ''' </summary>
    ''' <param name="dataSet">table(0).Column(0) contiene los nombres</param>
    ''' <remarks></remarks>
    Public Sub setColumnsName(ByVal dataSet As Data.DataSet)
        For counter As Integer = 0 To dataSet.Tables(0).Rows.Count - 1
            source.Tables(0).Columns(counter).ColumnName = dataSet.Tables(0).Rows(counter).Item(0)
            If source.Tables(0).Columns.Count < dataSet.Tables(0).Rows.Count Then
                source.Tables(0).Columns.Add()
            End If
        Next
        GenerateFieldsList(source)
    End Sub

    Public Property titulo() As String
        Get
            Return _titulo
        End Get
        Set(ByVal value As String)
            _titulo = value
        End Set
    End Property

    Public Property anchoTotal() As Boolean
        Get
            Return _anchoTotal
        End Get
        Set(ByVal value As Boolean)
            _anchoTotal = value
        End Set
    End Property

    Public Property headerStyle() As orbeStyle
        Get
            Return _headerStyle
        End Get
        Set(ByVal value As orbeStyle)
            _headerStyle = value
        End Set
    End Property

    Public Property rowsStyle() As orbeStyle
        Get
            Return _rowsStyle
        End Get
        Set(ByVal value As orbeStyle)
            _rowsStyle = value
        End Set
    End Property

    Public Property charts() As Generic.List(Of ReportChart)
        Get
            Return _charts
        End Get
        Set(ByVal value As Generic.List(Of ReportChart))
            _charts = value
        End Set
    End Property

    Public Property titleStye() As orbeStyle
        Get
            Return _titleStyle
        End Get
        Set(ByVal value As orbeStyle)
            _titleStyle = value
        End Set
    End Property


End Class
