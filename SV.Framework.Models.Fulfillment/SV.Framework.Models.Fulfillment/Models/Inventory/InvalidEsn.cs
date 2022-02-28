using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.Inventory
{
    public class InvalidEsn
    {
        public string SKU { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public string ESN { get; set; }
        public string MEID { get; set; }
        public string FulfillmentNumber { get; set; }
        public string ESNColor { get; set; }
        public string MeidColor { get; set; }
    }
    public class InvalidEsnCSV
    {
        public string CategoryName { get; set; }

        public string ProductName { get; set; }
        public string SKU { get; set; }

        public string ESN { get; set; }
        public string MEID { get; set; }
        public string FulfillmentNumber { get; set; }
    }
}
