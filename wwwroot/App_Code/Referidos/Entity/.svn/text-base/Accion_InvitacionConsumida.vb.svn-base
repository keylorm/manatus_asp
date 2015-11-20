Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler
Imports Orbelink.Entity.Facturas

Namespace Orbelink.Entity.Referidos
    Public Class Accion_InvitacionConsumida
        Inherits DBTable

        Dim _tableName As String = "RF_Accion_InvitacionConsumida"

        Public ReadOnly Id_Factura As New FieldInt("id_Factura", Me)
        Public ReadOnly Detalle As New FieldInt("Detalle", Me)
        Public ReadOnly Id_Accion As New FieldInt("Id_Accion", Me)
        Public ReadOnly Id_Invitacion As New FieldInt("Id_Invitacion", Me)

        Public Sub New()
            SetFields()
        End Sub

        Public Sub New(ByVal theId_Factura As Integer, ByVal theDetalle As Integer, ByVal theId_Accion As Integer, ByVal theId_Invitacion As Integer)
            Id_Factura.Value = theId_Factura
            Detalle.Value = theDetalle
            Id_Accion.Value = theId_Accion
            Id_Invitacion.value = theId_Invitacion
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

            Dim detalleFactura As New DetalleFactura_Accion
            Dim theFK As New ForeingKey(Id_Factura, detalleFactura.Id_Factura, ForeingKey.DeleteRule.CASCADE)
            theFK.Add_FK(Detalle, detalleFactura.Detalle)
            theFK.Add_FK(Id_Accion, detalleFactura.id_accion)
            _foreingKeys.Add(theFK)

            _foreingKeys.Add(New ForeingKey(Id_Invitacion, New Invitacion().Id_Invitacion))

            _uniqueKeys = New System.Collections.ObjectModel.Collection(Of UniqueKey)
            _uniqueKeys.Add(New UniqueKey(Id_Accion, True))
        End Sub

        Private Sub SetFields()
            _fields = New FieldCollection
            _fields.Add(Id_Factura)
            _fields.Add(Detalle)
            _fields.Add(Id_Accion)
            _fields.Add(Id_Invitacion)
        End Sub

        Public Overrides ReadOnly Property TableName() As String
            Get
                Return _tableName
            End Get
        End Property

        Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As DBTable
            Dim theInstance As Accion_InvitacionConsumida
            If forScripting Then
                theInstance = NewForScripting()
            Else
                theInstance = New Accion_InvitacionConsumida
            End If
            Return theInstance
        End Function

        Shared Function NewForScripting() As Accion_InvitacionConsumida
            Dim temp As New Accion_InvitacionConsumida()
            temp.SetFields()
            temp.SetKeysForScripting()
            temp.Fields.SelectAll()
            Return temp
        End Function

    End Class
End Namespace