<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Question_Observaciones.aspx.cs" Inherits="OfertaEmpleo.Questions.Question_Observaciones" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <script>
        function disabled_img() {
            document.getElementById('not_img').style.display = "none";
            document.getElementById('container_not').style.display = "none";
        }
        function deshabilitaRetroceso() {
            window.location.hash = "no-back-button";
            window.location.hash = "Again-No-back-button" //chrome
            window.onhashchange = function () { window.location.hash = "no-back-button"; }
        }
        function open_msg() {
            document.getElementById("txt_title").innerHTML = "¡Etib escucha tus opiniones, ya que son valiosas para el bienestar de todos!";
            document.getElementById("txt_p_msg").innerHTML = "¡Se ha registrado correctamente las respuestas, gracias por participar en el proceso!";
            document.getElementById("txt_p_msg").style.color = "#28b463";
            document.getElementById("txt_input_ident").style.display = "none";
            document.getElementById("btn_input_ident").style.display = "none";
            document.getElementById("btn_input_nuevo").style.display = "inline-block";
        }
        function validacion_msg_register() {
            $('#alert_register').modal('show');
            document.getElementById('p_txt').innerHTML = "¡Este número de identificación ya ha realizado la encuesta!";
        }
        function validacion_msg_register_null() {
            $('#alert_register').modal('show');
            document.getElementById('p_txt').innerHTML = "¡No se ha escrito ningún número de identificación!";
        }
        function validacion_msg_register_incorrecto() {
            $('#alert_register').modal('show');
            document.getElementById('p_txt').innerHTML = "¡Número de identificación invalido!";
        }
        function validacion_msg_register_fecha_cc() {
            $('#alert_register').modal('show');
            document.getElementById('p_txt').innerHTML = "¡Fecha de expedición no coincide!";
        }

        function prueba() {
            let doc = document.getElementById('container');
            doc.removeAttribute('style');
        }
        document.addEventListener('load', prueba);
        window.addEventListener('load', prueba);

    </script>
    <link rel="Stylesheet" href="/Styles/css/encuesta_organizacional/encuesta_organizacional.css"/>
        
    <div class="sub-header">
        <div class="divisor-horizontal"></div>
    </div>
    <section class="animated fadeIn" style="padding-top:0px;">
        <div class="content-header" style="border:none;background:none;text-align:center;">
            <img src="../Img/Ilustracion_grupo.png" class="animated bounceIn encuesta-img"/>
            <h2 style="color:#ff4a22;" class="animated bounceIn" id="txt_title">¡Ha concluido la encuesta con éxito!</h2>
<%--            <h5 class="animated bounceIn" id="txt_p_msg"><small>Registra tu número de identificación para validar que eres colaborador de Etib.</small></h5>--%>
        </div>
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
               
                <div class="content-body animated bounceIn" style="text-align: center;" id="txt_input_ident">
<%--                    <asp:Textbox runat="server" ID="txt_cc" CssClass="txt-cc" placeholder="Número de identificación" autocomplete="off" onkeypress="return numbers_campos(event)"></asp:Textbox>
                    <asp:Textbox runat="server" ID="txt_date" CssClass="txt-cc" autocomplete="off" placeholder="Fecha expedición cédula"></asp:Textbox>--%>
                </div>
                <div class="content-footer animated bounceIn" style="text-align:center;" id="btn_input_ident">
                <%--    <h2 runat="server" style="color: #28a745; font-weight:bold; text-align:center;">¡Gracias!</h2>
                      <h4 runat="server" style="color: #28a745; font-weight:bold; text-align:center;">Tu encuesta se ha registrado correctamente</h4>--%>
<%--                    <asp:LinkButton runat="server" AutoPostBack="True" CssClass="btn-more" style="background:#1c3643;padding-left:30px;color:#fff;" OnClick="btn_insertar_cc">
                        <span class="txt-btn">FINALIZAR</span>
                    </asp:LinkButton>--%>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="content-header" style="border:none;background:none;text-align:center;">
            <h6 class="animated bounceIn" style="font-size: 20px; font-weight: bold; margin-bottom: 50px;">Nota: Las respuestas son totalmente anónimas, no seran asociadas a su identificación.</h6>
        </div>
        <div class="encuesta-boton-contenedor" style="" id="btn_input_nuevo">
                    <asp:Button  runat="server" text="TERMINAR" class="encuesta-boton_iniciar" onclick="btn_reload">
                    </asp:Button>
        </div>
    </section>


    <!--Modal alert error-->
    <div class="modal msg_error_login" id="alert_register" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-dialog-centered" role="document">
            <div class="modal-content">
                <div class="modal-body alert alert-warning" style="margin-bottom: 0px;">
                    <p style="margin:0px;text-align:center;font-size:15px;" id="p_txt">¡Tiene preguntas sin responder, por favor responda para continuar!</p>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
