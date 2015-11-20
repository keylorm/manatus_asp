Imports Orbelink.DBHandler
Imports Orbelink.Entity.Bitacora

Namespace Orbelink.Control.Bitacoras

    Public Class BitacoraHandler

        Dim connection As SQLServer
        Dim queryBuilder As QueryBuilder

        Sub New(ByRef theConnection As SQLServer)
            connection = theConnection
            queryBuilder = New QueryBuilder
        End Sub

        Public Function NuevaBitacora(ByVal Id_Tema As Integer, ByVal EscritoPor As Integer, ByVal Id_tipocomunicacion As Integer, ByVal comentario As String) As Boolean
            Return insertarBitacora(Id_Tema, EscritoPor, comentario, Id_tipocomunicacion)
        End Function

        Public Function selectBitacoras_byDueno(ByVal laControladora As IControladorBitacora, ByVal id_Dueno As Integer) As Data.DataTable
            Return laControladora.selectBitacoras_ByDueno(id_Dueno)
        End Function

        Private Function insertarBitacora(ByVal Id_Tema As Integer, ByVal EscritoPor As Integer, ByVal comentario As String, ByVal Id_tipocomunicacion As Integer) As Boolean
            Try
                Dim Bitacora As New BitacoraDB

                Bitacora.Fecha.ValueUtc = Date.UtcNow
                Bitacora.Comentario.Value = comentario
                Bitacora.EscritoPor.Value = EscritoPor
                Bitacora.Id_Tema.Value = Id_Tema
                Bitacora.Id_tipocomunicacion.Value = Id_tipocomunicacion
                Bitacora.Numero_Bitacora.Value = ultimoNumeroBitacora(Id_Tema) + 1

                connection.executeInsert(queryBuilder.InsertQuery(Bitacora))
                Return True
            Catch exc As Exception
                Return False
            End Try
        End Function

        Private Function ultimoNumeroBitacora(ByVal id_Tema As Integer) As Integer
            Dim BitacoraDB As New BitacoraDB
            Dim numero As Integer = 0

            BitacoraDB.Id_Tema.Where.EqualCondition(id_Tema)
            BitacoraDB.Fields.SelectAll()

            queryBuilder.Top = 1
            queryBuilder.Orderby.Add(BitacoraDB.Numero_Bitacora, False)

            queryBuilder.From.Add(BitacoraDB)

            Dim dataTable As Data.DataTable = connection.executeSelect_DT(queryBuilder.RelationalSelectQuery)

            If dataTable.Rows.Count > 0 Then
                ObjectBuilder.CreateObject(dataTable, 0, BitacoraDB)
                numero = BitacoraDB.Numero_Bitacora.Value
            End If

            Return numero
        End Function

        'Tema
        Public Function CrearTema(ByVal laControladora As IControladorBitacora, ByVal elnombre As String, ByVal id_Dueno As Integer) As Boolean
            If insertarTema(elnombre) Then
                Dim tema As New Tema
                Dim id_Insertado As Integer = connection.lastKey(tema.TableName, tema.Id_Tema.Name)
                If laControladora.insertarTemaIntermedio(id_Insertado, id_Dueno) Then
                    Return True
                End If
            End If
            Return False
        End Function

        Public Function selectTemas(ByVal laControladora As IControladorBitacora, ByVal id_Dueno As Integer) As Data.DataSet
            Return laControladora.generarDataSet(id_Dueno)
        End Function

        Public Sub selectTemas_BindListCollection(ByVal laControladora As IControladorBitacora, ByVal theList As ListItemCollection, ByVal id_Dueno As Integer)
            Dim dataset As Data.DataSet
            Dim tema As New Tema
            Dim query As New QueryBuilder

            tema.Fields.SelectAll()

            dataset = laControladora.generarDataSet(id_Dueno)

            If dataset.Tables.Count > 0 Then
                If dataset.Tables(0).Rows.Count > 0 Then
                    theList.Clear()
                    Dim resultado As ArrayList = Orbelink.DBHandler.ObjectBuilder.TransformDataTable(dataset.Tables(0), tema)

                    For Each actual As Tema In resultado
                        Dim itemTemp As New ListItem(actual.Nombre.Value, actual.Id_Tema.Value)
                        theList.Add(itemTemp)
                    Next
                End If
            End If
        End Sub

        Private Function insertarTema(ByVal elNombre As String) As Boolean
            Try
                Dim tema As New Tema

                tema.Nombre.Value = elNombre
                tema.FechaCreado.ValueUtc = Date.UtcNow

                connection.executeInsert(queryBuilder.InsertQuery(tema))
                Return True
            Catch exc As Exception
                Return False
            End Try
        End Function

        Public Function updateTema(ByVal nombre As String, ByVal id_Tema As Integer) As Boolean
            Dim Tema As Tema
            Try
                Tema = New Tema()
                Tema.Nombre.Value = nombre
                Tema.Id_Tema.Where.EqualCondition(id_Tema)
                connection.executeUpdate(queryBuilder.UpdateQuery(Tema))
                Return True
            Catch exc As Exception
                Return False
            End Try
        End Function

        Public Function deleteTema(ByVal id_Tema As Integer) As Boolean
            Dim Tema As New Tema
            Try
                Tema.Id_Tema.Where.EqualCondition(id_Tema)
                connection.executeDelete(queryBuilder.DeleteQuery(Tema))
                Return True
            Catch exc As Exception
                Return False
            End Try
        End Function

        'Tipo Comunicacion
        Public Sub selectTipoComunicacion_BindListCollection(ByVal theList As ListItemCollection)
            Dim tipo As New TipoComunicacion
            Dim query As New QueryBuilder

            tipo.Fields.SelectAll()

            query.From.Add(tipo)

            Dim dataTable As Data.DataTable = connection.executeSelect_DT(query.RelationalSelectQuery)

            If dataTable.Rows.Count > 0 Then
                theList.Clear()
                Dim resultado As ArrayList = Orbelink.DBHandler.ObjectBuilder.TransformDataTable(dataTable, tipo)

                For Each tipoActual As TipoComunicacion In resultado
                    Dim itemTemp As New ListItem(tipoActual.Nombre.Value, tipoActual.Id_TipoComunicacion.Value)
                    theList.Add(itemTemp)
                Next
            End If
        End Sub
    End Class

End Namespace