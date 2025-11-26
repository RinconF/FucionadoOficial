<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="V_Programacion.aspx.cs" Inherits="Intranet_3._0.Vistas.V_Operacional.V_Programacion"%>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        const aceptar_cookie = document.querySelector('#enlace_aceptar');

        function soloNumeros(e) {
            var key = window.Event ? e.which : e.keyCode
            return ((key >= 48 && key <= 57) || (key == 8))
        };

        const panelBotones = document.querySelector('#MainContent_contenedor_botones_formulario');

        function ImprimeDiv() {
            var divToPrint = document.getElementById('contenido_imprimible');
            var newWin = window.open('', 'Print-Window', 'width=1000,height=700');
            HTMLDocument.prototype.e = document.getElementById;

            //AGR - Se valida el Sistema Operativo desde donde se está realizando la petición de impresión
            var SO = "Desconocido";
            if (navigator.appVersion.indexOf("Win") != -1) SO = "Windows OS";
            if (navigator.appVersion.indexOf("Mac") != -1) SO = "MacOS";
            if (navigator.appVersion.indexOf("X11") != -1) SO = "UNIX OS";
            if (navigator.appVersion.indexOf("Linux") != -1) SO = "Linux OS";

            if (SO == "Windows OS" || SO == "MacOS") {
                newWin.document.open();
                newWin.document.write('<html><body onload="window.print()">' + divToPrint.innerHTML + '</body></html>');
                newWin.document.close();
                setTimeout(function () { newWin.close(); }, 10);
            }
            else {
                newWin.document.open();
                newWin.document.write('<html><body onload="window.print()">' + divToPrint.innerHTML + '</body></html>');
            }
        };

        function DescargaPDF() {
            var $identif;
            const $elementoParaConvertir = document.getElementById('contenido_imprimible'); // <-- Aquí se elige el elemento del DOM que se va a imprimir
            const numDoc = document.querySelector('#MainContent_campo_cedula').value; //AGR - captura fecha inicial y final para nombre de archivo pdf
            const codSAE = document.querySelector('#MainContent_campo_codigo').value;

            //Asigna num de documento o cod sae para nombre de archivo pdf.
            if (numDoc) {
                $identif = document.querySelector('#MainContent_campo_cedula').value;
                console.log($identif + '_');
            }
            else if (codSAE) {
                $identif = document.querySelector('#MainContent_campo_codigo').value;
                console.log($identif + '__');
            }
            else {
                $identif = 'No hay identificacion';
            }

            //AGR - Reemplazo de caracter '/' en fechas, por '-'
            const FecIni = document.querySelector('#MainContent_campo_fecha_inicial').value.replace(/[/]/gi, '-');
            const FecFin = document.querySelector('#MainContent_campo_fecha_final').value.replace(/[/]/gi, '-');

            //AGR - Creación de archivo pdf
            html2pdf()
                .set({
                    filename: $identif + '__' + FecIni + '__' + FecFin + '.pdf',
                    margin: [0.3, 0.3, 0.2, 0.2],
                    image: {
                        type: 'jpeg',
                        quality: 0.98
                    },
                    html2canvas: {
                        scale: 1.5, //AGR -  A mayor escala, mejores gráficos, pero mayor tamaño de archivo
                        letterRendering: true
                    },
                    jsPDF: {
                        unit: "in",
                        format: "a3", //AGR - letter, a3, a4
                        orientation: 'portrait' //AGR -  landscape o portrait
                    },
                    pagebreak: {
                        mode: ['avoid-all', 'css', 'legacy']
                    }
                })
                .from($elementoParaConvertir)
                .save()
                .catch(err => console.log(err));
        };
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="Stylesheet" href="/Styles/css/default_encuestas/default_encuestas.css"/>
    <link rel="Stylesheet" href="/Styles/css/programacion_plantilla/programacion_plantilla.css"/>

    <div class="etiqueta_panel">
        <p><i class="fas fa-tag"></i> Consulta de programación</p>
    </div>
    
    <div class="filtro_superior">
        <div class="caja_menu_crear">
        </div>
    </div>

    <div class="contenedor" id="contenedor-programacion">
        <asp:UpdatePanel ID="panel_actualizacion1" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <div class="fila seccion_filtros">
                    <div class="grupo_formulario col-6 col-lg">
                        <label for="staticEmail2">Código</label>
                        <asp:TextBox ID="campo_codigo" runat="server" CssClass="control_formulario" MaxLength="6" placeholder="Ej: 70XXXX" onKeyPress="return soloNumeros(event)" ></asp:TextBox>
                    </div>

                    <div class="grupo_formulario col-6 col-lg">
                        <label for="campo_cedula">Cédula</label>
                        <asp:TextBox ID="campo_cedula" runat="server" CssClass="control_formulario" onKeyPress="return soloNumeros(event)" placeholder="Ej: 1033123456" ></asp:TextBox>
                    </div>

                    <div class="grupo_formulario col-6 col-lg">
                        <label for="campo_fecha_inicial">Fecha Inicial</label>

                        <div class="grupo_entrada">
                            <asp:TextBox ID="campo_fecha_inicial" runat="server" CssClass="control_formulario" Enabled="false"></asp:TextBox>
                            <span class="complemento_grupo_entrada">
                                <asp:LinkButton runat="server" OnClick="abrirFechaInicio">
                                <span class="glyphicon glyphicon-calendar"></span>
                                </asp:LinkButton>
                            </span>
                        </div>

                        <asp:Panel ID="calendario_1" CssClass="panel_calendarios" runat="server" Visible="False">
                            <asp:Calendar ID="Calendar1" runat="server" OnSelectionChanged="Calendar1_SelectionChanged" BackColor="White" BorderStyle="None" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" NextPrevFormat="FullMonth">
                                <DayHeaderStyle Font-Bold="True" Font-Size="7pt" />
                                <NextPrevStyle Font-Bold="True" Font-Size="6pt" ForeColor="#333333" VerticalAlign="Bottom" />
                                <OtherMonthDayStyle ForeColor="#999999" />
                                <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                                <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="2px" Font-Bold="True" Font-Size="10pt" ForeColor="#333399" />
                                <TodayDayStyle BackColor="#CCCCCC" />
                            </asp:Calendar>
                        </asp:Panel>
                    </div>

                    <div class="grupo_formulario col-6 col-lg">
                        <label for="campo_fecha_final">Fecha Final</label>

                        <div class="grupo_entrada">
                            <asp:TextBox ID="campo_fecha_final" runat="server" CssClass="control_formulario" Enabled="false"></asp:TextBox>
                            <span class="complemento_grupo_entrada">
                                <asp:LinkButton runat="server" OnClick="abrirFechaFin">
                                <span class="glyphicon glyphicon-calendar"></span>
                                </asp:LinkButton>
                            </span>
                        </div>

                        <asp:Panel ID="calendario_2" CssClass="panel_calendarios" runat="server" Visible="False">
                            <asp:Calendar ID="Calendar2" runat="server" OnSelectionChanged="Calendar2_SelectionChanged" BackColor="White" BorderStyle="None" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" NextPrevFormat="FullMonth">
                                <DayHeaderStyle Font-Bold="True" Font-Size="7pt" />
                                <NextPrevStyle Font-Bold="True" Font-Size="6pt" ForeColor="#333333" VerticalAlign="Bottom" />
                                <OtherMonthDayStyle ForeColor="#999999" />
                                <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                                <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="2px" Font-Bold="True" Font-Size="10pt" ForeColor="#333399" />
                                <TodayDayStyle BackColor="#CCCCCC" />
                            </asp:Calendar>
                        </asp:Panel>
                    </div>

                    <div class="grupo_formulario columna col-lg">
                        <asp:UpdateProgress ID="progreso" runat="server" AssociatedUpdatePanelID="panel_actualizacion1">
                            <ProgressTemplate>
                                 <asp:Image ID="imagen_cargando1" runat="server" ImageUrl="../../Content/img/loading2.gif" AlternateText="Consultando..." Width="50px" Height="50px" />
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                        <asp:Button ID="boton_consultar" runat="server" Text="Consultar" OnClick="btnConsultar_Click" CssClass="boton boton_primario" />
                        <asp:Button ID="boton_limpiar" runat="server" Text="Limpiar" OnClick="btnLimpiar_Click" CssClass="boton boton_secundario" />
                    </div>

                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:UpdatePanel ID="panel_actualizacion2" runat="server">
            <ContentTemplate>
                <asp:Panel ID="panel_resultado" runat="server" Visible="false" CssClass="contenedor">

                    <div class="contenedor_botones_formulario col-12" id="seccion_botones">
                        <button type="button" id="boton_imprimir" onclick="ImprimeDiv();" class="boton boton_secundario">Imprimir</button>
                        <button type="button" id="boton_descargar_pdf" onclick="DescargaPDF();" class="boton boton_secundario">Descargar PDF</button>
                    </div>
                    
                    <div class="fila_principal" ID="contenido_imprimible">
                        <div class="col-12" style="max-width: 80%; margin: 0 auto;">
                            <div class="fila encabezado_hoja_trabajo">
                                <asp:Panel runat="server" CssClass="col-12 texto_centrado">
                                    <asp:Label ID="etiqueta_titulo" runat="server" Text="Hoja de Trabajo" Font-Bold="true"></asp:Label>
                                </asp:Panel>

                                <asp:Panel runat="server" CssClass="col-12 columna_media">
                                    <div class="fila">
                                        <asp:Panel runat="server" CssClass="columna_etiqueta">
                                            <span>Conductor: </span>
                                        </asp:Panel>
                                        <asp:Panel runat="server" CssClass="columna_contenido">
                                            <asp:Label ID="etiqueta_info_conductor" runat="server" Text=""></asp:Label>
                                        </asp:Panel>
                                    </div>
                                </asp:Panel>

                                <asp:Panel runat="server" CssClass="col-12 columna_media">
                                    <div class="fila">
                                        <asp:Panel runat="server" CssClass="columna_etiqueta">
                                            <span>Código: </span>
                                        </asp:Panel>
                                        <asp:Panel runat="server" CssClass="columna_contenido">
                                            <asp:Label ID="etiqueta_info_codigo" runat="server" Text=""></asp:Label>
                                        </asp:Panel>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>

                        <asp:Panel runat="server" CssClass="col-12 contenedor_sin_espacios">
                            <asp:Panel ID="panel_tablas_horarios" runat="server" CssClass="fila">
                            </asp:Panel>
                        </asp:Panel>
                    </div>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    
    <script>
        function quitarPadding() {
            let doc = document.getElementById('contenedor-programacion');
            doc.removeAttribute('style');
        }
        document.addEventListener('load', quitarPadding);
        window.addEventListener('load', quitarPadding);
    </script>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="scripts_css" runat="server">
    <script type="text/javascript">
        document.addEventListener('mousedown', function (event) {
            var cal1Panel = document.getElementById('<%= calendario_1.ClientID %>');
        var cal2Panel = document.getElementById('<%= calendario_2.ClientID %>');
            var btnIni = document.querySelector('[id$="abrirFechaInicio"]');
            var btnFin = document.querySelector('[id$="abrirFechaFin"]');

            if (cal1Panel && cal1Panel.style.display !== 'none' && !cal1Panel.contains(event.target) && (!btnIni || !btnIni.contains(event.target))) {
                cal1Panel.style.display = 'none';
            }
            if (cal2Panel && cal2Panel.style.display !== 'none' && !cal2Panel.contains(event.target) && (!btnFin || !btnFin.contains(event.target))) {
                cal2Panel.style.display = 'none';
            }
        });
    </script>
</asp:Content>