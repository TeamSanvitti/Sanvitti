using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace avii
{
    public partial class RunningStockCronJob : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            SV.Framework.Inventory.StockOperations stockOperations = SV.Framework.Inventory.StockOperations.CreateInstance<SV.Framework.Inventory.StockOperations>();

            int returnValue = stockOperations.RunningStockInsertUpdate(null);

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            SV.Framework.Inventory.StockOperations stockOperations = SV.Framework.Inventory.StockOperations.CreateInstance<SV.Framework.Inventory.StockOperations>();

            string date = txtDate.Text.Trim();

            int returnValue = stockOperations.RunningStockInsertUpdate(date);
        }
    }
}