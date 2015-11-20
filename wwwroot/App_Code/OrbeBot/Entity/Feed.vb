Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler

Namespace Orbelink.Entity.OrbeBot
    Public Class Feed : Inherits DBTable

        Dim _tableName As String = "OB_Feed"

        Public ReadOnly Id_Feed As New FieldInt("id_Feed", Me, False, True)
        Public ReadOnly Source As New FieldString("Source", Me, Field.FieldType.VARCHAR_MULTILANG, 150)
        Public ReadOnly Nombre As New FieldString("n_Feed", Me, Field.FieldType.VARCHAR_MULTILANG, 100)

        Public Sub New()
            SetFields()
        End Sub

        Public Sub New(ByVal theSource As String, ByVal theNombre As String)
            Source.Value = theSource
            Nombre.Value = theNombre
            SetFields()
            _primaryKey = Nothing
            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Protected Overrides Sub SetKeysForScripting()
            _primaryKey = New PrimaryKey(Id_Feed, True, True)

            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Private Sub SetFields()
            _fields = New FieldCollection
            _fields.Add(Id_Feed)
            _fields.Add(Source)
            _fields.Add(Nombre)
        End Sub

        Public Overrides ReadOnly Property TableName() As String
            Get
                Return _tableName
            End Get
        End Property

        Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As DBTable
            Dim theInstance As Feed
            If forScripting Then
                theInstance = NewForScripting()
            Else
                theInstance = New Feed
            End If
            Return theInstance
        End Function

        Shared Function NewForScripting() As Feed
            Dim temp As New Feed()
            temp.SetFields()
            temp.SetKeysForScripting()
            temp.Fields.SelectAll()
            Return temp
        End Function

    End Class
End Namespace