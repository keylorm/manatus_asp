<%@ Page Language="VB" MasterPageFile="../OrbeCatalog.master" AutoEventWireup="false"
    CodeFile="Atributos.aspx.vb" Inherits="_Publicaciones_Atributos" Title="Orbe Catalog - Atributos" %>

<asp:Content ID="ctn_TituloSeccion" ContentPlaceHolderID="cph_Titulo" runat="Server">
    Manejo de Atributos
</asp:Content>

<asp:Content ID="ctn_Contenido" ContentPlaceHolderID="cph_Contenido" runat="Server">
    <h1>
        Atributo</h1>
    <asp:UpdatePanel ID="upd_Contenido" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table class="Container" cellspacing="0" cellpadding="0" border="0">
                <tr>
                    <td style="width: 25%;">
                        Nombre</td>
                    <td style="width: 70%;">
                        <asp:TextBox ID="tbx_NombreAtributo" runat="server"></asp:TextBox></td>
                    <td style="width: 5%;">
                        <asp:RequiredFieldValidator ID="vld_tbx_NombreAtributo" runat="server" ControlToValidate="tbx_NombreAtributo"
                            Display="Dynamic" EnableClientScript="False" ErrorMessage="<%$ Resources:Orbecatalog_Resources, Campo_Requerido %>"></asp:RequiredFieldValidator></td>
                </tr>
                <tr>
                    <td>
                        Descripcion</td>
                    <td>
                        <asp:TextBox ID="tbx_Descripcion" runat="server" TextMode="MultiLine"></asp:TextBox>&nbsp;</td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        Auto Completar</td>
                    <td>
                        <asp:CheckBox ID="chk_AutoCompletar" runat="server" /></td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
<div class="botonesAcciones">
                    <label>
                        &nbsp;
                    </label>
                    <orbelink:ActionButton ID="btn_Salvar" CausesValidation="true" ImageURL="~/Orbecatalog/iconos/salvar_small.png"
                        runat="server" Text="<%$ Resources:Orbecatalog_Resources, Salvar %>" CssClass="positive">
                    </orbelink:ActionButton>
                    <orbelink:ActionButton ID="btn_Modificar" CausesValidation="true" ImageURL="~/Orbecatalog/iconos/modificar_small.png"
                        runat="server" Text="<%$ Resources:Orbecatalog_Resources, Salvar_Cambios %>" CssClass="positive">
                    </orbelink:ActionButton><orbelink:ActionButton ID="btn_Cancelar" ImageURL="~/Orbecatalog/iconos/cancelar_small.png"
                        runat="server" Text="<%$ Resources:Orbecatalog_Resources, Cancelar %>">
                    </orbelink:ActionButton>
                    <orbelink:ActionButton ID="btn_Eliminar" ImageURL="~/Orbecatalog/iconos/eliminar_small.png"
                        runat="server" Text="<%$ Resources:Orbecatalog_Resources, Eliminar %>" CssClass="negative">
                    </orbelink:ActionButton>
                </div>
</td>
                    <td>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="cph_Busqueda" ContentPlaceHolderID="cph_ColumnaDerecha" runat="Server">
    <h1>
        Atributos Actuales</h1>
    <asp:UpdatePanel ID="upd_Busqueda" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="Container">
                <asp:DataGrid ID="dg_Atributos" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                    PageSize="10" CssClass="tablaResultados">
                    <AlternatingItemStyle CssClass="tablaResultados_Alternate" />
                    <ItemStyle CssClass="tablaResultados_Item" />
                    <HeaderStyle CssClass="tablaResultados_Header" />
                    <FooterStyle CssClass="tablaResultados_Footer" />
                    <PagerStyle Mode="NumericPages" CssClass="tablaResultados_Pager" />
                    <Columns>
                        <asp:EditCommandColumn CancelText="Cancel" EditText="Edit" UpdateText="Update">
                            <ItemStyle Width="15%" />
                        </asp:EditCommandColumn>
                        <asp:TemplateColumn HeaderText="Atributo">
                            <ItemTemplate>
                                <asp:Label ID="lbl_NombreAtributo" runat="server" Text="Label"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="50%" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Auto Completar">
                            <ItemTemplate>
                                <asp:Label ID="lbl_AutoCompletar" runat="server" Text="Label"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="30%" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn>
                            <ItemTemplate>
                                <asp:CheckBox ID="chk_Control" runat="server" /><br />
                            </ItemTemplate>
                            <ItemStyle Width="5%" />
                            <HeaderTemplate>
                                <asp:CheckBox ID="chk_TodosGrupos" runat="server" OnCheckedChanged="chk_TodosGrupos_CheckedChanged"
                                    AutoPostBack="true" />
                            </HeaderTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                </asp:DataGrid>
                <asp:Label ID="lbl_ResultadoElementos" runat="server" Text="No hay elementos"></asp:Label><br />
                <asp:Button ID="btn_BorrarSeleccionados" runat="server" Text="Borrar Seleccionados" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

