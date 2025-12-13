using BRL;
using DCL;
using Intranet_3._0.Interna;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;

namespace Intranet_3._0.Vistas.V_Comunicacion
{
    public partial class V_Control_Tutoriales : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarTutoriales();
                ProcesarAccionesURL();
            }
        }

        private void CargarTutoriales()
        {
            try
            {
                Int_Tutoriales obj = new Int_Tutoriales();
                Int_TutorialesCollection tutoriales = Int_Tutoriales_BRL.SelectByParams(obj, 0);

                if (tutoriales != null && tutoriales.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("<table class='tutorial-table'>");
                    sb.Append("<thead><tr>");
                    sb.Append("<th>ID</th>");
                    sb.Append("<th>TÍTULO</th>");
                    sb.Append("<th>DESCRIPCIÓN</th>");
                    sb.Append("<th>VIDEO</th>");
                    sb.Append("<th>SECCIÓN</th>");
                    sb.Append("<th>FECHA</th>");
                    sb.Append("<th>ESTADO</th>");
                    sb.Append("<th>ACCIONES</th>");
                    sb.Append("</tr></thead><tbody>");

                    foreach (Int_Tutoriales tutorial in tutoriales)
                    {
                        string descripcion = tutorial.Descripcion ?? "";
                        if (descripcion.Length > 100)
                            descripcion = descripcion.Substring(0, 100) + "...";

                        string videoNombre = !string.IsNullOrEmpty(tutorial.Video) ?
                            Path.GetFileName(tutorial.Video) : "Sin video";

                        string estadoTexto = tutorial.Estado == true ? "Activo" : "Inactivo";
                        string estadoColor = tutorial.Estado == true ? "green" : "red";

                        sb.Append("<tr>");
                        sb.AppendFormat("<td>{0}</td>", tutorial.Id_Tutorial);
                        sb.AppendFormat("<td>{0}</td>", HttpUtility.HtmlEncode(tutorial.Titulo));
                        sb.AppendFormat("<td>{0}</td>", HttpUtility.HtmlEncode(descripcion));
                        sb.AppendFormat("<td>{0}</td>", HttpUtility.HtmlEncode(videoNombre));
                        sb.AppendFormat("<td>{0}</td>", HttpUtility.HtmlEncode(tutorial.Seccion));
                        sb.AppendFormat("<td>{0}</td>", tutorial.Fecha_Creacion?.ToString("dd/MM/yyyy HH:mm") ?? "");
                        sb.AppendFormat("<td style='color:{1}'>{0}</td>", estadoTexto, estadoColor);
                        sb.Append("<td>");
                        sb.AppendFormat("<button class='btn-action btn btn-info' onclick=\"location.href='?action=edit&id={0}'\">Editar</button>", tutorial.Id_Tutorial);
                        sb.AppendFormat("<button class='btn-action btn btn-danger' onclick=\"if(confirmarEliminacion()) location.href='?action=delete&id={0}'\">Eliminar</button>", tutorial.Id_Tutorial);
                        sb.Append("</td></tr>");
                    }

                    sb.Append("</tbody></table>");
                    lit_tabla_tutoriales.Text = sb.ToString();
                }
                else
                {
                    lit_tabla_tutoriales.Text = "<p>No hay tutoriales registrados.</p>";
                }
            }
            catch (Exception ex)
            {
                lit_tabla_tutoriales.Text = "<p class='msg-error'>Error: " + ex.Message + "</p>";
            }
        }

        private void ProcesarAccionesURL()
        {
            string action = Request.QueryString["action"];
            string idStr = Request.QueryString["id"];

            if (!string.IsNullOrEmpty(action) && !string.IsNullOrEmpty(idStr))
            {
                int id;
                if (int.TryParse(idStr, out id))
                {
                    if (action == "edit")
                    {
                        CargarTutorialParaEditar(id);
                    }
                    else if (action == "delete")
                    {
                        EliminarTutorial(id);
                    }
                }
            }
        }

        protected void btn_guardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txt_titulo.Text))
                {
                    lbl_mensaje.Text = "El título es obligatorio.";
                    return;
                }

                if (ddl_seccion.SelectedValue == "")
                {
                    lbl_mensaje.Text = "Debe seleccionar una sección.";
                    return;
                }

                Int_Tutoriales obj = new Int_Tutoriales
                {
                    Titulo = txt_titulo.Text.Trim(),
                    Descripcion = txt_descripcion.Text.Trim(),
                    Seccion = ddl_seccion.SelectedValue,
                    Usuario_Creacion = ObtenerIdUsuarioActual(),
                    Estado = true
                };

                if (fud_video.HasFile)
                {
                    string rutaVideo = GuardarVideo(fud_video);
                    if (!string.IsNullOrEmpty(rutaVideo))
                    {
                        obj.Video = rutaVideo;
                    }
                    else
                    {
                        lbl_mensaje.Text = "Error al guardar el video.";
                        return;
                    }
                }

                int resultado = Int_Tutoriales_BRL.InsertOrUpdate(obj, 3);

                if (resultado > 0)
                {
                    Response.Redirect(Request.RawUrl.Split('?')[0]);
                }
                else
                {
                    lbl_mensaje.Text = "Error al guardar el tutorial.";
                }
            }
            catch (Exception ex)
            {
                lbl_mensaje.Text = "Error: " + ex.Message;
            }
        }

        private void CargarTutorialParaEditar(int id)
        {
            try
            {
                Int_Tutoriales obj = new Int_Tutoriales { Id_Tutorial = id };
                Int_TutorialesCollection tutoriales = Int_Tutoriales_BRL.SelectByParams(obj, 2);

                if (tutoriales != null && tutoriales.Count > 0)
                {
                    Int_Tutoriales tutorial = tutoriales[0];

                    hf_id_tutorial.Value = tutorial.Id_Tutorial.ToString();
                    txt_titulo_edit.Text = tutorial.Titulo;
                    txt_descripcion_edit.Text = tutorial.Descripcion;
                    ddl_seccion_edit.SelectedValue = tutorial.Seccion;
                    ddl_estado_edit.SelectedValue = tutorial.Estado == true ? "1" : "0";

                    if (!string.IsNullOrEmpty(tutorial.Video))
                    {
                        hf_video_actual.Value = tutorial.Video;
                        lbl_video_actual.Text = Path.GetFileName(tutorial.Video);
                    }
                    else
                    {
                        hf_video_actual.Value = "";
                        lbl_video_actual.Text = "No hay video";
                    }
                }
            }
            catch (Exception ex)
            {
                lbl_mensaje_edit.Text = "Error: " + ex.Message;
            }
        }

        protected void btn_actualizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txt_titulo_edit.Text))
                {
                    lbl_mensaje_edit.Text = "El título es obligatorio.";
                    return;
                }

                if (ddl_seccion_edit.SelectedValue == "")
                {
                    lbl_mensaje_edit.Text = "Debe seleccionar una sección.";
                    return;
                }

                int idTutorial = Convert.ToInt32(hf_id_tutorial.Value);

                Int_Tutoriales obj = new Int_Tutoriales
                {
                    Id_Tutorial = idTutorial,
                    Titulo = txt_titulo_edit.Text.Trim(),
                    Descripcion = txt_descripcion_edit.Text.Trim(),
                    Seccion = ddl_seccion_edit.SelectedValue,
                    Usuario_Actualizacion = ObtenerIdUsuarioActual(),
                    Estado = ddl_estado_edit.SelectedValue == "1"
                };

                if (fud_video_edit.HasFile)
                {
                    if (!string.IsNullOrEmpty(hf_video_actual.Value))
                    {
                        EliminarArchivoFisico(hf_video_actual.Value);
                    }

                    string rutaVideo = GuardarVideo(fud_video_edit);
                    if (!string.IsNullOrEmpty(rutaVideo))
                    {
                        obj.Video = rutaVideo;
                    }
                    else
                    {
                        lbl_mensaje_edit.Text = "Error al guardar el video.";
                        return;
                    }
                }
                else
                {
                    obj.Video = hf_video_actual.Value;
                }

                int resultado = Int_Tutoriales_BRL.InsertOrUpdate(obj, 4);

                if (resultado > 0)
                {
                    Response.Redirect(Request.RawUrl.Split('?')[0]);
                }
                else
                {
                    lbl_mensaje_edit.Text = "Error al actualizar.";
                }
            }
            catch (Exception ex)
            {
                lbl_mensaje_edit.Text = "Error: " + ex.Message;
            }
        }

        private void EliminarTutorial(int id)
        {
            try
            {
                Int_Tutoriales objBuscar = new Int_Tutoriales { Id_Tutorial = id };
                Int_TutorialesCollection tutoriales = Int_Tutoriales_BRL.SelectByParams(objBuscar, 2);

                if (tutoriales != null && tutoriales.Count > 0)
                {
                    Int_Tutoriales tutorial = tutoriales[0];

                    Int_Tutoriales objEliminar = new Int_Tutoriales
                    {
                        Id_Tutorial = id,
                        Usuario_Actualizacion = ObtenerIdUsuarioActual()
                    };

                    int resultado = Int_Tutoriales_BRL.InsertOrUpdate(objEliminar, 5);

                    if (resultado > 0 && !string.IsNullOrEmpty(tutorial.Video))
                    {
                        EliminarArchivoFisico(tutorial.Video);
                    }
                }

                Response.Redirect(Request.RawUrl.Split('?')[0]);
            }
            catch (Exception ex)
            {
                lit_tabla_tutoriales.Text = "<p class='msg-error'>Error: " + ex.Message + "</p>";
            }
        }

        private string GuardarVideo(System.Web.UI.WebControls.FileUpload fileUpload)
        {
            try
            {
                if (!fileUpload.HasFile)
                    return null;

                string extension = Path.GetExtension(fileUpload.FileName).ToLower();
                string[] extensionesPermitidas = { ".mp4", ".avi", ".mov", ".wmv", ".flv", ".mkv", ".webm" };

                if (!extensionesPermitidas.Contains(extension))
                    return null;

                string[] rutas = AG_Utils.ObtenerRutasVideos();
                string rutaLocal = rutas[0];
                string carpetaFisica = Server.MapPath(rutaLocal);

                if (!Directory.Exists(carpetaFisica))
                {
                    Directory.CreateDirectory(carpetaFisica);
                }

                string nombreArchivo = "video_" + DateTime.Now.ToString("yyyyMMddHHmmss") +
                                      "_" + Guid.NewGuid().ToString("N").Substring(0, 8) + extension;

                string rutaCompleta = Path.Combine(carpetaFisica, nombreArchivo);
                fileUpload.SaveAs(rutaCompleta);

                return rutaLocal + nombreArchivo;
            }
            catch
            {
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
                // Ignorar errores al eliminar
            }
        }

        private int ObtenerIdUsuarioActual()
        {
            if (Session["Id_Usuario"] != null)
            {
                return Convert.ToInt32(Session["Id_Usuario"]);
            }
            return 1;
        }
    }
}