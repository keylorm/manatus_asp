Imports Microsoft.VisualBasic
Imports Orbelink.DBHandler
Imports Orbelink.Entity.Acciones
Imports Orbelink.Entity.Facturas
Imports Orbelink.Entity.Entidades
Imports Orbelink.Entity.Productos
Imports Orbelink.Control.Acciones


Namespace Orbelink.Control.Facturas
    Public Class FacturaMailer

        Dim _connString As String

        Sub New(ByVal theConnectionString As String)
            _connString = theConnectionString
        End Sub

        Public Function EnviarEmailSegunEstado(ByVal id_Factura As String, Optional ByVal esReservacion As Boolean = False, Optional ByVal id_reservacion As Integer = 0, Optional ByVal ReservacionFechaInicio As String = "", Optional ByVal ReservacionFechaFin As String = "", Optional ByVal ReservacionComentarios As String = "", Optional ByVal solicitud As Boolean = False) As Boolean

            Dim ds As Data.DataSet
            Dim entidad As New Entidad()
            Dim Ubicacion As New Ubicacion
            Ubicacion.Nombre.ToSelect = True
            Dim result As Boolean = False
            entidad.NombreDisplay.ToSelect = True
            entidad.Email.ToSelect = True
            If esReservacion Then
                entidad.Telefono.ToSelect = True
                entidad.Celular.ToSelect = True
            End If
            Dim factHandler As New FacturasHandler(_connString)
            Dim factura As Factura = factHandler.ConsultarFactura(id_Factura)
            entidad.Id_entidad.Where.EqualCondition(factura.Id_Comprador.Value)

            Dim connection As New SQLServer(_connString)
            Dim querybuilder As New QueryBuilder()
            querybuilder.Join.EqualCondition(entidad.Id_Ubicacion, Ubicacion.Id_ubicacion)
            querybuilder.From.Add(entidad)
            querybuilder.From.Add(Ubicacion)

            ds = connection.executeSelect(querybuilder.RelationalSelectQuery)

            If ds.Tables.Count > 0 Then
                If ds.Tables(0).Rows.Count > 0 Then
                    ObjectBuilder.CreateObject(ds.Tables(0), 0, entidad)
                    ObjectBuilder.CreateObject(ds.Tables(0), 0, Ubicacion)
                    Try
                        Dim textoEmail As String = ""
                        Dim textoEmailAdmin As String = ""
                        If esReservacion Then
                            textoEmail = EmailFactura(factHandler, factura, id_reservacion)
                            textoEmailAdmin = EmailFactura(factHandler, factura, id_reservacion, True)
                        Else
                            textoEmail = EmailFactura(factHandler, factura)
                            textoEmailAdmin = EmailFactura(factHandler, factura, True)
                        End If
                        Dim items As String = itemsEmail(factHandler, factura)
                        textoEmail += items
                        textoEmailAdmin += items
                        If textoEmail.Length > 0 Then
                            Dim body As String = ""
                            Dim bodyAdmin As String = ""
                            Try
                                'body = Orbelink.Helpers.Mailer.RequestContent_ByURL(Configuration.Config_WebSite_LocalhostRoot & "emails.aspx?tipo=5")
                                body = Orbelink.Helpers.Mailer.RequestContent_ByURL(Configuration.Config_WebsiteRoot & "emails.aspx?tipo=5")
                                bodyAdmin = body
                            Catch ex As Exception

                            End Try
                            'Dim Shippings_Address As Shippings_Address = factHandler.ConsultaShippingAddressEntidad(factura.id_ShippingAddress.Value)
                            'textoEmail += getShippings_Address(Shippings_Address, factura)

                            If esReservacion Then
                                Dim datos As String = DatosReservacion(entidad, Ubicacion, ReservacionFechaInicio, ReservacionFechaFin, ReservacionComentarios, factura.id_TipoFactura.Value)
                                textoEmail += datos
                                textoEmailAdmin += datos

                            End If
                            body = body.Replace("Factura_Replace", textoEmail)
                            bodyAdmin = bodyAdmin.Replace("Factura_Replace", textoEmailAdmin)

                            body = body.Replace("Usuario_Replace", entidad.NombreDisplay.Value)
                            bodyAdmin = bodyAdmin.Replace("Usuario_Replace", "Administrator")

                            If Orbelink.DBHandler.LanguageHandler.CurrentLanguage = Orbelink.DBHandler.LanguageHandler.Language.INGLES Then
                                body = body.Replace("Estimado (a)", "Dear")
                                bodyAdmin = bodyAdmin.Replace("Estimado (a)", "Dear")
                            End If
 
                            Dim subject As String = MensajeSubjectEstado(id_reservacion, factura.Estado_Factura.Value)
                            'Usuario
                            If Not solicitud Then
                                result = Orbelink.Helpers.Mailer.SendOneMail(Configuration.Config_DefaultSenderEmail, Configuration.Config_SiteName, entidad.Email.Value, entidad.NombreDisplay.Value, subject, body)
                            End If

                            'Administradores
                            ''Configuration.Config_DefaultAdminEmail

                            'result = Orbelink.Helpers.Mailer.SendOneMail(Configuration.Config_DefaultSenderEmail, entidad.NombreDisplay.Value, "rsansoc@manatuscostarica.com", Configuration.Config_SiteName, subject, bodyAdmin)
                            'result = Orbelink.Helpers.Mailer.SendOneMail(Configuration.Config_DefaultSenderEmail, entidad.NombreDisplay.Value, Configuration.Config_DefaultAdminEmail, Configuration.Config_SiteName, subject, bodyAdmin)
                            'result = Orbelink.Helpers.Mailer.SendOneMail(Configuration.Config_DefaultSenderEmail, entidad.NombreDisplay.Value, "joseortiz@orbelink.com", "Jose Ortiz", subject, bodyAdmin)
                            'result = Orbelink.Helpers.Mailer.SendOneMail(Configuration.Config_DefaultSenderEmail, entidad.NombreDisplay.Value, "fabiola@orbelink.com", "Fabiola Chaves", subject, bodyAdmin)
                            result = Orbelink.Helpers.Mailer.SendOneMail(Configuration.Config_DefaultSenderEmail, entidad.NombreDisplay.Value, "keylor@orbelink.com", "Luis Alonso", subject, bodyAdmin)
                        End If
                    Catch ex As Exception
                        result = False
                    End Try
                End If
            End If
            Return result
        End Function

        Protected Function DatosReservacion(ByVal Entidad As Entidad, ByVal Ubicacion As Ubicacion, ByVal ReservacionFechaInicio As String, ByVal ReservacionFechaFin As String, ByVal ReservacionComentarios As String, Optional ByVal id_tipoFactura As Integer = 0) As String
            Dim textoEmail As String = ""
            If Orbelink.DBHandler.LanguageHandler.CurrentLanguage = Orbelink.DBHandler.LanguageHandler.Language.INGLES Then
                textoEmail += "<p>The reservation is from: " & ReservacionFechaInicio & " to " & ReservacionFechaFin
                textoEmail += "<p>Client: " & Entidad.NombreDisplay.Value & "</p>"
                textoEmail += "<p>Email: " & Entidad.Email.Value & "</p>"
                textoEmail += "<p>Country: " & Ubicacion.Nombre.Value & "</p>"
                textoEmail += "<p>Phone: " & Entidad.Telefono.Value & "</p>"
                If ReservacionComentarios.Length > 0 Then
                    textoEmail += "<p>Observations: " & ReservacionComentarios & "</p>"

                End If

                If id_tipoFactura = 1 Then
                    textoEmail += "<p> The transaction was made using Paypal</p>"
                End If
                If id_tipoFactura = 2 Then
                    textoEmail += "<p> The transaction was made using BNCR (Banco Nacional de Costa Rica)</p>"
                End If

            Else
                textoEmail += "<p>La fecha de reservación es: " & ReservacionFechaInicio & " a " & ReservacionFechaFin
                textoEmail += "<p>Cliente: " & Entidad.NombreDisplay.Value & "</p>"
                textoEmail += "<p>Email: " & Entidad.Email.Value & "</p>"
                textoEmail += "<p>País: " & Ubicacion.Nombre.Value & "</p>"
                textoEmail += "<p>Teléfono: " & Entidad.Telefono.Value & "</p>"
                If ReservacionComentarios.Length > 0 Then
                    textoEmail += "<p>Observaciones: " & ReservacionComentarios & "</p>"
                End If

                If id_tipoFactura = 1 Then
                    textoEmail += "<p> La transacción se realizó por medio de Paypal</p>"
                End If
                If id_tipoFactura = 2 Then
                    textoEmail += "<p> La transacción se realizó por medio de BNCR (Banco Nacional de Costa Rica)</p>"
                End If

            End If
            Return textoEmail
        End Function

        Public Function MensajeSubjectEstado(ByVal id_reservacion As Integer, ByVal estadoActual As FacturasHandler.EstadoFactura) As String

            Dim body As String = ""
            If Orbelink.DBHandler.LanguageHandler.CurrentLanguage = Orbelink.DBHandler.LanguageHandler.Language.INGLES Then
                Select Case estadoActual
                    Case FacturasHandler.EstadoFactura.Transaction_Successful
                        body = "Order Reserved"
                    Case FacturasHandler.EstadoFactura.Transaction_Unsuccessful
                        body = "Order Cancelled"
                    Case FacturasHandler.EstadoFactura.Approved_Cart
                        body = "New Order"
                End Select
                body += " " & Configuration.Config_SiteName & " Order ID: " & id_reservacion
            Else
                Select Case estadoActual
                    Case FacturasHandler.EstadoFactura.Transaction_Successful
                        body = "Orden Reservada"
                    Case FacturasHandler.EstadoFactura.Transaction_Unsuccessful
                        body = "Orden Cancelada"
                    Case FacturasHandler.EstadoFactura.Approved_Cart
                        body = "Nueva Orden"
                End Select
                body += " " & Configuration.Config_SiteName & " Orden #: " & id_reservacion
            End If


            Return body
        End Function

        Private Function EmailFactura(ByVal factHandler As FacturasHandler, ByVal laFactura As Factura, Optional ByVal id_reservacion As Integer = 0, Optional ByVal esAdmin As Boolean = False) As String
            Dim texto As String = ""
            Dim estado As FacturasHandler.EstadoFactura = laFactura.Estado_Factura.Value
            If estado = FacturasHandler.EstadoFactura.Transaction_Successful Or estado = FacturasHandler.EstadoFactura.Transaction_Unsuccessful Or estado = FacturasHandler.EstadoFactura.Approved_Cart Then
                If id_reservacion > 0 Then
                    texto = MensajeSegunEstado(id_reservacion, estado, esAdmin)
                Else
                    texto = MensajeSegunEstado(laFactura.Id_Factura.Value, estado, esAdmin)
                End If
            End If
            Return texto
        End Function

        Private Function itemsEmail(ByVal factHandler As FacturasHandler, ByVal laFactura As Factura) As String
            Dim detalle_factura As New DetalleFactura
            Dim ds As Data.DataTable = factHandler.GetItems(laFactura.Id_Factura.Value, detalle_factura)
            Dim total As Double = laFactura.Total.Value
            Dim body As New StringBuilder
            If ds.Rows.Count > 0 Then

                If Orbelink.DBHandler.LanguageHandler.CurrentLanguage = Orbelink.DBHandler.LanguageHandler.Language.INGLES Then
                    body.Append("<p>Order Details:</p>")
                    body.Append("<table border='0' width='100%'>")
                    body.Append("<tr>")
                    body.Append("<td width='40%' align='left'><strong>Service</strong></td>")
                    body.Append("<td width='15%' align='center'><strong>Rooms</strong></td>")
                    body.Append("<td width='15%' align='center'><strong>Service Amount</strong></td>")
                    body.Append("<td width='15%' align='center'><strong>Additional Night(s) Amount</strong></td>")
                    body.Append("<td width='15%' align='center'><strong>Discount</strong></td>")
                    body.Append("<td width='15%' align='right'><strong>Subtotal</strong></td>")
                Else
                    body.Append("<p>Detalle de la Orden:</p>")
                    body.Append("<table border='0' width='100%'>")
                    body.Append("<tr>")
                    body.Append("<td width='40%' align='left'><strong>Servicio</strong></td>")
                    body.Append("<td width='15%' align='center'><strong>Habitaciones</strong></td>")
                    body.Append("<td width='15%' align='center'><strong>Monto Venta</strong></td>")
                    body.Append("<td width='15%' align='center'><strong>Monto Noches Adicionales</strong></td>")
                    body.Append("<td width='15%' align='right'><strong>Descuento</strong></td>")
                    body.Append("<td width='15%' align='right'><strong>Subtotal</strong></td>")
                End If

                body.Append("</tr>")

                For i As Integer = 0 To ds.Rows.Count - 1
                    ObjectBuilder.CreateObject(ds, i, detalle_factura)
                    ' ObjectBuilder.CreateObject(ds, i, Producto)

                    body.Append("<td align='left'>")
                    body.Append(detalle_factura.NombreDisplay.Value)
                    body.Append("</td><td align='center'>")
                    body.Append(detalle_factura.Cantidad.Value)
                    body.Append("</td><td align='center'>")
                    body.Append("$" & detalle_factura.Precio_Unitario.Value)
                    body.Append("</td><td align='center'>")
                    body.Append("$" & detalle_factura.Precio_Unitario_Extra.Value)
                    body.Append("</td><td align='center'>")
                    body.Append("$" & detalle_factura.Descuento.Value)
                    body.Append("</td><td align='right'>")
                    body.Append("$" & detalle_factura.MontoVenta.Value)
                    body.Append("</td> </tr>")
                Next

                body.Append("<tr>")
                body.Append("<td></td>")
                body.Append("<td></td>")
                body.Append("<td></td>")
                body.Append("<td></td>")
                body.Append("<td></td>")
                body.Append("<td align='right'><strong>TOTAL: ")
                body.Append("$" & total)
                body.Append("</strong></td>")
                body.Append("</tr>")
                body.Append("</table>")


            End If
            'Dim Shippings_Address As Shippings_Address = factHandler.ConsultaShippingAddressEntidad(laFactura.id_ShippingAddress.Value)
            'If Shippings_Address IsNot Nothing Then
            '    body.Append("")
            'End If
            Return body.ToString
        End Function

        Private Function getShippings_Address(ByVal Shippings_Address As Shippings_Address, ByVal laFactura As Factura) As String
            Dim body As New StringBuilder
            If Shippings_Address IsNot Nothing Then
                body.Append("<p><b> Detalle de Envío:</b></p>")
                body.Append("<p> ")
                body.Append("<b> Nombre: </b>" & Shippings_Address.Nombre.Value)
                body.Append("<br/><b> Email: </b>" & Shippings_Address.Email.Value)
                body.Append("<br/><b> Teléfono: </b>" & Shippings_Address.Telefono.Value)
                body.Append("<br/><b> Provincia: </b>" & Shippings_Address.Region.Value)
                body.Append("<br/><b> Ciudad: </b>" & Shippings_Address.Ciudad.Value)
                body.Append("<br/><b> Dirección: </b>" & Shippings_Address.Direccion.Value)
                body.Append("<br/><b>Código Postal: </b>" & Shippings_Address.Codigo_Postal.Value)
                body.Append("<br/><b>Enviar Por: </b>" & laFactura.Enviar_Por.Value)
                body.Append("</p> ")
            End If
            Return body.ToString
        End Function

        Private Function MensajeSegunEstado(ByVal id_Carrito As Integer, ByVal estadoActual As Integer, ByVal esAdmin As Boolean) As String
            Dim mensaje As String = ""

            If esAdmin Then
                If Orbelink.DBHandler.LanguageHandler.CurrentLanguage = Orbelink.DBHandler.LanguageHandler.Language.INGLES Then

                    If estadoActual = FacturasHandler.EstadoFactura.Transaction_Successful Then
                        mensaje += "<p><span style='color:Green'>Congratulations there has been a SUCCESSFUL transaction. </span>  Order #" & id_Carrito & "</p>"
                    End If
                    If estadoActual = FacturasHandler.EstadoFactura.Transaction_Unsuccessful Then
                        mensaje += "<p><span style='color:Red'>There has been a NOT SUCCESSFUL transaction. <br/><span style='color:Red'>NO PAYMENT HAVE BEEN MADE.</span> </span> <br/>  Order # " & id_Carrito & "</p>"
                    End If

                    If estadoActual = FacturasHandler.EstadoFactura.Approved_Cart Then
                        mensaje += "<p>THIS IS A RESERVATION REQUEST FORM .<br/> <span style='color:Red'> NO PAYMENT HAVE BEEN MADE.</span><br/>   Order # " & id_Carrito & "</p>"
                    End If
                Else

                    If estadoActual = FacturasHandler.EstadoFactura.Transaction_Successful Then
                        mensaje += "<p><span style='color:Green'>Felicidades se ha registrado una transacción EXITOSA. </span> <br/>  Orden # " & id_Carrito & "</p>"
                    End If
                    If estadoActual = FacturasHandler.EstadoFactura.Transaction_Unsuccessful Then
                        mensaje += "<p><span style='color:Red'>Se ha registrado una transacción NO EXITOSA. <br/>NO SE HA REALIZADO NINGUN PAGO </span> <br/>  Orden # " & id_Carrito & "</p>"
                    End If

                    If estadoActual = FacturasHandler.EstadoFactura.Approved_Cart Then
                        mensaje += "<p>Esto es una solicitud de reservación .<br/><span style='color:Red'>NO SE HA REALIZADO NINGUN PAGO </span> <br/>  Orden # " & id_Carrito & "</p>"
                    End If
                End If
            Else
                If Orbelink.DBHandler.LanguageHandler.CurrentLanguage = Orbelink.DBHandler.LanguageHandler.Language.INGLES Then

                    If estadoActual = FacturasHandler.EstadoFactura.Transaction_Successful Then
                        mensaje = "<p>Thank you for buying in " & Configuration.Config_SiteName & "</p>"
                        mensaje += "<p>Congratulations your transacction was successfull.   Order #" & id_Carrito & "</p>"
                    End If
                    If estadoActual = FacturasHandler.EstadoFactura.Transaction_Unsuccessful Then
                        mensaje += "<p>We are sorry, we can not fulfill your reservation at this time.  You can contact us to: <a href='mailto:" & Configuration.Config_DefaultAdminEmail & "'>" & Configuration.Config_DefaultAdminEmail & "</a>.<br/>   Order # " & id_Carrito & "</p>"
                    End If

                    If estadoActual = FacturasHandler.EstadoFactura.Approved_Cart Then
                        mensaje += "<p>Your transacction is pending to confirm. You can contact us to: <a href='mailto:" & Configuration.Config_DefaultAdminEmail & "'>" & Configuration.Config_DefaultAdminEmail & "</a>.<br/>   Order # " & id_Carrito & "</p>"
                    End If
                Else

                    If estadoActual = FacturasHandler.EstadoFactura.Transaction_Successful Then
                        mensaje = "<p>Gracias por comprar en " & Configuration.Config_SiteName & "</p>"
                        mensaje += "<p>Felicidades su transacción fue exitosa.   Orden #" & id_Carrito & "</p>"
                    End If
                    If estadoActual = FacturasHandler.EstadoFactura.Transaction_Unsuccessful Then
                        mensaje += "<p>Lo sentimos.  Su transacción no fue exitosa. Puede contactarnos a: <a href='mailto:" & Configuration.Config_DefaultAdminEmail & "'>" & Configuration.Config_DefaultAdminEmail & "</a>.<br/>   Orden # " & id_Carrito & "</p>"
                    End If

                    If estadoActual = FacturasHandler.EstadoFactura.Approved_Cart Then
                        mensaje += "<p>Su transacción está pendiente por confirmar. Puede contactarnos a: <a href='mailto:" & Configuration.Config_DefaultAdminEmail & "'>" & Configuration.Config_DefaultAdminEmail & "</a>.<br/>   Orden # " & id_Carrito & "</p>"
                    End If
                End If
            End If
            Return mensaje
        End Function

        'Acciones 
        Public Function EnviarEmailAccion(ByVal id_Factura As String, ByVal id_Accion As String) As Boolean
            Dim result As Boolean = False

            Dim factHandler As New FacturasHandler(_connString)
            Dim factura As Factura = factHandler.ConsultarFactura(id_Factura)
            If factura.Estado_Factura.Value = FacturasHandler.EstadoFactura.Transaction_Successful Then
                Dim ds As Data.DataSet
                Dim entidad As New Entidad()
                entidad.NombreDisplay.ToSelect = True
                entidad.Email.ToSelect = True
                Entidad.Id_entidad.Where.EqualCondition(factura.Id_Comprador.Value)

                Dim connection As New SQLServer(_connString)
                Dim querybuilder As New QueryBuilder()
                ds = connection.executeSelect(querybuilder.SelectQuery(entidad))

                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        ObjectBuilder.CreateObject(ds.Tables(0), 0, entidad)
                        Try
                            Dim Shippings_Address As Shippings_Address = factHandler.ConsultaShippingAddressEntidad(factura.id_ShippingAddress.Value)

                            Dim textoEmail As String = EmailAccion(entidad.NombreDisplay.Value, Shippings_Address.Nombre.Value, id_Accion, Shippings_Address.Direccion_Alterna.Value)
                            If textoEmail.Length > 0 Then
                                Dim body As String = ""
                                Try
                                    'body = Orbelink.Helpers.Mailer.RequestContent_ByURL(Configuration.Config_WebSite_LocalhostRoot & "emails.aspx?tipo=5")
                                    body = Orbelink.Helpers.Mailer.RequestContent_ByURL(Configuration.Config_WebsiteRoot & "emails.aspx?tipo=5")

                                Catch ex As Exception

                                End Try
                                body = body.Replace("Factura_Replace", textoEmail)
                                body = body.Replace("Usuario_Replace", Shippings_Address.Nombre.Value)

                                result = Orbelink.Helpers.Mailer.SendOneMail(Configuration.Config_DefaultSenderEmail, entidad.NombreDisplay.Value, Shippings_Address.Email.Value, Shippings_Address.Nombre.Value, "Su amigo le ha regalado un producto de: " & Configuration.Config_SiteName, body)
                            End If
                        Catch ex As Exception
                            result = False
                        End Try
                    End If
                End If
            End If
            Return result
        End Function
        Private Function EmailAccion(ByVal remitente As String, ByVal destinatario As String, ByVal id_accion As Integer, ByVal mensaje As String) As String
            Dim body As New StringBuilder

            Dim AccionesHandler As AccionesHandler = New AccionesHandler(_connString)
            Dim accion As Accion = AccionesHandler.ConsultarAccion(id_accion)
            body.Append("<p>Muchas Felicidades :<b> " & destinatario & "!<br/> " & remitente & "</b> le ha enviado un certificado de regalo </p>")
            If mensaje.Length > 0 Then
                body.Append("<br/>Mensaje de " & remitente & ": " & mensaje)
            End If

            body.Append("<br/>Necesitará los siguientes datos:")
            body.Append("<br/><b>El regalo es intercambiable por: </b> " & FormatCurrency(accion.Valor.Value, 2))
            body.Append("<br/><b>Válido hasta: </b> " & FormatDateTime(accion.FechaCaducidad.ValueLocalized, DateFormat.ShortDate))
            body.Append("<br/><b>Código de Acción: </b> " & accion.CodigoAccion.Value)
            body.Append("<br/><b>Código de Activación: </b> " & accion.CodigoValidacion.Value)


            Return body.ToString
        End Function
        Public Function EnviarEmailRegalo(ByVal id_Factura As String, ByVal N_producto As String) As Boolean
            Dim result As Boolean = False
            Dim factHandler As New FacturasHandler(_connString)
            Dim factura As Factura = factHandler.ConsultarFactura(id_Factura)
            If factura.Estado_Factura.Value = FacturasHandler.EstadoFactura.Transaction_Successful Then
                Dim ds As Data.DataSet
                Dim entidad As New Entidad()

                entidad.NombreDisplay.ToSelect = True
                entidad.Email.ToSelect = True
                Entidad.Id_entidad.Where.EqualCondition(factura.Id_Comprador.Value)

                Dim connection As New SQLServer(_connString)
                Dim querybuilder As New QueryBuilder()
                ds = connection.executeSelect(querybuilder.SelectQuery(entidad))

                If ds.Tables.Count > 0 Then
                    If ds.Tables(0).Rows.Count > 0 Then
                        ObjectBuilder.CreateObject(ds.Tables(0), 0, entidad)
                        Try
                            Dim Shippings_Address As Shippings_Address = factHandler.ConsultaShippingAddressEntidad(factura.id_ShippingAddress.Value)
                            If Shippings_Address.Email.Value <> entidad.Email.Value Then
                                Dim textoEmail As String = EmailRegalo(entidad.NombreDisplay.Value, Shippings_Address.Nombre.Value, Shippings_Address.Direccion_Alterna.Value, N_producto, id_Factura)
                                If textoEmail.Length > 0 Then
                                    Dim body As String = ""
                                    Try
                                        'body = Orbelink.Helpers.Mailer.RequestContent_ByURL(Configuration.Config_WebSite_LocalhostRoot & "emails.aspx?tipo=5")
                                        body = Orbelink.Helpers.Mailer.RequestContent_ByURL(Configuration.Config_WebsiteRoot & "emails.aspx?tipo=5")

                                    Catch ex As Exception

                                    End Try
                                    body = body.Replace("Factura_Replace", textoEmail)
                                    body = body.Replace("Usuario_Replace", Shippings_Address.Nombre.Value)

                                    result = Orbelink.Helpers.Mailer.SendOneMail(Configuration.Config_DefaultSenderEmail, entidad.NombreDisplay.Value, Shippings_Address.Email.Value, Shippings_Address.Nombre.Value, "Su amigo le ha regalado un producto de: " & Configuration.Config_SiteName, body)
                                End If
                            End If
                        Catch ex As Exception
                            result = False
                        End Try
                    End If
                End If
            End If
            Return result
        End Function
        Private Function EmailRegalo(ByVal remitente As String, ByVal destinatario As String, ByVal mensaje As String, ByVal N_producto As String, ByVal id_factura As Integer) As String
            Dim body As New StringBuilder

            body.Append("<p>Muchas Felicidades :<b> " & destinatario & "!<br/> " & remitente & "</b> le ha enviado un regalo: " & N_producto & "</p>")
            body.Append("<b>Detalles:</b>")
            If mensaje.Length > 0 Then
                body.Append("<br/>Mensaje de " & remitente & ": " & mensaje)
            End If

            body.Append("<p>Necesitará los siguientes datos:")
            body.Append("<br/>El regalo es: </p>" & N_producto)
            body.Append("<br/>Número de transacción: </p>" & id_factura)

            Return body.ToString
        End Function
    End Class
End Namespace