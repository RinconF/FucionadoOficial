<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="V_DiaCumpleaños.aspx.cs" Inherits="Intranet_3._0.Vistas.V_Solicitudes.V_DiaCumpleaños" %>

<asp:Content ID="Content1" ContentPlaceHolderID="scripts_css" runat="server">
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
                            type="button"
                            id="btn_crear_publicacion"
                            class="btn-modal"
                            data-id="modal_crear_noticia">
                            <i class="fas fa-plus"></i>Nueva solicitud
                        </button>
                        <%--Diego Córdoba: Se inactiva este botón para una primer fase, la siguiente fase se vuelve a activar--%>
                        <button hidden                            
                            disabled="true"
                            type="button"
                            id="btn_actualizar_grupo"
                            class="btn-modal"
                            data-id="modal_actualizar_grupo">
                            <i class="fas fa-cog"></i>Actualizar solicitud
                        </button>
                        <button
                            type="button"
                            id="btn_info_solicitud"
                            class="btn-modal"
                            data-id="modal_info_solicitud">
                            <i class="fa fa-user-alt"></i>Información solicitud
                        </button>
        
                    </div>
                    <div class="box_search">
                        <i class="fas fa-search"></i>
                        <asp:TextBox
                            runat="server"
                            ID="txt_filter_grupo"
                            AutoComplete="off"
                            AutoPostBack="true"
                            placeholder="Búsqueda rápida"></asp:TextBox>
                    </div>
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
                <h1 class="title">Crear nueva solicitud</h1>
                <div class="modal-i-gl-cerrar">
                    <button type="button" class="btn-modal-close">
                        <i class="fas fa-times"></i>
                    </button>
                </div>
            </div>
            <div class="modal-i-gl-content">
                <!--Aquí el contenido-->
                <section class="box_content_crear_vista">
                    <div class="content row">
                        <div class="pnl_input col">
                            <p><center><b>Tipo de solicitud</b></center></p><hr/>
                            <asp:DropDownList runat="server" ID="ddl_tipoSolicitud"></asp:DropDownList>
                            <input runat="server" type="hidden" id="tipoSolicitud" />
                        </div>
                    </div>
                    <div class="content row">
                        <div class="pnl_input col">
                            <p><center><b>Fecha tentativa 1</b></center></p><hr/>
                            <asp:TextBox runat="server" ID="FechaTentativa1" type="date" max="2022-11-26"/>
                        </div>
                        <div class="pnl_input col">
                            <p><center><b>Fecha tentativa 2</b></center></p><hr/>
                            <asp:TextBox runat="server" ID="FechaTentativa2" type="date" max="2022-11-26"/>
                        </div>
                        <div class="pnl_input col">
                            <p><center><b>Fecha tentativa 3</b></center></p><hr/>
                            <asp:TextBox runat="server" ID="FechaTentativa3" type="date" max="2022-11-26" />
                        </div>                        
                    </div>
                    <asp:LinkButton runat="server" ID="LnkCrear" OnClick="LnkCrear_Click">CREAR</asp:LinkButton>                
                </section>
            </div>
        </div>
    </div>

    <!--MODAL ACTUALIZAR GRUPOS-->
    <div
        class="modal-i-gl modal-i-gl-hide animated fadeIn"
        id="modal_actualizar_grupo">
        <div class="modal-i-gl-body">
            <div class="modal-i-gl-title">
                <h1 class="title">Actualizar datos solicitud</h1>
                <div class="modal-i-gl-cerrar">
                    <button type="button" class="btn-modal-close">
                        <i class="fas fa-times"></i>
                    </button>
                </div>
            </div>
            <div class="modal-i-gl-content">
                <!--Aquí el contenido-->
                <section class="box_content_crear_vista">
                    <div class="content row">
                        <div class="pnl_input col">
                            <p><center><b>Tipo de solicitud</b></center></p><hr/>
                            <asp:DropDownList runat="server" ID="ddl_tipoSolicitudActualizar" name="RadioButton"></asp:DropDownList>
                            <input runat="server" type="hidden" id="tipoSolicitudActualizar" />
                        </div>
                    </div>
                    <div class="content row">
                        <div class="pnl_input col">
                            <p><center><b>Fecha tentativa 1</b></center></p><hr/>
                            <asp:TextBox runat="server" ID="FechaTentativa1Actualizar" type="date" />
                        </div>
                        <div class="pnl_input col">
                            <p><center><b>Fecha tentativa 2</b></center></p><hr/>
                            <asp:TextBox runat="server" ID="FechaTentativa2Actualizar" type="date" />
                        </div>
                        <div class="pnl_input col">
                            <p><center><b>Fecha tentativa 3</b></center></p><hr/>
                            <asp:TextBox runat="server" ID="FechaTentativa3Actualizar" type="date" />
                        </div>                       
                    </div>
                    <div id="ActualizarSolitud">
                    <asp:LinkButton runat="server" ID="LnkActualizar" OnClick="LnkActualizar_Click" >ACTUALIZAR</asp:LinkButton>
                        <!--El botón se inhabilita-->
                    <asp:LinkButton runat="server" ID="LnkAnular" OnClick="LnkAnular_Click" Visible="false">ANULAR</asp:LinkButton>
                        </div>
                </section>
            </div>
        </div>
    </div>


    <!--MODAL VISUALIZAR INFORMACIÓN-->
    <div
        class="modal-i-gl modal-i-gl-hide animated fadeIn"
        id="modal_info_solicitud">
        <div class="modal-i-gl-body">
            <div class="modal-i-gl-title">
                <h1 class="title">Información solicitud</h1>
                <div class="modal-i-gl-cerrar">
                    <button type="button" class="btn-modal-close">
                        <i class="fas fa-times"></i>
                    </button>
                </div>
            </div>
            <div class="modal-i-gl-content">
                <!--Aquí el contenido-->
                <section class="box_content_crear_vista">
                    <div class="content row">
                        <div class="pnl_input col">
                            <p><center><b>Tipo de solicitud</b></center></p><hr/>
                            <asp:TextBox runat="server" ID="TipoDeSolicitud" type="txt" Enabled="false"></asp:TextBox>
                            <input runat="server" type="hidden" id="TipoDeSolicitudinf" />
                        </div>
                    </div>
                    <div class="content row">
                        <div class="pnl_input col">
                            <p><center><b>Fecha aprobada</b></center></p><hr/>
                            <asp:TextBox runat="server" ID="FechaAprovada" type="date"  Enabled="false"/>
                        </div>
                        <div class="pnl_input col">
                            <p><center><b style ="color:darkred">Estado de solicitud</b></center></p><hr/>
                            <asp:TextBox runat="server" ID="EstadoSolicitudInfo" type="txt" Enabled="false"/>
                        </div>
                    </div>
                    <div id="observacionAprovacion" class="content row hide">
                             <div class="pnl_input col">
                                 <p><center><b>Observación</b></center></p><hr/>
                                  <asp:TextBox runat="server" ID="Observaciones" Enabled="false"  />                                
                             </div>
                        
                        </div>
                </section>
            </div>
        </div>
    </div>


    <%--MODAL VERIFICACION DE EXISTENCIA DE CAMPOS VACIOS--%>
    <div class="modal-i-gl modal-i-gl-hide animated fadeIn" id="modal_verificacion_existe">
        <div class="modal-i-gl-body-small">
            <div class="modal-i-gl-title">
                <h1 class="title">Ya existe un registro con el mismo numero de documento ¿Desea actualizar la informacion?</h1>
                <div class="modal-i-gl-cerrar">
                    <button type="button" class="btn-modal-close"><i class="fas fa-times"></i></button>
                </div>
            </div>
            <div class="modal-i-gl-content">

                <!--Aquí el contenido-->
                <asp:LinkButton runat="server" ID="LinkButton1"><i class="fas fa-check"></i> Agregar</asp:LinkButton>
                <button type="button" class="btn-modal-close"><i class="fas fa-times"></i>Cancelar</button>
            </div>
        </div>
    </div>

    <script defer>
        Sys.Application.add_load(() => {
            //Variables para uso local
            const params = new URLSearchParams(location.search);
            const Id_Usuario = params.get('Id_Usuario');
            let diaCumpleaños = '';
            const diaSolicitud = new Date();

            //Variables de inputs
            const ddl_tipoSolicitud = document.querySelector('#MainContent_ddl_tipoSolicitud');
            const FechaTentativa1 = document.querySelector('#MainContent_FechaTentativa1');
            const FechaTentativa2 = document.querySelector('#MainContent_FechaTentativa2');
            const FechaTentativa3 = document.querySelector('#MainContent_FechaTentativa3');
            const tipoSolicitud = document.querySelector('#MainContent_tipoSolicitud');
            const tablas = [...document.querySelectorAll('input[name="rd_estado_vista"]')];

            const ddl_tipoSolicitudActualizar = document.querySelector('#MainContent_ddl_tipoSolicitudActualizar');
            const FechaTentativa1Actualizar = document.querySelector('#MainContent_FechaTentativa1Actualizar');
            const FechaTentativa2Actualizar = document.querySelector('#MainContent_FechaTentativa2Actualizar');
            const FechaTentativa3Actualizar = document.querySelector('#MainContent_FechaTentativa3Actualizar');
            const tipoSolicitudActualizar = document.querySelector('#MainContent_tipoSolicitudActualizar');

            const TipoDeSolicitud = document.querySelector('#MainContent_TipoDeSolicitud');
            const FechaAprovada = document.querySelector('#MainContent_FechaAprovada');
            const EstadoSolicitudInfo = document.querySelector('#MainContent_EstadoSolicitudInfo');
            const Observaciones = document.querySelector('#MainContent_Observaciones');
            const TipoDeSolicitudinf = document.querySelector('#MainContent_TipoDeSolicitudinf');

            //Variables de botones
            const lnkCrear = document.querySelector('#MainContent_LnkCrear');
            const btn_actualizar_grupo = document.querySelector('#btn_actualizar_grupo');
            const LnkActualizar = document.querySelector('#MainContent_LnkActualizar');
            const LnkAnular = document.querySelector('#MainContent_LnkAnular');
            const btn_info_solicitud = document.querySelector('#btn_info_solicitud');
            

            //Creacion de funciones
            const validationEdad = async () => {
                const rest = await fetch(`WebService_V_Solicitudes.asmx/ObtenerFechaNacimiento`, {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                    },
                    body: JSON.stringify({
                        Id_Usuario
                    })
                })
                const datos = await rest.json()
                diaCumpleaños = datos.d;
            }

            const fechaInicio = (fechaHoy = new Date, input = FechaTentativa1) => {
                input.setAttribute('min', fechaHoy.toJSON().split("T")[0]); 
            }
            


            const fechaSabado = e => {
                let hoy = new Date(e.target.value).getUTCDay();
                if (hoy !== 6) {
                    e.preventDefault();
                    e.target.value = '';
                    mensajeDiasSabados();
                    return;
                }               
            }

            //Mensaje de alerta para campos del modal vacío
            const mensajeCamposVacios = () => {
                $('.modal-noti').addClass('modal-noti-show');//agregar
                $('.modal-noti').removeClass('modal-noti-hide');//quitar
                $('.body-noti').addClass('advert'); //tipo notificación
                $('.title-noti').html('<span class="far fa-clock"></span> Campos Vacios');//título
                $('.content-noti').html('¡Existen campos sin diligenciar!');//mensaje
                setTimeout(function () {
                    $('.modal-noti').addClass('modal-noti-hide');
                    $('.modal-noti').removeClass('modal-noti-show');
                }, 4000);
            }
            //Mensaje de alerta para la actualización de la solicitud
            const mensajeActualizarSolicitud = () => {
                $('.modal-noti').addClass('modal-noti-show');//agregar
                $('.modal-noti').removeClass('modal-noti-hide');//quitar
                $('.body-noti').addClass('advert'); //tipo notificación
                $('.title-noti').html('<span class="far fa-clock"></span> Información de la solicitud');//título
                $('.content-noti').html('¡La solicitud ya no puede ser actualizada!');//mensaje
                setTimeout(function () {
                    $('.modal-noti').addClass('modal-noti-hide');
                    $('.modal-noti').removeClass('modal-noti-show');
                }, 4000);
            }
            //Mensaje de alerta para la actualización de la solicitud
            const mensajeFechasIguales = () => {
                $('.modal-noti').addClass('modal-noti-show');//agregar
                $('.modal-noti').removeClass('modal-noti-hide');//quitar
                $('.body-noti').addClass('advert'); //tipo notificación
                $('.title-noti').html('<span class="far fa-clock"></span> Fechas iguales');//título
                $('.content-noti').html('¡Existen fechas iguales, por favor corrija e intente nuevamente!');//mensaje
                setTimeout(function () {
                    $('.modal-noti').addClass('modal-noti-hide');
                    $('.modal-noti').removeClass('modal-noti-show');
                }, 4000);
            }

            //Mensaje para dias que no sean sábados
            const mensajeDiasSabados = () => {
                $('.modal-noti').addClass('modal-noti-show');//agregar
                $('.modal-noti').removeClass('modal-noti-hide');//quitar
                $('.body-noti').addClass('advert'); //tipo notificación
                $('.title-noti').html('<span class="far fa-clock"></span> Error');//título
                $('.content-noti').html('Por favor, seleccione únicamente días sábados.');//mensaje
                setTimeout(function () {
                    $('.modal-noti').addClass('modal-noti-hide');
                    $('.modal-noti').removeClass('modal-noti-show');
                }, 4000);
            }



            //Ejecucion de Eventos
            ddl_tipoSolicitud.addEventListener('change', () => {
                tipoSolicitud.value = ddl_tipoSolicitud.options[ddl_tipoSolicitud.selectedIndex].value;
            })

            FechaTentativa1.addEventListener('change', e => {
                fechaSabado(e);
                fechaInicio(new Date(e.target.value), FechaTentativa2);
                fechaInicio(new Date(e.target.value), FechaTentativa3);
               
            });

            FechaTentativa2.addEventListener('change', e => {
                fechaSabado(e);
                fechaInicio(new Date(e.target.value), FechaTentativa3);
            });

            FechaTentativa3.addEventListener('change', e => {
                fechaSabado(e);
            })

            $(lnkCrear).on('click', () => {
                if (ddl_tipoSolicitud.value === '0' || FechaTentativa1.value === '' || FechaTentativa2.value === '' || FechaTentativa3.value === '' || ddl_tipoSolicitud.value === '') {
                    //notificación
                    mensajeCamposVacios();
                    return false;
                } else {

                    if (FechaTentativa1.value === FechaTentativa2.value || FechaTentativa1.value === FechaTentativa3.value || FechaTentativa2.value === FechaTentativa1.value || FechaTentativa2.value === FechaTentativa3.value || FechaTentativa3.value === FechaTentativa1.value || FechaTentativa3.value === FechaTentativa2.value) {
                        mensajeFechasIguales();
                        return false;
                    }
                }

                //Implementar mensaje sí hay una una solicitud abierta con el mismo tipo

            })

            //Evento para el botón actualizar solicitud
                btn_actualizar_grupo.addEventListener('click', () => {
                    tablas.forEach(async tabla => {
                        if (tabla.checked) {
                                const rest = await fetch(`WebService_V_Solicitudes.asmx/ObtenerDatosSolicitudActualizar`, {
                                    method: 'POST',
                                    headers: {
                                        'Content-Type': 'application/json',
                                    },
                                    body: JSON.stringify({
                                        'idSolicitud': tabla.value
                                    })
                                });
                                const datos = await rest.json();
                                console.log(datos.d);
                                ddl_tipoSolicitudActualizar.value = datos.d[0][1];
                                tipoSolicitudActualizar.value = datos.d[0][1];
                                FechaTentativa1Actualizar.value = datos.d[0][2];
                                FechaTentativa2Actualizar.value = datos.d[0][3];
                                FechaTentativa3Actualizar.value = datos.d[0][4];
                                EstadoSolicitud = datos.d[0][6];

                                fechaInicio(new Date(datos.d[0][2]), FechaTentativa1Actualizar);
                                 if (EstadoSolicitud != 'Abierta') {
                                    FechaTentativa3Actualizar.disabled = true;
                                    FechaTentativa2Actualizar.disabled = true;
                                    FechaTentativa1Actualizar.disabled = true;
                                    ddl_tipoSolicitudActualizar.disabled = true;
                                    
                                     //Notificación
                                     mensajeActualizarSolicitud();
                                     return false;
                                 }
                                 else {
                                    FechaTentativa3Actualizar.disabled = false;
                                    FechaTentativa2Actualizar.disabled = false;
                                    FechaTentativa1Actualizar.disabled = false;
                                    ddl_tipoSolicitudActualizar.disabled = false;
                                    
                                 } 
                            
                        }

                    })
                })
            
            ddl_tipoSolicitudActualizar.addEventListener('change', async () => {
                tipoSolicitudActualizar.value = ddl_tipoSolicitudActualizar.options[ddl_tipoSolicitudActualizar.selectedIndex].value;
            })

            FechaTentativa1Actualizar.addEventListener('change', e => {
                fechaSabado(e);
                fechaInicio(new Date(e.target.value), FechaTentativa2Actualizar);
                fechaInicio(new Date(e.target.value), FechaTentativa3Actualizar);
            });

            FechaTentativa2Actualizar.addEventListener('change', e => {
                fechaSabado(e);
                fechaInicio(new Date(e.target.value), FechaTentativa3Actualizar);
            });

            FechaTentativa3Actualizar.addEventListener('change', e => {
                fechaSabado(e);
            })

            $(LnkActualizar).on('click', () => {
                if (ddl_tipoSolicitudActualizar.value === '0' || FechaTentativa1Actualizar.value === '' || FechaTentativa2Actualizar.value === '' || FechaTentativa3Actualizar.value === '') {
                    //notificación
                    mensajeCamposVacios();
                    return false;
                }
            });

            validationEdad();
            fechaInicio();

            //Evento para el botón información solicitud
            btn_info_solicitud.addEventListener('click', () => {
                tablas.forEach(async tabla => {
                    if (tabla.checked) {
                        const rest = await fetch(`WebService_V_Solicitudes.asmx/ObtenerDatosSolicitud`, {
                            method: 'POST',
                            headers: {
                                'Content-Type': 'application/json',
                            },
                            body: JSON.stringify({
                                'idSolicitud': tabla.value
                            })
                        });
                        const datos = await rest.json();
                        console.log(datos.d);
                        TipoDeSolicitud.value = datos.d[0][1];
                        TipoDeSolicitudinf.value = datos.d[0][1];
                        FechaAprovada.value = datos.d[0][2];
                        EstadoSolicitudInfo.value = datos.d[0][4];
                        Observaciones.value = datos.d[0][3];
                       
                    
                    }
                })
            })
        });
    </script>
</asp:Content>
