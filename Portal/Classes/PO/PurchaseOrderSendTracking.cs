using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using avii.Classes;
namespace avii.Classes
{
    
    public class PurchaseOrderSendTracking
    {
        private const int COMPANY_ID = 464;
        private const string API_URL = "http://partner.t.freedompop.com/langlobal/order/status";
        public List<PurchaseOrderShipmentDB> GetPurchaseOrderToBeSent(int companyID, string fromDate, string toDate, string poNum, bool ShipmentSentOnly, string TrackingNumber)
        {
            //List<FulfillmentTracking> shipments = null;
            List<PurchaseOrderShipmentDB> shipmentDB = null;
            System.Text.StringBuilder sbError = new System.Text.StringBuilder();
            // List<PurchaseOrderShipmentDB> shipments = null;
            int recordCount = 0, errornumber = 0;
           // string errorMessage = string.Empty;

            string errorMessage = null;
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataTable dt = new DataTable();
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@piStatusID", 3);
                objCompHash.Add("@piCompanyID", companyID);
                objCompHash.Add("@piPO_Num", poNum);
                objCompHash.Add("@piFromDate", string.IsNullOrWhiteSpace(fromDate) ? null : fromDate);
                objCompHash.Add("@piToDate", string.IsNullOrWhiteSpace(toDate) ? null : toDate);
                objCompHash.Add("@ShipmentSentOnly", ShipmentSentOnly);
                objCompHash.Add("@TrackingNumber", TrackingNumber);
                

                arrSpFieldSeq = new string[] { "@piStatusID", "@piCompanyID", "@piPO_Num", "@piFromDate", "@piToDate", "@ShipmentSentOnly", "@TrackingNumber" };
                dt = db.GetTableRecords(objCompHash, "av_PurchaseOrder_TrackingToBeSent", arrSpFieldSeq, "@poRowCount", "@poErrorNumber", "@poErrorMessage",out recordCount, out errornumber, out errorMessage);

                shipmentDB = PopulateShipmentDB(dt);
                //HttpContext.Current.Session["shipmentDB"] = shipmentDB;
               // shipments = PopulateShipmentdb(shipmentDB);

            }


            catch (Exception ex)
            {
                if (string.IsNullOrWhiteSpace(errorMessage))
                    errorMessage = ex.ToString();
                else
                    errorMessage = string.Concat(errorMessage, ex.ToString());
                sbError.Append(errorMessage);
            }
            finally
            {
                db = null;
                if (sbError.Length > 0)
                    errorMessage = string.Concat(errorMessage, sbError.ToString());
            }


            return shipmentDB;
        }

        

