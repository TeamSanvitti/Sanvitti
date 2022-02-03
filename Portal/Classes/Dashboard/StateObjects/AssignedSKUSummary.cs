using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace avii.Classes
{
    public class AssignedSKUSummary
    {
        public string SKU { get; set; }
        public int Pending { get; set; }
        public int InProcess { get; set; }
        public int Processed { get; set; }
        public int Shipped { get; set; }
        public int Closed { get; set; }
        public int Return { get; set; }
        public int OnHold { get; set; }
        public int OutofStock { get; set; }
        public int Cancel { get; set; }
        public int PartialProcessed { get; set; }
        public int PartialShipped { get; set; }
        public int UnusedESN { get; set; }
        public int IsAdmin { get; set; }



    }
}