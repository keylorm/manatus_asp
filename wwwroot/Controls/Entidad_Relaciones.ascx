<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Entidad_Relaciones.ascx.vb"
    Inherits="Controls_Entidad_Relaciones" %>
<asp:UpdatePanel ID="upd_relaciones" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <table cellspacing="0" cellpadding="0" border="0" style="width: 100%">
            <tr>
                <td style="width: 25%;">
                    &nbsp;
                </td>
                <td style="width: 70%;">
                </td>
                <td style="width: 5%;">
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <h2>
                        Relaciones</h2>
                </td>
                <td>
                    <asp:ImageButton ID="btn_Relaciones" runat="server" ToolTip="Ingresar nueva Relación"
                        ImageUrl="~/Orbecatalog/images/buttons/agregar.gif" Width="13" Height="11"></asp:ImageButton>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:DataGrid ID="dg_Relaciones" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        PageSize="10" Width="100%">
                        <AlternatingItemStyle CssClass="tablaResultados_Alternate" />
                        <ItemStyle CssClass="tablaResultados_Item" />
                        <HeaderStyle CssClass="tablaResultados_Header" />
                        <FooterStyle CssClass="tablaResultados_Footer" />
                        <PagerStyle Mode="NumericPages" CssClass="tablaResultados_Pager" />
                        <Columns>
                            <asp:TemplateColumn HeaderText="Relacion">
                                <ItemTemplate>
                                    <asp:HyperLink ID="lnk_Relacion" runat="server">Editar</asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Relacion">
                                <ItemTemplate>
                                    <asp:HyperLink ID="lnk_Entidad_Dependiente" runat="server">Dependiente</asp:HyperLink>
                                    <asp:Label ID="lbl_Relacion_Entidades" runat="server" Text="es ... de"></asp:Label>
                                    <asp:HyperLink ID="lnk_Entidad_Base" runat="server">Base</asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
                <td>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
