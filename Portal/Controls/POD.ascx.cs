using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SV.Framework.Models.Fulfillment;

namespace avii.Controls
{

    public partial class POD : System.Web.UI.UserControl
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
                if (!string.IsNullOrEmpty(PoNum))
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
            //avii.Classes.clsPurchaseOrder purchaseOrders = null;
            if (Session["pos"] != null || Session["polist"] != null)
            {
                List<clsPurchaseOrder> purchaseOrders = null;
                if (Session["pos"] != null)
                    purchaseOrders = Session["pos"] as List<clsPurchaseOrder>;
                if (purchaseOrders == null)
                    purchaseOrders = Session["polist"] as List<clsPurchaseOrder>;


                if (purchaseOrders != null)
                {
                    //List<avii.Classes.BasePurchaseOrder> poInfoList = purchaseOrders.PurchaseOrderList;
                    var poList = (from item in purchaseOrders where item.PurchaseOrderNumber.Equals(poNum) select item).ToList();

                    //var poInfoList = (from item in purchaseOrderList where item.PurchaseOrderNumber.Equals(poNum) select item).ToList();
                    if (poList != null && poList.Count > 0)
                    {
                        pnlControl.Visible = true;
                        pnlMsg.Visible = false;
                        List<PurchaseOrderItem> purchaseOrderItemList = poList[0].PurchaseOrderItems;
                        gvPODetail.DataSource = purchaseOrderItemList;
                        gvPODetail.DataBind();
                        lblPODCount.Text = "Total count: " + purchaseOrderItemList.Count;

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
        }
        
    }
}