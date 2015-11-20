Imports Orbelink.DBHandler
Imports Orbelink.Entity.Archivos
Imports Orbelink.Control.Archivos
Imports Orbelink.Entity.Publicaciones

Public Class ControladoraArchivos_Publicacion
    Implements IControladorArchivos

    Dim connection As SQLServer
    Dim queryBuilder As QueryBuilder

    Sub New(ByRef theConnection As SQLServer)
        connection = theConnection
        queryBuilder = New QueryBuilder
    End Sub

    Public Function generarDataSet(ByVal id_Publicacion As Integer, ByVal fileType As Integer) As System.Data.DataSet Implements IControladorArchivos.generarDataSet
        Dim dataSet As Data.DataSet
        Dim archivo As New Archivo
        Dim archivo_Publicacion As New Archivo_Publicacion

        archivo.Fields.SelectAll()
        archivo_Publicacion.Principal.ToSelect = True
        archivo_Publicacion.Principal.AsName = "Principal"

        queryBuilder.Join.EqualCondition(archivo.Id_Archivo, archivo_Publicacion.id_Archivo)
        archivo_Publicacion.id_Publicacion.Where.EqualCondition(id_Publicacion)

        If Not fileType = 5 Then
            archivo.FileType.Where.EqualCondition(fileType)
        End If

        queryBuilder.From.Add(archivo_Publicacion)
        queryBuilder.From.Add(archivo)
        Dim consulta As String = queryBuilder.RelationalSelectQuery
        dataSet = connection.executeSelect(consulta)
        Return dataSet
    End Function

    Public Function insertarArchivoIntermedio(ByVal id_Archivo As Integer, ByVal id_Publicacion As Integer, ByVal principal As Integer, ByVal pertenencia As Integer) As Boolean Implements IControladorArchivos.insertarArchivoIntermedio
        Dim resultado As Boolean = False
        Dim archivo_Publicacion As New Archivo_Publicacion
        Dim archivo As New Archivo
        archivo_Publicacion.Principal.Value = principal
        archivo_Publicacion.id_Archivo.Value = id_Archivo
        archivo_Publicacion.id_Publicacion.Value = id_Publicacion
        archivo_Publicacion.tipoPertenencia.Value = pertenencia
        connection.executeInsert(queryBuilder.InsertQuery(archivo_Publicacion))
        Return True
    End Function

    Public Function ActualizarArchivoIntermedio(ByVal id_Archivo As Integer, ByVal id_dueno As Integer, ByVal principal As Integer) As Boolean Implements IControladorArchivos.ActualizarArchivoIntermedio
        Try
            Dim archivoIntermedio As New Archivo_Publicacion
            archivoIntermedio.Principal.Value = principal
            archivoIntermedio.id_Archivo.Where.EqualCondition(id_Archivo)
            archivoIntermedio.id_Publicacion.Where.EqualCondition(id_dueno)
            'archivo_producto.tipoPertenencia.Value = pertenencia
            connection.executeUpdate(queryBuilder.UpdateQuery(archivoIntermedio))
            Return True
        Catch ex As Exception
            Return False
        End Try
        Return False
    End Function
End Class
