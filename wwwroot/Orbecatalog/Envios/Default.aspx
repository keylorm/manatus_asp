<%@ Page Language="VB" MasterPageFile="../OrbeCatalog.master" AutoEventWireup="false"
    CodeFile="Default.aspx.vb" Inherits="_DefaultEnvios" Title="Orbe Catalog - Envios" %>

<asp:Content ID="ctn_TituloSeccion" ContentPlaceHolderID="cph_Titulo" runat="Server">
    Envios
</asp:Content>
<asp:Content ID="DefaultPage" ContentPlaceHolderID="cph_Contenido" runat="Server">
    <asp:Panel ID="pnl_Envios" runat="server">
        <h1>
            Ultimos Envios</h1>
        <div class="Container">
            <a href="Envios.aspx">Ingresar nuevo Envio</a><br />
            <br />
            <asp:DataGrid ID="dg_Envios" runat="server" PageSize="3" AutoGenerateColumns="False"
                Width="100%" ShowHeader="False" CssClass="tablaResultados">
                <Columns>
                    <asp:TemplateColumn HeaderText="Nombre">
                        <ItemTemplate>
                            <h2>
                                <asp:HyperLink ID="lnk_Entidad" runat="server" Text="Entidad"></asp:HyperLink></h2>
                            <b>
                                <asp:Label ID="lbl_TipoEntidad" runat="server" Text="Tipo"></asp:Label></b><br />
                            <asp:Label ID="lbl_Email" runat="server" Text="Email"></asp:Label><br />
                            <br />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid></div>
    </asp:Panel>
</asp:Content>
<asp:Content ID="cph_Busqueda" ContentPlaceHolderID="cph_ColumnaDerecha" runat="Server">
    <h1>
        Panel de Control</h1>
    <div class="Container">
        <asp:TreeView ID="trv_Envios" runat="server">
        </asp:TreeView>
    </div>
</asp:Content>

