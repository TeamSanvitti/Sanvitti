using System;
using System.Configuration;
using System.Data;

namespace avii
{
	/// <summary>
	/// Summary description for frmNews.
	/// </summary>
	public class frmNews : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.RadioButton rbForms;
		protected System.Web.UI.WebControls.CheckBox chkPublish;
		protected System.Web.UI.WebControls.Button btnSubmit;
		protected System.Web.UI.WebControls.Button btnCancel;
		protected System.Web.UI.WebControls.TextBox txtTitle;
		protected System.Web.UI.WebControls.TextBox txtDesc;
		protected System.Web.UI.WebControls.TextBox txtStart;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdnItmImage;
		protected System.Web.UI.WebControls.RadioButton rbNews;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdnID;
		protected System.Web.UI.WebControls.Panel pnlSearch;
		protected System.Web.UI.WebControls.TextBox txtFTitle;
		protected System.Web.UI.WebControls.DropDownList dpDocType;
		protected System.Web.UI.WebControls.Button btnSearch;
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.WebControls.Panel pnlAdd;
		protected System.Web.UI.WebControls.DataGrid dgNews;
		protected System.Web.UI.WebControls.Button btnAdd;
		protected System.Web.UI.HtmlControls.HtmlInputFile fAttach;
		protected System.Web.UI.WebControls.Button btnAttach;
		protected System.Web.UI.HtmlControls.HtmlGenericControl attachtxt;
		protected System.Web.UI.HtmlControls.HtmlInputHidden hdnItmDoc;
		protected System.Web.UI.WebControls.HyperLink imgAttachment;
		protected System.Web.UI.HtmlControls.HtmlInputFile fImage;
		protected System.Web.UI.WebControls.Label lblError;
		protected System.Web.UI.WebControls.TextBox txtLink;
	
