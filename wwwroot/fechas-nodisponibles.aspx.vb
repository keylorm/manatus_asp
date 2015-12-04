Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports Orbelink.Entity.Entidades
Imports Orbelink.Entity.Productos
Imports Orbelink.Entity.Reservaciones
Imports Orbelink.Entity.Facturas
Imports Orbelink.DBHandler
Imports Orbelink.Control.Reservaciones
Imports Orbelink.Control.Facturas
Imports Orbelink.DateAndTime.DateHandler
Imports Orbelink.DateAndTime

Public Class JSON_result
    Public fechas() As String
End Class

Partial Class fechas_nodisponibles
    Inherits Orbelink.FrontEnd6.PageBaseClass

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        'Llamado e instancia del objeto JSON que se utilizara para serializar las fechas
        Dim mydate As New JSON_result
        If Not IsPostBack Then
            mydate.fechas = ObtenerFechas()
            Dim json = JsonConvert.SerializeObject(mydate)
            form1.InnerText = json
        End If
    End Sub
    'Logica para extraer las fechas no disponibles
    Function ObtenerFechas() As String()
        'Obtener los valores (mes y ano) por medio del query string
        Dim Year As Integer = Date.UtcNow.Year
        Dim Month As Integer = Date.UtcNow.Month

        If Request.QueryString("month") <> "" Then
            Month = Request.QueryString("month")
        End If

        If Request.QueryString("year") <> "" Then
            Year = Request.QueryString("year")
        End If

        Return LLenarCalendario(Year, Month)
    End Function
    'Funcion que permite llenar un array que posteriormente se va a convertir en un objeto JSON
    Function LLenarCalendario(ByVal ano As Integer, Optional ByVal elMes As Integer = -1) As String()

        Dim formatoFecha = "dd-MM-yy"

        If Request.QueryString("lang") = "en" Then
            formatoFecha = "MM-dd-yy"
        End If

        Dim arrayFechas() As String = {}
        Dim fechaLocalizada As Date
        If elMes > -1 And ano > -1 Then
            fechaLocalizada = New Date(ano, elMes, 1)
        End If

        Dim dias As Integer = Date.DaysInMonth(fechaLocalizada.Year, fechaLocalizada.Month)

        'Dias
        Dim i As Integer = 0
        For counter As Integer = 0 To dias - 1
            Dim fechaActual As Date = New Date(fechaLocalizada.Year, fechaLocalizada.Month, counter + 1)
            If buscarReservaciones(fechaActual.ToUniversalTime) Then
                ReDim Preserve arrayFechas(i)
                arrayFechas(i) = CType(fechaActual.ToUniversalTime.ToString(formatoFecha), String)
                i += 1
            End If
        Next

        Return arrayFechas
    End Function
    'Funcion que permite verificar las reservaciones realizadas en la fecha actual (misma para de inicio y final)
    Function buscarReservaciones(ByVal fechaUTC As Date) As Boolean
        'permite definir si todos los rooms estan reservados
        Dim verificador = False

        Dim reservacion As New Reservacion
        Dim detalle As New Detalle_Reservacion
        Dim query As New QueryBuilder()
        Dim tipoEstado As New TipoEstadoReservacion
        Dim fechaInicio As New Date(fechaUTC.Year, fechaUTC.Month, fechaUTC.Day, ControladorReservaciones.Config_HoraEntrada.Hour, ControladorReservaciones.Config_HoraEntrada.Minute, 0)
        Dim fechaFinal As New Date(fechaUTC.Year, fechaUTC.Month, fechaUTC.Day, ControladorReservaciones.Config_HoraSalida.Hour, ControladorReservaciones.Config_HoraSalida.Minute, 0)
        reservacion.fecha_inicioProgramado.Where.LessThanOrEqualCondition(fechaInicio.ToUniversalTime)
        reservacion.fecha_finalProgramado.Where.GreaterThanCondition(fechaFinal.ToUniversalTime)
        reservacion.Id_Reservacion.ToSelect = True
        detalle.ordinal.ToSelect = True

        tipoEstado.RepresentaReservado.Where.EqualCondition(1)

        query.Join.EqualCondition(reservacion.Id_TipoEstado, tipoEstado.Id_TipoEstadoReservacion)
        query.Join.EqualCondition(detalle.id_reservacion, reservacion.Id_Reservacion)

        query.From.Add(reservacion)
        query.From.Add(detalle)
        query.From.Add(tipoEstado)

        Dim consulta As String = query.RelationalSelectQuery

        Dim dataTable As Data.DataTable = connection.executeSelect_DT(consulta)
        If dataTable.Rows.Count = 12 Then
            verificador = True
        End If
        Return verificador
    End Function

End Class
