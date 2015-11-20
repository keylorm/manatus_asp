Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler
Imports Orbelink.Entity.Productos

Namespace Orbelink.Entity.Facturas
    Public Class DetalleFactura : Inherits DBTable

        Dim _tableName As String = "FR_DetalleFactura"

        Public ReadOnly Id_Factura As New FieldInt("id_Factura", Me)
        Public ReadOnly Detalle As New FieldInt("Detalle", Me)
        Public ReadOnly NombreDisplay As New FieldString("NombreDisplay", Me, Field.FieldType.VARCHAR, 500)
        Public ReadOnly Cantidad As New FieldInt("Cantidad", Me)
        Public ReadOnly Precio_Unitario As New FieldFloat("Precio_Unitario", Me)
        Public ReadOnly Precio_Unitario_Extra As New FieldFloat("Precio_Unitario_Extra", Me)
        Public ReadOnly Descuento As New FieldFloat("Descuento", Me)
        Public ReadOnly MontoVenta As New FieldFloat("MontoVenta", Me)

        Public Sub New()
            SetFields()
        End Sub

        Public Sub New(ByVal theId_Factura As Integer, ByVal theDetalle As Integer, ByVal theNombreDisplay As String, ByVal theCantidad As Integer, _
                       ByVal thePrecio_Unitario As Double, ByVal thePrecio_Unitario_Extra As Double, ByVal theDescuento As Double, _
                       ByVal theMontoVenta As Double)
            Id_Factura.Value = theId_Factura
            Detalle.Value = theDetalle
            NombreDisplay.Value = theNombreDisplay
            Cantidad.Value = theCantidad
            MontoVenta.Value = theMontoVenta
            Precio_Unitario_Extra.Value = thePrecio_Unitario_Extra
            Descuento.Value = theDescuento
            Precio_Unitario.Value = thePrecio_Unitario
            SetFields()
            _primaryKey = Nothing
            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Protected Overrides Sub SetKeysForScripting()
            _primaryKey = New PrimaryKey(Id_Factura, True, True)
            _primaryKey.Add_PK(Detalle, True)

            _foreingKeys = New System.Collections.ObjectModel.Collection(Of ForeingKey)
            _foreingKeys.Add(New ForeingKey(Id_Factura, New Factura().Id_Factura, ForeingKey.DeleteRule.CASCADE))

            _uniqueKeys = Nothing
        End Sub

        Private Sub SetFields()
            _fields = New FieldCollection
            _fields.Add(Id_Factura)
            _fields.Add(Detalle)
            _fields.Add(NombreDisplay)
            _fields.Add(Cantidad)
            _fields.Add(Precio_Unitario)
            _fields.Add(Precio_Unitario_Extra)
            _fields.Add(Descuento)
            _fields.Add(MontoVenta)
        End Sub

        Public Overrides ReadOnly Property TableName() As String
            Get
                Return _tableName
            End Get
        End Property

        Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As DBTable
            Dim theInstance As DetalleFactura
            If forScripting Then
                theInstance = NewForScripting()
            Else
                theInstance = New DetalleFactura
            End If
            Return theInstance
        End Function

        Shared Function NewForScripting() As DetalleFactura
            Dim temp As New DetalleFactura()
            temp.SetFields()
            temp.SetKeysForScripting()
            temp.Fields.SelectAll()
            Return temp
        End Function

    End Class
End Namespace