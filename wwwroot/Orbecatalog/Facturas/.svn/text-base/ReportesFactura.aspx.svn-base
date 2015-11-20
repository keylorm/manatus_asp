<%@ Page Language="VB" MasterPageFile="~/Orbecatalog/OrbeCatalog.master" AutoEventWireup="false"
    CodeFile="ReportesFactura.aspx.vb" Inherits="Orbecatalog_Facturas_ReportesFactura"
    Title="Orbecatalog - Facturas - Reporte" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cph_Titulo" runat="Server">
    <asp:Literal ID="Literal1" runat="server" Text="Reporte de Facturas"></asp:Literal>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_Navegacion" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cph_Acciones" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cph_Contenido" runat="Server">
    <asp:UpdatePanel ID="Upd_pagina" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
            <div class="Container fieldset">
                <div>
                    <label>
                        Inicio de reporte
                    </label>
                    <asp:TextBox ID="txb_inicioReporte" Width="150px" runat="server"></asp:TextBox>
                    <obout:Calendar ID="startCalendar" runat="server" DatePickerMode="true" TextBoxId="txb_inicioReporte"
                        DatePickerImagePath="../iconos/calendar.gif" />
                </div>
                <div>
                    <label>
                        Fin de reporte
                    </label>
                    <asp:TextBox ID="txb_finalReporte" Width="150px" runat="server"></asp:TextBox>
                    <obout:Calendar ID="endCalendar" runat="server" DatePickerMode="true" TextBoxId="txb_finalReporte"
                        DatePickerImagePath="../iconos/calendar.gif" />
                </div>
                <div>
                    <label>
                        Estado de Factura
                    </label>
                    <asp:DropDownList ID="ddl_estadoFactura" runat="server">
                    </asp:DropDownList>
                </div>
                <div>
                    <orbelink:ActionButton ID="btn_mostrar" CausesValidation="true" ImageURL="~/Orbecatalog/iconos/salvar_small.png"
                        runat="server" Text="Generar" CssClass="positive">
                    </orbelink:ActionButton>                    
                </div>
            </div>
            <br />
            <br />
            <rsweb:ReportViewer Width="95%" ID="RW_factura" runat="server">
            </rsweb:ReportViewer>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="cph_BusquedaNueva" runat="Server">
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="cph_ColumnaDerecha" runat="Server">
</asp:Content>
