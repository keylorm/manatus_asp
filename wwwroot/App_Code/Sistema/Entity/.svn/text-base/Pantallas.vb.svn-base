Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler
Imports Orbelink.Orbecatalog6

Namespace Orbelink.Entity.Orbecatalog
    Public Class Pantallas : Inherits DBTable

        Dim _tableName As String = "OR_Pantallas"

        Public ReadOnly Codigo_Pantalla As New FieldString("codigo_Pantalla", Me, Field.FieldType.VARCHAR, 10)
        Public ReadOnly ResourceClass As New FieldString("resourceClass", Me, Field.FieldType.VARCHAR, 100)
        Public ReadOnly ResourceKey As New FieldString("resourceKey", Me, Field.FieldType.VARCHAR, 100)
        Public ReadOnly Link As New FieldString("Link", Me, Field.FieldType.VARCHAR, 100)
        Public ReadOnly codigo_Padre As New FieldString("codigo_Padre", Me, Field.FieldType.VARCHAR, 50)
        Public ReadOnly activo As New FieldTinyInt("activo", Me)
        Public ReadOnly orden As New FieldInt("orden", Me)
        Public ReadOnly Visibilidad As New FieldTinyInt("Visibilidad", Me)

        Public Sub New()
            SetFields()
        End Sub

        Public Sub New(ByVal thecodigo_Pantalla As String, ByVal theResourceClass As String, ByVal theResourceKey As String, ByVal theLink As String, ByVal thecodigo_Padre As String, _
            ByVal theActivo As Integer, ByVal theOrden As Integer, ByVal theVisibilidad As Orbelink.Control.Security.SecurityHandler.VisibilidadPantalla)
            Codigo_Pantalla.Value = thecodigo_Pantalla
            ResourceClass.Value = theResourceClass
            ResourceKey.Value = theResourceKey
            Link.Value = theLink
            codigo_Padre.Value = thecodigo_Padre
            activo.Value = theActivo
            orden.Value = theOrden
            Visibilidad.Value = theVisibilidad
            SetFields()
            _primaryKey = Nothing
            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Protected Overrides Sub SetKeysForScripting()
            _primaryKey = New PrimaryKey(codigo_Pantalla, True, True)

            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Private Sub SetFields()
            _fields = New FieldCollection
            _fields.Add(codigo_Pantalla)
            _fields.Add(ResourceClass)
            _fields.Add(ResourceKey)
            _fields.Add(Link)
            _fields.Add(codigo_Padre)
            _fields.Add(activo)
            _fields.Add(orden)
            _fields.Add(Visibilidad)
        End Sub

        Public Overrides ReadOnly Property TableName() As String
            Get
                Return _tableName
            End Get
        End Property

        Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As DBTable
            Dim theInstance As Pantallas
            If forScripting Then
                theInstance = NewForScripting()
            Else
                theInstance = New Pantallas
            End If
            Return theInstance
        End Function

        Shared Function NewForScripting() As Pantallas
            Dim temp As New Pantallas()
            temp.SetFields()
            temp.SetKeysForScripting()
            temp.Fields.SelectAll()
            Return temp
        End Function

    End Class
End Namespace