		private avii.Classes.clsUpdate oUpd = new avii.Classes.clsUpdate();
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!this.IsPostBack)
			{
				ViewState["Doc"] = null;
				imgAttachment.Visible= true;
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
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			this.btnAttach.Click += new System.EventHandler(this.btnAttach_Click);
			this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion



		private void btnSubmit_Click(object sender, System.EventArgs e)
		{
			bool bSave = true;
			lblError.Text = string.Empty;
			string sID,sTitle, sDesc, sStart, sType, sImage, sPublish, strSavedFileName,strDir, sDoc,sLink;
			sType=strSavedFileName=sID=sDoc=null;
			sTitle = (txtTitle.Text.Length > 0?txtTitle.Text:null);
			sDesc = (txtDesc.Text.Length > 0?txtDesc.Text:null);
			sStart = (txtStart.Text.Length > 0?txtStart.Text:null);
			sLink  = (txtLink.Text.Length > 0?txtLink.Text:null);
			sPublish = (chkPublish.Checked?"1":"0");
			sID = (hdnID.Value.Length > 0?hdnID.Value:null);
			if (sStart != null)
			{
				try
				{
					if (DateTime.Today < Convert.ToDateTime(sStart))
					{
						lblError.Text = "Date should not be less than Today's date";
						bSave = false;
					}
				}
				catch(Exception ex)
				{
					lblError.Text = "Incorrect date format";
					bSave = false;
				}
				
			}
			if (fAttach.PostedFile.FileName.Trim().Length > 0  && ViewState["Doc"] == null)
			{
				this.RegisterStartupScript("aaa","<script language='javascript'>alert('Click on ATTACH button to upload the Attachment.');</script>");
				bSave = false;
			}
			else
			{
				sDoc = (ViewState["Doc"]!= null?ViewState["Doc"].ToString():null);
			}
			if (bSave)
			{
				if (rbForms.Checked)
					sType = "F";
				else if (rbNews.Checked )
					sType = "N";
				if (fImage.PostedFile.FileName.Trim().Length > 0 )
				{
					Random r = new Random();
					int intUpLdStatus = 0;
					strDir = Server.MapPath(ConfigurationSettings.AppSettings["DownloadImages"]) + @"\";
					if (fImage.PostedFile.ContentType.ToUpper() == "IMAGE/GIF" ) 
						strSavedFileName =  "UF" +  r.Next() + ".gif";
					else if (fImage.PostedFile.ContentType.ToUpper() == "IMAGE/PJPEG")
						strSavedFileName =  "UF" + r.Next() + ".jpg";
					else
						throw new Exception ("File type is not valid, please enter only GIF or JPG files");
					avii.Classes.clsCommon.PicType = "I";
					intUpLdStatus = avii.Classes.clsCommon.fnUploadFile(fImage,strDir ,strSavedFileName);
					if (intUpLdStatus==0)
						sImage =  strSavedFileName;
					else
						sImage = ConfigurationSettings.AppSettings["DefaultImage"].ToString();
					strSavedFileName = sImage;
					//imgNews.ImageUrl = "./content/" + sImage;
				}
				else if (hdnItmImage.Value.Length > 0)
				{
					sImage =  hdnItmImage.Value.Trim();
				}
				oUpd.SetRecords(sID,sTitle,sDesc,strSavedFileName,sStart,sPublish,sType, sDoc,sLink);
				fnClean();
				dgNews.DataSource = oUpd.SrchUpdate((txtFTitle.Text.Trim().Length >0?txtFTitle.Text.Trim():null),dpDocType.SelectedIndex >0?dpDocType.SelectedValue:null);
				dgNews.DataBind();
			}
		}

		private void Button1_Click(object sender, System.EventArgs e)
		{
			pnlAdd.Visible = false;
			pnlSearch.Visible = true;
			dgNews.CurrentPageIndex = 0;
			dgNews.DataSource = null;
			dgNews.DataBind();
			txtFTitle.Text = string.Empty;
			dpDocType.SelectedIndex = 0;
			fnClean();
		}

		private void btnSearch_Click(object sender, System.EventArgs e)
		{
			if (pnlSearch.Visible == true)
			{
				string sTitle, sDoc;
				pnlAdd.Visible = false;
				sTitle =  (txtFTitle.Text.Trim().Length >0?txtFTitle.Text.Trim():null);
				sDoc = (dpDocType.SelectedIndex >0?dpDocType.SelectedValue:null);
				dgNews.CurrentPageIndex = 0;
				dgNews.DataSource = oUpd.SrchUpdate(sTitle,sDoc);
				dgNews.DataBind();
			}
			else
			{
				pnlSearch.Visible = true;
				pnlAdd.Visible = false;
			}
		}

		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			fnClean();
			pnlAdd.Visible = true;
			pnlSearch.Visible = false;
			dgNews.CurrentPageIndex = 0;
			dgNews.DataSource = null;
			dgNews.DataBind();
		}

		protected void dgNews_Command( System.Object sender, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			if (e.CommandName == "select")
			{
				if (e.CommandArgument.ToString().Length > 0)
				{
					fnUpdates(e.CommandArgument.ToString());
					pnlAdd.Visible = true;
				}
			}
		}


		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			fnClean();
		}
		private void fnClean()
		{
			txtTitle.Text = string.Empty;
			txtDesc.Text = string.Empty;
			txtLink.Text = string.Empty;
			hdnItmImage.Value =  string.Empty;
			hdnID.Value = string.Empty;
			txtStart.Text = string.Empty;
			rbForms.Checked = false;
			rbNews.Checked = false;
			chkPublish.Checked = false;
			pnlAdd.Visible = false;
			imgAttachment.Visible =false;
			ViewState["Doc"] = null;
		}


		private void  fnUpdates(string sUID)
		{
			DataTable oDt = oUpd.GetUpdates(sUID);
			if (oDt.Rows.Count > 0)
			{
				rbForms.Checked = false;
				rbNews.Checked = false;
				ViewState["Doc"] = null;
				hdnItmImage.Value = (oDt.Rows[0]["UImage"]  == DBNull.Value?string.Empty:oDt.Rows[0]["UImage"].ToString());
				hdnID.Value = (oDt.Rows[0]["UID"] == DBNull.Value?string.Empty:oDt.Rows[0]["UID"].ToString());
				txtTitle.Text = (oDt.Rows[0]["Title"] == DBNull.Value?string.Empty:oDt.Rows[0]["Title"].ToString());
				txtDesc.Text = (oDt.Rows[0]["UDesc"] == DBNull.Value?string.Empty:oDt.Rows[0]["UDesc"].ToString());
				txtLink.Text = (oDt.Rows[0]["ULink"] == DBNull.Value?string.Empty:oDt.Rows[0]["ULink"].ToString());
				chkPublish.Checked = Convert.ToBoolean(oDt.Rows[0]["Active"].ToString());
				imgAttachment.NavigateUrl = (oDt.Rows[0]["UDoc"] == DBNull.Value?string.Empty:"../content/" + oDt.Rows[0]["UDoc"].ToString());
				txtStart.Text = (oDt.Rows[0]["sDate"]  == DBNull.Value?string.Empty:Convert.ToDateTime(oDt.Rows[0]["sDate"].ToString()).ToShortDateString());
				//imgNews.ImageUrl =  (oDt.Rows[0]["UImage"]  == DBNull.Value?string.Empty:"../content/" + oDt.Rows[0]["UImage"].ToString());
				
				ViewState["Doc"] = (oDt.Rows[0]["UDoc"]  == DBNull.Value?null: oDt.Rows[0]["UDoc"].ToString());
				if (ViewState["Doc"] !=null)
				{
					imgAttachment.NavigateUrl = "../content/" + (oDt.Rows[0]["UDoc"]  == DBNull.Value?string.Empty:"../content/" + oDt.Rows[0]["UDoc"].ToString());
					imgAttachment.Visible= true;
				}
				else
					imgAttachment.Visible= false;
				if (oDt.Rows[0]["UType"] != DBNull.Value)
				{
					if (oDt.Rows[0]["UType"].ToString().ToUpper().Equals("FORM"))
						rbForms.Checked = true;
					else
						rbNews.Checked = true;	
				}			
			}


		}

		private void btnAttach_Click(object sender, System.EventArgs e)
		{
			string strSavedFileName,strDir,sImage;
			if (fAttach.PostedFile.FileName.Trim().Length > 0 )
			{
				Random r = new Random();
				int intUpLdStatus = 0;
			
				strDir = Server.MapPath(ConfigurationSettings.AppSettings["DownloadImages"]) + @"\";
				if (fAttach.PostedFile.ContentType.ToLower() == "application/msword" ) 
					strSavedFileName =  "UFA" +  r.Next() + ".doc";
				else if (fAttach.PostedFile.ContentType.ToLower() == "application/vnd.ms-excel")
					strSavedFileName =  "UFA" + r.Next() + ".xls";
				else if (fAttach.PostedFile.ContentType.ToLower() == "text/plain")
					strSavedFileName =  "UFA" + r.Next() + ".txt";
				else if (fAttach.PostedFile.ContentType.ToLower() == "application/pdf")
					strSavedFileName =  "UFA" + r.Next() + ".pdf";
				else if (fAttach.PostedFile.ContentType.ToLower() == "text/plain")
					strSavedFileName =  "UFA" + r.Next() + ".txt";
				else if (fAttach.PostedFile.ContentType.ToLower() == "application/vnd.ms-powerpoint")
					strSavedFileName =  "UFA" + r.Next() + ".ppt";
				else
					throw new Exception ("File type is not valid, please enter only MS Word/MS Excel/Text/PDF/Power Point files");
				avii.Classes.clsCommon.PicType = "I";
				intUpLdStatus = avii.Classes.clsCommon.fnUploadDocument(fAttach,strDir ,strSavedFileName);
				if (intUpLdStatus==0)
					sImage =  strSavedFileName;
				else
					sImage = ConfigurationSettings.AppSettings["DefaultImage"].ToString();
				strSavedFileName = null;
				//imgNews.ImageUrl = "./content/" + sImage;
				ViewState["Doc"] = sImage;
				imgAttachment.Visible= true;
				imgAttachment.NavigateUrl = "../content/" + sImage;
			}
			else if (hdnItmDoc.Value.Length > 0)
			{
				sImage =  hdnItmDoc.Value.Trim();
				ViewState["Doc"]=  hdnItmDoc.Value.Trim();
			}		
		}

        protected void btnSubmit_Click1(object sender, EventArgs e)
        {

        }


	}
}