        public PurchaseOrderShippingResponse GetPurchaseOrderShipmentToBeSent(PurchaseOrderShippingRequest serviceRequest)
        {
            string requestXML = clsGeneral.SerializeObject(serviceRequest);

            LogModel request = new LogModel();
            request.RequestData = requestXML;
            request.ModuleName = "GetPurchaseOrderShipmentToBeSent";
            request.RequestTimeStamp = DateTime.Now;
            Exception ex = null;

            PurchaseOrderShippingResponse serviceResponse = new PurchaseOrderShippingResponse();
            try
            {
                CredentialValidation credentialValidation = AuthenticationOperation.AuthenticateRequest(serviceRequest.UserCredentials, out ex);

                if (ex != null)
                {
                    serviceResponse.Comment = ex.Message;
                    serviceResponse.ReturnCode = ResponseErrorCode.ErrowWhileLoadingData.ToString();

                    request.ResponseData = clsGeneral.SerializeObject(serviceResponse);
                    request.ResponseTimeStamp = DateTime.Now;
                    request.ExceptionOccured = true;
                    request.ReturnMessage = ResponseErrorCode.ErrowWhileLoadingData.ToString();
                    
                }
                else
                {

                    if (credentialValidation != null && credentialValidation.CompanyID > 0)
                    {
                        request.UserID = credentialValidation.UserID;
                        request.CompanyID = credentialValidation.CompanyID;
                        List<PurchaseOrderShipmentDB> shipmentdb = GetPurchaseOrderToBeSent(credentialValidation.CompanyID, "", "", "", false, "");
                        List<PurchaseOrderShipment> shipments = new List<PurchaseOrderShipment>();

                        if (shipmentdb != null && shipmentdb.Count > 0)
                        {
                            shipments = PopulateShipment(shipmentdb);

                            serviceResponse.Comment = "Successfully Retrieved";
                            serviceResponse.ReturnCode = ResponseErrorCode.SuccessfullyRetrieved.ToString();
                            serviceResponse.Shipments = shipments;


                        }
                        else
                        {

                            serviceResponse.Comment = "No records found";
                            serviceResponse.ReturnCode = ResponseErrorCode.NoRecordsFound.ToString();
                            serviceResponse.Shipments = null;
                        }

                    }
                    else
                    {
                        serviceResponse.Comment = "Cannot Authenticate User";
                        serviceResponse.ReturnCode = ResponseErrorCode.CannotAuthenticateUser.ToString();
                        serviceResponse.Shipments = null;
                    }
                    request.ResponseData = clsGeneral.SerializeObject(serviceResponse);
                    request.ResponseTimeStamp = DateTime.Now;
                    request.ExceptionOccured = false;
                    request.ReturnMessage = serviceResponse.Comment;
                   // LogOperations.ApiLogInsert(request);
                }
            }
            catch (Exception exc)
            {
                ex = exc;
                //  serviceResponse.Comment = exc.Message;
                //serviceResponse.ReturnCode = ResponseErrorCode.ErrowWhileLoadingData.ToString();
                serviceResponse.Comment = ex.Message;
                serviceResponse.ReturnCode = ResponseErrorCode.ErrowWhileLoadingData.ToString();

                request.ResponseData = clsGeneral.SerializeObject(serviceResponse);
                request.ResponseTimeStamp = DateTime.Now;
                request.ExceptionOccured = true;
                request.ReturnMessage = ResponseErrorCode.ErrowWhileLoadingData.ToString();
               // LogOperations.ApiLogInsert(request);
            }
            finally
            {
                LogOperations.ApiLogInsert(request);
            }
            return serviceResponse;
        }

        public SetPurchaseOrderShippingResponse SetPurchaseOrderShipment(SetPurchaseOrderShippingRequest serviceRequest)
        {
            string errorMessage = string.Empty;
            SetPurchaseOrderShippingResponse serviceResponse = new SetPurchaseOrderShippingResponse();
            string requestXML = clsGeneral.SerializeObject(serviceRequest);

            LogModel request = new LogModel();
            request.RequestData = requestXML;
            request.ModuleName = "SetPurchaseOrderShipment";
            request.RequestTimeStamp = DateTime.Now;
            Exception ex = null;

            try
            {
                CredentialValidation credentialValidation = AuthenticationOperation.AuthenticateRequest(serviceRequest.UserCredentials, out ex);
                if (ex != null)
                {
                    serviceResponse.Comment = ex.Message;
                    serviceResponse.ReturnCode = ResponseErrorCode.ErrowWhileLoadingData.ToString();

                    request.ResponseData = clsGeneral.SerializeObject(serviceResponse);
                    request.ResponseTimeStamp = DateTime.Now;
                    request.ExceptionOccured = true;
                    request.ReturnMessage = ResponseErrorCode.ErrowWhileLoadingData.ToString();
                   
                }
                else
                {
                    if (credentialValidation != null && credentialValidation.CompanyID > 0)
                    {
                        request.UserID = credentialValidation.UserID;
                        request.CompanyID = credentialValidation.CompanyID;

                        if (serviceRequest.Shipments != null && serviceRequest.Shipments.Count > 0)
                        {
                            SetAcknowledgementAssign(serviceRequest.Shipments);

                            serviceResponse.Comment = "Updated successfully";
                            serviceResponse.ReturnCode = ResponseErrorCode.UpdatedSuccessfully.ToString();
                            // serviceResponse.Shipments = shipments;


                        }
                        else
                        {

                            serviceResponse.Comment = "No data updated";
                            serviceResponse.ReturnCode = ResponseErrorCode.DataNotUpdated.ToString();
                            //serviceResponse.Shipments = null;
                        }

                    }
                    else
                    {
                        serviceResponse.Comment = "Cannot Authenticate User";
                        serviceResponse.ReturnCode = ResponseErrorCode.CannotAuthenticateUser.ToString();
                        // serviceResponse.Shipments = null;
                    }
                    request.ResponseData = clsGeneral.SerializeObject(serviceResponse);
                    request.ResponseTimeStamp = DateTime.Now;
                    request.ExceptionOccured = false;
                    request.ReturnMessage = serviceResponse.Comment;
                   // LogOperations.ApiLogInsert(request);
                }
            }
            catch (Exception exc)
            {
                ex = exc;
                //  serviceResponse.Comment = exc.Message;
                //serviceResponse.ReturnCode = ResponseErrorCode.ErrowWhileLoadingData.ToString();
                serviceResponse.Comment = ex.Message;
                serviceResponse.ReturnCode = ResponseErrorCode.ErrowWhileLoadingData.ToString();

                request.ResponseData = clsGeneral.SerializeObject(serviceResponse);
                request.ResponseTimeStamp = DateTime.Now;
                request.ExceptionOccured = true;
                request.ReturnMessage = ResponseErrorCode.ErrowWhileLoadingData.ToString();
              //  LogOperations.ApiLogInsert(request);
            }
            finally
            {
                LogOperations.ApiLogInsert(request);
            }
            return serviceResponse;
        }



