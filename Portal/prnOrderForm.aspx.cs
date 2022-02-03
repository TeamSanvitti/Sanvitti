using System;
using System.Data;

namespace avii
{
	/// <summary>
	/// Summary description for prnOrderForm.
	/// </summary>
	public class prnOrderForm : System.Web.UI.Page
	{
		#region "Protected Objects"
		protected System.Web.UI.WebControls.Label lblState;
		protected System.Web.UI.WebControls.Label lblZip;
		protected System.Web.UI.WebControls.Label lblBAddr;
		protected System.Web.UI.WebControls.Label lblBState;
		protected System.Web.UI.WebControls.Label lblBZip;
		protected System.Web.UI.WebControls.Label lblBillDate;
		protected System.Web.UI.WebControls.Label lblCity;
		protected System.Web.UI.WebControls.Label lblBCity;
		protected System.Web.UI.WebControls.DataGrid dgCart;
		protected System.Web.UI.WebControls.Label lblGTotal;
		protected System.Web.UI.WebControls.Label lblComments;
		protected System.Web.UI.WebControls.Label lblAddr;
		#endregion

		private string sCustomerID;
		private void Page_Load(object sender, System.EventArgs e)
		{
			
			if (!this.IsPostBack)
			{
				if (Request.Params["oid"] != null)
				{
					Classes.clsOrder oOrd = new avii.Classes.clsOrder();
					DataSet oDs;
					oDs = oOrd.GetOrderPrint(Request.Params["oid"]);
					if (oDs != null)
					{
						if (oDs.Tables[0].Rows.Count > 0)
						{
							lblBillDate.Text = DateTime.Today.ToShortDateString();
							sCustomerID = Convert.ToString(oDs.Tables[0].Rows[0]["CustomerID"]);
							fnPopulateClient();
							lblGTotal.Text= (oDs.Tables[0].Rows[0]["InvoiceAmt"] != DBNull.Value?string.Format("{0:c}",oDs.Tables[0].Rows[0]["InvoiceAmt"]):string.Empty);;
							lblComments.Text =  (oDs.Tables[0].Rows[0]["Comments"] != DBNull.Value?Convert.ToString(oDs.Tables[0].Rows[0]["Comments"]):string.Empty);
						}
						if (oDs.Tables[1].Rows.Count > 0)
						{
							dgCart.DataSource = oDs.Tables[1];
							dgCart.DataBind();
						}
					}
				}
				else
				{
					if (Session["shopcart"] != null)
					{
						lblBillDate.Text = DateTime.Today.ToShortDateString();
						fnPopulateClient();
						dgCart.DataSource = (DataTable)Session["shopcart"];
						dgCart.DataBind();
						lblGTotal.Text= GetTotalSale((DataTable)Session["shopcart"]).ToString("c");
						//pnlcart.Visible= true;
					}
					else
					{
						//					Response.Write(Server
						//lblErr.Text="Shopping cart is empty";
						//pnlcart.Visible=false;
					}
				}
			}
		}

		private void fnPopulateClient()
		{
			if (Session["CustID"] != null)
			{
				sCustomerID  = Session["CustID"].ToString();
			}
				Classes.clsCust oCust = new Classes.clsCust();
				DataSet oDs =	oCust.GetCustInfo(sCustomerID);
				if (oDs.Tables[0].Rows.Count > 0)
				{
					lblAddr.Text = (oDs.Tables[0].Rows[0]["Address"] !=null?oDs.Tables[0].Rows[0]["Address"].ToString():string.Empty);
					//lblAddr2.Text = (oDs.Tables[0].Rows[0]["Address2"]!=null?oDs.Tables[0].Rows[0]["Address2"].ToString():string.Empty);
					lblCity.Text = (oDs.Tables[0].Rows[0]["City"] !=null?oDs.Tables[0].Rows[0]["City"].ToString():string.Empty);
					lblState.Text = (oDs.Tables[0].Rows[0]["State"] !=null?oDs.Tables[0].Rows[0]["State"].ToString():string.Empty);
					lblZip.Text = (oDs.Tables[0].Rows[0]["Zip"] !=null?oDs.Tables[0].Rows[0]["Zip"].ToString():string.Empty);
					lblBAddr.Text = (oDs.Tables[0].Rows[0]["BillAddress"] !=null?oDs.Tables[0].Rows[0]["BillAddress"].ToString():string.Empty);
					//lblBAddr2.Text = (oDs.Tables[0].Rows[0]["BillAddress2"] !=null?oDs.Tables[0].Rows[0]["BillAddress2"].ToString():string.Empty);
					lblBCity.Text = (oDs.Tables[0].Rows[0]["BillCity"] !=null?oDs.Tables[0].Rows[0]["BillCity"].ToString():string.Empty);
					lblBState.Text = (oDs.Tables[0].Rows[0]["BillState"] !=null?oDs.Tables[0].Rows[0]["BillState"].ToString():string.Empty);
					lblBZip.Text = (oDs.Tables[0].Rows[0]["BillZip"] !=null?oDs.Tables[0].Rows[0]["BillZip"].ToString():string.Empty);
				}
				if (Session["OrderCmt"] != null)
					lblComments.Text = Session["OrderCmt"].ToString();
		}

		private double GetTotalSale(DataTable oDt)
		{
			double dPrice = 0;
			if (oDt != null)
			{
				foreach (DataRow dRow in oDt.Rows)
				{
					dPrice = dPrice + Convert.ToDouble(dRow["Total"]);
				}
			}
			return dPrice;
		}

		private void fnGetOrderInfo(string sOid)
		{

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
