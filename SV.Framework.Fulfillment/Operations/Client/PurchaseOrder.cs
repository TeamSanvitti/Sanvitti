using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Web;
//using System.Web.UI;
//using System.Web.UI.WebControls;
//using System.IO;
using System.Linq;
//using System.Configuration;
using SV.Framework.Models.Fulfillment;
using SV.Framework.Models.Common;
using SV.Framework.Models.Service;

namespace SV.Framework.Fulfillment
{   
    public class PurchaseOrder : BaseCreateInstance
    {

        private string ValidateDuplicateSKU(SV.Framework.Models.Service.PurchaseOrderRequest purchaseOrderRequest)
        {
            string errorMessage = string.Empty;
            List<PurchaseOrderItem> polist = purchaseOrderRequest.PurchaseOrder.PurchaseOrderItems;
            var duplicateItems = polist.GroupBy(x => x).Where(g => g.Count() > 1).Select(y => y.Key).ToList();

            // var duplicateItems = (from itemcodes in polist where itemcodes.ItemCode.ToUpper().Equals(itemCode.ToUpper()) select itemcodes).ToList();
            if (duplicateItems != null && duplicateItems.Count > 0 && !string.IsNullOrEmpty(duplicateItems[0].ItemCode))
            {
                errorMessage = "Duplicate SKU are  not allowed!";

            }
            return errorMessage;
        }
        // Validate mamdatory fields
        private string ValidateSave(SV.Framework.Models.Service.PurchaseOrderRequest purchaseOrderRequest)
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
        
        private bool ValidateShipByCode(SV.Framework.Models.Service.PurchaseOrderRequest purchaseOrderRequest)
        {
            SV.Framework.DAL.Fulfillment.PurchaseOrder purchaseOrderOperation = SV.Framework.DAL.Fulfillment.PurchaseOrder.CreateInstance<DAL.Fulfillment.PurchaseOrder>();

            bool errorValue = false;
            string errorMessage = string.Empty;
            if (!string.IsNullOrEmpty(purchaseOrderRequest.PurchaseOrder.ShipThrough.Trim()))
            {
                errorMessage = purchaseOrderOperation.ValidateShipByText(purchaseOrderRequest.PurchaseOrder.ShipThrough.Trim());
                if (!string.IsNullOrEmpty(errorMessage))
                    errorValue = true;
            }
            //else
            //    errorMessage = " Ship By is required";

            return errorValue;
        }
        private bool ValidateQty(List<PurchaseOrderItem> poItems)
        {
            bool returnValue = true;
            if (poItems != null)
            {
                foreach (PurchaseOrderItem poItem in poItems)
                {
                    if (poItem.Quantity != null && poItem.Quantity < 1)
                    {
                        returnValue = false;
                    }
                }
            }
            return returnValue;


        }
        private bool ValidationItems(clsPurchaseOrder purchaseOrder, SV.Framework.Models.Inventory.InventoryList inventoryList, out string itemsCodeList)
        {
            bool found = true;
            itemsCodeList = string.Empty;
            foreach (PurchaseOrderItem inv in purchaseOrder.PurchaseOrderItems)
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

        public PurchaseOrderResponse CreatePurchaseOrder(SV.Framework.Models.Service.PurchaseOrderRequest purchaseOrderRequest, SV.Framework.Models.Inventory.InventoryList inventoryList, int userId)
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
        public PurchaseOrderResponse SavePurchaseOrder(clsPurchaseOrder purchaseOrder, int UserID)
        {
            //if (purchaseOrder.B2B == true && string.IsNullOrEmpty(purchaseOrder.POType))
            //    purchaseOrder.POType = "B2B";

            bool IsInterNational = false;
            SV.Framework.DAL.Fulfillment.PurchaseOrder purchaseOrderOperation = SV.Framework.DAL.Fulfillment.PurchaseOrder.CreateInstance<DAL.Fulfillment.PurchaseOrder>();

            if (System.Enum.IsDefined(typeof(SV.Framework.Common.LabelGenerator.CanadaStates), purchaseOrder.Shipping.ShipToState))
                IsInterNational = true;

            String errorMessage = string.Empty, PurchaseOrderNumber = string.Empty;
            PurchaseOrderResponse response = new PurchaseOrderResponse();
            errorMessage = purchaseOrderOperation.CreatePurchaseOrderDB(BaseAerovoice.SerializeObject<clsPurchaseOrder>(purchaseOrder), UserID, 0, PurchaseOrderFlag.A.ToString(), IsInterNational, out PurchaseOrderNumber);
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
                
        public PurchaseOrderResponse SaveNewPurchaseOrder(clsPurchaseOrder purchaseOrder, int UserID, int forecastGUID, string poFlag)
        {
            SV.Framework.DAL.Fulfillment.PurchaseOrder purchaseOrderOperation = SV.Framework.DAL.Fulfillment.PurchaseOrder.CreateInstance<DAL.Fulfillment.PurchaseOrder>();

            String errorMessage = string.Empty, PurchaseOrderNumber = string.Empty;
            bool IsInterNational = false;

            if (System.Enum.IsDefined(typeof(CanadaStates), purchaseOrder.Shipping.ShipToState))
                IsInterNational = true;

            PurchaseOrderResponse response = new PurchaseOrderResponse();
            errorMessage = purchaseOrderOperation.CreatePurchaseOrderDB(BaseAerovoice.SerializeObject<clsPurchaseOrder>(purchaseOrder), UserID, forecastGUID, poFlag, IsInterNational, out PurchaseOrderNumber);
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
                    if (!string.IsNullOrEmpty(errorMessage))
                    {
                        response.PurchaseOrderNumber = string.Empty;
                        response.ErrorCode = errorMessage;
                    }
                    else
                    {
                        response.PurchaseOrderNumber = string.Empty;
                        response.ErrorCode = ResponseErrorCode.ErrowWhileLoadingData.ToString();
                    }
                }
            }
            return response;
        }

