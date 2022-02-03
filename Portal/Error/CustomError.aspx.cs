using System;
using System.Web;

namespace avii.Error
{
    public partial class CustomError : System.Web.UI.Page
    {
       // public bool IsAuth { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bool IsAuth = false;
                if (HttpContext.Current.Session["IsAuth"] != null)
                {
                    HttpContext.Current.Session["IsAuth"] = null;
                    lblMsg.Text = "Authentication is missing to access this page.";
                    lblHeader.Text = "Authentication failed";
                }
                else
                {
                    lblHeader.Text = "An Error Has Occurred";
                    lblMsg.Text = "An unexpected error occurred on our website. The website administrator has been notified.";
                }
                //Exception ex1 = Server.GetLastError();
                //if (ex1 != null && ex1.InnerException != null)
                //{
                //    lblMsg.Text = ex1.StackTrace.ToString();
                ////    string url = Convert.ToString(Request.UrlReferrer);
                ////    int userID = 0;
                ////    string source = ex1.Source;
                ////    if (Session["userInfo"] != null)
                ////    {
                ////        avii.Classes.UserInfo objUserInfo = (avii.Classes.UserInfo)Session["userInfo"];
                ////        userID = objUserInfo.UserGUID;
                ////    }
                ////    string innerMsg = ex1.InnerException.Message;
                ////    avii.Classes.CustomErrorOperation.InesrtIntoErrorLog(0, source, url, innerMsg, userID);
                //}
            }
        }
        protected void lnkReturn_Click(object sender, EventArgs e)
        {
            string url = "~/Dashboard.aspx";
            
            Response.Redirect(url);

        }
    }
}