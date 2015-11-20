Imports Microsoft.VisualBasic
Imports System.IO
Imports System.Xml
Imports System.Data.SqlClient

Namespace Orbelink.Helpers
    Public Module Logger

        Public limiteItems As Integer = 100

        Public lockNoExiste As Object = New Object

        Private Function GetRutaDirectory(ByVal tipoLog As String, ByVal context As HttpContext) As String
            Dim logDirectory As String = "C:\domains\booking.manatuscostarica.com\logs\"
            If context IsNot Nothing Then
                Dim security As New Orbelink.Control.Security.SecurityHandler(Configuration.Config_DefaultConnectionString)
                logDirectory &= "Users\"
                If security.Perfil > 0 Then
                    logDirectory &= security.Usuario & "\"
                Else
                    logDirectory &= "_\"
                End If
            End If
            logDirectory &= tipoLog & "\"
            Directory.CreateDirectory(logDirectory)
            Return logDirectory
        End Function

        Private Function GetRutaArchivo(ByVal tipoLog As String, ByVal extension As String, ByVal withDate As Boolean, ByVal withHour As Boolean, ByVal context As HttpContext, Optional ByVal newFile As Boolean = False) As String
            Dim logDirectory As String = GetRutaDirectory(tipoLog, context)
            Dim filePath As String = logDirectory & tipoLog
            If withDate Then
                Dim fecha As Date = Date.UtcNow
                filePath &= "_" & fecha.Month & "-" & fecha.Day & "-" & fecha.Year
                If withHour Then
                    filePath &= "_" & fecha.Hour & "-" & fecha.Minute & "-" & fecha.Second
                End If
            End If
            If File.Exists(filePath & "." & extension) And newFile Then
                Dim counter As Integer = 0
                Do While File.Exists(filePath & "--" & counter & "." & extension)
                    counter += 1
                Loop
                Return filePath & "--" & counter & "." & extension
            Else
                Return filePath & "." & extension
            End If
        End Function

        'SlowRequest
        Public Sub RegistrarRequestLento(ByVal fechaInicio As Date, ByVal fechaFin As Date, ByVal milisegundos As Long, ByVal requestUrl As String)
            Dim filePath As String = GetRutaArchivo("SlowRequests", "xml", True, False, Nothing)
            Dim xmldoc As New System.Xml.XmlDocument
            Dim nodoPrincipal As System.Xml.XmlElement

            If File.Exists(filePath) Then
                xmldoc.Load(filePath)
                nodoPrincipal = xmldoc.FirstChild.NextSibling
            Else
                Dim declarationNode As XmlNode = xmldoc.CreateNode(XmlNodeType.XmlDeclaration, "", "")
                xmldoc.AppendChild(declarationNode)

                'let's add the root element
                nodoPrincipal = xmldoc.CreateElement("slowRequests")
                xmldoc.AppendChild(nodoPrincipal)
            End If

            Dim nuevoNodo As XmlElement = xmldoc.CreateElement("request")
            nuevoNodo.SetAttribute("request_Begin", fechaInicio.ToShortDateString & " " & fechaInicio.ToLongTimeString)
            nuevoNodo.SetAttribute("request_End", fechaFin.ToShortDateString & " " & fechaFin.ToLongTimeString)
            nuevoNodo.SetAttribute("miliseconds", milisegundos)
            nuevoNodo.InnerText = requestUrl
            nodoPrincipal.AppendChild(nuevoNodo)

            xmldoc.Save(filePath)
        End Sub

        'App Errors
        Public Sub RegistrarAppError(ByVal ex As Exception, ByVal context As HttpContext)
            Dim filePath As String = GetRutaArchivo("AppErrors", "txt", True, False, context)
            Dim L_fichero As New StreamWriter(filePath, True)
            L_fichero.WriteLine("******************")
            L_fichero.WriteLine(Orbelink.DateAndTime.DateHandler.ToStringFromDate(Date.UtcNow))
            L_fichero.WriteLine("Descripcion: " + ex.Message)
            L_fichero.WriteLine("Origen:" + ex.Source)
            L_fichero.WriteLine("Pila:" + ex.StackTrace)
            L_fichero.WriteLine()
            L_fichero.Close()
        End Sub

        'OC Errors
        Public Sub RegistrarMensajeErrorOC(ByRef contexto As HttpContext, ByVal theMessage As String)
            Dim filePath As String = GetRutaArchivo("OCErrors", "txt", True, False, contexto)
            Dim L_fichero As New StreamWriter(filePath, True)
            L_fichero.WriteLine("******************")
            L_fichero.WriteLine(Orbelink.DateAndTime.DateHandler.ToStringFromDate(Date.UtcNow))
            L_fichero.WriteLine("Mensaje: " + theMessage)
            L_fichero.WriteLine()
            L_fichero.Close()
            If contexto IsNot Nothing Then
                Dim registraRutinaOcError_T As New Threading.Thread(AddressOf RegistrarRutinaOCError)
                Dim params As New RutinaOcErrorParams
                params.filePath = GetRutaArchivo("Rutina", "xml", False, False, contexto)
                params.theMessage = theMessage
                registraRutinaOcError_T.Start(params)
            End If
        End Sub

        Public Sub RegistrarReporteErroresOC(ByRef contexto As HttpContext)
            CopiarRutinaHistorial(contexto)
        End Sub

        'SQL
        Public Sub RegistrarSQLError(ByVal theException As SqlException, ByVal theConnectionString As String, ByVal theQuery As String, ByVal context As HttpContext)
            Dim filePath As String = GetRutaArchivo("SqlErrors", "xml", True, False, context)
            Dim xmldoc As New System.Xml.XmlDocument
            Dim nodoPrincipal As System.Xml.XmlElement
            If File.Exists(filePath) Then
                xmldoc.Load(filePath)
                nodoPrincipal = xmldoc.FirstChild.NextSibling
            Else
                Dim declarationNode As XmlNode = xmldoc.CreateNode(XmlNodeType.XmlDeclaration, "", "")
                xmldoc.AppendChild(declarationNode)
                nodoPrincipal = xmldoc.CreateElement("errors")
                xmldoc.AppendChild(nodoPrincipal)
            End If
            WriteSqlException(xmldoc, nodoPrincipal, theException, theConnectionString, theQuery)
            xmldoc.Save(filePath)
            If context IsNot Nothing Then
                Dim registraRutina_T As New Threading.Thread(AddressOf RegistrarRutinaSQLError)
                Dim params As New RutinaSQLErrorParams
                params.filePath = GetRutaArchivo("Rutina", "xml", False, False, context)
                params.exc = theException
                params.connectionString = theConnectionString
                params.query = theQuery
                registraRutina_T.Start(params)
            End If
        End Sub

        Private Sub WriteSqlException(ByVal xmldoc As XmlDocument, ByVal nodoPrincipal As XmlNode, ByVal theException As SqlException, ByVal theConnectionString As String, ByVal theQuery As String)
            Dim fecha As Date = Date.UtcNow
            Dim nuevoNodo As XmlElement = xmldoc.CreateElement("exception")
            nuevoNodo.SetAttribute("fecha", fecha.ToShortDateString & " " & fecha.ToLongTimeString)
            nuevoNodo.SetAttribute("servidor", theException.Server)

            Dim nodeA As XmlElement = xmldoc.CreateElement("connectionString")
            nodeA.InnerText = theConnectionString
            nuevoNodo.AppendChild(nodeA)

            Dim nodeB As XmlElement = xmldoc.CreateElement("sqlString")
            nodeB.InnerText = theQuery
            nuevoNodo.AppendChild(nodeB)

            Dim nodeC As XmlElement = xmldoc.CreateElement("exceptionMessage")
            nodeC.InnerText = theException.Message
            nuevoNodo.AppendChild(nodeC)
            nodoPrincipal.AppendChild(nuevoNodo)
        End Sub

        Public Sub RegistrarSQL(ByVal theConnectionString As String, ByVal theQuery As String, ByVal context As HttpContext)
            Dim filePath As String = GetRutaArchivo("Sql", "xml", True, False, context)
            Dim xmldoc As New System.Xml.XmlDocument
            Dim nodoPrincipal As System.Xml.XmlElement
            If File.Exists(filePath) Then
                xmldoc.Load(filePath)
                nodoPrincipal = xmldoc.FirstChild.NextSibling
            Else
                Dim declarationNode As XmlNode = xmldoc.CreateNode(XmlNodeType.XmlDeclaration, "", "")
                xmldoc.AppendChild(declarationNode)
                nodoPrincipal = xmldoc.CreateElement("querys")
                xmldoc.AppendChild(nodoPrincipal)
            End If
            If nodoPrincipal.ChildNodes.Count > limiteItems Then
                File.Copy(filePath, GetRutaArchivo("Sql", "xml", True, False, context, True))
                File.Delete(filePath)
                RegistrarSQL(theConnectionString, theQuery, context)
            Else
                WriteSQL(xmldoc, nodoPrincipal, theConnectionString, theQuery)
                xmldoc.Save(filePath)
            End If
        End Sub

        Private Sub WriteSQL(ByVal xmldoc As XmlDocument, ByVal nodoPrincipal As XmlNode, ByVal theConnectionString As String, ByVal theQuery As String)
            Dim fecha As Date = Date.UtcNow
            Dim nuevoNodo As XmlElement = xmldoc.CreateElement("query")
            nuevoNodo.SetAttribute("fecha", fecha.ToShortDateString & " " & fecha.ToLongTimeString)

            Dim nodeA As XmlElement = xmldoc.CreateElement("connectionString")
            nodeA.InnerText = theConnectionString
            nuevoNodo.AppendChild(nodeA)

            Dim nodeB As XmlElement = xmldoc.CreateElement("sqlString")
            nodeB.InnerText = theQuery
            nuevoNodo.AppendChild(nodeB)

            nodoPrincipal.AppendChild(nuevoNodo)
        End Sub

        'SendOneMail
        Public Sub RegistrarSendOneMail(ByRef theMail As System.Net.Mail.MailMessage, ByVal theClient As System.Net.Mail.SmtpClient, ByVal exito As Boolean, ByVal registrarBody As Boolean)
            Dim filePath As String = GetRutaArchivo("SendOneMail", "xml", True, False, Nothing)
            Dim xmldoc As New System.Xml.XmlDocument
            Dim nodoPrincipal As System.Xml.XmlElement

            If File.Exists(filePath) Then
                xmldoc.Load(filePath)
                nodoPrincipal = xmldoc.FirstChild.NextSibling
            Else
                Dim declarationNode As XmlNode = xmldoc.CreateNode(XmlNodeType.XmlDeclaration, "", "")
                xmldoc.AppendChild(declarationNode)

                'let's add the root element
                nodoPrincipal = xmldoc.CreateElement("mails")
                xmldoc.AppendChild(nodoPrincipal)
            End If

            Dim fecha As Date = Date.UtcNow
            Dim nuevoNodo As XmlElement = xmldoc.CreateElement("mail")

            nuevoNodo.SetAttribute("subject", theMail.Subject)
            nuevoNodo.SetAttribute("from", theMail.From.DisplayName & "(" & theMail.From.Address & ")")
            nuevoNodo.SetAttribute("date", fecha.ToShortDateString & " " & fecha.ToLongTimeString)
            nuevoNodo.SetAttribute("exito", exito)

            Dim toNode As XmlElement = xmldoc.CreateElement("toList")
            For Each toAddres As System.Net.Mail.MailAddress In theMail.To
                nuevoNodo.SetAttribute("to", toAddres.DisplayName & "(" & toAddres.Address & ")")
            Next

            Dim clientNode As XmlElement = xmldoc.CreateElement("client")
            clientNode.SetAttribute("host", theClient.Host)
            clientNode.SetAttribute("port", theClient.Port)
            If theClient.Credentials IsNot Nothing Then
                Try
                    Dim cred As Net.NetworkCredential = theClient.Credentials
                    clientNode.SetAttribute("domain", cred.Domain)
                    clientNode.SetAttribute("username", cred.UserName)
                Catch ex As Exception

                End Try
            End If
            nuevoNodo.AppendChild(clientNode)

            If registrarBody Then
                Dim mailNode As XmlElement = xmldoc.CreateElement("body")
                mailNode.InnerText = theMail.Body
                nuevoNodo.AppendChild(mailNode)
            End If

            nodoPrincipal.AppendChild(nuevoNodo)
            xmldoc.Save(filePath)
        End Sub

        'NotFind
        Public Sub RegistrarNoExisteRequest(ByVal theContext As HttpContext)
            If Not theContext.Request.CurrentExecutionFilePath.EndsWith(".axd") Then
                SyncLock lockNoExiste
                    Dim filePath As String = GetRutaArchivo("NotFoundRequest", "xml", True, False, theContext)
                    Dim xmldoc As New System.Xml.XmlDocument
                    Dim nodoPrincipal As System.Xml.XmlElement

                    If File.Exists(filePath) Then
                        xmldoc.Load(filePath)
                        nodoPrincipal = xmldoc.FirstChild.NextSibling
                    Else
                        Dim declarationNode As XmlNode = xmldoc.CreateNode(XmlNodeType.XmlDeclaration, "", "")
                        xmldoc.AppendChild(declarationNode)

                        'let's add the root element
                        nodoPrincipal = xmldoc.CreateElement("notfounds")
                        xmldoc.AppendChild(nodoPrincipal)
                    End If
                    Dim fecha As Date = Date.UtcNow
                    Dim nuevoNodo As XmlElement = xmldoc.CreateElement("notfound")
                    nuevoNodo.SetAttribute("fecha", fecha.ToShortDateString & " " & fecha.ToLongTimeString)
                    nuevoNodo.SetAttribute("path", theContext.Request.PhysicalPath)
                    nuevoNodo.SetAttribute("execPath", theContext.Request.CurrentExecutionFilePath)
                    nuevoNodo.SetAttribute("rawurl", theContext.Request.RawUrl)
                    'nuevoNodo.SetAttribute("referer", theRequest.UrlReferrer.PathAndQuery)
                    nuevoNodo.SetAttribute("userAgent", theContext.Request.UserAgent)
                    nodoPrincipal.AppendChild(nuevoNodo)
                    xmldoc.Save(filePath)
                End SyncLock
            End If
        End Sub

        'Rutina
        Public Sub RegistrarRutina(ByVal app As HttpApplication, Optional ByVal elError As Exception = Nothing)
            'Dim registraRutina_T As New Threading.Thread(AddressOf RegistrarRutina_Private)
            Dim params As New RutinaParams
            If File.Exists(app.Request.FilePath) Then
                params.session = app.Session
            End If
            params.request = app.Request
            params.context = app.Context
            params.elError = elError
            RegistrarRutina_Private(params)
            'registraRutina_T.Start(params)
        End Sub

        Private Structure RutinaParams
            Dim session As HttpSessionState
            Dim request As HttpRequest
            Dim context As HttpContext
            Dim elError As Exception
        End Structure

        Private Sub RegistrarRutina_Private(ByVal losParams As Object)
            Dim params As RutinaParams = losParams
            Dim filePath As String = GetRutaArchivo("Rutina", "xml", False, False, params.context)
            Dim xmldoc As New System.Xml.XmlDocument
            Dim nodoPrincipal As System.Xml.XmlElement

            If File.Exists(filePath) Then
                xmldoc.Load(filePath)
                nodoPrincipal = xmldoc.FirstChild.NextSibling
            Else
                Dim declarationNode As System.Xml.XmlNode = xmldoc.CreateNode(System.Xml.XmlNodeType.XmlDeclaration, "", "")
                xmldoc.AppendChild(declarationNode)

                'let's add the root element
                nodoPrincipal = xmldoc.CreateElement("LogRutinas")
                xmldoc.AppendChild(nodoPrincipal)
            End If
            If nodoPrincipal.ChildNodes.Count > 50 Then
                nodoPrincipal.RemoveChild(nodoPrincipal.FirstChild)
            End If

            'Registra
            Dim nuevoNodo As System.Xml.XmlElement = xmldoc.CreateElement("rutina")
            nuevoNodo.SetAttribute("IP", params.request.UserHostAddress.ToString)
            nuevoNodo.SetAttribute("Date", Date.UtcNow.ToShortDateString & " " & Date.UtcNow.ToLongTimeString)

            Dim controlID As String = params.request("__EVENTTARGET")
            Dim argument As String = params.request("__EVENTARGUMENT")

            Dim page As System.Xml.XmlElement = xmldoc.CreateElement("request")
            page.SetAttribute("rawurl", params.request.RawUrl.ToString)
            page.SetAttribute("httpMethod", params.request.HttpMethod)
            RegistrarVariablesRequest(xmldoc, params.request, params.session, page)
            nuevoNodo.AppendChild(page)

            'Registra informacion del control
            If controlID IsNot Nothing Then
                Dim control As System.Xml.XmlElement = xmldoc.CreateElement("ControlInfo")
                control.SetAttribute("controlID", controlID)
                If argument IsNot Nothing Then
                    control.SetAttribute("argument", argument)
                End If
                nuevoNodo.AppendChild(control)
            End If

            If params.elError IsNot Nothing Then
                Dim errorNode As System.Xml.XmlElement = xmldoc.CreateElement("error")
                errorNode.SetAttribute("descripcion", params.elError.Message)
                errorNode.InnerText = params.elError.StackTrace
                nuevoNodo.AppendChild(errorNode)
            End If

            'Agrega el nuevo registro y cierra el archivo.
            nodoPrincipal.AppendChild(nuevoNodo)
            Try
                xmldoc.Save(filePath)
                If params.elError IsNot Nothing Then
                    CopiarRutinaHistorial(params.context)
                End If
            Catch ex As Exception

            End Try
        End Sub

        Private Sub RegistrarVariablesRequest(ByRef xmldoc As XmlDocument, ByRef theRequest As HttpRequest, ByVal theSession As HttpSessionState, ByVal requestNode As XmlNode)
            Dim queryStringNode As System.Xml.XmlElement = xmldoc.CreateElement("queryString")
            For counter As Integer = 0 To theRequest.QueryString.Count - 1
                Dim paramNode As System.Xml.XmlElement = xmldoc.CreateElement("param")
                paramNode.SetAttribute("key", theRequest.QueryString.Keys.Item(counter))
                paramNode.SetAttribute("value", theRequest.QueryString.Item(counter))
                queryStringNode.AppendChild(paramNode)
            Next
            requestNode.AppendChild(queryStringNode)

            Dim formNode As System.Xml.XmlElement = xmldoc.CreateElement("form")
            For counter As Integer = 0 To theRequest.Form.Count - 1
                Dim paramNode As System.Xml.XmlElement = xmldoc.CreateElement("param")
                paramNode.SetAttribute("key", theRequest.Form.Keys.Item(counter))
                paramNode.SetAttribute("value", theRequest.Form.Item(counter))
                formNode.AppendChild(paramNode)
            Next
            requestNode.AppendChild(formNode)

            If theSession IsNot Nothing Then
                Dim sessionNode As System.Xml.XmlElement = xmldoc.CreateElement("session")
                For counter As Integer = 0 To theSession.Count - 1
                    Try
                        Dim paramNode As System.Xml.XmlElement = xmldoc.CreateElement("param")
                        paramNode.SetAttribute("key", theSession.Keys.Item(counter))
                        paramNode.SetAttribute("value", theSession.Item(counter))
                        sessionNode.AppendChild(paramNode)
                    Catch ex As Exception

                    End Try
                Next
                requestNode.AppendChild(sessionNode)
            End If

            Dim cookiesNode As System.Xml.XmlElement = xmldoc.CreateElement("cookies")
            For counter As Integer = 0 To theRequest.Cookies.Count - 1
                Dim paramNode As System.Xml.XmlElement = xmldoc.CreateElement("param")
                paramNode.SetAttribute("key", theRequest.Cookies.Keys.Item(counter))
                paramNode.SetAttribute("value", theRequest.Cookies.Item(counter).Value)
                cookiesNode.AppendChild(paramNode)
            Next
            requestNode.AppendChild(cookiesNode)
        End Sub

        'Rutina con OC
        Private Structure RutinaOcErrorParams
            Dim filePath As String
            Dim theMessage As String
        End Structure

        Private Sub RegistrarRutinaOCError(ByVal losParams As Object)
            Dim params As RutinaOcErrorParams = losParams
            Try
                Dim xmldoc As New System.Xml.XmlDocument
                Dim nodoPrincipal As System.Xml.XmlElement

                If File.Exists(params.filePath) Then
                    xmldoc.Load(params.filePath)
                    nodoPrincipal = xmldoc.FirstChild.NextSibling
                Else
                    Dim declarationNode As System.Xml.XmlNode = xmldoc.CreateNode(System.Xml.XmlNodeType.XmlDeclaration, "", "")
                    xmldoc.AppendChild(declarationNode)
                    nodoPrincipal = xmldoc.CreateElement("LogRutinas")
                    xmldoc.AppendChild(nodoPrincipal)
                End If
                Dim ultimoRutinaNodo As System.Xml.XmlNode = nodoPrincipal.LastChild

                'Registra Oc Error
                If ultimoRutinaNodo IsNot Nothing Then
                    Dim ocErrorsNode As System.Xml.XmlElement = ultimoRutinaNodo.Item("ocErrors")
                    If ocErrorsNode Is Nothing Then
                        ocErrorsNode = xmldoc.CreateElement("ocErrors")
                        ultimoRutinaNodo.AppendChild(ocErrorsNode)
                    End If
                    Dim ocErrorNode As System.Xml.XmlElement = xmldoc.CreateElement("ocError")
                    ocErrorNode.InnerText = params.theMessage
                    ocErrorsNode.AppendChild(ocErrorNode)
                End If
                'Salva
                xmldoc.Save(params.filePath)
            Catch ex As Exception
            End Try
        End Sub

        'Rutina con SQLError
        Private Structure RutinaSQLErrorParams
            Dim filePath As String
            Dim exc As SqlException
            Dim connectionString As String
            Dim query As String
        End Structure

        Private Sub RegistrarRutinaSQLError(ByVal losParams As Object)
            Dim params As RutinaSQLErrorParams = losParams
            Try
                Dim xmldoc As New System.Xml.XmlDocument
                Dim nodoPrincipal As System.Xml.XmlElement
                If File.Exists(params.filePath) Then
                    xmldoc.Load(params.filePath)
                    nodoPrincipal = xmldoc.FirstChild.NextSibling
                Else
                    Dim declarationNode As System.Xml.XmlNode = xmldoc.CreateNode(System.Xml.XmlNodeType.XmlDeclaration, "", "")
                    xmldoc.AppendChild(declarationNode)
                    nodoPrincipal = xmldoc.CreateElement("LogRutinas")
                    xmldoc.AppendChild(nodoPrincipal)
                End If
                Dim ultimoRutinaNodo As System.Xml.XmlNode = nodoPrincipal.LastChild
                If ultimoRutinaNodo IsNot Nothing Then
                    Dim errorsNode As System.Xml.XmlElement = ultimoRutinaNodo.Item("sqlErrors")
                    If errorsNode Is Nothing Then
                        errorsNode = xmldoc.CreateElement("sqlErrors")
                        ultimoRutinaNodo.AppendChild(errorsNode)
                    End If
                    WriteSqlException(xmldoc, errorsNode, params.exc, params.connectionString, params.query)
                End If
                xmldoc.Save(params.filePath)
            Catch ex As Exception
            End Try
        End Sub


        'Rutina historial
        Private Sub CopiarRutinaHistorial(ByVal context As HttpContext)
            Dim filePath As String = GetRutaArchivo("Rutina", "xml", False, False, context)
            Dim fileDestino As String = GetRutaArchivo("Rutina_Historial", "xml", True, True, context)
            If File.Exists(filePath) Then
                File.Copy(filePath, fileDestino, True)
            End If
        End Sub

        Public Function LastRutinaHistorial(ByVal context As HttpContext) As String
            Dim directorio As String = GetRutaDirectory("Rutina_Historial", context)
            Dim archivos() As String = Directory.GetFiles(directorio, "*.xml", SearchOption.TopDirectoryOnly)
            Dim ultimoFile As String = Nothing
            Dim ultimaFechaAcceso As Date = Date.MinValue
            For Each archivo As String In archivos
                Dim fechaArchivoActual As Date = File.GetLastWriteTimeUtc(archivo)
                If fechaArchivoActual > ultimaFechaAcceso Then
                    ultimoFile = archivo
                    ultimaFechaAcceso = fechaArchivoActual
                End If
            Next
            Return ultimoFile
        End Function

        'NoFileMapper
        Public Sub RegistrarNoFileMapper(ByVal theFileMissing As String)
            Dim filePath As String = GetRutaArchivo("filesMissing", "xml", True, False, Nothing)

            Dim xmldoc As New System.Xml.XmlDocument
            Dim nodoPrincipal As System.Xml.XmlElement

            If File.Exists(filePath) Then
                xmldoc.Load(filePath)
                nodoPrincipal = xmldoc.FirstChild.NextSibling
            Else
                Dim declarationNode As System.Xml.XmlNode = xmldoc.CreateNode(System.Xml.XmlNodeType.XmlDeclaration, "", "")
                xmldoc.AppendChild(declarationNode)

                'let's add the root element
                nodoPrincipal = xmldoc.CreateElement("files")
                xmldoc.AppendChild(nodoPrincipal)
            End If

            Dim nuevoNodo As System.Xml.XmlElement = xmldoc.CreateElement("fileMissing")
            nuevoNodo.SetAttribute("file", theFileMissing)
            nuevoNodo.SetAttribute("Date", Date.UtcNow.ToShortDateString & " " & Date.UtcNow.ToLongTimeString)
            nodoPrincipal.AppendChild(nuevoNodo)

            xmldoc.Save(filePath)
        End Sub

        'CronJobs
        Public Sub RegistrarCronJob()
            Dim filePath As String = GetRutaArchivo("CronJobs", "xml", True, False, Nothing)
            Dim xmldoc As New System.Xml.XmlDocument
            Dim nodoPrincipal As System.Xml.XmlElement
            If File.Exists(filePath) Then
                xmldoc.Load(filePath)
                nodoPrincipal = xmldoc.FirstChild.NextSibling
            Else
                Dim declarationNode As XmlNode = xmldoc.CreateNode(XmlNodeType.XmlDeclaration, "", "")
                xmldoc.AppendChild(declarationNode)

                'let's add the root element
                nodoPrincipal = xmldoc.CreateElement("cronJobs")
                xmldoc.AppendChild(nodoPrincipal)
            End If

            Dim fecha As Date = Date.UtcNow
            Dim nuevoNodo As XmlElement = xmldoc.CreateElement("cronjob")
            nuevoNodo.SetAttribute("fecha", fecha.ToShortDateString & " " & fecha.ToLongTimeString)
            nodoPrincipal.AppendChild(nuevoNodo)
            xmldoc.Save(filePath)
        End Sub

        'GarbageCollector
        Public Sub DeleteOldLogs()
            Dim fechaMinima As Date = Date.UtcNow.AddDays(-60)
            DeleteFilesByDate(Configuration.logsPath, fechaMinima)
        End Sub

        Private Sub DeleteFilesByDate(ByVal theDir As String, ByVal minDate As Date)
            'Recursivo sobre los directorios hijos
            Dim childDirs() As String = Directory.GetDirectories(theDir)
            For Each child As String In childDirs
                DeleteFilesByDate(child, minDate)
            Next
            'Lee los archivos
            Dim files() As String = Directory.GetFiles(theDir)
            For Each inputFilePath As String In files
                Dim theFile As New FileInfo(inputFilePath)
                If theFile.LastWriteTimeUtc <= minDate Then
                    theFile.Delete()
                End If
            Next
        End Sub
    End Module

End Namespace