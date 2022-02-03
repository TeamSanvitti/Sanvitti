using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace avii.Classes
{
    public class RMATrackning
    {
        public byte[] ShippingLabel { get; set; }
        public string ShippingLabelImage { get; set; }
        public string RMANumber { get; set; }
        public string TrackingNumber { get; set; }
        public string ShipmentType { get; set; }
        public string ShipDate { get; set; }
        public double Weight { get; set; }
        public string Package { get; set; }
        public string ShipVia { get; set; }
        public string Comments { get; set; }
        public string Prepaid { get; set; }
        public decimal FinalPostage { get; set; }
        public bool IsManualTracking { get; set; }



    }
}