using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using SV.Framework.Services;
using SV.Framework.Models.Service;
using SV.Framework.Models.Fulfillment;
using SV.Framework.Models.Common;

namespace SanvittiServices
{
    [SoapDocumentService(RoutingStyle = SoapServiceRoutingStyle.RequestElement)]
    [WebService(Namespace = "http://sanvitti.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]


   
    public class FulfillmentService : System.Web.Services.WebService
    {

        [WebMethod()]
        public PurchaseOrderResponse addPurchaseOrder(PurchaseOrderRequest purchaseOrderRequest)
        {
            FulfillmentServices fulfillmentServices = new FulfillmentServices();
            return fulfillmentServices.addPurchaseOrder(purchaseOrderRequest);
        }

        [WebMethod]
        public PurchaseOrderInfoResponse GetPurchaseOrder(PurchaseOrderInfoRequest purchaseOrderRequest)
        {
            FulfillmentServices fulfillmentServices = new FulfillmentServices();
            return fulfillmentServices.GetPurchaseOrder(purchaseOrderRequest);
        }
        [WebMethod]
        public CancelPurchaseOrderResponse CancelFulfillment(PurchaseOrderCancelRequest fulfillmentRequest)
        {
            FulfillmentServices fulfillmentServices = new FulfillmentServices();
            return fulfillmentServices.CancelFulfillment(fulfillmentRequest);
        }

        [WebMethod]
        public FulfillmentResponse UpdatePurchaseOrder(FulfillmentRequest serviceRequest)
        {
            FulfillmentServices fulfillmentServices = new FulfillmentServices();
            return fulfillmentServices.FulfillmentOrderUpdate(serviceRequest);
        }

        [WebMethod]
        public PurchaseOrderShipmentAPIResponse GetPurchaseOrderShipment(PurchaseOrderShipmentRequest serviceRequest)
        {
            FulfillmentServices fulfillmentServices = new FulfillmentServices();

            return fulfillmentServices.GetPurchaseOrderShipmentAPI(serviceRequest);
        }
        [WebMethod]
        public PurchaseOrderShippingResponse GetPurchaseOrderShipmentToBeSent(PurchaseOrderShippingRequest serviceRequest)
        {
            FulfillmentServices fulfillmentServices = new FulfillmentServices();

            return fulfillmentServices.GetPurchaseOrderShipmentToBeSent(serviceRequest);
        }
        //[WebMethod]
        //public SetPurchaseOrderShippingResponse SetPurchaseOrderShipment(SetPurchaseOrderShippingRequest serviceRequest)
        //{
        //    FulfillmentServices fulfillmentServices = new FulfillmentServices();
        //    return fulfillmentServices.SetPurchaseOrderShipment(serviceRequest);
        //}

        [WebMethod]
        public ShippByResponse GetShippingCodes()
        {
            FulfillmentServices fulfillmentServices = new FulfillmentServices();
            return fulfillmentServices.GetShippingCodes();
        }

        //[WebMethod]
        //public FulfillmentReportResponse GetPurchaseOrderProvisioning(FulfillmentReportRequest poRequest)
        //{
        //    return FulfillmentServices.FulfillmentOrdersProvisionalList(poRequest);
        //}

    }
}


