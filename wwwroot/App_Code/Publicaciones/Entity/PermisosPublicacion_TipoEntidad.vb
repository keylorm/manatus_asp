Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler
Imports Orbelink.Entity.Entidades

Namespace Orbelink.Entity.Publicaciones
    Public Class Publicacion_TipoEntidad : Inherits DBTable

        Dim _tableName As String = "PU_Publicacion_TipoEntidad"

        Public ReadOnly id_TipoEntidad As New FieldInt("id_TipoEntidad", Me)
        Public ReadOnly id_Publicacion As New FieldInt("id_Publicacion", Me)
        Public ReadOnly Escribir As New FieldTinyInt("Escribir", Me)
        Public ReadOnly Leer As New FieldTinyInt("Leer", Me)
        Public ReadOnly Descargar As New FieldTinyInt("Descargar", Me)
        Public ReadOnly Subir As New FieldTinyInt("Subir", Me)

        Public Sub New()
            SetFields()
        End Sub

        Public Sub New(ByVal theid_TipoEntidad As String, ByVal theid_Publicacion As String, ByVal theEscribir As String, _
            ByVal theLeer As String, ByVal theDescargar As String, ByVal theSubir As String)
            id_TipoEntidad.Value = theid_TipoEntidad
            id_Publicacion.Value = theid_Publicacion
            Escribir.Value = theEscribir
            Leer.Value = theLeer
            Descargar.Value = theDescargar
            Subir.Value = theSubir
            SetFields()
            _primaryKey = Nothing
            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Protected Overrides Sub SetKeysForScripting()
            _primaryKey = New PrimaryKey(id_Publicacion, True, True)
            _primaryKey.Add_PK(id_TipoEntidad, True)

            _foreingKeys = New System.Collections.ObjectModel.Collection(Of ForeingKey)
            _foreingKeys.Add(New ForeingKey(id_Publicacion, New Publicacion().Id_Publicacion, ForeingKey.DeleteRule.CASCADE))
            _foreingKeys.Add(New ForeingKey(id_TipoEntidad, New TipoEntidad().Id_TipoEntidad))

            _uniqueKeys = Nothing
        End Sub

        Private Sub SetFields()
            _fields = New FieldCollection
            _fields.Add(id_TipoEntidad)
            _fields.Add(id_Publicacion)
            _fields.Add(Escribir)
            _fields.Add(Leer)
            _fields.Add(Descargar)
            _fields.Add(Subir)
        End Sub


        Public Overrides ReadOnly Property TableName() As String
            Get
                Return _tableName
            End Get
        End Property

        Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As DBTable
            Dim theInstance As Publicacion_TipoEntidad
            If forScripting Then
                theInstance = NewForScripting()
            Else
                theInstance = New Publicacion_TipoEntidad
            End If
            Return theInstance
        End Function

        Shared Function NewForScripting() As Publicacion_TipoEntidad
            Dim temp As New Publicacion_TipoEntidad()
            temp.SetFields()
            temp.SetKeysForScripting()
            temp.Fields.SelectAll()
            Return temp
        End Function

    End Class
End Namespace