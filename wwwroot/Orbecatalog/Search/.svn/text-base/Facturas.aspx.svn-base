<%@ Page Language="VB" MasterPageFile="~/orbecatalog/OrbeCatalog.master" AutoEventWireup="false"
    CodeFile="Facturas.aspx.vb" Inherits="orbecatalog_Search_Factura" Title="OrbeCatalog - Busqueda - Facturas" %>

<asp:Content ID="ctn_TituloSeccion" ContentPlaceHolderID="cph_Titulo" runat="Server">
    Busqueda de Facturas
</asp:Content>
<asp:Content ID="Publicacion" ContentPlaceHolderID="cph_Contenido" runat="Server">
    <h1>
        Resultados</h1>
    <asp:UpdatePanel ID="upd_Contenido" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:DataGrid ID="dg_Facturas" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                PageSize="20" Width="100%" CssClass="Contenido_Table">
                <AlternatingItemStyle CssClass="tablaResultados_Alternate" />
                <ItemStyle CssClass="tablaResultados_Item" />
                <HeaderStyle CssClass="tablaResultados_Header" />
                <FooterStyle CssClass="tablaResultados_Footer" />
                <PagerStyle Mode="NumericPages" CssClass="tablaResultados_Pager" />
                <Columns>
                    <asp:TemplateColumn>
                        <ItemTemplate>
                            <asp:HyperLink ID="lnk_View" runat="server" Text="Detalle"></asp:HyperLink><br />
                            <br />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Cliente">
                        <ItemTemplate>
                            <asp:Label ID="lbl_Cliente" runat="server"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Estado">
                        <ItemTemplate>
                            <b>
                                <asp:Label ID="lbl_Estado" runat="server"></asp:Label>
                            </b>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Fecha Factura">
                        <ItemTemplate>
                            <asp:Label ID="lbl_Fecha" runat="server"></asp:Label>
                            <br />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Monto">
                        <ItemTemplate>
                            <b>
                                <asp:Label ID="lbl_Monto" runat="server"></asp:Label>
                            </b>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid>
            <asp:Label ID="lbl_ResultadoElementos" runat="server" Text="No hay elementos"></asp:Label>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="cph_Busqueda" ContentPlaceHolderID="cph_ColumnaDerecha" runat="Server">
    <h1>
        Busqueda y Filtros</h1>
    <div class="Container">
        <table border="0" cellpadding="0" cellspacing="0" width="300">
            <tr>
                <td style="width: 30%">
                    Estado Factura:
                </td>
                <td style="width: 68%">
                    <asp:DropDownList ID="ddl_Filtro_Estado" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <br />
                    <br />
                </td>
            </tr>
            <tr>
                <td style="width: 30%">
                    Cliente:
                </td>
                <td style="width: 68%">
                    <asp:DropDownList ID="ddl_cliente" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <br />
                    <br />
                </td>
            </tr>
            <tr id="tr_Producto" runat="server">
                <td>
                    Producto:
                </td>
                <td>
                    <br />
                    <br />
                </td>
                <td>
                    <asp:HyperLink ID="lnk_Producto" runat="server" Text="Producto"></asp:HyperLink>
                </td>
            </tr>
            <tr>
                <td>
                    Fecha Inicio
                </td>
                <td>
                    <BDP:BDPLite ID="bdp_FechaInicio" runat="server" Width="75" />
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    Fecha Final
                </td>
                <td>
                    <BDP:BDPLite ID="bdp_FechaFinal" runat="server" Width="75" />
                </td>
                <td>
                </td>
            </tr>
        </table>
    </div>
    <h1>
        Buscar!</h1>
    <div class="Container">
        Resultados por página
        <asp:DropDownList ID="ddl_PageSize" runat="server" Width="40px">
            <asp:ListItem Selected="True">20</asp:ListItem>
            <asp:ListItem>40</asp:ListItem>
            <asp:ListItem>60</asp:ListItem>
            <asp:ListItem>80</asp:ListItem>
            <asp:ListItem>100</asp:ListItem>
        </asp:DropDownList>
        <br />
        <asp:Button ID="btn_Buscar" runat="server" Height="20px" Text="Buscar" Width="55px">
        </asp:Button>
    </div>
</asp:Content>
