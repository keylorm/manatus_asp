<%@ Page Language="VB" MasterPageFile="~/Orbecatalog/popUp.master" AutoEventWireup="false"
    CodeFile="AgregarItem.aspx.vb" Inherits="Orbecatalog_Reservacion_AgregarItem"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_Titulo" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_Contenido" runat="Server">
    <table class="Container" cellspacing="0" cellpadding="0" border="0" width="100%">
        <tr>
            <td style="width: 25%">
                <asp:Label ID="Label1" runat="server" Text="Producto"></asp:Label>
            </td>
            <td style="width: 75%">
                <asp:DropDownList ID="ddl_productos" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
            </td>
            <td>
                <div class="botonesAcciones" style="padding-left: 30px">
                    <br />
                    <orbelink:ActionButton ID="btn_buscar" Text="Buscar" runat="server" />
                </div>
            </td>
        </tr>
    </table>
    <div id="verReservaciones" visible="false" runat="server" style="padding-top: 20px">
        <h1>
            <asp:Label ID="Label7" runat="server" Font-Bold="true" Text=" Resultados Disponibles"></asp:Label></h1>
        <asp:Label ID="Label5" runat="server" Text="Descripcion:"></asp:Label>
        <asp:Label ID="lbl_descripcion" runat="server" Text=""></asp:Label>
        <br />
        <br />
        <h2>
            <asp:Label ID="lbl_noDisponibles" Style="padding-left: 50px" runat="server" Visible="false"
                Text="NO hay Disponibles"></asp:Label></h2>
        <asp:GridView ID="dg_reservaciones" runat="server" AutoGenerateColumns="false" Width="100%"
            GridLines="None">
            <AlternatingRowStyle CssClass="tablaResultados_Alternate" />
            <Columns>
                <asp:TemplateField HeaderText="Producto">
                    <ItemTemplate>
                        <asp:Label ID="lbl_nombre" runat="server" Text="Nombre"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Descripcion">
                    <ItemTemplate>
                        <asp:Label ID="lbl_descripcion" runat="server" Text="descripcion"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Adultos">
                    <ItemTemplate>
                        <asp:TextBox ID="tbx_Adultos" runat="server"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Ninos">
                    <ItemTemplate>
                        <asp:TextBox ID="tbx_Ninos" runat="server"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Reservar">
                    <ItemTemplate>
                        <asp:CheckBox ID="chk_reservar" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <div class="botonesAcciones" style="padding-left: 285px">
            <br />
            <orbelink:ActionButton ID="btn_agregar" Text="Reservar" runat="server" />
        </div>
        <br />
        <br />
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_ColumnaDerecha" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cph_Acciones" runat="Server">
</asp:Content>
