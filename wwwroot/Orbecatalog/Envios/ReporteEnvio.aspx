<%@ Page Language="VB" MasterPageFile="~/orbecatalog/OrbeCatalogB.master" AutoEventWireup="false"
    CodeFile="ReporteEnvio.aspx.vb" Inherits="_ReporteEnvio" Title="OrbeCatalog - Reporte Envio" %>

<asp:Content ID="Content2" ContentPlaceHolderID="cph_titulo" runat="Server">
    Reporte de Envio
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_Contenido" runat="Server">
    <h1>
        Envio
    </h1>
    <table class="Container" cellspacing="0" cellpadding="0" border="0">
        <tr>
            <td style="width:25%;">
                Subject</td>
            <td style="width:70%;">
                <asp:Label ID="lbl_SubjectEnvio" runat="server"></asp:Label></td>
            <td style="width:5%">
            </td>
        </tr>
        <tr>
            <td>
                Sender</td>
            <td>
                <asp:Label ID="lbl_Sender" runat="server"></asp:Label></td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                Reply-To</td>
            <td>
                <asp:Label ID="lbl_ReplyTo" runat="server"></asp:Label></td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                Entidad</td>
            <td>
                <asp:Label ID="lbl_Entidad" runat="server"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
            </td>
            <td>
            </td>
        </tr>
    </table>
    <h1>
        Reporte</h1>
    <asp:UpdatePanel ID="upd_Contenido" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <rsweb:ReportViewer ID="report_Entidades_Envio" runat="server" Width="100%" Height="350px"
                ShowToolBar="false">
            </rsweb:ReportViewer>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

