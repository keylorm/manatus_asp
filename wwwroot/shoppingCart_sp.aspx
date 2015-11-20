<%@ Page Language="VB" AutoEventWireup="false" CodeFile="shoppingCart_sp.aspx.vb"
    Inherits="shoppingCar_sp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <title>Reservaciones online, reservar hotel en Tortuguero</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta content="width=device-width; initial-scale=1.0; maximum-scale=1.0; user-scalable=0;" name="viewport">
    <meta name="revisit-after" content="4 days" />
    <meta name="robots" content="INDEX,FOLLOW" />
    <meta name="GOOGLEBOT" content="INDEX, FOLLOW" />
    <meta name="author" content="Poveda" />
    <meta name="keywords" content="Hotel Manatus Tortuguero Costa Rica, reservaciones on-line, reservaciÃÂÃÂÃÂÃÂ³n en lÃÂÃÂÃÂÃÂ­nea, ocupaciÃÂÃÂÃÂÃÂ³n,alojamiento, hospedaje, ocupaciÃÂÃÂÃÂÃÂ³n, mejor hotel, hotel tortuguero, tours, tours de aventura, tours desove de tortugas" />
    <meta name="description" content="Reservaciones de Hotel en Tortuguero Costa Rica" />
    <!--<link href="styles/styles_new.css" rel="stylesheet" type="text/css" />
    <link href="styles/stylereservations.css" rel="stylesheet" type="text/css" />-->
    <link type="text/css" rel="stylesheet" href="http://fonts.googleapis.com/css?family=Open+Sans:400,300,600,700|Open+Sans+Condensed:300,700|Raleway:400,300,600,700">
	<link href="styles/stylereservations_nuevo.css" rel="stylesheet" type="text/css" />

    <!--[if IE]>
	<link rel="stylesheet" type="text/css" href="styles/iestyle.css" />
	<![endif]-->
    <!--[if lt IE 7]>
        <script defer type="text/javascript" src="Scripts/pngfix.js"></script>
    <![endif]-->
    <!--[if lte IE 7]>
        <link type="text/css" rel="stylesheet" href="styles/stylereservations_nuevo_ie7.css" />
        
  <![endif]-->

    
    <!--[if gte IE 9]><!-->
        <link rel="stylesheet" href="styles/stylereservation_nuevo_responsivo.css" type="text/css" />
    <!--<![endif]-->



    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.1/jquery.min.js"></script>


    <script type="text/javascript" src="http://booking.manatuscostarica.com/js/myJS.js"></script>
    <script type="text/javascript" src="http://booking.manatuscostarica.com/js/tinynav.min.js"></script>


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


<!--[if lte IE 9]>
	
