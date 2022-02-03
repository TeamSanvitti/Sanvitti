using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace avii.Classes
{
    [Serializable]
    [XmlRoot(ElementName = "EsnInventory", IsNullable = true)]
    public class SkusEsnList
    {
        [XmlElement(ElementName = "SKU", IsNullable = true)]
        public string SKU { get; set; }
        [XmlIgnore]
        public string ProductCode { get; set; }
        [XmlElement(ElementName = "ProductName", IsNullable = true)]
        public string ItemName { get; set; }


        [XmlElement(ElementName = "OpeningBalance", IsNullable = true)]
        public int? OpeningBalance { get; set; }
        [XmlElement(ElementName = "NewESN", IsNullable = true)]
        
        public int? NewESN { get; set; }
        [XmlElement(ElementName = "AvailableESN", IsNullable = true)]
        public int? AvailableESN { get; set; }
        

        [XmlElement(ElementName = "EsnProcessed", IsNullable = true)]
        public int? UsedEsnProcessed { get; set; }
        [XmlElement(ElementName = "EsnShipped", IsNullable = true)]
        public int? UsedEsnShipped { get; set; }
        [XmlElement(ElementName = "UnusedESN", IsNullable = true)]
        public int? UnusedESN { get; set; }
        [XmlIgnore]
        public int TotalESN { get; set; }
        [XmlElement(ElementName = "RmaESN", IsNullable = true)]
        public int? RmaESN { get; set; }
        [XmlIgnore]
        public int ItemCompanyGUID { get; set; }
    }
}