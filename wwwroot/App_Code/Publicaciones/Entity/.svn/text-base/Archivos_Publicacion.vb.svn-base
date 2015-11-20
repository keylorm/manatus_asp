Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler
Imports Orbelink.Entity.Archivos

Namespace Orbelink.Entity.Publicaciones
    Public Class Archivo_Publicacion : Inherits DBTable

        Dim _tableName As String = "AR_Archivos_Publicacion"
        Public ReadOnly id_Archivo As New FieldInt("id_Archivo", Me)
        Public ReadOnly id_Publicacion As New FieldInt("id_Publicacion", Me)
        Public ReadOnly tipoPertenencia As New FieldInt("tipoPertenencia", Me)
        Public ReadOnly Principal As New FieldTinyInt("Principal", Me)

        Public Sub New()
            setFields()
        End Sub

        Public Sub New(ByVal theid_Archivo As String, ByVal theid_Publicacion As String, ByVal thetipoPertenencia As String, ByVal thePrincipal As Integer)
            id_Archivo.Value = theid_Archivo
            id_Publicacion.Value = theid_Publicacion
            tipoPertenencia.Value = thetipoPertenencia
            Principal.Value = thePrincipal
            setFields()
            _primaryKey = Nothing
            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Protected Overrides Sub SetKeysForScripting()
            _primaryKey = New PrimaryKey(id_Archivo, True, True)
            _primaryKey.Add_PK(id_Publicacion, True)
            _primaryKey.Add_PK(tipoPertenencia, True)

            _foreingKeys = New System.Collections.ObjectModel.Collection(Of ForeingKey)
            _foreingKeys.Add(New ForeingKey(id_Archivo, New Archivo().Id_Archivo, ForeingKey.DeleteRule.CASCADE))
            _foreingKeys.Add(New ForeingKey(id_Publicacion, New Publicacion().Id_Publicacion))

            _uniqueKeys = Nothing
        End Sub
        Private Sub setFields()
            _fields = New FieldCollection
            _fields.Add(id_Archivo)
            _fields.Add(id_Publicacion)
            _fields.Add(Principal)
            _fields.Add(tipoPertenencia)
        End Sub

        Public Overrides ReadOnly Property TableName() As String
            Get
                Return _tableName
            End Get
        End Property

        Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As DBTable
            Dim theInstance As Archivo_Publicacion
            If forScripting Then
                theInstance = NewForScripting()
            Else
                theInstance = New Archivo_Publicacion
            End If
            Return theInstance
        End Function

        Shared Function NewForScripting() As Archivo_Publicacion
            Dim temp As New Archivo_Publicacion()
            temp.setFields()
            temp.SetKeysForScripting()
            temp.Fields.SelectAll()
            Return temp
        End Function
    End Class
End Namespace