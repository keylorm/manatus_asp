<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Ubicacion.ascx.vb" Inherits="EntidadUbicacion" %>
<asp:UpdatePanel ID="upd_Ubicacion" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <div>
            <label>
                Ubicacion</label>
            <asp:HyperLink ID="lnk_Ubicacion" runat="server" Text="Sin Asignar"></asp:HyperLink>
            <asp:CustomValidator ID="vld_Ubicacion" runat="server" Display="Dynamic" ErrorMessage="<%$ Resources:Orbecatalog_Resources, Campo_Requerido %>"></asp:CustomValidator>
            <asp:ImageButton ID="btn_AgregarUbicacion" runat="server" ToolTip="Ingresar nueva Ubicacion"
                ImageUrl="~/Orbecatalog/iconos/agregar_small.png"></asp:ImageButton>
        </div>
        <div>
            <label>
                &nbsp;</label>
            <asp:TreeView ID="trv_Ubicaciones" runat="server" Style="display: none">
            </asp:TreeView>
        </div>
        <div style="clear: both">
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
