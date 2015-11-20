Imports Orbelink.DBHandler
Imports Orbelink.Entity.Archivos
Imports Orbelink.Entity.Entidades
Imports Orbelink.Control.Archivos

Namespace Orbelink.Control.Entidades
    Public Class ControladoraArchivos_Entidad
        Implements IControladorArchivos

        Dim connection As SQLServer
        Dim queryBuilder As QueryBuilder

        Sub New(ByRef theConnection As SQLServer)
            connection = theConnection
            queryBuilder = New QueryBuilder
        End Sub

        Public Function generarDataSet(ByVal id_entidad As Integer, ByVal fileType As Integer) As System.Data.DataSet Implements IControladorArchivos.generarDataSet
            Dim dataSet As Data.DataSet
            Dim archivo As New Archivo
            Dim archivo_entidad As New Archivo_Entidad

            archivo.Fields.SelectAll()
            archivo_entidad.Principal.ToSelect = True
            archivo_entidad.Principal.AsName = "Principal"

            queryBuilder.Join.EqualCondition(archivo.Id_Archivo, archivo_entidad.id_Archivo)
            archivo_entidad.id_Entidad.Where.EqualCondition(id_entidad)

            If Not fileType = 5 Then
                archivo.FileType.Where.EqualCondition(fileType)
            End If

            queryBuilder.From.Add(archivo_entidad)
            queryBuilder.From.Add(archivo)
            Dim consulta As String = queryBuilder.RelationalSelectQuery
            dataSet = connection.executeSelect(consulta)
            Return dataSet
        End Function


        Public Function insertarArchivoIntermedio(ByVal id_Archivo As Integer, ByVal id_entidad As Integer, ByVal principal As Integer, ByVal pertenencia As Integer) As Boolean Implements IControladorArchivos.insertarArchivoIntermedio
            Dim resultado As Boolean = False
            Dim archivo_entidad As New Archivo_Entidad
            Dim archivo As New Archivo
            archivo_entidad.Principal.Value = principal
            archivo_entidad.id_Archivo.Value = id_Archivo
            archivo_entidad.id_Entidad.Value = id_entidad
            archivo_entidad.tipoPertenencia.Value = pertenencia
            connection.executeInsert(queryBuilder.InsertQuery(archivo_entidad))
            Return True
        End Function

        Public Function ActualizarArchivoIntermedio(ByVal id_Archivo As Integer, ByVal id_dueno As Integer, ByVal principal As Integer) As Boolean Implements IControladorArchivos.ActualizarArchivoIntermedio
            Try
                Dim archivoIntermedio As New Archivo_Entidad
                archivoIntermedio.Principal.Value = principal
                archivoIntermedio.id_Archivo.Value = id_Archivo
                archivoIntermedio.id_Entidad.Value = id_dueno
                'archivo_producto.tipoPertenencia.Value = pertenencia
                connection.executeUpdate(queryBuilder.UpdateQuery(archivoIntermedio))
                Return True
            Catch ex As Exception
                Return False
            End Try
            Return False
        End Function
    End Class
End Namespace