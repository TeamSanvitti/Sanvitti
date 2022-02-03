using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using avii.Classes;

namespace avii.Controls
{
    public partial class SimLogControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void BindSimlog(string sim)
        {
            lblLog.Text = string.Empty;
            try
            {
                List<SimLog> simLog = SimOperations.GetSimLogList(sim);
                if (simLog != null && simLog.Count > 0)
                {
                    rptSimLog.DataSource = simLog;
                    rptSimLog.DataBind();

                }
                else
                {
                    rptSimLog.DataSource = null;
                    rptSimLog.DataBind();
                    lblLog.Text = "No log found.";

                }
            }
            catch (Exception ex)
            {
                lblLog.Text = ex.Message;
            }

        }
    }
}