using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace avii.Classes
{
    public class FulfillmentLabel
    {
        public decimal ShippingWeight { get; set; }
        public int POID { get; set; }
        public string FulfillmentNumber { get; set; }
        public string FulfillmentDate { get; set; }
        public string ShipDate { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public string StoreID { get; set; }
        public string StreetAddress1 { get; set; }
        public string StreetAddress2 { get; set; }
        public string ShipToCity { get; set; }
        public string ShipToState { get; set; }
        public string ShipToZip { get; set; }
        public string Status { get; set; }
        public string ShipMethod { get; set; }
        public string ShippingLabelImage { get; set; }
        public string TrackingNumber { get; set; }

        public string ShipPackage { get; set; }
        public string CompanyName { get; set; }
    }
}