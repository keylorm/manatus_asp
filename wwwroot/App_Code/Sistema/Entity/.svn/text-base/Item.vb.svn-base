Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler

Namespace Orbelink.Entity.Productos

    Public Class Item : Inherits DBTable
        Dim _tableName As String = "Rs_Item"

        Public ReadOnly Id_producto As New Orbelink.DBHandler.FieldInt("Id_producto", Me)
        Public ReadOnly ordinal As New Orbelink.DBHandler.FieldInt("ordinal", Me)
        Public ReadOnly SKU As New Orbelink.DBHandler.FieldString("SKU", Me, Field.FieldType.VARCHAR, 250, True)
        Public ReadOnly descripcion As New Orbelink.DBHandler.FieldString("descripcion", Me, Orbelink.DBHandler.Field.FieldType.VARCHAR_MULTILANG, 250, True)

        Public Sub New(ByVal theId_producto As Integer, ByVal theordinal As Integer, ByVal theSKu As Integer, ByVal theDescripcion As String)
            Id_producto.Value = theId_producto
            ordinal.Value = theordinal
            SKU.Value = theSKu
            descripcion.Value = theDescripcion
            SetFields()
            _primaryKey = Nothing
            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Public Sub New()
            SetFields()
        End Sub

        Public Sub New(ByVal forScripting As Boolean)
            If forScripting Then
                SetFields()
                SetKeysForScripting()
            End If
        End Sub

        Private Sub SetFields()
            _fields = New FieldCollection
            _fields.Add(Id_producto)
            _fields.Add(ordinal)
            _fields.Add(SKU)
            _fields.Add(descripcion)
        End Sub

        Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As Orbelink.DBHandler.DBTable
            Dim theInstance As Item
            If forScripting Then
                theInstance = New Item(True)
            Else
                theInstance = New Item
            End If
            Return theInstance
        End Function

        Protected Overrides Sub SetKeysForScripting()
            _primaryKey = New PrimaryKey(Id_producto, True, True)
            _primaryKey.Add_PK(ordinal, True)

            _foreingKeys = New System.Collections.ObjectModel.Collection(Of ForeingKey)
            _foreingKeys.Add(New ForeingKey(Id_producto, New Producto().Id_Producto))

            _uniqueKeys = Nothing
        End Sub

        Public Overrides ReadOnly Property TableName() As String
            Get
                Return _tableName
            End Get
        End Property

        Shared Function NewForScripting() As Item
            Dim temp As New Item()
            temp.SetFields()
temp.SetKeysForScripting()
temp.Fields.SelectAll()
Return temp
        End Function

    End Class

End Namespace