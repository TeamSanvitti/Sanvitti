using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace avii.Classes
{
    public class SKUPrices
    {
        public string SKU { get; set; }
        public string ProductCode { get; set; }
        public double SKUPrice { get; set; }
        //public double AVSupplyChainPrices { get; set; }
        public double ProposePrice { get; set; }
        [XmlIgnore]
        public double SKULastPrice { get; set; }
        [XmlIgnore]
        public string UserName { get; set; }
        [XmlIgnore]
        public DateTime ChangeDate { get; set; }
        

    }
}