using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace SV.Framework.Models.SOR
{

    public class DekitESN
    {
        public string ESN { get; set; }
        public string ICCID { get; set; }
        public string WhLocation { get; set; }
        [XmlIgnore]
        public string SKU { get; set; }
        [XmlIgnore]
        public string ValidationMsg { get; set; }
        //[XmlIgnore]
        public int ItemCompanyGUID { get; set; }
        //[XmlIgnore]
        public int MappedItemCompanyGUID { get; set; }
        [XmlIgnore]
        public string MappedSKU { get; set; }
        [XmlIgnore]
        public string ICCIDValidationMsg { get; set; }


    }
}
