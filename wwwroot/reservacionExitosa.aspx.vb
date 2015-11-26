Imports Orbelink.DBHandler
Imports Orbelink.Entity.Reservaciones
Imports Orbelink.Entity.Productos
Imports Orbelink.Control.Facturas
Imports Orbelink.Control.Reservaciones
Imports Orbelink.Entity.Facturas
Imports Orbelink.Entity.Entidades
Imports System.Data
Partial Class reservacionExitosa
    Inherits Orbelink.FrontEnd6.PageBaseClass

    'Datos de la reservacion
    Protected ingresoSalida As String = "29/11/2015 - 30/11/2015"
    Protected servicio As String = "2 habitaciones para 3 personas, 2 noches, 0 noches adicionales Impuestos incluidos. Traslados incluidos."
    Protected personas As Integer = 3
    Protected habitaciones As Integer = 2
    Protected costoEstadia As Double = 790.0
    Protected costoNocheAdicional As Double = 0.0

    'Datos personales
    Protected nombreCompleto As String = "María Quesadas López"
    Protected telefono As String = "2234-5390"
    Protected email As String = "mquesada@gmail.com"
    Protected noTarjeta As String = "10418920001"
    Protected tarjetaVencimiento As String = "08/17"
    Protected tarjeta As String = "Visa"

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        '*********** Originalmente el codigo para mostrar los datos de la reserva y del usuario deben de cargarse dinamicamente por medio de sesion ***********

        'If Session("id_reservacion") IsNot Nothing Then
        '    Dim id_reservacion As Integer = Session("id_reservacion")
        '    numero_orden.InnerText = id_reservacion
        '    CargarCarrito(numero_orden.InnerText)


        'End If

        '************ Temporalmente se mostrará la información estaticamente *********
        ValueLblIngresoSalida.Text = ingresoSalida
        ValueLblServicio.Text = servicio
        ValueLblPersonas.Text = personas
        ValueLblHabitaciones.Text = habitaciones
        ValueLblCostoSinTransporte.Text = String.Format("{0:$###,###,###.##}", costoEstadia)
        ValueLblCostoAdicional.Text = String.Format("{0:$###,###,###.##}", costoNocheAdicional)
        ValueLblCostoTotal.Text = String.Format("{0:$###,###,###.##}", (costoEstadia + costoNocheAdicional))

        lblNombreCompleto.Text = nombreCompleto
        lblTelefono.Text = telefono
        lblEmail.Text = email
        lblNoTarjeta.Text = noTarjeta
        lblFechaVencimiento.Text = tarjetaVencimiento
        lblTipoTarjeta.Text = tarjeta
        '************ Fin de inicializacion de datos temporales *********

    End Sub
End Class
