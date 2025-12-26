using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Services;
using System.Data;
using BRL;
using DCL;

namespace Intranet_3._0
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Obtener el ID del usuario de la sesión o query string
            string idUsuarioStr = null;

            // Prioridad 1: Desde query string (cuando viene de Login)
            if (Request.QueryString["Id_Usuario"] != null)
            {
                idUsuarioStr = Request.QueryString["Id_Usuario"];
                Session["Id_Usuario"] = idUsuarioStr; // Guardar en sesión
            }
            // Prioridad 2: Desde sesión
            else if (Session["Id_Usuario"] != null)
            {
                idUsuarioStr = Session["Id_Usuario"].ToString();
            }
            // Prioridad 3: Desde cookie
            else if (Request.Cookies["login"] != null && Request.Cookies["login"]["userid"] != null)
            {
                idUsuarioStr = Request.Cookies["login"]["userid"];
                Session["Id_Usuario"] = idUsuarioStr; // Guardar en sesión
            }

            // Si tenemos ID de usuario, inicializar popups
            if (!string.IsNullOrEmpty(idUsuarioStr) && int.TryParse(idUsuarioStr, out int idUsuario))
            {
                // Registrar script para inicializar el popup manager
                string initScript = $@"
                    <script>
                        // Esperar a que el DOM esté listo
                        document.addEventListener('DOMContentLoaded', function() {{
                            console.log('=== INICIALIZANDO POPUP MANAGER ===');
                            console.log('Usuario ID:', {idUsuario});
                            console.log('Página actual:', window.location.pathname);
                            
                            // Inicializar popup manager
                            if (typeof popupManager !== 'undefined') {{
                                popupManager.init({idUsuario});
                                
                                // Mostrar primer popup después de 1 segundo
                                setTimeout(function() {{
                                    console.log('Intentando mostrar primer popup...');
                                    popupManager.mostrarSiguientePopup();
                                }}, 1000);
                            }} else {{
                                console.error('popupManager no está definido');
                            }}
                        }});
                    </script>
                ";

                // Registrar el script en el cliente
                ClientScript.RegisterStartupScript(
                    this.GetType(),
                    "InitPopupManager",
                    initScript,
                    false
                );
            }
            else
            {
                // Log para debugging
                System.Diagnostics.Debug.WriteLine("Default.aspx: No se pudo obtener ID de usuario");
            }
        }
    }
}