using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace SV.Framework.Models.Catalog
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
