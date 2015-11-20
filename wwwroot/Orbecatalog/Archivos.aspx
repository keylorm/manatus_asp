<%@ Page Language="VB" MasterPageFile="OrbeCatalog.master" AutoEventWireup="false"
    ValidateRequest="False" CodeFile="Archivos.aspx.vb" Inherits="_Archivos" Title="Orbe Catalog - Archivos" %>

<asp:Content ID="ctn_Titulo" ContentPlaceHolderID="cph_Titulo" runat="Server">
</asp:Content>
<asp:Content ID="ctn_Contenido" ContentPlaceHolderID="cph_Contenido" runat="Server">
    <div class="Container" style="text-align: left">
        <table>
            <tr>
                <td colspan="2">
                    <h1>
                        Filtros</h1>
                </td>
            </tr>
            <tr>
                <td style="width: 25%">
                    Tipo Archivos:
                </td>
                <td style="width: 75%">
                    <asp:DropDownList ID="ddl_FileTypes" runat="server" AutoPostBack="true">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <h1>
                        Archivo
                    </h1>
                </td>
            </tr>
            <tr>
                <td style="width: 25%">
                    Archivos por pagina:
                </td>
                <td style="width: 75%">
                    <asp:DropDownList ID="cmb_CantiArchivo" runat="server" AutoPostBack="True">
                        <asp:ListItem Value="5" Selected="True">5</asp:ListItem>
                        <asp:ListItem Value="10">10</asp:ListItem>
                        <asp:ListItem Value="15">15</asp:ListItem>
                        <asp:ListItem Value="20">20</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 25%">
                    Configuracion a usar:
                </td>
                <td style="width: 75%">
                    <asp:DropDownList ID="ddl_Configuration" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <div class="Container">
        <asp:DataGrid ID="dg_Archivo" runat="server" Width="100%" AutoGenerateColumns="False"
            AllowPaging="True" PageSize="5" CssClass="Contenido_Table">
            <AlternatingItemStyle CssClass="tablaResultados_Alternate" />
            <ItemStyle CssClass="tablaResultados_Item" />
            <HeaderStyle CssClass="tablaResultados_Header" />
            <FooterStyle CssClass="tablaResultados_Footer" />
            <PagerStyle Mode="NumericPages" CssClass="tablaResultados_Pager" />
            <Columns>
                <asp:TemplateColumn HeaderText="Archivo">
                    <ItemTemplate>
                        <div class="fieldset" id="div_Agregar" runat="server">
                            <div>
                                <label>
                                    Nombre:
                                </label>
                                <asp:TextBox ID="tbx_NombreArchivo" runat="server"></asp:TextBox>
                            </div>
                            <div>
                                <label>
                                    Archivo:
                                </label>
                                <asp:FileUpload ID="upl_Imagen" runat="server" />
                            </div>
                            <div>
                                <label>
                                    Comentario:
                                </label>
                                <asp:TextBox ID="tbx_Comentarios" runat="server" TextMode="MultiLine" Height="40px"></asp:TextBox><br />
                            </div>
                        </div>
                        <asp:HyperLink ID="lnk_Archivo" runat="server">
                            <asp:Image ID="img_Archivo" runat="server" ImageUrl="~/orbecatalog/images/mm.jpg" /></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="Archivo">
                    <ItemTemplate>
                        <div id="div_Info" runat="server">
                            <asp:Label ID="lbl_NombreArchivo" runat="server"></asp:Label><br />
                            <asp:Label ID="lbl_Info" runat="server" Text="No hay archivo"></asp:Label><br />
                            <asp:Button ID="btn_Descargar" runat="server" Text="Descargar" OnClick="btn_Descargar_Click" /><br />
                            <asp:Label ID="lbl_comentario" runat="server" Font-Italic="true"></asp:Label><br />
                        </div>
                    </ItemTemplate>
                    <ItemStyle Width="30%" />
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="Principal">
                    <ItemTemplate>
                        <asp:CheckBox ID="chk_Principal" runat="server"></asp:CheckBox>
                    </ItemTemplate>
                    <ItemStyle Width="10%" />
                </asp:TemplateColumn>
                <asp:ButtonColumn HeaderImageUrl="~/Orbecatalog/images/buttons/basurero.gif" HeaderText="Eliminar"
                    CommandName="Delete" Text="Delete">
                    <ItemStyle Width="10%" />
                </asp:ButtonColumn>
            </Columns>
        </asp:DataGrid>
        <br />
        <table cellspacing="0" cellpadding="0" border="0" width="100%">
            <tr>
                <td style="width: 35%">
                </td>
                <td>
                    <div class="botonesAcciones">
                        <orbelink:ActionButton ID="btn_Salvar" CausesValidation="true" Text="Salvar" runat="server"
                            ImageURL="~/Orbecatalog/iconos/salvar_small.png" />
                        <orbelink:ActionButton ID="btn_cancelar" Text="Cancelar" runat="server" ImageURL="~/Orbecatalog/iconos/cancelar_small.png" />
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="ctn_Busqueda" ContentPlaceHolderID="cph_ColumnaDerecha" runat="Server">
</asp:Content>
