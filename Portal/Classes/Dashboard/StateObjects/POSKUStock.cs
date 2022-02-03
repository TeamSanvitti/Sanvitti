using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace avii.Classes
{
    public class POSKUStock
    {
        public string SKU { get; set; }
        public string CategoryName { get; set; }
        public string ProductName { get; set; }

        public int StockInHand { get; set; }
        public int Qty { get; set; }

    }
}