<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Rel_Publicacion_Entidad.ascx.vb" Inherits="Controls_Rel_Publicacion_Entidad" %>

<asp:UpdatePanel runat="server" ID="udp_publicacion_entidad" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:Panel ID="pnl_rel" runat="server">
            <asp:UpdatePanel runat="server" ID="udp_rel_publicacion_entidad" UpdateMode="Conditional">
                <ContentTemplate>
                    <h1>
                        <asp:Image ID="img_entidad" ImageUrl="~/Orbecatalog/iconos/Modulos/co_med.png" runat="server" />
                        <asp:Label ID="lbl_relacionado" runat="server" Text="Entidades"></asp:Label></h1>
                    <div class="Container">
                        <asp:DataList ID="dl_Rel_publicacion_entidad" runat="server">
                            <ItemTemplate>
                                <asp:Label ID="lbl_relacion" runat="server" Text=""></asp:Label>
                            </ItemTemplate>
                        </asp:DataList>
                        <asp:Label ID="lbl_vacio" runat="server" Visible="false" Text="No contiene relaciones"></asp:Label><br />
                        <asp:HyperLink ID="hyl_agregarRelPublicacion_Entidad" Visible="false" Style="padding-left: 130px"
                            runat="server">+ Agregar Relacion</asp:HyperLink>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>