using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.Inventory
{
    public class PurchaseOrderKitting
    {
        public int POAssignId { get; set; }
        public DateTime PO_Date { get; set; }
        public string POStatus { get; set; }
        public string FulfillmentNumber { get; set; }
        public int QuantityCount { get; set; }
        public string ContainerID { get; set; }
        public string PalletID { get; set; }
        public string ESN { get; set; }
        public string DEC { get; set; }
        public string HEX { get; set; }
        public string KittedSKU { get; set; }
        public string BoxNumber { get; set; }
        public int POID { get; set; }
        public string ErrorMessage { get; set; }
        public string ShipToDate { get; set; }
        public string EsnSource { get; set; }
    }
    
    
    

}
