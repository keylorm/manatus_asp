Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler
Imports System.Data.SqlClient

Namespace Orbelink.DBHandler

    ''' <summary>
    ''' Permite administrar la conexión a la base de datos. Escribe a una archivo de log cuando sucede un error en el intento de ejecución de alguna consulta.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class SQLServer

        Dim classID As String = "SQLServer "
        Dim theConnectionString As String
        'Dim sqlPath As String
        Dim _catchNoTable As Boolean

        Public Sub New(ByVal datasource As String, ByVal catalog As String, ByVal user As String, ByVal password As String)
            theConnectionString = "Data Source=" & datasource & ";"
            theConnectionString &= "Initial Catalog=" & catalog & ";"
            theConnectionString &= "User ID=" & user & ";"
            theConnectionString &= "Password=" & password & ""
            _catchNoTable = True
        End Sub

        Public Sub New(ByVal connectionString As String)
            theConnectionString = connectionString
            _catchNoTable = True
        End Sub

        ''' <summary>
        ''' Propiedad que contiene el string de conexion
        ''' </summary>
        ''' <value></value>
        ''' <returns>El string de conexion</returns>
        ''' <remarks></remarks>
        Public ReadOnly Property connectionString() As String
            Get
                Return theConnectionString
            End Get
        End Property

        Public Property catchNoTable() As Boolean
            Get
                Return _catchNoTable
            End Get
            Set(ByVal value As Boolean)
                _catchNoTable = value
            End Set
        End Property

        ''' <summary>
        ''' Obtiene el ultimo registro de la tabla segun la llave
        ''' </summary>
        ''' <param name="theTableName">El nombre de la tabla en la DB</param>
        ''' <param name="theKeyName">El nombre del campo llave</param>
        ''' <returns>El ultimo id registrado en la tabla</returns>
        ''' <remarks></remarks>
        Public Function lastKey(ByVal theTableName As String, ByVal theKeyName As String) As Integer
            Dim _lastKey As Integer
            Dim dataTable As Data.DataTable
            Dim query As String = "SELECT MAX(" & theKeyName & ") AS " & theKeyName & " FROM " & theTableName
            dataTable = executeSelect_DT(query)
            Try
                _lastKey = dataTable.Rows(0).Item(0)
            Catch ex As Exception
                _lastKey = 0
            End Try
            Return _lastKey
        End Function

        '''' <summary>
        '''' Obtiene el ultimo registro de la tabla segun la llave
        '''' </summary>
        '''' <param name="theTableName">El nombre de la tabla en la DB</param>
        '''' <param name="theKeyName">El nombre del campo llave</param>
        '''' <returns>El ultimo id registrado en la tabla</returns>
        '''' <remarks></remarks>
        'Public Function lastKey(ByRef theDBTable As DBHandler.DBTable) As Orbelink.DBHandler.PrimaryKey
        '    Dim _lastKey As Orbelink.DBHandler.PrimaryKey
        '    lastKey.Item(0).Field.Value

        '    Dim dataTable As Data.DataTable
        '    Dim query As String = "SELECT MAX(" & theKeyName & ") AS " & theKeyName & " FROM " & theDBTable.TableName
        '    dataSet = executeSelect_DT(query)
        '    Try
        '        _lastKey = dataSet.Tables("Table").Rows(0).Item(0)
        '    Catch ex As Exception
        '        _lastKey = 0
        '    End Try
        '    Return _lastKey
        'End Function

        ''' <summary>
        ''' Ejecuta query de select
        ''' </summary>
        ''' <param name="theQuery">El string con el query</param>
        ''' <returns>El DataSet resultante de la consulta</returns>
        ''' <remarks></remarks>
        Public Function executeSelect(ByVal theQuery As String) As Data.DataSet
            Dim sql_Connection As New SqlConnection(Me.connectionString)
            Dim sql_DataAdapter As New SqlDataAdapter
            Dim sql_command As New SqlCommand(theQuery, sql_Connection)
            Dim dataSet As New System.Data.DataSet
            Try
                sql_Connection.Open()
                sql_DataAdapter.SelectCommand = sql_command
                sql_DataAdapter.Fill(dataSet, "Table")
                sql_command.Dispose()
                sql_Connection.Close()
            Catch exc As SqlException
                verifySQLException(exc, theQuery, Me._catchNoTable)
            End Try
            Return dataSet
        End Function

        ''' <summary>
        ''' Ejecuta query de select
        ''' </summary>
        ''' <param name="theQuery">El string con el query</param>
        ''' <returns>El DataSet resultante de la consulta</returns>
        ''' <remarks></remarks>
        Public Function executeSelect_DT(ByVal theQuery As String) As System.Data.DataTable
            Dim sql_Connection As New SqlConnection(Me.connectionString)
            Dim sql_DataAdapter As New SqlDataAdapter
            Dim sql_command As New SqlCommand(theQuery, sql_Connection)
            Dim dataTable As New System.Data.DataTable
            Try
                sql_Connection.Open()
                sql_DataAdapter.SelectCommand = sql_command
                Dim cantidad As Integer = sql_DataAdapter.Fill(dataTable)
                sql_command.Dispose()
                sql_Connection.Close()
            Catch exc As SqlException
                verifySQLException(exc, theQuery, Me._catchNoTable)
            End Try
            Return dataTable
        End Function

        ''' <summary>
        ''' Ejecuta query de update
        ''' </summary>
        ''' <param name="theQuery">El string con el query</param>
        ''' <returns>Cantidad de filas afectadas por la consulta</returns>
        ''' <remarks></remarks>
        Public Function executeUpdate(ByVal theQuery As String) As Integer
            Dim sql_Connection As New SqlConnection(Me.connectionString)
            Dim sql_DataAdapter As New SqlDataAdapter
            Dim sql_command As New SqlCommand(theQuery, sql_Connection)
            Dim rowsAfected As Integer = -1
            Try
                sql_Connection.Open()
                sql_DataAdapter.UpdateCommand() = sql_command
                rowsAfected = sql_DataAdapter.UpdateCommand.ExecuteNonQuery()
                sql_command.Dispose()
                sql_Connection.Close()
            Catch exc As SqlException
                verifySQLException(exc, theQuery, False)
            End Try
            Return rowsAfected
        End Function

        ''' <summary>
        ''' Ejecuta query de delete
        ''' </summary>
        ''' <param name="theQuery">El string con el query</param>
        ''' <returns>Cantidad de filas afectadas por la consulta</returns>
        ''' <remarks></remarks>
        Public Function executeDelete(ByVal theQuery As String) As Integer
            Dim sql_Connection As New SqlConnection(Me.connectionString)
            Dim sql_DataAdapter As New SqlDataAdapter
            Dim sql_command As New SqlCommand(theQuery, sql_Connection)
            Dim rowsAfected As Integer = -1
            Try
                sql_Connection.Open()
                sql_DataAdapter.DeleteCommand = sql_command
                rowsAfected = sql_DataAdapter.DeleteCommand.ExecuteNonQuery()
                sql_command.Dispose()
                sql_Connection.Close()
            Catch exc As SqlException
                verifySQLException(exc, theQuery, False)
            End Try
            Return rowsAfected
        End Function

        Public Function executeCommand(ByVal theQuery As String) As Integer
            Dim sql_Connection As New SqlConnection(Me.connectionString)
            Dim sql_DataAdapter As New SqlDataAdapter
            Dim sql_command As New SqlCommand(theQuery, sql_Connection)
            Dim rowsAfected As Integer = -1
            Try
                sql_Connection.Open()
                rowsAfected = sql_command.ExecuteNonQuery()
                sql_command.Dispose()
                sql_Connection.Close()
            Catch exc As SqlException
                verifySQLException(exc, theQuery, False)
            End Try
            Return rowsAfected
        End Function

        ''' <summary>
        ''' Ejecuta query de insert
        ''' </summary>
        ''' <param name="theQuery">El string con el query</param>
        ''' <returns>El resultado de la consulta</returns>
        ''' <remarks></remarks>
        Public Function executeInsert(ByVal theQuery As String) As Integer
            Dim sql_Connection As New SqlConnection(Me.connectionString)
            Dim sql_DataAdapter As New SqlDataAdapter
            Dim sql_command As New SqlCommand(theQuery, sql_Connection)
            Dim result As Integer
            Try
                sql_Connection.Open()
                sql_DataAdapter.InsertCommand = sql_command
                result = sql_DataAdapter.InsertCommand.ExecuteNonQuery()
                sql_command.Dispose()
                sql_Connection.Close()
            Catch exc As SqlException
                verifySQLException(exc, theQuery, False)
            End Try
            Return result
        End Function

        ''' <summary>
        ''' Verifica la excepcion producto de alguna consulta
        ''' </summary>
        ''' <param name="theException">La excepcion de SQL</param>
        ''' <remarks></remarks>
        Private Sub verifySQLException(ByVal theException As SqlException, ByVal theQuery As String, ByVal CatchNoTableError As Boolean)
            If SaveLogErr(theException, theQuery, CatchNoTableError) Then
                Throw New DBException(classID, theException, theQuery)
            End If
        End Sub

        ''' <summary>
        ''' Escribe a una archivo de log cuando sucede un error en el intento de ejecución de alguna consulta.
        ''' </summary>
        ''' <param name="theException"></param>
        ''' <param name="theQuery"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Private Function SaveLogErr(ByVal theException As SqlException, ByVal theQuery As String, ByVal CatchNoTableError As Boolean) As Boolean
            Orbelink.Helpers.Logger.RegistrarSQLError(theException, connectionString, theQuery, HttpContext.Current)
            Return True
        End Function

    End Class

End Namespace