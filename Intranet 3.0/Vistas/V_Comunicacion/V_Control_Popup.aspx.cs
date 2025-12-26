using BRL;
using DCL;
using Intranet_3._0.Interna;
using System;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Intranet_3._0.Vistas.V_Comunicacion
{
    public partial class V_Control_Popup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarTablaPopups();
                CargarRoles();
            }
        }

        #region Cargar Datos

        /// <summary>
        /// Carga la tabla HTML con todos los popups activos
        /// </summary>
        private void CargarTablaPopups()
        {
            try
            {
                // Action 1: Listar todos los popups activos
                DataTable dt = Int_Popup_BRL.SelectTable(new Int_Popup(), 1);

                if (dt != null && dt.Rows.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("<table class='tbl_vistas_general table table-striped table-hover'>");
                    sb.Append("<thead><tr>");
                    sb.Append("<th style='width: 50px;'>#</th>"); // Número de fila
                    sb.Append("<th style='width: 60px;'>ID</th>");
                    sb.Append("<th>TITULO</th>");
                    sb.Append("<th>DESCRIPCION</th>");
                    sb.Append("<th style='width: 80px;'>Imagen</th>");
                    sb.Append("<th style='width: 80px;'>Video</th>");
                    sb.Append("<th style='width: 100px;'>Tiempo (s)</th>");
                    sb.Append("<th style='width: 120px;'>Fecha Inicio</th>");
                    sb.Append("<th style='width: 120px;'>Fecha Fin</th>");
                    sb.Append("<th style='width: 80px;'>ACCION</th>"); // Radio button
                    sb.Append("</tr></thead><tbody>");

                    int contador = 1;
                    foreach (DataRow row in dt.Rows)
                    {
                        string idPopup = row["Id_Popup"].ToString();
                        string titulo = row["Titulo"]?.ToString() ?? "";
                        string descripcion = row["Descripcion"]?.ToString() ?? "";
                        string imagen = row["Imagen"]?.ToString() ?? "";
                        string video = row["Video"]?.ToString() ?? "";
                        string url = row["Url"]?.ToString() ?? "";
                        string tiempo = row["Tiempo_Visualizacion"]?.ToString() ?? "5";
                        DateTime? fechaInicio = row["Fecha_Inicio"] != DBNull.Value ? Convert.ToDateTime(row["Fecha_Inicio"]) : (DateTime?)null;
                        DateTime? fechaFin = row["Fecha_Fin"] != DBNull.Value ? Convert.ToDateTime(row["Fecha_Fin"]) : (DateTime?)null;
                        bool estado = row["Estado"] != DBNull.Value && Convert.ToBoolean(row["Estado"]);

                        // Truncar descripción
                        if (descripcion.Length > 80)
                            descripcion = descripcion.Substring(0, 80) + "...";

                        // Íconos para imagen y video
                        string imagenIcono = !string.IsNullOrEmpty(imagen) ?
                            "<i class='fas fa-image' style='color: #3498db;'></i>" : "-";

                        string videoIcono = !string.IsNullOrEmpty(video) ?
                            "<i class='fas fa-video' style='color: #e74c3c;'></i>" : "-";

                        // Obtener roles (si tiene)
                        string rolesIds = "";
                        try
                        {
                            DataTable dtRoles = Int_Popup_BRL.SelectTable(new Int_Popup { Id_Popup = Convert.ToInt32(idPopup) }, 14);
                            if (dtRoles != null && dtRoles.Rows.Count > 0)
                            {
                                var roles = dtRoles.AsEnumerable().Select(r => r["Id_Rol"].ToString()).ToList();
                                rolesIds = string.Join(",", roles);
                            }
                        }
                        catch { }

                        // Formato de fechas para input date (yyyy-MM-dd)
                        string fechaInicioInput = fechaInicio.HasValue ? fechaInicio.Value.ToString("yyyy-MM-dd") : "";
                        string fechaFinInput = fechaFin.HasValue ? fechaFin.Value.ToString("yyyy-MM-dd") : "";

                        sb.Append("<tr>");
                        // Número consecutivo
                        sb.AppendFormat("<td class='text-center'>{0}</td>", contador);
                        // ID
                        sb.AppendFormat("<td>{0}</td>", idPopup);
                        // TITULO
                        sb.AppendFormat("<td>{0}</td>", System.Web.HttpUtility.HtmlEncode(titulo));
                        // DESCRIPCION
                        sb.AppendFormat("<td>{0}</td>", System.Web.HttpUtility.HtmlEncode(descripcion));
                        // Imagen
                        sb.AppendFormat("<td class='text-center'>{0}</td>", imagenIcono);
                        // Video
                        sb.AppendFormat("<td class='text-center'>{0}</td>", videoIcono);
                        // Tiempo
                        sb.AppendFormat("<td class='text-center'>{0}</td>", tiempo);
                        // Fecha Inicio
                        sb.AppendFormat("<td>{0}</td>", fechaInicio.HasValue ? fechaInicio.Value.ToString("dd/MM/yyyy") : "-");
                        // Fecha Fin
                        sb.AppendFormat("<td>{0}</td>", fechaFin.HasValue ? fechaFin.Value.ToString("dd/MM/yyyy") : "-");
                        // ACCION - Radio button con datos ocultos
                        sb.Append("<td class='text-center'>");
                        sb.AppendFormat("<input type='radio' name='rd_popup' value='{0}' ", idPopup);
                        sb.AppendFormat("data-titulo='{0}' ", System.Web.HttpUtility.HtmlEncode(titulo));
                        sb.AppendFormat("data-descripcion='{0}' ", System.Web.HttpUtility.HtmlEncode(row["Descripcion"]?.ToString() ?? ""));
                        sb.AppendFormat("data-imagen='{0}' ", System.Web.HttpUtility.HtmlEncode(imagen));
                        sb.AppendFormat("data-video='{0}' ", System.Web.HttpUtility.HtmlEncode(video));
                        sb.AppendFormat("data-url='{0}' ", System.Web.HttpUtility.HtmlEncode(url));
                        sb.AppendFormat("data-tiempo='{0}' ", tiempo);
                        sb.AppendFormat("data-fecha-inicio='{0}' ", fechaInicioInput);
                        sb.AppendFormat("data-fecha-fin='{0}' ", fechaFinInput);
                        sb.AppendFormat("data-roles='{0}' ", rolesIds);
                        sb.AppendFormat("data-estado='{0}' ", estado ? "1" : "0");
                        sb.Append("/>");
                        sb.Append("</td>");
                        sb.Append("</tr>");

                        contador++;
                    }

                    sb.Append("</tbody></table>");
                    lit_tabla_popups.Text = sb.ToString();
                }
                else
                {
                    lit_tabla_popups.Text = "<p style='text-align: center; padding: 20px;'>No hay popups registrados</p>";
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al cargar popups: " + ex.Message, false, lbl_mensaje);
            }
        }

        /// <summary>
        /// Carga la lista de roles disponibles
        /// </summary>
        private void CargarRoles()
        {
            try
            {
                // Action 15: Listar todos los roles
                DataTable dt = Int_Popup_BRL.SelectTable(new Int_Popup(), 15);

                cbl_roles.Items.Clear();
                cbl_roles_edit.Items.Clear();

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        ListItem item = new ListItem
                        {
                            Text = row["Nombre_Rol"].ToString(),
                            Value = row["Id_Rol"].ToString()
                        };
                        cbl_roles.Items.Add(item);
                        cbl_roles_edit.Items.Add(new ListItem(item.Text, item.Value));
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al cargar roles: " + ex.Message, false, lbl_mensaje);
            }
        }

        #endregion

        #region Eventos de Botones

        /// <summary>
        /// Guardar nuevo popup
        /// </summary>
        protected void btn_guardar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar campos requeridos
                if (string.IsNullOrWhiteSpace(txt_titulo.Text))
                {
                    MostrarMensaje("El título es obligatorio", false, lbl_mensaje);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txt_descripcion.Text))
                {
                    MostrarMensaje("La descripción es obligatoria", false, lbl_mensaje);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txt_fecha_inicio.Text))
                {
                    MostrarMensaje("La fecha de inicio es obligatoria", false, lbl_mensaje);
                    return;
                }

                // Obtener ID de usuario de sesión
                int idUsuario = Session["Id_Usuario"] != null
                    ? Convert.ToInt32(Session["Id_Usuario"])
                    : 0;

                if (idUsuario == 0)
                {
                    MostrarMensaje("Error: No se pudo obtener el usuario de la sesión", false, lbl_mensaje);
                    return;
                }

                // Construir objeto popup
                Int_Popup popup = new Int_Popup
                {
                    Titulo = txt_titulo.Text.Trim(),
                    Descripcion = txt_descripcion.Text.Trim(),
                    Url = txt_url.Text.Trim(),
                    Tiempo_Visualizacion = string.IsNullOrWhiteSpace(txt_tiempo.Text) ? 5 : Convert.ToInt32(txt_tiempo.Text),
                    Fecha_Inicio = Convert.ToDateTime(txt_fecha_inicio.Text),
                    Fecha_Fin = string.IsNullOrWhiteSpace(txt_fecha_fin.Text) ? (DateTime?)null : Convert.ToDateTime(txt_fecha_fin.Text),
                    Estado = true, // Nuevo popup siempre activo
                    Id_Usuario = idUsuario
                };

                // Procesar imagen si se subió
                if (fud_imagen.HasFile)
                {
                    string rutaImagen = GuardarArchivo(fud_imagen, "Imagenes", idUsuario);
                    if (!string.IsNullOrEmpty(rutaImagen))
                    {
                        popup.Imagen = rutaImagen;
                    }
                }

                // Procesar video si se subió
                if (fud_video.HasFile)
                {
                    string rutaVideo = GuardarArchivo(fud_video, "Videos", idUsuario);
                    if (!string.IsNullOrEmpty(rutaVideo))
                    {
                        popup.Video = rutaVideo;
                    }
                }

                // Obtener roles seleccionados
                string rolesIds = ObtenerRolesSeleccionados(cbl_roles);
                popup.RolesIds = rolesIds;

                // Insertar popup con roles
                int resultado = Int_Popup_BRL.InsertarPopupConRoles(popup);

                if (resultado > 0)
                {
                    MostrarMensaje("Popup creado exitosamente", true, lbl_mensaje);
                    LimpiarFormulario();
                    CargarTablaPopups();

                    // Ejecutar JavaScript para cerrar modal
                    ScriptManager.RegisterStartupScript(this, GetType(), "CerrarModal",
                        "setTimeout(function(){ $('.modal-i-gl').removeClass('modal-i-gl-show').addClass('modal-i-gl-hide'); }, 1500);", true);
                }
                else
                {
                    MostrarMensaje("Error al guardar el popup", false, lbl_mensaje);
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error: " + ex.Message, false, lbl_mensaje);
            }
        }

        /// <summary>
        /// Actualizar popup existente
        /// </summary>
        protected void btn_actualizar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar que haya un ID de popup
                if (string.IsNullOrWhiteSpace(hf_id_popup.Value) || hf_id_popup.Value == "0")
                {
                    MostrarMensaje("Error: No se ha seleccionado un popup", false, lbl_mensaje_edit);
                    return;
                }

                int idPopup = Convert.ToInt32(hf_id_popup.Value);

                // Obtener ID de usuario de sesión
                int idUsuario = Session["Id_Usuario"] != null
                    ? Convert.ToInt32(Session["Id_Usuario"])
                    : 0;

                if (idUsuario == 0)
                {
                    MostrarMensaje("Error: No se pudo obtener el usuario de la sesión", false, lbl_mensaje_edit);
                    return;
                }

                // Construir objeto popup
                Int_Popup popup = new Int_Popup
                {
                    Id_Popup = idPopup,
                    Titulo = txt_titulo_edit.Text.Trim(),
                    Descripcion = txt_descripcion_edit.Text.Trim(),
                    Url = txt_url_edit.Text.Trim(),
                    Tiempo_Visualizacion = string.IsNullOrWhiteSpace(txt_tiempo_edit.Text) ? 5 : Convert.ToInt32(txt_tiempo_edit.Text),
                    Fecha_Inicio = Convert.ToDateTime(txt_fecha_inicio_edit.Text),
                    Fecha_Fin = string.IsNullOrWhiteSpace(txt_fecha_fin_edit.Text) ? (DateTime?)null : Convert.ToDateTime(txt_fecha_fin_edit.Text),
                    Estado = ddl_estado_edit.SelectedValue == "1",
                    Id_Usuario = idUsuario
                };

                // Mantener archivos actuales por defecto
                popup.Imagen = hf_imagen_actual.Value;
                popup.Video = hf_video_actual.Value;

                // Procesar nueva imagen si se subió
                if (fud_imagen_edit.HasFile)
                {
                    string rutaImagen = GuardarArchivo(fud_imagen_edit, "Imagenes", idUsuario);
                    if (!string.IsNullOrEmpty(rutaImagen))
                    {
                        popup.Imagen = rutaImagen;
                    }
                }

                // Procesar nuevo video si se subió
                if (fud_video_edit.HasFile)
                {
                    string rutaVideo = GuardarArchivo(fud_video_edit, "Videos", idUsuario);
                    if (!string.IsNullOrEmpty(rutaVideo))
                    {
                        popup.Video = rutaVideo;
                    }
                }

                // Obtener roles seleccionados
                string rolesIds = ObtenerRolesSeleccionados(cbl_roles_edit);
                popup.RolesIds = rolesIds;

                // Actualizar popup con roles
                int resultado = Int_Popup_BRL.ActualizarPopupConRoles(popup);

                if (resultado > 0)
                {
                    MostrarMensaje("Popup actualizado exitosamente", true, lbl_mensaje_edit);
                    CargarTablaPopups();

                    // Ejecutar JavaScript para cerrar modal
                    ScriptManager.RegisterStartupScript(this, GetType(), "CerrarModal",
                        "setTimeout(function(){ $('.modal-i-gl').removeClass('modal-i-gl-show').addClass('modal-i-gl-hide'); }, 1500);", true);
                }
                else
                {
                    MostrarMensaje("Error al actualizar el popup", false, lbl_mensaje_edit);
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error: " + ex.Message, false, lbl_mensaje_edit);
            }
        }

        /// <summary>
        /// Eliminar popup (cambiar estado a inactivo)
        /// </summary>
        protected void btn_eliminar_Click(object sender, EventArgs e)
        {
            try
            {
                int idPopup = Convert.ToInt32(hf_id_popup_eliminar.Value);

                // Obtener ID de usuario de sesión
                int idUsuario = Session["Id_Usuario"] != null
                    ? Convert.ToInt32(Session["Id_Usuario"])
                    : 0;

                if (idUsuario == 0)
                {
                    MostrarMensaje("Error: No se pudo obtener el usuario de la sesión", false, lbl_mensaje);
                    return;
                }

                // Cambiar estado a inactivo (Action 6)
                Int_Popup popup = new Int_Popup
                {
                    Id_Popup = idPopup,
                    Estado = false,
                    Id_Usuario = idUsuario
                };

                int resultado = Int_Popup_BRL.InsertOrUpdate(popup, 6);

                if (resultado > 0)
                {
                    MostrarMensaje("Popup eliminado exitosamente", true, lbl_mensaje);
                    CargarTablaPopups();

                    // Ejecutar JavaScript para cerrar modal
                    ScriptManager.RegisterStartupScript(this, GetType(), "CerrarModal",
                        "setTimeout(function(){ $('.modal-i-gl').removeClass('modal-i-gl-show').addClass('modal-i-gl-hide'); }, 1500);", true);
                }
                else
                {
                    MostrarMensaje("Error al eliminar el popup", false, lbl_mensaje);
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error: " + ex.Message, false, lbl_mensaje);
            }
        }

        #endregion

        #region Métodos Auxiliares

        /// <summary>
        /// Guardar archivo (imagen o video) usando TratamientoPopups
        /// </summary>
        private string GuardarArchivo(FileUpload fileUpload, string carpeta, int idUsuario)
        {
            AG_Utils utilidades = new AG_Utils();

            try
            {
                if (!fileUpload.HasFile)
                    return null;

                // Validar tamaño
                long maxSize = carpeta == "Imagenes" ? 3 * 1024 * 1024 : 50 * 1024 * 1024;

                if (fileUpload.PostedFile.ContentLength > maxSize)
                {
                    MostrarMensaje($"El archivo es demasiado grande. Máximo: {(carpeta == "Imagenes" ? "3MB" : "50MB")}", false, lbl_mensaje);
                    return null;
                }

                // Obtener ambiente
                string ambiente = ConfigurationManager.AppSettings["ambiente"] ?? "DESA";

                // Obtener rutas usando ObtenerRutasPopups
                var rutas = utilidades.ObtenerRutasPopups(ambiente, carpeta);
                string rutaLocal = rutas.rutaPopupsLocal;
                string rutaRemota = rutas.rutaPopupsRemoto;

                if (string.IsNullOrEmpty(rutaLocal) || string.IsNullOrEmpty(rutaRemota))
                {
                    MostrarMensaje("Error: No se pudieron obtener las rutas de almacenamiento", false, lbl_mensaje);
                    return null;
                }

                // Preparar nombre de archivo normalizado
                string extension = Path.GetExtension(fileUpload.FileName);
                string nombreSinExtension = Path.GetFileNameWithoutExtension(fileUpload.FileName);

                // Normalizar nombre
                string nombreNormalizado = System.Text.RegularExpressions.Regex.Replace(
                    nombreSinExtension.Normalize(NormalizationForm.FormD),
                    @"[^a-zA-z0-9 ]+", "");
                nombreNormalizado = nombreNormalizado.Replace(" ", "_");

                // Usar timestamp como consecutivo
                string consecutivo = DateTime.Now.Ticks.ToString();

                // Nombre final
                string nombreFinal = $"{consecutivo}-{nombreNormalizado}{extension}";

                // Ruta del log
                string pathLog = Server.MapPath("~/Logs");

                // Llamar al método TratamientoPopups
                var resultado = utilidades.TratamientoPopups(
                    nombreFinal,
                    consecutivo,
                    rutaLocal,
                    rutaRemota,
                    fileUpload,
                    idUsuario.ToString(),
                    pathLog
                );

                // Verificar si se guardó correctamente
                if (resultado.bl_GuardaArchivoRemoto)
                {
                    return resultado.rutaArchivoRemoto;
                }
                else if (resultado.bl_GuardaArchivoLocal)
                {
                    MostrarMensaje("Advertencia: Solo se guardó localmente", false, lbl_mensaje);
                    return resultado.rutaArchivoRemoto;
                }
                else
                {
                    MostrarMensaje("Error: No se pudo guardar el archivo", false, lbl_mensaje);
                    return null;
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al guardar archivo: " + ex.Message, false, lbl_mensaje);
                return null;
            }
        }

        /// <summary>
        /// Obtener IDs de roles seleccionados separados por coma
        /// </summary>
        private string ObtenerRolesSeleccionados(CheckBoxList checkBoxList)
        {
            var rolesSeleccionados = checkBoxList.Items
                .Cast<ListItem>()
                .Where(item => item.Selected)
                .Select(item => item.Value)
                .ToList();

            return rolesSeleccionados.Count > 0 ? string.Join(",", rolesSeleccionados) : null;
        }

        /// <summary>
        /// Limpiar formulario de crear
        /// </summary>
        private void LimpiarFormulario()
        {
            txt_titulo.Text = "";
            txt_descripcion.Text = "";
            txt_url.Text = "";
            txt_tiempo.Text = "5";
            txt_fecha_inicio.Text = "";
            txt_fecha_fin.Text = "";

            // Desmarcar roles
            foreach (ListItem item in cbl_roles.Items)
            {
                item.Selected = false;
            }
        }

        /// <summary>
        /// Mostrar mensaje
        /// </summary>
        private void MostrarMensaje(string mensaje, bool esExito, Label label)
        {
            if (label != null)
            {
                label.Text = mensaje;
                label.CssClass = esExito ? "msg-success" : "msg-error";
                label.Visible = true;
            }
        }

        #endregion
    }
}