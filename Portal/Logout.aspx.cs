using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
namespace avii
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                UserSignInUpdate();
           // string url = Server.MapPath("~/logon.aspx");
            Session.Abandon();
            string url = ConfigurationSettings.AppSettings["LogonPage"].ToString();
            Response.Redirect(url);
            //Response.Redirect("/logon.aspx");
            //Response.Redirect(@"https://www.aerovoice.com/Index.aspx");
        }

        private void UserSignInUpdate()
        {
            if (Session["userInfo"] != null)
            {
                avii.Classes.UserInfo objUserInfo = (avii.Classes.UserInfo)Session["userInfo"];

                avii.Classes.user_utility.UserSignInUpdate(objUserInfo.SignInID, objUserInfo.UserGUID, Session.SessionID);
            }
        }

    }
}