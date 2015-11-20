Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler

Namespace Orbelink.Entity.Archivos
    Public Class Archivo : Inherits DBTable

        Dim _tableName As String = "AR_Archivos"

        Public ReadOnly Id_Archivo As New FieldInt("Id_Archivo", Me, False, True)
        Public ReadOnly Nombre As New FieldString("n_Archivo", Me, Field.FieldType.VARCHAR_MULTILANG, 250)
        Public ReadOnly Comentario As New FieldString("Comentario", Me, Field.FieldType.VARCHAR_MULTILANG, 200, True)
        Public ReadOnly FileName As New FieldString("FileName", Me, Field.FieldType.VARCHAR, 250)
        Public ReadOnly Extension As New FieldString("Extension", Me, Field.FieldType.VARCHAR, 5)
        Public ReadOnly Size As New FieldInt("Size", Me)
        Public ReadOnly FileType As New FieldInt("FileType", Me)
        Public ReadOnly Fecha As New FieldDateTime("Fecha", Me)
        Public ReadOnly Id_ArchivoConfiguration As New FieldInt("Id_ArchivoConfiguration", Me)

        Public Sub New()
            SetFields()
        End Sub

        <Obsolete("Esta metodo recibe una o mas fechas en UTC", False)> _
        Public Sub New(ByVal theNombre As String, ByVal theComentario As String, _
            ByVal theFileName As String, ByVal theExtension As String, _
            ByVal theSize As Integer, ByVal theFileType As Integer, ByVal theFecha As Date, _
            ByVal theId_ArchivoConfiguration As Integer)
            Nombre.Value = theNombre
            Comentario.Value = theComentario
            FileName.Value = theFileName
            Extension.Value = theExtension
            Size.Value = theSize
            FileType.Value = theFileType
            Fecha.ValueUtc = theFecha
            Id_ArchivoConfiguration.Value = theId_ArchivoConfiguration
            SetFields()
            _primaryKey = Nothing
            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Protected Overrides Sub SetKeysForScripting()
            _primaryKey = New PrimaryKey(Id_Archivo, True, True)

            _foreingKeys = New System.Collections.ObjectModel.Collection(Of ForeingKey)
            _foreingKeys.Add(New ForeingKey(Id_ArchivoConfiguration, New Archivo_Configuration().Id_Configuration))
            _uniqueKeys = New System.Collections.ObjectModel.Collection(Of Orbelink.DBHandler.UniqueKey)
            Dim uniqueKey1 As New UniqueKey(FileName, True)
            uniqueKey1.Add_UK(Extension, True)
            _uniqueKeys.Add(uniqueKey1)
        End Sub

        Private Sub SetFields()
            _fields = New FieldCollection
            _fields.Add(Id_Archivo)
            _fields.Add(Nombre)
            _fields.Add(Comentario)
            _fields.Add(FileName)
            _fields.Add(Extension)
            _fields.Add(Size)
            _fields.Add(FileType)
            _fields.Add(Fecha)
            _fields.Add(Id_ArchivoConfiguration)
        End Sub

        Public Overrides ReadOnly Property TableName() As String
            Get
                Return _tableName
            End Get
        End Property

        Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As DBTable
            Dim theInstance As Archivo
            If forScripting Then
                theInstance = NewForScripting()
            Else
                theInstance = New Archivo
            End If
            Return theInstance
        End Function

        Shared Function NewForScripting() As Archivo
            Dim temp As New Archivo()
            temp.SetFields()
temp.SetKeysForScripting()
temp.Fields.SelectAll()
Return temp
        End Function
    End Class
End Namespace