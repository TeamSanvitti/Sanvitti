using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace avii.Classes
{
    public class SimInfo
    {
        public int SimID { get; set; }
        public int RmaGUID { get; set; }
        public string SIM { get; set; }
        public string ESN { get; set; }
        public string SKU { get; set; }
        public DateTime UploadDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string CompanyName { get; set; }
        public string FulfillmentNumber { get; set; }
        public string RmaNumber { get; set; }


    }
}