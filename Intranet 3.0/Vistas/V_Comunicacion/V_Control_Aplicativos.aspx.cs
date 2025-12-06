using System;
using System.Data;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using BRL;
using DCL;

namespace Intranet_3._0.Vistas.V_Comunicacion
{
    public partial class V_Control_Aplicativos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarTablaAplicativos();
            }
        }

        protected void txt_buscar_TextChanged(object sender, EventArgs e)
        {
            //CargarTablaAplicativos(txt_buscar.Text.Trim());
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

            Int_Aplicativo aplicativo = new Int_Aplicativo
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
                string rutaImagen = GuardarArchivo(fud_imagen);

                Int_Aplicativo aplicativo = new Int_Aplicativo
                {
                    Titulo = txt_titulo.Text.Trim(),
                    Descripcion = txt_descripcion.Text.Trim(),
                    Url = txt_url.Text.Trim(),
                    Imagen = rutaImagen,
                    Seccion = ddl_seccion.SelectedValue,
                    Fecha_Creacion = DateTime.Now,
                    Estado = true
                };

                Int_Aplicativos_BRL.InsertOrUpdate(aplicativo, 3);
                //CargarTablaAplicativos(txt_buscar.Text.Trim());
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
                    rutaImagen = GuardarArchivo(fud_imagen_edit);
                }

                Int_Aplicativo aplicativo = new Int_Aplicativo
                {
                    Id_Aplicativo = Convert.ToInt32(hf_id_aplicativo.Value),
                    Titulo = txt_titulo_edit.Text.Trim(),
                    Descripcion = txt_descripcion_edit.Text.Trim(),
                    Url = txt_url_edit.Text.Trim(),
                    Imagen = rutaImagen,
                    Seccion = ddl_seccion_edit.SelectedValue,
                    Estado = ddl_estado.SelectedValue == "1",
                    Fecha_Actualizacion = DateTime.Now
                };

                Int_Aplicativos_BRL.InsertOrUpdate(aplicativo, 4);
                //CargarTablaAplicativos(txt_buscar.Text.Trim());
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

                Int_Aplicativo aplicativo = new Int_Aplicativo
                {
                    Id_Aplicativo = Convert.ToInt32(hf_id_aplicativo.Value),
                    Estado = false,
                    Fecha_Actualizacion = DateTime.Now
                };

                Int_Aplicativos_BRL.InsertOrUpdate(aplicativo, 5);
                //CargarTablaAplicativos(txt_buscar.Text.Trim());
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
                DataTable dt = Int_Aplicativos_BRL.SelectTable(new Int_Aplicativo(), 0);

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
                sb.Append("<th>#</th><th>Título</th><th>Descripción</th><th>Sección</th><th>Fecha creación</th><th>Estado</th><th>Acción</th>");
                sb.Append("</tr></thead><tbody>");

                int contador = 1;
                foreach (DataRow row in dt.Rows)
                {
                    string id = row["Id_Aplicativo"].ToString();
                    string titulo = Server.HtmlEncode(row["Titulo"].ToString());
                    string descripcion = Server.HtmlEncode(row["Descripcion"].ToString());
                    string seccion = Server.HtmlEncode(row["Seccion"].ToString());
                    string fecha = row["Fecha_Creacion"].ToString();
                    bool estado = row["Estado"] != DBNull.Value && Convert.ToBoolean(row["Estado"]);

                    sb.Append("<tr>");
                    sb.AppendFormat("<td>{0}</td>", contador);
                    sb.AppendFormat("<td>{0}</td>", titulo);
                    sb.AppendFormat("<td>{0}</td>", descripcion);
                    sb.AppendFormat("<td>{0}</td>", FormatearSeccion(seccion));
                    sb.AppendFormat("<td>{0}</td>", fecha);
                    sb.AppendFormat("<td>{0}</td>", estado ? "<span class='badge badge-success'>Activo</span>" : "<span class='badge badge-secondary'>Inactivo</span>");
                    sb.AppendFormat("<td><input type='radio' name='rd_aplicativo' value='{0}' /></td>", id);
                    sb.Append("</tr>");
                    contador++;
                }

                if (contador == 1)
                {
                    sb.Append("<tr><td colspan='7'>No hay aplicativos registrados.</td></tr>");
                }

                sb.Append("</tbody></table>");
                tbl_aplicativos.InnerHtml = sb.ToString();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Error al cargar la tabla de aplicativos: " + ex.Message, false);
            }
        }

        private string GuardarArchivo(FileUpload control)
        {
            if (control == null || !control.HasFile)
            {
                return hf_imagen_actual.Value;
            }

            string extension = Path.GetExtension(control.FileName);
            string nombreArchivo = $"aplicativo_{DateTime.Now:yyyyMMddHHmmssfff}{extension}";
            string carpeta = Server.MapPath("~/Content/img/aplicativos/");
            if (!Directory.Exists(carpeta))
            {
                Directory.CreateDirectory(carpeta);
            }

            string rutaCompleta = Path.Combine(carpeta, nombreArchivo);
            control.SaveAs(rutaCompleta);
            return $"/Content/img/aplicativos/{nombreArchivo}";
        }

        private void LimpiarFormularioCrear()
        {
            txt_titulo.Text = string.Empty;
            txt_descripcion.Text = string.Empty;
            txt_url.Text = string.Empty;
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

        private void MostrarMensaje(string mensaje, bool exitoso)
        {
            string clase = exitoso ? "text-success" : "text-danger";
            //ltr_mensaje.Text = $"<p class='{clase}'>{mensaje}</p>";
        }
    }
}
