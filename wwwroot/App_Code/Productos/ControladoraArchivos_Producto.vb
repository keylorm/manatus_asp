Imports Orbelink.DBHandler
Imports Orbelink.Entity.Archivos
Imports Orbelink.Control.Archivos
Imports Orbelink.Entity.Productos

Namespace Orbelink.Control.Productos

    Public Class ControladoraArchivos_Producto
        Implements IControladorArchivos

        Dim connection As SQLServer
        Dim queryBuilder As QueryBuilder

        Sub New(ByRef theConnection As SQLServer)
            connection = theConnection
            queryBuilder = New QueryBuilder
        End Sub

        Public Function generarDataSet(ByVal id_producto As Integer, ByVal fileType As Integer) As System.Data.DataSet Implements IControladorArchivos.generarDataSet
            Dim dataSet As Data.DataSet
            Dim archivo As New Archivo
            Dim archivo_producto As New Archivo_Producto

            archivo.Fields.SelectAll()
            archivo_producto.Principal.ToSelect = True
            archivo_producto.Principal.AsName = "Principal"

            queryBuilder.Join.EqualCondition(archivo.Id_Archivo, archivo_producto.id_Archivo)
            archivo_producto.id_Producto.Where.EqualCondition(id_producto)

            If Not fileType = 5 Then
                archivo.FileType.Where.EqualCondition(fileType)
            End If

            queryBuilder.From.Add(archivo_producto)
            queryBuilder.From.Add(archivo)
            Dim consulta As String = queryBuilder.RelationalSelectQuery
            dataSet = connection.executeSelect(consulta)
            Return dataSet
        End Function

        Public Function insertarArchivoIntermedio(ByVal id_Archivo As Integer, ByVal id_producto As Integer, ByVal principal As Integer, ByVal pertenencia As Integer) As Boolean Implements IControladorArchivos.insertarArchivoIntermedio
            Dim resultado As Boolean = False
            Dim archivo_producto As New Archivo_Producto
            Dim archivo As New Archivo
            archivo_producto.Principal.Value = principal
            archivo_producto.id_Archivo.Value = id_Archivo
            archivo_producto.id_Producto.Value = id_producto
            archivo_producto.tipoPertenencia.Value = pertenencia
            connection.executeInsert(queryBuilder.InsertQuery(archivo_producto))
            Return True
        End Function

        Public Function ActualizarArchivoIntermedio(ByVal id_Archivo As Integer, ByVal id_dueno As Integer, ByVal principal As Integer) As Boolean Implements IControladorArchivos.ActualizarArchivoIntermedio
            Try
                Dim archivo_producto As New Archivo_Producto
                archivo_producto.Principal.Value = principal
                archivo_producto.id_Archivo.Where.EqualCondition(id_Archivo)
                archivo_producto.id_Producto.Where.EqualCondition(id_dueno)
                'archivo_producto.tipoPertenencia.Value = pertenencia
                connection.executeUpdate(queryBuilder.UpdateQuery(archivo_producto))
                Return True
            Catch ex As Exception
                Return False
            End Try
            Return False
        End Function
    End Class
End Namespace