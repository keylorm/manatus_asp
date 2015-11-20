<%@ Page Language="VB" MasterPageFile="../OrbeCatalog.master" AutoEventWireup="false"
    CodeFile="Grupo.aspx.vb" Inherits="_Grupo" Title="Orbe Catalog - Grupos" %>

<asp:Content ID="ctn_TituloSeccion" ContentPlaceHolderID="cph_Titulo" runat="Server">
    Manejo de Grupos
</asp:Content>

<asp:Content ID="ctn_Contenido" ContentPlaceHolderID="cph_Contenido" runat="Server">
    <h1>
        Grupo</h1>
    <asp:UpdatePanel ID="upd_Contenido" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table class="Container" cellspacing="0" cellpadding="0" border="0">
                <tr>
                    <td>
                        Nombre</td>
                    <td>
                        <asp:TextBox ID="tbx_NombreGrupo" runat="server"></asp:TextBox></td>
                    <td>
                        <asp:RequiredFieldValidator ID="vld_tbx_NombreGrupo" runat="server" ControlToValidate="tbx_NombreGrupo"
                            Display="Dynamic" EnableClientScript="False" ErrorMessage="<%$ Resources:Orbecatalog_Resources, Campo_Requerido %>"></asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td>
                        Descripcion</td>
                    <td>
                        <asp:TextBox ID="tbx_Descripcion" runat="server"></asp:TextBox></td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        Perteneciente</td>
                    <td>
                        <asp:Label ID="lbl_GrupoPerteneciente" runat="server" Text="Sin Asignar"></asp:Label><br />
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
        Grupos Actuales</h1>
    <asp:UpdatePanel ID="upd_Busqueda" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="Container">
                <asp:TreeView ID="trv_Grupos_Entidades" runat="server">
                </asp:TreeView>
            </div>
            <asp:Panel ID="pnl_ListaGrupo" runat="server">
                <h1>
                    Lista de grupo</h1>
                <div class="Container">
                    <asp:HyperLink ID="lnk_ListaGrupo" runat="server">Ver la lista de este grupo</asp:HyperLink><br />
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

