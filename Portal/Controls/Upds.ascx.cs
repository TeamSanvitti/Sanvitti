namespace avii.Controls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		Summary description for Upds.
	/// </summary>
	public class Upds : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.DataList dl;
		protected System.Web.UI.WebControls.Label msg;
		public string FormType = "F";
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!this.IsPostBack)
			{
				msg.Visible = false;
				DataTable oDT = new DataTable();
				 avii.Classes.clsUpdate oUpd = new avii.Classes.clsUpdate();
				oDT = oUpd.SrchUpdate(null,FormType);
				if (oDT != null)
					if (oDT.Rows.Count > 0)
					{
						oDT.DefaultView.RowFilter = "Active = 1";
						if (oDT.DefaultView.Count  > 0)
						{
							dl.DataSource = oDT.DefaultView;
							dl.DataBind();
						}
						else
						{
							msg.Text = "No document exists";
							msg.Visible = true;
						}
					}
					else
					{
						msg.Text = "No document exists";
						msg.Visible = true;

					}
				

			}
		}

			protected void itmCmd( System.Object sender, System.Web.UI.WebControls.DataListItemEventArgs e)
			{

				if (e.Item.FindControl("hlinktop") != null)
				{
					if (((HyperLink)e.Item.FindControl("hlinktop")).NavigateUrl.Length > 0)
					{
						((HyperLink)e.Item.FindControl("hlinktop")).Visible = true;
						((Label)e.Item.FindControl("linktop")).Visible = false;
					}
					else
					{
						((Label)e.Item.FindControl("linktop")).Visible = true;
						((HyperLink)e.Item.FindControl("hlinktop")).Visible = false;
					}
				}
				if (e.Item.FindControl("imgAttachment") != null)
				{
					if (((HyperLink)e.Item.FindControl("imgAttachment")).NavigateUrl.IndexOf("UFA") <= 0)
					{
						((HyperLink)e.Item.FindControl("imgAttachment")).Visible = false;
					}
			
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
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
