Imports Orbelink.DBHandler
Imports System.Data
Imports Orbelink.Service
Imports Orbelink.Entity.Facturas
Imports Orbelink.Entity.Productos
Imports Orbelink.Control.Facturas
Imports Orbelink.Control.Reservaciones

Partial Class Intermedia
    Inherits Orbelink.FrontEnd6.PageBaseClass


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Session("id_reservacion") Is Nothing Then
            'Response.Redirect("reservaciones.aspx?exito=0")
        Else
         
            Dim id_reservacion As Integer = Session("id_reservacion")
            Dim controladora As New ControladorReservaciones(connection, Resources.Reservaciones_Resources.ResourceManager)
            Dim id_Carrito As Integer = controladora.obtenerFactura(id_reservacion)

            If id_Carrito > 0 Then

                Dim id_encriptado As String = crypto.CifrarCadena("id_reserva" & id_reservacion)
                Dim carrito As New FacturasHandler(connection)
                Try
                    Dim DetalleFactura As New DetalleFactura
                    cargaLiteral(carrito.GetItems(id_Carrito, DetalleFactura), id_encriptado, carrito.buscarPorcentajeImpuestos(1))
                Catch ex As Exception
                    Response.Redirect("errorT.aspx?id=" & id_encriptado)
                End Try
            Else
                Response.Redirect("reservaciones.aspx?exito=0")
            End If
            'controladora.Emails(id_reservacion)
        End If
    End Sub

    Protected Sub cargaLiteral(ByVal dataset As DataTable, ByVal id_Encriptado As String, ByVal impuestos As Double)
        Culture = "en-us"
        Dim i As Integer = 0
        'Dim Producto As New Producto
        Dim DetalleFactura As New DetalleFactura

        Dim id_reservacion As Integer = 0
        If Session("id_reservacion") IsNot Nothing Then
            id_reservacion = (Session("id_reservacion"))
        End If

        Dim carrito As New FacturasHandler(Configuration.Config_DefaultConnectionString)

        Dim controladora As New ControladorReservaciones(connection, Resources.Reservaciones_Resources.ResourceManager)
        Dim id_Carrito As Integer = controladora.obtenerFactura(id_reservacion)

        Dim literal As String = ""

        Dim return_ As String = "<input type='hidden' name='return' value='  " & Configuration.Config_WebsiteRoot & "Paypal/exitosaTransaccion.aspx?id=" & id_Encriptado & " ' />"
        Dim cancel_return As String = "<input type='hidden' name='cancel_return' value='  " & Configuration.Config_WebsiteRoot & "Paypal/errorT.aspx?id=" & id_Encriptado & " ' />"
        'Dim return_ As String = "<input type='hidden' name='return' value='  " & Configuration.Config_WebsiteRoot & "Paypal/exitosaTransaccion.aspx?id=" & id_Encriptado & "&idioma=" & Orbelink.DBHandler.LanguageHandler.CurrentLanguage & " ' />"
        'Dim cancel_return As String = "<input type='hidden' name='cancel_return' value='  " & Configuration.Config_WebsiteRoot & "Paypal/errorT.aspx?id=" & id_Encriptado & "&idioma=" & Orbelink.DBHandler.LanguageHandler.CurrentLanguage & " ' />"

        Dim itemName As String = "<input type='hidden' name='item_name_"
        Dim ammount As String = "<input type='hidden' name='amount_"
        Dim itemNumber As String = "<input type='hidden' name='item_number_"
        Dim quantity As String = "<input type='hidden' name='quantity_"


        If dataset.Rows.Count > 0 Then
            literal += return_
            literal += cancel_return
            'For i = 0 To dataset.Rows.Count - 1
            'ObjectBuilder.CreateObject(dataset, i, Producto)
            'ObjectBuilder.CreateObject(dataset, i, DetalleFactura)

            literal += itemName & i + 1 & "' value='" & " (" & Configuration.Config_SiteName & " / Reservation: #" & id_reservacion & ")'/>"
            'literal += itemName & i + 1 & "' value='" & " (" & Configuration.Config_SiteName & " / " & DetalleFactura.NombreDisplay.Value & ")'/>"
            'Dim subtotal As Double = (CDbl(DetalleFactura.Precio_Unitario.Value) + CDbl(DetalleFactura.Precio_Unitario_Extra.Value))
            'Dim subtotalIMP As Double = subtotal + (subtotal * impuestos / 100)
            'subtotalIMP = Math.Round(subtotalIMP, 2)
            Dim total As Double = carrito.GetTotal(id_carrito)
            literal += ammount & i + 1 & "' value='" & total & "'/>"
            'literal += ammount & i + 1 & "' value='" & subtotalIMP & "'/>"
            literal += itemNumber & i + 1 & "' value='" & id_reservacion & "'/>"
            literal += quantity & i + 1 & "' value='1'/>"

            'Next
            ltr_values.Text = literal
        End If

    End Sub


End Class

