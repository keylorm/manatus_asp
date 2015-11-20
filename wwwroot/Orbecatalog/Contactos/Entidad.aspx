<%@ Page Language="VB" MasterPageFile="../OrbeCatalog.master" AutoEventWireup="false"
    CodeFile="Entidad.aspx.vb" Inherits="Orbecatalog_Entidades_Entidad" Title="Orbe Catalog - Entidad" %>

<%@ Register TagPrefix="orbelinkTest" TagName="DetalleEntidad" Src="~/Controls/DetalleEntidad.ascx" %>
<%@ Register TagPrefix="orbelinkTest" TagName="Ubicacion" Src="~/Controls/Ubicacion.ascx" %>
<%@ Register TagPrefix="orbelinkTest" TagName="Entidad_Atributos" Src="~/Controls/Entidad_Atributos.ascx" %>
<%@ Register TagPrefix="orbelinkTest" TagName="Entidad_Relaciones" Src="~/Controls/Entidad_Relaciones.ascx" %>
<%@ Register TagPrefix="orbelinkTest" TagName="Control_Relaciones_Entidad_Publicacion"
    Src="~/Controls/Rel_Entidad_Publicacion.ascx" %>
<%@ Register TagPrefix="orbelinkTest" TagName="Control_Relaciones_Entidad_Producto"
    Src="~/Controls/Rel_Entidad_Producto.ascx" %>
<%--<%@ Register TagPrefix="orbelinkTest" TagName="Control_Relaciones_Entidad_Proyecto"
    Src="~/Controls/Rel_Entidad_Proyecto.ascx" %>--%>
<%@ Register TagPrefix="orbelinkTest" TagName="Control_Relaciones_Entidad_Reservacion"
    Src="~/Controls/Rel_Entidad_Reservacion.ascx" %>
<asp:Content ID="ctn_TituloSeccion" ContentPlaceHolderID="cph_Titulo" runat="Server">
    <asp:Panel ID="pnl_editarTitulo" runat="server">
        <asp:Literal runat="server" Text="<%$ Resources:Entidades_Resources, Entidad_Pantalla %>"></asp:Literal>
    </asp:Panel>
    <asp:Panel ID="pnl_verTitulo" runat="server" Visible="false">
        <h2>
            <asp:Label ID="lbl_nombreTitulo" runat="server" Text="..."></asp:Label></h2>
    </asp:Panel>
