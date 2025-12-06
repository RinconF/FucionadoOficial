using System;
using System.Web.UI;

namespace OfertaEmpleo.Questions
{
    public partial class Question_Observaciones : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
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
                ScriptManager.RegisterStartupScript(this, GetType(), "Pop", "disabled_img();", true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void btn_reload(object sender, EventArgs e)
        {
            try
            {
                Session.Remove("ini");
                Session.Remove("fin");
                Session.Remove("Id_Ingreso");
                Session.Remove("operador");
                Response.Redirect("/Default.aspx?Id_Usuario=" + Request.QueryString["Id_Usuario"], false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}