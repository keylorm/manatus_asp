Imports Orbelink.DBHandler
Imports System.IO
Imports Orbelink.Entity.Envios
Imports Orbelink.Entity.Entidades

Partial Class _Envio_Imagen
    Inherits PageBaseClass

    Const codigo_pantalla As String = "EV-05"
    Const level As Integer = 2

    'Pagina
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddScripts(level)

        securityHandler.VerifyPantalla(codigo_pantalla, level)
        If Not IsPostBack Then
            ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
            pnl_Reporte.Visible = False
        End If
    End Sub

    Protected Sub ClearInfo(ByVal estado As Configuration.EstadoPantalla, ByVal clearInfo As Boolean)
        If clearInfo Then
            tbx_upl_NombreEntidad.Text = ""
            tbx_upl_NombreEntidad.Text = ""
            tbx_upl_Apellido.Text = ""
            tbx_upl_Apellido2.Text = ""
            tbx_upl_Email.Text = ""
            tbx_upl_NombreEntidad.ToolTip = ""
        End If

        Select Case estado
            Case Configuration.EstadoPantalla.NORMAL
                btn_Salvar.Visible = True

            Case Configuration.EstadoPantalla.CONSULTAR
                btn_Salvar.Visible = False

        End Select
    End Sub

    'Envio
    Protected Function insertEnvio(ByVal elContenidoURL As String) As Boolean
        Dim Envio As Envio
        Dim id_Entidad As Integer
        Try
            If securityHandler.Entidad > 0 Then
                id_Entidad = securityHandler.Entidad
            Else
                id_Entidad = 1
            End If
            Envio = New Envio(id_Entidad, Date.UtcNow, tbx_ReplyTo.Text, tbx_SenderName.Text, tbx_SenderEmail.Text, tbx_SubjectEnvio.Text, tbx_SMTPUsername.Text, Me.tbx_SMTPPassword.Text, Me.tbx_ServerName.Text, elContenidoURL, "", Orbelink.Helpers.Mailer.TipoEnvio.IMAGEN)
            connection.executeInsert(queryBuilder.InsertQuery(Envio))
            Return True
        Catch exc As Exception
            MyMaster.MostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

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
            MyMaster.MostrarMensaje(exc.Message, True)

            Return False
        End Try
    End Function

    'Correos_Envio
    Protected Function insertCorreos_Envio(ByVal email As String, ByVal nombre As String, ByVal id_Envio As Integer, ByVal lote As Integer, ByVal hashValue As Integer) As Boolean
        Dim Correos_Envio As New Correos_Envio
        Try
            Correos_Envio.Correo.Value = email
            Correos_Envio.Nombre.Value = nombre
            Correos_Envio.Id_Envio.Value = id_Envio
            Correos_Envio.Lote.Value = lote
            Correos_Envio.Fecha.ValueUtc = Date.UtcNow
            Correos_Envio.Status.Value = Orbelink.Helpers.Mailer.StatusEnvio.ESPERA
            Correos_Envio.HashValue.Value = hashValue
            connection.executeInsert(queryBuilder.InsertQuery(Correos_Envio))
            Return True
        Catch exc As Exception
            MyMaster.MostrarMensaje(exc.Message, True)
            Return False
        End Try
    End Function

    'Archivo HTML
    Protected Function salvarContenidoHTML() As String
        Dim imagePath As String = ""
        Dim imageURL As String = ""
        Dim fileURL As String = ""
        Dim fileLocalURL As String = ""
        Dim filePath As String = ""

        Dim theDate As String = Date.UtcNow
        Dim newFileName As String = theDate.Replace(" ", "_")
        newFileName = newFileName.Replace("/", "-")
        newFileName = newFileName.Replace(":", "-")

        If upl_ImagenCorreo.HasFile Then
            Dim filename As String = upl_ImagenCorreo.FileName
            Dim extension As String = filename.Substring(filename.LastIndexOf("."))

            System.IO.Directory.CreateDirectory(Configuration.logsPath & "Mailer\")
            imagePath = Configuration.logsPath & "Mailer\" & newFileName & extension
            imageURL = Configuration.Config_WebsiteRoot & "Mailer/" & newFileName & extension
            upl_ImagenCorreo.SaveAs(imagePath)

            filePath = Configuration.rootPath & "Mailer\" & newFileName & ".html"
            fileLocalURL = Configuration.Config_WebSite_LocalhostRoot & "Mailer/" & newFileName & ".html"
            fileURL = Configuration.Config_WebsiteRoot & "Mailer/" & newFileName & ".html"

            crearArchivoHTML(filePath, fileURL, tbx_SubjectEnvio.Text, imageURL)
        End If

        Return fileLocalURL
    End Function

    Protected Sub crearArchivoHTML(ByVal theFilePath As String, ByVal theFileURL As String, ByVal theTitle As String, ByVal theImageURL As String)
        Dim fileWriter As New StreamWriter(theFilePath, True)

        fileWriter.WriteLine("<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Strict//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd"">")
        fileWriter.WriteLine("<html xmlns=""http://www.w3.org/1999/xhtml"">")
        fileWriter.WriteLine("<head><title>" & theTitle & "</title></head>")
        fileWriter.WriteLine("<body><div align=""center"">")
        fileWriter.WriteLine("<img src=""" & theImageURL & """ /><br />")
        fileWriter.WriteLine("Si no puede ver este mensaje <a href=""" & theFileURL & """>haga click aqui</a><br />")
        fileWriter.WriteLine("Tambien puede copiar en su navegador la direccion <a href=""" & theFileURL & """>" & theFileURL & "</a>")
        fileWriter.WriteLine("</div></body></html>")

        fileWriter.Close()
    End Sub

    'Correos desde excel
    Protected Function guardarCorreosEnvio(ByVal id_Envio As Integer, ByVal lote As Integer, ByVal hashValue As Integer) As Integer
        Dim dataset As Data.DataSet = cargadelexcelDS(tbx_upl_NombreHoja.Text)
        Dim guardados As Integer = 0

        If dataset.Tables.Count > 0 Then
            If dataset.Tables(0).Rows.Count > 0 Then
                For counter As Integer = 0 To dataset.Tables(0).Rows.Count - 1
                    Dim theRow As Data.DataRow = dataset.Tables(0).Rows(counter)
                    If salvarCorreoDesdeRow(theRow, id_Envio, lote, hashValue) Then
                        guardados += 1
                    End If
                Next
            Else
                MyMaster.concatenarMensaje("Archivo de excel no valido.", False)
            End If
        Else
            MyMaster.concatenarMensaje("Archivo de excel no valido.", False)
        End If
        Return guardados
    End Function

    Protected Function salvarCorreoDesdeRow(ByRef theRow As Data.DataRow, ByVal id_Envio As Integer, ByVal lote As Integer, ByVal hashValue As Integer) As Boolean
        Dim entidad As New Entidad
        Dim indice As Integer
        Try
            Integer.TryParse(tbx_upl_NombreEntidad.Text, indice)
            entidad.NombreEntidad.Value = theRow.Item(indice)

            If tbx_upl_Apellido.Text.Length > 0 Then
                Integer.TryParse(tbx_upl_Apellido.Text, indice)
                entidad.Apellido.Value = theRow.Item(indice)
            End If

            If tbx_upl_Apellido2.Text.Length > 0 Then
                Integer.TryParse(tbx_upl_Apellido2.Text, indice)
                entidad.Apellido2.Value = theRow.Item(indice)
            End If

            If tbx_upl_Email.Text.Length > 0 Then
                Integer.TryParse(tbx_upl_Email.Text, indice)
                entidad.Email.Value = theRow.Item(indice)
            End If

            Dim nombreEntidad As String = entidad.NombreEntidad.Value
            If entidad.Apellido.IsValid Then
                nombreEntidad &= entidad.Apellido.Value
                If entidad.Apellido2.IsValid Then
                    nombreEntidad &= entidad.Apellido2.Value
                End If
            End If

            'Salvar el correo
            Return insertCorreos_Envio(entidad.Email.Value, nombreEntidad, id_Envio, lote, hashValue)

        Catch exc As Exception
            'lbl_Errores.Text &= "Error: " & exc.Message & "<br />"
            '
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Lee un archivo de excel y devuelve un dataset con la informacion
    ''' </summary>
    ''' <param name="hoja">Sheet del cual leer la informacion en excel</param>
    ''' <returns>Dataset con la informacion del archivo</returns>
    ''' <remarks></remarks>
    Private Function cargadelexcelDS(ByVal hoja As String) As Data.DataSet
        Dim ds As New Data.DataSet
        Try
            If upl_Archivo.HasFile Then
                Dim filename As String = upl_Archivo.FileName
                Dim filePath As String = Server.MapPath("") & "\" & filename
                upl_Archivo.SaveAs(filePath)
                Dim extension As String = filename.Substring(filename.LastIndexOf("."))

                If extension.Equals(".xls") Then
                    Dim cnn As New System.Data.OleDb.OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & filePath & ";Extended Properties=""Excel 8.0;HDR=No;IMEX=1; """)
                    Dim da As New System.Data.OleDb.OleDbDataAdapter("Select * from [" & hoja & "$]", cnn)
                    da.Fill(ds)
                End If
                File.Delete(filePath)
            End If
        Catch ex As Exception

        End Try
        Return ds
    End Function

    'Enviar
    Protected Sub UpdatingScript(ByVal id_Envio As Integer, ByVal lote As Integer, ByVal hashValue As Integer, ByVal totalCorreos As Integer)
        'Hacer el request al AsyncSender...
        Dim requestURL As String = "AsyncSender.aspx?id_Envio=" & id_Envio & "&lote=" & lote & "&hashValue=" & hashValue

        Dim intervalScript As String = Orbelink.Helpers.JavaScript.Script_SetIntervalCallback(div_Response.ClientID, 3000, totalCorreos + 2, "updateElementWithRequest('" & div_Response.ClientID & "', '" & requestURL & "', true);", lnk_StopSending)
        Orbelink.Helpers.JavaScript.WriteScriptToLiteral(intervalScript, ltl_ScriptResponse)
    End Sub

    'Eventos
    Protected Sub btn_Salvar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Salvar.Click

        If Orbelink.Helpers.Mailer.AuthenticateSMTPServer(tbx_SenderEmail.Text, tbx_SenderName.Text, tbx_ServerName.Text, tbx_SMTPUsername.Text, tbx_SMTPPassword.Text) Then
            Dim contenidoURL As String = salvarContenidoHTML()
            If contenidoURL.Length > 0 Then
                If insertEnvio(contenidoURL) Then
                    Dim envio As New Envio
                    Dim id_Envio As Integer = connection.lastKey(envio.TableName, envio.Id_Envio.Name)

                    If insertLotes_Envio(id_Envio, 0) Then
                        'Salvar los correos desde excel
                        Dim randObj As New Random(New TimeSpan(Date.UtcNow.Hour, Date.UtcNow.Minute, Date.UtcNow.Second).TotalSeconds)
                        Dim hashValue As Integer = randObj.Next(1000, 9999)

                        Dim guardados As Integer = guardarCorreosEnvio(id_Envio, 0, hashValue)
                        If guardados > 0 Then
                            MyMaster.MostrarMensaje("Enviando " & guardados & " correos...<br />", False)

                            'Cargar para que asyncsender mande los correos.
                            pnl_Reporte.Visible = True
                            UpdatingScript(id_Envio, 0, hashValue, guardados)
                        Else
                            MyMaster.MostrarMensaje("No se pudo extraer correos desde el excel.<br />", False)
                        End If
                    Else
                        MyMaster.MostrarMensaje("No se pudo salvar el lote.<br />", False)
                    End If
                Else
                    MyMaster.MostrarMensaje("Error al sarlvar el contenido.<br />", False)
                End If
            Else
                MyMaster.MostrarMensaje("No se pudo guardar el archivo html.<br />", False)
            End If
        Else
            MyMaster.MostrarMensaje("No se pudo verificar la informacion para autentificacion.<br />", False)
        End If
    End Sub

    Protected Sub btn_Cancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_Cancelar.Click
        ClearInfo(Configuration.EstadoPantalla.NORMAL, True)
    End Sub

End Class
