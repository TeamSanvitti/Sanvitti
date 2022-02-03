using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace avii.Controls
{
    public partial class RmaReasons : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindShipVia();
        }
        private void BindShipVia()
        {
            //DataTable dt = avii.Classes.RmaOperations.GetRmaReasons(0);
            //rptReason.DataSource = dt;
            //rptReason.DataBind();

        }
    }
}