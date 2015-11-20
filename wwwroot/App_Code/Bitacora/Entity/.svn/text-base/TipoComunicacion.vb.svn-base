Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler

Namespace Orbelink.Entity.Bitacora

    Public Class TipoComunicacion : Inherits DBTable

        Dim _tableName As String = "BIT_TipoComunicacion"

        Public ReadOnly Id_TipoComunicacion As New FieldInt("id_tipoComunicacion", Me, False, True)
        Public ReadOnly Nombre As New FieldString("n_tipoComunicacion", Me, Field.FieldType.VARCHAR_MULTILANG, 150)

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
            _primaryKey = New PrimaryKey(Id_TipoComunicacion, True, True)

            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Private Sub SetFields()
            _fields = New FieldCollection
            _fields.Add(Id_TipoComunicacion)
            _fields.Add(Nombre)
        End Sub

        Public Overrides ReadOnly Property TableName() As String
            Get
                Return _tableName
            End Get
        End Property

        Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As DBTable
            Dim theInstance As TipoComunicacion
            If forScripting Then
                theInstance = New TipoComunicacion(True)
            Else
                theInstance = New TipoComunicacion
            End If
            Return theInstance
        End Function

        Shared Function NewForScripting() As TipoComunicacion
            Dim temp As New TipoComunicacion()
            temp.SetFields()
            temp.SetKeysForScripting()
            temp.Fields.SelectAll()
            Return temp
        End Function
    End Class

End Namespace