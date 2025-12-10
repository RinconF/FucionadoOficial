using BRL;
using DCL;
using Intranet_3._0.Interna;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Intranet_3._0.Vistas.V_Comunicacion
{
    public partial class V_Control_Aplicativos : System.Web.UI.Page
    {
        private AG_Utils utilidades = new AG_Utils();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarTablaAplicativos();

                // Vincular eventos JavaScript para los botones
                Page.ClientScript.RegisterStartupScript(
                    this.GetType(),
                    "BindButtons",
                    @"
                    document.getElementById('btn_modal_crear').onclick = function() { mostrarModalCrear(); };
                    document.getElementById('btn_modal_actualizar').onclick = function() { " + Page.ClientScript.GetPostBackEventReference(this, "actualizar") + @"; };
                    document.getElementById('btn_modal_eliminar').onclick = function() { " + Page.ClientScript.GetPostBackEventReference(this, "eliminar") + @"; };
                    ",
                    true
                );
            }
        }

        public void RaisePostBackEvent(string eventArgument)
        {
            if (eventArgument == "actualizar")
            {
                btn_modal_actualizar_Click(null, null);
            }
            else if (eventArgument == "eliminar")
            {
                btn_modal_eliminar_Click(null, null);
            }
        }

        protected void txt_buscar_TextChanged(object sender, EventArgs e)
        {
            CargarTablaAplicativos(txt_buscar.Text.Trim());
        }

        protected void btn_modal_crear_Click(object sender, EventArgs e)
        {
            LimpiarFormularioCrear();
            ScriptManager.RegisterStartupScript(this, GetType(), "modal_crear", "mostrarModalCrear();", true);
        }

        protected void btn_modal_actualizar_Click(object sender, EventArgs e)
        {
            string seleccionado = Request.Form["rd_aplicativo"];
            if (string.IsNullOrWhiteSpace(seleccionado))
            {
                MostrarMensaje("Selecciona un aplicativo para actualizar.", false);
                return;
            }

            Int_Aplicativos aplicativo = new Int_Aplicativos
            {
                Id_Aplicativo = Convert.ToInt32(seleccionado)
            };

            DataTable dt = Int_Aplicativos_BRL.SelectTable(aplicativo, 2);
            if (dt.Rows.Count == 0)
            {
                MostrarMensaje("No se encontraron datos del aplicativo.", false);
                return;
            }

            DataRow row = dt.Rows[0];
            hf_id_aplicativo.Value = seleccionado;
            txt_titulo_edit.Text = row["Titulo"].ToString();
            txt_descripcion_edit.Text = row["Descripcion"].ToString();
            txt_url_edit.Text = row["Url"].ToString();
            ddl_seccion_edit.SelectedValue = row["Seccion"].ToString();
            ddl_estado.SelectedValue = Convert.ToBoolean(row["Estado"]) ? "1" : "0";
            hf_imagen_actual.Value = row["Imagen"].ToString();

            // Cargar orden si existe
            if (row["Orden"] != DBNull.Value)
            {
                txt_orden_edit.Text = row["Orden"].ToString();
            }
            else
            {
                txt_orden_edit.Text = string.Empty;
            }

            ScriptManager.RegisterStartupScript(this, GetType(), "modal_actualizar", "mostrarModalActualizar();", true);
        }

        protected void btn_modal_eliminar_Click(object sender, EventArgs e)
        {
            string seleccionado = Request.Form["rd_aplicativo"];
            if (string.IsNullOrWhiteSpace(seleccionado))
            {
                MostrarMensaje("Selecciona un aplicativo para eliminar.", false);
                return;
            }

            hf_id_aplicativo.Value = seleccionado;
            ScriptManager.RegisterStartupScript(this, GetType(), "modal_eliminar", "mostrarModalEliminar();", true);
        }

        protected void lnk_crear_aplicativo_Click(object sender, EventArgs e)
        {
            try
            {
                string rutaImagen = null;

                if (fud_imagen.HasFile)
                {
                    // Obtener ID del nuevo aplicativo (simulado, después del INSERT retornará el real)
                    int nuevoId = ObtenerProximoId();
                    rutaImagen = GuardarImagenAplicativo(fud_imagen, nuevoId.ToString(), null);

                    if (string.IsNullOrEmpty(rutaImagen))
                    {
                        MostrarMensaje("Error al guardar la imagen del aplicativo.", false);
                        return;
                    }
                }

                int idUsuarioActual = ObtenerIdUsuarioActual();

                int? orden = null;
                if (!string.IsNullOrWhiteSpace(txt_orden.Text))
                {
                    orden = Convert.ToInt32(txt_orden.Text);
                }

                Int_Aplicativos aplicativo = new Int_Aplicativos
                {
                    Titulo = txt_titulo.Text.Trim(),
                    Descripcion = txt_descripcion.Text.Trim(),
                    Url = txt_url.Text.Trim(),
                    Imagen = rutaImagen,
                    Seccion = ddl_seccion.SelectedValue,
                    Orden = orden,
                    Estado = true,
                    Usuario_Creacion = idUsuarioActual
                };

                Int_Aplicativos_BRL.InsertOrUpdate(aplicativo, 3);
                CargarTablaAplicativos(txt_buscar.Text.Trim());
                LimpiarFormularioCrear();
                MostrarMensaje("Aplicativo creado correctamente.", true);
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al crear el aplicativo: " + ex.Message, false);
            }
        }

        protected void lnk_actualizar_aplicativo_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(hf_id_aplicativo.Value))
                {
                    MostrarMensaje("No se encontró el identificador del aplicativo.", false);
                    return;
                }

                string rutaImagen = hf_imagen_actual.Value;

                if (fud_imagen_edit.HasFile)
                {
                    rutaImagen = GuardarImagenAplicativo(fud_imagen_edit, hf_id_aplicativo.Value, hf_imagen_actual.Value);

                    if (string.IsNullOrEmpty(rutaImagen))
                    {
                        MostrarMensaje("Error al guardar la imagen del aplicativo.", false);
                        return;
                    }
                }

                int idUsuarioActual = ObtenerIdUsuarioActual();

                int? orden = null;
                if (!string.IsNullOrWhiteSpace(txt_orden_edit.Text))
                {
                    orden = Convert.ToInt32(txt_orden_edit.Text);
                }

                Int_Aplicativos aplicativo = new Int_Aplicativos
                {
                    Id_Aplicativo = Convert.ToInt32(hf_id_aplicativo.Value),
                    Titulo = txt_titulo_edit.Text.Trim(),
                    Descripcion = txt_descripcion_edit.Text.Trim(),
                    Url = txt_url_edit.Text.Trim(),
                    Imagen = rutaImagen,
                    Seccion = ddl_seccion_edit.SelectedValue,
                    Orden = orden,
                    Estado = ddl_estado.SelectedValue == "1",
                    Usuario_Actualizacion = idUsuarioActual
                };

                Int_Aplicativos_BRL.InsertOrUpdate(aplicativo, 4);
                CargarTablaAplicativos(txt_buscar.Text.Trim());
                MostrarMensaje("Aplicativo actualizado correctamente.", true);
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al actualizar el aplicativo: " + ex.Message, false);
            }
        }

        protected void lnk_eliminar_aplicativo_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(hf_id_aplicativo.Value))
                {
                    MostrarMensaje("No se encontró el identificador del aplicativo.", false);
                    return;
                }

                // Obtener el usuario actual
                int idUsuarioActual = ObtenerIdUsuarioActual();

                Int_Aplicativos aplicativo = new Int_Aplicativos
                {
                    Id_Aplicativo = Convert.ToInt32(hf_id_aplicativo.Value),
                    Estado = false,
                    Usuario_Actualizacion = idUsuarioActual
                };

                Int_Aplicativos_BRL.InsertOrUpdate(aplicativo, 5);
                CargarTablaAplicativos(txt_buscar.Text.Trim());
                MostrarMensaje("Aplicativo eliminado correctamente.", true);
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al eliminar el aplicativo: " + ex.Message, false);
            }
        }

        private void CargarTablaAplicativos(string filtro = null)
        {
            try
            {
                DataTable dt = Int_Aplicativos_BRL.SelectTable(new Int_Aplicativos(), 0);

                if (!string.IsNullOrWhiteSpace(filtro))
                {
                    string filtroSeguro = filtro.Replace("'", "''");
                    DataView dv = dt.DefaultView;
                    dv.RowFilter = string.Format("Titulo LIKE '%{0}%' OR Descripcion LIKE '%{0}%'", filtroSeguro);
                    dt = dv.ToTable();
                }

                StringBuilder sb = new StringBuilder();
                sb.Append("<table class='tbl_vistas_general'>");
                sb.Append("<thead><tr>");
                sb.Append("<th>#</th><th>Título</th><th>Descripción</th><th>Sección</th><th>Orden</th><th>Fecha creación</th><th>Estado</th><th>Acción</th>");
                sb.Append("</tr></thead><tbody>");

                int contador = 1;
                foreach (DataRow row in dt.Rows)
                {
                    string id = row["Id_Aplicativo"].ToString();
                    string titulo = Server.HtmlEncode(row["Titulo"].ToString());
                    string descripcion = Server.HtmlEncode(row["Descripcion"].ToString());
                    string seccion = Server.HtmlEncode(row["Seccion"].ToString());
                    string orden = row["Orden"] != DBNull.Value ? row["Orden"].ToString() : "-";
                    string fecha = row["Fecha_Creacion"] != DBNull.Value ?
                        Convert.ToDateTime(row["Fecha_Creacion"]).ToString("dd/MM/yyyy") : "-";
                    bool estado = row["Estado"] != DBNull.Value && Convert.ToBoolean(row["Estado"]);

                    sb.Append("<tr>");
                    sb.AppendFormat("<td>{0}</td>", contador);
                    sb.AppendFormat("<td>{0}</td>", titulo);
                    sb.AppendFormat("<td>{0}</td>", descripcion);
                    sb.AppendFormat("<td>{0}</td>", FormatearSeccion(seccion));
                    sb.AppendFormat("<td>{0}</td>", orden);
                    sb.AppendFormat("<td>{0}</td>", fecha);
                    sb.AppendFormat("<td>{0}</td>", estado ?
                        "<span class='badge badge-success'>Activo</span>" :
                        "<span class='badge badge-secondary'>Inactivo</span>");
                    sb.AppendFormat("<td><input type='radio' name='rd_aplicativo' value='{0}' /></td>", id);
                    sb.Append("</tr>");
                    contador++;
                }

                if (contador == 1)
                {
                    sb.Append("<tr><td colspan='8'>No hay aplicativos registrados.</td></tr>");
                }

                sb.Append("</tbody></table>");
                tbl_aplicativos.InnerHtml = sb.ToString();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al cargar la tabla de aplicativos: " + ex.Message, false);
            }
        }

        /// <summary>
        /// Guarda la imagen del aplicativo siguiendo el patrón de AG_Utils
        /// </summary>
        private string GuardarImagenAplicativo(FileUpload control, string idAplicativo, string imagenActual)
        {
            if (control == null || !control.HasFile)
            {
                return imagenActual;
            }

            string rutaImagenLocal = "";
            string rutaImagenRemota = "";
            string logPath = Server.MapPath("~/App_Data/Logs/");

            try
            {
                // Validar extensión
                string extension = Path.GetExtension(control.FileName).ToLower();
                string[] extensionesPermitidas = { ".jpg", ".jpeg", ".png", ".gif", ".jfif" };

                if (!extensionesPermitidas.Contains(extension))
                {
                    utilidades.logError($"Extensión no permitida: {extension}. Usuario: {ObtenerIdUsuarioActual()}", logPath);
                    return imagenActual;
                }

                // Normalizar nombre del archivo
                string nombreFinalArchivo = utilidades.AjusteNombreImagenNoticia(
                    Path.GetFileNameWithoutExtension(control.FileName),
                    idAplicativo,
                    extension
                );

                // Obtener rutas usando el patrón del sistema
                string ambiente = System.Configuration.ConfigurationManager.AppSettings.Get("Ambiente") ?? "DESA";
                var rutas = ObtenerRutasAplicativos(ambiente);

                if (string.IsNullOrEmpty(rutas.rutaLocal) || string.IsNullOrEmpty(rutas.rutaRemota))
                {
                    utilidades.logError("No se pudieron obtener las rutas de aplicativos. Verifique la configuración.", logPath);
                    return imagenActual;
                }

                // Validar tamaño del archivo original
                var tamanioOriginal = control.FileBytes;
                if (tamanioOriginal.Length == 0)
                {
                    utilidades.logError($"El archivo está vacío. Usuario: {ObtenerIdUsuarioActual()}", logPath);
                    return imagenActual;
                }

                if (utilidades.impersonateValidUser())
                {
                    // Guardar en local
                    rutaImagenLocal = Path.Combine(rutas.rutaLocal, nombreFinalArchivo);

                    if (!Directory.Exists(rutas.rutaLocal))
                    {
                        Directory.CreateDirectory(rutas.rutaLocal);
                    }

                    // Eliminar imágenes previas del mismo aplicativo
                    var archivosExistentes = Directory.GetFiles(rutas.rutaLocal, $"{idAplicativo}-*.*");
                    foreach (var archivo in archivosExistentes)
                    {
                        File.Delete(archivo);
                    }

                    // Guardar archivo local
                    Thread.Sleep(500);
                    control.SaveAs(rutaImagenLocal);
                    Thread.Sleep(500);

                    // Validar integridad del archivo local
                    var tamanioDestLocal = File.ReadAllBytes(rutaImagenLocal);
                    if (tamanioOriginal.Length != tamanioDestLocal.Length)
                    {
                        utilidades.logError($"El tamaño del archivo local no coincide. Original: {tamanioOriginal.Length}, Destino: {tamanioDestLocal.Length}. Usuario: {ObtenerIdUsuarioActual()}", logPath);
                        File.Delete(rutaImagenLocal);
                        utilidades.undoImpersonation();
                        return imagenActual;
                    }

                    // Intentar guardar en remoto
                    string ipServerAttach = System.Configuration.ConfigurationManager.AppSettings.Get("IPServerAttach") ?? "";

                    if (!string.IsNullOrEmpty(ipServerAttach) && utilidades.Ping(ipServerAttach))
                    {
                        rutaImagenRemota = Path.Combine(rutas.rutaRemota, nombreFinalArchivo);

                        if (!Directory.Exists(rutas.rutaRemota))
                        {
                            Directory.CreateDirectory(rutas.rutaRemota);
                        }

                        // Eliminar imágenes previas remotas
                        var archivosRemotosExistentes = Directory.GetFiles(rutas.rutaRemota, $"{idAplicativo}-*.*");
                        foreach (var archivo in archivosRemotosExistentes)
                        {
                            File.Delete(archivo);
                        }

                        Thread.Sleep(500);
                        control.SaveAs(rutaImagenRemota);
                        Thread.Sleep(500);

                        // Validar integridad del archivo remoto
                        var tamanioDestRemoto = File.ReadAllBytes(rutaImagenRemota);
                        if (tamanioOriginal.Length != tamanioDestRemoto.Length)
                        {
                            utilidades.logError($"El tamaño del archivo remoto no coincide. Usuario: {ObtenerIdUsuarioActual()}", logPath);
                        }
                    }
                    else
                    {
                        utilidades.logError($"No se pudo conectar al servidor remoto: {ipServerAttach}. Usuario: {ObtenerIdUsuarioActual()}", logPath);
                    }

                    utilidades.undoImpersonation();

                    // Retornar ruta virtual para la BD
                    return $"/Content/img/aplicativos/{nombreFinalArchivo}";
                }
                else
                {
                    utilidades.logError($"No se pudo autenticar para guardar archivos. Usuario: {ObtenerIdUsuarioActual()}", logPath);
                    return imagenActual;
                }
            }
            catch (Exception ex)
            {
                utilidades.logError($"Error al guardar imagen de aplicativo: {ex.Message}. Usuario: {ObtenerIdUsuarioActual()}", logPath);

                // Limpiar archivos en caso de error
                if (File.Exists(rutaImagenLocal))
                    File.Delete(rutaImagenLocal);
                if (File.Exists(rutaImagenRemota))
                    File.Delete(rutaImagenRemota);

                return imagenActual;
            }
        }

        /// <summary>
        /// Obtiene las rutas local y remota para aplicativos
        /// </summary>
        private (string rutaLocal, string rutaRemota) ObtenerRutasAplicativos(string ambiente)
        {
            try
            {
                string pathServerConfig = System.Configuration.ConfigurationManager.AppSettings.Get("pathServer");
                string pathRemoteConfig = System.Configuration.ConfigurationManager.AppSettings.Get("pathRemote");

                if (string.IsNullOrWhiteSpace(pathServerConfig) || string.IsNullOrWhiteSpace(pathRemoteConfig))
                {
                    return (string.Empty, string.Empty);
                }

                string pathServer = Server.MapPath(pathServerConfig);
                string pathRemote = pathRemoteConfig;
                ambiente = string.IsNullOrWhiteSpace(ambiente) ? "DESA" : ambiente;

                string rutaRemota = Path.Combine(pathRemote, @"publicaciones\Aplicativos\Imagenes") + @"\";
                string rutaLocal = Path.Combine(pathServer + ambiente, @"intranet\publicaciones\Aplicativos\Imagenes") + @"\";

                return (rutaLocal, rutaRemota);
            }
            catch
            {
                return (string.Empty, string.Empty);
            }
        }

        /// <summary>
        /// Obtiene el próximo ID disponible para aplicativos
        /// </summary>
        private int ObtenerProximoId()
        {
            try
            {
                DataTable dt = Int_Aplicativos_BRL.SelectTable(new Int_Aplicativos(), 0);
                if (dt.Rows.Count > 0)
                {
                    int maxId = 0;
                    foreach (DataRow row in dt.Rows)
                    {
                        int id = Convert.ToInt32(row["Id_Aplicativo"]);
                        if (id > maxId) maxId = id;
                    }
                    return maxId + 1;
                }
                return 1;
            }
            catch
            {
                return 1;
            }
        }

        private void LimpiarFormularioCrear()
        {
            txt_titulo.Text = string.Empty;
            txt_descripcion.Text = string.Empty;
            txt_url.Text = string.Empty;
            txt_orden.Text = string.Empty;
            ddl_seccion.SelectedIndex = 0;
        }

        private string FormatearSeccion(string seccion)
        {
            switch (seccion)
            {
                case "EMPRESARIALES":
                    return "Aplicativos empresariales";
                case "CONSULTA":
                    return "Aplicativos consulta";
                case "SOPORTE":
                    return "Aplicativos soporte";
                default:
                    return seccion;
            }
        }

        private int ObtenerIdUsuarioActual()
        {
            // Ajusta esto según tu sistema de sesión
            // Ejemplo:
            if (Session["Id_Usuario"] != null)
            {
                return Convert.ToInt32(Session["Id_Usuario"]);
            }
            return 1; // Usuario por defecto si no hay sesión
        }

        private void MostrarMensaje(string mensaje, bool exitoso)
        {
            string tipo = exitoso ? "success" : "error";
            ScriptManager.RegisterStartupScript(
                this,
                GetType(),
                "mensaje",
                $"alert('{mensaje.Replace("'", "\\'")}');",
                true
            );
        }
    }
}