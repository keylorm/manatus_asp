Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler
Imports Orbelink.Entity.Facturas
Imports Orbelink.Entity.Productos
Imports Orbelink.Entity.Currency

Namespace Orbelink.Entity.Reservaciones

    Public Class Precios_Temporada : Inherits DBTable

        Dim _tableName As String = "Rs_Precios_Temporada"

        Public ReadOnly Id_Temporada As New Orbelink.DBHandler.FieldInt("Id_Temporada", Me)
        Public ReadOnly Id_Producto As New Orbelink.DBHandler.FieldInt("Id_Producto", Me)
        Public ReadOnly Id_Moneda As New Orbelink.DBHandler.FieldInt("Id_Moneda", Me)
        Public ReadOnly PrecioProducto As New Orbelink.DBHandler.FieldFloat("precioProducto", Me)
        Public ReadOnly PrecioAdAdulto As New Orbelink.DBHandler.FieldFloat("precioAdAdulto", Me)
        Public ReadOnly PrecioAdNino As New Orbelink.DBHandler.FieldFloat("precioAdNino", Me)
        Public ReadOnly PrecioAdAdultoExtra As New Orbelink.DBHandler.FieldFloat("precioAdAdultoExtra", Me)
        Public ReadOnly PrecioAdNinoExtra As New Orbelink.DBHandler.FieldFloat("precioAdNinoExtra", Me)
        Public ReadOnly UnidadTiempo As New Orbelink.DBHandler.FieldInt("UnidadTiempo", Me)
        Public ReadOnly CantidadNoches As New Orbelink.DBHandler.FieldInt("CantidadNoches", Me)
        Public ReadOnly CantidadPersonas As New Orbelink.DBHandler.FieldInt("CantidadPersonas", Me)
        Public ReadOnly PrecioNocheExtraAd As New Orbelink.DBHandler.FieldFloat("precioNocheExtraAd", Me)
        Public ReadOnly PrecioNocheExtraNino As New Orbelink.DBHandler.FieldFloat("PrecioNocheExtraNino", Me)

        Public Sub New(ByVal theId_Producto As Integer, ByVal theId_Temporada As Integer, ByVal thePrecioProducto As Double, _
                       ByVal thePrecioAdAdulto As Double, ByVal thePrecioAdNino As Double, ByVal thePrecioAdAdultoExtra As Double, ByVal thePrecioAdNinoExtra As Double, _
                       ByVal thePrioridad As Integer, ByVal theDescripcion As String, ByVal theId_Unidad As Integer, ByVal theUnidadTiempo As DateInterval)
            Id_Temporada.Value = theId_Temporada
            Id_Producto.Value = theId_Producto
            Id_Moneda.Value = theId_Unidad
            PrecioProducto.Value = thePrecioProducto
            PrecioAdAdulto.Value = thePrecioAdAdulto
            PrecioAdNino.Value = thePrecioAdNino
            PrecioAdAdultoExtra.Value = thePrecioAdAdultoExtra
            PrecioAdNinoExtra.Value = thePrecioAdNinoExtra

            UnidadTiempo.Value = theUnidadTiempo

            CantidadNoches.Value = 0
            CantidadPersonas.Value = 0
            PrecioNocheExtraAd.Value = 0
            PrecioNocheExtraNino.Value = 0
            SetFields()
            _primaryKey = Nothing
            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Public Sub New(ByVal theId_Producto As Integer, ByVal theId_Temporada As Integer, ByVal thePrecioProducto As Double, _
                       ByVal thecantidadNoches As Integer, ByVal thecantidadPersonas As Integer, ByVal theprecioNocheExtraAd As Double, ByVal theprecioNocheExtraNino As Double, _
                       ByVal thePrioridad As Integer, ByVal theDescripcion As String, ByVal theId_Unidad As Integer, ByVal theUnidadTiempo As DateInterval)
            Id_Temporada.Value = theId_Temporada
            Id_Producto.Value = theId_Producto
            Id_Moneda.Value = theId_Unidad

            PrecioProducto.Value = thePrecioProducto
            CantidadNoches.Value = thecantidadNoches
            CantidadPersonas.Value = thecantidadPersonas
            PrecioNocheExtraAd.Value = theprecioNocheExtraAd
            PrecioNocheExtraNino.Value = theprecioNocheExtraNino

            PrecioAdAdulto.Value = 0
            PrecioAdNino.Value = 0
            PrecioAdAdultoExtra.Value = 0
            PrecioAdNinoExtra.Value = 0
            UnidadTiempo.Value = theUnidadTiempo

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
            _fields.Add(Id_Producto)
            _fields.Add(Id_Temporada)
            _fields.Add(Id_Moneda)
            _fields.Add(PrecioProducto)
            _fields.Add(PrecioAdAdulto)
            _fields.Add(PrecioAdNino)
            _fields.Add(PrecioAdAdultoExtra)
            _fields.Add(PrecioAdNinoExtra)
            _fields.Add(UnidadTiempo)

            _fields.Add(CantidadNoches)
            _fields.Add(CantidadPersonas)
            _fields.Add(PrecioNocheExtraAd)
            _fields.Add(PrecioNocheExtraNino)
        End Sub

        Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As Orbelink.DBHandler.DBTable
            Dim theInstance As Precios_Temporada
            If forScripting Then
                theInstance = New Precios_Temporada(True)
            Else
                theInstance = New Precios_Temporada
            End If
            Return theInstance
        End Function

        Protected Overrides Sub SetKeysForScripting()
            _primaryKey = New PrimaryKey(Id_Producto, True, True)
            _primaryKey.Add_PK(Id_Temporada, True)

            _foreingKeys = New System.Collections.ObjectModel.Collection(Of ForeingKey)
            _foreingKeys.Add(New ForeingKey(Id_Temporada, New Temporada().Id_Temporada))
            _foreingKeys.Add(New ForeingKey(Id_Producto, New Producto().Id_Producto))
            _foreingKeys.Add(New ForeingKey(Id_Moneda, New Moneda().Id_Moneda))

            _uniqueKeys = New System.Collections.ObjectModel.Collection(Of Orbelink.DBHandler.UniqueKey)
        End Sub

        Public Overrides ReadOnly Property TableName() As String
            Get
                Return _tableName
            End Get
        End Property

        Shared Function NewForScripting() As Precios_Temporada
            Dim temp As New Precios_Temporada()
            temp.SetFields()
            temp.SetKeysForScripting()
            temp.Fields.SelectAll()
            Return temp
        End Function
    End Class


End Namespace