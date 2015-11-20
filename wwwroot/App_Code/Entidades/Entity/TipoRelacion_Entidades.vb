Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler

Namespace Orbelink.Entity.Entidades
    Public Class TipoRelacion_Entidades : Inherits DBTable

        Dim _tableName As String = "EN_TipoRelacion_Entidades"
        Public ReadOnly Id_TipoRelacion As New FieldInt("Id_TipoRelacion", Me, False, True)
        Public ReadOnly NombreBaseDependiente As New FieldString("n_TipoRelacionBD", Me, Field.FieldType.VARCHAR_MULTILANG, 50)
        Public ReadOnly NombreDependienteBase As New FieldString("n_TipoRelacionDB", Me, Field.FieldType.VARCHAR_MULTILANG, 50)

        Public Sub New()
            SetFields()
        End Sub

        Public Sub New(ByVal theNombreBaseDependiente As String, ByVal theNombreDependienteBase As String)
            NombreBaseDependiente.Value = theNombreBaseDependiente
            NombreDependienteBase.Value = theNombreDependienteBase
            SetFields()
            _primaryKey = Nothing
            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Protected Overrides Sub SetKeysForScripting()
            _primaryKey = New PrimaryKey(Id_TipoRelacion, True, True)
            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Private Sub SetFields()
            _fields = New FieldCollection
            _fields.Add(Id_TipoRelacion)
            _fields.Add(NombreBaseDependiente)
            _fields.Add(NombreDependienteBase)
        End Sub

        Public Overrides ReadOnly Property TableName() As String
            Get
                Return _tableName
            End Get
        End Property

        Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As DBTable
            Dim theInstance As TipoRelacion_Entidades
            If forScripting Then
                theInstance = NewForScripting()
            Else
                theInstance = New TipoRelacion_Entidades
            End If
            Return theInstance
        End Function

        Shared Function NewForScripting() As TipoRelacion_Entidades
            Dim temp As New TipoRelacion_Entidades()
            temp.SetFields()
            temp.SetKeysForScripting()
            temp.Fields.SelectAll()
            Return temp
        End Function

    End Class
End Namespace