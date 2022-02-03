using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace avii.Classes
{
    public class TrackingDetail
    {
        public DateTime TrackingSentDateTime { get; set; }
        public string TrackingNumber { get; set; }
        public string ReturnValue { get; set; }
        public int ShipByID { get; set; }
        public string ShipByCode { get; set; }
        public int LineNumber { get; set; }
        public DateTime  ShipDate { get; set; }
        public string Comments { get; set; }
        public int EsnCount { get; set; }

        public string ShipPackage { get; set; }
        public decimal ShipWeight { get; set; }
        public decimal ShipPrice { get; set; }
    }
}