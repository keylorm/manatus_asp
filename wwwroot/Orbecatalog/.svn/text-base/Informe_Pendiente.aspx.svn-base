<%@ Page Language="VB" MasterPageFile="OrbeCatalog.master" AutoEventWireup="false"
    CodeFile="Informe_Pendiente.aspx.vb" Inherits="_Informe_Pendiente" Title="Orbe Catalog - Informe de Pendiente" %>

<%@ Register Assembly="BasicFrame.WebControls.BasicDatePicker" Namespace="BasicFrame.WebControls"
    TagPrefix="BDP" %>

<asp:Content ID="ctn_TituloSeccion" ContentPlaceHolderID="cph_Titulo" runat="Server">
    Informe de Pendiente
</asp:Content>
<asp:Content ID="ctn_Contenido" ContentPlaceHolderID="cph_Contenido" runat="Server">
      <table class="Container" cellspacing="0" cellpadding="0" border="0">
        <tr>
            <td style="width: 25%;">
            </td>
            <td style="width: 70%;">
                &nbsp;</td>
            <td style="width: 5%">
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <h2>
                    <asp:Label ID="lbl_NombrePendiente" runat="server" Text="Pendiente"></asp:Label></h2>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <i>
                    <asp:Label ID="lbl_Descripcion" runat="server" Text="Descripcion"></asp:Label></i>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                Por</td>
            <td>
                <asp:Label ID="lbl_Autor" runat="server" Text="Autor"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td>
                Programado</td>
            <td>
                <asp:Label ID="lbl_FechaProgramado" runat="server" Text="Programado"></asp:Label>
            </td>
            <td>
            </td>
        </tr>
    </table>
    <br />
    <h2>
        Informes</h2>
    <asp:UpdatePanel ID="upd_Contenido" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="Container">
                <asp:DataGrid ID="dg_Pendientes_TareaProyectos" runat="server" AutoGenerateColumns="False"
                    CssClass="tableClean" ShowHeader="false">
                    <Columns>
                        <asp:TemplateColumn>
                            <ItemTemplate>
                                <span style="font-size: 90%">
                                    <asp:Label ID="lbl_Entidad" runat="server" Text="Entidad"></asp:Label>,
                                    <asp:Label ID="lbl_Fecha" runat="server" Text="Fecha"></asp:Label><br />
                                </span>
                                <asp:Label ID="lbl_Comentario" runat="server" Text="Comentario"></asp:Label><hr />
                            </ItemTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                </asp:DataGrid>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="cph_Busqueda" ContentPlaceHolderID="cph_ColumnaDerecha" runat="Server">
    <h1>
        Agregar entrada al informe</h1>
    <asp:UpdatePanel ID="upd_Busqueda" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="Container">
                <table class="Container" cellspacing="0" cellpadding="0" border="0">
                    <tr>
                        <td style="width: 25%;">
                            Comentario</td>
                        <td style="width: 75%">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:TextBox ID="tbx_Comentario" runat="server" Height="87px" TextMode="MultiLine"
                                Width="232px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            Terminar</td>
                        <td>
                            <asp:CheckBox ID="chk_Terminar" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <orbelink:ActionButton ID="btn_Salvar" CausesValidation="true" ImageURL="~/Orbecatalog/iconos/salvar_small.png"
                        runat="server" Text="<%$ Resources:Orbecatalog_Resources, Salvar %>" CssClass="positive">
                    </orbelink:ActionButton>
                            
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
