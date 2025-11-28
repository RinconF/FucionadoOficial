// Gestor centralizado de popups de usuario
// Requiere jQuery cargado previamente
(function (window, $) {
    const colaPopups = [];
    let indiceActual = 0;
    let temporizador = null;
    let idUsuario = null;
    let eventosEnlazados = false;
    const baseAplicacion = (() => {
        let base = window.APP_BASE || "/";

        if (!base.startsWith("/")) {
            base = `/${base}`;
        }

        if (!base.endsWith("/")) {
            base = `${base}/`;
        }

        return base;
    })();

    const construirUrlServicio = (ruta) => {
        const relativa = (ruta || "").replace(/^\/+/, "");
        return `${baseAplicacion}${relativa}`;
    };

    const esPaginaInicio = () => {
        const path = (window.location.pathname || "").toLowerCase();
        return path === "/" || path.endsWith("/default.aspx") || path.endsWith("default.aspx");
    };

    const limpiarTemporizador = () => {
        if (temporizador) {
            clearTimeout(temporizador);
            temporizador = null;
        }
    };

    const registrarInteraccion = (interaccion) => {
        const popup = colaPopups[indiceActual];
        if (!popup || !popup.Id_Popup || !idUsuario) {
            return;
        }

        $.ajax({
            type: "POST",
            url: construirUrlServicio("Vistas/V_Comunicacion/WebService_V_Comunicacion.asmx/Registrar_Interaccion_Popup"),
            data: JSON.stringify({ Id_Popup: parseInt(popup.Id_Popup), Id_Usuario: parseInt(idUsuario), Interaccion: interaccion }),
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        });
    };

    const ocultarPopup = (cerrarTodo) => {
        limpiarTemporizador();
        $("#popup-usuario").addClass("modal-noti-hide");

        if (!cerrarTodo && indiceActual < colaPopups.length - 1) {
            indiceActual++;
            mostrarPopup();
        }
    };

    const mostrarPopup = () => {
        limpiarTemporizador();

        if (colaPopups.length === 0 || indiceActual >= colaPopups.length) {
            ocultarPopup(false);
            return;
        }

        const popup = colaPopups[indiceActual];
        const contenedor = $("#popup-usuario-media").empty();

        $("#popup-usuario-titulo").text(popup.Titulo || "");
        $("#popup-usuario-descripcion").text(popup.Descripcion || "");

        if (popup.Tipo === "video" && popup.RutaMultimedia) {
            const video = $("<video>", {
                src: popup.RutaMultimedia,
                controls: true,
                autoplay: true,
                playsinline: true
            });
            contenedor.append(video);
        } else if (popup.RutaMultimedia) {
            const imagen = $("<img>", {
                src: popup.RutaMultimedia,
                alt: popup.Titulo || "Popup"
            });
            contenedor.append(imagen);
        } else {
            contenedor.append("<span>No se encuentra el recurso del popup.</span>");
        }

        if (popup.Url) {
            $("#popup-usuario-link").attr("href", popup.Url).show();
        } else {
            $("#popup-usuario-link").hide();
        }

        $("#popup-usuario").removeClass("modal-noti-hide");
        registrarInteraccion("visto");

        const segundos = Number(popup.Tiempo_Visualizacion) || 5;
        temporizador = setTimeout(avanzarPopup, segundos * 1000);
    };

    const avanzarPopup = () => {
        indiceActual++;
        mostrarPopup();
    };

    const cargarPopups = async () => {
        const params = new URLSearchParams(location.search);
        idUsuario = params.get("Id_Usuario");

        if (!idUsuario) {
            return;
        }

        try {
            const resultado = await $.ajax({
                type: "POST",
                url: construirUrlServicio("Vistas/V_Comunicacion/WebService_V_Comunicacion.asmx/Obtener_Popups_Usuario"),
                data: JSON.stringify({ Id_Usuario: parseInt(idUsuario) }),
                contentType: "application/json; charset=utf-8",
                dataType: "json"
            });

            const popups = resultado.d || [];
            colaPopups.length = 0;

            popups.forEach(p => {
                const estadoActivo = typeof p.Estado === "undefined" || p.Estado === true || p.Estado === 1 || p.Estado === "True";
                if (estadoActivo) {
                    colaPopups.push(p);
                }
            });

            indiceActual = 0;
            mostrarPopup();
        } catch (error) {
            console.error("No fue posible cargar los popups del usuario", error);
        }
    };

    const asegurarContenedor = () => {
        if (document.getElementById("popup-usuario")) {
            return;
        }

        const plantilla = `
            <div class="popup-usuario-overlay modal-noti-hide" id="popup-usuario">
                <div class="popup-usuario">
                    <button type="button" class="popup-usuario__cerrar" id="popup-usuario-cerrar" aria-label="Cerrar popup">×</button>
                    <div class="popup-usuario__encabezado">
                        <h2 id="popup-usuario-titulo">Aviso</h2>
                        <p id="popup-usuario-descripcion"></p>
                    </div>
                    <div class="popup-usuario__media" id="popup-usuario-media"></div>
                    <div class="popup-usuario__acciones">
                        <a href="#" id="popup-usuario-link" target="_blank" rel="noopener">Ver más</a>
                        <button type="button" id="popup-usuario-siguiente">Cerrar</button>
                    </div>
                </div>
            </div>`;

        $("body").append(plantilla);
    };

    const enlazarEventos = () => {
        if (eventosEnlazados) {
            return;
        }

        $("body").on("click", "#popup-usuario-cerrar", () => ocultarPopup(true));
        $("body").on("click", "#popup-usuario-siguiente", () => avanzarPopup());
        $("body").on("click", "#popup-usuario-link", () => registrarInteraccion("click"));

        eventosEnlazados = true;
    };

    const init = () => {
        if (!esPaginaInicio()) {
            $("#popup-usuario").remove();
            return;
        }

        asegurarContenedor();
        enlazarEventos();
        cargarPopups();
    };

    window.PopupManager = {
        init
    };
})(window, jQuery);

$(function () {
    if (window.PopupManager) {
        window.PopupManager.init();
    }
});
