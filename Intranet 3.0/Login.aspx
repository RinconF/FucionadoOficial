<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Intranet_3._0.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="scripts_css" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="scripts_js" runat="server">
    <link type="text/css" rel="stylesheet" href="/styles/css/login/login.css">
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
            // Activar el estado de toggle
            $('.row').addClass('active');
            limpiar_campos();
        }

        function mostrar_iniciar_sesion() {
            // Desactivar el estado de toggle
            $('.row').removeClass('active');
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
                    <!-- Toggle box para la animación de fondo -->
                    <div class="toggle-box"></div>
                    
                    <!-- Panel izquierdo (login) -->
                    <div class="toggle-panel toggle-left">
                        <h1>¡Bienvenido!</h1>
                        <div class="subtitle">MI ETIB</div>
                        <div class="box-logo"><img src="Content/img/logotipo_intranet.png" /></div>
                        <div class="group-image">
                            <img src="Content/img/Ilustracion_grupo.png" width="100%" />
                        </div>

                    </div>

                    <!-- Panel derecho (registro) -->
                    <div class="toggle-panel toggle-right">
                        <h1>¡Bienvenido de vuelta!</h1>
                        <div class="box-logo"><img src="Content/img/logotipo_intranet.png" /></div>
                        <div class="group-image">
                            <img src="Content/img/Ilustracion_grupo.png" width="100%" />
                        </div>
                    </div>

                    <!-- Contenedor de formularios -->
                    <div class="form-container">
                        <!-- FORMULARIO DE INICIO DE SESIÓN -->
                        <div class="iniciar_sesion">
                            <p class="title-login"><i class="fas fa-user-tie"></i>Iniciar sesión</p>
                            
                            <asp:Panel runat="server" DefaultButton="Button200">
                                <div class="txt-user-input">
                                    <asp:TextBox runat="server" ID="txt_user" placeholder="Número de Cédula" type="text" autocomplete="on"></asp:TextBox>
                                </div>
                                <asp:Button ID="Button200" runat="server" OnClick="Login_Datos" Style="display: none"/>
                            </asp:Panel>

                            <asp:Panel runat="server" DefaultButton="Button1">
                                <div class="pass-verif"> 
                                    <i class="fas fa-eye" onmouseover="mostrar_password(this)" onmouseout="ocultar_password(this)"></i>
                                    <asp:TextBox runat="server" ID="txt_pass" placeholder="Contraseña" type="password" autocomplete="on"></asp:TextBox>
                                </div>
                                <asp:Button ID="Button1" runat="server" OnClick="Login_Datos" Style="display: none"/>
                            </asp:Panel>

                            <asp:LinkButton runat="server" ID="btn_ingresar" AutoPostBack="false" OnClick="Login_Datos">Ingresar</asp:LinkButton>
                            
                            <button type="button" class="btn-modal btn-recuperar-pass" onclick="mostrar_restablecer_clave()">
                                <i class="fas fa-unlock-alt"></i> Recuperar contraseña 
                            </button>

                            <div class="social-icons">
                                <div class="social-icon" onclick="window.open('https://www.facebook.com/etibsas', '_blank')">
                                    <i class="fab fa-facebook-f"></i>
                                </div>
                                <div class="social-icon" onclick="window.open('https://www.instagram.com/etibsas?igsh=bXF4cHl4bDZjd3J4', '_blank')">
                                    <i class="fab fa-instagram"></i>
                                </div>
                                <div class="social-icon" onclick="window.open('https://co.linkedin.com/company/etib-sas', '_blank')">
                                    <i class="fab fa-linkedin"></i>
                                </div>
                            </div>
                        </div>

                        <%--FORMULARIO DE RESTABLECER CONTRASEÑA--%>
                        <div class="restablecer_clave">
                            <p class="title-login rc"><i class="fas fa-unlock-alt"></i>Restablecer contraseña</p>
                            
                            <asp:Panel runat="server" DefaultButton="Button2">
                                <div class="txt-user-input">
                                    <asp:TextBox runat="server" ID="txt_cc" placeholder="Número de Cédula" type="text" autocomplete="on"></asp:TextBox>
                                </div>
                                <asp:Button ID="Button2" runat="server" OnClick="Restablecer_Clave" Style="display: none"/>
                            </asp:Panel>

                            <asp:Panel runat="server" DefaultButton="Button3">
                                <div class="pass-verif">
                                    <i class="fas fa-eye" onmouseover="mostrar_fec_exp(this)" onmouseout="ocultar_fec_exp(this)"></i>
                                    <asp:TextBox runat="server" ID="txt_fec_exp" placeholder="Fecha de expedición (dd-mm-aaaa)" type="password" autocomplete="on"></asp:TextBox>
                                </div>
                                <asp:Button ID="Button3" runat="server" OnClick="Restablecer_Clave" Style="display: none"/>
                            </asp:Panel>
                            
                            <asp:LinkButton runat="server" ID="btn_restablecer" AutoPostBack="false" OnClick="Restablecer_Clave">Restablecer</asp:LinkButton>
                            
                            <button type="button" class="btn-modal btn-recuperar-pass" onclick="mostrar_iniciar_sesion()">
                                <i class="fas fa-backward"></i> Volver 
                            </button>

                            <div class="social-icons">
                                <div class="social-icon" onclick="window.open('https://www.facebook.com/etibsas', '_blank')">
                                    <i class="fab fa-facebook-f"></i>
                                </div>
                                <div class="social-icon" onclick="window.open('https://www.instagram.com/etibsas?igsh=bXF4cHl4bDZjd3J4', '_blank')">
                                    <i class="fab fa-instagram"></i>
                                </div>
                                <div class="social-icon" onclick="window.open('https://co.linkedin.com/company/etib-sas', '_blank')">
                                    <i class="fab fa-linkedin"></i>
                                </div>
                            </div>
                        </div>
                        <%--TERMINA FORMULARIO DE RESTABLECER CONTRASEÑA--%>
                    </div>
                </div>
                    <!-- Footer de versión fuera del contenedor -->
                <div class="login-footer">
                    MIETIB V<%: ConfigurationManager.AppSettings["AppVersion"] %> &copy; ETIB SAS <%: DateTime.Now.Year %>
                </div>
            </section>
                                    <div class="footer-text">
                            MIETIB V<%: ConfigurationManager.AppSettings["AppVersion"] %> &copy; ETIB SAS <%: DateTime.Now.Year %>
                        </div>
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
        if (btn_ingresar) {
            btn_ingresar.addEventListener('click', () => btn_ingresar.style.display = 'none');
        }
    </script>
</asp:Content>