using System.Collections.Generic;
using System.Data;
using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using SV.Framework.Models.Fulfillment;
using SV.Framework.Models.Common;

namespace SV.Framework.Fulfillment
{
    public class FulfillmentOperations : BaseCreateInstance
    {         
        public  List<FulfillmentComment> GetFulfillmentComments(int poID)
        {
            SV.Framework.DAL.Fulfillment.FulfillmentOperations fulfillmentOperations = SV.Framework.DAL.Fulfillment.FulfillmentOperations.CreateInstance<DAL.Fulfillment.FulfillmentOperations>();
            List<FulfillmentComment> commentsList = fulfillmentOperations.GetFulfillmentComments(poID);
           
            return commentsList;
        }
        public  FulfillmentEsNInfo GetPurchaseOrderESNs(int POID)
        {
            SV.Framework.DAL.Fulfillment.FulfillmentOperations fulfillmentOperations = SV.Framework.DAL.Fulfillment.FulfillmentOperations.CreateInstance<DAL.Fulfillment.FulfillmentOperations>();
            FulfillmentEsNInfo fulfillmentEsNInfo = fulfillmentOperations.GetPurchaseOrderESNs(POID);// new FulfillmentEsNInfo();
            
            return fulfillmentEsNInfo;
        }

        public  string UEDF_File_Update(int po_id, string fileName, DateTime currentCSTDateTime, DateTime currentUtcDateTime)
        {
            SV.Framework.DAL.Fulfillment.FulfillmentOperations fulfillmentOperations = SV.Framework.DAL.Fulfillment.FulfillmentOperations.CreateInstance<DAL.Fulfillment.FulfillmentOperations>();
            string errorMessage = fulfillmentOperations.UEDF_File_Update(po_id, fileName, currentCSTDateTime, currentUtcDateTime);
            
            return errorMessage;
        }

        public  List<FulfillmentDetailModel> GetPurchaseOrderTxtFile(int POID, out string ShipmentDate)
        {
            ShipmentDate = "";
            SV.Framework.DAL.Fulfillment.FulfillmentOperations fulfillmentOperations = SV.Framework.DAL.Fulfillment.FulfillmentOperations.CreateInstance<DAL.Fulfillment.FulfillmentOperations>();
            List<FulfillmentDetailModel> poDetails = fulfillmentOperations.GetPurchaseOrderTxtFile(POID, out ShipmentDate);
           
            return poDetails;
        }

        public string FulfillmentAddLineItems(List<FulfillmentItem> fulfillmentItems, int companyID, int POID, int userID, out int recordCount, out int returnCount)
        {
            string errorMessage = "";
            recordCount = 0;
            returnCount = 0;
            SV.Framework.DAL.Fulfillment.FulfillmentOperations fulfillmentOperations = SV.Framework.DAL.Fulfillment.FulfillmentOperations.CreateInstance<DAL.Fulfillment.FulfillmentOperations>();
            errorMessage = fulfillmentOperations.FulfillmentAddLineItems(fulfillmentItems, companyID, POID, userID, out recordCount, out returnCount);
            
            return errorMessage;
        }


