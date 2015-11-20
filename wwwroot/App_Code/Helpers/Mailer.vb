Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler
Imports System.IO
Imports System.Configuration
Imports System.Web.Configuration
Imports System.Net.Configuration
Imports System.Net.Mail
Namespace Orbelink.Helpers
    Public Module Mailer

        Public Const Replace_Correos_Envio As String = "THE_ID_CORREOS_ENVIO"
        Public Const Replace_Nombre_Envio As String = "USUARIO DEL NEWSLETTER"

        Public Enum StatusEnvio As Integer
            ENVIADO = 0
            VISTO = 1
            RECHAZADO = 2
            ESPERA = 3
            NO_ENVIADO = 4
        End Enum

        Public Enum TipoEnvio As Integer
            ENVIO = 0
            RAPIDO = 1
            IMAGEN = 2
        End Enum

        Public Function SendOneMail(ByVal ReplyTo_Email As String, ByVal From_Name As String, ByVal To_Email As String, ByVal To_Name As String, _
              ByVal Subject As String, ByVal Body As String, Optional ByVal attachments As String() = Nothing, Optional ByVal asThread As Boolean = False) As Boolean
            'ReplyTo
            Dim ReplyTo_Name As String
            If From_Name IsNot Nothing Then
                ReplyTo_Name = From_Name
            Else
                ReplyTo_Name = ReplyTo_Email
            End If

            'Toma de web.config el correo
            Dim configurationFile As System.Configuration.Configuration = WebConfigurationManager.OpenWebConfiguration("~/Web.Config")
            Dim mailSettings As MailSettingsSectionGroup = configurationFile.GetSectionGroup("system.net/mailSettings")
            Dim From_Email As String
            Dim client As SmtpClient
            If mailSettings IsNot Nothing Then
                From_Email = mailSettings.Smtp.From
                client = New SmtpClient(mailSettings.Smtp.Network.Host, mailSettings.Smtp.Network.Port)
            Else
                From_Email = Configuration.Config_DefaultAdminEmail
                client = New SmtpClient()
            End If
            If From_Name Is Nothing Then
                From_Name = From_Email
            End If

            'El correo
            Dim mailFrom As New MailAddress(From_Email, From_Name)
            Dim mailTo As New MailAddress(To_Email, To_Name)
            Dim correo As New MailMessage(mailFrom, mailTo)
            If ReplyTo_Email IsNot Nothing And ReplyTo_Name IsNot Nothing Then
                Dim mailReplyTo As New MailAddress(ReplyTo_Email, ReplyTo_Name)
                correo.ReplyTo = mailReplyTo
            End If
            correo.IsBodyHtml = True
            correo.BodyEncoding = Encoding.GetEncoding("iso-8859-1")
            correo.Subject = Subject
            correo.Body = Body

            'Attachments
            If attachments IsNot Nothing Then
                For Each attachment As String In attachments
                    If File.Exists(attachment) Then
                        Dim attach As New Attachment(attachment)
                        correo.Attachments.Add(attach)
                    End If
                Next
            End If

            'Lo envia, ya sea como hilo o secuencial
            If Not asThread Then
                Return SendOneMail(correo, client)
            Else
                Dim params As New SendOneMail_Params
                params.correo = correo
                params.client = client
                Dim t_SendOneMail As New Threading.Thread(AddressOf SendOneMail)
                t_SendOneMail.Start(params)
                Return True
            End If
        End Function

        Public Function SendOneMail_Authenticate(ByVal From_Email As String, ByVal From_Name As String, ByVal To_Email As String, ByVal To_Name As String, _
                 ByVal ReplyTo_Email As String, ByVal ReplyTo_Name As String, ByVal Subject As String, ByVal Body As String, ByVal SMTPServer As String, _
                 ByVal Username As String, ByVal Password As String, Optional ByVal attachments As String() = Nothing, Optional ByVal asThread As Boolean = False) As Boolean
            Dim client As New SmtpClient(SMTPServer)
            Dim mailReplyTo As New MailAddress(ReplyTo_Email, ReplyTo_Name)
            Dim mailFrom As New MailAddress(From_Email, From_Name)
            Dim mailTo As New MailAddress(To_Email, To_Name)
            Dim correo As New MailMessage(mailFrom, mailTo)

            correo.IsBodyHtml = True
            correo.BodyEncoding = Encoding.GetEncoding("iso-8859-1")
            correo.Subject = Subject
            correo.Body = Body
            correo.ReplyTo = mailReplyTo

            client.Credentials = New System.Net.NetworkCredential(Username, Password)
            client.Port = 25
            client.Send(correo)

            'Attachments
            If attachments IsNot Nothing Then
                For Each attachment As String In attachments
                    If File.Exists(attachment) Then
                        Dim attach As New Attachment(attachment)
                        correo.Attachments.Add(attach)
                    End If
                Next
            End If

            'Lo envia, ya sea como hilo o secuencial
            If Not asThread Then
                Return SendOneMail(correo, client)
            Else
                Dim params As New SendOneMail_Params
                params.correo = correo
                params.client = client
                Dim t_SendOneMail As New Threading.Thread(AddressOf SendOneMail)
                t_SendOneMail.Start(params)
                Return True
            End If
        End Function

        Private Structure SendOneMail_Params
            Dim correo As MailMessage
            Dim client As SmtpClient
        End Structure

        Private Sub SendOneMail(ByVal theParams As Object)
            Dim params As SendOneMail_Params = theParams
            Dim enviado As Boolean
            Try
                params.client.Send(params.correo)
                enviado = True
            Catch exc As Exception
                enviado = False
            End Try
            Logger.RegistrarSendOneMail(params.correo, params.client, enviado, True)
        End Sub

        Private Function SendOneMail(ByVal correo As MailMessage, ByVal client As SmtpClient) As Boolean
            Dim enviado As Boolean
            Try
                client.Send(correo)
                enviado = True
            Catch exc As Exception
                enviado = False
            End Try
            Logger.RegistrarSendOneMail(correo, client, enviado, True)
            Return enviado
        End Function

        Public Function AuthenticateSMTPServer(ByVal From_Email As String, ByVal From_Name As String, ByVal ServerName As String, ByVal SMTPUserName As String, ByVal SMTPPassword As String) As Boolean
            Dim valido As Boolean = False

            If ServerName.Length > 0 And SMTPUserName.Length > 0 And SMTPPassword.Length > 0 Then
                If Orbelink.Helpers.Mailer.SendOneMail_Authenticate(From_Email, From_Name, "mailer@tambormanager.com", "Authentication Test Mail", "mailer@tambormanager.com", "Authentication Test Mail", "Authentication Test", "Authentication Test", ServerName, SMTPUserName, SMTPPassword) Then
                    valido = True
                End If
            End If

            Return valido
        End Function

        Public Function RequestContent_ByURL(ByVal url As String) As String
            Dim contenido As String = ""
            Dim myHttpWebRequest As System.Net.HttpWebRequest = System.Net.HttpWebRequest.Create(url)
            Dim myHttpWebResponse As System.Net.HttpWebResponse = myHttpWebRequest.GetResponse()

            Dim receiveStream As Stream = myHttpWebResponse.GetResponseStream()
            Dim encode As Encoding = System.Text.Encoding.GetEncoding("utf-8")
            Dim readStream As New StreamReader(receiveStream, encode)

            contenido = readStream.ReadToEnd()

            readStream.Close()
            myHttpWebResponse.Close()
            Return contenido
        End Function

        'Mails predefinidos
        Public Function SendPasswordRecovery(ByVal To_Email As String, ByVal To_Name As String, _
                ByVal Username As String, ByVal validationCode As String, ByVal SiteName As String, ByVal SiteURL As String) As Boolean
            Dim body As String = "<h1>Recuperar su password para " & SiteName & "</h1><br><br>"
            body &= "Este es un email autogenerado porque usted ha olvidado su password.<br><br>"
            body &= "Su informacion es:<br>"
            body &= "  - Username: " & Username & "<br>"
            body &= "  - Validation Code: " & validationCode & "<br>"
            body &= "<br><br>Puede entrar a " & SiteName & " en "
            body &= "<a href=""" & SiteURL & """ >" & SiteURL & "</a><br>"
            Return SendOneMail(Nothing, SiteName, To_Email, To_Name, "Recuperar su password para " & SiteName, body)
        End Function

        Public Function SendNewUser(ByVal To_Email As String, ByVal To_Name As String, _
              ByVal Username As String, ByVal password As String, ByVal SiteName As String, ByVal SiteURL As String) As Boolean
            Dim body As String = String.Format(Resources.Mailer_Resources.NuevoUsuario_MailFormat, To_Name, Username, password, SiteName, SiteURL)
            Dim subject As String = String.Format(Resources.Mailer_Resources.NuevoUsuario_SubjectFormat, SiteName)
            Return SendOneMail(Nothing, Nothing, To_Email, To_Name, subject, body)
        End Function

        Public Function SendErrorMail(ByVal From_Email As String, ByVal From_Name As String, ByVal To_Email As String, ByVal To_Name As String, _
                                      ByVal techInfo As String, ByVal userInfo As String, ByVal siteName As String, ByVal fechaError As Date, _
                                      ByVal SiteURL As String, Optional ByVal attachments As String() = Nothing) As Boolean
            Dim body As String = String.Format(Resources.Mailer_Resources.ErrorMail_Format, siteName, SiteURL, techInfo, "---", userInfo)
            Return SendOneMail(From_Email, From_Name, To_Email, To_Name, siteName, body, attachments, True)
        End Function

        Public Function SendRecordatorio(ByVal To_Email As String, ByVal To_Name As String, _
               ByVal nombrePendiente As String, ByVal fechaProgramadaUTC As Date, ByVal SiteURL As String) As Boolean
            Dim programado As String = Orbelink.DateAndTime.DateHandler.ToStringFromDate(fechaProgramadaUTC)
            Dim restante As String = Orbelink.DateAndTime.DateHandler.ToStringFromTimeSpan(fechaProgramadaUTC.Subtract(Date.UtcNow))
            Dim body As String = String.Format(Resources.Mailer_Resources.Recordatorio_MailFormat, To_Name, nombrePendiente, programado, restante, SiteURL)
            Return SendOneMail(Nothing, Nothing, To_Email, To_Name, nombrePendiente, body)
        End Function

        Public Function SendPendienteUpdated(ByVal To_Email As String, ByVal To_Name As String, ByVal Pendiente_Nombre As String, _
                     ByVal Name As String, ByVal Status As String, ByVal Comment As String, _
                     ByVal SiteName As String, ByVal SiteURL As String) As Boolean
            Dim body As String = "This is an autogenerated email to inform you of a change on your " & Pendiente_Nombre & "<br><br>"
            body &= "Your information is:<br>"
            body &= "  - Name: " & Name & "<br>"
            body &= "  - Status:  " & Status & "<br>"
            body &= "  - Comment:  " & Comment & "<br>"
            body &= "<br><br>View it at <a href=""" & SiteURL & """ >" & SiteURL & "</a><br>"

            Return SendOneMail(Nothing, Nothing, To_Email, To_Name, "Actualizacion de pendiente en " & SiteName, body)
        End Function

    End Module

End Namespace