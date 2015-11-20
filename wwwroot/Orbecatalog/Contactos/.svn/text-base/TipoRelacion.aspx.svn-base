<%@ Page Language="VB" MasterPageFile="../OrbeCatalog.master" AutoEventWireup="false"
    CodeFile="TipoRelacion.aspx.vb" Inherits="_TipoRelacion_Entidades" Title="Orbe Catalog - Tipo Entidades" %>

<asp:Content ID="ctn_TituloSeccion" ContentPlaceHolderID="cph_Titulo" runat="Server">
    Manejo de Tipos de Relacion
</asp:Content>
<asp:Content ID="ctn_Contenido" ContentPlaceHolderID="cph_Contenido" runat="Server">
    <h1>
        Tipo de Relacion</h1>
    <asp:UpdatePanel ID="upd_Contenido" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <%--    TipoRelacion_Entidades(.Text, .Text)--%>
            <div class="Container">
                <div>
                    <label>
                        Nombre Base Dependeiente
                    </label>
                    <asp:TextBox ID="tbx_NombreBaseDependeiente" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="vld_tbx_NombreBaseDependeiente" runat="server" ControlToValidate="tbx_NombreBaseDependeiente"
                        Display="Dynamic" EnableClientScript="False" ErrorMessage="<%$ Resources:Orbecatalog_Resources, Campo_Requerido %>"></asp:RequiredFieldValidator>
                </div>
                <div>
                    <label>
                        Nombre Dependiente Base
                    </label>
                    <asp:TextBox ID="tbx_NombreDependienteBase" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="vld_tbx_NombreDependienteBase" runat="server" ControlToValidate="tbx_NombreDependienteBase"
                        Display="Dynamic" EnableClientScript="False" ErrorMessage="<%$ Resources:Orbecatalog_Resources, Campo_Requerido %>"></asp:RequiredFieldValidator>
                </div>
                <div class="botonesAcciones">
                    <label>
                        &nbsp;
                    </label>
                    <orbelink:ActionButton ID="btn_Salvar" CausesValidation="true" ImageURL="~/Orbecatalog/iconos/salvar_small.png"
                        runat="server" Text="<%$ Resources:Orbecatalog_Resources, Salvar %>" CssClass="positive">
                    </orbelink:ActionButton>
                    <orbelink:ActionButton ID="btn_Modificar" CausesValidation="true" ImageURL="~/Orbecatalog/iconos/modificar_small.png"
                        runat="server" Text="<%$ Resources:Orbecatalog_Resources, Salvar_Cambios %>"
                        CssClass="positive">
                    </orbelink:ActionButton>
                    <orbelink:ActionButton ID="btn_Cancelar" ImageURL="~/Orbecatalog/iconos/cancelar_small.png"
                        runat="server" Text="<%$ Resources:Orbecatalog_Resources, Cancelar %>">
                    </orbelink:ActionButton>
                    <orbelink:ActionButton ID="btn_Eliminar" ImageURL="~/Orbecatalog/iconos/eliminar_small.png"
                        runat="server" Text="<%$ Resources:Orbecatalog_Resources, Eliminar %>" CssClass="negative">
                    </orbelink:ActionButton>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="cph_Busqueda" ContentPlaceHolderID="cph_ColumnaDerecha" runat="Server">
    <h1>
        Tipos de Relacion Actuales</h1>
    <asp:UpdatePanel ID="upd_Busqueda" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="Container">
                <asp:DataGrid ID="dg_TipoRelacion_Entidades" CssClass="tablaResultados" runat="server"
                    AutoGenerateColumns="False" Width="100%" AllowPaging="True" PageSize="10">
                    <AlternatingItemStyle CssClass="tablaResultados_Alternate" />
                    <ItemStyle CssClass="tablaResultados_Item" />
                    <HeaderStyle CssClass="tablaResultados_Header" />
                    <FooterStyle CssClass="tablaResultados_Footer" />
                    <PagerStyle Mode="NumericPages" CssClass="tablaResultados_Pager" />
                    <Columns>
                        <asp:EditCommandColumn CancelText="Cancel" EditText="Edit" UpdateText="Update">
                            <ItemStyle Width="10%" />
                        </asp:EditCommandColumn>
                        <asp:TemplateColumn HeaderText="Tipo Entidad">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Nombre" runat="server" Text="Label"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="85%" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn>
                            <ItemTemplate>
                                <asp:CheckBox ID="chk_Control" runat="server" /><br />
                            </ItemTemplate>
                            <ItemStyle Width="5%" />
                            <HeaderTemplate>
                                <asp:CheckBox ID="chk_Todos" runat="server" OnCheckedChanged="chk_Todos_CheckedChanged"
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
