using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SV.Framework.Models.Fulfillment;

namespace avii.Controls
{
    public partial class FulfillmentComments : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                int poID = 0;
                if (Request["pid"] != null)
                {
                    poID = Convert.ToInt32(Request["pid"]);
                    BindComments(poID);
                }
            }
        }
        public void BindComments(int poID)
        {
            SV.Framework.Fulfillment.FulfillmentOperations fulfillmentOperations = SV.Framework.Fulfillment.FulfillmentOperations.CreateInstance<SV.Framework.Fulfillment.FulfillmentOperations>();

            lblMsg.Text = string.Empty;
            List<FulfillmentComment> commentList = fulfillmentOperations.GetFulfillmentComments(poID);
            if (commentList != null && commentList.Count > 0)
            {
                rptComments.DataSource = commentList;
                rptComments.DataBind();
            }
            else
            {
                rptComments.DataSource = null;
                rptComments.DataBind();
                lblMsg.Text = "No records found";
            }
        }
    }
}