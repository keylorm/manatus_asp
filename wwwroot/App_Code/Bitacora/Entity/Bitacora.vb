Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler
Imports Orbelink.Entity.Entidades

Namespace Orbelink.Entity.Bitacora

    Public Class BitacoraDB : Inherits DBTable

        Dim _tableName As String = "BIT_Bitacora"

        Public ReadOnly Id_Tema As New FieldInt("Id_Tema", Me)
        Public ReadOnly Numero_Bitacora As New FieldInt("Numero_Bitacora", Me)
        Public ReadOnly Fecha As New FieldDateTime("F_Bitacora", Me)
        Public ReadOnly Comentario As New FieldString("Comentario", Me, Field.FieldType.VARCHAR_MULTILANG, 200)
        Public ReadOnly EscritoPor As New FieldInt("EscritoPor", Me)
        Public ReadOnly Id_tipocomunicacion As New FieldInt("Id_tipocomunicacion", Me)

        Public Sub New()
            SetFields()
        End Sub

        <Obsolete("Esta metodo recibe una o mas fechas en UTC", False)> _
        Public Sub New(ByVal theId_Tema As Integer, ByVal theNumero_Bitacora As Integer, ByVal theFecha As Date, ByVal theComentario As String, _
            ByVal theEscritoPor As Integer, ByVal theId_tipocomunicacion As Integer)
            Id_Tema.Value = theId_Tema
            Numero_Bitacora.Value = theNumero_Bitacora
            Fecha.ValueUtc = theFecha
            Comentario.Value = theComentario
            EscritoPor.Value = theEscritoPor
            Id_tipocomunicacion.Value = theId_tipocomunicacion
            SetFields()
            _primaryKey = Nothing
            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Protected Overrides Sub SetKeysForScripting()
            _primaryKey = New PrimaryKey(Id_Tema, True, True)
            _primaryKey.Add_PK(Numero_Bitacora, True)

            _foreingKeys = New System.Collections.ObjectModel.Collection(Of ForeingKey)
            _foreingKeys.Add(New ForeingKey(EscritoPor, New Entidad().Id_entidad))
            _foreingKeys.Add(New ForeingKey(Id_tipocomunicacion, New TipoComunicacion().Id_TipoComunicacion))
            _foreingKeys.Add(New ForeingKey(Id_Tema, New Tema().Id_Tema))
            _uniqueKeys = Nothing
        End Sub

        Private Sub SetFields()
            _fields = New FieldCollection
            _fields.Add(Id_Tema)
            _fields.Add(Numero_Bitacora)
            _fields.Add(Fecha)
            _fields.Add(Comentario)
            _fields.Add(EscritoPor)
            _fields.Add(Id_tipocomunicacion)
        End Sub

        Public Overrides ReadOnly Property TableName() As String
            Get
                Return _tableName
            End Get
        End Property

        Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As DBTable
            Dim theInstance As BitacoraDB
            If forScripting Then
                theInstance = NewForScripting()
            Else
                theInstance = New BitacoraDB
            End If
            Return theInstance
        End Function

        Shared Function NewForScripting() As BitacoraDB
            Dim temp As New BitacoraDB()
            temp.SetFields()
            temp.SetKeysForScripting()
            temp.Fields.SelectAll()
            Return temp
        End Function
    End Class

End Namespace