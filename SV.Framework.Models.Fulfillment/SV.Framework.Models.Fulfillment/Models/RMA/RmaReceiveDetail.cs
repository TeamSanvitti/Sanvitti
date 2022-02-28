using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace SV.Framework.Models.RMA
{
    public class RmaReceiveDetail
    {
        [XmlIgnore]
        public int RMAReceiveGUID { get; set; }
        public int? RMAReceiveDetailGUID { get; set; }
        [XmlIgnore]
        public int RmaGUID { get; set; }
        public int? RMADetGUID { get; set; }
        public int? ReceiveStatusID { get; set; }
        [XmlIgnore]
        public string ReceiveStatus { get; set; }

        [XmlIgnore]
        public int Quantity { get; set; }
        public int? QtyReceived { get; set; }
        [XmlIgnore]
        public string RmaNumber { get; set; }
        public string ESNReceived { get; set; }
        public string ShippingTrackingNumber { get; set; }
        [XmlIgnore]
        public string SKU { get; set; }
        [XmlIgnore]
        public string ItemName { get; set; }
    }

}