        public List<ShipBy> GetShipByList()
        {
            SV.Framework.DAL.Fulfillment.PurchaseOrder purchaseOrderOperation = SV.Framework.DAL.Fulfillment.PurchaseOrder.CreateInstance<DAL.Fulfillment.PurchaseOrder>();

            List<ShipBy> shipBylist = purchaseOrderOperation.GetShipByList();
            
            return shipBylist;
        }

        public PurchaseOrders GerPurchaseOrdersNew(string PONumber, string ContactName, string POFromDate, string POToDate, int UserID,
                                                       string statusID, int CompanyID, string esn, string AvOrder, string MslNumber, string PhoneCategory,
                                                       string ItemCode, string StoreID, string fmUPC, string zoneGUID, string shipFrom, string shipTo,
                                                       int PO_ID, string trackingNumber, string customerOrderNumber, string POType, int StockInDemand)
        {
            SV.Framework.DAL.Fulfillment.PurchaseOrder purchaseOrderOperation = SV.Framework.DAL.Fulfillment.PurchaseOrder.CreateInstance<DAL.Fulfillment.PurchaseOrder>();

            return purchaseOrderOperation.GerPurchaseOrdersNew(PONumber, ContactName, POFromDate, POToDate, UserID,
                                                       statusID, CompanyID, esn, AvOrder, MslNumber, PhoneCategory,
                                                       ItemCode, StoreID, fmUPC, zoneGUID, shipFrom, shipTo,
                                                       PO_ID, trackingNumber, customerOrderNumber, POType, StockInDemand);
            
        }





        public PurchaseOrders GerPurchaseOrders(string PONumber, string ContactName, string POFromDate, string POToDate, int UserID,
                                                       string statusID, int CompanyID, string esn, string AvOrder, string MslNumber,
                                                       string PhoneCategory, string ItemCode, string StoreID, string fmUPC, string zoneGUID,
                                                       string shipFrom, string shipTo, int PO_ID)
        {
            SV.Framework.DAL.Fulfillment.PurchaseOrder purchaseOrderOperation = SV.Framework.DAL.Fulfillment.PurchaseOrder.CreateInstance<DAL.Fulfillment.PurchaseOrder>();

            return purchaseOrderOperation.GerPurchaseOrders(PONumber, ContactName, POFromDate, POToDate, UserID,
                                                       statusID, CompanyID, esn, AvOrder, MslNumber, PhoneCategory,
                                                       ItemCode, StoreID, fmUPC, zoneGUID, shipFrom, shipTo,
                                                       PO_ID);
            
        }

