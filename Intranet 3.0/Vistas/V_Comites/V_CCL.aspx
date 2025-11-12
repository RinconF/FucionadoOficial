<%@ Page ValidateRequest="false" Title="CCL" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="V_CCL.aspx.cs" Inherits="Intranet_3._0.Vistas.V_Comites.CCL" %>

<asp:Content ID="Content1" ContentPlaceHolderID="scripts_css" runat="server">
    <link href="../../Styles/bootstrap/bootstrap.min.css" rel="stylesheet" />
    <link rel="Stylesheet" href="/Styles/css/default_encuestas/default_encuestas.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="Stylesheet" href="/Styles/css/copasst/copasst.css" />
    <br />
    <div class="pnl_tag">
        <p><i class="fa fa-tag"></i>¡Conozca el CCL!</p>
    </div>
    
    <section>
        <div class="container-fluid">
            <div class="row">
                <div class="col-12">
                    <div class="copasst-grid">
                        
                        <!-- ¿Qué es el CCL? -->
                        <div class="copasst-card">
                            <div class="copasst-card-header">
                                <h3>¿Qué es el CCL?</h3>
                            </div>
                            <div class="copasst-card-body">
                                <div class="content-preview">
                                    <p>El Comité de Convivencia Laboral (CCL) es un grupo de vigilancia conformado por empleados de la empresa...</p>
                                </div>
                                <button type="button" class="btn-ver-mas" onclick="abrirModal('¿Qué es el CCL?', `
                                    <p>El <strong>Comité de Convivencia Laboral (CCL)</strong> es un grupo de vigilancia conformado por empleados de la empresa quienes contribuyen a proteger a los trabajadores contra los riesgos psicosociales que puedan afectar su salud, como es el caso del estrés laboral y el acoso laboral.</p>
                                `)">Ver Más</button>
                            </div>
                        </div>

                        <!-- Objetivo del CCL -->
                        <div class="copasst-card">
                            <div class="copasst-card-header">
                                <h3>Objetivo del CCL</h3>
                            </div>
                            <div class="copasst-card-body">
                                <div class="content-preview">
                                    <p>El artículo 14 de la Resolución número 2646 del 17 de julio de 2008, contempla como medida preventiva de acoso laboral...</p>
                                </div>
                                <button type="button" class="btn-ver-mas" onclick="abrirModal('Objetivo del CCL', `
                                    <p>El artículo 14 de la <strong>Resolución número 2646 del 17 de julio de 2008</strong>, contempla como medida preventiva de acoso laboral el Conformar el Comité de Convivencia Laboral y establecer un procedimiento interno confidencial, conciliatorio y efectivo para prevenir las conductas de acoso laboral.</p>
                                    <p><strong>ETIB S.A.S</strong> al constituir el Comité de Convivencia Laboral implementa una medida preventiva de acoso laboral que contribuye a proteger a los trabajadores contra los riesgos psicosociales que afectan la salud en los lugares de trabajo.</p>

                                `)">Ver Más</button>
                            </div>
                        </div>

                        <!-- Alcance del CCL -->
                        <div class="copasst-card">
                            <div class="copasst-card-header">
                                <h3>Alcance del CCL</h3>
                            </div>
                            <div class="copasst-card-body">
                                <div class="content-preview">
                                    <p>El Comité de Convivencia Laboral tiene por objeto contribuir con mecanismos alternativos...</p>
                                </div>
                                <button type="button" class="btn-ver-mas" onclick="abrirModal('Alcance del CCL', `
                                    <p>El <strong>Comité de Convivencia Laboral</strong> tiene por objeto contribuir con mecanismos alternativos a los establecidos en los demás reglamentos de ETIB S.A.S a la prevención y solución de las situaciones causadas por conductas de presunto acoso laboral de los trabajadores al interior de la empresa.</p>

                                `)">Ver Más</button>
                            </div>
                        </div>

                        <!-- Designación -->
                        <div class="copasst-card">
                            <div class="copasst-card-header">
                                <h3>Designación</h3>
                            </div>
                            <div class="copasst-card-body">
                                <div class="content-preview">
                                    <p>Este Comité estará integrado en forma bipartita por cinco titulares (5) con sus respectivos suplentes...</p>
                                </div>
                                <button type="button" class="btn-ver-mas" onclick="abrirModal('Designación del CCL', `
                                    <p>Este Comité estará integrado en forma <strong>bipartita</strong> por cinco titulares (5) con sus respectivos suplentes; Los representantes de los Trabajadores tres (3) y los representantes del empleador dos (2), los representantes fueron designados de la siguiente forma:</p>
                                    
                                    <h3>Representantes de los trabajadores:</h3>
                                    <p>Por <strong>votación abierta</strong>, mediante elección popular. Que se realizó el <strong>01 de Abril de 2016</strong> y de cual quedaron los siguientes registros de votación acta de escrutinio y de votación.</p>
                                    <p>Los representante del empleador, fueron <strong>delegados por el Gerente</strong>.</p>
                                `)">Ver Más</button>
                            </div>
                        </div>

                        <!-- Representantes del CCL -->
                        <div class="copasst-card">
                            <div class="copasst-card-header">
                                <h3>Representantes del CCL</h3>
                            </div>
                            <div class="copasst-card-body">
                                <div class="content-preview">
                                    <h4>Por parte de gerencia:</h4>
                                    <div class="integrantes-preview">
                                        <div class="integrante-mini">
                                            <img src="../Img/Comites/CCL/Principal1-1.png" alt="Guillermo Leal">
                                            <h5>Guillermo Leal</h5>
                                            <h6>Jefe de Operaciones</h6>
                                            <p>Auto Sur</p>
                                        </div>
                                        <div class="integrante-mini">
                                            <img src="../Img/Comites/CCL/Principal1-2.png" alt="Yesid Villamizar">
                                            <h5>Yesid Villamizar</h5>
                                            <h6>Líder de Asuntos Laborales</h6>
                                            <p>Auto Sur</p>
                                        </div>
                                    </div>
                                    <p>+ 6 representantes más entre gerencia y trabajadores...</p>
                                </div>
                                <button type="button" class="btn-ver-mas" onclick="abrirModalRepresentantes()">Ver Todos los Representantes</button>
                            </div>
                        </div>
                        
                    </div>
                </div>
            </div>
        </div>
    </section>

    <!-- Modal para contenido completo -->
    <div id="modalCCL" class="modal-copasst">
        <div class="modal-copasst-content">
            <div class="modal-copasst-header">
                <h3 id="modalTitle" class="modal-copasst-title"></h3>
                <button type="button" class="modal-copasst-close" onclick="cerrarModal()">&times;</button>
            </div>
            <div id="modalBody" class="modal-copasst-body"></div>
        </div>
    </div>

    <script src="../../Styles/bootstrap/bootstrap.bundle.min.js"></script>
    
    <script>
        // Función para abrir modal con contenido
        function abrirModal(titulo, contenido) {
            document.getElementById('modalTitle').innerHTML = titulo;
            document.getElementById('modalBody').innerHTML = contenido;
            document.getElementById('modalCCL').classList.add('show');
            document.body.style.overflow = 'hidden';
        }

        // Función para mostrar todos los representantes en modal
        function abrirModalRepresentantes() {
            const contenido = `
                <h3>Por parte de gerencia:</h3>
                <div class="integrantes-grid">
                    <div class="integrante-card">
                        <img src="../Img/Comites/CCL/Principal1-1.png" alt="Guillermo Leal">
                        <h5>Guillermo Leal</h5>
                        <h6>Jefe de Operaciones</h6>
                        <p>Auto Sur</p>
                    </div>
                    <div class="integrante-card">
                        <img src="../Img/Comites/CCL/Principal1-2.png" alt="Yesid Villamizar">
                        <h5>Yesid Villamizar</h5>
                        <h6>Líder de Asuntos Laborales</h6>
                        <p>Auto Sur</p>
                    </div>
                    <div class="integrante-card">
                        <img src="../Img/Comites/CCL/Principal1-3.png" alt="Carmen Salamanca">
                        <h5>Carmen Salamanca</h5>
                        <h6>Jefe de Selección y Contratación</h6>
                        <p>Auto Sur</p>
                    </div>
                    <div class="integrante-card">
                        <img src="../Img/Comites/CCL/Principal1-4.png" alt="William Beltrán">
                        <h5>William Beltrán</h5>
                        <h6>Jefe de Nómina</h6>
                        <p>Auto Sur</p>
                    </div>
                </div>
                
                <h3>Por parte de los trabajadores:</h3>
                <div class="integrantes-grid">
                    <div class="integrante-card">
                        <img src="../Img/Comites/CCL/Principal2-3.png" alt="Edward Alonso">
                        <h5>Edward Alonso</h5>
                        <h6>Operador Zonal</h6>
                        <p>San Jose 1</p>
                    </div>
                    <div class="integrante-card">
                        <img src="../Img/Comites/CCL/Principal2-1.png" alt="Alejandro Tibata">
                        <h5>Alejandro Tibata</h5>
                        <h6>Operador Zonal</h6>
                        <p>San Jose 1</p>
                    </div>
                    <div class="integrante-card">
                        <img src="../Img/Comites/CCL/Principal2-4.png" alt="Jhonnatan Alberto Ortegón Manjarres">
                        <h5>Jhonnatan Alberto Ortegón Manjarres</h5>
                        <h6>Operador de Nueva Tecnología</h6>
                        <p>San Jose 1</p>
                    </div>
                    <div class="integrante-card">
                        <img src="../Img/Comites/CCL/Principal2-2.png" alt="Johana García">
                        <h5>Johana García</h5>
                        <h6>Auxiliar de Novedades</h6>
                        <p>Sevillana</p>
                    </div>
                </div>
            `;
            abrirModal('Representantes del CCL', contenido);
        }

        // Función para cerrar modal
        function cerrarModal() {
            document.getElementById('modalCCL').classList.remove('show');
            document.body.style.overflow = 'auto';
        }

        // Cerrar modal al hacer click fuera del contenido
        document.getElementById('modalCCL').addEventListener('click', function (e) {
            if (e.target === this) {
                cerrarModal();
            }
        });

        // Cerrar modal con tecla ESC
        document.addEventListener('keydown', function (e) {
            if (e.key === 'Escape' && document.getElementById('modalCCL').classList.contains('show')) {
                cerrarModal();
            }
        });

        function quitarPadding() {
            let doc = document.getElementById('container');
            if (doc) {
                doc.removeAttribute('style');
            }
        }
        document.addEventListener('DOMContentLoaded', quitarPadding);
        window.addEventListener('load', quitarPadding);
    </script>
</asp:Content>