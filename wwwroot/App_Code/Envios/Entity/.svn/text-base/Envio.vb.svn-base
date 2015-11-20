Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler
Imports Orbelink.Entity.Entidades

Namespace Orbelink.Entity.Envios
    Public Class Envio : Inherits DBTable

        Dim _tableName As String = "EV_Envio"

        Public ReadOnly Id_Envio As New FieldInt("id_Envio", Me, False, True)
        Public ReadOnly Id_Entidad As New FieldInt("Id_Entidad", Me)
        Public ReadOnly FechaEnvio As New FieldDateTime("FechaEnvio", Me)
        Public ReadOnly ReplyTo As New FieldString("ReplyTo", Me, Field.FieldType.VARCHAR, 50)
        Public ReadOnly SenderName As New FieldString("SenderName", Me, Field.FieldType.VARCHAR_MULTILANG, 50)
        Public ReadOnly SenderEmail As New FieldString("SenderEmail", Me, Field.FieldType.VARCHAR, 50)
        Public ReadOnly Subject As New FieldString("Subject", Me, Field.FieldType.VARCHAR_MULTILANG, 50)
        Public ReadOnly UserName As New FieldString("UserName", Me, Field.FieldType.VARCHAR, 50, True)
        Public ReadOnly Password As New FieldString("Password", Me, Field.FieldType.VARCHAR, 50, True)
        Public ReadOnly SMTPServer As New FieldString("SMTPServer", Me, Field.FieldType.VARCHAR, 50, True)
        Public ReadOnly ContenidoURL As New FieldString("ContenidoURL", Me, Field.FieldType.VARCHAR, 200)
        Public ReadOnly Saludo As New FieldString("Saludo", Me, Field.FieldType.VARCHAR_MULTILANG, 250, True)
        Public ReadOnly TipoEnvio As New FieldInt("TipoEnvio", Me)

        Public Sub New()
            SetFields()
        End Sub

        '<Obsolete("Esta metodo recibe una o mas fechas en UTC", False)> _
        Public Sub New(ByVal theId_Entidad As Integer, ByVal theFechaEnvio As Date, ByVal theReplyTo As String, _
            ByVal theSenderName As String, ByVal theSenderEmail As String, ByVal theSubject As String, _
            ByVal theUserName As String, ByVal thePassword As String, ByVal theSMTPServer As String, _
            ByVal theContenidoURL As String, ByVal theSaludo As String, ByVal theTipoEnvio As String)
            Id_Entidad.Value = theId_Entidad
            FechaEnvio.ValueUtc = theFechaEnvio
            ReplyTo.Value = theReplyTo
            SenderName.Value = theSenderName
            SenderEmail.Value = theSenderEmail
            Subject.Value = theSubject
            UserName.Value = theUserName
            Password.Value = thePassword
            SMTPServer.Value = theSMTPServer
            ContenidoURL.Value = theContenidoURL
            Saludo.Value = theSaludo
            TipoEnvio.Value = theTipoEnvio

            SetFields()
            _primaryKey = Nothing
            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Protected Overrides Sub SetKeysForScripting()
            _primaryKey = New PrimaryKey(Id_Envio, True, True)

            _foreingKeys = New System.Collections.ObjectModel.Collection(Of ForeingKey)
            _foreingKeys.Add(New ForeingKey(Id_Entidad, New Entidad().Id_entidad))

            _uniqueKeys = Nothing
        End Sub

        Private Sub SetFields()
            _fields = New FieldCollection
            _fields.Add(Id_Envio)
            _fields.Add(Id_Entidad)
            _fields.Add(FechaEnvio)
            _fields.Add(ReplyTo)
            _fields.Add(SenderName)
            _fields.Add(SenderEmail)
            _fields.Add(Subject)
            _fields.Add(UserName)
            _fields.Add(Password)
            _fields.Add(SMTPServer)
            _fields.Add(ContenidoURL)
            _fields.Add(Saludo)
            _fields.Add(TipoEnvio)
        End Sub

        Public Overrides ReadOnly Property TableName() As String
            Get
                Return _tableName
            End Get
        End Property

        Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As DBTable
            Dim theInstance As Envio
            If forScripting Then
                theInstance = NewForScripting()
            Else
                theInstance = New Envio
            End If
            Return theInstance
        End Function

        Shared Function NewForScripting() As Envio
            Dim temp As New Envio()
            temp.SetFields()
            temp.SetKeysForScripting()
            temp.Fields.SelectAll()
            Return temp
        End Function

    End Class
End Namespace