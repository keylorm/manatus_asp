Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler
Imports Orbelink.Entity.Entidades
Imports Orbelink.Entity.Currency

Namespace Orbelink.Entity.Productos
    Public Class Paquete : Inherits DBTable

        Dim _tableName As String = "PR_Paquete"

        Public ReadOnly Id_Paquete As New FieldInt("id_Paquete", Me, False, True)
        Public ReadOnly Nombre As New FieldString("n_Paquete", Me, Field.FieldType.VARCHAR_MULTILANG, 250)
        Public ReadOnly Descripcion As New FieldString("Desc_Paquete", Me, Field.FieldType.VARCHAR_MULTILANG, 8000, True)
        Public ReadOnly Precio As New FieldFloat("Precio", Me, True)
        Public ReadOnly Fecha As New FieldDateTime("F_Paquete", Me)
        Public ReadOnly Activo As New FieldTinyInt("Activo", Me)
        Public ReadOnly Id_Moneda As New FieldInt("Id_Moneda", Me)
        Public ReadOnly id_Entidad As New FieldInt("id_Entidad", Me)

        Public Sub New()
            SetFields()
        End Sub

        '<Obsolete("Esta metodo recibe una o mas fechas en UTC", False)> _
        Public Sub New(ByVal theNombre As String, ByVal theDescripcion As String, ByVal thePrecio As String, _
            ByVal theFecha As String, ByVal theActivo As String, _
            ByVal theid_Entidad As String, ByVal theid_Unidad As String)
            Nombre.Value = theNombre
            Descripcion.Value = theDescripcion
            Precio.Value = thePrecio
            Fecha.ValueUtc = theFecha
            Activo.Value = theActivo
            id_Entidad.Value = theid_Entidad
            Id_Moneda.Value = theid_Unidad
            SetFields()
            _primaryKey = Nothing
            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Protected Overrides Sub SetKeysForScripting()
            _primaryKey = New PrimaryKey(Id_Paquete, True, True)

            _foreingKeys = New System.Collections.ObjectModel.Collection(Of ForeingKey)
            _foreingKeys.Add(New ForeingKey(Id_Moneda, New Moneda().Id_Moneda))
            _foreingKeys.Add(New ForeingKey(id_Entidad, New Entidad().Id_entidad))

            _uniqueKeys = Nothing
        End Sub

        Private Sub SetFields()
            _fields = New FieldCollection
            _fields.Add(Id_Paquete)
            _fields.Add(Nombre)
            _fields.Add(Descripcion)
            _fields.Add(Precio)
            _fields.Add(Fecha)
            _fields.Add(Activo)
            _fields.Add(id_Entidad)
            _fields.Add(Id_Moneda)
        End Sub



        Public Overrides ReadOnly Property TableName() As String
            Get
                Return _tableName
            End Get
        End Property

        Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As DBTable
            Dim theInstance As Paquete
            If forScripting Then
                theInstance = NewForScripting()
            Else
                theInstance = New Paquete
            End If
            Return theInstance
        End Function

        Shared Function NewForScripting() As Paquete
            Dim temp As New Paquete()
            temp.SetFields()
            temp.SetKeysForScripting()
            temp.Fields.SelectAll()
            Return temp
        End Function

    End Class
End Namespace