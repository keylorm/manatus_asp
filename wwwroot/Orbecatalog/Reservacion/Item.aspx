<%@ Page Language="VB" MasterPageFile="~/Orbecatalog/OrbeCatalog.master" AutoEventWireup="false"
    CodeFile="Item.aspx.vb" Inherits="Orbecatalog_Reservacion_Item" Title="Items" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_Titulo" runat="Server">

    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:Reservaciones_Resources, Item_Pantalla %>"></asp:Literal>:
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_Acciones" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cph_Contenido" runat="Server">
    <asp:UpdatePanel ID="udp_contenido" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table class="Container" cellspacing="0" cellpadding="0" border="0">
                <tr>
                    <td style="width: 25%">
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:Productos_Resources, Producto %>"></asp:Literal>:
                        <asp:HiddenField ID="hf_ordinal" runat="server" />
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
                        SKU:
                    </td>
                    <td>
                        <asp:TextBox ID="txb_sku" runat="server" ValidationGroup="01"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                     <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:Productos_Resources, Cantidad_Agregar %>"></asp:Literal>
                       :
                    </td>
                    <td>
                        <asp:TextBox ID="txb_cantidad" runat="server" ValidationGroup="01"></asp:TextBox><asp:RequiredFieldValidator
                            ID="vld_txb_cantidad" runat="server" Display="Dynamic" ControlToValidate="txb_cantidad"
                            ValidationGroup="01"><br />Debe seleccionar una cantidad a agregar</asp:RequiredFieldValidator>
                        <ajaxToolkit:FilteredTextBoxExtender ID="filter_txb_cantidad" runat="server" FilterType="Numbers"
                            FilterMode="ValidChars" TargetControlID="txb_cantidad">
                        </ajaxToolkit:FilteredTextBoxExtender>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <br />
                        <br />
                         <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:Productos_Resources, Item_Descripcion %>"></asp:Literal>
                    
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <winthusiasm:HtmlEditor ID="tbx_Descripcion" runat="server" Height="200px" />
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <table id="tablaBotones" class="botonesAcciones" cellspacing="0" cellpadding="0"
                            border="0">
                            <tr>
                                <td>
                                    <orbelink:ActionButton ID="btn_Salvar" CausesValidation="true" ImageURL="~/Orbecatalog/iconos/salvar_small.png"
                                        runat="server" Text="<%$ Resources:Orbecatalog_Resources, Salvar %>" CssClass="positive">
                                    </orbelink:ActionButton>
                                </td>
                                <td>
                                    <orbelink:ActionButton ID="btn_Modificar" CausesValidation="true" ImageURL="~/Orbecatalog/iconos/modificar_small.png"
                                        runat="server" Text="<%$ Resources:Orbecatalog_Resources, Salvar_Cambios %>"
                                        CssClass="positive">
                                    </orbelink:ActionButton>
                                </td>
                                <td>
                                    <orbelink:ActionButton ID="btn_Cancelar" ImageURL="~/Orbecatalog/iconos/cancelar_small.png"
                                        runat="server" Text="<%$ Resources:Orbecatalog_Resources, Cancelar %>">
                                    </orbelink:ActionButton>
                                </td>
                                <td>
                                    <orbelink:ActionButton ID="btn_Eliminar" ImageURL="~/Orbecatalog/iconos/eliminar_small.png"
                                        runat="server" Text="<%$ Resources:Orbecatalog_Resources, Eliminar %>" CssClass="negative">
                                    </orbelink:ActionButton>
                                </td>
                            </tr>
                        </table>
                        <asp:ValidationSummary ID="vld_Summary" runat="server" ValidationGroup="01" DisplayMode="BulletList"
                            ShowSummary="true" EnableClientScript="false" HeaderText="Revise estos errores" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cph_BusquedaNueva" runat="Server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="cph_ColumnaDerecha" runat="Server">
    <asp:UpdatePanel ID="udp_columnaDere" runat="server">
        <ContentTemplate>
            <h1>
               <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:Productos_Resources, Items_Actuales %>"></asp:Literal>
               
            </h1>
            <div class="container">
                  <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:Productos_Resources, Producto %>"></asp:Literal>:
                <br />
                <asp:DropDownList ID="ddl_productosDer" runat="server" AutoPostBack="True">
                </asp:DropDownList>
                <br />
                <br />
                <asp:GridView ID="gv_buscarItems" runat="server" AutoGenerateColumns="false" Width="100%"
                    GridLines="None">
                    <HeaderStyle HorizontalAlign="Center" />
                    <AlternatingRowStyle CssClass="tablaResultados_Alternate" />
                    <Columns>
                        <asp:CommandField ShowSelectButton="true" SelectText="<%$ Resources:Productos_Resources, Ver %>" />
                        <asp:TemplateField HeaderText="<%$ Resources:Productos_Resources, Numero %>" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lbl_ordinal" runat="server" Text="Ordinal"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="<%$ Resources:Productos_Resources, Item_Descripcion %>" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lbl_descripcion" runat="server" Text="descripcion"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
