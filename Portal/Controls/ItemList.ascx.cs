using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace avii.Controls
{
    public partial class ItemList : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!IsPostBack)
            //{
                PopulateData();
            //}
        }

        private void PopulateData()
        {
            grdItem.DataSource = null;
            grdItem.DataBind();

            avii.Classes.InventoryList inventoryList = avii.Classes.PurchaseOrder.GetInventory(0);
            if (inventoryList != null && inventoryList.CurrentInventory.Count > 0)
            {
                grdItem.DataSource = inventoryList.CurrentInventory;
                grdItem.DataBind();
            }
            //else
            //{
            //    chkHistory.Visible = false;
            //    lblMsg.Text = "No Inventory assigned, please contact Aerovocie Administrator";
            //}
        }

    }
}