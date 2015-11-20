Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler

Namespace Orbelink.Entity.Facturas
    Public Class TipoFactura : Inherits DBTable

        Dim _tableName As String = "FR_TipoFactura"

        Public ReadOnly Id_TipoFactura As New FieldInt("Id_TipoFactura", Me, False, True)
        Public ReadOnly Nombre As New FieldString("N_TipoFactura", Me, Field.FieldType.VARCHAR, 50)
        Public ReadOnly PorcentajeImpuestos As New FieldFloat("PorcentajeImpuestos", Me)

        Public Sub New()
            SetFields()
        End Sub

        Public Sub New(ByVal theNombre As String, ByVal thePorcentajeImpuestos As Double)
            Nombre.Value = theNombre
            PorcentajeImpuestos.Value = thePorcentajeImpuestos
            SetFields()
            _primaryKey = Nothing
            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Protected Overrides Sub SetKeysForScripting()
            _primaryKey = New PrimaryKey(Id_TipoFactura, True, True)

            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Private Sub SetFields()
            _fields = New FieldCollection
            _fields.Add(Id_TipoFactura)
            _fields.Add(Nombre)
            _fields.Add(PorcentajeImpuestos)
        End Sub

        Public Overrides ReadOnly Property TableName() As String
            Get
                Return _tableName
            End Get
        End Property

        Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As DBTable
            Dim theInstance As TipoFactura
            If forScripting Then
                theInstance = NewForScripting()
            Else
                theInstance = New TipoFactura
            End If
            Return theInstance
        End Function

        Shared Function NewForScripting() As TipoFactura
            Dim temp As New TipoFactura()
            temp.SetFields()
temp.SetKeysForScripting()
temp.Fields.SelectAll()
Return temp
        End Function

    End Class

End Namespace