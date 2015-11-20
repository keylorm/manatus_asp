Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler
Imports Orbelink.Entity.Entidades

Namespace Orbelink.Entity.Publicaciones
    Public Class Aprobacion_Publicacion : Inherits DBTable

        Dim _tableName As String = "PU_Aprobacion_Publicacion"

        Public ReadOnly Id_Entidad As New FieldInt("Id_Entidad", Me)
        Public ReadOnly Id_Publicacion As New FieldInt("Id_Publicacion", Me)
        Public ReadOnly Visto As New FieldTinyInt("Visto", Me)
        Public ReadOnly Aprobado As New FieldTinyInt("Aprobado", Me)
        Public ReadOnly Fecha_Visto As New FieldDateTime("Fecha_Visto", Me)
        Public ReadOnly Fecha_Aprobado As New FieldDateTime("Fecha_Aprobado", Me, True)
        Public ReadOnly Comentario As New FieldString("Comentario", Me, Field.FieldType.VARCHAR_MULTILANG, 200, True)
        Public ReadOnly Id_TipoRelacion As New FieldInt("Id_TipoRelacion", Me)
        Public ReadOnly Id_Grupo As New FieldInt("Id_Grupo", Me)

        Public Sub New()
            SetFields()
        End Sub

        <Obsolete("Esta metodo recibe una o mas fechas en UTC", False)> _
        Public Sub New(ByVal theId_Entidad As String, ByVal theId_Publicacion As String, ByVal theVisto As String, _
            ByVal theAprobado As String, ByVal theFecha_Visto As String, ByVal theFecha_Aprobado As String, _
            ByVal theComentario As String, ByVal theId_TipoRelacion As String, ByVal theId_Grupo As String)
            Id_Entidad.Value = theId_Entidad
            Id_Publicacion.Value = theId_Publicacion
            Visto.Value = theVisto
            Aprobado.Value = theAprobado
            Fecha_Visto.ValueUtc = theFecha_Visto
            Fecha_Aprobado.ValueUtc = theFecha_Aprobado
            Comentario.Value = theComentario
            Id_TipoRelacion.Value = theId_TipoRelacion
            Id_Grupo.Value = theId_Grupo
            SetFields()
            _primaryKey = Nothing
            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Protected Overrides Sub SetKeysForScripting()
            _primaryKey = New PrimaryKey(Id_Publicacion, True, True)
            _primaryKey.Add_PK(Id_Grupo, True)
            _primaryKey.Add_PK(Id_TipoRelacion, True)

            _foreingKeys = New System.Collections.ObjectModel.Collection(Of ForeingKey)
            _foreingKeys.Add(New ForeingKey(Id_Entidad, New Entidad().Id_entidad))
            _foreingKeys.Add(New ForeingKey(Id_Publicacion, New Publicacion().Id_Publicacion, ForeingKey.DeleteRule.CASCADE))
            _foreingKeys.Add(New ForeingKey(Id_TipoRelacion, New TipoRelacion_Grupos().Id_TipoRelacion))
            _foreingKeys.Add(New ForeingKey(Id_Grupo, New Grupos_Entidades().Id_Grupo))

            _uniqueKeys = Nothing
        End Sub

        Private Sub SetFields()
            _fields = New FieldCollection
            _fields.Add(Id_Entidad)
            _fields.Add(Id_Publicacion)
            _fields.Add(Visto)
            _fields.Add(Aprobado)
            _fields.Add(Fecha_Visto)
            _fields.Add(Fecha_Aprobado)
            _fields.Add(Comentario)
            _fields.Add(Id_TipoRelacion)
            _fields.Add(Id_Grupo)

        End Sub


        Public Overrides ReadOnly Property TableName() As String
            Get
                Return _tableName
            End Get
        End Property

        Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As DBTable
            Dim theInstance As Aprobacion_Publicacion
            If forScripting Then
                theInstance = NewForScripting()
            Else
                theInstance = New Aprobacion_Publicacion
            End If
            Return theInstance
        End Function

        Shared Function NewForScripting() As Aprobacion_Publicacion
            Dim temp As New Aprobacion_Publicacion()
            temp.SetFields()
            temp.SetKeysForScripting()
            temp.Fields.SelectAll()
            Return temp
        End Function

    End Class
End Namespace