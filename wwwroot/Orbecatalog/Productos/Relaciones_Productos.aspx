<%@ Page Language="VB" MasterPageFile="../OrbeCatalog.master" AutoEventWireup="false"
    CodeFile="Relaciones_Productos.aspx.vb" Inherits="_Relacion_Productos" Title="Orbe Catalog - Relaciones Productos" %>

<asp:Content ID="ctn_TituloSeccion" ContentPlaceHolderID="cph_Titulo" runat="Server">
    <asp:Literal runat="server" Text="<%$ Resources:Productos_Resources, Relaciones_Pantalla %>" />
</asp:Content>
<asp:Content ID="ctn_Contenido" ContentPlaceHolderID="cph_Contenido" runat="Server">
    <h1>
        Relacion</h1>
    <table class="Container" cellspacing="0" cellpadding="0" border="0">
        <tr>
            <td>
                Tipo Productos Dependientes
            </td>
            <td>
                <asp:DropDownList ID="ddl_Tipo_Producto_Dependiente" runat="server" AutoPostBack="True">
                </asp:DropDownList>
            </td>
            <td>
                <asp:ImageButton ID="btn_AgregarTipo_Producto" runat="server" ToolTip="Ingresar nuevo Tipo Producto"
                    ImageUrl="../iconos/agregar_small.png"></asp:ImageButton>
            </td>
        </tr>
        <tr>
            <td>
                Tipo Productos Bases
            </td>
            <td>
                <asp:DropDownList ID="ddl_Tipo_Producto_Base" runat="server" AutoPostBack="True">
                </asp:DropDownList>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <h2>
                    Relacion Productos</h2>
            </td>
        </tr>
        <tr>
            <td>
                Producto Dependiente
            </td>
            <td>
                <asp:DropDownList ID="ddl_ProductoDependiente" runat="server">
                </asp:DropDownList>
                es
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                Tipo Relacion
            </td>
            <td>
                <asp:DropDownList ID="ddl_TipoRelacion_Productos" runat="server">
                </asp:DropDownList>
                de
            </td>
            <td>
                <asp:ImageButton ID="btn_AgregarTipoRelacion" runat="server" ToolTip="Ingresar nuevo Tipo de Relacion_Productos"
                    ImageUrl="../iconos/agregar_small.png"></asp:ImageButton>
            </td>
        </tr>
        <tr>
            <td>
                Producto Base
            </td>
            <td>
                <asp:DropDownList ID="ddl_ProductoBase" runat="server">
                </asp:DropDownList>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                Comentario
            </td>
            <td>
                <asp:TextBox ID="tbx_ComentariosRelaciones" runat="server" Height="87px" TextMode="MultiLine"
                    Width="232px"></asp:TextBox>
            </td>
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
                &nbsp;
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <table id="tablaBotones" class="botonesAcciones" cellspacing="0" cellpadding="0"
                    border="0">
                    <tr>
                        <td>
                            <orbelink:ActionButton ID="btn_Salvar" CausesValidation="true" ImageURL="~/Orbecatalog/iconos/salvar_small.png"
                                runat="server" Text="<%$ Resources:Orbecatalog_Resources, Salvar %>" CssClass="positive">
                            </orbelink:ActionButton>
                        </td>
                        <td>
                            <orbelink:ActionButton ID="btn_Modificar" CausesValidation="true" ImageURL="~/Orbecatalog/iconos/modificar_small.png"
                                runat="server" Text="<%$ Resources:Orbecatalog_Resources, Salvar_Cambios %>"
                                CssClass="positive">
                            </orbelink:ActionButton>
                        </td>
                        <td>
                            <orbelink:ActionButton ID="btn_Cancelar" ImageURL="~/Orbecatalog/iconos/cancelar_small.png"
                                runat="server" Text="<%$ Resources:Orbecatalog_Resources, Cancelar %>">
                            </orbelink:ActionButton>
                        </td>
                        <td>
                            <orbelink:ActionButton ID="btn_Eliminar" ImageURL="~/Orbecatalog/iconos/eliminar_small.png"
                                runat="server" Text="<%$ Resources:Orbecatalog_Resources, Eliminar %>" CssClass="negative">
                            </orbelink:ActionButton>
                        </td>
                    </tr>
                </table>
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
                    Tipo Relacion:
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    Tipo Producto
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DropDownList ID="ddl_Filtro_TipoRelaciones" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                </td>
                <td>
                    <asp:DropDownList ID="ddl_Filtro_Tipo_Productos" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
                <td>
                    Producto
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
                <td>
                    <asp:DropDownList ID="ddl_Filtro_Producto" runat="server">
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
                    <asp:Button ID="btn_Buscar" runat="server" Text="Buscar" />
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
        </table>
    </div>
    <div class="Container">
        <asp:DataGrid ID="dg_Relacion_Productos" runat="server" AutoGenerateColumns="False"
            AllowPaging="True" PageSize="10" CssClass="tablaResultados">
            <AlternatingItemStyle CssClass="tablaResultados_Alternate" />
            <ItemStyle CssClass="tablaResultados_Item" />
            <HeaderStyle CssClass="tablaResultados_Header" />
            <FooterStyle CssClass="tablaResultados_Footer" />
            <PagerStyle Mode="NumericPages" CssClass="tablaResultados_Pager" />
            <Columns>
                <asp:EditCommandColumn CancelText="Cancel" EditText="Edit" UpdateText="Update"></asp:EditCommandColumn>
                <asp:TemplateColumn>
                    <HeaderTemplate>
                        Relaciones
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lbl_Relacion_Productos" runat="server" Text="Label"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
            </Columns>
        </asp:DataGrid>
        <asp:Label ID="lbl_ResultadoElementos" runat="server" Text="No hay elementos"></asp:Label></div>
</asp:Content>
