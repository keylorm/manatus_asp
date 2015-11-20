
Partial Class Orbecatalog_popUp
    Inherits Orbelink.Orbecatalog6.MasterPageBaseClass

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        div_Mensajes.Controls.Clear()
    End Sub

    Public Overrides Sub limpiarMensajes()
        div_Mensajes.Controls.Clear()
    End Sub

    Public Overrides Sub concatenarMensaje(ByVal mensaje As String, ByVal esError As Boolean)
        div_Mensajes.Controls.Add(generarLabelDeMensaje(mensaje, esError))
    End Sub

    Public Overrides Sub MostrarMensaje(ByVal mensaje As String, ByVal esError As Boolean)
        div_Mensajes.Controls.Add(generarLabelDeMensaje(mensaje, esError))
        Me.pop_Pnl_Mensaje.Show()
    End Sub

    Public Overrides Sub RedirectMe(ByVal urlDestino As String, ByVal codigoOrigen As String)
        Dim corteDatos As Integer = urlDestino.IndexOf("?"c)
        If corteDatos > 0 Then
            urlDestino &= "&popUp=true"
        Else
            urlDestino &= "?popUp=true"
        End If
        urlDestino &= "&reference=" & codigoOrigen
        Response.Redirect(urlDestino)
    End Sub

    Public Overrides ReadOnly Property TheScriptManager() As System.Web.UI.ScriptManager
        Get
            Return sman_AjaxManager
        End Get
    End Property

    Public Overrides Sub mostrarPopUp(ByVal direccion As String)
        Me.RedirectMe(direccion, Session("codigoPop"))
    End Sub

    Public Overrides Function obtenerIframeString(ByVal direccion As String) As String
        Return ""
    End Function
End Class

