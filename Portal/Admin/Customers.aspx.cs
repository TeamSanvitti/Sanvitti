using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace avii.Admin
{
	/// <summary>
	/// Summary description for Customers.
	/// </summary>
	public class Customers : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label lblException;
		protected System.Web.UI.WebControls.DataGrid dgCustomer;
		
		
		private avii.Classes.clsCust  oCust = new avii.Classes.clsCust();
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack )
			{
				DataGridBind();
			}
		}

		protected void dg_ItemCreated(System.Object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
//			if ( e.Item.ItemIndex == 0) 
//			{
//				System.Web.UI.WebControls.LinkButton lb = (System.Web.UI.WebControls.LinkButton)e.Item.Cells[0].Controls[0];
//				if (lb.Text =="Edit") 
//				{
//					lb.Text = "Add";
//				}
//				else if (lb.Text == "Update") 
//				{
//					lb.Text = "Insert" ;
//					//((TextBox)e.Item.FindControl("txtCustType")).Text = string.Empty;
//				}
//			}
		}

		protected void dg_ItemCommand(object sender, DataGridCommandEventArgs e)
		{	
		//	Response.Write( e.CommandName);
			if (e.CommandName.ToLower() == "save")
			{
				oCust.SetRecords(e.Item.Cells[11].Text,((DropDownList)e.Item.Cells[7].Controls[1]).SelectedValue,((DropDownList)e.Item.Cells[9].Controls[1]).SelectedValue);
				DataGridBind();
			}
			if (e.CommandName.ToLower() == "delete")
			{
				oCust.DelCust(e.Item.Cells[11].Text);
				DataGridBind();
			}
		}

		private void DataGridBind()
		{
			Session["custAdmin"] = oCust.GetCustInfo(null);
			dgCustomer.DataSource = ((DataSet)Session["custAdmin"]).Tables[0];
			dgCustomer.DataBind();
		}
		
		protected void dg_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if ( e.Item.ItemType ==ListItemType.AlternatingItem ||  e.Item.ItemType == ListItemType.Item)
			{
				if (e.Item.Cells[7].Controls[0] != null)
				{
					DropDownList dp;
					dp = (DropDownList)e.Item.Cells[7].Controls[1];
					dp.DataSource = ((DataSet)Session["custAdmin"]).Tables[1];
					dp.DataTextField = "CustType";
					dp.DataValueField = "CustTypeID";
					dp.DataBind();
					if (e.Item.Cells[8].Text != "&nbsp;")
						dp.SelectedValue = e.Item.Cells[8].Text;
				}
				if (e.Item.Cells[9].Controls[0] != null)
				{
					DropDownList dp;
					dp = (DropDownList)e.Item.Cells[9].Controls[1];
					dp.DataSource = ((DataSet)Session["custAdmin"]).Tables[2];
					dp.DataTextField = "Employee";
					dp.DataValueField = "SalesID";
					dp.DataBind();
					dp.Items.Insert(0,string.Empty);
					if (e.Item.Cells[10].Text != "&nbsp;")
					{
						if (dp.Items.FindByValue(e.Item.Cells[10].Text) != null)
							dp.SelectedValue = e.Item.Cells[10].Text;
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
			this.Load += new System.EventHandler(this.Page_Load);

		}

		#endregion
	}
}
