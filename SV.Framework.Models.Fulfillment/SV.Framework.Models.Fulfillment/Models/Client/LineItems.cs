using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.Fulfillment
{
    public class LineItems
    {
        //public int Line_no { get; set; }
        public int Qty { get; set; }
        public string SKU { get; set; }
        public string ESN { get; set; }
        public string ICCID { get; set; }
        public string BatchNumber { get; set; }


    }
}
