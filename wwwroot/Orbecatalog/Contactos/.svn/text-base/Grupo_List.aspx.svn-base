<%@ Page Language="VB" MasterPageFile="../Orbecatalog.master" AutoEventWireup="false"
    CodeFile="Grupo_List.aspx.vb" Inherits="_Grupo_View" Title="OrbeCatalog - Grupo" %>

<asp:Content ID="ctn_Titulo" ContentPlaceHolderID="cph_Titulo" runat="Server">
    Lista de Grupo
</asp:Content>
<asp:Content ID="ctn_Contenido" ContentPlaceHolderID="cph_Contenido" runat="Server">
    <h1>
        <asp:Label ID="lbl_Titulo" runat="server"></asp:Label></h1>
    <div class="Container">
        <i>
            <asp:Label ID="lbl_Descripcion" runat="server"></asp:Label></i>
        <br />
        <br />
        <asp:DataList ID="dtl_Relaciones" runat="server" Width="100%">
            <ItemTemplate>
                <h2>
                    <asp:Label ID="lbl_TipoRelacion" runat="server"></asp:Label></h2>
                <asp:DataList ID="dtl_Entidades" CssClass="tableClean" runat="server" RepeatColumns="2"
                    RepeatDirection="vertical" Width="100%">
                    <ItemTemplate>
                        <asp:HyperLink ID="lnk_Entidad" runat="server"></asp:HyperLink>
                    </ItemTemplate>
                    <ItemStyle Width="50%" />
                </asp:DataList>
                <br />
            </ItemTemplate>
        </asp:DataList>
    </div>
</asp:Content>
<asp:Content ID="cph_Busqueda" ContentPlaceHolderID="cph_ColumnaDerecha" runat="Server">
</asp:Content>

