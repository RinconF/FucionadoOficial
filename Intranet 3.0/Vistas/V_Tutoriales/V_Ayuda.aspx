<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="V_Ayuda.aspx.cs" Inherits="Intranet_3._0.Vistas.V_Tutoriales.V_Ayuda" %>
<asp:Content ID="Content1" ContentPlaceHolderID="scripts_css" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="scripts_js" runat="server">
    <link type="text/css" rel="stylesheet" href="/styles/css/tutoriales/tutoriales.css">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <!--Modal anuncio tutoriales-->
    <section class="pnl_table">
        <div class="pnl_tag">
            <p><i class="fas fa-tag"></i> bienvenido a mietib</p>
        </div>
        <div class="anuncios-body">
            <div class="anuncios-content">
                
                <div class="video-tumb" data-category="TUTORIAL">
                    <div class="video-info">
                        <h3 class="video-title">Presentación Inicial</h3>
                        <p class="video-duration">2:34 min</p>
                    </div>
                    <video src="../../Content/video/1- Presentación Inicial.mp4" controls></video>
                </div>

                <div class="video-tumb" data-category="SESIÓN">
                    <div class="video-info">
                        <h3 class="video-title">Inicio de Sesión</h3>
                        <p class="video-duration">1:45 min</p>
                    </div>
                    <video src="../../Content/video/2- Presentación Inicio de Sesión.mp4" controls></video>
                </div>

                <div class="video-tumb" data-category="NOTICIAS">
                    <div class="video-info">
                        <h3 class="video-title">Presentación Noticias</h3>
                        <p class="video-duration">3:12 min</p>
                    </div>
                    <video src="../../Content/video/3- Presentación Noticias.mp4" controls></video>
                </div>

                <div class="video-tumb" data-category="PERFIL">
                    <div class="video-info">
                        <h3 class="video-title">Datos de Perfil</h3>
                        <p class="video-duration">2:18 min</p>
                    </div>
                    <video src="../../Content/video/4- Presentación Datos de Perfil.mp4" controls></video>
                </div>

                <div class="video-tumb" data-category="FAMILIA">
                    <div class="video-info">
                        <h3 class="video-title">Núcleo Familiar</h3>
                        <p class="video-duration">1:58 min</p>
                    </div>
                    <video src="../../Content/video/5- Presentación Núcleo Familiar.mp4" controls></video>
                </div>

                <div class="video-tumb" data-category="FOTO">
                    <div class="video-info">
                        <h3 class="video-title">Foto de Perfil</h3>
                        <p class="video-duration">1:32 min</p>
                    </div>
                    <video src="../../Content/video/6-Presentación Foto de Perfil.mp4" controls></video>
                </div>

                <div class="video-tumb" data-category="DIRECTORIO">
                    <div class="video-info">
                        <h3 class="video-title">Directorio Empresarial</h3>
                        <p class="video-duration">2:45 min</p>
                    </div>
                    <video src="../../Content/video/7-Presentación Directorio Empresarial.mp4" controls></video>
                </div>

                <div class="video-tumb" data-category="APPS">
                    <div class="video-info">
                        <h3 class="video-title">Aplicativos</h3>
                        <p class="video-duration">3:22 min</p>
                    </div>
                    <video src="../../Content/video/8-Presentación Aplicativos.mp4" controls></video>
                </div>

                <div class="video-tumb" data-category="SEGURIDAD">
                    <div class="video-info">
                        <h3 class="video-title">Actualización Contraseña</h3>
                        <p class="video-duration">1:15 min</p>
                    </div>
                    <video src="../../Content/video/9- Presentación Actualización Contraseña.mp4" controls></video>
                </div>

            </div>
        </div>
    </section>

</asp:Content>
