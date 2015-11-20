Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler
Imports Orbelink.Entity.Entidades

Namespace Orbelink.Entity.Comentarios

    Public Class Comentario : Inherits Orbelink.DBHandler.DBTable

        Dim _tableName As String = "CMT_Comentario"

        Public ReadOnly Id_Comentario As New FieldInt("id_Comentario", Me, False, True)
        Public ReadOnly Id_Entidad As New FieldInt("Id_Entidad", Me)
        Public ReadOnly Titulo As New FieldString("Titulo", Me, False, 50)
        Public ReadOnly Comentario As New FieldText("Comentario", Me, False)
        Public ReadOnly Fecha As New FieldDateTime("Fecha", Me)
        Public ReadOnly Rating As New FieldInt("Rating", Me)
        Public ReadOnly Valido As New FieldTinyInt("Valido", Me)

        Public Sub New()
            SetFields()
        End Sub

        <Obsolete("Esta metodo recibe una o mas fechas en UTC", False)> _
        Public Sub New(ByVal theId_Entidad As Integer, ByVal theTitulo As String, _
            ByVal theComentario As String, ByVal theFecha As Date, ByVal theRating As Integer, _
            ByVal theValido As Integer)
            Id_Entidad.Value = theId_Entidad
            Titulo.Value = theTitulo
            Comentario.Value = theComentario
            Fecha.ValueUtc = theFecha
            Rating.Value = theRating
            Valido.Value = theValido
            SetFields()
            _primaryKey = Nothing
            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Protected Overrides Sub SetKeysForScripting()
            _primaryKey = New PrimaryKey(Id_Comentario, True, True)

            _foreingKeys = New System.Collections.ObjectModel.Collection(Of ForeingKey)
            _foreingKeys.Add(New ForeingKey(Id_Entidad, New Entidad().Id_entidad))

            _uniqueKeys = Nothing
        End Sub

        Private Sub SetFields()
            _fields = New Orbelink.DBHandler.FieldCollection
            _fields.Add(Id_Comentario)
            _fields.Add(Id_Entidad)
            _fields.Add(Titulo)
            _fields.Add(Comentario)
            _fields.Add(Fecha)
            _fields.Add(Rating)
            _fields.Add(Valido)
        End Sub

        Public Overrides ReadOnly Property TableName() As String
            Get
                Return _tableName
            End Get
        End Property

        Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As DBTable
            Dim theInstance As Comentario
            If forScripting Then
                theInstance = NewForScripting()
            Else
                theInstance = New Comentario
            End If
            Return theInstance
        End Function

        Shared Function NewForScripting() As Comentario
            Dim temp As New Comentario()
            temp.SetFields()
            temp.SetKeysForScripting()
            temp.Fields.SelectAll()
            Return temp
        End Function

    End Class

End Namespace