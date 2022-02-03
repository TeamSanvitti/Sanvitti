using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace avii.Controls
{
    public partial class PoShipVia : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindShipVia();
        }
        private void BindShipVia()
        {
            List<avii.Classes.ShipBy> shippByList = avii.Classes.PurchaseOrder.GetShipByList();
            rptShipVia.DataSource = shippByList;
            rptShipVia.DataBind();

        }
    }
}