Imports Microsoft.VisualBasic
Imports Orbelink.DBhandler
Imports Orbelink.Entity.Entidades

Namespace Orbelink.Entity.OrbeEvents
    Public Class Suceso : Inherits DBTable

        Dim _tableName As String = "OE_Suceso"

        Public ReadOnly Id_OrbeEvento As New Orbelink.DBHandler.FieldInt("Id_OrbeEvento", Me)
        Public ReadOnly Fecha As New Orbelink.DBHandler.FieldDateTime("F_Suceso", Me)
        Public ReadOnly TriggeredBy As New Orbelink.DBHandler.FieldInt("TriggeredBy", Me)
        Public ReadOnly SubReferencia As New Orbelink.DBHandler.FieldString("SubReferencia", Me, Field.FieldType.VARCHAR, 150, True)
        Public ReadOnly Revisado As New Orbelink.DBHandler.FieldInt("Revisado", Me)
        Public ReadOnly Comment As New Orbelink.DBHandler.FieldString("Comment", Me, Field.FieldType.VARCHAR, 250, True)

        Public Sub New()
            SetFields()
        End Sub

        '<Obsolete("Esta metodo recibe una o mas fechas en UTC", False)> _
        Public Sub New(ByVal theId_OrbeEvento As Integer, ByVal theFecha As Date, ByVal theTriggeredBy As Integer, _
            ByVal theSubReferencia As String, ByVal theRevisado As Integer, ByVal theComment As String)
            Id_OrbeEvento.Value = theId_OrbeEvento
            Fecha.ValueUtc = theFecha
            TriggeredBy.Value = theTriggeredBy
            SubReferencia.Value = theSubReferencia
            Revisado.Value = theRevisado
            Comment.Value = theComment
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
            _primaryKey.Add_PK(Fecha, True)

            _foreingKeys = New System.Collections.ObjectModel.Collection(Of ForeingKey)
            _foreingKeys.Add(New ForeingKey(Id_OrbeEvento, New OrbeEvento().Id_OrbeEvento, ForeingKey.DeleteRule.CASCADE))
            _foreingKeys.Add(New ForeingKey(TriggeredBy, New Entidad().Id_entidad, ForeingKey.DeleteRule.CASCADE))

            _uniqueKeys = Nothing
        End Sub

        Private Sub SetFields()
            _fields = New FieldCollection
            _fields.Add(Id_OrbeEvento)
            _fields.Add(Fecha)
            _fields.Add(TriggeredBy)
            _fields.Add(SubReferencia)
            _fields.Add(Revisado)
            _fields.Add(Comment)
        End Sub

        Public Overrides ReadOnly Property TableName() As String
            Get
                Return _tableName
            End Get
        End Property

        Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As Orbelink.DBHandler.DBTable
            Dim theInstance As Suceso
            If forScripting Then
                theInstance = New Suceso(True)
            Else
                theInstance = New Suceso
            End If
            Return theInstance
        End Function

        Shared Function NewForScripting() As Suceso
            Dim temp As New Suceso()
            temp.SetFields()
            temp.SetKeysForScripting()
            temp.Fields.SelectAll()
            Return temp
        End Function

    End Class
End Namespace