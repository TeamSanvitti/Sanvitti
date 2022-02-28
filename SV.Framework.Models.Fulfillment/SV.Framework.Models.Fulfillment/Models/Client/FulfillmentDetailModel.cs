using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.Fulfillment
{
    public class FulfillmentDetailModel
    {
        public string BlankColumn { get; set; }
        public string MeidDec { get; set; }
        public string MeidIMEI { get; set; }
        public string MeidHex { get; set; }
        public string SKU { get; set; }
        public string ModelNumber { get; set; }
        public string Status { get; set; }
        public string PalletManDate { get; set; }
        public string PalletID { get; set; }
        public string CartonID { get; set; }
        public string CartonManDate { get; set; }
        public string SerialNumber { get; set; }
        public string Destination { get; set; }
    }
}
