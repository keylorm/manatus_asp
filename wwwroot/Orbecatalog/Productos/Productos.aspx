<%@ Page Language="VB" MasterPageFile="../OrbeCatalog.master" AutoEventWireup="false"
    ValidateRequest="False" CodeFile="Productos.aspx.vb" Inherits="Orbecatalog_Productos"
    Title="Orbe Catalog - Productos" %>

<%@ Register TagPrefix="orbelinkTest" TagName="DetalleProducto" Src="~/Controls/DetalleProducto.ascx" %>
<%@ Register TagPrefix="orbelinkTest" TagName="Control_Producto_Entidad" Src="~/Controls/Rel_Producto_Entidad.ascx" %>
<%@ Register TagPrefix="orbelinkTest" TagName="Control_Producto_Publicacion" Src="~/Controls/Rel_Producto_Publicacion.ascx" %>
<%@ Register TagPrefix="orbelinkTest" TagName="DireccionesProducto" Src="~/Controls/DireccionesProducto.ascx" %>
<asp:Content ID="ctn_TituloSeccion" ContentPlaceHolderID="cph_Titulo" runat="Server">
    <asp:Panel ID="pnl_editarTitulo" runat="server">
        <asp:Literal runat="server" Text="<%$ Resources:Productos_Resources, Producto_Pantalla %>" />
    </asp:Panel>
    <asp:Panel ID="pnl_verTitulo" runat="server" Visible="false">
        <h1>
            <asp:Label ID="lbl_tipoTitulo" runat="server" Text="Tipo:"></asp:Label>:</h1>
        <h2>
            <asp:Label ID="lbl_nombreTitulo" runat="server" Text="Producto"></asp:Label></h2>
    </asp:Panel>
