using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace avii.Controls
{
    public partial class UserLog : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PopulateUserLogSummary();
        }

        public void PopulateUserLogSummary()
        {
            if (Session["userInfo"] != null)
            {
                avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                List<avii.Classes.UserSignInLog> userSignInLogList;
                if (userInfo != null)
                {
                     
                     if (Session["userlog"] != null)
                         userSignInLogList = (List<avii.Classes.UserSignInLog>)Session["userlog"];
                     //else
                     //    userSignInLogList = avii.Classes.DashboardOperations.GetUserSignInLogSummary(userInfo.UserGUID);
                    //if (userSignInLogList.Count > 0)
                    //{
                    //    rptUser.DataSource = userSignInLogList;
                    //    Session["userlog"] = userSignInLogList;
                    //    lblUser.Text = string.Empty;
                    //}
                    //else
                    //{
                    //    rptUser.DataSource = null;
                    //    lblUser.Text = "No records found";
                    //}
                    //rptUser.DataBind();
                }

            }
        }

    }
}