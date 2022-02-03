using System;
using System.Collections.Generic;
using System.Text;
using SV.Framework.Models.Service;
using SV.Framework.Models.Customer;
using SV.Framework.Models.Fulfillment;

namespace SV.Framework.Services
{
    public class CustomerServices
    {
        public  CompanyStoreResponse GetCompanyStore(CompanyStoreRequest companyStoreRequest)
        {
            CompanyStoreResponse serviceResponse = new CompanyStoreResponse();
            SV.Framework.Authenticate.AuthenticationOperation authenticationOperation = SV.Framework.Authenticate.AuthenticationOperation.CreateInstance<SV.Framework.Authenticate.AuthenticationOperation>();
            SV.Framework.Customer.CustomerOperation customerOperation = SV.Framework.Customer.CustomerOperation.CreateInstance<SV.Framework.Customer.CustomerOperation>();
            SV.Framework.Fulfillment.LogOperations logOperations = SV.Framework.Fulfillment.LogOperations.CreateInstance<SV.Framework.Fulfillment.LogOperations>();

            serviceResponse.ErrorCode = ResponseErrorCode.MissingParameter.ToString();
            if (companyStoreRequest != null)
            {
                string requestXML = clsGeneral.SerializeObject(companyStoreRequest);

                LogModel request = new LogModel();
                request.RequestData = requestXML;
                request.ModuleName = "GetCompanyStores";
                request.RequestTimeStamp = DateTime.Now;
                Exception ex = default;

                try
                {
                    if (companyStoreRequest != null)
                    {
                        int userID = authenticationOperation.AuthenticateRequest(companyStoreRequest.Authentication, out ex);
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
                        if (userID > 0)
                        {
                            CompanyInfo companyInfo = customerOperation.GetCompanyInfo(userID);
                            serviceResponse.CompanyInformation = companyInfo;
                            serviceResponse.ErrorCode = string.Empty;
                            serviceResponse.Comment = ResponseErrorCode.SuccessfullyRetrieved.ToString();
                        }
                        else
                        {
                            serviceResponse.Comment = "Cannot authenticate user";
                            serviceResponse.ErrorCode = ResponseErrorCode.CannotAuthenticateUser.ToString();
                        }
                        request.UserID = userID;
                        request.ResponseData = clsGeneral.SerializeObject(serviceResponse);
                        request.ResponseTimeStamp = DateTime.Now;
                        request.ExceptionOccured = false;
                        request.ReturnMessage = serviceResponse.Comment;

                    }
                }
                catch (Exception exc)
                {
                    ex = exc;
                    serviceResponse.CompanyInformation = null;
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

        public  UsersResponse GetAssignedUsers(UsersRequest userRequest)
        {
            UsersResponse serviceResponse = new UsersResponse();
            SV.Framework.Authenticate.AuthenticationOperation authenticationOperation = SV.Framework.Authenticate.AuthenticationOperation.CreateInstance<SV.Framework.Authenticate.AuthenticationOperation>();
            SV.Framework.Customer.CustomerOperation customerOperation = SV.Framework.Customer.CustomerOperation.CreateInstance<SV.Framework.Customer.CustomerOperation>();
            SV.Framework.Fulfillment.LogOperations logOperations = SV.Framework.Fulfillment.LogOperations.CreateInstance<SV.Framework.Fulfillment.LogOperations>();

            serviceResponse.ReturnCode = ResponseErrorCode.MissingParameter.ToString();
            if (userRequest != null)
            {
                string requestXML = clsGeneral.SerializeObject(userRequest);

                LogModel request = new LogModel();
                request.RequestData = requestXML;
                request.ModuleName = "GetAssignedUsers";
                request.RequestTimeStamp = DateTime.Now;
                Exception ex = default;

                try
                {

                    if (userRequest != null)
                    {
                        int userId = authenticationOperation.AuthenticateRequest(userRequest.Authentication, out ex);
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
                            serviceResponse = customerOperation.GetAssignedUsers(userId);
                        }
                        else
                        {
                            serviceResponse.Comment = "Cannot authenticate user";
                            serviceResponse.ReturnCode = ResponseErrorCode.CannotAuthenticateUser.ToString();
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
                    serviceResponse.CompanyInfo = default;
                    serviceResponse.Comment = ex.Message;
                    serviceResponse.ReturnCode = ResponseErrorCode.InconsistantData.ToString();
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

    }
}
