using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.RMA
{
    public class RMADetail
    {
        public string Warranty { get; set; }
        public string Reason { get; set; }
        public string Notes { get; set; }
        public string ESN { get; set; }
        public string Disposition { get; set; }
        public string SKU { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public string Status { get; set; }
        public string TriageStatus { get; set; }
        public string TriageNotes { get; set; }
        public int Quantity { get; set; }
        public int RmaDetGUID { get; set; }

    }

}
