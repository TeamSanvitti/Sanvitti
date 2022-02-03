using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using avii.Classes;

namespace avii.Controls 
{
    public partial class POList : System.Web.UI.UserControl
    {

        public delegate void RowCommand(object sender, GridViewCommandEventArgs e);
        public event RowCommand GridRowCommand;
    

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
            //if (Session["missingtype"] != null && Session["missingrec"] != null)
            //{
               
            //    avii.Classes.PurchaseOrders pos = Session["missingrec"] as avii.Classes.PurchaseOrders;
            //    if (pos != null && pos.PurchaseOrderList.Count > 0)
            //    {
            //        grdPO.DataSource = pos.PurchaseOrderList;
            //        grdPO.DataBind();

            //    }
            //}
        }

        protected void OnRowCommand(GridViewCommandEventArgs e)
        {
            if (GridRowCommand != null)
                GridRowCommand(this, e);       
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            OnRowCommand(e);

        }



        private List<avii.Classes.BasePurchaseOrderItem> ChildDataSourcebyPODID(int podID, string strSort)
        {
            avii.Classes.PurchaseOrders purchaseOrders = (avii.Classes.PurchaseOrders)Session["POS"];
            if (purchaseOrders != null && purchaseOrders.PurchaseOrderList.Count > 0)
            {
                Classes.BasePurchaseOrder purchaseOrder = purchaseOrders.FindPurchaseOrderbyPodID(podID);

                return purchaseOrder.PurchaseOrderItems;
            }
            else
            {
                return null;
            }
        }


        public void LoadControl(int CompanyID, string PurchaseOrderNumber, avii.Classes.MissingList missingList)
        {
            avii.Classes.PurchaseOrders pos = null;
            if (MissingList.MSL == missingList)
            {
                pos = avii.Classes.PurchaseOrder.GetPurchaseOrder_Missing_MSL(PurchaseOrderNumber, CompanyID);
                Session["missingtype"] = missingList;
                Session["missingrec"] = pos;
            }
            else if (MissingList.ESN == missingList)
            {
                pos = avii.Classes.PurchaseOrder.GetPurchaseOrder_Missing_ESN(PurchaseOrderNumber, CompanyID);
                Session["missingtype"] = missingList;
                Session["missingrec"] = pos;
            }
            else if (MissingList.ESN_MSL == missingList)
            {
                pos = avii.Classes.PurchaseOrder.GetPurchaseOrder_Missing_ESN_MSL(PurchaseOrderNumber, CompanyID);
                Session["missingtype"] = missingList;
                Session["missingrec"] = pos;
            }
            else if (MissingList.ASN == missingList)
            {
                pos = avii.Classes.PurchaseOrder.GetPurchaseOrder_Missing_ASN(PurchaseOrderNumber, CompanyID);
                Session["missingtype"] = missingList;
                Session["missingrec"] = pos;
            }
            else if (MissingList.ESN_MSL_ASN == missingList)
            {
                pos = avii.Classes.PurchaseOrder.GetPurchaseOrder_Missing_ESN_MSL_ASN(PurchaseOrderNumber, CompanyID);
                Session["missingtype"] = missingList;
                Session["missingrec"] = pos;
            }
            else if (MissingList.MSL_ASN == missingList)
            {
                pos = avii.Classes.PurchaseOrder.GetPurchaseOrder_Missing_MSL_ASN(PurchaseOrderNumber, CompanyID);
                Session["missingtype"] = missingList;
                Session["missingrec"] = pos;
            }          
            if (pos != null && pos.PurchaseOrderList.Count > 0)
            {
                lbCount.Text = "Total Records: " + pos.PurchaseOrderList.Count.ToString();
                grdPO.DataSource = pos.PurchaseOrderList;
                grdPO.DataBind();
                
            }
        }
    }
}