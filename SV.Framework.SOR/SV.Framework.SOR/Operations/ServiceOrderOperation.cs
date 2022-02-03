using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using SV.Framework.Models.SOR;
using SV.Framework.Models.Common;

namespace SV.Framework.SOR
{
    public class ServiceOrderOperation : BaseCreateInstance
    {
        public List<ServiceOrderDetail> ValidateServiceOrderEsnRange(int qty, string ESNs, string SKUs, out string errorMessage, out int IsValidQty)
        {
            SV.Framework.DAL.SOR.ServiceOrderOperation serviceOrderOperation = SV.Framework.DAL.SOR.ServiceOrderOperation.CreateInstance<SV.Framework.DAL.SOR.ServiceOrderOperation>();

            IsValidQty = 0;
            errorMessage = string.Empty;
            List<ServiceOrderDetail> esnList = serviceOrderOperation.ValidateServiceOrderEsnRange(qty, ESNs, SKUs, out errorMessage, out IsValidQty);
            
            return esnList;

        }
        public List<ServiceOrderDetail> ValidateServiceOrderEsnRange(List<SOQtyValidate> sOQtyValidates, out string errorMessage, out int IsValidQty)
        {
            SV.Framework.DAL.SOR.ServiceOrderOperation serviceOrderOperation = SV.Framework.DAL.SOR.ServiceOrderOperation.CreateInstance<SV.Framework.DAL.SOR.ServiceOrderOperation>();

            IsValidQty = 0;
            errorMessage = "";
            List<ServiceOrderDetail> esnList = serviceOrderOperation.ValidateServiceOrderEsnRange(sOQtyValidates, out errorMessage, out IsValidQty);
            return esnList;

        }

        public  List<ServiceOrderDetail> ValidateServiceOrder(ServiceOrders serviceOrder, out string errorMessage, out bool IsValidate)
        {
            SV.Framework.DAL.SOR.ServiceOrderOperation serviceOrderOperation = SV.Framework.DAL.SOR.ServiceOrderOperation.CreateInstance<SV.Framework.DAL.SOR.ServiceOrderOperation>();
            errorMessage = string.Empty;
            IsValidate = true;
            List<ServiceOrderDetail> esnList = serviceOrderOperation.ValidateServiceOrder(serviceOrder, out errorMessage, out IsValidate);
            
            
            return esnList;

        }

        public List<ServiceOrderDetail> Validate_ServiceOrder(ServiceOrders serviceOrder, out string errorMessage, out bool IsValidate)
        {
            SV.Framework.DAL.SOR.ServiceOrderOperation serviceOrderOperation = SV.Framework.DAL.SOR.ServiceOrderOperation.CreateInstance<SV.Framework.DAL.SOR.ServiceOrderOperation>();

            IsValidate = true;
            errorMessage = "";
            List<ServiceOrderDetail> esnList = serviceOrderOperation.Validate_ServiceOrder(serviceOrder, out errorMessage, out IsValidate);
            errorMessage = string.Empty;
            return esnList;

        }

