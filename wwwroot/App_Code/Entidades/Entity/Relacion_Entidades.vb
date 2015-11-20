Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler

Namespace Orbelink.Entity.Entidades
    Public Class Relacion_Entidades : Inherits DBTable

        Dim _tableName As String = "EN_Relacion_Entidades"
        Public ReadOnly Id_Relacion As New FieldInt("id_Relacion", Me, False, True)
        Public ReadOnly Id_TipoRelacion As New FieldInt("id_TipoRelacion", Me)
        Public ReadOnly Id_EntidadBase As New FieldInt("Id_EntidadBase", Me)
        Public ReadOnly Id_EntidadDependiente As New FieldInt("Id_EntidadDependiente", Me)
        Public ReadOnly Comentarios As New FieldString("Comentarios", Me, Field.FieldType.VARCHAR_MULTILANG, 200, True)

        Public Sub New()
            SetFields()
        End Sub

        Public Sub New(ByVal theId_TipoRelacion As String, ByVal theId_EntidadBase As String, ByVal theId_EntidadDependiente As String, _
            ByVal theComentarios As String)
            Id_TipoRelacion.Value = theId_TipoRelacion
            Id_EntidadBase.Value = theId_EntidadBase
            Id_EntidadDependiente.Value = theId_EntidadDependiente
            Comentarios.Value = theComentarios
            SetFields()
            _primaryKey = Nothing
            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Protected Overrides Sub SetKeysForScripting()
            _primaryKey = New PrimaryKey(Id_TipoRelacion, True, True)
            _primaryKey.Add_PK(Id_EntidadBase, True)
            _primaryKey.Add_PK(Id_EntidadDependiente, True)

            _foreingKeys = New System.Collections.ObjectModel.Collection(Of ForeingKey)
            _foreingKeys.Add(New ForeingKey(Id_EntidadBase, New Entidad().Id_entidad, ForeingKey.DeleteRule.CASCADE))
            _foreingKeys.Add(New ForeingKey(Id_EntidadDependiente, New Entidad().Id_entidad))
            _foreingKeys.Add(New ForeingKey(Id_TipoRelacion, New TipoRelacion_Entidades().Id_TipoRelacion))
            _uniqueKeys = Nothing
        End Sub

        Private Sub SetFields()
            _fields = New FieldCollection
            _fields.Add(Id_Relacion)
            _fields.Add(Id_TipoRelacion)
            _fields.Add(Id_EntidadBase)
            _fields.Add(Id_EntidadDependiente)
            _fields.Add(Comentarios)
        End Sub

        Public Overrides ReadOnly Property TableName() As String
            Get
                Return _tableName
            End Get
        End Property

        Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As DBTable
            Dim theInstance As Relacion_Entidades
            If forScripting Then
                theInstance = NewForScripting()
            Else
                theInstance = New Relacion_Entidades
            End If
            Return theInstance
        End Function

        Shared Function NewForScripting() As Relacion_Entidades
            Dim temp As New Relacion_Entidades()
            temp.SetFields()
            temp.SetKeysForScripting()
            temp.Fields.SelectAll()
            Return temp
        End Function

    End Class
End Namespace