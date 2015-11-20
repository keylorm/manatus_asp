
Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler

Namespace Orbelink.Entity.Facturas
    Public Class Shippings_Country : Inherits DBTable

        Dim _tableName As String = "FR_Shippings_Country"

        Public ReadOnly Id_Country As New FieldInt("Id_Country", Me, False, True)
        Public ReadOnly Nombre As New FieldString("Nombre", Me, Field.FieldType.VARCHAR_MULTILANG, 100)
        Public ReadOnly CodigoISO As New FieldString("CodigoISO", Me, Field.FieldType.VARCHAR, 2)

        Public Sub New()
            SetFields()
        End Sub

        Public Sub New(ByVal theNombre As String, ByVal theCodigoISO As String)
            Nombre.Value = theNombre
            CodigoISO.Value = theCodigoISO
            SetFields()
            _primaryKey = Nothing
            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Protected Overrides Sub SetKeysForScripting()
            _primaryKey = New Orbelink.DBHandler.PrimaryKey(Id_Country, True, True)

            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Private Sub SetFields()
            _fields = New FieldCollection
            _fields.Add(Id_Country)
            _fields.Add(Nombre)
            _fields.Add(CodigoISO)
        End Sub

        Public Overrides ReadOnly Property TableName() As String
            Get
                Return _tableName
            End Get
        End Property

        Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As DBTable
            Dim theInstance As Shippings_Country
            If forScripting Then
                theInstance = NewForScripting()
            Else
                theInstance = New Shippings_Country
            End If
            Return theInstance
        End Function

        Shared Function NewForScripting() As Shippings_Country
            Dim temp As New Shippings_Country()
            temp.SetFields()
            temp.SetKeysForScripting()
            temp.Fields.SelectAll()
            Return temp
        End Function

    End Class

End Namespace