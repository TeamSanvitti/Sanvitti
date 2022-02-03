using System;
using System.Configuration;
using System.Data;
using System.Web.UI.WebControls;

namespace avii.Admin
{
	/// <summary>
	/// Summary description for frmItem.
	/// </summary>
	public class frmItem : System.Web.UI.Page
	{
		private DataSet odsItems = new DataSet();
		private DataTable dtFeature;
		private DataTable dtPrice;
		private DataTable dtSP;
		private avii.Classes.clsItem clsItm = new avii.Classes.clsItem();
		private avii.Classes.clsManuf oManuf = new avii.Classes.clsManuf();
		private Int32 iEditIndex;

		#region "PROTECTED OBJECTS"
		protected System.Web.UI.WebControls.DropDownList dpPType;
		protected System.Web.UI.WebControls.DropDownList dpManu;
		protected System.Web.UI.WebControls.TextBox txtModel;
		protected System.Web.UI.WebControls.DataGrid dgFeatures;
		protected System.Web.UI.WebControls.TextBox txtDesc;
		protected System.Web.UI.WebControls.CheckBox chkNew;
		protected System.Web.UI.WebControls.CheckBox chkUsed;
		protected System.Web.UI.WebControls.CheckBox chkRef;
		protected System.Web.UI.WebControls.Button btnSubmit;
		protected System.Web.UI.WebControls.Button btnCancel;
		protected System.Web.UI.WebControls.TextBox txtName;
		protected System.Web.UI.WebControls.DataGrid dgPrice;
		protected System.Web.UI.WebControls.CheckBox chkSpecial;
		protected System.Web.UI.WebControls.Image imgPhone;
		protected System.Web.UI.WebControls.DataGrid dgSP;
		protected System.Web.UI.WebControls.Button btnAddNew;
		protected System.Web.UI.WebControls.TextBox txtWarnt;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdnItmImage;
		protected System.Web.UI.WebControls.DropDownList dpAvail;
		protected System.Web.UI.WebControls.CheckBox chkActive;
		protected System.Web.UI.WebControls.CheckBox chkPrice;
		protected System.Web.UI.WebControls.Label lblError;
		protected System.Web.UI.HtmlControls.HtmlInputFile fImage;
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
			this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion



		private void Page_Load(object sender, System.EventArgs e)
		{

			if (!this.IsPostBack) 
			{
				lblError.Text =string.Empty;
				dpPType.DataSource = clsItm.GetPTypes(null);
				dpPType.DataTextField = "PDesc";
				dpPType.DataValueField = "ID";
				dpPType.DataBind();
				dpManu.DataSource = oManuf.GetMauf();
				dpManu.DataTextField = "ManufName";
				dpManu.DataValueField = "ManufID";
				dpManu.DataBind();
				dpManu.Items.Insert(0,"");
				if (Request.Params["tid"] != null)
				{
					ViewState["ItemID"] = Request.Params["tid"];
					odsItems = clsItm.GetItemDetail(Request.Params["tid"]);
					if (odsItems != null)
					{
						ViewState["Feature"] = odsItems.Tables[1];
						Feature_datagridBind(1);
						ViewState["Price"] = odsItems.Tables[2];
						Price_datagridBind(0);
						ViewState["SP"] = odsItems.Tables[3];
						SP_datagridBind(0);
//						dgPrice.DataSource = odsItems.Tables[2];
//						//dgPrice.DataBind();
						if (odsItems.Tables[0].Rows.Count >0)
						{
							dpPType.SelectedValue = (odsItems.Tables[0].Rows[0]["PhoneType"] == DBNull.Value?string.Empty:odsItems.Tables[0].Rows[0]["PhoneType"].ToString());
							dpManu.SelectedValue = (odsItems.Tables[0].Rows[0]["PhoneManuf"] == DBNull.Value?string.Empty:odsItems.Tables[0].Rows[0]["PhoneManuf"].ToString());
							txtModel.Text = (odsItems.Tables[0].Rows[0]["PhoneModel"] == DBNull.Value?string.Empty:odsItems.Tables[0].Rows[0]["PhoneModel"].ToString());
							txtName.Text = (odsItems.Tables[0].Rows[0]["PhoneTitle"] == DBNull.Value?string.Empty:odsItems.Tables[0].Rows[0]["PhoneTitle"].ToString());
							txtDesc.Text = (odsItems.Tables[0].Rows[0]["PhoneDesc"] == DBNull.Value?string.Empty:odsItems.Tables[0].Rows[0]["PhoneDesc"].ToString());
							dpAvail.SelectedValue  = (odsItems.Tables[0].Rows[0]["Available"] == DBNull.Value?string.Empty:odsItems.Tables[0].Rows[0]["Available"].ToString());
							chkNew.Checked = Convert.ToBoolean(odsItems.Tables[0].Rows[0]["Cond_New"]);
							chkUsed.Checked = (odsItems.Tables[0].Rows[0]["Cond_Old"] != DBNull.Value?Convert.ToBoolean(odsItems.Tables[0].Rows[0]["Cond_Old"]):false);
							chkRef.Checked = (odsItems.Tables[0].Rows[0]["Cond_Ref"] != DBNull.Value?Convert.ToBoolean(odsItems.Tables[0].Rows[0]["Cond_Ref"]):false);
							chkSpecial.Checked = (odsItems.Tables[0].Rows[0]["Special"] != DBNull.Value?Convert.ToBoolean(odsItems.Tables[0].Rows[0]["Special"]):false);
							imgPhone.ImageUrl = ".."  + ConfigurationSettings.AppSettings["Content"] + (odsItems.Tables[0].Rows[0]["PhoneImage"] == DBNull.Value?string.Empty:odsItems.Tables[0].Rows[0]["PhoneImage"].ToString());
							hdnItmImage.Value = (odsItems.Tables[0].Rows[0]["PhoneImage"] == DBNull.Value?string.Empty:odsItems.Tables[0].Rows[0]["PhoneImage"].ToString());
							//txtDim.Text = (odsItems.Tables[0].Rows[0]["PDimension"] == DBNull.Value?string.Empty:odsItems.Tables[0].Rows[0]["PDimension"].ToString());
							//txtWarnt.Text = (odsItems.Tables[0].Rows[0]["warrenty"] == DBNull.Value?string.Empty:odsItems.Tables[0].Rows[0]["warrenty"].ToString());
							//txtBat.Text = (odsItems.Tables[0].Rows[0]["Battery"] == DBNull.Value?string.Empty:odsItems.Tables[0].Rows[0]["Battery"].ToString());
							chkActive.Checked = (odsItems.Tables[0].Rows[0]["InActive"] != DBNull.Value?true:false);
							chkPrice.Checked = (odsItems.Tables[0].Rows[0]["HidePrice"] != DBNull.Value?true:false);
						}
					}
				}
				else
				{
					Feature_datagridBind(0);
					Price_datagridBind(0);
					SP_datagridBind(0);

//					dgPrice.DataSource = clsItm.GetPrice();
//					dgPrice.DataBind();
				}
			}

		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			
			Response.Redirect(@"/frmItmLst.aspx");
		}

