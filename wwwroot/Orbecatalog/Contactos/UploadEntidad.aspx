<%@ Page Language="VB" MasterPageFile="../OrbeCatalog.master" AutoEventWireup="false"
    CodeFile="UploadEntidad.aspx.vb" Inherits="_Entidad" Title="Orbe Catalog - Upload Entidades" %>

<asp:Content ID="ctn_TituloSeccion" ContentPlaceHolderID="cph_Titulo" runat="Server">
    Upload Entidades
</asp:Content>
<asp:Content ID="ctn_Contenido" ContentPlaceHolderID="cph_Contenido" runat="Server">
    <h1>
        Detalle de Entidad</h1>
    <asp:UpdatePanel ID="upd_Contenido" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="Container fieldset">
                <div>
                    <h2>
                        Informacion</h2>
                </div>
                <div>
                    <label>
                        Tipo Entidad
                    </label>
                    <asp:DropDownList ID="ddl_Tipoentidad" runat="server">
                    </asp:DropDownList>
                </div>
                <div>
                    <label>
                        Nombre
                    </label>
                    <asp:DropDownList ID="ddl_upl_NombreEntidad" runat="server" Width="35px">
                    </asp:DropDownList>
                </div>
                <div>
                    <label>
                        Primer Apellido
                    </label>
                    <asp:DropDownList ID="ddl_upl_Apellido" runat="server" Width="35px">
                    </asp:DropDownList>
                </div>
                <div>
                    <label>
                        Segundo Apellido
                    </label>
                    <asp:DropDownList ID="ddl_upl_Apellido2" runat="server" Width="35px">
                    </asp:DropDownList>
                </div>
                <div>
                    <label>
                        Identificacion
                    </label>
                    <asp:DropDownList ID="ddl_upl_identificacion" runat="server" Width="35px">
                    </asp:DropDownList>
                </div>
                <div>
                    <label>
                        Fecha Importante
                    </label>
                    <asp:DropDownList ID="ddl_upl_FechaImportante" runat="server" Width="35px">
                    </asp:DropDownList>
                </div>
                <div>
                    <label>
                        Descripcion
                    </label>
                    <asp:DropDownList ID="ddl_upl_Descripcion" runat="server" Width="35px">
                    </asp:DropDownList>
                </div>
                <div>
                    &nbsp;
                </div>
                <div>
                    <h2>
                        Contactar</h2>
                </div>
                <div>
                    <label>
                        Telefono
                    </label>
                    <asp:DropDownList ID="ddl_upl_Telefono" runat="server" Width="35px">
                    </asp:DropDownList>
                </div>
                <div>
                    <label>
                        Email
                    </label>
                    <asp:DropDownList ID="ddl_upl_Email" runat="server" Width="35px">
                    </asp:DropDownList>
                </div>
                <div>
                    <label>
                        Celular
                    </label>
                    <asp:DropDownList ID="ddl_upl_Celular" runat="server" Width="35px">
                    </asp:DropDownList>
                </div>
                <div>
                    &nbsp;
                </div>
                <div>
                    <h2>
                        Ubicacion</h2>
                </div>
                <div>
                    <label>
                        Seleccionada
                    </label>
                    <asp:LinkButton ID="lnk_Ubicacion" runat="server" Text="Sin Asignar"></asp:LinkButton>
                    <asp:CustomValidator ID="vld_Ubicacion" runat="server" Display="Dynamic" ErrorMessage="<%$ Resources:Orbecatalog_Resources, Campo_Requerido %>"></asp:CustomValidator>
                </div>
                <div>
                    <asp:TreeView ID="trv_Ubicaciones" runat="server" Visible="False">
                    </asp:TreeView>
                </div>
                <div>
                    <h2>
                        Usuario</h2>
                </div>
                <div>
                    <label>
                        Usuario
                    </label>
                    <asp:DropDownList ID="ddl_upl_Usuario" runat="server" Width="35px">
                    </asp:DropDownList>
                </div>
                <div>
                    <label>
                        Password
                    </label>
                    <asp:DropDownList ID="ddl_upl_PasswordEntidad" runat="server" Width="35px">
                    </asp:DropDownList>
                </div>
                <div>
                    &nbsp;
                </div>
                <div>
                    <h2>
                        Dependencias y relaciones</h2>
                </div>
                <div>
                    <label>
                        Cuentas
                    </label>
                    <asp:DropDownList ID="ddl_cuentas" runat="server">
                    </asp:DropDownList>
                </div>
                <div>
                    <label>
                        Relaciòn
                    </label>
                    <asp:DropDownList ID="ddl_upl_DependienteTexto" runat="server" Width="35px">
                    </asp:DropDownList>
                </div>
                <asp:DataGrid ID="dg_relaciones" Width="90%" AlternatingItemStyle-BackColor="LightSkyBlue" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="true" GridLines="None"
                    runat="server">
                </asp:DataGrid>
                <div>
                    &nbsp;
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <h1>
        Archivo Excel</h1>
    <div id="table_Upload" class="Container fieldset">
        <div>
            <label>
                Archivo
            </label>
            <asp:FileUpload ID="upl_Archivo" runat="server" />
        </div>
        <div>
            <label>
                Nombre Hoja
            </label>
            <asp:TextBox ID="tbx_upl_NombreHoja" runat="server" Text="Sheet1"></asp:TextBox>
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
            <orbelink:ActionButton ID="btn_Cancelar" ImageURL="~/Orbecatalog/iconos/cancelar_small.png"
                runat="server" Text="<%$ Resources:Orbecatalog_Resources, Cancelar %>">
            </orbelink:ActionButton>
        </div>
    </div>
</asp:Content>
<asp:Content ID="cph_Busqueda" ContentPlaceHolderID="cph_ColumnaDerecha" runat="Server">
    <asp:UpdatePanel ID="upd_Busqueda" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="pnl_Grupos" runat="server">
                <h1>
                    Grupos</h1>
                <div class="Container">
                    Grupo
                    <br />
                    <asp:HyperLink ID="lnk_Grupo" runat="server">(Sin Asignar)</asp:HyperLink>
                    <asp:TreeView ID="trv_Grupos" runat="server" Style="display: none;">
                    </asp:TreeView>
                    <br />
                    <br />
                    Tipo Relacion
                    <br />
                    <asp:DropDownList ID="ddl_TipoRelacion_Grupos" runat="server">
                    </asp:DropDownList>
                    <br />
                    <br />
                    <asp:Button ID="btn_AgregarAGrupo" runat="server" Text="Agregar a grupo" Width="100" />
                    <br />
                    <br />
                    <h2>
                        Perteneciente</h2>
                    <asp:ListBox ID="lst_Grupos" runat="server"></asp:ListBox>
                    <br />
                    <asp:Button ID="btn_QuitarDeGrupo" runat="server" Text="Quitar de grupo" Width="100" />
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
