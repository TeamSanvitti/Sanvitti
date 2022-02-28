using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.Fulfillment
{

    public class StoreFulfillmentStatus
    {
        public int Total { get; set; }
        public string StoreID { get; set; }
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

    }
}
