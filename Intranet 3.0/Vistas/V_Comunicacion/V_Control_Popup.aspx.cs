using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BRL;
using DCL;
using Intranet_3._0.Interna;

namespace Intranet_3._0.Vistas.V_Comunicacion
{
    public partial class V_Control_Popup : System.Web.UI.Page
    {
        string pathLog = "";
        string ipServer = "";
        const string CONST_ERRORCONEXIONSERV = "al intentar conectarse al servidor: ";
        const string CONST_ERROR = " - ERROR: ";

        #region Eventos de página

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarRoles(chkl_roles);
                CargarRoles(chkl_roles_pub);
                CargarTablaPopups();
            }
        }

        #endregion

        #region Eventos de botones

        /// <summary>
        /// Crear nuevo popup (Action 2)
        /// </summary>
        protected void lnk_crear_popup_Click(object sender, EventArgs e)
        {
            try
            {
                Int_Popup popup = new Int_Popup
                {
                    Titulo = txt_titulo.Text.Trim(),
                    Descripcion = txt_descripcion.Text.Trim(),
                    Url = string.IsNullOrWhiteSpace(txt_url.Text) ? null : txt_url.Text.Trim(),
                    Tiempo_Visualizacion = string.IsNullOrWhiteSpace(txt_tiempo.Text)
                        ? (int?)null
                        : int.Parse(txt_tiempo.Text),
                    Fecha_Inicio = ParseDate(txt_fecha_inicio.Text),
                    Fecha_Fin = ParseDate(txt_fecha_fin.Text),
                    Estado = true,
                    Id_Usuario = ObtenerIdUsuarioActual(), // usuario creador
                    RolesIds = ObtenerRolesSeleccionados(chkl_roles)
                };

                string consecutivo = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                string nombreBaseArchivo = string.IsNullOrWhiteSpace(popup.Titulo)
                    ? "popup"
                    : popup.Titulo;

                // Multimedia: imagen o video (nunca ambos)
                string rutaImagen = GuardarArchivo(fud_Adjunto, "Imagenes", consecutivo, nombreBaseArchivo);
                string rutaVideo = GuardarArchivo(fud_Video, "Videos", consecutivo, nombreBaseArchivo);

                if (!string.IsNullOrEmpty(rutaImagen))
                {
                    popup.Imagen = rutaImagen;
                    popup.Video = null;
                }
                else if (!string.IsNullOrEmpty(rutaVideo))
                {
                    popup.Video = rutaVideo;
                    popup.Imagen = null;
                }
                else
                {
                    popup.Imagen = null;
                    popup.Video = null;
                }

                int result = Int_Popup_BRL.InsertarPopupConRoles(popup);

                if (result > 0)
                {
                    LimpiarFormularioCrear();
                    CargarTablaPopups();
                    MostrarMensaje("Popup creado correctamente.");
                }
                else
                {
                    MostrarMensaje("No se pudo crear el popup.");
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al crear el popup: " + ex.Message);
            }
        }

        /// <summary>
        /// Actualizar popup (Action 4)
        /// </summary>
        protected void lnk_actualizar_popup_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(hf_id_popup.Value))
                {
                    MostrarMensaje("No se encontró el ID del popup a actualizar.");
                    return;
                }

                Int_Popup popup = new Int_Popup
                {
                    Id_Popup = int.Parse(hf_id_popup.Value),
                    Titulo = txt_titulo_pub.Text.Trim(),
                    Descripcion = txt_descripcion_pub.Text.Trim(),
                    Url = string.IsNullOrWhiteSpace(txt_url_pub.Text) ? null : txt_url_pub.Text.Trim(),
                    Tiempo_Visualizacion = string.IsNullOrWhiteSpace(txt_tiempo_pub.Text)
                        ? (int?)null
                        : int.Parse(txt_tiempo_pub.Text),
                    Fecha_Inicio = ParseDate(txt_fecha_inicio_pub.Text),
                    Fecha_Fin = ParseDate(txt_fecha_fin_pub.Text),
                    Estado = ddl_estado_pub.SelectedValue == "1",
                    Id_Usuario = ObtenerIdUsuarioActual(), // usuario que actualiza
                    RolesIds = ObtenerRolesSeleccionados(chkl_roles_pub)
                };

                string consecutivo = popup.Id_Popup?.ToString() ?? DateTime.Now.ToString("yyyyMMddHHmmssfff");
                string nombreBaseArchivo = string.IsNullOrWhiteSpace(popup.Titulo)
                    ? "popup"
                    : popup.Titulo;
                popup.Imagen = null;
                popup.Video = null;

                if (fud_Adjunto_pub.HasFile)
                {
                    string rutaImagen = GuardarArchivo(fud_Adjunto_pub, "Imagenes", consecutivo, nombreBaseArchivo);
                    popup.Imagen = rutaImagen;  // se actualiza
                    popup.Video = "";           // se elimina el video si existía
                }
                else if (fud_Video_pub.HasFile)
                {
                    string rutaVideo = GuardarArchivo(fud_Video_pub, "Videos", consecutivo, nombreBaseArchivo);
                    popup.Video = rutaVideo;    // se actualiza
                    popup.Imagen = "";          // se elimina la imagen si existía
                }
                // si no se sube nada: Imagen = null, Video = null → SP no cambia multimedia

                int result = Int_Popup_BRL.ActualizarPopupConRoles(popup);

                if (result > 0)
                {
                    CargarTablaPopups();
                    MostrarMensaje("Popup actualizado correctamente.");
                }
                else
                {
                    MostrarMensaje("No se actualizó ningún registro.");
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al actualizar el popup: " + ex.Message);
            }
        }

        /// <summary>
        /// Eliminar (soft delete) popup (Action 6)
        /// </summary>
        protected void lnk_eliminar_popup_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(hf_id_popup_eliminar.Value))
                {
                    MostrarMensaje("No se encontró el ID del popup a eliminar.");
                    return;
                }

                Int_Popup popup = new Int_Popup
                {
                    Id_Popup = int.Parse(hf_id_popup_eliminar.Value),
                    Estado = false,
                    Id_Usuario = ObtenerIdUsuarioActual()
                };

                int result = Int_Popup_BRL.InsertOrUpdate(popup, 6);

                if (result > 0)
                {
                    CargarTablaPopups();
                    MostrarMensaje("Popup eliminado correctamente.");
                }
                else
                {
                    MostrarMensaje("No se eliminó ningún registro.");
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al eliminar el popup: " + ex.Message);
            }
        }

        #endregion

        #region Métodos auxiliares

        private void CargarRoles(CheckBoxList cbl)
        {
            try
            {
                Int_Popup obj = new Int_Popup();
                DataTable dt = Int_Popup_BRL.SelectTable(obj, 15); // Action 15: lista de roles

                cbl.DataSource = dt;
                cbl.DataTextField = "Nombre_Rol";
                cbl.DataValueField = "Id_Rol";
                cbl.DataBind();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al cargar roles: " + ex.Message);
            }
        }


        private void CargarTablaPopups()
        {
            try
            {
                Int_Popup obj = new Int_Popup();
                DataTable dt = Int_Popup_BRL.SelectTable(obj, 1); // Action 1: listar activos

                StringBuilder sb = new StringBuilder();

                sb.Append("<table class='tbl_vistas_general'>");

                sb.Append("<thead>");
                sb.Append("<tr>");
                sb.Append("<th>#</th>");
                sb.Append("<th>ID</th>");
                sb.Append("<th>Título</th>");
                sb.Append("<th>Descripción</th>");
                sb.Append("<th>Fecha Creación</th>");
                sb.Append("<th>Fecha Inicio</th>");
                sb.Append("<th>Fecha Fin</th>");
                sb.Append("<th>Estado</th>");
                sb.Append("<th>Acción</th>");
                sb.Append("</tr>");
                sb.Append("</thead>");
                sb.Append("<tbody>");

                int contador = 1;
                foreach (DataRow row in dt.Rows)
                {
                    string id = row["Id_Popup"].ToString();
                    string titulo = Server.HtmlEncode(row["Titulo"].ToString());
                    string descripcion = Server.HtmlEncode(row["Descripcion"].ToString());
                    string fechaCreacion = row["Fecha_Creacion"].ToString();
                    string fechaInicio = row["Fecha_Inicio"].ToString();
                    string fechaFin = row["Fecha_Fin"].ToString();
                    bool estado = row["Estado"] != DBNull.Value && Convert.ToBoolean(row["Estado"]);

                    sb.Append("<tr>");
                    sb.AppendFormat("<td>{0}</td>", contador);
                    sb.AppendFormat("<td>{0}</td>", id);
                    sb.AppendFormat("<td>{0}</td>", titulo);
                    sb.AppendFormat("<td>{0}</td>", descripcion);
                    sb.AppendFormat("<td>{0}</td>", fechaCreacion);
                    sb.AppendFormat("<td>{0}</td>", fechaInicio);
                    sb.AppendFormat("<td>{0}</td>", fechaFin);

                    string badge = estado
                        ? "<span class='badge badge-success'>Activo</span>"
                        : "<span class='badge badge-secondary'>Inactivo</span>";
                    sb.AppendFormat("<td>{0}</td>", badge);

                    sb.AppendFormat("<td><input type='radio' name='rd_estado_vista' value='{0}' /></td>", id);
                    sb.Append("</tr>");
                    contador++;
                }

                sb.Append("</tbody>");
                sb.Append("</table>");

                tbl_grupos.InnerHtml = sb.ToString();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al cargar la tabla de popups: " + ex.Message);
            }
        }


        private string ObtenerRolesSeleccionados(CheckBoxList cbl)
        {
            StringBuilder sb = new StringBuilder();
            foreach (ListItem item in cbl.Items)
            {
                if (item.Selected)
                {
                    if (sb.Length > 0) sb.Append(",");
                    sb.Append(item.Value);
                }
            }
            return sb.Length == 0 ? null : sb.ToString();
        }

        private DateTime? ParseDate(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return null;
            if (DateTime.TryParse(value, out DateTime fecha)) return fecha;
            return null;
        }

        private int ObtenerIdUsuarioActual()
        {
            return Convert.ToInt32(Session["Id_Usuario"]);
        }

        private string GuardarArchivo(FileUpload control, string carpetaPopups, string consecutivo, string nombreBaseArchivo)
        {
            if (control == null || !control.HasFile)
                return null;

            AG_Utils utilidades = new AG_Utils();
            pathLog = Server.MapPath(@"~/logs");
            ipServer = ConfigurationManager.AppSettings.Get("IPServerAttach");

            bool conectaAdjuntos = utilidades.Ping(ipServer);
            if (!conectaAdjuntos)
            {
                utilidades.logError($"{CONST_ERROR}{CONST_ERRORCONEXIONSERV} {ipServer}. \nMétodo: {System.Reflection.MethodBase.GetCurrentMethod().Name}. \nUsuario:  {ObtenerIdUsuarioActual()}", pathLog);
                return null;
            }

            string pathServer = Server.MapPath(ConfigurationManager.AppSettings.Get("pathServer"));
            string pathRemote = ConfigurationManager.AppSettings.Get("pathRemote");
            string ambiente = ConfigurationManager.AppSettings.Get("ambiente") ?? "DESA";

            if (string.IsNullOrWhiteSpace(pathServer) || string.IsNullOrWhiteSpace(pathRemote))
            {
                utilidades.logError($"{CONST_ERROR}{System.Reflection.MethodBase.GetCurrentMethod().Name}\nNo se encuentran configuradas las rutas de almacenamiento para popups.", pathLog);
                return null;
            }

            var (rutaPopupsLocal, rutaPopupsRemoto) = utilidades.ObtenerRutasPopups(ambiente, carpetaPopups);

            if (string.IsNullOrWhiteSpace(rutaPopupsLocal) || string.IsNullOrWhiteSpace(rutaPopupsRemoto))
            {
                utilidades.logError($"{CONST_ERROR}{System.Reflection.MethodBase.GetCurrentMethod().Name}\nNo se pudieron calcular las rutas de popups con la configuración actual.", pathLog);
                return null;
            }

            string extensionArchivo = Path.GetExtension(control.FileName);
            string nombreFinalArchivo = utilidades.AjusteNombreImagenNoticia(nombreBaseArchivo, consecutivo, extensionArchivo);

            var (guardaImagenLocal, guardaImagenRemota, rutaPopupRemoto) = utilidades.TratamientoNoticias(
                nombreFinalArchivo,
                consecutivo,
                rutaPopupsLocal,
                rutaPopupsRemoto,
                control,
                ObtenerIdUsuarioActual().ToString(),
                pathLog);

            if (guardaImagenLocal && guardaImagenRemota && !string.IsNullOrEmpty(rutaPopupRemoto))
            {
                return rutaPopupRemoto;
            }

            utilidades.logError($"{CONST_ERROR}{System.Reflection.MethodBase.GetCurrentMethod().Name}\nLos archivos del popup no fueron almacenados.", pathLog);
            return null;
        }

        private void LimpiarFormularioCrear()
        {
            txt_titulo.Text = string.Empty;
            txt_descripcion.Text = string.Empty;
            txt_url.Text = string.Empty;
            txt_tiempo.Text = "5";
            txt_fecha_inicio.Text = string.Empty;
            txt_fecha_fin.Text = string.Empty;

            foreach (ListItem item in chkl_roles.Items)
            {
                item.Selected = false;
            }
        }

        private void MostrarMensaje(string mensaje)
        {
            ScriptManager.RegisterStartupScript(
                this,
                GetType(),
                "popupMsg",
                $"alert('{mensaje.Replace("'", "\\'")}');",
                true
            );
        }

        #endregion
    }
}