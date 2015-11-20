<%@ Page Language="VB" MasterPageFile="../Orbecatalog.master" AutoEventWireup="false"
    ValidateRequest="false" CodeFile="Productos.aspx.vb" Inherits="_Productos" Title="OrbeCatalog - Busqueda - Productos" %>

<asp:Content ID="ctn_TituloSeccion" ContentPlaceHolderID="cph_Titulo" runat="Server">
    Busqueda de Productos
</asp:Content>
<asp:Content ID="Producto" ContentPlaceHolderID="cph_Contenido" runat="Server">
    <h1>
        Resultados</h1>
    <asp:UpdatePanel ID="upd_Contenido" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="Container">
                <a href="../Productos/Productos.aspx">Ingresar nuevo Producto</a><br />
                <br />
                <asp:DataGrid ID="dg_Productos" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                    PageSize="5" CssClass="Contenido_Table">
                    <AlternatingItemStyle CssClass="tablaResultados_Alternate" />
                    <ItemStyle CssClass="tablaResultados_Item" />
                    <HeaderStyle CssClass="tablaResultados_Header" />
                    <FooterStyle CssClass="tablaResultados_Footer" />
                    <PagerStyle Mode="NumericPages" CssClass="tablaResultados_Pager" />
                    <Columns>
                        <asp:TemplateColumn HeaderText="Control">
                            <ItemTemplate>
                                <asp:HyperLink ID="lnk_Editar" runat="server" Text="Editar"></asp:HyperLink><br />
                            </ItemTemplate>
                            <ItemStyle Width="10%" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Producto">
                            <ItemTemplate>
                                <b>
                                    <asp:Label ID="lbl_Nombre" runat="server"></asp:Label>
                                </b>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Descripcion">
                            <ItemTemplate>
                                <i>
                                    <asp:Label ID="lbl_Descripcion" runat="server" Text="Label"></asp:Label></i>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Tipo">
                            <ItemTemplate>
                                <asp:Label ID="lbl_TipoProducto" runat="server" Text="Label"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn>
                            <ItemTemplate>
                                <asp:CheckBox ID="chk_Control" runat="server" /><br />
                            </ItemTemplate>
                            <ItemStyle Width="5%" />
                            <HeaderTemplate>
                                <input id="Checkbox1" type="checkbox" onclick="checkAll2(this)" />
                            </HeaderTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                </asp:DataGrid>
                <asp:Label ID="lbl_ResultadoElementos" runat="server" Text="No hay elementos"></asp:Label>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <asp:UpdatePanel ID="upd_Control" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="pnl_AccionesBatch" runat="server">
                <h2>
                    Acciones sobre Seleccionados</h2>
                <orbelink:ActionButton ID="btn_Eliminar" runat="server" Text="Eliminar" CssClass="botonAccion negative">
                </orbelink:ActionButton>
                <br />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="cph_Busqueda" ContentPlaceHolderID="cph_ColumnaDerecha" runat="Server">
    <h1>
        Busqueda y Filtros</h1>
    <div class="Container">
        <table cellspacing="0" cellpadding="0" border="0" width="100%">
            <tr>
                <td>
                    <h2>
                        Tipo Producto</h2>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:LinkButton ID="lnk_TipoProducto" runat="server" Text="Sin Asignar"></asp:LinkButton>
                    <asp:TreeView ID="trv_TipoProducto" runat="server" Visible="False">
                    </asp:TreeView>
                    <br />
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <h2>
                        Orígenes</h2>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:LinkButton ID="lnk_Origen" runat="server" Text="Sin Asignar"></asp:LinkButton><br />
                    <asp:TreeView ID="trv_Origenes" runat="server" Visible="False">
                    </asp:TreeView>
                    <br />
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <h2>
                        Nombre</h2>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="tbx_Search_Producto" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <%-- <tr>
                <td>
                    Solo Foto:<br />
                    <asp:CheckBox ID="chk_SoloFoto" runat="server" /></td>
            </tr>--%>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <h2>
                        Estado</h2>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DropDownList ID="ddl_Estados" runat="server">
                    </asp:DropDownList>
                    <br />
                </td>
            </tr>
        </table>
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
</asp:Content>
