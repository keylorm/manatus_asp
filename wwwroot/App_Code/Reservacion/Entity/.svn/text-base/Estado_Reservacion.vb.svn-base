Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler
Imports Orbelink.Entity.Entidades

Namespace Orbelink.Entity.Reservaciones

    Public Class Estado_Reservacion : Inherits DBTable
        Implements Orbelink.Control.HistorialEstados.IEstadosDueno

        Dim _tableName As String = "RS_Estado_Reservacion"

        Private ReadOnly _Id_Reservacion As New FieldInt("Id_Reservacion", Me)
        Private ReadOnly _Id_TipoEstadoReservacion As New FieldInt("Id_TipoEstadoReservacion", Me)
        Private ReadOnly _Fecha As New FieldDateTime("F_Estado_Reservacion", Me)
        Private ReadOnly _Actual As New FieldInt("Actual", Me)
        Private ReadOnly _Responsable As New FieldInt("Responsable", Me)
        Private ReadOnly _Comentario As New FieldString("Comentario", Me, Field.FieldType.VARCHAR_MULTILANG, 50, True)

        Public Sub New()
            SetFields()
        End Sub

        <Obsolete("Esta metodo recibe una o mas fechas en UTC", False)> _
        Public Sub New(ByVal the_Id_Reservacion As Integer, ByVal theEstado As Integer, ByVal the_Fecha As Date, ByVal the_Actual As String, _
            ByVal the_Responsable As Integer, ByVal the_Comentario As String)
            _Id_Reservacion.Value = the_Id_Reservacion
            _Id_TipoEstadoReservacion.Value = theEstado
            _Fecha.ValueUtc = the_Fecha
            _Actual.Value = the_Actual
            _Responsable.Value = the_Responsable
            _Comentario.Value = the_Comentario
            SetFields()
            _primaryKey = Nothing
            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Protected Overrides Sub SetKeysForScripting()
            _primaryKey = New PrimaryKey(_Id_Reservacion, True, True)
            _primaryKey.Add_PK(_Id_TipoEstadoReservacion, True)

            _foreingKeys = New System.Collections.ObjectModel.Collection(Of ForeingKey)
            _foreingKeys.Add(New ForeingKey(_Responsable, New Entidad().Id_entidad))
            _foreingKeys.Add(New ForeingKey(_Id_Reservacion, New Reservacion().Id_Reservacion, ForeingKey.DeleteRule.CASCADE))
            _foreingKeys.Add(New ForeingKey(_Id_TipoEstadoReservacion, New TipoEstadoReservacion().Id_TipoEstadoReservacion))

            _uniqueKeys = Nothing
        End Sub

        Private Sub SetFields()
            _fields = New FieldCollection
            _fields.Add(_Id_Reservacion)
            _fields.Add(_Id_TipoEstadoReservacion)
            _fields.Add(_Fecha)
            _fields.Add(_Actual)
            _fields.Add(_Responsable)
            _fields.Add(_Comentario)
        End Sub

        Public Overrides ReadOnly Property TableName() As String
            Get
                Return _tableName
            End Get
        End Property

        Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As DBTable
            Dim theInstance As Estado_Reservacion
            If forScripting Then
                theInstance = NewForScripting()
            Else
                theInstance = New Estado_Reservacion
            End If
            Return theInstance
        End Function

        Shared Function NewForScripting() As Estado_Reservacion
            Dim temp As New Estado_Reservacion()
            temp.SetFields()
            temp.SetKeysForScripting()
            temp.Fields.SelectAll()
            Return temp
        End Function


        Public ReadOnly Property Actual() As DBHandler.FieldInt Implements Control.HistorialEstados.IEstadosDueno.Actual
            Get
                Return Me._Actual
            End Get
        End Property

        Public ReadOnly Property Fecha() As DBHandler.FieldDateTime Implements Control.HistorialEstados.IEstadosDueno.Fecha
            Get
                Return Me._Fecha
            End Get
        End Property

        Public ReadOnly Property Id_Reservacion() As DBHandler.FieldInt Implements Control.HistorialEstados.IEstadosDueno.Id_Dueno
            Get
                Return Me._Id_Reservacion
            End Get
        End Property

        Public ReadOnly Property Id_TipoEstadoReservacion() As DBHandler.FieldInt Implements Control.HistorialEstados.IEstadosDueno.Id_TipoEstado
            Get
                Return _Id_TipoEstadoReservacion
            End Get
        End Property

        Public ReadOnly Property Responsable() As DBHandler.FieldInt Implements Control.HistorialEstados.IEstadosDueno.Responsable
            Get
                Return _Responsable
            End Get
        End Property

        Public ReadOnly Property Comentario() As DBHandler.FieldString Implements Control.HistorialEstados.IEstadosDueno.Comentario
            Get
                Return _Comentario
            End Get
        End Property
    End Class
End Namespace