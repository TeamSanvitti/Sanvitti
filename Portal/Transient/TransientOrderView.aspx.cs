using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SV.Framework.Models.Inventory;

namespace avii.Transient
{
    public partial class TransientOrderView : System.Web.UI.Page
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
                SV.Framework.Inventory.TransientOrderOperation orderOperations = SV.Framework.Inventory.TransientOrderOperation.CreateInstance<SV.Framework.Inventory.TransientOrderOperation>();
                List<TransientOrderAssignment> orderList = orderOperations.GetTransientOrderAssignments(orderTransferID);
                if (orderList != null && orderList.Count > 0)
                {
                    lblOrderTransferNumber.Text = orderList[0].MemoNumber.ToString();
                    lblOrderDate.Text = orderList[0].TransientOrderDate;
                    lblRequestedQty.Text = orderList[0].RequestedQty.ToString();
                    lblOrderStatus.Text = orderList[0].OrderTransientStatus;
                    lblAssignmentStatus.Text = orderList[0].OrderTransientReceiveStatus;
                    gvOrders.DataSource = orderList;
                    gvOrders.DataBind();
                    lblCount.Text = "Total count: " + orderList.Count.ToString();

                }
            }

        }
    }
}