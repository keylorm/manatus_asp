<%@ Page Language="VB" MasterPageFile="~/Orbecatalog/OrbeCatalog.master" AutoEventWireup="false"
    CodeFile="ReporteDetalleFactura.aspx.vb" Inherits="Orbecatalog_Facturas_ReporteDetalleFactura"
    Title="Orbecatlog - Facturas - Detalle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_Titulo" runat="Server">
    <asp:Literal ID="Literal1" runat="server" Text="Reporte Detalle Factura"></asp:Literal>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_Navegacion" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_Acciones" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cph_Contenido" runat="Server">
    <asp:UpdatePanel ID="Upd_pagina" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <rsweb:ReportViewer Width="95%" ID="RW_factura" runat="server">
            </rsweb:ReportViewer>
            <br />
            <br />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cph_BusquedaNueva" runat="Server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="cph_ColumnaDerecha" runat="Server">
</asp:Content>
