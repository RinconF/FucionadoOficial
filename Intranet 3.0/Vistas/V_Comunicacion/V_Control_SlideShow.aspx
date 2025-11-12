<%@ Page Title="SlideShow" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="V_Control_SlideShow.aspx.cs" Inherits="Intranet_3._0.Vistas.V_Comunicacion.V_Control_SlideShow" %>

<asp:Content ID="Content1" ContentPlaceHolderID="scripts_css" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="scripts_js" runat="server">
    <script>
        $(document).ready(function () {
            ////////////Funciones de noticia////////////////
            //modal actualizar
            $('body').on('click', '.btn-actu-grupo', function () {
                if ($("#MainContent_tbl_grupos input[name='rd_estado_vista']:radio").is(':checked')) {
                    $('#modal_actualizar_grupo').addClass('modal-i-gl-show');
                    $('#modal_actualizar_grupo').removeClass('modal-i-gl-hide');


                    $.ajax({
                        type: "POST",
                        url: "WebService_V_Comunicacion.asmx/cargar_datos_modal_actualizar_slidernoticia",
                        data: '{"Id_Noticia": "' + $('input:radio[name=rd_estado_vista]:checked').val() + '"}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (resultado) {
                            let items = resultado.d;
                            $.each(items, function (index, item) {
                                $("#MainContent_txt_descripcion_pub").val(item[1]);
                                $('#datos_imagen').val(item[2]);
                                $("#MainContent_datos_img").val(item[2]);
                            });
                        }
                    });

                } else {
                    $('#modal_actualizar_grupo').addClass('modal-i-gl-hide');
                    $('#modal_actualizar_grupo').removeClass('modal-i-gl-show');

                    //notificación
                    $('.modal-noti').addClass('modal-noti-show');//agregar
                    $('.modal-noti').removeClass('modal-noti-hide');//quitar
                    $('.body-noti').addClass('advert'); //tipo notificación
                    $('.title-noti').html('<span class="fas fa-exclamation-circle"></span> Validación advertencia');//título
                    $('.content-noti').html('Seleccione una publicacion del slide para validar.');//mensaje
                    setTimeout(function () {
                        $(".modal-noti").addClass("modal-noti-hide");
                        $(".modal-noti").removeClass("modal-noti-show");
                    }, 4000);
                }
            });






            $('body').on('change', 'input[type="file"]', function () {
                var ext = $(this).val().split('.').pop();
                if ($(this).val() != '') {
                    if (ext === "jpg" || ext === "jpeg" || ext === "gif" || ext === "png" || ext === "jfif") {
                        if ($(this)[0].files[0].size > 3072000) {
                            alert("El documento excede el tamaño máximo de 3MB");
                            $(this).val('');
                        }
                    }
                    else {
                        $(this).val('');
                        alert("Extensión no permitida: " + ext);
                    }
                }
            });




        });
    </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <section class="pnl_table">
                <div class="pnl_tag">
                    <p><i class="fas fa-tag"></i>Tabla de publicaciones</p>
                </div>
                <div class="filter">
                    <div class="box_menu_crear">
                        <button 
                            type="button" 
                            id="btn_crear_publicacion" 
                            class="btn-modal" 
                            data-id="modal_crear_noticia">
                            <i class="fas fa-plus"></i>Nueva publicación carrusel
                        </button>
                        <button 
                            type="button" 
                            id="btn_actualizar_grupo" 
                            class="btn-actu-grupo" 
                            data-id="modal_actualizar_grupo">
                            <i class="fas fa-cog"></i>Actualizar publicación carrusel
                        </button>
                    </div>
                    <div class="box_search">
                        <i class="fas fa-search"></i>
                        <asp:TextBox runat="server" ID="txt_filter_grupo" placeholder="Búsqueda rápida" OnTextChanged="txt_filter_grupo_TextChanged" AutoPostBack="true"></asp:TextBox>
                    </div>
                </div>
                <table runat="server" id="tbl_grupos" class="tbl_vistas_general"></table>
            </section>
        </ContentTemplate>
    </asp:UpdatePanel>

    <!--modales-->
    <!--GRUPOS-->
    <!--MODAL GRUPOS-->
    <div class="modal-i-gl modal-i-gl-hide animated fadeIn" id="modal_crear_noticia">
        <div class="modal-i-gl-body">
            <div class="modal-i-gl-title">
                <h1 class="title">Crear nueva publicacion</h1>
                <div class="modal-i-gl-cerrar">
                    <button type="button" class="btn-modal-close"><i class="fas fa-times"></i></button>
                </div>
            </div>
            <div class="modal-i-gl-content">

                <!--Aquí el contenido-->
                <section class="box_content_crear_vista">
                    <div class="content row">
                        <div class="pnl_input col">
                            <i class="fas fa-align-right"></i>
                            <asp:TextBox runat="server" ID="txt_descripcion" MaxLength="250" placeholder="DESCRIPCIÓN DE LA PUBLICACION"></asp:TextBox>
                        </div>
                    </div>
                    <div class="content row">
                        <div class="pnl_input col">
                            <i class="fas fa-images"></i>
                            <asp:FileUpload runat="server" ID="fud_Adjunto" accept="image/png, image/gif, image/jpeg, image/jfif" />
                        </div>
                    </div>
                    <asp:LinkButton runat="server" ID="lnk_crear_" OnClick="lnk_crear__Click">CREAR</asp:LinkButton>
                </section>

            </div>
        </div>
    </div>

    <!--MODAL ACTUALIZAR GRUPOS-->
    <div class="modal-i-gl modal-i-gl-hide animated fadeIn" id="modal_actualizar_grupo">
        <div class="modal-i-gl-body">
            <div class="modal-i-gl-title">
                <h1 class="title">Actualizar datos grupo</h1>
                <div class="modal-i-gl-cerrar">
                    <button type="button" class="btn-modal-close"><i class="fas fa-times"></i></button>
                </div>
            </div>
            <div class="modal-i-gl-content">

                <!--Aquí el contenido-->
                <section class="box_content_crear_vista">
                    <div class="content row">
                        <div class="pnl_input col">
                            <i class="fas fa-align-right"></i>
                            <asp:TextBox runat="server" ID="txt_descripcion_pub" MaxLength="250" placeholder="DESCRIPCIÓN DE LA PUBLICACION"></asp:TextBox>
                        </div>
                    </div>
                    <div class="content row">
                        <div class="pnl_input col">
                            <i class="fas fa-images"></i>
                            <asp:FileUpload runat="server" ID="fud_Adjunto_pub" accept="image/png, image/gif, image/jpeg, image/jfif" />
                            
                            <input type="hidden" name="imagen" id="datos_imagen" />
                            <asp:TextBox type="hidden" runat="server" ID="datos_img" MaxLength="250" placeholder="DATOS"></asp:TextBox>
                        </div>
                        <div class="col">
                            <asp:LinkButton runat="server" OnClick="Unnamed_Click">
                                <i class="fas fa-trash"></i>
                            </asp:LinkButton>
                        </div>
                    </div>
                    <asp:LinkButton runat="server" ID="LinkButton1" OnClick="Actualizar_datos_publicacion">ACTUALIZAR</asp:LinkButton>
                </section>

            </div>
        </div>
    </div>

    <script defer>
        const txt_filter_grupo = document.querySelector('#MainContent_txt_filter_grupo');
        const a = [...document.querySelectorAll('a')];

        a.forEach(button => {
            button.addEventListener('click', () => {
                button.style.display = 'none';
            })
        })
    </script>
</asp:Content>
