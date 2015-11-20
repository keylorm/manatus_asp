<%@ Page Language="VB" MasterPageFile="../OrbeCatalog.master" AutoEventWireup="false"
    CodeFile="MachoteAccion.aspx.vb" Inherits="Orbecatalog_Acciones_MachoteAccion"
    Title="OrbeCatalog - Acciones - Machote Accion" %>

<asp:Content ID="ctn_TituloSeccion" ContentPlaceHolderID="cph_Titulo" runat="Server">
    <asp:Literal runat="server" Text="<%$ Resources:Acciones_Resources, MachoteAcciones_Pantalla %>"></asp:Literal>
</asp:Content>
<asp:Content ID="ctn_Contenido" ContentPlaceHolderID="cph_Contenido" runat="Server">
    <asp:UpdatePanel ID="upd_Contenido" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <h1>
                Machote de Accion</h1>
            <div class="Container fieldset">
                <div>
                    <label>
                        Nombre
                    </label>
                    <asp:TextBox ID="tbx_NombreMachoteAccion" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="vld_Nombre" runat="server" ControlToValidate="tbx_NombreMachoteAccion"
                        Display="Dynamic" EnableClientScript="False" ValidationGroup="01" ErrorMessage="<%$ Resources:Orbecatalog_Resources, Campo_Requerido %>"></asp:RequiredFieldValidator>
                </div>
                <div>
                    <label>
                        Prefijo de Codigo
                    </label>
                    <asp:TextBox ID="tbx_PrefijoCodigo" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="vld_tbx_PrefijoCodigo" runat="server" ControlToValidate="tbx_PrefijoCodigo"
                        Display="Dynamic" EnableClientScript="False" ValidationGroup="01" ErrorMessage="<%$ Resources:Orbecatalog_Resources, Campo_Requerido %>"></asp:RequiredFieldValidator>
                </div>
                <div>
                    <label>
                        Moneda
                    </label>
                    <asp:DropDownList ID="ddl_Moneda" runat="server">
                    </asp:DropDownList>
                    <asp:HyperLink ID="Hyl_agregarunidad" ToolTip="Ingresar nueva unidad" ImageUrl="../iconos/agregar_small.png"
                        runat="server"></asp:HyperLink>
                </div>
                <div>
                    <label>
                        Valor
                    </label>
                    <asp:TextBox ID="tbx_Valor" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="vld_tbx_Valor" runat="server" ControlToValidate="tbx_Valor"
                        Display="Dynamic" EnableClientScript="False" ValidationGroup="01" ErrorMessage="<%$ Resources:Orbecatalog_Resources, Campo_Requerido %>"></asp:RequiredFieldValidator>
                </div>
                <div>
                    <label>
                        Dias de Validez
                    </label>
                    <asp:TextBox ID="tbx_Diasvalidez" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="vld_tbx_Diasvalidez" runat="server" ControlToValidate="tbx_Diasvalidez"
                        Display="Dynamic" EnableClientScript="False" ValidationGroup="01" ErrorMessage="<%$ Resources:Orbecatalog_Resources, Campo_Requerido %>"></asp:RequiredFieldValidator>
                </div>
                <div>
                    <label>
                        Fecha Vencimiento
                    </label>
                    <asp:TextBox ID="tbx_FechaVencimiento" Width="150px" runat="server"></asp:TextBox>
                    <obout:Calendar ID="cln_FechaVencimiento" runat="server" DatePickerMode="true" TextBoxId="tbx_FechaVencimiento"
                        DatePickerImagePath="../iconos/calendar.gif" />
                    <%--<asp:RequiredFieldValidator ID="vld_tbx_FechaVencimiento" runat="server" ControlToValidate="tbx_FechaVencimiento"
                        Display="Dynamic" EnableClientScript="False" ValidationGroup="01" ErrorMessage="<%$ Resources:Orbecatalog_Resources, Campo_Requerido %>"></asp:RequiredFieldValidator>--%>
                </div>
                <div>
                    <label>
                        &nbsp;
                    </label>
                    <div class="botonesAcciones">
                        <orbelink:ActionButton ID="btn_Salvar" CausesValidation="true" ImageURL="~/Orbecatalog/iconos/salvar_small.png"
                            runat="server" Text="<%$ Resources:Orbecatalog_Resources, Salvar %>" CssClass="positive"
                            ValidationGroup="01">
                        </orbelink:ActionButton>
                        <orbelink:ActionButton ID="btn_Modificar" CausesValidation="true" ImageURL="~/Orbecatalog/iconos/modificar_small.png"
                            runat="server" Text="<%$ Resources:Orbecatalog_Resources, Salvar_Cambios %>"
                            CssClass="positive" ValidationGroup="01">
                        </orbelink:ActionButton>
                        <orbelink:ActionButton ID="btn_Cancelar" ImageURL="~/Orbecatalog/iconos/cancelar_small.png"
                            runat="server" Text="<%$ Resources:Orbecatalog_Resources, Cancelar %>">
                        </orbelink:ActionButton>
                        <orbelink:ActionButton ID="btn_Eliminar" ImageURL="~/Orbecatalog/iconos/eliminar_small.png"
                            runat="server" Text="<%$ Resources:Orbecatalog_Resources, Eliminar %>" CssClass="negative">
                        </orbelink:ActionButton>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="cph_Busqueda" ContentPlaceHolderID="cph_ColumnaDerecha" runat="Server">
    <h1>
        Machotes Accion Actuales</h1>
    <asp:UpdatePanel ID="upd_Busqueda" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="Container">
                <asp:DataGrid ID="dg_MachoteAccion" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                    PageSize="10" CssClass="tablaResultados">
                    <AlternatingItemStyle CssClass="tablaResultados_Alternate" />
                    <ItemStyle CssClass="tablaResultados_Item" />
                    <HeaderStyle CssClass="tablaResultados_Header" />
                    <FooterStyle CssClass="tablaResultados_Footer" />
                    <PagerStyle Mode="NumericPages" CssClass="tablaResultados_Pager" />
                    <Columns>
                        <asp:EditCommandColumn CancelText="Cancel" EditText="Edit" UpdateText="Update"></asp:EditCommandColumn>
                        <asp:TemplateColumn HeaderText="Machote Accion">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Nombre" runat="server" Text="Label"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                </asp:DataGrid>
                <asp:Label ID="lbl_ResultadoElementos" runat="server" Text="No hay elementos"></asp:Label>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
