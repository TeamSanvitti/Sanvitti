using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace avii.Classes
{

    //public class PurchaseOrderShipmentLineItems
    //{
    //    public string PurchaseOrderNumber { get; set; }

    //    public List<string> ItemCodes { get; set; }

    //    public string ShippingTrackingNumber { get; set; }

    //}

    public class PurchaseOrderShipmentResponse
    {
        private List<avii.Classes.PurchaseOrderShipment> shipments;
        private string returnCode;
        private string comment;
        public PurchaseOrderShipmentResponse()
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
    public class PurchaseOrderShipmentAPIResponse
    {
        private List<avii.Classes.PurchaseOrderShipping> shipments;
        private string returnCode;
        private string comment;
        public PurchaseOrderShipmentAPIResponse()
        {
            shipments = new List<PurchaseOrderShipping>();
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
        public List<PurchaseOrderShipping> Shipments
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

    public class PurchaseOrderShipmentRequest
    {
        private UserCredentials userCredentials;
        private DateTime dateFrom;
        private DateTime dateTo;
        private string _purchaseOrderNumber;

        public PurchaseOrderShipmentRequest()
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

        public string PurchaseOrderNumber
        {
            get
            {
                return _purchaseOrderNumber;
            }
            set
            {
                _purchaseOrderNumber = value;
            }
        }
        public DateTime DateFrom
        {
            get
            {
                return dateFrom;
            }
            set
            {
                dateFrom = value;
            }
        }
        public DateTime DateTo
        {
            get
            {
                return dateTo;
            }
            set
            {
                dateTo = value;
            }
        }

    }
    public class LineItem
    {
        public int LineNo { get; set; }
        public string ItemCode { get; set; }
        public int Quantity { get; set; }
        //[JsonIgnore]
        public string ESN { get; set; }
        public string ICCID { get; set; }
        [JsonIgnore]
        public string BatchNumber { get; set; }
        
        public string ContainerID { get; set; }

        

    }
    public class ReturnAPI
    {
        public string error { get; set; }
        public string httpStatus { get; set; }

    }
    public class FulfillmentsTracking
    {
        public PurchaseOrderShipment Shipment { get; set; }

    }

    public class PurchaseOrderShipment
    {
        [JsonIgnore]
        public int PO_ID { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public string ShippingDate { get; set; }
        public string ShippingMethod { get; set; }
        public string ShippingTrackingNumber { get; set; }
        public List<LineItem> LineItems { get; set; }
    }
    public class PurchaseOrderShipping
    {
        
        public string PurchaseOrderNumber { get; set; }
        public string PODate { get; set; }
        public string ShippingMethod { get; set; }
        
        public List<PurchaseOrderTracking> Trackings { get; set; }
    }
    public class PurchaseOrderTracking
    {
        public string ShipDate { get; set; }
        public string TrackingNumber { get; set; }
        public string AcknowledgmentSent { get; set; }
        public string ShipmentType { get; set; }
        public List<LineItems> LineItems { get; set; }

    }
    public class LineItems
    {
        //public int Line_no { get; set; }
        public int Qty { get; set; }
        public string SKU { get; set; }
        public string ESN { get; set; }
        public string ICCID { get; set; }
        public string BatchNumber { get; set; }
        

    }
    public class PurchaseOrderShipmentDB
    {
        public string CustomerOrderNumber { get; set; }

        public int PO_ID { get; set; }
        public string PO_NUM { get; set; }
        public string PO_Date { get; set; }
        public string PO_Status { get; set; }
        public string Ship_Via { get; set; }
        public int Line_no { get; set; }
        public int Qty { get; set; }
        public string SKU { get; set; }
        public string ShipDate { get; set; }
        public string ESN { get; set; }
        public string ICC_ID { get; set; }
        public string BatchNumber { get; set; }
        public string TrackingNumber { get; set; }
        public string AcknowledgmentSent { get; set; }
        public string ShipmentType { get; set; }
        public int LineNumber { get; set; }
        public string UPC { get; set; }
        public string ContainerID { get; set; }
        public string PoType { get; set; }
        public string ShipPackage { get; set; }
        public decimal ShipPrice { get; set; }
        public decimal ShipWeight { get; set; }
        public string ContactName { get; set; }
        public string ShipMethod { get; set; }
        public string CategoryName { get; set; }
        public string ItemName { get; set; }

    }
    public class PurchaseOrderShipmentCSV
    {

        public string FulfillmentNumber { get; set; }
        public string FulfillmentDate { get; set; }
        public string Ship_Via { get; set; }
        //public string FulfillmentStatus { get; set; }
        public string TrackingNumber { get; set; }
        public string ShipDate { get; set; }
        public string ShipmentType { get; set; }
        public string AcknowledgmentSent { get; set; }
        // public int Line_no { get; set; }
        // public int Qty { get; set; }
        //  public string SKU { get; set; }

        public string ESN { get; set; }
        public string ICC_ID { get; set; }
        public string BatchNumber { get; set; }

        public string ShipPackage { get; set; }
        public decimal Weight { get; set; }
        public decimal Price { get; set; }
        public string ContainerID { get; set; }
        public string ContactName { get; set; }
        public string FulfillmentType { get; set; }

        


    }
    public class SKUSummary
    {
        public string CategoryName { get; set; }

        public string SKU { get; set; }
        public string ProductName { get; set; }
        public int Count { get; set; }

    }
    public class ShipMethodSummary
    {
        public string ShipMethod { get; set; }

        public string ShipPackage { get; set; }
        public decimal Cost { get; set; }

    }
    public class PurchaseOrderShipmentTracking
    {
        public PurchaseOrderShipmentResponse GetPurchaseOrderShipment(PurchaseOrderShipmentRequest serviceRequest)
        {

            PurchaseOrderShipmentResponse serviceResponse = new PurchaseOrderShipmentResponse();
            Exception exc = null;
            try
            {
                CredentialValidation credentialValidation = AuthenticationOperation.AuthenticateRequest(serviceRequest.UserCredentials, out exc);
                if (credentialValidation != null && credentialValidation.CompanyID > 0)
                {

                    List<PurchaseOrderShipment> shipments = GetPurchaseOrderShipment(serviceRequest, credentialValidation.CompanyID);


                    if (shipments != null && shipments.Count > 0)
                    {

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
            }
            catch (Exception ex)
            {
                serviceResponse.Comment = ex.Message;
                serviceResponse.ReturnCode = ResponseErrorCode.ErrowWhileLoadingData.ToString();
                serviceResponse.Shipments = null;

            }

            return serviceResponse;
        }

        public List<PurchaseOrderShipment> GetPurchaseOrderShipment(PurchaseOrderShipmentRequest request, int companyId)
        {
            List<PurchaseOrderShipmentDB> shipmentDB = null;
            System.Text.StringBuilder sbError = new System.Text.StringBuilder();
            List<PurchaseOrderShipment> shipments = null;


            string errorMessage = null;
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataTable dt = new DataTable();
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@piCompanyID", companyId);
                objCompHash.Add("@PO_Num", request.PurchaseOrderNumber);
                objCompHash.Add("@DateFrom", request.DateFrom.ToShortDateString() == "1/1/0001"?null: request.DateFrom.ToShortDateString()); //
                objCompHash.Add("@DateTo", request.DateTo.ToShortDateString() == "1/1/0001" ? null : request.DateTo.ToShortDateString());
                arrSpFieldSeq = new string[] { "@piCompanyID", "@PO_Num", "@DateFrom", "@DateTo" };
                dt = db.GetTableRecords(objCompHash, "av_PurchaseOrder_ShipmentTracking_Select", arrSpFieldSeq);
                shipmentDB = PopulateShipmentDB(dt);
                if (shipmentDB != null && shipmentDB.Count > 0)
                    shipments = PopulateShipment(shipmentDB);
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


            return shipments;
        }

        public List<PurchaseOrderShipmentDB> GetPurchaseOrderShipmentReport(string PurchaseOrderNumber, DateTime DateFrom, DateTime DateTo, int companyId, out decimal totalShipPrice)
        {
            totalShipPrice = 0;
            List<PurchaseOrderShipmentDB> shipments = null;
            List<PurchaseOrderShipmentDB> shipmentDB = null;
            System.Text.StringBuilder sbError = new System.Text.StringBuilder();
            // List<PurchaseOrderShipmentDB> shipments = null;

            string dateFrom = DateFrom.ToShortDateString();
            string dateTo = DateTo.ToShortDateString();


            string errorMessage = null;
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataTable dt = new DataTable();
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@piCompanyID", companyId);
                objCompHash.Add("@PO_Num", PurchaseOrderNumber);
                objCompHash.Add("@DateFrom", dateFrom == "1/1/0001" ? null : dateFrom == "01/01/0001" ? null: dateFrom == "01-01-0001" ? null : dateFrom); //
                objCompHash.Add("@DateTo", dateTo == "1/1/0001" ? null : dateTo == "01/01/0001" ? null : dateTo == "01-01-0001" ? null : dateTo);

                arrSpFieldSeq = new string[] { "@piCompanyID", "@PO_Num", "@DateFrom", "@DateTo" };
                dt = db.GetTableRecords(objCompHash, "av_PurchaseOrder_ShipmentTracking_Select", arrSpFieldSeq);
                shipmentDB = PopulateShipmentDB(dt);
                if(shipmentDB!=null && shipmentDB.Count > 0)
                {
                    var res = shipmentDB.GroupBy(e => new { e.ShipPrice, e.TrackingNumber});

                    totalShipPrice = res.Sum(x => x.Key.ShipPrice);
                }
                HttpContext.Current.Session["shipmentDB"] = shipmentDB;
                shipments = PopulateShipmentdb(shipmentDB);

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


            return shipments;
        }

        public PurchaseOrderShipmentAPIResponse GetPurchaseOrderShipmentAPI(PurchaseOrderShipmentRequest serviceRequest)
        {
            string requestXML = clsGeneral.SerializeObject(serviceRequest);

            LogModel request = new LogModel();
            request.RequestData = requestXML;
            request.ModuleName = "GetPurchaseOrderShipment";
            request.RequestTimeStamp = DateTime.Now;
            Exception ex = null;

            PurchaseOrderShipmentAPIResponse serviceResponse = new PurchaseOrderShipmentAPIResponse();
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

                        List<PurchaseOrderShipping> shipments = GetPurchaseOrderShipmentAPI(serviceRequest, credentialValidation.CompanyID);


                        if (shipments != null && shipments.Count > 0)
                        {

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
                    request.UserID = credentialValidation.UserID;
                    request.CompanyID = credentialValidation.CompanyID;
                    request.ResponseData = clsGeneral.SerializeObject(serviceResponse);
                    request.ResponseTimeStamp = DateTime.Now;
                    request.ExceptionOccured = false;
                    request.ReturnMessage = serviceResponse.Comment;
                    //LogOperations.ApiLogInsert(request);
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
                
            }
            finally
            {
                LogOperations.ApiLogInsert(request);
            }
            return serviceResponse;
        }

        //private List<PurchaseOrderShipmentDB> PopulateShipmentdb(List<PurchaseOrderShipmentDB> shipmentDB)
        //{

        //    List<PurchaseOrderShipmentDB> shipments = new List<PurchaseOrderShipmentDB>();
        //    PurchaseOrderShipmentDB shipment = new PurchaseOrderShipmentDB();
        //    var skuSummary = shipmentDB.GroupBy(k => k.SKU).Select(g => new { Key = g.Key, SKU = g.First().SKU, CategoryName = g.First().CategoryName, ItemName = g.First().ItemName, Quantity = g.Sum(s => s.Qty) }).ToList();

        //    var skuList = shipmentDB.GroupBy(e => new { e.CategoryName, e.PO_ID, e.PO_Date, e.Ship_Via, e.PoType, e.ContactName });
            

        //    return shipments;
        //}


        public List<PurchaseOrderShipping> GetPurchaseOrderShipmentAPI(PurchaseOrderShipmentRequest request, int companyId)
        {
            List<PurchaseOrderShipping> shipments = default;
            List<PurchaseOrderShipmentDB> shipmentDB = default;
            System.Text.StringBuilder sbError = new System.Text.StringBuilder();
            // List<PurchaseOrderShipmentDB> shipments = null;


            string errorMessage = default;
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataTable dt = new DataTable();
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@piCompanyID", companyId);
                objCompHash.Add("@PO_Num", request.PurchaseOrderNumber);
                objCompHash.Add("@DateFrom", request.DateFrom.ToShortDateString() == "1/1/0001" ? null : request.DateFrom.ToShortDateString()); //
                objCompHash.Add("@DateTo", request.DateTo.ToShortDateString() == "1/1/0001" ? null : request.DateTo.ToShortDateString());

                arrSpFieldSeq = new string[] { "@piCompanyID", "@PO_Num", "@DateFrom", "@DateTo" };
                dt = db.GetTableRecords(objCompHash, "av_PurchaseOrder_ShipmentTracking_Select", arrSpFieldSeq);
                shipmentDB = PopulateShipmentDB(dt);
               // HttpContext.Current.Session["shipmentDB"] = shipmentDB;
                shipments = PopulateShipmentAPI(shipmentDB);

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


            return shipments;
        }
        
        private List<PurchaseOrderShipping> PopulateShipmentAPI(List<PurchaseOrderShipmentDB> shipmentDB)
        {
            List<PurchaseOrderShipping> shipments = new List<PurchaseOrderShipping>();
            PurchaseOrderShipping shipment = new PurchaseOrderShipping();
            var res = shipmentDB.GroupBy(e => new { e.PO_NUM, e.PO_ID, e.PO_Date, e.Ship_Via });
            List<PurchaseOrderTracking> trackings = default;
            PurchaseOrderTracking tracking = default;
            List<LineItems> lineItems = default;
            LineItems lineItem = default;
            int qty = 1;
            foreach (var val in res)
            {
                shipment = new PurchaseOrderShipping()
                {
                    PurchaseOrderNumber = val.Key.PO_NUM,
                    PODate = val.Key.PO_Date,
                    ShippingMethod = val.Key.Ship_Via,
                    
                };
                trackings = new List<PurchaseOrderTracking>();
                foreach (PurchaseOrderShipmentDB e in val)
                {
                    tracking = new PurchaseOrderTracking();

                    tracking.TrackingNumber = e.TrackingNumber;
                    tracking.AcknowledgmentSent = e.AcknowledgmentSent;
                    tracking.ShipDate = e.ShipDate;
                    tracking.ShipmentType = e.ShipmentType;

                    lineItems = new List<LineItems>();
                    var poLineItemList = (from item in shipmentDB where item.TrackingNumber.Equals(tracking.TrackingNumber) select item).ToList();
                    foreach (PurchaseOrderShipmentDB item in poLineItemList)
                    {
                        lineItem = new LineItems();
                        lineItem.BatchNumber = item.BatchNumber;
                        lineItem.ESN = item.ESN;
                        lineItem.ICCID = item.ICC_ID;
                        lineItem.SKU = item.SKU;
                        if (string.IsNullOrWhiteSpace(lineItem.ESN))
                            qty = item.Qty;
                        lineItem.Qty = qty;
                        lineItems.Add(lineItem);

                    }
                    tracking.LineItems = lineItems;
                    trackings.Add(tracking);
                }

                shipment.Trackings = trackings;
                shipments.Add(shipment);
            }
            return shipments;
        }
        private List<PurchaseOrderShipmentDB> PopulateShipmentdb(List<PurchaseOrderShipmentDB> shipmentDB)
        {

            List<PurchaseOrderShipmentDB> shipments = new List<PurchaseOrderShipmentDB>();
            PurchaseOrderShipmentDB shipment = new PurchaseOrderShipmentDB();
            var res = shipmentDB.GroupBy(e => new { e.PO_NUM, e.PO_ID, e.PO_Date, e.Ship_Via, e.PoType, e.ContactName });
            //e.ShipPackage, e.ShipPrice, e.ShipWeight, 
            foreach (var val in res)
            {
                shipment = new PurchaseOrderShipmentDB()
                {
                    PO_NUM = val.Key.PO_NUM,
                    Ship_Via = val.Key.Ship_Via,
                    PO_ID = val.Key.PO_ID,
                    PO_Date = val.Key.PO_Date,
                    ContactName = val.Key.ContactName,
                    PoType = val.Key.PoType
                   // ShipWeight = val.Key.ShipWeight,
                   // ShipPrice = val.Key.ShipPrice,
                   // ShipPackage = val.Key.ShipPackage
//                    ShipDate = (val.Key.ShipDate != null ? val.Key.ShipDate.ToString() : null),
// AcknowledgmentSent = (val.Key.AcknowledgmentSent != null ? val.Key.AcknowledgmentSent.ToString() : ""),
                };
                
                shipments.Add(shipment);
            }


            return shipments;
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
                    shipment.UPC = clsGeneral.getColumnData(dataRow, "UPC", string.Empty, false) as string;

                    shipment.Line_no = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Line_no", 0, false));
                    shipment.PO_Date = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "PO_Date", DateTime.MinValue, false)).ToShortDateString();
                    shipment.PO_ID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Po_ID", 0, false));
                    shipment.Qty = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Qty", 0, false));
                    shipment.PO_NUM = clsGeneral.getColumnData(dataRow, "PO_NUM", string.Empty, false) as string;
                    shipment.ShipDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "ShipDate", DateTime.MinValue, false)).ToShortDateString();
                    shipment.Ship_Via = clsGeneral.getColumnData(dataRow, "Ship_Via", string.Empty, false) as string;
                    shipment.SKU = clsGeneral.getColumnData(dataRow, "item_code", string.Empty, false) as string;
                    shipment.TrackingNumber = clsGeneral.getColumnData(dataRow, "TrackingNumber", string.Empty, false) as string;
                    shipment.AcknowledgmentSent = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "AcknowledgmentSent", DateTime.MinValue, false)).ToShortDateString();//clsGeneral.getColumnData(dataRow, "AcknowledgmentSent", string.Empty, false) as string;
                    shipment.LineNumber = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "LineNumber", 0, false));
                    
                    shipment.PoType = clsGeneral.getColumnData(dataRow, "PoType", string.Empty, false) as string;
                    shipment.ContactName = clsGeneral.getColumnData(dataRow, "Contact_Name", string.Empty, false) as string;
                    shipment.ShipmentType = clsGeneral.getColumnData(dataRow, "ShipmentType", string.Empty, false) as string;
                    shipment.ShipPackage = clsGeneral.getColumnData(dataRow, "ShipPackage", string.Empty, false) as string;
                    shipment.ContainerID = clsGeneral.getColumnData(dataRow, "ContainerID", string.Empty, false) as string;
                    shipment.ShipMethod = clsGeneral.getColumnData(dataRow, "ShipByText", string.Empty, false) as string;
                    shipment.CategoryName = clsGeneral.getColumnData(dataRow, "CategoryName", string.Empty, false) as string;
                    shipment.ItemName = clsGeneral.getColumnData(dataRow, "ItemName", string.Empty, false) as string;

                    shipment.ShipPrice = Convert.ToDecimal(clsGeneral.getColumnData(dataRow, "FinalPostage", 0, false));
                    shipment.ShipWeight = Convert.ToDecimal(clsGeneral.getColumnData(dataRow, "ShippingWeight", 0, false));
                    shipments.Add(shipment);
                }
            }

            return shipments;
        }
        private List<PurchaseOrderShipment> PopulateShipment(List<PurchaseOrderShipmentDB> shipmentDB)
        {

            List<PurchaseOrderShipment> shipments = new List<PurchaseOrderShipment>();
            PurchaseOrderShipment shipment = new PurchaseOrderShipment();
            LineItem lineItem = null;
            List<LineItem> lineItems = null;
            var res = shipmentDB.GroupBy(e => new { e.PO_NUM, e.PO_ID, e.PO_Date, e.Ship_Via, e.TrackingNumber, e.ShipDate });

            foreach (var val in res)
            {
                shipment = new PurchaseOrderShipment()
                {
                    PurchaseOrderNumber = val.Key.PO_NUM,
                    ShippingMethod = val.Key.Ship_Via,
                    ShippingTrackingNumber = val.Key.TrackingNumber,
                    ShippingDate = (val.Key.ShipDate != null? val.Key.ShipDate.ToString():null) ,
                };
                lineItems = new List<LineItem>();
                foreach (PurchaseOrderShipmentDB e in val)
                {
                    lineItem = new LineItem();
                 //   lineItem.BatchNumber = e.BatchNumber;
                  //  lineItem.ESN = e.ESN;
                    lineItem.ICCID = e.ICC_ID;
                    lineItem.ItemCode = e.SKU;
                    lineItem.LineNo = (int)e.Line_no;
                    lineItem.Quantity = e.Qty;

                    
                    lineItems.Add(lineItem);
                }

                shipment.LineItems = lineItems;
                shipments.Add(shipment);
            }
            

            return shipments;
        }

        

    }
    }
