<%@ Control Language="VB" AutoEventWireup="false" CodeFile="DireccionesProducto.ascx.vb"
    Inherits="Controls_DireccionesProducto" %>
<asp:UpdatePanel ID="upd_Direcciones" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:DataGrid ID="dg_Puntos" runat="server" AutoGenerateColumns="False" CssClass="tablaResultados">
            <AlternatingItemStyle CssClass="tablaResultados_Alternate" />
            <ItemStyle CssClass="tablaResultados_Item" />
            <HeaderStyle CssClass="tablaResultados_Header" />
            <FooterStyle CssClass="tablaResultados_Footer" />
            <PagerStyle Mode="NumericPages" CssClass="tablaResultados_Pager" />
            <Columns>
                <asp:TemplateColumn HeaderText="Direccion">
                    <ItemTemplate>
                        <asp:TextBox ID="tbx_Nombre" runat="server"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="Descripcion">
                    <ItemTemplate>
                        <asp:TextBox ID="tbx_Descripcion" runat="server"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="Coordenadas">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Latitud" runat="server"></asp:Label>,
                        <asp:Label ID="lbl_Longitud" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:ButtonColumn HeaderText="Eliminar" CommandName="Delete" Text="Delete">
                    <ItemStyle Width="35px" />
                </asp:ButtonColumn>
            </Columns>
        </asp:DataGrid>
        <br />
        <div class="botonesAcciones">
            <orbelink:ActionButton ID="btn_Salvar" CausesValidation="true" ImageURL="~/Orbecatalog/iconos/salvar_small.png"
                runat="server" Text="<%$ Resources:Orbecatalog_Resources, Salvar_Cambios %>" CssClass="positive">
            </orbelink:ActionButton>
            <orbelink:ActionButton ID="btn_Cancelar" ImageURL="~/Orbecatalog/iconos/cancelar_small.png"
                runat="server" Text="<%$ Resources:Orbecatalog_Resources, Cancelar %>">
            </orbelink:ActionButton>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