        private async void SetParallelShipmentNotification()
        {
            SV.Framework.DAL.Fulfillment.FulfillmentOperations fulfillmentOperations = SV.Framework.DAL.Fulfillment.FulfillmentOperations.CreateInstance<DAL.Fulfillment.FulfillmentOperations>();
            List<PurchaseOrderShipmentDB> shipments = default;// new List<PurchaseOrderShipmentDB>();
            List<PurchaseOrderShipmentDB> shipmentDB = fulfillmentOperations.GetPurchaseOrderToBeSent(0, "", "", "", false, "");
            if(shipmentDB != null && shipmentDB.Count > 0)
            {
                shipments = new List<PurchaseOrderShipmentDB>();
                PurchaseOrderShipmentDB shipment = default;// new PurchaseOrderShipmentDB();
                var res = shipmentDB.GroupBy(e => new {e.PO_NUM, e.PO_ID, e.PO_Date, e.Ship_Via, e.TrackingNumber, e.ShipDate, e.PO_Status, e.CustomerOrderNumber, e.AcknowledgmentSent, e.APIAddress });
                foreach (var val in res)
                {
                    shipment = new PurchaseOrderShipmentDB()
                    {
                        //UserID = val.Key.UserID,
                        PO_NUM = val.Key.PO_NUM,
                        Ship_Via = val.Key.Ship_Via,
                        PO_ID = val.Key.PO_ID,
                        PO_Date = val.Key.PO_Date,
                        ShipDate = (val.Key.ShipDate != null ? val.Key.ShipDate.ToString() : null),
                        TrackingNumber = val.Key.TrackingNumber,
                        PO_Status = val.Key.PO_Status,
                        CustomerOrderNumber = val.Key.CustomerOrderNumber,
                        APIAddress = val.Key.APIAddress,
                        AcknowledgmentSent = (val.Key.AcknowledgmentSent != null ? val.Key.AcknowledgmentSent.ToString() : null),
                    };

                    shipments.Add(shipment);
                }
                string errorMessage = string.Empty;
                int userID = 0;
                List<PurchaseOrderShipment> poShipments = default;// new List<PurchaseOrderShipment>();
                if (shipments != null && shipments.Count > 0)
                {
                    poShipments = new List<PurchaseOrderShipment>();
                    foreach (PurchaseOrderShipmentDB item in shipments)
                    {
                        PurchaseOrderShipment poShipment = PopulateShipmentNew(shipmentDB, item.TrackingNumber, out userID);
                        poShipment.API_URL = item.APIAddress;
                        poShipment.UserID = userID;
                        poShipments.Add(poShipment);
                        //errorMessage = await SetAcknowledgementAssignNew(poShipment, item.APIAddress, userID);
                    }
                    if(poShipments != null && poShipments.Count > 0)
                    {
                        Parallel.ForEach(poShipments, async po =>
                        {
                            errorMessage = await SetAcknowledgementAssignNew(po);

                        });
                    }
                }
            }
        }
        private async void SetShipmentNotification()
        {
            SV.Framework.DAL.Fulfillment.FulfillmentOperations fulfillmentOperations = SV.Framework.DAL.Fulfillment.FulfillmentOperations.CreateInstance<DAL.Fulfillment.FulfillmentOperations>();
            List<PurchaseOrderShipmentDB> shipments = default;// new List<PurchaseOrderShipmentDB>();
            List<PurchaseOrderShipmentDB> shipmentDB = fulfillmentOperations.GetPurchaseOrderToBeSent(0, "", "", "", false, "");
            if (shipmentDB != null && shipmentDB.Count > 0)
            {
                shipments = new List<PurchaseOrderShipmentDB>();
                PurchaseOrderShipmentDB shipment = default;// new PurchaseOrderShipmentDB();
                var res = shipmentDB.GroupBy(e => new { e.PO_NUM, e.PO_ID, e.PO_Date, e.Ship_Via, e.TrackingNumber, e.ShipDate, e.PO_Status, e.CustomerOrderNumber, e.AcknowledgmentSent, e.APIAddress });
                foreach (var val in res)
                {
                    shipment = new PurchaseOrderShipmentDB()
                    {
                        //UserID = val.Key.UserID,
                        PO_NUM = val.Key.PO_NUM,
                        Ship_Via = val.Key.Ship_Via,
                        PO_ID = val.Key.PO_ID,
                        PO_Date = val.Key.PO_Date,
                        ShipDate = (val.Key.ShipDate != null ? val.Key.ShipDate.ToString() : null),
                        TrackingNumber = val.Key.TrackingNumber,
                        PO_Status = val.Key.PO_Status,
                        CustomerOrderNumber = val.Key.CustomerOrderNumber,
                        APIAddress = val.Key.APIAddress,
                        AcknowledgmentSent = (val.Key.AcknowledgmentSent != null ? val.Key.AcknowledgmentSent.ToString() : null),
                    };

                    shipments.Add(shipment);
                }
                string errorMessage = string.Empty;
                int userID = 0;
                if (shipments != null && shipments.Count > 0)
                {
                    foreach (PurchaseOrderShipmentDB item in shipments)
                    {
                        PurchaseOrderShipment poShipment = PopulateShipmentNew(shipmentDB, item.TrackingNumber, out userID);
                        errorMessage = await SetAcknowledgementAssignNew(poShipment, item.APIAddress, userID);
                    }
                }
            }
        }

        public void SetShipmentNotifications()
        {
            SetShipmentNotification();
            //SetParallelShipmentNotification();
        }              

