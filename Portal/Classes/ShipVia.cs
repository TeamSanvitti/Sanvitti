using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace avii.Classes
{
    [XmlRoot(ElementName = "purchaseorders", IsNullable = true)]
    public class ShipVia
    {
        [XmlElement(ElementName = "shipthrough", IsNullable = true)]
        public string ShipThrough { get; set; }
    }
}