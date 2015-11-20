<%@ Page Language="VB" MasterPageFile="~/Orbecatalog/OrbeCatalog.master" AutoEventWireup="false"
    CodeFile="preciosTemporada.aspx.vb" Inherits="Orbecatalog_Reservacion_preciosTemporada"
    Title="OrbeCatalog - Reservacion - Precios Temporada" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_Titulo" runat="Server">
    <asp:Literal runat="server" Text="<%$ Resources:Reservaciones_Resources, Temporada_Precios %>"></asp:Literal>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_Navegacion" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_Acciones" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cph_Contenido" runat="Server">
    <h1>
        <asp:Literal runat="server" Text="<%$ Resources:Reservaciones_Resources, Temporada %>"></asp:Literal></h1>
    <asp:UpdatePanel ID="upd_Contenido" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="fieldset">
                <div>
                    <label>
                        <asp:Literal runat="server" Text="<%$ Resources:Reservaciones_Resources, Temporada %>"></asp:Literal>
                    </label>
                  <%--   <asp:Label ID="lbl_temporada" runat="server" Text=""></asp:Label>--%>
                 <asp:DropDownList ID="ddl_temporada" AutoPostBack="true" runat="server">
                    </asp:DropDownList>
                                <asp:HyperLink ID="Hyl_AgregarTemporada" ToolTip="Ingresar nueva temporada"
                                                ImageUrl="../iconos/agregar_small.png" runat="server"></asp:HyperLink>
                </div>
                <div>
                    <label>
                        <asp:Literal runat="server" Text="<%$ Resources:Productos_Resources, Producto %>"></asp:Literal>
                    </label>
                    <asp:DropDownList ID="ddl_producto" AutoPostBack="true" runat="server">
                    </asp:DropDownList>
                          <asp:HyperLink ID="Hyl_AgregarProducto" ToolTip="Ingresar nuevo producto" ImageUrl="../iconos/agregar_small.png"
                                                runat="server"></asp:HyperLink>
                </div>
                <div>
                    &nbsp;
                </div>
                <div>
                    <h2>
                        <asp:Literal runat="server" Text="<%$ Resources:Reservaciones_Resources, precios %>"></asp:Literal></h2>
                    <table cellpadding="0">
                        <tr>
                            <td style="width: 120px">
                                <asp:Literal runat="server" Text="<%$ Resources:Reservaciones_Resources, Temporada_precioUnidad %>"></asp:Literal>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddl_unidades" Width="73px" runat="server">
                                </asp:DropDownList>
                                         <asp:HyperLink ID="Hyl_agregarunidad" ToolTip="Ingresar nueva unidad" ImageUrl="../iconos/agregar_small.png"
                                                runat="server"></asp:HyperLink>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal runat="server" Text="<%$ Resources:Reservaciones_Resources, Temporada_unidadTiempo %>"></asp:Literal>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddl_unidadTiempo" Width="73px" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal runat="server" Text="<%$ Resources:Reservaciones_Resources, Temporada_precioProducto %>"></asp:Literal>
                            </td>
                            <td>
                                <asp:TextBox ID="txb_precioProducto" Width="70px" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal runat="server" Text="<%$ Resources:Reservaciones_Resources, Temporada_precioAdulto %>"></asp:Literal>
                            </td>
                            <td>
                                <asp:TextBox ID="txb_precioAdulto" runat="server" Width="70px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal runat="server" Text="<%$ Resources:Reservaciones_Resources, Temporada_precioNino %>"></asp:Literal>
                            </td>
                            <td>
                                <asp:TextBox ID="txb_precioNino" runat="server" Width="70px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal runat="server" Text="<%$ Resources:Reservaciones_Resources, Temporada_precioAdultoExtra %>"></asp:Literal>
                            </td>
                            <td>
                                <asp:TextBox ID="txb_precioAdultoExtra" runat="server" Width="70px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Literal runat="server" Text="<%$ Resources:Reservaciones_Resources, Temporada_precioNinoExtra %>"></asp:Literal>
                            </td>
                            <td>
                                <asp:TextBox ID="txb_precioNinoExtra" runat="server" Width="70px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
                <div>
                    &nbsp;
                </div>
                <div class="botonesAcciones">
                    <label>
                        &nbsp;
                    </label>
                    <orbelink:ActionButton ID="btn_Salvar" CausesValidation="true" ImageURL="~/Orbecatalog/iconos/salvar_small.png"
                        runat="server" Text="<%$ Resources:Orbecatalog_Resources, Salvar %>" CssClass="positive">
                    </orbelink:ActionButton>
                    <%--<orbelink:ActionButton ID="btn_Modificar" CausesValidation="true" ImageURL="~/Orbecatalog/iconos/modificar_small.png"
                        runat="server" Text="<%$ Resources:Orbecatalog_Resources, Salvar_Cambios %>"
                        CssClass="positive">
                    </orbelink:ActionButton>--%>
                    <orbelink:ActionButton ID="btn_Cancelar" ImageURL="~/Orbecatalog/iconos/cancelar_small.png"
                        runat="server" Text="<%$ Resources:Orbecatalog_Resources, Cancelar %>">
                    </orbelink:ActionButton>
                    <%--<orbelink:ActionButton ID="btn_Eliminar" ImageURL="~/Orbecatalog/iconos/eliminar_small.png"
                        runat="server" Text="<%$ Resources:Orbecatalog_Resources, Eliminar %>" CssClass="negative">
                    </orbelink:ActionButton>--%>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cph_BusquedaNueva" runat="Server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="cph_ColumnaDerecha" runat="Server">
    <div style="display:none">
  <h1>
        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:Reservaciones_Resources, Temporada_Actuales %>"></asp:Literal></h1>
    <asp:UpdatePanel ID="upd_Busqueda" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="Container">
                <asp:DataGrid ID="dg_Temporada" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                    PageSize="10" CssClass="tablaResultados">
                    <AlternatingItemStyle CssClass="tablaResultados_Alternate" />
                    <ItemStyle CssClass="tablaResultados_Item" />
                    <HeaderStyle CssClass="tablaResultados_Header" />
                    <FooterStyle CssClass="tablaResultados_Footer" />
                    <PagerStyle Mode="NumericPages" CssClass="tablaResultados_Pager" />
                    <Columns>
                        <asp:EditCommandColumn CancelText="Cancel" EditText="Edit" UpdateText="Update"></asp:EditCommandColumn>
                        <asp:TemplateColumn HeaderText="<%$ Resources:Reservaciones_Resources, Temporada %>">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Nombre" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                </asp:DataGrid>
                <asp:Label ID="lbl_ResultadoElementos" runat="server" Text="<%$ Resources:Reservaciones_Resources, TipoEstado_NoHay %>"></asp:Label>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </div>
  
</asp:Content>
