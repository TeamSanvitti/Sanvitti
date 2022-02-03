using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace avii
{
    public partial class SessionTimeout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {

                
                Session["Reset"] = true;
                // Configuration config = WebConfigurationManager.OpenWebConfiguration("~/Web.Config");
                //SessionStateSection section = (SessionStateSection)config.GetSection("system.web/sessionState");

                //int timeout = (int)section.Timeout.TotalMinutes * 1000 * 60;
                int timeouttime = 5;
                int timeout = timeouttime * 1000 * 60;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "onLoad", "DisplaySessionTimeout(" + timeout + ")", true);



            }

        }
    }
}