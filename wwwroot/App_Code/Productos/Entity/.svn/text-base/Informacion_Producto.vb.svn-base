Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler

Namespace Orbelink.Entity.Productos
    Public Class Informacion_Producto : Inherits DBTable

        Dim _tableName As String = "PR_Informacion_Producto"

        Public ReadOnly Id_Producto As New FieldInt("Id_Producto", Me)
        Public ReadOnly Contador As New FieldInt("Contador", Me)
        Public ReadOnly UltimaVisita As New FieldDateTime("UltimaVisita", Me, True)
        Public ReadOnly Rating As New FieldFloat("Rating", Me)
        Public ReadOnly Votos As New FieldInt("Votos", Me)

        Public Sub New()
            SetFields()
        End Sub

        <Obsolete("Esta metodo recibe una o mas fechas en UTC", False)> _
        Public Sub New(ByVal theId_Producto As String, ByVal theContador As String, ByVal theUltimaVisita As String, _
            ByVal theRating As String, ByVal theVotos As String)
            Id_Producto.Value = theId_Producto
            Contador.Value = theContador
            UltimaVisita.ValueUtc = theUltimaVisita
            Rating.Value = theRating
            Votos.Value = theVotos
            SetFields()
            _primaryKey = Nothing
            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Protected Overrides Sub SetKeysForScripting()
            _primaryKey = New PrimaryKey(Id_Producto, True, True)

            _foreingKeys = New System.Collections.ObjectModel.Collection(Of ForeingKey)
            _foreingKeys.Add(New ForeingKey(Id_Producto, New Producto().Id_Producto, ForeingKey.DeleteRule.CASCADE))

            _uniqueKeys = Nothing
        End Sub

        Private Sub SetFields()
            _fields = New FieldCollection
            _fields.Add(Id_Producto)
            _fields.Add(Contador)
            _fields.Add(UltimaVisita)
            _fields.Add(Rating)
            _fields.Add(Votos)
        End Sub

        Public Overrides ReadOnly Property TableName() As String
            Get
                Return _tableName
            End Get
        End Property

        Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As DBTable
            Dim theInstance As Informacion_Producto
            If forScripting Then
                theInstance = NewForScripting()
            Else
                theInstance = New Informacion_Producto
            End If
            Return theInstance
        End Function

        Shared Function NewForScripting() As Informacion_Producto
            Dim temp As New Informacion_Producto()
            temp.SetFields()
            temp.SetKeysForScripting()
            temp.Fields.SelectAll()
            Return temp
        End Function

    End Class
End Namespace