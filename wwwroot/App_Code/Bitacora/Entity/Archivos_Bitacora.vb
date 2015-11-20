Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler
Imports Orbelink.Entity.Archivos

Namespace Orbelink.Entity.Bitacora
    Public Class Archivo_Bitacora : Inherits DBTable

        Dim _tableName As String = "AR_Archivos_Bitacora"
        Public ReadOnly id_Archivo As New FieldInt("id_Archivo", Me)
        Public ReadOnly Id_Tema As New FieldInt("Id_Tema", Me)
        Public ReadOnly Numero_Bitacora As New FieldInt("Numero_Bitacora", Me)
        Public ReadOnly tipoPertenencia As New FieldInt("tipoPertenencia", Me)
        Public ReadOnly Principal As New FieldTinyInt("Principal", Me)

        Public Sub New()
            setFields()
        End Sub

        Public Sub New(ByVal theid_Archivo As Integer, ByVal theId_Tema As Integer, ByVal theNumero_Bitacora As Integer, ByVal thetipoPertenencia As Integer, ByVal thePrincipal As Integer)
            id_Archivo.Value = theid_Archivo
            Id_Tema.Value = theId_Tema
            Numero_Bitacora.Value = theNumero_Bitacora
            tipoPertenencia.Value = thetipoPertenencia
            Principal.Value = thePrincipal
            setFields()
            _primaryKey = Nothing
            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Protected Overrides Sub SetKeysForScripting()
            _primaryKey = New PrimaryKey(id_Archivo, True, True)
            _primaryKey.Add_PK(Id_Tema, True)
            _primaryKey.Add_PK(Numero_Bitacora, True)
            _primaryKey.Add_PK(tipoPertenencia, True)

            _foreingKeys = New System.Collections.ObjectModel.Collection(Of ForeingKey)
            _foreingKeys.Add(New ForeingKey(id_Archivo, New Archivo().Id_Archivo, ForeingKey.DeleteRule.CASCADE))

            Dim bitacora As New BitacoraDB()
            Dim llaveBitacora As New ForeingKey(Id_Tema, bitacora.Id_Tema)
            llaveBitacora.Add_FK(Numero_Bitacora, bitacora.Numero_Bitacora)

            _foreingKeys.Add(llaveBitacora)

            _uniqueKeys = Nothing
        End Sub

        Private Sub setFields()
            _fields = New FieldCollection
            _fields.Add(id_Archivo)
            _fields.Add(Id_Tema)
            _fields.Add(Numero_Bitacora)
            _fields.Add(Principal)
            _fields.Add(tipoPertenencia)
        End Sub

        Public Overrides ReadOnly Property TableName() As String
            Get
                Return _tableName
            End Get
        End Property

        Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As DBTable
            Dim theInstance As Archivo_Bitacora
            If forScripting Then
                theInstance = NewForScripting()
            Else
                theInstance = New Archivo_Bitacora
            End If
            Return theInstance
        End Function

        Shared Function NewForScripting() As Archivo_Bitacora
            Dim temp As New Archivo_Bitacora()
            temp.setFields()
            temp.SetKeysForScripting()
            temp.Fields.SelectAll()
            Return temp
        End Function
    End Class
End Namespace