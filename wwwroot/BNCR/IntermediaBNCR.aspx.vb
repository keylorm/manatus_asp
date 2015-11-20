Imports Orbelink.Control.Facturas
Imports Orbelink.Control.Reservaciones
Imports Orbelink.Control.Facturas.FacturasHandler
Imports Orbelink.Entity.Facturas
Imports Orbelink.Entity.Reservaciones
Partial Class IntermediaBNCR
    Inherits Orbelink.FrontEnd6.PageBaseClass

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim carrito As New FacturasHandler(Configuration.Config_DefaultConnectionString)
        Dim id_reservacion As Integer
        If Session("id_reservacion") IsNot Nothing Then
            id_reservacion = Session("id_reservacion")
        End If

        If id_reservacion > 0 Then
            Dim ControladorReservaciones As New ControladorReservaciones(connection, Resources.Reservaciones_Resources.ResourceManager)
            Dim reservacion As Reservacion
            reservacion = ControladorReservaciones.ConsultarReservacion(id_reservacion)
            Dim factura As Factura
            factura = carrito.ConsultarFactura(reservacion.Id_factura.Value)
            If factura.Id_Factura.IsValid Then
                'If factura.Id_Comprador.Value = securityHandler.EntidadAsociada Then
                Dim EntidadHandler As New Orbelink.Control.Entidades.EntidadHandler(Configuration.Config_DefaultConnectionString)
                Dim entidad As Orbelink.Entity.Entidades.Entidad = EntidadHandler.ConsultarEntidad(factura.Id_Comprador.Value)
                Dim id_carrito As Integer = reservacion.Id_factura.Value
                Dim total As Double = carrito.GetTotal(id_carrito)
                Dim vpos As VPOS = New VPOS
                ltr_values.Text = vpos.EnviarInfo(carrito.GetItemsProductos(id_carrito), id_reservacion, total, entidad.NombreEntidad.Value, entidad.Apellido.Value, entidad.Email.Value, entidad.Descripcion.Value, "506", "San Jose", "San Jose", "CR", entidad.Telefono.Value)
                'End If
            Else
                Response.Redirect(ArmarQueryString(), False)
            End If
        Else
            Response.Redirect(ArmarQueryString(), False)
        End If
    End Sub

    Protected Function ArmarQueryString() As String
        Dim query As String = ""
        query = "~/reservacion_sp.aspx?return=IntermediaBNCR.aspx"
        Return query
    End Function
End Class
