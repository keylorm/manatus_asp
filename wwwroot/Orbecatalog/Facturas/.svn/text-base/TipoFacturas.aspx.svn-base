<%@ Page Language="VB" MasterPageFile="../OrbeCatalog.master" AutoEventWireup="false"
    CodeFile="TipoFacturas.aspx.vb" Inherits="_Facturas_TipoFactura" Title="Orbe Catalog - Tipo Facturases" %>

<asp:Content ID="ctn_TituloSeccion" ContentPlaceHolderID="cph_titulo" runat="Server">
    Manejo de Tipos de Facturas
</asp:Content>
<asp:Content ID="ctn_Trabajo" ContentPlaceHolderID="cph_Contenido" runat="Server">
    <h1>
        Tipo de Facturas</h1>
    <div class="fieldset">
        <div>
            <label>
                Nombre
            </label>
            <asp:TextBox ID="tbx_NombreTipoFactura" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="vld_tbx_NombreTipoFactura" runat="server" ControlToValidate="tbx_NombreTipoFactura"
                Display="Dynamic" EnableClientScript="False" ErrorMessage="<%$ Resources:Orbecatalog_Resources, Campo_Requerido %>"></asp:RequiredFieldValidator>
        </div>
        <div>
            <label>
                Porcentaje Impuestos
            </label>
            <asp:TextBox ID="tbx_PorcentajeImpuestos" runat="server"></asp:TextBox>
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
</asp:Content>
<asp:Content ID="cph_Busqueda" ContentPlaceHolderID="cph_ColumnaDerecha" runat="Server">
    <h1>
        Tipos de Facturas Actuales</h1>
    <div class="Seccion_Busqueda_Container">
        <asp:DataGrid ID="DG_TipoFactura" CssClass="tablaResultados" runat="server"
            AutoGenerateColumns="False" Width="100%" AllowPaging="True" PageSize="5">
            <AlternatingItemStyle CssClass="tablaResultados_Alternate" />
            <ItemStyle CssClass="tablaResultados_Item" />
            <HeaderStyle CssClass="tablaResultados_Header" />
            <FooterStyle CssClass="tablaResultados_Footer" />
            <PagerStyle Mode="NumericPages" CssClass="tablaResultados_Pager" />
            <Columns>
                <asp:EditCommandColumn CancelText="Cancel" EditText="Edit" UpdateText="Update"></asp:EditCommandColumn>
                <asp:TemplateColumn HeaderText="Tipo Facturas">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Nombre" runat="server" Text="Label"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
            </Columns>
        </asp:DataGrid>
        </div>
</asp:Content>
