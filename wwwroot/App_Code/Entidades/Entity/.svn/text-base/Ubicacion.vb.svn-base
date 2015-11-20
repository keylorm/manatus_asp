Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler

Namespace Orbelink.Entity.Entidades
    Public Class Ubicacion : Inherits DBTable

        Dim _tableName As String = "EN_Ubicacion"

        Public ReadOnly Id_ubicacion As New FieldInt("id_ubicacion", Me, False, True)
        Public ReadOnly Id_padre As New FieldInt("id_padre", Me)
        Public ReadOnly Nombre As New FieldString("n_ubicacion", Me, Field.FieldType.VARCHAR_MULTILANG, 150)

        Public Sub New()
            SetFields()
        End Sub

        Public Sub New(ByVal theId_padre As Integer, ByVal theNombre As String)
            Id_padre.Value = theId_padre
            Nombre.Value = theNombre
            SetFields()
            _primaryKey = Nothing
            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Protected Overrides Sub SetKeysForScripting()
            _primaryKey = New PrimaryKey(Id_ubicacion, True, True)
            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Private Sub SetFields()
            _fields = New FieldCollection
            _fields.Add(Id_ubicacion)
            _fields.Add(Id_padre)
            _fields.Add(Nombre)
        End Sub

        Public Overrides ReadOnly Property TableName() As String
            Get
                Return _tableName
            End Get
        End Property

        Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As DBTable
            Dim theInstance As Ubicacion
            If forScripting Then
                theInstance = NewForScripting()
            Else
                theInstance = New Ubicacion
            End If
            Return theInstance
        End Function

        Shared Function NewForScripting() As Ubicacion
            Dim temp As New Ubicacion()
            temp.SetFields()
            temp.SetKeysForScripting()
            temp.Fields.SelectAll()
            Return temp
        End Function

    End Class
End Namespace