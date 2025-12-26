var popupManager = (function () {
    let popupsActivos = [];
    let indiceActual = 0;
    let usuarioId = null;
    let popupMostrandose = false;

    function init(idUsuario) {
        usuarioId = idUsuario;

        // VERIFICAR que estamos en la página Default.aspx
        const rutaActual = window.location.pathname.toLowerCase();
                
        const esHome = rutaActual.endsWith('default.aspx') ||
            rutaActual.endsWith('default') ||
            rutaActual.endsWith('/default') ||
            rutaActual === '/' ||
            rutaActual === '/intranet_3._0/' ||
            rutaActual === '/intranet_3._0';

        if (!esHome) {
            console.log('PopupManager: No está en Home, no se mostrarán popups');
            return;
        }

        cargarPopupsActivos();
    }

    async function cargarPopupsActivos() {
        try {

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

            if (datos.d && datos.d.success && Array.isArray(datos.d.popups)) {
                popupsActivos = datos.d.popups;

                // Si hay popups, mostrar el primero automáticamente después de 500ms
                if (popupsActivos.length > 0) {
                    setTimeout(() => {
                        mostrarSiguientePopup();
                    }, 500);
                }
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

        // Contador de popups (arriba a la izquierda)
        if (popupsActivos.length > 1) {
            const contador = document.createElement('div');
            contador.className = 'popup-contador';
            contador.textContent = `${indiceActual + 1}/${popupsActivos.length}`;
            contador.style.cssText = `
                position: absolute;
                top: 15px;
                left: 20px;
                background: rgba(52, 152, 219, 0.9);
                color: white;
                padding: 5px 12px;
                border-radius: 15px;
                font-size: 13px;
                font-weight: bold;
                z-index: 10;
            `;
            modal.appendChild(contador);
        }

        // Botón cerrar (X)
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
            z-index: 10;
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

            // Para video: cerrar cuando termine (si no hay botón Siguiente)
            if (indiceActual >= popupsActivos.length - 1) {
                multimedia.addEventListener('ended', () => {
                    cerrarPopup(popup.Id_Popup, 'auto_cerrado');
                });
            }
        }

        // Contenedor de botones
        const contenedorBotones = document.createElement('div');
        contenedorBotones.style.cssText = `
            display: flex;
            gap: 10px;
            justify-content: center;
            margin-top: 20px;
        `;

        // Botón "Más información" (si hay URL)
        if (popup.Url) {
            const btnMasInfo = document.createElement('a');
            btnMasInfo.href = popup.Url;
            btnMasInfo.target = '_blank';
            btnMasInfo.textContent = 'Más información';
            btnMasInfo.style.cssText = `
                display: inline-block;
                padding: 12px 25px;
                background: #3498db;
                color: white;
                text-decoration: none;
                border-radius: 25px;
                font-weight: 600;
                transition: all 0.3s;
                border: 2px solid #3498db;
            `;
            btnMasInfo.onmouseover = () => {
                btnMasInfo.style.background = '#2980b9';
                btnMasInfo.style.borderColor = '#2980b9';
            };
            btnMasInfo.onmouseout = () => {
                btnMasInfo.style.background = '#3498db';
                btnMasInfo.style.borderColor = '#3498db';
            };
            btnMasInfo.onclick = () => {
                registrarInteraccion(popup.Id_Popup, 'clic_url');
            };
            contenedorBotones.appendChild(btnMasInfo);
        }

        // Botón "Siguiente" (si hay más popups)
        if (indiceActual < popupsActivos.length - 1) {
            const btnSiguiente = document.createElement('button');
            btnSiguiente.textContent = 'Siguiente';
            btnSiguiente.style.cssText = `
                padding: 12px 25px;
                background: #27ae60;
                color: white;
                border: 2px solid #27ae60;
                border-radius: 25px;
                font-weight: 600;
                cursor: pointer;
                transition: all 0.3s;
            `;
            btnSiguiente.onmouseover = () => {
                btnSiguiente.style.background = '#229954';
                btnSiguiente.style.borderColor = '#229954';
            };
            btnSiguiente.onmouseout = () => {
                btnSiguiente.style.background = '#27ae60';
                btnSiguiente.style.borderColor = '#27ae60';
            };
            btnSiguiente.onclick = () => {
                cerrarPopup(popup.Id_Popup, 'siguiente');
            };
            contenedorBotones.appendChild(btnSiguiente);
        }

        // Ensamblar el modal
        modal.appendChild(btnCerrar);
        modal.appendChild(titulo);
        modal.appendChild(descripcion);
        if (multimedia) modal.appendChild(multimedia);
        if (contenedorBotones.children.length > 0) {
            modal.appendChild(contenedorBotones);
        }

        contenedor.appendChild(modal);
        document.body.appendChild(contenedor);

        // Registrar visualización
        registrarInteraccion(popup.Id_Popup, 'visto');

        // Cerrar automáticamente si tiene tiempo definido Y NO es video Y no hay más popups
        if (popup.Tipo !== 'video' &&
            popup.Tiempo_Visualizacion &&
            popup.Tiempo_Visualizacion > 0 &&
            indiceActual >= popupsActivos.length - 1) {
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

                // Si fue "Siguiente" o hay más popups, mostrar el siguiente inmediatamente
                if (tipoInteraccion === 'siguiente' || indiceActual < popupsActivos.length) {
                    setTimeout(() => {
                        mostrarSiguientePopup();
                    }, 500);
                }
            }, 300);
        }
    }

    async function registrarInteraccion(idPopup, tipoInteraccion) {
        try {
            console.log(`PopupManager: Registrando interacción '${tipoInteraccion}' para popup:`, idPopup);

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
        
        /* Estilos responsivos para móvil */
        @media (max-width: 768px) {
            .popup-modal {
                max-width: 90% !important;
                max-height: 85vh !important;
                padding: 20px !important;
                margin: 0 10px;
            }
            .popup-contador {
                font-size: 11px !important;
                padding: 4px 10px !important;
            }
        }
    `;
    document.head.appendChild(style);

    return {
        init: init,
        mostrarSiguientePopup: mostrarSiguientePopup
    };
})();