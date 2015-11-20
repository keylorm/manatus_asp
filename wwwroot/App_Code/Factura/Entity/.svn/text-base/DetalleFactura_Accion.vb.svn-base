Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler
Imports Orbelink.Entity.Acciones

Namespace Orbelink.Entity.Facturas
    Public Class DetalleFactura_Accion : Inherits DBTable

        Dim _tableName As String = "FR_DetalleFactura_Accion"

        Public ReadOnly Id_Factura As New FieldInt("id_Factura", Me)
        Public ReadOnly Detalle As New FieldInt("Detalle", Me)
        Public ReadOnly Id_Accion As New FieldInt("Id_Accion", Me)

        Public Sub New()
            SetFields()
        End Sub

        Public Sub New(ByVal theId_Factura As Integer, ByVal theDetalle As Integer, ByVal theId_Accion As Integer)
            Id_Factura.Value = theId_Factura
            Detalle.Value = theDetalle
            Id_Accion.Value = theId_Accion
            SetFields()
            _primaryKey = Nothing
            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Protected Overrides Sub SetKeysForScripting()
            _primaryKey = New PrimaryKey(Id_Factura, True, True)
            _primaryKey.Add_PK(Detalle, True)
            _primaryKey.Add_PK(Id_Accion, True)

            _foreingKeys = New System.Collections.ObjectModel.Collection(Of ForeingKey)

            Dim detalleFactura As New DetalleFactura
            Dim theFK As New ForeingKey(Id_Factura, detalleFactura.Id_Factura, ForeingKey.DeleteRule.CASCADE)
            theFK.Add_FK(Detalle, detalleFactura.Detalle)
            _foreingKeys.Add(theFK)
            _foreingKeys.Add(New ForeingKey(Id_Accion, New Accion().Id_Accion))

            _uniqueKeys = New System.Collections.ObjectModel.Collection(Of UniqueKey)
            _uniqueKeys.Add(New UniqueKey(Id_Accion, True))
        End Sub

        Private Sub SetFields()
            _fields = New FieldCollection
            _fields.Add(Id_Factura)
            _fields.Add(Detalle)
            _fields.Add(Id_Accion)
        End Sub

        Public Overrides ReadOnly Property TableName() As String
            Get
                Return _tableName
            End Get
        End Property

        Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As DBTable
            Dim theInstance As DetalleFactura_Accion
            If forScripting Then
                theInstance = NewForScripting()
            Else
                theInstance = New DetalleFactura_Accion
            End If
            Return theInstance
        End Function

        Shared Function NewForScripting() As DetalleFactura_Accion
            Dim temp As New DetalleFactura_Accion()
            temp.SetFields()
            temp.SetKeysForScripting()
            temp.Fields.SelectAll()
            Return temp
        End Function

    End Class
End Namespace