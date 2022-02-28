using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.Inventory
{
    public class FulfillmentLineItem
    {
        public string SKU { get; set; }
        public int Quantity { get; set; }
        public string CategoryName { get; set; }
        public string ProductName { get; set; }
        //public string ESN { get; set; }
    }

}
