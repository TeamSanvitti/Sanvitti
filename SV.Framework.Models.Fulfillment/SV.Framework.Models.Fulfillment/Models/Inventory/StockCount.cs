using System;

namespace SV.Framework.Models.Inventory
{
    public class StockCount
    {
        public string CategoryName { get; set; }
        public string SKU { get; set; }
        public string ItemName { get; set; }
        public DateTime StockDate { get; set; }
        public DateTime RefreshDate { get; set; }
        public int OpeningBalance { get; set; }
        public int StockReceived { get; set; }
        public int StockAssigned { get; set; }
        public int StockReassignment { get; set; }
        public int DekitCount { get; set; }
        public int UnProvisioningCount { get; set; }
        public int ClosingBalance { get; set; }
        public int DiscardedSKU { get; set; }

    }
    public class StockCountCSV
    {
        public string ReceivedDate { get; set; }
        public string CategoryName { get; set; }

        public string SKU { get; set; }
        public string ProductName { get; set; }
        public int OpeningBalance { get; set; }
        public int StockReceived { get; set; }

        public int StockAssigned { get; set; }
        public int StockReassignment { get; set; }
        public int ClosingBalance { get; set; }

    }

}
