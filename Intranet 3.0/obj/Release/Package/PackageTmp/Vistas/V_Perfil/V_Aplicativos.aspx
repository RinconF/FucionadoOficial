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
            <div class="card-body-app">
                <div class="card text-center">
                    <div class="card-body">
                        <img src="../../Content/img/portal_nomina.png" class="card-img-top" />
                    </div>
                    <div class="card-footer">
                        <a href="http://200.91.219.103:35693/AuthAG/LoginFormAG?IdCia=1&NroConexion=1" target="_blank" data-name="PORTAL" data-description="NOMINA">
                            <p>Siesa Portal Nómina</p>
                        </a>
                    </div>
                </div>
            </div>
            <div class="card-body-app">
                <div class="card text-center">
                    <div class="card-body">
                        <img src="../../Content/img/maximo.jpg" class="card-img-top" />
                    </div>
                    <div class="card-footer">
                        <a href="http://www.maximoetib.com.co/maximo" target="_blank" data-name="IBM" data-description="MAXIMO">
                            <p>Control Mantenimiento Flota</p>
                        </a>
                    </div>
                </div>
            </div>

            <div class="card-body-app">
                <div class="card text-center">
                    <div class="card-body">
                        <img src="../../Content/img/siesa.jpg" class="card-img-top" />
                    </div>
                    <div class="card-footer">
                        <a href="http://200.91.219.106:46713/siesa/jsp/index.jsp?pre=S" target="_blank" data-name="SIESA" data-description="NOMINA">
                            <p>Control Financiero e Inventarios</p>
                        </a>
                    </div>
                </div>
            </div>

            <div class="card-body-app">
                <div class="card text-center">
                    <div class="card-body">
                        <img src="../../Content/img/bit.png" class="card-img-top" />
                    </div>
                    <div class="card-footer">
                        <a href="https://www.bitenterprise.com.co/" target="_blank" data-name="BIT" data-description="ENTERPRISE">
                            <p>Gestión Administrativa Empresarial</p>
                        </a>
                    </div>
                </div>
            </div>

            <div class="card-body-app">
                <div class="card text-center">
                    <div class="card-body">
                        <img src="../../Content/img/moodle.png" class="card-img-top" />
                    </div>
                    <div class="card-footer">
                        <a href="http://educaetib.com/moodle/login/index.php" target="_blank" data-name="MOODLE">
                            <p>Capacitación y Aprendizaje </p>
                        </a>
                    </div>
                </div>
            </div>

            <div class="card-body-app">
                <div class="card text-center">
                    <div class="card-body">
                        <img src="../../Content/img/etib.png" class="card-img-top" />
                    </div>
                    <div class="card-footer">
                        <p target="_blank" data-name="DESPRENDIBLES" data-description="DE PAGO">
                            <asp:LinkButton ID="LinkDesprediblesNomina" runat="server" OnClick="LinkDesprediblesNomina_Click">
                              <p>Desprendibles de pago</p>
                            </asp:LinkButton>
                        </p>
                    </div>
                </div>
            </div>
            
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

            <div class="card-body-app">
                <div class="card text-center">
                    <div class="card-body">
                        <img src="../../Content/img/alfresco.png" class="card-img-top" />
                    </div>
                    <div class="card-footer">
                        <a href="http://documentos.etib.co" target="_blank" data-name="ALFRESCO">
                            <p>Portal Documental</p>
                        </a>
                    </div>
                </div>
            </div>


            <div class="card-body-app">
                <div class="card text-center">
                    <div class="card-body">
                        <img src="../../Content/img/webmail.png" class="card-img-top" />
                    </div>
                    <div class="card-footer">
                        <a href="https://etib.com.co:2096/" target="_blank" data-name="WEBMAIL">
                            <p>Correo Empresarial</p>
                        </a>
                    </div>
                </div>
            </div>


            <div class="card-body-app">
                <div class="card text-center">
                    <div class="card-body">
                        <img src="../../Content/img/etib.png" class="card-img-top" />
                    </div>
                    <div class="card-footer">
                        <a href="https://etib.com.co/index.php?lang=es" target="_blank" data-name="ETIB WEB">
                            <p>Pagina Web Oficial</p>
                        </a>
                    </div>
                </div>
            </div>
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
            <div class="card-body-app">
                <div class="card text-center">
                    <div class="card-body">
                        <img src="../../Content/img/itop.png" class="card-img-top" />
                    </div>
                    <div class="card-footer">
                        <a href="http://sistemas.etib.co" target="_blank" data-name="HELPDESK">
                            <p>Soporte Tecnológico</p>
                        </a>
                    </div>
                </div>
            </div>
        </div>
    </section>

</asp:Content>
