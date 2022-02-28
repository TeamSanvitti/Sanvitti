using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.Inventory
{

    public class ESNAuthorizatedInfo
    {
        public int ESNAuthorizationID { get; set; }
        public string SKU { get; set; }
        public string ProductType { get; set; }
        public string KittedSKU { get; set; }
        public string SWVersion { get; set; }
        public string DisplayName { get; set; }
        public string ManufactureCode { get; set; }
        public int EsnCount { get; set; }
        public int SquenceNumber { get; set; }
        public string ESNDATA { get; set; }
        public string ESNXml { get; set; }
        public string CreatedBy { get; set; }
        public string RunNumber { get; set; }
        public DateTime CreateDate { get; set; }

    }
}
