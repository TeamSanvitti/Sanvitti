using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace avii.Classes
{
    public class ReturnShipment
    {
        public string TrackingNumber { get; set; }
        public DateTime ReturnDate { get; set; }
        public string ShipViaCode { get; set; }
    }
}