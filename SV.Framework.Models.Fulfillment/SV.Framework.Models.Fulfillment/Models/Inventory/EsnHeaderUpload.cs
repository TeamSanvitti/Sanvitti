using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.Inventory
{
    public class EsnHeaderUpload
    {
        public int ESNHeaderId { get; set; }
        public string OrderNumber { get; set; }
        public string CustomerOrderNumber { get; set; }
        public string OrderDate { get; set; }
        public string ShipDate { get; set; }
        public string Shipvia { get; set; }
        public string TrackingNumber { get; set; }
        public int OrderQty { get; set; }
        public int ShipQty { get; set; }
        public string CustomerAccountNumber { get; set; }
        public decimal UnitPrice { get; set; }
        public string SKU { get; set; }
        public string Comments { get; set; }
        public bool IsESNRequired { get; set; }
        public string ReceivedAs { get; set; }
        public bool IsInspection { get; set; }
        public int UserId { get; set; }
        public string SupplierName { get; set; }
        public List<EsnUploadNew> ESNs { get; set; }
    }
}
