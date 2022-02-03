using System;
using System.ComponentModel;
using System.Text;
using System.Web.Services;
using System.Web.Services.Protocols;
using avii.Classes;
using System.Web;
using System.Web.UI;

namespace avii
{
    /// <summary>
    /// Summary description for PurchaseOrder
    /// </summary>
    
    [SoapDocumentService(RoutingStyle = SoapServiceRoutingStyle.RequestElement)]
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public partial class FulfillmentService : System.Web.Services.WebService
    {
       
        [WebMethod(EnableSession=true)]
        public PurchaseOrderResponse addPurchaseOrder(PurchaseOrderRequest purchaseOrderRequest)
        {
           // purchaseOrderRequest.PurchaseOrder.PurchaseOrderNumber = DateTime.Now.Ticks.ToString();
            //purchaseOrder.CustomerOrderNumber = string.Empty;
            PurchaseOrderResponse purchaseOrderResponse = new PurchaseOrderResponse();
            string requestXML = clsGeneral.SerializeObject(purchaseOrderRequest);
           
            LogModel request = new LogModel();
            request.RequestData = requestXML;
            request.ModuleName = "addPurchaseOrder";
            request.RequestTimeStamp = DateTime.Now;
            Exception ex = null;
            try
            {
                int userId = avii.Classes.PurchaseOrder.AuthenticateRequest(purchaseOrderRequest.Authentication, out ex);
                request.UserID = userId;
                request.CompanyID = 0;
                if (ex != null)
                {
                    purchaseOrderResponse.Comment = ex.Message;
                    purchaseOrderResponse.ErrorCode = ResponseErrorCode.ErrowWhileLoadingData.ToString();
                    purchaseOrderResponse.PurchaseOrderNumber = null;
                    request.ResponseData = clsGeneral.SerializeObject(purchaseOrderResponse);
                    request.ResponseTimeStamp = DateTime.Now;
                    request.ExceptionOccured = true;
                    request.ReturnMessage = ResponseErrorCode.ErrowWhileLoadingData.ToString();
                    //LogOperations.ApiLogInsert(request);
                }
                else
                {
                    if (userId > 0)
                    {

                        InventoryList inventoryList = new InventoryList();
                        string sessionKey = "inv" + purchaseOrderRequest.Authentication.UserName.Trim();

                        if (Session[sessionKey] == null)
                        {
                            InventoryItemRequest inventoryItemRequest = new InventoryItemRequest();
                            inventoryItemRequest.Authentication.UserName = purchaseOrderRequest.Authentication.UserName;
                            inventoryItemRequest.Authentication.Password = purchaseOrderRequest.Authentication.Password;
                            inventoryList = avii.Classes.PurchaseOrder.GetInventoryItems(inventoryItemRequest);
                            Session[sessionKey] = inventoryList;
                        }
                        else
                        {
                            inventoryList = (InventoryList)Session[sessionKey];
                        }

                        if (inventoryList != null)
                        {
                            purchaseOrderResponse = avii.Classes.PurchaseOrder.CreatePurchaseOrder(purchaseOrderRequest, inventoryList, userId);

                            
                        }
                        else
                        {
                            purchaseOrderResponse.Comment = "Inventory is not assinged this user";
                            purchaseOrderResponse.ErrorCode = ResponseErrorCode.InconsistantData.ToString();
                            purchaseOrderResponse.PurchaseOrderNumber = null;
                        }
                    }
                    else
                    {
                        purchaseOrderResponse.Comment = "Cannot authenticate user";
                        purchaseOrderResponse.ErrorCode = ResponseErrorCode.CannotAuthenticateUser.ToString();
                        //purchaseOrderResponse.PurchaseOrderNumber = purchaseOrderRequest.PurchaseOrder.PurchaseOrderNumber;
                    }
                    request.ResponseData = clsGeneral.SerializeObject(purchaseOrderResponse);
                    request.ResponseTimeStamp = DateTime.Now;
                    request.ExceptionOccured = false;
                    request.ReturnMessage = purchaseOrderResponse.Comment;

                }

            }
            catch (Exception exc)
            {
                ex = exc;
                purchaseOrderResponse.Comment = ex.Message;
                purchaseOrderResponse.ErrorCode = ResponseErrorCode.ErrowWhileLoadingData.ToString();
                purchaseOrderResponse.PurchaseOrderNumber = null;
                request.ResponseData = clsGeneral.SerializeObject(purchaseOrderResponse);
                request.ResponseTimeStamp = DateTime.Now;
                request.ExceptionOccured = true;
                request.ReturnMessage = ResponseErrorCode.ErrowWhileLoadingData.ToString();
                //LogOperations.ApiLogInsert(request);
            }
            finally
            {
                LogOperations.ApiLogInsert(request);
            }
             return purchaseOrderResponse;
        }
        [WebMethod]
        public PurchaseOrderInfoResponse GetPurchaseOrder(PurchaseOrderInfoRequest purchaseOrderRequest)
        {
            return avii.Classes.PurchaseOrder.GerPurchaseOrderAPI(purchaseOrderRequest);
        }

        // Update Fulfillment Order
        [WebMethod]
        public FulfillmentResponse UpdatePurchaseOrder(FulfillmentRequest serviceRequest)
        {
            avii.Classes.FulfillmentOperations operation = new FulfillmentOperations();
            return operation.FulfillmentOrderUpdate(serviceRequest);
        }


        [WebMethod]
        public CurrentStockResponse GetCurrentStock(CurrentStockRequest serviceRequest)
        {
            avii.Classes.StockOperation operation = new StockOperation();
            return operation.GetInventoryStockCurrent(serviceRequest);
        }

        //[WebMethod]
        public StockReceivalResponse GetStockReceive(StockReceivalRequest serviceRequest)
        {
            avii.Classes.StockOperation operation = new StockOperation();
            return operation.GetInventoryStockReceival(serviceRequest);
        }
        [WebMethod]
        public RunningStockResponse GetRunningStock(RunningStockRequest serviceRequest)
        {
            avii.Classes.StockOperation operation = new StockOperation();
            return operation.GetRunningStock(serviceRequest);
        }

        //[WebMethod]
        //public PurchaseOrderShipmentResponse GetPurchaseOrderShipment(PurchaseOrderShipmentRequest serviceRequest)
        //{
        //    avii.Classes.PurchaseOrderShipmentTracking operation = new PurchaseOrderShipmentTracking();
        //    return operation.GetPurchaseOrderShipment(serviceRequest);
        //}
        [WebMethod]
        public PurchaseOrderShipmentAPIResponse GetPurchaseOrderShipment(PurchaseOrderShipmentRequest serviceRequest)
        {
            avii.Classes.PurchaseOrderShipmentTracking operation = new PurchaseOrderShipmentTracking();
            return operation.GetPurchaseOrderShipmentAPI(serviceRequest);
        }

        [WebMethod]
        public PurchaseOrderShippingResponse GetPurchaseOrderShipmentToBeSent(PurchaseOrderShippingRequest serviceRequest)
        {
            avii.Classes.PurchaseOrderSendTracking operation = new PurchaseOrderSendTracking();
            return operation.GetPurchaseOrderShipmentToBeSent(serviceRequest);
        }
        //[WebMethod]
        public SetPurchaseOrderShippingResponse SetPurchaseOrderShipment(SetPurchaseOrderShippingRequest serviceRequest)
        {
            avii.Classes.PurchaseOrderSendTracking operation = new PurchaseOrderSendTracking();
            return operation.SetPurchaseOrderShipment(serviceRequest);
        }
        // Get Bad ESN's
        //[WebMethod]
        public BadESNResponse GetBadESN(BadESNRequest serviceRequest)
        {
            avii.Classes.BabEsnOperation operation = new BabEsnOperation();
            return operation.GetBadESN(serviceRequest);

        }

        // Get GetReassignSKU with ESN's
        //[WebMethod]
        public ReassignSKUResponse GetReassignSKU(ReassignSKURequest serviceRequest)
        {
            avii.Classes.BabEsnOperation operation = new BabEsnOperation();
            return operation.GetReassignSkuList(serviceRequest);

        }

        //Get CUSTOMER'S SHIPVIA INFO
        //[WebMethod]
        public CustomerShipViaResponse GetCustomerShipViaSummary(CustomerShipViaRequest serviceRequest)
        {
            avii.Classes.ReportOperations operation = new ReportOperations();
            return operation.GetCustomerShipViaSummary(serviceRequest);
        }

        //Get FulfillmentTracking info
        //[WebMethod]
        public FulfillmentTrackingResponse GetFulfillmentShipment(FulfillmentTrackingRequest serviceRequest)
        {
            avii.Classes.ReportOperations operation = new ReportOperations();
            return operation.GetFulfillmentTracking(serviceRequest);
        }

        //Get ESN Inventory Detail
        //[WebMethod]
        public EsnInventoryResponse GetEsnInventorySummary(EsnInventoryRequest serviceRequest)
        {
            avii.Classes.ReportOperations operation = new ReportOperations();
            return operation.GetEsnInventorySummary(serviceRequest);
        }

        //Get ESN Repository Detail
        //[WebMethod]
        public EsnRepositoryDetailResponse GetEsnRepositoryDetail(EsnRepositoryDetailRequest serviceRequest)
        {
            avii.Classes.ReportOperations operation = new ReportOperations();
            return operation.GetEsnRepositoryDetail(serviceRequest);
        }
        //Get ESN Repository List
        [WebMethod]
        public EsnRepositoryResponse GetEsnRepositoryList(EsnRepositoryRequest serviceRequest)
        {
            avii.Classes.ReportOperations operation = new ReportOperations();
            return operation.GetEsnRepository(serviceRequest);

        }
        //RMA ESN Listing
        [WebMethod]
        public RmaEsnListingResponse GetRmaEsnListing(RmaEsnListingRequest serviceRequest)
        {
            avii.Classes.ReportOperations operation = new ReportOperations();
            return operation.GetRmaEsnListing(serviceRequest);
        }
        [WebMethod]
        public RmaEsnDetailResponse GetRmaEsnDetail(RmaEsnListingRequest serviceRequest)
        {
            avii.Classes.ReportOperations operation = new ReportOperations();
            return operation.GetRmaEsnDetail(serviceRequest);
        }

        //Product and RMA reasons summary
        // [WebMethod]
        public ProductRmaReasonsResponse GetRmaPivotSKUandReason(ProductRmaReasonsRequest serviceRequest)
        {
            avii.Classes.ReportOperations operation = new ReportOperations();
            return operation.GetProductRmaReasons(serviceRequest);

        }
        //RMA Statuses summary
       // [WebMethod]
        public RMAStatusResponse GetRmaStatusesPivot(RMAStatusRequest serviceRequest)
        {
            avii.Classes.ReportOperations operation = new ReportOperations();
            return operation.GetCustomerRmaStatuses(serviceRequest);

        }

        //FULFILLMENT ORDER STATUSES summary
       // [WebMethod]
        public FulfillmentOrderStatusResponse GetFilfillmentPivotStatuses(FulfillmentOrderStatusRequest serviceRequest)
        {
            avii.Classes.ReportOperations operation = new ReportOperations();
            return operation.GetCustomerFulfillmentStatuses(serviceRequest);

        }
        
        //New Bad ESN Upload
       // [WebMethod]
        public InventoryEsnResponse SetBadEsn(InventoryEsnRequest serviceRequest)
        {
            InventoryEsnResponse serviceResponse = new InventoryEsnResponse();
            BabEsnOperation operation = new BabEsnOperation();
            serviceResponse = operation.BadEsnUpdateAPI(serviceRequest);
            return serviceResponse;
        }

        [WebMethod]
        public CancelPurchaseOrderResponse CancelFulfillment(PurchaseOrderCancelRequest fulfillmentRequest)
        {
            return avii.Classes.PurchaseOrder.CancelFulfillment(fulfillmentRequest);

        }

        [WebMethod]
        public ShippByResponse GetShippingCodes()
        {
            return avii.Classes.PurchaseOrder.GetShippingCodes();
        }

        //[WebMethod]
        public FulfillmentOrderResponse GetFulfillmentOrder(FulfillmentOrderRequest fulfillmentOrder)
        {
            return avii.Classes.PurchaseOrder.GerFulfillmentOrder(fulfillmentOrder);
        }

        [WebMethod]
        public InventoryResponse GetInventorySKU(InventoryRequest inventoryRequest)
        {
            return avii.Classes.PurchaseOrder.GetInventoryList(inventoryRequest);
        }
        [WebMethod]
        public CompanyStoreResponse GetCompanyStores(CompanyStoreRequest companyStoreRequest)
        {
            return avii.Classes.CompanyOperations.GetCompanyStore(companyStoreRequest);
        }
        //[WebMethod]
        public PurchaseOrderProvisioningResponse PurchaseOrderProvisioning(PurchaseOrderProvisioningRequest purchaseOrderProvisioningRequest)
        {
            return avii.Classes.PurchaseOrder.PurchaseOrderProvisioning(purchaseOrderProvisioningRequest);
        }
        //new to get RMAs search report
        //[WebMethod]
        //public RmaResponse GetRMAReport(RMASearchCriteria rmaRequest)
        //{
        //    return avii.Classes.RMAUtility.GetRMAReport(rmaRequest);
        //}

        //to get FulfillmentOrdersProvisionalList report
        //[WebMethod]
        public FulfillmentReportResponse GetPurchaseOrderProvisioning(FulfillmentReportRequest poRequest)
        {

            return avii.Classes.PurchaseOrder.FulfillmentOrdersProvisionalList(poRequest);
        }

        //to get FulfillmentOrderWithHeaderResponse report
        //[WebMethod]
        public FulfillmentOrderWithHeaderResponse FulfillmentOrdersListWithHeader(FulfillmentReportRequest poRequest)
        {
            return avii.Classes.PurchaseOrder.FulfillmentOrdersListWithHeader(poRequest);
        }
        //new to get FulfillmentOrdersListItems report
        //[WebMethod]
        public FulfillmentOrderListItemsResponse FulfillmentOrdersListItems(FulfillmentReportRequest poRequest)
        {
            return avii.Classes.PurchaseOrder.FulfillmentOrdersListItems(poRequest);
        }

        //new to get RMA search result
        //[WebMethod]
        //public RmaResponses GetRMA(RMARequests rmaRequest)
        //{
        //    return avii.Classes.RMAUtility.GetRMA(rmaRequest);
        //}
        [WebMethod]
        public RMAResponse GetRMA(RMASearchCriteria rmaRequest)
        {
            return avii.Classes.RMAUtility.GetRMAList(rmaRequest);
        }


        ////new to Set RMA 
        //[WebMethod]
        //public RMAResponse SetRMA(RMARequest rmaRequest)
        //{
        //    return avii.Classes.RMAUtility.SetRMA(rmaRequest);
        //}
        [WebMethod(EnableSession = true)]
        public RMAAPIResponse SetRMA(RMAAPIRequest RMARequest)
        {
            RMAAPIResponse RMAResponse = new RMAAPIResponse();
            int userID = 0;
            RMAResponse = RMAUtility.CreateNewRMA(RMARequest, true, out userID);

            // Insert rma log
            //try
            //{
                //avii.Classes.RMAUtility.RMALogInsert(RMARequest, RMAResponse, userID);
            //}
            //catch (Exception ex)
            //{

            //    RMAResponse.Comment = ex.Message;
            //    //RMAResponse.ErrorCode = ex.Message;

            //}
            return RMAResponse;
        }


        // New Cancel RMA
        [WebMethod]
        public CancelRMAResponse CancelRMA(RMARequests rmaRequest)
        {
            return avii.Classes.RMAUtility.CancelRMA(rmaRequest);
            //CancelRMAResponse ss = new DeleteRMAResponse();
            //return ss;

        }
        
        [WebMethod]
        public UsersResponse GetAssignedUsers(UsersRequest userRequest)
        {
            return avii.Classes.CompanyOperations.GetAssignedUsers(userRequest);
        }

        //new to get RMAs search report
        //[WebMethod]
        //public StockResponse GetInventoryStockOnHand(StockRequest stockRequest)
        //{
        //    return avii.Classes.ReportOperations.GetStockInHandList(stockRequest);
        //}

        //[WebMethod]
        public PurchaseOrderResponse PurchaseOrderRequest(PurchaseOrderRequest purchaseOrderRequest)
        {
            return addPurchaseOrder(purchaseOrderRequest);
        }

        //[WebMethod]
        public LabelResponse GetEsnsForLabelPrint(LabelRequest serviceRequest)
        {
            return avii.Classes.ShippingLabelOperation.GetEsnsForLabelPrint(serviceRequest);
        }

        //[WebMethod]
        public ShippingLabelResponse SetShippingLabel(ShippingLabelRequest serviceRequest)
        {
            return avii.Classes.ShippingLabelOperation.ShippingLabelUpdate(serviceRequest);
        }

        //[WebMethod]
        public ServiceOrderReponse SetServiceOrder(ServiceOrderRequest serviceOrderRequest)
        {
            avii.Classes.FulfillmentService service = new avii.Classes.FulfillmentService();
            return service.SetServiceOrder(serviceOrderRequest);
        }
        //[WebMethod]
        public ServiceOrderSearchResponse GetServiceOrder(ServiceOrderSearchRequest serviceOrderRequest)
        {
            avii.Classes.FulfillmentService service = new avii.Classes.FulfillmentService();
            return service.GetServiceOrder(serviceOrderRequest);
        }
        //[WebMethod]
        public SKUStockResponse GetSKUInventory(SKUStockRequest skuStockRequest)
        {
            avii.Classes.FulfillmentService service = new avii.Classes.FulfillmentService();
            return service.GetSKUInventory(skuStockRequest);
        }


        //[WebMethod]
        public ServiceResponseNew Test(FulfillmentsTracking fulfillmentTracking)
        {
            ServiceResponseNew serviceResponse = new ServiceResponseNew();
            serviceResponse.httpStatus = "OK";
            return serviceResponse;
        }


        #region TelSpace 
        //[WebMethod]
        public PurchaseOrderShipResponse CheckOrderShippingStatus(PurchaseOrderShipRequest purchaseOrderShipRequest)
        {
            return avii.Classes.PurchaseOrder.GetPurchaseOrderShipping(purchaseOrderShipRequest);
        }

        //[WebMethod]
        public ServiceResponse ReportBadInventoryItem(InventoryBadItemRequest inventoryItems)
        {
            return avii.Classes.PurchaseOrder.ReportBadInventoryItem(inventoryItems);
        }

        //[WebMethod]
        public InventoryList GetPhonesInventory(InventoryItemRequest inventoryItemRequest)
        {
            return avii.Classes.PurchaseOrder.GetPhoneInventory(inventoryItemRequest);
        }

        //[WebMethod]
        public InventoryList GetAccessoryInventory(InventoryItemRequest inventoryItemRequest)
        {
            return avii.Classes.PurchaseOrder.GetAccessoryInventory(inventoryItemRequest);
        }

        //[WebMethod]
        public InventoryLastUpdate GetInventoryLastUpdate()
        {
            return avii.Classes.PurchaseOrder.GetInventoryLastUpdate();
        }

        #endregion

        private String UTF8ByteArrayToString(Byte[] characters)
        {

            UTF8Encoding encoding = new UTF8Encoding();

            String constructedString = encoding.GetString(characters);

            return (constructedString);

        }

    }
}