        private List<PurchaseOrderShipment> PopulateShipment(List<PurchaseOrderShipmentDB> shipment_DB)
        {
            int po_id = 0;
            string trackingNumber = string.Empty;
            var shipmentDB = shipment_DB.GroupBy(e => new { e.PO_ID, e.TrackingNumber }).ToList();
            List<PurchaseOrderShipment> shipments = new List<PurchaseOrderShipment>();
            //List<PurchaseOrderShipmentDB> shipmentDB = Session["shipmentDB"] as List<PurchaseOrderShipmentDB>;
            foreach(var row in shipmentDB)
            {
                po_id = row.Key.PO_ID;
                trackingNumber = row.Key.TrackingNumber;

                var shipmentList = (from item in shipment_DB where item.TrackingNumber.Equals(trackingNumber) select item).ToList();

                
                PurchaseOrderShipment shipment = new PurchaseOrderShipment();
                LineItem lineItem = null;
                List<LineItem> lineItems = null;
                var res = shipmentList.GroupBy(e => new { e.PO_NUM, e.PO_ID, e.PO_Date, e.Ship_Via, e.TrackingNumber, e.ShipDate });

                foreach (var val in res)
                {
                    shipment = new PurchaseOrderShipment()
                    {
                        PO_ID = val.Key.PO_ID,
                        PurchaseOrderNumber = val.Key.PO_NUM,
                        ShippingMethod = val.Key.Ship_Via,
                        ShippingTrackingNumber = val.Key.TrackingNumber,
                        ShippingDate = (val.Key.ShipDate != null ? val.Key.ShipDate.ToString() : null),
                    };
                    lineItems = new List<LineItem>();
                    foreach (PurchaseOrderShipmentDB e in val)
                    {
                        lineItem = new LineItem();
                        // lineItem.BatchNumber = e.BatchNumber;
                        // lineItem.ESN = e.ESN;
                        lineItem.ICCID = e.ICC_ID;
                        lineItem.ItemCode = e.SKU;
                        lineItem.LineNo = (int)e.Line_no;
                        lineItem.Quantity = e.Qty;


                        lineItems.Add(lineItem);
                    }

                    shipment.LineItems = lineItems;
                    shipments.Add(shipment);
                }
            }

            return shipments;
        }