        public PurchaseOrders GerSelectedPurchaseOrders(string POIds, int downloadFlag)
        {
            SV.Framework.DAL.Fulfillment.PurchaseOrder purchaseOrderOperation = SV.Framework.DAL.Fulfillment.PurchaseOrder.CreateInstance<DAL.Fulfillment.PurchaseOrder>();

            return purchaseOrderOperation.GerSelectedPurchaseOrders(POIds, downloadFlag);
            
        }

        public List<BasePurchaseOrder> GetPurchaseOrderHistory(int poID)
        { 
            SV.Framework.DAL.Fulfillment.PurchaseOrder purchaseOrderOperation = SV.Framework.DAL.Fulfillment.PurchaseOrder.CreateInstance<DAL.Fulfillment.PurchaseOrder>();

            List<BasePurchaseOrder> poStatusHistory = purchaseOrderOperation.GetPurchaseOrderHistory(poID);
           
            return poStatusHistory;
        }

        public string  UpdatePurchaseOrder(BasePurchaseOrder purchaseOrder, int userID)
        {
            SV.Framework.DAL.Fulfillment.PurchaseOrder purchaseOrderOperation = SV.Framework.DAL.Fulfillment.PurchaseOrder.CreateInstance<DAL.Fulfillment.PurchaseOrder>();

            return purchaseOrderOperation.UpdatePurchaseOrder(purchaseOrder, userID);            
        }


        public  void DeletePurchaseOrder(int poID, int userID)
        {
            SV.Framework.DAL.Fulfillment.PurchaseOrder purchaseOrderOperation = SV.Framework.DAL.Fulfillment.PurchaseOrder.CreateInstance<DAL.Fulfillment.PurchaseOrder>();

            purchaseOrderOperation.DeletePurchaseOrder(poID, userID);            
        }

        public void PurchaseOrderUpdateDetail(int podID, string esn, string msl, string msid, string mdn, string passCode, string fmupc, int statusID, int userID, string lteICCiD, string lteIMSI, string akey, string otksl, string simNumber, out string returnMessage)
        {
            returnMessage = string.Empty;
            SV.Framework.DAL.Fulfillment.PurchaseOrder purchaseOrderOperation = SV.Framework.DAL.Fulfillment.PurchaseOrder.CreateInstance<DAL.Fulfillment.PurchaseOrder>();

            purchaseOrderOperation.PurchaseOrderUpdateDetail(podID, esn, msl, msid, mdn, passCode, fmupc, statusID, userID, lteICCiD, lteIMSI, akey, otksl, simNumber, out returnMessage);
            
        }

        public int DeletePurchaseOrderDetail(int podID, int userID)
        {
            SV.Framework.DAL.Fulfillment.PurchaseOrder purchaseOrderOperation = SV.Framework.DAL.Fulfillment.PurchaseOrder.CreateInstance<DAL.Fulfillment.PurchaseOrder>();

            int returnValue = purchaseOrderOperation.DeletePurchaseOrderDetail(podID, userID);
            
            return returnValue;
        }

        public  List<BasePurchaseOrderItem> GetPurchaseOrderItemList(int PO_ID)
        {
            SV.Framework.DAL.Fulfillment.PurchaseOrder purchaseOrderOperation = SV.Framework.DAL.Fulfillment.PurchaseOrder.CreateInstance<DAL.Fulfillment.PurchaseOrder>();

            return purchaseOrderOperation.GetPurchaseOrderItemList(PO_ID);
        }
        public int ValidateFulfillmentOrder(List<FulfillmentNumber> poList, List<FulfillmentSKU> skuList, string companyAccountNumber, out int poRecordCount, 
            out string poErrorMessage, out string poStoreIDErrorMessage, out string poShipViaErrorMessage, out string poSKUsErrorMessage, out string poStateErrorMessage, out string errorMessage)
        {
            poRecordCount = 0;
            poErrorMessage = default;
            poStoreIDErrorMessage = default;
            poShipViaErrorMessage = default;
            poSKUsErrorMessage = default;
            errorMessage = default;
            poStateErrorMessage = string.Empty;
            SV.Framework.DAL.Fulfillment.PurchaseOrder purchaseOrderOperation = SV.Framework.DAL.Fulfillment.PurchaseOrder.CreateInstance<DAL.Fulfillment.PurchaseOrder>();

            int returnValue = purchaseOrderOperation.ValidateFulfillmentOrder(poList, skuList, companyAccountNumber, out poRecordCount, out poErrorMessage
                , out poStoreIDErrorMessage, out poShipViaErrorMessage, out poSKUsErrorMessage, out poStateErrorMessage, out errorMessage);
                   

            return returnValue;
        }

