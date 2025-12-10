using BRL;
using DCL;
using System;
using System.Data;
using System.Web.UI.WebControls;

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
            DataTable dt = Int_Tutoriales_BRL.SelectTable(new Int_Tutoriales { Estado = true }, 1);

            if (dt.Rows.Count == 0)
            {
                pnlSinTutoriales.Visible = true;
                rptTutoriales.Visible = false;
                return;
            }

            pnlSinTutoriales.Visible = false;
            rptTutoriales.Visible = true;
            rptTutoriales.DataSource = dt;
            rptTutoriales.DataBind();
        }

        protected void rptTutoriales_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != System.Web.UI.WebControls.ListItemType.Item &&
                e.Item.ItemType != System.Web.UI.WebControls.ListItemType.AlternatingItem)
            {
                return;
            }

            DataRowView row = (DataRowView)e.Item.DataItem;
            var contenedor = (System.Web.UI.HtmlControls.HtmlGenericControl)e.Item.FindControl("cardTutorial");
            var imgPortada = (Image)e.Item.FindControl("imgPortada");
            var link = (HyperLink)e.Item.FindControl("lnkTutorial");

            if (contenedor != null && row["Seccion"] != DBNull.Value)
            {
                contenedor.Attributes["data-category"] = row["Seccion"].ToString();
            }

            if (imgPortada != null && row["Imagen"] != DBNull.Value && !string.IsNullOrWhiteSpace(row["Imagen"].ToString()))
            {
                imgPortada.ImageUrl = row["Imagen"].ToString();
                imgPortada.Visible = true;
            }

            if (link != null)
            {
                link.NavigateUrl = row["Url"].ToString();
                link.Visible = !string.IsNullOrWhiteSpace(link.NavigateUrl);
            }
        }
    }
}
