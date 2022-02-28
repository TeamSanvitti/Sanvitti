using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.Fulfillment
{
    public class ShipmentInfo
    {
        public string ShipMethod { get; set; }
        public string ShipPackage { get; set; }
        public string TrackingNumber { get; set; }
        public string LabelGenerationDate { get; set; }
        public decimal FinalPostage { get; set; }
        public decimal ShipWeight { get; set; }
        public string LabelUsedDate { get; set; }
        public string LabelType { get; set; }
        public string AssignmentNumber { get; set; }
        public int ID { get; set; }
        public bool IsPrint { get; set; }

    }
}
