Imports Orbelink.DBHandler
Imports Orbelink.Entity.Entidades

Partial Class emails
    Inherits Orbelink.FrontEnd6.PageBaseClass

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim id_entidad As Integer = 0
        Try
            id_entidad = Request.QueryString("id_entidad")
        Catch ex As Exception
            id_entidad = 0
        End Try
        If id_entidad <> 0 Then
            selectEntidad(id_entidad)
        End If

        Dim tipo As Integer = 0
        Try
            tipo = Request.QueryString("tipo")
        Catch ex As Exception
            tipo = 0
        End Try

        Select Case tipo
            Case 1 'CONTACTENOS
                pnl_contactenos.Visible = True
            Case 2 'REGISTRO
                pnl_registro.Visible = True
                lnk_activarCuenta.NavigateUrl = Configuration.Config_WebsiteRoot & "micuenta.aspx"
                lnk_activarCuenta.Text = Configuration.Config_WebsiteRoot & "micuenta.aspx"
                lnk_Envio.NavigateUrl = Configuration.Config_WebsiteRoot & "emails.aspx?tipo=" & tipo & "&id_entidad=" & id_entidad
                lnk_Envio.Text = Configuration.Config_WebsiteRoot & "emails.aspx"
                div_link.Visible = True
            Case 3 'ENVIAR A UN AMIGO
                pnl_enviarAmigo.Visible = True
            Case 4 'SOLICITAR MAS INFO
                pnl_solicitarInfo.Visible = True
            Case 5 'FACTURAS
                pnl_faltura.Visible = True

        End Select
      
    End Sub


    'REGISTRO 
    Protected Sub selectEntidad(ByVal id_entidad As Integer)
        Dim nombre As String = ""
        Dim email As String = ""
        Dim dataSet As Data.DataSet

        Dim entidad As New Entidad
        entidad.NombreDisplay.ToSelect = True
        entidad.Apellido.ToSelect = True
        entidad.UserName.ToSelect = True
        entidad.Password.ToSelect = True
        entidad.Id_entidad.Where.EqualCondition(id_entidad)

        queryBuilder.From.Add(entidad)

        Dim query As String = queryBuilder.RelationalSelectQuery
        dataSet = connection.executeSelect(query)
        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                ObjectBuilder.CreateObject(dataSet.Tables(0), 0, entidad)
                Me.lbl_usuario.Text = entidad.NombreDisplay.Value
                Me.lbl_email.Text = entidad.UserName.Value
                Me.lbl_password.Text = entidad.Password.Value
            End If
        End If
    End Sub


End Class
