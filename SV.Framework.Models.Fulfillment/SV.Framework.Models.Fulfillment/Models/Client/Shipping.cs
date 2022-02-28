using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.Fulfillment
{
    public class Shipping
    {
        public string TrackingNumber { get; set; }
        public DateTime ShipDate { get; set; }
        public string ShipViaCode { get; set; }

    }
}
