using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.SOR
{
    public class DekitServiceOrder
    {
        public string CustomerRequestNumber { get; set; }
        public string DekitRequestNumber { get; set; }
        //public int Quantity { get; set; }
        //public int KittedSKUId { get; set; }
        public int RequestedBy { get; set; }
        public Int64 DeKittingID { get; set; }
        public int ApprovedBy { get; set; }
        public int CompanyId { get; set; }
        public int CreatedBy { get; set; }
        public int DeKitStatusID { get; set; }
        public DateTime DeKitDate { get; set; }
        public DateTime ApprovalDate { get; set; }
        public DateTime RequestedDate { get; set; }
        public string DeKitStatus { get; set; }
        public List<DekitOrderDetail> DekitDetails { get; set; }

        public List<DekitESN> EsnList { get; set; }

    }
}
