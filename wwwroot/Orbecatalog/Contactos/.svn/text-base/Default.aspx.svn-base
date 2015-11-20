<%@ Page Language="VB" MasterPageFile="../OrbeCatalog.master" AutoEventWireup="false"
    CodeFile="Default.aspx.vb" Inherits="_Entidades_Default" Title="Orbe Catalog - Entidades" %>

<asp:Content ID="ctn_TituloSeccion" ContentPlaceHolderID="cph_Titulo" runat="Server">
    Entidades
</asp:Content>
<asp:Content ID="DefaultPage" ContentPlaceHolderID="cph_Contenido" runat="Server">
    <asp:Panel ID="pnl_Entidades" runat="server">
        <h1>
            Ultimas Entidades</h1>
        <div class="Container">
            <a href="Entidad.aspx">Ingresar nueva Entidad</a><br />
            <br />
            <asp:DataGrid ID="dg_Entidades" runat="server" PageSize="3" AutoGenerateColumns="False"
                Width="100%" ShowHeader="False" CssClass="tableClean">
                <Columns>
                    <asp:TemplateColumn HeaderText="Nombre">
                        <ItemTemplate>
                            <h2>
                                <asp:HyperLink ID="lnk_Entidad" runat="server" Text="Entidad"></asp:HyperLink></h2>
                            <b>
                                <asp:Label ID="lbl_TipoEntidad" runat="server" Text="Tipo"></asp:Label></b><br />
                                <asp:HyperLink ID="lnk_Email" runat="server" Text="Entidad"></asp:HyperLink>
                            <br /><br />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid>
            </div>
    </asp:Panel>
</asp:Content>
<asp:Content ID="cph_Busqueda" ContentPlaceHolderID="cph_ColumnaDerecha" runat="Server">
    <h1>
        Panel de Control</h1>
    <div class="Container">
        <asp:TreeView ID="trv_Entidades" runat="server">
        </asp:TreeView>
    </div>
</asp:Content>

