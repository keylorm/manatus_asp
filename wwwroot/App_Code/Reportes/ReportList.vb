Imports Microsoft.VisualBasic

Public Class ReportList
    Inherits ReportItem

    Private _source As ArrayList
    Private _verticalalign As posicionVertical
    Private _witdth As Double
    Private _listStyle As orbeStyle

    Public Sub New(ByVal arreglo As ArrayList)
        IniciarVariables()
        If Not arreglo Is Nothing Then
            For counter As Integer = 0 To arreglo.Count - 1
                source.Add(arreglo(counter))
            Next
        End If
    End Sub

    Public Sub New(ByVal dataSet As Data.DataSet, ByVal indice As Integer, ByVal recorriso As recorrido)
        IniciarVariables()
        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                If recorriso = recorrido.columna Then
                    For Each rows As Data.DataRow In dataSet.Tables(0).Rows
                        source.Add(rows.Item(indice))
                    Next
                Else
                    For counter As Integer = 1 To dataSet.Tables(0).Columns.Count - 1
                        source.Add(dataSet.Tables(0).Rows(indice).Item(counter))
                    Next
                End If
            End If
        End If
    End Sub

    Public Sub New()
        IniciarVariables()
    End Sub

    Protected Sub IniciarVariables()
        source = New ArrayList
        HorizontalAlign = posicionHorizontal.Izquierda
        verticalAlign = posicionVertical.Encabezado
        listStyle = New orbeStyle
        paddingLeft = 0.1
        paddingTop = 0
        width = 1
    End Sub

    Enum recorrido
        fila = 0
        columna = 1
    End Enum

    Enum posicionVertical
        Encabezado = 0
        Pie = 1
    End Enum

    Public Property verticalAlign() As posicionVertical
        Get
            Return _verticalalign
        End Get
        Set(ByVal value As posicionVertical)
            _verticalalign = value
        End Set
    End Property


    Public Overloads Property source() As ArrayList
        Get
            Return _source
        End Get
        Set(ByVal value As ArrayList)
            _source = value
        End Set
    End Property

    Public Sub add(ByVal objeto As Object)
        source.Add(objeto)
    End Sub

    Public Property width() As Double
        Get
            Return _witdth
        End Get
        Set(ByVal value As Double)
            _witdth = value
        End Set
    End Property

    Public Property listStyle() As orbeStyle
        Get
            Return _listStyle
        End Get
        Set(ByVal value As orbeStyle)
            _listStyle = value
        End Set
    End Property



End Class
