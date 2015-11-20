Imports Orbelink.DBHandler
Imports Orbelink.Entity.Comentarios
Imports Orbelink.Control.Comentarios
Imports Orbelink.Entity.Productos
Imports Orbelink.Entity.Entidades

Namespace Orbelink.Control.Productos

    Public Class ControladoraComentario_Producto
        Implements IControladorComentario

        Dim connection As SQLServer
        Dim queryBuilder As QueryBuilder

        Sub New(ByRef theConnection As SQLServer)
            connection = theConnection
            queryBuilder = New QueryBuilder
        End Sub

        Public Function selectComentarios(ByVal id_dueno As Integer) As ComentarioHandler.ResultSelect Implements Orbelink.Control.Comentarios.IControladorComentario.selectComentarios
            Dim Comentario As New Comentario
            Dim Comentario_Producto As New Comentario_Producto
            Dim entidad As New Entidad
            Dim resultado As New ComentarioHandler.ResultSelect

            Comentario.Titulo.ToSelect = True
            Comentario.Fecha.ToSelect = True
            Comentario.Comentario.ToSelect = True
            entidad.NombreDisplay.ToSelect = True

            queryBuilder.Join.EqualCondition(Comentario.Id_Comentario, Comentario_Producto.id_Comentario)
            queryBuilder.Join.EqualCondition(entidad.Id_entidad, Comentario.Id_Entidad)

            Comentario_Producto.id_Producto.Where.EqualCondition(id_dueno)
            Comentario_Producto.id_Comentario.ToSelect = True
            Comentario_Producto.id_Producto.ToSelect = True

            queryBuilder.From.Add(Comentario_Producto)
            queryBuilder.From.Add(Comentario)
            queryBuilder.From.Add(entidad)
            Dim consulta As String = queryBuilder.RelationalSelectQuery
            resultado.datos = connection.executeSelect_DT(consulta)

            If resultado.datos.Rows.Count > 0 Then
                resultado.datos.Columns(entidad.NombreDisplay.SelectIndex).ColumnName = "Autor"
                resultado.datos.Columns(Comentario.Fecha.SelectIndex).ColumnName = "Fecha"
                resultado.datos.Columns(Comentario.Titulo.SelectIndex).ColumnName = "Titulo"
                resultado.datos.Columns(Comentario.Comentario.SelectIndex).ColumnName = " "
                resultado.datos.Columns(Comentario_Producto.id_Comentario.SelectIndex).ColumnMapping = Data.MappingType.Hidden
                resultado.datos.Columns(Comentario_Producto.id_Producto.SelectIndex).ColumnMapping = Data.MappingType.Hidden
            End If
            resultado.DataFieldNames = New String() {Comentario_Producto.id_Comentario.Name, Comentario_Producto.id_Producto.Name}
            Return resultado
        End Function

        Public Function insertarComentarioIntermedio(ByVal id_Comentario As Integer, ByVal id_dueno As Integer) As Boolean Implements Orbelink.Control.Comentarios.IControladorComentario.insertarComentarioIntermedio
            Dim resultado As Boolean = False
            Dim Comentario_Producto As New Comentario_Producto
            Dim Comentario As New Comentario

            Comentario_Producto.id_Producto.Value = id_dueno
            Comentario_Producto.id_Comentario.Value = id_Comentario
            connection.executeInsert(queryBuilder.InsertQuery(Comentario_Producto))
            Return True
        End Function

    End Class

End Namespace