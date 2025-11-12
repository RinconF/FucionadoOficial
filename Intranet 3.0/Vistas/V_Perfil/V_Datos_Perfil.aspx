<%@ Page Async="true" Title="Perfil" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="V_Datos_Perfil.aspx.cs" Inherits="Intranet_3._0.Vistas.V_Perfil.V_Datos_Perfil" %>

<asp:Content ID="Content1" ContentPlaceHolderID="scripts_css" runat="server">
    <style>
        .suspender {
            pointer-events: none;
            display: none;
        }

        .row {
            margin-right: 0px;
            margin-left: 0px;
        }

        .col {
            padding-right: 0px;
            padding-left: 0px;
        }

        .body-content .pnl_table {
            border: none;
            box-shadow: none;
        }
        /*52456*/
        .pnl_body {
            position: relative;
            display: inline-flex;
            width: 100%;
        }

            .pnl_body .pnl_title {
                background: rgb(31,43,55);
                background: linear-gradient(90deg, rgba(31,43,55,1) 0%, rgba(36,58,80,1) 100%);
                width: 100%;
                border-radius: 5px;
                padding: 20px;
                box-shadow: 2px 2px 5px rgba(0,0,0,.3);
                position: relative;
            }

                .pnl_body .pnl_title .btn_actualizar_info {
                    font-size: 13px;
                    color: #fff;
                    background: rgba(255, 112, 82, 1);
                    padding: 5px 20px;
                    margin-left: 10px;
                    border-radius: 50px;
                    border: none;
                    box-shadow: 2px 2px 5px rgba(0,0,0,.3);
                    outline: none;
                    position: absolute;
                    right: 20px;
                    top: 20px;
                    z-index: 1;
                }

                    .pnl_body .pnl_title .btn_actualizar_info:hover {
                        opacity: .8;
                    }

                    .pnl_body .pnl_title .btn_actualizar_info:active {
                        box-shadow: 2px 2px 5px inset rgba(0,0,0,.3);
                    }

                .pnl_body .pnl_title .body_img {
                    width: 250px;
                    height: 250px;
                    text-align: center;
                    background: #fff;
                    border: 3px solid #e1e1e1;
                    border-radius: 50%;
                    margin: 60px 20px 10px auto;
                    position: relative;
                    box-shadow: 2px 2px 5px rgba(0,0,0,.3);
                }

        @media screen and (max-width: 1366px) {
            .pnl_body .pnl_title .body_img {
                width: 200px;
                height: 200px;
                margin: 30px 30px auto 30px;
            }
        }

        .pnl_body .pnl_title .img {
            background-position: center;
            background-repeat: no-repeat;
            background-size: 100% auto;
            position: absolute;
            top: 0;
            bottom: 0;
            left: 0;
            right: 0;
            border-radius: 50%;
        }

            .pnl_body .pnl_title .img .btn_actualizar_foto {
                width: 100%;
                height: 100%;
                border-radius: 50%;
                border: none;
                font-size: 50px;
                background: rgba(0,0,0,.7);
                color: #fff;
                text-shadow: 2px 2px 5px rgba(0,0,0,.3);
                outline: none;
                opacity: 0;
                transition: all 0.5s;
            }

                .pnl_body .pnl_title .img .btn_actualizar_foto:hover {
                    opacity: 1;
                }

        /*Imagen*/
        .pnl_body .pnl_title .col:nth-child(1) {
            max-width: 25rem;
        }

        @media screen and (max-width: 1366px) {
            .pnl_body .pnl_title .col:nth-child(1) {
                max-width: max-content;
            }
        }


        /*Datos personales*/
        .pnl_body .pnl_title .col:nth-child(2) {
            padding: 20px;
        }

            .pnl_body .pnl_title .col:nth-child(2) hr {
                border-top: 1px solid rgba(255, 255, 255, 0.1);
            }

            .pnl_body .pnl_title .col:nth-child(2) p {
                margin: 0px;
                padding: 0px;
                font-size: 16px;
                text-transform: uppercase;
            }

                .pnl_body .pnl_title .col:nth-child(2) p:nth-child(1) {
                    font-size: 20px;
                    color: rgba(22, 160, 133, 1);
                }

                .pnl_body .pnl_title .col:nth-child(2) p:nth-child(3) {
                    font-size: 15px;
                    color: rgba(22, 160, 133, .9);
                    margin-top: 10px;
                }

                .pnl_body .pnl_title .col:nth-child(2) p span {
                    font-size: 15px;
                    color: rgba(110, 130, 151,.9);
                    background: rgba(31,43,55,1);
                    padding: 5px 20px;
                    margin-left: 10px;
                    border-radius: 50px;
                }

            .pnl_body .pnl_title .col:nth-child(2) .card_info {
                border-radius: 5px;
            }

                .pnl_body .pnl_title .col:nth-child(2) .card_info p {
                    font-size: 15px;
                    color: #fff;
                    padding: 10px 20px;
                }

                    .pnl_body .pnl_title .col:nth-child(2) .card_info p span {
                        font-size: 15px;
                        color: #fff;
                        background: rgba(31,43,55,1);
                        padding: 5px 20px;
                        margin-left: 10px;
                        border-radius: 50px;
                        display: inline-block;
                    }


        /*Datos intranet*/
        /*content*/
        .pnl_body .pnl_content {
            width: 30%;
            color: #fff;
            background: rgb(31,43,55);
            background: linear-gradient(270deg, rgba(31,43,55,1) 0%, rgba(36,58,80,1) 100%);
            border-radius: 5px;
            margin-left: 20px;
            box-shadow: 2px 2px 5px rgba(0,0,0,.3);
            text-transform: uppercase;
            font-weight: bold;
        }

        @media screen and (max-width: 1366px) {
            .pnl_body .pnl_content {
                width: 50%;
            }
        }

        .pnl_body .pnl_content h3 {
            padding: 20px;
            text-align: center;
            font-weight: bold;
            font-size: 15px;
            border-bottom: 1px solid rgba(255, 255, 255, 0.1);
            color: rgba(22, 160, 133, 1);
        }

        .pnl_body .pnl_content p {
            padding: 10px 20px 10px 30px;
            margin-bottom: 0px;
            font-weight: bold;
            font-size: 15px;
            color: #fff;
        }

            .pnl_body .pnl_content p span {
                font-size: 15px;
                color: #fff;
                background: rgba(31,43,55,1);
                padding: 5px 20px;
                margin-left: 10px;
                border-radius: 50px;
                display: inline-block;
            }

            .pnl_body .pnl_content p button {
                font-size: 13px;
                color: #fff;
                background: rgba(255, 112, 82, 1);
                padding: 5px 20px;
                margin-left: 10px;
                border-radius: 50px;
                border: none;
                box-shadow: 2px 2px 5px rgba(0,0,0,.3);
                outline: none;
            }

                .pnl_body .pnl_content p button:hover {
                    opacity: .8;
                }

                .pnl_body .pnl_content p button:active {
                    box-shadow: 2px 2px 5px inset rgba(0,0,0,.3);
                }

        .pnl_body .pnl_content hr {
            border-top: 1px solid rgba(255, 255, 255, 0.1);
            margin-top: 5px;
            margin-bottom: 5px;
        }


        /*contacto de emergencia*/
        /*Imagen*/
        .pnl_body .pnl_title_emergencia .col:nth-child(1) {
            max-width: 5rem;
        }

        @media screen and (max-width: 1366px) {
            .pnl_body .pnl_title_emergencia .col:nth-child(1) {
                max-width: max-content;
            }
        }

        .pnl_body .pnl_title_emergencia { /*datos*/
        }


        .pnl_body .pnl_content_noti { /*notificaciones*/
            background: #f9f9f9;
            width: 70%;
        }

            .pnl_body .pnl_content_noti h3 {
                background: #f5f5f5;
                border-bottom: 1px solid #e1e1e1;
                color: rgba(22, 160, 133, .7);
                font-weight: normal;
                font-size: 15px;
            }

            .pnl_body .pnl_content_noti .pnl_notificaciones {
                overflow-y: auto;
                height: 250px;
                padding: 20px 30px;
            }

                .pnl_body .pnl_content_noti .pnl_notificaciones .pnl_txt_notificacion p {
                    padding: 0px;
                    margin: 10px;
                    color: rgba(22, 160, 133, .7);
                    font-weight: normal;
                }

                    .pnl_body .pnl_content_noti .pnl_notificaciones .pnl_txt_notificacion p:nth-child(2) {
                        background: linear-gradient(90deg, rgba(31,43,55,1) 0%, rgba(36,58,80,1) 100%);
                        color: #fff;
                        padding: 10px 10px 10px 20px;
                        border-radius: 50px;
                        font-size: 15px;
                        position: relative;
                    }

                        .pnl_body .pnl_content_noti .pnl_notificaciones .pnl_txt_notificacion p:nth-child(2):before {
                            position: absolute;
                            margin-top: -15px;
                            left: 30px;
                            content: '';
                            width: 0px;
                            height: 0px;
                            border-left: 7px solid transparent;
                            border-right: 7px solid transparent;
                            border-bottom: 7px solid rgba(31,43,55,1);
                        }

        .box-img-pre {
            width: 100%;
            height: 300px;
            background: #f9f9f9;
            border: 1px solid #e1e1e1;
            width: 100%;
            border-radius: 5px 5px 0px 0px;
            overflow: auto;
        }

        .txt-img-pre {
            background: #fff;
            border-radius: 5px 5px 0px 0px;
            margin-bottom: -1px;
            border: 1px solid #e1e1e1;
            background: #f0f0f0;
            padding: 10px;
            white-space: pre-wrap;
            word-break: break-word;
        }

        .img-pre {
            height: 100%;
            border-radius: 5px 5px 0px 0px;
        }

        .btn_cargar_foto {
            width: 100%;
            border: 1px solid rgba(17,122,102,.8);
            background: rgba(22, 160, 133, .9);
            color: #fff;
            cursor: pointer;
            border-radius: 0px 0px 5px 5px;
            position: relative;
            padding: 10px;
        }

            .btn_cargar_foto i {
                font-size: 25px;
                padding: 10px;
                background: rgba(18,140,116,1);
                border-right: 1px solid rgba(17,122,102,.8);
                position: absolute;
                top: 0;
                left: 0;
                border-radius: 0px 0px 0px 5px;
            }

        @media screen and (max-width: 1366px) {
            .modal-i-gl-body-small {
                width: 90%;
            }
        }

        @media only screen and (max-width: 750px) {
            .pnl_body .pnl_title .col:nth-child(2) .card_info p {
                color: rgba(255, 112, 82, 1);
            }

            .pnl_body .pnl_content p {
                color: rgba(255, 112, 82, 1);
            }
        }



        /*address*/
        .btn_cambiar_address {
            position: absolute;
            top: 0;
            bottom: 0;
            background: rgba(22, 160, 133, 1);
            color: #fff;
            border-radius: 5px 0px 0px 5px;
            border: none;
            outline: none;
        }
        /*@media only screen and (max-width: 1100px) {
            label {
                display: block;
            }

            .row {
                display: block;
            }

            .box_content_crear_vista .row .pnl_input {
                margin: 30px 0;
            }
        }*/
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="scripts_js" runat="server">
    <link rel="Stylesheet" href="/Styles/css/nucleo_familiar/nucleo_familiar.css"/>
    <script defer>
        $(document).ready(function () {
            cargar_drop_ciudad();
            validar_drop_ciudad();
            let emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;

            $("body").on("click", ".btn-modal-actualizar", function () {

                if (
                    $('#MainContent_txt_number_actu').val() != "" &&
                    $('#MainContent_txt_number_actu').val().length === 10 &&
                    $('#MainContent_txt_mail_actu').val() != "" &&
                    emailReg.test($('#MainContent_txt_mail_actu').val()) &&
                    $('#MainContent_txt_direccion_actu').val() != "" &&
                    $('#MainContent_drop_estado').val() != "" &&
                    $('#MainContent_drop_estrato').val() != "" &&
                    $('#MainContent_drop_etnico').val() != "" &&
                    $('#MainContent_drop_ciudad').val() != "" &&
                    $('#MainContent_drop_local').val() != "" &&
                    $('#MainContent_drop_barrio').val() != "" &&
                    $('#MainContent_drop_depart').val() != "" &&
                    $('#MainContent_drop_genero').val() != "" &&
                    $('#MainContent_drop_vivienda').val() != "" &&
                    $('#MainContent_txt_hobbie_act').val() != ""
                ) {
                    let params = new URLSearchParams(location.search);
                    let Id_Usuario = params.get('Id_Usuario');

                    if ($('#MainContent_drop_depart').val() == "25") {
                        $('#MainContent_drop_local').val(0);
                        $('#MainContent_drop_barrio').val(0);
                    }

                    $.ajax({
                        type: "POST",
                        url: "WebService_V_Perfil.asmx/Actualizar_Informacion_Colaborador",
                        data: '{"N_Identificacion": "' + Id_Usuario +
                            '","Celular": "' + $('#MainContent_txt_number_actu').val() +
                            '","Email_Personal": "' + $('#MainContent_txt_mail_actu').val() +
                            '","Dir_Residencia": "' + $('#MainContent_txt_direccion_actu').val() +
                            '","Id_EstadoCivil": "' + $('#MainContent_drop_estado').val() +
                            '","Drop_estrato": "' + $('#MainContent_drop_estrato').val() +
                            '","Drop_etnico": "' + $('#MainContent_drop_etnico').val() +
                            '","Drop_Ciudad": "' + $('#MainContent_drop_ciudad').val() +
                            '","Drop_Localidad": "' + $('#MainContent_drop_local').val() +
                            '","Drop_Barrio": "' + $('#MainContent_drop_barrio').val() +
                            '","Drop_Depart": "' + $('#MainContent_drop_depart option:selected').text() +
                            '","Drop_Depart_id": "' + $('#MainContent_drop_depart').val() +
                            '","Drop_Genero": "' + $('#MainContent_drop_genero').val() +
                            '","Drop_Vivienda": "' + $('#MainContent_drop_vivienda').val() +
                            '","Hobbie": "' + $('#MainContent_txt_hobbie_act').val() + '"}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (resultado) {
                            let items = resultado.d;
                            if (items == "1") {
                                location.href = location.href;
                            }
                        }
                    });
                }
                else {
                    $('#lnk_actualizar').attr('style', 'display:block')
                    //notificación
                    $('.modal-noti').addClass('modal-noti-show');//agregar
                    $('.modal-noti').removeClass('modal-noti-hide');//quitar
                    $('.body-noti').attr("class", 'body-noti advert'); //tipo notificación
                    $('.title-noti').html('<span class="fas fa-check"></span> Validación advertencia');//título
                    $('.content-noti').html('Campos vacíos o datos no válidos, verifique e intente de nuevo.');//mensaje
                    setTimeout(function () {
                        $(".modal-noti").addClass("modal-noti-hide");
                        $(".modal-noti").removeClass("modal-noti-show");
                    }, 4000);
                }
            });

            $("body").on("click", ".btn-modal-cambiar", function () {
                if (
                    $('#MainContent_txt_nueva_pass').val() != "" &&
                    $('#MainContent_txt_nueva_pass').val().length > 6
                ) {
                    let params = new URLSearchParams(location.search);
                    let Id_Usuario = params.get('Id_Usuario');

                    $.ajax({
                        type: "POST",
                        url: "WebService_V_Perfil.asmx/Actualizar_Contraseña",
                        data: '{"Id_Usuario": "' + Id_Usuario + '","txt_actual_pass": "' + $('#MainContent_txt_actual_pass').val() + '","txt_nueva_pass": "' + $('#MainContent_txt_nueva_pass').val() + '","txt_conf_pass": "' + $('#MainContent_txt_conf_pass').val() + '"}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (resultado) {
                            let items = resultado.d;
                            if (items == "vacio") {
                                $('#lnk_cambiar').attr('style', 'display:block');
                                //notificación
                                $('.modal-noti').addClass('modal-noti-show');//agregar
                                $('.modal-noti').removeClass('modal-noti-hide');//quitar
                                $('.body-noti').attr("class", 'body-noti advert'); //tipo notificación
                                $('.title-noti').html('<span class="fas fa-check"></span> Validación advertencia');//título
                                $('.content-noti').html('Campos vacíos, verifique e intente de nuevo.');//mensaje
                                setTimeout(function () {
                                    $(".modal-noti").addClass("modal-noti-hide");
                                    $(".modal-noti").removeClass("modal-noti-show");
                                }, 4000);
                            }
                            else if (items == "error") {
                                $('#lnk_cambiar').attr('style', 'display:block');
                                //notificación
                                $('.modal-noti').addClass('modal-noti-show');//agregar
                                $('.modal-noti').removeClass('modal-noti-hide');//quitar
                                $('.body-noti').attr("class", 'body-noti error'); //tipo notificación
                                $('.title-noti').html('<span class="fas fa-times-circle"></span> Validación error');//título
                                $('.content-noti').html('Error desde el servidor.');//mensaje
                                setTimeout(function () {
                                    $(".modal-noti").addClass("modal-noti-hide");
                                    $(".modal-noti").removeClass("modal-noti-show");
                                }, 4000);
                            }
                            else if (items == "0") {
                                $('#lnk_cambiar').attr('style', 'display:block');
                                //notificación
                                $('.modal-noti').addClass('modal-noti-show');//agregar
                                $('.modal-noti').removeClass('modal-noti-hide');//quitar
                                $('.body-noti').attr("class", 'body-noti error'); //tipo notificación
                                $('.title-noti').html('<span class="fas fa-times-circle"></span> Validación error');//título
                                $('.content-noti').html('Contraseña actual no coincide.');//mensaje
                                setTimeout(function () {
                                    $(".modal-noti").addClass("modal-noti-hide");
                                    $(".modal-noti").removeClass("modal-noti-show");
                                }, 4000);
                            }
                            else {
                                //notificación
                                $('.modal-noti').addClass('modal-noti-show');//agregar
                                $('.modal-noti').removeClass('modal-noti-hide');//quitar
                                $('.body-noti').attr("class", 'body-noti sucess'); //tipo notificación
                                $('.title-noti').html('<span class="fas fa-check-circle"></span> Validación correcta');//título
                                $('.content-noti').html('Contraseña actualizada correctamente.');//mensaje
                                setTimeout(function () {
                                    $(".modal-noti").addClass("modal-noti-hide");
                                    $(".modal-noti").removeClass("modal-noti-show");
                                }, 4000);

                                $('#MainContent_txt_actual_pass').val("");
                                $('#MainContent_txt_nueva_pass').val("");
                                $('#MainContent_txt_conf_pass').val("");
                                window.location.reload();
                            }
                        }
                    });
                } else {
                    $('#lnk_cambiar').attr('style', 'display:block');
                    //notificación
                    $('.modal-noti').addClass('modal-noti-show');//agregar
                    $('.modal-noti').removeClass('modal-noti-hide');//quitar
                    $('.body-noti').attr("class", 'body-noti advert'); //tipo notificación
                    $('.title-noti').html('<span class="fas fa-check"></span> Validación advertencia');//título
                    $('.content-noti').html('Campos vacíos o datos no válidos, verifique e intente de nuevo.');//mensaje
                    setTimeout(function () {
                        $(".modal-noti").addClass("modal-noti-hide");
                        $(".modal-noti").removeClass("modal-noti-show");
                    }, 4000);
                }
            });

            $("#MainContent_txt_nueva_pass").keyup(function () {
                if ($('#MainContent_txt_nueva_pass').val() == $('#MainContent_txt_conf_pass').val()) {
                    $('#icon_verf_pass').removeAttr("class");
                    $('#icon_verf_pass').addClass("far fa-check-circle correct");
                    $("#txt_verificacion").text("verificado");
                }
                else {
                    $('#icon_verf_pass').removeAttr("class");
                    $('#icon_verf_pass').addClass("far fa-times-circle err");
                    $("#txt_verificacion").text("No verificado");
                }
            });
            $("#MainContent_txt_conf_pass").keyup(function () {
                if ($('#MainContent_txt_nueva_pass').val() == $('#MainContent_txt_conf_pass').val()) {
                    $('#icon_verf_pass').removeAttr("class");
                    $('#icon_verf_pass').addClass("far fa-check-circle correct");
                    $('#lnk_cambiar').removeAttr("disabled");
                    $("#txt_verificacion").text("verificado");
                }
                else {
                    $('#icon_verf_pass').removeAttr("class");
                    $('#icon_verf_pass').addClass("far fa-times-circle err");
                    $('#lnk_cambiar').attr("disabled", "disabled");
                    $("#txt_verificacion").text("No verificado");
                }
            });

            //CAMBIAR FOTO
            $("#MainContent_file_foto").change(function () {

                if ($(this).val() != null) {
                    //$(".txt-img-pre").text($(this).val().replace(/C:\\fakepath\\/i, ''));
                    $("#MainContent_lnk_actualizar_foto").removeAttr("style");
                    let reader = new FileReader();
                    reader.onload = function (e) {
                        $('#img_prev').attr('src', e.target.result);
                    }
                    reader.readAsDataURL(this.files[0]);
                }
                else {
                    $("#MainContent_lnk_actualizar_foto").attr("style", "display: none;");
                    //notificación
                    $('.modal-noti').addClass('modal-noti-show');//agregar
                    $('.modal-noti').removeClass('modal-noti-hide');//quitar
                    $('.body-noti').attr("class", 'body-noti advert'); //tipo notificación
                    $('.title-noti').html('<span class="fas fa-exclamation-circle"></span> Validación advertencia');//título
                    $('.content-noti').html('No se puede guardar hasta que seleccione una imagen por favor.');//mensaje
                    setTimeout(function () {
                        $(".modal-noti").addClass("modal-noti-hide");
                        $(".modal-noti").removeClass("modal-noti-show");
                    }, 4000);
                }
            });



            //drop ubicación change departamento
            $("#MainContent_drop_depart").change(function () {
                cargar_drop_ciudad();
                validar_drop_ciudad();
            });
            //drop ubicación change ciudad
            $("#MainContent_drop_ciudad").change(function () {
                cargar_drop_localidad();
            });
            //drop ubicación change localidad
            $("#MainContent_drop_local").change(function () {
                cargar_drop_barrios();
                validar_drop_localidad();
            });



            //actualizar contacto de emergencia
            $("body").on("click", ".btn-modal-actualizar-contacto", function () {
                if
                (
                    $('#MainContent_txt_nombre_contacto').val() != "" &&
                    $('#MainContent_txt_apellido_contacto').val() != "" &&
                    $('#MainContent_drop_parentesco').val() != "" &&
                    //$('#MainContent_txt_direc_contacto').val() != "" &&
                    $('#MainContent_txt_number_contacto').val() != "" &&
                    $('#MainContent_txt_number_contacto').val().length === 10
                ) {
                    let params = new URLSearchParams(location.search);
                    let Id_Usuario = params.get('Id_Usuario');

                    $.ajax({
                        type: "POST",
                        url: "WebService_V_Perfil.asmx/Actualizar_Contacto_Emergencia",
                        data: '{"Id_Usuario": "' + Id_Usuario +
                            '","Nombre": "' + $('#MainContent_txt_nombre_contacto').val() +
                            '","Apellido": "' + $('#MainContent_txt_apellido_contacto').val() +
                            '","Id_Parentesco": "' + $('#MainContent_drop_parentesco').val() +
                            '","Direccion": "' + $('#MainContent_txt_direc_contacto').val() +
                            '","Celular": "' + $('#MainContent_txt_number_contacto').val() +
                            '"}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (resultado) {
                            let items = resultado.d;
                            if (items == "1") {
                                location.href = location.href;
                            }
                        }
                    });
                }
                else {
                    $('#lnk_actualizar_contacto').attr('style', 'display:block');
                    //notificación
                    $('.modal-noti').addClass('modal-noti-show');//agregar
                    $('.modal-noti').removeClass('modal-noti-hide');//quitar
                    $('.body-noti').attr("class", 'body-noti advert'); //tipo notificación
                    $('.title-noti').html('<span class="fas fa-check"></span> Validación advertencia');//título
                    $('.content-noti').html('Campos vacíos, verifique e intente de nuevo.');//mensaje
                    setTimeout(function () {
                        $(".modal-noti").addClass("modal-noti-hide");
                        $(".modal-noti").removeClass("modal-noti-show");
                    }, 4000);
                }
            });

        });

        function validar_drop_ciudad() {
            setTimeout(function () {
                if ($("#MainContent_drop_ciudad option:selected").text() == "BOGOTA D.C.") {
                    $("#box_drop_local").removeAttr("style");
                    $("#box_drop_barrio").removeAttr("style");
                    cargar_drop_localidad();
                    setTimeout(function () {
                        cargar_drop_barrios();
                    }, 1000);
                }
                else {
                    $("#box_drop_local").attr("style", "display: none;");
                    $("#box_drop_barrio").attr("style", "display: none;");
                }
            }, 1000);
        }

        function validar_drop_localidad() {
            if ($("#MainContent_drop_ciudad option:selected").text() == "BOGOTA D.C.") {
                $("#box_drop_barrio").removeAttr("style");
            }
            else {
                $("#box_drop_barrio").attr("style", "display: none;");
            }
        }

        function cargar_drop_ciudad() {
            $("#MainContent_drop_ciudad").find("option").remove();
            $.ajax({
                type: "POST",
                url: "WebService_V_Perfil.asmx/cargar_drop_ciudad",
                data: '{"Id_Depart": "' + $("#MainContent_drop_depart").val() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (resultado) {
                    let items = resultado.d;
                    $.each(items, function (index, item) {
                        $("#MainContent_drop_ciudad").append(
                            "<option value='" + item[0] + "'>" + item[1] + "</option>"
                        );
                    });

                    if ($("#MainContent_drop_ciudad option:selected").text() === 'AGUA DE DIOS') {
                        $('#MainContent_drop_ciudad').val($('#MainContent_txt_ciudadUsuario').val());
                    }
                    if ($("#MainContent_drop_ciudad option:selected").text() == "BOGOTA D.C.") {
                    }
                }
            });
        }

        function cargar_drop_localidad() {
            $("#MainContent_drop_local").find("option").remove();
            $.ajax({
                type: "POST",
                url: "WebService_V_Perfil.asmx/cargar_drop_localidad",
                data: '',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (resultado) {
                    let items = resultado.d;
                    $.each(items, function (index, item) {
                        $("#MainContent_drop_local").append(
                            "<option value='" + item[0] + "'>" + item[1] + "</option>"
                        );
                    });
                    $('#MainContent_drop_local').val($('#MainContent_txt_localidadUsuario').val());
                }
            });
        }

        function cargar_drop_barrios() {
            $("#MainContent_drop_barrio").find("option").remove();
            $.ajax({
                type: "POST",
                url: "WebService_V_Perfil.asmx/cargar_drop_barrios",
                data: '{"Id_Localidad": "' + $("#MainContent_drop_local").val() + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (resultado) {
                    let items = resultado.d;
                    $.each(items, function (index, item) {
                        $("#MainContent_drop_barrio").append(
                            "<option value='" + item[0] + "'>" + item[1] + "</option>"
                        );
                    });
                    if ($('#MainContent_drop_local').val() === $('#MainContent_txt_localidadUsuario').val()) {
                        $('#MainContent_drop_barrio').val($('#MainContent_txt_barrioUsuario').val());
                    }
                }
            });
        }



        $("body").ready(function () {
            Cargar_Datos_Usuario();
        });

        function Cargar_Datos_Usuario() {
            let params = new URLSearchParams(location.search);
            let Id_Usuario = params.get('Id_Usuario');

            $.ajax({
                type: "POST",
                url: "WebService_V_Perfil.asmx/Cargar_Datos_Usuario",
                data: '{"Id_Usuario": "' + Id_Usuario + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (resultado) {
                    let items = resultado.d;
                    $("#MainContent_drop_ciudad option[value='" + items[0] + "']").attr("selected", "selected");
                    $("#MainContent_drop_local option[value='" + items[1] + "']").attr("selected", "selected");
                    $("#MainContent_drop_barrio option[value='" + items[2] + "']").attr("selected", "selected");
                }
            });
        }
        function solonumeros(e) {
            key = e.keycode || e.which;
            teclado = String.fromCharCode(key);
            numeros = "0123456789";
            especiales = "8-37-38-46-09";
            teclado_especial = false;

            for (var i in especiales) {
                if (key == especiales[i]) {
                    teclado_especial = true;
                }
            }
            if (numeros.indexOf(teclado) == -1 && !teclado_especial) {
                return false;
            }
        }

        function sololetras(e) {
            key = e.keycode || e.which;
            teclado = String.fromCharCode(key);
            numeros = "abcdefghijklmnñopqrstuvwxyz áéíóúüÜABCDEFGHIJKLMNÑOPQRSTUVWXYZ";
            especiales = "8-37-38-46";
            teclado_especial = false;

            for (var i in especiales) {
                if (key == especiales[i]) {
                    teclado_especial = true;
                }
            }
            if (numeros.indexOf(teclado) == -1 && !teclado_especial) {
                return false;
            }

        }

    </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <section class="pnl_table">
        <div class="pnl_tag">
            <p><i class="fas fa-tag"></i>Datos personales</p>
        </div>
        <div class="pnl_body">
            <div class="pnl_title">
                <button type="button" class="btn-modal btn_actualizar_info" data-id="modal_datos_personales">
                    <i class="fas fa-cog"></i>Editar
                </button>
                <div class="row">
                    <div class="col">
                        <!--Imagen-->
                        <div class="body_img">
                            <div runat="server" id="user_profile" class="img">
                                <button type="button" id="btn_actualizar_foto_" class="btn-modal btn_actualizar_foto" data-id="modal_actualizar_foto">
                                    <i class="fas fa-camera"></i>
                                </button>
                            </div>
                        </div>
                    </div>
                    <div class="col">
                        <!--Datos personales y laborales-->
                        <p runat="server" id="txt_name">nombre</p>
                        <p runat="server" id="txt_cargo"><i class="fas fa-tags"></i>cargo</p>
                        <p runat="server" id="txt_fecha_ing"><i class="far fa-calendar-alt"></i>Fecha de ingreso <span>02-12-2021</span></p>
                        <hr />
                        <div class="card_info">
                            <p runat="server" id="txt_Number"><i class="fas fa-mobile-alt"></i>Número contacto: <span>+57 - 0000000000</span></p>
                            <p runat="server" id="txt_mail"><i class="fas fa-envelope"></i>Correo personal: <span>example@hotmail.com</span></p>
                            <p runat="server" id="txt_mail_corporativo"><i class="fas fa-mail-bulk"></i>Correo corporativo: <span>example@hotmail.com</span></p>
                            <p runat="server" id="txt_fecha_naci"><i class="fas fa-birthday-cake"></i>Fecha nacimiento: <span>02-12-2021</span></p>
                            <p runat="server" id="txt_direc"><i class="fas fa-map-marked-alt"></i>Dirección residencia: <span>2b este # 25 - 63</span></p>
                            <p runat="server" id="txt_ciudad"><i class="fas fa-globe-americas"></i>Ciudad: <span>Ciudad</span></p>
                            <p runat="server" id="txt_locali"><i class="fas fa-location-arrow"></i>Localidad: <span>Localidad</span></p>
                            <p runat="server" id="txt_barrio"><i class="fas fa-street-view"></i>Barrio: <span>Barrio</span></p>
                        </div>
                    </div>
                </div>
            </div>
            <div class="pnl_content">
                <h3>Datos adicionales</h3>
                <p runat="server" id="txt_estado"><i class="far fa-handshake"></i>Estado civil: <span>Soltero(a)</span></p>
                <p runat="server" id="txt_estrato"><i class="far fa-chart-bar"></i>Estrato: <span>2</span></p>
                <p runat="server" id="txt_etnia"><i class="fas fa-users"></i>Grupo étnico: <span>Ninguno</span></p>
                <p runat="server" id="txt_genero"><i class="fas fa-restroom"></i>Genero: <span>Genero</span></p>
                <p runat="server" id="txt_vivienda"><i class="fas fa-home"></i>Vivienda: <span>Vivienda</span></p>
                <p runat="server" id="txt_hobbie"><i class="fas fa-dice"></i>Hobbie: <span>Hobbie</span></p>
                <hr />
                <h3>Datos intranet</h3>
                <p runat="server" id="txt_usu"><i class="fas fa-user-tie"></i>Usuario: <span>1024563320</span></p>
                <p runat="server" id="txt_passw">
                    <i class="fas fa-unlock-alt"></i>Contraseña:
                    <button type="button" class="btn-modal" data-id="modal_cambiar_pass">Cambiar</button>
                </p>
                <p runat="server" id="txt_fecha_intranet"><i class="fas fa-calendar-check"></i>Fecha creación: <span>02-12-2019</span></p>
                <br />
            </div>
        </div>
    </section>

    <br />
    <br />

    <section class="pnl_table">
        <div class="pnl_tag">
            <p><i class="fas fa-tag"></i>Datos contacto de emergencia</p>
        </div>
        <div class="pnl_body">
            <div class="pnl_title pnl_title_emergencia">
                <button type="button" class="btn-modal btn_actualizar_info" data-id="modal_datos_emergencia">
                    <i class="fas fa-cog"></i>Editar
                </button>
                <div class="row" style="margin-top: 20px;">
                    <div class="col"></div>
                    <div class="col">
                        <!--Datos personales y laborales-->
                        <p runat="server" id="txt_nombre_cont">No registrado</p>
                        <p runat="server" id="txt_parentesco_cont"><i class="fas fa-tags"></i>No registrado</p>
                        <hr />
                        <div class="card_info">
                            <p runat="server" id="txt_number_cont"><i class="fas fa-mobile-alt"></i>Número contacto: <span>No registrado</span></p>
                            <p runat="server" id="txt_direccion_cont"><i class="fas fa-map-marked-alt"></i>Dirección residencia: <span>No registrado</span></p>
                        </div>
                    </div>
                </div>
            </div>
            <div class="pnl_content pnl_content_noti">
                <!--Notificaciones-->
                <h3><i class="fas fa-bullhorn"></i>Notificaciones y novedades</h3>
                <div class="pnl_notificaciones">
                    <div class="pnl_txt_notificacion">
                        <p runat="server"><i class="fas fa-circle"></i>11/03/2021</p>
                        <p runat="server">Bienvenido a MiEtib corporativo.</p>
                        <p style="text-align: center; font-size: 20px;"><i class="fas fa-ellipsis-h"></i></p>
                    </div>
                </div>
            </div>
        </div>
    </section>


    <!--modales-->
    <!--ACTUALIZAR-->
    <!--MODAL DATOS PERSONALES-->
    <div class="modal-i-gl modal-i-gl-hide animated fadeIn" id="modal_datos_personales">
        <div class="modal-i-gl-body">
            <div class="modal-i-gl-title">
                <h1 class="title">Actualizar información personal</h1>
                <div class="modal-i-gl-cerrar">
                    <button type="button" class="btn-modal-close"><i class="fas fa-times"></i></button>
                </div>
            </div>
            <div class="modal-i-gl-content">

                <!--Aquí el contenido-->
                <section class="box_content_crear_vista">
                    <div class="content row">
                        <div class="pnl_input col">
                            <label style="position: absolute; margin-top: -20px; margin-left: 10px; font-size: 12px; font-weight: bold;">NÚMERO PERSONAL:</label>
                            <i class="fas fa-mobile-alt"></i>
                            <asp:TextBox runat="server" ID="txt_number_actu" placeholder="NÚMERO CONTACTO PERSONAL" MaxLength="12" CssClass="input-number"></asp:TextBox>
                        </div>

                        <div class="pnl_input col">
                            <label style="position: absolute; margin-top: -20px; margin-left: 10px; font-size: 12px; font-weight: bold;">CORREO ELECTRÓNICO PERSONAL:</label>
                            <i class="fas fa-envelope"></i>
                            <asp:TextBox runat="server" ID="txt_mail_actu" placeholder="CORREO ELECTRÓNICO PERSONAL" TextMode="Email"></asp:TextBox>
                        </div>
                    </div>
                    <div class="content row">
                        <div class="pnl_input col">
                            <label style="position: absolute; margin-top: -20px; margin-left: 10px; font-size: 12px; font-weight: bold;">DIRECCIÓN RESIDENCIAL:</label>
                            <button type="button" class="btn_cambiar_address btn-address" data-id="MainContent_txt_direccion_actu">
                                <span class="fas fa-cog"></span>
                            </button>
                            <i class="fas fa-map-marked-alt"></i>
                            <asp:TextBox runat="server" ID="txt_direccion_actu" placeholder="DIRECCIÓN RESIDENCIAL" Enabled="false" Style="padding-left: 50px;"></asp:TextBox>
                        </div>

                        <div class="pnl_input col">
                            <label style="position: absolute; margin-top: -20px; margin-left: 10px; font-size: 12px; font-weight: bold;">DEPARTAMENTO:</label>
                            <asp:DropDownList runat="server" ID="drop_depart">
                                <asp:ListItem Value="25">CUNDINAMARCA</asp:ListItem>
                                <asp:ListItem Value="11">BOGOTÁ D.C.</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="content row">
                        <div class="pnl_input col">
                            <label style="position: absolute; margin-top: -20px; margin-left: 10px; font-size: 12px; font-weight: bold;">CIUDAD:</label>
                            <asp:DropDownList runat="server" ID="drop_ciudad">
                            </asp:DropDownList>
                        </div>
                        <div class="pnl_input col" id="box_drop_local" style="display: none;">
                            <label style="position: absolute; margin-top: -20px; margin-left: 10px; font-size: 12px; font-weight: bold;">LOCALIDAD:</label>
                            <asp:DropDownList runat="server" ID="drop_local">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="content row">
                        <div class="pnl_input col" id="box_drop_barrio" style="display: none;">
                            <label style="position: absolute; margin-top: -20px; margin-left: 10px; font-size: 12px; font-weight: bold;">BARRIO:</label>
                            <asp:DropDownList runat="server" ID="drop_barrio">
                            </asp:DropDownList>
                        </div>
                        <div class="pnl_input col">
                            <label style="position: absolute; margin-top: -20px; margin-left: 10px; font-size: 12px; font-weight: bold;">ESTADO CIVIL:</label>
                            <asp:DropDownList runat="server" ID="drop_estado"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="content row">
                        <div class="pnl_input col">
                            <label style="position: absolute; margin-top: -20px; margin-left: 10px; font-size: 12px; font-weight: bold;">ESTRATO:</label>
                            <asp:DropDownList runat="server" ID="drop_estrato"></asp:DropDownList>
                        </div>
                        <div class="pnl_input col">
                            <label style="position: absolute; margin-top: -20px; margin-left: 10px; font-size: 12px; font-weight: bold;">GRUPO ÉTNICO:</label>
                            <asp:DropDownList runat="server" ID="drop_etnico"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="content row">
                        <div class="pnl_input col">
                            <label style="position: absolute; margin-top: -20px; margin-left: 10px; font-size: 12px; font-weight: bold;">GENERO:</label>
                            <asp:DropDownList runat="server" ID="drop_genero"></asp:DropDownList>
                        </div>
                        <div class="pnl_input col">
                            <label style="position: absolute; margin-top: -20px; margin-left: 10px; font-size: 12px; font-weight: bold;">VIVIENDA:</label>
                            <asp:DropDownList runat="server" ID="drop_vivienda"></asp:DropDownList>
                        </div>
                        <div class="pnl_input col">
                            <label style="position: absolute; margin-top: -20px; margin-left: 10px; font-size: 12px; font-weight: bold;">HOBBIE:</label>
                            <i class="fas fa-dice"></i>
                            <asp:TextBox runat="server" ID="txt_hobbie_act" placeholder="HOBBIE"></asp:TextBox>
                        </div>
                    </div>
                    <button type="button" id="lnk_actualizar" class="lnk_btn_modal btn-modal-actualizar">Actualizar</button>
                </section>

            </div>
        </div>
    </div>

    <!--MODAL CONTRASEÑA-->
    <div class="modal-i-gl modal-i-gl-hide animated fadeIn" id="modal_cambiar_pass">
        <div class="modal-i-gl-body">
            <div class="modal-i-gl-title">
                <h1 class="title">Cambiar contraseña actual</h1>
                <div class="modal-i-gl-cerrar">
                    <button type="button" class="btn-modal-close"><i class="fas fa-times"></i></button>
                </div>
            </div>
            <div class="modal-i-gl-content">

                <!--Aquí el contenido-->
                <section class="box_content_crear_vista">
                    <div class="content row">
                        <div class="pnl_input col">
                            <label style="position: absolute; margin-top: -20px; margin-left: 10px; font-size: 12px; font-weight: bold;">CONTRASEÑA ACTUAL:</label>
                            <i class="fas fa-lock-open"></i>
                            <asp:TextBox runat="server" ID="txt_actual_pass" type="password" placeholder="CONTRASEÑA ACTUAL"></asp:TextBox>
                        </div>
                        <div class="col"></div>
                    </div>
                    <div class="content row">
                        <div class="pnl_input col">
                            <label style="position: absolute; margin-top: -20px; margin-left: 10px; font-size: 12px; font-weight: bold;">CONTRASEÑA NUEVA:</label>
                            <i class="fas fa-lock"></i>
                            <asp:TextBox runat="server" ID="txt_nueva_pass" type="password" placeholder="CONTRASEÑA NUEVA"></asp:TextBox>
                        </div>
                        <div class="pnl_input col">
                            <label style="position: absolute; margin-top: -20px; margin-left: 10px; font-size: 12px; font-weight: bold;">
                                CONFIRMAR CONTRASEÑA NUEVA:
                                <span id="icon_verf_pass" class="far fa-check-circle correct"><small id="txt_verificacion" style="font-family: Arial, Helvetica, sans-serif;">Verificado</small></span>
                            </label>
                            <i class="fas fa-lock"></i>
                            <asp:TextBox runat="server" ID="txt_conf_pass" type="password" placeholder="CONFIRMAR CONTRASEÑA"></asp:TextBox>
                        </div>
                    </div>
                    <button type="button" id="lnk_cambiar" class="lnk_btn_modal btn-modal-cambiar">Cambiar</button>
                </section>

            </div>
        </div>
    </div>

    <!--MODAL DATOS CONTACTO EMERGENCIA-->
    <div class="modal-i-gl modal-i-gl-hide animated fadeIn" id="modal_datos_emergencia">
        <div class="modal-i-gl-body">
            <div class="modal-i-gl-title">
                <h1 class="title">Actualizar información contacto emergencia</h1>
                <div class="modal-i-gl-cerrar">
                    <button type="button" class="btn-modal-close"><i class="fas fa-times"></i></button>
                </div>
            </div>
            <div class="modal-i-gl-content">

                <!--Aquí el contenido-->
                <section class="box_content_crear_vista">
                    <div class="content row">
                        <div class="pnl_input col">
                            <label style="position: absolute; margin-top: -20px; margin-left: 10px; font-size: 12px; font-weight: bold;">NOMBRES:</label>
                            <i class="fas fa-mobile-alt"></i>
                            <asp:TextBox runat="server" ID="txt_nombre_contacto" placeholder="NOMBRES" MaxLength="50" onkeypress="return sololetras(event)"></asp:TextBox>
                        </div>
                        <div class="pnl_input col">
                            <label style="position: absolute; margin-top: -20px; margin-left: 10px; font-size: 12px; font-weight: bold;">APELLIDOS:</label>
                            <i class="fas fa-mobile-alt"></i>
                            <asp:TextBox runat="server" ID="txt_apellido_contacto" placeholder="APELLIDOS" MaxLength="50" onkeypress="return sololetras(event)"></asp:TextBox>
                        </div>
                        <div class="pnl_input col">
                            <label style="position: absolute; margin-top: -20px; margin-left: 10px; font-size: 12px; font-weight: bold;">PARENTESCO:</label>
                            <asp:DropDownList runat="server" ID="drop_parentesco"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="content row">
                        <div class="pnl_input col">
                            <label style="position: absolute; margin-top: -20px; margin-left: 10px; font-size: 12px; font-weight: bold;">NÚMERO DE CONTACTO:</label>
                            <i class="fas fa-map-marked-alt"></i>
                            <asp:TextBox runat="server" ID="txt_number_contacto" placeholder="NÚMERO DE CONTACTO" MaxLength="10" CssClass="input-number"></asp:TextBox>
                        </div>

                        <div class="pnl_input col">
                            <label style="position: absolute; margin-top: -20px; margin-left: 10px; font-size: 12px; font-weight: bold;">DIRECCIÓN RESIDENCIAL:</label>
                            <button type="button" class="btn_cambiar_address btn-address" data-id="MainContent_txt_direc_contacto">
                                <span class="fas fa-cog"></span>
                            </button>
                            <i class="fas fa-map-marked-alt"></i>
                            <asp:TextBox runat="server" ID="txt_direc_contacto" placeholder="DIRECCIÓN RESIDENCIAL" Enabled="false" Style="padding-left: 50px;"></asp:TextBox>
                        </div>
                    </div>
                    <button type="button" id="lnk_actualizar_contacto" class="lnk_btn_modal btn-modal-actualizar-contacto">Actualizar</button>
                </section>

            </div>
        </div>
    </div>

    <!--MODAL ACTUALIZAR FOTO-->
    <div class="modal-i-gl modal-i-gl-hide animated fadeIn" id="modal_actualizar_foto">
        <div class="modal-i-gl-body modal-i-gl-body-small">
            <div class="modal-i-gl-title">
                <h1 class="title">Actualizar foto de perfil</h1>
                <div class="modal-i-gl-cerrar">
                    <button type="button" class="btn-modal-close"><i class="fas fa-times"></i></button>
                </div>
            </div>
            <div class="modal-i-gl-content">

                <!--Aquí el contenido-->
                <section class="box_content_crear_vista">
                    <!--<p class="txt-img-pre"></p>-->
                    <div class="box-img-pre">
                        <img id="img_prev" src="../../Content/img/perfilD.png" class="img-pre" />
                    </div>
                    <label for="MainContent_file_foto" class="btn_cargar_foto">
                        <i class="fas fa-cloud-upload-alt"></i>
                        <small>Seleccionar imagen</small>
                    </label>
                    <asp:FileUpload runat="server" ID="file_foto" CssClass="file-foto-pf" Style="display: none;" accept="image/png, image/gif, image/jpeg, image/jfif" />
                    <br />
                    <br />
                    <asp:LinkButton runat="server" ID="lnk_actualizar_foto" Style="display: none;" OnClick="lnk_actualizar_foto_Click">Actualizar</asp:LinkButton>
                </section>
                <div>
                        *Recuerda que únicamente podrás cargar imagenes con extensión '.jpg', '.png' ó '.jfif' con un tamaño máximo de 2MB.
                </div>
                    
            </div>
        </div>
    </div>

    <!--Modal address-->
    <div id="address_card_i" class="address-card-i animated fadeIn" style="display: none;">
        <div class="address-card-i-body">
            <button type="button" class="btn-address-close"><i class="fas fa-times"></i></button>
            <div class="row">
                <div class="col" style="margin-right: 5px; max-width: max-content; padding: 0px;">
                    <select id="tipo_i" class="address-card-i-element">
                        <option selected value="">-Tipo vía-</option>
                    </select>
                </div>
                <div class="col" style="margin-right: 5px; max-width: max-content; padding: 0px;">
                    <input type="text" id="num_a" placeholder="N°" class="address-card-i-element" autocomplete="off" />
                </div>
                <div class="col" style="margin-right: 5px; max-width: max-content; padding: 0px;">
                    <select id="letra_i" class="address-card-i-element">
                        <option selected value="">-Letra-</option>
                    </select>
                </div>
                <div class="col" style="margin-right: 5px; max-width: max-content; padding: 0px;">
                    <select id="especial_i" class="address-card-i-element">
                        <option selected value="">-Bis-</option>
                    </select>
                </div>
                <div class="col" style="margin-right: 5px; max-width: max-content; padding: 0px;">
                    <select id="letra_i_b" class="address-card-i-element">
                        <option selected value="">-Letra-</option>
                    </select>
                </div>
                <div class="col" style="margin-right: 5px; max-width: max-content; padding: 0px;">
                    <select id="cuadrante_i" class="address-card-i-element">
                        <option selected value="">-Cuadrante-</option>
                    </select>
                </div>
                <div class="col" style="margin-right: 5px; max-width: max-content; padding: 0px;">
                    <span>#</span>
                    <input type="text" id="num_b" placeholder="N°" class="address-card-i-element" autocomplete="off" />
                </div>
                <div class="col" style="margin-right: 5px; max-width: max-content; padding: 0px;">
                    <select id="letra_a_i" class="address-card-i-element">
                        <option selected value="">-Letra-</option>
                    </select>
                </div>
                <div class="col" style="margin-right: 5px; max-width: max-content; padding: 0px;">
                    <span>-</span>
                    <input type="text" id="num_c" placeholder="N°" class="address-card-i-element" autocomplete="off" />
                </div>
                <div class="col" style="margin-right: 5px; max-width: max-content; padding: 0px;">
                    <select id="cuadrante_a_i" class="address-card-i-element">
                        <option selected value="">-Cuadrante-</option>
                    </select>
                </div>
                <div class="col" style="padding: 0px;">
                    <button type="button" class="btn-agregar-address">Agregar</button>
                </div>
            </div>
            <div class="row">
                <div class="col" style="text-align: left; margin-top: 10px;">
                    <input type="text" id="txt_adicional" placeholder="Apartamento - Casa - local" class="address-card-i-element" autocomplete="off" style="max-width: 100%; width: 100%; text-align: center;" />
                </div>
            </div>

        </div>
        <asp:TextBox runat="server" ID="txt_ciudadUsuario" CssClass="suspender" />
        <asp:TextBox runat="server" ID="txt_localidadUsuario" CssClass="suspender" />
        <asp:TextBox runat="server" ID="txt_barrioUsuario" CssClass="suspender" />
    </div>

    <script defer>
        const img_user = document.querySelector('#img_user');
        const user_profile = document.querySelector('#MainContent_user_profile')
        const usuario = document.querySelector('#MainContent_img_user');
        const perfil = document.querySelector('#MainContent_lnk_actualizar_foto');
        const actualizarDatos = document.querySelector('#lnk_actualizar');
        const actualizarContacto = document.querySelector('#lnk_actualizar_contacto');
        const actualizarContraseña = document.querySelector('#lnk_cambiar');
        const dropLocalidad = document.querySelector('#MainContent_drop_local');
        const localidadActual = document.querySelector('#MainContent_txt_localidadUsuario');
        const botonActualizar = document.querySelector('.btn_actualizar_info');

        //Variables de botones
        const btn_agregar = document.querySelector('#btn_agregar');
        const a = [...document.querySelectorAll('a')];

        a.forEach(button => {
            button.addEventListener('click', () => {
                button.style.display = 'none';
            })
        })

        const txt_usu = document.querySelector('#MainContent_txt_usu');

        actualizarDatos.addEventListener('click', () => {
            actualizarDatos.style.display = 'none';
        });

        actualizarContacto.addEventListener('click', () => {
            actualizarContacto.style.display = 'none';
        })

        actualizarContraseña.addEventListener('click', () => {
            actualizarContraseña.style.display = 'none';
        })

        console.log(user_profile);
        console.log(`url(${img_user.childNodes[1].childNodes[0].src})`);
        user_profile.style.backgroundImage = `url(${img_user.childNodes[1].childNodes[0].src})`;

        function quitarPadding() {
            let doc = document.getElementById('container');
            doc.removeAttribute('style');
        }
        document.addEventListener('load', quitarPadding);
        window.addEventListener('load', quitarPadding);
    </script>

</asp:Content>
