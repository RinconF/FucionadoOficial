<%@ Page ValidateRequest="false" Title="Módulos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="V_Crear_Modulo.aspx.cs" Inherits="Intranet_3._0.Vistas.V_Control_General.V_Crear_Modulo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="scripts_css" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="scripts_js" runat="server">
    <script>
        $(document).ready(function () {
            ////////////Funciones de grupos////////////////
            //modal actualizar
            $('body').on('click', '.btn-actu-grupo', function () {

                if ($("#MainContent_tbl_grupos input[name='rd_estado_grupo']:radio").is(':checked')) {
                    $('#modal_actualizar_grupo').addClass('modal-i-gl-show');
                    $('#modal_actualizar_grupo').removeClass('modal-i-gl-hide');


                    $.ajax({
                        type: "POST",
                        url: "WebService_V_Control_General.asmx/cargar_datos_modal_actualizar_grupo",
                        data: '{"Id_Grupo": "' + $('input:radio[name=rd_estado_grupo]:checked').val() + '"}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (resultado) {
                            let items = resultado.d;
                            $.each(items, function (index, item) {

                                $("#MainContent_txt_grupo_actu").val(item[1]);
                                $("#MainContent_txt_drop_estado_grupo").val(item[2]);
                                $("#MainContent_txt_grupo_descrip_actu").val(item[3]);
                                $("#MainContent_txt_grupo_icono_actu").val(item[4]);
                            });
                        }
                    });

                } else {
                    $('#modal_actualizar_grupo').addClass('modal-i-gl-hide');
                    $('#modal_actualizar_grupo').removeClass('modal-i-gl-show');

                    //notificación
                    $('.modal-noti').addClass('modal-noti-show');//agregar
                    $('.modal-noti').removeClass('modal-noti-hide');//quitar
                    $('.body-noti').addClass('advert'); //tipo notificación
                    $('.title-noti').html('<span class="fas fa-exclamation-circle"></span> Validación advertencia');//título
                    $('.content-noti').html('Seleccione un grupo para validar.');//mensaje
                    setTimeout(function () {
                        $(".modal-noti").addClass("modal-noti-hide");
                        $(".modal-noti").removeClass("modal-noti-show");
                    }, 4000);
                }

            });

            //modal_reasignar
            $('body').on('click', '.btn-reasig-grupo', function () {

                if ($("#MainContent_tbl_grupos input[name='rd_estado_grupo']:radio").is(':checked')) {
                    $('#modal_reasignar_grupo').addClass('modal-i-gl-show');
                    $('#modal_reasignar_grupo').removeClass('modal-i-gl-hide');

                    $.ajax({
                        type: "POST",
                        url: "WebService_V_Control_General.asmx/cargar_tabla_rol_grupo",
                        data: '{"Id_Grupo": "' + $('input:radio[name=rd_estado_grupo]:checked').val() + '"}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (resultado) {
                            let items = resultado.d;
                            $("#MainContent_tbl_grupo_rol_asignado").find("tr:gt(0)").remove();
                            $.each(items, function (index, item) {
                                $("#MainContent_txt_nombre_grupo_reasig").val(item[3]);

                                $("#MainContent_tbl_grupo_rol_asignado").append(
                                    "<tr>" +
                                    "<td>" + item[0] + "</td>" +
                                    "<td>" + item[1] + "</td>" +
                                    "<td>" + item[2] + "</td>" +
                                    "</tr>"
                                );
                            });
                        }
                    });

                } else {
                    $('#modal_actualizar_grupo').addClass('modal-i-gl-hide');
                    $('#modal_actualizar_grupo').removeClass('modal-i-gl-show');

                    //notificación
                    $('.modal-noti').addClass('modal-noti-show');//agregar
                    $('.modal-noti').removeClass('modal-noti-hide');//quitar
                    $('.body-noti').addClass('advert'); //tipo notificación
                    $('.title-noti').html('<span class="fas fa-exclamation-circle"></span> Validación advertencia');//título
                    $('.content-noti').html('Seleccione un grupo para validar.');//mensaje
                    setTimeout(function () {
                        $(".modal-noti").addClass("modal-noti-hide");
                        $(".modal-noti").removeClass("modal-noti-show");
                    }, 4000);
                }

            });

            //Estado grupos
            $("#MainContent_drop_grupos_inhabilitado").change(function () {
                $.ajax({
                    type: "POST",
                    url: "WebService_V_Control_General.asmx/cargar_estado_grupos",
                    data: '{"Id_Grupo": "' + $(this).val() + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (resultado) {
                        let items = resultado.d;
                        $.each(items, function (index, item) {

                            $("#MainContent_txt_estado_").val(item[0]);
                        });
                    }
                });
            });


            /////////////////funciones de vistas/////////////////
            $('body').on('click', '.btn-actu-vista', function () {

                if ($("#MainContent_tbl_vistas input[name='rd_estado_vista']:radio").is(':checked')) {
                    $('#modal_actualizar_vista').addClass('modal-i-gl-show');
                    $('#modal_actualizar_vista').removeClass('modal-i-gl-hide');

                    $.ajax({
                        type: "POST",
                        url: "WebService_V_Control_General.asmx/cargar_datos_modal_actualizar_vista",
                        data: '{"Id_Vista": "' + $('input:radio[name=rd_estado_vista]:checked').val() + '"}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (resultado) {
                            let items = resultado.d;
                            $.each(items, function (index, item) {

                                $("#MainContent_txt_vista_actu").val(item[1]);
                                $("#MainContent_txt_drop_estado_vista").val(item[2]);
                                $("#MainContent_txt_vista_descrip_actu").val(item[3]);
                                $("#MainContent_txt_vista_icono_actu").val(item[4]);
                                $("#MainContent_txt_ruta_act").val(item[5]);
                            });
                        }
                    });

                } else {
                    $('#modal_actualizar_vista').addClass('modal-i-gl-hide');
                    $('#modal_actualizar_vista').removeClass('modal-i-gl-show');

                    //notificación
                    $('.modal-noti').addClass('modal-noti-show');//agregar
                    $('.modal-noti').removeClass('modal-noti-hide');//quitar
                    $('.body-noti').addClass('advert'); //tipo notificación
                    $('.title-noti').html('<span class="fas fa-exclamation-circle"></span> Validación advertencia');//título
                    $('.content-noti').html('Seleccione una vista para validar.');//mensaje
                    setTimeout(function () {
                        $(".modal-noti").addClass("modal-noti-hide");
                        $(".modal-noti").removeClass("modal-noti-show");
                    }, 4000);
                }

            });

            //modal_reasignar VISTA A GRUPOS
            $('body').on('click', '.btn-reasig-vista', function () {

                if ($("#MainContent_tbl_vistas input[name='rd_estado_vista']:radio").is(':checked')) {
                    $('#modal_reasignar_vista').addClass('modal-i-gl-show');
                    $('#modal_reasignar_vista').removeClass('modal-i-gl-hide');

                    $.ajax({
                        type: "POST",
                        url: "WebService_V_Control_General.asmx/cargar_tabla_grupo_vista",
                        data: '{"Id_Vista": "' + $('input:radio[name=rd_estado_vista]:checked').val() + '"}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (resultado) {
                            let items = resultado.d;
                            $.each(items, function (index, item) {
                                $("#MainContent_txt_vista_reasig").val(item[3]);
                                $("#MainContent_tbl_grupos_vista").find("tr:gt(0)").remove();
                                $("#MainContent_tbl_grupos_vista").append(
                                    "<tr>" +
                                    "<td>" + item[0] + "</td>" +
                                    "<td>" + item[1] + "</td>" +
                                    "<td>" + item[2] + "</td>" +
                                    "</tr>"
                                );
                            });
                        }
                    });

                } else {
                    $('#modal_reasignar_vista').addClass('modal-i-gl-hide');
                    $('#modal_reasignar_vista').removeClass('modal-i-gl-show');

                    //notificación
                    $('.modal-noti').addClass('modal-noti-show');//agregar
                    $('.modal-noti').removeClass('modal-noti-hide');//quitar
                    $('.body-noti').addClass('advert'); //tipo notificación
                    $('.title-noti').html('<span class="fas fa-exclamation-circle"></span> Validación advertencia');//título
                    $('.content-noti').html('Seleccione una vista para validar.');//mensaje
                    setTimeout(function () {
                        $(".modal-noti").addClass("modal-noti-hide");
                        $(".modal-noti").removeClass("modal-noti-show");
                    }, 4000);
                }

            });

            //Estado VISTAS
            $("#MainContent_drop_estado_vista_re").change(function () {
                $.ajax({
                    type: "POST",
                    url: "WebService_V_Control_General.asmx/cargar_estado_vistas",
                    data: '{"Id_Vista": "' + $(this).val() + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (resultado) {
                        let items = resultado.d;
                        $.each(items, function (index, item) {

                            $("#MainContent_txt_estado_vist").val(item[0]);
                        });
                    }
                });
            });

            //asignar rol
            $("body").on("click", ".btn_asignar_rol", function () {
                $.ajax({
                    type: "POST",
                    url: "WebService_V_Control_General.asmx/asignar_roles",
                    data: '{"Id_Grupo": "' + $('input:radio[name=rd_estado_grupo]:checked').val() + '","Id_Rol": "' + $('#MainContent_drop_roles').val() + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (resultado) {
                        let items = resultado.d;
                    }
                });
            });
        });
    </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <section class="pnl_table">
                <div class="pnl_tag">
                    <p><i class="fas fa-tag"></i>Tabla de Grupos</p>
                </div>
                <div class="filter">
                    <div class="box_menu_crear">
                        <button type="button" id="btn_crear_grupo" class="btn-modal" data-id="modal_crear_grupo">
                            <i class="fas fa-plus"></i>Nuevo grupo
                        </button>
                        <button type="button" id="btn_actualizar_grupo" class="btn-actu-grupo" data-id="modal_actualizar_grupo">
                            <i class="fas fa-cog"></i>Actualizar grupo
                        </button>
                        <button type="button" id="btn_visibilidad_grupo" class="btn-reasig-grupo" data-id="modal_reasignar_grupo">
                            <i class="fas fa-cogs"></i>Reasignación grupo
                        </button>
                        <button type="button" runat="server" id="btn_estado_grupo" class="btn-modal modal-button" data-id="modal_estado_grupo">
                            <i class="fas fa-eye-slash"></i>Grupos inhabilitados
                        </button>
                    </div>
                    <div class="box_search">
                        <i class="fas fa-search"></i>
                        <asp:TextBox runat="server" ID="txt_filter_grupo" placeholder="Búsqueda rápida" OnTextChanged="txt_filter_grupo_TextChanged" AutoPostBack="true" AutoCompleteType="Disabled"></asp:TextBox>
                    </div>
                </div>
                <table runat="server" id="tbl_grupos" class="tbl_vistas_general"></table>
            </section>
            </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <hr class="hr" />

            <section class="pnl_table">
                <div class="pnl_tag">
                    <p><i class="fas fa-tag"></i>Tabla de vistas</p>
                </div>
                <div class="filter">
                    <div class="box_menu_crear">
                        <button type="button" id="btn_crear_vista" class="btn-modal" data-id="modal_crear_vista">
                            <i class="fas fa-plus"></i>Nueva vista
                        </button>
                        <button type="button" id="btn_actualizar_vista" class="btn-actu-vista" data-id="modal_actualizar_vista">
                            <i class="fas fa-cog"></i>Actualizar vista
                        </button>
                        <button type="button" id="btn_visibilidad_vista" class="btn-reasig-vista" data-id="modal_reasignar_vista">
                            <i class="fas fa-cogs"></i>Reasignación vista
                        </button>
                        <button type="button" runat="server" id="btn_estado_vista" class="btn-modal modal-button" data-id="modal_estado_vista">
                            <i class="fas fa-eye-slash"></i>Vistas inhabilitadas
                        </button>
                    </div>
                    <div class="box_search">
                        <i class="fas fa-search"></i>
                        <asp:TextBox runat="server" ID="txt_filter_vista" placeholder="Búsqueda rápida" OnTextChanged="txt_filter_vista_TextChanged" AutoPostBack="true" AutoCompleteType="Disabled"></asp:TextBox>
                    </div>
                </div>
                <table runat="server" id="tbl_vistas" class="tbl_vistas_general"></table>
            </section>
        </ContentTemplate>
    </asp:UpdatePanel>










    <!--modales-->
    <!--GRUPOS-->
    <!--MODAL GRUPOS-->
    <div class="modal-i-gl modal-i-gl-hide animated fadeIn" id="modal_crear_grupo">
        <div class="modal-i-gl-body">
            <div class="modal-i-gl-title">
                <h1 class="title">Crear nuevo grupo</h1>
                <div class="modal-i-gl-cerrar">
                    <button type="button" class="btn-modal-close"><i class="fas fa-times"></i></button>
                </div>
            </div>
            <div class="modal-i-gl-content">

                <!--Aquí el contenido-->
                <section class="box_content_crear_vista">
                    <div class="content row">
                        <div class="pnl_input col">
                            <i class="far fa-keyboard"></i>
                            <asp:TextBox runat="server" ID="txt_grupo" placeholder="NOMBRE NUEVO GRUPO"></asp:TextBox>
                        </div>

                        <div class="pnl_input col">
                            <asp:DropDownList runat="server" ID="drop_rol"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="content row">
                        <div class="pnl_input col">
                            <i class="fas fa-align-right"></i>
                            <asp:TextBox runat="server" ID="txt_grupo_descrip" placeholder="DESCRIPCIÓN BREVE GRUPO"></asp:TextBox>
                        </div>

                        <div class="pnl_input col">
                            <i class="fas fa-icons"></i>
                            <asp:TextBox runat="server" ID="txt_grupo_icono" placeholder="COPIAR ETIQUETA ICONO <i></i>"></asp:TextBox>
                        </div>
                    </div>
                    <asp:LinkButton runat="server" ID="lnk_crear_" OnClick="crear_nuevo_grupo">CREAR</asp:LinkButton>
                </section>

            </div>
        </div>
    </div>

    <!--MODAL ACTUALIZAR GRUPOS-->
    <div class="modal-i-gl modal-i-gl-hide animated fadeIn" id="modal_actualizar_grupo">
        <div class="modal-i-gl-body">
            <div class="modal-i-gl-title">
                <h1 class="title">Actualizar datos grupo</h1>
                <div class="modal-i-gl-cerrar">
                    <button type="button" class="btn-modal-close"><i class="fas fa-times"></i></button>
                </div>
            </div>
            <div class="modal-i-gl-content">

                <!--Aquí el contenido-->
                <section class="box_content_crear_vista">
                    <div class="content row">
                        <div class="pnl_input col">
                            <label style="position: absolute; margin-top: -20px; margin-left: 10px; font-size: 12px; font-weight: bold;">Nombre:</label>
                            <i class="far fa-keyboard"></i>
                            <asp:TextBox runat="server" ID="txt_grupo_actu" placeholder="NOMBRE GRUPO"></asp:TextBox>
                        </div>

                        <div class="col">
                            <label style="position: absolute; margin-top: -20px; margin-left: 10px; font-size: 12px; font-weight: bold;">Estado:</label>
                            <div class="row" style="margin: 0px;">
                                <div class="pnl_input col" style="margin: 0px; margin-right: 10px;">
                                    <i class="far fa-keyboard"></i>
                                    <asp:TextBox runat="server" ID="txt_drop_estado_grupo" placeholder="VALOR DROP" Enabled="false"></asp:TextBox>
                                </div>
                                <div class="pnl_input col" style="margin: 0px; max-width: max-content;">
                                    <asp:DropDownList runat="server" ID="drop_estado_grupo">
                                        <asp:ListItem Value="1">Activo</asp:ListItem>
                                        <asp:ListItem Value="0">Inactivo</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>

                    </div>
                    <div class="content row">
                        <div class="pnl_input col">
                            <label style="position: absolute; margin-top: -20px; margin-left: 10px; font-size: 12px; font-weight: bold;">Descripción:</label>
                            <i class="fas fa-align-right"></i>
                            <asp:TextBox runat="server" ID="txt_grupo_descrip_actu" placeholder="DESCRIPCIÓN BREVE GRUPO"></asp:TextBox>
                        </div>

                        <div class="pnl_input col">
                            <label style="position: absolute; margin-top: -20px; margin-left: 10px; font-size: 12px; font-weight: bold;">Icono:</label>
                            <i class="fas fa-icons"></i>
                            <asp:TextBox runat="server" ID="txt_grupo_icono_actu" placeholder="COPIAR ETIQUETA ICONO <i></i>"></asp:TextBox>
                        </div>
                    </div>
                    <asp:LinkButton runat="server" ID="lnk_actualizar_grupo" OnClick="actualizar_datos_grupo">ACTUALIZAR</asp:LinkButton>
                </section>

            </div>
        </div>
    </div>

    <!--MODAL REASIGNAR GRUPOS-->
    <div class="modal-i-gl modal-i-gl-hide animated fadeIn" id="modal_reasignar_grupo">
        <div class="modal-i-gl-body">
            <div class="modal-i-gl-title">
                <h1 class="title">Reasignar grupo</h1>
                <div class="modal-i-gl-cerrar">
                    <button type="button" class="btn-modal-close"><i class="fas fa-times"></i></button>
                </div>
            </div>
            <div class="modal-i-gl-content">

                <!--Aquí el contenido-->
                <section class="box_content_crear_vista">
                    <div class="content row">
                        <div class="pnl_input col">
                            <label style="position: absolute; margin-top: -20px; margin-left: 10px; font-size: 12px; font-weight: bold;">Nombre:</label>
                            <i class="far fa-keyboard"></i>
                            <asp:TextBox runat="server" ID="txt_nombre_grupo_reasig" placeholder="NOMBRE GRUPO" Enabled="false"></asp:TextBox>
                        </div>
                        <div class="pnl_input col">
                            <label style="position: absolute; margin-top: -20px; margin-left: 10px; font-size: 12px; font-weight: bold;">Roles:</label>
                            <asp:DropDownList runat="server" ID="drop_roles">
                                <asp:ListItem Value="1">Activo</asp:ListItem>
                                <asp:ListItem Value="0">Inactivo</asp:ListItem>
                            </asp:DropDownList>
                        </div>

                    </div>
                    <button class="btn_asignar_rol">Asignar</button>
                </section>

                <section class="pnl_table" style="border-radius: 5px; margin-top: 20px;">
                    <table runat="server" id="tbl_grupo_rol_asignado" class="tbl_vistas_general">
                        <thead>
                            <tr class="th">
                                <td>#</td>
                                <td>ROL</td>
                                <td>FECHA ASIGNACIÓN</td>
                            </tr>
                        </thead>
                    </table>
                </section>

            </div>
        </div>
    </div>

    <!--MODAL ESTADO GRUPOS-->
    <div class="modal-i-gl modal-i-gl-hide animated fadeIn" id="modal_estado_grupo">
        <div class="modal-i-gl-body">
            <div class="modal-i-gl-title">
                <h1 class="title">Grupos inhabilitados</h1>
                <div class="modal-i-gl-cerrar">
                    <button type="button" class="btn-modal-close"><i class="fas fa-times"></i></button>
                </div>
            </div>
            <div class="modal-i-gl-content">

                <!--Aquí el contenido-->
                <section class="box_content_crear_vista">
                    <div class="content row">
                        <div class="pnl_input col">
                            <label style="position: absolute; margin-top: -20px; margin-left: 10px; font-size: 12px; font-weight: bold;">Grupos:</label>
                            <select runat="server" id="drop_grupos_inhabilitado"></select>
                        </div>
                        <div class="pnl_input col">
                            <label style="position: absolute; margin-top: -20px; margin-left: 10px; font-size: 12px; font-weight: bold;">Estado:</label>
                            <i class="fas fa-eye-slash"></i>
                            <asp:TextBox runat="server" ID="txt_estado_" placeholder="ESTADO GRUPO" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                    <asp:LinkButton runat="server" ID="LinkButton1" OnClick="actualizar_estado_grupo">Habilitar</asp:LinkButton>
                </section>

            </div>
        </div>
    </div>


    <!--VISTAS-->
    <!--MODAL VISTAS-->
    <div class="modal-i-gl modal-i-gl-hide animated fadeIn" id="modal_crear_vista">
        <div class="modal-i-gl-body">
            <div class="modal-i-gl-title">
                <h1 class="title">Crear nueva vista</h1>
                <div class="modal-i-gl-cerrar">
                    <button type="button" class="btn-modal-close"><i class="fas fa-times"></i></button>
                </div>
            </div>
            <div class="modal-i-gl-content">

                <!--Aquí el contenido-->
                <section class="box_content_crear_vista">
                    <div class="content row">
                        <div class="pnl_input col">
                            <i class="far fa-keyboard"></i>
                            <asp:TextBox runat="server" ID="txt_vista" placeholder="NOMBRE NUEVO MÓDULO"></asp:TextBox>
                        </div>

                        <div class="pnl_input col">
                            <asp:DropDownList runat="server" ID="drop_grupo"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="content row">
                        <div class="pnl_input col">
                            <i class="fas fa-icons"></i>
                            <asp:TextBox runat="server" ID="txt_icono" placeholder="COPIAR ETIQUETA ICONO <i></i>"></asp:TextBox>
                        </div>

                        <div class="pnl_input col">
                            <i class="fas fa-link"></i>
                            <asp:TextBox runat="server" ID="txt_ruta" placeholder="RUTA DEL MÓDULO"></asp:TextBox>
                        </div>
                    </div>

                    <div class="content row">
                        <div class="pnl_input col">
                            <i class="fas fa-align-right"></i>
                            <asp:TextBox runat="server" ID="txt_descrip" placeholder="DESCRIPCIÓN BREVE MÓDULO"></asp:TextBox>
                        </div>

                        <div class="col"></div>
                    </div>
                    <asp:LinkButton runat="server" ID="btn_crear_modulo" OnClick="crear_nuevo_modulo">CREAR</asp:LinkButton>
                </section>

            </div>
        </div>
    </div>

    <!--MODAL ACTUALIZAR VISTA-->
    <div class="modal-i-gl modal-i-gl-hide animated fadeIn" id="modal_actualizar_vista">
        <div class="modal-i-gl-body">
            <div class="modal-i-gl-title">
                <h1 class="title">Actualizar datos vista</h1>
                <div class="modal-i-gl-cerrar">
                    <button type="button" class="btn-modal-close"><i class="fas fa-times"></i></button>
                </div>
            </div>
            <div class="modal-i-gl-content">

                <!--Aquí el contenido-->
                <section class="box_content_crear_vista">
                    <div class="content row">
                        <div class="pnl_input col">
                            <label style="position: absolute; margin-top: -20px; margin-left: 10px; font-size: 12px; font-weight: bold;">Nombre:</label>
                            <i class="far fa-keyboard"></i>
                            <asp:TextBox runat="server" ID="txt_vista_actu" placeholder="NOMBRE VISTA"></asp:TextBox>
                        </div>

                        <div class="col">
                            <label style="position: absolute; margin-top: -20px; margin-left: 10px; font-size: 12px; font-weight: bold;">Estado:</label>
                            <div class="row" style="margin: 0px;">
                                <div class="pnl_input col" style="margin: 0px; margin-right: 10px;">
                                    <i class="far fa-keyboard"></i>
                                    <asp:TextBox runat="server" ID="txt_drop_estado_vista" placeholder="VALOR DROP" Enabled="false"></asp:TextBox>
                                </div>
                                <div class="pnl_input col" style="margin: 0px; max-width: max-content;">
                                    <asp:DropDownList runat="server" ID="drop_estado_vista">
                                        <asp:ListItem Value="1">Activo</asp:ListItem>
                                        <asp:ListItem Value="0">Inactivo</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="content row">
                        <div class="pnl_input col">
                            <label style="position: absolute; margin-top: -20px; margin-left: 10px; font-size: 12px; font-weight: bold;">Descripción:</label>
                            <i class="fas fa-align-right"></i>
                            <asp:TextBox runat="server" ID="txt_vista_descrip_actu" placeholder="DESCRIPCIÓN BREVE VISTA"></asp:TextBox>
                        </div>

                        <div class="pnl_input col">
                            <label style="position: absolute; margin-top: -20px; margin-left: 10px; font-size: 12px; font-weight: bold;">Icono:</label>
                            <i class="fas fa-icons"></i>
                            <asp:TextBox runat="server" ID="txt_vista_icono_actu" placeholder="COPIAR ETIQUETA ICONO <i></i>"></asp:TextBox>
                        </div>
                    </div>

                    <div class="content row">
                        <div class="pnl_input col">
                            <label style="position: absolute; margin-top: -20px; margin-left: 10px; font-size: 12px; font-weight: bold;">Descripción:</label>
                            <i class="fas fa-align-right"></i>
                            <asp:TextBox runat="server" ID="txt_ruta_act" placeholder="RUTA DE LA VISTA"></asp:TextBox>
                        </div>

                        <div class="col"></div>
                    </div>
                    <asp:LinkButton runat="server" ID="LinkButton3" OnClick="actualizar_datos_vista">ACTUALIZAR</asp:LinkButton>
                </section>

            </div>
        </div>
    </div>

    <!--MODAL REASIGNAR VISTAS-->
    <div class="modal-i-gl modal-i-gl-hide animated fadeIn" id="modal_reasignar_vista">
        <div class="modal-i-gl-body">
            <div class="modal-i-gl-title">
                <h1 class="title">Reasignar vista</h1>
                <div class="modal-i-gl-cerrar">
                    <button type="button" class="btn-modal-close"><i class="fas fa-times"></i></button>
                </div>
            </div>
            <div class="modal-i-gl-content">

                <!--Aquí el contenido-->
                <section class="box_content_crear_vista">
                    <div class="content row">
                        <div class="pnl_input col">
                            <label style="position: absolute; margin-top: -20px; margin-left: 10px; font-size: 12px; font-weight: bold;">Nombre:</label>
                            <i class="far fa-keyboard"></i>
                            <asp:TextBox runat="server" ID="txt_vista_reasig" placeholder="NOMBRE VISTA" Enabled="false"></asp:TextBox>
                        </div>
                        <div class="pnl_input col">
                            <label style="position: absolute; margin-top: -20px; margin-left: 10px; font-size: 12px; font-weight: bold;">Grupos:</label>
                            <asp:DropDownList runat="server" ID="drop_grupos_vista">
                                <asp:ListItem Value="1">Activo</asp:ListItem>
                                <asp:ListItem Value="0">Inactivo</asp:ListItem>
                            </asp:DropDownList>
                        </div>

                    </div>
                    <asp:LinkButton runat="server" ID="LinkButton4">Asignar</asp:LinkButton>
                </section>

                <section class="pnl_table" style="border-radius: 5px; margin-top: 20px;">
                    <table runat="server" id="tbl_grupos_vista" class="tbl_vistas_general">
                        <thead>
                            <tr class="th">
                                <td>#</td>
                                <td>GRUPO</td>
                                <td>FECHA ASIGNACIÓN</td>
                            </tr>
                        </thead>
                    </table>
                </section>

            </div>
        </div>
    </div>

    <!--MODAL ESTADO VISTAS-->
    <div class="modal-i-gl modal-i-gl-hide animated fadeIn" id="modal_estado_vista">
        <div class="modal-i-gl-body">
            <div class="modal-i-gl-title">
                <h1 class="title">Vistas inhabilitadas</h1>
                <div class="modal-i-gl-cerrar">
                    <button type="button" class="btn-modal-close"><i class="fas fa-times"></i></button>
                </div>
            </div>
            <div class="modal-i-gl-content">

                <!--Aquí el contenido-->
                <section class="box_content_crear_vista">
                    <div class="content row">
                        <div class="pnl_input col">
                            <label style="position: absolute; margin-top: -20px; margin-left: 10px; font-size: 12px; font-weight: bold;">Vista:</label>
                            <select runat="server" id="drop_estado_vista_re"></select>
                        </div>
                        <div class="pnl_input col">
                            <label style="position: absolute; margin-top: -20px; margin-left: 10px; font-size: 12px; font-weight: bold;">Estado:</label>
                            <i class="fas fa-eye-slash"></i>
                            <asp:TextBox runat="server" ID="txt_estado_vist" placeholder="ESTADO VISTA" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                    <asp:LinkButton runat="server" ID="LinkButton5" OnClick="actualizar_estado_vista">Habilitar</asp:LinkButton>
                </section>

            </div>
        </div>
    </div>

    <script defer>
        const botonCrear = [...document.querySelectorAll('button')];

        botonCrear.map(boton => {
            boton.addEventListener('click', () => {
                boton.style.display = 'none';
            })
        });
    </script>
</asp:Content>
