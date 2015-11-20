<%@ Control Language="VB" AutoEventWireup="false" CodeFile="DetalleEntidad.ascx.vb"
    Inherits="Controls_DetalleEntidad" %>
<asp:Panel ID="pnl_ver" runat="server">
    <h1>
        <asp:Literal runat="server" Text="<%$ Resources:Entidades_Resources, Entidad_Informacion %>"></asp:Literal>
    </h1>
    <div id="info" style="padding-bottom: 20px" class="Container">
        <table style="border-style: solid; border-width: 1px; border-color: Gray; width: 100%">
            <tr>
                <td rowspan="5" style="text-align: center; width: 20%">
                    <asp:Image ID="img_principal" runat="server" CssClass="photoholderClass" AlternateText="Entidad"
                        ImageUrl="~/orbecatalog/images/foto.gif" />
                </td>
                <td colspan="2" style="text-align: left">
                    <asp:Label ID="lbl_nombre" Style="font-size: medium; font-weight: bold" runat="server"></asp:Label>
                    <br />
                    <asp:Label ID="lbl_tipo" runat="server"></asp:Label>
                    <br />
                    <hr />
                </td>
            </tr>
            <tr>
                <td style="text-align: left; width: 40%">
                    <br />
                    <asp:Label ID="lbl_ubicacion" Style="font-style: italic; font-weight: bold" runat="server"></asp:Label>
                </td>
                <td style="text-align: right; width: 40%">
                    <span style="font-weight: bold">
                        <asp:Literal runat="server" Text="<%$ Resources:Entidades_Resources, Entidad_Telefono %>"></asp:Literal>:
                    </span>
                    <asp:Label ID="lbl_telefono" runat="server" Text="-"></asp:Label><br />
                    <span style="font-weight: bold">
                        <asp:Literal runat="server" Text="<%$ Resources:Entidades_Resources, Entidad_Celular %>"></asp:Literal>:
                    </span>
                    <asp:Label ID="lbl_Celular" runat="server" Text="-"></asp:Label><br />
                    <span style="font-weight: bold">
                        <asp:Literal runat="server" Text="<%$ Resources:Entidades_Resources, Entidad_Email %>"></asp:Literal>:
                    </span>
                    <asp:Label ID="lbl_mail" runat="server" Text="-"></asp:Label>
                    <br />
                </td>
            </tr>
        </table>
    </div>
    <div id="atributos" style="float: left; width: 49%;">
        <h1>
            <asp:Literal runat="server" Text="<%$ Resources:Entidades_Resources, Atributos %>"></asp:Literal>
        </h1>
        <div style="border-style: solid; width: 82%; min-height: 200px; padding: 5px; border-color: Gray;
            border-width: 1px; margin-left: 40px">
            <asp:Label ID="lbl_atributosExtra" Font-Bold="true" runat="server" Text="<%$ Resources:Entidades_Resources, Atributos_NoTiene %>"
                Visible="false"></asp:Label>
            <asp:DataList ID="DL_atributos" runat="server" Width="100%">
                <ItemTemplate>
                    <asp:Label ID="lbl_nombreAtributo" Font-Bold="true" runat="server"></asp:Label>
                    <asp:Label ID="lbl_valorAtributo" runat="server"></asp:Label><br />
                    <hr style="border-style: dashed" />
                </ItemTemplate>
            </asp:DataList>
        </div>
    </div>
    <div id="notas" style="float: right; width: 49%">
        <h1>
            <asp:Literal runat="server" Text="<%$ Resources:Entidades_Resources, Entidad_Notas %>"></asp:Literal>
        </h1>
        <div style="background-image: url( ../images/post.png); background-repeat: no-repeat;
            background-position: top; width: 95%; height: 200px; padding-top: 40px; line-height: 25px">
            <asp:Repeater ID="rep_Notas" runat="server">
                <ItemTemplate>
                    <asp:TextBox ID="tbx_Nota" Style="background-color: Transparent; width: 130px; border-width: 0px"
                        runat="server" AutoPostBack="true" OnTextChanged="tbx_Nota_TextChanged"></asp:TextBox><br />
                </ItemTemplate>
            </asp:Repeater>
            <asp:LinkButton ID="lnk_notas" Style="padding-left: 190px" Font-Bold="true" ForeColor="Green"
                runat="server">+ Ver mas</asp:LinkButton>
        </div>
    </div>
    <div id="archivos" style="text-align: left; width: 100%; clear: both; padding-top: 20px"
        runat="server">
        <h1>
            <asp:Literal runat="server" Text="<%$ Resources:Entidades_Resources, Entidad_Archivos %>"></asp:Literal>
        </h1>
        <div style="border-style: solid; margin-top: 5px; width: 92%; padding: 5px; border-color: Gray;
            border-width: 1px; margin-left: 0px">
            <asp:DataList ID="dl_imagenes" runat="server" CellSpacing="5" RepeatColumns="3" Width="100%">
                <ItemStyle Width="33%" BackColor="#F2F2F2" />
                <ItemTemplate>
                    <table style="width: 100%; height: 100%">
                        <tr>
                            <td style="width: 50%; text-align: center; vertical-align: bottom">
                                <asp:Image ID="img_archivo" runat="server" Style="max-height: 100px; max-width: 100px" />
                            </td>
                            <td style="width: 50%; vertical-align: top">
                                <asp:Label ID="lbl_imgNombre" runat="server" Font-Bold="true"></asp:Label><br />
                                <asp:Label ID="lbl_imgFecha" runat="server"></asp:Label><br />
                                <asp:Label ID="lbl_imgTamano" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:DataList>
        </div>
    </div>
</asp:Panel>
