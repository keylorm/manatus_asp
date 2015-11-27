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
    'Protected ingresoSalida As String = "29/11/2015 - 30/11/2015"
    'Protected servicio As String = "2 habitaciones para 3 personas, 2 noches, 0 noches adicionales Impuestos incluidos. Traslados incluidos."
    'Protected personas As Integer = 3
    'Protected habitaciones As Integer = 2
    'Protected costoEstadia As Double = 790.0
    'Protected costoNocheAdicional As Double = 0.0

    ''Datos personales
    'Protected nombreCompleto As String = "María Quesadas López"
    'Protected telefono As String = "2234-5390"
    'Protected email As String = "mquesada@gmail.com"
    'Protected noTarjeta As String = "10418920001"
    'Protected tarjetaVencimiento As String = "08/17"
    'Protected tarjeta As String = "Visa"

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        '*********** Originalmente el codigo para mostrar los datos de la reserva y del usuario deben de cargarse dinamicamente por medio de sesion ***********
        'If Session("id_reservacion") IsNot Nothing Then
        '    Dim id_reservacion As Integer = Session("id_reservacion")
        '    numero_orden.InnerText = id_reservacion
        '    CargarCarrito(numero_orden.InnerText)
        'End If

        Dim id_reservacion As Integer = 10960
        CargarCarrito(id_reservacion)

        '************ Temporalmente se mostrará la información estaticamente *********
        'ValueLblIngresoSalida.Text = ingresoSalida
        'ValueLblServicio.Text = servicio
        'ValueLblPersonas.Text = personas
        'ValueLblHabitaciones.Text = habitaciones
        'ValueLblCostoSinTransporte.Text = String.Format("{0:$###,###,###.##}", costoEstadia)
        'ValueLblCostoAdicional.Text = String.Format("{0:$###,###,###.##}", costoNocheAdicional)
        'ValueLblCostoTotal.Text = String.Format("{0:$###,###,###.##}", (costoEstadia + costoNocheAdicional))

        'lblNombreCompleto.Text = nombreCompleto
        'lblTelefono.Text = telefono
        'lblEmail.Text = email
        'lblNoTarjeta.Text = noTarjeta
        'lblFechaVencimiento.Text = tarjetaVencimiento
        'lblTipoTarjeta.Text = tarjeta
        '************ Fin de inicializacion de datos temporales *********

    End Sub

    Public Function CargarCarrito(ByVal id_reservacion As String) As Boolean

        Dim QueryBuilder As New QueryBuilder
        Dim ds As DataSet
        Dim reservacion As New Reservacion
        Dim factura As New Factura
        Dim ubicacion As New Ubicacion
        Dim entidad As New Entidad()
        Dim detalle_factura As New DetalleFactura
        Dim detalle_reservacion As New Detalle_Reservacion

        'selects
        factura.Id_Factura.ToSelect = True
        factura.SubTotal.ToSelect = True
        factura.PorcentajeImpuestos.ToSelect = True
        factura.Total.ToSelect = True
        entidad.Fields.SelectAll()
        ubicacion.Nombre.ToSelect = True
        ubicacion.Id_ubicacion.ToSelect = True
        reservacion.fecha_inicioProgramado.ToSelect = True
        reservacion.fecha_finalProgramado.ToSelect = True
        reservacion.Id_factura.ToSelect = True
        reservacion.id_Cliente.ToSelect = True
        reservacion.Id_Reservacion.ToSelect = True
        reservacion.descripcion.ToSelect = True
        detalle_reservacion.Adultos.ToSelect = True
        detalle_reservacion.Ninos.ToSelect = True

        'where
        reservacion.Id_Reservacion.Where.EqualCondition(id_reservacion)

        'joins
        QueryBuilder.Join.EqualCondition(entidad.Id_entidad, reservacion.id_Cliente)
        QueryBuilder.Join.EqualCondition(reservacion.Id_factura, factura.Id_Factura)
        QueryBuilder.Join.EqualCondition(ubicacion.Id_ubicacion, entidad.Id_Ubicacion)
        QueryBuilder.Join.EqualCondition(detalle_reservacion.id_reservacion, reservacion.Id_Reservacion)

        'froms
        QueryBuilder.From.Add(reservacion)
        QueryBuilder.From.Add(ubicacion)
        QueryBuilder.From.Add(entidad)
        QueryBuilder.From.Add(factura)
        QueryBuilder.From.Add(detalle_reservacion)

        'execute
        ds = connection.executeSelect(QueryBuilder.RelationalSelectQuery)

        If ds.Tables.Count > 0 Then
            Dim personas As Integer = 0  'suma entre adultos y niños
            Dim habitaciones As Integer = ds.Tables(0).Rows.Count  'conteo de filas
            If ds.Tables(0).Rows.Count > 0 Then

                ObjectBuilder.CreateObject(ds.Tables(0), 0, entidad)
                ObjectBuilder.CreateObject(ds.Tables(0), 0, reservacion)
                ObjectBuilder.CreateObject(ds.Tables(0), 0, factura)
                ObjectBuilder.CreateObject(ds.Tables(0), 0, ubicacion)

                Dim fechaInicio As Date = FormatDateTime(reservacion.fecha_inicioProgramado.ValueLocalized, DateFormat.ShortDate)
                Dim fechaFinal As Date = FormatDateTime(reservacion.fecha_finalProgramado.ValueLocalized, DateFormat.ShortDate)
                Dim idReservacion As Integer = CType(reservacion.Id_Reservacion.Value, Integer)

                'extraemos el total del costo de la noche adicional
                Dim precioUnitarioExtra As Double = precio_noche_adicional(idReservacion)

                'recorremos cada tupla para extraer la cantidad de personas (adultos + niños)
                For index = 0 To ds.Tables(0).Rows.Count - 1
                    ObjectBuilder.CreateObject(ds.Tables(0), index, detalle_reservacion)
                    personas = (personas + (detalle_reservacion.Adultos.Value + detalle_reservacion.Ninos.Value))
                Next

                'Personal Information
                lblNombreCompleto.Text = entidad.NombreDisplay.Value
                lblTelefono.Text = entidad.Telefono.Value
                lblEmail.Text = entidad.Email.Value

                'Definir si tiene impuestos
                Dim impuestos As Double = CDbl(factura.Total.Value) - CDbl(factura.SubTotal.Value)
                Dim taxesText = "Impuestos incluidos"
                If impuestos <> 0 Then
                    taxesText = "Impuestos no incluidos"
                End If

                'Booking information
                ValueLblIngresoSalida.Text = fechaInicio + " - " + fechaFinal
                ValueLblServicio.Text = CType(habitaciones, String) + " habitaciones para " + CType(personas, String) + " personas, 2 noches, noches adicionales " + CType(precioUnitarioExtra, String) + ", " + taxesText + " . Transporte incluido."
                ValueLblPersonas.Text = CType(personas, String)
                ValueLblHabitaciones.Text = CType(habitaciones, String)
                ValueLblCostoSinTransporte.Text = String.Format("{0:$###,###,###.##}", factura.SubTotal.Value)
                If (precioUnitarioExtra = 0.0) Then
                    ValueLblCostoAdicional.Text = "$0"
                Else
                    ValueLblCostoAdicional.Text = String.Format("{0:$###,###,###.##}", precioUnitarioExtra)
                End If
                ValueLblCostoTotal.Text = String.Format("{0:$###,###,###.##}", (factura.SubTotal.Value + precioUnitarioExtra))

            End If
        End If

    End Function

    Public Function precio_noche_adicional(id_reserva As Integer) As Double

        'realizamos una subconsulta para obtener los datos de la noche adicional (relacion entre la tabla factura y detalle factura)
        Dim QueryBuilder As New QueryBuilder
        Dim ds As DataSet
        Dim reservacion As New Reservacion
        Dim factura As New Factura
        Dim detalle_factura As New DetalleFactura
        Dim total As Double = 0

        'selects
        detalle_factura.Precio_Unitario_Extra.ToSelect = True

        'where
        reservacion.Id_Reservacion.Where.EqualCondition(id_reserva)

        'Join
        QueryBuilder.Join.EqualCondition(reservacion.Id_factura, factura.Id_Factura)
        QueryBuilder.Join.EqualCondition(factura.Id_Factura, detalle_factura.Id_Factura)

        'froms
        QueryBuilder.From.Add(reservacion)
        QueryBuilder.From.Add(factura)
        QueryBuilder.From.Add(detalle_factura)

        ds = connection.executeSelect(QueryBuilder.RelationalSelectQuery)
        'execute

        If ds.Tables.Count > 0 Then
            If ds.Tables(0).Rows.Count > 0 Then

                For i As Integer = 0 To ds.Tables(0).Rows.Count - 1
                    ObjectBuilder.CreateObject(ds.Tables(0), i, reservacion)
                    ObjectBuilder.CreateObject(ds.Tables(0), i, factura)
                    ObjectBuilder.CreateObject(ds.Tables(0), i, detalle_factura)
                    total += (CType(detalle_factura.Precio_Unitario_Extra.Value, Double))
                Next

            End If
        End If
        Return total
    End Function

End Class
