Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler

Namespace Orbelink.Entity.Entidades

    Public Class Atributos_Entidad : Inherits DBTable

        Dim _tableName As String = "EN_Atributos_Entidad"

        Public ReadOnly Id_entidad As New FieldInt("id_entidad", Me)
        Public ReadOnly Id_atributo As New FieldInt("id_atributo", Me)
        Public ReadOnly Ordinal As New FieldInt("ordinal", Me)
        Public ReadOnly Valor As New FieldString("valor", Me, Field.FieldType.VARCHAR_MULTILANG, 50, True)
        Public ReadOnly visible As New FieldTinyInt("visible", Me, True)

        Public Sub New()
            SetFields()
        End Sub

        Public Sub New(ByVal theId_entidad As Integer, ByVal theId_atributo As Integer, ByVal theordinal As Integer, _
            ByVal theValor As String, ByVal theVisible As Integer)
            Id_entidad.Value = theId_entidad
            Id_atributo.Value = theId_atributo
            Ordinal.Value = theordinal
            Valor.Value = theValor
            visible.Value = theVisible
            SetFields()
            _primaryKey = Nothing
            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Protected Overrides Sub SetKeysForScripting()
            _primaryKey = New PrimaryKey(Id_entidad, True, True)
            _primaryKey.Add_PK(Id_atributo, True)
            _primaryKey.Add_PK(Ordinal, True)

            _foreingKeys = New System.Collections.ObjectModel.Collection(Of ForeingKey)
            _foreingKeys.Add(New ForeingKey(Id_entidad, New Entidad().Id_entidad, ForeingKey.DeleteRule.CASCADE))
            _foreingKeys.Add(New ForeingKey(Id_atributo, New Atributos_Para_Entidades().Id_atributo))

            _uniqueKeys = Nothing
        End Sub

        Private Sub SetFields()
            _fields = New FieldCollection
            _fields.Add(Id_entidad)
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
            Dim theInstance As Atributos_Entidad
            If forScripting Then
                theInstance = NewForScripting()
            Else
                theInstance = New Atributos_Entidad
            End If
            Return theInstance
        End Function

        Shared Function NewForScripting() As Atributos_Entidad
            Dim temp As New Atributos_Entidad()
            temp.SetFields()
            temp.SetKeysForScripting()
            temp.Fields.SelectAll()
            Return temp
        End Function

    End Class
End Namespace