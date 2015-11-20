Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler

Namespace Orbelink.Entity.Productos
    Public Class Estados_Productos : Inherits DBTable

        Dim _tableName As String = "PR_Estados_Productos"

        Public ReadOnly Id_Estado_Producto As New FieldInt("Id_Estado_Producto", Me, False, True)
        Public ReadOnly Nombre As New FieldString("N_Estado_Producto", Me, Field.FieldType.VARCHAR_MULTILANG, 50)

        Public Sub New()
            SetFields()
        End Sub

        Public Sub New(ByVal theNombre As String)
            Nombre.Value = theNombre
            SetFields()
            _primaryKey = Nothing
            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Protected Overrides Sub SetKeysForScripting()
            _primaryKey = New PrimaryKey(Id_Estado_Producto, True, True)

            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Private Sub SetFields()
            _fields = New FieldCollection
            _fields.Add(Id_Estado_Producto)
            _fields.Add(Nombre)
        End Sub

        Public Overrides ReadOnly Property TableName() As String
            Get
                Return _tableName
            End Get
        End Property

        Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As DBTable
            Dim theInstance As Estados_Productos
            If forScripting Then
                theInstance = NewForScripting()
            Else
                theInstance = New Estados_Productos
            End If
            Return theInstance
        End Function

        Shared Function NewForScripting() As Estados_Productos
            Dim temp As New Estados_Productos()
            temp.SetFields()
            temp.SetKeysForScripting()
            temp.Fields.SelectAll()
            Return temp
        End Function

    End Class
End Namespace