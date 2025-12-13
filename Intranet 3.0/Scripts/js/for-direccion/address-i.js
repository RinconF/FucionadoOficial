function cargar_select_address_i() {
    var existeAlguno = false;

    function cargarSelect(id, valores) {
        var select = document.getElementById(id);
        if (!select) {
            return;
        }

        existeAlguno = true;
        for (var i = 0; i < valores.length; i++) {
            var option = document.createElement("option");
            option.innerHTML = valores[i];
            select.appendChild(option);
        }
    }

    var via = [
        " ",
        "Av. Calle",
        "Av. Carrera",
        "Autopista",
        "Avenida",
        "Circular",
        "Calle",
        "Diagonal",
        "Carrera",
        "Transversal"
    ];

    var letra = [
        " ",
        "A",
        "B",
        "C",
        "D",
        "E",
        "F",
        "G",
        "H",
        "I",
        "J",
        "K",
        "L",
        "M",
        "N",
        "O",
        "P",
        "Q",
        "R",
        "S",
        "T",
        "U",
        "V",
        "W",
        "X",
        "Y",
        "Z"
    ];

    var cuadrante = [
        " ",
        "Norte",
        "Sur",
        "Este",
        "Oeste"
    ];

    var especial = [
        " ",
        "Bis"
    ];

    cargarSelect("tipo_i", via);
    cargarSelect("letra_i", letra);
    cargarSelect("letra_a_i", letra);
    cargarSelect("letra_i_b", letra);
    cargarSelect("cuadrante_i", cuadrante);
    cargarSelect("cuadrante_a_i", cuadrante);
    cargarSelect("especial_i", especial);

    return existeAlguno;
}

//Cargar datos
function cargar_input_address_i() {
    var input = document.getElementById("MainContent_txt_dire");

    var a = document.getElementById("tipo_i").value;
    if (a == "-Tipo vía-") {
        a = " ";
    }
    var b = document.getElementById("num_a").value;
    var c = document.getElementById("letra_i").value;
    if (c == "-Letra-") {
        c = " ";
    }
    var d = document.getElementById("especial_i").value;
    if (d == "-Bis-") {
        d = " ";
    }
    var e = document.getElementById("cuadrante_i").value;
    if (e == "-Cuadrante-") {
        e = " ";
    }
    var f = document.getElementById("num_b").value;
    var g = document.getElementById("letra_a_i").value;
    if (g == "-Letra-") {
        g = " ";
    }
    var h = document.getElementById("num_c").value;
    var i = document.getElementById("cuadrante_a_i").value;
    if (i == "-Cuadrante-") {
        i = " ";
    }

    var direccion = a + " " + b + c + " " + d + " " + e + " # " + f + g + " - " + h + " " + i;

    input.value = direccion.toUpperCase();
}



$(document).ready(function () {

    $("body").on("click", ".btn-address", function () {
        if ($("#address_card_i").attr("style") != "display: none;") {
            $("#address_card_i").attr("style", "display: none;");
        }
        else {
            $("#address_card_i").removeAttr("style");
            $(".btn-agregar-address").attr("data-id", $(this).attr("data-id"));
        }
    });

    $("body").on("click", ".btn-address-close", function () {
        $("#address_card_i").attr("style", "display: none;");
    });


    $("body").on("click", ".btn-agregar-address", function () {

        let id_input = $(this).attr("data-id");
        if (
            $("#tipo_i").val().length != 0 &&
            $("#num_a").val().length != 0 &&
            $("#num_b").val().length != 0 &&
            $("#num_c").val().length != 0
        ) {
            let valor =
                $("#tipo_i").val() +
                " " +
                $("#num_a").val() +
                " " +
                $("#letra_i").val() +
                " " +
                $("#especial_i").val() +
                " " +
                $("#letra_i_b").val() +
                " " +
                $("#cuadrante_i").val() +
                " # " +
                $("#num_b").val() +
                " " +
                $("#letra_a_i").val() +
                " - " +
                $("#num_c").val() +
                " " +
                $("#cuadrante_a_i").val() +
                " , " +
                $("#cardinal_id").val();

            $("#" + id_input).val(valor.toUpperCase());
            $("#address_card_i").attr("style", "display: none;");
        }
        else {
            $("#" + id_input).val("");
        }
    });

    cargar_select_address_i();
});



//Ocultar y mostrar
function show_hide_address_i() {
    var input = document.getElementById("MainContent_txt_dire");
    var div = document.getElementById("address_card_i");

    if (div.style.display != "none") {
        div.className = "address-card-i animated fadeOut";
        setTimeout(function () { div.style.display = "none"; }, 1000);
        input.readOnly = false;
    }
    else {
        div.className = "address-card-i animated fadeIn";
        div.style.display = "block";
        input.readOnly = true;
    }
}
