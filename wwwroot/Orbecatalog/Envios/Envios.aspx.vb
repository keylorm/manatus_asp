Imports Orbelink.DBHandler
Imports Orbelink.Entity.Envios
Imports Orbelink.Entity.Publicaciones
Imports Orbelink.Entity.Entidades

Partial Class _Envios
    Inherits PageBaseClass

    Const codigo_pantalla As String = "EV-02"
    Const level As Integer = 2

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddScripts(level)

        securityHandler.VerifyPantalla(codigo_pantalla, level)
        If Not IsPostBack Then
            dg_Envio.Attributes.Add("SelectedIndex", -1)
            dg_Envio.Attributes.Add("SelectedPage", -1)
            ClearInfo(Configuration.EstadoPantalla.NORMAL, False)
            securityHandler.VerifyActions(btn_Salvar, btn_Modificar, btn_Eliminar)

            pnl_Reporte.Visible = False

            selectEnvio()
            selectPublicaciones()
        End If
    End Sub

    Protected Sub ClearInfo(ByVal estado As Configuration.EstadoPantalla, ByVal clearInfo As Boolean)
        If clearInfo Then
            vld_Subject.IsValid = True
            tbx_SubjectEnvio.Text = ""
            tbx_SenderName.Text = ""
            tbx_SenderEmail.Text = ""
            id_Actual = 0
            tbx_ReplyTo.Text = ""
            tbx_upl_NombreEntidad.Text = ""
            tbx_upl_Email.Text = ""
            tbx_upl_NombreEntidad.ToolTip = ""
            Me.ClearDataGrid(dg_Envio, clearInfo)
        End If

        Select Case estado
            Case Configuration.EstadoPantalla.NORMAL
                btn_Modificar.Visible = False
                btn_Salvar.Visible = True
                btn_Eliminar.Visible = False

                pnl_ArchivoExcel.Visible = False
                tab_Publicaciones.Enabled = False
                tab_Lotes.Enabled = False
                tab_SubirExcel.Enabled = False
                tr_Entidad.Visible = False
            Case Configuration.EstadoPantalla.CONSULTAR
                btn_Modificar.Visible = True
                btn_Salvar.Visible = False
                btn_Eliminar.Visible = True

                pnl_ArchivoExcel.Visible = True
                tab_Publicaciones.Enabled = True
                tab_Lotes.Enabled = True
                tab_SubirExcel.Enabled = True
                tr_Entidad.Visible = True
        End Select
    End Sub

    'Envio
    Protected Sub selectEnvio()
        Dim dataSet As Data.DataSet
        Dim Envio As New Envio

        Envio.Id_Envio.ToSelect = True
        Envio.Subject.ToSelect = True
        Envio.SenderName.ToSelect = True
        Envio.TipoEnvio.Where.EqualCondition(Orbelink.Helpers.Mailer.TipoEnvio.ENVIO)

        queryBuilder.Orderby.Add(Envio.Subject)
        dataSet = connection.executeSelect(queryBuilder.SelectQuery(Envio))

        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                dg_Envio.DataSource = dataSet
                dg_Envio.DataKeyField = Envio.Id_Envio.Name
                dg_Envio.DataBind()

                'Llena el grid
                Dim offset As Integer = dg_Envio.CurrentPageIndex * dg_Envio.PageSize
                Dim result_Envio As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), Envio)
                For counter As Integer = 0 To dg_Envio.Items.Count - 1
                    Dim act_Envio As Envio = result_Envio(offset + counter)
                    Dim lbl_SubjectAtributo As Label = dg_Envio.Items(counter).FindControl("lbl_Subject")
                    'Dim lbl_Sender As Label = dg_Envio.Items(counter).FindControl("lbl_Sender")
                    lbl_SubjectAtributo.Text = act_Envio.Subject.Value
                    'lbl_Sender.Text = act_Envio.SenderName.Value

                    'Javascript
                    dg_Envio.Items(counter).Attributes.Add("onmouseover", "setRowFocus(this, " & counter & ", false)")
                    dg_Envio.Items(counter).Attributes.Add("onmouseout", "unsetRowFocus(this, " & counter & ", false)")
                Next
                lbl_ResultadoElementos.Text = contadorElementos(offset, dg_Envio.PageSize, dataSet.Tables(0).Rows.Count)
                dg_Envio.Visible = True
            Else
                lbl_ResultadoElementos.Text = contadorElementos(0, 0, 0)
                dg_Envio.Visible = False
            End If
        End If
    End Sub

    Protected Sub loadEnvio(ByVal id_Envio As Integer)
        Dim envio As New Envio
        Dim autoCompletar As Integer = 0
        Dim entidad As New Entidad

        entidad.NombreDisplay.ToSelect = True
        queryBuilder.Join.EqualCondition(entidad.Id_entidad, Envio.Id_Entidad)

        Envio.Fields.SelectAll()
        Envio.Id_Envio.Where.EqualCondition(id_Envio)

        queryBuilder.From.Add(envio)
        queryBuilder.From.Add(entidad)

        Dim resultTable As Data.DataTable = connection.executeSelect_DT(queryBuilder.RelationalSelectQuery)
        If resultTable.Rows.Count > 0 Then
            ObjectBuilder.CreateObject(resultTable, 0, envio)
            ObjectBuilder.CreateObject(resultTable, 0, entidad)

            tbx_SubjectEnvio.Text = envio.Subject.Value
            id_Actual = envio.Id_Envio.Value
            tbx_SenderName.Text = envio.SenderName.Value
            tbx_SenderEmail.Text = envio.SenderEmail.Value
            tbx_ReplyTo.Text = envio.ReplyTo.Value
            tbx_Saludo.Text = envio.Saludo.Value
            tbx_SMTPUsername.Text = envio.UserName.Value
            tbx_SMTPPassword.Text = envio.Password.Value
            tbx_ServerName.Text = envio.SMTPServer.Value

            lbl_Entidad.Text = entidad.NombreDisplay.Value
        End If
    End Sub

    Protected Function insertEnvio() As Boolean
        Dim Envio As Envio
        Try
            Envio = New Envio(securityHandler.Usuario, Date.UtcNow, tbx_ReplyTo.Text, tbx_SenderName.Text, tbx_SenderEmail.Text, tbx_SubjectEnvio.Text, tbx_SMTPUsername.Text, tbx_SMTPPassword.Text, tbx_ServerName.Text, " ", tbx_Saludo.Text, Orbelink.Helpers.Mailer.TipoEnvio.ENVIO)
            connection.executeInsert(queryBuilder.InsertQuery(Envio))
            Return True
        Catch exc As Exception
            MyMaster.MostrarMensaje(exc.Message, True)
            Return False
        End Try
    End Function

    Protected Function updateEnvio_ContenidoURL(ByVal id_Envio As Integer) As Boolean
        Try
            Dim contenidoURL As String = Configuration.Config_WebSite_LocalhostRoot & "mailer.aspx?id_Envio=" & id_Envio

            Dim Envio As Envio = New Envio()
            Envio.ContenidoURL.Value = contenidoURL
            Envio.Id_Envio.Where.EqualCondition(id_Envio)
            connection.executeUpdate(queryBuilder.UpdateQuery(Envio))
            Return True
        Catch exc As Exception
            MyMaster.MostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    Protected Function updateEnvio(ByVal id_Envio As Integer) As Boolean
        Dim Envio As Envio
        Try
            Envio = New Envio(securityHandler.Usuario, Date.UtcNow, tbx_ReplyTo.Text, tbx_SenderName.Text, tbx_SenderEmail.Text, tbx_SubjectEnvio.Text, tbx_SMTPUsername.Text, tbx_SMTPPassword.Text, tbx_ServerName.Text, " ", tbx_Saludo.Text, Orbelink.Helpers.Mailer.TipoEnvio.ENVIO)
            Envio.ContenidoURL.ToUpdate = False
            Envio.Id_Envio.Where.EqualCondition(id_Envio)
            connection.executeUpdate(queryBuilder.UpdateQuery(Envio))
            Return True
        Catch exc As Exception
            MyMaster.MostrarMensaje(exc.Message, True)
            Return False
        End Try
    End Function

    Protected Function deleteEnvio(ByVal id_Envio As Integer) As Boolean
        Dim Envio As New Envio
        Try
            Envio.Id_Envio.Where.EqualCondition(id_Envio)
            connection.executeDelete(queryBuilder.DeleteQuery(Envio))
            Return True
        Catch exc As Exception
            MyMaster.MostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    'Publicaciones
    Protected Sub selectPublicaciones()
        Dim dataSet As Data.DataSet
        Dim Publicacion As New Publicacion

        Publicacion.Id_Publicacion.ToSelect = True
        Publicacion.Titulo.ToSelect = True
        Publicacion.Aprobada.Where.EqualCondition(1)
        Publicacion.Visible.Where.EqualCondition(1)

        queryBuilder.From.Add(Publicacion)
        queryBuilder.Orderby.Add(Publicacion.Titulo)
        dataSet = connection.executeSelect(queryBuilder.RelationalSelectQuery)

        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                ddl_Publicaciones.DataSource = dataSet
                ddl_Publicaciones.DataValueField = Publicacion.Id_Publicacion.Name
                ddl_Publicaciones.DataTextField = Publicacion.Titulo.Name
                ddl_Publicaciones.DataBind()
            End If
        End If
        ddl_Publicaciones.Items.Add(Resources.Orbecatalog_Resources.Opcion_Todos)
        ddl_Publicaciones.SelectedIndex = ddl_Publicaciones.Items.Count - 1
    End Sub

    'Publicaciones_Envio
    Protected Sub selectPublicaciones_Envio(ByVal id_Envio As Integer)
        Dim dataSet As Data.DataSet
        Dim Publicaciones_Envio As New Publicaciones_Envio
        Dim Publicacion As New Publicacion

        Publicacion.Id_Publicacion.ToSelect = True
        Publicacion.Titulo.ToSelect = True
        Publicacion.Corta.ToSelect = True
        queryBuilder.Join.EqualCondition(Publicacion.Id_Publicacion, Publicaciones_Envio.Id_Publicacion)
        Publicacion.Aprobada.Where.EqualCondition(1)
        Publicacion.Visible.Where.EqualCondition(1)

        Publicaciones_Envio.Id_Envio.Where.EqualCondition(id_Envio)

        queryBuilder.From.Add(Publicacion)
        queryBuilder.From.Add(Publicaciones_Envio)
        queryBuilder.Orderby.Add(Publicaciones_Envio.Orden)
        dataSet = connection.executeSelect(queryBuilder.RelationalSelectQuery)

        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                lst_Publicaciones.DataSource = dataSet
                lst_Publicaciones.DataValueField = Publicacion.Id_Publicacion.Name
                lst_Publicaciones.DataTextField = Publicacion.Titulo.Name
                lst_Publicaciones.DataBind()
                dg_Lotes.Visible = True
            Else
                lst_Publicaciones.Items.Clear()
                dg_Lotes.Visible = False
            End If
        End If
    End Sub

    Protected Function insertPublicaciones_Envio(ByVal id_Publicacion As Integer, ByVal id_Envio As Integer) As Boolean
        Dim Publicaciones_Envio As New Publicaciones_Envio
        Try
            Publicaciones_Envio.Id_Publicacion.Value = id_Publicacion
            Publicaciones_Envio.Id_Envio.Value = id_Envio
            Publicaciones_Envio.Orden.Value = 0
            connection.executeInsert(queryBuilder.InsertQuery(Publicaciones_Envio))
            Return True
        Catch exc As Exception
            'MyMaster.mostrarMensaje(exc.Message, true)
            '
            Return False
        End Try
    End Function

    Protected Function deletePublicaciones_Envio(ByVal id_Envio As Integer, ByVal id_Publicacion As Integer) As Boolean
        Dim Publicaciones_Envio As New Publicaciones_Envio
        Try
            Publicaciones_Envio.Id_Envio.Where.EqualCondition(id_Envio)
            If id_Publicacion <> 0 Then
                Publicaciones_Envio.Id_Publicacion.Where.EqualCondition(id_Publicacion)
            End If
            connection.executeDelete(queryBuilder.DeleteQuery(Publicaciones_Envio))
            Return True
        Catch exc As Exception
            MyMaster.MostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    'Correos_Envio
    Protected Function insertCorreos_Envio(ByVal email As String, ByVal nombre As String, ByVal id_Envio As Integer, ByVal lote As Integer) As Boolean
        Dim Correos_Envio As New Correos_Envio
        Try
            Correos_Envio.Correo.Value = email
            Correos_Envio.Nombre.Value = nombre
            Correos_Envio.Id_Envio.Value = id_Envio
            Correos_Envio.Lote.Value = lote
            Correos_Envio.Fecha.ValueUtc = Date.UtcNow
            Correos_Envio.Status.Value = Orbelink.Helpers.Mailer.StatusEnvio.ESPERA
            connection.executeInsert(queryBuilder.InsertQuery(Correos_Envio))
            Return True
        Catch exc As Exception
            'MyMaster.mostrarMensaje(exc.Message, true)
            '
            Return False
        End Try
    End Function

    Protected Function updateCorreos_Envio(ByVal id_Envio As Integer, ByVal lote As Integer, ByVal hashValue As Integer, ByVal theStatusCondition As Integer) As Integer
        Dim Correos_Envio As New Correos_Envio
        Try
            If theStatusCondition > 0 Then
                Correos_Envio.Status.Where.EqualCondition(theStatusCondition)
            End If
            Correos_Envio.Fecha.ValueUtc = Date.UtcNow
            Correos_Envio.HashValue.Value = hashValue
            Correos_Envio.Status.Value = Orbelink.Helpers.Mailer.StatusEnvio.ESPERA
            Correos_Envio.Id_Envio.Where.EqualCondition(id_Envio)
            Correos_Envio.Lote.Where.EqualCondition(lote)

            Return connection.executeUpdate(queryBuilder.UpdateQuery(Correos_Envio))
        Catch exc As Exception
            MyMaster.MostrarMensaje(exc.Message, True)

            Return 0
        End Try
    End Function

    'Lotes
    Protected Sub selectLotes_Envio(ByVal id_Envio As Integer)
        Dim dataSet As Data.DataSet
        Dim Lotes_Envio As New Lotes_Envio

        Lotes_Envio.Id_Envio.Where.EqualCondition(id_Envio)
        Lotes_Envio.Fields.SelectAll()

        queryBuilder.From.Add(Lotes_Envio)
        queryBuilder.Orderby.Add(Lotes_Envio.Lote)
        dataSet = connection.executeSelect(queryBuilder.RelationalSelectQuery)

        ddl_Lotes.Items.Clear()

        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                dg_Lotes.DataSource = dataSet
                dg_Lotes.DataKeyField = Lotes_Envio.Lote.Name
                dg_Lotes.DataBind()

                ddl_Lotes.DataSource = dataSet
                ddl_Lotes.DataTextField = Lotes_Envio.Fecha_Creado.Name
                ddl_Lotes.DataValueField = Lotes_Envio.Lote.Name
                ddl_Lotes.DataBind()

                'Llena el grid
                Dim results_Lotes_Envio As ArrayList = ObjectBuilder.TransformDataTable(dataSet.Tables(0), Lotes_Envio)
                For counter As Integer = 0 To dg_Lotes.Items.Count - 1
                    Dim act_Lotes_Envio As Lotes_Envio = results_Lotes_Envio(counter)

                    Dim lbl_Fecha As Label = dg_Lotes.Items(counter).FindControl("lbl_Fecha")
                    Dim lbl_FechaEnviado As Label = dg_Lotes.Items(counter).FindControl("lbl_FechaEnviado")
                    Dim lbl_Total As Label = dg_Lotes.Items(counter).FindControl("lbl_Total")
                    Dim lnk_VerReporte As HyperLink = dg_Lotes.Items(counter).FindControl("lnk_VerReporte")
                    Dim ddl_EnviarA As DropDownList = dg_Lotes.Items(counter).FindControl("ddl_EnviarA")
                    Dim btn_EnviarAhora As Button = dg_Lotes.Items(counter).FindControl("btn_EnviarAhora")

                    lbl_Fecha.Text = act_Lotes_Envio.Fecha_Creado.ValueLocalized
                    lnk_VerReporte.NavigateUrl = "ReporteEnvio.aspx?id_Envio=" & id_Envio & "&lote=" & act_Lotes_Envio.Lote.Value
                    btn_EnviarAhora.Attributes.Add("index", counter)

                    'OJO: falta registra async button.
                    MyMaster.TheScriptManager.RegisterPostBackControl(btn_EnviarAhora)

                    If act_Lotes_Envio.Enviado.Value = "1" Then
                        lnk_VerReporte.Visible = True
                        selectStatusCorreo(ddl_EnviarA, True)
                        lbl_FechaEnviado.Text = act_Lotes_Envio.Fecha_Enviado.ValueLocalized
                    Else
                        lnk_VerReporte.Visible = False
                        selectStatusCorreo(ddl_EnviarA, False)
                        lbl_FechaEnviado.Text = "Sin enviar"
                    End If

                    'Javascript
                    'dg_Lotes.Items(counter).Attributes.Add("onmouseover", "setRowFocus(this, " & counter & ", false)")
                    'dg_Publicaciones_Envio.Items(counter).Attributes.Add("onmouseout", "unsetRowFocus(this, " & counter & ", false)")
                Next
                dg_Lotes.Visible = True
                lbl_NoLotes.Visible = False
            Else
                dg_Lotes.Visible = False
                lbl_NoLotes.Visible = True
            End If
        End If

        ddl_Lotes.Items.Add(" - Nuevo - ")
        ddl_Lotes.SelectedIndex = ddl_Lotes.Items.Count - 1
    End Sub

    Protected Function insertLotes_Envio(ByVal id_Envio As Integer, ByVal lote As Integer) As Boolean
        Dim Lotes_Envio As New Lotes_Envio
        Try
            Lotes_Envio.Id_Envio.Value = id_Envio
            Lotes_Envio.Lote.Value = lote
            Lotes_Envio.Fecha_Creado.ValueUtc = Date.UtcNow
            Lotes_Envio.Enviado.Value = 0
            connection.executeInsert(queryBuilder.InsertQuery(Lotes_Envio))
            Return True
        Catch exc As Exception
            'MyMaster.mostrarMensaje(exc.Message, true)
            '
            Return False
        End Try
    End Function

    Protected Function updateLotes_Envio(ByVal id_Envio As Integer, ByVal lote As Integer) As Boolean
        Dim Lotes_Envio As New Lotes_Envio
        Try
            Lotes_Envio.Id_Envio.Where.EqualCondition(id_Envio)
            Lotes_Envio.Lote.Where.EqualCondition(lote)

            Lotes_Envio.Fecha_Enviado.ValueUtc = Date.UtcNow
            Lotes_Envio.Enviado.Value = 1
            connection.executeUpdate(queryBuilder.UpdateQuery(Lotes_Envio))
            Return True
        Catch exc As Exception
            'MyMaster.mostrarMensaje(exc.Message, true)
            '
            Return False
        End Try
    End Function

    Protected Function deleteLotes_Envio(ByVal id_Envio As Integer, ByVal Lote As Integer) As Boolean
        Dim Lotes_Envio As New Lotes_Envio
        Try
            Lotes_Envio.Id_Envio.Where.EqualCondition(id_Envio)
            If Lote <> 0 Then
                Lotes_Envio.Lote.Where.EqualCondition(Lote)
            End If
            connection.executeDelete(queryBuilder.DeleteQuery(Lotes_Envio))
            Return True
        Catch exc As Exception
            MyMaster.MostrarMensaje(exc.Message, True)
            Return False
        End Try
    End Function

    'Leer del archivo excel
    Protected Function guardarCorreos(ByRef theRow As Data.DataRow, ByVal id_Envio As Integer, ByVal lote As Integer) As Boolean
        Dim entidad As New Entidad
        Dim indice As Integer
        Dim enviado As Boolean
        Try
            Integer.TryParse(tbx_upl_NombreEntidad.Text, indice)
            entidad.NombreEntidad.Value = theRow.Item(indice)

            If tbx_upl_Email.Text.Length > 0 Then
                Integer.TryParse(tbx_upl_Email.Text, indice)
                entidad.Email.Value = theRow.Item(indice)
            End If

            'Guarda Registro
            If insertCorreos_Envio(entidad.Email.Value, entidad.NombreEntidad.Value, id_Envio, lote) Then
                enviado = True
            Else
                enviado = False
            End If

        Catch exc As Exception
            'MyMaster.mostrarMensaje(exc.Message, true)
            '
        End Try
        Return enviado
    End Function

    'Enviar
    Protected Function ParaEnviar(ByVal id_Envio As Integer, ByVal indiceGrid As Integer) As Boolean
        Dim lote As Integer = dg_Lotes.DataKeys.Item(indiceGrid)
        Dim ddl_EnviarA As DropDownList = dg_Lotes.Items(indiceGrid).FindControl("ddl_EnviarA")
        Dim actualizados As Integer

        Dim randObj As New Random(New TimeSpan(Date.UtcNow.Hour, Date.UtcNow.Minute, Date.UtcNow.Second).TotalSeconds)
        Dim hashValue As Integer = randObj.Next(1000, 9999)

        If ddl_EnviarA.SelectedIndex = ddl_EnviarA.Items.Count - 1 Then
            'Actulizar todos para enviar
            actualizados = updateCorreos_Envio(id_Envio, lote, hashValue, 0)
        Else
            If ddl_EnviarA.SelectedIndex < ddl_EnviarA.Items.Count - 2 Then
                actualizados = updateCorreos_Envio(id_Envio, lote, hashValue, ddl_EnviarA.SelectedValue)
            End If
        End If

        If actualizados > 0 Then
            'Hacer el request al AsyncSender...
            Dim requestURL As String = "AsyncSender.aspx?id_Envio=" & id_Envio & "&lote=" & lote & "&hashValue=" & hashValue

            'Dim myHttpWebRequest As System.Net.HttpWebRequest = System.Net.HttpWebRequest.Create(Configuration.WebSite_LocalhostRoot & "orbecatalog/Envios/" & requestURL)
            'Dim myHttpWebResponse As System.Net.HttpWebResponse = myHttpWebRequest.GetResponse()

            'Dim receiveStream As System.IO.Stream = myHttpWebResponse.GetResponseStream()
            'Dim encode As Encoding = System.Text.Encoding.GetEncoding("utf-8")
            'Dim readStream As New System.IO.StreamReader(receiveStream, encode)
            'Dim theResponse As String = readStream.ReadToEnd()
            'readStream.Close()

            'myHttpWebResponse.Close()

            'div_Response.InnerHtml = theResponse

            Dim intervalScript As String = Orbelink.Helpers.JavaScript.Script_SetIntervalCallback(div_Response.ClientID, 3000, actualizados + 2, "updateElementWithRequest('" & div_Response.ClientID & "', '" & requestURL & "', true);", lnk_StopSending)
            Orbelink.Helpers.JavaScript.WriteScriptToLiteral(intervalScript, ltl_ScriptResponse)
        End If

        pnl_ArchivoExcel.Visible = False
        Return actualizados
    End Function

    Protected Sub selectStatusCorreo(ByVal theDropDownList As DropDownList, ByVal enviado As Boolean)
        theDropDownList.Items.Clear()

        theDropDownList.Items.Add("Enviados")
        theDropDownList.Items(0).Value = Orbelink.Helpers.Mailer.StatusEnvio.ENVIADO

        theDropDownList.Items.Add("Espera (Sin enviar)")
        theDropDownList.Items(1).Value = Orbelink.Helpers.Mailer.StatusEnvio.ESPERA

        theDropDownList.Items.Add("Rechazados")
        theDropDownList.Items(2).Value = Orbelink.Helpers.Mailer.StatusEnvio.RECHAZADO

        theDropDownList.Items.Add("Vistos")
        theDropDownList.Items(3).Value = Orbelink.Helpers.Mailer.StatusEnvio.VISTO

        theDropDownList.Items.Add("- Todos -")

        If enviado Then
            theDropDownList.SelectedIndex = theDropDownList.Items.Count - 1
            theDropDownList.Enabled = True
        Else
            theDropDownList.SelectedIndex = 1
            theDropDownList.Enabled = False
        End If

    End Sub

    'Eventos
    Protected Sub btn_Salvar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Salvar.Click
        If IsValid() Then
            If insertEnvio() Then
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Salvar, "Envio", False)
                ClearInfo(Configuration.EstadoPantalla.CONSULTAR, False)
                selectEnvio()

                Dim envio As New Envio
                Dim id_Envio As Integer = connection.lastKey(envio.TableName, envio.Id_Envio.Name)

                'Actualizar con el contenido url verdadero
                updateEnvio_ContenidoURL(id_Envio)

                loadEnvio(id_Envio)
                selectPublicaciones_Envio(id_Envio)

                If insertLotes_Envio(id_Envio, 0) Then
                    selectLotes_Envio(id_Envio)
                End If

                upd_PantallaContenido.Update()
            Else
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Salvar, "Envio", True)
            End If
        Else
            MyMaster.MostrarMensaje(Resources.Orbecatalog_Resources.CamposRequeridos, False)
        End If
        'upd_Contenido.Update()
        'upd_Busqueda.Update()
    End Sub

    Protected Sub btn_Modificar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Modificar.Click

        If IsValid() Then
            If updateEnvio(id_Actual) Then
                selectEnvio()
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Modificar, "Envio", False)
            Else
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Modificar, "Envio", True)
            End If
        Else
            MyMaster.MostrarMensaje(Resources.Orbecatalog_Resources.CamposRequeridos, False)
        End If
        'upd_Busqueda.Update()
    End Sub

    Protected Sub btn_Cancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Cancelar.Click
        ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
        'upd_Contenido.Update()
        'upd_Busqueda.Update()
    End Sub

    Protected Sub btn_Eliminar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Eliminar.Click
        If deleteEnvio(id_Actual) Then
            ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
            selectEnvio()
            MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Eliminar, "Envio", False)
        Else
            MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Eliminar, "Envio", True)
        End If
    End Sub

    Protected Sub dg_Envio_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dg_Envio.EditCommand
        ClearInfo(Configuration.EstadoPantalla.CONSULTAR, True)
        Me.EditCommandDataGrid(dg_Envio, e.Item.ItemIndex)
        Dim id_Envio As Integer = dg_Envio.DataKeys(e.Item.ItemIndex)
        loadEnvio(id_Envio)
        selectPublicaciones_Envio(id_Envio)
        selectLotes_Envio(id_Envio)
        'upd_Contenido.Update()
        'upd_Busqueda.Update()
    End Sub

    Protected Sub dg_Envio_PageIndexChanged(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridPageChangedEventArgs) Handles dg_Envio.PageIndexChanged
        dg_Envio.CurrentPageIndex = e.NewPageIndex
        selectEnvio()
        Me.PageIndexChange(dg_Envio)
        'upd_Busqueda.Update()
    End Sub

    Protected Sub btn_EnviarAhora_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim btn_EnviarAhora As Button = sender
        Dim indiceGrid As Integer = btn_EnviarAhora.Attributes("index")

        If ParaEnviar(id_Actual, indiceGrid) Then
            MyMaster.MostrarMensaje("Correos enviandose...", False)
        Else
            MyMaster.MostrarMensaje("No se pudo enviar a ningun correo.", False)
        End If
        pnl_Reporte.Visible = True
        'upd_Busqueda.Update()
    End Sub

    Protected Sub btn_SubirExcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_SubirExcel.Click
        Dim dataset As Data.DataSet = Orbelink.Helpers.CommonTasks.cargadelexcelDS(Me.upl_Archivo, tbx_upl_NombreHoja.Text)
        Dim guardados As Integer = 0

        Dim id_Envio As Integer = id_Actual
        Dim lote As Integer = 0
        If Not dataset Is Nothing Then
            If dataset.Tables.Count > 0 Then
                If dataset.Tables(0).Rows.Count > 0 Then

                    'Selecciona el lote a guardar
                    If ddl_Lotes.SelectedIndex = ddl_Lotes.Items.Count - 1 Then
                        If ddl_Lotes.Items.Count > 1 Then
                            lote = ddl_Lotes.Items(ddl_Lotes.Items.Count - 2).Value + 1
                        End If

                        If insertLotes_Envio(id_Envio, lote) Then
                            MyMaster.MostrarMensaje("Error al guardar lote.", False)
                        End If
                    Else
                        lote = ddl_Lotes.SelectedValue
                    End If

                    'Guarda los correos
                    For counter As Integer = 0 To dataset.Tables(0).Rows.Count - 1
                        Dim theRow As Data.DataRow = dataset.Tables(0).Rows(counter)
                        If guardarCorreos(theRow, id_Envio, lote) Then
                            guardados += 1
                        End If
                    Next
                    MyMaster.MostrarMensaje("Correos guardados exitosamente: " & guardados, False)

                    'Muestra el lote
                    MyMaster.concatenarMensaje("Lote de este envio: " & lote, False)

                Else
                    MyMaster.MostrarMensaje("Archivo de excel no valido.", True)
                End If
            End If
        Else
            MyMaster.MostrarMensaje("No existe archivo que subir.", True)
        End If
        selectLotes_Envio(id_Envio)
    End Sub

    Protected Sub btn_AgregarPublicacion_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_AgregarPublicacion.Click
        If ddl_Publicaciones.SelectedIndex < ddl_Publicaciones.Items.Count - 1 Then
            If insertPublicaciones_Envio(ddl_Publicaciones.SelectedValue, id_Actual) Then
                selectPublicaciones_Envio(id_Actual)
                MyMaster.MostrarMensaje("Publicacion agregada al envio exitosamente.", False)
                upd_Publicaciones.Update()
            Else
                MyMaster.MostrarMensaje("Error al agregar publicacion al envio.", True)
            End If
        Else
            MyMaster.MostrarMensaje("Seleccione una publicacion para agregar.", True)
        End If
    End Sub

    Protected Sub btn_QuitarPublicacion_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_QuitarPublicacion.Click
        If lst_Publicaciones.SelectedValue.Length > 0 Then
            If deletePublicaciones_Envio(id_Actual, lst_Publicaciones.SelectedValue) Then
                selectPublicaciones_Envio(id_Actual)
                MyMaster.MostrarMensaje("Entidad eliminada del grupo exitosamente.", False)
                upd_Publicaciones.Update()
            Else
                MyMaster.MostrarResultadoAccion(MasterPageBaseClass.Acciones.Eliminar, "entidad del grupo", True)
            End If
        Else
            MyMaster.MostrarMensaje("Debe seleccionar un grupo.", True)
        End If
    End Sub

    Protected Sub btn_Accion_Ayuda_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Accion_Ayuda.Click
        MyMaster.MostrarMensaje("Puede subir desde un archivo en excel los correos.", False)
        MyMaster.MostrarMensaje("Los campos de nombre y correo reciben el <b>indice basado en 0 (cero)</b> de las columnas en el archivo de excel<br />", False)
        MyMaster.MostrarMensaje("Se el check de enviar ahora esta activado, se enviaran los correos. De no ser asi los puede enviar desde la pantalla de envios seleccionando este lote.", False)
    End Sub
End Class
