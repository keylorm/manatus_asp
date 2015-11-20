Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler
Imports Orbelink.Entity.Entidades

Namespace Orbelink.Entity.EntidadesVieja
    Public Class EntidadVieja : Inherits DBTable

        Dim _tableName As String = "EN_Entidad"

        Public ReadOnly Id_entidad As New FieldInt("id_entidad", Me, False, True)
        Public ReadOnly Id_tipoEntidad As New FieldInt("Id_tipoEntidad", Me)
        Public ReadOnly Nombre As New FieldString("n_entidad", Me, Field.FieldType.VARCHAR, 150)
        Public ReadOnly Apellido As New FieldString("Apellido", Me, Field.FieldType.VARCHAR, 50, True)
        Public ReadOnly Apellido2 As New FieldString("Apellido2", Me, Field.FieldType.VARCHAR, 50, True)
        Public ReadOnly UserName As New FieldString("UserName", Me, Field.FieldType.VARCHAR, 50, True)
        Public ReadOnly Password As New FieldString("Password", Me, Field.FieldType.VARCHAR, 50, True)
        Public ReadOnly Telefono As New FieldString("Telefono", Me, Field.FieldType.VARCHAR, 50, True)
        Public ReadOnly Email As New FieldString("Email", Me, Field.FieldType.VARCHAR, 50, True)
        Public ReadOnly Website As New FieldString("website", Me, Field.FieldType.VARCHAR, 50, True)
        Public ReadOnly Id_Ubicacion As New FieldInt("id_Ubicacion", Me)
        Public ReadOnly Theme As New FieldString("theme", Me, Field.FieldType.VARCHAR, 50, True)
        Public ReadOnly Identificacion As New FieldString("Identificacion", Me, Field.FieldType.VARCHAR, 250, True)
        Public ReadOnly Descripcion As New FieldString("Descripcion", Me, Field.FieldType.VARCHAR_MULTILANG, 250, True)

        Public Sub New()
            SetFields()
        End Sub

        Public Sub New(ByVal theId_tipoEntidad As Integer, ByVal theNombre As String, ByVal theApellido As String, _
            ByVal theApellido2 As String, ByVal theUserName As String, ByVal thePassword As String, _
            ByVal theTelefono As String, ByVal theEmail As String, ByVal theWebsite As String, ByVal theID_Ubicacion As Integer, _
            ByVal theTheme As String, ByVal theIdentificacion As String, ByVal theDescripcion As String)
            Id_tipoEntidad.Value = theId_tipoEntidad
            Nombre.Value = theNombre
            Apellido.Value = theApellido
            Apellido2.Value = theApellido2
            UserName.Value = theUserName
            Password.Value = thePassword
            Telefono.Value = theTelefono
            Email.Value = theEmail
            Website.Value = theWebsite
            Id_Ubicacion.Value = theID_Ubicacion
            Theme.Value = theTheme
            Identificacion.Value = theIdentificacion
            Descripcion.Value = theDescripcion
            SetFields()
            _primaryKey = Nothing
            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Protected Overrides Sub SetKeysForScripting()
            _primaryKey = New PrimaryKey(Id_entidad, True, True)

            _foreingKeys = New System.Collections.ObjectModel.Collection(Of ForeingKey)
            _foreingKeys.Add(New ForeingKey(Id_tipoEntidad, New TipoEntidad().Id_TipoEntidad))
            _foreingKeys.Add(New ForeingKey(Id_Ubicacion, New Ubicacion().Id_ubicacion))

            _uniqueKeys = New System.Collections.ObjectModel.Collection(Of Orbelink.DBHandler.UniqueKey)
            _uniqueKeys.Add(New Orbelink.DBHandler.UniqueKey(UserName, True))
            _uniqueKeys.Add(New Orbelink.DBHandler.UniqueKey(Email, True))
        End Sub

        Private Sub SetFields()
            _fields = New FieldCollection
            _fields.Add(Id_entidad)
            _fields.Add(Id_tipoEntidad)
            _fields.Add(Nombre)
            _fields.Add(Apellido)
            _fields.Add(Apellido2)
            _fields.Add(UserName)
            _fields.Add(Password)
            _fields.Add(Telefono)
            _fields.Add(Email)
            _fields.Add(Website)
            _fields.Add(Id_Ubicacion)
            _fields.Add(Theme)
            _fields.Add(Identificacion)
            _fields.Add(Descripcion)
        End Sub

        Public Overrides ReadOnly Property TableName() As String
            Get
                Return _tableName
            End Get
        End Property

        Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As DBTable
            Dim theInstance As EntidadVieja
            If forScripting Then
                theInstance = NewForScripting()
            Else
                theInstance = New EntidadVieja
            End If
            Return theInstance
        End Function

        Shared Function NewForScripting() As EntidadVieja
            Dim temp As New EntidadVieja()
            temp.SetFields()
            temp.SetKeysForScripting()
            temp.Fields.SelectAll()
            Return temp
        End Function

    End Class
End Namespace