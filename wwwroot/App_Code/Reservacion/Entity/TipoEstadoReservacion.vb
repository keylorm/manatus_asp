Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler
Imports Orbelink.Control.HistorialEstados

Namespace Orbelink.Entity.Reservaciones

    Public Class TipoEstadoReservacion : Inherits DBTable
        Implements ITipoEstado

        Dim _tableName As String = "RS_TipoEstadoReservacion"

        Private ReadOnly _Id_TipoEstadoReservacion As New FieldInt("Id_TipoEstadoReservacion", Me, False, True)
        Private ReadOnly _Nombre As New FieldString("n_tipoEstadoReservacion", Me, Field.FieldType.VARCHAR_MULTILANG, 150)
        Private ReadOnly _Inicial As New FieldTinyInt("Inicial", Me)
        Private ReadOnly _Terminador As New FieldTinyInt("Terminador", Me)
        Private ReadOnly _bloqueado As New FieldTinyInt("Bloqueado", Me)
        Private ReadOnly _RepresentaCancelado As New FieldTinyInt("RepresentaCancelado", Me)
        Private ReadOnly _RepresentaReservado As New FieldTinyInt("RepresentaReservado", Me)


        Public Sub New()
            SetFields()
        End Sub

        Public Sub New(ByVal the_Nombre As String, ByVal the_Terminador As Integer, ByVal the_RepresentaCancelado As Integer, _
                       ByVal the_RepresentaReservado As Integer, ByVal the_inicial As Integer, ByVal the_bloqueado As Integer)
            _Nombre.Value = the_Nombre
            _Inicial.Value = the_inicial
            _Terminador.Value = the_Terminador
            _bloqueado.Value = the_bloqueado
            _RepresentaCancelado.Value = the_RepresentaCancelado
            _RepresentaReservado.Value = the_RepresentaReservado

            SetFields()
            _primaryKey = Nothing
            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Protected Overrides Sub SetKeysForScripting()
            _primaryKey = New PrimaryKey(_Id_TipoEstadoReservacion, True, True)

            _foreingKeys = Nothing
            _uniqueKeys = Nothing
        End Sub

        Private Sub SetFields()
            _fields = New FieldCollection
            _fields.Add(_Id_TipoEstadoReservacion)
            _fields.Add(_Nombre)
            _fields.Add(_Inicial)
            _fields.Add(_bloqueado)
            _fields.Add(_Terminador)
            _fields.Add(_RepresentaCancelado)
            _fields.Add(_RepresentaReservado)
        End Sub

        Public Overrides ReadOnly Property TableName() As String
            Get
                Return _tableName
            End Get
        End Property

        Public Overrides Function CreateInstance(Optional ByVal forScripting As Boolean = False) As DBTable
            Dim theInstance As TipoEstadoReservacion
            If forScripting Then
                theInstance = NewForScripting()
            Else
                theInstance = New TipoEstadoReservacion
            End If
            Return theInstance
        End Function

        Shared Function NewForScripting() As TipoEstadoReservacion
            Dim temp As New TipoEstadoReservacion()
            temp.SetFields()
            temp.SetKeysForScripting()
            temp.Fields.SelectAll()
            Return temp
        End Function

        Public ReadOnly Property Id_TipoEstadoReservacion() As DBHandler.FieldInt Implements Control.HistorialEstados.ITipoEstado.Id_TipoEstado
            Get
                Return _Id_TipoEstadoReservacion
            End Get
        End Property

        Public ReadOnly Property Nombre() As DBHandler.FieldString Implements Control.HistorialEstados.ITipoEstado.Nombre
            Get
                Return _Nombre
            End Get
        End Property

        Public ReadOnly Property Inicial() As FieldTinyInt
            Get
                Return _Inicial
            End Get
        End Property


        Public ReadOnly Property Bloqueado() As FieldTinyInt
            Get
                Return _bloqueado
            End Get
        End Property

        Public ReadOnly Property Terminador() As FieldTinyInt
            Get
                Return _Terminador
            End Get
        End Property

        Public ReadOnly Property RepresentaCancelado() As FieldTinyInt
            Get
                Return _RepresentaCancelado
            End Get
        End Property

        Public ReadOnly Property RepresentaReservado() As FieldTinyInt
            Get
                Return _RepresentaReservado
            End Get
        End Property

    End Class

End Namespace