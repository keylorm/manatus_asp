Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler
Imports Orbelink.Entity.Entidades

Namespace Orbelink.Entity.Publicaciones
    Public Class Publicacion_Grupo : Inherits DBTable

        Dim _tableName As String = "PU_Publicacion_Grupo"


        Public ReadOnly Id_Publicacion As New FieldInt("Id_Publicacion", Me)
        Public ReadOnly Id_Grupo As New FieldInt("Id_Grupo", Me)
        Public ReadOnly Id_TipoRelacion As New FieldInt("Id_tipoRelacion", Me)
        Public ReadOnly Leer As New FieldTinyInt("Leer", Me)
        Public ReadOnly Escribir As New FieldTinyInt("Escribir", Me)
        Public ReadOnly Descargar As New FieldTinyInt("Descargar", Me)
        Public ReadOnly Subir As New FieldTinyInt("Subir", Me)

        Public Sub New()
            SetFields()
        End Sub

        Public Sub New(ByVal theId_Publicacion As String, ByVal theId_Grupo As String, ByVal theId_TipoRelacion As String, _
            ByVal theLeer As String, ByVal theEscribir As String, ByVal theDescargar As String, _
            ByVal theSubir As String)
            Id_Publicacion.Value = theId_Publicacion
            Id_Grupo.Value = theId_Grupo
            Id_TipoRelacion.Value = theId_TipoRelacion
            Leer.Value = theLeer
            Escribir.Value = theEscribir
            Descargar.Value = theDescargar
            Subir.Value = theSubir
            SetFields()
            _primaryKey = Nothing
            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Protected Overrides Sub SetKeysForScripting()
            _primaryKey = New PrimaryKey(Id_Publicacion, True, True)
            _primaryKey.Add_PK(Id_Grupo, True)
            _primaryKey.Add_PK(Id_TipoRelacion, True)

            _foreingKeys = New System.Collections.ObjectModel.Collection(Of ForeingKey)
            _foreingKeys.Add(New ForeingKey(Id_Publicacion, New Publicacion().Id_Publicacion, ForeingKey.DeleteRule.CASCADE))
            _foreingKeys.Add(New ForeingKey(Id_TipoRelacion, New TipoRelacion_Grupos().Id_TipoRelacion))
            _foreingKeys.Add(New ForeingKey(Id_Grupo, New Grupos_Entidades().Id_Grupo))

            _uniqueKeys = Nothing
        End Sub

        Private Sub SetFields()
            _fields = New FieldCollection
            _fields.Add(Id_Publicacion)
            _fields.Add(Id_Grupo)
            _fields.Add(Id_TipoRelacion)
            _fields.Add(Leer)
            _fields.Add(Escribir)
            _fields.Add(Descargar)
            _fields.Add(Subir)

        End Sub


        Public Overrides ReadOnly Property TableName() As String
            Get
                Return _tableName
            End Get
        End Property

        Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As DBTable
            Dim theInstance As Publicacion_Grupo
            If forScripting Then
                theInstance = NewForScripting()
            Else
                theInstance = New Publicacion_Grupo
            End If
            Return theInstance
        End Function

        Shared Function NewForScripting() As Publicacion_Grupo
            Dim temp As New Publicacion_Grupo()
            temp.SetFields()
            temp.SetKeysForScripting()
            temp.Fields.SelectAll()
            Return temp
        End Function

    End Class
End Namespace