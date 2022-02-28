using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.Inventory
{

    public class RawSKUInfo
    {
        public int ItemCompanyGUID { get; set; }
        public string SKU { get; set; }
        public string SWVersion { get; set; }
        public int EsnLength { get; set; }
        public int DecLength { get; set; }


    }
}
