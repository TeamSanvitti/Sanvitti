using System;
using System.Web.UI.WebControls;

namespace avii.Admin
{
	/// <summary>
	/// Summary description for frmItmLst.
	/// </summary>
	public class frmItmLst : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.DropDownList dpPType;
		protected System.Web.UI.WebControls.DropDownList dpManu;
		protected System.Web.UI.WebControls.CheckBox chkNew;
		protected System.Web.UI.WebControls.CheckBox chkUsed;
		protected System.Web.UI.WebControls.CheckBox chkRef;
		protected System.Web.UI.WebControls.Button btnSrch;
		protected System.Web.UI.WebControls.Button btnCancel;
		protected System.Web.UI.WebControls.DataGrid dgItems;
		protected System.Web.UI.WebControls.Button btnAdd;
		protected System.Web.UI.WebControls.TextBox txtModel;
		protected System.Web.UI.WebControls.Label lblErr;
	avii.Classes.clsItem clsItem = new avii.Classes.clsItem();

		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!this.IsPostBack)
			{
				avii.Classes.clsManuf oManuf = new avii.Classes.clsManuf();
				dpManu.DataSource = oManuf.GetMauf();
				dpManu.DataTextField = "ManufName";
				dpManu.DataValueField = "ManufID";
				dpManu.DataBind();
				
				dpPType.DataSource = clsItem.GetPTypes(null);
				dpPType.DataTextField = "PDesc";
				dpPType.DataValueField = "ID";
				dpPType.DataBind();
				dpPType.Items.Insert(0,"");
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
			this.btnSrch.Click += new System.EventHandler(this.btnSrch_Click);
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			lblErr.Text = string.Empty;
			txtModel.Text = string.Empty;
			dpPType.SelectedIndex = 0;
			dpManu.SelectedIndex = 0;
			chkNew.Checked= false;
			chkUsed.Checked = false;
			chkRef.Checked = false;
			dgItems.DataSource = null;
			dgItems.DataBind();

		}

		private void btnSrch_Click(object sender, System.EventArgs e)
		{	
			lblErr.Text = string.Empty;
			
			dgItems.CurrentPageIndex = 0;
			dgItems.DataSource = clsItem.GetPhoneList((dpPType.SelectedIndex==0?null:dpPType.SelectedValue),(dpManu.SelectedIndex==0?null:dpManu.SelectedValue),(txtModel.Text.Trim().Length==0?null:txtModel.Text.Trim()),(chkNew.Checked?"Y":null),(chkUsed.Checked?"Y":null),(chkRef.Checked?"Y":null));
			dgItems.DataBind();
		
		}
		protected void dgDelete(object sender, DataGridCommandEventArgs e)
		{
			try 
			{
				lblErr.Text = string.Empty;
				if (e.Item.Cells[2].Text != "&nbsp;")
				{
					clsItem.DeleteItem(e.Item.Cells[2].Text);
					dgItems.DataSource = clsItem.GetPhoneList((dpPType.SelectedIndex==0?null:dpPType.SelectedValue),(dpManu.SelectedIndex==0?null:dpManu.SelectedValue),(txtModel.Text.Trim().Length==0?null:txtModel.Text.Trim()),(chkNew.Checked?"Y":null),(chkUsed.Checked?"Y":null),(chkRef.Checked?"Y":null));
					dgItems.DataBind();
				}	
			}
			catch (Exception ex)
			{
				lblErr.Text = "Item is already referred in other place, please remove all those references(Price, Order) to remove this item";

			}
		}

		protected void dgItemBound(object sender, DataGridItemEventArgs e)
		{
			if (e.Item.Cells[0].Controls.Count > 0)
				((LinkButton)e.Item.Cells[0].Controls[0]).Attributes.Add("onclick","return validate();");
		}


		private void btnAdd_Click(object sender, System.EventArgs e)
		{
            Server.Transfer(@"/frmItem.aspx");
		}
	}
}
