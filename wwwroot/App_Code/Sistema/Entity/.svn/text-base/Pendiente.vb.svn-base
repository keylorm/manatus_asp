Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler
Imports Orbelink.Entity.Entidades

Namespace Orbelink.Entity.Orbecatalog
    Public Class Pendiente : Inherits DBTable

        Dim _tableName As String = "OR_Pendiente"

        Public ReadOnly Id_Pendiente As New FieldInt("id_Pendiente", Me, False, True)
        Public ReadOnly Nombre As New FieldString("N_Pendiente", Me, Field.FieldType.VARCHAR_MULTILANG, 50)
        Public ReadOnly Descripcion As New FieldString("Descripcion", Me, Field.FieldType.VARCHAR_MULTILANG, 200, True)
        Public ReadOnly Id_Responsable As New FieldInt("Id_Responsable", Me)
        Public ReadOnly Id_Publicador As New FieldInt("Id_Publicador", Me)
        Public ReadOnly Prioridad As New FieldInt("Prioridad", Me)
        Public ReadOnly FechaInicio As New FieldDateTime("F_Inicio", Me)
        Public ReadOnly FechaProgramado As New FieldDateTime("F_Programado", Me)
        Public ReadOnly FechaTerminado As New FieldDateTime("F_Terminado", Me, True)
        Public ReadOnly Terminado As New FieldTinyInt("Terminado", Me)

        Public Sub New()
            SetFields()
        End Sub

        <Obsolete("Esta metodo recibe una o mas fechas en UTC", False)> _
        Public Sub New(ByVal theNombre As String, ByVal theDescripcion As String, ByVal theId_Responsable As String, _
            ByVal theId_Publicador As String, ByVal thePrioridad As String, ByVal theFechaInicio As String, ByVal theFechaProgramado As String, _
            ByVal theFechaTerminado As String, ByVal theTerminado As String)
            Nombre.Value = theNombre
            Descripcion.Value = theDescripcion
            Id_Responsable.Value = theId_Responsable
            Id_Publicador.Value = theId_Publicador
            Prioridad.Value = thePrioridad
            FechaInicio.ValueUtc = theFechaInicio
            FechaProgramado.ValueUtc = theFechaProgramado
            FechaTerminado.ValueUtc = theFechaTerminado
            Terminado.Value = theTerminado
            SetFields()
            _primaryKey = Nothing
            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Protected Overrides Sub SetKeysForScripting()
            _primaryKey = New PrimaryKey(Id_Pendiente, True, True)

            _foreingKeys = New System.Collections.ObjectModel.Collection(Of ForeingKey)
            _foreingKeys.Add(New ForeingKey(Id_Responsable, New Entidad().Id_entidad, ForeingKey.DeleteRule.CASCADE))
            _foreingKeys.Add(New ForeingKey(Id_Publicador, New Entidad().Id_entidad))

            _uniqueKeys = Nothing
        End Sub

        Private Sub SetFields()
            _fields = New FieldCollection
            _fields.Add(Id_Pendiente)
            _fields.Add(Nombre)
            _fields.Add(Descripcion)
            _fields.Add(Id_Responsable)
            _fields.Add(Id_Publicador)
            _fields.Add(Prioridad)
            _fields.Add(FechaInicio)
            _fields.Add(FechaProgramado)
            _fields.Add(FechaTerminado)
            _fields.Add(Terminado)
        End Sub

        Public Overrides ReadOnly Property TableName() As String
            Get
                Return _tableName
            End Get
        End Property

        Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As DBTable
            Dim theInstance As Pendiente
            If forScripting Then
                theInstance = NewForScripting()
            Else
                theInstance = New Pendiente
            End If
            Return theInstance
        End Function

        Shared Function NewForScripting() As Pendiente
            Dim temp As New Pendiente()
            temp.SetFields()
            temp.SetKeysForScripting()
            temp.Fields.SelectAll()
            Return temp
        End Function

    End Class
End Namespace