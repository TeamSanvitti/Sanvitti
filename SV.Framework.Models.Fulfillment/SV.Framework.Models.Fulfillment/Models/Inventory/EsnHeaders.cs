using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.Inventory
{
    public class EsnHeaders
    {
        public string UserName { get; set; }
        public bool IsESN { get; set; }
        public string CategoryName { get; set; }
        public int CategoryID { get; set; }
        public int CompanyID { get; set; }
        public int ESNHeaderId { get; set; }
        public string OrderNumber { get; set; }
        public string CustomerOrderNumber { get; set; }
        public string OrderDate { get; set; }
        public string ShipDate { get; set; }
        public string Shipvia { get; set; }
        public string TrackingNumber { get; set; }
        public int OrderQty { get; set; }
        public int ShipQty { get; set; }
        public int AssignedQty { get; set; }
        public int ItemCompanyGUID { get; set; }
        public decimal UnitPrice { get; set; }
        public string SKU { get; set; }
        public String CompanyAccountNumber { get; set; }
        public List<EsnUploadNew> EsnList { get; set; }
        public string ProductName { get; set; }
        public string ReceivedAs { get; set; }
        public bool IsInspection { get; set; }
        //public string ICC_ID { get; set; }
    }
}
