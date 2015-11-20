Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler

Namespace Orbelink.Entity.Orbecatalog
    Public Class Valores_Session : Inherits DBTable

        Dim _tableName As String = "OR_Valores_Session"
        Public ReadOnly Id_Entidad As New FieldInt("Id_Entidad", Me)
        Public ReadOnly Pantalla As New FieldString("Pantalla", Me, Field.FieldType.VARCHAR, 10)
        Public ReadOnly ControlID As New FieldString("ControlID", Me, Field.FieldType.VARCHAR, 50)
        Public ReadOnly Valor As New FieldString("Valor", Me, Field.FieldType.VARCHAR, 50)

        Public Sub New()
            SetFields()
        End Sub

        Public Sub New(ByVal theId_Entidad As String, ByVal thePantalla As String, ByVal theControlID As String, _
            ByVal theValor As String)
            Id_Entidad.Value = theId_Entidad
            Pantalla.Value = thePantalla
            ControlID.Value = theControlID
            Valor.Value = theValor
            SetFields()
            _primaryKey = Nothing
            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Protected Overrides Sub SetKeysForScripting()
            _primaryKey = New PrimaryKey(Id_Entidad, True, True)
            _primaryKey.Add_PK(Pantalla, True)
            _primaryKey.Add_PK(ControlID, True)

            _foreingKeys = New System.Collections.ObjectModel.Collection(Of ForeingKey)
            '_foreingKeys.Add(New ForeingKey(Id_Entidad, New Entidad().Id_entidad, ForeingKey.DeleteRule.CASCADE))
            _foreingKeys.Add(New ForeingKey(Pantalla, New Pantallas().codigo_Pantalla))

            _uniqueKeys = Nothing
        End Sub

        Private Sub SetFields()
            _fields = New FieldCollection
            _fields.Add(Id_Entidad)
            _fields.Add(Pantalla)
            _fields.Add(ControlID)
            _fields.Add(Valor)

        End Sub

        Public Overrides ReadOnly Property TableName() As String
            Get
                Return _tableName
            End Get
        End Property

        Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As DBTable
            Dim theInstance As Valores_Session
            If forScripting Then
                theInstance = NewForScripting()
            Else
                theInstance = New Valores_Session
            End If
            Return theInstance
        End Function

        Shared Function NewForScripting() As Valores_Session
            Dim temp As New Valores_Session()
            temp.SetFields()
            temp.SetKeysForScripting()
            temp.Fields.SelectAll()
            Return temp
        End Function

    End Class
End Namespace