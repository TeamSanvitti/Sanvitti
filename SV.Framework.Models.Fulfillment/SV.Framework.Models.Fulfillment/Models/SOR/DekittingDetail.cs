using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.SOR
{
    public class DekittingDetail
    {
        public string CustomerRequestNumber { get; set; }
        public string DekitRequestNumber { get; set; }
        public int Quantity { get; set; }
        //public int KittedSKUId { get; set; }
        public string RequestedBy { get; set; }
        public Int64 DeKittingID { get; set; }
        public string ApprovedBy { get; set; }
        //public int CompanyId { get; set; }
        public string CreatedBy { get; set; }
        public string DeKitStatus { get; set; }
        public DateTime DeKitDate { get; set; }
        public string SKU { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public List<DekittingRawSKU> RawSKUs { get; set; }
        public List<DekittingSKUESN> EsnList { get; set; }
        public string CustomerName { get; set; }
        // public string DekiitedSKU { get; set; }






    }
}
