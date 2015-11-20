Imports Microsoft.VisualBasic

Namespace Orbelink.Control.Reservaciones

    Public Class Controladora_HistorialEstados_Reservacion
        Implements Orbelink.Control.HistorialEstados.IControladorHistorialEstados

        Public Function CreateNewEstado() As HistorialEstados.IEstadosDueno Implements HistorialEstados.IControladorHistorialEstados.CreateNewEstado
            Return New Orbelink.Entity.Reservaciones.Estado_Reservacion
        End Function

        Public Function CreateNewTipoEstado() As HistorialEstados.ITipoEstado Implements HistorialEstados.IControladorHistorialEstados.CreateNewTipoEstado
            Return New Orbelink.Entity.Reservaciones.TipoEstadoReservacion
        End Function
    End Class

End Namespace
