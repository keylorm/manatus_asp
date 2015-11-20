<%@ Page Language="VB" AutoEventWireup="false" CodeFile="reservacion_en.aspx.vb"
    Inherits="reservacion_sp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <title>Manatus Hotel Reservations, on-line reservations, booking, hotel Tortuguero
    
    </title>
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
        <link href="styles/stylereservation_nuevo_responsivo.css" type="text/css" />
    <!--<![endif]-->



    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.1/jquery.min.js"></script>


    <script type="text/javascript" src="http://booking.manatuscostarica.com/js/myJS.js"></script>
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
        window.$zopim || (function(d, s) {
            var z = $zopim = function(c) { z._.push(c) }, $ = z.s =
d.createElement(s), e = d.getElementsByTagName(s)[0]; z.set = function(o) {
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




</head>
<body class="reservation-form en">
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
					<ul><li><a href="reservacion_en.aspx" class="active">English</a></li>|<li><a href="reservacion_sp.aspx">Español</a></li></ul>
				</div>
				<div class="menu-top">
					<ul><li><a href="http://manatuscostarica.com/">Home</a></li>|<li><a href="http://booking.manatuscostarica.com/reservacion_en.aspx">Book Now</a></li>|<li><a href="http://manatuscostarica.com/gallery">Gallery</a></li>|<li><a href="http://manatuscostarica.com/contact-us">Contact</a></li></ul>
				</div>
				<div class="contact-info">
					<p>MANATUS HOTEL, TORTUGUERO COSTA RICA | TEL: <a href="tel: +50627098197">(506) 2709.8197</a></p>
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
			<div id="page-head">
				<div class="page-title">
					<h1 title="On-line reservations, booking at Manatus, tortuguero hotel reservations" class="h1ReservaNew">RESERVATION</h1>
				</div>
				<div class="breadcrumbs">
                    <a href="http://manatuscostarica.com/" title="P&aacute;gina principal Hotel Manatus Tortuguero Costa Rica">Home</a> / Reservation
                </div>
			</div>
		</div>
    
    </div>

    <div id="main-content">

                <div class="content container-980">

                               <div class="content-box container-2-3">
                                            <asp:UpdatePanel ID="updtpanel_reservacion" runat="server">
                                                <ContentTemplate>
                                                    <h3 class="lbl-paquete">
                                                        <asp:Label ID="lbl_paquete" runat="server" Text="" Visible="false"></asp:Label>
                                                    </h3>
                                                    
                                                    <div class="headerReservation">
                                                        <h2>MAKE your reservation TODAY</h2>
                                                    </div>
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
                                                                        
                                                                                    <div style="display: none;"><asp:Label ID="lbl_nombre" runat="server" Text="Nombre:"></asp:Label></div>
                                                                                
                                                                                    <asp:TextBox ID="txt_nombre" runat="server" CssClass="textBoxNuevo"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="rfv_nombre" runat="server" ControlToValidate="txt_direccion"
                                                                                        Display="Dynamic" ErrorMessage="Campo requerido" ValidationGroup="registrese"></asp:RequiredFieldValidator>
                                                                                
                                                                                    <div style="display: none;"><asp:Label ID="lbl_dir" runat="server" Text="Dirección:"></asp:Label></div>
                                                                                
                                                                                    <asp:TextBox ID="txt_direccion" runat="server" CssClass="textBoxNuevo"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="rfv_direccion" runat="server" ControlToValidate="txt_direccion"
                                                                                        Display="Dynamic" ErrorMessage="Campo requerido" ValidationGroup="registrese"></asp:RequiredFieldValidator>
                                                                                
                                                                                    <div style="display: none;"><asp:Label ID="lbl_codigo" runat="server" Text="Código Postal:"></asp:Label>
                                                                                
                                                                                    <asp:TextBox ID="txt_codPostal" runat="server" CssClass="textBoxNuevo"></asp:TextBox></div>
                                                                                
                                                                                    <div style="display: none;"><asp:Label ID="lbl_ubicacion" runat="server" Text="País:"></asp:Label></div>
                                                                                
                                                                                    <div class="box-ubicacion"><asp:DropDownList ID="ddl_ubicacion" runat="server" CssClass="dropdownsReserva">
                                                                                    </asp:DropDownList></div>
                                                                                
                                                                                    <div style="display: none;"><asp:Label ID="lbl_codigoA" runat="server" Text="Código de Area:"></asp:Label></div>
                                                                                
                                                                                    <asp:TextBox ID="txt_codArea" runat="server" CssClass="textBoxNuevo"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="rfv_codigoA" runat="server" ControlToValidate="txt_codArea"
                                                                                        Display="Dynamic" ErrorMessage="Campo requerido" ValidationGroup="registrese"></asp:RequiredFieldValidator>
                                                                                
                                                                                    <div style="display: none;"><asp:Label ID="lbl_telefono" runat="server" Text="Teléfono:"></asp:Label></div>
                                                                                
                                                                                    <asp:TextBox ID="txt_tel" runat="server" CssClass="textBoxNuevo"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="rfv_Tel" runat="server" ControlToValidate="txt_tel"
                                                                                        Display="Dynamic" ErrorMessage="Campo requerido" ValidationGroup="registrese"></asp:RequiredFieldValidator>
                                                                                    
                                                                                
                                                                                    <div style="display: none;"><asp:Label ID="lbl_email" runat="server" Text="  Email:"></asp:Label></div>
                                                                                
                                                                                    <asp:TextBox ID="txt_email" runat="server" CssClass="textBoxNuevo"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="rfv_email" runat="server" ControlToValidate="txt_email"
                                                                                        Display="Dynamic" ErrorMessage="Campo Requerido" ValidationGroup="registrese"></asp:RequiredFieldValidator>
                                                                                    <asp:RegularExpressionValidator ID="rev_email" runat="server" ControlToValidate="txt_email"
                                                                                        Display="Dynamic" ErrorMessage="Formato incorrecto" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                                                        ValidationGroup="registrese"></asp:RegularExpressionValidator>
                                                                                
                                                                                    <div style="display: none;"><asp:Label ID="lbl_fechaE" runat="server" Text="Fecha de Entrada:"></asp:Label></div>
                                                                                
                                                                                    <div style="width: 100%; display: block;">
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
                                                                                                <%-- --%>
                                                                                            
                                                                                                <div style="display: none;"><asp:Label ID="lbl_fechaS" runat="server" Text="Fecha de Salida:"></asp:Label></div>   
                                                                                            
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
                                                                                    <div style="display: none;"><asp:Label runat="server" ID="lbl_noches" Text="La 3er y 4ta noche son en Manatus"
                                                                                        Visible="false">
                                                                                    </asp:Label></div>
                                                                                    <div id="traslado-box">
                                                                                    <p>** Transfer</p>
                                                                                
                                                                                
                                                                                    <asp:RadioButtonList ID="rdbtnlist_transporte2014" runat="server" AutoPostBack="true">
                                                                                        <asp:ListItem Value="1">Transfer to Manatus</asp:ListItem>
                                                                                        <asp:ListItem Value="2">Transfer back to San Jose</asp:ListItem>
                                                                                        <asp:ListItem Value="3">Round Trip</asp:ListItem>
                                                                                    </asp:RadioButtonList>
                                                                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="rdbtnlist_transporte2014"
                                                                                                            Display="Dynamic" ErrorMessage="Campo Requerido" ValidationGroup="registrese"></asp:RequiredFieldValidator>
                                                                                
                                                                                    <div style="display: none;"><p>Pick up place</p></div>
                                                                                    <div ID="box_pickup" runat="server" visible="false">
                                                                                        <asp:TextBox ID="txt_pickup" runat="server" CssClass="textBoxNuevo"></asp:TextBox>
                                                                                    </div>
                                                                                   <div style="display: none;"><p>Drop off place</p></div>
                                                                                    <div ID="box_leave" runat="server" visible="false">
                                                                                        <asp:TextBox ID="txt_leave" runat="server" CssClass="textBoxNuevo"></asp:TextBox>
                                                                                    </div>

                                                                                    </div>

                                                                                    <div id="box-general-habitaciones-personas">
                                                                                    <div class="habitaciones-wrapper">
                                                                                    <asp:Label ID="lbl_nHabitaciones" runat="server" Text="Habitaciones:"></asp:Label>
                                                                               
                                                                                                <div class="box-select-habitaciones"><asp:DropDownList ID="ddl_habitaciones" runat="server" AutoPostBack="true" CssClass="dropdownsReserva">
                                                                                                </asp:DropDownList></div>
                                                                                            </div>
                                                                                                <asp:Panel ID="pnl_resultados" runat="server">
                                                                                                    <asp:GridView ID="gv_ResultadosDisponibles" runat="server" ShowHeader="false" AutoGenerateColumns="False"
                                                                                                        GridLines="None" Width="246px">
                                                                                                        <RowStyle HorizontalAlign="Left" />
                                                                                                        <HeaderStyle Font-Bold="false" HorizontalAlign="Left" />
                                                                                                        <Columns>
                                                                                                            <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Left">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label ID="lbl_nombre" CssClass="marginleft15" runat="server" Font-Bold="false"
                                                                                                                        Font-Italic="true" ForeColor="#999999" Text="Nombre"></asp:Label>
                                                                                                                </ItemTemplate>
                                                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField HeaderText="Personas" HeaderStyle-HorizontalAlign="Right">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label ID="lbl_personas" Font-Bold="false" runat="server" Text=""></asp:Label>
                                                                                                                    <div class="box-select-personas"><asp:DropDownList ID="ddl_personas" CssClass="dropdownsReserva" runat="server">
                                                                                                                        <asp:ListItem Text="1"></asp:ListItem>
                                                                                                                        <asp:ListItem Text="2"></asp:ListItem>
                                                                                                                        <asp:ListItem Text="3"></asp:ListItem>
                                                                                                                        <asp:ListItem Text="4"></asp:ListItem>
                                                                                                                    </asp:DropDownList></div>
                                                                                                                </ItemTemplate>
                                                                                                                <ItemStyle HorizontalAlign="Right" />
                                                                                                                <HeaderStyle Font-Bold="true" />
                                                                                                            </asp:TemplateField>
                                                                                                        </Columns>
                                                                                                        <RowStyle Font-Bold="true" />
                                                                                                    </asp:GridView>
                                                                                                </asp:Panel>
                                                                                     </div>
                                                                                    <div style="display: none;"><asp:Label ID="lbl_observaciones" runat="server" Text="Observaciones:"></asp:Label></div>
                                                                                
                                                                                    <asp:TextBox ID="txt_observaciones" runat="server" CssClass="textBoxNuevo" Height="70px"
                                                                                        Rows="5" TextMode="MultiLine"></asp:TextBox>
                                                                                
                                                                                    <%-- <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                                                        <ContentTemplate>
                                                                                        </ContentTemplate>
                                                                                    </asp:UpdatePanel>--%>
                                                                                    <span class="terminosNuevo">
                                                                                        <asp:HyperLink ID="hyplnk_paquetes" runat="server" Visible="false" Text="&nbsp;Read
                                                                                        our Terms and Conditions »" Target="_blank"></asp:HyperLink>
                                                                                        <a href="#?w=620" rel="popup_code" class="poplight" id="old_terms" runat="server">&nbsp;Read
                                                                                            our Terms and Conditions »</a> </span>
                                                                                    <br />
                                                                                    <asp:CheckBox ID="chkbox_terminos" runat="server" Text="I have read and I Accepted the Terms and Conditions"
                                                                                        ForeColor="#666452" /><br />
                                                                                    <asp:Label ID="lbl_terminos" runat="server" Text="*You must accept the terms and conditions"
                                                                                        Visible="false" ForeColor="Red"></asp:Label>
                                                                                
                                                                                    <asp:Label ID="lbl_erroFechas" runat="server" Text="" ForeColor="red"></asp:Label>
                                                                                    <asp:Label ID="lbl_ResultadoReservacion" runat="server" Text=""></asp:Label>
                                                                                
                                                                    </asp:Panel>
                                                                </asp:Panel>
                                                                <br />
                                                                <p class="link-chat">* If you have questions please do not hesitate to contact us through our <span class="colorRojo">
                                                                    <a href="javascript: $zopim.livechat.window.show();">CHAT</a></span><br />
                                                                    ** Service subject to availability. Guests must contact Manatus Hotel in order to establish the pick up or drop off place.</p>
                                                                <br />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="alignRight btn_reserva">
                                                        <%-- Comentado por Keylor para Cambios mayo 2013: SE CAMBIO EL COLOR DEL LINK #842702 POR BLANCO  --%>
                                                        <asp:LinkButton ID="btn_reservar" runat="server" ToolTip="Reservar" ValidationGroup="registrese"
                                                            ForeColor="#FFFFFF" Visible="false">BOOK NOW »</asp:LinkButton>
                                                        <%--<asp:ImageButton ID="btn_reservar" runat="server" Text="Reservar" ImageUrl="~/images/Otros/book.gif" />--%>
                                                    </div>
                                                    <br />
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                               </div>
                               <div class="sidebar">         
                                            <div class="paddingBottomp10">
                                                <h3 class="h2Reserva">
                                                    CONTACT US</h3>
                                                
                                                <p>Tortuguero, Costa Rica<br />
                                                Office(506) 2239-7364<br />
                                                Hotel (506) 2709-8197<br />
                                                Fax (506) 2239-7366<br />
                                                Email: <a href="mailto:info@manatuscostarica.com">info@manatuscostarica.com</a></p>
                                               <p>
                                                To call Costa Rica, dial:
                                                <br />
                                                International Access Code + Area Code Costa Rica (506) + (number you want call)
                                                (Ask the telephone company of your country the International Access Code.)</p>
                                                <br />
                                                <h3 class="chat"><a href="javascript:%20$zopim.livechat.window.show();">CHAT WITH US</a></h3>
                                            </div>

                                            <ul class="menu-sidebar"><li class="first leaf virtual-tours mid-959"><a title="" href="http://manatuscostarica.com/virtual-tours/garden">Virtual Tours</a></li>
<li class="leaf photo-video-gallery mid-961"><a title="" href="http://manatuscostarica.com/gallery">Photo &amp; Video Gallery</a></li>
<li class="last leaf from-our-blog mid-963"><a title="" href="http://manatuscostarica.com/blog">From our Blog</a></li>
</ul>

                                            
                                        
                                </div>
                            
                </div>
    </div>
    </div>
            <div id="footer">
		        <div id="footer-inner" class="container-980">

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
    
            </div>    
            
    </form>
    
    

    <script type="text/javascript">

        $('a.poplight[href^=#]').live("click", function() {

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

        $('a.close, #fade').live('click', function() { //When clicking on the close or fade layer...

            $('#fade , .popup_block').fadeOut(function() {

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

    <div id="popup_code" class="popup_block">
        <a class="close" href="#">
            <img alt="Close" title="" class="btn_close" src="images/close_pop.png"></a>
        <br />
        <asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical" Height="400px" Visible="true">
            <div class="textoTerminos">
                <center>
                    <h1>
                        Terms and Conditions 2013</h1>
                    <span class="terminosNuevo"><a href="#terminos2014">(see below Terms and Conditions
                        2014)</a></span>
                </center>
                <h2>
                    The package Includes</h2>
                <ul>
                    <li>Ground Transfer: San Jos&eacute; - (Pavona / Ca&ntilde;o Blanco) - San Jos&eacute;</li>
                    <li>Boat transfer: (Pavona / Ca&ntilde;o Blanco) - Tortuguero - (Pavona / Ca&ntilde;o
                        Blanco)</li>
                    <li>Lodging, all meals and excursions with guide: Tour to the town and water channels.</li>
                    <%--  <li>The package of more than 1 night includes the hiking into the national park (entrance
                        not included)</li>--%>
                    <li>Breakfast on route and lunch on route</li>
                    <li>All taxes without ICT tax.</li>
                </ul>
                <h2>
                    Not included on the package:</h2>
                <ul>
                    <li>Alcoholic beverages, sodas, bottled water.</li>
                    <li>Entrance to the Museum of Turtle USD $1 per pax (Subject to change)</li>
                    <li>Entrance to National Park USD $10 per pax (Subject to change)</li>
                    <li>Night Tour of egg-laying of turtles (Jul-Oct) USD $35 per pax (Subject to change)</li>
                </ul>
                <h2>
                    General Conditions</h2>
                <ul>
                    <li>Check in at 12:00 MD - Check out: 09:00 am.</li>
                    <li>We do not admit groups greater than 6 rooms - only upon request</li>
                    <li>No Pets admitted - of any type</li>
                    <li>We reserved the right to load a supplement for the dinners or celebrations of Christmas
                        and New Year, that will be of compulsory purchase.</li>
                    <li>Limit of Maximum capacity by room is of 4 people.</li>
                </ul>
                <h2>
                    Children Policies</h2>
                <ul>
                    <li>Given the characteristics of the hotel, the atmosphere of privacy and tranquility
                        that we want to provide our guests, the hotel has taken the decision to allow only
                        children older than 6 years old.</li>
                    <li>They will pay full adult rates</li>
                </ul>
                <h2>
                    Guides Policies</h2>
                <ul>
                    <li>Guides in groups with more of 6 rooms will be receiving CPL with room and meals
                        for guides</li>
                    <li>Guides in groups from 4 to 5 rooms must be paid 25% of our SGL rack rates for the
                        guide package</li>
                    <li>Guides in groups from 1 to 3 rooms must be paid 50% of our SGL rack rates for the
                        guide package</li>
                    <li>Guides in regular room of the hotel must be paid regular net rates of the agencyr
                        pax (Subject to change)</li>
                </ul>
                <h2>
                    Policies of Payment for FIT's and Groups</h2>
                <p>
                    Due to the small size of our Hotel and to its remote location, the prepayment of
                    all reservation is necessary. The reservation of the services will be kept during
                    21 days after receiving the written request by fax or by electronic mail. A deposit
                    of 25% is required, in order to confirm after this period of 21 days or the reservation
                    will be cancelled. The reservations are confirmed once 25% of the total amount is
                    received by our offices, via check, credit card or by means of deposit in our bank
                    account. The payment balance will have to be received not later than 30 days before
                    the arrival from the guests to the Hotel, in the case of FIT's and 60 days before
                    the check in of the guests in the case of GROUPS. Reservations made within 30 days
                    before (FIT'S) or 60 days earlier (groups) of the check in to the hotel, will have
                    two business days to confirm and pay or cancel without any surcharge.
                </p>
                <h2>
                    Bank Accounts for Payments</h2>
                <p>
                    Bank Account in dollars U.S. $ in the Bac de San Jose (BAC) # 90 58 87 964 Under
                    the name of: La Casa del Manat&iacute; S.A. Please send copy of deposit by fax to
                    the number 00(506) - 2239 - 4857 Cancellation Policies for Groups.
                </p>
                <p>
                    Groups requiring more than 6 rooms are not admitted. Cancellations of reserved groups
                    will have the following penalties: The days are counted as days before the check
                    in of the clients.
                </p>
                <ul>
                    <li>Before 60 days - No charges</li>
                    <li>59 to 30 days 10% of the amount of the bookings</li>
                    <li>29 to 15 days 25% of the amount of the bookings</li>
                    <li>14 to 08 days 50% of the amount of the bookings</li>
                    <li>Less than 07 days, 100% of the amount of the bookings</li>
                    <li>No refunds for "no shows"</li>
                </ul>
                <h2>
                    No refunds for "no shows"
                </h2>
                <p>
                    Cancellations of FIT's or individual passengers will have the following penalties:
                    The days are counted as days before the check in of the clients
                </p>
                <ul>
                    <li>Before 30 days - without charges</li>
                    <li>29 to 15 days 10% of the amount of the booking</li>
                    <li>14 to 10 days 25% of the amount of the booking</li>
                    <li>09 to 04 days 50% of the amount of the booking</li>
                    <li>Less than 03 days, 100% of the amount of the booking</li>
                    <li>No refunds for "no shows"</li>
                </ul>
                <h2>
                    Refund policies for reservations cancellations</h2>
                <p>
                    Regarding the return money for paid and cancelled reservations, the hotel will only
                    refund if the reservations was made directly at the hotel and if the reason was
                    that the costumer wasn't able to travel.<br />
                    If the reservation was made through a travel agency that charges a commission, the
                    hotel will cancel the reservation but it won't refund the money, it will issue a
                    credit for either the customer or the travel agency staff.<br />
                    The Hotels booking and account department will send a memo via email or fax to the
                    travel agency with the amount that will apply in favor of the travel agency. This
                    credit is valid for a year.<br />
                    Terms of payment and reimbursement in case of cancellation of booking by the hotel:
                    <br />
                    <br />
                    In case of natural emergencies, strikes, landslides or situations beyond the contol
                    of the Hotel and thes is no possibility of providing the service both the transportation
                    and accomodation.<br />
                    THE HOTEL, reserves the right to return 70% of the reservation, the remaining 30%
                    of it is to cover operation expenses.<br />
                    Returns to the agencies shall be made through letters of credit by the amount corresponding
                    to 70% net of booking, if it is a booking direct to the hotel cover 70% of the reservation
                    in cash within 24 hours.<br />
                    If for reasons attributable to the hotel, this will assume the cost of lodging,
                    meals, transportation and derivates (over a period of time equivalent to the reservation
                    made at the HOTEL), until the HOTEL will be able to provide the service booked.
                </p>
                <br />
                <center>
                    <a name="terminos2014"></a>
                    <h1>
                        Terms and Conditions 2014</h1>
                </center>
                <h2>
                    The package Includes</h2>
                <ul>
                    <li>Lodging, all meals. Beverages are not included.</li>
                    <li>2 days 1 night package includes two excursions with guide: one tour to the town
                        and one tour to the water channels. Entrance not included. </li>
                    <li>Lodging, all meals and excursions with guide: Tour to the town and water channels.</li>
                    <%--  <li>The package of more than 1 night includes the hiking into the national park (entrance
                        not included)</li>--%>
                    <li>3 days 2 nights package includes three excursions with guide: one tour to the town
                        and two tours to the water channels. Entrance not included. </li>
                    <li>All taxes</li>
                </ul>
                <h2>
                    Not included on the package:</h2>
                <ul>
                    <li>Ground Transfer: San José - (Pavona / Caño Blanco) - San José</li>
                    <li>Boat transfer: (Pavona / Caño Blanco) - Tortuguero - (Pavona / Caño Blanco)</li>
                    <li>Breakfast and lunch on route.</li>
                    <li>The transfer will cost $ 70 each way. In the way SJ-Tortuguero breakfast will be
                        provided on route. In the way Tortuguero-SJ lunch will be provided on route.</li>
                    <li>Alcoholic beverages, sodas, bottled water.</li>
                    <li>Entrance to the Museum of the Turtle USD $1 per pax (Subject to change)</li>
                    <li>Entrance to the Tortuguero National Park USD $10 per pax (Subject to change)</li>
                    <li>Turtle nesting night tour (Jul-Oct) USD $35 per pax (Subject to change). </li>
                </ul>
                <h2>
                    General Conditions</h2>
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
                        and New Year, which will not be of obligatory purchase.</li>
                </ul>
                <h2>
                    Children Policies</h2>
                <ul>
                    <li>Given the characteristics of the hotel, the atmosphere of privacy and tranquility
                        that we want to provide our guests, the hotel has taken the decision to allow only
                        children older than 3 years.</li>
                    <li>They will pay full adult rates.</li>
                </ul>
                <h2>
                    Guides Policies</h2>
                <ul>
                    <li>Guides in groups of more than 6 rooms will be receiving CPL with room and meals
                        for guides.</li>
                    <li>Guides in groups from 4 to 5 rooms must pay 25% of our SGL rack rates for the guide
                        package</li>
                    <li>Guides in groups from 1 to 3 rooms must pay 50% of our SGL rack rates for the guide
                        package</li>
                    <li>Guides in regular room of the hotel must pay regular net rates of the agency</li>
                </ul>
                <h2>
                    Policies of Payment for FIT's and Groups</h2>
                <p>
                    Due to the small size of our Hotel and to its remote location, the prepayment of
                    all reservation is necessary. The reservation of the services will be kept during
                    21 days after receiving the written request by fax or by electronic mail. A deposit
                    of 25% is required, in order to confirm the reservation, during this 21 days period,
                    or the reservation will be cancelled. The reservations are confirmed once 25% of
                    the total amount is received by our offices, via check, credit card or by means
                    of deposit in our bank account. The payment balance must be received by the hotel,
                    not later than 30 days before the arrival of the guests, in case of FIT's; and 60
                    days before the check-in of the guests in case of GROUPS. Reservations made within
                    30 days before (FIT'S) or 60 days earlier (groups) of the check in to the hotel,
                    will have two business days to confirm and pay or cancel without any surcharge.
                </p>
                <h2>
                    Bank Accounts for Payments</h2>
                <p>
                    Bank Account in dollars U.S. $ in the Bac de San Jose (BAC) # 90 58 87 964 Under
                    the name of: La Casa del Manat&iacute; S.A. Please send copy of deposit by fax to
                    the number 00(506) - 2239 - 4857 or by email info@manatuscostarica.com
                </p>
                <h2>
                    Cancellation Policies for Groups</h2>
                <p>
                    For purposes of these policies are understood as a group a booking of 3 or more
                    rooms for the same dates. Groups requiring more than 6 rooms are not admitted.
                    <br />
                    Cancellations of reserved groups will have the following penalties: The days are
                    counted as days before the check in of the clients.
                </p>
                <ul>
                    <li>Before 60 days - No charges</li>
                    <li>59 to 30 days 10% of the amount of the bookings</li>
                    <li>29 to 15 days 25% of the amount of the bookings</li>
                    <li>14 to 08 days 50% of the amount of the bookings</li>
                    <li>Less than 07 days, 100% of the amount of the bookings</li>
                    <li>No refunds for "no shows"</li>
                </ul>
                <h2>
                    No refunds for "no shows"
                </h2>
                <p>
                    Cancellations of FIT's or individual passengers will have the following penalties:
                    The days are counted as days before the check in of the clients
                </p>
                <ul>
                    <li>Before 30 days - without charges</li>
                    <li>29 to 15 days 10% of the amount of the booking</li>
                    <li>14 to 10 days 25% of the amount of the booking</li>
                    <li>09 to 04 days 50% of the amount of the booking</li>
                    <li>Less than 03 days, 100% of the amount of the booking</li>
                    <li>No refunds for "no shows"</li>
                </ul>
                <h2>
                    Refund policies for reservations cancellations</h2>
                <p>
                    Regarding the return money for paid and cancelled reservations, the hotel will only
                    refund if the reservations were made directly at the hotel and if the reason was
                    that the customer wasn&#8217;t able to travel.<br />
                    If the reservation was made through a travel agency that charges a commission, the
                    hotel will cancel the reservation but it won&#8217;t refund the money, it will issue a
                    credit for either the customer or the travel agency staff.<br />
                    TThe Hotel&#8217;s booking and account department will send a memo via email or fax to
                    the travel agency with the amount that will apply in favor of the agency. This credit
                    is valid for a year.<br />
                    Terms of payment and reimbursement in case of cancellation of booking by the hotel:
                    <br />
                </p>
                <h2>
                    Terms of payment and reimbursement in case of cancellation of booking by the hotel:</h2>
                <p>
                    In case of natural emergencies, strikes, landslides or situations beyond the control
                    of the Hotel and there is no possibility of providing the service both the transportation
                    and accommodation.<br />
                    THE HOTEL, reserves the right to return 70% of the reservation, the remaining 30%
                    of it is to cover operating expenses. Returns to the agencies shall be made through
                    letters of credit by the amount corresponding to 70% net of the booking, if it is
                    a direct booking; the hotel covers 70% of the reservation in cash within 24 hours.<br />
                    If for reasons attributable to the hotel, it will assume the cost of lodging, meals,
                    transportation and derivatives (over a period of time equivalent to the reservation
                    made at the HOTEL), until the Hotel is able to provide the service booked.
                </p>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnl_terminospaquete" runat="server" ScrollBars="Vertical" Height="400px"
            Visible="false">
            <div class="textoTerminos">
                <center>
                    <h1>
                        Terms and Conditions</h1>
                </center>
                <ul>
                    <li>Free-Flight offer, valid for the months of September, October and November.</li>
                    <li>Nesting-Tour offer free,valid for the months of September and October.</li>
                    <li>Promotion is not applicable with other offers.</li>
                    <li>Flight one way for the trip, (San Jose-Tortuguero) through Nature Air.</li>
                    <li>Does not apply to the return flight(Tortuguero-San Jose).</li>
                    <li>Prices and promotions of independent use.</li>
                    <li>Applicable only through direct bookings,online at our website (www.manatuscostarica.com)</li>
                    <li>Does not include other aspects that are not described in the promotional poster.</li>
                    <li>Does not apply to telephone bookings or through agencies.</li>
                </ul>
                <h2>
                    Flight Conditions:</h2>
                <ul>
                    <li>The weight limit on baggage to check for each person is 27lbs.</li>
                    <li>The weight limit on hand luggage is of 10lbs.<br />
                        Do not allow cancellations.</li>
                    <li>Changes in schedule must be requested 24 hours prior to departure.</li>
                    <li>Penalty for changes is $40 per person each way.</li>
                    <li>Flights are subject to availability</li>
                </ul>
                <br />
                <br />
            </div>
        </asp:Panel>
    </div>
</body>
</html>
