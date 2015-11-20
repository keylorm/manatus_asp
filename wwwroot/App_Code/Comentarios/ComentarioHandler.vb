Imports Orbelink.DBHandler
Imports Orbelink.Entity.Comentarios

Namespace Orbelink.Control.Comentarios

    Public Class ComentarioHandler

        Dim _connString As String
        Dim queryBuilder As QueryBuilder

        Sub New(ByRef theConnectionString As String)
            _connString = theConnectionString
            queryBuilder = New QueryBuilder
        End Sub

        'Comentario
        Public Function CrearComentario(ByVal laControladora As IControladorComentario, ByVal elId_Entidad As Integer, ByVal elTitulo As String, ByVal elComentario As String, ByVal elRating As Integer, ByVal id_Dueno As Integer) As Integer
            Dim idInsertado As Integer = 0
            If insertarComentario(elId_Entidad, elTitulo, elComentario, elRating) Then
                Dim Comentario As New Comentario

                Dim connection As New SQLServer(_connString)
                idInsertado = connection.lastKey(Comentario.TableName, Comentario.Id_Comentario.Name)
                Try
                    If Not laControladora.insertarComentarioIntermedio(idInsertado, id_Dueno) Then
                        idInsertado = 0
                        deleteComentario(idInsertado)
                    End If
                Catch ex As Exception
                    idInsertado = 0
                    deleteComentario(idInsertado)
                End Try
            End If
            Return idInsertado
        End Function

        Public Function ConsultarComentario(ByVal id_Comentario As Integer) As Comentario
            Dim theComentario As New Comentario
            theComentario.Fields.SelectAll()
            theComentario.Id_Comentario.Where.EqualCondition(id_Comentario)
            Dim connection As New SQLServer(_connString)
            Dim query As New QueryBuilder
            Dim resultTable As Data.DataTable = connection.executeSelect_DT(query.SelectQuery(theComentario))
            If resultTable.Rows.Count > 0 Then
                Return ObjectBuilder.CreateDBTable_BySelectIndex(resultTable.Rows(0), theComentario)
            End If
            Return Nothing
        End Function

        Public Function EliminarComentario(ByVal id_Comentario As Integer) As Boolean
            Return deleteComentario(id_Comentario)
        End Function

        'Comentario select
        Public Structure ResultSelect
            Dim datos As Data.DataTable
            Dim DataFieldNames() As String
        End Structure

        Public Function selectComentarios(ByVal laControladora As IControladorComentario, ByVal id_Dueno As Integer) As ResultSelect
            Return laControladora.selectComentarios(id_Dueno)
        End Function

        Public Sub selectComentarios_BindListCollection(ByVal laControladora As IControladorComentario, ByVal theList As ListItemCollection, ByVal id_Dueno As Integer)
            Dim Comentario As New Comentario
            Dim query As New QueryBuilder

            Comentario.Fields.SelectAll()

            Dim result As ResultSelect = laControladora.selectComentarios(id_Dueno)
            If result.datos.Rows.Count > 0 Then
                theList.Clear()
                Dim resultado As ArrayList = Orbelink.DBHandler.ObjectBuilder.TransformDataTable(result.datos, Comentario)
                For Each actual As Comentario In resultado
                    Dim itemTemp As New ListItem(actual.Titulo.Value, actual.Id_Comentario.Value)
                    theList.Add(itemTemp)
                Next
            End If
        End Sub

        Public Sub selectComentarios_ForNotas(ByVal laControladora As IControladorComentario, ByVal id_Dueno As Integer, ByVal theRepeater As Repeater, ByVal theDelegate As EventHandler)
            'ojo voy por aqui
            Dim datos As New Data.DataTable
            datos.Columns.Add()

            Dim theRow As Data.DataRow = datos.NewRow()
            datos.Rows.Add(theRow)

            theRepeater.DataSource = datos
            theRepeater.DataBind()
            For Each item As RepeaterItem In theRepeater.Items
                Dim tbx_Nota As TextBox = item.FindControl("tbx_Nota")
                AddHandler tbx_Nota.TextChanged, theDelegate
            Next
        End Sub

        'Privadas db
        Private Function insertarComentario(ByVal elId_Entidad As Integer, ByVal elTitulo As String, ByVal elComentario As String, ByVal elRating As Integer) As Boolean
            Try
                Dim Comentario As New Comentario
                Comentario.Comentario.Value = elComentario
                Comentario.Titulo.Value = elTitulo
                Comentario.Fecha.ValueUtc = Date.Now
                Comentario.Id_Entidad.Value = elId_Entidad
                Comentario.Rating.Value = elRating
                Comentario.Valido.Value = "1"

                Dim connection As New SQLServer(_connString)
                connection.executeInsert(queryBuilder.InsertQuery(Comentario))
                Return True
            Catch exc As Exception
                Return False
            End Try
        End Function

        Public Function updateComentario(ByVal id_Comentario As Integer, ByVal elComentario As String, ByVal elRating As Integer) As Boolean
            Try
                Dim Comentario As New Comentario()
                Comentario.Comentario.Value = elComentario
                Comentario.Rating.Value = elRating
                Comentario.Id_Comentario.Where.EqualCondition(id_Comentario)

                Dim connection As New SQLServer(_connString)
                connection.executeUpdate(queryBuilder.UpdateQuery(Comentario))
                Return True
            Catch exc As Exception
                Return False
            End Try
        End Function

        Public Function deleteComentario(ByVal id_Comentario As Integer) As Boolean
            Try
                Dim Comentario As New Comentario
                Comentario.Id_Comentario.Where.EqualCondition(id_Comentario)

                Dim connection As New SQLServer(_connString)
                connection.executeDelete(queryBuilder.DeleteQuery(Comentario))
                Return True
            Catch exc As Exception
                Return False
            End Try
        End Function

    End Class

End Namespace