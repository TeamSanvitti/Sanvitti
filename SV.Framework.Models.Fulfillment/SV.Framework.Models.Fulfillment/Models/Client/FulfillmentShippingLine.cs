using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.Fulfillment
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
}
