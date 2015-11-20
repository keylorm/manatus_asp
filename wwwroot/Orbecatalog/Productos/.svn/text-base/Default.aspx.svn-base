<%@ Page Language="VB" MasterPageFile="../OrbeCatalog.master" AutoEventWireup="false"
    CodeFile="Default.aspx.vb" Inherits="_DefaultProductos" Title="Orbe Catalog - Productos" %>

<asp:Content ID="ctn_TituloSeccion" ContentPlaceHolderID="cph_Titulo" runat="Server">
    <asp:Literal runat="server" Text="<%$ Resources:Productos_Resources, ProductoModulo_Pantalla %>" />
</asp:Content>
<asp:Content ID="DefaultPage" ContentPlaceHolderID="cph_Contenido" runat="Server">
    <h1>
        Productos Recientes</h1>
    <div class="Container">
        <div class="links">
            <a href="Productos.aspx">Ingresar nuevo Producto</a><br />
        </div>
        <br />
        <asp:DataGrid ID="dg_Productos" runat="server" PageSize="3" AutoGenerateColumns="False"
            Width="100%" ShowHeader="False" CssClass="tableClean">
            <Columns>
                <asp:TemplateColumn HeaderText="Producto">
                    <ItemTemplate>
                        <asp:HyperLink ID="lnk_img_Producto" runat="server" ImageUrl="../images/mm.jpg"></asp:HyperLink>
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
    <h1>
        Grupos de Productos Recientes</h1>
    <div class="Container">
        <div class="links">
            <a href="Grupo_Producto.aspx">Crear nuevo grupo</a><br />
        </div>
    </div>
</asp:Content>
<asp:Content ID="cph_Busqueda" ContentPlaceHolderID="cph_ColumnaDerecha" runat="Server">
    <h1>
        Mantenimientos</h1>
    <div id="div_Panel" class="Container">
        <asp:TreeView ID="trv_Productos" runat="server">
        </asp:TreeView>
    </div>
</asp:Content>
