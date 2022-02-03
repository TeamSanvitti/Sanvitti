using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SV.Framework.Models.Fulfillment;

namespace avii.Controls
{
    public partial class PODetails : System.Web.UI.UserControl
    {
        private string poNum = string.Empty;
        private string companyAccountNumber = string.Empty;
        
        public string PoNum
        {
            get
            {
                return poNum;
            }
            set
            {
                poNum = value;
            }
        }
        public string CompanyAccountNumber
        {
            get
            {
                return companyAccountNumber;
            }
            set
            {
                companyAccountNumber = value;
            }
        }
        
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(PoNum) && !string.IsNullOrEmpty(CompanyAccountNumber))
                {
                    //string poNum = Request["ponum"].ToString();
                    //string companyAccountNumber = Request["companyaccountnumber"].ToString();

                    //BindPO(poNum, companyAccountNumber);
                    LoadPoDetail();
                }
            }
        }
        private void LoadPoDetail()
        {
            BindPO(PoNum, CompanyAccountNumber);
        }
        public void BindPO(string poNum, string companyAccountNumber)
        {
            SV.Framework.Fulfillment.PurchaseOrder purchaseOrder = SV.Framework.Fulfillment.PurchaseOrder.CreateInstance<SV.Framework.Fulfillment.PurchaseOrder>();

            PurchaseOrders purchaseOrders = default;
            purchaseOrders = purchaseOrder.GerPurchaseOrdersView(poNum, companyAccountNumber);
            if (purchaseOrders != null)
            {
                List<BasePurchaseOrder> poInfoList = purchaseOrders.PurchaseOrderList;

                //var poInfoList = (from item in purchaseOrderList where item.PurchaseOrderNumber.Equals(poNum) select item).ToList();
                if (poInfoList != null && poInfoList.Count > 0)
                {
                    pnlControl.Visible = true;
                    pnlMsg.Visible = false;

                    //}
                    //Classes.BasePurchaseOrder purchaseOrder = purchaseOrders.FindPurchaseOrder(poID);
                    //if (purchaseOrder != null)
                    //{
                    lblPO.Text = poInfoList[0].PurchaseOrderNumber;
                    lblPODate.Text = poInfoList[0].PurchaseOrderDate.ToShortDateString();
                    lblAddress.Text = poInfoList[0].Shipping.ShipToAddress;
                    lblAVSO.Text = poInfoList[0].AerovoiceSalesOrderNumber;
                    lblContactName.Text = poInfoList[0].Shipping.ContactName;
                    lblCustName.Text = poInfoList[0].CustomerName;
                    lblShipBy.Text = poInfoList[0].Tracking.ShipToBy;
                    if ("1/1/0001" != poInfoList[0].Tracking.ShipToDate.ToShortDateString())
                        lblShippDate.Text = poInfoList[0].Tracking.ShipToDate.ToShortDateString();
                    lblState.Text = poInfoList[0].Shipping.ShipToState;
                    lblStoreID.Text = poInfoList[0].StoreID;
                    lblTrackNo.Text = poInfoList[0].Tracking.ShipToTrackingNumber;
                    lblZip.Text = poInfoList[0].Shipping.ShipToZip;
                    lblStatus.Text = poInfoList[0].PurchaseOrderStatus.ToString();
                    lblComment.Text = poInfoList[0].Comments;
                    gvPODetail.DataSource = poInfoList[0].PurchaseOrderItems;
                    gvPODetail.DataBind();
                    lblPODCount.Text = "Total count: " + poInfoList[0].PurchaseOrderItems.Count;

                }
                else
                {
                    pnlControl.Visible = false;
                    pnlMsg.Visible = true;
                    lblMsg.Text = "No records founds";


                }

            }
            else
            {
                pnlControl.Visible = false;
                pnlMsg.Visible = true;
                lblMsg.Text = "No records founds";


            }


        }
        public void BindPO(int poID, bool poQuery)
        {
            SV.Framework.Fulfillment.PurchaseOrder purchaseOrder = SV.Framework.Fulfillment.PurchaseOrder.CreateInstance<SV.Framework.Fulfillment.PurchaseOrder>();

            PurchaseOrders purchaseOrders = default;
            if (poQuery)
                purchaseOrders = Session["POS"] as PurchaseOrders;
            else
                purchaseOrders = purchaseOrder.GerPurchaseOrders(null, null, null, null, 0, "0", 0, null, null, null, null, null, null, null, null, null, null, poID);
            
            List<BasePurchaseOrder> purchaseOrderList = purchaseOrders.PurchaseOrderList;

            var poInfoList = (from item in purchaseOrderList where item.PurchaseOrderID.Equals(poID) select item).ToList();
            if (poInfoList != null && poInfoList.Count > 0)
            {
                pnlControl.Visible = true;
                pnlMsg.Visible = false;

            //}
            //Classes.BasePurchaseOrder purchaseOrder = purchaseOrders.FindPurchaseOrder(poID);
            //if (purchaseOrder != null)
            //{
                lblPO.Text = poInfoList[0].PurchaseOrderNumber;
                lblPODate.Text = poInfoList[0].PurchaseOrderDate.ToShortDateString();
                lblAddress.Text = poInfoList[0].Shipping.ShipToAddress;
                lblAVSO.Text = poInfoList[0].AerovoiceSalesOrderNumber;
                lblContactName.Text = poInfoList[0].Shipping.ContactName;
                lblCustName.Text = poInfoList[0].CustomerName;
                lblShipBy.Text = poInfoList[0].Tracking.ShipToBy;
                if ("1/1/0001" != poInfoList[0].Tracking.ShipToDate.ToShortDateString())
                    lblShippDate.Text = poInfoList[0].Tracking.ShipToDate.ToShortDateString();
                lblState.Text = poInfoList[0].Shipping.ShipToState;
                lblStoreID.Text = poInfoList[0].StoreID;
                lblTrackNo.Text = poInfoList[0].Tracking.ShipToTrackingNumber;
                lblZip.Text = poInfoList[0].Shipping.ShipToZip;
                lblStatus.Text = poInfoList[0].PurchaseOrderStatus.ToString();
                lblComment.Text = poInfoList[0].Comments;
                gvPODetail.DataSource = poInfoList[0].PurchaseOrderItems;
                gvPODetail.DataBind();

            }
            else
            {
                pnlControl.Visible = false;
                pnlMsg.Visible = true;
                lblMsg.Text = "No records founds";


            }

            
                        
        
        }

    }
}   