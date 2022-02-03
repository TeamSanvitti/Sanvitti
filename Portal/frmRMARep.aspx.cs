using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace avii.Admin
{
	/// <summary>
	/// Summary description for frmRMARep.
	/// </summary>
	public class frmRMARep : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.TextBox txtDate;
		protected System.Web.UI.WebControls.TextBox txtComp;
		protected System.Web.UI.WebControls.TextBox txtAccNum;
		protected System.Web.UI.WebControls.Button btnSrch;
		protected System.Web.UI.WebControls.Button btnCancel;
		protected System.Web.UI.WebControls.DropDownList dpStatus;
		protected System.Web.UI.WebControls.DataGrid dgRMA;
		protected System.Web.UI.WebControls.TextBox txtInv;
	
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
			this.btnSrch.Click += new System.EventHandler(this.btnSrch_Click);
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			txtDate.Text = string.Empty;
			txtComp.Text = string.Empty;
			txtAccNum.Text = string.Empty;
			txtInv.Text = string.Empty;
			dpStatus.SelectedIndex = 0;
			dgRMA.DataSource = null;
			dgRMA.DataBind();
		}

		private void btnSrch_Click(object sender, System.EventArgs e)
		{
			string sDate, sComp, sAcc, sInv,  sStat;
			//sp_RMA_List
			sDate = (txtDate.Text.Trim().Length > 0?txtDate.Text:null);
			sComp = (txtComp.Text.Trim().Length > 0?txtComp.Text:null);
			sAcc = (txtAccNum.Text.Trim().Length > 0?txtAccNum.Text:null);
			sInv = (txtInv.Text.Trim().Length > 0?txtInv.Text:null);
			sStat = (dpStatus.SelectedValue.Trim().Length > 0?dpStatus.SelectedValue:null);

			DataTable oDt = new DataTable();
			oDt = Classes.clsRIMA.GetRMAList(sDate, sComp, sAcc, sInv, sStat);
			dgRMA.DataSource = oDt;
			dgRMA.DataBind();


		}
	}
}
