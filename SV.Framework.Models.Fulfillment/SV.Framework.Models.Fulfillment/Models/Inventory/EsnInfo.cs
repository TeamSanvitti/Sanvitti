using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.Inventory
{
    public class EsnInfo
    {
        public int PO_ID { get; set; }
        public int RmaGuid { get; set; }

        public string ESN { get; set; }
        public string SKU { get; set; }
        public string Item_Code { get; set; }
        public string MslNumber { get; set; }
        public string AerovoiceSalesOrderNumber { get; set; }
        public string MEID { get; set; }
        public string HEX { get; set; }
        public string AKEY { get; set; }
        public string AVPO { get; set; }
        public string OTKSL { get; set; }
        public string ICC_ID { get; set; }
        public string LTE_IMSI { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public string RmaNumber { get; set; }
        public string SimNumber { get; set; }
        public string CustomerName { get; set; }
        public string BatchNumber { get; set; }
        public bool IsESN { get; set; }
        public string NewSKU { get; set; }
        public string ContainerID { get; set; }
        public Int64 KITID { get; set; }
        public string Location { get; set; }
        public string BoxID { get; set; }

    }
}
