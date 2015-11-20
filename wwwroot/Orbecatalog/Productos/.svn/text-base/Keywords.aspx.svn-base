<%@ Page Language="VB" MasterPageFile="../OrbeCatalog.master" AutoEventWireup="false"
    CodeFile="Keywords.aspx.vb" Inherits="_Keywords" Title="Orbe Catalog - Keywords" %>

<asp:Content ID="ctn_TituloSeccion" ContentPlaceHolderID="cph_Titulo" runat="Server">
    <asp:Literal runat="server" Text="<%$ Resources:Productos_Resources, Keywords_Pantalla %>" />
</asp:Content>
<asp:Content ID="ctn_Contenido" ContentPlaceHolderID="cph_Contenido" runat="Server">
    <h1>
        Keyword</h1>
    <asp:UpdatePanel ID="upd_Contenido" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table class="Container" cellspacing="0" cellpadding="0" border="0">
                <tr>
                    <td>
                        Nombre
                    </td>
                    <td>
                        <asp:TextBox ID="tbx_NombreKeyword" runat="server"></asp:TextBox>&nbsp;
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="vld_tbx_NombreKeyword" runat="server" ControlToValidate="tbx_NombreKeyword"
                            Display="Dynamic" EnableClientScript="False" ErrorMessage="<%$ Resources:Orbecatalog_Resources, Campo_Requerido %>"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        Visible
                    </td>
                    <td>
                        <asp:CheckBox ID="chk_Visible" runat="server" />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <div class="botonesAcciones">
                            <label>
                                &nbsp;
                            </label>
                            <orbelink:ActionButton ID="btn_Salvar" CausesValidation="true" ImageURL="~/Orbecatalog/iconos/salvar_small.png"
                                runat="server" Text="<%$ Resources:Orbecatalog_Resources, Salvar %>" CssClass="positive">
                            </orbelink:ActionButton>
                            <orbelink:ActionButton ID="btn_Modificar" CausesValidation="true" ImageURL="~/Orbecatalog/iconos/modificar_small.png"
                                runat="server" Text="<%$ Resources:Orbecatalog_Resources, Salvar_Cambios %>"
                                CssClass="positive">
                            </orbelink:ActionButton>
                            <orbelink:ActionButton ID="btn_Cancelar" ImageURL="~/Orbecatalog/iconos/cancelar_small.png"
                                runat="server" Text="<%$ Resources:Orbecatalog_Resources, Cancelar %>">
                            </orbelink:ActionButton>
                            <orbelink:ActionButton ID="btn_Eliminar" ImageURL="~/Orbecatalog/iconos/eliminar_small.png"
                                runat="server" Text="<%$ Resources:Orbecatalog_Resources, Eliminar %>" CssClass="negative">
                            </orbelink:ActionButton>
                        </div>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="cph_Busqueda" ContentPlaceHolderID="cph_ColumnaDerecha" runat="Server">
    <h1>
        Keywords Actuales</h1>
    <asp:UpdatePanel ID="upd_Busqueda" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="Container">
                <asp:DataGrid ID="dg_Keywords" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                    PageSize="10" CssClass="tablaResultados">
                    <AlternatingItemStyle CssClass="tablaResultados_Alternate" />
                    <ItemStyle CssClass="tablaResultados_Item" />
                    <HeaderStyle CssClass="tablaResultados_Header" />
                    <FooterStyle CssClass="tablaResultados_Footer" />
                    <PagerStyle Mode="NumericPages" CssClass="tablaResultados_Pager" />
                    <Columns>
                        <asp:EditCommandColumn CancelText="Cancel" EditText="Edit" UpdateText="Update">
                            <ItemStyle Width="15%" />
                        </asp:EditCommandColumn>
                        <asp:TemplateColumn HeaderText="Keyword">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Nombre" runat="server" Text="Label"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="55%" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Visible">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Visible" runat="server" Text="Label"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="30%" />
                        </asp:TemplateColumn>
                    </Columns>
                </asp:DataGrid>
                <asp:Label ID="lbl_ResultadoElementos" runat="server" Text="No hay elementos"></asp:Label>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
