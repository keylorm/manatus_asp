Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler
Imports System.IO
Imports Orbelink.Control.Archivos

Namespace Orbelink.Helpers

    Public Class RSSPublisher

        Dim _rssFilesPath As String
        Dim _rssFilesDir As String
        Dim _isWriting As Boolean
        Dim _fileWriter As StreamWriter
        Dim _rssWriter As System.Xml.XmlTextWriter
        Dim _siteURL As String
        Dim _itemsLink As String

        Sub New(ByVal siteURl As String, ByVal theRootPath As String, ByVal theRSSDirectory As String)
            _rssFilesDir = theRSSDirectory
            _siteURL = siteURl
            Directory.CreateDirectory(theRootPath & theRSSDirectory)
            _rssFilesPath = theRootPath & _rssFilesDir
        End Sub

        ''' <summary>
        ''' Borra un archivo rss
        ''' </summary>
        ''' <param name="fileName">Nombre del archivo, no debe incluir extension</param>
        ''' <remarks></remarks>
        Sub DeleteRssFile(ByVal fileName As String)
            File.Delete(_rssFilesPath & "\" & fileName & ".xml")
        End Sub

        ''' <summary>
        ''' Abre un archivo rss con los parametros pedidos
        ''' </summary>
        ''' <param name="fileName">Nombre del archivo, no debe incluir extension</param>
        ''' <param name="channelTitle"></param>
        ''' <param name="channelLink"></param>
        ''' <param name="itemGetIdentifier"></param>
        ''' <param name="channelDescription"></param>
        ''' <param name="channelPubDate"></param>
        ''' <param name="styleSheet"></param>
        ''' <remarks></remarks>
        Sub OpenRssFile(ByVal fileName As String, ByVal channelTitle As String, ByVal channelLink As String, ByVal itemGetIdentifier As String, _
                ByVal channelDescription As String, ByVal channelPubDate As String, Optional ByVal styleSheet As String = "")
            _fileWriter = New StreamWriter(_rssFilesPath & "\" & fileName & ".xml")
            _rssWriter = New System.Xml.XmlTextWriter(_fileWriter)

            channelPubDate = dateTo_RFC822(channelPubDate)
            _itemsLink = channelLink & "?" & itemGetIdentifier & "="

            'Encabezado
            _rssWriter.WriteStartDocument()
            _rssWriter.WriteStartElement("rss")
            _rssWriter.WriteAttributeString("version", "2.0")
            _rssWriter.WriteStartElement("channel")
            _rssWriter.WriteElementString("title", channelTitle)
            _rssWriter.WriteElementString("link", _siteURL & channelLink)
            _rssWriter.WriteElementString("description", channelDescription)
            _rssWriter.WriteElementString("pubDate", channelPubDate)
            _rssWriter.WriteElementString("generator", "RSS News Generator by Orbelink.com")
            _rssWriter.WriteElementString("copyright", "(c) 2006, Orbelink.com. All rights reserved.")

            _isWriting = True
        End Sub

        Sub CloseRssFile()
            If _isWriting Then
                _rssWriter.WriteEndElement()
                _rssWriter.WriteEndElement()
                _rssWriter.WriteEndDocument()
                _rssWriter.Close()
            End If
        End Sub

        Sub WriteItem(ByVal itemID As String, ByVal itemTitle As String, ByVal itemDescription As String, ByVal itemPubDate As String, ByVal itemCategory As String)
            itemPubDate = dateTo_RFC822(itemPubDate)
            If _isWriting Then
                _rssWriter.WriteStartElement("item")
                _rssWriter.WriteElementString("title", itemTitle)
                _rssWriter.WriteElementString("description", itemDescription)
                _rssWriter.WriteElementString("pubDate", itemPubDate)
                _rssWriter.WriteElementString("guid", _siteURL & _itemsLink & itemID)
                _rssWriter.WriteElementString("link", _siteURL & _itemsLink & itemID)
                _rssWriter.WriteElementString("category", itemCategory)
                _rssWriter.WriteEndElement()
            End If
        End Sub

        Function dateTo_RFC822(ByVal dateString As String) As String
            'Wed, 02 Aug 2006 15:00:00 +0200

            Dim dateFormated As String = ""
            Dim dateTime As New System.DateTime
            dateTime = System.DateTime.Parse(dateString)

            dateFormated &= dateTime.ToString("R")
            Return dateFormated
        End Function

        Public Sub WriteAveliableRSS(ByRef thePage As System.Web.UI.Page, Optional ByVal titlePrefix As String = "")
            Dim allXMl As ArrayList = ArchivoHandler.AllFilesInFolder(_rssFilesPath, "xml")
            Dim counter As Integer

            For counter = 0 To allXMl.Count - 1
                'Agrega CSS al head
                Dim linkXML As New HtmlLink()
                linkXML.Href = _rssFilesDir & "/" & allXMl(counter)
                linkXML.Attributes.Add("rel", "alternate")
                linkXML.Attributes.Add("type", "application/atom+xml")
                linkXML.Attributes.Add("title", titlePrefix & allXMl(counter))
                thePage.Header.Controls.Add(linkXML)
            Next
        End Sub

    End Class

End Namespace

