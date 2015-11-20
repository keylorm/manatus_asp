<%@ Page Language="VB" AutoEventWireup="false" CodeFile="pagError500.aspx.vb" Inherits="pagError500" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Manatus Hotel Tortuguero Costa Rica, finest accommodations Tortuguero, vip services,
        Tortuguero Best Hotel</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <meta name="y_key" content="64cc9486b65ce6b5" />
    <meta name="revisit-after" content="8 days" />
    <meta name="GOOGLEBOT" content="INDEX, FOLLOW" />
    <meta name="author" content="Poveda" />
    <meta http-equiv="Content-Language" content="English" />
    <meta name="keywords" content="Manatus Hotel, Tortuguero Costa Rica, tortuguero, Adventure tours, eco-tourism costa rica, romantic getaway, wedding in costa rica, vip services, best rates, turtle nesting tours, tortuguero finest accommodations, vip hotel costa rica, Costa rica vacation packages, Costa Rica romantic honeymoon, adventure tours, vip hotel services, costa rica vip hotels, spa tortuguero, massages, spa, costa rica hotel, costa rica hotels, tortuguero hotel, tortuguero hotels, hotels tortuguero, hotel tortuguero, tortuguero costa rica hotels, Tortuguero National Park Costa Rica, Canales de Tortuguero, Costa Rica jungle, Tortuguero navigation channels, Fishing tour, canopy tour, bird watching tour, turtle nesting tour, Sarapiqui river fishing tour, San Juan River fishing tour, Barra del Colorado fishing tour" />
    <meta name="description" content="Manatus Hotel is a beautiful and romantic hotel in Tortuguero Costa Rica where you can enjoy the finest accommodations with vip hotel services and the best tropical tours through the costarican jungle and the Tortuguero navigation channels" />
    <link href="styles/styles_new.css" rel="stylesheet" type="text/css" />
    <!--[if lt IE 7]>
        <script defer type="text/javascript" src="Scripts/pngfix.js"></script>
    <![endif]-->
</head>
<body style="background-color:#fffcf1">
    <form id="form1" runat="server">
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td>&nbsp;
                
            </td>
            <td class="Main">
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            <a href="http://manatuscostarica.com/" title="Main Page Manatus Hotel Tortuguero Costa Rica">
                                <img src="http://manatuscostarica.com/sites/files/manatus/logo.png" alt="Main Page Manatus Hotel Tortuguero Costa Rica" />
                            </a>
                        </td>
                        <td class="alignRight width262">
                            <div class="colorGris">
                                <div class="colorRojo">
                                    
                                </div>
                                <span style="color:#934221">MANATUS HOTEL, TORTUGUERO COSTA RICA | RESERVATIONS: (506) 2239.7364 |<br />HOTEL: (506) 2709.8197</span></div>
                            <div class="colorRojo">
                                Email: <a href="mailto:info@manatuscostarica.com">info@manatuscostarica.com</a>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
            <td>&nbsp;
                
            </td>
        </tr>
        <tr>
            <td class="">
             &nbsp;                
            </td>
            <td class="Main">
                
                <div class="content" style="background-color:White;border:2px solid #f2e3da">
                    <table cellpadding="0" cellspacing="2" width="100%">
                        <tr>
                            <td valign="top">
                                <div class="columnaIzq paddingRight7">
                                  
                                </div>
                            </td>
                            <td class="width738">
                                <div class="trebuchet">
                                    <table width="100%">
                                        <tr>
                                            <td valign="top">
                                                <asp:Panel ID="pnl_msg" runat="server">
                                                    <p>
                                                        <asp:Label ID="lbl_msg" runat="server" Text="There was an error, please tell us what happened." ForeColor="#934221"></asp:Label>
                                                    </p>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">
                                                <asp:Panel ID="pnl_formulario" runat="server">
                                                    <div>
                                                        <table>
                                                            <tr>
                                                                <td valign="top" class="paddingRight10">
                                                                    <asp:Label ID="lbl_msg2" runat="server" Text="Message:" ForeColor="#934221"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txt_comentario" runat="server" TextMode="MultiLine" Width="290px"
                                                                        Height="91px" BorderColor="#CCCCCC" BorderStyle="Solid" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                </td>
                                                                <td>
                                                                    <table id="Table1" cellspacing="0" cellpadding="0" runat="server" width="100%">
                                                                        <tr>
                                                                            <td>
                                                                                <div>
                                                                                    <div class="paddingRigth7 paddingTop8">
                                                                                        <div class="botonRojo2-New">
                                                                                            <a href="http://manatuscostarica.com/">Home &raquo;</a>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </td>
                                                                            <td>
                                                                                <div class="floatRight paddingRigth7 paddingTop8">
                                                                                    <div class="botonRojo2-New">
                                                                                        <asp:LinkButton ID="lnkbtn_enviar" runat="server">Send &raquo;</asp:LinkButton>
                                                                                    </div>
                                                                                </div>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </asp:Panel>
                                                <asp:Panel ID="pnl_mensaje" runat="server" Visible="false">
                                                    <div>
                                                        <br />
                                                        <asp:Label ID="lbl_mensaje2" runat="server" Text="Label" Visible="true" ForeColor="#934221"></asp:Label>
                                                        <br />
                                                        <br />
                                                        <div>
                                                            <asp:HyperLink ID="HyperLink1" runat="server" ForeColor="#832800" NavigateUrl="http://manatuscostarica.com/"
                                                                Text="Home"></asp:HyperLink>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <br />
                                <br />
                                <br />
                                <br />
                                
                            </td>
                        </tr>
                      
                    </table>
                </div>
               
            </td>
            <td class="">&nbsp;
                
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
