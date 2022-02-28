using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.SOR
{
    public class KittedRawSKU
    {
        //public bool IsESNRequired { get; set; }
        public string SKU { get; set; }
        //public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public int Quantity { get; set; }
        public int ItemcompanyGUID { get; set; }
        public int MappedItemcompanyGUID { get; set; }
        public bool IsESNRequired { get; set; }
        public string StockMsg { get; set; }

        public string MappedSKU { get; set; }
        //public string MappedProductName { get; set; }
        //public string MappedCategoryName { get; set; }
    }

}
