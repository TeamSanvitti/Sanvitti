using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SV.Framework.Models.Fulfillment;
using System.Linq;

namespace avii.Controls
{
    public partial class PoSum : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PurchaseOrders pos = new PurchaseOrders();
                pos = Session["POS"] as  PurchaseOrders;
                if (pos != null && pos.PurchaseOrderList.Count > 0)
                {
                    GenerateSummary(pos);
                }
            }
        }


        private void GenerateSummary(PurchaseOrders pos)
        {
            SortedList hshItems = new SortedList(); 
            //Hashtable hshItems = new Hashtable();
            int countItems = 0;
            int pendingCount, shippedCount, processedCount, closeCount, cancelledCount, returnCount, onHoldCount, inProcessCount, OutOfStockCount;
            pendingCount = shippedCount = processedCount = closeCount = cancelledCount = returnCount = onHoldCount = inProcessCount = OutOfStockCount = 0;
            string poNum, fromDate, toDate, contactName, storeID, statusID, esn, mslNumber, phoneCategory, itemCode, shipFrom, shipTo, trackingNumber, customerOrderNumber, POType, sku;
            poNum = fromDate = toDate = shipFrom = shipTo = trackingNumber = null;
            int userID = 0, companyID = 0;
            List<FulfillmentStatus> statusList = new List<FulfillmentStatus>();
            List<BasePurchaseOrderItem> itemlist = null;
            //Session["posearchcriteria"] = poNum + "~" + contactName + "~" + fromDate + "~" + toDate + "~" + userID + "~" + statusID + "~" + companyID 
            //+ "~" + esn + "~" + avOrder + "~" + mslNumber + "~" + phoneCategory + "~" + itemCode + "~" + storeID + "~" + fmUPC + "~" + zoneGUID + "~" + shipFrom + "~" + shipTo;
            //poNum + "~" +  fromDate + "~" + toDate + "~" + userID + "~" + statusID + "~" + companyID + "~" + esn + "~" +  mslNumber + "~" +  storeID + "~" + shipFrom + "~" + shipTo + "~" +
            //trackingNumber + "~"+ customerOrderNumber + "~" + contactName + "~" + POType + "~" + sku;
            if (Session["posearchcriteria"] != null)
            {
                string searchString = (string)Session["posearchcriteria"];
                string[] searchArr = searchString.Split('~');
                poNum = searchArr[0].Length > 0 ? searchArr[0] : null;
                //contactName = searchArr[1].Length > 0 ? searchArr[1] : null;
                fromDate = searchArr[1].Length > 0 ? searchArr[1] : null;
                toDate = searchArr[2].Length > 0 ? searchArr[2] : null;
                userID = Convert.ToInt32(searchArr[3]);
                statusID = searchArr[4].Length > 0 ? searchArr[4] : "0";
                companyID = Convert.ToInt32(searchArr[5]);
                esn = searchArr[6].Length > 0 ? searchArr[6] : null;
               // avOrder = searchArr[8].Length > 0 ? searchArr[8] : null;
                mslNumber = searchArr[7].Length > 0 ? searchArr[7] : null;
                //phoneCategory = searchArr[10].Length > 0 ? searchArr[10] : null;
                //itemCode = searchArr[11].Length > 0 ? searchArr[11] : null;
                storeID = searchArr[8].Length > 0 ? searchArr[8] : null;
                //fmUPC = searchArr[13].Length > 0 ? searchArr[13] : null;
                //zoneGUID = searchArr[14].Length > 0 ? searchArr[14] : null;
                shipFrom = searchArr[9].Length > 0 ? searchArr[9] : null;
                shipTo = searchArr[10].Length > 0 ? searchArr[10] : null;
                trackingNumber = searchArr[11].Length > 0 ? searchArr[11] : null;
                customerOrderNumber = searchArr[12].Length > 0 ? searchArr[12] : null;
                contactName = searchArr[13].Length > 0 ? searchArr[13] : null;
                POType = searchArr[14].Length > 0 ? searchArr[14] : null;

                itemCode = searchArr[15].Length > 0 ? searchArr[15] : null;
                SV.Framework.Fulfillment.PurchaseOrder purchaseOrderOperation = SV.Framework.Fulfillment.PurchaseOrder.CreateInstance<SV.Framework.Fulfillment.PurchaseOrder>();
                //   +"~" + customerOrderNumber + "~" + contactName + "~" + POType + "~" + sku;

                itemlist = purchaseOrderOperation.GetPurchaseOrderInventorySummary(poNum, contactName, fromDate, toDate, userID, statusID, companyID, esn, trackingNumber, mslNumber, null, itemCode, storeID, POType, customerOrderNumber, shipFrom, shipTo, 0, out statusList);
                if (itemlist != null && itemlist.Count > 0)
                {
                    int totalNumberOfUnits = itemlist.Sum((item) => item.LineNo);
                    lblUnits.Text = totalNumberOfUnits.ToString();
                }
            }

            if (pos.PurchaseOrderList != null && pos.PurchaseOrderList.Count > 0)
            {
                DateTime poDateTo, poDateFrom;
                poDateTo = new DateTime();
                poDateFrom = new DateTime();
                poDateFrom = pos.PurchaseOrderList[pos.PurchaseOrderList.Count - 1].PurchaseOrderDate;
                poDateTo = pos.PurchaseOrderList[0].PurchaseOrderDate;

                if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
                    lblPoSummaryDate.Text = "Fulfillment Summary between Fulfillment Date <b>" + fromDate + "</b> and <b>" + toDate + "</b>";
                else
                {
                    if (!string.IsNullOrEmpty(fromDate) && string.IsNullOrEmpty(toDate))
                    {

                        lblPoSummaryDate.Text = "Fulfillment Summary between Fulfillment Date <b>" + fromDate + "</b> and <b>" + DateTime.Now.ToShortDateString() + "</b>";
                    }
                    else
                        lblPoSummaryDate.Text = "Fulfillment Summary between Fulfillment Date <b>" + poDateFrom.ToShortDateString() + "</b> and <b>" + poDateTo.ToShortDateString() + "</b>";

                }

            }


            //foreach (avii.Classes.BasePurchaseOrder po in pos.PurchaseOrderList)
            //{
                
            //    if (po.PurchaseOrderStatus == PurchaseOrderStatus.Pending)
            //    {
            //        pendingCount++;
            //    }
            //    else if (po.PurchaseOrderStatus == PurchaseOrderStatus.Shipped)
            //    {
            //        shippedCount++;
            //    }
            //    else if (po.PurchaseOrderStatus == PurchaseOrderStatus.Processed)
            //    {
            //        processedCount++;
            //    }
            //    else if (po.PurchaseOrderStatus == PurchaseOrderStatus.Closed)
            //    {
            //        closeCount++;
            //    }
            //    else if (po.PurchaseOrderStatus == PurchaseOrderStatus.Cancelled)
            //    {
            //        cancelledCount++;
            //    }
            //    else if (po.PurchaseOrderStatus == PurchaseOrderStatus.Return)
            //    {
            //        returnCount++;
            //    }
            //    else if (po.PurchaseOrderStatus == PurchaseOrderStatus.OnHold)
            //    {
            //        onHoldCount++;
            //    }
            //    else if (po.PurchaseOrderStatus == PurchaseOrderStatus.OutofStock)
            //    {
            //        OutOfStockCount++;
            //    }
            //    else if (po.PurchaseOrderStatus == PurchaseOrderStatus.InProcess)
            //    {
            //        inProcessCount++;
            //    }






            //    //if ((po.PurchaseOrderStatus == PurchaseOrderStatus.Pending) || (po.PurchaseOrderStatus == PurchaseOrderStatus.Shipped)
            //    //    || (po.PurchaseOrderStatus == PurchaseOrderStatus.Processed) || (po.PurchaseOrderStatus == PurchaseOrderStatus.Closed))
            //    //{

                    
            //    //}
            //}



            

            //FulfillmentStatus statusObj = null;

            //if (pendingCount > 0)
            //{
            //    statusObj = new FulfillmentStatus();
            //    statusObj.FulfillmentOrderStatus = PurchaseOrderStatus.Pending;
            //    statusObj.StatusCount = pendingCount;
            //    statusList.Add(statusObj);
            //}
            //if (processedCount > 0)
            //{
            //    statusObj = new FulfillmentStatus();
            //    statusObj.FulfillmentOrderStatus = PurchaseOrderStatus.Processed;
            //    statusObj.StatusCount = processedCount;
            //    statusList.Add(statusObj);
            //}
            //if (shippedCount > 0)
            //{
            //    statusObj = new FulfillmentStatus();
            //    statusObj.FulfillmentOrderStatus = PurchaseOrderStatus.Shipped;
            //    statusObj.StatusCount = shippedCount;
            //    statusList.Add(statusObj);
            //} if (inProcessCount > 0)
            //{
            //    statusObj = new FulfillmentStatus();
            //    statusObj.FulfillmentOrderStatus = PurchaseOrderStatus.InProcess;
            //    statusObj.StatusCount = inProcessCount;
            //    statusList.Add(statusObj);
            //} if (closeCount > 0)
            //{
            //    statusObj = new FulfillmentStatus();
            //    statusObj.FulfillmentOrderStatus = PurchaseOrderStatus.Closed;
            //    statusObj.StatusCount = closeCount;
            //    statusList.Add(statusObj);
            //} if (returnCount > 0)
            //{
            //    statusObj = new FulfillmentStatus();
            //    statusObj.FulfillmentOrderStatus = PurchaseOrderStatus.Return;
            //    statusObj.StatusCount = returnCount;
            //    statusList.Add(statusObj);
            //} if (onHoldCount > 0)
            //{
            //    statusObj = new FulfillmentStatus();
            //    statusObj.FulfillmentOrderStatus = PurchaseOrderStatus.OnHold;
            //    statusObj.StatusCount = onHoldCount;
            //    statusList.Add(statusObj);
            //} if (OutOfStockCount > 0)
            //{
            //    statusObj = new FulfillmentStatus();
            //    statusObj.FulfillmentOrderStatus = PurchaseOrderStatus.OutofStock;
            //    statusObj.StatusCount = OutOfStockCount;
            //    statusList.Add(statusObj);
            //} if (cancelledCount > 0)
            //{
            //    statusObj = new FulfillmentStatus();
            //    statusObj.FulfillmentOrderStatus = PurchaseOrderStatus.Cancelled;
            //    statusObj.StatusCount = cancelledCount;
            //    statusList.Add(statusObj);
            //}


            rptPOStatus.DataSource = statusList;
            rptPOStatus.DataBind();



            if (itemlist != null && itemlist.Count > 0)
            {
                rptSummary.DataSource = itemlist;
                rptSummary.DataBind();
                //*********** Commented on 7/2/2014
                //foreach (avii.Classes.BasePurchaseOrderItem pitems in itemlist)
                //{

                //    if (!hshItems.ContainsKey(pitems.ItemCode))
                //    {
                //        hshItems.Add(pitems.ItemCode, pitems.LineNo);
                //    }
                //    else
                //    {
                //        countItems = Convert.ToInt32(hshItems[pitems.ItemCode].ToString());
                //        countItems = countItems + pitems.LineNo;
                //        hshItems[pitems.ItemCode] = countItems;
                //    }
                //}

                //HtmlTable tbl = new HtmlTable();
                //HtmlTableRow tRow = new HtmlTableRow();
                //HtmlTableCell tCell;
                //foreach (string key in hshItems.Keys)
                //{
                //    tRow = new HtmlTableRow();

                //    tCell = new HtmlTableCell();
                //    tCell.Attributes.Add("class", "copy10grey");
                //    tCell.InnerText = key;
                //    tRow.Cells.Add(tCell);

                //    tCell = new HtmlTableCell();
                //    tCell.Attributes.Add("class", "copy10grey");
                //    tCell.InnerText = hshItems[key].ToString();
                //    tRow.Cells.Add(tCell);

                //    tbl.Rows.Add(tRow);
                //}

                //pnlItemSummary.Controls.Add(tbl);
                //// * ///
            }
            else
            {
                rptSummary.DataSource = null;
                rptSummary.DataBind();

            }

            //foreach (avii.Classes.BasePurchaseOrderItem pitems in itemlist)
            //{
            //    tRow = new HtmlTableRow();

            //    tCell = new HtmlTableCell();
            //    tCell.Attributes.Add("class", "copy10grey");
            //    tCell.InnerText = pitems.ItemCode;
            //    tRow.Cells.Add(tCell);

            //    tCell = new HtmlTableCell();
            //    tCell.Attributes.Add("class", "copy10grey");
            //    tCell.InnerText = pitems.LineNo.ToString();
            //    tRow.Cells.Add(tCell);

            //    tbl.Rows.Add(tRow);
            //}

            

            lblTotalPOs.Text = pos.PurchaseOrderList.Count.ToString();
            //lblPendingPOs.Text = pendingCount.ToString();
            //lblShippedPOs.Text = shippedCount.ToString();
            //lblProcessedPOs.Text = processedCount.ToString();
            //lblClosedPO.Text = closeCount.ToString();

        }

    }
}