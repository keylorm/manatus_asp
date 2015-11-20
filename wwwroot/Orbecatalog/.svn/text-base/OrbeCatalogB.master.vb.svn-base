Imports Orbelink.DBHandler
Imports Orbelink.DateAndTime

Partial Class _Orbecatalog_OrbeCatalogB
    Inherits Orbelink.Orbecatalog6.MasterPageBaseClass

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        div_Mensajes.Controls.Clear()
        securityHandler.cargarMenu(mainMenu, submenu)
        securityHandler.cargarBusqueda(Me.dl_search, img_modulo.ClientID, Me.hf_codigo.ClientID)
        Me.img_flecha.Attributes.Add("onclick", "javascript:linkElementPosition('" & Me.img_modulo.ClientID & "','searchList','" & Me.img_flecha.ClientID & "')")
        literalMenu.Text = "<script>initMenu('" & mainMenu.ClientID & "','" & submenu.ClientID & "')</script>"
        securityHandler.VerifyUser(lnk_Usuario, lnk_Logout, 1)

        If Not IsPostBack Then
            'imagenesInicial()
            lbl_Language.Text = Orbelink.DBHandler.LanguageHandler.CurrentLanguageName
            'securityHandler.VerifyReference(lnk_Regresar, level, securityHandler.TipoEntidad)
            'securityHandler.VerifyAveliableScreens(mnu_Panel, level, "")

            Dim cod_pantalla As String = Session("Pantalla")
            If cod_pantalla IsNot Nothing Then
                Dim corteGuion As Integer = cod_pantalla.IndexOf("-"c)
                Dim codigo As String = cod_pantalla.Substring(0, corteGuion)
                If codigo = "OR" Or codigo = "MA" Then
                    imagenesInicial("CO")
                    img_moduloGrande.Visible = False
                    If cod_pantalla = "OR-06" Then
                        pnl_busqueda.Visible = False
                    End If
                Else
                    imagenesInicial(codigo)
                End If
            End If
        End If
        lbl_DiaHora.Text = DateHandler.ToLocalizedStringFromUtc(Date.UtcNow)
    End Sub

    Protected Sub imagenesInicial(ByVal codigo As String)
        Dim elNivel = Session("NivelPantalla")
        Dim direccion As String = ""
        For counter = 0 To elNivel - 2
            direccion &= "../"
        Next
        hf_codigo.Value = codigo
        img_modulo.ImageUrl = "~/orbecatalog/iconos/Modulos/" & codigo & "_small.JPG"
        img_moduloGrande.ImageUrl = direccion & "orbecatalog/iconos/Modulos/" & codigo & "_big.png"
    End Sub

    Protected Sub btn_Buscar_Clik(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_buscar.Click
        Me.Buscar(Me.hf_codigo.Value, tbx_Search.Text)
    End Sub

    Public Overrides Sub limpiarMensajes()
        div_Mensajes.Controls.Clear()
    End Sub

    Public Overrides Sub concatenarMensaje(ByVal mensaje As String, ByVal esError As Boolean)
        div_Mensajes.Controls.Add(generarLabelDeMensaje(mensaje, esError))
        If esError Then
            agregarRutinaError(mensaje)
        End If
    End Sub

    Public Overrides Sub MostrarMensaje(ByVal mensaje As String, ByVal esError As Boolean)
        div_Mensajes.Controls.Add(generarLabelDeMensaje(mensaje, esError))
        If esError Then
            agregarRutinaError(mensaje)
        End If
        Me.pop_Pnl_Mensaje.Show()
    End Sub

    Public Overrides Sub RedirectMe(ByVal urlDestino As String, ByVal codigoOrigen As String)
        Response.Redirect(urlDestino)
    End Sub

    Public Overrides ReadOnly Property TheScriptManager() As System.Web.UI.ScriptManager
        Get
            Return sman_AjaxManager
        End Get
    End Property

    Public Overrides Sub mostrarPopUp(ByVal direccion As String)
        pop_Pnl_Acciones.Show()
        Dim corteDatos As Integer = direccion.IndexOf("?"c)
        If corteDatos > 0 Then
            direccion &= "&popUp=true"
        Else
            direccion &= "?popUp=true"
        End If
        if_popAcciones.Attributes("src") = direccion
    End Sub

    Public Overrides Function obtenerIframeString(ByVal direccion As String) As String
        Dim corteDatos As Integer = direccion.IndexOf("?"c)
        If corteDatos > 0 Then
            direccion &= "&popUp=true"
        Else
            direccion &= "?popUp=true"
        End If
        Dim iframeString As String = "javascript:loadURLonIframe(this, '" & if_popAcciones.ClientID & "', '" & direccion & "');"
        Dim id As String = Me.pop_Pnl_Acciones.BehaviorID
        iframeString &= "showPopUp('" & id & "');"
        Return iframeString
    End Function
End Class

