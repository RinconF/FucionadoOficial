<%@ Page Title="Informes Encuestas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="V_Info_Encuestas.aspx.cs" Inherits="Intranet_3._0.Vistas.V_Encuestas.V_Info_Encuestas" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="Stylesheet" href="../../Styles/css/default_encuestas/default_encuestas.css" />  

    <div class="pnl_tag">
        <p><i class="fas fa-tag"></i>Informes Encuestas</p>
    </div>

    <div style="text-align: center;">
        <!-- Mostrar el nombre de la encuesta -->
        <h3><%= NombreEncuesta %></h3> <!-- Aquí se muestra el nombre de la encuesta -->


        <div class ="ContainerEn">
            <!-- Mensaje en caso de no haber encuestas -->
            <asp:Label ID="lblMensaje" runat="server" CssClass="mensaje" Visible="false" />
        </div>
        
        <!-- Panel que contiene el gráfico y botones -->
        <asp:Panel ID="encuestaPanel" runat="server" Visible="false">
            <!-- Gráfica de torta -->
            <canvas id="pieChart" width="300" height="300" style="border: 1px solid #ddd; box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2); border-radius: 8px;"></canvas>

            <!-- Leyenda -->
            <div id="legend" style="text-align: center; margin-top: 20px;">
                <div style="display: flex; justify-content: center; gap: 20px;">
                    <div>
                        <span style="display: inline-block; width: 16px; height: 16px; background-color: #007bff; border-radius: 4px;"></span>
                        <span>Finalizadas: <strong id="finalizadasCount"></strong></span>
                    </div>
                    <div>
                        <span style="display: inline-block; width: 16px; height: 16px; background-color: #e52e34; border-radius: 4px;"></span>
                        <span>Pendientes: <strong id="pendientesCount"></strong></span>
                    </div>
                </div>
            </div>

            <!-- Botones de acciones -->
            <div style="display: flex; justify-content: center; margin-top: 20px;">

                <!-- Dropdown para seleccionar el tipo de informe -->
                <asp:DropDownList ID="ddlTipoInformeEnc" runat="server" CssClass="form-select"
                    Style="width: 200px; font-size: 15px; border-radius: 5px;">
                    <asp:ListItem Text="Seleccione informe..." Value="" Selected="True" />
                    <asp:ListItem Text="Informe General" Value="general" />
                    <asp:ListItem Text="Informe Por Sede" Value="sede" />
                    <asp:ListItem Text="Informe Por Cargo" Value="cargo" />
                    <asp:ListItem Text="Informe De Respuestas" Value="respuestas" />
                </asp:DropDownList>

                <asp:Button ID="btnDescargarExcel" runat="server" Text="Descargar Informe"
                    class="btn btn-secondary"
                    Style="margin-left: 20px; background: rgba(22,160,133,1); color: #fff; font-size: 15px; border-radius: 5px;"
                    OnClick="btnDescargarExcel_Click" />
                <button class="btn btn-primary" style="margin-left: 20px; background: #007bff; color: #fff; font-size: 15px; border-radius: 5px;"
                    onclick="refreshPage()">
                    Refrescar
                </button>
            </div>
        </asp:Panel>
    </div>



    <script type="text/javascript">
        function refreshPage() {
            window.location.reload(true); // Fuerza una recarga completa del servidor
        }

        document.addEventListener("DOMContentLoaded", function () {
            var totalFinalizadas = <%= TotalFinalizadas %>;
            var totalPendientes = <%= TotalPendientes %>;

            if (totalFinalizadas == null || totalPendientes == null) {
                console.error("Datos faltantes: TotalFinalizadas o TotalPendientes no se cargaron.");
                return;
            }

            // Configuración de exageración mínima
            var total = totalFinalizadas + totalPendientes;
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
            var ctx = canvas.getContext('2d');
            var centerX = canvas.width / 2;
            var centerY = canvas.height / 2;
            var radius = Math.min(centerX, centerY) - 10;

            ctx.shadowColor = 'rgba(0, 0, 0, 0.3)';
            ctx.shadowBlur = 10;

            ctx.beginPath();
            ctx.moveTo(centerX, centerY);
            ctx.arc(centerX, centerY, radius, 0, finalizadasAngle);
            ctx.closePath();
            ctx.fillStyle = '#007bff  ';
            ctx.fill();

            ctx.beginPath();
            ctx.moveTo(centerX, centerY);
            ctx.arc(centerX, centerY, radius, finalizadasAngle, finalizadasAngle + pendientesAngle);
            ctx.closePath();
            ctx.fillStyle = '#e42e34   ';
            ctx.fill();

            ctx.shadowColor = 'transparent';

            ctx.fillStyle = '#fff';
            ctx.font = 'bold 14px Arial';

            // Ajustar posición del texto en segmentos
            function drawCenteredText(ctx, text, angleStart, angleEnd, radiusOffset) {
                var midAngle = angleStart + (angleEnd - angleStart) / 2;
                var x = centerX + Math.cos(midAngle) * (radius / 2 + radiusOffset);
                var y = centerY + Math.sin(midAngle) * (radius / 2 + radiusOffset);

                var textWidth = ctx.measureText(text).width;
                ctx.fillText(text, x - textWidth / 2, y + 5); // Centramos el texto
            }

            // Dibujar valores en los segmentos
            drawCenteredText(ctx, totalFinalizadas, 0, finalizadasAngle, -10);
            drawCenteredText(ctx, totalPendientes, finalizadasAngle, finalizadasAngle + pendientesAngle, -10);

            // Actualizar leyendas
            document.getElementById('finalizadasCount').textContent = totalFinalizadas;
            document.getElementById('pendientesCount').textContent = totalPendientes;
        });

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
    </script>
</asp:Content>