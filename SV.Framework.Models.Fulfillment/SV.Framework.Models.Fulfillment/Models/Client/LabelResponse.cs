using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.Fulfillment
{
    public class LabelResponse
    {
        public string ErrorCode { get; set; }
        public string Comment { get; set; }
        public List<ShippingLabel> LabelList { get; set; }

    }
}
