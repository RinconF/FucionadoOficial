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
            display: block;
            margin-bottom: 8px;
            cursor: pointer;
        }
        .roles-selector input[type="checkbox"] {
            margin-right: 8px;
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
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="scripts_js" runat="server">
    <script>
        $(document).ready(function () {
            // Validación de archivos
            $('body').on('change', 'input[type="file"]', function () {
                var ext = $(this).val().split('.').pop().toLowerCase();
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
                        <asp:TextBox 
                            runat="server" 
                            ID="txt_filter_grupo" 
                            placeholder="Búsqueda rápida" 
                            OnTextChanged="txt_filter_grupo_TextChanged" 
                            AutoPostBack="true">
                        </asp:TextBox>
                    </div>
                </div>
                <table runat="server" id="tbl_grupos" class="tbl_vistas_general"></table>
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
                                placeholder="Descripción del popup"
                                Style="width:94%">
                            </asp:TextBox>                                
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
                    </div>

                    <!-- Cargar Imagen -->
                    <div class="content row">
                        <div class="pnl_input col">
                            <label><i class="fas fa-images"></i> Cargar imagen</label>
                            <asp:FileUpload 
                                runat="server" 
                                ID="fud_Adjunto" 
                                accept="image/png, image/gif, image/jpeg, image/jfif" />
                            <small style="color: #7f8c8d;">Tamaño máximo: 3MB. Formatos: JPG, PNG, GIF</small>
                        </div>
                    </div>

                    <!-- Botón Crear -->
                    <asp:LinkButton
                        runat="server"
                        ID="lnk_crear_"
                        CssClass="btn btn-primary"
                        OnClick="Crear_nueva_publicacion">
                        <i class="fas fa-plus"></i> CREAR
                    </asp:LinkButton>
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

                    <!-- Selector de Roles (CheckBoxList) -->
                    <div class="roles-selector">
                        <label style="font-weight: bold; display: block; margin-bottom: 10px;">
                            <i class="fas fa-users"></i> Roles que pueden visualizar:
                        </label>
                        <asp:CheckBoxList ID="chkl_roles_pub" runat="server"></asp:CheckBoxList>
                    </div>

                    <!-- Cargar Imagen -->
                    <div class="content row">
                        <div class="pnl_input col">
                            <label><i class="fas fa-images"></i> Cambiar imagen (opcional)</label>
                            <asp:FileUpload 
                                runat="server" 
                                ID="fud_Adjunto_pub" 
                                accept="image/png, image/gif, image/jpeg, image/jfif" />
                            <small style="color: #7f8c8d;">
                                Deja vacío si no quieres cambiar la imagen actual
                            </small>
                        </div>
                    </div>

                    <!-- Botón Actualizar -->
                    <asp:LinkButton
                        runat="server"
                        ID="LinkButton1"
                        CssClass="btn btn-success"
                        OnClick="Actualizar_datos_publicacion">
                        <i class="fas fa-sync-alt"></i> ACTUALIZAR
                    </asp:LinkButton>
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
                    <p style="font-size: 16px; margin-bottom: 10px;">
                        ¿Está seguro que deseas eliminar este Popup?
                    </p>
                    <p id="popup_titulo_eliminar" style="font-weight: bold; color: #2c3e50; margin-bottom: 20px;">
                        <!-- Se llenará por JavaScript -->
                    </p>
                    <div style="display: flex; gap: 10px; justify-content: center;">
                        <asp:LinkButton
                            runat="server"
                            ID="btn_confirmar_eliminar"
                            CssClass="btn"
                            style="background-color: #e74c3c; color: white; padding: 12px 30px; border-radius: 25px;"
                            OnClick="Eliminar_Popup">
                            <i class="fas fa-check"></i> Sí, eliminar
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
        const txt_filter_grupo = document.querySelector('#MainContent_txt_filter_grupo');
        const lnk_crear_ = document.querySelector('#MainContent_lnk_crear_');
        const a = [...document.querySelectorAll('a')];

        // Función auxiliar para formatear fechas a yyyy-MM-dd
        function formatearFecha(fecha) {
            if (!fecha) return '';
            const d = new Date(fecha);
            const year = d.getFullYear();
            const month = String(d.getMonth() + 1).padStart(2, '0');
            const day = String(d.getDate()).padStart(2, '0');
            return `${year}-${month}-${day}`;
        }

        // ========================================
        // Función principal: ejecutarDatos()
        // ========================================
        const ejecutarDatos = () => {
            // Botones principales
            const btnActualizarPopup = document.querySelector('#btn_actualizar_popup');
            const btnEliminarPopup = document.querySelector('#btn_eliminar_popup');
            const btnEstadisticas = document.querySelector('#btn_estadisticas_popup');

            // Modales
            const modalActualizarPopup = document.querySelector('#modal_actualizar_popup');
            const modalEliminarPopup = document.querySelector('#modal_eliminar_popup');

            // Radio buttons de la tabla
            let tablas = [...document.querySelectorAll('input[name="rd_estado_vista"]')];

            // Campos del modal actualizar
            const tituloPublicacion = document.querySelector('#MainContent_txt_titulo_pub');
            const descripcionPublicacion = document.querySelector('#MainContent_txt_descripcion_pub');
            const urlPublicacion = document.querySelector('#MainContent_txt_url_pub');
            const tiempoPublicacion = document.querySelector('#MainContent_txt_tiempo_pub');
            const fechaInicioPublicacion = document.querySelector('#MainContent_txt_fecha_inicio_pub');
            const fechaFinPublicacion = document.querySelector('#MainContent_txt_fecha_fin_pub');
            const estadoPublicacion = document.querySelector('#MainContent_ddl_estado_pub');
            const rolesPublicacion = document.querySelector('#MainContent_chkl_roles_pub');

            // ========================================
            // BOTÓN: ACTUALIZAR POPUP
            // ========================================
            if (btnActualizarPopup) {
                btnActualizarPopup.addEventListener('click', async (e) => {
                    e.preventDefault();

                    const popupSeleccionado = tablas.find(t => t.checked);

                    if (!popupSeleccionado) {
                        alert('Por favor, selecciona un popup de la tabla');
                        return;
                    }

                    // Mostrar modal
                    modalActualizarPopup.classList.add('modal-i-gl-show');
                    modalActualizarPopup.classList.remove('modal-i-gl-hide');

                    try {
                        // Cargar datos del popup (Action 3)
                        const response = await fetch(
                            `WebService_V_Comunicacion.asmx/cargar_datos_modal_actualizar_Popup`,
                            {
                                method: 'POST',
                                headers: {
                                    'Content-Type': 'application/json',
                                },
                                body: JSON.stringify({
                                    Id_Popup: parseInt(popupSeleccionado.value)
                                }),
                            }
                        );

                        const datos = await response.json();
                        const item = datos.d[0]; // Primer elemento del array

                        // Rellenar campos
                        if (item) {
                            tituloPublicacion.value = item[1] || '';
                            descripcionPublicacion.value = item[2] || '';
                            urlPublicacion.value = item[5] || '';
                            tiempoPublicacion.value = item[6] || 5;
                            fechaInicioPublicacion.value = formatearFecha(item[7]);
                            fechaFinPublicacion.value = formatearFecha(item[8]);
                            estadoPublicacion.value = item[4] ? '1' : '0';

                            // Marcar roles seleccionados
                            const rolesIds = item[9] ? item[9].split(',') : [];
                            const checkboxes = rolesPublicacion.querySelectorAll('input[type="checkbox"]');
                            checkboxes.forEach(cb => {
                                cb.checked = rolesIds.includes(cb.value);
                            });
                        }
                    } catch (error) {
                        console.error('Error al cargar popup:', error);
                        alert('Error al cargar los datos del popup');
                    }
                });
            }

            // ========================================
            // BOTÓN: ELIMINAR POPUP
            // ========================================
            if (btnEliminarPopup) {
                btnEliminarPopup.addEventListener('click', (e) => {
                    e.preventDefault();

                    const popupSeleccionado = tablas.find(t => t.checked);

                    if (!popupSeleccionado) {
                        alert('Por favor, selecciona un popup de la tabla');
                        return;
                    }

                    // Obtener título del popup desde la tabla
                    const fila = popupSeleccionado.closest('tr');
                    const titulo = fila.cells[2].innerText; // Columna TITULO

                    // Mostrar en modal de confirmación
                    document.getElementById('popup_titulo_eliminar').innerText = titulo;

                    // Mostrar modal
                    modalEliminarPopup.classList.add('modal-i-gl-show');
                    modalEliminarPopup.classList.remove('modal-i-gl-hide');
                });
            }

            // ========================================
            // BOTÓN: ESTADÍSTICAS (Opcional - Futuro)
            // ========================================
            if (btnEstadisticas) {
                btnEstadisticas.addEventListener('click', async (e) => {
                    e.preventDefault();

                    const popupSeleccionado = tablas.find(t => t.checked);

                    if (!popupSeleccionado) {
                        alert('Por favor, selecciona un popup de la tabla');
                        return;
                    }

                    // Llamar Action 8 para obtener estadísticas
                    try {
                        const response = await fetch(
                            `WebService_V_Comunicacion.asmx/Obtener_Estadisticas_Popup`,
                            {
                                method: 'POST',
                                headers: {
                                    'Content-Type': 'application/json',
                                },
                                body: JSON.stringify({
                                    Id_Popup: parseInt(popupSeleccionado.value)
                                }),
                            }
                        );

                        const datos = await response.json();
                        console.log('Estadísticas:', datos.d);

                        // Aquí puedes mostrar un modal con gráficos o tabla
                        alert('Estadísticas cargadas en consola');
                    } catch (error) {
                        console.error('Error:', error);
                    }
                });
            }
        }

        // Prevenir doble submit
        if (lnk_crear_) {
            lnk_crear_.addEventListener('click', () => {
                lnk_crear_.style.display = 'none';
            });
        }

        a.forEach(button => {
            button.addEventListener('click', () => {
                button.style.display = 'none';
            });
        });
    </script>
</asp:Content>