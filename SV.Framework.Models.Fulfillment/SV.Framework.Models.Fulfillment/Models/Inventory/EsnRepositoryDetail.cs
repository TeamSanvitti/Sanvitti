using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.Inventory
{

    public class EsnRepositoryDetail
    {
        public string ESN { get; set; }
        public string SKU { get; set; }
        public DateTime UploadDate { get; set; }
        public string FulfillmentNumber { get; set; }
        public DateTime FulfillmentDate { get; set; }
        public DateTime ShipDate { get; set; }
        public string FulfillmentStatus { get; set; }
        public string RmaNumber { get; set; }
        public string RmaStatus { get; set; }
        public string RmaEsnStatus { get; set; }
        public DateTime RmaDate { get; set; }

    }
}
