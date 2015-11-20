Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler
Imports Orbelink.Entity.Entidades
Imports Orbelink.Entity.Productos

Public Class Relacion_Entidad_Origen : Inherits DBTable

    Dim _tableName As String = "RE_Entidad_Origen"


    Public ReadOnly id_entidad As New FieldInt("id_entidad", Me)
    Public ReadOnly id_Origen As New FieldInt("id_Origen", Me)
    Public ReadOnly id_TipoRelacion As New FieldInt("id_TipoRelacion", Me)

    Public Sub New()
        setFields()
    End Sub

    Public Sub New(ByVal theid_entidad As String, ByVal theid_Origen As String, ByVal theid_TipoRelacion As String)
        id_entidad.Value = theid_entidad
        id_Origen.Value = theid_Origen
        id_TipoRelacion.Value = theid_TipoRelacion
        setFields()
        _primaryKey = Nothing
        _foreingKeys = Nothing
        _uniqueKeys = Nothing
    End Sub

    Protected Overrides Sub SetKeysForScripting()
        _primaryKey = New PrimaryKey(id_entidad, True, True)
        _primaryKey.Add_PK(id_Origen, True)
        _primaryKey.Add_PK(id_TipoRelacion, True)

        _foreingKeys = New System.Collections.ObjectModel.Collection(Of ForeingKey)
        _foreingKeys.Add(New ForeingKey(id_entidad, New Entidad().Id_entidad))
        _foreingKeys.Add(New ForeingKey(id_Origen, New Origenes().Id_Origen))
        _foreingKeys.Add(New ForeingKey(id_TipoRelacion, New TipoRelacion_Entidad_Origen().Id_TipoRelacion))

        _uniqueKeys = Nothing
    End Sub
    Private Sub setFields()
        _fields = New FieldCollection
        _fields.Add(id_entidad)
        _fields.Add(id_Origen)
        _fields.Add(id_TipoRelacion)
    End Sub


    Public Overrides ReadOnly Property TableName() As String
        Get
            Return _tableName
        End Get
    End Property

            Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As DBTable
            Dim theInstance As Relacion_Entidad_Origen
            If forScripting Then
                theInstance = NewForScripting()
            Else
                theInstance = New Relacion_Entidad_Origen
            End If
            Return theInstance
        End Function

        Shared Function NewForScripting() As Relacion_Entidad_Origen
            Dim temp As New Relacion_Entidad_Origen()
            temp.SetFields()
temp.SetKeysForScripting()
temp.Fields.SelectAll()
Return temp
        End Function

End Class
