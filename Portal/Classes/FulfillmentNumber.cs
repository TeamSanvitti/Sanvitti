using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace avii.Classes
{
    [XmlRoot(ElementName = "fulfillmentorders", IsNullable = true)]
    public class FulfillmentNumber
    {
        [XmlElement(ElementName = "fulfillmentOrder", IsNullable = true)]
        public string FulfillmentOrder { get; set; }

    }
}