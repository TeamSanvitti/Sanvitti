using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using avii.Classes;
namespace avii.Controls
{
    public partial class WarehouseCode : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void BindWarehouseCode(int companyID)
        {
            List<CustomerWarehouseCode> warehouseCodeList = WarehouseCodeOperations.GetCompanyWarehouseCode(companyID, null, false);
            if (warehouseCodeList != null && warehouseCodeList.Count > 0)
            {
                gvWarehouse.DataSource = warehouseCodeList;
                gvWarehouse.DataBind();
                lblMessage.Text = string.Empty;
            }
            else
            {
                lblMessage.Text = "No record exists";
                gvWarehouse.DataSource = null;
                gvWarehouse.DataBind();
            }
        }
        
    }
}