Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler
Imports Orbelink.Entity.Entidades
Imports Orbelink.Entity.Acciones

Namespace Orbelink.Entity.Referidos
    Public Class Accion_InvitacionAceptada
        Inherits DBTable

        Dim _tableName As String = "RF_Accion_InvitacionAceptada"

        Public ReadOnly Id_Invitacion As New FieldInt("Id_Invitacion", Me)
        Public ReadOnly Id_Accion As New FieldInt("Id_Accion", Me)

        Public Sub New()
            SetFields()
        End Sub

        '<Obsolete("Esta metodo recibe una o mas FechaAceptadas en UTC", False)> _
        Public Sub New(ByVal theId_Invitacion As Integer, ByVal theId_Accion As Integer)
            Id_Invitacion.Value = theId_Invitacion
            Id_Accion.Value = theId_Accion
            SetFields()
            _primaryKey = Nothing
            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Protected Overrides Sub SetKeysForScripting()
            _primaryKey = New PrimaryKey(Id_Invitacion, True, True)

            _foreingKeys = New System.Collections.ObjectModel.Collection(Of ForeingKey)
            _foreingKeys.Add(New ForeingKey(Id_Invitacion, New Invitacion().Id_Invitacion))
            _foreingKeys.Add(New ForeingKey(Id_Accion, New Accion().Id_Accion))

            _uniqueKeys = Nothing
        End Sub

        Private Sub SetFields()
            _fields = New FieldCollection
            _fields.Add(Id_Invitacion)
            _fields.Add(Id_Accion)
        End Sub

        Public Overrides ReadOnly Property TableName() As String
            Get
                Return _tableName
            End Get
        End Property

        Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As DBTable
            Dim theInstance As Accion_InvitacionAceptada
            If forScripting Then
                theInstance = NewForScripting()
            Else
                theInstance = New Accion_InvitacionAceptada
            End If
            Return theInstance
        End Function

        Shared Function NewForScripting() As Accion_InvitacionAceptada
            Dim temp As New Accion_InvitacionAceptada()
            temp.SetFields()
            temp.SetKeysForScripting()
            temp.Fields.SelectAll()
            Return temp
        End Function

    End Class
End Namespace