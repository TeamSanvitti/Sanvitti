using System;
using System.Data;
using System.Web.UI.WebControls;

namespace avii.Admin
{
	/// <summary>
	/// Summary description for frmCust.
	/// </summary>
	public class frmCust : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.DataGrid dgFeatures;

		private DataTable dtCust;
		private Int32 iEditIndex;
		protected System.Web.UI.WebControls.DropDownList dpCust;
		private  avii.Classes.clsCustType oCust = new avii.Classes.clsCustType();

		private void Page_Load(object sender, System.EventArgs e)
		{


			if (!this.IsPostBack)
			{
				string sDefCust;
				datagridBind(1);
				dpCust.DataSource = dtCust;
				dpCust.DataTextField = "CustType";
				dpCust.DataValueField = "CustTypeID";
				dpCust.DataBind();
				sDefCust = oCust.GetDefault();
				if (dpCust.Items.FindByValue(sDefCust)!=null)
				{
					dpCust.SelectedValue = sDefCust;
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
			this.dpCust.SelectedIndexChanged += new System.EventHandler(this.dpCust_SelectedIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	
	
	
		private void datagridBind(Int32 iRowAdd)
		{ 
			if (ViewState["Cust"] == null) 
				dtCust = oCust.GetCustType();
			else
				dtCust = (DataTable)ViewState["Cust"];
			AddTableRows(iRowAdd);
			ViewState["Cust"] = dtCust;
			dgFeatures.DataSource = dtCust;
			dgFeatures.DataBind();

			
			
		}

		private void AddTableRows(Int32 iRowAdd)
		{
			if (ViewState["Cust"] == null) 
				dtCust = oCust.GetCustType();
			else
				dtCust = (DataTable)ViewState["Cust"];
			if (iRowAdd > 0)
			{
				DataRow dRow;
				dRow = dtCust.NewRow();
				dtCust.Rows.InsertAt(dRow,0);
				
			}
		}

		protected void dg_ItemCreated( System.Object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{

			System.Web.UI.WebControls.LinkButton lb;
			if (dgFeatures.CurrentPageIndex ==0) 
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
			dgFeatures.EditItemIndex = -1;
			datagridBind(0);
		}

		protected void dg_Edit( System.Object sender, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			iEditIndex = e.Item.ItemIndex;
			ViewState["EditIndex"] = iEditIndex;
			dgFeatures.EditItemIndex = iEditIndex;
			datagridBind(0);
		}

		protected void dg_Update( System.Object sender, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string sCustType;
			iEditIndex = (Int32)ViewState["EditIndex"];
			sCustType = ((TextBox)e.Item.Cells[2].Controls[1]).Text;
			if (iEditIndex > 0)
				oCust.SetRecords(1,Convert.ToInt32(e.Item.Cells[3].Text.Trim()),sCustType);
			else
				oCust.SetRecords(1,0,sCustType);

			ViewState["Cust"] = dtCust;
			dgFeatures.EditItemIndex = -1;
			datagridBind(1);
		}
		protected void dg_ItemCommand( System.Object sender, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			if (e.CommandName == "delete" && e.Item.ItemIndex > 0)
			{
				if (e.Item.Cells[3].Text != "&nbsp;")
					oCust.SetRecords(2,Convert.ToInt32(e.Item.Cells[3].Text.Trim()),null);
				ViewState["Cust"] = null;
				datagridBind(1);
			}
		}

		private void dpCust_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (dpCust.SelectedValue.Length > 0)
				oCust.SetDefault(dpCust.SelectedValue);
		}
	}
}
