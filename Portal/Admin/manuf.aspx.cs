using System;
using System.Configuration;
using System.Data;
using System.Web.UI.WebControls;

namespace avii.Admin
{
	/// <summary>
	/// Summary description for manuf.
	/// </summary>
	public class manuf : System.Web.UI.Page
	{
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		protected System.Web.UI.WebControls.DataGrid dgManuf;
		protected System.Web.UI.WebControls.RadioButton rdManuf;
		protected System.Web.UI.WebControls.RadioButton rdSP;
		protected System.Web.UI.WebControls.DataGrid dgSP;
		
		private DataTable dtManuf;
		private Int32 iEditIndex;

		private  avii.Classes.clsManuf oManuf = new avii.Classes.clsManuf();
		private  avii.Classes.clsSP  oSP = new avii.Classes.clsSP();
		
		private void Page_Load(object sender, System.EventArgs e)
		{
			DataTable dtManuf = new DataTable();
			if (!this.IsPostBack)
			{
				datagridBindSP(1);
				if (rdManuf.Checked)
				{
					datagridBind(1);
					dgSP.DataSource= null;
					dgSP.DataBind();
				}
				//else if (rdSP.Checked)
					
		
			}

		}

		private void datagridBind(Int32 iRowAdd)
		{ 
			if (ViewState["Manuf"] == null) 
				dtManuf = oManuf.GetMauf();
			else
				dtManuf = (DataTable)ViewState["Manuf"];
			AddTableRows(iRowAdd);
			ViewState["Manuf"] = dtManuf;
			dgManuf.DataSource = dtManuf;
			dgManuf.DataBind();
		}

		private void AddTableRows(Int32 iRowAdd)
		{
			if (ViewState["Manuf"] == null) 
				dtManuf = oManuf.GetMauf();
			else
				dtManuf = (DataTable)ViewState["Manuf"];
			if (iRowAdd > 0)
			{
				DataRow dRow;
				dRow = dtManuf.NewRow();
				dtManuf.Rows.InsertAt(dRow,0);
				
			}
		}

