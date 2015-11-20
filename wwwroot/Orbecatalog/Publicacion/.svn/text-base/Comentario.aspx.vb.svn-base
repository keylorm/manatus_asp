Imports Orbelink.Entity.Comentarios
Imports Orbelink.Control.Comentarios
Imports Orbelink.Control.Publicaciones
Imports Orbelink.Orbecatalog6

Partial Class Orbecatalog_Publicacion_Comentario
    Inherits PageBaseClass

    Const codigo_pantalla As String = "PU-10"
    Const level As Integer = 2

    'Viewstate
    Dim _tempPublicacionActual As Integer
    Protected Property PublicacionActual() As Integer
        Get
            If _tempPublicacionActual <= 0 Then
                If ViewState("vs_PublicacionActual") IsNot Nothing Then
                    _tempPublicacionActual = ViewState("vs_PublicacionActual")
                Else
                    ViewState.Add("vs_PublicacionActual", 0)
                    _tempPublicacionActual = 0
                End If
            End If
            Return _tempPublicacionActual
        End Get
        Set(ByVal value As Integer)
            _tempPublicacionActual = value
            ViewState("vs_PublicacionActual") = _tempPublicacionActual
        End Set
    End Property

    'Pagina
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddScripts(level)
        securityHandler.VerifyPantalla(codigo_pantalla, level)

        If Not IsPostBack Then
            securityHandler.VerifyActions(btn_Salvar, Nothing, Nothing)
            'selectComentarios()

            If Request.Params("id_Publicacion") IsNot Nothing Then
                Me.PublicacionActual = Request.Params("id_Publicacion")
            Else
                If Request.Params("id_comentario") IsNot Nothing Then
                    id_Actual = Request.Params("id_comentario")
                Else
                    MyMaster.RedirectMe("Default.aspx", codigo_pantalla)
                End If
            End If
            loadInfo()
        End If
    End Sub

    Protected Sub ClearInfo(ByVal estado As Configuration.EstadoPantalla, ByVal clearInfo As Boolean)
        If clearInfo Then
            tbx_Comentario.Text = ""
            tbx_Titulo.Text = ""
            rate_Comentario.CurrentRating = 1
            lbl_FechaCreado.Text = ""
            id_Actual = 0
        End If

        Select Case estado
            Case Configuration.EstadoPantalla.NORMAL
                'btn_Modificar.Visible = False
                btn_Salvar.Visible = True
                'btn_Eliminar.Visible = False

                div_FechaIngreso.Visible = False
                div_EscritoPor.Visible = False
            Case Configuration.EstadoPantalla.CONSULTAR
                btn_Modificar.Visible = True
                btn_Salvar.Visible = False
                btn_Eliminar.Visible = True
                div_FechaIngreso.Visible = True
                div_EscritoPor.Visible = True
            Case Configuration.EstadoPantalla.MODIFICAR
                btn_Modificar.Visible = True
                btn_Salvar.Visible = False
                btn_Eliminar.Visible = True
                div_FechaIngreso.Visible = True
                div_EscritoPor.Visible = True
        End Select
    End Sub

    Private Sub loadInfo()
        If id_Actual > 0 Then
            Dim controladora As New ComentarioHandler(Configuration.Config_DefaultConnectionString)
            If loadComentario(controladora, id_Actual) Then
                ClearInfo(Configuration.EstadoPantalla.CONSULTAR, False)
            Else
                ConsultarUsuario(securityHandler.Usuario)
                ClearInfo(Configuration.EstadoPantalla.NORMAL, False)
                id_Actual = 0
            End If
        Else
            ConsultarUsuario(securityHandler.Usuario)
            ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
        End If
        upd_ContenidoPantalla.Update()
    End Sub

    'Comentario
    Protected Function loadComentario(ByVal laControladora As ComentarioHandler, ByVal id_Comentario As Integer) As Boolean
        Dim elComentario As Comentario = laControladora.ConsultarComentario(id_Comentario)
        If elComentario IsNot Nothing Then
            tbx_Comentario.Text = elComentario.Comentario.Value
            rate_Comentario.CurrentRating = elComentario.Rating.Value
            tbx_Titulo.Text = elComentario.Titulo.Value
            lbl_FechaCreado.Text = elComentario.Fecha.ValueLocalized
            ConsultarUsuario(elComentario.Id_Entidad.Value)
            Return True
        End If
        Return False
    End Function

    Protected Function updateComentario(ByVal laControladora As ComentarioHandler, ByVal id_Comentario As Integer) As Boolean
        Return laControladora.updateComentario(id_Comentario, tbx_Comentario.Text, rate_Comentario.CurrentRating)
    End Function

    Protected Function deleteComentario(ByVal laControladora As ComentarioHandler, ByVal id_Comentario As Integer) As Boolean
        Dim resultado As Boolean = laControladora.deleteComentario(id_Comentario)
        If resultado Then
            id_Actual = 0
            ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
            upd_ContenidoPantalla.Update()
        End If
        Return resultado
    End Function

    'Usuario
    Private Sub ConsultarUsuario(ByVal id_usuario As Integer)
        Dim controlEntidad As New Orbelink.Control.Entidades.EntidadHandler(Configuration.Config_DefaultConnectionString)
        lbl_EscritorPor.Text = controlEntidad.ConsultarNombreEntidad(id_usuario)
    End Sub

    'Eventos
    Protected Sub btn_Salvar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Salvar.Click
        If IsValid() Then
            Dim controladora As New ComentarioHandler(Configuration.Config_DefaultConnectionString)
            Dim controlPublicacion As New ControladoraComentario_Publicacion(Configuration.Config_DefaultConnectionString)

            Dim id_Insertado As Integer = controladora.CrearComentario(controlPublicacion, securityHandler.Usuario, tbx_Titulo.Text, tbx_Comentario.Text, rate_Comentario.CurrentRating, PublicacionActual)
            If id_Insertado > 0 Then
                MyMaster.MostrarMensaje("Comentario salvado", False)
                id_Actual = id_Insertado
                loadInfo()
            Else
                MyMaster.MostrarMensaje("Comentario no salvado", True)
            End If
        Else
            MyMaster.MostrarMensaje(Resources.Orbecatalog_Resources.CamposRequeridos, False)
        End If
    End Sub

    Protected Sub btn_Modificar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Modificar.Click
        If IsValid() Then
            Dim controladora As New ComentarioHandler(Configuration.Config_DefaultConnectionString)
            If updateComentario(controladora, id_Actual) Then
                MyMaster.MostrarMensaje("Comentario actualizado", False)
            Else
                MyMaster.MostrarMensaje("Comentario no actualizado", True)
            End If
        Else
            MyMaster.MostrarMensaje(Resources.Orbecatalog_Resources.CamposRequeridos, True)
        End If
    End Sub

    Protected Sub btn_Cancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Cancelar.Click
        loadInfo()
    End Sub

    Protected Sub btn_Eliminar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Eliminar.Click
        Dim controladora As New ComentarioHandler(Configuration.Config_DefaultConnectionString)
        If deleteComentario(controladora, id_Actual) Then
            MyMaster.MostrarMensaje("Comentario eliminado", False)
        Else
            MyMaster.MostrarMensaje("Comentario no eliminado", True)
        End If
    End Sub

End Class
