using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.Fulfillment
{
    public class FulfillmentEsNInfo
    {
        public string FulfillmentNumber { get; set; }
        public int Qty { get; set; }
        public string ShippedDate { get; set; }
        public List<FulfillmentEsn> EsnList { get; set; }
    }
    public class FulfillmentEsn
    {
        public string ESN { get; set; }
    }
}
