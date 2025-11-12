<%@ Page ValidateRequest="false" Title="Consulta nómina y pagos no salariales" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="V_DesprendiblesNomina.aspx.cs" Inherits="Intranet_3._0.Vistas.V_Nomina.V_DesprendiblesNomina" %>

<asp:Content ID="Content1" ContentPlaceHolderID="scripts_css" runat="server">
    <link href="../../Styles/bootstrap/bootstrap.min.css" rel="stylesheet" />
    <link rel="Stylesheet" href="/Styles/css/desprendibles_pago/desprendibles_pago.css" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <div class="pnl_tag">
        <p><i class="fas fa-tag"></i>Desprendibles de pago</p>
    </div>
    <div class="filter">
        <div class="box_menu_crear">
        </div>
    </div>

    <div class="container">
        <div class="container-fluid">
            <div class="row row-cols-1 row-cols-md-4 filtros">
                <div class="col form-group">
                    <label for="mes">Mes</label>
                    <asp:DropDownList ID="DropDownListMonth" runat="server" CssClass="form-select" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_SelectedIndexChanged">
                        <asp:ListItem Value="0" Text="Selecciona un mes" />
                        <asp:ListItem Value="1">Enero</asp:ListItem>
                        <asp:ListItem Value="2">Febrero</asp:ListItem>
                        <asp:ListItem Value="3">Marzo</asp:ListItem>
                        <asp:ListItem Value="4">Abril</asp:ListItem>
                        <asp:ListItem Value="5">Mayo</asp:ListItem>
                        <asp:ListItem Value="6">Junio</asp:ListItem>
                        <asp:ListItem Value="7">Julio</asp:ListItem>
                        <asp:ListItem Value="8">Agosto</asp:ListItem>
                        <asp:ListItem Value="9">Septiembre</asp:ListItem>
                        <asp:ListItem Value="10">Octubre</asp:ListItem>
                        <asp:ListItem Value="11">Noviembre</asp:ListItem>
                        <asp:ListItem Value="12">Diciembre</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col form-group">
                    <label for="año">Año</label>
                    <asp:DropDownList ID="DropDownListYear" runat="server" CssClass="form-select" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_SelectedIndexChanged">
                        <asp:ListItem Value="0" Text="Selecciona un año" />
                    </asp:DropDownList>
                </div>
                <div class="col form-group">
                    <label for="periodo">Período</label>
                    <asp:DropDownList ID="DropDownListPeriod" runat="server" CssClass="form-select" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_SelectedIndexChanged">
                        <asp:ListItem Value="0" Text="Selecciona un período" />
                        <asp:ListItem Value="1">Quincena 1</asp:ListItem>
                        <asp:ListItem Value="2">Quincena 2</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col form-group">
                    <asp:Button ID="btnConsultar" runat="server" Text="Consultar" CssClass="btn btn-primary btn-spacing" OnClick="BtnConsultar_Click" />
                    <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" OnClick="btnLimpiar_Click" CssClass="btn btn-secondary btn-spacing" />
                </div>
            </div>
        </div>
    </div>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Panel ID="pnl_Resultado" runat="server" Visible="false" CssClass="container">
                <div class="pnl_Botones_Formulario col-12" id="Botones">
                    <button type="button" id="btn_Imprimir" runat="server" onclick="ImprimeDiv();" class="btn btn-secondary" style="background: rgba(22,160,133,1); color: #fff; font-size: 15px;">Imprimir</button>
                    <button type="button" id="btn_DescargaPDF" runat="server" onclick="DescargaPDF();" class="btn btn-secondary" style="background: rgba(22,160,133,1); color: #fff; font-size: 15px;">Descargar</button>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

    <%--    INTERFAZ PARA GENERAR LOS DESPRENDIBLES --%>

    <div class="pdfs row-2 fs-6" id="imprimir">
        <%--1. DESPRENDIBLE DE NOMINA--%>
        <div id="contenedor-pdf">
            <div class="container-desprendible" style="width: 1550px !important;">
                <asp:UpdatePanel ID="DesprendibleNomina" runat="server">
                    <ContentTemplate>
                        <div class="contenido-informe">
                            <%--   Encabezado  --%>
                            <table class="tabla_encabezado">
                                <tr>
                                    <td>
                                        <asp:Image runat="server" ID="img_etib" ImageUrl="~/Content/img/logo_etib.png" Width="75px" />
                                    </td>
                                    <td class="titulo lh-1">
                                        <div>
                                            <asp:Label ID="lbl_NombreEmpresa" runat="server" CssClass="fs-6"></asp:Label>
                                        </div>
                                        <div>
                                            <asp:Label ID="lbl_NombreComprobante" runat="server" class="tex_Encabezado fs-6"></asp:Label>
                                        </div>
                                        <div>
                                            <asp:Label ID="lbl_NombreQuicena" runat="server" class="tex_Encabezado fs-6"></asp:Label>
                                        </div>
                                        <div>
                                            <asp:Label ID="lbl_Periodo" runat="server" class="tex_Encabezado fs-6"></asp:Label>
                                        </div>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_FechaHora" runat="server" CssClass="align-top"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                            <%--       Tabla de la información del documento generado  --%>
                            <table class="informacion-Colaborador">
                                <tr>
                                    <td>
                                        <span class="fs-6">Página:</span>
                                    </td>
                                    <td>
                                        <asp:Label runat="server" CssClass="fs-6">1</asp:Label>
                                    </td>
                                    <td>
                                        <span class="fs-6">Email:</span>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_Email" runat="server" CssClass="fs-6">Correo del colaborador</asp:Label>
                                    </td>
                                    <td>
                                        <span class="fs-6">Documento:</span>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_IdentificadorDocumento" runat="server" CssClass="fs-6">Id del Documento</asp:Label>
                                    </td>
                                </tr>

                                <tr>
                                    <td class="fs-6">Dirección:</td>
                                    <td>
                                        <asp:Label ID="lbl_Dirección" runat="server" CssClass="fs-6">Dirección del colaborador</asp:Label>
                                    </td>
                                    <td class="fs-6">Teléfono:</td>
                                    <td>
                                        <asp:Label ID="lbl_Telefono" runat="server" CssClass="fs-6">Telefono del colaborador</asp:Label>
                                    </td>
                                    <td class="fs-6">Fecha de ingreso:</td>
                                    <td>
                                        <asp:Label ID="lbl_FechaIngreso" runat="server" CssClass="fs-6">Fecha Ingreso colaborador</asp:Label>
                                    </td>
                                </tr>
                            </table>

                            <%--       Tabla de la información del colaborador  --%>
                            <table class="informacion-Colaborador">
                                <tr>
                                    <td class="fs-6">Nombre Colaborador:</td>
                                    <td>
                                        <asp:Label ID="lbl_NombreColaborador" runat="server" CssClass="fs-6">Nombre del colaborador</asp:Label></td>
                                    <td class="fs-6">Cédula Número:</td>
                                    <td>
                                        <asp:Label ID="lbl_CedulaColaborador" runat="server" CssClass="fs-6">Cedula del colaborador</asp:Label></td>
                                    <td class="fs-6">Entidad pensión:</td>
                                    <td>
                                        <asp:Label ID="lbl_EntidadPension" runat="server" CssClass="fs-6">Pension del colaborador</asp:Label></td>
                                </tr>

                                <tr>
                                    <td class="fs-6">Cargo:</td>
                                    <td>
                                        <asp:Label ID="lbl_CargoColaborador" runat="server" CssClass="fs-6">Cargo del colaborador</asp:Label></td>
                                    <td class="fs-6">No. de Cuenta Bancaria:</td>
                                    <td>
                                        <asp:Label ID="lbl_CuentaColaborador" runat="server" CssClass="fs-6">Cuenta del colaborador</asp:Label></td>
                                    <td class="fs-6">Entidad Bancaria:</td>
                                    <td>
                                        <asp:Label ID="lbl_EntidadBancaria" runat="server" CssClass="fs-6">Banco del colaborador</asp:Label></td>
                                </tr>

                                <tr>
                                    <td class="fs-6">Sueldo básico:</td>
                                    <td>
                                        <asp:Label ID="lbl_SueldoColaborador" runat="server" CssClass="fs-6">Sueldo del colaborador</asp:Label></td>
                                    <td class="fs-6">Entidad salud:</td>
                                    <td>
                                        <asp:Label ID="lbl_EpsColaborador" runat="server" CssClass="fs-6">EPS del colaborador</asp:Label></td>
                                    <td class="fs-6">C. Costo:</td>
                                    <td>
                                        <asp:Label ID="lbl_ConceptoCosto" runat="server" CssClass="fs-6">Nombre del Concepto</asp:Label></td>
                                </tr>
                            </table>

                            <%--       Tabla de encabezado para relación de nomina  --%>
                            <table class="encabezado-tabla-conceptos" id="encabezado-tabla-conceptos">
                                <tr>
                                    <td class="fs-6">Concepto</td>
                                    <td class="fs-6">Descripción</td>
                                    <td class="fs-6">Unidades</td>
                                    <td class="fs-6">Vlr devengo</td>
                                    <td class="fs-6">Vlr deducción</td>
                                    <td class="fs-6">Valor total</td>
                                    <td class="fs-6">Descontado</td>
                                    <td class="fs-6">Saldo</td>
                                </tr>
                            </table>

                            <%--    Tabla conceptos de nomina  --%>
                            <table id="tbl_ConceptosNomina" runat="server" class="tabla-conceptos">
                                <!-- Las filas dinámicas se agregarán aquí -->
                            </table>

                            <span class="separador"></span>

                            <%--   Tabla totales Concepto Nomina  --%>
                            <table id="tbl_TotalesConcepto" runat="server" class="tabla-totalesConceptos">
                                <tr>
                                    <td colspan="2"><span class="fs-6">Totales </span></td>
                                    <td>
                                        <asp:Label ID="lbl_TotalUnidades" runat="server" CssClass="fs-6"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lbl_TotalValorDevengo" runat="server" CssClass="fs-6"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lbl_TotalValorDeduccion" runat="server" CssClass="fs-6"></asp:Label></td>
                                    <td class="fs-6" style="font-weight: bold; text-align: right;">Tipo de Cuenta: </td>
                                    <td colspan="2" style="text-align: left;">
                                        <asp:Label ID="lbl_TipoCuenta" runat="server" CssClass="fs-6">Tipo Cuenta Colaborador</asp:Label></td>
                                </tr>
                            </table>

                            <%--   Total Neto Concepto Nomina  --%>
                            <table id="tbl_TotalNeto" runat="server" class="tabla-totalNeto">
                                <tr>
                                    <td colspan="2"><span class="fs-6">Neto a pagar:</span></td>
                                    <td>
                                        <asp:Label ID="lbl_TotalNeto" runat="server" CssClass="fs-6"></asp:Label></td>
                                    <td colspan="5"></td>
                                </tr>
                            </table>

                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <%-- 2. DESPRENDIBLE DE PAGOS NO SALARIALES--%>
            <div class="container-desprendible-no-salariales" style="width: 1550px !important;">
                <asp:UpdatePanel ID="DesprendibleNoSalarial" runat="server" CssClass="fs-6">
                    <ContentTemplate>
                        <div class="contenido-informe">
                            <%--   Encabezado  --%>
                            <table class="tabla_encabezado">
                                <tr>
                                    <br />
                                    <%--<td>
                                        <asp:Image runat="server" ID="Image1" ImageUrl="~/Content/img/logo_etib.png" Width="80px" />
                                    </td>--%>
                                    <td class="titulo">
                                        <%--<asp:Label ID="lbl_NS_NombreEmpresa" runat="server"></asp:Label>
                                        <br />--%>
                                        <asp:Label ID="lbl_NS_NombreComprobante" runat="server" class="tex_Encabezado fs-6"></asp:Label>
                                        <br />
                                        <%--<asp:Label ID="lbl_NS_NombreQuicena" runat="server" class="tex_Encabezado"></asp:Label>
                                        <br />
                                        <asp:Label ID="lbl_NS_Periodo" runat="server" class="tex_Encabezado"></asp:Label>--%>
                                    </td>
                                    <%--<td>
                                        <asp:Label ID="lbl_NS_FechaHora" runat="server"></asp:Label>
                                    </td>--%>
                                </tr>
                            </table>

                            <%--<%--       Tabla de la información del documento generado  --%>
                            <%-- <table class="informacion-Colaborador">
                                <tr>
                                    <td>
                                        <span>Página:</span>
                                    </td>
                                    <td>
                                        <asp:Label runat="server">1</asp:Label>
                                    </td>
                                    <td>
                                        <span>Email:</span>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_NS_Email" runat="server">Correo del colaborador</asp:Label>
                                    </td>
                                    <td>
                                        <span>Documento:</span>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_NS_IdentificadorDocumento" runat="server">Id del Documento</asp:Label>
                                    </td>
                                </tr>

                                <tr>
                                    <td>Dirección:</td>
                                    <td>
                                        <asp:Label ID="lbl_NS_Dirección" runat="server">Dirección del colaborador</asp:Label>
                                    </td>
                                    <td>Teléfono:</td>
                                    <td>
                                        <asp:Label ID="lbl_NS_Telefono" runat="server">Telefono del colaborador</asp:Label>
                                    </td>
                                    <td>Fecha de ingreso:</td>
                                    <td>
                                        <asp:Label ID="lbl_NS_FechaIngreso" runat="server">Fecha Ingreso colaborador</asp:Label>
                                    </td>
                                </tr>
                            </table>--%>

                            <%--       Tabla de la información del colaborador  --%>
                            <%--<table class="informacion-Colaborador">
                                <tr>
                                    <td>Nombre Colaborador:</td>
                                    <td>
                                        <asp:Label ID="lbl_NS_NombreColaborador" runat="server">Nombre del colaborador</asp:Label></td>
                                    <td>Cédula Número:</td>
                                    <td>
                                        <asp:Label ID="lbl_NS_CedulaColaborador" runat="server">Cedula del colaborador</asp:Label></td>
                                    <td>Sueldo básico:</td>
                                    <td>
                                        <asp:Label ID="lbl_NS_SueldoColaborador" runat="server">Sueldo del colaborador</asp:Label></td>

                                </tr>

                                <tr>
                                    <td>Cargo:</td>
                                    <td>
                                        <asp:Label ID="lbl_NS_CargoColaborador" runat="server">Cargo del colaborador</asp:Label></td>
                                    <td>C. Costo:</td>
                                    <td>
                                        <asp:Label ID="lbl_NS_ConceptoCosto" runat="server">Nombre del Concepto</asp:Label></td>--%>
                            <%--<td>No. de Cuenta Bancaria:</td>
                                    <td>
                                        <asp:Label ID="lbl_NS_CuentaColaborador" runat="server">Cuenta del colaborador</asp:Label></td>--%>
                            <%-- </tr>--%>
                            <%--<tr>
                                    <td>Entidad pensión:</td>
                                    <td>
                                        <asp:Label ID="lbl_NS_EntidadPension" runat="server">Pension del colaborador</asp:Label></td>
                                    <td>Entidad salud:</td>
                                    <td>
                                        <asp:Label ID="lbl_NS_EpsColaborador" runat="server">EPS del colaborador</asp:Label></td>
                                     <td>Entidad Bancaria:</td>
                                     <td>
                                         <asp:Label ID="lbl_NS_EntidadBancaria" runat="server">Banco del colaborador</asp:Label></td>
                                </tr>--%>
                            <%--</table>--%>

                            <%--       Tabla de encabezado para relación de conceptos No salariales  --%>
                            <table id="encabezado-tabla-conceptosNoSalarial" class="encabezado-tabla-conceptos">
                                <tr>
                                    <td>Concepto</td>
                                    <td>Descripción</td>
                                    <td>Unidades</td>
                                    <td>Vlr devengo</td>
                                    <td>Vlr deducción</td>
                                    <td>Valor total</td>
                                    <td>Descontado</td>
                                    <td>Saldo</td>
                                </tr>
                            </table>

                            <%--    Tabla conceptos No salariales  --%>
                            <table id="tbl_ConceptosNoSalariales" runat="server" class="tabla-conceptos">
                                <!-- Las filas dinámicas se agregarán aquí -->
                            </table>

                            <span class="separador"></span>

                            <%--   Tabla totales Conceptos No salariales  --%>
                            <table id="tbl_TotalesConceptoNoSalariales" runat="server" class="tabla-totalesConceptos">
                                <tr>
                                    <td colspan="2"><span>Totales </span></td>
                                    <td>
                                        <asp:Label ID="lbl_NS_TotalUnidades" runat="server" CssClass="fs-6"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lbl_NS_TotalValorDevengo" runat="server" CssClass="fs-6"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lbl_NS_TotalValorDeduccion" runat="server" CssClass="fs-6"></asp:Label></td>
                                    <td style="font-weight: bold; text-align: right;"></td>
                                    <td colspan="2" style="text-align: left;">
                                        <asp></asp>
                                    </td>
                                </tr>
                            </table>

                            <%--   Total Neto Concepto No salariales  --%>
                            <table id="tbl_NS_TotalNeto" runat="server" class="tabla-totalNeto">
                                <tr>
                                    <td colspan="2"><span>Neto a pagar:</span></td>
                                    <td>
                                        <asp:Label ID="lbl_NS_TotalNeto" runat="server" CssClass="fs-6"></asp:Label></td>
                                    <td colspan="5"></td>
                                </tr>
                            </table>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <!-- Script para la descarga de PDFs -->
    <script>
        // Función para descargar ambos desprendibles en un solo PDF
        function DescargaPDF() {
            var $nominaElemento = document.getElementById('contenedor-pdf');

            // Agregar la clase 'scale-down' al elemento
            $nominaElemento.classList.add('scale-down');
            var originalHeight = $nominaElemento.offsetHeight;
            var newHeight = originalHeight * 0.55;
            $nominaElemento.style.height = newHeight + 'px';
            // Datos de identificación 
            var numDoc = document.querySelector('#<%= lbl_CedulaColaborador.ClientID %>').innerText.trim();
            var codSAE = document.querySelector('#<%= lbl_IdentificadorDocumento.ClientID %>').innerText.trim();

            // Asignar identificador único para el nombre del archivo
            var $identif = numDoc || codSAE || 'Sin_Identificacion';
            const fecha = new Date().toISOString().split('T')[0];

            // Crear el PDF combinando ambos elementos
            html2pdf()
                .set({
                    margin: [0.3, 0.3, 0.2, 0.2],  // Márgenes del PDF (en pulgadas)
                    image: {
                        type: 'jpeg',  // Tipo de imagen
                        quality: 0.98  // Calidad de la imagen
                    },
                    html2canvas: {
                        scale: 1.5,  // A mayor escala, mejor resolución de gráficos
                        letterRendering: true  // Mejor renderización de texto
                    },
                    jsPDF: {
                        unit: 'in',  // Unidad de medida en pulgadas
                        format: 'letter',  // Tamaño de página: carta (8.5 x 11 in)
                        orientation: 'landscape'  // Orientación horizontal
                    },
                    pagebreak: {
                        mode: ['avoid-all', 'css', 'legacy']  // Controlar saltos de página
                    }
                })
                .from($nominaElemento)
                .save('Desprendibles_' + $identif + '_' + fecha + '.pdf')
                .then(function () {
                    $nominaElemento.classList.remove('scale-down');
                    $nominaElemento.style.height = '';
                });
        }

        function ImprimeDiv() {
            var divToPrint = document.getElementById('imprimir');

            if (!divToPrint) {
                console.error("Elemento con ID 'imprimir' no encontrado.");
                return;
            }

            var newWin = window.open('', 'Print-Window', 'width=1000,height=700');
            HTMLDocument.prototype.e = document.getElementById;

            // Captura todas las hojas de estilo del documento principal
            var styles = Array.from(document.querySelectorAll('link[rel="stylesheet"], style')).map(style => style.outerHTML).join("");

            // Estilo para orientación landscape
            const printStyles = `
                <style>
                    @media print {
                        @page {
                            size: a4 landscape;
                            margin: 10px;
                            padding: 10px;
                        }
                        body {
                            margin: 10px;
                            padding: 10px;
                            height: 100%;
                        }
                    }
                </style>
            `;

            // Construye el contenido del documento con los estilos incluidos
            var htmlContent = `
                <html>
                    <head>
                        <title>Impresión</title>
                        ${styles} <!-- Inserta las hojas de estilo -->
                        ${printStyles} <!-- Inserta los estilos para impresión -->
                    </head>
                    <body onload="window.print();">
                        ${divToPrint.innerHTML}
                    </body>
                </html>`;

            // Escribe el contenido en la nueva ventana
            newWin.document.open();
            newWin.document.write(htmlContent);
            newWin.document.close();

            // Cierra automáticamente la ventana después de un tiempo
            setTimeout(() => {
                newWin.close();
            }, 500);
        };
    </script>
</asp:Content>
