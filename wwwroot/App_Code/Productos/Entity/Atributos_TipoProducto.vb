Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler

Namespace Orbelink.Entity.Productos
    Public Class Atributos_TipoProducto : Inherits DBTable

        Dim _tableName As String = "PR_Atributos_TipoProducto"

        Public ReadOnly Id_Atributo As New FieldInt("Id_Atributo", Me, Field.FieldType.INT)
        Public ReadOnly Id_TipoProducto As New FieldInt("Id_TipoProducto", Me, Field.FieldType.INT)
        Public ReadOnly orden As New FieldInt("orden", Me, Field.FieldType.INT, 0, True)
        Public ReadOnly buscable As New FieldTinyInt("buscable", Me, Field.FieldType.TINYINT)

        Public Sub New()
            SetFields()
        End Sub

        Public Sub New(ByVal theid_atributo As String, ByVal theId_TipoProducto As String, _
            ByVal theorden As String, ByVal thebuscable As String)
            Id_Atributo.Value = theid_atributo
            Id_TipoProducto.Value = theId_TipoProducto
            orden.Value = theorden
            buscable.Value = thebuscable
            SetFields()
            _primaryKey = Nothing
            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Protected Overrides Sub SetKeysForScripting()
            _primaryKey = New PrimaryKey(Id_Atributo, True, True)
            _primaryKey.Add_PK(Id_TipoProducto, True)

            _foreingKeys = New System.Collections.ObjectModel.Collection(Of ForeingKey)
            _foreingKeys.Add(New ForeingKey(Id_TipoProducto, New TipoProducto().Id_TipoProducto, ForeingKey.DeleteRule.CASCADE))
            _foreingKeys.Add(New ForeingKey(Id_Atributo, New Atributos_Para_Productos().Id_atributo, ForeingKey.DeleteRule.CASCADE))

            _uniqueKeys = Nothing
        End Sub

        Private Sub SetFields()
            _fields = New FieldCollection
            _fields.Add(Id_Atributo)
            _fields.Add(Id_TipoProducto)
            _fields.Add(orden)
            _fields.Add(buscable)

        End Sub

        Public Overrides ReadOnly Property TableName() As String
            Get
                Return _tableName
            End Get
        End Property

        Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As DBTable
            Dim theInstance As Atributos_TipoProducto
            If forScripting Then
                theInstance = NewForScripting()
            Else
                theInstance = New Atributos_TipoProducto
            End If
            Return theInstance
        End Function

        Shared Function NewForScripting() As Atributos_TipoProducto
            Dim temp As New Atributos_TipoProducto()
            temp.SetFields()
            temp.SetKeysForScripting()
            temp.Fields.SelectAll()
            Return temp
        End Function

    End Class
End Namespace