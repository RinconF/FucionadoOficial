<%@ Page Title="Home" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Intranet_3._0._Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="scripts_css" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="scripts_js" runat="server">
    <link rel="Stylesheet" href="/Styles/css/default/default.css" />
    <script>
        $(document).ready(function () {

            //eventos_encuesta_binaria();

            $("#pnl_navegacion").attr("style", "display: none;");

            cargar_noticias_slideshow();

            $("body").on("click", ".ver-mas", function () {
                window.location.href = "./Vistas/V_Comunicacion/V_Noticias.aspx";
            });
        });

        function cargar_noticias_slideshow() {

            let array = [];
            let index = 0;

            $.ajax({
                type: "POST",
                url: "WebService_Default.asmx/cargar_card_slideshow",
                data: '',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (resultado) {
                    let items = resultado.d;

                    $.each(items, async function (index, item) {
                        array[index] = item[1];
                        array[index] = item[1];
                    });


                    $(".img-slideshow").attr("style", "background-image: url(" + array[0] + ");");
                },
                error: function () {
                    alert("Error con el servidor");
                }
            });


            $('body').on('click', '.btn_prev', function () {

                index--;
                if (index < 0) {
                    index = array.length - 1;
                }
                $(".img-slideshow").attr("style", "background-image: url(" + array[index] + ");");
            });

            $('body').on('click', '.btn_next', function () {

                index++;
                index %= array.length;

                $(".img-slideshow").attr("style", "background-image: url(" + array[index] + ");");
            });

            let auto = setInterval(function () {
                index++;
                index %= array.length;
                $(".img-slideshow").attr("style", "background-image: url(" + array[index] + ");");
            }, 7000);

            let onOff = false;
            $(".pnl_slide_body").hover(function () {
                if (!onOff) {
                    onOff = true;
                    clearInterval(auto);
                    //$(".lbl_estado").removeAttr("style").attr("style", "opacity:1;");
                } else {
                    onOff = false;
                    //$(".lbl_estado").removeAttr("style").attr("style", "opacity:0;");
                    auto = setInterval(function () {
                        index++;
                        index %= array.length;
                        $(".img-slideshow").attr("style", "background-image: url(" + array[index] + ");");
                    }, 7000);
                }
            });
        }
        //PARA VIDEO INFORMATIVO
        function iniciar() {
            var boton = document.getElementById('btn-video-info');
            boton.addEventListener('click', presionar, false);
        }

        //window.addEventListener('load', iniciar, false);

        function presionar() {
            var video = document.getElementById('video-informativo');
            video.pause();
        }

        // Cierra el modal específico
        function cerrarModal(modalId) {
            document.getElementById(modalId).style.display = 'none';
        }

        // Muestra el modal de evaluación cuando el video termina
        function mostrarModalEvaluacion() {
            document.getElementById('modal-video').style.display = 'none';
            document.getElementById('modal-evaluacion2').style.display = 'block';
        }

        function volverAlVideo() {
            // Regresar al video
            document.getElementById('modal-evaluacion2').style.display = 'none';
            document.getElementById('modal-video').style.display = 'block';
        }

        // FUNCIÓN PARA VALIDAR CAMPOS DE CELULAR (TIPO 7)
        function validarCamposCelular() {
            const camposCelular = document.querySelectorAll('.encuesta-input-celular');
            camposCelular.forEach(campo => {
                // Validación en tiempo real
                campo.addEventListener('input', function (e) {
                    let valor = e.target.value;
                    const errorDiv = document.getElementById(`error-${e.target.id}`);

                    // Eliminar caracteres que no sean números
                    valor = valor.replace(/[^0-9]/g, '');
                    e.target.value = valor;

                    // Solo validar longitud si hay contenido
                    if (valor.length > 0) {
                        if (valor.length != 10) {
                            errorDiv.style.display = 'block';
                            e.target.style.borderColor = 'red';
                        } else {
                            errorDiv.style.display = 'none';
                            e.target.style.borderColor = '';
                        }
                    } else {
                        // Si está vacío, ocultar error
                        errorDiv.style.display = 'none';
                        e.target.style.borderColor = '';
                    }
                });

                // Validación al perder el foco (blur) 
                campo.addEventListener('blur', function (e) {
                    const valor = e.target.value;
                    const errorDiv = document.getElementById(`error-${e.target.id}`);

                    if (valor.length > 0 && (valor.length != 10)) {
                        errorDiv.style.display = 'block';
                        e.target.style.borderColor = 'red';
                        // Limpiar el campo si la validación falla al perder el foco
                        e.target.value = '';
                    }
                });

                // Prevenir pegado de caracteres inválidos
                campo.addEventListener('paste', function (e) {
                    e.preventDefault();
                    let paste = (e.clipboardData || window.clipboardData).getData('text');
                    paste = paste.replace(/[^0-9]/g, '');

                    if (paste.length === 10) {
                        e.target.value = paste;
                        e.target.dispatchEvent(new Event('input'));
                    } else {
                        // Limpiar el campo si el texto pegado no es válido
                        e.target.value = '';
                        const errorDiv = document.getElementById(`error-${e.target.id}`);
                        errorDiv.style.display = 'block';
                        e.target.style.borderColor = 'red';
                    }
                });

                // Prevenir entrada de teclas especiales
                campo.addEventListener('keypress', function (e) {
                    const char = String.fromCharCode(e.which);
                    if (!/[0-9]/.test(char)) {
                        e.preventDefault();
                    }
                });
            });
        }

        // FUNCIÓN PARA VALIDAR CAMPOS DE EMAIL (TIPO 8)
        function validarCamposEmail() {
            const camposEmail = document.querySelectorAll('.encuesta-input-correo');
            camposEmail.forEach(campo => {
                campo.addEventListener('blur', function (e) {
                    const valor = e.target.value.trim();
                    const errorDiv = document.getElementById(`error-${e.target.id}`);

                    if (valor === '') {
                        errorDiv.style.display = 'none';
                        e.target.style.borderColor = '';
                        return;
                    }

                    // Validación de formato de email
                    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
                    const esValido = emailRegex.test(valor) &&
                        !valor.startsWith('.') &&
                        !valor.endsWith('.') &&
                        !valor.startsWith('@') &&
                        !valor.endsWith('@') &&
                        !valor.includes(' ');

                    if (!esValido) {
                        errorDiv.style.display = 'block';
                        e.target.style.borderColor = 'red';
                        // Limpiar el campo si la validación falla
                        e.target.value = '';
                    } else {
                        errorDiv.style.display = 'none';
                        e.target.style.borderColor = '';
                    }
                });

                // Validación en tiempo real para espacios
                campo.addEventListener('input', function (e) {
                    if (e.target.value.includes(' ')) {
                        e.target.value = e.target.value.replace(/\s/g, '');
                    }
                });
            });
        }
    </script>

</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="Stylesheet" href="/Styles/css/default_encuestas/default_encuestas.css" />
    <section class="pnl_slide">
        <div class="pnl_slide_body">
            <div class="pnl_slide_img">
                <ul>
                    <li class="show animated fadeInRight" id="img_1">
                        <div class="img img-slideshow"></div>
                    </li>
                </ul>
            </div>
            <div class="pnl_slide_btn_next_prev">
                <button type="button" class="btn_prev" style="background-color: transparent; color: rgb(161 153 153 / 70%);"><i class="fas fa-angle-left"></i></button>
                <button type="button" class="btn_next" style="background-color: transparent; color: rgb(161 153 153 / 70%);"><i class="fas fa-angle-right"></i></button>
                <%--<p class="lbl_estado" style="opacity: 0;">Pausado</p>--%>
            </div>
        </div>
    </section>

    <section class="pnl_table">
        <div class="pnl_tag">
            <p><i class="fas fa-tag"></i>Lo más reciente</p>
        </div>

       <div class="body-content-card" id="pnl-body-content-card-reci">
        </div>
    </section>

    <section class="pnl_table">
        <div class="pnl_tag">
            <p><i class="fas fa-tag"></i>Publicaciones</p>
        </div>

        <div class="section">
            <div class="section__noticias" id="pnl-body-content-card"></div>
        </div>
        <div class="section__social">
    <div class="content__social">
        <div class="td-block-title-wrap">
            <h4 class="block-title td-block-title"><span class="td-pulldown-size">Redes</span></h4>
        </div>
        <div class="social__iconos">
            <a href="https://www.facebook.com/etibsas/" target="_blank">
                <i class="fab fa-facebook-f"></i>
            </a>
            <a href="https://twitter.com/ETIB_SAS?t=zJu0GlifSHITxHbRpYFm0w&s=08" target="_blank">
                <i class="fab fa-twitter"></i>
            </a>
            <a href="https://instagram.com/etibsas?igshid=YmMyMTA2M2Y=" target="_blank">
                <i class="fab fa-instagram"></i>
            </a>
        </div>
    </div>
