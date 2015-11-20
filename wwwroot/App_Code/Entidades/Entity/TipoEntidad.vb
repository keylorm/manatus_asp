Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler

Namespace Orbelink.Entity.Entidades
    Public Class TipoEntidad : Inherits DBTable

        Dim _tableName As String = "EN_TipoEntidad"

        Public ReadOnly Id_TipoEntidad As New FieldInt("id_tipoEntidad", Me, False, True)
        Public ReadOnly Nombre As New FieldString("n_tipoEntidad", Me, Field.FieldType.VARCHAR_MULTILANG, 150)
        Public ReadOnly Descripcion As New FieldString("descripcion", Me, Field.FieldType.VARCHAR_MULTILANG, 200, True)
        Public ReadOnly Nivel As New FieldInt("Nivel", Me)

        Public Sub New()
            SetFields()
        End Sub

        Public Sub New(ByVal theNombre As String, ByVal theDescripcion As String, _
            ByVal theNivel As Integer)
            Nombre.Value = theNombre
            Descripcion.Value = theDescripcion
            Nivel.Value = theNivel
            SetFields()
            _primaryKey = Nothing
            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Protected Overrides Sub SetKeysForScripting()
            _primaryKey = New PrimaryKey(Id_TipoEntidad, True, True)

            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Private Sub SetFields()
            _fields = New FieldCollection
            _fields.Add(Id_TipoEntidad)
            _fields.Add(Nombre)
            _fields.Add(Descripcion)
            _fields.Add(Nivel)
        End Sub

        Public Overrides ReadOnly Property TableName() As String
            Get
                Return _tableName
            End Get
        End Property

        Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As DBTable
            Dim theInstance As TipoEntidad
            If forScripting Then
                theInstance = NewForScripting()
            Else
                theInstance = New TipoEntidad
            End If
            Return theInstance
        End Function

        Shared Function NewForScripting() As TipoEntidad
            Dim temp As New TipoEntidad()
            temp.SetFields()
            temp.SetKeysForScripting()
            temp.Fields.SelectAll()
            Return temp
        End Function

    End Class
End Namespace