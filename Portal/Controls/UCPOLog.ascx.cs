using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace avii.Controls
{
    public partial class UCPOLog : System.Web.UI.UserControl
    {
        //private int pages = 1;
        //public int Pages
        //{
        //    get
        //    {
        //        return pages;
        //    }
        //    set
        //    {
        //        pages = value;
        //    }
        //}
        protected void Page_Load(object sender, EventArgs e)
        {
            if(IsPostBack)
            {
                
            }
        }

        public void BindFulfillmentLog(int poID, int pages)
        {
            lblMsg.Text = string.Empty;
            SV.Framework.Fulfillment.LogOperations logOperations = SV.Framework.Fulfillment.LogOperations.CreateInstance<SV.Framework.Fulfillment.LogOperations>();

            List<SV.Framework.Models.Fulfillment.FulfillmentLogInfo> logList = logOperations.GetFulfillmentLog(poID, pages);
            if (logList != null && logList.Count > 0)
            {
                rptLog.DataSource = logList;
                rptLog.DataBind();
            }
            else
            {
                rptLog.DataSource = null;
                rptLog.DataBind();
                lblMsg.Text = "No log found";
            }
        }
    }
}