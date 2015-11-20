<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Rel_Producto_Publicacion.ascx.vb"
    Inherits="Controls_Rel_Producto_Publicacion" %>
<asp:UpdatePanel runat="server" ID="udp_producto_publicacion" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:Panel ID="pnl_rel" runat="server">
            <asp:UpdatePanel runat="server" ID="udp_rel_producto_publicacion" UpdateMode="Conditional">
                <ContentTemplate>
                    <h1>
                        <asp:Image ID="img_publicacion" ImageUrl="~/Orbecatalog/iconos/Modulos/pu_med.png" runat="server" />
                        <asp:Label ID="lbl_relacionado" runat="server" Text="Publicaciones"></asp:Label></h1>
                    <div class="Container">
                        <asp:DataList ID="dl_Rel_producto_publicacion" runat="server">
                            <ItemTemplate>
                                <asp:Label ID="lbl_relacion" runat="server" Text=""></asp:Label>
                            </ItemTemplate>
                        </asp:DataList>
                        <asp:Label ID="lbl_vacio" runat="server" Visible="false" Text="No contiene relaciones"></asp:Label><br />
                        <asp:HyperLink ID="hyl_agregarRelProducto_publicacion" Visible="false" Style="padding-left: 130px"
                            runat="server">+ Agregar Relacion</asp:HyperLink>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