        //private const string API_URL = "http://partner.t.freedompop.com/langlobal/order/status";
        public async void SetAcknowledgementAssign(List<PurchaseOrderShipment> shipments)
        {
            FulfillmentsTracking fulfillmentTracking = new FulfillmentsTracking();

            string errorMessage = string.Empty;
            foreach (PurchaseOrderShipment po in shipments)
            {
                try
                {
                    fulfillmentTracking.Shipment = po;
                    string js = Newtonsoft.Json.JsonConvert.SerializeObject(fulfillmentTracking);
                    var responseJson = await ApiHelper.PostAsync<ReturnAPI, FulfillmentsTracking>(fulfillmentTracking, API_URL);
                    if (responseJson != null && responseJson.httpStatus.Equals("OK", StringComparison.InvariantCultureIgnoreCase))
                    {
                        //       if (po.Shipment != null && po.Shipment.PO_ID > 0)
                        errorMessage = PurchaseOrderSendTracking.AssignAcknowledgement(po.PO_ID, po.ShippingTrackingNumber);

                    }
                    else
                        errorMessage = responseJson.error;


                }
                catch (Exception ex)
                {
                    errorMessage = ex.Message;
                    // Console.WriteLine(ex.Message);
                }
            }
            //if (!string.IsNullOrWhiteSpace(errorMessage))
              //  lblMsg.Text = errorMessage;

            // return errorMessage;
        }
        public static string AssignAcknowledgement(int po_id, string trackingNumber)
        {
            string errorMessage = string.Empty;
            int recordCount = 0, errornumber = 0;
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            string sCode = string.Empty;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@po_id", po_id);
                objCompHash.Add("@TrackingNumber", trackingNumber);
                
                arrSpFieldSeq = new string[] { "@po_id", "@TrackingNumber" };
                db.ExeCommand(objCompHash, "av_purchaseorder_AckSent_Insert", arrSpFieldSeq, "@poRowCount", "@poErrorNumber", "@poErrorMessage", out recordCount, out errornumber, out errorMessage);
                if (recordCount > 0)
                {
                    errorMessage = "";// ResponseErrorCode.UpdatedSuccessfully.ToString();
                    
                }
                //else if (!string.IsNullOrEmpty(errorMessage))
                //{
                //   // errorMessage = ResponseErrorCode.PurchaseOrderNotExists.ToString();
                    
                //}
                else
                {
                    errorMessage = ResponseErrorCode.DataNotUpdated.ToString();
                    
                }
            }
            catch (Exception objExp)
            {
                
                errorMessage = objExp.Message.ToString();
            }
            finally
            {
                objCompHash = null;
                arrSpFieldSeq = null;
            }
            
