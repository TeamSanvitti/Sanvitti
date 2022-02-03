using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace avii
{
    public partial class WarehouseCodeItems : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindWarehouseCodeItems();
        }
        private void BindWarehouseCodeItems()
        { 
           //gvWarehouseItems
            string warehouseCode = string.Empty;
            if (Request["wh"] != null)
            {
                warehouseCode = Request["wh"].ToString();
                DataTable dt = avii.Classes.WarehouseCodeOperations.GetWarehouseCodeItems(warehouseCode);
                if (dt != null && dt.Rows.Count > 0)
                {
                    lblCompany.Text = dt.Rows[0]["CompanyName"].ToString() + " (" + dt.Rows[0]["CompanyAccountNumber"].ToString() + "):";

                    gvWarehouseItems.DataSource = dt;
                    msgPanel.Visible = false;
                }
                else
                {
                    gvWarehouseItems.DataSource = null;
                    msgPanel.Visible = true;

                }
                gvWarehouseItems.DataBind();


            }
        }
    }
}