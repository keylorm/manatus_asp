Imports Orbelink.Control.Facturas
Imports Orbelink.Control.Reservaciones
Partial Class respuestatransaccion
    Inherits Orbelink.FrontEnd6.PageBaseClass


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            Try
                Dim controladora As New ControladorReservaciones(connection, Resources.Reservaciones_Resources.ResourceManager)
                Dim carrito As New FacturasHandler(Configuration.Config_DefaultConnectionString)
                Dim vpos As VPOS = New VPOS
                Dim resultado As VPOS.TransactionResult = vpos.RecibirInfo(Request.Params)


                'Este mensaje es para hacer las pruebas con el banco donde le piden estos datos por cada transaccion 
                'La idea es mostrar esta pagina con estos datos y no hacer redirect como deberia cuando ya esta en produccion

                lbl_mensaje.Text += "<br/><br/>purchaseOperationNumber: " & resultado.purchaseOperationNumber & "<br/>authorizationResult: " & resultado.authorizationResult & "<br/> message:" & resultado.errorMessage & "<br/>authorizationCode: " & resultado.authorizationCode
                lbl_mensaje.Text += "<br/>errorCode" & resultado.errorCode

                If resultado.purchaseOperationNumber > 0 Then
                    

                    If resultado.result = True Then
                        'lbl_mensaje.Text = resultado.message & " " & "Authorization Code:" & resultado.authorizationCode
                        controladora.NuevoEstadoParaReservacion(resultado.purchaseOperationNumber, controladora.BuscarEstadoReservado, Configuration.Config_DefaultIdEntidad, "", True)
                    Else
                        'lbl_mensaje.Text = resultado.message
                        'lbl_mensaje.ForeColor = Drawing.Color.Red
                        controladora.NuevoEstadoParaReservacion(resultado.purchaseOperationNumber, controladora.BuscarEstadoCancelado, Configuration.Config_DefaultIdEntidad, "", True)
                    End If

                    If resultado.language = "EN" Then
                        Orbelink.DBHandler.LanguageHandler.CurrentLanguage = Orbelink.DBHandler.Language.INGLES
                    Else
                        Orbelink.DBHandler.LanguageHandler.CurrentLanguage = Orbelink.DBHandler.Language.ESPANOL
                    End If

                    If controladora.Emails(resultado.purchaseOperationNumber) Then
                        'lbl_mensaje.Text += "<br/><br/>Email Success"
                        'Else
                        'lbl_mensaje.Text += "<br/><br/>Email Error"
                    End If
                End If
                Dim factura As FacturaWebState = New FacturaWebState
                factura.BorrarMyCarrito()

                'Obliga a ver un resultado positivo para pruebas
                resultado.result = True

                If resultado.result = True Then 'Exito
                    If resultado.language = "EN" Then
                        'Response.Redirect("http://booking.manatuscostarica.com/reservacion_en_paso1.aspx?exito=1")
                        Response.Redirect("http://manatus.orbelink.com/reservacion_en_paso1.aspx?exito=1")
                    Else
                        'Response.Redirect("http://booking.manatuscostarica.com/reservacion_es_paso1.aspx?exito=1")
                        Response.Redirect("http://manatus.orbelink.com/reservacion_es_paso1.aspx?exito=1")
                    End If
                Else 'Error

                    If resultado.language = "EN" Then
                        'Response.Redirect("http://booking.manatuscostarica.com/reservacion_en_paso1.aspx?exito=0")
                        Response.Redirect("http://manatus.orbelink.com/reservacion_en_paso1.aspx?exito=0")
                    Else
                        'Response.Redirect("http://booking.manatuscostarica.com/reservacion_es_paso1.aspx?exito=0")
                        Response.Redirect("http://manatus.orbelink.com/reservacion_es_paso1.aspx?exito=0")
                    End If
                End If
            Catch ex As Exception
                lbl_mensaje.Text = "We are sorry, your order couldn´t be confirmed."
            End Try

        End If

       
    End Sub


End Class
