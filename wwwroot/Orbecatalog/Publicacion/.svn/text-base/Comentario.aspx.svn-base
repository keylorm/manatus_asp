<%@ Page Language="VB" MasterPageFile="~/Orbecatalog/popUp.master" AutoEventWireup="false"
    CodeFile="Comentario.aspx.vb" Inherits="Orbecatalog_Publicacion_Comentario" %>

<asp:Content ID="Content3" ContentPlaceHolderID="cph_Head" runat="Server">
    <link href="../css/rating.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cph_Titulo" runat="Server">
    Comentario
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph_Contenido" runat="Server">
    <asp:UpdatePanel ID="upd_ContenidoPantalla" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <h1>
                Comentario</h1>
            <div class="Container fieldset">
                <div id="div_EscritoPor" runat="server">
                    <label>
                        Escrito por
                    </label>
                    <asp:Label ID="lbl_EscritorPor" runat="server"></asp:Label>
                </div>
                <div>
                    <label>
                        Titulo
                    </label>
                    <asp:TextBox ID="tbx_Titulo" runat="server"></asp:TextBox>
                </div>
                <div>
                    <label>
                        Rating
                    </label>
                    <ajaxToolkit:Rating ID="rate_Comentario" runat="server" 
                        CurrentRating="1" MaxRating="5" StarCssClass="ratingStar" WaitingStarCssClass="savedRatingStar"
                        FilledStarCssClass="filledRatingStar" EmptyStarCssClass="emptyRatingStar" />
                </div>
                <div id="div_FechaIngreso" runat="server">
                    <label>
                        Fecha Creado
                    </label>
                    <asp:Label ID="lbl_FechaCreado" runat="server"></asp:Label>
                </div>
            </div>
            <br />
            <div>
                <h2>
                    El Comentario</h2>
                <br />
                <winthusiasm:HtmlEditor ID="tbx_Comentario" runat="server" Height="200px" />
            </div>
            <br />
            <div class="Container fieldset">
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
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <br />
</asp:Content>
