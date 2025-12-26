<%@ Page ValidateRequest="false" Title="Control de Popups" Language="C#"
    MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="V_Control_Popup.aspx.cs"
    Inherits="Intranet_3._0.Vistas.V_Comunicacion.V_Control_Popup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="scripts_css" runat="server">
    <link rel="Stylesheet" href="/Styles/css/Popup/Control_Popup.css" />
    <link rel="Stylesheet" href="/Styles/css/Popup/Control_Popup_Modern.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="scripts_js" runat="server">
    <script>
        $(document).ready(function () {
            const extensionesImagen = ["jpg", "jpeg", "png", "gif", "jfif"];
            const extensionesVideo = ["mp4", "webm", "ogg", "avi", "mov", "wmv", "flv", "mkv"];
            const limiteImagen = 3 * 1024 * 1024; // 3 MB
            const limiteVideo = 50 * 1024 * 1024; // 50 MB

            // Validación de imágenes
            $('body').on('change', '#MainContent_fud_imagen, #MainContent_fud_imagen_edit', function () {
                const inputFile = $(this)[0];
                if (!inputFile.files || inputFile.files.length === 0) {
                    return;
                }

                const archivo = inputFile.files[0];
                const ext = archivo.name.split('.').pop().toLowerCase();

                if (!extensionesImagen.includes(ext)) {
                    alert("Extensión no permitida: " + ext + ". Solo se permiten imágenes (JPG, JPEG, PNG, GIF, JFIF)");
                    inputFile.value = '';
                    return;
                }

                if (archivo.size > limiteImagen) {
                    alert("La imagen excede el tamaño máximo de 3MB");
                    inputFile.value = '';
                }
            });

            // Validación de videos
            $('body').on('change', '#MainContent_fud_video, #MainContent_fud_video_edit', function () {
                const inputFile = $(this)[0];
                if (!inputFile.files || inputFile.files.length === 0) {
                    return;
                }

                const archivo = inputFile.files[0];
                const ext = archivo.name.split('.').pop().toLowerCase();

                if (!extensionesVideo.includes(ext)) {
                    alert("Extensión no permitida: " + ext + ". Solo se permiten videos (MP4, WEBM, OGG, AVI, MOV, WMV, FLV, MKV)");
                    inputFile.value = '';
                    return;
                }

                if (archivo.size > limiteVideo) {
                    alert("El video excede el tamaño máximo de 50MB");
                    inputFile.value = '';
                }
            });

            // Búsqueda rápida en la tabla
            $('#txt_busqueda_rapida').on('keyup', function () {
                const valor = $(this).val().toLowerCase();
                $('.tbl_vistas_general tbody tr').filter(function () {
                    $(this).toggle($(this).text().toLowerCase().indexOf(valor) > -1);
                });
            });

            // ===============================================
            // MANEJO DE TIPOS DE MULTIMEDIA - MODAL CREAR
            // ===============================================
            $('.multimedia-option').on('click', function () {
                // Remover selección previa
                $('.multimedia-option').removeClass('active');
                $(this).addClass('active');

                const tipo = $(this).data('tipo');

                // Ocultar todos los uploads
                $('.upload-section').hide();

                // Mostrar el upload correspondiente
                if (tipo === 'imagen') {
                    $('#upload-imagen').show();
                } else if (tipo === 'video') {
                    $('#upload-video').show();
                }
                // Si es 'ninguno', no mostramos nada
            });

            // ===============================================
            // MANEJO DE TIPOS DE MULTIMEDIA - MODAL EDITAR
            // ===============================================
            $('.multimedia-option-edit').on('click', function () {
                // Remover selección previa
                $('.multimedia-option-edit').removeClass('active');
                $(this).addClass('active');

                const tipo = $(this).data('tipo');

                // Ocultar todos los uploads
                $('.upload-section-edit').hide();

                // Mostrar el upload correspondiente
                if (tipo === 'imagen') {
                    $('#upload-imagen-edit').show();
                } else if (tipo === 'video') {
                    $('#upload-video-edit').show();
                }
            });

            // Seleccionar "Sin multimedia" por defecto
            $('.multimedia-option[data-tipo="ninguno"]').click();
        });
    </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel runat="server" ID="PanelUpdate">
        <ContentTemplate>
            <section class="pnl_table">
                <div class="pnl_tag">
                    <p><i class="fas fa-window-restore"></i>Tabla de Popups</p>
                </div>
                <div class="filter">
                    <div class="box_menu_crear">
                        <button 
                            type="button" 
                            id="btn_modal_crear" 
                            class="btn-modal" 
                            data-id="modal_crear_popup">
                            <i class="fas fa-plus"></i>Nuevo Popup
                        </button>
                        <button 
                            type="button" 
                            ID="btn_modal_actualizar" 
                            class="btn-actu-grupo" 
                            data-id="modal_actualizar_popup">
                            <i class="fas fa-cog"></i>Actualizar Popup
                        </button>
                        <button 
                            type="button" 
                            id="btn_modal_eliminar" 
                            class="btn-modal btn-eliminar-popup">
                            <i class="fas fa-trash"></i>Eliminar Popup
                        </button>
                    </div>
                    <div class="contenedor-busqueda">
                        <input 
                            type="text" 
                            id="txt_busqueda_rapida" 
                            class="input-busqueda-rapida"
                            placeholder="Búsqueda rápida" />
                    </div>
                </div>
                <div class="table-responsive">
                    <asp:Literal ID="lit_tabla_popups" runat="server"></asp:Literal>
                </div>
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
                                MaxLength="500"
                                placeholder="Descripción del popup"></asp:TextBox>
                        </div>
                    </div>

                    <!-- CONTENIDO MULTIMEDIA MEJORADO -->
                    <div class="content row">
                        <div class="col">
                            <label class="section-label">
                                <i class="fas fa-photo-video"></i> Contenido Multimedia (Opcional)
                            </label>
                            <p class="section-description">Selecciona el tipo de contenido que deseas agregar</p>
                            
                            <!-- Opciones de multimedia -->
                            <div class="multimedia-selector">
                                <div class="multimedia-option" data-tipo="ninguno">
                                    <div class="multimedia-icon">
                                        <i class="fas fa-ban"></i>
                                    </div>
                                    <div class="multimedia-label">Sin multimedia</div>
                                </div>
                                
                                <div class="multimedia-option" data-tipo="imagen">
                                    <div class="multimedia-icon">
                                        <i class="fas fa-image"></i>
                                    </div>
                                    <div class="multimedia-label">Imagen</div>
                                </div>
                                
                                <div class="multimedia-option" data-tipo="video">
                                    <div class="multimedia-icon">
                                        <i class="fas fa-video"></i>
                                    </div>
                                    <div class="multimedia-label">Video</div>
                                </div>
                            </div>

                            <!-- Upload de Imagen -->
                            <div id="upload-imagen" class="upload-section" style="display: none;">
                                <div class="upload-container">
                                    <i class="fas fa-cloud-upload-alt"></i>
                                    <label class="upload-label">Cargar Imagen</label>
                                    <asp:FileUpload
                                        runat="server"
                                        ID="fud_imagen"
                                        accept="image/*"
                                        CssClass="upload-input" />
                                    <small class="upload-hint">Máx: 3MB. Formatos: JPG, PNG, GIF</small>
                                </div>
                            </div>

                            <!-- Upload de Video -->
                            <div id="upload-video" class="upload-section" style="display: none;">
                                <div class="upload-container">
                                    <i class="fas fa-cloud-upload-alt"></i>
                                    <label class="upload-label">Cargar Video</label>
                                    <asp:FileUpload
                                        runat="server"
                                        ID="fud_video"
                                        accept="video/*"
                                        CssClass="upload-input" />
                                    <small class="upload-hint">Máx: 50MB. Formatos: MP4, WEBM, OGG</small>
                                </div>
                            </div>
                        </div>
                    </div>

                    <!-- URL y Tiempo -->
                    <div class="content row">
                        <div class="pnl_input col-md-6">
                            <i class="fas fa-link"></i>
                            <label class="form-label-bold">URL (opcional)</label>
                            <asp:TextBox
                                runat="server"
                                ID="txt_url"
                                placeholder="https://ejemplo.com"></asp:TextBox>
                        </div>
                        <div class="pnl_input col-md-6">
                            <i class="fas fa-clock"></i>
                            <label class="form-label-bold">Tiempo visualización (seg)</label>
                            <asp:TextBox
                                runat="server"
                                ID="txt_tiempo"
                                TextMode="Number"
                                Text="5"
                                min="1"
                                max="60"></asp:TextBox>
                        </div>
                    </div>

                    <!-- Fechas -->
                    <div class="content row">
                        <div class="pnl_input col-md-6">
                            <i class="fas fa-calendar-alt"></i>
                            <label class="form-label-bold">Fecha Inicio</label>
                            <asp:TextBox
                                runat="server"
                                ID="txt_fecha_inicio"
                                TextMode="Date"></asp:TextBox>
                        </div>
                        <div class="pnl_input col-md-6">
                            <i class="fas fa-calendar-check"></i>
                            <label class="form-label-bold">Fecha Fin (opcional)</label>
                            <asp:TextBox
                                runat="server"
                                ID="txt_fecha_fin"
                                TextMode="Date"></asp:TextBox>
                        </div>
                    </div>

                    <!-- ROLES MEJORADOS -->
                    <div class="content row">
                        <div class="col">
                            <label class="section-label">
                                <i class="fas fa-users-cog"></i> Roles que pueden visualizar
                            </label>
                            <p class="section-description">
                                Si no seleccionas ningún rol, el popup será visible para todos los usuarios
                            </p>
                            
                            <div class="roles-grid">
                                <asp:CheckBoxList ID="cbl_roles" runat="server" CssClass="roles-checkbox-list"></asp:CheckBoxList>
                            </div>
                        </div>
                    </div>

                    <!-- Botón Guardar -->
                    <div class="content row mt-20">
                        <div class="col text-right">
                            <asp:LinkButton
                                runat="server"
                                ID="btn_guardar"
                                CssClass="button btn-guardar-popup"
                                OnClick="btn_guardar_Click">
                                <i class="fas fa-save"></i> Guardar Popup
                            </asp:LinkButton>
                        </div>
                    </div>

                    <asp:Label ID="lbl_mensaje" runat="server" CssClass="msg-error"></asp:Label>
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
                    <asp:HiddenField ID="hf_id_popup" runat="server" />
                    <asp:HiddenField ID="hf_imagen_actual" runat="server" />
                    <asp:HiddenField ID="hf_video_actual" runat="server" />
                    <asp:HiddenField ID="hf_roles_actuales" runat="server" />

                    <!-- Título -->
                    <div class="content row">
                        <div class="pnl_input col">
                            <i class="far fa-keyboard"></i>
                            <asp:TextBox
                                runat="server"
                                ID="txt_titulo_edit"
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
                                ID="txt_descripcion_edit"
                                TextMode="MultiLine"
                                Rows="4"
                                MaxLength="500"
                                placeholder="Descripción del popup"></asp:TextBox>
                        </div>
                    </div>

                    <!-- Archivos actuales -->
                    <div class="content row">
                        <div class="col-md-6">
                            <label class="form-label-bold">Imagen Actual:</label>
                            <div class="archivo-actual">
                                <i class="fas fa-image"></i>
                                <asp:Label ID="lbl_imagen_actual" runat="server" Text="No hay imagen"></asp:Label>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <label class="form-label-bold">Video Actual:</label>
                            <div class="archivo-actual">
                                <i class="fas fa-video"></i>
                                <asp:Label ID="lbl_video_actual" runat="server" Text="No hay video"></asp:Label>
                            </div>
                        </div>
                    </div>

                    <!-- CONTENIDO MULTIMEDIA MEJORADO - EDITAR -->
                    <div class="content row">
                        <div class="col">
                            <label class="section-label">
                                <i class="fas fa-photo-video"></i> Actualizar Contenido Multimedia
                            </label>
                            
                            <!-- Opciones de multimedia -->
                            <div class="multimedia-selector">
                                <div class="multimedia-option-edit" data-tipo="ninguno">
                                    <div class="multimedia-icon">
                                        <i class="fas fa-ban"></i>
                                    </div>
                                    <div class="multimedia-label">Mantener actual</div>
                                </div>
                                
                                <div class="multimedia-option-edit" data-tipo="imagen">
                                    <div class="multimedia-icon">
                                        <i class="fas fa-image"></i>
                                    </div>
                                    <div class="multimedia-label">Cambiar imagen</div>
                                </div>
                                
                                <div class="multimedia-option-edit" data-tipo="video">
                                    <div class="multimedia-icon">
                                        <i class="fas fa-video"></i>
                                    </div>
                                    <div class="multimedia-label">Cambiar video</div>
                                </div>
                            </div>

                            <!-- Upload de Imagen -->
                            <div id="upload-imagen-edit" class="upload-section-edit" style="display: none;">
                                <div class="upload-container">
                                    <i class="fas fa-cloud-upload-alt"></i>
                                    <label class="upload-label">Nueva Imagen</label>
                                    <asp:FileUpload
                                        runat="server"
                                        ID="fud_imagen_edit"
                                        accept="image/*"
                                        CssClass="upload-input" />
                                    <small class="upload-hint">Dejar vacío para mantener actual</small>
                                </div>
                            </div>

                            <!-- Upload de Video -->
                            <div id="upload-video-edit" class="upload-section-edit" style="display: none;">
                                <div class="upload-container">
                                    <i class="fas fa-cloud-upload-alt"></i>
                                    <label class="upload-label">Nuevo Video</label>
                                    <asp:FileUpload
                                        runat="server"
                                        ID="fud_video_edit"
                                        accept="video/*"
                                        CssClass="upload-input" />
                                    <small class="upload-hint">Dejar vacío para mantener actual</small>
                                </div>
                            </div>
                        </div>
                    </div>

                    <!-- URL y Tiempo -->
                    <div class="content row">
                        <div class="pnl_input col-md-6">
                            <i class="fas fa-link"></i>
                            <label class="form-label-bold">URL</label>
                            <asp:TextBox
                                runat="server"
                                ID="txt_url_edit"
                                placeholder="https://ejemplo.com"></asp:TextBox>
                        </div>
                        <div class="pnl_input col-md-6">
                            <i class="fas fa-clock"></i>
                            <label class="form-label-bold">Tiempo visualización (seg)</label>
                            <asp:TextBox
                                runat="server"
                                ID="txt_tiempo_edit"
                                TextMode="Number"
                                min="1"
                                max="60"></asp:TextBox>
                        </div>
                    </div>

                    <!-- Fechas -->
                    <div class="content row">
                        <div class="pnl_input col-md-6">
                            <i class="fas fa-calendar-alt"></i>
                            <label class="form-label-bold">Fecha Inicio</label>
                            <asp:TextBox
                                runat="server"
                                ID="txt_fecha_inicio_edit"
                                TextMode="Date"></asp:TextBox>
                        </div>
                        <div class="pnl_input col-md-6">
                            <i class="fas fa-calendar-check"></i>
                            <label class="form-label-bold">Fecha Fin</label>
                            <asp:TextBox
                                runat="server"
                                ID="txt_fecha_fin_edit"
                                TextMode="Date"></asp:TextBox>
                        </div>
                    </div>

                    <!-- ROLES MEJORADOS -->
                    <div class="content row">
                        <div class="col">
                            <label class="section-label">
                                <i class="fas fa-users-cog"></i> Roles que pueden visualizar
                            </label>
                            
                            <div class="roles-grid">
                                <asp:CheckBoxList ID="cbl_roles_edit" runat="server" CssClass="roles-checkbox-list"></asp:CheckBoxList>
                            </div>
                        </div>
                    </div>

                    <!-- Estado -->
                    <div class="content row">
                        <div class="pnl_input col">
                            <i class="fas fa-toggle-on"></i>
                            <label class="form-label-bold">Estado</label>
                            <asp:DropDownList runat="server" ID="ddl_estado_edit" CssClass="form-control">
                                <asp:ListItem Value="1" Text="Activo"></asp:ListItem>
                                <asp:ListItem Value="0" Text="Inactivo"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>

                    <!-- Botón Actualizar -->
                    <div class="content row mt-20">
                        <div class="col text-right">
                            <asp:LinkButton
                                runat="server"
                                ID="btn_actualizar"
                                CssClass="button btn-actualizar-popup"
                                OnClick="btn_actualizar_Click">
                                <i class="fas fa-sync-alt"></i> Actualizar Popup
                            </asp:LinkButton>
                        </div>
                    </div>

                    <asp:Label ID="lbl_mensaje_edit" runat="server" CssClass="msg-error"></asp:Label>
                </section>
            </div>
        </div>
    </div>

    <!-- ========================================
         MODAL: CONFIRMAR ELIMINACIÓN
         ======================================== -->
    <div class="modal-i-gl modal-i-gl-hide animated fadeIn" id="modal_eliminar_popup">
        <div class="modal-i-gl-body modal-small">
            <div class="modal-i-gl-title">
                <h1 class="title modal-title-danger">
                    <i class="fas fa-exclamation-triangle"></i> Eliminar Popup
                </h1>
                <div class="modal-i-gl-cerrar">
                    <button type="button" class="btn-modal-close">
                        <i class="fas fa-times"></i>
                    </button>
                </div>
            </div>
            <div class="modal-i-gl-content">
                <section class="text-center-padded">
                    <asp:HiddenField ID="hf_id_popup_eliminar" runat="server" />
                    <p class="text-confirmation">
                        ¿Está seguro que desea eliminar este popup?
                    </p>
                    <p id="popup_titulo_eliminar" class="text-popup-title">
                        <!-- Se llenará por JavaScript -->
                    </p>
                    <div class="flex-center-gap">
                        <asp:LinkButton
                            runat="server"
                            ID="btn_eliminar"
                            CssClass="button btn-eliminar-confirm"
                            OnClick="btn_eliminar_Click">
                            <i class="fas fa-trash"></i> Eliminar
                        </asp:LinkButton>
                        <button 
                            type="button" 
                            class="btn btn-modal-close btn-cancelar">
                            <i class="fas fa-times"></i> Cancelar
                        </button>
                    </div>
                </section>
            </div>
        </div>
    </div>

    <!-- ========================================
         JAVASCRIPT: Manejo de Modales
         ======================================== -->
    <script defer>
        // Devuelve el radio actualmente seleccionado
        function getPopupSeleccionado() {
            return document.querySelector('input[name="rd_popup"]:checked');
        }

        // Función principal para enganchar eventos
        function ejecutarDatos() {
            // Botones principales
            const btnActualizarPopup = document.querySelector('#btn_modal_actualizar');
            const btnEliminarPopup = document.querySelector('#btn_modal_eliminar');

            // Modales
            const modalActualizarPopup = document.querySelector('#modal_actualizar_popup');
            const modalEliminarPopup = document.querySelector('#modal_eliminar_popup');

            // Controles del modal Actualizar
            const tituloEdit = document.querySelector('#MainContent_txt_titulo_edit');
            const descripcionEdit = document.querySelector('#MainContent_txt_descripcion_edit');
            const urlEdit = document.querySelector('#MainContent_txt_url_edit');
            const tiempoEdit = document.querySelector('#MainContent_txt_tiempo_edit');
            const fechaInicioEdit = document.querySelector('#MainContent_txt_fecha_inicio_edit');
            const fechaFinEdit = document.querySelector('#MainContent_txt_fecha_fin_edit');
            const estadoEdit = document.querySelector('#MainContent_ddl_estado_edit');
            const imagenActualLabel = document.querySelector('#MainContent_lbl_imagen_actual');
            const videoActualLabel = document.querySelector('#MainContent_lbl_video_actual');
            const hfIdPopup = document.querySelector('#MainContent_hf_id_popup');
            const hfImagenActual = document.querySelector('#MainContent_hf_imagen_actual');
            const hfVideoActual = document.querySelector('#MainContent_hf_video_actual');
            const hfRolesActuales = document.querySelector('#MainContent_hf_roles_actuales');

            // ================================
            // BOTÓN: ACTUALIZAR POPUP
            // ================================
            if (btnActualizarPopup && modalActualizarPopup && hfIdPopup) {
                btnActualizarPopup.onclick = function (e) {
                    e.preventDefault();

                    const popupSeleccionado = getPopupSeleccionado();
                    if (!popupSeleccionado) {
                        alert('Por favor, selecciona un popup de la tabla');
                        return;
                    }

                    // Obtener datos de los atributos data-
                    hfIdPopup.value = popupSeleccionado.value;
                    tituloEdit.value = popupSeleccionado.getAttribute('data-titulo') || '';
                    descripcionEdit.value = popupSeleccionado.getAttribute('data-descripcion') || '';
                    urlEdit.value = popupSeleccionado.getAttribute('data-url') || '';
                    tiempoEdit.value = popupSeleccionado.getAttribute('data-tiempo') || '5';
                    estadoEdit.value = popupSeleccionado.getAttribute('data-estado') || '1';

                    const imagenNombre = popupSeleccionado.getAttribute('data-imagen') || 'No hay imagen';
                    imagenActualLabel.innerText = imagenNombre;
                    hfImagenActual.value = imagenNombre;

                    const videoNombre = popupSeleccionado.getAttribute('data-video') || 'No hay video';
                    videoActualLabel.innerText = videoNombre;
                    hfVideoActual.value = videoNombre;

                    // Fechas
                    fechaInicioEdit.value = popupSeleccionado.getAttribute('data-fecha-inicio') || '';
                    fechaFinEdit.value = popupSeleccionado.getAttribute('data-fecha-fin') || '';

                    // Roles
                    const rolesIds = popupSeleccionado.getAttribute('data-roles') || '';
                    hfRolesActuales.value = rolesIds;

                    // Marcar checkboxes de roles
                    const rolesArray = rolesIds.split(',').filter(r => r);
                    const checkboxes = document.querySelectorAll('#MainContent_cbl_roles_edit input[type="checkbox"]');
                    checkboxes.forEach(cb => {
                        cb.checked = rolesArray.includes(cb.value);
                    });

                    // Mostrar modal
                    modalActualizarPopup.classList.add('modal-i-gl-show');
                    modalActualizarPopup.classList.remove('modal-i-gl-hide');
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

                    // Obtener título del atributo data-
                    const titulo = popupSeleccionado.getAttribute('data-titulo') || '';

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
        }

        // Ejecutar al cargar la página
        window.addEventListener('load', function () {
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