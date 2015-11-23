<%@ Page Language="VB" AutoEventWireup="false" CodeFile="reservacion_es_paso1.aspx.vb"
    Inherits="reservacion_es_paso1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Reservaciones online, reservar hotel en Tortuguero </title>
    <link rel="shortcut icon" href="images/images2/favicon.ico" />
    <meta charset="UTF-8">
    <meta content="width=device-width; initial-scale=1.0; maximum-scale=1.0; user-scalable=0;"
        name="viewport" />
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="y_key" content="64cc9486b65ce6b5" />
    <meta name="revisit-after" content="4 days" />
    <meta name="robots" content="INDEX,FOLLOW" />
    <meta name="GOOGLEBOT" content="INDEX, FOLLOW" />
    <meta name="author" content="Poveda" />
    <meta name="keywords" content="reservaciones On-line, reservas, hotel tortuguero" />
    <meta name="description" content="Reserve ya en el hotel Manatus Tortuguero Costa Rica. Su mejor opción, una vacación de primera clase.  Reservaciones on-line. Reservas" />
    <!--<link href="styles/styles_new.css" rel="stylesheet" type="text/css" />
    <link href="styles/stylereservations.css" rel="stylesheet" type="text/css" />-->
    <link type="text/css" rel="stylesheet" href="http://fonts.googleapis.com/css?family=Open+Sans:400,300,600,700|Open+Sans+Condensed:300,700|Raleway:400,300,600,700">
    <link href="styles/stylereservations_nuevo.css" rel="stylesheet" type="text/css" />
    <!--[if gte IE 9]><!-->
    <link rel="Stylesheet" href="styles/stylereservation_nuevo_responsivo.css" type="text/css" />
    <!--<![endif]-->
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
    <script type="text/javascript" src="http://booking.manatuscostarica.com/js/myJS.js"></script>
    <script type="text/javascript" src="http://booking.manatuscostarica.com/js/tinynav.min.js"></script>

    <!-- scripts range datepicker -->
    <script type="text/javascript" src="datepicker/js/datepicker-es.js"></script>
    <script type="text/javascript" src="datepicker/js/eye.js"></script>
    <script type="text/javascript" src="datepicker/js/utils.js"></script>
    <script type="text/javascript" src="datepicker/js/layout-es.js"></script>    
    <!-- end scripts range datepicker -->

    <!-- style range datepicker -->
    <link type="text/css" rel="stylesheet" href="datepicker/css/datepicker.css" />
    <link type="text/css" rel="stylesheet" href="datepicker/css/layout.css" />
    <!-- end style range datepicker -->

    <style type="text/css">
        /*Codigo de css para el pop up*/#fade
        {
            /*--Transparent background layer--*/
            display: none; /*--hidden by default--*/
            background: #000;
            position: fixed;
            left: 0;
            top: 0;
            width: 100%;
            height: 100%;
            opacity: .80;
            z-index: 9999;
        }
        .popup_block
        {
            display: none; /*--hidden by default--*/
            background: #fff;
            padding: 20px;
            border: 20px solid #ddd;
            float: left;
            font-size: 1.2em;
            position: fixed;
            top: 50%;
            left: 50%;
            z-index: 99999; /*--CSS3 Box Shadows--*/
            -webkit-box-shadow: 0px 0px 20px #000;
            -moz-box-shadow: 0px 0px 20px #000;
            box-shadow: 0px 0px 20px #000; /*--CSS3 Rounded Corners--*/
            -webkit-border-radius: 10px;
            -moz-border-radius: 10px;
            border-radius: 10px;
        }
        img.btn_close
        {
            float: right;
            margin: -55px -55px 0 0;
        }
        /*--Making IE6 Understand Fixed Positioning--*/*html #fade
        {
            position: absolute;
        }
        *html .popup_block
        {
            position: absolute;
        }
    </style>
    <!--Start of Zopim Live Chat Script-->
    <script type="text/javascript">
        window.$zopim || (function (d, s) {
            var z = $zopim = function (c) { z._.push(c) }, $ = z.s =
d.createElement(s), e = d.getElementsByTagName(s)[0]; z.set = function (o) {
    z.set.
_.push(o)
}; z._ = []; z.set._ = []; $.async = !0; $.setAttribute('charset', 'utf-8');
            $.src = '//cdn.zopim.com/?1FZ3HqPURg49wsdlOVwgJorSzjrXqLEZ'; z.t = +new Date; $.
type = 'text/javascript'; e.parentNode.insertBefore($, e)
        })(document, 'script');
    </script>
    <!--End of Zopim Live Chat Script-->
    <!--[if lte IE 10]>
     <link type="text/css" rel="stylesheet" href="styles/stylereservations_nuevo_ie.css" />
