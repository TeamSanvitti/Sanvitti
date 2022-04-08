using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.Inventory
{
    public class ESNReplacedInfo
    {
        public string ErrorMessage { get; set; }
        public string SKU { get; set; }
        public string MeidHex { get; set; }
        public string MeidDec { get; set; }
        public string ESN { get; set; }

        public string SerialNumber { get; set; }
        public string ItemName { get; set; }
        public string CategoryName { get; set; }
        public string Location { get; set; }

        public string BOXID { get; set; }
        public string ESNType { get; set; }
        public string UploadDate { get; set; }
        public Int64 KitID { get; set; }
    }

}
