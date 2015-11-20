Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler
Imports Orbelink.Entity.Productos

Namespace Orbelink.Entity.Reservaciones

    Public Class Detalle_Reservacion : Inherits DBTable

        Dim _tableName As String = "Rs_Detalle_reservacion"

        Public ReadOnly id_reservacion As New Orbelink.DBHandler.FieldInt("id_reservacion", Me)
        Public ReadOnly Id_producto As New Orbelink.DBHandler.FieldInt("Id_producto", Me)
        Public ReadOnly ordinal As New Orbelink.DBHandler.FieldInt("ordinal", Me)
        Public ReadOnly Adultos As New Orbelink.DBHandler.FieldInt("Adultos", Me)
        Public ReadOnly Ninos As New Orbelink.DBHandler.FieldInt("Ninos", Me)

        Public Sub New(ByVal theId_producto As Integer, ByVal theordinal As Integer, ByVal theid_reservacion As Integer, ByVal theAdultos As Integer, ByVal theNinos As Integer)
            ordinal.Value = theordinal
            id_reservacion.Value = theid_reservacion
            Id_producto.Value = theId_producto
            Adultos.Value = theAdultos
            Ninos.Value = theNinos
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
            _fields.Add(id_reservacion)
            _fields.Add(Id_producto)
            _fields.Add(ordinal)
            _fields.Add(Adultos)
            _fields.Add(Ninos)
        End Sub

        Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As Orbelink.DBHandler.DBTable
            Dim theInstance As detalle_reservacion
            If forScripting Then
                theInstance = New detalle_reservacion(True)
            Else
                theInstance = New detalle_reservacion
            End If
            Return theInstance
        End Function

        Protected Overrides Sub SetKeysForScripting()
            _primaryKey = New PrimaryKey(Id_producto, True, True)
            _primaryKey.Add_PK(ordinal, True)
            _primaryKey.Add_PK(id_reservacion, True)

            _foreingKeys = New System.Collections.ObjectModel.Collection(Of ForeingKey)

            Dim llave As New ForeingKey(Id_producto, New Item().Id_producto)
            llave.Add_FK(ordinal, New Item().ordinal)
            _foreingKeys.Add(llave)
            _foreingKeys.Add(New ForeingKey(id_reservacion, New Reservacion().Id_Reservacion, ForeingKey.DeleteRule.CASCADE))

            _uniqueKeys = New System.Collections.ObjectModel.Collection(Of Orbelink.DBHandler.UniqueKey)
        End Sub

        Public Overrides ReadOnly Property TableName() As String
            Get
                Return _tableName
            End Get
        End Property

        Shared Function NewForScripting() As detalle_reservacion
            Dim temp As New detalle_reservacion()
            temp.SetFields()
temp.SetKeysForScripting()
temp.Fields.SelectAll()
Return temp
        End Function

    End Class
End Namespace