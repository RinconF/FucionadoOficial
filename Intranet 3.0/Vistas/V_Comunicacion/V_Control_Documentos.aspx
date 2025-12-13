<%@ Page ValidateRequest="false" Title="Control de Documentación Corporativa" Language="C#"
    MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="V_Control_Documentos.aspx.cs"
    Inherits="Intranet_3._0.Vistas.V_Comunicacion.V_Control_Documentos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="scripts_css" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="scripts_js" runat="server">
    <link rel="Stylesheet" href="/Styles/css/documentos/control-documentos.css" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    
    <section class="pnl_table">
        <div class="pnl_tag">
            <p><i class="fas fa-file-alt"></i>Control de Documentos</p>
        </div>
        <div class="pnl_body">
            <div class="control-header">
                <button type="button" class="btn btn-primary" onclick="mostrarModal('modalNuevo')">
                    <i class="fas fa-plus"></i> NUEVO DOCUMENTO
                </button>
            </div>

            <div class="table-responsive">
                <asp:Literal ID="lit_tabla_documentos" runat="server"></asp:Literal>
            </div>
        </div>
    </section>

    <!-- Modal Nuevo Documento -->
    <div id="modalNuevo" class="modal">
        <div class="modal-content">
            <h3><i class="fas fa-file-upload"></i> NUEVO DOCUMENTO</h3>
            
            <div class="form-group">
                <label>TÍTULO:</label>
                <asp:TextBox ID="txt_titulo" runat="server" MaxLength="150" CssClass="form-control"></asp:TextBox>
            </div>
            
            <div class="form-group">
                <label>DESCRIPCIÓN:</label>
                <asp:TextBox ID="txt_descripcion" runat="server" TextMode="MultiLine" Rows="3" MaxLength="200" CssClass="form-control"></asp:TextBox>
            </div>
            
            <div class="form-group">
                <label>ARCHIVO:</label>
                <asp:FileUpload ID="fud_archivo" runat="server" CssClass="form-control" />
                <small class="form-text">Formatos permitidos: PDF, DOC, DOCX, XLS, XLSX, PPT, PPTX</small>
            </div>
            
            <div class="form-group">
                <label>URL (OPCIONAL):</label>
                <asp:TextBox ID="txt_url" runat="server" CssClass="form-control" placeholder="https://ejemplo.com"></asp:TextBox>
                <small class="form-text">URL externa relacionada con el documento</small>
            </div>
            
            <div class="btn-group">
                <asp:Button ID="btn_guardar" runat="server" Text="GUARDAR" CssClass="btn btn-success" OnClick="btn_guardar_Click" />
                <button type="button" class="btn btn-secondary" onclick="ocultarModal('modalNuevo')">CANCELAR</button>
            </div>
            
            <asp:Label ID="lbl_mensaje" runat="server" CssClass="msg-error"></asp:Label>
        </div>
    </div>

    <!-- Modal Editar Documento -->
    <div id="modalEditar" class="modal">
        <div class="modal-content">
            <h3><i class="fas fa-edit"></i> EDITAR DOCUMENTO</h3>
            
            <asp:HiddenField ID="hf_id_documento" runat="server" />
            <asp:HiddenField ID="hf_archivo_actual" runat="server" />
            
            <div class="form-group">
                <label>TÍTULO:</label>
                <asp:TextBox ID="txt_titulo_edit" runat="server" MaxLength="150" CssClass="form-control"></asp:TextBox>
            </div>
            
            <div class="form-group">
                <label>DESCRIPCIÓN:</label>
                <asp:TextBox ID="txt_descripcion_edit" runat="server" TextMode="MultiLine" Rows="3" MaxLength="200" CssClass="form-control"></asp:TextBox>
            </div>
            
            <div class="form-group">
                <label>ARCHIVO ACTUAL:</label>
                <div class="archivo-actual">
                    <i class="fas fa-file-pdf"></i>
                    <asp:Label ID="lbl_archivo_actual" runat="server" Text="No hay archivo"></asp:Label>
                </div>
            </div>
            
            <div class="form-group">
                <label>NUEVO ARCHIVO (OPCIONAL):</label>
                <asp:FileUpload ID="fud_archivo_edit" runat="server" CssClass="form-control" />
                <small class="form-text">Dejar vacío para mantener el archivo actual</small>
            </div>
            
            <div class="form-group">
                <label>URL:</label>
                <asp:TextBox ID="txt_url_edit" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            
            <div class="form-group">
                <label>ESTADO:</label>
                <asp:DropDownList ID="ddl_estado_edit" runat="server" CssClass="form-control">
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

    <script type="text/javascript">
        function mostrarModal(modalId) {
            document.getElementById(modalId).style.display = 'block';
        }

        function ocultarModal(modalId) {
            document.getElementById(modalId).style.display = 'none';
        }

        function confirmarEliminacion() {
            return confirm('¿Está seguro de eliminar este documento?');
        }

        window.onload = function () {
            var urlParams = new URLSearchParams(window.location.search);
            if (urlParams.get('action') === 'edit') {
                mostrarModal('modalEditar');
            }
        };
    </script>

</asp:Content>