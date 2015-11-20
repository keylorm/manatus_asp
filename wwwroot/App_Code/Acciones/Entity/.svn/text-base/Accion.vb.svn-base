Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler
Imports Orbelink.Entity.Entidades
Imports Orbelink.Entity.Currency

Namespace Orbelink.Entity.Acciones
    Public Class Accion
        Inherits DBTable

        Dim _tableName As String = "AC_Accion"

        Public ReadOnly Id_Accion As New FieldInt("id_Accion", Me, False, True)
        Public ReadOnly Id_MachoteAccion As New FieldInt("Id_MachoteAccion", Me)
        Public ReadOnly CodigoAccion As New FieldString("CodigoAccion", Me, Field.FieldType.VARCHAR, 250)
        Public ReadOnly CodigoValidacion As New FieldString("CodigoValidacion", Me, Field.FieldType.VARCHAR, 10)
        Public ReadOnly Valor As New FieldFloat("Valor", Me)
        Public ReadOnly FechaCreada As New FieldDateTime("FechaCreada", Me)
        Public ReadOnly FechaCaducidad As New FieldDateTime("FechaCaducidad", Me, True)
        Public ReadOnly GeneradoPor As New FieldInt("GeneradoPor", Me)
        Public ReadOnly Estado As New FieldInt("Estado", Me)
        Public ReadOnly Id_Moneda As New FieldInt("Id_Moneda", Me)
        Public ReadOnly Cliente As New FieldInt("Cliente", Me, True)
        Public ReadOnly FechaActivada As New FieldDateTime("FechaActivada", Me, True)

        Public Sub New()
            SetFields()
        End Sub

        '<Obsolete("Esta metodo recibe una o mas FechaCreadas en UTC", False)> _
        Public Sub New(ByVal theId_MachoteAccion As Integer, ByVal theCodigoAccion As String, ByVal theCodigoValidacion As String, ByVal theValor As Double, _
            ByVal theFechaCreada As Date, ByVal theFechaCaducidad As String, _
            ByVal theCliente As Integer, ByVal theGeneradoPor As Integer, ByVal theEstado As Integer, ByVal theId_Moneda As Integer, _
            ByVal theFechaActivada As Date)
            Id_MachoteAccion.Value = theId_MachoteAccion
            CodigoAccion.Value = theCodigoAccion
            CodigoValidacion.Value = theCodigoValidacion
            Valor.Value = theValor
            FechaCreada.ValueUtc = theFechaCreada
            FechaCaducidad.ValueUtc = theFechaCaducidad
            Cliente.Value = theCliente
            Estado.Value = theEstado
            Id_Moneda.Value = theId_Moneda
            GeneradoPor.Value = theGeneradoPor
            FechaActivada.ValueUtc = theFechaActivada
            SetFields()
            _primaryKey = Nothing
            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Protected Overrides Sub SetKeysForScripting()
            _primaryKey = New PrimaryKey(Id_Accion, True, True)

            _foreingKeys = New System.Collections.ObjectModel.Collection(Of ForeingKey)
            _foreingKeys.Add(New ForeingKey(Id_MachoteAccion, New MachoteAccion().Id_MachoteAccion))
            '_foreingKeys.Add(New ForeingKey(Estado, New Estados_Accions().Estado_Accion))
            _foreingKeys.Add(New ForeingKey(Id_Moneda, New Moneda().Id_Moneda))
            _foreingKeys.Add(New ForeingKey(Cliente, New Entidad().Id_entidad))
            _foreingKeys.Add(New ForeingKey(GeneradoPor, New Entidad().Id_entidad))

            _uniqueKeys = Nothing
        End Sub

        Private Sub SetFields()
            _fields = New FieldCollection
            _fields.Add(Id_Accion)
            _fields.Add(Id_MachoteAccion)
            _fields.Add(CodigoAccion)
            _fields.Add(CodigoValidacion)
            _fields.Add(Valor)
            _fields.Add(FechaCreada)
            _fields.Add(FechaCaducidad)
            _fields.Add(Cliente)
            _fields.Add(GeneradoPor)
            _fields.Add(Estado)
            _fields.Add(Id_Moneda)
            _fields.Add(FechaActivada)
        End Sub

        Public Overrides ReadOnly Property TableName() As String
            Get
                Return _tableName
            End Get
        End Property

        Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As DBTable
            Dim theInstance As Accion
            If forScripting Then
                theInstance = NewForScripting()
            Else
                theInstance = New Accion
            End If
            Return theInstance
        End Function

        Shared Function NewForScripting() As Accion
            Dim temp As New Accion()
            temp.SetFields()
            temp.SetKeysForScripting()
            temp.Fields.SelectAll()
            Return temp
        End Function

    End Class
End Namespace