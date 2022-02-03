using System;
using System.Web.Routing;

namespace avii 
{
	/// <summary>
	/// Summary description for Global.
	/// </summary>
	public class Global : System.Web.HttpApplication
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		public Global()
		{
			InitializeComponent();
		}
		static void RegisterRoutes(RouteCollection routes)
		{
			routes.MapPageRoute("signin", "signin", "~/logon.aspx");
			routes.MapPageRoute("home", "dashboard", "~/dashboard.aspx");
			routes.MapPageRoute("PoContainer", "Container/FulfillmentContainer", "~/Container/PoContainer.aspx");
			routes.MapPageRoute("POQueryNew", "FulfillmentSearch", "~/POQueryNew.aspx");
			routes.MapPageRoute("POB2C", "FulfillmentB2C", "~/POB2C.aspx");
			routes.MapPageRoute("NewPO", "FulfillmentB2B", "~/NewPO.aspx");
			routes.MapPageRoute("ShippingLabel", "FulfillmentLebel", "~/FulfillmentDetail.aspx");
			routes.MapPageRoute("FulfillmentView", "FulfillmentView", "~/FulfillmentDetails.aspx");
		}
		protected void Application_Start(Object sender, EventArgs e)
		{
			//RegisterRoutes(RouteTable.Routes);
		}
 
		protected void Session_Start(Object sender, EventArgs e)
		{
            
		}

		protected void Application_BeginRequest(Object sender, EventArgs e)
		{

		}

		protected void Application_EndRequest(Object sender, EventArgs e)
		{

		}

		protected void Application_AuthenticateRequest(Object sender, EventArgs e)
		{

		}

		protected void Application_Error(Object sender, EventArgs e)
		{
            Exception ex1 = Server.GetLastError();
            if (ex1 != null && ex1.InnerException != null)
            {
                string url = Convert.ToString(Request.UrlReferrer);
                int userID = 0;
                string source = ex1.Source;

                string innerMsg = ex1.Message + " " + ex1.InnerException.ToString();
                if (Session["userInfo"] != null)
                {
                    avii.Classes.UserInfo objUserInfo = (avii.Classes.UserInfo)Session["userInfo"];
                    userID = objUserInfo.UserGUID;
                }

                avii.Classes.CustomErrorOperation.InesrtIntoErrorLog(0, source, url, innerMsg, userID);
                Server.ClearError();
                Server.Transfer("~/Error/CustomError.aspx");
            }
		}

		protected void Session_End(Object sender, EventArgs e)
		{

		}

		protected void Application_End(Object sender, EventArgs e)
		{

		}
			
		#region Web Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.components = new System.ComponentModel.Container();
		}
		#endregion
	}
}

