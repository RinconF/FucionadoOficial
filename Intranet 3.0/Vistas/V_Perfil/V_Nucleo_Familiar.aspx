<%@ Page Title="Núcleo familiar" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="V_Nucleo_Familiar.aspx.cs" Inherits="Intranet_3._0.Vistas.V_Perfil.V_Nucleo_Familiar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="scripts_css" runat="server">

    <style>
        .content__spinner {
            width: 100%;
            height: 100%;
            position: fixed;
            top: 0;
            left: 0;
            background-color: #fff;
        }

        .spinner {
            border: 4px solid rgba(0, 0, 0, 0.1);
            width: 36px;
            height: 36px;
            border-radius: 50%;
            border-left-color: #09f;
            animation: spin 1s ease infinite;
        }

        @keyframes spin {
            0% {
                transform: rotate(0deg);
            }

            100% {
                transform: rotate(360deg);
            }
        }

        input#MainContent_txt_data::-webkit-calendar-picker-indicator {
            filter: invert(1);
        }

        .pnl_table {
            border-radius: 5px;
            border: 1px solid #e1e1e1;
        }

            .pnl_table .pnl_content {
                padding: 30px 60px 50px 60px;
                background: linear-gradient(90deg, rgb(36, 58, 80) 0%, rgb(36, 58, 80) 100%);
                border-radius: 5px;
                color: rgba(110, 130, 151,.9);
            }

                .pnl_table .pnl_content .row {
                    padding: 0px;
                    margin: 30px;
                }

                .pnl_table .pnl_content .col {
                    padding: 10px;
                    margin: 0px;
                    position: relative;
                }

                    .pnl_table .pnl_content .col i {
                        position: absolute;
                        top: 5px;
                        left: 5px;
                    }

                    .pnl_table .pnl_content .col input, select {
                        width: 100%;
                        max-width: 100%;
                        padding: 15px 20px;
                        border: 1px solid rgba(29,40,51,1);
                        border-radius: 10px;
                        outline: none;
                        background: rgba(29,40,51,.5);
                        color: #fff;
                        /*box-shadow: 2px 2px 5px inset rgba(0,0,0,.3);*/
                        border-bottom: 1px solid rgba(22, 160, 133, 1);
                        text-transform: uppercase;
                    }

                    .pnl_table .pnl_content .col .select-mdl {
                        box-shadow: none;
                    }

                    .pnl_table .pnl_content .col label {
                        font-size: 20px;
                        font-weight: bold;
                        color: rgba(22, 160, 133, 1);
                    }

                .pnl_table .pnl_content .col_ input {
                    width: 49%;
                    max-width: 100%;
                }

                .pnl_table .pnl_content .col input:disabled {
                    /*border: 1px solid rgba(255, 112, 82, 0.9);*/
                    cursor: no-drop;
                }

                .pnl_table .pnl_content p {
                    margin: 0px;
                    width: 100%;
                    text-align: center;
                    font-size: 30px;
                    color: #e1e1e1;
                }

                .pnl_table .pnl_content a {
                    padding: 10px 30px;
                    border: 1px solid rgba(22, 160, 133, 1);
                    border-radius: 50px;
                    background: none;
                    color: rgba(22, 160, 133, 1);
                    outline: none;
                    text-decoration: none;
                }




        /*card familia*/
        .body-card-fm {
            width: 30rem;
            border: 1px solid #e1e1e1;
            border-radius: 5px;
            background: #f9f9f9;
            box-shadow: 2px 2px 5px rgba(0,0,0,.1);
            position: relative;
            color: rgba(110, 130, 151,.9);
            text-transform: uppercase;
            margin: 20px;
        }

        .title-card-fm {
            padding: 10px;
            border-bottom: 1px solid #e1e1e1;
            text-align: right;
            background: #f0f0f0;
        }

            .title-card-fm button {
                padding: 10px 15px;
                background: rgba(255, 112, 82, .8);
                color: #fff;
                border: none;
                border-radius: 50%;
                box-shadow: 2px 2px 5px rgba(0,0,0,.3);
                transition: all 0.5s;
                outline: none;
                margin-left: 5px;
            }

                .title-card-fm button:nth-child(1) {
                    background: rgba(22, 160, 133, .8);
                }

                    .title-card-fm button:nth-child(1):hover {
                        background: rgba(22, 160, 133, 1);
                    }

                .title-card-fm button:hover {
                    background: rgba(255, 112, 82, 1);
                }

                .title-card-fm button:active {
                    box-shadow: 2px 2px 3px inset rgba(0,0,0,.3);
                }

        .icon-card-fm {
            padding: 10px 26px;
            background: linear-gradient(90deg, rgba(31,43,55,1) 0%, rgba(36,58,80,1) 100%);
            color: #fff;
            max-width: max-content;
            font-size: 40px;
            border-radius: 50%;
            margin-top: -40px;
            margin-left: 20px;
            box-shadow: 2px 2px 5px rgba(0,0,0,.3);
        }

        .content-card-fm .content-card-fm-parent {
            padding: 10px 20px;
            border: 1px solid #e1e1e1;
            border-radius: 50px;
            background: #f9f9f9;
            color: rgba(110, 130, 151, 1);
            font-weight: bold;
            box-shadow: 2px 2px 3px inset rgba(0,0,0,.1);
            width: max-content;
            position: absolute;
            top: 10px;
            left: 120px;
            font-size: 13px;
        }

        .content-card-fm .content-card-fm-nombre {
            text-transform: uppercase;
            text-align: right;
            font-weight: bold;
            font-size: 15px;
            padding: 10px;
            border-bottom: 1px solid #e1e1e1;
            margin-top: -37px;
            margin-left: 120px;
            margin-right: 20px;
            margin-bottom: 0px;
            color: rgba(22, 160, 133, 1);
        }

        .content-card-fm .content-card-fm-cedu {
            text-align: right;
            font-weight: bold;
            font-size: 12px;
            padding: 10px 30px 10px 0px;
        }

        .content-card-fm .row {
            margin: 0px;
            padding: 20px;
            background: #f0f0f0;
            border-top: 1px solid #e1e1e1;
        }

            .content-card-fm .row .col {
                margin: 0px;
                padding: 0px;
            }

                .content-card-fm .row .col ul {
                    margin: 0px;
                    padding: 0px;
                }

                    .content-card-fm .row .col ul li {
                        margin: 0px;
                        padding: 0px;
                        list-style: none;
                        font-size: 13px;
                    }

                        .content-card-fm .row .col ul li strong {
                            margin-right: 5px;
                        }

        .content-card-fm .row_ {
            text-align: center;
            background: #f9f9f9;
            box-shadow: none;
            border-radius: 0px 0px 5px 5px;
        }

            .content-card-fm .row_ a {
                font-size: 12px;
                background: rgba(237, 26, 26,.8);
                color: #fff;
                padding: 10px 20px;
                border-radius: 50px;
                text-decoration: none;
                box-shadow: 2px 2px 3px rgba(0,0,0,.1);
                transition: all 0.5s;
            }

                .content-card-fm .row_ a:hover {
                    background: rgba(237, 26, 26,1);
                    box-shadow: 2px 2px 3px rgba(0,0,0,.3);
                }

                .content-card-fm .row_ a:active {
                    box-shadow: 2px 2px 3px inset rgba(0,0,0,.3);
                }

        #card-fm-container {
            display: inline-flex;
            flex-wrap: wrap;
            padding-left: 60px;
        }

        #btn_agregar {
            border: 1px solid rgba(17,122,102,.8);
            color: #fff;
            background: rgba(22, 160, 133, .9);
            padding: 15px 50px 15px 100px;
            margin-left: 40px;
            margin-top: 20px;
            border-radius: 5px;
            outline: none;
            font-size: unset;
            position: relative;
            text-transform: uppercase;
            transition: all 0.2s;
        }

            #btn_agregar:hover {
                opacity: .8;
            }

            #btn_agregar i {
                font-size: 25px;
                padding: 15px;
                background: rgba(18,140,116,1);
                border-right: 1px solid rgba(17,122,102,.8);
                position: absolute;
                top: 0;
                left: 0;
                border-radius: 0px 0px 0px 5px;
            }

        .select-mdl {
            box-shadow: none;
            color: rgba(0, 0, 0, .7);
        }

        #modal_actualizar_familiar input, select {
            text-transform: uppercase;
            outline: none;
        }

            #modal_actualizar_familiar input:disabled {
                border: 1px solid rgba(255, 112, 82, 0.9);
                cursor: no-drop;
            }



        @media only screen and (max-width: 1127px) {
            .pnl_table .pnl_content {
                padding: 0;
            }

                .pnl_table .pnl_content .row {
                    flex-direction: column;
                }

            #btn_agregar {
                margin: 20px;
            }

            .content-card-fm-parent {
                display: none;
            }

            #card-fm-container {
                padding: 0;
            }

            .body-card-fm {
                width: 100%;
            }

            .row_ {
                display: flex;
                flex-direction: column;
            }

                .row_ a {
                    text-align: center;
                    display: block;
                    margin: 5px;
                }

            .required__input {
                border: 2px solid #8a0808 !important;
            }
        }
    </style>
    <script>
        function solonumeros(e) {
            key = e.keycode || e.which;
            teclado = String.fromCharCode(key);
            numeros = "0123456789";
            especiales = "8-37-38-46-09";
            teclado_especial = false;

            for (var i in especiales) {
                if (key == especiales[i]) {
                    teclado_especial = true;
                }
            }
            if (numeros.indexOf(teclado) == -1 && !teclado_especial) {
                return false;
            }
        }

        function sololetras(e) {
            key = e.keycode || e.which;
            teclado = String.fromCharCode(key);
            numeros = "abcdefghijklmnñopqrstuvwxyz áéíóúüÜABCDEFGHIJKLMNÑOPQRSTUVWXYZ";
            especiales = "8-37-38-46";
            teclado_especial = false;

            for (var i in especiales) {
                if (key == especiales[i]) {
                    teclado_especial = true;
                }
            }
            if (numeros.indexOf(teclado) == -1 && !teclado_especial) {
                return false;
            }
        }
       
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="scripts_js" runat="server">
    <script>
        $(document).ready(function () {
            //Cargar_Datos_Familia();
            $('#MainContent_txt_edad_modal').css({ 'pointer-events': 'none', 'border': '1px solid red' });

            $("body").change("#MainContent_drop_discapacidad", function () {
                if ($("#MainContent_drop_discapacidad").val() == "0") {
                    $("#MainContent_txt_cual").val("");
                }
            });
            $("body").change("#MainContent_drop_parentesco", function () {
                if ($("#MainContent_drop_parentesco").val() != "3") {
                    $("#MainContent_txt_file_registro").val(null);
                    $("#MainContent_txt_file_certificacion").val(null);
                }
            });

            $("body").change("#MainContent_drop_parentesco", function () {
                let value = $("#MainContent_drop_parentesco").val();
                if (value == "3") {
                    $("#MainContent_txt_file_registro").removeAttr("disabled");
                    $("#MainContent_txt_file_certificacion").removeAttr("disabled");
                }
                else {
                    $("#MainContent_txt_file_registro").attr("disabled", "disabled");
                    $("#MainContent_txt_file_certificacion").attr("disabled", "disabled");
                }
            });

            $("body").change("#MainContent_drop_discapacidad", function () {
                let value = $("#MainContent_drop_discapacidad").val();
                if (value == "1") {
                    $("#MainContent_txt_cual").removeAttr("disabled");
                }
                else {
                    $("#MainContent_txt_cual").attr("disabled", "disabled");
                }
            });

            //MODAL CHANGE
            $("body").change("#MainContent_drop_parentesco_modal", function () {
                let value = $("#MainContent_drop_parentesco_modal").val();
                if (value == "3") {
                    $("#MainContent_file_a_modal").removeAttr("disabled");
                    $("#MainContent_file_b_modal").removeAttr("disabled");
                }
                else {
                    $("#MainContent_file_a_modal").attr("disabled", "disabled");
                    $("#MainContent_file_b_modal").attr("disabled", "disabled");
                }
            });

            $("body").change("#MainContent_drop_discapacidad_modal", function () {
                let value = $("#MainContent_drop_discapacidad_modal").val();
                if (value == "1") {
                    $("#MainContent_txt_cual_modal").removeAttr("disabled");
                }
                else {
                    $("#MainContent_txt_cual_modal").attr("disabled", "disabled");
                }
            });


            $('input[type="file"]').change(function () {
                var ext = $(this).val().split('.').pop();
                if ($(this).val() != '') {
                    if (ext == "pdf") {
                        if ($(this)[0].files[0].size > 4194304) {
                            alert("El documento excede el tamaño máximo 4MB");
                            $(this).val('');
                        }
                    }
                    else {
                        $(this).val('');
                        alert("Extensión no permitida: " + ext);
                    }
                }
            });

            $("body").change("#MainContent_txt_data", function () {
                let date = document.getElementById("MainContent_txt_data").value;

                if (!date.includes("dd") || !date.includes("mm") || !date.includes("aaaa")) {
                    //let year = new Date().getFullYear();
                    //let mes = new Date().getMonth() + 1;
                    //let mes_edad = date.substring(5, 7) - mes;
                    //let edad;

                    //if (mes_edad == 0) {
                    //    edad = year - date.substring(0, 4);
                    //}
                    //else if ($("#MainContent_txt_edad").val() == year) {
                    //    edad = "";
                    //}
                    //else {
                    //    edad = (year - date.substring(0, 4)) - 1;
                    //}
                    date = new Date(date);
                    let edadDifMs = Date.now() - date.getTime();
                    let edadFecha = new Date(edadDifMs);
                    let nuevaEdad = Math.abs(edadFecha.getFullYear() - 1970);
                    document.getElementById("MainContent_txt_edad").value = nuevaEdad;
                }
            });

            //validar campos
            $("body").on("click", "#btn_agregar", function (e) {
                e.preventDefault();

                if (
                    $("#MainContent_txt_nombre").val().length != 0 &&
                    $("#MainContent_txt_apellido").val().length != 0 &&
                    $("#MainContent_txt_celular").val().length != 0 &&
                    $("#MainContent_txt_id").val().length != 0 &&
                    $("#MainContent_txt_edad").val().length != 0 &&
                    $("#MainContent_txt_celular").val().length != 0 &&
                    $("#MainContent_drop_genero").val().length != 0 &&
                    $("#MainContent_drop_tipo").val().length != 0 &&
                    $("#MainContent_drop_parentesco").val().length != 0 &&
                    $("#MainContent_drop_escolaridad").val().length != 0 &&
                    $("#MainContent_drop_ocupacion").val().length != 0
                ) {
                    $("#MainContent_txt_edad").removeAttr("disabled");
                    $('btn_agregar').css('display', 'none');
                    if ($("#MainContent_txt_file_registro").attr("disabled") != "disabled" && $("#MainContent_txt_file_certificacion").attr("disabled") != "disabled") {
                        //if ($("#MainContent_txt_file_registro").val().length != 0 /*&& $("#MainContent_txt_file_certificacion").val().length != 0*/) {
                        //verificación para agregar
                        const datos = [...document.querySelectorAll('.content-card-fm-cedu')];
                        const existe = datos.filter(dato => dato.textContent.split('-')[1].trim() === $('#MainContent_txt_id').val());
                        if (existe.length !== 0) {
                            let data_id = '#' + "modal_verificacion_existe";
                            let modal = $(data_id).attr('class');
                            if (!modal.includes('modal-i-gl-hide')) {
                                $(data_id).addClass('modal-i-gl-hide');
                                $(data_id).removeClass('modal-i-gl-show');
                            }
                            else {
                                $(data_id).addClass('modal-i-gl-show');
                                $(data_id).removeClass('modal-i-gl-hide');
                            }
                            return;
                        }


                        let data_id = '#' + "modal_verificacion_agregar";
                        let modal = $(data_id).attr('class');
                        if (!modal.includes('modal-i-gl-hide')) {
                            $(data_id).addClass('modal-i-gl-hide');
                            $(data_id).removeClass('modal-i-gl-show');
                        }
                        else {
                            $(data_id).addClass('modal-i-gl-show');
                            $(data_id).removeClass('modal-i-gl-hide');
                        }
                        //}
                        //else {
                        //    //notificación
                        //    $('.modal-noti').addClass('modal-noti-show');//agregar
                        //    $('.modal-noti').removeClass('modal-noti-hide');//quitar
                        //    $('.body-noti').addClass('advert'); //tipo notificación
                        //    $('.title-noti').html('<span class="fas fa-exclamation-circle"></span> Validación advertencia');//título
                        //    $('.content-noti').html('Campos vacios por favor completar e intentar de nuevo.');//mensaje
                        //    setTimeout(function () {
                        //        $(".modal-noti").addClass("modal-noti-hide");
                        //        $(".modal-noti").removeClass("modal-noti-show");
                        //    }, 4000);
                        //}
                    }
                    else {
                        //verificación para agregar
                        const datos = [...document.querySelectorAll('.content-card-fm-cedu')];
                        const existe = datos.filter(dato => dato.textContent.split('-')[1].trim() === $('#MainContent_txt_id').val());
                        if (existe.length !== 0) {
                            let data_id = '#' + "modal_verificacion_existe";
                            let modal = $(data_id).attr('class');
                            if (!modal.includes('modal-i-gl-hide')) {
                                $(data_id).addClass('modal-i-gl-hide');
                                $(data_id).removeClass('modal-i-gl-show');
                            }
                            else {
                                $(data_id).addClass('modal-i-gl-show');
                                $(data_id).removeClass('modal-i-gl-hide');
                            }
                            return;
                        }


                        let data_id = '#' + "modal_verificacion_agregar";
                        let modal = $(data_id).attr('class');
                        if (!modal.includes('modal-i-gl-hide')) {
                            $(data_id).addClass('modal-i-gl-hide');
                            $(data_id).removeClass('modal-i-gl-show');
                        }
                        else {
                            $(data_id).addClass('modal-i-gl-show');
                            $(data_id).removeClass('modal-i-gl-hide');
                        }
                    }
                }
                else {
                    //notificación
                    $('.modal-noti').addClass('modal-noti-show');//agregar
                    $('.modal-noti').removeClass('modal-noti-hide');//quitar
                    $('.body-noti').addClass('advert'); //tipo notificación
                    $('.title-noti').html('<span class="fas fa-exclamation-circle"></span> Validación advertencia');//título
                    $('.content-noti').html('Campos vacíos por favor completar e intentar de nuevo.');//mensaje
                    setTimeout(function () {
                        $(".modal-noti").addClass("modal-noti-hide");
                        $(".modal-noti").removeClass("modal-noti-show");
                    }, 4000);
                }
            });

            //cargar id de la card del familiar para eliminar
            $("body").on("click", ".btn-modal", function () {

                $("#btn_delete_familiar").attr("data-id", $(this).attr("id"));

            });

            //Cargar datos en modal actualizar
            $("body").on("click", ".btn-edit", function () {
                let id = $(this).attr("id");
                let Id_Familiar = id.replace("edit", "");
                $.ajax({
                    type: "POST",
                    url: "WebService_V_Perfil.asmx/Cargar_Datos_Familiar_Modal",
                    data: '{"Id_Familiar": "' + Id_Familiar + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (resultado) {
                        let items = resultado.d;
                        $.each(items, function (index, item) {
                            $("#MainContent_txt_nombre_modal").val(item[2]);
                            $("#MainContent_txt_apellido_modal").val(item[14]);
                            $("#MainContent_drop_genero_modal").val(item[3]);
                            $("#MainContent_txt_numero_modal").val(item[7]);
                            $("#MainContent_drop_tipo_modal").val(item[4]);
                            $("#MainContent_txt_id_modal").val(item[5]);
                            $("#MainContent_txt_date_modal").val(item[6]);
                            $("#MainContent_txt_edad_modal").val(item[15]);
                            $("#MainContent_drop_parentesco_modal").val(item[8]);
                            $("#MainContent_drop_escolaridad_modal").val(item[9]);
                            $("#MainContent_drop_ocupacion_modal").val(item[10]);
                            $("#MainContent_txt_cual_modal").val(item[11]);

                            $("#MainContent_number_familia").val(" #" + Id_Familiar);

                            //VALIDAR CAMPOS
                            let value = $("#MainContent_drop_parentesco_modal").val();
                            if (value == "3") {
                                $("#MainContent_file_a_modal").removeAttr("disabled");
                                $("#MainContent_file_b_modal").removeAttr("disabled");
                            }
                            else {
                                $("#MainContent_file_a_modal").attr("disabled", "disabled");
                                $("#MainContent_file_b_modal").attr("disabled", "disabled");
                            }
                        });
                    }
                });
            });
        });
    </script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="Stylesheet" href="/Styles/css/nucleo_familiar/nucleo_familiar.css"/>
    <section class="pnl_table">

        <div class="pnl_tag">
            <p><i class="fas fa-tag"></i>Datos núcleo familiar</p>
        </div>

        <div class="pnl_content">
            <div class="row">
                <div class="col">
                    <label>Nombres:</label>
                    <asp:TextBox runat="server" ID="txt_nombre" placeholder="NOMBRES" MaxLength="40" onkeypress="return sololetras(event)"></asp:TextBox>
                </div>
                <div class="col">
                    <label>Apellidos:</label>
                    <asp:TextBox runat="server" ID="txt_apellido" placeholder="APELLIDO" MaxLength="40" onkeypress="return sololetras(event)"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col">
                    <label>Tipo de documento:</label>
                    <asp:DropDownList runat="server" ID="drop_tipo"></asp:DropDownList>
                </div>
                <div class="col">
                    <label>Identificación:</label>
                    <asp:TextBox runat="server" ID="txt_id" placeholder="IDENTIFICACIÓN" MaxLength="12" CssClass="input-number"></asp:TextBox>
                </div>
                <div class="col col_">
                    <label>Fecha de nacimiento:</label><br />
                    <asp:TextBox runat="server" ID="txt_data" type="date"></asp:TextBox>
                    <asp:TextBox runat="server" ID="txt_edad" placeholder="EDAD" MaxLength="2" CssClass="input-number" Enabled="false"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col">
                    <label>Número de contacto:</label>
                    <asp:TextBox runat="server" ID="txt_celular" placeholder="NÚMERO DE CONTACTO" MaxLength="10" CssClass="input-number"></asp:TextBox>
                </div>
                <div class="col">
                    <label>Género:</label>
                    <asp:DropDownList runat="server" ID="drop_genero"></asp:DropDownList>
                </div>
            </div>
            <div class="row">
                <div class="col">
                    <label>Parentesco:</label>
                    <asp:DropDownList runat="server" ID="drop_parentesco"></asp:DropDownList>
                </div>
                <div class="col">
                    <label>Escolaridad:</label>
                    <asp:DropDownList runat="server" ID="drop_escolaridad"></asp:DropDownList>
                </div>
            </div>
            <div class="row">
                <div class="col">
                    <label>Ocupación:</label>
                    <asp:DropDownList runat="server" ID="drop_ocupacion"></asp:DropDownList>
                </div>
                <div class="col">
                    <label>Discapacidad:</label>
                    <asp:DropDownList runat="server" ID="drop_discapacidad">
                        <asp:ListItem Value="0">NO</asp:ListItem>
                        <asp:ListItem Value="1">SI</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="col">
                    <label>Cuál:</label>
                    <asp:TextBox runat="server" ID="txt_cual" placeholder="¿Cuál?" Enabled="false"></asp:TextBox>
                </div>
            </div>
            <div class="row">
                <div class="col">
                    <label>Anexo registro civil:</label>
                    <asp:FileUpload runat="server" ID="txt_file_registro" Enabled="false" accept="application/pdf" />
                </div>
                <div class="col">
                    <label>Anexo certificado estudiantil:</label>
                    <asp:FileUpload runat="server" ID="txt_file_certificacion" Enabled="false" accept="application/pdf" />
                </div>
                <div class="col"></div>
            </div>
            <button type="button" id="btn_agregar" data-id="modal_verificacion" class="btn-modal"><i class="fas fa-cloud-upload-alt"></i>Agregar</button>
        </div>

    </section>

    <br />
    <hr />
    <br />

    <section id="card-fm-container">
    </section>

    <asp:Panel runat="server" ID="pnl_cardContainer"></asp:Panel>

    <!--modales-->
    <!--validación-->
    <div class="modal-i-gl modal-i-gl-hide animated fadeIn" id="modal_verificacion_agregar">
        <div class="modal-i-gl-body-small">
            <div class="modal-i-gl-title">
                <h1 class="title">¿Agregar nuevo familiar?</h1>
                <div class="modal-i-gl-cerrar">
                    <button type="button" class="btn-modal-close"><i class="fas fa-times"></i></button>
                </div>
            </div>
            <div class="modal-i-gl-content">

                <!--Aquí el contenido-->
                <asp:LinkButton runat="server" ID="lnk_crear_" OnClick="agregar_datos_familia"><i class="fas fa-check"></i> Agregar</asp:LinkButton>
                <button type="button" class="btn-modal-close"><i class="fas fa-times"></i>Cancelar</button>

            </div>
        </div>
    </div>


    <div class="modal-i-gl modal-i-gl-hide animated fadeIn" id="modal_verificacion_existe">
        <div class="modal-i-gl-body-small">
            <div class="modal-i-gl-title">
                <h1 class="title">Ya existe un registro con el mismo numero de documento ¿Desea actualizar la informacion?</h1>
                <div class="modal-i-gl-cerrar">
                    <button type="button" class="btn-modal-close"><i class="fas fa-times"></i></button>
                </div>
            </div>
            <div class="modal-i-gl-content">

                <!--Aquí el contenido-->
                <asp:LinkButton runat="server" ID="LinkButton1" OnClick="agregar_datos_familia"><i class="fas fa-check"></i> Agregar</asp:LinkButton>
                <button type="button" class="btn-modal-close"><i class="fas fa-times"></i>Cancelar</button>

            </div>
        </div>
    </div>



    <!--validación eliminar-->
    <div class="modal-i-gl modal-i-gl-hide animated fadeIn" id="modal_verificacion_eliminar">
        <div class="modal-i-gl-body-small">
            <div class="modal-i-gl-title">
                <h1 class="title">¿Eliminar familiar?</h1>
                <div class="modal-i-gl-cerrar">
                    <button type="button" class="btn-modal-close"><i class="fas fa-times"></i></button>
                </div>
            </div>
            <div class="modal-i-gl-content">

                <!--Aquí el contenido-->
                <button type="button" id="btn_delete_familiar" class="btn-delete" style="color: rgba(22,160,133,1); border: 1px solid rgba(22,160,133,1);"><i class="fas fa-check"></i>Eliminar</button>
                <button type="button" class="btn-modal-close"><i class="fas fa-times"></i>Cancelar</button>

            </div>
        </div>
    </div>

    <!--MODAL ACTUALIZAR DATOS FAMILIAR-->
    <div class="modal-i-gl modal-i-gl-hide animated fadeIn" id="modal_actualizar_familiar">
        <div class="modal-i-gl-body">
            <div class="modal-i-gl-title">
                <h1 class="title">Actualizar información familiar
                </h1>
                <div class="modal-i-gl-cerrar">
                    <button type="button" class="btn-modal-close"><i class="fas fa-times"></i></button>
                </div>
            </div>
            <div class="modal-i-gl-content">

                <!--Aquí el contenido-->
                <section class="box_content_crear_vista">

                    <asp:TextBox runat="server" ID="number_familia" placeholder="id" Style="position: absolute; opacity: 0;"></asp:TextBox>
                    <div class="content row">
                        <div class="pnl_input col">
                            <label style="position: absolute; margin-top: -20px; margin-left: 10px; font-size: 12px; font-weight: bold;">Nombre:</label>
                            <i class="far fa-keyboard"></i>
                            <asp:TextBox runat="server" ID="txt_nombre_modal" placeholder="NOMBRES" MaxLength="40" onkeypress="return sololetras(event)"></asp:TextBox>
                        </div>
                        <div class="pnl_input col">
                            <label style="position: absolute; margin-top: -20px; margin-left: 10px; font-size: 12px; font-weight: bold;">Apellido:</label>
                            <i class="far fa-keyboard"></i>
                            <asp:TextBox runat="server" ID="txt_apellido_modal" placeholder="APELLIDOS" MaxLength="40" onkeypress="return sololetras(event)"></asp:TextBox>
                        </div>
                    </div>
                    <div class="content row">
                        <div class="pnl_input col">
                            <label style="position: absolute; margin-top: -20px; margin-left: 10px; font-size: 12px; font-weight: bold;">Genero:</label>
                            <asp:DropDownList runat="server" CssClass="select-mdl" ID="drop_genero_modal"></asp:DropDownList>
                        </div>
                        <div class="pnl_input col">
                            <label style="position: absolute; margin-top: -20px; margin-left: 10px; font-size: 12px; font-weight: bold;">Número contacto:</label>
                            <i class="fas fa-phone-alt"></i>
                            <asp:TextBox runat="server" ID="txt_numero_modal" MaxLength="10" CssClass="input-number" placeholder="NÚMERO DE CONTACTO"></asp:TextBox>
                        </div>
                    </div>
                    <div class="content row">
                        <div class="pnl_input col">
                            <label style="position: absolute; margin-top: -20px; margin-left: 10px; font-size: 12px; font-weight: bold;">Tipo documento:</label>
                            <asp:DropDownList runat="server" CssClass="select-mdl" ID="drop_tipo_modal"></asp:DropDownList>
                        </div>
                        <div class="pnl_input col">
                            <label style="position: absolute; margin-top: -20px; margin-left: 10px; font-size: 12px; font-weight: bold;">Identificación:</label>
                            <i class="fas fa-id-card"></i>
                            <asp:TextBox runat="server" ID="txt_id_modal" MaxLength="12" CssClass="input-number" placeholder="NÚMERO DE IDENTIFICACIÓN"></asp:TextBox>
                        </div>
                    </div>
                    <div class="content row">
                        <div class="pnl_input col">
                            <label style="position: absolute; margin-top: -20px; margin-left: 10px; font-size: 12px; font-weight: bold;">Fecha nacimiento:</label>
                            <asp:TextBox runat="server" type="date" ID="txt_date_modal" placeholder="FECHA DE NACIMIENTO"></asp:TextBox>
                        </div>
                        <div class="pnl_input col">
                            <label style="position: absolute; margin-top: -20px; margin-left: 10px; font-size: 12px; font-weight: bold;">EDAD:</label>
                            <i class="fas fa-calendar-alt"></i>
                            <asp:TextBox runat="server" type="text" ID="txt_edad_modal" CssClass="input-number" placeholder="EDAD"></asp:TextBox>
                        </div>
                        <div class="pnl_input col">
                            <label style="position: absolute; margin-top: -20px; margin-left: 10px; font-size: 12px; font-weight: bold;">Parentesco:</label>
                            <asp:DropDownList runat="server" CssClass="select-mdl" ID="drop_parentesco_modal"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="content row">
                        <div class="pnl_input col">
                            <label style="position: absolute; margin-top: -20px; margin-left: 10px; font-size: 12px; font-weight: bold;">Escolaridad:</label>
                            <asp:DropDownList runat="server" CssClass="select-mdl" ID="drop_escolaridad_modal"></asp:DropDownList>
                        </div>
                        <div class="pnl_input col">
                            <label style="position: absolute; margin-top: -20px; margin-left: 10px; font-size: 12px; font-weight: bold;">Ocupación:</label>
                            <asp:DropDownList runat="server" CssClass="select-mdl" ID="drop_ocupacion_modal"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="content row">
                        <div class="pnl_input col">
                            <label style="position: absolute; margin-top: -20px; margin-left: 10px; font-size: 12px; font-weight: bold;">Discapacidad:</label>
                            <asp:DropDownList runat="server" CssClass="select-mdl" ID="drop_discapacidad_modal">
                                <asp:ListItem Value="0">NO</asp:ListItem>
                                <asp:ListItem Value="1">SI</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="pnl_input col">
                            <label style="position: absolute; margin-top: -20px; margin-left: 10px; font-size: 12px; font-weight: bold;">Cuál:</label>
                            <i class="far fa-keyboard"></i>
                            <asp:TextBox runat="server" ID="txt_cual_modal" Enabled="false" placeholder="CUÁL" onkeypress="return sololetras(event)"></asp:TextBox>
                        </div>
                    </div>
                    <hr />
                    <br />
                    <div class="content row">
                        <div class="pnl_input col">
                            <label style="position: absolute; margin-top: -20px; margin-left: 10px; font-size: 12px; font-weight: bold;">Anexo registro civil:</label>
                            <asp:FileUpload runat="server" ID="file_a_modal" Enabled="false" accept="application/pdf" />
                        </div>
                        <div class="pnl_input col">
                            <label style="position: absolute; margin-top: -20px; margin-left: 10px; font-size: 12px; font-weight: bold;">Anexo certificado estudiantil:</label>
                            <asp:FileUpload runat="server" ID="file_b_modal" Enabled="false" accept="application/pdf" />
                        </div>
                    </div>
                    <asp:LinkButton runat="server" ID="lnk_actualizar_familiar" AutoPostBack="true" OnClick="actualizar_datos_familia">Actualizar</asp:LinkButton>

                </section>

            </div>
        </div>
    </div>


    <script defer>
        //Variables de inputs
        const txt_nombre = document.querySelector('#MainContent_txt_nombre');
        const txt_apellido = document.querySelector('#MainContent_txt_apellido');
        const drop_tipo = document.querySelector('#MainContent_drop_tipo');
        const txt_id = document.querySelector('#MainContent_txt_id');
        const txt_edad = document.querySelector('#MainContent_txt_edad');
        const txt_celular = document.querySelector('#MainContent_txt_celular');
        const drop_genero = document.querySelector('#MainContent_drop_genero');
        const drop_parentesco = document.querySelector('#MainContent_drop_parentesco');
        const drop_escolaridad = document.querySelector('#MainContent_drop_escolaridad');
        const drop_ocupacion = document.querySelector('#MainContent_drop_ocupacion');

        const btnDelete = document.querySelector('.btn-delete');
        const btn_agregar = document.querySelector('#btn_agregar');

        let idFamilia = 0;

        const Cargar_Datos_Familia = async () => {
            const cardFmContainer = document.querySelector('#card-fm-container');
            const params = new URLSearchParams(location.search);
            const Id_Usuario = params.get('Id_Usuario');

            const rest = await fetch(`WebService_V_Perfil.asmx/Cargar_Datos_Familiar`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({
                    Id_Usuario
                })
            });
            const data = await rest.json();
            console.log(rest);

            const datos = [...data.d];

            const promises = datos.map(dato => {
                const div = document.createElement('div');

                if (dato[8] === 'Hijo(a)') {
                    let anexoRegistroCivil;
                    let anexoCertificadoEstudiantil;

                    if (dato[12] != "Ninguno") {
                        anexoRegistroCivil = `<a href="#" onclick="enviarDatos('${dato[12]}');"><i class="fas fa-file-pdf"></i> Registro civil</a>`;
                    }
                    else {
                        anexoRegistroCivil = "";
                    }
                    if (dato[13] != "Ninguno") {
                        anexoCertificadoEstudiantil = `<a href="#" onclick="enviarDatos('${dato[13]}');"><i class="fas fa-file-pdf"></i> Certificado estudiantil</a>`;
                    }
                    else {
                        anexoCertificadoEstudiantil = "";
                    }

                    if (anexoCertificadoEstudiantil == "" && anexoRegistroCivil == "") {
                        anexoRegistroCivil = '<p style="margin:0px; font-weight:bold; text-align:center;"><i class="fas fa-exclamation"></i > Ningún anexo</p>';
                    }

                    div.innerHTML = `
                        <div class="body-card-fm">
                            <div class="title-card-fm">
                                <button id="edit ${dato[0]}" type="button" class="btn-edit btn-modal fas fa-pencil-alt" data-id="modal_actualizar_familiar"></button>
                                <button id="${dato[0]}" type="button" class="btn-modal fas fa-trash-alt" data-id="modal_verificacion_eliminar"></button>
                            </div>
                            <div class="icon-card-fm">
                                <i class="fas fa-baby"></i>
                            </div>
                            <div class="content-card-fm">
                                <p class="content-card-fm-parent">${dato[8]} - ${dato[3]}</p>
                                <p class="content-card-fm-nombre">${dato[2]} ${dato[14]}</p>
                                <p class="content-card-fm-cedu">${dato[4]} - ${dato[5]}</p>
                                <div class="row">
                                    <div class="col">
                                        <ul>
                                            <li><strong><i class="fas fa-child"></i> Edad:</strong>${dato[6]}</li>
                                            <li><strong><i class="fas fa-graduation-cap"></i> Escolaridad:</strong>${dato[9]}</li>
                                        </ul>
                                    </div>
                                    <div class="col">
                                        <ul>
                                            <li><strong><i class="fas fa-address-card"></i> Ocupación:</strong>${dato[10]}</li>
                                            <li><strong><i class="fab fa-accessible-icon"></i> Discapacidad:</strong>${dato[11]}</li>
                                        </ul>
                                    </div>
                                </div>
                                <div class="row row_">
                                    <div class="col">
                                        ${anexoRegistroCivil}
                                    </div>
                                    <div class="col">
                                        ${anexoCertificadoEstudiantil}
                                    </div>
                                </div>
                            </div>
                        </div>
                    `
                }
                else if (dato[0] != "0") {
                    div.innerHTML = `
                        <div class="body-card-fm">
                            <div class="title-card-fm">
                                <button id="edit ${dato[0]}" type="button" class="btn-edit btn-modal fas fa-pencil-alt" data-id="modal_actualizar_familiar"></button>
                                <button id="${dato[0]}" type="button" class="btn-modal fas fa-trash-alt" data-id="modal_verificacion_eliminar"></button>
                            </div>
                            <div class="icon-card-fm">
                                <i class="fas fa-child"></i>
                            </div>
                            <div class="content-card-fm">
                                <p class="content-card-fm-parent">${dato[8]} - ${dato[3]}</p>
                                <p class="content-card-fm-nombre">${dato[2]} ${dato[14]}</p>
                                <p class="content-card-fm-cedu">${dato[4]} - ${dato[5]}</p>
                                <div class="row" style="border-bottom:1px solid #e1e1e1;">
                                    <div class="col">
                                        <ul>
                                            <li><strong><i class="fas fa-child"></i> Edad:</strong>${dato[6]}</li>
                                            <li><strong><i class="fas fa-graduation-cap"></i> Escolaridad:</strong>${dato[9]}</li>
                                        </ul>
                                    </div>
                                    <div class="col">
                                        <ul>
                                            <li><strong><i class="fas fa-address-card"></i> Ocupación:</strong>${dato[10]}</li>
                                            <li><strong><i class="fab fa-accessible-icon"></i> Discapacidad:</strong>${dato[11]}</li>
                                        </ul>
                                    </div>
                                </div>
                                <div class="row row_">
                                    <div class="col">
                                        
                                    </div>
                                </div>
                            </div>
                        </div>
                    `
                }
                return div;
            });

            const divs = await Promise.all(promises);
            for (const div of divs) cardFmContainer.appendChild(div);

            const cards = document.querySelector('#card-fm-container');
            cards.addEventListener('click', e => {
                if (e.target.tagName === 'BUTTON' && !e.target.classList.contains('btn-edit')) {
                idFamilia = e.target.id;
                }
            })
        }

        const enviarDatos = async imagen => {
            window.open(imagen, '_blank');
        }

        btnDelete.addEventListener('click', async e => {
            e.preventDefault();

            await fetch(`WebService_V_Perfil.asmx/Cambiar_Estado_Familia`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({
                    'Id_Familiar': idFamilia
                })
            });

            actualizarPagina();
        });

        const actualizarPagina = () => {
            window.location.reload();
        }

        Cargar_Datos_Familia();

        document.querySelector('#ctl01').reset();


        var today = new Date();
        var dd = today.getDate();
        var mm = today.getMonth() + 1; //January is 0!
        var yyyy = today.getFullYear();
        if (dd < 10) {
            dd = '0' + dd
        }
        if (mm < 10) {
            mm = '0' + mm
        }

        today = yyyy + '-' + mm + '-' + dd;
        document.querySelector("#MainContent_txt_data").setAttribute("max", today);

        const fechas = [...document.querySelectorAll('#MainContent_txt_date_modal')];

        fechas.map(fecha => {
            fecha.addEventListener('change', () => {
                let fechaActual = fecha.value;

                if (!fechaActual.includes("dd") || !fechaActual.includes("mm") || !fechaActual.includes("aaaa")) {
                    //let year = new Date().getFullYear();
                    //let mes = new Date().getMonth() + 1;
                    //let mes_edad = fechaActual.substring(5, 7) - mes;
                    //let edad;
                    //if (mes_edad == 0) {
                    //    edad = year - fechaActual.substring(0, 4);
                    //}
                    //else if ($("#MainContent_txt_edad_modal").val() == year) {
                    //    edad = "";
                    //}
                    //else {
                    //    edad = (year - fechaActual.substring(0, 4)) - 1;
                    //}
                    fechaActual = new Date(fechaActual);
                    let edadDifMs = Date.now() - fechaActual.getTime();
                    let edadFecha = new Date(edadDifMs);
                    let nuevaEdad = Math.abs(edadFecha.getFullYear() - 1970);
                    document.getElementById("MainContent_txt_edad_modal").value = nuevaEdad;
                }
            })
        })

        const as = [...document.querySelectorAll('a')];
        as.map(a => {
            a.addEventListener('click', () => {
                a.style.display = 'none';
            })
        })

        const botones = [...document.querySelectorAll('botton')];
        botones.map(boton => {
            boton.addEventListener('click', () => {
                boton.style.display = 'none';
            })
        })

        const validarIdentificacionFamiliar = () => {
            $('.modal-noti').addClass('modal-noti-show');//agregar
            $('.modal-noti').removeClass('modal-noti-hide');//quitar
            $('.body-noti').addClass('advert'); //tipo notificación
            $('.title-noti').html('<span class="fas fa-exclamation-circle"></span> Validación advertencia');//título
            $('.content-noti').html('La identificación del familiar no puedo ser igual a la del Usuario.');//mensaje
            setTimeout(function () {
                $(".modal-noti").addClass("modal-noti-hide");
                $(".modal-noti").removeClass("modal-noti-show");
            }, 4000);
        }

        btn_agregar.addEventListener('click', () => {
            txt_nombre.value === '' ? txt_nombre.classList.add('required__input') : txt_nombre.classList.remove('required__input')
            txt_apellido.value === '' ? txt_apellido.classList.add('required__input') : txt_apellido.classList.remove('required__input')
            drop_tipo.value === '0' ? drop_tipo.classList.add('required__input') : drop_tipo.classList.remove('required__input')
            txt_id.value === '' ? txt_id.classList.add('required__input') : txt_id.classList.remove('required__input')
            txt_edad.value === '' ? txt_edad.classList.add('required__input') : txt_edad.classList.remove('required__input')
            txt_celular.value === '' ? txt_celular.classList.add('required__input') : txt_celular.classList.remove('required__input')
            drop_genero.value === '0' ? drop_genero.classList.add('required__input') : drop_genero.classList.remove('required__input')
            drop_parentesco.value === '0' ? drop_parentesco.classList.add('required__input') : drop_parentesco.classList.remove('required__input')
            drop_escolaridad.value === '0' ? drop_ocupacion.classList.add('required__input') : drop_escolaridad.classList.remove('required__input')
            drop_ocupacion.value === '0' ? drop_ocupacion.classList.add('required__input') : drop_ocupacion.classList.remove('required__input')
        })
        function quitarPadding() {
            let doc = document.getElementById('container');
            doc.removeAttribute('style');
        }
        document.addEventListener('load', quitarPadding);
        window.addEventListener('load', quitarPadding);
    </script>
</asp:Content>
