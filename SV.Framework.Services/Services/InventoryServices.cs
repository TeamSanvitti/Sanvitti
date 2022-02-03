using System;
using System.Collections.Generic;
using System.Text;
using SV.Framework.Models.Service;
using SV.Framework.Models.Fulfillment;
using SV.Framework.Models.Authenticate;
using SV.Framework.Models.Inventory;

namespace SV.Framework.Services
{
    public class InventoryServices
    {
        public InventoryResponse GetInventoryList(InventoryRequest inventoryRequest)
        {
            SV.Framework.Inventory.InventoryOperation inventoryOperation = SV.Framework.Inventory.InventoryOperation.CreateInstance<SV.Framework.Inventory.InventoryOperation>();
            SV.Framework.Authenticate.AuthenticationOperation authenticationOperation = SV.Framework.Authenticate.AuthenticationOperation.CreateInstance<SV.Framework.Authenticate.AuthenticationOperation>();
            SV.Framework.Fulfillment.LogOperations logOperations = SV.Framework.Fulfillment.LogOperations.CreateInstance<SV.Framework.Fulfillment.LogOperations>();

            InventoryResponse serviceResponse = new InventoryResponse();
            serviceResponse.ErrorCode = ResponseErrorCode.MissingParameter.ToString();
            serviceResponse.InventoryList = default;
            if (inventoryRequest != null)
            {
                string requestXML = clsGeneral.SerializeObject(inventoryRequest);

                LogModel request = new LogModel();
                request.RequestData = requestXML;
                request.ModuleName = "GetInventorySKU";
                request.RequestTimeStamp = DateTime.Now;
                Exception ex = default;

                try
                {

                    if (inventoryRequest != null)
                    {

                        int userId = authenticationOperation.AuthenticateRequest(inventoryRequest.Authentication, out ex);
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

                            List<SV.Framework.Models.Inventory.InventorySKU> inventoryItemList = inventoryOperation.GetInventorySKUList(userId);

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
                    logOperations.ApiLogInsert(request);
                }
            }
            return serviceResponse;
        }

        public RunningStockResponse GetRunningStock(RunningStockRequest serviceRequest)
        {
            SV.Framework.Inventory.InventoryReportOperation inventoryReportOperation = SV.Framework.Inventory.InventoryReportOperation.CreateInstance<SV.Framework.Inventory.InventoryReportOperation>();
            SV.Framework.Authenticate.AuthenticationOperation authenticationOperation = SV.Framework.Authenticate.AuthenticationOperation.CreateInstance<SV.Framework.Authenticate.AuthenticationOperation>();
            SV.Framework.Fulfillment.LogOperations logOperations = SV.Framework.Fulfillment.LogOperations.CreateInstance<SV.Framework.Fulfillment.LogOperations>();
            RunningStockResponse serviceResponse = new RunningStockResponse();

            serviceResponse.ReturnCode = ResponseErrorCode.MissingParameter.ToString();
            if (serviceRequest != null)
            {
                string requestXML = clsGeneral.SerializeObject(serviceRequest);

                LogModel request = new LogModel();
                request.RequestData = requestXML;
                request.ModuleName = "GetRunningStock";
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
                            string DateFrom = serviceRequest.DateFrom.ToShortDateString() == "1/1/0001" ? null : serviceRequest.DateFrom.ToShortDateString();
                            string DateTo = serviceRequest.DateTo.ToShortDateString() == "1/1/0001" ? null : serviceRequest.DateTo.ToShortDateString();
                            //List<StockStatus> StockReceivals =  GetStockStatus(credentialValidation.CompanyID, DateFrom, DateTo, serviceRequest.SKU);
                            List<StockCount> runningStock = inventoryReportOperation.GetStockCountSummary(credentialValidation.CompanyID, DateFrom, DateTo, serviceRequest.SKU, serviceRequest.IncludeDisabledSKU);


                            if (runningStock != null && runningStock.Count > 0)
                            {

                                serviceResponse.Comment = "Successfully Retrieved";
                                serviceResponse.ReturnCode = ResponseErrorCode.SuccessfullyRetrieved.ToString();
                                serviceResponse.RunningStock = runningStock;


                            }
                            else
                            {

                                serviceResponse.Comment = "No records found";
                                serviceResponse.ReturnCode = ResponseErrorCode.NoRecordsFound.ToString();
                                serviceResponse.RunningStock = null;
                            }

                        }
                        else
                        {
                            serviceResponse.Comment = "Cannot Authenticate User";
                            serviceResponse.ReturnCode = ResponseErrorCode.CannotAuthenticateUser.ToString();
                            serviceResponse.RunningStock = null;
                        }
                        request.UserID = credentialValidation.UserID;
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

        public EsnRepositoryResponse GetEsnRepository(EsnRepositoryRequest serviceRequest)
        {
            SV.Framework.Inventory.InventoryReportOperation inventoryReportOperation = SV.Framework.Inventory.InventoryReportOperation.CreateInstance<SV.Framework.Inventory.InventoryReportOperation>();
            SV.Framework.Authenticate.AuthenticationOperation authenticationOperation = SV.Framework.Authenticate.AuthenticationOperation.CreateInstance<SV.Framework.Authenticate.AuthenticationOperation>();
            SV.Framework.Fulfillment.LogOperations logOperations = SV.Framework.Fulfillment.LogOperations.CreateInstance<SV.Framework.Fulfillment.LogOperations>();
            EsnRepositoryResponse serviceResponse = new EsnRepositoryResponse();
            serviceResponse.ReturnCode = ResponseErrorCode.MissingParameter.ToString();
            if (serviceRequest != null)
            {
                string requestXML = clsGeneral.SerializeObject(serviceRequest);

                LogModel request = new LogModel();
                request.RequestData = requestXML;
                request.ModuleName = "GetEsnRepositoryList";
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
                        logOperations.ApiLogInsert(request);
                    }
                    else
                    {

                        if (credentialValidation != null && credentialValidation.CompanyID > 0)
                        {

                            List<EsnRepository> esnList = inventoryReportOperation.GetCustomerEsnRepositoryDownload(credentialValidation.CompanyID, 0, serviceRequest.FromDate, serviceRequest.ToDate, serviceRequest.UnusedOnly, serviceRequest.ShowAllUnusedEsn);


                            //esnInfoList = avii.Classes.BabEsnOperation.ValidateBadESN(serviceRequest.EsnList, credentialValidation.CompanyID, out recordCount, out inactiveESNMsg, out invalidESNMsg, out esnAlreadyExists);
                            if (esnList != null && esnList.Count > 0)
                            {

                                serviceResponse.Comment = "Successfully Retrieved.  Record Count: " + esnList.Count;
                                serviceResponse.ReturnCode = ResponseErrorCode.SuccessfullyRetrieved.ToString();
                                serviceResponse.EsnList = esnList;


                            }
                            else
                            {

                                serviceResponse.Comment = "No records found";
                                serviceResponse.ReturnCode = ResponseErrorCode.NoRecordsFound.ToString();
                                serviceResponse.EsnList = null;
                            }

                        }
                        else
                        {
                            serviceResponse.Comment = "Cannot Authenticate User";
                            serviceResponse.ReturnCode = ResponseErrorCode.CannotAuthenticateUser.ToString();
                            serviceResponse.EsnList = null;
                        }
                        request.UserID = credentialValidation.UserID;

                        request.ResponseData = clsGeneral.SerializeObject(serviceResponse);
                        request.ResponseTimeStamp = DateTime.Now;
                        request.ExceptionOccured = false;
                        request.ReturnMessage = serviceResponse.Comment;
                        logOperations.ApiLogInsert(request);
                    }
                }
                catch (Exception exc)
                {
                    ex = exc;
                    //  serviceResponse.Comment = exc.Message;
                    //serviceResponse.ReturnCode = ResponseErrorCode.ErrowWhileLoadingData.ToString();
                    serviceResponse.Comment = ex.Message;
                    serviceResponse.ReturnCode = ResponseErrorCode.ErrowWhileLoadingData.ToString();
                    serviceResponse.EsnList = null;

                    request.ResponseData = clsGeneral.SerializeObject(serviceResponse);
                    request.ResponseTimeStamp = DateTime.Now;
                    request.ExceptionOccured = true;
                    request.ReturnMessage = ResponseErrorCode.ErrowWhileLoadingData.ToString();
                    logOperations.ApiLogInsert(request);
                }

            }
            return serviceResponse;
        }

