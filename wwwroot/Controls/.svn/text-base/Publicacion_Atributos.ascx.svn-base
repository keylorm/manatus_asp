<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Publicacion_Atributos.ascx.vb"
    Inherits="Controls_Publicacion_Atributos" %>
<table class="Container" cellspacing="0" cellpadding="0" border="0">
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
                Otros Atributos</h2>
        </td>
        <td>
            <asp:ImageButton ID="btn_OtrosAtributos" runat="server" ToolTip="Manejar Otros Atributos"
                ImageUrl="../iconos/agregar_small.png"></asp:ImageButton>
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <asp:UpdatePanel ID="upd_Atributos" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:DataGrid ID="dg_Atributos" runat="server" AutoGenerateColumns="False" CellPadding="0"
                        CssClass="tableClean" ShowFooter="false" ShowHeader="false" GridLines="None">
                        <Columns>
                            <asp:TemplateColumn HeaderText="Nombre">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Atributo" runat="server"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="20%" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:Image ID="img_Info" runat="server" ImageUrl="../images/icons/info.gif" />
                                </ItemTemplate>
                                <ItemStyle Width="5%" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Valor">
                                <ItemTemplate>
                                    <asp:DataGrid ID="dg_Valores" runat="server" Width="100%" AutoGenerateColumns="False"
                                        CellPadding="0" CssClass="tableClean" ShowFooter="false" ShowHeader="false" GridLines="None">
                                        <Columns>
                                            <asp:TemplateColumn HeaderText="Valor">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="tbx_Atributos_Valor" runat="server"></asp:TextBox>
                                                    <asp:DropDownList ID="ddl_Atributos_Valores" runat="server">
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                                <ItemStyle Width="95%" />
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Visible">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chk_Atributo_Visible" runat="server" />
                                                </ItemTemplate>
                                                <ItemStyle Width="5%" />
                                            </asp:TemplateColumn>
                                        </Columns>
                                    </asp:DataGrid>
                                </ItemTemplate>
                                <ItemStyle Width="70%" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Valor">
                                <ItemTemplate>
                                    <asp:HyperLink ID="lnk_OtroAtributo" runat="server" ToolTip="Agregar otro valor">Otro</asp:HyperLink>
                                </ItemTemplate>
                                <ItemStyle Width="5%" />
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>
                    <br />
                </ContentTemplate>
            </asp:UpdatePanel>
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
