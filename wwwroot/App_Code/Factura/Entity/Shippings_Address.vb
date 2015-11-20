
Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler
Imports Orbelink.Entity.Entidades
Namespace Orbelink.Entity.Facturas
    Public Class Shippings_Address : Inherits DBTable

        Dim _tableName As String = "FR_Shippings_Address"

        Public ReadOnly id_Shipping As New FieldInt("id_Shipping", Me, False, True)
        Public ReadOnly id_entidad As New FieldInt("id_entidad", Me)
        Public ReadOnly Actual As New FieldTinyInt("Actual", Me)
        Public ReadOnly Nombre As New FieldString("Nombre", Me, Field.FieldType.VARCHAR, 500, True)
        Public ReadOnly Apellido As New FieldString("Apellido", Me, Field.FieldType.VARCHAR, 500, True)
        Public ReadOnly Email As New FieldString("Email", Me, Field.FieldType.VARCHAR, 500, True)
        Public ReadOnly Telefono As New FieldString("Telefono", Me, Field.FieldType.VARCHAR, 50, True)
        Public ReadOnly Fax As New FieldString("Telefono_Alterno", Me, Field.FieldType.VARCHAR, 50, True)
        Public ReadOnly Codigo_Postal As New FieldString("Codigo_Postal", Me, Field.FieldType.VARCHAR, 500, True)
        Public ReadOnly Ciudad As New FieldString("Ciudad", Me, Field.FieldType.VARCHAR, 500, True)
        Public ReadOnly Region As New FieldString("Region", Me, Field.FieldType.VARCHAR, 500, True)
        Public ReadOnly Id_Pais As New FieldInt("Id_Pais", Me, True)
        Public ReadOnly Direccion As New FieldString("Direccion", Me, Field.FieldType.VARCHAR, 8000, True)
        Public ReadOnly Direccion_Alterna As New FieldString("Direccion_Alterna", Me, Field.FieldType.VARCHAR, 8000, True)
   

        Public Sub New()
            SetFields()
        End Sub

        Public Sub New(ByVal _Id_entidad As Integer, ByVal _Actual As String, ByVal _Nombre As String, ByVal _Apellido As String, ByVal _Email As String, ByVal _Telefono As String, ByVal _Fax As String, ByVal _Codigo_Postal As String, ByVal _Ciudad As String, ByVal _Region As String, ByVal _Id_Pais As String, ByVal _Direccion As String, ByVal _Direccion_Alterna As String)
            id_entidad.Value = _Id_entidad
            Actual.Value = _Actual
            Nombre.Value = _Nombre
            Apellido.Value = _Apellido
            Email.Value = _Email
            Telefono.Value = _Telefono
            Fax.Value = _Fax
            Codigo_Postal.Value = _Codigo_Postal
            Ciudad.Value = _Ciudad
            Region.Value = _Region
            Id_Pais.Value = _Id_Pais
            Direccion.Value = _Direccion
            Direccion_Alterna.Value = _Direccion_Alterna
            SetFields()
            _primaryKey = Nothing
            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Protected Overrides Sub SetKeysForScripting()
            _primaryKey = New Orbelink.DBHandler.PrimaryKey(id_Shipping, True, True)

            _foreingKeys = New System.Collections.ObjectModel.Collection(Of Orbelink.DBHandler.ForeingKey)
            _foreingKeys.Add(New Orbelink.DBHandler.ForeingKey(id_entidad, New Entidad().Id_entidad))
            _foreingKeys.Add(New Orbelink.DBHandler.ForeingKey(Id_Pais, New Shippings_Country().Id_Country))


            _uniqueKeys = Nothing
        End Sub

        Private Sub SetFields()
            _fields = New FieldCollection
            _fields.Add(id_Shipping)
            _fields.Add(id_entidad)
            _fields.Add(Actual)
            _fields.Add(Nombre)
            _fields.Add(Apellido)
            _fields.Add(Email)
            _fields.Add(Telefono)
            _fields.Add(Fax)
            _fields.Add(Codigo_Postal)
            _fields.Add(Ciudad)
            _fields.Add(Region)
            _fields.Add(Id_Pais)
            _fields.Add(Direccion)
            _fields.Add(Direccion_Alterna)
        End Sub

        Public Overrides ReadOnly Property TableName() As String
            Get
                Return _tableName
            End Get
        End Property

        Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As DBTable
            Dim theInstance As Shippings_Address
            If forScripting Then
                theInstance = NewForScripting()
            Else
                theInstance = New Shippings_Address
            End If
            Return theInstance
        End Function

        Shared Function NewForScripting() As Shippings_Address
            Dim temp As New Shippings_Address()
            temp.SetFields()
            temp.SetKeysForScripting()
            temp.Fields.SelectAll()
            Return temp
        End Function

    End Class
End Namespace