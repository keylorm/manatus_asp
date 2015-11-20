<%@ Page Language="VB" MasterPageFile="~/Orbecatalog/OrbeCatalog.master" AutoEventWireup="false"
    CodeFile="TipoRelacion_Entidad_Publicacion.aspx.vb" Inherits="Orbecatalog_Relaciones_TipoRelacion_Entidad_Publicacion"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_Titulo" runat="Server">
    <h1>
        Tipo de Relacion entre Entidades y Publicaciones</h1>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_Navegacion" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_Acciones" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cph_Contenido" runat="Server">
    <asp:UpdatePanel UpdateMode="Always" runat="server">
        <ContentTemplate>
            <table class="Container" cellspacing="0" cellpadding="0" border="0">
                <tr>
                    <td>
                        Nombre Entidad-Publicacion
                    </td>
                    <td>
                        <asp:TextBox ID="tbx_NombreRelacion_Entidad_Publicacion" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="vld_tbx_Entidad_Publicacion" runat="server" ControlToValidate="tbx_NombreRelacion_Entidad_Publicacion"
                            Display="Dynamic" EnableClientScript="False" ErrorMessage="<%$ Resources:Orbecatalog_Resources, Campo_Requerido %>"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        Nombre Publicacion-Entidad
                    </td>
                    <td>
                        <asp:TextBox ID="tbx_NombreRelacion_Publicacion_Entidad" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="vld_tbx_Publicacion_Entidad" runat="server" ControlToValidate="tbx_NombreRelacion_Publicacion_Entidad"
                            Display="Dynamic" EnableClientScript="False" ErrorMessage="<%$ Resources:Orbecatalog_Resources, Campo_Requerido %>"></asp:RequiredFieldValidator>
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
<asp:Content ID="Content5" ContentPlaceHolderID="cph_BusquedaNueva" runat="Server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="cph_ColumnaDerecha" runat="Server">
    <asp:UpdatePanel UpdateMode="Always" runat="server">
        <ContentTemplate>
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
