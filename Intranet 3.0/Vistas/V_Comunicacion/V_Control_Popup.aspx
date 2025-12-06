<%@ Page ValidateRequest="false" Title="Control de Popups" Language="C#"
    MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="V_Control_Popup.aspx.cs"
    Inherits="Intranet_3._0.Vistas.V_Comunicacion.V_Control_Popup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="scripts_css" runat="server">
    <style>
        /* Estilos base */
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

        /* Badges de estado */
        .badge {
            padding: 5px 12px;
            border-radius: 12px;
            font-size: 11px;
            font-weight: bold;
            display: inline-block;
        }
        .badge-success {
            background-color: #27ae60;
            color: white;
        }
        .badge-secondary {
            background-color: #95a5a6;
            color: white;
        }

        /* Selector de roles */
        .roles-selector {
            border: 1px solid #ddd;
            padding: 15px;
            border-radius: 8px;
            margin: 10px 0;
            max-height: 250px;
            overflow-y: auto;
        }
        .roles-selector label {
            display: contents;
            margin-bottom: 8px;
            cursor: pointer;
        }
        .roles-selector input[type="checkbox"] {
            margin-right: 8px;
        }

        .body-content .tbl_vistas_general th {
            background-color: rgb(40 55 71 / 50%);
            color: #343a40;
            font-weight: bold;
        }

        .body-content .tbl_vistas_general tr {
            background: #fff;
        }

        /* Campos de fecha y número */
        .input-group {
            display: flex;
            gap: 10px;
            margin-bottom: 15px;
        }
        .input-group > div {
            flex: 1;
        }
        .input-group label {
            display: block;
            font-weight: bold;
            margin-bottom: 5px;
            color: #555;
        }

        /* NUEVO: Selector de tipo de multimedia */
        .media-type-selector {
            display: flex;
            gap: 10px;
            margin-bottom: 15px;
        }
        .media-type-btn {
            flex: 1;
            padding: 12px;
            border: 2px solid #ddd;
            border-radius: 8px;
            background: white;
            cursor: pointer;
            transition: all 0.3s;
            text-align: center;
            font-weight: 600;
        }
        .media-type-btn:hover {
            border-color: #3498db;
            background: #ecf0f1;
        }
        .media-type-btn.active-ninguno {
            border-color: #3498db;
            background: #e3f2fd;
            color: #2980b9;
        }
        .media-type-btn.active-imagen {
            border-color: #27ae60;
            background: #e8f8f5;
            color: #27ae60;
        }
        .media-type-btn.active-video {
            border-color: #9b59b6;
            background: #f4ecf7;
            color: #9b59b6;
        }
        .media-upload-area {
            display: none;
            margin-top: 15px;
        }
        .media-upload-area.show {
            display: block;
        }

        /* Textarea con ancho completo */
        .pnl_input textarea {
            width: 100% !important;
            box-sizing: border-box;
            resize: vertical;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="scripts_js" runat="server">
    <script>
        $(document).ready(function () {
            const extensionesImagen = ["jpg", "jpeg", "gif", "png", "jfif"];
            const extensionesVideo = ["mp4", "webm", "ogg"];
            const limiteImagen = 3 * 1024 * 1024; // 3 MB
            const limiteVideo = 50 * 1024 * 1024; // 50 MB

            $('body').on('change', 'input[type="file"]', function () {
                const inputFile = $(this)[0];
                if (!inputFile.files || inputFile.files.length === 0) {
                    return;
                }

                const archivo = inputFile.files[0];
                const ext = archivo.name.split('.').pop().toLowerCase();

                if (extensionesImagen.includes(ext)) {
                    if (archivo.size > limiteImagen) {
                        alert("La imagen excede el tamaño máximo de 3MB");
                        inputFile.value = '';
                    }
                    return;
                }

                if (extensionesVideo.includes(ext)) {
                    if (archivo.size > limiteVideo) {
                        alert("El video excede el tamaño máximo de 50MB");
                        inputFile.value = '';
                    }
                    return;
                }

                alert("Extensión no permitida: " + ext + ". Solo se permiten imágenes (JPG, PNG, GIF, JFIF) o videos (MP4, WEBM, OGG).");
                inputFile.value = '';
            });

            // ========================================
            // SELECTOR DE TIPO DE MULTIMEDIA - CREAR
            // ========================================
            window.setupMediaSelector = function (prefix) {
                const btnNinguno = document.getElementById('btn_media_ninguno_' + prefix);
                const btnImagen = document.getElementById('btn_media_imagen_' + prefix);
                const btnVideo = document.getElementById('btn_media_video_' + prefix);
                const areaImagen = document.getElementById('area_imagen_' + prefix);
                const areaVideo = document.getElementById('area_video_' + prefix);

                function setActiveMedia(type) {
                    // Remover todas las clases activas
                    btnNinguno.classList.remove('active-ninguno');
                    btnImagen.classList.remove('active-imagen');
                    btnVideo.classList.remove('active-video');
                    areaImagen.classList.remove('show');
                    areaVideo.classList.remove('show');

                    // Activar el seleccionado
                    if (type === 'ninguno') {
                        btnNinguno.classList.add('active-ninguno');
                    } else if (type === 'imagen') {
                        btnImagen.classList.add('active-imagen');
                        areaImagen.classList.add('show');
                    } else if (type === 'video') {
                        btnVideo.classList.add('active-video');
                        areaVideo.classList.add('show');
                    }
                }

                btnNinguno.addEventListener('click', () => setActiveMedia('ninguno'));
                btnImagen.addEventListener('click', () => setActiveMedia('imagen'));
                btnVideo.addEventListener('click', () => setActiveMedia('video'));

                // Por defecto: ninguno
                setActiveMedia('ninguno');
            };

            // Inicializar selectores al cargar la página
            setupMediaSelector('crear');
            setupMediaSelector('actualizar');
        });
    </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel runat="server" ID="PanelUpdate">
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
                            data-id="modal_crear_popup">
                            <i class="fas fa-plus"></i>Nuevo Popup
                        </button>
                        <button 
                            type="button" 
                            id="btn_actualizar_popup" 
                            class="btn-actu-grupo" 
                            data-id="modal_actualizar_popup">
                            <i class="fas fa-cog"></i>Actualizar Popup
                        </button>
                        <button 
                            type="button" 
                            id="btn_eliminar_popup" 
                            class="btn-modal"
                            style="background-color: #e74c3c; color: white;">
                            <i class="fas fa-trash"></i>Eliminar Popup
                        </button>
                        <button 
                            type="button" 
                            id="btn_estadisticas_popup" 
                            class="btn-modal">
                            <i class="fas fa-chart-bar"></i>Estadísticas
                        </button>
                    </div>
                    <div class="box_search">
                        <i class="fas fa-search"></i>
                    </div>
                </div>
                <div runat="server" id="tbl_grupos"></div>
            </section>
        </ContentTemplate>
    </asp:UpdatePanel>

    <!-- ========================================
         MODAL: CREAR NUEVO POPUP
         ======================================== -->
    <div class="modal-i-gl modal-i-gl-hide animated fadeIn" id="modal_crear_popup">
        <div class="modal-i-gl-body">
            <div class="modal-i-gl-title">
                <h1 class="title">Crear Nuevo Popup</h1>
                <div class="modal-i-gl-cerrar">
                    <button type="button" class="btn-modal-close">
                        <i class="fas fa-times"></i>
                    </button>
                </div>
            </div>
            <div class="modal-i-gl-content">
                <section class="box_content_crear_vista">
                    <!-- Título -->
                    <div class="content row">
                        <div class="pnl_input col">
                            <i class="far fa-keyboard"></i>
                            <asp:TextBox
                                runat="server"
                                ID="txt_titulo"
                                MaxLength="200"
                                placeholder="Título del popup"></asp:TextBox>
                        </div>
                    </div>

                    <!-- Descripción -->
                    <div class="content row">
                        <div class="pnl_input col">
                            <i class="fas fa-align-right"></i>
                            <asp:TextBox
                                runat="server"
                                ID="txt_descripcion"
                                TextMode="MultiLine"
                                Rows="4"
                                placeholder="Descripción del popup"></asp:TextBox>
                        </div>
                    </div>

                    <!-- URL y Tiempo de visualización -->
                    <div class="input-group">
                        <div class="pnl_input">
                            <label><i class="fas fa-link"></i> URL (opcional)</label>
                            <asp:TextBox
                                runat="server"
                                ID="txt_url"
                                placeholder="https://ejemplo.com"></asp:TextBox>
                        </div>
                        <div class="pnl_input">
                            <label><i class="fas fa-clock"></i> Tiempo de visualización (segundos)</label>
                            <asp:TextBox
                                runat="server"
                                ID="txt_tiempo"
                                TextMode="Number"
                                Text="5"
                                min="1"
                                max="60"
                                placeholder="5"></asp:TextBox>
                        </div>
                    </div>

                    <!-- Vigencia: Fecha Inicio y Fecha Fin -->
                    <div class="input-group">
                        <div class="pnl_input">
                            <label><i class="fas fa-calendar-alt"></i> Fecha inicio</label>
                            <asp:TextBox
                                runat="server"
                                ID="txt_fecha_inicio"
                                TextMode="Date"></asp:TextBox>
                        </div>
                        <div class="pnl_input">
                            <label><i class="fas fa-calendar-times"></i> Fecha fin (opcional)</label>
                            <asp:TextBox
                                runat="server"
                                ID="txt_fecha_fin"
                                TextMode="Date"></asp:TextBox>
                        </div>
                    </div>

                    <!-- Selector de Roles (CheckBoxList) -->
                    <div class="roles-selector">
                        <label style="font-weight: bold; display: block; margin-bottom: 10px;">
                            <i class="fas fa-users"></i> Roles que pueden visualizar:
                        </label>
                        <asp:CheckBoxList ID="chkl_roles" runat="server"></asp:CheckBoxList>
                        <small style="color: #7f8c8d;">Si no seleccionas ningún rol, será visible para todos</small>
                    </div>

                    <!-- NUEVO: Selector de Tipo de Multimedia -->
                    <div class="content row">
                        <div class="col">
                            <label style="font-weight: bold; display: block; margin-bottom: 10px;">
                                <i class="fas fa-photo-video"></i> Contenido Multimedia (Opcional)
                            </label>
                            
                            <div class="media-type-selector">
                                <button type="button" id="btn_media_ninguno_crear" class="media-type-btn">
                                    <i class="fas fa-times"></i><br>Sin multimedia
                                </button>
                                <button type="button" id="btn_media_imagen_crear" class="media-type-btn">
                                    <i class="fas fa-image"></i><br>Imagen
                                </button>
                                <button type="button" id="btn_media_video_crear" class="media-type-btn">
                                    <i class="fas fa-video"></i><br>Video
                                </button>
                            </div>

                            <!-- Área de carga de Imagen -->
                            <div id="area_imagen_crear" class="media-upload-area">
                                <div class="pnl_input">
                                    <label><i class="fas fa-images"></i> Cargar imagen</label>
                                    <asp:FileUpload
                                        runat="server"
                                        ID="fud_Adjunto"
                                        accept="image/png, image/gif, image/jpeg, image/jfif" />
                                    <small style="color: #7f8c8d;">Tamaño máximo: 3MB. Formatos: JPG, PNG, GIF, JFIF</small>
                                </div>
                            </div>

                            <!-- Área de carga de Video -->
                            <div id="area_video_crear" class="media-upload-area">
                                <div class="pnl_input">
                                    <label><i class="fas fa-video"></i> Cargar video</label>
                                    <asp:FileUpload
                                        runat="server"
                                        ID="fud_Video"
                                        accept="video/mp4, video/webm, video/ogg" />
                                    <small style="color: #7f8c8d;">Tamaño máximo:  4.7 MB. Formatos: MP4, WEBM, OGG</small>
                                </div>
                            </div>
                        </div>
                    </div>

                    <!-- Botón Crear -->
                    <div class="content row" style="margin-top: 20px;">
                        <div class="col" style="text-align: right;">
                            <asp:LinkButton
                                runat="server"
                                ID="lnk_crear_popup"
                                CssClass="button"
                                OnClick="lnk_crear_popup_Click"
                                Style="background-color: #27ae60; color: white; padding: 12px 40px;">
                                <i class="fas fa-save"></i> Crear Popup
                            </asp:LinkButton>
                        </div>
                    </div>
                </section>
            </div>
        </div>
    </div>

    <!-- ========================================
         MODAL: ACTUALIZAR POPUP
         ======================================== -->
    <div class="modal-i-gl modal-i-gl-hide animated fadeIn" id="modal_actualizar_popup">
        <div class="modal-i-gl-body">
            <div class="modal-i-gl-title">
                <h1 class="title">Actualizar Popup</h1>
                <div class="modal-i-gl-cerrar">
                    <button type="button" class="btn-modal-close">
                        <i class="fas fa-times"></i>
                    </button>
                </div>
            </div>
            <div class="modal-i-gl-content">
                <section class="box_content_crear_vista">
                    <!-- ID oculto del popup a actualizar -->
                    <asp:HiddenField ID="hf_id_popup" runat="server" />

                    <!-- Título -->
                    <div class="content row">
                        <div class="pnl_input col">
                            <i class="far fa-keyboard"></i>
                            <asp:TextBox
                                runat="server"
                                ID="txt_titulo_pub"
                                MaxLength="200"
                                placeholder="Título del popup"></asp:TextBox>
                        </div>
                    </div>

                    <!-- Descripción -->
                    <div class="content row">
                        <div class="pnl_input col">
                            <i class="fas fa-align-right"></i>
                            <asp:TextBox
                                runat="server"
                                ID="txt_descripcion_pub"
                                TextMode="MultiLine"
                                Rows="4"
                                placeholder="Descripción del popup"></asp:TextBox>
                        </div>
                    </div>

                    <!-- URL y Tiempo -->
                    <div class="input-group">
                        <div class="pnl_input">
                            <label><i class="fas fa-link"></i> URL (opcional)</label>
                            <asp:TextBox
                                runat="server"
                                ID="txt_url_pub"
                                placeholder="https://ejemplo.com"></asp:TextBox>
                        </div>
                        <div class="pnl_input">
                            <label><i class="fas fa-clock"></i> Tiempo (segundos)</label>
                            <asp:TextBox
                                runat="server"
                                ID="txt_tiempo_pub"
                                TextMode="Number"
                                min="1"
                                max="60"></asp:TextBox>
                        </div>
                    </div>

                    <!-- Vigencia -->
                    <div class="input-group">
                        <div class="pnl_input">
                            <label><i class="fas fa-calendar-alt"></i> Fecha inicio</label>
                            <asp:TextBox
                                runat="server"
                                ID="txt_fecha_inicio_pub"
                                TextMode="Date"></asp:TextBox>
                        </div>
                        <div class="pnl_input">
                            <label><i class="fas fa-calendar-times"></i> Fecha fin</label>
                            <asp:TextBox
                                runat="server"
                                ID="txt_fecha_fin_pub"
                                TextMode="Date"></asp:TextBox>
                        </div>
                    </div>

                    <!-- Estado Activo/Inactivo -->
                    <div class="content row">
                        <div class="pnl_input col">
                            <label style="font-weight: bold; display: block; margin-bottom: 10px;">
                                <i class="fas fa-toggle-on"></i> Estado
                            </label>
                            <asp:DropDownList runat="server" ID="ddl_estado_pub" CssClass="form-control">
                                <asp:ListItem Value="1" Text="Activo"></asp:ListItem>
                                <asp:ListItem Value="0" Text="Inactivo"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>

                    <!-- Selector de Roles -->
                    <div class="roles-selector">
                        <label style="font-weight: bold; display: block; margin-bottom: 10px;">
                            <i class="fas fa-users"></i> Roles que pueden visualizar:
                        </label>
                        <asp:CheckBoxList ID="chkl_roles_pub" runat="server"></asp:CheckBoxList>
                    </div>

                    <!-- NUEVO: Selector de Tipo de Multimedia para Actualizar -->
                    <div class="content row">
                        <div class="col">
                            <label style="font-weight: bold; display: block; margin-bottom: 10px;">
                                <i class="fas fa-photo-video"></i> Cambiar Multimedia (Opcional)
                            </label>
                            
                            <div class="media-type-selector">
                                <button type="button" id="btn_media_ninguno_actualizar" class="media-type-btn">
                                    <i class="fas fa-times"></i><br>Sin cambios
                                </button>
                                <button type="button" id="btn_media_imagen_actualizar" class="media-type-btn">
                                    <i class="fas fa-image"></i><br>Cambiar Imagen
                                </button>
                                <button type="button" id="btn_media_video_actualizar" class="media-type-btn">
                                    <i class="fas fa-video"></i><br>Cambiar Video
                                </button>
                            </div>

                            <!-- Área de carga de Imagen -->
                            <div id="area_imagen_actualizar" class="media-upload-area">
                                <div class="pnl_input">
                                    <label><i class="fas fa-images"></i> Nueva imagen</label>
                                    <asp:FileUpload
                                        runat="server"
                                        ID="fud_Adjunto_pub"
                                        accept="image/png, image/gif, image/jpeg, image/jfif" />
                                    <small style="color: #7f8c8d;">Deja vacío para mantener la imagen actual</small>
                                </div>
                            </div>

                            <!-- Área de carga de Video -->
                            <div id="area_video_actualizar" class="media-upload-area">
                                <div class="pnl_input">
                                    <label><i class="fas fa-video"></i> Nuevo video</label>
                                    <asp:FileUpload
                                        runat="server"
                                        ID="fud_Video_pub"
                                        accept="video/mp4, video/webm, video/ogg" />
                                    <small style="color: #7f8c8d;">Deja vacío para conservar el video actual</small>
                                </div>
                            </div>
                        </div>
                    </div>

                    <!-- Botón Actualizar -->
                    <div class="content row" style="margin-top: 20px;">
                        <div class="col" style="text-align: right;">
                            <asp:LinkButton
                                runat="server"
                                ID="lnk_actualizar_popup"
                                CssClass="button"
                                OnClick="lnk_actualizar_popup_Click"
                                Style="background-color: #3498db; color: white; padding: 12px 40px;">
                                <i class="fas fa-sync-alt"></i> Actualizar Popup
                            </asp:LinkButton>
                        </div>
                    </div>
                </section>
            </div>
        </div>
    </div>

    <!-- ========================================
         MODAL: CONFIRMAR ELIMINACIÓN
         ======================================== -->
    <div class="modal-i-gl modal-i-gl-hide animated fadeIn" id="modal_eliminar_popup">
        <div class="modal-i-gl-body" style="max-width: 500px;">
            <div class="modal-i-gl-title">
                <h1 class="title" style="color: #e74c3c;">
                    <i class="fas fa-exclamation-triangle"></i> Eliminar Popup
                </h1>
                <div class="modal-i-gl-cerrar">
                    <button type="button" class="btn-modal-close">
                        <i class="fas fa-times"></i>
                    </button>
                </div>
            </div>
            <div class="modal-i-gl-content">
                <section style="text-align: center; padding: 20px;">
                    <asp:HiddenField ID="hf_id_popup_eliminar" runat="server" />
                    <p style="font-size: 16px; margin-bottom: 10px;">
                        ¿Está seguro que deseas eliminar este Popup?
                    </p>
                    <p id="popup_titulo_eliminar" style="font-weight: bold; color: #2c3e50; margin-bottom: 20px;">
                        <!-- Se llenará por JavaScript -->
                    </p>
                    <div style="display: flex; gap: 10px; justify-content: center;">
                        <asp:LinkButton
                            runat="server"
                            ID="lnk_eliminar_popup"
                            CssClass="button"
                            OnClick="lnk_eliminar_popup_Click"
                            Style="background-color: #e74c3c; color: white; padding: 12px 30px; border-radius: 25px;">
                            <i class="fas fa-trash"></i> Eliminar
                        </asp:LinkButton>
                        <button 
                            type="button" 
                            class="btn btn-modal-close"
                            style="background-color: #95a5a6; color: white; padding: 12px 30px; border-radius: 25px; border: none;">
                            <i class="fas fa-times"></i> Cancelar
                        </button>
                    </div>
                </section>
            </div>
        </div>
    </div>

    <!-- ========================================
         JAVASCRIPT: Manejo de Modales y AJAX
         ======================================== -->
    <script defer>
        // Helper: formato fecha yyyy-MM-dd
        function formatearFecha(fecha) {
            if (!fecha) return '';
            const d = new Date(fecha);
            if (isNaN(d)) return '';
            const year = d.getFullYear();
            const month = String(d.getMonth() + 1).padStart(2, '0');
            const day = String(d.getDate()).padStart(2, '0');
            return `${year}-${month}-${day}`;
        }

        // Devuelve el radio actualmente seleccionado
        function getPopupSeleccionado() {
            return document.querySelector('input[name="rd_estado_vista"]:checked');
        }

        // Función principal para enganchar eventos
        function ejecutarDatos() {
            // Botones principales
            const btnActualizarPopup = document.querySelector('#btn_actualizar_popup');
            const btnEliminarPopup = document.querySelector('#btn_eliminar_popup');
            const btnEstadisticas = document.querySelector('#btn_estadisticas_popup');

            // Modales
            const modalActualizarPopup = document.querySelector('#modal_actualizar_popup');
            const modalEliminarPopup = document.querySelector('#modal_eliminar_popup');

            // Controles del modal Actualizar
            const tituloPublicacion = document.querySelector('#MainContent_txt_titulo_pub');
            const descripcionPublicacion = document.querySelector('#MainContent_txt_descripcion_pub');
            const urlPublicacion = document.querySelector('#MainContent_txt_url_pub');
            const tiempoPublicacion = document.querySelector('#MainContent_txt_tiempo_pub');
            const fechaInicioPublicacion = document.querySelector('#MainContent_txt_fecha_inicio_pub');
            const fechaFinPublicacion = document.querySelector('#MainContent_txt_fecha_fin_pub');
            const estadoPublicacion = document.querySelector('#MainContent_ddl_estado_pub');
            const rolesPublicacion = document.querySelector('#MainContent_chkl_roles_pub');
            const hfIdPopup = document.querySelector('#MainContent_hf_id_popup');

            // ================================
            // BOTÓN: ACTUALIZAR POPUP
            // ================================
            if (btnActualizarPopup && modalActualizarPopup && hfIdPopup) {
                btnActualizarPopup.onclick = async function (e) {
                    e.preventDefault();

                    const popupSeleccionado = getPopupSeleccionado();
                    if (!popupSeleccionado) {
                        alert('Por favor, selecciona un popup de la tabla');
                        return;
                    }

                    hfIdPopup.value = popupSeleccionado.value;

                    // Mostrar modal
                    modalActualizarPopup.classList.add('modal-i-gl-show');
                    modalActualizarPopup.classList.remove('modal-i-gl-hide');

                    try {
                        const response = await fetch('WebService_V_Comunicacion.asmx/cargar_datos_modal_actualizar_Popup', {
                            method: 'POST',
                            headers: { 'Content-Type': 'application/json; charset=utf-8' },
                            body: JSON.stringify({ Id_Popup: parseInt(popupSeleccionado.value) })
                        });

                        const datos = await response.json();
                        const item = datos.d && datos.d[0];

                        if (Array.isArray(item)) {
                            // Compatibilidad con la antigua respuesta tipo arreglo
                            tituloPublicacion.value = item[1] || '';
                            descripcionPublicacion.value = item[2] || '';
                            urlPublicacion.value = item[5] || '';
                            tiempoPublicacion.value = item[6] || 5;
                            fechaInicioPublicacion.value = formatearFecha(item[7]);
                            fechaFinPublicacion.value = formatearFecha(item[8]);
                            estadoPublicacion.value = item[9] ? '1' : '0';

                            // El índice 11 corresponde a RolesIds en el SP (luego de Tiempo_Visualizacion)
                            const rolesIds = item[11] ? item[11].split(',') : [];
                            const checkboxes = rolesPublicacion.querySelectorAll('input[type="checkbox"]');
                            checkboxes.forEach(cb => {
                                cb.checked = rolesIds.includes(cb.value);
                            });
                        } else if (item) {
                            // Nueva respuesta con propiedades nombradas
                            tituloPublicacion.value = item.Titulo || '';
                            descripcionPublicacion.value = item.Descripcion || '';
                            urlPublicacion.value = item.Url || '';
                            tiempoPublicacion.value = item.Tiempo_Visualizacion || 5;
                            fechaInicioPublicacion.value = formatearFecha(item.Fecha_Inicio);
                            fechaFinPublicacion.value = formatearFecha(item.Fecha_Fin);
                            estadoPublicacion.value = item.Estado ? '1' : '0';

                            const rolesIds = item.RolesIds ? item.RolesIds.toString().split(',') : [];
                            const checkboxes = rolesPublicacion.querySelectorAll('input[type="checkbox"]');
                            checkboxes.forEach(cb => {
                                cb.checked = rolesIds.includes(cb.value);
                            });
                        }
                    } catch (error) {
                        console.error('Error al cargar popup:', error);
                        alert('Error al cargar los datos del popup');
                    }
                };
            }

            // ================================
            // BOTÓN: ELIMINAR POPUP
            // ================================
            if (btnEliminarPopup && modalEliminarPopup) {
                btnEliminarPopup.onclick = function (e) {
                    e.preventDefault();

                    const popupSeleccionado = getPopupSeleccionado();
                    if (!popupSeleccionado) {
                        alert('Por favor, selecciona un popup de la tabla');
                        return;
                    }

                    // Guardar ID en HiddenField
                    const hfEliminar = document.querySelector('#MainContent_hf_id_popup_eliminar');
                    if (hfEliminar) {
                        hfEliminar.value = popupSeleccionado.value;
                    }

                    // Obtener título del popup desde la fila
                    const fila = popupSeleccionado.closest('tr');
                    const titulo = fila && fila.cells[2] ? fila.cells[2].innerText : '';

                    // Mostrar en modal de confirmación
                    const lblTituloEliminar = document.getElementById('popup_titulo_eliminar');
                    if (lblTituloEliminar) {
                        lblTituloEliminar.innerText = titulo;
                    }

                    // Mostrar modal
                    modalEliminarPopup.classList.add('modal-i-gl-show');
                    modalEliminarPopup.classList.remove('modal-i-gl-hide');
                };
            }

            // ================================
            // BOTÓN: ESTADÍSTICAS
            // ================================
            if (btnEstadisticas) {
                btnEstadisticas.onclick = async function (e) {
                    e.preventDefault();

                    const popupSeleccionado = getPopupSeleccionado();
                    if (!popupSeleccionado) {
                        alert('Por favor, selecciona un popup de la tabla');
                        return;
                    }

                    try {
                        const response = await fetch('WebService_V_Comunicacion.asmx/Obtener_Estadisticas_Popup', {
                            method: 'POST',
                            headers: { 'Content-Type': 'application/json; charset=utf-8' },
                            body: JSON.stringify({ Id_Popup: parseInt(popupSeleccionado.value) })
                        });

                        const datos = await response.json();
                        const stats = datos.d || [];

                        const texto = stats
                            .map(s => `${s[0]}: ${s[1]} (${s[2]}%)`)
                            .join('\n');

                        alert('Estadísticas:\n\n' + texto);
                    }
                    catch (error) {
                        console.error('Error estadísticas:', error);
                        alert('Error al cargar estadísticas');
                    }
                };
            }
        }

        // Prevenir doble submit SOLO en el botón de crear
        window.addEventListener('load', function () {
            const lnk_crear_ = document.querySelector('#MainContent_lnk_crear_popup');
            if (lnk_crear_) {
                lnk_crear_.addEventListener('click', () => {
                    lnk_crear_.disabled = true;
                    setTimeout(() => { lnk_crear_.disabled = false; }, 2000);
                });
            }

            ejecutarDatos();
        });

        // Reenganchar eventos después de cada postback del UpdatePanel
        if (typeof (Sys) !== "undefined" &&
            Sys.WebForms &&
            Sys.WebForms.PageRequestManager) {

            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(function () {
                ejecutarDatos();
            });
        }
    </script>

</asp:Content>