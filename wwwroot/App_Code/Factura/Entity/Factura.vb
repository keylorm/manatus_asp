Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler
Imports Orbelink.Entity.Entidades
Imports Orbelink.Entity.Productos
Imports Orbelink.Entity.Currency

Namespace Orbelink.Entity.Facturas
    Public Class Factura : Inherits DBTable

        Dim _tableName As String = "FR_Factura"

        Public ReadOnly Id_Factura As New FieldInt("id_Factura", Me, False, True)
        Public ReadOnly Id_Comprador As New FieldInt("Id_Comprador", Me)
        Public ReadOnly id_Vendedor As New FieldInt("id_Vendedor", Me)
        Public ReadOnly id_ShippingAddress As New FieldInt("id_ShippingAddress", Me)
        Public ReadOnly id_TipoFactura As New FieldInt("id_TipoFactura", Me)
        Public ReadOnly Num_Factura As New FieldInt("Num_Factura", Me, True)
        Public ReadOnly Fecha_Factura As New FieldDateTime("Fecha_Factura", Me)
        Public ReadOnly Fecha_Cancelado As New FieldDateTime("Fecha_Cancelado", Me, True)
        Public ReadOnly Enviar_Por As New FieldString("Enviar_Por", Me, Field.FieldType.VARCHAR, 50, True)
        Public ReadOnly Estado_Factura As New FieldInt("Estado_Factura", Me)
        Public ReadOnly Direccion_Alterna As New FieldString("Direccion_Alterna", Me, Field.FieldType.VARCHAR, 8000, True)
        Public ReadOnly Comprobante As New FieldString("Comprobante", Me, Field.FieldType.VARCHAR, 100, True)
        Public ReadOnly Fecha_Comprobante As New FieldDateTime("Fecha_Comprobante", Me, True)
        Public ReadOnly Id_Moneda As New FieldInt("Id_Moneda", Me, True)
        Public ReadOnly SubTotal As New FieldFloat("SubTotal", Me, False, False, 0)
        Public ReadOnly PorcentajeImpuestos As New FieldFloat("PorcentajeImpuestos", Me, False, False, 0)
        Public ReadOnly Total As New FieldFloat("Total", Me, False, False, 0)

        Public Sub New()
            SetFields()
        End Sub

        <Obsolete("Esta metodo recibe una o mas fechas en UTC", False)> _
        Public Sub New(ByVal theId_Comprador As Integer, ByVal theid_Vendedor As Integer, ByVal theId_TipoFactura As Integer, ByVal theNum_Factura As Integer, ByVal theFecha_Factura As Date, _
            ByVal theFecha_Cancelado As Date, ByVal theEnviar_Por As String, ByVal theEstado_Factura As Integer, _
            ByVal theDireccion_Alterna As String, ByVal theComprobante As String, ByVal theFecha_Comprobante As Date, ByVal theId_Unidad As Integer, _
            ByVal theSubTotal As Double, ByVal thePorcentajeImpuestos As Double, ByVal theTotal As Double)
            Num_Factura.Value = theNum_Factura
            Fecha_Factura.ValueUtc = theFecha_Factura
            Fecha_Cancelado.ValueUtc = theFecha_Cancelado
            id_Vendedor.Value = theid_Vendedor
            Enviar_Por.Value = theEnviar_Por
            Id_Comprador.Value = theId_Comprador
            id_TipoFactura.Value = theId_TipoFactura
            Estado_Factura.Value = theEstado_Factura
            Direccion_Alterna.Value = theDireccion_Alterna
            Comprobante.Value = theComprobante
            Fecha_Comprobante.ValueUtc = theFecha_Comprobante
            Id_Moneda.Value = theId_Unidad
            PorcentajeImpuestos.Value = thePorcentajeImpuestos
            Total.Value = theTotal
            SetFields()
            _primaryKey = Nothing
            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Protected Overrides Sub SetKeysForScripting()
            _primaryKey = New PrimaryKey(Id_Factura, True, True)

            _foreingKeys = New System.Collections.ObjectModel.Collection(Of ForeingKey)
            _foreingKeys.Add(New ForeingKey(id_Vendedor, New Entidad().Id_entidad))
            _foreingKeys.Add(New ForeingKey(Id_Comprador, New Entidad().Id_entidad))
            _foreingKeys.Add(New ForeingKey(id_TipoFactura, New TipoFactura().Id_TipoFactura))
            _foreingKeys.Add(New ForeingKey(Id_Moneda, New Moneda().Id_Moneda))
            _foreingKeys.Add(New ForeingKey(id_ShippingAddress, New Shippings_Address().id_Shipping))

            _uniqueKeys = Nothing
        End Sub

        Private Sub SetFields()
            _fields = New FieldCollection
            _fields.Add(Id_Factura)
            _fields.Add(Id_Comprador)
            _fields.Add(id_Vendedor)
            _fields.Add(id_ShippingAddress)
            _fields.Add(id_TipoFactura)
            _fields.Add(Num_Factura)
            _fields.Add(Fecha_Factura)
            _fields.Add(Fecha_Cancelado)
            _fields.Add(Enviar_Por)
            _fields.Add(Estado_Factura)
            _fields.Add(Direccion_Alterna)
            _fields.Add(Comprobante)
            _fields.Add(Fecha_Comprobante)
            _fields.Add(Id_Moneda)
            _fields.Add(SubTotal)
            _fields.Add(PorcentajeImpuestos)
            _fields.Add(Total)
        End Sub

        Public Overrides ReadOnly Property TableName() As String
            Get
                Return _tableName
            End Get
        End Property

        Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As DBTable
            Dim theInstance As Factura
            If forScripting Then
                theInstance = NewForScripting()
            Else
                theInstance = New Factura
            End If
            Return theInstance
        End Function

        Shared Function NewForScripting() As Factura
            Dim temp As New Factura()
            temp.SetFields()
            temp.SetKeysForScripting()
            temp.Fields.SelectAll()
            Return temp
        End Function

    End Class
End Namespace