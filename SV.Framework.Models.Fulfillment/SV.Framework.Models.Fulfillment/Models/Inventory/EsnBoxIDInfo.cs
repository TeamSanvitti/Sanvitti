using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.Inventory
{

    public class EsnBoxIDInfo
    {
        public int POID { get; set; }
        public int EsnID { get; set; }
        public string FulfillmentNumber { get; set; }
        public string ErrorMessage { get; set; }
        public string ESN { get; set; }
        public string HEX { get; set; }
        public string DEC { get; set; }
        public string SerialNumber { get; set; }
        public string BoxID { get; set; }
        public string ReceiveDate { get; set; }
        public string SKU { get; set; }
        public string CategoryName { get; set; }
        public int BoxItems { get; set; }
        public int AssignedBoxItems { get; set; }
    }
}
