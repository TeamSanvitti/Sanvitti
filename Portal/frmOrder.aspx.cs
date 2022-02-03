using System;
using System.Data;
using System.Text;
using System.Web.UI.WebControls;

namespace avii
{
	/// <summary>
	/// Summary description for frmOrder.
	/// </summary>
	public class frmOrder : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label lblState;
		protected System.Web.UI.WebControls.DataGrid dgCart;
		protected System.Web.UI.WebControls.Button btnSubmit;
		protected System.Web.UI.WebControls.Button btnPrint;
		protected System.Web.UI.WebControls.TextBox txtComment;
		protected System.Web.UI.WebControls.Label lblGTotal;
		protected System.Web.UI.WebControls.Label lblAddr1;
		protected System.Web.UI.WebControls.Label lblAddr2;
		protected System.Web.UI.WebControls.Label lblCity;
		protected System.Web.UI.WebControls.Label lblZip;
		protected System.Web.UI.WebControls.Label lblBAddr1;
		protected System.Web.UI.WebControls.Label lblBAddr2;
		protected System.Web.UI.WebControls.Label lblBCity;
		protected System.Web.UI.WebControls.Label lblBZip;
		protected System.Web.UI.WebControls.Label lblBState;
		protected System.Web.UI.WebControls.Label lblErr;
	
		Classes.clsCust oCust = new Classes.clsCust();
		private void Page_Load(object sender, System.EventArgs e)
		{
			btnPrint.Attributes.Add("onclick","fnPrnMsg();");
			if (!this.IsPostBack)
			{
				if (Session["shopcart"] != null)
				{
					fnPopulateClient();
					dgCart.DataSource = (DataTable)Session["shopcart"];
					dgCart.DataBind();
					lblGTotal.Text= GetTotalSale((DataTable)Session["shopcart"]).ToString("c");
					//pnlcart.Visible= true;
				}
				else
				{
					//					Response.Write(Server
					this.RegisterStartupScript("cart","<script language='javascript'>alert('Shopping cart is empty');</script>");
					//pnlcart.Visible=false;
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
			this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void btnSubmit_Click(object sender, System.EventArgs e)
		{
			string sOrderNum = string.Empty;
            if (Session["userInfo"] == null)
//			{
//				Response.Write("<BR>" + Session["User"].ToString());
//				Response.Write("<BR>" + Session["DefCust"].ToString());
//				Response.Write("<BR>" + Session["Name"].ToString());
//			}
//			else
                Response.Redirect(@"/logon.aspx");
		
			StringBuilder oSb = new StringBuilder();
			foreach (DataGridItem dgItem in dgCart.Items)
			{
				oSb.Append(dgItem.Cells[0].Text + "|" + dgItem.Cells[2].Text + "||" + dgItem.Cells[3].Text + "#" );
			}
			if (txtComment.Text.Trim().Length > 0)
				Session["OrderCmt"] = txtComment.Text.Trim();

			Classes.clsOrder oOrder = new Classes.clsOrder();
			sOrderNum = oOrder.SetOrder(Session["CustID"].ToString(), Session["DefCust"].ToString(),null,(txtComment.Text.Length == 0?null:txtComment.Text.Trim()),oSb.ToString());
			if (sOrderNum.Length > 0)
			{

				btnPrint.Visible = true;
				btnSubmit.Enabled = false;
				this.RegisterStartupScript("submit","<script language='javascript'>fnSbmtMsg('" + sOrderNum +"');</script>");
			}
			else
				lblErr.Text = "Order cannot be placed, please contact administrator";
			
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

		private void fnPopulateClient()
		{
			if (Session["CustID"] != null)
			{
				DataSet oDs =	oCust.GetCustInfo(Session["CustID"].ToString());
				if (oDs.Tables[0].Rows.Count > 0)
				{
					lblAddr1.Text = (oDs.Tables[0].Rows[0]["Address1"] !=null?oDs.Tables[0].Rows[0]["Address1"].ToString():string.Empty);
					lblAddr2.Text = (oDs.Tables[0].Rows[0]["Address2"]!=null?oDs.Tables[0].Rows[0]["Address2"].ToString():string.Empty);
					lblCity.Text = (oDs.Tables[0].Rows[0]["City"] !=null?oDs.Tables[0].Rows[0]["City"].ToString():string.Empty);
					lblState.Text = (oDs.Tables[0].Rows[0]["State"] !=null?oDs.Tables[0].Rows[0]["State"].ToString():string.Empty);
					lblZip.Text = (oDs.Tables[0].Rows[0]["Zip"] !=null?oDs.Tables[0].Rows[0]["Zip"].ToString():string.Empty);
					lblBAddr1.Text = (oDs.Tables[0].Rows[0]["BillAddress1"] !=null?oDs.Tables[0].Rows[0]["BillAddress1"].ToString():string.Empty);
					lblBAddr2.Text = (oDs.Tables[0].Rows[0]["BillAddress2"] !=null?oDs.Tables[0].Rows[0]["BillAddress2"].ToString():string.Empty);
					lblBCity.Text = (oDs.Tables[0].Rows[0]["BillCity"] !=null?oDs.Tables[0].Rows[0]["BillCity"].ToString():string.Empty);
					lblBState.Text = (oDs.Tables[0].Rows[0]["BillState"] !=null?oDs.Tables[0].Rows[0]["BillState"].ToString():string.Empty);
					lblBZip.Text = (oDs.Tables[0].Rows[0]["BillZip"] !=null?oDs.Tables[0].Rows[0]["BillZip"].ToString():string.Empty);
				}
			}
		}

		private void btnPrint_Click(object sender, System.EventArgs e)
		{
		
		}

	}
}
