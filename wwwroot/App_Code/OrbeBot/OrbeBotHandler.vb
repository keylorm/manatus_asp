Imports Microsoft.VisualBasic
Imports Orbelink.Control.OrbeEventos
Imports Orbelink.Entity.OrbeBot

Namespace Orbelink.Control.OrbeBot

    Public Class OrbeBotHandler

        Protected WithEvents bot As OrbeBot

        'Instancias de clases
        Protected connection As Orbelink.DBHandler.SQLServer
        Protected queryBuilder As Orbelink.DBHandler.QueryBuilder
        Protected orbeEventsHandler As OrbeEventsHandler

        Public Sub New(ByVal theConnection As Orbelink.DBHandler.SQLServer, ByVal theQueryBuilder As Orbelink.DBHandler.QueryBuilder, ByVal theOrbeEventsHandler As OrbeEventsHandler)
            connection = theConnection
            queryBuilder = theQueryBuilder
            orbeEventsHandler = theOrbeEventsHandler
            bot = New OrbeBot()
        End Sub

        Public Sub DoSearch()
            Dim feedsThread As New System.Threading.Thread(AddressOf selectFeeds)
            feedsThread.Start()
        End Sub

        'Feed
        Private Sub selectFeeds()
            Dim feed As New Feed
            Dim dataset As New Data.DataSet

            'Consulta sql
            feed.Fields.SelectAll()

            'OJO
            feed.Id_Feed.Where.EqualCondition(6)

            queryBuilder.From.Add(feed)
            dataset = connection.executeSelect(queryBuilder.RelationalSelectQuery)

            If dataset.Tables.Count > 0 Then
                If dataset.Tables(0).Rows.Count > 0 Then
                    Dim result_Feed As ArrayList = Orbelink.DBHandler.ObjectBuilder.TransformDataTable(dataset.Tables(0), feed)

                    For counter As Integer = 0 To result_Feed.Count - 1
                        Dim act_Feed As Feed = result_Feed(counter)
                        selectSearchKeywords_Feed(act_Feed.Id_Feed.Value, act_Feed.Source.Value)
                    Next

                End If
            End If
        End Sub

        'SearchKeywords_Feed
        Private Sub selectSearchKeywords_Feed(ByVal id_Feed As Integer, ByVal SourceURL As String)
            Dim dataset As Data.DataSet
            Dim SearchKeywords_Feed As New SearchKeywords_Feed
            Dim SearchKeywords As New SearchKeywords

            'Para agrupar por id
            Dim losKeywords As New System.Collections.Generic.List(Of String)

            'Consulta sql
            SearchKeywords_Feed.Fields.SelectAll()

            queryBuilder.Join.EqualCondition(SearchKeywords.Id_Keyword, SearchKeywords_Feed.Id_Keyword)
            SearchKeywords.Nombre.ToSelect = True

            queryBuilder.From.Add(SearchKeywords)
            queryBuilder.From.Add(SearchKeywords_Feed)
            dataset = connection.executeSelect(queryBuilder.RelationalSelectQuery)

            If dataset.Tables.Count > 0 Then
                If dataset.Tables(0).Rows.Count > 0 Then
                    Dim result_SearchKeywords_Feed As ArrayList = Orbelink.DBHandler.ObjectBuilder.TransformDataTable(dataset.Tables(0), SearchKeywords_Feed)
                    Dim result_SearchKeywords As ArrayList = Orbelink.DBHandler.ObjectBuilder.TransformDataTable(dataset.Tables(0), SearchKeywords)

                    For counter As Integer = 0 To result_SearchKeywords_Feed.Count - 1
                        Dim act_SearchKeywords_Feed As SearchKeywords_Feed = result_SearchKeywords_Feed(counter)
                        Dim act_SearchKeywords As SearchKeywords = result_SearchKeywords(counter)

                        'Agrupa los keywords
                        losKeywords.Add(act_SearchKeywords.Nombre.Value)
                    Next

                    If losKeywords.Count > 0 Then
                        'Envia al bot a buscar
                        Dim theSearch As New OrbeBot.BotSearch
                        theSearch.RootURL = SourceURL
                        theSearch.OnlySameSite = True
                        theSearch.Keywords = losKeywords.ToArray
                        theSearch.Identifier = id_Feed
                        bot.BeASpider(theSearch)
                    End If
                End If
            End If
        End Sub

        Protected Sub searchCompleted(ByVal theResults As OrbeBot.BotResult()) Handles bot.Completed
            Dim comment As New StringBuilder
            Dim id_Feed As Integer = 0

            'If theResults.Length > 0 Then
            '    id_Feed = theResults(theResults.Length - 1).Identifier
            'End If

            For Each result As OrbeBot.BotResult In theResults
                comment.Append(result.Keyword)
                comment.Append(": ")
                comment.Append(result.TimesFound)
                comment.AppendLine()
            Next

            comment.AppendLine()
            orbeEventsHandler.AgregarSuceso(0, "OR-01", 1, theResults(theResults.Length - 1).ReferenceURL, comment.ToString, "id_Feed", theResults(theResults.Length - 1).Identifier)
        End Sub

    End Class

End Namespace
