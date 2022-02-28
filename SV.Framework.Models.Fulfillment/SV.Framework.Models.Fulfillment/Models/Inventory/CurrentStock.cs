using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace SV.Framework.Models.Inventory
{
    public class CurrentStock
    {
        public string SKU { get; set; }
        public string CategoryName { get; set; }
        public string ProductName { get; set; }

        public int StockInHand { get; set; }
        [XmlIgnore]
        public bool IsDisable { get; set; }

    }
}
