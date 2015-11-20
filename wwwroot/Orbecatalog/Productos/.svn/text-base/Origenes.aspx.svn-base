<%@ Page Language="VB" MasterPageFile="../OrbeCatalog.master" AutoEventWireup="false"
    CodeFile="Origenes.aspx.vb" Inherits="_Origenes" Title="Orbe Catalog - Origenes" %>

<asp:Content ID="ctn_TituloSeccion" ContentPlaceHolderID="cph_Titulo" runat="Server">
    <asp:Literal runat="server" Text="<%$ Resources:Productos_Resources, Origenes_Pantalla %>" />
</asp:Content>
<asp:Content ID="Atributos" ContentPlaceHolderID="cph_Contenido" runat="Server">
    <h1>
        Origen</h1>
    <table class="Container" cellspacing="0" cellpadding="0" border="0">
        <tr>
            <td style="width: 25%;">
                Nombre
            </td>
            <td style="width: 70%;">
                <asp:TextBox ID="tbx_NombreOrigenes" runat="server"></asp:TextBox>
            </td>
            <td style="width: 5%">
                <asp:RequiredFieldValidator ID="vld_tbx_NombreOrigenes" runat="server" ControlToValidate="tbx_NombreOrigenes"
                    Display="Dynamic" EnableClientScript="False" ErrorMessage="<%$ Resources:Orbecatalog_Resources, Campo_Requerido %>"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                Origen Perteneciente
            </td>
            <td>
                <asp:LinkButton ID="lbl_OrigenesPerteneciente" runat="server" Text="Sin Asignar"></asp:LinkButton><br />
                <asp:TreeView ID="trv_Perteneciente" runat="server">
                </asp:TreeView>
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
            <td>
                <div class="botonesAcciones">
                    <label>
                        &nbsp;
                    </label>
                    <orbelink:ActionButton ID="btn_Salvar" CausesValidation="true" ImageURL="~/Orbecatalog/iconos/salvar_small.png"
                        runat="server" Text="<%$ Resources:Orbecatalog_Resources, Salvar %>" CssClass="positive">
                    </orbelink:ActionButton>
                    <orbelink:ActionButton ID="btn_Modificar" CausesValidation="true" ImageURL="~/Orbecatalog/iconos/modificar_small.png"
                        runat="server" Text="<%$ Resources:Orbecatalog_Resources, Salvar_Cambios %>"
                        CssClass="positive">
                    </orbelink:ActionButton>
                    <orbelink:ActionButton ID="btn_Cancelar" ImageURL="~/Orbecatalog/iconos/cancelar_small.png"
                        runat="server" Text="<%$ Resources:Orbecatalog_Resources, Cancelar %>">
                    </orbelink:ActionButton>
                    <orbelink:ActionButton ID="btn_Eliminar" ImageURL="~/Orbecatalog/iconos/eliminar_small.png"
                        runat="server" Text="<%$ Resources:Orbecatalog_Resources, Eliminar %>" CssClass="negative">
                    </orbelink:ActionButton>
                </div>
            </td>
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="cph_Busqueda" ContentPlaceHolderID="cph_ColumnaDerecha" runat="Server">
    <asp:Panel ID="pnl_Enlaces" runat="server">
        <h1>
            Enlaces</h1>
        <div class="Container">
            <asp:HyperLink ID="lnk_Archivo" runat="server">Archivo de este origen</asp:HyperLink><br />
        </div>
    </asp:Panel>
    <h1>
        Origenes Actuales</h1>
    <div class="Container">
        <asp:TreeView ID="trv_Origenes" runat="server">
        </asp:TreeView>
    </div>
</asp:Content>
