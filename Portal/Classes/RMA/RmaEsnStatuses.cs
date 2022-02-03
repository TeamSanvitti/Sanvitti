using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace avii.Classes
{
    public class RmaEsnStatuses
    {
        public string SKU { get; set; }
        public string RmaNumber { get; set; }
        public string ESN { get; set; }
        public string RmaStatus { get; set; }
        public string EsnStatus { get; set; }
        public DateTime RmaDate { get; set; }
        public string AVPO { get; set; }

    }
}