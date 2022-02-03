using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using avii.Classes;

namespace avii.Controls
{
    public partial class poESN : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void BindESN(int poID, string trackingNumber)
        {
            List<EsnList> esnList = FulfillmentOperations.GetTrackingESNList(poID, trackingNumber);

            if (esnList != null && esnList.Count > 0)
            {
                rptESN.DataSource = esnList;
                rptESN.DataBind();
            }
            else
            {
                rptESN.DataSource = null;
                rptESN.DataBind();
                lblESN.Text = "No records found";
            }
        }

        
    }
}