using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.Fulfillment
{
    public class UnprovisioingInfo
    {
        public int POID { get; set; }
        public int UnprovisioningID { get; set; }
        public int TotalQty { get; set; }
        public string FulfillmentNumber { get; set; }
        public string RequestedBy { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime CreateDate { get; set; }
        public string CustomerComment { get; set; }
        public string AdminComment { get; set; }
        public string Status { get; set; }
        public bool IsVisible { get; set; }
    }
}
