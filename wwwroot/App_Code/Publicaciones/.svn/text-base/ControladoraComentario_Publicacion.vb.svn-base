Imports Orbelink.DBHandler
Imports Orbelink.Entity.Comentarios
Imports Orbelink.Control.Comentarios
Imports Orbelink.Entity.Publicaciones
Imports Orbelink.Entity.Entidades

Namespace Orbelink.Control.Publicaciones

    Public Class ControladoraComentario_Publicacion
        Implements IControladorComentario

        Dim _connString As String
        Sub New(ByRef theConnectionString As String)
            _connString = theConnectionString
        End Sub

        Public Function selectComentarios(ByVal id_dueno As Integer) As ComentarioHandler.ResultSelect Implements Orbelink.Control.Comentarios.IControladorComentario.selectComentarios
            Dim Comentario As New Comentario
            Dim Comentario_Publicacion As New Comentario_Publicacion
            Dim entidad As New Entidad
            Dim resultado As New ComentarioHandler.ResultSelect

            Comentario.Titulo.ToSelect = True
            Comentario.Fecha.ToSelect = True
            Comentario.Comentario.ToSelect = True
            entidad.NombreDisplay.ToSelect = True

            Dim queryBuilder As New QueryBuilder
            queryBuilder.Join.EqualCondition(Comentario.Id_Comentario, Comentario_Publicacion.id_Comentario)
            queryBuilder.Join.EqualCondition(entidad.Id_entidad, Comentario.Id_Entidad)

            Comentario_Publicacion.id_Publicacion.Where.EqualCondition(id_dueno)
            Comentario_Publicacion.id_Comentario.ToSelect = True
            Comentario_Publicacion.id_Publicacion.ToSelect = True

            queryBuilder.From.Add(entidad)
            queryBuilder.From.Add(Comentario_Publicacion)
            queryBuilder.From.Add(Comentario)
            Dim consulta As String = queryBuilder.RelationalSelectQuery

            Dim connection As New SQLServer(_connString)
            resultado.datos = connection.executeSelect_DT(consulta)

            If resultado.datos.Columns.Count > 0 Then
                resultado.datos.Columns(entidad.NombreDisplay.SelectIndex).ColumnName = "Autor"
                resultado.datos.Columns(Comentario.Fecha.SelectIndex).ColumnName = "Fecha"
                resultado.datos.Columns(Comentario.Titulo.SelectIndex).ColumnName = "Titulo"
                resultado.datos.Columns(Comentario.Comentario.SelectIndex).ColumnName = " "
                resultado.datos.Columns(Comentario_Publicacion.id_Comentario.SelectIndex).ColumnMapping = Data.MappingType.Hidden
                resultado.datos.Columns(Comentario_Publicacion.id_Publicacion.SelectIndex).ColumnMapping = Data.MappingType.Hidden
            End If
            resultado.DataFieldNames = New String() {Comentario_Publicacion.id_Comentario.Name, Comentario_Publicacion.id_Publicacion.Name}
            Return resultado
        End Function

        Public Function insertarComentarioIntermedio(ByVal id_Comentario As Integer, ByVal id_dueno As Integer) As Boolean Implements Orbelink.Control.Comentarios.IControladorComentario.insertarComentarioIntermedio
            Dim resultado As Boolean = False
            Dim Comentario_Publicacion As New Comentario_Publicacion
            Dim Comentario As New Comentario

            Comentario_Publicacion.id_Publicacion.Value = id_dueno
            Comentario_Publicacion.id_Comentario.Value = id_Comentario

            Dim connection As New SQLServer(_connString)
            Dim queryBuilder As New QueryBuilder
            connection.executeInsert(queryBuilder.InsertQuery(Comentario_Publicacion))
            Return True
        End Function

    End Class

End Namespace