		private Int32 fnPrice()
		{
			int RetVal = 0;
			dtPrice = (ViewState["Price"]== null?null:(DataTable)ViewState["Price"]);
			if (dtPrice !=null)
			{
				foreach (DataGridItem dgi in dgPrice.Items)
				{
					if (((TextBox)dgi.Cells[1].Controls[1]).Text.Length > 0)
					{
						if (Convert.ToDouble(((TextBox)dgi.Cells[1].Controls[1]).Text) > 0)
						{
							dtPrice.Rows[dgi.ItemIndex]["Price"] = ((TextBox)dgi.Cells[1].Controls[1]).Text;
							RetVal = 1;
						}
					}
					else
						dtPrice.Rows[dgi.ItemIndex]["Price"] = "0";
				}
				ViewState["Price"]= dtPrice;
			}
			
			return RetVal;
		}

		private Int32 fnSetServiceProvider()
		{
			int RetVal = 0;
			dtSP = (ViewState["SP"]== null?null:(DataTable)ViewState["SP"]);
			if (dtSP !=null)
			{
				foreach (DataGridItem dgi in dgSP.Items)
				{
					if (((CheckBox)dgi.Cells[2].Controls[1]).Checked)
					{
						dtSP.Rows[dgi.ItemIndex]["chk"] = "True";
						RetVal = 1;
					}
					else
						dtSP.Rows[dgi.ItemIndex]["chk"] = "False";
				}
				ViewState["SP"]= dtSP;
			}
			return RetVal;
		}

