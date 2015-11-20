Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler

Namespace Orbelink.Entity.OrbeBot
    Public Class SearchKeywords : Inherits DBTable

        Dim _tableName As String = "OB_SearchKeywords"

        Public ReadOnly Id_Keyword As New FieldInt("id_Keyword", Me, False, True)
        Public ReadOnly Nombre As New FieldString("N_Keyword", Me, Field.FieldType.VARCHAR_MULTILANG, 50)
        Public ReadOnly Descripcion As New FieldString("Descripcion", Me, Field.FieldType.VARCHAR_MULTILANG, 250)
        Public ReadOnly Visible As New FieldTinyInt("Visible", Me)

        Public Sub New()
            SetFields()
        End Sub

        Public Sub New(ByVal theNombre As String, ByVal theDescripcion As String, ByVal theVisible As Integer)
            Nombre.Value = theNombre
            Descripcion.Value = theDescripcion
            Visible.Value = theVisible
            SetFields()
            _primaryKey = Nothing
            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Protected Overrides Sub SetKeysForScripting()
            _primaryKey = New PrimaryKey(Id_Keyword, True, True)

            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Private Sub SetFields()
            _fields = New FieldCollection
            _fields.Add(Id_Keyword)
            _fields.Add(Nombre)
            _fields.Add(Descripcion)
            _fields.Add(Visible)
        End Sub

        Public Overrides ReadOnly Property TableName() As String
            Get
                Return _tableName
            End Get
        End Property

        Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As DBTable
            Dim theInstance As SearchKeywords
            If forScripting Then
                theInstance = NewForScripting()
            Else
                theInstance = New SearchKeywords
            End If
            Return theInstance
        End Function

        Shared Function NewForScripting() As SearchKeywords
            Dim temp As New SearchKeywords()
            temp.SetFields()
            temp.SetKeysForScripting()
            temp.Fields.SelectAll()
            Return temp
        End Function

    End Class
End Namespace