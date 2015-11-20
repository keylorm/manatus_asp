<%@ Page Language="VB" MasterPageFile="../OrbeCatalogB.master" AutoEventWireup="false"
    CodeFile="Facturas.aspx.vb" Inherits="Orbecatalog_Facturas_Facturas" Title="Orbe Catalog - Facturas" %>

<asp:Content ID="ctn_TituloSeccion" ContentPlaceHolderID="cph_titulo" runat="Server">
    Reporte de Facturas
</asp:Content>
<asp:Content ID="ctn_Trabajo" ContentPlaceHolderID="cph_Contenido" runat="Server">
    <h1>
        Gráfico</h1>
    <div class="Container">
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="100%">
        </rsweb:ReportViewer>
        <br />
    </div>
</asp:Content>

