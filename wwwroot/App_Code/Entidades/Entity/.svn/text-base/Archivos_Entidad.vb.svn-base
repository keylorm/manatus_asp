Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler
Imports Orbelink.Entity.Archivos

Namespace Orbelink.Entity.Entidades
    Public Class Archivo_Entidad : Inherits DBTable

        Dim _tableName As String = "EN_Archivos_Entidad"
        Public ReadOnly id_Archivo As New FieldInt("id_Archivo", Me)
        Public ReadOnly id_Entidad As New FieldInt("id_Entidad", Me)
        Public ReadOnly tipoPertenencia As New FieldInt("tipoPertenencia", Me)
        Public ReadOnly Principal As New FieldTinyInt("Principal", Me)

        Public Sub New()
            setFields()
        End Sub

        Public Sub New(ByVal theid_Archivo As String, ByVal theid_Entidad As String, ByVal thetipoPertenencia As String, ByVal thePrincipal As Integer)
            id_Archivo.Value = theid_Archivo
            id_Entidad.Value = theid_Entidad
            tipoPertenencia.Value = thetipoPertenencia
            Principal.Value = thePrincipal
            setFields()
            _primaryKey = Nothing
            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Protected Overrides Sub SetKeysForScripting()
            _primaryKey = New PrimaryKey(id_Archivo, True, True)
            _primaryKey.Add_PK(id_Entidad, True)
            _primaryKey.Add_PK(tipoPertenencia, True)


            _foreingKeys = New System.Collections.ObjectModel.Collection(Of ForeingKey)
            _foreingKeys.Add(New ForeingKey(id_Archivo, New Archivo().Id_Archivo, ForeingKey.DeleteRule.CASCADE))
            _foreingKeys.Add(New ForeingKey(id_Entidad, New Entidad().Id_entidad, ForeingKey.DeleteRule.CASCADE))

            _uniqueKeys = Nothing
        End Sub
        Private Sub setFields()
            _fields = New FieldCollection
            _fields.Add(id_Archivo)
            _fields.Add(id_Entidad)
            _fields.Add(Principal)
            _fields.Add(tipoPertenencia)
        End Sub

        Public Overrides ReadOnly Property TableName() As String
            Get
                Return _tableName
            End Get
        End Property

        Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As DBTable
            Dim theInstance As Archivo_Entidad
            If forScripting Then
                theInstance = NewForScripting()
            Else
                theInstance = New Archivo_Entidad
            End If
            Return theInstance
        End Function

        Shared Function NewForScripting() As Archivo_Entidad
            Dim temp As New Archivo_Entidad()
            temp.setFields()
            temp.SetKeysForScripting()
            temp.Fields.SelectAll()
            Return temp
        End Function
    End Class
End Namespace