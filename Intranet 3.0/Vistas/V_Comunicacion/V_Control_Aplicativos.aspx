<%@ Page ValidateRequest="false" Title="Control de Aplicativos" Language="C#"
    MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="V_Control_Aplicativos.aspx.cs"
    Inherits="Intranet_3._0.Vistas.V_Comunicacion.V_Control_Aplicativos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="scripts_css" runat="server">
    <link rel="Stylesheet" href="/Styles/css/aplicativos/Control_Aplicativos.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="scripts_js" runat="server">
    <script>
        $(document).ready(function () {
            const extensionesImagen = ["jpg", "jpeg", "png", "gif", "jfif"];
            const limiteImagen = 5 * 1024 * 1024; // 5 MB

            $('body').on('change', 'input[type="file"]', function () {
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
                    alert("La imagen excede el tamaño máximo de 5MB");
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
                    <p><i class="fas fa-desktop"></i>Tabla de Aplicativos</p>
                </div>
                <div class="filter">
                    <div class="box_menu_crear">
                        <button 
                            type="button" 
                            id="btn_modal_crear" 
                            class="btn-modal" 
                            data-id="modal_crear_aplicativo">
                            <i class="fas fa-plus"></i>Nuevo Aplicativo
                        </button>
                        <button 
                            type="button" 
                            ID="btn_modal_actualizar" 
                            class="btn-actu-grupo" 
                            data-id="modal_actualizar_aplicativo">
                            <i class="fas fa-cog"></i>Actualizar Aplicativo
                        </button>
                        <button 
                            type="button" 
                            id="btn_modal_eliminar" 
                            class="btn-modal btn-eliminar-aplicativo">
                            <i class="fas fa-trash"></i>Eliminar Aplicativo
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
                    <asp:Literal ID="lit_tabla_aplicativos" runat="server"></asp:Literal>
                </div>
            </section>
        </ContentTemplate>
    </asp:UpdatePanel>

    <!-- ========================================
         MODAL: CREAR NUEVO APLICATIVO
         ======================================== -->
    <div class="modal-i-gl modal-i-gl-hide animated fadeIn" id="modal_crear_aplicativo">
        <div class="modal-i-gl-body">
            <div class="modal-i-gl-title">
                <h1 class="title">Crear Nuevo Aplicativo</h1>
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
                                placeholder="Título del aplicativo"></asp:TextBox>
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
                                placeholder="Descripción del aplicativo"></asp:TextBox>
                        </div>
                    </div>

                    <!-- URL -->
                    <div class="content row">
                        <div class="pnl_input col">
                            <i class="fas fa-link"></i>
                            <asp:TextBox
                                runat="server"
                                ID="txt_url"
                                MaxLength="500"
                                placeholder="URL del aplicativo (https://...)"></asp:TextBox>
                        </div>
                    </div>

                    <!-- Imagen -->
                    <div class="content row">
                        <div class="pnl_input col">
                            <i class="fas fa-image"></i>
                            <label class="form-label-bold">Cargar Imagen</label>
                            <asp:FileUpload
                                runat="server"
                                ID="fud_imagen"
                                accept="image/*"
                                CssClass="form-control" />
                            <small class="form-help-text">Máx: 5MB. Formatos: JPG, JPEG, PNG, GIF</small>
                        </div>
                    </div>

                    <!-- Sección y Orden -->
                    <div class="content row">
                        <div class="pnl_input col-md-6">
                            <i class="fas fa-folder"></i>
                            <label class="form-label-bold">Sección</label>
                            <asp:DropDownList runat="server" ID="ddl_seccion" CssClass="form-control">
                                <asp:ListItem Value="" Text="-- Seleccione --"></asp:ListItem>
                                <asp:ListItem Value="EMPRESARIALES" Text="Aplicativos Empresariales"></asp:ListItem>
                                <asp:ListItem Value="CONSULTA" Text="Aplicativos de Consulta"></asp:ListItem>
                                <asp:ListItem Value="SOPORTE" Text="Aplicativos de Soporte"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="pnl_input col-md-6">
                            <i class="fas fa-sort-numeric-down"></i>
                            <label class="form-label-bold">Orden (opcional)</label>
                            <asp:TextBox
                                runat="server"
                                ID="txt_orden"
                                TextMode="Number"
                                placeholder="Orden de visualización"></asp:TextBox>
                        </div>
                    </div>

                    <!-- Botón Guardar -->
                    <div class="content row mt-20">
                        <div class="col text-right">
                            <asp:LinkButton
                                runat="server"
                                ID="btn_guardar"
                                CssClass="button btn-guardar-aplicativo"
                                OnClick="btn_guardar_Click">
                                <i class="fas fa-save"></i> Guardar Aplicativo
                            </asp:LinkButton>
                        </div>
                    </div>

                    <asp:Label ID="lbl_mensaje" runat="server" CssClass="msg-error"></asp:Label>
                </section>
            </div>
        </div>
    </div>

    <!-- ========================================
         MODAL: ACTUALIZAR APLICATIVO
         ======================================== -->
    <div class="modal-i-gl modal-i-gl-hide animated fadeIn" id="modal_actualizar_aplicativo">
        <div class="modal-i-gl-body">
            <div class="modal-i-gl-title">
                <h1 class="title">Actualizar Aplicativo</h1>
                <div class="modal-i-gl-cerrar">
                    <button type="button" class="btn-modal-close">
                        <i class="fas fa-times"></i>
                    </button>
                </div>
            </div>
            <div class="modal-i-gl-content">
                <section class="box_content_crear_vista">
                    <asp:HiddenField ID="hf_id_aplicativo" runat="server" />
                    <asp:HiddenField ID="hf_imagen_actual" runat="server" />

                    <!-- Título -->
                    <div class="content row">
                        <div class="pnl_input col">
                            <i class="far fa-keyboard"></i>
                            <asp:TextBox
                                runat="server"
                                ID="txt_titulo_edit"
                                MaxLength="200"
                                placeholder="Título del aplicativo"></asp:TextBox>
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
                                placeholder="Descripción del aplicativo"></asp:TextBox>
                        </div>
                    </div>

                    <!-- URL -->
                    <div class="content row">
                        <div class="pnl_input col">
                            <i class="fas fa-link"></i>
                            <asp:TextBox
                                runat="server"
                                ID="txt_url_edit"
                                MaxLength="500"
                                placeholder="URL del aplicativo"></asp:TextBox>
                        </div>
                    </div>

                    <!-- Imagen actual -->
                    <div class="content row">
                        <div class="col">
                            <label class="form-label-bold">Imagen Actual:</label>
                            <div class="imagen-actual">
                                <i class="fas fa-image"></i>
                                <asp:Label ID="lbl_imagen_actual" runat="server" Text="No hay imagen"></asp:Label>
                            </div>
                        </div>
                    </div>

                    <!-- Nueva Imagen -->
                    <div class="content row">
                        <div class="pnl_input col">
                            <i class="fas fa-upload"></i>
                            <label class="form-label-bold">Cambiar Imagen (opcional)</label>
                            <asp:FileUpload
                                runat="server"
                                ID="fud_imagen_edit"
                                accept="image/*"
                                CssClass="form-control" />
                            <small class="form-help-text">Si no selecciona imagen, se mantendrá la actual</small>
                        </div>
                    </div>

                    <!-- Sección y Orden -->
                    <div class="content row">
                        <div class="pnl_input col-md-6">
                            <i class="fas fa-folder"></i>
                            <label class="form-label-bold">Sección</label>
                            <asp:DropDownList runat="server" ID="ddl_seccion_edit" CssClass="form-control">
                                <asp:ListItem Value="" Text="-- Seleccione --"></asp:ListItem>
                                <asp:ListItem Value="EMPRESARIALES" Text="Aplicativos Empresariales"></asp:ListItem>
                                <asp:ListItem Value="CONSULTA" Text="Aplicativos de Consulta"></asp:ListItem>
                                <asp:ListItem Value="SOPORTE" Text="Aplicativos de Soporte"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="pnl_input col-md-6">
                            <i class="fas fa-sort-numeric-down"></i>
                            <label class="form-label-bold">Orden (opcional)</label>
                            <asp:TextBox
                                runat="server"
                                ID="txt_orden_edit"
                                TextMode="Number"
                                placeholder="Orden de visualización"></asp:TextBox>
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
                                CssClass="button btn-actualizar-aplicativo"
                                OnClick="btn_actualizar_Click">
                                <i class="fas fa-sync-alt"></i> Actualizar Aplicativo
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
    <div class="modal-i-gl modal-i-gl-hide animated fadeIn" id="modal_eliminar_aplicativo">
        <div class="modal-i-gl-body modal-small">
            <div class="modal-i-gl-title">
                <h1 class="title modal-title-danger">
                    <i class="fas fa-exclamation-triangle"></i> Eliminar Aplicativo
                </h1>
                <div class="modal-i-gl-cerrar">
                    <button type="button" class="btn-modal-close">
                        <i class="fas fa-times"></i>
                    </button>
                </div>
            </div>
            <div class="modal-i-gl-content">
                <section class="text-center-padded">
                    <asp:HiddenField ID="hf_id_aplicativo_eliminar" runat="server" />
                    <p class="text-confirmation">
                        ¿Está seguro que desea eliminar este aplicativo?
                    </p>
                    <p id="aplicativo_titulo_eliminar" class="text-aplicativo-title">
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
        function getAplicativoSeleccionado() {
            return document.querySelector('input[name="rd_aplicativo"]:checked');
        }

        // Función principal para enganchar eventos
        function ejecutarDatos() {
            // Botones principales
            const btnActualizarApp = document.querySelector('#btn_modal_actualizar');
            const btnEliminarApp = document.querySelector('#btn_modal_eliminar');

            // Modales
            const modalActualizarApp = document.querySelector('#modal_actualizar_aplicativo');
            const modalEliminarApp = document.querySelector('#modal_eliminar_aplicativo');

            // Controles del modal Actualizar
            const tituloEdit = document.querySelector('#MainContent_txt_titulo_edit');
            const descripcionEdit = document.querySelector('#MainContent_txt_descripcion_edit');
            const urlEdit = document.querySelector('#MainContent_txt_url_edit');
            const seccionEdit = document.querySelector('#MainContent_ddl_seccion_edit');
            const ordenEdit = document.querySelector('#MainContent_txt_orden_edit');
            const estadoEdit = document.querySelector('#MainContent_ddl_estado_edit');
            const imagenActualLabel = document.querySelector('#MainContent_lbl_imagen_actual');
            const hfIdAplicativo = document.querySelector('#MainContent_hf_id_aplicativo');
            const hfImagenActual = document.querySelector('#MainContent_hf_imagen_actual');

            // ================================
            // BOTÓN: ACTUALIZAR APLICATIVO
            // ================================
            if (btnActualizarApp && modalActualizarApp && hfIdAplicativo) {
                btnActualizarApp.onclick = function (e) {
                    e.preventDefault();

                    const aplicativoSeleccionado = getAplicativoSeleccionado();
                    if (!aplicativoSeleccionado) {
                        alert('Por favor, selecciona un aplicativo de la tabla');
                        return;
                    }

                    // Obtener datos de los atributos data-
                    hfIdAplicativo.value = aplicativoSeleccionado.value;
                    tituloEdit.value = aplicativoSeleccionado.getAttribute('data-titulo') || '';
                    descripcionEdit.value = aplicativoSeleccionado.getAttribute('data-descripcion') || '';
                    urlEdit.value = aplicativoSeleccionado.getAttribute('data-url') || '';
                    seccionEdit.value = aplicativoSeleccionado.getAttribute('data-seccion') || '';
                    ordenEdit.value = aplicativoSeleccionado.getAttribute('data-orden') || '';
                    estadoEdit.value = aplicativoSeleccionado.getAttribute('data-estado') || '1';

                    const imagenNombre = aplicativoSeleccionado.getAttribute('data-imagen') || 'Sin imagen';
                    imagenActualLabel.innerText = imagenNombre;
                    hfImagenActual.value = imagenNombre;

                    // Mostrar modal
                    modalActualizarApp.classList.add('modal-i-gl-show');
                    modalActualizarApp.classList.remove('modal-i-gl-hide');
                };
            }

            // ================================
            // BOTÓN: ELIMINAR APLICATIVO
            // ================================
            if (btnEliminarApp && modalEliminarApp) {
                btnEliminarApp.onclick = function (e) {
                    e.preventDefault();

                    const aplicativoSeleccionado = getAplicativoSeleccionado();
                    if (!aplicativoSeleccionado) {
                        alert('Por favor, selecciona un aplicativo de la tabla');
                        return;
                    }

                    // Guardar ID en HiddenField
                    const hfEliminar = document.querySelector('#MainContent_hf_id_aplicativo_eliminar');
                    if (hfEliminar) {
                        hfEliminar.value = aplicativoSeleccionado.value;
                    }

                    // Obtener título del atributo data-
                    const titulo = aplicativoSeleccionado.getAttribute('data-titulo') || '';

                    // Mostrar en modal de confirmación
                    const lblTituloEliminar = document.getElementById('aplicativo_titulo_eliminar');
                    if (lblTituloEliminar) {
                        lblTituloEliminar.innerText = titulo;
                    }

                    // Mostrar modal
                    modalEliminarApp.classList.add('modal-i-gl-show');
                    modalEliminarApp.classList.remove('modal-i-gl-hide');
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