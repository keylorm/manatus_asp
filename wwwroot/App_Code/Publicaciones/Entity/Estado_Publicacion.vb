Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler

Namespace Orbelink.Entity.Publicaciones
    Public Class Estado_Publicacion : Inherits DBTable

        Dim _tableName As String = "PU_Estado_Publicacion"


        Public ReadOnly Id_Estado_Publicacion As New FieldInt("Id_Estado_Publicacion", Me, False, True)
        Public ReadOnly Nombre As New FieldString("N_Estado_Publicacion", Me, Field.FieldType.VARCHAR_MULTILANG, 50)

        Public Sub New()
            SetFields()
        End Sub

        Public Sub New(ByVal theNombre As String)
            Nombre.Value = theNombre
            SetFields()
            _primaryKey = Nothing
            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Protected Overrides Sub SetKeysForScripting()
            _primaryKey = New PrimaryKey(Id_Estado_Publicacion, True, True)

            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Private Sub SetFields()
            _fields = New FieldCollection
            _fields.Add(Id_Estado_Publicacion)
            _fields.Add(Nombre)
        End Sub


        Public Overrides ReadOnly Property TableName() As String
            Get
                Return _tableName
            End Get
        End Property

        Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As DBTable
            Dim theInstance As Estado_Publicacion
            If forScripting Then
                theInstance = NewForScripting()
            Else
                theInstance = New Estado_Publicacion
            End If
            Return theInstance
        End Function

        Shared Function NewForScripting() As Estado_Publicacion
            Dim temp As New Estado_Publicacion()
            temp.SetFields()
            temp.SetKeysForScripting()
            temp.Fields.SelectAll()
            Return temp
        End Function

    End Class
End Namespace