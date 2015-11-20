Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler
Imports Orbelink.Entity.Entidades

Namespace Orbelink.Entity.Publicaciones
    Public Class Publicacion : Inherits DBTable

        Dim _tableName As String = "PU_Publicacion"

        Public ReadOnly Id_Publicacion As New FieldInt("id_Publicacion", Me, False, True)
        Public ReadOnly Id_tipoPublicacion As New FieldInt("Id_tipoPublicacion", Me)
        Public ReadOnly IncluirRSS As New FieldTinyInt("incluirRSS", Me)
        Public ReadOnly Titulo As New FieldString("titulo", Me, Field.FieldType.VARCHAR_MULTILANG, 300)
        Public ReadOnly Corta As New FieldString("corta", Me, Field.FieldType.VARCHAR_MULTILANG, 3000, True)
        Public ReadOnly Texto As New FieldText("Texto", Me, True, True)
        Public ReadOnly Link As New FieldString("Link", Me, Field.FieldType.VARCHAR, 50, True)
        Public ReadOnly Visible As New FieldTinyInt("Visible", Me)
        Public ReadOnly Aprobada As New FieldTinyInt("Aprobada", Me)
        Public ReadOnly Fecha As New FieldDateTime("Fecha", Me)
        Public ReadOnly FechaInicio As New FieldDateTime("FechaInicio", Me, True)
        Public ReadOnly FechaFin As New FieldDateTime("FechaFin", Me, True)
        Public ReadOnly id_Entidad As New FieldInt("id_Entidad", Me)
        Public ReadOnly Id_Categoria As New FieldInt("id_Categoria", Me)
        Public ReadOnly Id_Estado_Publicacion As New FieldInt("Id_Estado_Publicacion", Me)

        Public Sub New()
            SetFields()
        End Sub

        '<Obsolete("Esta metodo recibe una o mas fechas en UTC", False)> _
        Public Sub New(ByVal theId_tipoPublicacion As Integer, ByVal theincluirRSS As Integer, ByVal theTitulo As String, ByVal theCorta As String, ByVal theTexto As String, _
            ByVal theLink As String, ByVal theVisible As Integer, ByVal theFecha As Date, ByVal theFechaInicio As Date, ByVal theFechaFin As Date, ByVal theid_Entidad As Integer, _
            ByVal theID_Categoria As Integer, ByVal theAprobada As Integer, ByVal theId_Estado_Publicacion As Integer)
            Id_tipoPublicacion.Value = theId_tipoPublicacion
            IncluirRSS.Value = theincluirRSS
            Titulo.Value = theTitulo
            Texto.Value = theTexto
            Corta.Value = theCorta
            Link.Value = theLink
            Visible.Value = theVisible
            Aprobada.Value = theAprobada
            Fecha.ValueUtc = theFecha
            FechaInicio.ValueUtc = theFechaInicio
            FechaFin.ValueUtc = theFechaFin
            id_Entidad.Value = theid_Entidad
            Id_Categoria.Value = theID_Categoria
            Id_Estado_Publicacion.Value = theId_Estado_Publicacion
            SetFields()
            _primaryKey = Nothing
            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Protected Overrides Sub SetKeysForScripting()
            _primaryKey = New PrimaryKey(Id_Publicacion, True, True)

            _foreingKeys = New System.Collections.ObjectModel.Collection(Of ForeingKey)
            _foreingKeys.Add(New ForeingKey(id_Entidad, New Entidad().Id_entidad))
            _foreingKeys.Add(New ForeingKey(Id_tipoPublicacion, New TipoPublicacion().Id_TipoPublicacion))
            _foreingKeys.Add(New ForeingKey(Id_Categoria, New Categorias().Id_Categoria))
            _foreingKeys.Add(New ForeingKey(Id_Estado_Publicacion, New Estado_Publicacion().Id_Estado_Publicacion))

            _uniqueKeys = Nothing
        End Sub

        Private Sub SetFields()
            _fields = New FieldCollection
            _fields.Add(Id_Publicacion)
            _fields.Add(Id_tipoPublicacion)
            _fields.Add(IncluirRSS)
            _fields.Add(Titulo)
            _fields.Add(Corta)
            _fields.Add(Texto)
            _fields.Add(Link)
            _fields.Add(Visible)
            _fields.Add(Aprobada)
            _fields.Add(Fecha)
            _fields.Add(FechaInicio)
            _fields.Add(FechaFin)
            _fields.Add(id_Entidad)
            _fields.Add(Id_Categoria)
            _fields.Add(Id_Estado_Publicacion)
        End Sub

        Public Overrides ReadOnly Property TableName() As String
            Get
                Return _tableName
            End Get
        End Property

        Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As DBTable
            Dim theInstance As Publicacion
            If forScripting Then
                theInstance = NewForScripting()
            Else
                theInstance = New Publicacion
            End If
            Return theInstance
        End Function

        Shared Function NewForScripting() As Publicacion
            Dim temp As New Publicacion()
            temp.SetFields()
            temp.SetKeysForScripting()
            temp.Fields.SelectAll()
            Return temp
        End Function

    End Class
End Namespace