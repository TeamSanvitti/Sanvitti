using System;
using System.Web.UI;

namespace avii
{
	/// <summary>
	/// Summary description for news_updates.
	/// </summary>
	public class news_updates : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Panel pnlForm;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!this.IsPostBack)
			{
				Control ct = LoadControl("Controls/Upds.ascx");
				((avii.Controls.Upds)ct).FormType = "N";
				pnlForm.Controls.Add(ct);
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
