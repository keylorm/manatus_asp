<%@ Page Language="VB" MasterPageFile="~/Orbecatalog/OrbeCatalog.master" AutoEventWireup="false"
    CodeFile="Search.aspx.vb" Inherits="Orbecatalog_Reservacion_Search" Title="Orbecatalog - Reservaciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_Titulo" runat="Server">
    <asp:Literal runat="server" Text="<%$ Resources:Reservaciones_Resources, BusquedaReservaciones_Pantalla %>"></asp:Literal>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_Acciones" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cph_Contenido" runat="Server">
    <h1>
        <asp:Literal runat="server" Text="<%$ Resources:Orbecatalog_Resources, Resultados%>"></asp:Literal>
    </h1>
    <div class="Container">
        <div class="Container">
            <a href="Reservacion.aspx">
                <asp:Literal runat="server" Text="<%$ Resources:Reservaciones_Resources, Ingresar_nueva_Reservacion %>"></asp:Literal></a><br />
            <asp:GridView ID="gv_Reservacions" runat="server" CssClass="tableClean" ShowFooter="false" AllowPaging="true" 
                ShowHeader="true" Width="100%">
                <RowStyle CssClass="tablaResultados_Item" />
                <AlternatingRowStyle CssClass="tablaResultados_Item" />
            </asp:GridView>
            <br />
            <asp:Label ID="lbl_NoReservacions" runat="server" Text="<%$ Resources:Reservaciones_Resources, No_hay_Reservaciones %>"></asp:Label>
            <br />
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="cph_ColumnaDerecha" runat="Server">
    <h1>
        <asp:Literal runat="server" Text="<%$ Resources:Orbecatalog_Resources, Busqueda_Filtros %>"></asp:Literal></h1>
    <div class="Container">
        <h2>
            <asp:Literal runat="server" Text="<%$ Resources:Reservaciones_Resources, Reservacion_Cliente %>"></asp:Literal>
        </h2>
        <asp:DropDownList ID="ddl_clientes" runat="server">
        </asp:DropDownList>
        <br />
        <h2>
            <asp:Literal runat="server" Text="<%$ Resources:Reservaciones_Resources, Reservacion_Ubicacion %>"></asp:Literal>
        </h2>
        <asp:DropDownList ID="ddl_ubicaciones" runat="server">
        </asp:DropDownList>
      <%--  <br />
        <h2>
            <asp:Literal runat="server" Text="<%$ Resources:Reservaciones_Resources, Fecha_Inicio %>"></asp:Literal>
        </h2>
        <BDP:BDPLite ID="bdp_FechaInicio" runat="server" Width="55"></BDP:BDPLite>
        <br />
        <h2>
            <asp:Literal runat="server" Text="<%$ Resources:Reservaciones_Resources, Fecha_Fin %>"></asp:Literal>
        </h2>
        <BDP:BDPLite ID="bdp_FechaFinal" runat="server" Width="55"></BDP:BDPLite>
        <br />--%>
                <br />
        <h2>
            <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:Reservaciones_Resources, Fecha_Inicio %>"></asp:Literal>
        </h2>
        <asp:TextBox runat="server" ID="tbx_FechaInicio"></asp:TextBox>
        <obout:Calendar ID="cln_FechaInicio" runat="server" ShowTimeSelector="true" DatePickerMode="true"
            TextBoxId="tbx_FechaInicio" DatePickerImagePath="../iconos/calendar.gif">
        </obout:Calendar>
        <br />
        <h2>
            <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:Reservaciones_Resources, Fecha_Fin %>"></asp:Literal>
        </h2>
        <asp:TextBox runat="server" ID="tbx_FechaFin"></asp:TextBox>
        <obout:Calendar ID="cln_FechaFin" runat="server" ShowTimeSelector="true" DatePickerMode="true"
            TextBoxId="tbx_FechaFin" DatePickerImagePath="../iconos/calendar.gif">
        </obout:Calendar>
        <br />
        <h2>
            <asp:Literal runat="server" Text="<%$ Resources:Reservaciones_Resources, Estado %>"></asp:Literal>
        </h2>
        <asp:DropDownList ID="ddl_TipoEstado" runat="server">
        </asp:DropDownList>
    </div>
    <br />
    <h1>
        <asp:Literal runat="server" Text="<%$ Resources:Orbecatalog_Resources, Buscar %>"></asp:Literal></h1>
    <div class="Container">
        <asp:Literal runat="server" Text="<%$ Resources:Orbecatalog_Resources, Resultados_Pagina %>"></asp:Literal>
        <asp:DropDownList ID="ddl_PageSize" runat="server" Width="40px">
            <asp:ListItem Selected="True">20</asp:ListItem>
            <asp:ListItem>40</asp:ListItem>
            <asp:ListItem>60</asp:ListItem>
            <asp:ListItem>80</asp:ListItem>
            <asp:ListItem>100</asp:ListItem>
        </asp:DropDownList>
        <br />
        <br />
        <asp:Button ID="btn_Buscar" runat="server" Height="30px" Text="<%$ Resources:Orbecatalog_Resources, Buscar %>"
            Width="150px"></asp:Button>
    </div>
</asp:Content>
