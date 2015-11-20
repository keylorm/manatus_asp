Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler

Namespace Orbelink.Entity.Publicaciones
    Public Class TipoPublicacion : Inherits DBTable

        Dim _TableName As String = "PU_TipoPublicacion"

        Public ReadOnly Id_TipoPublicacion As New FieldInt("id_TipoPublicacion", Me, False, True)
        Public ReadOnly Nombre As New FieldString("N_TipoPublicacion", Me, Field.FieldType.VARCHAR_MULTILANG, 200)
        Public ReadOnly Descripcion As New FieldString("Descripcion", Me, Field.FieldType.VARCHAR_MULTILANG, 200, True)

        Public Sub New()
            SetFields()
        End Sub

        Public Sub New(ByVal theNombre As String, ByVal theDescripcion As String)
            Nombre.Value = theNombre
            Descripcion.Value = theDescripcion
            SetFields()
            _primaryKey = Nothing
            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Protected Overrides Sub SetKeysForScripting()
            _primaryKey = New PrimaryKey(Id_TipoPublicacion, True, True)

            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Private Sub SetFields()
            _fields = New FieldCollection
            _fields.Add(Id_TipoPublicacion)
            _fields.Add(Nombre)
            _fields.Add(Descripcion)
        End Sub

        Public Overrides ReadOnly Property TableName() As String
            Get
                Return _TableName
            End Get
        End Property

        Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As DBTable
            Dim theInstance As TipoPublicacion
            If forScripting Then
                theInstance = NewForScripting()
            Else
                theInstance = New TipoPublicacion
            End If
            Return theInstance
        End Function

        Shared Function NewForScripting() As TipoPublicacion
            Dim temp As New TipoPublicacion()
            temp.SetFields()
            temp.SetKeysForScripting()
            temp.Fields.SelectAll()
            Return temp
        End Function

    End Class
End Namespace