var popupManager = (function () {
    let popupsActivos = [];
    let indiceActual = 0;
    let usuarioId = null;
    let popupMostrandose = false;

    function init(idUsuario) {
        usuarioId = idUsuario;

        // VERIFICAR que estamos en la página Default.aspx (Home)
        const rutaActual = window.location.pathname.toLowerCase();
        const esHome = rutaActual.endsWith('default.aspx') ||
            rutaActual.endsWith('/') ||
            rutaActual === '/intranet_3._0/' ||
            rutaActual === '/';

        if (!esHome) {
            console.log('PopupManager: No está en Home, no se mostrarán popups');
            return;
        }

        cargarPopupsActivos();
    }

    async function cargarPopupsActivos() {
        try {
            console.log('PopupManager: Cargando popups para usuario:', usuarioId);

            const response = await fetch('/WebService_Default.asmx/Obtener_Popups_Usuario', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json; charset=utf-8'
                },
                body: JSON.stringify({
                    Id_Usuario: parseInt(usuarioId)
                })
            });

            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }

            const datos = await response.json();
            console.log('PopupManager: Respuesta del servidor:', datos);

            if (datos.d && datos.d.success && Array.isArray(datos.d.popups)) {
                popupsActivos = datos.d.popups;
                console.log(`PopupManager: ${popupsActivos.length} popups disponibles`);
            } else {
                console.warn('PopupManager: No hay popups o formato incorrecto:', datos);
                popupsActivos = [];
            }
        } catch (error) {
            console.error('PopupManager: Error al cargar popups:', error);
            popupsActivos = [];
        }
    }

    function mostrarSiguientePopup() {
        if (popupMostrandose) {
            return;
        }

        if (indiceActual >= popupsActivos.length) {
            console.log('PopupManager: No hay más popups para mostrar');
            return;
        }

        const popup = popupsActivos[indiceActual];
        mostrarPopup(popup);
    }

    function mostrarPopup(popup) {
        popupMostrandose = true;

        // Crear contenedor del popup
        const contenedor = document.createElement('div');
        contenedor.id = `popup-${popup.Id_Popup}`;
        contenedor.className = 'popup-overlay';
        contenedor.style.cssText = `
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background: rgba(0, 0, 0, 0.7);
            display: flex;
            align-items: center;
            justify-content: center;
            z-index: 9999;
            animation: fadeIn 0.3s ease-in;
        `;

        // Crear modal del popup
        const modal = document.createElement('div');
        modal.className = 'popup-modal';
        modal.style.cssText = `
            background: white;
            border-radius: 12px;
            max-width: 600px;
            max-height: 80vh;
            overflow-y: auto;
            position: relative;
            padding: 30px;
            box-shadow: 0 10px 40px rgba(0, 0, 0, 0.3);
            animation: slideIn 0.3s ease-out;
        `;

        // Botón cerrar
        const btnCerrar = document.createElement('button');
        btnCerrar.innerHTML = '×';
        btnCerrar.style.cssText = `
            position: absolute;
            top: 10px;
            right: 15px;
            background: none;
            border: none;
            font-size: 32px;
            color: #999;
            cursor: pointer;
            line-height: 1;
            padding: 0;
            width: 30px;
            height: 30px;
        `;
        btnCerrar.onmouseover = () => btnCerrar.style.color = '#333';
        btnCerrar.onmouseout = () => btnCerrar.style.color = '#999';
        btnCerrar.onclick = () => cerrarPopup(popup.Id_Popup, 'cerrado_manual');

        // Título
        const titulo = document.createElement('h2');
        titulo.textContent = popup.Titulo;
        titulo.style.cssText = `
            margin: 0 0 15px 0;
            color: #2c3e50;
            font-size: 24px;
            padding-right: 30px;
        `;

        // Descripción
        const descripcion = document.createElement('p');
        descripcion.textContent = popup.Descripcion;
        descripcion.style.cssText = `
            margin: 0 0 20px 0;
            color: #555;
            line-height: 1.6;
        `;

        // Multimedia (imagen o video)
        let multimedia = null;

        if (popup.Tipo === 'imagen' && popup.RutaMultimedia) {
            multimedia = document.createElement('img');
            multimedia.src = popup.RutaMultimedia;
            multimedia.alt = popup.Titulo;
            multimedia.style.cssText = `
                max-width: 100%;
                height: auto;
                border-radius: 8px;
                margin-bottom: 20px;
                display: block;
            `;
        } else if (popup.Tipo === 'video' && popup.RutaMultimedia) {
            multimedia = document.createElement('video');
            multimedia.src = popup.RutaMultimedia;
            multimedia.controls = true;
            multimedia.autoplay = false;
            multimedia.style.cssText = `
                max-width: 100%;
                height: auto;
                border-radius: 8px;
                margin-bottom: 20px;
                display: block;
            `;

            // Para video: cerrar cuando termine
            multimedia.addEventListener('ended', () => {
                cerrarPopup(popup.Id_Popup, 'auto_cerrado');
            });
        }

        // URL (si existe)
        let linkBtn = null;
        if (popup.Url) {
            linkBtn = document.createElement('a');
            linkBtn.href = popup.Url;
            linkBtn.target = '_blank';
            linkBtn.textContent = 'Más información';
            linkBtn.style.cssText = `
                display: inline-block;
                padding: 10px 20px;
                background: #3498db;
                color: white;
                text-decoration: none;
                border-radius: 5px;
                margin-top: 10px;
                transition: background 0.3s;
            `;
            linkBtn.onmouseover = () => linkBtn.style.background = '#2980b9';
            linkBtn.onmouseout = () => linkBtn.style.background = '#3498db';
            linkBtn.onclick = () => {
                // Registrar clic en URL
                registrarInteraccion(popup.Id_Popup, 'clic_url');
            };
        }

        modal.appendChild(btnCerrar);
        modal.appendChild(titulo);
        modal.appendChild(descripcion);
        if (multimedia) modal.appendChild(multimedia);
        if (linkBtn) modal.appendChild(linkBtn);

        contenedor.appendChild(modal);
        document.body.appendChild(contenedor);

        // Registrar visualización
        registrarInteraccion(popup.Id_Popup, 'visto');

        // Cerrar automáticamente si tiene tiempo definido Y NO es video
        if (popup.Tipo !== 'video' && popup.Tiempo_Visualizacion && popup.Tiempo_Visualizacion > 0) {
            setTimeout(() => {
                cerrarPopup(popup.Id_Popup, 'auto_cerrado');
            }, popup.Tiempo_Visualizacion * 1000);
        }

        // Cerrar al hacer clic fuera del modal
        contenedor.onclick = (e) => {
            if (e.target === contenedor) {
                cerrarPopup(popup.Id_Popup, 'cerrado_manual');
            }
        };
    }

    function cerrarPopup(idPopup, tipoInteraccion) {
        const contenedor = document.getElementById(`popup-${idPopup}`);
        if (contenedor) {
            // Registrar cómo se cerró (si no es 'visto' que ya se registró)
            if (tipoInteraccion && tipoInteraccion !== 'visto') {
                registrarInteraccion(idPopup, tipoInteraccion);
            }

            contenedor.style.animation = 'fadeOut 0.3s ease-out';
            setTimeout(() => {
                contenedor.remove();
                popupMostrandose = false;
                indiceActual++;

                // Esperar 1 segundo antes de mostrar el siguiente popup
                setTimeout(() => {
                    mostrarSiguientePopup();
                }, 1000);
            }, 300);
        }
    }

    async function registrarInteraccion(idPopup, tipoInteraccion) {
        try {
            console.log(`PopupManager: Registrando interacción '${tipoInteraccion}' para popup:`, idPopup);

            // ✅ CORRECCIÓN: Usar el método correcto del WebService
            const response = await fetch('/WebService_Default.asmx/Registrar_Interaccion_Popup', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json; charset=utf-8'
                },
                body: JSON.stringify({
                    Id_Popup: parseInt(idPopup),
                    Id_Usuario: parseInt(usuarioId),
                    Interaccion: tipoInteraccion
                })
            });

            const resultado = await response.json();
            console.log('PopupManager: Interacción registrada:', resultado);
        } catch (error) {
            console.error('PopupManager: Error al registrar interacción:', error);
        }
    }

    // CSS para animaciones
    const style = document.createElement('style');
    style.textContent = `
        @keyframes fadeIn {
            from { opacity: 0; }
            to { opacity: 1; }
        }
        @keyframes fadeOut {
            from { opacity: 1; }
            to { opacity: 0; }
        }
        @keyframes slideIn {
            from { 
                transform: translateY(-50px);
                opacity: 0;
            }
            to { 
                transform: translateY(0);
                opacity: 1;
            }
        }
    `;
    document.head.appendChild(style);

    return {
        init: init,
        mostrarSiguientePopup: mostrarSiguientePopup
    };
})();
