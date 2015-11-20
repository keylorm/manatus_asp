<%@ Page Language="VB" MasterPageFile="~/MEmails.master" AutoEventWireup="false"
    CodeFile="emails.aspx.vb" Inherits="emails" Title="" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table style="width:801px; text-align: justify; padding-top: 5px; padding-bottom: 10px; padding-left:25px; padding-right:25px;
        padding-top: 10px">
        <tr>
            <td width="801px">
                <br />
         
                <strong> Estimado (a) <br />
                    <asp:Label ID="lbl_usuario" runat="server" Text="Usuario_Replace"></asp:Label></strong><br />
                <br />
                <br />
                <table id="pnl_registro" runat="server" visible="false" cellpadding="0" cellspacing="0"
                    width="100%">
                    <tr>
                        <td style="text-align: justify">
                            Gracias por registrarse en 
                            <br />
                            <br />
                            La siguiente información es indispensable para ingresar a su cuenta en nuestro sistema,
                            le sugerimos que la mantenga en un lugar seguro y fácil de recordar.<br />
                            <br />
                            <br />
                            Usuario:
                            <asp:Label ID="lbl_email" runat="server" Text=""></asp:Label><br />
                            Password:
                            <asp:Label ID="lbl_password" runat="server" Text=""></asp:Label><br />
                            Para ingresar a su cuenta haga click en el siguiente link:
                            <asp:HyperLink ID="lnk_activarCuenta" runat="server" ForeColor="#BE202E"></asp:HyperLink>
                        </td>
                    </tr>
                </table>
                <table id="pnl_registroAdmin" runat="server" visible="false" cellpadding="0" cellspacing="0"
                    width="100%">
                    <tr>
                        <td style="text-align: justify">
                            Se ha registrado un nuevo usuario en 
                            <br />
                            <br />
                            La información de la cuenta es la siguiente.<br />
                            <br />
                            <br />
                            <b>Usuario:</b>
                            <asp:Label ID="lbl_NombreCuenta" runat="server" Text=""></asp:Label><br />
                            <b>Telefono:</b>
                            <asp:Label ID="lbl_TelefonoCuenta" runat="server" Text=""></asp:Label>
                            <br />
                            <br />
                        </td>
                    </tr>
                </table>
                <table id="pnl_contactenos" runat="server" visible="false" cellpadding="0" cellspacing="0"
                    width="100%">
                    <tr>
                        <td style="text-align: justify">
                            Nuevo mensaje de 
                            <br />
                            <br />
                            Nombre: Nombre_Replace
                            <br />
                            Email: Email_Replace
                            <br />      <br />
                            Mensaje: Mensaje_Replace
                        </td>
                    </tr>
                </table>
                <table id="pnl_enviarAmigo" runat="server" visible="false" cellpadding="0" cellspacing="0"
                    width="100%">
                    <tr>
                        <td style="text-align: left">
                        EnviarAmigo_Replace
                        </td>
                    </tr>
                </table>
                <table id="pnl_solicitarInfo" runat="server" visible="false" cellpadding="0" cellspacing="0"
                    width="100%">
                    <tr>
                        <td  style="text-align: left">
                              SolicitarInfo_Replace
                        </td>
                    </tr>
                </table>
                          <table id="pnl_faltura" runat="server" visible="false" cellpadding="0" cellspacing="0"
                    width="100%">
                    <tr>
                        <td  style="text-align: left">
                            Factura_Replace
                        </td>
                    </tr>
                </table>
                <br />
          
            
                
                <div id="div_link" runat ="server" visible ="false" >
                    Si no puede ver el contenido de este correo, puede copiar en su navegador la siguiente
                ruta:
                <br />
                <asp:HyperLink ID="lnk_Envio" runat="server" ForeColor="#BE202E" Text="email.aspx"  ></asp:HyperLink>
           
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
