Imports Microsoft.VisualBasic
Imports System.IO
Imports System.Xml

Public Class OrbeReportBuilder

    Private _altura As Double
    Private report As OrbeReport

    Public Sub New(ByRef reporte As OrbeReport)
        report = reporte
    End Sub

    Public Function build() As Boolean
        GenerateRdl()
        Return True
    End Function

    ''Funciones de construccion

    ''' <summary>
    ''' Este funcion es la principal a partir de esta se crea todo el reporte a partir de llamados a funciones que gereral los diversos items
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub GenerateRdl()
        ' Open a new RDL file stream for writing
        Dim stream As FileStream
        Dim direccion As String = System.AppDomain.CurrentDomain.BaseDirectory & "Orbecatalog\Report2.rdlc"
        stream = File.Create(direccion)
        Dim writer As New XmlTextWriter(stream, Encoding.UTF8)

        ' Causes child elements to be indented
        writer.Formatting = Formatting.Indented

        ' Report element
        writer.WriteProcessingInstruction("xml", "version=""1.0"" encoding=""utf-8""")
        writer.WriteStartElement("Report")
        CrearDatosInicialesXml(writer)
        ' Body element
        writer.WriteStartElement("Body")
        writer.WriteElementString("Height", "5in")
        ' ReportItems element
        writer.WriteStartElement("ReportItems")

        If report.fecha Then
            agregarFecha(writer)
        End If

        If report.Titulo.Length > 0 Then
            agregarTitulo(writer, report.tituloStyle)
        End If

        verificarListas(writer, ReportList.posicionVertical.Encabezado)

        verificarTablas(writer)

        verificarCharts(writer, report)

        verificarListas(writer, ReportList.posicionVertical.Pie)

        CrearFin(writer)
        writer.WriteEndElement() ' ReportItems
        writer.WriteEndElement() ' Body
        writer.WriteEndElement() ' Report

        ' Flush the writer and close the stream
        writer.Flush()
        stream.Close()

    End Sub

    ''' <summary>
    ''' En esta funcion se especifica el ancho del reporte, el cual es una propiedad del report
    ''' El reporte hace referencia a dataSets, los cuales se crean a partir de las tablas que contenga el repor (hasta ahorita obligatorias). Por lo cual se crean todos los necesarios
    ''' </summary>
    ''' <param name="writer">El objeto que escribe en el archivo .rdlc</param>
    ''' <remarks></remarks>
    Protected Sub CrearDatosInicialesXml(ByRef writer As XmlTextWriter)
        writer.WriteAttributeString("xmlns", Nothing, "http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")
        writer.WriteAttributeString("xmlns:rd", Nothing, "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner")
        ' DataSource element
        writer.WriteStartElement("DataSources")
        writer.WriteStartElement("DataSource")
        writer.WriteAttributeString("Name", Nothing, "DataSource1")
        writer.WriteElementString("rd:DataSourceID", "1a06eaf1-994b-4318-b685-76a5eb35a884")
        writer.WriteStartElement("ConnectionProperties")
        writer.WriteElementString("DataProvider", "SQL")
        writer.WriteElementString("ConnectString", "")
        writer.WriteElementString("IntegratedSecurity", "true")
        writer.WriteEndElement() ' ConnectionProperties
        writer.WriteEndElement() ' DataSource
        writer.WriteEndElement() ' DataSources

        writer.WriteElementString("InteractiveHeight", "30in")
        writer.WriteElementString("rd:DrawGrid", "true")
        writer.WriteElementString("InteractiveWidth", "8.5in")
        writer.WriteElementString("rd:SnapToGrid", "true")
        writer.WriteElementString("rd:ReportID", "78f1d4af-25cf-4210-8ce7-00ebd07921d3")
        writer.WriteElementString("Width", Me.formatoMedidas(report.reportSize))

        ' DataSet element
        writer.WriteStartElement("DataSets")
        agregarDataSets(writer, report.tables)
        agregarDataSets(writer, report.charts)
        writer.WriteEndElement() ' DataSets
    End Sub

    Protected Sub agregarDataSets(ByRef writer As XmlTextWriter, ByVal item As Object)
        If Not item Is Nothing Then
            For Each Data As Object In item
                writer.WriteStartElement("DataSet")
                writer.WriteAttributeString("Name", Nothing, Data.source.DataSetName)

                ' Query element
                writer.WriteStartElement("Query")
                writer.WriteElementString("DataSourceName", "DataSource1")
                writer.WriteElementString("CommandText", "")
                writer.WriteElementString("Timeout", "30")
                writer.WriteEndElement() ' Query

                ' Fields elements
                writer.WriteStartElement("Fields")
                For Each fieldName As String In Data.m_fields
                    writer.WriteStartElement("Field")
                    writer.WriteAttributeString("Name", Nothing, formatoNombre(fieldName))
                    writer.WriteElementString("DataField", Nothing, formatoNombre(fieldName))
                    writer.WriteEndElement() ' Field
                Next fieldName
                ' End previous elements
                writer.WriteEndElement() ' Fields
                writer.WriteEndElement() ' DataSet

                Dim propiedades() As System.Reflection.PropertyInfo = Data.GetType.GetProperties
                For contador As Integer = 0 To propiedades.Length - 1
                    Dim laPropiedad As System.Reflection.PropertyInfo = propiedades(contador)
                    If laPropiedad.Name = "charts" Then
                        agregarDataSets(writer, Data.charts)
                    End If
                Next
            Next
        End If
    End Sub

    ''' <summary>
    ''' Utiliza un textbox simple en el cual se indica la fecha en la que se elaboro el reporte
    ''' </summary>
    ''' <param name="writer"></param>
    ''' <remarks></remarks>
    Protected Sub agregarFecha(ByRef writer As XmlTextWriter)
        writer.WriteStartElement("Textbox")
        writer.WriteAttributeString("Name", Nothing, "fecha")
        writer.WriteElementString("ZIndex", "4")
        writer.WriteElementString("Height", "0.25in")
        writer.WriteElementString("Top", obtenerAlturaEIncrementar(0.4))
        writer.WriteElementString("Width", "4in")
        writer.WriteElementString("Left", "0in")
        writer.WriteElementString("CanGrow", "true")
        writer.WriteElementString("CanShrink", "false")
        writer.WriteElementString("Value", "Fecha de Elaboracion: " & Date.Now.ToString("d MMM yyyy"))
        writer.WriteStartElement("Style")
        writer.WriteElementString("TextAlign", "Left")
        writer.WriteEndElement() ' Style        
        writer.WriteEndElement() ' Textbox
    End Sub

    ''' <summary>
    ''' Se agrega el titulo del reporte en un textbox
    ''' </summary>
    ''' <param name="writer"></param>
    ''' <remarks></remarks>
    Protected Sub agregarTitulo(ByRef writer As XmlTextWriter, ByVal style As orbeStyle)
        writer.WriteStartElement("Textbox")
        writer.WriteAttributeString("Name", Nothing, "Titulo")
        writer.WriteElementString("ZIndex", "4")
        writer.WriteElementString("Height", "0.25in")
        writer.WriteElementString("Top", obtenerAlturaEIncrementar(0.4))
        writer.WriteElementString("Width", "6in")
        writer.WriteElementString("Left", "1in")
        writer.WriteElementString("CanGrow", "true")
        writer.WriteElementString("CanShrink", "false")
        writer.WriteElementString("Value", report.Titulo)
        setStyle(writer, style)
        writer.WriteEndElement() ' Textbox
    End Sub

    ''' <summary>
    ''' Una lista es un grupo de textbox que se despliegan de manera vertical de arriba hacia abajo, estas pueden estar en el encabezado o en el pie de pagina
    ''' y estar alineadas horizontalmente.
    ''' Estas listas estan agregadas en el report
    ''' </summary>
    ''' <param name="writer"></param>
    ''' <param name="verticalAlign"></param>
    ''' <remarks></remarks>
    Protected Sub verificarListas(ByRef writer As XmlTextWriter, ByVal verticalAlign As ReportList.posicionVertical)
        For lista As Integer = 0 To report.Listas.Count - 1
            Dim laLista As ReportList = report.Listas(lista)
            If laLista.verticalAlign = verticalAlign Then
                If laLista.HorizontalAlign = ReportList.posicionHorizontal.Izquierda Then
                    For item As Integer = 0 To laLista.source.Count - 1
                        Dim lineas As String() = Split(laLista.source(item), "<br />", -1, CompareMethod.Binary)
                        For counter As Integer = 0 To lineas.Length - 1
                            agregarTextBox(writer, "par" & lista & "_" & item & counter, 0.3, laLista.width, lineas(counter), laLista.paddingLeft)
                        Next
                    Next
                ElseIf laLista.HorizontalAlign = ReportList.posicionHorizontal.Derecha Then
                    For item As Integer = 0 To laLista.source.Count - 1
                        Dim lineas As String() = Split(laLista.source(item), "<br />", -1, CompareMethod.Binary)
                        For counter As Integer = 0 To lineas.Length - 1
                            agregarTextBox(writer, "par" & lista & "_" & item & counter, 0.3, laLista.width, lineas(counter), horizontalAlignPosition(ReportList.posicionHorizontal.Derecha, laLista.width, laLista.paddingLeft), "Right")
                        Next
                    Next
                ElseIf laLista.HorizontalAlign = ReportList.posicionHorizontal.Centro Then
                    For item As Integer = 0 To laLista.source.Count - 1
                        Dim lineas As String() = Split(laLista.source(item), "<br />", -1, CompareMethod.Binary)
                        For counter As Integer = 0 To lineas.Length - 1
                            agregarTextBox(writer, "par" & lista & "_" & item & counter, 0.3, laLista.width, lineas(counter), horizontalAlignPosition(ReportList.posicionHorizontal.Centro, laLista.width, laLista.paddingLeft), "Center")
                        Next
                    Next
                End If
            End If
        Next
    End Sub

    ''' <summary>
    ''' Agrega un textbox utilizado princiaplemente para listas y titulos
    ''' </summary>
    ''' <param name="writer"></param>
    ''' <param name="titulo"></param>
    ''' <param name="alturaIncremento"></param>
    ''' <param name="ancho"></param>
    ''' <param name="valor"></param>
    ''' <param name="left"></param>
    ''' <param name="TextAlign"></param>
    ''' <remarks></remarks>
    Protected Sub agregarTextBox(ByRef writer As XmlTextWriter, ByVal titulo As String, ByVal alturaIncremento As Double, ByVal ancho As Double, ByVal valor As String, Optional ByVal left As Double = 0.1, Optional ByVal TextAlign As String = "Left", Optional ByVal style As orbeStyle = Nothing)
        writer.WriteStartElement("Textbox")
        writer.WriteAttributeString("Name", Nothing, titulo)
        writer.WriteElementString("ZIndex", "4")
        writer.WriteElementString("Height", "0.25in")
        writer.WriteElementString("Top", obtenerAlturaEIncrementar(alturaIncremento))
        writer.WriteElementString("Width", formatoMedidas(ancho))
        writer.WriteElementString("Left", formatoMedidas(left))
        writer.WriteElementString("CanGrow", "true")
        writer.WriteElementString("CanShrink", "false")
        writer.WriteElementString("Value", valor)
        If style Is Nothing Then
            writer.WriteStartElement("Style")
            writer.WriteElementString("TextAlign", TextAlign)
            writer.WriteElementString("FontSize", "10pt")
            writer.WriteEndElement() ' Style   
        Else
            setStyle(writer, style)
        End If
        
        writer.WriteEndElement() ' Textbox

    End Sub

    Protected Sub verificarCharts(ByRef writer As XmlTextWriter, ByVal parent As Object)
        For counter As Integer = 0 To parent.charts.Count - 1
            Dim elChart As ReportChart = parent.charts(counter)
            agregarChart(writer, elChart)
        Next
    End Sub

    Protected Sub agregarChart(ByRef writer As XmlTextWriter, ByRef elChart As ReportChart, Optional ByVal left As Double = 1)
        writer.WriteStartElement("Chart")
        ''Datos Generales
        writer.WriteAttributeString("Name", Nothing, "c_" & elChart.source.DataSetName)
        writer.WriteElementString("DataSetName", elChart.source.DataSetName)
        writer.WriteElementString("PointWidth", "0")
        writer.WriteElementString("Top", obtenerAlturaEIncrementar(2.5))
        writer.WriteElementString("Subtype", "Plain")
        writer.WriteElementString("Type", elChart.type.ToString)
        writer.WriteElementString("Width", "3.25in")
        writer.WriteElementString("Palette", "Pastel")
        writer.WriteElementString("Title", "")
        writer.WriteElementString("Height", "2.4in")
        writer.WriteElementString("Left", formatoMedidas(left))

        agregarCharLegend(writer) 'ES el cuadrito que indica el color de cada cosa
        agregarCharCategoryAxis(writer) 'Las lineas que indican las medidas
        agregarCharPlotArea(writer)
        agregarCharThreeDProperties(writer)
        agregarCharSeriesGroupings(writer)
        agregarCharValueAxis(writer) 'Las lineas que indican las medidas
        agregarCharCategoryGroupings(writer, elChart)
        agregarCharChartData(writer, elChart)

        writer.WriteStartElement("Style")
        writer.WriteElementString("BackgroundColor", "White")
        writer.WriteEndElement() ' Style

        writer.WriteEndElement() ' Chart
    End Sub

    Protected Sub agregarCharLegend(ByRef writer As XmlTextWriter)
        writer.WriteStartElement("Legend")
        writer.WriteElementString("Visible", "true")
        writer.WriteElementString("Position", "RightCenter")
        writer.WriteStartElement("Style")
        writer.WriteStartElement("BorderStyle")
        writer.WriteElementString("Default", "Solid")
        writer.WriteEndElement() ' BorderStyle
        writer.WriteEndElement() ' Style
        writer.WriteEndElement() ' Legend
    End Sub

    Protected Sub agregarCharCategoryAxis(ByRef writer As XmlTextWriter)
        writer.WriteStartElement("CategoryAxis")
        writer.WriteStartElement("Axis")
        writer.WriteElementString("Title", "")
        writer.WriteElementString("MajorTickMarks", "Outside")
        writer.WriteElementString("Min", "0")
        writer.WriteElementString("Margin", "true")
        writer.WriteElementString("Visible", "true")
        writer.WriteStartElement("MajorGridLines")
        writer.WriteStartElement("Style")
        writer.WriteStartElement("BorderStyle")
        writer.WriteElementString("Default", "Solid")
        writer.WriteEndElement() ' BorderStyle
        writer.WriteEndElement() ' Style
        writer.WriteEndElement() ' MajorGridLines
        writer.WriteStartElement("MinorGridLines")
        writer.WriteStartElement("Style")
        writer.WriteStartElement("BorderStyle")
        writer.WriteElementString("Default", "Solid")
        writer.WriteEndElement() ' BorderStyle
        writer.WriteEndElement() ' Style
        writer.WriteEndElement() ' MinorGridLines
        writer.WriteEndElement() ' Axis
        writer.WriteEndElement() ' CategoryAxis
    End Sub

    Protected Sub agregarCharPlotArea(ByRef writer As XmlTextWriter)
        writer.WriteStartElement("PlotArea")
        writer.WriteStartElement("Style")
        writer.WriteElementString("BackgroundColor", "LightGrey")
        writer.WriteStartElement("BorderStyle")
        writer.WriteElementString("Default", "Solid")
        writer.WriteEndElement() ' BorderStyle
        writer.WriteEndElement() ' Style
        writer.WriteEndElement() ' PlotArea
    End Sub

    Protected Sub agregarCharThreeDProperties(ByRef writer As XmlTextWriter)
        writer.WriteStartElement("ThreeDProperties")
        writer.WriteElementString("Rotation", "-0")
        writer.WriteElementString("Enabled", "false")
        writer.WriteElementString("Inclination", "-10")
        writer.WriteElementString("Shading", "Simple")
        writer.WriteElementString("WallThickness", "50")
        writer.WriteEndElement() ' ThreeDProperties
    End Sub

    Protected Sub agregarCharSeriesGroupings(ByRef writer As XmlTextWriter)
        writer.WriteStartElement("SeriesGroupings")
        writer.WriteStartElement("SeriesGrouping")
        writer.WriteStartElement("StaticSeries")
        writer.WriteStartElement("StaticMember")
        writer.WriteElementString("Label", "Valor")
        writer.WriteEndElement() ' StaticMember
        writer.WriteEndElement() ' StaticSeries
        writer.WriteEndElement() ' SeriesGrouping
        writer.WriteEndElement() ' SeriesGroupings
    End Sub

    Protected Sub agregarCharValueAxis(ByRef writer As XmlTextWriter)
        writer.WriteStartElement("ValueAxis")
        writer.WriteStartElement("Axis")
        writer.WriteElementString("Title", "")
        writer.WriteElementString("MajorTickMarks", "Outside")
        writer.WriteElementString("Min", "0")
        writer.WriteElementString("Visible", "true")
        writer.WriteElementString("Margin", "true")
        writer.WriteElementString("Scalar", "true")
        writer.WriteStartElement("MajorGridLines")
        writer.WriteElementString("ShowGridLines", "true")
        writer.WriteStartElement("Style")
        writer.WriteStartElement("BorderStyle")
        writer.WriteElementString("Default", "Solid")
        writer.WriteEndElement() ' BorderStyle
        writer.WriteEndElement() ' Style
        writer.WriteEndElement() ' MajorGridLines
        writer.WriteStartElement("MinorGridLines")
        writer.WriteStartElement("Style")
        writer.WriteStartElement("BorderStyle")
        writer.WriteElementString("Default", "Solid")
        writer.WriteEndElement() ' BorderStyle
        writer.WriteEndElement() ' Style
        writer.WriteEndElement() ' MinorGridLines
        writer.WriteEndElement() ' Axis
        writer.WriteEndElement() ' ValueAxis
    End Sub

    Protected Sub agregarCharCategoryGroupings(ByRef writer As XmlTextWriter, ByRef elChart As ReportChart)
        writer.WriteStartElement("CategoryGroupings")
        writer.WriteStartElement("CategoryGrouping")
        writer.WriteStartElement("DynamicCategories")
        writer.WriteStartElement("Grouping")
        writer.WriteAttributeString("Name", Nothing, "CateGroup" & elChart.source.DataSetName)
        writer.WriteStartElement("GroupExpressions")
        writer.WriteElementString("GroupExpression", "=Fields!" + formatoNombre(elChart.m_fields(0)) + ".Value")
        writer.WriteEndElement() ' GroupExpressions
        writer.WriteEndElement() ' Grouping
        writer.WriteElementString("Label", "=Fields!" + formatoNombre(elChart.m_fields(0)) + ".Value")
        writer.WriteEndElement() ' DynamicCategories
        writer.WriteEndElement() ' CategoryGrouping
        writer.WriteEndElement() ' CategoryGroupings
    End Sub

    Protected Sub agregarCharChartData(ByRef writer As XmlTextWriter, ByRef elChart As ReportChart)
        writer.WriteStartElement("ChartData")
        writer.WriteStartElement("ChartSeries")
        writer.WriteStartElement("DataPoints")
        writer.WriteStartElement("DataPoint")
        writer.WriteStartElement("DataValues")
        writer.WriteStartElement("DataValue")
        writer.WriteElementString("Value", "=cdbl(Fields!" + formatoNombre(elChart.m_fields(1)) + ".Value)")
        writer.WriteEndElement() ' DataValue
        writer.WriteEndElement() ' DataValues
        writer.WriteElementString("DataLabel", "")
        writer.WriteStartElement("Marker")
        writer.WriteElementString("Size", "6pt")
        writer.WriteEndElement() ' Marker
        writer.WriteEndElement() ' DataPoint
        writer.WriteEndElement() ' DataPoints
        writer.WriteEndElement() ' ChartSeries
        writer.WriteEndElement() ' ChartData
    End Sub
    

    ''' <summary>
    ''' Se agregan tablas al reporte, estas se insertan en la parte media del reporte(en sentido vertical) pueden estar alineadas horizontalmente, se obtienen a 
    ''' partir del report
    ''' </summary>
    ''' <param name="writer"></param>
    ''' <remarks></remarks>
    Protected Sub verificarTablas(ByRef writer As XmlTextWriter)
        For counter As Integer = 0 To report.tables.Count - 1
            Dim laTabla As ReportTable = report.tables(counter)
            If laTabla.HorizontalAlign = ReportTable.posicionHorizontal.Izquierda Then
                agregarTabla(writer, laTabla, laTabla.paddingLeft)
            ElseIf laTabla.HorizontalAlign = ReportTable.posicionHorizontal.Derecha Then
                agregarTabla(writer, laTabla, horizontalAlignPosition(ReportTable.posicionHorizontal.Derecha, laTabla.m_fields.Count, laTabla.paddingLeft))
            ElseIf laTabla.HorizontalAlign = ReportTable.posicionHorizontal.Centro Then
                agregarTabla(writer, laTabla, horizontalAlignPosition(ReportTable.posicionHorizontal.Centro, laTabla.m_fields.Count, laTabla.paddingLeft))
            End If
        Next
    End Sub

    ''' <summary>
    ''' Esta funcion crea la estructura general de la tabkla que se esta agregando
    ''' </summary>
    ''' <param name="writer"></param>
    ''' <param name="laTabla"></param>
    ''' <param name="left"></param>
    ''' <remarks></remarks>
    Protected Sub agregarTabla(ByRef writer As XmlTextWriter, ByRef laTabla As ReportTable, ByVal left As Double)
        'Titlu tabla
        If laTabla.titulo <> Nothing Then
            If laTabla.titulo.Length > 0 Then
                agregarTituloTabla(writer, laTabla)
            End If
        End If

        ' Table element
        writer.WriteStartElement("Table")
        writer.WriteAttributeString("Name", Nothing, "Table_" & laTabla.source.Tables(0).TableName)
        writer.WriteElementString("DataSetName", laTabla.source.DataSetName)
        writer.WriteElementString("Top", obtenerAlturaEIncrementar(0.5))
        writer.WriteElementString("Left", formatoMedidas(left))
        writer.WriteElementString("Height", "2in")

        ' Table Columns
        addTableColums(writer, laTabla.m_fields, laTabla.anchoTotal)

        ' Header Row      
        addHeaderFooter(writer, laTabla.m_fields, laTabla.source.Tables(0).TableName, "Header", laTabla.headerStyle)

        setStyle(writer, laTabla.rowsStyle)

        ' Footer Row      
        addHeaderFooter(writer, laTabla.m_fields, laTabla.source.Tables(0).TableName, "Footer", laTabla.headerStyle, laTabla.SumColumn)

        ' Details Row
        addDetailsRows(writer, laTabla.m_fields, laTabla.source.Tables(0).TableName, laTabla.rowsStyle)

        For columna As Integer = 0 To laTabla.source.Tables(0).Columns.Count - 1
            laTabla.source.Tables(0).Columns(columna).ColumnName = formatoNombre(laTabla.source.Tables(0).Columns(columna).ColumnName)
        Next
        ' End table element and end report definition file
        writer.WriteEndElement() ' Table

        verificarCharts(writer, laTabla)



    End Sub

    ''' <summary>
    ''' Titulo de la tabla, agregando un texybox simple
    ''' </summary>
    ''' <param name="writer"></param>
    ''' <param name="laTabla"></param>
    ''' <remarks></remarks>
    Protected Sub agregarTituloTabla(ByRef writer As XmlTextWriter, ByRef laTabla As ReportTable)
        Dim lineas As String() = Split(laTabla.titulo, "<br />", -1, CompareMethod.Binary)
        For counter As Integer = 0 To lineas.Length - 1
            agregarTextBox(writer, "tituloTable_" & laTabla.source.Tables(0).TableName & counter, 0.4, 6, lineas(counter), 0.1, "Left", laTabla.titleStye)
        Next
    End Sub

    ''' <summary>
    ''' Se definen las columnas de la tabla y el ancho de cada columna
    ''' </summary>
    ''' <param name="writer"></param>
    ''' <param name="m_fields"></param>
    ''' <param name="anchoTotal"></param>
    ''' <remarks></remarks>
    Protected Sub addTableColums(ByRef writer As XmlTextWriter, ByVal m_fields As ArrayList, ByVal anchoTotal As Boolean)
        Dim elAncho As Double = 1
        If anchoTotal Then
            ''Se realiza el calculo del acnho de las columnas a partir de si la tabla ocupara todo el ancho del reporte en caso contrario el ancho default es 1
            ''ancho = ancho de una columna(.4 es para que exista un "margin")
            elAncho = (report.reportSize - 0.4) / m_fields.Count
        End If
        writer.WriteElementString("Width", formatoMedidas(elAncho * m_fields.Count))
        writer.WriteStartElement("TableColumns")
        For Each fieldName In m_fields
            writer.WriteStartElement("TableColumn")
            writer.WriteElementString("Width", formatoMedidas(elAncho))
            writer.WriteEndElement() ' TableColumn
        Next fieldName
        writer.WriteEndElement() ' TableColumns
    End Sub

    ''' <summary>
    ''' Inserta ya sea el header o el footer en una tabla, se utiliza la misma funcion porque tienen caracteristicas similares.
    ''' Si es header se centran los titulos y se repiten en todas las paginas
    ''' Si es footer puede tener la opcion de sumar todos los campos de la columna
    ''' </summary>
    ''' <param name="writer"></param>
    ''' <param name="m_fields">campos de la tabla(columnas)</param>
    ''' <param name="tableName"></param>
    ''' <param name="encabezado">diferencia si es header o footer</param>
    ''' <param name="sumColumn">nombre de la columna que se va a sumar</param>
    ''' <remarks></remarks>
    Protected Sub addHeaderFooter(ByRef writer As XmlTextWriter, ByVal m_fields As ArrayList, ByVal tableName As String, ByVal encabezado As String, ByVal style As orbeStyle, Optional ByVal sumColumn As String = "")
        If encabezado = "Header" Or Not sumColumn Is Nothing Then
            writer.WriteStartElement(encabezado)
            If encabezado = "Header" Then
                writer.WriteElementString("FixedHeader", "true")
                writer.WriteElementString("RepeatOnNewPage", "true")
            End If
            writer.WriteStartElement("TableRows")
            writer.WriteStartElement("TableRow")
            writer.WriteElementString("Height", "0.2in")
            writer.WriteStartElement("TableCells")
            For Each fieldName In m_fields
                writer.WriteStartElement("TableCell")
                writer.WriteStartElement("ReportItems")
                ' Textbox               
                writer.WriteStartElement("Textbox")
                writer.WriteAttributeString("Name", Nothing, tableName + encabezado + formatoNombre(fieldName))
                setStyle(writer, style)
                writer.WriteElementString("ZIndex", "4")
                writer.WriteElementString("Height", "0.2in")
                writer.WriteElementString("CanGrow", "true")
                writer.WriteElementString("CanShrink", "false")

                If encabezado = "Header" Then
                    writer.WriteElementString("Value", fieldName)
                ElseIf encabezado = "Footer" And fieldName = sumColumn Then
                    writer.WriteElementString("Value", "=""Total: "" & Sum(Fields!" + formatoNombre(fieldName) + ".Value)")
                Else
                    writer.WriteElementString("Value", "")
                End If
                writer.WriteEndElement() ' Textbox
                writer.WriteEndElement() ' ReportItems
                writer.WriteEndElement() ' TableCell
            Next fieldName
            writer.WriteEndElement() ' TableCells
            writer.WriteEndElement() ' TableRow
            writer.WriteEndElement() ' TableRows
            writer.WriteEndElement() ' Header
        End If
    End Sub

    ''' <summary>
    ''' Se agregan las filas de la tabla con los valores correspondientes, en general son textbox que se agregan por fila
    ''' </summary>
    ''' <param name="writer"></param>
    ''' <param name="m_fields"></param>
    ''' <param name="tableName"></param>
    ''' <remarks></remarks>
    Protected Sub addDetailsRows(ByRef writer As XmlTextWriter, ByVal m_fields As ArrayList, ByVal tableName As String, ByVal style As orbeStyle)
        writer.WriteStartElement("Details")
        writer.WriteStartElement("TableRows")
        writer.WriteStartElement("TableRow")
        writer.WriteElementString("Height", "0.2in")
        writer.WriteStartElement("TableCells")
        For Each fieldName In m_fields
            writer.WriteStartElement("TableCell")
            writer.WriteStartElement("ReportItems")
            ' Textbox
            writer.WriteStartElement("Textbox")
            writer.WriteAttributeString("Name", Nothing, tableName + formatoNombre(fieldName))
            setStyle(writer, style)
            writer.WriteElementString("ZIndex", "2")
            writer.WriteElementString("Height", "0.2in")
            writer.WriteElementString("CanGrow", "true")
            writer.WriteElementString("CanShrink", "false")
            writer.WriteElementString("Value", "=Fields!" + formatoNombre(fieldName) + ".Value")
            writer.WriteEndElement() ' Textbox
            writer.WriteEndElement() ' ReportItems
            writer.WriteEndElement() ' TableCell
        Next fieldName
        ' End Details element and children   
        writer.WriteEndElement() ' TableCells
        writer.WriteEndElement() ' TableRow
        writer.WriteEndElement() ' TableRows
        writer.WriteEndElement() ' Details
    End Sub

    ''' <summary>
    ''' Indica el fin del reporte un textbox sencillo
    ''' </summary>
    ''' <param name="writer"></param>
    ''' <remarks></remarks>
    Protected Sub CrearFin(ByRef writer As XmlTextWriter)
        writer.WriteStartElement("Textbox")
        writer.WriteAttributeString("Name", Nothing, "Fin")
        writer.WriteElementString("ZIndex", "4")
        writer.WriteElementString("Height", "0.25in")
        Dim laAltura As String = Me.altura
        writer.WriteElementString("Top", obtenerAlturaEIncrementar(0.4))
        writer.WriteElementString("Width", "6in")
        writer.WriteElementString("Left", "1in")
        writer.WriteElementString("CanGrow", "true")
        writer.WriteElementString("CanShrink", "false")
        writer.WriteElementString("Value", "Fin de Reporte")
        writer.WriteStartElement("Style")
        writer.WriteElementString("TextAlign", "Center")
        writer.WriteElementString("FontWeight", "Bold")
        writer.WriteEndElement() ' Style       

        writer.WriteEndElement() ' Textbox
    End Sub

    ''' <summary>
    ''' Funcion que agrega el style segun lo establecido
    ''' </summary>
    ''' <param name="writer"></param>
    ''' <param name="style"></param>
    ''' <remarks></remarks>
    Protected Sub setStyle(ByRef writer As XmlTextWriter, ByVal style As orbeStyle)
        writer.WriteStartElement("Style")
        For Each p As System.Reflection.PropertyInfo In style.GetType().GetProperties()
            If p.CanRead Then
                Dim valor As String = GetPropertyByNamedValue(p.Name, style)
                If valor.Length > 0 And p.Name <> "BorderStyle" Then
                    writer.WriteElementString(p.Name, valor)
                ElseIf p.Name = "BorderStyle" And valor.Length > 0 Then
                    writer.WriteStartElement("BorderStyle")
                    writer.WriteElementString("Default", valor)
                    writer.WriteEndElement()
                End If
            End If
        Next
        writer.WriteEndElement() ' Style
    End Sub

    ''' <summary>
    ''' Obtiene el valor de cada una de las propiedades del style
    ''' </summary>
    ''' <param name="PropertyName"></param>
    ''' <param name="style"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetPropertyByNamedValue(ByVal PropertyName As String, ByVal style As orbeStyle) As String
        Dim userType As Type = style.GetType()
        Dim UserProp As System.Reflection.PropertyInfo = userType.GetProperty(PropertyName)
        Dim myValue As Object
        myValue = UserProp.GetValue(style, Nothing).ToString
        If PropertyName.ToLower.Contains("size") Then
            myValue &= "pt"
        End If
        Return myValue
    End Function

    ''' <summary>
    ''' Linea separa (hr)
    ''' </summary>
    ''' <param name="writer"></param>
    ''' <remarks></remarks>
    Protected Sub agregarLinea(ByRef writer As XmlTextWriter)
        writer.WriteStartElement("Line")
        writer.WriteAttributeString("Name", Nothing, "line1")
        writer.WriteElementString("ZIndex", "2")
        writer.WriteElementString("Height", "0in")
        Dim laAltura1 As String = Me.altura
        writer.WriteElementString("Top", obtenerAlturaEIncrementar(0.1))
        writer.WriteElementString("Width", "2in")
        writer.WriteElementString("Left", "3in")
        writer.WriteStartElement("Style")
        writer.WriteStartElement("BorderStyle")
        writer.WriteElementString("Default", "Solid")
        writer.WriteEndElement() ' border
        writer.WriteElementString("TextAlign", "Center")
        writer.WriteElementString("FontWeight", "Bold")
        writer.WriteEndElement() ' Style        
        writer.WriteEndElement() ' Line
    End Sub

    ''Funciones de Apoyo

    ''' <summary>
    ''' El id de los nombres de los items tienen una serie de restricciones, por ejemplo no pueden contener espacios o caracteres especiales
    ''' al igual que iniciar con algun numero.
    ''' </summary>
    ''' <param name="nombre">Nombre original del item</param>
    ''' <returns>Nombre valido para el item</returns>
    ''' <remarks></remarks>
    Protected Function formatoNombre(ByVal nombre As String) As String
        Dim esNumero As Boolean = False
        Try
            Dim numero As Integer = 0
            numero = nombre.Substring(0, 1)
            esNumero = True
        Catch ex As Exception

        End Try

        If esNumero Then
            nombre = "aa" & nombre
        End If
        nombre = nombre.Replace(" ", "")
        Return nombre
    End Function

    ''' <summary>
    ''' Los items se van insertando al reporte de arriba hacia abajo, por lo cual se obtiene la altura(top) en el reporte donde el iten actual se va a insertar para evitar 
    ''' que se coloque sobre algun otro item, y ademas
    ''' se actualiza la altura con un incremento para que el siguiente item que se desee agregar sepa el top donde va a ser insertado yasi sucesivamente.
    ''' </summary>
    ''' <param name="aumentar"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function obtenerAlturaEIncrementar(ByVal aumentar As Double) As String
        Dim laAltura As String = altura
        If laAltura.Contains(",") Then
            laAltura = laAltura.Replace(",", ".")
        End If
        laAltura &= "in"
        altura += aumentar
        Return laAltura
    End Function

    ''' <summary>
    ''' propiedad que lleva el control de la altura de los items que se van agregando al reporte
    ''' el primer item siempre se va insertar en la altura de 0.25in
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property altura() As Double
        Get
            If _altura = 0 Then
                _altura = 0.25
            End If
            Return _altura
        End Get
        Set(ByVal value As Double)
            _altura = value
        End Set
    End Property

    ''' <summary>
    ''' Indica el desplazamiento a la izquierda segun la alineacion horizontal establecida, sto se calcula a partir del ancho y el padding
    ''' </summary>
    ''' <param name="posicion"></param>
    ''' <param name="ancho"></param>
    ''' <param name="padding"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function horizontalAlignPosition(ByVal posicion As ReportList.posicionHorizontal, ByVal ancho As Double, ByVal padding As Double) As Double
        Dim tamano As Double = report.reportSize
        If posicion = ReportList.posicionHorizontal.Derecha Then
            tamano -= (ancho + padding)
            Return tamano
        ElseIf posicion = ReportList.posicionHorizontal.Centro Then
            tamano = tamano / 2
            tamano -= ancho / 2
            tamano += padding
            Return tamano
        End If
        Return posicion
    End Function

    ''' <summary>
    ''' Todas las medidas se indican en IN y tienen que estar separadas por un "."
    ''' </summary>
    ''' <param name="nueAltura"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function formatoMedidas(ByVal nueAltura As Double) As String
        Dim laAltura As String = nueAltura
        If laAltura.Contains(",") Then
            laAltura = laAltura.Replace(",", ".")
        End If
        laAltura &= "in"
        Return laAltura
    End Function

    Protected Function anchoColumna2DecimalPiso(ByVal numero As Double) As Double
        Dim punto As Integer = numero.ToString.IndexOf(",")
        Dim decimales As String = numero.ToString.Substring(punto + 1)
        If decimales.Length > 2 Then
            numero -= 0.01
        End If
        Return numero
    End Function







End Class
