Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler
Imports Orbelink.Entity.Acciones
Imports Orbelink.Crypter
Imports Orbelink.Control.Facturas

Namespace Orbelink.Control.Acciones
    Public Class MachoteAccionesHandler

        Dim _connString As String

        Sub New(ByRef theConnectionString As String)
            _connString = theConnectionString
        End Sub

        'Enums
        'Public Enum EstadosMachoteAcciones As Integer
        '    SinActivar
        '    Activada
        '    Consumida
        'End Enum

        'Public Enum ResultadoMachoteAccion As Integer
        '    NoExisteMachoteAccion
        '    YaActivadaAntes
        '    ActivadaExitosamente
        '    YaCaducada
        '    ErrorGeneral
        '    ErrorFactura
        '    AplicadaEnFactura
        '    NoActivada
        '    NoDueno
        'End Enum

        'MachoteAccion
        Public Function ConsultarMachoteAccion(ByVal id_MachoteAccion As Integer) As MachoteAccion
            Dim MachoteAccion As New MachoteAccion
            MachoteAccion.Fields.SelectAll()
            MachoteAccion.Id_MachoteAccion.Where.EqualCondition(id_MachoteAccion)
            Dim connection As New SQLServer(_connString)
            Dim query As New QueryBuilder
            Dim resultTable As Data.DataTable = connection.executeSelect_DT(query.SelectQuery(MachoteAccion))
            If resultTable.Rows.Count > 0 Then
                Return ObjectBuilder.CreateDBTable_BySelectIndex(resultTable.Rows(0), MachoteAccion)
            End If
            Return Nothing
        End Function

        Public Function CrearMachoteAccion(ByVal usuarioResponsable As Integer, ByVal elNombre As String, ByVal elPrefijoCodigo As String, ByVal elValor As Double, ByVal laMoneda As Integer, ByVal laFechaExpiracion As Date) As Boolean
            Dim machoteTemp As New MachoteAccion
            machoteTemp.CreadoPor.Value = usuarioResponsable
            machoteTemp.DiasValidez.Value = 0
            machoteTemp.FechaCreado.ValueUtc = Date.UtcNow
            machoteTemp.FechaExpiracion.ValueUtc = laFechaExpiracion
            machoteTemp.Moneda.Value = laMoneda
            machoteTemp.Nombre.Value = elNombre
            machoteTemp.PrefijoCodigo.Value = elPrefijoCodigo
            machoteTemp.Valor.Value = elValor
            Return CrearMachoteAccion(machoteTemp)
        End Function

        Public Function CrearMachoteAccion(ByVal usuarioResponsable As Integer, ByVal elNombre As String, ByVal elPrefijoCodigo As String, ByVal elValor As Double, ByVal laMoneda As Integer, ByVal losDiasValidez As Integer) As Boolean
            Dim machoteTemp As New MachoteAccion
            machoteTemp.CreadoPor.Value = usuarioResponsable
            machoteTemp.DiasValidez.Value = losDiasValidez
            machoteTemp.FechaCreado.ValueUtc = Date.UtcNow
            machoteTemp.Moneda.Value = laMoneda
            machoteTemp.Nombre.Value = elNombre
            machoteTemp.PrefijoCodigo.Value = elPrefijoCodigo
            machoteTemp.Valor.Value = elValor
            Return CrearMachoteAccion(machoteTemp)
        End Function

        Public Function CrearMachoteAccion(ByVal elMachoteAccion As MachoteAccion) As Boolean
            Return insertMachoteAccion(elMachoteAccion)
        End Function

        Public Function ActualizarMachoteAccion(ByVal elMachoteAccion As MachoteAccion, ByVal id_MachoteAccion As Integer) As Boolean
            Return updateMachoteAccion(elMachoteAccion, id_MachoteAccion)
        End Function

        Public Function BorrarMachoteAccion(ByVal id_MachoteAccion As Boolean) As Boolean
            Return deleteMachoteAccion(id_MachoteAccion)
        End Function

        Private Function insertMachoteAccion(ByVal elMachoteAccion As MachoteAccion) As Boolean
            Dim connection As New SQLServer(_connString)
            Dim query As New QueryBuilder
            Try
                If elMachoteAccion.FechaExpiracion.IsValid Then
                    If elMachoteAccion.FechaExpiracion.ValueUtc < Date.UtcNow Then
                        elMachoteAccion.FechaExpiracion.SetToNull()
                    End If
                End If
                connection.executeInsert(query.InsertQuery(elMachoteAccion))
                Return True
            Catch exc As Exception
                Return False
            End Try
        End Function

        Private Function updateMachoteAccion(ByVal elMachoteAccion As MachoteAccion, ByVal id_MachoteAccion As Integer) As Boolean
            Dim connection As New SQLServer(_connString)
            Dim query As New QueryBuilder
            Try
                elMachoteAccion.CreadoPor.ToUpdate = False
                elMachoteAccion.FechaCreado.ToUpdate = False
                elMachoteAccion.Id_MachoteAccion.ToUpdate = False
                elMachoteAccion.Id_MachoteAccion.Where.EqualCondition(id_MachoteAccion)
                connection.executeUpdate(query.UpdateQuery(elMachoteAccion))
                Return True
            Catch exc As Exception
                Return False
            End Try
        End Function

        Private Function deleteMachoteAccion(ByVal Id_MachoteAccion As Integer) As Boolean
            Dim connection As New SQLServer(_connString)
            Dim query As New QueryBuilder
            Try
                Dim elMachoteAccion As New MachoteAccion
                elMachoteAccion.Id_MachoteAccion.Where.EqualCondition(Id_MachoteAccion)
                connection.executeDelete(query.DeleteQuery(elMachoteAccion))
                Return True
            Catch exc As Exception
                Return False
            End Try
        End Function

        'MachoteAcciones consultas
        'Private Function QueryMachoteAcciones_DoQuery(ByVal theQuery As String, ByVal theMachoteAccion As MachoteAccion) As Data.DataTable
        '    Dim connection As New SQLServer(_connString)
        '    Dim datos As Data.DataTable = connection.executeSelect_DT(theQuery)

        '    If datos.Rows.Count > 0 Then
        '        datos.Columns(theMachoteAccion.Codigo.SelectIndex).ColumnName = Resources.MachoteAcciones_Resources.MachoteAccion_Codigo
        '        datos.Columns(theMachoteAccion.Valor.SelectIndex).ColumnName = Resources.MachoteAcciones_Resources.MachoteAccion_Valor
        '        datos.Columns(theMachoteAccion.FechaActivada.SelectIndex).ColumnName = Resources.MachoteAcciones_Resources.MachoteAccion_FechaActivada
        '        datos.Columns(theMachoteAccion.FechaCaducidad.SelectIndex).ColumnName = Resources.MachoteAcciones_Resources.MachoteAccion_FechaCaducidad
        '        datos.Columns(theMachoteAccion.FechaCreada.SelectIndex).ColumnName = Resources.MachoteAcciones_Resources.MachoteAccion_FechaCreada
        '        datos.Columns(theMachoteAccion.Estado.SelectIndex).ColumnMapping = Data.MappingType.Hidden

        '        datos.Columns(theMachoteAccion.Id_MachoteAccion.SelectIndex).ColumnMapping = Data.MappingType.Hidden
        '        datos.Columns(theMachoteAccion.Id_MachoteAccion.SelectIndex).ColumnMapping = Data.MappingType.Hidden
        '        datos.Columns(theMachoteAccion.Id_Moneda.SelectIndex).ColumnMapping = Data.MappingType.Hidden
        '        datos.Columns(theMachoteAccion.GeneradoPor.SelectIndex).ColumnMapping = Data.MappingType.Hidden

        '        datos.Columns.Add(Resources.MachoteAcciones_Resources.MachoteAccion_Estado)
        '        Dim ultimoIndice As Integer = datos.Columns.Count - 1

        '        For counter As Integer = 0 To datos.Rows.Count - 1
        '            Dim theRow As Data.DataRow = datos.Rows(counter)
        '            Dim elEstado As EstadosMachoteAcciones = theRow(theMachoteAccion.Estado.SelectIndex)
        '            theRow(ultimoIndice) = Resources.MachoteAcciones_Resources.ResourceManager.GetString(elEstado.ToString)
        '        Next

        '    End If

        '    Return datos
        'End Function

    End Class
End Namespace