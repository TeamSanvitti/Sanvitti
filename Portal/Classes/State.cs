using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace avii.Classes
{
    [XmlRoot(ElementName = "State", IsNullable = true)]
    public class State
    {
        [XmlIgnore]
        public string StateName { get; set; }

        [XmlElement(ElementName = "statecode", IsNullable = true)]
        public string StateCode { get; set; }
    }
}