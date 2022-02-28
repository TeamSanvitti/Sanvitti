using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.Fulfillment
{

    public class FulfillmentShippingLineItem
    {
        public int PODID { get; set; }
        public int Quantity { get; set; }
        public string SKU { get; set; }
    }
}
