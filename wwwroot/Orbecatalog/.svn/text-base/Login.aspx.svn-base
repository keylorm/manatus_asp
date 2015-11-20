<%@ Page Language="VB" MasterPageFile="OrbeCatalog.master" AutoEventWireup="false"
    CodeFile="Login.aspx.vb" Inherits="_Login" Title="Orbe Catalog - Login" %>

<asp:Content ID="ctn_TituloSeccion" ContentPlaceHolderID="cph_Titulo" runat="Server">
    <asp:Literal runat="server" Text="<%$ Resources:Orbecatalog_Resources, Login %>" />
</asp:Content>
<asp:Content ID="ctn_Contenido" ContentPlaceHolderID="cph_Contenido" runat="Server">
    <asp:Panel ID="pnl_Login" runat="server" DefaultButton="btn_Login">
        <div class="fieldset Container">
            <div>
                <label>
                    <asp:Literal runat="server" Text="<%$ Resources:Orbecatalog_Resources, Login_Usuario %>" />
                </label>
                <asp:TextBox ID="tbx_Username" runat="server"></asp:TextBox>
                <asp:HyperLink ID="lnk_Usuario1" runat="server" Text="Invitado"></asp:HyperLink>
            </div>
            <div>
                <label>
                    <asp:Literal runat="server" Text="<%$ Resources:Orbecatalog_Resources, Login_Password %>" />
                </label>
                <asp:TextBox ID="tbx_Password" runat="server" TextMode="Password"></asp:TextBox>
            </div>
            <div>
                <label>
                    <asp:Literal runat="server" Text="<%$ Resources:Orbecatalog_Resources, Idioma %>" />
                </label>
                <asp:DropDownList ID="ddl_Languages" runat="server">
                </asp:DropDownList>
            </div>
            <div>
                <label>
                    <asp:Literal runat="server" Text="<%$ Resources:Orbecatalog_Resources, TimeZone %>" />
                </label>
                <asp:DropDownList ID="ddl_timezones" runat="server">
                </asp:DropDownList>
            </div>
            <div>
                <label>
                </label>
                <asp:HyperLink ID="lnk_Recordar" runat="server" NavigateUrl="Login.aspx?action=remember"
                    Text="<%$ Resources:Orbecatalog_Resources, Login_OlvidoPassword %>"></asp:HyperLink>
            </div>
            <div>
                <label>
                    &nbsp;
                </label>
            </div>
            <div class="botonesAcciones">
                <orbelink:ActionButton ID="btn_Login" runat="server" Text="<%$ Resources:Orbecatalog_Resources, Login %>">
                </orbelink:ActionButton>
                <orbelink:ActionButton ID="btn_Cancelar" runat="server" Text="<%$ Resources:Orbecatalog_Resources, Cancelar %>">
                </orbelink:ActionButton>
                <orbelink:ActionButton ID="btn_Logout" runat="server" Text="<%$ Resources:Orbecatalog_Resources, Logout %>">
                </orbelink:ActionButton>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnl_Recordar" runat="server">
        <h2>
            <asp:Literal runat="server" Text="<%$ Resources:Orbecatalog_Resources, Login_RememberPassword %>" /></h2>
        <table cellspacing="0" cellpadding="0" border="0" width="100%">
            <tr>
                <td style="width: 25%;">
                    <asp:Literal runat="server" Text="<%$ Resources:Orbecatalog_Resources, Login_RemeberPasswordEmail %>" />
                </td>
                <td style="width: 70%;">
                    <asp:TextBox ID="tbx_Email" runat="server"></asp:TextBox>
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
                <td>
                </td>
                <td align="center">
                    <asp:Panel ID="pnl_BotonRecordar" runat="server">
                        <orbelink:ActionButton ID="btn_Recordar" runat="server" Text="<%$ Resources:Orbecatalog_Resources, Login_RemeberMe %>">
                        </orbelink:ActionButton>
                        <br />
                        <asp:Label ID="lbl_Recordar" runat="server" Text="Recordarme ¿?"></asp:Label>
                    </asp:Panel>
                </td>
                <td>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
<asp:Content ID="cph_Busqueda" ContentPlaceHolderID="cph_ColumnaDerecha" runat="Server">
</asp:Content>
