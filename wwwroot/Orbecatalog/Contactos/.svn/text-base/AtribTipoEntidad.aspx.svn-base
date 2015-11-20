<%@ Page Language="VB" MasterPageFile="~/orbecatalog/OrbeCatalog.master" AutoEventWireup="false"
    CodeFile="AtribTipoEntidad.aspx.vb" Inherits="_Entidades_AtribTipoEntidad" Title="Orbe Catalog - Atributos por Tipo Entidad" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_Titulo" runat="Server">
    Manejo de Atributos por Tipo Entidad
</asp:Content>
<asp:Content ID="ctn_Contenido" ContentPlaceHolderID="cph_Contenido" runat="Server">
    <asp:UpdatePanel ID="upd_Contenido" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table class="Container" cellspacing="0" cellpadding="0" border="0">
                <tr>
                    <td colspan="2">
                        <h2>
                            Tipo Entidad</h2>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        Tipo Entidad</td>
                    <td>
                        <asp:DropDownList ID="ddl_TipoEntidades" runat="server" AutoPostBack="true">
                        </asp:DropDownList></td>
                    <td>
                        <asp:ImageButton ID="btn_AgregarTipoEntidad" runat="server" ToolTip="Ingresar nuevo Tipo Entidad"
                            ImageUrl="../iconos/agregar_small.png" ></asp:ImageButton>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <h2>
                            Atributos Seleccionados</h2>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:DataGrid ID="dg_Atributos_Para_Entidades" runat="server" AutoGenerateColumns="False"
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
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
</asp:Content>
<asp:Content ID="cph_Busqueda" ContentPlaceHolderID="cph_ColumnaDerecha" runat="Server">
    <h1>
        Maestro de Atributos</h1>
    <div class="Container">
        <table width="100%" cellpadding="0" cellspacing="0" border="0">
            <tr>
                <td style="width:95%">
                    Atributos Actuales
                </td>
                <td style="width:5%">
                    <asp:ImageButton ID="btn_AgregarAtributo" runat="server" ToolTip="Ingresar nuevo atributo"
                        ImageUrl="../iconos/agregar_small.png" ></asp:ImageButton>
                </td>
            </tr>
        </table>
        <br />
        <asp:UpdatePanel ID="upd_Busqueda" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:DataGrid ID="dg_MaestroAtributos_Para_Entidades" runat="server" AutoGenerateColumns="False"
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
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

