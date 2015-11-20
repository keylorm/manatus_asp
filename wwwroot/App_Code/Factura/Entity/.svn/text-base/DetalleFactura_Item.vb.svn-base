Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler
Imports Orbelink.Entity.Productos

Namespace Orbelink.Entity.Facturas
    Public Class DetalleFactura_Item : Inherits DBTable

        Dim _tableName As String = "FR_DetalleFactura_Item"

        Public ReadOnly Id_Factura As New FieldInt("id_Factura", Me)
        Public ReadOnly Detalle As New FieldInt("Detalle", Me)
        Public ReadOnly Id_Producto As New FieldInt("Id_Producto", Me)
        Public ReadOnly Ordinal As New FieldInt("Ordinal", Me)

        Public Sub New()
            SetFields()
        End Sub

        Public Sub New(ByVal theId_Factura As Integer, ByVal theDetalle As Integer, ByVal theId_Producto As Integer, ByVal theOrdinal As Integer)
            Id_Factura.Value = theId_Factura
            Detalle.Value = theDetalle
            Id_Producto.Value = theId_Producto
            Ordinal.Value = theOrdinal
            SetFields()
            _primaryKey = Nothing
            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Protected Overrides Sub SetKeysForScripting()
            _primaryKey = New PrimaryKey(Id_Factura, True, True)
            _primaryKey.Add_PK(Detalle, True)
            _primaryKey.Add_PK(Id_Producto, True)

            _foreingKeys = New System.Collections.ObjectModel.Collection(Of ForeingKey)

            Dim detalleFactura As New DetalleFactura
            Dim theFKDetalle As New ForeingKey(Id_Factura, detalleFactura.Id_Factura, ForeingKey.DeleteRule.CASCADE)
            theFKDetalle.Add_FK(Detalle, detalleFactura.Detalle)
            _foreingKeys.Add(theFKDetalle)

            Dim elItem As New Item
            Dim theFKItem As New ForeingKey(Id_Producto, elItem.Id_producto)
            theFKItem.Add_FK(Ordinal, elItem.ordinal)
            _foreingKeys.Add(theFKItem)

            _uniqueKeys = Nothing
        End Sub

        Private Sub SetFields()
            _fields = New FieldCollection
            _fields.Add(Id_Factura)
            _fields.Add(Detalle)
            _fields.Add(Id_Producto)
            _fields.Add(Ordinal)
        End Sub

        Public Overrides ReadOnly Property TableName() As String
            Get
                Return _tableName
            End Get
        End Property

        Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As DBTable
            Dim theInstance As DetalleFactura_Item
            If forScripting Then
                theInstance = NewForScripting()
            Else
                theInstance = New DetalleFactura_Item
            End If
            Return theInstance
        End Function

        Shared Function NewForScripting() As DetalleFactura_Item
            Dim temp As New DetalleFactura_Item()
            temp.SetFields()
            temp.SetKeysForScripting()
            temp.Fields.SelectAll()
            Return temp
        End Function

    End Class
End Namespace