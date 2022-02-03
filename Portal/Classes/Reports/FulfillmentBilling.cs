using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace avii.Classes
{
    public class FulfillmentBilling
    {
        public string FulfillmentNumber { get; set; }
        public DateTime FulfillmentDate { get; set; }
        public string ShipVia { get; set; }
        public string TrackingNumber { get; set; }
        public DateTime ShipDate { get; set; }
        public string ShipmentType { get; set; }
        public string ESN { get; set; }
        public string ICC_ID { get; set; }
        public string BatchNumber { get; set; }
        public string ShipPackage { get; set; }
        public decimal Weight { get; set; }
        public decimal Price { get; set; }
        public string ContainerID { get; set; }
        public string ContactName { get; set; }
        public string FulfillmentType { get; set; }

    }
    public class FulfillmentBillingCSV
    {
        public string FulfillmentNumber { get; set; }
        public string FulfillmentDate { get; set; }
        public string ShipVia { get; set; }
        public string TrackingNumber { get; set; }
        public string ShipDate { get; set; }
        public string ShipmentType { get; set; }
        public string ESN { get; set; }
        public string ICC_ID { get; set; }
        public string BatchNumber { get; set; }
        public string ShipPackage { get; set; }
        public decimal Weight { get; set; }
        public decimal Price { get; set; }
        public string ContainerID { get; set; }
        public string ContactName { get; set; }
        public string FulfillmentType { get; set; }

    }
}