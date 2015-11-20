<%@ Page Language="VB" MasterPageFile="../OrbeCatalog.master" AutoEventWireup="false"
    CodeFile="Envios.aspx.vb" Inherits="_Envios" Title="OrbeCatalog - Envios" %>

<asp:Content ID="ctn_TituloSeccion" ContentPlaceHolderID="cph_Titulo" runat="Server">
    <asp:Literal runat="server" Text="<%$ Resources:Envios_Resources, Envios_Pantalla %>"></asp:Literal>
</asp:Content>
<asp:Content ID="ctn_Acciones" ContentPlaceHolderID="cph_Acciones" runat="server">
    <asp:UpdatePanel ID="upd_BotonesAcciones" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <orbelink:ActionButton ID="btn_IngresarNuevo" ImageURL="~/Orbecatalog/iconos/agregar_small.png"
                runat="server" Text="<%$ Resources:Orbecatalog_Resources, Nuevo %>">
            </orbelink:ActionButton>
            <orbelink:ActionButton ID="btn_Accion_Ayuda" ImageURL="~/Orbecatalog/iconos/info_small.png"
                runat="server" Text="<%$ Resources:Orbecatalog_Resources, Ayuda %>">
            </orbelink:ActionButton>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="ctn_Contenido" ContentPlaceHolderID="cph_Contenido" runat="Server">
    <asp:UpdatePanel ID="upd_PantallaContenido" runat="server" UpdateMode="Conditional"
        ChildrenAsTriggers="false">
        <ContentTemplate>
            <ajaxToolkit:TabContainer ID="tabs_Envios" runat="server">
                <ajaxToolkit:TabPanel ID="tab_Detalle" runat="server" HeaderText="<%$ Resources:Envios_Resources, Envio_Detalle %>">
                    <ContentTemplate>
                        <asp:UpdatePanel ID="upd_Contenido" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <h1>
                                    <asp:Literal runat="server" Text="<%$ Resources:Envios_Resources, Envio %>"></asp:Literal></h1>
                                <table class="Container" cellspacing="0" cellpadding="0" border="0">
                                    <tr>
                                        <td style="width: 25%;">
                                            <asp:Literal runat="server" Text="<%$ Resources:Envios_Resources, Envio_Subject %>"></asp:Literal>
                                        </td>
                                        <td style="width: 70%;">
                                            <asp:TextBox ID="tbx_SubjectEnvio" runat="server"></asp:TextBox>
                                        </td>
                                        <td style="width: 5%">
                                            <asp:RequiredFieldValidator ID="vld_Subject" runat="server" ControlToValidate="tbx_SubjectEnvio"
                                                Display="Dynamic" EnableClientScript="False" ErrorMessage="<%$ Resources:Orbecatalog_Resources, Campo_Requerido %>"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Literal runat="server" Text="<%$ Resources:Envios_Resources, Envio_Saludo %>"></asp:Literal>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tbx_Saludo" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Literal runat="server" Text="<%$ Resources:Envios_Resources, Envio_SenderName %>"></asp:Literal>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tbx_SenderName" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Literal runat="server" Text="<%$ Resources:Envios_Resources, Envio_SenderEmail %>"></asp:Literal>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tbx_SenderEmail" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Literal runat="server" Text="<%$ Resources:Envios_Resources, Envio_ReplyTo %>"></asp:Literal>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tbx_ReplyTo" runat="server"></asp:TextBox>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr id="tr_Entidad" runat="server">
                                        <td>
                                            <asp:Literal runat="server" Text="<%$ Resources:Envios_Resources, Envio_Entidad %>"></asp:Literal>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_Entidad" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                                <h1>
                                    Autentificacion</h1>
                                <table class="Container" cellspacing="0" cellpadding="0" border="0">
                                    <tr>
                                        <td style="width: 25%;">
                                            Servidor SMTP
                                        </td>
                                        <td style="width: 70%;">
                                            <asp:TextBox ID="tbx_ServerName" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Usuario SMTP
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tbx_SMTPUsername" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Password SMTP
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tbx_SMTPPassword" runat="server" TextMode="Password"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
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
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
                <ajaxToolkit:TabPanel ID="tab_Publicaciones" runat="server" HeaderText="<%$ Resources:Envios_Resources, Envios_Publicaciones %>">
                    <ContentTemplate>
                        <h1>
                            <asp:Literal runat="server" Text="<%$ Resources:Envios_Resources, Envios_Publicaciones %>"></asp:Literal></h1>
                        <br />
                        <asp:UpdatePanel ID="upd_Publicaciones" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddl_Publicaciones" runat="server">
                                </asp:DropDownList>
                                <br />
                                <asp:Button ID="btn_AgregarPublicacion" runat="server" Text="Agregar Publicacion" />
                                <br />
                                <br />
                                <b>Perteneciente</b><br />
                                <asp:ListBox ID="lst_Publicaciones" runat="server" Width="200"></asp:ListBox>
                                <br />
                                <asp:Button ID="btn_QuitarPublicacion" runat="server" Text="Quitar Publicacion" Width="100" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
                <ajaxToolkit:TabPanel ID="tab_Lotes" runat="server" HeaderText="<%$ Resources:Envios_Resources, Envios_Lotes %>">
                    <ContentTemplate>
                        <h1>
                            <asp:Literal runat="server" Text="<%$ Resources:Envios_Resources, Envios_Lotes %>"></asp:Literal></h1>
                        <asp:DataGrid ID="dg_Lotes" runat="server" AutoGenerateColumns="False" AllowPaging="False"
                            Width="100%">
                            <AlternatingItemStyle CssClass="tablaResultados_Alternate" />
                            <ItemStyle CssClass="tablaResultados_Item" />
                            <HeaderStyle CssClass="tablaResultados_Header" />
                            <FooterStyle CssClass="tablaResultados_Footer" />
                            <PagerStyle Mode="NumericPages" CssClass="tablaResultados_Pager" />
                            <Columns>
                                <asp:TemplateColumn HeaderText="Fecha Creado">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Fecha" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="25%" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Fecha Ultimo Envio">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_FechaEnviado" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="20%" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Enviar a ">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddl_EnviarA" runat="server">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                    <ItemStyle Width="20%" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Enviar">
                                    <ItemTemplate>
                                        <asp:Button ID="btn_EnviarAhora" runat="server" Text="Enviar" OnClick="btn_EnviarAhora_Click" />
                                    </ItemTemplate>
                                    <ItemStyle Width="20%" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Reporte">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="lnk_VerReporte" runat="server" Text="Ver Reporte"></asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle Width="15%" />
                                </asp:TemplateColumn>
                            </Columns>
                        </asp:DataGrid>
                        <asp:Label ID="lbl_NoLotes" runat="server" Text="No hay lotes para este envio"></asp:Label>
                        <asp:Panel ID="pnl_Reporte" runat="server" Width="100%">
                            <h1>
                                Reporte de Envios
                            </h1>
                            <div class="Container">
                                <asp:Literal ID="ltl_ScriptResponse" runat="server"></asp:Literal>
                                <div id="div_Response" runat="server">
                                </div>
                                <asp:HyperLink ID="lnk_StopSending" runat="server">Parar envio</asp:HyperLink>
                            </div>
                        </asp:Panel>
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
                <ajaxToolkit:TabPanel ID="tab_SubirExcel" runat="server" HeaderText="<%$ Resources:Envios_Resources, Envios_SubirExcel %>">
                    <ContentTemplate>
                        <asp:Panel ID="pnl_ArchivoExcel" runat="server" Width="100%">
                            <h1>
                                Archivo Excel</h1>
                            <div class="Container">
                                <table class="Container" cellspacing="0" cellpadding="0" border="0" width="100%">
                                    <tr>
                                        <td style="width: 25%;">
                                            Archivo
                                        </td>
                                        <td style="width: 75%">
                                            <asp:FileUpload ID="upl_Archivo" runat="server" Width="100" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Nombre Hoja
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tbx_upl_NombreHoja" runat="server" Text="Sheet1"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Columna Nombre
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tbx_upl_NombreEntidad" runat="server" Width="25px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Columna Email
                                        </td>
                                        <td>
                                            <asp:TextBox ID="tbx_upl_Email" runat="server" Width="25px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Lote
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddl_Lotes" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btn_SubirExcel" runat="server" Text="Subir Correos" />
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </asp:Panel>
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
            </ajaxToolkit:TabContainer>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="cph_Busqueda" ContentPlaceHolderID="cph_ColumnaDerecha" runat="Server">
    <h1>
        Envios Actuales</h1>
    <%--<asp:UpdatePanel ID="upd_Busqueda" runat="server" UpdateMode="Conditional">
        <ContentTemplate>--%>
    <div class="Container">
        <asp:DataGrid ID="dg_Envio" runat="server" AutoGenerateColumns="False" AllowPaging="True"
            PageSize="10" CssClass="tablaResultados">
            <AlternatingItemStyle CssClass="tablaResultados_Alternate" />
            <ItemStyle CssClass="tablaResultados_Item" />
            <HeaderStyle CssClass="tablaResultados_Header" />
            <FooterStyle CssClass="tablaResultados_Footer" />
            <PagerStyle Mode="NumericPages" CssClass="tablaResultados_Pager" />
            <Columns>
                <asp:EditCommandColumn CancelText="Cancel" EditText="Edit" UpdateText="Update"></asp:EditCommandColumn>
                <asp:TemplateColumn HeaderText="<%$ Resources:Envios_Resources, Envio_Subject %>">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Subject" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
            </Columns>
        </asp:DataGrid>
        <asp:Label ID="lbl_ResultadoElementos" runat="server" Text="No hay elementos"></asp:Label>
    </div>
    <%--</ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btn_SubirExcel" />
        </Triggers>
    </asp:UpdatePanel>--%>
</asp:Content>
