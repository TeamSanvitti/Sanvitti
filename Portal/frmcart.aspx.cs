using System;

namespace avii
{
	/// <summary>
	/// Summary description for frmcart.
	/// </summary>
	public class frmcart : System.Web.UI.Page
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (Session["User"] == null)
                Response.Redirect(@"/logon.aspx");

		
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
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
