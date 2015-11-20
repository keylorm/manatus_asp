<%@ Page Language="VB" MasterPageFile="../OrbeCatalog.master" AutoEventWireup="false"
    ValidateRequest="false" CodeFile="Aprobacion_TipoPublicacion.aspx.vb" Inherits="_Aprobacion_TipoPublicacion"
    Title="OrbeCatalog - Aprobacion por Tipo de Publicacion" %>

<asp:Content ID="ctn_TituloSeccion" ContentPlaceHolderID="cph_Titulo" runat="Server">
    Aprobaciones requeridas por Tipo Publicacion
</asp:Content>
<asp:Content ID="ctn_Contenido" ContentPlaceHolderID="cph_Contenido" runat="Server">
    <h1>
        Aprobaciones por Tipo Publicacion</h1>
    <table class="Container" cellspacing="0" cellpadding="0" border="0">
        <tr>
            <td>
                Tipo Publicacion</td>
            <td>
                <asp:DropDownList ID="ddl_TipoPublicacion" runat="server">
                </asp:DropDownList></td>
            <td>
                <asp:ImageButton ID="btn_AgregarTipoPublicacion" runat="server" ToolTip="Ingresar nuevo Tipo Publicacion"
                    ImageUrl="../iconos/agregar_small.png"  /></td>
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
                    Pertenencia a grupos</h2>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                Tipo Relacion</td>
            <td>
                <asp:DropDownList ID="ddl_tipoRelacion" runat="server">
                </asp:DropDownList></td>
            <td>
                <asp:ImageButton ID="btn_AgregarTipoRelacion" runat="server" ToolTip="Ingresar nuevo Tipo Relacion"
                    ImageUrl="../iconos/agregar_small.png" ></asp:ImageButton></td>
        </tr>
        <tr>
            <td>
                Grupo</td>
            <td>
                <asp:HyperLink ID="lnk_Grupos_Entidades" runat="server">(Sin Asignar)</asp:HyperLink>
            </td>
            <td>
                <asp:CustomValidator ID="vld_Grupo" runat="server" Display="Dynamic" ErrorMessage="<%$ Resources:Orbecatalog_Resources, Campo_Requerido %>"></asp:CustomValidator>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:TreeView ID="trv_Grupos" runat="server" Style="display: none;">
                </asp:TreeView>
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
</asp:Content>
<asp:Content ID="cph_Busqueda" ContentPlaceHolderID="cph_ColumnaDerecha" runat="Server">
    <h1>
        Busqueda y Filtros</h1>
    <div class="Container">
        <table border="0" cellpadding="0" cellspacing="0" width="300">
            <tr>
                <td>
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    Tipo Publicacion:</td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
                <td>
                    <asp:DropDownList ID="ddl_Filtro_TipoPublicacion" runat="server">
                    </asp:DropDownList></td>
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
    <h1>
        Aprobaciones por Tipos Publicaciones</h1>
    <div class="Container">
        <asp:DataGrid ID="dg_Aprobacion_TipoPublicacions" runat="server" AutoGenerateColumns="False"
            AllowPaging="True" PageSize="10" CssClass="tablaResultados">
            <AlternatingItemStyle CssClass="tablaResultados_Alternate" />
            <ItemStyle CssClass="tablaResultados_Item" />
            <HeaderStyle CssClass="tablaResultados_Header" />
            <FooterStyle CssClass="tablaResultados_Footer" />
            <PagerStyle Mode="NumericPages" CssClass="tablaResultados_Pager" />
            <Columns>
                <asp:EditCommandColumn CancelText="Cancel" EditText="Edit" UpdateText="Update">
                    <ItemStyle Width="15%" />
                </asp:EditCommandColumn>
                <asp:TemplateColumn HeaderText="Tipo Publicacion">
                    <ItemTemplate>
                        <asp:Label ID="lbl_TipoPublicacion" runat="server" Text="Tipo Publicacion"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
                               <asp:TemplateColumn HeaderText="Relacion">
                    <ItemTemplate>
                        <asp:Label ID="lbl_TipoRelacion" runat="server" Text="Tipo Relacion"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="Grupo">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Grupos_Entidades" runat="server" Text="Grupo"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
            </Columns>
        </asp:DataGrid>
        <asp:Label ID="lbl_ResultadoElementos" runat="server" Text="No hay elementos"></asp:Label>
    </div>
    
</asp:Content>

