<%@ Page Language="VB" MasterPageFile="~/Orbecatalog/OrbeCatalog.master" AutoEventWireup="false"
    CodeFile="Administracion.aspx.vb" Inherits="Orbecatalog_Reservacion_Administracion"
    Title="Adminitración Reservaciones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_Titulo" runat="Server">
    Administracion Reservaciones
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_Acciones" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cph_Contenido" runat="Server">
    <asp:UpdatePanel ID="upd_admi" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table class="Container" cellspacing="0" cellpadding="0" border="0">
                <tr>
                    <td style="width: 25%">
                        Producto:
                    </td>
                    <td style="width: 75%">
                        <asp:DropDownList ID="ddl_productos" runat="server">
                        </asp:DropDownList>
                          <asp:HyperLink ID="Hyl_AgregarProducto" ToolTip="Ingresar nuevo producto" ImageUrl="../iconos/agregar_small.png"
                                                runat="server"></asp:HyperLink>
                    </td>
                </tr>
                <tr>
                    <td>
                        Fecha Inicio:
                    </td>
                    <td>
                        <BDP:BDPLite ID="bdp_FechaInicio" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Fecha Final:
                    </td>
                    <td>
                        <BDP:BDPLite ID="bdp_FechaFinal" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Estado:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddl_estados" runat="server">
                        </asp:DropDownList>
                          <asp:HyperLink ID="Hyl_AgregarEstado" ToolTip="Ingresar nuevo estado" ImageUrl="../iconos/agregar_small.png"
                                                runat="server"></asp:HyperLink>
                    </td>
                </tr>
                <tr>
                    <td>
                        Cliente:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddl_cliente" runat="server">
                        </asp:DropDownList>
                         <asp:HyperLink ID="Hyl_AgregarEntidad" ToolTip="Ingresar nueva entidad"
                                                ImageUrl="../iconos/agregar_small.png" runat="server"></asp:HyperLink>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <div class="botonesAcciones" style="padding-left: 100px">
                            <br />
                            <orbelink:ActionButton ID="btn_buscar" Text="Buscar" runat="server" />
                            <orbelink:ActionButton ID="btn_limpiar" Text="Limpiar" runat="server" />
                        </div>
                    </td>
                </tr>
            </table>
            <div id="verReservaciones" visible="false" runat="server" style="padding-top: 20px">
                <asp:Label ID="Label7" runat="server" Font-Bold="true" Text=" Resultados Disponibles"></asp:Label>
                <br />
                <br />
                <asp:Label ID="lbl_noDisponibles" Style="padding-left: 50px" runat="server" Visible="false"
                    Text="No hay resultados"></asp:Label>
                <asp:GridView ID="dg_reservaciones" runat="server" AutoGenerateColumns="false" Width="100%"
                    GridLines="None">
                    <AlternatingRowStyle CssClass="tablaResultados_Alternate" />
                    <Columns>
                        <asp:TemplateField HeaderText="Cliente">
                            <ItemTemplate>
                                <asp:Label ID="lbl_nombre" runat="server" Text="Nombre"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Fecha Inicio">
                            <ItemTemplate>
                                <asp:Label ID="lbl_fechaInicio" runat="server" Text="inicio"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Fecha Final">
                            <ItemTemplate>
                                <asp:Label ID="lbl_fechaFin" runat="server" Text="final"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cantidad">
                            <ItemTemplate>
                                <asp:Label ID="lbl_cantidad" runat="server" Text="cantidad"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Estado">
                            <ItemTemplate>
                                <asp:Label ID="lbl_estado" runat="server" Text="estado"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Total">
                            <ItemTemplate>
                                <asp:Label ID="lbl_total" runat="server" Text="total"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Ver Detalle">
                            <ItemTemplate>
                                <asp:HyperLink ID="hyl_detalle" runat="server">Ver</asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <br />
                <br />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="cph_ColumnaDerecha" runat="Server">
</asp:Content>
