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
            toggleModal('modal_crear_aplicativo', true);
        }

        function mostrarModalActualizar() {
            toggleModal('modal_actualizar_aplicativo', true);
        }

        function mostrarModalEliminar() {
            toggleModal('modal_eliminar_aplicativo', true);
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
    <div class="container">
        <h2>CONTROL DE TUTORIALES</h2>
        
        <button type="button" class="btn btn-primary" onclick="mostrarModal('modalNuevo')">
            + NUEVO TUTORIAL
        </button>
        
        <asp:Literal ID="lit_tabla_tutoriales" runat="server"></asp:Literal>
        
        <!-- Modal Nuevo Tutorial -->
        <div id="modalNuevo" class="modal">
            <div class="modal-content">
                <h3>NUEVO TUTORIAL</h3>
                
                <div class="form-group">
                    <label>TÍTULO:</label>
                    <asp:TextBox ID="txt_titulo" runat="server" MaxLength="150"></asp:TextBox>
                </div>
                
                <div class="form-group">
                    <label>DESCRIPCIÓN:</label>
                    <asp:TextBox ID="txt_descripcion" runat="server" TextMode="MultiLine" Rows="3" MaxLength="300"></asp:TextBox>
                </div>
                
                <div class="form-group">
                    <label>VIDEO:</label>
                    <asp:FileUpload ID="fud_video" runat="server" accept="video/*" />
                </div>
                
                <div class="form-group">
                    <label>SECCIÓN:</label>
                    <asp:DropDownList ID="ddl_seccion" runat="server">
                        <asp:ListItem Value="" Text="-- Seleccione --"></asp:ListItem>
                        <asp:ListItem Value="General" Text="General"></asp:ListItem>
                        <asp:ListItem Value="Básico" Text="Básico"></asp:ListItem>
                        <asp:ListItem Value="Intermedio" Text="Intermedio"></asp:ListItem>
                        <asp:ListItem Value="Avanzado" Text="Avanzado"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                
                <div class="btn-group">
                    <asp:Button ID="btn_guardar" runat="server" Text="GUARDAR" CssClass="btn btn-success" OnClick="btn_guardar_Click" />
                    <button type="button" class="btn btn-secondary" onclick="ocultarModal('modalNuevo')">CANCELAR</button>
                </div>
                
                <asp:Label ID="lbl_mensaje" runat="server" CssClass="msg-error"></asp:Label>
            </div>
        </div>
        
        <!-- Modal Editar Tutorial -->
        <div id="modalEditar" class="modal">
            <div class="modal-content">
                <h3>EDITAR TUTORIAL</h3>
                
                <asp:HiddenField ID="hf_id_tutorial" runat="server" />
                <asp:HiddenField ID="hf_video_actual" runat="server" />
                
                <div class="form-group">
                    <label>TÍTULO:</label>
                    <asp:TextBox ID="txt_titulo_edit" runat="server" MaxLength="150"></asp:TextBox>
                </div>
                
                <div class="form-group">
                    <label>DESCRIPCIÓN:</label>
                    <asp:TextBox ID="txt_descripcion_edit" runat="server" TextMode="MultiLine" Rows="3" MaxLength="300"></asp:TextBox>
                </div>
                
                <div class="form-group">
                    <label>VIDEO ACTUAL:</label>
                    <asp:Label ID="lbl_video_actual" runat="server" Text="No hay video"></asp:Label>
                </div>
                
                <div class="form-group">
                    <label>NUEVO VIDEO (opcional):</label>
                    <asp:FileUpload ID="fud_video_edit" runat="server" accept="video/*" />
                    <small>Dejar vacío para mantener el video actual</small>
                </div>
                
                <div class="form-group">
                    <label>SECCIÓN:</label>
                    <asp:DropDownList ID="ddl_seccion_edit" runat="server">
                        <asp:ListItem Value="" Text="-- Seleccione --"></asp:ListItem>
                        <asp:ListItem Value="General" Text="General"></asp:ListItem>
                        <asp:ListItem Value="Básico" Text="Básico"></asp:ListItem>
                        <asp:ListItem Value="Intermedio" Text="Intermedio"></asp:ListItem>
                        <asp:ListItem Value="Avanzado" Text="Avanzado"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                
                <div class="form-group">
                    <label>ESTADO:</label>
                    <asp:DropDownList ID="ddl_estado_edit" runat="server">
                        <asp:ListItem Value="1" Text="Activo"></asp:ListItem>
                        <asp:ListItem Value="0" Text="Inactivo"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                
                <div class="btn-group">
                    <asp:Button ID="btn_actualizar" runat="server" Text="ACTUALIZAR" CssClass="btn btn-success" OnClick="btn_actualizar_Click" />
                    <button type="button" class="btn btn-secondary" onclick="ocultarModal('modalEditar')">CANCELAR</button>
                </div>
                
                <asp:Label ID="lbl_mensaje_edit" runat="server" CssClass="msg-error"></asp:Label>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        function mostrarModal(modalId) {
            document.getElementById(modalId).style.display = 'block';
        }

        function ocultarModal(modalId) {
            document.getElementById(modalId).style.display = 'none';
        }

        function confirmarEliminacion() {
            return confirm('¿Está seguro de eliminar este tutorial?');
        }

        window.onload = function () {
            var urlParams = new URLSearchParams(window.location.search);
            if (urlParams.get('action') === 'edit') {
                mostrarModal('modalEditar');
            }
        };
    </script>
</asp:Content>