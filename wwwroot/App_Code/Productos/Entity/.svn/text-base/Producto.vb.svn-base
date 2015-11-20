Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler
Imports Orbelink.Entity.Entidades
Imports Orbelink.Entity.Currency

Namespace Orbelink.Entity.Productos
    Public Class Producto : Inherits DBTable

        Dim _tableName As String = "PR_Productos"

        Public ReadOnly Id_Producto As New FieldInt("id_Producto", Me, False, True)
        Public ReadOnly Id_tipoProducto As New FieldInt("Id_tipoProducto", Me)
        Public ReadOnly Nombre As New FieldString("n_Producto", Me, Field.FieldType.VARCHAR_MULTILANG, 250)
        Public ReadOnly DescCorta_Producto As New FieldString("DescCorta_Producto", Me, Field.FieldType.VARCHAR_MULTILANG, 300, True)
        Public ReadOnly DescLarga_Producto As New FieldText("DescLarga_Producto", Me, True, True)
        Public ReadOnly Precio_Unitario As New FieldFloat("Precio_Unitario", Me)
        Public ReadOnly Fecha As New FieldDateTime("F_Producto", Me)
        Public ReadOnly SKU As New FieldString("SKU", Me, Field.FieldType.VARCHAR, 50, True)
        Public ReadOnly enPrinc As New FieldTinyInt("enPrinc", Me)
        Public ReadOnly Activo As New FieldTinyInt("Activo", Me)
        Public ReadOnly id_Origen As New FieldInt("id_Origen", Me)
        Public ReadOnly id_Estado As New FieldInt("id_Estado", Me)
        Public ReadOnly Id_Moneda As New FieldInt("Id_Moneda", Me)
        Public ReadOnly id_Entidad As New FieldInt("id_Entidad", Me)
        Public ReadOnly Capacidad As New FieldInt("Capacidad", Me, False, False, 0)
        Public ReadOnly CapacidadMaxima As New FieldInt("CapacidadMaxima", Me, False, False, 0)

        Public Sub New()
            SetFields()
        End Sub

        '<Obsolete("Esta metodo recibe una o mas fechas en UTC", False)> _
        Public Sub New(ByVal theId_tipoProducto As Integer, ByVal theNombre As String, ByVal theDescCorta_Producto As String, _
            ByVal theDescLarga_Producto As String, ByVal thePrecio_Unitario As Double, _
            ByVal theFecha As Date, ByVal theSKU As String, ByVal theenPrinc As Integer, ByVal theActivo As Integer, _
            ByVal theid_Entidad As Integer, ByVal theid_Origen As Integer, ByVal theid_Estado As Integer, ByVal theid_Unidad As Integer, _
            ByVal theCapacidad As Integer, ByVal theCapacidadMaximo As Integer)
            Id_tipoProducto.Value = theId_tipoProducto
            Nombre.Value = theNombre
            DescCorta_Producto.Value = theDescCorta_Producto
            DescLarga_Producto.Value = theDescLarga_Producto
            Precio_Unitario.Value = thePrecio_Unitario
            Fecha.ValueUtc = theFecha
            SKU.Value = theSKU
            enPrinc.Value = theenPrinc
            Activo.Value = theActivo
            id_Entidad.Value = theid_Entidad
            id_Estado.Value = theid_Estado
            Id_Moneda.Value = theid_Unidad
            id_Origen.Value = theid_Origen
            Capacidad.Value = theCapacidad
            CapacidadMaxima.Value = theCapacidadMaximo
            SetFields()
            _primaryKey = Nothing
            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Protected Overrides Sub SetKeysForScripting()
            _primaryKey = New PrimaryKey(Id_Producto, True, True)

            _foreingKeys = New System.Collections.ObjectModel.Collection(Of ForeingKey)
            _foreingKeys.Add(New ForeingKey(Id_tipoProducto, New TipoProducto().Id_TipoProducto))
            _foreingKeys.Add(New ForeingKey(id_Origen, New Origenes().Id_Origen))
            _foreingKeys.Add(New ForeingKey(id_Estado, New Estados_Productos().Id_Estado_Producto))
            _foreingKeys.Add(New ForeingKey(Id_Moneda, New Moneda().Id_Moneda))
            _foreingKeys.Add(New ForeingKey(id_Entidad, New Entidad().Id_entidad))

            _uniqueKeys = Nothing
        End Sub

        Private Sub SetFields()
            _fields = New FieldCollection
            _fields.Add(Id_Producto)
            _fields.Add(Id_tipoProducto)
            _fields.Add(Nombre)
            _fields.Add(DescCorta_Producto)
            _fields.Add(DescLarga_Producto)
            _fields.Add(Precio_Unitario)
            _fields.Add(Fecha)
            _fields.Add(SKU)
            _fields.Add(enPrinc)
            _fields.Add(Activo)
            _fields.Add(id_Entidad)
            _fields.Add(id_Origen)
            _fields.Add(id_Estado)
            _fields.Add(Id_Moneda)
            _fields.Add(Capacidad)
            _fields.Add(CapacidadMaxima)
        End Sub

        Public Overrides ReadOnly Property TableName() As String
            Get
                Return _tableName
            End Get
        End Property

        Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As DBTable
            Dim theInstance As Producto
            If forScripting Then
                theInstance = NewForScripting()
            Else
                theInstance = New Producto
            End If
            Return theInstance
        End Function

        Shared Function NewForScripting() As Producto
            Dim temp As New Producto()
            temp.SetFields()
            temp.SetKeysForScripting()
            temp.Fields.SelectAll()
            Return temp
        End Function

    End Class
End Namespace