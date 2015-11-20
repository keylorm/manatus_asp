Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler

Namespace Orbelink.Entity.Security
    Public Class Perfil : Inherits DBTable

        Dim _tableName As String = "EN_TipoEntidad"

        Public ReadOnly Id_Perfil As New FieldInt("id_TipoEntidad", Me, False, True)
        Public ReadOnly Nombre As New FieldString("n_TipoEntidad", Me, Field.FieldType.VARCHAR_MULTILANG, 150)
        'Public ReadOnly Descripcion As New FieldString("descripcion", Me, Field.FieldType.VARCHAR_MULTILANG, 200, True)
        Public ReadOnly ClearanceLevel As New FieldInt("Nivel", Me)

        Public Sub New()
            SetFields()
        End Sub

        Public Sub New(ByVal theNombre As String, ByVal theDescripcion As String, _
            ByVal theClearanceLevel As Integer)
            Nombre.Value = theNombre
            ClearanceLevel.Value = theClearanceLevel
            SetFields()
            _primaryKey = Nothing
            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Protected Overrides Sub SetKeysForScripting()
            _primaryKey = New PrimaryKey(Id_Perfil, True, True)

            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Private Sub SetFields()
            _fields = New FieldCollection
            _fields.Add(Id_Perfil)
            _fields.Add(Nombre)
            _fields.Add(ClearanceLevel)
        End Sub

        Public Overrides ReadOnly Property TableName() As String
            Get
                Return _tableName
            End Get
        End Property

        Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As DBTable
            Dim theInstance As Perfil
            If forScripting Then
                theInstance = NewForScripting()
            Else
                theInstance = New Perfil
            End If
            Return theInstance
        End Function

        Shared Function NewForScripting() As Perfil
            Dim temp As New Perfil()
            temp.SetFields()
            temp.SetKeysForScripting()
            temp.Fields.SelectAll()
            Return temp
        End Function

    End Class
End Namespace