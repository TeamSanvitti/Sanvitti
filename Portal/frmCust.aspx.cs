using System;
using System.Data;
using System.Web.UI;

namespace avii
{
	/// <summary>
	/// Summary description for frmCust.
	/// </summary>
	public class frmCust : System.Web.UI.Page
	{
		#region "Protected Objects"
		protected System.Web.UI.WebControls.TextBox txtFirstName;
		protected System.Web.UI.WebControls.TextBox txtBillFirstName;
		protected System.Web.UI.WebControls.TextBox txtLastName;
		protected System.Web.UI.WebControls.TextBox txtBillLastName;
		protected System.Web.UI.WebControls.TextBox txtMiddleName;
		protected System.Web.UI.WebControls.TextBox txtBillMiddleName;
		protected System.Web.UI.WebControls.TextBox txtAddress1;
		protected System.Web.UI.WebControls.TextBox txtBillAddress1;
		protected System.Web.UI.WebControls.TextBox txtAddress2;
		protected System.Web.UI.WebControls.TextBox txtBillAddress2;
		protected System.Web.UI.WebControls.TextBox txtBillCity;
		protected System.Web.UI.WebControls.DropDownList dpState;
		protected System.Web.UI.WebControls.DropDownList dpBillState;
		protected System.Web.UI.WebControls.TextBox txtZip;
		protected System.Web.UI.WebControls.TextBox txtBillZip;
		protected System.Web.UI.WebControls.CheckBox chkBillingAddress;
		protected System.Web.UI.WebControls.TextBox txtOfficePhone;
		protected System.Web.UI.WebControls.TextBox txtCellPhone;
		protected System.Web.UI.WebControls.Button btnSave;
		protected System.Web.UI.WebControls.Label lblException;
		protected System.Web.UI.WebControls.ValidationSummary valSum;
		protected System.Web.UI.WebControls.RequiredFieldValidator vldFN;
		protected System.Web.UI.WebControls.RequiredFieldValidator vldLN;
		protected System.Web.UI.WebControls.RequiredFieldValidator vldAddr1;
		protected System.Web.UI.WebControls.RequiredFieldValidator vldCity;
		protected System.Web.UI.WebControls.RequiredFieldValidator vldZip;
		protected System.Web.UI.WebControls.RequiredFieldValidator vldBFN;
		protected System.Web.UI.WebControls.RequiredFieldValidator vldBLN;
		protected System.Web.UI.WebControls.RequiredFieldValidator vldBAddr1;
		protected System.Web.UI.WebControls.RequiredFieldValidator vldBCity;
		protected System.Web.UI.WebControls.TextBox txtCity;
		protected System.Web.UI.WebControls.TextBox txtUser;
		protected System.Web.UI.WebControls.TextBox txtEmail;
		protected System.Web.UI.WebControls.RequiredFieldValidator Requiredfieldvalidator1;
		protected System.Web.UI.WebControls.RequiredFieldValidator Requiredfieldvalidator3;
		protected System.Web.UI.WebControls.RequiredFieldValidator vldBZip;
		protected System.Web.UI.HtmlControls.HtmlInputText txtPwd;
		protected System.Web.UI.HtmlControls.HtmlInputText txtCPwd;
		protected System.Web.UI.WebControls.Panel pnlUser;
		protected System.Web.UI.HtmlControls.HtmlForm cust;
		protected System.Web.UI.WebControls.TextBox txtStores;	
#endregion

		private bool bUpdate =false;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdnCustTypeID;
		protected System.Web.UI.WebControls.DropDownList dpSP;
		private avii.Classes.clsRegis oCust = new avii.Classes.clsRegis();
		private avii.Classes.clsSP  oSP = new avii.Classes.clsSP();

