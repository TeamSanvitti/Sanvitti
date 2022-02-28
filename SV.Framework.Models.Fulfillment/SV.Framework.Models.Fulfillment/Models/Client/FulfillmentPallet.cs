using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace SV.Framework.Models.Fulfillment
{
    public class FulfillmentPallet
    {
        [XmlIgnore]
        public string Comment { get; set; }
        public string PalletID { get; set; }
        [XmlIgnore]
        public int POID { get; set; }

        //public int ItemCompanyGUID { get; set; }
    }
}
