<%@ Page ValidateRequest="false" Title="CSV" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="V_CSV.aspx.cs" Inherits="Intranet_3._0.Vistas.V_Comites.V_CSV" %>

<asp:Content ID="Content1" ContentPlaceHolderID="scripts_css" runat="server">
    <link href="../../Styles/bootstrap/bootstrap.min.css" rel="stylesheet" />
    <link rel="Stylesheet" href="/Styles/css/default_encuestas/default_encuestas.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="Stylesheet" href="/Styles/css/copasst/copasst.css" />
    <br />
    <div class="pnl_tag">
        <p><i class="fa fa-tag"></i>¡Conozca el CSV!</p>
    </div>
    
    <section>
        <div class="container-fluid">
            <div class="row">
                <div class="col-12">
                    <div class="copasst-grid">
                        
                        <!-- ¿Qué es el CSV? -->
                        <div class="copasst-card">
                            <div class="copasst-card-header">
                                <h3>¿Qué es el CSV?</h3>
                            </div>
                            <div class="copasst-card-body">
                                <div class="content-preview">
                                    <p>El Comité de Seguridad Vial (CSV) es el conjunto de personas que apoyan el diseño, implementación, seguimiento y mejora del Plan Estratégico de Seguridad Vial...</p>
                                </div>
                                <button type="button" class="btn-ver-mas" onclick="abrirModal('¿Qué es el CSV?', `
                                    <p>El <strong>Comité de Seguridad Vial (CSV)</strong> es el conjunto de personas que apoyan el diseño, implementación, seguimiento y mejora de Plan Estratégico de Seguridad Vial, influenciando y promoviendo en la organización la formación de hábitos y comportamientos seguros en vía.</p>
                                `)">Ver Más</button>
                            </div>
                        </div>

                        <!-- Objetivo del CSV -->
                        <div class="copasst-card">
                            <div class="copasst-card-header">
                                <h3>Objetivo del CSV</h3>
                            </div>
                            <div class="copasst-card-body">
                                <div class="content-preview">
                                    <p>Establecer los principales aspectos que se deben tener en cuenta en la conformación y funcionamiento del comité de seguridad vial...</p>
                                </div>
                                <button type="button" class="btn-ver-mas" onclick="abrirModal('Objetivo del CSV', `
                                    <p>Establecer los principales aspectos que se deben tener en cuenta en la conformación y funcionamiento del comité de seguridad vial, definiendo las responsabilidades, competencias y demás acciones en las cuales deben participar los miembros activos y partes interesadas de la organización.</p>
                                `)">Ver Más</button>
                            </div>
                        </div>

                        <!-- Alcance del CSV -->
                        <div class="copasst-card">
                            <div class="copasst-card-header">
                                <h3>Alcance del CSV</h3>
                            </div>
                            <div class="copasst-card-body">
                                <div class="content-preview">
                                    <p>Aplica para planificación, ejecución y verificación de las actividades desarrolladas por ETIB S.A.S., en materia de seguridad vial...</p>
                                </div>
                                <button type="button" class="btn-ver-mas" onclick="abrirModal('Alcance del CSV', `
                                    <p>Aplica para planificación, ejecución y verificación de las actividades desarrolladas por <strong>ETIB S.A.S.</strong>, en materia de seguridad vial durante la ejecución de sus funciones misionales, estratégicas y de apoyo, de acuerdo a los requisitos legales vigentes aplicables al sector.</p>
                                `)">Ver Más</button>
                            </div>
                        </div>

                        <!-- Estructura y Miembros del CSV -->
                        <div class="copasst-card">
                            <div class="copasst-card-header">
                                <h3>Estructura y Miembros del CSV</h3>
                            </div>
                            <div class="copasst-card-body">
                                <div class="content-preview">
                                    <p>El Comité de Seguridad Vial estará conformado por 11 integrantes, dentro de los cuales se designarán las responsabilidades de Presidente y Secretario...</p>
                                </div>
                                <button type="button" class="btn-ver-mas" onclick="abrirModal('Estructura y Miembros del CSV', `
                                    <p>El <strong>Comité de Seguridad Vial</strong> estará conformado por <strong>11 integrantes</strong>, dentro de los cuales se designarán las responsabilidades de Presidente y Secretario, los cuales deben ser nombrados durante la primera sesión del comité y de presentarse algún cambio, este debe quedar notificado mediante el acta de su respectivo comité.</p>
                                    <p>La selección de los integrantes del comité de seguridad vial, se realiza considerando los procesos y colaboradores que intervienen de forma directa en la operación, con el fin de influenciar la mejora en términos de seguridad vial.</p>
                                `)">Ver Más</button>
                            </div>
                        </div>

                        <!-- Integrantes del CSV -->
                        <div class="copasst-card">
                            <div class="copasst-card-header">
                                <h3>Integrantes del CSV</h3>
                            </div>
                            <div class="copasst-card-body">
                                <div class="content-preview">
                                    <p>El comité está conformado por 11 integrantes distribuidos en diferentes roles y responsabilidades dentro de la organización...</p>
                                    <div class="integrantes-preview">
                                        <div class="integrante-mini">
                                            <img src="../Img/Comites/CSV/PRINCIPAL 1 - copia.jpg" alt="Integrante Principal CSV">
                                            <h5>Presidente CSV</h5>
                                            <h6>Líder del Comité</h6>
                                            <p>Representante Principal</p>
                                        </div>
                                        <div class="integrante-mini">
                                            <img src="../Img/Comites/CSV/PRINCIPAL 2 - copia.jpg" alt="Integrante CSV">
                                            <h5>Miembro Activo</h5>
                                            <h6>Integrante</h6>
                                            <p>Área Operativa</p>
                                        </div>
                                    </div>
                                    <p>+ 9 integrantes más del comité...</p>
                                </div>
                                <button type="button" class="btn-ver-mas" onclick="abrirModalIntegrantes()">Ver Todos los Integrantes</button>
                            </div>
                        </div>
                        
                    </div>
                </div>
            </div>
        </div>
    </section>

    <!-- Modal para contenido completo -->
    <div id="modalCSV" class="modal-copasst">
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
            document.getElementById('modalCSV').classList.add('show');
            document.body.style.overflow = 'hidden';
        }

        // Función para mostrar todos los integrantes en modal
        function abrirModalIntegrantes() {
            const contenido = `
                <h3>Integrantes del Comité de Seguridad Vial</h3>
                
                <h4>Presidente del Comité</h4>
                <div class="integrantes-grid">
                    <div class="integrante-card">
                        <img src="../Img/Comites/CSV/PRINCIPAL 1 - copia.jpg" alt="Presidente CSV">
                        <h5>Presidente CSV</h5>
                        <h6>Líder del Comité</h6>
                        <p>Representante Principal</p>
                    </div>
                </div>
                
                <h4>Miembros del Comité</h4>
                <div class="integrantes-grid">
                    <div class="integrante-card">
                        <img src="../Img/Comites/CSV/PRINCIPAL 2 - copia.jpg" alt="Integrante CSV">
                        <h5>Integrante CSV</h5>
                        <h6>Miembro Activo</h6>
                        <p>Área Operativa</p>
                    </div>
                    <div class="integrante-card">
                        <img src="../Img/Comites/CSV/PRINCIPAL 3 - copia.jpg" alt="Integrante CSV">
                        <h5>Integrante CSV</h5>
                        <h6>Miembro Activo</h6>
                        <p>Área Técnica</p>
                    </div>
                    <div class="integrante-card">
                        <img src="../Img/Comites/CSV/PRINCIPAL 4 - copia.jpg" alt="Integrante CSV">
                        <h5>Integrante CSV</h5>
                        <h6>Miembro Activo</h6>
                        <p>Área de Seguridad</p>
                    </div>
                    <div class="integrante-card">
                        <img src="../Img/Comites/CSV/PRINCIPAL 5.jpg" alt="Integrante CSV">
                        <h5>Integrante CSV</h5>
                        <h6>Miembro Activo</h6>
                        <p>Área de Control</p>
                    </div>
                    <div class="integrante-card">
                        <img src="../Img/Comites/CSV/PRINCIPAL 6 - copia.jpg" alt="Integrante CSV">
                        <h5>Integrante CSV</h5>
                        <h6>Miembro Activo</h6>
                        <p>Área Administrativa</p>
                    </div>
                    <div class="integrante-card">
                        <img src="../Img/Comites/CSV/PRINCIPAL 7 - copia.jpg" alt="Integrante CSV">
                        <h5>Integrante CSV</h5>
                        <h6>Miembro Activo</h6>
                        <p>Área de Transporte</p>
                    </div>
                    <div class="integrante-card">
                        <img src="../Img/Comites/CSV/PRINCIPAL 8 - copia.jpg" alt="Integrante CSV">
                        <h5>Integrante CSV</h5>
                        <h6>Miembro Activo</h6>
                        <p>Área de Mantenimiento</p>
                    </div>
                    <div class="integrante-card">
                        <img src="../Img/Comites/CSV/PRINCIPAL 9.jpg" alt="Integrante CSV">
                        <h5>Integrante CSV</h5>
                        <h6>Miembro Activo</h6>
                        <p>Área de Capacitación</p>
                    </div>
                    <div class="integrante-card">
                        <img src="../Img/Comites/CSV/principal 11.jpg" alt="Integrante CSV">
                        <h5>Integrante CSV</h5>
                        <h6>Miembro Activo</h6>
                        <p>Área de Supervisión</p>
                    </div>
                    <div class="integrante-card">
                        <img src="../Img/Comites/CSV/principal 12.jpg" alt="Integrante CSV">
                        <h5>Secretario CSV</h5>
                        <h6>Secretario del Comité</h6>
                        <p>Documentación y Actas</p>
                    </div>
                </div>
            `;
            abrirModal('Integrantes del CSV', contenido);
        }

        // Función para cerrar modal
        function cerrarModal() {
            document.getElementById('modalCSV').classList.remove('show');
            document.body.style.overflow = 'auto';
        }

        // Cerrar modal al hacer click fuera del contenido
        document.getElementById('modalCSV').addEventListener('click', function (e) {
            if (e.target === this) {
                cerrarModal();
            }
        });

        // Cerrar modal con tecla ESC
        document.addEventListener('keydown', function (e) {
            if (e.key === 'Escape' && document.getElementById('modalCSV').classList.contains('show')) {
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