using System;
using System.Web.UI;

namespace avii
{
	/// <summary>
	/// Summary description for List.
	/// </summary>
	public class List : System.Web.UI.Page
	{
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected System.Web.UI.WebControls.Panel pnlItem;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!this.IsPostBack)
			{
				 
				Control ct = LoadControl("Controls/ctlPhone.ascx");
//				((ejewel.Controls.ItemList)ct).RecType = Convert.ToString(ViewState["itemType"]);
//				((ejewel.Controls.ItemList)ct).PageCalled = "X";
//				((ejewel.Controls.ItemList)ct).Verb = sVerb;
				pnlItem.Controls.Add(ct);
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
