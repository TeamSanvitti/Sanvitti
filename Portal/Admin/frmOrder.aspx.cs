using System;
using System.Data;
using System.Web.UI.WebControls;

namespace avii.Admin
{
	/// <summary>
	/// Summary description for frmOrder.
	/// </summary>
	public class frmOrder : System.Web.UI.Page
	{
		#region "Protected Objects"
		protected System.Web.UI.WebControls.TextBox txtOrderNo;
		protected System.Web.UI.WebControls.TextBox txtOrderDt;
		protected System.Web.UI.WebControls.TextBox txtCustLName;
		protected System.Web.UI.WebControls.TextBox txtCustFName;
		protected System.Web.UI.WebControls.Button btnSearch;
		protected System.Web.UI.WebControls.Button btnCancel;
		protected System.Web.UI.WebControls.Label lblOrderDt;
		protected System.Web.UI.WebControls.Label lblCustName;
		protected System.Web.UI.WebControls.Label lblAddr;
		protected System.Web.UI.WebControls.Label lblCity;
		protected System.Web.UI.WebControls.Label lblZip;
		protected System.Web.UI.WebControls.DataGrid dgStatus;
		protected System.Web.UI.WebControls.Button btnClear;
		protected System.Web.UI.WebControls.Panel pnlSearch;
		protected System.Web.UI.WebControls.Panel pnlAdd;
		protected System.Web.UI.WebControls.DataGrid dgOrders;
		#endregion

		private Classes.clsOrder oOrd = new Classes.clsOrder();
		private DataTable oStat = new DataTable();
		protected System.Web.UI.WebControls.Label txtOrderNum;
		private int iEditIndex;

		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!this.IsPostBack)
			{
				if (Request.Params["oid"] != null)
				{
					if (Request.Params["oid"].ToString().Length > 0)
					{
						txtOrderNo.Text = Request.Params["oid"].ToString();
						fnSearch();
						fnGetOrder(txtOrderNo.Text);
						pnlAdd.Visible = true;
					}
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			pnlSearch.Visible = true;
			pnlAdd.Visible = false;
			clearform(true);

		}

		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			pnlSearch.Visible = false;
			pnlAdd.Visible = true;
			dgStatus.DataSource = null;
			dgStatus.CurrentPageIndex = 0;
			dgStatus.DataBind();
		}

		private void btnClear_Click(object sender, System.EventArgs e)
		{
			clearform(false);
			if (Request.Params["oid"] != null)
			{
				txtOrderNo.Text = Request.Params["oid"].ToString();
			}
		}

