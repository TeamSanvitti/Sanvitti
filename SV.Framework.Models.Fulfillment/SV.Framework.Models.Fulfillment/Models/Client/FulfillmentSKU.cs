using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace SV.Framework.Models.Fulfillment
{
    [XmlRoot(ElementName = "fulfillmentskus", IsNullable = true)]
    public class FulfillmentSKU
    {
        [XmlElement(ElementName = "fulfillmentsku", IsNullable = true)]
        public string SKU { get; set; }

    }
}
