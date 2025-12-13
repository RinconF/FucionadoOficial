using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Intranet_3._0
{
    public partial class HorarioVacio : System.Web.UI.UserControl
    {
        public Label LabelInfoProd
        {
            get { return lblInfoProd; }
            set { lblInfoProd = LabelInfoProd; }
        }

        public Label LabelInfoAsig
        {
            get { return lblInfoAsig; }
            set { lblInfoAsig = LabelInfoAsig; }
        }

        public Label LabelFecha
        {
            get { return lblFecha; }
            set { lblFecha = LabelFecha; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}