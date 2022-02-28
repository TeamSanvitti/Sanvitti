using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.Fulfillment
{
    public class UnprovisioningRequest
    {
        public string FulfillmentNumber { get; set; }
        public string TrackingNumber { get; set; }
        public string CustomerComment { get; set; }
        public string AdminComment { get; set; }
        public int CreatedBy { get; set; }
        public int RequestedBy { get; set; }
        public int ApprovedBy { get; set; }

    }

}
