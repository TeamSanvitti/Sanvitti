using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.Fulfillment
{
    public class ReturnShipment
    {
        public string TrackingNumber { get; set; }
        public DateTime ReturnDate { get; set; }
        public string ShipViaCode { get; set; }
    }
}
