Imports Orbelink.DBHandler
Imports Orbelink.Entity.Reservaciones
Imports Orbelink.Entity.Productos
Imports Orbelink.Entity.Facturas
Imports Orbelink.Entity.Encuestas
Imports Orbelink.Entity.Entidades
Imports System.IO

Partial Class Orbecatalog_Reportes
    Inherits PageBaseClass

    Const codigo_pantalla As String = "RP-01"
    Const level As Integer = 1

    Protected Sub Page_Load1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddScripts(level)
        'securityHandler.VerifyPantalla(codigo_pantalla, level)
        If Not IsPostBack Then
            'Dim respueta As New EncuestaPreguntaRespuesta
            If Not Request.QueryString("id_encuesta") Is Nothing Then
                Panel1.Visible = False
                mostrarEncuesta(Request.QueryString("id_encuesta"))
            Else
                cargarDatosUbicaciones()
                cargarDatosTipos()
            End If

        End If
    End Sub



    Protected Sub mostrarEncuesta(ByVal id_encuesta As Integer)
        'Dim built As New EncuestasBuilder(connection, securityHandler)
        'Dim lista2 As New ReportList
        'lista2.add("Creador: Investigacion y Desarrollo")
        'lista2.add("Motivo: Conocimiento Orbecatalog")
        'lista2.width = 3
        'lista2.verticalAlign = ReportList.posicionVertical.Encabezado
        'lista2.HorizontalAlign = ReportList.posicionHorizontal.Derecha

        'Dim reporte As New OrbeReport()
        'reporte.Titulo = nombreEncuesta(id_encuesta)
        'reporte.fecha = IIf(chb_fecha.Checked, True, False)
        'built.disenarReporteEncuesta(reporte, id_encuesta)
        'built.diseñarReporteCategoria(reporte, id_encuesta)
        'reporte.Listas.Add(lista2)
        'reporte.mostrarReporte(rv_reporte)
    End Sub

    Protected Function nombreEncuesta(ByVal id_encuesta As Integer) As String
        'Dim encuesta As New Encuesta
        'encuesta.N_Encuesta.ToSelect = True
        'encuesta.Id_Encuesta.Where.EqualCondition(id_encuesta)
        'Dim dataSet As New Data.DataSet
        'Dim query As String = queryBuilder.SelectQuery(encuesta)
        'dataSet = connection.executeSelect(query)
        'If dataSet.Tables.Count > 0 Then
        '    If dataSet.Tables(0).Rows.Count > 0 Then
        '        ObjectBuilder.CreateObject(dataSet.Tables(0), 0, encuesta)
        '        Return encuesta.N_Encuesta.Value
        '    End If
        'End If
        Return ""
    End Function

    Protected Sub prueba(ByVal dataSet As Data.DataSet)
        Dim lista2 As New ReportList
        lista2.add("Creador: Orbecatalog")
        lista2.add("Motivo: Pruebas")
        lista2.width = 3
        lista2.verticalAlign = ReportList.posicionVertical.Encabezado
        lista2.HorizontalAlign = ReportList.posicionHorizontal.Derecha

        Dim laTabla As New ReportTable(dataSet)
        Dim unDataSet As New Data.DataSet
        unDataSet.Tables.Add()
        unDataSet.Tables(0).Columns.Add("Pais")
        unDataSet.Tables(0).Columns.Add("Valor")
        unDataSet.Tables(0).Rows.Add("A", 5)
        unDataSet.Tables(0).Rows.Add("B", 10)
        unDataSet.Tables(0).Rows.Add("C", 15)
        Dim elChart As New ReportChart(unDataSet)
        elChart.type = ReportChart.TiposChart.Bar

        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                Dim reporte As New OrbeReport()
                reporte.Titulo = txb_elTitulo.Text
                reporte.fecha = IIf(chb_fecha.Checked, True, False)
                reporte.tables.Add(laTabla)
                reporte.charts.Add(elChart)
                reporte.Listas.Add(lista2)
                reporte.mostrarReporte(rv_reporte)
            End If
        End If
    End Sub

    Protected Sub cargarDatosUbicaciones()
        Dim ubicaciones As New Ubicacion
        ubicaciones.Id_ubicacion.ToSelect = True
        ubicaciones.Nombre.ToSelect = True
        Dim dataSet As New Data.DataSet
        Dim consulta As String = queryBuilder.SelectQuery(ubicaciones)
        dataSet = connection.executeSelect(consulta)
        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                ddl_Ubicaciones.DataSource = dataSet
                ddl_Ubicaciones.DataTextField = ubicaciones.Nombre.Name
                ddl_Ubicaciones.DataValueField = ubicaciones.Id_ubicacion.Name
                ddl_Ubicaciones.DataBind()
                ddl_Ubicaciones.Items.Add("Cualquiera")
                ddl_Ubicaciones.SelectedIndex = ddl_Ubicaciones.Items.Count - 1
            End If
        End If
    End Sub

    Protected Sub cargarDatosTipos()
        Dim tipo As New TipoEntidad
        tipo.Id_TipoEntidad.ToSelect = True
        tipo.Nombre.ToSelect = True
        Dim dataSet As New Data.DataSet
        Dim consulta As String = queryBuilder.SelectQuery(tipo)
        dataSet = connection.executeSelect(consulta)
        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                ddl_tiposEntidades.DataSource = dataSet
                ddl_tiposEntidades.DataTextField = tipo.Nombre.Name
                ddl_tiposEntidades.DataValueField = tipo.Id_TipoEntidad.Name
                ddl_tiposEntidades.DataBind()
                ddl_tiposEntidades.Items.Add("Cualquiera")
                ddl_tiposEntidades.SelectedIndex = ddl_tiposEntidades.Items.Count - 1
            End If
        End If
    End Sub

    Protected Sub btn_generar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_generar.Click


        Dim entidad As New Entidad
        Dim dataSet As New Data.DataSet

        If ChB_nombreCompleto.Checked Then
            entidad.NombreDisplay.ToSelect = True
            entidad.NombreDisplay.AsName = "Nombre"
        End If

        If chk_apellido1.Checked Then
            entidad.Apellido.ToSelect = True
            entidad.Apellido.AsName = "Apellido"
        End If

        If chk_apellido2.Checked Then
            entidad.Apellido2.ToSelect = True
            entidad.Apellido2.AsName = "Apellido2"
        End If

        If chk_celular.Checked Then
            entidad.Celular.ToSelect = True
        End If

        If chk_Descripcion.Checked Then
            entidad.Descripcion.ToSelect = True
            entidad.Descripcion.AsName = "Descripcion"
        End If

        If chk_Email.Checked Then
            entidad.Email.ToSelect = True
        End If

        If chk_identificacion.Checked Then
            entidad.Identificacion.ToSelect = True
        End If

        If chk_telefono.Checked Then
            entidad.Telefono.ToSelect = True
        End If

        If ddl_tiposEntidades.SelectedIndex < ddl_tiposEntidades.Items.Count - 1 Then
            Dim tipo As New TipoEntidad
            tipo.Nombre.ToSelect = True
            tipo.Id_TipoEntidad.Where.EqualCondition(ddl_tiposEntidades.SelectedValue)
            queryBuilder.Join.EqualCondition(tipo.Id_TipoEntidad, entidad.Id_tipoEntidad)
            queryBuilder.From.Add(tipo)
        End If

        If ddl_Ubicaciones.SelectedIndex < ddl_Ubicaciones.Items.Count - 1 Then
            Dim ubicacion As New Ubicacion
            ubicacion.Nombre.ToSelect = True
            ubicacion.Id_ubicacion.Where.EqualCondition(ddl_Ubicaciones.SelectedValue)
            queryBuilder.Join.EqualCondition(ubicacion.Id_ubicacion, entidad.Id_Ubicacion)
            queryBuilder.From.Add(ubicacion)
        End If

        Dim consulta As String = queryBuilder.SelectQuery(entidad)
        dataSet = connection.executeSelect(consulta)
        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                prueba(dataSet)
            End If
        End If
    End Sub
End Class
