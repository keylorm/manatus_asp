Imports Orbelink.DBHandler
Imports System.IO
Imports Orbelink.Entity.Envios
Imports Orbelink.DateAndTime

Partial Class _AsyncSender
    Inherits PageBaseClass

    Const codigo_pantalla As String = "EV-02"
    Const level As Integer = 2
    Dim cantidadEnviados As Integer
    Dim enviados As String
    Dim noEnviados As String
    Const corteDatos As String = "+---+"

    'Pagina
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AddScripts(level)
        Dim id_Envio As Integer
        Dim lote As Integer
        Dim hashValue As String

        cantidadEnviados = 0

        If Not IsPostBack Then
            If Request.Params("id_Envio") IsNot Nothing Then
                id_Envio = Request.Params("id_Envio")
                If Request.Params("lote") IsNot Nothing Then
                    lote = Request.Params("lote")
                    If Request.Params("hashValue") IsNot Nothing Then
                        hashValue = Request.Params("hashValue")

                        enviarEnvio(id_Envio, lote, hashValue)
                    End If
                End If
            End If
        End If

    End Sub

    'Correos_Envio 
    Protected Function updateCorreos_Envio(ByVal Id_Correos_Envio As Integer, ByVal status As Orbelink.Helpers.Mailer.StatusEnvio) As Boolean
        Dim Correos_Envio As New Correos_Envio
        Try
            Correos_Envio.Id_Correos_Envio.Where.EqualCondition(Id_Correos_Envio)
            Correos_Envio.Fecha.ValueUtc = Date.UtcNow
            Correos_Envio.Status.Value = status
            connection.executeUpdate(queryBuilder.UpdateQuery(Correos_Envio))
            Return True
        Catch exc As Exception
            'MyMaster.mostrarMensaje(exc.Message, true)
            Return False
        End Try
    End Function

    'Lotes_Envio
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

    'Log
    Protected Sub ReadFile(ByVal id_Envio As String, ByVal lote As String, ByVal hashValue As String)
        Dim theFile As String = id_Envio & "-" & lote & "_" & hashValue & ".txt"
        Dim fileDirectory As String = Configuration.logsPath & "Mailer\"
        Dim filePath As String = fileDirectory & theFile
        Dim primeraLinea As String = ""

        If File.Exists(filePath) Then
            Dim fileReader As New StreamReader(filePath)

            primeraLinea = fileReader.ReadLine()
            Dim resto As String = fileReader.ReadToEnd

            Dim indiceCortePLinea As Integer = primeraLinea.LastIndexOf(":")
            cantidadEnviados = primeraLinea.Substring(indiceCortePLinea + 1, primeraLinea.Length - indiceCortePLinea - 1)

            'Lee los enviados
            Dim indiceCorteEnviados As Integer = resto.IndexOf(corteDatos)
            Dim indiceCorteNoEnviados As Integer = resto.IndexOf(corteDatos, indiceCorteEnviados + corteDatos.Length)

            If indiceCorteEnviados >= 0 Then
                enviados = resto.Substring(indiceCorteEnviados + corteDatos.Length + 2, indiceCorteNoEnviados - indiceCorteEnviados - corteDatos.Length - 2)
            Else
                enviados = ""
            End If

            'Lee los enviados
            If indiceCorteNoEnviados >= 0 Then
                noEnviados = resto.Substring(indiceCorteNoEnviados + corteDatos.Length + 2, resto.Length - indiceCorteNoEnviados - corteDatos.Length - 2)
            Else
                noEnviados = ""
            End If

            fileReader.Close()
        End If

    End Sub

    Protected Sub RecordMailSent(ByVal id_Envio As String, ByVal lote As String, ByVal hashValue As String, ByVal theName As String, ByVal theMail As String, ByVal enviado As Boolean)
        Dim theFile As String = id_Envio & "-" & lote & "_" & hashValue & ".txt"
        Dim fileDirectory As String = Configuration.logsPath & "Mailer\"
        Dim filePath As String = fileDirectory & theFile
        Directory.CreateDirectory(fileDirectory)
        Dim fileWriter As New StreamWriter(filePath, False)

        If enviado Then
            cantidadEnviados = cantidadEnviados + 1
        End If

        If cantidadEnviados = 0 Then
            fileWriter.WriteLine("Total:0")
        Else
            fileWriter.WriteLine("Total:" & cantidadEnviados)
        End If

        'Escribe los enviados
        fileWriter.WriteLine(corteDatos)
        fileWriter.Write(enviados)
        If enviado Then
            fileWriter.WriteLine(cantidadEnviados & " : " & theMail & " - " & theName)
        End If

        'Escribe los no enviados
        fileWriter.WriteLine(corteDatos)
        fileWriter.Write(noEnviados)
        If Not enviado Then
            fileWriter.WriteLine("0 : " & theMail & " - " & theName)
        End If

        fileWriter.Close()
    End Sub

    'Enviar
    Protected Sub enviarEnvio(ByVal id_Envio As Integer, ByVal lote As Integer, ByVal hashValue As Integer)
        Dim dataSet As Data.DataSet
        Dim Correos_Envio As New Correos_Envio
        Dim Envio As New Envio
        Dim Lotes_Envio As New Lotes_Envio

        Envio.Fields.SelectAll()
        Envio.Id_Envio.Where.EqualCondition(id_Envio)

        queryBuilder.Join.EqualCondition(Lotes_Envio.Id_Envio, Envio.Id_Envio)
        Lotes_Envio.Lote.Where.EqualCondition(lote)

        Correos_Envio.Correo.ToSelect = True
        Correos_Envio.Id_Correos_Envio.ToSelect = True
        Correos_Envio.Nombre.ToSelect = True
        queryBuilder.Join.EqualCondition(Correos_Envio.Id_Envio, Lotes_Envio.Id_Envio)
        queryBuilder.Join.EqualCondition(Correos_Envio.Lote, Lotes_Envio.Lote)
        Correos_Envio.Status.Where.EqualCondition(Orbelink.Helpers.Mailer.StatusEnvio.ESPERA)
        Correos_Envio.HashValue.Where.EqualCondition(hashValue)

        queryBuilder.From.Add(Correos_Envio)
        queryBuilder.From.Add(Envio)
        queryBuilder.From.Add(Lotes_Envio)
        queryBuilder.Top = 1
        dataSet = connection.executeSelect(queryBuilder.RelationalSelectQuery)

        ReadFile(id_Envio, lote, hashValue)

        If dataSet.Tables.Count > 0 Then
            If dataSet.Tables(0).Rows.Count > 0 Then
                ObjectBuilder.CreateObject(dataSet.Tables(0), 0, Envio)
                ObjectBuilder.CreateObject(dataSet.Tables(0), 0, Correos_Envio)

                'Obtener el contenido
                Dim contenido As String = Orbelink.Helpers.Mailer.RequestContent_ByURL(Envio.ContenidoURL.Value)

                'Contenido Personalizado
                Dim contenidoEntidad As String = contenido.Replace(Orbelink.Helpers.Mailer.Replace_Correos_Envio, Correos_Envio.Id_Correos_Envio.Value)
                contenidoEntidad = contenidoEntidad.Replace(Orbelink.Helpers.Mailer.Replace_Nombre_Envio, Correos_Envio.Nombre.Value)
                'Falta lo de no recibir mas correos
                Dim sent As Boolean = False

                If Envio.SMTPServer.IsValid And Envio.UserName.IsValid And Envio.Password.IsValid Then
                    sent = Orbelink.Helpers.Mailer.SendOneMail_Authenticate(Envio.SenderEmail.Value, Envio.SenderName.Value, Correos_Envio.Correo.Value, Correos_Envio.Nombre.Value, Envio.ReplyTo.Value, Envio.SenderName.Value, Envio.Subject.Value, contenidoEntidad, Envio.SMTPServer.Value, Envio.UserName.Value, Envio.Password.Value)
                Else
                    sent = Orbelink.Helpers.Mailer.SendOneMail(Envio.SenderEmail.Value, Envio.SenderName.Value, Correos_Envio.Correo.Value, Correos_Envio.Nombre.Value, Envio.Subject.Value, contenidoEntidad)
                End If

                If sent Then
                    sent = updateCorreos_Envio(Correos_Envio.Id_Correos_Envio.Value, Orbelink.Helpers.Mailer.StatusEnvio.ENVIADO)
                Else
                    updateCorreos_Envio(Correos_Envio.Id_Correos_Envio.Value, Orbelink.Helpers.Mailer.StatusEnvio.NO_ENVIADO)
                End If

                'Registra correo
                RecordMailSent(id_Envio, lote, hashValue, Correos_Envio.Nombre.Value, Correos_Envio.Correo.Value, sent)
                If sent Then
                    Response.Write("<span style='color:blue'>Correo enviado: " & Correos_Envio.Correo.Value & "</span>")
                Else
                    Response.Write("<span style='color:red'>Correo no enviado: " & Correos_Envio.Correo.Value & "</span>")
                End If
            Else
                updateLotes_Envio(id_Envio, lote)
                Response.Write("No mas correos por enviar.")
            End If
        End If

        Response.Write("<br />Total enviados: " & cantidadEnviados)
        Response.Write("<br />Hora: " & DateHandler.ToLocalizedStringFromUtc(Date.UtcNow))
        Response.End()
    End Sub

End Class
