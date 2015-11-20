<%@ Page Language="VB" MasterPageFile="../OrbeCatalog.master" AutoEventWireup="false"
    CodeFile="Atributos_TipoProducto.aspx.vb" Inherits="_Atributos_TipoProducto"
    Title="Orbe Catalog - Atributos por Tipo de Entidad" %>

<asp:Content ID="ctn_TituloSeccion" ContentPlaceHolderID="cph_Titulo" runat="Server">
    <asp:Literal runat="server" Text="<%$ Resources:Productos_Resources, AtributosTipoProducto_Pantalla %>" />
</asp:Content>
<asp:Content ID="ctn_Contenido" ContentPlaceHolderID="cph_Contenido" runat="Server">
    <asp:UpdatePanel ID="upd_Contenido" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table class="Container" cellspacing="0" cellpadding="0" border="0">
                <tr>
                    <td colspan="2">
                        <h2>
                            Tipo Producto</h2>
                    </td>
                    <td>
                        <asp:ImageButton ID="btn_AregarTipoProducto" runat="server" ToolTip="Ingresar nuevo tipo de Producto"
                            ImageUrl="../iconos/agregar_small.png" ></asp:ImageButton>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:HyperLink ID="lnk_TipoProducto" runat="server" Text="Sin Asignar"></asp:HyperLink>
                        <asp:CustomValidator ID="vld_TipoProducto" runat="server" Display="Dynamic" ErrorMessage="<%$ Resources:Orbecatalog_Resources, Campo_Requerido %>"></asp:CustomValidator>
                        <br />
                        <br />
                        <asp:TreeView ID="trv_TipoProducto" runat="server" Style="display: none">
                        </asp:TreeView>
                        <br />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <h2>
                            Atributos Seleccionados</h2>
                    </td>
                    <td>
                        <asp:ImageButton ID="btn_AgregarAtributo" runat="server" ToolTip="Ingresar nuevo atributo"
                            ImageUrl="../iconos/agregar_small.png" ></asp:ImageButton>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:DataGrid ID="dg_Atributos_Productos" runat="server" AutoGenerateColumns="False"
                            CssClass="Contenido_Table">
                            <AlternatingItemStyle CssClass="tablaResultados_Alternate" />
                            <ItemStyle CssClass="tablaResultados_Item" />
                            <HeaderStyle CssClass="tablaResultados_Header" />
                            <FooterStyle CssClass="tablaResultados_Footer" />
                            <PagerStyle Mode="NumericPages" CssClass="tablaResultados_Pager" />
                            <Columns>
                                <asp:TemplateColumn HeaderText="Atributo">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_NombreCheck" runat="server" Text="Label"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="75%" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Buscable">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chk_Buscable" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle Width="10%" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Orden">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btn_Subir" runat="server" AlternateText="Subir" OnClick="btn_Subir_Click"
                                            ImageUrl="../images/buttons/arriba.png" Width="15" Height="21" />
                                        <asp:ImageButton ID="btn_Bajar" runat="server" AlternateText="Bajar" OnClick="btn_Bajar_Click"
                                            ImageUrl="../images/buttons/abajo.png" Width="15" Height="21" />
                                    </ItemTemplate>
                                    <ItemStyle Width="15%" />
                                </asp:TemplateColumn>
                            </Columns>
                        </asp:DataGrid><br />
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
                        runat="server" Text="<%$ Resources:Orbecatalog_Resources, Salvar_Cambios %>" CssClass="positive">
                    </orbelink:ActionButton><orbelink:ActionButton ID="btn_Cancelar" ImageURL="~/Orbecatalog/iconos/cancelar_small.png"
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
        Maestro de Atributos</h1>
    <asp:UpdatePanel ID="upd_Busqueda" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="Container">
                <asp:DataGrid ID="dg_MaestroAtributos_Productos" runat="server" AutoGenerateColumns="False"
                    CssClass="tablaResultados" ShowHeader="false">
                    <AlternatingItemStyle CssClass="tablaResultados_Alternate" />
                    <ItemStyle CssClass="tablaResultados_Item" />
                    <HeaderStyle CssClass="tablaResultados_Header" />
                    <FooterStyle CssClass="tablaResultados_Footer" />
                    <Columns>
                        <asp:TemplateColumn HeaderText=" ">
                            <ItemTemplate>
                                <asp:CheckBox ID="chk_Seleccionado" runat="server" />
                            </ItemTemplate>
                            <ItemStyle Width="10%" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Check">
                            <ItemTemplate>
                                <asp:Label ID="lbl_NombreCheck" runat="server" Text="Check"></asp:Label><asp:Label
                                    ID="lbl_Extra" runat="server" Text="Extra" Visible="false"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="80%" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn>
                            <ItemTemplate>
                                <asp:Image ID="img_Info" runat="server" ImageUrl="../images/icons/info.gif" />
                            </ItemTemplate>
                            <ItemStyle Width="10%" />
                        </asp:TemplateColumn>
                    </Columns>
                </asp:DataGrid><br />
                <asp:Button ID="btn_Actualizar" runat="server" Text="Actualizar" Width="100" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

