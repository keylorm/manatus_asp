Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler
Imports Orbelink.Entity.Entidades

Namespace Orbelink.Entity.Publicaciones
    Public Class Aprobacion_TipoPublicacion : Inherits DBTable

        Dim _tableName As String = "PU_Aprobacion_TipoPublicacion"

        Public ReadOnly Id_TipoPublicacion As New FieldInt("Id_TipoPublicacion", Me)
        Public ReadOnly Id_TipoRelacion As New FieldInt("Id_TipoRelacion", Me)
        Public ReadOnly Id_Grupo As New FieldInt("Id_Grupo", Me)

        Public Sub New()
            SetFields()
        End Sub

        Public Sub New(ByVal theId_TipoPublicacion As String, ByVal theId_TipoRelacion As Integer, _
            ByVal theId_Grupo As String)
            Id_TipoPublicacion.Value = theId_TipoPublicacion
            Id_TipoRelacion.Value = theId_TipoRelacion
            Id_Grupo.Value = theId_Grupo
            SetFields()
            _primaryKey = Nothing
            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Protected Overrides Sub SetKeysForScripting()
            _primaryKey = New PrimaryKey(Id_Grupo, True, True)
            _primaryKey.Add_PK(Id_TipoRelacion, True)
            _primaryKey.Add_PK(Id_TipoPublicacion, True)

            _foreingKeys = New System.Collections.ObjectModel.Collection(Of ForeingKey)
            _foreingKeys.Add(New ForeingKey(Id_TipoPublicacion, New TipoPublicacion().Id_TipoPublicacion))
            _foreingKeys.Add(New ForeingKey(Id_TipoRelacion, New TipoRelacion_Grupos().Id_TipoRelacion))
            _foreingKeys.Add(New ForeingKey(Id_Grupo, New Grupos_Entidades().Id_Grupo))

            _uniqueKeys = Nothing
        End Sub

        Private Sub SetFields()
            _fields = New FieldCollection
            _fields.Add(Id_TipoPublicacion)
            _fields.Add(Id_TipoRelacion)
            _fields.Add(Id_Grupo)

        End Sub


        Public Overrides ReadOnly Property TableName() As String
            Get
                Return _tableName
            End Get
        End Property

        Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As DBTable
            Dim theInstance As Aprobacion_TipoPublicacion
            If forScripting Then
                theInstance = NewForScripting()
            Else
                theInstance = New Aprobacion_TipoPublicacion
            End If
            Return theInstance
        End Function

        Shared Function NewForScripting() As Aprobacion_TipoPublicacion
            Dim temp As New Aprobacion_TipoPublicacion()
            temp.SetFields()
            temp.SetKeysForScripting()
            temp.Fields.SelectAll()
            Return temp
        End Function

    End Class
End Namespace