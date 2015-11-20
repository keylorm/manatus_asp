<%@ Page Language="VB" MasterPageFile="../OrbeCatalog.master" AutoEventWireup="false"
    CodeFile="Entidades.aspx.vb" Inherits="_Entidades" Title="Orbe Catalog - Busqueda de Entidades" %>

<asp:Content ID="ctn_TituloSeccion" ContentPlaceHolderID="cph_Titulo" runat="Server">
    Busqueda de Entidades
</asp:Content>
<asp:Content ID="ctn_Contenido" ContentPlaceHolderID="cph_Contenido" runat="Server">
    <h1>
        Resultados</h1>
    <asp:UpdatePanel ID="upd_Contenido" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <a href="../Contactos/Entidad.aspx">Ingresar nueva Entidad</a><br />
            <br />
            <asp:DataGrid ID="dg_Entidades" runat="server" PageSize="5" AutoGenerateColumns="False"
                AllowSorting="True" AllowPaging="True" CssClass="Contenido_Table">
                <AlternatingItemStyle CssClass="tablaResultados_Alternate" />
                <ItemStyle CssClass="tablaResultados_Item" />
                <HeaderStyle CssClass="tablaResultados_Header" />
                <FooterStyle CssClass="tablaResultados_Footer" />
                <PagerStyle Mode="NumericPages" CssClass="tablaResultados_Pager" />
                <Columns>
                    <asp:TemplateColumn HeaderText="Nombre">
                        <ItemTemplate>
                            <b>
                                <asp:HyperLink ID="lnk_Entidad" runat="server"></asp:HyperLink></b>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Telefono">
                        <ItemTemplate>
                            <asp:Label ID="lbl_Telefono" runat="server" Text="Telefono"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Email">
                        <ItemTemplate>
                            <asp:HyperLink ID="lnk_Email" runat="server" Text="Email"></asp:HyperLink>
                        </ItemTemplate>
                        <ItemStyle />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <ItemTemplate>
                            <asp:CheckBox ID="chk_Control" runat="server" /><br />
                            <asp:Label ID="lbl_Enviado" runat="server" Text="Enviado" Visible="false"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="5%" />
                        <HeaderTemplate>
                            <asp:CheckBox ID="chk_Todos" runat="server" OnCheckedChanged="chk_Todos_CheckedChanged"
                                AutoPostBack="true" />
                        </HeaderTemplate>
                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid>
            <div id="div_NoResultado" runat="server">
                <h3>
                    No hay resultados para esta busqueda</h3>
                <br />
                <a href="../Contactos/Entidad.aspx">Ingresar nueva Entidad</a>
            </div>
            <asp:Label ID="lbl_Resultado" runat="server"></asp:Label>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <asp:UpdatePanel ID="upd_Newsletter" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <br />
            <h1>
                Envios</h1>
            <asp:Panel ID="pnl_Envios" runat="server" Visible="false">
                <div class="Container fieldset">
                    <div>
                        <label>
                            Envios
                        </label>
                        <asp:DropDownList ID="ddl_Envios" runat="server">
                        </asp:DropDownList>
                    </div>
                    <div>
                        <label>
                        </label>
                        <asp:CheckBox ID="chk_UltimoLote" runat="server" Checked="true" Text="Agregar a ultimo Lote" />
                    </div>
                    <div>
                        <label>
                        </label>
                        <asp:Button ID="btn_AgregarEnvio" runat="server" Text="Agregar a Envio" Width="100" />
                        </td>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="cph_Busqueda" ContentPlaceHolderID="cph_ColumnaDerecha" runat="Server">
    <%--<asp:UpdatePanel ID="upd_Busqueda" runat="server" UpdateMode="Conditional">
        <ContentTemplate>--%>
    <h1>
        <asp:HyperLink ID="lnk_Busqueda" runat="server">Búsqueda y Filtros</asp:HyperLink></h1>
    <asp:Panel ID="pnl_Busqueda" runat="server" DefaultButton="btn_Buscar" CssClass="Container">
        <h2>
            Tipo Entidad</h2>
        <asp:DropDownList ID="ddl_Filtro_TipoEntidades" runat="server" AutoPostBack="True">
        </asp:DropDownList>
        <br />
        <h2>
            Nombre</h2>
        <asp:TextBox ID="tbx_NombreEntidad_Search" runat="server"></asp:TextBox>
        <br />
        <h2>
            Apellido</h2>
        <asp:TextBox ID="tbx_Apellido_Search" runat="server"></asp:TextBox>
        <br />
        <h2>
            Ubicacion</h2>
        <br />
        <asp:UpdatePanel ID="upd_Ubicacion" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div>
                    <asp:HyperLink ID="lnk_Ubicacion" runat="server" Text="-- Sin Asignar --"></asp:HyperLink>
                </div>
                <asp:TreeView ID="trv_Ubicaciones" runat="server" Style="display: none;">
                </asp:TreeView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <asp:Panel ID="pnl_Grupos" runat="server">
        <h1>
            <a href="javascript:toggleLayer('div_Grupos', this)">Busqueda por Grupos</a></h1>
        <div id="div_Grupos" class="Container" style="display: none;">
            <h2>
                Tipo Relaciones</h2>
            <asp:DropDownList ID="ddl_TipoRelaciones" runat="server">
            </asp:DropDownList>
            <br />
            <h2>
                Grupo</h2>
            <asp:DropDownList ID="ddl_Grupos" runat="server">
            </asp:DropDownList>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnl_Atributos" runat="server">
        <h1>
            Busqueda de otros Atributos</h1>
        <div class="Container">
            <asp:DataGrid ID="dg_Atributos" runat="server" AutoGenerateColumns="False" Width="100%"
                CellPadding="0" GridLines="None">
                <HeaderStyle CssClass="tablaResultados_Header" />
                <PagerStyle Mode="NumericPages" CssClass="tablaResultados_Pager" />
                <Columns>
                    <asp:TemplateColumn HeaderText="Nombre">
                        <ItemTemplate>
                            <asp:Label ID="lbl_Atributo" runat="server">lbl_Atributos_Nombre</asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="50%" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Valor">
                        <ItemTemplate>
                            <asp:TextBox ID="tbx_Atributos_Valor" runat="server"></asp:TextBox>
                            <asp:DropDownList ID="ddl_Atributos_Valores" runat="server" DataTextField="Valor"
                                Visible="False">
                            </asp:DropDownList>
                        </ItemTemplate>
                        <ItemStyle Width="50%" />
                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid>
        </div>
    </asp:Panel>
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
    <%--     </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
