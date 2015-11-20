<%@ Page Language="VB" MasterPageFile="~/Orbecatalog/OrbeCatalog.master" AutoEventWireup="false"
    CodeFile="Default.aspx.vb" Inherits="Orbecatalog_Reservacion_Default" Title="Orbecatalog - Reservaciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_Titulo" runat="Server">
    <asp:Literal runat="server" Text="<%$ Resources:Reservaciones_Resources, Modulo_Reservaciones %>"></asp:Literal>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_Acciones" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cph_Contenido" runat="Server">
    <h1>
        <asp:Literal runat="server" Text="<%$ Resources:Reservaciones_Resources, Ultimas_Reservaciones %>"></asp:Literal>
    </h1>
    <div class="Container">
        <div class="Container">
            <a href="Reservacion.aspx">
                <asp:Literal runat="server" Text="<%$ Resources:Reservaciones_Resources, Ingresar_nueva_Reservacion %>"></asp:Literal></a><br />
            <asp:GridView ID="gv_Reservacions" runat="server" CssClass="tableClean" ShowFooter="false"
                ShowHeader="true" Width="100%">
                <RowStyle CssClass="tablaResultados_Item" HorizontalAlign="Center" />
                <AlternatingRowStyle CssClass="tablaResultados_Item" />
                <HeaderStyle HorizontalAlign="Center" />
            </asp:GridView>
            <br />
            <asp:Label ID="lbl_NoReservacions" runat="server" Text="<%$ Resources:Reservaciones_Resources, No_hay_Reservaciones %>"></asp:Label>
            <br />
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cph_BusquedaNueva" runat="Server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="cph_ColumnaDerecha" runat="Server">
    <h1>
        <asp:Literal runat="server" Text="<%$ Resources:Orbecatalog_Resources, Panel_Control %>"></asp:Literal></h1>
    <div class="Container">
        <asp:TreeView ID="trv_Reservaciones" runat="server">
        </asp:TreeView>
    </div>
</asp:Content>
