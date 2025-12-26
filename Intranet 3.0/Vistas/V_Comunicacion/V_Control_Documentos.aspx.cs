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
            }
        }

        private void CargarDocumentos()
        {
            try
            {
                Int_Documentos obj = new Int_Documentos();
                // Action 5: SELECT ALL - Lista todos los documentos activos
                Int_DocumentosCollection documentos = Int_Documentos_BRL.SelectByParams(obj, 5);

                if (documentos != null && documentos.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("<table class='tbl_vistas_general table table-striped table-hover'>");
                    sb.Append("<thead><tr>");
                    sb.Append("<th style='width: 50px;'>#</th>"); // Número de fila
                    sb.Append("<th style='width: 60px;'>ID</th>");
                    sb.Append("<th>TITULO</th>");
                    sb.Append("<th>DESCRIPCION</th>");
                    sb.Append("<th style='width: 80px;'>URL</th>");
                    sb.Append("<th style='width: 80px;'>Archivo</th>");
                    sb.Append("<th style='width: 180px;'>FECHA DE CREACION</th>");
                    sb.Append("<th style='width: 80px;'>ACCION</th>"); // Radio button
                    sb.Append("</tr></thead><tbody>");

                    int contador = 1;
                    foreach (Int_Documentos doc in documentos)
                    {
                        string descripcion = doc.Descripcion ?? "";
                        if (descripcion.Length > 80)
                            descripcion = descripcion.Substring(0, 80) + "...";

                        string urlIcono = !string.IsNullOrEmpty(doc.Url) ?
                            "<a href='" + doc.Url + "' target='_blank' title='Ver enlace' style='color: #3498db;'><i class='fas fa-external-link-alt'></i></a>" :
                            "-";

                        string archivoIcono = !string.IsNullOrEmpty(doc.Archivo) ?
                            "<a href='" + ResolveUrl(doc.Archivo) + "' download title='Descargar archivo' style='color: #27ae60;'><i class='fas fa-download'></i></a>" :
                            "-";

                        sb.Append("<tr>");
                        // Número consecutivo
                        sb.AppendFormat("<td class='text-center'>{0}</td>", contador);
                        // ID
                        sb.AppendFormat("<td>{0}</td>", doc.Id_Documentos);
                        // TITULO
                        sb.AppendFormat("<td>{0}</td>", HttpUtility.HtmlEncode(doc.Titulo));
                        // DESCRIPCION
                        sb.AppendFormat("<td>{0}</td>", HttpUtility.HtmlEncode(descripcion));
                        // URL
                        sb.AppendFormat("<td class='text-center'>{0}</td>", urlIcono);
                        // Archivo
                        sb.AppendFormat("<td class='text-center'>{0}</td>", archivoIcono);
                        // FECHA DE CREACION
                        sb.AppendFormat("<td>{0}</td>", doc.FechaCreacion?.ToString("dd/MM/yyyy h:mm:ss tt") ?? "");
                        // ACCION - Radio button con datos ocultos
                        sb.Append("<td class='text-center'>");
                        sb.AppendFormat("<input type='radio' name='rd_documento' value='{0}' ", doc.Id_Documentos);
                        sb.AppendFormat("data-titulo='{0}' ", HttpUtility.HtmlEncode(doc.Titulo));
                        sb.AppendFormat("data-descripcion='{0}' ", HttpUtility.HtmlEncode(doc.Descripcion ?? ""));
                        sb.AppendFormat("data-url='{0}' ", HttpUtility.HtmlEncode(doc.Url ?? ""));
                        sb.AppendFormat("data-archivo='{0}' ", HttpUtility.HtmlEncode(doc.Archivo ?? ""));
                        sb.AppendFormat("data-estado='{0}' ", doc.Estado == true ? "1" : "0");
                        sb.Append("/>");
                        sb.Append("</td>");
                        sb.Append("</tr>");

                        contador++;
                    }

                    sb.Append("</tbody></table>");
                    lit_tabla_documentos.Text = sb.ToString();
                }
                else
                {
                    lit_tabla_documentos.Text = "<p class='text-center' style='padding: 30px;'>No hay documentos registrados.</p>";
                }
            }
            catch (Exception ex)
            {
                lit_tabla_documentos.Text = "<p class='msg-error'>Error al cargar documentos: " + ex.Message + "</p>";
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

        protected void btn_guardar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validaciones
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

                if (!fud_archivo.HasFile)
                {
                    lbl_mensaje.Text = "Debe seleccionar un archivo.";
                    lbl_mensaje.CssClass = "msg-error";
                    return;
                }

                // Crear objeto
                Int_Documentos obj = new Int_Documentos
                {
                    Titulo = txt_titulo.Text.Trim(),
                    Descripcion = txt_descripcion.Text.Trim(),
                    Url = txt_url.Text.Trim(),
                    UsuarioCreacion = ObtenerIdUsuarioActual(),
                    Estado = true
                };

                // Guardar archivo físico
                string rutaArchivo = GuardarArchivo(fud_archivo);
                if (!string.IsNullOrEmpty(rutaArchivo))
                {
                    obj.Archivo = rutaArchivo;
                }
                else
                {
                    lbl_mensaje.Text = "Error al guardar el archivo.";
                    lbl_mensaje.CssClass = "msg-error";
                    return;
                }

                // Action 3: INSERT - Insertar nuevo documento
                int resultado = Int_Documentos_BRL.InsertOrUpdate(obj, 3);

                if (resultado > 0)
                {
                    // Limpiar campos
                    txt_titulo.Text = "";
                    txt_descripcion.Text = "";
                    txt_url.Text = "";
                    lbl_mensaje.Text = "Documento creado exitosamente.";
                    lbl_mensaje.CssClass = "msg-success";

                    // Recargar tabla
                    CargarDocumentos();
                }
                else
                {
                    lbl_mensaje.Text = "Error al guardar el documento.";
                    lbl_mensaje.CssClass = "msg-error";
                }
            }
            catch (Exception ex)
            {
                lbl_mensaje.Text = "Error: " + ex.Message;
                lbl_mensaje.CssClass = "msg-error";
            }
        }

        protected void btn_actualizar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validaciones
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

                if (string.IsNullOrWhiteSpace(hf_id_documento.Value))
                {
                    lbl_mensaje_edit.Text = "ID de documento no válido.";
                    lbl_mensaje_edit.CssClass = "msg-error";
                    return;
                }

                int idDocumento = Convert.ToInt32(hf_id_documento.Value);

                // Crear objeto para actualizar
                Int_Documentos obj = new Int_Documentos
                {
                    Id_Documentos = idDocumento,
                    Titulo = txt_titulo_edit.Text.Trim(),
                    Descripcion = txt_descripcion_edit.Text.Trim(),
                    Url = txt_url_edit.Text.Trim(),
                    UsuarioActualizacion = ObtenerIdUsuarioActual(),
                    Estado = ddl_estado_edit.SelectedValue == "1"
                };

                // Verificar si hay nuevo archivo
                if (fud_archivo_edit.HasFile)
                {
                    // Eliminar archivo anterior si existe
                    if (!string.IsNullOrEmpty(hf_archivo_actual.Value) && hf_archivo_actual.Value != "Sin archivo")
                    {
                        EliminarArchivoFisico(hf_archivo_actual.Value);
                    }

                    // Guardar nuevo archivo
                    string rutaArchivo = GuardarArchivo(fud_archivo_edit);
                    if (!string.IsNullOrEmpty(rutaArchivo))
                    {
                        obj.Archivo = rutaArchivo;
                    }
                    else
                    {
                        lbl_mensaje_edit.Text = "Error al guardar el nuevo archivo.";
                        lbl_mensaje_edit.CssClass = "msg-error";
                        return;
                    }
                }
                else
                {
                    // Mantener archivo actual
                    obj.Archivo = hf_archivo_actual.Value;
                }

                // Action 4: UPDATE - Actualizar documento
                int resultado = Int_Documentos_BRL.InsertOrUpdate(obj, 4);

                if (resultado > 0)
                {
                    lbl_mensaje_edit.Text = "Documento actualizado exitosamente.";
                    lbl_mensaje_edit.CssClass = "msg-success";

                    // Recargar tabla
                    CargarDocumentos();
                }
                else
                {
                    lbl_mensaje_edit.Text = "Error al actualizar el documento.";
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
                if (string.IsNullOrWhiteSpace(hf_id_documento_eliminar.Value))
                {
                    CargarDocumentos();
                    return;
                }

                int idDocumento = Convert.ToInt32(hf_id_documento_eliminar.Value);

                // Primero obtener el documento para eliminar el archivo físico
                Int_Documentos objBuscar = new Int_Documentos { Id_Documentos = idDocumento };
                // Action 2: LOAD - Cargar documento por ID
                Int_Documentos doc = Int_Documentos_BRL.Load(objBuscar);

                if (doc != null)
                {
                    // Crear objeto para eliminación lógica
                    Int_Documentos objEliminar = new Int_Documentos
                    {
                        Id_Documentos = idDocumento,
                        UsuarioActualizacion = ObtenerIdUsuarioActual()
                    };

                    // Action 1: DELETE - Eliminación lógica (cambia Estado a 0)
                    int resultado = Int_Documentos_BRL.InsertOrUpdate(objEliminar, 1);

                    if (resultado > 0)
                    {
                        // Eliminar archivo físico si existe
                        if (!string.IsNullOrEmpty(doc.Archivo))
                        {
                            EliminarArchivoFisico(doc.Archivo);
                        }
                    }
                }

                // Recargar tabla
                CargarDocumentos();
            }
            catch (Exception ex)
            {
                lit_tabla_documentos.Text = "<p class='msg-error'>Error al eliminar: " + ex.Message + "</p>";
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
                {
                    return null;
                }

                // Tamaño máximo: 10 MB
                if (fileUpload.PostedFile.ContentLength > 10 * 1024 * 1024)
                {
                    return null;
                }

                // Obtener rutas desde configuración
                string[] rutas = AG_Utils.ObtenerRutasDocumentos();
                string rutaLocal = rutas[0];
                string carpetaFisica = Server.MapPath(rutaLocal);

                // Crear carpeta si no existe
                if (!Directory.Exists(carpetaFisica))
                {
                    Directory.CreateDirectory(carpetaFisica);
                }

                // Generar nombre único para el archivo
                string nombreArchivo = "doc_" + DateTime.Now.ToString("yyyyMMddHHmmss") +
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