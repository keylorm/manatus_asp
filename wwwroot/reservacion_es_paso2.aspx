<%@ Page Language="VB" AutoEventWireup="false" CodeFile="reservacion_es_paso2.aspx.vb" Inherits="reservacion_es_paso2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
    <link href="styles/stylereservations_nuevo.css" rel="stylesheet" type="text/css" />
    <!--[if gte IE 9]><!-->
    <link rel="stylesheet" href="styles/stylereservation_nuevo_responsivo.css" type="text/css" />
    <!--<![endif]-->

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
    <script type="text/javascript" src="http://booking.manatuscostarica.com/js/myJS.js"></script>
    <script type="text/javascript" src="http://booking.manatuscostarica.com/js/tinynav.min.js"></script>

    <style type="text/css">
        /*Codigo de css para el pop up*/ #fade {
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


        .popup_block {
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

        img.btn_close {
            float: right;
            margin: -55px -55px 0 0;
        }
        /*--Making IE6 Understand Fixed Positioning--*/ *html #fade {
            position: absolute;
        }

        *html .popup_block {
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

    <!-- client validations -->
    <script type = "text/javascript">
        function CustomValidatorTerminosCondiciones(sender, args) {
            if (document.getElementById("<%=chkTerminosCondiciones.ClientID%>").checked == true) {
                args.IsValid = true;
            } else {
                args.IsValid = false;
            }
        }
    </script>
    <!-- end client validations -->

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
        _gaq.push(['_trackEvent', 'Reservación', 'Paso 2', urlactual]);
    </script>

</head>
<body class="reservation-form en">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div id="loader" runat="server" visible="false">
            <img src="images/ajax-loader.gif" />
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
                            <li><a href="reservacion_en.aspx" class="active">English</a></li>
                            |<li><a href="reservacion_sp.aspx">Español</a></li>
                        </ul>
                    </div>
                    <div class="menu-top">
                        <ul>
                            <li><a href="http://manatuscostarica.com/">Home</a></li>
                            |<li><a href="http://booking.manatuscostarica.com/reservacion_en.aspx">Book Now</a></li>
                            |<li><a href="http://manatuscostarica.com/gallery">Gallery</a></li>
                            |<li>
                                <a href="http://manatuscostarica.com/contact-us">Contact</a></li>
                        </ul>
                    </div>
                    <div class="contact-info">
                        <p>
                            MANATUS HOTEL, TORTUGUERO COSTA RICA | RESERVATIONS: <a href="tel: +50622397364">(506)
                            2239.7364</a> | HOTEL: <a href="tel: +50627098197">(506) 2709.8197</a>
                        </p>
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
                            <li><a href="http://manatuscostarica.com/blog">Blog</a></li>
                        </ul>
                        <ul class="mi-menu-responsivo">
                            <li><a href="http://manatuscostarica.com/manatus-experience">Manatus Experience</a>
                            </li>
                            <li><a href="http://manatuscostarica.com/what-to-do">What to do?</a> </li>
                            <li><a href="http://manatuscostarica.com/package-and-rates">Packages &amp; Rates</a>
                            </li>
                            <li><a href="http://manatuscostarica.com/tortuguero-area">Tortuguero</a></li>
                            <li class="active-trail"><a href="http://booking.manatuscostarica.com/reservacion_en.aspx">Reservations</a></li>
                            <li><a href="http://manatuscostarica.com/blog">Blog</a></li>
                        </ul>
                    </div>
                </div>
                <div id="page-head">
                    <div class="page-title">
                        <h1 title="On-line reservations, booking at Manatus, tortuguero hotel reservations"
                            class="h1ReservaNew">RESERVATION</h1>
                    </div>
                    <div class="breadcrumbs">
                        <a href="http://manatuscostarica.com/" title="P&aacute;gina principal Hotel Manatus Tortuguero Costa Rica">Home</a> / Reservation
                    </div>
                </div>
            </div>
        </div>
        <div id="main-content">
            <div class="content container-980">
                <div class="above-content">
                    <div class="titulo-logos">
                        <div class="nombre-hotel">
                            <h2>Hotel Manatus</h2>
                        </div>
                        <div class="logos-premios">
                            <a href="http://www.tripadvisor.com.mx/Hotel_Review-g309268-d308651-Reviews-Manatus_Hotel-Tortuguero_Province_of_Limon.html">
                                <img src="images/logo-trip-1.jpg" /></a>
                            <a href="http://www.tripadvisor.com.mx/Hotel_Review-g309268-d308651-Reviews-Manatus_Hotel-Tortuguero_Province_of_Limon.html">
                                <img src="images/logo-trip-2.jpg" /></a>
                            <img src="images/logo-rainforest.png" />
                            <div id="TA_excellent625" class="TA_excellent">
                                <ul id="zWYVpp9Tm" class="TA_links VlM6UjTeF">
                                    <li id="9QVb1lq" class="mW6emucXI9Tk"><a target="_blank" href="http://www.tripadvisor.es/">
                                        <img src="http://e2.tacdn.com/img2/widget/tripadvisor_logo_115x18.gif" alt="TripAdvisor" class="widEXCIMG" id="CDSWIDEXCLOGO" /></a></li>
                                </ul>
                            </div>
                            <script src="http://www.jscache.com/wejs?wtype=excellent&amp;uniq=625&amp;locationId=308651&amp;lang=es&amp;display_version=2"></script>

                        </div>
                    </div>

                    <div class="step-process-box">
                        <div class="inner">
                            <div class="step-1">
                                <div class="step-title">
                                    <p><a href="reservacion_es_paso1.aspx">Availability</a></p>
                                </div>
                                <div class="step-point">
                                    <a class="point" href="">1</a>
                                </div>
                            </div>
                            <div class="step-2 active">
                                <div class="step-title">
                                    <p><a href="">Personal Information</a></p>
                                </div>
                                <div class="step-point">
                                    <a class="point" href="">2</a>
                                </div>
                            </div>
                            <div class="step-3">
                                <div class="step-title">
                                    <p><a href="">Payment and Confirmation</a></p>
                                </div>
                                <div class="step-point">
                                    <a class="point" href="">3</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>


                <div class="content-box container-2-3">
                    <h2 class="h2Celeste">Favor completar su información personal</h2>
                    <!-- register form -->
                    <asp:UpdatePanel ID="updtpanel_reservacion" runat="server">
                    <ContentTemplate>
                        <asp:Label ID="lbl_paquete" runat="server" Text="" Visible="false"></asp:Label>
                        <div>
                            <div class="fondoGrisReserva">
                                 <div class="paddingReserva">
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
                                            <div class="wrapper-field">
                                                <asp:Label ID="lbl_nombre" runat="server" Text="Nombre completo" CssClass="span-field"></asp:Label>
                                                <asp:TextBox ID="txt_nombre" runat="server" CssClass="textBoxNuevo"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfv_nombre" runat="server" ControlToValidate="txt_nombre" Display="Dynamic" ErrorMessage="Campo requerido" ValidationGroup="registrese"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="wrapper-field">
                                                <asp:Label ID="lbl_dir" runat="server" Text="Dirección" CssClass="span-field"></asp:Label>
                                                <asp:TextBox ID="txt_direccion" runat="server" CssClass="textBoxNuevo"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfv_direccion" runat="server" ControlToValidate="txt_direccion" Display="Dynamic" ErrorMessage="Campo requerido" ValidationGroup="registrese"></asp:RequiredFieldValidator>
                                            </div>
                                            <div class="box-ubicacion wrapper-field">
                                                <asp:Label ID="lbl_ubicacion" runat="server" Text="País" CssClass="span-field"></asp:Label>
                                                <asp:DropDownList ID="ddl_ubicacion" runat="server" CssClass="dropdownsReserva"></asp:DropDownList>
                                            </div>
                                            <!--<div>
                                                <asp:Label ID="lbl_codigo" runat="server" Text="Código Postal:"></asp:Label>
                                                <asp:TextBox ID="txt_codPostal" runat="server" CssClass="textBoxNuevo"></asp:TextBox>
                                            </div>-->    
                                            <div class="cod-telefono">                                        
                                                <div class="wrapper-field">
                                                    <asp:Label ID="lbl_codigoA" runat="server" Text="Código de Area" CssClass="span-field"></asp:Label>
                                                    <asp:TextBox ID="txt_codArea" runat="server" CssClass="textBoxNuevo"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfv_codigoA" runat="server" ControlToValidate="txt_codArea" Display="Dynamic" ErrorMessage="Campo requerido" ValidationGroup="registrese"></asp:RequiredFieldValidator>
                                                </div>                                            
                                                <div class="wrapper-field">
                                                    <asp:Label ID="lbl_telefono" runat="server" Text="Teléfono" CssClass="span-field"></asp:Label>
                                                    <asp:TextBox ID="txt_tel" runat="server" CssClass="textBoxNuevo"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfv_Tel" runat="server" ControlToValidate="txt_tel" Display="Dynamic" ErrorMessage="Campo requerido" ValidationGroup="registrese"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                            <div class="wrapper-field">
                                                <asp:Label ID="lbl_email" runat="server" Text="Email" CssClass="span-field"></asp:Label>
                                                <asp:TextBox ID="txt_email" runat="server" CssClass="textBoxNuevo"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfv_email" runat="server" ControlToValidate="txt_email" Display="Dynamic" ErrorMessage="Campo Requerido" ValidationGroup="registrese"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="rev_email" runat="server" ControlToValidate="txt_email" Display="Dynamic" ErrorMessage="Formato incorrecto" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="registrese"></asp:RegularExpressionValidator>
                                            </div>                                            
                                            <div class="wrapper-field">
                                                <asp:Label ID="lbl_mensajeAdicional" runat="server" Text="Mensaje Adicional" CssClass="span-field"></asp:Label>
                                                <asp:TextBox TextMode="MultiLine" runat="server" CssClass="textMensajeAdicional" ID="txtMensajeAdicional"></asp:TextBox>
                                            </div>
                                            <div id="terminos-condiciones">
                                                <asp:HyperLink NavigateUrl="javascript:void(0)" runat="server" text="Ver Términos y Condiciones" ID="linkTerminosCondiciones" />
                                            </div>
                                            <div class="wrapper-field">
                                                <asp:CheckBox Text="He leído y acepto los Términos y Condiciones" runat="server" ID="chkTerminosCondiciones"/>
                                                <asp:CustomValidator ID="val_terminosCondiciones" runat="server" ErrorMessage="Favor aceptar los términos y condiciones" ClientValidationFunction="CustomValidatorTerminosCondiciones" ValidationGroup="registrese" ></asp:CustomValidator>
                                                <%--<asp:RequiredFieldValidator ID="rev_terminosCondiciones" runat="server" ControlToValidate="chkTerminosCondiciones" Display="Dynamic" ErrorMessage="Campo requerido" ValidationGroup="registrese"></asp:RequiredFieldValidator>--%>
                                            </div>
                                            <!-- mensajes -->
                                            <asp:Label ID="lbl_ResultadoReservacion" runat="server" Text=""></asp:Label>
                                        </asp:Panel>
                                     </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>

                <div class="alignLeft btn_toStep3">
                    <asp:LinkButton ID="btn_aPaso3" runat="server" ToolTip="Reservar" ValidationGroup="registrese" ForeColor="#FFFFFF" Visible="true">ENVIAR INFORMACIÓN Y SEGUIR AL PASO 3: DATOS DE PAGO</asp:LinkButton>
                </div>
                    <!-- end register form -->
                </div>


                <div class="content-box container-1-3 sidebar">
                    <div class="logo-ssl">
                        <img src="images/verisign.png" />
                    </div>


                    <!-- sidebar with the information previous step -->
                    <!-- end sidebar with the information previous step -->
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
                    <p>
                        Manatus Hotel<br />
                        Tortuguero Costa Rica<br />
                        Tel: <a href="tel:+50627098197">(506) 2709.8197</a>
                    </p>
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
                        <ul>
                            <li><a href="http://manatuscostarica.com/">Home</a></li>
                            |<li><a href="http://booking.manatuscostarica.com/reservacion_en.aspx">Book Now</a></li>
                            |<li><a href="http://manatuscostarica.com/gallery">Gallery</a></li>
                            |<li>
                                <a href="http://manatuscostarica.com/contact-us">Contact</a></li>
                        </ul>
                    </div>
                    <div class="contact-info">
                        <p>
                            MANATUS HOTEL, TORTUGUERO COSTA RICA | TEL: <a href="tel: +50627098197">(506) 2709.8197</a>
                        </p>
                    </div>
                    <div class="menu-idioma">
                        <ul>
                            <li><a href="reservacion_en.aspx" class="active">English</a></li>
                            |<li><a href="reservacion_sp.aspx">Español</a></li>
                        </ul>
                    </div>
                </div>


            </div>
        </div>

    </form>

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