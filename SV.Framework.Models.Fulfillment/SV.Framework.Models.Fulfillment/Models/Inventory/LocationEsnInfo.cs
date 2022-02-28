using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.Inventory
{
    public class LocationEsnInfo
    {
        public string CustomerName { get; set; }
        public string SKU { get; set; }
        public int EsnCount { get; set; }
        public string ReceivedBy { get; set; }
        public DateTime ReceivedDate { get; set; }
        //public string ESN { get; set; }
        //public string DEC { get; set; }
        //public string HEX { get; set; }
        //public string BOXID { get; set; }
        //public string SerialNumber { get; set; }
        //public string FulfillmentNumber { get; set; }
        //public string RmaNumber { get; set; }
        //public int RmaGUID { get; set; }
        //public int POID { get; set; }
    }
}
