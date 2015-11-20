Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler

Namespace Orbelink.Entity.Security
    Public Class Usuario : Inherits DBTable

        Dim _tableName As String = "EN_Entidad"

        Public ReadOnly Id_Usuario As New FieldInt("id_entidad", Me, False, True)
        Public ReadOnly Id_Perfil As New FieldInt("Id_tipoEntidad", Me)
        Public ReadOnly FechaRegistro As New FieldDateTime("F_Entidad", Me)
        Public ReadOnly UserName As New FieldString("UserName", Me, Field.FieldType.VARCHAR, 50, True)
        Public ReadOnly PasswordHash As New FieldString("Password", Me, Field.FieldType.VARCHAR, 50, True)
        Public ReadOnly Email As New FieldString("Email", Me, Field.FieldType.VARCHAR, 50, True)
        Public ReadOnly Theme As New FieldString("theme", Me, Field.FieldType.VARCHAR, 50, True)

        Public Sub New()
            SetFields()
        End Sub

        '<Obsolete("Esta metodo recibe una o mas fechas en UTC", False)> _
        Public Sub New(ByVal theId_Perfil As Integer, ByVal theFechaRegistro As Date, ByVal theUserName As String, ByVal thePasswordHash As String, _
            ByVal theEmail As String, ByVal theTheme As String)
            Id_Perfil.Value = theId_Perfil
            FechaRegistro.ValueUtc = theFechaRegistro
            UserName.Value = theUserName
            PasswordHash.Value = thePasswordHash
            Email.Value = theEmail
            Theme.Value = theTheme
            SetFields()
            _primaryKey = Nothing
            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Protected Overrides Sub SetKeysForScripting()
            _primaryKey = New PrimaryKey(Id_Usuario, True, True)

            _foreingKeys = New System.Collections.ObjectModel.Collection(Of ForeingKey)
            _foreingKeys.Add(New ForeingKey(Id_Perfil, New Perfil().Id_Perfil))

            _uniqueKeys = New System.Collections.ObjectModel.Collection(Of Orbelink.DBHandler.UniqueKey)
            _uniqueKeys.Add(New Orbelink.DBHandler.UniqueKey(UserName, True))
            _uniqueKeys.Add(New Orbelink.DBHandler.UniqueKey(Email, True))
        End Sub

        Private Sub SetFields()
            _fields = New FieldCollection
            _fields.Add(Id_Usuario)
            _fields.Add(Id_Perfil)
            _fields.Add(FechaRegistro)
            _fields.Add(UserName)
            _fields.Add(PasswordHash)
            _fields.Add(Email)
            _fields.Add(Theme)
        End Sub

        Public Overrides ReadOnly Property TableName() As String
            Get
                Return _tableName
            End Get
        End Property

        Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As DBTable
            Dim theInstance As Usuario
            If forScripting Then
                theInstance = NewForScripting()
            Else
                theInstance = New Usuario
            End If
            Return theInstance
        End Function

        Shared Function NewForScripting() As Usuario
            Dim temp As New Usuario()
            temp.SetFields()
            temp.SetKeysForScripting()
            temp.Fields.SelectAll()
            Return temp
        End Function

    End Class
End Namespace