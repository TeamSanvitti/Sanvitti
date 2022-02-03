using System;
using System.Data;
using System.Text;
using System.Web.UI.WebControls;

namespace avii.Admin
{
	/// <summary>
	/// Summary description for ItmInt.
	/// </summary>
	public class ItmInt : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button btnSubmit;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected System.Web.UI.WebControls.DropDownList dpItem;
		protected System.Web.UI.WebControls.DataGrid dgItemRel;
		protected System.Web.UI.WebControls.Button btnCancel;
		protected System.Web.UI.WebControls.DataGrid dgItemInt;
		protected System.Web.UI.WebControls.Label outError;
		protected System.Web.UI.WebControls.Panel pnlItem;


		private avii.Classes.clsItem clsItem = new avii.Classes.clsItem();
		private DataTable odtRelItems;
		private DataTable odtAccess;

		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!this.IsPostBack)
			{
				outError.Text =string.Empty;
				dpItem.DataSource = clsItem.GetPhoneList(null,null,null,null,null,null); 
				dpItem.DataTextField = "PhoneTitle";
				dpItem.DataValueField = "ItemID";
				dpItem.DataBind();
				dpItem.Items.Insert(0,new ListItem("","0"));
				


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
			this.dpItem.SelectedIndexChanged += new System.EventHandler(this.dpItem_SelectedIndexChanged);
			this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnSubmit_Click(object sender, System.EventArgs e)
		{
			StringBuilder oRelItems = new StringBuilder();
			StringBuilder oAccItems = new StringBuilder();
			foreach (DataGridItem dgi in dgItemRel.Items)
			{
				if (((CheckBox)dgi.Cells[3].Controls[1]).Checked  == true)
				{
					oRelItems.Append(dgi.Cells[0].Text + "#");
				}
			}
			foreach (DataGridItem dgi in dgItemInt.Items)
			{
				if (((CheckBox)dgi.Cells[3].Controls[1]).Checked == true)
				{
					oAccItems.Append(dgi.Cells[0].Text + "#");
				}
			}
			clsItem.SetRelatedItems(dpItem.SelectedValue,oRelItems.ToString(),oAccItems.ToString());
			outError.Text =string.Empty;
		}

		private void dpItem_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			outError.Text = string.Empty;
			if (dpItem.SelectedValue != "0")
			{
				odtRelItems  = clsItem.GetSimilarItems(dpItem.SelectedValue); 
				odtAccess  = clsItem.GetAccessories(dpItem.SelectedValue); 
				if (odtRelItems.Rows.Count ==0)
					outError.Text = "No related item exists in system";
				if (odtAccess.Rows.Count ==0)
					if (outError.Text.Trim().Length == 0)
						outError.Text = "No Accessory item exists in system";
					else
						outError.Text = "No Related item and Accessory exists in system";
				dgItemRel.DataSource = odtRelItems;
				dgItemRel.DataBind();
				dgItemInt.DataSource = odtAccess;
				dgItemInt.DataBind();
				if (odtRelItems.Rows.Count > 0 || odtAccess.Rows.Count > 0)
					pnlItem.Visible = true;
			}
			else
				fnClear();
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			fnClear();
		}

		private void fnClear()
		{
			dpItem.SelectedIndex = 0;
			dgItemRel.DataSource = null;
			dgItemRel.DataBind();
			dgItemInt.DataSource = null;
			dgItemInt.DataBind();
			pnlItem.Visible = false;
			outError.Text = string.Empty;
		}
	}
}
