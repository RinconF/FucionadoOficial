using System;

namespace Intranet_3._0.Vistas.V_Comites
{
    public partial class CCL : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["Id_Usuario"] != null)
            {
                if (Response.Cookies.Count > 0 && Session["cerrar"] != null)
                {

                }
                else
                {
                    Page.Response.Redirect("~/Login", false);
                }
            }
            else
            {
                Page.Response.Redirect("~/Login", true);
            }
        }
    }
}