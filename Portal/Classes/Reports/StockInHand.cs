using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace avii.Classes
{
    public class StockInHand
    {
        public string ItemCode { get; set; }
        public string ItemDescription { get; set; }
        public string WarehouseCode { get; set; }
        public double TotalQtyInHand { get; set; }
        public double QtyOnPurchaseOrder { get; set; }
        public double QtyOnBackOrder { get; set; }
        public double QtyOnSalesOrder { get; set; }
    }
}