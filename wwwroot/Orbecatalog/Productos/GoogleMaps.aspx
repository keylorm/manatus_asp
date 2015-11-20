<%@ Page Language="VB" MasterPageFile="~/orbecatalog/popUp.master" AutoEventWireup="false"
    CodeFile="GoogleMaps.aspx.vb" Inherits="_GoogleMaps" Title="OrbeCatalog - Google Maps" %>

<asp:Content ID="ctn_Titulo" ContentPlaceHolderID="cph_titulo" runat="Server">
    <asp:Literal runat="server" Text="<%$ Resources:Productos_Resources, Mapa_Pantalla %>" />
</asp:Content>
<asp:Content ID="ctn_Contenido" ContentPlaceHolderID="cph_Contenido" runat="Server">
    <subgurim:GMap ID="GMap1" runat="server" />
    <asp:UpdatePanel ID="upd_Contenido" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="div_AgregarDireccion" class="fieldset" runat="server" DefaultButton="btn_Agregar">
                <div>
                    <label>
                        Nombre
                    </label>
                    <asp:TextBox ID="tbx_N_Punto" runat="server"></asp:TextBox>
                </div>
                <div>
                    <label>
                        Descripcion
                    </label>
                    <asp:TextBox ID="tbx_Descripcion" runat="server"></asp:TextBox>
                </div>
                <div>
                    <label>
                        Latitud
                    </label>
                    <asp:TextBox ID="tbx_Latitud" runat="server" Enabled="false"></asp:TextBox>
                </div>
                <div>
                    <label>
                        Longitud
                    </label>
                    <asp:TextBox ID="tbx_Longitud" runat="server" Enabled="false"></asp:TextBox>
                </div>
                <div class="botonesAcciones">
                    <orbelink:ActionButton ID="btn_Agregar" runat="server" Text="Agregar Direccion" />
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
