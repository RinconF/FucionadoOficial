///////////////////////// Site.Master /////////////////////////////
$(document).ready(function () {
    //loading
    loading();
    
    //class body container
    $('body').on('click', '.li', function (e) {
        let clase = $(this).attr('class');
        let reemp = "li ";
        let val = clase.replace(reemp, "");

        let id = $(this).attr('id').replace("li_menu", "");

        if (val != "active_li") {
            $("#ul_aside_menu li").each(function () {
                $(".li").removeClass("active_li");
            });
            $("#ul_aside_menu li ul").each(function () {
                $(".ul_aside_submenu").removeAttr("style");
            });

            $(this).addClass("active_li");
            document.getElementById("ul_menu" + id).style.display = "unset";
            //document.getElementById("ul_menu" + id).style.top = `${e.pageY - 100}px`;
        }
        else {
            $(this).removeClass("active_li");
            $(".ul_aside_submenu").removeAttr("style");
        }
    });

    $(function () {
        $('[data-toggle="tooltip"]').tooltip()
    })

    //modal ver noticia reciente
    $("body").on("click", ".body-card-reci", function () {

        let clss = $(this).attr("class");
        let noti = clss.substr(-1);

        $("#modal_noticia_content").html("");
        $(".btn-close-noticia").remove();
        $(".modal_noticia").append(
            "<button type='button' class='btn-close-noticia'><i class='fas fa-times'></i></button>"
        );
        $(".card-" + noti).clone(true).appendTo("#modal_noticia_content");
        $(".modal_noticia").removeAttr("style");
        $("#modal_noticia_content .list-group-item").attr("style", "border: none;");
        $("#modal_noticia_content .card-title").html("");
        $("#modal_noticia_content .card-title").attr("style", "padding: 20px;");
        $(".btn-body-card").removeClass("btn-body-card");
    });

    //modal ver noticia
    $("body").on("click", ".btn-body-card", function () {
        $("#modal_noticia_content").html("");
        $(".btn-close-noticia").remove();
        $(".modal_noticia").append(
            "<button type='button' class='btn-close-noticia'><i class='fas fa-times'></i></button>"
        );
        $(this).clone(true).appendTo("#modal_noticia_content");
        $(".modal_noticia").removeAttr("style");
        $("#modal_noticia_content .list-group-item").attr("style", "border: none;");
        $("#modal_noticia_content .card-title").html("");
        $("#modal_noticia_content .card-title").attr("style", "padding: 20px;");
        $(".btn-body-card").removeClass("btn-body-card");
        
    });

    $('body').on('click', '.btn-close-noticia', function () {
        $(".modal_noticia").attr("style", "display: none;");
        $("#pnl-body-content-card .card__noticias").addClass("btn-body-card");
    });




    //Campos solo númericos
    $('.input-number').on('input', function () {
        let origi = this.value;
        origi = this.value.replace(/[^0-9]/g, '');
        this.value = origi.replace("-", "");
    });



    //Abrir o cerrar aside versión móvil
    $('#nav__icon-menu').click(function () {
        if (!$('#aside').hasClass('active')) {
            $('#aside').addClass('active');
        } else {
            $('#aside').removeClass('active');
        }
    });
});

//abrir ventana emergente
function emergente(url) {
    window.open(url, "Anexos", "width=100, height=100")
}

// hover tooltip
function hover_hide() {
    $(".bs-tooltip-right").attr("style","display: none;");
}
function hover_show() {
    $(".bs-tooltip-right").removeAttr("style");
}






//loading
function loading_init() {
    setTimeout(function () {
        loading();
    },1000);
}
function loading() {
    let box = $(".card-loading-spinner").attr("class");
    if (box.includes("modal-noti-hide")) {
        $(".card-loading-spinner").removeClass("modal-noti-hide");
        $(".card-loading-spinner").addClass("modal-noti-show");
    }
    else {
        $(".card-loading-spinner").removeClass("modal-noti-show");
        $(".card-loading-spinner").addClass("modal-noti-hide");
    }
}

































/*\\console\\ Viernes 01 de agosto 2025, Diego Córdoba comenta estos bloques de código
ya que al momento que los usuarios combinan las teclas (Alt Gr + Q) ó (Control + Q)
el sistema abre una consola que puede ocasionar riesgos de ciberseguridad.*/

//$(document).ready(function () {

//    let isCtrl = false;
//    document.onkeyup = function (e) {
//        if (e.which == 17) isCtrl = false;
//    }
//    document.onkeydown = function (e) {
//        if (e.which == 17) { isCtrl = true }
//        if (e.which == 81 && isCtrl == true) {
//            if ($(".modal-console").attr("class").includes("modal-noti-show")) {
//                $(".modal-console").removeClass("modal-noti-show");
//                $(".modal-console").addClass("modal-noti-hide");
//                $("#line_code_console").val("");
//                $("#console_title").html("");
//                $("#console_title").append(
//                    "<p><i class='fas fa-terminal'></i>Terminal intranet v_0.0.1</p>" +
//                    "<p>=======================================</p>"
//                );
//            }
//            else {
//                $(".modal-console").removeClass("modal-noti-hide");
//                $(".modal-console").addClass("modal-noti-show");
//                $("#line_code_console").val("");
//                $("#line_code_console").focus();
//                $("#console_code").scrollTop($("#console_code").prop("scrollHeight"));
//                $("#console_title").html("");
//                $("#console_title").append(
//                    "<p><i class='fas fa-terminal'></i>Terminal intranet v_0.0.1</p>" +
//                    "<p>=======================================</p>"
//                );
//            }
//        }
//    }

//    $("#line_code_console").keypress(function (e) {
//        let code = $("#line_code_console").val();
//        if (e.which == 13) {
//            if (code == "clear") {
//                $("#console_code").html("");
//            }
//            else {
//                $("#console_code").append(
//                    "<p id='p_loading'>" + code + " <i class='fas fa-circle-notch'></i></p>"
//                );
//                Datos(code);
//            }
//            $("#console_code").scrollTop($("#console_code").prop("scrollHeight"));
//            $("#line_code_console").val("");
//        }
//    });
//});

//function Datos(e) {
//    $.ajax({
//        type: 'POST',
//        url: '',
//        data: '{"code": "' + e + '"}',
//        contentType: "application/json; charset=utf-8",
//        dataType: "json",
//        success: function (resultado) {
//            $("#console_code").append(
//                "<p>" + e + "</p>"
//            );
//        },
//        error: function () {
//            $("#p_loading").remove();
//            $("#console_code").append(
//                "<p>la consulta [" + e + "] no es permitida.</p>"
//            );
//        }
//    }).done(function () {
//        $("#p_loading").remove();
//        $("#console_code").append(
//            "<p>consulta exitosa</p>"
//        );
//    });
//}