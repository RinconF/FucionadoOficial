using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Intranet_3._0
{
    public partial class HorarioDia : System.Web.UI.UserControl
    {
        public Label LabelFecha
        {
            get { return lblFecha; }
            set { lblFecha = LabelFecha; }
        }

        public Label LabelAsignacion
        {
            get { return lblInfoAsig; }
            set { lblInfoAsig = LabelAsignacion; }
        }

        public Label labelAmplitud
        {
            get { return lblInfoAmpli; }
            set { lblInfoAmpli = labelAmplitud; }
        }

        public Label labelProd
        {
            get { return lblInfoProd; }
            set { lblInfoProd = labelProd; }
        }

        public Label labelpar
        {
            get { return lblpar; }
            set { lblpar = labelpar; }
        }

        public Label labelpar2
        {
            get { return lblpar2; }
            set { lblpar2 = labelpar2; }
        }

        public GridView gvHorOne
        {
            get { return gvPartOne; }
            set { gvPartOne = gvHorOne; }
        }

        public GridView gvHorTwo
        {
            get { return gvPartTwo; }
            set { gvPartTwo = gvHorTwo; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}