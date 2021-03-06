﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="reservacion_es_paso1.aspx.vb"
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
    <script type="text/javascript" src="Scripts/global.js"></script>
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
    <!-- client validations -->
    <script type="text/javascript">
        function CustomValidatorTerminosCondiciones(sender, args) {
            if (document.getElementById("<%=chkTerminosCondiciones.ClientID%>").checked == true) {
                args.IsValid = true;
            } else {
                args.IsValid = false;
            }
        }
    </script>
    <!-- end client validations -->
    <!-- Estilo para Pop Up de Pago -->
    <link type="text/css" rel="stylesheet" href="styles/style_payment_vpos.css" />
</head>
<body class="reservation-form es">
    <form id="form2" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div id="loader" class="loader" runat="server">
                <%--<asp:Image ID="Image1" runat="server" ImageUrl="~/images/ajax-loader.gif" />
                <asp:Label ID="Label1" runat="server" Text="Cargando ..."></asp:Label>--%>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <div id="header">
        <div id="header-inner" class="container-980">
            <div id="logo-box">
                <a href="http://manatuscostarica.com">
                    <img src="images/2014/logo.png" /></a>
            </div>
            <div class="menu-top-box">
                <div class="menu-idioma">
                    <ul>
                        <li><a href="reservacion_en_paso1.aspx">English</a></li>|<li><a href="reservacion_es_paso1.aspx"
                            class="active">Español</a></li></ul>
                </div>
                <div class="menu-top">
                    <ul>
                        <li><a href="http://manatuscostarica.com/es/">Inicio</a></li>|<li><a href="http://booking.manatuscostarica.com/reservacion_es_paso1.aspx">
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
                        <li class="active-trail"><a href="http://booking.manatuscostarica.com/reservacion_es_paso1.aspx">
                            Reservaciones</a></li>
                        <li><a href="http://manatuscostarica.com/es/blog">Blog</a></li></ul>
                    <ul class="mi-menu-responsivo">
                        <li><a href="http://manatuscostarica.com/es/experiencia-manatus">Experiencia Manatus</a>
                        </li>
                        <li><a href="http://manatuscostarica.com/qué-hacer">¿Qué hacer?</a> </li>
                        <li><a href="http://manatuscostarica.com/es/paquetes-tarifas">Paquetes &amp; Tarifas</a>
                        </li>
                        <li><a href="http://manatuscostarica.com/es/area-de-tortuguero">Tortuguero</a></li>
                        <li class="active-trail"><a href="http://booking.manatuscostarica.com/reservacion_es_paso1.aspx">
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
                <asp:UpdatePanel ID="up_step_process" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <div class="step-process-box">
                            <div class="inner">
                                <asp:Panel ID="step_1" runat="server" CssClass="step-1 active step" Visible="true">
                                    <div class="step-title">
                                        <p>
                                            <asp:LinkButton ID="step_1_link" runat="server">Disponibilidad</asp:LinkButton></p>
                                    </div>
                                    <div class="step-point">
                                        <asp:LinkButton ID="step_1_point" runat="server" CssClass="point">1</asp:LinkButton>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="step_2" runat="server" CssClass="step-2 step" Visible="true">
                                    <div class="step-title">
                                        <p>
                                            <asp:LinkButton ID="step_2_link" runat="server">Datos Personales</asp:LinkButton></p>
                                    </div>
                                    <div class="step-point">
                                        <asp:LinkButton ID="step_2_point" runat="server" CssClass="point">2</asp:LinkButton>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="step_3" runat="server" CssClass="step-3 step" Visible="true">
                                    <div class="step-title">
                                        <p>
                                            <asp:LinkButton ID="step_3_link" runat="server">Datos de pago y cancelación</asp:LinkButton></p>
                                    </div>
                                    <div class="step-point">
                                        <asp:LinkButton ID="step_3_point" runat="server" CssClass="point">3</asp:LinkButton>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div id="widgetCalendar" class="hidden">
                <div id='loader-calendar'>
                    <img src='images/ajax-loader.gif' /></div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <asp:LinkButton ID="AplicarSeleccion" runat="server" Text="Aplicar Selección"></asp:LinkButton>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div id="TxtCheckinCheckout-selector">
            </div>
            <asp:UpdatePanel ID="up_paso1" runat="server" UpdateMode="Always">
                <ContentTemplate>
                    <div id="paso1" runat="server" visible="true">
                        <div class="form-title">
                            <h2 class="h2Celeste">
                                Viva la experiencia Manatus</h2>
                            <div class="logo-ssl">
                                <%--<img src="images/verisign.png" />--%></div>
                        </div>
                        <div class="content-box container-2-3">
                            <div id="updtpanel_reservacion">
                                <div>
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
                                                        <asp:UpdatePanel ID="updatePane_fecha" runat="server" UpdateMode="Always">
                                                            <ContentTemplate>
                                                                <div class="fecha-field">
                                                                    <!-- rango de fecha -->
                                                                    <div id="widget">
                                                                        <div id="widgetField">
                                                                            <asp:Label ID="lblIngresoSalida" runat="server" Text="Ingreso y Salida"></asp:Label>
                                                                            <asp:TextBox runat="server" ID="TxtCheckinCheckout" AutoPostBack="true" name="checkin-checkout"
                                                                                value='Seleccionar un rango' ReadOnly="false"></asp:TextBox>
                                                                            <asp:LinkButton ID="book_now_1" CssClass="btn" runat="server">Reservar</asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                    <!-- fin rango de fecha -->
                                                                </div>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                    <hr />
                                                    <div class="contenedor-hab-personas">
                                                        <h3>
                                                            Paquete para sus vacaciones en Costa Rica</h3>
                                                        <hr />
                                                        <asp:UpdatePanel ID="updatePane_habitaciones" runat="server" UpdateMode="Always">
                                                            <ContentTemplate>
                                                                <asp:Panel ID="pnl_resultados" runat="server">
                                                                    <asp:Label ID="costo_title_columna" CssClass="costo_title_columna" runat="server" Text="Costo"  Visible="False"></asp:Label>
                                                                    <asp:GridView ID="gv_ResultadosDisponibles" runat="server" ShowHeader="false" AutoGenerateColumns="false"
                                                                        GridLines="None" Width="246px">
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
                                                                                        <asp:DropDownList ID="ddl_personas" runat="server" CssClass="dropdownsReserva" OnSelectedIndexChanged="ddl_personas_SelectedIndexChanged"
                                                                                            AutoPostBack="true">
                                                                                            <asp:ListItem Text="1 persona" Value="1"></asp:ListItem>
                                                                                            <asp:ListItem Text="2 personas" Value="2"></asp:ListItem>
                                                                                            <asp:ListItem Text="3 personas" Value="3"></asp:ListItem>
                                                                                            <asp:ListItem Text="4 personas" Value="4"></asp:ListItem>
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
                                                    <asp:Label ID="lbl_ResultadoHabitaciones" runat="server" Text="" Visible="false"></asp:Label>
                                                    <div class="desc-paquete">
                                                        <asp:UpdatePanel ID="updatePane_detalleprecio" runat="server" UpdateMode="Always">
                                                            <ContentTemplate>
                                                                <div class="desc-paquete-inner">
                                                                    <div class="box-precio-sin-transporte">
                                                                        <asp:LinkButton ID="btn_reservar1" runat="server" ToolTip="Reservar" ValidationGroup="registrese"
                                                                            ForeColor="#FFFFFF" Visible="false">RESERVAR »</asp:LinkButton>
                                                                        <div class="precio-sin-transporte">
                                                                            <div class="preciovalue precio-sin-transporte-value">
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
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                    <div class="loader-error-wrapper">
                                                        <asp:UpdatePanel ID="updatePane_errores_loader" runat="server" UpdateMode="Always">
                                                            <ContentTemplate>
                                                                <div class="desc-paquete-labels-errores">
                                                                    <asp:Label ID="lbl_erroFechas" runat="server" Text="" ForeColor="red" Visible="false"></asp:Label>
                                                                    <asp:Label ID="lbl_ResultadoReservacion" runat="server" Text="" Visible="false"></asp:Label>
                                                                </div>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                    <hr />
                                                    <div id="traslado-box">
                                                        <asp:UpdatePanel ID="updatePane_transporte" runat="server" UpdateMode="Always">
                                                            <ContentTemplate>
                                                                <h3>
                                                                    ¿Necesita transporte hacia y desde Manatus?</h3>
                                                                <div class="traslado-box-inner">
                                                                    <p>
                                                                        Seleccione las opciones de traslado que requiere y agréguelas a su paquete:</p>
                                                                    <div class="bdbtnlist_transporte_box">
                                                                        <asp:RadioButtonList ID="rdbtnlist_transporte2014" runat="server" AutoPostBack="true">
                                                                            <asp:ListItem Value="1">Traslado a Manatus</asp:ListItem>
                                                                            <asp:ListItem Value="2">Traslado de regreso a San Jose</asp:ListItem>
                                                                            <asp:ListItem Value="3">Transporte Completo</asp:ListItem>
                                                                            <asp:ListItem Value="4" Selected="True">Ninguno</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="rdbtnlist_transporte2014"
                                                                            Display="Dynamic" ErrorMessage="Campo Requerido" ValidationGroup="registrese"></asp:RequiredFieldValidator>

                                                                        <div class="box-precio-transporte">
                                                                            <div class="precio-transporte">
                                                                                <div class="preciovalue precio-transporte-value">
                                                                                    $
                                                                                    <asp:Label ID="lbl_preciotransporte" runat="server" Text="0"></asp:Label>
                                                                                </div>
                                                                            
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <hr />
                                                                    <div class="box-precio-con-transporte">
                                                                        <div class="precio-con-transporte">
                                                                            <div class="precio-con-transporte-value">
                                                                                <span>Costo Total de
                                                                                    <br />
                                                                                    hospedaje y transporte</span>
                                                                                <div class="preciot">
                                                                                    $
                                                                                    <asp:Label ID="lbl_precioConTransporte" runat="server" Text="0"></asp:Label></div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <asp:UpdatePanel ID="book_now_2_wrapper" runat="server" UpdateMode="Always">
                                                                        <ContentTemplate>
                                                                            <div class="content_btn">
                                                                                <asp:LinkButton ID="book_now_2" CssClass="btn" runat="server">Reservar</asp:LinkButton>
                                                                            </div>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
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
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                </div>
                                            </asp:Panel>
                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>
                            <div id="tabs" runat="server">
                                <ul>
                                    <li style="width:45%;"><a href="#descripcion">Descripción del paquete</a></li>
                                    <li style="width:35%;"><a href="#incluye">Lo que incluye</a></li>
                                    <li style="width:20%;"><a href="#tarifas">Tarifas</a></li>
                                </ul>
                                <div id="descripcion" class="tbcontent">
                                    <div class="des_paq_2_1" id="des_paq_2_1" runat="server">
                                        <h4>
                                            Tortuguero en bus y bote:</h4>
                                        <p>
                                            <span class="titulo-dia">Día 1</span><br>
                                            Entre 6:00a.m y 7:00 a.m. estaremos recogiendo a nuestros huéspedes en diferentes
                                            hoteles del Área Metropolitana de San José.<br>
                                            Aproximadamente entre las 7:45 a.m. y 8:30 a.m se hará una parada para desayunar
                                            en el restaurante Nava Café en Guápiles.<br>
                                            A las 11:00 se llega a Caño Blanco donde los espera la lancha que los llevará hacia
                                            el Hotel Manatus.<br>
                                            A las 12:30 p.m. en el Hotel Manatus los estarán esperando con un cóctel de bienvenida,
                                            luego se realiza el check-in de las habitaciones y se les ofrecerá el almuerzo<br>
                                            A las 2:30p.m. se realiza el tour a los canales y posteriormente se realiza el tour
                                            al pueblo. El tour incluido en el paquete se llevan a cabo en la tarde del primer
                                            día.<br>
                                            El horario de la cena es de 6:30 p.m. a 8:30 p.m.</p>
                                        <p>
                                            <span class="titulo-dia">Día 2</span><br>
                                            El horario del desayuno es de 7am a 8:30 am.<br>
                                            Se realiza el check out hasta las 9:15 a.m.<br>
                                            A las 9:30 a.m. sale la lancha para Caño Blanco.<br>
                                            De 12:50 p.m. a 1:15 p.m. tomaremos el almuerzo en el Restaurante Rancho Roberto’s
                                            en Guápiles.<br>
                                            Entre las 3 p.m. y 3:30 p.m. se llega a San José.</p>
                                        <h4>
                                            Tortuguero llegando con Nature Air ó Sansa</h4>
                                        <p>
                                            <span class="titulo-dia">Día 1</span><br>
                                            Entre 6:40 a.m. y 7 a.m. estaremos recogiendo a nuestros huéspedes en la Pista de
                                            Aterrizaje de Tortuguero..<br>
                                            A las 7 a.m. en el Hotel Manatus los estarán esperando con un cóctel de bienvenida,
                                            luego se realiza el check-in de las habitaciones y se les ofrecerá el desayuno<br>
                                            De 8:30a.m se realiza el tour a los Canales de Tortuguero.<br>
                                            El horario del almuerzo es de 12m.d. a 1:30 p.m.<br>
                                            A las 3:00p.m. se realiza el tour al pueblo.<br>
                                            El horario de la cena es de 6:30 p.m. a 8:30 p.m.</p>
                                        <p>
                                            <span class="titulo-dia">Día 2</span><br>
                                            Se hace el check out el día anterior.<br>
                                            Se ofrece un desayuno liviano a las 6 a.m.<br>
                                            Traslado de los huéspedes a la Pista de Aterrizaje de Tortuguero a las 6:30 a.m.</p>
                                    </div>
                                    <div class="des_paq_3_2" id="des_paq_3_2" runat="server">
                                        <h4>
                                            Llegando en Bus y Bote:</h4>
                                        <p>
                                            <span class="titulo-dia">Día 1</span><br>
                                            Entre 6:00a.m y 7:00a.m estaremos recogiendo a nuestros huéspedes en hoteles del
                                            Área Metropolitana.<br>
                                            Aproximadamente entre las 7:45 a.m. y las 8:30 a.m. se hará una parada para desayunar
                                            en el restaurante Nava Café en Guápiles.<br>
                                            A las 11:00 a.m. se llega a Caño Blanco donde una lancha espera a los huéspedes
                                            para llevarlos Hotel Manatus.<br>
                                            A las 12:30 p.m. en el Hotel Manatus los estarán esperando con un cóctel de bienvenida,
                                            luego se realiza el check-in de las habitaciones y se les ofrecerá el almuerzo.<br>
                                            A las 3:00 p.m. se realiza el tour al pueblo<br>
                                            El horario de la cena es de 6:30 p.m. a 8:30 p.m.</p>
                                        <p>
                                            <strong>Nota:</strong><br>
                                            Solo se incluyen un tour en este día.</p>
                                        <h4>
                                            Llegando con Nature Air ó Sansa:</h4>
                                        <p>
                                            <span class="titulo-dia">Día 1</span><br>
                                            Entre 6:40 a.m. y 7 a.m. estaremos recogiendo a nuestros huéspedes en la Pista de
                                            Aterrizaje de Tortuguero<br>
                                            A las 7 AM. en el Hotel Manatus los estarán esperando con un cóctel de bienvenida,
                                            luego se realiza el check-in de las habitaciones y se les ofrecerá el almuerzo.<br>
                                            El horario del almuerzo es de 12m.d. a 1:30 p.m.<br>
                                            A las 3:00 p.m. se realiza el tour al pueblo.<br>
                                            El horario de la cena es de 6:30 p.m. a 8:30 p.m.</p>
                                        <p>
                                            <strong>Nota:</strong><br>
                                            Solo se incluyen un tour en este día.</p>
                                        <p>
                                            <span class="titulo-dia">Día 2</span><br>
                                            El horario del desayuno es de 7 a.m. a 8:30 a.m.<br>
                                            De 8:30a.m a 10:30 a.m. se realiza el tour a uno de los Canales de Tortuguero.<br>
                                            El horario del almuerzo es de 12 a.m. a 1:30 p.m.<br>
                                            De 2:30 p.m. a 4:30p.m. se realiza un tour a otro Canal del Parque Nacional Tortuguero.<br>
                                            El horario de la cena es de 6:30 p.m. a 8:30 p.m.</p>
                                        <p>
                                            <strong>Nota:</strong><br>
                                            Se incluyen dos tour en este día.</p>
                                        <h4>
                                            Saliendo con Bote y Bus:</h4>
                                        <p>
                                            <span class="titulo-dia">Día 3</span><br>
                                            El horario del desayuno es de 7 a.m. a 8:30 a.m.<br>
                                            Se realiza el check out hasta las 9:15 a.m.<br>
                                            A las 9:30 a.m. Sale la lancha para Caño Blanco<br>
                                            De 1 p.m. a 1:30 p.m. se hace una parada para almorzar en el Restaurante Nava Café
                                            en Guápiles.<br>
                                            Entre las 3 p.m. y 3:30 p.m. se llega a San José.</p>
                                        <h4>
                                            Saliendo con Nature Air ó Sansa:</h4>
                                        <p>
                                            <span class="titulo-dia">Día 3</span><br>
                                            Se hace el check out el día anterior.<br>
                                            Se ofrece un desayuno liviano a las 6 a.m.<br>
                                            Traslado de los huéspedes a la Pista de Aterrizaje Tortuguero a las 6:30 a.m.</p>
                                    </div>
                                    <div class="des_paq_custom" id="des_paq_custom" runat="server">
                                        Si usted desea permanecer más tiempo con nosotros, usted puede personalizar su paquete.
                                        Se incluyen todas las comidas y el Tour del Canal del Agua.
                                    </div>
                                </div>
                                <div id="incluye" class="tbcontent">
                                    <div class="inc_paq_2_1" id="inc_paq_2_1" runat="server">
                                        <h4>
                                            2015</h4>
                                        <p>
                                            Los paquetes incluyen:</p>
                                        <ul>
                                            <li>Hospedaje y alimentación, bebidas no incluidas</li>
                                            <li>2 Tours en el paquete 2 días y 1 noche: un tour por el pueblo de Tortuguero y un
                                                tour a los canales del Parque Nacional Tortuguero- entrada no incluida.</li>
                                            <li>3 Tours en el paquete de 3 días y 2 noches: un tour al pueblo de Tortuguero y dos
                                                tours a los canales del Parque Nacional– entrada no incluida.</li>
                                            <li>Incluye todos los impuestos.</li>
                                        </ul>
                                        <p>
                                            No incluye:</p>
                                        <ul>
                                            <li>Traslado terrestre: San Jose - (Pavona / Caño Blanco) - San José</li>
                                            <li>Traslado en Bote: (Pavona / Caño Blanco) - Tortuguero - (Pavona / Cano Blanco)</li>
                                            <li>Desayuno a la ida y almuerzo al regreso.</li>
                                            <li>Los transfer tendrán un costo de $75 por vía por persona. En la vía SJ-Tortuguero
                                                se les brindará desayuno en ruta. </li>
                                            <li>En la vía Tortuguero-SJ se brindará almuerzo en ruta.</li>
                                            <li>Bebidas alcohólicas, gaseosas, y agua embotellada</li>
                                            <li>Entrada al parque nacional USD $15,00 por pax (sujeto a cambios). A partir del 1
                                                de agosto del 2014 el precio será de $15 + impuestos para adultos y $5 + impuestos
                                                para niños.</li>
                                            <li>Entrada al Museo de La Tortuga USD $2.00 por pax (sujeto a cambios)</li>
                                            <li>Tour nocturno de desove de tortugas (Jul-Oct) USD $35 por pax (Sujeto a cambios).
                                                Debe de ser pagado en efectivo en el hotel.</li>
                                        </ul>
                                        <p>
                                            Condiciones generales</p>
                                        <ul>
                                            <li>Precios por persona en US Dólares.</li>
                                            <li>Incluye impuestos alimentación y transporte desde San José</li>
                                            <li>Peso máximo de equipaje 10kg = 25 Lbs por persona.</li>
                                            <li>Check in será a partir de las 12 MD - Check out a las 09 AM.</li>
                                            <li>La capacidad máxima por habitación es de 4 personas.</li>
                                            <li>No se admiten grupos mayores de 6 habitaciones - solo bajo solicitud</li>
                                            <li>No se admiten mascotas de ningún tipo</li>
                                            <li>Nos reservamos el derecho de cargar un suplemento para las cenas o fiestas de Navidad
                                                y de Año Nuevo, que serán de compra no obligatoria.</li>
                                        </ul>
                                        <h4>
                                            2016</h4>
                                        <p>
                                            Los paquetes incluyen:</p>
                                        <ul>
                                            <li>Hospedaje y alimentación, bebidas no incluidas</li>
                                            <li>2 Tours en el paquete 2 días y 1 noche: un tour por el pueblo de Tortuguero y un
                                                tour a los canales del Parque Nacional Tortuguero- entrada no incluida.</li>
                                            <li>3 Tours en el paquete de 3 días y 2 noches: un tour al pueblo de Tortuguero y dos
                                                tours a los canales del Parque Nacional– entrada no incluida.</li>
                                            <li>Incluye todos los impuestos.</li>
                                        </ul>
                                        <p>
                                            No incluye:</p>
                                        <ul>
                                            <li>Traslado terrestre: San Jose - (Pavona / Caño Blanco) - San José</li>
                                            <li>Traslado en Bote: (Pavona / Caño Blanco) - Tortuguero - (Pavona / Cano Blanco)</li>
                                            <li>Desayuno a la ida y almuerzo al regreso.</li>
                                            <li>Los transfer tendrán un costo de $75 por vía por persona. En la vía SJ-Tortuguero
                                                se les brindará desayuno en ruta. </li>
                                            <li>En la vía Tortuguero-SJ se brindará almuerzo en ruta.</li>
                                            <li>Bebidas alcohólicas, gaseosas, y agua embotellada</li>
                                            <li>Entrada al parque nacional USD $15,00 por pax (sujeto a cambios)</li>
                                            <li>Entrada al Museo de La Tortuga USD $2.00 por pax (sujeto a cambios)</li>
                                            <li>Tour nocturno de desove de tortugas (Jul-Oct) USD $35 por pax (Sujeto a cambios).
                                                Debe de ser pagado en efectivo en el hotel.</li>
                                        </ul>
                                        <p>
                                            Condiciones generales</p>
                                        <ul>
                                            <li>Precios por persona en US Dólares.</li>
                                            <li>Incluye impuestos alimentación y transporte desde San José</li>
                                            <li>Peso máximo de equipaje 10kg = 25 Lbs por persona.</li>
                                            <li>Check in será a partir de las 12 MD - Check out a las 09 AM.</li>
                                            <li>La capacidad máxima por habitación es de 4 personas.</li>
                                            <li>No se admiten grupos mayores de 6 habitaciones - solo bajo solicitud</li>
                                            <li>No se admiten mascotas de ningún tipo</li>
                                            <li>Nos reservamos el derecho de cargar un suplemento para las cenas o fiestas de Navidad
                                                y de Año Nuevo, que serán de compra no obligatoria.</li>
                                        </ul>
                                    </div>
                                    <div class="inc_paq_3_2" id="inc_paq_3_2" runat="server">
                                        <h4>
                                            2015</h4>
                                        <p>
                                            Los paquetes incluyen:</p>
                                        <ul>
                                            <li>Hospedaje y alimentación, bebidas no incluidas</li>
                                            <li>2 Tours en el paquete 2 días y 1 noche: un tour por el pueblo de Tortuguero y un
                                                tour a los canales del Parque Nacional Tortuguero- entrada no incluida.</li>
                                            <li>3 Tours en el paquete de 3 días y 2 noches: un tour al pueblo de Tortuguero y dos
                                                tours a los canales del Parque Nacional– entrada no incluida.</li>
                                            <li>Incluye todos los impuestos.</li>
                                        </ul>
                                        <p>
                                            No incluye:</p>
                                        <ul>
                                            <li>Traslado terrestre: San Jose - (Pavona / Caño Blanco) - San José</li>
                                            <li>Traslado en Bote: (Pavona / Caño Blanco) - Tortuguero - (Pavona / Cano Blanco)</li>
                                            <li>Desayuno a la ida y almuerzo al regreso.</li>
                                            <li>Los transfer tendrán un costo de $75 por vía por persona. En la vía SJ-Tortuguero
                                                se les brindará desayuno en ruta. </li>
                                            <li>En la vía Tortuguero-SJ se brindará almuerzo en ruta.</li>
                                            <li>Bebidas alcohólicas, gaseosas, y agua embotellada</li>
                                            <li>Entrada al parque nacional USD $15,00 por pax (sujeto a cambios). A partir del 1
                                                de agosto del 2014 el precio será de $15 + impuestos para adultos y $5 + impuestos
                                                para niños.</li>
                                            <li>Entrada al Museo de La Tortuga USD $2.00 por pax (sujeto a cambios)</li>
                                            <li>Tour nocturno de desove de tortugas (Jul-Oct) USD $35 por pax (Sujeto a cambios).
                                                Debe de ser pagado en efectivo en el hotel.</li>
                                        </ul>
                                        <p>
                                            Condiciones generales</p>
                                        <ul>
                                            <li>Precios por persona en US Dólares.</li>
                                            <li>Incluye impuestos alimentación y transporte desde San José</li>
                                            <li>Peso máximo de equipaje 10kg = 25 Lbs por persona.</li>
                                            <li>Check in será a partir de las 12 MD - Check out a las 09 AM.</li>
                                            <li>La capacidad máxima por habitación es de 4 personas.</li>
                                            <li>No se admiten grupos mayores de 6 habitaciones - solo bajo solicitud</li>
                                            <li>No se admiten mascotas de ningún tipo</li>
                                            <li>Nos reservamos el derecho de cargar un suplemento para las cenas o fiestas de Navidad
                                                y de Año Nuevo, que serán de compra no obligatoria.</li>
                                        </ul>
                                        <h4>
                                            2016</h4>
                                        <p>
                                            Los paquetes incluyen:</p>
                                        <ul>
                                            <li>Hospedaje y alimentación, bebidas no incluidas</li>
                                            <li>2 Tours en el paquete 2 días y 1 noche: un tour por el pueblo de Tortuguero y un
                                                tour a los canales del Parque Nacional Tortuguero- entrada no incluida.</li>
                                            <li>3 Tours en el paquete de 3 días y 2 noches: un tour al pueblo de Tortuguero y dos
                                                tours a los canales del Parque Nacional– entrada no incluida.</li>
                                            <li>Incluye todos los impuestos.</li>
                                        </ul>
                                        <p>
                                            No incluye:</p>
                                        <ul>
                                            <li>Traslado terrestre: San Jose - (Pavona / Caño Blanco) - San José</li>
                                            <li>Traslado en Bote: (Pavona / Caño Blanco) - Tortuguero - (Pavona / Cano Blanco)</li>
                                            <li>Desayuno a la ida y almuerzo al regreso.</li>
                                            <li>Los transfer tendrán un costo de $75 por vía por persona. En la vía SJ-Tortuguero
                                                se les brindará desayuno en ruta. </li>
                                            <li>En la vía Tortuguero-SJ se brindará almuerzo en ruta.</li>
                                            <li>Bebidas alcohólicas, gaseosas, y agua embotellada</li>
                                            <li>Entrada al parque nacional USD $15,00 por pax (sujeto a cambios)</li>
                                            <li>Entrada al Museo de La Tortuga USD $2.00 por pax (sujeto a cambios)</li>
                                            <li>Tour nocturno de desove de tortugas (Jul-Oct) USD $35 por pax (Sujeto a cambios).
                                                Debe de ser pagado en efectivo en el hotel.</li>
                                        </ul>
                                        <p>
                                            Condiciones generales</p>
                                        <ul>
                                            <li>Precios por persona en US Dólares.</li>
                                            <li>Incluye impuestos alimentación y transporte desde San José</li>
                                            <li>Peso máximo de equipaje 10kg = 25 Lbs por persona.</li>
                                            <li>Check in será a partir de las 12 MD - Check out a las 09 AM.</li>
                                            <li>La capacidad máxima por habitación es de 4 personas.</li>
                                            <li>No se admiten grupos mayores de 6 habitaciones - solo bajo solicitud</li>
                                            <li>No se admiten mascotas de ningún tipo</li>
                                            <li>Nos reservamos el derecho de cargar un suplemento para las cenas o fiestas de Navidad
                                                y de Año Nuevo, que serán de compra no obligatoria.</li>
                                        </ul>
                                    </div>
                                    <div class="inc_paq_custom" id="inc_paq_custom" runat="server">
                                        <h4>
                                            2015</h4>
                                        <p>
                                            Los paquetes incluyen:</p>
                                        <ul>
                                            <li>Hospedaje y alimentación, bebidas no incluidas</li>
                                            <li>2 Tours en el paquete 2 días y 1 noche: un tour por el pueblo de Tortuguero y un
                                                tour a los canales del Parque Nacional Tortuguero- entrada no incluida.</li>
                                            <li>3 Tours en el paquete de 3 días y 2 noches: un tour al pueblo de Tortuguero y dos
                                                tours a los canales del Parque Nacional– entrada no incluida.</li>
                                            <li>Incluye todos los impuestos.</li>
                                        </ul>
                                        <p>
                                            No incluye:</p>
                                        <ul>
                                            <li>Traslado terrestre: San Jose - (Pavona / Caño Blanco) - San José</li>
                                            <li>Traslado en Bote: (Pavona / Caño Blanco) - Tortuguero - (Pavona / Cano Blanco)</li>
                                            <li>Desayuno a la ida y almuerzo al regreso.</li>
                                            <li>Los transfer tendrán un costo de $75 por vía por persona. En la vía SJ-Tortuguero
                                                se les brindará desayuno en ruta. </li>
                                            <li>En la vía Tortuguero-SJ se brindará almuerzo en ruta.</li>
                                            <li>Bebidas alcohólicas, gaseosas, y agua embotellada</li>
                                            <li>Entrada al parque nacional USD $15,00 por pax (sujeto a cambios). A partir del 1
                                                de agosto del 2014 el precio será de $15 + impuestos para adultos y $5 + impuestos
                                                para niños.</li>
                                            <li>Entrada al Museo de La Tortuga USD $2.00 por pax (sujeto a cambios)</li>
                                            <li>Tour nocturno de desove de tortugas (Jul-Oct) USD $35 por pax (Sujeto a cambios).
                                                Debe de ser pagado en efectivo en el hotel.</li>
                                        </ul>
                                        <p>
                                            Condiciones generales</p>
                                        <ul>
                                            <li>Precios por persona en US Dólares.</li>
                                            <li>Incluye impuestos alimentación y transporte desde San José</li>
                                            <li>Peso máximo de equipaje 10kg = 25 Lbs por persona.</li>
                                            <li>Check in será a partir de las 12 MD - Check out a las 09 AM.</li>
                                            <li>La capacidad máxima por habitación es de 4 personas.</li>
                                            <li>No se admiten grupos mayores de 6 habitaciones - solo bajo solicitud</li>
                                            <li>No se admiten mascotas de ningún tipo</li>
                                            <li>Nos reservamos el derecho de cargar un suplemento para las cenas o fiestas de Navidad
                                                y de Año Nuevo, que serán de compra no obligatoria.</li>
                                        </ul>
                                        <h4>
                                            2016</h4>
                                        <p>
                                            Los paquetes incluyen:</p>
                                        <ul>
                                            <li>Hospedaje y alimentación, bebidas no incluidas</li>
                                            <li>2 Tours en el paquete 2 días y 1 noche: un tour por el pueblo de Tortuguero y un
                                                tour a los canales del Parque Nacional Tortuguero- entrada no incluida.</li>
                                            <li>3 Tours en el paquete de 3 días y 2 noches: un tour al pueblo de Tortuguero y dos
                                                tours a los canales del Parque Nacional– entrada no incluida.</li>
                                            <li>Incluye todos los impuestos.</li>
                                        </ul>
                                        <p>
                                            No incluye:</p>
                                        <ul>
                                            <li>Traslado terrestre: San Jose - (Pavona / Caño Blanco) - San José</li>
                                            <li>Traslado en Bote: (Pavona / Caño Blanco) - Tortuguero - (Pavona / Cano Blanco)</li>
                                            <li>Desayuno a la ida y almuerzo al regreso.</li>
                                            <li>Los transfer tendrán un costo de $75 por vía por persona. En la vía SJ-Tortuguero
                                                se les brindará desayuno en ruta. </li>
                                            <li>En la vía Tortuguero-SJ se brindará almuerzo en ruta.</li>
                                            <li>Bebidas alcohólicas, gaseosas, y agua embotellada</li>
                                            <li>Entrada al parque nacional USD $15,00 por pax (sujeto a cambios)</li>
                                            <li>Entrada al Museo de La Tortuga USD $2.00 por pax (sujeto a cambios)</li>
                                            <li>Tour nocturno de desove de tortugas (Jul-Oct) USD $35 por pax (Sujeto a cambios).
                                                Debe de ser pagado en efectivo en el hotel.</li>
                                        </ul>
                                        <p>
                                            Condiciones generales</p>
                                        <ul>
                                            <li>Precios por persona en US Dólares.</li>
                                            <li>Incluye impuestos alimentación y transporte desde San José</li>
                                            <li>Peso máximo de equipaje 10kg = 25 Lbs por persona.</li>
                                            <li>Check in será a partir de las 12 MD - Check out a las 09 AM.</li>
                                            <li>La capacidad máxima por habitación es de 4 personas.</li>
                                            <li>No se admiten grupos mayores de 6 habitaciones - solo bajo solicitud</li>
                                            <li>No se admiten mascotas de ningún tipo</li>
                                            <li>Nos reservamos el derecho de cargar un suplemento para las cenas o fiestas de Navidad
                                                y de Año Nuevo, que serán de compra no obligatoria.</li>
                                        </ul>
                                    </div>
                                </div>
                                <div id="tarifas" class="tbcontent">
                                    <div class="tar_paq_2_1" id="tar_paq_2_1" runat="server">
                                        <div class="contenedor-rates">
                                            <div class="rates-columna1">
                                                <h4>
                                                    2015</h4>
                                                <p>
                                                    Tarifas por persona, válidas del 01 de Enero hasta el 31 de Diciembre del 2015</p>
                                                <div class="columna-tabla-rates first">
                                                    <table cellspacing="0" cellpadding="0" border="1" class="width325 aligncenter season-a encabezado">
                                                        <tbody>
                                                            <tr>
                                                                <td class="headerTabla2 bordeTable">
                                                                    <span class="titulo-dia">Temporada Alta</span> Ene - Feb - Mar - Abr - Jul - Ago
                                                                    - Dic
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                    <table cellspacing="0" cellpadding="0" border="1" class="width325 aligncenter season-a">
                                                        <tbody>
                                                            <tr class="headerTabla">
                                                                <td>
                                                                    OCUPACIÓN
                                                                </td>
                                                                <td>
                                                                    TARIFA
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    INDIVIDUAL
                                                                </td>
                                                                <td>
                                                                    $ 301
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    DOBLE
                                                                </td>
                                                                <td>
                                                                    $ 270
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    TRIPLE
                                                                </td>
                                                                <td>
                                                                    $ 247
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    CUADRUPLE
                                                                </td>
                                                                <td>
                                                                    $ 225
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    ADULTO (Noche Adicional)
                                                                </td>
                                                                <td>
                                                                    $ 210
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                                <div class="columna-tabla-rates">
                                                    <table cellspacing="0" cellpadding="0" border="1" class="width325 aligncenter season-b encabezado">
                                                        <tbody>
                                                            <tr>
                                                                <td class="headerTabla2 bordeTable">
                                                                    <span class="titulo-dia">Temporada Baja</span> May - Jun - Set - Oct - Nov
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                    <table cellspacing="0" cellpadding="0" border="1" class="width325 aligncenter season-b">
                                                        <tbody>
                                                            <tr class="headerTabla">
                                                                <td>
                                                                    OCUPACIÓN
                                                                </td>
                                                                <td>
                                                                    TARIFA
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    INDIVIDUAL
                                                                </td>
                                                                <td>
                                                                    $ 250
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    DOBLE
                                                                </td>
                                                                <td>
                                                                    $ 220
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    TRIPLE
                                                                </td>
                                                                <td>
                                                                    $ 200
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    CUADRUPLE
                                                                </td>
                                                                <td>
                                                                    $ 190
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    ADULTO (Noche Adicional)
                                                                </td>
                                                                <td>
                                                                    $ 178
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                            <div class="rates-columna2">
                                                <h4>
                                                    2016</h4>
                                                <p>
                                                    Tarifas por persona, válidas del 01 de Enero hasta el 31 de Diciembre del 2016</p>
                                                <div class="columna-tabla-rates first">
                                                    <table cellspacing="0" cellpadding="0" border="1" class="width325 aligncenter season-a encabezado">
                                                        <tbody>
                                                            <tr>
                                                                <td class="headerTabla2 bordeTable">
                                                                    <span class="titulo-dia">Temporada Alta</span> Ene - Feb - Mar - Abr - Jul - Ago
                                                                    - Dic
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                    <table cellspacing="0" cellpadding="0" border="1" class="width325 aligncenter season-a">
                                                        <tbody>
                                                            <tr class="headerTabla">
                                                                <td>
                                                                    OCUPACIÓN
                                                                </td>
                                                                <td>
                                                                    TARIFA
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    INDIVIDUAL
                                                                </td>
                                                                <td>
                                                                    $ 317
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    DOBLE
                                                                </td>
                                                                <td>
                                                                    $ 283
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    TRIPLE
                                                                </td>
                                                                <td>
                                                                    $ 260
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    CUADRUPLE
                                                                </td>
                                                                <td>
                                                                    $236
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    ADULTO (Noche Adicional)
                                                                </td>
                                                                <td>
                                                                    $ 220
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                                <div class="columna-tabla-rates">
                                                    <table cellspacing="0" cellpadding="0" border="1" class="width325 aligncenter season-b encabezado">
                                                        <tbody>
                                                            <tr>
                                                                <td class="headerTabla2 bordeTable">
                                                                    <span class="titulo-dia">Temparada Baja</span> May - Jun - Set - Oct - Nov
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                    <table cellspacing="0" cellpadding="0" border="1" class="width325 aligncenter season-b">
                                                        <tbody>
                                                            <tr class="headerTabla">
                                                                <td>
                                                                    OCUPACIÓN
                                                                </td>
                                                                <td>
                                                                    TARIFA
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    INDIVIDUAL
                                                                </td>
                                                                <td>
                                                                    $ 262
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    DOBLE
                                                                </td>
                                                                <td>
                                                                    $ 231
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    TRIPLE
                                                                </td>
                                                                <td>
                                                                    $ 210
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    CUADRUPLE
                                                                </td>
                                                                <td>
                                                                    $ 199
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    ADULTO (Noche Adicional)
                                                                </td>
                                                                <td>
                                                                    $ 185
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="tar_paq_3_2" id="tar_paq_3_2" runat="server">
                                        <div class="contenedor-rates">
                                            <div class="rates-columna1">
                                                <h4>
                                                    2015</h4>
                                                <p>
                                                    Tarifas por persona, válidas del 01 de Enero hasta el 31 de Diciembre del 2015</p>
                                                <div class="columna-tabla-rates first">
                                                    <table cellspacing="0" cellpadding="0" border="1" class="width325 aligncenter season-a encabezado">
                                                        <tbody>
                                                            <tr>
                                                                <td class="headerTabla2 bordeTable">
                                                                    <span class="titulo-dia">Temporada Alta</span> Ene - Feb - Mar - Abr - Jul - Ago
                                                                    - Dic
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                    <table cellspacing="0" cellpadding="0" border="1" class="width325 aligncenter season-a">
                                                        <tbody>
                                                            <tr class="headerTabla">
                                                                <td>
                                                                    OCUPACIÓN
                                                                </td>
                                                                <td>
                                                                    TARIFA
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    INDIVIDUAL
                                                                </td>
                                                                <td>
                                                                    $ 428
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    DOBLE
                                                                </td>
                                                                <td>
                                                                    $ 383
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    TRIPLE
                                                                </td>
                                                                <td>
                                                                    $ 346
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    CUADRUPLE
                                                                </td>
                                                                <td>
                                                                    $ 322
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    ADULTO (Noche Adicional)
                                                                </td>
                                                                <td>
                                                                    $ 210
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                                <div class="columna-tabla-rates">
                                                    <table cellspacing="0" cellpadding="0" border="1" class="width325 aligncenter season-b encabezado">
                                                        <tbody>
                                                            <tr>
                                                                <td class="headerTabla2 bordeTable">
                                                                    <span class="titulo-dia">Temporada Baja</span> May - Jun - Set - Oct - Nov
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                    <table cellspacing="0" cellpadding="0" border="1" class="width325 aligncenter season-b">
                                                        <tbody>
                                                            <tr class="headerTabla">
                                                                <td>
                                                                    OCUPACIÓN
                                                                </td>
                                                                <td>
                                                                    TARIFA
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    INDIVIDUAL
                                                                </td>
                                                                <td>
                                                                    $ 354
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    DOBLE
                                                                </td>
                                                                <td>
                                                                    $ 310
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    TRIPLE
                                                                </td>
                                                                <td>
                                                                    $ 284
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    CUADRUPLE
                                                                </td>
                                                                <td>
                                                                    $ 262
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    ADULTO (Noche Adicional)
                                                                </td>
                                                                <td>
                                                                    $ 178
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                            <div class="rates-columna2">
                                                <h4>
                                                    2016</h4>
                                                <p>
                                                    Tarifas por persona, válidas del 01 de Enerohasta el 31 de Diciembre del 2016</p>
                                                <div class="columna-tabla-rates first">
                                                    <table cellspacing="0" cellpadding="0" border="1" class="width325 aligncenter season-a encabezado">
                                                        <tbody>
                                                            <tr>
                                                                <td class="headerTabla2 bordeTable">
                                                                    <span class="titulo-dia">Temporada Alta</span> Ene - Feb - Mar - Abr - Jul - Ago
                                                                    - Dic
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                    <table cellspacing="0" cellpadding="0" border="1" class="width325 aligncenter season-a">
                                                        <tbody>
                                                            <tr class="headerTabla">
                                                                <td>
                                                                    OCUPACIÓN
                                                                </td>
                                                                <td>
                                                                    TARIFA
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    INDIVIDUAL
                                                                </td>
                                                                <td>
                                                                    $ 450
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    DOBLE
                                                                </td>
                                                                <td>
                                                                    $ 402
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    TRIPLE
                                                                </td>
                                                                <td>
                                                                    $ 363
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    CUADRUPLE
                                                                </td>
                                                                <td>
                                                                    $ 338
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    ADULTO (Noche Adicional)
                                                                </td>
                                                                <td>
                                                                    $ 220
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                                <div class="columna-tabla-rates">
                                                    <table cellspacing="0" cellpadding="0" border="1" class="width325 aligncenter season-b encabezado">
                                                        <tbody>
                                                            <tr>
                                                                <td class="headerTabla2 bordeTable">
                                                                    <span class="titulo-dia">Temparada Baja</span> May - Jun - Set - Oct - Nov
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                    <table cellspacing="0" cellpadding="0" border="1" class="width325 aligncenter season-b">
                                                        <tbody>
                                                            <tr class="headerTabla">
                                                                <td>
                                                                    OCUPACIÓN
                                                                </td>
                                                                <td>
                                                                    TARIFA
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    INDIVIDUAL
                                                                </td>
                                                                <td>
                                                                    $ 371
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    DOBLE
                                                                </td>
                                                                <td>
                                                                    $ 325
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    TRIPLE
                                                                </td>
                                                                <td>
                                                                    $ 298
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    CUADRUPLE
                                                                </td>
                                                                <td>
                                                                    $ 275
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    ADULTO (Noche Adicional)
                                                                </td>
                                                                <td>
                                                                    $ 185
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="tar_paq_custom" id="tar_paq_custom" runat="server">
                                        Reserve ahora su paquete.
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="content-box container-1-3 sidebar">
                            <div class="paddingBottomp10">
                                <h3 class="h3Sidebar" id="tbar">
                                    <strong>Otros paquetes</strong> que podrían interesarle</h3>
                                <div class="sidebar-block-content" id="ct_tbar">
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
                                                    <a href="http://manatuscostarica.com/es/contacto" class="boton-form-rojo">Formulario</a>
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
                                                    <a href="mailto:info@manatuscostarica.com">info@manatuscostarica.com</a>
                                                </p>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <%--</asp:Panel>--%>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="up_paso2" runat="server" UpdateMode="Always">
                <ContentTemplate>
                    <div id="paso2" runat="server" visible="false">
                        <div class="form-title">
                            <h2 class="h2Celeste">
                                Favor completar su información<br />
                                Personal</h2>
                            <div class="logo-ssl">
                                <%--<img src="images/verisign.png" />--%></div>
                        </div>
                        <div class="content-box container-2-3">
                            <!-- register form -->
                            <asp:UpdatePanel ID="updtpanel_form" runat="server">
                                <ContentTemplate>
                                    <div>
                                        <div class="fondoGrisReserva">
                                            <div class="paddingReserva">
                                                <asp:Panel ID="panel_2" runat="server" Visible="true">
                                                    <asp:Panel ID="pnl_contenido_form" runat="server" Visible="true">
                                                        <asp:Label ID="lbl_error_registro" runat="server" Text="" Visible="false"></asp:Label>
                                                        <div class="wrapper-field">
                                                            <asp:Label ID="lbl_nombre" runat="server" Text="Nombre completo" CssClass="span-field"></asp:Label>
                                                            <asp:TextBox ID="txt_nombre" runat="server" CssClass="textBoxNuevo"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfv_nombre" runat="server" ControlToValidate="txt_nombre"
                                                                Display="Dynamic" ErrorMessage="Campo requerido" ValidationGroup="registrese"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="wrapper-field">
                                                            <asp:Label ID="lbl_dir" runat="server" Text="Dirección" CssClass="span-field"></asp:Label>
                                                            <asp:TextBox ID="txt_direccion" runat="server" CssClass="textBoxNuevo"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfv_direccion" runat="server" ControlToValidate="txt_direccion"
                                                                Display="Dynamic" ErrorMessage="Campo requerido" ValidationGroup="registrese"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="box-ubicacion wrapper-field">
                                                            <asp:Label ID="lbl_ubicacion" runat="server" Text="País" CssClass="span-field"></asp:Label>
                                                            <asp:DropDownList ID="ddl_ubicacion" runat="server" CssClass="dropdownsReserva">
                                                            </asp:DropDownList>
                                                        </div>
                                                        <!--<div>
                                                <asp:Label ID="lbl_codigo" runat="server" Text="Código Postal:"></asp:Label>
                                                <asp:TextBox ID="txt_codPostal" runat="server" CssClass="textBoxNuevo"></asp:TextBox>
                                            </div>-->
                                                        <div class="cod-telefono">
                                                            <div class="wrapper-field">
                                                                <asp:Label ID="lbl_codigoA" runat="server" Text="Código de Area" CssClass="span-field"></asp:Label>
                                                                <asp:TextBox ID="txt_codArea" runat="server" CssClass="textBoxNuevo"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfv_codigoA" runat="server" ControlToValidate="txt_codArea"
                                                                    Display="Dynamic" ErrorMessage="Campo requerido" ValidationGroup="registrese"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="wrapper-field">
                                                                <asp:Label ID="lbl_telefono" runat="server" Text="Teléfono" CssClass="span-field"></asp:Label>
                                                                <asp:TextBox ID="txt_tel" runat="server" CssClass="textBoxNuevo"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfv_Tel" runat="server" ControlToValidate="txt_tel"
                                                                    Display="Dynamic" ErrorMessage="Campo requerido" ValidationGroup="registrese"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                        <div class="wrapper-field">
                                                            <asp:Label ID="lbl_email" runat="server" Text="Email" CssClass="span-field"></asp:Label>
                                                            <asp:TextBox ID="txt_email" runat="server" CssClass="textBoxNuevo"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfv_email" runat="server" ControlToValidate="txt_email"
                                                                Display="Dynamic" ErrorMessage="Campo Requerido" ValidationGroup="registrese"></asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="rev_email" runat="server" ControlToValidate="txt_email"
                                                                Display="Dynamic" ErrorMessage="Formato incorrecto" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                                ValidationGroup="registrese"></asp:RegularExpressionValidator>
                                                        </div>
                                                        <div class="wrapper-field">
                                                            <asp:Label ID="lbl_mensajeAdicional" runat="server" Text="Mensaje Adicional" CssClass="span-field"></asp:Label>
                                                            <asp:TextBox ID="txt_observaciones" runat="server" CssClass="textMensajeAdicional"
                                                                Height="70px" Rows="5" TextMode="MultiLine"></asp:TextBox>
                                                        </div>
                                                        <div id="terminos-condiciones">
                                                            <asp:HyperLink NavigateUrl="javascript:void(0)" runat="server" Text="Ver Términos y Condiciones"
                                                                ID="linkTerminosCondiciones" />
                                                            <asp:HyperLink NavigateUrl="#?w=620" runat="server" Text="Leer términos y condiciones"
                                                                ID="linkTerminosCondiciones2" rel="popup_code2" CssClass="poplight" />
                                                        </div>
                                                        <div class="wrapper-field">
                                                            <asp:CheckBox Text="He leído y acepto los Términos y Condiciones" runat="server"
                                                                ID="chkTerminosCondiciones" />
                                                            <asp:CustomValidator ID="val_terminosCondiciones" runat="server" ErrorMessage="Favor aceptar los términos y condiciones"
                                                                ClientValidationFunction="CustomValidatorTerminosCondiciones" ValidationGroup="registrese"></asp:CustomValidator>
                                                            <%--<asp:RequiredFieldValidator ID="rev_terminosCondiciones" runat="server" ControlToValidate="chkTerminosCondiciones" Display="Dynamic" ErrorMessage="Campo requerido" ValidationGroup="registrese"></asp:RequiredFieldValidator>--%>
                                                        </div>
                                                        <!-- mensajes -->
                                                        <asp:Label ID="Label3" runat="server" Text=""></asp:Label>
                                                    </asp:Panel>
                                                </asp:Panel>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <asp:UpdatePanel ID="up_button_submit" runat="server">
                                <ContentTemplate>
                                    <div class="alignLeft btn_toStep3">
                                        <asp:LinkButton ID="btn_aPaso3" runat="server" ToolTip="Reservar" ValidationGroup="registrese"
                                            ForeColor="#FFFFFF" Visible="true">ENVIAR INFORMACIÓN Y SEGUIR AL PASO 3: DATOS DE PAGO</asp:LinkButton>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <!-- end register form -->
                        </div>
                        <div class="content-box container-1-3 sidebar">
                            <!-- sidebar with the information previous step -->
                            <div id="su-reservacion">
                                <h2 id="titulo-reservacion">
                                    Su reservación</h2>
                                <div class="detalle">
                                    <div class="wrapper-field ingreso-salida">
                                        <div class="key">
                                            <asp:Label ID="KeyLblIngresoSalida" CssClass="span-field" Text="Ingreso y Salida"
                                                runat="server"></asp:Label>
                                        </div>
                                        <div class="value">
                                            <asp:Label ID="ValueLblIngresoSalida" CssClass="span-field" Text="" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="wrapper-field servicio">
                                        <div class="key">
                                            <asp:Label ID="KeyLblServicio" CssClass="span-field" Text="Servicio" runat="server"></asp:Label>
                                        </div>
                                        <div class="value">
                                            <asp:Label ID="ValueLblServicio" CssClass="span-field" Text="" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="wrapper-field personas">
                                        <div class="key">
                                            <asp:Label ID="KeyLblPersonas" CssClass="span-field" Text="Personas" runat="server"></asp:Label>
                                        </div>
                                        <div class="value">
                                            <asp:Label ID="ValueLblPersonas" CssClass="span-field" Text="" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="wrapper-field habitaciones">
                                        <div class="key">
                                            <asp:Label ID="KeyLblHabitaciones" CssClass="span-field" Text="Habitaciones" runat="server"></asp:Label>
                                        </div>
                                        <div class="value">
                                            <asp:Label ID="ValueLblHabitaciones" CssClass="span-field" Text="" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="wrapper-field costo-sin-transporte">
                                        <div class="key">
                                            <asp:Label ID="KeyLblCostoSinTransporte" CssClass="span-field" Text="Costo estadia"
                                                runat="server"></asp:Label>
                                        </div>
                                        <div class="value">
                                            <asp:Label ID="ValueLblCostoSinTransporte" CssClass="span-field" Text="" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="wrapper-field costo-adicional">
                                        <div class="key">
                                            <asp:Label ID="KeyLblCostoAdicional" CssClass="span-field" Text="Costo noches adicionales"
                                                runat="server"></asp:Label>
                                        </div>
                                        <div class="value">
                                            <asp:Label ID="ValueLblCostoAdicional" CssClass="span-field" Text="" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="wrapper-field total">
                                        <div class="key">
                                            <asp:Label ID="KeyLblCostoTotal" CssClass="span-field" Text="Total" runat="server"></asp:Label>
                                        </div>
                                        <div class="value">
                                            <asp:Label ID="ValueLblCostoTotal" CssClass="span-field" Text="" runat="server"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <asp:LinkButton ID="LinkEditarInformacion" runat="server">< Editar información</asp:LinkButton>
                            </div>
                            <!-- end sidebar with the information previous step -->
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
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
                    <li class="active-trail"><a href="http://booking.manatuscostarica.com/reservacion_es_paso1.aspx">
                        Reservaciones</a></li>
                    <li><a href="http://manatuscostarica.com/blog">Blog</a></li>
                </ul>
            </div>
            <div class="column1 column">
                <ul>
                    <li><a href="http://booking.manatuscostarica.com/reservacion_es_paso1.aspx" class="active-trail">
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
                        <li><a href="http://manatuscostarica.com/es/">Inicio</a></li>|<li><a href="http://booking.manatuscostarica.com/reservacion_es_paso1.aspx">
                            Book Now</a></li>|<li><a href="http://manatuscostarica.com/es/gallery">Galeria</a></li>|<li>
                                <a href="http://manatuscostarica.com/es/contacto">Contacto</a></li></ul>
                </div>
                <div class="contact-info">
                    <p>
                        MANATUS HOTEL, TORTUGUERO COSTA RICA | TEL: <a href="tel: +50627098197">(506) 2709.8197</a></p>
                </div>
                <div class="menu-idioma">
                    <ul>
                        <li><a href="reservacion_en_paso1.aspx">English</a></li>|<li><a href="reservacion_es_paso1.aspx"
                            class="active">Español</a></li></ul>
                </div>
            </div>
        </div>
    </div>
    <div id="overlayvpos" class="overlayvpos">
    </div>
    <div id="imgloadvpos" class="imgloadvpos">
        <img alt="Cargando VPOS" src="images/vpos/loading.gif" class="imgloadingvpos" /></div>
    <div id="modalvpos" class="modalvpos">
        <iframe name="iframevpos" class="iframevpos" frameborder="0"></iframe>
    </div>
    <asp:UpdatePanel ID="up_pago" runat="server">
        <ContentTemplate>
            <div class="lbl_pago">
                <asp:Label ID="ltr_values_pago" runat="server" Text=""></asp:Label>
            </div>
            <script type="text/javascript">

                function enviarvpos() {


                    var inputsPago = document.getElementById("ltr_values_pago");

                    var myform = document.createElement("form");
                    myform.action = "https://vpayment.verifika.com/VPOS/MM/transactionStart20.do";
                    myform.method = "post";
                    myform.name = 'frmSolicitudPago';
                    myform.target = 'iframevpos';
                    myform.appendChild(inputsPago);
                    document.body.appendChild(myform);
                    var divOverlay = document.getElementById('overlayvpos');
                    var divImgLoad = document.getElementById('imgloadvpos');
                    var divModal = document.getElementById('modalvpos');
                    divOverlay.style.visibility = 'visible';
                    divImgLoad.style.visibility = 'visible';
                    divModal.style.visibility = 'visible';
                    document.frmSolicitudPago.submit();
                }

                function ocultarCalendario() {
                    $("#TxtCheckinCheckout-selector").css("visibility", "hidden");
                    $("#widgetCalendar").css("visibility", "hidden");

                }
                function mostrarCalendario() {
                    $("#TxtCheckinCheckout-selector").css("visibility", "visible");
                    $("#widgetCalendar").css("visibility", "visible");
                }


            </script>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">


        $('#tbar').live("click", function () {
            if ($(this).hasClass('activ')) {
                $(this).removeClass('activ').parent().removeClass('activ');
            } else {
                $(this).addClass('activ').parent().addClass('activ');
            }
            $('#ct_tbar').toggle("slow");

        });



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
    <script src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>
    <script>
        $(function () {
            $("#tabs").tabs();
        });
        function pageLoad() {
            $(function () {
                $("#tabs").tabs();
            });

        }
        
    </script>
    <!--terminos y condiciones-->
    <div id="popup_code2" class="popup_block">
        <a class="close" href="#">
            <img alt="Close" title="" class="btn_close" src="images/close_pop.png"></a>
        <br />
        <asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical" Height="400px" Visible="true">
            <div class="textoTerminos">
                <center>
                    <h2 class="principal">
                        T&eacute;rminos y Condiciones</h2>
                    <%--  <span class="terminosNuevo"><a href="#terminos2014">(ver T&eacute;rminos y Condiciones
                                                                2014)</a></span>--%>
                </center>
                <%-- <h2>
                                                            El paquete incluye</h2>
                                                        <ul>
                                                            <li>Traslado terrestre: San Jos&eacute; - (Pavona/ Caño Blanco) - San Jos&eacute;</li>
                                                            <li>Traslado en Bote: (Pavona/ Caño Blanco) - Tortuguero - (Pavona/ Caño Blanco)</li>
                                                            <li>Hospedaje, alimentaci&oacute;n y excursiones con gu&iacute;a: recorrido por el pueblo
                                                                y canales</li>
                                                            <li>El paquete de m&aacute;s de 1 noche incluye la visita al parque - entrada no incluida
                                                            </li>
                <li>Desayuno a la ida y almuerzo al regreso </li>
                <li>Incluye todos los impuestos (NOTA: No incluye el impuesto ICT porque se elimin&oacute;)</li>
                </ul>--%>
                <h2>
                    No incluye</h2>
                <ul>
                    <li>Traslado terrestre: San José - (Pavona/Caño Blanco) – San José</li>
                    <li>Traslado en Bote: (Pavona/Caño Blanco) – Tortuguero - (Pavona/Cano Blanco)</li>
                    <li>Desayuno a la ida y almuerzo al regreso
                        <ul>
                            <li>Los transfer tienen un costo de $75por vía por persona.</li>
                            <li>En la vía SJ-Tortuguero se les brindará desayuno en ruta.</li>
                            <li>En la víaTortuguero-SJ se brindará almuerzo en ruta.</li>
                        </ul>
                    </li>
                    <li>Bebidas alcohólicas, gaseosas, y agua embotellada</li>
                    <li>Entrada al parque nacional USD$15,00 por pax (sujeto a cambios)</li>
                    <li>Entrada al Museo de La Tortuga USD $2.00 por pax (sujeto a cambios)</li>
                    <li>Tour nocturno de desove de tortugas (Jul-Oct) USD $35 por pax (Sujeto A cambios).
                        Debe de ser pagado en efectivo en el hotel.</li>
                </ul>
                <h2>
                    Condiciones generales</h2>
                <ul>
                    <li>Precios por persona en US D&oacute;lares.</li>
                    <li>Incluye todos los impuestos.</li>
                    <li>Peso m&aacute;ximo de equipaje 10kg = 25 Lbs por persona.</li>
                    <li>Check in ser&aacute; a partir de las 12 MD - Check out a las 09 AM.</li>
                    <li>La capacidad m&aacute;xima por habitaci&oacute;n es de 4 personas. </li>
                    <li>No se admiten grupos mayores de 6 habitaciones - solo bajo solicitud </li>
                    <li>No se admiten mascotas de ning&uacute;n tipo </li>
                    <li>Nos reservamos el derecho de cargar un suplemento para las cenas o fiestas de Navidad
                        y de Año Nuevo, que ser&aacute;n de compra no obligatoria.</li>
                </ul>
                <h2>
                    Pol&iacute;tica de los niños</h2>
                <ul>
                    <li>Dadas las características del hotel, el ambiente de privacidad y tranquilidad que
                        se desea proporcionar a nuestros huéspedes, <strong>se ha tomado la decisión de permitir
                            únicamente a niños mayores de 3años</strong>. Estos a su vez se contabilizarán
                        como adultos para asignar las tarifas.</li>
                </ul>
                <h2>
                    Pol&iacute;tica de gu&iacute;as</h2>
                <ul>
                    <li>Los guías en grupos mayores de 6 habitaciones se recibirán CPL en habitación y alimentación
                        para guías.</li>
                    <li>Los guías en grupos de 4 a 5 habitaciones se recibirán cancelando un 25% de la tarifa
                        RACK SGL del paquete en habitación para guías.</li>
                    <li>Los guías para grupos de 1 a 3 habitaciones se recibirán cancelando un 50% de la
                        tarifa RACK SGL del paquete en habitación para guías.</li>
                    <li>Guías en habitación regular del hotel pagarán tarifa regular neta de la agencia.</li>
                </ul>
                <h2>
                    Pol&iacute;ticas de pago para FIT'S y para grupos</h2>
                <p>
                    Debido al tamaño de nuestro hotel y a su ubicación remota, el prepago de sus reservaciones
                    es necesario. La reservación de los servicios se mantendrá durante 21 días posteriores
                    a recibir la solicitud escrita o por correo electrónico. Se requiere un depósito
                    del 25%, a ser efectuado durante este período de 21 días o la reservación será cancelada.
                    Las reservaciones son confirmadas una vez que el 25% de la cuenta total sea recibido
                    por nuestras oficinas, ya sea por cheque o mediante depósito en cuenta bancaria.
                    El balance de pago deberá recibirse a más tardar 30 días antes de la llegada de
                    los huéspedes al hotel, en el caso de FIT’s y 60 días antes del ingreso de los huéspedes
                    en el caso de GRUPOS menores a 6 habitaciones.
                    <br />
                    Las reservaciones realizadas dentro de los 30 días antes (fit’s) ó 60 días antes
                    (grupos menos de 6 habitaciones) del ingreso al hotel, se dará un plazo de dos días
                    hábiles para confirmar / pagar ó cancelar sin recargos.
                </p>
                <h2>
                    Cuenta Corriente para Pagos</h2>
                <p>
                    <strong>Se deben de realizar:</strong><br />
                    A nombre de: La Casa del Manatí S.A.<br />
                    Cedula Jurídica #: 3-101-130226<br />
                    <br />
                    <strong>A las siguientes cuentas corrientes en dólares:</strong>
                </p>
                <ul>
                    <li>Banco de SanJosé (BAC) <strong># 905887964 / CC 10200009058879648</strong></li>
                    <li>Banco de CostaRica (BCR) <strong># 001-0252364-7 / CC 15201001025236479</strong></li>
                </ul>
                <p>
                    Por favor enviar copia del depósito al correo <a href="mailto:info@manatuscostarica.com">
                        info@manatuscostarica.com</a> O al fax 00 (506) 2239-4857.
                </p>
                <h2>
                    Pol&iacute;ticas de cancelaci&oacute;n para grupos
                </h2>
                <p>
                    Para efectos de estas políticas se entenderá como grupo reservaciones de 3 o más
                    habitaciones para las mismas fechas. No se admiten grupos mayores de 6 habitaciones.
                    Cancelaciones de grupos reservados tendrán los siguientes cargos: Los días se cuentan
                    tomando como referencia la fecha del ingreso de los huéspedes.
                </p>
                <ul>
                    <li>Antes de 60 d&iacute;as - sin cargo </li>
                    <li>59 a 30 d&iacute;as el 10% del monto de la reserva</li>
                    <li>29 a 15 d&iacute;as el 25% del monto de la reserva</li>
                    <li>14 a 08 d&iacute;as el 50% del monto de la reserva</li>
                    <li>07 d&iacute;as o antes, 100% del monto de la reserva.</li>
                    <li>NO se har&aacute;n reembolsos por "NO presentaci&oacute;n" (no shows)</li>
                </ul>
                <h2>
                    Pol&iacute;ticas de cancelaci&oacute;n para FIT'S
                </h2>
                <p>
                    Cancelaciones de FIT's o pasajeros individuales reservados tendr&aacute;n los siguientes
                    cargos:<br />
                    Los d&iacute;as se cuentan tomando como referencia la fecha del ingreso de los huspedes.
                </p>
                <ul>
                    <li>Antes de 30 d&iacute;as - sin cargo</li>
                    <li>29 a 15 d&iacute;as el 10% del monto de la reserva</li>
                    <li>14 a 10 d&iacute;as el 25% del monto de la reserva</li>
                    <li>09 a 04 d&iacute;as el 50% del monto de la reserva</li>
                    <li>Menos de 03 d&iacute;as, 100% del monto de la reserva.</li>
                    <li>NO se har&aacute;n reembolsos por "NO presentaci&oacute;n" (no shows)</li>
                </ul>
                <h2>
                    Pol&iacute;ticas de reembolsos por cancelaci&oacute;n de reservaciones:</h2>
                <p>
                    Con lo concerniente a la devolución del dinero por reservaciones pagadas y canceladas,
                    dentro de nuestras políticas de cancelación, el hotel realiza devoluciones de dinero
                    únicamente a las reservaciones realizadas de forma directa y que por distintas razones
                    justificadas el cliente no pueda realizar el viaje.
                    <br />
                    En caso de agencias o entidades que cobran un porcentaje de comisión, el hotel trabaja
                    únicamente bajo el formato de notas de crédito consumibles.
                    <br />
                    El departamento de reservaciones y contabilidad envía una nota de crédito por medio
                    del correo electrónico o fax, en el cuál indicará el monto para aplicar a favor
                    de la agencia para hacerlo efectivo dentro de un período de un año.
                    <br />
                    <br />
                </p>
                <h2>
                    T&eacute;rminos de reembolso y pago en caso de cancelaci&oacute;n de reserva por
                    parte del hotel:</h2>
                <p>
                    En caso de emergencias naturales, huelgas, derrumbes o situaciones en las cuales
                    el hotel se vea imposibilitado de ofrecer el servicio tanto de transporte como alojamiento.
                    EL HOTEL, se reserva el derecho de rembolsar el 70% del total de la reservación,
                    el restante 30% de la misma se destinarán para cubrir los gastos de operación. Las
                    devoluciones a las agencias se realizarán mediante notas de crédito por el monto
                    correspondiente al 70% neto de la reservación, para el caso de reservaciones directas
                    el hotel cubrirá el 70% de la reservación en efectivo en un plazo de24horas.
                </p>
                <p>
                    Si fuese por causas atribuibles al HOTEL, este asumirá los gastos de hospedaje,
                    alimentación, transporte y derivados, (DURANTE UN PERÍODO DE TIEMPO EQUIVALENTE
                    A LA ESTADÍA DENTRO DEL HOTEL.) hasta poder suministrar el servicio reservado.
                </p>
                <br />
                <%-- <center>
                                                            <a name="terminos2014"></a>
                                                            <h2 class="principal">
                                                                T&eacute;rminos y Condiciones 2014</h2>
                                                        </center>
                                                        <h2>
                                                            El paquete incluye</h2>
                                                        <ul>
                                                            <li>Hospedaje y alimentaci&oacute;n, bebidas no incluidas</li>
                                                            <li>2 Tours en el paquete 2 d&iacute;as y 1 noche: un tour por el pueblo de Tortuguero
                                                                y un tour a los canales del Parque Nacional Tortuguero- entrada no incluida.</li>
                                                            <li>3 Tours en el paquete de 3 d&iacute;as y 2 noches: un tour al pueblo de Tortuguero
                                                                y dos tours a los canales del Parque Nacional– entrada no incluida.</li>
                                                                                                  
                                                            <li>Incluye todos los impuestos.</li>
                                                        </ul>
                                                        <h2>
                                                            No incluye</h2>
                                                        <ul>
                                                            <li>Traslado terrestre: San Jose - (Pavona / Ca&ntilde;o Blanco) - San Jos&eacute;</li>
                                                            <li>Traslado en Bote: (Pavona / Ca&ntilde;o Blanco) - Tortuguero - (Pavona / Cano Blanco)
                                                            </li>
                                                            <li>Desayuno a la ida y almuerzo al regreso.</li>
                                                            <li>Los transfer tendrán un costo de $70 por vía. En la vía SJ-Tortuguero se les brindará
                                                                desayuno en ruta. En la vía Tortuguero-SJ se brindará almuerzo en ruta.</li>
                                                            <li>Bebidas alcohólicas, gaseosas, y agua embotellada</li>
                                                            <li>Entrada al parque nacional USD $10,00 por pax (sujeto a cambios)</li>
                                                            <li>Entrada al Museo de La Tortuga USD $1.00 por pax (sujeto a cambios)</li>
                                                            <li>Tour nocturno de desove de tortugas (Jul-Oct) USD $35 por pax (Sujeto a cambios).
                                                                Debe de ser pagado en efectivo en el hotel.</li>
                                                        </ul>
                                                        <h2>
                                                            Condiciones generales</h2>
                                                        <ul>
                                                            <li>Precios por persona en US D&oacute;lares.</li>
                                                            <li>Incluye impuestos alimentaci&oacute;n y transporte desde San Jos&eacute;</li>
                                                            <li>Peso m&aacute;ximo de equipaje 10kg = 25 Lbs por persona.</li>
                                                            <li>Check in ser&aacute; a partir de las 12 MD - Check out a las 09 AM.</li>
                                                            <li>La capacidad m&aacute;xima por habitaci&oacute;n es de 4 personas. </li>
                                                            <li>No se admiten grupos mayores de 6 habitaciones - solo bajo solicitud </li>
                                                            <li>No se admiten mascotas de ning&uacute;n tipo </li>
                                                            <li>Nos reservamos el derecho de cargar un suplemento para las cenas o fiestas de Navidad
                                                                y de Año Nuevo, que ser&aacute;n de compra no obligatoria.</li>
                                                        </ul>
                                                        <h2>
                                                            Pol&iacute;tica de los niños</h2>
                                                        <ul>
                                                            <li>Dadas las caracter&iacute;sticas del hotel, el ambiente de privacidad y tranquilidad
                                                                que se desea proporcionar a nuestros huspedes, se ha tomado la decisi&oacute;n de
                                                                permitir &uacute;nicamente a niños mayores de 6 años. Estos a su vez se contabilizar&aacute;n
                                                                como adultos para asignar las tarifas</li>
                                                        </ul>
                                                        <h2>
                                                            Pol&iacute;tica de gu&iacute;as</h2>
                                                        <ul>
                                                            <li>Los gu&iacute;as en grupos mayores de 6 habitaciones se recibir&aacute;n CPL en
                                                                habitaci&oacute;n y alimentaci&oacute;n para gu&iacute;as.</li>
                                                            <li>Los gu&iacute;as en grupos de 4 a 5 habitaciones se recibir&aacute;n cancelando
                                                                un 25% de la tarifa RACK SGL del paquete en habitaci&oacute;n para gu&iacute;as.</li>
                                                            <li>Los gu&iacute;as para grupos de 1 a 3 habitaciones se recibir&aacute;n cancelando
                                                                un 50% de la tarifa RACK SGL del paquete en habitaci&oacute;n para gu&iacute;as.</li>
                                                            <li>Gu&iacute;as en habitaci&oacute;n regular del hotel pagar&aacute;n tarifa regular
                                                                neta de la agencia. </li>
                                                        </ul>
                                                        <h2>
                                                            Pol&iacute;ticas de pago para FIT'S y para grupos</h2>
                                                        <p>
                                                            Debido a lo pequeño de nuestro Hotel y a su ubicaci&oacute;n remota, el prepago
                                                            de sus reservaciones es necesario. La reservaci&oacute;n de los servicios se mantendr&aacute;
                                                            durante 21 d&iacute;as posteriores a recibir la solicitud escrita o por correo electr&oacute;nico.
                                                            Se requiere un dep&oacute;sito del 25%, a ser efectuado durante este per&iacute;odo
                                                            de 21 d&iacute;as o la reservaci&oacute;n ser&aacute; cancelada. Las reservaciones
                                                            son confirmadas una vez que el 25% de la cuenta total sea recibido por nuestras
                                                            oficinas, v&iacute;a tarjeta de cr&eacute;dito, cheque o mediante dep&oacute;sito
                                                            en cuenta bancaria. El balance de pago deber recibirse a m&aacute;s tardar 30 d&iacute;as
                                                            antes de la llegada de los huspedes al Hotel, en el caso de FIT's y 60 d&iacute;as
                                                            antes del ingreso de los huspedes en el caso de GRUPOS. Las reservaciones realizadas
                                                            dentro de los 30 d&iacute;as antes (fit's) o 60 d&iacute;as antes (grupos) del ingreso
                                                            al hotel, se dar&aacute; un plazo de dos d&iacute;as h&aacute;biles para confirmar/pagar
                                                            o cancelar sin recargos.
                                                        </p>
                                                        <h2>
                                                            Cuenta Corriente para Pagos</h2>
                                                        <p>
                                                            CUENTA corriente en dlares US $ en el Bac de San Jos&eacute; (BAC) # 90 58 87 964
                                                            A nombre de: La Casa del Manat&iacute; S.A.<br />
                                                            Por favor enviar copia del dep&oacute;sito por correo electr&oacute;nico a info@manatuscostarica.com
                                                            o por fax al n&uacute;mero 00 (506) – 2239-4857.
                                                        </p>
                                                        <h2>
                                                            Pol&iacute;ticas de cancelaci&oacute;n para grupos
                                                        </h2>
                                                        <p>
                                                            Para efectos de estas pol&iacute;ticas se entender&aacute; como grupo, reservaciones
                                                            de 3 o m&aacute;s habitaciones para las mismas fechas. No se admiten grupos mayores
                                                            de 6 habitaciones.
                                                            <br />
                                                            <br />
                                                            Cancelaciones de grupos reservados tendr&aacute;n los siguientes cargos: Los d&iacute;as
                                                            se cuentan tomando como referencia la fecha del ingreso de los huspedes.
                                                        </p>
                                                        <ul>
                                                            <li>Antes de 60 d&iacute;as - sin cargo </li>
                                                            <li>59 a 30 d&iacute;as el 10% del monto de la reserva</li>
                                                            <li>29 a 15 d&iacute;as el 25% del monto de la reserva</li>
                                                            <li>14 a 08 d&iacute;as el 50% del monto de la reserva</li>
                                                            <li>07 d&iacute;as o antes, 100% del monto de la reserva.</li>
                                                            <li>NO se har&aacute;n reembolsos por "NO presentaci&oacute;n" (no shows)</li>
                                                        </ul>
                                                        <h2>
                                                            Pol&iacute;ticas de cancelaci&oacute;n para FIT'S
                                                        </h2>
                                                        <p>
                                                            Cancelaciones de FIT's o pasajeros individuales reservados tendr&aacute;n los siguientes
                                                            cargos:<br />
                                                            Los d&iacute;as se cuentan tomando como referencia la fecha del ingreso de los huspedes.
                                                        </p>
                                                        <ul>
                                                            <li>Antes de 30 d&iacute;as - sin cargo</li>
                                                            <li>29 a 15 d&iacute;as el 10% del monto de la reserva</li>
                                                            <li>14 a 10 d&iacute;as el 25% del monto de la reserva</li>
                                                            <li>09 a 04 d&iacute;as el 50% del monto de la reserva</li>
                                                            <li>Menos de 03 d&iacute;as, 100% del monto de la reserva.</li>
                                                            <li>NO se har&aacute;n reembolsos por "NO presentaci&oacute;n" (no shows)</li>
                                                        </ul>
                                                        <h2>
                                                            Pol&iacute;ticas de reenbolsos por cancelaci&oacute;n de reservaciones:</h2>
                                                        <p>
                                                            Con lo concerniente a la devoluci&oacute;n del dinero por reservaciones pagadas
                                                            y canceladas dentro de nuestras pol&iacute;ticas de cancelaci&oacute;n, el hotel
                                                            realiza devoluciones de dinero &uacute;nicamente a las reservaciones realizadas
                                                            de forma directa y que por distintas razones el cliente no pueda realizar el viaje.<br />
                                                            En caso de agencias o entidades que cobran un porcentaje de comisi&oacute;n, el
                                                            hotel trabaja &uacute;nicamente bajo el formato de notas de cr&eacute;dito consumibles
                                                            ya sea por clientes o personal interno de la agencia.
                                                            <br />
                                                            El departamento de reservaciones y contabilidad env&iacute;a una nota de cr&eacute;dito
                                                            por medio del correo electr&oacute;nico o fax, en el c&uacute;al indicar el monto
                                                            para aplicar a favor de la agencia para hacerlo efectivo dentro de un per&iacute;odo
                                                            de un año.
                                                            <br />
                                                            <br />
                                                            T&eacute;rminos de reembolso y pago en caso de cancelaci&oacute;n de reserva por
                                                            parte del hotel:<br />
                                                            En caso de emergencias naturales, huelgas, derrumbes o situaciones en las cuales
                                                            el Hotel se vea imposibilitado de ofrecer el servicio tanto de transporte como alojamiento.
                                                            EL HOTEL, se reserva el derecho de reembolsar el 70% del total de la reservaci&oacute;n,
                                                            el restante 30% de la misma se destinar&aacute;n para cubrir los gastos de operaci&oacute;n.<br />
                                                            Las devoluciones a las agencias se realizar&aacute;n mediante notas de cr&eacute;dito
                                                            por el monto correspondiente al 70% neto de la reservaci&oacute;n, para el caso
                                                            de reservaciones directas el hotel cubrir el 70% de la reservaci&oacute;n en efectivo
                                                            en un plazo de 24 horas. Si fuese por causas atribuibles al HOTEL, esta asumir los
                                                            gastos de hospedaje, alimentaci&oacute;n, transporte y derivados, (DURANTE UN PERODO
                                                            DE TIEMPO EQUIVALENTE A LA ESTAD&Iacute;A DENTRO DEL HOTEL) hasta poder suministrar
                                                            el servicio reservado.
                                                        </p>--%>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnl_terminospaquete" runat="server" ScrollBars="Vertical" Height="400px"
            Visible="false">
            <div class="textoTerminos">
                <center>
                    <h2 class="font22 fontVerde">
                        T&eacute;rminos y condiciones de la oferta</h2>
                    <br />
                </center>
                <ul>
                    <li>Oferta de vuelo gratis, v&aacute;lido por los meses de setiembre, octubre y noviembre.</li>
                    <li>Oferta de tour de anidaci&oacute;n gratis, v&aacute;lido por los meses de setiembre
                        y octubre.</li>
                    <li>Promoción no aplicable con otras ofertas. </li>
                    <li>Vuelo de una sola v&iacute;a, para la ida,(San Jos&eacute;- Tortuguero) a trav&eacute;s
                        de Nature Air.</li>
                    <li>No aplica para el vuelo de regreso (Tortuguero-San Jos&eacute;). </li>
                    <li>Tarifas y promociones de uso independiente.</li>
                    <li>&Uacute;nicamente aplicable a trav&eacute;s de reservas directas, en línea en nuestra
                        p&aacute;gina de internet (www.manatuscostarica.com )</li>
                    <li>No incluye otros aspectos que no estén descritos en el afiche de la promoci&oacute;n.
                    </li>
                    <li>No aplica para reservas telef&oacute;nicas o a trav&eacute;s de agencias.</li>
                </ul>
                <h2>
                    Condiciones de vuelo:</h2>
                <ul>
                    <li>El peso permitido en equipaje para cada persona es de 27 lbs para chequear y 10
                        lbs en equipaje de mano.</li>
                    <li>No se permiten cancelaciones.</li>
                    <li>Los cambios en itinerario deben ser solicitados con 24 horas antes de la salida
                        del vuelo.</li>
                    <li>Penalidad por cambios es de $40 por persona y por vía.</li>
                    <li>Vuelos sujetos a disponibilidad</li>
                </ul>
            </div>
        </asp:Panel>
    </div>
</body>
</html>
