Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler
Imports System.Text.RegularExpressions
Imports System.IO
Imports Orbelink.Control.Archivos

Namespace Orbelink.Control.OrbeBot
    Public Class OrbeBot

        Public Event Completed(ByVal theResults As BotResult())

        'Structuras de clase
        Structure BotSearch
            Dim RootURL As String
            Dim FechaSuceso As String
            Dim OnlySameSite As Boolean
            Dim Keywords As String()
            Dim Identifier As String
        End Structure

        Structure BotResult
            Dim ReferenceURL As String
            Dim Keyword As String
            Dim TimesFound As Integer
            Dim Identifier As String
        End Structure

        ''' <summary>
        ''' Hace el recorrido de los feeds que contiene. 
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub BeASpider(ByRef theParam As BotSearch)
            Dim spider As New System.Threading.Thread(AddressOf doSearch)
            spider.Start(theParam)
        End Sub

        'Funciones Privada
        Private Sub doSearch(ByVal theParam As Object)
            Dim param As BotSearch = theParam
            Dim resultados As System.Collections.Generic.List(Of BotResult) = Nothing
            Dim webClient As New System.Net.WebClient
            Dim fileContent As String = ""

            Dim laExtension As String = Path.GetExtension(param.RootURL)

            'Descarga el archivo del request
            'Dim htmlResult As String = webClient.DownloadString(param.RootURL)
            Dim identificador As String = Date.UtcNow.ToShortDateString.Replace("/", "-") & " " & Date.UtcNow.Hour & " " & Date.UtcNow.Minute & " " & Date.UtcNow.Second
            Dim direccionArchivo As String = ArchivoHandler.GetFolder(ArchivoHandler.ArchivoType.DocumentType) & identificador

            If laExtension.Length > 0 Then
                direccionArchivo &= laExtension
            Else
                direccionArchivo &= ".html"
            End If
            webClient.DownloadFile(param.RootURL, direccionArchivo)

            Dim theType As Orbelink.Control.Archivos.ArchivoHandler.ArchivoType = Orbelink.Control.Archivos.ArchivoHandler.GetArchivoType(laExtension)
            If theType = Orbelink.Control.Archivos.ArchivoHandler.ArchivoType.DocumentType Then
                Dim handler As New Orbelink.Control.Archivos.DocumentHandler()
                fileContent = handler.ReadDocument(direccionArchivo)
            End If

            'Busca los links para procesamiento a fondo...
            'Dim linksRegExp As New Regex("href=""([a-z]|\.|-|_)""", RegexOptions.IgnoreCase)
            'Dim links As MatchCollection = linksRegExp.Matches(htmlResult)

            For Each keyword As String In param.Keywords
                Dim regex As New Regex(keyword, RegexOptions.IgnoreCase)
                Dim matches As MatchCollection = regex.Matches(fileContent)

                If matches.Count > 0 Then
                    If resultados Is Nothing Then
                        resultados = New System.Collections.Generic.List(Of BotResult)
                    End If

                    Dim resultado As New BotResult
                    resultado.ReferenceURL = param.RootURL
                    resultado.Keyword = keyword
                    resultado.TimesFound = matches.Count
                    resultado.Identifier = param.Identifier

                    resultados.Add(resultado)
                End If
            Next

            If resultados IsNot Nothing Then
                RaiseEvent Completed(resultados.ToArray())
            End If
        End Sub

    End Class

End Namespace