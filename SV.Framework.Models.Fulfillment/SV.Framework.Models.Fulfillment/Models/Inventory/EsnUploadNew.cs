using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace SV.Framework.Models.Inventory
{
    public class EsnUploadNew
    {
        [XmlIgnore]
        public string ErrorMessage { get; set; }
        public string ESN { get; set; }
        public string MslNumber { get; set; }
        public string ICC_ID { get; set; }
        public string MeidHex { get; set; }
        public string MeidDec { get; set; }
        public string Location { get; set; }
        public string MSL { get; set; }
        public string OTKSL { get; set; }
        public string SerialNumber { get; set; }
        public string BoxID { get; set; }
        public string SNo { get; set; }
        [XmlIgnore]
        public bool InUse { get; set; }
        [XmlIgnore]
        public int EsnID { get; set; }

    }
}
