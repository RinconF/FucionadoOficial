using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BRL;

namespace Intranet_3._0.Vistas.V_Gestion_Humana
{
    public partial class V_Directorio_Empresarial : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.QueryString["Id_Usuario"] != null)
            {
                if (!IsPostBack)
                {
                    cargar_dropdownlist();
                }
            }
            else
            {
                Page.Response.Redirect("~/Login", true);
            }
        }
        protected void cargar_dropdownlist()
        {
            try
            {
                drop_une.Items.Clear();

                   DataTable dt;
                DCL.Int_Usuarios obj = new DCL.Int_Usuarios();
                dt = Int_Usuarios_BRL.SelectTable(obj, 37);

                drop_une.DataSource = dt;
                drop_une.DataTextField = "Desc_Sede";
                drop_une.DataValueField = "Id_Sede";
                drop_une.DataBind();
                drop_une.Items.Insert(0, new ListItem("-Seleccione UNE-", "0"));

                //string une = Request.QueryString["Une"].ToString();
                //drop_une.SelectedValue = une;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        protected void cargar_card_colaborador(object sender,  EventArgs e)
        {
            try
            {

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}