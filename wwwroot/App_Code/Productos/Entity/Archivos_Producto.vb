Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler
Imports Orbelink.Entity.Archivos

Namespace Orbelink.Entity.Productos
    Public Class Archivo_Producto : Inherits DBTable

        Dim _tableName As String = "AR_Archivos_Producto"
        Public ReadOnly id_Archivo As New FieldInt("id_Archivo", Me)
        Public ReadOnly id_Producto As New FieldInt("id_Producto", Me)
        Public ReadOnly tipoPertenencia As New FieldInt("tipoPertenencia", Me)
        Public ReadOnly Principal As New FieldTinyInt("Principal", Me)

        Public Sub New()
            setFields()
        End Sub

        Public Sub New(ByVal theid_Archivo As String, ByVal theid_Producto As String, ByVal thetipoPertenencia As String, ByVal thePrincipal As Integer)
            id_Archivo.Value = theid_Archivo
            id_Producto.Value = theid_Producto
            tipoPertenencia.Value = thetipoPertenencia
            Principal.Value = thePrincipal
            setFields()
            _primaryKey = Nothing
            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Protected Overrides Sub SetKeysForScripting()
            _primaryKey = New PrimaryKey(id_Archivo, True, True)
            _primaryKey.Add_PK(id_Producto, True)
            _primaryKey.Add_PK(tipoPertenencia, True)

            _foreingKeys = New System.Collections.ObjectModel.Collection(Of ForeingKey)
            _foreingKeys.Add(New ForeingKey(id_Archivo, New Archivo().Id_Archivo, ForeingKey.DeleteRule.CASCADE))
            _foreingKeys.Add(New ForeingKey(id_Producto, New Producto().Id_Producto, ForeingKey.DeleteRule.CASCADE))

            _uniqueKeys = Nothing
        End Sub
        Private Sub setFields()
            _fields = New FieldCollection
            _fields.Add(id_Archivo)
            _fields.Add(id_Producto)
            _fields.Add(Principal)
            _fields.Add(tipoPertenencia)
        End Sub

        Public Overrides ReadOnly Property TableName() As String
            Get
                Return _tableName
            End Get
        End Property

        Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As DBTable
            Dim theInstance As Archivo_Producto
            If forScripting Then
                theInstance = NewForScripting()
            Else
                theInstance = New Archivo_Producto
            End If
            Return theInstance
        End Function

        Shared Function NewForScripting() As Archivo_Producto
            Dim temp As New Archivo_Producto()
            temp.setFields()
            temp.SetKeysForScripting()
            temp.Fields.SelectAll()
            Return temp
        End Function

    End Class
End Namespace