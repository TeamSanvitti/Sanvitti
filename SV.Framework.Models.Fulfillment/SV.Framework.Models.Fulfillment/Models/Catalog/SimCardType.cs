using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.Catalog
{

    // [Serializable]
    // [XmlRoot(ElementName = "Carriers", IsNullable = true)]
    public class SimCardType
    {

        //[XmlElement(ElementName = "CarrierGUID", IsNullable = true)]
        public int SimCardTypeID { get; set; }
        // [XmlIgnore]
        public string SimCardTypeText { get; set; }

    }
}