		private void btnSubmit_Click(object sender, System.EventArgs e)
		{
			try
			{
				lblError.Text =string.Empty;
				bool bSave = true;
				string sCode = string.Empty ;
				fnSetServiceProvider();
				if (fnPrice() == 0 && chkPrice.Checked == false)
				{
					this.RegisterStartupScript("aa1","<script languange='javascript'>alert('Item price is required or select Hide Price');</script>");
					bSave = false;
				}
				if (bSave == true)
				{
					clsItm.ItemID = (ViewState["ItemID"] ==null?null:ViewState["ItemID"].ToString());
					clsItm.Manufacturar = dpManu.SelectedValue;
					clsItm.Model = txtModel.Text.Trim();
					clsItm.Name = txtName.Text.Trim();
					clsItm.Type = dpPType.SelectedValue;
					clsItm.Description = txtDesc.Text.Trim();
					clsItm.Cond_New = (chkNew.Checked?"Y":null);
					clsItm.Cond_Old = (chkUsed.Checked?"Y":null);
					clsItm.Cond_Ref = (chkRef.Checked?"Y":null);
					clsItm.NewArrival = (chkNew.Checked?"Y":null);
					clsItm.Availability = dpAvail.SelectedValue;
					clsItm.SpecialItem = (chkSpecial.Checked?"1":"0");
					clsItm.Warrenty = (txtWarnt.Text.Trim().Length ==0?null:txtWarnt.Text.Trim());
					//clsItm.Dimension = (txtDim.Text.Trim().Length ==0?null:txtDim.Text.Trim());
					//clsItm.Battery = (txtBat.Text.Trim().Length ==0?null:txtBat.Text.Trim());
					clsItm.Prices =  (ViewState["Price"] == null?null:(DataTable)ViewState["Price"]);
					clsItm.Features = (ViewState["Feature"]== null?null:(DataTable)ViewState["Feature"]);
					clsItm.SPs = (ViewState["SP"]== null?null:(DataTable)ViewState["SP"]);
					clsItm.InActive = (chkActive.Checked?"1":null);
					clsItm.HidePrice = (chkPrice.Checked?"1":null);

					if (fImage.PostedFile.FileName.Trim().Length > 0 )
					{
						string strSavedFileName,strDir;
						Random r = new Random();
						int intUpLdStatus = 0;
						strDir = Server.MapPath(ConfigurationSettings.AppSettings["DownloadImages"]) + @"\";
						if (fImage.PostedFile.ContentType.ToUpper() == "IMAGE/GIF" ) 
							strSavedFileName =  txtModel.Text.Trim() +   r.Next() + ".gif";
						else if (fImage.PostedFile.ContentType.ToUpper() == "IMAGE/PJPEG")
							strSavedFileName = txtModel.Text.Trim() + r.Next() + ".jpg";
						else
							throw new Exception ("File type is not valid, please enter only GIF or JPG files");
						avii.Classes.clsCommon.PicType = "I";
						intUpLdStatus = avii.Classes.clsCommon.fnUploadFile(fImage,strDir ,strSavedFileName);
						if (intUpLdStatus==0)
							clsItm.Image =  strSavedFileName;
						else
							clsItm.Image = ConfigurationSettings.AppSettings["DefaultImage"].ToString();
						strSavedFileName = null;
						imgPhone.ImageUrl = "./content/" + clsItm.Image;
					}
					else if (hdnItmImage.Value.Length > 0)
					{
						clsItm.Image =  hdnItmImage.Value.Trim();
					}
					
					sCode = clsItm.SetRecords(1);
					if (sCode.Length > 0)
					{					
						if (ViewState["ItemID"] ==null)
							this.RegisterStartupScript("aa","<script languange='javascript'>alert('Item is created successfully');</script>");
						else
							this.RegisterStartupScript("aa","<script languange='javascript'>alert('Item is modified successfully');</script>");
						ViewState["ItemID"] = sCode;	
					}

				}
			}
			catch (Exception ex)
			{
				lblError.Text = ex.Message;
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
			Feature_datagridBind(0);
		}

		protected void dg_Edit( System.Object sender, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
				iEditIndex = e.Item.ItemIndex;
				ViewState["EditIndex"] = iEditIndex;
				dgFeatures.EditItemIndex = iEditIndex;
				Feature_datagridBind(0);
		}

		protected void dg_Update( System.Object sender, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string sFeature;
			iEditIndex = (Int32)ViewState["EditIndex"];
			sFeature = ((TextBox)e.Item.Cells[2].Controls[1]).Text;
			dtFeature = (DataTable)ViewState["Feature"];
			dtFeature.Rows[iEditIndex]["FeatureText"] = sFeature;
			dtFeature.Rows[iEditIndex]["ItemID"] = ViewState["ItemID"];
			dtFeature.AcceptChanges();
			ViewState["Feature"] = dtFeature;
			dgFeatures.EditItemIndex = -1;
			if (iEditIndex ==0)
				Feature_datagridBind(1);
			else
				Feature_datagridBind(0);
		}
		protected void dg_ItemCommand( System.Object sender, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			if (e.CommandName == "delete" && e.Item.ItemIndex > 0)
			{
				if (ViewState["Feature"] != null)
				{
					dtFeature = (DataTable)ViewState["Feature"];
					dtFeature.Rows.Remove(dtFeature.Rows[e.Item.ItemIndex]);
					dtFeature.AcceptChanges();
					ViewState["Feature"] = dtFeature;
					Feature_datagridBind(0);
				}

			}
		}

		private void Feature_datagridBind(Int32 iRowAdd)
		{ 
			if (ViewState["Feature"] == null) 
				dtFeature = clsItm.GetFeatures();
			else
				dtFeature = (DataTable)ViewState["Feature"];
			AddTableRows(iRowAdd);
			ViewState["Feature"] = dtFeature;
			dgFeatures.DataSource = dtFeature;
			dgFeatures.DataBind();
		}

		private void AddTableRows(Int32 iRowAdd)
		{
			if (ViewState["Feature"] == null) 
				dtFeature = clsItm.GetFeatures();
			else
				dtFeature = (DataTable)ViewState["Feature"];
			if (iRowAdd > 0)
			{
				DataRow dRow;
				dRow = dtFeature.NewRow();
				dtFeature.Rows.InsertAt(dRow,0);
				
			}
		}

		private void Price_datagridBind(Int32 iRowAdd)
		{ 
			if (ViewState["Price"] == null) 
				dtPrice = clsItm.GetPrice();
			else
				dtPrice = (DataTable)ViewState["Price"];
			AddTableRowsPrice(iRowAdd);
			ViewState["Price"] = dtPrice;
			dgPrice.DataSource = dtPrice;
			dgPrice.DataBind();
		}

		private void AddTableRowsPrice(Int32 iRowAdd)
		{
			if (ViewState["Price"] == null) 
				dtPrice = clsItm.GetPrice();
			else
				dtPrice = (DataTable)ViewState["Price"];
			if (iRowAdd > 0)
			{
				DataRow dRow;
				dRow = dtPrice.NewRow();
				dtPrice.Rows.InsertAt(dRow,0);
				
			}
		}

		private void SP_datagridBind(Int32 iRowAdd)
		{ 
			if (ViewState["SP"] == null) 
				dtSP = clsItm.GetServiceProvider();
			else
				dtSP = (DataTable)ViewState["SP"];
			AddTableRowsSP(iRowAdd);
			ViewState["SP"] = dtSP;
			dgSP.DataSource = dtSP;
			dgSP.DataBind();
		}

		private void AddTableRowsSP(Int32 iRowAdd)
		{
			if (ViewState["SP"] == null) 
				dtSP = clsItm.GetServiceProvider();
			else
				dtSP = (DataTable)ViewState["SP"];
			if (iRowAdd > 0)
			{
				DataRow dRow;
				dRow = dtSP.NewRow();
				dtSP.Rows.InsertAt(dRow,0);
				
			}
		}

		private void chkAccess_CheckedChanged(object sender, System.EventArgs e)
		{
//			if (chkAccess.Checked)
//			{
//				dpPType.DataSource = clsItm.GetPTypes("A");
//			}
//			else
//			{
//				dpPType.DataSource = clsItm.GetPTypes("P");
//			}
			dpPType.DataTextField = "PDesc";
			dpPType.DataValueField = "ID";
			dpPType.DataBind();
		}

		private void btnAddNew_Click(object sender, System.EventArgs e)
		{
			ClearForm();
			fnPrelimForm();
		}

		private void ClearForm()
		{
			lblError.Text =string.Empty;
			dpPType.SelectedIndex = 0;
			dpManu.SelectedIndex = 0;
			txtModel.Text = string.Empty;
			txtName.Text = string.Empty;
			dpAvail.SelectedIndex = 0;
			txtDesc.Text = string.Empty;

			chkSpecial.Checked = false;
			this.chkActive.Checked = false;
			this.chkPrice.Checked = false;
			chkNew.Checked = false;
			chkUsed.Checked = false;
			chkRef.Checked = false;

			dgFeatures.DataSource = null;
			dgFeatures.DataBind();

			dgPrice.DataSource = null;
			dgPrice.DataBind();

			dgSP.DataSource = null;
			dgSP.DataBind();
		}

		private void fnPrelimForm()
		{
			ViewState["Feature"] = null;
			ViewState["Price"] = null;
			ViewState["SP"] = null;
			ViewState["Price"] = null;
			ViewState["ItemID"] = null;
			dpPType.DataSource = clsItm.GetPTypes(null);
			dpPType.DataTextField = "PDesc";
			dpPType.DataValueField = "ID";
			dpPType.DataBind();
			dpManu.DataSource = oManuf.GetMauf();
			dpManu.DataTextField = "ManufName";
			dpManu.DataValueField = "ManufID";
			dpManu.DataBind();
			dpManu.Items.Insert(0,"");
			Feature_datagridBind(0);
			Price_datagridBind(0);
			SP_datagridBind(0);
		}

        protected void btnSubmit_Click1(object sender, EventArgs e)
        {

        }

	}
}
