<%@ Page Language="VB" MasterPageFile="../OrbeCatalog.master" AutoEventWireup="false"
    CodeFile="TipoRelacion.aspx.vb" Inherits="_TipoRelacion_Productos" Title="Orbe Catalog - Productos - Tipo Relaciones" %>
<asp:Content ID="ctn_TituloSeccion" ContentPlaceHolderID="cph_Titulo" runat="Server">
    <asp:Literal runat="server" Text="<%$ Resources:Productos_Resources, TiposRelacion_Pantalla %>" />
</asp:Content>
<asp:Content ID="ctn_Contenido" ContentPlaceHolderID="cph_Contenido" runat="Server">
    <h1>
        Tipo de Relacion entre Productos</h1>
    <table class="Container" cellspacing="0" cellpadding="0" border="0">
        <tr>
            <td>
                Nombre
            </td>
            <td>
                <asp:TextBox ID="tbx_NombreTipoRelacion_Productos" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="vld_tbx_NombreTipoRelacion_Productos" runat="server"
                    ControlToValidate="tbx_NombreTipoRelacion_Productos" Display="Dynamic" EnableClientScript="False"
                    ErrorMessage="<%$ Resources:Orbecatalog_Resources, Campo_Requerido %>"></asp:RequiredFieldValidator>
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
</asp:Content>
<asp:Content ID="cph_Busqueda" ContentPlaceHolderID="cph_ColumnaDerecha" runat="Server">
    <h1>
        Tipos de Relaciones Actuales</h1>
    <div class="Container">
        <asp:DataGrid ID="dg_TipoRelacion" runat="server" AutoGenerateColumns="False" AllowPaging="true"
            CssClass="tablaResultados" PageSize="10">
            <AlternatingItemStyle CssClass="tablaResultados_Alternate" />
            <ItemStyle CssClass="tablaResultados_Item" />
            <HeaderStyle CssClass="tablaResultados_Header" />
            <FooterStyle CssClass="tablaResultados_Footer" />
            <PagerStyle Mode="NumericPages" CssClass="tablaResultados_Pager" />
            <Columns>
                <asp:EditCommandColumn CancelText="Cancel" EditText="Edit" UpdateText="Update"></asp:EditCommandColumn>
                <asp:TemplateColumn HeaderText="Tipo Relacion">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Nombre" runat="server" Text="Label"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
            </Columns>
        </asp:DataGrid></div>
</asp:Content>
