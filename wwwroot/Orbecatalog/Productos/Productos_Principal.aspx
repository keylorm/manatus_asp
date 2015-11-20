<%@ Page Language="VB" MasterPageFile="../OrbeCatalogB.master" AutoEventWireup="false"
    CodeFile="Productos_Principal.aspx.vb" Inherits="ProductosPagPrinc" Title="Orbe Catalog - Productos de Pagina Principal" %>

<asp:Content ID="ctn_TituloSeccion" ContentPlaceHolderID="cph_Titulo" runat="Server">
    <asp:Literal runat="server" Text="<%$ Resources:Productos_Resources, PaginaPrincipal_Pantalla %>" />
</asp:Content>
<asp:Content ID="ctn_Contenido" ContentPlaceHolderID="cph_Contenido" runat="Server">
    <asp:UpdatePanel ID="upd_existentes" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table class="Container" cellspacing="0" cellpadding="0" border="0">
                <tr>
                    <td>
                        Productos Existentes y Activos
                    </td>
                    <td>
                    </td>
                    <td>
                        Productos en la página Principal
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:ListBox ID="lstbx_activos" runat="server" Width="200px" Height="400"></asp:ListBox>
                    </td>
                    <td>
                        <asp:Button ID="btn_agregar" runat="server" Text="->"></asp:Button><br />
                        <asp:Button ID="btn_borrar" runat="server" Text="<-"></asp:Button>
                    </td>
                    <td>
                        <asp:ListBox ID="lstbx_principal" runat="server" Width="200px" Height="400"></asp:ListBox>
                    </td>
                </tr>
            </table>
            <div class="botonesAcciones">
                <orbelink:ActionButton ID="btn_Salvar" CausesValidation="true" ImageURL="~/Orbecatalog/iconos/salvar_small.png"
                    runat="server" Text="<%$ Resources:Orbecatalog_Resources, Salvar_Cambios %>" CssClass="positive">
                </orbelink:ActionButton>
                <orbelink:ActionButton ID="btn_Cancelar" ImageURL="~/Orbecatalog/iconos/cancelar_small.png"
                    runat="server" Text="<%$ Resources:Orbecatalog_Resources, Cancelar %>">
                </orbelink:ActionButton>
                <orbelink:ActionButton ID="btn_Eliminar" ImageURL="~/Orbecatalog/iconos/eliminar_small.png"
                    runat="server" Text="Limpiar Principales" CssClass="negative">
                </orbelink:ActionButton>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
