Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler

Namespace Orbelink.Entity.Currency

    Public Class Moneda : Inherits DBTable

        Dim _tableName As String = "OR_Moneda"

        Public ReadOnly Id_Moneda As New FieldInt("Id_Moneda", Me, False, True)
        Public ReadOnly Nombre As New FieldString("n_Unidad", Me, Field.FieldType.VARCHAR_MULTILANG, 50)
        Public ReadOnly Simbolo As New FieldString("Simbolo", Me, Field.FieldType.VARCHAR, 1)

        Public Sub New()
            SetFields()
        End Sub

        Public Sub New(ByVal theNombre As String, ByVal theSimbolo As String)
            Nombre.Value = theNombre
            Simbolo.Value = theSimbolo
            SetFields()
            _primaryKey = Nothing
            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Protected Overrides Sub SetKeysForScripting()
            _primaryKey = New Orbelink.DBHandler.PrimaryKey(Id_Moneda, True, True)

            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Private Sub SetFields()
            _fields = New FieldCollection
            _fields.Add(Id_Moneda)
            _fields.Add(Nombre)
            _fields.Add(Simbolo)
        End Sub


        Public Overrides ReadOnly Property TableName() As String
            Get
                Return _tableName
            End Get
        End Property

        Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As DBTable
            Dim theInstance As Moneda
            If forScripting Then
                theInstance = NewForScripting()
            Else
                theInstance = New Moneda
            End If
            Return theInstance
        End Function

        Shared Function NewForScripting() As Moneda
            Dim temp As New Moneda()
            temp.SetFields()
            temp.SetKeysForScripting()
            temp.Fields.SelectAll()
            Return temp
        End Function

    End Class
End Namespace