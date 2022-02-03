using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace avii.Classes
{
    public class ESNAssignment
    {
        public string CustomerAccountNumber { get; set; }
        public string FulfillmentNumber { get; set; }
        public List<FulfillmentAssignESN> EsnList { get; set; }
        //public List<FulfillmentAssignNonESN> NonEsnList { get; set; }

    }

}