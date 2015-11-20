<%@ Page Language="VB" MasterPageFile="../OrbeCatalog.master" AutoEventWireup="false"
    CodeFile="Default.aspx.vb" Inherits="_Default" Title="Orbe Catalog" %>

<asp:Content ID="DefaultPage" ContentPlaceHolderID="cph_Contenido" runat="Server">
    <h1>
        Busquedas</h1>
    <asp:TreeView ID="trv_Search" runat="server">
    </asp:TreeView>
</asp:Content>
<asp:Content ID="ctn_TituloSeccion" ContentPlaceHolderID="cph_Titulo" runat="Server">
    Busqueda
</asp:Content>
<asp:Content ID="cph_Busqueda" ContentPlaceHolderID="cph_ColumnaDerecha" runat="Server">
    <h1>
        Utilidades</h1>
    <div class="Container">
        Algo
    </div>
</asp:Content>

