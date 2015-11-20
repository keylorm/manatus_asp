Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler

''' <summary>
''' TipoRelacion_Entidad_Producto. Version 5.0
''' </summary>
''' <remarks></remarks>
Public Class TipoRelacion_Entidad_Producto : Inherits DBTable

    Dim _tableName As String = "RE_TipoRelacion_Entidad_Producto"
    

    Public ReadOnly Id_TipoRelacion As New FieldInt("Id_TipoRelacion", Me, False, True)
    Public ReadOnly Nombre_Entidad_Producto As New FieldString("n_TipoRelacion_En_PR", Me, Field.FieldType.VARCHAR_MULTILANG, 50)
    Public ReadOnly Nombre_Producto_Entidad As New FieldString("n_TipoRelacion_Pr_En", Me, Field.FieldType.VARCHAR_MULTILANG, 50)

    Public Sub New()
        SetFields()
    End Sub

    Public Sub New(ByVal theNombre_En_Pr As String, ByVal theNombre_Pr_En As String)
        Nombre_Entidad_Producto.Value = theNombre_En_Pr
        Nombre_Producto_Entidad.Value = theNombre_Pr_En
        SetFields()
        _primaryKey = Nothing
        _foreingKeys = Nothing
        _uniqueKeys = Nothing
    End Sub

    Protected Overrides Sub SetKeysForScripting()
        _primaryKey = New PrimaryKey(Id_TipoRelacion, True, True)
        _foreingKeys = Nothing
        _uniqueKeys = Nothing
    End Sub

    Private Sub SetFields()
        _fields = New FieldCollection
        _fields.Add(Id_TipoRelacion)
        _fields.Add(Nombre_Entidad_Producto)
        _fields.Add(Nombre_Producto_Entidad)

    End Sub

    Public Overrides ReadOnly Property TableName() As String
        Get
            Return _tableName
        End Get
    End Property

            Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As DBTable
            Dim theInstance As TipoRelacion_Entidad_Producto
            If forScripting Then
                theInstance = NewForScripting()
            Else
                theInstance = New TipoRelacion_Entidad_Producto
            End If
            Return theInstance
        End Function

        Shared Function NewForScripting() As TipoRelacion_Entidad_Producto
            Dim temp As New TipoRelacion_Entidad_Producto()
            temp.SetFields()
temp.SetKeysForScripting()
temp.Fields.SelectAll()
Return temp
        End Function

End Class