</div>
    </section>

    <div class="modal_noticia" style="display: none;">
        <div class="modal_noticia_body">
            <div class="modal_noticia_content_" id="modal_noticia_content"></div>
        </div>
    </div>

    <!--MODAL AVISO POLITICAS-->
    <div class="modal-i-gl modal-i-gl-show animated fadeIn" id="modal_aviso_politicas" style="z-index: 2000;">
        <div class="modal-i-gl-body">
            <div class="modal-i-gl-title">
                <h1 class="title"><i class="fas fa-shield-alt"></i>Políticas de tratamiento de datos</h1>
                <div class="modal-i-gl-cerrar">
                    <!--<button type="button" class="btn-modal-close"><i class="fas fa-times"></i></button>-->
                </div>
            </div>
            <div class="modal-i-gl-content">

                <!--Aquí el contenido-->
                <section class="box_content_crear_vista" style="margin-bottom: 0px; padding-bottom: 0px; padding-top: 0px;">
                    <p class="modal-i-gl-content-text" style="margin-bottom: 20px;">
                        Sus datos personales serán tratados conforme a 
                            <strong>LA LEY 1581 DE 2012 Y NUESTRAS POLÍTICAS DE PROTECCIÓN Y TRATAMIENTO DE DATOS PERSONALES</strong>
                        disponibles en 
                            <a href="https://drive.google.com/file/d/14hFkbjcGmd0Q_J-A4ndCaz51g745qeXR/view?usp=sharing" target="_blank" style="font-size: 14px; border: none; padding: 0px 0px;">https://drive.google.com/file/d/14hFkbjcGmd0Q_J-A4ndCaz51g745qeXR/view?usp=sharing
                            </a>cualquier petición, queja o reclamo, revocatoria o supresión sobre datos personales puede realizarse al 
                            correo <a href="#" style="border: none; padding: 0px 0px;">protecciondedatos@etib.com.co.</a>
                    </p>

                    <button type="button" id="lnk_aceptar" onclick="validar_politicas();" class="lnk_btn_modal btn-modal-acepto" style="background: rgba(22,160,133,1); color: #fff; font-size: 15px;">Acepto</button>
                    <button type="button" id="lnk_no_aceptar" onclick="validar_politicas();" class="lnk_btn_modal btn-modal-no-acepto" style="font-size: 14px; color: #fff; border: none; background: #b92929;">No acepto</button>
                </section>

            </div>
        </div>
    </div>

    <!---MODAL EVALUACIONES-->
    <div class="d-none" id="modal-evaluacion">
        <!-- Modal 1: Video Informativo -->
        <div class="modal-i-gl modal-i-gl-show animated fadeIn" id="modal-video">
            <div class="modal-i-gl-body">
                <div class="modal-i-gl-title ps-3 pe-3 pt-1 pb-1">
                    <h1 class="title" id="titulo_evaluaciones"><i class="fas fa-shield-alt"></i></h1>
                    <p style="text-align: center; margin: 0; margin-top: 5px;"><strong style="color: #b92929;">Por favor, visualice el video. Al finalizar, se habilitará una evaluación con respecto al video. </strong></p>
                </div>
                <div class="modal-i-gl-content d-flex justify-content-center">
                    <!-- Video -->
                   <div class="video-tumb d-flex justify-content-center" title="Evaluación de formación virtual" style="width: 80%">
                        <video id="video-informativo"
                            src="./Content/video/Id_Evaluacion_8.mp4"
                            controls autoplay
                            type="video/mp4"
                            onended="mostrarModalEvaluacion()">
                        </video>
                    </div>
                </div>
            </div>
        </div>

        <!-- Modal 2: Evaluación -->
        <div class="modal-i-gl modal-i-gl-hide animated fadeIn" id="modal-evaluacion2" display: none;">
            <div class="modal-i-gl-body">
                <div class="modal-i-gl-title">
                    <h1 class="title" id="titulo_evaluaciones1"><i class="fas fa-clipboard-list"></i></h1>
                </div>
                <div class="modal-i-gl-content">
                    <!-- Contenido de Evaluación -->
                    <label id="ingreso_evaluacion" style="display: none"></label>
                    <label id="evaluacion" style="display: none"></label>
                    <label id="competencia_eva" style="display: none"></label>
                    <!-- LABEL DE LA NOTA DE APROBCION DE LA EVALUACION -->
                    <section id="cuerpo_evaluacion" class="box_content_crear_vista" style="margin-bottom: 0px; padding-bottom: 150px; padding-top: 0px;">
                        <!--Aquí el contenido-->
                    </section>
                </div>
            </div>
        </div>
    </div>

    <%--    <!--MODAL VIDEO INFORMATIVO-->
      <div class="modal-i-gl modal-i-gl-show animated fadeIn" style="z-index: 1800;">
      <div class="modal-i-gl-body">
          <div class="modal-i-gl-title">
              <h1 class="title"><i class="fas fa-shield-alt"></i>Video Informativo</h1>
              <div class="modal-i-gl-cerrar">
                  <button id="btn-video-info" type="button" class="btn-modal-close"><i class="fas fa-times"></i></button>
              </div>
          </div>
           <div class="modal-i-gl-content">
               <div class="video-tumb d-flex justify-content-center" title="7 Pecados Operacionales">
                   <video id="video-informativo" src="./Content/video/11-  7 Pecados Operacionales.mp4" controls autoplay type="video/mp4"></video>
               </div>
          </div>
      </div>
  </div>--%>

    <%--        <!--MODAL IMAGEN INFORMATIVA MANTENIMIENTO DEL APLICATIVO-->
      <div class="modal-i-gl modal-i-gl-show animated fadeIn" style="z-index: 1800;">
      <div class="modal-i-gl-body">
          <div class="modal-i-gl-title">
              <h1 class="title"><i class="fas fa-shield-alt"></i>ADVERTENCIA: ESTA INFORMACIÓN ES IMPORTANTE</h1>
              <div class="modal-i-gl-cerrar">
                  <button id="btn-video-info" type="button" class="btn-modal-close"><i class="fas fa-times"></i></button>
              </div>
          </div>
           <div class="modal-i-gl-content">
               <div class="d-flex justify-content-center"><img src="Vistas/Img/Propuesta 3.png" style="max-height: 600px; width: 100%;" alt="imagen informativa"/></div>
          </div>
      </div>
    </div>--%>

    <!--MODAL ENCUESTAS-->
    <div class="modal-i-gl modal-i-gl-hide animated fadeIn" id="modal_encuestas" style="z-index: 2000;">
        <div class="modal-i-gl-body ">
            <div class="modal-i-gl-title">
                <h1 class="title" id="titulo_encuestas"><i class="fas fa-file-alt"></i></h1>
                <hr />
                <p style="text-align: center;"><strong style="color: #b92929;">¡Esta ventana no se cerrará hasta que la encuesta sea respondida!</strong></p>
                <!--<div class="modal-i-gl-cerrar">
                        <button type="button" class="btn-modal-close"><i class="fas fa-times"></i></button>
                    </div>-->
            </div>
            <div class="modal-i-gl-content" id="scroll">
                <div id="encuesta-descripcion">
                    <%-- DIGITAR LA DESCRIPCIÓN DE LA ENCUESTA EN ESTE ESPACIO SI SE REQUIERE: --%>                    
                    <p class="encuesta-aviso">

                        <%-- 1. Apartado dedicado a la presentación de imagenes --%> 
                        <%--<img src="Vistas/Img/Encuestas/Identificacion-Riesgo-Peligro.jpeg" style="width:100%"/>
                            <img src="Vistas/Img/Encuestas/Guia-Tecnica-Colombiana-GTC45.jpeg" style="width:100%"/>--%>

                        <%-- 2. Funcionalidad Para Presentar PDF --%>    
                        <%--<div id="encuesta-descripcion">
                            <embed src="Vistas/Img/Encuestas/INFORMATIVO-OBZ-OBA-PESV-EMIC-(Carta)-(002).pdf" width="100%" height="600px" type="application/pdf" />
                        </div>--%>

                        <%-- 3. Apartado dedicado a la presentación textos introductorios a las encuestas --%>  
                        Toda la información aquí consignada es de manejo confidencial, la cual es solo para fines estadísticos y de mejora para la toma de decisiones de la empresa. Sea muy honesto en sus respuestas, por lo tanto no existen respuestas buenas o malas, sino verdaderas.    
                    </p>
                </div>
                <label id="ingreso_encuesta" style="display: none"></label>
                <label id="encuesta" style="display: none"></label>
                <label id="competencia" style="display: none"></label>
                <section id="cuerpo_encuestas" class="box_content_crear_vista" style="margin-bottom: 0px; padding-bottom: 400px; padding-top: 0px;">
                    <!--Aquí el contenido-->
                </section>

            </div>
        </div>
    </div>

    <script defer>
        //Declaracion de variables de tipo modal
        const modalNoticiaContent = document.querySelector('#modal_noticia_content');

        const cargar_noticias_recientes = async () => {
            const noticias_recientes = document.querySelector('#pnl-body-content-card-reci');
            const res = await fetch(`WebService_Default.asmx/cargar_card_noticia_recientes`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: ''
            });
            const datos = await res.json();
            let nuevos = [...datos.d];
            const promises = nuevos.map(async nuevo => {
                const div = document.createElement('div');
                div.innerHTML = `
                    <div class="body-card body-card-reci ${nuevo[0]}">
                        <div class="card-title">
                            <span>Nuevo</span>
                        </div>
                        <div class="img-card">
                            <div class="img" style="background-image: url(${nuevo[3]});"></div>
                        </div>
                        <div class="content-card">
                            <p>
                                <i class="fas fa-tags"></i> ${nuevo[1]}
                            </p>
                        </div>
                    </div>
                `

                return div;
            });

            // Collect the DIVs in the order of the originally fetched data:
            const divs = await Promise.all(promises);
            // ...And populate the DOM
            for (const div of divs) noticias_recientes.appendChild(div);
        }

        const cargar_noticias = async () => {
            const noticias_recientes = document.querySelector('#pnl-body-content-card');
            const res = await fetch(`WebService_Default.asmx/cargar_card_noticia`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: ''
            });
            const datos = await res.json();
            let nuevos = [...datos.d];
            const promises = nuevos.map(async item => {
                const div = document.createElement('div');
                div.innerHTML = `
                    <div class="card__noticias btn-body-card card-${item[0]}">
                        <div class="card__content">
                            <div class=img-card>
                                <div class="img" style="background-image: url(${item[3]});"></div>
                            </div>
                            <div class="card__texto">
                                <div class="card__title">
                                    <p>${item[1]}</p>
                                    <p><i class="fas fa-tags"></i>${item[4]}</p>
                                </div>
                                <div class="sub-content-card">
                                    <p><i class="fas fa-align-left"></i>${item[2]}</p>
                                </div>
                            </div>
                        </div>
                    </div>
                `

                return div;
            });

            // Collect the DIVs in the order of the originally fetched data:
            const divs = await Promise.all(promises);
            // ...And populate the DOM
            for (const div of divs) noticias_recientes.appendChild(div);
        }

        const validarPoliticas = async () => {
            const aceptar_cookie = document.querySelector('#lnk_aceptar');
            const aceptar_no_cookie = document.querySelector('#lnk_no_aceptar');

            const params = new URLSearchParams(location.search);
            const idUsuario = params.get('Id_Usuario');

            if (idUsuario === null) {
                return;
            }
            const rest = await fetch(`WebService_Default.asmx/verificacionTratamientoDatos`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    idUsuario,
                })
            })

            const respuesta = await rest.json();
            if (respuesta.d[0][0] === '1') {
                $("#modal_aviso_politicas").removeClass("modal-i-gl-show").addClass("modal-i-gl-hide");
            };

            aceptar_cookie.addEventListener('click', async () => {
                await fetch(`WebService_Default.asmx/actualizarTratamientoDatos`, {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                    },
                    body: JSON.stringify({
                        idUsuario,
                        'accion': '1',
                    })
                });

                $("#modal_aviso_politicas").removeClass("modal-i-gl-show").addClass("modal-i-gl-hide");
            });

            aceptar_no_cookie.addEventListener('click', async () => {
                await fetch(`WebService_Default.asmx/actualizarTratamientoDatos`, {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                    },
                    body: JSON.stringify({
                        idUsuario,
                        'accion': '2',
                    })
                });

                $("#modal_aviso_politicas").removeClass("modal-i-gl-show").addClass("modal-i-gl-hide");
            });
        }

        //CONSUME WEB SERVICE PARA VALIDAR SI HAY EVALUACIONES HABILITADAS
        //SI NO HAY EVALUACIONES HABILITADAS, HAY MÁS DE UNA EVALUACIÓN HABILITADA O EL USUARIO YA FINALIZÓ LA EVALUACIÓN HABILITADA
        //NO MUESTRA EVALUACIONES, DE LO CONTRARIO MUESTRA LA EVALUACION HABILITADA
        const validarEvaluaciones = async () => {
            const paramsE = new URLSearchParams(location.search);
            const Int_Id_UsuarioE = paramsE.get('Id_Usuario');
            const restE = await fetch(`WebService_Default.asmx/validarEvaluaciones`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    Int_Id_UsuarioE
                })
            })

            const respuestaE = await restE.json();
            if (respuestaE.d == null) {
                console.log("Si espera visualizar una evaluación, valide: 1. ¿La evaluación está habilitada? 2. ¿Hay más de una evaluación habilitada? 3. ¿El Id_Info_Empleado existe?");
                let video = document.getElementById('video-informativo');
                video.pause();
            }
            else {
                const evaluaciones = document.querySelector('#cuerpo_evaluacion');
                $("#modal-evaluacion").removeClass("d-none");

                const titulo_evaluaciones = document.querySelector('#titulo_evaluaciones');
                titulo_evaluaciones.textContent = respuestaE.d[0];
                const titulo_evaluaciones1 = document.querySelector('#titulo_evaluaciones1');
                titulo_evaluaciones1.textContent = respuestaE.d[0];
                const ingreso_evaluacion = document.querySelector('#ingreso_evaluacion');
                const evaluacion = document.querySelector('#evaluacion');
                ingreso_evaluacion.textContent = respuestaE.d[2];
                evaluacion.textContent = respuestaE.d[3];

                //CUANDO LA ENCUESTA TIENE MÁS DE 10 PREGUNTAS LA DIVIDO EN GRUPOS PARA QUE SEA MÁS AMIGABLE CON EL USUARIO
                if (respuestaE.d[4].length > 10) {

                    let paginador = 0;
                    let paginador2 = 10;
                    let contador = 1;
                    let respuesta_copia = Array.from(respuestaE.d[4]).slice(paginador, paginador2);
                    let eva_multiple = false;

                    do {
                        const pagina = document.createElement('div');
                        pagina.setAttribute('id', 'pagina' + (contador));
                        //SE AÑADE EL CUADRO CON LA GUIA DE RESPUESTAS
                        const guia_respuestas = document.createElement('div');
                        guia_respuestas.setAttribute('class', 'guia-respuestas');
                        /*guia_respuestas.innerHTML = '<ul class="encuesta-ayuda-contenedor"><li class="encuesta-aviso encuesta-ayuda"><span class="encuesta-opcion">A</span>    Totalmente Satisfecho / Totalmente de acuerdo</li><li class="encuesta-aviso encuesta-ayuda"><span class="encuesta-opcion">B</span>    Satisfecho / De acuerdo</li><li class="encuesta-aviso encuesta-ayuda"><span class="encuesta-opcion">C</span>    Insatisfecho / En desacuerdo</li><li class="encuesta-aviso encuesta-ayuda"><span class="encuesta-opcion">D</span>    Totalmente Insatisfecho / Totalmente En desacuerdo</li></ul>'*/
                        pagina.appendChild(guia_respuestas);


                        for (let j = 0; j < respuesta_copia.length; j++) {
                            validar_competencia(respuesta_copia[j].competencia, pagina);
                            const div_preguntas = document.createElement('div');
                            div_preguntas.innerHTML = `
            <span class="encuesta-pregunta" >${respuesta_copia[j].numero_pregunta}. ${respuesta_copia[j].pregunta}</span>
        `;
                            div_preguntas.setAttribute('class', 'encuesta-pregunta');
                            const div_respuestas = document.createElement('div');
                            //TIPO DE RESPUESTA 1: RADIO BUTTON
                            if (respuesta_copia[j].id_tipo_respuesta == '1') {
                                eva_multiple = true;
                                var criterio_respuesta = Array.from(respuesta_copia[j].criterios_respuesta.split(","));
                                let k = 0;
                                criterio_respuesta.forEach(() => {
                                    const div_respuestas1 = document.createElement('div');
                                    let respuestaTexto = criterio_respuesta[k].split('-')[0];
                                    //SI EL CRITERIO DE RESPUESTA ES 'OTRO(A)S' PRESENTO UN TEXT AREA PARA QUE EL USUARIO OBLIGATORIAMENTE LO DIGITE, DE LO CONTRARIO NO
                                    if (criterio_respuesta[k] != "Otro(a)s") {
                                        div_respuestas1.innerHTML = `
                <input type="radio" class="form-check-input encuesta-radiobutton" id="${criterio_respuesta[k]}---${respuesta_copia[j].id_respuesta}" name="${respuesta_copia[j].id_respuesta}" value="${criterio_respuesta[k]}"><label class="encuesta-etiqueta" for="${criterio_respuesta[k]}---${respuesta_copia[j].id_respuesta}"> ${respuestaTexto}</label>
            `;
                                    }
                                    else {
                                        div_respuestas1.innerHTML = `
               <input type="radio" class="form-check-input encuesta-radiobutton" id="${criterio_respuesta[k]}---${respuesta_copia[j].id_respuesta}" name="${respuesta_copia[j].id_respuesta}" value="${criterio_respuesta[k]}"><label class="encuesta-etiqueta" for="${criterio_respuesta[k]}---${respuesta_copia[j].id_respuesta}"> ${respuestaTexto}</label>
                <div><textarea placeholder="${criterio_respuesta[k]}" maxlength="250" class="encuesta-text-area_otro" id="OTRO-TEXTO-${respuesta_copia[j].id_respuesta}" rows="5" cols="50" style="display:none"></textarea></div>
            `;
                                    }

                                    div_respuestas1.setAttribute('class', 'form-check');
                                    div_respuestas.appendChild(div_respuestas1);
                                    k++;
                                });
                                eva_multiple = false;
                                //TIPO DE RESPUESTA 2: TEXT
                            } else if (respuesta_copia[j].id_tipo_respuesta == '2') {
                                div_respuestas.innerHTML = `
            <textarea placeholder="Máximo 250 caracteres." maxlength="250" class="encuesta-text-area" id="${respuesta_copia[j].id_respuesta}" rows="10" cols="50"></textarea>
        `;
                            }
                            //TIPO DE RESPUESTA 3: CHECKBOX
                            else if (respuesta_copia[j].id_tipo_respuesta == '3') {
                                eva_multiple = true;
                                var criterio_respuesta = Array.from(respuesta_copia[j].criterios_respuesta.split(","));
                                let k = 0;
                                criterio_respuesta.forEach(() => {
                                    const div_respuestas1 = document.createElement('div');
                                    let respuestaTexto = criterio_respuesta[k].split('-')[0];
                                    //SI EL CRITERIO DE RESPUESTA ES 'OTRO(A)S' PRESENTO UN TEXT AREA PARA QUE EL USUARIO OBLIGATORIAMENTE LO DIGITE, DE LO CONTRARIO NO
                                    if (criterio_respuesta[k] != "Otro(a)s") {
                                        div_respuestas1.innerHTML = `
                    <input type="checkbox" class="form-check-input encuesta-checkbox" id="${criterio_respuesta[k]}___${respuesta_copia[j].id_respuesta}" name="${respuesta_copia[j].id_respuesta}" value="${criterio_respuesta[k]}"><label class="encuesta-etiqueta" for="${criterio_respuesta[k]}___${respuesta_copia[j].id_respuesta}"> ${respuestaTexto}</label>
                `;
                                    } else {
                                        div_respuestas1.innerHTML = `
                    <input type="checkbox" class="form-check-input encuesta-checkbox encuesta-checkbox_otro" id="${criterio_respuesta[k]}___${respuesta_copia[j].id_respuesta}" name="${respuesta_copia[j].id_respuesta}" value="${criterio_respuesta[k]}"><label class="encuesta-etiqueta" for="${criterio_respuesta[k]}___${respuesta_copia[j].id_respuesta}"> ${respuestaTexto}</label>
                    <div><textarea placeholder="${criterio_respuesta[k]}" maxlength="250" class="encuesta-text-area_otro" id="OTRO-TEXTO-${respuesta_copia[j].id_respuesta}" rows="5" cols="50" style="display:none"></textarea></div>
                `;
                                    }

                                    div_respuestas1.setAttribute('class', 'form-check');
                                    div_respuestas.appendChild(div_respuestas1);
                                    k++;
                                });
                                eva_multiple = false;
                            }

                            const hr = document.createElement('hr');

                            pagina.appendChild(div_preguntas);
                            pagina.appendChild(div_respuestas);
                            pagina.appendChild(hr);
                        }

                        if (Array.from(respuestaE.d[4]).slice(paginador + 10, paginador2 + 10).length > 0) {
                            const encuesta_boton_contenedor = document.createElement('div');
                            encuesta_boton_contenedor.innerHTML = `<button id="btn-volver-video" type="button" class="encuesta-boton_volver_video" onclick="volverAlVideo()"><i class="fas fa-video"></i> VOLVER AL VIDEO</button> <button class="encuesta-boton_enviar encuesta-boton_continuar" id="ir_a${contador + 1}">CONTINUAR</button>`;

                            // Mostrar botón "VOLVER" en las páginas superiores a 3, excepto en la primera
                            if (paginador > 2) {
                                encuesta_boton_contenedor.innerHTML = `<button id="btn-volver-video" type="button" class="encuesta-boton_volver_video" onclick="volverAlVideo()"><i class="fas fa-video"></i> VOLVER AL VIDEO</button> <button class="encuesta-boton_atras" id="ir_a${contador - 1}">VOLVER</button> <button class="encuesta-boton_enviar encuesta-boton_continuar" id="ir_a${contador + 1}">CONTINUAR</button>`;
                            }
                            pagina.appendChild(encuesta_boton_contenedor);
                            encuesta_boton_contenedor.setAttribute('class', 'encuesta-boton_contenedor');
                        } else {
                            const encuesta_boton_contenedor = document.createElement('div');
                            encuesta_boton_contenedor.innerHTML = `<button id="btn-volver-video" type="button" class="encuesta-boton_volver_video" onclick="volverAlVideo()"><i class="fas fa-video"></i> VOLVER AL VIDEO</button> <button class="encuesta-boton_atras" id="ir_a${contador - 1}">VOLVER</button> <button class="encuesta-boton_enviar" id="enviar-evaluacion">ENVIAR</button>`;
                            pagina.appendChild(encuesta_boton_contenedor);
                            encuesta_boton_contenedor.setAttribute('class', 'encuesta-boton_contenedor');
                        }


                        evaluaciones.appendChild(pagina);

                        if (contador == 1) {
                            pagina.setAttribute('class', 'modal-i-gl-show');
                        } else {
                            pagina.setAttribute('class', 'modal-i-gl-hide');
                        }

                        paginador = paginador2;
                        paginador2 += 10;
                        respuesta_copia = Array.from(respuestaE.d[4]).slice(paginador, paginador2);
                        contador++;

                    } while (respuesta_copia.length > 0)

                    if (!eva_multiple) {
                        const quitar_guia = document.getElementsByClassName('guia-respuestas');
                        for (var i = 0; i < quitar_guia.length; i++) {
                            quitar_guia[i].setAttribute('style', 'display: none');
                        }
                    }
                }
                else {
                    const eva_guia_respuestas = document.createElement('div');
                    eva_guia_respuestas.setAttribute('class', 'guia-respuestas');
                    /*guia_respuestas.innerHTML = '<ul class="encuesta-ayuda-contenedor"><li class="encuesta-aviso encuesta-ayuda"><span class="encuesta-opcion">A</span>    Totalmente Satisfecho / Totalmente de acuerdo</li><li class="encuesta-aviso encuesta-ayuda"><span class="encuesta-opcion">B</span>    Satisfecho / De acuerdo</li><li class="encuesta-aviso encuesta-ayuda"><span class="encuesta-opcion">C</span>    Insatisfecho / En desacuerdo</li><li class="encuesta-aviso encuesta-ayuda"><span class="encuesta-opcion">D</span>    Totalmente Insatisfecho / Totalmente En desacuerdo</li></ul>'*/
                    evaluaciones.appendChild(eva_guia_respuestas);
                    let eva_multiple = false;

                    for (let eva_r = 0; eva_r < respuestaE.d[4].length; eva_r++) {
                        validar_competencia_eva(respuestaE.d[4][eva_r].competencia, evaluaciones);
                        const div_preguntas = document.createElement('div');
                        div_preguntas.innerHTML = `
                                            <span class="encuesta-pregunta" >${eva_r + 1}. ${respuestaE.d[4][eva_r].pregunta}</span>
                                            `;
                        div_preguntas.setAttribute('class', 'encuesta-pregunta');
                        const div_respuestas = document.createElement('div');
                        //TIPO DE RESPUESTA 1: RADIO BUTTON
                        if (respuestaE.d[4][eva_r].id_tipo_respuesta == '1') {

                            eva_multiple = true;

                            var criterio_respuesta = Array.from(respuestaE.d[4][eva_r].criterios_respuesta.split(","));
                            let k = 0;
                            criterio_respuesta.forEach(() => {
                                const div_respuestas1 = document.createElement('div');
                                let respuestaTexto = criterio_respuesta[k].split('-')[0];
                                //SI EL CRITERIO DE RESPUESTA ES 'OTRO(A)S' PRESENTO UN TEXT AREA PARA QUE EL USUARIO OBLIGATORIAMENTE LO DIGITE, DE LO CONTRARIO NO
                                if (criterio_respuesta[k] != "Otro(a)s") {
                                    div_respuestas1.innerHTML = `
                                        <input type="radio" class="form-check-input encuesta-radiobutton" id="${criterio_respuesta[k]}---${respuestaE.d[4][eva_r].id_respuesta}" name="${respuestaE.d[4][eva_r].id_respuesta}" value="${criterio_respuesta[k]}"><label class="encuesta-etiqueta" for="${criterio_respuesta[k]}---${respuestaE.d[4][eva_r].id_respuesta}"> ${respuestaTexto}</label>
                                    `;
                                } else {
                                    div_respuestas1.innerHTML = `
                                       <input type="radio" class="form-check-input encuesta-radiobutton" id="${criterio_respuesta[k]}---${respuestaE.d[4][eva_r].id_respuesta}" name="${respuesta.d[4][eva_r].id_respuesta}" value="${criterio_respuesta[k]}"><label class="encuesta-etiqueta" for="${criterio_respuesta[k]}---${respuestaE.d[4][eva_r].id_respuesta}"> ${respuestaTexto}</label>
                                        <div><textarea placeholder="${criterio_respuesta[k]}" maxlength="250" class="encuesta-text-area_otro" id="OTRO-TEXTO-${respuestaE.d[4][eva_r].id_respuesta}" rows="5" cols="50" style="display:none"></textarea></div>
                                    `;
                                }
                                div_respuestas1.setAttribute('class', 'form-check');
                                div_respuestas.appendChild(div_respuestas1);
                                k++;
                            });
                            eva_multiple = false;
                        } //TIPO DE RESPUESTA 2: ABIERTA
                        else if (respuestaE.d[4][eva_r].id_tipo_respuesta == '2') {
                            div_respuestas.innerHTML = `
                            <textarea placeholder="Máximo 250 caracteres." maxlength="250" class="encuesta-text-area" id="${respuestaE.d[4][eva_r].id_respuesta}" rows="10" cols="50"></textarea>
                        `;
                        }//TIPO DE RESPUESTA 3: CHECKBOX
                        else if (respuestaE.d[4][eva_r].id_tipo_respuesta == '3') {
                            eva_multiple = true;

                            var criterio_respuesta = Array.from(respuestaE.d[4][eva_r].criterios_respuesta.split(","));
                            let k = 0;
                            criterio_respuesta.forEach(() => {
                                const div_respuestas1 = document.createElement('div');
                                let respuestaTexto = criterio_respuesta[k].split('-')[0];
                                //SI EL CRITERIO DE RESPUESTA ES 'OTRO(A)S' PRESENTO UN TEXT AREA PARA QUE EL USUARIO OBLIGATORIAMENTE LO DIGITE, DE LO CONTRARIO NO
                                if (criterio_respuesta[k] != "Otro(a)s") {
                                    div_respuestas1.innerHTML = `
                                                            <input type="checkbox" class="form-check-input encuesta-checkbox" id="${criterio_respuesta[k]}___${respuestaE.d[4][eva_r].id_respuesta}" name="${respuestaE.d[4][eva_r].id_respuesta}" value="${criterio_respuesta[k]}"><label class="encuesta-etiqueta" for="${criterio_respuesta[k]}___${respuestaE.d[4][eva_r].id_respuesta}"> ${respuestaTexto}</label>
                                                        `;
                                } else {
                                    div_respuestas1.innerHTML = `
                                                            <input type="checkbox" class="form-check-input encuesta-checkbox encuesta-checkbox_otro" id="${criterio_respuesta[k]}___${respuestaE.d[4][eva_r].id_respuesta}" name="${respuestaE.d[4][eva_r].id_respuesta}" value="${criterio_respuesta[k]}"><label class="encuesta-etiqueta" for="${criterio_respuesta[k]}___${respuestaE.d[4][eva_r].id_respuesta}"> ${respuestaTexto}</label>
                                                            <div><textarea placeholder="${criterio_respuesta[k]}" maxlength="250" class="encuesta-text-area_otro" id="OTRO-TEXTO-${respuestaE.d[4][eva_r].id_respuesta}" rows="5" cols="50" style="display:none"></textarea></div>
                                                        `;
                                }
                                div_respuestas1.setAttribute('class', 'form-check');
                                div_respuestas.appendChild(div_respuestas1);
                                k++;
                            });
                            eva_multiple = false;
                        }
                        evaluaciones.appendChild(div_preguntas);
                        evaluaciones.appendChild(div_respuestas);
                    }

                    if (respuestaE.d[4].length > 1 || (respuestaE.d[4].length == 1 && respuestaE.d[4][0].id_tipo_respuesta == '2') || eva_multiple) {
                        const evaluacion_boton_contenedor = document.createElement('div');
                        evaluacion_boton_contenedor.innerHTML = '<button id="btn-volver-video" type="button" class="encuesta-boton_volver_video" onclick="volverAlVideo()"><i class="fas fa-video"></i> VOLVER AL VIDEO</button> <button class="encuesta-boton_enviar" id="enviar-evaluacion">ENVIAR</button>';
                        evaluaciones.appendChild(evaluacion_boton_contenedor);
                        evaluacion_boton_contenedor.setAttribute('class', 'encuesta-boton_contenedor');
                    }

                    if (!eva_multiple) {
                        const quitar_guia = document.getElementsByClassName('guia-respuestas');
                        for (var i = 0; i < quitar_guia.length; i++) {
                            quitar_guia[i].setAttribute('style', 'display: none');
                        }
                    }
                }
                eventos_evaluacion_multiple();
                eventos_boton_continuar();
                eventos_boton_atras();
                cantidadNumero();
                inputOpcionOtra();
                limitarMaxCheckbox(3);
            }
        }

        function validar_competencia_eva(competencia_actual, pagina) {
            let competencia_eva = document.getElementById('competencia_eva');
            if (competencia_eva.textContent != competencia_actual) {
                competencia_eva.textContent = competencia_actual;

                if (competencia_actual != "Ninguno") {
                    const titulo_competencia = document.createElement('h2');
                    titulo_competencia.textContent = competencia_actual;
                    titulo_competencia.setAttribute('style', 'color: #6e8297; font-size: 2em; text-align: center; margin-top: 10px; margin-bottom: 20px;');
                    pagina.appendChild(titulo_competencia);
                }
            }
        }

        //AÑADE LA ESCUCHA DE EVENTOS AL BOTÓN 'ENVIAR' CUANDO LAS ENCUESTAS SON DE TIPO MULTIPLE O ABIERTAS
        function eventos_evaluacion_multiple() {
            var a = document.getElementById('enviar-evaluacion');
            a.addEventListener("click", responderEvaluacionMultiple)
        }

        //CONSUME WEB SERVICE PARA RESPONDER LAS EVALUACION DE TIPO MULTIPLE (A/B/C/D/TEXTO)
        async function responderEvaluacionMultiple(e) {
            e.preventDefault();
             const ingreso_evaluacion = document.querySelector('#ingreso_evaluacion').textContent;
            const evaluacion = document.querySelector('#evaluacion').textContent;

            // DEFINIMOS VARIABLE QUE SUMA PUNTOS DE LA EVALUACION
            let radios = document.querySelectorAll('input[type="radio"]:checked');
            let text_area = document.getElementsByClassName('encuesta-text-area');
            let input_date = document.getElementsByClassName('encuesta-input-date');
            let input_number = document.getElementsByClassName('encuesta-input-number');
            let check = document.querySelectorAll('input[type="checkbox"]:checked');
            let contador_check = 0;
            let valor_actual_check = 0;
            let array_check = [];
            let cantidad_checked = [];

            // VARIABLE QUE SUMA PUNTOS DE LA EVALUACION
            let suma_puntos = 0;
            parseFloat(suma_puntos);

            //RECORRO TODOS LOS RADIO BUTTON PARA VALIDAR QUE LOS QUE HAYAN SIDO SELECCIONADOS CON EL CRITERIO DE RESPUESTA 'OTRO(A)S' TENGAN DILIGENCIADO SU RESPECTIVO CAMPO TEXT AREA
            for (let i = 0; i < radios.length; i++) {
                let id_parts = radios[i].id.split('---');
                let puntos = parseFloat(id_parts[0].split('-')[1]); // Extraer el valor decimal
                suma_puntos += puntos; // Sumar los puntos

                if (radios[i].id.includes('Otro(a)s')) {
                    if (document.getElementById('OTRO-TEXTO-' + radios[i].name).value.trim() == "") {
                        alert('¡Si en alguna de las preguntas seleccionó Otro(a)s, debe digitar cuál(es)!');
                        return;
                    } else {
                        cantidad_checked.push('Otro(a)s: ' + document.getElementById('OTRO-TEXTO-' + radios[i].name).value.trim() + '---' + radios[i].name);
                    }
                } else {
                    cantidad_checked.push(radios[i].id);
                }
            }

            for (let i = 0; i < text_area.length; i++) {
                cantidad_checked.push(text_area[i].id + 'ENCUESTA-TEXTO' + text_area[i].value.trim());
            }

            for (let i = 0; i < input_date.length; i++) {
                cantidad_checked.push(input_date[i].id + 'ENCUESTA-TEXTO' + input_date[i].value.trim());
            }

            for (let i = 0; i < input_number.length; i++) {
                cantidad_checked.push(input_number[i].id + 'ENCUESTA-TEXTO' + input_number[i].value.trim());
            }

            for (let i = 0; i < check.length; i++) {
                let name = check[i].name;
                if (name != valor_actual_check) {
                    contador_check++;
                    valor_actual_check = name;

                    //RECORRO TODOS LOS CHECKBOX PARA VALIDAR QUE LOS QUE HAYAN SIDO SELECCIONADOS CON EL CRITERIO DE RESPUESTA 'OTRO(A)S' TENGAN DILIGENCIADO SU RESPECTIVO CAMPO TEXT AREA
                    if (check[i].id.includes('Otro(a)s')) {
                        if (document.getElementById('OTRO-TEXTO-' + check[i].name).value.trim() == "") {
                            alert('¡Si en alguna de las preguntas seleccionó Otro(a)s, debe digitar cuál(es)!');
                            return;
                        } else {
                            array_check.push('Otro(a)s: ' + document.getElementById('OTRO-TEXTO-' + check[i].name).value.trim() + '___' + check[i].name + '\\');
                        }
                    } else {
                        let id_parts_checks = check[i].id.split('___');
                        let puntos_check = parseFloat(id_parts_checks[0].split('-')[1]); // Extraer el valor decimal
                        suma_puntos += puntos_check; // Sumar los puntos
                        array_check.push(id_parts_checks[0].split('-')[0] + '___' + id_parts_checks[1] + '\\');
                    }
                } else {
                    //RECORRO TODOS LOS CHECKBOX PARA VALIDAR QUE LOS QUE HAYAN SIDO SELECCIONADOS CON EL CRITERIO DE RESPUESTA 'OTRO(A)S' TENGAN DILIGENCIADO SU RESPECTIVO CAMPO TEXT AREA
                    if (check[i].id.includes('Otro(a)s')) {
                        if (document.getElementById('OTRO-TEXTO-' + check[i].name).value.trim() == "") {
                            alert('¡Si en alguna de las preguntas seleccionó Otro(a)s, debe digitar cuál(es)!');
                            return;
                        } else {
                            array_check.push('Otro(a)s: ' + document.getElementById('OTRO-TEXTO-' + check[i].name).value.trim() + '___' + check[i].name + '\\');
                        }
                    } else {
                         let id_parts_checks = check[i].id.split('___');
                        let puntos_check = parseFloat(id_parts_checks[0].split('-')[1]); // Extraer el valor decimal
                        suma_puntos += puntos_check; // Sumar los puntos
                        array_check.push(id_parts_checks[0].split('-')[0] + '___' + id_parts_checks[1] + '\\');
                    }
                }
            }

            if (array_check.length != 0) {
                cantidad_checked.push(array_check.toString());
            }

            // Preguntar al usuario si está seguro de enviar las respuestas
            const confirmacion = confirm("¿Está seguro de que desea enviar las respuestas?");
            if (!confirmacion) {
                return;
            }
            else {
                const paramsE = new URLSearchParams(location.search);
                const Int_Id_UsuarioE = paramsE.get('Id_Usuario');//id del usuario
                //PARA RESPONDER LA EVALUACIÓN
                const rest = await fetch(`WebService_Default.asmx/responder_evaluacion_multiple`, {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                    },
                    body: JSON.stringify({
                        ingreso_evaluacion,
                        evaluacion,
                        cantidad_checked,
                        suma_puntos,
                        Int_Id_UsuarioE
                    })
                })
                const respuesta_json = await rest.json();
                //USUARIO APROBO
                if (respuesta_json.d.includes("Evaluación aprobada y enviada con éxito. Su calificación es: ")) {
                    document.getElementById('modal-evaluacion').style.display = 'none';
                    document.getElementById('modal-video').style.display = 'none';
                    alert(respuesta_json.d);
                //USUARIO NO APROBO Y NO TIENE MAS INTENTO
                } else if (respuesta_json.d.includes("2-Evaluación no aprobada. Su calificación es: ")) {
                    document.getElementById('modal-evaluacion').style.display = 'none';
                    let mensaje_separado = respuesta_json.d.split("-")
                    let mensaje = mensaje_separado[1]
                    alert(mensaje);
                //USUARIO AUN TIENE INTENTOS 
                } else if (respuesta_json.d.includes("1-Evaluación no aprobada. Su calificación es: ")) {
                    let mensaje_separado = respuesta_json.d.split("-")
                    let mensaje = mensaje_separado[1]
                    alert(mensaje);
                    location.reload();
                //EN CASO DE OTRO MENSAJE
                } else {
                    alert(respuesta_json.d);
                }
            }
        }

        //CONSUME WEB SERVICE PARA VALIDAR SI HAY ENCUESTAS HABILITADAS 
        //SI NO HAY ENCUESTAS HABILITADAS, HAY MÁS DE UNA ENCUESTA HABILITADA O EL USUARIO YA FINALIZÓ LA ENCUESTA HABILITADA
        //NO MUESTRA ENCUESTAS, DE LO CONTRARIO MUESTRA LA ENCUESTA HABILITADA
        const validarEncuestas = async () => {
            const params = new URLSearchParams(location.search);
            const Int_Id_Usuario = params.get('Id_Usuario');
            const rest = await fetch(`WebService_Default.asmx/validarEncuestas`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    Int_Id_Usuario,
                })
            })

            const respuesta = await rest.json();
            if (respuesta.d == null) {
                console.log("Si espera visualizar una encuesta, valide: 1. ¿La encuesta está habilitada? 2. ¿Hay más de una encuesta habilitada? 3. ¿El Id_Info_Empleado existe?");
            }
            else {
                const encuestas = document.querySelector('#cuerpo_encuestas');
                $("#modal_encuestas").removeClass("modal-i-gl-hide").addClass("modal-i-gl-show");

                const titulo_encuestas = document.querySelector('#titulo_encuestas');
                titulo_encuestas.textContent = respuesta.d[0];
                const ingreso_encuesta = document.querySelector('#ingreso_encuesta');
                const encuesta = document.querySelector('#encuesta');
                ingreso_encuesta.textContent = respuesta.d[2];
                encuesta.textContent = respuesta.d[3];

                //CUANDO LA ENCUESTA TIENE MÁS DE 10 PREGUNTAS LA DIVIDO EN GRUPOS PARA QUE SEA MÁS AMIGABLE CON EL USUARIO
                if (respuesta.d[4].length > 10) {

                    let paginador = 0;
                    let paginador2 = 10;
                    let contador = 1;
                    let respuesta_copia = Array.from(respuesta.d[4]).slice(paginador, paginador2);
                    let multiple = false;

                    do {
                        const pagina = document.createElement('div');
                        pagina.setAttribute('id', 'pagina' + (contador));
                        //SE AÑADE EL CUADRO CON LA GUIA DE RESPUESTAS
                        const guia_respuestas = document.createElement('div');
                        guia_respuestas.setAttribute('class', 'guia-respuestas');
                        pagina.appendChild(guia_respuestas);

                        for (let j = 0; j < respuesta_copia.length; j++) {
                            validar_competencia(respuesta_copia[j].competencia, pagina);
                            const div_preguntas = document.createElement('div');
                            div_preguntas.innerHTML = `
                    <span class="encuesta-pregunta" >${respuesta_copia[j].numero_pregunta}. ${respuesta_copia[j].pregunta}</span>
                `;
                            div_preguntas.setAttribute('class', 'encuesta-pregunta');
                            const div_respuestas = document.createElement('div');

                            //TIPO DE RESPUESTA 1: RADIO BUTTON
                            if (respuesta_copia[j].id_tipo_respuesta == '1') {
                                multiple = true;
                                var criterio_respuesta = Array.from(respuesta_copia[j].criterios_respuesta.split(","));
                                let k = 0;
                                criterio_respuesta.forEach(() => {
                                    const div_respuestas1 = document.createElement('div');
                                    //SI EL CRITERIO DE RESPUESTA ES 'OTRO(A)S' PRESENTO UN TEXT AREA PARA QUE EL USUARIO OBLIGATORIAMENTE LO DIGITE, DE LO CONTRARIO NO
                                    if (criterio_respuesta[k] != "Otro(a)s") {
                                        div_respuestas1.innerHTML = `
                        <input type="radio" class="form-check-input encuesta-radiobutton" id="${criterio_respuesta[k]}---${respuesta_copia[j].id_respuesta}" name="${respuesta_copia[j].id_respuesta}" value="${criterio_respuesta[k]}"><label class="encuesta-etiqueta" for="${criterio_respuesta[k]}---${respuesta_copia[j].id_respuesta}"> ${criterio_respuesta[k]}</label>
                    `;
                                    }
                                    else {
                                        div_respuestas1.innerHTML = `
                       <input type="radio" class="form-check-input encuesta-radiobutton" id="${criterio_respuesta[k]}---${respuesta_copia[j].id_respuesta}" name="${respuesta_copia[j].id_respuesta}" value="${criterio_respuesta[k]}"><label class="encuesta-etiqueta" for="${criterio_respuesta[k]}---${respuesta_copia[j].id_respuesta}"> ${criterio_respuesta[k]}</label>
                        <div><textarea placeholder="${criterio_respuesta[k]}" maxlength="250" class="encuesta-text-area_otro" id="OTRO-TEXTO-${respuesta_copia[j].id_respuesta}" rows="5" cols="50" style="display:none"></textarea></div>
                    `;
                                    }

                                    div_respuestas1.setAttribute('class', 'form-check');
                                    div_respuestas.appendChild(div_respuestas1);
                                    k++;
                                });
                                multiple = false;
                                //TIPO DE RESPUESTA 3: TEXT
                            } else if (respuesta_copia[j].id_tipo_respuesta == '3') {
                                div_respuestas.innerHTML = `
                    <textarea placeholder="Máximo 250 caracteres." maxlength="250" class="encuesta-text-area" id="${respuesta_copia[j].id_respuesta}" name="${respuesta_copia[j].id_pregunta}" rows="10" cols="50"></textarea>
                `;
                                //TIPO DE RESPUESTA 4: FECHA
                            } else if (respuesta_copia[j].id_tipo_respuesta == '4') {
                                div_respuestas.innerHTML = `
                    <input type="date" value="2020-01-01" class="encuesta-input-date" id="${respuesta_copia[j].id_respuesta}"/>
                `;
                            }
                            //TIPO DE RESPUESTA 5: NUMERO
                            else if (respuesta_copia[j].id_tipo_respuesta == '5') {
                                div_respuestas.innerHTML = `
                    <input type="number" class="encuesta-input-number max2000" min="0" max="2000" step="1" maxlength="7" id="${respuesta_copia[j].id_respuesta}"/>
                `;
                            }
                            //TIPO DE RESPUESTA 6: CHECKBOX
                            else if (respuesta_copia[j].id_tipo_respuesta == '6') {
                                multiple = true;
                                var criterio_respuesta = Array.from(respuesta_copia[j].criterios_respuesta.split(","));
                                let k = 0;
                                criterio_respuesta.forEach(() => {
                                    const div_respuestas1 = document.createElement('div');
                                    //SI EL CRITERIO DE RESPUESTA ES 'OTRO(A)S' PRESENTO UN TEXT AREA PARA QUE EL USUARIO OBLIGATORIAMENTE LO DIGITE, DE LO CONTRARIO NO
                                    if (criterio_respuesta[k] != "Otro(a)s") {
                                        div_respuestas1.innerHTML = `
                            <input type="checkbox" class="form-check-input encuesta-checkbox" id="${criterio_respuesta[k]}___${respuesta_copia[j].id_respuesta}" name="${respuesta_copia[j].id_respuesta}" value="${criterio_respuesta[k]}"><label class="encuesta-etiqueta" for="${criterio_respuesta[k]}___${respuesta_copia[j].id_respuesta}"> ${criterio_respuesta[k]}</label>
                        `;
                                    } else {
                                        div_respuestas1.innerHTML = `
                            <input type="checkbox" class="form-check-input encuesta-checkbox encuesta-checkbox_otro" id="${criterio_respuesta[k]}___${respuesta_copia[j].id_respuesta}" name="${respuesta_copia[j].id_respuesta}" value="${criterio_respuesta[k]}"><label class="encuesta-etiqueta" for="${criterio_respuesta[k]}___${respuesta_copia[j].id_respuesta}"> ${criterio_respuesta[k]}</label>
                            <div><textarea placeholder="${criterio_respuesta[k]}" maxlength="250" class="encuesta-text-area_otro" id="OTRO-TEXTO-${respuesta_copia[j].id_respuesta}" rows="5" cols="50" style="display:none"></textarea></div>
                        `;
                                    }

                                    div_respuestas1.setAttribute('class', 'form-check');
                                    div_respuestas.appendChild(div_respuestas1);
                                    k++;
                                });
                                multiple = false;
                            }
                            //TIPO DE RESPUESTA 7: CELULAR
                            else if (respuesta_copia[j].id_tipo_respuesta == '7') {
                                div_respuestas.innerHTML = `
                    <input type="text" class="encuesta-input-celular" id="${respuesta_copia[j].id_respuesta}" name="${respuesta_copia[j].id_pregunta}" placeholder="Ingrese su número de celular (10 dígitos)" maxlength="10"/>
                    <div class="encuesta-error-mensaje" id="error-${respuesta_copia[j].id_respuesta}" style="display: none; color: red; font-size: 12px; margin-top: 5px;">
                        El número de celular debe contener únicamente números y tener 10 dígitos.
                    </div>
                `;
                            }
                            //TIPO DE RESPUESTA 8: CORREO ELECTRONICO
                            else if (respuesta_copia[j].id_tipo_respuesta == '8') {
                                div_respuestas.innerHTML = `
                    <input type="email" class="encuesta-input-correo" id="${respuesta_copia[j].id_respuesta}" name="${respuesta_copia[j].id_pregunta}" placeholder="Ingrese su correo electrónico" maxlength="100"/>
                    <div class="encuesta-error-mensaje" id="error-${respuesta_copia[j].id_respuesta}" style="display: none; color: red; font-size: 12px; margin-top: 5px;">
                        Ingrese un correo electrónico válido (ejemplo: usuario@dominio.com).
                    </div>
                `;
                            }

                            const hr = document.createElement('hr');

                            pagina.appendChild(div_preguntas);
                            pagina.appendChild(div_respuestas);
                            pagina.appendChild(hr);
                        }

                        if (Array.from(respuesta.d[4]).slice(paginador + 10, paginador2 + 10).length > 0) {
                            const encuesta_boton_contenedor = document.createElement('div');
                            encuesta_boton_contenedor.innerHTML = `<button class="encuesta-boton_enviar encuesta-boton_continuar" id="ir_a${contador + 1}">CONTINUAR</button>`;
                            pagina.appendChild(encuesta_boton_contenedor);
                            encuesta_boton_contenedor.setAttribute('class', 'encuesta-boton_contenedor');
                        } else {
                            const encuesta_boton_contenedor = document.createElement('div');
                            encuesta_boton_contenedor.innerHTML = '<button class="encuesta-boton_enviar" id="enviar-encuesta">ENVIAR</button>';
                            pagina.appendChild(encuesta_boton_contenedor);
                            encuesta_boton_contenedor.setAttribute('class', 'encuesta-boton_contenedor');
                        }

                        encuestas.appendChild(pagina);

                        if (contador == 1) {
                            pagina.setAttribute('class', 'modal-i-gl-show');
                        } else {
                            pagina.setAttribute('class', 'modal-i-gl-hide');
                        }

                        paginador = paginador2;
                        paginador2 += 10;
                        respuesta_copia = Array.from(respuesta.d[4]).slice(paginador, paginador2);
                        contador++;

                    } while (respuesta_copia.length > 0)

                    if (!multiple) {
                        const quitar_guia = document.getElementsByClassName('guia-respuestas');
                        for (var i = 0; i < quitar_guia.length; i++) {
                            quitar_guia[i].setAttribute('style', 'display: none');
                        }
                    }
                }
                else {
                    //SE AÑADE EL CUADRO CON LA GUIA DE RESPUESTAS
                    const guia_respuestas = document.createElement('div');
                    guia_respuestas.setAttribute('class', 'guia-respuestas');
                    encuestas.appendChild(guia_respuestas);
                    let multiple = false;

                    for (let j = 0; j < respuesta.d[4].length; j++) {
                        validar_competencia(respuesta.d[4][j].competencia, encuestas);
                        const div_preguntas = document.createElement('div');
                        div_preguntas.innerHTML = `
                    <span class="encuesta-pregunta" >${j + 1}. ${respuesta.d[4][j].pregunta}</span>
                `;
                        div_preguntas.setAttribute('class', 'encuesta-pregunta');
                        const div_respuestas = document.createElement('div');

                        //TIPO DE RESPUESTA 1: RADIO BUTTON
                        if (respuesta.d[4][j].id_tipo_respuesta == '1') {
                            multiple = true;
                            var criterio_respuesta = Array.from(respuesta.d[4][j].criterios_respuesta.split(","));
                            let k = 0;
                            criterio_respuesta.forEach(() => {
                                const div_respuestas1 = document.createElement('div');
                                //SI EL CRITERIO DE RESPUESTA ES 'OTRO(A)S' PRESENTO UN TEXT AREA PARA QUE EL USUARIO OBLIGATORIAMENTE LO DIGITE, DE LO CONTRARIO NO
                                if (criterio_respuesta[k] != "Otro(a)s") {
                                    div_respuestas1.innerHTML = `
                    <input type="radio" class="form-check-input encuesta-radiobutton" id="${criterio_respuesta[k]}---${respuesta.d[4][j].id_respuesta}" name="${respuesta.d[4][j].id_respuesta}" value="${criterio_respuesta[k]}"><label class="encuesta-etiqueta" for="${criterio_respuesta[k]}---${respuesta.d[4][j].id_respuesta}"> ${criterio_respuesta[k]}</label>
                `;
                                } else {
                                    div_respuestas1.innerHTML = `
                   <input type="radio" class="form-check-input encuesta-radiobutton" id="${criterio_respuesta[k]}---${respuesta.d[4][j].id_respuesta}" name="${respuesta.d[4][j].id_respuesta}" value="${criterio_respuesta[k]}"><label class="encuesta-etiqueta" for="${criterio_respuesta[k]}---${respuesta.d[4][j].id_respuesta}"> ${criterio_respuesta[k]}</label>
                    <div><textarea placeholder="${criterio_respuesta[k]}" maxlength="250" class="encuesta-text-area_otro" id="OTRO-TEXTO-${respuesta.d[4][j].id_respuesta}" rows="5" cols="50" style="display:none"></textarea></div>
                `;
                                }
                                div_respuestas1.setAttribute('class', 'form-check');
                                div_respuestas.appendChild(div_respuestas1);
                                k++;
                            });
                            multiple = false;
                            //TIPO DE RESPUESTA 2: BOTONES SÍ/NO
                        } else if (respuesta.d[4][j].id_tipo_respuesta == '2') {
                            const encuesta_boton_contenedor = document.createElement('div');
                            var criterio_respuesta = Array.from(respuesta.d[4][j].criterios_respuesta.split(","));
                            let k = 0;
                            criterio_respuesta.forEach(() => {
                                const div_respuestas1 = document.createElement('div');
                                div_respuestas1.innerHTML = `
                    <button class="${criterio_respuesta[k]} encuesta-boton-binario" type="button" id="${criterio_respuesta[k]}${respuesta.d[4][j].id_respuesta}" name="${respuesta.d[4][j].id_respuesta}">${criterio_respuesta[k]}</button>
                `;
                                div_respuestas1.setAttribute("class", "encuesta-boton-contenedor");
                                encuesta_boton_contenedor.setAttribute("style", "text-align: center");
                                encuesta_boton_contenedor.appendChild(div_respuestas1);
                                k++;
                            });
                            div_respuestas.appendChild(encuesta_boton_contenedor);
                            //TIPO DE RESPUESTA 3: TEXT
                        } else if (respuesta.d[4][j].id_tipo_respuesta == '3') {
                            div_respuestas.innerHTML = `
                    <textarea placeholder="Máximo 250 caracteres." maxlength="250" class="encuesta-text-area" id="${respuesta.d[4][j].id_respuesta}" name="${respuesta_copia[j].id_pregunta}" rows="10" cols="50"></textarea>
                `;
                            //TIPO DE RESPUESTA 4: FECHA
                        } else if (respuesta.d[4][j].id_tipo_respuesta == '4') {
                            div_respuestas.innerHTML = `
                    <input type="date" value="2020-01-01" class="encuesta-input-date" id="${respuesta.d[4][j].id_respuesta}"/>
                `;
                        }
                        //TIPO DE RESPUESTA 5: NUMERO
                        else if (respuesta.d[4][j].id_tipo_respuesta == '5') {
                            div_respuestas.innerHTML = `
                    <input type="number" class="encuesta-input-number max2000" min="0" max="2000" step="1" maxlength="7" id="${respuesta.d[4][j].id_respuesta}"/>
                `;
                        }
                        //TIPO DE RESPUESTA 6: CHECKBOX
                        else if (respuesta.d[4][j].id_tipo_respuesta == '6') {
                            multiple = true;
                            var criterio_respuesta = Array.from(respuesta.d[4][j].criterios_respuesta.split(","));
                            let k = 0;
                            criterio_respuesta.forEach(() => {
                                const div_respuestas1 = document.createElement('div');
                                //SI EL CRITERIO DE RESPUESTA ES 'OTRO(A)S' PRESENTO UN TEXT AREA PARA QUE EL USUARIO OBLIGATORIAMENTE LO DIGITE, DE LO CONTRARIO NO
                                if (criterio_respuesta[k] != "Otro(a)s") {
                                    div_respuestas1.innerHTML = `
                        <input type="checkbox" class="form-check-input encuesta-checkbox" id="${criterio_respuesta[k]}___${respuesta.d[4][j].id_respuesta}" name="${respuesta.d[4][j].id_respuesta}" value="${criterio_respuesta[k]}"><label class="encuesta-etiqueta" for="${criterio_respuesta[k]}___${respuesta.d[4][j].id_respuesta}"> ${criterio_respuesta[k]}</label>
                    `;
                                } else {
                                    div_respuestas1.innerHTML = `
                        <input type="checkbox" class="form-check-input encuesta-checkbox encuesta-checkbox_otro" id="${criterio_respuesta[k]}___${respuesta.d[4][j].id_respuesta}" name="${respuesta.d[4][j].id_respuesta}" value="${criterio_respuesta[k]}"><label class="encuesta-etiqueta" for="${criterio_respuesta[k]}___${respuesta.d[4][j].id_respuesta}"> ${criterio_respuesta[k]}</label>
                        <div><textarea placeholder="${criterio_respuesta[k]}" maxlength="250" class="encuesta-text-area_otro" id="OTRO-TEXTO-${respuesta.d[4][j].id_respuesta}" rows="5" cols="50" style="display:none"></textarea></div>
                    `;
                                }

                                div_respuestas1.setAttribute('class', 'form-check');
                                div_respuestas.appendChild(div_respuestas1);
                                k++;
                            });
                            multiple = false;
                        }
                        //TIPO DE RESPUESTA 7: CELULAR
                        else if (respuesta.d[4][j].id_tipo_respuesta == '7') {
                            div_respuestas.innerHTML = `
                    <input type="text" class="encuesta-input-celular" id="${respuesta.d[4][j].id_respuesta}" name="${respuesta_copia[j].id_pregunta}" placeholder="Ingrese su número de celular (10 dígitos)" maxlength="10"/>
                    <div class="encuesta-error-mensaje" id="error-${respuesta.d[4][j].id_respuesta}" style="display: none; color: red; font-size: 12px; margin-top: 5px;">
                        El número de celular debe contener únicamente números y tener 10 dígitos.
                    </div>
                `;
                        }
                        //TIPO DE RESPUESTA 8: CORREO ELECTRONICO
                        else if (respuesta.d[4][j].id_tipo_respuesta == '8') {
                            div_respuestas.innerHTML = `
                    <input type="email" class="encuesta-input-correo" id="${respuesta.d[4][j].id_respuesta}" name="${respuesta_copia[j].id_pregunta}" placeholder="Ingrese su correo electrónico" maxlength="100"/>
                    <div class="encuesta-error-mensaje" id="error-${respuesta.d[4][j].id_respuesta}" style="display: none; color: red; font-size: 12px; margin-top: 5px;">
                        Ingrese un correo electrónico válido (ejemplo: usuario@dominio.com).
                    </div>
                `;
                        }

                        encuestas.appendChild(div_preguntas);
                        encuestas.appendChild(div_respuestas);
                    }

                    if (respuesta.d[4].length > 1 || (respuesta.d[4].length == 1 && respuesta.d[4][0].id_tipo_respuesta == '3') || multiple) {
                        const encuesta_boton_contenedor = document.createElement('div');
                        encuesta_boton_contenedor.innerHTML = '<button class="encuesta-boton_enviar" id="enviar-encuesta">ENVIAR</button>';
                        encuestas.appendChild(encuesta_boton_contenedor);
                        encuesta_boton_contenedor.setAttribute('class', 'encuesta-boton_contenedor');
                    }

                    if (!multiple) {
                        const quitar_guia = document.getElementsByClassName('guia-respuestas');
                        for (var i = 0; i < quitar_guia.length; i++) {
                            quitar_guia[i].setAttribute('style', 'display: none');
                        }
                    }
                }
                eventos_encuesta_binaria();
                eventos_encuesta_multiple();
                eventos_boton_continuar();
                cantidadNumero();
                inputOpcionOtra();
                limitarMaxCheckbox(3);

                // Agregar las nuevas validaciones
                validarCamposCelular();
                validarCamposEmail();
            }
        }

        //CONSUME WEB SERVICE PARA RESPONDER LAS ENCUESTAS DE TIPO BINARIO (SI/NO)
        async function responderEncuestaBinaria(e) {
            const ingreso_encuesta = document.querySelector('#ingreso_encuesta').textContent;
            const sender = e.target.id;
            const respuesta = sender.substring(0, 2);
            const id_respuesta = sender.substring(2);

            const rest = await fetch(`WebService_Default.asmx/responder_encuesta_binaria`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    ingreso_encuesta,
                    respuesta,
                    id_respuesta
                })
            })
            const respuesta_json = await rest.json();
            if (respuesta_json.d == 1) {
                $("#modal_encuestas").removeClass("modal-i-gl-show").addClass("modal-i-gl-hide");
            }
        }

        //AÑADE LA ESCUCHA DE EVENTOS A LOS BOTONES SI/NO
        function eventos_encuesta_binaria() {
            var a = document.getElementsByClassName('encuesta-boton-binario');
            for (var i = 0; i < a.length; i++) {
                a[i].addEventListener("click", responderEncuestaBinaria)
            }
        }

        //CONSUME WEB SERVICE PARA RESPONDER LAS ENCUESTAS DE TIPO MULTIPLE (A/B/C/D/TEXTO)
        async function responderEncuestaMultiple(e) {
            e.preventDefault();
            const ingreso_encuesta = document.querySelector('#ingreso_encuesta').textContent;
            const encuesta = document.querySelector('#encuesta').textContent;
            let radios = document.querySelectorAll('input[type="radio"]:checked');
            let text_area = document.getElementsByClassName('encuesta-text-area');
            let input_date = document.getElementsByClassName('encuesta-input-date');
            let input_number = document.getElementsByClassName('encuesta-input-number');
            let input_correo = document.getElementsByClassName('encuesta-input-correo');
            let input_celular = document.getElementsByClassName('encuesta-input-celular');
            let check = document.querySelectorAll('input[type="checkbox"]:checked');
            let contador_check = 0;
            let valor_actual_check = 0;
            let array_check = [];
            let cantidad_checked = [];
            let id_preguntas = [];

            //RECORRO TODOS LOS RADIO BUTTON PARA VALIDAR QUE LOS QUE HAYAN SIDO SELECCIONADOS CON EL CRITERIO DE RESPUESTA 'OTRO(A)S' TENGAN DILIGENCIADO SU RESPECTIVO CAMPO TEXT AREA
            for (let i = 0; i < radios.length; i++) {
                if (radios[i].id.includes('Otro(a)s')) {
                    if (document.getElementById('OTRO-TEXTO-' + radios[i].name).value.trim() == "") {
                        alert('¡Si en alguna de las preguntas seleccionó Otro(a)s, debe digitar cuál(es)!');
                        return;
                    } else {
                        cantidad_checked.push('Otro(a)s: ' + document.getElementById('OTRO-TEXTO-' + radios[i].name).value.trim() + '---' + radios[i].name)
                    }

                } else {
                    cantidad_checked.push(radios[i].id)
                }
            }

            for (let i = 0; i < text_area.length; i++) {
                cantidad_checked.push(text_area[i].id + 'ENCUESTA-TEXTO' + text_area[i].value.trim());
                id_preguntas.push(text_area[i].name + '-' + text_area[i].id);
            }

            for (let i = 0; i < input_date.length; i++) {
                cantidad_checked.push(input_date[i].id + 'ENCUESTA-TEXTO' + input_date[i].value.trim())
            }

            for (let i = 0; i < input_number.length; i++) {
                cantidad_checked.push(input_number[i].id + 'ENCUESTA-TEXTO' + input_number[i].value.trim())
            }

            for (let i = 0; i < input_correo.length; i++) {
                cantidad_checked.push(input_correo[i].id + 'ENCUESTA-TEXTO' + input_correo[i].value.trim());
                id_preguntas.push(input_correo[i].name + '-' + input_correo[i].id);
            }

            for (let i = 0; i < input_celular.length; i++) {
                cantidad_checked.push(input_celular[i].id + 'ENCUESTA-TEXTO' + input_celular[i].value.trim());
                id_preguntas.push(input_celular[i].name + '-' + input_celular[i].id);
            }

            for (let i = 0; i < check.length; i++) {
                let name = check[i].name;
                if (name != valor_actual_check) {
                    contador_check++;
                    valor_actual_check = name;
                    //RECORRO TODOS LOS CHECKBOX PARA VALIDAR QUE LOS QUE HAYAN SIDO SELECCIONADOS CON EL CRITERIO DE RESPUESTA 'OTRO(A)S' TENGAN DILIGENCIADO SU RESPECTIVO CAMPO TEXT AREA
                    if (check[i].id.includes('Otro(a)s')) {
                        if (document.getElementById('OTRO-TEXTO-' + check[i].name).value.trim() == "") {
                            alert('¡Si en alguna de las preguntas seleccionó Otro(a)s, debe digitar cuál(es)!');
                            return;
                        } else {
                            array_check.push('Otro(a)s: ' + document.getElementById('OTRO-TEXTO-' + check[i].name).value.trim() + '___' + check[i].name + '\\');
                        }
                    } else {
                        array_check.push(check[i].id + '\\')
                    }
                } else {
                    //RECORRO TODOS LOS CHECKBOX PARA VALIDAR QUE LOS QUE HAYAN SIDO SELECCIONADOS CON EL CRITERIO DE RESPUESTA 'OTRO(A)S' TENGAN DILIGENCIADO SU RESPECTIVO CAMPO TEXT AREA
                    if (check[i].id.includes('Otro(a)s')) {
                        if (document.getElementById('OTRO-TEXTO-' + check[i].name).value.trim() == "") {
                            alert('¡Si en alguna de las preguntas seleccionó Otro(a)s, debe digitar cuál(es)!');
                            return;
                        } else {
                            array_check.push('Otro(a)s: ' + document.getElementById('OTRO-TEXTO-' + check[i].name).value.trim() + '___' + check[i].name + '\\');
                        }
                    } else {
                        array_check.push(check[i].id + '\\')
                    }
                }
            }

            if (array_check.length != 0) {
                cantidad_checked.push(array_check.toString());
            }

            const rest = await fetch(`WebService_Default.asmx/responder_encuesta_multiple`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({
                    ingreso_encuesta,
                    encuesta,
                    cantidad_checked,
                    id_preguntas
                })
            })
            const respuesta_json = await rest.json();
            //SI EL WEB SERVICE RETORNA 1 SIGNIFICA QUE SE RESPONDIERON TODAS LAS PREGUNTAS Y LA ENCUESTA ESTÁ FINALIZADA SINO ES PORQUE HAY PREGUNTAS SIN RESPONDER
            if (respuesta_json.d == 1) {
                $("#modal_encuestas").removeClass("modal-i-gl-show").addClass("modal-i-gl-hide");
            } else {
                alert('¡Tiene preguntas sin responder!');
            }
        }

        //AÑADE LA ESCUCHA DE EVENTOS AL BOTÓN 'ENVIAR' CUANDO LAS ENCUESTAS SON DE TIPO MULTIPLE O ABIERTAS
        function eventos_encuesta_multiple() {
            var a = document.getElementById('enviar-encuesta');
            a.addEventListener("click", responderEncuestaMultiple)
        }

        //OCULTA LA 'PÁGINA DE 10 PREGUNTAS' ACTUAL Y MUESTRA EL SIGUIENTE GRUPO 
        function boton_continuar(e) {
            e.preventDefault();
            const mostrar_siguiente = e.target.id.replace('ir_a', '');
            let id_pagina_siguiente = document.getElementById('pagina' + mostrar_siguiente);
            let id_pagina_actual = document.getElementById('pagina' + (mostrar_siguiente - 1));
            let radios = document.querySelectorAll('input[type="radio"]:checked').length;
            let radiobuttons = document.querySelectorAll('input[type="radio"]:checked');
            let texts = document.getElementsByClassName('encuesta-text-area');;
            let input_date = document.getElementsByClassName('encuesta-input-date');
            let input_number = document.getElementsByClassName('encuesta-input-number');
            let input_correo = document.getElementsByClassName('encuesta-input-correo');
            let input_celular = document.getElementsByClassName('encuesta-input-celular');
            let check = document.querySelectorAll('input[type="checkbox"]:checked');
            let contador_texts = 0;
            let contador_input_date = 0;
            let contador_input_number = 0;
            let contador_input_correo = 0;
            let contador_input_celular = 0;
            let contador_check = 0;
            let valor_actual_check = 0;

            for (let i = 0; i < texts.length; i++) {

                if (texts[i].value.trim() != "") {
                    contador_texts++;
                }
            }

            for (let i = 0; i < input_date.length; i++) {

                if (input_date[i].value.trim() != "") {
                    contador_input_date++;
                }
            }

            for (let i = 0; i < input_number.length; i++) {

                if (input_number[i].value.trim() != "") {
                    contador_input_number++;
                }
            }

            for (let i = 0; i < input_correo.length; i++) {

                if (input_correo[i].value.trim() != "") {
                    contador_input_correo++;
                }
            }

            for (let i = 0; i < input_celular.length; i++) {

                if (input_celular[i].value.trim() != "") {
                    contador_input_celular++;
                }
            }

            for (let i = 0; i < check.length; i++) {
                let name = check[i].name;
                if (name != valor_actual_check) {
                    contador_check++;
                    valor_actual_check = name;
                    //RECORRO TODOS LOS CHECKBOX PARA VALIDAR QUE LOS QUE HAYAN SIDO SELECCIONADOS CON EL CRITERIO DE RESPUESTA 'OTRO(A)S' TENGAN DILIGENCIADO SU RESPECTIVO CAMPO TEXT AREA
                    if (check[i].id.includes('Otro(a)s')) {
                        if (document.getElementById('OTRO-TEXTO-' + check[i].name).value.trim() == "") {
                            alert('¡Si en alguna de las preguntas seleccionó Otro(a)s, debe digitar cuál(es)!');
                            return;
                        }
                    }
                } else {
                    //RECORRO TODOS LOS CHECKBOX PARA VALIDAR QUE LOS QUE HAYAN SIDO SELECCIONADOS CON EL CRITERIO DE RESPUESTA 'OTRO(A)S' TENGAN DILIGENCIADO SU RESPECTIVO CAMPO TEXT AREA
                    if (check[i].id.includes('Otro(a)s')) {
                        if (document.getElementById('OTRO-TEXTO-' + check[i].name).value.trim() == "") {
                            alert('¡Si en alguna de las preguntas seleccionó Otro(a)s, debe digitar cuál(es)!');
                            return;
                        }
                    }
                }
            }

            let paginador = (mostrar_siguiente - 1) * 10;

            if ((radios + contador_texts + contador_input_date + contador_input_number + contador_input_correo + contador_input_celular + contador_check) < paginador) {
                alert('¡Tiene preguntas sin responder!');
            } else {

                for (let i = 0; i < radios; i++) {
                    //RECORRO TODOS LOS RADIO BUTTON PARA VALIDAR QUE LOS QUE HAYAN SIDO SELECCIONADOS CON EL CRITERIO DE RESPUESTA 'OTRO(A)S' TENGAN DILIGENCIADO SU RESPECTIVO CAMPO TEXT AREA
                    if (radiobuttons[i].id.includes('Otro(a)s')) {
                        if (document.getElementById('OTRO-TEXTO-' + radiobuttons[i].name).value.trim() == "") {
                            alert('¡Si en alguna de las preguntas seleccionó Otro(a)s, debe digitar cuál(es)!');
                            return;
                        }
                    }
                }



                id_pagina_actual.removeAttribute('class');
                id_pagina_actual.setAttribute('class', "modal-i-gl-hide")
                id_pagina_siguiente.removeAttribute('class');
                id_pagina_siguiente.setAttribute('class', "modal-i-gl-show animated fadeInRight");
                const scroll = document.getElementById('scroll');
                scroll.scrollTo({ top: 0, behavior: 'smooth' });

                //OCULTO EL DIV QUE CONTIENE LA DESCRIPCIÓN DE LA ENCUESTA PARA QUE NO SE MUESTRE EN LAS OTRAS PÁGINAS
                let descripcion = document.getElementById('encuesta-descripcion');
                descripcion.setAttribute('style', 'display:none');
            }
        }

        //AÑADE LA ESCUCHA DE EVENTOS AL BOTÓN 'CONTINUAR' CUANDO LAS ENCUESTAS TIENEN MÁS DE 10 PREGUNTAS
        function eventos_boton_continuar() {
            var a = document.getElementsByClassName('encuesta-boton_continuar');
            for (var i = 0; i < a.length; i++) {
                a[i].addEventListener("click", boton_continuar)
            }
        }

        //OCULTA LA 'PÁGINA DE 10 PREGUNTAS' ACTUAL Y MUESTRA EL GRUPO ANTERIOR 
        function boton_atras(e) {
            e.preventDefault();
            const mostrar_anterior = parseInt(e.target.id.replace('ir_a', ''));
            let id_pagina_anterior = document.getElementById('pagina' + mostrar_anterior);
            let id_pagina_actual = document.getElementById('pagina' + (mostrar_anterior + 1));
            let radios = document.querySelectorAll('input[type="radio"]:checked').length;
            let radiobuttons = document.querySelectorAll('input[type="radio"]:checked');
            let texts = document.getElementsByClassName('encuesta-text-area');
            let input_date = document.getElementsByClassName('encuesta-input-date');
            let input_number = document.getElementsByClassName('encuesta-input-number');
            let check = document.querySelectorAll('input[type="checkbox"]:checked');
            let contador_texts = 0;
            let contador_input_date = 0;
            let contador_input_number = 0;
            let contador_check = 0;
            let valor_actual_check = 0;

            for (let i = 0; i < texts.length; i++) {
                if (texts[i].value.trim() != "") {
                    contador_texts++;
                }
            }

            for (let i = 0; i < input_date.length; i++) {
                if (input_date[i].value.trim() != "") {
                    contador_input_date++;
                }
            }

            for (let i = 0; i < input_number.length; i++) {
                if (input_number[i].value.trim() != "") {
                    contador_input_number++;
                }
            }

            for (let i = 0; i < check.length; i++) {
                let name = check[i].name;
                if (name != valor_actual_check) {
                    contador_check++;
                    valor_actual_check = name;
                    //RECORRO TODOS LOS CHECKBOX PARA VALIDAR QUE LOS QUE HAYAN SIDO SELECCIONADOS CON EL CRITERIO DE RESPUESTA 'OTRO(A)S' TENGAN DILIGENCIADO SU RESPECTIVO CAMPO TEXT AREA
                    if (check[i].id.includes('Otro(a)s')) {
                        if (document.getElementById('OTRO-TEXTO-' + check[i].name).value.trim() == "") {
                            alert('¡Si en alguna de las preguntas seleccionó Otro(a)s, debe digitar cuál(es)!');
                            return;
                        }
                    }
                } else {
                    //RECORRO TODOS LOS CHECKBOX PARA VALIDAR QUE LOS QUE HAYAN SIDO SELECCIONADOS CON EL CRITERIO DE RESPUESTA 'OTRO(A)S' TENGAN DILIGENCIADO SU RESPECTIVO CAMPO TEXT AREA
                    if (check[i].id.includes('Otro(a)s')) {
                        if (document.getElementById('OTRO-TEXTO-' + check[i].name).value.trim() == "") {
                            alert('¡Si en alguna de las preguntas seleccionó Otro(a)s, debe digitar cuál(es)!');
                            return;
                        }
                    }
                }
            }

            let paginador = mostrar_anterior * 10;

            if ((radios + contador_texts + contador_input_date + contador_input_number + contador_check) < paginador) {
                alert('¡Tiene preguntas sin responder!');
            } else {
                for (let i = 0; i < radios; i++) {
                    //RECORRO TODOS LOS RADIO BUTTON PARA VALIDAR QUE LOS QUE HAYAN SIDO SELECCIONADOS CON EL CRITERIO DE RESPUESTA 'OTRO(A)S' TENGAN DILIGENCIADO SU RESPECTIVO CAMPO TEXT AREA
                    if (radiobuttons[i].id.includes('Otro(a)s')) {
                        if (document.getElementById('OTRO-TEXTO-' + radiobuttons[i].name).value.trim() == "") {
                            alert('¡Si en alguna de las preguntas seleccionó Otro(a)s, debe digitar cuál(es)!');
                            return;
                        }
                    }
                }

                id_pagina_actual.removeAttribute('class');
                id_pagina_actual.setAttribute('class', "modal-i-gl-hide");
                id_pagina_anterior.removeAttribute('class');
                id_pagina_anterior.setAttribute('class', "modal-i-gl-show animated fadeInLeft");
                const scroll = document.getElementById('scroll');
                scroll.scrollTo({ top: 0, behavior: 'smooth' });

                //OCULTO EL DIV QUE CONTIENE LA DESCRIPCIÓN DE LA ENCUESTA PARA QUE NO SE MUESTRE EN LAS OTRAS PÁGINAS
                let descripcion = document.getElementById('encuesta-descripcion');
                descripcion.setAttribute('style', 'display:none');
            }
        }

        // AÑADE LA ESCUCHA DE EVENTOS AL BOTÓN 'ATRÁS' CUANDO LAS ENCUESTAS TIENEN MÁS DE 10 PREGUNTAS
        function eventos_boton_atras() {
            var a = document.getElementsByClassName('encuesta-boton_atras');
            for (var i = 0; i < a.length; i++) {
                a[i].addEventListener("click", boton_atras);
            }
        }

        function validar_competencia(competencia_actual, pagina) {
            let competencia = document.getElementById('competencia');
            if (competencia.textContent != competencia_actual) {
                competencia.textContent = competencia_actual;

                if (competencia_actual != "Ninguno") {
                    const titulo_competencia = document.createElement('h2');
                    titulo_competencia.textContent = competencia_actual;
                    titulo_competencia.setAttribute('style', 'color: #6e8297; font-size: 2em; text-align: center; margin-top: 10px; margin-bottom: 20px;');
                    pagina.appendChild(titulo_competencia);
                }
            }
        }

        modalNoticiaContent.addEventListener('click', e => {
            if (e.target.className === 'img') {
                let nuevaImagen = e.target.style.backgroundImage.replace(/(url\(|\)|")/g, '');
                window.open(nuevaImagen);
            }
        })
        //cargar_noticias_slideshow();
        cargar_noticias();
        cargar_noticias_recientes();
        validarPoliticas();
        validarEvaluaciones();
        validarEncuestas();

        function cantidadNumero() {
            let input_number = document.getElementsByClassName('max2000');

            for (var i = 0; i < input_number.length; i++) {
                input_number[i].addEventListener('input', (e) => {
                    let valor = e.target.value;

                    // Validar longitud total máxima de 7 caracteres
                    if (valor.length > 7) {
                        e.target.value = valor.slice(0, 7);
                        return;
                    }

                    // Separar parte entera y decimal
                    let partes = valor.split('.');
                    let parteEntera = partes[0];
                    let parteDecimal = partes[1];

                    // Validar que la parte entera no tenga más de 4 dígitos
                    if (parteEntera && parteEntera.length > 4) {
                        alert("No se permiten más de 4 dígitos enteros");
                        e.target.value = parteEntera.slice(0, 4) + (parteDecimal ? '.' + parteDecimal : '');
                        return;
                    }

                    // Validar que la parte decimal no tenga más de 2 dígitos
                    if (parteDecimal && parteDecimal.length > 2) {
                        e.target.value = parteEntera + '.' + parteDecimal.slice(0, 2);
                        return;
                    }
                });

                // Validar el rango solo cuando el usuario termine de escribir (blur)
                input_number[i].addEventListener('blur', (e) => {
                    let numero = parseFloat(e.target.value);

                    if (!isNaN(numero) && (numero > 2000 || numero < 0)) {
                        e.target.value = "";
                        alert("El número no puede ser inferior a 0 ni mayor a 2000");
                    }
                });
            }
        }

        //OCULTA Y/O MUESTRA EL TEXT AREA CORRESPONDIENTE A CADA CHECKBOX Y RADIO BUTTON CON CRITERIO DE RESPUESTA 'OTRO(A)S'
        function inputOpcionOtra() {
            let todosRadios = document.getElementsByClassName('encuesta-radiobutton');
            let todosCheckOtro = document.getElementsByClassName('encuesta-checkbox_otro');

            for (let i = 0; i < todosRadios.length; i++) {
                todosRadios[i].addEventListener('change',
                    function (e) {
                        let myname = this.getAttribute('name');
                        let mytextarea = document.getElementById('OTRO-TEXTO-' + myname);
                        if (e.target.value == 'Otro(a)s') {
                            mytextarea.setAttribute('style', 'display:initial')
                        } else {
                            mytextarea.setAttribute('style', 'display:none')
                        }
                    }
                );
            }

            for (let i = 0; i < todosCheckOtro.length; i++) {
                todosCheckOtro[i].addEventListener('change',
                    function (e) {
                        let myname = this.getAttribute('name');
                        let mytextarea = document.getElementById('OTRO-TEXTO-' + myname);
                        if (this.checked) {
                            mytextarea.setAttribute('style', 'display:initial')
                        } else {
                            mytextarea.setAttribute('style', 'display:none')
                        }
                    }
                );
            }
        }

        //CUANDO SE SOLICITE QUE LOS CHECKBOX (EN TODAS LAS PREGUNTAS, NO POR INDIVIVUAL) TENGAN UN LIMITE DE SELECCIONADOS EJ, 'MÁXIMO ELIJA 3 OPCIONES' ESTA FUNCION LOS LIMITA
        function limitarMaxCheckbox(cantidad) {
            if (cantidad > 0) {
                let todosCheck = document.getElementsByClassName('encuesta-checkbox');
                for (let i = 0; i < todosCheck.length; i++) {
                    todosCheck[i].addEventListener('change',
                        function (e) {
                            let myname = this.getAttribute('name');
                            let grupoCheck = document.getElementsByName(myname);
                            let contadorCheck = 0;
                            for (let j = 0; j < grupoCheck.length; j++) {
                                if (grupoCheck[j].type == 'checkbox' && grupoCheck[j].checked) {
                                    contadorCheck++;
                                }
                                if (contadorCheck > cantidad) {
                                    alert('Máximo puede seleccionar ' + cantidad + ' opciones.')
                                    this.checked = false;

                                    if (this.id.includes('Otro(a)s')) {
                                        let mytextarea = document.getElementById('OTRO-TEXTO-' + myname);
                                        mytextarea.setAttribute('style', 'display:none');
                                    }
                                    return;
                                }
                            }
                        }
                    );
                }
            }
        }


        function quitarPadding() {
            let doc = document.getElementById('container');
            doc.removeAttribute('style');
        }
        document.addEventListener('load', quitarPadding);
        window.addEventListener('load', quitarPadding);
    </script>
</asp:Content>
