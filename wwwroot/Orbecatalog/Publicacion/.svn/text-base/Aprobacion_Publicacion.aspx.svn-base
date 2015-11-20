<%@ Page Language="VB" MasterPageFile="../OrbeCatalog.master" AutoEventWireup="false"
    CodeFile="Aprobacion_Publicacion.aspx.vb" Inherits="_Aprobacion_Publicacion" Title="OrbeCatalog - Aprobacion Publicacion" %>

<asp:Content ID="ctn_NombreSeccion" ContentPlaceHolderID="cph_Titulo" runat="Server">
    Manejo de Aprobaciones por Publicacion
</asp:Content>
<asp:Content ID="Publicacion" ContentPlaceHolderID="cph_Contenido" runat="Server">
    <asp:UpdatePanel ID="upd_Contenido" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="pnl_Aprobacion_Publicacion" runat="server">
                <h1>
                    Publicacion</h1>
                <table class="Container" cellspacing="0" cellpadding="0" border="0">
                    <tr>
                        <td style="width:25%;">
                            Nombre</td>
                        <td style="width:75%">
                            <asp:HyperLink ID="lnk_Publicacion" runat="server" Text="Label"></asp:HyperLink></td>
                        <td style="width:5%">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Corta</td>
                        <td>
                            <asp:Label ID="lbl_Corta" runat="server" Text="Label"></asp:Label></td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Autor</td>
                        <td>
                            <asp:Label ID="lbl_Autor" runat="server" Text="Label"></asp:Label></td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;</td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <h2>
                                Aprobaciones</h2>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:DataGrid ID="dg_Relaciones" runat="server" Width="100%" AutoGenerateColumns="False">
                                <AlternatingItemStyle CssClass="tablaResultados_Alternate" />
                                <ItemStyle CssClass="tablaResultados_Item" />
                                <HeaderStyle CssClass="tablaResultados_Header" />
                                <FooterStyle CssClass="tablaResultados_Footer" />
                                <Columns>
                                    <asp:TemplateColumn HeaderText="Aprobar">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chk_Aprobado" runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Aprobacion">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Entidad" runat="server"></asp:Label>
                                            como
                                           
                                            <asp:Label ID="lbl_TipoRelacion" runat="server"></asp:Label> de
                                            <asp:Label ID="lbl_Grupo" runat="server"></asp:Label>
                                            <asp:Label ID="lbl_Aprobar" runat="server"></asp:Label>
                                            esta Publicacion.
                                            <br />
                                            <asp:TextBox ID="tbx_Comentario" runat="server" TextMode="MultiLine"></asp:TextBox><br />
                                        </ItemTemplate>
                                        <ItemStyle Width="90%" />
                                    </asp:TemplateColumn>
                                </Columns>
                            </asp:DataGrid>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            &nbsp;</td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <table id="tablaBotones" class="botonesAcciones" cellspacing="0" cellpadding="0" border="0">
                                <tr>
                                    <td>
                                        <orbelink:ActionButton ID="btn_Salvar" CausesValidation="true" ImageURL="~/Orbecatalog/iconos/salvar_small.png"
                        runat="server" Text="<%$ Resources:Orbecatalog_Resources, Salvar %>" CssClass="positive">
                    </orbelink:ActionButton>
                                    </td>
                                    <td><orbelink:ActionButton ID="btn_Cancelar" ImageURL="~/Orbecatalog/iconos/cancelar_small.png"
                        runat="server" Text="<%$ Resources:Orbecatalog_Resources, Cancelar %>">
                    </orbelink:ActionButton></td>
                                </tr>
                            </table>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="cph_Busqueda" ContentPlaceHolderID="cph_ColumnaDerecha" runat="Server">
    <h1>
        Busqueda y Filtros</h1>
    <div class="Container">
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="width:48%">
                    Tipo Publicacion:</td>
            </tr>
            <tr>
                <td>
                    <asp:DropDownList ID="ddl_Filtro_TipoPublicacion" runat="server">
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btn_Buscar" runat="server" Text="Buscar" Width="100" /></td>
            </tr>
        </table>
    </div>
    <h1>
        Publicaciones</h1>
    <asp:UpdatePanel ID="upd_Busqueda" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="Container">
                <asp:DataGrid ID="dg_Publicaciones" runat="server" AutoGenerateColumns="False" DataKeyField="id_Entidad"
                    AllowPaging="True" PageSize="10" CssClass="tablaResultados">
                    <AlternatingItemStyle CssClass="tablaResultados_Alternate" />
                    <ItemStyle CssClass="tablaResultados_Item" />
                    <HeaderStyle CssClass="tablaResultados_Header" />
                    <FooterStyle CssClass="tablaResultados_Footer" />
                    <PagerStyle Mode="NumericPages" CssClass="tablaResultados_Pager" />
                    <Columns>
                        <asp:EditCommandColumn CancelText="Cancel" EditText="Edit" UpdateText="Update">
                            <ItemStyle Width="15%" />
                        </asp:EditCommandColumn>
                        <asp:TemplateColumn HeaderText="Publicacion">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Publicacion" runat="server">Publicacion</asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Autor">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Autor" runat="server" Text="Autor"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                </asp:DataGrid>
                <asp:Label ID="lbl_ResultadoElementos" runat="server" Text="No hay elementos"></asp:Label>
            </div>
            
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btn_Buscar" EventName="click" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

