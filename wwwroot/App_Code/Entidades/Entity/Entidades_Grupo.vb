Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler

Namespace Orbelink.Entity.Entidades
    Public Class Entidades_Grupo : Inherits DBTable

        Dim _tableName As String = "EN_Entidades_Grupo"

        Public ReadOnly Id_Grupo As New FieldInt("Id_Grupo", Me, False)
        Public ReadOnly Id_Entidad As New FieldInt("Id_Entidad", Me, False)
        Public ReadOnly Id_TipoRelacion As New FieldInt("Id_TipoRelacion", Me, False)

        Public Sub New()
            SetFields()
        End Sub

        Public Sub New(ByVal theId_Grupo As Integer, ByVal theId_Entidad As Integer, ByVal theId_TipoRelacion As Integer)
            Id_Entidad.Value = theId_Entidad
            Id_TipoRelacion.Value = theId_TipoRelacion
            Id_Grupo.Value = theId_Grupo
            SetFields()
            _primaryKey = Nothing
            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Protected Overrides Sub SetKeysForScripting()
            _primaryKey = New PrimaryKey(Id_Entidad, True, True)
            _primaryKey.Add_PK(Id_Grupo, True)
            _primaryKey.Add_PK(Id_TipoRelacion, True)

            _foreingKeys = New System.Collections.ObjectModel.Collection(Of ForeingKey)
            _foreingKeys.Add(New ForeingKey(Id_Entidad, New Entidad().Id_entidad, ForeingKey.DeleteRule.CASCADE))
            _foreingKeys.Add(New ForeingKey(Id_Grupo, New Grupos_Entidades().Id_Grupo, ForeingKey.DeleteRule.CASCADE))
            _foreingKeys.Add(New ForeingKey(Id_TipoRelacion, New TipoRelacion_Grupos().Id_TipoRelacion))

            _uniqueKeys = Nothing
        End Sub

        Private Sub SetFields()
            _fields = New FieldCollection
            _fields.Add(Id_Grupo)
            _fields.Add(Id_Entidad)
            _fields.Add(Id_TipoRelacion)

        End Sub

        Public Overrides ReadOnly Property TableName() As String
            Get
                Return _tableName
            End Get
        End Property

        Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As DBTable
            Dim theInstance As Entidades_Grupo
            If forScripting Then
                theInstance = NewForScripting()
            Else
                theInstance = New Entidades_Grupo
            End If
            Return theInstance
        End Function

        Shared Function NewForScripting() As Entidades_Grupo
            Dim temp As New Entidades_Grupo()
            temp.SetFields()
            temp.SetKeysForScripting()
            temp.Fields.SelectAll()
            Return temp
        End Function

    End Class
End Namespace