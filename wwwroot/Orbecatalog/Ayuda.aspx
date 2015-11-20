<%@ Page Language="VB" MasterPageFile="OrbeCatalog.master" AutoEventWireup="false"
    CodeFile="Ayuda.aspx.vb" Inherits="_TipoEntidad" Title="Orbe Catalog - Ayuda" %>

<asp:Content ID="ctn_TituloSeccion" ContentPlaceHolderID="cph_Titulo" runat="Server">
    Ayuda
</asp:Content>
<asp:Content ID="ctn_Contenido" ContentPlaceHolderID="cph_Contenido" runat="Server">
    <h1>
        Formulario de Ayuda</h1>
    <table class="Container" cellspacing="0" cellpadding="0" border="0">
        <tr>
            <td>
                Nombre
            </td>
            <td>
                <asp:Label ID="lbl_Entidad" runat="server" Text="Nombre"></asp:Label><asp:TextBox
                    ID="tbx_NombreAyuda" runat="server"></asp:TextBox><br />
                <br />
            </td>
            <td>
                <asp:RequiredFieldValidator ID="vld_tbx_NombreAyuda" runat="server" ControlToValidate="tbx_NombreAyuda"
                    Display="Dynamic" EnableClientScript="False" ErrorMessage="<%$ Resources:Orbecatalog_Resources, Campo_Requerido %>"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                Email
            </td>
            <td>
                <asp:TextBox ID="tbx_EmailAyuda" runat="server"></asp:TextBox><br />
                <br />
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                Comentario
            </td>
            <td>
                <asp:TextBox CssClass="form" ID="tbx_Comentario" runat="server" TextMode="MultiLine"></asp:TextBox>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <table id="tablaBotones" class="botonesAcciones" cellspacing="0" cellpadding="0" border="0">
                    <tr>
                        <td>
                            <orbelink:ActionButton ID="btn_Salvar" CausesValidation="true" ImageURL="~/Orbecatalog/iconos/salvar_small.png"
                        runat="server" Text="<%$ Resources:Orbecatalog_Resources, Salvar %>" CssClass="positive">
                    </orbelink:ActionButton>
                        </td>
                        <td>
                            <orbelink:ActionButton ID="btn_Modificar" CausesValidation="true" ImageURL="~/Orbecatalog/iconos/modificar_small.png"
                        runat="server" Text="<%$ Resources:Orbecatalog_Resources, Salvar_Cambios %>" CssClass="positive">
                    </orbelink:ActionButton>
                        </td>
                        <td><orbelink:ActionButton ID="btn_Cancelar" ImageURL="~/Orbecatalog/iconos/cancelar_small.png"
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
            <td>
            </td>
        </tr>
    </table>
</asp:Content>
