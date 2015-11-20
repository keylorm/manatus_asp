Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler

Namespace Orbelink.Entity.Envios
    Public Class Correos_Envio : Inherits DBTable

        Dim _tableName As String = "EV_Correos_Envio"


        Public ReadOnly Id_Correos_Envio As New FieldInt("id_Correos_Envio", Me, False, True)
        Public ReadOnly Id_Envio As New FieldInt("Id_Envio", Me)
        Public ReadOnly Lote As New FieldInt("Lote", Me)
        Public ReadOnly Correo As New FieldString("Correo", Me, Field.FieldType.VARCHAR, 50)
        Public ReadOnly Nombre As New FieldString("Nombre", Me, Field.FieldType.VARCHAR, 50)
        Public ReadOnly Status As New FieldString("Status", Me, Field.FieldType.VARCHAR, 50)
        Public ReadOnly Fecha As New FieldDateTime("Fecha", Me)
        Public ReadOnly HashValue As New FieldInt("HashValue", Me, False, False, "0")

        Public Sub New()
            SetFields()
        End Sub

        <Obsolete("Esta metodo recibe una o mas fechas en UTC", False)> _
        Public Sub New(ByVal theId_Envio As String, ByVal theLote As String, ByVal theCorreo As String, ByVal theNombre As String, _
            ByVal theStatus As String, ByVal theFecha As String, ByVal theHashValue As Integer)
            Id_Envio.Value = theId_Envio
            Lote.Value = theLote
            Correo.Value = theCorreo
            Nombre.Value = theNombre
            Status.Value = theStatus
            Fecha.ValueUtc = theFecha
            HashValue.Value = theHashValue

            SetFields()
            _primaryKey = Nothing
            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Protected Overrides Sub SetKeysForScripting()
            _primaryKey = New PrimaryKey(Id_Correos_Envio, True, True)

            _foreingKeys = New System.Collections.ObjectModel.Collection(Of ForeingKey)
            Dim lotes_Envio As New Lotes_Envio()
            Dim fk01 As New ForeingKey(Id_Envio, lotes_Envio.Id_Envio, ForeingKey.DeleteRule.CASCADE)
            fk01.Add_FK(Lote, lotes_Envio.Lote)
            _foreingKeys.Add(fk01)

            _uniqueKeys = Nothing
        End Sub

        Private Sub SetFields()
            _fields = New FieldCollection
            _fields.Add(Id_Correos_Envio)
            _fields.Add(Id_Envio)
            _fields.Add(Lote)
            _fields.Add(Correo)
            _fields.Add(Nombre)
            _fields.Add(Status)
            _fields.Add(Fecha)
            _fields.Add(HashValue)
        End Sub



        Public Overrides ReadOnly Property TableName() As String
            Get
                Return _tableName
            End Get
        End Property

        Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As DBTable
            Dim theInstance As Correos_Envio
            If forScripting Then
                theInstance = NewForScripting()
            Else
                theInstance = New Correos_Envio
            End If
            Return theInstance
        End Function

        Shared Function NewForScripting() As Correos_Envio
            Dim temp As New Correos_Envio()
            temp.SetFields()
            temp.SetKeysForScripting()
            temp.Fields.SelectAll()
            Return temp
        End Function

    End Class
End Namespace