        public int SetPurchaseOrderChangeStatusDB(List<PurchaseOrderChangeStatus> poList, string status, int userID, int conditionalStatus)
        {
            SV.Framework.DAL.Fulfillment.PurchaseOrder purchaseOrderOperation = SV.Framework.DAL.Fulfillment.PurchaseOrder.CreateInstance<DAL.Fulfillment.PurchaseOrder>();

            int returnValue = purchaseOrderOperation.SetPurchaseOrderChangeStatusDB(poList, status, userID, conditionalStatus);
                        
            return returnValue;
        }
        public List<clsPurchaseOrder> SaveBulkPurchaseOrderDB(List<clsPurchaseOrder> poList, string companyAccountNumber, int userID, string poSource, string fileName, string comment, out int poRecordCount, out int poReturnCount, out string errorMessage)
        {
            //int returnValue = 0;
            poRecordCount = 0;
            poReturnCount = 0;
            errorMessage = default;
            SV.Framework.DAL.Fulfillment.PurchaseOrder purchaseOrderOperation = SV.Framework.DAL.Fulfillment.PurchaseOrder.CreateInstance<DAL.Fulfillment.PurchaseOrder>();

            poList = purchaseOrderOperation.SaveBulkPurchaseOrderDB(poList, companyAccountNumber, userID, poSource, fileName, comment, out poRecordCount, out poReturnCount, out errorMessage);

            return poList;
        }

        public List<BasePurchaseOrderItem> GetPurchaseOrderItemsAndTrackingList(int PO_ID, out List<TrackingDetail> trackingList)
        {
            SV.Framework.DAL.Fulfillment.PurchaseOrder purchaseOrderOperation = SV.Framework.DAL.Fulfillment.PurchaseOrder.CreateInstance<DAL.Fulfillment.PurchaseOrder>();

            trackingList = default;
            List<BasePurchaseOrderItem> poItems = purchaseOrderOperation.GetPurchaseOrderItemsAndTrackingList(PO_ID, out trackingList);

            return poItems;
        }

