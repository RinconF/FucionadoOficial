<%@ Page ValidateRequest="false" Title="Control de Tutoriales" Language="C#"
    MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="V_Control_Tutoriales.aspx.cs"
    Inherits="Intranet_3._0.Vistas.V_Comunicacion.V_Control_Tutoriales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="scripts_css" runat="server">
    <link rel="Stylesheet" href="/Styles/css/tutoriales/Control_Tutoriales.css" />
    <style>
        .roles-container {
            max-height: 200px;
            overflow-y: auto;
            border: 1px solid #ddd;
            padding: 10px;
            border-radius: 4px;
            background: #f9f9f9;
        }
        .role-checkbox {
            display: block;
            margin: 5px 0;
            padding: 5px;
        }
        .role-checkbox input[type="checkbox"] {
            margin-right: 8px;
        }
        .roles-badge {
            display: inline-block;
            background: #3498db;
            color: white;
            padding: 2px 8px;
            border-radius: 3px;
            font-size: 11px;
            margin: 2px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="scripts_js" runat="server">
    <script>
        $(document).ready(function () {
            const extensionesVideo = ["mp4", "webm", "ogg", "avi", "mov", "wmv", "flv", "mkv"];
            const limiteVideo = 50 * 1024 * 1024; // 50 MB

            $('body').on('change', 'input[type="file"]', function () {
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
        });
    </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel runat="server" ID="PanelUpdate">
        <ContentTemplate>
            <section class="pnl_table">
                <div class="pnl_tag">
                    <p><i class="fas fa-graduation-cap"></i>Tabla de Tutoriales</p>
                </div>
                <div class="filter">
                    <div class="box_menu_crear">
                        <button 
                            type="button" 
                            id="btn_modal_crear" 
                            class="btn-modal" 
                            data-id="modal_crear_tutorial">
                            <i class="fas fa-plus"></i>Nuevo Tutorial
                        </button>
                        <button 
                            type="button" 
                            ID="btn_modal_actualizar" 
                            class="btn-actu-grupo" 
                            data-id="modal_actualizar_tutorial">
                            <i class="fas fa-cog"></i>Actualizar Tutorial
                        </button>
                        <button 
                            type="button" 
                            id="btn_modal_eliminar" 
                            class="btn-modal btn-eliminar-tutorial">
                            <i class="fas fa-trash"></i>Eliminar Tutorial
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
                    <asp:Literal ID="lit_tabla_tutoriales" runat="server"></asp:Literal>
                </div>
            </section>
        </ContentTemplate>
    </asp:UpdatePanel>

    <!-- ========================================
         MODAL: CREAR NUEVO TUTORIAL
         ======================================== -->
    <div class="modal-i-gl modal-i-gl-hide animated fadeIn" id="modal_crear_tutorial">
        <div class="modal-i-gl-body">
            <div class="modal-i-gl-title">
                <h1 class="title">Crear Nuevo Tutorial</h1>
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
                                MaxLength="150"
                                placeholder="Título del tutorial"></asp:TextBox>
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
                                MaxLength="300"
                                placeholder="Descripción del tutorial"></asp:TextBox>
                        </div>
                    </div>

                    <!-- Video -->
                    <div class="content row">
                        <div class="pnl_input col">
                            <i class="fas fa-video"></i>
                            <label class="form-label-bold">Cargar Video</label>
                            <asp:FileUpload
                                runat="server"
                                ID="fud_video"
                                accept="video/*"
                                CssClass="form-control" />
                            <small class="form-help-text">Máx: 50MB. Formato: MP4</small>
                        </div>
                    </div>

                    <!-- Sección -->
                    <div class="content row">
                        <div class="pnl_input col">
                            <i class="fas fa-folder"></i>
                            <label class="form-label-bold">Sección</label>
                            <asp:DropDownList runat="server" ID="ddl_seccion" CssClass="form-control">
                                <asp:ListItem Value="" Text="-- Seleccione --"></asp:ListItem>
                                <asp:ListItem Value="General" Text="General"></asp:ListItem>
                                <asp:ListItem Value="Ventas" Text="Ventas"></asp:ListItem>
                                <asp:ListItem Value="Marketing" Text="Marketing"></asp:ListItem>
                                <asp:ListItem Value="Recursos Humanos" Text="Recursos Humanos"></asp:ListItem>
                                <asp:ListItem Value="Finanzas" Text="Finanzas"></asp:ListItem>
                                <asp:ListItem Value="TI" Text="Tecnología de Información"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>

                    <!-- ================================== -->
                    <!-- NUEVO: SEGMENTACIÓN POR ROL -->
                    <!-- ================================== -->
                    <div class="content row">
                        <div class="pnl_input col">
                            <i class="fas fa-users-cog"></i>
                            <label class="form-label-bold">
                                Roles con acceso <span style="color: red;">*</span>
                            </label>
                            <div class="roles-container">
                                <asp:CheckBoxList 
                                    ID="cbl_roles" 
                                    runat="server" 
                                    CssClass="role-checkbox-list">
                                </asp:CheckBoxList>
                            </div>
                            <small class="form-help-text">Seleccione los roles que podrán ver este tutorial</small>
                        </div>
                    </div>

                    <!-- Botón Guardar -->
                    <div class="content row mt-20">
                        <div class="col text-right">
                            <asp:LinkButton
                                runat="server"
                                ID="btn_guardar"
                                CssClass="button btn-guardar-tutorial"
                                OnClick="btn_guardar_Click">
                                <i class="fas fa-save"></i> Guardar Tutorial
                            </asp:LinkButton>
                        </div>
                    </div>

                    <asp:Label ID="lbl_mensaje" runat="server" CssClass="msg-error"></asp:Label>
                </section>
            </div>
        </div>
    </div>

    <!-- ========================================
         MODAL: ACTUALIZAR TUTORIAL
         ======================================== -->
    <div class="modal-i-gl modal-i-gl-hide animated fadeIn" id="modal_actualizar_tutorial">
        <div class="modal-i-gl-body">
            <div class="modal-i-gl-title">
                <h1 class="title">Actualizar Tutorial</h1>
                <div class="modal-i-gl-cerrar">
                    <button type="button" class="btn-modal-close">
                        <i class="fas fa-times"></i>
                    </button>
                </div>
            </div>
            <div class="modal-i-gl-content">
                <section class="box_content_crear_vista">
                    <asp:HiddenField ID="hf_id_tutorial" runat="server" />
                    <asp:HiddenField ID="hf_video_actual" runat="server" />

                    <!-- Título -->
                    <div class="content row">
                        <div class="pnl_input col">
                            <i class="far fa-keyboard"></i>
                            <asp:TextBox
                                runat="server"
                                ID="txt_titulo_edit"
                                MaxLength="150"
                                placeholder="Título del tutorial"></asp:TextBox>
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
                                MaxLength="300"
                                placeholder="Descripción del tutorial"></asp:TextBox>
                        </div>
                    </div>

                    <!-- Video actual -->
                    <div class="content row">
                        <div class="col">
                            <label class="form-label-bold">Video Actual:</label>
                            <div class="video-actual">
                                <i class="fas fa-video"></i>
                                <asp:Label ID="lbl_video_actual" runat="server" Text="No hay video"></asp:Label>
                            </div>
                        </div>
                    </div>

                    <!-- Nuevo Video -->
                    <div class="content row">
                        <div class="pnl_input col">
                            <i class="fas fa-upload"></i>
                            <label class="form-label-bold">Cambiar Video (opcional)</label>
                            <asp:FileUpload
                                runat="server"
                                ID="fud_video_edit"
                                accept="video/*"
                                CssClass="form-control" />
                            <small class="form-help-text">Si no selecciona video, se mantendrá el actual</small>
                        </div>
                    </div>

                    <!-- Sección -->
                    <div class="content row">
                        <div class="pnl_input col">
                            <i class="fas fa-folder"></i>
                            <label class="form-label-bold">Sección</label>
                            <asp:DropDownList runat="server" ID="ddl_seccion_edit" CssClass="form-control">
                                <asp:ListItem Value="" Text="-- Seleccione --"></asp:ListItem>
                                <asp:ListItem Value="General" Text="General"></asp:ListItem>
                                <asp:ListItem Value="Ventas" Text="Ventas"></asp:ListItem>
                                <asp:ListItem Value="Marketing" Text="Marketing"></asp:ListItem>
                                <asp:ListItem Value="Recursos Humanos" Text="Recursos Humanos"></asp:ListItem>
                                <asp:ListItem Value="Finanzas" Text="Finanzas"></asp:ListItem>
                                <asp:ListItem Value="TI" Text="Tecnología de Información"></asp:ListItem>
                            </asp:DropDownList>
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

                    <!-- ================================== -->
                    <!-- NUEVO: SEGMENTACIÓN POR ROL (EDICIÓN) -->
                    <!-- ================================== -->
                    <div class="content row">
                        <div class="pnl_input col">
                            <i class="fas fa-users-cog"></i>
                            <label class="form-label-bold">
                                Roles con acceso <span style="color: red;">*</span>
                            </label>
                            <div class="roles-container">
                                <asp:CheckBoxList 
                                    ID="cbl_roles_edit" 
                                    runat="server" 
                                    CssClass="role-checkbox-list">
                                </asp:CheckBoxList>
                            </div>
                            <small class="form-help-text">Seleccione los roles que podrán ver este tutorial</small>
                        </div>
                    </div>

                    <!-- Botón Actualizar -->
                    <div class="content row mt-20">
                        <div class="col text-right">
                            <asp:LinkButton
                                runat="server"
                                ID="btn_actualizar"
                                CssClass="button btn-actualizar-tutorial"
                                OnClick="btn_actualizar_Click">
                                <i class="fas fa-sync-alt"></i> Actualizar Tutorial
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
    <div class="modal-i-gl modal-i-gl-hide animated fadeIn" id="modal_eliminar_tutorial">
        <div class="modal-i-gl-body modal-small">
            <div class="modal-i-gl-title">
                <h1 class="title modal-title-danger">
                    <i class="fas fa-exclamation-triangle"></i> Eliminar Tutorial
                </h1>
                <div class="modal-i-gl-cerrar">
                    <button type="button" class="btn-modal-close">
                        <i class="fas fa-times"></i>
                    </button>
                </div>
            </div>
            <div class="modal-i-gl-content">
                <section class="text-center-padded">
                    <asp:HiddenField ID="hf_id_tutorial_eliminar" runat="server" />
                    <p class="text-confirmation">
                        ¿Está seguro que desea eliminar este tutorial?
                    </p>
                    <p id="tutorial_titulo_eliminar" class="text-tutorial-title">
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
        function getTutorialSeleccionado() {
            return document.querySelector('input[name="rd_tutorial"]:checked');
        }

        // Función principal para enganchar eventos
        function ejecutarDatos() {
            // Botones principales
            const btnActualizarTut = document.querySelector('#btn_modal_actualizar');
            const btnEliminarTut = document.querySelector('#btn_modal_eliminar');

            // Modales
            const modalActualizarTut = document.querySelector('#modal_actualizar_tutorial');
            const modalEliminarTut = document.querySelector('#modal_eliminar_tutorial');

            // Controles del modal Actualizar
            const tituloEdit = document.querySelector('#MainContent_txt_titulo_edit');
            const descripcionEdit = document.querySelector('#MainContent_txt_descripcion_edit');
            const seccionEdit = document.querySelector('#MainContent_ddl_seccion_edit');
            const estadoEdit = document.querySelector('#MainContent_ddl_estado_edit');
            const videoActualLabel = document.querySelector('#MainContent_lbl_video_actual');
            const hfIdTutorial = document.querySelector('#MainContent_hf_id_tutorial');
            const hfVideoActual = document.querySelector('#MainContent_hf_video_actual');

            // ================================
            // BOTÓN: ACTUALIZAR TUTORIAL
            // ================================
            if (btnActualizarTut && modalActualizarTut && hfIdTutorial) {
                btnActualizarTut.onclick = function (e) {
                    e.preventDefault();

                    const tutorialSeleccionado = getTutorialSeleccionado();
                    if (!tutorialSeleccionado) {
                        alert('Por favor, selecciona un tutorial de la tabla');
                        return;
                    }

                    // Obtener datos de los atributos data-
                    hfIdTutorial.value = tutorialSeleccionado.value;
                    tituloEdit.value = tutorialSeleccionado.getAttribute('data-titulo') || '';
                    descripcionEdit.value = tutorialSeleccionado.getAttribute('data-descripcion') || '';
                    seccionEdit.value = tutorialSeleccionado.getAttribute('data-seccion') || '';
                    estadoEdit.value = tutorialSeleccionado.getAttribute('data-estado') || '1';

                    const videoNombre = tutorialSeleccionado.getAttribute('data-video') || 'Sin video';
                    videoActualLabel.innerText = videoNombre;
                    hfVideoActual.value = videoNombre;

                    // ================================
                    // NUEVO: Cargar roles asignados
                    // ================================
                    const rolesAsignados = tutorialSeleccionado.getAttribute('data-roles') || '';
                    const rolesArray = rolesAsignados.split(',').map(r => r.trim()).filter(r => r !== '');

                    // Desmarcar todos los checkboxes primero
                    document.querySelectorAll('#MainContent_cbl_roles_edit input[type="checkbox"]').forEach(cb => {
                        cb.checked = false;
                    });

                    // Marcar los roles asignados
                    rolesArray.forEach(rolId => {
                        const checkbox = document.querySelector(`#MainContent_cbl_roles_edit input[value="${rolId}"]`);
                        if (checkbox) {
                            checkbox.checked = true;
                        }
                    });

                    // Mostrar modal
                    modalActualizarTut.classList.add('modal-i-gl-show');
                    modalActualizarTut.classList.remove('modal-i-gl-hide');
                };
            }

            // ================================
            // BOTÓN: ELIMINAR TUTORIAL
            // ================================
            if (btnEliminarTut && modalEliminarTut) {
                btnEliminarTut.onclick = function (e) {
                    e.preventDefault();

                    const tutorialSeleccionado = getTutorialSeleccionado();
                    if (!tutorialSeleccionado) {
                        alert('Por favor, selecciona un tutorial de la tabla');
                        return;
                    }

                    // Guardar ID en HiddenField
                    const hfEliminar = document.querySelector('#MainContent_hf_id_tutorial_eliminar');
                    if (hfEliminar) {
                        hfEliminar.value = tutorialSeleccionado.value;
                    }

                    // Obtener título del atributo data-
                    const titulo = tutorialSeleccionado.getAttribute('data-titulo') || '';

                    // Mostrar en modal de confirmación
                    const lblTituloEliminar = document.getElementById('tutorial_titulo_eliminar');
                    if (lblTituloEliminar) {
                        lblTituloEliminar.innerText = titulo;
                    }

                    // Mostrar modal
                    modalEliminarTut.classList.add('modal-i-gl-show');
                    modalEliminarTut.classList.remove('modal-i-gl-hide');
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
