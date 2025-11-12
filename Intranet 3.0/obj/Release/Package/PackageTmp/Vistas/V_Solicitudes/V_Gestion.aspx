<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="V_Gestion.aspx.cs" Inherits="Intranet_3._0.Vistas.V_Solicitudes.V_Gestion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="scripts_css" runat="server">
    <style>
        .block__input {
            pointer-events: none;
            background-color: hsl(210, 8%, 95%)
        }

        .hide {
            display: none;
        }

        .accepted {
            border: 1px solid rgba(22, 160, 133, 1) !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="scripts_js" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <section class="pnl_table">
                <div class="pnl_tag">
                    <p><i class="fas fa-tag"></i>Tabla de Solicitudes de dias</p>
                </div>
                <div class="filter">
                    <div class="box_menu_crear">
                        <button                            
                            runat="server"
                            ID="txt_filter_grupo"
                            AutoComplete="on"
                            AutoPostBack="true">
                            <i class="fas fa-history"></i>Recargar
                        </button>
                        <button
                            type="button"
                            id="btn_crear_publicacion"
                            class="btn-modal"
                            data-id="modal_crear_noticia">
                            <i class="fas fa-plus"></i>Gestionar Solicitud
                        </button>
                    </div>
                    <%--Diego Cordoba: Cada vez que se oprime enter dentro de la búsqueda, me carga los datos--%>
                    <%--<div class="box_search">
                        <i class="fas fa-search"></i>
                        <asp:TextBox
                            runat="server"
                            ID="txt_filter_grupo"
                            AutoComplete="off"
                            AutoPostBack="true"
                            placeholder="Búsqueda rápida"></asp:TextBox>
                    </div>--%>
                </div>
                <table
                    runat="server"
                    id="tbl_grupos"
                    class="tbl_vistas_general">
                </table>
            </section>
        </ContentTemplate>
    </asp:UpdatePanel>

    <!--modales-->
    <!--GRUPOS-->
    <!--MODAL GRUPOS-->
    <div
        class="modal-i-gl modal-i-gl-hide animated fadeIn"
        id="modal_crear_noticia">
        <div class="modal-i-gl-body">
            <div class="modal-i-gl-title">
                <h1 class="title">Gestionar Solicitud</h1>
                <div class="modal-i-gl-cerrar">
                    <button type="button" class="btn-modal-close">
                        <i class="fas fa-times"></i>
                    </button>
                </div>
            </div>
            <div class="modal-i-gl-content">
                <!--Aquí el contenido-->
                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <section class="box_content_crear_vista">
                             <div class="content row">
                                <div class="pnl_input col" style="width:fit-content">
                                    <p><center><b>Fecha y hora de creación</b></center></p><hr/>
                                    <asp:TextBox runat="server" ID="FechaCreacion" name="RadioButton" CssClass="block__input"></asp:TextBox>
                                </div>
                                <div class="pnl_input col" style="width:fit-content">
                                    <p><center><b>Código Sae</b></center></p><hr/>
                                    <asp:TextBox runat="server" ID="Codigo_Sae" name="RadioButton" CssClass="block__input"></asp:TextBox>
                                </div>
                            </div>
                            <div class="content row"> 
                                <div class="pnl_input col">
                                    <p><center><b>Nombre del empleado</b></center></p><hr/>
                                    <asp:TextBox runat="server" ID="NombreEmpleado" name="RadioButton" CssClass="block__input"></asp:TextBox>
                                </div>
                            </div>
                            <div class="content row">
                                <div class="pnl_input col" style="width:fit-content">
                                    <p><center><b>Número del cédula</b></center></p><hr/>
                                    <asp:TextBox runat="server" ID="NumeroCedula" name="RadioButton" CssClass="block__input"></asp:TextBox>
                                </div>
                                <div class="pnl_input col" style="width:fit-content">
                                    <p><center><b>Cargo</b></center></p><hr/>
                                    <asp:TextBox runat="server" ID="Cargo" name="RadioButton" CssClass="block__input"></asp:TextBox>
                                </div>
                            </div>
                            <div class="content row">
                                <div class="pnl_input col">
                                    <p><center><b>Tipo de solicitud</b></center></p><hr/>
                                    <asp:DropDownList runat="server" ID="ddl_tipoSolicitud" name="RadioButton" CssClass="block__input"></asp:DropDownList>
                                    <input runat="server" type="hidden" id="tipoSolicitud" />
                                </div>
                            </div>
                            <div class="content row accepted__date">          
                                <div class="pnl_input col">
                                    <p><center><b>Fecha tentativa 1</b></center></p><hr/>
                                    <asp:TextBox runat="server" ID="FechaTentativa1" type="date" CssClass="block__input " />
                                </div>
                                <div class="pnl_input col">
                                     <p><center><b>Fecha tentativa 2</b></center></p><hr/>
                                    <asp:TextBox runat="server" ID="FechaTentativa2" type="date" CssClass="block__input " />
                                </div>
                                <div class="pnl_input col">
                                     <p><center><b>Fecha tentativa 3</b></center></p><hr/>
                                    <asp:TextBox runat="server" ID="FechaTentativa3" type="date" CssClass="block__input " />
                                </div>
                                <input runat="server" type="hidden" id="fechaAceptada" />
                            </div>
                            <div id="observacionAprovacion" class="content row hide">
                                <div class="pnl_input col">
                                     <p><center><b>Observaciones</b></center></p><hr/>
                                    <asp:TextBox runat="server" ID="Observaciones" />
                                </div>
                            </div>
                           <div class="content row">                         
                            <div id="solicitudGestionada">
                                <asp:LinkButton runat="server" OnClientClick="return confirm('¿Está seguro de ACEPTAR esta solicitud?');"  Text="Button"  ID="LnkAceptar" OnClick="LnkAceptar_Click">Aceptar</asp:LinkButton>
                                <asp:LinkButton runat="server" OnClientClick="return confirm('¿Está seguro de RECHAZAR esta solicitud?');" Text="Button"  ID="LnkRechazar" OnClick="LnkRechazar_Click">Rechazar</asp:LinkButton>
                            </div>
                               <div id="solicitudGestionar">
                                <asp:LinkButton runat="server" OnClientClick="return confirm('¿Está seguro de cambiar el estado de esta solicitud a EN PROCESO?');" Text="Button" ID="LnkGestionar" OnClick="LnkGestionar_Click">En proceso</asp:LinkButton>
                            </div>
                           </div>
                        </section>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <script defer>
        Sys.Application.add_load(() => {
            //Variables de inputs
            const ddlTipoSolicitud = document.querySelector('#MainContent_ddl_tipoSolicitud');
            const tipoSolicitud = document.querySelector('#MainContent_tipoSolicitud');
            const fechaTentativa1 = document.querySelector('#MainContent_FechaTentativa1');
            const fechaTentativa2 = document.querySelector('#MainContent_FechaTentativa2');
            const fechaTentativa3 = document.querySelector('#MainContent_FechaTentativa3');
            const observaciones = document.querySelector('#MainContent_Observaciones');
            const checks = [...document.querySelectorAll('input[name="rd_estado_vista"]')];
            const accepted__date = document.querySelector('.accepted__date');
            const fechaAceptada = document.querySelector('#MainContent_fechaAceptada');

            //VAriables campos nuevos
            const NombreEmpleado = document.querySelector('#MainContent_NombreEmpleado');
            const NumeroCedula = document.querySelector('#MainContent_NumeroCedula');
            const Cargo = document.querySelector('#MainContent_Cargo');
            const Codigo_Sae = document.querySelector('#MainContent_Codigo_Sae');
            const FechaCreacion = document.querySelector('#MainContent_FechaCreacion');

            //Variables de botones
            const btnCrearPublicacion = document.querySelector('#btn_crear_publicacion');
            const LnkAceptar = document.querySelector('#LnkAceptar');
            const LnkRechazar = document.querySelector('#LnkRechazar');
            const LnkGestionar = document.querySelector('#LnkGestionar');

            //Variables de divs
            const solicitudGestionar = document.querySelector('#solicitudGestionar');
            const solicitudGestionada = document.querySelector('#solicitudGestionada');
            const observacionAprovacion = document.querySelector('#observacionAprovacion');


            //Ejecución de eventos
            btnCrearPublicacion.addEventListener('click', () => {
                checks.forEach(async check => {
                    if (check.checked) {
                        let estado = check.parentNode.parentNode.childNodes[21].textContent.trim();
                        console.log(estado);
                        if (estado === 'Abierta') {
                            solicitudGestionada.classList.remove('hide');
                            observacionAprovacion.classList.remove('hide');
                        }
                        if (estado === 'En Proceso') {
                            solicitudGestionar.classList.remove('hide');
                            solicitudGestionada.classList.remove('hide');
                            observacionAprovacion.classList.remove('hide');
                        }
                        if (estado === 'Aceptada' || estado === 'Rechazada' || estado === 'Anulada') {
                            solicitudGestionar.classList.add('hide');
                            solicitudGestionada.classList.add('hide');
                            observacionAprovacion.classList.add('hide');
                        }
                        const rest = await fetch(`WebService_V_Solicitudes.asmx/ObtenerDatosSolicitudActualizar`, {
                            method: 'POST',
                            headers: {
                                'Content-Type': 'application/json',
                            },
                            body: JSON.stringify({
                                'idSolicitud': check.value
                            })
                        });
                        const datos = await rest.json();
                        console.log(datos);
                        ddlTipoSolicitud.value = datos.d[0][1];
                        tipoSolicitud.value = datos.d[0][1];
                        fechaTentativa1.value = datos.d[0][2];
                        fechaTentativa2.value = datos.d[0][3];
                        fechaTentativa3.value = datos.d[0][4];
                        NombreEmpleado.value = datos.d[0][7];
                        NumeroCedula.value = datos.d[0][8];
                        Cargo.value = datos.d[0][9];
                        Codigo_Sae.value = datos.d[0][10];
                        FechaCreacion.value = datos.d[0][11];
                    }
                })
            });

            accepted__date.addEventListener('click', e => {
                if (e.target.classList.contains('pnl_input')) {
                    e.target.classList.toggle('accepted');
                    fechaAceptada.value = e.target.childNodes[6].value;
                    

                    let inputs = [...e.composedPath()[1].childNodes];
                    let inputOcultar = inputs.filter(input => input.tagName === 'DIV' && !input.classList.contains('accepted'))
                    inputOcultar.forEach(input => {
                        input.classList.add('hide')
                    });

                    let inputMostrar = inputs.filter(input => input.tagName === 'DIV' && !input.classList.contains('accepted'));
                    if (inputMostrar.length === inputs.filter(input => input.tagName === 'DIV').length) {
                        inputMostrar.forEach(input => {
                            input.classList.remove('hide')
                        });
                    }
                }
            })  
        });
    </script>
</asp:Content>
