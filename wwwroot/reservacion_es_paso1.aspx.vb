Imports Orbelink.Entity.Entidades
Imports Orbelink.Entity.Productos
Imports Orbelink.Entity.Reservaciones
Imports Orbelink.DBHandler
Imports Orbelink.Control.Reservaciones
Imports Orbelink.DateAndTime.DateHandler
Imports Orbelink.DateAndTime

Partial Class reservacion_es_paso1
    Inherits Orbelink.FrontEnd6.PageBaseClass

    Private Const id_tipoEntidadUSUARIO As Integer = 2 'Usuario
    Private Const id_producto As Integer = 3 'Habitacion Sencilla
    Private Const horaEntrada As Integer = 12
    Private Const horaSalida As Integer = 11

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Orbelink.DBHandler.LanguageHandler.CurrentLanguage = Orbelink.DBHandler.LanguageHandler.Language.ESPANOL
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Request.QueryString("checkin") IsNot Nothing Then
                Culture = "en-us"
                calendarEntrada.SelectedDate = DateTime.ParseExact(Request.QueryString("checkin"), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)


            End If

            If Request.QueryString("checkout") IsNot Nothing Then
                Culture = "en-us"
                calendarSalida.SelectedDate = DateTime.ParseExact(Request.QueryString("checkout"), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture)

            End If
            btn_reservar1.Visible = True

            lbl_erroFechas.Visible = False


            'trPaquetes2014.Visible = True
            'box_leave.Visible = False
            'box_pickup.Visible = False

            Dim paquete As Integer = Request.QueryString("paquete")
            'paquete = 0


            calendarEntrada.DateMin = Date.Today

            Dim habitacionesDisponibles As Integer = selectHabitaciones(id_producto, 6)

            If ((Request.QueryString("rooms") IsNot Nothing) And (Request.QueryString("rooms") > 0)) Then
                If (habitacionesDisponibles >= Request.QueryString("rooms")) Then
                    cargarHabitacionesDeseadas(Request.QueryString("rooms"))
                Else
                    cargarHabitacionesDeseadas(habitacionesDisponibles)

                End If

            End If




            If ((Request.QueryString("people") IsNot Nothing) And (Request.QueryString("rooms") IsNot Nothing)) Then
                If calculoPersonasXHabitacion(Request.QueryString("rooms"), Request.QueryString("people")) Then
                    If ((Request.QueryString("checkin") IsNot Nothing) And (Request.QueryString("checkout") IsNot Nothing)) Then
                        CalculoPrecio()
                    End If

                End If

            End If

            'cargarCantidadNoches(id_producto, 0)
            'cargarNochesAdicionales()


            ' CODIGO PARA LA PROMOCION DEL DESCUENTO O DE VIAJE EN AVION
            'If Request.QueryString("promo") IsNot Nothing Then
            '    rbtnlst_Promocion.SelectedIndex = Request.QueryString("promo")
            'End If

            'cambiaIdioma()
        End If
    End Sub

    Protected Function validarPrecios(ByVal id_temporada As Integer, ByVal id_producto As Integer, ByVal noches As Integer, ByVal theGridView As GridView) As Boolean
        Dim result As Boolean = True
        For counter As Integer = 0 To theGridView.Rows.Count - 1
            Dim unItem As GridViewRow = theGridView.Rows(counter)

            Dim paquete As Integer = Request.QueryString("paquete")
            Dim numeroadultos As Integer
            Dim adultos As DropDownList = unItem.FindControl("ddl_personas")

            'If paquete <> 0 And paquete <> 4 And paquete <> 6 Then
            '    numeroadultos = 2
            'Else
            numeroadultos = adultos.SelectedValue
            'End If

            Dim controladora As New ControladorReservaciones(connection, Resources.Reservaciones_Resources.ResourceManager)
            If Not controladora.existPrecios(id_temporada, id_producto, noches, numeroadultos) Then
                result = False
                mensajePrecios(numeroadultos)
                Exit For
            End If
        Next
        Return result
    End Function

    Protected Function cargarItemsDisponibles(ByVal id_producto As Integer, ByVal fechaInicio As Date, ByVal fechaFin As Date) As Data.DataTable

        Dim id_ubicacion As Integer = ControladorReservaciones.Config_UbicacionDefault

        Dim controladora As New ControladorReservaciones(connection, Resources.Reservaciones_Resources.ResourceManager)
        Dim dataTable As Data.DataTable = controladora.cargarItemsDisponibles_ByProducto(id_producto, fechaInicio, fechaFin, id_ubicacion)

        Return dataTable
    End Function

    Protected Function agregaItemTemporal(ByVal id_producto As Integer, ByVal noches As Integer, ByVal nochesadicionales As Integer, ByVal habitacionesDisponibles As Data.DataTable, ByVal theGridView As GridView, ByVal usarPrecioTransporte As Boolean, Optional ByVal descuento As String = "") As Double
        Dim result As Double = 0

        For counter As Integer = 0 To theGridView.Rows.Count - 1
            Dim unItem As GridViewRow = theGridView.Rows(counter)
            Dim adultos As DropDownList = unItem.FindControl("ddl_personas")

            Dim paquete As Integer = Request.QueryString("paquete")

            Dim numeroadultos As Integer

            'If paquete <> 0 And paquete <> 4 And paquete <> 6 Then
            '    numeroadultos = 2
            'Else
            numeroadultos = adultos.SelectedValue
            'End If

            Dim item As New Item
            ObjectBuilder.CreateObject(habitacionesDisponibles, counter, item)
            Try
                Dim resultado As Orbelink.Control.Reservaciones.ControladorReservaciones.Resultado_Code
                Dim controladora As New ControladorReservaciones(connection, Resources.Reservaciones_Resources.ResourceManager)
                Dim temporadaVieja As Integer = Session("id_temporadaNueva")
                'paquete = 0
                If paquete = 2 Then
                    Session("id_temporadaNueva") = 14 'Upgrades Package 2015
                Else
                    Session("id_temporadaNueva") = Nothing
                End If

                Dim precio_transporte As Integer = 0

                Dim entrada As Date = calendarEntrada.SelectedDate
                Dim salida As Date = calendarSalida.SelectedDate

                Dim fechaInicio As New Date(entrada.Year, entrada.Month, entrada.Day, 12, 0, 0)
                Dim fechaFin As New Date(salida.Year, salida.Month, salida.Day, 11, 0, 0)

                If rdbtnlist_transporte2014.Visible = True Then

                    Dim preciotransporte As Integer = 0

                    If entrada.Year = 2014 Then
                        preciotransporte = 70

                    ElseIf entrada.Year = 2015 Then
                        preciotransporte = 75

                    End If

                    Select Case rdbtnlist_transporte2014.SelectedValue
                        Case 1
                            precio_transporte = preciotransporte ' Compra de ida
                        Case 2
                            precio_transporte = preciotransporte ' Compra de vuelta
                        Case 3
                            precio_transporte = preciotransporte * 2 ' Compra ida y vuelta
                        Case 4
                            precio_transporte = 0
                    End Select
                End If

                If usarPrecioTransporte = True Then
                    result = result + controladora.AgregarItemTemporal(id_producto, item.ordinal.Value, numeroadultos, 0, True, fechaInicio, fechaFin, 1, noches, nochesadicionales, descuento, Session("id_temporadaNueva"), precio_transporte, 1)
                Else
                    result = result + controladora.AgregarItemTemporal(id_producto, item.ordinal.Value, numeroadultos, 0, True, fechaInicio, fechaFin, 1, noches, nochesadicionales, descuento, Session("id_temporadaNueva"), 0, 1)
                End If


            Catch ex As Exception
                result = result + 0
            End Try
        Next
        Return result
    End Function

    Protected Function agregaItemTemporalIndividual(ByVal id_producto As Integer, ByVal noches As Integer, ByVal nochesadicionales As Integer, ByVal habitacionesDisponibles As Data.DataTable, ByVal theGridViewRow As GridViewRow, ByVal counter As Integer, ByVal usarPrecioTransporte As Boolean, Optional ByVal descuento As String = "") As Double
        Dim result As Double = 0


        Dim unItem As GridViewRow = theGridViewRow
        Dim adultos As DropDownList = unItem.FindControl("ddl_personas")

        Dim paquete As Integer = Request.QueryString("paquete")

        Dim numeroadultos As Integer

        'If paquete <> 0 And paquete <> 4 And paquete <> 6 Then
        '    numeroadultos = 2
        'Else
        numeroadultos = adultos.SelectedValue
        'End If

        Dim item As New Item
        ObjectBuilder.CreateObject(habitacionesDisponibles, counter, item)
        Try
            Dim resultado As Orbelink.Control.Reservaciones.ControladorReservaciones.Resultado_Code
            Dim controladora As New ControladorReservaciones(connection, Resources.Reservaciones_Resources.ResourceManager)
            Dim temporadaVieja As Integer = Session("id_temporadaNueva")
            'paquete = 0
            If paquete = 2 Then
                Session("id_temporadaNueva") = 14 'Upgrades Package 2015
            Else
                Session("id_temporadaNueva") = Nothing
            End If

            Dim precio_transporte As Integer = 0

            Dim entrada As Date = calendarEntrada.SelectedDate
            Dim salida As Date = calendarSalida.SelectedDate

            Dim fechaInicio As New Date(entrada.Year, entrada.Month, entrada.Day, 12, 0, 0)
            Dim fechaFin As New Date(salida.Year, salida.Month, salida.Day, 11, 0, 0)

            If rdbtnlist_transporte2014.Visible = True Then

                Dim preciotransporte As Integer = 0

                If entrada.Year = 2014 Then
                    preciotransporte = 70

                ElseIf entrada.Year = 2015 Then
                    preciotransporte = 75

                End If

                Select Case rdbtnlist_transporte2014.SelectedValue
                    Case 1
                        precio_transporte = preciotransporte ' Compra de ida
                    Case 2
                        precio_transporte = preciotransporte ' Compra de vuelta
                    Case 3
                        precio_transporte = preciotransporte * 2 ' Compra ida y vuelta
                    Case 4
                        precio_transporte = 0
                End Select
            End If

            If usarPrecioTransporte = True Then
                result = controladora.AgregarItemTemporal(id_producto, item.ordinal.Value, numeroadultos, 0, True, fechaInicio, fechaFin, 1, noches, nochesadicionales, descuento, Session("id_temporadaNueva"), precio_transporte, 1)
            Else
                result = controladora.AgregarItemTemporal(id_producto, item.ordinal.Value, numeroadultos, 0, True, fechaInicio, fechaFin, 1, noches, nochesadicionales, descuento, Session("id_temporadaNueva"), 0, 1)
            End If


        Catch ex As Exception
            result = result + 0
        End Try

        Return result
    End Function


    Protected Function selectHabitaciones(ByVal id_producto As Integer, ByVal maximoAReservar As Integer) As Integer
        Dim dataset As Data.DataSet
        Dim Item As New Item
        Dim res As String
        Dim hasta As Integer = 0
        Item.ordinal.ToSelect = True
        Item.Id_producto.Where.EqualCondition(id_producto)

        queryBuilder.Orderby.Add(Item.ordinal, False)
        queryBuilder.From.Add(Item)
        res = queryBuilder.RelationalSelectQuery()

        dataset = connection.executeSelect(res)
        If dataset.Tables.Count > 0 Then
            If dataset.Tables(0).Rows.Count > 0 Then


                If dataset.Tables(0).Rows.Count - 1 > maximoAReservar Then
                    hasta = maximoAReservar
                Else
                    hasta = dataset.Tables(0).Rows.Count - 1
                End If



               
            End If
        End If
        Return hasta
    End Function
    Protected Sub cargarHabitacionesDeseadas(ByVal habitaciones As Integer)
        Dim dataTable As New Data.DataTable
        dataTable.Columns.Add("numero")
        For i As Integer = 0 To habitaciones - 1
            dataTable.Rows.Add(i)
        Next
        gv_ResultadosDisponibles.DataSource = dataTable
        gv_ResultadosDisponibles.DataBind()

        Dim resul_producto As ArrayList = TransformDataTable(dataTable, New Producto)
        Dim resul_Item As ArrayList = TransformDataTable(dataTable, New Item)

       
    End Sub
    

    Protected Function calculoPersonasXHabitacion(ByVal num_habitaciones As Integer, ByVal num_personas As Integer) As Boolean
        Dim resultado As Boolean = True
        Dim habitaciones(num_habitaciones - 1) As Integer
        Dim i = 0

        If ((num_habitaciones <= num_personas)) Then
            If ((num_personas / 4) <= num_habitaciones) Then
                While (num_personas > 0)

                    If i < num_habitaciones Then
                        If habitaciones(i) <= 4 Then
                            habitaciones(i) = habitaciones(i) + 1
                        End If
                        i = i + 1
                        num_personas = num_personas - 1
                    Else
                        i = 0

                    End If


                End While
                For counter As Integer = 0 To gv_ResultadosDisponibles.Rows.Count - 1
                    Dim unItem As GridViewRow = gv_ResultadosDisponibles.Rows(counter)
                    Dim selectPersonas As DropDownList = unItem.FindControl("ddl_personas")

                    selectPersonas.SelectedIndex = habitaciones(counter) - 1

                Next
            Else
                mensajeErrorCantPersonasXHabitacion(2)
                resultado = False
            End If
        Else
            mensajeErrorCantPersonasXHabitacion(1)
            resultado = False
        End If

        Return resultado




    End Function


    Public Sub CalculoPrecio()
        loader.Visible = True

        Dim paquete As Integer = Request.QueryString("paquete")
        'paquete = 0
        If paquete = 2 Then
            CalculoPrecioConPaquete()
        Else
            lbl_ResultadoReservacion.ForeColor = Drawing.Color.Red

            Dim controladora As New ControladorReservaciones(connection, Resources.Reservaciones_Resources.ResourceManager)

            Dim entrada As Date = calendarEntrada.SelectedDate
            Dim salida As Date = calendarSalida.SelectedDate

            Dim fechaInicio As New Date(entrada.Year, entrada.Month, entrada.Day, 12, 0, 0)
            Dim fechaFin As New Date(salida.Year, salida.Month, salida.Day, 11, 0, 0)

            Dim total_de_noches As Integer = controladora.TotalNoches(fechaInicio, fechaFin)
            If total_de_noches > 0 Then
                Dim id_temporada As Integer = controladora.buscarTemporada(fechaInicio, fechaInicio.AddDays(1)).id_Temporada

                Dim habitacionesDeseadas As Integer = gv_ResultadosDisponibles.Rows.Count

                Dim noches As Integer = controladora.NochesSegunTarifas(id_temporada, id_producto, 0, total_de_noches)
                Dim nochesAdicionales As Integer = total_de_noches - noches

               


                If validarPrecios(id_temporada, id_producto, noches, gv_ResultadosDisponibles) Then

                    Dim habitacionesDisponibles As Data.DataTable = cargarItemsDisponibles(id_producto, fechaInicio, fechaFin)
                    If habitacionesDisponibles.Rows.Count >= habitacionesDeseadas Then

                      
                        For counter As Integer = 0 To gv_ResultadosDisponibles.Rows.Count - 1
                            Dim unItem As GridViewRow = gv_ResultadosDisponibles.Rows(counter)
                            Dim lbl_tipo_paquete As Label = unItem.FindControl("lbl_tipo_paquete")
                            'Para cambiar el radio Button del paquete
                            If ((noches = 1) And (nochesAdicionales = 0)) Then

                                lbl_tipo_paquete.Text = "2 days 1 night"
                            ElseIf ((noches = 2) And (nochesAdicionales = 0)) Then
                                lbl_tipo_paquete.Text = "3 days 2 nights"
                            End If
                            Dim lbl_precio_habitacion As Label = unItem.FindControl("lbl_precio_habitacion")
                            lbl_precio_habitacion.Text = "$ " + Convert.ToString(agregaItemTemporalIndividual(id_producto, noches, nochesAdicionales, habitacionesDisponibles, unItem, counter, False))

                        Next


                        Dim precioSintrasporte As Double = agregaItemTemporal(id_producto, noches, nochesAdicionales, habitacionesDisponibles, gv_ResultadosDisponibles, False)
                        Dim precioContranporte As Double = agregaItemTemporal(id_producto, noches, nochesAdicionales, habitacionesDisponibles, gv_ResultadosDisponibles, True)

                        If ((precioSintrasporte <> 0)) Then
                            lbl_precioSinTransporte.Text = precioSintrasporte

                            'exito(ReservacionActual)
                        Else
                            mensajeErrorReservacion()

                        End If
                        If ((precioContranporte <> 0)) Then
                            lbl_precioConTransporte.Text = precioContranporte

                            'exito(ReservacionActual)
                        Else
                            mensajeErrorReservacion()

                        End If
                    Else
                        mensajeCantidadHabitaciones(habitacionesDisponibles.Rows.Count)
                    End If
                End If
            Else
                mensajeErrorFechas()
            End If

        End If
        loader.Visible = False
    End Sub

    Public Sub CalculoPrecioConPaquete()
        loader.Visible = True
        loader.Visible = False
    End Sub


    Public Sub CalculoPrecioConTransporte()
        loader.Visible = True

        Dim paquete As Integer = Request.QueryString("paquete")
        'paquete = 0
        If paquete = 2 Then
            CalculoPrecioConPaquete()
        Else
            lbl_ResultadoReservacion.ForeColor = Drawing.Color.Red

            Dim controladora As New ControladorReservaciones(connection, Resources.Reservaciones_Resources.ResourceManager)

            Dim entrada As Date = calendarEntrada.SelectedDate
            Dim salida As Date = calendarSalida.SelectedDate

            Dim fechaInicio As New Date(entrada.Year, entrada.Month, entrada.Day, 12, 0, 0)
            Dim fechaFin As New Date(salida.Year, salida.Month, salida.Day, 11, 0, 0)

            Dim total_de_noches As Integer = controladora.TotalNoches(fechaInicio, fechaFin)
            If total_de_noches > 0 Then
                Dim id_temporada As Integer = controladora.buscarTemporada(fechaInicio, fechaInicio.AddDays(1)).id_Temporada

                Dim habitacionesDeseadas As Integer = gv_ResultadosDisponibles.Rows.Count

                Dim noches As Integer = controladora.NochesSegunTarifas(id_temporada, id_producto, 0, total_de_noches)
                Dim nochesAdicionales As Integer = total_de_noches - noches




                If validarPrecios(id_temporada, id_producto, noches, gv_ResultadosDisponibles) Then

                    Dim habitacionesDisponibles As Data.DataTable = cargarItemsDisponibles(id_producto, fechaInicio, fechaFin)
                    If habitacionesDisponibles.Rows.Count >= habitacionesDeseadas Then



                        Dim precioContransporte As Double = agregaItemTemporal(id_producto, noches, nochesAdicionales, habitacionesDisponibles, gv_ResultadosDisponibles, True)

                        If ((precioContransporte <> 0)) Then
                            lbl_precioConTransporte.Text = precioContransporte

                            'exito(ReservacionActual)
                        Else
                            mensajeErrorReservacion()

                        End If
                    Else
                        mensajeCantidadHabitaciones(habitacionesDisponibles.Rows.Count)
                    End If
                End If
            Else
                mensajeErrorFechas()
            End If

        End If

    End Sub



    Protected Sub mensajeErrorCantPersonasXHabitacion(ByVal codigo_error As Integer)
        If codigo_error = 1 Then
            If Orbelink.DBHandler.LanguageHandler.CurrentLanguage = Orbelink.DBHandler.LanguageHandler.Language.INGLES Then
                lbl_ResultadoHabitaciones.Text = "You have chosen more rooms than they actually needed as the number of people indicated."
            Else
                lbl_ResultadoHabitaciones.Text = "Usted ha seleccionado más habitaciones de las que realmente necesarias según la cantidad de personas indicada."
            End If
        ElseIf codigo_error = 2 Then
            If Orbelink.DBHandler.LanguageHandler.CurrentLanguage = Orbelink.DBHandler.LanguageHandler.Language.INGLES Then
                lbl_ResultadoHabitaciones.Text = "The requested rooms are not enough to accommodate the desired number of persons. Each room can accommodate up to 4 people."
            Else
                lbl_ResultadoHabitaciones.Text = "Las habitaciones solicitadas no son suficientes para hospedar a la cantidad de personas deseadas. Cada habitación puede hospedar un máximo de 4 personas."
            End If
        End If

    End Sub


    Protected Sub mensajePrecios(ByVal capacidadMax As Integer)
        If Orbelink.DBHandler.LanguageHandler.CurrentLanguage = Orbelink.DBHandler.LanguageHandler.Language.INGLES Then
            lbl_ResultadoReservacion.Text = "Sorry there are no rates for : " & capacidadMax & " person(s) in this date."
        Else
            lbl_ResultadoReservacion.Text = "Lo sentimos, no existen tarifas asociadas para: " & capacidadMax & " persona(s) en esta fecha."
        End If
    End Sub

    Protected Sub mensajeCantidadHabitaciones(ByVal habitacionesDisponibles As Integer)
        If habitacionesDisponibles = 0 Then
            If Orbelink.DBHandler.LanguageHandler.CurrentLanguage = Orbelink.DBHandler.LanguageHandler.Language.INGLES Then
                lbl_ResultadoReservacion.Text = "Sorry there are no rooms available for that date"
            Else
                lbl_ResultadoReservacion.Text = "Lo sentimos no quedan habitaciones disponibles para esa fecha "
            End If
        Else
            If Orbelink.DBHandler.LanguageHandler.CurrentLanguage = Orbelink.DBHandler.LanguageHandler.Language.INGLES Then
                lbl_ResultadoReservacion.Text = "Sorry there are only " & habitacionesDisponibles & " availables rooms for this date"
            Else
                lbl_ResultadoReservacion.Text = "Lo sentimos solo quedan " & habitacionesDisponibles & " habitaciones disponibles para esa fecha "
            End If
        End If

    End Sub

    Protected Sub mensajeErrorFechas()
        If Orbelink.DBHandler.LanguageHandler.CurrentLanguage = Orbelink.DBHandler.LanguageHandler.Language.INGLES Then
            lbl_ResultadoReservacion.Text = "The final date has to be greater than the start date"
        Else
            lbl_ResultadoReservacion.Text = "La fecha de salida debe ser mayor a la fecha de entrada"
        End If
    End Sub

    Protected Sub mensajeErrorReservacion()
        If Orbelink.DBHandler.LanguageHandler.CurrentLanguage = Orbelink.DBHandler.LanguageHandler.Language.INGLES Then
            lbl_ResultadoReservacion.Text = "Error on the reservation"
        Else
            lbl_ResultadoReservacion.Text = "Error al registrar la reservación"
        End If
    End Sub

    Protected Sub exito(ByVal ReservacionActual As Integer)
        'Dim controladoraReservaciones As New Orbelink.Control.Reservaciones.ControladorReservaciones(connection, Resources.Reservaciones_Resources.ResourceManager)
        'controladoraReservaciones.Emails(ReservacionActual)
        Session("id_reservacion") = ReservacionActual
        Response.Redirect("shoppingCart_en.aspx?id_reservacion=" & Session("id_reservacion") & "&termino=" & 0)
    End Sub

    Protected Sub rdbtnlist_transporte2014_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles rdbtnlist_transporte2014.SelectedIndexChanged
        If rdbtnlist_transporte2014.SelectedValue.Length > 0 Then
            CalculoPrecioConTransporte()
        End If
    End Sub



    Protected Sub txtDateEntrada_TextChanged(sender As Object, e As System.EventArgs) Handles txtDateEntrada.TextChanged
        If txtDateEntrada.Text.Length > 0 Then
            CalculoPrecio()
        End If
    End Sub


    Protected Sub txtDateSalida_TextChanged(sender As Object, e As System.EventArgs) Handles txtDateSalida.TextChanged
        If txtDateSalida.Text.Length > 0 Then
            CalculoPrecio()
        End If
    End Sub


   

    Protected Sub gv_ResultadosDisponibles_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gv_ResultadosDisponibles.RowCommand
        If (e.CommandName = "borrarHabitacion") Then
            ' Retrieve the row index stored in the CommandArgument property.
            Dim index As Integer = Convert.ToInt32(e.CommandArgument)


            Dim contador As Integer = 0
            Dim arrayPrecio(gv_ResultadosDisponibles.Rows.Count - 1) As Integer
            Dim arrayPaquete(gv_ResultadosDisponibles.Rows.Count - 1) As String


            For Each m_row As GridViewRow In gv_ResultadosDisponibles.Rows
                If m_row.RowIndex <> index Then
                    Dim ddlpersonas As DropDownList = gv_ResultadosDisponibles.Rows(m_row.RowIndex).FindControl("ddl_personas")
                    Dim Lblpaquete As Label = gv_ResultadosDisponibles.Rows(m_row.RowIndex).FindControl("lbl_tipo_paquete")
                    Dim paquete As String = Lblpaquete.text
                    arrayPrecio(contador) = ddlpersonas.SelectedValue
                    arrayPaquete(contador) = paquete
                    contador = contador + 1
                End If


            Next

            cargarHabitacionesDeseadas(contador)

            Dim contador2 As Integer = 0

            For Each m_row As GridViewRow In gv_ResultadosDisponibles.Rows
                Dim ddlPersonas As DropDownList = gv_ResultadosDisponibles.Rows(m_row.RowIndex).FindControl("ddl_personas")
                Dim Lblpaquete As Label = gv_ResultadosDisponibles.Rows(m_row.RowIndex).FindControl("lbl_tipo_paquete")
                ddlPersonas.SelectedValue = arrayPrecio(contador2)
                Lblpaquete.Text = arrayPaquete(contador2)
                contador2 = contador2 + 1
            Next

            CalculoPrecio()

            'Metodo alterno

            'Dim index As Integer = Convert.ToInt32(e.CommandArgument)

            '' Retrieve the row that contains the button 
            '' from the Rows collection.


            'Dim dataTable As New Data.DataTable
            'dataTable.Columns.Add("delete")
            'dataTable.Columns.Add("descripcion")
            'dataTable.Columns.Add("tipo_paquete")
            'dataTable.Columns.Add("personas")
            'dataTable.Columns.Add("precio")




            'For Each m_row As GridViewRow In gv_ResultadosDisponibles.Rows
            '    If m_row.RowIndex <> index Then

            '        dataTable.Rows.Add(m_row)
            '    End If

            'Next

            'gv_ResultadosDisponibles.DataSource = dataTable
            'gv_ResultadosDisponibles.DataBind()



        End If
    End Sub


   
    Protected Sub add_room_Click(sender As Object, e As System.EventArgs) Handles add_room.Click
        


        Dim contador As Integer = 0
        Dim arrayPrecio(gv_ResultadosDisponibles.Rows.Count - 1) As Integer
        Dim arrayPaquete(gv_ResultadosDisponibles.Rows.Count - 1) As String


        For Each m_row As GridViewRow In gv_ResultadosDisponibles.Rows

            Dim ddlpersonas As DropDownList = gv_ResultadosDisponibles.Rows(m_row.RowIndex).FindControl("ddl_personas")
            Dim Lblpaquete As Label = gv_ResultadosDisponibles.Rows(m_row.RowIndex).FindControl("lbl_tipo_paquete")
            Dim paquete As String = Lblpaquete.Text
            arrayPrecio(contador) = ddlpersonas.SelectedValue
            arrayPaquete(contador) = paquete
            contador = contador + 1


        Next

        cargarHabitacionesDeseadas(contador + 1)

        Dim contador2 As Integer = 0

        For Each m_row As GridViewRow In gv_ResultadosDisponibles.Rows
            If (contador2 < arrayPaquete.Length) Then
                Dim ddlPersonas As DropDownList = gv_ResultadosDisponibles.Rows(m_row.RowIndex).FindControl("ddl_personas")
                Dim Lblpaquete As Label = gv_ResultadosDisponibles.Rows(m_row.RowIndex).FindControl("lbl_tipo_paquete")
                ddlPersonas.SelectedValue = arrayPrecio(contador2)
                Lblpaquete.Text = arrayPaquete(contador2)
                contador2 = contador2 + 1
            End If
            
        Next

        CalculoPrecio()
    End Sub
End Class
