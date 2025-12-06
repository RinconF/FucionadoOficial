// ========================================
// POPUP - SISTEMA DE AUTO-CIERRE
// ========================================

class PopupManager {
    constructor() {
        this.popupsQueue = [];
        this.currentIndex = 0;
        this.autoCloseTimer = null;
        this.progressInterval = null;
        this.userId = null;
    }

    // Inicializar sistema
    init(userId) {
        this.userId = userId;
        this.cargarPopupsPendientes();
    }

    // Obtener popups del servidor (Action 0)
    async cargarPopupsPendientes() {
        try {
            const response = await fetch('WebService_Default.asmx/Obtener_Popups_Usuario', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({
                    Id_Usuario: this.userId
                })
            });

            const data = await response.json();

            if (data.d && data.d.success && data.d.popups) {
                this.popupsQueue = data.d.popups;
            } else {
                this.popupsQueue = [];
            }
        } catch (error) {
            console.error('Error al cargar popups:', error);
        }
    }

    // Mostrar popup actual con auto-cierre
    mostrarSiguientePopup() {
        if (this.currentIndex >= this.popupsQueue.length) {
            return; // No hay más popups
        }

        const popup = this.popupsQueue[this.currentIndex];
        const tiempoVisualizacion = Number(popup.Tiempo_Visualizacion) || 5;

        // Construir HTML del popup
        this.renderizarPopup(popup);

        // Registrar vista inicial (Action 7)
        this.registrarInteraccion(popup.Id_Popup, 'visto');

        // Iniciar auto-cierre
        this.iniciarAutoClose(popup.Id_Popup, tiempoVisualizacion);
    }

    // Renderizar HTML del popup
    renderizarPopup(popup) {
        const totalPopups = this.popupsQueue.length;
        const currentNumber = this.currentIndex + 1;

        const popupHTML = `
            <div id="popupOverlay" style="
                position: fixed;
                top: 0;
                left: 0;
                width: 100%;
                height: 100%;
                background: rgba(0,0,0,0.7);
                display: flex;
                justify-content: center;
                align-items: center;
                z-index: 9999;
                animation: fadeIn 0.3s ease;
            ">
                <div class="popup-container" style="
                    background: white;
                    border-radius: 15px;
                    padding: 30px;
                    max-width: 600px;
                    width: 90%;
                    position: relative;
                    box-shadow: 0 10px 40px rgba(0,0,0,0.3);
                    animation: slideUp 0.4s ease;
                ">
                    <!-- Contador -->
                    <div style="
                        position: absolute;
                        top: 15px;
                        left: 15px;
                        background: rgba(22, 160, 133, 0.9);
                        color: white;
                        padding: 5px 12px;
                        border-radius: 20px;
                        font-size: 12px;
                        font-weight: bold;
                    ">
                        ${currentNumber}/${totalPopups}
                    </div>

                    <!-- Botón cerrar -->
                    <button 
                        onclick="popupManager.cerrarPopupManual(${popup.Id_Popup})"
                        style="
                            position: absolute;
                            top: 15px;
                            right: 15px;
                            background: #e74c3c;
                            color: white;
                            border: none;
                            width: 30px;
                            height: 30px;
                            border-radius: 50%;
                            cursor: pointer;
                            font-size: 18px;
                            line-height: 1;
                        "
                    >
                        ✕
                    </button>

                     <!-- Imagen / Video -->
                    ${popup.Tipo === 'video' && popup.RutaMultimedia ? `
                        <video
                            src="${popup.RutaMultimedia}"
                            controls
                            autoplay
                            playsinline
                            style="
                                width: 100%;
                                max-height: 300px;
                                object-fit: cover;
                                border-radius: 10px;
                                margin-bottom: 20px;
                                background: #000;
                            "
                        ></video>
                    ` : popup.RutaMultimedia ? `
                        <img
                            src="${popup.RutaMultimedia}"
                            alt="${popup.Titulo}"
                            style="
                                width: 100%;
                                max-height: 300px;
                                object-fit: cover;
                                border-radius: 10px;
                                margin-bottom: 20px;
                            "
                        />
                    ` : ''}

                    <!-- Contenido -->
                    <h2 style="
                        color: #2c3e50;
                        margin: 0 0 15px 0;
                        font-size: 24px;
                    ">
                        ${popup.Titulo}
                    </h2>

                    <p style="
                        color: #7f8c8d;
                        line-height: 1.6;
                        margin: 0 0 25px 0;
                    ">
                        ${popup.Descripcion}
                    </p>

                    <!-- Botones -->
                    <div style="display: flex; gap: 10px; justify-content: center;">
                        ${popup.Url ? `
                            <button 
                                onclick="popupManager.abrirURL('${popup.Url}', ${popup.Id_Popup})"
                                style="
                                    background: #3498db;
                                    color: white;
                                    border: none;
                                    padding: 12px 24px;
                                    border-radius: 25px;
                                    cursor: pointer;
                                    font-weight: bold;
                                    transition: all 0.3s;
                                "
                            >
                                📄 Más información
                            </button>
                        ` : ''}

                        ${totalPopups > 1 ? `
                            <button 
                                onclick="popupManager.siguientePopup(${popup.Id_Popup})"
                                style="
                                    background: #16a085;
                                    color: white;
                                    border: none;
                                    padding: 12px 24px;
                                    border-radius: 25px;
                                    cursor: pointer;
                                    font-weight: bold;
                                "
                            >
                                ➡️ Siguiente
                            </button>
                        ` : ''}
                    </div>

                    <!-- Barra de progreso -->
                    <div style="
                        margin-top: 20px;
                        height: 6px;
                        background: #ecf0f1;
                        border-radius: 3px;
                        overflow: hidden;
                    ">
                        <div 
                            id="timerProgress"
                            style="
                                height: 100%;
                                background: linear-gradient(90deg, #16a085, #3498db);
                                width: 0%;
                                transition: width 0.1s linear;
                            "
                        ></div>
                    </div>

                    <p style="
                        text-align: center;
                        color: #95a5a6;
                        font-size: 12px;
                        margin: 10px 0 0 0;
                    ">
                        Se cerrará en <span id="countdown">${popup.Tiempo_Visualizacion}</span>s
                    </p>
                </div>
            </div>
        `;

        // Insertar en el DOM
        const existingOverlay = document.getElementById('popupOverlay');
        if (existingOverlay) {
            existingOverlay.remove();
        }

        document.body.insertAdjacentHTML('beforeend', popupHTML);
        document.body.style.overflow = 'hidden';
    }

    // Iniciar auto-cierre con barra de progreso
    iniciarAutoClose(idPopup, tiempoSegundos) {
        this.limpiarTimers();

        const tiempoMs = tiempoSegundos * 1000;
        const startTime = Date.now();
        const progressBar = document.getElementById('timerProgress');
        const countdown = document.getElementById('countdown');

        // Actualizar barra cada 100ms
        this.progressInterval = setInterval(() => {
            const elapsed = Date.now() - startTime;
            const progress = Math.min((elapsed / tiempoMs) * 100, 100);
            const remaining = Math.max(Math.ceil((tiempoMs - elapsed) / 1000), 0);

            if (progressBar) progressBar.style.width = progress + '%';
            if (countdown) countdown.textContent = remaining;

            if (progress >= 100) {
                clearInterval(this.progressInterval);
            }
        }, 100);

        // Timer principal
        this.autoCloseTimer = setTimeout(() => {
            this.registrarInteraccion(idPopup, 'auto_cerrado');
            this.cerrarYSiguiente();
        }, tiempoMs);
    }

    // Limpiar timers
    limpiarTimers() {
        if (this.autoCloseTimer) {
            clearTimeout(this.autoCloseTimer);
            this.autoCloseTimer = null;
        }
        if (this.progressInterval) {
            clearInterval(this.progressInterval);
            this.progressInterval = null;
        }
    }

    // Cerrar manual
    cerrarPopupManual(idPopup) {
        this.limpiarTimers();
        this.registrarInteraccion(idPopup, 'cerrado_manual');
        this.cerrarYSiguiente();
    }

    // Abrir URL
    abrirURL(url, idPopup) {
        this.limpiarTimers();
        this.registrarInteraccion(idPopup, 'clic_url');
        window.open(url, '_blank');
        this.cerrarYSiguiente();
    }

    // Siguiente popup
    siguientePopup(idPopup) {
        this.limpiarTimers();
        this.registrarInteraccion(idPopup, 'visto');
        this.cerrarYSiguiente();
    }

    // Cerrar y mostrar siguiente
    cerrarYSiguiente() {
        const overlay = document.getElementById('popupOverlay');
        if (overlay) {
            overlay.style.animation = 'fadeOut 0.3s ease';
            setTimeout(() => {
                overlay.remove();
                document.body.style.overflow = '';
            }, 300);
        }

        this.currentIndex++;
        setTimeout(() => {
            this.mostrarSiguientePopup();
        }, 500);
    }

    // Registrar interacción (Action 7)
    async registrarInteraccion(idPopup, tipoInteraccion) {
        try {
            await fetch('WebService_Default.asmx/Registrar_Interaccion_Popup', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({
                    Id_Popup: idPopup,
                    Id_Usuario: this.userId,
                    Interaccion: tipoInteraccion
                })
            });
        } catch (error) {
            console.error('Error al registrar interacción:', error);
        }
    }
}

// Animaciones CSS
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

    @keyframes slideUp {
        from {
            transform: translateY(50px);
            opacity: 0;
        }
        to {
            transform: translateY(0);
            opacity: 1;
        }
    }
`;
document.head.appendChild(style);

// Instancia global
const popupManager = new PopupManager();