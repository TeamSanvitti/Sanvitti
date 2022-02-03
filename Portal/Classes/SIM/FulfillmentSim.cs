using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;


namespace avii.Classes
{
    public class FulfillmentSim
    {
        public int PODID { get; set; }
        public string FulfillmentNumber { get; set; }
        public string SIM { get; set; }
        public string SKU { get; set; }

        [XmlIgnore]
        public string FMUPC { get; set; }
        [XmlIgnore]
        public string LteICCID { get; set; }
        [XmlIgnore]
        public string LleIMSI { get; set; }
        [XmlIgnore]
        public string OTKSL { get; set; }
        [XmlIgnore]
        public string AKEY { get; set; }
        
        
    }
}