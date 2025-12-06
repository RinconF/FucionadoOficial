<%@ Page Title="Aplicativos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="V_Aplicativos.aspx.cs" Inherits="Intranet_3._0.Vistas.V_Perfil.V_Aplicativos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="scripts_css" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="scripts_js" runat="server">
    <link rel="Stylesheet" href="/Styles/css/aplicativos/aplicativos.css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <section class="pnl_table">
        <div class="pnl_tag">
            <p><i class="fas fa-tag"></i>Aplicativos Empresariales</p>
        </div>
        <div class="pnl_body">
            <asp:PlaceHolder ID="phEmpresarialesVacio" runat="server" Visible="false">
                <p class="text-center">No hay aplicativos empresariales disponibles.</p>
            </asp:PlaceHolder>
            <asp:Repeater ID="rptEmpresariales" runat="server">
                <ItemTemplate>
                    <div class="card-body-app">
                        <div class="card text-center">
                            <div class="card-body">
                                <img src='<%# ResolveUrl(Eval("Imagen") as string ?? "~/Content/img/etib.png") %>' class="card-img-top" alt='<%# Eval("Titulo") %>' />
                            </div>
                            <div class="card-footer">
                                <asp:HyperLink runat="server" NavigateUrl='<%# Eval("UrlProcesada") %>' Target="_blank" data-name='<%# Eval("Titulo") %>' data-description='<%# Eval("Descripcion") %>'>
                                    <p><%# Eval("Titulo") %></p>
                                </asp:HyperLink>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </section>

    <br />
    <br />
    <br />

    <section class="pnl_table">
        <div class="pnl_tag">
            <p><i class="fas fa-tag"></i>Aplicativos Consulta</p>
        </div>
        <div class="pnl_body">
            <asp:PlaceHolder ID="phConsultaVacio" runat="server" Visible="false">
                <p class="text-center">No hay aplicativos de consulta disponibles.</p>
            </asp:PlaceHolder>
            <asp:Repeater ID="rptConsulta" runat="server">
                <ItemTemplate>
                    <div class="card-body-app">
                        <div class="card text-center">
                            <div class="card-body">
                                <img src='<%# ResolveUrl(Eval("Imagen") as string ?? "~/Content/img/etib.png") %>' class="card-img-top" alt='<%# Eval("Titulo") %>' />
                            </div>
                            <div class="card-footer">
                                <asp:HyperLink runat="server" NavigateUrl='<%# Eval("UrlProcesada") %>' Target="_blank" data-name='<%# Eval("Titulo") %>' data-description='<%# Eval("Descripcion") %>'>
                                    <p><%# Eval("Titulo") %></p>
                                </asp:HyperLink>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </section>

    <br />
    <br />
    <br />

    <section class="pnl_table">
        <div class="pnl_tag">
            <p><i class="fas fa-tag"></i>Aplicativos Soporte</p>
        </div>
        <div class="pnl_body">
            <asp:PlaceHolder ID="phSoporteVacio" runat="server" Visible="false">
                <p class="text-center">No hay aplicativos de soporte disponibles.</p>
            </asp:PlaceHolder>
            <asp:Repeater ID="rptSoporte" runat="server">
                <ItemTemplate>
                    <div class="card-body-app">
                        <div class="card text-center">
                            <div class="card-body">
                                <img src='<%# ResolveUrl(Eval("Imagen") as string ?? "~/Content/img/etib.png") %>' class="card-img-top" alt='<%# Eval("Titulo") %>' />
                            </div>
                            <div class="card-footer">
                                <asp:HyperLink runat="server" NavigateUrl='<%# Eval("UrlProcesada") %>' Target="_blank" data-name='<%# Eval("Titulo") %>' data-description='<%# Eval("Descripcion") %>'>
                                    <p><%# Eval("Titulo") %></p>
                                </asp:HyperLink>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </section>
</asp:Content>
