using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace avii.Classes
{
    public class EsnMslDetail
    {
        public string FulfillmentNumber { get; set; }
        public DateTime FulfillmentDate { get; set; }
        public string CustomerName { get; set; }
        public string ESN { get; set; }
        public string MslNumber { get; set; }
        public string AKEY { get; set; }
        public string OTKSL { get; set; }
        public string ICC_ID { get; set; }
        public string LTE_IMSI { get; set; }
        
    }
}