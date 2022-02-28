using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.Fulfillment
{

    public class PurchaseOrderTracking
    {
        public string ShipDate { get; set; }
        public string TrackingNumber { get; set; }
        public string AcknowledgmentSent { get; set; }
        public string ShipmentType { get; set; }
        public List<LineItems> LineItems { get; set; }

    }
}
