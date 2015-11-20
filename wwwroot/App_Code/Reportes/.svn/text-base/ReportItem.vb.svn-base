Imports Microsoft.VisualBasic

Public Class ReportItem
    Private _source As Data.DataSet
    Private _HorizontalAlign As posicionHorizontal
    Private _paddingLeft As Double
    Private _paddingTop As Double
    Private _m_fields As ArrayList


    Enum posicionHorizontal
        Izquierda = 0
        Derecha = 1
        Centro = 2
    End Enum

    Public Property HorizontalAlign() As posicionHorizontal
        Get
            Return _HorizontalAlign
        End Get
        Set(ByVal value As posicionHorizontal)
            _HorizontalAlign = value
        End Set
    End Property

    Public Overridable Property source() As Data.DataSet
        Get
            Return _source
        End Get
        Set(ByVal value As Data.DataSet)
            _source = value
            GenerateFieldsList(value)
        End Set
    End Property

    Public Property paddingLeft() As Double
        Get
            Return _paddingLeft
        End Get
        Set(ByVal value As Double)
            _paddingLeft = value
        End Set
    End Property


    Public Property paddingTop() As Double
        Get
            Return _paddingTop
        End Get
        Set(ByVal value As Double)
            _paddingTop = value
        End Set
    End Property

    Public Property m_fields() As ArrayList
        Get
            Return _m_fields
        End Get
        Set(ByVal value As ArrayList)
            _m_fields = value
        End Set
    End Property

    Protected Sub GenerateFieldsList(ByVal dataSet As Data.DataSet)
        If Not dataSet Is Nothing Then
            m_fields = New ArrayList()
            For Each columna As Data.DataColumn In dataSet.Tables(0).Columns
                If columna.ColumnMapping <> Data.MappingType.Hidden Then
                    m_fields.Add(columna.ColumnName)
                End If
            Next
        End If
    End Sub 'GenerateFieldsList

End Class
