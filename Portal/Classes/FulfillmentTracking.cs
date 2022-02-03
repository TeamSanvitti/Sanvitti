using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace avii.Classes
{
    public class FulfillmentTracking
    {
        public string FulfillmentNumber { get; set; }
        public DateTime FulfillmentDate { get; set; }
        public string TrackingNumber { get; set; }
        public string ShipmentType { get; set; }
        [XmlIgnore]
        public int POID { get; set; }
        public string ShipByCode { get; set; }
        public DateTime ShipDate { get; set; }
        public int EsnCount { get; set; }

    }
}