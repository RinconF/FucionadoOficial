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
    public partial class V_Control_Tutoriales : System.Web.UI.Page
    {
        private readonly AG_Utils utilidades = new AG_Utils();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarTablaTutoriales();
            }
        }

        #region Eventos de la UI

        protected void txt_buscar_TextChanged(object sender, EventArgs e)
        {
            CargarTablaTutoriales(txt_buscar.Text.Trim());
        }

        protected void btn_modal_crear_Click(object sender, EventArgs e)
        {
            LimpiarFormularioCrear();
            ScriptManager.RegisterStartupScript(this, GetType(), "modal_crear", "mostrarModalCrear();", true);
        }

        protected void btn_modal_actualizar_Click(object sender, EventArgs e)
        {
            string seleccionado = Request.Form["rd_tutorial"];
            if (string.IsNullOrWhiteSpace(seleccionado))
            {
                MostrarMensaje("Selecciona un tutorial para actualizar.", false);
                return;
            }

            DataRow row = ObtenerTutorialPorId(Convert.ToInt32(seleccionado));
            if (row == null)
            {
                MostrarMensaje("No se encontraron datos del tutorial.", false);
                return;
            }

            hf_id_Tutorial.Value = seleccionado;
            txt_titulo_edit.Text = row["Titulo"].ToString();
            txt_descripcion_edit.Text = row["Descripcion"].ToString();
            txt_url_edit.Text = row["Url"].ToString();
            ddl_seccion_edit.SelectedValue = row["Seccion"].ToString();
            ddl_estado.SelectedValue = row["Estado"] != DBNull.Value && Convert.ToBoolean(row["Estado"]) ? "1" : "0";
            hf_imagen_actual.Value = row["Imagen"].ToString();

            txt_orden_edit.Text = row["Orden"] != DBNull.Value ? row["Orden"].ToString() : string.Empty;

            ScriptManager.RegisterStartupScript(this, GetType(), "modal_actualizar", "mostrarModalActualizar();", true);
        }

        protected void btn_modal_eliminar_Click(object sender, EventArgs e)
        {
            string seleccionado = Request.Form["rd_tutorial"];
            if (string.IsNullOrWhiteSpace(seleccionado))
            {
                MostrarMensaje("Selecciona un tutorial para eliminar.", false);
                return;
            }

            DataRow row = ObtenerTutorialPorId(Convert.ToInt32(seleccionado));
            if (row == null)
            {
                MostrarMensaje("No se encontraron datos del tutorial seleccionado.", false);
                return;
            }

            hf_id_Tutorial.Value = seleccionado;
            lit_tutorial_eliminar.Text = Server.HtmlEncode(row["Titulo"].ToString());
            ScriptManager.RegisterStartupScript(this, GetType(), "modal_eliminar", "mostrarModalEliminar();", true);
        }

        protected void lnk_crear_Tutorial_Click(object sender, EventArgs e)
        {
            try
            {
                string rutaImagen = null;

                if (fud_imagen.HasFile)
                {
                    int nuevoId = ObtenerProximoId();
                    rutaImagen = GuardarImagenTutorial(fud_imagen, nuevoId.ToString(), null);

                    if (string.IsNullOrEmpty(rutaImagen))
                    {
                        MostrarMensaje("No se pudo guardar la imagen del tutorial.", false);
                        return;
                    }
                }

                int usuarioActual = ObtenerIdUsuarioActual();
                int? orden = string.IsNullOrWhiteSpace(txt_orden.Text) ? (int?)null : Convert.ToInt32(txt_orden.Text);

                Int_Tutoriales tutorial = new Int_Tutoriales
                {
                    Titulo = txt_titulo.Text.Trim(),
                    Descripcion = txt_descripcion.Text.Trim(),
                    Url = txt_url.Text.Trim(),
                    Imagen = rutaImagen,
                    Seccion = ddl_seccion.SelectedValue,
                    Orden = orden,
                    Estado = true,
                    Usuario_Creacion = usuarioActual
                };

                Int_Tutoriales_BRL.InsertOrUpdate(tutorial, 3);
                CargarTablaTutoriales(txt_buscar.Text.Trim());
                LimpiarFormularioCrear();
                MostrarMensaje("Tutorial creado correctamente.", true);
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al crear el tutorial: " + ex.Message, false);
            }
        }

        protected void lnk_actualizar_Tutorial_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(hf_id_Tutorial.Value))
                {
                    MostrarMensaje("No se encontró el identificador del tutorial.", false);
                    return;
                }

                string rutaImagen = hf_imagen_actual.Value;
                if (fud_imagen_edit.HasFile)
                {
                    rutaImagen = GuardarImagenTutorial(fud_imagen_edit, hf_id_Tutorial.Value, hf_imagen_actual.Value);
                    if (string.IsNullOrEmpty(rutaImagen))
                    {
                        MostrarMensaje("No se pudo guardar la imagen del tutorial.", false);
                        return;
                    }
                }

                int usuarioActual = ObtenerIdUsuarioActual();
                int? orden = string.IsNullOrWhiteSpace(txt_orden_edit.Text) ? (int?)null : Convert.ToInt32(txt_orden_edit.Text);

                Int_Tutoriales tutorial = new Int_Tutoriales
                {
                    Id_Tutorial = Convert.ToInt32(hf_id_Tutorial.Value),
                    Titulo = txt_titulo_edit.Text.Trim(),
                    Descripcion = txt_descripcion_edit.Text.Trim(),
                    Url = txt_url_edit.Text.Trim(),
                    Imagen = rutaImagen,
                    Seccion = ddl_seccion_edit.SelectedValue,
                    Orden = orden,
                    Estado = ddl_estado.SelectedValue == "1",
                    Usuario_Actualizacion = usuarioActual
                };

                Int_Tutoriales_BRL.InsertOrUpdate(tutorial, 4);
                CargarTablaTutoriales(txt_buscar.Text.Trim());
                MostrarMensaje("Tutorial actualizado correctamente.", true);
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al actualizar el tutorial: " + ex.Message, false);
            }
        }

        protected void lnk_eliminar_Tutorial_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(hf_id_Tutorial.Value))
                {
                    MostrarMensaje("No se encontró el identificador del tutorial.", false);
                    return;
                }

                int usuarioActual = ObtenerIdUsuarioActual();

                Int_Tutoriales tutorial = new Int_Tutoriales
                {
                    Id_Tutorial = Convert.ToInt32(hf_id_Tutorial.Value),
                    Estado = false,
                    Usuario_Actualizacion = usuarioActual
                };

                Int_Tutoriales_BRL.InsertOrUpdate(tutorial, 5);
                CargarTablaTutoriales(txt_buscar.Text.Trim());
                MostrarMensaje("Tutorial eliminado correctamente.", true);
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al eliminar el tutorial: " + ex.Message, false);
            }
        }

        #endregion

        #region Lógica de negocio

        private void CargarTablaTutoriales(string filtro = null)
        {
            try
            {
                DataTable dt = Int_Tutoriales_BRL.SelectTable(new Int_Tutoriales(), 0);

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
                    string id = row["Id_Tutorial"].ToString();
                    string titulo = Server.HtmlEncode(row["Titulo"].ToString());
                    string descripcion = Server.HtmlEncode(row["Descripcion"].ToString());
                    string seccion = Server.HtmlEncode(row["Seccion"].ToString());
                    string orden = row["Orden"] != DBNull.Value ? row["Orden"].ToString() : "-";
                    string fecha = row["Fecha_Creacion"] != DBNull.Value ?
                        Convert.ToDateTime(row["Fecha_Creacion"]).ToString("dd/MM/yyyy HH:mm:ss") : "-";
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
                    sb.AppendFormat("<td><input type='radio' name='rd_tutorial' value='{0}' /></td>", id);
                    sb.Append("</tr>");
                    contador++;
                }

                if (contador == 1)
                {
                    sb.Append("<tr><td colspan='8'>No hay tutoriales registrados.</td></tr>");
                }

                sb.Append("</tbody></table>");
                tbl_Tutoriales.InnerHtml = sb.ToString();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al cargar la tabla de tutoriales: " + ex.Message, false);
            }
        }

        private DataRow ObtenerTutorialPorId(int idTutorial)
        {
            Int_Tutoriales tutorial = new Int_Tutoriales
            {
                Id_Tutorial = idTutorial
            };

            DataTable dt = Int_Tutoriales_BRL.SelectTable(tutorial, 2);
            return dt.Rows.Count > 0 ? dt.Rows[0] : null;
        }

        private string GuardarImagenTutorial(FileUpload control, string idTutorial, string imagenActual)
        {
            if (control == null || !control.HasFile)
            {
                return imagenActual;
            }

            string rutaImagenLocal = string.Empty;
            string rutaImagenRemota = string.Empty;
            string logPath = Server.MapPath("~/App_Data/Logs/");

            try
            {
                string extension = Path.GetExtension(control.FileName).ToLower();
                string[] extensionesPermitidas = { ".jpg", ".jpeg", ".png", ".gif", ".jfif" };

                if (!extensionesPermitidas.Contains(extension))
                {
                    utilidades.logError($"Extensión no permitida: {extension}. Usuario: {ObtenerIdUsuarioActual()}", logPath);
                    return imagenActual;
                }

                string nombreFinalArchivo = utilidades.AjusteNombreImagenNoticia(
                    Path.GetFileNameWithoutExtension(control.FileName),
                    idTutorial,
                    extension
                );

                string ambiente = System.Configuration.ConfigurationManager.AppSettings.Get("Ambiente") ?? "DESA";
                var rutas = utilidades.ObtenerRutasTutoriales(ambiente);

                if (string.IsNullOrEmpty(rutas.rutaLocal) || string.IsNullOrEmpty(rutas.rutaRemota))
                {
                    utilidades.logError("No se pudieron obtener las rutas de tutoriales. Verifique la configuración.", logPath);
                    return imagenActual;
                }

                var tamanioOriginal = control.FileBytes;
                if (tamanioOriginal.Length == 0)
                {
                    utilidades.logError($"El archivo está vacío. Usuario: {ObtenerIdUsuarioActual()}", logPath);
                    return imagenActual;
                }

                if (utilidades.impersonateValidUser())
                {
                    rutaImagenLocal = Path.Combine(rutas.rutaLocal, nombreFinalArchivo);

                    if (!Directory.Exists(rutas.rutaLocal))
                    {
                        Directory.CreateDirectory(rutas.rutaLocal);
                    }

                    foreach (var archivo in Directory.GetFiles(rutas.rutaLocal, $"{idTutorial}-*.*"))
                    {
                        File.Delete(archivo);
                    }

                    Thread.Sleep(500);
                    control.SaveAs(rutaImagenLocal);
                    Thread.Sleep(500);

                    var tamanioDestLocal = File.ReadAllBytes(rutaImagenLocal);
                    if (tamanioOriginal.Length != tamanioDestLocal.Length)
                    {
                        utilidades.logError("El tamaño del archivo local no coincide con el original.", logPath);
                        File.Delete(rutaImagenLocal);
                        utilidades.undoImpersonation();
                        return imagenActual;
                    }

                    string ipServerAttach = System.Configuration.ConfigurationManager.AppSettings.Get("IPServerAttach") ?? string.Empty;
                    if (!string.IsNullOrEmpty(ipServerAttach) && utilidades.Ping(ipServerAttach))
                    {
                        rutaImagenRemota = Path.Combine(rutas.rutaRemota, nombreFinalArchivo);

                        if (!Directory.Exists(rutas.rutaRemota))
                        {
                            Directory.CreateDirectory(rutas.rutaRemota);
                        }

                        foreach (var archivo in Directory.GetFiles(rutas.rutaRemota, $"{idTutorial}-*.*"))
                        {
                            File.Delete(archivo);
                        }

                        Thread.Sleep(500);
                        control.SaveAs(rutaImagenRemota);
                        Thread.Sleep(500);

                        var tamanioDestRemoto = File.ReadAllBytes(rutaImagenRemota);
                        if (tamanioOriginal.Length != tamanioDestRemoto.Length)
                        {
                            utilidades.logError("El tamaño del archivo remoto no coincide con el original.", logPath);
                        }
                    }
                    else
                    {
                        utilidades.logError("No se pudo conectar al servidor remoto para guardar la imagen del tutorial.", logPath);
                    }

                    utilidades.undoImpersonation();
                    return $"/Content/img/tutoriales/{nombreFinalArchivo}";
                }

                utilidades.logError("No se pudo obtener impersonación para guardar archivos de tutoriales.", logPath);
                return imagenActual;
            }
            catch (Exception ex)
            {
                utilidades.logError($"Error al guardar imagen de tutorial: {ex.Message}.", logPath);

                if (File.Exists(rutaImagenLocal))
                    File.Delete(rutaImagenLocal);
                if (File.Exists(rutaImagenRemota))
                    File.Delete(rutaImagenRemota);

                return imagenActual;
            }
        }

        private int ObtenerProximoId()
        {
            try
            {
                DataTable dt = Int_Tutoriales_BRL.SelectTable(new Int_Tutoriales(), 0);
                if (dt.Rows.Count > 0)
                {
                    int maxId = 0;
                    foreach (DataRow row in dt.Rows)
                    {
                        int id = Convert.ToInt32(row["Id_Tutorial"]);
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
                    return "Tutoriales empresariales";
                case "CONSULTA":
                    return "Tutoriales consulta";
                case "SOPORTE":
                    return "Tutoriales soporte";
                default:
                    return seccion;
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

        private void MostrarMensaje(string mensaje, bool exitoso)
        {
            ScriptManager.RegisterStartupScript(
                this,
                GetType(),
                "mensajeTutorial",
                $"alert('{mensaje.Replace("'", "\\'")}');",
                true
            );
        }

        #endregion
    }
}
