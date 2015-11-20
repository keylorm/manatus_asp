Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler

Namespace Orbelink.Entity.Bitacora

    Public Class Tema : Inherits DBTable

        Dim _tableName As String = "BIT_Tema"

        Public ReadOnly Id_Tema As New FieldInt("id_Tema", Me, False, True)
        Public ReadOnly Nombre As New FieldString("n_Tema", Me, Field.FieldType.VARCHAR_MULTILANG, 150)
        Public ReadOnly FechaCreado As New FieldDateTime("FechaCreado", Me)

        Public Sub New()
            SetFields()
        End Sub

        <Obsolete("Esta metodo recibe una o mas fechas en UTC", False)> _
        Public Sub New(ByVal theNombre As String, ByVal theFechaCreado As Date)
            Nombre.Value = theNombre
            FechaCreado.ValueUtc = theFechaCreado

            SetFields()
            _primaryKey = Nothing
            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Protected Overrides Sub SetKeysForScripting()
            _primaryKey = New PrimaryKey(Id_Tema, True, True)

            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Private Sub SetFields()
            _fields = New FieldCollection
            _fields.Add(Id_Tema)
            _fields.Add(Nombre)
            _fields.Add(FechaCreado)
        End Sub

        Public Overrides ReadOnly Property TableName() As String
            Get
                Return _tableName
            End Get
        End Property

        Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As DBTable
            Dim theInstance As Tema
            If forScripting Then
                theInstance = NewForScripting()
            Else
                theInstance = New Tema
            End If
            Return theInstance
        End Function

        Shared Function NewForScripting() As Tema
            Dim temp As New Tema()
            temp.SetFields()
            temp.SetKeysForScripting()
            temp.Fields.SelectAll()
            Return temp
        End Function

    End Class

End Namespace