        public CurrentStockResponse GetInventoryCurrentStock(CurrentStockRequest serviceRequest)
        {
            SV.Framework.Inventory.InventoryReportOperation inventoryReportOperation = SV.Framework.Inventory.InventoryReportOperation.CreateInstance<SV.Framework.Inventory.InventoryReportOperation>();
            SV.Framework.Authenticate.AuthenticationOperation authenticationOperation = SV.Framework.Authenticate.AuthenticationOperation.CreateInstance<SV.Framework.Authenticate.AuthenticationOperation>();
            SV.Framework.Fulfillment.LogOperations logOperations = SV.Framework.Fulfillment.LogOperations.CreateInstance<SV.Framework.Fulfillment.LogOperations>();
            CurrentStockResponse serviceResponse = new CurrentStockResponse();
            serviceResponse.ReturnCode = ResponseErrorCode.MissingParameter.ToString();
            if (serviceRequest != null)
            {
                string requestXML = clsGeneral.SerializeObject(serviceRequest);

                LogModel request = new LogModel();
                request.RequestData = requestXML;
                request.ModuleName = "GetInventoryStockCurrent";
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
                        //LogOperations.ApiLogInsert(request);
                    }
                    else
                    {
                        if (credentialValidation != null && credentialValidation.CompanyID > 0)
                        {
                            List<CurrentStock> CurrentStocks = inventoryReportOperation.GetCurrentStock(credentialValidation.CompanyID, serviceRequest.SKU, false, false);
                            if (CurrentStocks != null && CurrentStocks.Count > 0)
                            {
                                serviceResponse.Comment = "Successfully Retrieved";
                                serviceResponse.ReturnCode = ResponseErrorCode.SuccessfullyRetrieved.ToString();
                                serviceResponse.CurrentStocks = CurrentStocks;
                            }
                            else
                            {
                                serviceResponse.Comment = "No records found";
                                serviceResponse.ReturnCode = ResponseErrorCode.NoRecordsFound.ToString();
                                serviceResponse.CurrentStocks = default;
                            }
                        }
                        else
                        {
                            serviceResponse.Comment = "Cannot Authenticate User";
                            serviceResponse.ReturnCode = ResponseErrorCode.CannotAuthenticateUser.ToString();
                            serviceResponse.CurrentStocks = default;
                        }
                        request.UserID = credentialValidation.UserID;
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
                    serviceResponse.ReturnCode = ResponseErrorCode.ErrowWhileLoadingData.ToString();
                    serviceResponse.CurrentStocks = null;
                    request.ResponseData = clsGeneral.SerializeObject(serviceResponse);
                    request.ResponseTimeStamp = DateTime.Now;
                    request.ExceptionOccured = true;
                    request.ReturnMessage = ResponseErrorCode.ErrowWhileLoadingData.ToString();
                    //     LogOperations.ApiLogInsert(request);


                }
                finally
                {
                    logOperations.ApiLogInsert(request);
                }
            }
            return serviceResponse;
        }

        
    }
}
