using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.Fulfillment
{
    public class PurchaseOrderShipment
    {
        [JsonIgnore]
        public int UserID { get; set; }
        [JsonIgnore]
        public string API_URL { get; set; }

        [JsonIgnore]
        public int PO_ID { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public string ShippingDate { get; set; }
        public string ShippingMethod { get; set; }
        public string ShippingTrackingNumber { get; set; }
        public List<LineItem> LineItems { get; set; }
    }
    public class LineItem
    {
        public int LineNo { get; set; }
        public string ItemCode { get; set; }
        public int Quantity { get; set; }
        //[JsonIgnore]
        public string ESN { get; set; }
        public string ICCID { get; set; }
        [JsonIgnore]
        public string BatchNumber { get; set; }

        public string ContainerID { get; set; }



    }
    public class ReturnAPI
    {
        public string error { get; set; }
        public string httpStatus { get; set; }

    }

    public class FulfillmentsTracking
    {
        public PurchaseOrderShipment Shipment { get; set; }

    }

}
