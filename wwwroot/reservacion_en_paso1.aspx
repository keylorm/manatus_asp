<%@ Page Language="VB" AutoEventWireup="false" CodeFile="reservacion_en_paso1.aspx.vb"
    Inherits="reservacion_en_paso1" %>

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
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.0/jquery.min.js"></script>
    <script type="text/javascript" src="http://booking.manatuscostarica.com/js/myJS.js"></script>
    <script type="text/javascript" src="http://booking.manatuscostarica.com/js/tinynav.min.js"></script>
    <!-- scripts range datepicker -->
    <script type="text/javascript" src="datepicker/js/datepicker.js"></script>
    <script type="text/javascript" src="datepicker/js/eye.js"></script>
    <script type="text/javascript" src="datepicker/js/utils.js"></script>
    <script type="text/javascript" src="datepicker/js/layout.js"></script>
    <script type="text/javascript" src="Scripts/global.js"></script>
    <!-- end scripts range datepicker -->
    <script type="text/javascript" src="http://booking.manatuscostarica.com/js/tinynav.min.js"></script>
    <!-- style range datepicker -->
    <link type="text/css" rel="stylesheet" href="datepicker/css/datepicker.css" />
    <!--<link type="text/css" rel="stylesheet" href="datepicker/css/layout.css" />-->
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
</head>
<body class="reservation-form en">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div id="loader" runat="server" class="loader">
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
                        <li><a href="reservacion_en.aspx" class="active">English</a></li>|<li><a href="reservacion_sp.aspx">
                            Español</a></li></ul>
                </div>
                <div class="menu-top">
                    <ul>
                        <li><a href="http://manatuscostarica.com/">Home</a></li>|<li><a href="http://booking.manatuscostarica.com/reservacion_en.aspx">
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
                        <li class="active-trail"><a href="http://booking.manatuscostarica.com/reservacion_en.aspx">
                            Reservations</a></li>
                        <li><a href="http://manatuscostarica.com/blog">Blog</a></li></ul>
                    <ul class="mi-menu-responsivo">
                        <li><a href="http://manatuscostarica.com/manatus-experience">Manatus Experience</a>
                        </li>
                        <li><a href="http://manatuscostarica.com/what-to-do">What to do?</a> </li>
                        <li><a href="http://manatuscostarica.com/package-and-rates">Packages &amp; Rates</a>
                        </li>
                        <li><a href="http://manatuscostarica.com/tortuguero-area">Tortuguero</a></li>
                        <li class="active-trail"><a href="http://booking.manatuscostarica.com/reservacion_en.aspx">
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
                <div class="step-process-box">
                    <asp:UpdatePanel ID="up_step_process" runat="server" UpdateMode="Always">
                        <ContentTemplate>
                            <div class="inner">
                                <div class="step-1 active step">
                                    <div class="step-title">
                                        <p>
                                            <asp:LinkButton ID="step_1_link" runat="server">Availability</asp:LinkButton></p>
                                    </div>
                                    <div class="step-point">
                                        <a class="point" href="">1</a>
                                    </div>
                                </div>
                                <div class="step-2 step">
                                    <div class="step-title">
                                        <p>
                                            <asp:LinkButton ID="step_2_link" runat="server">Personal Information</asp:LinkButton></p>
                                    </div>
                                    <div class="step-point">
                                        <a class="point" href="">2</a>
                                    </div>
                                </div>
                                <div class="step-3 step">
                                    <div class="step-title">
                                        <p>
                                            <asp:LinkButton ID="step_3_link" runat="server">ayment and Confirmation</asp:LinkButton></p>
                                    </div>
                                    <div class="step-point">
                                        <a class="point" href="">3</a>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div id="widgetCalendar" class="hidden">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <asp:LinkButton ID="AplicarSeleccion" runat="server" Text="Apply selection"></asp:LinkButton>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <asp:UpdatePanel ID="up_paso1" runat="server" UpdateMode="Always">
                <ContentTemplate>
                    <div id="paso1" runat="server" visible="true">
                        <div class="form-title">
                            <h2 class="h2Celeste">
                                <strong>Live the Manatus</strong> Experience</h2>
                            <div class="logo-ssl">
                                <img src="" /></div>
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
                                                        <!-- rango de fecha -->
                                                        <!-- fin rango de fecha -->
                                                        <asp:UpdatePanel ID="updatePane_fecha" runat="server" UpdateMode="Always">
                                                            <ContentTemplate>
                                                                <div class="fecha-field">
                                                                    <div id="widget">
                                                                        <div id="widgetField">
                                                                            <asp:Label ID="lblIngresoSalida" runat="server" Text="Arrival and departure"></asp:Label>
                                                                            <asp:TextBox runat="server" ID="TxtCheckinCheckout" AutoPostBack="true" name="checkin-checkout"
                                                                                value='Select range'></asp:TextBox>
                                                                            <asp:LinkButton ID="book_now_1" CssClass="btn" runat="server">Book Now</asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                    <hr />
                                                    <div class="contenedor-hab-personas">
                                                        <h3>
                                                            Package for your vacations in Costa Rica</h3>
                                                        <hr />
                                                        <asp:UpdatePanel ID="updatePane_habitaciones" runat="server" UpdateMode="Always">
                                                            <ContentTemplate>
                                                                <asp:Panel ID="pnl_resultados" runat="server">
                                                                    <asp:GridView ID="gv_ResultadosDisponibles" runat="server" ShowHeader="False" AutoGenerateColumns="False"
                                                                        GridLines="None" Width="246px" EnableModelValidation="True">
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
                                                                                        Font-Italic="true" ForeColor="#999999" Text="1 room"></asp:Label>
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
                                                                    <asp:LinkButton ID="add_room" runat="server">+ Add Room</asp:LinkButton>
                                                                </asp:Panel>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                    <asp:Label ID="lbl_ResultadoHabitaciones" runat="server" Text=""></asp:Label>
                                                    <div class="desc-paquete">
                                                        <asp:UpdatePanel ID="updatePane_detalleprecio" runat="server" UpdateMode="Always">
                                                            <ContentTemplate>
                                                                <div class="desc-paquete-inner">
                                                                    <div class="box-precio-sin-transporte">
                                                                        <asp:LinkButton ID="btn_reservar1" runat="server" ToolTip="Book now" ValidationGroup="registrese"
                                                                            ForeColor="#FFFFFF" Visible="false">BOOK NOW »</asp:LinkButton>
                                                                        <div class="precio-sin-transporte">
                                                                            <div class="precio-sin-transporte-value">
                                                                                $
                                                                                <asp:Label ID="lbl_precioSinTransporte" runat="server" Text="0"></asp:Label>
                                                                            </div>
                                                                            <div class="precio-sin-transporte-letra-pequena">
                                                                                <p>
                                                                                    * Includes: taxes, footd and tours on Tortuguero Channels
                                                                                </p>
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
                                                                    <asp:Label ID="lbl_erroFechas" runat="server" Text="" ForeColor="red"></asp:Label>
                                                                    <asp:Label ID="lbl_ResultadoReservacion" runat="server" Text=""></asp:Label>
                                                                </div>
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </div>
                                                    <hr />
                                                    <div id="traslado-box">
                                                        <asp:UpdatePanel ID="updatePane_transporte" runat="server" UpdateMode="Always">
                                                            <ContentTemplate>
                                                                <h3>
                                                                    Do you need <strong>transport</strong> from and to Manatus?</h3>
                                                                <div class="traslado-box-inner">
                                                                    <p>
                                                                        Select the transfer options that you require and add it to your package:</p>
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
                                                                                <div class="preciot">
                                                                                    $
                                                                                    <asp:Label ID="lbl_precioConTransporte" runat="server" Text="0"></asp:Label></div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <div class="content_btn">
                                                                        <asp:LinkButton ID="book_now_2" CssClass="btn" runat="server">Book Now</asp:LinkButton></div>
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
                            <div id="tabs">
                                <ul>
                                    <li><a href="#description">Package description</a></li>
                                    <li><a href="#included">Included</a></li>
                                    <li><a href="#rates">Rates</a></li>
                                </ul>
                                <div id="description" class="tbcontent">
                                    <div class="des_paq_2_1" id="des_paq_2_1" runat="server">
                                        <h4>
                                            Tortuguero by Boat:</h4>
                                        <p>
                                            <span class="titulo-dia">Day 1</span><br>
                                            Pick up at different hotels located in San Jose´s Metropolitan Area<br>
                                            8:00 a.m. Breakfast at Rancho Roberto’s Restaurant (located in Guapiles)<br>
                                            11:00 a.m. Arrival to Caño Blanco, in order to take a boat transfer (Caño Blanco/
                                            Manatus Hotel)<br>
                                            12:30 p.m. Welcome Cocktail at the Manatus Hotel<br>
                                            Hotel Check-in<br>
                                            Mid-day to 1:30 p.m. Lunch at the Manatus Hotel “Ara Macaw” Restaurant<br>
                                            2:30 p.m. Tour to the Tortuguero Canals and then a Village Tour. This tour is included
                                            in the package on the first day in the afternoon<br>
                                            6:30 p.m. to 8:30 pm Dinner at the Manatus Hotel “Ara Macaw” Restaurant</p>
                                        <p>
                                            <span class="titulo-dia">Day 2</span><br>
                                            Between 7:00 a.m. to 8:30 a.m. Breakfast at Manatus Hotel “Ara Macaw” Restaurant<br>
                                            9:00 a.m. Check out.<br>
                                            9:30 a.m. Leaving the Hotel.<br>
                                            11:00 a.m. Arriving to Caño Blanco.<br>
                                            1:00 p.m. Lunch at Rancho Roberto’s Restaurant (Located in Guapiles)<br>
                                            3:30 p.m. Arriving to San Jose</p>
                                        <h4>
                                            Tortuguero by Air</h4>
                                        <p>
                                            <span class="titulo-dia">Day 1</span><br>
                                            Pick up at Tortuguero Air Track at 7:00 a.m. or at the time that the guest set.<br>
                                            7:00 a.m. to 8:30 a.m. Breakfast at the Manatus Hotel “Ara Macaw” Restaurant<br>
                                            8:30 a.m Tortuguero Canals Tour<br>
                                            Mid-day to 1:30 p.m. Lunch at the Manatus Hotel “Ara Macaw” Restaurant<br>
                                            3:00 p.m. Downtown Tortuguero Tour.<br>
                                            6:30 p.m. to 8:30 p.m. Dinner at the Manatus Hotel “Ara Macaw” Restaurant.</p>
                                        <p>
                                            <span class="titulo-dia">Day 2</span><br>
                                            6:00 a.m. Light Breakfast.<br>
                                            6:20 a.m. Check out.<br>
                                            6:30 a.m. Drop off at Tortuguero Track.</p>
                                    </div>
                                    <div class="des_paq_3_2"  id="des_paq_3_2" runat="server">
                                        <h4>
                                            Tortuguero by Boat:</h4>
                                        <p>
                                            <span class="titulo-dia">Day 1</span><br>
                                            Pick up at San Jose Metropolitan Area at the hotel.<br>
                                            7:45 a.m to 8:30 a.m Breakfast at Nava Café Restaurant (located in Guapiles)<br>
                                            11:00 a.m Arriving to Caño Blanco, to take a boat transfer (Caño Blanco/ Manatus
                                            Hotel)<br>
                                            12:30 pm Welcome Cocktail at Manatus Hotel<br>
                                            Hotel Check-in<br>
                                            Mid-day to 1:30 p.m. Lunch at the Manatus Hotel “Ara Macaw” Restaurant<br>
                                            3:00 p.m. Visit to downtown Tortuguero.<br>
                                            6:30 p.m. to 8:30 p.m. Dinner at the Manatus Hotel “Ara Macaw” Restaurant.</p>
                                        <p>
                                            <span class="titulo-dia">Day 2</span><br>
                                            7:00 a.m. to 8:30 a.m. Breakfast at the Manatus Hotel “Ara Macaw” Restaurant<br>
                                            8:30 a.m. to 10:30 a.m. Tortuguero Canals Tour in Tortuguero National Park (We visit
                                            only one canal at this time)<br>
                                            Mid-day to 1:30 p.m. Lunch at the Manatus Hotel “Ara Macaw” Restaurant<br>
                                            2:30 p.m. to 4:30 p.m. Canals Tour in Tortuguero National Park (We visit only one
                                            canal at this time)<br>
                                            6:30 p.m. to 8:30p.m. Dinner at the Manatus Hotel “Ara Macaw” Restaurant</p>
                                        <p>
                                            <strong>Note:</strong><br>
                                            Only two tours are included in this day.<br>
                                            We visit different canals at the two tours offered.</p>
                                        <p>
                                            <span class="titulo-dia">Day 3</span><br>
                                            7:45 a.m. to 8:30 am Breakfast at the Manatus Hotel “Ara Macaw” Restaurant<br>
                                            9:00 a.m Check out.<br>
                                            9:30 a.m Leaving the Hotel<br>
                                            11:00 a.m Arriving to Caño Blanco<br>
                                            1:00 pm Lunch at Nava Café Restaurant (located in Guapiles)<br>
                                            3:30 pm Arriving to San Jose</p>
                                        <p>
                                            <strong>Note:</strong><br>
                                            We can drop off in route to San José
                                        </p>
                                    </div>
                                    <div class="des_paq_custom"   id="des_paq_custom" runat="server">
                                        If you want to stay more time with us, you can customize your package. It will include
                                        all meals and the Water Canal Tour.
                                    </div>
                                </div>
                                <div id="included" class="tbcontent">
                                    <div class="inc_paq_2_1" id="inc_paq_2_1" runat="server">
                                        <h4>
                                            2015</h4>
                                        <p>
                                            The package Includes:</p>
                                        <ul>
                                            <li>Lodging, all meals. Beverages are not included.</li>
                                            <li>2 days 1 night package includes two excursions with guide: one tour to the town
                                                and one tour to the water channels. Entrance not included</li>
                                            <li>3 days 2 nights package includes three excursions with guide: one tour to the town
                                                and two tours to the water channels. Entrance not included</li>
                                            <li>All taxes</li>
                                        </ul>
                                        <p>
                                            Not included on the package:</p>
                                        <ul>
                                            <li>Ground Transfer: San José - (Pavona / Caño Blanco) - San José</li>
                                            <li>Boat transfer: (Pavona / Caño Blanco) - Tortuguero - (Pavona / Caño Blanco)</li>
                                            <li>Breakfast and lunch on route.</li>
                                            <li>The transfer will cost $75 each way. In the way SJ-Tortuguero breakfast will be
                                                provided on route. In the way Tortuguero-SJ lunch will be provided on route.</li>
                                            <li>Alcoholic beverages, sodas, bottled water.</li>
                                            <li>Entrance to the Museum of the Turtle USD $2 per pax (Subject to change)</li>
                                            <li>Entrance to the Tortuguero National Park USD $15 per pax (Subject to change). From
                                                August 1, 2014 the price will be $ 15 + tax for adults and $ 5 + tax for children.</li>
                                            <li>Turtle nesting night tour (Jul-Oct) USD $35 per pax (Subject to change).</li>
                                        </ul>
                                        <p>
                                            General Conditions</p>
                                        <ul>
                                            <li>Rates per person in US dollars.</li>
                                            <li>Includes food and transportation taxes from San José.</li>
                                            <li>10kg = 25 Lbs., maximum luggage weight per person.</li>
                                            <li>Check in at 12:00 MD - Check out: 09:00 am.</li>
                                            <li>Maximum room capacity is 4 people.</li>
                                            <li>Maximum capacity per room has a 4 people limit.</li>
                                            <li>We do not admit groups greater than 6 rooms - only upon request</li>
                                            <li>No Pets admitted - of any kind</li>
                                            <li>We reserved the right to charge a fee for the dinners or celebrations of Christmas
                                                and New Year, which will not be of obligatory purchase</li>
                                        </ul>
                                        <h4>
                                            2016</h4>
                                        <p>
                                            The package Includes:</p>
                                        <ul>
                                            <li>Lodging, all meals. Beverages are not included.</li>
                                            <li>2 days 1 night package includes two excursions with guide: one tour to the town
                                                and one tour to the water channels. Entrance not included</li>
                                            <li>3 days 2 nights package includes three excursions with guide: one tour to the town
                                                and two tours to the water channels. Entrance not included</li>
                                            <li>All taxes</li>
                                        </ul>
                                        <p>
                                            Not included on the package:</p>
                                        <ul>
                                            <li>Ground Transfer: San José - (Pavona / Caño Blanco) - San José</li>
                                            <li>Boat transfer: (Pavona / Caño Blanco) - Tortuguero - (Pavona / Caño Blanco)</li>
                                            <li>Breakfast and lunch on route.</li>
                                            <li>The transfer will cost $75 each way. In the way SJ-Tortuguero breakfast will be
                                                provided on route. In the way Tortuguero-SJ lunch will be provided on route.</li>
                                            <li>Alcoholic beverages, sodas, bottled water.</li>
                                            <li>Entrance to the Museum of the Turtle USD $2 per pax (Subject to change)</li>
                                            <li>Entrance to the Tortuguero National Park USD $15 per pax (Subject to change)</li>
                                            <li>Turtle nesting night tour (Jul-Oct) USD $35 per pax (Subject to change).</li>
                                        </ul>
                                        <p>
                                            General Conditions</p>
                                        <ul>
                                            <li>Rates per person in US dollars.</li>
                                            <li>Includes food and transportation taxes from San José.</li>
                                            <li>10kg = 25 Lbs., maximum luggage weight per person.</li>
                                            <li>Check in at 12:00 MD - Check out: 09:00 am.</li>
                                            <li>Maximum room capacity is 4 people.</li>
                                            <li>Maximum capacity per room has a 4 people limit.</li>
                                            <li>We do not admit groups greater than 6 rooms - only upon request</li>
                                            <li>No Pets admitted - of any kind</li>
                                            <li>We reserved the right to charge a fee for the dinners or celebrations of Christmas
                                                and New Year, which will not be of obligatory purchase</li>
                                        </ul>
                                    </div>
                                    <div class="inc_paq_3_2"  id="inc_paq_3_2" runat="server">
                                        <h4>
                                            2015</h4>
                                        <p>
                                            The package Includes:</p>
                                        <ul>
                                            <li>Lodging, all meals. Beverages are not included.</li>
                                            <li>2 days 1 night package includes two excursions with guide: one tour to the town
                                                and one tour to the water channels. Entrance not included</li>
                                            <li>3 days 2 nights package includes three excursions with guide: one tour to the town
                                                and two tours to the water channels. Entrance not included</li>
                                            <li>All taxes</li>
                                        </ul>
                                        <p>
                                            Not included on the package:</p>
                                        <ul>
                                            <li>Ground Transfer: San José - (Pavona / Caño Blanco) - San José</li>
                                            <li>Boat transfer: (Pavona / Caño Blanco) - Tortuguero - (Pavona / Caño Blanco)</li>
                                            <li>Breakfast and lunch on route.</li>
                                            <li>The transfer will cost $75 each way. In the way SJ-Tortuguero breakfast will be
                                                provided on route. In the way Tortuguero-SJ lunch will be provided on route.</li>
                                            <li>Alcoholic beverages, sodas, bottled water.</li>
                                            <li>Entrance to the Museum of the Turtle USD $2 per pax (Subject to change)</li>
                                            <li>Entrance to the Tortuguero National Park USD $15 per pax (Subject to change). From
                                                August 1, 2014 the price will be $ 15 + tax for adults and $ 5 + tax for children.</li>
                                            <li>Turtle nesting night tour (Jul-Oct) USD $35 per pax (Subject to change).</li>
                                        </ul>
                                        <p>
                                            General Conditions</p>
                                        <ul>
                                            <li>Rates per person in US dollars.</li>
                                            <li>Includes food and transportation taxes from San José.</li>
                                            <li>10kg = 25 Lbs., maximum luggage weight per person.</li>
                                            <li>Check in at 12:00 MD - Check out: 09:00 am.</li>
                                            <li>Maximum room capacity is 4 people.</li>
                                            <li>Maximum capacity per room has a 4 people limit.</li>
                                            <li>We do not admit groups greater than 6 rooms - only upon request</li>
                                            <li>No Pets admitted - of any kind</li>
                                            <li>We reserved the right to charge a fee for the dinners or celebrations of Christmas
                                                and New Year, which will not be of obligatory purchase</li>
                                        </ul>
                                        <h4>
                                            2016</h4>
                                        <p>
                                            The package Includes:</p>
                                        <ul>
                                            <li>Lodging, all meals. Beverages are not included.</li>
                                            <li>2 days 1 night package includes two excursions with guide: one tour to the town
                                                and one tour to the water channels. Entrance not included</li>
                                            <li>3 days 2 nights package includes three excursions with guide: one tour to the town
                                                and two tours to the water channels. Entrance not included</li>
                                            <li>All taxes</li>
                                        </ul>
                                        <p>
                                            Not included on the package:</p>
                                        <ul>
                                            <li>Ground Transfer: San José - (Pavona / Caño Blanco) - San José</li>
                                            <li>Boat transfer: (Pavona / Caño Blanco) - Tortuguero - (Pavona / Caño Blanco)</li>
                                            <li>Breakfast and lunch on route.</li>
                                            <li>The transfer will cost $75 each way. In the way SJ-Tortuguero breakfast will be
                                                provided on route. In the way Tortuguero-SJ lunch will be provided on route.</li>
                                            <li>Alcoholic beverages, sodas, bottled water.</li>
                                            <li>Entrance to the Museum of the Turtle USD $2 per pax (Subject to change)</li>
                                            <li>Entrance to the Tortuguero National Park USD $15 per pax (Subject to change)</li>
                                            <li>Turtle nesting night tour (Jul-Oct) USD $35 per pax (Subject to change).</li>
                                        </ul>
                                        <p>
                                            General Conditions</p>
                                        <ul>
                                            <li>Rates per person in US dollars.</li>
                                            <li>Includes food and transportation taxes from San José.</li>
                                            <li>10kg = 25 Lbs., maximum luggage weight per person.</li>
                                            <li>Check in at 12:00 MD - Check out: 09:00 am.</li>
                                            <li>Maximum room capacity is 4 people.</li>
                                            <li>Maximum capacity per room has a 4 people limit.</li>
                                            <li>We do not admit groups greater than 6 rooms - only upon request</li>
                                            <li>No Pets admitted - of any kind</li>
                                            <li>We reserved the right to charge a fee for the dinners or celebrations of Christmas
                                                and New Year, which will not be of obligatory purchase</li>
                                        </ul>
                                    </div>
                                    <div class="inc_paq_custom" id="inc_paq_custom" runat="server">
                                        <h4>
                                            2015</h4>
                                        <p>
                                            The package Includes:</p>
                                        <ul>
                                            <li>Lodging, all meals. Beverages are not included.</li>
                                            <li>2 days 1 night package includes two excursions with guide: one tour to the town
                                                and one tour to the water channels. Entrance not included</li>
                                            <li>3 days 2 nights package includes three excursions with guide: one tour to the town
                                                and two tours to the water channels. Entrance not included</li>
                                            <li>All taxes</li>
                                        </ul>
                                        <p>
                                            Not included on the package:</p>
                                        <ul>
                                            <li>Ground Transfer: San José - (Pavona / Caño Blanco) - San José</li>
                                            <li>Boat transfer: (Pavona / Caño Blanco) - Tortuguero - (Pavona / Caño Blanco)</li>
                                            <li>Breakfast and lunch on route.</li>
                                            <li>The transfer will cost $75 each way. In the way SJ-Tortuguero breakfast will be
                                                provided on route. In the way Tortuguero-SJ lunch will be provided on route.</li>
                                            <li>Alcoholic beverages, sodas, bottled water.</li>
                                            <li>Entrance to the Museum of the Turtle USD $2 per pax (Subject to change)</li>
                                            <li>Entrance to the Tortuguero National Park USD $15 per pax (Subject to change). From
                                                August 1, 2014 the price will be $ 15 + tax for adults and $ 5 + tax for children.</li>
                                            <li>Turtle nesting night tour (Jul-Oct) USD $35 per pax (Subject to change).</li>
                                        </ul>
                                        <p>
                                            General Conditions</p>
                                        <ul>
                                            <li>Rates per person in US dollars.</li>
                                            <li>Includes food and transportation taxes from San José.</li>
                                            <li>10kg = 25 Lbs., maximum luggage weight per person.</li>
                                            <li>Check in at 12:00 MD - Check out: 09:00 am.</li>
                                            <li>Maximum room capacity is 4 people.</li>
                                            <li>Maximum capacity per room has a 4 people limit.</li>
                                            <li>We do not admit groups greater than 6 rooms - only upon request</li>
                                            <li>No Pets admitted - of any kind</li>
                                            <li>We reserved the right to charge a fee for the dinners or celebrations of Christmas
                                                and New Year, which will not be of obligatory purchase</li>
                                        </ul>
                                        <h4>
                                            2016</h4>
                                        <p>
                                            The package Includes:</p>
                                        <ul>
                                            <li>Lodging, all meals. Beverages are not included.</li>
                                            <li>2 days 1 night package includes two excursions with guide: one tour to the town
                                                and one tour to the water channels. Entrance not included</li>
                                            <li>3 days 2 nights package includes three excursions with guide: one tour to the town
                                                and two tours to the water channels. Entrance not included</li>
                                            <li>All taxes</li>
                                        </ul>
                                        <p>
                                            Not included on the package:</p>
                                        <ul>
                                            <li>Ground Transfer: San José - (Pavona / Caño Blanco) - San José</li>
                                            <li>Boat transfer: (Pavona / Caño Blanco) - Tortuguero - (Pavona / Caño Blanco)</li>
                                            <li>Breakfast and lunch on route.</li>
                                            <li>The transfer will cost $75 each way. In the way SJ-Tortuguero breakfast will be
                                                provided on route. In the way Tortuguero-SJ lunch will be provided on route.</li>
                                            <li>Alcoholic beverages, sodas, bottled water.</li>
                                            <li>Entrance to the Museum of the Turtle USD $2 per pax (Subject to change)</li>
                                            <li>Entrance to the Tortuguero National Park USD $15 per pax (Subject to change)</li>
                                            <li>Turtle nesting night tour (Jul-Oct) USD $35 per pax (Subject to change).</li>
                                        </ul>
                                        <p>
                                            General Conditions</p>
                                        <ul>
                                            <li>Rates per person in US dollars.</li>
                                            <li>Includes food and transportation taxes from San José.</li>
                                            <li>10kg = 25 Lbs., maximum luggage weight per person.</li>
                                            <li>Check in at 12:00 MD - Check out: 09:00 am.</li>
                                            <li>Maximum room capacity is 4 people.</li>
                                            <li>Maximum capacity per room has a 4 people limit.</li>
                                            <li>We do not admit groups greater than 6 rooms - only upon request</li>
                                            <li>No Pets admitted - of any kind</li>
                                            <li>We reserved the right to charge a fee for the dinners or celebrations of Christmas
                                                and New Year, which will not be of obligatory purchase</li>
                                        </ul>
                                    </div>
                                </div>
                                <div id="rates" class="tbcontent">
                                    <div class="tar_paq_2_1"  id="tar_paq_2_1" runat="server">
                                        <div class="contenedor-rates">
                                            <div class="rates-columna1">
                                                <h4>
                                                    2015</h4>
                                                <p>
                                                    Rates per person, valid from January 1st until December 31st, 2015</p>
                                                <div class="columna-tabla-rates first">
                                                    <table cellspacing="0" cellpadding="0" border="1" class="width325 aligncenter season-a encabezado">
                                                        <tbody>
                                                            <tr>
                                                                <td class="headerTabla2 bordeTable">
                                                                    <span class="titulo-dia">Season A</span> Jan - Feb - Mar - Apr - Jul - Agust - Dec
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                    <table cellspacing="0" cellpadding="0" border="1" class="width325 aligncenter season-a">
                                                        <tbody>
                                                            <tr class="headerTabla">
                                                                <td>
                                                                    OCCUPATION
                                                                </td>
                                                                <td>
                                                                    RATE
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    SINGLE
                                                                </td>
                                                                <td>
                                                                    $ 301
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    DOUBLE
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
                                                                    QUADRUPLE
                                                                </td>
                                                                <td>
                                                                    $ 225
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    ADULT (Additional Night)
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
                                                                    <span class="titulo-dia">Season B</span> May - Jun - Set - Oct - Nov
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                    <table cellspacing="0" cellpadding="0" border="1" class="width325 aligncenter season-b">
                                                        <tbody>
                                                            <tr class="headerTabla">
                                                                <td>
                                                                    OCCUPATION.
                                                                </td>
                                                                <td>
                                                                    RATE
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    SINGLE
                                                                </td>
                                                                <td>
                                                                    $ 250
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    DOUBLE
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
                                                                    QUADRUPLE
                                                                </td>
                                                                <td>
                                                                    $ 190
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    ADULT (Additional Night)
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
                                                    Rates per person, valid from January 1st until December 31st, 2016</p>
                                                <div class="columna-tabla-rates first">
                                                    <table cellspacing="0" cellpadding="0" border="1" class="width325 aligncenter season-a encabezado">
                                                        <tbody>
                                                            <tr>
                                                                <td class="headerTabla2 bordeTable">
                                                                    <span class="titulo-dia">Season A</span> Jan - Feb - Mar - Apr - Jul - Agust - Dec
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                    <table cellspacing="0" cellpadding="0" border="1" class="width325 aligncenter season-a">
                                                        <tbody>
                                                            <tr class="headerTabla">
                                                                <td>
                                                                    OCCUPATION
                                                                </td>
                                                                <td>
                                                                    RATE
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    SINGLE
                                                                </td>
                                                                <td>
                                                                    $ 317
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    DOUBLE
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
                                                                    QUADRUPLE
                                                                </td>
                                                                <td>
                                                                    $236
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    ADULT (Additional Night)
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
                                                                    <span class="titulo-dia">Season B</span> May - Jun - Set - Oct - Nov
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                    <table cellspacing="0" cellpadding="0" border="1" class="width325 aligncenter season-b">
                                                        <tbody>
                                                            <tr class="headerTabla">
                                                                <td>
                                                                    OCCUPATION.
                                                                </td>
                                                                <td>
                                                                    RATE
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    SINGLE
                                                                </td>
                                                                <td>
                                                                    $ 262
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    DOUBLE
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
                                                                    QUADRUPLE
                                                                </td>
                                                                <td>
                                                                    $ 199
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    ADULT (Additional Night)
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
                                                    Rates per person, valid from January 1st until December 31st, 2015</p>
                                                <div class="columna-tabla-rates first">
                                                    <table cellspacing="0" cellpadding="0" border="1" class="width325 aligncenter season-a encabezado">
                                                        <tbody>
                                                            <tr>
                                                                <td class="headerTabla2 bordeTable">
                                                                    <span class="titulo-dia">Season A</span> Jan - Feb - Mar - Apr - Jul - Agust - Dec
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                    <table cellspacing="0" cellpadding="0" border="1" class="width325 aligncenter season-a">
                                                        <tbody>
                                                            <tr class="headerTabla">
                                                                <td>
                                                                    OCCUPATION
                                                                </td>
                                                                <td>
                                                                    RATE
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    SINGLE
                                                                </td>
                                                                <td>
                                                                    $ 428
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    DOUBLE
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
                                                                    QUADRUPLE
                                                                </td>
                                                                <td>
                                                                    $ 322
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    ADULT (Additional Night)
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
                                                                    <span class="titulo-dia">Season B</span> May - Jun - Set - Oct - Nov
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                    <table cellspacing="0" cellpadding="0" border="1" class="width325 aligncenter season-b">
                                                        <tbody>
                                                            <tr class="headerTabla">
                                                                <td>
                                                                    OCCUPATION.
                                                                </td>
                                                                <td>
                                                                    RATE
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    SINGLE
                                                                </td>
                                                                <td>
                                                                    $ 354
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    DOUBLE
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
                                                                    QUADRUPLE
                                                                </td>
                                                                <td>
                                                                    $ 262
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    ADULT (Additional Night)
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
                                                    Rates per person, valid from January 1st until December 31st, 2016</p>
                                                <div class="columna-tabla-rates first">
                                                    <table cellspacing="0" cellpadding="0" border="1" class="width325 aligncenter season-a encabezado">
                                                        <tbody>
                                                            <tr>
                                                                <td class="headerTabla2 bordeTable">
                                                                    <span class="titulo-dia">Season A</span> Jan - Feb - Mar - Apr - Jul - Agust - Dec
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                    <table cellspacing="0" cellpadding="0" border="1" class="width325 aligncenter season-a">
                                                        <tbody>
                                                            <tr class="headerTabla">
                                                                <td>
                                                                    OCCUPATION
                                                                </td>
                                                                <td>
                                                                    RATE
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    SINGLE
                                                                </td>
                                                                <td>
                                                                    $ 450
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    DOUBLE
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
                                                                    QUADRUPLE
                                                                </td>
                                                                <td>
                                                                    $ 338
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    ADULT (Additional Night)
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
                                                                    <span class="titulo-dia">Season B</span> May - Jun - Set - Oct - Nov
                                                                </td>
                                                            </tr>
                                                        </tbody>
                                                    </table>
                                                    <table cellspacing="0" cellpadding="0" border="1" class="width325 aligncenter season-b">
                                                        <tbody>
                                                            <tr class="headerTabla">
                                                                <td>
                                                                    OCCUPATION.
                                                                </td>
                                                                <td>
                                                                    RATE
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    SINGLE
                                                                </td>
                                                                <td>
                                                                    $ 371
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    DOUBLE
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
                                                                    QUADRUPLE
                                                                </td>
                                                                <td>
                                                                    $ 275
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    ADULT (Additional Night)
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
                                        Book now your package.
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="content-box container-1-3 sidebar">
                            <div class="paddingBottomp10">
                                <h3 class="h3Sidebar">
                                    <strong>Other packages</strong> that might interested</h3>
                                <div class="sidebar-block-content">
                                    <div class="sidebar-paquete sidebar-paquete-1">
                                        <h3 class="paquete-sidebar-titulo">
                                            1 Free extra night</h3>
                                        <div class="paquete-sidebar-desc-precio">
                                            <div class="paquete-sidebar-desc">
                                                <ul>
                                                    <li>Upgrade from 2 nights/3 days package to 3 nights/4 days one.</li>
                                                    <li>During may and june.</li>
                                                    <li>Valid only for foreigners.</li>
                                                </ul>
                                            </div>
                                            <div class="paquete-sidebar-precio">
                                                <p class="paquete-precio">
                                                    <span>From </span>$310</p>
                                                <p class="habitacion-desc">
                                                    Double Room</p>
                                            </div>
                                        </div>
                                        <div class="paquete-sidebar-book-box">
                                            <a class="paquets-sidebar-book-link poplight" rel="popup_code" href="#?w=408">Book now</a>
                                        </div>
                                        <div class="sidebar-paquete-popup-1 popup_block" id="popup_code">
                                            <div class="sidebar-paquete-popup-texto">
                                                <p>
                                                    <strong>Contact a Manatus representative</strong> to book this package</p>
                                            </div>
                                            <div class="sidebar-paquete-popup-botones">
                                                <div class="sidebar-paquete-popup-botones-form">
                                                    <a href="http://manatus.net/contact-us" class="boton-form-rojo">Form</a>
                                                </div>
                                                <div class="sidebar-paquete-popup-botones-chat">
                                                    <a href="javascript:%20$zopim.livechat.window.show();" class="boton-chat-cafe">Live
                                                        Chat</a>
                                                </div>
                                            </div>
                                            <div class="sidebar-paquete-popup-contact-info">
                                                <p>
                                                    <strong>Manatus Hotel</strong><br />
                                                    Office: <a href="tel:+50622394854">(506) 2239-4854</a><br />
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
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="up_paso2" runat="server" UpdateMode="Always">
                <ContentTemplate>
                    <div id="paso2" runat="server" visible="false">
                        <div class="form-title">
                            <h2 class="h2Celeste">
                                <strong>Please fill out your personal information</strong></h2>
                        </div>
                        <!-- register form -->
                        <div class="content-box container-2-3">
                            <asp:UpdatePanel ID="updtpanel_form" runat="server">
                                <ContentTemplate>
                                    <div>
                                        <div class="fondoGrisReserva">
                                            <div class="paddingReserva">
                                                <asp:Panel ID="panel_2" runat="server" Visible="true">
                                                    <asp:Panel ID="pnl_contenido_form" runat="server" Visible="true">
                                                        <div class="wrapper-field">
                                                            <asp:Label ID="lbl_nombre" runat="server" Text="Full name" class="span-field"></asp:Label>
                                                            <asp:TextBox ID="txt_nombre" runat="server" CssClass="textBoxNuevo"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfv_nombre" runat="server" ControlToValidate="txt_nombre"
                                                                Display="Dynamic" ErrorMessage="Required field" ValidationGroup="registrese"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="wrapper-field">
                                                            <asp:Label ID="lbl_dir" runat="server" Text="Address" class="span-field"></asp:Label>
                                                            <asp:TextBox ID="txt_direccion" runat="server" CssClass="textBoxNuevo"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfv_direccion" runat="server" ControlToValidate="txt_direccion"
                                                                Display="Dynamic" ErrorMessage="Required field" ValidationGroup="registrese"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="box-ubicacion wrapper-field">
                                                            <asp:Label ID="lbl_ubicacion" runat="server" Text="Country" class="span-field"></asp:Label>
                                                            <asp:DropDownList ID="ddl_ubicacion" runat="server" CssClass="dropdownsReserva">
                                                            </asp:DropDownList>
                                                        </div>
                                                        <!--<div>
                                                <asp:Label ID="lbl_codigo" runat="server" Text="Código Postal:"></asp:Label>
                                                <asp:TextBox ID="txt_codPostal" runat="server" CssClass="textBoxNuevo"></asp:TextBox>
                                                </div>-->
                                                        <div class="cod-telefono">
                                                            <div class="wrapper-field">
                                                                <asp:Label ID="lbl_codigoA" runat="server" Text="Area code" class="span-field"></asp:Label>
                                                                <asp:TextBox ID="txt_codArea" runat="server" CssClass="textBoxNuevo"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfv_codigoA" runat="server" ControlToValidate="txt_codArea"
                                                                    Display="Dynamic" ErrorMessage="Required field" ValidationGroup="registrese"></asp:RequiredFieldValidator>
                                                            </div>
                                                            <div class="wrapper-field">
                                                                <asp:Label ID="lbl_telefono" runat="server" Text="Phone" class="span-field"></asp:Label>
                                                                <asp:TextBox ID="txt_tel" runat="server" CssClass="textBoxNuevo"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfv_Tel" runat="server" ControlToValidate="txt_tel"
                                                                    Display="Dynamic" ErrorMessage="Required field" ValidationGroup="registrese"></asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                        <div class="wrapper-field">
                                                            <asp:Label ID="lbl_email" runat="server" Text="Email" class="span-field"></asp:Label>
                                                            <asp:TextBox ID="txt_email" runat="server" CssClass="textBoxNuevo"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfv_email" runat="server" ControlToValidate="txt_email"
                                                                Display="Dynamic" ErrorMessage="Required field" ValidationGroup="registrese"></asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="rev_email" runat="server" ControlToValidate="txt_email"
                                                                Display="Dynamic" ErrorMessage="Formato incorrecto" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                                ValidationGroup="registrese"></asp:RegularExpressionValidator>
                                                        </div>
                                                        <div class="wrapper-field">
                                                            <asp:Label ID="lbl_mensajeAdicional" runat="server" Text="Additional message" class="span-field"></asp:Label>
                                                            <asp:TextBox ID="txt_observaciones" runat="server" CssClass="textMensajeAdicional"
                                                                Height="70px" Rows="5" TextMode="MultiLine"></asp:TextBox>
                                                        </div>
                                                        <div id="terminos-condiciones" class="wrapper-field">
                                                            <asp:HyperLink NavigateUrl="javascript:void(0)" runat="server" Text="See Terms and Conditions"
                                                                ID="linkTerminosCondiciones" />
                                                        </div>
                                                        <div class="wrapper-field">
                                                            <asp:CheckBox Text="I have read and accept the Terms and Conditions" runat="server"
                                                                ID="chkTerminosCondiciones" />
                                                            <asp:CustomValidator ID="val_terminosCondiciones" runat="server" ErrorMessage="Please accept the terms..."
                                                                ClientValidationFunction="CustomValidatorTerminosCondiciones" ValidationGroup="registrese"></asp:CustomValidator>
                                                            <%--<asp:RequiredFieldValidator ID="rev_terminosCondiciones" runat="server" ControlToValidate="chkTerminosCondiciones" Display="Dynamic" ErrorMessage="Required field" ValidationGroup="registrese"></asp:RequiredFieldValidator>--%>
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
                                            ForeColor="#FFFFFF" Visible="true">SEND INFORMATION AND CONTINUE TO STEP 3: PAYMENT DATA</asp:LinkButton>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <!-- end register form -->
                        </div>
                        <div class="content-box container-1-3 sidebar">
                            <div class="logo-ssl">
                                <img src="images/verisign.png" />
                            </div>
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
                                    <asp:LinkButton ID="LinkEditarInformacion" runat="server">< Editar información</asp:LinkButton>
                                </div>
                            </div>
                            <!-- end sidebar with the information previous step -->
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
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
                    <li class="active-trail"><a href="http://booking.manatuscostarica.com/reservacion_en.aspx">
                        Reservations</a></li>
                    <li><a href="http://manatuscostarica.com/blog">Blog</a></li>
                </ul>
            </div>
            <div class="column1 column">
                <ul>
                    <li><a href="http://booking.manatuscostarica.com/reservacion_en.aspx" class="active-trail">
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
                        <li><a href="http://manatuscostarica.com/">Home</a></li>|<li><a href="http://booking.manatuscostarica.com/reservacion_en.aspx">
                            Book Now</a></li>|<li><a href="http://manatuscostarica.com/gallery">Gallery</a></li>|<li>
                                <a href="http://manatuscostarica.com/contact-us">Contact</a></li></ul>
                </div>
                <div class="contact-info">
                    <p>
                        MANATUS HOTEL, TORTUGUERO COSTA RICA | TEL: <a href="tel: +50627098197">(506) 2709.8197</a></p>
                </div>
                <div class="menu-idioma">
                    <ul>
                        <li><a href="reservacion_en.aspx" class="active">English</a></li>|<li><a href="reservacion_sp.aspx">
                            Español</a></li></ul>
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
</body>
</html>
