using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace avii.Classes
{
    public class FulfillmentShippingLine
    {
        public int POID { get; set; }
        public string ShipDate { get; set; }
        public int ShipById { get; set; }
        public string TrackingNumber { get; set; }
        public string Comments { get; set; }
        public List<FulfillmentShippingLineItem> LineItems { get; set; }


    }
    public class FulfillmentShippingLineItem
    {
        public int PODID { get; set; }
        public int Quantity { get; set; }
        public string SKU { get; set; }
    }

}