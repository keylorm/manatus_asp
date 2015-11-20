
Partial Class _OrbeCatalog_Mailer
    Inherits MasterPage

    Const level As Integer = 1

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            css_Sizes.Href = Configuration.Config_WebsiteRoot & "orbeCatalog/" & css_Sizes.Href
            css_Classic.Href = css_Classic.Href.Replace("../", "")
            css_Classic.Href = Configuration.Config_WebsiteRoot & css_Classic.Href

            img_Orbecatalog.ImageUrl = img_Orbecatalog.ImageUrl.Replace("~/", "")
            img_Orbecatalog.ImageUrl = Configuration.Config_WebsiteRoot & img_Orbecatalog.ImageUrl

            lnk_Usuario.NavigateUrl = Configuration.Config_WebsiteRoot & "orbeCatalog/"
            lnk_Logout.NavigateUrl = Configuration.Config_WebsiteRoot & "orbeCatalog/Login.aspx"

            lnk_Panel.NavigateUrl = Configuration.Config_WebsiteRoot & "orbeCatalog/" & lnk_Panel.NavigateUrl
            lnk_Search.NavigateUrl = Configuration.Config_WebsiteRoot & "orbeCatalog/" & lnk_Search.NavigateUrl
            lnk_Ayuda.NavigateUrl = Configuration.Config_WebsiteRoot & "orbeCatalog/" & lnk_Ayuda.NavigateUrl

            Dim id_entidad As Integer = 0
            Dim id_Envio As Integer = 0
            If Not Request.QueryString("id_Entidad") Is Nothing Then
                id_entidad = Request.QueryString("id_Entidad")
            End If

            If Not Request.QueryString("id_Envio") Is Nothing Then
                id_Envio = Request.QueryString("id_Envio")
            End If

            If id_entidad <> 0 And id_Envio <> 0 Then
                img_Envio.ImageUrl = Configuration.Config_WebsiteRoot & "orbeCatalog/orbeNewsletter.aspx?id_Entidad=" & id_entidad & "&id_envio=" & id_Envio
                img_Envio.Visible = True
            Else
                img_Envio.Visible = False
            End If
        End If
    End Sub

End Class

