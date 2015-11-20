Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler

Namespace Orbelink.Entity.Publicaciones
    Public Class Informacion_Publicacion : Inherits DBTable

        Dim _tableName As String = "PU_Informacion_Publicacion"


        Public ReadOnly Id_Publicacion As New FieldInt("Id_Publicacion", Me)
        Public ReadOnly ReglasUso As New FieldString("ReglasUso", Me, Field.FieldType.VARCHAR, 250)

        Public Sub New()
            SetFields()
        End Sub

        Public Sub New(ByVal theId_Publicacion As String, ByVal theReglasUso As String)
            Id_Publicacion.Value = theId_Publicacion
            ReglasUso.Value = theReglasUso
            SetFields()
            _primaryKey = Nothing
            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Protected Overrides Sub SetKeysForScripting()
            _primaryKey = New PrimaryKey(Id_Publicacion, True, True)

            _foreingKeys = New System.Collections.ObjectModel.Collection(Of ForeingKey)
            _foreingKeys.Add(New ForeingKey(Id_Publicacion, New Publicacion().Id_Publicacion, ForeingKey.DeleteRule.CASCADE))

            _uniqueKeys = Nothing
        End Sub

        Private Sub SetFields()
            _fields = New FieldCollection
            _fields.Add(Id_Publicacion)
            _fields.Add(ReglasUso)
        End Sub


        Public Overrides ReadOnly Property TableName() As String
            Get
                Return _tableName
            End Get
        End Property

        Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As DBTable
            Dim theInstance As Informacion_Publicacion
            If forScripting Then
                theInstance = NewForScripting()
            Else
                theInstance = New Informacion_Publicacion
            End If
            Return theInstance
        End Function

        Shared Function NewForScripting() As Informacion_Publicacion
            Dim temp As New Informacion_Publicacion()
            temp.SetFields()
            temp.SetKeysForScripting()
            temp.Fields.SelectAll()
            Return temp
        End Function

    End Class
End Namespace