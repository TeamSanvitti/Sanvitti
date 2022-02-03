using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using avii.Classes;
//using System.Net.Http;
using System.Threading.Tasks;
using System.Reflection;
using System.Configuration;
using System.Text;
using SV.Framework.Fulfillment;
using SV.Framework.Models.Fulfillment;

namespace avii.Tracking
{
    public partial class POTrackingToBeSent : System.Web.UI.Page
    {
        private PurchaseOrderSendTracking purchaseOrderSendTracking = PurchaseOrderSendTracking.CreateInstance<PurchaseOrderSendTracking>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["adm"] == null)
            {
                string url = "/avii/logon.aspx";
                try
                {
                    url = ConfigurationSettings.AppSettings["LogonPage"].ToString();

                }
                catch
                {
                    url = "/avii/logon.aspx";
                }
                Response.Redirect(url);
            }
            if (!IsPostBack)
            {
                string script = "$(document).ready(function () { $('[id*=btnSubmit]').click(); });";
                ClientScript.RegisterStartupScript(this.GetType(), "load", script, true);

                avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                int companyID = 0;
                if (Session["adm"] == null)
                {
                    trCustomer.Visible = true;
                    companyID = userInfo.CompanyGUID;
                    ViewState["CompanyID"] = companyID;
                }
                else
                {
                    trCustomer.Visible = true;
                    BindCustomers();
                }
                if(companyID > 0)
                    GetPOToBeSent(companyID);



                //{"Shipment":{"ShippingDate":"2019-10-16","ShippingMethod":"First-Class Letter","ShippingTrackingNumber":"0004000074527948640333813","LineItems":[{"LineNo":1,"ItemCode":"ZMP-USIM-DOTRIVDKT02","Quantity":1,"ICCID":"89011704278369207859"}]}}
                // string js = Newtonsoft.Json.JsonConvert.SerializeObject(po);
                //List<PurchaseOrderShipment> Shipments = new List<PurchaseOrderShipment>();
                //PurchaseOrderShipment Shipment = new PurchaseOrderShipment();
                ////   Shipment.PO_ID = 0;
                //Shipment.PurchaseOrderNumber = "2449536";
                //Shipment.ShippingDate = "2019-10-16";
                //Shipment.ShippingMethod = "First-Class Letter";
                //Shipment.ShippingTrackingNumber = "0004000074527948640333813";
                //LineItem item = new LineItem();
                //List<LineItem> items = new List<LineItem>();
                ////  item.BatchNumber = "Test1";
                ////  item.ESN = "89011704278369207859";
                //item.ICCID = "89011704278369207859";
                //item.ItemCode = "ZMP-USIM-DOTRIVDKT02";
                //item.LineNo = 1;
                //item.Quantity = 1;
                //items.Add(item);
                //Shipment.LineItems = items;
                //Shipments.Add(Shipment);
                //SetAcknowledgementAssign(Shipments);
                //{ "Shipment": { "PurchaseOrderNumber": "2449536", "ShippingDate": "2019-10-10", "ShippingMethod": "First-Class Letter", "ShippingTrackingNumber" : "0004000074527948640333813",
                //"LineItems" : [{ "LineNo" : 1, "ItemCode" : "ZMP-USIM-DOTRIVDKT02", "Quantity" : 1, "ICCID" : "89011704278369207859" } ] } }
            }
        }

        protected void BindCustomers()
        {
            dpCompany.DataSource = SV.Framework.Admin.CustomerOperations.GetCustomers(0, 0, 1);
            dpCompany.DataValueField = "CompanyID";
            dpCompany.DataTextField = "CompanyName";
            dpCompany.DataBind();

        }
        private void GetPOToBeSent(int companyID)
        {
            bool ShipmentSentOnly = chkShipment.Checked;
                
            string fulfillmentNumber = txtPONum.Text.Trim();
            string fromDate, toDate, TrackingNumber;
            TrackingNumber = txtTrackingNo.Text.Trim();
            fromDate = toDate = null;

            fromDate = txtFromDate.Text.Trim().Length > 0 ? txtFromDate.Text.Trim() : null;
            toDate = txtEndDate.Text.Trim().Length > 0 ? txtEndDate.Text.Trim() : null;
            string sortExpression = "PO_Date";
            string sortDirection = "DESC";
            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = sortExpression;
            btnCancel.Visible = false;
            btnSubmit.Visible = false;
            lblCount.Text = string.Empty;
            lblMsg.Text = string.Empty;
            List<PurchaseOrderShipmentDB> shipments = new List<PurchaseOrderShipmentDB>();

           // PurchaseOrderSendTracking po = new PurchaseOrderSendTracking();
            List<PurchaseOrderShipmentDB> shipmentDB = purchaseOrderSendTracking.GetPurchaseOrderToBeSent(companyID, fromDate, toDate, fulfillmentNumber, ShipmentSentOnly, TrackingNumber);
            if (shipmentDB != null && shipmentDB.Count > 0)
            {
                Session["shipmentDB"] = shipmentDB;
                PurchaseOrderShipmentDB shipment = new PurchaseOrderShipmentDB();
                var res = shipmentDB.GroupBy(e => new { e.PO_NUM, e.PO_ID, e.PO_Date, e.Ship_Via, e.TrackingNumber, e.ShipDate, e.PO_Status, e.CustomerOrderNumber, e.AcknowledgmentSent });

                foreach (var val in res)
                {
                    shipment = new PurchaseOrderShipmentDB()
                    {
                        PO_NUM = val.Key.PO_NUM,
                        Ship_Via = val.Key.Ship_Via,
                        PO_ID = val.Key.PO_ID,
                        PO_Date = val.Key.PO_Date,
                        ShipDate = (val.Key.ShipDate != null ? val.Key.ShipDate.ToString() : null),
                        TrackingNumber = val.Key.TrackingNumber,
                        PO_Status = val.Key.PO_Status,
                        CustomerOrderNumber = val.Key.CustomerOrderNumber,
                        AcknowledgmentSent = (val.Key.AcknowledgmentSent != null ? val.Key.AcknowledgmentSent.ToString() : null),

                    };

                    shipments.Add(shipment);
                }
                Session["shipments"] = shipments;
                gvPOTracking.DataSource = shipments;
                gvPOTracking.DataBind();
                //  btnCancel.Visible = true;
                btnSubmit.Visible = true;
                //btnRefresh.Visible = true;
                lblCount.Text = "<strong>Total Shipment:</strong> " + shipments.Count;
            }
            else
            {
                Session["shipments"] = null;
                gvPOTracking.DataSource = shipments;
                gvPOTracking.DataBind();

                lblMsg.Text = "No purchase order exists to process";
            }
        }
        //private void ReloadPOToBeSent()
        //{
        //    if (Session["shipmentDB"] != null)
        //    {
        //        List<PurchaseOrderShipmentDB> shipments = new List<PurchaseOrderShipmentDB>();

        //        PurchaseOrderSendTracking po = new PurchaseOrderSendTracking();
        //        List<PurchaseOrderShipmentDB> shipmentDB = Session["shipmentDB"] as List<PurchaseOrderShipmentDB>
        //    if (shipmentDB != null && shipmentDB.Count > 0)
        //        {
        //            Session["shipmentDB"] = shipmentDB;
        //            PurchaseOrderShipmentDB shipment = new PurchaseOrderShipmentDB();
        //            var res = shipmentDB.GroupBy(e => new { e.PO_NUM, e.PO_ID, e.PO_Date, e.Ship_Via, e.TrackingNumber, e.ShipDate, e.PO_Status });

        //            foreach (var val in res)
        //            {
        //                shipment = new PurchaseOrderShipmentDB()
        //                {
        //                    PO_NUM = val.Key.PO_NUM,
        //                    Ship_Via = val.Key.Ship_Via,
        //                    PO_ID = val.Key.PO_ID,
        //                    PO_Date = val.Key.PO_Date,
        //                    ShipDate = (val.Key.ShipDate != null ? val.Key.ShipDate.ToString() : null),
        //                    TrackingNumber = val.Key.TrackingNumber,
        //                    PO_Status = val.Key.PO_Status,
        //                };

        //                shipments.Add(shipment);
        //            }
        //            Session["shipments"] = shipments;
        //            gvPOTracking.DataSource = shipments;
        //            gvPOTracking.DataBind();
                   
        //        }
                
        //    }
        //}
        private const string API_URL = "http://partner.freedompop.com/langlobal/order/status";
        public  async  Task<string> SetAcknowledgementAssign(List<PurchaseOrderShipment> shipments, IntegrationModel model)
        {
            SV.Framework.Fulfillment.LogOperations logOperations = SV.Framework.Fulfillment.LogOperations.CreateInstance<SV.Framework.Fulfillment.LogOperations>();

            FulfillmentsTracking fulfillmentTracking = new FulfillmentsTracking();
            LogModel request = new LogModel();
            int userID = 0;
            if (Session["userInfo"] != null)
            {
                avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                if (userInfo != null)
                {
                    userID = userInfo.UserGUID;
                    // ViewState["companyID"] = companyID;
                }
            }
            request.ModuleName = "ShipmentTracking";
            request.RequestTimeStamp = DateTime.Now;;
            request.UserID = userID;
            int companyID = 0;
            int.TryParse(dpCompany.SelectedValue, out companyID);
            request.CompanyID = companyID;

            string errorMessage = string.Empty;
            foreach (PurchaseOrderShipment po in shipments)
            {
               // try
                {
                    fulfillmentTracking.Shipment = po;
                    
                    string js = Newtonsoft.Json.JsonConvert.SerializeObject(fulfillmentTracking);
                    request.RequestData = js;
                    var responseJson = await ApiHelper.PostAsync<ReturnAPI, FulfillmentsTracking>(fulfillmentTracking, model.APIAddress);

                    if (responseJson != null && responseJson.httpStatus.Equals("OK", StringComparison.InvariantCultureIgnoreCase))
                    {
                        // errorMessage = po.PurchaseOrderNumber + ": " + responseJson.httpStatus;
                        //       if (po.Shipment != null && po.Shipment.PO_ID > 0)
                        errorMessage = purchaseOrderSendTracking.AssignAcknowledgement(po.PO_ID, po.ShippingTrackingNumber);

                        errorMessage = (!string.IsNullOrWhiteSpace(errorMessage) ? errorMessage : responseJson.httpStatus);
                        request.ResponseData = Newtonsoft.Json.JsonConvert.SerializeObject(responseJson);
                        request.ResponseTimeStamp = DateTime.Now;
                        request.ExceptionOccured = false;
                        request.ReturnMessage = errorMessage;

                        logOperations.ApiLogInsert(request);
                    }
                    else if (responseJson != null)
                    {
                        errorMessage = (!string.IsNullOrWhiteSpace(responseJson.error) ? responseJson.error : responseJson.httpStatus);
                        request.ResponseData = Newtonsoft.Json.JsonConvert.SerializeObject(responseJson);
                        request.ResponseTimeStamp = DateTime.Now;
                        request.ExceptionOccured = true;
                        request.ReturnMessage = errorMessage;
                        logOperations.ApiLogInsert(request);

                    }
                    else
                    {
                        errorMessage = "No response is received!";
                        request.ResponseTimeStamp = DateTime.Now;
                        request.ExceptionOccured = true;

                        request.ResponseData = errorMessage;
                        logOperations.ApiLogInsert(request);
                    }
                }
                //catch (Exception ex)
                //{
                //    errorMessage = ex.Message;
                //    request.ResponseTimeStamp = DateTime.Now;
                //    request.ExceptionOccured = true;
                //    request.ResponseData = errorMessage;
                //    LogOperations.ApiLogInsert(request);
                //    // Console.WriteLine(ex.Message);
                //}
                //request.ReturnMessage = errorMessage;
                

            }
            //if (!string.IsNullOrWhiteSpace(errorMessage))
              //  lblMsg.Text = errorMessage;

            return errorMessage;
        }
        public async Task<string> SetAcknowledgementAssignNew(PurchaseOrderShipment po)
        {
            SV.Framework.Fulfillment.LogOperations logOperations = SV.Framework.Fulfillment.LogOperations.CreateInstance<SV.Framework.Fulfillment.LogOperations>();

            FulfillmentsTracking fulfillmentTracking = new FulfillmentsTracking();
            LogModel request = new LogModel();
            int userID = 0;
            if (Session["userInfo"] != null)
            {
                avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                if (userInfo != null)
                {
                    userID = userInfo.UserGUID;
                    // ViewState["companyID"] = companyID;
                }
            }
            request.UserID = userID;
            request.ModuleName = "ShipmentTracking";
            request.RequestTimeStamp = DateTime.Now;
            int companyID = 0;
            int.TryParse(dpCompany.SelectedValue, out companyID);
            request.CompanyID = companyID;

            string errorMessage = string.Empty;
            //Parallel.ForEach(shipments, po =>
            //foreach (PurchaseOrderShipment po in shipments)
            //{
                try
                {
                    fulfillmentTracking.Shipment = po;
                    string js = Newtonsoft.Json.JsonConvert.SerializeObject(fulfillmentTracking);
                    request.RequestData = js;
                    var responseJson = await ApiHelper.PostAsync<ReturnAPI, FulfillmentsTracking>(fulfillmentTracking, API_URL);
                    if (responseJson != null && responseJson.httpStatus.Equals("OK", StringComparison.InvariantCultureIgnoreCase))
                    {
                        // errorMessage = po.PurchaseOrderNumber + ": " + responseJson.httpStatus;
                        //       if (po.Shipment != null && po.Shipment.PO_ID > 0)
                        errorMessage = purchaseOrderSendTracking.AssignAcknowledgement(po.PO_ID, po.ShippingTrackingNumber);

                        errorMessage = (!string.IsNullOrWhiteSpace(errorMessage) ? errorMessage : responseJson.httpStatus);
                        request.ResponseData = Newtonsoft.Json.JsonConvert.SerializeObject(responseJson);
                        request.ResponseTimeStamp = DateTime.Now;
                        request.ExceptionOccured = false;
                        request.ReturnMessage = errorMessage;

                        logOperations.ApiLogInsert(request);
                    }
                    else if (responseJson != null)
                    {
                        errorMessage = (!string.IsNullOrWhiteSpace(responseJson.error) ? responseJson.error : responseJson.httpStatus);
                        request.ResponseData = Newtonsoft.Json.JsonConvert.SerializeObject(responseJson);
                        request.ResponseTimeStamp = DateTime.Now;
                        request.ExceptionOccured = true;
                        request.ReturnMessage = errorMessage;
                        logOperations.ApiLogInsert(request);

                    }
                    else
                    {
                        errorMessage = "No response is received!";
                        request.ResponseTimeStamp = DateTime.Now;
                        request.ExceptionOccured = true;

                        request.ResponseData = errorMessage;
                        logOperations.ApiLogInsert(request);
                    }
                }
                catch (Exception ex)
                {
                    errorMessage = ex.Message;
                    request.ResponseTimeStamp = DateTime.Now;
                    request.ExceptionOccured = true;
                    request.ResponseData = errorMessage;
                    logOperations.ApiLogInsert(request);
                    // Console.WriteLine(ex.Message);
                }
                //request.ReturnMessage = errorMessage;


            //});
            //if (!string.IsNullOrWhiteSpace(errorMessage))
            //  lblMsg.Text = errorMessage;

            return errorMessage;
        }

        public async void SetAcknowledgementNew()
        {
            lblMsg.Text = string.Empty;
            bool isSelected = false;
            int PO_ID = 0;

            string TrackingNumber = string.Empty, errorMessage = string.Empty;
            StringBuilder sb = new StringBuilder();
            List<PurchaseOrderShipment> shipments = new List<PurchaseOrderShipment>();
            PurchaseOrderShipment shipment = new PurchaseOrderShipment();
            foreach (GridViewRow row in gvPOTracking.Rows)
            {
                CheckBox chkPO = row.FindControl("chkPO") as CheckBox;
                HiddenField hdnPOID = row.FindControl("hdnPOID") as HiddenField;
                HiddenField hdnTrackingNumber = row.FindControl("hdnTrackingNumber") as HiddenField;
                if (chkPO.Checked)
                {
                    Label lblErrorMsg = row.FindControl("lblErrorMsg") as Label;
                    PO_ID = Convert.ToInt32(hdnPOID.Value);
                    TrackingNumber = hdnTrackingNumber.Value;
                    shipment = PopulateShipmentNew(PO_ID, TrackingNumber);
                    shipments.Add(shipment);

                    
                    isSelected = true;


                }

            }

            if (!isSelected)
                lblMsg.Text = "Purchaseorder not selected!";
            else
            {
                Parallel.ForEach(shipments, async po =>
                {
                    errorMessage = await SetAcknowledgementAssignNew(po);

                });
                //if (lblErrorMsg != null)
                //{
                //    lblErrorMsg.Text = errorMessage.ToLower() == "ok" ? "Successfull" : errorMessage;
                //}
                if (!string.IsNullOrWhiteSpace(errorMessage))
                    sb.Append(" </br>" + errorMessage);

                avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                int companyID = 0;
                if (Session["adm"] == null)
                {
                    trCustomer.Visible = true;
                    companyID = userInfo.CompanyGUID;
                    ViewState["CompanyID"] = companyID;
                }
                else
                {
                    companyID = Convert.ToInt32(dpCompany.SelectedValue);
                }
                GetPOToBeSent(companyID);
                if (string.IsNullOrWhiteSpace(errorMessage))
                    lblMsg.Text = "Submitted successfully!";
                // else
                //    lblMsg.Text = sb.ToString();
            }

        }

        private PurchaseOrderShipment PopulateShipmentNew(int po_id, string trackingNumber)
        {
            List<PurchaseOrderShipmentDB> shipmentDB = Session["shipmentDB"] as List<PurchaseOrderShipmentDB>;
            var shipmentList = (from item in shipmentDB where item.TrackingNumber.Equals(trackingNumber) select item).ToList();

            //List<PurchaseOrderShipment> shipments = new List<PurchaseOrderShipment>();
            PurchaseOrderShipment shipment = new PurchaseOrderShipment();
            LineItem lineItem = null;
            List<LineItem> lineItems = null;
            var res = shipmentList.GroupBy(e => new { e.CustomerOrderNumber, e.PO_ID, e.PO_Date, e.Ship_Via, e.TrackingNumber, e.ContainerID, e.ShipDate });

            foreach (var val in res)
            {
                shipment = new PurchaseOrderShipment()
                {
                    PO_ID = val.Key.PO_ID,
                    PurchaseOrderNumber = val.Key.CustomerOrderNumber,
                    ShippingMethod = val.Key.Ship_Via,
                    ShippingTrackingNumber = val.Key.TrackingNumber,
                    ShippingDate = (val.Key.ShipDate != null ? val.Key.ShipDate.ToString() : null),
                };
                lineItems = new List<LineItem>();
                foreach (PurchaseOrderShipmentDB e in val)
                {
                    lineItem = new LineItem();
                    // lineItem.BatchNumber = e.BatchNumber;
                    lineItem.ESN = e.ESN;
                    lineItem.ICCID = e.ICC_ID;
                    lineItem.ItemCode = e.SKU;
                    lineItem.LineNo = (int)e.Line_no;
                    lineItem.Quantity = e.Qty;
                    lineItem.ContainerID = e.ContainerID;

                    lineItems.Add(lineItem);
                }

                shipment.LineItems = lineItems;
               // shipments.Add(shipment);
            }


            return shipment;
        }

        public async void SetAcknowledgement(IntegrationModel model)
        {
            lblMsg.Text = string.Empty;
            bool isSelected = false;
            int PO_ID = 0;

            string TrackingNumber = string.Empty, errorMessage = string.Empty;
            StringBuilder sb = new StringBuilder();
            foreach (GridViewRow row in gvPOTracking.Rows)
            {
                CheckBox chkPO = row.FindControl("chkPO") as CheckBox;
                HiddenField hdnPOID = row.FindControl("hdnPOID") as HiddenField;
                HiddenField hdnTrackingNumber = row.FindControl("hdnTrackingNumber") as HiddenField;
                if (chkPO.Checked)
                {
                    Label lblErrorMsg = row.FindControl("lblErrorMsg") as Label;
                    PO_ID = Convert.ToInt32(hdnPOID.Value);
                    TrackingNumber = hdnTrackingNumber.Value;
                    List<PurchaseOrderShipment> shipments = PopulateShipment(PO_ID, TrackingNumber);
                    errorMessage = await SetAcknowledgementAssign(shipments, model);
                    if(lblErrorMsg!=null)
                    {
                        lblErrorMsg.Text = errorMessage.ToLower() == "ok" ? "Successfull": errorMessage;
                    }
                    if (!string.IsNullOrWhiteSpace(errorMessage))
                        sb.Append(" </br>" + errorMessage);
                    isSelected = true;


                }

            }

            if (!isSelected)
                lblMsg.Text = "Purchaseorder not selected!";
            else
            {
                avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                int companyID = 0;
                if (Session["adm"] == null)
                {
                    trCustomer.Visible = true;
                    companyID = userInfo.CompanyGUID;
                    ViewState["CompanyID"] = companyID;
                }
                else
                {
                    companyID = Convert.ToInt32(dpCompany.SelectedValue);
                }
                GetPOToBeSent(companyID);

                if (string.IsNullOrWhiteSpace(errorMessage))
                    lblMsg.Text = "Submitted successfully!";
               // else
                //    lblMsg.Text = sb.ToString();
            }

        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string shipmentTracking = System.Configuration.ConfigurationManager.AppSettings["ShipmentTracking"].ToString();
            avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
            int companyID = 0;
            if (Session["adm"] == null)
            {
                //trCustomer.Visible = true;
                companyID = userInfo.CompanyGUID;
                ViewState["CompanyID"] = companyID;
            }
            else
            {
                if (dpCompany.SelectedIndex == 0)
                {
                    lblMsg.Text = "Customer is required!";
                    
                    return;
                }
                companyID = Convert.ToInt32(dpCompany.SelectedValue);
            }
            IntegrationModel model = purchaseOrderSendTracking.GetShipmentTrackingAPIInfo(companyID, shipmentTracking);

            SetAcknowledgement(model);
            //SetAcknowledgementNew();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "stop loader", "StopProgress();", true);
            ///   GetPOToBeSent();
        }
        protected void gvPOTracking_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPOTracking.PageIndex = e.NewPageIndex;


            if (Session["shipments"] != null)
            {
                List<PurchaseOrderShipmentDB> poList = (List<PurchaseOrderShipmentDB>)Session["shipments"];

                gvPOTracking.DataSource = poList;
                gvPOTracking.DataBind();
            }
            else
            {
                avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                int companyID = 0;
                if (Session["adm"] == null)
                {
                    trCustomer.Visible = true;
                    companyID = userInfo.CompanyGUID;
                    ViewState["CompanyID"] = companyID;
                }
                else
                {
                    companyID = Convert.ToInt32(dpCompany.SelectedValue);
                }
                GetPOToBeSent(companyID);
                //BindPO();
            }

        }
        protected void gvPOTracking_RowDataBound(object sender, GridViewRowEventArgs e)
       {

          //header select all function
           if (e.Row.RowType == DataControlRowType.Header)
           {
               ((CheckBox) e.Row.FindControl("allchk")).Attributes.Add("onclick",
                   "javascript:SelectAll('" +
                   ((CheckBox) e.Row.FindControl("allchk")).ClientID + "')");
           }

}
        private List<PurchaseOrderShipment> PopulateShipment(int po_id, string trackingNumber)
        {
            List<PurchaseOrderShipmentDB> shipmentDB = Session["shipmentDB"] as List<PurchaseOrderShipmentDB>;
            var shipmentList =  (from item in shipmentDB where item.TrackingNumber.Equals(trackingNumber) select item).ToList();

            List<PurchaseOrderShipment> shipments = new List<PurchaseOrderShipment>();
            PurchaseOrderShipment shipment = new PurchaseOrderShipment();
            LineItem lineItem = null;
            List<LineItem> lineItems = null;
            var res = shipmentList.GroupBy(e => new { e.CustomerOrderNumber, e.PO_ID, e.PO_Date, e.Ship_Via, e.TrackingNumber,e.ContainerID, e.ShipDate });

            foreach (var val in res)
            {
                shipment = new PurchaseOrderShipment()
                {
                    PO_ID = val.Key.PO_ID,
                    PurchaseOrderNumber = val.Key.CustomerOrderNumber,
                    ShippingMethod = val.Key.Ship_Via,
                    ShippingTrackingNumber = val.Key.TrackingNumber,
                    ShippingDate = (val.Key.ShipDate != null ? val.Key.ShipDate.ToString() : null),
                };
                lineItems = new List<LineItem>();
                foreach (PurchaseOrderShipmentDB e in val)
                {
                    lineItem = new LineItem();
                   // lineItem.BatchNumber = e.BatchNumber;
                    lineItem.ESN = e.ESN;
                    lineItem.ICCID = e.ICC_ID;
                    lineItem.ItemCode = e.SKU;
                    lineItem.LineNo = (int)e.Line_no;
                    lineItem.Quantity = e.Qty;
                    lineItem.ContainerID = e.ContainerID;

                    lineItems.Add(lineItem);
                }

                shipment.LineItems = lineItems;
                shipments.Add(shipment);
            }


            return shipments;
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtTrackingNo.Text = string.Empty;
            txtPONum.Text = string.Empty;
            txtEndDate.Text = string.Empty;
            txtFromDate.Text = string.Empty;
            chkShipment.Checked = false;
            btnSubmit.Visible = false;
            btnCancel.Visible = false;
            lblCount.Text = string.Empty;
            lblMsg.Text = string.Empty;
            gvPOTracking.DataSource = null;
            gvPOTracking.DataBind();
            if (Session["adm"] != null)
                dpCompany.SelectedIndex = 0;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "stop loader", "StopProgress();", true);
            //GetPOToBeSent();
        }
        private string GetSortDirection(string column)
        {
            string sortDirection = "ASC";
            string sortExpression = ViewState["SortExpression"] as string;
            if (sortExpression != null)
            {
                if (sortExpression == column)
                {
                    string lastDirection = ViewState["SortDirection"] as string;
                    if ((lastDirection != null) && (lastDirection == "ASC"))
                    {
                        sortDirection = "DESC";
                    }
                }
            }
            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = column;
            return sortDirection;
        }
        public List<PurchaseOrderShipmentDB> Sort<TKey>(List<PurchaseOrderShipmentDB> list, string sortBy, SortDirection direction)
        {
            PropertyInfo property = list.GetType().GetGenericArguments()[0].GetProperty(sortBy);
            if (direction == SortDirection.Ascending)
            {
                return list.OrderBy(e => property.GetValue(e, null)).ToList<PurchaseOrderShipmentDB>();
            }
            else
            {
                return list.OrderByDescending(e => property.GetValue(e, null)).ToList<PurchaseOrderShipmentDB>();
            }
        }
        protected void gvPOTracking_Sorting(object sender, GridViewSortEventArgs e)
        {
            string Sortdir = GetSortDirection(e.SortExpression);
            string SortExp = e.SortExpression;

            if (Session["shipments"] != null)
            {
                List<PurchaseOrderShipmentDB> shipments = (List<PurchaseOrderShipmentDB>)Session["shipments"];

                if (shipments != null && shipments.Count > 0)
                {
                    //var list = shipments;
                    if (Sortdir == "ASC")
                    {
                        shipments = Sort<PurchaseOrderShipmentDB>(shipments, SortExp, SortDirection.Ascending);
                    }
                    else
                    {
                        shipments = Sort<PurchaseOrderShipmentDB>(shipments, SortExp, SortDirection.Descending);
                    }
                    Session["shipments"] = shipments;
                    gvPOTracking.DataSource = shipments;
                    gvPOTracking.DataBind();
                }
            }
        }

        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
            int companyID = 0;
            if (Session["adm"] == null)
            {
                trCustomer.Visible = true;
                companyID = userInfo.CompanyGUID;
                ViewState["CompanyID"] = companyID;
            }
            else
            {
                companyID = Convert.ToInt32(dpCompany.SelectedValue);
            }
            GetPOToBeSent(companyID);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
            int companyID = 0;
            if (Session["adm"] == null)
            {
                trCustomer.Visible = true;
                companyID = userInfo.CompanyGUID;
                ViewState["CompanyID"] = companyID;
            }
            else
            {
                if (dpCompany.SelectedIndex == 0)
                {
                    lblMsg.Text = "Customer is required!";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "stop loader", "StopProgress();", true);
                    return;
                }
                companyID = Convert.ToInt32(dpCompany.SelectedValue);
            }
            GetPOToBeSent(companyID);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "stop loader", "StopProgress();", true);
        }

    }
}