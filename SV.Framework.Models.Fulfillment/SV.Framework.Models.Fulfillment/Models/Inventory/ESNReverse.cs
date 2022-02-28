using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.Inventory
{
    public class ESNReverse
    {
        public string KittedSKU { get; set; }
        public string RawSKU { get; set; }
        public string ESN { get; set; }
        public int ESNID { get; set; }
        public int POAssignID { get; set; }
        public int POID { get; set; }
        public int OrderDetailId { get; set; }
        public string ErrorMessage { get; set; }
        public string FulfillmentNumber { get; set; }
    }
    public class ESNskuReverse
    {
        public string CategoryName { get; set; }
        public string ItemName { get; set; }
        public string RawSKU { get; set; }
        public int ESNCount { get; set; }
        public int CurrentStock { get; set; }
        public int ProposedStock { get; set; }
        //public int ESNID { get; set; }
        //public int POAssignID { get; set; }
        //public int OrderDetailId { get; set; }
        //public string ErrorMessage { get; set; }
        //public string FulfillmentNumber { get; set; }
    }
}
