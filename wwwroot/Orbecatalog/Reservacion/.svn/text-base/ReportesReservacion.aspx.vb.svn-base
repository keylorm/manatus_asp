Imports Orbelink.DBHandler
Imports Orbelink.Entity.Reservaciones
Imports Orbelink.Entity.Productos
Imports Orbelink.Entity.Entidades
Imports Orbelink.Control.Reservaciones
Imports Orbelink.DateAndTime

Partial Class Orbecatalog_Reservacion_ReportesReservacion
    Inherits PageBaseClass

    Const codigo_pantalla As String = "RS-RP"
    Const level As Integer = 2

    Protected Sub Orbecatalog_Reservacion_ReportesReservacion_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddScripts(level)
        If Not IsPostBack Then
            securityHandler.VerifyPantalla(codigo_pantalla, level)
            CalendarDia.SelectedDate = DateHandler.ToLocalizedDateFromUtc(Date.UtcNow).Date
            txb_diaReporte.Text = DateHandler.ToLocalizedDateFromUtc(Date.UtcNow)
            reporteDia(CalendarDia.SelectedDate)
        End If
    End Sub

    Protected Sub reporteDia(ByVal fecha As Date)
        Dim dataTAbleEntrada As Data.DataTable = reservacionesEntrantes(fecha)
        Dim dataTAbleSalida As Data.DataTable = reservacionesSalientes(fecha)

        RW_dia.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local
        Dim rep As Microsoft.Reporting.WebForms.LocalReport = RW_dia.LocalReport
        rep.ReportPath = "Orbecatalog\Reservacion\reservacion.rdlc"
        rep.DataSources.Clear()
        Dim dataEntrada As New Microsoft.Reporting.WebForms.ReportDataSource()
        dataEntrada.Name = "reservaDatSet_DataTable1"
        dataEntrada.Value = dataTAbleEntrada
        rep.DataSources.Add(dataEntrada)
        Dim dataSalida As New Microsoft.Reporting.WebForms.ReportDataSource()
        dataSalida.Name = "reservaDatSet_DataTable2"
        dataSalida.Value = dataTAbleSalida
        rep.DataSources.Add(dataSalida)
    End Sub

    Protected Function reservacionesEntrantes(ByVal fecha As Date) As Data.DataTable
        Dim item As New Item
        Dim reservacion As New Reservacion
        Dim detalle As New Detalle_Reservacion
        Dim entidad As New Entidad
        Dim controladora As New ControladorReservaciones(connection, Resources.Reservaciones_Resources.ResourceManager)
        Dim dataTAble As New Data.DataTable
        queryBuilder.Join.EqualCondition(reservacion.Id_Reservacion, detalle.id_reservacion)
        queryBuilder.Join.EqualCondition(reservacion.id_Cliente, entidad.Id_entidad)
        queryBuilder.Join.EqualCondition(detalle.ordinal, item.ordinal)

        Dim rangoInicio As New Date(fecha.Year, fecha.Month, fecha.Day, ControladorReservaciones.Config_HoraEntrada.Hour, ControladorReservaciones.Config_HoraEntrada.Minute, 0)
        Dim rangoFin As New Date(fecha.Year, fecha.Month, fecha.Day, ControladorReservaciones.Config_HoraSalida.Hour, ControladorReservaciones.Config_HoraSalida.Minute, 0)
        rangoFin = rangoFin.AddDays(1)
        reservacion.fecha_inicioProgramado.Where.GreaterThanOrEqualCondition(rangoInicio)
        reservacion.fecha_inicioProgramado.Where.LessThanCondition(rangoFin)

        Dim condicion As Condition = reservacion.Id_TipoEstado.Where.EqualCondition(controladora.BuscarEstadoReservado)
        Dim condicion2 As Condition = reservacion.Id_TipoEstado.Where.EqualCondition(controladora.BuscaEstadoBloqueado)
        queryBuilder.GroupConditions(condicion, condicion2, Where.FieldRelations.OR_)

        item.descripcion.ToSelect = True
        item.descripcion.AsName = "descripcion"
        entidad.NombreDisplay.ToSelect = True
        reservacion.fecha_inicioProgramado.ToSelect = True
        reservacion.fecha_finalProgramado.ToSelect = True
        detalle.Adultos.ToSelect = True
        detalle.Ninos.ToSelect = True
        queryBuilder.From.Add(item)
        queryBuilder.From.Add(reservacion)
        queryBuilder.From.Add(detalle)
        queryBuilder.From.Add(entidad)
        queryBuilder.Orderby.Add(item.ordinal)
        Dim consulta As String = queryBuilder.RelationalSelectQuery
        dataTAble = connection.executeSelect_DT(consulta)
        Return dataTAble
    End Function

    Protected Function reservacionesSalientes(ByVal fecha As Date) As Data.DataTable
        Dim item As New Item
        Dim reservacion As New Reservacion
        Dim detalle As New Detalle_Reservacion
        Dim entidad As New Entidad
        Dim controladora As New ControladorReservaciones(connection, Resources.Reservaciones_Resources.ResourceManager)
        Dim dataTAble As New Data.DataTable

        queryBuilder.Join.EqualCondition(reservacion.Id_Reservacion, detalle.id_reservacion)
        queryBuilder.Join.EqualCondition(reservacion.id_Cliente, entidad.Id_entidad)
        queryBuilder.Join.EqualCondition(detalle.ordinal, item.ordinal)

        Dim rangoInicio As New Date(fecha.Year, fecha.Month, fecha.Day, ControladorReservaciones.Config_HoraSalida.Hour, ControladorReservaciones.Config_HoraSalida.Minute, 0)
        Dim rangoFin As New Date(fecha.Year, fecha.Month, fecha.Day, ControladorReservaciones.Config_HoraSalida.Hour, ControladorReservaciones.Config_HoraSalida.Minute, 0)
        rangoFin = rangoFin.AddDays(1)
        reservacion.fecha_finalProgramado.Where.GreaterThanOrEqualCondition(rangoInicio)
        reservacion.fecha_finalProgramado.Where.LessThanCondition(rangoFin)

        reservacion.Id_TipoEstado.Where.EqualCondition(controladora.BuscarEstadoReservado)
        item.descripcion.ToSelect = True
        item.descripcion.AsName = "descripcion"
        entidad.NombreDisplay.ToSelect = True
        queryBuilder.From.Add(item)
        queryBuilder.From.Add(reservacion)
        queryBuilder.From.Add(detalle)
        queryBuilder.From.Add(entidad)
        queryBuilder.Orderby.Add(item.ordinal)
        Dim consulta As String = queryBuilder.RelationalSelectQuery
        dataTAble = connection.executeSelect_DT(consulta)
        Return dataTAble
    End Function

    Protected Sub btn_verReporte_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_verReporte.Click
        reporteDia(CalendarDia.SelectedDate)
    End Sub
End Class
