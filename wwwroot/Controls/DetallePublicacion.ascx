<%@ Control Language="VB" AutoEventWireup="false" CodeFile="DetallePublicacion.ascx.vb"
    Inherits="Controls_DetallePublicacion" %>
<asp:Panel ID="pnl_ver" runat="server">
    <div class="Container">
        <h1>
            Informacion General
        </h1>
        <div style="float: left; width: 20%">
            <asp:Image ID="img_principal" runat="server" CssClass="photoholderClass" ImageUrl="~/Orbecatalog/iconos/Modulos/pu_big.png" />
        </div>
        <div style="float: right; width: 79%">
            <div>
                <asp:Label ID="lbl_nombre" Style="font-size: medium; font-weight: bold" runat="server"></asp:Label>
                <br />
                <asp:Label ID="lbl_tipo" runat="server"></asp:Label>
                <br />
                <hr />
            </div>
            <div>
                <div style="text-align: left; width: 50%">
                    <br />
                    <asp:Label ID="lbl_descripcion" Style="font-style: italic; font-weight: bold" runat="server"></asp:Label>
                </div>
                <div style="text-align: right; width: 50%">
                    <span style="font-weight: bold">Fecha: </span>
                    <asp:Label ID="lbl_fechaInicio" runat="server" Text="No Aplica"></asp:Label>
                    <br />
                    <span style="font-weight: bold">Fecha Fin: </span>
                    <asp:Label ID="lbl_FechaFin" runat="server" Text="No Aplica"></asp:Label>
                    <br />
                    <span style="font-weight: bold">Categoria: </span>
                    <asp:Label ID="lbl_categoria" runat="server" Text="No aplica"></asp:Label>
                    <br />
                    <span style="font-weight: bold">Estado: </span>
                    <asp:Label ID="lbl_estado" runat="server" Text="No aplica"></asp:Label>
                    <br />
                    <span style="font-weight: bold">Creador: </span>
                    <asp:Label ID="lbl_entidadid" runat="server" Text="No aplica"></asp:Label>
                    <br />
                </div>
            </div>
        </div>
    </div>
    <div style="clear: both; padding-bottom: 20px">
    </div>
    <div style="float: left; width: 49%;">
        <h1>
            <asp:Label ID="Label2" runat="server" Text="Atributos"></asp:Label>
        </h1>
        <div style="border-style: solid; width: 82%; min-height: 200px; padding: 5px; border-color: Gray;
            border-width: 1px; margin-left: 40px">
            <asp:Label ID="lbl_atributosExtra" Font-Bold="true" runat="server" Text="No Posee Atributos Extra"
                Visible="false"></asp:Label>
            <asp:DataList ID="DL_atributos" runat="server" Width="100%">
                <ItemTemplate>
                    <asp:Label ID="lbl_nombreAtributo" Font-Bold="true" runat="server" Text="nombre"></asp:Label>
                    <asp:Label ID="lbl_valorAtributo" runat="server" Text="valor"></asp:Label><br />
                    <hr style="border-style: dashed" />
                </ItemTemplate>
            </asp:DataList>
        </div>
    </div>
    <div style="float: right; width: 49%;">
        <h1>
            <asp:Label ID="Label3" runat="server" Text="Notas"></asp:Label>
        </h1>
        <div style="background-image: url( ../images/post.png); background-repeat: no-repeat;
            background-position: top; width: 95%; height: 200px; padding-top: 40px; line-height: 25px">
            <span style="padding-left: 75px">1. </span>
            <asp:TextBox ID="Tb_nota1" Style="background-color: Transparent; width: 130px; border-width: 0px"
                runat="server"></asp:TextBox>
            <br />
            <span style="padding-left: 75px">2. </span>
            <asp:TextBox ID="Tb_nota2" Style="background-color: Transparent; width: 130px; border-width: 0px"
                runat="server"></asp:TextBox>
            <br />
            <span style="padding-left: 75px">3. </span>
            <asp:TextBox ID="Tb_nota3" Style="background-color: Transparent; width: 130px; border-width: 0px"
                runat="server"></asp:TextBox>
            <br />
            <span style="padding-left: 75px">4. </span>
            <asp:TextBox ID="Tb_nota4" Style="background-color: Transparent; width: 130px; border-width: 0px"
                runat="server"></asp:TextBox>
            <br />
            <span style="padding-left: 75px">5. </span>
            <asp:TextBox ID="Tb_nota5" Style="background-color: Transparent; width: 130px; border-width: 0px"
                runat="server"></asp:TextBox>
            <br />
            <span style="padding-left: 75px">6. </span>
            <asp:TextBox ID="Tb_nota6" Style="background-color: Transparent; width: 130px; border-width: 0px"
                runat="server"></asp:TextBox>
            <asp:LinkButton ID="lnk_notas" Style="padding-left: 190px" Font-Bold="true" ForeColor="Green"
                runat="server">+ Ver mas</asp:LinkButton>
        </div>
    </div>
    <div style="clear: both; padding-bottom: 20px">
    </div>
    <div id="archivos" style="clear: both;" runat="server">
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
                            <td style="vertical-align: top">
                                <asp:LinkButton ID="btn_imgNombre" runat="server" Font-Bold="true" OnClick="btn_imgNombre_Click">LinkButton</asp:LinkButton>
                                <br />
                                <asp:Label ID="lbl_imgFecha" runat="server"></asp:Label>
                                <br />
                                <asp:Label ID="lbl_imgTamano" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:DataList>
        </div>
    </div>
</asp:Panel>
