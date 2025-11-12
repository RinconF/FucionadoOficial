<%@ Page Title="Sesiones" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="V_Sesiones.aspx.cs" Inherits="Intranet_3._0.Vistas.V_Control_General.V_Sesiones" %>

<asp:Content ID="Content1" ContentPlaceHolderID="scripts_css" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="scripts_js" runat="server">

    <script>
        $(document).ready(function () {

            //liberar
            $('body').on('click', '.btn-modal', function () {

                if ($("#MainContent_tbl_sesiones input[name='rd_estado_sesion']:radio").is(':checked')) {
                    $('#modal_liberar').addClass('modal-i-gl-show');
                    $('#modal_liberar').removeClass('modal-i-gl-hide');

                } else {
                    $('#modal_liberar').addClass('modal-i-gl-hide');
                    $('#modal_liberar').removeClass('modal-i-gl-show');

                    //notificación
                    $('.modal-noti').addClass('modal-noti-show');//agregar
                    $('.modal-noti').removeClass('modal-noti-hide');//quitar
                    $('.body-noti').addClass('advert'); //tipo notificación
                    $('.title-noti').html('<span class="fas fa-exclamation-circle"></span> Validación advertencia');//título
                    $('.content-noti').html('Seleccione un usuario para validar.');//mensaje
                    setTimeout(function () {
                        $(".modal-noti").addClass("modal-noti-hide");
                        $(".modal-noti").removeClass("modal-noti-show");
                    }, 4000);
                }
            });

        });
    </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">

    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <section class="pnl_table">
                <div class="pnl_tag">
                    <p><i class="fas fa-tag"></i>Sesiones iniciadas</p>
                </div>
                <div class="filter">
                    <div class="box_menu_crear">
                        <button type="button" id="btn_cerrar_all" class="btn-modal">
                            <i class="fas fa-power-off"></i>Liberar sesión
                        </button>
                    </div>
                    <div class="box_search">
                        <i class="fas fa-search"></i>
                        <asp:TextBox runat="server" ID="txt_filter_grupo" placeholder="Búsqueda rápida" OnTextChanged="txt_filter_grupo_TextChanged" AutoPostBack="true"></asp:TextBox>
                    </div>
                </div>
                <table runat="server" id="tbl_sesiones" class="tbl_sesiones tbl_vistas_general"></table>
            </section>
        </ContentTemplate>
    </asp:UpdatePanel>

    <!--MODAL LIBERAR SESIÓN-->
    <div class="modal-i-gl modal-i-gl-hide animated fadeIn" id="modal_liberar">
        <div class="modal-i-gl-body modal-i-gl-body-small">
            <div class="modal-i-gl-title">
                <h1 class="title">Liberar sesión</h1>
                <div class="modal-i-gl-cerrar">
                    <button type="button" class="btn-modal-close"><i class="fas fa-times"></i></button>
                </div>
            </div>
            <div class="modal-i-gl-content">

                <!--Aquí el contenido-->
                <section class="box_content_crear_vista">
                    <p>¿Esta seguro que desea liberar la sesión de este usuario?</p>
                    <asp:LinkButton runat="server" ID="lnk_si" CssClass="lnk_btn" OnClick="Cerrar_Sesion">
                        <i class="fas fa-check"></i> Si
                    </asp:LinkButton>
                    <button runat="server" id="btn_no" class="lnk_btn btn-modal-close" type="button">
                        <i class="fas fa-times"></i>No
                    </button>
                </section>

            </div>
        </div>
    </div>

</asp:Content>
