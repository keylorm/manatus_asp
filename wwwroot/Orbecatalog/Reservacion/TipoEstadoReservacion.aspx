<%@ Page Language="VB" MasterPageFile="../OrbeCatalog.master" AutoEventWireup="false"
    CodeFile="TipoEstadoReservacion.aspx.vb" Inherits="Orbecatalog_Reservacion_TipoEstadoReservacion"
    Title="OrbeCatalog - CRM - Tipo Estado Reservacion" %>

<asp:Content ID="ctn_TituloSeccion" ContentPlaceHolderID="cph_Titulo" runat="Server">
    <asp:Literal runat="server" Text="<%$ Resources:Reservaciones_Resources, TipoEstadoReservacion_Pantalla %>"></asp:Literal>
</asp:Content>
<asp:Content ID="ctn_Contenido" ContentPlaceHolderID="cph_Contenido" runat="Server">
    <h1>
        <asp:Literal runat="server" Text="<%$ Resources:Reservaciones_Resources, TipoEstadoReservacion %>"></asp:Literal></h1>
    <asp:UpdatePanel ID="upd_Contenido" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="fieldset">
                <div>
                    <label>
                        <asp:Literal runat="server" Text="<%$ Resources:Reservaciones_Resources, TipoEstado_Nombre %>"></asp:Literal></h1>
                    </label>
                    <asp:TextBox ID="tbx_NombreTipoEstadoReservacion" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="vld_Nombre" runat="server" ControlToValidate="tbx_NombreTipoEstadoReservacion"
                        Display="Dynamic" EnableClientScript="False" ErrorMessage="<%$ Resources:Orbecatalog_Resources, Campo_Requerido %>"></asp:RequiredFieldValidator>
                </div>
                <div>
                    <label>
                        <asp:Literal runat="server" Text="<%$ Resources:Reservaciones_Resources, TipoEstado_Terminador %>"></asp:Literal></label>
                    <asp:CheckBox ID="chk_Terminador" runat="server" />
                </div>
                <div>
                    <label>
                        <asp:Literal runat="server" Text="<%$ Resources:Reservaciones_Resources, TipoEstado_RepresentaCancelado %>"></asp:Literal></label>
                    <asp:CheckBox ID="chk_RepresentaCancelado" runat="server" />
                </div>
                <div>
                    <label>
                        <asp:Literal runat="server" Text="<%$ Resources:Reservaciones_Resources, TipoEstado_RepresentaReservado %>"></asp:Literal></label>
                    <asp:CheckBox ID="chk_RepresentaReservado" runat="server" />
                </div>
                <div>
                    <label>
                        <asp:Literal runat="server" Text="<%$ Resources:Reservaciones_Resources, TipoEstado_RepresentaInicial %>"></asp:Literal></label>
                    <asp:CheckBox ID="chk_RepresentaInicial" runat="server" />
                </div>
                 <div>
                    <label>
                        <asp:Literal runat="server" Text="<%$ Resources:Reservaciones_Resources, TipoEstado_RepresentaBloqueado %>"></asp:Literal></label>
                    <asp:CheckBox ID="chk_bloqueado" runat="server" />
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
        <asp:Literal runat="server" Text="<%$ Resources:Reservaciones_Resources, TipoEstado_Actuales %>"></asp:Literal></h1>
    <asp:UpdatePanel ID="upd_Busqueda" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="Container">
                <asp:DataGrid ID="dg_TipoEstadoReservacion" runat="server" AutoGenerateColumns="False"
                    AllowPaging="True" PageSize="10" CssClass="tablaResultados">
                    <AlternatingItemStyle CssClass="tablaResultados_Alternate" />
                    <ItemStyle CssClass="tablaResultados_Item" />
                    <HeaderStyle CssClass="tablaResultados_Header" />
                    <FooterStyle CssClass="tablaResultados_Footer" />
                    <PagerStyle Mode="NumericPages" CssClass="tablaResultados_Pager" />
                    <Columns>
                        <asp:EditCommandColumn CancelText="Cancel" EditText="Edit" UpdateText="Update"></asp:EditCommandColumn>
                        <asp:TemplateColumn HeaderText="<%$ Resources:Reservaciones_Resources, TipoEstadoReservacion %>">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Nombre" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                </asp:DataGrid>
                <asp:Label ID="lbl_ResultadoElementos" runat="server" Text="<%$ Resources:Reservaciones_Resources, TipoEstado_NoHay %>"></asp:Label>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
