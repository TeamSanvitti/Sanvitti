using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SV.Framework.Models.Inventory;

namespace avii.InternalInventory
{
    public partial class TransferOrderAssignments : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                LoadTransferOrders();
        }
        private void LoadTransferOrders()
        {

            lblCount.Text = "";
            lblMsg.Text = "";
            gvOrders.DataSource = null;
            gvOrders.DataBind();

            if (Session["OrderTransferID"] != null)
            {
                Int64 orderTransferID = Convert.ToInt64(Session["OrderTransferID"]);
                SV.Framework.Inventory.TransferOrderOperation orderOperations = SV.Framework.Inventory.TransferOrderOperation.CreateInstance<SV.Framework.Inventory.TransferOrderOperation>();
                List<TransferOrderAssignment> orderList = orderOperations.GetTransferOrderAssignments(orderTransferID);
                if (orderList != null && orderList.Count > 0)
                {
                    lblOrderTransferNumber.Text = orderList[0].OrderTransferNumber.ToString();
                    lblOrderDate.Text = orderList[0].OrderTransferDate;
                    lblRequestedQty.Text = orderList[0].RequestedQty.ToString();
                    lblOrderStatus.Text = orderList[0].OrderTransferStatus;
                    lblAssignmentStatus.Text = orderList[0].OrderTransferAssignmentStatus;
                    gvOrders.DataSource = orderList;
                    gvOrders.DataBind();
                    lblCount.Text = "Total count: " + orderList.Count.ToString();

                }
            }

        }
    }
}