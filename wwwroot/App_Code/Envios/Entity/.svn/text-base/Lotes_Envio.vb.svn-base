Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler

Namespace Orbelink.Entity.Envios
    Public Class Lotes_Envio : Inherits DBTable

        Dim _tableName As String = "EV_Lotes_Envio"


        Public ReadOnly Id_Envio As New FieldInt("Id_Envio", Me)
        Public ReadOnly Lote As New FieldInt("Lote", Me)
        Public ReadOnly Fecha_Creado As New FieldDateTime("Fecha_Creado", Me)
        Public ReadOnly Fecha_Enviado As New FieldDateTime("Fecha_Enviado", Me, True)
        Public ReadOnly Enviado As New FieldInt("Enviado", Me)

        Public Sub New()
            SetFields()
        End Sub

        <Obsolete("Esta metodo recibe una o mas fechas en UTC", False)> _
        Public Sub New(ByVal theId_Envio As String, ByVal theLote As String, ByVal theFecha_Creado As String, _
            ByVal theFecha_Enviado As String, ByVal theEnviado As String)
            Id_Envio.Value = theId_Envio
            Lote.Value = theLote
            Fecha_Creado.ValueUtc = theFecha_Creado
            Fecha_Enviado.ValueUtc = theFecha_Enviado
            Enviado.Value = theEnviado
            SetFields()
            _primaryKey = Nothing
            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Protected Overrides Sub SetKeysForScripting()
            _primaryKey = New PrimaryKey(Id_Envio, True, True)
            _primaryKey.Add_PK(Lote, True)

            _foreingKeys = New System.Collections.ObjectModel.Collection(Of ForeingKey)
            _foreingKeys.Add(New ForeingKey(Id_Envio, New Envio().Id_Envio, ForeingKey.DeleteRule.CASCADE))

            _uniqueKeys = Nothing
        End Sub

        Private Sub SetFields()
            _fields = New FieldCollection
            _fields.Add(Id_Envio)
            _fields.Add(Lote)
            _fields.Add(Fecha_Creado)
            _fields.Add(Fecha_Enviado)
            _fields.Add(Enviado)

        End Sub


        Public Overrides ReadOnly Property TableName() As String
            Get
                Return _tableName
            End Get
        End Property

        Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As DBTable
            Dim theInstance As Lotes_Envio
            If forScripting Then
                theInstance = NewForScripting()
            Else
                theInstance = New Lotes_Envio
            End If
            Return theInstance
        End Function

        Shared Function NewForScripting() As Lotes_Envio
            Dim temp As New Lotes_Envio()
            temp.SetFields()
            temp.SetKeysForScripting()
            temp.Fields.SelectAll()
            Return temp
        End Function

    End Class
End Namespace