        public void PurchaseOrderUpdateDetailNew(int podID, int qty, int userID, out string returnMessage)
        {
            SV.Framework.DAL.Fulfillment.PurchaseOrder purchaseOrderOperation = SV.Framework.DAL.Fulfillment.PurchaseOrder.CreateInstance<DAL.Fulfillment.PurchaseOrder>();

            returnMessage = string.Empty;
            purchaseOrderOperation.PurchaseOrderUpdateDetailNew(podID, qty, userID, out returnMessage);
           
        }
        public List<ShipBy> GetShipByList(string Nationality)
        {
            SV.Framework.DAL.Fulfillment.PurchaseOrder purchaseOrderOperation = SV.Framework.DAL.Fulfillment.PurchaseOrder.CreateInstance<DAL.Fulfillment.PurchaseOrder>();

            List<ShipBy> shipBylist = purchaseOrderOperation.GetShipByList(Nationality);
                       
            return shipBylist;
        }
        public List<FulfillmentSKUs> GetPurchaseSKUList(int PO_ID, out List<POEsn> esnList)
        {
            SV.Framework.DAL.Fulfillment.PurchaseOrder purchaseOrderOperation = SV.Framework.DAL.Fulfillment.PurchaseOrder.CreateInstance<DAL.Fulfillment.PurchaseOrder>();

            esnList = default;
            List<FulfillmentSKUs> fulfillmentSKUs = purchaseOrderOperation.GetPurchaseSKUList(PO_ID, out esnList);

            return fulfillmentSKUs;          
        }
        public List<POEsn> GetUnProvisionEsnList(int POID)
        {
            SV.Framework.DAL.Fulfillment.PurchaseOrder purchaseOrderOperation = SV.Framework.DAL.Fulfillment.PurchaseOrder.CreateInstance<DAL.Fulfillment.PurchaseOrder>();

            List<POEsn> esnList = purchaseOrderOperation.GetUnProvisionEsnList(POID);

            return esnList;
        }
        public PurchaseOrders GerPurchaseOrdersView(string PONumber, string companyAccountNumber)
        {
            SV.Framework.DAL.Fulfillment.PurchaseOrder purchaseOrderOperation = SV.Framework.DAL.Fulfillment.PurchaseOrder.CreateInstance<DAL.Fulfillment.PurchaseOrder>();

            PurchaseOrders purchaseOrders = purchaseOrderOperation.GerPurchaseOrdersView(PONumber, companyAccountNumber);
            return purchaseOrders;
        }
        public PurchaseOrderInfoResponse GerPurchaseOrderAPI(SV.Framework.Models.Service.PurchaseOrderInfoRequest purchaseOrderRequest)
        {
            SV.Framework.DAL.Fulfillment.PurchaseOrder purchaseOrderOperation = SV.Framework.DAL.Fulfillment.PurchaseOrder.CreateInstance<DAL.Fulfillment.PurchaseOrder>();
            return purchaseOrderOperation.GerPurchaseOrderAPI(purchaseOrderRequest);
        }
        public CancelPurchaseOrderResponse CancelFulfillment(string purchaseOrderNumber, int userID)
        {
            SV.Framework.DAL.Fulfillment.PurchaseOrder purchaseOrderOperation = SV.Framework.DAL.Fulfillment.PurchaseOrder.CreateInstance<DAL.Fulfillment.PurchaseOrder>();
            return purchaseOrderOperation.CancelFulfillment(purchaseOrderNumber, userID);
        }
        public void FulfillmentUpdate(SV.Framework.Models.Fulfillment.Fulfillment fulfillmentInfo, int companyID, int userID, out int recordCount, out int returnCount)
        {
            SV.Framework.DAL.Fulfillment.PurchaseOrder purchaseOrderOperation = SV.Framework.DAL.Fulfillment.PurchaseOrder.CreateInstance<DAL.Fulfillment.PurchaseOrder>();
            purchaseOrderOperation.FulfillmentUpdate(fulfillmentInfo, companyID, userID, out recordCount, out returnCount);
        }
        public List<BasePurchaseOrderItem> GetPurchaseOrderInventorySummary(string PONumber, string ContactName, string POFromDate, string POToDate, int UserID,
                                                       string statusID, int CompanyID, string esn, string trackingNumber, string MslNumber,
                                                       string PhoneCategory, string ItemCode, string StoreID, string POType, string customerOrderNumber,
                                                       string shipFrom, string shipTo, int PO_ID, out List<FulfillmentStatus> statusList)
        {
            SV.Framework.DAL.Fulfillment.PurchaseOrder purchaseOrderOperation = SV.Framework.DAL.Fulfillment.PurchaseOrder.CreateInstance<DAL.Fulfillment.PurchaseOrder>();

            return purchaseOrderOperation.GetPurchaseOrderInventorySummary(PONumber,ContactName, POFromDate, POToDate, UserID, statusID, CompanyID, esn, trackingNumber, MslNumber, PhoneCategory, ItemCode, StoreID, POType, customerOrderNumber, shipFrom, shipTo, PO_ID, out statusList);
        }

        //public static bool AuthenticateRequestNew(clsAuthentication AuthenticateUser, out int userid, out int companyID)
        //{
        //    userid = companyID = 0;
        //    bool returnValue = false;
        //    returnValue = clsUser.ValidateUserNew(AuthenticateUser, out userid, out companyID);
        //    //int userId = 0;
        //    //userId = clsUser.ValidateUser(AuthenticateUser);
        //    return returnValue;
        //}


    }

}
