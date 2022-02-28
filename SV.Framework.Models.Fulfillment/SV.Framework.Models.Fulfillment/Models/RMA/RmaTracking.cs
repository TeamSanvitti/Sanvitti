using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.RMA
{
    public class RmaTracking
    {
        public int TrackingId { get; set; }
        public string Prepaid { get; set; }
        public string TrackingNumber { get; set; }
        // public string ShipmentType { get; set; }
        public string ShipDate { get; set; }
        public decimal Weight { get; set; }
        public string Package { get; set; }
        public string ShipVia { get; set; }
        public string Comments { get; set; }
        public decimal FinalPostage { get; set; }
        //public bool IsManualTracking { get; set; }

    }

}
