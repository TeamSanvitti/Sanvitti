using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using SV.Framework.Models.SOR;
using SV.Framework.Models.Common;

namespace SV.Framework.SOR
{
    public class ServiceRequestOperations : BaseCreateInstance
    {
        public int ServiceOrderRequestInsertUpdate(ServiceOrderRequestModel request, out string errorMessage)
        {
            SV.Framework.DAL.SOR.ServiceRequestOperations serviceRequestOperations = SV.Framework.DAL.SOR.ServiceRequestOperations.CreateInstance<SV.Framework.DAL.SOR.ServiceRequestOperations>();
            errorMessage = string.Empty;

            int returnValue = serviceRequestOperations.ServiceOrderRequestInsertUpdate(request, out errorMessage);
            return returnValue;

        }
        public ServiceRequestDetail ServiceRequestNumberSearch(int companyID, string serviceRequestNumber)
        {
            SV.Framework.DAL.SOR.ServiceRequestOperations serviceRequestOperations = SV.Framework.DAL.SOR.ServiceRequestOperations.CreateInstance<SV.Framework.DAL.SOR.ServiceRequestOperations>();

            ServiceRequestDetail serviceRequestDetail = serviceRequestOperations.ServiceRequestNumberSearch(companyID, serviceRequestNumber);// new ServiceRequestDetail();
            return serviceRequestDetail;
        }
        
        public int ServiceOrderRequestDelete(int serviceRequestID, int userID)
        {
            SV.Framework.DAL.SOR.ServiceRequestOperations serviceRequestOperations = SV.Framework.DAL.SOR.ServiceRequestOperations.CreateInstance<SV.Framework.DAL.SOR.ServiceRequestOperations>();

            int returnValue = serviceRequestOperations.ServiceOrderRequestDelete(serviceRequestID, userID);
            return returnValue;

        }

        public  List<ServiceOrderRequestModel> GetServiceOrderRequests(ServiceOrderRequestModel serviceOrder)
        {
            SV.Framework.DAL.SOR.ServiceRequestOperations serviceRequestOperations = SV.Framework.DAL.SOR.ServiceRequestOperations.CreateInstance<SV.Framework.DAL.SOR.ServiceRequestOperations>();

            List<ServiceOrderRequestModel> sorList = serviceRequestOperations.GetServiceOrderRequests(serviceOrder);
            return sorList;

        }
        public List<ServiceOrderRequestModel> GetServiceOrderRequestLog(int serviceRequestID)
        {
            SV.Framework.DAL.SOR.ServiceRequestOperations serviceRequestOperations = SV.Framework.DAL.SOR.ServiceRequestOperations.CreateInstance<SV.Framework.DAL.SOR.ServiceRequestOperations>();

            List<ServiceOrderRequestModel> sorList = serviceRequestOperations.GetServiceOrderRequestLog(serviceRequestID);
            return sorList;

        }

        public ServiceOrderRequestModel GetServiceOrderRequest(int ServiceRequestID)
        {
            SV.Framework.DAL.SOR.ServiceRequestOperations serviceRequestOperations = SV.Framework.DAL.SOR.ServiceRequestOperations.CreateInstance<SV.Framework.DAL.SOR.ServiceRequestOperations>();

            ServiceOrderRequestModel sorInfo = serviceRequestOperations.GetServiceOrderRequest(ServiceRequestID);
            return sorInfo;

        }
        public List<ServiceRequestStatus> GetSoRStatusList()
        {
            SV.Framework.DAL.SOR.ServiceRequestOperations serviceRequestOperations = SV.Framework.DAL.SOR.ServiceRequestOperations.CreateInstance<SV.Framework.DAL.SOR.ServiceRequestOperations>();
            List<ServiceRequestStatus> sorStatusList = serviceRequestOperations.GetSoRStatusList();// new List<ServiceRequestStatus>();
            
            return sorStatusList;
        }

        public List<ServiceOrderRequestModel> GetServiceOrderRequestWidget(int companyID, string SKU, string status)
        {
            SV.Framework.DAL.SOR.ServiceRequestOperations serviceRequestOperations = SV.Framework.DAL.SOR.ServiceRequestOperations.CreateInstance<SV.Framework.DAL.SOR.ServiceRequestOperations>();

            List<ServiceOrderRequestModel> sorList = serviceRequestOperations.GetServiceOrderRequestWidget(companyID, SKU, status);
            return sorList;
        }
        
        
    }
}

