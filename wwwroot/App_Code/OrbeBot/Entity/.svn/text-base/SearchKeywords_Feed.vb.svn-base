Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler

Namespace Orbelink.Entity.OrbeBot
    Public Class SearchKeywords_Feed : Inherits DBTable

        Dim _tableName As String = "OB_SearchKeywords_Feed"

        Public ReadOnly Id_Feed As New FieldInt("Id_Feed", Me)
        Public ReadOnly Id_Keyword As New FieldInt("Id_Keyword", Me)

        Public Sub New()
            SetFields()
        End Sub

        Public Sub New(ByVal theId_Feed As Integer, ByVal theId_Keyword As Integer)
            Id_Feed.Value = theId_Feed
            Id_Keyword.Value = theId_Keyword
            SetFields()
            _primaryKey = Nothing
            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Protected Overrides Sub SetKeysForScripting()
            _primaryKey = New PrimaryKey(Id_Feed, True, True)
            _primaryKey.Add_PK(Id_Keyword, True)

            _foreingKeys = New System.Collections.ObjectModel.Collection(Of ForeingKey)
            _foreingKeys.Add(New ForeingKey(Id_Feed, New Feed().Id_Feed, ForeingKey.DeleteRule.CASCADE))
            _foreingKeys.Add(New ForeingKey(Id_Keyword, New SearchKeywords().Id_Keyword, ForeingKey.DeleteRule.CASCADE))

            _uniqueKeys = Nothing
        End Sub

        Private Sub SetFields()
            _fields = New FieldCollection
            _fields.Add(Id_Feed)
            _fields.Add(Id_Keyword)
        End Sub

        Public Overrides ReadOnly Property TableName() As String
            Get
                Return _tableName
            End Get
        End Property

        Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As DBTable
            Dim theInstance As SearchKeywords_Feed
            If forScripting Then
                theInstance = NewForScripting()
            Else
                theInstance = New SearchKeywords_Feed
            End If
            Return theInstance
        End Function

        Shared Function NewForScripting() As SearchKeywords_Feed
            Dim temp As New SearchKeywords_Feed()
            temp.SetFields()
            temp.SetKeysForScripting()
            temp.Fields.SelectAll()
            Return temp
        End Function

    End Class
End Namespace