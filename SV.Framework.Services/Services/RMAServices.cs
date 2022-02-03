using System;
using System.Collections.Generic;
using System.Text;
//using SV.Framework.Services.Model;
using SV.Framework.Models.Service;
using SV.Framework.Models.Fulfillment;
using SV.Framework.Models.RMA;
using SV.Framework.Models.Authenticate;

namespace SV.Framework.Services
{
    public class RMAServices
    {
        public RMAAPIResponse CreateNewRMA(RMAAPIRequest rmaRequest, bool API)
        {
            RMAAPIResponse serviceResponse = new RMAAPIResponse();
            SV.Framework.Authenticate.AuthenticationOperation authenticationOperation = SV.Framework.Authenticate.AuthenticationOperation.CreateInstance<SV.Framework.Authenticate.AuthenticationOperation>();
            SV.Framework.RMA.RMAUtility rmaUtility = SV.Framework.RMA.RMAUtility.CreateInstance<SV.Framework.RMA.RMAUtility>();
            SV.Framework.Fulfillment.LogOperations logOperations = SV.Framework.Fulfillment.LogOperations.CreateInstance<SV.Framework.Fulfillment.LogOperations>();

            string esn = string.Empty;
            int userID = 0;
            //bool API = true;
            serviceResponse.ErrorCode = ResponseErrorCode.MissingParameter.ToString();
            if (rmaRequest != null)
            {
                string requestXML = clsGeneral.SerializeObject(rmaRequest);

                LogModel request = new LogModel();
                request.RequestData = requestXML;
                request.ModuleName = "SetRMA";
                request.RequestTimeStamp = DateTime.Now;
                Exception ex = default;

                try
                {
                    if (rmaRequest != null)
                    {

                        userID = 0;
                        int companyID = 0;

                        if (authenticationOperation.AuthenticateRequestNew(rmaRequest.Authentication, out userID, out companyID))
                        {
                            serviceResponse = rmaUtility.Update_RMA_New(rmaRequest.RMA, userID, companyID, API);
                        }
                        else
                        {
                            serviceResponse.Comment = "Cannot authenticate user";
                            serviceResponse.ErrorCode = ResponseErrorCode.CannotAuthenticateUser.ToString();
                            //RMAResponse.RMANumber = RMARequest.RMA.RmaNumber;
                        }
                        request.UserID = userID;
                        request.ResponseData = clsGeneral.SerializeObject(serviceResponse);
                        request.ResponseTimeStamp = DateTime.Now;
                        request.ExceptionOccured = false;
                        request.ReturnMessage = string.IsNullOrWhiteSpace(serviceResponse.Comment) ? serviceResponse.ErrorCode : serviceResponse.Comment;

                    }
                }
                catch (Exception exc)
                {
                    ex = exc;
                    //serviceResponse.RMACount = 0;
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

        public CancelRMAResponse CancelRMA(RMARequests rmaRequest)
        {
            CancelRMAResponse serviceResponse = new CancelRMAResponse();
            SV.Framework.Authenticate.AuthenticationOperation authenticationOperation = SV.Framework.Authenticate.AuthenticationOperation.CreateInstance<SV.Framework.Authenticate.AuthenticationOperation>();
            SV.Framework.RMA.RMAUtility rmaUtility = SV.Framework.RMA.RMAUtility.CreateInstance<SV.Framework.RMA.RMAUtility>();
            SV.Framework.Fulfillment.LogOperations logOperations = SV.Framework.Fulfillment.LogOperations.CreateInstance<SV.Framework.Fulfillment.LogOperations>();

            serviceResponse.ErrorCode = ResponseErrorCode.MissingParameter.ToString();
            if (rmaRequest != null)
            {
                string requestXML = clsGeneral.SerializeObject(rmaRequest);

                LogModel request = new LogModel();
                request.RequestData = requestXML;
                request.ModuleName = "CancelRMA";
                request.RequestTimeStamp = DateTime.Now;
                Exception ex = default;

                try
                {
                    if (rmaRequest != null)
                    {
                        int userId = authenticationOperation.AuthenticateRequest(rmaRequest.Authentication, out ex);
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
                            serviceResponse = rmaUtility.CancelRMA(rmaRequest.RMANumber, userId);
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

        public RMAResponse GetRMAList(RMASearchCriteria rmaRequest)
        {
            RMAResponse serviceResponse = new RMAResponse();
            SV.Framework.Authenticate.AuthenticationOperation authenticationOperation = SV.Framework.Authenticate.AuthenticationOperation.CreateInstance<SV.Framework.Authenticate.AuthenticationOperation>();
            SV.Framework.RMA.RMAUtility rmaUtility = SV.Framework.RMA.RMAUtility.CreateInstance<SV.Framework.RMA.RMAUtility>();
            SV.Framework.Fulfillment.LogOperations logOperations = SV.Framework.Fulfillment.LogOperations.CreateInstance<SV.Framework.Fulfillment.LogOperations>();

            serviceResponse.ErrorCode = ResponseErrorCode.MissingParameter.ToString();
            if (rmaRequest != null)
            {
                string requestXML = clsGeneral.SerializeObject(rmaRequest);

                LogModel request = new LogModel();
                request.RequestData = requestXML;
                request.ModuleName = "GetRMA";
                request.RequestTimeStamp = DateTime.Now;
                Exception ex = default;

                try
                {
                    if (rmaRequest != null)
                    {
                        int userId = 0;
                        int companyID = 0;
                        if (authenticationOperation.AuthenticateRequestNew(rmaRequest.Authentication, out userId, out companyID))
                        {
                            List<RMAReport> rmaList = rmaUtility.GetRMAReport(rmaRequest.RMANumber, rmaRequest.RMA_From_Date.ToShortDateString(), rmaRequest.RMA_To_Date.ToShortDateString(), (int)rmaRequest.RMAStatus, companyID, 0, rmaRequest.ESN, userId);

                            if (rmaList != null && rmaList.Count > 0)
                            {
                                serviceResponse.RmaReportList = rmaList;
                                serviceResponse.ErrorCode = ResponseErrorCode.SuccessfullyRetrieved.ToString();
                            }
                            else
                                serviceResponse.ErrorCode = ResponseErrorCode.NoRecordsFound.ToString();
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
                        request.ReturnMessage = serviceResponse.ErrorCode;
                    }
                }
                catch (Exception exc)
                {
                    ex = exc;
                    serviceResponse.RmaReportList = null;
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

        public RmaEsnListingResponse GetRmaEsnListing(RmaEsnListingRequest serviceRequest)
        {
            RmaEsnListingResponse serviceResponse = new RmaEsnListingResponse();

            SV.Framework.Authenticate.AuthenticationOperation authenticationOperation = SV.Framework.Authenticate.AuthenticationOperation.CreateInstance<SV.Framework.Authenticate.AuthenticationOperation>();
            SV.Framework.RMA.RMAUtility rmaUtility = SV.Framework.RMA.RMAUtility.CreateInstance<SV.Framework.RMA.RMAUtility>();
            SV.Framework.Fulfillment.LogOperations logOperations = SV.Framework.Fulfillment.LogOperations.CreateInstance<SV.Framework.Fulfillment.LogOperations>();
            serviceResponse.ReturnCode = ResponseErrorCode.MissingParameter.ToString();
            if (serviceRequest != null)
            {
                string requestXML = clsGeneral.SerializeObject(serviceRequest);

                LogModel request = new LogModel();
                request.RequestData = requestXML;
                request.ModuleName = "GetRmaEsnListing";
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
                            List<RmaEsnStatuses> rmaStatusList = rmaUtility.GetCustomerRmaEsnStatusReport(credentialValidation.CompanyID, serviceRequest.FromDate, serviceRequest.ToDate, Convert.ToInt32(serviceRequest.EsnStatus), Convert.ToInt32(serviceRequest.RmaStatus));

                            //esnInfoList = avii.Classes.BabEsnOperation.ValidateBadESN(serviceRequest.EsnList, credentialValidation.CompanyID, out recordCount, out inactiveESNMsg, out invalidESNMsg, out esnAlreadyExists);
                            if (rmaStatusList != null && rmaStatusList.Count > 0)
                            {
                                serviceResponse.Comment = "Successfully Retrieved.  Record Count: " + rmaStatusList.Count;
                                serviceResponse.ReturnCode = ResponseErrorCode.SuccessfullyRetrieved.ToString();
                                serviceResponse.RmaEsnListing = rmaStatusList;
                            }
                            else
                            {
                                serviceResponse.Comment = "No records found";
                                serviceResponse.ReturnCode = ResponseErrorCode.NoRecordsFound.ToString();
                                serviceResponse.RmaEsnListing = null;
                            }
                        }
                        else
                        {
                            serviceResponse.Comment = "Cannot Authenticate User";
                            serviceResponse.ReturnCode = ResponseErrorCode.CannotAuthenticateUser.ToString();
                            serviceResponse.RmaEsnListing = null;
                        }
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
                    serviceResponse.RmaEsnListing = null;
                    request.ResponseData = clsGeneral.SerializeObject(serviceResponse);
                    request.ResponseTimeStamp = DateTime.Now;
                    request.ExceptionOccured = true;
                    request.ReturnMessage = ResponseErrorCode.ErrowWhileLoadingData.ToString();
                    logOperations.ApiLogInsert(request);
                }
            }
            return serviceResponse;
        }

        public RmaEsnDetailResponse GetRmaEsnDetail(RmaEsnListingRequest serviceRequest)
        {
            RmaEsnDetailResponse serviceResponse = new RmaEsnDetailResponse();

            SV.Framework.Authenticate.AuthenticationOperation authenticationOperation = SV.Framework.Authenticate.AuthenticationOperation.CreateInstance<SV.Framework.Authenticate.AuthenticationOperation>();
            SV.Framework.RMA.RMAUtility rmaUtility = SV.Framework.RMA.RMAUtility.CreateInstance<SV.Framework.RMA.RMAUtility>();
            SV.Framework.Fulfillment.LogOperations logOperations = SV.Framework.Fulfillment.LogOperations.CreateInstance<SV.Framework.Fulfillment.LogOperations>();
            serviceResponse.ReturnCode = ResponseErrorCode.MissingParameter.ToString();
            if (serviceRequest != null)
            {
                string requestXML = clsGeneral.SerializeObject(serviceRequest);

                LogModel request = new LogModel();
                request.RequestData = requestXML;
                request.ModuleName = "GetRmaEsnDetail";
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
                            List<RMAESNDetail> rmaDetailList = rmaUtility.GetRmaEsnDetailReport(credentialValidation.CompanyID, "", "", serviceRequest.FromDate, serviceRequest.ToDate, Convert.ToInt32(serviceRequest.EsnStatus), Convert.ToInt32(serviceRequest.RmaStatus));

                            if (rmaDetailList != null && rmaDetailList.Count > 0)
                            {
                                serviceResponse.Comment = "Successfully Retrieved.  Record Count: " + rmaDetailList.Count;
                                serviceResponse.ReturnCode = ResponseErrorCode.SuccessfullyRetrieved.ToString();
                                serviceResponse.RmaEsnDetail = rmaDetailList;
                            }
                            else
                            {
                                serviceResponse.Comment = "No records found";
                                serviceResponse.ReturnCode = ResponseErrorCode.NoRecordsFound.ToString();
                                serviceResponse.RmaEsnDetail = default;
                            }
                        }
                        else
                        {
                            serviceResponse.Comment = "Cannot Authenticate User";
                            serviceResponse.ReturnCode = ResponseErrorCode.CannotAuthenticateUser.ToString();
                            serviceResponse.RmaEsnDetail = default;
                        }
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
                    serviceResponse.RmaEsnDetail = null;
                    request.ResponseData = clsGeneral.SerializeObject(serviceResponse);
                    request.ResponseTimeStamp = DateTime.Now;
                    request.ExceptionOccured = true;
                    request.ReturnMessage = ResponseErrorCode.ErrowWhileLoadingData.ToString();
                    logOperations.ApiLogInsert(request);
                }
            }
            return serviceResponse;
        }

    }
}
