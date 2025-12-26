using System;
using System.Data;
using System.Web.UI;
using BRL;
using DCL;

namespace Intranet_3._0.Vistas.V_Tutoriales
{
    public partial class V_Ayuda : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarTutoriales();
            }
        }

        private void CargarTutoriales()
        {
            try
            {
                string rolesUsuario = ObtenerRolesUsuarioActual();

                if (string.IsNullOrEmpty(rolesUsuario))
                {
                    MostrarMensajeSinRoles();
                    return;
                }

                Int_Tutoriales filtro = new Int_Tutoriales
                {
                    Estado = true,
                    Roles = rolesUsuario
                };

                DataTable dt = Int_Tutoriales_BRL.SelectTable(filtro, 1);

                if (dt == null)
                {
                    dt = new DataTable();
                }

                AsegurarColumnas(dt);

                if (!dt.Columns.Contains("VideoProcesado"))
                {
                    dt.Columns.Add("VideoProcesado", typeof(string));
                }

                foreach (DataRow row in dt.Rows)
                {
                    string video = row["Video"].ToString();
                    row["VideoProcesado"] = ProcesarUrlVideo(video);
                }

                if (dt.Rows.Count > 0)
                {
                    rptTutoriales.DataSource = dt;
                    rptTutoriales.DataBind();
                    phTutorialesVacio.Visible = false;
                }
                else
                {
                    rptTutoriales.DataSource = null;
                    rptTutoriales.DataBind();
                    phTutorialesVacio.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }
        }

        // =============================================
        // MÉTODO CORREGIDO - OBTENER ROL DEL USUARIO
        // =============================================
        private string ObtenerRolesUsuarioActual()
        {
            try
            {
                // ==========================================
                // OPCIÓN 1: Variables más comunes (PROBAR PRIMERO)
                // ==========================================

                // Intento 1: Id_Rol (MÁS COMÚN)
                if (Session["Id_Rol"] != null)
                    return Session["Id_Rol"].ToString();

                // Intento 2: IdRol (sin guión bajo)
                if (Session["IdRol"] != null)
                    return Session["IdRol"].ToString();

                // Intento 3: Objeto Usuario con propiedad Id_Rol
                if (Session["Usuario"] != null)
                {
                    try
                    {
                        var usuario = Session["Usuario"];
                        var idRolProp = usuario.GetType().GetProperty("Id_Rol");
                        if (idRolProp != null)
                        {
                            var valor = idRolProp.GetValue(usuario, null);
                            if (valor != null)
                                return valor.ToString();
                        }

                        // También intentar sin guión bajo
                        var idRolProp2 = usuario.GetType().GetProperty("IdRol");
                        if (idRolProp2 != null)
                        {
                            var valor = idRolProp2.GetValue(usuario, null);
                            if (valor != null)
                                return valor.ToString();
                        }
                    }
                    catch { }
                }

                // ==========================================
                // OPCIÓN 2: Otras variables posibles
                // ==========================================

                if (Session["Rol"] != null)
                    return Session["Rol"].ToString();

                if (Session["RolId"] != null)
                    return Session["RolId"].ToString();

                if (Session["Roles"] != null)
                    return Session["Roles"].ToString();

                if (Session["id_rol"] != null)
                    return Session["id_rol"].ToString();

                // ==========================================
                // OPCIÓN 3: Objeto UsuarioActual
                // ==========================================

                if (Session["UsuarioActual"] != null)
                {
                    try
                    {
                        var usuario = Session["UsuarioActual"];
                        var idRolProp = usuario.GetType().GetProperty("Id_Rol");
                        if (idRolProp != null)
                        {
                            var valor = idRolProp.GetValue(usuario, null);
                            if (valor != null)
                                return valor.ToString();
                        }

                        var idRolProp2 = usuario.GetType().GetProperty("IdRol");
                        if (idRolProp2 != null)
                        {
                            var valor = idRolProp2.GetValue(usuario, null);
                            if (valor != null)
                                return valor.ToString();
                        }
                    }
                    catch { }
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        private void MostrarMensajeSinRoles()
        {
            rptTutoriales.DataSource = null;
            rptTutoriales.DataBind();
            phTutorialesVacio.Visible = true;
        }

        private void MostrarError(string mensaje)
        {
            rptTutoriales.DataSource = null;
            rptTutoriales.DataBind();
            phTutorialesVacio.Visible = true;
        }

        private void AsegurarColumnas(DataTable dt)
        {
            if (dt == null) return;

            string[] nombres = {
                "Id_Tutorial", "Titulo", "Descripcion", "Video", "Seccion",
                "Fecha_Creacion", "Fecha_Actualizacion", "Usuario_Creacion",
                "Usuario_Actualizacion", "Estado"
            };

            Type[] tipos = {
                typeof(int), typeof(string), typeof(string), typeof(string), typeof(string),
                typeof(DateTime), typeof(DateTime), typeof(int), typeof(int), typeof(bool)
            };

            for (int i = 0; i < nombres.Length; i++)
            {
                if (!dt.Columns.Contains(nombres[i]))
                {
                    dt.Columns.Add(nombres[i], tipos[i]);
                }
            }
        }

        private string ProcesarUrlVideo(string video)
        {
            if (string.IsNullOrWhiteSpace(video)) return "#";
            if (video.StartsWith("~/")) return ResolveUrl(video);
            if (video.StartsWith("/")) return video;
            if (video.StartsWith("http://") || video.StartsWith("https://")) return video;
            return "/" + video;
        }

        protected string ObtenerIconoSeccion(object seccion)
        {
            if (seccion == null || seccion == DBNull.Value)
                return "fas fa-video fa-3x";

            string sec = seccion.ToString().ToUpper().Trim();

            switch (sec)
            {
                case "GENERAL": return "fas fa-graduation-cap fa-3x";
                case "VENTAS": return "fas fa-chart-line fa-3x";
                case "MARKETING": return "fas fa-bullhorn fa-3x";
                case "RECURSOS HUMANOS": return "fas fa-users fa-3x";
                case "FINANZAS": return "fas fa-dollar-sign fa-3x";
                case "TI":
                case "TECNOLOGÍA DE INFORMACIÓN": return "fas fa-laptop-code fa-3x";
                default: return "fas fa-video fa-3x";
            }
        }

        protected bool TieneVideo(object video)
        {
            if (video == null || video == DBNull.Value) return false;
            string videoStr = video.ToString();
            return !string.IsNullOrWhiteSpace(videoStr) && videoStr != "#";
        }

        protected string GenerarBotonVideo(object videoProcesado, object titulo)
        {
            if (!TieneVideo(videoProcesado))
                return "<span class='btn btn-disabled'>Sin video</span>";

            string url = videoProcesado.ToString();
            string tituloVideo = titulo != null && titulo != DBNull.Value
                ? System.Web.HttpUtility.HtmlAttributeEncode(titulo.ToString())
                : "Ver tutorial";

            return $"<a href='{url}' class='btn btn-ver-video' target='_blank' title='{tituloVideo}'><i class='fas fa-play'></i> Ver Tutorial</a>";
        }
    }
}