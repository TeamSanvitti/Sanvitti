using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SV.Framework.Models.SOR
{
    public class FulfillmentService
    {
        public ServiceOrderReponse SetServiceOrder(ServiceOrderRequest serviceOrderhRequest)
        {
            throw new NotImplementedException();
        }

        public ServiceOrderSearchResponse GetServiceOrder(ServiceOrderSearchRequest serviceOrderRequest)
        {
            throw new NotImplementedException();
        }
        public SKUStockResponse GetSKUInventory(SKUStockRequest skuStockRequest)
        {
            throw new NotImplementedException();
        }
    }
    #region BASE
    public class BaseResponse
    {
        public string ReturnMessage { get; set; }
        public string ReturnStatus { get; set; }
    }

    public class BaseRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    #endregion

    #region ServiceOrder
    public class ServiceOrderSearchRequest : BaseRequest
    {
        public string SearviceOrderNumber { get; set; }
        public string CustomerOrderNumber { get; set; }
        public string KittedSKU { get; set; }
        public string ESN { get; set; }
    }

    public class ServiceOrderRequest : BaseRequest
    {
        public ServiceOrder ServiceOrderInfo { get; set; }
    }

    public class ServiceOrderReponse : BaseResponse
    {
        public string SearviceOrderNumber { get; set; }
    }


    public class ServiceOrderSearchResponse : BaseResponse
    {
        public ServiceOrder ServiceOrderInfo { get; set; }
    }

    public class ServiceOrder
    {
        public string SearviceOrderNumber { get; set; }
        public string CustomerOrderNumber { get; set; }
        public CustomerHeader Customer { get; set; }
        public DateTime ServiceOrderDate { get; set; }
        public string KittedSKU { get; set; }
        public int QuantityProcessed { get; set; }

        public List<SKUAssignment> SKUAssignment { get; set; }
    }

    public class SKUAssignment
    {
        public SKU SKU { get; set; }
        public List<Provision> ProvisionList { get; set; }
    }

    public class Provision
    {
        public string ESN { get; set; }
        public string MSL { get; set; }
        public string ICC_ID { get; set; }
    }

    public class CustomerHeader
    {
        public string AccountNumber { get; set; }
        public string CompanyName { get; set; }

    }

    #endregion

    #region Stock
    public class SKU
    {
        public CustomerHeader Customer { get; set; }

        public string ProductCode { get; set; }
        public string CustomerProductCode { get; set; }

    }

    //public class SKUStock
    //{
    //    public SKU SKUName { get; set; }

    //    public int StockInHand { get; set; }
    //    public int StockAssigned { get; set; }
    //    public int StockReceived { get; set; }
    //    public int StockShipped { get; set; }
    //    public int BadStock { get; set; }
    //}

    public class SKUStockResponse : BaseResponse
    {
        public List<SKUStock> SKUStock { get; set; }
    }
    public class ServiceResponseNew
    {
        public string httpStatus { get; set; }
    }

    public class SKUStockRequest : BaseRequest
    {
        public string CategoryName { get; set; }
        public string SKU { get; set; }
    }
    #endregion
}