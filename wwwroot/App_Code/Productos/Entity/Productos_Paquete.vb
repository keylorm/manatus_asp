Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler

Namespace Orbelink.Entity.Productos
    Public Class Productos_Paquete : Inherits DBTable

        Dim _tableName As String = "PR_Productos_Paquete"


        Public ReadOnly Id_Paquete As New FieldInt("Id_Paquete", Me, False)
        Public ReadOnly Id_Producto As New FieldInt("Id_Producto", Me, False)
        Public ReadOnly Cantidad As New FieldInt("Cantidad", Me, False)

        Public Sub New()
            SetFields()
        End Sub

        Public Sub New(ByVal theId_Paquete As String, ByVal theId_Producto As String, ByVal theCantidad As String)
            Id_Producto.Value = theId_Producto
            Cantidad.Value = theCantidad
            Id_Paquete.Value = theId_Paquete
            SetFields()
            _primaryKey = Nothing
            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Protected Overrides Sub SetKeysForScripting()
            _primaryKey = New PrimaryKey(Id_Producto, True, True)
            _primaryKey.Add_PK(Id_Paquete, True)

            _foreingKeys = New System.Collections.ObjectModel.Collection(Of ForeingKey)
            _foreingKeys.Add(New ForeingKey(Id_Producto, New Producto().Id_Producto, ForeingKey.DeleteRule.CASCADE))
            _foreingKeys.Add(New ForeingKey(Id_Paquete, New Paquete().Id_Paquete, ForeingKey.DeleteRule.CASCADE))

            _uniqueKeys = Nothing
        End Sub

        Private Sub SetFields()
            _fields = New FieldCollection
            _fields.Add(Id_Paquete)
            _fields.Add(Id_Producto)
            _fields.Add(Cantidad)

        End Sub



        Public Overrides ReadOnly Property TableName() As String
            Get
                Return _tableName
            End Get
        End Property

        Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As DBTable
            Dim theInstance As Productos_Paquete
            If forScripting Then
                theInstance = NewForScripting()
            Else
                theInstance = New Productos_Paquete
            End If
            Return theInstance
        End Function

        Shared Function NewForScripting() As Productos_Paquete
            Dim temp As New Productos_Paquete()
            temp.SetFields()
            temp.SetKeysForScripting()
            temp.Fields.SelectAll()
            Return temp
        End Function

    End Class
End Namespace