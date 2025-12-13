using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using BRL;
using DCL;
using Intranet_3._0.Interna;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.HtmlControls;

namespace Intranet_3._0.Vistas.V_Comunicacion
{
    public partial class V_Control_Documentos : System.Web.UI.Page
    {
        string pathLog = "";
        string ipServer = "";
        const string CONST_ERRORCONEXIONSERV = "al intentar conectarse al servidor: ";
        const string CONST_ERRORPERMISOS = "al intentar acceder a archivos. ACCESO DENEGADO. ";
        const string CONST_ERROR = " - ERROR: ";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarDocumentos();
                ProcesarAccionesURL();
            }
        }

        private void CargarDocumentos()
        {
            try
            {
                Int_Documentos obj = new Int_Documentos();
                Int_DocumentosCollection documentos = Int_Documentos_BRL.SelectByParams(obj, 0);

                if (documentos != null && documentos.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("<table class='tabla-documentos'>");
                    sb.Append("<thead><tr>");
                    sb.Append("<th>ID</th>");
                    sb.Append("<th>TÍTULO</th>");
                    sb.Append("<th>DESCRIPCIÓN</th>");
                    sb.Append("<th>ARCHIVO</th>");
                    sb.Append("<th>URL</th>");
                    sb.Append("<th>FECHA</th>");
                    sb.Append("<th>ESTADO</th>");
                    sb.Append("<th>ACCIONES</th>");
                    sb.Append("</tr></thead><tbody>");

                    foreach (Int_Documentos doc in documentos)
                    {
                        string descripcion = doc.Descripcion ?? "";
                        if (descripcion.Length > 80)
                            descripcion = descripcion.Substring(0, 80) + "...";

                        string archivoNombre = !string.IsNullOrEmpty(doc.Archivo) ?
                            Path.GetFileName(doc.Archivo) : "Sin archivo";

                        string urlMostrar = !string.IsNullOrEmpty(doc.Url) ?
                            "<a href='" + doc.Url + "' target='_blank'><i class='fas fa-external-link-alt'></i></a>" : "-";

                        string estadoTexto = doc.Estado == true ? "Activo" : "Inactivo";
                        string estadoColor = doc.Estado == true ? "green" : "red";

                        string iconoArchivo = ObtenerIconoArchivo(doc.Archivo);

                        sb.Append("<tr>");
                        sb.AppendFormat("<td>{0}</td>", doc.Id_Documentos);
                        sb.AppendFormat("<td>{0}</td>", HttpUtility.HtmlEncode(doc.Titulo));
                        sb.AppendFormat("<td>{0}</td>", HttpUtility.HtmlEncode(descripcion));
                        sb.AppendFormat("<td><i class='{0}'></i> {1}</td>", iconoArchivo, HttpUtility.HtmlEncode(archivoNombre));
                        sb.AppendFormat("<td class='text-center'>{0}</td>", urlMostrar);
                        sb.AppendFormat("<td>{0}</td>", doc.FechaCreacion?.ToString("dd/MM/yyyy HH:mm") ?? "");
                        sb.AppendFormat("<td style='color:{1}'><strong>{0}</strong></td>", estadoTexto, estadoColor);
                        sb.Append("<td class='acciones'>");
                        sb.AppendFormat("<button class='btn-action btn-info' onclick=\"location.href='?action=edit&id={0}'\"><i class='fas fa-edit'></i></button>", doc.Id_Documentos);
                        sb.AppendFormat("<button class='btn-action btn-danger' onclick=\"if(confirmarEliminacion()) location.href='?action=delete&id={0}'\"><i class='fas fa-trash'></i></button>", doc.Id_Documentos);
                        sb.Append("</td></tr>");
                    }

                    sb.Append("</tbody></table>");
                    lit_tabla_documentos.Text = sb.ToString();
                }
                else
                {
                    lit_tabla_documentos.Text = "<p class='text-center'>No hay documentos registrados.</p>";
                }
            }
            catch (Exception ex)
            {
                lit_tabla_documentos.Text = "<p class='msg-error'>Error: " + ex.Message + "</p>";
            }
        }

        private string ObtenerIconoArchivo(string rutaArchivo)
        {
            if (string.IsNullOrEmpty(rutaArchivo))
                return "fas fa-file";

            string extension = Path.GetExtension(rutaArchivo).ToLower();

            switch (extension)
            {
                case ".pdf":
                    return "fas fa-file-pdf";
                case ".doc":
                case ".docx":
                    return "fas fa-file-word";
                case ".xls":
                case ".xlsx":
                    return "fas fa-file-excel";
                case ".ppt":
                case ".pptx":
                    return "fas fa-file-powerpoint";
                case ".zip":
                case ".rar":
                    return "fas fa-file-archive";
                default:
                    return "fas fa-file";
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
                        CargarDocumentoParaEditar(id);
                    }
                    else if (action == "delete")
                    {
                        EliminarDocumento(id);
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

                if (string.IsNullOrWhiteSpace(txt_descripcion.Text))
                {
                    lbl_mensaje.Text = "La descripción es obligatoria.";
                    return;
                }

                if (!fud_archivo.HasFile)
                {
                    lbl_mensaje.Text = "Debe seleccionar un archivo.";
                    return;
                }

                Int_Documentos obj = new Int_Documentos
                {
                    Titulo = txt_titulo.Text.Trim(),
                    Descripcion = txt_descripcion.Text.Trim(),
                    Url = txt_url.Text.Trim(),
                    UsuarioCreacion = ObtenerIdUsuarioActual(),
                    Estado = true
                };

                string rutaArchivo = GuardarArchivo(fud_archivo);
                if (!string.IsNullOrEmpty(rutaArchivo))
                {
                    obj.Archivo = rutaArchivo;
                }
                else
                {
                    lbl_mensaje.Text = "Error al guardar el archivo.";
                    return;
                }

                int resultado = Int_Documentos_BRL.InsertOrUpdate(obj, 3);

                if (resultado > 0)
                {
                    Response.Redirect(Request.RawUrl.Split('?')[0]);
                }
                else
                {
                    lbl_mensaje.Text = "Error al guardar el documento.";
                }
            }
            catch (Exception ex)
            {
                lbl_mensaje.Text = "Error: " + ex.Message;
            }
        }

        private void CargarDocumentoParaEditar(int id)
        {
            try
            {
                Int_Documentos obj = new Int_Documentos { Id_Documentos = id };
                Int_DocumentosCollection documentos = Int_Documentos_BRL.SelectByParams(obj, 2);

                if (documentos != null && documentos.Count > 0)
                {
                    Int_Documentos doc = documentos[0];

                    hf_id_documento.Value = doc.Id_Documentos.ToString();
                    txt_titulo_edit.Text = doc.Titulo;
                    txt_descripcion_edit.Text = doc.Descripcion;
                    txt_url_edit.Text = doc.Url;
                    ddl_estado_edit.SelectedValue = doc.Estado == true ? "1" : "0";

                    if (!string.IsNullOrEmpty(doc.Archivo))
                    {
                        hf_archivo_actual.Value = doc.Archivo;
                        lbl_archivo_actual.Text = Path.GetFileName(doc.Archivo);
                    }
                    else
                    {
                        hf_archivo_actual.Value = "";
                        lbl_archivo_actual.Text = "No hay archivo";
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

                if (string.IsNullOrWhiteSpace(txt_descripcion_edit.Text))
                {
                    lbl_mensaje_edit.Text = "La descripción es obligatoria.";
                    return;
                }

                int idDocumento = Convert.ToInt32(hf_id_documento.Value);

                Int_Documentos obj = new Int_Documentos
                {
                    Id_Documentos = idDocumento,
                    Titulo = txt_titulo_edit.Text.Trim(),
                    Descripcion = txt_descripcion_edit.Text.Trim(),
                    Url = txt_url_edit.Text.Trim(),
                    UsuarioActualizacion = ObtenerIdUsuarioActual(),
                    Estado = ddl_estado_edit.SelectedValue == "1"
                };

                if (fud_archivo_edit.HasFile)
                {
                    if (!string.IsNullOrEmpty(hf_archivo_actual.Value))
                    {
                        EliminarArchivoFisico(hf_archivo_actual.Value);
                    }

                    string rutaArchivo = GuardarArchivo(fud_archivo_edit);
                    if (!string.IsNullOrEmpty(rutaArchivo))
                    {
                        obj.Archivo = rutaArchivo;
                    }
                    else
                    {
                        lbl_mensaje_edit.Text = "Error al guardar el archivo.";
                        return;
                    }
                }
                else
                {
                    obj.Archivo = hf_archivo_actual.Value;
                }

                int resultado = Int_Documentos_BRL.InsertOrUpdate(obj, 4);

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

        private void EliminarDocumento(int id)
        {
            try
            {
                Int_Documentos objBuscar = new Int_Documentos { Id_Documentos = id };
                Int_DocumentosCollection documentos = Int_Documentos_BRL.SelectByParams(objBuscar, 2);

                if (documentos != null && documentos.Count > 0)
                {
                    Int_Documentos doc = documentos[0];

                    Int_Documentos objEliminar = new Int_Documentos
                    {
                        Id_Documentos = id,
                        UsuarioActualizacion = ObtenerIdUsuarioActual()
                    };

                    int resultado = Int_Documentos_BRL.InsertOrUpdate(objEliminar, 5);

                    if (resultado > 0 && !string.IsNullOrEmpty(doc.Archivo))
                    {
                        EliminarArchivoFisico(doc.Archivo);
                    }
                }

                Response.Redirect(Request.RawUrl.Split('?')[0]);
            }
            catch (Exception ex)
            {
                lit_tabla_documentos.Text = "<p class='msg-error'>Error: " + ex.Message + "</p>";
            }
        }

        private string GuardarArchivo(System.Web.UI.WebControls.FileUpload fileUpload)
        {
            try
            {
                if (!fileUpload.HasFile)
                    return null;

                string extension = Path.GetExtension(fileUpload.FileName).ToLower();
                string[] extensionesPermitidas = { ".pdf", ".doc", ".docx", ".xls", ".xlsx", ".ppt", ".pptx", ".zip", ".rar" };

                if (!extensionesPermitidas.Contains(extension))
                    return null;

                string[] rutas = AG_Utils.ObtenerRutasDocumentos();
                string rutaLocal = rutas[0];
                string carpetaFisica = Server.MapPath(rutaLocal);

                if (!Directory.Exists(carpetaFisica))
                {
                    Directory.CreateDirectory(carpetaFisica);
                }

                string nombreArchivo = "doc_" + DateTime.Now.ToString("yyyyMMddHHmmss") +
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