            return errorMessage;
        }
        public static IntegrationModel GetShipmentTrackingAPIInfo(int companyID, string shipmentTracking)
        {
            IntegrationModel model = new IntegrationModel();
            string errorMessage = string.Empty;
         //   int recordCount = 0, errornumber = 0;
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            //string sCode = string.Empty;
            Hashtable objCompHash = new Hashtable();
            DataTable dt = new DataTable();
            try
            {
                objCompHash.Add("@CompanyID", companyID);
                objCompHash.Add("@ShipmentTracking", shipmentTracking);

                arrSpFieldSeq = new string[] { "@CompanyID", "@ShipmentTracking" };
                dt = db.GetTableRecords(objCompHash, "av_Company_APIAddressInfo", arrSpFieldSeq);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach(DataRow dataRow in dt.Rows)
                    {
                        model.APIAddress = clsGeneral.getColumnData(dataRow, "APIAddress", string.Empty, false) as string;
                        model.UserName = clsGeneral.getColumnData(dataRow, "UserName", string.Empty, false) as string;
                        model.Password = clsGeneral.getColumnData(dataRow, "Password", string.Empty, false) as string;
                        model.APIName = clsGeneral.getColumnData(dataRow, "IntegrationName", string.Empty, false) as string;

                    }
                    errorMessage = "";// ResponseErrorCode.UpdatedSuccessfully.ToString();

                }                
                else
                {
                    errorMessage = ResponseErrorCode.DataNotUpdated.ToString();

                }
            }
            catch (Exception objExp)
            {

                errorMessage = objExp.Message.ToString();
            }
            finally
            {
                objCompHash = null;
                arrSpFieldSeq = null;
            }

            return model;
        }
        private List<PurchaseOrderShipmentDB> PopulateShipmentDB(DataTable dt)
        {
            List<PurchaseOrderShipmentDB> shipments = null;// new List<PurchaseOrderShipmentDB>();
            PurchaseOrderShipmentDB shipment = null;
            if (dt != null && dt.Rows.Count > 0)
            {
                shipments = new List<PurchaseOrderShipmentDB>();
                foreach (DataRow dataRow in dt.Rows)
                {
                    shipment = new PurchaseOrderShipmentDB();
                    shipment.BatchNumber = clsGeneral.getColumnData(dataRow, "BatchNumber", string.Empty, false) as string;
                    shipment.ESN = clsGeneral.getColumnData(dataRow, "ESN", string.Empty, false) as string;
                    shipment.ICC_ID = clsGeneral.getColumnData(dataRow, "ICC_ID", string.Empty, false) as string;
                    shipment.Line_no = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Line_no", 0, false));
                    shipment.PO_Date = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "PO_Date", string.Empty, false)).ToShortDateString();
                    shipment.PO_ID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Po_ID", 0, false));
                    shipment.Qty = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Qty", 0, false));
                    shipment.PO_NUM = clsGeneral.getColumnData(dataRow, "PO_NUM", string.Empty, false) as string;
                    shipment.CustomerOrderNumber = clsGeneral.getColumnData(dataRow, "CustomerOrderNumber", string.Empty, false) as string;

                    shipment.ShipDate =Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "ShipDate", string.Empty, false)).ToString("yyyy-MM-dd");
                    shipment.Ship_Via = clsGeneral.getColumnData(dataRow, "Ship_Via", string.Empty, false) as string;
                    shipment.SKU = clsGeneral.getColumnData(dataRow, "item_code", string.Empty, false) as string;
                    shipment.TrackingNumber = clsGeneral.getColumnData(dataRow, "TrackingNumber", string.Empty, false) as string;
                    shipment.ContainerID = clsGeneral.getColumnData(dataRow, "ContainerID", string.Empty, false) as string;
                    shipment.AcknowledgmentSent = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "TrackingSentDateTime", DateTime.MinValue, false)).ToString("yyyy-MM-dd"); //clsGeneral.getColumnData(dataRow, "AcknowledgmentSent", string.Empty, false) as string;

                    //  shipment.ShipmentType = clsGeneral.getColumnData(dataRow, "ShipmentType", string.Empty, false) as string;
                    shipment.PO_Status = clsGeneral.getColumnData(dataRow, "PO_Status", string.Empty, false) as string;
                    shipments.Add(shipment);
                }
            }

            return shipments;
        }

    }
    public class PurchaseOrderShippingRequest
    {
        private UserCredentials userCredentials;
        public PurchaseOrderShippingRequest()
        {
            userCredentials = new UserCredentials();
        }

        public UserCredentials UserCredentials
        {
            get
            {
                return userCredentials;
            }
            set
            {
                userCredentials = value;
            }
        }


    }
    public class PurchaseOrderShippingResponse
    {
        private List<avii.Classes.PurchaseOrderShipment> shipments;
        private string returnCode;
        private string comment;
        public PurchaseOrderShippingResponse()
        {
            shipments = new List<PurchaseOrderShipment>();
        }
        public string ReturnCode
        {
            get
            {
                return returnCode;
            }
            set
            {
                returnCode = value;
            }
        }

        public string Comment
        {
            get
            {
                return comment;
            }
            set
            {
                comment = value;
            }
        }
        public List<PurchaseOrderShipment> Shipments
        {
            get
            {
                return shipments;
            }
            set
            {
                shipments = value;
            }
        }
    }

    public class SetPurchaseOrderShippingRequest
    {
        private List<avii.Classes.PurchaseOrderShipment> shipments;

        private UserCredentials userCredentials;
        public SetPurchaseOrderShippingRequest()
        {
            userCredentials = new UserCredentials();
            shipments = new List<PurchaseOrderShipment>();
        }

        public UserCredentials UserCredentials
        {
            get
            {
                return userCredentials;
            }
            set
            {
                userCredentials = value;
            }
        }
        public List<PurchaseOrderShipment> Shipments
        {
            get
            {
                return shipments;
            }
            set
            {
                shipments = value;
            }
        }

    }
    public class SetPurchaseOrderShippingResponse
    {
        private string returnCode;
        private string comment;
        
        public string ReturnCode
        {
            get
            {
                return returnCode;
            }
            set
            {
                returnCode = value;
            }
        }

        public string Comment
        {
            get
            {
                return comment;
            }
            set
            {
                comment = value;
            }
        }
        
    }

}