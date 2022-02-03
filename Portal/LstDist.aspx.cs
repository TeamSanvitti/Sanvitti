using System;
using System.Configuration;
using System.Data;

namespace avii
{
	/// <summary>
	/// Summary description for LstDist.
	/// </summary>
	public class LstDist : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label lblPhoneTitle;
		protected System.Web.UI.WebControls.Label lblDesc;
		protected System.Web.UI.HtmlControls.HtmlImage imgPhone;
		protected System.Web.UI.HtmlControls.HtmlImage imgManuf;
		protected System.Web.UI.WebControls.Label lblModel;
		protected System.Web.UI.WebControls.Label lblAvail;
		protected System.Web.UI.WebControls.DataGrid dgItems;
		protected System.Web.UI.WebControls.Panel pnlSim;
		protected System.Web.UI.WebControls.DataGrid dgFeature;
		protected System.Web.UI.WebControls.Label lblWarnt;
		protected System.Web.UI.WebControls.DataGrid dgSP;
		protected System.Web.UI.WebControls.DataGrid dgSP1;
		protected System.Web.UI.WebControls.LinkButton LinkButton1;
		protected System.Web.UI.WebControls.Label lblPhoneType;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				if (!this.IsPostBack){
                    if (Session["userInfo"] == null && Request.Params["p"] == "o")
                    Response.Redirect(@"/logon.aspx");
 
					if (Request.Params["pid"] != null)
					{
						ViewState["pid"] = Request.Params["pid"] ;
						avii.Classes.clsItem oItm = new avii.Classes.clsItem();
						DataSet ods = new DataSet();
						ods = oItm.GetItemDetail(Request.Params["pid"].ToString());
						if (ods.Tables.Count > 0)
						{
							string sHidePrice;
							sHidePrice = (ods.Tables[0].Rows[0]["HidePrice"] == DBNull.Value?string.Empty:ods.Tables[0].Rows[0]["HidePrice"].ToString());
							lblPhoneTitle.Text = (ods.Tables[0].Rows[0]["PhoneTitle"] == DBNull.Value?string.Empty:ods.Tables[0].Rows[0]["PhoneTitle"].ToString());
							lblModel.Text =  (ods.Tables[0].Rows[0]["PhoneModel"] == DBNull.Value?string.Empty:ods.Tables[0].Rows[0]["PhoneModel"].ToString());
							lblAvail.Text =  (ods.Tables[0].Rows[0]["Available"] == DBNull.Value?string.Empty:ods.Tables[0].Rows[0]["Available"].ToString());
							lblDesc.Text =  (ods.Tables[0].Rows[0]["PhoneDesc"] == DBNull.Value?string.Empty:ods.Tables[0].Rows[0]["PhoneDesc"].ToString());
							//lblDim.Text =  (ods.Tables[0].Rows[0]["PDimension"] == DBNull.Value?string.Empty:ods.Tables[0].Rows[0]["PDimension"].ToString());
							//lblWarnt.Text =  (ods.Tables[0].Rows[0]["warrenty"] == DBNull.Value?string.Empty:ods.Tables[0].Rows[0]["warrenty"].ToString());
							//lblBat.Text =  (ods.Tables[0].Rows[0]["Battery"] == DBNull.Value?string.Empty:ods.Tables[0].Rows[0]["Battery"].ToString());

							//lblManuf.Text = ods.Tables[0].Rows[0]["ManufNAme"].ToString();
							lblPhoneType.Text = ods.Tables[0].Rows[0]["PhoneTypeTxt"].ToString();
							imgPhone.Src = ConfigurationSettings.AppSettings["Content"] + ( (ods.Tables[0].Rows[0]["PhoneImage"] == DBNull.Value?string.Empty:ods.Tables[0].Rows[0]["PhoneImage"].ToString()));
							imgManuf.Src = ConfigurationSettings.AppSettings["Content"] + ods.Tables[0].Rows[0]["ManufImage"].ToString();
						
							if (ods.Tables[1].Rows.Count > 0)
							{
								dgFeature.DataSource = ods.Tables[1];
								dgFeature.DataBind();
							}
							if (sHidePrice.Length == 0)
							{
								if (ods.Tables[2].Rows.Count > 0)
								{
									ods.Tables[2].DefaultView.RowFilter = "DefaultType = 'Y'";
							/*		if (ods.Tables[2].DefaultView.Count > 0)
									{
										lblPrice.Text = string.Format("{0:C}", ods.Tables[2].DefaultView[0]["Price"]);
										this.Button2.Enabled = true;
										txtQty.ReadOnly = false;
									}
							*/
								}
							/*	else{
									this.Button2.Enabled = false;
									txtQty.ReadOnly = true;
									}
							*/
							}
							/*else 
							{
								lblPrice.Text = "Please call to get the price";
								this.Button2.Enabled = false;
								txtQty.ReadOnly = true;
							}*/

							if (ods.Tables[3].Rows.Count > 0)
							{
								ods.Tables[3].DefaultView.RowFilter = "chk='True'";
								dgSP.DataSource = ods.Tables[3].DefaultView;
								dgSP.DataBind();
							}
							else
							{
								dgSP.CurrentPageIndex= 0;
								dgSP.DataSource= null;
								dgSP.DataBind();
							}
							

							if (ods.Tables[4].Rows.Count > 0)
							{
								pnlSim.Visible=true;
								dgItems.DataSource = ods.Tables[4].DefaultView;
								dgItems.DataBind();
							}
							else
								pnlSim.Visible=false;


						}
					}
				}
			}
			catch (Exception ex)
			{
//				outError.Text= ex.Message;
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
			this.LinkButton1.Click += new System.EventHandler(this.LinkButton1_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion



		private void Button2_Click(object sender, System.EventArgs e)
		{
/*			try 
			{
				if (ViewState["pid"] != null)
				{
					if (txtQty.Text.Trim().Length > 0)
					{
						DataTable odt = ((DataTable)Session["shopcart"]);
						//						if (odt.Select("ItemID = '" + ViewState["pid"].ToString() + "'")  != null)
						//							this.RegisterStartupScript("itmext","<script language='javascript'>alert('Item already exists in order list, please go to order list to update the quantity of item');</script>");
						//						else 
						//						{
						if (Convert.ToInt32(txtQty.Text.Trim()) > 0)
						{
							try 
							{
								Classes.clsCart.Add(ref odt,ViewState["pid"].ToString(),lblPhoneTitle.Text + "[" + lblModel.Text + "]" + " - " + lblPhoneType.Text,"0","0");	//txtQty.Text,lblPrice.Text
								//Response.Write(odt.Rows.Count);
								Session["shopcart"] = odt;
//								if (Session["shopcart"] != null)
//									this.imgCart.Visible=true;
//								else
//									imgCart.Visible = false;
								this.RegisterStartupScript("itmqty","<script language='javascript'>alert('Item has been added to the Order list, click on view cart to change the item quantity');</script>");
							}
							catch (Exception ex)
							{
								this.RegisterStartupScript("itmqty","<script language='javascript'>alert('" + ex.Message + "');</script>");
							}
						}
						else
							this.RegisterStartupScript("itmqty","<script language='javascript'>alert('Qty should be greater than 0');</script>");

						//						}
					}
				}
				else
				{
					this.RegisterStartupScript("itmqty","<script language='javascript'>alert('Invalid item selection, Please select the item from the list');</script>");

				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
*/		}

		private void imgCart_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
            Response.Redirect(@"/frmCart.aspx");
		}

		private void LinkButton1_Click(object sender, System.EventArgs e)
		{
			string  s = string.Empty;
			if (Request.Params["p"] !=null)
			{
				Response.Redirect(@"/list.aspx?p=" + Request.Params["p"].ToString());
			}
			else
			{
				Response.Redirect("@/list.aspx?p=n");
			}
		}
	}
}
