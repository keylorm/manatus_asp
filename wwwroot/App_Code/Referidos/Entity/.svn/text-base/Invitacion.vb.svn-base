Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler
Imports Orbelink.Entity.Entidades
Imports Orbelink.Entity.Acciones

Namespace Orbelink.Entity.Referidos
    Public Class Invitacion
        Inherits DBTable

        Dim _tableName As String = "RF_Invitacion"

        Public ReadOnly Id_Invitacion As New FieldInt("id_Invitacion", Me, False, True)
        Public ReadOnly Id_MachoteAccion As New FieldInt("Id_MachoteAccion", Me, True)
        Public ReadOnly CodigoInvitacion As New FieldString("CodigoInvitacion", Me, Field.FieldType.VARCHAR, 75)
        Public ReadOnly CodigoValidacion As New FieldString("CodigoValidacion", Me, Field.FieldType.VARCHAR, 75)
        Public ReadOnly FechaInvitacion As New FieldDateTime("FechaInvitacion", Me)
        Public ReadOnly UsuarioInvita As New FieldInt("UsuarioInvita", Me)
        Public ReadOnly CorreoInvitado As New FieldString("CorreoInvitado", Me, Field.FieldType.VARCHAR, 150)
        Public ReadOnly NombreInvitado As New FieldString("NombreInvitado", Me, Field.FieldType.VARCHAR, 150)
        Public ReadOnly Activo As New FieldInt("Activo", Me)

        Public Sub New()
            SetFields()
        End Sub

        '<Obsolete("Esta metodo recibe una o mas FechaInvitacions en UTC", False)> _
        Public Sub New(ByVal theId_MachoteAccion As Integer, ByVal theCodigoInvitacion As String, ByVal theCodigoValidacion As String, _
            ByVal theFechaInvitacion As Date, ByVal theUsuarioInvita As Integer, ByVal theCorreoInvitado As String, ByVal theNombreInvitado As String, _
            ByVal theActivo As Integer)
            Id_MachoteAccion.Value = theId_MachoteAccion
            CodigoInvitacion.Value = theCodigoInvitacion
            CodigoValidacion.Value = theCodigoValidacion
            FechaInvitacion.ValueUtc = theFechaInvitacion
            Activo.Value = theActivo
            CorreoInvitado.Value = theCorreoInvitado
            NombreInvitado.Value = theNombreInvitado
            UsuarioInvita.Value = theUsuarioInvita
            SetFields()
            _primaryKey = Nothing
            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Protected Overrides Sub SetKeysForScripting()
            _primaryKey = New PrimaryKey(Id_Invitacion, True, True)

            _foreingKeys = New System.Collections.ObjectModel.Collection(Of ForeingKey)
            _foreingKeys.Add(New ForeingKey(Id_MachoteAccion, New MachoteAccion().Id_MachoteAccion))
            _foreingKeys.Add(New ForeingKey(UsuarioInvita, New Entidad().Id_entidad))

            _uniqueKeys = Nothing
        End Sub

        Private Sub SetFields()
            _fields = New FieldCollection
            _fields.Add(Id_Invitacion)
            _fields.Add(Id_MachoteAccion)
            _fields.Add(CodigoInvitacion)
            _fields.Add(CodigoValidacion)
            _fields.Add(FechaInvitacion)
            _fields.Add(UsuarioInvita)
            _fields.Add(CorreoInvitado)
            _fields.Add(NombreInvitado)
            _fields.Add(Activo)
        End Sub

        Public Overrides ReadOnly Property TableName() As String
            Get
                Return _tableName
            End Get
        End Property

        Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As DBTable
            Dim theInstance As Invitacion
            If forScripting Then
                theInstance = NewForScripting()
            Else
                theInstance = New Invitacion
            End If
            Return theInstance
        End Function

        Shared Function NewForScripting() As Invitacion
            Dim temp As New Invitacion()
            temp.SetFields()
            temp.SetKeysForScripting()
            temp.Fields.SelectAll()
            Return temp
        End Function

    End Class
End Namespace