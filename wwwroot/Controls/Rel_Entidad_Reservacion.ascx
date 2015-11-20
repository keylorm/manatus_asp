<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Rel_Entidad_Reservacion.ascx.vb"
    Inherits="Controls_Rel_Entidad_Reservacion" %>
<asp:UpdatePanel runat="server" ID="udp_entidad_publicacion" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:Panel ID="pnl_rel" runat="server">
            <asp:UpdatePanel runat="server" ID="udp_rel_entidad_reservacion" UpdateMode="Conditional">
                <ContentTemplate>
                    <h1>
                        <asp:Image ID="img_proyecto" ImageUrl="~/Orbecatalog/iconos/Modulos/rs_med.png" runat="server" />
                        <asp:Label ID="lbl_relacionado" runat="server" Text="Reservaciones"></asp:Label></h1>
                    <div class="Container">
                        <asp:DataList ID="dl_Rel_entidad_reservacion" runat="server">
                            <ItemTemplate>
                                <asp:HyperLink ID="hyl_descripcion" runat="server"></asp:HyperLink>
                            </ItemTemplate>
                        </asp:DataList>
                        <asp:Label ID="lbl_vacio" runat="server" Visible="false" Text="No contiene relaciones"></asp:Label><br />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
