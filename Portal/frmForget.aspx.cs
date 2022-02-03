using System;

namespace avii
{
	/// <summary>
	/// Summary description for frmForget.
	/// </summary>
	public class frmForget : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.TextBox txtUser;
		protected System.Web.UI.WebControls.TextBox txtEmail;
		protected System.Web.UI.WebControls.Button btnSubmit;
        protected System.Web.UI.WebControls.CheckBox chkFull;
	
		private avii.Classes.clsRegis oCust = new avii.Classes.clsRegis();

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnSubmit_Click(object sender, System.EventArgs e)
		{
			if (txtUser.Text.Trim().Length > 0 && txtEmail.Text.Trim().Length > 0)
			{
                if (chkFull.Checked == false)
                {
                    if (oCust.iValidateLogin(txtUser.Text.Trim(), txtEmail.Text.Trim()) > 0)
                    {
                        if (Classes.clsRegis.ChangePwd(txtUser.Text.Trim(), txtEmail.Text.Trim(), Server.MapPath(".//Emails") + @"\" + "ChgPwd.htm") >= 0)
                        {
                            txtUser.Text = string.Empty;
                            txtEmail.Text = string.Empty;
                            this.RegisterStartupScript("aa", "<script language=javascript>alert('Password has been changed and send to your email address.\n\nPlease login to website using New Password.');</script>");
                        }
                    }
                    else
                    {
                        this.RegisterStartupScript("aa", "<script language=javascript>alert('User does not exist in the system');</script>");
                    }
                }
                else
                {
                    if (oCust.iValidateFullfillmentLogin(txtUser.Text.Trim(), txtEmail.Text.Trim()) > 0)
                    {
                        if (Classes.clsRegis.GetFullfillmentPwd(txtUser.Text.Trim(), txtEmail.Text.Trim(), Server.MapPath(".//Emails") + @"\" + "ChgPwd.htm") >= 0)
                        {
                            txtUser.Text = string.Empty;
                            txtEmail.Text = string.Empty;
                            this.RegisterStartupScript("aa", "<script language=javascript>alert('Password has been changed and send to your email address.\n\nPlease login to website using New Password.');</script>");
                        }
                    }
                    else
                    {
                        this.RegisterStartupScript("aa", "<script language=javascript>alert('User does not exist in the system');</script>");
                    }
                }
			}
		
		}

        protected void btnSubmit_Click1(object sender, EventArgs e)
        {

        }
	}
}
