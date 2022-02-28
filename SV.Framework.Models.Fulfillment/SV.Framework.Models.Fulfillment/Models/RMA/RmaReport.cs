using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.RMA
{
    public class RmaReport
    {
        public string RmaNumber { get; set; }
        public string RmaDate { get; set; }
        public string RmaStatus { get; set; }
        public string ReceiveStatus { get; set; }
        public string TriageStatus { get; set; }
        public string FulfillmentNumber { get; set; }
    }

    public class RMAReport
    {
        public string RMANumber { get; set; }
        public DateTime RMADate { get; set; }
        public string SKU { get; set; }
        public string ESN { get; set; }
        public string DefectReason { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
        public string LanglobalNotes { get; set; }
    }
}
