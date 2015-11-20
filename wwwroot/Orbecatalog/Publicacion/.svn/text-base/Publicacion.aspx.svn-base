<%@ Page Language="VB" MasterPageFile="../OrbeCatalog.master" AutoEventWireup="false"
    ValidateRequest="false" CodeFile="Publicacion.aspx.vb" Inherits="_Publicacion"
    Title="OrbeCatalog - Publicacion" %>

<%@ Register TagPrefix="orbelinkTest" TagName="DetallePublicacion" Src="~/Controls/DetallePublicacion.ascx" %>
<%@ Register TagPrefix="orbelinkTest" TagName="Publicacion_Atributos" Src="~/Controls/Publicacion_Atributos.ascx" %>
<%@ Register TagPrefix="orbelinkTest" TagName="Control_Publicacion_Entidad" Src="~/Controls/Rel_Publicacion_Entidad.ascx" %>
<%@ Register TagPrefix="orbelinkTest" TagName="Control_Publicacion_Producto" Src="~/Controls/Rel_Publicacion_Producto.ascx" %>
<asp:Content ID="ctn_TituloSeccion" ContentPlaceHolderID="cph_Titulo" runat="Server">
    <asp:Panel ID="pnl_editarTitulo" runat="server">
        Manejo de Publicaciones
    </asp:Panel>
    <asp:Panel ID="pnl_verTitulo" runat="server" Visible="false">
        <h1>
            <asp:Label ID="lbl_tipoTitulo" runat="server" Text="Tipo:"></asp:Label>:</h1>
        <h2>
            <asp:Label ID="lbl_nombreTitulo" runat="server" Text="Publicacion"></asp:Label></h2>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Publicacion" ContentPlaceHolderID="cph_Contenido" runat="Server">
    <asp:UpdatePanel ID="upd_Pantalla" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <orbelinkTest:DetallePublicacion runat="server" ID="ocwc_DetallePublicacion"></orbelinkTest:DetallePublicacion>
            <asp:Panel ID="pnl_Editar" runat="server">
                <ajaxToolkit:TabContainer ID="tabs_Publicacion" runat="server">
                    <ajaxToolkit:TabPanel ID="tab_Detalle" runat="server" HeaderText="Detalle">
                        <ContentTemplate>
                            <asp:UpdatePanel ID="upd_Contenido" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="Container fieldset">
                                        <div>
                                            <label>
                                                Tipo Publicacion
                                            </label>
                                            <asp:DropDownList ID="ddl_TipoPublicacion" runat="server" AutoPostBack="true">
                                            </asp:DropDownList>
                                            <asp:HyperLink ID="lnk_AgregarTipoPublicacion" runat="server"><img src="../iconos/agregar_small.png" /></asp:HyperLink>
                                        </div>
                                        <div>
                                            <label>
                                            </label>
                                            <asp:CheckBox ID="chk_IncluirRSS" runat="server" Text="Incluir en archivo RSS" TextAlign="Left" />
                                        </div>
                                        <div>
                                            &nbsp;
                                        </div>
                                        <div>
                                            <h2>
                                                Publicacion</h2>
                                        </div>
                                        <div>
                                            <label>
                                                Titulo
                                            </label>
                                            <asp:TextBox ID="tbx_Titulo" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="vld_tbx_Titulo" runat="server" ErrorMessage="<%$ Resources:Orbecatalog_Resources, Campo_Requerido %>"
                                                ControlToValidate="tbx_Titulo" Display="Dynamic" EnableClientScript="False"></asp:RequiredFieldValidator>
                                        </div>
                                        <div>
                                            <label>
                                                Autor
                                            </label>
                                            <asp:Label ID="lbl_Entidad" runat="server" Text="Autor" Visible="False"></asp:Label>
                                        </div>
                                        <div>
                                            <label>
                                                Descripcion Corta
                                            </label>
                                            <asp:TextBox ID="tbx_Corta" runat="server"></asp:TextBox>
                                        </div>
                                        <div>
                                            <label>
                                                Link
                                            </label>
                                            <asp:TextBox ID="tbx_Link" runat="server"></asp:TextBox>
                                        </div>
                                        <div>
                                            <label>
                                                Fecha Inicio
                                            </label>
                                            <BDP:BDPLite ID="bdp_FechaInicio" runat="server">
                                                <TextBoxStyle Width="75px" />
                                            </BDP:BDPLite>
                                            <BDP:TimePicker ID="time_FechaInicio" runat="server" Width="75px">
                                                <DesignatorStyle Width="75px" />
                                                <InputStyle Width="75px" />
                                            </BDP:TimePicker>
                                        </div>
                                        <div>
                                            <label>
                                                Fecha Fin
                                            </label>
                                            <BDP:BDPLite ID="bdp_FechaFin" runat="server">
                                                <TextBoxStyle Width="75px" />
                                            </BDP:BDPLite>
                                            <BDP:TimePicker ID="time_FechaFin" runat="server" Width="75px">
                                                <InputStyle Width="75px" />
                                            </BDP:TimePicker>
                                        </div>
                                        <div>
                                            <label>
                                            </label>
                                        </div>
                                        <div>
                                            <label>
                                                Visible
                                            </label>
                                            <asp:CheckBox ID="chk_Visible" runat="server" />
                                        </div>
                                        <div>
                                            <label>
                                                Estado
                                            </label>
                                            <asp:DropDownList ID="ddl_Estados" runat="server">
                                            </asp:DropDownList>
                                            <asp:HyperLink ID="lnk_AgregarEstados" runat="server"><img alt="Agregar" src="../iconos/agregar_small.png" /></asp:HyperLink>
                                        </div>
                                        <div>
                                            &nbsp;
                                        </div>
                                    </div>
                                    <asp:UpdatePanel ID="upd_Categorias" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <div class="Container fieldset">
                                                <div>
                                                    <h2>
                                                        Categoria</h2>
                                                    <asp:HyperLink ID="lnk_AgregarCategorias" runat="server"><img src="../iconos/agregar_small.png" /></asp:HyperLink>
                                                </div>
                                                <div>
                                                    <label>
                                                        Seleccionada
                                                    </label>
                                                    <asp:HyperLink ID="lnk_Categoria" runat="server" Text="Sin Asignar"></asp:HyperLink><br />
                                                    <asp:CustomValidator ID="vld_lnk_Categoria" runat="server" Display="Dynamic" ErrorMessage="<%$ Resources:Orbecatalog_Resources, Campo_Requerido %>"></asp:CustomValidator>
                                                </div>
                                                <div>
                                                    <asp:TreeView ID="trv_Categorias" runat="server" Style="display: none">
                                                    </asp:TreeView>
                                                </div>
                                                <div>
                                                    &nbsp;
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <orbelinkTest:Publicacion_Atributos ID="ocwc_Atributos" runat="server" />
                                    <div class="Container">
                                        <h2>
                                            Contenido</h2>
                                        <winthusiasm:HtmlEditor ID="tbx_Texto" runat="server" Height="400px" />
                                        <br />
                                    </div>
                                    <div class="Container fieldset">
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
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel ID="tab_Archivos" runat="server" HeaderText="Archivos">
                        <ContentTemplate>
                            <iframe style="border: solid 1px white; width: 100%; height: 950px" frameborder="0"
                                scrolling="auto" id="if_Archivos" runat="server"></iframe>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel ID="tab_Permisos" runat="server" HeaderText="Permisos por Tipo de Entidad">
                        <ContentTemplate>
                            <div class="Container">
                                <asp:DataGrid ID="dg_PermisosTipoEntidad" runat="server" AutoGenerateColumns="False"
                                    Width="100%" CellPadding="0" GridLines="None">
                                    <HeaderStyle CssClass="tablaResultados_Header" />
                                    <Columns>
                                        <asp:TemplateColumn HeaderText="Titulo">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_TipoEntidad" runat="server">lbl_TipoEntidad</asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Leer">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_Leer" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Escribir">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_Escribir" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Descargar">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_Descargar" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Subir">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk_Subir" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                </asp:DataGrid>
                                <asp:Button ID="btn_PermisosTipoEntidad" runat="server" Text="Guardar Permisos" />
                            </div>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel ID="tab_Grupos" runat="server" HeaderText="Permisos por Grupo">
                        <ContentTemplate>
                            <asp:UpdatePanel ID="upd_Grupos" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="Container">
                                        <table cellpadding="0" cellspacing="0" border="0" width="100%">
                                            <tr>
                                                <td style="width: 95%">
                                                    Grupo
                                                </td>
                                                <td style="width: 5%">
                                                    <asp:ImageButton ID="btn_AgregarGrupo" runat="server" ToolTip="Ingresar nuevo Grupo"
                                                        ImageUrl="../iconos/agregar_small.png" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:HyperLink ID="lnk_Grupo" runat="server">(Sin Asignar)</asp:HyperLink>
                                                    <asp:TreeView ID="trv_Grupos" runat="server" Style="display: none;">
                                                    </asp:TreeView>
                                                </td>
                                            </tr>
                                        </table>
                                        <div id="div_PermisosGrupos" runat="server">
                                            <br />
                                            <asp:DataGrid ID="dg_PermisosGrupo" runat="server" AutoGenerateColumns="False" Width="100%"
                                                GridLines="None">
                                                <HeaderStyle CssClass="tablaResultados_Header" />
                                                <Columns>
                                                    <asp:TemplateColumn HeaderText="Titulo">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_TipoRelacion" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Leer">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chk_Leer" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Escribir">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chk_Escribir" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Descargar">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chk_Descargar" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Subir">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chk_Subir" runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                </Columns>
                                            </asp:DataGrid>
                                            <asp:Button ID="btn_GuardarPermisos" runat="server" Text="Guardar Permisos" Width="100" />
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </ContentTemplate>
                    </ajaxToolkit:TabPanel>
                    <ajaxToolkit:TabPanel ID="tab_Comentarios" runat="server" HeaderText="Comentarios">
                        <ContentTemplate>
                            <asp:UpdatePanel ID="upd_Comentarios" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:GridView ID="gv_Comentarios" runat="server" AutoGenerateColumns="false" GridLines="None"
                                        CssClass="tablaResultados">
                                    </asp:GridView>
                                    <asp:HyperLink ID="lnk_AgregarComentario" runat="server">Agregar comentario</asp:HyperLink>
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
    <orbelinkTest:Control_Publicacion_Entidad ID="ocwc_Publicacion_Entidad" runat="server" />
    <orbelinkTest:Control_Publicacion_Producto ID="ocwc_Publicacion_Producto" runat="server" />
<%--    <orbelinkTest:Control_Publicacion_Proyecto ID="ocwc_Publicacion_Proyecto" runat="server" />
    <orbelinkTest:Control_Publicacion_cuenta ID="ocwc_Publicacion_cuenta" runat="server" />--%>
</asp:Content>
<asp:Content ID="ctn_Acciones" ContentPlaceHolderID="cph_Acciones" runat="server">
    <asp:UpdatePanel ID="upd_Actions" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <orbelink:ActionButton ID="btn_Accion_Ver" ImageURL="~/Orbecatalog/iconos/ver_small.png"
                runat="server" Text="Ver ">
            </orbelink:ActionButton>
            <orbelink:ActionButton ID="btn_Accion_Editar" ImageURL="~/Orbecatalog/iconos/editar_small.png"
                runat="server" Text="Editar ">
            </orbelink:ActionButton>
            <orbelink:ActionButton ID="btn_Accion_Ayuda" ImageURL="~/Orbecatalog/iconos/info_small.png"
                runat="server" Text="Ayuda ">
            </orbelink:ActionButton>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
