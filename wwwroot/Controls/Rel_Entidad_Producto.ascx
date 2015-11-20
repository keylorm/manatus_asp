<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Rel_Entidad_Producto.ascx.vb" Inherits="Control_Relaciones_Entidad_Producto" %>
<asp:UpdatePanel runat="server" ID="udp_productos" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:Panel ID="pnl_rel" runat="server">
            <asp:UpdatePanel runat="server" ID="udp_rel_productos" UpdateMode="Conditional">
                <ContentTemplate>
                    <h1>
                        <asp:Image ID="img_producto" ImageUrl="~/Orbecatalog/iconos/Modulos/pr_med.png" runat="server" />
                        <asp:Label ID="lbl_relacionado" runat="server" Text="Productos"></asp:Label>
                    </h1>
                    <div class="Container">
                        <asp:DataList ID="dl_Rel_producto_entidad" runat="server">
                            <ItemTemplate>
                                <asp:Label ID="lbl_relacion" runat="server" Text=""></asp:Label>
                            </ItemTemplate>
                        </asp:DataList>
                        <asp:Label ID="lbl_vacio" runat="server" Visible="false" Text="No contiene relaciones"></asp:Label><br />
                        <asp:HyperLink ID="hyl_agregarRelProducto" Visible="false" Style="padding-left: 130px"
                            runat="server">+ Agregar Relacion</asp:HyperLink>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
