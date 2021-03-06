using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

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

        public static PurchaseOrderProvisioningResponse PurchaseOrderProvisioning(PurchaseOrderProvisioningRequest purchaseOrderProvisioningRequest)
        {
            int UserID = AuthenticateRequest(purchaseOrderProvisioningRequest.Authentication);
            PurchaseOrderProvisioningResponse purchaseOrderProvisioningResponse =  new PurchaseOrderProvisioningResponse();
            if (UserID > 0)
            {
                purchaseOrderProvisioningResponse = SavePOProvisioning(purchaseOrderProvisioningRequest.ProvisioningInfo.ESN,
                                    purchaseOrderProvisioningRequest.ProvisioningInfo.MdnNumber,
                                    purchaseOrderProvisioningRequest.ProvisioningInfo.MsID,
                                    purchaseOrderProvisioningRequest.ProvisioningInfo.PassCode);

            }
            else
             {
                 purchaseOrderProvisioningResponse.Comment = "Cannot authenticate user";
                 purchaseOrderProvisioningResponse.ErrorCode = ResponseErrorCode.CannotAuthenticateUser.ToString();
             }

             return purchaseOrderProvisioningResponse;
        }

        private static PurchaseOrderProvisioningResponse SavePOProvisioning(string esn, string mdn, string msid, string passCode)
        {
            String errorMessage = string.Empty;

            PurchaseOrderProvisioningResponse response = new PurchaseOrderProvisioningResponse();
            errorMessage = PurchaseOrderProvisioning(esn, mdn, msid, passCode);
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

        private static PurchaseOrderProvisioningResponse SavePOProvisioning(string esn, string mdn, string msid, string passCode, string mslNumber)
        {
            String errorMessage = string.Empty;

            PurchaseOrderProvisioningResponse response = new PurchaseOrderProvisioningResponse();
            errorMessage = PurchaseOrderProvisioning(esn, mdn, msid, passCode, mslNumber);
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
                errorMessage = ValidateShipBy(purchaseOrderRequest.PurchaseOrder.ShipThrough.Trim());
                if (!string.IsNullOrEmpty(errorMessage))
                    errorValue = true;
            }
            //else
            //    errorMessage = " Ship By is required";

            return errorValue;
        }
        // Validate mamdatory fields
        private static string ValidateSave(PurchaseOrderRequest purchaseOrderRequest)
        {

            string errorMessage = string.Empty;
            if (string.IsNullOrEmpty(purchaseOrderRequest.PurchaseOrder.PurchaseOrderNumber))
            {

                errorMessage = " Fulfillment Number is required";
            }
            else if (string.IsNullOrEmpty(purchaseOrderRequest.PurchaseOrder.PurchaseOrderDate.ToShortDateString()))
            {
                if (string.IsNullOrEmpty(errorMessage))
                    errorMessage = " Fulfillment Date is required";
                else
                    errorMessage = " \r\n Fulfillment Date is required";
            }
            else if (string.IsNullOrEmpty(purchaseOrderRequest.PurchaseOrder.StoreID))
            {
                if (string.IsNullOrEmpty(errorMessage))
                    errorMessage = " StoreID is required";
                else
                    errorMessage = " \r\n StoreID is required";

            }
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
            //else if (string.IsNullOrEmpty(purchaseOrderRequest.PurchaseOrder.ShipThrough))
            //{
            //    if (string.IsNullOrEmpty(errorMessage))
            //        errorMessage = " Ship By is required";
            //    else
            //        errorMessage = " \r\n Ship By is required";
            //}




            return errorMessage;
        }

        // Called by webmethod
        public static PurchaseOrderResponse CreatePurchaseOrder(PurchaseOrderRequest purchaseOrderRequest, InventoryList inventoryList, int userId)
        {
            PurchaseOrderResponse purchaseOrderResponse = new PurchaseOrderResponse();
            purchaseOrderResponse.ErrorCode = ResponseErrorCode.ErrowWhileLoadingData.ToString();
            purchaseOrderResponse.PurchaseOrderNumber = purchaseOrderRequest.PurchaseOrder.PurchaseOrderNumber;
            try
            {
                string errorMessage = string.Empty;
                // Validate mamdatory fields
                errorMessage = ValidateSave(purchaseOrderRequest);
                if (string.IsNullOrEmpty(errorMessage) && userId > 0)
                {
                    //if (ValidateShipByCode(purchaseOrderRequest))
                    //{
                        if (ValidatePurchaseOrder(purchaseOrderRequest.PurchaseOrder, userId))
                        {
                            string itemCodes = string.Empty;
                            if (ValidationItems(purchaseOrderRequest.PurchaseOrder, inventoryList, out itemCodes))
                            {
                                purchaseOrderResponse = SavePurchaseOrder(purchaseOrderRequest.PurchaseOrder, userId);
                            }
                            else
                            {
                                purchaseOrderResponse.Comment = "Purchase Order items (" + itemCodes + ") are not valid, please check the item code from catalog";

                            }
                        }
                        else
                        {
                            purchaseOrderResponse.Comment = "Purchase Order number already exists";
                        }
                    //}
                    //else
                    //{
                    //    purchaseOrderResponse.Comment = "Ship by is not correct"; ;
                    //    purchaseOrderResponse.ErrorCode = ResponseErrorCode.ShipByIsNotCorrect.ToString();
                    //}
                }
                else
                {
                    purchaseOrderResponse.Comment = errorMessage;
                    purchaseOrderResponse.ErrorCode = ResponseErrorCode.MissingParameter.ToString();
                }
            }
            catch (Exception ex)
            {
                purchaseOrderResponse.ErrorCode = ResponseErrorCode.InternalError.ToString();
                purchaseOrderResponse.Comment = ex.Message;
            }
            return purchaseOrderResponse;
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



        public static PurchaseOrderShipResponse GetPurchaseOrderShipping(PurchaseOrderShipRequest purchaseOrderShipRequest)
        {
            PurchaseOrderShipResponse purchaseOrderShipResponse = new PurchaseOrderShipResponse();
            try
            {

                int userId = AuthenticateRequest(purchaseOrderShipRequest.Authentication);
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
            try
            {
                int userId = AuthenticateRequest(purchaseOrderRequest.Authentication);
                if (userId > 0 && !string.IsNullOrEmpty(purchaseOrderRequest.PurchaseOrderNumber))
                {

                    DBConnect db = new DBConnect();
                    string[] arrSpFieldSeq;
                    DataSet ds = new DataSet();

                    Hashtable objCompHash = new Hashtable();
                    try
                    {
                        objCompHash.Add("@Po_Num", purchaseOrderRequest.PurchaseOrderNumber);
                        objCompHash.Add("@UserID", userId);

                        arrSpFieldSeq = new string[] { "@Po_Num", "@UserID" };
                        ds = db.GetDataSet(objCompHash, "Aero_PurchaseOrder_Select", arrSpFieldSeq);

                        avii.Classes.clsPurchaseOrder po = PopulatePurchaseOrder(ds);
                        if (po != null)
                        {
                            serviceResponse.PurchaseOrder = po;
                           // serviceResponse.ErrorCode = ResponseErrorCode.SuccessfullyRetrieved.ToString();
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

        public static ShippByResponse Get_ShippBy()
        {
            ShippByResponse serviceResponse = new ShippByResponse();
            serviceResponse.ErrorCode = ResponseErrorCode.None.ToString();
            try
            {
                List<avii.Classes.ShipBy> shippByList = GetShipByList();
                if (shippByList != null && shippByList.Count > 0)
                {
                    serviceResponse.ShipByList = shippByList;
                    // serviceResponse.ErrorCode = ResponseErrorCode.SuccessfullyRetrieved.ToString();
                }
                else
                {
                    serviceResponse.ErrorCode = ResponseErrorCode.PurchaseOrderNotExists.ToString();
                }
            }
            catch (Exception ex)
            {
                serviceResponse.ShipByList = null;
                serviceResponse.Comment = ex.Message;
                serviceResponse.ErrorCode = ResponseErrorCode.InconsistantData.ToString();
            }
            return serviceResponse;
        }

        public static FulfillmentOrderResponse GerFulfillmentOrder(avii.Classes.FulfillmentOrderRequest orderRequest)
        {
            FulfillmentOrderResponse serviceResponse = new FulfillmentOrderResponse();
            serviceResponse.ErrorCode = ResponseErrorCode.MissingParameter.ToString();
            try
            {  
                if (orderRequest != null)
                {
                    string poNum = orderRequest.PurchaseOrderNumber;
                    int userId = AuthenticateRequest(orderRequest.Authentication);
                    if (userId > 0 && !string.IsNullOrEmpty(poNum))
                    {

                        DBConnect db = new DBConnect();
                        string[] arrSpFieldSeq;
                        DataSet ds = new DataSet();

                        Hashtable objCompHash = new Hashtable();
                        try
                        {
                            objCompHash.Add("@Po_Num", poNum);
                            objCompHash.Add("@UserID", userId);

                            arrSpFieldSeq = new string[] { "@Po_Num", "@UserID" };
                            ds = db.GetDataSet(objCompHash, "Aero_PurchaseOrder_Select", arrSpFieldSeq);

                            avii.Classes.FulfillmentOrder po = PopulateFulfillmentOrder(ds);
                            if (po != null)
                            {
                                serviceResponse.PurchaseOrder = po;
                                serviceResponse.ErrorCode = ResponseErrorCode.SuccessfullyRetrieved.ToString();
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


        public static ServiceResponse ReportBadInventoryItem(InventoryBadItemRequest inventory)
        {
            int userId = 0;
            ServiceResponse serviceResponse = new ServiceResponse();
            try
            {
                serviceResponse.ErrorCode = string.Empty;
                serviceResponse.Comment = "acknowledged";
                userId = AuthenticateRequest(inventory.Authentication);
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

        public static void DeletePurchaseOrder(int poID)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@Po_ID", poID);

                arrSpFieldSeq = new string[] { "@Po_ID" };
                db.ExeCommand(objCompHash, "Aero_PurchaseOrder_Delete", arrSpFieldSeq);
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

        public static void UpdatePurchaseOrder(BasePurchaseOrder purchaseOrder)
        {
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
                

                arrSpFieldSeq = new string[] { "@PO_ID", "@PO_Num", "@Contact_Name", "@ShipTo_Attn", "@Ship_Via", 
                "@Tracking", "@StatusID", "@ShipTo_Address","@ShipTo_Address2", "@ShipTo_City", "@ShipTo_State", "@ShipTo_Zip", "@Contact_Phone" };
                db.ExeCommand(objCompHash, "Aero_PurchaseOrder_Update", arrSpFieldSeq);
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

        public static void UpLoadESN(string POXml)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataSet ds = new DataSet();

            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@Po_Xml", POXml);

                arrSpFieldSeq = new string[] { "@Po_Xml" };
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

        public static void DeletePurchaseOrderDetail(int podID)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataSet ds = new DataSet();

            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@POD_ID", podID);

                arrSpFieldSeq = new string[] { "@POD_ID" };
                db.ExeCommand(objCompHash, "Aero_PurchaseOrderDetail_Delete", arrSpFieldSeq);
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

        public static void PurchaseOrderUpdateDetail(int podID, string esn, string msl, string msid, string mdn, string passCode, string fmupc, int statusID)
        {
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


                arrSpFieldSeq = new string[] { "@POD_ID", "@ESN", "@msl", "@MSID", "@mdn", "@Pass_Code", "@fmupc", "@StatusID" };
                db.ExeCommand(objCompHash, "Aero_PurchaseOrderDetail_Update", arrSpFieldSeq);
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

        public static bool SetTrackingInfo(int companyID, string purchaseOrderNumber, string trackingNumber, string avOrder)
        {
            bool saved = false;
            
            if (!string.IsNullOrEmpty(purchaseOrderNumber) && !string.IsNullOrEmpty(trackingNumber))
            {

                SetTrackingInfoDB(trackingNumber, companyID, purchaseOrderNumber, avOrder);
                        saved = true;
             
            }
            
            return saved;
        }

        public static int UpdateTrackingInfo(List<TrackingInfo> trackingInfoList)
        {
            int returnValue = 0;

            string trackingXML = clsGeneral.SerializeObject(trackingInfoList);

            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@TrackingXML", trackingXML);
                arrSpFieldSeq = new string[] { "@TrackingXML" };
                db.ExeCommand(objCompHash, "av_PurchaseOrderTrackingInfo_Update", arrSpFieldSeq, "@POCount", out returnValue);
            }
            
            catch (Exception exp)
            {
                throw exp;
            }

            return returnValue;
        }

        public static bool DeletePurchaseOrders(string purchaseOrders, int companyID, int statusID)
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

                    arrSpFieldSeq = new string[] { "@PO_Num", "@CompanyID", "@StatusID" };
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

        public static bool SetEsnMsl(string esnData, int itemID)
        {
            bool retboolValue = true;
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            int retIntVal = 0;
            try
            {
                objCompHash.Add("@esnxml", esnData);
                objCompHash.Add("@ItemGUID", itemID);
                arrSpFieldSeq = new string[] { "@esnxml", "@ItemGUID" };
                object retVal = db.ExecuteNonQuery(objCompHash, "Aero_ESN_Insert", arrSpFieldSeq);
                int.TryParse(retVal.ToString(), out retIntVal);
                if (retIntVal == 0)
                    retboolValue = false;
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
            int userId = AuthenticateRequest(inventoryItemRequest.Authentication);
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
            InventoryList inventoryList = new InventoryList(); ;
            int userId = AuthenticateRequest(inventoryItemRequest.Authentication);
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
            InventoryList inventoryList = new InventoryList(); ;
            int userId = AuthenticateRequest(inventoryItemRequest.Authentication);
            if (userId > 0)
            {
                inventoryList = clsInventoryDB.GetInventoryItems(userId);
            }
            return inventoryList;
        }

        public static InventoryList GetInventoryItems(int userID)
        {
            return clsInventoryDB.GetInventoryItems(userID);
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
            List<ShipBy> shipBylist = null;
            try
            {
                objCompHash.Add("@ShipByID", 0);

                arrSpFieldSeq = new string[] { "@ShipByID" };
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

        #endregion

        #region Private Methods
        private static string ValidateShipBy(string shipBy)
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
        private static string PurchaseOrderProvisioning(string esn, string mdn, string msid, string passCode, string mslNumber)
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
                arrSpFieldSeq = new string[] { "@ESN", "@MDN", "@MSID", "@Pass_Code", "@MslNumer" };
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


        private static string PurchaseOrderProvisioning(string esn, string mdn, string msid, string passCode)
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
                arrSpFieldSeq = new string[] { "@ESN", "@MDN", "@MSID", "@Pass_Code" };
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

        public static int AuthenticateRequest(clsAuthentication AuthenticateUser)
        {
            int userId = 0;
            userId = clsUser.ValidateUser(AuthenticateUser);
            return userId;
        }

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

        public static void SetPurchaseOrderStatusDB(string PurchaseOrderIDs, string statusId)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@PO_IDs", PurchaseOrderIDs);
                objCompHash.Add("@StatusID", statusId);

                arrSpFieldSeq = new string[] { "@PO_IDs", "@StatusID" };
                db.ExeCommand(objCompHash, "Aero_PurchaseOrder_StatusUpdate", arrSpFieldSeq);
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

        public static PurchaseOrderResponse SavePurchaseOrder(clsPurchaseOrder purchaseOrder, int UserID)
        {
            String errorMessage = string.Empty;
            PurchaseOrderResponse response = new PurchaseOrderResponse();
            errorMessage = CreatePurchaseOrderDB(BaseAerovoice.SerializeObject<clsPurchaseOrder>(purchaseOrder), UserID, 0);
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
                {
                    response.PurchaseOrderNumber = string.Empty;
                    response.ErrorCode = ResponseErrorCode.ErrowWhileLoadingData.ToString();
                }
                    
                
            }
            
            return response;
        }

        public static PurchaseOrderResponse SaveNewPurchaseOrder(clsPurchaseOrder purchaseOrder, int UserID, int forecastGUID)
        {
            String errorMessage = string.Empty;
            PurchaseOrderResponse response = new PurchaseOrderResponse();
            errorMessage = CreatePurchaseOrderDB(BaseAerovoice.SerializeObject<clsPurchaseOrder>(purchaseOrder), UserID, forecastGUID);
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
                {
                    response.PurchaseOrderNumber = string.Empty;
                    response.ErrorCode = ResponseErrorCode.ErrowWhileLoadingData.ToString();
                }
            }

            return response;
        }

        public static PurchaseOrderResponse SaveNewPurchaseOrderCompany(clsPurchaseOrder purchaseOrder, int CompanyID)
        {
            String errorMessage = string.Empty;
            PurchaseOrderResponse response = new PurchaseOrderResponse();
            errorMessage = CreatePurchaseOrderCompanyDB(BaseAerovoice.SerializeObject<clsPurchaseOrder>(purchaseOrder), CompanyID);
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
                {
                    response.PurchaseOrderNumber = string.Empty;
                    response.ErrorCode = ResponseErrorCode.ErrowWhileLoadingData.ToString();
                }
            }

            return response;
        }

        private static String UTF8ByteArrayToString(Byte[] characters)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            String constructedString = encoding.GetString(characters);
            return (constructedString);
        }

        private static string CreatePurchaseOrderDB(string POXml, int UserID, int forecastGUID)
        {
            string errorMessage = string.Empty;
            int returnValue = 0;
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            string sCode = string.Empty;
            Hashtable objCompHash = new Hashtable();
            try
            {

                POXml = "<purchaseorder>" + POXml.Substring(POXml.IndexOf("<purchaseordernumber>"));
                objCompHash.Add("@Po_Xml", POXml);
                objCompHash.Add("@UserID", UserID);
                objCompHash.Add("@ForecastGUID", forecastGUID);

                arrSpFieldSeq = new string[] { "@Po_Xml", "@UserID", "@ForecastGUID" };
                returnValue = db.ExecCommand(objCompHash, "Aero_PurchaseOrder_Create", arrSpFieldSeq);
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

        private static string CreatePurchaseOrderCompanyDB(string POXml, int CompanyID)
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
                arrSpFieldSeq = new string[] { "@Po_Xml", "@CompanyID" };
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

        private static void SetTrackingInfoDB(string trackingNumber, int companyID, string purchaseOrderNumber, string avOrder)
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

                arrSpFieldSeq = new string[] { "@CompanyID", "@PO_Num", "@TrackingNum", "@POStatus", "@AVOrderNumber" };
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
                    
                    if (ds.Tables[1].Rows.Count > 0)
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
                //purchaseOrder.PurchaseOrderStatus = (avii.Classes.PurchaseOrderStatus)Convert.ToInt16(clsGeneral.getColumnData(dataRow, "Status", 1, false));

                if (ds.Tables[1].Rows.Count > 0)
                {
                    PurchaseOrderItem purchaseOrderItem;
                    foreach (DataRow dataRowItem in ds.Tables[1].Select("POID = " + purchaseOrder.PurchaseOrderID.ToString()))
                    {
                        purchaseOrderItem = new PurchaseOrderItem(Convert.ToInt32(clsGeneral.getColumnData(dataRowItem, "POD_ID", 0, false)));
                        purchaseOrderItem.LineNo = Convert.ToInt32(clsGeneral.getColumnData(dataRowItem, "Line_no", 0, false));
                        purchaseOrderItem.ESN = (string)clsGeneral.getColumnData(dataRowItem, "ESN", string.Empty, false);
                        purchaseOrderItem.ItemCode = (string)clsGeneral.getColumnData(dataRowItem, "Item_Code", string.Empty, false);
                        purchaseOrderItem.MdnNumber = (string)clsGeneral.getColumnData(dataRowItem, "MDN", string.Empty, false);
                        purchaseOrderItem.Quantity = (int?)clsGeneral.getColumnData(dataRowItem, "Qty", 0, false);
                        
                        purchaseOrderItem.PhoneCategory = (PhoneCategoryType)Convert.ToChar(clsGeneral.getColumnData(dataRowItem, "PhoneCategory", "C", false));

                        purchaseOrder.PurchaseOrderItems.Add(purchaseOrderItem);
                    }
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

                if (poID != null & poID > 0)
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
                    //purchaseOrder.PurchaseOrderStatus = (avii.Classes.PurchaseOrderStatus)Convert.ToInt16(clsGeneral.getColumnData(dataRow, "Status", 1, false));


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

                            purchaseOrderItem.PhoneCategory = (avii.Classes.PhoneCategoryType)Convert.ToChar(clsGeneral.getColumnData(dataRowItem, "PhoneCategory", "C", false));

                            purchaseOrder.PurchaseOrderItems.Add(purchaseOrderItem);
                        }
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
                    shipByInfo.ShipByCode = (string)clsGeneral.getColumnData(dataRow, "ShipByCode", string.Empty, true);
                    shipByInfo.ShipByText = (string)clsGeneral.getColumnData(dataRow, "ShipByText", string.Empty, true);
                    shipBylist.Add(shipByInfo);
                }
            }
            return shipBylist;
        }
        #endregion

     }
}
