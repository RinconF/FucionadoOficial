using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DCL;
using BRL;
using System.Web.UI.HtmlControls;

namespace OfertaEmpleo
{
    public partial class Questions_Observacion : Page
    {

        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["Id_Usuario"] == null)
            {
                Page.Response.Redirect("~/Login", false);
                return;
            }
            if (Response.Cookies.Count == 0 && Session["cerrar"] == null)
            {
                Page.Response.Redirect("~/Login", false);
                return;
            }
        }

        protected void btn_siguientes_datos(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtObservacion.Value))
            {
                DCL.ENC_Respuesta obj = new ENC_Respuesta();
                obj.Respuesta = txtObservacion.Value;
                obj.Id_Usuario_Responde = Convert.ToInt32(Session["Id_Ingreso"]);
                ENC_Respuesta_BRL.InsertOrUpdate(obj, 13);
            }
            Response.Redirect("/Vistas/V_Encuesta_Organizacional/Question_Observaciones.aspx?Id_Usuario=" + Request.QueryString["Id_Usuario"] + "&Id_Grupo=" + Request.QueryString["Id_Grupo"] + (String.IsNullOrEmpty(Request.QueryString["Id_Vista"]) ? "" : "Id_Vista=" + Request.QueryString["Id_Vista"]), false);
        }
        }
}