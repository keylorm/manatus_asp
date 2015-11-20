<%@ Page Language="VB" MasterPageFile="~/Orbecatalog/OrbeCatalog.master" AutoEventWireup="false"
    CodeFile="ReportesReservacion.aspx.vb"  Inherits="Orbecatalog_Reservacion_ReportesReservacion"
    Title="Orbecatalog - Reservaciones - Reporte" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_Titulo" runat="Server">
    <asp:Literal runat="server" Text="<%$ Resources:Reservaciones_Resources, Reservacion_reporte %>"></asp:Literal>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_Navegacion" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_Acciones" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cph_Contenido" runat="Server">
    <div>
        <asp:TextBox ID="txb_diaReporte" Width="150px" runat="server"></asp:TextBox>
        <obout:Calendar ID="CalendarDia" runat="server" DatePickerMode="true" TextBoxId="txb_diaReporte"
            DatePickerImagePath="../iconos/calendar.gif" />
        <asp:Button ID="btn_verReporte" runat="server" Text="Mostrar" />
    </div>
    <br />
    <rsweb:ReportViewer Width="95%" ID="RW_dia" runat="server">
    </rsweb:ReportViewer>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cph_BusquedaNueva" runat="Server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="cph_ColumnaDerecha" runat="Server">
</asp:Content>