</asp:Content>
<asp:Content ID="ctn_Contenido" ContentPlaceHolderID="cph_Contenido" runat="Server">
    <asp:UpdatePanel ID="upd_PantallaContenido" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <orbelinkTest:DetalleProducto runat="server" ID="ocwc_DetalleProducto"></orbelinkTest:DetalleProducto>
            <asp:Panel ID="pnl_editar" runat="server">
                <ajaxToolkit:TabContainer ID="tabs_Productos" runat="server">
                    <ajaxToolkit:TabPanel ID="tab_Detalle" runat="server" HeaderText="Detalle de Producto">
                        <ContentTemplate>
                            <asp:UpdatePanel ID="upd_Contenido" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="Container fieldset">
                                        <div>
                                            <h2>
                                                Tipo Producto</h2>
                                        </div>
                                        <div>
                                            <label>
                                                Seleccionada
                                            </label>
                                            <asp:HyperLink ID="lnk_TipoProducto" runat="server" Text="Sin Asignar"></asp:HyperLink>
                                            <asp:HyperLink ID="Hyl_AregarTipoProducto" ToolTip="Ingresar nuevo tipo de Producto"
                                                ImageUrl="../iconos/agregar_small.png" runat="server"></asp:HyperLink>
                                            <asp:CustomValidator ID="vld_TipoProducto" runat="server" Display="Dynamic" ErrorMessage="<%$ Resources:Orbecatalog_Resources, Campo_Requerido %>"
                                                ValidationGroup="01"></asp:CustomValidator>
                                        </div>
                                        <div>
                                            <label>
                                                &nbsp;</label>
                                            <asp:TreeView ID="trv_TipoProducto" runat="server" Style="display: none">
                                            </asp:TreeView>
                                        </div>
                                        <div>
                                            <h2>
                                                Autor</h2>
                                        </div>
                                        <div>
                                            <label>
                                                Entidad
                                            </label>
                                            <asp:Label ID="lbl_Entidad" runat="server"></asp:Label>
                                        </div>
                                        <div>
                                            <label>
                                                &nbsp;
                                            </label>
                                        </div>
                                        <div>
                                            <h2>
                                                Datos Basicos</h2>
                                        </div>
                                        <div>
                                            <label>
                                                Nombre del producto
                                            </label>
                                            <asp:TextBox ID="tbx_NombreProducto" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="vld_Producto" runat="server" ErrorMessage="<%$ Resources:Orbecatalog_Resources, Campo_Requerido %>"
                                                ControlToValidate="tbx_NombreProducto" Display="Dynamic" EnableClientScript="False"
                                                ValidationGroup="01"></asp:RequiredFieldValidator>
                                        </div>
                                        <div>
                                            <label>
                                                SKU
                                            </label>
                                            <asp:TextBox ID="tbx_Codigo" runat="server"></asp:TextBox>
                                        </div>
                                        <div>
                                            <label>
                                                Unidad de venta
                                            </label>
                                            <asp:DropDownList ID="ddl_Unidades" runat="server">
                                            </asp:DropDownList>
                                            <asp:HyperLink ID="Hyl_agregarunidad" ToolTip="Ingresar nueva unidad" ImageUrl="../iconos/agregar_small.png"
                                                runat="server"></asp:HyperLink>
                                        </div>
                                        <div>
                                            <label>
                                                Precio Por Unidad
                                            </label>
                                            <asp:TextBox ID="tbx_PrecioUnitario" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="vld_tbx_PrecioUnitario" runat="server" ErrorMessage="<%$ Resources:Orbecatalog_Resources, Campo_Requerido %>"
                                                ControlToValidate="tbx_PrecioUnitario" Display="Dynamic" EnableClientScript="False"
                                                ValidationGroup="01"></asp:RequiredFieldValidator>
                                        </div>
                                        <div>
                                            <label>
                                                Capacidad
                                            </label>
                                            <asp:TextBox ID="tbx_Capacidad" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="vld_tbx_Capacidad" runat="server" ErrorMessage="<%$ Resources:Orbecatalog_Resources, Campo_Requerido %>"
                                                ControlToValidate="tbx_Capacidad" Display="Dynamic" EnableClientScript="False"
                                                ValidationGroup="01"></asp:RequiredFieldValidator>
                                        </div>
                                        <div>
                                            <label>
                                                Capacidad Maxima
                                            </label>
                                            <asp:TextBox ID="tbx_CapacidadMaxima" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="vld_tbx_CapacidadMaxima" runat="server" ErrorMessage="<%$ Resources:Orbecatalog_Resources, Campo_Requerido %>"
                                                ControlToValidate="tbx_CapacidadMaxima" Display="Dynamic" EnableClientScript="False"
                                                ValidationGroup="01"></asp:RequiredFieldValidator>
                                        </div>
                                        <div>
                                            <label>
                                                Estado
                                            </label>
                                            <asp:DropDownList ID="ddl_Estados" runat="server">
                                            </asp:DropDownList>
                                            <asp:HyperLink ID="Hyl_AgregarEstado" ToolTip="Ingresar nuevo estado" ImageUrl="../iconos/agregar_small.png"
                                                runat="server"></asp:HyperLink>
                                        </div>
                                        <div>
                                            <label>
                                                Activo
                                            </label>
                                            <asp:CheckBox ID="Chk_Activo" runat="server"></asp:CheckBox>
                                        </div>
                                    </div>
                                    <asp:UpdatePanel ID="upd_Origen" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <div class="Container fieldset">
                                                <div>
                                                    <h2>
                                                        Origen</h2>
                                                </div>
                                                <div>
                                                    <label>
                                                        Seleccionada</label>
                                                    <asp:HyperLink ID="lnk_Origen" runat="server" Text="Sin Asignar"></asp:HyperLink>
                                                    <asp:HyperLink ID="Hyl_AgregarOrigen" ToolTip="Ingresar nuevo origen" ImageUrl="../iconos/agregar_small.png"
                                                        runat="server"></asp:HyperLink>
                                                    <asp:CustomValidator ID="vld_Origen" runat="server" Display="Dynamic" ErrorMessage="<%$ Resources:Orbecatalog_Resources, Campo_Requerido %>"
                                                        ValidationGroup="01"></asp:CustomValidator>
                                                </div>
                                                <div>
                                                    <asp:TreeView ID="trv_Origenes" runat="server" Style="display: none;">
                                                    </asp:TreeView>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <asp:UpdatePanel ID="upd_KeywordsYAtributos" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <div class="Container fieldset">
                                                <div>
                                                    <h2>
                                                        Keywords</h2>
                                                    <asp:HyperLink ID="Hyl_AgregarKeyword" ToolTip="Ingresar nuevo Keyword" ImageUrl="../iconos/agregar_small.png"
                                                        runat="server"></asp:HyperLink>
                                                </div>
                                                <div>
                                                    <asp:DataList ID="dtl_Keywords" runat="server" Width="100%" RepeatColumns="5" RepeatDirection="Horizontal">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chk_Keyword" runat="server" /><br />
                                                            <asp:Label ID="lbl_Keyword" runat="server" Text="Label"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:DataList>
                                                </div>
                                                <div>
                                                    &nbsp;
                                                </div>
                                                <div>
                                                    <h2>
                                                        Otros Atributos</h2>
                                                    <asp:HyperLink ID="Hyl_AgregarAtVar" ToolTip="Manejar atributos por tipo de producto"
                                                        ImageUrl="../iconos/agregar_small.png" runat="server"></asp:HyperLink>
                                                </div>
                                                <div>
                                                    <asp:DataGrid ID="dg_Atributos" runat="server" AutoGenerateColumns="False" CellPadding="0"
                                                        Width="100%" CssClass="Contenido_Table">
                                                        <FooterStyle CssClass="tablaResultados_Footer" />
                                                        <AlternatingItemStyle CssClass="tablaResultados_Alternate" />
                                                        <ItemStyle CssClass="tablaResultados_Item" />
                                                        <HeaderStyle CssClass="tablaResultados_Header" />
                                                        <Columns>
                                                            <asp:TemplateColumn HeaderText="Nombre">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbl_Atributo" runat="server">lbl_Atributos_Nombre</asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="20%" />
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="Valor">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="tbx_Atributos_Valor" runat="server"></asp:TextBox>
                                                                    <asp:DropDownList ID="ddl_Atributos_Valores" runat="server" DataTextField="Valor"
                                                                        Visible="False">
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="60%" />
                                                            </asp:TemplateColumn>
                                                            <asp:EditCommandColumn CancelText="Cancel" EditText="Otro" UpdateText="Update">
                                                                <ItemStyle Width="10%" />
                                                            </asp:EditCommandColumn>
                                                            <asp:TemplateColumn HeaderText="Visible">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chk_Atributo_Visible" runat="server" />
                                                                </ItemTemplate>
                                                                <ItemStyle Width="10%" />
                                                            </asp:TemplateColumn>
                                                        </Columns>
                                                    </asp:DataGrid>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <asp:UpdatePanel ID="upd_Descripciones" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <div class="Container">
                                                <h2>
                                                    Descripcion Corta</h2>
                                                <br />
                                                <winthusiasm:HtmlEditor ID="tbx_DescripcionCorta" runat="server" Height="200px" />
             <%--                                   <textarea id="elm1" name="elm1" rows="15" cols="80" style="width: 80%" runat="server">
		
	</textarea>--%>
                                                <br />
                                                <h2>
                                                    Descripcion Larga
                                                </h2>
                                                <br />
                                                <winthusiasm:HtmlEditor ID="tbx_DescripcionLarga" runat="server" Height="200px" />
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <div class="Container fieldset">
                                        <div class="botonesAcciones">
                                            <label>
                                                &nbsp;</label>
                                            <orbelink:ActionButton ID="btn_Salvar" CausesValidation="true" Text="Salvar" runat="server"
                                                ImageURL="~/Orbecatalog/iconos/salvar_small.png" />
                                            <orbelink:ActionButton ID="btn_Modificar" CausesValidation="true" Text="Modificar"
                                                runat="server" ImageURL="~/Orbecatalog/iconos/salvar_small.png" />
                                            <orbelink:ActionButton ID="btn_cancelar" Text="Cancelar" runat="server" ImageURL="~/Orbecatalog/iconos/cancelar_small.png" />
                                            <orbelink:ActionButton ID="btn_eliminar" Text="Eliminar" runat="server" ImageURL="~/Orbecatalog/iconos/eliminar_small.png" />
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel ID="tab_Archivos" runat="server" HeaderText="Archivos de Producto">
                        <ContentTemplate>
                            <iframe style="border: solid 1px white; width: 100%; height: 400px" frameborder="0"
                                scrolling="auto" id="if_Archivos" runat="server"></iframe>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel ID="tab_grupos" runat="server" HeaderText="Grupos">
                        <ContentTemplate>
                            <asp:UpdatePanel ID="upd_Grupos" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:Panel ID="pnl_Grupos" runat="server">
                                        <h1>
                                            Grupos</h1>
                                        <div class="Container">
                                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                <tr>
                                                    <td style="width: 95%">
                                                        Grupo
                                                    </td>
                                                    <td style="width: 5%">
                                                        <asp:HyperLink ID="Hyp_agregarGrupo" Width="13" Height="11" ToolTip="Ingresar nuevo grupo"
                                                            ImageUrl="../images/buttons/agregar.gif" runat="server"></asp:HyperLink>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:HyperLink ID="lnk_Grupo" runat="server">(Sin Asignar)</asp:HyperLink>
                                                        <asp:TreeView ID="trv_Grupos" runat="server">
                                                        </asp:TreeView>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Tipo Relacion
                                                    </td>
                                                    <td>
                                                        <asp:HyperLink ID="Hyp_tipoRelacionGrupos" ImageUrl="../images/buttons/agregar.gif"
                                                            Width="13" Height="11" ToolTip="Ingresar nuevo tipo relacion" runat="server"></asp:HyperLink>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:DropDownList ID="ddl_TipoRelacion_Grupos" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                            </table>
                                            <br />
                                            <asp:Button ID="btn_AgregarAGrupo" runat="server" Text="Agregar a grupo" Width="100" />
                                            <br />
                                            <br />
                                            <h2>
                                                Perteneciente</h2>
                                            <asp:ListBox ID="lst_Grupos" runat="server"></asp:ListBox>
                                            <br />
                                            <asp:Button ID="btn_QuitarDeGrupo" runat="server" Text="Quitar de grupo" Width="100" />
                                            <asp:Button ID="btn_ListaGrupo" runat="server" Text="Ver lista de grupo" Width="100" />
                                        </div>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel ID="tab_Direcciones" runat="server" HeaderText="Direcciones">
                        <ContentTemplate>
                            <h1>
                                Direcciones</h1>
                            <asp:HyperLink ID="lnk_NuevaDireccion" runat="server">Agregar direccion nueva</asp:HyperLink>
                            <orbelinkTest:DireccionesProducto ID="ocwc_ProductoDirecciones" runat="server"></orbelinkTest:DireccionesProducto>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                </ajaxToolkit:TabContainer>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="cph_Busqueda" ContentPlaceHolderID="cph_ColumnaDerecha" runat="Server">
    <orbelinkTest:Control_Producto_Entidad ID="ocwc_Producto_Entidad" runat="server" />
    <orbelinkTest:Control_Producto_Publicacion ID="ocwc_Producto_Publicacion" runat="server" />
   <%-- <orbelinkTest:Control_Producto_Proyecto ID="ocwc_Producto_Proyecto" runat="server" />--%>
    <%--<orbelinkTest:Control_Producto_Cuenta ID="ocwc_Producto_cuenta" runat="server" />--%>
</asp:Content>
<asp:Content ID="ctn_Acciones" ContentPlaceHolderID="cph_Acciones" runat="server">
    <asp:UpdatePanel ID="upd_BotonesAcciones" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <orbelink:ActionButton ID="btn_Accion_Ver" ImageURL="~/Orbecatalog/iconos/ver_small.png"
                runat="server" Text="Ver ">
            </orbelink:ActionButton>
            <orbelink:ActionButton ID="btn_Accion_Editar" ImageURL="~/Orbecatalog/iconos/editar_small.png"
                runat="server" Text="Editar ">
            </orbelink:ActionButton>
            <orbelink:ActionButton ID="btn_IngresarNuevo" ImageURL="~/Orbecatalog/iconos/agregar_small.png"
                runat="server" Text="Nuevo">
            </orbelink:ActionButton>
            <orbelink:ActionButton ID="btn_Accion_Ayuda" ImageURL="~/Orbecatalog/iconos/info_small.png"
                runat="server" Text="Ayuda ">
            </orbelink:ActionButton>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
