using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace avii.Classes
{
    [XmlRoot(ElementName = "fulfillmentskus", IsNullable = true)]
    public class FulfillmentSKU
    {
        [XmlElement(ElementName = "fulfillmentsku", IsNullable = true)]
        public string SKU { get; set; }

    }
}