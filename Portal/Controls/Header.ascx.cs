namespace avii.Controls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
    using System.Collections.Generic;
	/// <summary>
	///		Summary description for Header.
	/// </summary>
	public class Header : System.Web.UI.UserControl                                        
	{
		protected System.Web.UI.WebControls.Label lblName;
        protected System.Web.UI.WebControls.Label lbUserName;
        //protected System.Web.UI.WebControls.Panel pnlhead;
        //protected System.Web.UI.HtmlControls.HtmlImage imgLogOut;
        protected System.Web.UI.HtmlControls.HtmlGenericControl spnName;

        //protected System.Web.UI.HtmlControls.HtmlInputImage imglogout;

        //protected System.Web.UI.WebControls.Button Button1;

        //protected System.Web.UI.HtmlControls.HtmlInputImage imgLogin;

        private void Page_Load(object sender, System.EventArgs e)
		{
            //Image2.Attributes.Add("onclick","return fnValid();");
            if (!this.IsPostBack)
            {

                if (Session["userInfo"] != null)
                {
                    avii.Classes.UserInfo objUserInfo = (avii.Classes.UserInfo)Session["userInfo"];
                    lblName.Text = "Welcome <strong>" + objUserInfo.UserName + "</strong>(" + objUserInfo.CompanyName + ")";
                    spnName.InnerText = "Hello " + objUserInfo.UserName + ", ";

                }
                else
                {

                }


                //Session["Reset"] = true;
                // Configuration config = WebConfigurationManager.OpenWebConfiguration("~/Web.Config");
                //SessionStateSection section = (SessionStateSection)config.GetSection("system.web/sessionState");

                //int timeout = (int)section.Timeout.TotalMinutes * 1000 * 60;
                int timeouttime = 5;
                int timeout = timeouttime * 1000 * 60;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "onLoad", "DisplaySessionTimeout(" + timeout + ")", true);



            }

        }

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			//this.imgLogin.ServerClick += new System.Web.UI.ImageClickEventHandler(this.imgLogin_ServerClick);
		//	this.imglogout.ServerClick += new System.Web.UI.ImageClickEventHandler(this.imglogout_ServerClick);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion


		private void Image1_ServerClick(object sender, System.Web.UI.ImageClickEventArgs e)
		{
		}

		private void imglogout_ServerClick(object sender, System.Web.UI.ImageClickEventArgs e)
		{
					Session.Abandon();
                    Response.Redirect(@"https://www.aerovoice.com/Index.aspx");
		}


		private void Button1_Click(object sender, System.EventArgs e)
		{
			if (Session["CustID"] != null)
                Response.Redirect(@"/frmCust.aspx?c=" + Session["CustID"].ToString());
		}

		private void imgLogin_ServerClick(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Session.Abandon();
            Response.Redirect(@"https://www.aerovoice.com/logon.aspx");
		}

		private void Image2_ServerClick(object sender, System.Web.UI.ImageClickEventArgs e)
		{
		
		}

        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                avii.Classes.clsDatabaseAction.ClearDBLog();
            }
            catch
            {

            }
            
        }

        protected void btn_logout_Click(object sender, EventArgs e)
        {
            string loginurl = "https://www.aerovoice.com";

            if (Session["userInfo"] == null)
                Response.Redirect(loginurl);
            Session.Abandon();
            Response.Redirect("http://www.aerovoice.com");
        }

	}
}
