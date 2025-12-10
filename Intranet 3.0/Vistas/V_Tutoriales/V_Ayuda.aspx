<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="V_Ayuda.aspx.cs" Inherits="Intranet_3._0.Vistas.V_Tutoriales.V_Ayuda" %>
<asp:Content ID="Content1" ContentPlaceHolderID="scripts_css" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="scripts_js" runat="server">
    <link type="text/css" rel="stylesheet" href="/styles/css/tutoriales/tutoriales.css">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Listado dinámico de tutoriales administrables -->
    <section class="pnl_table">
        <div class="pnl_tag">
            <p><i class="fas fa-tag"></i> Tutoriales de MIETIB</p>
        </div>

        <div class="anuncios-body">
            <div class="anuncios-content">
                <asp:Panel ID="pnlSinTutoriales" runat="server" CssClass="alert alert-info" Visible="false">
                    Aún no hay tutoriales publicados. Cuando un administrador los cree aparecerán aquí.
                </asp:Panel>

                <asp:Repeater ID="rptTutoriales" runat="server" OnItemDataBound="rptTutoriales_ItemDataBound">
                    <ItemTemplate>
                        <div class="video-tumb" runat="server" id="cardTutorial">
                            <div class="video-info">
                                <h3 class="video-title"><%# Eval("Titulo") %></h3>
                                <p class="video-duration"><%# Eval("Descripcion") %></p>
                            </div>

                            <asp:Image ID="imgPortada" runat="server" CssClass="tutorial-img" Visible="false" />
                            <asp:HyperLink ID="lnkTutorial" runat="server" CssClass="tutorial-link" Target="_blank">Ver tutorial</asp:HyperLink>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </section>

</asp:Content>
