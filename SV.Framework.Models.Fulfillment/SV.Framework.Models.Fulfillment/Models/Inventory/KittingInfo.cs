using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.Inventory
{
    public class KittingInfo
    {
        public List<PurchaseOrderKitting> PurchaseOrderKits { get; set; }
        public List<KitRawSKU> RawSKUs { get; set; }

    }
}
