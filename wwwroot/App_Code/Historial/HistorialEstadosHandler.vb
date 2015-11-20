Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler
Imports Orbelink.Entity.Entidades

Namespace Orbelink.Control.HistorialEstados

    Public Class HistorialHandler

        Dim connection As SQLServer

        Sub New(ByRef theConnection As SQLServer)
            connection = theConnection
        End Sub

        Public Sub selectTipoEstado_BindListCollection(ByVal laControladora As IControladorHistorialEstados, ByVal theList As ListItemCollection)
            Dim tipo As ITipoEstado = laControladora.CreateNewTipoEstado
            Dim query As New QueryBuilder
            Dim dataset As Data.DataSet

            tipo.Id_TipoEstado.ToSelect = True
            tipo.Nombre.ToSelect = True

            query.From.Add(tipo)

            dataset = connection.executeSelect(query.RelationalSelectQuery)

            If dataset.Tables.Count > 0 Then
                If dataset.Tables(0).Rows.Count > 0 Then
                    Dim resultado As ArrayList = Orbelink.DBHandler.ObjectBuilder.TransformDataTable(dataset.Tables(0), tipo)

                    For Each tipoActual As ITipoEstado In resultado
                        Dim itemTemp As New ListItem(tipoActual.Nombre.Value, tipoActual.Id_TipoEstado.Value)
                        theList.Add(itemTemp)
                    Next
                End If
            End If
        End Sub

        Public Function NuevoEstado(ByVal laControladora As IControladorHistorialEstados, ByVal Id_Dueno As Integer, ByVal Id_tipoestado As Integer, ByVal Responsable As Integer, Optional ByVal comentario As String = "") As Boolean
            If Not existeEstado(laControladora, Id_Dueno, Id_tipoestado) Then
                If updateEstado_NoActual(laControladora, Id_Dueno) Then
                    If insertEstado(laControladora, Id_Dueno, Id_tipoestado, Responsable, comentario) Then
                        Return True
                    Else
                        Throw New Exception("No se pudo insertar un nuevo estado")
                    End If
                End If
            End If
            Return False
        End Function

        Public Function selectEstados(ByVal laControladora As IControladorHistorialEstados, ByVal Id_Dueno As Integer, Optional ByVal top As Integer = 0) As Data.DataTable
            Dim tipoEstado As ITipoEstado = laControladora.CreateNewTipoEstado
            Dim estado As IEstadosDueno = laControladora.CreateNewEstado

            Dim datos As Data.DataTable = Nothing
            Dim dataset As Data.DataSet
            Dim entidad As New Entidad
            Dim query As New QueryBuilder

            tipoEstado.Nombre.ToSelect = True
            query.Join.EqualCondition(tipoEstado.Id_TipoEstado, estado.Id_TipoEstado)

            entidad.NombreDisplay.ToSelect = True
            entidad.NombreDisplay.AsName = "Responsable"
            query.Join.EqualCondition(entidad.Id_entidad, estado.Responsable)

            estado.Fecha.ToSelect = True
            estado.Comentario.ToSelect = True

            If Id_Dueno > 0 Then
                estado.Id_Dueno.Where.EqualCondition(Id_Dueno)
            End If

            query.Top = top
            query.Orderby.Add(estado.Fecha, False)

            query.From.Add(tipoEstado)
            query.From.Add(estado)
            query.From.Add(entidad)

            dataset = connection.executeSelect(query.RelationalSelectQuery)

            If dataset.Tables.Count > 0 Then
                If dataset.Tables(0).Rows.Count > 0 Then
                    datos = dataset.Tables(0)

                    Dim columna As Data.DataColumn = datos.Columns(estado.Fecha.SelectIndex)
                    columna.ColumnName = "Fecha"

                    columna = datos.Columns(tipoEstado.Nombre.SelectIndex)
                    columna.ColumnName = "Estado"
                End If
            End If

            Return datos
        End Function

        Public Function existeEstado(ByVal controladora As IControladorHistorialEstados, ByVal Id_Dueno As Integer, ByVal Id_tipoestado As Integer) As Boolean
            Dim dataset As Data.DataSet
            Dim estado As IEstadosDueno = controladora.CreateNewEstado
            Dim query As New QueryBuilder

            estado.Fecha.ToSelect = True

            If Id_Dueno > 0 Then
                estado.Id_Dueno.Where.EqualCondition(Id_Dueno)
            End If

            If Id_tipoestado > 0 Then
                estado.Id_TipoEstado.Where.EqualCondition(Id_tipoestado)
            End If

            query.From.Add(estado)

            dataset = connection.executeSelect(query.RelationalSelectQuery)

            If dataset.Tables.Count > 0 Then
                If dataset.Tables(0).Rows.Count > 0 Then
                    Return True
                End If
            End If

            Return False
        End Function

        Private Function updateEstado_NoActual(ByVal controladora As IControladorHistorialEstados, ByVal Id_Dueno As Integer) As Boolean
            Try
                Dim query As New QueryBuilder
                Dim Estado As IEstadosDueno = controladora.CreateNewEstado
                Estado.Actual.Value = 0
                Estado.Id_Dueno.Where.EqualCondition(Id_Dueno)
                connection.executeUpdate(query.UpdateQuery(Estado))
                Return True
            Catch exc As Exception
            End Try
            Return False
        End Function

        Private Function insertEstado(ByVal controladora As IControladorHistorialEstados, ByVal Id_Dueno As Integer, ByVal Id_tipoestado As Integer, ByVal Responsable As Integer, ByVal Comentario As String) As Boolean
            Try
                Dim query As New QueryBuilder
                Dim Estado As IEstadosDueno = controladora.CreateNewEstado
                Estado.Actual.Value = 1
                Estado.Fecha.ValueUtc = Date.UtcNow
                Estado.Id_Dueno.Value = Id_Dueno
                Estado.Id_TipoEstado.Value = Id_tipoestado
                Estado.Responsable.Value = Responsable
                Estado.Comentario.Value = Comentario
                If connection.executeInsert(query.InsertQuery(Estado)) > 0 Then
                    Return True
                End If
            Catch exc As Exception
            End Try
            Return False
        End Function

    End Class

End Namespace