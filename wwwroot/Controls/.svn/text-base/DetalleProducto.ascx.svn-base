<%@ Control Language="VB" AutoEventWireup="false" CodeFile="DetalleProducto.ascx.vb"
    Inherits="Controls_DetalleProducto" %>
<asp:Panel ID="pnl_ver" runat="server">
    <h1>
        Informacion General
    </h1>
    <div id="info" style="padding-bottom: 20px" class="Container">
        <table style="border-style: solid; border-width: 1px; border-color: Gray; width: 100%">
            <tr>
                <td rowspan="5" style="text-align: center; width: 20%">
                    <img id="img_principal" runat="server" class="photoholderClass" alt="." src="../Orbecatalog/images/foto.gif" />
                </td>
                <td colspan="2" style="text-align: left">
                    <asp:Label ID="lbl_nombre" Style="font-size: medium; font-weight: bold" runat="server"
                        Text="Pato"></asp:Label><br />
                    <asp:Label ID="lbl_tipo" runat="server" Text="Juguete"></asp:Label><br />
                    <hr />
                </td>
            </tr>
            <tr>
                <td style="text-align: left; width: 40%">
                    <br />
                    <asp:Label ID="lbl_descripcion" Style="font-style: italic; font-weight: bold" runat="server"
                        Text="San Pedro de Poas"></asp:Label>
                </td>
                <td style="text-align: right; width: 40%">
                    <span style="font-weight: bold">Precio: </span>
                    <asp:Label ID="lbl_precio" runat="server" Text="No Aplica"></asp:Label><br />
                    <span style="font-weight: bold">Sku: </span>
                    <asp:Label ID="lbl_Sku" runat="server" Text="No aplica"></asp:Label><br />
                    <span style="font-weight: bold">Creador: </span>
                    <asp:Label ID="lbl_entidadid" runat="server" Text="No aplica"></asp:Label>
                    <br />
                </td>
            </tr>
        </table>
    </div>
    <div id="keywords" style="float: left; width: 49%;">
        <h1>
            Keywords
        </h1>
        <div style="border-style: solid; width: 82%; min-height: 200px; padding: 5px; border-color: Gray;
            border-width: 1px; margin-left: 40px">
            <asp:Label ID="lbl_keywords" Font-Bold="true" runat="server" Text="No Posee KeyWords"
                Visible="false"></asp:Label>
            <asp:DataList ID="DL_Keyword" runat="server" Width="100%">
                <ItemTemplate>
                    <asp:Label ID="lbl_nombreKeyword" Font-Bold="true" runat="server" Text="nombre"></asp:Label>
                    <hr style="border-style: dashed" />
                </ItemTemplate>
            </asp:DataList>
        </div>
    </div>
    <div id="notas" style="float: right; width: 49%">
        <h1>
            Notas
        </h1>
        <div style="background-image: url( ../images/post.png); background-repeat: no-repeat;
            background-position: top; width: 95%; height: 200px; padding-top: 40px; line-height: 25px">
            <span style="padding-left: 75px">1. </span>
            <asp:TextBox ID="Tb_nota1" Style="background-color: Transparent; width: 130px; border-width: 0px"
                runat="server"></asp:TextBox><br />
            <span style="padding-left: 75px">2. </span>
            <asp:TextBox ID="Tb_nota2" Style="background-color: Transparent; width: 130px; border-width: 0px"
                runat="server"></asp:TextBox><br />
            <span style="padding-left: 75px">3. </span>
            <asp:TextBox ID="Tb_nota3" Style="background-color: Transparent; width: 130px; border-width: 0px"
                runat="server"></asp:TextBox><br />
            <span style="padding-left: 75px">4. </span>
            <asp:TextBox ID="Tb_nota4" Style="background-color: Transparent; width: 130px; border-width: 0px"
                runat="server"></asp:TextBox><br />
            <span style="padding-left: 75px">5. </span>
            <asp:TextBox ID="Tb_nota5" Style="background-color: Transparent; width: 130px; border-width: 0px"
                runat="server"></asp:TextBox><br />
            <span style="padding-left: 75px">6. </span>
            <asp:TextBox ID="Tb_nota6" Style="background-color: Transparent; width: 130px; border-width: 0px"
                runat="server"></asp:TextBox>
            <asp:LinkButton ID="lnk_notas" Style="padding-left: 190px" Font-Bold="true" ForeColor="Green"
                runat="server">+ Ver mas</asp:LinkButton>
        </div>
    </div>
    <div id="archivos" style="text-align: left; width: 100%; clear: both; padding-top: 20px"
        runat="server">
        <h1>
            Archivos
        </h1>
        <div style="border-style: solid; margin-top: 5px; width: 92%; padding: 5px; border-color: Gray;
            border-width: 1px; margin-left: 30px">
            <asp:DataList ID="dl_imagenes" runat="server" CellSpacing="5" RepeatColumns="3" Width="100%">
                <ItemStyle Width="33%" BackColor="#F2F2F2" />
                <ItemTemplate>
                    <table style="width: 100%; height: 100%">
                        <tr>
                            <td style="width: 50%; text-align: center; vertical-align: bottom">
                                <asp:Image ID="img_archivo" runat="server" Style="max-height: 100px; max-width: 100px" />
                            </td>
                            <td style="width: 50%; vertical-align: top">
                                <asp:Label ID="lbl_imgNombre" runat="server" Font-Bold="true" Text="Yo A los 15"></asp:Label><br />
                                <asp:Label ID="lbl_imgFecha" runat="server" Text="Update 15/08/08"></asp:Label><br />
                                <asp:Label ID="lbl_imgTamano" runat="server" Text="15x15, 58 KB"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:DataList>
        </div>
    </div>
</asp:Panel>
