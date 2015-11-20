Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler
Imports Orbelink.Control.HistorialEstados

Namespace Orbelink.Entity.Reservaciones

    Public Class Temporada : Inherits DBTable

        Dim _tableName As String = "RS_Temporada"

        Private ReadOnly _Id_Temporada As New FieldInt("Id_Temporada", Me, False, True)
        Private ReadOnly _Nombre As New FieldString("n_Temporada", Me, Field.FieldType.VARCHAR_MULTILANG, 150)
        Private ReadOnly _Prioridad As New Orbelink.DBHandler.FieldInt("Prioridad", Me)

        Public Sub New()
            SetFields()
        End Sub

        Public Sub New(ByVal the_Nombre As String, ByVal thePrioridad As Integer)
            _Nombre.Value = the_Nombre
            _Prioridad.Value = thePrioridad

            SetFields()
            _primaryKey = Nothing
            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Protected Overrides Sub SetKeysForScripting()
            _primaryKey = New PrimaryKey(_Id_Temporada, True, True)

            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Private Sub SetFields()
            _fields = New FieldCollection
            _fields.Add(_Id_Temporada)
            _fields.Add(_Nombre)
            _fields.Add(_Prioridad)
        End Sub

        Public Overrides ReadOnly Property TableName() As String
            Get
                Return _tableName
            End Get
        End Property

        Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As DBTable
            Dim theInstance As Temporada
            If forScripting Then
                theInstance = NewForScripting()
            Else
                theInstance = New Temporada
            End If
            Return theInstance
        End Function

        Shared Function NewForScripting() As Temporada
            Dim temp As New Temporada()
            temp.SetFields()
        temp.SetKeysForScripting()
        temp.Fields.SelectAll()
        Return temp
        End Function

        Public ReadOnly Property Id_Temporada() As DBHandler.FieldInt
            Get
                Return _Id_Temporada
            End Get
        End Property

        Public ReadOnly Property Nombre() As DBHandler.FieldString
            Get
                Return _Nombre
            End Get
        End Property

        Public ReadOnly Property Prioridad() As DBHandler.FieldInt
            Get
                Return _Prioridad
            End Get
        End Property
    End Class

End Namespace