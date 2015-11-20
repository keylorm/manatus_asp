<%@ Page Language="VB" MasterPageFile="OrbeCatalogB.master" AutoEventWireup="false"
    ValidateRequest="false" CodeFile="Pantallas.aspx.vb" Inherits="_Pantallas" Title="Orbe Catalog - Pantallas" %>

<asp:Content ID="ctn_TituloSeccion" ContentPlaceHolderID="cph_Titulo" runat="Server">
    Manejo de Pantallas
</asp:Content>
<asp:Content ID="ctn_Contenido" ContentPlaceHolderID="cph_Contenido" runat="Server">
    <asp:UpdatePanel ID="upd_Pantalla" runat="server">
        <ContentTemplate>
            <table class="Container" cellspacing="0" cellpadding="0" border="0" width="100%">
                <tr>
                    <td colspan="2">
                        <h2>
                            Pantallas por Perfil</h2>
                    </td>
                    <td>
                        <asp:ImageButton ID="btn_AgregarPerfil" runat="server" ToolTip="Ingresar nuevo Tipo Publicacion"
                            ImageUrl="images/buttons/agregar.gif" Width="13" Height="11"></asp:ImageButton>
                    </td>
                </tr>
                <tr>
                    <td>
                        Tipo Entidad
                    </td>
                    <td>
                        <asp:DropDownList ID="ddl_Perfil" runat="server" AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
            <br />
            <h2>
                Permisos</h2>
            <asp:DataGrid ID="dg_Pantallas" runat="server" AutoGenerateColumns="False" Width="100%"
                CssClass="Contenido_Table">
                <AlternatingItemStyle CssClass="tablaResultados_Alternate" />
                <ItemStyle CssClass="tablaResultados_Item" />
                <HeaderStyle CssClass="tablaResultados_Header" />
                <FooterStyle CssClass="tablaResultados_Footer" />
                <Columns>
                    <asp:TemplateColumn HeaderText="Pantalla">
                        <ItemTemplate>
                            <asp:Label ID="lbl_Pantalla" runat="server"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="60%" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <ItemTemplate>
                            <asp:CheckBox ID="chk_Leer" runat="server" />
                        </ItemTemplate>
                        <HeaderTemplate>
                            Entrar a Pantalla<br />
                            <asp:CheckBox ID="chk_TodosLeer" runat="server" OnCheckedChanged="chk_TodosLeer_CheckedChanged"
                                AutoPostBack="true" />
                        </HeaderTemplate>
                        <ItemStyle Width="10%" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <ItemTemplate>
                            <asp:CheckBox ID="chk_Crear" runat="server" />
                        </ItemTemplate>
                        <HeaderTemplate>
                            Salvar (Crear nuevos)<br />
                            <asp:CheckBox ID="chk_TodosCrear" runat="server" OnCheckedChanged="chk_TodosCrear_CheckedChanged"
                                AutoPostBack="true" />
                        </HeaderTemplate>
                        <ItemStyle Width="10%" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Modificar">
                        <ItemTemplate>
                            <asp:CheckBox ID="chk_Modificar" runat="server" />
                        </ItemTemplate>
                        <HeaderTemplate>
                            Modificar<br />
                            <asp:CheckBox ID="chk_TodosModificar" runat="server" OnCheckedChanged="chk_TodosModificar_CheckedChanged"
                                AutoPostBack="true" />
                        </HeaderTemplate>
                        <ItemStyle Width="10%" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <ItemTemplate>
                            <asp:CheckBox ID="chk_Borrar" runat="server" />
                        </ItemTemplate>
                        <HeaderTemplate>
                            Borrar<br />
                            <asp:CheckBox ID="chk_TodosBorrar" runat="server" OnCheckedChanged="chk_TodosBorrar_CheckedChanged"
                                AutoPostBack="true" />
                        </HeaderTemplate>
                        <ItemStyle Width="10%" />
                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid>
            <br />
            <div class="botonesAcciones">
                <orbelink:ActionButton ID="btn_Salvar" CausesValidation="true" ImageURL="~/Orbecatalog/iconos/salvar_small.png"
                    runat="server" Text="<%$ Resources:Orbecatalog_Resources, Salvar %>" CssClass="positive">
                </orbelink:ActionButton>
                <orbelink:ActionButton ID="btn_Cancelar" ImageURL="~/Orbecatalog/iconos/cancelar_small.png"
                    runat="server" Text="<%$ Resources:Orbecatalog_Resources, Cancelar %>">
                </orbelink:ActionButton>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
