Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler

Namespace Orbelink.Entity.Publicaciones
    Public Class Atributos_TipoPublicacion : Inherits DBTable

        Dim _tableName As String = "PU_Atributos_TipoPublicacion"


        Public ReadOnly id_atributo As New FieldInt("id_atributo", Me, False, False)
        Public ReadOnly id_tipoPublicacion As New FieldInt("id_tipoPublicacion", Me, False, False)
        Public ReadOnly orden As New FieldInt("orden", Me)
        Public ReadOnly buscable As New FieldTinyInt("buscable", Me)

        Public Sub New()
            SetFields()
        End Sub

        Public Sub New(ByVal theid_atributo As String, ByVal theid_tipoPublicacion As String, _
            ByVal theorden As String, ByVal thebuscable As String)
            id_atributo.Value = theid_atributo
            id_tipoPublicacion.Value = theid_tipoPublicacion
            orden.Value = theorden
            buscable.Value = thebuscable
            SetFields()
            _primaryKey = Nothing
            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Protected Overrides Sub SetKeysForScripting()
            _primaryKey = New PrimaryKey(id_atributo, True, True)
            _primaryKey.Add_PK(id_tipoPublicacion, True)

            _foreingKeys = New System.Collections.ObjectModel.Collection(Of ForeingKey)
            _foreingKeys.Add(New ForeingKey(id_tipoPublicacion, New TipoPublicacion().Id_TipoPublicacion, ForeingKey.DeleteRule.CASCADE))
            _foreingKeys.Add(New ForeingKey(id_atributo, New Atributos_Para_Publicaciones().Id_atributo, ForeingKey.DeleteRule.CASCADE))

            _uniqueKeys = Nothing
        End Sub

        Private Sub SetFields()
            _fields = New FieldCollection
            _fields.Add(id_atributo)
            _fields.Add(id_tipoPublicacion)
            _fields.Add(orden)
            _fields.Add(buscable)

        End Sub


        Public Overrides ReadOnly Property TableName() As String
            Get
                Return _tableName
            End Get
        End Property

        Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As DBTable
            Dim theInstance As Atributos_TipoPublicacion
            If forScripting Then
                theInstance = NewForScripting()
            Else
                theInstance = New Atributos_TipoPublicacion
            End If
            Return theInstance
        End Function

        Shared Function NewForScripting() As Atributos_TipoPublicacion
            Dim temp As New Atributos_TipoPublicacion()
            temp.SetFields()
            temp.SetKeysForScripting()
            temp.Fields.SelectAll()
            Return temp
        End Function

    End Class
End Namespace