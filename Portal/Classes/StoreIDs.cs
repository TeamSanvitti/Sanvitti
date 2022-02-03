using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace avii.Classes
{
    [XmlRoot(ElementName = "storeids", IsNullable = true)]
    public class StoreIDs
    {
        [XmlElement(ElementName = "storeid", IsNullable = true)]
        public string StoreID { get; set; }

    }
}