		private void Page_Load(object sender, System.EventArgs e)
		{
			this.btnSave.Attributes.Add ("onclick","return fnValidate();");
			if (!this.IsPostBack)
			{
				if (Request.Params["c"] != null) 
				{
					ViewState["Code"] = Request.Params["c"] ;
					fnCustInfo(Request.Params["c"]);
					pnlUser.Visible= false;
					bUpdate = true;
				}
				dpSP.DataSource = oSP.GetServiceProvider();
				dpSP.DataTextField = "ServiceProvider";
				dpSP.DataValueField = "SPID";
				dpSP.DataBind();
				dpSP.Items.Insert(0,string.Empty);
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
			this.chkBillingAddress.CheckedChanged += new System.EventHandler(this.chkBillingAddress_CheckedChanged);
			this.dpSP.SelectedIndexChanged += new System.EventHandler(this.dpSP_SelectedIndexChanged);
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			string sCode =string.Empty;
			bool bSave = true;
			if (Page.IsValid)
			{
				if (pnlUser.Visible == true)
				{
					if (txtUser.Text.Trim().Length == 0)
					{
						this.RegisterStartupScript("aa","<script language=javascript>alert('User name cannot left blank');</script>");
						bSave = false;
					}
					if (txtPwd.Value.Trim().Length == 0)
					{
						this.RegisterStartupScript("aa","<script language=javascript>alert('Password cannot left blank');</script>");
						bSave =false;
					}
					if (bSave == true)
					{
						if (oCust.iValidateLogin(txtUser.Text.Trim()) <= 0)
						{
							oCust.UserName = txtUser.Text.Trim();
							oCust.Password = txtPwd.Value.Trim();
						}
						else
						{
							this.RegisterStartupScript("aa","<script language=javascript>alert('User already exist in the system');</script>");
							bSave =false;
						}
					}
				}
				if (bSave == true)
				{
					oCust.FirstName = txtFirstName.Text.Trim();
					oCust.LastName = txtLastName.Text.Trim();
					oCust.MiddleName= txtMiddleName.Text.Trim();
					oCust.Address1 = txtAddress1.Text.Trim();
					oCust.Address2 = txtAddress2.Text.Trim();
					oCust.City = txtCity.Text.Trim();
					oCust.State = dpState.SelectedValue;
					oCust.Zip = txtZip.Text.Trim();
					oCust.BillFirstName = txtBillFirstName.Text.Trim();
					oCust.BillLastName = txtBillLastName.Text.Trim();
					oCust.BillAddress1 = txtBillAddress1.Text.Trim();
					oCust.BillAddress2 = txtBillAddress2.Text.Trim();
					oCust.BillMiddleName= txtBillMiddleName.Text.Trim();
					oCust.BillCity= txtBillCity.Text.Trim();
					oCust.BillState = dpBillState.SelectedValue;
					oCust.BillZip = txtBillZip.Text.Trim();
					oCust.EmailAddress = txtEmail.Text.Trim();
					//oCust.HomePhone = txtHomePhone.Text.Trim();
					oCust.OfficePhone = txtOfficePhone.Text.Trim();
					oCust.CustTypeID = hdnCustTypeID.Value ;
					oCust.Stores = txtStores.Text.Trim();
					oCust.ServiceProvider = dpSP.SelectedValue;
					oCust.CellPhone= txtCellPhone.Text.Trim();
					if (ViewState["Code"] != null)
					{
						oCust.LoginID = Convert.ToInt32(ViewState["Code"]);
					}
					sCode = oCust.SetRecords(1);
					if (ViewState["Code"] == null && sCode.Length > 0)
					{
						this.RegisterStartupScript("aaaaa","<script language='javascript'>fnTha();</script>");
					}
					else
						this.RegisterStartupScript("aaaaa","<script language='javascript'>fnTha1();</script>");
					ViewState["Code"] = sCode;

				}
			}
		}

		private void fnCustInfo(string sCustID)
		{
			DataTable oDt = oCust.GetCustInfo(sCustID);
			if (oDt.Rows.Count > 0)
			{
				txtUser.Text = (oDt.Rows[0]["LogonName"] != DBNull.Value?Convert.ToString(oDt.Rows[0]["LogonName"]):string.Empty);
				txtPwd.Value = (oDt.Rows[0]["Pwd"] != DBNull.Value?Convert.ToString(oDt.Rows[0]["Pwd"]):string.Empty);
				txtCPwd.Value = (oDt.Rows[0]["Pwd"] != DBNull.Value?Convert.ToString(oDt.Rows[0]["Pwd"]):string.Empty);
				txtFirstName.Text = (oDt.Rows[0]["FirstName"] != DBNull.Value?Convert.ToString(oDt.Rows[0]["FirstName"]):string.Empty);
				txtLastName.Text = (oDt.Rows[0]["LastName"] != DBNull.Value?Convert.ToString(oDt.Rows[0]["LastName"]):string.Empty);
				txtMiddleName.Text = (oDt.Rows[0]["MiddleName"] != DBNull.Value?Convert.ToString(oDt.Rows[0]["MiddleName"]):string.Empty);
				txtAddress1.Text = (oDt.Rows[0]["Address1"] != DBNull.Value?Convert.ToString(oDt.Rows[0]["Address1"]):string.Empty);
				txtAddress2.Text =(oDt.Rows[0]["Address2"] != DBNull.Value?Convert.ToString(oDt.Rows[0]["Address2"]):string.Empty);
				txtCity.Text = (oDt.Rows[0]["City"] != DBNull.Value?Convert.ToString(oDt.Rows[0]["City"]):string.Empty);
				dpState.SelectedValue  = (oDt.Rows[0]["State"] != DBNull.Value?Convert.ToString(oDt.Rows[0]["State"]):string.Empty);
				txtZip.Text = (oDt.Rows[0]["Zip"] != DBNull.Value?Convert.ToString(oDt.Rows[0]["Zip"]):string.Empty);
				//chkBillingAddress.Checked = false;
				
				txtBillFirstName.Text = (oDt.Rows[0]["BillFirstName"] != DBNull.Value?Convert.ToString(oDt.Rows[0]["BillFirstName"]):string.Empty);
				txtBillLastName.Text = (oDt.Rows[0]["BillLastName"] != DBNull.Value?Convert.ToString(oDt.Rows[0]["BillLastName"]):string.Empty);
				txtBillMiddleName.Text = (oDt.Rows[0]["BillMiddleName"] != DBNull.Value?Convert.ToString(oDt.Rows[0]["BillMiddleName"]):string.Empty);
				txtBillAddress1.Text = (oDt.Rows[0]["BillAddress1"] != DBNull.Value?Convert.ToString(oDt.Rows[0]["BillAddress1"]):string.Empty);
				txtBillAddress2.Text = (oDt.Rows[0]["BillAddress2"] != DBNull.Value?Convert.ToString(oDt.Rows[0]["BillAddress2"]):string.Empty);
				txtBillCity.Text = (oDt.Rows[0]["BillCity"] != DBNull.Value?Convert.ToString(oDt.Rows[0]["BillCity"]):string.Empty);
				dpBillState.SelectedValue =(oDt.Rows[0]["BillState"] != DBNull.Value?Convert.ToString(oDt.Rows[0]["BillState"]):string.Empty);
				txtStores.Text = (oDt.Rows[0]["Stores"] != DBNull.Value?Convert.ToString(oDt.Rows[0]["Stores"]):string.Empty);
				txtBillZip.Text = (oDt.Rows[0]["BillZip"] != DBNull.Value?Convert.ToString(oDt.Rows[0]["BillZip"]):string.Empty);
				if (oDt.Rows[0]["SP"] != DBNull.Value)
					dpSP.SelectedValue  = Convert.ToString(oDt.Rows[0]["SP"]);
				//txtHomePhone.Text = (oDt.Rows[0]["HomePhone"] != DBNull.Value?Convert.ToString(oDt.Rows[0]["HomePhone"]):string.Empty);
				txtOfficePhone.Text = (oDt.Rows[0]["OfficePhone"] != DBNull.Value?Convert.ToString(oDt.Rows[0]["OfficePhone"]):string.Empty);
				txtCellPhone.Text = (oDt.Rows[0]["CellPhone"] != DBNull.Value?Convert.ToString(oDt.Rows[0]["CellPhone"]):string.Empty);
				txtEmail.Text = (oDt.Rows[0]["Email"] != DBNull.Value?Convert.ToString(oDt.Rows[0]["Email"]):string.Empty);

				hdnCustTypeID.Value =  (oDt.Rows[0]["CustomerTypeID"] != DBNull.Value?Convert.ToString(oDt.Rows[0]["CustomerTypeID"]):string.Empty);
			}

		}

		private void cleanForm()
		{
			txtUser.Text = String.Empty;
			txtPwd.Value = String.Empty;
			txtCPwd.Value = String.Empty;
			txtFirstName.Text = String.Empty;
			txtLastName.Text = String.Empty;
			txtMiddleName.Text = String.Empty;
			txtAddress1.Text = String.Empty;
			txtAddress2.Text = String.Empty;
			txtCity.Text = String.Empty;
			dpState.SelectedIndex = 0;
			txtZip.Text = String.Empty;
			chkBillingAddress.Checked = false;

			txtBillFirstName.Text = String.Empty;
			txtBillLastName.Text = String.Empty;
			txtBillMiddleName.Text = String.Empty;
			txtBillAddress1.Text = String.Empty;
			txtBillAddress2.Text = String.Empty;
			txtBillCity.Text = String.Empty;
			dpBillState.SelectedIndex = 0;
			txtBillZip.Text = String.Empty;

			//txtHomePhone.Text = String.Empty;
			dpSP.SelectedValue = string.Empty;
			txtOfficePhone.Text = String.Empty;
			txtCellPhone.Text = String.Empty;
			txtEmail.Text = String.Empty;
		}

		private void chkBillingAddress_CheckedChanged(object sender, System.EventArgs e)
		{
			if (chkBillingAddress.Checked)
			{
				txtBillFirstName.Text = txtFirstName.Text;
				txtBillLastName.Text = txtLastName.Text;
				txtBillMiddleName.Text = txtMiddleName.Text;
				txtBillAddress1.Text = txtAddress1.Text;
				txtBillAddress2.Text = txtAddress2.Text;
				txtBillCity.Text = txtCity.Text;
				dpBillState.SelectedIndex =  dpState.SelectedIndex;
				txtBillZip.Text = txtZip.Text;
			}
			else
			{
				txtBillFirstName.Text = String.Empty;
				txtBillLastName.Text = String.Empty;
				txtBillMiddleName.Text = String.Empty;
				txtBillAddress1.Text = String.Empty;
				txtBillAddress2.Text = String.Empty;
				txtBillCity.Text = String.Empty;
				dpBillState.SelectedIndex = 0;
				txtBillZip.Text = String.Empty;
			}
		}


		private void dpSP_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}
	}
}
