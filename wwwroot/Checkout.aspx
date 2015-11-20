<%@ Page Language="VB" AutoEventWireup="false" CodeFile="checkout.aspx.vb" Inherits="reservacion_sp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Online hotel reservations, booking a hotel in Tortuguero
    </title>
    <link rel="shortcut icon" href="images/images2/favicon.ico" />
     <meta content="width=device-width; initial-scale=1.0; maximum-scale=1.0; user-scalable=0;" name="viewport">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="y_key" content="64cc9486b65ce6b5" />
    <meta name="revisit-after" content="4 days" />
    <meta name="robots" content="INDEX,FOLLOW" />
    <meta name="GOOGLEBOT" content="INDEX, FOLLOW" />
    <meta name="author" content="Poveda" />
    <meta name="keywords" content="Hotel Manatus Tortuguero Costa Rica on-line reservations, booking, wedding costa rica, honeymoon costa rica" />
    <meta name="description" content="Hotel reservations at Manatus Hotel Tortuguero Costa Rica, on-line reservations.  Visit a wildlife sanctuary" />
    <!--<link href="styles/styles_new.css" rel="stylesheet" type="text/css" />
    <link href="styles/stylereservations.css" rel="stylesheet" type="text/css" />-->
    <link type="text/css" rel="stylesheet" href="http://fonts.googleapis.com/css?family=Open+Sans:400,300,600,700|Open+Sans+Condensed:300,700|Raleway:400,300,600,700">
	<link href="styles/stylereservations_nuevo.css" rel="stylesheet" type="text/css" />

    <!--[if gte IE 9]><!-->
        <link rel="stylesheet" href="styles/stylereservation_nuevo_responsivo.css" type="text/css" />
    <!--<![endif]-->

    <!--[if lte IE 7]>
        <link type="text/css" rel="stylesheet" href="styles/stylereservations_nuevo_ie7.css" />
        
  <![endif]-->

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.1/jquery.min.js"></script>


    <script type="text/javascript" src="http://booking.manatuscostarica.com/js/myJS.js"></script>
    <script type="text/javascript" src="http://booking.manatuscostarica.com/js/tinynav.min.js"></script>
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
    <style type="text/css">
        
        
        .style1
        {
            height: 298px;
        }
        .style2
        {
            height: 298px;
            width: 3px;
        }
    </style>
<script type="text/javascript">

  var _gaq = _gaq || [];
  _gaq.push(['_setAccount', 'UA-7635413-1']);
  _gaq.push(['_setDomainName', 'manatuscostarica.com']);
  _gaq.push(['_setAllowLinker', true]);
  _gaq.push(['_trackPageview']);

  (function() {
    var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
    ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
    var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
  })();

