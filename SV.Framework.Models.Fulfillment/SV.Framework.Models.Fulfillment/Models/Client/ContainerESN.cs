using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace SV.Framework.Models.Fulfillment
{
    public class ContainerESN
    {
        public string ContainerID { get; set; }
        public string ESN { get; set; }
        public string ICCID { get; set; }
        public string Location { get; set; }
        public string TrackingNumber { get; set; }
        [XmlIgnore]
        public int SNo { get; set; }

    }
    public class ContainerNonESN
    {
        public string ContainerID { get; set; }
        public string TrackingNumber { get; set; }
        
    }

}
