<%@ Page Language="VB" MasterPageFile="OrbeCatalog.master" AutoEventWireup="false"
    CodeFile="HorarioOrdinario.aspx.vb" Inherits="_HorarioOrdinario" Title="Orbe Catalog - Horario Ordinario" %>

<asp:Content ID="ctn_TituloSeccion" ContentPlaceHolderID="cph_Titulo" runat="Server">
    Manejo de Horario ordinario
</asp:Content>
<asp:Content ID="ctn_Contenido" ContentPlaceHolderID="cph_Contenido" runat="Server">
    <h1>
        Dia de la semana</h1>
    <asp:UpdatePanel ID="upd_Contenido" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table class="Container" cellspacing="0" cellpadding="0" border="0">
                <tr>
                    <td>
                        Dia</td>
                    <td>
                        <asp:DropDownList ID="ddl_Dia" runat="server">
                            <asp:ListItem>Domingo</asp:ListItem>
                            <asp:ListItem>Lunes</asp:ListItem>
                            <asp:ListItem>Martes</asp:ListItem>
                            <asp:ListItem>Miercoles</asp:ListItem>
                            <asp:ListItem>Jueves</asp:ListItem>
                            <asp:ListItem>Viernes</asp:ListItem>
                            <asp:ListItem>Sabado</asp:ListItem>
                        </asp:DropDownList></td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        Hora Inicio</td>
                    <td>
                        <asp:TextBox ID="tbx_HoraInicio" runat="server"></asp:TextBox></td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        Hora Final</td>
                    <td>
                        <asp:TextBox ID="tbx_HoraFinal" runat="server"></asp:TextBox></td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
<div class="botonesAcciones">
                    <label>
                        &nbsp;
                    </label>
                    <orbelink:ActionButton ID="btn_Salvar" CausesValidation="true" ImageURL="~/Orbecatalog/iconos/salvar_small.png"
                        runat="server" Text="<%$ Resources:Orbecatalog_Resources, Salvar %>" CssClass="positive">
                    </orbelink:ActionButton>
                    <orbelink:ActionButton ID="btn_Modificar" CausesValidation="true" ImageURL="~/Orbecatalog/iconos/modificar_small.png"
                        runat="server" Text="<%$ Resources:Orbecatalog_Resources, Salvar_Cambios %>" CssClass="positive">
                    </orbelink:ActionButton><orbelink:ActionButton ID="btn_Cancelar" ImageURL="~/Orbecatalog/iconos/cancelar_small.png"
                        runat="server" Text="<%$ Resources:Orbecatalog_Resources, Cancelar %>">
                    </orbelink:ActionButton>
                    <orbelink:ActionButton ID="btn_Eliminar" ImageURL="~/Orbecatalog/iconos/eliminar_small.png"
                        runat="server" Text="<%$ Resources:Orbecatalog_Resources, Eliminar %>" CssClass="negative">
                    </orbelink:ActionButton>
                </div>
</td>
                    <td>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="cph_Busqueda" ContentPlaceHolderID="cph_ColumnaDerecha" runat="Server">
    <h1>
        Horarios actuales</h1>
    <asp:UpdatePanel ID="upd_Busqueda" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="Container">
                <asp:DataGrid ID="dg_HorarioOrdinario" CssClass="tablaResultados" runat="server"
                    AutoGenerateColumns="False" Width="100%" AllowPaging="True" PageSize="10">
                    <AlternatingItemStyle CssClass="tablaResultados_Alternate" />
                    <ItemStyle CssClass="tablaResultados_Item" />
                    <HeaderStyle CssClass="tablaResultados_Header" />
                    <FooterStyle CssClass="tablaResultados_Footer" />
                    <PagerStyle Mode="NumericPages" CssClass="tablaResultados_Pager" />
                    <Columns>
                        <asp:EditCommandColumn CancelText="Cancel" EditText="Edit" UpdateText="Update"></asp:EditCommandColumn>
                        <asp:TemplateColumn HeaderText="Dia">
                            <ItemTemplate>
                                <asp:Label ID="lbl_NombreDia" runat="server" Text="Dia"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Horas">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Descripcion" runat="server" Text="Horas"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                </asp:DataGrid>
                <asp:Label ID="lbl_ResultadoElementos" runat="server" Text="No hay elementos"></asp:Label>
            </div>
            
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