		protected void dg_ItemCreated( System.Object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{

			System.Web.UI.WebControls.LinkButton lb;
			if (dgManuf.CurrentPageIndex ==0) 
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
			dgManuf.EditItemIndex = -1;
			datagridBind(0);
		}

		protected void dg_Edit( System.Object sender, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			iEditIndex = e.Item.ItemIndex;
			ViewState["EditIndex"] = iEditIndex;
			dgManuf.EditItemIndex = iEditIndex;
			datagridBind(0);
			ViewState["ID"] = dtManuf.Rows[iEditIndex]["ManufID"];
		}

		protected void dg_Update( System.Object sender, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string sMName,sMImage,sContentType,strSavedFileName,strDir;
			iEditIndex = (Int32)ViewState["EditIndex"];
			sMName = ((TextBox)e.Item.Cells[2].Controls[1]).Text;
			sMImage = ((System.Web.UI.HtmlControls.HtmlInputFile)e.Item.Cells[3].Controls[1]).Value;
			sContentType =  ((System.Web.UI.HtmlControls.HtmlInputFile)e.Item.Cells[3].Controls[1]).PostedFile.ContentType;
			if (sMImage.Length > 0 )
			{
				Random r = new Random();
				int intUpLdStatus = 0;
				
				strDir = Server.MapPath(System.Configuration.ConfigurationSettings.AppSettings["DownloadImages"]) + @"\";
				if (sContentType.ToUpper() == "IMAGE/GIF" ) 
					strSavedFileName =  "Mf" +  r.Next() + ".gif";
				else if (sContentType.ToUpper() == "IMAGE/PJPEG")
					strSavedFileName = "Mf" +  r.Next() + ".jpg";
				else
					throw new Exception ("File type is not valid, please enter only GIF or JPG files");

				intUpLdStatus = avii.Classes.clsCommon.fnUploadFile((System.Web.UI.HtmlControls.HtmlInputFile)e.Item.Cells[3].Controls[1],strDir ,strSavedFileName);
				if (intUpLdStatus!=0)
					strSavedFileName = ConfigurationSettings.AppSettings["DefaultImage"].ToString();
			}
			else
				strSavedFileName = ConfigurationSettings.AppSettings["DefaultImage"].ToString();

			if (iEditIndex > 0)
				oManuf.SetRecords(1,Convert.ToInt32(ViewState["ID"]),sMName, strSavedFileName,"M");
			else
				oManuf.SetRecords(1,0,sMName,strSavedFileName,"M");

			ViewState["Manuf"] = dtManuf;
			dgManuf.EditItemIndex = -1;
			datagridBind(1);
		}
	
		protected void dg_ItemCommand( System.Object sender, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			if (e.CommandName == "delete" && e.Item.ItemIndex > 0)
			{
				if (e.Item.Cells[3].Text != "&nbsp;")
					oManuf.SetRecords(2,Convert.ToInt32(e.Item.Cells[4].Text.Trim()),null,null,null);
				ViewState["Manuf"] = null;
				datagridBind(1);
			}
		}

		private void dpCust_SelectedIndexChanged(object sender, System.EventArgs e)
		{
//			if (dpCust.SelectedValue.Length > 0)
//				oManuf.SetDefault(dpCust.SelectedValue);
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
			this.rdManuf.CheckedChanged += new System.EventHandler(this.rdManuf_CheckedChanged);
			this.rdSP.CheckedChanged += new System.EventHandler(this.rdSP_CheckedChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void rdManuf_CheckedChanged(object sender, System.EventArgs e)
		{
			dgSP.DataSource = null;
			dgSP.DataBind();
			datagridBind(0);
		}

		private void rdSP_CheckedChanged(object sender, System.EventArgs e)
		{
			dgManuf.DataSource = null;
			dgManuf.DataBind();
			datagridBindSP(0);
		}

		//sp_SP_List
		private void datagridBindSP(Int32 iRowAdd)
		{ 
			if (ViewState["SP"] == null) 
				dtManuf = oSP.GetServiceProvider();
			else
				dtManuf = (DataTable)ViewState["SP"];
			AddTableRowsSP(iRowAdd);
			ViewState["SP"] = dtManuf;
			dgSP.DataSource = dtManuf;
			dgSP.DataBind();
		}

		private void AddTableRowsSP(Int32 iRowAdd)
		{
			if (ViewState["SP"] == null) 
				dtManuf = oSP.GetServiceProvider();
			else
				dtManuf = (DataTable)ViewState["SP"];
			if (iRowAdd > 0)
			{
				DataRow dRow;
				dRow = dtManuf.NewRow();
				dtManuf.Rows.InsertAt(dRow,0);
				ViewState["SP"] = dtManuf;
			}
		}

		protected void dg_SPItemCreated( System.Object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			System.Web.UI.WebControls.LinkButton lb;
			if (dgSP.CurrentPageIndex ==0) 
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

		protected void dg_SPCancel( System.Object sender, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			dgSP.EditItemIndex = -1;
			datagridBindSP(0);
		}

		protected void dg_SPEdit( System.Object sender, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			iEditIndex = e.Item.ItemIndex;
			ViewState["EditIndex"] = iEditIndex;
			dgSP.EditItemIndex = iEditIndex;
			datagridBindSP(0);
			ViewState["SID"] = dtManuf.Rows[iEditIndex]["SPID"];
		}

		protected void dg_SPUpdate( System.Object sender, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string sMName,sMImage,sContentType,strSavedFileName,strDir;
			iEditIndex = (Int32)ViewState["EditIndex"];
			sMName = ((TextBox)e.Item.Cells[2].Controls[1]).Text;
			sMImage = ((System.Web.UI.HtmlControls.HtmlInputFile)e.Item.Cells[3].Controls[1]).Value;
			sContentType =  ((System.Web.UI.HtmlControls.HtmlInputFile)e.Item.Cells[3].Controls[1]).PostedFile.ContentType;
			if (sMImage.Length > 0 )
			{
				Random r = new Random();
				int intUpLdStatus = 0;
				
				strDir = Server.MapPath(System.Configuration.ConfigurationSettings.AppSettings["DownloadImages"]) + @"\";
				if (sContentType.ToUpper() == "IMAGE/GIF" ) 
					strSavedFileName =  "Mf" +  r.Next() + ".gif";
				else if (sContentType.ToUpper() == "IMAGE/PJPEG")
					strSavedFileName = "Mf" +  r.Next() + ".jpg";
				else
					throw new Exception ("File type is not valid, please enter only GIF or JPG files");

				intUpLdStatus = avii.Classes.clsCommon.fnUploadFile((System.Web.UI.HtmlControls.HtmlInputFile)e.Item.Cells[3].Controls[1],strDir ,strSavedFileName);
				if (intUpLdStatus!=0)
					strSavedFileName = ConfigurationSettings.AppSettings["DefaultImage"].ToString();
			}
			else
				strSavedFileName = ConfigurationSettings.AppSettings["DefaultImage"].ToString();

			if (iEditIndex > 0)
				oSP.SetRecords(1,Convert.ToInt32(ViewState["ID"]),sMName, strSavedFileName,"S");
			else
				oSP.SetRecords(1,0,sMName,strSavedFileName,"S");

			ViewState["SP"] = dtManuf;
			dgSP.EditItemIndex = -1;
			datagridBindSP(1);
		}
	
		protected void dg_SPItemCommand( System.Object sender, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			if (e.CommandName == "delete" && e.Item.ItemIndex > 0)
			{
				if (e.Item.Cells[3].Text != "&nbsp;")
					oSP.SetRecords(2,Convert.ToInt32(e.Item.Cells[4].Text.Trim()),null,null,null);
				ViewState["SP"] = null;
				datagridBindSP(1);
			}
		}

	}
}
