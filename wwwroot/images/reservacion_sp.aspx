<%@ Page Language="VB" AutoEventWireup="false" CodeFile="reservacion_sp.aspx.vb"
    Inherits="reservacion_sp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Reservaciones Hotel Manatus, reservaciones on-line, reservas, hotel Tortuguero
    </title>
    <link rel="shortcut icon" href="images/images2/favicon.ico" />
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
window.$zopim||(function(d,s){var z=$zopim=function(c){z._.push(c)},$=z.s=
d.createElement(s),e=d.getElementsByTagName(s)[0];z.set=function(o){z.set.
_.push(o)};z._=[];z.set._=[];$.async=!0;$.setAttribute('charset','utf-8');
$.src='//cdn.zopim.com/?1FZ3HqPURg49wsdlOVwgJorSzjrXqLEZ';z.t=+new Date;$.
type='text/javascript';e.parentNode.insertBefore($,e)})(document,'script');
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
</head>
<body class="reservation-form es">
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
					<li class="active-trail"><a href="http://booking.manatuscostarica.com/reservacion_sp.aspx">Reservaciones</a></li>
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
			<div id="page-head">
				<div class="page-title">
					<h1 title="Reservaciones On-line, reservas en el hotel Manatus" class="h1ReservaNew">RESERVACIÓN</h1>
				</div>
				<div class="breadcrumbs">
                     <a href="http://manatuscostarica.com/es/" title="P&aacute;gina principal Hotel Manatus Tortuguero Costa Rica">Inicio</a> / Reservación
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
                                                        <h2>HAGA su reservación HOY</h2>
                                                    </div>
                                                    <div>
                                                        <div class="fondoGrisReserva">
                                                            <div class="paddingReserva2">
                                                                <asp:Panel ID="panel" runat="server" Visible="true">
                                                                    <asp:Panel ID="pnl_exito" runat="server" Visible="false">
                                                                        <center>
                                                                            <br />
                                                                            <br />
                                                                            <asp:Label ID="lbl_exito" runat="server" Text="" Visible="true"></asp:Label>
                                                                            <asp:HyperLink ID="hyl_inicio" ImageUrl="~/images/Otros/principal.gif" runat="server"></asp:HyperLink>
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
                                                                                
                                                                                    <asp:TextBox ID="txt_codPostal" runat="server" CssClass="textBoxNuevo"></asp:TextBox>
                                                                                    </div>
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
                                                                                                        <%--AutoPostBack="true" -> CODIGO PARA LA PROMOCION DEL DESCUENTO O DE VIAJE EN AVION--%>
                                                                                                        
                                                                                                                    <asp:TextBox runat="server" ID="txtDateEntrada" AutoPostBack="true" CssClass="fechasNuevas"></asp:TextBox>
                                                                                                                
                                                                                                                    &nbsp;
                                                                                                                    <obout:Calendar runat="server" ID="calendarEntrada" DatePickerMode="true" TextBoxId="txtDateEntrada"
                                                                                                                        DatePickerImagePath="images/2014/bg-fecha-reservation.png" />
                                                                                                               
                                                                                                        <asp:RequiredFieldValidator ID="rfv_fecha" runat="server" ControlToValidate="txtDateEntrada"
                                                                                                            Display="Dynamic" ErrorMessage="Campo Requerido" ValidationGroup="registrese"></asp:RequiredFieldValidator>
                                                                                                    </ContentTemplate>
                                                                                                </asp:UpdatePanel>
                                                                                           
                                                                                               <div style="display: none;"> <asp:Label ID="lbl_fechaS" runat="server" Text="Fecha de Salida:"></asp:Label></div>
                                                                                            
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
                                                                                    <div id="traslado-box">        
                                                                                    <p>** Traslado</p>
                                                                                
                                                                                    <asp:RadioButtonList ID="rdbtnlist_transporte2014" runat="server" AutoPostBack="true">
                                                                                        <asp:ListItem Value="1">Traslado a Manatus</asp:ListItem>
                                                                                        <asp:ListItem Value="2">Traslado a San José</asp:ListItem>
                                                                                        <asp:ListItem Value="3">Transporte completo</asp:ListItem>
                                                                                    </asp:RadioButtonList>
                                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="rdbtnlist_transporte2014"
                                                                                                            Display="Dynamic" ErrorMessage="Campo Requerido" ValidationGroup="registrese"></asp:RequiredFieldValidator>
                                                                                
                                                                                    <div style="display: none;"><p>Lugar donde se recoge</p></div>
                                                                                    <div ID="box_pickup" runat="server" visible="false">
                                                                                        <asp:TextBox ID="txt_pickup" runat="server" CssClass="textBoxNuevo"></asp:TextBox>
                                                                                    </div>
                                                                                    <div style="display: none;"><p>Lugar donde se deja</p></div>
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
                                                                                                        GridLines="None" Width="282px">
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
                                                                                
                                                                                <%-- class class="texto-terminos-condiciones-reser" de este td añadido por Keylor Mora para cambios mayo 2013 --%>
                                                                               
                                                                                <span class="terminosNuevo">
                                                                                    <asp:HyperLink ID="hyplnk_paquetes" runat="server" Visible="false" Text="T&eacute;rminos
                                                                                        y Condiciones »" class="poplight" Target="_blank"></asp:HyperLink>
                                                                                    <a href="#?w=620" rel="popup_code" class="poplight" id="old_terms" runat="server">&nbsp;T&eacute;rminos y Condiciones »</a> </span>
                                                                                    <br />
                                                                                    <asp:CheckBox ID="chkbox_terminos" runat="server" Text="He leido y acepto los T&eacute;rminos y Condiciones" /><br />
                                                                                    <asp:Label ID="lbl_terminos" runat="server" Text="*Debe de aceptar los T&eacute;rminos y Condiciones"
                                                                                        Visible="false" ForeColor="Red"></asp:Label>
                                                                                    <div id="popup_code" class="popup_block">
                                                                                        <a class="close" href="#">
                                                                                            <img alt="Close" title="" class="btn_close" src="images/close_pop.png"></a>
                                                                                        <br />
                                                                                        <asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical" Height="400px" Visible="true">
                                                                                            <div class="textoTerminos">
                                                                                                <center>
                                                                                                    <h1>
                                                                                                        T&eacute;rminos y Condiciones</h1>
                                                                                                    <span class="terminosNuevo"><a href="#terminos2014">(ver T&eacute;rminos y Condiciones
                                                                                                        2014)</a></span>
                                                                                                </center>
                                                                                                <h2>
                                                                                                    El paquete incluye</h2>
                                                                                                <ul>
                                                                                                    <li>Traslado terrestre: San Jos&eacute; - (Pavona/ Caño Blanco) - San Jos&eacute;</li>
                                                                                                    <li>Traslado en Bote: (Pavona/ Caño Blanco) - Tortuguero - (Pavona/ Caño Blanco)</li>
                                                                                                    <li>Hospedaje, alimentaci&oacute;n y excursiones con gu&iacute;a: recorrido por el pueblo
                                                                                                        y canales</li>
                                                                                                    <%-- <li>El paquete de m&aacute;s de 1 noche incluye la visita al parque - entrada no incluida
                                                                                                    </li>--%>
                                                                                                    <li>Desayuno a la ida y almuerzo al regreso </li>
                                                                                                    <li>Incluye todos los impuestos (NOTA: No incluye el impuesto ICT porque se elimin&oacute;)</li>
                                                                                                </ul>
                                                                                                <h2>
                                                                                                    No incluye</h2>
                                                                                                <ul>
                                                                                                    <li>Bebidas alcoh&oacute;licas, gaseosas, y agua embotellada</li>
                                                                                                    <li>Entrada al parque nacional USD $10,00 por pax (sujeto a cambios) </li>
                                                                                                    <li>Entrada al Museo de La Tortuga USD $1.00 por pax (sujeto a cambios) </li>
                                                                                                    <li>Tour nocturno de desove de tortugas (Jul-Oct) USD $35 por pax (sujeto a cambios)</li>
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
                                                                                                    Por favor enviar copia del dep&oacute;sito por fax al n&uacute;mero 00 (506) - 2239
                                                                                                    - 4857
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
                                                                                                </p>
                                                                                                <br />
                                                                                                <center>
                                                                                                    <a name="terminos2014"></a>
                                                                                                    <h1>
                                                                                                        T&eacute;rminos y Condiciones 2014</h1>
                                                                                                </center>
                                                                                                <h2>
                                                                                                    El paquete incluye</h2>
                                                                                                <ul>
                                                                                                    <li>Hospedaje y alimentaci&oacute;n, bebidas no incluidas</li>
                                                                                                    <li>2 Tours en el paquete 2 d&iacute;as y 1 noche: un tour por el pueblo de Tortuguero
                                                                                                        y un tour a los canales del Parque Nacional Tortuguero- entrada no incluida.</li>
                                                                                                    <li>3 Tours en el paquete de 3 d&iacute;as y 2 noches: un tour al pueblo de Tortuguero
                                                                                                        y dos tours a los canales del Parque Nacional– entrada no incluida.</li>
                                                                                                    <%-- <li>El paquete de m&aacute;s de 1 noche incluye la visita al parque - entrada no incluida
                                                                                                    </li>--%>
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
                                                                                                </p>
                                                                                            </div>
                                                                                        </asp:Panel>
                                                                                        <asp:Panel ID="pnl_terminospaquete" runat="server" ScrollBars="Vertical" Height="400px"
                                                                                            Visible="false">
                                                                                            <div class="textoTerminos">
                                                                                                <center>
                                                                                                    <h1 class="font22 fontVerde">
                                                                                                        T&eacute;rminos y condiciones de la oferta</h1>
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
                                                                               
                                                                                    <asp:Label ID="lbl_erroFechas" runat="server" Text="" ForeColor="red"></asp:Label>
                                                                                    <asp:Label ID="lbl_ResultadoReservacion" runat="server" Text=""></asp:Label>
                                                                             
                                                                    </asp:Panel>
                                                                </asp:Panel>
                                                                <br />
                                                                <p class="link-chat">*Si tiene una duda o consulta no dude en contactarnos a través de nuestro <span class="colorRojo"><a href="javascript: $zopim.livechat.window.show();">CHAT</a></span><br />
                                                                **Servicio sujeto a disponibilidad. El huesped debe contactar a hotel Manatus para definir el lugar donde se recoge y/o se deja antes de realizar la reservación.</p>
