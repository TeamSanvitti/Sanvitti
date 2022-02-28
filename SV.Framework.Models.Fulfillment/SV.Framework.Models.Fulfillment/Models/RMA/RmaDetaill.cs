using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.RMA
{
    public class RmaDetail
    {
        public string RmaNumber { get; set; }
        public string RmaDate { get; set; }
        public string RmaStatus { get; set; }
        public string ReceiveStatus { get; set; }
        public string TriageStatus { get; set; }
        public string FulfillmentNumber { get; set; }
    }
}
