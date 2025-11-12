<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="V_Noticias.aspx.cs" Inherits="Intranet_3._0.Vistas.V_Comunicacion.V_Noticias" %>
<asp:Content ID="Content1" ContentPlaceHolderID="scripts_css" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="scripts_js" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <section class="pnl_table">
        <div class="pnl_tag">
            <p><i class="fas fa-tag"></i> Publicaciones</p>
        </div>

        <div>
            <div class="body-content-card" id="pnl-body-content-card"></div>
        </div>
    </section>

    <div class="modal_noticia" style="display: none;">
        <div class="modal_noticia_body">
            <div class="modal_noticia_content_" id="modal_noticia_content"></div>
        </div>
    </div>

</asp:Content>
