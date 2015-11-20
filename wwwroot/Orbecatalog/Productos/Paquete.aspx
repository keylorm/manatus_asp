<%@ Page Language="VB" MasterPageFile="../OrbeCatalog.master" AutoEventWireup="false"
    ValidateRequest="False" CodeFile="Paquete.aspx.vb" Inherits="_Paquete_Paquetes"
    Title="Orbe Catalog - Paquete" %>

<asp:Content ID="ctn_TituloSeccion" ContentPlaceHolderID="cph_Titulo" runat="Server">
    <asp:Literal runat="server" Text="<%$ Resources:Productos_Resources, Paquetes_Pantalla %>" />
</asp:Content>
<asp:Content ID="ctn_Contenido" ContentPlaceHolderID="cph_Contenido" runat="Server">
    <h1>
        Paquete</h1>
    <asp:UpdatePanel ID="upd_DatosBasicos" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table class="Container" cellspacing="0" cellpadding="0" border="0">
                <tr class="noTR">
                    <td class="tdA">
                    </td>
                    <td class="tdB">
                    </td>
                    <td class="tdC">
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <h2>
                            Datos Basicos</h2>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        Nombre del Paquete
                    </td>
                    <td>
                        <asp:TextBox ID="tbx_NombrePaquete" runat="server"></asp:TextBox>
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="vld_Paquete" runat="server" ErrorMessage="<%$ Resources:Orbecatalog_Resources, Campo_Requerido %>"
                            ControlToValidate="tbx_NombrePaquete" Display="Dynamic" EnableClientScript="False"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        Creado por
                    </td>
                    <td>
                        <asp:Label ID="lbl_Entidad" runat="server" Text="Autor" Visible="False"></asp:Label>
                        <asp:DropDownList ID="ddl_Entidad" runat="server" Visible="False">
                        </asp:DropDownList>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        Unidad de venta
                    </td>
                    <td>
                        <asp:DropDownList ID="ddl_Unidades" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:ImageButton ID="btn_agregarunidad" runat="server" ToolTip="Ingresar nueva unidad"
                            ImageUrl="../iconos/agregar_small.png"></asp:ImageButton>
                    </td>
                </tr>
                <tr>
                    <td>
                        Precio Por Unidad
                    </td>
                    <td>
                        <asp:TextBox ID="tbx_PrecioUnitario" runat="server"></asp:TextBox>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        Activo
                    </td>
                    <td>
                        <asp:CheckBox ID="Chk_Activo" runat="server"></asp:CheckBox>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="upd_Descripcion" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table class="Container" cellspacing="0" cellpadding="0" border="0">
                <tr class="noTR">
                    <td class="tdA">
                    </td>
                    <td class="tdB">
                    </td>
                    <td class="tdC">
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <h2>
                            Descripcion</h2>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <winthusiasm:HtmlEditor ID="tbx_Descripcion" runat="server" Height="200px" />
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="upd_ProductosPaquete" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="pnl_Grupos" runat="server">
                <table class="Container" cellspacing="0" cellpadding="0" border="0">
                    <tr class="noTR">
                        <td class="tdA">
                        </td>
                        <td class="tdB">
                        </td>
                        <td class="tdC">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <h2>
                                Producos por Paquete</h2>
                        </td>
                        <td>
                            <asp:ImageButton ID="btn_AgregarProductos" runat="server" ToolTip="Ingresar nuevo producto"
                                ImageUrl="../iconos/agregar_small.png" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Producto
                        </td>
                        <td>
                            <asp:DropDownList ID="ddl_Productos" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Cantidad
                        </td>
                        <td>
                            <asp:TextBox ID="tbx_CantidadProducto" runat="server"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:Button ID="btn_AgregarProductoAPaquete" runat="server" Text="Agregar a grupo"
                                Width="100" />
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Productos
                        </td>
                        <td>
                            <asp:ListBox ID="lst_Grupos" runat="server"></asp:ListBox>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:Button ID="btn_QuitarProductoDePaquete" runat="server" Text="Quitar producto"
                                Width="100" />
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="upd_Botones" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="botonesAcciones">
                <orbelink:ActionButton ID="btn_Salvar" CausesValidation="true" ImageURL="~/Orbecatalog/iconos/salvar_small.png"
                    runat="server" Text="<%$ Resources:Orbecatalog_Resources, Salvar %>" CssClass="positive">
                </orbelink:ActionButton>
                <orbelink:ActionButton ID="btn_Modificar" CausesValidation="true" ImageURL="~/Orbecatalog/iconos/modificar_small.png"
                    runat="server" Text="<%$ Resources:Orbecatalog_Resources, Salvar_Cambios %>"
                    CssClass="positive">
                </orbelink:ActionButton>
                <orbelink:ActionButton ID="btn_Cancelar" ImageURL="~/Orbecatalog/iconos/cancelar_small.png"
                    runat="server" Text="<%$ Resources:Orbecatalog_Resources, Cancelar %>">
                </orbelink:ActionButton>
                <orbelink:ActionButton ID="btn_Eliminar" ImageURL="~/Orbecatalog/iconos/eliminar_small.png"
                    runat="server" Text="<%$ Resources:Orbecatalog_Resources, Eliminar %>" CssClass="negative">
                </orbelink:ActionButton>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="cph_Busqueda" ContentPlaceHolderID="cph_ColumnaDerecha" runat="Server">
    <h1>
        En Esta Seccion</h1>
    <div class="Seccion_Busqueda_Ayuda">
        Puede crear y editar la información de los <b>Paquete</b>.
    </div>
    <h1>
        Moneda Actuales</h1>
    <asp:UpdatePanel ID="upd_DGPaquetes" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="Container">
                <asp:DataGrid ID="dg_Paquetes" runat="server" AutoGenerateColumns="False" AllowPaging="true"
                    PageSize="10" CssClass="tablaResultados">
                    <AlternatingItemStyle CssClass="tablaResultados_Alternate" />
                    <ItemStyle CssClass="tablaResultados_Item" />
                    <HeaderStyle CssClass="tablaResultados_Header" />
                    <FooterStyle CssClass="tablaResultados_Footer" />
                    <PagerStyle Mode="NumericPages" CssClass="tablaResultados_Pager" />
                    <Columns>
                        <asp:EditCommandColumn CancelText="Cancel" EditText="Edit" UpdateText="Update"></asp:EditCommandColumn>
                        <asp:TemplateColumn HeaderText="Paquete">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Nombre" runat="server" Text="Label"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                </asp:DataGrid>
                <asp:Label ID="lbl_ResultadoElementos" runat="server" Text="No hay elementos"></asp:Label>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
