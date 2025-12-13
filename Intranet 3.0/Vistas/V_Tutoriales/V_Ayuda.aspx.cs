using System;
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
                // Action 1: Solo tutoriales activos
                Int_Tutoriales obj = new Int_Tutoriales();
                Int_TutorialesCollection tutoriales = Int_Tutoriales_BRL.SelectByParams(obj, 1);

                if (tutoriales != null && tutoriales.Count > 0)
                {
                    rptTutoriales.DataSource = tutoriales;
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
                // Log error
                rptTutoriales.DataSource = null;
                rptTutoriales.DataBind();
                phTutorialesVacio.Visible = true;
            }
        }
    }
}