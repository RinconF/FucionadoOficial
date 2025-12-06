using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using BRL;
using DCL;

namespace Intranet_3._0.Vistas.V_Perfil
{
    public partial class V_Aplicativos : System.Web.UI.Page
    {
        private const string SECCION_EMPRESARIALES = "EMPRESARIALES";
        private const string SECCION_CONSULTA = "CONSULTA";
        private const string SECCION_SOPORTE = "SOPORTE";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarAplicativos();
            }
        }

        private void CargarAplicativos()
        {
            DataTable dt = Int_Aplicativos_BRL.SelectTable(new Int_Aplicativo { Estado = true }, 1);
            if (dt == null)
            {
                dt = new DataTable();
            }

            AsegurarColumnas(dt);

            if (!dt.Columns.Contains("UrlProcesada"))
            {
                dt.Columns.Add("UrlProcesada", typeof(string));
            }

            string idUsuario = Request.QueryString["Id_Usuario"];

            foreach (DataRow row in dt.Rows)
            {
                string url = row["Url"].ToString();
                row["UrlProcesada"] = ProcesarUrl(url, idUsuario);

                if (string.IsNullOrWhiteSpace(row["Imagen"].ToString()))
                {
                    row["Imagen"] = "/Content/img/etib.png";
                }
            }

            BindSeccion(dt, SECCION_EMPRESARIALES, rptEmpresariales, phEmpresarialesVacio);
            BindSeccion(dt, SECCION_CONSULTA, rptConsulta, phConsultaVacio);
            BindSeccion(dt, SECCION_SOPORTE, rptSoporte, phSoporteVacio);
        }

        private void AsegurarColumnas(DataTable dt)
        {
            if (dt == null)
            {
                return;
            }

            string[] nombres = { "Id_Aplicativo", "Titulo", "Descripcion", "Imagen", "Url", "Seccion", "Fecha_Creacion", "Fecha_Actualizacion", "Estado" };
            Type[] tipos = { typeof(int), typeof(string), typeof(string), typeof(string), typeof(string), typeof(string), typeof(DateTime), typeof(DateTime), typeof(bool) };

            for (int i = 0; i < nombres.Length; i++)
            {
                if (!dt.Columns.Contains(nombres[i]))
                {
                    dt.Columns.Add(nombres[i], tipos[i]);
                }
            }
        }

        private void BindSeccion(DataTable dt, string seccion, System.Web.UI.WebControls.Repeater repeater, System.Web.UI.WebControls.PlaceHolder placeholder)
        {
            var datos = dt.AsEnumerable().Where(r => string.Equals(r["Seccion"].ToString(), seccion, StringComparison.OrdinalIgnoreCase));
            if (datos.Any())
            {
                repeater.DataSource = datos.CopyToDataTable();
                repeater.DataBind();
                placeholder.Visible = false;
            }
            else
            {
                repeater.DataSource = null;
                repeater.DataBind();
                placeholder.Visible = true;
            }
        }

        private string ProcesarUrl(string url, string idUsuario)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                return "#";
            }

            if (!string.IsNullOrWhiteSpace(idUsuario))
            {
                url = url.Replace("{Id_Usuario}", idUsuario);
            }

            return url;
        }
    }
}
