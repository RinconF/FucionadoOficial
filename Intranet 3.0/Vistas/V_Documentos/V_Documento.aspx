<%@ Page Title="Documentos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="V_Documento.aspx.cs" Inherits="Intranet_3._0.Vistas.V_Documentos.V_Documentos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="scripts_css" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="scripts_js" runat="server">
    <link rel="Stylesheet" href="/Styles/css/documentos/documentos.css" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    
    <section class="pnl_table">
        <div class="pnl_tag">
            <p><i class="fas fa-file-alt"></i>Documentos Corporativos</p>
        </div>
        <div class="pnl_body">
            <asp:PlaceHolder ID="phDocumentosVacio" runat="server" Visible="false">
                <p class="text-center">No hay documentos disponibles en este momento.</p>
            </asp:PlaceHolder>
            
            <asp:Repeater ID="rptDocumentos" runat="server">
                <ItemTemplate>
                    <div class="documento-card">
                        <div class="documento-icon">
                            <i class='<%# ObtenerIconoDocumento(Eval("Archivo") as string) %>'></i>
                        </div>
                        <div class="documento-info">
                            <h4 class="documento-titulo"><%# Eval("Titulo") %></h4>
                            <p class="documento-descripcion"><%# Eval("Descripcion") %></p>
                            <div class="documento-meta">
                                <span class="documento-fecha">
                                    <i class="far fa-calendar"></i>
                                    <%# Eval("FechaCreacion", "{0:dd/MM/yyyy}") %>
                                </span>
                            </div>
                        </div>
                        <div class="documento-acciones">
                            <a href='<%# ResolveUrl(Eval("Archivo") as string ?? "#") %>' 
                               class="btn btn-download" 
                               download 
                               title="Descargar documento">
                                <i class="fas fa-download"></i> Descargar
                            </a>
                            <%# !string.IsNullOrEmpty(Eval("Url") as string) ? 
                                "<a href='" + Eval("Url") + "' class='btn btn-link' target='_blank' title='Ver enlace externo'><i class='fas fa-external-link-alt'></i> Ver online</a>" : "" %>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </section>

</asp:Content>