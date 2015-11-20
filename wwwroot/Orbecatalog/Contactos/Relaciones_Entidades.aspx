<%@ Page Language="VB" MasterPageFile="../OrbeCatalog.master" AutoEventWireup="false"
    CodeFile="Relaciones_Entidades.aspx.vb" Inherits="_Relacion_Entidades" Title="Orbe Catalog - Relaciones Entidades" %>

<asp:Content ID="ctn_TituloSeccion" ContentPlaceHolderID="cph_Titulo" runat="Server">
    Manejo de Relaciones entre Entidades
</asp:Content>

<asp:Content ID="ctn_Contenido" ContentPlaceHolderID="cph_Contenido" runat="Server">
    <h1>
        Relacion</h1>
    <asp:UpdatePanel ID="upd_Contenido" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table class="Container" cellspacing="0" cellpadding="0" border="0">
                <tr>
                    <td>
                        Tipo Entidades Dependientes</td>
                    <td>
                        <asp:DropDownList ID="ddl_TipoEntidad_Dependiente" runat="server" AutoPostBack="True">
                        </asp:DropDownList></td>
                    <td>
                        <asp:ImageButton ID="btn_AgregarTipoEntidad" runat="server" ToolTip="Ingresar nuevo Tipo Entidad"
                            ImageUrl="../iconos/agregar_small.png" ></asp:ImageButton>
                    </td>
                </tr>
                <tr>
                    <td>
                        Tipo Entidades Bases</td>
                    <td>
                        <asp:DropDownList ID="ddl_TipoEntidad_Base" runat="server" AutoPostBack="True">
                        </asp:DropDownList></td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <h2>
                            Relacion Entidades</h2>
                    </td>
                </tr>
                <tr>
                    <td>
                        Entidad Dependiente
                    </td>
                    <td>
                        <asp:DropDownList ID="ddl_EntidadDependiente" runat="server">
                        </asp:DropDownList>
                        es</td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        Tipo Relacion</td>
                    <td>
                        <asp:DropDownList ID="ddl_TipoRelacion_Entidades" runat="server">
                        </asp:DropDownList>
                        de</td>
                    <td>
                        <asp:ImageButton ID="btn_AgregarTipoRelacion" runat="server" ToolTip="Ingresar nuevo Tipo de Relacion entre Entidades"
                            ImageUrl="../iconos/agregar_small.png" ></asp:ImageButton></td>
                </tr>
                <tr>
                    <td>
                        Entidad Base</td>
                    <td>
                        <asp:DropDownList ID="ddl_EntidadBase" runat="server">
                        </asp:DropDownList></td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        Comentario</td>
                    <td>
                        <asp:TextBox ID="tbx_ComentariosRelaciones" runat="server" Height="87px" TextMode="MultiLine"
                            Width="232px"></asp:TextBox></td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        &nbsp;</td>
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
        Busqueda y Filtros</h1>
    <asp:UpdatePanel ID="upd_Busqueda" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="Container">
                <table border="0" cellpadding="0" cellspacing="0" width="300">
                    <tr>
                        <td>
                            Tipo Relacion:</td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            Tipo Entidad</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DropDownList ID="ddl_Filtro_TipoRelaciones" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddl_Filtro_TipoEntidades" runat="server" >
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btn_Buscar" runat="server" Text="Buscar" /></td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="Container">
                <asp:DataGrid ID="dg_Relacion_Entidades" runat="server" AutoGenerateColumns="False"
                    AllowPaging="True" PageSize="10" CssClass="tablaResultados">
                    <AlternatingItemStyle CssClass="tablaResultados_Alternate" />
                    <ItemStyle CssClass="tablaResultados_Item" />
                    <HeaderStyle CssClass="tablaResultados_Header" />
                    <FooterStyle CssClass="tablaResultados_Footer" />
                    <PagerStyle Mode="NumericPages" CssClass="tablaResultados_Pager" />
                    <Columns>
                        <asp:EditCommandColumn CancelText="Cancel" EditText="Edit" UpdateText="Update"></asp:EditCommandColumn>
                        <asp:TemplateColumn HeaderText="Relaciones">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Relacion_Entidades" runat="server" Text="Label"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                </asp:DataGrid>
                <asp:Label ID="lbl_ResultadoElementos" runat="server" Text="No hay elementos" Visible="False"></asp:Label>
            </div>
            
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

