<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Intranet_3._0.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="scripts_css" runat="server">
    <style>
        .body-login {
            background-image: url('Content/img/0001.jpg');
            background-position: center;
            background-repeat: no-repeat;
            background-size: 100% 100%;
            position: absolute;
            top: 0;
            bottom: 0;
            left: 0;
            right: 0;
        }

        .row {
            margin: 0px;
            padding: 0px;
            border-radius: 10px;
            margin-right: 500px;
            margin-left: 500px;
            margin-top: 200px;
            box-shadow: 2px 2px 5px rgba(0,0,0,.3);
        }

        @media screen and (max-width: 1366px) {
            .row {
                margin-top: 50px;
                margin-right: 200px;
                margin-left: 200px;
            }
        }

        /*modal address*/
        @media only screen and (max-width: 750px) {
            .row {
                margin-right: 0;
                margin-left: 0;
            }
        }

        .row .col {
            margin: 0px;
            padding: 0px;
        }


        .body-login .row .col:nth-child(1) {
            background: linear-gradient(90deg, rgba(31,43,55,1) 0%, rgba(36,58,80,1) 100%);
            border-radius: 10px 0px 0px 10px;
            color: #fff;
            padding: 50px 50px;
            box-shadow: 2px 2px 5px rgba(0,0,0,.3);
        }

            .body-login .row .col:nth-child(1) .title-login {
                font-size: 30px;
                font-weight: bold;
                text-transform: uppercase;
                padding: 20px 20px 5px 20px;
                margin: 0px;
                text-shadow: 2px 2px 5px rgba(0,0,0,.5);
                letter-spacing: 5px;
                text-align: center;
            }

            .body-login .row .col:nth-child(1) .title-login-sub {
                font-size: 30px;
                font-weight: normal;
                text-transform: uppercase;
                padding: 0px 20px 0px 20px;
                margin: 0px;
                margin-top: -10px;
                text-shadow: 2px 2px 5px rgba(0,0,0,.5);
                color: rgba(110, 130, 151,1);
                letter-spacing: 5px;
            }

            .body-login .row .col:nth-child(1) .hr-login {
                border-top: 3px solid #fff;
                width: 70%;
                margin-left: auto;
                margin-right: auto;
            }

            .body-login .row .col:nth-child(1) .content_login {
                text-transform: uppercase;
                padding: 20px 20px;
                margin: 0px;
                font-size: 15px;
            }


        .body-login .row .col:nth-child(2) {
            background: #f9f9f9;
            border: 1px solid #e1e1e1;
            border-radius: 0px 10px 10px 0px;
            color: rgba(110, 130, 151,.9);
            padding: 50px 50px;
            text-align: center;
        }

            .body-login .row .col:nth-child(2) .title-login {
                font-size: 30px;
                font-weight: bold;
                text-transform: uppercase;
                padding: 20px 20px 0px 20px;
                margin: 0px;
            }

            .body-login .row .col:nth-child(2) .hr-login {
                border-top: 3px solid rgba(110, 130, 151,.9);
                width: 5rem;
            }

            .body-login .row .col:nth-child(2) input {
                border: none;
                border-bottom: 1px solid rgba(110, 130, 151,.5);
                background: none;
                color: rgba(110, 130, 151,.9);
                padding: 20px 20px 10px 20px;
                max-width: 100%;
                width: 80%;
                margin: 10px;
                outline: none;
                transition: all 0.5s;
            }

                .body-login .row .col:nth-child(2) input:focus {
                    border-bottom: 1px solid rgba(255, 112, 82, 1);
                    color: rgba(255, 112, 82, 1);
                }

                .body-login .row .col:nth-child(2) input:hover {
                    border-bottom: 1px solid rgba(255, 112, 82, 1);
                    color: rgba(255, 112, 82, 1);
                }

            .body-login .row .col:nth-child(2) a {
                border: 1px solid rgba(255, 112, 82, 1);
                color: rgba(255, 112, 82, 1);
                text-decoration: none;
                border-radius: 50px;
                padding: 10px 30px;
                margin: auto;
                text-align: center;
                text-transform: uppercase;
                opacity: .5;
                transition: all 0.5s;
            }

                .body-login .row .col:nth-child(2) a:hover {
                    opacity: 1;
                }

                .body-login .row .col:nth-child(2) a:active {
                    box-shadow: 2px 2px 5px inset rgba(0,0,0,.3);
                }

            .body-login .row .col:nth-child(2) .btn-recuperar-pass {
                border: none;
                background: none;
                text-transform: uppercase;
                margin-top: 10px;
                font-size: 12px;
                color: rgba(255, 112, 82, 1);
                outline: none;
            }

            .body-login .row .col:nth-child(2) .pass-verif {
                position: relative;
                margin-bottom: 30px;
            }

                .body-login .row .col:nth-child(2) .pass-verif i {
                    position: absolute;
                    right: 50px;
                    top: 35px;
                    cursor: pointer;
                    color: rgba(110, 130, 151,.5);
                    transition: all 0.5s;
                }

                    .body-login .row .col:nth-child(2) .pass-verif i:hover {
                        color: rgba(255, 112, 82, 1);
                    }


        .box-logo {
            border-style: none;
            width: 100px;
            position: absolute;
            right: -26px;
            margin-right: -20px;
            z-index: 2000;
            background: #fff;
            padding: 10px 20px;
            border-radius: 50%;
            top: 180px;
            border: 1px solid #e1e1e1;
            box-shadow: 2px 2px 5px inset rgba(0,0,0,.3);
        }

            .box-logo img {
                width: 100%;
            }

        .modal__contacto__admin {
            font-size: 16px;
            font-weight: bold;
            width: 100%;
            margin: auto;
            margin-bottom: 50px;
            color: rgb(110, 130, 151,1);
        }

        .rc {
            color: rgba(36,58,80,1);
        }

    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="scripts_js" runat="server">
    <script>
        $(document).ready(function () {
            $("#header_").attr("style", "display:none;");
            $("#modal_aviso_politicas").removeClass("modal-i-gl-show").addClass("modal-i-gl-hide");

            //RESTABLECER CONTRASEÑA
            $("body").on("click", "#lnk_restablecer", function () {
                $(".modal-noti").addClass("modal-noti-hide");
                $(".modal-noti").removeClass("modal-noti-show");
            });
        });
        function sesion() {
            //notificación
            $('.modal-noti').addClass('modal-noti-show');//agregar
            $('.modal-noti').removeClass('modal-noti-hide');//quitar
            $('.body-noti').addClass('advert'); //tipo notificación
            $('.title-noti').html('<span class="far fa-clock"></span> Sesión advertencia');//título
            $('.content-noti').html('¡Usuario ya tiene una sesión iniciada!');//mensaje
            setTimeout(function () {
                $('.modal-noti').addClass('modal-noti-hide');
                $('.modal-noti').removeClass('modal-noti-show');
            }, 4000);
            limpiar_campos();
        }
        function vacio(recargar) {
            //notificación 
            //se agrega al final de la funcion "recargar" y se recibe como parametro ya que sin esta, siempre se va a recargar en el inicio aunque esté en restablecer contraseña JGC
            $('.modal-noti').addClass('modal-noti-show');//agregar
            $('.modal-noti').removeClass('modal-noti-hide');//quitar
            $('.body-noti').addClass('advert'); //tipo notificación
            $('.title-noti').html('<span class="far fa-clock"></span> Sesión advertencia');//título
            $('.content-noti').html('¡Campos no pueden estar vacíos!');//mensaje
            setTimeout(function () {
                $('.modal-noti').addClass('modal-noti-hide');
                $('.modal-noti').removeClass('modal-noti-show');
            }, 4000);
            recargar;
            limpiar_campos();
        }
        function validar() {
            //notificación
            $('.modal-noti').addClass('modal-noti-show');//agregar
            $('.modal-noti').removeClass('modal-noti-hide');//quitar
            $('.body-noti').addClass('advert'); //tipo notificación
            $('.title-noti').html('<span class="fas fa-exclamation-circle"></span> Validación advertencia');//título
            $('.content-noti').html('¡El usuario ingresado se encuentra inactivo!');//mensaje
            setTimeout(function () {
                $('.modal-noti').addClass('modal-noti-hide');
                $('.modal-noti').removeClass('modal-noti-show');
            }, 7000);
            limpiar_campos();
        }
        function validar_datos() {
            //notificación
            $('.modal-noti').addClass('modal-noti-show');//agregar
            $('.modal-noti').removeClass('modal-noti-hide');//quitar
            $('.body-noti').addClass('advert'); //tipo notificación
            $('.title-noti').html('<span class="fas fa-exclamation-circle"></span> Validación advertencia');//título
            $('.content-noti').html('¡Usuario y/o contraseña incorrectos!');//mensaje
            setTimeout(function () {
                $('.modal-noti').addClass('modal-noti-hide');
                $('.modal-noti').removeClass('modal-noti-show');
            }, 7000);
            limpiar_campos();
        }

        //se crea funcion "rc" de Restablecer Clave para que al enviar la alerta no devuelva a Iniciar Sesión JGC
        function validar_datos_rc() {
            //notificación
            $('.modal-noti').addClass('modal-noti-show');//agregar
            $('.modal-noti').removeClass('modal-noti-hide');//quitar
            $('.body-noti').addClass('advert'); //tipo notificación
            $('.title-noti').html('<span class="fas fa-exclamation-circle"></span> Validación advertencia');//título
            $('.content-noti').html('¡Fecha de expedición incorrecta, recuerde digitarla con el guion! Ejemplo: 31-12-2000');//mensaje
            setTimeout(function () {
                $('.modal-noti').addClass('modal-noti-hide');
                $('.modal-noti').removeClass('modal-noti-show');
            }, 8000);
            mostrar_restablecer_clave();
            limpiar_campos();
        }



        function validar_existe() {
            //notificación
            $('.modal-noti').addClass('modal-noti-show');//agregar
            $('.modal-noti').removeClass('modal-noti-hide');//quitar
            $('.body-noti').addClass('advert'); //tipo notificación
            $('.title-noti').html('<span class="fas fa-exclamation-circle"></span> Validación advertencia');//título
            $('.content-noti').html('¡El usuario ingresado no se encuentra registrado!');//mensaje
            setTimeout(function () {
                $('.modal-noti').addClass('modal-noti-hide');
                $('.modal-noti').removeClass('modal-noti-show');
            }, 7000);
            limpiar_campos();
        }

        function cerrarSesion() {
            //notificación
            $('.modal-noti').addClass('modal-noti-show');//agregar
            $('.modal-noti').removeClass('modal-noti-hide');//quitar
            $('.body-noti').addClass('advert'); //tipo notificación
            $('.title-noti').html('<span class="fas fa-exclamation-circle"></span> Validación advertencia');//título
            $('.content-noti').html('¡El usuario ingresado no se encuentra registrado!');//mensaje
            setTimeout(function () {
                $('.modal-noti').addClass('modal-noti-hide');
                $('.modal-noti').removeClass('modal-noti-show');
            }, 7000);
            limpiar_campos();
        }

        function validar_existe(recargar) {
            //notificación
            $('.modal-noti').addClass('modal-noti-show');//agregar
            $('.modal-noti').removeClass('modal-noti-hide');//quitar
            $('.body-noti').addClass('advert'); //tipo notificación
            $('.title-noti').html('<span class="fas fa-exclamation-circle"></span> Validación advertencia');//título
            $('.content-noti').html('¡El usuario ingresado no se encuentra registrado!');//mensaje
            setTimeout(function () {
                $('.modal-noti').addClass('modal-noti-hide');
                $('.modal-noti').removeClass('modal-noti-show');
            }, 7000);
            recargar;
            limpiar_campos();
        }

        function mostrar_restablecer_clave() {
            //cambio de opción
            $('.restablecer_clave').removeClass('modal hide');//quitar
            $('.iniciar_sesion').addClass('modal hide');//agregar
            limpiar_campos();
        }

        function mostrar_iniciar_sesion() {
            //cambio de opción
            $('.iniciar_sesion').removeClass('modal hide');//quitar
            $('.restablecer_clave').addClass('modal hide');//agregar
            limpiar_campos();
        }

        function limpiar_campos() {

            $('input[type="text"]').val('');
            $('input[type="password"]').val('');

        }

        //SE IMPLEMENTA EN EL FRONTEND EL EVENTO DE MOSTRAR CONTRASEÑA Y FECHA DE EXPEDICIÓN CUANDO SE PASA EL CURSOR SOBRE EL ICONO DE 'EYE'
        //YA QUE EN EL MASTER NO LAS TOMA 23/09/2022 - JGC

        //Mostrar password
        function mostrar_password(e) {
            var campo_password = document.getElementById('MainContent_txt_pass');
            campo_password.type = "text";
        }

        function ocultar_password(e) {
            var campo_password = document.getElementById('MainContent_txt_pass');
            campo_password.type = "password";
        }

        //Mostrar fecha expedición

        function mostrar_fec_exp(e) {
            var campo_fec_exp = document.getElementById('MainContent_txt_fec_exp');
            campo_fec_exp.type = "text";
        }

        function ocultar_fec_exp(e) {
            var campo_fec_exp = document.getElementById('MainContent_txt_fec_exp');
            campo_fec_exp.type = "password";
        }

    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel runat="server" ID="pnl_login">
        <ContentTemplate>
            <section class="body-login">
                <div class="row">
                    <div class="col">

                        <div class="box-logo">
                            <img src="Content/img/logotipo_intranet.png" />
                        </div>

                        <div class="col-tumb">
                            <p class="title-login">
                                <i class="fas fa-star" style="font-size: 12px; color: rgba(245,176,65, 1); position: absolute; margin-left: 37px; margin-top: -5px;"></i>
                                ¡Bienvenido!
                            </p>
                            <p class="title-login title-login-sub">
                                MI ETIB
                            </p>
                            <hr class="hr-login" />
                            <img src="Content/img/Ilustracion_grupo.png" width="100%" />
                            <p class="title-login" style="font-size: 10px;">
                                 MIETIB V<%: ConfigurationManager.AppSettings["AppVersion"] %> &copy; ETIB SAS <%: DateTime.Now.Year %> 
                            </p>
                        </div>

                    </div>
                    <div class="col">
                        <div class="iniciar_sesion">

                        <p class="title-login"><i class="fas fa-user-tie"></i>Inicio de sesión</p>
                        <hr class="hr-login" />
                        
                        <asp:Panel runat="server" DefaultButton="Button200">
                            <div class="txt-user-input">
                                <asp:TextBox runat="server" ID="txt_user" placeholder="Número de Cédula" type="text" autocomplete="on"></asp:TextBox>
                            </div>
                            <asp:Button ID="Button200" runat="server" OnClick="Login_Datos" Style="display: none"/>
                        </asp:Panel>

                        <asp:Panel runat="server" DefaultButton="Button1">
                            <div class="pass-verif"> <i class="fas fa-eye" onmouseover="mostrar_password(this)" onmouseout="ocultar_password(this)"></i>
                                <asp:TextBox runat="server" ID="txt_pass" placeholder="Contraseña" type="password" autocomplete="on"></asp:TextBox>
                            </div>
                            <asp:Button ID="Button1" runat="server" OnClick="Login_Datos" Style="display: none"/>
                        </asp:Panel>

                        <asp:LinkButton runat="server" ID="btn_ingresar" AutoPostBack="false" OnClick="Login_Datos">Ingresar</asp:LinkButton>
                        <br />
                        <br />
                        <button type="button" class="btn-modal btn-recuperar-pass" onclick="mostrar_restablecer_clave()"><i class="fas fa-unlock-alt"></i> Recuperar contraseña </button>

                        </div>

                        <%--ACA HAGO PRUEBAS--%>
                        <div class="restablecer_clave modal hide">
                        <p class="title-login rc"><i class="fas fa-user-tie"></i>Restablecer contraseña</p>
                        <hr class="hr-login" />
                        <asp:Panel runat="server" DefaultButton="Button2">
                            <div class="txt-user-input hide">
                                <asp:TextBox runat="server" ID="txt_cc" placeholder="Número de Cédula" type="text" autocomplete="on"></asp:TextBox>
                            </div>
                            <asp:Button ID="Button2" runat="server" OnClick="Restablecer_Clave" Style="display: none"/>
                        </asp:Panel>

                        <asp:Panel runat="server" DefaultButton="Button3">
                            <div class="pass-verif"><i class="fas fa-eye" onmouseover="mostrar_fec_exp(this)" onmouseout="ocultar_fec_exp(this)"></i>
                                <asp:TextBox runat="server" ID="txt_fec_exp" placeholder="Fecha de expedición (dd-mm-aaaa)" type="password" autocomplete="on"></asp:TextBox>
                            </div>
                            <asp:Button ID="Button3" runat="server" OnClick="Restablecer_Clave" Style="display: none"/>
                        </asp:Panel>
                        <br />
                        <asp:LinkButton runat="server" ID="btn_restablecer" AutoPostBack="false" OnClick="Restablecer_Clave">Restablecer</asp:LinkButton>
                        <br />
                        <br />
                        <button type="button" class="btn-modal btn-recuperar-pass" onclick="mostrar_iniciar_sesion()"><i class="fas fa-backward"></i> Volver </button>
                        </div>
                        <%--ACÁ LAS TERMINO--%>

                        
                    </div>


                </div>
            </section>
        </ContentTemplate>
    </asp:UpdatePanel>

    <!--modales-->
    <!--MODAL RECUPERAR CONTRASEÑA-->
    <%--<div class="modal-i-gl modal-i-gl-hide animated fadeIn" id="modal_recuperar_pass" style="z-index: 2000;">
        <div class="modal-i-gl-body modal-i-gl-body-small" style="min-width: 33%;">
            <div class="modal-i-gl-title">
                <h1 class="title">Recuperación de contraseña</h1>
                <div class="modal-i-gl-cerrar">
                    <button type="button" class="btn-modal-close"><i class="fas fa-times"></i></button>
                </div>
            </div>
            <div class="modal-i-gl-content">

                <!--Aquí el contenido-->                
                <section class="box_content_crear_vista">
                    <p class="modal-i-gl-content-text">
                        Por seguridad se hace la solicitud de su usuario y 
                        correo corporativo para poder validar la veracidad 
                        de los datos y así permitir el restablecimiento de 
                        su contraseña.
                    </p>
                    <div class="content row" style="display: block; margin-top: 0px; box-shadow: none;">
                        <div class="pnl_input col">
                            <label style="position:absolute;margin-top:-20px;margin-left:10px;font-size:12px;font-weight:bold;">
                                CÉDULA:
                            </label>
                            <i class="fas fa-id-card"></i>
                            <asp:TextBox runat="server" ID="txt_cc" CssClass="input-number" type="text" placeholder="NÚMERO DE CÉDULA"></asp:TextBox>
                        </div>
                        <div class="pnl_input col" style="margin-top: 30px;">
                            <label style="position:absolute;margin-top:-20px;margin-left:10px;font-size:12px;font-weight:bold;">
                                CORREO CORPORATIVO:
                            </label>
                            <i class="fas fa-envelope"></i>
                            <asp:TextBox runat="server" ID="txt_correo" type="text" placeholder="CORREO CORPORATIVO"></asp:TextBox>
                        </div>
                    </div>
                    <button type="button" id="lnk_restablecer" class="lnk_btn_modal btn-modal-restablecer" >RESTABLECER</button>
                </section>

            </div>
        </div>
    </div>--%>

   <%-- <div class="modal-i-gl modal-i-gl-hide animated fadeIn" id="modal_recuperar_pass" style="z-index: 2000;">
        <div class="modal-i-gl-body modal-i-gl-body-small" style="min-width: 33%;">
            <div class="modal-i-gl-title">
                <h1 class="title">Recuperación de contraseña</h1>
                <div class="modal-i-gl-cerrar">
                    <button type="button" class="btn-modal-close"><i class="fas fa-times"></i></button>
                </div>
            </div>
            <div class="modal-i-gl-content">
                <!--Aquí el contenido-->
                <section class="box_content_crear_vista">
                    <p class="modal-i-gl-content-text">
                        Para restablecer su contraseña, por favor comuniquese con el administrador del sistema.
                    </p>
                    <div class="modal__contacto__admin">
                        <p>Tel: 5082121 (Ext. 3800)</p>
                        <p>Correo: soporte.intranet@etib.com.co</p>
                    </div>
                    <button type="button" id="lnk_restablecer" class="lnk_btn_modal btn-modal-close" data-dismiss="modal">Aceptar</button>
                </section>

            </div>
        </div>
    </div>--%>


    <!--modales-->
    <!--MODAL RECUPERAR CONTRASEÑA-->
    <%--<div class="modal-i-gl modal-i-gl-hide animated fadeIn" id="modal_recuperar_pass" style="z-index: 2000;">
        <div class="modal-i-gl-body modal-i-gl-body-small" style="min-width: 33%;">
            <div class="modal-i-gl-title">
                <h1 class="title">Recuperación de contraseña</h1>
                <div class="modal-i-gl-cerrar">
                    <button type="button" class="btn-modal-close"><i class="fas fa-times"></i></button>
                </div>
            </div>
            <div class="modal-i-gl-content">--%>

                <!--Aquí el contenido-->                
               <%-- <section class="box_content_crear_vista">
                    <p class="modal-i-gl-content-text">
                        Por seguridad se hace la solicitud de la fecha de
                        expedición de su cédula para poder validar la veracidad 
                        de los datos y así permitir el restablecimiento de 
                        su contraseña.
                    </p>
                    <div class="content row" style="display: block; margin-top: 0px; box-shadow: none;">
                        <div class="pnl_input col">
                            <label style="position:absolute;margin-top:-20px;margin-left:10px;font-size:12px;font-weight:bold;">
                                CÉDULA:
                            </label>
                            <i class="fas fa-id-card"></i>
                            <asp:TextBox runat="server" ID="txt_cc" CssClass="input-number" type="text" placeholder="NÚMERO DE CÉDULA"></asp:TextBox>
                        </div>
                        <div class="pnl_input col" style="margin-top: 30px;">
                            <label style="position:absolute;margin-top:-20px;margin-left:10px;font-size:12px;font-weight:bold;">
                                FECHA DE EXPEDICIÓN DE LA CÉDULA (dd-mm-aaaa):
                            </label>
                            <i class="fas fa-calendar-day"></i>
                            <asp:TextBox runat="server" ID="txt_fec_exp" type="text" placeholder="Ej: 31-12-2000"></asp:TextBox>
                        </div>
                    </div>
                    
                    <p class="modal-i-gl-content-text" style="color: red;" >
                        Información incorrecta
                    </p>
                    
                    <asp:LinkButton runat="server" ID="btn_restablecer" class="lnk_btn_modal btn-modal-restablecer" AutoPostBack="false" OnClick="Restablecer_Clave">RESTABLECER</asp:LinkButton>
                </section>--%>

            <%--</div>
        </div>
    </div>--%>

    <script defer>
        const btn_ingresar = document.querySelector('#MainContent_btn_ingresar');

        btn_ingresar.addEventListener('click', () => btn_ingresar.style.display = 'none');
    </script>
</asp:Content>
