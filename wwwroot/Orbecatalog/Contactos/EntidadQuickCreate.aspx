<%@ Page Language="VB" MasterPageFile="../Popup.master" AutoEventWireup="false" CodeFile="EntidadQuickCreate.aspx.vb"
    Inherits="Orbecatalog_Entidades_EntidadQuickCreate" Title="Orbe Catalog - Entidad" %>

<%@ Register TagPrefix="orbelinkTest" TagName="Ubicacion" Src="~/Controls/Ubicacion.ascx" %>
<asp:Content ID="ctn_TituloSeccion" ContentPlaceHolderID="cph_Titulo" runat="Server">
    Crear nueva Entidad
</asp:Content>
<asp:Content ID="ctn_Contenido" ContentPlaceHolderID="cph_Contenido" runat="Server">
    <asp:UpdatePanel ID="upd_Contenido" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="div_Insertar" runat="server" class="container fieldset">
                <div>
                    <label>
                        Tipo Entidad
                    </label>
                    <asp:DropDownList ID="ddl_TipoEntidad" runat="server">
                    </asp:DropDownList>
                </div>
                <div>
                    &nbsp;
                </div>
                <div>
                    <h2>
                        Informacion</h2>
                </div>
                <div>
                    <label>
                        Nombre
                    </label>
                    <asp:TextBox ID="tbx_NombreEntidad" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="vld_tbx_NombreEntidad" runat="server" Display="Dynamic"
                        ErrorMessage="<%$ Resources:Orbecatalog_Resources, Campo_Requerido %>" ControlToValidate="tbx_NombreEntidad"
                        EnableClientScript="False"></asp:RequiredFieldValidator>
                </div>
                <div>
                    <label>
                        Primer Apellido
                    </label>
                    <asp:TextBox ID="tbx_Apellido" runat="server"></asp:TextBox>
                </div>
                <div>
                    <label>
                        Segundo Apellido
                    </label>
                    <asp:TextBox ID="tbx_Apellido2" runat="server"></asp:TextBox>
                </div>
                <div>
                    &nbsp;
                </div>
                <div>
                    <h2>
                        Contactar</h2>
                </div>
                <div>
                    <label>
                        Telefono
                    </label>
                    <asp:TextBox ID="tbx_Telefono" runat="server"></asp:TextBox>
                </div>
                <div>
                    <label>
                        Email
                    </label>
                    <asp:TextBox ID="tbx_Email" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="vld_tbx_Email" runat="server" Display="Dynamic" ErrorMessage="<%$ Resources:Orbecatalog_Resources, Campo_Requerido %>"
                        ControlToValidate="tbx_Email" EnableClientScript="False"></asp:RequiredFieldValidator>
                </div>
                <div>
                    <label>
                        Celular
                    </label>
                    <asp:TextBox ID="tbx_Celular" runat="server"></asp:TextBox>
                </div>
                <div>
                    &nbsp;
                </div>
                <orbelinkTest:Ubicacion runat="server" ID="ocwc_Ubicacion"></orbelinkTest:Ubicacion>
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
                    <orbelink:ActionButton ID="btn_Cancelar" ImageURL="~/Orbecatalog/iconos/cancelar_small.png"
                        runat="server" Text="<%$ Resources:Orbecatalog_Resources, Cancelar %>">
                    </orbelink:ActionButton>
                </div>
            </div>
            <div id="div_Consultar" runat="server">
                <asp:Label ID="lbl_Entidad" runat="server"></asp:Label>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
