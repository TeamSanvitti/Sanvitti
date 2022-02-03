using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace avii.Classes
{
    public class SKUStock
    {

        public string SKU { get; set; }
        public int OpeningBalance { get; set; }
        public int StockInHand { get; set; }
        public int StockAssigned { get; set; }
        public int StockReceived { get; set; }
        public int StockShipped { get; set; }
        public int BadStock { get; set; }
        public int PendingAssignment { get; set; }
    }
}