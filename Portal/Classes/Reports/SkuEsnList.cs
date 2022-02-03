using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace avii.Classes
{
    public class SkuEsnList
    {
        public string ProductCode { get; set; }
        public string ItemName { get; set; }
        public string CarrierName { get; set; }
        public string CategoryName { get; set; }
        public string MakerName { get; set; }
        
        public int UsedESN {get; set; }
        public int UnusedESN { get; set; }
        public int TotalESN { get; set; }
        public int RmaESN { get; set; }
        public int ItemGUID { get; set; }
    }
}