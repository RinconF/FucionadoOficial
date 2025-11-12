<%@ Page ValidateRequest="false" Title="COPASST" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="V_Copasst.aspx.cs" Inherits="Intranet_3._0.Vistas.V_Comites.V_Copasst" %>

<asp:Content ID="Content1" ContentPlaceHolderID="scripts_css" runat="server">
    <link href="../../Styles/bootstrap/bootstrap.min.css" rel="stylesheet" />
    <link rel="Stylesheet" href="/Styles/css/default_encuestas/default_encuestas.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="Stylesheet" href="/Styles/css/copasst/copasst.css" />
    <br />
    <div class="pnl_tag">
        <p><i class="fas fa-tag"></i>¡Conozca el COPASST!</p>
    </div>
    
    <section>
        <div class="container-fluid">
            <div class="row">
                <div class="col-12">
                    <div class="copasst-grid">
                        
                        <!-- ¿Qué es el COPASST? -->
                        <div class="copasst-card">
                            <div class="copasst-card-header">
                                <h3>¿Qué es el COPASST?</h3>
                            </div>
                            <div class="copasst-card-body">
                                <div class="content-preview">
                                    <p>Comité Paritario de Seguridad y Salud en el Trabajo.</p>
                                    <p>Es el Comité Paritario de Seguridad y Salud en el Trabajo</p>
                                </div>
                                <button type="button" class="btn-ver-mas" onclick="abrirModal('¿Qué es el COPASST?', `
                                    <p>Es el <strong>Comité Paritario de Seguridad y Salud en el Trabajo.</strong></p>
                                `)">Ver Más</button>
                            </div>
                        </div>

                        <!-- Objetivo del COPASST -->
                        <div class="copasst-card">
                            <div class="copasst-card-header">
                                <h3>Objetivo del COPASST</h3>
                            </div>
                            <div class="copasst-card-body">
                                <div class="content-preview">
                                    <p>Promover y vigilar el cumplimiento de las normas y reglamentos de salud y seguridad dentro de la Empresa, sin ocuparse de...</p>
                                </div>
                                <button type="button" class="btn-ver-mas" onclick="abrirModal('Objetivo del COPASST', `
                                    <p><strong>Objetivo Principal:</strong></p>
                                    <p>Promover y vigilar el cumplimiento de las normas y reglamentos de salud y seguridad dentro de la Empresa, sin ocuparse de tramitar asuntos referentes a la relación contractual-laboral propiamente dicha, inconvenientes de personal, disciplinarios o sindicales; estos deben ventilarse ante otros organismos y por lo tanto están sujetos a reglamentaciones diferentes.</p>
                                `)">Ver Más</button>
                            </div>
                        </div>

                        <!-- Responsabilidades del COPASST -->
                        <div class="copasst-card">
                            <div class="copasst-card-header">
                                <h3>Responsabilidades del COPASST</h3>
                            </div>
                            <div class="copasst-card-body">
                                <div class="content-preview">
                                    <h4>Responsabilidades principales:</h4>
                                    <ul>
                                        <li>Proponer medidas para mantener la salud en los lugares de trabajo</li>
                                        <li>Participar en actividades de capacitación</li>
                                        <li>Colaborar con entidades gubernamentales...</li>
                                    </ul>
                                </div>
                                <button type="button" class="btn-ver-mas" onclick="abrirModalResponsabilidades()">Ver Más</button>
                            </div>
                        </div>

                        <!-- Integrantes del COPASST -->
                        <div class="copasst-card">
                            <div class="copasst-card-header">
                                <h3>Integrantes del COPASST</h3>
                            </div>
                            <div class="copasst-card-body">
                                <div class="content-preview">
                                    <h4>Por parte de la empresa:</h4>
                                    <div class="integrantes-preview">
                                        <div class="integrante-mini">
                                            <img src="../Img/Comites/Copasst/Principal-1-1-JulianaAcosta.png" alt="Juliana Andrea Acosta Castellanos">
                                            <h5>Juliana Andrea Acosta</h5>
                                            <h6>Presidente</h6>
                                            <p>Auto Sur</p>
                                        </div>
                                        <div class="integrante-mini">
                                            <img src="../Img/Comites/Copasst/Principal-1-2-CarlosUrrego.png" alt="Carlos Alberto Urrego Sierra">
                                            <h5>Carlos Alberto Urrego</h5>
                                            <h6>Integrante</h6>
                                            <p>Auto Sur</p>
                                        </div>
                                    </div>
                                    <p>+ 14 integrantes más entre principales y suplentes...</p>
                                </div>
                                <button type="button" class="btn-ver-mas" onclick="abrirModalIntegrantes()">Ver Todos los Integrantes</button>
                            </div>
                        </div>

                        <!-- Funciones del Presidente -->
                        <div class="copasst-card">
                            <div class="copasst-card-header">
                                <h3>Funciones del Presidente</h3>
                            </div>
                            <div class="copasst-card-body">
                                <div class="content-preview">
                                    <ul>
                                        <li>Presidir y orientar las reuniones del comité</li>
                                        <li>Organizar el sitio de las reuniones</li>
                                        <li>Preparar los temas de cada reunión...</li>
                                    </ul>
                                </div>
                                <button type="button" class="btn-ver-mas" onclick="abrirModal('Funciones del Presidente del COPASST', `
                                    <h3>Según el artículo 12 de la Resolución 2013 de 1986</h3>
                                    <ul>
                                        <li>Presidir y orientar las reuniones del comité.</li>
                                        <li>Llevar a cabo los arreglos necesarios para determinar el sitio de las reuniones.</li>
                                        <li>Preparar los temas de cada reunión.</li>
                                        <li>Tramitar ante la dirección de la empresa las recomendaciones aprobadas en el seno del comité.</li>
                                    </ul>
                                `)">Ver Más</button>
                            </div>
                        </div>

                        <!-- Funciones del Secretario -->
                        <div class="copasst-card">
                            <div class="copasst-card-header">
                                <h3>Funciones del Secretario</h3>
                            </div>
                            <div class="copasst-card-body">
                                <div class="content-preview">
                                    <ul>
                                        <li>Verificar la asistencia a las reuniones</li>
                                        <li>Tomar nota de todos los temas tratados</li>
                                        <li>Elaborar el acta de cada reunión...</li>
                                    </ul>
                                </div>
                                <button type="button" class="btn-ver-mas" onclick="abrirModal('Funciones del Secretario del COPASST', `
                                    <h3>Según el artículo 12 de la Resolución 2013 de 1986</h3>
                                    <ul>
                                        <li>Verificar la asistencia a las reuniones programadas.</li>
                                        <li>Tomar atenta nota de todos los temas tratados en cada reunión.</li>
                                        <li>Elaborar el acta de cada reunión y someterla a votación.</li>
                                        <li>Llevar el archivo referente a las actividades desarrolladas por el comité.</li>
                                    </ul>
                                `)">Ver Más</button>
                            </div>
                        </div>
                        
                    </div>
                </div>
            </div>
        </div>
    </section>

    <!-- Modal para contenido completo -->
    <div id="modalCopasst" class="modal-copasst">
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
            document.getElementById('modalCopasst').classList.add('show');
            document.body.style.overflow = 'hidden';
        }

        // Función específica para responsabilidades (contenido largo)
        function abrirModalResponsabilidades() {
            const contenido = `
                <h3>Responsabilidades establecidas en el artículo 11 de la Resolución 2013 de 1986</h3>
                <ul>
                    <li>Proponer a la administración de la empresa o establecimiento de trabajo la adopción de medidas en el desarrollo de actividades que procuren y mantengan la salud en los lugares y ambientes de trabajo.</li>
                    <li>Proponer y participar en actividades de capacitación en salud ocupacional dirigidas a trabajadores, supervisores y directivos de la empresa o establecimiento de trabajo.</li>
                    <li>Colaborar con los funcionarios de entidades gubernamentales de salud ocupacional en las actividades que éstos adelanten en la empresa y recibir por derecho propio los informes correspondientes.</li>
                    <li>Vigilar el desarrollo de las actividades que en materia de medicina, higiene y seguridad industrial debe realizar la empresa de acuerdo con el Reglamento de Higiente y Seguridad Industrial y las normas vigentes, promover su divulgación y observancia.</li>
                    <li>Colaborar con el análisis de las causas de los accidentes de trabajo y enfermedades profesionales y proponer al empleador las medidas correctivas a que haya lugar para evitar su ocurrencia. Evaluar los programas que se hayan realizado.</li>
                    <li>Visitar periódicamente los lugares de trabajo e inspeccionar los ambientes, máquinas, equipos, aparatos y las operaciones realizadas por el personal de trabajadores en cada área o sección de la empresa e informar al empleador sobre la existencia de factores de riesgo y sugerir las medidas correctivas y de control.</li>
                    <li>Estudiar y considerar las sugerencias que presenten los trabajadores, en materia de medicina, higiente y seguridad industrial.</li>
                    <li>Servir como organismo de coordinación entre empleador y los trabajadores en la solución de los problemas relativos a la salud ocupacional. Tramitar los reclamos de los trabajadores relacionados con la salud ocupacional.</li>
                    <li>Solicitar periódicamente a la empresa informes sobre accidentalidad y enfermedades profesionales con el objeto de dar cumplimiento a lo estipulado en la presente resolución.</li>
                    <li>Elegir el secretario del Comité.</li>
                    <li>Mantener un archivo de las actas de cada reunión y demás actividades que se desarrollen, el cual estará en cualquier momento a disposición del empleador, trabajadores y las autoridades competentes.</li>
                    <li>Las demás funciones que le señalen las normas sobre salud ocupacional.</li>
                </ul>
                
                <h3>Responsabilidades establecidas en el Decreto 1072 de 2015</h3>
                <ul>
                    <li>Recibir, por parte del empleador, la comunicación de la política del Sistema de Gestión de Seguridad de la Salud en el Trabajo (artículo 2.2.4.6.5).</li>
                    <li>Recibir, por parte del empleador, información sobre el desarrollo de todas las etapas del sistema de Gestión de Seguridad de la Salud en el Trabajo (artículo 2.2.4.6.8).</li>
                    <li>Rendir cuentas internamente en relación con su desempeño (artículo 2.2.4.6.8).</li>
                    <li>Dar recomendaciones para el mejoramiento del Sistema de Gestión de Seguridad de la Salud en el Trabajo (2.2.4.6.8).</li>
                    <li>Participar en las capacitaciones que realice la Administradora de Riesgos Laborales (artículo 2.2.4.6.9).</li>
                    <li>Revisar mínimo una (1) vez al año, en conjunto con el empleador, el programa de capacitación en Seguridad y Salud en el Trabajo (2.2.4.6.11).</li>
                    <li>Recibir los resultados de las evaluaciones de los ambientes de trabajo y emitir recomendaciones (artículo 2.2.4.6.15).</li>
                    <li>Apoyar la adopción de las medidas de prevención y control derivadas de la gestión del cambio (artículo 2.2.4.6.26).</li>
                    <li>Participar en la planificación de las auditorias al sistema de Gestión de Seguridad de la Salud en el Trabajo (2.2.4.6.29).</li>
                    <li>Recibir los resultados de la revisión por la dirección del Sistema de Gestión de Seguridad de la Salud en el Trabajo (2.2.4.6.31).</li>
                    <li>Formar parte del equipo investigador de incidentes, accidentes de trabajo y enfermedades laborales (artículos 2.2.4.1.6 y 2.2.4.6.32).</li>
                </ul>
            `;
            abrirModal('Responsabilidades del COPASST', contenido);
        }

        // Función para mostrar integrantes en modal
        function abrirModalIntegrantes() {
            const contenido = `
                <h3>Por parte de la empresa:</h3>
                <h4>Principales</h4>
                <div class="integrantes-grid">
                    <div class="integrante-card">
                        <img src="../Img/Comites/Copasst/Principal-1-1-JulianaAcosta.png" alt="Juliana Andrea Acosta Castellanos">
                        <h5>Juliana Andrea Acosta Castellanos</h5>
                        <h6>Presidente COPASST</h6>
                        <p>Auto Sur</p>
                    </div>
                    <div class="integrante-card">
                        <img src="../Img/Comites/Copasst/Principal-1-2-CarlosUrrego.png" alt="Carlos Alberto Urrego Sierra">
                        <h5>Carlos Urrego</h5>
                        <h6>Integrante</h6>
                        <p>Auto Sur</p>
                    </div>
                    <div class="integrante-card">
                        <img src="../Img/Comites/Copasst/Principal-1-3-CarlosRubio.png" alt="Carlos Mauricio Rubio Vargas">
                        <h5>Carlos Rubio Vargas</h5>
                        <h6>Integrante</h6>
                        <p>Auto Sur</p>
                    </div>
                    <div class="integrante-card">
                        <img src="../Img/Comites/Copasst/Principal-1-4-LuisDevia.png" alt="Luis Enrique Devia Buitrago">
                        <h5>Luis Enrique Devia Buitrago</h5>
                        <h6>Integrante</h6>
                        <p>Auto Sur</p>
                    </div>
                </div>
                
                <h4>Suplentes</h4>
                <div class="integrantes-grid">
                    <div class="integrante-card">
                        <img src="../Img/Comites/Copasst/Suplente-1-1-MarcelLancheros.png" alt="Marcel Eduardo Lancheros Guerrero">
                        <h5>Marcel Eduardo Lancheros Guerrero</h5>
                        <h6>Integrante</h6>
                        <p>Auto Sur</p>
                    </div>
                    <div class="integrante-card">
                        <img src="../Img/Comites/Copasst/Suplente-1-2-CarlosEduardoRivera.png" alt="Carlos Eduardo Rivera Ruiz">
                        <h5>Carlos Eduardo Rivera Ruiz</h5>
                        <h6>Integrante</h6>
                        <p>Auto Sur</p>
                    </div>
                    <div class="integrante-card">
                        <img src="../Img/Comites/Copasst/Suplente-1-3-JorgeEstupiñan.png" alt="Jorge Andrés Estupiñán Cuy">
                        <h5>Jorge Andrés Estupiñán Cuy</h5>
                        <h6>Integrante</h6>
                        <p>San Jose II</p>
                    </div>
                    <div class="integrante-card">
                        <img src="../Img/Comites/Copasst/Suplente-1-4-ErikaSerna.png" alt="Erika Andrea Serna Castro">
                        <h5>Erika Andrea Serna Castro</h5>
                        <h6>Integrante</h6>
                        <p>Auto Sur</p>
                    </div>
                </div>
                
                <h3>Por parte de los trabajadores:</h3>
                <h4>Principales</h4>
                <div class="integrantes-grid">
                    <div class="integrante-card">
                        <img src="../Img/Comites/Copasst/Principal-2-1-DaniaArias.png" alt="Dania Geraldine Arias García">
                        <h5>Dania Geraldine Arias García</h5>
                        <h6>Secretario COPASST</h6>
                        <p>Auto Sur</p>
                    </div>
                    <div class="integrante-card">
                        <img src="../Img/Comites/Copasst/Principal-2-2-JuanCarlosHerrera.png" alt="Juan Carlos Herrera Fajardo">
                        <h5>Juan Carlos Herrera Fajardo</h5>
                        <h6>Integrante</h6>
                        <p>San Jose I</p>
                    </div>
                    <div class="integrante-card">
                        <img src="../Img/Comites/Copasst/Principal-2-3-CamiloMoreno.png" alt="Camilo Andrés Moreno Mora">
                        <h5>Camilo Andrés Moreno Mora</h5>
                        <h6>Integrante</h6>
                        <p>San Jose I</p>
                    </div>
                    <div class="integrante-card">
                        <img src="../Img/Comites/Copasst/Principal-2-4-GamanielCampos.png" alt="Gamaniel Campos Ospina">
                        <h5>Gamaniel Campos Ospina</h5>
                        <h6>Integrante</h6>
                        <p>Alimentadores</p>
                    </div>
                </div>
                
                <h4>Suplentes</h4>
                <div class="integrantes-grid">
                    <div class="integrante-card">
                        <img src="../Img/Comites/Copasst/Suplente-2-1-CarlosPulido.png" alt="Carlos Alberto Pulido Mancipe">
                        <h5>Carlos Alberto Pulido Mancipe</h5>
                        <h6>Integrante</h6>
                        <p>Alimentadores</p>
                    </div>
                    <div class="integrante-card">
                        <img src="../Img/Comites/Copasst/Suplente-2-2-JhonnatanOrtegón-MesaTrabajo-1.png" alt="Jhonnatan Alberto Ortegón Manjarres">
                        <h5>Jhonnatan Alberto Ortegón Manjarres</h5>
                        <h6>Integrante</h6>
                        <p>San Jose I</p>
                    </div>
                    <div class="integrante-card">
                        <img src="../Img/Comites/Copasst/Suplente-2-3-MarthaRamirez.png" alt="Martha Patricia Ramírez Bobadilla">
                        <h5>Martha Patricia Ramírez Bobadilla</h5>
                        <h6>Integrante</h6>
                        <p>Auto Sur</p>
                    </div>
                    <div class="integrante-card">
                        <img src="../Img/Comites/Copasst/Suplente-2-4-AlejandroTibata.png" alt="Alejandro Tibata Tocarruncho">
                        <h5>Alejandro Tibata Tocarruncho</h5>
                        <h6>Integrante</h6>
                        <p>San Jose I</p>
                    </div>
                </div>
            `;
            abrirModal('Integrantes del COPASST', contenido);
        }

        // Función para cerrar modal
        function cerrarModal() {
            document.getElementById('modalCopasst').classList.remove('show');
            document.body.style.overflow = 'auto';
        }

        // Cerrar modal al hacer click fuera del contenido
        document.getElementById('modalCopasst').addEventListener('click', function (e) {
            if (e.target === this) {
                cerrarModal();
            }
        });

        // Cerrar modal con tecla ESC
        document.addEventListener('keydown', function (e) {
            if (e.key === 'Escape' && document.getElementById('modalCopasst').classList.contains('show')) {
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