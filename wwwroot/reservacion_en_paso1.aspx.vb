Imports Orbelink.Entity.Entidades
Imports Orbelink.Entity.Productos
Imports Orbelink.Entity.Reservaciones
Imports Orbelink.DBHandler
Imports Orbelink.Control.Reservaciones
Imports Orbelink.Control.Facturas
Imports Orbelink.DateAndTime.DateHandler
Imports Orbelink.DateAndTime
Imports Orbelink.Entity.Facturas
Imports System.Globalization


Partial Class reservacion_en_paso1
    Inherits Orbelink.FrontEnd6.PageBaseClass

    Private Const id_tipoEntidadUSUARIO As Integer = 2 'Usuario
    Private Const id_producto As Integer = 3 'Habitacion Sencilla
    Private Const horaEntrada As Integer = 12
    Private Const horaSalida As Integer = 11

    'variables para almacenar los calculos
    Public Shared GlobalIngresoSalida As String = ""
    Public Shared GlobalHabitaciones As Integer = 0
    Public Shared GlobalPersonas As Integer = 0
    Public Shared GlobalNoches As Integer = 0
    Public Shared GlobalNoches_adicionales As Integer = 0
    Public Shared GlobalCosto_estadia As Double = 0.0
    Public Shared GlobalCosto_noche_adicional As Double = 0.0

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Orbelink.DBHandler.LanguageHandler.CurrentLanguage = Orbelink.DBHandler.LanguageHandler.Language.INGLES
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then

            If Request.QueryString("exito") IsNot Nothing Then
                Try
                    Dim exito As Integer = Request.QueryString("exito")
                    cargarPanelExito(exito)
                Catch ex As Exception
                    cargarPanelExito(0)
                End Try
            End If

            selectPaises()
            If Request.QueryString("rango_fecha") IsNot Nothing Then
                Culture = "en-us"
                TxtCheckinCheckout.Text = Request.QueryString("rango_fecha")

            End If
            btn_reservar1.Visible = True

            lbl_erroFechas.Visible = False

            'muestra descripciones de paquetes custom por default
            cargarDescripcionCorrespondiente(3)

            Dim paquete As Integer = Request.QueryString("paquete")

            If paquete = 2 Then
                linkTerminosCondiciones.Visible = True
                linkTerminosCondiciones2.Visible = False

                pnl_terminospaquete.Visible = True
                Panel1.Visible = False

                linkTerminosCondiciones.NavigateUrl = "Terms_and_Conditions_Manatus_may_and_june.pdf"
                lbl_paquete.Text = "<br />Free extra Night at Manatus Hotel<br /><br />"
                'trHabitaciones.Attributes.Add("style", "display:table-row")

                lbl_paquete.Visible = True
            Else
                linkTerminosCondiciones2.Visible = True
                linkTerminosCondiciones.Visible = False
                pnl_terminospaquete.Visible = False
                Panel1.Visible = True
                'trHabitaciones.Attributes.Add("style", "display:table-row")

                lbl_paquete.Visible = False


            End If

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
                    lbl_ResultadoReservacion.Text = "Sorry, to continue with the payment you must first complete the step 2."
                    lbl_error_registro.Visible = True
                    lbl_error_registro.Text = "You must complete the form to continue with the payment"
                Else
                    lbl_ResultadoReservacion.Text = "Para continuar con el proceso de pago usted debe primero completar el paso 2"
                    lbl_error_registro.Visible = True

                    lbl_error_registro.Text = "Debe completar todo el formulario para poder continuar con el proceso de pago"
                End If

            End If
        Else
            Dim id_entidad As Integer = txt_nombre.ToolTip

            If Not updateEntidad(id_entidad, ddl_ubicacion.SelectedValue, txt_nombre.Text, "", txt_codPostal.Text, telefonoConArea, txt_email.Text, txt_direccion.Text) Then
                resul = False
                If Orbelink.DBHandler.LanguageHandler.CurrentLanguage = Orbelink.DBHandler.LanguageHandler.Language.INGLES Then
                    lbl_ResultadoReservacion.Text = "Sorry, to continue with the payment you must first complete the step 2."
                    lbl_error_registro.Visible = True
                    lbl_error_registro.Text = "You must complete the form to continue with the payment"
                Else
                    lbl_ResultadoReservacion.Text = "Para continuar con el proceso de pago usted debe primero completar el paso 2"
                    lbl_error_registro.Visible = True
                    lbl_error_registro.Text = "Debe completar todo el formulario para poder continuar con el proceso de pago"
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

                    Dim preciotransporte As Integer = 75

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

    Protected Function consultarPreciosNochesNormales(ByVal id_producto As Integer, ByVal noches As Integer, ByVal nochesadicionales As Integer, ByVal habitacionesDisponibles As Data.DataTable, ByVal theGridView As GridView, ByVal usarPrecioTransporte As Boolean, ByVal fechaInicio As Date, ByVal fechaFin As Date, Optional ByVal descuento As String = "") As Double
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
                If rdbtnlist_transporte2014.Visible = True Then

                    Dim preciotransporte As Integer = 75

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
                    result = result + controladora.GetPrecioNoches(id_producto, item.ordinal.Value, numeroadultos, 0, True, fechaInicio, fechaFin, 1, noches, nochesadicionales, descuento, Session("id_temporadaNueva"), precio_transporte, 1)
                Else
                    result = result + controladora.GetPrecioNoches(id_producto, item.ordinal.Value, numeroadultos, 0, True, fechaInicio, fechaFin, 1, noches, nochesadicionales, descuento, Session("id_temporadaNueva"), 0, 1)
                End If

            Catch ex As Exception
                result = result + 0
            End Try
        Next
        Return result
    End Function

    Protected Function consultarPreciosNochesAdicionales(ByVal id_producto As Integer, ByVal noches As Integer, ByVal nochesadicionales As Integer, ByVal habitacionesDisponibles As Data.DataTable, ByVal theGridView As GridView, ByVal fechaInicio As Date, ByVal fechaFin As Date, Optional ByVal descuento As String = "") As Double
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


                result = result + controladora.GetPrecioNochesAdicionales(id_producto, item.ordinal.Value, numeroadultos, 0, True, fechaInicio, fechaFin, 1, noches, nochesadicionales, descuento, Session("id_temporadaNueva"), 1)


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

                Dim preciotransporte As Integer = 75

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





                Dim preciotransporte As Integer = 75

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


        Select Case rdbtnlist_transporte2014.SelectedValue
            Case 1
                transporte = "<br/>Transfer to Manatus"
            Case 2
                transporte = "<br/>Transfer back to San Jose"
            Case 3
                transporte = "<br/>Round Trip"
        End Select




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

    Protected Sub cargarPanelExito(ByVal exito As Integer)
        If Request.QueryString("exito") IsNot Nothing Then
            lbl_ResultadoReservacion.Visible = True
            lbl_ResultadoHabitaciones.Visible = False
            lbl_erroFechas.Visible = False
            lbl_error_registro.Visible = False

            pnl_contenido.Visible = False
            tabs.Visible = False
            pnl_exito.Visible = True


            If Request.QueryString("exito") = 1 Then
                Response.Redirect("reservacionExitosa_en.aspx")

            Else
                lbl_exito.ForeColor = Drawing.Color.Red
                If Request.QueryString("exito") = 0 Then
                    If Orbelink.DBHandler.LanguageHandler.CurrentLanguage = Orbelink.DBHandler.LanguageHandler.Language.INGLES Then
                        lbl_exito.Text = "<br/><br/>Sorry, your transacction was cancelled. Please click <a href='reservacion_en_paso1.aspx'>HERE</a> to try again.<br/><br/>"
                    Else
                        lbl_exito.Text = "<br/><br/>Lo sentimos, su transacción fue cancelada.  Por favor haga click <a href='reservacion_es_paso1.aspx'>AQUÍ</a> para intentar de nuevo.<br/><br/>"
                    End If
                End If
            End If
        Else

        End If
    End Sub

    Protected Sub cambioAPaso2()
        lbl_ResultadoHabitaciones.Visible = False
        lbl_erroFechas.Visible = False
        lbl_ResultadoReservacion.Visible = False
        lbl_error_registro.Visible = False
        If (String.Equals(lbl_precioSinTransporte.Text, "0") = False And String.Equals(lbl_precioConTransporte.Text, "0") = False) Then
            paso1.Visible = False
            paso2.Visible = True
            Dim class1 As String = step_1.CssClass
            class1 = class1.Replace("active", "")
            step_1.CssClass = class1
            step_2.CssClass = step_2.CssClass + " active"

            'cargar los datos de la reserva almacenados en variables globales
            ValueLblIngresoSalida.Text = TxtCheckinCheckout.Text
            Dim txt_habitaciones As String
            If (GlobalHabitaciones > 1) Then
                txt_habitaciones = CType(GlobalHabitaciones, String) + " rooms"
            Else
                txt_habitaciones = CType(GlobalHabitaciones, String) + " room"
            End If

            Dim txt_personas As String
            If (GlobalPersonas > 1) Then
                txt_personas = CType(GlobalPersonas, String) + " people"
            Else
                txt_personas = CType(GlobalPersonas, String) + " person"
            End If

            Dim texto_servicio As String = txt_habitaciones + " for " + txt_personas + ", " + CType(GlobalNoches, String) + " night(s), " + CType(GlobalNoches_adicionales, String) + " additional night(s). Taxes included."

            If rdbtnlist_transporte2014.SelectedValue <> 4 Then
                texto_servicio += " Transport included."
                Session("incluye_transporte") = "true"
            Else
                Session("incluye_transporte") = "false"
            End If
            ValueLblServicio.Text = texto_servicio
            ValueLblPersonas.Text = CType(GlobalPersonas, String)
            ValueLblHabitaciones.Text = CType(GlobalHabitaciones, String)
            ValueLblCostoSinTransporte.Text = String.Format("{0:$###,###,###.##}", GlobalCosto_estadia)
            If GlobalCosto_noche_adicional = 0.0 Then
                ValueLblCostoAdicional.Text = "$0.0"
            Else
                ValueLblCostoAdicional.Text = String.Format("{0:$###,###,###.##}", GlobalCosto_noche_adicional)
            End If
            ValueLblCostoTotal.Text = String.Format("{0:$###,###,###.##}", (GlobalCosto_estadia + GlobalCosto_noche_adicional))
        Else
            lbl_ResultadoReservacion.Visible = True
            lbl_ResultadoReservacion.ForeColor = Drawing.Color.Red
            lbl_ResultadoReservacion.Text = "You need to choose your reservations days and rooms to continue with the process"
        End If
    End Sub

    Protected Sub cambioAPaso1()
        lbl_ResultadoHabitaciones.Visible = False
        lbl_erroFechas.Visible = False
        lbl_ResultadoReservacion.Visible = False
        lbl_error_registro.Visible = False
        paso2.Visible = False
        paso1.Visible = True

        Dim class2 As String = step_2.CssClass
        class2 = class2.Replace("active", "")
        step_2.CssClass = class2
        step_1.CssClass = step_1.CssClass + " active"
    End Sub

    Protected Sub cargarDescripcionCorrespondiente(ByVal tipoReserva As Integer)
        des_paq_2_1.Visible = False
        des_paq_3_2.Visible = False
        des_paq_custom.Visible = False
        inc_paq_2_1.Visible = False
        inc_paq_3_2.Visible = False
        inc_paq_custom.Visible = False
        tar_paq_2_1.Visible = False
        tar_paq_3_2.Visible = False
        tar_paq_custom.Visible = False

        Select Case tipoReserva
            Case 1
                '2 dias 1 noche
                des_paq_2_1.Visible = True
                inc_paq_2_1.Visible = True
                tar_paq_2_1.Visible = True

            Case 2
                '3 dias 2 noches
                des_paq_3_2.Visible = True
                inc_paq_3_2.Visible = True
                tar_paq_3_2.Visible = True
            Case 3
                'custom package
                des_paq_custom.Visible = True
                inc_paq_custom.Visible = True
                tar_paq_custom.Visible = True

        End Select
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
                lbl_ResultadoReservacion.Text = "Sorry, there are no rooms available for the chosen dates"
            Else
                lbl_ResultadoReservacion.Text = "Lo sentimos, no quedan habitaciones disponibles para esa fecha "
            End If
        Else
            If Orbelink.DBHandler.LanguageHandler.CurrentLanguage = Orbelink.DBHandler.LanguageHandler.Language.INGLES Then
                lbl_ResultadoReservacion.Text = "Sorry there are only " & habitacionesDisponibles & " available rooms for this date"
            Else
                lbl_ResultadoReservacion.Text = "Lo sentimos solo quedan " & habitacionesDisponibles & " habitaciones disponibles para esa fecha "
            End If
        End If

    End Sub

    Protected Sub mensajeErrorFechas()
        lbl_ResultadoReservacion.Visible = True
        lbl_precioConTransporte.Text = 0
        lbl_precioSinTransporte.Text = 0
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
            lbl_ResultadoReservacion.Text = "Error al calcular la reservación"
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
        Dim control As New ControladorReservaciones(connection, Resources.Reservaciones_Resources.ResourceManager)
        Dim carrito As New FacturasHandler(connection)
        Dim factura As New Factura
        Session("id_reservacion") = ReservacionActual
        If carrito.ActualizarFactura(factura, control.obtenerFactura(ReservacionActual)) Then
            control.Emails(ReservacionActual, True)
        End If



        factura.id_TipoFactura.Value = FacturasHandler.Config_TipoFacturaBNCR
        If carrito.ActualizarFactura(factura, control.obtenerFactura(ReservacionActual)) Then
            control.Emails(ReservacionActual)
            Response.Redirect("checkoutbncr.aspx")
            'Response.Redirect("~/BNCR/IntermediaBNCR.aspx", False)
        End If

    End Sub

    Protected Sub rdbtnlist_transporte2014_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles rdbtnlist_transporte2014.SelectedIndexChanged
        lbl_ResultadoHabitaciones.Visible = False
        lbl_erroFechas.Visible = False
        lbl_ResultadoReservacion.Visible = False
        lbl_error_registro.Visible = False
        If TxtCheckinCheckout.Text.Length > 0 And TxtCheckinCheckout.Text.Contains(" - ") Then
            If (gv_ResultadosDisponibles.Rows.Count > 0) Then
                If rdbtnlist_transporte2014.SelectedValue.Length > 0 Then
                    CalculoPrecioConTransporte()
                Else
                    lbl_precioConTransporte.Text = 0
                End If

            Else
                lbl_precioSinTransporte.Text = 0
                lbl_precioConTransporte.Text = 0
            End If
        Else
            lbl_precioSinTransporte.Text = 0
            lbl_precioConTransporte.Text = 0
        End If
    End Sub

    Protected Sub gv_ResultadosDisponibles_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gv_ResultadosDisponibles.RowCommand
        lbl_ResultadoHabitaciones.Visible = False
        lbl_erroFechas.Visible = False
        lbl_ResultadoReservacion.Visible = False
        lbl_error_registro.Visible = False
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
                Else
                    lbl_precioSinTransporte.Text = 0
                    lbl_precioConTransporte.Text = 0
                End If
            Else
                lbl_precioSinTransporte.Text = 0
                lbl_precioConTransporte.Text = 0
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
        lbl_error_registro.Visible = False

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
            Else
                lbl_precioSinTransporte.Text = 0
                lbl_precioConTransporte.Text = 0
            End If
        Else
            lbl_precioSinTransporte.Text = 0
            lbl_precioConTransporte.Text = 0
        End If
    End Sub

    Protected Sub ddl_personas_SelectedIndexChanged(sender As Object, e As System.EventArgs)
        lbl_ResultadoHabitaciones.Visible = False
        lbl_erroFechas.Visible = False
        lbl_ResultadoReservacion.Visible = False
        lbl_error_registro.Visible = False
        If TxtCheckinCheckout.Text.Length > 0 And TxtCheckinCheckout.Text.Contains(" - ") Then
            If (gv_ResultadosDisponibles.Rows.Count > 0) Then
                CalculoPrecio()
            Else
                lbl_precioSinTransporte.Text = 0
                lbl_precioConTransporte.Text = 0
            End If
        Else
            lbl_precioSinTransporte.Text = 0
            lbl_precioConTransporte.Text = 0
        End If

    End Sub

    Protected Sub book_now_1_Click(sender As Object, e As System.EventArgs) Handles book_now_1.Click
        cambioAPaso2()
    End Sub

    Protected Sub book_now_2_Click(sender As Object, e As System.EventArgs) Handles book_now_2.Click
        cambioAPaso2()
    End Sub

    Protected Sub LinkEditarInformacion_Click(sender As Object, e As System.EventArgs) Handles LinkEditarInformacion.Click
        cambioAPaso1()
    End Sub

    Protected Sub step_2_link_Click(sender As Object, e As System.EventArgs) Handles step_2_link.Click
        cambioAPaso2()
    End Sub

    Protected Sub step_2_point_Click(sender As Object, e As System.EventArgs) Handles step_2_point.Click
        cambioAPaso2()
    End Sub

    Protected Sub step_1_link_Click(sender As Object, e As System.EventArgs) Handles step_1_link.Click

        cambioAPaso1()

    End Sub

    Protected Sub step_1_point_Click(sender As Object, e As System.EventArgs) Handles step_1_point.Click
        cambioAPaso1()
    End Sub

    Protected Sub step_3_link_Click(sender As Object, e As System.EventArgs) Handles step_3_link.Click
        reservarNormal()
    End Sub

    Protected Sub step_3_point_Click(sender As Object, e As System.EventArgs) Handles step_3_point.Click
        reservarNormal()
    End Sub

    Protected Sub btn_aPaso3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btn_aPaso3.Click
        reservarNormal()

    End Sub

    Protected Sub AplicarSeleccion_Click(sender As Object, e As EventArgs) Handles AplicarSeleccion.Click
        lbl_ResultadoHabitaciones.Visible = False
        lbl_erroFechas.Visible = False
        lbl_ResultadoReservacion.Visible = False
        lbl_error_registro.Visible = False
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
            'almacenar el rango de fecha en una sesion
            Session("rango") = TxtCheckinCheckout.Text
            If (gv_ResultadosDisponibles.Rows.Count > 0) Then
                CalculoPrecio()
            Else
                lbl_precioSinTransporte.Text = 0
                lbl_precioConTransporte.Text = 0
            End If
        Else
            lbl_precioSinTransporte.Text = 0
            lbl_precioConTransporte.Text = 0
        End If

    End Sub

    Protected Sub TxtCheckinCheckout_TextChanged(sender As Object, e As System.EventArgs) Handles TxtCheckinCheckout.TextChanged

        If TxtCheckinCheckout.Text.Length > 0 And TxtCheckinCheckout.Text.Contains(" - ") Then
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

                min = DateValue("2015/12/01") ' Abajo
                max = DateValue("2015/12/30") ' Abajo

                If entrada >= min And entrada <= max Then
                    salida = entrada.AddDays(3)
                Else
                    salida = entrada
                End If

                TxtCheckinCheckout.Text = Convert.ToString(entrada.Month) + "/" + Convert.ToString(entrada.Day) + "/" + Convert.ToString(entrada.Year) + " - " + Convert.ToString(salida.Month) + "/" + Convert.ToString(salida.Day) + "/" + Convert.ToString(salida.Year)

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
        End If
        


    End Sub

    'Calculos de Precio  y reservaciones
    Protected Sub CalculoPrecio()

        Dim ready As Boolean = True
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
            GlobalIngresoSalida = rango
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
                GlobalNoches = noches
                Dim nochesAdicionales As Integer = total_de_noches - noches
                GlobalNoches_adicionales = nochesAdicionales
                If validarPrecios(id_temporada, id_producto, noches, gv_ResultadosDisponibles) Then

                    Dim habitacionesDisponibles As Data.DataTable = cargarItemsDisponibles(id_producto, fechaInicio, fechaFin)
                    If habitacionesDisponibles.Rows.Count >= habitacionesDeseadas Then
                        lbl_precioConTransporte.Text = 0
                        lbl_precioSinTransporte.Text=0
                        GlobalHabitaciones = habitacionesDeseadas
                        Dim total_personas As Integer = 0
                        For counter As Integer = 0 To gv_ResultadosDisponibles.Rows.Count - 1
                            Dim unItem As GridViewRow = gv_ResultadosDisponibles.Rows(counter)
                            Dim lbl_tipo_paquete As Label = unItem.FindControl("lbl_tipo_paquete")
                            lbl_tipo_paquete.Text = "Custom Package"
                            'Para cambiar el radio Button del paquete

                            'mostrar descripciones de paquetes custom por default
                            cargarDescripcionCorrespondiente(3)
                            If ((noches = 1) And (nochesAdicionales = 0)) Then
                                'mostrar descripciones de paquetes de 2 dias y una noche
                                lbl_tipo_paquete.Text = "2 days 1 night"
                                cargarDescripcionCorrespondiente(1)


                            ElseIf ((noches = 2) And (nochesAdicionales = 0)) Then
                                lbl_tipo_paquete.Text = "3 days 2 nights"
                                'mostrar descripciones de paquetes de 3 dias y dos noches
                                cargarDescripcionCorrespondiente(2)
                            End If
                            Dim lbl_precio_habitacion As Label = unItem.FindControl("lbl_precio_habitacion")
                            lbl_precio_habitacion.Text = "$ " + Convert.ToString(agregaItemTemporalIndividual(id_producto, noches, nochesAdicionales, habitacionesDisponibles, unItem, counter, False, fechaInicio, fechaFin))
                            Dim ddlPersonas As DropDownList = unItem.FindControl("ddl_personas")
                            total_personas = total_personas + ddlPersonas.SelectedValue
                        Next
                        GlobalPersonas = total_personas

                        Dim precioSintrasporte As Double = agregaItemTemporal(id_producto, noches, nochesAdicionales, habitacionesDisponibles, gv_ResultadosDisponibles, False, fechaInicio, fechaFin)
                        Dim precioContranporte As Double = agregaItemTemporal(id_producto, noches, nochesAdicionales, habitacionesDisponibles, gv_ResultadosDisponibles, True, fechaInicio, fechaFin)

                        Dim precioNochesNormales As Double = consultarPreciosNochesNormales(id_producto, noches, nochesAdicionales, habitacionesDisponibles, gv_ResultadosDisponibles, True, fechaInicio, fechaFin)
                        Dim precioNochesAdicionales As Double = consultarPreciosNochesAdicionales(id_producto, noches, nochesAdicionales, habitacionesDisponibles, gv_ResultadosDisponibles, fechaInicio, fechaFin)

                        GlobalCosto_estadia = precioNochesNormales
                        GlobalCosto_noche_adicional = precioNochesAdicionales

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
                        lbl_precioConTransporte.Text = 0
                        lbl_precioSinTransporte.Text = 0
                        If (habitacionesDisponibles.Rows.Count > 0) Then
                            cargarHabitacionesDeseadas(habitacionesDisponibles.Rows.Count)

                            Dim total_personas As Integer = 0

                            For counter As Integer = 0 To gv_ResultadosDisponibles.Rows.Count - 1
                                Dim unItem As GridViewRow = gv_ResultadosDisponibles.Rows(counter)
                                Dim lbl_tipo_paquete As Label = unItem.FindControl("lbl_tipo_paquete")
                                lbl_tipo_paquete.Text = "Paquete Personalizado"

                                'mostrar descripciones de paquetes custom por default
                                cargarDescripcionCorrespondiente(3)


                                'Para cambiar el radio Button del paquete
                                If ((noches = 1) And (nochesAdicionales = 0)) Then

                                    'mostrar descripciones de paquetes de 2 dias y una noche
                                    lbl_tipo_paquete.Text = "2 days 1 night"
                                    cargarDescripcionCorrespondiente(1)
                                ElseIf ((noches = 2) And (nochesAdicionales = 0)) Then
                                    lbl_tipo_paquete.Text = "3 days 2 nights"
                                    'mostrar descripciones de paquetes de 3 dias y dos noches
                                    cargarDescripcionCorrespondiente(2)
                                End If
                                Dim lbl_precio_habitacion As Label = unItem.FindControl("lbl_precio_habitacion")
                                lbl_precio_habitacion.Text = "$ " + Convert.ToString(agregaItemTemporalIndividual(id_producto, noches, nochesAdicionales, habitacionesDisponibles, unItem, counter, False, fechaInicio, fechaFin))

                                Dim ddlPersonas As DropDownList = unItem.FindControl("ddl_personas")
                                total_personas = total_personas + ddlPersonas.SelectedValue
                            Next
                            GlobalPersonas = total_personas

                            Dim precioSintrasporte As Double = agregaItemTemporal(id_producto, noches, nochesAdicionales, habitacionesDisponibles, gv_ResultadosDisponibles, False, fechaInicio, fechaFin)
                            Dim precioContranporte As Double = agregaItemTemporal(id_producto, noches, nochesAdicionales, habitacionesDisponibles, gv_ResultadosDisponibles, True, fechaInicio, fechaFin)

                            Dim precioNochesNormales As Double = consultarPreciosNochesNormales(id_producto, noches, nochesAdicionales, habitacionesDisponibles, gv_ResultadosDisponibles, True, fechaInicio, fechaFin)
                            Dim precioNochesAdicionales As Double = consultarPreciosNochesAdicionales(id_producto, noches, nochesAdicionales, habitacionesDisponibles, gv_ResultadosDisponibles, fechaInicio, fechaFin)

                            GlobalCosto_estadia = precioNochesNormales
                            GlobalCosto_noche_adicional = precioNochesAdicionales

                            If ((precioSintrasporte <> 0)) Then

                                lbl_precioSinTransporte.Text = precioSintrasporte

                                'exito(ReservacionActual)
                            Else
                                mensajeErrorReservacion()
                                ready = False

                            End If
                            If ((precioContranporte <> 0)) Then

                                lbl_precioConTransporte.Text = precioContranporte

                                'exito(ReservacionActual)
                            Else
                                mensajeErrorReservacion()
                                ready = False
                            End If

                        End If



                    End If
                End If
            Else
                mensajeErrorFechas()
                cargarHabitacionesDeseadas(0)
            End If

        End If

    End Sub

    Protected Sub CalculoPrecioConPaquete()
        'loader.Visible = True
        'loader.Visible = False
        Dim ready As Boolean = True
        Dim paquete As Integer = Request.QueryString("paquete")


        'Paquete 2, Noche extra

        Dim PromoIni As New Date(2015, 12, 1)
        Dim PromoFin As New Date(2015, 12, 30)



        Dim controladora As New ControladorReservaciones(connection, Resources.Reservaciones_Resources.ResourceManager)

        Dim entrada As Date
        Dim salida As Date

        'separador para el rango de fecha
        Dim rango = TxtCheckinCheckout.Text
        GlobalIngresoSalida = rango
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


        If entrada >= PromoIni And salida <= PromoFin Then
            'lbl_terminos.Visible = False
            pnl_exito.Visible = False
            lbl_ResultadoReservacion.ForeColor = Drawing.Color.Red
            'Dim entrada As Date = calendarEntrada.SelectedDate
            'Dim salida As Date = calendarSalida.SelectedDate

            Dim fechaInicio As Date = controladora.crearFechaHora(entrada, horaEntrada, 0)
            Dim fechaFin As Date = controladora.crearFechaHora(salida, horaSalida, 0)
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

                habitacionesDeseadas = gv_ResultadosDisponibles.Rows.Count
                Dim noches As Integer = controladora.NochesSegunTarifas(id_temporada, id_producto, 0, total_de_noches)
                GlobalNoches = noches
                Dim nochesAdicionales As Integer = total_de_noches - noches
                GlobalNoches_adicionales = nochesAdicionales

                If validarPrecios(id_temporada, id_producto, noches, gv_ResultadosDisponibles) Then

                    Dim habitacionesDisponibles As Data.DataTable = cargarItemsDisponibles(id_producto, fechaInicio, fechaFin)
                    If habitacionesDisponibles.Rows.Count >= habitacionesDeseadas Then
                        GlobalHabitaciones = habitacionesDeseadas
                        Dim total_personas As Integer = 0

                        For counter As Integer = 0 To gv_ResultadosDisponibles.Rows.Count - 1
                            Dim unItem As GridViewRow = gv_ResultadosDisponibles.Rows(counter)
                            Dim lbl_tipo_paquete As Label = unItem.FindControl("lbl_tipo_paquete")
                            lbl_tipo_paquete.Text = "1 free extra night Package"

                            'mostrar descripciones de paquetes custom por default
                            cargarDescripcionCorrespondiente(3)


                            'Para cambiar el radio Button del paquete
                            If ((noches = 1) And (nochesAdicionales = 0)) Then

                                lbl_tipo_paquete.Text = "2 days 1 night"
                                cargarDescripcionCorrespondiente(1)
                            ElseIf ((noches = 2) And (nochesAdicionales = 0)) Then
                                lbl_tipo_paquete.Text = "3 days 2 nights"
                                cargarDescripcionCorrespondiente(2)
                            End If
                            Dim lbl_precio_habitacion As Label = unItem.FindControl("lbl_precio_habitacion")
                            lbl_precio_habitacion.Text = "$ " + Convert.ToString(agregaItemTemporalIndividual(id_producto, noches, nochesAdicionales, habitacionesDisponibles, unItem, counter, False, fechaInicio, fechaFin))

                            Dim ddlPersonas As DropDownList = unItem.FindControl("ddl_personas")
                            total_personas = total_personas + ddlPersonas.SelectedValue
                        Next
                        GlobalPersonas = total_personas

                        Dim precioSintrasporte As Double = agregaItemTemporal(id_producto, noches, nochesAdicionales, habitacionesDisponibles, gv_ResultadosDisponibles, False, fechaInicio, fechaFin)
                        Dim precioContranporte As Double = agregaItemTemporal(id_producto, noches, nochesAdicionales, habitacionesDisponibles, gv_ResultadosDisponibles, True, fechaInicio, fechaFin)
                        Dim precioNochesNormales As Double = consultarPreciosNochesNormales(id_producto, noches, nochesAdicionales, habitacionesDisponibles, gv_ResultadosDisponibles, True, fechaInicio, fechaFin)
                        Dim precioNochesAdicionales As Double = consultarPreciosNochesAdicionales(id_producto, noches, nochesAdicionales, habitacionesDisponibles, gv_ResultadosDisponibles, fechaInicio, fechaFin)
                        GlobalCosto_estadia = precioNochesNormales
                        GlobalCosto_noche_adicional = precioNochesAdicionales
                        If ((precioSintrasporte <> 0)) Then

                            lbl_precioSinTransporte.Text = precioSintrasporte

                            'exito(ReservacionActual)
                        Else
                            mensajeErrorReservacion()
                            ready = False

                        End If
                        If ((precioContranporte <> 0)) Then

                            lbl_precioConTransporte.Text = precioContranporte
                            'exito(ReservacionActual)
                        Else
                            mensajeErrorReservacion()
                            ready = False
                        End If
                        terminosycondiciones()
                    Else
                        mensajeCantidadHabitaciones(habitacionesDisponibles.Rows.Count)
                        lbl_precioConTransporte.Text = 0
                        lbl_precioSinTransporte.Text = 0
                        If (habitacionesDisponibles.Rows.Count > 0) Then
                            cargarHabitacionesDeseadas(habitacionesDisponibles.Rows.Count)

                            Dim total_personas As Integer = 0

                            For counter As Integer = 0 To gv_ResultadosDisponibles.Rows.Count - 1
                                Dim unItem As GridViewRow = gv_ResultadosDisponibles.Rows(counter)
                                Dim lbl_tipo_paquete As Label = unItem.FindControl("lbl_tipo_paquete")

                                lbl_tipo_paquete.Text = "Custom Package"
                                'mostrar descripciones de paquetes custom por default
                                cargarDescripcionCorrespondiente(3)

                                'Para cambiar el radio Button del paquete
                                If ((noches = 1) And (nochesAdicionales = 0)) Then

                                    lbl_tipo_paquete.Text = "2 days 1 night"
                                    cargarDescripcionCorrespondiente(1)

                                ElseIf ((noches = 2) And (nochesAdicionales = 0)) Then
                                    lbl_tipo_paquete.Text = "3 days 2 nights"
                                    cargarDescripcionCorrespondiente(2)
                                End If
                                Dim lbl_precio_habitacion As Label = unItem.FindControl("lbl_precio_habitacion")
                                lbl_precio_habitacion.Text = "$ " + Convert.ToString(agregaItemTemporalIndividual(id_producto, noches, nochesAdicionales, habitacionesDisponibles, unItem, counter, False, fechaInicio, fechaFin))

                                Dim ddlPersonas As DropDownList = unItem.FindControl("ddl_personas")
                                total_personas = total_personas + ddlPersonas.SelectedValue
                            Next
                            GlobalPersonas = total_personas

                            Dim precioSintrasporte As Double = agregaItemTemporal(id_producto, noches, nochesAdicionales, habitacionesDisponibles, gv_ResultadosDisponibles, False, fechaInicio, fechaFin)
                            Dim precioContranporte As Double = agregaItemTemporal(id_producto, noches, nochesAdicionales, habitacionesDisponibles, gv_ResultadosDisponibles, True, fechaInicio, fechaFin)

                            Dim precioNochesNormales As Double = consultarPreciosNochesNormales(id_producto, noches, nochesAdicionales, habitacionesDisponibles, gv_ResultadosDisponibles, True, fechaInicio, fechaFin)
                            Dim precioNochesAdicionales As Double = consultarPreciosNochesAdicionales(id_producto, noches, nochesAdicionales, habitacionesDisponibles, gv_ResultadosDisponibles, fechaInicio, fechaFin)
                            GlobalCosto_estadia = precioNochesNormales
                            GlobalCosto_noche_adicional = precioNochesAdicionales

                            If ((precioSintrasporte <> 0)) Then

                                lbl_precioSinTransporte.Text = precioSintrasporte

                                'exito(ReservacionActual)
                            Else
                                mensajeErrorReservacion()
                                ready = False

                            End If
                            If ((precioContranporte <> 0)) Then

                                lbl_precioConTransporte.Text = precioContranporte

                                'exito(ReservacionActual)
                            Else
                                mensajeErrorReservacion()
                                ready = False
                            End If
                        End If
                        

                        'terminosycondiciones()

                    End If
                End If
            Else
                mensajeErrorFechas()
                ready = False
                cargarHabitacionesDeseadas(0)
            End If
        End If
    End Sub

    Protected Sub CalculoPrecioConTransporte()


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
                GlobalNoches = noches
                Dim nochesAdicionales As Integer = total_de_noches - noches

                'Para cambiar el radio Button del paquete



                If validarPrecios(id_temporada, id_producto, noches, gv_ResultadosDisponibles) Then

                    Dim habitacionesDisponibles As Data.DataTable = cargarItemsDisponibles(id_producto, fechaInicio, fechaFin)
                    If habitacionesDisponibles.Rows.Count >= habitacionesDeseadas Then


                        Dim precioContransporte As Double = agregaItemTemporal(id_producto, noches, nochesAdicionales, habitacionesDisponibles, gv_ResultadosDisponibles, True, fechaInicio, fechaFin)

                        Dim precioNochesNormales As Double = consultarPreciosNochesNormales(id_producto, noches, nochesAdicionales, habitacionesDisponibles, gv_ResultadosDisponibles, True, fechaInicio, fechaFin)
                        Dim precioNochesAdicionales As Double = consultarPreciosNochesAdicionales(id_producto, noches, nochesAdicionales, habitacionesDisponibles, gv_ResultadosDisponibles, fechaInicio, fechaFin)

                        GlobalCosto_estadia = precioNochesNormales
                        GlobalCosto_noche_adicional = precioNochesAdicionales

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
                cargarHabitacionesDeseadas(0)
            End If

        End If

    End Sub

    Protected Sub reservarNormal()
        lbl_ResultadoHabitaciones.Visible = False
        lbl_erroFechas.Visible = False
        lbl_ResultadoReservacion.Visible = False
        lbl_error_registro.Visible = False
        Dim paquete As Integer = Request.QueryString("paquete")
        'paquete = 0
        If paquete = 2 Then
            reservarconPaquete()
        ElseIf paquete = 4 Then
            reservaTortuga()
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




                                    Dim preciotransporte As Integer = 75

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
        Dim ready As Boolean = True
        Dim paquete As Integer = Request.QueryString("paquete")


        'Paquete 2, Noche extra

        Dim PromoIni As New Date(2015, 12, 1)
        Dim PromoFin As New Date(2015, 12, 30)



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



        If entrada >= PromoIni And salida <= PromoFin Then
            'lbl_terminos.Visible = False
            pnl_exito.Visible = False
            lbl_ResultadoReservacion.ForeColor = Drawing.Color.Red
            'Dim entrada As Date = calendarEntrada.SelectedDate
            'Dim salida As Date = calendarSalida.SelectedDate

            Dim fechaInicio As Date = controladora.crearFechaHora(entrada, horaEntrada, 0)
            Dim fechaFin As Date = controladora.crearFechaHora(salida, horaSalida, 0)
            fechaInicio = DateHandler.ToUtcFromLocalizedDate(fechaInicio)
            fechaFin = DateHandler.ToUtcFromLocalizedDate(fechaFin)

            Dim id_entidad As Integer = 0
            If InsertarModificar(id_tipoEntidadUSUARIO) Then
                If txt_nombre.ToolTip <> "" Then
                    id_entidad = txt_nombre.ToolTip
                End If
                If id_entidad > 0 Then
                    If txt_observaciones.Text.Length < 7000 Then
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

                                    Dim preciotransporte As Integer = 75

                                    If entrada.Year = 2014 Then
                                        preciotransporte = 70

                                    ElseIf entrada.Year = 2015 Then
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

    Protected Sub reservaTortuga()
            Dim PromoIni As Date
            Dim PromoFin As Date

            Dim boletin As String = Request.QueryString("newsletter")

            If boletin = "1" Then 'Viene del Boletin 
            PromoIni = New Date(2015, 12, 1)
            PromoFin = New Date(2015, 12, 31)
                ' lbl_erroFechas.Text = "Package valid for the months of july through october 2011 <br/>"
            Else 'No viene del Boletin
                PromoIni = New Date(2011, 9, 1)
                PromoFin = New Date(2011, 10, 31)
                lbl_erroFechas.Text = "Package valid for the months of september through october 2011 <br/>"
            End If

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


        Dim paquete As Integer = Request.QueryString("paquete")
        If entrada >= PromoIni And salida <= PromoFin Then
            'lbl_terminos.Visible = False
            pnl_exito.Visible = False
            lbl_ResultadoReservacion.ForeColor = Drawing.Color.Red
            'Dim entrada As Date = calendarEntrada.SelectedDate
            'Dim salida As Date = calendarSalida.SelectedDate

            Dim fechaInicio As Date = controladora.crearFechaHora(entrada, horaEntrada, 0)
            Dim fechaFin As Date = controladora.crearFechaHora(salida, horaSalida, 0)
            fechaInicio = DateHandler.ToUtcFromLocalizedDate(fechaInicio)
            fechaFin = DateHandler.ToUtcFromLocalizedDate(fechaFin)

            Dim id_entidad As Integer = 0
            If InsertarModificar(id_tipoEntidadUSUARIO) Then
                If txt_nombre.ToolTip <> "" Then
                    id_entidad = txt_nombre.ToolTip
                End If

                If id_entidad > 0 Then
                    If txt_observaciones.Text.Length < 7000 Then


                        Dim total_de_noches As Integer = controladora.TotalNoches(fechaInicio, fechaFin)
                        If total_de_noches > 0 Then

                            Dim id_temporada As Integer

                            Dim habitacionesDeseadas As Integer


                            id_temporada = controladora.buscarTemporada(fechaInicio, fechaInicio.AddDays(1)).id_Temporada
                            habitacionesDeseadas = gv_ResultadosDisponibles.Rows.Count


                            Dim noches As Integer = controladora.NochesSegunTarifas(id_temporada, id_producto, 0, total_de_noches)
                            Dim nochesAdicionales As Integer = total_de_noches - noches
                            If validarPrecios(id_temporada, id_producto, noches, gv_ResultadosDisponibles) Then

                                Dim habitacionesDisponibles As Data.DataTable = cargarItemsDisponibles(id_producto, fechaInicio, fechaFin)
                                If habitacionesDisponibles.Rows.Count >= habitacionesDeseadas Then
                                    Dim descripcion As String = txt_observaciones.Text

                                    Dim preciotransporte As Integer = 75

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

                                    ' CODIGO PARA LA PROMOCION DEL DESCUENTO O DE VIAJE EN AVION
                                    'Dim PromoIni As New Date(2010, 9, 1)
                                    'Dim PromoFin As New Date(2010, 11, 30)
                                    'Dim descuento As String = ""

                                    'If DateEntrada >= PromoIni And DateSalida <= PromoFin Then
                                    '    If rbtnlst_Promocion.SelectedValue = "1" Then
                                    '        If txtDateEntrada.Text <> "" And txtDateSalida.Text <> "" Then
                                    '            If DateEntrada > Date.Now.AddDays(7) Then
                                    '                descripcion &= "<br /><br /><strong> Promotion: You have selected the plane trip option (San José-Torguero). We will contact you soon with further details!</strong><br />"
                                    '            Else
                                    '                mensajeErrorFechaVueloPromo()
                                    '                Exit Sub
                                    '            End If
                                    '        End If
                                    '    ElseIf rbtnlst_Promocion.SelectedValue = "0" Then
                                    '        If DateEntrada >= PromoIni And DateSalida <= PromoFin Then
                                    '            descuento = "10" 'representa el descuento del 10%
                                    '        End If
                                    '    End If
                                    'End If

                                    Select Case paquete


                                        Case 4
                                            descripcion &= "<br /><br /><strong> Package: FREE TURTLE NESTING TOUR, Courtesy Turtle Tour on nesting season.</strong><br />"

                                        Case 5
                                            descripcion &= "<br /><br /><strong> Package: FREE </strong><br />"

                                    End Select

                                    Dim ReservacionActual As Integer = controladora.CrearReservacion(id_entidad, fechaInicio, fechaFin, id_entidad, True, "", 0, descripcion)

                                    If ReservacionActual > 0 Then
                                        ' CODIGO PARA LA PROMOCION DEL DESCUENTO O DE VIAJE EN AVION
                                        'If agregaItem(ReservacionActual, id_entidad, id_producto, noches, nochesAdicionales, habitacionesDisponibles, gv_ResultadosDisponibles, descuento) Then
                                        If agregaItem(ReservacionActual, id_entidad, id_producto, noches, nochesAdicionales, habitacionesDisponibles, gv_ResultadosDisponibles, fechaInicio, fechaFin) Then
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

        Else
            lbl_erroFechas.Visible = True
        End If

    End Sub




End Class
