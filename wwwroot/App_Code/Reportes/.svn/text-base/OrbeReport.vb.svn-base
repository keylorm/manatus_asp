Imports Microsoft.Reporting.WebForms

Public Class OrbeReport

    Private _titulo As String = ""
    Private _fecha As Boolean
    Private _Listas As Generic.List(Of ReportList)
    Private _reportSize As Double = 8.1
    Private _tables As Generic.List(Of ReportTable)
    Private _charts As Generic.List(Of ReportChart)
    Private _tituloStyle As orbeStyle

    Public Sub New(Optional ByVal dataSet As Data.DataSet = Nothing)
        Listas = New Generic.List(Of ReportList)
        _tables = New Generic.List(Of ReportTable)
        charts = New Generic.List(Of ReportChart)
        tituloStyle = New orbeStyle
        With tituloStyle
            .FontWeight = "Bold"
            .FontStyle = "Italic"
            .FontSize = 12
        End With
    End Sub

    Protected Sub generarRdlc()
        Dim builder As New OrbeReportBuilder(Me)
        builder.build()
    End Sub

    Public Sub mostrarReporte(ByRef reporte As ReportViewer)
        renameDatasets(tables, "tab")
        renameDatasets(charts, "cha")
        generarRdlc()
        reporte.Reset()
        reporte.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local
        Dim rep As Microsoft.Reporting.WebForms.LocalReport = reporte.LocalReport
        Dim stream As New System.IO.StringReader(System.AppDomain.CurrentDomain.BaseDirectory & "Orbecatalog\Report2.rdl2")
        rep.LoadReportDefinition(stream)
        Dim direccion As String = System.AppDomain.CurrentDomain.BaseDirectory & "Orbecatalog\Report2.rdlc"
        rep.ReportPath = direccion
        rep.DataSources.Clear()
        agregarDatasetsGeneral(rep)
        rep.Refresh()
    End Sub

    ''' <summary>
    ''' Este se hace debido a que todos los dataset de cualquier item perteneciente al report debe tener un nombre unico
    ''' </summary>
    ''' <param name="item"></param>
    ''' <param name="prefijo"></param>
    ''' <remarks></remarks>
    Protected Sub renameDatasets(ByVal item As Object, ByVal prefijo As String, Optional ByVal secondPre As String = "")
        For counter As Integer = 0 To item.Count - 1
            Dim dataSet As Object = item(counter)
            dataSet.source.DataSetName = prefijo & secondPre & "Ds" & counter + 1
            dataSet.source.Tables(0).TableName = prefijo & "Ds" & counter + 1
            verificarCharsHijos(dataSet, 0)
        Next
    End Sub

    ''' <summary>
    ''' Obtiene todas las propiedades que sean de tipo generic, ya que por lo general estas ocupan datasets para poder generear el reporte
    ''' </summary>
    ''' <param name="rep"></param>
    ''' <remarks></remarks>
    Protected Sub agregarDatasetsGeneral(ByRef rep As Microsoft.Reporting.WebForms.LocalReport)
        Dim propiedades() As System.Reflection.PropertyInfo = Me.GetType.GetProperties
        For counter As Integer = 0 To propiedades.Length - 1
            Dim laPropiedad As System.Reflection.PropertyInfo = propiedades(counter)
            If laPropiedad.PropertyType.FullName.Contains("Generic.List") And Not laPropiedad.Name = "Listas" Then
                Dim userType As Type = Me.GetType
                Dim UserProp As System.Reflection.PropertyInfo = userType.GetProperty(laPropiedad.Name)
                Dim myValue As Object
                myValue = UserProp.GetValue(Me, Nothing)
                agregarDataset(rep, myValue)
            End If
        Next
    End Sub

    Protected Sub agregarDataset(ByRef rep As Microsoft.Reporting.WebForms.LocalReport, ByVal item As Object)
        For counter As Integer = 0 To item.Count - 1
            Dim dataSet As Object = item(counter)
            Dim nombre As String = dataSet.source.DataSetName
            Dim dataEntrada As New Microsoft.Reporting.WebForms.ReportDataSource(nombre, dataSet.source.Tables(0))
            rep.DataSources.Add(dataEntrada)
            verificarCharsHijos(dataSet, 1, rep)
        Next
    End Sub

    Protected Sub verificarCharsHijos(ByVal dataSet As Object, ByVal destino As Integer, Optional ByRef rep As Microsoft.Reporting.WebForms.LocalReport = Nothing)
        Dim propiedades() As System.Reflection.PropertyInfo = dataSet.GetType.GetProperties
        For contador As Integer = 0 To propiedades.Length - 1
            Dim laPropiedad As System.Reflection.PropertyInfo = propiedades(contador)
            If laPropiedad.Name = "charts" Then
                Select Case destino
                    Case 0
                        renameDatasets(dataSet.charts, "ct", dataSet.source.Tables(0).TableName)
                    Case 1
                        agregarDataset(rep, dataSet.charts)
                End Select
            End If
        Next
    End Sub

    Public Property Titulo() As String
        Get
            Return _titulo
        End Get
        Set(ByVal value As String)
            _titulo = value
        End Set
    End Property

    Public Property fecha() As Boolean
        Get
            Return _fecha
        End Get
        Set(ByVal value As Boolean)
            _fecha = value
        End Set
    End Property

    Public Property Listas() As Generic.List(Of ReportList)
        Get
            Return _Listas
        End Get
        Set(ByVal value As Generic.List(Of ReportList))
            _Listas = value
        End Set
    End Property

    Public Property tables() As Generic.List(Of ReportTable)
        Get
            Return _tables
        End Get
        Set(ByVal value As Generic.List(Of ReportTable))
            _tables = value
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

    Public Property reportSize() As Double
        Get
            Return _reportSize
        End Get
        Set(ByVal value As Double)
            _reportSize = value
        End Set
    End Property

    Public Property tituloStyle() As orbeStyle
        Get
            Return _tituloStyle
        End Get
        Set(ByVal value As orbeStyle)
            _tituloStyle = value
        End Set
    End Property

End Class 'RdlGenerator ' TODO: Generate RDL using XmlTextWriter


