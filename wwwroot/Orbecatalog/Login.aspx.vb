Imports Orbelink.DBHandler
Imports Orbelink.Entity.Entidades

Partial Class _Login
    Inherits PageBaseClass

    Const codigo_pantalla As String = "OR-06"
    Dim loginOk As Boolean
    Const level As Integer = 1

    'Pagina
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddScripts(level)
        Session("pantalla") = codigo_pantalla

        pnl_Recordar.Visible = False
        pnl_Login.Visible = True

        If Not IsPostBack Then
            Me.Title = Resources.Orbecatalog_Resources.Orbecatalog & " - " & Resources.Orbecatalog_Resources.Login
            LanguageHandler.LoadDDL(ddl_Languages, False)
            Orbelink.DateAndTime.DateHandler.CargarTimeZones(ddl_timezones)
        End If

        Dim action As String = Request.Params("action")
        If Not action Is Nothing Then
            If action = "out" Then
                securityHandler.LogOut()
                Response.Redirect("login.aspx")
            ElseIf action = "remember" Then
                pnl_Recordar.Visible = True
                pnl_Login.Visible = False
            End If
        Else
            loginOk = securityHandler.VerifyUser(lnk_Usuario1, Nothing, level)
            If loginOk Then
                resetCampos(Configuration.EstadoPantalla.LOGED)
            End If
        End If
    End Sub

    Protected Sub Page_LoadComplete(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.LoadComplete
        Dim action As String = Request.Params("action")
        loginOk = securityHandler.VerifyUser(lnk_Usuario1, Nothing, 0)
        If loginOk Then
            resetCampos(Configuration.EstadoPantalla.LOGED)
            'pnl_Login.Visible = False
            pnl_Recordar.Visible = False
        Else
            lnk_Usuario1.Visible = False
            resetCampos(Configuration.EstadoPantalla.NORMAL)
        End If
    End Sub

    Protected Sub resetCampos(ByVal estado As Configuration.EstadoPantalla)
        Select Case estado
            Case Configuration.EstadoPantalla.NORMAL
                btn_Login.Visible = True
                btn_Logout.Visible = False
                btn_Cancelar.Visible = True

                'lbl_Password.Visible = True
                tbx_Username.Visible = True
                tbx_Password.Visible = True
                tbx_Username.Text = ""
                tbx_Password.Text = ""
                lnk_Recordar.Visible = True
            Case Configuration.EstadoPantalla.LOGED
                btn_Login.Visible = False
                btn_Cancelar.Visible = False
                btn_Logout.Visible = True

                'lbl_Password.Visible = False
                tbx_Username.Visible = False
                tbx_Password.Visible = False
                lnk_Recordar.Visible = False
        End Select
    End Sub

    Protected Sub btn_Cancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Cancelar.Click
        'resetCampos(Configuration.EstadoPantalla.NORMAL)
        '
        ' 
        Response.Redirect("Login.aspx")
    End Sub

    Protected Sub btn_Eliminar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Logout.Click
        securityHandler.LogOut()
        pnl_Login.Visible = True
        pnl_Recordar.Visible = False
        Response.Redirect("Login.aspx")
    End Sub

    Protected Sub btn_Salvar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Login.Click
        loginOk = securityHandler.LogIn(tbx_Username.Text, tbx_Password.Text, ddl_timezones.SelectedValue, ddl_Languages.SelectedValue)
        If Not loginOk Then
            MyMaster.MostrarMensaje("Usuario o password invalidos.", False)
        Else
            Dim elLink As New HyperLink
            securityHandler.VerifyReference(elLink, level, securityHandler.Perfil)
            If elLink.NavigateUrl.Length > 0 Then
                Response.Redirect(elLink.NavigateUrl)
            Else
                Response.Redirect("Default.aspx")
            End If

        End If
    End Sub

    Protected Sub btn_Recordar_Click1(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_Recordar.Click
        Dim entidad As New Entidad
        Dim dataset As Data.DataSet
        If tbx_Email.Text.Length > 0 Then
            pnl_Recordar.Visible = False
            pnl_Login.Visible = True

            entidad.UserName.ToSelect = True
            entidad.Password.ToSelect = True
            entidad.Email.ToSelect = True
            entidad.NombreDisplay.ToSelect = True
            entidad.Email.Where.EqualCondition(tbx_Email.Text)

            dataset = connection.executeSelect(queryBuilder.SelectQuery(entidad))
            If dataset.Tables.Count > 0 Then
                If dataset.Tables(0).Rows.Count > 0 Then
                    ObjectBuilder.CreateObject(dataset.Tables(0), 0, entidad)

                    If entidad.UserName.IsValid And entidad.Password.IsValid Then
                        Dim sent As Boolean = Orbelink.Helpers.Mailer.SendPasswordRecovery(entidad.Email.Value, entidad.NombreDisplay.Value, _
                            entidad.UserName.Value, entidad.Password.Value, Configuration.Config_SiteName, Configuration.Config_WebsiteRoot & "orbecatalog/Login.aspx")

                        If sent Then
                            MyMaster.mostrarMensaje("Su informacion de usuario le sera enviada al correo.", False)
                        Else
                            MyMaster.mostrarMensaje("Error al enviar el correo.", False)
                        End If
                    Else
                        MyMaster.mostrarMensaje("No tiene usuario registrado.", False)
                    End If
                Else
                    MyMaster.mostrarMensaje("Email no está registrado.", False)
                End If
            End If
        Else
            MyMaster.mostrarMensaje("Debe escribir un correo.", False)
        End If
    End Sub
End Class
