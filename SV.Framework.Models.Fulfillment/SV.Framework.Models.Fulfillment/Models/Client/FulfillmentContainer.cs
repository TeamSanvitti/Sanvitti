using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace SV.Framework.Models.Fulfillment
{
    public class FulfillmentContainer
    {
        [XmlIgnore]
        public string PalletID { get; set; }
        public string ContainerID { get; set; }
        [XmlIgnore]
        public int POID { get; set; }
        public string Code { get; set; }

        //public int ItemCompanyGUID { get; set; }
    }
}
