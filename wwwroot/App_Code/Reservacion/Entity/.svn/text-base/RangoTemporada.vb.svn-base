Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler
Imports Orbelink.Entity.Facturas

Namespace Orbelink.Entity.Reservaciones

    Public Class RangoTemporada : Inherits DBTable

        Dim _tableName As String = "Rs_RangoTemporada"

        Public ReadOnly Id_Temporada As New Orbelink.DBHandler.FieldInt("Id_Temporada", Me)
        Public ReadOnly Ordinal As New Orbelink.DBHandler.FieldInt("Ordinal", Me)
        Public ReadOnly fecha_inicio As New Orbelink.DBHandler.FieldDateTime("f_inicio", Me)
        Public ReadOnly fecha_final As New Orbelink.DBHandler.FieldDateTime("f_final", Me)

        <Obsolete("Esta metodo recibe una o mas fechas en UTC", False)> _
        Public Sub New(ByVal theId_Temporada As Integer, ByVal theOrdinal As Integer, ByVal thefechaInicio As Date, ByVal theFechaFinal As Date)
            Id_Temporada.Value = theId_Temporada
            Ordinal.Value = theOrdinal
            fecha_inicio.ValueUtc = thefechaInicio
            fecha_final.ValueUtc = theFechaFinal
            SetFields()
            _primaryKey = Nothing
            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Public Sub New()
            SetFields()
        End Sub

        Public Sub New(ByVal forScripting As Boolean)
            If forScripting Then
                SetFields()
                SetKeysForScripting()
            End If
        End Sub

        Private Sub SetFields()
            _fields = New FieldCollection
            _fields.Add(Id_Temporada)
            _fields.Add(Ordinal)
            _fields.Add(fecha_inicio)
            _fields.Add(fecha_final)
        End Sub

        Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As Orbelink.DBHandler.DBTable
            Dim theInstance As RangoTemporada
            If forScripting Then
                theInstance = New RangoTemporada(True)
            Else
                theInstance = New RangoTemporada
            End If
            Return theInstance
        End Function

        Protected Overrides Sub SetKeysForScripting()
            _primaryKey = New PrimaryKey(Id_Temporada, True, True)
            _primaryKey.Add_PK(Ordinal, True)

            _foreingKeys = New System.Collections.ObjectModel.Collection(Of ForeingKey)
            _foreingKeys.Add(New ForeingKey(Id_Temporada, New Temporada().Id_Temporada, ForeingKey.DeleteRule.CASCADE))
          
            _uniqueKeys = Nothing
        End Sub

        Public Overrides ReadOnly Property TableName() As String
            Get
                Return _tableName
            End Get
        End Property

        Shared Function NewForScripting() As RangoTemporada
            Dim temp As New RangoTemporada()
            temp.SetFields()
temp.SetKeysForScripting()
temp.Fields.SelectAll()
Return temp
        End Function
    End Class


End Namespace