<%@ Page Language="VB" MasterPageFile="../Orbecatalog.master" AutoEventWireup="false"
    ValidateRequest="false" CodeFile="Publicaciones.aspx.vb" Inherits="_Publicaciones"
    Title="OrbeCatalog - Busqueda - Publicaciones" %>

<asp:Content ID="ctn_TituloSeccion" ContentPlaceHolderID="cph_Titulo" runat="Server">
    Busqueda de Publicaciones
</asp:Content>
<asp:Content ID="Publicacion" ContentPlaceHolderID="cph_Contenido" runat="Server">
    <h1>
        Resultados</h1>
    <asp:UpdatePanel ID="upd_Contenido" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:DataGrid ID="dg_Publicaciones" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                PageSize="20" Width="100%" CssClass="Contenido_Table">
                <AlternatingItemStyle CssClass="tablaResultados_Alternate" />
                <ItemStyle CssClass="tablaResultados_Item" />
                <HeaderStyle CssClass="tablaResultados_Header" />
                <FooterStyle CssClass="tablaResultados_Footer" />
                <PagerStyle Mode="NumericPages" CssClass="tablaResultados_Pager" />
                <Columns>
                    <asp:TemplateColumn HeaderText="Control">
                        <ItemTemplate>
                            <asp:HyperLink ID="lnk_View" runat="server" Text="Consultar"></asp:HyperLink><br />
                        </ItemTemplate>
                        <ItemStyle Width="10%" />
                    </asp:TemplateColumn>
                    
                    <asp:TemplateColumn HeaderText="Nombre">
                        <ItemTemplate>
                            <b>
                                <asp:Label ID="lbl_Nombre" runat="server"></asp:Label>
                            </b>
                            <br />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Detalle">
                        <ItemTemplate>
                            <i>
                                <asp:Label ID="lbl_Descripcion" runat="server" Text="Descripcion"></asp:Label></i>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <ItemTemplate>
                            <asp:CheckBox ID="chk_Control" runat="server" /><br />
                            <asp:Label ID="lbl_Enviado" runat="server" Text="Enviado" Visible="false"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="5%" />
                        <HeaderTemplate>
                            Control<br />
                            <asp:CheckBox ID="chk_Todos" runat="server" OnCheckedChanged="chk_Todos_CheckedChanged"
                                AutoPostBack="true" />
                        </HeaderTemplate>
                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid>
            <asp:Label ID="lbl_ResultadoElementos" runat="server" Text="No hay elementos"></asp:Label>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <asp:UpdatePanel ID="upd_Control" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="pnl_AccionesBatch" runat="server">
                <h2>
                    Acciones sobre Seleccionados</h2>
                <orbelink:ActionButton ID="btn_Eliminar" runat="server" Text="Eliminar" CssClass="botonAccion negative"></orbelink:ActionButton>
                <br />
            </asp:Panel>
            <asp:Panel ID="pnl_Envios" runat="server">
                <h2>
                    Envio</h2>
                <asp:DropDownList ID="ddl_Envios" runat="server">
                </asp:DropDownList>
                <br />
                <orbelink:ActionButton ID="btn_AgregarEnvio" runat="server" Text="Agregar Publicaciones" CssClass="botonAccion" />
                <br />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="cph_Busqueda" ContentPlaceHolderID="cph_ColumnaDerecha" runat="Server">
    <h1>
        Busqueda y Filtros</h1>
    <%--<asp:UpdatePanel ID="upd_Busqueda" runat="server" UpdateMode="Conditional">
        <ContentTemplate>--%>
    <div class="Container">
        Tipo Publicacion:
        <br />
        <asp:DropDownList ID="ddl_Filtro_TipoPublicacion" runat="server">
        </asp:DropDownList>
        <br />
        Titulo:
        <br />
        <asp:TextBox ID="tbx_Search_Nombre" runat="server"></asp:TextBox>
        <br />
        Categoria
        <br />
        <asp:LinkButton ID="lnk_Categoria" runat="server" Text="Sin Asignar"></asp:LinkButton><br />
        <asp:TreeView ID="trv_Categorias" runat="server" Visible="False">
        </asp:TreeView>
        <br />
        <br />
        <div id="tr_Producto" runat="server">
            Producto:
            <asp:HyperLink ID="lnk_Producto" runat="server" Text="Producto"></asp:HyperLink>
            <br />
        </div>
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
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
