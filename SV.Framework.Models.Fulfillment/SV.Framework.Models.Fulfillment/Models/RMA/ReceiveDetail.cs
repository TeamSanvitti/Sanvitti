using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.RMA
{
    public class ReceiveDetail
    {
        //[XmlIgnore]
        //public int RMAReceiveGUID { get; set; }
        //public int? RMAReceiveDetailGUID { get; set; }
        //[XmlIgnore]
        //public int RmaGUID { get; set; }
        //public int? RMADetGUID { get; set; }
        //[XmlIgnore]
        public int QtyReceived { get; set; }

        public string ReceiveStatus { get; set; }
        public string ESNReceived { get; set; }
        public string ShippingTrackingNumber { get; set; }

        public string SKU { get; set; }
        public string ProductName { get; set; }
    }

}
