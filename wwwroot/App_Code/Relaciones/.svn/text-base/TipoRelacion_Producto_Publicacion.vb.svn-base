Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler

''' <summary>
''' TipoRelacion_Producto_Publicacion. Version 5.0
''' </summary>
''' <remarks></remarks>
Public Class TipoRelacion_Producto_Publicacion : Inherits DBTable

    Dim _tableName As String = "RE_TipoRelacion_Producto_Publicacion"
    

    Public ReadOnly Id_TipoRelacion As New FieldInt("Id_TipoRelacion", Me, False, True)
    Public ReadOnly Nombre_Publicacion_Producto As New FieldString("n_TipoRelacion_PU_PR", Me, Field.FieldType.VARCHAR_MULTILANG, 50)
    Public ReadOnly Nombre_Producto_Publicacion As New FieldString("n_TipoRelacion_Pr_PU", Me, Field.FieldType.VARCHAR_MULTILANG, 50)

    Public Sub New()
        SetFields()
    End Sub

    Public Sub New(ByVal theNombre_Pu_Pr As String, ByVal theNombre_Pr_Pu As String)
        Nombre_Publicacion_Producto.Value = theNombre_Pu_Pr
        Nombre_Producto_Publicacion.Value = theNombre_Pr_Pu
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
        _fields.Add(Nombre_Publicacion_Producto)
        _fields.Add(Nombre_Producto_Publicacion)

    End Sub


    Public Overrides ReadOnly Property TableName() As String
        Get
            Return _tableName
        End Get
    End Property

            Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As DBTable
            Dim theInstance As TipoRelacion_Producto_Publicacion
            If forScripting Then
                theInstance = NewForScripting()
            Else
                theInstance = New TipoRelacion_Producto_Publicacion
            End If
            Return theInstance
        End Function

        Shared Function NewForScripting() As TipoRelacion_Producto_Publicacion
            Dim temp As New TipoRelacion_Producto_Publicacion()
            temp.SetFields()
temp.SetKeysForScripting()
temp.Fields.SelectAll()
Return temp
        End Function

End Class
