Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler

Namespace Orbelink.Entity.Productos
    Public Class Puntos_Producto : Inherits DBTable

        Dim _tableName As String = "PR_Puntos_Producto"

        Public ReadOnly Id_Punto As New FieldInt("id_Punto", Me, False, True)
        Public ReadOnly Id_Producto As New FieldInt("Id_Producto", Me, False)
        Public ReadOnly Nombre As New FieldString("N_Punto", Me, Field.FieldType.VARCHAR_MULTILANG, 50, False)
        Public ReadOnly Descr_Punto As New FieldString("Descr_Punto", Me, Field.FieldType.VARCHAR_MULTILANG, 200, True)
        Public ReadOnly Lon As New FieldFloat("Lon", Me)
        Public ReadOnly Lat As New FieldFloat("Lat", Me)

        Public Sub New()
            SetFields()
        End Sub

        Public Sub New(ByVal theId_Punto As Integer, ByVal theId_Producto As Integer, ByVal theNombre As String, _
        ByVal theDescr_Punto As String, ByVal theLon As String, ByVal theLat As String)
            Id_Punto.Value = theId_Punto
            Id_Producto.Value = theId_Producto
            Nombre.Value = theNombre
            Descr_Punto.Value = theDescr_Punto
            Lon.Value = theLon
            Lat.Value = theLat
            SetFields()
            _primaryKey = Nothing
            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Protected Overrides Sub SetKeysForScripting()
            _primaryKey = New PrimaryKey(Id_Punto, True, True)

            _foreingKeys = New System.Collections.ObjectModel.Collection(Of ForeingKey)
            _foreingKeys.Add(New ForeingKey(Id_Producto, New Producto().Id_Producto, ForeingKey.DeleteRule.CASCADE))

            _uniqueKeys = Nothing
        End Sub

        Private Sub SetFields()
            _fields = New FieldCollection
            _fields.Add(Id_Punto)
            _fields.Add(Id_Producto)
            _fields.Add(Nombre)
            _fields.Add(Descr_Punto)
            _fields.Add(Lon)
            _fields.Add(Lat)
        End Sub

        Public Overrides ReadOnly Property TableName() As String
            Get
                Return _tableName
            End Get
        End Property

        Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As DBTable
            Dim theInstance As Puntos_Producto
            If forScripting Then
                theInstance = NewForScripting()
            Else
                theInstance = New Puntos_Producto
            End If
            Return theInstance
        End Function

        Shared Function NewForScripting() As Puntos_Producto
            Dim temp As New Puntos_Producto()
            temp.SetFields()
            temp.SetKeysForScripting()
            temp.Fields.SelectAll()
            Return temp
        End Function

    End Class
End Namespace