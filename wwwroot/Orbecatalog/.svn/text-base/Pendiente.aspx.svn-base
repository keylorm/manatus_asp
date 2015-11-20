<%@ Page Language="VB" MasterPageFile="OrbeCatalog.master" AutoEventWireup="false"
    CodeFile="Pendiente.aspx.vb" Inherits="_Pendiente" Title="Orbe Catalog - Pendiente" %>

<asp:Content ID="ctn_TituloSeccion" ContentPlaceHolderID="cph_Titulo" runat="Server">
    Manejo de Pendientes
</asp:Content>
<asp:Content ID="ctn_Contenido" ContentPlaceHolderID="cph_Contenido" runat="Server">
    <h1>
        Detalle de Pendiente</h1>
    <asp:UpdatePanel ID="upd_Contenido" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <table class="Container" cellspacing="0" cellpadding="0" border="0" width="100%">
                <tr>
                    <td colspan="3">
                        <h2>
                            Pendiente</h2>
                    </td>
                </tr>
                <tr>
                    <td>
                        Nombre</td>
                    <td>
                        <asp:TextBox ID="tbx_NombrePendiente" runat="server"></asp:TextBox></td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        Por</td>
                    <td>
                        <asp:DropDownList ID="ddl_Publicador" runat="server">
                        </asp:DropDownList></td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        Para</td>
                    <td>
                        <asp:DropDownList ID="ddl_Responsable" runat="server">
                        </asp:DropDownList></td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        Prioridad</td>
                    <td>
                        <asp:DropDownList ID="ddl_Prioridad" runat="server">
                            <asp:ListItem Selected="True" Value="0">Baja</asp:ListItem>
                            <asp:ListItem Value="1">Archivo</asp:ListItem>
                            <asp:ListItem Value="2">Alta</asp:ListItem>
                        </asp:DropDownList></td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        Comentario</td>
                    <td>
                        <asp:TextBox ID="tbx_Descripcion" runat="server" Height="87px" TextMode="MultiLine"></asp:TextBox></td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td style="width: 25%;">
                        &nbsp;
                    </td>
                    <td style="width: 70%;">
                        &nbsp;</td>
                    <td style="width: 5%">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <h2>
                            Fechas
                        </h2>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        Fecha Inicio</td>
                    <td>
                        <asp:Label ID="lbl_Inicio" runat="server" Text="Fecha Inicio"></asp:Label>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        Fecha Programado</td>
                    <td>
                        <BDP:BDPLite ID="tbx_FechaProgramado" runat="server" />
                    </td>
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
        Pendientes Actuales</h1>
    <asp:UpdatePanel ID="upd_Busqueda" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="Container">
                Prioridad:<br />
                <asp:DropDownList ID="ddl_Filtro_Estados" runat="server" AutoPostBack="true">
                    <asp:ListItem Value="0">Baja</asp:ListItem>
                    <asp:ListItem Value="1">Archivo</asp:ListItem>
                    <asp:ListItem Value="2">Alta</asp:ListItem>
                    <asp:ListItem Selected="True">Todos</asp:ListItem>
                </asp:DropDownList><br />
                <br />
                <asp:DataGrid ID="dg_Pendientes" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    PageSize="10" CssClass="tablaResultados">
                    <AlternatingItemStyle CssClass="tablaResultados_Alternate" />
                    <ItemStyle CssClass="tablaResultados_Item" />
                    <HeaderStyle CssClass="tablaResultados_Header" />
                    <FooterStyle CssClass="tablaResultados_Footer" />
                    <PagerStyle Mode="NumericPages" CssClass="tablaResultados_Pager" />
                    <Columns>
                        <asp:EditCommandColumn CancelText="Cancel" EditText="Edit" UpdateText="Update">
                            <ItemStyle Width="10%" />
                        </asp:EditCommandColumn>
                        <asp:TemplateColumn HeaderText="Nombre">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Nombre" runat="server" Text="Nombre"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="35%" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Estado">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Estado" runat="server" Text="Label"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="15%" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Entidad">
                            <ItemTemplate>
                                <asp:Label ID="lbl_Entidad" runat="server" Text="Label"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="35%" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="">
                            <ItemTemplate>
                                <asp:Image ID="img_Estado" runat="server" />
                            </ItemTemplate>
                            <ItemStyle Width="5%" />
                        </asp:TemplateColumn>
                    </Columns>
                </asp:DataGrid>
                <asp:Label ID="lbl_ResultadoElementos" runat="server" Text="No hay elementos"></asp:Label>
                <br />
            </div>
            
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddl_Filtro_Estados" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

