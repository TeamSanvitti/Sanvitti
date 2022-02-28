using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace SV.Framework.Models.Fulfillment
{
    public class ContainerESNInfo
    {
        public string ContainerID { get; set; }
        public string ESN { get; set; }
        [XmlIgnore]
        public string SKU { get; set; }
        [XmlIgnore]
        public string CategoryName { get; set; }
        [XmlIgnore]
        public string ProductName { get; set; }
        public int? ItemCompanyGUID { get; set; }
        public string BatchNumber { get; set; }
        [XmlIgnore]
        public string ErrorMessage { get; set; }
        public string ICCID { get; set; }
        public string TrackingNumber { get; set; }
        public string Location { get; set; }
        [XmlIgnore]
        public string LocationMessage { get; set; }
        [XmlIgnore]

        public int SNo { get; set; }
        [XmlIgnore]

        public Int64 KITID { get; set; }

    }
}
