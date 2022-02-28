using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.Inventory
{
    public class PurchaseOrderKittingSummary
    {
        public string PalletID { get; set; }
        public string ContainerID { get; set; }
        public string Username { get; set; }
        public string FulfillmentNumber { get; set; }
        public string SKU { get; set; }
        public string BoxID { get; set; }
        public DateTime KittedCSTDate { get; set; }
        public int EsnCount { get; set; }

    }

}
