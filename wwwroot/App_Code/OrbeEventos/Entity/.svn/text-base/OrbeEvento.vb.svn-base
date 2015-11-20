Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler
Imports Orbelink.Entity.Orbecatalog

Namespace Orbelink.Entity.OrbeEvents

    Public Class OrbeEvento
        Inherits DBTable

        Dim _tableName As String = "OE_OrbeEvento"

        Public ReadOnly Id_OrbeEvento As New FieldInt("Id_OrbeEvento", Me, False, True)
        Public ReadOnly Codigo_Pantalla As New FieldString("Codigo_Pantalla", Me, Field.FieldType.VARCHAR, 10)
        Public ReadOnly Numero_Evento As New FieldInt("Numero_Evento", Me)
        Public ReadOnly Accion As New FieldInt("Accion", Me)
        Public ReadOnly FechaCreado As New FieldDateTime("F_OrbeEvento", Me)
        Public ReadOnly Nombre As New FieldString("N_OrbeEvento", Me, Field.FieldType.VARCHAR_MULTILANG, 150)
        Public ReadOnly Activo As New FieldTinyInt("Activo", Me)
        Public ReadOnly EspecificacionLlave As New FieldString("EspecificacionLlave", Me, Field.FieldType.VARCHAR, 250)
        Public ReadOnly Expresion As New FieldString("Expresion", Me, Field.FieldType.VARCHAR, 500)
        Public ReadOnly Referencia As New FieldString("Referencia", Me, Field.FieldType.VARCHAR_MULTILANG, 250, True)

        Public Sub New()
            SetFields()
        End Sub

        <Obsolete("Esta metodo recibe una o mas fechas en UTC", False)> _
        Public Sub New(ByVal theCodigo_Pantalla As String, ByVal theNumero_Evento As Integer, ByVal theAccion As Integer, _
            ByVal theFechaCreado As Date, ByVal theNombre As String, ByVal theActivo As Integer, ByVal theEspecificacionLlave As String, _
            ByVal theExpresion As String, ByVal theReferencia As String)
            Codigo_Pantalla.Value = theCodigo_Pantalla
            Numero_Evento.Value = theNumero_Evento
            Accion.Value = theAccion
            FechaCreado.ValueUtc = theFechaCreado
            Nombre.Value = theNombre
            Activo.Value = theActivo
            EspecificacionLlave.Value = theEspecificacionLlave
            Expresion.Value = theExpresion
            Referencia.Value = theReferencia
            SetFields()
            _primaryKey = Nothing
            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Protected Overrides Sub SetKeysForScripting()
            _primaryKey = New PrimaryKey(Id_OrbeEvento, True, True)

            _foreingKeys = New System.Collections.ObjectModel.Collection(Of ForeingKey)
            _foreingKeys.Add(New ForeingKey(Codigo_Pantalla, New Pantallas().codigo_Pantalla))

            _uniqueKeys = Nothing
        End Sub

        Private Sub SetFields()
            _fields = New FieldCollection
            _fields.Add(Id_OrbeEvento)
            _fields.Add(Codigo_Pantalla)
            _fields.Add(Numero_Evento)
            _fields.Add(Accion)
            _fields.Add(FechaCreado)
            _fields.Add(Nombre)
            _fields.Add(Activo)
            _fields.Add(EspecificacionLlave)
            _fields.Add(Expresion)
            _fields.Add(Referencia)
        End Sub

        Public Overrides ReadOnly Property TableName() As String
            Get
                Return _tableName
            End Get
        End Property

        Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As DBTable
            Dim theInstance As OrbeEvento
            If forScripting Then
                theInstance = NewForScripting()
            Else
                theInstance = New OrbeEvento
            End If
            Return theInstance
        End Function

        Shared Function NewForScripting() As OrbeEvento
            Dim temp As New OrbeEvento()
            temp.SetFields()
            temp.SetKeysForScripting()
            temp.Fields.SelectAll()
            Return temp
        End Function

    End Class
End Namespace