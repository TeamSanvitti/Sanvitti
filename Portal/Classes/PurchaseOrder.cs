using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
//using System.Web;
//using System.Web.UI;
//using System.Web.UI.WebControls;
//using System.IO;
using System.Linq;
//using System.Configuration;


namespace avii.Classes
{

    public enum MissingList
    {
        ESN,
        MSL,
        ESN_MSL,
        ASN,
        ESN_MSL_ASN,
        MSL_ASN
    }
    public struct PhoneSummary
    {
        private DateTime startDate;
        private DateTime endDate;
        private string itemCode;
        private int userID;
        private bool addHistory;
        private int companyID;

        public int CompanyID
        {
            get
            {
                return companyID;
            }
            set
            {
                companyID = value;
            }
        }

        public DateTime StartDate
        {
            get
            {
                return startDate;
            }
            set
            {
                startDate = value;
            }
        }

        public DateTime EndDate
        {
            get
            {
                return endDate;
            }
            set
            {
                endDate = value;
            }
        }

        public int UserID
        {
            get
            {
                return userID;
            }
            set
            {
                userID = value;
            }
        }

        public bool AddHistory
        {
            get
            {
                return addHistory;
            }
            set
            {
                addHistory = value;
            }
        }

        public string ItemCode
        {
            get
            {
                return itemCode;
            }
            set
            {
                itemCode = value;
            }
        }
    }

    [XmlRoot(ElementName = "lineitem", IsNullable = true)]
    public class PurchaseOrderESNItem
    {
        private int _lineNo = 0;
        private int _POD_ID = 0;
        private string _itemCode = string.Empty;
        private string _esn = string.Empty;
        private string _mslNumber = string.Empty;
        private string _storeID = string.Empty;
        private string _trackingNumber = string.Empty;
        private string _fmupc = string.Empty;


        [XmlElement(ElementName = "podid")]
        public int PodID
        {
            get
            {
                return _POD_ID;
            }
            set
            {
                _POD_ID = value;
            }
        }

        [XmlElement(ElementName = "lineno")]
        public int LineNo
        {
            get
            {
                return _lineNo;
            }
            set
            {
                _lineNo = value;
            }
        }

        [XmlElement(ElementName = "itemcode", IsNullable = true)]
        public string ItemCode
        {
            get
            {
                if (string.IsNullOrEmpty(_itemCode))
                    return string.Empty;
                else
                    return _itemCode;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new Exception("Item code should not be null");
                }
                else
                {
                    _itemCode = value;
                }
            }
        }

        [XmlElement(ElementName = "esn", IsNullable = true)]
        public string ESN
        {
            get
            {
                if (string.IsNullOrEmpty(_esn))
                    return string.Empty;
                else
                    return _esn;
            }
            set
            {
                _esn = value;
            }
        }


        [XmlElement(ElementName = "mslnumber", IsNullable = true)]
        public string MslNumber
        {
            get
            {
                return _mslNumber;
            }
            set
            {
                _mslNumber = value;
            }
        }


        [XmlElement(ElementName = "storeid", IsNullable = true)]
        public string StoreID
        {
            get
            {
                return _storeID;
            }
            set
            {
                _storeID = value;
            }
        }

        [XmlElement(ElementName = "trackingnumber", IsNullable = true)]
        public string TrackingNumber
        {
            get
            {
                return _trackingNumber;
            }
            set
            {
                _trackingNumber = value;
            }
        }