</script>
<script>
var urlactual = document.URL.replace( /#.*/, "");
urlactual = urlactual.replace( /\?.*/, "");
_gaq.push(['_trackEvent', 'Reservación', 'Paso 3 checkout', urlactual]);
</script>    
</head>
<body class="checkout">
    <form id="form1" runat="server">
    <asp:scriptmanager id="ScriptManager1" runat="server">
    </asp:scriptmanager>
    <div id="header">
		<div id="header-inner" class="container-980">
			<div id="logo-box">
				<a href="http://manatuscostarica.com"><img src="images/2014/logo.png" /></a>
			</div>
            <div id="header_menus_eng" runat="server">
			    <div class="menu-top-box">
				    <div class="menu-idioma">
					    <ul><li><a href="reservacion_en.aspx" class="active">English</a></li>|<li><a href="reservacion_sp.aspx">Español</a></li></ul>
				    </div>
				    <div class="menu-top">
					    <ul><li><a href="http://manatuscostarica.com/">Home</a></li>|<li><a href="http://booking.manatuscostarica.com/reservacion_en.aspx">Book Now</a></li>|<li><a href="http://manatuscostarica.com/gallery">Gallery</a></li>|<li><a href="http://manatuscostarica.com/contact-us">Contact</a></li></ul>
				    </div>
				    <div class="contact-info">
					    <p>MANATUS HOTEL, TORTUGUERO COSTA RICA | RESERVATIONS: <a href="tel: +50622397364">(506) 2239.7364</a> | HOTEL: <a href="tel: +50627098197">(506) 2709.8197</a></p>
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
					    <li class="active-trail"><a href="http://booking.manatuscostarica.com/reservacion_en.aspx">Reservations</a></li>
					    <li><a href="http://manatuscostarica.com/blog">Blog</a></li></ul>

                        <ul class="mi-menu-responsivo">
                        <li><a href="http://manatuscostarica.com/manatus-experience">Manatus Experience</a>
                        
                        </li>
					    <li><a href="http://manatuscostarica.com/what-to-do">What to do?</a>
                        
                        </li>
					    <li><a href="http://manatuscostarica.com/package-and-rates">Packages &amp; Rates</a>
                        
                        </li>
					    <li><a href="http://manatuscostarica.com/tortuguero-area">Tortuguero</a></li>
					    <li class="active-trail"><a href="http://booking.manatuscostarica.com/reservacion_en.aspx">Reservations</a></li>
					    <li><a href="http://manatuscostarica.com/blog">Blog</a></li></ul>
				    </div>
			    </div>
            </div>
            <div id="header_menus_esp" runat="server">
                <div class="menu-top-box">
				    <div class="menu-idioma">
					    <ul><li><a href="reservacion_en.aspx">English</a></li>|<li><a href="reservacion_sp.aspx" class="active">Español</a></li></ul>
				    </div>
				    <div class="menu-top">
					    <ul><li><a href="http://manatuscostarica.com/es/">Inicio</a></li>|<li><a href="http://booking.manatuscostarica.com/reservacion_sp.aspx">Reservar</a></li>|<li><a href="http://manatuscostarica.com/es/gallery">Galeria</a></li>|<li><a href="http://manatuscostarica.com/es/contacto">Contacto</a></li></ul>
				    </div>
				    <div class="contact-info">
					    <p>HOTEL MANATUS, TORTUGUERO COSTA RICA | TEL: <a href="tel: +50627098197">(506) 2709.8197</a></p>
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
					    <li><a href="http://manatuscostarica.com/qué-hacer">¿Qué hacer?</a>
                            <ul>
                                <li><a href="http://manatuscostarica.com/es/tours-actividades">Tours de Aventura y Actividades</a></li>
                                <li><a href="http://manatuscostarica.com/es/pesca-deportiva">Tours de Pesca Deportiva</a></li>
                            </ul>
                        </li>
					    <li><a href="http://manatuscostarica.com/es/paquetes-tarifas">Paquetes &amp; Tarifas</a>
                            <ul>
                                <li><a href="http://manatuscostarica.com/es/paquetes-luna-de-miel">Paquetes de Luna de Miel</a></li>
                                <li><a href="http://manatuscostarica.com/es/paquetes-regulares-tarifas">Paquetes Regulares</a></li>
                                <li><a href="http://manatuscostarica.com/es/paquetes-especiales">Paquetes Especiales</a></li>
                            </ul>
                        </li>
					    <li><a href="http://manatuscostarica.com/es/area-de-tortuguero">Tortuguero</a></li>
					    <li class="active-trail"><a href="http://booking.manatuscostarica.com/reservacion_sp.aspx">Reservations</a></li>
					    <li><a href="http://manatuscostarica.com/es/blog">Blog</a></li></ul>

                        <ul class="mi-menu-responsivo">
                        <li><a href="http://manatuscostarica.com/es/experiencia-manatus">Experiencia Manatus</a>
                        
                        </li>
					    <li><a href="http://manatuscostarica.com/qué-hacer">¿Qué hacer?</a>
                        
                        </li>
					    <li><a href="http://manatuscostarica.com/es/paquetes-tarifas">Paquetes &amp; Tarifas</a>
                        
                        </li>
					    <li><a href="http://manatuscostarica.com/es/area-de-tortuguero">Tortuguero</a></li>
					    <li class="active-trail"><a href="http://booking.manatuscostarica.com/reservacion_sp.aspx">Reservaciones</a></li>
					    <li><a href="http://manatuscostarica.com/es/blog">Blog</a></li></ul>
				    </div>
			    </div>
            </div>
			<div id="page-head">
				<div class="page-title">
					<h1 id="titulo_reservacion_h1_eng" runat="server"  title="On-line reservations, booking at Manatus, tortuguero hotel reservations" class="h1ReservaNew">RESERVATION</h1>
                    <h1 id="titulo_reservacion_h1_esp" runat="server"  title="Reservaciones On-line, reservas en el hotel Manatus" class="h1ReservaNew">RESERVACIÓN</h1>
				</div>
				<div class="breadcrumbs" id="breadcrumbs_esp" runat="server">
                    <a href="http://manatuscostarica.com/es/" title="P&aacute;gina principal Hotel Manatus Tortuguero Costa Rica">Inicio</a> / Reservación
                </div>

                <div class="breadcrumbs" id="breadcrumbs_eng" runat="server">
                     <a href="http://manatuscostarica.com/" title="P&aacute;gina principal Hotel Manatus Tortuguero Costa Rica">Home</a> / Reservation
                </div>
			</div>
		</div>
    
    </div>

    <div id="main-content">

                <div class="content container-980">

                               <div class="content-box container-full">
                            <%--Keylor Mora - mayo 2013: Se comentó el breadcrumb, y se pasaron los h1 como labels además de los parrafos --%>
                                <%--<div class="breadcrumbs">
                                    <a href="home_sp.html" title="P&aacute;gina principal Hotel Manatus Tortuguero Costa Rica">
                                        principal</a> / reservaciones
                                </div>--%>

                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <asp:Panel ID="pnl_principal" runat="server">
                                <h2  class="h2ReservaNew">
                                    <asp:Label ID="titulo_pagina_checkout" runat="server" Text="PAYMENT DESCRIPTION"></asp:Label></h2>
                                    <p style="margin-top: 0px" class="texto-payment-description" ID="parrafo1_checkout" runat="server" >Below is the full amount of the payment for the reservation and the available credit cards. Check this amount and the type of credit card that you want to use.</p>
                                    <p style="margin-top: 0px" class="texto-payment-description"   ID="parrafo2_checkout" runat="server">Then, press the button <b>fulfill your payment</b> that will take you to the virtual bank where you can complete the payment of the reservation in Manatus Hotel.</p>
                                    <p style="margin-top: 0px" class="texto-payment-description"  ID="parrafo1_checkout_esp" runat="server" >A continuación se muestra el monto total del pago a realizar por la reservación y las tarjetas disponibles para llevarlo a cabo. Compruebe este monto y el tipo de tarjeta que utilizará.</p>
                                    <p style="margin-top: 0px" class="texto-payment-description"   ID="parrafo2_checkout_esp" runat="server">Finalmente pulse el botón de <b>completar el pago</b> que lo llevará la sucursal del banco virtual donde podrá finalizar el pago de la reserva en Hotel Manatus.</p>
                                <%--<div class="bodeAbajo">
                                
                                </div>--%>
                                 <div class="derecha botones-checkout-arriba">
                                      <%--Keylor Mora arro - mayo2013: Añadir botón de Cancelar y cambiar el de checkOut --%>
                                                            
                                      <%--<asp:ImageButton ImageUrl="~/images/Otros/checkout.gif" ID="btn_continuar" runat="server" Text="Checkout" CssClass="botones" 
                                                                            ValidationGroup="otro"/>--%>
                                     <asp:ImageButton ID="btn_cancelar_arriba" runat="server" style="float:left" CssClass="marginRight15" ImageUrl="~/images/2014/img-btn-back.png" />
                                                                        <asp:ImageButton ID="btn_pagar_arriba" runat="server" ImageUrl="~/images/2014/completar_pago_eng.jpg" />
                                                         
                                 </div>
                                <br />

                                <table id="tabla_titulo_esp" class="tableShopping" cellpadding="0" cellspacing="0" border="0" width="100%">
                                                            <tr>
                                                                <td class="celda-titulo-tabla-shoppingcart" style="width: 30%">
                                                                    <h2 class="titulo-tabla-shoppincart">
                                                                        <asp:Label ID="lbl_service" runat="server" Text="Service"></asp:Label></h2>
                                                                </td>
                                                                <td class="celda-titulo-tabla-shoppingcart" style="width: 10%">
                                                                    <h2 class="titulo-tabla-shoppincart">
                                                                        <asp:Label ID="lbl_dias" runat="server" Text="Rooms"></asp:Label></h2>
                                                                </td>
                                                                <td class="celda-titulo-tabla-shoppingcart" style="width: 15%">
                                                                    <h2 class="titulo-tabla-shoppincart">
                                                                        <asp:Label ID="lbl_montoventa" runat="server" Text="Service Amount"></asp:Label></h2>
                                                                </td>
                                                                <td class="celda-titulo-tabla-shoppingcart" style="width: 21%">
                                                                    <h2 class="titulo-tabla-shoppincart">
                                                                        <asp:Label ID="lbl_extras" runat="server" Text="Extra night(s) amount"></asp:Label></h2>
                                                                </td>
                                                                <td class="celda-titulo-tabla-shoppingcart" style="width: 12%">
                                                                    <h2 class="titulo-tabla-shoppincart">
                                                                        <asp:Label ID="lbl_descuento" runat="server" Text="Discount"></asp:Label></h2>
                                                                </td>
                                                                <td class="celda-titulo-tabla-shoppingcart" style="width: 17%">
                                                                    <h2 class="titulo-tabla-shoppincart">
                                                                        <asp:Label ID="lbl_subTotal" runat="server" Text="SubTotal"></asp:Label></h2>
                                                                </td>
                                                            </tr>
                                                        </table>
                                
                                <asp:DataList ID="dtl_shoppingCart" runat="server" RepeatDirection="Horizontal" Width="100%"
                                                            RepeatColumns="1">
                                                            <%--<ItemStyle CssClass="bordeAbajoShopping" />--%>
                                                            <ItemTemplate>
                                                                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                                    <tr>
                                                                        <td class="alignLeft" style="width: 30%; padding-left: 12px;">
                                                                            <asp:Label ID="lbl_producto" runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                        <td class="alignLeft" style="width: 10%; padding-left: 12px;">
                                                                            <asp:Label ID="lbl_cantidad" runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                        <td class="alignLeft" style="width: 15%; padding-left: 12px;">
                                                                            <asp:Label ID="lbl_montoventa" runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                        <td class="alignLeft" style="width: 21%; padding-left: 12px;">
                                                                            <asp:Label ID="lbl_extras" runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                        <td class="alignLeft" style="width: 12%; padding-left: 12px;">
                                                                            <asp:Label ID="lbl_desc" runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                        <td class="alignLeft" style="width: 17%; padding-left: 12px;">
                                                                            <asp:Label ID="lbl_subTot" runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <span>
                                                                    <asp:Label ID="lbl_mensaje" runat="server" Text="Label" Visible="false"></asp:Label>
                                                                </span>
                                                            </ItemTemplate>
                                                        </asp:DataList>

                                                        <asp:DataList ID="dtl_shoppingCart_responsivo" runat="server" RepeatDirection="Horizontal" Width="100%"
                                                            RepeatColumns="1">
                                                            <%--<ItemStyle CssClass="bordeAbajoShopping" />--%>
                                                            <ItemTemplate>
                                                                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                                    <tr>
                                                                        <td class="celda-titulo-tabla-shoppingcart" style="width: 30%">
                                                                    <h2 class="titulo-tabla-shoppincart">
                                                                        <asp:Label ID="lbl_service" runat="server" Text="Service"></asp:Label></h2>
                                                                </td>
                                                                
                                                                        <td class="celda-valor-tabla-shoppingcart alignLeft" style="width: 30%; padding-left: 12px;">
                                                                            <asp:Label ID="lbl_producto_responsivo" runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="celda-titulo-tabla-shoppingcart" style="width: 10%">
                                                                    <h2 class="titulo-tabla-shoppincart">
                                                                        <asp:Label ID="lbl_dias" runat="server" Text="Rooms"></asp:Label></h2>
                                                                </td>
                                                                
                                                                        <td class="alignLeft celda-valor-tabla-shoppingcart" style="padding-left: 12px; width: 10%;">
                                                                            <asp:Label ID="lbl_cantidad_responsivo" runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="celda-titulo-tabla-shoppingcart" style="width: 15%">
                                                                    <h2 class="titulo-tabla-shoppincart">
                                                                        <asp:Label ID="lbl_montoventa" runat="server" Text="Service Amount"></asp:Label></h2>
                                                                </td>
                                                                        <td class="width15porciento alignLeft celda-valor-tabla-shoppingcart" style="padding-left: 12px; width: 15%;">
                                                                            <asp:Label ID="lbl_montoventa_responsivo" runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="celda-titulo-tabla-shoppingcart" style="width: 20%">
                                                                        <h2 class="titulo-tabla-shoppincart">
                                                                            <asp:Label ID="lbl_extras" runat="server" Text="Extra Night(s) Amount"></asp:Label></h2>
                                                                    </td>
                                                                        <td class="alignLeft celda-valor-tabla-shoppingcart" style="width: 20%; padding-left: 12px;">
                                                                            <asp:Label ID="lbl_extras_responsivo" runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="celda-titulo-tabla-shoppingcart" style="width: 13%">
                                                                        <h2 class="titulo-tabla-shoppincart">
                                                                            <asp:Label ID="lbl_descuento" runat="server" Text="Discount"></asp:Label></h2>
                                                                    </td>
                                                                        <td class="alignLeft celda-valor-tabla-shoppingcart" style="width: 13%; padding-left: 12px;">
                                                                            <asp:Label ID="lbl_descuento_responsivo" runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="celda-titulo-tabla-shoppingcart" style="width: 17%">
                                                                        <h2 class="titulo-tabla-shoppincart">
                                                                            <asp:Label ID="lbl_subTotal" runat="server" Text="Sub Total"></asp:Label></h2>
                                                                    </td>
                                                                        <td class="alignLeft celda-valor-tabla-shoppingcart" style="width: 17%; padding-left: 12px;">
                                                                            <asp:Label ID="lbl_subTot_responsivo" runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <span>
                                                                    <asp:Label ID="lbl_mensaje" runat="server" Text="Label" Visible="false"></asp:Label>
                                                                </span>
                                                            </ItemTemplate>
                                                        </asp:DataList>
                                <asp:updatepanel id="updtpanel_reservacion" runat="server">
                                    <ContentTemplate>
                                        <asp:Panel ID="panel" runat="server" Visible="true">
                                                         <asp:Panel ID="pnl_contenido" runat="server" Visible="true" Width="727px">
                                                <%-- Keylor Mora Garro - cambios mayo 2013 - se cambio la estructura de esta tabla --%>
                                               
                                                            <asp:Panel ID="pnl_total" runat="server" CssClass="CarritoTotal">
                                                                <table cellpadding="5" cellspacing="0" width="570px">
                                                                    <tr>
                                                                        <td class="CarritoTotalTableTD1 columna1" width="50%">
                                                                            <span class="CarritoTotalTitulo">Subtotal</span>
                                                                        </td>
                                                                        <td class="CarritoTotalTableTD2 columna2" width="50%">
                                                                            <div class="borde-cafe-tabla-td-interno-bg-blanco"><asp:Label ID="lbl_monto" runat="server" Text="$0"></asp:Label></div>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="CarritoTotalTableTD1 columna1" width="50%">
                                                                            <span class="CarritoTotalTitulo" id="label_Taxes" runat="server">Taxes</span>
                                                                        </td>
                                                                        <td class="CarritoTotalTableTD2 columna2" width="50%">
                                                                            <div class="borde-cafe-tabla-td-interno-bg-blanco"><asp:Label ID="lbl_taxes" runat="server" Text="$0"></asp:Label></div>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="CarritoTotalTR-TD1 columna1 last-row" width="50%">
                                                                            <span class="CarritoTotalTitulo">Total</span>
                                                                        </td>
                                                                        <td class="CarritoTotalTR-TD2 last-row columna2" width="50%">
                                                                            <asp:Label ID="lbl_Total" runat="server" Text="$0"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                        
                                                        
                                                            
                                                        <!--<asp:ListItem Text="Pay using:" Value="paypal"></asp:ListItem>-->
                                                        
                                                            
                                                    <div class="radio-buttom-box">                                                        <asp:RadioButtonList ID="radio_checkout" runat="server" RepeatDirection="Vertical"
                                                                CssClass="subtitulos" CellPadding="0" CellSpacing="0">
                                                                <asp:ListItem Selected="True" Text="Pay with credit card using:" Value="bncr"></asp:ListItem>                                                                
                                                            </asp:RadioButtonList></td>
                                                    
                                                     </div>
   
                                                             <%--   <br />
                                                            &nbsp; &nbsp;
                                                        <img src="images/logo_ccMC.gif" alt="manatuscostarica.com" />
                                                            <img src="images/logo_ccVisa.gif" alt="manatuscostarica.com" />--%>
                                                            
                                                               <asp:Panel ID="Panel1" BackColor="White" runat="server">
                                                                    
                                                                           <%-- Editado por Keylor Mora - mayo 2013--%>
                                                                            
                                                                                <div class="logos-bancos">
                                                                                    <img src="BNCR/logo_bncr.png" alt="Banco Nacional de Costa Rica" />
                                                                            
                                                                                <a href="http://www.mastercard.com/cr/gateway.html"
                                                                                    target="_blank">
                                                                                    <img src="BNCR/logo_mastercard.png" alt="" /></a>
                                                                            
                                                                                
                                                                                    <img src="BNCR/logo_discover.png" alt="" />
                                                                            
                                                                                <a href=" http://www.visa.com/globalgateway/gg_selectcountry.html?retcountry=1" target="_blank">
                                                                                    <img src="BNCR/logo_visa.png" alt="" /></a>
                                                                            
                                                                                <a href="https://usa.visa.com/personal/security/vbv/index.html?it=wb|/|Learn%20More"
                                                                                    target="_blank">
                                                                                    <img src="BNCR/logo_verified.png" alt=""  /></a>
                                                                            
                                                                                <a href="http://www.mastercard.com/us/merchant/security/what_can_do/SecureCode/index.html"
                                                                                    target="_blank">
                                                                                    <img src="BNCR/logo_secure.png" alt=""  /></a>
                                                                                </div>
                                                                                <div class="nota-debajo-box">
                                                                                <asp:Label ID="nota_debajo" runat="server" class="notafinalpago" Text="This establishment is authorized by Visa to perform electronic transactions.">
                                                                                </asp:Label>                 
                                                                                
                                                                                </div>                                                               
                                                                           
                                                                </asp:Panel>

                                                            <br />
                                                         
                                                            <!--<img src="images/paypal_logo.gif" alt="manatuscostarica.com" />-->
                                                            &nbsp; &nbsp;
                                                            
                                                            <%--Comentado por Keylor Mora - mayo 2013--%>
                                                            <%--<img src="images/logo_ccBank.gif" alt="manatuscostarica.com" />
                                                            <img src="images/logo_ccDiscover.gif" alt="manatuscostarica.com" />
                                                            <img src="images/logo_ccMC.gif" alt="manatuscostarica.com" />
                                                            <img src="images/logo_ccVisa.gif" alt="manatuscostarica.com" />--%>
                                                        
                                                    
                                               <asp:Label ID="lbl_term" runat="server" Visible="false"></asp:Label>
                                               
                                                <%--<table cellpadding="0" cellspacing="0" class="Shipping">
                                                    <tr>
                                                        <td style=" padding-top: 105px; padding-right: 15px; padding-left: 15px; vertical-align: top" class="style2">
                                                            <asp:Panel ID="pnl_total" runat="server" CssClass="CarritoTotal">
                                                                <table cellpadding="5" cellspacing="0">
                                                                    <tr>
                                                                        <td class="CarritoTotalTableTD1">
                                                                            <span class="CarritoTotalTitulo">Subtotal</span>
                                                                        </td>
                                                                        <td class="CarritoTotalTableTD2">
                                                                            <asp:Label ID="lbl_monto" runat="server" Text="$0"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <span class="CarritoTotalTitulo">Taxes</span>
                                                                        </td>
                                                                        <td>
                                                                            <asp:Label ID="lbl_taxes" runat="server" Text="$0"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="CarritoBordeArriba">
                                                                            <span class="CarritoTotalTitulo">Total</span>
                                                                        </td>
                                                                        <td class="CarritoBordeArriba">
                                                                            <asp:Label ID="lbl_Total" runat="server" Text="$0"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </asp:Panel>
                                                        </td>
                                                        <td vertical-align: top" 
                                                            class="style1">
                                                            <asp:RadioButtonList ID="radio_checkout" runat="server" RepeatDirection="Vertical"
                                                                CssClass="subtitulos" CellPadding="0" CellSpacing="0" Height="299px" 
                                                                Width="177px">
                                                                <asp:ListItem Selected="True" Text="Pay with credit card using:" Value="bncr"></asp:ListItem>                                                                
                                                            </asp:RadioButtonList>
                                                        </td><!--<asp:ListItem Text="Pay using:" Value="paypal"></asp:ListItem>-->
                                                        <td vertical-align: top" class="style1">
                                                            <img src="images/bncr_logo.gif" alt="manatuscostarica.com" />
                                                             <%--   <br />
                                                            &nbsp; &nbsp;
                                                        <img src="images/logo_ccMC.gif" alt="manatuscostarica.com" />
                                                            <img src="images/logo_ccVisa.gif" alt="manatuscostarica.com" />--%>
                                                            
                                                               <%--<asp:Panel ID="pnl_BNCR" BackColor="White" runat="server">
                                                                    <table cellpadding="3" cellspacing="0" width="100%">
                                                                        <tr>
                                                                            <td>
                                                                                <a href="http://www.mastercard.com/us/merchant/security/what_can_do/SecureCode/index.html"
                                                                                    target="_blank">
                                                                                    <img src="BNCR/mastercard.gif" alt="" width="50px" /></a>
                                                                            </td>
                                                                            <td>
                                                                                <a href=" http://www.visa.com/globalgateway/gg_selectcountry.html?retcountry=1" target="_blank">
                                                                                    <img src="BNCR/visa.png" alt="" width="50px" /></a>
                                                                            </td>
                                                                            <td>
                                                                                <a href="https://usa.visa.com/personal/security/vbv/index.html?it=wb|/|Learn%20More"
                                                                                    target="_blank">
                                                                                    <img src="BNCR/verifiedbyvisa.png" alt="" width="50px" /></a>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                           <td colspan="3">
                                                                                <asp:Label ID="lbl_visa" runat="server" Text="This establishment is authorized by Visa to perform electronic transactions.">
                                                                                </asp:Label>                                                                                
                                                                           </td> 
                                                                        </tr>
                                                                    </table>
                                                                </asp:Panel>

                                                            <br />
                                                            <br />
                                                            <br />
                                                            <!--<img src="images/paypal_logo.gif" alt="manatuscostarica.com" />-->
                                                            &nbsp; &nbsp;
                                                            <img src="images/logo_ccBank.gif" alt="manatuscostarica.com" />
                                                            <img src="images/logo_ccDiscover.gif" alt="manatuscostarica.com" />
                                                            <img src="images/logo_ccMC.gif" alt="manatuscostarica.com" />
                                                            <img src="images/logo_ccVisa.gif" alt="manatuscostarica.com" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3" class="CarritoBordeArriba" style=" text-align: right;">
                                                            <asp:ImageButton ImageUrl="~/images/Otros/checkout.gif" ID="btn_continuar" runat="server" Text="Checkout" CssClass="botones" 
                                                                            ValidationGroup="otro"/>
                                                         </td>
                                                    </tr>
                                                </table>--%>
                                            </asp:Panel>
                                        </asp:Panel>
                                        
                                    </ContentTemplate>
                                </asp:updatepanel>
                                <center>
                                                        <asp:Label ID="lbl_mensaje" runat="server" Visible="false"> 
                                                        Your Shooping Cart is empty                                                                                       
                                                        </asp:Label>
                                                    </center>
                                

                                
                                <div class="botones-pago-cancel-abajo">
                                                    <div class="derecha">

                                                   
                                                    <%--Keylor Mora arro - mayo2013: Añadir botón de Cancelar y cambiar el de checkOut --%>
                                                            
                                                                <%--<asp:ImageButton ImageUrl="~/images/Otros/checkout.gif" ID="btn_continuar" runat="server" Text="Checkout" CssClass="botones" 
                                                                                ValidationGroup="otro"/>--%>
                                                       <asp:ImageButton ID="btn_cancelar" runat="server" style="float:left" CssClass="marginRight15" ImageUrl="~/images/2014/img-btn-cancel.png" />
                                                                            <asp:ImageButton ID="btn_pagar" runat="server" ImageUrl="~/images/2014/completar_pago_eng.jpg" />
                                                                        </div>
                                                         
                                                    </div>

                                                </div>
                               
                                <%--<div class="bordeArriba">
                                    <div class="fondoRosado">
                                        Take a look at our Virtual Tours                                 
                                    </div>
                                <a href="tourPiscina.html">
                                    <img src="images/images2/Otros/img1.jpg" alt="" /></a><a href="tourHabitacion.html"><img
                                        src="images/images2/Otros/img2.jpg" alt="" /></a><a href="tourJardines.html"><img
                                            src="images/images2/Otros/img3.jpg" alt="" /></a><a href="tourDeck.html"><img src="images/images2/Otros/img4.jpg"
                                                alt="" /></a></div>--%>
                                
                                </asp:Panel>
                              </ContentTemplate>
                           </asp:UpdatePanel>

                    </div>
            </div>
    </div>

    <div id="footer">
		        <div id="footer-inner" class="container-980">
                    <div id="footer_eng" runat="server">
                    <div class="menu-footer-responsivo">
                        <ul class="mi-menu-responsivo">
                            <li><a href="http://manatuscostarica.com/manatus-experience">Manatus Experience</a>
                        
                            </li>
					        <li><a href="http://manatuscostarica.com/what-to-do">What to do?</a>
                        
                            </li>
					        <li><a href="http://manatuscostarica.com/package-and-rates">Packages &amp; Rates</a>
                        
                            </li>
					        <li><a href="http://manatuscostarica.com/tortuguero-area">Tortuguero</a></li>
					        <li class="active-trail"><a href="http://booking.manatuscostarica.com/reservacion_en.aspx">Reservations</a></li>
					        <li><a href="http://manatuscostarica.com/blog">Blog</a></li>
                        </ul>
                    </div>
			        <div class="column1 column">
                        <ul>
                            <li><a href="http://booking.manatuscostarica.com/reservacion_en.aspx" class="active-trail">Reservations</a></li>
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
                
                            <p>Manatus Hotel<br />
                            Tortuguero Costa Rica<br />
                            Tel: <a href="tel:+50627098197">(506) 2709.8197</a></p>
                            <div class="redes-footer">
                            <a href="#"><img src="images/2014/face.png" /></a><a href="#"><img src="images/2014/flickr.png" /></a><a href="#"><img src="images/2014/tripadvisor.png" /></a>
                            </div>
                
                    </div>
                    <div class="footer-info-box">
				        
				        <div class="menu-top">
					        <ul><li><a href="http://manatuscostarica.com/">Home</a></li>|<li><a href="http://booking.manatuscostarica.com/reservacion_en.aspx">Book Now</a></li>|<li><a href="http://manatuscostarica.com/gallery">Gallery</a></li>|<li><a href="http://manatuscostarica.com/contact-us">Contact</a></li></ul>
				        </div>
				        <div class="contact-info">
					        <p>MANATUS HOTEL, TORTUGUERO COSTA RICA | TEL: <a href="tel: +50627098197">(506) 2709.8197</a></p>
				        </div>
                        <div class="menu-idioma">
					        <ul><li><a href="reservacion_en.aspx" class="active">English</a></li>|<li><a href="reservacion_sp.aspx">Español</a></li></ul>
				        </div>
			        </div>	
                    </div>
                    <div id="footer_esp" runat="server">
                    <div class="menu-footer-responsivo">
                        <ul class="mi-menu-responsivo">
                            <li><a href="http://manatuscostarica.com/es/experiencia-manatus">Experiencia Manatus</a>
                        
                            </li>
					        <li><a href="http://manatuscostarica.com/es/qué-hacer">¿Qué hacer?</a>
                        
                            </li>
					        <li><a href="http://manatuscostarica.com/es/paquetes-tarifas">Paquetes &amp; Tarifas</a>
                        
                            </li>
					        <li><a href="http://manatuscostarica.com/es/area-de-tortuguero">Tortuguero</a></li>
					        <li class="active-trail"><a href="http://booking.manatuscostarica.com/reservacion_sp.aspx">Reservaciones</a></li>
					        <li><a href="http://manatuscostarica.com/blog">Blog</a></li>
                        </ul>
                    </div>
                        <div class="column1 column">
                        <ul>
                            <li><a href="http://booking.manatuscostarica.com/reservacion_sp.aspx" class="active-trail">Reservaciones</a></li>
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
                
                            <p>Manatus Hotel<br />
                            Tortuguero Costa Rica<br />
                            Tel: <a href="tel:+50627098197">(506) 2709.8197</a></p>
                            <div class="redes-footer">
                              <a href="https://www.facebook.com/manatuscostarica?fref=ts">
                        <img src="images/2014/face.png" /></a><a href="#"><a
                            href="https://instagram.com/manatuscostarica/"><img src="images/2014/instagram.png" /></a><a
                            href="https://www.pinterest.com/manatuscr/"><img src="images/2014/pinterest.png" /></a><a
                            href="http://www.tripadvisor.com.mx/Hotel_Review-g309268-d308651-Reviews-Manatus_Hotel-Tortuguero_Province_of_Limon.html"><img src="images/2014/tripadvisor.png" /></a>
                            </div>

                            
                
                    </div>
                    <div class="footer-info-box">
				        
				        <div class="menu-top">
					        <ul><li><a href="http://manatuscostarica.com/es/">Inicio</a></li>|<li><a href="http://booking.manatuscostarica.com/reservacion_sp.aspx">Book Now</a></li>|<li><a href="http://manatuscostarica.com/es/gallery">Galeria</a></li>|<li><a href="http://manatuscostarica.com/es/contacto">Contacto</a></li></ul>
				        </div>
				        <div class="contact-info">
					        <p>MANATUS HOTEL, TORTUGUERO COSTA RICA | TEL: <a href="tel: +50627098197">(506) 2709.8197</a></p>
				        </div>
                        <div class="menu-idioma">
					        <ul><li><a href="reservacion_en.aspx">English</a></li>|<li><a href="reservacion_sp.aspx" class="active">Español</a></li></ul>
				        </div>
		            </div>
                    <div class="logos-trip-advisor">
                
                            <div id="TA_certificateOfExcellence690" class="TA_certificateOfExcellence">
                            <ul id="KRmJMGP9Hz" class="TA_links FD5bzwic2KI">
                            <li id="FSOEvM2k4h" class="kTA0rM0">
                            <a target="_blank" href="http://www.tripadvisor.es/Hotel_Review-g309268-d308651-Reviews-Manatus_Hotel-Tortuguero_Province_of_Limon.html"><img src="http://www.tripadvisor.es/img/cdsi/img2/awards/CoE2015_WidgetAsset-14348-2.png" alt="TripAdvisor" class="widCOEImg" id="CDSWIDCOELOGO"/></a>
                            </li>
                            </ul>
                            </div>
                            <script src="http://www.jscache.com/wejs?wtype=certificateOfExcellence&amp;uniq=690&amp;locationId=308651&amp;lang=es&amp;year=2015&amp;display_version=2"></script>


                            <div id="TA_excellent625" class="TA_excellent"><ul id="zWYVpp9Tm" class="TA_links VlM6UjTeF"><li id="9QVb1lq" class="mW6emucXI9Tk"><a target="_blank" href="http://www.tripadvisor.es/"><img src="http://e2.tacdn.com/img2/widget/tripadvisor_logo_115x18.gif" alt="TripAdvisor" class="widEXCIMG" id="CDSWIDEXCLOGO"/></a></li></ul></div><script src="http://www.jscache.com/wejs?wtype=excellent&amp;uniq=625&amp;locationId=308651&amp;lang=es&amp;display_version=2"></script>


                            <div id="TA_tchotel599" class="TA_tchotel"><ul id="7gtLp8IDZ" class="TA_links HyUQ5LRr"><li id="N6jirHyZN" class="9Ri0TFZgX"><a target="_blank" href="http://www.tripadvisor.es/"><img src="http://e2.tacdn.com/img2/branding/taOwlWhite.png" alt="TripAdvisor"/></a></li></ul></div><script src="http://www.jscache.com/wejs?wtype=tchotel&amp;uniq=599&amp;locationId=308651&amp;lang=es&amp;year=2012&amp;display_version=2"></script>

                            <div class="footer_logos">
                            <img src="images/2014/mark_tourism.jpg" alt="Rainforest Alliance" class="margen_footer_logo"><img src="images/2014/Travelife_Logo.png" alt="Travelife">
                            </div>
                        </div>
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
} catch(err) {}</script>
</body>
</html>