        private async Task<string> SetAcknowledgementAssignNew(PurchaseOrderShipment po)
        {
            SV.Framework.DAL.Fulfillment.LogOperations LogOperations = SV.Framework.DAL.Fulfillment.LogOperations.CreateInstance<DAL.Fulfillment.LogOperations>();
            SV.Framework.DAL.Fulfillment.FulfillmentOperations fulfillmentOperations = SV.Framework.DAL.Fulfillment.FulfillmentOperations.CreateInstance<DAL.Fulfillment.FulfillmentOperations>();
            FulfillmentsTracking fulfillmentTracking = new FulfillmentsTracking();
            LogModel request = new LogModel();

            request.ModuleName = "ShipmentTracking";
            request.RequestTimeStamp = DateTime.Now;
            request.UserID = po.UserID;
            string errorMessage = string.Empty;
            //Parallel.ForEach(shipments, po =>
            //foreach (PurchaseOrderShipment po in shipments)
            //{
            try
            {
                fulfillmentTracking.Shipment = po;
                string js = Newtonsoft.Json.JsonConvert.SerializeObject(fulfillmentTracking);
                request.RequestData = js;
                var responseJson = await ApiHelper.PostAsync<ReturnAPI, FulfillmentsTracking>(fulfillmentTracking, po.API_URL);
                if (responseJson != null && responseJson.httpStatus.Equals("OK", StringComparison.InvariantCultureIgnoreCase))
                {
                    // errorMessage = po.PurchaseOrderNumber + ": " + responseJson.httpStatus;
                    //       if (po.Shipment != null && po.Shipment.PO_ID > 0)
                    errorMessage = fulfillmentOperations.AssignAcknowledgement(po.PO_ID, po.ShippingTrackingNumber);

                    errorMessage = (!string.IsNullOrWhiteSpace(errorMessage) ? errorMessage : responseJson.httpStatus);
                    request.ResponseData = Newtonsoft.Json.JsonConvert.SerializeObject(responseJson);
                    request.ResponseTimeStamp = DateTime.Now;
                    request.ExceptionOccured = false;
                    request.ReturnMessage = errorMessage;

                    LogOperations.ApiLogInsert(request);
                }
                else if (responseJson != null)
                {
                    errorMessage = (!string.IsNullOrWhiteSpace(responseJson.error) ? responseJson.error : responseJson.httpStatus);
                    request.ResponseData = Newtonsoft.Json.JsonConvert.SerializeObject(responseJson);
                    request.ResponseTimeStamp = DateTime.Now;
                    request.ExceptionOccured = true;
                    request.ReturnMessage = errorMessage;
                    LogOperations.ApiLogInsert(request);
                }
                else
                {
                    errorMessage = "No response is received!";
                    request.ResponseTimeStamp = DateTime.Now;
                    request.ExceptionOccured = true;

                    request.ResponseData = errorMessage;
                    LogOperations.ApiLogInsert(request);
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                request.ResponseTimeStamp = DateTime.Now;
                request.ExceptionOccured = true;
                request.ResponseData = errorMessage;
                LogOperations.ApiLogInsert(request);
            }
            return errorMessage;
        }

        private async Task<string> SetAcknowledgementAssignNew(PurchaseOrderShipment po, string API_URL, int userID)
        {
            SV.Framework.DAL.Fulfillment.LogOperations LogOperations = SV.Framework.DAL.Fulfillment.LogOperations.CreateInstance<DAL.Fulfillment.LogOperations>();
            SV.Framework.DAL.Fulfillment.FulfillmentOperations fulfillmentOperations = SV.Framework.DAL.Fulfillment.FulfillmentOperations.CreateInstance<DAL.Fulfillment.FulfillmentOperations>();

            FulfillmentsTracking fulfillmentTracking = new FulfillmentsTracking();
            LogModel request = new LogModel();

            request.ModuleName = "ShipmentTracking";
            request.RequestTimeStamp = DateTime.Now;
            request.UserID = userID;
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
                    errorMessage = fulfillmentOperations.AssignAcknowledgement(po.PO_ID, po.ShippingTrackingNumber);

                    errorMessage = (!string.IsNullOrWhiteSpace(errorMessage) ? errorMessage : responseJson.httpStatus);
                    request.ResponseData = Newtonsoft.Json.JsonConvert.SerializeObject(responseJson);
                    request.ResponseTimeStamp = DateTime.Now;
                    request.ExceptionOccured = false;
                    request.ReturnMessage = errorMessage;

                    LogOperations.ApiLogInsert(request);
                }
                else if (responseJson != null)
                {
                    errorMessage = (!string.IsNullOrWhiteSpace(responseJson.error) ? responseJson.error : responseJson.httpStatus);
                    request.ResponseData = Newtonsoft.Json.JsonConvert.SerializeObject(responseJson);
                    request.ResponseTimeStamp = DateTime.Now;
                    request.ExceptionOccured = true;
                    request.ReturnMessage = errorMessage;
                    LogOperations.ApiLogInsert(request);
                }
                else
                {
                    errorMessage = "No response is received!";
                    request.ResponseTimeStamp = DateTime.Now;
                    request.ExceptionOccured = true;
                    request.ResponseData = errorMessage;
                    LogOperations.ApiLogInsert(request);
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                request.ResponseTimeStamp = DateTime.Now;
                request.ExceptionOccured = true;
                request.ResponseData = errorMessage;
                LogOperations.ApiLogInsert(request);                
            }
            return errorMessage;
        }        

        private PurchaseOrderShipment PopulateShipmentNew(List<PurchaseOrderShipmentDB> shipmentDB, string trackingNumber, out int userID)
        {
            userID = 0;
            //List<PurchaseOrderShipmentDB> shipmentDB = Session["shipmentDB"] as List<PurchaseOrderShipmentDB>;
            var shipmentList = (from item in shipmentDB where item.TrackingNumber.Equals(trackingNumber) select item).ToList();

            //List<PurchaseOrderShipment> shipments = new List<PurchaseOrderShipment>();
            PurchaseOrderShipment shipment = new PurchaseOrderShipment();
            LineItem lineItem = null;
            List<LineItem> lineItems = null;
            var res = shipmentList.GroupBy(e => new { e.CustomerOrderNumber, e.PO_ID, e.PO_Date, e.Ship_Via, e.TrackingNumber, e.ContainerID, e.ShipDate, e.APIAddress });

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
                    userID = e.UserID;
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
        
    }
}
