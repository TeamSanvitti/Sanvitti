using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.Fulfillment
{
    public class ShippingLabel
    {
        public string SKU { get; set; }
        public string IMEI { get; set; }
        public string ICCID { get; set; }
        public string UPC { get; set; }
    }
}
