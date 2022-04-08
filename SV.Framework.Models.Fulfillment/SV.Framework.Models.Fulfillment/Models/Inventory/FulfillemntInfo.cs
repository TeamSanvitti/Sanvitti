using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.Inventory
{
    public class FulfillemntInfo
    {
        public string FulfillemntNumber { get; set; }
        public string FulfillemntDate { get; set; }
        public string ContactName { get; set; }
        public string PoStatus { get; set; }
        public string PoType { get; set; }
        public string ESN { get; set; }
        public string Location { get; set; }
        public string BOXID { get; set; }
        public string ESNType { get; set; }
        public string AssignmentDate { get; set; }
        public string UploadDate { get; set; }
        public Int64 KitID { get; set; }
        public string MeidHex { get; set; }
        public string MeidDec { get; set; }

        public List<FulfillmentLineItem> LineItems { get; set; }
    }

    
    

}
