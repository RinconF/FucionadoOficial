<%@ Page Title="Encuesta clima organizacional" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="OfertaEmpleo._Default" %>

<asp:Content ID="Content" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        if (window.history.forward(1) != null)
            window.history.forward(1);


        function alerta() {

            $('#alert_danger').modal('show');
        }

        function deshabilitaRetroceso() {
            window.location.hash = "no-back-button";
            window.location.hash = "Again-No-back-button" //chrome
            window.onhashchange = function () { window.location.hash = "no-back-button"; }
        }
        //document.onload = deshabilitaRetroceso;
        //window.onload = deshabilitaRetroceso;
        function validacion_msg_defa() {
            $('#alert_msg').modal('show');
        }
        function validacion_msg_defa2() {
            $('#alert_msg_val').modal('show');
        }

        function validacion_msg_defa3() {
            $('#alert_msg_validacion').modal('show');
        }

        function deshabilitar_btn_inicio() {
            document.getElementById('#MainContent_btn_inicio_encuesta').style.display = "none";
        }
        function numbers_campos(e) {
            var key = window.Event ? e.which : e.keyCode
            return (key >= 48 && key <= 57)
        }
    </script>
    <link rel="Stylesheet" href="/Styles/css/encuesta_organizacional/encuesta_organizacional.css"/>
    

</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
<div class="container-not" id="container_not">
                <div class="not">
                    <div class="row">
                        <div class="col col1">
                            <h4>¡RECUERDE!</h4>
                            <h6>Responder sinceramente esta encuesta.</h6>
                        </div>
                    </div>
                </div>
                <div class="not">
                    <div class="row">
                        <div class="col col2">
                            <h4>¡SIN REGISTRO!</h4>
                            <h6>Esta encuesta la realiza sin la necesidad de algún registro.</h6>
                        </div>
                    </div>
                </div>
            </div>

        <div class="container-not-img" id="not_img">
                    <div class="not-img"> 
                        <span class="img-a animated fadeInRight">
                            <img src="../Img/imagenInicio.png" alt="Foto" style="width:270px;" />
                        </span>
                       <%-- <span class="img-b animated zoomIn">
                            <img src="../Img/dialogo.png" alt="Foto" width="200px" />
                        </span>--%>
                    </div>
                </div>
<div class="encuesta-container" id="encuesta-container">
    <div>
        <h5 class="encuesta-titulo">Bienvenido a la encuesta de clima organizacional</h5>
    </div>

    <section> <!--class="animated jackInTheBox"-->
        <div>
            <h5 class="encuesta-subtitulo">Diligencie los siguientes datos para iniciar la encuesta organizacional.</h5>
            <p class="encuesta-aviso"><small>¡Recuerde completar todos los campos, ya que todos son obligatorios para iniciar el proceso!</small></p>
            <hr />
            <p class="encuesta-aviso"><small>Campos obligatorios <span style="color:#ff0000;">*</span></small></p>
        </div>
        <div>
            <p class="encuesta-info"><small>Toda la información aquí consignada es de manejo confidencial, la cual es solo para fines estadísticos y de mejora para la toma de decisiones de la empresa. Sea muy sincero en sus respuestas, porque de ellas dependerán los planes de acción, mejoras y cambios a realizar.  por lo tanto no existen respuestas buenas o malas, sino sinceras e importantes para ETIB.</small></p>
            
               <p class="encuesta-info" style="color:#ff0000;"><small>Su número de documento será utilizado únicamente para control de participación, Los datos ingresados en el formulario serán totalmente anónimos. </small></p>
        </div>
        <div>
            <div>
                <div>
                       <p class="encuesta-pregunta encuesta-etiqueta-inicio">Numero de cédula: <span style="color:#ff0000;">*</span></p>
                    <%--<asp:Textbox class="encuesta-txt" Enabled="false" runat="server" ID="txt_cc" placeholder="Número de identificación" autocomplete="off" onkeypress="return numbers_campos(event)" MaxLength="13"></asp:Textbox>--%>
                    <asp:Label class="txt_cc" runat="server" ID="txt_cc"></asp:Label>
                </div>
                  <div>
                    <p class="encuesta-pregunta encuesta-etiqueta-inicio">Proceso al que pertenece: <span style="color:#ff0000;">*</span></p>
                    <asp:DropDownList class="encuesta-desplegable" runat="server" ID="drop_proceso">
                    </asp:DropDownList>
                </div>
            </div>
        </div>
                 
        <br />
        <div role="alert" id="msg_empty" style="display:none;width:100%;text-align:center;">
            <p style="margin:0px;"><small>¡Campos vacios!</small></p>
        </div>

        <div class="encuesta-boton-contenedor">
            <asp:Button id="btn_inicio_encuesta" class="encuesta-boton_iniciar" Text="INICIAR" runat="server" OnClick="btn_registro_encuenta_inicio">
            </asp:Button>
        </div>
        
    </section>

    <!--Modal alert error-->
    <div class="modal msg_error_login" id="alert_msg" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-body alert alert-warning" style="margin-bottom: 0px;">
                    <p style="margin:0px;text-align:center;font-size:15px;">¡Hay campos vacios, por favor termina de seleccionar la información!</p>
                </div>
            </div>
        </div>
    </div>

    <!--Modal alert error-->
    <div class="modal msg_error_login" id="alert_msg_val" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-body alert alert-warning" style="margin-bottom: 0px;">
                    <p style="margin:0px;text-align:center;font-size:15px;">¡El usuario no se encuentra en la base de datos!</p>
                </div>
            </div>
        </div>
    </div>

      <div class="modal msg_error_login" id="alert_msg_validacion" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-body alert alert-warning" style="margin-bottom: 0px;">
                    <p style="margin:0px;text-align:center;font-size:15px;">¡El usuario ingresado ya cuenta con una encuesta realizada!</p>
                </div>
            </div>
        </div>
    </div>
</div>    
    <script>
        var boton = document.getElementById('MainContent_btn_inicio_encuesta');
        boton.addEventListener("click", () => {
            boton.style.display = 'none';
        });

        function prueba() {
            let doc = document.getElementById('container');
            doc.removeAttribute('style');
        }
        document.addEventListener('load',prueba);
        window.addEventListener('load', prueba);
    </script>
</asp:Content>
