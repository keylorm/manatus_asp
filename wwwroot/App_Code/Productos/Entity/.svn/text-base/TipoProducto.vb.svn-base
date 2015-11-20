Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler

Namespace Orbelink.Entity.Productos
    Public Class TipoProducto : Inherits DBTable

        Dim _tableName As String = "PR_TipoProducto"

        Public ReadOnly Id_TipoProducto As New FieldInt("Id_TipoProducto", Me, False, True)
        Public ReadOnly Id_padre As New FieldInt("id_padre", Me)
        Public ReadOnly Nombre As New FieldString("n_TipoProducto", Me, Field.FieldType.VARCHAR_MULTILANG, 250)
        Public ReadOnly Codigo As New FieldString("codigo", Me, Field.FieldType.VARCHAR, 4000, True)

        Public Sub New()
            SetFields()
        End Sub

        Public Sub New(ByVal theId_padre As Integer, ByVal theNombre As String, ByVal theCodigo As String)
            Id_padre.Value = theId_padre
            Nombre.Value = theNombre
            Codigo.Value = theCodigo
            SetFields()
            _primaryKey = Nothing
            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Protected Overrides Sub SetKeysForScripting()
            _primaryKey = New PrimaryKey(Id_TipoProducto, True, True)

            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Private Sub SetFields()
            _fields = New FieldCollection
            _fields.Add(Id_TipoProducto)
            _fields.Add(Id_padre)
            _fields.Add(Nombre)
            _fields.Add(Codigo)
        End Sub


        Public Overrides ReadOnly Property TableName() As String
            Get
                Return _tableName
            End Get
        End Property

        Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As DBTable
            Dim theInstance As TipoProducto
            If forScripting Then
                theInstance = NewForScripting()
            Else
                theInstance = New TipoProducto
            End If
            Return theInstance
        End Function

        Shared Function NewForScripting() As TipoProducto
            Dim temp As New TipoProducto()
            temp.SetFields()
            temp.SetKeysForScripting()
            temp.Fields.SelectAll()
            Return temp
        End Function

    End Class
End Namespace