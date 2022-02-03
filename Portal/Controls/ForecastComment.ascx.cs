using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using avii.Classes;
namespace avii.Controls
{
    public partial class ForecastComment : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int forecastID = 0;
                if (Request["fid"] != null)
                {
                    forecastID = Convert.ToInt32(Request["fid"]);
                    BindForecastComments(forecastID);
                }
                if (Request["pid"] != null)
                {
                    
                    BindFulfillmentComments(Convert.ToInt32(Request["pid"]));
                }
                if (Request["rid"] != null && Request["ctype"] != null)
                {
                    
                    BindRMAComments(Convert.ToInt32(Request["rid"]), Request["ctype"].ToString());
                }
            }
        }

        public void BindRMAComments(int rmaGUID, string commentType)
        {
            lblMsg.Text = string.Empty;
            List<RMAComments> commentList = FulfillmentOperations.GetRMAComments(rmaGUID, commentType);
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
        public void BindFulfillmentComments(int poID)
        {
            lblMsg.Text = string.Empty;
            List<FulfillmentComment> commentList = FulfillmentOperations.GetFulfillmentComments(poID);
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

        public void BindForecastComments(int forecastID)
        {
            lblMsg.Text = string.Empty;
            List<ForecastComments> commentList = SKUPricesOperations.GetForecastComments(forecastID);
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