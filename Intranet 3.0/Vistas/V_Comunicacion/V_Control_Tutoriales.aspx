<%@ Page ValidateRequest="false" Title="Control de Tutoriales" Language="C#"
    MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="V_Control_Tutoriales.aspx.cs"
    Inherits="Intranet_3._0.Vistas.V_Comunicacion.V_Control_Tutoriales" %>

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
            toggleModal('modal_crear_Tutorial', true);
        }

        function mostrarModalActualizar() {
            toggleModal('modal_actualizar_Tutorial', true);
        }

        function mostrarModalEliminar() {
            toggleModal('modal_eliminar_Tutorial', true);
        }

        document.addEventListener('click', function (ev) {
            if (ev.target.classList.contains('btn-modal-close')) {
                var modal = ev.target.closest('.modal-i-gl');
                if (modal) { toggleModal(modal.id, false); }
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
                        <asp:LinkButton
                            ID="btn_modal_crear"
                            runat="server"
                            CssClass="btn-modal"
                            OnClick="btn_modal_crear_Click">
                            <i class="fas fa-plus"></i>Nuevo tutorial
                        </asp:LinkButton>
                        <asp:LinkButton
                            ID="btn_modal_actualizar"
                            runat="server"
                            CssClass="btn-actu-grupo"
                            OnClick="btn_modal_actualizar_Click">
                            <i class="fas fa-cog"></i>Actualizar tutorial
                        </asp:LinkButton>
                        <asp:LinkButton
                            ID="btn_modal_eliminar"
                            runat="server"
                            CssClass="button"
                            Style="background-color: #e74c3c; color: white;"
                            OnClick="btn_modal_eliminar_Click">
                            <i class="fas fa-trash"></i>Eliminar tutorial
                        </asp:LinkButton>
                    </div>
                    <div class="box_search">
                        <i class="fas fa-search"></i>
                        <asp:TextBox ID="txt_buscar" runat="server" AutoComplete="off" AutoPostBack="true" OnTextChanged="txt_buscar_TextChanged" placeholder="Búsqueda rápida"></asp:TextBox>
                    </div>
                </div>
                <div runat="server" id="tbl_Tutoriales"></div>
            </section>

            <asp:HiddenField ID="hf_id_Tutorial" runat="server" />
            <asp:HiddenField ID="hf_imagen_actual" runat="server" />

            <!-- MODAL CREAR -->
            <div class="modal-i-gl modal-i-gl-hide animated fadeIn" id="modal_crear_Tutorial">
                <div class="modal-i-gl-body">
                    <div class="modal-i-gl-title">
                        <h1 class="title">Crear nuevo Tutorial</h1>
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
                                    <asp:TextBox runat="server" ID="txt_titulo" MaxLength="150" placeholder="TÍTULO"></asp:TextBox>
                                </div>
                                <div class="pnl_input col">
                                    <i class="fas fa-align-right"></i>
                                    <asp:TextBox runat="server" ID="txt_descripcion" MaxLength="300" placeholder="DESCRIPCIÓN"></asp:TextBox>
                                </div>
                            </div>
                            <div class="content row">
                                <div class="pnl_input col">
                                    <i class="fas fa-link"></i>
                                    <asp:TextBox runat="server" ID="txt_url" MaxLength="500" placeholder="URL del Tutorial"></asp:TextBox>
                                </div>
                                <div class="pnl_input col">
                                    <i class="fas fa-images"></i>
                                    <asp:FileUpload runat="server" ID="fud_imagen" accept="image/png, image/gif, image/jpeg, image/jfif" />
                                </div>
                            </div>
                            <div class="content row">
                                <div class="pnl_input col">
                                    <i class="fas fa-list"></i>
                                    <asp:DropDownList ID="ddl_seccion" runat="server">
                                        <asp:ListItem Text="Tutoriales empresariales" Value="EMPRESARIALES" />
                                        <asp:ListItem Text="Tutoriales consulta" Value="CONSULTA" />
                                        <asp:ListItem Text="Tutoriales soporte" Value="SOPORTE" />
                                    </asp:DropDownList>
                                </div>
                                <div class="pnl_input col">
                                    <i class="fas fa-sort-numeric-down"></i>
                                    <asp:TextBox runat="server" ID="txt_orden" TextMode="Number" placeholder="ORDEN (Opcional)"></asp:TextBox>
                                </div>
                            </div>
                            <asp:LinkButton runat="server" ID="lnk_crear_Tutorial" OnClick="lnk_crear_Tutorial_Click">CREAR</asp:LinkButton>
                        </section>
                    </div>
                </div>
            </div>

            <!-- MODAL ACTUALIZAR -->
            <div class="modal-i-gl modal-i-gl-hide animated fadeIn" id="modal_actualizar_Tutorial">
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
                            <div class="content row">
                                <div class="pnl_input col">
                                    <i class="far fa-keyboard"></i>
                                    <asp:TextBox runat="server" ID="txt_titulo_edit" MaxLength="150" placeholder="TÍTULO"></asp:TextBox>
                                </div>
                                <div class="pnl_input col">
                                    <i class="fas fa-align-right"></i>
                                    <asp:TextBox runat="server" ID="txt_descripcion_edit" MaxLength="300" placeholder="DESCRIPCIÓN"></asp:TextBox>
                                </div>
                            </div>
                            <div class="content row">
                                <div class="pnl_input col">
                                    <i class="fas fa-link"></i>
                                    <asp:TextBox runat="server" ID="txt_url_edit" MaxLength="500" placeholder="URL del Tutorial"></asp:TextBox>
                                </div>
                                <div class="pnl_input col">
                                    <i class="fas fa-images"></i>
                                    <asp:FileUpload runat="server" ID="fud_imagen_edit" accept="image/png, image/gif, image/jpeg, image/jfif" />
                                </div>
                            </div>
                            <div class="content row">
                                <div class="pnl_input col">
                                    <i class="fas fa-list"></i>
                                    <asp:DropDownList ID="ddl_seccion_edit" runat="server">
                                        <asp:ListItem Text="Tutoriales empresariales" Value="EMPRESARIALES" />
                                        <asp:ListItem Text="Tutoriales consulta" Value="CONSULTA" />
                                        <asp:ListItem Text="Tutoriales soporte" Value="SOPORTE" />
                                    </asp:DropDownList>
                                </div>
                                <div class="pnl_input col">
                                    <i class="fas fa-sort-numeric-down"></i>
                                    <asp:TextBox runat="server" ID="txt_orden_edit" TextMode="Number" placeholder="ORDEN (Opcional)"></asp:TextBox>
                                </div>
                            </div>
                            <div class="content row">
                                <div class="pnl_input col">
                                    <i class="fas fa-toggle-on"></i>
                                    <asp:DropDownList ID="ddl_estado" runat="server">
                                        <asp:ListItem Text="Activo" Value="1" />
                                        <asp:ListItem Text="Inactivo" Value="0" />
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <asp:LinkButton runat="server" ID="lnk_actualizar_Tutorial" OnClick="lnk_actualizar_Tutorial_Click">ACTUALIZAR</asp:LinkButton>
                        </section>
                    </div>
                </div>
            </div>

            <!-- MODAL ELIMINAR -->
            <div class="modal-i-gl modal-i-gl-hide animated fadeIn" id="modal_eliminar_Tutorial">
                <div class="modal-i-gl-body modal-i-gl-body-small">
                    <div class="modal-i-gl-title">
                        <h1 class="title">Eliminar Tutorial</h1>
                        <div class="modal-i-gl-cerrar">
                            <button type="button" class="btn-modal-close">
                                <i class="fas fa-times"></i>
                            </button>
                        </div>
                    </div>
                    <div class="modal-i-gl-content">
                        <section class="box_content_crear_vista">
                            <p class="modal-i-gl-content-text">¿Estás seguro que deseas eliminar este Tutorial?</p>
                            <p class="modal-i-gl-content-text">
                                <asp:Literal ID="lit_tutorial_eliminar" runat="server"></asp:Literal>
                            </p>
                            <div class="content row">
                                <asp:LinkButton runat="server" ID="lnk_eliminar_Tutorial" OnClick="lnk_eliminar_Tutorial_Click" CssClass="lnk_btn_modal btn_guardar">Si, eliminar</asp:LinkButton>
                                <button type="button" class="lnk_btn_modal btn-modal-close">Cancelar</button>
                            </div>
                        </section>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>