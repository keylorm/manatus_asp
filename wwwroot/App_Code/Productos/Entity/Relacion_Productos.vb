Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler

Namespace Orbelink.Entity.Productos
    Public Class Relacion_Productos : Inherits DBTable

        Dim _tableName As String = "PR_Relacion_Productos"

        Public ReadOnly Id_Relacion As New FieldInt("id_Relacion", Me, False, True)
        Public ReadOnly Id_TipoRelacion As New FieldInt("id_TipoRelacion", Me)
        Public ReadOnly Id_ProductoBase As New FieldInt("Id_ProductoBase", Me)
        Public ReadOnly Id_ProductoDependiente As New FieldInt("Id_ProductoDependiente", Me)
        Public ReadOnly Comentarios As New FieldString("comentarios", Me, Field.FieldType.VARCHAR_MULTILANG, 200, True)

        Public Sub New()
            SetFields()
        End Sub

        Public Sub New(ByVal theId_TipoRelacion As String, ByVal theId_ProductoBase As String, ByVal theId_ProductoDependiente As String, _
            ByVal theComentarios As String)
            Id_TipoRelacion.Value = theId_TipoRelacion
            Id_ProductoBase.Value = theId_ProductoBase
            Id_ProductoDependiente.Value = theId_ProductoDependiente
            Comentarios.Value = theComentarios
            SetFields()
            _primaryKey = Nothing
            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Protected Overrides Sub SetKeysForScripting()
            _primaryKey = New PrimaryKey(Id_TipoRelacion, True, True)
            _primaryKey.Add_PK(Id_ProductoBase, True)
            _primaryKey.Add_PK(Id_ProductoDependiente, True)

            _foreingKeys = New System.Collections.ObjectModel.Collection(Of ForeingKey)
            _foreingKeys.Add(New ForeingKey(Id_ProductoBase, New Producto().Id_Producto))
            _foreingKeys.Add(New ForeingKey(Id_ProductoDependiente, New Producto().Id_Producto))
            _foreingKeys.Add(New ForeingKey(Id_TipoRelacion, New TipoRelacion_Productos().Id_TipoRelacion))
            _uniqueKeys = Nothing
        End Sub

        Private Sub SetFields()
            _fields = New FieldCollection
            _fields.Add(Id_Relacion)
            _fields.Add(Id_TipoRelacion)
            _fields.Add(Id_ProductoBase)
            _fields.Add(Id_ProductoDependiente)
            _fields.Add(Comentarios)
        End Sub

        Public Overrides ReadOnly Property TableName() As String
            Get
                Return _tableName
            End Get
        End Property

        Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As DBTable
            Dim theInstance As Relacion_Productos
            If forScripting Then
                theInstance = NewForScripting()
            Else
                theInstance = New Relacion_Productos
            End If
            Return theInstance
        End Function

        Shared Function NewForScripting() As Relacion_Productos
            Dim temp As New Relacion_Productos()
            temp.SetFields()
            temp.SetKeysForScripting()
            temp.Fields.SelectAll()
            Return temp
        End Function

    End Class
End Namespace