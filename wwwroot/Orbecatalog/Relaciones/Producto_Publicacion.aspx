<%@ Page Language="VB" MasterPageFile="~/Orbecatalog/popUp.master" AutoEventWireup="false"
    CodeFile="Producto_Publicacion.aspx.vb" Inherits="Orbecatalog_Relaciones_Producto_Publicacion"
    Title="Orbe Catalog Producto-Publicacion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_Titulo" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_Contenido" runat="Server">
    <asp:Panel ID="pnl_Producto_Publicacion" runat="server">
        <div class="Container_popUp">
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 95%">
                    </td>
                    <td style="width: 5%">
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <h1>
                            <asp:Label ID="lbl_titulo" runat="server" Text=""></asp:Label>
                        </h1>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                    <b>Relacion:</b><br /><br />
                        <asp:Label ID="lbl_nombreRelacionado" runat="server" Text="" Width="33%"></asp:Label>
                        <asp:DropDownList ID="ddl_TipoRelacion" runat="server" Width="33%">
                        </asp:DropDownList>
                        <asp:DropDownList ID="ddl_Relacionados" runat="server" Width="33%">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <br />
                        <asp:Button ID="btn_Agregar" runat="server" Text="Agregar" Width="100px" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <h2>
                            Actuales</h2>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:ListBox ID="lst_Producto_Publicacion" runat="server"></asp:ListBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <br />
                        <asp:Button ID="btn_Eliminar" runat="server" Text="Quitar" Width="100px" />
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_Acciones" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cph_Navegacion" runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cph_BusquedaNueva" runat="Server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="cph_ColumnaDerecha" runat="Server">
</asp:Content>
