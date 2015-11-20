<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Rel_Producto_Entidad.ascx.vb"
    Inherits="Controls_Rel_Producto_Entidad" %>
<asp:UpdatePanel runat="server" ID="udp_producto_entidad" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:Panel ID="pnl_rel" runat="server">
            <asp:UpdatePanel runat="server" ID="udp_rel_producto_entidad" UpdateMode="Conditional">
                <ContentTemplate>
                    <h1>
                        <asp:Image ID="img_entidad" ImageUrl="~/Orbecatalog/iconos/Modulos/co_med.png" runat="server" />
                        <asp:Label ID="lbl_relacionado" runat="server" Text="Entidades"></asp:Label></h1>
                    <div class="Container">
                        <asp:DataList ID="dl_Rel_producto_entidad" runat="server">
                            <ItemTemplate>
                                <asp:Label ID="lbl_relacion" runat="server" Text=""></asp:Label>
                            </ItemTemplate>
                        </asp:DataList>
                        <asp:Label ID="lbl_vacio" runat="server" Visible="false" Text="No contiene relaciones"></asp:Label><br />
                        <asp:HyperLink ID="hyl_agregarRelProducto_Entidad" Visible="false" Style="padding-left: 130px"
                            runat="server">+ Agregar Relacion</asp:HyperLink>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
