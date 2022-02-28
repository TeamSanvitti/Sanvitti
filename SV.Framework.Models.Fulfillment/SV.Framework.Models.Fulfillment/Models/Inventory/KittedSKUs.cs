using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.Inventory
{
    public class KittedSKUs
    {
        public int ItemCompanyGUID { get; set; }
        public string SKU { get; set; }
        public string KittedSKU { get; set; }
        public string DisplayName { get; set; }
        public string ManufactureCode { get; set; }
        public string SWVersion { get; set; }

    }

}
