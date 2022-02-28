using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace SV.Framework.Models.Fulfillment
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
