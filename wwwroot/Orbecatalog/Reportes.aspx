<%@ Page Language="VB" MasterPageFile="~/Orbecatalog/OrbeCatalogB.master" AutoEventWireup="false"
    CodeFile="Reportes.aspx.vb" Inherits="Orbecatalog_Reportes" ValidateRequest="false"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_Titulo" runat="Server">
    Reportes Entidades
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_Navegacion" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_Acciones" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cph_Contenido" runat="Server">
    <asp:Panel ID="Panel1" runat="server">        
        <table width="600px">
            <tr>
                <td style="width: 150px">
                    <asp:CheckBox ID="ChB_nombreCompleto" runat="server" Text="Nombre Completo" />
                </td>
                <td style="width: 150px">
                    <asp:CheckBox ID="chk_apellido1" runat="server" Text="Apellido 1" />
                </td>
                <td style="width: 150px">
                    <asp:CheckBox ID="chk_apellido2" runat="server" Text="Apellido 2" />
                </td>
                <td>
                    <asp:CheckBox ID="chk_telefono" runat="server" Text="Telefono" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:CheckBox ID="chk_celular" runat="server" Text="Celular" />
                </td>
                <td>
                    <asp:CheckBox ID="chk_Email" runat="server" Text="Email" />
                </td>
                <td>
                    <asp:CheckBox ID="chk_Descripcion" runat="server" Text="Descripcion" />
                </td>
                <td>
                    <asp:CheckBox ID="chk_identificacion" runat="server" Text="Identificacion" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="Tipo Entidad"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList Width="95%" ID="ddl_tiposEntidades" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="Ubicacion"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList Width="95%" ID="ddl_Ubicaciones" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <br />
        <asp:Label ID="Label1" runat="server" Text="Titulo" /><asp:TextBox ID="txb_elTitulo"
            runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="Label2" runat="server" Text="Mostrar Fecha" /><asp:CheckBox ID="chb_fecha"
            runat="server" />
        <br />
        <br />
        <asp:Button ID="btn_generar" runat="server" Text="Generar" />
    </asp:Panel>
    <br />
    <rsweb:ReportViewer ID="rv_reporte" Width="850px" runat="server">
    </rsweb:ReportViewer>
    <br />
    <br />
    <br />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cph_BusquedaNueva" runat="Server">
</asp:Content>
