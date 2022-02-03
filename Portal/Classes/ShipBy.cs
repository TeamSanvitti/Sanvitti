using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace avii.Classes
{
    public class ShipBy
    {
        public string ShipByCode { get; set; }
        public string ShipByText { get; set; }
        [XmlIgnore]
        public string ShipCodeNText { get; set; }

        [XmlIgnore]
        public int ShipByID { get; set; }
    }
}