<br />
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="alignRight btn_reserva">
                                                    <%-- Comentado por Keylor para Cambios mayo 2013: SE CAMBIO EL COLOR DEL LINK #842702 POR BLANCO  --%>
                                                        <asp:LinkButton ID="btn_reservar" runat="server" ToolTip="Reservar" ValidationGroup="registrese"
                                                             ForeColor="#FFFFFF" Visible="false">RESERVAR »</asp:LinkButton>
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
                                                Oficina(506) 2239-7364<br />
                                                Hotel (506) 2709-8197<br />
                                                Fax (506) 2239-7366br />
                                                    Email: <a href="mailto:info@manatuscostarica.com">info@manatuscostarica.com</a>
                                                </p>
                                                <p>
                                                Para llamar a Costa Rica marque:
                                                <br />
                                                C&oacute;digo de Acceso Internacional + C&oacute;digo de Costa Rica (506) + (n&uacute;mero
                                                al que desea llamar) ( Pregunte en la compa&ntilde;&iacute;a telefonica de su pa&iacute;s
                                                cu&aacute;l es el C&oacute;digo de Acceso Internacional.)</p>
                                                <br />
                                                <h3 class="chat"><a href="javascript:%20$zopim.livechat.window.show();">CHAT WITH US</a></h3>
                                            </div>

                                            <ul class="menu-sidebar"><li class="first leaf tours-virtuales mid-960"><a title="" href="/manatus/es/virtual-tours/jardines">Tours Virtuales</a></li>
