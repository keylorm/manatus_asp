Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
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

' Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente.
' <System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class FechasNoDisponibles
    Inherits System.Web.Services.WebService

    Dim connection As New SQLServer("192.163.216.119", "manatus_keylor", "manatus_keylor_user", "robot")
    Dim queryBuilder As QueryBuilder

    <WebMethod()> _
    Public Function GetDates(ByVal month As String, ByVal year As String, ByVal lang As String) As String
        'Llamado e instancia del objeto JSON que se utilizara para serializar las fechas
        Dim mydate As New JSON_result

        mydate.fechas = ObtenerFechas(month, year, lang)
        Dim json = JsonConvert.SerializeObject(mydate)
        Return json

    End Function
    'Logica para extraer las fechas no disponibles
    Function ObtenerFechas(ByVal month As String, ByVal year As String, ByVal lang As String) As String()
        'Obtener los valores (mes y ano) por medio del query string
        Dim CurrentYear As Integer = Date.UtcNow.Year
        Dim CurrentMonth As Integer = Date.UtcNow.Month

        If month <> "" Then
            CurrentMonth = month
        End If

        If year <> "" Then
            CurrentYear = year
        End If

        Return LLenarCalendario(CurrentYear, lang, CurrentMonth)
    End Function
    'Funcion que permite llenar un array que posteriormente se va a convertir en un objeto JSON
    Function LLenarCalendario(ByVal ano As Integer, ByVal lang As String, Optional ByVal elMes As Integer = -1) As String()

        Dim formatoFecha = "dd-MM-yyyy"

        If lang = "en" Then
            formatoFecha = "MM-dd-yyyy"
        End If

        'arreglo que permite almacenar las fechas que no estan disponibles para reservar
        Dim arrayFechas() As String = {}

        'Declaracion de variables
        Dim fechaLocalizada As Date
        'proyeccion de mes y año
        Dim anoP = ano
        Dim mesP = elMes
        Dim dias As Integer = 0
        'Proyeccion establecida para el mes actual y los proximos 11 meses
        Dim P = 12
        Dim i As Integer = 0

        For c = 1 To P
            'ciclo que permite extraer el mes y año que el usuario selecciono, pero tambien una proyeccion a 12 meses
            If mesP > -1 And anoP > -1 Then
                'Vlidador de proyeccion de fecha
                If (anoP = ano) And (mesP = elMes) Then
                    fechaLocalizada = New Date(anoP, mesP, 1)
                    mesP += 1
                Else
                    If mesP > P Then
                        'actualizo mes y año a inicio del año
                        mesP = 1
                        anoP += 1
                    Else
                        mesP += 1
                    End If
                    fechaLocalizada = New Date(anoP, mesP, 1)
                End If
                
            End If
            'Dias
            dias = Date.DaysInMonth(fechaLocalizada.Year, fechaLocalizada.Month)

            For counter As Integer = 0 To dias - 1
                Dim fechaActual As Date = New Date(fechaLocalizada.Year, fechaLocalizada.Month, counter + 1)
                If buscarReservaciones(fechaActual.ToUniversalTime) Then
                    ReDim Preserve arrayFechas(i)
                    arrayFechas(i) = CType(fechaActual.ToUniversalTime.ToString(formatoFecha), String)
                    i += 1
                End If
            Next
        Next

        Return arrayFechas
    End Function
    'Funcion que permite verificar las reservaciones realizadas en la fecha actual (misma para de inicio y final)
    Protected Function buscarReservaciones(ByVal fechaUTC As Date) As Boolean
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
        'connection.
        Dim dataTable As Data.DataTable = connection.executeSelect_DT(consulta)
        If dataTable.Rows.Count = 12 Then
            verificador = True
        End If
        Return verificador
    End Function

End Class

Public Class JSON_result
    Public fechas() As String
End Class