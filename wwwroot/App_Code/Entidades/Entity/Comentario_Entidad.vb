Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler
Imports Orbelink.Entity.Comentarios

Namespace Orbelink.Entity.Entidades

    Public Class Comentario_Entidad : Inherits DBTable

        Dim _tableName As String = "EN_Comentario_Entidad"

        Public ReadOnly id_Comentario As New FieldInt("id_Comentario", Me)
        Public ReadOnly id_Entidad As New FieldInt("id_Entidad", Me)

        Public Sub New()
            setFields()
        End Sub

        Public Sub New(ByVal theid_Comentario As String, ByVal theid_Entidad As String)
            id_Comentario.Value = theid_Comentario
            id_Entidad.Value = theid_Entidad

            setFields()
            _primaryKey = Nothing
            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Protected Overrides Sub SetKeysForScripting()
            _primaryKey = New PrimaryKey(id_Comentario, True, True)
            _primaryKey.Add_PK(id_Entidad, True)

            _foreingKeys = New System.Collections.ObjectModel.Collection(Of ForeingKey)
            _foreingKeys.Add(New ForeingKey(id_Comentario, New Comentario().Id_Comentario, ForeingKey.DeleteRule.CASCADE))
            _foreingKeys.Add(New ForeingKey(id_Entidad, New Entidad().Id_Entidad))

            _uniqueKeys = Nothing
        End Sub
        Private Sub setFields()
            _fields = New FieldCollection
            _fields.Add(id_Comentario)
            _fields.Add(id_Entidad)
        End Sub

        Public Overrides ReadOnly Property TableName() As String
            Get
                Return _tableName
            End Get
        End Property

        Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As DBTable
            Dim theInstance As Comentario_Entidad
            If forScripting Then
                theInstance = NewForScripting()
            Else
                theInstance = New Comentario_Entidad
            End If
            Return theInstance
        End Function

        Shared Function NewForScripting() As Comentario_Entidad
            Dim temp As New Comentario_Entidad()
            temp.setFields()
            temp.SetKeysForScripting()
            temp.Fields.SelectAll()
            Return temp
        End Function
    End Class

End Namespace