		private void clearform(bool bSearch)
		{
			txtOrderNum.Text = string.Empty;
			lblOrderDt.Text = string.Empty;
			lblCustName.Text = string.Empty;
			lblAddr.Text = string.Empty;
			lblCity.Text = string.Empty;
			lblZip.Text = string.Empty;
			pnlSearch.Visible = true;
			pnlAdd.Visible = false;
			dgStatus.DataSource = null;
			dgStatus.CurrentPageIndex = 0;
			dgStatus.DataBind();
			if (bSearch) 
			{
				dgOrders.DataSource = null;
				dgOrders.CurrentPageIndex = 0;
				dgOrders.DataBind();	
				txtOrderNo.Text = string.Empty;
				txtOrderDt.Text = string.Empty;
				txtCustLName.Text = string.Empty;
				txtCustFName.Text = string.Empty;
			}
		}



		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			if (pnlSearch.Visible == false)
				pnlSearch.Visible = true;
			else
			{
				fnSearch();
			}
			
		}

		private void fnSearch()
		{
			
			string soNum, soDate, sFName, sLName;
			DataTable oDt;
			soNum = (txtOrderNo.Text.Trim().Length == 0?null:txtOrderNo.Text.Trim());
			soDate = (txtOrderDt.Text.Trim().Length == 0?null:txtOrderDt.Text.Trim());
			sFName = (txtCustLName.Text.Trim().Length == 0?null:txtCustLName.Text.Trim());
			sLName = (txtCustFName.Text.Trim().Length == 0?null:txtCustFName.Text.Trim());

			oDt = oOrd.GetOrderSearch(soNum, soDate, sFName, sLName);
			if (oDt != null)
			{
				if (oDt.Rows.Count > 0)
				{
					dgOrders.CurrentPageIndex= 0;
					dgOrders.DataSource = oDt;
					dgOrders.DataBind();
				}
				else
					this.RegisterStartupScript("aa1","<script languange='javascript'>alert('Order(s) belongs to selected criteria does not exists');</script>");

			}
		}

		private void txtOrderNum_TextChanged(object sender, System.EventArgs e)
		{
			if (txtOrderNum.Text.Trim().Length > 0)
			{
				fnGetOrder(txtOrderNum.Text.Trim());
			}
		}

	
		private void fnGetOrder(string sOrderNum)
		{
			DataSet oDs;
			ViewState["Stat"] = null;
			oDs = oOrd.GetOrderDetail(sOrderNum);
			if (oDs != null)
			{
				if (oDs.Tables.Count  > 0)
				{
					if (oDs.Tables[0].Rows.Count  > 0)
					{
						txtOrderNum.Text = (oDs.Tables[0].Rows[0]["OrderID"] != DBNull.Value?oDs.Tables[0].Rows[0]["OrderID"].ToString():string.Empty);
						lblOrderDt.Text = (oDs.Tables[0].Rows[0]["OrderDate"] != DBNull.Value?oDs.Tables[0].Rows[0]["OrderDate"].ToString():string.Empty);
						lblCustName.Text = (oDs.Tables[0].Rows[0]["CustName"] != DBNull.Value?oDs.Tables[0].Rows[0]["CustName"].ToString():string.Empty);
						lblAddr.Text =  (oDs.Tables[0].Rows[0]["Address"] != DBNull.Value?oDs.Tables[0].Rows[0]["Address"].ToString():string.Empty);
						lblCity.Text =  (oDs.Tables[0].Rows[0]["city"] != DBNull.Value?oDs.Tables[0].Rows[0]["city"].ToString():string.Empty);
						lblZip.Text =  (oDs.Tables[0].Rows[0]["zip"] != DBNull.Value?oDs.Tables[0].Rows[0]["zip"].ToString():string.Empty);
					}
//					if (oDs.Tables[1].Rows.Count  > 0)
//					{
						ViewState["Stat"] = oDs.Tables[1];
						datagridBind(1);
//					}
				}
				else
					this.RegisterStartupScript("aa1","<script languange='javascript'>alert('Order# does not exists');</script>");
			}
		}

		private void datagridBind(Int32 iRowAdd)
		{ 
			if (ViewState["Stat"] == null) 
				oStat = oOrd.GetOrderStatus(txtOrderNum.Text.Trim());
			else
				oStat = (DataTable)ViewState["Stat"];
			if (iRowAdd > 0)
			{
				AddTableRows(iRowAdd);
				ViewState["Stat"] = oStat;
			}
			dgStatus.DataSource = oStat;
			dgStatus.DataBind();
		}

		private void AddTableRows(Int32 iRowAdd)
		{
			if (ViewState["Stat"] == null) 
				oStat = oOrd.GetOrderStatus(txtOrderNum.Text.Trim());
			else
				oStat = (DataTable)ViewState["Stat"];
			if (iRowAdd > 0)
			{
				DataRow dRow;
				dRow = oStat.NewRow();
				oStat.Rows.InsertAt(dRow,0);
				
			}
		}

		protected void dg_ItemCreated( System.Object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{

			System.Web.UI.WebControls.LinkButton lb;
			if (dgStatus.CurrentPageIndex ==0) 
			{
				if (e.Item.ItemIndex == 0)
				{
					lb = (LinkButton)e.Item.Cells[0].Controls[0];
					if (lb.Text == "Edit")
						lb.Text = "Add";
					else if (lb.Text == "Update")
						lb.Text = "Insert";
				}
			}

		}

		protected void dg_Cancel( System.Object sender, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			dgStatus.EditItemIndex = -1;
			datagridBind(0);
		}

		protected void dg_Edit( System.Object sender, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			iEditIndex = e.Item.ItemIndex;
			ViewState["EditIndex"] = iEditIndex;
			dgStatus.EditItemIndex = iEditIndex;
			datagridBind(0);
			ViewState["ID"] = oStat.Rows[iEditIndex]["OrderNum"];
		}

		protected void dg_Update( System.Object sender, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string sONum, sODate, sComment;
			iEditIndex = (Int32)ViewState["EditIndex"];
			sONum = (e.Item.Cells[4].Text=="&nbsp;"?string.Empty:e.Item.Cells[4].Text);
			sONum = (sONum.Length >0?sONum:null);
			sODate = DateTime.Today.ToShortDateString();
			sComment = ((TextBox)e.Item.Cells[3].Controls[1]).Text;

			if (iEditIndex > 0)
				oOrd.SetOrderStatus("1",txtOrderNum.Text,sONum,sComment,sODate);
			else
				oOrd.SetOrderStatus("1",txtOrderNum.Text,sONum,sComment,sODate);
			ViewState["Stat"] = null;
			dgStatus.EditItemIndex = -1;
			datagridBind(1);
		}
	
		protected void dg_ItemCommand( System.Object sender, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			if (e.CommandName == "delete" && e.Item.ItemIndex > 0)
			{
				if (e.Item.Cells[3].Text != "&nbsp;")
					oOrd.SetOrderStatus("2",e.Item.Cells[4].Text.Trim(),null,null,null);
				ViewState["Stat"] = null;
				datagridBind(1);
			}
		}

		protected void dgOrditemcommand( System.Object sender, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			if (e.CommandName == "select")
			{
				if (e.CommandArgument.ToString().Length > 0)
				{
					fnGetOrder(e.CommandArgument.ToString());
					pnlAdd.Visible = true;
				}
			}
		}
	}
}
