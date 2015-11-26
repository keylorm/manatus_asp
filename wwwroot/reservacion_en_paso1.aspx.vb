﻿Imports Orbelink.Entity.Entidades
Imports Orbelink.Entity.Productos
Imports Orbelink.Entity.Reservaciones
Imports Orbelink.DBHandler
Imports Orbelink.Control.Reservaciones
Imports Orbelink.DateAndTime.DateHandler
Imports Orbelink.DateAndTime
Imports System.Globalization


Partial Class reservacion_en_paso1
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
            selectPaises()
            If Request.QueryString("rango_fecha") IsNot Nothing Then
                Culture = "en-us"
                TxtCheckinCheckout.Text = Request.QueryString("rango_fecha")

            End If
            btn_reservar1.Visible = True

            lbl_erroFechas.Visible = False


            'trPaquetes2014.Visible = True
            'box_leave.Visible = False
            'box_pickup.Visible = False

            Dim paquete As Integer = Request.QueryString("paquete")
            'paquete = 0


            'calendarEntrada.DateMin = Date.Today

            'selectHabitaciones(id_producto, 6)

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
                    If ((Request.QueryString("rango_fecha") IsNot Nothing)) Then
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

    'Funciones Requerida spara calculos y procesos
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
    'Devuelve listado de los paises
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

    Protected Function agregaItemTemporal(ByVal id_producto As Integer, ByVal noches As Integer, ByVal nochesadicionales As Integer, ByVal habitacionesDisponibles As Data.DataTable, ByVal theGridView As GridView, ByVal usarPrecioTransporte As Boolean, ByVal fechaInicio As Date, ByVal fechaFin As Date, Optional ByVal descuento As String = "") As Double
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

                'Dim entrada As Date = calendarEntrada.SelectedDate
                'Dim salida As Date = calendarSalida.SelectedDate

                'Dim fechaInicio As New Date(entrada.Year, entrada.Month, entrada.Day, 12, 0, 0)
                'Dim fechaFin As New Date(salida.Year, salida.Month, salida.Day, 11, 0, 0)

                If rdbtnlist_transporte2014.Visible = True Then

                    Dim preciotransporte As Integer = 0

                    If fechaInicio.Year = 2014 Then
                        preciotransporte = 70

                    ElseIf fechaInicio.Year = 2015 Then
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

    Protected Function agregaItemTemporalIndividual(ByVal id_producto As Integer, ByVal noches As Integer, ByVal nochesadicionales As Integer, ByVal habitacionesDisponibles As Data.DataTable, ByVal theGridViewRow As GridViewRow, ByVal counter As Integer, ByVal usarPrecioTransporte As Boolean, ByVal fechaInicio As Date, ByVal fechaFin As Date, Optional ByVal descuento As String = "") As Double
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

            'Dim entrada As Date = calendarEntrada.SelectedDate
            'Dim salida As Date = calendarSalida.SelectedDate

            'Dim fechaInicio As New Date(entrada.Year, entrada.Month, entrada.Day, 12, 0, 0)
            'Dim fechaFin As New Date(salida.Year, salida.Month, salida.Day, 11, 0, 0)

            If rdbtnlist_transporte2014.Visible = True Then

                Dim preciotransporte As Integer = 0

                If fechaInicio.Year = 2014 Then
                    preciotransporte = 70

                ElseIf fechaInicio.Year = 2015 Then
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

    Protected Function agregaItem(ByVal ReservacionActual As Integer, ByVal id_entidad As Integer, ByVal id_producto As Integer, ByVal noches As Integer, ByVal nochesadicionales As Integer, ByVal habitacionesDisponibles As Data.DataTable, ByVal theGridView As GridView, ByVal fechaInicio As Date, ByVal fechaFin As Date, Optional ByVal descuento As String = "") As Boolean
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

                If paquete = 2 Then
                    Session("id_temporadaNueva") = 14 'Upgrades Package 2014
                Else
                    Session("id_temporadaNueva") = Nothing
                End If

                Dim precio_transporte As Integer = 0



                If rdbtnlist_transporte2014.Visible = True Then

                    Dim preciotransporte As Integer = 0

                    If fechaInicio.Year = 2014 Then
                        preciotransporte = 70

                    ElseIf fechaInicio.Year = 2015 Then
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


    'Mensajes
    Protected Sub mensajeErrorCantPersonasXHabitacion(ByVal codigo_error As Integer)
        lbl_ResultadoHabitaciones.Visible = True
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
        lbl_ResultadoReservacion.Visible = True
        If Orbelink.DBHandler.LanguageHandler.CurrentLanguage = Orbelink.DBHandler.LanguageHandler.Language.INGLES Then
            lbl_ResultadoReservacion.Text = "Sorry there are no rates for : " & capacidadMax & " person(s) in this date."
        Else
            lbl_ResultadoReservacion.Text = "Lo sentimos, no existen tarifas asociadas para: " & capacidadMax & " persona(s) en esta fecha."
        End If
    End Sub

    Protected Sub mensajeCantidadHabitaciones(ByVal habitacionesDisponibles As Integer)
        lbl_ResultadoReservacion.Visible = True
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
        lbl_ResultadoReservacion.Visible = True
        If Orbelink.DBHandler.LanguageHandler.CurrentLanguage = Orbelink.DBHandler.LanguageHandler.Language.INGLES Then
            lbl_ResultadoReservacion.Text = "The final date has to be greater than the start date"
        Else
            lbl_ResultadoReservacion.Text = "La fecha de salida debe ser mayor a la fecha de entrada"
        End If
    End Sub

    Protected Sub mensajeErrorReservacion()
        lbl_ResultadoReservacion.Visible = True
        If Orbelink.DBHandler.LanguageHandler.CurrentLanguage = Orbelink.DBHandler.LanguageHandler.Language.INGLES Then
            lbl_ResultadoReservacion.Text = "Error on the reservation"
        Else
            lbl_ResultadoReservacion.Text = "Error al registrar la reservación"
        End If
    End Sub

    Protected Sub mensajeErrorObservaciones()
        lbl_ResultadoReservacion.Visible = True
        Orbelink.DBHandler.LanguageHandler.CurrentLanguage = Orbelink.DBHandler.LanguageHandler.Language.ESPANOL
        If Orbelink.DBHandler.LanguageHandler.CurrentLanguage = Orbelink.DBHandler.LanguageHandler.Language.INGLES Then
            lbl_ResultadoReservacion.Text = "The text inside the observations field is too long"
        Else
            lbl_ResultadoReservacion.Text = "Texto de observaciones demasiado largo"
        End If
    End Sub

    'Triggers

    Protected Sub exito(ByVal ReservacionActual As Integer)

        Session("id_reservacion") = ReservacionActual
        Response.Redirect("shoppingCart_en.aspx?id_reservacion=" & Session("id_reservacion") & "&termino=" & 0)
    End Sub

    Protected Sub rdbtnlist_transporte2014_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles rdbtnlist_transporte2014.SelectedIndexChanged
        lbl_ResultadoHabitaciones.Visible = False
        lbl_erroFechas.Visible = False
        lbl_ResultadoReservacion.Visible = False
        If TxtCheckinCheckout.Text.Length > 0 And TxtCheckinCheckout.Text.Contains(" - ") Then
            If (gv_ResultadosDisponibles.Rows.Count > 0) Then
                If rdbtnlist_transporte2014.SelectedValue.Length > 0 Then
                    CalculoPrecioConTransporte()
                End If
            End If
        End If
    End Sub

    Protected Sub gv_ResultadosDisponibles_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gv_ResultadosDisponibles.RowCommand
        lbl_ResultadoHabitaciones.Visible = False
        lbl_erroFechas.Visible = False
        lbl_ResultadoReservacion.Visible = False
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
                    Dim paquete As String = Lblpaquete.Text
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


            If TxtCheckinCheckout.Text.Length > 0 And TxtCheckinCheckout.Text.Contains(" - ") Then
                If (gv_ResultadosDisponibles.Rows.Count > 0) Then
                    CalculoPrecio()

                End If
            End If

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
        lbl_ResultadoHabitaciones.Visible = False
        lbl_erroFechas.Visible = False
        lbl_ResultadoReservacion.Visible = False


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


        Dim habitacionesDisponibles As Integer = selectHabitaciones(id_producto, 6)

        If (habitacionesDisponibles >= contador + 1) Then
            cargarHabitacionesDeseadas(contador + 1)
        Else
            cargarHabitacionesDeseadas(contador)
            mensajeCantidadHabitaciones(habitacionesDisponibles)

        End If






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

        If TxtCheckinCheckout.Text.Length > 0 And TxtCheckinCheckout.Text.Contains(" - ") Then
            If (gv_ResultadosDisponibles.Rows.Count > 0) Then
                CalculoPrecio()

            End If
        End If
    End Sub

    Protected Sub ddl_personas_SelectedIndexChanged(sender As Object, e As System.EventArgs)
        lbl_ResultadoHabitaciones.Visible = False
        lbl_erroFechas.Visible = False
        lbl_ResultadoReservacion.Visible = False
        If TxtCheckinCheckout.Text.Length > 0 And TxtCheckinCheckout.Text.Contains(" - ") Then
            If (gv_ResultadosDisponibles.Rows.Count > 0) Then
                CalculoPrecio()
            End If
        End If

    End Sub

    Protected Sub book_now_1_Click(sender As Object, e As System.EventArgs) Handles book_now_1.Click
        lbl_ResultadoHabitaciones.Visible = False
        lbl_erroFechas.Visible = False
        lbl_ResultadoReservacion.Visible = False

        If (String.Equals(lbl_precioSinTransporte.Text, "0") = False And String.Equals(lbl_precioConTransporte.Text, "0") = False) Then
            paso1.Visible = False
            paso2.Visible = True
        Else
            lbl_ResultadoReservacion.Visible = True
            lbl_ResultadoReservacion.ForeColor = Drawing.Color.Red
            lbl_ResultadoReservacion.Text = "You need to choose your reservations days and rooms to continue with the process"
        End If

    End Sub

    Protected Sub book_now_2_Click(sender As Object, e As System.EventArgs) Handles book_now_2.Click
        lbl_ResultadoHabitaciones.Visible = False
        lbl_erroFechas.Visible = False
        lbl_ResultadoReservacion.Visible = False

        If (String.Equals(lbl_precioSinTransporte.Text, "0") = False And String.Equals(lbl_precioConTransporte.Text, "0") = False) Then
            paso1.Visible = False
            paso2.Visible = True
        Else
            lbl_ResultadoReservacion.Visible = True
            lbl_ResultadoReservacion.ForeColor = Drawing.Color.Red
            lbl_ResultadoReservacion.Text = "You need to choose your reservations days and rooms to continue with the process"
        End If
    End Sub


    Protected Sub LinkEditarInformacion_Click(sender As Object, e As System.EventArgs) Handles LinkEditarInformacion.Click
        lbl_ResultadoHabitaciones.Visible = False
        lbl_erroFechas.Visible = False
        lbl_ResultadoReservacion.Visible = False
        paso2.Visible = False
        paso1.Visible = True

    End Sub

    Protected Sub step_2_link_Click(sender As Object, e As System.EventArgs) Handles step_2_link.Click
        lbl_ResultadoHabitaciones.Visible = False
        lbl_erroFechas.Visible = False
        lbl_ResultadoReservacion.Visible = False

        If (String.Equals(lbl_precioSinTransporte.Text, "0") = False And String.Equals(lbl_precioConTransporte.Text, "0") = False) Then
            paso1.Visible = False
            paso2.Visible = True
        Else
            lbl_ResultadoReservacion.Visible = True
            lbl_ResultadoReservacion.ForeColor = Drawing.Color.Red
            lbl_ResultadoReservacion.Text = "You need to choose your reservations days and rooms to continue with the process"
        End If
    End Sub

    Protected Sub step_1_link_Click(sender As Object, e As System.EventArgs) Handles step_1_link.Click
        lbl_ResultadoHabitaciones.Visible = False
        lbl_erroFechas.Visible = False
        lbl_ResultadoReservacion.Visible = False
        paso2.Visible = False
        paso1.Visible = True
    End Sub
    Protected Sub step_3_link_Click(sender As Object, e As System.EventArgs) Handles step_3_link.Click
        lbl_ResultadoHabitaciones.Visible = False
        lbl_erroFechas.Visible = False
        lbl_ResultadoReservacion.Visible = False
        Dim paquete As Integer = Request.QueryString("paquete")
        'paquete = 0
        If paquete = 2 Then
            reservarconPaquete()
        Else

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

                        Dim entrada As Date
                        Dim salida As Date

                        'separador para el rango de fecha
                        Dim rango = TxtCheckinCheckout.Text
                        Dim delimiter As Char = " - "
                        Dim substrings() As String = rango.Split(delimiter)
                        Dim counter1 = 0

                        For Each substring In substrings
                            If (substring <> "-") Then
                                'separador para el checkin
                                Dim delimiter2 As Char = "/"
                                Dim substrings2() As String = substring.Split(delimiter2)
                                Dim counter2 = 0
                                Dim d = 0
                                Dim m = 0
                                Dim y = 0
                                If counter1 = 0 Then
                                    For Each substring2 In substrings2
                                        If (substring2 <> "/") Then
                                            If counter2 = 0 Then

                                                m = substring2
                                            ElseIf counter2 = 1 Then
                                                d = substring2
                                            Else
                                                y = substring2
                                            End If
                                        End If
                                        counter2 = counter2 + 1
                                    Next
                                    Dim date1 As New Date(y, m, d, 0, 0, 0)
                                    entrada = date1
                                Else
                                    For Each substring2 In substrings2
                                        If (substring2 <> "/") Then
                                            If counter2 = 0 Then

                                                m = substring2
                                            ElseIf counter2 = 1 Then
                                                d = substring2
                                            Else
                                                y = substring2
                                            End If
                                        End If
                                        counter2 = counter2 + 1
                                    Next
                                    Dim date1 As New Date(y, m, d, 0, 0, 0)
                                    salida = date1
                                End If
                            End If
                            counter1 = counter1 + 1
                        Next

                        'Dim entrada As Date = calendarEntrada.SelectedDate
                        'Dim salida As Date = calendarSalida.SelectedDate

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
                                    Dim descripcion As String = txt_observaciones.Text


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
                                        '    descripcion &= "<br /><strong> Lugar donde se recoge:</strong>" & txt_pickup.Text
                                        'End If
                                        'If trPlacetoLeave.Visible = True Then
                                        '    descripcion &= "<br /><strong>Lugar donde se deja:</strong>" & txt_leave.Text

                                        'End If
                                        descripcion &= "<br/><strong>For Pick up place and Drop off place:</strong> Service subject to availability. Guests must contact Manatus Hotel in order to establish the pick up or drop off place."
                                    End If

                                    Dim ReservacionActual As Integer = controladora.CrearReservacion(id_entidad, fechaInicio, fechaFin, id_entidad, True, "", 0, descripcion)

                                    If ReservacionActual > 0 Then

                                        If agregaItem(ReservacionActual, id_entidad, id_producto, noches, nochesAdicionales, habitacionesDisponibles, gv_ResultadosDisponibles, fechaInicio, fechaFin) Then
                                            exito(ReservacionActual)
                                        Else
                                            mensajeErrorReservacion()
                                        End If
                                        'If agregaItem(ReservacionActual, id_entidad, id_producto, noches, nochesAdicionales, habitacionesDisponibles, gv_ResultadosDisponibles, fechaInicio, fechaFin) Then
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
    End Sub



    Protected Sub AplicarSeleccion_Click(sender As Object, e As EventArgs) Handles AplicarSeleccion.Click
        lbl_ResultadoHabitaciones.Visible = False
        lbl_erroFechas.Visible = False
        lbl_ResultadoReservacion.Visible = False
        'Separar la fecha por checkin - checkout
        'Dim rango = TxtCheckinCheckout.Text
        'Dim delimiter As Char = " - "
        'Dim substrings() As String = rango.Split(delimiter)
        'Dim counter = 0
        'For Each substring In substrings
        '    If counter = 0 Then
        '        txtDateEntrada.Text = substring
        '    Else
        '        txtDateSalida.Text = substring
        '    End If
        '    counter = counter + 1
        'Next
        If TxtCheckinCheckout.Text.Length > 0 And TxtCheckinCheckout.Text.Contains(" - ") Then
            If (gv_ResultadosDisponibles.Rows.Count > 0) Then
                CalculoPrecio()
            End If
        End If

    End Sub

    'Calculos de Precio  y reservaciones
    Public Sub CalculoPrecio()


        Dim paquete As Integer = Request.QueryString("paquete")
        'paquete = 0
        If paquete = 2 Then
            CalculoPrecioConPaquete()
        Else
            lbl_ResultadoReservacion.ForeColor = Drawing.Color.Red

            Dim controladora As New ControladorReservaciones(connection, Resources.Reservaciones_Resources.ResourceManager)

            Dim entrada As Date
            Dim salida As Date

            'separador para el rango de fecha
            Dim rango = TxtCheckinCheckout.Text
            Dim delimiter As Char = " - "
            Dim substrings() As String = rango.Split(delimiter)
            Dim counter1 = 0

            For Each substring In substrings
                If (substring <> "-") Then
                    'separador para el checkin
                    Dim delimiter2 As Char = "/"
                    Dim substrings2() As String = substring.Split(delimiter2)
                    Dim counter2 = 0
                    Dim d = 0
                    Dim m = 0
                    Dim y = 0
                    If counter1 = 0 Then
                        For Each substring2 In substrings2
                            If (substring2 <> "/") Then
                                If counter2 = 0 Then

                                    m = substring2

                                ElseIf counter2 = 1 Then
                                    d = substring2
                                Else
                                    y = substring2
                                End If
                            End If
                            counter2 = counter2 + 1
                        Next
                        Dim date1 As New Date(y, m, d, 0, 0, 0)
                        entrada = date1
                    Else
                        For Each substring2 In substrings2
                            If (substring2 <> "/") Then
                                If counter2 = 0 Then

                                    m = substring2
                                ElseIf counter2 = 1 Then
                                    d = substring2
                                Else
                                    y = substring2
                                End If
                            End If
                            counter2 = counter2 + 1
                        Next
                        Dim date1 As New Date(y, m, d, 0, 0, 0)
                        salida = date1
                    End If
                End If
                counter1 = counter1 + 1
            Next

            'Dim entrada As Date = calendarEntrada.SelectedDate
            'Dim salida As Date = calendarSalida.SelectedDate

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
                            lbl_precio_habitacion.Text = "$ " + Convert.ToString(agregaItemTemporalIndividual(id_producto, noches, nochesAdicionales, habitacionesDisponibles, unItem, counter, False, fechaInicio, fechaFin))

                        Next

                        Dim precioSintrasporte As Double = agregaItemTemporal(id_producto, noches, nochesAdicionales, habitacionesDisponibles, gv_ResultadosDisponibles, False, fechaInicio, fechaFin)
                        Dim precioContranporte As Double = agregaItemTemporal(id_producto, noches, nochesAdicionales, habitacionesDisponibles, gv_ResultadosDisponibles, True, fechaInicio, fechaFin)

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

    End Sub

    Public Sub CalculoPrecioConPaquete()
        'loader.Visible = True
        'loader.Visible = False
    End Sub

    Public Sub CalculoPrecioConTransporte()


        Dim paquete As Integer = Request.QueryString("paquete")
        'paquete = 0
        If paquete = 2 Then
            CalculoPrecioConPaquete()
        Else
            lbl_ResultadoReservacion.ForeColor = Drawing.Color.Red

            Dim controladora As New ControladorReservaciones(connection, Resources.Reservaciones_Resources.ResourceManager)

            Dim entrada As Date
            Dim salida As Date

            'separador para el rango de fecha
            Dim rango = TxtCheckinCheckout.Text
            Dim delimiter As Char = " - "
            Dim substrings() As String = rango.Split(delimiter)
            Dim counter1 = 0

            For Each substring In substrings
                If (substring <> "-") Then
                    'separador para el checkin
                    Dim delimiter2 As Char = "/"
                    Dim substrings2() As String = substring.Split(delimiter2)
                    Dim counter2 = 0
                    Dim d = 0
                    Dim m = 0
                    Dim y = 0
                    If counter1 = 0 Then
                        For Each substring2 In substrings2
                            If (substring2 <> "/") Then
                                If counter2 = 0 Then

                                    m = substring2
                                ElseIf counter2 = 1 Then
                                    d = substring2
                                Else
                                    y = substring2
                                End If
                            End If
                            counter2 = counter2 + 1
                        Next
                        Dim date1 As New Date(y, m, d, 0, 0, 0)
                        entrada = date1
                    Else
                        For Each substring2 In substrings2
                            If (substring2 <> "/") Then
                                If counter2 = 0 Then

                                    m = substring2
                                ElseIf counter2 = 1 Then
                                    d = substring2
                                Else
                                    y = substring2
                                End If
                            End If
                            counter2 = counter2 + 1
                        Next
                        Dim date1 As New Date(y, m, d, 0, 0, 0)
                        salida = date1
                    End If
                End If
                counter1 = counter1 + 1
            Next

            'Dim entrada As Date = calendarEntrada.SelectedDate
            'Dim salida As Date = calendarSalida.SelectedDate


            Dim fechaInicio As New Date(entrada.Year, entrada.Month, entrada.Day, 12, 0, 0)
            Dim fechaFin As New Date(salida.Year, salida.Month, salida.Day, 11, 0, 0)

            Dim total_de_noches As Integer = controladora.TotalNoches(fechaInicio, fechaFin)
            If total_de_noches > 0 Then
                Dim id_temporada As Integer = controladora.buscarTemporada(fechaInicio, fechaInicio.AddDays(1)).id_Temporada


                Dim habitacionesDeseadas As Integer = gv_ResultadosDisponibles.Rows.Count

                Dim noches As Integer = controladora.NochesSegunTarifas(id_temporada, id_producto, 0, total_de_noches)
                Dim nochesAdicionales As Integer = total_de_noches - noches

                'Para cambiar el radio Button del paquete



                If validarPrecios(id_temporada, id_producto, noches, gv_ResultadosDisponibles) Then

                    Dim habitacionesDisponibles As Data.DataTable = cargarItemsDisponibles(id_producto, fechaInicio, fechaFin)
                    If habitacionesDisponibles.Rows.Count >= habitacionesDeseadas Then


                        Dim precioContransporte As Double = agregaItemTemporal(id_producto, noches, nochesAdicionales, habitacionesDisponibles, gv_ResultadosDisponibles, True, fechaInicio, fechaFin)

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

    Protected Sub btn_aPaso3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_aPaso3.Click
        Dim paquete As Integer = Request.QueryString("paquete")
        'paquete = 0
        If paquete = 2 Then
            reservarconPaquete()
        Else

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

                        Dim entrada As Date
                        Dim salida As Date

                        'separador para el rango de fecha
                        Dim rango = TxtCheckinCheckout.Text
                        Dim delimiter As Char = " - "
                        Dim substrings() As String = rango.Split(delimiter)
                        Dim counter1 = 0

                        For Each substring In substrings
                            If (substring <> "-") Then
                                'separador para el checkin
                                Dim delimiter2 As Char = "/"
                                Dim substrings2() As String = substring.Split(delimiter2)
                                Dim counter2 = 0
                                Dim d = 0
                                Dim m = 0
                                Dim y = 0
                                If counter1 = 0 Then
                                    For Each substring2 In substrings2
                                        If (substring2 <> "/") Then
                                            If counter2 = 0 Then

                                                m = substring2
                                            ElseIf counter2 = 1 Then
                                                d = substring2
                                            Else
                                                y = substring2
                                            End If
                                        End If
                                        counter2 = counter2 + 1
                                    Next
                                    Dim date1 As New Date(y, m, d, 0, 0, 0)
                                    entrada = date1
                                Else
                                    For Each substring2 In substrings2
                                        If (substring2 <> "/") Then
                                            If counter2 = 0 Then

                                                m = substring2
                                            ElseIf counter2 = 1 Then
                                                d = substring2
                                            Else
                                                y = substring2
                                            End If
                                        End If
                                        counter2 = counter2 + 1
                                    Next
                                    Dim date1 As New Date(y, m, d, 0, 0, 0)
                                    salida = date1
                                End If
                            End If
                            counter1 = counter1 + 1
                        Next

                        'Dim entrada As Date = calendarEntrada.SelectedDate
                        'Dim salida As Date = calendarSalida.SelectedDate

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
                                    Dim descripcion As String = txt_observaciones.Text


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
                                        '    descripcion &= "<br /><strong> Lugar donde se recoge:</strong>" & txt_pickup.Text
                                        'End If
                                        'If trPlacetoLeave.Visible = True Then
                                        '    descripcion &= "<br /><strong>Lugar donde se deja:</strong>" & txt_leave.Text

                                        'End If
                                        descripcion &= "<br/><strong>For Pick up place and Drop off place:</strong> Service subject to availability. Guests must contact Manatus Hotel in order to establish the pick up or drop off place."
                                    End If

                                    Dim ReservacionActual As Integer = controladora.CrearReservacion(id_entidad, fechaInicio, fechaFin, id_entidad, True, "", 0, descripcion)

                                    If ReservacionActual > 0 Then

                                        If agregaItem(ReservacionActual, id_entidad, id_producto, noches, nochesAdicionales, habitacionesDisponibles, gv_ResultadosDisponibles, fechaInicio, fechaFin) Then
                                            exito(ReservacionActual)
                                        Else
                                            mensajeErrorReservacion()
                                        End If
                                        'If agregaItem(ReservacionActual, id_entidad, id_producto, noches, nochesAdicionales, habitacionesDisponibles, gv_ResultadosDisponibles, fechaInicio, fechaFin) Then
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
    End Sub

    Protected Sub reservarconPaquete()

        Dim entrada As Date
        Dim salida As Date

        'separador para el rango de fecha
        Dim rango = TxtCheckinCheckout.Text
        Dim delimiter As Char = " - "
        Dim substrings() As String = rango.Split(delimiter)
        Dim counter1 = 0

        For Each substring In substrings
            If (substring <> "-") Then
                'separador para el checkin
                Dim delimiter2 As Char = "/"
                Dim substrings2() As String = substring.Split(delimiter2)
                Dim counter2 = 0
                Dim d = 0
                Dim m = 0
                Dim y = 0
                If counter1 = 0 Then
                    For Each substring2 In substrings2
                        If (substring2 <> "/") Then
                            If counter2 = 0 Then

                                m = substring2
                            ElseIf counter2 = 1 Then
                                d = substring2
                            Else
                                y = substring2
                            End If
                        End If
                        counter2 = counter2 + 1
                    Next
                    Dim date1 As New Date(y, m, d, 0, 0, 0)
                    entrada = date1
                Else
                    For Each substring2 In substrings2
                        If (substring2 <> "/") Then
                            If counter2 = 0 Then

                                m = substring2
                            ElseIf counter2 = 1 Then
                                d = substring2
                            Else
                                y = substring2
                            End If
                        End If
                        counter2 = counter2 + 1
                    Next
                    Dim date1 As New Date(y, m, d, 0, 0, 0)
                    salida = date1
                End If
            End If
            counter1 = counter1 + 1
        Next

        'Dim entrada As Date = calendarEntrada.SelectedDate
        'Dim salida As Date = calendarSalida.SelectedDate

        Dim DateEntrada As New Date(entrada.Year, entrada.Month, entrada.Day, 12, 0, 0)
        Dim DateSalida As New Date(salida.Year, salida.Month, salida.Day, 11, 0, 0)

        Dim paquete As Integer = Request.QueryString("paquete")

        'Paquete 2, noche extra
        Dim PromoIni As New Date(2015, 5, 1)
        Dim PromoFin As New Date(2015, 6, 30)


        If DateEntrada >= PromoIni And DateSalida <= PromoFin Then

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
                                id_temporada = 14 'Upgrades Package 2014
                            End If
                            '
                            habitacionesDeseadas = gv_ResultadosDisponibles.Rows.Count
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

                                        If agregaItem(ReservacionActual, id_entidad, id_producto, noches, nochesAdicionales, habitacionesDisponibles, gv_ResultadosDisponibles, fechaInicio, fechaFin) Then
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


            'lbl_erroFechas.Text = "El paquete de la noche gratis en el Hotel Grano de Oro es válido por el mes de Mayo a Junio 2013.<br/>"
            lbl_erroFechas.Visible = True
        End If



    End Sub







End Class
