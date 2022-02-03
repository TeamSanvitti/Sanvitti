using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace avii.Classes
{
    public class RMAReport
    {
        public string RMANumber { get; set; }
        public DateTime RMADate { get; set; }
        public string SKU { get; set; }
        public string ESN { get; set; }
        public string DefectReason { get; set; }
        public string Status { get; set; }
        public string Notes { get; set; }
        public string WSANotes { get; set; }
    }
}