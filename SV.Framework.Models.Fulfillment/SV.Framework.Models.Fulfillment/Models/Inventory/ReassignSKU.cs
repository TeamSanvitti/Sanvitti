using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.Inventory
{

    public class ReassignSKU
    {
        public string ESN { get; set; }
        public string OldSKUCategory { get; set; }
        public string OldSKU { get; set; }
        public string NewSKUCategory { get; set; }
        public string NewSKU { get; set; }
        public DateTime ChangeDate { get; set; }
        public string CustomerName { get; set; }
        public string FulfillmentNumber { get; set; }
        public string RMANumber { get; set; }



    }
}
