Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler
Imports Orbelink.Entity.Entidades
Imports Orbelink.Entity.Currency

Namespace Orbelink.Entity.Acciones
    Public Class MachoteAccion
        Inherits DBTable

        Dim _tableName As String = "AC_MachoteAccion"

        Public ReadOnly Id_MachoteAccion As New FieldInt("Id_MachoteAccion", Me, False, True)
        Public ReadOnly Nombre As New FieldString("n_MachoteAccion", Me, Field.FieldType.VARCHAR_MULTILANG, 250)
        Public ReadOnly PrefijoCodigo As New FieldString("PrefijoCodigo", Me, Field.FieldType.VARCHAR, 5)
        Public ReadOnly FechaCreado As New FieldDateTime("FechaCreado", Me)
        Public ReadOnly CreadoPor As New FieldInt("CreadoPor", Me)
        Public ReadOnly Valor As New FieldFloat("Valor", Me)
        Public ReadOnly Moneda As New FieldInt("Moneda", Me)
        Public ReadOnly DiasValidez As New FieldInt("DiasValidez", Me)
        Public ReadOnly FechaExpiracion As New FieldDateTime("FechaExpiracion", Me, True)

        Public Sub New()
            SetFields()
        End Sub

        Public Sub New(ByVal theNombre As String, ByVal thePrefijoCodigo As String, ByVal theFechaCreado As Date, ByVal theCreadoPor As Integer, _
                       ByVal theValor As Double, ByVal theMoneda As Integer, ByVal theDiasValidez As Integer, ByVal theFechaExpiracion As Date)
            Nombre.Value = theNombre
            PrefijoCodigo.Value = thePrefijoCodigo
            FechaCreado.ValueUtc = theFechaCreado
            CreadoPor.Value = theCreadoPor
            Valor.Value = theValor
            Moneda.Value = theMoneda
            DiasValidez.Value = theDiasValidez
            FechaExpiracion.ValueUtc = theFechaExpiracion
            SetFields()
            _primaryKey = Nothing
            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Protected Overrides Sub SetKeysForScripting()
            _primaryKey = New PrimaryKey(Id_MachoteAccion, True, True)

            _foreingKeys = New System.Collections.ObjectModel.Collection(Of ForeingKey)
            _foreingKeys.Add(New ForeingKey(Moneda, New Moneda().Id_Moneda))
            _foreingKeys.Add(New ForeingKey(CreadoPor, New Entidad().Id_entidad))

            _uniqueKeys = Nothing
        End Sub

        Private Sub SetFields()
            _fields = New FieldCollection
            _fields.Add(Id_MachoteAccion)
            _fields.Add(Nombre)
            _fields.Add(PrefijoCodigo)
            _fields.Add(FechaCreado)
            _fields.Add(CreadoPor)
            _fields.Add(Valor)
            _fields.Add(Moneda)
            _fields.Add(DiasValidez)
            _fields.Add(FechaExpiracion)
        End Sub

        Public Overrides ReadOnly Property TableName() As String
            Get
                Return _tableName
            End Get
        End Property

        Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As DBTable
            Dim theInstance As MachoteAccion
            If forScripting Then
                theInstance = NewForScripting()
            Else
                theInstance = New MachoteAccion
            End If
            Return theInstance
        End Function

        Shared Function NewForScripting() As MachoteAccion
            Dim temp As New MachoteAccion()
            temp.SetFields()
            temp.SetKeysForScripting()
            temp.Fields.SelectAll()
            Return temp
        End Function

    End Class
End Namespace