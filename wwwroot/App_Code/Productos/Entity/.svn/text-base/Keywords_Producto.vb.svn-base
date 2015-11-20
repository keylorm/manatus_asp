Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler

Namespace Orbelink.Entity.Productos
    Public Class Keywords_Producto : Inherits DBTable

        Dim _tableName As String = "PR_Keywords_Producto"


        Public ReadOnly Id_Producto As New FieldInt("Id_Producto", Me)
        Public ReadOnly Id_Keyword As New FieldInt("Id_Keyword", Me)

        Public Sub New()
            SetFields()
        End Sub

        Public Sub New(ByVal theId_Producto As String, ByVal theId_Keyword As String)
            Id_Producto.Value = theId_Producto
            Id_Keyword.Value = theId_Keyword
            SetFields()
            _primaryKey = Nothing
            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Protected Overrides Sub SetKeysForScripting()
            _primaryKey = New PrimaryKey(Id_Producto, True, True)
            _primaryKey.Add_PK(Id_Keyword, True)

            _foreingKeys = New System.Collections.ObjectModel.Collection(Of ForeingKey)
            _foreingKeys.Add(New ForeingKey(Id_Producto, New Producto().Id_Producto, ForeingKey.DeleteRule.CASCADE))
            _foreingKeys.Add(New ForeingKey(Id_Keyword, New Keywords().Id_Keyword))

            _uniqueKeys = Nothing
        End Sub

        Private Sub SetFields()
            _fields = New FieldCollection
            _fields.Add(Id_Producto)
            _fields.Add(Id_Keyword)
        End Sub


        Public Overrides ReadOnly Property TableName() As String
            Get
                Return _tableName
            End Get
        End Property

        Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As DBTable
            Dim theInstance As Keywords_Producto
            If forScripting Then
                theInstance = NewForScripting()
            Else
                theInstance = New Keywords_Producto
            End If
            Return theInstance
        End Function

        Shared Function NewForScripting() As Keywords_Producto
            Dim temp As New Keywords_Producto()
            temp.SetFields()
            temp.SetKeysForScripting()
            temp.Fields.SelectAll()
            Return temp
        End Function

    End Class
End Namespace