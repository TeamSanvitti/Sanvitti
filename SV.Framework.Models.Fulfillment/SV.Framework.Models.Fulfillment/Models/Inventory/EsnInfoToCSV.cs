using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.Inventory
{
    public class EsnInfoToCSV
    {

        public string CustomerName { get; set; }
        public string ESN { get; set; }
        public string BatchNumber { get; set; }
        public string NewSKU { get; set; }
        public string SKU { get; set; }
        public string ProductCode { get; set; }

        public string SalesOrderNumber { get; set; }
        public string ICC_ID { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public string RmaNumber { get; set; }
        public string ContainerID { get; set; }

    }
}
