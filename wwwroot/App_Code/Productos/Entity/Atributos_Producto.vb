Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler

Namespace Orbelink.Entity.Productos
    Public Class Atributos_Producto : Inherits DBTable

        Dim _tableName As String = "PR_Atributos_Producto"

        Public ReadOnly Id_Producto As New FieldInt("id_Producto", Me)
        Public ReadOnly Id_atributo As New FieldInt("id_atributo", Me)
        Public ReadOnly Valor As New FieldString("valor", Me, Field.FieldType.VARCHAR_MULTILANG, 200, True)
        Public ReadOnly Visible As New FieldTinyInt("visible", Me, False, False, 0)

        Public Sub New()
            SetFields()
        End Sub

        Public Sub New(ByVal theId_Producto As String, ByVal theId_atributo As String, _
            ByVal theValor As String, ByVal thevisible As String)
            Id_Producto.Value = theId_Producto
            Id_atributo.Value = theId_atributo
            Valor.Value = theValor
            Visible.Value = thevisible
            SetFields()
            _primaryKey = Nothing
            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Protected Overrides Sub SetKeysForScripting()
            _primaryKey = New PrimaryKey(Id_Producto, True, True)
            _primaryKey.Add_PK(Id_atributo, True)

            _foreingKeys = New System.Collections.ObjectModel.Collection(Of ForeingKey)
            _foreingKeys.Add(New ForeingKey(Id_Producto, New Producto().Id_Producto, ForeingKey.DeleteRule.CASCADE))
            _foreingKeys.Add(New ForeingKey(Id_atributo, New Atributos_Para_Productos().Id_atributo))

            _uniqueKeys = Nothing
        End Sub

        Private Sub SetFields()
            _fields = New FieldCollection
            _fields.Add(Id_Producto)
            _fields.Add(Id_atributo)
            _fields.Add(Visible)
            _fields.Add(Valor)
        End Sub

        Public Overrides ReadOnly Property TableName() As String
            Get
                Return _tableName
            End Get
        End Property

        Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As DBTable
            Dim theInstance As Atributos_Producto
            If forScripting Then
                theInstance = NewForScripting()
            Else
                theInstance = New Atributos_Producto
            End If
            Return theInstance
        End Function

        Shared Function NewForScripting() As Atributos_Producto
            Dim temp As New Atributos_Producto()
            temp.SetFields()
            temp.SetKeysForScripting()
            temp.Fields.SelectAll()
            Return temp
        End Function

    End Class
End Namespace