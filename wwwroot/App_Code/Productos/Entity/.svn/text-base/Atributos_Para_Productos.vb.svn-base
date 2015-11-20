Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler

Namespace Orbelink.Entity.Productos
    Public Class Atributos_Para_Productos : Inherits DBTable

        Dim _tableName As String = "PR_Atributos_Para_Productos"

        Public ReadOnly Id_atributo As New FieldInt("id_atributo", Me, False, True)
        Public ReadOnly Nombre As New FieldString("n_atributo", Me, Field.FieldType.VARCHAR_MULTILANG, 50)
        Public ReadOnly AutoCompletar As New FieldTinyInt("autoCompletar", Me)
        Public ReadOnly Descripcion As New FieldString("descripcion", Me, Field.FieldType.VARCHAR_MULTILANG, 200, True)

        Public Sub New()
            SetFields()
        End Sub

        Public Sub New(ByVal theNombre As String, ByVal theAutoCompletar As String, ByVal theDescripcion As String)
            Nombre.Value = theNombre
            AutoCompletar.Value = theAutoCompletar
            Descripcion.Value = theDescripcion
            SetFields()
            _primaryKey = Nothing
            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Protected Overrides Sub SetKeysForScripting()
            _primaryKey = New PrimaryKey(Id_atributo, True, True)

            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Private Sub SetFields()
            _fields = New FieldCollection
            _fields.Add(Id_atributo)
            _fields.Add(Nombre)
            _fields.Add(AutoCompletar)
            _fields.Add(Descripcion)
        End Sub

        Public Overrides ReadOnly Property TableName() As String
            Get
                Return _tableName
            End Get
        End Property

        Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As DBTable
            Dim theInstance As Atributos_Para_Productos
            If forScripting Then
                theInstance = NewForScripting()
            Else
                theInstance = New Atributos_Para_Productos
            End If
            Return theInstance
        End Function

        Shared Function NewForScripting() As Atributos_Para_Productos
            Dim temp As New Atributos_Para_Productos()
            temp.SetFields()
            temp.SetKeysForScripting()
            temp.Fields.SelectAll()
            Return temp
        End Function

    End Class
End Namespace