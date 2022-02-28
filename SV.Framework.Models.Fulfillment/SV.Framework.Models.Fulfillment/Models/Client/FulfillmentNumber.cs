using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace SV.Framework.Models.Fulfillment
{
    [XmlRoot(ElementName = "fulfillmentorders", IsNullable = true)]
    public class FulfillmentNumber
    {
        [XmlElement(ElementName = "fulfillmentOrder", IsNullable = true)]
        public string FulfillmentOrder { get; set; }

    }
}
