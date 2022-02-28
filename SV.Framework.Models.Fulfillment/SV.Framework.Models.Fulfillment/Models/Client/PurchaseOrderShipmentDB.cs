using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.Fulfillment
{
    public class PurchaseOrderShipmentDB
    {
        public string APIAddress { get; set; }
        public string CustomerOrderNumber { get; set; }

        public int UserID { get; set; }
        public int PO_ID { get; set; }
        public string PO_NUM { get; set; }
        public string PO_Date { get; set; }
        public string PO_Status { get; set; }
        public string Ship_Via { get; set; }
        public int Line_no { get; set; }
        public int Qty { get; set; }
        public string SKU { get; set; }
        public string ShipDate { get; set; }
        public string ESN { get; set; }
        public string ICC_ID { get; set; }
        public string BatchNumber { get; set; }
        public string TrackingNumber { get; set; }
        public string AcknowledgmentSent { get; set; }
        public string ShipmentType { get; set; }
        public int LineNumber { get; set; }
        public string UPC { get; set; }
        public string ContainerID { get; set; }
        public string PoType { get; set; }
        public string ShipPackage { get; set; }
        public decimal ShipPrice { get; set; }
        public decimal ShipWeight { get; set; }
        public string ContactName { get; set; }
        public string ShipMethod { get; set; }
        public string CategoryName { get; set; }
        public string ItemName { get; set; }

    }
}
