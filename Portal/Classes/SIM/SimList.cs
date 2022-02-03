using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Serialization;


namespace avii.Classes
{
    //[Serializable]
    public class SimList
    {
        public string SIM { get; set; }
        public string ESN { get; set; }
        [XmlIgnore]
        public string SKU { get; set; }
    }
}