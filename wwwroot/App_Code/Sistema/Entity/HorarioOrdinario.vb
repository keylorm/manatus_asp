Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler

Namespace Orbelink.Entity.Orbecatalog
    Public Class HorarioOrdinario : Inherits DBTable

        Dim _tableName As String = "OR_HorarioOrdinario"

        Public ReadOnly Id_HorarioOrdinario As New FieldInt("id_HorarioOrdinario", Me, False, True)
        Public ReadOnly Dia As New FieldString("Dia", Me, Field.FieldType.VARCHAR, 50)
        Public ReadOnly HoraInicio As New FieldInt("HoraInicio", Me)
        Public ReadOnly HoraFinal As New FieldInt("HoraFinal", Me)

        Public Sub New()
            SetFields()
        End Sub

        Public Sub New(ByVal theDia As String, ByVal theHoraInicio As Integer, ByVal theHoraFinal As Integer)
            Dia.Value = theDia
            HoraInicio.Value = theHoraInicio
            HoraFinal.Value = theHoraFinal
            SetFields()
            _primaryKey = Nothing
            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Protected Overrides Sub SetKeysForScripting()
            _primaryKey = New PrimaryKey(Id_HorarioOrdinario, True, True)

            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Private Sub SetFields()
            _fields = New FieldCollection
            _fields.Add(Id_HorarioOrdinario)
            _fields.Add(Dia)
            _fields.Add(HoraInicio)
            _fields.Add(HoraFinal)
        End Sub

        Public Overrides ReadOnly Property TableName() As String
            Get
                Return _tableName
            End Get
        End Property

        Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As DBTable
            Dim theInstance As HorarioOrdinario
            If forScripting Then
                theInstance = NewForScripting()
            Else
                theInstance = New HorarioOrdinario
            End If
            Return theInstance
        End Function

        Shared Function NewForScripting() As HorarioOrdinario
            Dim temp As New HorarioOrdinario()
            temp.SetFields()
            temp.SetKeysForScripting()
            temp.Fields.SelectAll()
            Return temp
        End Function

    End Class
End Namespace