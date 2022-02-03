using System;
using System.Data;
using System.Web.UI.WebControls;

namespace avii.Admin
{
	/// <summary>
	/// Summary description for frmSales.
	/// </summary>
	public class frmSales : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.DataGrid dgEmp;
	
		private DataTable dtSales = new DataTable();
		private avii.Classes.clsSales  oSales = new avii.Classes.clsSales();
		private Int32 iEditIndex;
		
		private void Page_Load(object sender, System.EventArgs e)
		{
			DataTable dtManuf = new DataTable();
			if (!this.IsPostBack)
			{
				datagridBind(1);			}
		}


		private void datagridBind(Int32 iRowAdd)
		{ 
			if (ViewState["Sale"] == null) 
				dtSales = oSales.GetSales();
			else
				dtSales = (DataTable)ViewState["Sale"];
			AddTableRows(iRowAdd);
			ViewState["Sale"] = dtSales;
			dgEmp.DataSource = dtSales;
			dgEmp.DataBind();
		}

		private void AddTableRows(Int32 iRowAdd)
		{
			if (ViewState["Sale"] == null) 
				dtSales = oSales.GetSales();
			else
				dtSales = (DataTable)ViewState["Sale"];
			if (iRowAdd > 0)
			{
				DataRow dRow;
				dRow = dtSales.NewRow();
				dRow["Active"] = "False";
				dtSales.Rows.InsertAt(dRow,0);
				
			}
		}

		protected void dg_ItemCreated( System.Object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{

			System.Web.UI.WebControls.LinkButton lb;
			if (dgEmp.CurrentPageIndex ==0) 
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
			dgEmp.EditItemIndex = -1;
			datagridBind(0);
		}

		protected void dg_Edit( System.Object sender, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			iEditIndex = e.Item.ItemIndex;
			ViewState["EditIndex"] = iEditIndex;
			dgEmp.EditItemIndex = iEditIndex;
			datagridBind(0);
			ViewState["ID"] = dtSales.Rows[iEditIndex]["SalesID"];
		}

		protected void dg_Update( System.Object sender, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string sName,sActive,sId;
			iEditIndex = (Int32)ViewState["EditIndex"];
			sName = ((TextBox)e.Item.Cells[2].Controls[1]).Text;
			if (((CheckBox)e.Item.Cells[3].Controls[1]).Checked)
				sActive = "1";
			else
				sActive= null;
 
			if (iEditIndex > 0)
				oSales.SetRecords(1,Convert.ToInt32(ViewState["ID"]),sName, sActive);
			else
				oSales.SetRecords(1,0,sName, sActive);

			ViewState["Sale"] = null;
			dgEmp.EditItemIndex = -1;
			datagridBind(1);
		}
	
		protected void dg_ItemCommand( System.Object sender, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			if (e.CommandName == "delete" && e.Item.ItemIndex > 0)
			{
				if (e.Item.Cells[3].Text != "&nbsp;")
					oSales.SetRecords(2,Convert.ToInt32(e.Item.Cells[4].Text.Trim()),null,null);
				ViewState["Sale"] = null;
				datagridBind(1);
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
