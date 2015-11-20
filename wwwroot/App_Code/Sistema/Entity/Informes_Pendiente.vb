Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler
Imports Orbelink.Entity.Entidades

Namespace Orbelink.Entity.Orbecatalog
    Public Class Informes_Pendiente : Inherits DBTable

        Dim _tableName As String = "OR_Informes_Pendiente"

        Public ReadOnly Id_Informe As New FieldInt("Id_Informe", Me, False, True)
        Public ReadOnly Id_Entidad As New FieldInt("Id_Entidad", Me)
        Public ReadOnly Id_Pendiente As New FieldInt("Id_Pendiente_TareaProyecto", Me)
        Public ReadOnly Comentario As New FieldString("Comentario", Me, Field.FieldType.VARCHAR_MULTILANG, 200, True)
        Public ReadOnly FechaInforme As New FieldDateTime("F_Informe", Me)
        Public ReadOnly Hilo As New FieldInt("Hilo", Me)

        Public Sub New()
            SetFields()
        End Sub

        <Obsolete("Esta metodo recibe una o mas fechas en UTC", False)> _
        Public Sub New(ByVal theId_Entidad As Integer, ByVal theId_Pendiente As Integer, ByVal theComentario As String, _
            ByVal theFechaInforme As Date, ByVal theHilo As Integer)
            Id_Entidad.Value = theId_Entidad
            Id_Pendiente.Value = theId_Pendiente
            Comentario.Value = theComentario
            FechaInforme.ValueUtc = theFechaInforme
            Hilo.Value = theHilo
            SetFields()
            _primaryKey = Nothing
            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Protected Overrides Sub SetKeysForScripting()
            _primaryKey = New PrimaryKey(Id_Informe, True, True)

            _foreingKeys = New System.Collections.ObjectModel.Collection(Of ForeingKey)
            _foreingKeys.Add(New ForeingKey(Id_Entidad, New Entidad().Id_entidad))
            _foreingKeys.Add(New ForeingKey(Id_Pendiente, New Pendiente().Id_Pendiente, ForeingKey.DeleteRule.CASCADE))

            _uniqueKeys = Nothing
        End Sub

        Private Sub SetFields()
            _fields = New FieldCollection
            _fields.Add(Id_Informe)
            _fields.Add(Id_Entidad)
            _fields.Add(Id_Pendiente)
            _fields.Add(Comentario)
            _fields.Add(FechaInforme)
            _fields.Add(Hilo)
        End Sub

        Public Overrides ReadOnly Property TableName() As String
            Get
                Return _tableName
            End Get
        End Property

        Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As DBTable
            Dim theInstance As Informes_Pendiente
            If forScripting Then
                theInstance = NewForScripting()
            Else
                theInstance = New Informes_Pendiente
            End If
            Return theInstance
        End Function

        Shared Function NewForScripting() As Informes_Pendiente
            Dim temp As New Informes_Pendiente()
            temp.SetFields()
            temp.SetKeysForScripting()
            temp.Fields.SelectAll()
            Return temp
        End Function

    End Class
End Namespace