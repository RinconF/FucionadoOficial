<%@ Page ValidateRequest="false" Title="Módulos" Language="C#"
    MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="V_Control_Noticia.aspx.cs"
    Inherits="Intranet_3._0.Vistas.V_Comunicacion.V_Control_Noticia" %>

<asp:Content ID="Content1" ContentPlaceHolderID="scripts_css" runat="server">
    <style>
        .button {
            background: none;
            border: 1px solid rgba(22, 160, 133, 1);
            color: rgba(22, 160, 133, 1);
            padding: 10px 25px;
            margin-left: 5px;
            border-radius: 50px;
            outline: none;
            box-shadow: 2px 2px 5px rgb(0 0 0 / 20%);
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="scripts_js" runat="server">
    <script>
        $(document).ready(function () {

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

            ////////////Funciones de noticia////////////////
            //modal actualizar
            //$('body').on('click', '.btn-actu-grupo', function () {
            //    if ($("#MainContent_tbl_grupos input[name='rd_estado_vista']:radio").is(':checked')) {
            //        $('#modal_actualizar_grupo').addClass('modal-i-gl-show');
            //        $('#modal_actualizar_grupo').removeClass('modal-i-gl-hide');
            //        $.ajax({
            //            type: "POST",
            //            url: "WebService_V_Comunicacion.asmx/cargar_datos_modal_actualizar_noticia",
            //            data: '{"Id_Noticia": "' + $('input:radio[name=rd_estado_vista]:checked').val() + '"}',
            //            contentType: "application/json; charset=utf-8",
            //            dataType: "json",
            //            success: function (resultado) {
            //                let items = resultado.d;
            //                $.each(items, function (index, item) {
            //                    $("#MainContent_txt_titulo_pub").val(item[1]);
            //                    $("#MainContent_txt_descripcion_pub").val(item[2]);
            //                    if (item[4].toLowerCase() === 'true') {
            //                        $("#MainContent_chck_estado").prop('checked', true);
            //                    } else {
            //                        $("#MainContent_chck_estado").prop('checked', false);
            //                    }
            //                    $("#MainContent_fud_Adjunto_pub").val(item[3])
            //                });
            //            }
            //        });
            //    } else {
            //        $('#modal_actualizar_grupo').addClass('modal-i-gl-hide');
            //        $('#modal_actualizar_grupo').removeClass('modal-i-gl-show');
            //        //notificación
            //        $('.modal-noti').addClass('modal-noti-show');//agregar
            //        $('.modal-noti').removeClass('modal-noti-hide');//quitar
            //        $('.body-noti').addClass('advert'); //tipo notificación
            //        $('.title-noti').html('<span class="fas fa-exclamation-circle"></span> Validación advertencia');//título
            //        $('.content-noti').html('Seleccione una publicacion para validar.');//mensaje
            //        setTimeout(function () {
            //            $(".modal-noti").addClass("modal-noti-hide");
            //            $(".modal-noti").removeClass("modal-noti-show");
            //        }, 4000);
            //    }
            //});
        });
    </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="PanelUpdate" runat="server">
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
                            <i class="fas fa-plus"></i>Nueva publicacion
                        </button>
                        <%--<asp:Button Text="Actualizar publicacion" ID="btn_actualizar_grupo" runat="server" CssClass="button btn-actu-grupo" OnClick="btn_actualizar_grupo_Click" />--%>
                        <button
                            type="button"
                            id="btn_actualizar_grupo"
                            class="btn-actu-grupo"
                            data-id="modal_actualizar_grupo">
                            <i class="fas fa-cog"></i>Actualizar publicacion
                        </button>
                    </div>
                    <div class="box_search">
                        <i class="fas fa-search"></i>
                        <asp:TextBox
                            runat="server"
                            ID="txt_filter_grupo"
                            OnTextChanged="txt_filter_grupo_TextChanged"
                            AutoPostBack="true"
                            AutoComplete="off"
                            placeholder="Búsqueda rápida"></asp:TextBox>
                    </div>
                </div>
                <table
                    runat="server"
                    id="tbl_grupos"
                    class="tbl_vistas_general">
                </table>
            </section>
        </ContentTemplate>
    </asp:UpdatePanel>

    <!--modales-->
    <!--GRUPOS-->
    <!--MODAL GRUPOS-->
    <div
        class="modal-i-gl modal-i-gl-hide animated fadeIn"
        id="modal_crear_noticia">
        <div class="modal-i-gl-body">
            <div class="modal-i-gl-title">
                <h1 class="title">Crear nueva publicacion</h1>
                <div class="modal-i-gl-cerrar">
                    <button type="button" class="btn-modal-close">
                        <i class="fas fa-times"></i>
                    </button>
                </div>
            </div>
            <div class="modal-i-gl-content">
                <!--Aquí el contenido-->
                <section class="box_content_crear_vista">
                    <div class="content row">
                        <div class="pnl_input col">
                            <i class="far fa-keyboard"></i>
                            <asp:TextBox
                                runat="server"
                                ID="txt_titulo"
                                MaxLength="30"
                                placeholder="TITULO DE LA PUBLICACION"></asp:TextBox>
                        </div>

                        <div class="pnl_input col">
                            <i class="fas fa-align-right"></i>
                            <asp:TextBox
                                runat="server"
                                ID="txt_descripcion"
                                placeholder="DESCRIPCIÓN DE LA PUBLICACION"></asp:TextBox>
                        </div>
                    </div>
                    <div class="content row">
                        <div class="pnl_input col">
                            <i class="fas fa-images"></i>
                            <asp:FileUpload runat="server" ID="fud_Adjunto" accept="image/png, image/gif, image/jpeg, image/jfif" />
                        </div>
                    </div>
                    <asp:LinkButton
                        runat="server"
                        ID="lnk_crear_"
                        OnClick="Crear_nueva_publicacion">CREAR</asp:LinkButton>
                </section>
            </div>
        </div>
    </div>

    <!--MODAL ACTUALIZAR GRUPOS-->
    <div
        class="modal-i-gl modal-i-gl-hide animated fadeIn"
        id="modal_actualizar_grupo">
        <div class="modal-i-gl-body">
            <div class="modal-i-gl-title">
                <h1 class="title">Actualizar datos grupo</h1>
                <div class="modal-i-gl-cerrar">
                    <button type="button" class="btn-modal-close">
                        <i class="fas fa-times"></i>
                    </button>
                </div>
            </div>
            <div class="modal-i-gl-content">
                <!--Aquí el contenido-->
                <section class="box_content_crear_vista">
                    <div class="content row">
                        <div class="pnl_input col">
                            <i class="far fa-keyboard"></i>
                            <asp:TextBox
                                runat="server"
                                ID="txt_titulo_pub"
                                MaxLength="30"
                                placeholder="TITULO DE LA PUBLICACION"></asp:TextBox>
                        </div>

                        <div class="pnl_input col">
                            <i class="fas fa-align-right"></i>
                            <asp:TextBox
                                runat="server"
                                ID="txt_descripcion_pub"
                                placeholder="DESCRIPCIÓN DE LA PUBLICACION"></asp:TextBox>
                        </div>
                    </div>
                    <div class="content row">
                        <div class="pnl_input col">
                            <i class="fas fa-images"></i>
                            <asp:FileUpload runat="server" ID="fud_Adjunto_pub" accept="image/png, image/gif, image/jpeg, image/jfif" />
                        </div>
                        <div class="col">
                            <asp:LinkButton runat="server" OnClick="Unnamed_Click">
                    <i class="fas fa-trash"></i>
                            </asp:LinkButton>
                        </div>
                    </div>
                    <asp:LinkButton
                        runat="server"
                        ID="LinkButton1"
                        OnClick="Actualizar_datos_publicacion">ACTUALIZAR</asp:LinkButton>
                </section>
            </div>
        </div>
    </div>

    <script defer>
        const txt_filter_grupo = document.querySelector('#MainContent_txt_filter_grupo')
        const lnk_crear_ = document.querySelector('#MainContent_lnk_crear_');
        const a = [...document.querySelectorAll('a')];

        const ejecutarDatos = () => {
            //Obtencion de los botones para la apertura de modales
            const fotoNoticia = document.querySelector('#MainContent_lnk_crear_');
            const actualizarNoticia = document.querySelector('.btn-actu-grupo');

            //Obtencion de datos directos del html como tabla e inputs
            const modalActualizarGrupo = document.querySelector(
                '#modal_actualizar_grupo'
            );
            let tablas = [
                ...document.querySelectorAll('input[name="rd_estado_vista"]'),
            ];

            const tituloPublicacion = document.querySelector(
                '#MainContent_txt_titulo_pub'
            );
            const descripcionPublicacion = document.querySelector(
                '#MainContent_txt_descripcion_pub'
            );
            const estadoPublicacion = document.querySelector(
                '#MainContent_chck_estado'
            );
            const archivoPublicacion = document.querySelector(
                '#MainContent_fud_Adjunto_pub'
            );

            actualizarNoticia.addEventListener('click', (e) => {
                e.preventDefault();

                tablas.map(async (tabla) => {
                    if (tabla.checked) {
                        modalActualizarGrupo.classList.add('modal-i-gl-show');
                        modalActualizarGrupo.classList.remove('modal-i-gl-hide');

                        const actualizar = await fetch(
                            `WebService_V_Comunicacion.asmx/cargar_datos_modal_actualizar_noticia`,
                            {
                                method: 'POST',
                                headers: {
                                    'Content-Type': 'application/json',
                                },
                                body: JSON.stringify({
                                    Id_Noticia: tabla.value,
                                }),
                            }
                        );
                        const datos = await actualizar.json();
                        const items = [...datos.d];

                        items.map((item) => {
                            tituloPublicacion.value = item[1];
                            descripcionPublicacion.value = item[2];
                            if (item[4].toLowerCase() === 'true') {
                                estadoPublicacion.checked = true;
                            } else {
                                estadoPublicacion.checked = false;
                            }
                            archivoPublicacion.value = item[3];
                        });
                    } else {
                    }
                });
            });
        }

        lnk_crear_.addEventListener('click', () => {
            lnk_crear_.style.display = 'none';
        });

        a.forEach(button => {
            button.addEventListener('click', () => {
                button.style.display = 'none';
            })
        })

    </script>
</asp:Content>
