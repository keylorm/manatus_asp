Imports Orbelink.DBHandler
Imports Orbelink.Entity.Reservaciones
Imports Orbelink.Control
Imports Orbelink.Entity.Facturas
Imports Orbelink.Control.Facturas
Imports Orbelink.Control.Reservaciones

Partial Class exitosaTransaccion
    Inherits Orbelink.FrontEnd6.PageBaseClass

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.QueryString("id") Is Nothing Then
            errorRedirect()
        Else

            Dim controladora As New ControladorReservaciones(connection, Resources.Reservaciones_Resources.ResourceManager)
            Dim id_RESERVACION_descifrado As String = getIdDescifrado()
            id_RESERVACION_descifrado = id_RESERVACION_descifrado.Remove(0, 10)
            If id_RESERVACION_descifrado > 0 Then
                Dim micarrito As Integer = controladora.obtenerFactura(id_RESERVACION_descifrado)
                If exitoTransaccion(id_RESERVACION_descifrado) Then
                    exitoRedirect()
                Else
                    errorRedirect()
                End If
            Else
                errorTransaccion(id_RESERVACION_descifrado)
                errorRedirect()
            End If

        End If

    End Sub

    Protected Sub errorRedirect()
        If Orbelink.DBHandler.LanguageHandler.CurrentLanguage = Orbelink.DBHandler.LanguageHandler.Language.INGLES Then
            Response.Redirect("~/reservacion_en.aspx?exito=0")
        Else
            Response.Redirect("~/reservacion_sp.aspx?exito=0")
        End If

    End Sub
    Protected Sub exitoRedirect()
        If Orbelink.DBHandler.LanguageHandler.CurrentLanguage = Orbelink.DBHandler.LanguageHandler.Language.INGLES Then
            Response.Redirect("~/reservacion_en.aspx?exito=1")
        Else
            Response.Redirect("~/reservacion_sp.aspx?exito=1")
        End If
    End Sub

    Protected Function getIdDescifrado() As String
        Dim iddescifrado As String = ""
        Dim id As String = ""

        Dim a As Array = Request.RawUrl.Split("=")
        For i As Integer = 1 To a.Length - 1
            If a(i) = "" Then
                id += "="
            Else
                id += a(i)
            End If

        Next
        iddescifrado = crypto.DescifrarCadena(id)
        Return iddescifrado
    End Function

    Protected Function exitoTransaccion(ByVal Id_Reservacion As Integer) As Boolean

        Dim controladoraReservaciones As New Orbelink.Control.Reservaciones.ControladorReservaciones(connection, Resources.Reservaciones_Resources.ResourceManager)
        If controladoraReservaciones.NuevoEstadoParaReservacion(Id_Reservacion, controladoraReservaciones.BuscarEstadoReservado, 1, "Exito paypal") = ControladorReservaciones.Resultado_Code.OK Then
            controladoraReservaciones.Emails(Id_Reservacion)
            Return True
        Else
            controladoraReservaciones.Emails(Id_Reservacion)
            Return False
        End If
    End Function

    Protected Function errorTransaccion(ByVal Id_Reservacion As Integer) As Boolean
        Dim controladoraReservaciones As New Orbelink.Control.Reservaciones.ControladorReservaciones(connection, Resources.Reservaciones_Resources.ResourceManager)
        If controladoraReservaciones.NuevoEstadoParaReservacion(Id_Reservacion, controladoraReservaciones.BuscarEstadoCancelado(), 1, "Error Paypal") Then
            Return True
        Else
            controladoraReservaciones.Emails(Id_Reservacion)
            Return False
        End If
    End Function



End Class
