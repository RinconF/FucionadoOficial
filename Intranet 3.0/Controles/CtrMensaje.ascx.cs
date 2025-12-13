using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Intranet_3._0
{
    public partial class CtrMensaje : System.Web.UI.UserControl
    {
        public Label LabelMensaje1
        {
            get { return lblMensaje1; }
            set { lblMensaje1 = LabelMensaje1; }
        }
        public Label LabelMensaje2
        {
            get { return lblMensaje2; }
            set { lblMensaje2 = LabelMensaje2; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}