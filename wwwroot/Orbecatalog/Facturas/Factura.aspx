<%@ Page Language="VB" MasterPageFile="~/orbecatalog/OrbeCatalog.master" AutoEventWireup="false"
    CodeFile="Factura.aspx.vb" Inherits="orbecatalog_Facturas_Factura" Title="OrbeCatalog - Factura" %>

<asp:Content ID="ctn_Titulo" ContentPlaceHolderID="cph_titulo" runat="Server">
    Factura
</asp:Content>
<asp:Content ID="ctn_Contenido" ContentPlaceHolderID="cph_contenido" runat="Server">
    <asp:UpdatePanel ID="Upd_pagina" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <ajaxToolkit:TabContainer ID="tabs_Factura" runat="server">
                <ajaxToolkit:TabPanel ID="tab_InformacionFactura" runat="server" HeaderText="Información de la Factura">
                    <ContentTemplate>
                        <h1>
                            Información de la Factura</h1>
                        <asp:UpdatePanel ID="upd_Factura" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="fieldset">
                                    <div>
                                        <h2>
                                            Entidades</h2>
                                    </div>
                                    <div>
                                        <label>
                                            Cliente
                                        </label>
                                        <asp:DropDownList ID="cmb_Entidad" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                    <div>
                                        <label>
                                            Vendedor
                                        </label>
                                        <asp:Label ID="lbl_Vendedor" runat="server" Text="Vendedor"></asp:Label>
                                    </div>
                                    <div>
                                        <h2>
                                            Pago</h2>
                                    </div>
                                    <div>
                                        <label>
                                            Dias de Crédito*
                                        </label>
                                        <asp:TextBox ID="txt_PorcentajeImpuestos" runat="server"></asp:TextBox>
                                    </div>
                                    <div>
                                        <label>
                                            Enviar Por
                                        </label>
                                        <asp:TextBox ID="txt_EnviarPor" runat="server"></asp:TextBox>
                                    </div>
                                    <div id="div_FechaPago" runat="server">
                                        <label>
                                            Fecha Pago*
                                        </label>
                                        <BDP:BDPLite ID="bdp_FechaPago" runat="server">
                                        </BDP:BDPLite>
                                    </div>
                                    <div>
                                        <h2>
                                            Factura</h2>
                                    </div>
                                    <div>
                                        <label>
                                            Tipo de Factura
                                        </label>
                                        <asp:DropDownList ID="cmb_TipoFactura" runat="server" DataTextField="n_Tipo" DataValueField="id_Tipo">
                                        </asp:DropDownList>
                                    </div>
                                    <div>
                                        <label>
                                            Número de Factura*
                                        </label>
                                        <asp:TextBox ID="txt_NumeroFactura" runat="server"></asp:TextBox>
                                    </div>
                                    <div>
                                        <label>
                                            Fecha Factura*
                                        </label>
                                        <BDP:BDPLite ID="txt_FechaFactura" runat="server" />
                                    </div>
                                    <div>
                                        <label>
                                            Unidad de Venta
                                        </label>
                                        <asp:DropDownList ID="ddl_Unidad" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                    <div>
                                        <h2>
                                            Comprobante</h2>
                                    </div>
                                    <div>
                                        <label>
                                            Comprobante
                                        </label>
                                        <asp:TextBox ID="txt_Comprobante" runat="server"></asp:TextBox>
                                    </div>
                                    <div id="div_FechaComprobante" runat="server">
                                        <label>
                                            Fecha Comprobante
                                        </label>
                                        <asp:Label ID="lbl_FechaComprobante" runat="server"></asp:Label>
                                    </div>
                                    <div>
                                        <label>
                                            Direccion Alterna</label>
                                        <asp:TextBox ID="tbx_DireccionAlterna" runat="server" TextMode="MultiLine" Height="100"></asp:TextBox>
                                    </div>
                                    <div>
                                        <h1>
                                            Detalle Totales</h1>
                                    </div>
                                    <div>
                                        <label>
                                            Total
                                        </label>
                                        <asp:Label ID="lbl_Total" runat="server" Font-Bold="True"></asp:Label>
                                    </div>
                                    <div>
                                        <label>
                                            Sub Total
                                        </label>
                                        <asp:Label ID="lbl_subTotalInfo" runat="server" Font-Bold="True"></asp:Label>
                                    </div>
                                    <div>
                                        <asp:HyperLink ID="hyl_reportDetalle" Visible="false" runat="server">Ver reporte factura</asp:HyperLink>
                                    </div>
                                    <div>
                                        &nbsp;</div>
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
                                        <orbelink:ActionButton ID="btn_MarcarCancelado" ImageURL="~/Orbecatalog/iconos/modificar_small.png"
                                            runat="server" Text="Marcar Cancelado">
                                        </orbelink:ActionButton>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
                <ajaxToolkit:TabPanel ID="tab_Productos" runat="server" HeaderText="Productos en factura">
                    <ContentTemplate>
                        <h1>
                            Detalle Factura</h1>
                        <asp:UpdatePanel ID="upd_DetalleFactura" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:DataGrid ID="dg_DetalleFactura" runat="server" AutoGenerateColumns="False"
                                    CssClass="tablaResultados">
                                    <AlternatingItemStyle CssClass="tablaResultados_Alternate" />
                                    <ItemStyle CssClass="tablaResultados_Item" />
                                    <HeaderStyle CssClass="tablaResultados_Header" />
                                    <FooterStyle CssClass="tablaResultados_Footer" />
                                    <Columns>
                                        <asp:TemplateColumn HeaderText="Control">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkbtn_Delete" runat="server">Eliminar</asp:LinkButton>
                                               <%-- /
                                                <asp:LinkButton ID="lnkbtn_Modificar" runat="server">Cargar</asp:LinkButton>--%>
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Detalle">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_DetalleFactura" runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="15%" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Cantidad">
                                            <ItemStyle Width="10%" />
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Cantidad" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Precio Unitario">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_PrecioUnitario" runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Precio Unitario Extra">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_PrecioUnitarioExtra" runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Descuento">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_descuento" runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Precio Venta">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_PrecioVenta" runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                </asp:DataGrid>
                                <div style="text-align: right; margin-right: 125px; margin-top: 15px">
                                    <asp:Label ID="lbl_totalFacturaTitulo" Visible="false" runat="server" Text="Total:"
                                        Style="font-weight: bold"></asp:Label>
                                    <asp:Label ID="lbl_totalFactura" runat="server" Visible="false" Text="Label"></asp:Label>
                                </div>
                                <asp:HyperLink ID="hyl_reporteDetalle" Visible="false" Font-Size="Medium" runat="server">Ver reporte factura</asp:HyperLink>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <br />
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
                <ajaxToolkit:TabPanel ID="tab_reservaciones" runat="server" HeaderText="Reservacion">
                    <ContentTemplate>
                        <asp:UpdatePanel ID="Upd_reserva" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <iframe style="border: solid 1px white; width: 100%; height: 975px" frameborder="0"
                                    scrolling="auto" id="if_reservacion" runat="server"></iframe>
                                <br />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </ContentTemplate>
                </ajaxToolkit:TabPanel>
            </ajaxToolkit:TabContainer>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
