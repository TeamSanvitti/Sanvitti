using System;
using System.Data;
using System.Web.UI.WebControls;

namespace avii.Admin
{
	/// <summary>
	/// Summary description for frmtype.
	/// </summary>
	public class frmtype : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.DataGrid dgType;

		private Classes.clsPType  oPtype = new Classes.clsPType();
		private Int32 iEditIndex;
		private DataTable dtType;
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!this.IsPostBack)
			{
				datagridBind(1);
			}
		}

		private void datagridBind(Int32 iRowAdd)
		{ 
			if (ViewState["Type"] == null) 
				dtType = oPtype.GetPType();
			else
				dtType = (DataTable)ViewState["Type"];
			AddTableRows(iRowAdd);
			ViewState["Type"] = dtType;
			dgType.DataSource = dtType;
			dgType.DataBind();
		}

		private void AddTableRows(Int32 iRowAdd)
		{
			if (ViewState["Type"] == null) 
				dtType = oPtype.GetPType();
			else
				dtType = (DataTable)ViewState["Type"];
			if (iRowAdd > 0)
			{
				DataRow dRow;
				dRow = dtType.NewRow();
				dtType.Rows.InsertAt(dRow,0);
				
			}
		}

		protected void dg_ItemCreated( System.Object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{

			System.Web.UI.WebControls.LinkButton lb;
			if (dgType.CurrentPageIndex ==0) 
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
			dgType.EditItemIndex = -1;
			datagridBind(0);
		}

		protected void dg_Edit( System.Object sender, System.Web.UI.WebControls.DataGridCommandEventArgs  e)
		{
			iEditIndex = e.Item.ItemIndex;
			ViewState["EditIndex"] = iEditIndex;
			dgType.EditItemIndex = iEditIndex;
			datagridBind(0);
			ViewState["ID"] = dtType.Rows[iEditIndex]["ID"];
		}

		protected void dg_Update( System.Object sender, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string sDesc, sType, sID;
			iEditIndex = (Int32)ViewState["EditIndex"];
			sDesc = ((TextBox)e.Item.Cells[2].Controls[1]).Text;
			 sType = ((System.Web.UI.WebControls.DropDownList)e.Item.Cells[3].Controls[1]).SelectedValue;
			if (iEditIndex > 0)
				oPtype.SetRecords(1,Convert.ToInt32(ViewState["ID"]),sDesc,sType);
			else
				oPtype.SetRecords(1,0,sDesc,sType);

			ViewState["Type"] = dtType;
			dgType.EditItemIndex = -1;
			datagridBind(1);
		}
	
		protected void dg_ItemCommand( System.Object sender, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			if (e.CommandName == "delete" && e.Item.ItemIndex > 0)
			{
				if (e.Item.Cells[4].Text != "&nbsp;")
					oPtype.SetRecords(2,Convert.ToInt32(e.Item.Cells[4].Text.Trim()),null,null);
				ViewState["Type"] = null;
				datagridBind(1);
			}
		}


		protected void dg_ItemBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if ( e.Item.ItemType ==ListItemType.AlternatingItem ||  e.Item.ItemType == ListItemType.Item)
			{
				if (dgType.EditItemIndex > 0)
				{
//					if (e.Item.Cells[3].Controls[1] != null)
//					{
						DropDownList dp;
						if (dgType.FindControl("dpType") != null)
						{
							dp = (DropDownList)e.Item.FindControl("dpType");
							////						dp.DataSource = ((DataSet)Session["custAdmin"]).Tables[1];
							////						dp.DataTextField = "CustType";
							////						dp.DataValueField = "CustTypeID";
							////						dp.DataBind();
							////						if (e.Item.Cells[8].Text != "&nbsp;")
													dp.SelectedValue = "A";
						}
//					}
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
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
