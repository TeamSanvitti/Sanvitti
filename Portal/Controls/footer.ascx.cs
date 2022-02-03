namespace avii.Controls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		Summary description for footer.
	/// </summary>
	public class footer : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.LinkButton lnkAdmin;

		private void Page_Load(object sender, System.EventArgs e)
		{
            //if (Session["admin"] != null)
            //    lnkAdmin.Visible = true;
            //else
            //    lnkAdmin.Visible = false;
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
			//this.lnkAdmin.Click += new System.EventHandler(this.lnkAdmin_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void lnkAdmin_Click(object sender, System.EventArgs e)
		{
            Response.Redirect(@"/admin/index.aspx");
		}
	}
}
