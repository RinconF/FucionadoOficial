<%@ Page ValidateRequest="false" Title="Control de Tutoriales" Language="C#"
    MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="V_Control_Tutoriales.aspx.cs"
    Inherits="Intranet_3._0.Vistas.V_Comunicacion.V_Control_Tutoriales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="scripts_css" runat="server">
    <style>
        /* Botones y badges en la misma línea que Popups */
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

        .modal-i-gl .pnl_input textarea {
            width: 100% !important;
            resize: vertical;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="scripts_js" runat="server">
    <script>
        function toggleModal(modalId, show) {
            var modal = document.getElementById(modalId);
            if (!modal) return;
            if (show) {
                modal.classList.add('modal-i-gl-show');
                modal.classList.remove('modal-i-gl-hide');
            } else {
                modal.classList.add('modal-i-gl-hide');
                modal.classList.remove('modal-i-gl-show');
            }
        }

        function mostrarModalCrear() {
            toggleModal('modal_crear_tutorial', true);
        }

        function mostrarModalEditar() {
            toggleModal('modal_editar_tutorial', true);
        }

        document.addEventListener('click', function (ev) {
            if (ev.target.classList.contains('btn-modal-close')) {
                var modal = ev.target.closest('.modal-i-gl');
                if (modal) { toggleModal(modal.id, false); }
            }
        });

        function confirmarEliminacion() {
            return confirm('¿Está seguro de eliminar este tutorial?');
        }

        window.addEventListener('load', function () {
            var urlParams = new URLSearchParams(window.location.search);
            if (urlParams.get('action') === 'edit') {
                mostrarModalEditar();
            }
        });
    </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="pnlTutoriales" runat="server">
        <ContentTemplate>
            <section class="pnl_table">
                <div class="pnl_tag">
                    <p><i class="fas fa-tag"></i>Tabla de tutoriales</p>
                </div>
                <div class="filter">
                    <div class="box_menu_crear">
                        <button type="button" id="btn_modal_crear" class="btn-modal" onclick="mostrarModalCrear()">
                            <i class="fas fa-plus"></i>Nuevo Tutorial
                        </button>
                        <asp:LinkButton
                            runat="server"
                            ID="btn_modal_editar"
                            CssClass="btn-actu-grupo"
                            OnClientClick="mostrarModalEditar(); return false;">
                            <i class="fas fa-cog"></i>Editar Tutorial
                        </asp:LinkButton>
                    </div>
                </div>
                <div runat="server" id="tbl_tutoriales">
                    <asp:Literal ID="lit_tabla_tutoriales" runat="server"></asp:Literal>
                </div>
            </section>

            <asp:HiddenField ID="hf_id_tutorial" runat="server" />
            <asp:HiddenField ID="hf_video_actual" runat="server" />

            <!-- Modal Nuevo Tutorial -->
            <div class="modal-i-gl modal-i-gl-hide animated fadeIn" id="modal_crear_tutorial">
                <div class="modal-i-gl-body">
                    <div class="modal-i-gl-title">
                        <h1 class="title">Nuevo Tutorial</h1>
                        <div class="modal-i-gl-cerrar">
                            <button type="button" class="btn-modal-close">
                                <i class="fas fa-times"></i>
                            </button>
                        </div>
                    </div>
                    <div class="modal-i-gl-content">
                        <section class="box_content_crear_vista">
                            <div class="content row">
                                <div class="pnl_input col">
                                    <i class="far fa-keyboard"></i>
                                    <asp:TextBox ID="txt_titulo" runat="server" MaxLength="150" placeholder="TÍTULO"></asp:TextBox>
                                </div>
                            </div>

                            <div class="content row">
                                <div class="pnl_input col">
                                    <i class="fas fa-align-right"></i>
                                    <asp:TextBox ID="txt_descripcion" runat="server" TextMode="MultiLine" Rows="3" MaxLength="300" placeholder="DESCRIPCIÓN"></asp:TextBox>
                                </div>
                            </div>

                            <div class="content row">
                                <div class="pnl_input col">
                                    <i class="fas fa-photo-video"></i>
                                    <asp:FileUpload ID="fud_video" runat="server" accept="video/*" />
                                </div>
                            </div>

                            <div class="content row">
                                <div class="pnl_input col">
                                    <label style="font-weight: bold; display: block; margin-bottom: 10px;">
                                        <i class="fas fa-layer-group"></i> Sección
                                    </label>
                                    <asp:DropDownList ID="ddl_seccion" runat="server">
                                        <asp:ListItem Value="" Text="-- Seleccione --"></asp:ListItem>
                                        <asp:ListItem Value="General" Text="General"></asp:ListItem>
                                        <asp:ListItem Value="Básico" Text="Básico"></asp:ListItem>
                                        <asp:ListItem Value="Intermedio" Text="Intermedio"></asp:ListItem>
                                        <asp:ListItem Value="Avanzado" Text="Avanzado"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="content row">
                                <div class="col" style="display: flex; gap: 10px;">
                                    <asp:Button ID="btn_guardar" runat="server" Text="Guardar" CssClass="button" OnClick="btn_guardar_Click" Style="background-color: #27ae60; color: white;" />
                                    <button type="button" class="button" onclick="toggleModal('modal_crear_tutorial', false)">Cancelar</button>
                                </div>
                            </div>

                            <asp:Label ID="lbl_mensaje" runat="server" CssClass="msg-error"></asp:Label>
                        </section>
                    </div>
                </div>
            </div>

            <!-- Modal Editar Tutorial -->
            <div class="modal-i-gl modal-i-gl-hide animated fadeIn" id="modal_editar_tutorial">
                <div class="modal-i-gl-body">
                    <div class="modal-i-gl-title">
                        <h1 class="title">Editar Tutorial</h1>
                        <div class="modal-i-gl-cerrar">
                            <button type="button" class="btn-modal-close">
                                <i class="fas fa-times"></i>
                            </button>
                        </div>
                    </div>
                    <div class="modal-i-gl-content">
                        <section class="box_content_crear_vista">
                            <div class="content row">
                                <div class="pnl_input col">
                                    <i class="far fa-keyboard"></i>
                                    <asp:TextBox ID="txt_titulo_edit" runat="server" MaxLength="150" placeholder="TÍTULO"></asp:TextBox>
                                </div>
                            </div>

                            <div class="content row">
                                <div class="pnl_input col">
                                    <i class="fas fa-align-right"></i>
                                    <asp:TextBox ID="txt_descripcion_edit" runat="server" TextMode="MultiLine" Rows="3" MaxLength="300" placeholder="DESCRIPCIÓN"></asp:TextBox>
                                </div>
                            </div>

                            <div class="content row">
                                <div class="pnl_input col">
                                    <label style="font-weight: bold; display: block; margin-bottom: 10px;">
                                        <i class="fas fa-photo-video"></i> Video actual:
                                    </label>
                                    <asp:Label ID="lbl_video_actual" runat="server" Text="No hay video"></asp:Label>
                                </div>
                            </div>

                            <div class="content row">
                                <div class="pnl_input col">
                                    <i class="fas fa-photo-video"></i>
                                    <asp:FileUpload ID="fud_video_edit" runat="server" accept="video/*" />
                                    <small>Dejar vacío para mantener el video actual</small>
                                </div>
                            </div>

                            <div class="content row">
                                <div class="pnl_input col">
                                    <label style="font-weight: bold; display: block; margin-bottom: 10px;">
                                        <i class="fas fa-layer-group"></i> Sección
                                    </label>
                                    <asp:DropDownList ID="ddl_seccion_edit" runat="server">
                                        <asp:ListItem Value="" Text="-- Seleccione --"></asp:ListItem>
                                        <asp:ListItem Value="General" Text="General"></asp:ListItem>
                                        <asp:ListItem Value="Básico" Text="Básico"></asp:ListItem>
                                        <asp:ListItem Value="Intermedio" Text="Intermedio"></asp:ListItem>
                                        <asp:ListItem Value="Avanzado" Text="Avanzado"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="content row">
                                <div class="pnl_input col">
                                    <label style="font-weight: bold; display: block; margin-bottom: 10px;">
                                        <i class="fas fa-toggle-on"></i> Estado
                                    </label>
                                    <asp:DropDownList ID="ddl_estado_edit" runat="server">
                                        <asp:ListItem Value="1" Text="Activo"></asp:ListItem>
                                        <asp:ListItem Value="0" Text="Inactivo"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="content row">
                                <div class="col" style="display: flex; gap: 10px;">
                                    <asp:Button ID="btn_actualizar" runat="server" Text="Actualizar" CssClass="button" OnClick="btn_actualizar_Click" Style="background-color: #27ae60; color: white;" />
                                    <button type="button" class="button" onclick="toggleModal('modal_editar_tutorial', false)">Cancelar</button>
                                </div>
                            </div>

                            <asp:Label ID="lbl_mensaje_edit" runat="server" CssClass="msg-error"></asp:Label>
                        </section>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