<![endif]-->
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
_gaq.push(['_trackEvent', 'ReservaciÃÂ³n', 'Paso 2', urlactual]);
</script> 
</head>
<body class="reservation-details es">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div id="header">
		<div id="header-inner" class="container-980">
			<div id="logo-box">
				<a href="http://manatuscostarica.com"><img src="images/2014/logo.png" /></a>
			</div>
			<div class="menu-top-box">
				<div class="menu-idioma">
					<ul><li><a href="shoppingCart_en.aspx">English</a></li>|<li><a href="shoppingCart_sp.aspx" class="active">Espa&ntilde;ol</a></li></ul>
				</div>
				<div class="menu-top">
					<ul><li><a href="http://manatuscostarica.com/es/">Inicio</a></li>|<li><a href="http://booking.manatuscostarica.com/reservacion_sp.aspx">Reservar</a></li>|<li><a href="http://manatuscostarica.com/es/gallery">Galeria</a></li>|<li><a href="http://manatuscostarica.com/es/contacto">Contacto</a></li></ul>
				</div>
				<div class="contact-info">
					<p>MANATUS HOTEL, TORTUGUERO COSTA RICA | RESERVACIONES: <a href="tel: +50622397364">(506) 2239.7364</a> | HOTEL: <a href="tel: +50627098197">(506) 2709.8197</a></p>
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
                            <li><a href="http://manatuscostarica.com/es/restaurante">Restaurante de Comida Caribe&ntilde;a</a></li>
                            <li><a href="http://manatuscostarica.com/es/piscina">&Aacute;rea de la Piscina</a></li>
                        </ul>
                    </li>
					<li><a href="http://manatuscostarica.com/es/quÃÂ©-hacer">&iquest;Qu&eacute; hacer?</a>
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
					<li><a href="http://manatuscostarica.com/quÃÂÃÂ©-hacer">&iquest;Qu&eacute; hacer?</a>
                        
                    </li>
					<li><a href="http://manatuscostarica.com/es/paquetes-tarifas">Paquetes &amp; Tarifas</a>
                        
                    </li>
					<li><a href="http://manatuscostarica.com/es/area-de-tortuguero">Tortuguero</a></li>
					<li class="active-trail"><a href="http://booking.manatuscostarica.com/reservacion_sp.aspx">Reservaciones</a></li>
					<li><a href="http://manatuscostarica.com/es/blog">Blog</a></li></ul>
                    <ul class="mi-menu-responsivo">
                        <li><a href="http://manatuscostarica.com/es/experiencia-manatus">Experiencia Manatus</a>
                        
                    </li>
					<li><a href="http://manatuscostarica.com/quÃÂÃÂ©-hacer">&iquest;QuÃÂÃÂ© hacer?</a>
                        
                    </li>
					<li><a href="http://manatuscostarica.com/es/paquetes-tarifas">Paquetes &amp; Tarifas</a>
                        
                    </li>
					<li><a href="http://manatuscostarica.com/es/area-de-tortuguero">Tortuguero</a></li>
					<li class="active-trail"><a href="http://booking.manatuscostarica.com/reservacion_sp.aspx">Reservaciones</a></li>
					<li><a href="http://manatuscostarica.com/es/blog">Blog</a></li></ul>
				</div>
			</div>
			<div id="page-head">
				<div class="page-title">
					<h1 title="Reservaciones On-line, reservas en el hotel Manatus" class="h1ReservaNew">RESERVACI&Oacute;N</h1>
				</div>
				<div class="breadcrumbs">
                     <a href="http://manatuscostarica.com/es/" title="P&aacute;gina principal Hotel Manatus Tortuguero Costa Rica">Inicio</a> / Reservaci&oacute;n
                </div>
			</div>
		</div>
    
    </div>
      <div id="main-content">

                <div class="content container-980">

                               <div class="content-box container-full">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <asp:Panel ID="pnl_principal" runat="server">
                                                        <h2 class="h2ReservaNew">
                                                            <!-- AÃÂÃÂ±adido por Keylor Mora para cambios de mayo 2013: se cambio texto "Carrito de Compra por" por "Detalles de ReservaciÃÂÃÂ³n" -->
                                                            <%--<asp:Label ID="Label1" runat="server" Text="Shopping Cart"></asp:Label>--%>
                                                            <asp:Label ID="Label1" runat="server" Text="DETALLES DE LA RESERVACI&Oacute;N"></asp:Label>
                                                        </h2>
                                                        <p  class="textonegro_normal" style="margin-top: 0px">Por favor verifique que los detalles que se muestran a continuaci&oacute;n son los que usted eligi&oacute; y luego de eso pulse el bot&oacute;n de <a href="checkout.aspx">realizar el pago.</a></p>
                                                        <%-- Cambios de Keylor para mayo 2013: se cambio el estilo de los botones, original esta comentado --%>
                                                                    <div class="derecha botones-checkout-arriba">
                                                                        <asp:ImageButton ID="btn_cancelar_arriba" runat="server" style="float:left" CssClass="marginRight15" ImageUrl="~/images/2014/img-btn-back-sp.png" />
                                                                        <asp:ImageButton ID="btn_pagar_arriba" runat="server" ImageUrl="~/images/2014/img-btn-make-your-payment-sp.png" />
                                                                    </div>
                                                        <%-- Cambios de Keylor para mayo 2013: se aÃÂÃÂ±adieron clases a los td y h2 y se cambiaron % de los width de los td--%>
                                                        <table class="tableShopping" cellpadding="0" cellspacing="0" border="0" width="100%">
                                                            <tr>
                                                                <td class="celda-titulo-tabla-shoppingcart" style="width: 30%">
                                                                    <h2 class="titulo-tabla-shoppincart">
                                                                        <asp:Label ID="lbl_service" runat="server" Text="Servicio"></asp:Label></h2>
                                                                </td>
                                                                <td class="celda-titulo-tabla-shoppingcart" style="width: 10%">
                                                                    <h2 class="titulo-tabla-shoppincart">
                                                                        <asp:Label ID="lbl_dias" runat="server" Text="Hab."></asp:Label></h2>
                                                                </td>
                                                                <td class="celda-titulo-tabla-shoppingcart" style="width: 15%">
                                                                    <h2 class="titulo-tabla-shoppincart">
                                                                        <asp:Label ID="lbl_montoventa" runat="server" Text="Monto Venta"></asp:Label></h2>
                                                                </td>
                                                                <td class="celda-titulo-tabla-shoppingcart" style="width: 20%">
                                                                    <h2 class="titulo-tabla-shoppincart">
                                                                        <asp:Label ID="lbl_extras" runat="server" Text="Monto Noche(s) Adicionales"></asp:Label></h2>
                                                                </td>
                                                                <td class="celda-titulo-tabla-shoppingcart" style="width: 13%">
                                                                    <h2 class="titulo-tabla-shoppincart">
                                                                        <asp:Label ID="lbl_descuento" runat="server" Text="Descuento"></asp:Label></h2>
                                                                </td>
                                                                <td class="celda-titulo-tabla-shoppingcart" style="width: 17%">
                                                                    <h2 class="titulo-tabla-shoppincart">
                                                                        <asp:Label ID="lbl_subTotal" runat="server" Text="Sub Total"></asp:Label></h2>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <%--<table class="tableShopping" cellpadding="0" cellspacing="0" border="0" width="100%">
                                                            <tr>
                                                                <td style="width: 30%">
                                                                    <h2>
                                                                        <asp:Label ID="lbl_service" runat="server" Text="Servicio"></asp:Label></h2>
                                                                </td>
                                                                <td style="width: 15%">
                                                                    <h2>
                                                                        <asp:Label ID="lbl_dias" runat="server" Text="Habitaciones"></asp:Label></h2>
                                                                </td>
                                                                <td style="width: 15%">
                                                                    <h2>
                                                                        <asp:Label ID="lbl_montoventa" runat="server" Text="Monto Venta"></asp:Label></h2>
                                                                </td>
                                                                <td style="width: 15%">
                                                                    <h2>
                                                                        <asp:Label ID="lbl_extras" runat="server" Text="Monto Noches Adicionales"></asp:Label></h2>
                                                                </td>
                                                                <td style="width: 15%">
                                                                    <h2>
                                                                        <asp:Label ID="lbl_descuento" runat="server" Text="Descuento"></asp:Label></h2>
                                                                </td>
                                                                <td style="width: 15%">
                                                                    <h2>
                                                                        <asp:Label ID="lbl_subTotal" runat="server" Text="SubTotal"></asp:Label></h2>
                                                                </td>
                                                            </tr>
                                                        </table> --%>
                                                        <asp:DataList ID="dtl_shoppingCart" runat="server" RepeatDirection="Horizontal" Width="100%"
                                                            RepeatColumns="1">
                                                            <%--<ItemStyle CssClass="bordeAbajoShopping" />--%>
                                                            <ItemTemplate>
                                                                <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                                    <tr>
                                                                        <td class="alignLeft" style="width: 30%; padding-left: 12px;">
                                                                            <asp:Label ID="lbl_producto" runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                        <td class="alignLeft" style="padding-left: 12px; width: 10%;">
                                                                            <asp:Label ID="lbl_cantidad" runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                        <td class="width15porciento alignLeft" style="padding-left: 12px; width: 15%;">
                                                                            <asp:Label ID="lbl_montoventa" runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                        <td class="alignLeft" style="width: 20%; padding-left: 12px;">
                                                                            <asp:Label ID="lbl_extras" runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                        <td class="alignLeft" style="width: 13%; padding-left: 12px;">
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
                                                                        <asp:Label ID="lbl_service" runat="server" Text="Servicio"></asp:Label></h2>
                                                                </td>
                                                                
                                                                        <td class="celda-valor-tabla-shoppingcart alignLeft" style="width: 30%; padding-left: 12px;">
                                                                            <asp:Label ID="lbl_producto_responsivo" runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="celda-titulo-tabla-shoppingcart" style="width: 10%">
                                                                    <h2 class="titulo-tabla-shoppincart">
                                                                        <asp:Label ID="lbl_dias" runat="server" Text="Hab."></asp:Label></h2>
                                                                </td>
                                                                
                                                                        <td class="alignLeft celda-valor-tabla-shoppingcart" style="padding-left: 12px; width: 10%;">
                                                                            <asp:Label ID="lbl_cantidad_responsivo" runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="celda-titulo-tabla-shoppingcart" style="width: 15%">
                                                                    <h2 class="titulo-tabla-shoppincart">
                                                                        <asp:Label ID="lbl_montoventa" runat="server" Text="Monto Venta"></asp:Label></h2>
                                                                </td>
                                                                        <td class="width15porciento alignLeft celda-valor-tabla-shoppingcart" style="padding-left: 12px; width: 15%;">
                                                                            <asp:Label ID="lbl_montoventa_responsivo" runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="celda-titulo-tabla-shoppingcart" style="width: 20%">
                                                                        <h2 class="titulo-tabla-shoppincart">
                                                                            <asp:Label ID="lbl_extras" runat="server" Text="Monto Noche(s) Adicionales"></asp:Label></h2>
                                                                    </td>
                                                                        <td class="alignLeft celda-valor-tabla-shoppingcart" style="width: 20%; padding-left: 12px;">
                                                                            <asp:Label ID="lbl_extras_responsivo" runat="server" Text=""></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td class="celda-titulo-tabla-shoppingcart" style="width: 13%">
                                                                        <h2 class="titulo-tabla-shoppincart">
                                                                            <asp:Label ID="lbl_descuento" runat="server" Text="Descuento"></asp:Label></h2>
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
                                                        <br />
                                                        <table class="tableShopping2 desktop" cellpadding="0" cellspacing="0" border="0" width="100%">
                                                            <tr class="displayNone">
                                                                <td class="width30porciento alignLeft">&nbsp;
                                                                    
                                                                </td>
                                                                <td class="width15porciento">&nbsp;
                                                                    
                                                                </td>
                                                                <td class="width20porciento">&nbsp;
                                                                    
                                                                </td>
                                                                <td class="width20porciento">
                                                                    <asp:Label ID="lbl_taxes" runat="server" Text="Impuestos:"></asp:Label>
                                                                </td>
                                                                <td class="width15porciento">
                                                                    <asp:Label ID="lbl_taxesT" runat="server" Text="0"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                            <%--Cambios AÃÂÃÂ±adidos por Keylor para mayo2013: tabla original comentada --%>
                                                                <td style="width: 30%" class="alignLeft">&nbsp;
                                                                    
                                                                </td>
                                                                <td style="width: 10%" class="alignLeft">&nbsp;
                                                                    
                                                                </td>
                                                                <td style="width: 18%">&nbsp;
                                                                    
                                                                </td>
                                                                <td style="width: 18%">&nbsp;
                                                                    
                                                                </td>
                                                                <td class="total-lbl-checkout" style="width: 12%">
                                                                    <asp:Label ID="lbl_MontoTot" runat="server" Text="Total"></asp:Label>
                                                                </td>
                                                                <td class="total-checkout" style="width: 17%">
                                                                    <asp:Label ID="lbl_monto" runat="server" Text="$"></asp:Label>
                                                                </td>
                                                                <%-- <td class="width30porciento alignLeft">
                                                                    &nbsp;
                                                                </td>
                                                                <td class="width15porciento">
                                                                    &nbsp;
                                                                </td>
                                                                <td class="width20porciento">
                                                                    &nbsp;
                                                                </td>
                                                                <td class="width20porciento">
                                                                    <asp:Label ID="lbl_MontoTot" runat="server" Text="Monto Total:"></asp:Label>
                                                                </td>
                                                                <td class="width15porciento">
                                                                    <asp:Label ID="lbl_monto" runat="server" Text="$"></asp:Label>
                                                                </td>--%>
                                                            </tr>
                                                        </table>

                                                        <table class="tableShopping2 movil" cellpadding="0" cellspacing="0" border="0" width="100%">
                                                            
                                                            <tr>
                                                            <%--Cambios AÃÂÃÂ±adidos por Keylor para mayo2013: tabla original comentada --%>
                                                                
                                                                <td class="total-lbl-checkout" style="width: 40%;">
                                                                    <asp:Label ID="lbl_total_responsivo" runat="server" Text="Total:"></asp:Label>
                                                                </td>
                                                                <td class="total-checkout" style="width: 60%;">
                                                                    <asp:Label ID="lbl_valor_total_responsivo" runat="server" Text="$"></asp:Label>
                                                                </td>
                                                                
                                                            </tr>
                                                        </table>
                                                        <div class="botones-pago-cancel-abajo">
                                                                    <div class="derecha displayNone">
                                                                        <asp:Label ID="lbl_termino1" runat="server" Text="Haga click"></asp:Label>
                                                                        <asp:LinkButton ID="lnkbtn_terminos" runat="server" Text="aquÃÂÃÂÃÂÃÂ­"></asp:LinkButton>
                                                                        <asp:Label ID="lbl_termino2" runat="server" Text="para leer y confirmar nuestros "></asp:Label>
                                                                        <asp:LinkButton ID="lnkbtn_terminos2" runat="server" Text="Terminos y Condiciones"></asp:LinkButton>
                                                                        <asp:Label ID="lbl_termino3" runat="server" Text="para continuar"></asp:Label>
                                                                    </div>
                                                                    <br />
                                                                    <div class="derecha">
                                                                    <%-- Cambios de Keylor para mayo 2013: se cambio el estilo de los botones, original esta comentado --%>
                                                                    
                                                                        <asp:ImageButton ID="btn_cancelar" runat="server" style="float:left" CssClass="marginRight15" ImageUrl="~/images/2014/img-btn-cancel-sp.png" />
                                                                        <asp:ImageButton ID="btn_pagar" runat="server" ImageUrl="~/images/2014/img-btn-make-your-payment-sp.png" />
                                                                    
                                                                        <%--<asp:ImageButton ID="btn_cancelar" runat="server" CssClass="marginRight15" ImageUrl="~/images/Otros/cancelar.gif" />
                                                                        <asp:ImageButton ID="btn_pagar" runat="server" ImageUrl="~/images/Otros/reservar.gif" />
                                                                         --%>
                                                                    </div>
                                                        </div>        
                                                    </asp:Panel>
                                                    <center>
                                                        <asp:Label ID="lbl_mensaje" runat="server" Visible="false"> 
                                                        Su carrito de compras esta vacÃÂÃÂÃÂÃÂ­o
                                                        </asp:Label>
                                                    </center>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <asp:Label ID="lbl_term" runat="server" Visible="false"></asp:Label>
                                        
                                <%-- <div class="fondoRosado">
                                    Sorpr&eacute;ndete con nuestro maravilloso Tour Virtual
                                </div>
                                <a href="tourPiscinaEspa.html">
                                    <img src="images/images2/Otros/img1_2.gif" alt="" /></a><a href="tourHabitacionEspa.html"><img
                                        src="images/images2/Otros/img2_2.gif" alt="" /></a><a href="tourJardinesEspa.html"><img
                                            src="images/images2/Otros/img3_2.gif" alt="" /></a><a href="tourDeckEspa.html"><img
                                                src="images/images2/Otros/img4_2.gif" alt="" /></a>
                                <br />
                                <br />--%>


                                </div>
                        </div>
                   </div>
                   </div>
            <div id="prefooter">
                <div id="prefooter-inner" class="container-980">
                    <div class="botones-prefooter">
                        <div class="btn-contact-us">
                            <a href="http://manatuscostarica.com/es/contacto">Cont&aacute;ctenos</a>
                        </div>
                        <div class="btn-chat">
                            <a href="javascript:%20$zopim.livechat.window.show();">Hable con nosotros</a>
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
					        <li><a href="http://manatuscostarica.com/es/quÃÂÃÂ©-hacer">&iquest;Qu&eacute; hacer?</a>
                        
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
                            <li><a href="http://manatuscostarica.com/es/paquetes-tarifas">Paquetes &amp; Tarifas</a></li>
                            <li><a href="http://manatuscostarica.com/es/blog">Blog</a></li>
                            <li><a href="http://manatuscostarica.com/es/gallery">Galeria</a></li>
                        </ul>
                    </div>
                    <div class="column3 column">
                        <ul>
                            <li><a href="http://manatuscostarica.com/es/virtual-tours/jardines">Tours Virtuales</a></li>
                            <li><a href="http://manatuscostarica.com/es/quÃÂÃÂ©-hacer">&iquest;Qu&eacute; hacer?</a></li>
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
