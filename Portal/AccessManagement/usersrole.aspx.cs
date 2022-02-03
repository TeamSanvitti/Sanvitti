using System;

namespace avii.AccessManagement
{
    public partial class usersrole : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Session["adm"] == null)
            //{
            //    string url = "/avii/logon.aspx";
            //    try
            //    {
            //        url = System.Configuration.ConfigurationSettings.AppSettings["LogonPage"].ToString();

            //    }
            //    catch
            //    {
            //        url = "/avii/logon.aspx";
            //    }
            //    //if (Session["UserID"] == null)
            //    {
            //        Response.Redirect(url);
            //    }
            //}
            if (!IsPostBack)
            {
                if (Request["userid"] != null && Request["userid"] != "")
                {
                    int userguid = Convert.ToInt32(Request["userid"]);
                    getRoles(userguid);
                }
            }
        }
        protected void getRoles(int userid)
        {
            avii.Classes.user_utility objroles = new avii.Classes.user_utility();
            gvRoles.DataSource = objroles.getUserRolelist(userid);
            gvRoles.DataBind();
        }

    }
}
