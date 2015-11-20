<%@ Page Language="VB" MasterPageFile="../OrbeCatalog.master" AutoEventWireup="false"
    CodeFile="TipoProducto.aspx.vb" Inherits="_TipoProducto" Title="Orbe Catalog - Tipo Producto" %>

<asp:Content ID="ctn_TituloSeccion" ContentPlaceHolderID="cph_Titulo" runat="Server">
    <asp:Literal runat="server" Text="<%$ Resources:Productos_Resources, TipoProducto_Pantalla %>" />
</asp:Content>
<asp:Content ID="Atributos" ContentPlaceHolderID="cph_Contenido" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <h1>
                Tipo Producto</h1>
            <table class="Container" cellspacing="0" cellpadding="0" border="0">
                <tr>
                    <td style="width: 25%;">
                        Nombre
                    </td>
                    <td style="width: 70%;">
                        <asp:TextBox ID="tbx_NombreTipoProducto" runat="server"></asp:TextBox>
                    </td>
                    <td style="width: 5%">
                        <asp:RequiredFieldValidator ID="vld_tbx_NombreTipoProducto" runat="server" ControlToValidate="tbx_NombreTipoProducto"
                            Display="Dynamic" EnableClientScript="False" ErrorMessage="<%$ Resources:Orbecatalog_Resources, Campo_Requerido %>"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        Codigo
                    </td>
                    <td>
                        <asp:TextBox ID="tbx_Codigo" runat="server"></asp:TextBox>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        Tipo Producto Perteneciente
                    </td>
                    <td>
                        <asp:HyperLink ID="lnk_Perteneciente" runat="server">(No perteneciente, primer nivel)</asp:HyperLink>
                        <asp:TreeView ID="trv_Perteneciente" runat="server" Style="display: none">
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="cph_Busqueda" ContentPlaceHolderID="cph_ColumnaDerecha" runat="Server">
    <asp:UpdatePanel ID="upd_TipoProductos" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnl_Enlaces" runat="server">
                <h1>
                    Enlaces</h1>
                <div class="Container">
                    <asp:HyperLink ID="lnk_Archivo" runat="server">Archivo de este tipo producto</asp:HyperLink><br />
                </div>
            </asp:Panel>
            <h1>
                Tipo Producto Actuales</h1>
            <div class="Container">
                <asp:TreeView ID="trv_TipoProducto" runat="server">
                </asp:TreeView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
