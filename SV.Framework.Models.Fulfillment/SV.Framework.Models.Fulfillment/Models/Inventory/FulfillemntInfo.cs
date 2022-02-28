using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.Inventory
{
    public class FulfillemntInfo
    {
        public string FulfillemntNumber { get; set; }
        public string FulfillemntDate { get; set; }
        public string ContactName { get; set; }
        public string PoStatus { get; set; }
        public string PoType { get; set; }
        public string ESN { get; set; }
        public List<FulfillmentLineItem> LineItems { get; set; }
    }

    
    

}
