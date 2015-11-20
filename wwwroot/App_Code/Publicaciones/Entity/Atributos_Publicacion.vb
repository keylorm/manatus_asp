Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler

Namespace Orbelink.Entity.Publicaciones
    Public Class Atributos_Publicacion : Inherits DBTable

        Dim _tableName As String = "PU_Atributos_Publicacion"

        Public ReadOnly Id_publicacion As New FieldInt("id_publicacion", Me, False, False)
        Public ReadOnly Id_atributo As New FieldInt("id_atributo", Me, False, False)
        Public ReadOnly Ordinal As New FieldInt("ordinal", Me)
        Public ReadOnly Valor As New FieldString("valor", Me, Field.FieldType.VARCHAR_MULTILANG, 5000, True)
        Public ReadOnly visible As New FieldTinyInt("visible", Me, True)

        Public Sub New()
            SetFields()
        End Sub

        Public Sub New(ByVal theId_entidad As String, ByVal theId_atributo As String, ByVal theordinal As String, _
            ByVal theValor As String, ByVal thevisible As String)
            Id_publicacion.Value = theId_entidad
            Id_atributo.Value = theId_atributo
            Ordinal.Value = theordinal
            Valor.Value = theValor
            visible.Value = thevisible
            SetFields()
            _primaryKey = Nothing
            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Protected Overrides Sub SetKeysForScripting()
            _primaryKey = New PrimaryKey(Id_publicacion, True, True)
            _primaryKey.Add_PK(Id_atributo, True)
            _primaryKey.Add_PK(Ordinal, True)

            _foreingKeys = New System.Collections.ObjectModel.Collection(Of ForeingKey)
            _foreingKeys.Add(New ForeingKey(Id_publicacion, New Publicacion().Id_Publicacion, ForeingKey.DeleteRule.CASCADE))
            _foreingKeys.Add(New ForeingKey(Id_atributo, New Atributos_Para_Publicaciones().Id_atributo))

            _uniqueKeys = Nothing
        End Sub

        Private Sub SetFields()
            _fields = New FieldCollection
            _fields.Add(Id_publicacion)
            _fields.Add(Id_atributo)
            _fields.Add(Ordinal)
            _fields.Add(visible)
            _fields.Add(Valor)
        End Sub


        Public Overrides ReadOnly Property TableName() As String
            Get
                Return _tableName
            End Get
        End Property

        Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As DBTable
            Dim theInstance As Atributos_Publicacion
            If forScripting Then
                theInstance = NewForScripting()
            Else
                theInstance = New Atributos_Publicacion
            End If
            Return theInstance
        End Function

        Shared Function NewForScripting() As Atributos_Publicacion
            Dim temp As New Atributos_Publicacion()
            temp.SetFields()
            temp.SetKeysForScripting()
            temp.Fields.SelectAll()
            Return temp
        End Function

    End Class
End Namespace