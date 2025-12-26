<%@ Page ValidateRequest="false" Title="Tutoriales" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="V_Ayuda.aspx.cs" Inherits="Intranet_3._0.Vistas.V_Tutoriales.V_Ayuda" %>

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
                        <div class="tutorial-icon">
                            <i class='<%# ObtenerIconoSeccion(Eval("Seccion") as string) %>'></i>
                        </div>
                        <div class="tutorial-info">
                            <h4 class="tutorial-titulo"><%# Eval("Titulo") %></h4>
                            <span class="tutorial-seccion-badge"><%# Eval("Seccion") %></span>
                            <p class="tutorial-descripcion"><%# Eval("Descripcion") %></p>
                            <div class="tutorial-meta">
                                <span class="tutorial-fecha">
                                    <i class="far fa-calendar"></i>
                                    <%# Eval("Fecha_Creacion", "{0:dd/MM/yyyy}") %>
                                </span>
                            </div>
                        </div>
                        <div class="tutorial-acciones">
                            <%# !string.IsNullOrEmpty(Eval("Video") as string) ? 
                                "<a href='" + ResolveUrl(Eval("Video") as string) + "' class='btn btn-ver-video' target='_blank' title='Ver tutorial'><i class='fas fa-play'></i> Ver Tutorial</a>" : 
                                "<span class='btn btn-disabled'>Sin video</span>" %>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </section>

</asp:Content>
