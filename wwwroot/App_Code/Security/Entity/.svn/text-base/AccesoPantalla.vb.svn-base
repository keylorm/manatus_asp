Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler
Imports Orbelink.Entity.Orbecatalog

Namespace Orbelink.Entity.Security

    Public Class Pantalla_Perfil : Inherits DBTable

        Dim _tableName As String = "OR_Pantalla_TipoEntidad"

        Public ReadOnly Id_Perfil As New FieldInt("Id_TipoEntidad", Me)
        Public ReadOnly codigo_Pantalla As New FieldString("codigo_Pantalla", Me, Field.FieldType.VARCHAR, 10)
        Public ReadOnly Leer As New FieldTinyInt("Leer", Me)
        Public ReadOnly Crear As New FieldTinyInt("Crear", Me)
        Public ReadOnly Modificar As New FieldTinyInt("Modificar", Me)
        Public ReadOnly Borrar As New FieldTinyInt("Borrar", Me)

        Public Sub New()
            SetFields()
        End Sub

        Public Sub New(ByVal theId_Perfil As Integer, ByVal thecodigo_Pantalla As String, ByVal theLeer As Integer, _
            ByVal theCrear As Integer, ByVal theModificar As Integer, ByVal theBorrar As Integer)
            Id_Perfil.Value = theId_Perfil
            codigo_Pantalla.Value = thecodigo_Pantalla
            Leer.Value = theLeer
            Crear.Value = theCrear
            Modificar.Value = theModificar
            Borrar.Value = theBorrar
            SetFields()
            _primaryKey = Nothing
            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Protected Overrides Sub SetKeysForScripting()
            _primaryKey = New PrimaryKey(Id_Perfil, True, True)
            _primaryKey.Add_PK(codigo_Pantalla, True)

            _foreingKeys = New System.Collections.ObjectModel.Collection(Of ForeingKey)
            _foreingKeys.Add(New ForeingKey(Id_Perfil, New Perfil().Id_Perfil, ForeingKey.DeleteRule.CASCADE))
            _foreingKeys.Add(New ForeingKey(codigo_Pantalla, New Pantallas().Codigo_Pantalla, ForeingKey.DeleteRule.CASCADE))

            _uniqueKeys = Nothing
        End Sub

        Private Sub SetFields()
            _fields = New FieldCollection
            _fields.Add(Id_Perfil)
            _fields.Add(codigo_Pantalla)
            _fields.Add(Leer)
            _fields.Add(Crear)
            _fields.Add(Modificar)
            _fields.Add(Borrar)

        End Sub

        Public Overrides ReadOnly Property TableName() As String
            Get
                Return _tableName
            End Get
        End Property

        Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As DBTable
            Dim theInstance As Pantalla_Perfil
            If forScripting Then
                theInstance = NewForScripting()
            Else
                theInstance = New Pantalla_Perfil
            End If
            Return theInstance
        End Function

        Shared Function NewForScripting() As Pantalla_Perfil
            Dim temp As New Pantalla_Perfil()
            temp.SetFields()
            temp.SetKeysForScripting()
            temp.Fields.SelectAll()
            Return temp
        End Function

    End Class
End Namespace