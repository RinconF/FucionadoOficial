<%@ Page ValidateRequest="false" Title="Envío Desprendibles de Pago" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="V_EnvioDesprendiblesNomina.aspx.cs" Inherits="Intranet_3._0.Vistas.V_Financiera.V_EnvioDesprendiblesNomina" Async="True" %>

<asp:Content ID="Content1" ContentPlaceHolderID="scripts_css" runat="server">
    <link href="../../Styles/bootstrap/bootstrap.min.css" rel="stylesheet" />
    <link rel="Stylesheet" href="/Styles/css/desprendibles_pago/desprendibles_pago.css" />
    <link href="../../Styles/css/sweetalert2/sweetalert2.min.css" rel="stylesheet" />
    <script src="../../js/sweetalert2.min.js"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <div class="pnl_tag">
        <p><i class="fas fa-tag"></i>Envio de desprendibles de pago</p>
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
                    <asp:DropDownList ID="DropDownListMonth" runat="server" CssClass="form-select" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_SelectedIndexChanged" disabled="true">
                        <asp:ListItem Value="0" Text="Seleccione un mes" />
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
                    <asp:DropDownList ID="DropDownListYear" runat="server" CssClass="form-select" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_SelectedIndexChanged" disabled="true">
                        <asp:ListItem Value="0" Text="Seleccione un año" />
                    </asp:DropDownList>
                </div>
                <div class="col form-group">
                    <label for="periodo">Período</label>
                    <asp:DropDownList ID="DropDownListPeriod" runat="server" CssClass="form-select" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_SelectedIndexChanged">
                        <asp:ListItem Value="0" Text="Seleccione un período" />
                        <asp:ListItem Value="1">Quincena 1</asp:ListItem>
                        <asp:ListItem Value="2">Quincena 2</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col form-group" id="Seccion_Consulta" runat="server">
                    <asp:Button ID="btnConsultar" runat="server" Text="Crear" CssClass="btn btn-primary btn-spacing" OnClick="BtnConsultar_Click"  OnClientClick="return confirmConsultaSweetAlert();"  />
                    <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" OnClick="btnLimpiar_Click" CssClass="btn btn-secondary btn-spacing" />
                </div>
            </div>
        </div>
    </div>

    <!-- Progreso de Lotes -->
    <div id="Seccion_Lotes" runat="server" class="container mt-4">
        <h5>Gestión de lotes para envío de correos </h5>
        <p><strong>Mes:</strong> <span id="mesLote" runat="server"></span></p>
        <p><strong>Año:</strong> <span id="añoLote" runat="server"></span></p>
        <p><strong>Período:</strong> <span id="periodoLote" runat="server"></span></p>
        <p><strong>Total de lotes:</strong> <span id="totalLotes" runat="server"></span></p>

        <div class="col form-group">
            <label for="periodo">Por favor seleccione un lote: </label>
            <asp:DropDownList ID="DropDownListLote" runat="server" CssClass="form-select" OnSelectedIndexChanged="DropDownList_SelectedIndexChanged">
                <asp:ListItem Value="0" Text="Seleccione" />
            </asp:DropDownList>
            <asp:HiddenField ID="HiddenFieldLote" runat="server" />
        </div>

        <div id="Seccion_Botones" runat="server">
            <asp:Button ID="btnGenerar" runat="server" Text="Generar PDF" CssClass="btn btn-secondary btn-spacing" OnClick="ProcesarLotePDF" OnClientClick="return confirmarGeneracion();" />
            <asp:Button ID="btnEnviar" runat="server" Text="Enviar Correo" OnClick="btnEnviar_Click" CssClass="btn btn-success btn-spacing" OnClientClick="return confirmarEnvio();" />
            <asp:Button ID="Button1" runat="server" Text="Depurar" OnClick="btnDepurar_Lotes" CssClass="btn btn-danger btn-spacing" OnClientClick="return confirmarDepuracion();" />
        </div>
    </div>

    <script>

        function confirmarGeneracion() {
            // Mostrar el mensaje de confirmación
            const confirmation = confirm('¿Está seguro de generar los PDF´s?. Recuerde que no se puede salir hasta que el proceso termine.');
            if (confirmation) {
                // Si el usuario confirma, deshabilitar el dropdown y ocultar botones
                var dropdown = document.getElementById("<%= DropDownListLote.ClientID %>");
                var hiddenField = document.getElementById("<%= HiddenFieldLote.ClientID %>");
                var botonSeccion = document.getElementById("<%= Seccion_Botones.ClientID %>");
                var botonConsulta = document.getElementById("<%= Seccion_Consulta.ClientID %>");

                hiddenField.value = dropdown.value;
                dropdown.disabled = true;
                botonSeccion.style.display = "none";
                botonConsulta.style.display = "none";
                return true; // Permitir la ejecución del evento del servidor
            }
            return false; // Cancelar la ejecución del evento del servidor
        }

        function confirmarEnvio() {
            // Mostrar el mensaje de confirmación
            const confirmation = confirm('¿Está seguro de enviar los PDF´s?. Recuerde que no se puede salir hasta que el proceso termine.');
            if (confirmation) {
                // Si el usuario confirma, deshabilitar el dropdown y ocultar botones
                var dropdown = document.getElementById("<%= DropDownListLote.ClientID %>");
            var hiddenField = document.getElementById("<%= HiddenFieldLote.ClientID %>");
            var botonSeccion = document.getElementById("<%= Seccion_Botones.ClientID %>");
            var botonConsulta = document.getElementById("<%= Seccion_Consulta.ClientID %>");

                hiddenField.value = dropdown.value;
                dropdown.disabled = true;
                botonSeccion.style.display = "none";
                botonConsulta.style.display = "none";
                return true; // Permitir la ejecución del evento del servidor
            }
            return false; // Cancelar la ejecución del evento del servidor
        }

        function confirmarDepuracion() {
            // Mostrar el mensaje de confirmación
            const confirmation = confirm('¿Está seguro de depurar los lotes?. Recuerde que al depurarlos, perderá todos los archivos PDFs generados y los lotes.');
            if (confirmation) {
                // Si el usuario confirma, deshabilitar el dropdown y ocultar botones
                var dropdown = document.getElementById("<%= DropDownListLote.ClientID %>");
        var hiddenField = document.getElementById("<%= HiddenFieldLote.ClientID %>");
        var botonSeccion = document.getElementById("<%= Seccion_Botones.ClientID %>");
                var botonConsulta = document.getElementById("<%= Seccion_Consulta.ClientID %>");

                hiddenField.value = dropdown.value;
                dropdown.disabled = true;
                botonSeccion.style.display = "none";
                botonConsulta.style.display = "none";
                return true; // Permitir la ejecución del evento del servidor
            }
            return false; // Cancelar la ejecución del evento del servidor
        }


        function confirmConsultaSweetAlert() {
            var dropdownPeriod = document.getElementById("<%= DropDownListPeriod.ClientID %>");
            var botonConsulta = document.getElementById("<%= Seccion_Consulta.ClientID %>");
        
            Swal.fire({
                title: '¿Está seguro de crear la estructura?',
                text: "Recuerde que esta acción creará la estructura para la gestión de los desprendibles. Por favor, verifique bien las fechas antes de proceder.",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Sí, crear',
                cancelButtonText: 'Cancelar'
            }).then((result) => {
                if (result.isConfirmed) {
                    // Bloquear el DropDownListPeriod
                    dropdownPeriod.disabled = true;
        
                    // Ocultar los botones
                    botonConsulta.style.display = 'none';
        
                    // Ejecutar la acción (postback)
                    __doPostBack('<%= btnConsultar.UniqueID %>', '');
                } 
            });
        
            return false; // Evitar el postback automático.
        }


        function enableDropdown() {
            document.getElementById("<%= DropDownListLote.ClientID %>").disabled = false;
        }
    </script>

</asp:Content>
