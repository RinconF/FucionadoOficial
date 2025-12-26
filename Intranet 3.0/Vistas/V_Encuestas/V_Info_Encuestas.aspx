<%@ Page Title="Informes Encuestas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="V_Info_Encuestas.aspx.cs" Inherits="Intranet_3._0.Vistas.V_Encuestas.V_Info_Encuestas" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="Stylesheet" href="/Styles/css/default_encuestas/default_encuestas.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css">

    <div class="banner-container">
        <div class="pnl_tag">
            <p><i class="fas fa-chart-pie"></i>Informes Encuestas</p>
        </div>
    </div>

    <div class="ContainerEn">

        <h3 class="page-title-encuesta"><%= NombreEncuesta %></h3>

        <asp:Label ID="lblMensaje" runat="server" CssClass="mensaje" Visible="false" />

        <asp:Panel ID="encuestaPanel" runat="server" Visible="false" CssClass="panel-encuesta">
            
            <div class="dashboard-grid-encuesta">
                
                <!-- CARD PRINCIPAL DEL GRÁFICO -->
                <div class="chart-card-encuesta">
                    <div class="chart-header-encuesta">
                        <h4>Estado de Encuestas</h4>
                        <p>Resumen actualizado de encuestas realizadas y pendientes</p>
                    </div>

                    <!-- GRÁFICO DE TORTA -->
                    <div class="chart-wrapper">
                        <canvas id="pieChart" width="300" height="300"></canvas>
                    </div>

                    <!-- RESUMEN DEL GRÁFICO -->
                    <div id="legend" class="chart-legend">
                        <div class="legend-items">
                            <div class="legend-item-wrapper">
                                <span class="legend-dot legend-finalizadas"></span>
                                <span class="legend-label">Finalizadas: <strong id="finalizadasCount"></strong></span>
                            </div>
                            <div class="legend-item-wrapper">
                                <span class="legend-dot legend-pendientes"></span>
                                <span class="legend-label">Pendientes: <strong id="pendientesCount"></strong></span>
                            </div>
                        </div>
                    </div>
                </div>

                <!-- COLUMNA DE ESTADÍSTICAS -->
                <div class="stats-column-encuesta">
                    
                    <!-- CARD FINALIZADAS -->
                    <div class="stat-card-encuesta stat-success">
                        <div class="stat-icon">
                            <i class="fas fa-check-circle"></i>
                        </div>
                        <div class="stat-content">
                            <h4 id="statFinalizadas"><%= TotalFinalizadas %></h4>
                            <p>Encuestas Finalizadas</p>
                        </div>
                    </div>

                    <!-- CARD PENDIENTES -->
                    <div class="stat-card-encuesta stat-danger">
                        <div class="stat-icon">
                            <i class="fas fa-clock"></i>
                        </div>
                        <div class="stat-content">
                            <h4 id="statPendientes"><%= TotalPendientes %></h4>
                            <p>Encuestas Pendientes</p>
                        </div>
                    </div>

                    <!-- CARD PORCENTAJE -->
                    <div class="stat-card-encuesta stat-warning">
                        <div class="stat-icon">
                            <i class="fas fa-percentage"></i>
                        </div>
                        <div class="stat-content">
                            <h4 id="statPorcentaje">0%</h4>
                            <p>Tasa de Completitud</p>
                        </div>
                    </div>

                </div>
            </div>

            <!-- CARD DE ACCIONES -->
            <div class="actions-card-encuesta">
                <h4 class="actions-title">Acciones Disponibles</h4>
                
                <div class="actions-controls">
                    <div class="dropdown-wrapper">
                        <asp:DropDownList ID="ddlTipoInformeEnc" runat="server" CssClass="form-select-encuesta">
                            <asp:ListItem Text="Seleccione informe..." Value="" Selected="True" />
                            <asp:ListItem Text="Informe General" Value="general" />
                            <asp:ListItem Text="Informe Por Sede" Value="sede" />
                            <asp:ListItem Text="Informe Por Cargo" Value="cargo" />
                            <asp:ListItem Text="Informe De Respuestas" Value="respuestas" />
                        </asp:DropDownList>
                    </div>

                    <div class="buttons-wrapper">
                        <!-- BOTÓN DESCARGAR -->
                        <asp:Button ID="btnDescargarExcel" runat="server" 
                            Text="Descargar Informe"
                            CssClass="btn-encuesta btn-download"
                            OnClick="btnDescargarExcel_Click"
                            OnClientClick="return confirmarDescargaInforme();" />

                        <!-- BOTÓN REFRESCAR -->
                        <button type="button" class="btn-encuesta btn-refresh" onclick="refreshPage()">
                            <i class="fas fa-sync-alt"></i> <span class="btn-text">Refrescar</span>
                        </button>
                    </div>
                </div>
            </div>

        </asp:Panel>
    </div>

    <script type="text/javascript">
        function refreshPage() {
            window.location.reload(true);
        }

        function confirmarDescargaInforme() {
            var ddl = document.getElementById('<%= ddlTipoInformeEnc.ClientID %>');
            var tipo = ddl ? ddl.value : '';
            var nombre = '';
            
            switch (tipo) {
                case 'general': nombre = 'Informe General'; break;
                case 'sede': nombre = 'Informe por Sede'; break;
                case 'cargo': nombre = 'Informe por Cargo'; break;
                case 'respuestas': nombre = 'Informe de Respuestas'; break;
                default: nombre = ''; break;
            }
            
            if (!nombre) {
                alert('Por favor, seleccione un tipo de informe antes de continuar.');
                return false;
            }
            
            return confirm('¿Está seguro que quiere descargar el ' + nombre + '?');
        }

        // Inicialización del gráfico y estadísticas
        document.addEventListener("DOMContentLoaded", function () {
            var totalFinalizadas = <%= TotalFinalizadas %>;
            var totalPendientes = <%= TotalPendientes %>;

            if (totalFinalizadas == null || totalPendientes == null) {
                console.error("Datos faltantes: TotalFinalizadas o TotalPendientes no se cargaron.");
                return;
            }

            // Calcular porcentaje de completitud
            var total = totalFinalizadas + totalPendientes;
            var porcentaje = total > 0 ? ((totalFinalizadas / total) * 100).toFixed(1) : 0;
            
            // Actualizar el porcentaje en la card
            var statPorcentaje = document.getElementById('statPorcentaje');
            if (statPorcentaje) {
                statPorcentaje.textContent = porcentaje + '%';
            }

            // Configuración de exageración mínima para el gráfico
            var minPercentage = 0.05;
            var finalizadasPercentage = totalFinalizadas / total;
            var pendientesPercentage = totalPendientes / total;

            if (finalizadasPercentage < minPercentage) {
                finalizadasPercentage = minPercentage;
                pendientesPercentage = 1 - minPercentage;
            } else if (pendientesPercentage < minPercentage) {
                pendientesPercentage = minPercentage;
                finalizadasPercentage = 1 - minPercentage;
            }

            var finalizadasAngle = finalizadasPercentage * 2 * Math.PI;
            var pendientesAngle = pendientesPercentage * 2 * Math.PI;

            // Dibujar el gráfico
            var canvas = document.getElementById('pieChart');
            if (!canvas) {
                console.error("Canvas 'pieChart' no encontrado.");
                return;
            }

            // Ajustar tamaño del canvas en móviles
            function resizeCanvas() {
                var container = canvas.parentElement;
                var containerWidth = container.clientWidth;
                var maxSize = Math.min(containerWidth - 40, 300);
                
                if (window.innerWidth <= 480) {
                    maxSize = Math.min(containerWidth - 20, 220);
                } else if (window.innerWidth <= 768) {
                    maxSize = Math.min(containerWidth - 30, 260);
                }
                
                canvas.width = maxSize;
                canvas.height = maxSize;
                
                drawChart();
            }

            function drawChart() {
                var ctx = canvas.getContext('2d');
                var centerX = canvas.width / 2;
                var centerY = canvas.height / 2;
                var radius = Math.min(centerX, centerY) - 20;

                // Limpiar canvas
                ctx.clearRect(0, 0, canvas.width, canvas.height);

                // Aplicar sombra al gráfico
                ctx.shadowColor = 'rgba(0, 0, 0, 0.2)';
                ctx.shadowBlur = 15;
                ctx.shadowOffsetX = 0;
                ctx.shadowOffsetY = 5;

                // Dibujar segmento de finalizadas (azul)
                ctx.beginPath();
                ctx.moveTo(centerX, centerY);
                ctx.arc(centerX, centerY, radius, 0, finalizadasAngle);
                ctx.closePath();
                ctx.fillStyle = '#007bff';
                ctx.fill();

                // Dibujar segmento de pendientes (rojo)
                ctx.beginPath();
                ctx.moveTo(centerX, centerY);
                ctx.arc(centerX, centerY, radius, finalizadasAngle, finalizadasAngle + pendientesAngle);
                ctx.closePath();
                ctx.fillStyle = '#e74c3c';
                ctx.fill();

                // Quitar sombra para el centro
                ctx.shadowColor = 'transparent';

                // Dibujar círculo blanco en el centro
                ctx.beginPath();
                ctx.arc(centerX, centerY, radius * 0.5, 0, 2 * Math.PI);
                ctx.fillStyle = '#fff';
                ctx.fill();
                ctx.strokeStyle = 'rgba(0, 0, 0, 0.05)';
                ctx.lineWidth = 2;
                ctx.stroke();

                // Dibujar texto en el centro (responsive)
                var fontSize = canvas.width > 250 ? 32 : 24;
                var labelSize = canvas.width > 250 ? 12 : 10;
                
                ctx.fillStyle = '#2c3e50';
                ctx.font = 'bold ' + fontSize + 'px Arial, sans-serif';
                ctx.textAlign = 'center';
                ctx.textBaseline = 'middle';
                ctx.fillText(total, centerX, centerY - (fontSize * 0.3));

                ctx.fillStyle = '#7f8c8d';
                ctx.font = labelSize + 'px Arial, sans-serif';
                ctx.fillText('TOTAL', centerX, centerY + (fontSize * 0.4));

                // Función para dibujar texto en los segmentos
                function drawSegmentText(ctx, text, angleStart, angleEnd) {
                    var midAngle = angleStart + (angleEnd - angleStart) / 2;
                    var textRadius = radius * 0.7;
                    var x = centerX + Math.cos(midAngle) * textRadius;
                    var y = centerY + Math.sin(midAngle) * textRadius;

                    var segmentFontSize = canvas.width > 250 ? 18 : 14;
                    
                    ctx.fillStyle = '#fff';
                    ctx.font = 'bold ' + segmentFontSize + 'px Arial, sans-serif';
                    ctx.shadowColor = 'rgba(0, 0, 0, 0.3)';
                    ctx.shadowBlur = 4;
                    
                    ctx.fillText(text, x, y);
                    
                    ctx.shadowColor = 'transparent';
                }

                // Dibujar números en los segmentos solo si son visibles
                if (finalizadasPercentage > 0.1) {
                    drawSegmentText(ctx, totalFinalizadas, 0, finalizadasAngle);
                }
                if (pendientesPercentage > 0.1) {
                    drawSegmentText(ctx, totalPendientes, finalizadasAngle, finalizadasAngle + pendientesAngle);
                }
            }

            // Ejecutar resize inicial y en cambios de tamaño
            resizeCanvas();
            window.addEventListener('resize', resizeCanvas);

            // Actualizar leyendas
            document.getElementById('finalizadasCount').textContent = totalFinalizadas;
            document.getElementById('pendientesCount').textContent = totalPendientes;

            // Agregar animación de hover a las stats cards
            var statCards = document.querySelectorAll('.stat-card-encuesta');
            statCards.forEach(function(card) {
                card.addEventListener('mouseenter', function() {
                    if (window.innerWidth > 768) {
                        this.style.transform = 'translateY(-5px)';
                    }
                });
                card.addEventListener('mouseleave', function() {
                    this.style.transform = 'translateY(0)';
                });
            });

            // Log para debugging
            console.log('Gráfico renderizado:', {
                total: total,
                finalizadas: totalFinalizadas,
                pendientes: totalPendientes,
                porcentaje: porcentaje + '%'
            });
        });

        // Validación adicional para el dropdown
        document.addEventListener("DOMContentLoaded", function() {
            var ddl = document.getElementById('<%= ddlTipoInformeEnc.ClientID %>');
            if (ddl) {
                ddl.addEventListener('change', function () {
                    if (this.value) {
                        this.style.borderColor = '#27ae60';
                    } else {
                        this.style.borderColor = '#e0e0e0';
                    }
                });
            }
        });

        // Prevenir envío del formulario al presionar Enter en el dropdown
        document.addEventListener('keypress', function (e) {
            if (e.key === 'Enter' && e.target.tagName === 'SELECT') {
                e.preventDefault();
            }
        });
    </script>

</asp:Content>