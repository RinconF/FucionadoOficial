using BRL;
using DCL;
using Intranet_3._0.Interna;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;

namespace Intranet_3._0.Vistas.V_Comunicacion
{
    public partial class V_Control_Aplicativos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarAplicativos();
            }
        }

        private void CargarAplicativos()
        {
            try
            {
                Int_Aplicativos obj = new Int_Aplicativos();
                // Action 0: SELECT ALL - Lista todos los aplicativos activos
                Int_AplicativoCollection aplicativos = Int_Aplicativos_BRL.SelectByParams(obj, 0);

                if (aplicativos != null && aplicativos.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("<table class='tbl_vistas_general table table-striped table-hover'>");
                    sb.Append("<thead><tr>");
                    sb.Append("<th style='width: 50px;'>#</th>"); // Número de fila
                    sb.Append("<th style='width: 60px;'>ID</th>");
                    sb.Append("<th>TITULO</th>");
                    sb.Append("<th>DESCRIPCION</th>");
                    sb.Append("<th style='width: 80px;'>URL</th>");
                    sb.Append("<th style='width: 80px;'>Imagen</th>");
                    sb.Append("<th style='width: 180px;'>FECHA DE CREACION</th>");
                    sb.Append("<th style='width: 80px;'>ACCION</th>"); // Radio button
                    sb.Append("</tr></thead><tbody>");

                    int contador = 1;
                    foreach (Int_Aplicativos app in aplicativos)
                    {
                        string descripcion = app.Descripcion ?? "";
                        if (descripcion.Length > 80)
                            descripcion = descripcion.Substring(0, 80) + "...";

                        string urlIcono = !string.IsNullOrEmpty(app.Url) ?
                            "<a href='" + app.Url + "' target='_blank' title='Ir al aplicativo' style='color: #3498db;'><i class='fas fa-external-link-alt'></i></a>" :
                            "-";

                        string imagenIcono = !string.IsNullOrEmpty(app.Imagen) ?
                            "<a href='" + ResolveUrl(app.Imagen) + "' target='_blank' title='Ver imagen' style='color: #3498db;'><i class='fas fa-eye'></i></a>" :
                            "-";

                        sb.Append("<tr>");
                        // Número consecutivo
                        sb.AppendFormat("<td class='text-center'>{0}</td>", contador);
                        // ID
                        sb.AppendFormat("<td>{0}</td>", app.Id_Aplicativo);
                        // TITULO
                        sb.AppendFormat("<td>{0}</td>", HttpUtility.HtmlEncode(app.Titulo));
                        // DESCRIPCION
                        sb.AppendFormat("<td>{0}</td>", HttpUtility.HtmlEncode(descripcion));
                        // URL
                        sb.AppendFormat("<td class='text-center'>{0}</td>", urlIcono);
                        // Imagen
                        sb.AppendFormat("<td class='text-center'>{0}</td>", imagenIcono);
                        // FECHA DE CREACION
                        sb.AppendFormat("<td>{0}</td>", app.Fecha_Creacion?.ToString("dd/MM/yyyy h:mm:ss tt") ?? "");
                        // ACCION - Radio button con datos ocultos
                        sb.Append("<td class='text-center'>");
                        sb.AppendFormat("<input type='radio' name='rd_aplicativo' value='{0}' ", app.Id_Aplicativo);
                        sb.AppendFormat("data-titulo='{0}' ", HttpUtility.HtmlEncode(app.Titulo));
                        sb.AppendFormat("data-descripcion='{0}' ", HttpUtility.HtmlEncode(app.Descripcion));
                        sb.AppendFormat("data-url='{0}' ", HttpUtility.HtmlEncode(app.Url));
                        sb.AppendFormat("data-seccion='{0}' ", HttpUtility.HtmlEncode(app.Seccion));
                        sb.AppendFormat("data-orden='{0}' ", app.Orden?.ToString() ?? "");
                        sb.AppendFormat("data-imagen='{0}' ", HttpUtility.HtmlEncode(app.Imagen ?? ""));
                        sb.AppendFormat("data-estado='{0}' ", app.Estado == true ? "1" : "0");
                        sb.Append("/>");
                        sb.Append("</td>");
                        sb.Append("</tr>");

                        contador++;
                    }

                    sb.Append("</tbody></table>");
                    lit_tabla_aplicativos.Text = sb.ToString();
                }
                else
                {
                    lit_tabla_aplicativos.Text = "<p class='text-center' style='padding: 30px;'>No hay aplicativos registrados.</p>";
                }
            }
            catch (Exception ex)
            {
                lit_tabla_aplicativos.Text = "<p class='msg-error'>Error al cargar aplicativos: " + ex.Message + "</p>";
            }
        }

        private string FormatearSeccion(string seccion)
        {
            if (string.IsNullOrEmpty(seccion))
                return "";

            switch (seccion.ToUpper())
            {
                case "EMPRESARIALES":
                    return "Aplicativos Empresariales";
                case "CONSULTA":
                    return "Aplicativos de Consulta";
                case "SOPORTE":
                    return "Aplicativos de Soporte";
                default:
                    return seccion;
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

                if (string.IsNullOrWhiteSpace(txt_url.Text))
                {
                    lbl_mensaje.Text = "La URL es obligatoria.";
                    lbl_mensaje.CssClass = "msg-error";
                    return;
                }

                if (ddl_seccion.SelectedValue == "")
                {
                    lbl_mensaje.Text = "Debe seleccionar una sección.";
                    lbl_mensaje.CssClass = "msg-error";
                    return;
                }

                if (!fud_imagen.HasFile)
                {
                    lbl_mensaje.Text = "Debe seleccionar una imagen.";
                    lbl_mensaje.CssClass = "msg-error";
                    return;
                }

                // Crear objeto
                Int_Aplicativos obj = new Int_Aplicativos
                {
                    Titulo = txt_titulo.Text.Trim(),
                    Descripcion = txt_descripcion.Text.Trim(),
                    Url = txt_url.Text.Trim(),
                    Seccion = ddl_seccion.SelectedValue,
                    Orden = !string.IsNullOrWhiteSpace(txt_orden.Text) ? Convert.ToInt32(txt_orden.Text) : (int?)null,
                    Usuario_Creacion = ObtenerIdUsuarioActual(),
                    Estado = true
                };

                // Guardar imagen física
                string rutaImagen = GuardarImagen(fud_imagen);
                if (!string.IsNullOrEmpty(rutaImagen))
                {
                    obj.Imagen = rutaImagen;
                }
                else
                {
                    lbl_mensaje.Text = "Error al guardar la imagen.";
                    lbl_mensaje.CssClass = "msg-error";
                    return;
                }

                // Action 3: INSERT - Insertar nuevo aplicativo
                int resultado = Int_Aplicativos_BRL.InsertOrUpdate(obj, 3);

                if (resultado > 0)
                {
                    // Limpiar campos
                    txt_titulo.Text = "";
                    txt_descripcion.Text = "";
                    txt_url.Text = "";
                    txt_orden.Text = "";
                    ddl_seccion.SelectedIndex = 0;
                    lbl_mensaje.Text = "Aplicativo creado exitosamente.";
                    lbl_mensaje.CssClass = "msg-success";

                    // Recargar tabla
                    CargarAplicativos();
                }
                else
                {
                    lbl_mensaje.Text = "Error al guardar el aplicativo.";
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
                if (string.IsNullOrWhiteSpace(hf_id_aplicativo.Value))
                {
                    lbl_mensaje_edit.Text = "No se encontró el identificador del aplicativo.";
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

                if (string.IsNullOrWhiteSpace(txt_url_edit.Text))
                {
                    lbl_mensaje_edit.Text = "La URL es obligatoria.";
                    lbl_mensaje_edit.CssClass = "msg-error";
                    return;
                }

                if (ddl_seccion_edit.SelectedValue == "")
                {
                    lbl_mensaje_edit.Text = "Debe seleccionar una sección.";
                    lbl_mensaje_edit.CssClass = "msg-error";
                    return;
                }

                int idAplicativo = Convert.ToInt32(hf_id_aplicativo.Value);

                // Crear objeto para actualizar
                Int_Aplicativos obj = new Int_Aplicativos
                {
                    Id_Aplicativo = idAplicativo,
                    Titulo = txt_titulo_edit.Text.Trim(),
                    Descripcion = txt_descripcion_edit.Text.Trim(),
                    Url = txt_url_edit.Text.Trim(),
                    Seccion = ddl_seccion_edit.SelectedValue,
                    Orden = !string.IsNullOrWhiteSpace(txt_orden_edit.Text) ? Convert.ToInt32(txt_orden_edit.Text) : (int?)null,
                    Usuario_Actualizacion = ObtenerIdUsuarioActual(),
                    Estado = ddl_estado_edit.SelectedValue == "1"
                };

                // Verificar si hay nueva imagen
                if (fud_imagen_edit.HasFile)
                {
                    // Eliminar imagen anterior si existe
                    if (!string.IsNullOrEmpty(hf_imagen_actual.Value) && hf_imagen_actual.Value != "Sin imagen")
                    {
                        EliminarArchivoFisico(hf_imagen_actual.Value);
                    }

                    // Guardar nueva imagen
                    string rutaImagen = GuardarImagen(fud_imagen_edit);
                    if (!string.IsNullOrEmpty(rutaImagen))
                    {
                        obj.Imagen = rutaImagen;
                    }
                    else
                    {
                        lbl_mensaje_edit.Text = "Error al guardar la nueva imagen.";
                        lbl_mensaje_edit.CssClass = "msg-error";
                        return;
                    }
                }
                else
                {
                    // Mantener imagen actual
                    obj.Imagen = hf_imagen_actual.Value;
                }

                // Action 4: UPDATE - Actualizar aplicativo
                int resultado = Int_Aplicativos_BRL.InsertOrUpdate(obj, 4);

                if (resultado > 0)
                {
                    lbl_mensaje_edit.Text = "Aplicativo actualizado exitosamente.";
                    lbl_mensaje_edit.CssClass = "msg-success";

                    // Recargar tabla
                    CargarAplicativos();
                }
                else
                {
                    lbl_mensaje_edit.Text = "Error al actualizar el aplicativo.";
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
                if (string.IsNullOrWhiteSpace(hf_id_aplicativo_eliminar.Value))
                {
                    CargarAplicativos();
                    return;
                }

                int idAplicativo = Convert.ToInt32(hf_id_aplicativo_eliminar.Value);

                // Primero obtener el aplicativo para eliminar la imagen física
                Int_Aplicativos objBuscar = new Int_Aplicativos { Id_Aplicativo = idAplicativo };
                // Action 2: LOAD - Cargar aplicativo por ID
                Int_AplicativoCollection aplicativos = Int_Aplicativos_BRL.SelectByParams(objBuscar, 2);

                if (aplicativos != null && aplicativos.Count > 0)
                {
                    Int_Aplicativos app = aplicativos[0];

                    // Crear objeto para eliminación lógica
                    Int_Aplicativos objEliminar = new Int_Aplicativos
                    {
                        Id_Aplicativo = idAplicativo,
                        Usuario_Actualizacion = ObtenerIdUsuarioActual()
                    };

                    // Action 5: DELETE - Eliminación lógica (cambia Estado a 0)
                    int resultado = Int_Aplicativos_BRL.InsertOrUpdate(objEliminar, 5);

                    if (resultado > 0)
                    {
                        // Eliminar imagen física si existe
                        if (!string.IsNullOrEmpty(app.Imagen))
                        {
                            EliminarArchivoFisico(app.Imagen);
                        }
                    }
                }

                // Recargar tabla
                CargarAplicativos();
            }
            catch (Exception ex)
            {
                lit_tabla_aplicativos.Text = "<p class='msg-error'>Error al eliminar: " + ex.Message + "</p>";
            }
        }

        private string GuardarImagen(System.Web.UI.WebControls.FileUpload fileUpload)
        {
            try
            {
                if (!fileUpload.HasFile)
                    return null;

                string extension = Path.GetExtension(fileUpload.FileName).ToLower();
                string[] extensionesPermitidas = { ".jpg", ".jpeg", ".png", ".gif", ".jfif" };

                if (!extensionesPermitidas.Contains(extension))
                {
                    return null;
                }

                // Tamaño máximo: 5 MB
                if (fileUpload.PostedFile.ContentLength > 5 * 1024 * 1024)
                {
                    return null;
                }

                // Obtener rutas desde configuración
                string[] rutas = AG_Utils.ObtenerRutasImagenesaplicativos();
                string rutaLocal = rutas[0];
                string carpetaFisica = Server.MapPath(rutaLocal);

                // Crear carpeta si no existe
                if (!Directory.Exists(carpetaFisica))
                {
                    Directory.CreateDirectory(carpetaFisica);
                }

                // Generar nombre único para el archivo
                string nombreArchivo = "aplicativo_" + DateTime.Now.ToString("yyyyMMddHHmmss") +
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