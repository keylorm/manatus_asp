﻿Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler
Imports Orbelink.Entity.Facturas
Imports Orbelink.Entity.Entidades

Namespace Orbelink.Entity.Reservaciones

    Public Class Reservacion : Inherits DBTable

        Dim _tableName As String = "Rs_Reservacion"

        Public ReadOnly Id_Reservacion As New Orbelink.DBHandler.FieldInt("Id_Resevacion", Me, False, True)
        Public ReadOnly fechaRegistro As New Orbelink.DBHandler.FieldDateTime("f_registro", Me)
        Public ReadOnly fecha_inicioReal As New Orbelink.DBHandler.FieldDateTime("f_inicioReal", Me, True)
        Public ReadOnly fecha_inicioProgramado As New Orbelink.DBHandler.FieldDateTime("f_inicioProgramado", Me)
        Public ReadOnly fecha_finalReal As New Orbelink.DBHandler.FieldDateTime("f_finalReal", Me, True)
        Public ReadOnly fecha_finalProgramado As New Orbelink.DBHandler.FieldDateTime("f_finalProgramado", Me)
        Public ReadOnly id_Cliente As New Orbelink.DBHandler.FieldInt("id_Cliente", Me)
        Public ReadOnly Id_Ubicacion As New Orbelink.DBHandler.FieldInt("Id_Ubicacion", Me)
        Public ReadOnly Id_factura As New Orbelink.DBHandler.FieldInt("Id_factura", Me, True)
        Public ReadOnly Id_TipoEstado As New Orbelink.DBHandler.FieldInt("Id_TipoEstado", Me)
        Public ReadOnly descripcion As New Orbelink.DBHandler.FieldString("descripcion", Me, Orbelink.DBHandler.Field.FieldType.VARCHAR, 8000, True)

        <Obsolete("Esta metodo recibe una o mas fechas en UTC", False)> _
        Public Sub New(ByVal theFechaRegistro As Date, ByVal thefechaInicioProgramado As Date, ByVal theFechaFinalProgramado As Date, ByVal theId_cliente As Integer, ByVal theId_ubicacion As Integer, ByVal theId_TipoEstado As Integer, ByVal theDescripcion As String, ByVal theId_factura As Integer)
            fechaRegistro.ValueUtc = theFechaRegistro
            fecha_inicioProgramado.ValueUtc = thefechaInicioProgramado
            fecha_finalProgramado.ValueUtc = theFechaFinalProgramado
            id_Cliente.Value = theId_cliente
            Id_Ubicacion.Value = theId_ubicacion
            Id_TipoEstado.Value = theId_TipoEstado
            descripcion.Value = theDescripcion
            Id_factura.Value = theId_factura
            SetFields()
            _primaryKey = Nothing
            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Public Sub New()
            SetFields()
        End Sub

        Public Sub New(ByVal forScripting As Boolean)
            If forScripting Then
                SetFields()
                SetKeysForScripting()
            End If
        End Sub

        Private Sub SetFields()
            _fields = New FieldCollection
            _fields.Add(Id_Reservacion)
            _fields.Add(Id_TipoEstado)
            _fields.Add(fechaRegistro)
            _fields.Add(fecha_inicioReal)
            _fields.Add(fecha_inicioProgramado)
            _fields.Add(fecha_finalReal)
            _fields.Add(Id_factura)
            _fields.Add(fecha_finalProgramado)
            _fields.Add(id_Cliente)
            _fields.Add(Id_Ubicacion)
            _fields.Add(descripcion)
        End Sub

        Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As Orbelink.DBHandler.DBTable
            Dim theInstance As Reservacion
            If forScripting Then
                theInstance = New Reservacion(True)
            Else
                theInstance = New Reservacion
            End If
            Return theInstance
        End Function

        Protected Overrides Sub SetKeysForScripting()
            _primaryKey = New PrimaryKey(Id_Reservacion, True, True)

            _foreingKeys = New System.Collections.ObjectModel.Collection(Of ForeingKey)
            _foreingKeys.Add(New ForeingKey(id_Cliente, New Entidad().Id_entidad))
            _foreingKeys.Add(New ForeingKey(Id_Ubicacion, New Ubicacion().Id_ubicacion))
            _foreingKeys.Add(New ForeingKey(Id_factura, New Factura().Id_Factura))
            _foreingKeys.Add(New ForeingKey(Id_TipoEstado, New TipoEstadoReservacion().Id_TipoEstadoReservacion))

            _uniqueKeys = New System.Collections.ObjectModel.Collection(Of Orbelink.DBHandler.UniqueKey)
        End Sub

        Public Overrides ReadOnly Property TableName() As String
            Get
                Return _tableName
            End Get
        End Property

        Shared Function NewForScripting() As Reservacion
            Dim temp As New Reservacion()
            temp.SetFields()
            temp.SetKeysForScripting()
            temp.Fields.SelectAll()
            Return temp
        End Function
    End Class

End Namespace