Imports Microsoft.VisualBasic
Imports Orbelink.DBhandler
Imports Orbelink.Entity.Entidades

Namespace Orbelink.Entity.OrbeEvents
    Public Class Suscripcion : Inherits DBTable

        Dim _tableName As String = "OE_Suscripcion"

        Public ReadOnly Id_OrbeEvento As New Orbelink.DBHandler.FieldInt("Id_OrbeEvento", Me)
        Public ReadOnly Id_Entidad As New Orbelink.DBHandler.FieldInt("Id_Entidad", Me)
        Public ReadOnly Periocidad As New Orbelink.DBHandler.FieldInt("Periocidad", Me)
        Public ReadOnly Fecha As New Orbelink.DBHandler.FieldDateTime("F_Suscripcion", Me)
        Public ReadOnly Email As New Orbelink.DBHandler.FieldTinyInt("Email", Me)
        Public ReadOnly Message As New Orbelink.DBHandler.FieldTinyInt("Message", Me)
        Public ReadOnly SMS As New Orbelink.DBHandler.FieldTinyInt("SMS", Me)

        Public Sub New()
            SetFields()
        End Sub

        <Obsolete("Esta metodo recibe una o mas fechas en UTC", False)> _
        Public Sub New(ByVal theId_OrbeEvento As Integer, ByVal theId_Entidad As Integer, ByVal thePeriocidad As Integer, _
            ByVal theFecha As Date, ByVal theEmail As Integer, ByVal theMessage As Integer, ByVal theSMS As Integer)
            Id_OrbeEvento.Value = theId_OrbeEvento
            Id_Entidad.Value = theId_Entidad
            Periocidad.Value = thePeriocidad
            Fecha.ValueUtc = theFecha
            Email.Value = theEmail
            Message.Value = theMessage
            SMS.Value = theSMS
            SetFields()
            _primaryKey = Nothing
            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Public Sub New(ByVal forScripting As Boolean)
            If forScripting Then
                SetFields()
                SetKeysForScripting()
            End If
        End Sub

        Protected Overrides Sub SetKeysForScripting()
            _primaryKey = New PrimaryKey(Id_OrbeEvento, True, True)
            _primaryKey.Add_PK(Id_Entidad, True)

            _foreingKeys = New System.Collections.ObjectModel.Collection(Of ForeingKey)
            _foreingKeys.Add(New ForeingKey(Id_OrbeEvento, New OrbeEvento().Id_OrbeEvento, ForeingKey.DeleteRule.CASCADE))
            _foreingKeys.Add(New ForeingKey(Id_Entidad, New Entidad().Id_entidad, ForeingKey.DeleteRule.CASCADE))

            _uniqueKeys = Nothing
        End Sub

        Private Sub SetFields()
            _fields = New FieldCollection
            _fields.Add(Id_OrbeEvento)
            _fields.Add(Id_Entidad)
            _fields.Add(Periocidad)
            _fields.Add(Fecha)
            _fields.Add(Email)
            _fields.Add(Message)
            _fields.Add(SMS)
        End Sub

        Public Overrides ReadOnly Property TableName() As String
            Get
                Return _tableName
            End Get
        End Property

        Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As Orbelink.DBHandler.DBTable
            Dim theInstance As Suscripcion
            If forScripting Then
                theInstance = New Suscripcion(True)
            Else
                theInstance = New Suscripcion
            End If
            Return theInstance
        End Function

        Shared Function NewForScripting() As Suscripcion
            Dim temp As New Suscripcion()
            temp.SetFields()
            temp.SetKeysForScripting()
            temp.Fields.SelectAll()
            Return temp
        End Function
    End Class
End Namespace