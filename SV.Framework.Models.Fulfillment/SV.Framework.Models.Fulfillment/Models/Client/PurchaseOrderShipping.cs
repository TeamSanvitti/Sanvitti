using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.Fulfillment
{
    public class PurchaseOrderShipping
    {

        public string PurchaseOrderNumber { get; set; }
        public string PODate { get; set; }
        public string ShippingMethod { get; set; }

        public List<PurchaseOrderTracking> Trackings { get; set; }
    }
}
