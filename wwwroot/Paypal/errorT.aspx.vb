Imports Orbelink.DBHandler
Imports Orbelink.Control
Imports Orbelink.Control.Reservaciones

Partial Class errorT
    Inherits Orbelink.FrontEnd6.PageBaseClass

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.QueryString("id") Is Nothing Then
            errorRedirect()
        Else

            Dim id_RESERVACION_descifrado As String = getIdDescifrado()
            id_RESERVACION_descifrado = id_RESERVACION_descifrado.Remove(0, 10)
            Dim controladora As New ControladorReservaciones(connection, Resources.Reservaciones_Resources.ResourceManager)
            Dim micarrito As Integer = controladora.obtenerFactura(id_RESERVACION_descifrado)
            If micarrito > 0 And id_RESERVACION_descifrado > 0 Then
                controladora.NuevoEstadoParaReservacion(id_RESERVACION_descifrado, controladora.BuscarEstadoCancelado(), controladora.obtenerCliente(id_RESERVACION_descifrado), "Error en paypal") 'Estado Cancelado
                If controladora.Emails(id_RESERVACION_descifrado) Then
                    errorRedirect()
                End If
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
End Class