</asp:Content>
<asp:Content ID="ctn_Contenido" ContentPlaceHolderID="cph_Contenido" runat="Server">
    <asp:UpdatePanel ID="udp_entidad" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <orbelinkTest:DetalleEntidad runat="server" ID="ocwc_DetalleEntidad"></orbelinkTest:DetalleEntidad>
            <asp:Panel ID="pnl_editar" runat="server">
                <ajaxToolkit:TabContainer ID="tabs_Entidad" runat="server">
                    <ajaxToolkit:TabPanel ID="tab_Detalle" runat="server" HeaderText="<%$ Resources:Entidades_Resources, Entidad_Detalle %>">
                        <ContentTemplate>
                            <asp:UpdatePanel ID="upd_Contenido" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="container fieldset">
                                        <asp:UpdatePanel ID="upd_detalle" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <div>
                                                    <label>
                                                        <asp:Literal runat="server" Text="<%$ Resources:Entidades_Resources, TipoEntidad %>"></asp:Literal>
                                                    </label>
                                                    <asp:DropDownList ID="ddl_TipoEntidad" runat="server" AutoPostBack="True">
                                                    </asp:DropDownList>
                                                    <asp:HyperLink ID="hyl_AgregarTipoEntidad" ToolTip="<%$ Resources:Entidades_Resources, TipoEntidad_NuevoTipoEntidad %>"
                                                        ImageUrl="~/Orbecatalog/iconos/agregar_small.png" runat="server"></asp:HyperLink>
                                                </div>
                                                <div>
                                                    &nbsp;
                                                </div>
                                                <div>
                                                    <h2>
                                                        <asp:Literal runat="server" Text="<%$ Resources:Entidades_Resources, Entidad_Informacion %>"></asp:Literal>
                                                    </h2>
                                                </div>
                                                <div>
                                                    <label>
                                                        <asp:Literal runat="server" Text="<%$ Resources:Entidades_Resources, Entidad_Nombre %>"></asp:Literal>
                                                    </label>
                                                    <asp:TextBox ID="tbx_NombreEntidad" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="vld_tbx_NombreEntidad" runat="server" Display="Dynamic"
                                                        ErrorMessage="<%$ Resources:Orbecatalog_Resources, Campo_Requerido %>" ControlToValidate="tbx_NombreEntidad"
                                                        EnableClientScript="False" ValidationGroup="01"></asp:RequiredFieldValidator>
                                                </div>
                                                <div>
                                                    <label>
                                                        <asp:Literal runat="server" Text="<%$ Resources:Entidades_Resources, Entidad_PrimerApellido %>"></asp:Literal>
                                                    </label>
                                                    <asp:TextBox ID="tbx_Apellido" runat="server"></asp:TextBox>
                                                </div>
                                                <div>
                                                    <label>
                                                        <asp:Literal runat="server" Text="<%$ Resources:Entidades_Resources, Entidad_SegundoApellido %>"></asp:Literal>
                                                    </label>
                                                    <asp:TextBox ID="tbx_Apellido2" runat="server"></asp:TextBox>
                                                </div>
                                                <div>
                                                    &nbsp;
                                                </div>
                                                <div>
                                                    <h2>
                                                        <asp:Literal runat="server" Text="<%$ Resources:Entidades_Resources, Entidad_Contactar %>"></asp:Literal></h2>
                                                </div>
                                                <div>
                                                    <label>
                                                        <asp:Literal runat="server" Text="<%$ Resources:Entidades_Resources, Entidad_Telefono %>"></asp:Literal>
                                                    </label>
                                                    <asp:TextBox ID="tbx_Telefono" runat="server"></asp:TextBox>
                                                </div>
                                                <div>
                                                    <label>
                                                        <asp:Literal runat="server" Text="<%$ Resources:Entidades_Resources, Entidad_Email %>"></asp:Literal>
                                                    </label>
                                                    <asp:TextBox ID="tbx_Email" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="vld_tbx_Email" runat="server" Display="Dynamic" ErrorMessage="<%$ Resources:Orbecatalog_Resources, Campo_Requerido %>"
                                                        ControlToValidate="tbx_Email" EnableClientScript="False" ValidationGroup="01"></asp:RequiredFieldValidator>
                                                </div>
                                                <div>
                                                    <label>
                                                        <asp:Literal runat="server" Text="<%$ Resources:Entidades_Resources, Entidad_Celular %>"></asp:Literal>
                                                    </label>
                                                    <asp:TextBox ID="tbx_Celular" runat="server"></asp:TextBox>
                                                </div>
                                                <div>
                                                    &nbsp;
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <orbelinkTest:Ubicacion runat="server" ID="ocwc_Ubicacion"></orbelinkTest:Ubicacion>
                                        <orbelinkTest:Entidad_Atributos runat="server" ID="ocwc_Entidad_Atributos"></orbelinkTest:Entidad_Atributos>
                                        <orbelinkTest:Entidad_Relaciones runat="server" ID="ocwc_Entidad_Relaciones"></orbelinkTest:Entidad_Relaciones>
                                        <div>
                                            &nbsp;
                                        </div>
                                    </div>
                                    <div class="container">
                                        <div>
                                            <h2>
                                                <asp:Literal runat="server" Text="<%$ Resources:Entidades_Resources, Entidad_Descripcion %>"></asp:Literal></h2>
                                        </div>
                                        <winthusiasm:HtmlEditor ID="tbx_Descripcion" runat="server" Height="200px" />
                                    </div>
                                    <div class="container fieldset">
                                        <div>
                                            &nbsp;
                                        </div>
                                        <div>
                                            <h2>
                                                <asp:Literal runat="server" Text="<%$ Resources:Entidades_Resources, Usuario_Usuario %>"></asp:Literal></h2>
                                        </div>
                                        <div>
                                            <label>
                                                <asp:Literal runat="server" Text="<%$ Resources:Entidades_Resources, Usuario_Usuario %>"></asp:Literal>
                                            </label>
                                            <asp:TextBox ID="tbx_Usuario" runat="server"></asp:TextBox>
                                        </div>
                                        <div>
                                            <label>
                                                <asp:Literal runat="server" Text="<%$ Resources:Entidades_Resources, Usuario_Password %>"></asp:Literal>
                                            </label>
                                            <asp:Label ID="lbl_Password" runat="server" Text="<%$ Resources:Entidades_Resources, Usuario_PasswordNoMostrado %>"></asp:Label>
                                            <asp:TextBox ID="tbx_PasswordEntidad" runat="server" TextMode="Password"></asp:TextBox>
                                        </div>
                                        <div>
                                            &nbsp;
                                        </div>
                                        <div>
                                            &nbsp;
                                        </div>
                                        <div class="botonesAcciones">
                                            <label>
                                                &nbsp;
                                            </label>
                                            <orbelink:ActionButton ID="btn_Salvar" CausesValidation="true" ImageURL="~/Orbecatalog/iconos/salvar_small.png"
                                                runat="server" Text="<%$ Resources:Orbecatalog_Resources, Salvar %>" ValidationGroup="01"
                                                CssClass="positive">
                                            </orbelink:ActionButton>
                                            <orbelink:ActionButton ID="btn_Modificar" CausesValidation="true" ImageURL="~/Orbecatalog/iconos/modificar_small.png"
                                                runat="server" Text="<%$ Resources:Orbecatalog_Resources, Salvar_Cambios %>"
                                                ValidationGroup="01" CssClass="positive">
                                            </orbelink:ActionButton>
                                            <orbelink:ActionButton ID="btn_Cancelar" ImageURL="~/Orbecatalog/iconos/cancelar_small.png"
                                                runat="server" Text="<%$ Resources:Orbecatalog_Resources, Cancelar %>">
                                            </orbelink:ActionButton>
                                            <orbelink:ActionButton ID="btn_Eliminar" ImageURL="~/Orbecatalog/iconos/eliminar_small.png"
                                                runat="server" Text="<%$ Resources:Orbecatalog_Resources, Eliminar %>" CssClass="negative">
                                            </orbelink:ActionButton>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel ID="tab_Archivos" runat="server" HeaderText="<%$ Resources:Entidades_Resources, Entidad_Archivos %>">
                        <ContentTemplate>
                            <iframe style="border: solid 1px white; width: 100%; height: 975px" frameborder="0"
                                scrolling="auto" id="if_Archivos" runat="server"></iframe>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel ID="tab_grupos" runat="server" HeaderText="<%$ Resources:Entidades_Resources, Entidad_Grupos %>">
                        <ContentTemplate>
                            <asp:UpdatePanel ID="udp_grupo" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:Panel ID="pnl_Grupos" runat="server">
                                        <div class="Container">
                                            <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                                <tr>
                                                    <td style="width: 95%">
                                                        <asp:Literal runat="server" Text="<%$ Resources:Entidades_Resources, Entidad_Grupos_Grupo %>"></asp:Literal>
                                                    </td>
                                                    <td style="width: 5%">
                                                        <asp:HyperLink ID="Hyp_agregarGrupo" ToolTip="<%$ Resources:Entidades_Resources, Entidad_Grupos_AgregarGrupo %>"
                                                            ImageUrl="~/Orbecatalog/iconos/agregar_small.png" runat="server"></asp:HyperLink>
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
                                                        <asp:Literal runat="server" Text="<%$ Resources:Entidades_Resources, Entidad_Grupos_TipoRelacion %>"></asp:Literal>
                                                    </td>
                                                    <td>
                                                        <asp:HyperLink ID="Hyp_tipoRelacionGrupos" ImageUrl="~/Orbecatalog/iconos/agregar_small.png"
                                                            ToolTip="<%$ Resources:Entidades_Resources, Entidad_Grupos_AgregarTipoRelacion %>"
                                                            runat="server"></asp:HyperLink>
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
                                            <asp:Button ID="btn_AgregarAGrupo" runat="server" Text="<%$ Resources:Entidades_Resources, Entidad_Grupos_Agregar %>" Width="100" />
                                            <br />
                                            <br />
                                            <h2>
                                                <asp:Literal runat="server" Text="<%$ Resources:Entidades_Resources, Entidad_Grupos_Perteneciente %>"></asp:Literal>
                                            </h2>
                                            <asp:ListBox ID="lst_Grupos" runat="server"></asp:ListBox>
                                            <br />
                                            <asp:Button ID="btn_QuitarDeGrupo" runat="server" Text="<%$ Resources:Entidades_Resources, Entidad_Grupos_Quitar %>" Width="100" />
                                            <asp:Button ID="btn_ListaGrupo" runat="server" Text="<%$ Resources:Entidades_Resources, Entidad_Grupos_VerList %>" Width="100" />
                                        </div>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                </ajaxToolkit:TabContainer>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="cph_Busqueda" ContentPlaceHolderID="cph_ColumnaDerecha" runat="Server">
    <orbelinkTest:Control_Relaciones_Entidad_Producto ID="ocwc_Rel_Entidad_Producto"
        runat="server"></orbelinkTest:Control_Relaciones_Entidad_Producto>
    <orbelinkTest:Control_Relaciones_Entidad_Publicacion ID="ocwc_Rel_Entidad_Publicacion"
        runat="server"></orbelinkTest:Control_Relaciones_Entidad_Publicacion>
 <%--   <orbelinkTest:Control_Relaciones_Entidad_Proyecto ID="ocwc_Rel_Entidad_Proyecto"
        runat="server"></orbelinkTest:Control_Relaciones_Entidad_Proyecto>--%>
    <orbelinkTest:Control_Relaciones_Entidad_Reservacion ID="ocwc_Rel_Entidad_Reservacion"
        runat="server"></orbelinkTest:Control_Relaciones_Entidad_Reservacion>
</asp:Content>
<asp:Content ID="ctn_Acciones" ContentPlaceHolderID="cph_Acciones" runat="server">
    <asp:UpdatePanel ID="upd_BotonesAcciones" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <orbelink:ActionButton ID="btn_Accion_Ver" ImageURL="~/Orbecatalog/iconos/ver_small.png"
                runat="server" Text="<%$ Resources:Orbecatalog_Resources, AccionVer %>">
            </orbelink:ActionButton>
            <orbelink:ActionButton ID="btn_Accion_Editar" ImageURL="~/Orbecatalog/iconos/editar_small.png"
                runat="server" Text="<%$ Resources:Orbecatalog_Resources, AccionEditar %>">
            </orbelink:ActionButton>
            <orbelink:ActionButton ID="btn_Accion_Ayuda" ImageURL="~/Orbecatalog/iconos/info_small.png"
                runat="server" Text="<%$ Resources:Orbecatalog_Resources, Ayuda %>">
            </orbelink:ActionButton>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
