using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace avii.Classes
{
    public class RMAEsnDetail
    {
        public string EsnStatus { get; set; }
        public string ProductName { get; set; }
        
        public string FulfillmentNumber { get; set; }
        public string CategoryName { get; set; }
        public string SKU { get; set; }
        public string ESN { get; set; }
        public string BatchNumber { get; set; }
        public string ICCID { get; set; }
        public string RmaNumber { get; set; }
        public string RmaDate { get; set; }
        public string RmaStatus { get; set; }
        //public string EsnStatus { get; set; }
        
        public string TriageDate { get; set; }
        public string TriageStatus { get; set; }
        public string Reason { get; set; }
        public string TrackingNumber { get; set; }

        //public string ShipLabel { get; set; }
       

    }
    public class RMAESNDetail
    {
        public string CategoryName { get; set; }
        public string SKU { get; set; }
        public string ESN { get; set; }
        public string BatchNumber { get; set; }
        public string ICCID { get; set; }
        public string RmaNumber { get; set; }
        public string RmaDate { get; set; }
        public string RmaStatus { get; set; }
        //public string EsnStatus { get; set; }
        public string TriageDate { get; set; }
        public string TriageStatus { get; set; }
        public string Reason { get; set; }
        public string TrackingNumber { get; set; }

        //public string ShipLabel { get; set; }
        public string CustomerName { get; set; }

        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }

    }
}