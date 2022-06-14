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
                LoadTransientOrders();
        }

        private void LoadTransientOrders()
        {
            lblCount.Text = "";
            lblMsg.Text = "";
            gvOrders.DataSource = null;
            gvOrders.DataBind();

            if (Session["TransientOrderID"] != null)
            {
                Int64 TransientOrderID = Convert.ToInt64(Session["TransientOrderID"]);
                SV.Framework.Inventory.TransientOrderOperation orderOperations = SV.Framework.Inventory.TransientOrderOperation.CreateInstance<SV.Framework.Inventory.TransientOrderOperation>();
                List<TransientOrderAssignment> orderList = orderOperations.GetTransientOrderAssignments(TransientOrderID);
                if (orderList != null && orderList.Count > 0)
                {
                    lblOrderTransferNumber.Text = orderList[0].MemoNumber.ToString();
                    lblOrderDate.Text = orderList[0].TransientOrderDate;
                    lblOrderedQty.Text = orderList[0].OrderedQty.ToString();
                    lblOrderStatus.Text = orderList[0].OrderTransientStatus;
                    lblAssignmentStatus.Text = orderList[0].OrderTransientReceiveStatus;
                    if (orderList[0].ReceivedQty > 0)
                    {
                        gvOrders.DataSource = orderList;
                        gvOrders.DataBind();
                        lblCount.Text = "Total count: " + orderList.Count.ToString();
                    }
                    else
                    {
                        lblMsg.Text = "No receive found";
                    }

                }
                else
                {
                    lblMsg.Text = "No receive found";
                }
            }

        }

    }
}