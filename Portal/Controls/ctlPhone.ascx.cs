namespace avii.Controls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		Summary description for ctlPhone.
	/// </summary>
	public class ctlPhone : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.DataList dl;
		protected System.Web.UI.WebControls.DropDownList dpType;
		protected System.Web.UI.WebControls.DropDownList dpManuf;
		protected System.Web.UI.WebControls.DropDownList dpSP;
		protected System.Web.UI.WebControls.Panel pnlNot;
		protected System.Web.UI.WebControls.Label outError;

		private void Page_Load(object sender, System.EventArgs e)
		{
            if (Session["userInfo"] == null && Request.Params["p"] == "o")
                Response.Redirect(@"/logon.aspx");

				
			if (!this.IsPostBack)
			{
				avii.Classes.clsContent oContent = new avii.Classes.clsContent();
				avii.Classes.clsCustType ocust = new avii.Classes.clsCustType();
				avii.Classes.clsItem clsItem = new avii.Classes.clsItem();
				avii.Classes.clsManuf oManuf = new avii.Classes.clsManuf();

				dpType.DataSource = clsItem.GetPTypes(null);
				dpType.DataTextField = "PDesc";
				dpType.DataValueField = "ID";
				dpType.DataBind();
				dpType.Items.Insert(0,"Type");

				dpManuf.DataSource = oManuf.GetMauf();
				dpManuf.DataTextField = "ManufName";
				dpManuf.DataValueField = "ManufID";
				dpManuf.DataBind();
				dpManuf.Items.Insert(0,"Manufacturer");


				
				string sPhoneType,  sManuf,  sSpecial,  sNew,  sOld,  sRef, sPhonetxt,sDefCust, sSP, sAcc;
				sPhoneType = (Request.Params["pt"] != null?Request.Params["pt"]:null);
				sSP = (Request.Params["sp"] != null?Request.Params["sp"]:null);
				sManuf = (Request.Params["mn"] != null?Request.Params["mn"]:null);  
				sSpecial =  (Request.Params["p"] == "s"?"1":null);
				sNew = (Request.Params["p"] == "n"?"1":null);
				sOld =  (Request.Params["p"] == "o"?"1":null);  
				sRef =  (Request.Params["p"] == "r"?"1":null);
				sAcc = (Request.Params["p"] == "a"?"1":null);
				sPhonetxt = (Request.Params["verb"] != null?Request.Params["verb"]:null);
				sDefCust = (Session["DefCust"]!=null?Session["DefCust"].ToString():ocust.GetDefault());
				if (sNew == null && sOld == null && sRef == null && sAcc == null)
				{
					sNew = "1";
				}
																
				//PUPULATE THE DROP DOWNS
				//===================================================================
				if (Request.Params["pt"] != null)
					dpType.SelectedValue =Request.Params["pt"];
				if (Request.Params["mn"] != null)
					dpManuf.SelectedValue =Request.Params["mn"];
				if (Request.Params["sp"] != null)
					dpSP.SelectedValue =Request.Params["sp"];
				//=====================================================================
				if (dpSP.Items.Count == 0)
				{
					avii.Classes.clsItem  oItem = new avii.Classes.clsItem();
					dpSP.DataSource = oItem.GetServiceProvider();
					dpSP.DataTextField ="ServiceProvider"; 
					dpSP.DataValueField = "SPID";
					dpSP.DataBind();
					dpSP.Items.Insert(0,"Service Provider");
					oItem = null;

				}
				if (sDefCust.Equals(string.Empty))
					Page.RegisterStartupScript("aa","<script language='javascript'>alert('Default customer has not set');</script>");
				else
				{
					DataSet ods = null;
					ods = oContent.GetContent(sPhoneType,sManuf,sSpecial,sNew,sOld,sRef,sPhonetxt,sDefCust,sSP,sAcc);
					if (ods.Tables.Count > 0 )
					{
						if (ods.Tables[0].Rows.Count > 0) 
						{
							Session["content"] = ods;
							dl.DataSource = ods;
							dl.DataBind();
							dl.Visible = true;
							pnlNot.Visible = false;
						}
						else
						{
//							Page.RegisterStartupScript("aa","<script language='javascript'>alert('None of record exist in list to selected criteria');</script>");
//							dl.DataSource = (Session["content"] != null? (DataSet)Session["content"]:null);
//							dl.DataBind();
							pnlNot.Visible = true;
							dl.DataSource = null;
							dl.DataBind();
							dl.Visible = false;							
						}
					}
					else
					{
						pnlNot.Visible = true;
						dl.DataSource = null;
						dl.DataBind();
						dl.Visible = false;
						//Page.RegisterStartupScript("aa","<script language='javascript'>alert('None of record exist in list to selected criteria');</script>");
					}	
					
//					ods = oContent.GetContent(sPhoneType,sManuf,"1",sNew,sOld,sRef,sPhonetxt,sDefCust);
//					if (ods.Tables.Count > 0 )
//					{
//						dlSpecial.DataSource = ods;
//						dlSpecial.DataBind();
//					}
//					else
//						Page.RegisterStartupScript("aa","<script language='javascript'>alert('None of record exist in list to selected criteria');</script>");
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
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
