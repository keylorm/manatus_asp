Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler

Namespace Orbelink.Entity.Orbecatalog
    Public Class HorarioExcepciones : Inherits DBTable

        Dim _tableName As String = "OR_HorarioExcepciones"

        Public ReadOnly Fecha As New FieldDateTime("Fecha", Me)
        
        Public Sub New()
            SetFields()
        End Sub

        '<Obsolete("Esta metodo recibe una o mas fechas en UTC", False)> _
        Public Sub New(ByVal theFecha As Date)
            Fecha.ValueUtc = theFecha
            SetFields()
            _primaryKey = Nothing
            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Protected Overrides Sub SetKeysForScripting()
            _primaryKey = New PrimaryKey(Fecha, True, True)
            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Private Sub SetFields()
            _fields = New FieldCollection
            _fields.Add(Fecha)
        End Sub

        Public Overrides ReadOnly Property TableName() As String
            Get
                Return _tableName
            End Get
        End Property

        Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As DBTable
            Dim theInstance As HorarioExcepciones
            If forScripting Then
                theInstance = NewForScripting()
            Else
                theInstance = New HorarioExcepciones
            End If
            Return theInstance
        End Function

        Shared Function NewForScripting() As HorarioExcepciones
            Dim temp As New HorarioExcepciones()
            temp.SetFields()
            temp.SetKeysForScripting()
            temp.Fields.SelectAll()
            Return temp
        End Function

    End Class
End Namespace