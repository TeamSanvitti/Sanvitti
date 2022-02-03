using System;
using System.Data;

namespace avii
{
	/// <summary>
	/// Summary description for OStat.
	/// </summary>
	public class OStat : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button btnSubmit;
		protected System.Web.UI.WebControls.Button btnCancel;
		protected System.Web.UI.WebControls.TextBox txtONum;
		protected System.Web.UI.WebControls.DataGrid dgOrder;
		protected System.Web.UI.WebControls.Label lblErr;
	
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
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnSubmit_Click(object sender, System.EventArgs e)
		{
			if (txtONum.Text.Trim().Length >0) 
			{
				Classes.clsOrder oOrder = new Classes.clsOrder();
				DataTable oDt;
				oDt = oOrder.GetOrderStatus(txtONum.Text.Trim());
				if (oDt.Rows.Count >0)
					dgOrder.DataSource =  oOrder.GetOrderStatus(txtONum.Text.Trim());
				else
				{
					this.RegisterStartupScript("aa","<script language='javascript'>alert('Order does not exist for selected Order#');</script>");
					dgOrder.DataSource = null;
				}
				dgOrder.DataBind();
			}
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			txtONum.Text = string.Empty;
			dgOrder.DataSource= null;
			dgOrder.DataBind();
		}
	}
}
