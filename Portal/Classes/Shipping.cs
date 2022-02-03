using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace avii.Classes
{
    public class Shipping
    {
        public string TrackingNumber { get; set; }
        public DateTime ShipDate { get; set; }
        public string ShipViaCode { get; set; }
       
    }
}