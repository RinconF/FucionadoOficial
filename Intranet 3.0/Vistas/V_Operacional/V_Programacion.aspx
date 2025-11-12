<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="V_Programacion.aspx.cs" Inherits="Intranet_3._0.Vistas.V_Operacional.V_Programacion"%>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        const aceptar_cookie = document.querySelector('#lnk_aceptar');

        function soloNumeros(e) {
            var key = window.Event ? e.which : e.keyCode
            return ((key >= 48 && key <= 57) || (key == 8))
        };

        const panelBotones = document.querySelector('#MainContent_pnl_Botones_Formulario');

        function ImprimeDiv() {

            var divToPrint = document.getElementById('imprimir');
            var newWin = window.open('', 'Print-Window', 'width=1000,height=700');
            HTMLDocument.prototype.e = document.getElementById;

            //AGR - Se valida el Sistema Operativo desde donde se está realizando la petición de impresión
            var SO = "Desconocido";
            if (navigator.appVersion.indexOf("Win") != -1) SO =
                "Windows OS";
            if (navigator.appVersion.indexOf("Mac") != -1) SO =
                "MacOS";
            if (navigator.appVersion.indexOf("X11") != -1) SO =
                "UNIX OS";
            if (navigator.appVersion.indexOf("Linux") != -1) SO =
                "Linux OS";

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
            const $elementoParaConvertir = document.getElementById('imprimir'); // <-- Aquí se elige el elemento del DOM quese va a imprimir
            const numDoc = document.querySelector('#MainContent_txtCedula').value; //AGR - captura fecha inicial y final para nombre de archivo pdf
            const codSAE = document.querySelector('#MainContent_txtCode').value;

            //Asigna num de documento o cod sae para nombre de archivo pdf.
            if (numDoc) {
                $identif = document.querySelector('#MainContent_txtCedula').value;
                console.log($identif + '_');
            }

            else if (codSAE) {

                $identif = document.querySelector('#MainContent_txtCode').value;
                console.log($identif + '__');
            }
            else {
                $identif = 'No hay identificacion';
            }

            //AGR - Reemplazo de caracter '/' en fechas, por '-'
            const FecIni = document.querySelector('#MainContent_txtFecIni').value.replace(/[/]/gi, '-');
            const FecFin = document.querySelector('#MainContent_txtFecFin').value.replace(/[/]/gi, '-');

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
                        format: "letter", //AGR - letter, a3, a4
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

    <style type="text/css">
        
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="Stylesheet" href="/Styles/css/default_encuestas/default_encuestas.css"/>


        <div class="pnl_tag">
            <p><i class="fas fa-tag"></i> Consulta de programación</p>
        </div>
        <div class="filter">
                    <div class="box_menu_crear">
                    </div>
                </div>

     <%--AGR - Se adiciona  style="width: 145%" en container para mejorar visualización en dispositivos móviles--%>
        <div class="container" id="container-programacion">
        <asp:UpdatePanel ID="update1" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <div class="row filtros">
                    <div class="form-group col-6 col-lg">
                        <label for="staticEmail2">Código</label>
                        <asp:TextBox ID="txtCode" runat="server" CssClass="form-control" MaxLength="6" placeholder="Ej: 70XXXX" onKeyPress="return soloNumeros(event)" ></asp:TextBox>
                    </div>

                    <div class="form-group col-6 col-lg">
                        <label for="txtCedula">Cédula</label>
                        <asp:TextBox ID="txtCedula" runat="server" CssClass="form-control" onKeyPress="return soloNumeros(event)" placeholder="Ej: 1033123456" ></asp:TextBox>
                    </div>

                    <div class="form-group col-6 col-lg">
                        <label for="txtFecIni">Fecha Inicial</label>

                        <div class="input-group" style="z-index: 0">
                            <asp:TextBox ID="txtFecIni" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                            <span class="input-group-addon">
                                <asp:LinkButton runat="server" OnClick="abrirFechaInicio">
                                <span class="glyphicon glyphicon-calendar"></span>
                                </asp:LinkButton>
                            </span>
                        </div>

                        <asp:Panel ID="Cal_1" CssClass="pnl_calendarios" runat="server" Visible="False">
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

                    <div class="form-group col-6 col-lg">
                        <label for="txtFecFin">Fecha Final</label>

                        <div class="input-group">
                            <asp:TextBox ID="txtFecFin" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                            <span class="input-group-addon">
                                <asp:LinkButton runat="server" OnClick="abrirFechaFin">
                                <span class="glyphicon glyphicon-calendar"></span>
                                </asp:LinkButton>
                            </span>
                        </div>

                        <asp:Panel ID="Cal_2" CssClass="pnl_calendarios" runat="server" Visible="False">
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

                    <div class="form-group col col-lg">
                        <asp:UpdateProgress ID="progress" runat="server" AssociatedUpdatePanelID="update1">
                            <ProgressTemplate>
                                <asp:Image ID="imgLoading1" runat="server" ImageUrl="../../Content/img/loading.gif" style = "max-width:100px" AlternateText="Consultando..." />
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                        <asp:Button ID="btnConsultar" runat="server" Text="Consultar" OnClick="btnConsultar_Click" CssClass="btn btn-primary" />
                        <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" OnClick="btnLimpiar_Click" CssClass="btn btn-secondary" />
                    </div>

                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Panel ID="pnl_Resultado" runat="server" Visible="false" CssClass="container">

                    <div class="pnl_Botones_Formulario col-12" id="Botones">
                            <button type="button" id="btn_Imprimir" onclick="ImprimeDiv();" class="btn btn-secondary" style="background: rgba(22,160,133,1); color: #fff; font-size: 15px;">Imprimir</button>
                            <button type="button" id="btn_DescargaPDF" onclick="DescargaPDF();" class="btn btn-secondary" style="background: rgba(22,160,133,1); color: #fff; font-size: 15px;">Descargar PDF</button>
                        </div>
                    <div class="row-2" ID="imprimir" >
                        

                        <div class="col-12">
                            <div class="row Titulo_Prog">
                                <asp:Panel runat="server" CssClass="col-12 text-center" Style="border: 1px solid black;">
                                    <asp:Label ID="lblTitle" runat="server" Text="Hoja de Trabajo" Font-Bold="true"></asp:Label>
                                </asp:Panel>

                                <asp:Panel runat="server" CssClass="col-12 col-lg-6" Style="border: 1px solid black; border-top: none;">
                                    <div class="row">
                                        <asp:Panel runat="server" CssClass="col-3" Style="border-right: 1px solid black;">
                                            <span>Conductor: </span>
                                        </asp:Panel>
                                        <asp:Panel runat="server" CssClass="col">
                                            <asp:Label ID="lblInfoConductor" runat="server" Text=""></asp:Label>
                                        </asp:Panel>
                                    </div>
                                </asp:Panel>

                                <asp:Panel runat="server" CssClass="col-12 col-lg-6" Style="border: 1px solid black; border-top: none;">
                                    <div class="row">
                                        <asp:Panel runat="server" CssClass="col-3" Style="border-right: 1px solid black;">
                                            <span>Código: </span>
                                        </asp:Panel>
                                        <asp:Panel runat="server" CssClass="col">
                                            <asp:Label ID="lblinfoCod" runat="server" Text=""></asp:Label>
                                        </asp:Panel>
                                    </div>
                                </asp:Panel>
                            </div>
                        </div>

                        <asp:Panel runat="server" CssClass="col-12 sinPadding">
                            <asp:Panel ID="Panel1" runat="server" CssClass="row">
                            </asp:Panel>
                        </asp:Panel>
                    </div>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <script>
        function quitarPadding() {
            let doc = document.getElementById('container');
            doc.removeAttribute('style');
        }
        document.addEventListener('load', quitarPadding);
        window.addEventListener('load', quitarPadding);
    </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="scripts_css" runat="server">

    <style>
        .pnl_table {
            border-radius: 5px;
        }
        iframe {
            padding: 50px 0px 0px 0px;
            width: 100%;
            height: 600px;
            border: none;
            border-radius: 5px;
        }
    </style>

</asp:Content>

