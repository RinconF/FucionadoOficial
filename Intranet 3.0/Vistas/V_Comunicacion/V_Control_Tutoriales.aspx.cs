using BRL;
using DCL;
using Intranet_3._0.Interna;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Intranet_3._0.Vistas.V_Comunicacion
{
    public partial class V_Control_Tutoriales : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarRolesDisponibles();
                CargarTutoriales();
            }
        }

        // =============================================
        // NUEVO: Cargar lista de roles disponibles
        // =============================================
        private void CargarRolesDisponibles()
        {
            try
            {
                Int_Tutoriales obj = new Int_Tutoriales();
                // Action 10: SELECT ROLES - Obtener todos los roles activos
                DataTable dtRoles = Int_Tutoriales_BRL.SelectTable(obj, 10);

                if (dtRoles != null && dtRoles.Rows.Count > 0)
                {
                    // Cargar CheckBoxList para modal CREAR
                    cbl_roles.DataSource = dtRoles;
                    cbl_roles.DataTextField = "Nombre_Rol";
                    cbl_roles.DataValueField = "Id_Rol";
                    cbl_roles.DataBind();

                    // Cargar CheckBoxList para modal EDITAR
                    cbl_roles_edit.DataSource = dtRoles;
                    cbl_roles_edit.DataTextField = "Nombre_Rol";
                    cbl_roles_edit.DataValueField = "Id_Rol";
                    cbl_roles_edit.DataBind();
                }
            }
            catch (Exception ex)
            {
                // Log del error si es necesario
                lbl_mensaje.Text = "Error al cargar roles: " + ex.Message;
                lbl_mensaje.CssClass = "msg-error";
            }
        }

        // =============================================
        // ACTUALIZADO: Cargar tutoriales CON ROLES
        // =============================================
        private void CargarTutoriales()
        {
            try
            {
                Int_Tutoriales obj = new Int_Tutoriales();
                // Action 0: SELECT ALL - Lista todos los tutoriales con roles asignados
                Int_TutorialesCollection tutoriales = Int_Tutoriales_BRL.SelectByParams(obj, 0);

                if (tutoriales != null && tutoriales.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("<table class='tbl_vistas_general table table-striped table-hover'>");
                    sb.Append("<thead><tr>");
                    sb.Append("<th style='width: 50px;'>#</th>");
                    sb.Append("<th style='width: 60px;'>ID</th>");
                    sb.Append("<th>TITULO</th>");
                    sb.Append("<th>DESCRIPCION</th>");
                    sb.Append("<th style='width: 80px;'>Video</th>");
                    sb.Append("<th style='width: 150px;'>Sección</th>");
                    sb.Append("<th style='width: 200px;'>Roles</th>"); // ← NUEVA COLUMNA
                    sb.Append("<th style='width: 180px;'>FECHA DE CREACION</th>");
                    sb.Append("<th style='width: 80px;'>ACCION</th>");
                    sb.Append("</tr></thead><tbody>");

                    int contador = 1;
                    foreach (Int_Tutoriales tutorial in tutoriales)
                    {
                        string descripcion = tutorial.Descripcion ?? "";
                        if (descripcion.Length > 80)
                            descripcion = descripcion.Substring(0, 80) + "...";

                        string videoIcono = !string.IsNullOrEmpty(tutorial.Video) ?
                            "<a href='" + ResolveUrl(tutorial.Video) + "' target='_blank' title='Ver video' style='color: #e74c3c;'><i class='fas fa-play'></i></a>" :
                            "-";

                        // ================================
                        // NUEVO: Mostrar roles asignados
                        // ================================
                        string rolesHtml = "";
                        if (!string.IsNullOrEmpty(tutorial.Roles_Asignados))
                        {
                            string[] roles = tutorial.Roles_Asignados.Split(',');
                            foreach (string rol in roles)
                            {
                                rolesHtml += $"<span class='roles-badge'>{HttpUtility.HtmlEncode(rol.Trim())}</span>";
                            }
                        }
                        else
                        {
                            rolesHtml = "<span style='color: red;'>Sin roles</span>";
                        }

                        sb.Append("<tr>");
                        sb.AppendFormat("<td class='text-center'>{0}</td>", contador);
                        sb.AppendFormat("<td>{0}</td>", tutorial.Id_Tutorial);
                        sb.AppendFormat("<td>{0}</td>", HttpUtility.HtmlEncode(tutorial.Titulo));
                        sb.AppendFormat("<td>{0}</td>", HttpUtility.HtmlEncode(descripcion));
                        sb.AppendFormat("<td class='text-center'>{0}</td>", videoIcono);
                        sb.AppendFormat("<td>{0}</td>", HttpUtility.HtmlEncode(tutorial.Seccion ?? ""));
                        sb.AppendFormat("<td>{0}</td>", rolesHtml); // ← NUEVA COLUMNA
                        sb.AppendFormat("<td>{0}</td>", tutorial.Fecha_Creacion?.ToString("dd/MM/yyyy h:mm:ss tt") ?? "");

                        // ================================
                        // ACTUALIZADO: Agregar data-roles
                        // ================================
                        sb.Append("<td class='text-center'>");
                        sb.AppendFormat("<input type='radio' name='rd_tutorial' value='{0}' ", tutorial.Id_Tutorial);
                        sb.AppendFormat("data-titulo='{0}' ", HttpUtility.HtmlEncode(tutorial.Titulo));
                        sb.AppendFormat("data-descripcion='{0}' ", HttpUtility.HtmlEncode(tutorial.Descripcion ?? ""));
                        sb.AppendFormat("data-video='{0}' ", HttpUtility.HtmlEncode(tutorial.Video ?? ""));
                        sb.AppendFormat("data-seccion='{0}' ", HttpUtility.HtmlEncode(tutorial.Seccion ?? ""));
                        sb.AppendFormat("data-estado='{0}' ", tutorial.Estado == true ? "1" : "0");

                        // NUEVO: Guardar IDs de roles (separados por coma) para JavaScript
                        string rolesIds = ObtenerIdsRoles(tutorial.Id_Tutorial.Value);
                        sb.AppendFormat("data-roles='{0}' ", HttpUtility.HtmlEncode(rolesIds));

                        sb.Append("/>");
                        sb.Append("</td>");
                        sb.Append("</tr>");

                        contador++;
                    }

                    sb.Append("</tbody></table>");
                    lit_tabla_tutoriales.Text = sb.ToString();
                }
                else
                {
                    lit_tabla_tutoriales.Text = "<p class='text-center' style='padding: 30px;'>No hay tutoriales registrados.</p>";
                }
            }
            catch (Exception ex)
            {
                lit_tabla_tutoriales.Text = "<p class='msg-error'>Error al cargar tutoriales: " + ex.Message + "</p>";
            }
        }

        // =============================================
        // NUEVO: Obtener IDs de roles de un tutorial
        // =============================================
        private string ObtenerIdsRoles(int idTutorial)
        {
            try
            {
                Int_Tutoriales obj = new Int_Tutoriales { Id_Tutorial = idTutorial };

                // Action 11: SELECT ROLES IDS - Retorna solo los IDs de roles
                DataTable dtRoles = Int_Tutoriales_BRL.SelectTable(obj, 11);

                if (dtRoles != null && dtRoles.Rows.Count > 0)
                {
                    List<string> rolesIds = new List<string>();
                    foreach (DataRow row in dtRoles.Rows)
                    {
                        rolesIds.Add(row["Id_Rol"].ToString());
                    }
                    return string.Join(",", rolesIds);
                }
            }
            catch
            {
                // Si hay error, retornar vacío
            }
            return "";
        }

        // =============================================
        // ACTUALIZADO: Guardar tutorial CON ROLES
        // =============================================
        protected void btn_guardar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validaciones existentes
                if (string.IsNullOrWhiteSpace(txt_titulo.Text))
                {
                    lbl_mensaje.Text = "El título es obligatorio.";
                    lbl_mensaje.CssClass = "msg-error";
                    return;
                }

                if (string.IsNullOrWhiteSpace(txt_descripcion.Text))
                {
                    lbl_mensaje.Text = "La descripción es obligatoria.";
                    lbl_mensaje.CssClass = "msg-error";
                    return;
                }

                if (ddl_seccion.SelectedValue == "")
                {
                    lbl_mensaje.Text = "Debe seleccionar una sección.";
                    lbl_mensaje.CssClass = "msg-error";
                    return;
                }

                if (!fud_video.HasFile)
                {
                    lbl_mensaje.Text = "Debe seleccionar un video.";
                    lbl_mensaje.CssClass = "msg-error";
                    return;
                }

                // ================================
                // NUEVA VALIDACIÓN: Roles
                // ================================
                List<string> rolesSeleccionados = ObtenerRolesSeleccionados(cbl_roles);
                if (rolesSeleccionados.Count == 0)
                {
                    lbl_mensaje.Text = "Debe seleccionar al menos un rol.";
                    lbl_mensaje.CssClass = "msg-error";
                    return;
                }

                // Crear objeto
                Int_Tutoriales obj = new Int_Tutoriales
                {
                    Titulo = txt_titulo.Text.Trim(),
                    Descripcion = txt_descripcion.Text.Trim(),
                    Seccion = ddl_seccion.SelectedValue,
                    Usuario_Creacion = ObtenerIdUsuarioActual(),
                    Estado = true
                };

                // Guardar video físico
                string rutaVideo = GuardarVideo(fud_video);
                if (!string.IsNullOrEmpty(rutaVideo))
                {
                    obj.Video = rutaVideo;
                }
                else
                {
                    lbl_mensaje.Text = "Error al guardar el video.";
                    lbl_mensaje.CssClass = "msg-error";
                    return;
                }

                // Action 3: INSERT - Insertar nuevo tutorial
                int idNuevo = Int_Tutoriales_BRL.InsertOrUpdate(obj, 3);

                if (idNuevo > 0)
                {
                    // ================================
                    // NUEVO: Asignar roles al tutorial
                    // ================================
                    Int_Tutoriales objRoles = new Int_Tutoriales
                    {
                        Id_Tutorial = idNuevo,
                        Roles = string.Join(",", rolesSeleccionados) // "1,2,3"
                    };

                    // Action 7: ASIGNAR ROLES
                    int resultadoRoles = Int_Tutoriales_BRL.InsertOrUpdate(objRoles, 7);

                    if (resultadoRoles > 0)
                    {
                        // Limpiar campos
                        txt_titulo.Text = "";
                        txt_descripcion.Text = "";
                        ddl_seccion.SelectedIndex = 0;
                        cbl_roles.ClearSelection();

                        lbl_mensaje.Text = "Tutorial creado y roles asignados exitosamente.";
                        lbl_mensaje.CssClass = "msg-success";

                        // Recargar tabla
                        CargarTutoriales();
                    }
                    else
                    {
                        lbl_mensaje.Text = "Tutorial creado pero hubo un error al asignar roles.";
                        lbl_mensaje.CssClass = "msg-warning";
                    }
                }
                else
                {
                    lbl_mensaje.Text = "Error al guardar el tutorial.";
                    lbl_mensaje.CssClass = "msg-error";
                }
            }
            catch (Exception ex)
            {
                lbl_mensaje.Text = "Error: " + ex.Message;
                lbl_mensaje.CssClass = "msg-error";
            }
        }

        // =============================================
        // ACTUALIZADO: Actualizar tutorial CON ROLES
        // =============================================
        protected void btn_actualizar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validaciones
                if (string.IsNullOrWhiteSpace(hf_id_tutorial.Value))
                {
                    lbl_mensaje_edit.Text = "No se encontró el identificador del tutorial.";
                    lbl_mensaje_edit.CssClass = "msg-error";
                    return;
                }

                if (string.IsNullOrWhiteSpace(txt_titulo_edit.Text))
                {
                    lbl_mensaje_edit.Text = "El título es obligatorio.";
                    lbl_mensaje_edit.CssClass = "msg-error";
                    return;
                }

                if (string.IsNullOrWhiteSpace(txt_descripcion_edit.Text))
                {
                    lbl_mensaje_edit.Text = "La descripción es obligatoria.";
                    lbl_mensaje_edit.CssClass = "msg-error";
                    return;
                }

                if (ddl_seccion_edit.SelectedValue == "")
                {
                    lbl_mensaje_edit.Text = "Debe seleccionar una sección.";
                    lbl_mensaje_edit.CssClass = "msg-error";
                    return;
                }

                // ================================
                // NUEVA VALIDACIÓN: Roles
                // ================================
                List<string> rolesSeleccionados = ObtenerRolesSeleccionados(cbl_roles_edit);
                if (rolesSeleccionados.Count == 0)
                {
                    lbl_mensaje_edit.Text = "Debe seleccionar al menos un rol.";
                    lbl_mensaje_edit.CssClass = "msg-error";
                    return;
                }

                int idTutorial = Convert.ToInt32(hf_id_tutorial.Value);

                // Crear objeto para actualizar
                Int_Tutoriales obj = new Int_Tutoriales
                {
                    Id_Tutorial = idTutorial,
                    Titulo = txt_titulo_edit.Text.Trim(),
                    Descripcion = txt_descripcion_edit.Text.Trim(),
                    Seccion = ddl_seccion_edit.SelectedValue,
                    Usuario_Actualizacion = ObtenerIdUsuarioActual(),
                    Estado = ddl_estado_edit.SelectedValue == "1"
                };

                // Verificar si hay nuevo video
                if (fud_video_edit.HasFile)
                {
                    // Eliminar video anterior si existe
                    if (!string.IsNullOrEmpty(hf_video_actual.Value) && hf_video_actual.Value != "Sin video")
                    {
                        EliminarArchivoFisico(hf_video_actual.Value);
                    }

                    // Guardar nuevo video
                    string rutaVideo = GuardarVideo(fud_video_edit);
                    if (!string.IsNullOrEmpty(rutaVideo))
                    {
                        obj.Video = rutaVideo;
                    }
                    else
                    {
                        lbl_mensaje_edit.Text = "Error al guardar el nuevo video.";
                        lbl_mensaje_edit.CssClass = "msg-error";
                        return;
                    }
                }
                else
                {
                    // Mantener video actual
                    obj.Video = hf_video_actual.Value;
                }

                // Action 4: UPDATE - Actualizar tutorial
                int resultado = Int_Tutoriales_BRL.InsertOrUpdate(obj, 4);

                if (resultado > 0)
                {
                    // ================================
                    // NUEVO: Reemplazar roles del tutorial
                    // ================================
                    Int_Tutoriales objRoles = new Int_Tutoriales
                    {
                        Id_Tutorial = idTutorial,
                        Roles = string.Join(",", rolesSeleccionados) // "1,2,3"
                    };

                    // Action 9: REEMPLAZAR TODOS LOS ROLES
                    int resultadoRoles = Int_Tutoriales_BRL.InsertOrUpdate(objRoles, 9);

                    if (resultadoRoles > 0)
                    {
                        lbl_mensaje_edit.Text = "Tutorial y roles actualizados exitosamente.";
                        lbl_mensaje_edit.CssClass = "msg-success";

                        // Recargar tabla
                        CargarTutoriales();
                    }
                    else
                    {
                        lbl_mensaje_edit.Text = "Tutorial actualizado pero hubo un error al actualizar roles.";
                        lbl_mensaje_edit.CssClass = "msg-warning";
                    }
                }
                else
                {
                    lbl_mensaje_edit.Text = "Error al actualizar el tutorial.";
                    lbl_mensaje_edit.CssClass = "msg-error";
                }
            }
            catch (Exception ex)
            {
                lbl_mensaje_edit.Text = "Error: " + ex.Message;
                lbl_mensaje_edit.CssClass = "msg-error";
            }
        }

        protected void btn_eliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(hf_id_tutorial_eliminar.Value))
                {
                    CargarTutoriales();
                    return;
                }

                int idTutorial = Convert.ToInt32(hf_id_tutorial_eliminar.Value);

                // Primero obtener el tutorial para eliminar el video físico
                Int_Tutoriales objBuscar = new Int_Tutoriales { Id_Tutorial = idTutorial };
                Int_TutorialesCollection tutoriales = Int_Tutoriales_BRL.SelectByParams(objBuscar, 2);

                if (tutoriales != null && tutoriales.Count > 0)
                {
                    Int_Tutoriales tutorial = tutoriales[0];

                    // Crear objeto para eliminación lógica
                    Int_Tutoriales objEliminar = new Int_Tutoriales
                    {
                        Id_Tutorial = idTutorial,
                        Usuario_Actualizacion = ObtenerIdUsuarioActual()
                    };

                    // Action 5: DELETE - Eliminación lógica (cambia Estado a 0)
                    int resultado = Int_Tutoriales_BRL.InsertOrUpdate(objEliminar, 5);

                    if (resultado > 0)
                    {
                        // Eliminar video físico si existe
                        if (!string.IsNullOrEmpty(tutorial.Video))
                        {
                            EliminarArchivoFisico(tutorial.Video);
                        }

                        // NOTA: Los roles se eliminan automáticamente por CASCADE DELETE
                    }
                }

                // Recargar tabla
                CargarTutoriales();
            }
            catch (Exception ex)
            {
                lit_tabla_tutoriales.Text = "<p class='msg-error'>Error al eliminar: " + ex.Message + "</p>";
            }
        }

        // =============================================
        // NUEVO: Obtener roles seleccionados
        // =============================================
        private List<string> ObtenerRolesSeleccionados(CheckBoxList cbl)
        {
            List<string> roles = new List<string>();
            foreach (ListItem item in cbl.Items)
            {
                if (item.Selected)
                {
                    roles.Add(item.Value);
                }
            }
            return roles;
        }

        // =============================================
        // Métodos auxiliares (sin cambios)
        // =============================================
        private string GuardarVideo(System.Web.UI.WebControls.FileUpload fileUpload)
        {
            try
            {
                if (!fileUpload.HasFile)
                    return null;

                string extension = Path.GetExtension(fileUpload.FileName).ToLower();
                string[] extensionesPermitidas = { ".mp4", ".avi", ".mov", ".wmv", ".flv", ".mkv", ".webm" };

                if (!extensionesPermitidas.Contains(extension))
                {
                    return null;
                }

                // Tamaño máximo: 50 MB
                if (fileUpload.PostedFile.ContentLength > 50 * 1024 * 1024)
                {
                    return null;
                }

                // Obtener rutas desde configuración
                string[] rutas = AG_Utils.ObtenerRutasVideos();
                string rutaLocal = rutas[0];
                string carpetaFisica = Server.MapPath(rutaLocal);

                // Crear carpeta si no existe
                if (!Directory.Exists(carpetaFisica))
                {
                    Directory.CreateDirectory(carpetaFisica);
                }

                // Generar nombre único para el archivo
                string nombreArchivo = "video_" + DateTime.Now.ToString("yyyyMMddHHmmss") +
                                      "_" + Guid.NewGuid().ToString("N").Substring(0, 8) + extension;

                string rutaCompleta = Path.Combine(carpetaFisica, nombreArchivo);
                fileUpload.SaveAs(rutaCompleta);

                // Retornar ruta relativa
                return rutaLocal + nombreArchivo;
            }
            catch (Exception ex)
            {
                // Log del error si es necesario
                return null;
            }
        }

        private void EliminarArchivoFisico(string rutaArchivo)
        {
            try
            {
                if (!string.IsNullOrEmpty(rutaArchivo))
                {
                    string rutaCompleta = Server.MapPath(rutaArchivo);
                    if (File.Exists(rutaCompleta))
                    {
                        File.Delete(rutaCompleta);
                    }
                }
            }
            catch
            {
                // Ignorar errores al eliminar archivo físico
            }
        }

        private int ObtenerIdUsuarioActual()
        {
            if (Session["Id_Usuario"] != null)
            {
                return Convert.ToInt32(Session["Id_Usuario"]);
            }
            return 1; // Usuario por defecto
        }

        private string ResolveUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
                return "#";

            if (url.StartsWith("~"))
                return VirtualPathUtility.ToAbsolute(url);

            return url;
        }
    }
}