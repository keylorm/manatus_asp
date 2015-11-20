Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler
Imports Orbelink.Entity.Bitacora

Namespace Orbelink.Entity.Entidades

    Public Class Tema_Entidad : Inherits DBTable

        Dim _tableName As String = "EN_Tema_Entidad"

        Public ReadOnly id_Tema As New FieldInt("id_Tema", Me)
        Public ReadOnly id_Entidad As New FieldInt("id_Entidad", Me)

        Public Sub New()
            setFields()
        End Sub

        Public Sub New(ByVal theid_Tema As String, ByVal theid_Entidad As String)
            id_Tema.Value = theid_Tema
            id_Entidad.Value = theid_Entidad

            setFields()
            _primaryKey = Nothing
            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Protected Overrides Sub SetKeysForScripting()
            _primaryKey = New PrimaryKey(id_Tema, True, True)
            _primaryKey.Add_PK(id_Entidad, True)

            _foreingKeys = New System.Collections.ObjectModel.Collection(Of ForeingKey)
            _foreingKeys.Add(New ForeingKey(id_Tema, New Tema().Id_Tema))
            _foreingKeys.Add(New ForeingKey(id_Entidad, New Entidad().Id_entidad))

            _uniqueKeys = Nothing
        End Sub
        Private Sub setFields()
            _fields = New FieldCollection
            _fields.Add(id_Tema)
            _fields.Add(id_Entidad)
        End Sub

        Public Overrides ReadOnly Property TableName() As String
            Get
                Return _tableName
            End Get
        End Property

        Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As DBTable
            Dim theInstance As Tema_Entidad
            If forScripting Then
                theInstance = NewForScripting()
            Else
                theInstance = New Tema_Entidad
            End If
            Return theInstance
        End Function

        Shared Function NewForScripting() As Tema_Entidad
            Dim temp As New Tema_Entidad()
            temp.setFields()
            temp.SetKeysForScripting()
            temp.Fields.SelectAll()
            Return temp
        End Function
    End Class

End Namespace