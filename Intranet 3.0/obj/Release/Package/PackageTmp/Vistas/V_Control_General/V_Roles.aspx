<%@ Page Title="Roles" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="V_Roles.aspx.cs" Inherits="Intranet_3._0.Vistas.V_Control_General.V_Roles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="scripts_css" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="scripts_js" runat="server">

    <script>
        $(document).ready(function () {
            //modal actualizar
            $('body').on('click', '.btn-actu-rol', function () {

                if ($("#MainContent_tbl_roles input[name='rd_estado_rol']:radio").is(':checked')) {
                    $('#modal_actualizar_roles').addClass('modal-i-gl-show');
                    $('#modal_actualizar_roles').removeClass('modal-i-gl-hide');

                    $.ajax({
                        type: "POST",
                        url: "WebService_V_Control_General.asmx/cargar_datos_modal_actualizar_roles",
                        data: '{"Id_Rol": "' + $('input:radio[name=rd_estado_rol]:checked').val() + '"}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (resultado) {
                            let items = resultado.d;
                            $.each(items, function (index, item) {

                                $("#MainContent_txt_rol_actu").val(item[1]);
                                $("#MainContent_txt_drop_estado_rol").val(item[2]);
                                $("#MainContent_txt_rol_descrip_actu").val(item[3]);
                            });
                        }
                    });

                } else {
                    $('#modal_actualizar_roles').addClass('modal-i-gl-hide');
                    $('#modal_actualizar_roles').removeClass('modal-i-gl-show');

                    //notificación
                    $('.modal-noti').addClass('modal-noti-show');//agregar
                    $('.modal-noti').removeClass('modal-noti-hide');//quitar
                    $('.body-noti').addClass('advert'); //tipo notificación
                    $('.title-noti').html('<span class="fas fa-exclamation-circle"></span> Validación advertencia');//título
                    $('.content-noti').html('Seleccione un rol para validar.');//mensaje
                    setTimeout(function () {
                        $(".modal-noti").addClass("modal-noti-hide");
                        $(".modal-noti").removeClass("modal-noti-show");
                    }, 4000);
                }

            });

            //Estado roles
            $("#MainContent_drop_rol_inhabilitado").change(function () {
                $.ajax({
                    type: "POST",
                    url: "WebService_V_Control_General.asmx/cargar_estado_roles",
                    data: '{"Id_Rol": "' + $(this).val() + '"}',
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
        });
    </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <section class="pnl_table">
                <div class="pnl_tag">
                    <p><i class="fas fa-tag"></i>Tabla de roles</p>
                </div>
                <div class="filter">
                    <div class="box_menu_crear">
                        <button type="button" id="btn_crear_rol" class="btn-modal" data-id="modal_crear_rol">
                            <i class="fas fa-plus"></i>Nuevo rol
                        </button>
                        <button type="button" id="btn_actualizar_rol" class="btn-actu-rol" data-id="modal_crear_rol">
                            <i class="fas fa-cog"></i>Actualizar rol
                        </button>
                        <button type="button" runat="server" id="btn_estado_rol" class="btn-modal modal-button" data-id="modal_estado_rol">
                            <i class="fas fa-eye-slash"></i>Roles inhabilitados
                        </button>
                    </div>
                    <div class="box_search">
                        <i class="fas fa-search"></i>
                        <asp:TextBox runat="server" ID="txt_filter_grupo" placeholder="Búsqueda rápida" OnTextChanged="txt_filter_grupo_TextChanged" AutoPostBack="true"></asp:TextBox>
                    </div>
                </div>
                <table runat="server" id="tbl_roles" class="tbl_roles tbl_vistas_general"></table>
            </section>
        </ContentTemplate>
    </asp:UpdatePanel>



    <!--modales-->
    <!--ROLES-->
    <!--MODAL ROLES-->
    <div class="modal-i-gl modal-i-gl-hide animated fadeIn" id="modal_crear_rol">
        <div class="modal-i-gl-body">
            <div class="modal-i-gl-title">
                <h1 class="title">Crear nuevo rol</h1>
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
                            <asp:TextBox runat="server" ID="txt_rol" placeholder="NOMBRE NUEVO ROL"></asp:TextBox>
                        </div>
                        <div class="pnl_input col">
                            <i class="fas fa-align-right"></i>
                            <asp:TextBox runat="server" ID="txt_rol_descrip" placeholder="DESCRIPCIÓN BREVE ROL"></asp:TextBox>
                        </div>
                    </div>
                    <asp:LinkButton runat="server" ID="lnk_crear_" OnClick="crear_nuevo_rol">CREAR</asp:LinkButton>
                </section>

            </div>
        </div>
    </div>

    <!--MODAL ACTUALIZAR ROLES-->
    <div class="modal-i-gl modal-i-gl-hide animated fadeIn" id="modal_actualizar_roles">
        <div class="modal-i-gl-body">
            <div class="modal-i-gl-title">
                <h1 class="title">Actualizar datos roles</h1>
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
                            <asp:TextBox runat="server" ID="txt_rol_actu" placeholder="NOMBRE ROL"></asp:TextBox>
                        </div>

                        <div class="col">
                            <label style="position: absolute; margin-top: -20px; margin-left: 10px; font-size: 12px; font-weight: bold;">Estado:</label>
                            <div class="row" style="margin: 0px;">
                                <div class="pnl_input col" style="margin: 0px; margin-right: 10px;">
                                    <i class="far fa-keyboard"></i>
                                    <asp:TextBox runat="server" ID="txt_drop_estado_rol" placeholder="VALOR DROP" Enabled="false"></asp:TextBox>
                                </div>
                                <div class="pnl_input col" style="margin: 0px; max-width: max-content;">
                                    <asp:DropDownList runat="server" ID="drop_estado_rol">
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
                            <asp:TextBox runat="server" ID="txt_rol_descrip_actu" placeholder="DESCRIPCIÓN BREVE ROL"></asp:TextBox>
                        </div>

                        <div class="col"></div>
                    </div>
                    <asp:LinkButton runat="server" ID="lnk_actualizar_rol" OnClick="actualizar_datos_roles">ACTUALIZAR</asp:LinkButton>
                </section>

            </div>
        </div>
    </div>

    <!--MODAL ESTADO ROLES-->
    <div class="modal-i-gl modal-i-gl-hide animated fadeIn" id="modal_estado_rol">
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
                            <label style="position: absolute; margin-top: -20px; margin-left: 10px; font-size: 12px; font-weight: bold;">Roles:</label>
                            <select runat="server" id="drop_rol_inhabilitado"></select>
                        </div>
                        <div class="pnl_input col">
                            <label style="position: absolute; margin-top: -20px; margin-left: 10px; font-size: 12px; font-weight: bold;">Estado:</label>
                            <i class="fas fa-eye-slash"></i>
                            <asp:TextBox runat="server" ID="txt_estado_" placeholder="ESTADO ROL" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                    <asp:LinkButton runat="server" ID="LinkButton1" OnClick="actualizar_estado_rol">Habilitar</asp:LinkButton>
                </section>

            </div>
        </div>
    </div>

    <script defer>
        const botones = [...document.querySelectorAll('a')];

        botones.map(boton => {
            boton.addEventListener('click', () => {
                boton.style.display = 'none';
            })
        })
    </script>
</asp:Content>
