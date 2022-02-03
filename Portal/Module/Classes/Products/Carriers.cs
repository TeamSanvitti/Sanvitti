using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace avii.Classes
{
    [Serializable]
    [XmlRoot(ElementName = "Carriers", IsNullable = true)]
    public class Carriers
    {

        [XmlElement(ElementName = "CarrierGUID", IsNullable = true)]
        public int? CarrierGUID { get; set; }
        [XmlIgnore]
        public string CarrierName { get; set; }
        [XmlIgnore]
        public string CarrierLogo { get; set; }
        [XmlIgnore]
        public bool Active { get; set; }

    }
}