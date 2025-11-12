<%@ Page Title="Encuesta clima organizacional" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Questions_Observacion.aspx.cs" Inherits="OfertaEmpleo.Questions_Observacion" %>

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
        function deshabilitaRetroceso2() {
            window.location.hash = "no-back-button";
            window.location.hash = "Again-No-back-button" //chrome
            window.onhashchange = function () { window.location.hash = "no-back-button"; }
        }

        document.onload = deshabilitaRetroceso;
        window.onload = deshabilitaRetroceso;

        function validacion_msg() {
            $('#alert_danger').modal('show');
        }
    </script>

    <style>

        .Textbox1 {
              width: 600px;
              height: 100px;
              align-items:center;
              align-content:center;
              padding: 12px 20px;
              padding-left:20px;
              margin: 8px 0;
              display: inline-block;
              overflow-y:auto;
              border: 1px solid #ccc;
             border-radius: 4px;
              box-sizing: border-box;
        }
            .Textbox2 {
              width: 100%;
              align-items:center;
              align-content:center;

             padding-top: 12px;
             padding-bottom: 12px;
             margin-top: 8px;
             margin-bottom:8px;
              display: inline-block;
        }


    </style>

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
<div class="encuesta-container" id="encuesta-container">
    <div class="sub-header">
        <div class="divisor-horizontal"></div>
        <h5 class="encuesta-titulo">Realizando la encuesta de clima organizacional</h5>
    </div>
    <section class="animated jackInTheBox">
        <div class="content-header">
            <p class="encuesta-aviso">A continuación puede (si lo desea) agregar un comentario sobre:</p>
   <%--         <p><small style="color: #ff0000;">¡Recuerda completar todos los campos, ya que todos son obligatorios!</small></p>--%>
            <p><small><span style="color: #ff0000;"></span></small></p>
            <hr />
            <p style="color:#28a745;" class="encuesta-pregunta">¿QUÉ LE GUSTARÍA QUE ETIB MEJORARA O IMPLEMENTARA EN SU GESTIÓN? </p>
            <textarea runat="server" ID="txtObservacion" placeholder="Observación" class="Textbox1" autocomplete="off" MaxLength="500"></textarea>
        </div>
<%--        <div class="content-body">
           
            <br>
            <div style="align-items:center;">
                <h2 id="Titulo_Question" runat="server" style="color: #28a745; font-weight:bold; text-align:center;"></h2>
            </div>

            <div class="body">
                <h4>QUE LE GUSTARÍA QUE ETIB MEJORARA O IMPLEMENTARA EN SU GESTIÓN. </h4>
                 <asp:Textbox runat="server" ID="txt_cc" placeholder="Observacion" autocomplete="off" MaxLength="500"></asp:Textbox>
            </div>
        </div>
        <div>
            <h4>GRACIAS POR SU PARTICIPACIÓN!!</h4>
        </div>--%>
        <div class="content-footer">
            <asp:LinkButton runat="server" ID="lnk" CssClass="btn-more" OnClick="btn_siguientes_datos">
                <span class="encuesta-boton_iniciar">FINALIZAR ENCUESTA</span>
            </asp:LinkButton>
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
