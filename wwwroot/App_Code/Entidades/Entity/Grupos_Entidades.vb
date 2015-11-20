Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler

Namespace Orbelink.Entity.Entidades
    Public Class Grupos_Entidades : Inherits DBTable

        Dim _tableName As String = "EN_Grupos_Entidades"
        Public ReadOnly Id_Grupo As New FieldInt("Id_Grupo", Me, False, True)
        Public ReadOnly Id_padre As New FieldInt("id_padre", Me)
        Public ReadOnly Nombre As New FieldString("n_Grupo", Me, Field.FieldType.VARCHAR_MULTILANG, 50)
        Public ReadOnly Descripcion As New FieldString("Descripcion", Me, Field.FieldType.VARCHAR_MULTILANG, 200, True)

        Public Sub New()
            SetFields()
        End Sub

        Public Sub New(ByVal theId_padre As Integer, ByVal theNombre As String, ByVal theDescripcion As String)
            Id_padre.Value = theId_padre
            Nombre.Value = theNombre
            Descripcion.Value = theDescripcion
            SetFields()
            _primaryKey = Nothing
            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Protected Overrides Sub SetKeysForScripting()
            _primaryKey = New PrimaryKey(Id_Grupo, True, True)
            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Private Sub SetFields()
            _fields = New FieldCollection
            _fields.Add(Id_Grupo)
            _fields.Add(Id_padre)
            _fields.Add(Nombre)
            _fields.Add(Descripcion)

        End Sub

        Public Overrides ReadOnly Property TableName() As String
            Get
                Return _tableName
            End Get
        End Property

        Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As DBTable
            Dim theInstance As Grupos_Entidades
            If forScripting Then
                theInstance = NewForScripting()
            Else
                theInstance = New Grupos_Entidades
            End If
            Return theInstance
        End Function

        Shared Function NewForScripting() As Grupos_Entidades
            Dim temp As New Grupos_Entidades()
            temp.SetFields()
            temp.SetKeysForScripting()
            temp.Fields.SelectAll()
            Return temp
        End Function

    End Class
End Namespace