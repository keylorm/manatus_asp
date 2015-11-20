Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler
Imports Orbelink.Entity.Productos
Imports Orbelink.Entity.Currency

Namespace Orbelink.Entity.Acciones
    Public Class ProductoAplicable
        Inherits DBTable

        Dim _tableName As String = "AC_ProductoAplicable"

        Public ReadOnly Id_Producto As New FieldInt("Id_Producto", Me)
        Public ReadOnly Id_MachoteAccion As New FieldInt("Id_MachoteAccion", Me)
        Public ReadOnly ValorMaximo As New FieldFloat("ValorMaximo", Me)
        Public ReadOnly Id_Moneda As New FieldInt("Id_Moneda", Me)

        Public Sub New()
            SetFields()
        End Sub

        Public Sub New(ByVal theId_MachoteAccion As Integer, ByVal theId_Producto As Integer, ByVal theValorMaximo As Double, _
                ByVal theId_Moneda As Integer)
            Id_Producto.Value = theId_Producto
            Id_MachoteAccion.Value = theId_MachoteAccion
            ValorMaximo.Value = theValorMaximo
            Id_Moneda.Value = theId_Moneda
            SetFields()
            _primaryKey = Nothing
            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Protected Overrides Sub SetKeysForScripting()
            _primaryKey = New PrimaryKey(Id_MachoteAccion, True, True)
            _primaryKey.Add_PK(Id_Producto, True)

            _foreingKeys = New System.Collections.ObjectModel.Collection(Of ForeingKey)
            _foreingKeys.Add(New ForeingKey(Id_MachoteAccion, New MachoteAccion().Id_MachoteAccion))
            _foreingKeys.Add(New ForeingKey(Id_Producto, New Producto().Id_Producto))
            _foreingKeys.Add(New ForeingKey(Id_Moneda, New Moneda().Id_Moneda))

            _uniqueKeys = Nothing
        End Sub

        Private Sub SetFields()
            _fields = New FieldCollection
            _fields.Add(Id_MachoteAccion)
            _fields.Add(Id_Producto)
            _fields.Add(ValorMaximo)
            _fields.Add(Id_Moneda)
        End Sub

        Public Overrides ReadOnly Property TableName() As String
            Get
                Return _tableName
            End Get
        End Property

        Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As DBTable
            Dim theInstance As ProductoAplicable
            If forScripting Then
                theInstance = NewForScripting()
            Else
                theInstance = New ProductoAplicable
            End If
            Return theInstance
        End Function

        Shared Function NewForScripting() As ProductoAplicable
            Dim temp As New ProductoAplicable()
            temp.SetFields()
            temp.SetKeysForScripting()
            temp.Fields.SelectAll()
            Return temp
        End Function

    End Class
End Namespace