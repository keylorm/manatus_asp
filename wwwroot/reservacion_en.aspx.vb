Imports Orbelink.Entity.Entidades
Imports Orbelink.Entity.Productos
Imports Orbelink.Entity.Reservaciones
Imports Orbelink.DBHandler
Imports Orbelink.Control.Reservaciones
Imports Orbelink.DateAndTime.DateHandler
Imports Orbelink.DateAndTime

Partial Class reservacion_sp
    Inherits Orbelink.FrontEnd6.PageBaseClass

    Private Const id_tipoEntidadUSUARIO As Integer = 2 'Usuario
    Private Const id_producto As Integer = 3 'Habitacion Sencilla
    Private Const horaEntrada As Integer = 12
    Private Const horaSalida As Integer = 11


    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Orbelink.DBHandler.LanguageHandler.CurrentLanguage = Orbelink.DBHandler.LanguageHandler.Language.INGLES
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
            btn_reservar.Visible = True
            lbl_erroFechas.Visible = False
            cambiaIdioma()

            'trPaquetes2014.Visible = True
            box_leave.Visible = False
            box_pickup.Visible = False

            Dim paquete As Integer = Request.QueryString("paquete")
            'paquete = 0
            If paquete = 2 Then
                hyplnk_paquetes.Visible = True
                old_terms.Visible = False

                pnl_terminospaquete.Visible = True
                Panel1.Visible = False

                hyplnk_paquetes.NavigateUrl = "Terms_and_Conditions_Manatus_may_and_june.pdf"
                lbl_paquete.Text = "<br />Free extra Night at Manatus Hotel<br /><br />"
                'trHabitaciones.Attributes.Add("style", "display:table-row")

                lbl_paquete.Visible = True
            Else
                old_terms.Visible = True
                hyplnk_paquetes.Visible = False
                pnl_terminospaquete.Visible = False
                Panel1.Visible = True
                'trHabitaciones.Attributes.Add("style", "display:table-row")

                lbl_paquete.Visible = False

                txtDateSalida.Enabled = True
                calendarSalida.Enabled = True
            End If

            calendarEntrada.DateMin = Date.Today

            selectPaises()
            selectHabitaciones(id_producto, 6)

            If Request.QueryString("rooms") IsNot Nothing Then

                ddl_habitaciones.SelectedValue = Request.QueryString("rooms")
            End If

            If ddl_habitaciones.SelectedValue.Length > 0 Then
                cargarHabitacionesDeseadas(ddl_habitaciones.SelectedValue)
            End If

            'cargarCantidadNoches(id_producto, 0)
            'cargarNochesAdicionales()
            If Request.QueryString("exito") IsNot Nothing Then
                Try
                    Dim exito As Integer = Request.QueryString("exito")
                    cargarPanelExito(exito)
                Catch ex As Exception
                    cargarPanelExito(0)
                End Try
            End If

            ' CODIGO PARA LA PROMOCION DEL DESCUENTO O DE VIAJE EN AVION
            'If Request.QueryString("promo") IsNot Nothing Then
            '    rbtnlst_Promocion.SelectedIndex = Request.QueryString("promo")
            'End If
        End If

        'Placeholders
        txt_nombre.Attributes.Add("placeholder", "Name")
        txt_direccion.Attributes.Add("placeholder", "Address")
        txt_codArea.Attributes.Add("placeholder", "Area Code")
        txt_tel.Attributes.Add("placeholder", "Phone")
        txt_email.Attributes.Add("placeholder", "Email")
        txtDateEntrada.Attributes.Add("placeholder", "Check-In")
        txtDateSalida.Attributes.Add("placeholder", "Check-Out")
        txt_pickup.Attributes.Add("placeholder", "Pick up place")
        txt_leave.Attributes.Add("placeholder", "Drop off place")
        txt_observaciones.Attributes.Add("placeholder", "Details")
    End Sub

    Protected Sub cambiaIdioma()
        Orbelink.DBHandler.LanguageHandler.CurrentLanguage = Orbelink.DBHandler.LanguageHandler.Language.INGLES
        If Orbelink.DBHandler.LanguageHandler.CurrentLanguage = Orbelink.DBHandler.LanguageHandler.Language.INGLES Then
            lbl_nombre.Text = "Name:"
            lbl_dir.Text = "Address:"
            lbl_codigo.Text = "Postal Code:"
            lbl_ubicacion.Text = "Country:"
            lbl_codigoA.Text = "Code Area:"
            lbl_telefono.Text = "Phone"
            lbl_email.Text = "Email:"
            lbl_fechaE.Text = "Check In:"
            lbl_fechaS.Text = "Check Out:"

            lbl_nHabitaciones.Text = "Rooms:"
            calendarEntrada.CultureName = "en-us"
            calendarSalida.CultureName = "en-us"
            lbl_observaciones.Text = "Observations:"
            'gv_ResultadosDisponibles.Columns(1).HeaderText = "Person(s)"

            rfv_nombre.ErrorMessage = "Required Field"
            rfv_codigoA.ErrorMessage = "Required Field"
            rfv_email.ErrorMessage = "Required Field"
            rfv_direccion.ErrorMessage = "Required Field"
            rfv_fecha.ErrorMessage = "Required Field"
            rfv_fecha2.ErrorMessage = "Required Field"
            rfv_Tel.ErrorMessage = "Required Field"
            rev_email.ErrorMessage = "Wrong Format"
            RequiredFieldValidator1.ErrorMessage = "Required Field"
            'cv_codigo.ErrorMessage = "Wrong Format"
            ' cv_tel.ErrorMessage = "Wrong Format"

            'pnl_info1_ing.Visible = True
            'pnl_info2_ing.Visible = True
            'pnl_info3_ing.Visible = True

            'pnl_info1_esp.Visible = False
            'pnl_info2_esp.Visible = False
            'pnl_info3_esp.Visible = False
        Else
            'pnl_info1_ing.Visible = False
            'pnl_info2_ing.Visible = False
            'pnl_info3_ing.Visible = False

            'pnl_info1_esp.Visible = True
            'pnl_info2_esp.Visible = True
            'pnl_info3_esp.Visible = True
            calendarEntrada.CultureName = "es-cr"
        End If
    End Sub
    Protected Sub cargarPanelExito(ByVal exito As Integer)
        If Request.QueryString("exito") IsNot Nothing Then
            btn_reservar.Visible = False
            lbl_ResultadoReservacion.Visible = True
            pnl_contenido.Visible = False
            pnl_exito.Visible = True

            If Request.QueryString("exito") = 1 Then

                Response.Redirect("reservacionExitosa.aspx")
                If Orbelink.DBHandler.LanguageHandler.CurrentLanguage = Orbelink.DBHandler.LanguageHandler.Language.INGLES Then
                    lbl_exito.Text = "<br/><br/>Congratulations!!! The reservation has been recorded correctly.<br/><br/>"
                Else
                    lbl_exito.Text = "<br/><br/>Felicidades!!!  La reservación ha sido registrada correctamente.<br/><br/>"
                End If
                lbl_exito.ForeColor = Drawing.Color.Black
            Else
                lbl_exito.ForeColor = Drawing.Color.Red
                If Request.QueryString("exito") = 0 Then
                    If Orbelink.DBHandler.LanguageHandler.CurrentLanguage = Orbelink.DBHandler.LanguageHandler.Language.INGLES Then
                        lbl_exito.Text = "<br/><br/>Sorry, your transacction was cancelled. Please click <a href='reservacion_en.aspx'>HERE</a> to try again.<br/><br/>"
                    Else
                        lbl_exito.Text = "<br/><br/>Lo sentimos, su transacción fue cancelada.  Por favor haga click <a href='reservacion_sp.aspx'>AQUI</a> para intentar de nuevo.<br/><br/>"
                    End If
                End If
            End If
        Else

        End If
    End Sub
    Protected Sub selectHabitaciones(ByVal id_producto As Integer, ByVal maximoAReservar As Integer)
        Dim dataset As Data.DataSet
        Dim Item As New Item
        Dim res As String

        Item.ordinal.ToSelect = True
        Item.Id_producto.Where.EqualCondition(id_producto)

        queryBuilder.Orderby.Add(Item.ordinal, False)
        queryBuilder.From.Add(Item)
        res = queryBuilder.RelationalSelectQuery()

        dataset = connection.executeSelect(res)
        If dataset.Tables.Count > 0 Then
            If dataset.Tables(0).Rows.Count > 0 Then

                Dim hasta As Integer
                If dataset.Tables(0).Rows.Count - 1 > maximoAReservar Then
                    hasta = maximoAReservar
                End If



                For i As Integer = 0 To hasta - 1

                    ddl_habitaciones.Items.Add(i + 1)
                Next
            End If
        End If
    End Sub
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

        For counter As Integer = 0 To dataTable.Rows.Count - 1
            Dim unItem As GridViewRow = gv_ResultadosDisponibles.Rows(counter)
            Dim lbl_nombre As Label = unItem.FindControl("lbl_nombre")
            Dim lbl_personas As Label = unItem.FindControl("lbl_personas")
            If Orbelink.DBHandler.LanguageHandler.CurrentLanguage = Orbelink.DBHandler.LanguageHandler.Language.INGLES Then
                lbl_nombre.Text = "Room " & counter + 1
                lbl_personas.Text = "Person(s)"
            Else
                lbl_nombre.Text = "Habitación " & counter + 1
                lbl_personas.Text = "Personas"
            End If
        Next
    End Sub
    Protected Sub ddl_habitaciones_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddl_habitaciones.SelectedIndexChanged
        If ddl_habitaciones.SelectedValue.Length > 0 Then
            cargarHabitacionesDeseadas(ddl_habitaciones.SelectedValue)
        End If
    End Sub

    'Entidades
    Protected Sub selectPaises()
        Dim dataset As Data.DataSet
        Dim Ubicacion As New Ubicacion
        Dim res As String
        Ubicacion.Id_ubicacion.ToSelect = True
        Ubicacion.Nombre.ToSelect = True

        queryBuilder.Orderby.Add(Ubicacion.Nombre)
        queryBuilder.From.Add(Ubicacion)
        res = queryBuilder.RelationalSelectQuery()

        dataset = connection.executeSelect(res)
        If dataset.Tables.Count > 0 Then
            If dataset.Tables(0).Rows.Count > 0 Then
                ddl_ubicacion.DataSource = dataset
                ddl_ubicacion.DataValueField = Ubicacion.Id_ubicacion.Name
                ddl_ubicacion.DataTextField = Ubicacion.Nombre.Name
                ddl_ubicacion.DataBind()

            End If
        End If
        ddl_ubicacion.SelectedValue = 1
    End Sub
    Protected Function insertEntidad(ByVal id_tipoEntidad As Integer, ByVal id_ubicacion As Integer, ByVal nombre As String, ByVal apellido As String, ByVal codigoPostal As String, ByVal tel As String, ByVal email As String, ByVal direccion As String) As Boolean
        Dim entidad As Entidad
        Try
            entidad = New Entidad()
            entidad.Id_tipoEntidad.Value = id_tipoEntidad
            entidad.NombreEntidad.Value = nombre
            entidad.NombreDisplay.Value = nombre & " " & apellido
            entidad.Apellido.Value = apellido
            entidad.Apellido2.Value = codigoPostal ' Codigo postal en apellido 2
            entidad.Telefono.Value = tel
            entidad.Id_Ubicacion.Value = id_ubicacion
            entidad.UserName.Value = email
            entidad.Email.Value = email
            entidad.Password.Value = "12345"
            entidad.Descripcion.Value = direccion 'Direccion en descripcion
            entidad.Fecha.ValueLocalized = DateHandler.ToLocalizedDateFromUtc(Date.UtcNow)
            Dim a As String = queryBuilder.InsertQuery(entidad)
            connection.executeInsert(a)
            Return True
        Catch exc As Exception
            'lbl_Errores.Text = exc.Message
            'lbl_Errores.Visible = True
            Return False
        End Try
    End Function
    Protected Function updateEntidad(ByVal id_entidad As Integer, ByVal id_ubicacion As Integer, ByVal nombre As String, ByVal apellido As String, ByVal codigoPostal As String, ByVal tel As String, ByVal email As String, ByVal direccion As String) As Boolean
        Dim result As Boolean = True
        Dim entidad As New Entidad
        Try
            entidad.Fields.UpdateAll()
            entidad.Id_tipoEntidad.ToUpdate = False
            entidad.Id_Ubicacion.Value = id_ubicacion
            entidad.NombreEntidad.Value = nombre
            entidad.NombreDisplay.Value = nombre & " " & apellido
            entidad.Apellido.Value = apellido
            entidad.Apellido2.Value = codigoPostal ' Codigo postal en apellido 2
            If txt_email.ToolTip = email Then
                entidad.UserName.ToUpdate = False
            Else
                entidad.UserName.Value = email
            End If
            If txt_email.ToolTip = email Then
                entidad.Email.ToUpdate = False
            Else
                entidad.Email.Value = email
            End If

            entidad.Password.ToUpdate = False
            entidad.Telefono.Value = tel
            entidad.Descripcion.Value = direccion 'Direccion en descripcion
            entidad.Id_entidad.Where.EqualCondition(id_entidad)
            Dim res As String = queryBuilder.UpdateQuery(entidad)
            If connection.executeUpdate(res) >= 1 Then
                result = True
            Else
                result = False
            End If

            Return result
        Catch exc As Exception
            Return False
        End Try
    End Function
    Protected Function existEmail(ByVal email As String) As Boolean
        Dim exist As Boolean = False
        Dim dataSet As Data.DataSet
        Dim entidad As New Entidad

        entidad.Id_entidad.ToSelect = True
        If email.Length > 0 Then
            entidad.Email.Where.EqualCondition(email)
            queryBuilder.From.Add(entidad)
            dataSet = connection.executeSelect(queryBuilder.RelationalSelectQuery)

            If dataSet.Tables.Count > 0 Then
                If dataSet.Tables(0).Rows.Count > 0 Then
                    ObjectBuilder.CreateObject(dataSet.Tables(0), 0, entidad)
                    txt_nombre.ToolTip = entidad.Id_entidad.Value
                    exist = True

                End If
            End If
        End If

        Return exist
    End Function
    Protected Function existUserName(ByVal username As String, ByVal id_entidad As String) As Boolean
        Dim exist As Boolean = False
        Dim dataSet As Data.DataSet
        Dim entidad As New Entidad

        entidad.Id_entidad.ToSelect = True
        If username.Length > 0 Then
            entidad.UserName.Where.EqualCondition(username)
            queryBuilder.From.Add(entidad)
            dataSet = connection.executeSelect(queryBuilder.RelationalSelectQuery)

            If dataSet.Tables.Count > 0 Then
                If dataSet.Tables(0).Rows.Count > 0 Then
                    ObjectBuilder.CreateObject(dataSet.Tables(0), 0, entidad)
                    If id_entidad.Length > 0 Then   'Si esta update
                        If entidad.Id_entidad.Value <> id_entidad Then
                            exist = True
                        End If
                    Else                            'Esta insert
                        exist = True
                    End If
                End If
            End If
        End If

        Return exist
    End Function
    Protected Function InsertarModificar(ByVal id_tipoEntidad As Integer) As Boolean
        Dim resul As Boolean = True
        Dim entidad As New Entidad
        Dim telefonoConArea As String = "(" & txt_codArea.Text & ")" & txt_tel.Text

        If Not existEmail(txt_email.Text) And Not existUserName(txt_email.Text, 0) Then
            If insertEntidad(id_tipoEntidad, ddl_ubicacion.SelectedValue, txt_nombre.Text, "", txt_codPostal.Text, telefonoConArea, txt_email.Text, txt_direccion.Text) Then
                Dim id_insertado As Integer = connection.lastKey(entidad.TableName, entidad.Id_entidad.Name)
                txt_nombre.ToolTip = id_insertado
                txt_email.ToolTip = txt_email.Text
            Else
                resul = False
                If Orbelink.DBHandler.LanguageHandler.CurrentLanguage = Orbelink.DBHandler.LanguageHandler.Language.INGLES Then
                    lbl_ResultadoReservacion.Text = "Sorry, an error occurred when registering the reservation. Please try again."
                Else
                    lbl_ResultadoReservacion.Text = "Ocurrió un error al ingresar los datos"
                End If

            End If
        Else
            Dim id_entidad As Integer = txt_nombre.ToolTip

            If Not updateEntidad(id_entidad, ddl_ubicacion.SelectedValue, txt_nombre.Text, "", txt_codPostal.Text, telefonoConArea, txt_email.Text, txt_direccion.Text) Then
                resul = False
                If Orbelink.DBHandler.LanguageHandler.CurrentLanguage = Orbelink.DBHandler.LanguageHandler.Language.INGLES Then
                    lbl_ResultadoReservacion.Text = "Sorry, an error occurred when updating your information. Please try again."
                Else
                    lbl_ResultadoReservacion.Text = "Ocurrió un error al modificar los datos"
                End If
            End If
        End If
        Return resul
    End Function

    Protected Function cargarItemsDisponibles(ByVal id_producto As Integer, ByVal fechaInicio As Date, ByVal fechaFin As Date) As Data.DataTable

        Dim id_ubicacion As Integer = ControladorReservaciones.Config_UbicacionDefault

        Dim controladora As New ControladorReservaciones(connection, Resources.Reservaciones_Resources.ResourceManager)
        Dim dataTable As Data.DataTable = controladora.cargarItemsDisponibles_ByProducto(id_producto, fechaInicio, fechaFin, id_ubicacion)

        Return dataTable
    End Function
    Protected Function agregaItem(ByVal ReservacionActual As Integer, ByVal id_entidad As Integer, ByVal id_producto As Integer, ByVal noches As Integer, ByVal nochesadicionales As Integer, ByVal habitacionesDisponibles As Data.DataTable, ByVal theGridView As GridView, Optional ByVal descuento As String = "") As Boolean
        Dim result As Boolean = True

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

                resultado = controladora.AgregarItem(ReservacionActual, id_entidad, id_producto, item.ordinal.Value, numeroadultos, 0, True, 1, noches, nochesadicionales, getNombreDisplay(numeroadultos, noches, nochesadicionales), descuento, Session("id_temporadaNueva"), precio_transporte)
                If Not resultado = ControladorReservaciones.Resultado_Code.OK Then
                    result = False
                End If
            Catch ex As Exception
                result = False
            End Try
        Next
        Return result
    End Function

    'Protected Function buscarPersonas(ByVal personas As Integer, ByVal theGridView As GridView) As Integer
    '    Dim result As Integer = 0
    '    For counter As Integer = 0 To theGridView.Rows.Count - 1
    '        Dim unItem As GridViewRow = theGridView.Rows(counter)
    '        Dim adultos As DropDownList = unItem.FindControl("ddl_personas")
    '        If adultos.SelectedValue = personas Then
    '            result += 1
    '        End If
    '    Next
    '    Return result
    'End Function

    Protected Function getNombreDisplay(ByVal personas As Integer, ByVal noches As Integer, ByVal nochesadicionales As Integer) As String
        Dim nombre As String = ""
        Dim transporte As String = ""

        If rdbtnlist_transporte2014.Visible = True Then
            Select Case rdbtnlist_transporte2014.SelectedValue
                Case 1
                    transporte = "<br/>Transfer to Manatus"
                Case 2
                    transporte = "<br/>Transfer back to San Jose"
                Case 3
                    transporte = "<br/>Round Trip"
            End Select

        End If


        If Orbelink.DBHandler.LanguageHandler.CurrentLanguage = Orbelink.DBHandler.LanguageHandler.Language.INGLES Then
            nombre = "Room for " & personas & " person(s), " & noches & " night(s). And " & nochesadicionales & " additional night(s)" & transporte


        Else
            nombre = "Habitación para " & personas & " person(as), " & noches & " noche(s). Y " & nochesadicionales & " noche(s) adicional(es)" & transporte
        End If

        Return nombre
    End Function
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
    Protected Sub exito(ByVal ReservacionActual As Integer)
        'Dim controladoraReservaciones As New Orbelink.Control.Reservaciones.ControladorReservaciones(connection, Resources.Reservaciones_Resources.ResourceManager)
        'controladoraReservaciones.Emails(ReservacionActual)
        Session("id_reservacion") = ReservacionActual
        Response.Redirect("shoppingCart_en.aspx?id_reservacion=" & Session("id_reservacion") & "&termino=" & 0)
    End Sub
    Protected Sub mensajeErrorReservacion()
        If Orbelink.DBHandler.LanguageHandler.CurrentLanguage = Orbelink.DBHandler.LanguageHandler.Language.INGLES Then
            lbl_ResultadoReservacion.Text = "Error on the reservation"
        Else
            lbl_ResultadoReservacion.Text = "Error al registrar la reservación"
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
    Protected Sub mensajePrecios(ByVal capacidadMax As Integer)
        If Orbelink.DBHandler.LanguageHandler.CurrentLanguage = Orbelink.DBHandler.LanguageHandler.Language.INGLES Then
            lbl_ResultadoReservacion.Text = "Sorry there are no rates for : " & capacidadMax & " person(s) in this date."
        Else
            lbl_ResultadoReservacion.Text = "Lo sentimos, no existen tarifas asociadas para: " & capacidadMax & " persona(s) en esta fecha."
        End If
    End Sub
    Protected Sub mensajeErrorFechas()
        If Orbelink.DBHandler.LanguageHandler.CurrentLanguage = Orbelink.DBHandler.LanguageHandler.Language.INGLES Then
            lbl_ResultadoReservacion.Text = "The final date has to be greater than the start date"
        Else
            lbl_ResultadoReservacion.Text = "La fecha de salida debe ser mayor a la fecha de entrada"
        End If
    End Sub
    Protected Sub mensajeErrorObservaciones()
        If Orbelink.DBHandler.LanguageHandler.CurrentLanguage = Orbelink.DBHandler.LanguageHandler.Language.INGLES Then
            lbl_ResultadoReservacion.Text = "The text inside the observations field is too long"
        Else
            lbl_ResultadoReservacion.Text = "Texto de observaciones demasiado largo"
        End If
    End Sub
    Protected Sub mensajeErrorFechaVueloPromo()
        'Orbelink.DBHandler.LanguageHandler.CurrentLanguage = Orbelink.DBHandler.LanguageHandler.Language.ESPANOL
        If Orbelink.DBHandler.LanguageHandler.CurrentLanguage = Orbelink.DBHandler.LanguageHandler.Language.INGLES Then
            lbl_ResultadoReservacion.Text = "Para poder optar por la promoción de un viaje en avión debe realizar su reservación con una semana de anticipación!"
        Else
            lbl_ResultadoReservacion.Text = "In order to participate in our plane trip promotion, you must fulfill your reservation at least one weak ahead!"
        End If
    End Sub

    Protected Sub btn_reservar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_reservar.Click
        If chkbox_terminos.Checked Then

            Dim paquete As Integer = Request.QueryString("paquete")
            'paquete = 0
            If paquete = 2 Then
                reservarconPaquete()
            Else
                lbl_terminos.Visible = False
                pnl_exito.Visible = False
                lbl_ResultadoReservacion.ForeColor = Drawing.Color.Red
                Dim id_entidad As Integer = 0
                If InsertarModificar(id_tipoEntidadUSUARIO) Then
                    If txt_nombre.ToolTip <> "" Then
                        id_entidad = txt_nombre.ToolTip
                    End If
                    Dim controladora As New ControladorReservaciones(connection, Resources.Reservaciones_Resources.ResourceManager)

                    If id_entidad > 0 Then
                        If txt_observaciones.Text.Length < 7000 Then

                            'Dim fechaInicio As Date = controladora.crearFechaHora(calendarEntrada.SelectedDate, horaEntrada, 0)
                            'Dim fechaFin As Date = controladora.crearFechaHora(calendarSalida.SelectedDate, horaSalida, 0)
                            'fechaInicio = DateHandler.ToUtcFromLocalizedDate(fechaInicio)
                            'fechaFin = DateHandler.ToUtcFromLocalizedDate(fechaFin)

                            Dim entrada As Date = calendarEntrada.SelectedDate
                            Dim salida As Date = calendarSalida.SelectedDate

                            Dim fechaInicio As New Date(entrada.Year, entrada.Month, entrada.Day, 12, 0, 0)
                            Dim fechaFin As New Date(salida.Year, salida.Month, salida.Day, 11, 0, 0)

                            Dim total_de_noches As Integer = controladora.TotalNoches(fechaInicio, fechaFin)
                            If total_de_noches > 0 Then

                                Dim id_temporada As Integer = controladora.buscarTemporada(fechaInicio, fechaInicio.AddDays(1)).id_Temporada

                                Dim habitacionesDeseadas As Integer = ddl_habitaciones.SelectedValue

                                Dim noches As Integer = controladora.NochesSegunTarifas(id_temporada, id_producto, 0, total_de_noches)
                                Dim nochesAdicionales As Integer = total_de_noches - noches
                                If validarPrecios(id_temporada, id_producto, noches, gv_ResultadosDisponibles) Then

                                    Dim habitacionesDisponibles As Data.DataTable = cargarItemsDisponibles(id_producto, fechaInicio, fechaFin)
                                    If habitacionesDisponibles.Rows.Count >= habitacionesDeseadas Then
                                        Dim descripcion As String = txt_observaciones.Text
                                        Dim DateEntrada As Date = calendarEntrada.SelectedDate
                                        Dim DateSalida As Date = calendarSalida.SelectedDate

                                        If rdbtnlist_transporte2014.Visible = True Then

                                            Dim preciotransporte As Integer = 0

                                            If entrada.Year = 2014 Then
                                                preciotransporte = 70

                                            ElseIf entrada.Year = 2015 Then
                                                preciotransporte = 75
                                            End If

                                            'If entrada.Year = 2014 Then
                                            '    preciotransporte = 70

                                            'ElseIf entrada.Year = 2015 Then
                                            '    preciotransporte = 75
                                            'End If


                                            Select Case rdbtnlist_transporte2014.SelectedValue
                                                Case 1
                                                    descripcion &= "<br /><strong> Transfer to Manatus ($" & preciotransporte & "p/p)</strong>"
                                                Case 2
                                                    descripcion &= "<br /><strong> Transfer back to San Jose ($" & preciotransporte & " p/p)</strong>"
                                                Case 3
                                                    descripcion &= "<br /><strong> Round Trip ($" & preciotransporte * 2 & " p/p)</strong>"
                                            End Select


                                            'If trPlacetoPickup.Visible = True Then
                                            '    descripcion &= "<br /><strong>Pick up place:</strong>" & txt_pickup.Text
                                            'End If
                                            'If trPlacetoLeave.Visible = True Then
                                            '    descripcion &= "<br /><strong>Drop off place:</strong>" & txt_leave.Text

                                            'End If
                                            descripcion &= "<br/><strong>For Pick up place and Drop off place:</strong> Service subject to availability. Guests must contact Manatus Hotel in order to establish the pick up or drop off place."
                                        End If

                                        Dim ReservacionActual As Integer = controladora.CrearReservacion(id_entidad, fechaInicio, fechaFin, id_entidad, True, "", 0, descripcion)

                                        If ReservacionActual > 0 Then

                                            If agregaItem(ReservacionActual, id_entidad, id_producto, noches, nochesAdicionales, habitacionesDisponibles, gv_ResultadosDisponibles) Then
                                                exito(ReservacionActual)
                                            Else
                                                mensajeErrorReservacion()
                                            End If
                                            'If agregaItem(ReservacionActual, id_entidad, id_producto, noches, nochesAdicionales, habitacionesDisponibles, gv_ResultadosDisponibles) Then
                                            '    exito(ReservacionActual)
                                            'Else
                                            '    mensajeErrorReservacion()
                                            'End If
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
                        Else
                            mensajeErrorObservaciones()
                        End If
                    End If
                End If
                lbl_ResultadoReservacion.Visible = True

            End If
        Else
            lbl_terminos.Visible = True
        End If
    End Sub

    'Protected Sub btn_reservar_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btn_reservar.Click

    'End Sub


    Protected Sub reservarconPaquete()
        If chkbox_terminos.Checked Then

            Dim DateEntrada As Date = calendarEntrada.SelectedDate
            Dim DateSalida As Date = calendarSalida.SelectedDate

            Dim paquete As Integer = Request.QueryString("paquete")


            'Paquete 2, Noche extra

            Dim PromoIni As New Date(2015, 5, 1)
            Dim PromoFin As New Date(2015, 6, 30)

            If DateEntrada >= PromoIni And DateSalida <= PromoFin Then
                lbl_terminos.Visible = False
                pnl_exito.Visible = False
                lbl_ResultadoReservacion.ForeColor = Drawing.Color.Red
                Dim id_entidad As Integer = 0
                If InsertarModificar(id_tipoEntidadUSUARIO) Then
                    If txt_nombre.ToolTip <> "" Then
                        id_entidad = txt_nombre.ToolTip
                    End If
                    Dim controladora As New ControladorReservaciones(connection, Resources.Reservaciones_Resources.ResourceManager)
                    If id_entidad > 0 Then
                        If txt_observaciones.Text.Length < 7000 Then
                            Dim fechaInicio As Date = controladora.crearFechaHora(DateEntrada, horaEntrada, 0)
                            Dim fechaFin As Date = controladora.crearFechaHora(DateSalida, horaSalida, 0)
                            fechaInicio = DateHandler.ToUtcFromLocalizedDate(fechaInicio)
                            fechaFin = DateHandler.ToUtcFromLocalizedDate(fechaFin)
                            Dim total_de_noches As Integer = controladora.TotalNoches(fechaInicio, fechaFin)
                            If total_de_noches > 0 Then
                                Dim id_temporada As Integer
                                Dim habitacionesDeseadas As Integer
                                'id_temporada = controladora.buscarTemporada(fechaInicio, fechaInicio.AddDays(1)).id_Temporada

                                If paquete = 2 Then
                                    id_temporada = 14 'Upgrades Package 2015
                                End If
                                '
                                habitacionesDeseadas = ddl_habitaciones.SelectedValue
                                Dim noches As Integer = controladora.NochesSegunTarifas(id_temporada, id_producto, 0, total_de_noches)
                                Dim nochesAdicionales As Integer = total_de_noches - noches
                                If validarPrecios(id_temporada, id_producto, noches, gv_ResultadosDisponibles) Then
                                    Dim habitacionesDisponibles As Data.DataTable = cargarItemsDisponibles(id_producto, fechaInicio, fechaFin)
                                    If habitacionesDisponibles.Rows.Count >= habitacionesDeseadas Then
                                        Dim descripcion As String = txt_observaciones.Text

                                        Dim preciotransporte As Integer = 0

                                        If DateEntrada.Year = 2014 Then
                                            preciotransporte = 70

                                        ElseIf DateEntrada.Year = 2015 Then
                                            preciotransporte = 75
                                        End If

                                        Select Case rdbtnlist_transporte2014.SelectedValue
                                            Case 1
                                                descripcion &= "<br /><strong> Transfer to Manatus ($" & preciotransporte & "p/p)</strong>"
                                            Case 2
                                                descripcion &= "<br /><strong> Transfer back to San Jose ($" & preciotransporte & " p/p)</strong>"
                                            Case 3
                                                descripcion &= "<br /><strong> Round Trip ($" & preciotransporte * 2 & " p/p)</strong>"
                                        End Select

                                        descripcion &= "<br/><strong>For Pick up place and Drop off place:</strong> Service subject to availability. Guests must contact Manatus Hotel in order to establish the pick up or drop off place."

                                        descripcion &= "<br /><br /><strong> FREE EXTRA NIGHT</strong><br />"

                                        Dim ReservacionActual As Integer = controladora.CrearReservacion(id_entidad, fechaInicio, fechaFin, id_entidad, True, "", 0, descripcion)

                                        If ReservacionActual > 0 Then

                                            If agregaItem(ReservacionActual, id_entidad, id_producto, noches, nochesAdicionales, habitacionesDisponibles, gv_ResultadosDisponibles) Then
                                                exito(ReservacionActual)
                                            Else
                                                mensajeErrorReservacion()
                                            End If

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
                        Else
                            mensajeErrorObservaciones()
                        End If
                    End If
                End If
                lbl_ResultadoReservacion.Visible = True

            Else

                lbl_erroFechas.Text &= "The Free extra Night at Manatus Hotel is valid only from May to June 2015.<br/>"

                lbl_erroFechas.Visible = True
            End If

        Else
            lbl_terminos.Visible = True
        End If
    End Sub


    'Protected Sub reservaTortuga()
    '    If chkbox_terminos.Checked Then
    '        Dim PromoIni As Date
    '        Dim PromoFin As Date

    '        Dim boletin As String = Request.QueryString("newsletter")

    '        If boletin = "1" Then 'Viene del Boletin 
    '            PromoIni = New Date(2011, 7, 1)
    '            PromoFin = New Date(2011, 10, 31)
    '            ' lbl_erroFechas.Text = "Package valid for the months of july through october 2011 <br/>"
    '        Else 'No viene del Boletin
    '            PromoIni = New Date(2011, 9, 1)
    '            PromoFin = New Date(2011, 10, 31)
    '            lbl_erroFechas.Text = "Package valid for the months of september through october 2011 <br/>"
    '        End If

    '        Dim DateEntrada As Date = calendarEntrada.SelectedDate
    '        Dim DateSalida As Date = calendarSalida.SelectedDate


    '        Dim paquete As Integer = Request.QueryString("paquete")
    '        paquete = 0
    '        If DateEntrada >= PromoIni And DateSalida <= PromoFin Then
    '            lbl_terminos.Visible = False
    '            pnl_exito.Visible = False
    '            lbl_ResultadoReservacion.ForeColor = Drawing.Color.Red
    '            Dim id_entidad As Integer = 0
    '            If InsertarModificar(id_tipoEntidadUSUARIO) Then
    '                If txt_nombre.ToolTip <> "" Then
    '                    id_entidad = txt_nombre.ToolTip
    '                End If
    '                Dim controladora As New ControladorReservaciones(connection, Resources.Reservaciones_Resources.ResourceManager)

    '                If id_entidad > 0 Then
    '                    If txt_observaciones.Text.Length < 7000 Then

    '                        Dim fechaInicio As Date = controladora.crearFechaHora(calendarEntrada.SelectedDate, horaEntrada, 0)
    '                        Dim fechaFin As Date = controladora.crearFechaHora(calendarSalida.SelectedDate, horaSalida, 0)
    '                        fechaInicio = DateHandler.ToUtcFromLocalizedDate(fechaInicio)
    '                        fechaFin = DateHandler.ToUtcFromLocalizedDate(fechaFin)
    '                        Dim total_de_noches As Integer = controladora.TotalNoches(fechaInicio, fechaFin)
    '                        If total_de_noches > 0 Then

    '                            Dim id_temporada As Integer

    '                            Dim habitacionesDeseadas As Integer


    '                            id_temporada = controladora.buscarTemporada(fechaInicio, fechaInicio.AddDays(1)).id_Temporada
    '                            habitacionesDeseadas = ddl_habitaciones.SelectedValue


    '                            Dim noches As Integer = controladora.NochesSegunTarifas(id_temporada, id_producto, 0, total_de_noches)
    '                            Dim nochesAdicionales As Integer = total_de_noches - noches
    '                            If validarPrecios(id_temporada, id_producto, noches, gv_ResultadosDisponibles) Then

    '                                Dim habitacionesDisponibles As Data.DataTable = cargarItemsDisponibles(id_producto, fechaInicio, fechaFin)
    '                                If habitacionesDisponibles.Rows.Count >= habitacionesDeseadas Then
    '                                    Dim descripcion As String = txt_observaciones.Text

    '                                    ' CODIGO PARA LA PROMOCION DEL DESCUENTO O DE VIAJE EN AVION
    '                                    'Dim PromoIni As New Date(2010, 9, 1)
    '                                    'Dim PromoFin As New Date(2010, 11, 30)
    '                                    'Dim descuento As String = ""

    '                                    'If DateEntrada >= PromoIni And DateSalida <= PromoFin Then
    '                                    '    If rbtnlst_Promocion.SelectedValue = "1" Then
    '                                    '        If txtDateEntrada.Text <> "" And txtDateSalida.Text <> "" Then
    '                                    '            If DateEntrada > Date.Now.AddDays(7) Then
    '                                    '                descripcion &= "<br /><br /><strong> Promotion: You have selected the plane trip option (San José-Torguero). We will contact you soon with further details!</strong><br />"
    '                                    '            Else
    '                                    '                mensajeErrorFechaVueloPromo()
    '                                    '                Exit Sub
    '                                    '            End If
    '                                    '        End If
    '                                    '    ElseIf rbtnlst_Promocion.SelectedValue = "0" Then
    '                                    '        If DateEntrada >= PromoIni And DateSalida <= PromoFin Then
    '                                    '            descuento = "10" 'representa el descuento del 10%
    '                                    '        End If
    '                                    '    End If
    '                                    'End If

    '                                    Select Case paquete


    '                                        Case 4
    '                                            descripcion &= "<br /><br /><strong> Package: FREE TURTLE NESTING TOUR, Courtesy Turtle Tour on nesting season.</strong><br />"

    '                                        Case 5
    '                                            descripcion &= "<br /><br /><strong> Package: FREE </strong><br />"

    '                                    End Select

    '                                    Dim ReservacionActual As Integer = controladora.CrearReservacion(id_entidad, fechaInicio, fechaFin, id_entidad, True, "", 0, descripcion)

    '                                    If ReservacionActual > 0 Then
    '                                        ' CODIGO PARA LA PROMOCION DEL DESCUENTO O DE VIAJE EN AVION
    '                                        'If agregaItem(ReservacionActual, id_entidad, id_producto, noches, nochesAdicionales, habitacionesDisponibles, gv_ResultadosDisponibles, descuento) Then
    '                                        If agregaItem(ReservacionActual, id_entidad, id_producto, noches, nochesAdicionales, habitacionesDisponibles, gv_ResultadosDisponibles) Then
    '                                            exito(ReservacionActual)
    '                                        Else
    '                                            mensajeErrorReservacion()
    '                                        End If
    '                                        If agregaItem(ReservacionActual, id_entidad, id_producto, noches, nochesAdicionales, habitacionesDisponibles, gv_ResultadosDisponibles) Then
    '                                            exito(ReservacionActual)
    '                                        Else
    '                                            mensajeErrorReservacion()
    '                                        End If
    '                                    Else
    '                                        mensajeErrorReservacion()
    '                                    End If
    '                                Else
    '                                    mensajeCantidadHabitaciones(habitacionesDisponibles.Rows.Count)
    '                                End If

    '                            End If
    '                        Else
    '                            mensajeErrorFechas()
    '                        End If
    '                    Else
    '                        mensajeErrorObservaciones()
    '                    End If
    '                End If
    '            End If
    '            lbl_ResultadoReservacion.Visible = True

    '        Else
    '            lbl_erroFechas.Visible = True
    '        End If

    '    Else
    '        lbl_terminos.Visible = True
    '    End If
    'End Sub

    ' CODIGO PARA LA PROMOCION DEL DESCUENTO O DE VIAJE EN AVION
    'Protected Sub txtDateEntrada_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDateEntrada.TextChanged, txtDateSalida.TextChanged
    '    If txtDateEntrada.Text <> "" And txtDateSalida.Text <> "" Then
    '        Dim DateEntrada As Date = calendarEntrada.SelectedDate
    '        Dim DateSalida As Date = calendarSalida.SelectedDate
    '        Dim PromoIni As New Date(2010, 9, 1)
    '        Dim PromoFin As New Date(2010, 11, 30)
    '        If DateEntrada >= PromoIni And DateSalida <= PromoFin Then
    '            If DateEntrada >= PromoIni And DateSalida <= PromoFin Then
    '                rbtnlst_Promocion.Enabled = True
    '            Else
    '                rbtnlst_Promocion.Enabled = False
    '                rbtnlst_Promocion.ClearSelection()
    '            End If
    '        Else
    '            rbtnlst_Promocion.Enabled = False
    '            rbtnlst_Promocion.ClearSelection()
    '        End If
    '    End If
    'End Sub

    Protected Sub calendarEntrada_DateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles calendarEntrada.DateChanged
        Dim paquete As Integer = Request.QueryString("paquete")
        Dim min, max As Date 'min2014, max2014 

        'min2014 = DateValue("01/01/2014")
        'max2014 = DateValue("12/31/2014")
        'paquete = 0
        If paquete = 2 Then
            'lbl_noches.visible = False
            ' Dim min, max As Date


            'min = DateValue("5/01/2015") 'Arriba
            'max = DateValue("6/27/2015") 'Arriba

            min = DateValue("2015/5/01") ' Abajo
            max = DateValue("2015/6/27") ' Abajo

            If calendarEntrada.SelectedDate >= min And calendarEntrada.SelectedDate <= max Then
                calendarSalida.Enabled = False
                txtDateSalida.Enabled = False
                calendarSalida.SelectedDate = calendarEntrada.SelectedDate.AddDays(3)
            Else
                calendarSalida.SelectedDate = calendarEntrada.SelectedDate
                calendarSalida.Enabled = True
                txtDateSalida.Enabled = True
            End If

            ' calendarSalida.SelectedDate = calendarEntrada.SelectedDate
        End If


        'If calendarEntrada.SelectedDate.Year >= 2014 And calendarEntrada.SelectedDate.Year <= 2015 Then
        '    trPaquetes2014.Visible = True
        '    trPlacetoPickup.Visible = True
        '    trPlacetoLeave.Visible = True
        'Else
        '    ' calendarSalida.SelectedDate = calendarEntrada.SelectedDate
        '    trPaquetes2014.Visible = False
        '    trPlacetoPickup.Visible = False
        '    trPlacetoLeave.Visible = False
        'End If



        'terminosycondiciones()
    End Sub

    Protected Sub terminosycondiciones()
        Dim paquete As Integer = Request.QueryString("paquete")

        If paquete <> 0 Then
            pnl_terminospaquete.Visible = True
            Panel1.Visible = False
        Else
            pnl_terminospaquete.Visible = False
            Panel1.Visible = True
        End If

    End Sub


    Protected Sub calendarSalida_DateChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles calendarSalida.DateChanged

    End Sub

    Protected Sub rdbtnlist_transporte2014_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdbtnlist_transporte2014.TextChanged
        box_pickup.Visible = False
        box_leave.Visible = False
        'Select Case rdbtnlist_transporte2014.SelectedValue
        '    Case 1
        '        box_pickup.Visible = True
        '        box_leave.Visible = False
        '    Case 2
        '        box_pickup.Visible = False
        '        box_leave.Visible = True
        '    Case 3
        '        box_pickup.Visible = True
        '        box_leave.Visible = True
        'End Select
    End Sub
End Class
