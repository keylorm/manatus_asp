<%@ Page Language="VB" MasterPageFile="../OrbeCatalog.master" AutoEventWireup="false"
    CodeFile="Temporada.aspx.vb" Inherits="Orbecatalog_Reservacion_Temporada" Title="OrbeCatalog - Reservacion - Temporada" %>

<asp:Content ID="ctn_TituloSeccion" ContentPlaceHolderID="cph_Titulo" runat="Server">
    <asp:Literal runat="server" Text="<%$ Resources:Reservaciones_Resources, Temporada_Pantalla %>"></asp:Literal>
</asp:Content>
<asp:Content ID="ctn_Contenido" ContentPlaceHolderID="cph_Contenido" runat="Server">
    <h1>
        <asp:Literal runat="server" Text="<%$ Resources:Reservaciones_Resources, Temporada %>"></asp:Literal></h1>
   <asp:UpdatePanel ID="upd_Contenido" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="fieldset">
                <div>
                    <label>
                        <asp:Literal runat="server" Text="<%$ Resources:Reservaciones_Resources, Temporada_Nombre %>"></asp:Literal></h1>
                    </label>
                    <asp:TextBox ID="tbx_NombreTemporada" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="vld_Nombre" runat="server" ControlToValidate="tbx_NombreTemporada"
                        Display="Dynamic" EnableClientScript="False" ErrorMessage="<%$ Resources:Orbecatalog_Resources, Campo_Requerido %>"></asp:RequiredFieldValidator>
                </div>
                <div>
                    <label>
                        <asp:Literal runat="server" Text="<%$ Resources:Reservaciones_Resources, Temporada_Prioridad %>"></asp:Literal></h1>
                    </label>
                    <asp:DropDownList ID="ddl_Prioridad" runat="server">
                    </asp:DropDownList>
                </div>
                <div>
                    &nbsp;
                </div>
                <div>
                    <h2>
                        <asp:Literal runat="server" Text="<%$ Resources:Reservaciones_Resources, Temporada_Rangos %>"></asp:Literal></h2>
                    <%--   <asp:GridView ID="gv_Rangos" runat="server" AutoGenerateColumns="false" CssClass="tablaResultados"
                        GridLines="None">
                        <FooterStyle CssClass="tablaResultados_Footer" />
                        <AlternatingRowStyle CssClass="tablaResultados_Alternate" />
                        <RowStyle CssClass="tablaResultados_Item" />
                        <HeaderStyle CssClass="tablaResultados_Header" />
                        <Columns>
                            <asp:TemplateField HeaderText="<%$ Resources:Reservaciones_Resources, Temporada_RangoOrden %>">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_RangoOrden" runat="server" Text="Label"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:Reservaciones_Resources, Temporada_RangoFechaInicio %>">
                                <ItemTemplate>
                                    <BDP:BDPLite ID="bdp_FechaInicio" runat="server" DateFormat="d/M/YYYY" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="<%$ Resources:Reservaciones_Resources, Temporada_RangoFechaFin %>">
                                <ItemTemplate>
                                    <BDP:BDPLite ID="bdp_FechaFin" runat="server" DateFormat="d/M/YYYY" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:ButtonField ButtonType ="Link"   CommandName="delete" ShowHeader="false" Text="<%$ Resources:Orbecatalog_Resources, Eliminar  %>" />
                        </Columns>
                    </asp:GridView>--%>
                    <asp:DataGrid ID="dg_Rangos" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                        PageSize="10" CssClass="tablaResultados">
                        <FooterStyle CssClass="tablaResultados_Footer" />
                        <AlternatingItemStyle cssclass="tablaResultados_Alternate" />
                        <ItemStyle cssclass="tablaResultados_Item" />
                        <HeaderStyle CssClass="tablaResultados_Header" />
                        <PagerStyle Mode="NumericPages" CssClass="tablaResultados_Pager" />
                        <Columns>
                           
                                <asp:TemplateColumn HeaderText="<%$ Resources:Reservaciones_Resources, Temporada_RangoOrden %>">
                                    <itemtemplate>
                                 <asp:Label ID="lbl_RangoOrden" runat="server" Text="Label"></asp:Label>
                            </itemtemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="<%$ Resources:Reservaciones_Resources, Temporada_RangoFechaInicio %>">
                                    <itemtemplate>
                                    <BDP:BDPLite ID="bdp_FechaInicio" runat="server" DateFormat="d/M/YYYY" />
                            </itemtemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="<%$ Resources:Reservaciones_Resources, Temporada_RangoFechaFin %>">
                                    <itemtemplate>
                                    <BDP:BDPLite ID="bdp_FechaFin" runat="server" DateFormat="d/M/YYYY" />
                            </itemtemplate>
                                </asp:TemplateColumn>
                                 <asp:ButtonColumn ButtonType="LinkButton"  CommandName="delete"  Text="<%$ Resources:Orbecatalog_Resources, Eliminar  %> "/>
                          
                             
                        </Columns>
                    </asp:DataGrid>
                </div>
                <div>
                    &nbsp;
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
                <asp:DataGrid ID="dg_Temporada" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                    PageSize="10" CssClass="tablaResultados">
                    <AlternatingItemStyle CssClass="tablaResultados_Alternate" />
                    <ItemStyle CssClass="tablaResultados_Item" />
                    <HeaderStyle CssClass="tablaResultados_Header" />
                    <FooterStyle CssClass="tablaResultados_Footer" />
                    <PagerStyle Mode="NumericPages" CssClass="tablaResultados_Pager" />
                    <Columns>
                    
                        <asp:EditCommandColumn CancelText="Cancel" EditText="Edit" UpdateText="Update"></asp:EditCommandColumn>
                        <asp:TemplateColumn HeaderText="<%$ Resources:Reservaciones_Resources, Temporada %>">
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