        [XmlElement(ElementName = "fmupc", IsNullable = true)]
        public string FmUPC
        {
            get
            {
                return _fmupc;
            }
            set
            {
                _fmupc = value;
            }
        }
    }

    public class PurchaseOrderESN
    {
        private string _purchaseOrderNumber;
        private string _trackingNumber;
        private List<PurchaseOrderESNItem> _items;


        public PurchaseOrderESN()
        {
            _items = new List<PurchaseOrderESNItem>();
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

        public string TrackingNumber
        {
            get
            {
                return _trackingNumber;
            }
            set
            {
                _trackingNumber = value;
            }
        }

        public List<PurchaseOrderESNItem> PurchaseOrderESNItems
        {
            get
            {
                return _items;
            }
            set
            {
                _items = value;
            }

        }
    }
    public class PurchaseOrder
    {

        #region Public Methods
        public static bool AuthenticateRequestNew(clsAuthentication AuthenticateUser, out int userid, out int companyID)
        {
            userid = companyID = 0;
            bool returnValue = false;
            returnValue = clsUser.ValidateUserNew(AuthenticateUser, out userid, out companyID);
            //int userId = 0;
            //userId = clsUser.ValidateUser(AuthenticateUser);
            return returnValue;
        }

        public static PurchaseOrderProvisioningResponse PurchaseOrderProvisioning(PurchaseOrderProvisioningRequest purchaseOrderProvisioningRequest)
        {
            PurchaseOrderProvisioningResponse purchaseOrderProvisioningResponse = new PurchaseOrderProvisioningResponse();
            string assignmentType = string.Empty;
            Exception exc = null;

            try
            {
                int UserID = AuthenticateRequest(purchaseOrderProvisioningRequest.Authentication, out exc);
                
                string esnType = purchaseOrderProvisioningRequest.AssignmentType.ToString();
                if (esnType == "NewAssign")
                    assignmentType = "A";
                else
                    assignmentType = "S";
                if (UserID > 0)
                {

                    purchaseOrderProvisioningResponse = SavePOProvisioning(purchaseOrderProvisioningRequest.ProvisioningInfo.ESN,
                                        purchaseOrderProvisioningRequest.ProvisioningInfo.MdnNumber,
                                        purchaseOrderProvisioningRequest.ProvisioningInfo.MsID,
                                        purchaseOrderProvisioningRequest.ProvisioningInfo.PassCode, purchaseOrderProvisioningRequest.ProvisioningInfo.MslNumber, purchaseOrderProvisioningRequest.ProvisioningInfo.OTKSL, purchaseOrderProvisioningRequest.ProvisioningInfo.Akey, assignmentType);

                }
                else
                {
                    purchaseOrderProvisioningResponse.Comment = "Cannot authenticate user";
                    purchaseOrderProvisioningResponse.ErrorCode = ResponseErrorCode.CannotAuthenticateUser.ToString();
                }
            }
            catch (Exception ex)
            {
                purchaseOrderProvisioningResponse.Comment = ex.Message;
                purchaseOrderProvisioningResponse.ErrorCode = ResponseErrorCode.ErrowWhileLoadingData.ToString();
            }
             return purchaseOrderProvisioningResponse;
        }

        private static PurchaseOrderProvisioningResponse SavePOProvisioning(string esn, string mdn, string msid, string passCode, string mslNumber, string otksl, string akey, string assignmentType)
        {
            String errorMessage = string.Empty;

            PurchaseOrderProvisioningResponse response = new PurchaseOrderProvisioningResponse();
            errorMessage = PurchaseOrderProvisioning(esn, mdn, msid, passCode,mslNumber, otksl, akey, assignmentType);
            if (errorMessage.Equals(string.Empty))
            {
                response.ErrorCode = string.Empty;
                response.Comment = string.Empty;
            }
            else
            {
                response.Comment = esn + " does not exists";
                response.ErrorCode = ResponseErrorCode.ErrowWhileLoadingData.ToString() ;
            }
            
            return response;
        }

        private static PurchaseOrderProvisioningResponse SavePOProvisioning2(string esn, string mdn, string msid, string passCode, string mslNumber, string otksl, string akey)
        {
            String errorMessage = string.Empty;

            PurchaseOrderProvisioningResponse response = new PurchaseOrderProvisioningResponse();
            errorMessage = PurchaseOrderProvisioning2(esn, mdn, msid, passCode, mslNumber, otksl, akey);
            if (errorMessage.Equals(string.Empty))
            {
                response.ErrorCode = string.Empty;
                response.Comment = string.Empty;
            }
            else
            {
                response.Comment = esn + " does not exists";
                response.ErrorCode = ResponseErrorCode.ErrowWhileLoadingData.ToString();
            }

            return response;
        }

        // Validate Ship BY Code from DB
        private static bool ValidateShipByCode(PurchaseOrderRequest purchaseOrderRequest)
        {
            bool errorValue = false;
            string errorMessage = string.Empty;
            if (!string.IsNullOrEmpty(purchaseOrderRequest.PurchaseOrder.ShipThrough.Trim()))
            {
                errorMessage = ValidateShipByText(purchaseOrderRequest.PurchaseOrder.ShipThrough.Trim());
                if (!string.IsNullOrEmpty(errorMessage))
                    errorValue = true;
            }
            //else
            //    errorMessage = " Ship By is required";

            return errorValue;
        }
        private static string ValidateDuplicateSKU(PurchaseOrderRequest purchaseOrderRequest)
        {
            string errorMessage = string.Empty;
            List<avii.Classes.PurchaseOrderItem> polist = purchaseOrderRequest.PurchaseOrder.PurchaseOrderItems;
            var duplicateItems = polist.GroupBy(x => x).Where(g => g.Count() > 1).Select(y => y.Key).ToList();

            // var duplicateItems = (from itemcodes in polist where itemcodes.ItemCode.ToUpper().Equals(itemCode.ToUpper()) select itemcodes).ToList();
            if (duplicateItems != null && duplicateItems.Count > 0 && !string.IsNullOrEmpty(duplicateItems[0].ItemCode))
            {
                errorMessage = "Duplicate SKU are  not allowed!";
            
            }
            return errorMessage;
        }
            // Validate mamdatory fields
            private static string ValidateSave(PurchaseOrderRequest purchaseOrderRequest)
        {
            DateTime currentDate = DateTime.Now;
            //DateTime podate = new DateTime();
           // DateTime requestedshipdate = new DateTime();
            TimeSpan diffResult = currentDate - currentDate;

            TimeSpan diffResultShipDate = currentDate - currentDate;


            if (!string.IsNullOrEmpty(purchaseOrderRequest.PurchaseOrder.PurchaseOrderDate.ToShortDateString()))
            {
                diffResult = currentDate - purchaseOrderRequest.PurchaseOrder.PurchaseOrderDate;
                //podate = purchaseOrderRequest.PurchaseOrder.PurchaseOrderDate;


            }
            if (!string.IsNullOrEmpty(purchaseOrderRequest.PurchaseOrder.RequestedShipDate.ToShortDateString()))
            {
                diffResultShipDate = currentDate - purchaseOrderRequest.PurchaseOrder.RequestedShipDate;
                //podate = purchaseOrderRequest.PurchaseOrder.PurchaseOrderDate;


            }
            

            //TimeSpan diffResult = currentDate - podate;



            string errorMessage = string.Empty;
            if (string.IsNullOrEmpty(purchaseOrderRequest.PurchaseOrder.PurchaseOrderNumber))
            {

                errorMessage = " Fulfillment Number is required";
            }
            //else if (purchaseOrderRequest.PurchaseOrder.PurchaseOrderNumber.Length > 15)
            //{
            //    errorMessage = " Fulfillment Number can not exceed 15 characters";
            //}
            else if (string.IsNullOrEmpty(purchaseOrderRequest.PurchaseOrder.PurchaseOrderDate.ToShortDateString()))
            {
                if (string.IsNullOrEmpty(errorMessage))
                    errorMessage = " Fulfillment Date is required";
                else
                    errorMessage = " \r\n Fulfillment Date is required";
            }
            else if (diffResult.Days > 90 || diffResult.Days <= -1)
            {
                if (string.IsNullOrEmpty(errorMessage))
                    errorMessage = " Invalid Fulfillment Date: Fulfillment order date can not be less than 90 days from Today's date.";
                else
                    errorMessage = " \r\n Invalid Fulfillment Date: Fulfillment order date can not be less than 90 days from Today's date.";
            }
            else if (diffResultShipDate.Days > 0)
            {
                if (string.IsNullOrEmpty(errorMessage))
                    errorMessage = " Invalid Requested Ship Date: Can not create Fulfillment order before  current date.";
                else
                    errorMessage = " \r\n Invalid Requested Ship Date: Can not create Fulfillment order before current date.";
            }
            //else if (string.IsNullOrEmpty(purchaseOrderRequest.PurchaseOrder.StoreID))
            //{
            //    if (string.IsNullOrEmpty(errorMessage))
            //        errorMessage = " StoreID is required";
            //    else
            //        errorMessage = " \r\n StoreID is required";

            //}
            else if (string.IsNullOrEmpty(purchaseOrderRequest.PurchaseOrder.Shipping.ContactName))
            {
                if (string.IsNullOrEmpty(errorMessage))
                    errorMessage = " Contact Name is required";
                else
                    errorMessage = " \r\n Contact Name is required";
            }
            
            else if (string.IsNullOrEmpty(purchaseOrderRequest.PurchaseOrder.Shipping.ShipToAddress))
            {
                if (string.IsNullOrEmpty(errorMessage))
                    errorMessage = " Address is required";
                else
                    errorMessage = " \r\n Address is required";

            }
            else if (string.IsNullOrEmpty(purchaseOrderRequest.PurchaseOrder.Shipping.ContactPhone))
            {
                purchaseOrderRequest.PurchaseOrder.Shipping.ContactPhone = System.Configuration.ConfigurationSettings.AppSettings["ShipFromPhone"].ToString();
                //if (string.IsNullOrEmpty(errorMessage))
                //    errorMessage = " Contact Phone is required";
                //else
                //    errorMessage = " \r\n Contact Phone is required";
            }
            else if (string.IsNullOrEmpty(purchaseOrderRequest.PurchaseOrder.Shipping.ShipToCity))
            {
                if (string.IsNullOrEmpty(errorMessage))
                    errorMessage = " City name is required";
                else
                    errorMessage = " \r\n City name is required";
            }
            else if (string.IsNullOrEmpty(purchaseOrderRequest.PurchaseOrder.Shipping.ShipToState))
            {
                if (string.IsNullOrEmpty(errorMessage))
                    errorMessage = " State is required";
                else
                    errorMessage = " \r\n State is required";
            }
            else if (string.IsNullOrEmpty(purchaseOrderRequest.PurchaseOrder.Shipping.ShipToZip))
            {
                if (string.IsNullOrEmpty(errorMessage))
                    errorMessage = " Zip code is required";
                else
                    errorMessage = " \r\n Zip code is required";
            }
            else if (string.IsNullOrEmpty(purchaseOrderRequest.PurchaseOrder.ShipThrough))
            {
                if (string.IsNullOrEmpty(errorMessage))
                    errorMessage = " Ship method is required";
                else
                    errorMessage = " \r\n Ship method is required";
            }
            return errorMessage;
        }

        // Called by webmethod
        private static bool ValidateQty(List<avii.Classes.PurchaseOrderItem> poItems)
        {
            bool returnValue = true;
            if (poItems != null)
            {
                foreach (avii.Classes.PurchaseOrderItem poItem in poItems)
                {
                    if (poItem.Quantity != null && poItem.Quantity < 1)
                    {
                        returnValue = false;
                    }
                }
            }
            return returnValue;


        }
        private static List<avii.Classes.PurchaseOrderItem> CleanItemList(List<avii.Classes.PurchaseOrderItem> poItems)
        {
            if (poItems != null)
            {
                List<avii.Classes.PurchaseOrderItem> tempItems = new List<avii.Classes.PurchaseOrderItem>();
                List<avii.Classes.PurchaseOrderItem> Itemsgreater = new List<avii.Classes.PurchaseOrderItem>();
                foreach (avii.Classes.PurchaseOrderItem poItem in poItems)
                {
                    if (string.IsNullOrEmpty(poItem.ItemCode) || poItem.Quantity == 0 || poItem.Quantity == null)
                    {
                        tempItems.Add(poItem);
                    }

                    if (poItem.Quantity != null && poItem.Quantity > 1)
                    {
                        Itemsgreater.Add(poItem);
                        tempItems.Add(poItem);
                    }
                }

                foreach (avii.Classes.PurchaseOrderItem poItem in tempItems)
                {
                    poItems.Remove(poItem);

                }

                int? qty = 0;
                foreach (avii.Classes.PurchaseOrderItem poItem in Itemsgreater)
                {
                    qty = (poItem.Quantity != null ? poItem.Quantity : 0);
                    poItem.Quantity = 1;
                    for (int ctr = 0; ctr <= qty - 1; ctr++)
                    {
                        poItems.Add(poItem);
                    }
                }
            }

            return poItems;
        }
        // Called by webmethod
        
        public static PurchaseOrderResponse CreatePurchaseOrder(PurchaseOrderRequest purchaseOrderRequest, InventoryList inventoryList, int userId)
        {
            PurchaseOrderResponse purchaseOrderResponse = new PurchaseOrderResponse();
            purchaseOrderResponse.ErrorCode = ResponseErrorCode.ErrowWhileLoadingData.ToString();
           // purchaseOrderResponse.PurchaseOrderNumber = purchaseOrderRequest.PurchaseOrder.PurchaseOrderNumber;
            try
            {
                string errorMessage = string.Empty;
                // Validate mamdatory fields
                errorMessage = ValidateDuplicateSKU(purchaseOrderRequest);
                if (string.IsNullOrEmpty(errorMessage) && userId > 0)
                {
                    errorMessage = ValidateSave(purchaseOrderRequest);
                    if (string.IsNullOrEmpty(errorMessage) && userId > 0)
                    {
                        if (ValidateShipByCode(purchaseOrderRequest))
                        {
                            //if (ValidatePurchaseOrder(purchaseOrderRequest.PurchaseOrder, userId))
                            //{
                                string itemCodes = string.Empty;
                                if (purchaseOrderRequest.PurchaseOrder.PurchaseOrderItems != null && purchaseOrderRequest.PurchaseOrder.PurchaseOrderItems.Count > 0)
                                {
                                    if (ValidationItems(purchaseOrderRequest.PurchaseOrder, inventoryList, out itemCodes))
                                    {
                                        if (ValidateQty(purchaseOrderRequest.PurchaseOrder.PurchaseOrderItems))
                                        {

                                        purchaseOrderResponse = SavePurchaseOrder(purchaseOrderRequest.PurchaseOrder, userId);
                                    }
                                    else
                                    {
                                     purchaseOrderResponse.Comment = "Line item quantity must be greater than 0";
                                     purchaseOrderResponse.ErrorCode = ResponseErrorCode.QuantityIsNotCorrect.ToString();
                                    }
                                }
                                else
                                    {
                                        purchaseOrderResponse.Comment = "Purchase Order items (" + itemCodes + ") are not valid, please check the item code from catalog";

                                    }
                                }
                                else
                                {
                                    purchaseOrderResponse.Comment = "Can not create purchase order without item";
                                    purchaseOrderResponse.ErrorCode = ResponseErrorCode.PurchaseOrderItemNotAssigned.ToString();
                                }

                            //}
                            //else
                            //{
                            //    purchaseOrderResponse.Comment = "Purchase Order number already exists";
                            //}
                        }
                        else
                        {
                            purchaseOrderResponse.Comment = "Ship via is not correct"; ;
                            purchaseOrderResponse.ErrorCode = ResponseErrorCode.ShipByIsNotCorrect.ToString();
                        }
                    }
                    else
                    {
                        purchaseOrderResponse.Comment = errorMessage;
                        purchaseOrderResponse.ErrorCode = ResponseErrorCode.MissingParameter.ToString();
                    }
                }
                else
                {
                    purchaseOrderResponse.Comment = errorMessage;
                    purchaseOrderResponse.ErrorCode = ResponseErrorCode.DuplicateItemFound.ToString();
                }
            }
            catch (Exception ex)
            {
                purchaseOrderResponse.ErrorCode = ResponseErrorCode.InternalError.ToString();
                purchaseOrderResponse.Comment = ex.Message;
            }
            return purchaseOrderResponse;
        }
        
        
        //Called by webmethod
        public static FulfillmentReportResponse FulfillmentOrdersProvisionalList(FulfillmentReportRequest poRequest)
        {
            FulfillmentReportResponse serviceResponse = new FulfillmentReportResponse();
            serviceResponse.ReturnCode = ResponseErrorCode.MissingParameter.ToString();
            string requestXML = clsGeneral.SerializeObject(poRequest);

            LogModel request = new LogModel();
            request.RequestData = requestXML;
            request.ModuleName = "GetPurchaseOrderProvisioning";
            request.RequestTimeStamp = DateTime.Now;
            Exception ex = null;

            try
            {

                if (poRequest != null)
                {

                    int userId = PurchaseOrder.AuthenticateRequest(poRequest.Authentication, out ex);
                    if (ex != null)
                    {
                        serviceResponse.Comment = ex.Message;
                        serviceResponse.ReturnCode = ResponseErrorCode.ErrowWhileLoadingData.ToString();

                        request.ResponseData = clsGeneral.SerializeObject(serviceResponse);
                        request.ResponseTimeStamp = DateTime.Now;
                        request.ExceptionOccured = true;
                        request.ReturnMessage = ResponseErrorCode.ErrowWhileLoadingData.ToString();
                        //LogOperations.ApiLogInsert(request);
                    }
                    if (userId > 0)
                    {
                        serviceResponse = FulfillmentOrdersProvisionalList(poRequest, userId);

                    }
                    else
                    {
                        serviceResponse.Comment = "Cannot authenticate user";
                        serviceResponse.ReturnCode = ResponseErrorCode.CannotAuthenticateUser.ToString();
                    }
                    request.UserID = userId;
                    request.CompanyID = 0;
                    request.ResponseData = clsGeneral.SerializeObject(serviceResponse);
                    request.ResponseTimeStamp = DateTime.Now;
                    request.ExceptionOccured = false;
                    request.ReturnMessage = serviceResponse.Comment;

                }
            }
            catch (Exception exc)
            {
                ex = exc;
                serviceResponse.FulfillmentOrderList = null;
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
        public static FulfillmentOrderWithHeaderResponse FulfillmentOrdersListWithHeader(FulfillmentReportRequest poRequest)
        {
            FulfillmentOrderWithHeaderResponse serviceResponse = new FulfillmentOrderWithHeaderResponse();
            serviceResponse.ReturnCode = ResponseErrorCode.MissingParameter.ToString();
            Exception exc = null;

            try
            {

                if (poRequest != null)
                {

                    int userId = PurchaseOrder.AuthenticateRequest(poRequest.Authentication, out exc);
                    if (userId > 0)
                    {
                        serviceResponse = FulfillmentOrdersListWithHeader(poRequest, userId);
                    }
                }
            }
            catch (Exception ex)
            {
                serviceResponse.FulfillmentOrderList = null;
                serviceResponse.Comment = ex.Message;
                serviceResponse.ReturnCode = ResponseErrorCode.InconsistantData.ToString();
            }
            return serviceResponse;
        }
        public static FulfillmentOrderListItemsResponse FulfillmentOrdersListItems(FulfillmentReportRequest poRequest)
        {
            FulfillmentOrderListItemsResponse serviceResponse = new FulfillmentOrderListItemsResponse();
            serviceResponse.ReturnCode = ResponseErrorCode.MissingParameter.ToString();
            Exception exc = null;

            try
            {

                if (poRequest != null)
                {

                    int userId = PurchaseOrder.AuthenticateRequest(poRequest.Authentication, out exc);
                    if (userId > 0)
                    {
                        serviceResponse = FulfillmentOrdersListItems(poRequest, userId);
                    }
                }
            }
            catch (Exception ex)
            {
                serviceResponse.FulfillmentOrderList = null;
                serviceResponse.Comment = ex.Message;
                serviceResponse.ReturnCode = ResponseErrorCode.InconsistantData.ToString();
            }
            return serviceResponse;
        }
        
        private static List<FulfillmentOrdersProvisional> FulfillmentDownloadReportDB(FulfillmentReportRequest poRequest, int userID, int downloadFlag)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            List<FulfillmentOrdersProvisional> posList = null;
            DataSet ds = new DataSet();
            Hashtable objCompHash = new Hashtable();
            StringBuilder stringBuilder = new StringBuilder();
            try
            {
                objCompHash.Add("@Po_Ids", null);
                objCompHash.Add("@DownloadFlag", downloadFlag);
                objCompHash.Add("@PO_Num", poRequest.FulfillmentNumber);
                objCompHash.Add("@FromDate", poRequest.FulfillmentFromDate.ToShortDateString());
                objCompHash.Add("@ToDate", poRequest.FulfillmentToDate.ToShortDateString());
                objCompHash.Add("@ShipFromDate", poRequest.ShipFromDate.ToShortDateString());
                objCompHash.Add("@ShipToDate", poRequest.ShipToDate.ToShortDateString());

                //if (poRequest.FromPODate.ToShortDateString().Contains("1/1/0001"))
                //    objCompHash.Add("@FromDate", null);
                //else
                //    objCompHash.Add("@FromDate", poRequest.FromPODate);
                
                //if (poRequest.ToPODate.ToShortDateString().Contains("1/1/0001"))
                //    objCompHash.Add("@ToDate", null);
                //else
                //    objCompHash.Add("@ToDate", poRequest.ToPODate);
                
                //if (poRequest.FromShipDate.ToShortDateString().Contains("1/1/0001"))
                //    objCompHash.Add("@ShipFromDate", null);
                //else
                //    objCompHash.Add("@ShipFromDate", poRequest.FromShipDate);

                //if (poRequest.ToShipDate.ToShortDateString().Contains("1/1/0001"))
                //    objCompHash.Add("@ShipToDate", null);
                //else
                //    objCompHash.Add("@ShipToDate", poRequest.ToShipDate);

                objCompHash.Add("@StatusID", poRequest.FulfillmentOrderStatus);
                objCompHash.Add("@TrackingNumber", poRequest.TrackingNumber);
                objCompHash.Add("@UserID", userID);


                arrSpFieldSeq = new string[] { "@Po_Ids", "@DownloadFlag", "@PO_Num", "@FromDate", "@ToDate", "@ShipFromDate", "@ShipToDate", "@StatusID", "@TrackingNumber", "@UserID" };


                ds = db.GetDataSet(objCompHash, "AV_PurchaseOrder_Download", arrSpFieldSeq);

                posList = PopulateFulfillmentOrdersProvisional(ds);

                
                //if (outParam == purchaseOrderNumber)
                //{
                //    rmaResponse.ErrorCode = string.Empty;
                //    rmaResponse.Comment = ResponseErrorCode.UpdatedSuccessfully.ToString();
                //}
                //else
                //{
                //    rmaResponse.ErrorCode = ResponseErrorCode.PurchaseOrderNotExists.ToString();
                //    rmaResponse.Comment = ResponseErrorCode.PurchaseOrderNotExists.ToString();
                //}
            }

            catch (Exception objExp)
            {
                //serviceResponse.Comment = ex.Message;
                //rmaResponse.ErrorCode = ResponseErrorCode.InconsistantData.ToString();
                //rmaResponse.Comment = objExp.Message.ToString();
                throw objExp;
            }
            finally
            {
                db.DBClose();
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;

            }

            return posList;
        }
        private static FulfillmentReportResponse FulfillmentOrdersProvisionalList(FulfillmentReportRequest poRequest, int userID)
        {

            FulfillmentReportResponse serviceResponse = new FulfillmentReportResponse();
            try
            {
                List<FulfillmentOrdersProvisional> posList = FulfillmentDownloadReportDB(poRequest, userID, 0);
                if (posList != null && posList.Count > 0)
                {
                    serviceResponse.FulfillmentOrderList = posList;
                    serviceResponse.Comment = string.Empty;
                    serviceResponse.ReturnCode = ResponseErrorCode.SuccessfullyRetrieved.ToString();
                }
                else
                {
                    serviceResponse.FulfillmentOrderList = null;
                    serviceResponse.Comment = string.Empty;
                    serviceResponse.ReturnCode = ResponseErrorCode.NoRecordsFound.ToString();
                }


            }
            catch (Exception ex)
            {
                serviceResponse.FulfillmentOrderList = null;
                serviceResponse.Comment = ex.Message;
                serviceResponse.ReturnCode = ResponseErrorCode.InternalError.ToString();
            }
            //serviceResponse = DownloadPoHeader(pos);
            return serviceResponse;
        }
        
        private static FulfillmentOrderWithHeaderResponse FulfillmentOrdersListWithHeader(FulfillmentReportRequest poRequest, int userID)
        {

            FulfillmentOrderWithHeaderResponse serviceResponse = new FulfillmentOrderWithHeaderResponse();
            try
            {
                List<FulfillmentOrdersListWithHeader> posList = FulfillmentOrdersListWithHeaderDB(poRequest, userID, 1);
                if (posList != null && posList.Count > 0)
                {
                    serviceResponse.FulfillmentOrderList = posList;
                    serviceResponse.Comment = string.Empty;
                    serviceResponse.ReturnCode = ResponseErrorCode.SuccessfullyRetrieved.ToString();
                }
                else
                {
                    serviceResponse.FulfillmentOrderList = null;
                    serviceResponse.Comment = string.Empty;
                    serviceResponse.ReturnCode = ResponseErrorCode.NoRecordsFound.ToString();
                }


            }
            catch (Exception ex)
            {
                serviceResponse.FulfillmentOrderList = null;
                serviceResponse.Comment = ex.Message;
                serviceResponse.ReturnCode = ResponseErrorCode.InternalError.ToString();
            }
            //serviceResponse = DownloadPoHeader(pos);
            return serviceResponse;
        }
        private static List<FulfillmentOrdersListWithHeader> FulfillmentOrdersListWithHeaderDB(FulfillmentReportRequest poRequest, int userID, int downloadFlag)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            List<FulfillmentOrdersListWithHeader> posList = new List<FulfillmentOrdersListWithHeader>();
            DataTable dt = new DataTable();
            Hashtable objCompHash = new Hashtable();
            StringBuilder stringBuilder = new StringBuilder();
            try
            {
                objCompHash.Add("@Po_Ids", null);
                objCompHash.Add("@DownloadFlag", downloadFlag);
                objCompHash.Add("@PO_Num", poRequest.FulfillmentNumber);
                objCompHash.Add("@FromDate", poRequest.FulfillmentFromDate.ToShortDateString());
                objCompHash.Add("@ToDate", poRequest.FulfillmentToDate.ToShortDateString());
                objCompHash.Add("@ShipFromDate", poRequest.ShipFromDate.ToShortDateString());
                objCompHash.Add("@ShipToDate", poRequest.ShipToDate.ToShortDateString());
         
                objCompHash.Add("@StatusID", poRequest.FulfillmentOrderStatus);
                objCompHash.Add("@TrackingNumber", poRequest.TrackingNumber);
                objCompHash.Add("@UserID", userID);


                arrSpFieldSeq = new string[] { "@Po_Ids", "@DownloadFlag", "@PO_Num", "@FromDate", "@ToDate", "@ShipFromDate", "@ShipToDate", "@StatusID", "@TrackingNumber", "@UserID" };


                dt = db.GetTableRecords(objCompHash, "AV_PurchaseOrder_Download", arrSpFieldSeq);

                //posList = PopulateFulfillmentOrders(ds);
                FulfillmentOrdersListWithHeader purchaseOrder;
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dt.Rows)
                    {
                        purchaseOrder = new FulfillmentOrdersListWithHeader();
                        purchaseOrder.FulfillmentOrderNumber = (string)clsGeneral.getColumnData(dataRow, "PO_Num", string.Empty, false);
                        purchaseOrder.TrackingNumber = (string)clsGeneral.getColumnData(dataRow, "TrackingNumber", string.Empty, false);
                        purchaseOrder.StoreID = (string)clsGeneral.getColumnData(dataRow, "Store_ID", string.Empty, false);
                        purchaseOrder.AerovoiceSaleOrderNumber = (string)clsGeneral.getColumnData(dataRow, "AerovoiceSalesOrderNumber", string.Empty, false);
                        purchaseOrder.ContactName = (string)clsGeneral.getColumnData(dataRow, "shipto_attn", string.Empty, false);
                        purchaseOrder.ShipAddress = (string)clsGeneral.getColumnData(dataRow, "shipTo_address", string.Empty, false);
                        purchaseOrder.ShipCity = (string)clsGeneral.getColumnData(dataRow, "shipTo_city", string.Empty, false);
                        purchaseOrder.ShipState = (string)clsGeneral.getColumnData(dataRow, "shipTo_state", string.Empty, false);
                        purchaseOrder.ShipZip = (string)clsGeneral.getColumnData(dataRow, "shipTo_zip", string.Empty, false);
                        purchaseOrder.ShipBy = (string)clsGeneral.getColumnData(dataRow, "ship_via", string.Empty, false);
                        purchaseOrder.CustomerAccountNumber = (string)clsGeneral.getColumnData(dataRow, "CustomerAccountNumber", string.Empty, false);
                        purchaseOrder.FulfillmentOrderDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "po_date", DateTime.MinValue, false));

                        posList.Add(purchaseOrder);

                    }
                    
                }


                
            }

            catch (Exception objExp)
            {
                throw objExp;
            }
            finally
            {
                db.DBClose();
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;

            }

            return posList;
        }
        private static FulfillmentOrderListItemsResponse FulfillmentOrdersListItems(FulfillmentReportRequest poRequest, int userID)
        {

            FulfillmentOrderListItemsResponse serviceResponse = new FulfillmentOrderListItemsResponse();
            try
            {
                List<FulfillmentOrdersListItems> posList = FulfillmentOrdersListItemsDB(poRequest, userID, 0);
                if (posList != null && posList.Count > 0)
                {
                    serviceResponse.FulfillmentOrderList = posList;
                    serviceResponse.Comment = string.Empty;
                    serviceResponse.ReturnCode = ResponseErrorCode.SuccessfullyRetrieved.ToString();
                }
                else
                {
                    serviceResponse.FulfillmentOrderList = null;
                    serviceResponse.Comment = string.Empty;
                    serviceResponse.ReturnCode = ResponseErrorCode.NoRecordsFound.ToString();
                }


            }
            catch (Exception ex)
            {
                serviceResponse.FulfillmentOrderList = null;
                serviceResponse.Comment = ex.Message;
                serviceResponse.ReturnCode = ResponseErrorCode.InternalError.ToString();
            }
            //serviceResponse = DownloadPoHeader(pos);
            return serviceResponse;
        }
        private static List<FulfillmentOrdersListItems> FulfillmentOrdersListItemsDB(FulfillmentReportRequest poRequest, int userID, int downloadFlag)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            List<FulfillmentOrdersListItems> posList = null;
            DataSet ds = new DataSet();
            Hashtable objCompHash = new Hashtable();
            StringBuilder stringBuilder = new StringBuilder();
            try
            {
                objCompHash.Add("@Po_Ids", null);
                objCompHash.Add("@DownloadFlag", downloadFlag);
                objCompHash.Add("@PO_Num", poRequest.FulfillmentNumber);
                objCompHash.Add("@FromDate", poRequest.FulfillmentFromDate.ToShortDateString());
                objCompHash.Add("@ToDate", poRequest.FulfillmentToDate.ToShortDateString());
                objCompHash.Add("@ShipFromDate", poRequest.ShipFromDate.ToShortDateString());
                objCompHash.Add("@ShipToDate", poRequest.ShipToDate.ToShortDateString());
               
                objCompHash.Add("@StatusID", poRequest.FulfillmentOrderStatus);
                objCompHash.Add("@TrackingNumber", poRequest.TrackingNumber);
                objCompHash.Add("@UserID", userID);


                arrSpFieldSeq = new string[] { "@Po_Ids", "@DownloadFlag", "@PO_Num", "@FromDate", "@ToDate", "@ShipFromDate", "@ShipToDate", "@StatusID", "@TrackingNumber", "@UserID" };


                ds = db.GetDataSet(objCompHash, "AV_PurchaseOrder_Download", arrSpFieldSeq);

                posList = PopulateFulfillmentOrdersListItems(ds);


                //if (outParam == purchaseOrderNumber)
                //{
                //    rmaResponse.ErrorCode = string.Empty;
                //    rmaResponse.Comment = ResponseErrorCode.UpdatedSuccessfully.ToString();
                //}
                //else
                //{
                //    rmaResponse.ErrorCode = ResponseErrorCode.PurchaseOrderNotExists.ToString();
                //    rmaResponse.Comment = ResponseErrorCode.PurchaseOrderNotExists.ToString();
                //}
            }

            catch (Exception objExp)
            {
                //serviceResponse.Comment = ex.Message;
                //rmaResponse.ErrorCode = ResponseErrorCode.InconsistantData.ToString();
                //rmaResponse.Comment = objExp.Message.ToString();
                throw objExp;
            }
            finally
            {
                db.DBClose();
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;

            }

            return posList;
        }
        
        private static List<FulfillmentOrdersListItems> PopulateFulfillmentOrdersListItems(DataSet ds)
        {
            int poid = 0;
            List<FulfillmentOrdersListItems> purchaseOrdersList = null;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                purchaseOrdersList = new List<FulfillmentOrdersListItems>();
                FulfillmentOrdersListItems purchaseOrder = null;
                foreach (DataRow dataRow in ds.Tables[0].Rows)
                {

                    poid = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "PO_ID", 0, false));
                    if (ds.Tables.Count > 1 && ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                    {
                        foreach (DataRow dataRowItem in ds.Tables[1].Select("POID = " + poid.ToString()))
                        {
                            purchaseOrder = new Classes.FulfillmentOrdersListItems();
                            

                            purchaseOrder.FulfillmentOrderNumber = (string)clsGeneral.getColumnData(dataRow, "PO_Num", string.Empty, false);
                            purchaseOrder.TrackingNumber = (string)clsGeneral.getColumnData(dataRow, "TrackingNumber", string.Empty, false);
                            purchaseOrder.StoreID = (string)clsGeneral.getColumnData(dataRow, "Store_ID", string.Empty, false);
                            purchaseOrder.AerovoiceSaleOrderNumber = (string)clsGeneral.getColumnData(dataRow, "AerovoiceSalesOrderNumber", string.Empty, false);
                            //purchaseOrder.Shipping.AerovoiceSalesOrderNumber = (string)clsGeneral.getColumnData(dataRow, "AerovoiceSalesOrderNumber", string.Empty, false);

                            purchaseOrder.ESN = (string)clsGeneral.getColumnData(dataRowItem, "esn", string.Empty, true);
                            purchaseOrder.UPC = (string)clsGeneral.getColumnData(dataRowItem, "UPC", string.Empty, false);
                            purchaseOrder.ProductCode = (string)clsGeneral.getColumnData(dataRowItem, "ITEM_CODE", string.Empty, false);
                            purchaseOrder.MSLNumber = (string)clsGeneral.getColumnData(dataRowItem, "MSLNUMBER", string.Empty, false);
                            //purchaseOrder.MDN = (string)clsGeneral.getColumnData(dataRowItem, "mdn", string.Empty, false);
                            //purchaseOrder.MSID = (string)clsGeneral.getColumnData(dataRowItem, "msid", string.Empty, false);
                            //purchaseOrder.PassCode = (string)clsGeneral.getColumnData(dataRowItem, "Pass_code", string.Empty, false);
                            purchaseOrder.LTEICCID = (string)clsGeneral.getColumnData(dataRowItem, "LTEICCID", string.Empty, false);
                           // purchaseOrder.LTEIMSI = (string)clsGeneral.getColumnData(dataRowItem, "LTEIMSI", string.Empty, false);
                           // purchaseOrder.LTEIMSI = (string)clsGeneral.getColumnData(dataRowItem, "LTEIMSI", string.Empty, false);
                          //  purchaseOrder.SimNumber = (string)clsGeneral.getColumnData(dataRowItem, "SimNumber", string.Empty, false);

                            purchaseOrdersList.Add(purchaseOrder);
                        }
                    }

                    
                }
            }

            return purchaseOrdersList;
        }
        private static List<FulfillmentOrdersProvisional> PopulateFulfillmentOrdersProvisional(DataSet ds)
        {
            int poid = 0;
            List<FulfillmentOrdersProvisional> purchaseOrdersList = null;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                purchaseOrdersList = new List<FulfillmentOrdersProvisional>();
                FulfillmentOrdersProvisional purchaseOrder = null;
                foreach (DataRow dataRow in ds.Tables[0].Rows)
                {
                    poid = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "PO_ID", 0, false));
                    
                    if (ds.Tables.Count > 1 && ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                    {
                        foreach (DataRow dataRowItem in ds.Tables[1].Select("POID = " + poid.ToString()))
                        {
                            purchaseOrder = new Classes.FulfillmentOrdersProvisional();
                            

                            purchaseOrder.FulfillmentOrderNumber = (string)clsGeneral.getColumnData(dataRow, "PO_Num", string.Empty, false);
                            purchaseOrder.TrackingNumber = (string)clsGeneral.getColumnData(dataRow, "TrackingNumber", string.Empty, false);
                            purchaseOrder.StoreID = (string)clsGeneral.getColumnData(dataRow, "Store_ID", string.Empty, false);
                            purchaseOrder.AerovoiceSaleOrderNumber = (string)clsGeneral.getColumnData(dataRow, "AerovoiceSalesOrderNumber", string.Empty, false);
                            //purchaseOrder.Shipping.AerovoiceSalesOrderNumber = (string)clsGeneral.getColumnData(dataRow, "AerovoiceSalesOrderNumber", string.Empty, false);
                    
                            purchaseOrder.ESN = (string)clsGeneral.getColumnData(dataRowItem, "esn", string.Empty, true);
                            purchaseOrder.UPC = (string)clsGeneral.getColumnData(dataRowItem, "UPC", string.Empty, false);
                            purchaseOrder.ProductCode = (string)clsGeneral.getColumnData(dataRowItem, "ITEM_CODE", string.Empty, false);
                            purchaseOrder.BatchNumber = (string)clsGeneral.getColumnData(dataRowItem, "batchNumber", string.Empty, false);
                            purchaseOrder.LTEICCID = (string)clsGeneral.getColumnData(dataRowItem, "ICC_ID", string.Empty, false);
                           // purchaseOrder.LTEIMSI = (string)clsGeneral.getColumnData(dataRowItem, "LTEIMSI", string.Empty, false);
                           // purchaseOrder.Akey = (string)clsGeneral.getColumnData(dataRowItem, "akey", string.Empty, false);
                           // purchaseOrder.Otksl = (string)clsGeneral.getColumnData(dataRowItem, "otksl", string.Empty, false);
                          //  purchaseOrder.SimNumber = (string)clsGeneral.getColumnData(dataRowItem, "SimNumber", string.Empty, false);

                            purchaseOrdersList.Add(purchaseOrder);
                        }
                    }

                    
                }
            }

            return purchaseOrdersList;
        }

        
        //Called by webmethod
        public static CancelPurchaseOrderResponse CancelFulfillment(PurchaseOrderCancelRequest poRequest)
        {
            CancelPurchaseOrderResponse serviceResponse = new CancelPurchaseOrderResponse();
            serviceResponse.ErrorCode = ResponseErrorCode.MissingParameter.ToString();
            string requestXML = clsGeneral.SerializeObject(poRequest);

            LogModel request = new LogModel();
            request.RequestData = requestXML;
            request.ModuleName = "CancelFulfillment";
            request.RequestTimeStamp = DateTime.Now;
            Exception ex = null;

            try
            {

                if (poRequest != null)
                {

                    int userId = PurchaseOrder.AuthenticateRequest(poRequest.Authentication, out ex);
                    if (ex != null)
                    {
                        serviceResponse.Comment = ex.Message;
                        serviceResponse.ErrorCode = ResponseErrorCode.ErrowWhileLoadingData.ToString();

                        request.ResponseData = clsGeneral.SerializeObject(serviceResponse);
                        request.ResponseTimeStamp = DateTime.Now;
                        request.ExceptionOccured = true;
                        request.ReturnMessage = ResponseErrorCode.ErrowWhileLoadingData.ToString();
                        //LogOperations.ApiLogInsert(request);
                    }
                    if (userId > 0)
                    {
                        serviceResponse = CancelFulfillment(poRequest.PurchaseOrderNumber, userId);
                        request.ResponseData = clsGeneral.SerializeObject(serviceResponse);
                        request.ResponseTimeStamp = DateTime.Now;
                        request.ExceptionOccured = false;
                        request.ReturnMessage = serviceResponse.Comment;
                        request.UserID = userId;
                        request.CompanyID = 0;
                    }
                    else
                    {

                    }
                }
            }
            catch (Exception exc)
            {
                ex = exc;
                serviceResponse.Comment = ex.Message;
                serviceResponse.ErrorCode = ResponseErrorCode.InconsistantData.ToString();
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
        private static CancelPurchaseOrderResponse CancelFulfillment(string purchaseOrderNumber, int userID)
        {
            CancelPurchaseOrderResponse fulfillmentResponse = new CancelPurchaseOrderResponse();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            //DataTable dataTable = new DataTable();
            Hashtable objCompHash = new Hashtable();
            string outParam = string.Empty;
            int statusID = 0;
            try
            {
                objCompHash.Add("@PO_Num", purchaseOrderNumber);
                objCompHash.Add("@UserID", userID);
                arrSpFieldSeq = new string[] { "@PO_Num", "@UserID" };

                statusID = db.ExCommand(objCompHash, "Av_PurchaseOrder_Cancel", arrSpFieldSeq, "@outparam", out outParam);

                if (outParam == purchaseOrderNumber)
                {
                    fulfillmentResponse.ErrorCode = string.Empty;
                    fulfillmentResponse.Comment = ResponseErrorCode.CancelledSuccessfully.ToString();
                }
                else if (outParam == string.Empty && statusID == 0)
                {
                    fulfillmentResponse.ErrorCode = ResponseErrorCode.PurchaseOrderNotExists.ToString();
                    fulfillmentResponse.Comment = ResponseErrorCode.PurchaseOrderNotExists.ToString();
                }
                else
                {
                    fulfillmentResponse.ErrorCode = ResponseErrorCode.PurchaseOrderCannotBeCancelled.ToString();
                    fulfillmentResponse.Comment = "PurchaseOrder should be in pending or processed state for cancellation";
                }
            }
            catch (Exception objExp)
            {
                //serviceResponse.Comment = ex.Message;
                fulfillmentResponse.ErrorCode = ResponseErrorCode.InconsistantData.ToString();
                fulfillmentResponse.Comment = objExp.Message.ToString();
                throw objExp;
            }
            finally
            {
                db.DBClose();
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }
            return fulfillmentResponse;
        }
        
        public static PurchaseOrderResponse CreatePurchaseOrder(PurchaseOrderRequest purchaseOrderRequest, int userId)
        {
            PurchaseOrderResponse purchaseOrderResponse = new PurchaseOrderResponse();
            purchaseOrderResponse.ErrorCode = ResponseErrorCode.None.ToString();
            purchaseOrderResponse.PurchaseOrderNumber = purchaseOrderRequest.PurchaseOrder.PurchaseOrderNumber;
            try
            {
                if (userId > 0)
                {
                    if (purchaseOrderRequest.PurchaseOrder != null && purchaseOrderRequest.PurchaseOrder.PurchaseOrderItems != null && purchaseOrderRequest.PurchaseOrder.PurchaseOrderItems.Count > 0)
                    {
                        purchaseOrderResponse.Comment = "Purchase Order should have Items assigned";
                        purchaseOrderResponse.ErrorCode = ResponseErrorCode.PurchaseOrderItemNotAssigned.ToString();
                    }
                    else
                    {
                        if (ValidatePurchaseOrder(purchaseOrderRequest.PurchaseOrder, userId))
                        {
                            purchaseOrderResponse = SavePurchaseOrder(purchaseOrderRequest.PurchaseOrder, userId);
                        }
                        else
                        {
                            purchaseOrderResponse.Comment = "Purchase Order number already exists";
                        }
                    }
                }
                else
                {
                    purchaseOrderResponse.Comment = "Can not authenticate user";
                    purchaseOrderResponse.ErrorCode = ResponseErrorCode.CannotAuthenticateUser.ToString();
                }
            }
            catch (Exception ex)
            {
                purchaseOrderResponse.ErrorCode = ResponseErrorCode.InternalError.ToString();
                purchaseOrderResponse.Comment = ex.Message;
            }
            return purchaseOrderResponse;
        }

        public static PurchaseOrders GerPurchaseOrdersNew(string PONumber, string ContactName, string POFromDate, string POToDate, int UserID,
                                                       string statusID, int CompanyID, string esn, string AvOrder, string MslNumber, string PhoneCategory,
                                                       string ItemCode, string StoreID, string fmUPC, string zoneGUID, string shipFrom, string shipTo,
                                                       int PO_ID, string trackingNumber, string customerOrderNumber, string POType, int StockInDemand)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataSet ds = new DataSet();

            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@Po_Num", PONumber);
                objCompHash.Add("@Contact_Name", ContactName);
                objCompHash.Add("@From_Date", string.IsNullOrWhiteSpace(POFromDate) ? null : POFromDate);
                objCompHash.Add("@To_Date", string.IsNullOrWhiteSpace(POToDate) ? null : POToDate); 
                objCompHash.Add("@StatusID", statusID);
                objCompHash.Add("@UserID", UserID);
                objCompHash.Add("@CompanyID", CompanyID);
                objCompHash.Add("@esn", esn);
                objCompHash.Add("@AvOrderNumber", AvOrder);
                objCompHash.Add("@MslNumber", MslNumber);
                objCompHash.Add("@PhoneCategory", PhoneCategory);
                objCompHash.Add("@ItemCode", ItemCode);
                objCompHash.Add("@StoreID", StoreID);
                objCompHash.Add("@fmUPC", fmUPC);
                objCompHash.Add("@ShipFrom", string.IsNullOrWhiteSpace(shipFrom) ? null : shipFrom); //shipFrom);
                objCompHash.Add("@ShipTo",  string.IsNullOrWhiteSpace(shipTo) ? null : shipTo); //shipTo);
                
                objCompHash.Add("@POID", PO_ID);
                objCompHash.Add("@TrackingNumber", trackingNumber);
                objCompHash.Add("@CustomerOrderNumber", customerOrderNumber);
                objCompHash.Add("@POType", POType);
                objCompHash.Add("@StockInDemand", StockInDemand);

                if (!string.IsNullOrEmpty(zoneGUID))
                {
                    objCompHash.Add("@ZoneGUID", zoneGUID);
                }

                arrSpFieldSeq = new string[] { "@Po_Num", "@Contact_Name", "@From_Date", "@To_Date", "@StatusID", "@UserID", "@CompanyID", "@esn",
                    "@AvOrderNumber", "@MslNumber", "@PhoneCategory", "@ItemCode", "@StoreID", "@fmUPC", "@ZoneGUID", "@ShipFrom", "@ShipTo",
                    "@TrackingNumber","@CustomerOrderNumber","@POType", "@POID","@StockInDemand" };
                ds = db.GetDataSet(objCompHash, "Aero_PurchaseOrder_Select_New", arrSpFieldSeq);

                return PopulatePurchaseOrdersNew(ds);
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db.DBClose();
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }
        }
        public static PurchaseOrders GerPurchaseOrdersView(string PONumber, string companyAccountNumber)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataSet ds = new DataSet();

            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@piPoNum", PONumber);
                objCompHash.Add("@piCompanyAccountNumber", companyAccountNumber);
                

                arrSpFieldSeq = new string[] { "@piPoNum", "@piCompanyAccountNumber" };
                ds = db.GetDataSet(objCompHash, "Av_PurchaseOrder_View", arrSpFieldSeq);

                return PopulatePurchaseOrdersNew(ds);
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db.DBClose();
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }
        }
        public static APIAddressInfo GetCustomerAPIAddress(int companyID, string apiMethodName)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataTable dt = new DataTable();
            APIAddressInfo aPIAddressInfo = null;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@ComapanyID", companyID);
                objCompHash.Add("@APIMethod", apiMethodName);

                arrSpFieldSeq = new string[] { "@ComapanyID", "@APIMethod" };
                dt = db.GetTableRecords(objCompHash, "av_CustomerAPIAddress_Select", arrSpFieldSeq);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dataRowItem in dt.Rows)
                    {
                        
                        aPIAddressInfo = new APIAddressInfo();
                        aPIAddressInfo.APIAddress = (string)clsGeneral.getColumnData(dataRowItem, "APIAddress", string.Empty, false);
                        aPIAddressInfo.ConnectionString = (string)clsGeneral.getColumnData(dataRowItem, "UserName", string.Empty, false);
                    }
                }
                return aPIAddressInfo;
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db.DBClose();
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }
        }

        public static List<BasePurchaseOrderItem> GetPurchaseOrderItemsAndTrackingList(int PO_ID, out List<TrackingDetail> trackingList)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataSet ds = new DataSet();
            trackingList = new List<TrackingDetail>();
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@POID", PO_ID);

                arrSpFieldSeq = new string[] { "@POID" };
                ds = db.GetDataSet(objCompHash, "Aero_PurchaseOrder_Detail_Select", arrSpFieldSeq);
                if (ds != null && ds.Tables.Count > 1)
                {
                    trackingList = PopulateTrackingList(ds.Tables[1]);
                }
                return PopulatePurchaseOrderItemList(ds.Tables[0]);
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db.DBClose();
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }
        }

        public static List<FulfillmentSKUs> GetPurchaseSKUList(int PO_ID, out List<POEsn> esnList)
        {
            List<FulfillmentSKUs> fulfillmentSKUs = new List<FulfillmentSKUs>();
            esnList = new List<POEsn>();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataSet ds = new DataSet();

            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@POID", PO_ID);

                arrSpFieldSeq = new string[] { "@POID" };
                ds = db.GetDataSet(objCompHash, "Av_PurchaseOrder_AssignedESN_Select", arrSpFieldSeq);
                if (ds != null && ds.Tables.Count > 0)
                {
                    fulfillmentSKUs = PopulateFufillmentSKUs(ds.Tables[0]);
                    if (ds != null && ds.Tables.Count > 1)
                        esnList = PopulateESNs(ds.Tables[1]);
                    
                }
                return fulfillmentSKUs;
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db.DBClose();
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }
        }
        private static List<POEsn> PopulateESNs(DataTable dt)
        {
            List<POEsn> esnList = new List<POEsn>();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dataRow in dt.Rows)
                {
                    POEsn esn = new POEsn();
                    esn.ESN = clsGeneral.getColumnData(dataRow, "ESN", string.Empty, false) as string;
                    esn.ICCID = "";// clsGeneral.getColumnData(dataRow, "ICC_ID", string.Empty, false) as string;
                    esn.ContainerID = clsGeneral.getColumnData(dataRow, "ContainerID", string.Empty, false) as string;
                    esn.PalletID = clsGeneral.getColumnData(dataRow, "PalletID", string.Empty, false) as string;
                    esn.BoxID = clsGeneral.getColumnData(dataRow, "BoxID", string.Empty, false) as string;
                    esn.Hex = clsGeneral.getColumnData(dataRow, "ContainerID", string.Empty, false) as string;
                    esn.Dec = clsGeneral.getColumnData(dataRow, "ContainerID", string.Empty, false) as string;
                    esn.SerialNumber = clsGeneral.getColumnData(dataRow, "SerialNumber", string.Empty, false) as string;

                    esnList.Add(esn);
                }
            }
            return esnList;
        }

        public static List<BasePurchaseOrderItem> GetPurchaseOrderItemList(int PO_ID)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataTable dt = new DataTable();

            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@POID", PO_ID);

                arrSpFieldSeq = new string[] { "@POID" };
                dt = db.GetTableRecords(objCompHash, "Av_PurchaseOrder_Assign_Select", arrSpFieldSeq);

                return PopulatePurchaseOrderItemList(dt);
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db.DBClose();
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }
        }
        public static List<BasePurchaseOrderItem> GetPurchaseOrderESNList(int PO_ID)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataTable dt = new DataTable();

            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@POID", PO_ID);

                arrSpFieldSeq = new string[] { "@POID" };
                dt = db.GetTableRecords(objCompHash, "Av_PurchaseOrder_Assign_Select", arrSpFieldSeq);

                return PopulatePurchaseOrderItemList(dt);
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db.DBClose();
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }
        }
        public static List<BasePurchaseOrderItem> GetPurchaseOrderInventorySummary(string PONumber, string ContactName, string POFromDate, string POToDate, int UserID,
                                                       string statusID, int CompanyID, string esn, string trackingNumber, string MslNumber,
                                                       string PhoneCategory, string ItemCode, string StoreID, string POType, string customerOrderNumber,
                                                       string shipFrom, string shipTo, int PO_ID, out List<FulfillmentStatus> statusList)
        {
            List<BasePurchaseOrderItem> itemList=null;// = new List<BasePurchaseOrderItem>();
            statusList = null;
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataSet ds = new DataSet();
            //List<FulfillmentStatus> statusList = new List<FulfillmentStatus>();
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@Po_Num", PONumber);
                objCompHash.Add("@Contact_Name", ContactName);
                objCompHash.Add("@From_Date", POFromDate);
                objCompHash.Add("@To_Date", POToDate);
                objCompHash.Add("@StatusID", statusID);
                objCompHash.Add("@UserID", UserID);
                objCompHash.Add("@CompanyID", CompanyID);
                objCompHash.Add("@esn", esn);
                objCompHash.Add("@TrackingNumber", trackingNumber);
                objCompHash.Add("@MslNumber", MslNumber);
                objCompHash.Add("@PhoneCategory", PhoneCategory);
                objCompHash.Add("@ItemCode", ItemCode);
                objCompHash.Add("@StoreID", StoreID);
                objCompHash.Add("@POType", POType);
                objCompHash.Add("@customerOrderNumber", customerOrderNumber);
                objCompHash.Add("@ShipFrom", shipFrom);
                objCompHash.Add("@ShipTo", shipTo);
                objCompHash.Add("@POID", PO_ID);
                
                //if (!string.IsNullOrEmpty(zoneGUID))
               // {
                //    objCompHash.Add("@ZoneGUID", zoneGUID);
                //}

                arrSpFieldSeq = new string[] { "@Po_Num", "@Contact_Name", "@From_Date", "@To_Date", "@StatusID", "@UserID", "@CompanyID", "@esn", "@TrackingNumber", "@MslNumber", 
                    "@PhoneCategory", "@ItemCode", "@StoreID", "@POType", "@customerOrderNumber", "@ShipFrom", "@ShipTo", "@POID" };

                ds = db.GetDataSet(objCompHash, "AV_PurchaseOrderInventory_Sumarry", arrSpFieldSeq);
                if(ds != null && ds.Tables.Count>0 && ds.Tables[0].Rows.Count>0)
                    itemList = PopulatePurchaseOrderItemSummary(ds.Tables[0]);
                if (ds != null && ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                    statusList = PopulateFulfillmentStatusList(ds.Tables[1]);
                
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db.DBClose();
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }
            return itemList;
        }

       

        public static PurchaseOrderShipResponse GetPurchaseOrderShipping(PurchaseOrderShipRequest purchaseOrderShipRequest)
        {
            PurchaseOrderShipResponse purchaseOrderShipResponse = new PurchaseOrderShipResponse();
            Exception exc = null;
            try
            {

                int userId = AuthenticateRequest(purchaseOrderShipRequest.Authentication, out exc);
                if (userId > 0)
                {
                    purchaseOrderShipResponse.ShipInfo = GetPurchaseOrderShippingDB(purchaseOrderShipRequest.PurchaseOrderNumber, userId);
                    if (purchaseOrderShipResponse.ShipInfo != null)
                    {
                        if (string.IsNullOrEmpty(purchaseOrderShipResponse.ShipInfo.ShipToTrackingNumber))
                        {
                            purchaseOrderShipResponse.Comment = "";
                            purchaseOrderShipResponse.ErrorCode = ResponseErrorCode.PurchaseOrderNotShipped.ToString();
                        }
                        else
                        {
                            purchaseOrderShipResponse.ErrorCode = ResponseErrorCode.PurchaseOrderShipped.ToString();
                        }
                    }
                    else
                    {
                        purchaseOrderShipResponse.ShipInfo = null;
                        purchaseOrderShipResponse.Comment = "Purchase Order does not exists";
                        purchaseOrderShipResponse.ErrorCode = ResponseErrorCode.PurchaseOrderNotExists.ToString();
                    }
                }
                else
                {
                    purchaseOrderShipResponse.ShipInfo = null;
                    purchaseOrderShipResponse.Comment = "Cannot authenticate user";
                    purchaseOrderShipResponse.ErrorCode = ResponseErrorCode.CannotAuthenticateUser.ToString();
                }

            }
            catch (Exception ex)
            {
                purchaseOrderShipResponse.ShipInfo = null;
                purchaseOrderShipResponse.Comment = ex.Message;
                purchaseOrderShipResponse.ErrorCode = ResponseErrorCode.InconsistantData.ToString();
            }


            return purchaseOrderShipResponse;
        }

        public static PurchaseOrderInfoResponse GerPurchaseOrder(PurchaseOrderInfoRequest purchaseOrderRequest)
        {
            PurchaseOrderInfoResponse serviceResponse = new PurchaseOrderInfoResponse();
            serviceResponse.ErrorCode = ResponseErrorCode.None.ToString();
            Exception exc = null;
            try
            {
                int userId = AuthenticateRequest(purchaseOrderRequest.Authentication, out exc);
                if (userId > 0)
                {
                    if (!string.IsNullOrEmpty(purchaseOrderRequest.PurchaseOrderNumber) || !string.IsNullOrEmpty(purchaseOrderRequest.ESN))
                    {
                        DBConnect db = new DBConnect();
                        string[] arrSpFieldSeq;
                        DataSet ds = new DataSet();

                        Hashtable objCompHash = new Hashtable();
                        try
                        {
                            objCompHash.Add("@Po_Num", purchaseOrderRequest.PurchaseOrderNumber);
                            objCompHash.Add("@UserID", userId);
                            objCompHash.Add("@esn", purchaseOrderRequest.ESN);


                            arrSpFieldSeq = new string[] { "@Po_Num", "@UserID", "@esn" };
                            ds = db.GetDataSet(objCompHash, "Aero_PurchaseOrder_Select", arrSpFieldSeq);

                            avii.Classes.clsPurchaseOrder po = PopulatePurchaseOrder(ds);
                            if (po != null)
                            {
                                serviceResponse.PurchaseOrder = po;
                                //serviceResponse.ErrorCode = ResponseErrorCode.SuccessfullyRetrieved.ToString();
                            }
                            else
                            {
                                serviceResponse.ErrorCode = ResponseErrorCode.PurchaseOrderNotExists.ToString();
                            }
                        }
                        catch (Exception objExp)
                        {
                            throw new Exception(objExp.Message.ToString());
                        }
                        finally
                        {
                            db.DBClose();
                            db = null;
                            objCompHash = null;
                            arrSpFieldSeq = null;
                        }
                    }
                    else
                    {
                        serviceResponse.Comment = "Search criteria required";
                        serviceResponse.ErrorCode = ResponseErrorCode.MissingParameter.ToString();
                    }
                }
                else
                {
                    serviceResponse.Comment = "Cannot authenticate user";
                    serviceResponse.ErrorCode = ResponseErrorCode.CannotAuthenticateUser.ToString();
                }
            }
            catch (Exception ex)
            {
                serviceResponse.PurchaseOrder = null;
                serviceResponse.Comment = ex.Message;
                serviceResponse.ErrorCode = ResponseErrorCode.InconsistantData.ToString();
            }
        
            return serviceResponse;
        }
        public static PurchaseOrderInfoResponse GerPurchaseOrderAPI(PurchaseOrderInfoRequest purchaseOrderRequest)
        {
            PurchaseOrderInfoResponse serviceResponse = new PurchaseOrderInfoResponse();
            serviceResponse.ErrorCode = ResponseErrorCode.None.ToString();
            string requestXML = clsGeneral.SerializeObject(purchaseOrderRequest);

            LogModel request = new LogModel();
            request.RequestData = requestXML;
            request.ModuleName = "GetPurchaseOrder";
            request.RequestTimeStamp = DateTime.Now;
            Exception ex = null;
            try
            {
                int userId = AuthenticateRequest(purchaseOrderRequest.Authentication, out ex);
                if (ex != null)
                {
                    serviceResponse.Comment = ex.Message;
                    serviceResponse.ErrorCode = ResponseErrorCode.ErrowWhileLoadingData.ToString();
                    serviceResponse.PurchaseOrder = null;
                    request.ResponseData = clsGeneral.SerializeObject(serviceResponse);
                    request.ResponseTimeStamp = DateTime.Now;
                    request.ExceptionOccured = true;
                    request.ReturnMessage = ResponseErrorCode.ErrowWhileLoadingData.ToString();
                    //LogOperations.ApiLogInsert(request);
                }
                else
                {

                    if (userId > 0)
                    {
                        if (!string.IsNullOrEmpty(purchaseOrderRequest.PurchaseOrderNumber) || !string.IsNullOrEmpty(purchaseOrderRequest.ESN))
                        {
                            DBConnect db = new DBConnect();
                            string[] arrSpFieldSeq;
                            DataSet ds = new DataSet();

                            Hashtable objCompHash = new Hashtable();
                            try
                            {
                                objCompHash.Add("@Po_Num", purchaseOrderRequest.PurchaseOrderNumber);
                                objCompHash.Add("@UserID", userId);
                                objCompHash.Add("@esn", purchaseOrderRequest.ESN);


                                arrSpFieldSeq = new string[] { "@Po_Num", "@UserID", "@esn" };
                                ds = db.GetDataSet(objCompHash, "Aero_PurchaseOrder_API_Select", arrSpFieldSeq);

                                avii.Classes.clsPurchaseOrder po = PopulatePurchaseOrder(ds);
                                if (po != null)
                                {
                                    serviceResponse.PurchaseOrder = po;

                                    serviceResponse.Comment = ResponseErrorCode.SuccessfullyRetrieved.ToString();
                                }
                                else
                                {
                                    serviceResponse.ErrorCode = ResponseErrorCode.PurchaseOrderNotExists.ToString();
                                }

                                
                            }
                            catch (Exception objExp)
                            {
                                ex = objExp;
                                serviceResponse.Comment = ex.Message;
                                serviceResponse.ErrorCode = ResponseErrorCode.ErrowWhileLoadingData.ToString();
                                serviceResponse.PurchaseOrder = null;
                                request.ResponseData = clsGeneral.SerializeObject(serviceResponse);
                                request.ResponseTimeStamp = DateTime.Now;
                                request.ExceptionOccured = true;
                                request.ReturnMessage = ResponseErrorCode.ErrowWhileLoadingData.ToString();
                              //  LogOperations.ApiLogInsert(request);
                                throw new Exception(objExp.Message.ToString());
                            }
                            finally
                            {
                                db.DBClose();
                                db = null;
                                objCompHash = null;
                                arrSpFieldSeq = null;
                            }
                        }
                        else
                        {
                            serviceResponse.Comment = "Search criteria required";
                            serviceResponse.ErrorCode = ResponseErrorCode.MissingParameter.ToString();
                        }
                    }
                    else
                    {
                        serviceResponse.Comment = "Cannot authenticate user";
                        serviceResponse.ErrorCode = ResponseErrorCode.CannotAuthenticateUser.ToString();
                    }
                    request.UserID = userId;
                    request.CompanyID = 0;
                    request.ResponseData = clsGeneral.SerializeObject(serviceResponse);
                    request.ResponseTimeStamp = DateTime.Now;
                    request.ExceptionOccured = false;
                    request.ReturnMessage = serviceResponse.Comment;
                    
                }
            }
            catch (Exception exc)
            {
                ex = exc;
                serviceResponse.Comment = ex.Message;
                serviceResponse.ErrorCode = ResponseErrorCode.ErrowWhileLoadingData.ToString();
                serviceResponse.PurchaseOrder = null;
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

        public static ProductResponse GetProducts()
        {
            ProductResponse serviceResponse = new ProductResponse();
            serviceResponse.ReturnCode = ResponseErrorCode.None.ToString();
            try
            {
                List<avii.Classes.Brands> productList = SKUOperations.GetProducts();
                if (productList != null && productList.Count > 0)
                {
                    serviceResponse.ProductList = productList;
                    serviceResponse.ReturnCode = string.Empty;//ResponseErrorCode.SuccessfullyRetrieved.ToString();
                }
                else
                {
                    serviceResponse.ReturnCode = ResponseErrorCode.NoRecordsFound.ToString();
                }
            }
            catch (Exception ex)
            {
                serviceResponse.ProductList = null;
                serviceResponse.Comment = ex.Message;
                serviceResponse.ReturnCode = ResponseErrorCode.InconsistantData.ToString();
            }

            return serviceResponse;
        }
        
        public static CustomerSKUResponse GetAssignedSKU(string AccountNumber)
        {
            CustomerSKUResponse serviceResponse = new CustomerSKUResponse();
            serviceResponse.ReturnCode = ResponseErrorCode.None.ToString();
            try
            {
                int returnValue = 0;
                List<avii.Classes.CustomerSKUs> skuList = SKUOperations.GetSKUs(AccountNumber, out returnValue);
                if (skuList != null && skuList.Count > 0)
                {
                    serviceResponse.SKUList = skuList;
                    serviceResponse.ReturnCode = string.Empty;//ResponseErrorCode.SuccessfullyRetrieved.ToString();
                }
                else
                {
                    if (returnValue == -1)
                    {
                        serviceResponse.Comment = "Wrong AccountNumber";
                        serviceResponse.ReturnCode = ResponseErrorCode.AccountNumberNotExists.ToString();
                    }
                    else
                        serviceResponse.ReturnCode = ResponseErrorCode.NoRecordsFound.ToString();
                }
            }
            catch (Exception ex)
            {
                serviceResponse.SKUList = null;
                serviceResponse.Comment = ex.Message;
                serviceResponse.ReturnCode = ResponseErrorCode.InconsistantData.ToString();
            }

            return serviceResponse;
        }
        public static ShippByResponse GetShippingCodes()
        {
            ShippByResponse serviceResponse = new ShippByResponse();
            serviceResponse.ErrorCode = ResponseErrorCode.None.ToString();
            string requestXML = string.Empty;// clsGeneral.SerializeObject(serviceRequest);

            LogModel request = new LogModel();
            request.RequestData = requestXML;
            request.ModuleName = "GetShippingCodes";
            request.RequestTimeStamp = DateTime.Now;
            Exception ex = null;

            try
            {
                List<avii.Classes.ShipBy> shippByList = GetShipByList();
                if (shippByList != null && shippByList.Count > 0)
                {
                    serviceResponse.ShipByList = shippByList;
                    serviceResponse.ErrorCode = string.Empty;//ResponseErrorCode.SuccessfullyRetrieved.ToString();
                }
                else
                {
                    serviceResponse.ShipByList = null;

                    serviceResponse.ErrorCode = ResponseErrorCode.NoRecordsFound.ToString();
                }
                request.ResponseData = clsGeneral.SerializeObject(serviceResponse);
                request.ResponseTimeStamp = DateTime.Now;
                request.ExceptionOccured = false;
                request.ReturnMessage = serviceResponse.ErrorCode;
                request.CompanyID = 0;
                request.UserID = 0;

            }
            catch (Exception exc)
            {
                ex = exc;
                serviceResponse.ShipByList = null;
                serviceResponse.Comment = ex.Message;
                serviceResponse.ErrorCode = ResponseErrorCode.InconsistantData.ToString();
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

        public static FulfillmentOrderResponse GerFulfillmentOrder(avii.Classes.FulfillmentOrderRequest orderRequest)
        {
            FulfillmentOrderResponse serviceResponse = new FulfillmentOrderResponse();
            serviceResponse.ErrorCode = ResponseErrorCode.MissingParameter.ToString();
            Exception exc = null;
            try
            {  
                if (orderRequest != null)
                {
                    string poNum = orderRequest.PurchaseOrderNumber;
                    int userId = AuthenticateRequest(orderRequest.Authentication, out exc);
                    if (userId > 0)
                    {
                        if (!string.IsNullOrEmpty(poNum) || !string.IsNullOrEmpty(orderRequest.ESN))
                        {
                            DBConnect db = new DBConnect();
                            string[] arrSpFieldSeq;
                            DataSet ds = new DataSet();

                            Hashtable objCompHash = new Hashtable();
                            try
                            {
                                objCompHash.Add("@Po_Num", poNum);
                                objCompHash.Add("@UserID", userId);
                                objCompHash.Add("@esn", orderRequest.ESN);

                                arrSpFieldSeq = new string[] { "@Po_Num", "@UserID", "@esn" };
                                ds = db.GetDataSet(objCompHash, "Aero_PurchaseOrder_Select", arrSpFieldSeq);

                                avii.Classes.FulfillmentOrder po = PopulateFulfillmentOrder(ds);
                                if (po != null)
                                {
                                    serviceResponse.PurchaseOrder = po;
                                    serviceResponse.ErrorCode = ResponseErrorCode.SuccessfullyRetrieved.ToString();
                                }
                                else
                                    serviceResponse.ErrorCode = ResponseErrorCode.NoRecordsFound.ToString();
                            }
                            catch (Exception objExp)
                            {
                                throw new Exception(objExp.Message.ToString());
                            }
                            finally
                            {
                                db.DBClose();
                                db = null;
                                objCompHash = null;
                                arrSpFieldSeq = null;
                            }
                        }
                        else
                        {
                            serviceResponse.Comment = "Search criteria required";
                            serviceResponse.ErrorCode = ResponseErrorCode.MissingParameter.ToString();
                        }
                    }
                    else
                    {
                        serviceResponse.Comment = "Cannot authenticate user";
                        serviceResponse.ErrorCode = ResponseErrorCode.CannotAuthenticateUser.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                serviceResponse.PurchaseOrder = null;
                serviceResponse.Comment = ex.Message;
                serviceResponse.ErrorCode = ResponseErrorCode.InconsistantData.ToString();
            }
            return serviceResponse;
        }

        public static InventoryResponse GetInventoryList(InventoryRequest inventoryRequest)
        {
            InventoryResponse serviceResponse = new InventoryResponse();
            serviceResponse.ErrorCode = ResponseErrorCode.MissingParameter.ToString();
            serviceResponse.InventoryList = null;

            string requestXML = clsGeneral.SerializeObject(inventoryRequest);
            
            LogModel request = new LogModel();
            request.RequestData = requestXML;
            request.ModuleName = "GetInventorySKU";
            request.RequestTimeStamp = DateTime.Now;
            Exception ex = null;

            try
            {

                if (inventoryRequest != null)
                {

                    int userId = AuthenticateRequest(inventoryRequest.Authentication, out ex);
                    if (ex != null)
                    {
                        serviceResponse.Comment = ex.Message;
                        serviceResponse.ErrorCode = ResponseErrorCode.ErrowWhileLoadingData.ToString();

                        request.ResponseData = clsGeneral.SerializeObject(serviceResponse);
                        request.ResponseTimeStamp = DateTime.Now;
                        request.ExceptionOccured = true;
                        request.ReturnMessage = ResponseErrorCode.ErrowWhileLoadingData.ToString();
                        
                    }
                    if (userId > 0)
                    {

                        List<InventorySKU> inventoryItemList = GetInventorySKUList(userId);

                        if (inventoryItemList != null && inventoryItemList.Count > 0)
                        {
                            serviceResponse.InventoryList = inventoryItemList;
                            serviceResponse.ErrorCode = string.Empty;
                        }
                        else
                            serviceResponse.ErrorCode = "SKU not assigned yet";
                    }
                    else
                    {
                        serviceResponse.Comment = "Cannot authenticate user";
                        serviceResponse.ErrorCode = ResponseErrorCode.CannotAuthenticateUser.ToString();
                    }
                    request.UserID = userId;
                    request.CompanyID = 0;
                    request.ResponseData = clsGeneral.SerializeObject(serviceResponse);
                    request.ResponseTimeStamp = DateTime.Now;
                    request.ExceptionOccured = false;
                    request.ReturnMessage = serviceResponse.Comment;

                }
            }
            catch (Exception exc)
            {
                ex = exc;
                serviceResponse.InventoryList = null;
                serviceResponse.Comment = ex.Message;
                serviceResponse.ErrorCode = ResponseErrorCode.InconsistantData.ToString();
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
        
        public static ServiceResponse ReportBadInventoryItem(InventoryBadItemRequest inventory)
        {
            int userId = 0;
            ServiceResponse serviceResponse = new ServiceResponse();
            Exception exc = null;
            try
            {
                serviceResponse.ErrorCode = string.Empty;
                serviceResponse.Comment = "acknowledged";
                userId = AuthenticateRequest(inventory.Authentication, out exc);
                if (userId > 0)
                {
                    ReportBadInventoryItemDB(inventory.InventoryItems, userId);
                }
                else
                {
                    serviceResponse.Comment = "Cannot authenticate user";
                    serviceResponse.ErrorCode = ResponseErrorCode.CannotAuthenticateUser.ToString();
                }
            }
            catch (Exception ex)
            {
                serviceResponse.ErrorCode = ResponseErrorCode.InternalError.ToString();
                serviceResponse.Comment = ex.Message;
            }

            return serviceResponse;
        }

        private static DataSet GerPurchaseOrders(string PONumber, string ContactName, string POFromDate, string POToDate, int UserID)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataSet ds = new DataSet();

            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@Po_Num", PONumber);
                objCompHash.Add("@Contact_Name", ContactName);
                objCompHash.Add("@From_Date", POFromDate);
                objCompHash.Add("@To_Date", POToDate);

                objCompHash.Add("@UserID", UserID);

                arrSpFieldSeq = new string[] { "@Po_Num", "@Contact_Name", "@From_Date", "@To_Date", "@UserID" };
                ds = db.GetDataSet(objCompHash, "Aero_PurchaseOrder_Select", arrSpFieldSeq);

                return ds;
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db.DBClose();
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }
        }
        public static List<BasePurchaseOrder> GetPurchaseOrderHistory(int poID)
        {
            List<BasePurchaseOrder> poStatusHistory = new List<BasePurchaseOrder>();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataTable dt = new DataTable();
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@PO_ID", poID);
                arrSpFieldSeq = new string[] { "@PO_ID" };
                dt = db.GetTableRecords(objCompHash, "AV_PurchaseOrder_History", arrSpFieldSeq);

                BasePurchaseOrder purchaseOrder = null;
                foreach (DataRow dataRow in dt.Rows)
                {

                    purchaseOrder = new BasePurchaseOrder(Convert.ToInt32(clsGeneral.getColumnData(dataRow, "PO_ID", 0, true)),
                        string.Empty);
                    //purchaseOrder.StatusColor = (string)clsGeneral.getColumnData(dataRow, "StatusColor", string.Empty, false);

                    purchaseOrder.PurchaseOrderDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "PO_Date", DateTime.Now, true));
                    purchaseOrder.PurchaseOrderNumber = (string)clsGeneral.getColumnData(dataRow, "PO_Num", string.Empty, false);
                    purchaseOrder.SentASN = (string)clsGeneral.getColumnData(dataRow, "PoDataXML", string.Empty, false);
                    purchaseOrder.Comments = (string)clsGeneral.getColumnData(dataRow, "Comments", string.Empty, false);

                    purchaseOrder.PurchaseOrderStatus = (avii.Classes.PurchaseOrderStatus)Convert.ToInt16(clsGeneral.getColumnData(dataRow, "POStatus", 1, false));
                    //purchaseOrder.PurchaseOrderStatusID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "POStatus", 0, false));
                    purchaseOrder.ModifiedDate = (string)clsGeneral.getColumnData(dataRow, "ModifiedDate", string.Empty, false);
                    purchaseOrder.CustomerName = (string)clsGeneral.getColumnData(dataRow, "Username", string.Empty, false);
                    purchaseOrder.SentESN = (string)clsGeneral.getColumnData(dataRow, "POFlag", string.Empty, false);
                    poStatusHistory.Add(purchaseOrder);
                }
                //return PopulatePurchaseOrders(ds);
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db.DBClose();
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }
            return poStatusHistory;
        }
        public static PurchaseOrders GerPurchaseOrders(string PONumber, string ContactName, string POFromDate, string POToDate, int UserID, 
                                                        string statusID, int CompanyID, string esn, string AvOrder, string MslNumber,
                                                        string PhoneCategory, string ItemCode, string StoreID, string fmUPC, string zoneGUID,
                                                        string shipFrom, string shipTo, int PO_ID)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataSet ds = new DataSet();

            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@Po_Num", PONumber);
                objCompHash.Add("@Contact_Name", ContactName);
                objCompHash.Add("@From_Date", POFromDate);
                objCompHash.Add("@To_Date", POToDate);
                objCompHash.Add("@StatusID", statusID);
                objCompHash.Add("@UserID", UserID);
                objCompHash.Add("@CompanyID", CompanyID);
                objCompHash.Add("@esn", esn);
                objCompHash.Add("@AvOrderNumber", AvOrder);
                objCompHash.Add("@MslNumber", MslNumber);
                objCompHash.Add("@PhoneCategory", PhoneCategory);
                objCompHash.Add("@ItemCode", ItemCode);
                objCompHash.Add("@StoreID", StoreID);
                objCompHash.Add("@fmUPC", fmUPC);
                objCompHash.Add("@ShipFrom", shipFrom);
                objCompHash.Add("@ShipTo", shipTo);
                objCompHash.Add("@POID", PO_ID);
                if (!string.IsNullOrEmpty(zoneGUID))
                {
                    objCompHash.Add("@ZoneGUID", zoneGUID);
                }

                arrSpFieldSeq = new string[] { "@Po_Num", "@Contact_Name", "@From_Date", "@To_Date", "@StatusID", "@UserID", "@CompanyID", "@esn", "@AvOrderNumber", "@MslNumber", "@PhoneCategory", "@ItemCode", "@StoreID", "@fmUPC", "@ZoneGUID", "@ShipFrom", "@ShipTo", "@POID" };
                ds = db.GetDataSet(objCompHash, "Aero_PurchaseOrder_Select", arrSpFieldSeq);

                return PopulatePurchaseOrders(ds);
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db.DBClose();
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }
        }


        public static PurchaseOrders GerSelectedPurchaseOrders(string POIds, int downloadFlag)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataSet ds = new DataSet();

            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@Po_Ids", POIds);
                objCompHash.Add("@DownloadFlag", downloadFlag);

                arrSpFieldSeq = new string[] { "@Po_Ids", "@DownloadFlag" };
                ds = db.GetDataSet(objCompHash, "AV_PurchaseOrder_Download", arrSpFieldSeq);

                return PopulatePurchaseOrdersDownload(ds);
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db.DBClose();
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }
        }


        public static DataTable GerPurchaseOrders_WithESN(string PONumber, string POFromDate, string POToDate, int UserID, int CompanyID, 
                                                 int statusId, string esn, string MslNumber)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataTable table = new DataTable();

            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@Po_Num", PONumber);
                objCompHash.Add("@From_Date", POFromDate);
                objCompHash.Add("@To_Date", POToDate);
                objCompHash.Add("@StatusID", statusId);
                objCompHash.Add("@CompanyID", CompanyID);
                objCompHash.Add("@UserID", UserID);
                objCompHash.Add("@esn", esn);
                objCompHash.Add("@MslNumber", MslNumber);

                arrSpFieldSeq = new string[] { "@Po_Num", "@From_Date", "@To_Date", "@StatusID", "@CompanyID", "@UserID", "@esn", "@MslNumber"};
                table = db.GetTableRecords(objCompHash, "Aero_PurchaseOrder_Items_Select", arrSpFieldSeq);

                return table;
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db.DBClose();
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }
        }

        public static void SetESNServiceLogging(int PodID, string PoNum, string ESN, string MslNumber, string returnCode, string returnMessage, string comment)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            string sCode = string.Empty;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@POD_ID", PodID);
                objCompHash.Add("@PoNum", PoNum);
                objCompHash.Add("@ESN", ESN);
                objCompHash.Add("@MslNumber", MslNumber);
                objCompHash.Add("@ReturnCode", returnCode);
                objCompHash.Add("@ReturnMessage", returnMessage);
                objCompHash.Add("@Comment", comment);
                arrSpFieldSeq = new string[] { "@POD_ID", "@PoNum", "@ESN", "@MslNumber", "@ReturnCode", "@ReturnMessage", "@Comment" };
                db.ExeCommand(objCompHash, "Aero_ESNService_Insert", arrSpFieldSeq);

            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                objCompHash = null;
                arrSpFieldSeq = null;
            }
        }


        public static PurchaseOrders GerPurchaseOrders(string PONumber, int CompanyID)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataSet ds = new DataSet();

            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@Po_Num", PONumber);
                objCompHash.Add("@CompanyID", CompanyID);

                arrSpFieldSeq = new string[] { "@Po_Num", "@CompanyID" };
                ds = db.GetDataSet(objCompHash, "Aero_PurchaseOrder_Select", arrSpFieldSeq);

                return PopulatePurchaseOrders(ds);
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db.DBClose();
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }
        }

        public static void DeletePurchaseOrder(int poID, int userID)
        {
            SV.Framework.Fulfillment.LogOperations logOperations = SV.Framework.Fulfillment.LogOperations.CreateInstance<SV.Framework.Fulfillment.LogOperations>();

            SV.Framework.Models.Fulfillment.FulfillmentLogModel logModel = new SV.Framework.Models.Fulfillment.FulfillmentLogModel();
            string response = "";

            logModel.ActionName = "Fulfillment Delete";
            logModel.CreateUserID = userID;
            logModel.StatusID = 0;
            logModel.PO_ID = poID;
            logModel.FulfillmentNumber = string.Empty;
            logModel.Comment = string.Empty;
            string poXML = "POID: "+ poID;

            logModel.RequestData = poXML;

            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@Po_ID", poID);
                objCompHash.Add("@UserID", userID);

                arrSpFieldSeq = new string[] { "@Po_ID","@UserID" };
                db.ExeCommand(objCompHash, "Aero_PurchaseOrder_Delete", arrSpFieldSeq);

                response = "Deleted successfuly.";
            }
            catch (Exception objExp)
            {
                logModel.Comment = objExp.Message;
                response = objExp.Message;
                throw new Exception(objExp.Message.ToString());
            }            
            finally
            {

                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
                logModel.ResponseData = response;// BaseAerovoice.SerializeObject<PurchaseOrderResponse>(response);

               logOperations.FulfillmentLogInsert(logModel);

            }
}

        //public static void UpdatePurchaseOrder(ShipTracking shipTrack)
        //{
        //    DBConnect db = new DBConnect();
        //    string[] arrSpFieldSeq;
        //    Hashtable objCompHash = new Hashtable();
        //    try
        //    {
        //        objCompHash.Add("@PO_Num", shipTrack.PurchaseOrderNumber);
        //        objCompHash.Add("@Ship_Via", shipTrack.ShipToBy);
        //        objCompHash.Add("@Tracking", shipTrack.ShipToTrackingNumber);
        //        //objCompHash.Add("@ShipDate", shipTrack.ShipToDate);


        //        arrSpFieldSeq = new string[] { "@PO_Num", "@Ship_Via", "@Tracking" };
        //        db.ExeCommand(objCompHash, "Aero_PurchaseOrder_Update", arrSpFieldSeq);
        //    }
        //    catch (Exception objExp)
        //    {
        //        throw new Exception(objExp.Message.ToString());
        //    }
        //    finally
        //    {
        //        db.DBClose();
        //        db = null;
        //        objCompHash = null;
        //        arrSpFieldSeq = null;
        //    }
        //}

        public static void UpdatePurchaseOrder(BasePurchaseOrder purchaseOrder, int userID)
        {
            SV.Framework.Fulfillment.LogOperations logOperations = SV.Framework.Fulfillment.LogOperations.CreateInstance<SV.Framework.Fulfillment.LogOperations>();
 
            SV.Framework.Models.Fulfillment.FulfillmentLogModel logModel = new SV.Framework.Models.Fulfillment.FulfillmentLogModel();
            string response = "";

            logModel.ActionName = "Fulfillment Update";
            logModel.CreateUserID = userID;
            logModel.StatusID = purchaseOrder.PurchaseOrderStatusID;
            logModel.PO_ID = Convert.ToInt32(purchaseOrder.PurchaseOrderID);
            logModel.FulfillmentNumber = string.Empty;
            logModel.Comment = string.Empty;
            string poXML =  BaseAerovoice.SerializeObject<BasePurchaseOrder>(purchaseOrder);
            poXML = "<BasePurchaseOrder>" + poXML.Substring(poXML.IndexOf("<RequestedShipDate>"));

            logModel.RequestData = poXML;
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@PO_ID", purchaseOrder.PurchaseOrderID);
                objCompHash.Add("@PO_Num", purchaseOrder.PurchaseOrderNumber);
                objCompHash.Add("@Contact_Name", purchaseOrder.Shipping.ContactName);
                objCompHash.Add("@ShipTo_Attn", purchaseOrder.Shipping.ShipToAttn);
                objCompHash.Add("@Ship_Via", purchaseOrder.Tracking.ShipToBy);
                objCompHash.Add("@Tracking", purchaseOrder.Tracking.ShipToTrackingNumber);
                //objCompHash.Add("@ShipDate", shipTrack.ShipToDate);
                objCompHash.Add("@StatusID", purchaseOrder.PurchaseOrderStatusID);
                objCompHash.Add("@ShipTo_Address", purchaseOrder.Shipping.ShipToAddress);
                objCompHash.Add("@ShipTo_Address2", purchaseOrder.Shipping.ShipToAddress2);
                objCompHash.Add("@ShipTo_City", purchaseOrder.Shipping.ShipToCity);
                objCompHash.Add("@ShipTo_State", purchaseOrder.Shipping.ShipToState);
                objCompHash.Add("@ShipTo_Zip", purchaseOrder.Shipping.ShipToZip);
                objCompHash.Add("@Contact_Phone", purchaseOrder.Shipping.ContactPhone);
                objCompHash.Add("@UserID", userID);
                objCompHash.Add("@Comments", purchaseOrder.Comments);

                objCompHash.Add("@PoDate", purchaseOrder.PurchaseOrderDate);
                objCompHash.Add("@IsShipmentRequired", purchaseOrder.IsShipmentRequired);
                objCompHash.Add("@RequestedShipDate", purchaseOrder.RequestedShipDate);

                
                arrSpFieldSeq = new string[] { "@PO_ID", "@PO_Num", "@Contact_Name", "@ShipTo_Attn", "@Ship_Via", 
                "@Tracking", "@StatusID", "@ShipTo_Address","@ShipTo_Address2", "@ShipTo_City", "@ShipTo_State", "@ShipTo_Zip", 
                "@Contact_Phone","@UserID", "@Comments", "@PoDate","@IsShipmentRequired","@RequestedShipDate" };
                db.ExeCommand(objCompHash, "Aero_PurchaseOrder_Update", arrSpFieldSeq);

                response = "Updated successfully";
            }
            catch (Exception objExp)
            {
                logModel.Comment = objExp.Message;
                response = objExp.Message;
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db.DBClose();
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
                
                logModel.ResponseData = response;// BaseAerovoice.SerializeObject<PurchaseOrderResponse>(response);

                logOperations.FulfillmentLogInsert(logModel);

            }
        }

        public static void UpLoadESN(string POXml, int userID)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataSet ds = new DataSet();

            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@Po_Xml", POXml);
                objCompHash.Add("@UserID", userID);

                arrSpFieldSeq = new string[] { "@Po_Xml", "@UserID" };
                db.ExeCommand(objCompHash, "Aero_PurchaseOrderESN_UPDATE", arrSpFieldSeq);
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }
        }

        public static string UpLoadESNs(string POXml, int userID, string fileName, string comment, out string invalidESNList, out string invalidSkuEsnList, 
            out string esnExistsMessage, out string badEsnMessage, out string simNumberMessage, out int recordCount, 
            out string poErrorMessage, out string poSimMessage, out string poskuMessage, out string poEsnSimMessage)
        {

            string esnList = string.Empty;
            invalidESNList = string.Empty;
            invalidSkuEsnList = string.Empty;
            esnExistsMessage = string.Empty;
            badEsnMessage = string.Empty;
            simNumberMessage = string.Empty;
            recordCount = 0;

            poErrorMessage = string.Empty;
            poSimMessage = string.Empty;
            poskuMessage = string.Empty;
            poEsnSimMessage = string.Empty;

            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataSet ds = new DataSet();

            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@Po_Xml", POXml);
                objCompHash.Add("@UserID", userID);
                objCompHash.Add("@FileName", fileName);
                objCompHash.Add("@Comment", comment);

                arrSpFieldSeq = new string[] { "@Po_Xml", "@UserID", "@FileName", "@Comment" };
                db.ExCommand(objCompHash, "Aero_PurchaseOrderESN_UPDATE", arrSpFieldSeq, "@Output", "@InvalidESNMessage", "@InvalidSkuESNMessage", "@EsnExistsMessage", "@BadEsnMessage", "@SimNumberMessage", "@RecordCount", "@poErrorMessage", "@poSimMessage", "@poSKUErrorMessage", "@poEsnSimErrorMessage", out esnList, out invalidESNList, out invalidSkuEsnList, out esnExistsMessage, out badEsnMessage, out simNumberMessage, out recordCount, out poErrorMessage, out poSimMessage, out poskuMessage, out poEsnSimMessage);
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }
            return esnList;
        }

        public static void AssignMSL2ESNData(string ESN)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataSet ds = new DataSet();

            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@ESN", ESN);

                arrSpFieldSeq = new string[] { "@ESN" };
                db.ExeCommand(objCompHash, "Aero_MSL_UPDATE", arrSpFieldSeq);
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }
        }
        public static void AssignMSL2ESNDataNew(string ESN, out int poRecordCount, out string poErrorMessage)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataSet ds = new DataSet();

            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@ESN", ESN);
                

                arrSpFieldSeq = new string[] { "@ESN" };
                db.ExeCommand(objCompHash, "Av_MSL_UPDATE", arrSpFieldSeq, "@poRecordCount", out poRecordCount, "poErrorMessage", out poErrorMessage);
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }
        }

        public static void ValidateAssignESN(List<FulfillmentAssignESN> esnList, out int poRecordCount, out string invalidLTEList, out string invalidESNList, out string invalidSkuEsnList, out string esnExistsMessage, out string badESNMessage)
        {
            int returnValue = 0;
            poRecordCount = 0;
            invalidLTEList = string.Empty;
            invalidESNList = string.Empty;
            invalidSkuEsnList = string.Empty;
            esnExistsMessage = string.Empty;
            badESNMessage = string.Empty;
            string esnXML = clsGeneral.SerializeObject(esnList);
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataSet ds = new DataSet();

            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@piEsnXML", esnXML);

                arrSpFieldSeq = new string[] { "@piEsnXML" };
                //db.ExCommand(objCompHash, "Aero_PurchaseOrderESN_UPDATE", arrSpFieldSeq, "@Output", "@InvalidESNMessage", "@InvalidSkuESNMessage", "@EsnExistsMessage", out esnList, out invalidESNList, out invalidSkuEsnList, out esnExistsMessage);
                returnValue = db.ExCommand(objCompHash, "av_PurchaseOrderAssignESN_Validate", arrSpFieldSeq, "@poRecordCount", "@InValidaeLTEMessase", "@InvalidESNMessage", "@InvalidSkuESNMessage", "@EsnExistsMessage", "@BadEsnMessage", out poRecordCount, out invalidLTEList, out invalidESNList, out invalidSkuEsnList, out esnExistsMessage, out badESNMessage);
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }
            //return esnList;
        }

        public static List<FulfillmentAssignESN> AssignEsnToFulfillmentOrder(List<FulfillmentAssignESN> esnList, int userID, string poSource, out int poRecordCount, out string invalidESNMessage, out string invalidSkuESNMessage, out string esnExistsNMessage)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataTable dt = new DataTable();
            string esnXML = clsGeneral.SerializeObject(esnList);
            List<FulfillmentAssignESN> esnsList = null;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@piEsnXML", esnXML);
                objCompHash.Add("@piUserID", userID);
                objCompHash.Add("@piPoSource", poSource);

                arrSpFieldSeq = new string[] { "@piEsnXML", "@piUserID", "@piPoSource" };
                dt = db.GetTableRecords(objCompHash, "av_PurchaseOrderESN_Assign", arrSpFieldSeq, "@poRecordCount", "@InvalidESNMessage", "@InvalidSkuESNMessage", "@EsnExistsMessage", out poRecordCount, out invalidESNMessage, out invalidSkuESNMessage, out esnExistsNMessage);

               esnsList = PopulateFulfillmentESN(dt);
                //db.ExeCommand(objCompHash, "av_PurchaseOrderESN_Assign", arrSpFieldSeq, "@poRecordCount", out poRecordCount, "@poErrorMessage", out poErrorMessage);
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }
            return esnsList;
        }

        public static void UpLoadESN(List<PurchaseOrderESNItem>  poEnsItemList)
        {
            DBConnect db = new DBConnect();

            try
            {
                System.Data.SqlClient.SqlConnection conn = db.DBConnection();
                if (conn.State == ConnectionState.Open)
                {
                    string[] arrSpFieldSeq;
                    DataSet ds = new DataSet();
                    Hashtable objCompHash = new Hashtable();

                    arrSpFieldSeq = new string[] { "@POD_ID", "@esn", "@fmupc" };
                    foreach (PurchaseOrderESNItem poItem in poEnsItemList)
                    {
                        objCompHash.Clear();

                        objCompHash.Add("@POD_ID", poItem.PodID);
                        objCompHash.Add("@esn", poItem.ESN);
                        objCompHash.Add("@fmupc", poItem.FmUPC);

                        db.ExeCommand(objCompHash, "Aero_PurchaseOrderESN_Assign", arrSpFieldSeq);
                      //  db.ExeCommand(conn, objCompHash, "Aero_PurchaseOrderESN_Assign", arrSpFieldSeq);
                    }

                }
                else
                {
                    throw new Exception("Could not open database connection. Exception, please check database connectivity"); 
                }
            }
            catch (Exception objExp)
            {                                                      
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db.DBClose();
                db = null;
            }
        }

        public static int DeletePurchaseOrderDetail(int podID, int userID)
        {
            int returnValue = 0;
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataSet ds = new DataSet();

            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@POD_ID", podID);
                objCompHash.Add("@UserID", userID);

                arrSpFieldSeq = new string[] { "@POD_ID","@UserID" };
                db.ExeCommand(objCompHash, "Aero_PurchaseOrderDetail_Delete", arrSpFieldSeq, "@ItemCount", out returnValue);
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }
            return returnValue;
        }

        public static void PurchaseOrderUpdate(int poID, string contactName, string shipAttn, string shipVia, string shipAddress, string shipAddress2,
                                                string city, string state, string zip)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataSet ds = new DataSet();

            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@PO_ID", poID);
                objCompHash.Add("@Contact_Name", contactName);
                objCompHash.Add("@ShipTo_Attn", shipAttn);
                objCompHash.Add("@ShipTo_Address", shipAddress);
                objCompHash.Add("@ShipTo_Address2", shipAddress2);
                objCompHash.Add("@ShipTo_City", city);
                objCompHash.Add("@ShipTo_State", state);
                objCompHash.Add("@ShipTo_Zip", zip);
                objCompHash.Add("@Ship_Via", shipVia);

                arrSpFieldSeq = new string[] { "@PO_ID", "@Contact_Name", "@ShipTo_Attn", "@ShipTo_Address", "@ShipTo_Address2", "@ShipTo_City", "@ShipTo_State", "@ShipTo_Zip", "@Ship_Via" };
                db.ExeCommand(objCompHash, "Aero_PurchaseOrder_Update", arrSpFieldSeq);
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }
        }

        public static void PurchaseOrderUpdateDetail(int podID, string esn, string msl, string msid, string mdn, string passCode, string fmupc, int statusID, int userID, string lteICCiD, string lteIMSI, string akey, string otksl, string simNumber, out string returnMessage)
        {
            returnMessage = string.Empty;
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataSet ds = new DataSet();

            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@POD_ID", podID);
                objCompHash.Add("@ESN", esn);
                objCompHash.Add("@msl", msl);
                objCompHash.Add("@MSID", msid);
                objCompHash.Add("@mdn", mdn);
                objCompHash.Add("@Pass_Code", passCode);
                objCompHash.Add("@fmupc", fmupc);
                objCompHash.Add("@StatusID", statusID);
                objCompHash.Add("@LTEICCID", lteICCiD);
                objCompHash.Add("@LTEIMSI", lteIMSI);
                objCompHash.Add("@Akey", akey);
                objCompHash.Add("@Otksl", otksl);
                objCompHash.Add("@SimNumber", simNumber);
                objCompHash.Add("@UserID", userID);

                arrSpFieldSeq = new string[] { "@POD_ID", "@ESN", "@msl", "@MSID", "@mdn", "@Pass_Code", "@fmupc", "@StatusID",
                    "@LTEICCID", "@LTEIMSI", "@Akey", "@Otksl","@SimNumber", "@UserID" };
                db.ExCommand(objCompHash, "Aero_PurchaseOrderDetail_Update", arrSpFieldSeq, "@ReturnMessage", out returnMessage);
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }
        }
        public static void PurchaseOrderUpdateDetailNew(int podID, int qty, int userID, out string returnMessage)
        {
            returnMessage = string.Empty;
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataSet ds = new DataSet();

            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@POD_ID", podID);
                objCompHash.Add("@Qty", qty);
                objCompHash.Add("@UserID", userID);

                arrSpFieldSeq = new string[] { "@POD_ID", "@Qty", "@UserID" };
                db.ExCommand(objCompHash, "av_PurchaseOrderDetail_Update", arrSpFieldSeq, "@ReturnMessage", out returnMessage);
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }
        }

        public static bool SetTrackingInfo(int companyID, string purchaseOrderNumber, string trackingNumber, string avOrder, int userID)
        {
            bool saved = false;
            
            if (!string.IsNullOrEmpty(purchaseOrderNumber) && !string.IsNullOrEmpty(trackingNumber))
            {

                SetTrackingInfoDB(trackingNumber, companyID, purchaseOrderNumber, avOrder, userID);
                        saved = true;
             
            }
            
            return saved;
        }

        public static int UpdateTrackingInfo(List<TrackingInfo> trackingInfoList, int userID)
        {
            int returnValue = 0;

            string trackingXML = clsGeneral.SerializeObject(trackingInfoList);

            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@TrackingXML", trackingXML);
                objCompHash.Add("@UserID", userID);
                arrSpFieldSeq = new string[] { "@TrackingXML", "@UserID" };
                db.ExeCommand(objCompHash, "av_PurchaseOrderTrackingInfo_Update", arrSpFieldSeq, "@POCount", out returnValue);
            }
            
            catch (Exception exp)
            {
                throw exp;
            }

            return returnValue;
        }
        
        public static List<TrackingInfo> UpdateFulfillmentTracking(List<TrackingInfo> trackingInfoList, string companyAccountNumber, string poSource, PurchaseOrderStatus poStatus, int userID, out int returnValue, out string returnMessage)
        {
            returnValue = 0;
            returnMessage = string.Empty;
            List<TrackingInfo> poList = new List<TrackingInfo>();

            string trackingXML = clsGeneral.SerializeObject(trackingInfoList);

            DataTable dt = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@piTrackingXML", trackingXML);
                objCompHash.Add("@piCompanyAccountNumber", companyAccountNumber);
                objCompHash.Add("@piUserID", userID);
                objCompHash.Add("@piStatusID", Convert.ToInt32(poStatus));
                objCompHash.Add("@piPoSource", poSource);

                arrSpFieldSeq = new string[] { "@piTrackingXML", "@piCompanyAccountNumber", "@piUserID", "@piStatusID","@piPoSource" };
                dt = db.GetTableRecords(objCompHash, "av_PurchaseOrderTracking_Update", arrSpFieldSeq, "@poRecordCount", "@poErrorMessage", out returnValue, out returnMessage);
                poList = PopulateTrackingInfo(dt);

            }

            catch (Exception exp)
            {
                throw exp;
            }

            return poList;
        }
        public static List<FulfillmentOrderHeader> ValidateFulfillmentOrders(List<FulfillmentChangeStatus> posList, int companyID, out int returnValue, out string returnMessage)
        {
            List<FulfillmentOrderHeader> poList = new List<FulfillmentOrderHeader>();

            returnValue = 0;
            returnMessage = string.Empty;
            
            string poXML = clsGeneral.SerializeObject(posList);

            DataTable dt = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@POXML", poXML);
                objCompHash.Add("@CompanyID", companyID);
                
                arrSpFieldSeq = new string[] { "@POXML", "@CompanyID" };
                dt = db.GetTableRecords(objCompHash, "Av_PurchaseOrder_Bulk_Validate", arrSpFieldSeq, "@poRecordCount", "@poErrorMessage", out returnValue, out returnMessage);
                poList = PopulateFulfillmentOrderWithHeader(dt);

            }

            catch (Exception exp)
            {
                throw exp;
            }

            return poList;
        }
        public static List<FulfillmentOrderHeader> UploadFulfillmentStatuses(List<FulfillmentChangeStatus> posList, int companyID, string poSource, int poStatusID, int userID, string fileName, string comment, out int returnValue, out string returnMessage)
        {
            returnValue = 0;
            returnMessage = string.Empty;
            List<FulfillmentOrderHeader> poList = new List<FulfillmentOrderHeader>();

            string poXML = clsGeneral.SerializeObject(posList);

            DataTable dt = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@POXML", poXML);
                objCompHash.Add("@CompanyID", companyID);
                objCompHash.Add("@StatusID", poStatusID);
                objCompHash.Add("@UserID", userID);
                objCompHash.Add("@FileName", fileName);
                objCompHash.Add("@Comment", comment);
                objCompHash.Add("@poSource", poSource);

                arrSpFieldSeq = new string[] { "@POXML", "@CompanyID", "@StatusID", "@UserID", "@FileName", "@Comment", "@poSource" };
                dt = db.GetTableRecords(objCompHash, "Av_PurchaseOrder_UploadStatus", arrSpFieldSeq, "@poRecordCount", "@Output", out returnValue, out returnMessage);
                poList = PopulateFulfillmentOrderWithHeader(dt);

            }

            catch (Exception exp)
            {
                throw exp;
            }

            return poList;
        }

        public static bool DeletePurchaseOrders(string purchaseOrders, int companyID, int statusID, int userID)
        {
            bool saved = false;
            if (!string.IsNullOrEmpty(purchaseOrders) && companyID > 0)
            {

                DBConnect db = new DBConnect();
                string[] arrSpFieldSeq;
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@PO_Num", purchaseOrders);
                    objCompHash.Add("@CompanyID", companyID);
                    objCompHash.Add("@StatusID", statusID);
                    objCompHash.Add("@UserID", userID);

                    arrSpFieldSeq = new string[] { "@PO_Num", "@CompanyID", "@StatusID", "@UserID" };
                    db.ExeCommand(objCompHash, "Aero_PurchaseOrder_ChangeStatus", arrSpFieldSeq);
                    saved = true;
                }
                catch (Exception objExp)
                {
                    throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }

            return saved;
        }

        public static bool SetEsnMsl(string UPC, string Esn, string Msl)//, string AvOrder, string PO_Num, string CompanyNumber, string Comment, string storeID)                    
        {
            bool retValue = true;
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@UPC", UPC);
                objCompHash.Add("@ESN", Esn);
                objCompHash.Add("@Msl", Msl);
                arrSpFieldSeq = new string[] { "@UPC", "@ESN", "@Msl" };
                object retVal = db.ExecuteScalar(objCompHash, "Aero_Inventory_ESN_MSL_Insert", arrSpFieldSeq);
                Boolean.TryParse(retVal.ToString(), out retValue);
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                objCompHash = null;
                arrSpFieldSeq = null;
            }

            return retValue;
        }

        public static bool SetEsnMsl(string esnData, int itemID, out int insertCout, out int updateCount, out string errorMessage, out bool isLTE)
        {
            bool retboolValue = true;
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            //int retIntVal = 0;
            try
            {
                objCompHash.Add("@esnxml", esnData);
                objCompHash.Add("@ItemGUID", itemID);
                arrSpFieldSeq = new string[] { "@esnxml", "@ItemGUID" };
                db.ExeCommand(objCompHash, "Aero_ESN_Insert", arrSpFieldSeq, "@poInsertCount", out insertCout, "@poUpdateCount", out updateCount, "@poErrorMessage", out errorMessage, "@poLteAttribute", out isLTE);
                //int.TryParse(retVal.ToString(), out retIntVal);
                //if (retIntVal == 0)
                if (errorMessage == string.Empty)
                    retboolValue = true;
                else
                    retboolValue = false;
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
                retboolValue = false;
            }
            finally
            {
                objCompHash = null;
                arrSpFieldSeq = null;
            }

            return retboolValue;
        }

        public static void SetESNServiceLogging(int PodID, string PoNum, string ESN, string MslNumber)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            string sCode = string.Empty;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@POD_ID", PodID);
                objCompHash.Add("@PoNum", PoNum);
                objCompHash.Add("@ESN", ESN);
                objCompHash.Add("@MslNumber", MslNumber);
                arrSpFieldSeq = new string[] { "@POD_ID", "@PoNum", "@ESN", "@MslNumber" };
                db.ExeCommand(objCompHash, "Aero_ESNService_Insert", arrSpFieldSeq);

            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                objCompHash = null;
                arrSpFieldSeq = null;
            }
        }

        public static void BulkUpload_ESN(string fileName)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            string sCode = string.Empty;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@filename", fileName);

                arrSpFieldSeq = new string[] { "@filename" };
                db.ExeCommand(objCompHash, "Aero_UploadESN_MSL", arrSpFieldSeq);

            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                objCompHash = null;
                arrSpFieldSeq = null;
            }
        }

        public static List<PurchaseOrderESN> GetEsnToSend(int companyID)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataSet ds = new DataSet();
            Hashtable objCompHash = new Hashtable();
            List<PurchaseOrderESN> esnList = null;
            try
            {
                objCompHash.Add("@companyID", companyID);

                arrSpFieldSeq = new string[] { "@companyID" };
                ds = db.GetDataSet(objCompHash, "Aero_ESNService_Select", arrSpFieldSeq);
                esnList = PopulatePurchaseOrderItems(ds);
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db = null;
            }

            return esnList;
        }

        public static DataSet GetPurchaseOrder_ESN_ASN(string po_num, int companyID)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataSet ds = new DataSet();
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@Po_Num", po_num);
                objCompHash.Add("@CompanyID", companyID);

                arrSpFieldSeq = new string[] { "@Po_Num", "@CompanyID" };
                ds = db.GetDataSet(objCompHash, "Aero_GetPurchaseOrderESN_ASN", arrSpFieldSeq);
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db = null;
            }

            return ds;
        }

        public static PurchaseOrders GetPurchaseOrder_Missing_MSL(string po_num, int companyID)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataSet ds = new DataSet();
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@Po_Num", po_num);
                objCompHash.Add("@CompanyID", companyID);

                arrSpFieldSeq = new string[] { "@Po_Num", "@CompanyID" };
                ds = db.GetDataSet(objCompHash, "Aero_PurchaseOrder_MSL_Select", arrSpFieldSeq);
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db = null;
            }

            return PopulatePurchaseOrders(ds);
        }

        public static PurchaseOrders GetPurchaseOrder_Missing_ESN(string po_num, int companyID)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataSet ds = new DataSet();
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@Po_Num", po_num);
                objCompHash.Add("@CompanyID", companyID);

                arrSpFieldSeq = new string[] { "@Po_Num", "@CompanyID" };
                ds = db.GetDataSet(objCompHash, "Aero_PurchaseOrder_ESN_Select", arrSpFieldSeq);
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db = null;
            }

            return PopulatePurchaseOrders(ds);
        }

        public static PurchaseOrders GetPurchaseOrder_Missing_ESN_MSL(string po_num, int companyID)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataSet ds = new DataSet();
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@Po_Num", po_num);
                objCompHash.Add("@CompanyID", companyID);

                arrSpFieldSeq = new string[] { "@Po_Num", "@CompanyID" };
                ds = db.GetDataSet(objCompHash, "Aero_PurchaseOrder_ESN_MSL_Select", arrSpFieldSeq);
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db = null;
            }

            return PopulatePurchaseOrders(ds);
        }

        public static PurchaseOrders GetPurchaseOrder_Missing_ASN(string po_num, int companyID)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataSet ds = new DataSet();
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@Po_Num", po_num);
                objCompHash.Add("@CompanyID", companyID);

                arrSpFieldSeq = new string[] { "@Po_Num", "@CompanyID" };
                ds = db.GetDataSet(objCompHash, "Aero_PurchaseOrder_ASN_Select", arrSpFieldSeq);
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db = null;
            }

            return PopulatePurchaseOrders(ds);
        }

        public static PurchaseOrders GetPurchaseOrder_Missing_ESN_MSL_ASN(string po_num, int companyID)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataSet ds = new DataSet();
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@Po_Num", po_num);
                objCompHash.Add("@CompanyID", companyID);

                arrSpFieldSeq = new string[] { "@Po_Num", "@CompanyID" };
                ds = db.GetDataSet(objCompHash, "Aero_PurchaseOrder_ESN_MSL_ASN_Select", arrSpFieldSeq);
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db = null;
            }

            return PopulatePurchaseOrders(ds);
        }

        public static PurchaseOrders GetPurchaseOrder_Missing_MSL_ASN(string po_num, int companyID)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataSet ds = new DataSet();
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@Po_Num", po_num);
                objCompHash.Add("@CompanyID", companyID);

                arrSpFieldSeq = new string[] { "@Po_Num", "@CompanyID" };
                ds = db.GetDataSet(objCompHash, "Aero_PurchaseOrder_MSL_ASN_Select", arrSpFieldSeq);
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db = null;
            }

            return PopulatePurchaseOrders(ds);
        }

        public static List<PurchaseOrderESN> GetAsnToSend(int companyID)
        {

            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataSet ds = new DataSet();
            Hashtable objCompHash = new Hashtable();
            List<PurchaseOrderESN> esnlist = null;
            try
            {
                objCompHash.Add("@companyID", companyID);

                arrSpFieldSeq = new string[] { "@companyID" };
                ds = db.GetDataSet(objCompHash, "Aero_ASNService_Select", arrSpFieldSeq);
                esnlist = PopulatePurchaseOrderItems(ds);
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db.DBClose();
                db = null;
            }

            return esnlist;
        }

        public static void SetASNServiceLogging(int po_id, string PoNum, string TrackingNumber)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            string sCode = string.Empty;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@Po_ID", po_id);
                objCompHash.Add("@PoNum", PoNum);
                objCompHash.Add("@TrackingNumber", TrackingNumber);
                arrSpFieldSeq = new string[] { "@Po_ID", "@PoNum", "@TrackingNumber" };
                db.ExeCommand(objCompHash, "Aero_ASNService_Insert", arrSpFieldSeq);

            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                objCompHash = null;
                arrSpFieldSeq = null;
            }
        }

        public static InventoryList GetPhoneInventory(InventoryItemRequest inventoryItemRequest)
        {
            InventoryList inventoryList = new InventoryList();
            Exception exc = null;
            int userId = AuthenticateRequest(inventoryItemRequest.Authentication, out exc);
            if (userId > 0)
            {
                inventoryList = clsInventoryDB.GetPhoneInventory(userId);
            }
            return inventoryList;
        }

        public static clsInventory GetInventoryItem(int ItemID)
        {
            clsInventory inv = null;
            if (ItemID > 0)
            {
                inv = clsInventoryDB.GetInventoryInformation(ItemID);
            }
            return inv;
        }

        public static List<InventorySummary> GetPhoneSummary(PhoneSummary PhoneSummaryData)
        {
            List<InventorySummary> inventoryList = new List<InventorySummary>();

            inventoryList = clsInventoryDB.GetPhonesSummary(PhoneSummaryData.StartDate, PhoneSummaryData.EndDate, PhoneSummaryData.ItemCode,
                                        PhoneSummaryData.UserID, PhoneSummaryData.AddHistory, PhoneSummaryData.CompanyID);
            
            return inventoryList;
        }

        public static DataTable GetAssignedEsnList(PhoneSummary PhoneSummaryData)
        {

            DataTable dataTable = clsInventoryDB.GetAssignedEsnList(PhoneSummaryData.StartDate, PhoneSummaryData.EndDate, PhoneSummaryData.ItemCode,
                                        PhoneSummaryData.UserID, PhoneSummaryData.AddHistory, PhoneSummaryData.CompanyID);

            return dataTable;
        }

        public static int SetInventoryItem(clsInventory inventoryInfo)
        {
            int result = 0;
            result = clsInventoryDB.SetInventoryItem(inventoryInfo);

            return result;
        }

        public static int UpdateInventoryItem(clsInventory inventoryInfo)
        {
            int result = 0;
            result = clsInventoryDB.UpdateInventoryItem(inventoryInfo);

            return result;
        }

        public static InventoryList GetInventory(int userId)
        {
            InventoryList inventoryList = new InventoryList();

            if (userId > 0)
            {
                inventoryList = clsInventoryDB.GetInventory(userId);
            }
            else
            {
                inventoryList = clsInventoryDB.GetInventory();
            }

            return inventoryList;
        }

        public static InventoryList GetInventory(int companyid, int userId)
        {
            InventoryList inventoryList = new InventoryList();

            if (userId == 0 && companyid > 0)
            {
                inventoryList = clsInventoryDB.GetInventory(0, companyid);
            }
            else if (userId > 0 && companyid == 0)
            {
                inventoryList = clsInventoryDB.GetInventory(userId);
            }
            else if (userId > 0 && companyid > 0)
            {
                inventoryList = clsInventoryDB.GetInventory(userId, companyid);
            }
            else
            {
                inventoryList = clsInventoryDB.GetInventory();
            }

            return inventoryList;
        }
        
        public static void DeleteEsns(string esns)
        {
            clsInventoryDB.DeleteEsns(esns);
        }

        public static InventoryList GetAccessoryInventory(InventoryItemRequest inventoryItemRequest)
        {
            InventoryList inventoryList = new InventoryList(); 
            Exception exc = null;
            int userId = AuthenticateRequest(inventoryItemRequest.Authentication, out exc);
            if (userId > 0)
            {
                inventoryList = clsInventoryDB.GetAccessoryInventory(userId);
            }
            return inventoryList;
        }

        public static DataTable GetPhoneEsns(int PhoneID)
        {
            return clsInventoryDB.GetPhoneEsns(PhoneID);
        }

        public static InventoryList GetInventoryItems(InventoryItemRequest inventoryItemRequest)
        {
            InventoryList inventoryList = new InventoryList();
            Exception exc = null;
            int userId = AuthenticateRequest(inventoryItemRequest.Authentication, out exc);
            if (userId > 0)
            {
                inventoryList = clsInventoryDB.GetInventoryItems(userId,-1, 0);
            }
            return inventoryList;
        }

        public static InventoryList GetInventoryItems(int userID, int sim, int CompanyID)
        {
            return clsInventoryDB.GetInventoryItems(userID, sim, CompanyID);
        }

        public static InventoryLastUpdate GetInventoryLastUpdate()
        {
            return clsInventoryDB.GetInventoryLastUpdate();
        }
        public static List<ShipBy> GetShipByList()
        {

            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataTable dt = new DataTable();
            Hashtable objCompHash = new Hashtable();
            List<ShipBy> shipBylist = default;
            try
            {
                objCompHash.Add("@ShipByID", 0);
                objCompHash.Add("@ShipByCode", string.Empty);
               // objCompHash.Add("@Nationality", string.Empty);
                arrSpFieldSeq = new string[] { "@ShipByID", "@ShipByCode" };
                dt = db.GetTableRecords(objCompHash, "av_ShipBy_Select", arrSpFieldSeq);
                shipBylist = PopulateShipBy(dt);
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db.DBClose();
                db = null;
            }

            return shipBylist;
        }
        public static List<ShipBy> GetShipByList(string Nationality)
        {

            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataTable dt = new DataTable();
            Hashtable objCompHash = new Hashtable();
            List<ShipBy> shipBylist = null;
            try
            {
                objCompHash.Add("@ShipByID", 0);
                objCompHash.Add("@ShipByCode", string.Empty);
                objCompHash.Add("@Nationality", Nationality);
                arrSpFieldSeq = new string[] { "@ShipByID", "@ShipByCode", "@Nationality" };
                dt = db.GetTableRecords(objCompHash, "av_ShipBy_Select", arrSpFieldSeq);
                shipBylist = PopulateShipBy(dt);
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db.DBClose();
                db = null;
            }

            return shipBylist;
        }

        public static Hashtable GetShipBy()
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataTable dt = new DataTable();
            Hashtable shipByHash = new Hashtable();
            Hashtable objCompHash = new Hashtable();
            
            try
            {
                objCompHash.Add("@ShipByID", 0);
                objCompHash.Add("@ShipByCode", string.Empty);
                arrSpFieldSeq = new string[] { "@ShipByID", "@ShipByCode" };
                dt = db.GetTableRecords(objCompHash, "av_ShipBy_Select", arrSpFieldSeq);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dt.Rows)
                    {

                        shipByHash.Add((string)clsGeneral.getColumnData(dataRow, "ShipByCode", string.Empty, true), (string)clsGeneral.getColumnData(dataRow, "ShipByText", string.Empty, true));

                    }
                }
                
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db.DBClose();
                db = null;
            }

            return shipByHash;
        }
       

        #endregion

        #region Private Methods
        public static string ValidateShipBy(string shipBy)
        {
            string returnMsg = string.Empty;
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataTable dt = new DataTable();
            Hashtable objCompHash = new Hashtable();
            
            try
            {
                objCompHash.Add("@ShipByID", 0);
                objCompHash.Add("@ShipByCode", shipBy);

                arrSpFieldSeq = new string[] { "@ShipByID", "@ShipByCode" };
                dt = db.GetTableRecords(objCompHash, "av_ShipBy_Select", arrSpFieldSeq);
                if (dt != null && dt.Rows.Count > 0)
                {
                    returnMsg = dt.Rows[0]["ShipByCode"].ToString();
                }
                
            }
            catch (Exception objExp)
            {
                //returnMsg = objExp.Message;
                throw new Exception(objExp.Message);
            }
            finally
            {
                db.DBClose();
                db = null;
            }

            return returnMsg;
        }
        public static string ValidateShipByText(string shipByText)
        {
            string returnMsg = string.Empty;
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataTable dt = new DataTable();
            Hashtable objCompHash = new Hashtable();

            try
            {
                objCompHash.Add("@ShipByID", 0);
                objCompHash.Add("@ShipByCode", "");
                objCompHash.Add("@ShipByText", shipByText);

                arrSpFieldSeq = new string[] { "@ShipByID", "@ShipByCode", "@ShipByText" };
                dt = db.GetTableRecords(objCompHash, "av_ShipBy_Select", arrSpFieldSeq);
                if (dt != null && dt.Rows.Count > 0)
                {
                    returnMsg = dt.Rows[0]["ShipByText"].ToString();
                }

            }
            catch (Exception objExp)
            {
                //returnMsg = objExp.Message;
                throw new Exception(objExp.Message);
            }
            finally
            {
                db.DBClose();
                db = null;
            }

            return returnMsg;
        }
        private static string PurchaseOrderProvisioning2(string esn, string mdn, string msid, string passCode, string mslNumber, string otksl, string akey)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            string errorMessage = string.Empty;
            int retValue = 0;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@ESN", esn);
                objCompHash.Add("@MDN", mdn);
                objCompHash.Add("@MSID", msid);
                objCompHash.Add("@Pass_Code", passCode);
                objCompHash.Add("@MslNumer", mslNumber);
                objCompHash.Add("@OTKSL", otksl);
                objCompHash.Add("@Akey", akey);
                arrSpFieldSeq = new string[] { "@ESN", "@MDN", "@MSID", "@Pass_Code", "@MslNumer", "@OTKSL", "@Akey" };
                
                //arrSpFieldSeq = new string[] { "@ESN", "@MDN", "@MSID", "@Pass_Code", "@MslNumer" };
                //int.TryParse(db.ExecuteNonQuery(objCompHash, "Aero_PurchaseOrderProvision_Insert", arrSpFieldSeq).ToString(), out retValue);
                retValue = db.ExecuteNonQuery(objCompHash, "Aero_PurchaseOrderProvision_Insert", arrSpFieldSeq);
                if (retValue > 0)
                {
                    errorMessage = string.Empty;
                }
                else
                {
                    errorMessage = ResponseErrorCode.ErrowWhileLoadingData.ToString();
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
                db.DBClose();
                db = null;
            }
            return errorMessage;
        }


        private static string PurchaseOrderProvisioning(string esn, string mdn, string msid, string passCode, string mslNumber, string otksl, string akey, string assignmenttype)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            string errorMessage = string.Empty;
            int retValue = 0;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@ESN", esn);
                objCompHash.Add("@MDN", mdn);
                objCompHash.Add("@MSID", msid);
                objCompHash.Add("@Pass_Code", passCode);
                objCompHash.Add("@MSLNumber", mslNumber);
                objCompHash.Add("@OTKSL", otksl);
                objCompHash.Add("@Akey", akey);
                objCompHash.Add("@Assignmenttype", assignmenttype);

                arrSpFieldSeq = new string[] { "@ESN", "@MDN", "@MSID", "@Pass_Code", "@MSLNumber", "@OTKSL", "@Akey", "@Assignmenttype" };
                //int.TryParse(db.ExecuteNonQuery(objCompHash, "Aero_PurchaseOrderProvision_Insert", arrSpFieldSeq).ToString(), out retValue);
                retValue = db.ExecuteNonQuery(objCompHash, "Aero_PurchaseOrderProvision_Insert", arrSpFieldSeq);
                if (retValue > 0)
                {
                    errorMessage = string.Empty;
                }
                else
                {
                    errorMessage = ResponseErrorCode.ErrowWhileLoadingData.ToString();
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
                db.DBClose();
                db = null;
            }
            return errorMessage;
        }

        public static int AuthenticateRequest(clsAuthentication AuthenticateUser, out Exception ex)
        {
            int userId = 0;
            userId = clsUser.ValidateUser(AuthenticateUser, out ex);
            return userId;
        }
        //public static int AuthenticateRequest(clsAuthentication AuthenticateUser, out Exception ex)
        //{
        //    int userId = 0;
        //    userId = clsUser.ValidateUser(AuthenticateUser);
        //    return userId;
        //}

        private static bool ValidatePurchaseOrder(clsPurchaseOrder purchaseOrder)
        {
            bool save = false;
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                if (!string.IsNullOrEmpty(purchaseOrder.PurchaseOrderNumber))
                {
                    objCompHash.Add("@PO_Num", purchaseOrder.PurchaseOrderNumber);
                    arrSpFieldSeq = new string[] { "@PO_Num" };
                    int cout = Convert.ToInt16(db.ExecuteScalar(objCompHash, "Aero_PurchaseOrder_Validate", arrSpFieldSeq));
                    save = (cout == 0 ? true : false);
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return save;
        }

        private static bool ValidatePurchaseOrder(clsPurchaseOrder purchaseOrder, int userId)
        {
            bool save = false;
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                if (!string.IsNullOrEmpty(purchaseOrder.PurchaseOrderNumber))
                {
                    objCompHash.Add("@PO_Num", purchaseOrder.PurchaseOrderNumber);
                    objCompHash.Add("@UserID", userId);
                    arrSpFieldSeq = new string[] { "@PO_Num", "@UserID" };
                    int cout = Convert.ToInt16(db.ExecuteScalar(objCompHash, "Aero_PurchaseOrder_Validate", arrSpFieldSeq));
                    save = (cout == 0 ? true : false);
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return save;
        }

        private static bool ValidationItems(clsPurchaseOrder purchaseOrder, InventoryList inventoryList, out string itemsCodeList)
        {
            bool found = true;
            itemsCodeList = string.Empty;
            foreach (Classes.PurchaseOrderItem inv in purchaseOrder.PurchaseOrderItems)
            {
                if (!inventoryList.Exists(inv.ItemCode))
                {
                    itemsCodeList = inv.ItemCode;
                    found = false;
                    break;
                }
            }

            return found;
        }
        

        private static void ReportBadInventoryItemDB(System.Collections.Generic.List<InventoryItem> inventoryItems, int userId)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            arrSpFieldSeq = new string[] { "@esn", "@Box_Id", "@User_Id" };
            try
            {
                foreach (InventoryItem item in inventoryItems)
                {
                    objCompHash.Add("@esn", item.Esn);
                    objCompHash.Add("@Box_Id", item.BoxId);
                    objCompHash.Add("@User_Id", userId);
                    db.ExeCommand(objCompHash, "Aero_BadESN_Insert", arrSpFieldSeq);
                    objCompHash.Clear();
                }
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }
        }

        public static string sxml(clsPurchaseOrder purchaseOrder)
        {
            return BaseAerovoice.SerializeObject<clsPurchaseOrder>(purchaseOrder);
        }

        public static void SetPurchaseOrderStatusChangeDB(List<PurchaseOrderChangeStatus> poIdList, int statusId, int userID)
        {
            string poXML = clsGeneral.SerializeObject(poIdList);
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@poXML", poXML);
                objCompHash.Add("@StatusID", statusId);
                objCompHash.Add("@UserID", userID);

                arrSpFieldSeq = new string[] { "@poXML", "@StatusID", "@UserID" };
                //db.ExeCommand(objCompHash, "Aero_PurchaseOrder_StatusUpdate", arrSpFieldSeq);

                //

            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }
        }

        public static void UpdateFulfillmentChangeStatus(List<avii.Classes.FulfillmentChangeStatus> poList, int companyID, int statusID, int userID, out int poRecordCount, out string poNum)
        {
            string poXml = clsGeneral.SerializeObject(poList);
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            poRecordCount = 0;
            poNum = string.Empty;
            try
            {
                objCompHash.Add("@POXML", poXml);
                objCompHash.Add("@CompanyID", companyID);
                objCompHash.Add("@StatusID", statusID);
                objCompHash.Add("@UserID", userID);
                objCompHash.Add("@poSource", "U");

                arrSpFieldSeq = new string[] { "@POXML", "@CompanyID", "@UserID", "@StatusID", "@poSource" };
                
                db.ExeCommand(objCompHash, "Av_PurchaseOrder_UploadStatus", arrSpFieldSeq, "@poRecordCount", out poRecordCount, "@Output", out poNum);


            }
            catch (Exception objExp)
            {

                throw objExp;
            }
            finally
            {
                db.DBClose();
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }
        }
        
        public static void MoveFulfillmentOrderFromHistoryToPO(string poNum, int companyID)
        {
            //string poXml = clsGeneral.SerializeObject(poList);
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            //poRecordCount = 0;
            
            try
            {
                objCompHash.Add("@PoNum", poNum);
                objCompHash.Add("@CompanyID", companyID);
                //objCompHash.Add("@StatusID", statusID);
                //objCompHash.Add("@UserID", userID);


                arrSpFieldSeq = new string[] { "@PoNum", "@CompanyID" };
                db.ExeCommand(objCompHash, "AV_MovePOFromArchiveToPortal", arrSpFieldSeq);


            }
            catch (Exception objExp)
            {

                throw objExp;
            }
            finally
            {
                db.DBClose();
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }
        }
        
        public static int SetPurchaseOrderChangeStatusDB(List<PurchaseOrderChangeStatus> poList, string status, int userID, int conditionalStatus)
        {
            int returnValue = 0;
            string poXML = clsGeneral.SerializeObject(poList);
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@piPoXML", poXML);
                objCompHash.Add("@piStatus", status);
                objCompHash.Add("@piUserID", userID);
                objCompHash.Add("@piConditionalStatus", conditionalStatus);

                arrSpFieldSeq = new string[] { "@piPoXML", "@piStatus", "@piUserID", "@piConditionalStatus" };
                returnValue = db.ExecCommand(objCompHash, "Av_PurchaseOrder_ChangeStatus", arrSpFieldSeq);
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }
            return returnValue;
        }

        public static PurchaseOrderResponse SavePurchaseOrder(clsPurchaseOrder purchaseOrder, int UserID)
        {
            if (purchaseOrder.B2B==true)
                purchaseOrder.POType = "B2B";
            else
                purchaseOrder.POType = "B2C";

            bool IsInterNational = false;

            if (System.Enum.IsDefined(typeof(SV.Framework.Common.LabelGenerator.CanadaStates), purchaseOrder.Shipping.ShipToState))
                IsInterNational = true;

            String errorMessage = string.Empty, PurchaseOrderNumber = string.Empty;
            PurchaseOrderResponse response = new PurchaseOrderResponse();
            errorMessage = CreatePurchaseOrderDB(BaseAerovoice.SerializeObject<clsPurchaseOrder>(purchaseOrder), UserID, 0, avii.Classes.PurchaseOrderFlag.A.ToString(), IsInterNational, out PurchaseOrderNumber);
            if (errorMessage.Equals(ResponseErrorCode.UploadedSuccessfully.ToString()))
            {
                response.ErrorCode = string.Empty;// ResponseErrorCode.UploadedSuccessfully.ToString();
                response.PurchaseOrderNumber = PurchaseOrderNumber;
                response.Comment = string.Empty;
            }
            else
            {                
                response.Comment = errorMessage;
                if (errorMessage.Equals(ResponseErrorCode.PurchaseOrderAlreadyExists.ToString()))
                {
                    response.PurchaseOrderNumber = purchaseOrder.CustomerOrderNumber;
                    response.ErrorCode = ResponseErrorCode.PurchaseOrderAlreadyExists.ToString();
                }
                else
                {
                    response.PurchaseOrderNumber = string.Empty;
                    response.ErrorCode = ResponseErrorCode.ErrowWhileLoadingData.ToString();
                }
            }            
            return response;
        }

        public static PurchaseOrderResponse SaveNewPurchaseOrder(clsPurchaseOrder purchaseOrder, int UserID, int forecastGUID, string poFlag)
        {
            String errorMessage = string.Empty, PurchaseOrderNumber = string.Empty;
            bool IsInterNational = false;

            if (System.Enum.IsDefined(typeof(SV.Framework.Common.LabelGenerator.CanadaStates), purchaseOrder.Shipping.ShipToState))
                IsInterNational = true;

            PurchaseOrderResponse response = new PurchaseOrderResponse();
            errorMessage = CreatePurchaseOrderDB(BaseAerovoice.SerializeObject<clsPurchaseOrder>(purchaseOrder), UserID, forecastGUID, poFlag, IsInterNational, out PurchaseOrderNumber);
            if (errorMessage.Equals(ResponseErrorCode.UploadedSuccessfully.ToString()))
            {
                response.ErrorCode = string.Empty;// ResponseErrorCode.UploadedSuccessfully.ToString();
                response.PurchaseOrderNumber = PurchaseOrderNumber;
                response.Comment = string.Empty;
            }
            else
            {
                response.Comment = errorMessage;
                if (errorMessage.Equals(ResponseErrorCode.PurchaseOrderAlreadyExists.ToString()))
                {
                    response.PurchaseOrderNumber = purchaseOrder.PurchaseOrderNumber;
                    response.ErrorCode = ResponseErrorCode.PurchaseOrderAlreadyExists.ToString();
                }
                else
                {
                    response.PurchaseOrderNumber = string.Empty;
                    response.ErrorCode = ResponseErrorCode.ErrowWhileLoadingData.ToString();
                }
            }

            return response;
        }
        public static List<clsPurchaseOrder> SaveBulkPurchaseOrderDB(List<clsPurchaseOrder> poList, string companyAccountNumber, int userID, string poSource, string fileName, string comment, out int poRecordCount, out int poReturnCount, out string errorMessage)
        {
            //int returnValue = 0;
            poRecordCount = 0;
            poReturnCount = 0;
            errorMessage = string.Empty;

            string poXML = clsGeneral.SerializeObject(poList);

            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            string sCode = string.Empty;
            DataTable dt = new DataTable();
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@piCompanyAccountNumber", companyAccountNumber);
                objCompHash.Add("@piXMLData", poXML);
                objCompHash.Add("@piUserID", userID);
                objCompHash.Add("@piPOSource", poSource);
                objCompHash.Add("@FileName", fileName);
                objCompHash.Add("@Comment", comment);


                arrSpFieldSeq = new string[] { "@piCompanyAccountNumber", "@piXMLData", "@piUserID", "@piPOSource", "@FileName", "@Comment" };
                dt = db.GetTableRecords(objCompHash, "av_PurchaseOrder_Bulk_Create", arrSpFieldSeq, "@poRecordCount", "@poReturnCount", out poRecordCount, out poReturnCount);
                int statusID = 0;
                string poNum = string.Empty;
                string comments = "None";

                foreach (DataRow dataRowItem in dt.Rows)
                {
                    statusID = Convert.ToInt32(clsGeneral.getColumnData(dataRowItem, "statusID", 0, false));
                    if (statusID > 0)
                        comments = "Uploaded";
                    poNum = clsGeneral.getColumnData(dataRowItem, "ponum", string.Empty, false) as string;
                    //update po status
                    poList
                       .Where(x => x.PurchaseOrderNumber == poNum)
                       .ToList()
                       .ForEach(x => { x.Comments = comments; });

                }
            }
            catch (Exception objExp)
            {
                errorMessage = "av_PurchaseOrder_Bulk_Create: " + objExp.Message.ToString();
            }
            finally
            {
                objCompHash = null;
                arrSpFieldSeq = null;
            }


            return poList;
        }
        
        public static int SaveBulkPurchaseOrderDBold(List<clsPurchaseOrder> poList, string companyAccountNumber, string poSource, out int poRecordCount, out string poErrorMessage, out string poStoreIDErrorMessage, out string poShipViaErrorMessage, out string poSKUsErrorMessage, out string errorMessage)
        {
            int returnValue = 0;
            poRecordCount = 0;
            poErrorMessage = string.Empty;
            poStoreIDErrorMessage = string.Empty;
            poShipViaErrorMessage = string.Empty;
            poSKUsErrorMessage = string.Empty;
            errorMessage = string.Empty;
            string poXML = clsGeneral.SerializeObject(poList);
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            string sCode = string.Empty;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@piCompanyAccountNumber", companyAccountNumber);
                objCompHash.Add("@piXMLData", poXML);
                objCompHash.Add("@piPOSource", poSource);
                arrSpFieldSeq = new string[] { "@piCompanyAccountNumber", "@piXMLData", "@piPOSource" };
                returnValue = db.ExCommand(objCompHash, "av_PurchaseOrder_Bulk_Create", arrSpFieldSeq, "@poRecordCount", "@poErrorMessage", "@poStoreIDErrorMessage", "@poSKUsErrorMessage", "@poShipViaErrorMessage", out poRecordCount, out poErrorMessage, out poStoreIDErrorMessage, out poSKUsErrorMessage, out poShipViaErrorMessage);
                
            }
            catch (Exception objExp)
            {
                errorMessage = "av_PurchaseOrder_Bulk_Create: " + objExp.Message.ToString();
            }
            finally
            {
                objCompHash = null;
                arrSpFieldSeq = null;
            }
            

            return returnValue;
        }
        public static PurchaseOrderResponse SaveNewPurchaseOrderCompany(clsPurchaseOrder purchaseOrder, int CompanyID, int userID, string fileName, string comment)
        {
            String errorMessage = string.Empty;
            Hashtable shipByHashTable = null;
            PurchaseOrderResponse response = new PurchaseOrderResponse();
            //shipByHashTable = avii.Classes.PurchaseOrder.GetShipBy();
            //if (shipByHashTable.ContainsKey(purchaseOrder.ShipThrough))
            {
                errorMessage = CreatePurchaseOrderCompanyDB(BaseAerovoice.SerializeObject<clsPurchaseOrder>(purchaseOrder), CompanyID, userID, fileName, comment);

                //errorMessage = purchaseOrder.ShipThrough + " " + ResponseErrorCode.ShipByIsNotCorrect.ToString();

                if (errorMessage.Equals(ResponseErrorCode.UploadedSuccessfully.ToString()))
                {
                    response.ErrorCode = string.Empty;// ResponseErrorCode.UploadedSuccessfully.ToString();
                    response.PurchaseOrderNumber = purchaseOrder.PurchaseOrderNumber;
                    response.Comment = string.Empty;
                }
                else
                {
                    response.Comment = errorMessage;
                    if (errorMessage.Equals(ResponseErrorCode.PurchaseOrderAlreadyExists.ToString()))
                    {
                        response.PurchaseOrderNumber = purchaseOrder.PurchaseOrderNumber;
                        response.ErrorCode = ResponseErrorCode.PurchaseOrderAlreadyExists.ToString();
                    }
                    else
                        if (errorMessage.Equals(ResponseErrorCode.PurchaseOrderAlreadyExists.ToString()))
                        {
                            response.PurchaseOrderNumber = purchaseOrder.PurchaseOrderNumber;
                            response.ErrorCode = ResponseErrorCode.PurchaseOrderAlreadyExists.ToString();
                        }
                        else
                        {
                            response.PurchaseOrderNumber = string.Empty;
                            response.ErrorCode = ResponseErrorCode.ErrowWhileLoadingData.ToString();
                            response.Comment = errorMessage;
                        }
                }
            }
            //else
            //{
            //    //errorMessage = ResponseErrorCode.ShipByIsNotCorrect.ToString();
            //    response.PurchaseOrderNumber = purchaseOrder.PurchaseOrderNumber;
            //    response.ErrorCode = ResponseErrorCode.ShipByIsNotCorrect.ToString();
            //}
            return response;
        }

        private static String UTF8ByteArrayToString(Byte[] characters)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            String constructedString = encoding.GetString(characters);
            return (constructedString);
        }

        private static string CreatePurchaseOrderDB(string POXml, int UserID, int forecastGUID, string poFlag, bool IsInterNational, out string poNumber)
        {
            SV.Framework.Fulfillment.LogOperations logOperations = SV.Framework.Fulfillment.LogOperations.CreateInstance<SV.Framework.Fulfillment.LogOperations>();

            SV.Framework.Models.Fulfillment.FulfillmentLogModel logModel = new SV.Framework.Models.Fulfillment.FulfillmentLogModel();
            PurchaseOrderResponse response = new PurchaseOrderResponse();

            logModel.ActionName = "Fulfillment Create";
            logModel.CreateUserID = UserID;
            logModel.StatusID = 1;
            logModel.PO_ID = 0;
            logModel.FulfillmentNumber = string.Empty;

            string errorMessage = string.Empty;
            poNumber = string.Empty;
            int returnValue = 0;
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            string sCode2 = string.Empty;
            Hashtable objCompHash = new Hashtable();
            try
            {

                POXml = "<purchaseorder>" + POXml.Substring(POXml.IndexOf("<purchaseordernumber>"));
                logModel.RequestData = POXml;
                objCompHash.Add("@Po_Xml", POXml);
                objCompHash.Add("@UserID", UserID);
                objCompHash.Add("@ForecastGUID", forecastGUID);
                objCompHash.Add("@POFlag", poFlag);
                objCompHash.Add("@IsInternational", IsInterNational);
                arrSpFieldSeq = new string[] { "@Po_Xml", "@UserID", "@ForecastGUID", "@POFlag", "@IsInternational" };
                //returnValue = db.ExecCommand(objCompHash, "Aero_PurchaseOrder_Create", arrSpFieldSeq);
                returnValue = db.ExCommand(objCompHash, "Aero_PurchaseOrder_Create", arrSpFieldSeq, "@poNumber", "@outparam", out poNumber, out sCode2);
                if (returnValue == 0 && string.IsNullOrEmpty(sCode2))
                { errorMessage = ResponseErrorCode.UploadedSuccessfully.ToString();

                    logModel.PO_ID = returnValue;
                    logModel.FulfillmentNumber = poNumber;
                    response.ErrorCode = string.Empty;// ResponseErrorCode.UploadedSuccessfully.ToString();
                    response.PurchaseOrderNumber = poNumber;
                    response.Comment = errorMessage;

                }
                else
                {
                    if (returnValue > 0)
                    {
                        errorMessage = ResponseErrorCode.PurchaseOrderAlreadyExists.ToString();
                        response.PurchaseOrderNumber = poNumber;
                        response.ErrorCode = errorMessage;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(sCode2))
                        {
                            errorMessage = ResponseErrorCode.StroreIDNotExists.ToString();
                            response.PurchaseOrderNumber = poNumber;
                            response.ErrorCode = errorMessage;

                        }
                    }
                }                
            }
            catch (Exception objExp)
            {
                errorMessage = "CreatePurchaseOrderDB:" + objExp.Message.ToString();
                response.PurchaseOrderNumber = string.Empty;
                response.ErrorCode = errorMessage;
                response.Comment = errorMessage;
                logModel.Comment = errorMessage;
            }
            finally
            {
                string POreposponseXml = BaseAerovoice.SerializeObject<PurchaseOrderResponse>(response);
                POreposponseXml = "<PurchaseOrderResponse>" + POreposponseXml.Substring(POreposponseXml.IndexOf("<PurchaseOrderNumber>"));

                logModel.ResponseData = POreposponseXml;// BaseAerovoice.SerializeObject<PurchaseOrderResponse>(response);

                logOperations.FulfillmentLogInsert(logModel);
                objCompHash = null;
                arrSpFieldSeq = null;
            }
            return errorMessage;
        }

        private static string CreatePurchaseOrderCompanyDB(string POXml, int CompanyID, int userID, string fileName, string comment)
        {
            int returnValue = 0;
            string errorMessage = string.Empty;
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            string sCode = string.Empty;
            Hashtable objCompHash = new Hashtable();
            try
            {

                POXml = "<purchaseorder>" + POXml.Substring(POXml.IndexOf("<purchaseordernumber>"));
                objCompHash.Add("@Po_Xml", POXml);
                objCompHash.Add("@CompanyID", CompanyID);
                objCompHash.Add("@piUserID", userID);
                objCompHash.Add("@piFileName", fileName);
                objCompHash.Add("@piComment", comment);


                arrSpFieldSeq = new string[] { "@Po_Xml", "@CompanyID", "@piUserID", "@piFileName", "@piComment" };

                returnValue = db.ExecCommand(objCompHash, "Aero_PurchaseOrder_Company_Create", arrSpFieldSeq);
                if (returnValue == 0)
                    errorMessage = ResponseErrorCode.UploadedSuccessfully.ToString();
                else
                    errorMessage = ResponseErrorCode.PurchaseOrderAlreadyExists.ToString();
            }
            catch (Exception objExp)
            {
                errorMessage = "CreatePurchaseOrderDB:" + objExp.Message.ToString(); 
            }
            finally
            {
                objCompHash = null;
                arrSpFieldSeq = null;
            }
            return errorMessage;
        }

        private static string CreateNewPoDB(string POXml, int UserID)
        {
            string errorMessage = string.Empty;
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            string sCode = string.Empty;
            Hashtable objCompHash = new Hashtable();
            try
            {
                POXml = "<purchaseorder>" + POXml.Substring(POXml.IndexOf("<purchaseordernumber>"));
                objCompHash.Add("@Po_Xml", POXml);
                objCompHash.Add("@UserID", UserID);
                arrSpFieldSeq = new string[] { "@Po_Xml", "@UserID" };
                db.ExeCommand(objCompHash, "Aero_PurchaseOrder_Create", arrSpFieldSeq);
                errorMessage = ResponseErrorCode.UploadedSuccessfully.ToString();
            }
            catch (Exception objExp)
            {
                errorMessage = "CreateNewPoDB:" + objExp.Message.ToString();
            }
            finally
            {
                objCompHash = null;
                arrSpFieldSeq = null;
            }
            return errorMessage;
        }

        private static ShipTracking GetPurchaseOrderShippingDB(string purchaseOrderNumber, int userID)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            ShipTracking shippingInfo = null;  
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@PO_Num", purchaseOrderNumber);
                objCompHash.Add("@UserID", userID);

                arrSpFieldSeq = new string[] { "@PO_Num", "@UserID" };
                shippingInfo = PopulatePurchaseOrderShipping(db.GetTableRecords(objCompHash, "Aero_PurchaseOrder_Shipping_Select", arrSpFieldSeq));
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }
            return shippingInfo;
        }

        private static void SetTrackingInfoDB(string trackingNumber, int companyID, string purchaseOrderNumber, string avOrder, int userID)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@CompanyID", companyID);
                objCompHash.Add("@PO_Num", purchaseOrderNumber);
                objCompHash.Add("@TrackingNum", trackingNumber);
                objCompHash.Add("@POStatus", PurchaseOrderStatus.Shipped);
                objCompHash.Add("@AVOrderNumber", avOrder);
                objCompHash.Add("@UserID", userID);

                arrSpFieldSeq = new string[] { "@CompanyID", "@PO_Num", "@TrackingNum", "@POStatus", "@AVOrderNumber", "@UserID" };
                db.ExeCommand(objCompHash, "Aero_PurchaseOrderTracking_Insert", arrSpFieldSeq);
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }
        }
        private static List<InventorySKU> GetInventorySKUList(int userID)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataTable dt = new DataTable();
            Hashtable objCompHash = new Hashtable();
            List<InventorySKU> inventorySKUList = null;
            try
            {
                objCompHash.Add("@UserID", userID);

                arrSpFieldSeq = new string[] { "@UserID" };
                dt = db.GetTableRecords(objCompHash, "av_CustomerInventoryList_Select", arrSpFieldSeq);
                inventorySKUList = PopulateInventoryList(dt);

            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db = null;
            }

            return inventorySKUList;
        }
        public static List<InventorySKU> PopulateInventoryList(DataTable dataTable)
        {

            List<InventorySKU> inventoryItemList = new List<InventorySKU>();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                InventorySKU objInventoryItem = new InventorySKU();
                objInventoryItem.SKU = clsGeneral.getColumnData(dataRow, "SKU", string.Empty, false) as string;
                objInventoryItem.ItemName = clsGeneral.getColumnData(dataRow, "ItemName", string.Empty, false) as string;
                objInventoryItem.ModelNumber = clsGeneral.getColumnData(dataRow, "ModelNumber", string.Empty, false) as string;
                objInventoryItem.CategoryName = clsGeneral.getColumnData(dataRow, "CategoryName", string.Empty, false) as string;
                //objInventoryItem.Color = clsGeneral.getColumnData(dataRow, "color", string.Empty, false) as string;
                objInventoryItem.UPC = clsGeneral.getColumnData(dataRow, "upc", string.Empty, false) as string;
                objInventoryItem.OEM = clsGeneral.getColumnData(dataRow, "makername", string.Empty, false) as string;
                //objInventoryItem.Carrier = clsGeneral.getColumnData(dataRow, "CarrierName", string.Empty, false) as string;
                


                inventoryItemList.Add(objInventoryItem);
            }
            return inventoryItemList;
        }

        private static PurchaseOrders PopulatePurchaseOrdersDownload(DataSet ds)
        {
            DateTime shipDateCheck;
            PurchaseOrders purchaseOrders = null;
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                purchaseOrders = new PurchaseOrders();
                BasePurchaseOrder purchaseOrder = null;
                foreach (DataRow dataRow in ds.Tables[0].Rows)
                {

                    purchaseOrder = new BasePurchaseOrder(Convert.ToInt32(clsGeneral.getColumnData(dataRow, "PO_ID", 0, true)),
                        (string)clsGeneral.getColumnData(dataRow, "AerovoiceSalesOrderNumber", string.Empty, true));
                    purchaseOrder.StatusColor = (string)clsGeneral.getColumnData(dataRow, "StatusColor", string.Empty, false);

                    purchaseOrder.PurchaseOrderDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "PO_Date", DateTime.Now, true));
                    purchaseOrder.PurchaseOrderNumber = (string)clsGeneral.getColumnData(dataRow, "PO_Num", string.Empty, false);
                    purchaseOrder.CustomerAccountNumber = (string)clsGeneral.getColumnData(dataRow, "CustomerAccountNumber", string.Empty, false);
                    purchaseOrder.StoreID = (string)clsGeneral.getColumnData(dataRow, "Store_ID", string.Empty, false);
                    purchaseOrder.Comments = (string)clsGeneral.getColumnData(dataRow, "Comments", string.Empty, false);
                    //purchaseOrder.Shipping.AerovoiceSalesOrderNumber = (string)clsGeneral.getColumnData(dataRow, "AerovoiceSalesOrderNumber", string.Empty, false);
                    purchaseOrder.Shipping.ContactName = (string)clsGeneral.getColumnData(dataRow, "Contact_Name", string.Empty, true);
                    purchaseOrder.Shipping.ContactPhone = (string)clsGeneral.getColumnData(dataRow, "Contact_Phone", string.Empty, false);
                    purchaseOrder.Shipping.ShipToAddress = (string)clsGeneral.getColumnData(dataRow, "ShipTo_Address", string.Empty, false);
                    purchaseOrder.Shipping.ShipToAddress2 = (string)clsGeneral.getColumnData(dataRow, "ShipTo_Address2", string.Empty, false);
                    purchaseOrder.Shipping.ShipToAttn = (string)clsGeneral.getColumnData(dataRow, "ShipTo_Attn", string.Empty, false);
                    purchaseOrder.Shipping.ShipToCity = (string)clsGeneral.getColumnData(dataRow, "ShipTo_City", string.Empty, true);
                    purchaseOrder.Shipping.ShipToState = (string)clsGeneral.getColumnData(dataRow, "ShipTo_State", string.Empty, false);
                    purchaseOrder.Shipping.ShipToZip = (string)clsGeneral.getColumnData(dataRow, "ShipTo_Zip", string.Empty, true);
                    purchaseOrder.Tracking.ShipToTrackingNumber = (string)clsGeneral.getColumnData(dataRow, "TrackingNumber", string.Empty, true);
                    purchaseOrder.CustomerNumber = (string)clsGeneral.getColumnData(dataRow, "customernumber", string.Empty, true);
                    purchaseOrder.CustomerName = (string)clsGeneral.getColumnData(dataRow, "companyname", string.Empty, false);

                    //purchaseOrder.ShipThrough = (string)clsGeneral.getColumnData(dataRow, "Ship_Via", string.Empty, true);
                    shipDateCheck = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "ShipTo_Date", DateTime.MinValue, false));
                    if (shipDateCheck != DateTime.MinValue)
                        purchaseOrder.Tracking.ShipToDate = shipDateCheck;

                    purchaseOrder.Tracking.ShipToBy = (string)clsGeneral.getColumnData(dataRow, "Ship_Via", string.Empty, false);
                    purchaseOrder.Tracking.PurchaseOrderNumber = purchaseOrder.PurchaseOrderNumber;
                    purchaseOrder.SentASN = (string)clsGeneral.getColumnData(dataRow, "SentASN", string.Empty, false);
                    purchaseOrder.SentESN = (string)clsGeneral.getColumnData(dataRow, "SentESN", string.Empty, false);
                    purchaseOrder.CompanyID = Convert.ToInt16(clsGeneral.getColumnData(dataRow, "CompanyID", 0, false));
                    purchaseOrder.PurchaseOrderStatus = (avii.Classes.PurchaseOrderStatus)Convert.ToInt16(clsGeneral.getColumnData(dataRow, "Status", 1, false));
                    purchaseOrder.PurchaseOrderStatusID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Status", 0, false));
                    purchaseOrder.ModifiedDate = (string)clsGeneral.getColumnData(dataRow, "LastUpdateDate", string.Empty, false);
                    purchaseOrder.ReturnLabel = (string)clsGeneral.getColumnData(dataRow, "ReturnLabel", string.Empty, false);
                    purchaseOrder.ReturnShipVia = (string)clsGeneral.getColumnData(dataRow, "ReturnShipVia", string.Empty, false);
                    if (ds.Tables.Count > 1 && ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                    {
                        BasePurchaseOrderItem purchaseOrderItem;
                        foreach (DataRow dataRowItem in ds.Tables[1].Select("POID = " + purchaseOrder.PurchaseOrderID.ToString()))
                        {
                            purchaseOrderItem = new BasePurchaseOrderItem(Convert.ToInt32(clsGeneral.getColumnData(dataRowItem, "POD_ID", 0, false)));
                            purchaseOrderItem.LineNo = Convert.ToInt32(clsGeneral.getColumnData(dataRowItem, "Line_no", 0, false));
                            purchaseOrderItem.ESN = (string)clsGeneral.getColumnData(dataRowItem, "ESN", string.Empty, false);
                            purchaseOrderItem.ItemCode = (string)clsGeneral.getColumnData(dataRowItem, "Item_Code", string.Empty, false);
                            purchaseOrderItem.MdnNumber = (string)clsGeneral.getColumnData(dataRowItem, "MDN", string.Empty, false);
                            purchaseOrderItem.Quantity = (int?)clsGeneral.getColumnData(dataRowItem, "Qty", 0, false);
                            purchaseOrderItem.BoxId = (string)clsGeneral.getColumnData(dataRowItem, "Box_id", string.Empty, false);
                            purchaseOrderItem.UPC = (string)clsGeneral.getColumnData(dataRowItem, "UPC", string.Empty, false);
                            purchaseOrderItem.FmUPC = (string)clsGeneral.getColumnData(dataRowItem, "fmupc", string.Empty, false);
                            purchaseOrderItem.WarehouseCode = (string)clsGeneral.getColumnData(dataRowItem, "whCode", string.Empty, false);
                            purchaseOrderItem.WholesaleCost = Convert.ToDouble(clsGeneral.getColumnData(dataRowItem, "cost", 0, false));
                            //purchaseOrderItem.TilaDocument = (string)clsGeneral.getColumnData(dataRowItem, "tilaDocument", string.Empty, false);
                            //purchaseOrderItem.V2Document = (string)clsGeneral.getColumnData(dataRowItem, "v2document", string.Empty, false);

                            //purchaseOrderItem.PhoneCategory = (PhoneCategoryType)Convert.ToChar(clsGeneral.getColumnData(dataRowItem, "PhoneCategory", "C", false));
                            purchaseOrderItem.MslNumber = (string)clsGeneral.getColumnData(dataRowItem, "mslNumber", string.Empty, false);
                            purchaseOrderItem.MsID = (string)clsGeneral.getColumnData(dataRowItem, "MSID", string.Empty, false);
                            purchaseOrderItem.PassCode = (string)clsGeneral.getColumnData(dataRowItem, "pass_code", string.Empty, false);
                            purchaseOrderItem.PODStatus = (avii.Classes.PurchaseOrderStatus)Convert.ToInt32(clsGeneral.getColumnData(dataRow, "StatusID", 1, false));
                            purchaseOrderItem.StatusID = Convert.ToInt32(clsGeneral.getColumnData(dataRowItem, "StatusID", 1, false));
                            purchaseOrderItem.LTEICCID = (string)clsGeneral.getColumnData(dataRowItem, "LTEICCID", string.Empty, false);
                            purchaseOrderItem.LTEIMSI = (string)clsGeneral.getColumnData(dataRowItem, "LTEIMSI", string.Empty, false);
                            purchaseOrderItem.Otksal = (string)clsGeneral.getColumnData(dataRowItem, "otksl", string.Empty, false);
                            purchaseOrderItem.Akey = (string)clsGeneral.getColumnData(dataRowItem, "akey", string.Empty, false);
                            purchaseOrderItem.SimNumber = (string)clsGeneral.getColumnData(dataRowItem, "SimNumber", string.Empty, false);
                            purchaseOrderItem.TrackingNumber = (string)clsGeneral.getColumnData(dataRowItem, "TrackingNumber", string.Empty, false);

                            purchaseOrder.PurchaseOrderItems.Add(purchaseOrderItem);
                        }
                    }

                    purchaseOrders.PurchaseOrderList.Add(purchaseOrder);
                }
            }

            return purchaseOrders;
        }

        private static PurchaseOrders PopulatePurchaseOrders(DataSet ds)
        {
            DateTime shipDateCheck;
            PurchaseOrders purchaseOrders = null;
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                purchaseOrders = new PurchaseOrders();
                BasePurchaseOrder purchaseOrder = null;
                foreach (DataRow dataRow in ds.Tables[0].Rows)
                {

                    purchaseOrder = new BasePurchaseOrder(Convert.ToInt32(clsGeneral.getColumnData(dataRow, "PO_ID", 0, true)),
                        (string)clsGeneral.getColumnData(dataRow, "AerovoiceSalesOrderNumber", string.Empty, true));
                    purchaseOrder.StatusColor = (string)clsGeneral.getColumnData(dataRow, "StatusColor", string.Empty, false);
                    
                    purchaseOrder.PurchaseOrderDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "PO_Date", DateTime.Now, true));
                    purchaseOrder.PurchaseOrderNumber = (string)clsGeneral.getColumnData(dataRow, "PO_Num", string.Empty, false);
                    purchaseOrder.CustomerAccountNumber = (string)clsGeneral.getColumnData(dataRow, "CustomerAccountNumber", string.Empty, false);
                    purchaseOrder.StoreID = (string)clsGeneral.getColumnData(dataRow, "Store_ID", string.Empty, false);
                    purchaseOrder.Comments = (string)clsGeneral.getColumnData(dataRow, "Comments", string.Empty, false);
                    //purchaseOrder.Shipping.AerovoiceSalesOrderNumber = (string)clsGeneral.getColumnData(dataRow, "AerovoiceSalesOrderNumber", string.Empty, false);
                    purchaseOrder.Shipping.ContactName = (string)clsGeneral.getColumnData(dataRow, "Contact_Name", string.Empty, true);
                    purchaseOrder.Shipping.ContactPhone = (string)clsGeneral.getColumnData(dataRow, "Contact_Phone", string.Empty, false);
                    purchaseOrder.Shipping.ShipToAddress = (string)clsGeneral.getColumnData(dataRow, "ShipTo_Address", string.Empty, false);
                    purchaseOrder.Shipping.ShipToAddress2 = (string)clsGeneral.getColumnData(dataRow, "ShipTo_Address2", string.Empty, false);
                    purchaseOrder.Shipping.ShipToAttn = (string)clsGeneral.getColumnData(dataRow, "ShipTo_Attn", string.Empty, false);
                    purchaseOrder.Shipping.ShipToCity = (string)clsGeneral.getColumnData(dataRow, "ShipTo_City", string.Empty, true);
                    purchaseOrder.Shipping.ShipToState = (string)clsGeneral.getColumnData(dataRow, "ShipTo_State", string.Empty, false);
                    purchaseOrder.Shipping.ShipToZip = (string)clsGeneral.getColumnData(dataRow, "ShipTo_Zip", string.Empty, true);
                    purchaseOrder.Tracking.ShipToTrackingNumber = (string)clsGeneral.getColumnData(dataRow, "TrackingNumber", string.Empty, true);
                    purchaseOrder.CustomerNumber = (string)clsGeneral.getColumnData(dataRow, "customernumber", string.Empty, true);
                    purchaseOrder.CustomerName = (string)clsGeneral.getColumnData(dataRow, "companyname", string.Empty, false);

                    //purchaseOrder.ShipThrough = (string)clsGeneral.getColumnData(dataRow, "Ship_Via", string.Empty, true);
                    shipDateCheck = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "ShipTo_Date", DateTime.MinValue, false));
                    if (shipDateCheck != DateTime.MinValue)
                        purchaseOrder.Tracking.ShipToDate = shipDateCheck;

                    purchaseOrder.Tracking.ShipToBy = (string)clsGeneral.getColumnData(dataRow, "Ship_Via", string.Empty, false);
                    purchaseOrder.Tracking.PurchaseOrderNumber = purchaseOrder.PurchaseOrderNumber;
                    purchaseOrder.SentASN = (string)clsGeneral.getColumnData(dataRow, "SentASN", string.Empty, false);
                    purchaseOrder.SentESN = (string)clsGeneral.getColumnData(dataRow, "SentESN", string.Empty, false);
                    purchaseOrder.CompanyID = Convert.ToInt16(clsGeneral.getColumnData(dataRow, "CompanyID", 0, false));
                    purchaseOrder.PurchaseOrderStatus = (avii.Classes.PurchaseOrderStatus)Convert.ToInt16(clsGeneral.getColumnData(dataRow, "Status", 1, false));
                    purchaseOrder.PurchaseOrderStatusID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Status", 0, false));
                    purchaseOrder.ModifiedDate = (string)clsGeneral.getColumnData(dataRow, "LastUpdateDate", string.Empty, false);
                    purchaseOrder.ReturnLabel = (string)clsGeneral.getColumnData(dataRow, "ReturnLabel", string.Empty, false);
                    if (ds.Tables.Count > 1 && ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                    {
                        BasePurchaseOrderItem purchaseOrderItem;
                        foreach (DataRow dataRowItem in ds.Tables[1].Select("POID = " + purchaseOrder.PurchaseOrderID.ToString()))
                        {
                            purchaseOrderItem = new BasePurchaseOrderItem(Convert.ToInt32(clsGeneral.getColumnData(dataRowItem, "POD_ID", 0, false)));
                            purchaseOrderItem.LineNo = Convert.ToInt32(clsGeneral.getColumnData(dataRowItem, "Line_no", 0, false));
                            purchaseOrderItem.ESN = (string)clsGeneral.getColumnData(dataRowItem, "ESN", string.Empty, false);
                            purchaseOrderItem.ItemCode = (string)clsGeneral.getColumnData(dataRowItem, "Item_Code", string.Empty, false);
                            purchaseOrderItem.MdnNumber = (string)clsGeneral.getColumnData(dataRowItem, "MDN", string.Empty, false);
                            purchaseOrderItem.Quantity = (int?)clsGeneral.getColumnData(dataRowItem, "Qty", 0, false);
                            purchaseOrderItem.BoxId = (string)clsGeneral.getColumnData(dataRowItem, "Box_id", string.Empty, false);
                            purchaseOrderItem.UPC = (string)clsGeneral.getColumnData(dataRowItem, "UPC", string.Empty, false);
                            purchaseOrderItem.FmUPC = (string)clsGeneral.getColumnData(dataRowItem, "fmupc", string.Empty, false);
                            purchaseOrderItem.WarehouseCode = (string)clsGeneral.getColumnData(dataRowItem, "whCode", string.Empty, false);
                            purchaseOrderItem.WholesaleCost = Convert.ToDouble(clsGeneral.getColumnData(dataRowItem, "cost", 0, false));
                            //purchaseOrderItem.TilaDocument = (string)clsGeneral.getColumnData(dataRowItem, "tilaDocument", string.Empty, false);
                            //purchaseOrderItem.V2Document = (string)clsGeneral.getColumnData(dataRowItem, "v2document", string.Empty, false);
                            
                            //purchaseOrderItem.PhoneCategory = (PhoneCategoryType)Convert.ToChar(clsGeneral.getColumnData(dataRowItem, "PhoneCategory", "C", false));
                            purchaseOrderItem.MslNumber = (string)clsGeneral.getColumnData(dataRowItem, "mslNumber", string.Empty, false);
                            purchaseOrderItem.MsID = (string)clsGeneral.getColumnData(dataRowItem, "MSID", string.Empty, false);
                            purchaseOrderItem.PassCode = (string)clsGeneral.getColumnData(dataRowItem, "pass_code", string.Empty, false);
                            purchaseOrderItem.PODStatus = (avii.Classes.PurchaseOrderStatus)Convert.ToInt32(clsGeneral.getColumnData(dataRow, "StatusID", 1, false));
                            purchaseOrderItem.StatusID = Convert.ToInt32(clsGeneral.getColumnData(dataRowItem, "StatusID", 1, false));
                            purchaseOrderItem.LTEICCID = (string)clsGeneral.getColumnData(dataRowItem, "LTEICCID", string.Empty, false);
                            purchaseOrderItem.LTEIMSI = (string)clsGeneral.getColumnData(dataRowItem, "LTEIMSI", string.Empty, false);
                            purchaseOrderItem.Otksal = (string)clsGeneral.getColumnData(dataRowItem, "otksl", string.Empty, false);
                            purchaseOrderItem.Akey = (string)clsGeneral.getColumnData(dataRowItem, "akey", string.Empty, false);
                            purchaseOrderItem.SimNumber = (string)clsGeneral.getColumnData(dataRowItem, "SimNumber", string.Empty, false);
                            purchaseOrderItem.BatchNumber = (string)clsGeneral.getColumnData(dataRowItem, "BatchNumber", string.Empty, false);

                            purchaseOrder.PurchaseOrderItems.Add(purchaseOrderItem);
                        }
                    }
                    
                    purchaseOrders.PurchaseOrderList.Add(purchaseOrder);
                }
            }

            return purchaseOrders;
        }

        private static clsPurchaseOrder PopulatePurchaseOrder(DataSet ds)
        {
            clsPurchaseOrder purchaseOrder = null;
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dataRow = ds.Tables[0].Rows[0];

                purchaseOrder = new clsPurchaseOrder(Convert.ToInt32(clsGeneral.getColumnData(dataRow, "PO_ID", 0, true)),
                    (string)clsGeneral.getColumnData(dataRow, "AerovoiceSalesOrderNumber", string.Empty, true));
                purchaseOrder.PurchaseOrderDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "PO_Date", DateTime.Now, true));
                purchaseOrder.PurchaseOrderNumber = (string)clsGeneral.getColumnData(dataRow, "PO_Num", string.Empty, false);
                purchaseOrder.StoreID = (string)clsGeneral.getColumnData(dataRow, "Store_ID", string.Empty, false);
                purchaseOrder.Comments = (string)clsGeneral.getColumnData(dataRow, "Comments", string.Empty, false);
                //purchaseOrder.AerovoiceSalesOrderNumber = (string)clsGeneral.getColumnData(dataRow, "AerovoiceSalesOrderNumber", string.Empty, false);
                purchaseOrder.Shipping.ContactName = (string)clsGeneral.getColumnData(dataRow, "Contact_Name", string.Empty, true);
                purchaseOrder.Shipping.ContactPhone = (string)clsGeneral.getColumnData(dataRow, "Contact_Phone", string.Empty, false);
                purchaseOrder.Shipping.ShipToAddress = (string)clsGeneral.getColumnData(dataRow, "ShipTo_Address", string.Empty, false);
                purchaseOrder.Shipping.ShipToAddress2 = (string)clsGeneral.getColumnData(dataRow, "ShipTo_Address2", string.Empty, false);
                purchaseOrder.Shipping.ShipToAttn = (string)clsGeneral.getColumnData(dataRow, "ShipTo_Attn", string.Empty, false);
                purchaseOrder.ShipThrough = (string)clsGeneral.getColumnData(dataRow, "Ship_Via", string.Empty, false);
                purchaseOrder.Shipping.ShipToCity = (string)clsGeneral.getColumnData(dataRow, "ShipTo_City", string.Empty, true);
                //purchaseOrder.Shipping.ShipToDate = (DateTime)clsGeneral.getColumnData(dataRow, "ShipTo_Date", DateTime.Now, false);
                purchaseOrder.Shipping.ShipToState = (string)clsGeneral.getColumnData(dataRow, "ShipTo_State", string.Empty, false);
                purchaseOrder.Shipping.ShipToZip = (string)clsGeneral.getColumnData(dataRow, "ShipTo_Zip", string.Empty, true);
                purchaseOrder.PurchaseOrderStatus = (avii.Classes.PurchaseOrderStatus)Convert.ToInt16(clsGeneral.getColumnData(dataRow, "Status", 1, false));

                if (ds.Tables[1].Rows.Count > 0)
                {
                    PurchaseOrderItem purchaseOrderItem;
                    foreach (DataRow dataRowItem in ds.Tables[1].Select("POID = " + purchaseOrder.PurchaseOrderID.ToString()))
                    {
                        purchaseOrderItem = new PurchaseOrderItem(Convert.ToInt32(clsGeneral.getColumnData(dataRowItem, "POD_ID", 0, false)));
                        purchaseOrderItem.LineNo = Convert.ToInt32(clsGeneral.getColumnData(dataRowItem, "Line_no", 0, false));
                        purchaseOrderItem.ESN = (string)clsGeneral.getColumnData(dataRowItem, "ESN", string.Empty, false);
                        purchaseOrderItem.ItemCode = (string)clsGeneral.getColumnData(dataRowItem, "Item_Code", string.Empty, false);
                        //purchaseOrderItem.MdnNumber = (string)clsGeneral.getColumnData(dataRowItem, "MDN", string.Empty, false);
                        //purchaseOrderItem.SimNumber = (string)clsGeneral.getColumnData(dataRowItem, "SimNumber", string.Empty, false);
                        purchaseOrderItem.Quantity = (int?)clsGeneral.getColumnData(dataRowItem, "Qty", 0, false);
                        
                        //purchaseOrderItem.PhoneCategory = (PhoneCategoryType)Convert.ToChar(clsGeneral.getColumnData(dataRowItem, "PhoneCategory", "C", false));
                        
                        purchaseOrder.PurchaseOrderItems.Add(purchaseOrderItem);
                    }
                }
                if (ds.Tables.Count > 2 && ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
                {
                    ReturnLabel returnLabelItem;

                    foreach (DataRow dataRowItem in ds.Tables[2].Select("POID = " + purchaseOrder.PurchaseOrderID.ToString()))
                    {
                        returnLabelItem = new ReturnLabel();
                        returnLabelItem.TrackingNumber = (string)clsGeneral.getColumnData(dataRowItem, "TrackingNumber", string.Empty, false);
                        returnLabelItem.ShipmentType = (string)clsGeneral.getColumnData(dataRowItem, "ReturnLabel", string.Empty, false);
                        //ShipmentType shipmentType = (ShipmentType)Convert.ToChar(clsGeneral.getColumnData(dataRowItem, "ReturnLabels", string.Empty, false));

                        //returnLabelItem.ShipmentType = shipmentType;
                        returnLabelItem.ShipByCode = (string)clsGeneral.getColumnData(dataRowItem, "ShipByCode", string.Empty, false);
                        purchaseOrder.ReturnLabelItems.Add(returnLabelItem);
                    }
                }
                if (ds.Tables.Count > 3 && ds.Tables[3] != null && ds.Tables[3].Rows.Count > 0)
                {
                    List<FulfillmentComment> fulfillmentCommentList = new List<FulfillmentComment>();

                    FulfillmentComment fulfillmentComment;

                    foreach (DataRow dataRowItem in ds.Tables[3].Select("POID = " + purchaseOrder.PurchaseOrderID.ToString()))
                    {
                        fulfillmentComment = new FulfillmentComment();
                        fulfillmentComment.CommentDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRowItem, "CommentDate", string.Empty, false));
                        fulfillmentComment.Comments = (string)clsGeneral.getColumnData(dataRowItem, "Comments", string.Empty, false);
                        fulfillmentCommentList.Add(fulfillmentComment);


                    }
                    purchaseOrder.FulfillmentCommentList = fulfillmentCommentList;
                }
            }

            return purchaseOrder;
        }
        private static FulfillmentOrder PopulateFulfillmentOrder(DataSet ds)
        {
            int poID = 0;
            DateTime shipDate = DateTime.MinValue;
            FulfillmentOrder purchaseOrder = null;
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dataRow = ds.Tables[0].Rows[0];
                poID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "PO_ID", "0", true));

                if (poID != null && poID > 0)
                {
                    purchaseOrder = new FulfillmentOrder(Convert.ToInt32(poID));

                    purchaseOrder.PurchaseOrderDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "PO_Date", DateTime.Now, true));
                    purchaseOrder.PurchaseOrderNumber = (string)clsGeneral.getColumnData(dataRow, "PO_Num", string.Empty, false);
                    purchaseOrder.StoreID = (string)clsGeneral.getColumnData(dataRow, "Store_ID", string.Empty, false);
                    purchaseOrder.Comments = (string)clsGeneral.getColumnData(dataRow, "Comments", string.Empty, false);
                    //purchaseOrder.AerovoiceSalesOrderNumber = (string)clsGeneral.getColumnData(dataRow, "AerovoiceSalesOrderNumber", string.Empty, false);
                    purchaseOrder.Shipping.ContactName = (string)clsGeneral.getColumnData(dataRow, "Contact_Name", string.Empty, true);
                    purchaseOrder.Shipping.ContactPhone = (string)clsGeneral.getColumnData(dataRow, "Contact_Phone", string.Empty, false);
                    purchaseOrder.Shipping.ShipToAddress = (string)clsGeneral.getColumnData(dataRow, "ShipTo_Address", string.Empty, false);
                    purchaseOrder.Shipping.ShipToAddress2 = (string)clsGeneral.getColumnData(dataRow, "ShipTo_Address2", string.Empty, false);
                    purchaseOrder.Shipping.ShipToAttn = (string)clsGeneral.getColumnData(dataRow, "ShipTo_Attn", string.Empty, false);
                    purchaseOrder.Tracking.ShipToBy = (string)clsGeneral.getColumnData(dataRow, "Ship_Via", string.Empty, false);
                    DateTime.TryParse(clsGeneral.getColumnData(dataRow, "ShipTo_Date", DateTime.MinValue, false).ToString(), out shipDate);
                    purchaseOrder.Tracking.ShipToDate = shipDate;
                    purchaseOrder.Tracking.ShipToTrackingNumber = (string)clsGeneral.getColumnData(dataRow, "TrackingNumber", string.Empty, false);
                    purchaseOrder.Tracking.PurchaseOrderNumber = (string)clsGeneral.getColumnData(dataRow, "PO_Num", string.Empty, false);
                    purchaseOrder.Shipping.ShipToCity = (string)clsGeneral.getColumnData(dataRow, "ShipTo_City", string.Empty, true);
                    //purchaseOrder.Shipping.ShipToDate = (DateTime)clsGeneral.getColumnData(dataRow, "ShipTo_Date", DateTime.Now, false);
                    purchaseOrder.Shipping.ShipToState = (string)clsGeneral.getColumnData(dataRow, "ShipTo_State", string.Empty, false);
                    purchaseOrder.Shipping.ShipToZip = (string)clsGeneral.getColumnData(dataRow, "ShipTo_Zip", string.Empty, true);
                    purchaseOrder.PurchaseOrderStatus = (avii.Classes.PurchaseOrderStatus)Convert.ToInt16(clsGeneral.getColumnData(dataRow, "Status", 1, false));


                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        BasePurchaseOrderItem purchaseOrderItem;
                        foreach (DataRow dataRowItem in ds.Tables[1].Select("POID = " + purchaseOrder.PurchaseOrderID.ToString()))
                        {
                            purchaseOrderItem = new BasePurchaseOrderItem();
                            purchaseOrderItem.LineNo = Convert.ToInt32(clsGeneral.getColumnData(dataRowItem, "Line_no", 0, false));
                            purchaseOrderItem.ESN = (string)clsGeneral.getColumnData(dataRowItem, "ESN", string.Empty, false);
                            purchaseOrderItem.ItemCode = (string)clsGeneral.getColumnData(dataRowItem, "Item_Code", string.Empty, false);
                            purchaseOrderItem.MdnNumber = (string)clsGeneral.getColumnData(dataRowItem, "MDN", string.Empty, false);
                            purchaseOrderItem.Quantity = (int?)clsGeneral.getColumnData(dataRowItem, "Qty", 0, false);
                            purchaseOrderItem.FmUPC = (string)clsGeneral.getColumnData(dataRowItem, "FMUPC", string.Empty, false);
                            purchaseOrderItem.MslNumber = (string)clsGeneral.getColumnData(dataRowItem, "MslNumber", string.Empty, false);
                            purchaseOrderItem.LTEICCID = (string)clsGeneral.getColumnData(dataRowItem, "LTEICCID", string.Empty, false);
                            purchaseOrderItem.LTEIMSI = (string)clsGeneral.getColumnData(dataRowItem, "LTEIMSI", string.Empty, false);
                            purchaseOrderItem.Akey = (string)clsGeneral.getColumnData(dataRowItem, "akey", string.Empty, false);
                            purchaseOrderItem.Otksal = (string)clsGeneral.getColumnData(dataRowItem, "otksl", string.Empty, false);
                            purchaseOrderItem.SimNumber = (string)clsGeneral.getColumnData(dataRowItem, "SimNumber", string.Empty, false);
                            purchaseOrderItem.TrackingNumber = (string)clsGeneral.getColumnData(dataRowItem, "TrackingNumber", string.Empty, false);

                            purchaseOrderItem.PhoneCategory = (avii.Classes.PhoneCategoryType)Convert.ToChar(clsGeneral.getColumnData(dataRowItem, "PhoneCategory", "C", false));

                            purchaseOrder.PurchaseOrderItems.Add(purchaseOrderItem);
                        }
                    }
                    if (ds.Tables.Count > 2 && ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
                    {
                        //ReturnLabel returnLabelItem;

                        Shipment shipment = new Shipment();
                        List<ReturnShipment> returnShipmentList = new List<ReturnShipment>();
                        List<Shipping> shippingList = new List<Shipping>();
                        ReturnShipment returnShipment;
                        Shipping shipping;
                        string shipmentType = string.Empty;
                        foreach (DataRow dataRowItem in ds.Tables[2].Select("POID = " + purchaseOrder.PurchaseOrderID.ToString()))
                        {
                            shipmentType = Convert.ToString(clsGeneral.getColumnData(dataRowItem, "ReturnLabels", string.Empty, false)).ToUpper();
                            if (shipmentType == "R")
                            {
                                returnShipment = new ReturnShipment();
                                returnShipment.TrackingNumber = (string)clsGeneral.getColumnData(dataRowItem, "TrackingNumber", string.Empty, false);
                                returnShipment.ShipViaCode = (string)clsGeneral.getColumnData(dataRowItem, "ShipByCode", string.Empty, false);
                                DateTime.TryParse(clsGeneral.getColumnData(dataRow, "ShipDate", DateTime.MinValue, false).ToString(), out shipDate);
                                returnShipment.ReturnDate = shipDate;
                    
                                returnShipmentList.Add(returnShipment);
                            }
                            else
                            {
                                shipping = new Shipping();
                                shipping.TrackingNumber = (string)clsGeneral.getColumnData(dataRowItem, "TrackingNumber", string.Empty, false);
                                shipping.ShipViaCode = (string)clsGeneral.getColumnData(dataRowItem, "ShipByCode", string.Empty, false);
                                DateTime.TryParse(clsGeneral.getColumnData(dataRow, "ShipDate", DateTime.MinValue, false).ToString(), out shipDate);
                                shipping.ShipDate = shipDate;
                    
                                shippingList.Add(shipping);
                            }
                            shipment.ReturnshipmentList = returnShipmentList;
                            shipment.ShippingList = shippingList;
                            purchaseOrder.Shipment = shipment;
                        }
                    }
                    if (ds.Tables.Count > 3 && ds.Tables[3] != null && ds.Tables[3].Rows.Count > 0)
                    {
                        List<FulfillmentComment> fulfillmentCommentList = new List<FulfillmentComment>();
                        
                        FulfillmentComment fulfillmentComment;

                        foreach (DataRow dataRowItem in ds.Tables[3].Select("POID = " + purchaseOrder.PurchaseOrderID.ToString()))
                        {
                            fulfillmentComment = new FulfillmentComment();
                            fulfillmentComment.CommentDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRowItem, "CommentDate", string.Empty, false));
                            fulfillmentComment.Comments = (string)clsGeneral.getColumnData(dataRowItem, "Comments", string.Empty, false);
                            fulfillmentCommentList.Add(fulfillmentComment);

                            
                        }
                        purchaseOrder.FulfillmentCommentList = fulfillmentCommentList;
                    }
                }
            }

            return purchaseOrder;
        }

        //private static FulfillmentOrder PopulateFulfillmentOrder(DataSet ds)
        //{
        //    int poID = 0;
        //    FulfillmentOrder purchaseOrder = null;
        //    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //    {
        //        DataRow dataRow = ds.Tables[0].Rows[0];
        //        poID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "PO_ID", "0", true));

        //        if (poID != null & poID > 0)
        //        {
        //            purchaseOrder = new FulfillmentOrder(Convert.ToInt32(poID));

        //            purchaseOrder.PurchaseOrderDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "PO_Date", DateTime.Now, true));
        //            purchaseOrder.PurchaseOrderNumber = (string)clsGeneral.getColumnData(dataRow, "PO_Num", string.Empty, false);
        //            purchaseOrder.StoreID = (string)clsGeneral.getColumnData(dataRow, "Store_ID", string.Empty, false);
        //            purchaseOrder.Comments = (string)clsGeneral.getColumnData(dataRow, "Comments", string.Empty, false);
        //            //purchaseOrder.AerovoiceSalesOrderNumber = (string)clsGeneral.getColumnData(dataRow, "AerovoiceSalesOrderNumber", string.Empty, false);
        //            purchaseOrder.Shipping.ContactName = (string)clsGeneral.getColumnData(dataRow, "Contact_Name", string.Empty, true);
        //            purchaseOrder.Shipping.ContactPhone = (string)clsGeneral.getColumnData(dataRow, "Contact_Phone", string.Empty, false);
        //            purchaseOrder.Shipping.ShipToAddress = (string)clsGeneral.getColumnData(dataRow, "ShipTo_Address", string.Empty, false);
        //            purchaseOrder.Shipping.ShipToAddress2 = (string)clsGeneral.getColumnData(dataRow, "ShipTo_Address2", string.Empty, false);
        //            purchaseOrder.Shipping.ShipToAttn = (string)clsGeneral.getColumnData(dataRow, "ShipTo_Attn", string.Empty, false);
        //            purchaseOrder.Tracking.ShipToBy = (string)clsGeneral.getColumnData(dataRow, "Ship_Via", string.Empty, false);
        //            purchaseOrder.Tracking.ShipToDate = (DateTime)clsGeneral.getColumnData(dataRow, "ShipTo_Date", DateTime.MinValue, false);
        //            purchaseOrder.Tracking.ShipToTrackingNumber = (string)clsGeneral.getColumnData(dataRow, "TrackingNumber", string.Empty, false);
        //            purchaseOrder.Tracking.PurchaseOrderNumber = (string)clsGeneral.getColumnData(dataRow, "PO_Num", string.Empty, false);
        //            purchaseOrder.Shipping.ShipToCity = (string)clsGeneral.getColumnData(dataRow, "ShipTo_City", string.Empty, true);
        //            //purchaseOrder.Shipping.ShipToDate = (DateTime)clsGeneral.getColumnData(dataRow, "ShipTo_Date", DateTime.Now, false);
        //            purchaseOrder.Shipping.ShipToState = (string)clsGeneral.getColumnData(dataRow, "ShipTo_State", string.Empty, false);
        //            purchaseOrder.Shipping.ShipToZip = (string)clsGeneral.getColumnData(dataRow, "ShipTo_Zip", string.Empty, true);
        //            //purchaseOrder.PurchaseOrderStatus = (avii.Classes.PurchaseOrderStatus)Convert.ToInt16(clsGeneral.getColumnData(dataRow, "Status", 1, false));


        //            if (ds.Tables[1].Rows.Count > 0)
        //            {
        //                BasePurchaseOrderItem purchaseOrderItem;
        //                foreach (DataRow dataRowItem in ds.Tables[1].Select("POID = " + purchaseOrder.PurchaseOrderID.ToString()))
        //                {
        //                    purchaseOrderItem = new BasePurchaseOrderItem();
        //                    purchaseOrderItem.LineNo = Convert.ToInt32(clsGeneral.getColumnData(dataRowItem, "Line_no", 0, false));
        //                    purchaseOrderItem.ESN = (string)clsGeneral.getColumnData(dataRowItem, "ESN", string.Empty, false);
        //                    purchaseOrderItem.ItemCode = (string)clsGeneral.getColumnData(dataRowItem, "Item_Code", string.Empty, false);
        //                    purchaseOrderItem.MdnNumber = (string)clsGeneral.getColumnData(dataRowItem, "MDN", string.Empty, false);
        //                    purchaseOrderItem.Quantity = (int?)clsGeneral.getColumnData(dataRowItem, "Qty", 0, false);
        //                    purchaseOrderItem.FmUPC = (string)clsGeneral.getColumnData(dataRowItem, "FMUPC", string.Empty, false);
        //                    purchaseOrderItem.MslNumber = (string)clsGeneral.getColumnData(dataRowItem, "MslNumber", string.Empty, false);

        //                    purchaseOrderItem.PhoneCategory = (avii.Classes.PhoneCategoryType)Convert.ToChar(clsGeneral.getColumnData(dataRowItem, "PhoneCategory", "C", false));

        //                    purchaseOrder.PurchaseOrderItems.Add(purchaseOrderItem);
        //                }
        //            }
        //        }
        //    }

        //    return purchaseOrder;
        //}


        private static ShipTracking PopulatePurchaseOrderShipping(DataTable dataTable)
        {
            ShipTracking shippingInfo = null;
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                shippingInfo = new ShipTracking();
                DataRow dataRow = dataTable.Rows[0];
                shippingInfo.PurchaseOrderNumber = (string)clsGeneral.getColumnData(dataRow, "Po_Num", string.Empty, true);
                shippingInfo.ShipToDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "ShipTo_Date", "01/01/0001", false));
                shippingInfo.ShipToBy = (string)clsGeneral.getColumnData(dataRow, "Ship_Via", string.Empty, false);
                shippingInfo.ShipToTrackingNumber = (string)clsGeneral.getColumnData(dataRow, "TrackingNumber", string.Empty, false);
            }
            return shippingInfo; 
        }

        private static List<FulfillmentAssignESN> PopulateFulfillmentESN(DataTable dt)
        {

            FulfillmentAssignESN esnItem = null;
            List<FulfillmentAssignESN> purchaseOrderEsns = null;
            try
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    purchaseOrderEsns = new List<FulfillmentAssignESN>();
                        foreach (DataRow dRowItem in dt.Rows)
                        {
                            //purchaseOrderEsn = new FulfillmentAssignESN();
                        
                            esnItem = new FulfillmentAssignESN();
                            esnItem.FulfillmentNumber = (string)clsGeneral.getColumnData(dRowItem, "FulfillmentNumber", string.Empty, false);
                            esnItem.CustomerAccountNumber = (string)clsGeneral.getColumnData(dRowItem, "CompanyAccountNumber", string.Empty, false);
                            esnItem.ESN = (string)clsGeneral.getColumnData(dRowItem, "ESN", string.Empty, false);
                            esnItem.MSL = (string)clsGeneral.getColumnData(dRowItem, "MSL", string.Empty, false);
                            esnItem.SKU = (string)clsGeneral.getColumnData(dRowItem, "SKU", string.Empty, false);
                            esnItem.FMUPC = (string)clsGeneral.getColumnData(dRowItem, "fmupc", string.Empty, false);
                            esnItem.OTKSL = (string)clsGeneral.getColumnData(dRowItem, "OTKSL", string.Empty, false);
                            esnItem.AKEY = (string)clsGeneral.getColumnData(dRowItem, "AKEY", string.Empty, false);
                            esnItem.LteICCID = (string)clsGeneral.getColumnData(dRowItem, "LteICCID", string.Empty, false);
                            esnItem.LteIMSI = (string)clsGeneral.getColumnData(dRowItem, "LteIMSI", string.Empty, false);
                            
                            purchaseOrderEsns.Add(esnItem);
                        }

                        
                    
                }
            }
            catch (Exception ex)
            {
                throw new Exception("PopulatePurchaseOrderItems : " + ex.Message);
            }


            return purchaseOrderEsns;
        }

        private static List<PurchaseOrderESN> PopulatePurchaseOrderItems(DataSet ds)
        {

            PurchaseOrderESN purchaseOrderEsn = null;
            List<PurchaseOrderESN> purchaseOrderEsns = null;
            try
            {
                if (ds != null && ds.Tables.Count > 1)
                {
                    purchaseOrderEsns = new List<PurchaseOrderESN>();
                    foreach (DataRow dRowPO in ds.Tables[0].Rows)
                    {
                        purchaseOrderEsn = new PurchaseOrderESN();
                        purchaseOrderEsn.PurchaseOrderNumber = (string)clsGeneral.getColumnData(dRowPO, "PO_NUM", string.Empty, true);
                        purchaseOrderEsn.TrackingNumber = (string)clsGeneral.getColumnData(dRowPO, "TrackingNumber", string.Empty, false);
                        PurchaseOrderESNItem esnItem = null;
                        foreach (DataRow dRowItem in ds.Tables[1].Select("PO_ID =" + dRowPO["PO_ID"].ToString()))
                        {
                            esnItem = new PurchaseOrderESNItem();
                            esnItem.PodID = Convert.ToInt32(clsGeneral.getColumnData(dRowItem, "POD_ID", 0, false));
                            esnItem.ESN = (string)clsGeneral.getColumnData(dRowItem, "ESN", string.Empty, false);
                            esnItem.MslNumber = (string)clsGeneral.getColumnData(dRowItem, "MSLNumber", string.Empty, false);
                           // esnItem.StoreID = (string)clsGeneral.getColumnData(dRowItem, "whCode", string.Empty, false);
                            esnItem.ItemCode = (string)clsGeneral.getColumnData(dRowItem, "Item_Code", string.Empty, false);
                            esnItem.LineNo = Convert.ToInt32(clsGeneral.getColumnData(dRowItem, "Line_no", 0, false));
                            esnItem.FmUPC = (string)clsGeneral.getColumnData(dRowItem, "fmupc", string.Empty, false);
                            purchaseOrderEsn.PurchaseOrderESNItems.Add(esnItem);
                        }

                        purchaseOrderEsns.Add(purchaseOrderEsn);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("PopulatePurchaseOrderItems : " + ex.Message);
            }


            return purchaseOrderEsns;
        }

        private static List<ShipBy> PopulateShipBy(DataTable dataTable)
        {
            List<ShipBy> shipBylist = new List<ShipBy>();
            ShipBy shipByInfo = null;
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    shipByInfo = new ShipBy();
                    shipByInfo.ShipByCode = Convert.ToString(clsGeneral.getColumnData(dataRow, "ShipByCode", string.Empty, true));
                    shipByInfo.ShipByText = (string)clsGeneral.getColumnData(dataRow, "ShipByText", string.Empty, true);
                    shipByInfo.ShipCodeNText = (string)clsGeneral.getColumnData(dataRow, "ShipCodeNText", string.Empty, true);
                    shipByInfo.ShipByID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ShipByID", 0, true));

                    shipBylist.Add(shipByInfo);
                }
            }
            return shipBylist;
        }
        private static PurchaseOrders PopulatePurchaseOrdersNew(DataSet ds)
        {
            DateTime shipDateCheck;
            PurchaseOrders purchaseOrders = null;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                purchaseOrders = new PurchaseOrders();
                BasePurchaseOrder purchaseOrder = null;
                foreach (DataRow dataRow in ds.Tables[0].Rows)
                {
                    
                    purchaseOrder = new BasePurchaseOrder(Convert.ToInt32(clsGeneral.getColumnData(dataRow, "PO_ID", 0, true)),
                        (string)clsGeneral.getColumnData(dataRow, "AerovoiceSalesOrderNumber", string.Empty, true));
                    purchaseOrder.POSource = (string)clsGeneral.getColumnData(dataRow, "POSource", string.Empty, false);

                    purchaseOrder.IsShipmentRequired = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "IsShipmentRequired", false, false));
                    purchaseOrder.IsInterNational = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "IsInterNational", false, false));
                    
                    purchaseOrder.LineItemCount = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "LineItemCount", 0, false));
                    purchaseOrder.OrderSent = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "OrderSent", 0, false));
                    purchaseOrder.StatusColor = (string)clsGeneral.getColumnData(dataRow, "StatusColor", string.Empty, false);
                    purchaseOrder.PurchaseOrderDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "PO_Date", DateTime.Now, true));
                    purchaseOrder.PurchaseOrderNumber = (string)clsGeneral.getColumnData(dataRow, "PO_Num", string.Empty, false);
                    purchaseOrder.CustomerOrderNumber = (string)clsGeneral.getColumnData(dataRow, "CustomerOrderNumber", string.Empty, false);
                    purchaseOrder.RequestedShipDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "Requestedshipdate", DateTime.Now, true));

                    purchaseOrder.CustomerAccountNumber = (string)clsGeneral.getColumnData(dataRow, "CustomerAccountNumber", string.Empty, false);
                    purchaseOrder.StoreID = (string)clsGeneral.getColumnData(dataRow, "Store_ID", string.Empty, false);
                    purchaseOrder.Comments = (string)clsGeneral.getColumnData(dataRow, "Comments", string.Empty, false);
                    //purchaseOrder.Shipping.AerovoiceSalesOrderNumber = (string)clsGeneral.getColumnData(dataRow, "AerovoiceSalesOrderNumber", string.Empty, false);
                    purchaseOrder.Shipping.ContactName = (string)clsGeneral.getColumnData(dataRow, "Contact_Name", string.Empty, true);
                    purchaseOrder.Shipping.ContactPhone = (string)clsGeneral.getColumnData(dataRow, "Contact_Phone", string.Empty, false);
                    purchaseOrder.Shipping.ShipToAddress = (string)clsGeneral.getColumnData(dataRow, "ShipTo_Address", string.Empty, false);
                    purchaseOrder.Shipping.ShipToAddress2 = (string)clsGeneral.getColumnData(dataRow, "ShipTo_Address2", string.Empty, false);
                    purchaseOrder.Shipping.ShipToAttn = (string)clsGeneral.getColumnData(dataRow, "ShipTo_Attn", string.Empty, false);
                    purchaseOrder.Shipping.ShipToCity = (string)clsGeneral.getColumnData(dataRow, "ShipTo_City", string.Empty, true);
                    purchaseOrder.Shipping.ShipToState = (string)clsGeneral.getColumnData(dataRow, "ShipTo_State", string.Empty, false);
                    purchaseOrder.Shipping.ShipToZip = (string)clsGeneral.getColumnData(dataRow, "ShipTo_Zip", string.Empty, true);
                    purchaseOrder.Tracking.ShipToTrackingNumber = (string)clsGeneral.getColumnData(dataRow, "TrackingNumber", string.Empty, true);
                    purchaseOrder.CustomerNumber = (string)clsGeneral.getColumnData(dataRow, "customernumber", string.Empty, true);
                    purchaseOrder.CustomerName = (string)clsGeneral.getColumnData(dataRow, "companyname", string.Empty, false);
                    purchaseOrder.CompanyLogo = (string)clsGeneral.getColumnData(dataRow, "LogoPath", string.Empty, false);

                    //purchaseOrder.ShipThrough = (string)clsGeneral.getColumnData(dataRow, "Ship_Via", string.Empty, true);
                    shipDateCheck = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "ShipTo_Date", DateTime.MinValue, false));
                    if (shipDateCheck != DateTime.MinValue)
                        purchaseOrder.Tracking.ShipToDate = shipDateCheck;

                    purchaseOrder.Tracking.ShipToBy = (string)clsGeneral.getColumnData(dataRow, "Ship_Via", string.Empty, false);
                    purchaseOrder.Tracking.PurchaseOrderNumber = purchaseOrder.PurchaseOrderNumber;
                    //purchaseOrder.SentASN = (string)clsGeneral.getColumnData(dataRow, "SentASN", string.Empty, false);
                    //purchaseOrder.SentESN = (string)clsGeneral.getColumnData(dataRow, "SentESN", string.Empty, false);
                    purchaseOrder.CompanyID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CompanyID", 0, false));
                    purchaseOrder.PurchaseOrderStatus = (avii.Classes.PurchaseOrderStatus)Convert.ToInt16(clsGeneral.getColumnData(dataRow, "Status", 1, false));
                    purchaseOrder.PurchaseOrderStatusID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Status", 0, false));
                    purchaseOrder.ModifiedDate = (string)clsGeneral.getColumnData(dataRow, "LastUpdateDate", string.Empty, false);

                    if (ds.Tables.Count > 1 &&  ds.Tables[1].Rows.Count > 0)
                    {
                        BasePurchaseOrderItem purchaseOrderItem;
                        foreach (DataRow dataRowItem in ds.Tables[1].Select("POID = " + purchaseOrder.PurchaseOrderID.ToString()))
                        {
                            purchaseOrderItem = new BasePurchaseOrderItem(Convert.ToInt32(clsGeneral.getColumnData(dataRowItem, "POD_ID", 0, false)));
                            purchaseOrderItem.LineNo = Convert.ToInt32(clsGeneral.getColumnData(dataRowItem, "Line_no", 0, false));
                            purchaseOrderItem.ESN = (string)clsGeneral.getColumnData(dataRowItem, "ESN", string.Empty, false);
                            purchaseOrderItem.ItemCode = (string)clsGeneral.getColumnData(dataRowItem, "Item_Code", string.Empty, false);
                          //  purchaseOrderItem.MdnNumber = (string)clsGeneral.getColumnData(dataRowItem, "MDN", string.Empty, false);
                            purchaseOrderItem.Quantity = (int?)clsGeneral.getColumnData(dataRowItem, "Qty", 0, false);
                           // purchaseOrderItem.BoxId = (string)clsGeneral.getColumnData(dataRowItem, "Box_id", string.Empty, false);
                            purchaseOrderItem.UPC = (string)clsGeneral.getColumnData(dataRowItem, "UPC", string.Empty, false);
                          //  purchaseOrderItem.FmUPC = (string)clsGeneral.getColumnData(dataRowItem, "fmupc", string.Empty, false);
                            purchaseOrderItem.WarehouseCode = (string)clsGeneral.getColumnData(dataRowItem, "whCode", string.Empty, false);
                            purchaseOrderItem.WholesaleCost = Convert.ToDouble(clsGeneral.getColumnData(dataRowItem, "cost", 0, false));
                            //purchaseOrderItem.TilaDocument = (string)clsGeneral.getColumnData(dataRowItem, "tilaDocument", string.Empty, false);
                            //purchaseOrderItem.V2Document = (string)clsGeneral.getColumnData(dataRowItem, "v2document", string.Empty, false);

                            //purchaseOrderItem.PhoneCategory = (PhoneCategoryType)Convert.ToChar(clsGeneral.getColumnData(dataRowItem, "PhoneCategory", "C", false));
                            purchaseOrderItem.MslNumber = (string)clsGeneral.getColumnData(dataRowItem, "mslNumber", string.Empty, false);
                           // purchaseOrderItem.MsID = (string)clsGeneral.getColumnData(dataRowItem, "MSID", string.Empty, false);
                            purchaseOrderItem.LTEICCID = (string)clsGeneral.getColumnData(dataRowItem, "ICC_ID", string.Empty, false);
                            purchaseOrderItem.PODStatus = (avii.Classes.PurchaseOrderStatus)Convert.ToInt32(clsGeneral.getColumnData(dataRow, "StatusID", 1, false));
                            purchaseOrderItem.StatusID = Convert.ToInt32(clsGeneral.getColumnData(dataRowItem, "StatusID", 1, false));

                            purchaseOrder.PurchaseOrderItems.Add(purchaseOrderItem);
                        }
                    }

                    purchaseOrders.PurchaseOrderList.Add(purchaseOrder);
                }
            }

            return purchaseOrders;
        }

        private static List<FulfillmentStatus> PopulateFulfillmentStatusList(DataTable dt)
        {
            List<FulfillmentStatus> statusList = new List<FulfillmentStatus>();
            if (dt != null && dt.Rows.Count > 0)
            {
                FulfillmentStatus fulfillmentStatus;
                foreach (DataRow dataRowItem in dt.Rows)
                {
                    fulfillmentStatus = new FulfillmentStatus();
                    fulfillmentStatus.FulfillmentOrderStatus = (PurchaseOrderStatus)Convert.ToInt32(clsGeneral.getColumnData(dataRowItem, "StatusID", 1, false));
                    fulfillmentStatus.StatusCount = Convert.ToInt32(clsGeneral.getColumnData(dataRowItem, "StatusCount", 0, false));
                    statusList.Add(fulfillmentStatus);


                }
            }

            return statusList;
        }
        private static List<TrackingDetail> PopulateTrackingList(DataTable dt)
        {
            List<TrackingDetail> trackingList = new List<TrackingDetail>();

            TrackingDetail tracking = null;
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dataRow in dt.Rows)
                {
                    tracking = new TrackingDetail();
                    tracking.TrackingNumber = clsGeneral.getColumnData(dataRow, "Trackingnumber", string.Empty, false) as string;
                    tracking.ShipByCode = clsGeneral.getColumnData(dataRow, "ShipByCode", string.Empty, false) as string;
                    tracking.ShipByID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ShipByID", 0, false));
                    tracking.LineNumber = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "LineNumber", 0, false));
                    tracking.ShipDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "ShipDate", DateTime.MinValue, false));

                    tracking.Comments = clsGeneral.getColumnData(dataRow, "Comments", string.Empty, false) as string;
                    tracking.ReturnValue = clsGeneral.getColumnData(dataRow, "ReturnLabel", string.Empty, false) as string;
                    tracking.EsnCount = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "EsnCount", 0, false));
                    tracking.TrackingSentDateTime = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "TrackingSentDateTime", DateTime.MinValue, false));
                    tracking.ShipPackage = Convert.ToString(clsGeneral.getColumnData(dataRow, "ShipPackage", string.Empty, false));
                    tracking.ShipPrice = Convert.ToDecimal(clsGeneral.getColumnData(dataRow, "FinalPostage", 0, false));
                    tracking.ShipWeight = Convert.ToDecimal(clsGeneral.getColumnData(dataRow, "ShippingWeight", 0, false));




                    trackingList.Add(tracking);
                }
            }
            return trackingList;
        }

        private static List<FulfillmentSKUs> PopulateFufillmentSKUs(DataTable dt)
        {
            List<FulfillmentSKUs> purchaseOrderItemList = null;

            //BasePurchaseOrder purchaseOrder = null;


            if (dt != null && dt.Rows.Count > 0)
            {
                purchaseOrderItemList = new List<FulfillmentSKUs>();
                FulfillmentSKUs purchaseOrderItem;
                foreach (DataRow dataRowItem in dt.Rows)
                {
                    purchaseOrderItem = new FulfillmentSKUs();
                    purchaseOrderItem.POD_ID = Convert.ToInt32(clsGeneral.getColumnData(dataRowItem, "POD_ID", 0, false));
                    purchaseOrderItem.SKU = (string)clsGeneral.getColumnData(dataRowItem, "Item_Code", string.Empty, false);
                    purchaseOrderItem.Quantity = Convert.ToInt32(clsGeneral.getColumnData(dataRowItem, "Qty", 0, false));
                    purchaseOrderItem.AssignedQty = Convert.ToInt32(clsGeneral.getColumnData(dataRowItem, "AssignedQty", 0, false));
                    purchaseOrderItem.IsDelete = Convert.ToBoolean(clsGeneral.getColumnData(dataRowItem, "IsDelete", 0, false));

                    
                    purchaseOrderItemList.Add(purchaseOrderItem);
                }
            }





            return purchaseOrderItemList;
        }

        private static List<BasePurchaseOrderItem> PopulatePurchaseOrderItemList(DataTable dt)
        {
            List<BasePurchaseOrderItem> purchaseOrderItemList = null;

            //BasePurchaseOrder purchaseOrder = null;


            if (dt != null && dt.Rows.Count > 0)
            {
                int itemCount = 0;
                purchaseOrderItemList = new List<BasePurchaseOrderItem>();
                BasePurchaseOrderItem purchaseOrderItem;
                foreach (DataRow dataRowItem in dt.Rows)
                {
                    purchaseOrderItem = new BasePurchaseOrderItem(Convert.ToInt32(clsGeneral.getColumnData(dataRowItem, "POD_ID", 0, false)));
                    purchaseOrderItem.LineNo = Convert.ToInt32(clsGeneral.getColumnData(dataRowItem, "Line_No", 0, false));
                    purchaseOrderItem.ESN = (string)clsGeneral.getColumnData(dataRowItem, "ESN", string.Empty, false);
                    purchaseOrderItem.ItemCode = (string)clsGeneral.getColumnData(dataRowItem, "Item_Code", string.Empty, false);
                    //purchaseOrderItem.BatchNumber = (string)clsGeneral.getColumnData(dataRowItem, "BatchNumber", string.Empty, false);
                    purchaseOrderItem.Quantity = (int?)clsGeneral.getColumnData(dataRowItem, "Qty", 0, false);
                    //itemCount = (int)clsGeneral.getColumnData(dataRowItem, "ItemCount", 0, false);
                    //purchaseOrderItem.BoxId = itemCount.ToString();
                    purchaseOrderItem.UPC = (string)clsGeneral.getColumnData(dataRowItem, "UPC", string.Empty, false);
                   // purchaseOrderItem.FmUPC = (string)clsGeneral.getColumnData(dataRowItem, "fmupc", string.Empty, false);
                    //purchaseOrderItem.WarehouseCode = (string)clsGeneral.getColumnData(dataRowItem, "whCode", string.Empty, false);
                    //purchaseOrderItem.WholesaleCost = Convert.ToDouble(clsGeneral.getColumnData(dataRowItem, "cost", 0, false));
                    //purchaseOrderItem.TilaDocument = (string)clsGeneral.getColumnData(dataRowItem, "tilaDocument", string.Empty, false);
                    //purchaseOrderItem.V2Document = (string)clsGeneral.getColumnData(dataRowItem, "v2document", string.Empty, false);

                    //purchaseOrderItem.PhoneCategory = (PhoneCategoryType)Convert.ToChar(clsGeneral.getColumnData(dataRowItem, "PhoneCategory", "C", false));
                    //purchaseOrderItem.MslNumber = (string)clsGeneral.getColumnData(dataRowItem, "mslNumber", string.Empty, false);
                    //purchaseOrderItem.MsID = (string)clsGeneral.getColumnData(dataRowItem, "MSID", string.Empty, false);
                    //purchaseOrderItem.PassCode = (string)clsGeneral.getColumnData(dataRowItem, "pass_code", string.Empty, false);
                    purchaseOrderItem.PODStatus = (avii.Classes.PurchaseOrderStatus)Convert.ToInt32(clsGeneral.getColumnData(dataRowItem, "StatusID", 1, false));
                    purchaseOrderItem.StatusID = Convert.ToInt32(clsGeneral.getColumnData(dataRowItem, "StatusID", 1, false));
                    purchaseOrderItem.CustomAttribute = Convert.ToBoolean(clsGeneral.getColumnData(dataRowItem, "CustomAttribute", false, false));
                    purchaseOrderItem.LTEICCID = (string)clsGeneral.getColumnData(dataRowItem, "ICC_ID", string.Empty, false);
                    //purchaseOrderItem.LTEIMSI = (string)clsGeneral.getColumnData(dataRowItem, "LTEIMSI", string.Empty, false);
                    //purchaseOrderItem.Otksal = (string)clsGeneral.getColumnData(dataRowItem, "otksl", string.Empty, false);
                    //purchaseOrderItem.Akey = (string)clsGeneral.getColumnData(dataRowItem, "akey", string.Empty, false);
                   // purchaseOrderItem.IsSim = Convert.ToBoolean(clsGeneral.getColumnData(dataRowItem, "IsSim", false, false));
                   // purchaseOrderItem.SimNumber = (string)clsGeneral.getColumnData(dataRowItem, "SimNumber", string.Empty, false);
                    purchaseOrderItem.TrackingNumber = (string)clsGeneral.getColumnData(dataRowItem, "TrackingNumber", string.Empty, false);
                    purchaseOrderItem.ProductName = (string)clsGeneral.getColumnData(dataRowItem, "ItemName", string.Empty, false);
                    //purchaseOrderItem.EsnCount = Convert.ToInt32(clsGeneral.getColumnData(dataRowItem, "ESNCOUNT", 0, false));
                    
                    purchaseOrderItemList.Add(purchaseOrderItem);
                }
            }





            return purchaseOrderItemList;
        }
        private static List<BasePurchaseOrderItem> PopulatePurchaseOrderESNList(DataTable dt)
        {
            List<BasePurchaseOrderItem> purchaseOrderItemList = null;

            //BasePurchaseOrder purchaseOrder = null;


            if (dt != null && dt.Rows.Count > 0)
            {
                int itemCount = 0;
                purchaseOrderItemList = new List<BasePurchaseOrderItem>();
                BasePurchaseOrderItem purchaseOrderItem;
                foreach (DataRow dataRowItem in dt.Rows)
                {
                    purchaseOrderItem = new BasePurchaseOrderItem(Convert.ToInt32(clsGeneral.getColumnData(dataRowItem, "POD_ID", 0, false)));
                   // purchaseOrderItem.LineNo = Convert.ToInt32(clsGeneral.getColumnData(dataRowItem, "Line_No", 0, false));
                    purchaseOrderItem.ESN = (string)clsGeneral.getColumnData(dataRowItem, "ESN", string.Empty, false);
                    purchaseOrderItem.ItemCode = (string)clsGeneral.getColumnData(dataRowItem, "Item_Code", string.Empty, false);
                    // purchaseOrderItem.MdnNumber = (string)clsGeneral.getColumnData(dataRowItem, "MDN", string.Empty, false);
                   // purchaseOrderItem.Quantity = (int?)clsGeneral.getColumnData(dataRowItem, "Qty", 0, false);
                    //itemCount = (int)clsGeneral.getColumnData(dataRowItem, "ItemCount", 0, false);
                    //purchaseOrderItem.BoxId = itemCount.ToString();
                    purchaseOrderItem.UPC = (string)clsGeneral.getColumnData(dataRowItem, "UPC", string.Empty, false);
                    // purchaseOrderItem.FmUPC = (string)clsGeneral.getColumnData(dataRowItem, "fmupc", string.Empty, false);
                    //purchaseOrderItem.WarehouseCode = (string)clsGeneral.getColumnData(dataRowItem, "whCode", string.Empty, false);
                    //purchaseOrderItem.WholesaleCost = Convert.ToDouble(clsGeneral.getColumnData(dataRowItem, "cost", 0, false));
                    //purchaseOrderItem.TilaDocument = (string)clsGeneral.getColumnData(dataRowItem, "tilaDocument", string.Empty, false);
                    //purchaseOrderItem.V2Document = (string)clsGeneral.getColumnData(dataRowItem, "v2document", string.Empty, false);

                    //purchaseOrderItem.PhoneCategory = (PhoneCategoryType)Convert.ToChar(clsGeneral.getColumnData(dataRowItem, "PhoneCategory", "C", false));
                    purchaseOrderItem.MslNumber = (string)clsGeneral.getColumnData(dataRowItem, "mslNumber", string.Empty, false);
                    //purchaseOrderItem.MsID = (string)clsGeneral.getColumnData(dataRowItem, "MSID", string.Empty, false);
                    //purchaseOrderItem.PassCode = (string)clsGeneral.getColumnData(dataRowItem, "pass_code", string.Empty, false);
                    purchaseOrderItem.PODStatus = (avii.Classes.PurchaseOrderStatus)Convert.ToInt32(clsGeneral.getColumnData(dataRowItem, "StatusID", 1, false));
                    purchaseOrderItem.StatusID = Convert.ToInt32(clsGeneral.getColumnData(dataRowItem, "StatusID", 1, false));
                    purchaseOrderItem.CustomAttribute = Convert.ToBoolean(clsGeneral.getColumnData(dataRowItem, "CustomAttribute", false, false));
                    purchaseOrderItem.LTEICCID = (string)clsGeneral.getColumnData(dataRowItem, "ICC_ID", string.Empty, false);
                    //purchaseOrderItem.LTEIMSI = (string)clsGeneral.getColumnData(dataRowItem, "LTEIMSI", string.Empty, false);
                    //purchaseOrderItem.Otksal = (string)clsGeneral.getColumnData(dataRowItem, "otksl", string.Empty, false);
                    //purchaseOrderItem.Akey = (string)clsGeneral.getColumnData(dataRowItem, "akey", string.Empty, false);
                    // purchaseOrderItem.IsSim = Convert.ToBoolean(clsGeneral.getColumnData(dataRowItem, "IsSim", false, false));
                    // purchaseOrderItem.SimNumber = (string)clsGeneral.getColumnData(dataRowItem, "SimNumber", string.Empty, false);
                    //purchaseOrderItem.TrackingNumber = (string)clsGeneral.getColumnData(dataRowItem, "TrackingNumber", string.Empty, false);
                    //purchaseOrderItem.EsnCount = Convert.ToInt32(clsGeneral.getColumnData(dataRowItem, "ESNCOUNT", 0, false));

                    purchaseOrderItemList.Add(purchaseOrderItem);
                }
            }





            return purchaseOrderItemList;
        }
        private static List<BasePurchaseOrderItem> PopulatePurchaseOrderItemSummary(DataTable dt)
        {
            List<BasePurchaseOrderItem> purchaseOrderItemList = null;

            //BasePurchaseOrder purchaseOrder = null;


            if (dt != null && dt.Rows.Count > 0)
            {
                int itemCount = 0;
                purchaseOrderItemList = new List<BasePurchaseOrderItem>();
                BasePurchaseOrderItem purchaseOrderItem;
                foreach (DataRow dataRowItem in dt.Rows)
                {
                    purchaseOrderItem = new BasePurchaseOrderItem(Convert.ToInt32(clsGeneral.getColumnData(dataRowItem, "POD_ID", 0, false)));
                    purchaseOrderItem.LineNo = Convert.ToInt32(clsGeneral.getColumnData(dataRowItem, "ItemCount", 0, false));
                    purchaseOrderItem.ESN = (string)clsGeneral.getColumnData(dataRowItem, "ESN", string.Empty, false);
                    purchaseOrderItem.ItemCode = (string)clsGeneral.getColumnData(dataRowItem, "Item_Code", string.Empty, false);
                    purchaseOrderItem.MdnNumber = (string)clsGeneral.getColumnData(dataRowItem, "MDN", string.Empty, false);
                    purchaseOrderItem.Quantity = (int?)clsGeneral.getColumnData(dataRowItem, "Qty", 0, false);
                    //itemCount = (int)clsGeneral.getColumnData(dataRowItem, "ItemCount", 0, false);
                    //purchaseOrderItem.BoxId = itemCount.ToString();
                    purchaseOrderItem.UPC = (string)clsGeneral.getColumnData(dataRowItem, "UPC", string.Empty, false);
                    purchaseOrderItem.FmUPC = (string)clsGeneral.getColumnData(dataRowItem, "fmupc", string.Empty, false);
                    //purchaseOrderItem.WarehouseCode = (string)clsGeneral.getColumnData(dataRowItem, "whCode", string.Empty, false);
                    //purchaseOrderItem.WholesaleCost = Convert.ToDouble(clsGeneral.getColumnData(dataRowItem, "cost", 0, false));
                    //purchaseOrderItem.TilaDocument = (string)clsGeneral.getColumnData(dataRowItem, "tilaDocument", string.Empty, false);
                    //purchaseOrderItem.V2Document = (string)clsGeneral.getColumnData(dataRowItem, "v2document", string.Empty, false);

                    //purchaseOrderItem.PhoneCategory = (PhoneCategoryType)Convert.ToChar(clsGeneral.getColumnData(dataRowItem, "PhoneCategory", "C", false));
                    purchaseOrderItem.MslNumber = (string)clsGeneral.getColumnData(dataRowItem, "mslNumber", string.Empty, false);
                    purchaseOrderItem.MsID = (string)clsGeneral.getColumnData(dataRowItem, "MSID", string.Empty, false);
                    purchaseOrderItem.PassCode = (string)clsGeneral.getColumnData(dataRowItem, "pass_code", string.Empty, false);
                    purchaseOrderItem.PODStatus = (avii.Classes.PurchaseOrderStatus)Convert.ToInt32(clsGeneral.getColumnData(dataRowItem, "StatusID", 1, false));
                    purchaseOrderItem.StatusID = Convert.ToInt32(clsGeneral.getColumnData(dataRowItem, "StatusID", 1, false));
                    purchaseOrderItem.CustomAttribute = Convert.ToBoolean(clsGeneral.getColumnData(dataRowItem, "CustomAttribute", false, false));
                    purchaseOrderItem.LTEICCID = (string)clsGeneral.getColumnData(dataRowItem, "LTEICCID", string.Empty, false);
                    purchaseOrderItem.LTEIMSI = (string)clsGeneral.getColumnData(dataRowItem, "LTEIMSI", string.Empty, false);
                    purchaseOrderItem.Otksal = (string)clsGeneral.getColumnData(dataRowItem, "otksl", string.Empty, false);
                    purchaseOrderItem.Akey = (string)clsGeneral.getColumnData(dataRowItem, "akey", string.Empty, false);

                    purchaseOrderItemList.Add(purchaseOrderItem);
                }
            }





            return purchaseOrderItemList;
        }

        private static List<TrackingInfo> PopulateTrackingInfo(DataTable dt)
        {
            List<TrackingInfo> trackingList = new List<TrackingInfo>();

            TrackingInfo tracking = null;
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dataRow in dt.Rows)
                {
                    tracking = new TrackingInfo();
                    tracking.PurchaseOrderNumber = clsGeneral.getColumnData(dataRow, "PurchaseOrderNumber", string.Empty, false) as string;
                    tracking.ShipToTrackingNumber = clsGeneral.getColumnData(dataRow, "TrackingNumber", string.Empty, false) as string;
                    tracking.SalesOrderNumber = clsGeneral.getColumnData(dataRow, "SalesOrderNumber", string.Empty, false) as string;


                    trackingList.Add(tracking);
                }
            }
            return trackingList;
        }
        private static List<FulfillmentChangeStatus> PopulateFulfillmentInfo(DataTable dt)
        {
            List<FulfillmentChangeStatus> poList = new List<FulfillmentChangeStatus>();

            FulfillmentChangeStatus po = null;
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dataRow in dt.Rows)
                {
                    po = new FulfillmentChangeStatus();
                    po.FulfillmentOrder = clsGeneral.getColumnData(dataRow, "PONUM", string.Empty, false) as string;
                    //tracking.ShipToTrackingNumber = clsGeneral.getColumnData(dataRow, "TrackingNumber", string.Empty, false) as string;
                    //tracking.SalesOrderNumber = clsGeneral.getColumnData(dataRow, "SalesOrderNumber", string.Empty, false) as string;


                    poList.Add(po);
                }
            }
            return poList;
        }

        private static List<FulfillmentOrderHeader> PopulateFulfillmentOrderWithHeader(DataTable dt)
        {
            List<FulfillmentOrderHeader> posList = new List<Classes.FulfillmentOrderHeader>();
            FulfillmentOrderHeader purchaseOrder;
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dataRow in dt.Rows)
                {
                    purchaseOrder = new FulfillmentOrderHeader();
                    purchaseOrder.FulfillmentOrderNumber = (string)clsGeneral.getColumnData(dataRow, "PO_Num", string.Empty, false);
                    purchaseOrder.TrackingNumber = (string)clsGeneral.getColumnData(dataRow, "TrackingNumber", string.Empty, false);
                    purchaseOrder.StoreID = (string)clsGeneral.getColumnData(dataRow, "Store_ID", string.Empty, false);
                    purchaseOrder.AerovoiceSaleOrderNumber = (string)clsGeneral.getColumnData(dataRow, "AerovoiceSalesOrderNumber", string.Empty, false);
                    purchaseOrder.ContactName = (string)clsGeneral.getColumnData(dataRow, "Contact_Name", string.Empty, false);
                    purchaseOrder.ShipAddress = (string)clsGeneral.getColumnData(dataRow, "shipTo_address", string.Empty, false);
                    purchaseOrder.ShipCity = (string)clsGeneral.getColumnData(dataRow, "shipTo_city", string.Empty, false);
                    purchaseOrder.ShipState = (string)clsGeneral.getColumnData(dataRow, "shipTo_state", string.Empty, false);
                    purchaseOrder.ShipZip = (string)clsGeneral.getColumnData(dataRow, "shipTo_zip", string.Empty, false);
                    purchaseOrder.ShipVia = (string)clsGeneral.getColumnData(dataRow, "ship_via", string.Empty, false);
                    purchaseOrder.CustomerAccountNumber = (string)clsGeneral.getColumnData(dataRow, "CustomerAccountNumber", string.Empty, false);
                    purchaseOrder.FulfillmentOrderDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "po_date", DateTime.MinValue, false));
                    purchaseOrder.FulfillmentStatus = (PurchaseOrderStatus)Convert.ToInt32(clsGeneral.getColumnData(dataRow, "statusid", 1, false));
                    posList.Add(purchaseOrder);

                }

            }
            return posList;

        }
        #endregion

     }

}
