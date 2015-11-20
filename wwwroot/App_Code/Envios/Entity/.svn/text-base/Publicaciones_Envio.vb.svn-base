Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler
Imports Orbelink.Entity.Publicaciones

Namespace Orbelink.Entity.Envios

    Public Class Publicaciones_Envio : Inherits DBTable

        Dim _tableName As String = "EV_Publicaciones_Envio"

        Public ReadOnly Id_Envio As New FieldInt("Id_Envio", Me)
        Public ReadOnly Id_Publicacion As New FieldInt("Id_Publicacion", Me)
        Public ReadOnly Orden As New FieldString("Orden", Me, Field.FieldType.VARCHAR, 50)

        Public Sub New()
            SetFields()
        End Sub

        Public Sub New(ByVal theId_Envio As String, ByVal theId_Publicacion As String, ByVal theOrden As String)
            Id_Envio.Value = theId_Envio
            Id_Publicacion.Value = theId_Publicacion
            Orden.Value = theOrden
            SetFields()
            _primaryKey = Nothing
            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Protected Overrides Sub SetKeysForScripting()
            _primaryKey = New PrimaryKey(Id_Envio, True, True)
            _primaryKey.Add_PK(Id_Publicacion, True)

            _foreingKeys = New System.Collections.ObjectModel.Collection(Of ForeingKey)
            _foreingKeys.Add(New ForeingKey(Id_Envio, New Envio().Id_Envio, ForeingKey.DeleteRule.CASCADE))
            _foreingKeys.Add(New ForeingKey(Id_Publicacion, New Publicacion().Id_Publicacion, ForeingKey.DeleteRule.CASCADE))

            _uniqueKeys = Nothing
        End Sub

        Private Sub SetFields()
            _fields = New FieldCollection
            _fields.Add(Id_Envio)
            _fields.Add(Id_Publicacion)
            _fields.Add(Orden)
        End Sub

        Public Overrides ReadOnly Property TableName() As String
            Get
                Return _tableName
            End Get
        End Property

        Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As DBTable
            Dim theInstance As Publicaciones_Envio
            If forScripting Then
                theInstance = NewForScripting()
            Else
                theInstance = New Publicaciones_Envio
            End If
            Return theInstance
        End Function

        Shared Function NewForScripting() As Publicaciones_Envio
            Dim temp As New Publicaciones_Envio()
            temp.SetFields()
            temp.SetKeysForScripting()
            temp.Fields.SelectAll()
            Return temp
        End Function

    End Class

End Namespace