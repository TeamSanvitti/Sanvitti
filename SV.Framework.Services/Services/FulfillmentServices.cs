using System;
using System.Collections.Generic;
using System.Text;
using SV.Framework.Models.Common;
using SV.Framework.Models.Fulfillment;
using SV.Framework.Models.Authenticate;
using SV.Framework.Authenticate;
using SV.Framework.Models.Service;
using System.Collections;
using System.Linq;
//using SV.Framework.Services.Model;

namespace SV.Framework.Services
{
    public class FulfillmentServices 
    {
        public PurchaseOrderResponse addPurchaseOrder(PurchaseOrderRequest purchaseOrderRequest)
        {
            SV.Framework.Authenticate.AuthenticationOperation authenticationOperation = SV.Framework.Authenticate.AuthenticationOperation.CreateInstance<SV.Framework.Authenticate.AuthenticationOperation>();
            SV.Framework.Fulfillment.PurchaseOrder purchaseOrderOperation = SV.Framework.Fulfillment.PurchaseOrder.CreateInstance<SV.Framework.Fulfillment.PurchaseOrder>();
            SV.Framework.Inventory.clsInventoryDB clsInventory = SV.Framework.Inventory.clsInventoryDB.CreateInstance<SV.Framework.Inventory.clsInventoryDB>();
            SV.Framework.Fulfillment.LogOperations logOperations = SV.Framework.Fulfillment.LogOperations.CreateInstance<SV.Framework.Fulfillment.LogOperations>();

            PurchaseOrderResponse purchaseOrderResponse = new PurchaseOrderResponse();
            purchaseOrderResponse.ErrorCode = ResponseErrorCode.MissingParameter.ToString();
            if (purchaseOrderRequest != null)
            {
                string requestXML = clsGeneral.SerializeObject(purchaseOrderRequest);

                LogModel request = new LogModel();
                request.RequestData = requestXML;
                request.ModuleName = "addPurchaseOrder";
                request.RequestTimeStamp = DateTime.Now;
                Exception ex = default;
                try
                {
                    int userId = authenticationOperation.AuthenticateRequest(purchaseOrderRequest.Authentication, out ex);
                    request.UserID = userId;
                    if (ex != null)
                    {
                        purchaseOrderResponse.Comment = ex.Message;
                        purchaseOrderResponse.ErrorCode = ResponseErrorCode.ErrowWhileLoadingData.ToString();
                        purchaseOrderResponse.PurchaseOrderNumber = default;
                        request.ResponseData = clsGeneral.SerializeObject(purchaseOrderResponse);
                        request.ResponseTimeStamp = DateTime.Now;
                        request.ExceptionOccured = true;
                        request.ReturnMessage = ResponseErrorCode.ErrowWhileLoadingData.ToString();
                    }
                    else
                    {
                        if (userId > 0)
                        {
                            SV.Framework.Models.Inventory.InventoryList inventoryList = default;// new InventoryList();                        
                            inventoryList = clsInventory.GetInventoryItems(userId, -1, 0);
                            if (inventoryList != null)
                            {
                                purchaseOrderResponse = purchaseOrderOperation.CreatePurchaseOrder(purchaseOrderRequest, inventoryList, userId);
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
                    logOperations.ApiLogInsert(request);
                }
            }
            return purchaseOrderResponse;
        }


        public PurchaseOrderInfoResponse GetPurchaseOrder(PurchaseOrderInfoRequest purchaseOrderRequest)
        {
            SV.Framework.Fulfillment.PurchaseOrder purchaseOrderOperation = SV.Framework.Fulfillment.PurchaseOrder.CreateInstance<SV.Framework.Fulfillment.PurchaseOrder>();
            SV.Framework.Authenticate.AuthenticationOperation authenticationOperation = SV.Framework.Authenticate.AuthenticationOperation.CreateInstance<SV.Framework.Authenticate.AuthenticationOperation>();
            SV.Framework.Fulfillment.LogOperations logOperations = SV.Framework.Fulfillment.LogOperations.CreateInstance<SV.Framework.Fulfillment.LogOperations>();
            PurchaseOrderInfoResponse purchaseOrderInfoResponse = new PurchaseOrderInfoResponse();
            Exception ex = default;
            purchaseOrderInfoResponse.ErrorCode = ResponseErrorCode.MissingParameter.ToString();
            if (purchaseOrderRequest != null)
            {
                string requestXML = clsGeneral.SerializeObject(purchaseOrderRequest);

                LogModel request = new LogModel();
                request.RequestData = requestXML;
                request.ModuleName = "GetPurchaseOrder";
                request.RequestTimeStamp = DateTime.Now;
                try
                {
                    int userId = authenticationOperation.AuthenticateRequest(purchaseOrderRequest.Authentication, out ex);
                    request.UserID = userId;
                    if (ex != null)
                    {
                        purchaseOrderInfoResponse.Comment = ex.Message;
                        purchaseOrderInfoResponse.ErrorCode = ResponseErrorCode.ErrowWhileLoadingData.ToString();
                        purchaseOrderInfoResponse.PurchaseOrder = default;
                        request.ResponseData = clsGeneral.SerializeObject(purchaseOrderInfoResponse);
                        request.ResponseTimeStamp = DateTime.Now;
                        request.ExceptionOccured = true;
                        request.ReturnMessage = ResponseErrorCode.ErrowWhileLoadingData.ToString();
                    }
                    else
                    {
                        purchaseOrderRequest.UserId = userId;
                        purchaseOrderInfoResponse = purchaseOrderOperation.GerPurchaseOrderAPI(purchaseOrderRequest);
                    }

                }
                catch (Exception exc)
                {
                    ex = exc;
                    purchaseOrderInfoResponse.Comment = ex.Message;
                    purchaseOrderInfoResponse.ErrorCode = ResponseErrorCode.ErrowWhileLoadingData.ToString();
                    purchaseOrderInfoResponse.PurchaseOrder = default;
                    request.ResponseData = clsGeneral.SerializeObject(purchaseOrderInfoResponse);
                    request.ResponseTimeStamp = DateTime.Now;
                    request.ExceptionOccured = true;
                    request.ReturnMessage = ResponseErrorCode.ErrowWhileLoadingData.ToString();
                    //LogOperations.ApiLogInsert(request);
                }
                finally
                {
                    logOperations.ApiLogInsert(request);
                }
            }
            return purchaseOrderInfoResponse;
        }

        public CancelPurchaseOrderResponse CancelFulfillment(PurchaseOrderCancelRequest fulfillmentRequest)
        {
            SV.Framework.Fulfillment.PurchaseOrder purchaseOrderOperation = SV.Framework.Fulfillment.PurchaseOrder.CreateInstance<SV.Framework.Fulfillment.PurchaseOrder>();
            SV.Framework.Authenticate.AuthenticationOperation authenticationOperation = SV.Framework.Authenticate.AuthenticationOperation.CreateInstance<SV.Framework.Authenticate.AuthenticationOperation>();
            SV.Framework.Fulfillment.LogOperations logOperations = SV.Framework.Fulfillment.LogOperations.CreateInstance<SV.Framework.Fulfillment.LogOperations>();

            CancelPurchaseOrderResponse serviceResponse = new CancelPurchaseOrderResponse();
            serviceResponse.ErrorCode = ResponseErrorCode.MissingParameter.ToString();
            if (fulfillmentRequest != null)
            {
                string requestXML = clsGeneral.SerializeObject(fulfillmentRequest);

                LogModel request = new LogModel();
                request.RequestData = requestXML;
                request.ModuleName = "CancelFulfillment";
                request.RequestTimeStamp = DateTime.Now;
                Exception ex = default;

                try
                {
                    int userId = authenticationOperation.AuthenticateRequest(fulfillmentRequest.Authentication, out ex);
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
                        serviceResponse = purchaseOrderOperation.CancelFulfillment(fulfillmentRequest.PurchaseOrderNumber, userId);
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
                    logOperations.ApiLogInsert(request);
                }
            }
            return serviceResponse;
        }
        public PurchaseOrderShipmentAPIResponse GetPurchaseOrderShipmentAPI(PurchaseOrderShipmentRequest serviceRequest)
        {
            SV.Framework.Fulfillment.FulfillmentReportOperation fulfillmentReportOperation = SV.Framework.Fulfillment.FulfillmentReportOperation.CreateInstance<SV.Framework.Fulfillment.FulfillmentReportOperation>();
            SV.Framework.Authenticate.AuthenticationOperation authenticationOperation = SV.Framework.Authenticate.AuthenticationOperation.CreateInstance<SV.Framework.Authenticate.AuthenticationOperation>();
            SV.Framework.Fulfillment.LogOperations logOperations = SV.Framework.Fulfillment.LogOperations.CreateInstance<SV.Framework.Fulfillment.LogOperations>();
            PurchaseOrderShipmentAPIResponse serviceResponse = new PurchaseOrderShipmentAPIResponse();
            serviceResponse.ReturnCode = ResponseErrorCode.MissingParameter.ToString();
            if (serviceRequest != null)
            {
                string requestXML = clsGeneral.SerializeObject(serviceRequest);

                LogModel request = new LogModel();
                request.RequestData = requestXML;
                request.ModuleName = "GetPurchaseOrderShipment";
                request.RequestTimeStamp = DateTime.Now;
                Exception ex = default;

                try
                {
                    CredentialValidation credentialValidation = authenticationOperation.AuthenticateRequest(serviceRequest.UserCredentials, out ex);
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
                            List<PurchaseOrderShipping> shipments = fulfillmentReportOperation.GetPurchaseOrderShipmentAPI(serviceRequest, credentialValidation.CompanyID);

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
                    //logOperations.ApiLogInsert(request);
                }
            }
            return serviceResponse;
        }


        public PurchaseOrderShippingResponse GetPurchaseOrderShipmentToBeSent(PurchaseOrderShippingRequest serviceRequest)
        {
            SV.Framework.Fulfillment.PurchaseOrderSendTracking purchaseOrderSendTracking = SV.Framework.Fulfillment.PurchaseOrderSendTracking.CreateInstance<SV.Framework.Fulfillment.PurchaseOrderSendTracking>();
            SV.Framework.Authenticate.AuthenticationOperation authenticationOperation = SV.Framework.Authenticate.AuthenticationOperation.CreateInstance<SV.Framework.Authenticate.AuthenticationOperation>();
            SV.Framework.Fulfillment.LogOperations logOperations = SV.Framework.Fulfillment.LogOperations.CreateInstance<SV.Framework.Fulfillment.LogOperations>();
            PurchaseOrderShippingResponse serviceResponse = new PurchaseOrderShippingResponse();

            serviceResponse.ReturnCode = ResponseErrorCode.MissingParameter.ToString();
            if (serviceRequest != null)
            {
                string requestXML = clsGeneral.SerializeObject(serviceRequest);

                LogModel request = new LogModel();
                request.RequestData = requestXML;
                request.ModuleName = "GetPurchaseOrderShipmentToBeSent";
                request.RequestTimeStamp = DateTime.Now;
                Exception ex = default;

                try
                {
                    CredentialValidation credentialValidation = authenticationOperation.AuthenticateRequest(serviceRequest.UserCredentials, out ex);

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

                            List<PurchaseOrderShipmentDB> shipmentdb = purchaseOrderSendTracking.GetPurchaseOrderToBeSent(credentialValidation.CompanyID, "", "", "", false, "");
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
                    logOperations.ApiLogInsert(request);
                }
            }
            return serviceResponse;
        }

        //public SetPurchaseOrderShippingResponse SetPurchaseOrderShipment(SetPurchaseOrderShippingRequest serviceRequest)
        //{
        //    ShipmentOperation operation = new ShipmentOperation();
        //    return operation.SetPurchaseOrderShipment(serviceRequest);
        //}

        public FulfillmentResponse FulfillmentOrderUpdate(FulfillmentRequest serviceRequest)
        {
            SV.Framework.Fulfillment.PurchaseOrder purchaseOrder = SV.Framework.Fulfillment.PurchaseOrder.CreateInstance<SV.Framework.Fulfillment.PurchaseOrder>();
            SV.Framework.Authenticate.AuthenticationOperation authenticationOperation = SV.Framework.Authenticate.AuthenticationOperation.CreateInstance<SV.Framework.Authenticate.AuthenticationOperation>();
            SV.Framework.Fulfillment.LogOperations logOperations = SV.Framework.Fulfillment.LogOperations.CreateInstance<SV.Framework.Fulfillment.LogOperations>();
            SV.Framework.Inventory.clsInventoryDB clsInventory = SV.Framework.Inventory.clsInventoryDB.CreateInstance<SV.Framework.Inventory.clsInventoryDB>();
            FulfillmentResponse serviceResponse = new FulfillmentResponse();

            serviceResponse.ReturnCode = ResponseErrorCode.MissingParameter.ToString();

            if (serviceRequest != null)
            {
                string requestXML = clsGeneral.SerializeObject(serviceRequest);

                LogModel request = new LogModel();
                request.RequestData = requestXML;
                request.ModuleName = "UpdatePurchaseOrder";
                request.RequestTimeStamp = DateTime.Now;
                Exception ex = default;

                int recordCount = 0;
                int returnCount = 0;
                string errorMessage = string.Empty;

                //bool approval = false;

                try
                {
                    CredentialValidation credentialValidation = authenticationOperation.AuthenticateRequest(serviceRequest.UserCredentials, out ex);
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
                    else
                    {
                        if (credentialValidation != null && credentialValidation.CompanyID > 0)
                        {
                            if (ValidateRequiredFields(serviceRequest.FulfillmentOrder, out errorMessage))
                            {
                                SV.Framework.Models.Inventory.InventoryList inventoryList = new SV.Framework.Models.Inventory.InventoryList();

                                InventoryItemRequest inventoryItemRequest = new InventoryItemRequest();
                                inventoryItemRequest.Authentication.UserName = serviceRequest.UserCredentials.UserName;
                                inventoryItemRequest.Authentication.Password = serviceRequest.UserCredentials.Password;
                                inventoryList = clsInventory.GetInventoryItems(credentialValidation.UserID, -1, 0);


                                if (inventoryList != null)
                                {
                                    //errorMessage = ValidateDuplicateSKU(serviceRequest.FulfillmentOrder.FulfillmentItems);
                                    if (ValidateDuplicateSKU(serviceRequest.FulfillmentOrder.FulfillmentItems, out errorMessage))
                                    {
                                        if (ValidationItems(serviceRequest.FulfillmentOrder.FulfillmentItems, inventoryList, out errorMessage))
                                        {
                                            purchaseOrder.FulfillmentUpdate(serviceRequest.FulfillmentOrder, credentialValidation.CompanyID, credentialValidation.UserID, out recordCount, out returnCount);
                                            if (returnCount == 0)
                                            {
                                                serviceResponse.Comment = "Fulfillment Order item count is: " + recordCount;
                                                serviceResponse.ReturnCode = ResponseErrorCode.UpdatedSuccessfully.ToString();
                                            }
                                            else
                                            {
                                                if (returnCount == 1)
                                                {
                                                    serviceResponse.Comment = serviceRequest.FulfillmentOrder.FulfillmentNumber + " fulfillment number does not exists."; ;
                                                    serviceResponse.ReturnCode = ResponseErrorCode.FulfillmentOrderNotExists.ToString();
                                                }
                                                else if (returnCount == 2)
                                                {
                                                    serviceResponse.Comment = serviceRequest.FulfillmentOrder.FulfillmentNumber + " fulfillment number already processed."; ;
                                                    serviceResponse.ReturnCode = ResponseErrorCode.FulfillmentOrderAlreadyProcessed.ToString();
                                                }
                                                else if (returnCount == 3)
                                                {
                                                    serviceResponse.Comment = serviceRequest.FulfillmentOrder.State + " is not a valid state code.";
                                                    serviceResponse.ReturnCode = ResponseErrorCode.StateCodeIsNotCorrect.ToString();
                                                }
                                                else if (returnCount == 4)
                                                {
                                                    serviceResponse.Comment = serviceRequest.FulfillmentOrder.ShipVia + " is not a valid shipvia code.";
                                                    serviceResponse.ReturnCode = ResponseErrorCode.ShipByIsNotCorrect.ToString();
                                                }
                                                else if (returnCount == 5)
                                                {
                                                    serviceResponse.Comment = "Duplicate SKU are  not allowed!";
                                                    serviceResponse.ReturnCode = ResponseErrorCode.DuplicateItemFound.ToString();
                                                }
                                            }
                                        }
                                        else
                                        {
                                            serviceResponse.Comment = "Fulfillment Order items (" + errorMessage + ") are not valid, please check the item code from catalog";
                                            serviceResponse.ReturnCode = ResponseErrorCode.InconsistantData.ToString();
                                        }
                                    }
                                    else
                                    {
                                        serviceResponse.Comment = errorMessage;
                                        serviceResponse.ReturnCode = ResponseErrorCode.DuplicateItemFound.ToString();
                                    }

                                }
                                else
                                {
                                    serviceResponse.Comment = "Inventory is not assinged this user";
                                    serviceResponse.ReturnCode = ResponseErrorCode.InconsistantData.ToString();

                                }
                            }
                            else
                            {
                                serviceResponse.Comment = errorMessage;
                                serviceResponse.ReturnCode = ResponseErrorCode.MissingParameter.ToString();
                            }



                        }
                        else
                        {
                            serviceResponse.Comment = "Cannot Authenticate User";
                            serviceResponse.ReturnCode = ResponseErrorCode.CannotAuthenticateUser.ToString();

                        }
                        request.UserID = credentialValidation.UserID;
                        request.ResponseData = clsGeneral.SerializeObject(serviceResponse);
                        request.ResponseTimeStamp = DateTime.Now;
                        request.ExceptionOccured = false;
                        request.ReturnMessage = serviceResponse.ReturnCode;
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
                    //LogOperations.ApiLogInsert(request);
                }
                finally
                {
                    logOperations.ApiLogInsert(request);
                }
            }
            return serviceResponse;
        }

        public  ShippByResponse GetShippingCodes()
        {
            ShippByResponse serviceResponse = new ShippByResponse();
            SV.Framework.Fulfillment.PurchaseOrder purchaseOrder = SV.Framework.Fulfillment.PurchaseOrder.CreateInstance<SV.Framework.Fulfillment.PurchaseOrder>();
            SV.Framework.Fulfillment.LogOperations logOperations = SV.Framework.Fulfillment.LogOperations.CreateInstance<SV.Framework.Fulfillment.LogOperations>();

            serviceResponse.ErrorCode = ResponseErrorCode.None.ToString();

            string requestXML = string.Empty;// clsGeneral.SerializeObject(serviceRequest);

            LogModel request = new LogModel();
            request.RequestData = requestXML;
            request.ModuleName = "GetShippingCodes";
            request.RequestTimeStamp = DateTime.Now;
            Exception ex = default;

            try
            {
                List<ShipBy> shippByList = purchaseOrder.GetShipByList();
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
                logOperations.ApiLogInsert(request);
            }
            return serviceResponse;
        }

        

        //public static FulfillmentReportResponse FulfillmentOrdersProvisionalList(FulfillmentReportRequest poRequest)
        //{
        //    FulfillmentReportResponse serviceResponse = new FulfillmentReportResponse();
        //    serviceResponse.ReturnCode = ResponseErrorCode.MissingParameter.ToString();
        //    string requestXML = clsGeneral.SerializeObject(poRequest);

        //    LogModel request = new LogModel();
        //    request.RequestData = requestXML;
        //    request.ModuleName = "GetPurchaseOrderProvisioning";
        //    request.RequestTimeStamp = DateTime.Now;
        //    Exception ex = null;

        //    try
        //    {

        //        if (poRequest != null)
        //        {

        //            int userId = PurchaseOrder.AuthenticateRequest(poRequest.Authentication, out ex);
        //            if (ex != null)
        //            {
        //                serviceResponse.Comment = ex.Message;
        //                serviceResponse.ReturnCode = ResponseErrorCode.ErrowWhileLoadingData.ToString();

        //                request.ResponseData = clsGeneral.SerializeObject(serviceResponse);
        //                request.ResponseTimeStamp = DateTime.Now;
        //                request.ExceptionOccured = true;
        //                request.ReturnMessage = ResponseErrorCode.ErrowWhileLoadingData.ToString();
        //                //LogOperations.ApiLogInsert(request);
        //            }
        //            if (userId > 0)
        //            {
        //                serviceResponse = PurchaseOrder.FulfillmentOrdersProvisionalList(poRequest, userId);

        //            }
        //            else
        //            {
        //                serviceResponse.Comment = "Cannot authenticate user";
        //                serviceResponse.ReturnCode = ResponseErrorCode.CannotAuthenticateUser.ToString();
        //            }
        //            request.UserID = userId;
        //            request.ResponseData = clsGeneral.SerializeObject(serviceResponse);
        //            request.ResponseTimeStamp = DateTime.Now;
        //            request.ExceptionOccured = false;
        //            request.ReturnMessage = serviceResponse.Comment;

        //        }
        //    }
        //    catch (Exception exc)
        //    {
        //        ex = exc;
        //        serviceResponse.FulfillmentOrderList = null;
        //        serviceResponse.Comment = ex.Message;
        //        serviceResponse.ReturnCode = ResponseErrorCode.ErrowWhileLoadingData.ToString();
        //        request.ResponseData = clsGeneral.SerializeObject(serviceResponse);
        //        request.ResponseTimeStamp = DateTime.Now;
        //        request.ExceptionOccured = true;
        //        request.ReturnMessage = ResponseErrorCode.ErrowWhileLoadingData.ToString();

        //    }
        //    finally
        //    {
        //        LogOperations.ApiLogInsert(request);
        //    }

        //    return serviceResponse;
        //}


        private List<PurchaseOrderShipment> PopulateShipment(List<PurchaseOrderShipmentDB> shipment_DB)
        {
            int po_id = 0;
            string trackingNumber = string.Empty;
            var shipmentDB = shipment_DB.GroupBy(e => new { e.PO_ID, e.TrackingNumber }).ToList();
            List<PurchaseOrderShipment> shipments = new List<PurchaseOrderShipment>();
            //List<PurchaseOrderShipmentDB> shipmentDB = Session["shipmentDB"] as List<PurchaseOrderShipmentDB>;
            foreach (PurchaseOrderShipmentDB row in shipmentDB)
            {
                po_id = row.PO_ID;
                trackingNumber = row.TrackingNumber;

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

        private bool ValidateRequiredFields(SV.Framework.Models.Fulfillment.Fulfillment fulfillmentOrderInfo, out string returnMessage)
        {
            bool returnValue = true;
            returnMessage = string.Empty;
            Hashtable hshLineNumberDuplicateCheck = new Hashtable();
            if (fulfillmentOrderInfo != null)
            {
                if (string.IsNullOrEmpty(fulfillmentOrderInfo.FulfillmentNumber))
                {
                    returnValue = false;
                    returnMessage = "Fulfillment number is required.";
                    return returnValue;
                }
                else
                if (fulfillmentOrderInfo.FulfillmentItems != null && fulfillmentOrderInfo.FulfillmentItems.Count > 0)
                {
                    foreach (FulfillmentItem item in fulfillmentOrderInfo.FulfillmentItems)
                    {

                        if (hshLineNumberDuplicateCheck.ContainsKey(item.LineNumber))
                        {

                            returnValue = false;
                            returnMessage = string.Format("Duplicate Line number ({0}) is found", item.LineNumber);
                            return returnValue;
                        }
                        else
                        {
                            hshLineNumberDuplicateCheck.Add(item.LineNumber, item.LineNumber);
                        }
                        if (string.IsNullOrEmpty(item.SKU))
                        {
                            returnValue = false;
                            returnMessage = "SKU is required.";
                            return returnValue;

                        }
                        if (item.LineNumber == 0)
                        {
                            returnValue = false;
                            returnMessage = "Line number can not be 0.";
                            return returnValue;

                        }
                        if (item.Quantity == 0)
                        {
                            returnValue = false;
                            returnMessage = "Quantity can not be 0.";
                            return returnValue;

                        }
                    }
                }

            }
            else
            {
                returnValue = false;
                returnMessage = "There in no data to update.";
                return returnValue;

            }
            return returnValue;
        }
        private static bool ValidationItems(List<FulfillmentItem> fulfillmentitems, SV.Framework.Models.Inventory.InventoryList inventoryList, out string skuList)
        {
            bool found = true;
            skuList = string.Empty;
            foreach (FulfillmentItem inv in fulfillmentitems)
            {
                if (!inventoryList.Exists(inv.SKU))
                {
                    skuList = inv.SKU;
                    found = false;
                    break;
                }
            }

            return found;
        }
        private static bool ValidateDuplicateSKU(List<FulfillmentItem> polist, out string errorMessage)
        {
            errorMessage = string.Empty;
            bool isNotDuplicate = true;
            // List<avii.Classes.PurchaseOrderItem> polist = purchaseOrderRequest.PurchaseOrder.PurchaseOrderItems;
            var duplicateItems = polist.GroupBy(x => x).Where(g => g.Count() > 1).Select(y => y.Key).ToList();

            // var duplicateItems = (from itemcodes in polist where itemcodes.ItemCode.ToUpper().Equals(itemCode.ToUpper()) select itemcodes).ToList();
            if (duplicateItems != null && duplicateItems.Count > 0 && !string.IsNullOrEmpty(duplicateItems[0].SKU))
            {
                errorMessage = "Duplicate SKU are  not allowed!";
                isNotDuplicate = false;
            }
            return isNotDuplicate;
        }
        

    }
}
