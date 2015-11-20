Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler
Imports Orbelink.Entity.Entidades
Imports Orbelink.Entity.Publicaciones

Public Class Relacion_Entidad_Publicacion : Inherits DBTable

    Dim _tableName As String = "RE_Entidad_Publicacion"


    Public ReadOnly id_entidad As New FieldInt("id_entidad", Me)
    Public ReadOnly id_Publicacion As New FieldInt("id_Publicacion", Me)
    Public ReadOnly id_TipoRelacion As New FieldInt("id_TipoRelacion", Me)

    Public Sub New()
        SetFields()
    End Sub

    Public Sub New(ByVal theid_entidad As String, ByVal theid_Publicacion As String, ByVal theid_TipoRelacion As String)
        id_entidad.Value = theid_entidad
        id_Publicacion.Value = theid_Publicacion
        id_TipoRelacion.Value = theid_TipoRelacion
        SetFields()
        _primaryKey = Nothing
        _foreingKeys = Nothing
        _uniqueKeys = Nothing
    End Sub

    Protected Overrides Sub SetKeysForScripting()
        _primaryKey = New PrimaryKey(id_entidad, True, True)
        _primaryKey.Add_PK(id_Publicacion, True)
        _primaryKey.Add_PK(id_TipoRelacion, True)

        _foreingKeys = New System.Collections.ObjectModel.Collection(Of ForeingKey)
        _foreingKeys.Add(New ForeingKey(id_entidad, New Entidad().Id_entidad, ForeingKey.DeleteRule.CASCADE))
        _foreingKeys.Add(New ForeingKey(id_Publicacion, New Publicacion().Id_Publicacion, ForeingKey.DeleteRule.CASCADE))
        _foreingKeys.Add(New ForeingKey(id_TipoRelacion, New TipoRelacion_Entidad_Publicacion().Id_TipoRelacion))

        _uniqueKeys = Nothing
    End Sub

    Private Sub SetFields()
        _fields = New FieldCollection
        _fields.Add(id_entidad)
        _fields.Add(id_Publicacion)
        _fields.Add(id_TipoRelacion)
    End Sub


    Public Overrides ReadOnly Property TableName() As String
        Get
            Return _tableName
        End Get
    End Property

            Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As DBTable
            Dim theInstance As Relacion_Entidad_Publicacion
            If forScripting Then
                theInstance = NewForScripting()
            Else
                theInstance = New Relacion_Entidad_Publicacion
            End If
            Return theInstance
        End Function

        Shared Function NewForScripting() As Relacion_Entidad_Publicacion
            Dim temp As New Relacion_Entidad_Publicacion()
            temp.SetFields()
temp.SetKeysForScripting()
temp.Fields.SelectAll()
Return temp
        End Function

End Class
