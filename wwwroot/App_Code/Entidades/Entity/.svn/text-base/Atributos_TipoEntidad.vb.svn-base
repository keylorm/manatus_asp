Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler

Namespace Orbelink.Entity.Entidades
    Public Class Atributos_TipoEntidad : Inherits DBTable

        Dim _tableName As String = "EN_Atributos_TipoEntidad"

        Public ReadOnly id_atributo As New FieldInt("id_atributo", Me)
        Public ReadOnly id_tipoEntidad As New FieldInt("id_tipoEntidad", Me)
        Public ReadOnly orden As New FieldInt("orden", Me)
        Public ReadOnly buscable As New FieldTinyInt("buscable", Me)

        Public Sub New()
            SetFields()
        End Sub

        Public Sub New(ByVal theid_atributo As Integer, ByVal theid_tipoEntidad As Integer, _
            ByVal theorden As Integer, ByVal thebuscable As Integer)
            id_atributo.Value = theid_atributo
            id_tipoEntidad.Value = theid_tipoEntidad
            orden.Value = theorden
            buscable.Value = thebuscable
            SetFields()
            _primaryKey = Nothing
            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Protected Overrides Sub SetKeysForScripting()
            _primaryKey = New PrimaryKey(id_atributo, True, True)
            _primaryKey.Add_PK(id_tipoEntidad, True)

            _foreingKeys = New System.Collections.ObjectModel.Collection(Of ForeingKey)
            _foreingKeys.Add(New ForeingKey(id_tipoEntidad, New TipoEntidad().Id_TipoEntidad, ForeingKey.DeleteRule.CASCADE))
            _foreingKeys.Add(New ForeingKey(id_atributo, New Atributos_Para_Entidades().Id_atributo, ForeingKey.DeleteRule.CASCADE))

            _uniqueKeys = Nothing
        End Sub

        Private Sub SetFields()
            _fields = New FieldCollection
            _fields.Add(id_atributo)
            _fields.Add(id_tipoEntidad)
            _fields.Add(orden)
            _fields.Add(buscable)
        End Sub

        Public Overrides ReadOnly Property TableName() As String
            Get
                Return _tableName
            End Get
        End Property

        Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As DBTable
            Dim theInstance As Atributos_TipoEntidad
            If forScripting Then
                theInstance = NewForScripting()
            Else
                theInstance = New Atributos_TipoEntidad
            End If
            Return theInstance
        End Function

        Shared Function NewForScripting() As Atributos_TipoEntidad
            Dim temp As New Atributos_TipoEntidad()
            temp.SetFields()
            temp.SetKeysForScripting()
            temp.Fields.SelectAll()
            Return temp
        End Function

    End Class
End Namespace