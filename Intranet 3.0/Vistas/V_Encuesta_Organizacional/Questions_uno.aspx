<%@ Page Title="Encuesta clima organizacional" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Questions_uno.aspx.cs" Inherits="OfertaEmpleo.Questions_uno" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <script type="text/javascript">
        if (window.history.forward(1) != null)
            window.history.forward(1);
    </script> 
    <script>

        function deshabilitaRetroceso(){
            window.location.hash="no-back-button";
            window.location.hash="Again-No-back-button" //chrome
            window.onhashchange=function(){window.location.hash="no-back-button";}
        }
        //function deshabilitaRetroceso2() {
        //    window.location.hash = "no-back-button";
        //    window.location.hash = "Again-No-back-button" //chrome
        //    window.onhashchange = function () { window.location.hash = "no-back-button"; }
        //}

        document.onload = deshabilitaRetroceso;
        window.onload = deshabilitaRetroceso;

        function validacion_msg() {
            $('#alert_danger').modal('show');
        }

        document.getelements

    </script>

    <link rel="Stylesheet" href="/Styles/css/encuesta_organizacional/encuesta_organizacional.css"/>
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
<div class ="encuesta-container" id="encuesta-container">
    <div class="sub-header">
        <div></div>
        <h5 class="encuesta-titulo">Realizando la encuesta de clima organizacional</h5>
    </div>
    <section>
        <div class="content-header">
            <p class="encuesta-info">Califique cada pregunta y solo debe diligenciar una opción de respuesta,  de acuerdo a los siguientes criterios que se relacionan a continuación.</p>
            <p class="encuesta-aviso"><small style="color: #ff0000;">¡Recuerde completar todos los campos, ya que todos son obligatorios!</small></p>
            <hr />
            <p class="encuesta-aviso"><small>Campos obligatorios <span style="color: #ff0000;">*</span></small></p>
        </div>
        <div>
            <ul class="encuesta-ayuda-contenedor">
                <li class="encuesta-aviso encuesta-ayuda"><span class="encuesta-opcion">A</span>    Totalmente Satisfecho / Totalmente de acuerdo</li>
                <li class="encuesta-aviso encuesta-ayuda"><span class="encuesta-opcion">B</span>    Satisfecho / De acuerdo</li>
                <li class="encuesta-aviso encuesta-ayuda"><span class="encuesta-opcion">C</span>    Insatisfecho / En desacuerdo</li>
                <li class="encuesta-aviso encuesta-ayuda"><span class="encuesta-opcion">D</span>    Totalmente Insatisfecho / Totalmente En desacuerdo</li>
            </ul>
            <br>
            <div style="align-items:center;">
                <h2 id="Titulo_Question" runat="server" style="color: #28a745; font-weight:bold; text-align:center; margin-bottom: 10px"></h2>
            </div>

            <div class="body">
                <asp:UpdatePanel runat="server">                    
                    <ContentTemplate>
                        <asp:Panel runat="server" ID="pnl_preguntas_container">
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>
        </div>
        <div class="encuesta-boton-contenedor">
            <asp:Button class="encuesta-boton_iniciar" runat="server" ID="lnk" OnClick="btn_siguientes_datos" Text="CONTINUAR">
            </asp:Button>
        </div>

    </section>

    <!--Modal alert error-->
    <div class="modal msg_error_login" id="alert_danger" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-dialog-centered animated bounceIn" role="document">
            <div class="modal-content">
                <div class="modal-body alert alert-warning" style="margin-bottom: 0px;">
                    <p style="margin:0px;text-align:center;font-size:15px;">¡Tiene preguntas sin responder, por favor valide para continuar!</p>
                </div>
            </div>
        </div>
    </div>
</div>
      <script>
          var boton = document.getElementById('MainContent_lnk');
          boton.addEventListener("click", () => {
              boton.style.display = 'none';
          });
      
          function prueba() {
              let doc = document.getElementById('container');
              doc.removeAttribute('style');
          }
          document.addEventListener('load', prueba);
          window.addEventListener('load', prueba);
      </script>
</asp:Content>
