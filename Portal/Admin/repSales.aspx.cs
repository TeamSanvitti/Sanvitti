using System;
using System.Data;
using System.Web.UI.WebControls;

namespace avii.Admin
{
	/// <summary>
	/// Summary description for repSales.
	/// </summary>
	public class repSales : System.Web.UI.Page
	{
		#region "Protected Objects"
		protected System.Web.UI.WebControls.RadioButton rdDt;
		protected System.Web.UI.WebControls.RadioButton rdMnth;
		protected System.Web.UI.WebControls.DropDownList dpDur;
		protected System.Web.UI.WebControls.DropDownList dpPhone;
		protected System.Web.UI.WebControls.TextBox txtSDate;
		protected System.Web.UI.WebControls.TextBox txtEDate;
		protected System.Web.UI.WebControls.DropDownList dpType;
		protected System.Web.UI.WebControls.DropDownList dpManuf;
		protected System.Web.UI.WebControls.DropDownList dpSP;
		protected System.Web.UI.WebControls.Button btnSubmit;
		protected System.Web.UI.WebControls.Button btnCancel;
		protected System.Web.UI.WebControls.RadioButton rdSum;
		protected System.Web.UI.WebControls.RadioButton rdDet;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected System.Web.UI.WebControls.DataGrid dgOrders;
				protected System.Web.UI.WebControls.DropDownList dpCust;
		#endregion

		#region "Private Class objects"
		private avii.Classes.clsItem oItm = new avii.Classes.clsItem();
		private avii.Classes.clsOrder  oOrd = new avii.Classes.clsOrder();
		private avii.Classes.clsManuf oManuf = new avii.Classes.clsManuf();
		private avii.Classes.clsCust oCust = new avii.Classes.clsCust ();
		protected System.Web.UI.WebControls.DropDownList dpSales;

		private avii.Classes.clsSP oSP = new avii.Classes.clsSP();
		#endregion

		#region "Page Load"
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!this.IsPostBack)
			{
				fnPopulate();
			}
		}
		#endregion

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
			this.rdDt.CheckedChanged += new System.EventHandler(this.rdDt_CheckedChanged);
			this.rdMnth.CheckedChanged += new System.EventHandler(this.rdMnth_CheckedChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void rdDt_CheckedChanged(object sender, System.EventArgs e)
		{
			if (rdDt.Checked)
			{
				dpDur.SelectedIndex = 0;
			}
		}

		private void rdMnth_CheckedChanged(object sender, System.EventArgs e)
		{
			if (rdMnth.Checked)
			{
				txtSDate.Text  = string.Empty;
				txtEDate.Text  = string.Empty;
				dpDur.SelectedIndex = 1;
			}
		}

		private void fnClear()
		{
			dpType.SelectedIndex = 0;
			dpManuf.SelectedIndex = 0;
			dpSP.SelectedIndex = 0;
			dpPhone.SelectedIndex = 0;
			txtSDate.Text  = string.Empty;
			txtEDate.Text  = string.Empty;
			dpDur.SelectedIndex = 1;
			rdMnth.Checked = true;
			rdDet.Checked = true;
			dgOrders.DataSource = null;
			dgOrders.DataBind();

		}

		private void fnPopulate()
		{
			DataSet ds = oCust.GetCustInfo(null);
			dpType.DataSource = oItm.GetPTypes(null);
			dpType.DataTextField = "PDesc";
			dpType.DataValueField = "ID";
			dpType.DataBind();
			dpType.Items.Insert(0,"");

			dpManuf.DataSource = oManuf.GetMauf();
			dpManuf.DataTextField = "ManufName";
			dpManuf.DataValueField = "ManufID";
			dpManuf.DataBind();
			dpManuf.Items.Insert(0,"");

			dpSP.DataSource = oSP.GetServiceProvider();
			dpSP.DataTextField = "ServiceProvider";
			dpSP.DataValueField = "SPID";
			dpSP.DataBind();
			dpSP.Items.Insert(0,"");

			dpPhone.DataSource = oItm.GetPhones();
			dpPhone.DataTextField = "PhoneTitle";
			dpPhone.DataValueField = "ItemID";
			dpPhone.DataBind();
			dpPhone.Items.Insert(0,"");

			dpCust.DataSource = ds.Tables[0];
			dpCust.DataTextField = "CustName";
			dpCust.DataValueField = "CustID";
			dpCust.DataBind();
			dpCust.Items.Insert(0,"");

			dpSales.DataSource = ds.Tables[2];
			dpSales.DataTextField = "Employee";
			dpSales.DataValueField = "SAlesID";
			dpSales.DataBind();
			dpSales.Items.Insert(0,"");

		}

	
		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			 fnClear();
		}

		protected void dgDelete(object sender, DataGridCommandEventArgs e)
		{
			if (e.Item.Cells[1].Text != "&nbsp;")
			{
				oOrd.DeleteOrder(e.Item.Cells[1].Text);
				fnSearch();
			}	
		}

		protected void dgItemBound(object sender, DataGridItemEventArgs e)
		{
			if (e.Item.Cells[0].Controls.Count > 0)
				((LinkButton)e.Item.Cells[0].Controls[0]).Attributes.Add("onclick","return validate();");
		}

		
		private void btnSubmit_Click(object sender, System.EventArgs e)
		{
			bool bRep = true;
			if (rdMnth.Checked)
			{
				if (dpDur.SelectedIndex == 0)
				{
					bRep = false;
				}
			}
			if (rdDt.Checked)
			{
				if (txtSDate.Text.Trim().Length == 0 && txtEDate.Text.Trim().Length == 0 )
				{
					bRep = false;
				}
			}
			if (bRep == false)
			{
				this.RegisterStartupScript("aa","<script language='javascript'>alert('Enter Start or End dates/Duration to get the report');</script>"); 
			}
			else
			{
				fnSearch();
			}
		}

		private void fnSearch()
		{
			string sSDate,  sEDate, sDur, sDType, sPType, sManuf, sPhone, sSP, sReportType, sCust, sSale ;
			sSDate = (txtSDate.Text.Trim().Length == 0?txtSDate.Text.Trim():null);
			sEDate = (txtEDate.Text.Trim().Length == 0?txtEDate.Text.Trim():null);
			sDur = (dpDur.SelectedIndex > 0?dpDur.SelectedValue:null);
			sDType = (rdDt.Checked?"D":null);
			sDType = (rdMnth.Checked?"M":null);
			sPType = (dpType.SelectedIndex > 0?dpType.SelectedValue:null);
			sManuf = (dpManuf.SelectedIndex > 0?dpManuf.SelectedValue:null);
			sPhone = (dpPhone.SelectedIndex > 0?dpPhone.SelectedValue:null);
			sSP = (dpSP.SelectedIndex > 0?dpSP.SelectedValue:null);
			sCust = (dpCust.SelectedIndex > 0?dpCust.SelectedValue:null);
			sSale = (dpSales.SelectedIndex > 0?dpSales.SelectedValue:null);
			sReportType = (rdSum.Checked?"S":null);
			sReportType = (rdDet.Checked?"D":null);
				
			DataTable oDt = avii.Classes.clsReport.GetSales( sSDate,  sEDate, sDur, sDType, sPType, sManuf, sPhone, sSP, sReportType,sCust,sSale);
			dgOrders.CurrentPageIndex  = 0;
			dgOrders.DataSource = oDt;
			dgOrders.DataBind();
			dgOrders.Visible= true;
		}
	}
}
