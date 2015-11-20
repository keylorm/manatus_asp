<%@ Page Language="VB" MasterPageFile="../OrbeCatalog.master" AutoEventWireup="false"
    CodeFile="Default.aspx.vb" Inherits="_DefaultPublicaciones" Title="Orbe Catalog - Publicaciones" %>

<asp:Content ID="DefaultPage" ContentPlaceHolderID="cph_Contenido" runat="Server">
    <asp:Panel ID="pnl_Publicaciones" runat="server">
        <h1>
            Ultimas Publicaciones</h1>
        <div class="Container">
            <a href="Publicacion.aspx">Ingresar nueva Publicacion</a><br />
            <br />
            <asp:DataList ID="dtl_Publicaciones" runat="server" Width="100%" CssClass="TablaPublicaciones">
                <ItemTemplate>
                    <asp:Image ID="img_Publicacion" runat="server"></asp:Image>
                    <h2>
                        <asp:HyperLink ID="lnk_Titulo" runat="server">Titulo</asp:HyperLink>
                    </h2>
                    <asp:Label ID="lbl_Fecha" runat="server" Text="Fecha"></asp:Label><br />
                    <asp:Label ID="lbl_Corta" runat="server" Text="Corta"></asp:Label><br />
                    <br />
                </ItemTemplate>
            </asp:DataList><br />
        </div>
    </asp:Panel>
</asp:Content>
<asp:Content ID="ctn_TituloSeccion" ContentPlaceHolderID="cph_Titulo" runat="Server">
    Publicaciones
</asp:Content>
<asp:Content ID="cph_Busqueda" ContentPlaceHolderID="cph_ColumnaDerecha" runat="Server">
    <h1>
        Panel de Control</h1>
    <div class="Container">
        <asp:TreeView ID="trv_Publicaciones" runat="server">
        </asp:TreeView>
    </div>
    
</asp:Content>

