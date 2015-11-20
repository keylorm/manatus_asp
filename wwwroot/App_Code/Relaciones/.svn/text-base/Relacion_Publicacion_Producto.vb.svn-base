Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler
Imports Orbelink.Entity.Productos
Imports Orbelink.Entity.Publicaciones

Public Class Relacion_Publicacion_Producto : Inherits DBTable

    Dim _tableName As String = "RE_Producto_Publicacion"


    Public ReadOnly id_Producto As New FieldInt("id_Producto", Me)
    Public ReadOnly id_Publicacion As New FieldInt("id_Publicacion", Me)
    Public ReadOnly id_TipoRelacion As New FieldInt("id_TipoRelacion", Me)

    Public Sub New()
        SetFields()
    End Sub

    Public Sub New(ByVal theid_Producto As String, ByVal theid_Publicacion As String, ByVal theid_TipoRelacion As String)
        id_Producto.Value = theid_Producto
        id_Publicacion.Value = theid_Publicacion
        id_TipoRelacion.Value = theid_TipoRelacion
        SetFields()
        _primaryKey = Nothing
        _foreingKeys = Nothing
        _uniqueKeys = Nothing
    End Sub

    Protected Overrides Sub SetKeysForScripting()
        _primaryKey = New PrimaryKey(id_Producto, True, True)
        _primaryKey.Add_PK(id_Publicacion, True)
        _primaryKey.Add_PK(id_TipoRelacion, True)

        _foreingKeys = New System.Collections.ObjectModel.Collection(Of ForeingKey)
        _foreingKeys.Add(New ForeingKey(id_Producto, New Producto().Id_Producto, ForeingKey.DeleteRule.CASCADE))
        _foreingKeys.Add(New ForeingKey(id_Publicacion, New Publicacion().Id_Publicacion, ForeingKey.DeleteRule.CASCADE))
        _foreingKeys.Add(New ForeingKey(id_TipoRelacion, New TipoRelacion_Producto_Publicacion().Id_TipoRelacion))

        _uniqueKeys = Nothing
    End Sub

    Private Sub SetFields()
        _fields = New FieldCollection
        _fields.Add(id_Producto)
        _fields.Add(id_Publicacion)
        _fields.Add(id_TipoRelacion)
    End Sub


    Public Overrides ReadOnly Property TableName() As String
        Get
            Return _tableName
        End Get
    End Property

    Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As DBTable
        Dim theInstance As Relacion_Publicacion_Producto
        If forScripting Then
            theInstance = NewForScripting()
        Else
            theInstance = New Relacion_Publicacion_Producto
        End If
        Return theInstance
    End Function

    Shared Function NewForScripting() As Relacion_Publicacion_Producto
        Dim temp As New Relacion_Publicacion_Producto()
        temp.SetFields()
        temp.SetKeysForScripting()
        temp.Fields.SelectAll()
        Return temp
    End Function

End Class

