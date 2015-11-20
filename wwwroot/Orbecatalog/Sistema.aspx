<%@ Page Language="VB" MasterPageFile="~/Orbecatalog/OrbeCatalog.master" AutoEventWireup="false"
    CodeFile="Sistema.aspx.vb" Inherits="Orbecatalog_Sistema" Title="Orbecatalog - Sistema" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_Titulo" runat="Server">
    <h1>
        Sistema</h1>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_Navegacion" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_Acciones" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cph_Contenido" runat="Server">
    <asp:Panel ID="pnl_mantenimiento" runat="server">
        <h1>
            Mantenimiento</h1>
        <asp:TreeView ID="trv_mantenimiento" runat="server">
        </asp:TreeView>
        <br />
    </asp:Panel>
    <asp:Panel ID="pnl_Proyectos" runat="server">
        <h1>
            Proyectos</h1>
        <asp:TreeView ID="trv_Proyectos" runat="server">
        </asp:TreeView>
        <br />
    </asp:Panel>
    <asp:Panel ID="pnl_crm" runat="server">
        <h1>
            CRM</h1>
        <asp:TreeView ID="trv_crm" runat="server">
        </asp:TreeView>
        <br />
    </asp:Panel>
    <asp:Panel ID="pnl_Procesos" runat="server">
        <h1>
            Procesos</h1>
        <asp:TreeView ID="trv_Procesos" runat="server">
        </asp:TreeView>
        <br />
    </asp:Panel>
    <asp:Panel ID="pnl_Productos" runat="server">
        <h1>
            Productos</h1>
        <asp:TreeView ID="trv_Productos" runat="server">
        </asp:TreeView>
        <br />
    </asp:Panel>
    <asp:Panel ID="pnl_Facturas" runat="server">
        <h1>
            Facturas</h1>
        <asp:TreeView ID="trv_Facturas" runat="server">
        </asp:TreeView>
        <br />
    </asp:Panel>
    <asp:Panel ID="pnl_Entidades" runat="server">
        <h1>
            Entidades</h1>
        <asp:TreeView ID="trv_Entidades" runat="server">
        </asp:TreeView>
        <br />
    </asp:Panel>
    <asp:Panel ID="pnl_Publicaciones" runat="server">
        <h1>
            Publicaciones</h1>
        <asp:TreeView ID="trv_Publicaciones" runat="server">
        </asp:TreeView>
        <br />
    </asp:Panel>
    <asp:Panel ID="pnl_Encuestas" runat="server">
        <h1>
            Encuestas</h1>
        <asp:TreeView ID="trv_Encuestas" runat="server">
        </asp:TreeView>
        <br />
    </asp:Panel>
    <asp:Panel ID="pnl_reportes" runat="server">
        <h1>
            Reportes</h1>
        <asp:TreeView ID="trv_Reportes" runat="server">
        </asp:TreeView>
        <br />
    </asp:Panel>
    <asp:Panel ID="pnl_Educativo" runat="server">
        <h1>
            Educativo</h1>
        <asp:TreeView ID="trv_Educativo" runat="server">
        </asp:TreeView>
        <br />
    </asp:Panel>
    <asp:Panel ID="pnl_Envios" runat="server">
        <h1>
            Envios</h1>
        <asp:TreeView ID="trv_Envios" runat="server">
        </asp:TreeView>
        <br />
    </asp:Panel>
    <asp:Panel ID="pnl_Rutas" runat="server">
        <h1>
            Rutas</h1>
        <asp:TreeView ID="trv_Rutas" runat="server">
        </asp:TreeView>
        <br />
    </asp:Panel>
    <asp:Panel ID="pnl_Laboral" runat="server">
        <h1>
            Laboral</h1>
        <asp:TreeView ID="trv_Laboral" runat="server">
        </asp:TreeView>
        <br />
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cph_BusquedaNueva" runat="Server">
</asp:Content>
<asp:Content ID="Content7" ContentPlaceHolderID="cph_ColumnaDerecha" runat="Server">
    <asp:Panel ID="pnl_Utilidades" runat="server">
        <h1>
            Utilidades</h1>
        <div class="Container">
            <asp:TreeView ID="trv_Orbecatalog" runat="server">
            </asp:TreeView>
        </div>
    </asp:Panel>
</asp:Content>
