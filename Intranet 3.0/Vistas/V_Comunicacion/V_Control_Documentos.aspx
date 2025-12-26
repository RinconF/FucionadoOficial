<%@ Page ValidateRequest="false" Title="Control de Documentación Corporativa" Language="C#"
    MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="V_Control_Documentos.aspx.cs"
    Inherits="Intranet_3._0.Vistas.V_Comunicacion.V_Control_Documentos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="scripts_css" runat="server">
    <link rel="Stylesheet" href="/Styles/css/Documentos/Control_Documentos.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="scripts_js" runat="server">
    <script>
        $(document).ready(function () {
            const extensionesPermitidas = ["pdf", "doc", "docx", "xls", "xlsx", "ppt", "pptx"];
            const limiteArchivo = 10 * 1024 * 1024; // 10 MB

            $('body').on('change', 'input[type="file"]', function () {
                const inputFile = $(this)[0];
                if (!inputFile.files || inputFile.files.length === 0) {
                    return;
                }

                const archivo = inputFile.files[0];
                const ext = archivo.name.split('.').pop().toLowerCase();

                if (!extensionesPermitidas.includes(ext)) {
                    alert("Extensión no permitida: " + ext + ". Solo se permiten: PDF, DOC, DOCX, XLS, XLSX, PPT, PPTX");
                    inputFile.value = '';
                    return;
                }

                if (archivo.size > limiteArchivo) {
                    alert("El archivo excede el tamaño máximo de 10MB");
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
                    <p><i class="fas fa-file-alt"></i>Tabla de Documentos</p>
                </div>
                <div class="filter">
                    <div class="box_menu_crear">
                        <button 
                            type="button" 
                            id="btn_modal_crear" 
                            class="btn-modal" 
                            data-id="modal_crear_documento">
                            <i class="fas fa-plus"></i>Nuevo Documento
                        </button>
                        <button 
                            type="button" 
                            ID="btn_modal_actualizar" 
                            class="btn-actu-grupo" 
                            data-id="modal_actualizar_documento">
                            <i class="fas fa-cog"></i>Actualizar Documento
                        </button>
                        <button 
                            type="button" 
                            id="btn_modal_eliminar" 
                            class="btn-modal btn-eliminar-documento">
                            <i class="fas fa-trash"></i>Eliminar Documento
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
                    <asp:Literal ID="lit_tabla_documentos" runat="server"></asp:Literal>
                </div>
            </section>
        </ContentTemplate>
    </asp:UpdatePanel>

    <!-- ========================================
         MODAL: CREAR NUEVO DOCUMENTO
         ======================================== -->
    <div class="modal-i-gl modal-i-gl-hide animated fadeIn" id="modal_crear_documento">
        <div class="modal-i-gl-body">
            <div class="modal-i-gl-title">
                <h1 class="title">Crear Nuevo Documento</h1>
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
                                placeholder="Título del documento"></asp:TextBox>
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
                                Rows="3"
                                MaxLength="200"
                                placeholder="Descripción del documento"></asp:TextBox>
                        </div>
                    </div>

                    <!-- Archivo -->
                    <div class="content row">
                        <div class="pnl_input col">
                            <label><i class="fas fa-file-upload"></i> Archivo</label>
                            <asp:FileUpload 
                                ID="fud_archivo" 
                                runat="server" 
                                CssClass="form-control" 
                                accept=".pdf,.doc,.docx,.xls,.xlsx,.ppt,.pptx" />
                            <small class="form-help-text">Formatos: PDF, DOC, DOCX, XLS, XLSX, PPT, PPTX - Máx: 10MB</small>
                        </div>
                    </div>

                    <!-- URL Opcional -->
                    <div class="content row">
                        <div class="pnl_input col">
                            <label><i class="fas fa-link"></i> URL (opcional)</label>
                            <asp:TextBox
                                runat="server"
                                ID="txt_url"
                                placeholder="https://ejemplo.com"></asp:TextBox>
                            <small class="form-help-text">URL externa relacionada con el documento</small>
                        </div>
                    </div>

                    <!-- Botón Crear -->
                    <div class="content row mt-20">
                        <div class="col text-right">
                            <asp:LinkButton
                                runat="server"
                                ID="btn_guardar"
                                CssClass="button btn-guardar-documento"
                                OnClick="btn_guardar_Click">
                                <i class="fas fa-save"></i> Guardar Documento
                            </asp:LinkButton>
                        </div>
                    </div>

                    <asp:Label ID="lbl_mensaje" runat="server" CssClass="msg-error"></asp:Label>
                </section>
            </div>
        </div>
    </div>

    <!-- ========================================
         MODAL: ACTUALIZAR DOCUMENTO
         ======================================== -->
    <div class="modal-i-gl modal-i-gl-hide animated fadeIn" id="modal_actualizar_documento">
        <div class="modal-i-gl-body">
            <div class="modal-i-gl-title">
                <h1 class="title">Actualizar Documento</h1>
                <div class="modal-i-gl-cerrar">
                    <button type="button" class="btn-modal-close">
                        <i class="fas fa-times"></i>
                    </button>
                </div>
            </div>
            <div class="modal-i-gl-content">
                <section class="box_content_crear_vista">
                    <!-- ID oculto del documento a actualizar -->
                    <asp:HiddenField ID="hf_id_documento" runat="server" />
                    <asp:HiddenField ID="hf_archivo_actual" runat="server" />

                    <!-- Título -->
                    <div class="content row">
                        <div class="pnl_input col">
                            <i class="far fa-keyboard"></i>
                            <asp:TextBox
                                runat="server"
                                ID="txt_titulo_edit"
                                MaxLength="150"
                                placeholder="Título del documento"></asp:TextBox>
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
                                Rows="3"
                                MaxLength="200"
                                placeholder="Descripción del documento"></asp:TextBox>
                        </div>
                    </div>

                    <!-- Archivo Actual -->
                    <div class="content row">
                        <div class="col">
                            <label class="form-label-bold">
                                <i class="fas fa-file"></i> Archivo Actual
                            </label>
                            <div class="archivo-actual">
                                <i class="fas fa-file-pdf"></i>
                                <asp:Label ID="lbl_archivo_actual" runat="server" Text="No hay archivo"></asp:Label>
                            </div>
                        </div>
                    </div>

                    <!-- Nuevo Archivo -->
                    <div class="content row">
                        <div class="pnl_input col">
                            <label><i class="fas fa-file-upload"></i> Nuevo Archivo (opcional)</label>
                            <asp:FileUpload 
                                ID="fud_archivo_edit" 
                                runat="server" 
                                CssClass="form-control"
                                accept=".pdf,.doc,.docx,.xls,.xlsx,.ppt,.pptx" />
                            <small class="form-help-text">Dejar vacío para mantener el archivo actual</small>
                        </div>
                    </div>

                    <!-- URL -->
                    <div class="content row">
                        <div class="pnl_input col">
                            <label><i class="fas fa-link"></i> URL</label>
                            <asp:TextBox
                                runat="server"
                                ID="txt_url_edit"
                                placeholder="https://ejemplo.com"></asp:TextBox>
                        </div>
                    </div>

                    <!-- Estado -->
                    <div class="content row">
                        <div class="pnl_input col">
                            <label class="form-label-bold">
                                <i class="fas fa-toggle-on"></i> Estado
                            </label>
                            <asp:DropDownList ID="ddl_estado_edit" runat="server" CssClass="form-control">
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
                                CssClass="button btn-actualizar-documento"
                                OnClick="btn_actualizar_Click">
                                <i class="fas fa-sync-alt"></i> Actualizar Documento
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
    <div class="modal-i-gl modal-i-gl-hide animated fadeIn" id="modal_eliminar_documento">
        <div class="modal-i-gl-body modal-small">
            <div class="modal-i-gl-title">
                <h1 class="title modal-title-danger">
                    <i class="fas fa-exclamation-triangle"></i> Eliminar Documento
                </h1>
                <div class="modal-i-gl-cerrar">
                    <button type="button" class="btn-modal-close">
                        <i class="fas fa-times"></i>
                    </button>
                </div>
            </div>
            <div class="modal-i-gl-content">
                <section class="text-center-padded">
                    <asp:HiddenField ID="hf_id_documento_eliminar" runat="server" />
                    <p class="text-confirmation">
                        ¿Está seguro que desea eliminar este documento?
                    </p>
                    <p id="documento_titulo_eliminar" class="text-documento-title">
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
        function getDocumentoSeleccionado() {
            return document.querySelector('input[name="rd_documento"]:checked');
        }

        // Función principal para enganchar eventos
        function ejecutarDatos() {
            // Botones principales
            const btnActualizarDoc = document.querySelector('#btn_modal_actualizar');
            const btnEliminarDoc = document.querySelector('#btn_modal_eliminar');

            // Modales
            const modalActualizarDoc = document.querySelector('#modal_actualizar_documento');
            const modalEliminarDoc = document.querySelector('#modal_eliminar_documento');

            // Controles del modal Actualizar
            const tituloEdit = document.querySelector('#MainContent_txt_titulo_edit');
            const descripcionEdit = document.querySelector('#MainContent_txt_descripcion_edit');
            const urlEdit = document.querySelector('#MainContent_txt_url_edit');
            const estadoEdit = document.querySelector('#MainContent_ddl_estado_edit');
            const archivoActualLabel = document.querySelector('#MainContent_lbl_archivo_actual');
            const hfIdDocumento = document.querySelector('#MainContent_hf_id_documento');
            const hfArchivoActual = document.querySelector('#MainContent_hf_archivo_actual');

            // ================================
            // BOTÓN: ACTUALIZAR DOCUMENTO
            // ================================
            if (btnActualizarDoc && modalActualizarDoc && hfIdDocumento) {
                btnActualizarDoc.onclick = function (e) {
                    e.preventDefault();

                    const documentoSeleccionado = getDocumentoSeleccionado();
                    if (!documentoSeleccionado) {
                        alert('Por favor, selecciona un documento de la tabla');
                        return;
                    }

                    // Obtener datos de los atributos data-
                    hfIdDocumento.value = documentoSeleccionado.value;
                    tituloEdit.value = documentoSeleccionado.getAttribute('data-titulo') || '';
                    descripcionEdit.value = documentoSeleccionado.getAttribute('data-descripcion') || '';
                    urlEdit.value = documentoSeleccionado.getAttribute('data-url') || '';
                    estadoEdit.value = documentoSeleccionado.getAttribute('data-estado') || '1';

                    const archivoNombre = documentoSeleccionado.getAttribute('data-archivo') || 'Sin archivo';
                    archivoActualLabel.innerText = archivoNombre;
                    hfArchivoActual.value = archivoNombre;

                    // Mostrar modal
                    modalActualizarDoc.classList.add('modal-i-gl-show');
                    modalActualizarDoc.classList.remove('modal-i-gl-hide');
                };
            }

            // ================================
            // BOTÓN: ELIMINAR DOCUMENTO
            // ================================
            if (btnEliminarDoc && modalEliminarDoc) {
                btnEliminarDoc.onclick = function (e) {
                    e.preventDefault();

                    const documentoSeleccionado = getDocumentoSeleccionado();
                    if (!documentoSeleccionado) {
                        alert('Por favor, selecciona un documento de la tabla');
                        return;
                    }

                    // Guardar ID en HiddenField
                    const hfEliminar = document.querySelector('#MainContent_hf_id_documento_eliminar');
                    if (hfEliminar) {
                        hfEliminar.value = documentoSeleccionado.value;
                    }

                    // Obtener título del atributo data-
                    const titulo = documentoSeleccionado.getAttribute('data-titulo') || '';

                    // Mostrar en modal de confirmación
                    const lblTituloEliminar = document.getElementById('documento_titulo_eliminar');
                    if (lblTituloEliminar) {
                        lblTituloEliminar.innerText = titulo;
                    }

                    // Mostrar modal
                    modalEliminarDoc.classList.add('modal-i-gl-show');
                    modalEliminarDoc.classList.remove('modal-i-gl-hide');
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