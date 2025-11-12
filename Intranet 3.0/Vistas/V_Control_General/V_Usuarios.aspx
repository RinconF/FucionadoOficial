<%@ Page Title="Usuarios" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="V_Usuarios.aspx.cs" Inherits="Intranet_3._0.Vistas.V_Control_General.V_Usuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="scripts_css" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="scripts_js" runat="server">

    <script>
        $(document).ready(function () {
            $('#MainContent_tbl_usuarios').DataTable({
                responsive: true
            });

            //modal actualizar
            $('body').on('click', '.btn-actu-usu', function () {

                if ($("#MainContent_tbl_usuarios input[name='rd_estado_usu']:radio").is(':checked')) {
                    $('#modal_actualizar_usu').addClass('modal-i-gl-show');
                    $('#modal_actualizar_usu').removeClass('modal-i-gl-hide');

                    $.ajax({
                        type: "POST",
                        url: "WebService_V_Control_General.asmx/cargar_datos_modal_actualizar_usuarios",
                        data: '{"Id_Usuario": "' + $('input:radio[name=rd_estado_usu]:checked').val() + '"}',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (resultado) {
                            let items = resultado.d;
                            $.each(items, function (index, item) {

                                $("#MainContent_txt_usu_actu").val(item[0]);
                                $("#MainContent_txt_pass_actu").val(item[1]);
                                $("#MainContent_txt_drop_rol_usu").val(item[2]);
                                $("#MainContent_drop_rol_usu").val(item[3]);
                                $("#MainContent_txt_drop_estado_usu").val(item[4]);

                            });
                        }
                    });

                } else {
                    $('#modal_actualizar_usu').addClass('modal-i-gl-hide');
                    $('#modal_actualizar_usu').removeClass('modal-i-gl-show');

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


            //Restablecer
            $('body').on('click', '.btn-restablecer-usu', function () {

                if ($("#MainContent_tbl_usuarios input[name='rd_estado_usu']:radio").is(':checked')) {
                    $('#modal_restablecer').addClass('modal-i-gl-show');
                    $('#modal_restablecer').removeClass('modal-i-gl-hide');

                } else {
                    $('#modal_actualizar_usu').addClass('modal-i-gl-hide');
                    $('#modal_actualizar_usu').removeClass('modal-i-gl-show');

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

            //Restablecer foto
            $('body').on('click', '.btn-restablecer-foto-usu', function () {

                if ($("#MainContent_tbl_usuarios input[name='rd_estado_usu']:radio").is(':checked')) {
                    $('#modal_restablecer_foto').addClass('modal-i-gl-show');
                    $('#modal_restablecer_foto').removeClass('modal-i-gl-hide');

                } else {
                    $('#modal_restablecer_foto').addClass('modal-i-gl-hide');
                    $('#modal_restablecer_foto').removeClass('modal-i-gl-show');

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
                    <p><i class="fas fa-tag"></i>Tabla de usuarios</p>
                </div>
                <div class="filter">
                    <div class="box_menu_crear">
                        <button type="button" id="btn_crear_usu" class="btn-modal" data-id="modal_crear_usu">
                            <i class="fas fa-plus"></i>Nuevo usuario
                        </button>
                        <button type="button" id="btn_actualizar_usu" class="btn-actu-usu" data-id="btn-actu-usu">
                            <i class="fas fa-cog"></i>Actualizar usuario
                        </button>
                        <button type="button" id="btn_restablecer_pass" class="btn-restablecer-usu modal-button">
                            <i class="fas fa-key"></i>Restablecer contraseña
                        </button>
                        <button type="button" id="btn_restablecer_foto" class="btn-restablecer-foto-usu modal-button">
                            <i class="fas fa-camera-retro"></i>Restablecer foto
                        </button>
                    </div>
                    <%--<div class="box_search">
                        <i class="fas fa-search"></i>
                        <asp:TextBox runat="server" ID="txt_filter_grupo" placeholder="Búsqueda rápida" OnTextChanged="txt_filter_grupo_TextChanged" AutoPostBack="true"></asp:TextBox>
                    </div>--%>
                </div>
                <%--<table runat="server" id="tbl_usuarios" class="tbl_roles tbl_vistas_general"></table>--%>
                <asp:GridView runat="server" ID="tbl_usuarios" AutoGenerateColumns="False" Width="100%" CssClass="display"  ForeColor="#333333" GridLines="None" HorizontalAlign="Center">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:BoundField DataField="Id_Usuario" HeaderText="#" >
                        <ControlStyle Font-Overline="False" Font-Underline="True" />
                        <FooterStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Wrap="True" />
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Nombre" HeaderText="Nombre" >
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Usuario" HeaderText="Usuario" />
                        <%--<asp:BoundField DataField="Contraseña" HeaderText="Contraseña" />--%>
                        <asp:BoundField DataField="Nombre_Rol" HeaderText="Rol" />
                        <asp:BoundField DataField="Usuario_Creacion" HeaderText="Usuario Creacion" >
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Fecha_Creacion" HeaderText="Fecha Creacion" />
                        <asp:BoundField DataField="Estado Contrato" HeaderText="Estado Contrato">
                          <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                          <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Estado" HeaderText="Estado" >
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Seleccionar">
                            <ItemTemplate>
                                <input type="radio" name="rd_estado_usu" value="<%#Eval("Id_Usuario")%>" />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        </asp:TemplateField>
                    </Columns>
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#273747" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                </asp:GridView>
            </section>
        </ContentTemplate>
    </asp:UpdatePanel>



    <!--modales-->
    <!--USUARIOS-->
    <!--MODAL USUARIOS-->
    <div class="modal-i-gl modal-i-gl-hide animated fadeIn" id="modal_crear_usu">
        <div class="modal-i-gl-body">
            <div class="modal-i-gl-title">
                <h1 class="title">Crear nuevo usuario</h1>
                <div class="modal-i-gl-cerrar">
                    <button type="button" class="btn-modal-close"><i class="fas fa-times"></i></button>
                </div>
            </div>
            <div class="modal-i-gl-content">

                <!--Aquí el contenido-->
                <section class="box_content_crear_vista">
                    <div class="content row">
                        <div class="pnl_input col">
                            <i class="fas fa-user"></i>
                            <asp:TextBox runat="server" ID="txt_usuario" placeholder="USUARIO"></asp:TextBox>
                        </div>
                        <div class="pnl_input col">
                            <i class="fas fa-lock"></i>
                            <asp:TextBox runat="server" type="password" ID="txt_contraseña" placeholder="CONTRASEÑA" Enabled="true" autocomplete="on"></asp:TextBox>
                        </div>
                    </div>
                    <div class="content row">
                        <div class="pnl_input col">
                            <asp:DropDownList runat="server" ID="drop_rol"></asp:DropDownList>
                        </div>
                        <div class="col"></div>
                    </div>
                    <asp:LinkButton runat="server" ID="lnk_crear_" OnClick="crear_nuevo_usu">CREAR</asp:LinkButton>
                </section>

            </div>
        </div>
    </div>

    <!--MODAL ACTUALIZAR USUARIOS-->
    <div class="modal-i-gl modal-i-gl-hide animated fadeIn" id="modal_actualizar_usu">
        <div class="modal-i-gl-body">
            <div class="modal-i-gl-title">
                <h1 class="title">Actualizar datos usuarios</h1>
                <div class="modal-i-gl-cerrar">
                    <button type="button" class="btn-modal-close"><i class="fas fa-times"></i></button>
                </div>
            </div>
            <div class="modal-i-gl-content">

                <!--Aquí el contenido-->
                <section class="box_content_crear_vista">
                    <div class="content row">
                        <div class="pnl_input col">
                            <label style="position: absolute; margin-top: -20px; margin-left: 10px; font-size: 12px; font-weight: bold;">Usuario:</label>
                            <i class="far fa-keyboard"></i>
                            <asp:TextBox runat="server" ID="txt_usu_actu" placeholder="USUARIO"></asp:TextBox>
                        </div>

                        <div class="pnl_input col">
                            <label style="position: absolute; margin-top: -20px; margin-left: 10px; font-size: 12px; font-weight: bold;">Contraseña:</label>
                            <i class="far fa-keyboard"></i>
                            <asp:TextBox runat="server" ID="txt_pass_actu" placeholder="CONTRASEÑA" Enabled="false"></asp:TextBox>
                        </div>
                    </div>
                    <div class="content row">
                        <div class="col">
                            <label style="position: absolute; margin-top: -20px; margin-left: 10px; font-size: 12px; font-weight: bold;">Rol:</label>
                            <div class="row" style="margin: 0px;">
                                <div class="pnl_input col" style="margin: 0px; margin-right: 10px;">
                                    <i class="far fa-keyboard"></i>
                                    <asp:TextBox runat="server" ID="txt_drop_rol_usu" placeholder="VALOR DROP ROL" Enabled="false"></asp:TextBox>
                                </div>
                                <div class="pnl_input col" style="margin: 0px; max-width: max-content;">
                                    <asp:DropDownList runat="server" ID="drop_rol_usu"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col">
                            <label style="position: absolute; margin-top: -20px; margin-left: 10px; font-size: 12px; font-weight: bold;">Estado:</label>
                            <div class="row" style="margin: 0px;">
                                <div class="pnl_input col" style="margin: 0px; margin-right: 10px;">
                                    <i class="far fa-keyboard"></i>
                                    <asp:TextBox runat="server" ID="txt_drop_estado_usu" placeholder="VALOR DROP" Enabled="false"></asp:TextBox>
                                </div>
                                <div class="pnl_input col" style="margin: 0px; max-width: max-content;">
                                    <asp:DropDownList runat="server" ID="drop_estado_usu">
                                        <asp:ListItem Value="">Seleccione...</asp:ListItem>
                                        <asp:ListItem Value="1">Activo</asp:ListItem>
                                        <asp:ListItem Value="0">Inactivo</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <asp:LinkButton runat="server" ID="lnk_actualizar_rol" OnClick="actualizar_datos_usuarios">ACTUALIZAR</asp:LinkButton>
                </section>

            </div>
        </div>
    </div>

    <!--MODAL RESTABLECER CONTRASEÑA-->
    <div class="modal-i-gl modal-i-gl-hide animated fadeIn" id="modal_restablecer">
        <div class="modal-i-gl-body modal-i-gl-body-small">
            <div class="modal-i-gl-title">
                <h1 class="title">Restablecer contraseña</h1>
                <div class="modal-i-gl-cerrar">
                    <button type="button" class="btn-modal-close"><i class="fas fa-times"></i></button>
                </div>
            </div>
            <div class="modal-i-gl-content">

                <!--Aquí el contenido-->
                <section class="box_content_crear_vista">
                    <p>¿Esta seguro que desea restablecer la contraseña de este usuario?</p>
                    <asp:LinkButton runat="server" ID="lnk_si" CssClass="lnk_btn" OnClick="restablecer_contraseña">
                        <i class="fas fa-check"></i> Si
                    </asp:LinkButton>
                    <button runat="server" id="btn_no" class="lnk_btn btn-modal-close" type="button">
                        <i class="fas fa-times"></i>No
                    </button>
                </section>

            </div>
        </div>
    </div>

    <!--MODAL RESTABLECER FOTO-->
    <div class="modal-i-gl modal-i-gl-hide animated fadeIn" id="modal_restablecer_foto">
        <div class="modal-i-gl-body modal-i-gl-body-small">
            <div class="modal-i-gl-title">
                <h1 class="title">Restablecer fotografía de perfil</h1>
                <div class="modal-i-gl-cerrar">
                    <button type="button" class="btn-modal-close"><i class="fas fa-times"></i></button>
                </div>
            </div>
            <div class="modal-i-gl-content">

                <!--Aquí el contenido-->
                <section class="box_content_crear_vista">
                    <p>¿Esta seguro que desea restablecer la fotografía de perfil de este usuario?</p>
                    <asp:LinkButton runat="server" ID="LinkButton1" CssClass="lnk_btn" OnClick="restablecer_foto">
                        <i class="fas fa-check"></i> Si
                    </asp:LinkButton>
                    <button runat="server" id="Button1" class="lnk_btn btn-modal-close" type="button">
                        <i class="fas fa-times"></i>No
                    </button>
                </section>

            </div>
        </div>
    </div>

    <script defer>
        const botones = [...document.querySelectorAll('a')];

        botones.map(boton => {
            boton.addEventListener('click', () => {
                boton.style.display = 'none';
            })
        })
    </script>
</asp:Content>
