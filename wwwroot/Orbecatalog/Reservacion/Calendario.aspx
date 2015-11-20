<%@ Page Language="VB" MasterPageFile="~/Orbecatalog/OrbeCatalog.master" AutoEventWireup="false"
    CodeFile="Calendario.aspx.vb" Inherits="Orbecatalog_Reservacion_Calendario" Title="Calendario"
    EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_Titulo" runat="Server">
    <h1>
        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:Reservaciones_Resources, CalendarioProductos_Pantalla %>"></asp:Literal>
    </h1>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_Navegacion" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_Acciones" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cph_Contenido" runat="Server">
    <table cellpadding="0" cellspacing="3" width="100%">
        <tr>
            <td style="width: 20%">
                <b>
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:Productos_Resources, Producto %>"></asp:Literal>:
                </b>
            </td>
            <td style="width: 80%">
                <asp:DropDownList ID="ddl_productos" runat="server" Width="50%" AutoPostBack="true">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <b>
                    <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:Reservaciones_Resources, Mes %>"></asp:Literal>:
                </b>
            </td>
            <td>
                <asp:DropDownList ID="ddl_meses" runat="server" Width="50%" AutoPostBack="true">
                    <asp:ListItem Value="1">Enero</asp:ListItem>
                    <asp:ListItem Value="2">Febrero</asp:ListItem>
                    <asp:ListItem Value="3">Marzo</asp:ListItem>
                    <asp:ListItem Value="4">Abril</asp:ListItem>
                    <asp:ListItem Value="5">Mayo</asp:ListItem>
                    <asp:ListItem Value="6">Junio</asp:ListItem>
                    <asp:ListItem Value="7">Julio</asp:ListItem>
                    <asp:ListItem Value="8">Agosto</asp:ListItem>
                    <asp:ListItem Value="9">Septiembre</asp:ListItem>
                    <asp:ListItem Value="10">Octubre</asp:ListItem>
                    <asp:ListItem Value="11">Noviembre</asp:ListItem>
                    <asp:ListItem Value="12">Diciembre</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <b>
                    <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:Reservaciones_Resources, Ano %>"></asp:Literal>:
                </b>
            </td>
            <td>
                <asp:DropDownList ID="ddl_ano" runat="server" Width="50%" AutoPostBack="true">
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <br />
    <br />
    <asp:DataList ID="DL_calendario" runat="server" RepeatDirection="Horizontal" CellPadding="0"
        CellSpacing="0" BorderStyle="None" BorderWidth="0" Width="90%">
        <ItemTemplate>
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td class="bordeAbajoGris">
                        <asp:Panel ID="fecha" Width="17px" runat="server" Style="text-align: center">
                            <div>
                                <asp:Label ID="lbl_fecha" Font-Size="12px" ForeColor="#87328C" runat="server" Text="1"></asp:Label>
                            </div>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="dia" runat="server" Width="17px" Style="text-align: center">
                            <asp:Label ID="lbl_dia" runat="server" Text="L"></asp:Label>
                        </asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Panel ID="pnl_items" CssClass="bordeIzqGris" runat="server" Width="17px" Style="text-align: center">
                            <asp:DataList ID="dl_cantItem" runat="server" RepeatDirection="Vertical">
                                <ItemTemplate>
                                    <asp:Panel ID="diaItem" Width="17px" Height="15px" runat="server" BackColor="#DADADA">
                                        <asp:HyperLink ID="hyl_detalle" runat="server" Width="100%" Height="100%"></asp:HyperLink>
                                    </asp:Panel>
                                </ItemTemplate>
                            </asp:DataList>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </ItemTemplate>
    </asp:DataList>
    <asp:Label ID="lbl_vacio" Font-Bold="true" runat="server" Text="No Posee Items" Visible="false"></asp:Label>
    <br />
    <br />
    <br />
    <b>
        <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:Reservaciones_Resources, Explicacion_items %>"></asp:Literal>
    </b>
    <br />
    <b>
        <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:Reservaciones_Resources, dias_reservados %>"></asp:Literal>
    </b>
    <br />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cph_BusquedaNueva" runat="Server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="cph_ColumnaDerecha" runat="Server">
</asp:Content>