<![endif]-->
    <!--[if lt IE 9]>
	<script src="http://html5shim.googlecode.com/svn/trunk/html5.js"></script>

<script type="text/javascript">
   document.createElement("nav");
   document.createElement("header");
   document.createElement("footer");
   document.createElement("section");
   document.createElement("article");
   document.createElement("aside");
   document.createElement("hgroup");
</script>
<![endif]-->
    <!--[if lte IE 7]>
        <link type="text/css" rel="stylesheet" href="styles/stylereservations_nuevo_ie7.css" />
        
  <![endif]-->
    <script type="text/javascript">

        var _gaq = _gaq || [];
        _gaq.push(['_setAccount', 'UA-7635413-1']);
        _gaq.push(['_setDomainName', 'manatuscostarica.com']);
        _gaq.push(['_setAllowLinker', true]);
        _gaq.push(['_trackPageview']);

        (function () {
            var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
            ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
            var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
        })();

    </script>
    <script>
        var urlactual = document.URL.replace(/#.*/, "");
        urlactual = urlactual.replace(/\?.*/, "");
        _gaq.push(['_trackEvent', 'Reservación', 'Paso 1', urlactual]);
    </script>
</head>
<body class="reservation-form es">
    <form id="form2" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="loader" runat="server">
    </div>
    <div id="header">
        <div id="header-inner" class="container-980">
            <div id="logo-box">
                <a href="http://manatuscostarica.com">
                    <img src="images/2014/logo.png" /></a>
            </div>
            <div class="menu-top-box">
                <div class="menu-idioma">
                    <ul>
                        <li><a href="reservacion_en.aspx">English</a></li>|<li><a href="reservacion_sp.aspx"
                            class="active">Español</a></li></ul>
                </div>
                <div class="menu-top">
                    <ul>
                        <li><a href="http://manatuscostarica.com/es/">Inicio</a></li>|<li><a href="http://booking.manatuscostarica.com/reservacion_sp.aspx">
                            Reservar</a></li>|<li><a href="http://manatuscostarica.com/es/gallery">Galeria</a></li>|<li>
                                <a href="http://manatuscostarica.com/es/contacto">Contacto</a></li></ul>
                </div>
                <div class="contact-info">
                    <p>
                        MANATUS HOTEL, TORTUGUERO COSTA RICA | RESERVACIONES: <a href="tel: +50622397364">(506)
                            2239.7364</a> | HOTEL: <a href="tel: +50627098197">(506) 2709.8197</a></p>
                </div>
            </div>
            <div class="menu-principal-box">
                <div class="menu-principal">
                    <ul class="mi-menu">
                        <li><a href="http://manatuscostarica.com/es/experiencia-manatus">Experiencia Manatus</a>
                            <ul>
                                <li><a href="http://manatuscostarica.com/es/habitaciones">Habitaciones Acogedoras</a></li>
                                <li><a href="http://manatuscostarica.com/es/jardines">Jardines y Alrededores del Hotel</a></li>
                                <li><a href="http://manatuscostarica.com/es/servicios">Servicios Exclusivos</a></li>
                                <li><a href="http://manatuscostarica.com/es/restaurante">Restaurante de Comida Caribeña</a></li>
                                <li><a href="http://manatuscostarica.com/es/piscina">Área de la Piscina</a></li>
                            </ul>
                        </li>
                        <li><a href="http://manatuscostarica.com/es/qué-hacer">¿Qué hacer?</a>
                            <ul>
                                <li><a href="http://manatuscostarica.com/es/tours-actividades">Tours de Aventura y Actividades</a></li>
                                <li><a href="http://manatuscostarica.com/es/pesca-deportiva">Tours de Pesca Deportiva</a></li>
                            </ul>
                        </li>
                        <li><a href="http://manatuscostarica.com/es/paquetes-tarifas">Paquetes &amp; Tarifas</a>
                            <ul>
                                <li><a href="http://manatuscostarica.com/es/paquetes-luna-de-miel">Paquetes de Luna
                                    de Miel</a></li>
                                <li><a href="http://manatuscostarica.com/es/paquetes-regulares-tarifas">Paquetes Regulares</a></li>
                                <li><a href="http://manatuscostarica.com/es/paquetes-especiales">Paquetes Especiales</a></li>
                            </ul>
                        </li>
                        <li><a href="http://manatuscostarica.com/es/area-de-tortuguero">Tortuguero</a></li>
                        <li class="active-trail"><a href="http://booking.manatuscostarica.com/reservacion_sp.aspx">
                            Reservaciones</a></li>
                        <li><a href="http://manatuscostarica.com/es/blog">Blog</a></li></ul>
                    <ul class="mi-menu-responsivo">
                        <li><a href="http://manatuscostarica.com/es/experiencia-manatus">Experiencia Manatus</a>
                        </li>
                        <li><a href="http://manatuscostarica.com/qué-hacer">¿Qué hacer?</a> </li>
                        <li><a href="http://manatuscostarica.com/es/paquetes-tarifas">Paquetes &amp; Tarifas</a>
                        </li>
                        <li><a href="http://manatuscostarica.com/es/area-de-tortuguero">Tortuguero</a></li>
                        <li class="active-trail"><a href="http://booking.manatuscostarica.com/reservacion_sp.aspx">
                            Reservaciones</a></li>
                        <li><a href="http://manatuscostarica.com/es/blog">Blog</a></li></ul>
                </div>
            </div>
            <div id="page-head">
                <div class="page-title">
                    <h1 title="Reservaciones On-line, reservas en el hotel Manatus" class="h1ReservaNew">
                        RESERVACIÓN</h1>
                </div>
                <div class="breadcrumbs">
                    <a href="http://manatuscostarica.com/es/" title="P&aacute;gina principal Hotel Manatus Tortuguero Costa Rica">
                        Inicio</a> / Reservación
                </div>
            </div>
        </div>
    </div>
    <div id="main-content">
        <div class="content container-980">
            <div class="above-content">
                <div class="titulo-logos">
                    <div class="nombre-hotel">
                        <h2>
                            Hotel Manatus</h2>
                    </div>
                    <div class="logos-premios">
                        <a href="http://www.tripadvisor.com.mx/Hotel_Review-g309268-d308651-Reviews-Manatus_Hotel-Tortuguero_Province_of_Limon.html">
                            <img src="images/logo-trip-1.jpg" /></a> <a href="http://www.tripadvisor.com.mx/Hotel_Review-g309268-d308651-Reviews-Manatus_Hotel-Tortuguero_Province_of_Limon.html">
                                <img src="images/logo-trip-2.jpg" /></a>
                        <img src="images/logo-rainforest.png" />
                        <div id="TA_excellent625" class="TA_excellent">
                            <ul id="zWYVpp9Tm" class="TA_links VlM6UjTeF">
                                <li id="9QVb1lq" class="mW6emucXI9Tk"><a target="_blank" href="http://www.tripadvisor.es/">
                                    <img src="http://e2.tacdn.com/img2/widget/tripadvisor_logo_115x18.gif" alt="TripAdvisor"
                                        class="widEXCIMG" id="CDSWIDEXCLOGO" /></a></li></ul>
                        </div>
                        <script src="http://www.jscache.com/wejs?wtype=excellent&amp;uniq=625&amp;locationId=308651&amp;lang=es&amp;display_version=2"></script>
                    </div>
                </div>
                <div class="step-process-box">
                    <div class="inner">
                        <div class="step-1 active">
                            <div class="step-title">
                                <p>
                                    <a href="">Disponibilidad</a></p>
                            </div>
                            <div class="step-point">
                                <a class="point" href="">1</a>
                            </div>
                        </div>
                        <div class="step-2">
                            <div class="step-title">
                                <p>
                                    <a href="">Datos Personales</a></p>
                            </div>
                            <div class="step-point">
                                <a class="point" href="">2</a>
                            </div>
                        </div>
                        <div class="step-3">
                            <div class="step-title">
                                <p>
                                    <a href="">Datos de pago y cancelación</a></p>
                            </div>
                            <div class="step-point">
                                <a class="point" href="">3</a>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="form-title">
                    <h2 class="h2Celeste">
                        <strong>Viva la experiencia</strong> Manatus</h2>
                    <div class="logo-ssl">
                        <img src="" /></div>
                </div>
            </div>
            <div class="content-box container-2-3">
                <asp:UpdatePanel ID="updtpanel_reservacion" runat="server">
                    <ContentTemplate>
                        <asp:Label ID="lbl_paquete" runat="server" Text="" Visible="false"></asp:Label>
                        <div>
                            <asp:Panel ID="panel" runat="server" Visible="true">
                                <asp:Panel ID="pnl_exito" runat="server" Visible="false">
                                    <center>
                                        <br />
                                        <br />
                                        <asp:Label ID="lbl_exito" runat="server" Text="" Visible="true"></asp:Label>
                                        <br />
                                        <br />
                                    </center>
                                </asp:Panel>
                                <asp:Panel ID="pnl_contenido" runat="server" Visible="true">
                                    <div class="contenedor-desc-reserva">
                                        <div class="contenedor-fechas-hab">

                                            <!-- rango de fecha -->
                                                <div id="widget">
                                                    <div id="widgetField">
                                                        <asp:Label ID="lblIngresoSalida" runat="server" Text="Ingreso y Salida"></asp:Label>
                                                        <asp:TextBox runat="server" ID="TxtCheckinCheckout" AutoPostBack="true" name="checkin-checkout" value='Seleccionar rango de fechas'></asp:TextBox>
                                                        <a class="btn" id="btn-reservar" href="javascript:void(0)">Reservar</a>
                                                    </div>
                                                    <div id="widgetCalendar" class="hidden">
                                                    </div>
                                                </div>
                                            <!-- fin rango de fecha -->

                                            <div class="fecha-field">
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                    <ContentTemplate>
                                                        <%-- AutoPostBack="true" -> CODIGO PARA LA PROMOCION DEL DESCUENTO O DE VIAJE EN AVION--%>
                                                        <asp:TextBox runat="server" ID="txtDateEntrada" AutoPostBack="true" CssClass="fechasNuevas"></asp:TextBox>
                                                        &nbsp;
                                                        <obout:Calendar runat="server" ID="calendarEntrada" DatePickerMode="true" TextBoxId="txtDateEntrada"
                                                            DatePickerImagePath="images/2014/bg-fecha-reservation.png" />
                                                        <asp:RequiredFieldValidator ID="rfv_fecha" runat="server" ControlToValidate="txtDateEntrada"
                                                            Display="Dynamic" ErrorMessage="Campo Requerido" ValidationGroup="registrese"></asp:RequiredFieldValidator>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                    <ContentTemplate>
                                                        <%--AutoPostBack="true" -> CODIGO PARA LA PROMOCION DEL DESCUENTO O DE VIAJE EN AVION--%>
                                                        <asp:TextBox runat="server" ID="txtDateSalida" AutoPostBack="true" CssClass="fechasNuevas"></asp:TextBox>
                                                        &nbsp;
                                                        <obout:Calendar runat="server" ID="calendarSalida" DatePickerMode="true" TextBoxId="txtDateSalida"
                                                            DatePickerImagePath="images/2014/bg-fecha-reservation.png" />
                                                        <asp:RequiredFieldValidator ID="rfv_fecha2" runat="server" ControlToValidate="txtDateSalida"
                                                            Display="Dynamic" ErrorMessage="Campo Requerido" ValidationGroup="registrese"></asp:RequiredFieldValidator>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <hr />
                                        <div class="contenedor-hab-personas">
                                            <h3>
                                                Paquete para sus vacaciones en Costa Rica</h3>
                                                <hr />
                                            
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Always">
                                                <ContentTemplate>
                                                    <asp:Panel ID="pnl_resultados" runat="server">
                                                        <asp:GridView ID="gv_ResultadosDisponibles" runat="server" ShowHeader="false" AutoGenerateColumns="false"
                                                            GridLines="None" Width="246px" >
                                                            <RowStyle HorizontalAlign="Left" />
                                                            <HeaderStyle Font-Bold="false" HorizontalAlign="Left" />
                                                            <Columns>

                                                            <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="borrarHabitacion" runat="server" ImageUrl="~/images/bg-borrar-habitacion.jpg"
                                                                    CommandName="borrarHabitacion" CommandArgument="<%# CType(Container,GridViewRow).RowIndex %>" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbl_nombre" CssClass="marginleft15" runat="server" Font-Bold="false"
                                                                            Font-Italic="true" ForeColor="#999999" Text="1 Habitación"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <div class="lbl_paquete_box">
                                                                            <asp:Label ID="lbl_tipo_paquete" runat="server" Text="Custom Package"></asp:Label>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <div class="box-select-personas">
                                                                            <asp:DropDownList ID="ddl_personas" runat="server" CssClass="dropdownsReserva">
                                                                                <asp:ListItem Text="1 person" Value="1"></asp:ListItem>
                                                                                <asp:ListItem Text="2 people" Value="2"></asp:ListItem>
                                                                                <asp:ListItem Text="3 people" Value="3"></asp:ListItem>
                                                                                <asp:ListItem Text="4 people" Value="4"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Font-Bold="True" HorizontalAlign="Right" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <div class="lbl_precio_habitacion">
                                                                            <asp:Label ID="lbl_precio_habitacion" runat="server" Text="$ 0"></asp:Label>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <RowStyle Font-Bold="true" />
                                                        </asp:GridView>

                                                        <asp:LinkButton ID="add_room" runat="server">+ Añadir habitaciones</asp:LinkButton>
                                                    </asp:Panel>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <asp:Label ID="lbl_ResultadoHabitaciones" runat="server" Text=""></asp:Label>
                                        
                                        <div class="desc-paquete">
                                            <div class="desc-paquete-inner">
                                                <div class="box-precio-sin-transporte">
                                                    <asp:LinkButton ID="btn_reservar1" runat="server" ToolTip="Reservar" ValidationGroup="registrese"
                                                        ForeColor="#FFFFFF" Visible="false">RESERVAR »</asp:LinkButton>
                                                    <div class="precio-sin-transporte">
                                                        <div class="precio-sin-transporte-value">
                                                            
                                                                $
                                                                <asp:Label ID="lbl_precioSinTransporte" runat="server" Text="0"></asp:Label>
                                                        </div>
                                                        <div class="precio-sin-transporte-letra-pequena">
                                                            <p>
                                                                * Incluye: impuestos, comidas y tour por los Canales de Tortuguero.</p>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="desc-paquete-labels-errores">
                                                <asp:Label ID="lbl_erroFechas" runat="server" Text="" ForeColor="red"></asp:Label>
                                                <asp:Label ID="lbl_ResultadoReservacion" runat="server" Text=""></asp:Label>
                                            </div>
                                        </div>
                                        <hr />
                                        <div id="traslado-box">
                                            <h3>
                                                ¿Necesita transporte hacia y desde Manatus?</h3>
                                            <div class="traslado-box-inner">
                                                <p>
                                                    Seleccione las opciones de traslado que requiere y agréguelas a su paquete:</p>
                                                <div class="bdbtnlist_transporte_box">
                                                    <asp:RadioButtonList ID="rdbtnlist_transporte2014" runat="server" AutoPostBack="true">
                                                        <asp:ListItem Value="1">Transfer to Manatus</asp:ListItem>
                                                        <asp:ListItem Value="2">Transfer back to San Jose</asp:ListItem>
                                                        <asp:ListItem Value="3">Round Trip</asp:ListItem>
                                                        <asp:ListItem Value="4" Selected="True">None</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="rdbtnlist_transporte2014"
                                                        Display="Dynamic" ErrorMessage="Campo Requerido" ValidationGroup="registrese"></asp:RequiredFieldValidator>
                                                </div>
                                                <hr />
                                                <div class="box-precio-con-transporte">
                                                    <div class="precio-con-transporte">
                                                        <div class="precio-con-transporte-value">
                                                            
                                                                <span>Total Cost of<br />
                                                                    hosting and transport</span> 
                                                            
                                                                $
                                                                <asp:Label ID="lbl_precioConTransporte" runat="server" Text="0"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                                
                                                <div style="display: none;">
                                                    <p>
                                                        Pick up place</p>
                                                </div>
                                                <div id="box_pickup" runat="server" visible="false">
                                                    <asp:TextBox ID="txt_pickup" runat="server" CssClass="textBoxNuevo"></asp:TextBox>
                                                </div>
                                                <div style="display: none;">
                                                    <p>
                                                        Drop off place</p>
                                                </div>
                                                <div id="box_leave" runat="server" visible="false">
                                                    <asp:TextBox ID="txt_leave" runat="server" CssClass="textBoxNuevo"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </asp:Panel>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="content-box container-1-3 sidebar">
                <div class="paddingBottomp10">
                    <h3 class="h3Sidebar">
                        <strong>Otros paquetes</strong> que podrían interesarle</h3>
                    <div class="sidebar-block-content">
                        <div class="sidebar-paquete sidebar-paquete-1">
                            <h3 class="paquete-sidebar-titulo">
                                1 noche extra gratis</h3>
                            <div class="paquete-sidebar-desc-precio">
                                <div class="paquete-sidebar-desc">
                                    <ul>
                                        <li>Upgrade del paquete de 2 noches/3 días a uno de 3 noches/4 días.</li>
                                        <li>Durante mayo y junio. </li>
                                        <li>Válido únicamente para extranjeros.</li>
                                    </ul>
                                </div>
                                <div class="paquete-sidebar-precio">
                                    <p class="paquete-precio">
                                        <span>Desde </span>$310</p>
                                    <p class="habitacion-desc">
                                        Habitación Doble</p>
                                </div>
                            </div>
                            <div class="paquete-sidebar-book-box">
                                <a class="paquets-sidebar-book-link poplight" rel="popup_code" href="#?w=408">Reservar</a>
                            </div>
                            <div class="sidebar-paquete-popup-1 popup_block" id="popup_code">
                                <div class="sidebar-paquete-popup-texto">
                                    <p>
                                        <strong>Contacte a un representante </strong>de Manatus para reservar este paquete</p>
                                </div>
                                <div class="sidebar-paquete-popup-botones">
                                    <div class="sidebar-paquete-popup-botones-form">
                                        <a href="http://manatus.net/es/contacto" class="boton-form-rojo">Formulario</a>
                                    </div>
                                    <div class="sidebar-paquete-popup-botones-chat">
                                        <a href="javascript:%20$zopim.livechat.window.show();" class="boton-chat-cafe">Chat
                                            en Vivo</a>
                                    </div>
                                </div>
                                <div class="sidebar-paquete-popup-contact-info">
                                    <p>
                                        <strong>Hotel Manatus</strong><br />
                                        Oficina: <a href="tel:+50622394854">(506) 2239-4854</a><br />
                                        Hotel: <a href="tel:+50627098197">(506) 2709-8197</a><br />
                                        Fax: (506) 2709-8198<br />
                                        <a href="mailto:info@manatuscostarica.com">info@manatuscostarica.com</a><
                                    </p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="footer">
        <div id="footer-inner" class="container-980">
            <div class="menu-footer-responsivo">
                <ul class="mi-menu-responsivo">
                    <li><a href="http://manatuscostarica.com/es/experiencia-manatus">Experiencia Manatus</a>
                    </li>
                    <li><a href="http://manatuscostarica.com/es/qué-hacer">¿Qué hacer?</a> </li>
                    <li><a href="http://manatuscostarica.com/es/paquetes-tarifas">Paquetes &amp; Tarifas</a>
                    </li>
                    <li><a href="http://manatuscostarica.com/es/area-de-tortuguero">Tortuguero</a></li>
                    <li class="active-trail"><a href="http://booking.manatuscostarica.com/reservacion_sp.aspx">
                        Reservaciones</a></li>
                    <li><a href="http://manatuscostarica.com/blog">Blog</a></li>
                </ul>
            </div>
            <div class="column1 column">
                <ul>
                    <li><a href="http://booking.manatuscostarica.com/reservacion_sp.aspx" class="active-trail">
                        Reservaciones</a></li>
                    <li><a href="http://manatuscostarica.com/es/experiencia-manatus">Experiencia Manatus</a></li>
                    <li><a href="http://manatuscostarica.com/es/area-de-tortuguero">Tortuguero</a></li>
                </ul>
            </div>
            <div class="column2 column">
                <ul>
                    <li><a href="http://manatuscostarica.com/es/paquetes-tarifas">Paquetes & Tarifas</a></li>
                    <li><a href="http://manatuscostarica.com/es/blog">Blog</a></li>
                    <li><a href="http://manatuscostarica.com/es/gallery">Galeria</a></li>
                </ul>
            </div>
            <div class="column3 column">
                <ul>
                    <li><a href="http://manatuscostarica.com/es/virtual-tours/jardines">Tours Virtuales</a></li>
                    <li><a href="http://manatuscostarica.com/es/qué-hacer">¿Qué hacer?</a></li>
                </ul>
            </div>
            <div class="column4 column">
                <p>
                    Manatus Hotel<br />
                    Tortuguero Costa Rica<br />
                    Tel: <a href="tel:+50627098197">(506) 2709.8197</a></p>
                <div class="redes-footer">
                    <a href="https://www.facebook.com/manatuscostarica?fref=ts">
                        <img src="images/2014/face.png" /></a><a href="#"><a href="https://instagram.com/manatuscostarica/"><img
                            src="images/2014/instagram.png" /></a><a href="https://www.pinterest.com/manatuscr/"><img
                                src="images/2014/pinterest.png" /></a><a href="http://www.tripadvisor.com.mx/Hotel_Review-g309268-d308651-Reviews-Manatus_Hotel-Tortuguero_Province_of_Limon.html"><img
                                    src="images/2014/tripadvisor.png" /></a>
                </div>
            </div>
            <div class="footer-info-box">
                <div class="menu-top">
                    <ul>
                        <li><a href="http://manatuscostarica.com/es/">Inicio</a></li>|<li><a href="http://booking.manatuscostarica.com/reservacion_sp.aspx">
                            Book Now</a></li>|<li><a href="http://manatuscostarica.com/es/gallery">Galeria</a></li>|<li>
                                <a href="http://manatuscostarica.com/es/contacto">Contacto</a></li></ul>
                </div>
                <div class="contact-info">
                    <p>
                        MANATUS HOTEL, TORTUGUERO COSTA RICA | TEL: <a href="tel: +50627098197">(506) 2709.8197</a></p>
                </div>
                <div class="menu-idioma">
                    <ul>
                        <li><a href="reservacion_en.aspx">English</a></li>|<li><a href="reservacion_sp.aspx"
                            class="active">Español</a></li></ul>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">



        $('a.poplight[href^=#]').live("click", function () {

            var popID = $(this).attr('rel'); //Get Popup Name

            var popURL = $(this).attr('href'); //Get Popup href to define size

            $('#amazon-widget').css({

                'display': "none"

            });

            //Pull Query & Variables from href URL

            var query = popURL.split('?');

            var dim = query[1].split('&');

            var popWidth = dim[0].split('=')[1]; //Gets the first query string value



            //Fade in the Popup and add close button

            $('#' + popID).fadeIn().css({ 'width': Number(popWidth) }).prepend('<a href="#" class="close"><img src="images/close_pop.png" class="btn_close" title="" alt="Close" /></a>');



            //Define margin for center alignment (vertical   horizontal) - we add 80px to the height/width to accomodate for the padding  and border width defined in the css

            var popMargTop = ($('#' + popID).height() + 80) / 2;

            var popMargLeft = ($('#' + popID).width() + 80) / 2;



            //Apply Margin to Popup

            $('#' + popID).css({

                'margin-top': -popMargTop,

                'margin-left': -popMargLeft

            });



            //Fade in Background

            $('body').append('<div id="fade"></div>'); //Add the fade layer to bottom of the body tag.

            $('#fade').css({ 'filter': 'alpha(opacity=80)' }).fadeIn(); //Fade in the fade layer - .css({'filter' : 'alpha(opacity=80)'}) is used to fix the IE Bug on fading transparencies 



            return false;

        });



        //Close Popups and Fade Layer

        $('a.close, #fade').live('click', function () { //When clicking on the close or fade layer...

            $('#fade , .popup_block').fadeOut(function () {

                $('#fade, a.close').remove();  //fade them both out
                $('#amazon-widget').css({

                    'display': "block"

                });
            });

            return false;

        });





    </script>
    </form>
    <script type="text/javascript">
        var gaJsHost = (("https:" == document.location.protocol) ? "https://ssl." : "http://www.");
        document.write(unescape("%3Cscript src='" + gaJsHost + "google-analytics.com/ga.js' type='text/javascript'%3E%3C/script%3E"));
    </script>
    <script type="text/javascript">
        try {
            var pageTracker = _gat._getTracker("UA-7635413-1");
            pageTracker._setDomainName("none");
            pageTracker._setAllowLinker(true);
            pageTracker._trackPageview();
        } catch (err) { }</script>
</body>
</html>
