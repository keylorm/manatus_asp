<%@ Page Language="VB" MasterPageFile="OrbeCatalog.master" AutoEventWireup="false"
    CodeFile="Default.aspx.vb" Inherits="Orbecatalog_Default" Title="Orbe Catalog" %>

<asp:Content ID="ctn_TituloSeccion" ContentPlaceHolderID="cph_Titulo" runat="Server">
    Bienvenido!
</asp:Content>
<asp:Content ID="DefaultPage" ContentPlaceHolderID="cph_Contenido" runat="Server">
    <h1>
        Pendientes</h1>
    <div class="Container">
        <a href="Pendiente.aspx">Ingresar nuevo Pendiente</a><br />
        <br />
        <asp:DataList ID="dtl_Pendientes" runat="server" Width="100%">
            <ItemTemplate>
                <h2>
                    <asp:HyperLink ID="lnk_Pendiente" runat="server" Text="Pendiente"></asp:HyperLink></h2>
                Programado para el:
                <asp:Label ID="lbl_Fecha" runat="server" Text="Fecha"></asp:Label><br />
                <asp:Label ID="lbl_Comentario" runat="server" Text="Comentario"></asp:Label><br />
                <br />
            </ItemTemplate>
        </asp:DataList>
    </div>
    <asp:Panel ID="pnl_Productos" runat="server" Width="100%">
        <h1>
            Ultimos Productos</h1>
        <div class="Container">
            <a href="Productos/Productos.aspx">Ingresar nuevo Producto</a><br />
            <br />
            <asp:DataGrid ID="dg_Productos" runat="server" PageSize="3" AutoGenerateColumns="False"
                Width="100%" ShowHeader="False" CssClass="tableClean">
                <Columns>
                    <asp:TemplateColumn HeaderText="Producto">
                        <ItemTemplate>
                            <asp:HyperLink ID="lnk_img_Producto" runat="server" ImageUrl="images/mm.jpg"></asp:HyperLink>
                            <h2>
                                <asp:HyperLink ID="lnk_Producto" runat="server" Text="Producto"></asp:HyperLink></h2>
                            <b>
                                <asp:Label ID="lbl_TipoProducto" runat="server" Text="Tipo"></asp:Label></b><br />
                            <asp:Label ID="lbl_DescripcionCorta" runat="server" Text="Descripcion"></asp:Label><br />
                            <br />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnl_Publicaciones" runat="server" Width="100%">
        <h1>
            Ultimas Publicaciones</h1>
        <div class="Container">
            <a href="Publicacion/Publicacion.aspx">Ingresar nueva Publicacion</a><br />
            <br />
            <asp:DataList ID="dtl_Publicaciones" runat="server" Width="100%" CssClass="tableClean">
                <ItemTemplate>
                    <asp:Image ID="img_Publicacion" runat="server"></asp:Image>
                    <h2>
                        <asp:HyperLink ID="lnk_Titulo" runat="server">Titulo</asp:HyperLink>
                    </h2>
                    <asp:Label ID="lbl_FechaImportante" runat="server" Text="Fecha"></asp:Label>
                    <asp:Label ID="lbl_Fecha" runat="server" Text="Fecha"></asp:Label><br />
                    <asp:Label ID="lbl_Corta" runat="server" Text="Corta"></asp:Label><br />
                    <br />
                </ItemTemplate>
            </asp:DataList>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnl_Proyectos" runat="server" Width="100%">
        <h1>
            Ultimos Trámites</h1>
        <div class="Container">
            <a href="Proyectos/Proyecto.aspx">Ingresar nuevo Trámite</a><br />
            <br />
            <asp:DataGrid ID="dg_Proyectos" runat="server" AutoGenerateColumns="False" Width="100%"
                ShowHeader="False" CssClass="tableClean">
                <Columns>
                    <asp:TemplateColumn HeaderText="Control">
                        <ItemTemplate>
                            <asp:HyperLink ID="lnk_Proyecto" runat="server" Text="Editar" /><br />
                            <asp:HyperLink ID="lnk_VerTareas" runat="server" Text="Ver Tareas"></asp:HyperLink><br />
                        </ItemTemplate>
                        <ItemStyle Width="20%" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Proyecto">
                        <ItemTemplate>
                            <asp:Label ID="lbl_Proceso" runat="server" Text="Proceso"></asp:Label><br />
                            <asp:Label ID="lbl_Cliente" runat="server" Text="Cliente"></asp:Label><br />
                            <asp:Label ID="lbl_Codigo" runat="server" Text="Codigo"></asp:Label><br />
                            <br />
                        </ItemTemplate>
                        <ItemStyle Width="80%" />
                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid>
        </div>
    </asp:Panel>
</asp:Content>
<asp:Content ID="cph_Busqueda" ContentPlaceHolderID="cph_ColumnaDerecha" runat="Server">
    <h1>
        Accesos Rápidos</h1>
    <div class="Container">
        <a href="Panel.aspx">Panel de Control</a><br />
        <a href="Contactos/Entidad.aspx">Ingresar nueva Entidad</a><br />
    </div>
    <h1>
        Tema Visual</h1>
    <div class="Container">
        <asp:DropDownList ID="ddl_Themes" runat="server" AutoPostBack="True">
            <asp:ListItem>Classic6</asp:ListItem>
            <asp:ListItem>Sea6</asp:ListItem>
            <asp:ListItem>Nature6</asp:ListItem>
        </asp:DropDownList>
    </div>
</asp:Content>
