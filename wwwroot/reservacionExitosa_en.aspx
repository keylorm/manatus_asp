<%@ Page Language="VB" AutoEventWireup="false" CodeFile="reservacionExitosa_en.aspx.vb" Inherits="reservacionExitosa_en" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <title>Online hotel reservations, booking a hotel in Tortuguero </title>
    <link rel="shortcut icon" href="images/images2/favicon.ico" />
    <meta content="width=device-width; initial-scale=1.0; maximum-scale=1.0; user-scalable=0;"
        name="viewport">
    <meta name="revisit-after" content="4 days" />
    <meta name="robots" content="INDEX,FOLLOW" />
    <meta name="GOOGLEBOT" content="INDEX, FOLLOW" />
    <meta name="author" content="Poveda" />
    <meta name="keywords" content="On-line hotel reservations, booking, hotel tortuguero" />
    <meta name="description" content="Hotel reservations at Manatus Hotel Tortuguero Costa Rica, on-line reservations. Booking." />
    <!--<link href="styles/styles_new.css" rel="stylesheet" type="text/css" />
    <link href="styles/stylereservations.css" rel="stylesheet" type="text/css" />-->
    <link type="text/css" rel="stylesheet" href="http://fonts.googleapis.com/css?family=Open+Sans:400,300,600,700|Open+Sans+Condensed:300,700|Raleway:400,300,600,700">
    <link href="styles/stylereservations_nuevo.css" rel="stylesheet" type="text/css"  />
    <link href="styles/stylereservations_nuevo_imprimir.css" rel="stylesheet" type="text/css" media="print" />
    <!--[if gte IE 9]><!-->
    <link rel="stylesheet" href="styles/stylereservation_nuevo_responsivo.css" type="text/css" />
    <!--<![endif]-->
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.0/jquery.min.js"></script>
    <script type="text/javascript" src="js/jquery.jcarousel.min.js"></script>
    <script type="text/javascript" src="js/myJS.js"></script>
    <script type="text/javascript" src="http://booking.manatuscostarica.com/js/tinynav.min.js"></script>
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
    <!--[if IE]>
     <link type="text/css" rel="stylesheet" href="styles/stylereservations_nuevo_ie.css" />
     <script src="http://html5shim.googlecode.com/svn/trunk/html5.js"></script>
    <script src="js/myJs_ie.js" type="text/javascript"></script>

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
    <!--[if lte IE 9]>
	
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

    <!-- custom scripts -->
    <script type="text/javascript">
        (function ($) {
            $(document).ready(function () {
                $(".downloadVoucher").on("click", function () {
                    /*call print page function*/
                    printPage();
                });
                function printPage() {
                    /*Remove elements*/
                    /*header*/
                    $(".menu-top-box").remove();
                    $(".menu-principal-box").remove();
                    $("#page-head").remove();
                    $(".above-content").hide(); /*ocultamos el bloque porque necesito clonar el html de los logos de los premos*/
                    $(".below-content").remove();
                    $("#footer").remove();
                    var styles = {
                        "position": "relative",
                        "margin-bottom": "20px",
                        "float": "left",
                        "left": "0px"
                    };
                    var styles2 = {
                        "float": "left",
                        "margin-top": "25px",
                        "margin-left": "50px"
                    };
                    var logos_premios = $(".logos-premios");
                    $(".logos_premios a").css("margin-right", "20px");
                    $(".logos_premios img").css("margin-right", "20px");
                    $(".logos-premios #CDSWIDEXC").css("display", "none");
                    $(logos_premios).css(styles2);
                    $("#header-inner").css("height", "160px");
                    $("#header-inner").append(logos_premios);
                    $("#logo-box").css(styles);
                    javascript: window.print(); void 0;
                    location.reload();
                }
            });
        })(jQuery);
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div id="header">
            <div id="header-inner" class="container-980">
                <div id="logo-box">
                    <a href="http://manatuscostarica.com">
                        <img src="images/2014/logo.png" /></a>
                </div>
				<div class="logos-premios" style="display: none;">
					<a href="http://www.tripadvisor.com.mx/Hotel_Review-g309268-d308651-Reviews-Manatus_Hotel-Tortuguero_Province_of_Limon.html">
						<img src="images/logo-trip-1.jpg" /></a> <a href="http://www.tripadvisor.com.mx/Hotel_Review-g309268-d308651-Reviews-Manatus_Hotel-Tortuguero_Province_of_Limon.html">
							<img src="images/logo-trip-2.jpg" /></a>
					<img src="images/logo-rainforest.png" />
					
				</div>
                <div class="menu-top-box">
                    <div class="menu-idioma">
                        <ul>
                            <li><a href="reservacion_en_paso1.aspx" class="active">English</a></li>|<li><a href="reservacion_es_paso1.aspx">
                                Español</a></li></ul>
                    </div>
                    <div class="menu-top">
                        <ul>
                            <li><a href="http://manatuscostarica.com/">Home</a></li>|<li><a href="http://booking.manatuscostarica.com/reservacion_en_paso1.aspx">
                                Book Now</a></li>|<li><a href="http://manatuscostarica.com/gallery">Gallery</a></li>|<li>
                                    <a href="http://manatuscostarica.com/contact-us">Contact</a></li></ul>
                    </div>
                    <div class="contact-info">
                        <p>
                            MANATUS HOTEL, TORTUGUERO COSTA RICA | RESERVATIONS: <a href="tel: +50622397364">(506)
                                2239.7364</a> | HOTEL: <a href="tel: +50627098197">(506) 2709.8197</a></p>
                    </div>
                </div>
                <div class="menu-principal-box">
                    <div class="menu-principal">
                        <ul class="mi-menu">
                            <li><a href="http://manatuscostarica.com/manatus-experience">Manatus Experience</a>
                                <ul>
                                    <li><a href="http://manatuscostarica.com/rooms">Cozy Rooms</a></li>
                                    <li><a href="http://manatuscostarica.com/gardens-surroundings">Hotel Gardens and Surroundings</a></li>
                                    <li><a href="http://manatuscostarica.com/amenities">Exclusive Amenities</a></li>
                                    <li><a href="http://manatuscostarica.com/restaurant">Caribbean Food Restaurant</a></li>
                                    <li><a href="http://manatuscostarica.com/pool">River Front Pool Area</a></li>
                                </ul>
                            </li>
                            <li><a href="http://manatuscostarica.com/what-to-do">What to do?</a>
                                <ul>
                                    <li><a href="http://manatuscostarica.com/tours-and-activities">Tours and Activities</a></li>
                                    <li><a href="http://manatuscostarica.com/sportsfishing-tour">Sportfishing Tours</a></li>
                                </ul>
                            </li>
                            <li><a href="http://manatuscostarica.com/package-and-rates">Packages &amp; Rates</a>
                                <ul>
                                    <li><a href="http://manatuscostarica.com/honeymoon-packages">Honeymoon Packages</a></li>
                                    <li><a href="http://manatuscostarica.com/regular-packages-rates">Regular Packages</a></li>
                                    <li><a href="http://manatuscostarica.com/special-packages">Seasonal Packages</a></li>
                                </ul>
                            </li>
                            <li><a href="http://manatuscostarica.com/tortuguero-area">Tortuguero</a></li>
                            <li class="active-trail"><a href="http://booking.manatuscostarica.com/reservacion_en_paso1.aspx">
                                Reservations</a></li>
                            <li><a href="http://manatuscostarica.com/blog">Blog</a></li></ul>
                        <ul class="mi-menu-responsivo">
                            <li><a href="http://manatuscostarica.com/manatus-experience">Manatus Experience</a>
                            </li>
                            <li><a href="http://manatuscostarica.com/what-to-do">What to do?</a> </li>
                            <li><a href="http://manatuscostarica.com/package-and-rates">Packages &amp; Rates</a>
                            </li>
                            <li><a href="http://manatuscostarica.com/tortuguero-area">Tortuguero</a></li>
                            <li class="active-trail"><a href="http://booking.manatuscostarica.com/reservacion_en_paso1.aspx">
                                Reservations</a></li>
                            <li><a href="http://manatuscostarica.com/blog">Blog</a></li></ul>
                    </div>
                </div>
                <div id="page-head">
                    <div class="page-title">
                        <h1 title="On-line reservations, booking at Manatus, tortuguero hotel reservations"
                            class="h1ReservaNew">
                            RESERVATION</h1>
                    </div>
                    <div class="breadcrumbs">
                        <a href="http://manatuscostarica.com/" title="P&aacute;gina principal Hotel Manatus Tortuguero Costa Rica">
                            Home</a> / Reservation
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
                    <div class="form-title">
                        <h2 class="h2Celeste">We will be happy to receive you in Hotel Manatus!</h2>
                        <%--<div class="logo-ssl">
                            <img src="images/verisign.png" />
                        </div>--%>
                    </div>
                    <p class="comprobante-reserva">
                        Your reservation has been processed successfully. Very soon you will receive a confirmation in your email.
                        
						<!--<a href="#" class="downloadVoucher"">Download voucher.</a></a>-->
                        <a href='javascript:window.print(); void 0;'>Download voucher.</a> 
                    </p>
                </div>

                <div class="ct_detalle_reserva">
                <div class="content-box container-2-3">
                    <!-- cuadro respuesta de reservacion -->
                    <div class="detalle-reservacion">
                        <div id="su-reservacion">
                            <h2 id="titulo-reservacion">Detail Booking</h2>
                            <div class="detalle">
                                <div class="wrapper-field ingreso-salida">
                                    <div class="key">
                                        <asp:Label ID="KeyLblIngresoSalida" CssClass="span-field" Text="Checkin and Checkout" runat="server"></asp:Label>
                                    </div>
                                    <div class="value">
                                        <asp:Label ID="ValueLblIngresoSalida" CssClass="span-field" Text="" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="wrapper-field servicio">
                                    <div class="key">
                                        <asp:Label ID="KeyLblServicio" CssClass="span-field" Text="Service" runat="server"></asp:Label>
                                    </div>
                                    <div class="value">
                                        <asp:Label ID="ValueLblServicio" CssClass="span-field" Text="" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="wrapper-field personas">
                                    <div class="key">
                                        <asp:Label ID="KeyLblPersonas" CssClass="span-field" Text="People" runat="server"></asp:Label>
                                    </div>
                                    <div class="value">
                                        <asp:Label ID="ValueLblPersonas" CssClass="span-field" Text="" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="wrapper-field habitaciones">
                                    <div class="key">
                                        <asp:Label ID="KeyLblHabitaciones" CssClass="span-field" Text="Rooms" runat="server"></asp:Label>
                                    </div>
                                    <div class="value">
                                        <asp:Label ID="ValueLblHabitaciones" CssClass="span-field" Text="" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="wrapper-field costo-sin-transporte">
                                    <div class="key">
                                        <asp:Label ID="KeyLblCostoSinTransporte" CssClass="span-field" Text="Cost stay" runat="server"></asp:Label>
                                    </div>
                                    <div class="value">
                                        <asp:Label ID="ValueLblCostoSinTransporte" CssClass="span-field" Text="" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <div class="wrapper-field costo-adicional">
                                    <div class="key">
                                        <asp:Label ID="KeyLblCostoAdicional" CssClass="span-field" Text="Additional nights cost" runat="server"></asp:Label>
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
                        </div>
                    </div>
                    <!-- end cuadro respuesta de reservacion -->
                </div>
                <div class="content-box container-1-3 sidebar">
                    <div id="sus-datos">
                        <h2 id="titulo-sus-datos">Your personal information</h2>
                        <div class="detalle">
                            <div class="wrapper-field nombre-completo">
                                <div class="row">
                                    <asp:Label ID="lblNombreCompleto" CssClass="span-field" Text="" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="wrapper-field telefono">
                                <div class="row">
                                    <asp:Label ID="lblTelefono" CssClass="span-field" Text="" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="wrapper-field email">
                                <div class="row">
                                    <asp:Label ID="lblEmail" CssClass="span-field" Text="" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="wrapper-field no-tarjeta">
                                <div class="row">
                                    <asp:Label ID="lblNoTarjeta" CssClass="span-field" Text="" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="wrapper-field fecha-vencimiento">
                                <div class="row">
                                    <asp:Label ID="lblFechaVencimiento" CssClass="span-field" Text="" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="wrapper-field tipo-tarjeta">
                                <div class="row">
                                    <asp:Label ID="lblTipoTarjeta" CssClass="span-field" Text="" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                </div>

                <div class="clear"></div>
                <div class="below-content blog">
                    <h3 class="blog-title">TOURS AVAILABLE TO ENJOY DURING YOUR HOLIDAYS IN MANATUS</h3>
                    <div class="jcarousel">
                        <ul>
                            <li>
                                <div class="blog-teaser">
                                    <div class="imagen-precio">
                                        <img src="images/tour1-reservacionExitosa.jpg" alt="" />
                                        <div class="precio">
                                            $ 123
                                            <span>by person</span>
                                        </div>
                                    </div>
                                    <div class="detalle">
                                        <h3>Turtle nesting</h3>
                                        <p class="descripcion-corta">Enjoy live nesting of the Green Turtle (only in the months of July, August, September and October)</p>
                                        <div class="boton-contacto">
                                            <a href="http://manatuscostarica.com/contact-us">Contact us to reserve</a>
                                        </div>
                                    </div>
                                </div>
                            </li>
                            <li>
                                <div class="blog-teaser">
                                    <div class="imagen-precio">
                                        <img src="images/tour2-reservacionExitosa.jpg" alt="" />
                                        <div class="precio">
                                            $ 123
                                            <span>by person</span>
                                        </div>
                                    </div>
                                    <div class="detalle">
                                        <h3>Tortuguero Channels</h3>
                                        <p class="descripcion-corta">Ride in boat through amazing channels of Tortuguero National Park</p>
                                        <div class="boton-contacto">
                                            <a href="http://manatuscostarica.com/contact-us">Contact us to reserve</a>
                                        </div>
                                    </div>
                                </div>
                            </li>
                            <li>
                                <div class="blog-teaser">
                                    <div class="imagen-precio">
                                        <img src="images/tour3-reservacionExitosa.jpg" alt="" />
                                        <div class="precio">
                                            $ 123
                                            <span>by person</span>
                                        </div>
                                    </div>
                                    <div class="detalle">
                                        <h3>Canopy</h3>
                                        <p class="descripcion-corta">The adrenalin will be your companion while soaring through the treetops.</p>
                                        <div class="boton-contacto">
                                            <a href="http://manatuscostarica.com/contact-us">Contact us to reserve</a>
                                        </div>
                                    </div>
                                </div>
                            </li>
                            <li>
                                <div class="blog-teaser">
                                    <div class="imagen-precio">
                                        <img src="images/tour1-reservacionExitosa.jpg" alt="" />
                                        <div class="precio">
                                            $ 123
                                            <span>by person</span>
                                        </div>
                                    </div>
                                    <div class="detalle">
                                        <h3>Turtle nesting</h3>
                                        <p class="descripcion-corta">Enjoy live nesting of the Green Turtle (only in the months of July, August, September and October)</p>
                                        <div class="boton-contacto">
                                            <a href="http://manatuscostarica.com/contact-us">Contact us to reserve</a>
                                        </div>
                                    </div>
                                </div>
                            </li>
                            <li>
                                <div class="blog-teaser">
                                    <div class="imagen-precio">
                                        <img src="images/tour2-reservacionExitosa.jpg" alt="" />
                                        <div class="precio">
                                            $ 123
                                            <span>by person</span>
                                        </div>
                                    </div>
                                    <div class="detalle">
                                        <h3>Tortuguero Channels</h3>
                                        <p class="descripcion-corta">Ride in boat through amazing channels of Tortuguero National Park</p>
                                        <div class="boton-contacto">
                                            <a href="http://manatuscostarica.com/contact-us">Contact us to reserve</a>
                                        </div>
                                    </div>
                                </div>
                            </li>
                            <li>
                                <div class="blog-teaser">
                                    <div class="imagen-precio">
                                        <img src="images/tour3-reservacionExitosa.jpg" alt="" />
                                        <div class="precio">
                                            $ 123
                                            <span>by person</span>
                                        </div>
                                    </div>
                                    <div class="detalle">
                                        <h3>Canopy</h3>
                                        <p class="descripcion-corta">The adrenalin will be your companion while soaring through the treetops.</p>
                                        <div class="boton-contacto">
                                            <a href="http://manatuscostarica.com/contact-us">Contact us to reserve</a>
                                        </div>
                                    </div>
                                </div>
                            </li>
                        </ul>
                    </div>
                    <!-- Prev/next controls -->
                    <a href="#" class="jcarousel-control-prev inactive" data-jcarouselcontrol="true">‹</a>
                    <a href="#" class="jcarousel-control-next" data-jcarouselcontrol="true">›</a>
                </div>
            </div>
        </div>
        <div id="footer">
            <div id="footer-inner" class="container-980">
                <div class="menu-footer-responsivo">
                    <ul class="mi-menu-responsivo">
                        <li><a href="http://manatuscostarica.com/manatus-experience">Manatus Experience</a>
                        </li>
                        <li><a href="http://manatuscostarica.com/what-to-do">What to do?</a> </li>
                        <li><a href="http://manatuscostarica.com/package-and-rates">Packages &amp; Rates</a>
                        </li>
                        <li><a href="http://manatuscostarica.com/tortuguero-area">Tortuguero</a></li>
                        <li class="active-trail"><a href="http://booking.manatuscostarica.com/reservacion_en_paso1.aspx">
                            Reservations</a></li>
                        <li><a href="http://manatuscostarica.com/blog">Blog</a></li>
                    </ul>
                </div>
                <div class="column1 column">
                    <ul>
                        <li><a href="http://booking.manatuscostarica.com/reservacion_en_paso1.aspx" class="active-trail">
                            Reservations</a></li>
                        <li><a href="http://manatuscostarica.com/manatus-experience">Hotel Experience</a></li>
                        <li><a href="http://manatuscostarica.com/tortuguero-area">Tortuguero</a></li>
                    </ul>
                </div>
                <div class="column2 column">
                    <ul>
                        <li><a href="http://manatuscostarica.com/package-and-rates">Packages & Rates</a></li>
                        <li><a href="http://manatuscostarica.com/blog">Blog</a></li>
                        <li><a href="http://manatuscostarica.com/gallery">Gallery</a></li>
                    </ul>
                </div>
                <div class="column3 column">
                    <ul>
                        <li><a href="http://manatuscostarica.com/virtual-tours/garden">Virtual Tours</a></li>
                        <li><a href="http://manatuscostarica.com/what-to-do">What to do?</a></li>
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
                            <li><a href="http://manatuscostarica.com/">Home</a></li>|<li><a href="http://booking.manatuscostarica.com/reservacion_en_paso1.aspx">
                                Book Now</a></li>|<li><a href="http://manatuscostarica.com/gallery">Gallery</a></li>|<li>
                                    <a href="http://manatuscostarica.com/contact-us">Contact</a></li></ul>
                    </div>
                    <div class="contact-info">
                        <p>
                            MANATUS HOTEL, TORTUGUERO COSTA RICA | TEL: <a href="tel: +50627098197">(506) 2709.8197</a></p>
                    </div>
                    <div class="menu-idioma">
                        <ul>
                            <li><a href="reservacion_en_paso1.aspx" class="active">English</a></li>|<li><a href="reservacion_es_paso1.aspx">
                                Español</a></li></ul>
                    </div>
                </div>
            </div>
        </div>     
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
