<%@ Page Language="VB" MasterPageFile="../OrbeCatalog.master" AutoEventWireup="false"
    CodeFile="Envio_Imagen.aspx.vb" Inherits="_Envio_Imagen" Title="Orbe Catalog - Envio Imagen" %>


<asp:Content ID="ctn_TituloSeccion" ContentPlaceHolderID="cph_Titulo" runat="Server">
    Envio Imagen
</asp:Content>
<asp:Content ID="ctn_Contenido" ContentPlaceHolderID="cph_Contenido" runat="Server">
    <h1>
        Archivo Excel</h1>
    <table class="Container" cellspacing="0" cellpadding="0" border="0" width="100%">
        <tr>
            <td style="width: 25%;">
                Archivo
            </td>
            <td style="width: 70%;">
                <asp:FileUpload ID="upl_Archivo" runat="server" />
            </td>
            <td style="width: 5%">
            </td>
        </tr>
        <tr>
            <td>
                Nombre Hoja
            </td>
            <td>
                <asp:TextBox ID="tbx_upl_NombreHoja" runat="server" Text="Sheet1"></asp:TextBox>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <h2>
                    Informacion</h2>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                Nombre
            </td>
            <td>
                <asp:TextBox ID="tbx_upl_NombreEntidad" runat="server" Width="25px"></asp:TextBox>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                Primer Apellido
            </td>
            <td>
                <asp:TextBox ID="tbx_upl_Apellido" runat="server" Width="25px"></asp:TextBox>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                Segundo Apellido
            </td>
            <td>
                <asp:TextBox ID="tbx_upl_Apellido2" runat="server" Width="25px"></asp:TextBox>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                Email
            </td>
            <td>
                <asp:TextBox ID="tbx_upl_Email" runat="server" Width="25px"></asp:TextBox>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <h1>
                    Imagen</h1>
            </td>
        </tr>
        <tr>
            <td>
                La Imagen
            </td>
            <td>
                <asp:FileUpload ID="upl_ImagenCorreo" runat="server" />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <h1>
                    Correo</h1>
            </td>
        </tr>
        <tr>
            <td>
                Nombre del Sender
            </td>
            <td>
                <asp:TextBox ID="tbx_SenderName" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Email del Sender
            </td>
            <td>
                <asp:TextBox ID="tbx_SenderEmail" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Email de reply
            </td>
            <td>
                <asp:TextBox ID="tbx_ReplyTo" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Subject
            </td>
            <td>
                <asp:TextBox ID="tbx_SubjectEnvio" runat="server"></asp:TextBox>
            </td>
        </tr>
    </table>
    <h1>
        Autentificacion</h1>
    <table class="Container" cellspacing="0" cellpadding="0" border="0" width="100%">
        <tr>
            <td style="width: 25%;">
                Servidor SMTP
            </td>
            <td style="width: 70%;">
                <asp:TextBox ID="tbx_ServerName" runat="server"></asp:TextBox>
            </td>
            <td style="width: 5%">
            </td>
        </tr>
        <tr>
            <td>
                Usuario SMTP
            </td>
            <td>
                <asp:TextBox ID="tbx_SMTPUsername" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Password SMTP
            </td>
            <td>
                <asp:TextBox ID="tbx_SMTPPassword" runat="server" TextMode="Password"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <table id="tablaBotones" class="botonesAcciones" cellspacing="0" cellpadding="0" border="0">
                    <tr>
                        <td>
                            <orbelink:ActionButton ID="btn_Salvar" CausesValidation="true" ImageURL="~/Orbecatalog/iconos/salvar_small.png"
                        runat="server" Text="<%$ Resources:Orbecatalog_Resources, Salvar %>" CssClass="positive">
                    </orbelink:ActionButton>
                        </td>
                        <td><orbelink:ActionButton ID="btn_Cancelar" ImageURL="~/Orbecatalog/iconos/cancelar_small.png"
                        runat="server" Text="<%$ Resources:Orbecatalog_Resources, Cancelar %>">
                    </orbelink:ActionButton></td>
                    </tr>
                </table>
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="cph_Busqueda" ContentPlaceHolderID="cph_ColumnaDerecha" runat="Server">
    <asp:Panel ID="pnl_Reporte" runat="server" Width="100%">
        <h1>
            Reporte de Envios
        </h1>
        <div class="Container">
            <asp:Literal ID="ltl_ScriptResponse" runat="server"></asp:Literal>
            <div id="div_Response" runat="server">
            </div>
            <asp:HyperLink ID="lnk_StopSending" runat="server">Parar envio</asp:HyperLink>
        </div>
    </asp:Panel>
</asp:Content>

