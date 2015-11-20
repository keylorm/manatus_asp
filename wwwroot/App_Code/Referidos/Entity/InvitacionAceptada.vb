Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler
Imports Orbelink.Entity.Entidades

Namespace Orbelink.Entity.Referidos
    Public Class InvitacionAceptada
        Inherits DBTable

        Dim _tableName As String = "RF_InvitacionAceptada"

        Public ReadOnly Id_Invitacion As New FieldInt("Id_Invitacion", Me)
        Public ReadOnly FechaAceptada As New FieldDateTime("FechaAceptada", Me)
        Public ReadOnly UsuarioInvitado As New FieldInt("UsuarioInvitado", Me)

        Public Sub New()
            SetFields()
        End Sub

        '<Obsolete("Esta metodo recibe una o mas FechaAceptadas en UTC", False)> _
        Public Sub New(ByVal theId_Invitacion As Integer, ByVal theUsuarioInvitado As Integer, ByVal theFechaAceptada As Date)
            Id_Invitacion.Value = theId_Invitacion
            FechaAceptada.ValueUtc = theFechaAceptada
            UsuarioInvitado.Value = theUsuarioInvitado
            SetFields()
            _primaryKey = Nothing
            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Protected Overrides Sub SetKeysForScripting()
            _primaryKey = New PrimaryKey(Id_Invitacion, True, True)

            _foreingKeys = New System.Collections.ObjectModel.Collection(Of ForeingKey)
            _foreingKeys.Add(New ForeingKey(Id_Invitacion, New Invitacion().Id_Invitacion))
            _foreingKeys.Add(New ForeingKey(UsuarioInvitado, New Entidad().Id_entidad))

            _uniqueKeys = Nothing
        End Sub

        Private Sub SetFields()
            _fields = New FieldCollection
            _fields.Add(Id_Invitacion)
            _fields.Add(UsuarioInvitado)
            _fields.Add(FechaAceptada)
        End Sub

        Public Overrides ReadOnly Property TableName() As String
            Get
                Return _tableName
            End Get
        End Property

        Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As DBTable
            Dim theInstance As InvitacionAceptada
            If forScripting Then
                theInstance = NewForScripting()
            Else
                theInstance = New InvitacionAceptada
            End If
            Return theInstance
        End Function

        Shared Function NewForScripting() As InvitacionAceptada
            Dim temp As New InvitacionAceptada()
            temp.SetFields()
            temp.SetKeysForScripting()
            temp.Fields.SelectAll()
            Return temp
        End Function

    End Class
End Namespace