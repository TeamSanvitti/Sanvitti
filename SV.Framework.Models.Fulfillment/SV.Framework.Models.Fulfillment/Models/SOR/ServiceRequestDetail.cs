using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.SOR
{
    public class ServiceRequestDetail
    {
        public string Status { get; set; }
        public int Quantity { get; set; }
        public int ItemcompanyGUID { get; set; }
        public List<KittedRawSKU> RawSKUs { get; set; }

    }

}
