using System;
using System.Data;
using System.Web.UI.WebControls;

namespace avii.Admin
{
	/// <summary>
	/// Summary description for frmRMARep.
	/// </summary>
	public class frmRMARep : System.Web.UI.Page
	{

        protected System.Web.UI.WebControls.Label lblError;
        protected System.Web.UI.WebControls.TextBox txtDate;
		protected System.Web.UI.WebControls.TextBox txtComp;
		protected System.Web.UI.WebControls.TextBox txtAccNum;
        protected System.Web.UI.WebControls.Button btnSrch;
        protected System.Web.UI.WebControls.Button btnSubmitStatus;
		protected System.Web.UI.WebControls.Button btnCancel;
        protected System.Web.UI.WebControls.DropDownList dpStatus;
        protected System.Web.UI.WebControls.DropDownList dpStatusQ;
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

		}

		private void btnSrch_Click(object sender, System.EventArgs e)
		{
            lblError.Text = string.Empty;
            fnSearch();
		}

		private void fnSearch()
		{
			string sDate, sComp, sAcc, sInv,  sStat;
			sDate = (txtDate.Text.Trim().Length > 0?txtDate.Text:null);
			sComp = (txtComp.Text.Trim().Length > 0?txtComp.Text:null);
			sAcc = (txtAccNum.Text.Trim().Length > 0?txtAccNum.Text:null);
			sInv = (txtInv.Text.Trim().Length > 0?txtInv.Text:null);
            sStat = (this.dpStatusQ.SelectedValue.Trim().Length > 0 ? dpStatusQ.SelectedValue : null);

			DataTable oDt = new DataTable();
			oDt = Classes.clsRIMA.GetRMAList(sDate, sComp, sAcc, sInv, sStat);
            if (oDt.Rows.Count > 0)
            {
                btnSubmitStatus.Visible = true;
                dgRMA.DataSource = oDt;
                dgRMA.DataBind();
            }
            else
            {
                btnSubmitStatus.Visible = false;
                lblError.Text = "No matching record found";
                dgRMA.DataSource = null;
                dgRMA.DataBind();
            }
		}


		protected void dg_delete( System.Object sender, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			if (e.Item.Cells[3].Text != "&nbsp;") 
			{
				Classes.clsRIMA.DeleteRecord(e.Item.Cells[2].Text.Trim());
				fnSearch();
			}
		}

        protected void btnSrch_Click2(object sender, EventArgs e)
        {
            lblError.Text = string.Empty;
            fnSearch();
        }

        protected void btnCancel_Click1(object sender, EventArgs e)
        {
            btnSubmitStatus.Visible = false;
            lblError.Text = string.Empty;
            txtDate.Text = string.Empty;
            txtComp.Text = string.Empty;
            txtAccNum.Text = string.Empty;
            txtInv.Text = string.Empty;
            dpStatusQ.SelectedIndex = 0;
            dgRMA.DataSource = null;
            dgRMA.DataBind();
        }

        protected void dgRMA_DataBound(object sender, DataGridItemEventArgs e)
        {
                 
            //Check if this is our Blank Row being databound, if so make the row invisible
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                object ctrl = e.Item.FindControl("dpStatus");
                if (ctrl != null)
                {
                    DropDownList dp = (DropDownList)ctrl;
                    DataRowView dRowView = e.Item.DataItem as DataRowView;

                    if (dRowView != null)
                    {
                        dp.SelectedValue = dRowView.Row["Status"].ToString();
                    }
                }
            }
        }

        protected void btnSubmitStatus_Click(object sender, EventArgs e)
        {
            foreach (DataGridItem itm in dgRMA.Items)
            {
                DropDownList dp = (DropDownList)itm.FindControl("dpStatus");
                if (dp != null && dp.SelectedValue.ToString() != "")
                {
                    avii.Classes.clsRIMA.SetRMAStatus(Convert.ToInt32(dgRMA.DataKeys[itm.DataSetIndex]), Convert.ToInt32(dp.SelectedValue));
                }
            }
        }



	}
}
