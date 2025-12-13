<%@ Page Title="Tutoriales" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="V_Ayuda.aspx.cs" Inherits="Intranet_3._0.Vistas.V_Tutoriales.V_Ayuda" %>

<asp:Content ID="Content1" ContentPlaceHolderID="scripts_css" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="scripts_js" runat="server">
    <link rel="Stylesheet" href="/Styles/css/tutoriales/tutoriales.css" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    
    <section class="pnl_table">
        <div class="pnl_tag">
            <p><i class="fas fa-graduation-cap"></i>Centro de Ayuda - Tutoriales</p>
        </div>
        <div class="pnl_body">
            <asp:PlaceHolder ID="phTutorialesVacio" runat="server" Visible="false">
                <p class="text-center">No hay tutoriales disponibles en este momento.</p>
            </asp:PlaceHolder>
            
            <asp:Repeater ID="rptTutoriales" runat="server">
                <ItemTemplate>
                    <div class="tutorial-card">
                        <div class="tutorial-header">
                            <h3 class="tutorial-title"><%# Eval("Titulo") %></h3>
                            <span class="tutorial-seccion"><%# Eval("Seccion") %></span>
                        </div>
                        <div class="tutorial-body">
                            <p class="tutorial-description"><%# Eval("Descripcion") %></p>
                            <div class="tutorial-video-container">
                                <video class="tutorial-video" controls>
                                    <source src='<%# ResolveUrl(Eval("Video") as string ?? "") %>' type="video/mp4">
                                    Tu navegador no soporta el elemento de video.
                                </video>
                            </div>
                        </div>
                        <div class="tutorial-footer">
                            <small class="text-muted">
                                <i class="far fa-calendar-alt"></i> 
                                <%# Eval("Fecha_Creacion", "{0:dd/MM/yyyy}") %>
                            </small>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </section>

</asp:Content>