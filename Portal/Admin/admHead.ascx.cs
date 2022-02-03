namespace avii.Admin
{
    using System;
    using System.Configuration;

    /// <summary>
	///		Summary description for admHead.
	/// </summary>
	public class admHead : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Panel pnlmenu;
		protected System.Web.UI.WebControls.Button btnlog;
        protected System.Web.UI.WebControls.Panel pnlAvCustomer;
        protected System.Web.UI.WebControls.Button btnClear;
		private void Page_Load(object sender, System.EventArgs e)
		{
			//btnlog.Attributes.Add("onclick","<script language='javascript'>this.window.close();</script>");
            if (Session["adm"] == null && Session["userInfo"] == null)
			{
				//this.RegisterStartupScript("aa","<script language='javascript'>alert('You donot have access to this webpage');</script>");
				pnlmenu.Visible = false;
                try
                {
                    string url = ConfigurationSettings.AppSettings["LogonPage"].ToString();

                    if (url == null)
                    {
                        url = @"\avii\logon.aspx";
                    }
                    url = url + "?usr=1";
                    Response.Redirect(url);
                }
                catch { }

			}
            else if (Session["adm"] != null)
            {
                string adminFilter = Session["admFilter"] as string;

                if (adminFilter == "2")
                {
                    btnClear.Visible = false;
                    pnlmenu.Visible = false;
                    pnlAvCustomer.Visible = true;
                }
                else
                {
                    btnClear.Visible = true;
                    pnlmenu.Visible = true;
                    pnlAvCustomer.Visible = false;
                }
            }
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

		private void btnlog_Click(object sender, System.EventArgs e)
		{
			Session["adm"] = null;
			Session.Abandon();
			//FormsAuthentication.SignOut();
			pnlmenu.Visible = false;	
			//this.RegisterStartupScript("aa","<script language='javascript'>parent.tdet.location.href = './index.aspx'; top.close();</script>");
			Response.Redirect("http://www.aerovoice.com");
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
			this.btnlog.Click += new System.EventHandler(this.btnlog_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
