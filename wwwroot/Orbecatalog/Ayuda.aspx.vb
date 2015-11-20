Imports Orbelink.DBHandler
Imports Orbelink.Entity.Entidades

Partial Class _TipoEntidad
    Inherits PageBaseClass

    Const codigo_pantalla As String = "OR-08"
    Const level As Integer = 1

    'Pagina

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddScripts(level)
        'securityHandler.VerifyPantalla(codigo_pantalla, level)
        If Not IsPostBack Then
            lbl_Entidad.Visible = False
            tbx_NombreAyuda.Visible = True

            btn_Modificar.Visible = False
            btn_Eliminar.Visible = False
            'securityHandler.VerifyActions(btn_Salvar, btn_Modificar, btn_Eliminar)
            selectEntidad(securityHandler.Entidad)
            ClearInfo(Configuration.EstadoPantalla.NORMAL, False)
        End If
    End Sub

    Protected Sub ClearInfo(ByVal estado As Configuration.EstadoPantalla, ByVal clearInfo As Boolean)
        If clearInfo Then
            vld_tbx_NombreAyuda.IsValid = True
            tbx_NombreAyuda.Text = ""
            tbx_EmailAyuda.Text = ""
            tbx_Comentario.Text = ""

        End If
        Select Case estado
            Case Configuration.EstadoPantalla.NORMAL
                btn_Modificar.Visible = False
                btn_Salvar.Visible = True
                btn_Eliminar.Visible = False

            Case Configuration.EstadoPantalla.CONSULTAR
                btn_Modificar.Visible = True
                btn_Salvar.Visible = False
                btn_Eliminar.Visible = True
        End Select
    End Sub

    'Entidad
    Sub selectEntidad(ByVal id_entidad As Integer)
        Dim entidad As New Entidad
        Dim dataset As Data.DataSet

        If id_entidad = 0 Then
            lbl_Entidad.Visible = False
            tbx_NombreAyuda.Visible = True
        Else
            entidad.NombreDisplay.ToSelect = True
            entidad.Id_entidad.Where.EqualCondition(id_entidad)
            dataset = connection.executeSelect(queryBuilder.SelectQuery(entidad))

            If dataset.Tables.Count > 0 Then
                If dataset.Tables(0).Rows.Count > 0 Then
                    ObjectBuilder.CreateObject(dataset.Tables(0), 0, entidad)
                    lbl_Entidad.Text = entidad.NombreDisplay.Value

                    lbl_Entidad.Visible = True
                    tbx_NombreAyuda.Visible = False
                End If
            End If
        End If
    End Sub

    'Eventos
    Protected Sub btn_Salvar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Salvar.Click

        If IsValid() Then
            Dim nombre As String = ""
            If tbx_NombreAyuda.Visible Then
                nombre = tbx_NombreAyuda.Text
            Else
                nombre = lbl_Entidad.Text
            End If

            Dim envidado As Boolean = Orbelink.Helpers.Mailer.SendOneMail(tbx_EmailAyuda.Text, nombre, "kvalenciano@orbelink.com", "Kenneth Valenciano", "Soporte Orbecatalog " & Configuration.Config_SiteName, tbx_Comentario.Text)
            If envidado Then
                MyMaster.MostrarMensaje("Comantario enviado exitosamente.", False)
            Else
                MyMaster.MostrarMensaje("Error al enviar su comentario.", False)
            End If
        Else
            MyMaster.MostrarMensaje(Resources.Orbecatalog_Resources.CamposRequeridos, False)
        End If
    End Sub

    Protected Sub btn_Cancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Cancelar.Click
        ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
    End Sub

End Class
