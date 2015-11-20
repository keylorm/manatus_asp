<%@ Page Language="VB" MasterPageFile="../OrbeCatalog.master" AutoEventWireup="false"
    CodeFile="Categorias.aspx.vb" Inherits="Orbecatalog_Publicacion_Categorias" Title="Orbe Catalog - Categorias" %>

<asp:Content ID="ctn_TituloSeccion" ContentPlaceHolderID="cph_Titulo" runat="Server">
    Manejo de Categorias
</asp:Content>
<asp:Content ID="Atributos" ContentPlaceHolderID="cph_Contenido" runat="Server">
    <h1>
        Categoria</h1>
    <asp:UpdatePanel ID="upd_Contenido" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table class="Container" cellspacing="0" cellpadding="0" border="0">
                <tr>
                    <td>
                        Nombre</td>
                    <td>
                        <asp:TextBox ID="tbx_NombreCategorias" runat="server"></asp:TextBox></td>
                    <td>
                        <asp:RequiredFieldValidator ID="vld_tbx_NombreCategorias" runat="server" ControlToValidate="tbx_NombreCategorias"
                            Display="Dynamic" EnableClientScript="False" ErrorMessage="<%$ Resources:Orbecatalog_Resources, Campo_Requerido %>"></asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td>
                        Categoria Perteneciente</td>
                    <td>
                        <asp:Label ID="lbl_CategoriasPerteneciente" runat="server" Text="Sin Asignar"></asp:Label><br />
                        <br />
                        <asp:TreeView ID="trv_Perteneciente" runat="server">
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="cph_Busqueda" ContentPlaceHolderID="cph_ColumnaDerecha" runat="Server">
    <h1>
        Categorias Actuales</h1>
    <asp:UpdatePanel ID="upd_Busqueda" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="Container">
                <asp:TreeView ID="trv_Categorias" runat="server">
                </asp:TreeView>
            </div>
            
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