        public int ServiceOrderInsertUpdate(ServiceOrders serviceOrder, int userId, out string errorMessage)
        {
            SV.Framework.DAL.SOR.ServiceOrderOperation serviceOrderOperation = SV.Framework.DAL.SOR.ServiceOrderOperation.CreateInstance<SV.Framework.DAL.SOR.ServiceOrderOperation>();

            int returnValue = 0;
            errorMessage = string.Empty;
            returnValue = serviceOrderOperation.ServiceOrderInsertUpdate(serviceOrder, userId, out errorMessage);
            return returnValue;

        }
        public  List<ServiceOrderDetail> Validate_ServiceOrder_New2(ServiceOrders serviceOrder, out string errorMessage, out bool IsValidate)
        {
            SV.Framework.DAL.SOR.ServiceOrderOperation serviceOrderOperation = SV.Framework.DAL.SOR.ServiceOrderOperation.CreateInstance<SV.Framework.DAL.SOR.ServiceOrderOperation>();

            IsValidate = true;
            List<ServiceOrderDetail> esnList = default;
            errorMessage = string.Empty;
            esnList = serviceOrderOperation.Validate_ServiceOrder_New2(serviceOrder, out errorMessage, out IsValidate);
            return esnList;

        }

        
        public  List<ServiceOrderDetail> Validate_ServiceOrder_New3(ServiceOrders serviceOrder, out string errorMessage, out bool IsValidate)
        {
            SV.Framework.DAL.SOR.ServiceOrderOperation serviceOrderOperation = SV.Framework.DAL.SOR.ServiceOrderOperation.CreateInstance<SV.Framework.DAL.SOR.ServiceOrderOperation>();

            IsValidate = true;
            List<ServiceOrderDetail> esnList = default;
            errorMessage = string.Empty;
            esnList = serviceOrderOperation.Validate_ServiceOrder_New3(serviceOrder, out errorMessage, out IsValidate);
            return esnList;

        }
        public  List<ServiceOrderDetail> Validate_ServiceOrder_New(ServiceOrders serviceOrder, out string errorMessage, out bool IsValidate)
        {
            SV.Framework.DAL.SOR.ServiceOrderOperation serviceOrderOperation = SV.Framework.DAL.SOR.ServiceOrderOperation.CreateInstance<SV.Framework.DAL.SOR.ServiceOrderOperation>();

            IsValidate = true;
            List<ServiceOrderDetail> esnList = default;
            errorMessage = string.Empty;
            esnList = serviceOrderOperation.Validate_ServiceOrder_New(serviceOrder, out errorMessage, out IsValidate);

            return esnList;

        }
        public  int ServiceOrder_InsertUpdate_New(ServiceOrders serviceOrder, int userId, out string errorMessage)
        {
            SV.Framework.DAL.SOR.ServiceOrderOperation serviceOrderOperation = SV.Framework.DAL.SOR.ServiceOrderOperation.CreateInstance<SV.Framework.DAL.SOR.ServiceOrderOperation>();

            int returnValue = 0;
            errorMessage = string.Empty;
            returnValue = serviceOrderOperation.ServiceOrder_InsertUpdate_New(serviceOrder, userId, out errorMessage);
            return returnValue;

        }
        public int ServiceOrder_NonESN_InsertUpdate(ServiceOrders serviceOrder, int userId, out string errorMessage)
        {
            SV.Framework.DAL.SOR.ServiceOrderOperation serviceOrderOperation = SV.Framework.DAL.SOR.ServiceOrderOperation.CreateInstance<SV.Framework.DAL.SOR.ServiceOrderOperation>();

            int returnValue = 0;
            errorMessage = string.Empty;
            returnValue = serviceOrderOperation.ServiceOrder_NonESN_InsertUpdate(serviceOrder, userId, out errorMessage);
            return returnValue;

        }

        public  int GenerateServiceOrder()
        {
            SV.Framework.DAL.SOR.ServiceOrderOperation serviceOrderOperation = SV.Framework.DAL.SOR.ServiceOrderOperation.CreateInstance<SV.Framework.DAL.SOR.ServiceOrderOperation>();

            int returnValue = serviceOrderOperation.GenerateServiceOrder();

            return returnValue;
        }
        public  List<ServiceOrders> GetServiceOrders(ServiceOrders serviceOrder)
        {
            SV.Framework.DAL.SOR.ServiceOrderOperation serviceOrderOperation = SV.Framework.DAL.SOR.ServiceOrderOperation.CreateInstance<SV.Framework.DAL.SOR.ServiceOrderOperation>();

            List<ServiceOrders> serviceOrders = serviceOrderOperation.GetServiceOrders(serviceOrder);// new List<ServiceOrders>();
            
            return serviceOrders;

        }
        public  List<SOSKUSummary> GetServiceOrderSummary(ServiceOrders serviceOrder)
        {
            SV.Framework.DAL.SOR.ServiceOrderOperation serviceOrderOperation = SV.Framework.DAL.SOR.ServiceOrderOperation.CreateInstance<SV.Framework.DAL.SOR.ServiceOrderOperation>();

            List<SOSKUSummary> serviceOrders = serviceOrderOperation.GetServiceOrderSummary(serviceOrder);// new List<SOSKUSummary>();
            

            return serviceOrders;

        }
        public  ServiceOrders GetServiceOrderDetail(int serviceOrderId)
        {
            SV.Framework.DAL.SOR.ServiceOrderOperation serviceOrderOperation = SV.Framework.DAL.SOR.ServiceOrderOperation.CreateInstance<SV.Framework.DAL.SOR.ServiceOrderOperation>();

            ServiceOrders serviceOrders = serviceOrderOperation.GetServiceOrderDetail(serviceOrderId);// new ServiceOrders();
            
            return serviceOrders;

        }

    }
}