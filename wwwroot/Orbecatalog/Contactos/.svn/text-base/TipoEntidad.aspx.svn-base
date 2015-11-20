<%@ Page Language="VB" MasterPageFile="../OrbeCatalog.master" AutoEventWireup="false"
    CodeFile="TipoEntidad.aspx.vb" Inherits="_TipoEntidad" Title="Orbe Catalog - Tipos de Entidad" %>

<asp:Content ID="ctn_TituloSeccion" ContentPlaceHolderID="cph_Titulo" runat="Server">
    Manejo de Tipos de Entidad
</asp:Content>
<asp:Content ID="ctn_Contenido" ContentPlaceHolderID="cph_Contenido" runat="Server">
    <h1>
        Tipo de Entidad</h1>
    <asp:UpdatePanel ID="upd_Contenido" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table class="Container" cellspacing="0" cellpadding="0" border="0">
                <tr>
                    <td style="width: 25%;">
                        Nombre
                    </td>
                    <td style="width: 70%;">
                        <asp:TextBox ID="tbx_NombreTipoEntidad" runat="server"></asp:TextBox>
                    </td>
                    <td style="width: 5%;">
                        <asp:RequiredFieldValidator ID="vld_tbx_NombreTipoEntidad" runat="server" ControlToValidate="tbx_NombreTipoEntidad"
                            Display="Dynamic" EnableClientScript="False" ErrorMessage="<%$ Resources:Orbecatalog_Resources, Campo_Requerido %>"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        Descripcion
                    </td>
                    <td>
                        <asp:TextBox ID="tbx_DescripcionTipoEntidad" runat="server"></asp:TextBox>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        Nivel
                    </td>
                    <td>
                        <asp:DropDownList ID="ddl_Nivel" runat="server">
                            <asp:ListItem Selected="True" Text="0"></asp:ListItem>
                            <asp:ListItem Text="1"></asp:ListItem>
                            <asp:ListItem Text="2"></asp:ListItem>
                            <asp:ListItem Text="3"></asp:ListItem>
                            <asp:ListItem Text="4"></asp:ListItem>
                            <asp:ListItem Text="5"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
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
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="cph_Busqueda" ContentPlaceHolderID="cph_ColumnaDerecha" runat="Server">
    <h1>
        Tipos de Entidad Actuales</h1>
    <asp:UpdatePanel ID="upd_Busqueda" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="Container">
                <asp:DataGrid ID="dg_TipoEntidad" CssClass="tableClean" runat="server" AutoGenerateColumns="False"
                    AllowPaging="True" PageSize="10" GridLines="none">
                    <AlternatingItemStyle CssClass="tablaResultados_Alternate" />
                    <ItemStyle CssClass="tablaResultados_Item" />
                    <HeaderStyle CssClass="tablaResultados_Header" />
                    <FooterStyle CssClass="tablaResultados_Footer" />
                    <PagerStyle Mode="NumericPages" CssClass="tablaResultados_Pager" />
                    <Columns>
                        <asp:EditCommandColumn CancelText="Cancel" EditText="Edit" UpdateText="Update"></asp:EditCommandColumn>
                        <asp:TemplateColumn HeaderText="Tipo Entidad">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Nombre" runat="server" Text="TipoEntidad"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="25%" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Descripcion">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Descripcion" runat="server" Text="Descripcion"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="65%" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn>
                            <ItemTemplate>
                                <asp:CheckBox ID="chk_Control" runat="server" /><br />
                            </ItemTemplate>
                            <ItemStyle Width="5%" />
                            <HeaderTemplate>
                                <asp:CheckBox ID="chk_TodosGrupos" runat="server" OnCheckedChanged="chk_TodosGrupos_CheckedChanged"
                                    AutoPostBack="true" />
                            </HeaderTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                </asp:DataGrid>
                <asp:Label ID="lbl_ResultadoElementos" runat="server" Text="No hay elementos"></asp:Label><br />
                <asp:Button ID="btn_BorrarSeleccionados" runat="server" Text="Borrar Seleccionados" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
