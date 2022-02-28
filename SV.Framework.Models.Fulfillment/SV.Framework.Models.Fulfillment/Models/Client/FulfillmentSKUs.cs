using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.Fulfillment
{
    public class FulfillmentSKUs
    {
        public int POD_ID { get; set; }
        public string SKU { get; set; }
        public int Quantity { get; set; }
        public int AssignedQty { get; set; }
        public bool IsDelete { get; set; }
    }
}
