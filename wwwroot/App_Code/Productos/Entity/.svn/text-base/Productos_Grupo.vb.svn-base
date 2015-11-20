Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler

Namespace Orbelink.Entity.Productos
    Public Class Productos_Grupo : Inherits DBTable

        Dim _tableName As String = "PR_Productos_Grupo"


        Public ReadOnly Id_Grupo As New FieldInt("Id_Grupo", Me, False)
        Public ReadOnly Id_Producto As New FieldInt("Id_Producto", Me, False)
        Public ReadOnly Id_TipoRelacion As New FieldInt("Id_TipoRelacion", Me, False)

        Public Sub New()
            SetFields()
        End Sub

        Public Sub New(ByVal theId_Grupo As String, ByVal theId_Producto As String, ByVal theId_TipoRelacion As String)
            Id_Producto.Value = theId_Producto
            Id_TipoRelacion.Value = theId_TipoRelacion
            Id_Grupo.Value = theId_Grupo
            SetFields()
            _primaryKey = Nothing
            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Protected Overrides Sub SetKeysForScripting()
            _primaryKey = New PrimaryKey(Id_Producto, True, True)
            _primaryKey.Add_PK(Id_Grupo, True)
            _primaryKey.Add_PK(Id_TipoRelacion, True)

            _foreingKeys = New System.Collections.ObjectModel.Collection(Of ForeingKey)
            _foreingKeys.Add(New ForeingKey(Id_Producto, New Producto().Id_Producto, ForeingKey.DeleteRule.CASCADE))
            _foreingKeys.Add(New ForeingKey(Id_Grupo, New Grupos_Productos().Id_Grupo, ForeingKey.DeleteRule.CASCADE))
            _foreingKeys.Add(New ForeingKey(Id_TipoRelacion, New TipoRelacion_Grupos_Producto().Id_TipoRelacion))

            _uniqueKeys = Nothing
        End Sub

        Private Sub SetFields()
            _fields = New FieldCollection
            _fields.Add(Id_Grupo)
            _fields.Add(Id_Producto)
            _fields.Add(Id_TipoRelacion)

        End Sub


        Public Overrides ReadOnly Property TableName() As String
            Get
                Return _tableName
            End Get
        End Property

        Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As DBTable
            Dim theInstance As Productos_Grupo
            If forScripting Then
                theInstance = NewForScripting()
            Else
                theInstance = New Productos_Grupo
            End If
            Return theInstance
        End Function

        Shared Function NewForScripting() As Productos_Grupo
            Dim temp As New Productos_Grupo()
            temp.SetFields()
            temp.SetKeysForScripting()
            temp.Fields.SelectAll()
            Return temp
        End Function

    End Class
End Namespace