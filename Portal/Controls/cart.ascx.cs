namespace pndt.Controls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		Summary description for cart.
	/// </summary>
	public class cart : System.Web.UI.UserControl
	{
		protected System.Web.UI.WebControls.Label lblGTotal;
		protected System.Web.UI.WebControls.Button btnCheckout;
		protected System.Web.UI.WebControls.Button btnUpd;
		protected System.Web.UI.WebControls.Label lblerr;
		protected System.Web.UI.WebControls.Panel pnlcart;
		protected System.Web.UI.WebControls.DataGrid dgCart;

		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!this.IsPostBack)
			{
				if (Session["shopcart"] != null)
				{
					dgCart.DataSource = (DataTable)Session["shopcart"];
					dgCart.DataBind();
					lblGTotal.Text= GetTotalSale((DataTable)Session["shopcart"]).ToString("c");
					pnlcart.Visible= true;
				}
				else
				{
//					Response.Write(Server
					lblerr.Text="Shopping cart is empty";
					pnlcart.Visible=false;
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
			this.btnUpd.Click += new System.EventHandler(this.btnUpd_Click);
			this.btnCheckout.Click += new System.EventHandler(this.btnCheckout_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion


		protected void dg_DataBound( System.Object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			
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
		private void btnCheckout_Click(object sender, System.EventArgs e)
		{
            Server.Transfer(@"/frmOrder.aspx");
		}

		private void btnUpd_Click(object sender, System.EventArgs e)
		{
			DataTable odt = ((DataTable)Session["shopcart"]);
			if (odt  == null)
			{
				lblerr.Text = "Items are not selected in Orderlist, please select item again" ;
				
			}
			else 
			{
				foreach (DataGridItem dgItem in dgCart.Items)
				{
					if (Convert.ToInt16(((TextBox)(dgItem.Cells[1].Controls[1])).Text) > 0)
					{
						odt.Rows[dgItem.ItemIndex]["Qty"] = ((TextBox)(dgItem.Cells[1].Controls[1])).Text;
						odt.Rows[dgItem.ItemIndex]["Total"] = (Convert.ToDouble(odt.Rows[dgItem.ItemIndex]["Price"])  * Convert.ToInt16(odt.Rows[dgItem.ItemIndex]["Qty"])) ;
						
					}
				}	
				odt.AcceptChanges();
				Session["shopcart"] = odt;
				dgCart.DataSource = (DataTable)Session["shopcart"];
				dgCart.DataBind();
				lblGTotal.Text= GetTotalSale(odt).ToString("c");
			}
		}

	}
}
