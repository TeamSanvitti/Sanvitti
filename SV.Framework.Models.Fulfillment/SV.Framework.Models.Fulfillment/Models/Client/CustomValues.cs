using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace SV.Framework.Models.Fulfillment
{
    public class CustomValues
    {
        public int? POD_ID { get; set; }
        public decimal? CustomValue { get; set; }
        [XmlIgnore]
        public string SKU { get; set; }
        [XmlIgnore]
        public string ProductName { get; set; }
        [XmlIgnore]
        public int Quantity { get; set; }
        [XmlIgnore]
        public string TrackingNumber { get; set; }



    }
}