<li class="leaf galer-a-de-fotos-y-videos mid-962"><a title="" href="/manatus/es/gallery">Galería de Fotos y Videos</a></li>
<li class="last leaf de-nuestro-blog mid-964"><a title="" href="/manatus/es/blog">De nuestro Blog</a></li>
</ul>
 
                                </div>
                            
                </div>  
       </div>                      
       <div id="footer">
		        <div id="footer-inner" class="container-980">

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
                            <a href="#"><img src="images/2014/face.png" /></a><a href="#"><img src="images/2014/flickr.png" /></a><a href="#"><img src="images/2014/tripadvisor.png" /></a>
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
    
            </div>                             
                           
                

    <script type="text/javascript">



$('a.poplight[href^=#]').live("click",function() {

    var popID = $(this).attr('rel'); //Get Popup Name

    var popURL = $(this).attr('href'); //Get Popup href to define size

    $('#amazon-widget').css({

        'display' : "none"

    });

    //Pull Query & Variables from href URL

    var query= popURL.split('?');

    var dim= query[1].split('&');

    var popWidth = dim[0].split('=')[1]; //Gets the first query string value



    //Fade in the Popup and add close button

    $('#' + popID).fadeIn().css({ 'width': Number( popWidth ) }).prepend('<a href="#" class="close"><img src="images/close_pop.png" class="btn_close" title="" alt="Close" /></a>');



    //Define margin for center alignment (vertical   horizontal) - we add 80px to the height/width to accomodate for the padding  and border width defined in the css

    var popMargTop = ($('#' + popID).height() + 80) / 2;

    var popMargLeft = ($('#' + popID).width() + 80) / 2;



    //Apply Margin to Popup

    $('#' + popID).css({

        'margin-top' : -popMargTop,

        'margin-left' : -popMargLeft

    });



    //Fade in Background

    $('body').append('<div id="fade"></div>'); //Add the fade layer to bottom of the body tag.

    $('#fade').css({'filter' : 'alpha(opacity=80)'}).fadeIn(); //Fade in the fade layer - .css({'filter' : 'alpha(opacity=80)'}) is used to fix the IE Bug on fading transparencies 



    return false;

});



//Close Popups and Fade Layer

$('a.close, #fade').live('click', function() { //When clicking on the close or fade layer...

    $('#fade , .popup_block').fadeOut(function() {

        $('#fade, a.close').remove();  //fade them both out
        $('#amazon-widget').css({

        'display' : "block"

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
} catch(err) {}</script>

</body>
</html>
                                                                 