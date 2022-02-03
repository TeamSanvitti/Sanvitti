using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace avii
{
    public class RMARepairEstimate
    {
        public string StoreID { get; set; }
        public string RMANumber { get; set; }
        public string ESN { get; set; }
        public string RepairDescription { get; set; }
        public int RepairEstID { get; set; }
        public int RMADetGUID { get; set; }
        public double RepairEstimate { get; set; }
        public DateTime EstimatedReadyDate { get; set; }
        public string ItemNumber { get; set; }
        public string ItemDescription { get; set; }
        public string Email { get; set; }
        public string ContactName { get; set; }
        public string RMAReason { get; set; }
        
    }
}