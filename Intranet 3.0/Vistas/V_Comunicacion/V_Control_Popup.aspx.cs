using BRL;
using DCL;
using Intranet_3._0.Interna;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Intranet_3._0.Vistas.V_Comunicacion
{
    public partial class V_Control_Popup : System.Web.UI.Page
    {
        string pathLog = "";
        string ipServer = "";
        const string CONST_ERRORCONEXIONSERV = "al intentar conectarse al servidor: ";
        const string CONST_ERRORPERMISOS = "al intentar acceder a archivos. ACCESO DENEGADO. ";
        const string CONST_ERROR = " - ERROR: ";

        // ========================================
        // PAGE INIT
        // ========================================
        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
                pathLog = Server.MapPath(@"~/logs");
                ipServer = ConfigurationManager.AppSettings.Get("IPServerAttach")?.ToString();
            }
            catch (Exception ex)
            {
                pathLog = Server.MapPath(@"~/logs");
                System.Diagnostics.Debug.WriteLine($"Error en Page_Init: {ex.Message}");
            }
        }

        // ========================================
        // PAGE LOAD
        // ========================================
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["Id_Usuario"] != null)
                {
                    if (Response.Cookies.Count > 0 && Session["cerrar"] != null)
                    {
                        if (!IsPostBack)
                        {
                            CargarRolesDisponibles();
                        }

                        cargar_tabla_vista();

                        ScriptManager.RegisterStartupScript(
                            PanelUpdate,
                            this.GetType(),
                            "MyAction",
                            "ejecutarDatos();",
                            true);
                    }
                }
                else
                {
                    Page.Response.Redirect("~/Login", true);
                }
            }
            catch (Exception ex)
            {
                AG_Utils utilidades = new AG_Utils();
                utilidades.logError(
                    $"{CONST_ERROR}Page_Load\n{ex.Message}\n{ex.StackTrace}",
                    pathLog
                );
                Page.Response.Redirect("~/Login", true);
            }
        }

        // ========================================
        // CARGAR ROLES DISPONIBLES
        // ========================================
        private void CargarRolesDisponibles()
        {
            AG_Utils utilidades = new AG_Utils();
            try
            {
                DataTable dtRoles = Int_Popup_BRL.SelectTable(new Int_Popup(), 15);

                if (dtRoles != null && dtRoles.Rows.Count > 0)
                {
                    chkl_roles.Items.Clear();
                    chkl_roles.DataSource = dtRoles;
                    chkl_roles.DataTextField = "Nombre_Rol";
                    chkl_roles.DataValueField = "Id_Rol";
                    chkl_roles.DataBind();

                    chkl_roles_pub.Items.Clear();
                    chkl_roles_pub.DataSource = dtRoles;
                    chkl_roles_pub.DataTextField = "Nombre_Rol";
                    chkl_roles_pub.DataValueField = "Id_Rol";
                    chkl_roles_pub.DataBind();
                }
            }
            catch (Exception ex)
            {
                utilidades.logError(
                    $"{CONST_ERROR}CargarRolesDisponibles\n{ex.Message}",
                    pathLog
                );
                throw;
            }
        }

        // ========================================
        // CREAR NUEVO POPUP
        // ========================================
        protected void Crear_nueva_publicacion(object sender, EventArgs e)
        {
            string imagenPopupRemoto = "";
            string imagenPopupLocal = "";
            AG_Utils utilidades = new AG_Utils();

            try
            {
                pathLog = Server.MapPath(@"~/logs");
                ipServer = ConfigurationManager.AppSettings.Get("IPServerAttach").ToString();
                bool conectaAdjuntos = utilidades.Ping(ipServer);
                string pathServer = Server.MapPath(ConfigurationManager.AppSettings.Get("pathServer"));
                string pathRemote = ConfigurationManager.AppSettings.Get("pathRemote");
                string ambiente = ConfigurationManager.AppSettings.Get("ambiente");
                string rutaPopupsRemoto = $@"{pathRemote}publicaciones\Popup\";
                string rutaPopupsLocal = pathServer + ambiente + $@"\intranet\publicaciones\Popup\";
                string Id_Usuario = Request.QueryString["Id_Usuario"].ToString();

                Int_Popup popup = new Int_Popup();
                popup.Titulo = txt_titulo.Text;
                popup.Descripcion = txt_descripcion.Text;
                popup.Url = txt_url.Text;
                popup.Tiempo_Visualizacion = string.IsNullOrEmpty(txt_tiempo.Text)
                    ? 5
                    : Convert.ToInt32(txt_tiempo.Text);
                popup.Fecha_Inicio = Convert.ToDateTime(txt_fecha_inicio.Text);
                popup.Fecha_Fin = string.IsNullOrEmpty(txt_fecha_fin.Text)
                    ? (DateTime?)null
                    : Convert.ToDateTime(txt_fecha_fin.Text);
                popup.Estado = true;
                popup.RolesIds = ObtenerRolesSeleccionados();

                if (fud_Adjunto.HasFile)
                {
                    if (conectaAdjuntos)
                    {
                        DataTable dataTable = Int_Popup_BRL.SelectTable(new Int_Popup(), 17);
                        int consecutivo;

                        string Resultado = dataTable.Rows[0][0].ToString();
                        if (string.IsNullOrEmpty(Resultado))
                        {
                            consecutivo = 1;
                        }
                        else
                        {
                            consecutivo = (int)Convert.ToInt64(dataTable.Rows[0].ItemArray.GetValue(0));
                            consecutivo++;
                        }

                        string nombreOriginalArchivo = txt_titulo.Text;
                        string extensionArchivo = Path.GetExtension(fud_Adjunto.FileName);
                        string nombreFinalArchivo = utilidades.AjusteNombreImagenNoticia(
                            nombreOriginalArchivo,
                            consecutivo.ToString(),
                            extensionArchivo
                        );

                        var (guardaImagenLocal, guardaImagenRemota, rutaPopupRemoto) =
                            utilidades.TratamientoNoticias(
                                nombreFinalArchivo,
                                consecutivo.ToString(),
                                rutaPopupsLocal,
                                rutaPopupsRemoto,
                                fud_Adjunto,
                                Id_Usuario,
                                pathLog
                            );

                        imagenPopupRemoto = rutaPopupRemoto;

                        if (guardaImagenRemota && guardaImagenLocal && !string.IsNullOrEmpty(imagenPopupRemoto))
                        {
                            popup.Imagen = imagenPopupRemoto;
                            Int_Popup_BRL.InsertOrUpdate(popup, 2);
                        }
                        else
                        {
                            utilidades.logError(
                                $"{CONST_ERROR}{System.Reflection.MethodBase.GetCurrentMethod().Name}\n" +
                                "Registro POPUP no almacenado en BD. Los archivos no fueron almacenados.",
                                pathLog
                            );
                            if (File.Exists(imagenPopupRemoto))
                                File.Delete(imagenPopupRemoto);
                            if (File.Exists(imagenPopupLocal))
                                File.Delete(imagenPopupLocal);
                        }
                    }
                    else
                    {
                        utilidades.logError(
                            $"{CONST_ERRORCONEXIONSERV} {ipServer}. \n" +
                            $"Método: {System.Reflection.MethodBase.GetCurrentMethod().Name}. \n" +
                            $"Usuario: {Id_Usuario}",
                            pathLog
                        );
                    }
                }

                Page.Response.Redirect(Page.Request.Url.ToString(), false);
            }
            catch (Exception ex)
            {
                utilidades.logError(
                    $"{CONST_ERROR} {System.Reflection.MethodBase.GetCurrentMethod().Name}\n" +
                    $"{ex.Message}\nLos archivos POPUP no fueron almacenados.",
                    pathLog
                );
                if (utilidades.impersonateValidUser())
                {
                    if (File.Exists(imagenPopupRemoto))
                        File.Delete(imagenPopupRemoto);
                    if (File.Exists(imagenPopupLocal))
                        File.Delete(imagenPopupLocal);
                    utilidades.undoImpersonation();
                }
            }
        }

        // ========================================
        // ACTUALIZAR POPUP
        // ========================================
        protected void Actualizar_datos_publicacion(object sender, EventArgs e)
        {
            string imagenPopupRemoto = "";
            string imagenPopupLocal = "";
            AG_Utils utilidades = new AG_Utils();

            try
            {
                pathLog = Server.MapPath(@"~/logs");
                ipServer = ConfigurationManager.AppSettings.Get("IPServerAttach").ToString();
                bool conectaAdjuntos = utilidades.Ping(ipServer);
                string pathServer = Server.MapPath(ConfigurationManager.AppSettings.Get("pathServer"));
                string pathRemote = ConfigurationManager.AppSettings.Get("pathRemote");
                string ambiente = ConfigurationManager.AppSettings.Get("ambiente");
                string rutaPopupsRemoto = $@"{pathRemote}publicaciones\Popup\";
                string rutaPopupsLocal = pathServer + ambiente + $@"\intranet\publicaciones\Popup\";
                string idPopupSeleccionado = Request.Form["rd_estado_vista"];
                string Id_Usuario = Request.QueryString["Id_Usuario"].ToString();

                Int_Popup popup = new Int_Popup();
                popup.Id_Popup = Convert.ToInt32(idPopupSeleccionado);
                popup.Titulo = txt_titulo_pub.Text;
                popup.Descripcion = txt_descripcion_pub.Text;
                popup.Url = txt_url_pub.Text;
                popup.Tiempo_Visualizacion = string.IsNullOrEmpty(txt_tiempo_pub.Text)
                    ? 5
                    : Convert.ToInt32(txt_tiempo_pub.Text);
                popup.Fecha_Inicio = Convert.ToDateTime(txt_fecha_inicio_pub.Text);
                popup.Fecha_Fin = string.IsNullOrEmpty(txt_fecha_fin_pub.Text)
                    ? (DateTime?)null
                    : Convert.ToDateTime(txt_fecha_fin_pub.Text);
                popup.Estado = ddl_estado_pub.SelectedValue == "1";
                popup.RolesIds = ObtenerRolesSeleccionadosActualizar();

                if (fud_Adjunto_pub.HasFile && conectaAdjuntos)
                {
                    string nombreOriginalArchivo = txt_titulo_pub.Text;
                    string extensionArchivo = Path.GetExtension(fud_Adjunto_pub.FileName);
                    string nombreFinalArchivo = utilidades.AjusteNombreImagenNoticia(
                        nombreOriginalArchivo,
                        idPopupSeleccionado,
                        extensionArchivo
                    );

                    var (guardaImagenLocal, guardaImagenRemota, rutaPopupRemoto) =
                        utilidades.TratamientoNoticias(
                            nombreFinalArchivo,
                            idPopupSeleccionado,
                            rutaPopupsLocal,
                            rutaPopupsRemoto,
                            fud_Adjunto_pub,
                            Id_Usuario,
                            pathLog
                        );

                    imagenPopupRemoto = rutaPopupRemoto;

                    if (guardaImagenRemota && guardaImagenLocal && !string.IsNullOrEmpty(imagenPopupRemoto))
                    {
                        popup.Imagen = imagenPopupRemoto;
                    }
                }

                Int_Popup_BRL.InsertOrUpdate(popup, 4);

                Page.Response.Redirect(Page.Request.Url.ToString(), false);
            }
            catch (Exception ex)
            {
                utilidades.logError(
                    $"{CONST_ERROR}{System.Reflection.MethodBase.GetCurrentMethod().Name}\n{ex.Message}",
                    pathLog
                );
                throw;
            }
        }

        // ========================================
        // CARGAR TABLA DE POPUPS
        // ========================================
        protected void cargar_tabla_vista()
        {
            AG_Utils utilidades = new AG_Utils();
            try
            {
                tbl_grupos.Rows.Clear();

                HtmlTableRow th = new HtmlTableRow();
                th.Attributes.Add("class", "th");

                th.Cells.Add(new HtmlTableCell { InnerText = "#" });
                th.Cells.Add(new HtmlTableCell { InnerText = "ID" });
                th.Cells.Add(new HtmlTableCell { InnerText = "TITULO" });
                th.Cells.Add(new HtmlTableCell { InnerText = "DESCRIPCION" });
                th.Cells.Add(new HtmlTableCell { InnerText = "FECHA CREACIÓN" });
                th.Cells.Add(new HtmlTableCell { InnerText = "ESTADO" });
                th.Cells.Add(new HtmlTableCell { InnerText = "ACCION" });

                tbl_grupos.Rows.Add(th);

                Int_Popup popup = new Int_Popup();
                popup.Titulo = txt_filter_grupo.Text;

                DataTable data;

                if (!string.IsNullOrEmpty(txt_filter_grupo.Text))
                {
                    data = Int_Popup_BRL.SelectTable(popup, 9);
                }
                else
                {
                    data = Int_Popup_BRL.SelectTable(new Int_Popup(), 1);
                }

                if (data.Rows.Count > 0)
                {
                    int numPopup = 1;
                    foreach (DataRow row in data.Rows)
                    {
                        HtmlTableRow tableRow = new HtmlTableRow();

                        tableRow.Cells.Add(new HtmlTableCell { InnerText = numPopup.ToString() });
                        tableRow.Cells.Add(new HtmlTableCell { InnerText = row["Id_Popup"].ToString() });
                        tableRow.Cells.Add(new HtmlTableCell { InnerText = row["Titulo"].ToString() });
                        tableRow.Cells.Add(new HtmlTableCell { InnerText = row["Descripcion"].ToString() });
                        tableRow.Cells.Add(new HtmlTableCell { InnerText = row["Fecha_Creacion"].ToString() });

                        HtmlTableCell cellEstado = new HtmlTableCell();
                        bool estado = Convert.ToBoolean(row["Estado"]);
                        cellEstado.InnerHtml = estado
                            ? "<span class='badge badge-success'>● Activo</span>"
                            : "<span class='badge badge-secondary'>○ Inactivo</span>";
                        tableRow.Cells.Add(cellEstado);

                        HtmlTableCell cellAccion = new HtmlTableCell();
                        var rdEstado = new HtmlGenericControl("input");
                        rdEstado.Attributes.Add("Type", "radio");
                        rdEstado.Attributes.Add("name", "rd_estado_vista");
                        rdEstado.Attributes.Add("value", row["Id_Popup"].ToString());
                        cellAccion.Controls.Add(rdEstado);
                        tableRow.Cells.Add(cellAccion);

                        tbl_grupos.Rows.Add(tableRow);
                        numPopup++;
                    }
                }
            }
            catch (Exception ex)
            {
                utilidades.logError(
                    $"{CONST_ERROR}{System.Reflection.MethodBase.GetCurrentMethod().Name}\n{ex.Message}.",
                    pathLog
                );
                throw;
            }
        }

        // ========================================
        // ELIMINAR POPUP
        // ========================================
        protected void Eliminar_Popup(object sender, EventArgs e)
        {
            AG_Utils utilidades = new AG_Utils();
            try
            {
                string pathServer = Server.MapPath(ConfigurationManager.AppSettings.Get("pathServer"));
                string ambiente = ConfigurationManager.AppSettings.Get("ambiente");

                Int_Popup popup = new Int_Popup();
                popup.Id_Popup = Convert.ToInt32(Request.Form["rd_estado_vista"].ToString());

                DataTable dt = Int_Popup_BRL.SelectTable(popup, 3);

                Int_Popup_BRL.InsertOrUpdate(popup, 6);

                if (dt.Rows.Count > 0 && utilidades.impersonateValidUser())
                {
                    string archivoRemoto = dt.Rows[0]["Imagen"].ToString();

                    if (!string.IsNullOrEmpty(archivoRemoto))
                    {
                        var anexoFoto = archivoRemoto.Split('\\');
                        List<string> arrayAnexoFoto = new List<string>(anexoFoto);
                        int index = arrayAnexoFoto.FindIndex(x => x == ambiente);

                        if (index >= 0)
                        {
                            arrayAnexoFoto.RemoveRange(0, index);
                        }

                        string rutaCompleta = string.Join("\\", arrayAnexoFoto);
                        string localTemp = $@"{pathServer}{rutaCompleta}";

                        if (File.Exists(localTemp))
                            File.Delete(localTemp);
                        if (File.Exists(archivoRemoto))
                            File.Delete(archivoRemoto);
                    }

                    utilidades.undoImpersonation();
                }
                else
                {
                    utilidades.logError(
                        $"{CONST_ERROR}{CONST_ERRORPERMISOS}" +
                        $"{System.Reflection.MethodBase.GetCurrentMethod().Name}.",
                        pathLog
                    );
                }

                Page.Response.Redirect(Page.Request.Url.ToString(), false);
            }
            catch (Exception ex)
            {
                utilidades.logError(
                    $"{DateTime.Now}\nError en método: " +
                    $"{System.Reflection.MethodBase.GetCurrentMethod().Name}\n{ex.Message}.",
                    pathLog
                );
                throw;
            }
        }

        // ========================================
        // BÚSQUEDA RÁPIDA
        // ========================================
        protected void txt_filter_grupo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                cargar_tabla_vista();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        // ========================================
        // OBTENER ROLES SELECCIONADOS (CREAR)
        // ========================================
        private string ObtenerRolesSeleccionados()
        {
            if (chkl_roles != null)
            {
                var rolesSeleccionados = new List<string>();
                foreach (ListItem item in chkl_roles.Items)
                {
                    if (item.Selected)
                    {
                        rolesSeleccionados.Add(item.Value);
                    }
                }
                return string.Join(",", rolesSeleccionados);
            }
            return string.Empty;
        }

        // ========================================
        // OBTENER ROLES SELECCIONADOS (ACTUALIZAR)
        // ========================================
        private string ObtenerRolesSeleccionadosActualizar()
        {
            if (chkl_roles_pub != null)
            {
                var rolesSeleccionados = new List<string>();
                foreach (ListItem item in chkl_roles_pub.Items)
                {
                    if (item.Selected)
                    {
                        rolesSeleccionados.Add(item.Value);
                    }
                }
                return string.Join(",", rolesSeleccionados);
            }
            return string.Empty;
        }
    }
}